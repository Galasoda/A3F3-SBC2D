using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures;
using SBC_2D.Shared;
using SBC_2D.Views;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Presenters
{
    public class FormMainPresenter : IDisposable
    {
        private readonly IFormMainView _view;

        public FormMainPresenter(IFormMainView view)
        {
            _view = view;
        }

        public void Dispose()
        {
        }
    }
}
