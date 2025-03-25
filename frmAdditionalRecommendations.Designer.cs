namespace FieldTool.UI {
    partial class frmAdditionalRecommendations {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.GroupInfo groupInfo1 = new FarPoint.Win.Spread.GroupInfo();
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.Color.SteelBlue, 2, false, false, false, true);
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.Color.SteelBlue, 2, false, false, false, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdditionalRecommendations));
            this.pnlSaveButtons = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.itmSaveButtons = new DevComponents.DotNetBar.ItemContainer();
            this.btnCancel = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnSave = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.pnlSelectButtons = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.itmSelectButtons = new DevComponents.DotNetBar.ItemContainer();
            this.btnSelectAll = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnUnselectAll = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.grdRecommendations = new FarPoint.Win.Spread.FpSpread();
            this.grdRecommendations_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblItemCount = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecommendations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecommendations_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSaveButtons
            // 
            this.pnlSaveButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSaveButtons.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pnlSaveButtons.BackgroundStyle.Class = "MetroTilePanel";
            this.pnlSaveButtons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pnlSaveButtons.ContainerControlProcessDialogKey = true;
            this.pnlSaveButtons.ForeColor = System.Drawing.Color.Black;
            this.pnlSaveButtons.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Right;
            this.pnlSaveButtons.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itmSaveButtons});
            this.pnlSaveButtons.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.pnlSaveButtons.Location = new System.Drawing.Point(1223, 666);
            this.pnlSaveButtons.Name = "pnlSaveButtons";
            this.pnlSaveButtons.Size = new System.Drawing.Size(175, 83);
            this.pnlSaveButtons.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlSaveButtons.TabIndex = 2;
            this.pnlSaveButtons.Text = "metroTilePanel4";
            // 
            // itmSaveButtons
            // 
            // 
            // 
            // 
            this.itmSaveButtons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itmSaveButtons.ItemSpacing = 30;
            this.itmSaveButtons.Name = "itmSaveButtons";
            this.itmSaveButtons.ResizeItemsToFit = false;
            this.itmSaveButtons.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnCancel,
            this.btnSave});
            // 
            // 
            // 
            this.itmSaveButtons.TitleStyle.Class = "MetroTileGroupTitle";
            this.itmSaveButtons.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itmSaveButtons.TitleText = "First";
            this.itmSaveButtons.TitleVisible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndent = new System.Drawing.Point(0, -4);
            this.btnCancel.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Symbol = "";
            this.btnCancel.SymbolColor = System.Drawing.Color.Empty;
            this.btnCancel.SymbolSize = 25F;
            this.btnCancel.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            this.btnCancel.TileSize = new System.Drawing.Size(41, 41);
            // 
            // 
            // 
            this.btnCancel.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnCancel.Tooltip = "Cancel (No save)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndent = new System.Drawing.Point(0, -4);
            this.btnSave.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.Name = "btnSave";
            this.btnSave.Symbol = "";
            this.btnSave.SymbolColor = System.Drawing.Color.Empty;
            this.btnSave.SymbolSize = 25F;
            this.btnSave.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            this.btnSave.TileSize = new System.Drawing.Size(41, 41);
            // 
            // 
            // 
            this.btnSave.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnSave.Tooltip = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlSelectButtons
            // 
            this.pnlSelectButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSelectButtons.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pnlSelectButtons.BackgroundStyle.Class = "MetroTilePanel";
            this.pnlSelectButtons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pnlSelectButtons.ContainerControlProcessDialogKey = true;
            this.pnlSelectButtons.ForeColor = System.Drawing.Color.Black;
            this.pnlSelectButtons.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Right;
            this.pnlSelectButtons.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itmSelectButtons});
            this.pnlSelectButtons.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.pnlSelectButtons.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.pnlSelectButtons.Location = new System.Drawing.Point(1368, 226);
            this.pnlSelectButtons.Name = "pnlSelectButtons";
            this.pnlSelectButtons.Size = new System.Drawing.Size(84, 173);
            this.pnlSelectButtons.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlSelectButtons.TabIndex = 3;
            this.pnlSelectButtons.Text = "metroTilePanel4";
            // 
            // itmSelectButtons
            // 
            // 
            // 
            // 
            this.itmSelectButtons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itmSelectButtons.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Right;
            this.itmSelectButtons.ItemSpacing = 30;
            this.itmSelectButtons.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itmSelectButtons.Name = "itmSelectButtons";
            this.itmSelectButtons.ResizeItemsToFit = false;
            this.itmSelectButtons.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSelectAll,
            this.btnUnselectAll});
            // 
            // 
            // 
            this.itmSelectButtons.TitleStyle.Class = "MetroTileGroupTitle";
            this.itmSelectButtons.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itmSelectButtons.TitleText = "First";
            this.itmSelectButtons.TitleVisible = false;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.ImageIndent = new System.Drawing.Point(0, -4);
            this.btnSelectAll.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Symbol = "";
            this.btnSelectAll.SymbolColor = System.Drawing.Color.Empty;
            this.btnSelectAll.SymbolSize = 25F;
            this.btnSelectAll.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            this.btnSelectAll.TileSize = new System.Drawing.Size(41, 41);
            // 
            // 
            // 
            this.btnSelectAll.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnSelectAll.Tooltip = "Select all";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.ImageIndent = new System.Drawing.Point(0, -4);
            this.btnUnselectAll.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Symbol = "";
            this.btnUnselectAll.SymbolColor = System.Drawing.Color.Empty;
            this.btnUnselectAll.SymbolSize = 25F;
            this.btnUnselectAll.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            this.btnUnselectAll.TileSize = new System.Drawing.Size(41, 41);
            // 
            // 
            // 
            this.btnUnselectAll.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnUnselectAll.Tooltip = "Unselect all";
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // grdRecommendations
            // 
            this.grdRecommendations.AccessibleDescription = "grdRecommendations, Sheet1, Row 0, Column 0, ";
            this.grdRecommendations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRecommendations.BackColor = System.Drawing.Color.White;
            this.grdRecommendations.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdRecommendations.Location = new System.Drawing.Point(13, 12);
            this.grdRecommendations.Name = "grdRecommendations";
            this.grdRecommendations.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdRecommendations_Sheet1});
            this.grdRecommendations.Size = new System.Drawing.Size(1369, 654);
            this.grdRecommendations.TabIndex = 4;
            this.grdRecommendations.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdRecommendations_CellClick);
            this.grdRecommendations.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdRecommendations_CellDoubleClick);
            // 
            // grdRecommendations_Sheet1
            // 
            this.grdRecommendations_Sheet1.Reset();
            this.grdRecommendations_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdRecommendations_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdRecommendations_Sheet1.ColumnCount = 13;
            this.grdRecommendations_Sheet1.RowCount = 50;
            this.grdRecommendations_Sheet1.RowHeader.ColumnCount = 0;
            this.grdRecommendations_Sheet1.AllowNoteEdit = false;
            this.grdRecommendations_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grdRecommendations_Sheet1.Cells.Get(0, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 8).Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Cells.Get(0, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(0, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(1, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(2, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(3, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(4, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(5, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(6, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(7, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(8, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(9, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(10, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(11, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(12, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(13, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(14, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(15, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(16, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(17, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(18, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(19, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(20, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(21, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(22, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(23, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(24, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(25, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(26, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(27, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(28, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(29, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(30, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(31, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(32, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(33, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(34, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(35, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(36, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(37, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(38, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(39, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(40, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(41, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(42, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(43, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(44, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(45, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(46, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(47, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(48, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 0).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 1).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 2).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 3).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 4).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 5).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 6).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 7).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 8).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 9).CanFocus = true;
            this.grdRecommendations_Sheet1.Cells.Get(49, 10).CanFocus = true;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Include?";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Type";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Recommendation Name";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Program";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Qty";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Size";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "kWh";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "MCF";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "$ Annual";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "$ Rebate";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = " ";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "SortIndex";
            this.grdRecommendations_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "RecommendationId";
            this.grdRecommendations_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.grdRecommendations_Sheet1.Columns.Get(0).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(0).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(0).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(0).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(0).CellPadding.Top = 3;
            this.grdRecommendations_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.grdRecommendations_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(0).Label = "Include?";
            this.grdRecommendations_Sheet1.Columns.Get(0).Resizable = false;
            this.grdRecommendations_Sheet1.Columns.Get(0).Tag = "Include";
            this.grdRecommendations_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(0).Width = 72F;
            this.grdRecommendations_Sheet1.Columns.Get(1).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(1).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(1).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(1).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(1).CellPadding.Top = 3;
            textCellType1.ReadOnly = true;
            textCellType1.ShrinkToFit = true;
            this.grdRecommendations_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.grdRecommendations_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(1).Label = "Type";
            this.grdRecommendations_Sheet1.Columns.Get(1).Locked = true;
            this.grdRecommendations_Sheet1.Columns.Get(1).Tag = "Type";
            this.grdRecommendations_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(1).Width = 135F;
            this.grdRecommendations_Sheet1.Columns.Get(2).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(2).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(2).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(2).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(2).CellPadding.Top = 3;
            textCellType2.MaxLength = 1000;
            textCellType2.ReadOnly = true;
            textCellType2.ShrinkToFit = true;
            textCellType2.WordWrap = true;
            this.grdRecommendations_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.grdRecommendations_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRecommendations_Sheet1.Columns.Get(2).Label = "Recommendation Name";
            this.grdRecommendations_Sheet1.Columns.Get(2).Locked = true;
            this.grdRecommendations_Sheet1.Columns.Get(2).Tag = "RecommendationName";
            this.grdRecommendations_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(2).Width = 577F;
            this.grdRecommendations_Sheet1.Columns.Get(3).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(3).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(3).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(3).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(3).CellPadding.Top = 3;
            textCellType3.MaxLength = 1000;
            textCellType3.ReadOnly = true;
            textCellType3.ShrinkToFit = true;
            textCellType3.WordWrap = true;
            this.grdRecommendations_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.grdRecommendations_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRecommendations_Sheet1.Columns.Get(3).Label = "Program";
            this.grdRecommendations_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(3).Visible = false;
            this.grdRecommendations_Sheet1.Columns.Get(3).Width = 145F;
            this.grdRecommendations_Sheet1.Columns.Get(4).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(4).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(4).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(4).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(4).CellPadding.Top = 3;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.ReadOnly = true;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            numberCellType1.ShrinkToFit = true;
            this.grdRecommendations_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.grdRecommendations_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRecommendations_Sheet1.Columns.Get(4).Label = "Qty";
            this.grdRecommendations_Sheet1.Columns.Get(4).Locked = true;
            this.grdRecommendations_Sheet1.Columns.Get(4).Tag = "Quantity";
            this.grdRecommendations_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(4).Width = 73F;
            this.grdRecommendations_Sheet1.Columns.Get(5).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(5).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(5).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(5).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(5).CellPadding.Top = 3;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.ReadOnly = true;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            numberCellType2.ShrinkToFit = true;
            this.grdRecommendations_Sheet1.Columns.Get(5).CellType = numberCellType2;
            this.grdRecommendations_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRecommendations_Sheet1.Columns.Get(5).Label = "Size";
            this.grdRecommendations_Sheet1.Columns.Get(5).Locked = true;
            this.grdRecommendations_Sheet1.Columns.Get(5).Tag = "Size";
            this.grdRecommendations_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(5).Width = 72F;
            this.grdRecommendations_Sheet1.Columns.Get(6).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(6).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(6).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(6).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(6).CellPadding.Top = 3;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.ReadOnly = true;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            numberCellType3.ShrinkToFit = true;
            this.grdRecommendations_Sheet1.Columns.Get(6).CellType = numberCellType3;
            this.grdRecommendations_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRecommendations_Sheet1.Columns.Get(6).Label = "kWh";
            this.grdRecommendations_Sheet1.Columns.Get(6).Locked = true;
            this.grdRecommendations_Sheet1.Columns.Get(6).Tag = "Kwh";
            this.grdRecommendations_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(6).Width = 72F;
            this.grdRecommendations_Sheet1.Columns.Get(7).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(7).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(7).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(7).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(7).CellPadding.Top = 3;
            numberCellType4.MinimumValue = 0D;
            numberCellType4.ReadOnly = true;
            numberCellType4.Separator = ",";
            numberCellType4.ShowSeparator = true;
            numberCellType4.ShrinkToFit = true;
            this.grdRecommendations_Sheet1.Columns.Get(7).CellType = numberCellType4;
            this.grdRecommendations_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRecommendations_Sheet1.Columns.Get(7).Label = "MCF";
            this.grdRecommendations_Sheet1.Columns.Get(7).Locked = true;
            this.grdRecommendations_Sheet1.Columns.Get(7).Tag = "Mcf";
            this.grdRecommendations_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(7).Width = 72F;
            this.grdRecommendations_Sheet1.Columns.Get(8).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(8).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(8).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(8).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(8).CellPadding.Top = 3;
            currencyCellType1.DecimalPlaces = 2;
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            currencyCellType1.ReadOnly = true;
            currencyCellType1.ShowSeparator = true;
            currencyCellType1.ShrinkToFit = true;
            this.grdRecommendations_Sheet1.Columns.Get(8).CellType = currencyCellType1;
            this.grdRecommendations_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRecommendations_Sheet1.Columns.Get(8).Label = "$ Annual";
            this.grdRecommendations_Sheet1.Columns.Get(8).Locked = true;
            this.grdRecommendations_Sheet1.Columns.Get(8).Tag = "Annual";
            this.grdRecommendations_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(8).Width = 72F;
            this.grdRecommendations_Sheet1.Columns.Get(9).CanFocus = true;
            this.grdRecommendations_Sheet1.Columns.Get(9).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(9).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(9).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(9).CellPadding.Top = 3;
            currencyCellType2.DecimalPlaces = 2;
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            currencyCellType2.ReadOnly = true;
            currencyCellType2.ShowSeparator = true;
            currencyCellType2.ShrinkToFit = true;
            this.grdRecommendations_Sheet1.Columns.Get(9).CellType = currencyCellType2;
            this.grdRecommendations_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRecommendations_Sheet1.Columns.Get(9).Label = "$ Rebate";
            this.grdRecommendations_Sheet1.Columns.Get(9).Locked = true;
            this.grdRecommendations_Sheet1.Columns.Get(9).Tag = "Rebate";
            this.grdRecommendations_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(9).Width = 75F;
            this.grdRecommendations_Sheet1.Columns.Get(10).CanFocus = false;
            this.grdRecommendations_Sheet1.Columns.Get(10).CellPadding.Bottom = 3;
            this.grdRecommendations_Sheet1.Columns.Get(10).CellPadding.Left = 3;
            this.grdRecommendations_Sheet1.Columns.Get(10).CellPadding.Right = 3;
            this.grdRecommendations_Sheet1.Columns.Get(10).CellPadding.Top = 3;
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "Edit";
            this.grdRecommendations_Sheet1.Columns.Get(10).CellType = buttonCellType1;
            this.grdRecommendations_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(10).ForeColor = System.Drawing.Color.DodgerBlue;
            this.grdRecommendations_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(10).Label = " ";
            this.grdRecommendations_Sheet1.Columns.Get(10).Tag = "Select";
            this.grdRecommendations_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRecommendations_Sheet1.Columns.Get(10).Width = 61F;
            this.grdRecommendations_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grdRecommendations_Sheet1.Columns.Get(11).Label = "SortIndex";
            this.grdRecommendations_Sheet1.Columns.Get(11).Tag = "SortIndex";
            this.grdRecommendations_Sheet1.Columns.Get(11).Visible = false;
            this.grdRecommendations_Sheet1.Columns.Get(12).Label = "RecommendationId";
            this.grdRecommendations_Sheet1.Columns.Get(12).Tag = "RecommendationId";
            this.grdRecommendations_Sheet1.Columns.Get(12).Visible = false;
            this.grdRecommendations_Sheet1.GroupBarHeight = 20;
            this.grdRecommendations_Sheet1.GroupBarInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRecommendations_Sheet1.GroupBarInfo.Height = 20;
            groupInfo1.BackColor = System.Drawing.SystemColors.ButtonFace;
            groupInfo1.Border = lineBorder1;
            groupInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupInfo1.FooterBorder = lineBorder2;
            groupInfo1.ForeColor = System.Drawing.SystemColors.ControlText;
            groupInfo1.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRecommendations_Sheet1.GroupInfos.AddRange(new FarPoint.Win.Spread.GroupInfo[] {
            groupInfo1});
            this.grdRecommendations_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.grdRecommendations_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdRecommendations_Sheet1.Rows.Get(0).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(1).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(2).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(3).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(4).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(5).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(6).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(7).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(8).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(9).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(10).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(11).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(12).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(13).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(14).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(15).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(16).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(17).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(18).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(19).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(20).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(21).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(22).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(23).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(24).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(25).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(26).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(27).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(28).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(29).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(30).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(31).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(32).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(33).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(34).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(35).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(36).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(37).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(38).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(39).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(40).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(41).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(42).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(43).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(44).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(45).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(46).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(47).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(48).Height = 38F;
            this.grdRecommendations_Sheet1.Rows.Get(49).Height = 38F;
            this.grdRecommendations_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblItemCount
            // 
            this.lblItemCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItemCount.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblItemCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblItemCount.ForeColor = System.Drawing.Color.Black;
            this.lblItemCount.Location = new System.Drawing.Point(13, 672);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(147, 23);
            this.lblItemCount.TabIndex = 5;
            this.lblItemCount.Text = "Included items: 0";
            // 
            // frmAdditionalRecommendations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 767);
            this.Controls.Add(this.lblItemCount);
            this.Controls.Add(this.grdRecommendations);
            this.Controls.Add(this.pnlSelectButtons);
            this.Controls.Add(this.pnlSaveButtons);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdditionalRecommendations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Additional Recommendations";
            this.Load += new System.EventHandler(this.frmAdditionalRecommendations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdRecommendations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecommendations_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Metro.MetroTilePanel pnlSaveButtons;
        private DevComponents.DotNetBar.ItemContainer itmSaveButtons;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnCancel;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnSave;
        private DevComponents.DotNetBar.Metro.MetroTilePanel pnlSelectButtons;
        private DevComponents.DotNetBar.ItemContainer itmSelectButtons;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnSelectAll;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnUnselectAll;
        private FarPoint.Win.Spread.FpSpread grdRecommendations;
        private FarPoint.Win.Spread.SheetView grdRecommendations_Sheet1;
        private DevComponents.DotNetBar.LabelX lblItemCount;
    }
}