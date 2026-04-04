using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views.Interfaces
{
    public interface IHomePageView
    {
        IMachineDiagramView MachineDiagramView { get; }
        event EventHandler AutoRunClicked;
        event EventHandler StopClicked;
        void AddMessage(string message);
        void ClearMessages();
        void AddErrorMessage(string errorMessage);
        void ClearErrorMessages();
        void SetSentCount(int count);
        void SetAutoRunEnabled(bool isEnabled);
        void SetStopEnabled(bool isEnabled);
    }
}
