namespace FieldTool.UI {
    partial class frmHelp {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHelp));
            this.pslMain = new DevComponents.DotNetBar.Controls.PageSlider();
            this.pnlSendDataFile = new DevComponents.DotNetBar.Controls.PageSliderPage();
            this.btnDataFileRecovery = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewDocument = new DevComponents.DotNetBar.ButtonX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lblDocumentation = new DevComponents.DotNetBar.LabelX();
            this.lblDocumentationInfo = new DevComponents.DotNetBar.LabelX();
            this.cboDocuments = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblClipboardDocs = new DevComponents.DotNetBar.LabelX();
            this.chbLogTrace = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSendDataInstallFolder = new DevComponents.DotNetBar.ButtonX();
            this.btnSendDataFilePaths = new DevComponents.DotNetBar.ButtonX();
            this.lblSendDataInternetMessage = new DevComponents.DotNetBar.LabelX();
            this.txtSendDataLocalPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboSendDataVia = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chbSendDataCompress = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblSendDataFile = new DevComponents.DotNetBar.LabelX();
            this.btnSendDataSend = new DevComponents.DotNetBar.ButtonX();
            this.lblSendDataNote = new DevComponents.DotNetBar.LabelX();
            this.lblSendDataVia = new DevComponents.DotNetBar.LabelX();
            this.cboSendDataUser = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblSendDataUser = new DevComponents.DotNetBar.LabelX();
            this.lblSendDataInfo = new DevComponents.DotNetBar.LabelX();
            this.pnlRestore = new DevComponents.DotNetBar.Controls.PageSliderPage();
            this.lblRestoreNote = new DevComponents.DotNetBar.LabelX();
            this.lstRestoreList = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colRestoreItems = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRestoreSelect = new DevComponents.DotNetBar.ButtonX();
            this.btnRestoreCancel = new DevComponents.DotNetBar.ButtonX();
            this.chbRestoreBackupCurrent = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboRestoreUser = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblRestoreUser = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.pnlDebug = new DevComponents.DotNetBar.Controls.PageSliderPage();
            this.btnDebugClear = new DevComponents.DotNetBar.ButtonX();
            this.chbDebugCopyToClipboard = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnDebugLoad = new DevComponents.DotNetBar.ButtonX();
            this.txtDebugData = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager();
            this.pslMain.SuspendLayout();
            this.pnlSendDataFile.SuspendLayout();
            this.pnlRestore.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // pslMain
            // 
            this.pslMain.AnimationTime = 250;
            this.pslMain.BackColor = System.Drawing.Color.White;
            this.pslMain.Controls.Add(this.pnlSendDataFile);
            this.pslMain.Controls.Add(this.pnlRestore);
            this.pslMain.Controls.Add(this.pnlDebug);
            this.pslMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pslMain.ForeColor = System.Drawing.Color.Black;
            this.pslMain.IndicatorVisible = false;
            this.pslMain.Location = new System.Drawing.Point(0, 0);
            this.pslMain.Name = "pslMain";
            this.pslMain.SelectedPage = this.pnlSendDataFile;
            this.pslMain.Size = new System.Drawing.Size(885, 632);
            this.pslMain.TabIndex = 0;
            this.pslMain.Text = "pageSlider1";
            // 
            // pnlSendDataFile
            // 
            this.pnlSendDataFile.BackColor = System.Drawing.Color.White;
            this.pnlSendDataFile.Controls.Add(this.btnDataFileRecovery);
            this.pnlSendDataFile.Controls.Add(this.btnViewDocument);
            this.pnlSendDataFile.Controls.Add(this.labelX6);
            this.pnlSendDataFile.Controls.Add(this.lblDocumentation);
            this.pnlSendDataFile.Controls.Add(this.lblDocumentationInfo);
            this.pnlSendDataFile.Controls.Add(this.cboDocuments);
            this.pnlSendDataFile.Controls.Add(this.lblClipboardDocs);
            this.pnlSendDataFile.Controls.Add(this.chbLogTrace);
            this.pnlSendDataFile.Controls.Add(this.btnCancel);
            this.pnlSendDataFile.Controls.Add(this.btnSendDataInstallFolder);
            this.pnlSendDataFile.Controls.Add(this.btnSendDataFilePaths);
            this.pnlSendDataFile.Controls.Add(this.lblSendDataInternetMessage);
            this.pnlSendDataFile.Controls.Add(this.txtSendDataLocalPath);
            this.pnlSendDataFile.Controls.Add(this.cboSendDataVia);
            this.pnlSendDataFile.Controls.Add(this.chbSendDataCompress);
            this.pnlSendDataFile.Controls.Add(this.lblSendDataFile);
            this.pnlSendDataFile.Controls.Add(this.btnSendDataSend);
            this.pnlSendDataFile.Controls.Add(this.lblSendDataNote);
            this.pnlSendDataFile.Controls.Add(this.lblSendDataVia);
            this.pnlSendDataFile.Controls.Add(this.cboSendDataUser);
            this.pnlSendDataFile.Controls.Add(this.lblSendDataUser);
            this.pnlSendDataFile.Controls.Add(this.lblSendDataInfo);
            this.pnlSendDataFile.ForeColor = System.Drawing.Color.Black;
            this.pnlSendDataFile.Location = new System.Drawing.Point(4, 4);
            this.pnlSendDataFile.Name = "pnlSendDataFile";
            this.pnlSendDataFile.Size = new System.Drawing.Size(779, 624);
            this.pnlSendDataFile.TabIndex = 0;
            // 
            // btnDataFileRecovery
            // 
            this.btnDataFileRecovery.AllowFocus = false;
            this.btnDataFileRecovery.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataFileRecovery.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnDataFileRecovery.Appearance.Options.UseFont = true;
            this.btnDataFileRecovery.Appearance.Options.UseForeColor = true;
            this.btnDataFileRecovery.Image = ((System.Drawing.Image)(resources.GetObject("btnDataFileRecovery.Image")));
            this.btnDataFileRecovery.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnDataFileRecovery.ImageToTextIndent = 10;
            this.btnDataFileRecovery.Location = new System.Drawing.Point(529, 50);
            this.btnDataFileRecovery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDataFileRecovery.Name = "btnDataFileRecovery";
            this.btnDataFileRecovery.Size = new System.Drawing.Size(228, 65);
            this.btnDataFileRecovery.TabIndex = 38;
            this.btnDataFileRecovery.Text = "Data File Recovery";
            this.btnDataFileRecovery.Click += new System.EventHandler(this.btnDataFileRecovery_Click);
            // 
            // btnViewDocument
            // 
            this.btnViewDocument.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewDocument.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnViewDocument.Location = new System.Drawing.Point(357, 80);
            this.btnViewDocument.Name = "btnViewDocument";
            this.btnViewDocument.Size = new System.Drawing.Size(40, 40);
            this.btnViewDocument.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnViewDocument.Symbol = "";
            this.btnViewDocument.SymbolSize = 25F;
            this.btnViewDocument.TabIndex = 20;
            this.btnViewDocument.Tooltip = "View selected document";
            this.btnViewDocument.Click += new System.EventHandler(this.btnViewDocument_Click);
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.labelX6.BackgroundStyle.BorderBottomColor = System.Drawing.Color.Black;
            this.labelX6.BackgroundStyle.BorderBottomWidth = 1;
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.ForeColor = System.Drawing.Color.Black;
            this.labelX6.Location = new System.Drawing.Point(16, 134);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(736, 10);
            this.labelX6.TabIndex = 19;
            // 
            // lblDocumentation
            // 
            this.lblDocumentation.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblDocumentation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDocumentation.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentation.ForeColor = System.Drawing.Color.Black;
            this.lblDocumentation.Location = new System.Drawing.Point(13, 15);
            this.lblDocumentation.Name = "lblDocumentation";
            this.lblDocumentation.Size = new System.Drawing.Size(259, 23);
            this.lblDocumentation.TabIndex = 17;
            this.lblDocumentation.Text = "Clipboard Documentation";
            // 
            // lblDocumentationInfo
            // 
            this.lblDocumentationInfo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblDocumentationInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDocumentationInfo.ForeColor = System.Drawing.Color.Black;
            this.lblDocumentationInfo.Location = new System.Drawing.Point(13, 34);
            this.lblDocumentationInfo.Name = "lblDocumentationInfo";
            this.lblDocumentationInfo.Size = new System.Drawing.Size(453, 23);
            this.lblDocumentationInfo.TabIndex = 18;
            this.lblDocumentationInfo.Text = "View the available Clipboard documentation and FAQs.";
            // 
            // cboDocuments
            // 
            this.cboDocuments.DisplayMember = "Text";
            this.cboDocuments.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDocuments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocuments.ForeColor = System.Drawing.Color.Black;
            this.cboDocuments.FormattingEnabled = true;
            this.cboDocuments.ItemHeight = 20;
            this.cboDocuments.Location = new System.Drawing.Point(53, 89);
            this.cboDocuments.Name = "cboDocuments";
            this.cboDocuments.Size = new System.Drawing.Size(281, 26);
            this.cboDocuments.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDocuments.TabIndex = 16;
            // 
            // lblClipboardDocs
            // 
            this.lblClipboardDocs.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblClipboardDocs.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblClipboardDocs.ForeColor = System.Drawing.Color.Black;
            this.lblClipboardDocs.Location = new System.Drawing.Point(53, 63);
            this.lblClipboardDocs.Name = "lblClipboardDocs";
            this.lblClipboardDocs.Size = new System.Drawing.Size(117, 23);
            this.lblClipboardDocs.TabIndex = 15;
            this.lblClipboardDocs.Text = "&Documents:";
            // 
            // chbLogTrace
            // 
            this.chbLogTrace.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbLogTrace.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbLogTrace.ForeColor = System.Drawing.Color.Black;
            this.chbLogTrace.Location = new System.Drawing.Point(386, 558);
            this.chbLogTrace.Name = "chbLogTrace";
            this.chbLogTrace.Size = new System.Drawing.Size(80, 45);
            this.chbLogTrace.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbLogTrace.TabIndex = 13;
            this.chbLogTrace.Text = "Log Trace";
            this.chbLogTrace.CheckedChanged += new System.EventHandler(this.chbLogTrace_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(717, 563);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(40, 40);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.Symbol = "";
            this.btnCancel.SymbolSize = 25F;
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Tooltip = "Cancel any changes and close this form";
            // 
            // btnSendDataInstallFolder
            // 
            this.btnSendDataInstallFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSendDataInstallFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendDataInstallFolder.Location = new System.Drawing.Point(53, 563);
            this.btnSendDataInstallFolder.Name = "btnSendDataInstallFolder";
            this.btnSendDataInstallFolder.Size = new System.Drawing.Size(40, 40);
            this.btnSendDataInstallFolder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSendDataInstallFolder.Symbol = "";
            this.btnSendDataInstallFolder.SymbolSize = 25F;
            this.btnSendDataInstallFolder.TabIndex = 11;
            this.btnSendDataInstallFolder.Tooltip = "Open Windows Explorer to the current installation folder and copy the path to you" +
    "r clipboard";
            this.btnSendDataInstallFolder.Click += new System.EventHandler(this.btnSendDataInstallFolder_Click);
            // 
            // btnSendDataFilePaths
            // 
            this.btnSendDataFilePaths.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSendDataFilePaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendDataFilePaths.Location = new System.Drawing.Point(130, 563);
            this.btnSendDataFilePaths.Name = "btnSendDataFilePaths";
            this.btnSendDataFilePaths.Size = new System.Drawing.Size(40, 40);
            this.btnSendDataFilePaths.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSendDataFilePaths.Symbol = "";
            this.btnSendDataFilePaths.SymbolSize = 25F;
            this.btnSendDataFilePaths.TabIndex = 12;
            this.btnSendDataFilePaths.Tooltip = "Open Windows Explorer to the current data folder and copy the path to your clipbo" +
    "ard";
            this.btnSendDataFilePaths.Click += new System.EventHandler(this.btnSendDataFilePaths_Click);
            // 
            // lblSendDataInternetMessage
            // 
            this.lblSendDataInternetMessage.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblSendDataInternetMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSendDataInternetMessage.ForeColor = System.Drawing.Color.Black;
            this.lblSendDataInternetMessage.Location = new System.Drawing.Point(341, 312);
            this.lblSendDataInternetMessage.Name = "lblSendDataInternetMessage";
            this.lblSendDataInternetMessage.Size = new System.Drawing.Size(372, 23);
            this.lblSendDataInternetMessage.TabIndex = 8;
            this.lblSendDataInternetMessage.Text = "Requires that this device be connected to the Franklin Energy network.";
            // 
            // txtSendDataLocalPath
            // 
            this.txtSendDataLocalPath.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSendDataLocalPath.Border.Class = "TextBoxBorder";
            this.txtSendDataLocalPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSendDataLocalPath.ButtonCustom.Text = "...";
            this.txtSendDataLocalPath.ButtonCustom.Visible = true;
            this.txtSendDataLocalPath.ForeColor = System.Drawing.Color.Black;
            this.txtSendDataLocalPath.Location = new System.Drawing.Point(341, 312);
            this.txtSendDataLocalPath.Name = "txtSendDataLocalPath";
            this.txtSendDataLocalPath.PreventEnterBeep = true;
            this.txtSendDataLocalPath.ReadOnly = true;
            this.txtSendDataLocalPath.Size = new System.Drawing.Size(416, 26);
            this.txtSendDataLocalPath.TabIndex = 9;
            this.txtSendDataLocalPath.Visible = false;
            this.txtSendDataLocalPath.WatermarkText = "Select a local path -->";
            this.txtSendDataLocalPath.ButtonCustomClick += new System.EventHandler(this.txtSendDataLocalPath_ButtonCustomClick);
            this.txtSendDataLocalPath.VisibleChanged += new System.EventHandler(this.txtSendDataLocalPath_VisibleChanged);
            // 
            // cboSendDataVia
            // 
            this.cboSendDataVia.DisplayMember = "Text";
            this.cboSendDataVia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSendDataVia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSendDataVia.ForeColor = System.Drawing.Color.Black;
            this.cboSendDataVia.FormattingEnabled = true;
            this.cboSendDataVia.ItemHeight = 20;
            this.cboSendDataVia.Location = new System.Drawing.Point(53, 312);
            this.cboSendDataVia.Name = "cboSendDataVia";
            this.cboSendDataVia.Size = new System.Drawing.Size(281, 26);
            this.cboSendDataVia.Sorted = true;
            this.cboSendDataVia.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSendDataVia.TabIndex = 7;
            this.cboSendDataVia.SelectedIndexChanged += new System.EventHandler(this.cboSendDataVia_SelectedIndexChanged);
            // 
            // chbSendDataCompress
            // 
            this.chbSendDataCompress.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbSendDataCompress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbSendDataCompress.Checked = true;
            this.chbSendDataCompress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSendDataCompress.CheckValue = "Y";
            this.chbSendDataCompress.Enabled = false;
            this.chbSendDataCompress.ForeColor = System.Drawing.Color.Black;
            this.chbSendDataCompress.Location = new System.Drawing.Point(53, 256);
            this.chbSendDataCompress.Name = "chbSendDataCompress";
            this.chbSendDataCompress.Size = new System.Drawing.Size(100, 23);
            this.chbSendDataCompress.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbSendDataCompress.TabIndex = 5;
            this.chbSendDataCompress.Text = "&Compress file";
            this.chbSendDataCompress.CheckedChanged += new System.EventHandler(this.chbSendDataCompress_CheckedChanged);
            // 
            // lblSendDataFile
            // 
            this.lblSendDataFile.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblSendDataFile.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSendDataFile.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSendDataFile.ForeColor = System.Drawing.Color.Black;
            this.lblSendDataFile.Location = new System.Drawing.Point(13, 150);
            this.lblSendDataFile.Name = "lblSendDataFile";
            this.lblSendDataFile.Size = new System.Drawing.Size(135, 23);
            this.lblSendDataFile.TabIndex = 1;
            this.lblSendDataFile.Text = "Send Data File";
            this.lblSendDataFile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblSendDataFile_MouseDown);
            // 
            // btnSendDataSend
            // 
            this.btnSendDataSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSendDataSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendDataSend.Enabled = false;
            this.btnSendDataSend.Location = new System.Drawing.Point(640, 563);
            this.btnSendDataSend.Name = "btnSendDataSend";
            this.btnSendDataSend.Size = new System.Drawing.Size(40, 40);
            this.btnSendDataSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSendDataSend.Symbol = "";
            this.btnSendDataSend.SymbolSize = 25F;
            this.btnSendDataSend.TabIndex = 14;
            this.btnSendDataSend.Tooltip = "Send the data file using the selected medium";
            this.btnSendDataSend.Click += new System.EventHandler(this.btnSendDataSend_Click);
            // 
            // lblSendDataNote
            // 
            this.lblSendDataNote.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblSendDataNote.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSendDataNote.ForeColor = System.Drawing.Color.Black;
            this.lblSendDataNote.Location = new System.Drawing.Point(53, 429);
            this.lblSendDataNote.Name = "lblSendDataNote";
            this.lblSendDataNote.Size = new System.Drawing.Size(704, 114);
            this.lblSendDataNote.TabIndex = 10;
            this.lblSendDataNote.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblSendDataVia
            // 
            this.lblSendDataVia.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblSendDataVia.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSendDataVia.ForeColor = System.Drawing.Color.Black;
            this.lblSendDataVia.Location = new System.Drawing.Point(53, 285);
            this.lblSendDataVia.Name = "lblSendDataVia";
            this.lblSendDataVia.Size = new System.Drawing.Size(75, 23);
            this.lblSendDataVia.TabIndex = 6;
            this.lblSendDataVia.Text = "&Send data via:";
            // 
            // cboSendDataUser
            // 
            this.cboSendDataUser.DisplayMember = "Text";
            this.cboSendDataUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSendDataUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSendDataUser.ForeColor = System.Drawing.Color.Black;
            this.cboSendDataUser.FormattingEnabled = true;
            this.cboSendDataUser.ItemHeight = 20;
            this.cboSendDataUser.Location = new System.Drawing.Point(53, 224);
            this.cboSendDataUser.Name = "cboSendDataUser";
            this.cboSendDataUser.Size = new System.Drawing.Size(281, 26);
            this.cboSendDataUser.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSendDataUser.TabIndex = 4;
            this.cboSendDataUser.SelectedIndexChanged += new System.EventHandler(this.cboSendDataUser_SelectedIndexChanged);
            // 
            // lblSendDataUser
            // 
            this.lblSendDataUser.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblSendDataUser.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSendDataUser.ForeColor = System.Drawing.Color.Black;
            this.lblSendDataUser.Location = new System.Drawing.Point(53, 198);
            this.lblSendDataUser.Name = "lblSendDataUser";
            this.lblSendDataUser.Size = new System.Drawing.Size(48, 23);
            this.lblSendDataUser.TabIndex = 3;
            this.lblSendDataUser.Text = "&User:";
            // 
            // lblSendDataInfo
            // 
            this.lblSendDataInfo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblSendDataInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSendDataInfo.ForeColor = System.Drawing.Color.Black;
            this.lblSendDataInfo.Location = new System.Drawing.Point(13, 169);
            this.lblSendDataInfo.Name = "lblSendDataInfo";
            this.lblSendDataInfo.Size = new System.Drawing.Size(453, 23);
            this.lblSendDataInfo.TabIndex = 2;
            this.lblSendDataInfo.Text = "This will allow you to send your current data to the System Administrator.";
            // 
            // pnlRestore
            // 
            this.pnlRestore.BackColor = System.Drawing.Color.White;
            this.pnlRestore.Controls.Add(this.lblRestoreNote);
            this.pnlRestore.Controls.Add(this.lstRestoreList);
            this.pnlRestore.Controls.Add(this.btnRestoreSelect);
            this.pnlRestore.Controls.Add(this.btnRestoreCancel);
            this.pnlRestore.Controls.Add(this.chbRestoreBackupCurrent);
            this.pnlRestore.Controls.Add(this.labelX1);
            this.pnlRestore.Controls.Add(this.cboRestoreUser);
            this.pnlRestore.Controls.Add(this.lblRestoreUser);
            this.pnlRestore.Controls.Add(this.labelX3);
            this.pnlRestore.ForeColor = System.Drawing.Color.Black;
            this.pnlRestore.Location = new System.Drawing.Point(831, 4);
            this.pnlRestore.Name = "pnlRestore";
            this.pnlRestore.Size = new System.Drawing.Size(779, 624);
            this.pnlRestore.TabIndex = 3;
            this.pnlRestore.Visible = false;
            // 
            // lblRestoreNote
            // 
            this.lblRestoreNote.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblRestoreNote.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRestoreNote.ForeColor = System.Drawing.Color.Black;
            this.lblRestoreNote.Location = new System.Drawing.Point(53, 460);
            this.lblRestoreNote.Name = "lblRestoreNote";
            this.lblRestoreNote.Size = new System.Drawing.Size(704, 83);
            this.lblRestoreNote.TabIndex = 23;
            this.lblRestoreNote.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lstRestoreList
            // 
            this.lstRestoreList.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lstRestoreList.Border.Class = "ListViewBorder";
            this.lstRestoreList.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstRestoreList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRestoreItems});
            this.lstRestoreList.ForeColor = System.Drawing.Color.Black;
            this.lstRestoreList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstRestoreList.HideSelection = false;
            this.lstRestoreList.Location = new System.Drawing.Point(53, 162);
            this.lstRestoreList.MultiSelect = false;
            this.lstRestoreList.Name = "lstRestoreList";
            this.lstRestoreList.Size = new System.Drawing.Size(281, 282);
            this.lstRestoreList.TabIndex = 22;
            this.lstRestoreList.UseCompatibleStateImageBehavior = false;
            this.lstRestoreList.View = System.Windows.Forms.View.Details;
            this.lstRestoreList.SelectedIndexChanged += new System.EventHandler(this.lstRestoreList_SelectedIndexChanged);
            // 
            // colRestoreItems
            // 
            this.colRestoreItems.Text = "Backups";
            this.colRestoreItems.Width = 281;
            // 
            // btnRestoreSelect
            // 
            this.btnRestoreSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRestoreSelect.Location = new System.Drawing.Point(640, 563);
            this.btnRestoreSelect.Name = "btnRestoreSelect";
            this.btnRestoreSelect.Size = new System.Drawing.Size(40, 40);
            this.btnRestoreSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRestoreSelect.Symbol = "";
            this.btnRestoreSelect.SymbolSize = 25F;
            this.btnRestoreSelect.TabIndex = 19;
            this.btnRestoreSelect.Tooltip = "Restore the selected backup data file.";
            this.btnRestoreSelect.Click += new System.EventHandler(this.btnRestoreSelect_Click);
            // 
            // btnRestoreCancel
            // 
            this.btnRestoreCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRestoreCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestoreCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRestoreCancel.Location = new System.Drawing.Point(717, 563);
            this.btnRestoreCancel.Name = "btnRestoreCancel";
            this.btnRestoreCancel.Size = new System.Drawing.Size(40, 40);
            this.btnRestoreCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRestoreCancel.Symbol = "";
            this.btnRestoreCancel.SymbolSize = 25F;
            this.btnRestoreCancel.TabIndex = 21;
            this.btnRestoreCancel.Tooltip = "Cancel any changes and close this form";
            // 
            // chbRestoreBackupCurrent
            // 
            this.chbRestoreBackupCurrent.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbRestoreBackupCurrent.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbRestoreBackupCurrent.Checked = true;
            this.chbRestoreBackupCurrent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRestoreBackupCurrent.CheckValue = "Y";
            this.chbRestoreBackupCurrent.ForeColor = System.Drawing.Color.Black;
            this.chbRestoreBackupCurrent.Location = new System.Drawing.Point(470, 572);
            this.chbRestoreBackupCurrent.Name = "chbRestoreBackupCurrent";
            this.chbRestoreBackupCurrent.Size = new System.Drawing.Size(154, 23);
            this.chbRestoreBackupCurrent.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbRestoreBackupCurrent.TabIndex = 20;
            this.chbRestoreBackupCurrent.Text = "&Backup current file first";
            this.chbRestoreBackupCurrent.CheckedChanged += new System.EventHandler(this.chbRestoreBackupCurrent_CheckedChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(13, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(237, 23);
            this.labelX1.TabIndex = 15;
            this.labelX1.Text = "Restore Backup Data File";
            // 
            // cboRestoreUser
            // 
            this.cboRestoreUser.DisplayMember = "Text";
            this.cboRestoreUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboRestoreUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRestoreUser.ForeColor = System.Drawing.Color.Black;
            this.cboRestoreUser.FormattingEnabled = true;
            this.cboRestoreUser.ItemHeight = 20;
            this.cboRestoreUser.Location = new System.Drawing.Point(53, 114);
            this.cboRestoreUser.Name = "cboRestoreUser";
            this.cboRestoreUser.Size = new System.Drawing.Size(281, 26);
            this.cboRestoreUser.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboRestoreUser.TabIndex = 18;
            this.cboRestoreUser.SelectedIndexChanged += new System.EventHandler(this.cboRestoreUser_SelectedIndexChanged);
            // 
            // lblRestoreUser
            // 
            this.lblRestoreUser.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblRestoreUser.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRestoreUser.ForeColor = System.Drawing.Color.Black;
            this.lblRestoreUser.Location = new System.Drawing.Point(57, 88);
            this.lblRestoreUser.Name = "lblRestoreUser";
            this.lblRestoreUser.Size = new System.Drawing.Size(48, 23);
            this.lblRestoreUser.TabIndex = 17;
            this.lblRestoreUser.Text = "&User:";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.Black;
            this.labelX3.Location = new System.Drawing.Point(13, 31);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(401, 23);
            this.labelX3.TabIndex = 16;
            this.labelX3.Text = "This will allow you to restore a previous version of Efficiency Clipboard data.";
            // 
            // pnlDebug
            // 
            this.pnlDebug.BackColor = System.Drawing.Color.White;
            this.pnlDebug.Controls.Add(this.btnDebugClear);
            this.pnlDebug.Controls.Add(this.chbDebugCopyToClipboard);
            this.pnlDebug.Controls.Add(this.btnDebugLoad);
            this.pnlDebug.Controls.Add(this.txtDebugData);
            this.pnlDebug.ForeColor = System.Drawing.Color.Black;
            this.pnlDebug.Location = new System.Drawing.Point(1658, 4);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(779, 624);
            this.pnlDebug.TabIndex = 0;
            this.pnlDebug.Visible = false;
            // 
            // btnDebugClear
            // 
            this.btnDebugClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDebugClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDebugClear.Location = new System.Drawing.Point(91, 12);
            this.btnDebugClear.Name = "btnDebugClear";
            this.btnDebugClear.Size = new System.Drawing.Size(75, 23);
            this.btnDebugClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDebugClear.TabIndex = 1;
            this.btnDebugClear.Text = "Clear";
            this.btnDebugClear.Click += new System.EventHandler(this.btnDebugClear_Click);
            // 
            // chbDebugCopyToClipboard
            // 
            this.chbDebugCopyToClipboard.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.chbDebugCopyToClipboard.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbDebugCopyToClipboard.Checked = true;
            this.chbDebugCopyToClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbDebugCopyToClipboard.CheckValue = "Y";
            this.chbDebugCopyToClipboard.ForeColor = System.Drawing.Color.Black;
            this.chbDebugCopyToClipboard.Location = new System.Drawing.Point(172, 12);
            this.chbDebugCopyToClipboard.Name = "chbDebugCopyToClipboard";
            this.chbDebugCopyToClipboard.Size = new System.Drawing.Size(132, 23);
            this.chbDebugCopyToClipboard.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbDebugCopyToClipboard.TabIndex = 2;
            this.chbDebugCopyToClipboard.Text = "Copy to clipboard";
            // 
            // btnDebugLoad
            // 
            this.btnDebugLoad.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDebugLoad.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDebugLoad.Location = new System.Drawing.Point(8, 12);
            this.btnDebugLoad.Name = "btnDebugLoad";
            this.btnDebugLoad.Size = new System.Drawing.Size(75, 23);
            this.btnDebugLoad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDebugLoad.TabIndex = 0;
            this.btnDebugLoad.Text = "Load";
            this.btnDebugLoad.Click += new System.EventHandler(this.btnDebugLoad_Click);
            // 
            // txtDebugData
            // 
            this.txtDebugData.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDebugData.Border.Class = "TextBoxBorder";
            this.txtDebugData.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDebugData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebugData.ForeColor = System.Drawing.Color.Black;
            this.txtDebugData.Location = new System.Drawing.Point(8, 41);
            this.txtDebugData.Multiline = true;
            this.txtDebugData.Name = "txtDebugData";
            this.txtDebugData.PreventEnterBeep = true;
            this.txtDebugData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDebugData.Size = new System.Drawing.Size(756, 478);
            this.txtDebugData.TabIndex = 3;
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(193))))));
            // 
            // frmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(885, 644);
            this.Controls.Add(this.pslMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHelp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Help";
            this.Load += new System.EventHandler(this.frmHelp_Load);
            this.pslMain.ResumeLayout(false);
            this.pnlSendDataFile.ResumeLayout(false);
            this.pnlRestore.ResumeLayout(false);
            this.pnlDebug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.PageSlider pslMain;
        private DevComponents.DotNetBar.Controls.PageSliderPage pnlSendDataFile;
        private DevComponents.DotNetBar.ButtonX btnSendDataSend;
        private DevComponents.DotNetBar.LabelX lblSendDataNote;
        private DevComponents.DotNetBar.LabelX lblSendDataVia;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSendDataUser;
        private DevComponents.DotNetBar.LabelX lblSendDataUser;
        private DevComponents.DotNetBar.LabelX lblSendDataInfo;
        private DevComponents.DotNetBar.LabelX lblSendDataFile;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSendDataVia;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbSendDataCompress;
        private DevComponents.DotNetBar.LabelX lblSendDataInternetMessage;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSendDataLocalPath;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX btnSendDataFilePaths;
        private DevComponents.DotNetBar.ButtonX btnSendDataInstallFolder;
        private DevComponents.DotNetBar.Controls.PageSliderPage pnlDebug;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDebugData;
        private DevComponents.DotNetBar.ButtonX btnDebugLoad;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbDebugCopyToClipboard;
        private DevComponents.DotNetBar.ButtonX btnDebugClear;
        private DevComponents.DotNetBar.Controls.PageSliderPage pnlRestore;
        private DevComponents.DotNetBar.ButtonX btnRestoreSelect;
        private DevComponents.DotNetBar.ButtonX btnRestoreCancel;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbRestoreBackupCurrent;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboRestoreUser;
        private DevComponents.DotNetBar.LabelX lblRestoreUser;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ListViewEx lstRestoreList;
        private System.Windows.Forms.ColumnHeader colRestoreItems;
        private DevComponents.DotNetBar.LabelX lblRestoreNote;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbLogTrace;
        private DevComponents.DotNetBar.ButtonX btnViewDocument;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX lblDocumentation;
        private DevComponents.DotNetBar.LabelX lblDocumentationInfo;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDocuments;
        private DevComponents.DotNetBar.LabelX lblClipboardDocs;
        private DevExpress.XtraEditors.SimpleButton btnDataFileRecovery;
    }
}