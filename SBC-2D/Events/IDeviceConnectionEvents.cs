using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Events
{
    public interface IDeviceConnectionEvents
    {
        event Action<string, bool> ConnectionChanged;
    }
}
