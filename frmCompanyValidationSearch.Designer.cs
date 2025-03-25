namespace FieldTool.UI {
    partial class frmCompanyValidationSearch {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompanyValidationSearch));
            this.lstResults = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colZip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colElectricAccountNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGasAccountNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUtility = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProgram = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCompanyId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblMessage = new DevComponents.DotNetBar.LabelX();
            this.metroTilePanel16 = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.itemContainer16 = new DevComponents.DotNetBar.ItemContainer();
            this.btnClear = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnSave = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.SuspendLayout();
            // 
            // lstResults
            // 
            this.lstResults.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lstResults.Border.Class = "ListViewBorder";
            this.lstResults.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstResults.CheckBoxes = true;
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colAddress,
            this.colCity,
            this.colState,
            this.colZip,
            this.colElectricAccountNumber,
            this.colGasAccountNumber,
            this.colUtility,
            this.colProgram,
            this.colId,
            this.colCompanyId});
            this.lstResults.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lstResults.ForeColor = System.Drawing.Color.Black;
            this.lstResults.FullRowSelect = true;
            this.lstResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstResults.HideSelection = false;
            this.lstResults.Location = new System.Drawing.Point(12, 69);
            this.lstResults.MultiSelect = false;
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(1011, 255);
            this.lstResults.TabIndex = 0;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            this.lstResults.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 100;
            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 181;
            // 
            // colCity
            // 
            this.colCity.Text = "City";
            this.colCity.Width = 100;
            // 
            // colState
            // 
            this.colState.Text = "State";
            // 
            // colZip
            // 
            this.colZip.Text = "ZIP";
            // 
            // colElectricAccountNumber
            // 
            this.colElectricAccountNumber.Text = "Electric Acct. Num.";
            this.colElectricAccountNumber.Width = 175;
            // 
            // colGasAccountNumber
            // 
            this.colGasAccountNumber.Text = "Gas Acct.  Num.";
            this.colGasAccountNumber.Width = 150;
            // 
            // colUtility
            // 
            this.colUtility.Text = "Utility";
            this.colUtility.Width = 84;
            // 
            // colProgram
            // 
            this.colProgram.Text = "Program";
            this.colProgram.Width = 98;
            // 
            // colId
            // 
            this.colId.Text = "Id";
            // 
            // colCompanyId
            // 
            this.colCompanyId.Text = "Company Id";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMessage.ForeColor = System.Drawing.Color.Black;
            this.lblMessage.Location = new System.Drawing.Point(12, 12);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(875, 51);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "The following companies were found in Bensight.";
            this.lblMessage.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // metroTilePanel16
            // 
            this.metroTilePanel16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTilePanel16.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.metroTilePanel16.BackgroundStyle.Class = "MetroTilePanel";
            this.metroTilePanel16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroTilePanel16.ContainerControlProcessDialogKey = true;
            this.metroTilePanel16.ForeColor = System.Drawing.Color.Black;
            this.metroTilePanel16.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Right;
            this.metroTilePanel16.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer16});
            this.metroTilePanel16.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.metroTilePanel16.Location = new System.Drawing.Point(893, 339);
            this.metroTilePanel16.Name = "metroTilePanel16";
            this.metroTilePanel16.Size = new System.Drawing.Size(146, 81);
            this.metroTilePanel16.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.metroTilePanel16.TabIndex = 4;
            this.metroTilePanel16.Text = "metroTilePanel16";
            // 
            // itemContainer16
            // 
            // 
            // 
            // 
            this.itemContainer16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer16.ItemSpacing = 20;
            this.itemContainer16.Name = "itemContainer16";
            this.itemContainer16.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnClear,
            this.btnSave});
            // 
            // 
            // 
            this.itemContainer16.TitleStyle.Class = "MetroTileGroupTitle";
            this.itemContainer16.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer16.TitleText = "First";
            this.itemContainer16.TitleVisible = false;
            // 
            // btnClear
            // 
            this.btnClear.ImageIndent = new System.Drawing.Point(0, -4);
            this.btnClear.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClear.Name = "btnClear";
            this.btnClear.Symbol = "";
            this.btnClear.SymbolColor = System.Drawing.Color.Empty;
            this.btnClear.SymbolSize = 25F;
            this.btnClear.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            this.btnClear.TileSize = new System.Drawing.Size(41, 41);
            // 
            // 
            // 
            this.btnClear.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnClear.Tooltip = "Return to the main form without saving a company";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
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
            this.btnSave.Tooltip = "Choose the selected company and return to the main form";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmCompanyValidationSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 431);
            this.Controls.Add(this.metroTilePanel16);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lstResults);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCompanyValidationSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Company Validation";
            this.Load += new System.EventHandler(this.frmCompanyValidationSearch_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx lstResults;
        private DevComponents.DotNetBar.LabelX lblMessage;
        private DevComponents.DotNetBar.Metro.MetroTilePanel metroTilePanel16;
        private DevComponents.DotNetBar.ItemContainer itemContainer16;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnClear;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnSave;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colCompanyId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colCity;
        private System.Windows.Forms.ColumnHeader colState;
        private System.Windows.Forms.ColumnHeader colZip;
        private System.Windows.Forms.ColumnHeader colUtility;
        private System.Windows.Forms.ColumnHeader colProgram;
        private System.Windows.Forms.ColumnHeader colElectricAccountNumber;
        private System.Windows.Forms.ColumnHeader colGasAccountNumber;
    }
}