namespace FieldTool.UI {
    partial class frmValidationTerms {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmValidationTerms));
            this.pnlButtons = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.itmSave = new DevComponents.DotNetBar.ItemContainer();
            this.btnCancel = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.txtElectricAccountNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCompany = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtAddress = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCity = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtZip = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboState = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtProgram = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chbElectricAccountNumber = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbCompanyName = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbAddress = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbCity = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbState = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbZip = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbProgram = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.metroTilePanel1 = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.btnSearch = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.lblTermCount = new DevComponents.DotNetBar.LabelX();
            this.txtCompanyExternalId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCompanyId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lstItemsToValidate = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colToDoElectricAccountNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colToDoGasAccountNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colToDoCompanyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colToDoAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colToDoCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colToDoState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colToDoZip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colToDoUtility = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colToDoProgram = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chbUtility = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtUtility = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chbGasAccountNumber = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtGasAccountNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtons.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pnlButtons.BackgroundStyle.Class = "MetroTilePanel";
            this.pnlButtons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pnlButtons.ContainerControlProcessDialogKey = true;
            this.pnlButtons.ForeColor = System.Drawing.Color.Black;
            this.pnlButtons.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Right;
            this.pnlButtons.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itmSave});
            this.pnlButtons.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.pnlButtons.Location = new System.Drawing.Point(761, 655);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(83, 83);
            this.pnlButtons.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlButtons.TabIndex = 21;
            this.pnlButtons.Text = "metroTilePanel4";
            // 
            // itmSave
            // 
            // 
            // 
            // 
            this.itmSave.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itmSave.ItemSpacing = 30;
            this.itmSave.Name = "itmSave";
            this.itmSave.ResizeItemsToFit = false;
            this.itmSave.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnCancel});
            // 
            // 
            // 
            this.itmSave.TitleStyle.Class = "MetroTileGroupTitle";
            this.itmSave.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itmSave.TitleText = "First";
            this.itmSave.TitleVisible = false;
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
            this.btnCancel.Tooltip = "Return to the main form without applying validation to any companies";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(193))))));
            // 
            // txtElectricAccountNumber
            // 
            this.txtElectricAccountNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtElectricAccountNumber.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtElectricAccountNumber.Border.Class = "TextBoxBorder";
            this.txtElectricAccountNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtElectricAccountNumber.ForeColor = System.Drawing.Color.Black;
            this.txtElectricAccountNumber.Location = new System.Drawing.Point(200, 252);
            this.txtElectricAccountNumber.Name = "txtElectricAccountNumber";
            this.txtElectricAccountNumber.PreventEnterBeep = true;
            this.txtElectricAccountNumber.Size = new System.Drawing.Size(481, 22);
            this.txtElectricAccountNumber.TabIndex = 3;
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCompany.Border.Class = "TextBoxBorder";
            this.txtCompany.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCompany.ForeColor = System.Drawing.Color.Black;
            this.txtCompany.Location = new System.Drawing.Point(200, 346);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.PreventEnterBeep = true;
            this.txtCompany.Size = new System.Drawing.Size(481, 22);
            this.txtCompany.TabIndex = 8;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtAddress.Border.Class = "TextBoxBorder";
            this.txtAddress.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAddress.ForeColor = System.Drawing.Color.Black;
            this.txtAddress.Location = new System.Drawing.Point(200, 393);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PreventEnterBeep = true;
            this.txtAddress.Size = new System.Drawing.Size(481, 22);
            this.txtAddress.TabIndex = 10;
            // 
            // txtCity
            // 
            this.txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCity.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCity.Border.Class = "TextBoxBorder";
            this.txtCity.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCity.ForeColor = System.Drawing.Color.Black;
            this.txtCity.Location = new System.Drawing.Point(200, 440);
            this.txtCity.Name = "txtCity";
            this.txtCity.PreventEnterBeep = true;
            this.txtCity.Size = new System.Drawing.Size(481, 22);
            this.txtCity.TabIndex = 12;
            // 
            // txtZip
            // 
            this.txtZip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZip.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtZip.Border.Class = "TextBoxBorder";
            this.txtZip.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtZip.ForeColor = System.Drawing.Color.Black;
            this.txtZip.Location = new System.Drawing.Point(200, 534);
            this.txtZip.Name = "txtZip";
            this.txtZip.PreventEnterBeep = true;
            this.txtZip.Size = new System.Drawing.Size(481, 22);
            this.txtZip.TabIndex = 16;
            // 
            // cboState
            // 
            this.cboState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboState.DisplayMember = "Text";
            this.cboState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboState.ForeColor = System.Drawing.Color.Black;
            this.cboState.FormattingEnabled = true;
            this.cboState.ItemHeight = 16;
            this.cboState.Location = new System.Drawing.Point(200, 487);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(481, 22);
            this.cboState.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboState.TabIndex = 14;
            // 
            // txtProgram
            // 
            this.txtProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProgram.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtProgram.Border.Class = "TextBoxBorder";
            this.txtProgram.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtProgram.ForeColor = System.Drawing.Color.Black;
            this.txtProgram.Location = new System.Drawing.Point(200, 628);
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.PreventEnterBeep = true;
            this.txtProgram.Size = new System.Drawing.Size(481, 22);
            this.txtProgram.TabIndex = 20;
            // 
            // chbElectricAccountNumber
            // 
            this.chbElectricAccountNumber.AutoSize = true;
            this.chbElectricAccountNumber.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbElectricAccountNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbElectricAccountNumber.BackgroundStyle.PaddingRight = 1;
            this.chbElectricAccountNumber.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbElectricAccountNumber.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbElectricAccountNumber.ForeColor = System.Drawing.Color.Black;
            this.chbElectricAccountNumber.Location = new System.Drawing.Point(28, 251);
            this.chbElectricAccountNumber.Name = "chbElectricAccountNumber";
            this.chbElectricAccountNumber.Size = new System.Drawing.Size(152, 24);
            this.chbElectricAccountNumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbElectricAccountNumber.TabIndex = 2;
            this.chbElectricAccountNumber.Text = "Electric Account Number";
            this.chbElectricAccountNumber.CheckedChanged += new System.EventHandler(this.chbElectricAccountNumber_CheckedChanged);
            // 
            // chbCompanyName
            // 
            this.chbCompanyName.AutoSize = true;
            this.chbCompanyName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbCompanyName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbCompanyName.BackgroundStyle.PaddingRight = 1;
            this.chbCompanyName.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbCompanyName.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbCompanyName.ForeColor = System.Drawing.Color.Black;
            this.chbCompanyName.Location = new System.Drawing.Point(70, 345);
            this.chbCompanyName.Name = "chbCompanyName";
            this.chbCompanyName.Size = new System.Drawing.Size(110, 24);
            this.chbCompanyName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbCompanyName.TabIndex = 7;
            this.chbCompanyName.Text = "Company Name";
            // 
            // chbAddress
            // 
            this.chbAddress.AutoSize = true;
            this.chbAddress.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbAddress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbAddress.BackgroundStyle.PaddingRight = 1;
            this.chbAddress.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbAddress.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbAddress.ForeColor = System.Drawing.Color.Black;
            this.chbAddress.Location = new System.Drawing.Point(110, 392);
            this.chbAddress.Name = "chbAddress";
            this.chbAddress.Size = new System.Drawing.Size(70, 24);
            this.chbAddress.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbAddress.TabIndex = 9;
            this.chbAddress.Text = "Address";
            // 
            // chbCity
            // 
            this.chbCity.AutoSize = true;
            this.chbCity.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbCity.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbCity.BackgroundStyle.PaddingRight = 1;
            this.chbCity.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbCity.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbCity.ForeColor = System.Drawing.Color.Black;
            this.chbCity.Location = new System.Drawing.Point(130, 439);
            this.chbCity.Name = "chbCity";
            this.chbCity.Size = new System.Drawing.Size(50, 24);
            this.chbCity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbCity.TabIndex = 11;
            this.chbCity.Text = "City";
            // 
            // chbState
            // 
            this.chbState.AutoSize = true;
            this.chbState.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbState.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbState.BackgroundStyle.PaddingRight = 1;
            this.chbState.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbState.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbState.ForeColor = System.Drawing.Color.Black;
            this.chbState.Location = new System.Drawing.Point(124, 486);
            this.chbState.Name = "chbState";
            this.chbState.Size = new System.Drawing.Size(56, 24);
            this.chbState.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbState.TabIndex = 13;
            this.chbState.Text = "State";
            // 
            // chbZip
            // 
            this.chbZip.AutoSize = true;
            this.chbZip.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbZip.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbZip.BackgroundStyle.PaddingRight = 1;
            this.chbZip.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbZip.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbZip.ForeColor = System.Drawing.Color.Black;
            this.chbZip.Location = new System.Drawing.Point(105, 533);
            this.chbZip.Name = "chbZip";
            this.chbZip.Size = new System.Drawing.Size(75, 24);
            this.chbZip.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbZip.TabIndex = 15;
            this.chbZip.Text = "ZIP Code";
            // 
            // chbProgram
            // 
            this.chbProgram.AutoSize = true;
            this.chbProgram.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbProgram.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbProgram.BackgroundStyle.PaddingRight = 1;
            this.chbProgram.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbProgram.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbProgram.ForeColor = System.Drawing.Color.Black;
            this.chbProgram.Location = new System.Drawing.Point(107, 627);
            this.chbProgram.Name = "chbProgram";
            this.chbProgram.Size = new System.Drawing.Size(73, 24);
            this.chbProgram.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbProgram.TabIndex = 19;
            this.chbProgram.Text = "Program";
            // 
            // metroTilePanel1
            // 
            this.metroTilePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTilePanel1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.metroTilePanel1.BackgroundStyle.Class = "MetroTilePanel";
            this.metroTilePanel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroTilePanel1.ContainerControlProcessDialogKey = true;
            this.metroTilePanel1.ForeColor = System.Drawing.Color.Black;
            this.metroTilePanel1.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Right;
            this.metroTilePanel1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.metroTilePanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.metroTilePanel1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.metroTilePanel1.Location = new System.Drawing.Point(682, 236);
            this.metroTilePanel1.Name = "metroTilePanel1";
            this.metroTilePanel1.Size = new System.Drawing.Size(102, 93);
            this.metroTilePanel1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.metroTilePanel1.TabIndex = 4;
            this.metroTilePanel1.Text = "metroTilePanel4";
            // 
            // itemContainer1
            // 
            // 
            // 
            // 
            this.itemContainer1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer1.ItemSpacing = 30;
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.ResizeItemsToFit = false;
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSearch});
            // 
            // 
            // 
            this.itemContainer1.TitleStyle.Class = "MetroTileGroupTitle";
            this.itemContainer1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer1.TitleText = "First";
            this.itemContainer1.TitleVisible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.ImageIndent = new System.Drawing.Point(0, -4);
            this.btnSearch.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Symbol = "";
            this.btnSearch.SymbolColor = System.Drawing.Color.Empty;
            this.btnSearch.SymbolSize = 25F;
            this.btnSearch.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            this.btnSearch.TileSize = new System.Drawing.Size(41, 41);
            // 
            // 
            // 
            this.btnSearch.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnSearch.Tooltip = "Use the specified terms to search for related companies";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblTermCount
            // 
            this.lblTermCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTermCount.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblTermCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTermCount.ForeColor = System.Drawing.Color.Black;
            this.lblTermCount.Location = new System.Drawing.Point(662, 220);
            this.lblTermCount.Name = "lblTermCount";
            this.lblTermCount.Size = new System.Drawing.Size(182, 23);
            this.lblTermCount.TabIndex = 1;
            this.lblTermCount.Text = "labelX1";
            this.lblTermCount.TextAlignment = System.Drawing.StringAlignment.Far;
            this.lblTermCount.UseMnemonic = false;
            // 
            // txtCompanyExternalId
            // 
            this.txtCompanyExternalId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCompanyExternalId.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCompanyExternalId.Border.Class = "TextBoxBorder";
            this.txtCompanyExternalId.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCompanyExternalId.ForeColor = System.Drawing.Color.Black;
            this.txtCompanyExternalId.Location = new System.Drawing.Point(705, 391);
            this.txtCompanyExternalId.Name = "txtCompanyExternalId";
            this.txtCompanyExternalId.PreventEnterBeep = true;
            this.txtCompanyExternalId.Size = new System.Drawing.Size(114, 22);
            this.txtCompanyExternalId.TabIndex = 23;
            this.txtCompanyExternalId.Visible = false;
            // 
            // txtCompanyId
            // 
            this.txtCompanyId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCompanyId.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCompanyId.Border.Class = "TextBoxBorder";
            this.txtCompanyId.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCompanyId.ForeColor = System.Drawing.Color.Black;
            this.txtCompanyId.Location = new System.Drawing.Point(705, 363);
            this.txtCompanyId.Name = "txtCompanyId";
            this.txtCompanyId.PreventEnterBeep = true;
            this.txtCompanyId.Size = new System.Drawing.Size(114, 22);
            this.txtCompanyId.TabIndex = 22;
            this.txtCompanyId.Visible = false;
            // 
            // lstItemsToValidate
            // 
            this.lstItemsToValidate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItemsToValidate.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lstItemsToValidate.Border.Class = "ListViewBorder";
            this.lstItemsToValidate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstItemsToValidate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colToDoElectricAccountNumber,
            this.colToDoGasAccountNumber,
            this.colToDoCompanyName,
            this.colToDoAddress,
            this.colToDoCity,
            this.colToDoState,
            this.colToDoZip,
            this.colToDoUtility,
            this.colToDoProgram});
            this.lstItemsToValidate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItemsToValidate.ForeColor = System.Drawing.Color.Black;
            this.lstItemsToValidate.FullRowSelect = true;
            this.lstItemsToValidate.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstItemsToValidate.HideSelection = false;
            this.lstItemsToValidate.Location = new System.Drawing.Point(12, 12);
            this.lstItemsToValidate.MultiSelect = false;
            this.lstItemsToValidate.Name = "lstItemsToValidate";
            this.lstItemsToValidate.Size = new System.Drawing.Size(832, 202);
            this.lstItemsToValidate.TabIndex = 0;
            this.lstItemsToValidate.UseCompatibleStateImageBehavior = false;
            this.lstItemsToValidate.View = System.Windows.Forms.View.Details;
            this.lstItemsToValidate.SelectedIndexChanged += new System.EventHandler(this.lstItemsToValidate_SelectedIndexChanged);
            // 
            // colToDoElectricAccountNumber
            // 
            this.colToDoElectricAccountNumber.Text = "Electric Account Number";
            // 
            // colToDoGasAccountNumber
            // 
            this.colToDoGasAccountNumber.Text = "Gas Account Number";
            // 
            // colToDoCompanyName
            // 
            this.colToDoCompanyName.Text = "Company Name";
            // 
            // colToDoAddress
            // 
            this.colToDoAddress.Text = "Address";
            // 
            // colToDoCity
            // 
            this.colToDoCity.Text = "City";
            // 
            // colToDoState
            // 
            this.colToDoState.Text = "State";
            // 
            // colToDoZip
            // 
            this.colToDoZip.Text = "Zip";
            // 
            // colToDoUtility
            // 
            this.colToDoUtility.Text = "Utility";
            // 
            // colToDoProgram
            // 
            this.colToDoProgram.Text = "Program";
            // 
            // chbUtility
            // 
            this.chbUtility.AutoSize = true;
            this.chbUtility.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbUtility.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbUtility.BackgroundStyle.PaddingRight = 1;
            this.chbUtility.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbUtility.Checked = true;
            this.chbUtility.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbUtility.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbUtility.CheckValue = "Y";
            this.chbUtility.ForeColor = System.Drawing.Color.Black;
            this.chbUtility.Location = new System.Drawing.Point(120, 580);
            this.chbUtility.Name = "chbUtility";
            this.chbUtility.Size = new System.Drawing.Size(60, 24);
            this.chbUtility.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbUtility.TabIndex = 17;
            this.chbUtility.Text = "Utility";
            this.chbUtility.CheckedChanging += new DevComponents.DotNetBar.Controls.CheckBoxXChangeEventHandler(this.chbUtility_CheckedChanging);
            // 
            // txtUtility
            // 
            this.txtUtility.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUtility.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtUtility.Border.Class = "TextBoxBorder";
            this.txtUtility.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUtility.ForeColor = System.Drawing.Color.Black;
            this.txtUtility.Location = new System.Drawing.Point(200, 581);
            this.txtUtility.Name = "txtUtility";
            this.txtUtility.PreventEnterBeep = true;
            this.txtUtility.Size = new System.Drawing.Size(481, 22);
            this.txtUtility.TabIndex = 18;
            // 
            // txtId
            // 
            this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtId.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtId.Border.Class = "TextBoxBorder";
            this.txtId.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(705, 335);
            this.txtId.Name = "txtId";
            this.txtId.PreventEnterBeep = true;
            this.txtId.Size = new System.Drawing.Size(114, 22);
            this.txtId.TabIndex = 24;
            this.txtId.Visible = false;
            // 
            // chbGasAccountNumber
            // 
            this.chbGasAccountNumber.AutoSize = true;
            this.chbGasAccountNumber.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbGasAccountNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbGasAccountNumber.BackgroundStyle.PaddingRight = 1;
            this.chbGasAccountNumber.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.chbGasAccountNumber.CheckSignSize = new System.Drawing.Size(22, 22);
            this.chbGasAccountNumber.ForeColor = System.Drawing.Color.Black;
            this.chbGasAccountNumber.Location = new System.Drawing.Point(45, 298);
            this.chbGasAccountNumber.Name = "chbGasAccountNumber";
            this.chbGasAccountNumber.Size = new System.Drawing.Size(135, 24);
            this.chbGasAccountNumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbGasAccountNumber.TabIndex = 5;
            this.chbGasAccountNumber.Text = "Gas Account Number";
            this.chbGasAccountNumber.CheckedChanged += new System.EventHandler(this.chbGasAccountNumber_CheckedChanged);
            // 
            // txtGasAccountNumber
            // 
            this.txtGasAccountNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGasAccountNumber.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtGasAccountNumber.Border.Class = "TextBoxBorder";
            this.txtGasAccountNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGasAccountNumber.ForeColor = System.Drawing.Color.Black;
            this.txtGasAccountNumber.Location = new System.Drawing.Point(200, 299);
            this.txtGasAccountNumber.Name = "txtGasAccountNumber";
            this.txtGasAccountNumber.PreventEnterBeep = true;
            this.txtGasAccountNumber.Size = new System.Drawing.Size(481, 22);
            this.txtGasAccountNumber.TabIndex = 6;
            // 
            // frmValidationTerms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 750);
            this.Controls.Add(this.chbGasAccountNumber);
            this.Controls.Add(this.txtGasAccountNumber);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.chbUtility);
            this.Controls.Add(this.txtUtility);
            this.Controls.Add(this.lstItemsToValidate);
            this.Controls.Add(this.txtCompanyId);
            this.Controls.Add(this.txtCompanyExternalId);
            this.Controls.Add(this.lblTermCount);
            this.Controls.Add(this.chbProgram);
            this.Controls.Add(this.chbZip);
            this.Controls.Add(this.chbState);
            this.Controls.Add(this.chbCity);
            this.Controls.Add(this.chbAddress);
            this.Controls.Add(this.chbCompanyName);
            this.Controls.Add(this.chbElectricAccountNumber);
            this.Controls.Add(this.txtProgram);
            this.Controls.Add(this.cboState);
            this.Controls.Add(this.txtZip);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.txtElectricAccountNumber);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.metroTilePanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmValidationTerms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Audit Validation Terms";
            this.Load += new System.EventHandler(this.frmValidationTerms_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmValidationTerms_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Metro.MetroTilePanel pnlButtons;
        private DevComponents.DotNetBar.ItemContainer itmSave;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnCancel;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtElectricAccountNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCompany;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAddress;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCity;
        private DevComponents.DotNetBar.Controls.TextBoxX txtZip;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboState;
        private DevComponents.DotNetBar.Controls.TextBoxX txtProgram;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbElectricAccountNumber;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbCompanyName;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbAddress;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbCity;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbState;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbZip;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbProgram;
        private DevComponents.DotNetBar.Metro.MetroTilePanel metroTilePanel1;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnSearch;
        private DevComponents.DotNetBar.LabelX lblTermCount;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCompanyExternalId;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCompanyId;
        private DevComponents.DotNetBar.Controls.ListViewEx lstItemsToValidate;
        private System.Windows.Forms.ColumnHeader colToDoElectricAccountNumber;
        private System.Windows.Forms.ColumnHeader colToDoCompanyName;
        private System.Windows.Forms.ColumnHeader colToDoAddress;
        private System.Windows.Forms.ColumnHeader colToDoCity;
        private System.Windows.Forms.ColumnHeader colToDoState;
        private System.Windows.Forms.ColumnHeader colToDoZip;
        private System.Windows.Forms.ColumnHeader colToDoUtility;
        private System.Windows.Forms.ColumnHeader colToDoProgram;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbUtility;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUtility;
        private System.Windows.Forms.ColumnHeader colToDoGasAccountNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtId;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbGasAccountNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGasAccountNumber;
    }
}