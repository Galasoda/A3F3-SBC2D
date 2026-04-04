using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views.Interfaces
{
    public interface IFormMainView
    {
        event Action AppStarted;
        void SetRecipeName(string modelName);
        void SetVersion(string version);
        void SetMachineStatus(string status);
        void SetUserRole(string role);
    }
}
