//using SBC_2D.Infrastructures;
//using SBC_2D.Infrastructures.Recipe;
//using SBC_2D.Presenters;
//using SBC_2D.Shared;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Forms.VisualStyles;

//namespace SBC_2D.Views.Forms
//{
//    internal class copy
//    {
//        private RecipePresenter _recipePresenter;

//        public copy()
//        {
//            InitializeComponent();
//            ApplyTheme();
//        }

//        public void SetPresenter(RecipePresenter recipePresenter)
//        {
//            _recipePresenter = recipePresenter;
//            _recipePresenter.RecipeChanged += OnRecipeChanged;
//            _recipePresenter.Initialized += OnInitialized;
//            _recipePresenter.OnValueChanged += OnValueChanged;
//        }


//        // ── IRecipeView 實作 ──────────────────────────────────────

//        public void ShowRecipeNames(IEnumerable<string> names)
//        {
//            comboBoxRecipes.Items.Clear();
//            comboBoxRecipes.Items.AddRange(names.ToArray());
//        }

//        public void ShowRecipe(Recipe recipe)
//        {

//            if (recipe == null) return;
//            labelRecipeName.Text = recipe.Name;
//            checkBoxMapModeBypass.Checked = recipe.IsMapModeBypass;
//            checkBoxUpperBrBypass.Checked = recipe.IsUpperBrBypass;
//            checkBoxLowerBrBypass.Checked = recipe.IsLowerBrBypass;
//            checkBoxLdsBypass.Checked = recipe.IsLdsBypass;
//            textBoxPcbThickness.Text = recipe.Thickness.ToString();
//            textBoxThicknessTolrence.Text = recipe.ThicknessPosTolerance.ToString();
//            labelPcbMaxThickness.Text = (recipe.Thickness + recipe.ThicknessPosTolerance).ToString();
//            textBoxThicknessZeroBias.Text = recipe.ThicknessZeroBias.ToString();
//            textBoxSubstrateBlockX.Text = recipe.PcbBlockX.ToString();
//            textBoxSubstrateBlockY.Text = recipe.PcbBlockY.ToString();
//            textBoxSubstrateBlockNumX.Text = recipe.PcbBlocksX.ToString();
//            textBoxSubstrateBlockNumY.Text = recipe.PcbBlocksY.ToString();
//            radioButtonSinglePcb.Checked = recipe.PcbCount == 1;
//            radioButtonDualPcb.Checked = recipe.PcbCount == 2;
//            checkBoxRotate.Checked = recipe.IsPcbRotate;
//            comboBoxRecipes.SelectedItem = recipe.Name;
//        }

//        public void SetEditMode(bool isEditing)
//        {
//            SuspendLayout();
//            foreach (GroupBox gb in Controls.OfType<GroupBox>())
//            {
//                if (gb != groupBoxRecipeManager)
//                    gb.Enabled = isEditing;
//            }
//            ResumeLayout();

//            buttonSwitchEditOrView.Text = isEditing ? "V" : "E";
//        }

//        public void SetViewMode(RecipeManageAction action)
//        {
//            bool needCombo = action != RecipeManageAction.Nothing && action != RecipeManageAction.Save;
//            bool isSaveAs = action == RecipeManageAction.SaveAs;

//            labelRecipeName.Visible = !needCombo;
//            comboBoxRecipes.Visible = needCombo;
//            comboBoxRecipes.Enabled = needCombo;
//            flowLayoutPanelRecipeManageCtrls.BringToFront();
//            flowLayoutPanelRecipeManageCtrls.Visible = action == RecipeManageAction.Nothing;
//            flowLayoutPanelConfirmCancel.Visible = action != RecipeManageAction.Nothing;
//            comboBoxRecipes.DropDownStyle = isSaveAs ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
//            if (action != RecipeManageAction.Nothing)
//                labelRecipeMamageAction.Text = action.ToString();

//            //SetEditMode(false);
//        }

//        public void ShowMessageBox(string message)
//            => Msgbox.MyInformation(message);

//        public void RemoveRecipeName(string name)
//        {
//            comboBoxRecipes.Items.Remove(name);
//            if (comboBoxRecipes.Items.Count > 0)
//                comboBoxRecipes.SelectedIndex = 0;
//        }

//        // ── 事件：Presenter → View ────────────────────────────────

//        private void OnInitialized(List<string> names) => ShowRecipeNames(names);

//        private void OnRecipeChanged(Recipe recipe) => ShowRecipe(recipe);

//        private void OnValueChanged(bool isChanged)
//        {
//            Color color = isChanged ? Color.Yellow : AppTheme.Surface;
//            buttonCanSaveRecipe.BackColor = color;
//            buttonCanSaveasRecipe.BackColor = color;
//        }

//        // ── Button Handlers ───────────────────────────────────────

//        private void ButtonSwitchEditOrView_Click(object sender, EventArgs e)
//            => _recipePresenter.SwitchEditOrView();


//        private void ButtonCanSelectRecipe_Click(object sender, EventArgs e)
//            => _recipePresenter.RequestAction(RecipeManageAction.Open);

//        private void ButtonCanSaveRecipe_Click(object sender, EventArgs e)
//            => _recipePresenter.RequestAction(RecipeManageAction.Save);

//        private void ButtonCanSaveasRecipe_Click(object sender, EventArgs e)
//            => _recipePresenter.RequestAction(RecipeManageAction.SaveAs);

//        private void ButtonCanDeleteRecipe_Click(object sender, EventArgs e)
//            => _recipePresenter.RequestAction(RecipeManageAction.Delete);

//        private void ButtonConfirmAction_Click(object sender, EventArgs e)
//            => _recipePresenter.ConfirmAction(comboBoxRecipes.Text);

//        private void ButtonCancelAction_Click(object sender, EventArgs e)
//        {
//            _recipePresenter.CancelAction();
//            panelSelectRecipe.Invalidate();
//            comboBoxRecipes.Invalidate();
//        }

//        // ── Bypass CheckBoxes ─────────────────────────────────────

//        private void CheckBoxMapModeBypass_CheckedChanged(object sender, EventArgs e)
//        {
//            _recipePresenter.Edit(nameof(Recipe.IsMapModeBypass), checkBoxMapModeBypass.Checked);
//            if (checkBoxMapModeBypass.Checked)
//            {
//                checkBoxUpperBrBypass.Checked = false;
//                checkBoxLowerBrBypass.Checked = false;
//            }
//        }

//        private void CheckBoxUpperBrBypass_CheckedChanged(object sender, EventArgs e)
//        {
//            _recipePresenter.Edit(nameof(Recipe.IsUpperBrBypass), checkBoxUpperBrBypass.Checked);
//            if (checkBoxUpperBrBypass.Checked)
//            {
//                checkBoxMapModeBypass.Checked = false;
//                checkBoxLowerBrBypass.Checked = false;
//            }
//        }

//        private void CheckBoxLowerBrBypass_CheckedChanged(object sender, EventArgs e)
//        {
//            _recipePresenter.Edit(nameof(Recipe.IsLowerBrBypass), checkBoxLowerBrBypass.Checked);
//            if (checkBoxLowerBrBypass.Checked)
//            {
//                checkBoxMapModeBypass.Checked = false;
//                checkBoxUpperBrBypass.Checked = false;
//            }
//        }

//        private void CheckBoxLdsBypass_CheckedChanged(object sender, EventArgs e)
//            => _recipePresenter.Edit(nameof(Recipe.IsLdsBypass), checkBoxLdsBypass.Checked);

//        // ── TextBox 驗證（提取共用方法）──────────────────────────

//        /// <summary>確保 TextBox 數值 >= 1，並通知 Presenter</summary>
//        private void HandlePositiveIntTextChanged(TextBox tb, string propertyName)
//        {
//            if (string.IsNullOrWhiteSpace(tb.Text) ||
//                (int.TryParse(tb.Text, out int v) && v < 1))
//            {
//                tb.Text = "1";
//                tb.SelectionStart = tb.Text.Length;
//            }
//            _recipePresenter.Edit(propertyName, tb.Text);
//        }

//        private void TextBoxPcbThickness_TextChanged(object sender, EventArgs e)
//            => HandlePositiveIntTextChanged(sender as TextBox, nameof(Recipe.Thickness));

//        private void TextBoxThicknessZeroBias_TextChanged(object sender, EventArgs e)
//            => HandlePositiveIntTextChanged(sender as TextBox, nameof(Recipe.ThicknessZeroBias));

//        private void TextBoxPcbBlockX_TextChanged(object sender, EventArgs e)
//            => HandlePositiveIntTextChanged(sender as TextBox, nameof(Recipe.PcbBlockX));

//        private void TextBoxPcbBlockY_TextChanged(object sender, EventArgs e)
//            => HandlePositiveIntTextChanged(sender as TextBox, nameof(Recipe.PcbBlockY));

//        private void TextBoxBlockNumX_TextChanged(object sender, EventArgs e)
//            => HandlePositiveIntTextChanged(sender as TextBox, nameof(Recipe.PcbBlocksX));

//        private void TextBoxBlockNumY_TextChanged(object sender, EventArgs e)
//            => HandlePositiveIntTextChanged(sender as TextBox, nameof(Recipe.PcbBlocksY));

//        private void RadioButtonSinglePcb_CheckedChanged(object sender, EventArgs e)
//            => _recipePresenter.Edit(nameof(Recipe.PcbCount), 1);

//        private void RadioButtonDualPcb_CheckedChanged(object sender, EventArgs e)
//            => _recipePresenter.Edit(nameof(Recipe.PcbCount), 2);

//        private void CheckBoxRotate_CheckedChanged(object sender, EventArgs e)
//            => _recipePresenter.Edit(nameof(Recipe.IsPcbRotate), checkBoxRotate.Checked);

//        private void TextBoxThicknessTolrence_TextChanged(object sender, EventArgs e)
//            => _recipePresenter.Edit(nameof(Recipe.ThicknessPosTolerance), textBoxThicknessTolrence.Text);

//        // ── Form Events ───────────────────────────────────────────

//        private void Form2_Load(object sender, EventArgs e)
//        {
//            _recipePresenter.RequestAction(RecipeManageAction.Nothing);
//            SetEditMode(false);
//        }

//        protected override void OnMouseDown(MouseEventArgs e)
//        {
//            Control clicked = GetChildAtPoint(e.Location);
//            if (clicked != panelSelectRecipe && clicked != comboBoxRecipes)
//                ActiveControl = null;
//            base.OnMouseDown(e);
//        }

//        // ── ComboBox / Panel Invalidate ───────────────────────────

//        private void ComboBoxRecipes_Enter(object sender, EventArgs e) => panelSelectRecipe.Invalidate();
//        private void ComboBoxRecipes_Leave(object sender, EventArgs e) => panelSelectRecipe.Invalidate();
//        private void ComboBoxRecipes_DropDown(object sender, EventArgs e) => panelSelectRecipe.Invalidate();
//        private void ComboBoxRecipes_DropDownClosed(object sender, EventArgs e) => panelSelectRecipe.Invalidate();
//        private void PanelSelectRecipe_MouseClick(object sender, MouseEventArgs e) => panelSelectRecipe.Invalidate();
//        private void PanelSelectRecipe_Leave(object sender, EventArgs e) => panelSelectRecipe.Invalidate();

//        // ── Theme ─────────────────────────────────────────────────

//        private void ApplyTheme()
//        {
//            AppTheme.ApplyForm(this);

//            ApplyButtonTheme(flowLayoutPanelRecipeManageCtrls);
//            ApplyButtonTheme(groupBoxThicknessSetting);
//            ApplyButtonTheme(flowLayoutPanelConfirmCancel);

//            AppTheme.ApplyComboBox(panelSelectRecipe, comboBoxRecipes);
//            AppTheme.ApplyButton(buttonGetPcbThickness);
//            AppTheme.ApplyGroupBox(groupBoxRecipeManager);
//            AppTheme.ApplyGroupBox(groupBoxSubstrateSetting);
//            AppTheme.ApplyGroupBox(groupBoxBypassSetting);
//            AppTheme.ApplyGroupBox(groupBoxThicknessSetting);
//            AppTheme.ApplyTextbox(panel5, textBoxPcbThickness);
//            AppTheme.ApplyTextbox(panel6, textBoxThicknessTolrence);
//        }

//        private void ApplyButtonTheme(Control container)
//        {
//            foreach (Button btn in container.Controls.OfType<Button>())
//                AppTheme.ApplyButton(btn);
//        }
//    }
//}
