using SBC_2D.Domain.Servicies;
using SBC_2D.Views;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Presenters
{
    public class FormMainPresenter
    {
        private readonly IFormMainView _view;
        private readonly DevicePresenter _devicePresenter;
        private readonly RecipePresenter _recipePresenter;

        public FormMainPresenter(IFormMainView view, DevicePresenter devicePresenter, RecipePresenter recipePresenter)
        {
            _view = view;
            _devicePresenter = devicePresenter;
            _recipePresenter = recipePresenter;
        }

        public void Initialize()
        {
            _view.Loaded += FormMainView_Loading;
        }

        private async void FormMainView_Loading(object sender, EventArgs e)
        {
            _recipePresenter.Initialize();
            _devicePresenter.Initialize();
            await _devicePresenter.ConnectAllAsync();
        }
    }
}
