namespace Clipboard.UI.Home.Settings.Tools
{
    partial class ucSavingData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSavingData));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtAbout = new DevExpress.XtraEditors.MemoEdit();
            this.chkSaveOnMenu = new DevExpress.XtraEditors.CheckEdit();
            this.btnDefault = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAndClose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSaveOnMenu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 16.2F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(13, 18);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(301, 37);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Saving Data User Settings";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraScrollableControl1.Controls.Add(this.labelControl5);
            this.xtraScrollableControl1.Controls.Add(this.labelControl4);
            this.xtraScrollableControl1.Controls.Add(this.labelControl3);
            this.xtraScrollableControl1.Controls.Add(this.labelControl2);
            this.xtraScrollableControl1.Controls.Add(this.txtAbout);
            this.xtraScrollableControl1.Controls.Add(this.chkSaveOnMenu);
            this.xtraScrollableControl1.Location = new System.Drawing.Point(4, 77);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(668, 582);
            this.xtraScrollableControl1.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(59, 381);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(488, 18);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "* If you UN CHECK me i will only save when you switch to the projects tab";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(59, 349);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(415, 18);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "* If you check this then I will save every time you switch tabs. ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 21);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "About";
            // 
            // txtAbout
            // 
            this.txtAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAbout.Location = new System.Drawing.Point(9, 41);
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAbout.Properties.Appearance.Options.UseFont = true;
            this.txtAbout.Properties.ReadOnly = true;
            this.txtAbout.Size = new System.Drawing.Size(634, 96);
            this.txtAbout.TabIndex = 4;
            // 
            // chkSaveOnMenu
            // 
            this.chkSaveOnMenu.Location = new System.Drawing.Point(9, 302);
            this.chkSaveOnMenu.Name = "chkSaveOnMenu";
            this.chkSaveOnMenu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSaveOnMenu.Properties.Appearance.Options.UseFont = true;
            this.chkSaveOnMenu.Properties.Caption = "Save on Menu change";
            this.chkSaveOnMenu.Size = new System.Drawing.Size(249, 30);
            this.chkSaveOnMenu.TabIndex = 3;
            // 
            // btnDefault
            // 
            this.btnDefault.AllowFocus = false;
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefault.Appearance.Options.UseFont = true;
            this.btnDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnDefault.Image")));
            this.btnDefault.Location = new System.Drawing.Point(389, 13);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(258, 42);
            this.btnDefault.TabIndex = 43;
            this.btnDefault.Text = "Set to default";
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
            this.btnSaveAndClose.Location = new System.Drawing.Point(13, 674);
            this.btnSaveAndClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(644, 90);
            this.btnSaveAndClose.TabIndex = 44;
            this.btnSaveAndClose.Text = "Save and Close";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(9, 191);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(574, 24);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "* Clicking on the PROJECTS tab will always save the data to file.";
            // 
            // ucSavingData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.labelControl1);
            this.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.True;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "ucSavingData";
            this.Size = new System.Drawing.Size(672, 782);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSaveOnMenu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.MemoEdit txtAbout;
        private DevExpress.XtraEditors.CheckEdit chkSaveOnMenu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnDefault;
        private DevExpress.XtraEditors.SimpleButton btnSaveAndClose;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}
