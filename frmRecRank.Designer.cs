namespace FieldTool.UI
{
    partial class frmRecRank
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.NamedStyle namedStyle1 = new FarPoint.Win.Spread.NamedStyle("DataAreaMidnght");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecRank));
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType5 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType6 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.mnuPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moveSelectedRecommendationUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveSelectedRecommendationDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUp = new DevComponents.DotNetBar.ButtonX();
            this.btnDown = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.styBrandingStyleManager = new DevComponents.DotNetBar.StyleManager(this.components);
            this.btnSelectAll = new DevComponents.DotNetBar.ButtonX();
            this.btnDeselectAll = new DevComponents.DotNetBar.ButtonX();
            this.fpSpreadRecommendations = new FarPoint.Win.Spread.FpSpread();
            this.fpSpreadRecommendations_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.recommendationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recommendationBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.mnuPopup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadRecommendations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadRecommendations_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recommendationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recommendationBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuPopup
            // 
            this.mnuPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveSelectedRecommendationUpToolStripMenuItem,
            this.moveSelectedRecommendationDownToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectAllToolStripMenuItem,
            this.unselectAllToolStripMenuItem});
            this.mnuPopup.Name = "mnuPopup";
            this.mnuPopup.Size = new System.Drawing.Size(279, 98);
            // 
            // moveSelectedRecommendationUpToolStripMenuItem
            // 
            this.moveSelectedRecommendationUpToolStripMenuItem.Name = "moveSelectedRecommendationUpToolStripMenuItem";
            this.moveSelectedRecommendationUpToolStripMenuItem.ShowShortcutKeys = false;
            this.moveSelectedRecommendationUpToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.moveSelectedRecommendationUpToolStripMenuItem.Text = "Move selected recommendation up";
            this.moveSelectedRecommendationUpToolStripMenuItem.Click += new System.EventHandler(this.moveSelectedRecommendationUpToolStripMenuItem_Click);
            // 
            // moveSelectedRecommendationDownToolStripMenuItem
            // 
            this.moveSelectedRecommendationDownToolStripMenuItem.Name = "moveSelectedRecommendationDownToolStripMenuItem";
            this.moveSelectedRecommendationDownToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.moveSelectedRecommendationDownToolStripMenuItem.Text = "Move selected recommendation down";
            this.moveSelectedRecommendationDownToolStripMenuItem.Click += new System.EventHandler(this.moveSelectedRecommendationDownToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(275, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // unselectAllToolStripMenuItem
            // 
            this.unselectAllToolStripMenuItem.Name = "unselectAllToolStripMenuItem";
            this.unselectAllToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.unselectAllToolStripMenuItem.Text = "Unselect all";
            this.unselectAllToolStripMenuItem.Click += new System.EventHandler(this.unselectAllToolStripMenuItem_Click);
            // 
            // btnUp
            // 
            this.btnUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Location = new System.Drawing.Point(1037, 39);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(41, 41);
            this.btnUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUp.Symbol = "";
            this.btnUp.SymbolSize = 25F;
            this.btnUp.TabIndex = 1;
            this.btnUp.Tooltip = "Move the selected recommendation up";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Location = new System.Drawing.Point(1037, 115);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(41, 41);
            this.btnDown.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDown.Symbol = "";
            this.btnDown.SymbolSize = 25F;
            this.btnDown.TabIndex = 2;
            this.btnDown.Tooltip = "Move the selected recommendation down";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(985, 654);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(41, 41);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.Symbol = "";
            this.btnSave.SymbolSize = 25F;
            this.btnSave.TabIndex = 3;
            this.btnSave.Tooltip = "Save this configuration";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(919, 654);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(41, 41);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.Symbol = "";
            this.btnCancel.SymbolSize = 25F;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Tooltip = "Return to previous screen (no save)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // styBrandingStyleManager
            // 
            this.styBrandingStyleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styBrandingStyleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(193))))));
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.Location = new System.Drawing.Point(1037, 232);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(41, 41);
            this.btnSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelectAll.Symbol = "";
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Tooltip = "Include all recommendations in the report";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselectAll.Location = new System.Drawing.Point(1037, 310);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(41, 41);
            this.btnDeselectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDeselectAll.Symbol = "";
            this.btnDeselectAll.TabIndex = 6;
            this.btnDeselectAll.Tooltip = "Exclude all recommendations from the report";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // fpSpreadRecommendations
            // 
            this.fpSpreadRecommendations.AccessibleDescription = "fpSpreadRecommendations, Sheet1, Row 0, Column 0, ";
            this.fpSpreadRecommendations.AllowUserToTouchZoom = false;
            this.fpSpreadRecommendations.AllowUserZoom = false;
            this.fpSpreadRecommendations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fpSpreadRecommendations.BackColor = System.Drawing.Color.White;
            this.fpSpreadRecommendations.FocusRenderer = new FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer(1);
            this.fpSpreadRecommendations.Location = new System.Drawing.Point(12, 12);
            this.fpSpreadRecommendations.Name = "fpSpreadRecommendations";
            namedStyle1.BackColor = System.Drawing.Color.DarkGray;
            namedStyle1.CellType = generalCellType1;
            namedStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle1.NoteIndicatorColor = System.Drawing.Color.Red;
            namedStyle1.Renderer = generalCellType1;
            this.fpSpreadRecommendations.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle1});
            this.fpSpreadRecommendations.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.fpSpreadRecommendations.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpreadRecommendations_Sheet1});
            this.fpSpreadRecommendations.Size = new System.Drawing.Size(1014, 636);
            this.fpSpreadRecommendations.TabIndex = 7;
            this.fpSpreadRecommendations.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpSpreadRecommendations_SelectionChanged);
            this.fpSpreadRecommendations.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpreadRecommendations_CellClick);
            this.fpSpreadRecommendations.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpreadRecommendations_ButtonClicked);
            // 
            // fpSpreadRecommendations_Sheet1
            // 
            this.fpSpreadRecommendations_Sheet1.Reset();
            this.fpSpreadRecommendations_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpreadRecommendations_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpreadRecommendations_Sheet1.ColumnCount = 5;
            this.fpSpreadRecommendations_Sheet1.RowHeader.ColumnCount = 0;
            this.fpSpreadRecommendations_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            checkBoxCellType1.Picture.False = ((System.Drawing.Image)(resources.GetObject("resource.False")));
            checkBoxCellType1.Picture.Indeterminate = ((System.Drawing.Image)(resources.GetObject("resource.Indeterminate")));
            checkBoxCellType1.Picture.True = ((System.Drawing.Image)(resources.GetObject("resource.True")));
            this.fpSpreadRecommendations_Sheet1.Cells.Get(0, 0).CellType = checkBoxCellType1;
            checkBoxCellType2.Picture.False = ((System.Drawing.Image)(resources.GetObject("resource.False1")));
            checkBoxCellType2.Picture.True = ((System.Drawing.Image)(resources.GetObject("resource.True1")));
            this.fpSpreadRecommendations_Sheet1.Cells.Get(0, 1).CellType = checkBoxCellType2;
            checkBoxCellType3.Picture.False = ((System.Drawing.Image)(resources.GetObject("resource.False2")));
            checkBoxCellType3.Picture.True = ((System.Drawing.Image)(resources.GetObject("resource.True2")));
            this.fpSpreadRecommendations_Sheet1.Cells.Get(1, 0).CellType = checkBoxCellType3;
            checkBoxCellType4.Picture.False = ((System.Drawing.Image)(resources.GetObject("resource.False3")));
            checkBoxCellType4.Picture.True = ((System.Drawing.Image)(resources.GetObject("resource.True3")));
            this.fpSpreadRecommendations_Sheet1.Cells.Get(1, 1).CellType = checkBoxCellType4;
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Include";
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Detail";
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Recommendation";
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Id";
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Rank";
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced";
            this.fpSpreadRecommendations_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).AutoFilterIndex = 0;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).AutoSortIndex = 0;
            checkBoxCellType5.Picture.False = ((System.Drawing.Image)(resources.GetObject("resource.False4")));
            checkBoxCellType5.Picture.True = ((System.Drawing.Image)(resources.GetObject("resource.True4")));
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).CellType = checkBoxCellType5;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).Label = "Include";
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).Resizable = false;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).ShowSortIndicator = false;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).Tag = "Include";
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(0).Width = 80F;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).AutoFilterIndex = 0;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).AutoSortIndex = 0;
            checkBoxCellType6.Picture.False = ((System.Drawing.Image)(resources.GetObject("resource.False5")));
            checkBoxCellType6.Picture.True = ((System.Drawing.Image)(resources.GetObject("resource.True5")));
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).CellType = checkBoxCellType6;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).Label = "Detail";
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).Resizable = false;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).ShowSortIndicator = false;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).Tag = "Detail";
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(1).Width = 80F;
            textCellType1.AutoFillAutoCompleteCustomSource = false;
            textCellType1.EnableSubEditor = false;
            textCellType1.MaxLength = 1000;
            textCellType1.Multiline = true;
            textCellType1.ReadOnly = true;
            textCellType1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textCellType1.StringTrim = System.Drawing.StringTrimming.Character;
            textCellType1.WordWrap = true;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.fpSpreadRecommendations_Sheet1.Columns.Get(2).Label = "Recommendation";
            this.fpSpreadRecommendations_Sheet1.Columns.Get(2).Locked = true;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(2).Width = 400F;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(4).AutoSortIndex = 0;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.fpSpreadRecommendations_Sheet1.Columns.Get(4).Label = "Rank";
            this.fpSpreadRecommendations_Sheet1.Columns.Get(4).Width = 77F;
            this.fpSpreadRecommendations_Sheet1.DataAutoHeadings = false;
            this.fpSpreadRecommendations_Sheet1.DataAutoSizeColumns = false;
            this.fpSpreadRecommendations_Sheet1.RestrictColumns = true;
            this.fpSpreadRecommendations_Sheet1.RestrictRows = true;
            this.fpSpreadRecommendations_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpreadRecommendations_Sheet1.Rows.Default.Height = 60F;
            this.fpSpreadRecommendations_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // recommendationBindingSource
            // 
            this.recommendationBindingSource.DataSource = typeof(FieldTool.BLL.Recommendation);
            // 
            // recommendationBindingSource1
            // 
            this.recommendationBindingSource1.DataSource = typeof(FieldTool.BLL.Recommendation);
            // 
            // frmRecRank
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1084, 697);
            this.Controls.Add(this.fpSpreadRecommendations);
            this.Controls.Add(this.btnDeselectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecRank";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recommendation Rankings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecRank_FormClosing);
            this.Load += new System.EventHandler(this.frmRecRank_Load);
            this.mnuPopup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadRecommendations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadRecommendations_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recommendationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recommendationBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnUp;
        private DevComponents.DotNetBar.ButtonX btnDown;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.StyleManager styBrandingStyleManager;
        private DevComponents.DotNetBar.ButtonX btnSelectAll;
        private DevComponents.DotNetBar.ButtonX btnDeselectAll;
        private System.Windows.Forms.ContextMenuStrip mnuPopup;
        private System.Windows.Forms.ToolStripMenuItem moveSelectedRecommendationUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveSelectedRecommendationDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllToolStripMenuItem;
        private FarPoint.Win.Spread.FpSpread fpSpreadRecommendations;
        private FarPoint.Win.Spread.SheetView fpSpreadRecommendations_Sheet1;
        private System.Windows.Forms.BindingSource recommendationBindingSource;
        private System.Windows.Forms.BindingSource recommendationBindingSource1;
    }
}