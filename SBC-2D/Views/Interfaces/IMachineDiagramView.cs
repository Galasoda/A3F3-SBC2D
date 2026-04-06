using SBC_2D.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views.Interfaces
{
    public interface IMachineDiagramView
    {
        void SystemDisUpdated(IReadOnlyDictionary<int, bool> dis);
        void SystemDosUpdated(IReadOnlyDictionary<int, bool> dos);
        void ShowThicknessValue(double thickness);
    }
}
