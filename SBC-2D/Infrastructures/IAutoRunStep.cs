using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Infrastructures
{
    public interface IAutoRunStep
    {
        Task<AutoRunStep> ExecuteAsync(AutoRunContext autoRun);
        string Name { get; }
    }
}
