using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Device
{
    public interface IIoDevice : IDevice
    {
        event Action<string, IReadOnlyCollection<bool>> DisUpdated;
        event Action<string, IReadOnlyCollection<bool>> DosUpdated;
        int DiCount { get; }
        int DoCount { get; }
        bool[] Dis { get; } 
        bool[] Dos { get; } 
        bool ReadDi(int channel, out bool data);
        bool ReadAllDi(out bool[] data);
        bool ReadDo(int channel, out bool data);
        bool ReadAllDo(out bool[] data);
        bool WriteDo(int channel, bool data);
        bool InverseDo(int channel, out bool result);
    }
}
