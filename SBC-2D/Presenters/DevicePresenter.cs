using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Shared;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Presenters
{
    public class DevicePresenter : IDisposable
    {
        private readonly IHardwarePageView _hardwarePageView;
        private readonly DeviceManager _deviceManager;
        private readonly SystemIo _systemIo;
        private readonly List<DeviceConnectionPresenter> _deviceConnectionPresenters;
        private Dictionary<int, IIoView> _diViewMap;
        private Dictionary<int, IIoView> _doViewMap;

        public DevicePresenter(IHardwarePageView hardwarePageView, DeviceManager deviceManager, SystemIo systemIo)
        {
            _hardwarePageView = hardwarePageView;
            _deviceManager = deviceManager;
            _systemIo = systemIo;
            _deviceConnectionPresenters = new List<DeviceConnectionPresenter>();
            _diViewMap = new Dictionary<int, IIoView>();
            _doViewMap = new Dictionary<int, IIoView>();
        }

        public void Dispose()
        {
            foreach (var p in _deviceConnectionPresenters)
            {
                try { p.Dispose(); } catch { }
            }
            _deviceConnectionPresenters.Clear();

            if (_systemIo != null)
            {
                try { _systemIo.SystemDisUpdated -= SystemDisUpdated; } catch { }
                try { _systemIo.SystemDosUpdated -= SystemDosUpdated; } catch { }
            }

            foreach (var kvp in _doViewMap)
            {
                if (kvp.Value is IOutView outView)
                {
                    try { outView.OutputClicked -= View_OutputClicked; } catch { }
                }
            }

            _diViewMap.Clear();
            _doViewMap.Clear();
        }

        //建議還是要分deviceConnectionlistview、iolistview
        //再加個barcodereaderCommand mvp
        //再加個laserthicknessSensor mvp
        public void Initialize()
        {
            _hardwarePageView.ClearDeviceConnectionView();
            foreach (IDevice device in _deviceManager.Devices)
            {
                string name = device.Name;
                var view = _hardwarePageView.AddDeviceConnectionView();
                var config = IniService.GetSocketConfig(name);
                if (config.Value == null)
                    config = new KeyValuePair<string, SocketConfig>(name, new SocketConfig("", -1));
                if (device is IConnectableDevice connectableDevice)
                {
                    var presenter = new DeviceConnectionPresenter(view, connectableDevice, config.Value);
                    _deviceConnectionPresenters.Add(presenter);
                    presenter.Initialize();
                }
            }
            _hardwarePageView.ClearInputView();
            _diViewMap.Clear();
            _hardwarePageView.ClearOutputView();
            _doViewMap.Clear();
            foreach (var sioc in _systemIo.SystemDis)
            {
                int systemDiNumber = sioc.Key;
                IIoView view = _hardwarePageView.AddInputView(systemDiNumber);
                view.SetNumber(systemDiNumber);
                view.SetDescription($"{"X"}{systemDiNumber}用ini設定");
                view.SetStatus(sioc.Value);
                _diViewMap.Add(systemDiNumber, view);
            }
            foreach (var sioc in _systemIo.SystemDos)
            {
                int systemDoNumber = sioc.Key;
                IOutView view = _hardwarePageView.AddOutputView(systemDoNumber);
                view.SetNumber(systemDoNumber);
                view.SetDescription($"{"Y"}{systemDoNumber}用ini設定");
                view.SetStatus(sioc.Value);
                _doViewMap.Add(systemDoNumber, view);
                view.OutputClicked += View_OutputClicked; ;
            }
            _systemIo.SystemDisUpdated += SystemDisUpdated;
            _systemIo.SystemDosUpdated += SystemDosUpdated;
            _ = _deviceManager.StartPollingAllDeviceConnection();
            _ = _deviceManager.StartUpdatingAllDios();
        }

        public async Task ConnectAllAsync()
            => await _deviceManager.ConnectAllAsync();

        private void View_OutputClicked(object sender, int index)
        {
            _systemIo.InverseDo(index, out bool isOn);
        }

        //不宣告查表: 時間換空間
        //宣告查表: 空間換時間
        private void SystemDisUpdated(IReadOnlyDictionary<int, bool> dis)
        {
            foreach (var din in dis)
            {
                if (_diViewMap.TryGetValue(din.Key, out IIoView view))
                    view.SetStatus(din.Value);
            }
        }

        private void SystemDosUpdated(IReadOnlyDictionary<int, bool> dos)
        {
            foreach (var dout in dos)
            {
                if (_doViewMap.TryGetValue(dout.Key, out IIoView view))
                    view.SetStatus(dout.Value);
            }
        }
    }
}
