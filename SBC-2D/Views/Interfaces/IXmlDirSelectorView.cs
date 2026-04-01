using Advantech.Adam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Views.Interfaces
{
    public interface IXmlDirSelectorView
    {
        event EventHandler InitializeRequested;
        event EventHandler ChangeDirRequested;
        event EventHandler<string> InsertTypeChanged;

        string SelectXmlFile();
        void ShowDirPath(string path);
        void SetInsertType(string type);
    }
}
