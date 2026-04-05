using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Infrastructures
{
    public class ErrorEntry
    {
        public ErrorCode Code { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }

        public ErrorEntry(ErrorCode code, string message)
        {
            Code = code;
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}
