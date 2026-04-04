using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Infrastructures
{
    public class AutoRunContext
    {
        public Recipe WorkRecipe { get; set; }
        public AutoRunStep CurrentStep { get; private set; }
        public ErrorCode ErrorCode { get; set; } = ErrorCode.NoError;
        public string BskNo { get; set; }
        public int Quantity { get; set; }
        public string BoardThickness { get; set; }
        public string Barcode1 { get; set; }
        public string Barcode2 { get; set; }
        public int ExecutedBskResult { get; set; }

        public AutoRunContext(SystemIo systemIo)
        {
            systemIo = systemIo;
        }

        public void Clear()
        {
            BskNo = string.Empty;
            Quantity = 0;
            BoardThickness = string.Empty;
            Barcode1 = string.Empty;
            Barcode2 = string.Empty;
            ExecutedBskResult = 0;
        }
    }
}
