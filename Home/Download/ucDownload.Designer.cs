namespace Clipboard.UI.Home.Download
{
    partial class ucDownload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDownload));
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnHome = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadAgain = new DevExpress.XtraEditors.SimpleButton();
            this.txtStatus = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 16.2F);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(31, 28);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(358, 37);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Downloading your programs....";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(31, 66);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(232, 21);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "This may take serveral minutes";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 14.2F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(31, 123);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 32);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Status";
            // 
            // btnHome
            // 
            this.btnHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHome.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 16.2F);
            this.btnHome.Appearance.Options.UseFont = true;
            this.btnHome.Appearance.Options.UseTextOptions = true;
            this.btnHome.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.btnHome.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.Image")));
            this.btnHome.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnHome.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnHome.ImageToTextIndent = 30;
            this.btnHome.Location = new System.Drawing.Point(728, 584);
            this.btnHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnHome.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnHome.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnHome.Size = new System.Drawing.Size(310, 90);
            this.btnHome.TabIndex = 12;
            this.btnHome.Text = "Home";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnDownloadAgain
            // 
            this.btnDownloadAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownloadAgain.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 16.2F);
            this.btnDownloadAgain.Appearance.Options.UseFont = true;
            this.btnDownloadAgain.Appearance.Options.UseTextOptions = true;
            this.btnDownloadAgain.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.btnDownloadAgain.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadAgain.Image")));
            this.btnDownloadAgain.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnDownloadAgain.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnDownloadAgain.ImageToTextIndent = 30;
            this.btnDownloadAgain.Location = new System.Drawing.Point(31, 584);
            this.btnDownloadAgain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDownloadAgain.Name = "btnDownloadAgain";
            this.btnDownloadAgain.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnDownloadAgain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDownloadAgain.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnDownloadAgain.Size = new System.Drawing.Size(323, 90);
            this.btnDownloadAgain.TabIndex = 13;
            this.btnDownloadAgain.Text = "Download Again";
            this.btnDownloadAgain.Click += new System.EventHandler(this.btnDownloadAgain_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.EditValue = "Loading...";
            this.txtStatus.Location = new System.Drawing.Point(31, 158);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Properties.Appearance.Options.UseFont = true;
            this.txtStatus.Properties.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(1007, 404);
            this.txtStatus.TabIndex = 14;
            // 
            // ucDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnDownloadAgain);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lblTitle);
            this.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.True;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucDownload";
            this.Size = new System.Drawing.Size(1071, 702);
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnHome;
        private DevExpress.XtraEditors.SimpleButton btnDownloadAgain;
        private DevExpress.XtraEditors.MemoEdit txtStatus;
    }
}
