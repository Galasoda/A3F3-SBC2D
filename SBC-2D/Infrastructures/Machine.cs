using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static SBC_2D.Shared.Enums;

public class Machine
{
    private readonly DeviceManager _deviceManager;
    private AutoRunContext _autoRunContext;
    private readonly Setup _setup;
    private Task _autoRunTask;
    private CancellationTokenSource _ctsAutoRun;
    private readonly Dictionary<ErrorCode, bool> _errors;
    public MachineStatus Status { get; private set; }
    public SystemIo SystemIo { get; private set; }
    public IReadOnlyDictionary<ErrorCode, bool> Errors => _errors;
    public event Action<MachineStatus> StatusChanged;
    public bool IsAutoRunning
        => _autoRunTask != null && !_autoRunTask.IsCompleted;

    public event Action<MachineStatus> OnStatusChanged;

    public Machine(
        DeviceManager deviceManager,
        Setup setup)
    {
        _deviceManager = deviceManager;
        _setup = setup;
        _errors = new Dictionary<ErrorCode, bool>();
    }

    public void Initialize()
    {
        int diStart = 0;
        int doStart = 0;
        List<(IIoDevice, int DiStart, int DoStart)> indexesMap
            = new List<(IIoDevice, int DiStart, int DoStart)>();
        foreach (var device in _deviceManager.Devices.OfType<IIoDevice>())
        {
            indexesMap.Add((device, diStart, doStart));
            diStart = diStart + device.DiCount;
            doStart = doStart + device.DoCount;
        }
        SystemIo = new SystemIo(indexesMap);
        SystemIo.Initialize();
        SetStatus(MachineStatus.Idle);
    }

    // ── AutoRun ───────────────────────────────────────────

    public async Task StartAutoRunAsync()
    {
        if (IsAutoRunning) return;
        _ctsAutoRun = new CancellationTokenSource();
        _autoRunTask = RunAutoAsync(_ctsAutoRun.Token);
        await _autoRunTask;
    }

    public async Task StopAutoRunAsync()
    {
        if (_ctsAutoRun == null) return;
        _ctsAutoRun.Cancel();
        try
        {
            await _autoRunTask;
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
        }
        finally
        {
            _ctsAutoRun.Dispose();
            _ctsAutoRun = null;
            _autoRunTask = null;
        }
    }

    private async Task RunAutoAsync(CancellationToken token)
    {
        //SetStatus(MachineStatus.Running);
        //try
        //{
        //    while (!token.IsCancellationRequested)
        //    {
        //        await ExecuteStepAsync(_autoRunContext.CurrentStep, token);
        //        token.ThrowIfCancellationRequested();
        //    }
        //}
        //catch (OperationCanceledException) { }
        //catch (Exception ex)
        //{
        //    SetStatus(MachineStatus.Alarm);
        //    return;
        //}
        //SetStatus(MachineStatus.Idle);
    }

    private async Task ExecuteStepAsync(AutoRunStep step, CancellationToken token)
    {
        //switch (step)
        //{
        //    case AutoRunStep.WaitBoard:
        //        await WaitBoardAsync(token);
        //        break;
        //    case AutoRunStep.ScanBarcode:
        //        await ScanBarcodeAsync(token);
        //        break;
        //    case AutoRunStep.MeasureThickness:
        //        await MeasureThicknessAsync(token);
        //        break;
        //    case AutoRunStep.Complete:
        //        await CompleteAsync(token);
        //        break;
        //}
    }

    private async Task WaitBoardAsync(CancellationToken token)
    {
        //SetStep(AutoRunStep.WaitBoard);
        //// 等待 DI 訊號
        //while (!SystemIo.SystemDis[IoIndex.BoardArrived] && !token.IsCancellationRequested)
        //    await Task.Delay(50, token);
        //SetStep(MachineStep.ScanBarcode);
    }

    private async Task ScanBarcodeAsync(CancellationToken token) { }
    private async Task MeasureThicknessAsync(CancellationToken token) { }
    private async Task CompleteAsync(CancellationToken token) { }

    public bool ControlDo(int systemIndex, bool isOn)
        => SystemIo.ControlDo(systemIndex, isOn);

    public bool InverseDo(int systemIndex, out bool isOn)
        => SystemIo.InverseDo(systemIndex, out isOn);

    private void SetStatus(MachineStatus status)
    {
        Status = status;
        StatusChanged?.Invoke(status);
    }

    private void SetStep(AutoRunStep step)
    {
        //_autoRunContext.CurrentStep = step;
        //StepChanged?.Invoke(step);
    }

    public void SetError(ErrorCode error, bool isActive)
    {
        _errors[error] = isActive;
    }
}