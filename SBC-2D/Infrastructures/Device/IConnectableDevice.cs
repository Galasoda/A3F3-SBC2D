using SBC_2D.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Device
{
    public interface IConnectableDevice : IDevice , IDeviceConnectionEvents
    {
        bool IsConnected { get; }
        bool Connect(IConnectionConfig config);
        void Disconnect();
        bool CheckConnection();
    }
}
