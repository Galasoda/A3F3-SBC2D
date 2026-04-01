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
    public class FormMainPresenter : IDisposable
    {
        private readonly IFormMainView _view;
        private IEventBus _eventBus;
        //private readonly DevicePresenter _devicePresenter;
        //private readonly RecipePresenter _recipePresenter;
        //private readonly UserPresenter _userPresenter;

        public FormMainPresenter(IFormMainView view, IEventBus eventBus)
        {
            _view = view;
            _eventBus = eventBus;
            _eventBus.Subscribe<Recipe>(RecipePresenter_RecipeChanged);
            _eventBus.Subscribe<(Role, string)>(UserPresenter_UserChanged);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<Recipe>(RecipePresenter_RecipeChanged);
            _eventBus.Unsubscribe<(Role, string)>(UserPresenter_UserChanged);
        }


        private void UserPresenter_UserChanged((Role role, string id) arg)
            => _view.SetUserRole(arg.role.ToString());

        private void RecipePresenter_RecipeChanged(Recipe recipe)
            => _view.SetRecipeName(recipe.Name);
    }
}
