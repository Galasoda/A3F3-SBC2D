using Dapper.FluentMap.Mapping;
using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Recipe;
using SBC_2D.Servicies;
using SBC_2D.Shared;
using SBC_2D.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Presenters
{
    public class RecipePresenter
    {
        private readonly RecipeService _recipeService;
        private readonly IniService _iniService;
        private readonly IRecipeView _recipeView;
        private bool _isOnEdit;
        private Recipe _editedRecipe;
        private Recipe _currentRecipe;
        private List<string> _allRecipeNames;
        private string _selectedName = "";
        public RecipeManageViewMode ViewModeAction { get; private set; }

        // Events
        public event Action<Recipe> RecipeChanged;
        public event Action<List<string>> Initialized;
        public event Action<bool> OnValueChanged;

        public RecipePresenter(RecipeService recipeService, IniService iniService, IRecipeView recipeView)
        {
            _recipeService = recipeService;
            _iniService = iniService;
            _recipeView = recipeView;
            _allRecipeNames = new List<string>();
        }

        public void Initialize()
        {
            _recipeView.Initialized += RecipeView_Initialized; ;
            _recipeView.ToggleEditModeRequested += RecipeView_ToggleEditMode;
            _recipeView.ActionRequested += RecipeView_ActionRequested;
            _recipeView.ActionConfirmed += RecipeView_ActionConfirmed;
            _recipeView.ActionCancelled += RecipeView_ActionCancelled;
            _recipeView.MapModeBypassChanged += RecipeView_MapModeBypassChanged;
            _recipeView.UpperBrBypassChanged += RecipeView_UpperBrBypassChanged;
            _recipeView.LowerBrBypassChanged += RecipeView_LowerBrBypassChanged;
            _recipeView.LdsBypassChanged += RecipeView_LdsBypassChanged;
            _recipeView.ModelNameSelectChanged += RecipeView_ModelNameSelectChanged;
            _recipeView.ThicknessChanged += RecipeView_ThicknessChanged;
            _recipeView.ThicknessTolerationChanged += RecipeView_ThicknessTolerationChanged;
            _recipeView.BlockXChanged += RecipeView_BlockXChanged;
            _recipeView.BlockYChanged += RecipeView_BlockYChanged;
            _recipeView.BlockNumXChanged += RecipeView_BlockNumXChanged;
            _recipeView.BlockNumYChanged += RecipeView_BlockNumYChanged;
            _recipeView.PcbCountChanged += RecipeView_PcbCountChanged;
            _recipeView.RotateChanged += RecipeView_RotateChanged;

            _isOnEdit = false;
            _recipeView.SetEditMode(_isOnEdit);
            ViewModeAction = RecipeManageViewMode.Nothing;
            _recipeView.SetViewMode(ViewModeAction);
        }

        private void RecipeView_Initialized(object sender, EventArgs e)
        {
            List<string> names = _recipeService.GetAllNames();
            string name = _iniService.GetName() ?? string.Empty;
            Recipe recipe = _recipeService.Get(name);
            _currentRecipe = recipe.DeepClone() ?? new Recipe();
            _editedRecipe = recipe.DeepClone() ?? new Recipe();
            _allRecipeNames = names;
            _recipeView.ShowRecipeNames(names);
            _recipeView.ShowRecipe(recipe);
            RecipeChanged?.Invoke(recipe);
        }

        private void RecipeView_LdsBypassChanged(object sender, bool e)
            => Edit(nameof(Recipe.IsLdsBypass), false);

        private void RecipeView_LowerBrBypassChanged(object sender, bool e)
        {
            Edit(nameof(Recipe.IsLowerBrBypass), e);
            if (e)
            {
                Edit(nameof(Recipe.IsMapModeBypass), false);
                Edit(nameof(Recipe.IsUpperBrBypass), false);
                _recipeView.ShowRecipe(_editedRecipe);
            }
        }

        private void RecipeView_UpperBrBypassChanged(object sender, bool e)
        {
            Edit(nameof(Recipe.IsUpperBrBypass), e);
            if (e)
            {
                Edit(nameof(Recipe.IsMapModeBypass), false);
                Edit(nameof(Recipe.IsLowerBrBypass), false);
                _recipeView.ShowRecipe(_editedRecipe);
            }
        }

        private void RecipeView_MapModeBypassChanged(object sender, bool e)
        {
            Edit(nameof(Recipe.IsMapModeBypass), e);
            if (e)
            {
                Edit(nameof(Recipe.IsUpperBrBypass), false);
                Edit(nameof(Recipe.IsLowerBrBypass), false);
                _recipeView.ShowRecipe(_editedRecipe);
            }
        }

        private void RecipeView_ThicknessChanged(object sender, string e)
        {
            Edit(nameof(Recipe.Thickness), e);
        }

        private void RecipeView_ThicknessTolerationChanged(object sender, string e)
        {
            Edit(nameof(Recipe.ThicknessPosTolerance), e);
        }

        private void RecipeView_BlockXChanged(object sender, string e)
        {
            Edit(nameof(Recipe.PcbBlockX), e);
        }

        private void RecipeView_BlockYChanged(object sender, string e)
        {
            Edit(nameof(Recipe.PcbBlockY), e);
        }

        private void RecipeView_BlockNumXChanged(object sender, string e)
        {
            Edit(nameof(Recipe.PcbBlocksX), e);
        }

        private void RecipeView_BlockNumYChanged(object sender, string e)
        {
            Edit(nameof(Recipe.PcbBlocksY), e);
        }

        private void RecipeView_PcbCountChanged(object sender, int e)
        {
            Edit(nameof(Recipe.PcbCount), e);
        }

        private void RecipeView_RotateChanged(object sender, bool e)
        {
            Edit(nameof(Recipe.IsPcbRotate), e);
        }

        private void RecipeView_ActionRequested(object sender, RecipeManageViewMode action)
        {
            if (string.IsNullOrWhiteSpace(_selectedName))
            {
                if (action == RecipeManageViewMode.Save || action == RecipeManageViewMode.Delete)
                {
                    _recipeView.SetViewMode(RecipeManageViewMode.Nothing);
                    return;
                }
            }

            ViewModeAction = action;
            _recipeView.SetViewMode(action);
            if (action == RecipeManageViewMode.Delete ||
                action == RecipeManageViewMode.Open)
                _recipeView.SetEditMode(false);
        }

        private void RecipeView_ActionConfirmed(object sender, string name)
        {
            string message = string.Empty;
            bool success = false;

            switch (ViewModeAction)
            {
                case RecipeManageViewMode.Open:
                    if (string.IsNullOrWhiteSpace(name))
                        break;
                    success = Load(name);
                    break;

                case RecipeManageViewMode.Save:
                    success = Save(out message);
                    break;

                case RecipeManageViewMode.SaveAs:
                    success = SaveNew(name, out message);
                    break;

                case RecipeManageViewMode.Delete:
                    success = Delete(name, out message);
                    break;

                default:
                    break;
            }

            //if (!string.IsNullOrEmpty(message))
            //    _recipeView.ShowMessageBox(message);

            ViewModeAction = RecipeManageViewMode.Nothing;
            _recipeView.SetViewMode(RecipeManageViewMode.Nothing);
        }

        private void RecipeView_ActionCancelled(object sender, EventArgs e)
        {
            ViewModeAction = RecipeManageViewMode.Nothing;
            _selectedName = _currentRecipe?.Name ?? "";
            _recipeView.SetSelectedName(_currentRecipe?.Name ?? "");
            _recipeView.SetViewMode(RecipeManageViewMode.Nothing);
            _recipeView.SetEditMode(_isOnEdit);
        }

        private void RecipeView_ToggleEditMode(object sender, EventArgs e)
        {
            _isOnEdit = !_isOnEdit;
            _recipeView.SetEditMode(_isOnEdit);
        }
        private void RecipeView_ModelNameSelectChanged(object sender, string e)
            => _selectedName = e;

        public bool Load(string name)
        {
            Recipe recipe = _recipeService.Get(name);
            _currentRecipe = recipe.DeepClone() ??  new Recipe();
            _editedRecipe = recipe.DeepClone() ?? new Recipe();
            _recipeView.ShowRecipe(recipe);
            RecipeChanged?.Invoke(recipe);
            return true;
        }

        public bool Save(out string message)
        {
            bool isSaved = _recipeService.Save(_editedRecipe, out message);
            if (isSaved)
            {
                _currentRecipe = _editedRecipe.DeepClone();
            }
            return isSaved;
        }

        public bool SaveNew(string newName, out string message)
        {
            Recipe newRecipe = _editedRecipe.DeepClone();
            newRecipe.Name = newName;
            bool isCreated = _recipeService.Create(newRecipe, out message);
            if (isCreated)
            {
                _allRecipeNames.Add(newName);
                _currentRecipe = newRecipe.DeepClone();
                _editedRecipe = newRecipe.DeepClone();
                _recipeView.ShowRecipeNames(_allRecipeNames);
                _recipeView.SetSelectedName(newRecipe.Name);
            }
            return isCreated;
        }

        public bool Delete(string name, out string message)
        {
            bool isDeleted = _recipeService.Delete(name, out message);
            if (isDeleted)
            {
                _recipeView.RemoveRecipeName(name);
                _allRecipeNames.Remove(name);
                if (_allRecipeNames.Any())
                    Load(_allRecipeNames.First());
                else
                {
                    _currentRecipe = new Recipe();
                    _editedRecipe = new Recipe();
                    _selectedName = string.Empty;
                    _recipeView.ShowRecipe(_currentRecipe);
                    RecipeChanged?.Invoke(_currentRecipe);
                }
            }
            return isDeleted;
        }

        private bool Edit(string propertyName, object value)
        {
            PropertyInfo[] propertys = typeof(Recipe).GetProperties();
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                    propertyName = "";
                PropertyInfo property = propertys.FirstOrDefault(p => p.Name == propertyName);
                object typedValue = Helper.ConvertValue(property.PropertyType, value);
                property.SetValue(_editedRecipe, typedValue);
                return true;
            }
            catch (Exception ex)
            {
                //message = $"Cant not edit recipy because invalid value for '{propertyName}'.";
                return false;
            }
        }

        public void RequestAction(RecipeManageViewMode action)
        {
            ViewModeAction = action;
            _recipeView.SetViewMode(action);
        }
    }
}