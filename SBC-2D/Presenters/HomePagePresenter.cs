using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Error;
using SBC_2D.Shared;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Presenters
{
    public class HomePagePresenter
    {
        private readonly IHomePageView _view;
        private readonly Machine _machine;
        private readonly SystemIo _systemIo;
        private readonly ErrorManager _errorManager;
        private int _sentCount = 0;

        public HomePagePresenter(IHomePageView view, Machine machine, SystemIo systemIo, ErrorManager errorManager)
        {
            _view = view;
            _machine = machine;
            _systemIo = systemIo;
            _errorManager = errorManager;
        }

        public void Dispose()
        {
            _view.AutoRunClicked -= View_AutoRunClicked;
            _view.StopClicked -= View_StopClicked;
            _machine.StatusChanged -= MachineContext_StatusChanged;
            _machine.StepMessageChanged -= Machine_StepMessageChanged;
            _systemIo.SystemDisUpdated -= _view.MachineDiagramView.SystemDisUpdated;
            _systemIo.SystemDosUpdated -= _view.MachineDiagramView.SystemDosUpdated;
            _errorManager.ErrorRaised -= ErrorManager_ErrorRaised;
        }

        public void Initialize()
        {
            _view.AutoRunClicked += View_AutoRunClicked;
            _view.StopClicked += View_StopClicked;
            _machine.StatusChanged += MachineContext_StatusChanged;
            _machine.StepMessageChanged += Machine_StepMessageChanged;
            _systemIo.SystemDisUpdated += _view.MachineDiagramView.SystemDisUpdated;
            _systemIo.SystemDosUpdated += _view.MachineDiagramView.SystemDosUpdated;
            _errorManager.ErrorRaised += ErrorManager_ErrorRaised;
            UpdateButtonStates();
        }

        private void ErrorManager_ErrorRaised(ErrorEntry error)
        {
            _view.AddErrorMessage($"{error.Code} {error.Message}");
        }

        private void Machine_StepMessageChanged(string msg)
        {
            _view.AddMessage(msg);
        }

        private void View_AutoRunClicked(object sender, EventArgs e)
        {
            try
            {
                _view.SetAutoRunEnabled(false);
                _view.SetStopEnabled(true);
                _view.AddMessage("準備自動運行...");
                _ = _machine.StartAutoRunAsync();
            }
            catch (Exception ex)
            {
                _view.AddErrorMessage($"自動運行失敗: {ex.Message}");
            }
            finally
            {
                UpdateButtonStates();
            }
        }

        private async void View_StopClicked(object sender, EventArgs e)
        {
            try
            {
                _view.AddMessage("正在停止...");
                await _machine.StopAutoRunAsync();
                _view.AddMessage("已停止");
            }
            catch (Exception ex)
            {
                _view.AddErrorMessage($"停止失敗: {ex.Message}");
            }
            finally
            {
                UpdateButtonStates();
            }
        }

        private void MachineContext_StatusChanged(MachineStatus status)
        {
            switch(status)
            {
                case MachineStatus.Idle:
                    _view.AddMessage("機器處於閒置狀態");
                    break;
                case MachineStatus.Running:
                    _view.AddMessage("機器正在運行");
                    break;
                case MachineStatus.Alarm:
                    _view.AddErrorMessage("機器進入警報狀態!");
                    break;
                case MachineStatus.Lock:
                    _view.AddMessage("機器被鎖定");
                    break;
            }
                    UpdateButtonStates();
        }

        ///// <summary>系統訊息事件</summary>
        //private void OnSystemMessage(SystemMessageEvent evt)
        //{
        //    _view.AddMessage(evt.Message);
        //    _sentCount++;
        //    _view.SetSentCount(_sentCount);
        //}

        ///// <summary>系統錯誤事件</summary>
        //private void OnSystemError(SystemErrorEvent evt)
        //{
        //    _view.AddErrorMessage(evt.ErrorMessage);
        //}

        ///// <summary>DO 被控制事件</summary>
        //private void OnDoControlled(DoControlledEvent evt)
        //{
        //    _view.AddMessage($"DO {evt.SystemIndex} 被設置為 {evt.IsOn}");
        //}

        /// <summary>更新按鈕狀態</summary>
        private void UpdateButtonStates()
        {
            bool isRunning = _machine.IsAutoRunning;
            _view.SetAutoRunEnabled(!isRunning);
            _view.SetStopEnabled(isRunning);
        }
    }
}
