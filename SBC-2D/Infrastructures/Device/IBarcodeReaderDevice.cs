using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Device
{
    public interface IBarcodeReaderDevice : IDevice
    {
        string ReadBarcodes(int timeout = 5000);
    }
}
