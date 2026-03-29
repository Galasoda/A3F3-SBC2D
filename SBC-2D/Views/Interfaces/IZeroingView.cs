using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views.Interfaces
{
    public interface IZeroingView
    {
        event EventHandler<string> ThicknessZeroBiasChanged;
        event EventHandler ZeroingRequested;
        event EventHandler ViewClosed; 
        void SetThicknessZeroBias(string bias);
        void CloseView();
    }
}
