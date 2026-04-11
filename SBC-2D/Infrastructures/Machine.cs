using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Bsk;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Error;
using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;
using static SBC_2D.Shared.Enums;

public class Machine
{
    private readonly DeviceManager _deviceManager;
    private readonly SystemIo _systemIo;
    private readonly Setup _setup;
    private readonly ErrorManager _errorManager;
    private Task _autoRunTask;
    private CancellationTokenSource _ctsAutoRun;
    public Recipe Recipe { get; set; }
    public string BskNo { get; set; }
    public int Quantity { get; set; }
    public string BoardThickness { get; set; }
    public string[] Barcodes { get; private set; }
    public int ExecutedBskResult { get; set; }
    public MachineStatus Status { get; private set; }
    public AutoRunStep CurrentStep { get; private set; }
    public string StepMessage { get; private set; }

    public event Action<MachineStatus> StatusChanged;
    public event Action<string> StepMessageChanged;
    public event Action<double> ThicknessMeasured;
    public event Action BarcodeRereadRequested;
    public event Action<string[]> BarcodesReaded;

    public bool IsAutoRunning
    {
        get => _autoRunTask != null && !_autoRunTask.IsCompleted;
    }

    public Machine(ErrorManager errorManager, DeviceManager deviceManager, SystemIo systemIo, Setup setup)
    {
        _errorManager = errorManager;
        _deviceManager = deviceManager;
        _setup = setup;
        _systemIo = systemIo;
    }

    public void Initialize()
    {
        SetStatus(MachineStatus.Idle);
        StepMessage = string.Empty;
        //BskNo = string.Empty;
    }

    public Task StartAutoRunAsync()
    {
        //事前判斷Recipe
        if (IsAutoRunning)
            return Task.CompletedTask;
        _ctsAutoRun = new CancellationTokenSource();
        CurrentStep = AutoRunStep.檢查輸送帶;
        _autoRunTask = RunAutoAsync(_ctsAutoRun.Token);
        return Task.CompletedTask;
    }

    public async Task StopAutoRunAsync()
    {
        if (_ctsAutoRun == null) return;
        _ctsAutoRun.Cancel();
        try
        {
            await _autoRunTask.ConfigureAwait(false);
        }
        catch (OperationCanceledException) { }
        catch (Exception)
        {
        }
        finally
        {
            _ctsAutoRun.Dispose();
            _ctsAutoRun = null;
            _autoRunTask = null;
            SetStatus(MachineStatus.Idle);
        }
    }

    private async Task RunAutoAsync(CancellationToken token)
    {
        SetStatus(MachineStatus.Running);
        try
        {
            while (!token.IsCancellationRequested)
            {
                switch (CurrentStep)
                {
                    case AutoRunStep.檢查輸送帶:
                    {
                        await CheckConveyor(token).ConfigureAwait(false);
                        break;
                    }

                    case AutoRunStep.進板前準備:
                    {
                        await PreparationBoardEntry(token).ConfigureAwait(false);
                        break;
                    }

                    case AutoRunStep.等待進板:
                    {
                        await WaitBoardEntry(token).ConfigureAwait(false);
                        break;
                    }

                    case AutoRunStep.等待流板到位:
                    {
                        await WaitBoardInplace(token).ConfigureAwait(false);
                        break;
                    }

                    case AutoRunStep.測量板子厚度:
                    {
                        await MeasureThickness(token).ConfigureAwait(false);
                        break;
                    }

                    case AutoRunStep.讀取條碼:
                    {
                        await ScanBarcode(token).ConfigureAwait(false);
                        break;
                    }

                    case AutoRunStep.MakeA3XML:
                    {
                        var xmlPaths = await MakeXmlPath(token).ConfigureAwait(false);
                        await SafeCopyXml(xmlPaths, token);
                        CurrentStep = AutoRunStep.讀取XML及擷取BSK資訊;
                        break;
                    }

                    case AutoRunStep.讀取XML及擷取BSK資訊:
                    {

                        break;
                    }

                    case AutoRunStep.錯誤流程:
                    {
                        SetStatus(MachineStatus.Alarm);
                        while (!token.IsCancellationRequested && _errorManager.ActiveErrors.Count != 0)
                        {
                            await Task.Delay(200, token).ConfigureAwait(false);
                        }
                        SetStatus(MachineStatus.Idle);
                        return;
                    }
                    //case AutoRunStep.判斷模式:
                    //await Task.Delay(50, token).ConfigureAwait(false);
                    //break;
                    //case AutoRunStep.進板前準備:
                    //await Task.Delay(100, token).ConfigureAwait(false);
                    //break;
                    //case AutoRunStep.等待進板:
                    //await WaitBoardAsync(token).ConfigureAwait(false);
                    //break;
                    //case AutoRunStep.讀取條碼:
                    //await ScanBarcodeAsync(token).ConfigureAwait(false);
                    //break;
                    //case AutoRunStep.Change_A3_XML:
                    //await Task.Delay(50, token).ConfigureAwait(false);
                    //break;
                    //case AutoRunStep.將板子移至下游:
                    //await Task.Delay(50, token).ConfigureAwait(false);
                    //break;
                    //case AutoRunStep.測量薄板厚度:
                    //await Task.Delay(50, token).ConfigureAwait(false);
                    //break;
                    //case AutoRunStep.錯誤流程:
                    //// if error occurred, handle it; otherwise skip
                    //if (CurrentError != ErrorCode.NoError)
                    //{
                    //    await HandleErrorFlow(token).ConfigureAwait(false);
                    //    // after handling error, break outer loop
                    //    token.ThrowIfCancellationRequested();
                    //}
                    //break;
                    //case AutoRunStep.停止自動:
                    //// finishing step
                    //await CompleteAsync(token).ConfigureAwait(false);
                    //break;
                    //default:
                    //await Task.Delay(10, token).ConfigureAwait(false);
                    //break;
                }
                await Task.Delay(50, token).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException)
        {
            // normal cancellation
        }
        catch (Exception ex)
        {
            // report and go to alarm
            //ReportError(ErrorCode.E1, $"AutoRun exception: {ex.Message}");
            SetStatus(MachineStatus.Alarm);
            return;
        }

        if (!token.IsCancellationRequested)
            SetStatus(MachineStatus.Idle);
    }

    private async Task CheckConveyor(CancellationToken token)
    {
        SetStepMessage("自動流程開始");
        bool isCvCleared = !GetDi(2) && !GetDi(3) && !GetDi(6) && !GetDi(10);
        if (!isCvCleared)
        {
            ReportError(ErrorCode.啟動失敗_輸送帶未清空, "軟體Auto已停止，請移除板子");
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        CurrentStep = AutoRunStep.進板前準備;
        await Task.Delay(1, token);
    }

    private async Task PreparationBoardEntry(CancellationToken token)
    {
        if (Recipe.IsMapModeBypass)
        {
            SetStepMessage("Conveyor Mode");
            SetStepMessage("止檔氣壓缸下降");
            ControlDo(4, false);
            SetStepMessage("開啟要版訊號");
            ControlDo(5, true);
            SetStepMessage("關閉停板訊號");
            ControlDo(3, false);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!_ctsAutoRun.IsCancellationRequested)
            {
                bool hasStopperDroped = GetDi(4) && !GetDi(5);
                if (stopwatch.ElapsedMilliseconds > _setup.ProductionConfig.Timeout_WaitStopper)
                {
                    bool hasStopperAbnormal = (!GetDi(4) && !GetDi(5)) || (GetDi(4) && GetDi(5));
                    if (hasStopperAbnormal)
                        ReportError(ErrorCode.止擋氣壓缸異常, "請檢查訊號、氣源、機構");
                    else if (!hasStopperDroped)
                        ReportError(ErrorCode.止擋氣壓缸未降落, "請檢查氣源和機構");
                    if (!hasStopperDroped || !hasStopperAbnormal)
                    {
                        CurrentStep = AutoRunStep.錯誤流程;
                        return;
                    }
                }
                else
                {
                    if (hasStopperDroped)
                    {
                        CurrentStep = AutoRunStep.等待進板;
                        return;
                    }
                }
                await Task.Delay(1, token);
            }
        }
        else
        {
            SetStepMessage("XML Mapping Mode");
            SetStepMessage("止檔氣壓缸上升");
            ControlDo(4, true);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!_ctsAutoRun.IsCancellationRequested)
            {
                bool hasStopperRaised = !GetDi(4) && GetDi(5);
                if (stopwatch.ElapsedMilliseconds > _setup.ProductionConfig.Timeout_WaitStopper)
                {
                    bool hasStopperAbnormal = (!GetDi(4) && !GetDi(5)) || (GetDi(4) && GetDi(5));
                    if (hasStopperAbnormal)
                        ReportError(ErrorCode.止擋氣壓缸異常, "請檢查訊號、氣源、機構");
                    else if (!hasStopperRaised)
                        ReportError(ErrorCode.止擋氣壓缸上升未到位, "請檢查氣源和機構");
                    if (!hasStopperRaised || !hasStopperAbnormal)
                    {
                        CurrentStep = AutoRunStep.錯誤流程;
                        return;
                    }
                }
                else
                {
                    if (hasStopperRaised)
                    {
                        SetStepMessage("開啟停板訊號");
                        ControlDo(3, true);
                        SetStepMessage("開啟要板訊號");
                        ControlDo(5, true);
                        CurrentStep = AutoRunStep.等待進板;
                        return;
                    }
                }
                await Task.Delay(1, token);
            }
        }
        await Task.Delay(1, token);
    }

    private async Task WaitBoardEntry(CancellationToken token)
    {
        SetStepMessage("等待進板");
        while (!_ctsAutoRun.IsCancellationRequested)
        {
            bool isBoardEntered = GetDi(6);
            if (isBoardEntered)
            {
                SetStepMessage("進板訊號ON");
                if (!Recipe.IsMapModeBypass)
                {
                    CurrentStep = AutoRunStep.等待流板到位;
                    return;
                }
                else
                {
                    SetStepMessage("關閉要版訊號");
                    ControlDo(5, false);
                    CurrentStep = AutoRunStep.等待流板抵達出口;
                    return;
                }
            }
            await Task.Delay(1, token);
        }
        await Task.Delay(1, token);
    }

    private async Task WaitBoardInplace(CancellationToken token)
    {
        SetStepMessage("等待流板到位");
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        while (!_ctsAutoRun.IsCancellationRequested)
        {
            bool isBoardInPlace = GetDi(2);
            if (stopwatch.ElapsedMilliseconds > _setup.ProductionConfig.Timeout_WaitStopper)
            {
                ReportError(ErrorCode.等待流板到位已超時, "請檢查是否卡板");
                CurrentStep = AutoRunStep.錯誤流程;
                return;
            }
            else
            {
                if (isBoardInPlace)
                {
                    SetStepMessage("開啟停板按鈕訊號");
                    ControlDo(2, true);
                    await Task.Delay(_setup.ProductionConfig.Delay_BoardStopAck, token);
                    SetStepMessage("關閉停板按鈕訊號");
                    ControlDo(2, false);
                    await Task.Delay(_setup.ProductionConfig.Delay_BoardStopAck, token);
                    //長度Sensor是檢查1Strip，而2Strip會使用載具(不用檢查)
                    bool isBoardLengthNg = Recipe.PcbBlocksY == 1 && GetDi(9);
                    if (isBoardLengthNg)
                    {
                        ReportError(ErrorCode.板子長度NG, "可能發生疊板，請移除板子");
                        CurrentStep = AutoRunStep.錯誤流程;
                        return;
                    }
                    else
                    {
                        SetStepMessage("流板已到位，載板長度OK");
                        SetStepMessage("關閉停板訊號");
                        ControlDo(3, false);
                        CurrentStep = AutoRunStep.測量板子厚度;
                        return;
                    }
                }
            }
        }
        return;
    }

    private async Task MeasureThickness(CancellationToken token)
    {
        if (Recipe.IsLdsBypass)
        {
            CurrentStep = AutoRunStep.讀取條碼;
            return;
        }
        SetStepMessage("開始測量薄板厚度");
        IDevice device = _deviceManager.Devices
            .FirstOrDefault(d => d.Name == DeviceNames.LaserDisplacementSensor);
        if (!(device is Dlen1 dlen1))
        {
            ReportError(ErrorCode.程式錯誤_型態不相符, $"{nameof(IDevice)}不等於{nameof(Dlen1)}");
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        var datas = dlen1.MS(_setup.ProductionConfig.Timeout_MeasuThickness);
        if (!string.IsNullOrEmpty(datas.error))
        {
            ReportError(ErrorCode.板厚量測異常_收到錯誤代碼ER, datas.error);
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        //因為dlen1是網路模組，回傳兩個讀頭iL-S065的數值
        if (datas.values.Count != 2)
        {
            ReportError(ErrorCode.板厚量測異常_回傳的讀頭數量不符, $"總數為{datas.values.Count} != 2");
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        var id0 = datas.values[0];
        var id1 = datas.values[1];
        for (int i = 0; i < datas.values.Count; i++)
        {
            var id = datas.values[i];
            if (id.status == -1 || id.value == null)
            {
                ReportError(
                    ErrorCode.板厚量測異常_回傳數值無法解析,
                    $"請程式人員檢查讀頭[{i}]的回傳資料"
                );
                CurrentStep = AutoRunStep.錯誤流程;
            }
            if (id.status == 3)
            {
                ReportError(
                    ErrorCode.板厚量測異常_讀頭狀態為Error,
                    $"請程式人員檢查讀頭[{i}]的回傳資料"
                );
                CurrentStep = AutoRunStep.錯誤流程;
            }
        }
        if (CurrentStep == AutoRunStep.錯誤流程)
            return;

        var thickness = Recipe.ThicknessZeroBias + (id0.value + id1.value);
        var upperLimit = Recipe.Thickness + Recipe.ThicknessPosTolerance;
        if (thickness >= upperLimit)
        {
            ReportError(ErrorCode.板厚數值超過設定上限, "可能發生疊板或翹板");
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        ThicknessMeasured?.Invoke((double)thickness.Value / 1000);
        await Task.Delay(1, token).ConfigureAwait(false);
    }

    private async Task ScanBarcode(CancellationToken token)
    {
        string[] barcodes = new string[Recipe.PcbBlocksY]; //命名不好，此為Strip數量，Y方向
        IBarcodeReaderDevice reader = null;
        string findName = string.Empty;
        string position = string.Empty;
        if (!Recipe.IsUpperBrBypass)
        {
            position = "Upper";
            findName = DeviceNames.UpperBarcodeReader;
        }
        else if (!Recipe.IsLowerBrBypass)
        {
            position = "Lower";
            findName = DeviceNames.LowerBarcodeReader;
        }
        foreach (var device in _deviceManager.Devices)
        {
            if (device is IBarcodeReaderDevice br)
            {
                if (br.Name == findName)
                {
                    reader = br;
                    break;
                }
            }
        }
        if (reader == null)
        {
            ReportError(ErrorCode.程式錯誤_型態不相符, $"查無此{position} barcode reader");
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        SetStepMessage($"{reader.Name}開始讀取{barcodes.Length}個條碼");
        try
        {
            //1. KeyenceReader可以設定一次讀幾個Code，且可設定依據畫面中的條碼位置編號(ex: 由上到下)
            //2. KeyenceReader可以設定單次觸發的讀取時間，此次為5秒
            //3. KeyenceReader讀取的條碼數達到設定值就會提早回傳，其它情況就要等到讀取時間結束
            string result = reader.ReadBarcodes();
            if (result == "ERROR")
            {
                ReportError(ErrorCode.條碼機回傳非數值, $"接收到ERROR，請調整條碼機");
                BarcodeRereadRequested?.Invoke();
                CurrentStep = AutoRunStep.錯誤流程;
                return;
            }
            if (reader is KeyenceBarcodeReader)
                barcodes = result.Split(',');
            if (barcodes.Length == 0)
                throw new Exception($"{position} barcode reader 回傳空白文字");
        }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
        {
            ReportError(ErrorCode.條碼機讀取超時, $"位置 = {position}, 名稱 = {reader.Name}");
            BarcodeRereadRequested?.Invoke();
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.NotConnected)
        {
            ReportError(ErrorCode.條碼機連線已中斷, $"位置 = {position}, 名稱 = {reader.Name}");
            BarcodeRereadRequested?.Invoke();
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        catch (Exception ex)
        {
            ReportError(ErrorCode.程式錯誤_條碼機讀取異常, "捕捉到例外，請程式人員處理");
            BarcodeRereadRequested?.Invoke();
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        if (Recipe.PcbBlocksY != barcodes.Length)
        {
            ReportError(ErrorCode.讀取到的條碼數量與設定條數不符, "");
            BarcodeRereadRequested?.Invoke();
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        Barcodes = barcodes;
        BarcodesReaded?.Invoke(barcodes);
        SetStepMessage($"{position} barcode reader 已讀取到{barcodes.Length}個條碼");
        CurrentStep = AutoRunStep.MakeA3XML;
        await Task.Delay(1, token).ConfigureAwait(false);
        return;
    }

    private async Task<List<string>> MakeXmlPath(CancellationToken token)
    {
        List<string> filePath = new List<string>();
        foreach (var barcode in Barcodes)
        {
            try
            {
                var parts = barcode.Split('-');
                if (parts.Length != 4)
                {
                    ReportError(ErrorCode.Barcode格式錯誤, "破折號分隔數量不等於4");
                    CurrentStep = AutoRunStep.錯誤流程;
                    return filePath;
                }
                string folderName = parts[1] + _setup.PathConfig.InsertType + parts[2];
                string path = Path.Combine(_setup.PathConfig.XmlDir, folderName, barcode + ".XML");
                filePath.Add(path);
            }
            catch (Exception ex)
            {
                //改codeexception事件，並跳到錯誤流程
                ReportError(ErrorCode.程式錯誤_捕捉到執行例外, ex.Message);
                CurrentStep = AutoRunStep.錯誤流程;
            }
        }
        await Task.Delay(1, token).ConfigureAwait(false);
        return filePath;
    }


    private async Task SafeCopyXml(List<string> filePaths, CancellationToken token)
    {
        try
        {
            string tempDir = _setup.PathConfig.TempXmlDir;
            foreach (var file in new DirectoryInfo(tempDir).GetFiles())
                file.Delete();
            foreach (var path in filePaths)
            {
                string fileName = Path.GetFileName(path ?? string.Empty);
                string tempPath = Path.Combine(tempDir, fileName);
                File.Copy(path, tempPath, true);
            }
        }
        catch (Exception ex)
        {
            ReportError(ErrorCode.程式錯誤_捕捉到執行例外, ex.Message);
            CurrentStep = AutoRunStep.錯誤流程;
        }
        await Task.Delay(1, token).ConfigureAwait(false);
        return;
    }

    private async Task ExtractBakNumbers(CancellationToken token)
    {
        !!!!!!!!!!!!!!!!!!
        int startNo = 1;
        int totalCount = 0;
        int[] skipNos = new int[0];
        string tempDir = _setup.PathConfig.TempXmlDir;
        bool isError = false;
        FileInfo[] xmlFiles = new DirectoryInfo(tempDir).GetFiles(); //Strip

        foreach (FileInfo file in xmlFiles)
        {
            SetStepMessage($"開始讀取{file.Name}");
            string path = file.FullName;
            var bskab = new BskArrayBuilder(path);
            if (!bskab.Phrase())
            {
                isError = true;
                continue;
            }
            else
            {
                if (!((Recipe.PcbBlockX * Recipe.PcbBlocksX) == bskab.LayoutX))
                {
                    isError = true;
                    continue;
                }
                if(!((Recipe.PcbBlockY * Recipe.PcbBlockY) == bskab.LayoutY))
                {
                    isError = true;
                    continue;
                }
                if(!(bskab.LayoutX > 0 && Recipe.PcbBlockX > 0 && ((bskab.LayoutX % Recipe.PcbBlockX) == 0)))
                {
                    isError = true;
                    continue;
                }
                if (!(bskab.LayoutY > 0 && Recipe.PcbBlockY > 0 && ((bskab.LayoutY % Recipe.PcbBlockY) == 0)))
                {
                    isError = true;
                    continue;
                }

                string[,] frontCodes = bskab.Codes;
                string[,] backCodes = bskab.RotateLeftRight(bskab.Codes);
                int[,] index = bskab.CreateLayoutIndex(1, bskab.LayoutX, bskab.LayoutY, ArraySortType.upperLeft_H);
                int[,] blocksIndex = new int[bskab.LayoutY, bskab.LayoutX];
                for (int row = 0; row < bskab.LayoutY; row++)
                {
                    for (int col = 0; col < bskab.LayoutX; col++)
                    {
                        int oldNumber = index[row, col];
                        int newNumber = bskab.ConvertIndex(
                            oldNumber,
                            bskab.LayoutX,
                            bskab.LayoutY,
                            bskab.LayoutX / Recipe.PcbBlockX,
                            bskab.LayoutY / Recipe.PcbBlockY,
                            ArraySortType.lowerRight_H);
                        blocksIndex[row, col] = newNumber + startNo - 1;
                    }
                }
                int[] fSkips = bskab.TakeSkips(frontCodes, blocksIndex);
                int[] bSkips = bskab.TakeSkips(backCodes, blocksIndex);
                bskab.FrontSkips = fSkips;
                bskab.BackSkips = bSkips;

                int count = bskab.TotalCount;
                int[] nos = Recipe.IsPcbRotate ? bskab.BackSkips : bskab.FrontSkips;
                startNo += count;
                totalCount += count;
                skipNos = skipNos.Concat(nos).ToArray();
            }
        }
        if (isError)
        {
            CurrentStep = AutoRunStep.錯誤流程;
            return;
        }
        RemoteBskHelper.Update(totalCount, skipNos);
        //_dataFlow.UpdateBskNos(skipNos);
        CurrentStep = AutoRunStep.等待下游要板訊號;
        return;
    }
    private async Task CompleteAsync(CancellationToken token)
    {
        // Do finalization — 設定結果、紀錄、回復狀態
        await Task.Delay(20, token).ConfigureAwait(false);
        ExecutedBskResult = 0;
    }

    public void ControlDo(int systemIndex, bool isOn)
    {
        try
        {
            _systemIo.ControlDo(systemIndex, isOn);
        }
        catch
        {
            ReportError(ErrorCode.止擋氣壓缸未降落, $"無法控制系統Output{systemIndex}={isOn}，可能是查無編號");
        }
    }

    public void InverseDo(int systemIndex)
    {
        try
        {
            _systemIo.InverseDo(systemIndex, out _);
        }
        catch
        {
            ReportError(ErrorCode.止擋氣壓缸未降落, $"無法控制系統Output{systemIndex}，可能是查無編號");
        }
    }

    public bool GetDi(int systemIndex)
    {
        try
        {
            return _systemIo.SystemDis[systemIndex];
        }
        catch
        {
            ReportError(ErrorCode.止擋氣壓缸未降落, $"系統Input列表內查無編號{systemIndex}");
            return false;
        }
    }

    public bool GetDo(int systemIndex)
    {
        try
        {
            return _systemIo.SystemDos[systemIndex];
        }
        catch (Exception ex)
        {
            ReportError(ErrorCode.止擋氣壓缸未降落, $"系統Dos裡面查無編號{systemIndex}");
            return false;
        }
    }

    private void SetStatus(MachineStatus status)
    {
        Status = status;
        try { StatusChanged?.Invoke(status); } catch { }
    }

    private void SetStepMessage(string message)
    {
        StepMessage = message;
        StepMessageChanged?.Invoke(StepMessage);
    }

    private void ReportError(ErrorCode code, string message)
    {
        try
        {
            _errorManager?.Report(code, message);
            SetStatus(MachineStatus.Alarm);
        }
        catch { }
    }
}