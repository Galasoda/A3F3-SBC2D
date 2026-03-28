using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Recipe;
using SBC_2D.Presenters;
using SBC_2D.Servicies;
using SBC_2D.Shared;
using SBC_2D.Views.Forms;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Views
{
    public partial class Form2 : Form, IRecipeView
    {
        public event EventHandler Initialized;
        public event EventHandler<RecipeManageViewMode> ActionRequested;
        public event EventHandler<string> ModelNameSelectChanged;
        public event EventHandler<string> ActionConfirmed;
        public event EventHandler ActionCancelled;
        public event EventHandler ToggleEditModeRequested;
        public event EventHandler<bool> MapModeBypassChanged;
        public event EventHandler<bool> UpperBrBypassChanged;
        public event EventHandler<bool> LowerBrBypassChanged;
        public event EventHandler<bool> LdsBypassChanged;
        public event EventHandler<string> ThicknessChanged;
        public event EventHandler<string> ThicknessTolerationChanged;
        public event EventHandler<string> BlockXChanged;
        public event EventHandler<string> BlockYChanged;
        public event EventHandler<string> BlockNumXChanged;
        public event EventHandler<string> BlockNumYChanged;
        public event EventHandler<int> PcbCountChanged;
        public event EventHandler<bool> RotateChanged;
        public event EventHandler<IZeroingView> ThicknessZeroingViewOpend;

        public Form2()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void Form2_Load(object sender, EventArgs e)
            => Initialized?.Invoke(this, EventArgs.Empty);

        // Recipe Manage Control
        private void ComboBoxRecipes_SelectedIndexChanged(object sender, EventArgs e)
            => ModelNameSelectChanged?.Invoke(sender, comboBoxRecipes.SelectedItem.ToString());
        private void ButtonToggleEditMode_Click(object sender, EventArgs e)
            => ToggleEditModeRequested?.Invoke(this, EventArgs.Empty);

        private void ButtonRecipeActionRequest_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            RecipeManageViewMode action = RecipeManageViewMode.Nothing;
            if (button == buttonCanSelectRecipe)
                action = RecipeManageViewMode.Open;
            else if (button == buttonCanSaveRecipe)
                action = RecipeManageViewMode.Save;
            else if (button == buttonCanSaveasRecipe)
                action = RecipeManageViewMode.SaveAs;
            else if (button == buttonCanDeleteRecipe)
                action = RecipeManageViewMode.Delete;
            if (action != RecipeManageViewMode.Nothing)
                ActionRequested?.Invoke(this, action);
        }

        private void ButtonConfirmAction_Click(object sender, EventArgs e)
            => ActionConfirmed?.Invoke(this, comboBoxRecipes.Text);

        private void ButtonCancelAction_Click(object sender, EventArgs e)
            => ActionCancelled?.Invoke(this, EventArgs.Empty);

        public void SetEditMode(bool isEditing)
        {
            SuspendLayout();
            foreach (GroupBox gb in Controls.OfType<GroupBox>())
            {
                if (gb != groupBoxRecipeManager)
                    gb.Enabled = isEditing;
            }
            ResumeLayout();

            buttonSwitchEditOrView.Text = isEditing ? "V" : "E";
        }

        public void SetSelectedName(string name)
        {
            if(!comboBoxRecipes.Items.Contains(name))
                return;
            comboBoxRecipes.SelectedItem = name;
            comboBoxRecipes.Text = name;
            labelRecipeName.Text = name;
        }

        public void ShowRecipeNames(IEnumerable<string> names)
        {
            comboBoxRecipes.Items.Clear();
            comboBoxRecipes.Items.AddRange(names.ToArray());
        }

        public void ShowHintForSave(bool isEnable)
        {
            buttonCanSaveRecipe.BackColor =  isEnable ? Color.Yellow : AppTheme.Surface;
            buttonCanSaveasRecipe.BackColor =  isEnable ? Color.Yellow : AppTheme.Surface;
        }

        public void RemoveRecipeName(string name)
        {
            comboBoxRecipes.Items.Remove(name);
            if (comboBoxRecipes.Items.Count > 0)
            {
                comboBoxRecipes.SelectedIndex = 0;
                labelRecipeName.Text = comboBoxRecipes.SelectedItem.ToString();
            }
        }

        public void SetViewMode(RecipeManageViewMode action)
        {
            bool isIdle = action == RecipeManageViewMode.Nothing;
            bool isSave = action == RecipeManageViewMode.Save;
            bool isSaveAs = action == RecipeManageViewMode.SaveAs;
            bool needCombo = !isIdle && !isSave;
            labelRecipeName.Visible = !needCombo;
            comboBoxRecipes.Visible = needCombo;
            comboBoxRecipes.Enabled = needCombo;
            comboBoxRecipes.DropDownStyle = isSaveAs
                ? ComboBoxStyle.DropDown
                : ComboBoxStyle.DropDownList;
            flowLayoutPanelRecipeManageCtrls.Visible = isIdle;
            flowLayoutPanelRecipeManageCtrls.BringToFront();
            flowLayoutPanelConfirmCancel.Visible = !isIdle;
            if (!isIdle)
                labelRecipeMamageAction.Text = action.ToString();
            Console.WriteLine(comboBoxRecipes.Text);
            Console.WriteLine(comboBoxRecipes.SelectedItem?.ToString() ?? "");
        }

        public void ShowRecipe(Recipe recipe)
        {
            if (recipe == null) return;
            labelRecipeName.Text = recipe.Name;
            checkBoxMapModeBypass.Checked = recipe.IsMapModeBypass;
            checkBoxUpperBrBypass.Checked = recipe.IsUpperBrBypass;
            checkBoxLowerBrBypass.Checked = recipe.IsLowerBrBypass;
            checkBoxLdsBypass.Checked = recipe.IsLdsBypass;
            textBoxPcbThickness.Text = recipe.Thickness.ToString();
            textBoxThicknessTolrence.Text = recipe.ThicknessPosTolerance.ToString();
            labelPcbMaxThickness.Text = (recipe.Thickness + recipe.ThicknessPosTolerance).ToString();
            textBoxSubstrateBlockX.Text = recipe.PcbBlockX.ToString();
            textBoxSubstrateBlockY.Text = recipe.PcbBlockY.ToString();
            textBoxSubstrateBlockNumX.Text = recipe.PcbBlocksX.ToString();
            textBoxSubstrateBlockNumY.Text = recipe.PcbBlocksY.ToString();
            radioButtonSinglePcb.Checked = recipe.PcbCount == 1;
            radioButtonDualPcb.Checked = recipe.PcbCount == 2;
            checkBoxRotate.Checked = recipe.IsPcbRotate;
            comboBoxRecipes.SelectedItem = recipe.Name;
        }

        private void CheckBoxMapModeBypass_CheckedChanged(object sender, EventArgs e)
            => MapModeBypassChanged?.Invoke(this, checkBoxMapModeBypass.Checked);

        private void CheckBoxUpperBrBypass_CheckedChanged(object sender, EventArgs e)
            => UpperBrBypassChanged?.Invoke(this, checkBoxUpperBrBypass.Checked);

        private void CheckBoxLowerBrBypass_CheckedChanged(object sender, EventArgs e)
            => LowerBrBypassChanged?.Invoke(this, checkBoxLowerBrBypass.Checked);

        private void CheckBoxLdsBypass_CheckedChanged(object sender, EventArgs e)
            => LdsBypassChanged?.Invoke(this, checkBoxLdsBypass.Checked);

        private void TextBoxPcbThickness_TextChanged(object sender, EventArgs e)
            => ThicknessChanged?.Invoke(this, textBoxPcbThickness.Text);

        private void TextBoxThicknessTolrence_TextChanged(object sender, EventArgs e)
            => ThicknessTolerationChanged?.Invoke(this, textBoxThicknessTolrence.Text);

        private void TextBoxSubstrateBlockX_TextChanged(object sender, EventArgs e)
            => BlockXChanged?.Invoke(this, textBoxSubstrateBlockX.Text);

        private void TextBoxSubstrateBlockY_TextChanged(object sender, EventArgs e)
            => BlockYChanged?.Invoke(this, textBoxSubstrateBlockY.Text);

        private void TextBoxSubstrateBlockNumX_TextChanged(object sender, EventArgs e)
            => BlockNumXChanged?.Invoke(this, textBoxSubstrateBlockNumX.Text);

        private void TextBoxSubstrateBlockNumY_TextChanged(object sender, EventArgs e)
            => BlockNumYChanged?.Invoke(this, textBoxSubstrateBlockNumY.Text);

        private void RadioButtonSinglePcb_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSinglePcb.Checked)
                PcbCountChanged?.Invoke(this, 1);
        }

        private void RadioButtonDualPcb_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDualPcb.Checked)
                PcbCountChanged?.Invoke(this, 2);
        }

        private void CheckBoxRotate_CheckedChanged(object sender, EventArgs e)
            => RotateChanged?.Invoke(this, checkBoxRotate.Checked);

        private void ButtonOpenZeroingForm_Click(object sender, EventArgs e)
        {
            FormZeroing formZeroing = new FormZeroing();
            formZeroing.Show();
            ThicknessZeroingViewOpend?.Invoke(this, formZeroing);
        }

        private void ApplyTheme()
        {
            AppTheme.ApplyForm(this);

            ApplyButtonTheme(flowLayoutPanelRecipeManageCtrls);
            ApplyButtonTheme(groupBoxThicknessSetting);
            ApplyButtonTheme(flowLayoutPanelConfirmCancel);
            AppTheme.ApplyComboBox(panelSelectRecipe, comboBoxRecipes);
            AppTheme.ApplyButton(buttonGetPcbThickness);
            AppTheme.ApplyGroupBox(groupBoxRecipeManager);
            AppTheme.ApplyGroupBox(groupBoxSubstrateSetting);
            AppTheme.ApplyGroupBox(groupBoxBypassSetting);
            AppTheme.ApplyGroupBox(groupBoxThicknessSetting);
            AppTheme.ApplyTextbox(panel5, textBoxPcbThickness);
            AppTheme.ApplyTextbox(panel6, textBoxThicknessTolrence);
        }

        private void ApplyButtonTheme(Control container)
        {
            foreach (Button btn in container.Controls.OfType<Button>())
                AppTheme.ApplyButton(btn);
        }
    }
}