namespace FieldTool.UI {
    partial class frmValidationResults {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmValidationResults));
            this.lstResults = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colBillingCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBillingPostalCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBillingState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBillingStreet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBusinessType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCustomerClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colElectricAccountNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colElectricAnnualBill = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colElectricMeterNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colElectricRateCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colElectricUtilityType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFlat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGasAccountNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGasPremiseNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGasUtilityName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGasMeterNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGasServiceClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGasRateCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProgram = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPhone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEligibilityStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEligibilityReason = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExternalId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.pnlButtons = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.itmSave = new DevComponents.DotNetBar.ItemContainer();
            this.btnCancel = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnSave = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.lblCount = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // lstResults
            // 
            this.lstResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResults.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lstResults.Border.Class = "ListViewBorder";
            this.lstResults.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colBillingCity,
            this.colBillingPostalCode,
            this.colBillingState,
            this.colBillingStreet,
            this.colBusinessType,
            this.colCustomerClass,
            this.colElectricAccountNumber,
            this.colElectricAnnualBill,
            this.colElectricMeterNumber,
            this.colElectricRateCode,
            this.colElectricUtilityType,
            this.colFlat,
            this.colGasAccountNumber,
            this.colGasPremiseNumber,
            this.colGasUtilityName,
            this.colGasMeterNumber,
            this.colGasServiceClass,
            this.colGasRateCode,
            this.colProgram,
            this.colName,
            this.colEmail,
            this.colPhone,
            this.colEligibilityStatus,
            this.colEligibilityReason,
            this.colId,
            this.colExternalId});
            this.lstResults.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstResults.ForeColor = System.Drawing.Color.Black;
            this.lstResults.FullRowSelect = true;
            this.lstResults.HideSelection = false;
            this.lstResults.Location = new System.Drawing.Point(12, 53);
            this.lstResults.MultiSelect = false;
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(993, 421);
            this.lstResults.TabIndex = 1;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            this.lstResults.View = System.Windows.Forms.View.Details;
            this.lstResults.DoubleClick += new System.EventHandler(this.lstResults_DoubleClick);
            // 
            // colBillingCity
            // 
            this.colBillingCity.Text = "Billing City";
            // 
            // colBillingPostalCode
            // 
            this.colBillingPostalCode.Text = "Billing Postal Code";
            // 
            // colBillingState
            // 
            this.colBillingState.Text = "Billing State";
            // 
            // colBillingStreet
            // 
            this.colBillingStreet.Text = "Billing Street";
            // 
            // colBusinessType
            // 
            this.colBusinessType.Text = "Business Type";
            // 
            // colCustomerClass
            // 
            this.colCustomerClass.Text = "Customer Class";
            // 
            // colElectricAccountNumber
            // 
            this.colElectricAccountNumber.Text = "Electric Account Number";
            // 
            // colElectricAnnualBill
            // 
            this.colElectricAnnualBill.Text = "Electric Annual Bill";
            // 
            // colElectricMeterNumber
            // 
            this.colElectricMeterNumber.Text = "Electric Meter Number";
            // 
            // colElectricRateCode
            // 
            this.colElectricRateCode.Text = "Electric Rate Code";
            // 
            // colElectricUtilityType
            // 
            this.colElectricUtilityType.Text = "Electric Utility Type";
            // 
            // colFlat
            // 
            this.colFlat.Text = "Flat";
            // 
            // colGasAccountNumber
            // 
            this.colGasAccountNumber.Text = "Gas Account Number";
            // 
            // colGasPremiseNumber
            // 
            this.colGasPremiseNumber.Text = "Gas Premise Number";
            // 
            // colGasUtilityName
            // 
            this.colGasUtilityName.Text = "Gas Utility Name";
            // 
            // colGasMeterNumber
            // 
            this.colGasMeterNumber.Text = "Gas Meter Number";
            // 
            // colGasServiceClass
            // 
            this.colGasServiceClass.Text = "Gas Service Class";
            // 
            // colGasRateCode
            // 
            this.colGasRateCode.Text = "Gas Rate Code";
            // 
            // colProgram
            // 
            this.colProgram.Text = "Program";
            // 
            // colName
            // 
            this.colName.Text = "Name";
            // 
            // colEmail
            // 
            this.colEmail.Text = "Email";
            // 
            // colPhone
            // 
            this.colPhone.Text = "Phone";
            // 
            // colEligibilityStatus
            // 
            this.colEligibilityStatus.Tag = "";
            this.colEligibilityStatus.Text = "Eligibility Status";
            // 
            // colEligibilityReason
            // 
            this.colEligibilityReason.Tag = "";
            this.colEligibilityReason.Text = "Eligibility Reason";
            // 
            // colId
            // 
            this.colId.Tag = "Hide";
            this.colId.Text = "Id";
            // 
            // colExternalId
            // 
            this.colExternalId.Tag = "Hide";
            this.colExternalId.Text = "External ID";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(12, 24);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(993, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Based on the specified search terms, the following items were found.  Select the " +
    "item that represents the search you provided and press the save button to valida" +
    "te..";
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
            this.pnlButtons.Location = new System.Drawing.Point(854, 521);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(151, 83);
            this.pnlButtons.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlButtons.TabIndex = 3;
            this.pnlButtons.Text = "c";
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
            this.btnCancel,
            this.btnSave});
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
            this.btnCancel.Tooltip = "Return to the Validation Terms form without selecting a company";
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
            this.btnSave.Tooltip = "Apply validation of the selected row to the active search and return to the Valid" +
    "ation Terms form";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(193))))));
            // 
            // lblCount
            // 
            // 
            // 
            // 
            this.lblCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCount.Location = new System.Drawing.Point(774, 480);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(231, 23);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "labelX2";
            this.lblCount.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // frmValidationResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 616);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lstResults);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmValidationResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Company Search Results";
            this.Load += new System.EventHandler(this.frmValidationResults_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx lstResults;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Metro.MetroTilePanel pnlButtons;
        private DevComponents.DotNetBar.ItemContainer itmSave;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnCancel;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnSave;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colBillingCity;
        private System.Windows.Forms.ColumnHeader colBillingPostalCode;
        private System.Windows.Forms.ColumnHeader colBillingState;
        private System.Windows.Forms.ColumnHeader colBillingStreet;
        private System.Windows.Forms.ColumnHeader colBusinessType;
        private System.Windows.Forms.ColumnHeader colCustomerClass;
        private System.Windows.Forms.ColumnHeader colElectricAccountNumber;
        private System.Windows.Forms.ColumnHeader colElectricAnnualBill;
        private System.Windows.Forms.ColumnHeader colElectricMeterNumber;
        private System.Windows.Forms.ColumnHeader colElectricRateCode;
        private System.Windows.Forms.ColumnHeader colElectricUtilityType;
        private System.Windows.Forms.ColumnHeader colFlat;
        private System.Windows.Forms.ColumnHeader colGasAccountNumber;
        private System.Windows.Forms.ColumnHeader colGasPremiseNumber;
        private System.Windows.Forms.ColumnHeader colGasUtilityName;
        private System.Windows.Forms.ColumnHeader colGasMeterNumber;
        private System.Windows.Forms.ColumnHeader colGasServiceClass;
        private System.Windows.Forms.ColumnHeader colGasRateCode;
        private System.Windows.Forms.ColumnHeader colProgram;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colEmail;
        private System.Windows.Forms.ColumnHeader colPhone;
        private System.Windows.Forms.ColumnHeader colExternalId;
        private System.Windows.Forms.ColumnHeader colEligibilityStatus;
        private System.Windows.Forms.ColumnHeader colEligibilityReason;
        private DevComponents.DotNetBar.LabelX lblCount;
    }
}