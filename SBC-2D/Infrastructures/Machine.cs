using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Error;
using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
    public string Barcode1 { get; set; }
    public string Barcode2 { get; set; }
    public int ExecutedBskResult { get; set; }
    public MachineStatus Status { get; private set; }
    public AutoRunStep CurrentStep { get; private set; }
    public string StepMessage { get; private set; }

    public event Action<MachineStatus> StatusChanged;
    public event Action<string> StepMessageChanged;

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
                        CurrentStep = AutoRunStep.測量薄板厚度;
                        return;
                    }
                }
            }
        }
        return;
    }

    private async Task ScanBarcodeAsync(CancellationToken token)
    {
        // attempt to read from barcode reader devices present in DeviceManager
        try
        {
            Barcode1 = null;
            Barcode2 = null;

            var barcodeDevices = _deviceManager?.Devices?.OfType<IBarcodeReaderDevice>().ToArray()
                ?? Array.Empty<IBarcodeReaderDevice>();

            if (!barcodeDevices.Any())
            {
                // no barcode reader available -> treat as OK but log
                ReportError(ErrorCode.ES2, "未偵測到條碼讀取器");
                await Task.Delay(50, token).ConfigureAwait(false);
                return;
            }

            // read available barcode devices sequentially (first -> Barcode1, second -> Barcode2)
            for (int i = 0; i < barcodeDevices.Length && i < 2; i++)
            {
                token.ThrowIfCancellationRequested();
                var dev = barcodeDevices[i];
                try
                {
                    // reading may block; protect with Task.Run + timeout via CancellationToken
                    string result = await Task.Run(() => dev.ReadBarcode(3000), token).ConfigureAwait(false);
                    if (i == 0) Barcode1 = result;
                    else Barcode2 = result;
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex)
                {
                    ReportError(ErrorCode.E2, $"讀取條碼例外: {ex.Message}");
                }
            }
        }
        finally
        {
            await Task.Delay(20, token).ConfigureAwait(false);
        }
    }

    private async Task MeasureThicknessAsync(CancellationToken token)
    {
        // placeholder: 嘗試從可用感測器讀值；目前先做模擬等待
        await Task.Delay(50, token).ConfigureAwait(false);
        BoardThickness = Recipe != null ? Recipe.Thickness.ToString() : "N/A";
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