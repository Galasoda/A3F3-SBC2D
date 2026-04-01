using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Events
{
    public interface IIoEvents
    {
        event Action<IReadOnlyDictionary<int, bool>> SystemDisUpdated;
        event Action<IReadOnlyDictionary<int, bool>> SystemDosUpdated;
    }
}
