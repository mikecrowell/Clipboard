namespace Clipboard.UI.Home.Settings.Tools
{
    partial class ucQA
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucQA));
            this.btnSaveAndClose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPassCode = new DevExpress.XtraEditors.TextEdit();
            this.tglSkipDownload = new DevExpress.XtraEditors.ToggleSwitch();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglSkipDownload.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 16.2F);
            this.btnSaveAndClose.Appearance.Options.UseFont = true;
            this.btnSaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAndClose.Image")));
            this.btnSaveAndClose.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSaveAndClose.ImageToTextIndent = 15;
            this.btnSaveAndClose.Location = new System.Drawing.Point(22, 782);
            this.btnSaveAndClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(848, 90);
            this.btnSaveAndClose.TabIndex = 3;
            this.btnSaveAndClose.Text = "Save and Close";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 16.2F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(22, 20);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 37);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "QA";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.AllowTouchScroll = true;
            this.xtraScrollableControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraScrollableControl1.Controls.Add(this.labelControl2);
            this.xtraScrollableControl1.Controls.Add(this.txtPassCode);
            this.xtraScrollableControl1.Controls.Add(this.tglSkipDownload);
            this.xtraScrollableControl1.Location = new System.Drawing.Point(3, 72);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(882, 694);
            this.xtraScrollableControl1.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(37, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 16);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Passcode";
            // 
            // txtPassCode
            // 
            this.txtPassCode.Location = new System.Drawing.Point(37, 51);
            this.txtPassCode.Name = "txtPassCode";
            this.txtPassCode.Size = new System.Drawing.Size(361, 42);
            this.txtPassCode.TabIndex = 5;
            // 
            // tglSkipDownload
            // 
            this.tglSkipDownload.Location = new System.Drawing.Point(37, 151);
            this.tglSkipDownload.Name = "tglSkipDownload";
            this.tglSkipDownload.Properties.OffText = "Auto Download Programs";
            this.tglSkipDownload.Properties.OnText = "Skip Auto Download";
            this.tglSkipDownload.Size = new System.Drawing.Size(278, 44);
            this.tglSkipDownload.TabIndex = 4;
            // 
            // ucQA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.labelControl1);
            this.Name = "ucQA";
            this.Size = new System.Drawing.Size(888, 895);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglSkipDownload.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSaveAndClose;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.ToggleSwitch tglSkipDownload;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPassCode;
    }
}
