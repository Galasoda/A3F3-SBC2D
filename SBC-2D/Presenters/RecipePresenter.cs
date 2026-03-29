using Dapper.FluentMap.Mapping;
using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Recipe;
using SBC_2D.Servicies;
using SBC_2D.Shared;
using SBC_2D.Views;
using SBC_2D.Views.Forms;
using SBC_2D.Views.Interfaces;
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
        private IZeroingView _zeroingView;
        private bool _isOnEdit;
        private Recipe _editedRecipe;
        private Recipe _currentRecipe;
        private List<string> _allRecipeNames;
        private string _selectedName = "";
        private RecipeManageViewMode _recipeManageViewMode;
        private readonly Dictionary<string, PropertyInfo> _propertyCache;

        // Events
        public event Action<Recipe> RecipeChanged;
        public event Action<List<string>> Initialized;
        public event Action<bool> OnValueChanged;

        public RecipePresenter(RecipeService recipeService, IniService iniService, IRecipeView recipeView)
        {
            _recipeService = recipeService;
            _iniService = iniService;
            _recipeView = recipeView;
            _editedRecipe = new Recipe();
            _currentRecipe = new Recipe();
            _propertyCache = typeof(Recipe).GetProperties().ToDictionary(p => p.Name);
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
            _recipeView.ThicknessZeroingViewOpend += RecipeView_ThicknessZeroingViewOpened;

            _isOnEdit = false;
            _recipeView.SetEditMode(_isOnEdit);
            _recipeManageViewMode = RecipeManageViewMode.Nothing;
            _recipeView.SetViewMode(_recipeManageViewMode);
        }

        private void RecipeView_Initialized(object sender, EventArgs e)
        {
            _isOnEdit = false;
            List<string> names = _recipeService.GetAllNames();
            string name = _iniService.GetCurrentRecipeName() ?? string.Empty;
            Recipe recipe = _recipeService.Get(name) ?? new Recipe();
            _allRecipeNames = names;
            _recipeView.ShowRecipeNames(names);
            Load(name);
        }

        private void RecipeView_LdsBypassChanged(object sender, bool e)
            => Edit(nameof(Recipe.IsLdsBypass), e);

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

            _recipeManageViewMode = action;
            _recipeView.SetViewMode(action);
            if (action == RecipeManageViewMode.Delete || action == RecipeManageViewMode.Open)
                _recipeView.SetEditMode(false);
        }

        private void RecipeView_ActionConfirmed(object sender, string name)
        {
            string message = string.Empty;

            switch (_recipeManageViewMode)
            {
                case RecipeManageViewMode.Open:
                    if (string.IsNullOrWhiteSpace(name))
                        break;
                    Load(name);
                    break;

                case RecipeManageViewMode.Save:
                    Save(out message);
                    break;

                case RecipeManageViewMode.SaveAs:
                    SaveNew(name, out message);
                    break;

                case RecipeManageViewMode.Delete:
                    Delete(name, out message);
                    break;

                default:
                    break;
            }

            //if (!string.IsNullOrEmpty(message))
            //    _recipeView.ShowMessageBox(message);

            _recipeManageViewMode = RecipeManageViewMode.Nothing;
            _recipeView.SetViewMode(RecipeManageViewMode.Nothing);
        }

        private void RecipeView_ActionCancelled(object sender, EventArgs e)
        {
            _recipeManageViewMode = RecipeManageViewMode.Nothing;
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

        public void Load(string name)
        {
            Recipe recipe = _recipeService.Get(name) ?? new Recipe();
            ChangeRecipe(recipe);
        }

        public void Save(out string message)
        {
            bool isSaved = _recipeService.Save(_editedRecipe, out message);
            if (isSaved)
            {
                _currentRecipe = _editedRecipe.DeepClone();
                _recipeView.ShowHintForSave(false);
            }
        }

        public void SaveNew(string newName, out string message)
        {
            Recipe newRecipe = _editedRecipe.DeepClone();
            newRecipe.Name = newName;
            bool isCreated = _recipeService.Create(newRecipe, out message);
            if (isCreated)
            {
                _allRecipeNames.Add(newName);
                _recipeView.ShowRecipeNames(_allRecipeNames);
                _recipeView.SetSelectedName(newRecipe.Name);
                ChangeRecipe(newRecipe);
            }
        }

        public void Delete(string name, out string message)
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
                    ChangeRecipe(new Recipe());
                    _selectedName = string.Empty;
                }
            }
        }

        private void Edit(string propertyName, object value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                    propertyName = "";
                PropertyInfo property = _propertyCache[propertyName];
                object typedValue = Helper.ConvertValue(property.PropertyType, value);
                property.SetValue(_editedRecipe, typedValue);
                bool isValueChanged = !_editedRecipe.Equals(_currentRecipe);
                _recipeView.ShowHintForSave(isValueChanged);
            }
            catch (Exception ex)
            {
                //message = $"Cant not edit recipy because invalid value for '{propertyName}'.";
            }
        }

        private void ChangeRecipe(Recipe recipe)
        {
            _currentRecipe = recipe.DeepClone();
            _editedRecipe = recipe.DeepClone();
            _recipeView.ShowRecipe(recipe);
            _recipeView.ShowHintForSave(false);
            _isOnEdit = false;
            _recipeView.SetEditMode(false);
            _iniService.SaveCurrentRecipeName(_currentRecipe.Name);
            RecipeChanged?.Invoke(recipe);
        }

        public void RequestAction(RecipeManageViewMode action)
        {
            _recipeManageViewMode = action;
            _recipeView.SetViewMode(action);
        }

        private void RecipeView_ThicknessZeroingViewOpened(object _, IZeroingView zeroingView)
        {
            UnsubscribeZeroingView();
            _zeroingView = zeroingView;
            _zeroingView.ThicknessZeroBiasChanged += ZeroingView_ThicknessZeroBiasChanged;
            _zeroingView.ViewClosed += ZeroingView_OnZeroingViewClosed;
            _zeroingView.SetThicknessZeroBias(_editedRecipe.ThicknessZeroBias.ToString());
        }

        private void ZeroingView_ThicknessZeroBiasChanged(object sender, string e)
            => Edit(nameof(Recipe.ThicknessZeroBias), e);

        private void ZeroingView_OnZeroingViewClosed(object sender, EventArgs e)
            => UnsubscribeZeroingView();

        private void UnsubscribeZeroingView()
        {
            if (_zeroingView == null) return;
            _zeroingView.ThicknessZeroBiasChanged -= ZeroingView_ThicknessZeroBiasChanged;
            _zeroingView.ViewClosed -= ZeroingView_OnZeroingViewClosed;
            _zeroingView.CloseView();
            _zeroingView = null;
        }
    }
}