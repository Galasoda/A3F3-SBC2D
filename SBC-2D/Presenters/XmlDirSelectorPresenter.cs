using SBC_2D.Domain.Servicies;
using SBC_2D.Shared;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBC_2D.Presenters
{
    public class XmlDirSelectorPresenter : IDisposable
    {
        private readonly IXmlDirSelectorView _view;
        private string _dirPath = string.Empty;
        private string _insertType = "A";

        public XmlDirSelectorPresenter(IXmlDirSelectorView view)
        {
            _view = view;
            _view.InitializeRequested += View_LoadXmlDirRequested;
            _view.ChangeDirRequested += View_ChangeDirRequested;
        }

        public void Dispose()
        {
            _view.InitializeRequested -= View_LoadXmlDirRequested;
            _view.ChangeDirRequested -= View_ChangeDirRequested;
        }

        private void View_ChangeDirRequested(object sender, EventArgs e)
        {
            try
            {
                var filePath = _view.SelectXmlFile();
                if (string.IsNullOrEmpty(filePath))
                    return;
                string directory = Path.GetDirectoryName(filePath);
                IniService.SaveXmlDirPath(directory);
                IniService.SetXmlDirPath(directory);
                _dirPath = directory;
            }
            catch (Exception ex)
            {
            }
        }

        private void View_LoadXmlDirRequested(object sender, EventArgs e)
        {
            var pathConfig = IniService.GetPathConfig();
            _dirPath = pathConfig?.XmlDir ?? "";
            _view.ShowDirPath(_dirPath);
            _insertType = pathConfig?.InsertType ?? "A";
            _view.SetInsertType(_insertType);
        }
    }
}
