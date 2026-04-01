using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Views.Interfaces;
using SBC_2D.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SBC_2D.Presenters
{
    public class DeviceConnectionPresenter
    {
        private readonly IDeviceConnectionView _view;
        private IConnectableDevice _device;
        private SocketConfig _socketConfig;
        private DeviceManager _deviceManager;

        public DeviceConnectionPresenter(IDeviceConnectionView view, IConnectableDevice device, SocketConfig config, DeviceManager deviceManager)
        {
            _view = view;
            _device = device;
            _socketConfig = config;
            _deviceManager = deviceManager;
        }

        public void Initialize()
        {
            _view.IpChanged += View_IpChanged;
            _view.PortChanged += View_PortChanged;
            _view.RequestConnection += View_RquestedConnection;
            _view.SetName(_device.Name ?? "");
            _view.SetIp(_socketConfig.Address);
            _view.SetPort(_socketConfig.Port > -1 ? _socketConfig.Port.ToString() : "");
            if (_device != null)
                _device.ConnectionChanged += Device_ConnectionChanged;
        }

        private void View_IpChanged(object sender, string ip)
        {
            _socketConfig.Address = ip;
            _view.SetIp(ip);
        }

        private void View_PortChanged(object sender, string port)
        {
            if (!int.TryParse(port, out int p))
                return;
            _socketConfig.Port = p;
            _view.SetPort(port);
        }

        private async void View_RquestedConnection(object sender, EndPointArgs e)
        {
            await TriggerConnectAsync();
        }

        private void Device_ConnectionChanged(string name, bool isConnected)
        {
            if (_device.Name != name)
                return;
            _view.SetConnected(isConnected);
        }

        public async Task<bool> TriggerConnectAsync()
        {
            _view.SetConnecting(true);
            (string Name, bool Value) result = await _deviceManager.ConnectAsync(_device, _socketConfig);
            _view.SetConnecting(false);
            _view.SetConnected(result.Value);
            if (result.Value)
                IniService.SaveSetupSectoinIpPort(_device.Name, _socketConfig.Address, _socketConfig.Port.ToString());
            return result.Value;
        }
    }
}
