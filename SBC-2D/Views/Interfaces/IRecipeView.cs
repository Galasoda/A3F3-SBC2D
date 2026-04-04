using SBC_2D.Shared;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Views
{
    public interface IRecipeView
    {
        event EventHandler<RecipeManageViewMode> ActionRequested;
        event EventHandler<string> ModelNameSelectChanged;
        event EventHandler<string> ActionConfirmed;
        event EventHandler ActionCancelled;
        event EventHandler ToggleEditModeRequested;
        event EventHandler<bool> MapModeBypassChanged;
        event EventHandler<bool> UpperBrBypassChanged;
        event EventHandler<bool> LowerBrBypassChanged;
        event EventHandler<bool> LdsBypassChanged;
        event EventHandler<string> ThicknessChanged;
        event EventHandler<string> ThicknessTolerationChanged;
        event EventHandler<string> BlockXChanged;
        event EventHandler<string> BlockYChanged;
        event EventHandler<string> BlockNumXChanged;
        event EventHandler<string> BlockNumYChanged;
        event EventHandler<bool> RotateChanged;
        event EventHandler<IZeroingView> ThicknessZeroingViewOpend;

        void ShowRecipeNames(IEnumerable<string> names);
        void ShowRecipe(Recipe recipe);
        void SetEditMode(bool isEditing);
        void EnableEditMode(bool isEnable);
        void SetSelectedName(string name);
        void SetViewMode(RecipeManageViewMode action);
        void ShowHintForSave(bool isEnable);
        void RemoveRecipeName(string name);
    }
}