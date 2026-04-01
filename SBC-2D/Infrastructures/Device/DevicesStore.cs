using System;
using System.Collections.Generic;
using System.Linq;
using SBC_2D.Infrastructures.Ini;

namespace SBC_2D.Infrastructures.Device
{
    public class DevicesStore
    {
        public List<IDevice> Devices { get; }
        public List<IoDeviceContext> IoDeviceContext { get; }

        public DevicesStore()
        {
            Devices = new List<IDevice>();
            IoDeviceContext = new List<IoDeviceContext>();
        }

        public bool TryGetConnectableDevice(string name, out IConnectableDevice device)
        {
            device = Devices.OfType<IConnectableDevice>().FirstOrDefault(d => d.Name.Equals(name));
            return device != null;
        }
    }
}
