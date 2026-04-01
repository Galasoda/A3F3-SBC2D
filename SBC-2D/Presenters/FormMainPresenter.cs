using SBC_2D.Domain.Servicies;
using SBC_2D.Events;
using SBC_2D.Infrastructures.Recipe;
using SBC_2D.Views;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Presenters
{
    public class FormMainPresenter
    {
        private readonly IFormMainView _view;
        private readonly DevicePresenter _devicePresenter;
        private readonly RecipePresenter _recipePresenter;
        private readonly UserPresenter _userPresenter;
        private PresenterEventBus _presenterEventBus;

        public FormMainPresenter(IFormMainView view, DevicePresenter devicePresenter, RecipePresenter recipePresenter, UserPresenter userPresenter, PresenterEventBus presenterEventBus)
        {
            _view = view;
            _devicePresenter = devicePresenter;
            _recipePresenter = recipePresenter;
            _userPresenter = userPresenter;
            _presenterEventBus = presenterEventBus;
        }

        public void Initialize()
        {
            _view.Loaded += FormMainView_Loading;
            _recipePresenter.RecipeChanged += RecipePresenter_RecipeChanged;
            //_userPresenter.UserChanged += UserPresenter_UserChanged;
            _presenterEventBus.Subscribe<(Role, string)>(UserPresenter_UserChanged);
        }

        private void UserPresenter_UserChanged((Role role, string id) arg)
            => _view.SetUserRole(arg.role.ToString());

        private void RecipePresenter_RecipeChanged(Recipe recipe)
            => _view.SetRecipeName(recipe.Name);

        private async void FormMainView_Loading(object sender, EventArgs e)
        {
            _userPresenter.Initialize();
            _recipePresenter.Initialize();
            _devicePresenter.Initialize();
            await _devicePresenter.ConnectAllAsync();
        }
    }
}
