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
    public class DeviceConnectionPresenter : IDisposable
    {
        private readonly IDeviceConnectionView _view;
        private IConnectableDevice _device;
        private SocketConfig _socketConfig;


        public DeviceConnectionPresenter(IDeviceConnectionView view, IConnectableDevice device, SocketConfig config)
        {
            _view = view;
            _device = device;
            _socketConfig = config;
            _view.IpChanged += View_IpChanged;
            _view.PortChanged += View_PortChanged;
            _view.RequestConnection += View_RquestedConnection;
        }
        public void Dispose()
        {
            _view.IpChanged -= View_IpChanged;
            _view.PortChanged -= View_PortChanged;
            _view.RequestConnection -= View_RquestedConnection;
        }

        public void Initialize()
        {
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
            _view.SetConnecting(true);
            var isConnected = await Task.Run(() =>
            {
                try
                {
                    return _device.Connect(_socketConfig);
                }
                catch
                {
                    return false;
                }
            });
            _view.SetConnecting(false);
            _view.SetConnected(isConnected);
            if (isConnected)
                IniService.SaveSetupSectoinIpPort(_device.Name, _socketConfig.Address, _socketConfig.Port.ToString());
        }

        private void Device_ConnectionChanged(string name, bool isConnected)
        {
            if (_device.Name != name)
                return;
            _view.SetConnected(isConnected);
        }
    }
}
