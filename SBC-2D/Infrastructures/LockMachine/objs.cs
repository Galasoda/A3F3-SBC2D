//using SBC_2D.Shared;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Timers;

//namespace SBC_2D.Infrastructures.LockMachine
//{
//    public class LockState
//    {
//        public ErrorPeriod Consecutive { get; set; } = new ErrorPeriod();
//        public ErrorPeriod Cumulative { get; set; } = new ErrorPeriod();
//        public Lock Lock { get; set; } = new Lock();

//        public bool CheckIsSameError(ErrorArgs args)
//        {
//            if (Consecutive.Errors.Count == 0)
//                return true;
//            var errors = Consecutive.Errors.OrderBy(x => x.Value.Timestamp).ToList();
//            string lastMsg = errors.Last().Value.Message;
//            if (args.Message == lastMsg)
//                return true;
//            return false;
//        }
//    }

//    public class ErrorPeriod
//    {
//        public DateTime StartTime { get; set; }
//        public DateTime RestartTime { get; set; }
//        public int Count => Errors.Count;
//        public Dictionary<int, ErrorArgs> Errors { get; set; } = new Dictionary<int, ErrorArgs>();
//    }

//    public class Lock
//    {
//        public bool LockStatus { get; set; }
//        public DateTime? LockTime { get; set; }
//        public string LockReason { get; set; }
//        public DateTime? UnlockTime { get; set; }
//        public string Engineer { get; set; }
//        public void Reset()
//        {
//            LockStatus = false;
//            LockTime = null;
//            LockReason = "";
//            UnlockTime = null;
//            Engineer = "";
//        }
//    }
//    public class objs
//    {
//        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
//        private JsonSerializerSettings _jsonTimeFormat;
//        private LockState _lockState;
//        private Timer _lockStateTimer;
//        private string _dataTimeFormat = "yyyy-MM-dd HH:mm:ss";
//        private int _refreshLock;
//        public int ConsecutiveErrorIntervalTime { get; set; }
//        public int CumulativeErrorIntervalTime { get; set; }
//        public bool IsLockTimerRun => _lockStateTimer?.Enabled ?? false;
//        public bool IsLock => _lockState.Lock.LockStatus;

//        /* Error and Lock */
//        private bool LoadLockStateData()
//        {
//            try
//            {
//                _lockState = JsonHelper.LoadJson<LockState>(SysPaths.LockState);
//                if (_lockState == null)
//                {
//                    _lockState = new LockState
//                    {
//                        Consecutive = new ErrorPeriod(),
//                        Cumulative = new ErrorPeriod(),
//                        Lock = new Lock()
//                    };
//                    string json = JsonConvert.SerializeObject(_lockState, _jsonTimeFormat);
//                    File.WriteAllText(SysPaths.LockState, json);
//                    Logger.RecordLog(SysPaths.MyLogDir, $"{GetType().Name} LockState檔案遺失或格式有誤，已將格式清空並重新寫入");
//                }
//            }
//            catch (Exception ex)
//            {
//                UIHelper.MyConfirmation(ex.ToString());
//                return false;
//            }
//            return true;
//        }
//        public void EnableLockMonitoring()
//        {
//            if (!_lockState.Lock.LockStatus)
//                _lockStateTimer.Start();
//            else
//                _dataFlow.UpdateLock(true);

//        }
//        public void DisableLockMonitoring()
//        {
//            _lockStateTimer.Stop();
//        }
//        private void ResetTimestamp_Elapsed(object sender, ElapsedEventArgs e)
//        {
//            try
//            {
//                DateTime now = DateTime.Now;
//                if (_lockState.Lock.LockStatus)
//                {
//                    string log = "";
//                    _lockStateTimer.Stop();
//                    if (_lockState.Consecutive.Errors.Count >= 3)
//                        log = $"{_lockState.Lock.LockReason}，{_lockState.Consecutive.Errors.Last().Value.Message}";
//                    else if (_lockState.Cumulative.Errors.Count >= 3)
//                        log = $"{_lockState.Lock.LockReason}，" +
//                            $"[{string.Join(", ", _lockState.Cumulative.Errors.Values.Select(x => x.Message))}]";
//                    Logger.RecordLog(SysPaths.ErrorLogDir, log);
//                    //SetBuzzer(true);
//                    IsError = true;
//                    _dataFlow.UpdateErrorDetial(new ErrorArgs(_lockState.Lock.LockReason));
//                    _dataFlow.UpdateError(true);
//                    _dataFlow.UpdateLock(_lockState.Lock.LockStatus);
//                    return;
//                }
//                if (Interlocked.CompareExchange(ref _refreshLock, 1, 1) == 1)
//                    return;
//                bool isRefresh = false;
//                bool isConsecutiveRestart = now >= _lockState.Consecutive.RestartTime;
//                bool isCumulativeRestart = now >= _lockState.Cumulative.RestartTime;
//                if (isConsecutiveRestart || isCumulativeRestart)
//                {
//                    Interlocked.CompareExchange(ref _refreshLock, 1, 1);
//                    isRefresh = true;
//                }
//                if (isConsecutiveRestart)
//                    ResetErrorPeriod(_lockState.Consecutive, ConsecutiveErrorIntervalTime);
//                if (isCumulativeRestart)
//                    ResetErrorPeriod(_lockState.Cumulative, CumulativeErrorIntervalTime);
//                if (isRefresh && !_lockState.Lock.LockStatus)
//                {
//                    string json = JsonConvert.SerializeObject(_lockState, _jsonTimeFormat);
//                    File.WriteAllText(SysPaths.LockState, json);
//                }
//                _dataFlow.UPdateLockDetial(_lockState);
//            }
//            catch (Exception ex)
//            {
//                Logger.RecordLog(SysPaths.MyLogDir, $"ResetTimestamp_Elapsed: {ex.Message}");
//            }
//            finally
//            {
//                Interlocked.Exchange(ref _refreshLock, 0);
//            }
//        }
//        public void CountErrorForLock(ErrorArgs args)
//        {
//            try
//            {
//                if (CheckErrorCount(_lockState.Consecutive, 3)
//                    || CheckErrorCount(_lockState.Cumulative, 3))
//                    return;

//                bool isConsecutiveOccur = false;

//                if (_lockState.CheckIsSameError(args))
//                {
//                    int conId = GenId(_lockState.Consecutive);
//                    _lockState.Consecutive.Errors[conId] = args;
//                }
//                else
//                {
//                    _lockState.Consecutive.Errors.Clear();
//                    _lockState.Consecutive.Errors[1] = args;
//                }

//                int cmuId = GenId(_lockState.Cumulative);
//                _lockState.Cumulative.Errors[cmuId] = args;

//                if (CheckErrorCount(_lockState.Consecutive, 3))
//                {
//                    isConsecutiveOccur = true;
//                    _lockState.Lock.LockStatus = true;
//                }
//                if (CheckErrorCount(_lockState.Cumulative, 3))
//                {
//                    _lockState.Lock.LockStatus = true;
//                }

//                if (_lockState.Lock.LockStatus)
//                {
//                    string zh_el1 = Program.ProgrameRM.GetString($"Msg_EL1", Language.ZhCultureInfo);
//                    string en_el1 = Program.ProgrameRM.GetString($"Msg_EL1", Language.EnCultureInfo);
//                    string zh_el2 = Program.ProgrameRM.GetString($"Msg_EL2", Language.ZhCultureInfo);
//                    string en_el2 = Program.ProgrameRM.GetString($"Msg_EL2", Language.EnCultureInfo);
//                    string el1 = $"{zh_el1} {en_el1}";
//                    string el2 = $"{zh_el2} {en_el2}";
//                    string reason = isConsecutiveOccur ? el1 : el2;
//                    _lockState.Lock.LockReason = reason;
//                    _lockState.Lock.LockTime = args.Timestamp;
//                }
//                string json = JsonConvert.SerializeObject(_lockState, _jsonTimeFormat);
//                File.WriteAllText(SysPaths.LockState, json);
//            }
//            catch (Exception ex)
//            {
//                Logger.RecordLog(SysPaths.MyLogDir, ex.Message);
//                UIHelper.MsgboxOK(ex.ToString());
//            }
//        }
//        public int GetConsecutiveErrorCount() => _lockState?.Consecutive?.Count ?? 0;
//        public int GetCumulativeErrorCount() => _lockState?.Cumulative?.Count ?? 0;
//        private bool CheckErrorCount(ErrorPeriod errorPeriod, int max) => errorPeriod.Count >= max;
//        private int GenId(ErrorPeriod errorPeriod)
//        {
//            if (errorPeriod.Errors.Count == 0)
//                return 1;
//            return errorPeriod.Errors.Keys.Max() + 1;
//        }
//        private void ResetErrorPeriod(ErrorPeriod errorPeriod, int minute)
//        {
//            DateTime now = DateTime.Now;
//            errorPeriod.StartTime = now;
//            errorPeriod.RestartTime = now.AddMinutes(minute);
//            errorPeriod.Errors.Clear();
//        }
//        public void ResetErrorCount()
//        {
//            _lockState.Consecutive.Errors.Clear();
//            _lockState.Cumulative.Errors.Clear();
//            string json = JsonConvert.SerializeObject(_lockState, _jsonTimeFormat);
//            File.WriteAllText(SysPaths.LockState, json);
//        }
//        public Task Unlock(string empId, DateTime now)
//        {
//            string json = "";
//            _lockState.Lock.Engineer = empId;
//            _lockState.Lock.UnlockTime = now;
//            json = JsonConvert.SerializeObject(_lockState, _jsonTimeFormat);
//            File.WriteAllText(SysPaths.LockState, json);
//            string savePath = $@"{SysPaths.ErrorLogDir}\{now.ToString("yyyyMMdd")}\{now.ToString("HHmmss")}.json";
//            File.WriteAllText(savePath, json);
//            _lockState = new LockState();
//            ResetErrorPeriod(_lockState.Consecutive, ConsecutiveErrorIntervalTime);
//            ResetErrorPeriod(_lockState.Cumulative, CumulativeErrorIntervalTime);
//            json = JsonConvert.SerializeObject(_lockState, _jsonTimeFormat);
//            File.WriteAllText(SysPaths.LockState, json);
//            EnableLockMonitoring();
//            return Task.CompletedTask;
//        }
//    }
//}
