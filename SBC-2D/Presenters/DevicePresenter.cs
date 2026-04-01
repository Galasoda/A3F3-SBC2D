using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Presenters
{
    public class DevicePresenter
    {
        private readonly IForm3View _form3View;
        private readonly DeviceManager _deviceManager;
        private readonly List<DeviceConnectionPresenter> _deviceConnectionPresenters;
        private readonly List<IoDeviceContext> _ioDeviceContexts;
        private Dictionary<int, IIoView> _diViewMap;
        private Dictionary<int, IIoView> _doViewMap;

        public DevicePresenter(IForm3View form3View, DeviceManager deviceManager)
        {
            _form3View = form3View;
            _deviceManager = deviceManager;
            _deviceConnectionPresenters = new List<DeviceConnectionPresenter>();
            _ioDeviceContexts = new List<IoDeviceContext>();
            _diViewMap = new Dictionary<int, IIoView>();
            _doViewMap = new Dictionary<int, IIoView>();
        }

        //建議還是要分deviceConnectionlistview、iolistview
        //再加個barcodereaderCommand mvp
        //再加個laserthicknessSensor mvp
        public void Initialize()
        {
            foreach (IDevice device in _deviceManager.Devices)
            {
                string name = device.Name;
                var view = _form3View.AddDeviceConnectionView();
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
            _form3View.ClearInputView();
            _diViewMap.Clear();
            _form3View.ClearOutputView();
            _doViewMap.Clear();
            foreach (IoDeviceContext context in _deviceManager.IoDeviceContexts)
            {
                _ioDeviceContexts.Add(context);
                for (int i = 0; i < context.Device.DiCount; i++)
                {
                    int systemDiNumber = context.ToSystemDi(i);
                    IIoView view = _form3View.AddInputView(systemDiNumber);
                    view.SetNumber(systemDiNumber);
                    view.SetDescription($"{"X"}{systemDiNumber}用ini設定");
                    view.SetStatus(context.State.Dis[i]);
                    _diViewMap.Add(systemDiNumber, view);
                }
                context.SystemDisUpdated -= Context_SystemDisUpdated;
                context.SystemDisUpdated += Context_SystemDisUpdated;
                for (int i = 0; i < context.Device.DoCount; i++)
                {
                    int systemDoNumber = context.ToSystemDo(i);
                    IOutView view = _form3View.AddOutputView(systemDoNumber);
                    view.SetNumber(systemDoNumber);
                    view.SetDescription($"{"Y"}{systemDoNumber}用ini設定");
                    view.SetStatus(context.State.Dos[i]);
                    _doViewMap.Add(systemDoNumber, view);
                    view.OutputClicked += View_OutputClicked; ;
                }
                context.SystemDosUpdated -= Context_SystemDosUpdated;
                context.SystemDosUpdated += Context_SystemDosUpdated;
            }
            _ = _deviceManager.StartPollingAllDeviceConnection();
        }

        public async Task ConnectAllAsync()
            => await _deviceManager.ConnectAllAsync();

        private void View_OutputClicked(object sender, int index)
        {
            _deviceManager.InverseDo(index, out bool isOn);
        }

        //不宣告查表: 時間換空間
        //宣告查表: 空間換時間
        private void Context_SystemDisUpdated(IReadOnlyDictionary<int, bool> dis)
        {
            foreach (var din in dis)
            {
                if (_diViewMap.TryGetValue(din.Key, out IIoView view))
                    view.SetStatus(din.Value);
            }
        }

        private void Context_SystemDosUpdated(IReadOnlyDictionary<int, bool> dos)
        {
            foreach (var dout in dos)
            {
                if (_doViewMap.TryGetValue(dout.Key, out IIoView view))
                    view.SetStatus(dout.Value);
            }
        }
    }
}
