namespace Clipboard.UI.LogIn
{
    partial class ucLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLogIn));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnLogIn = new DevExpress.XtraEditors.SimpleButton();
            this.pnlCredentials = new System.Windows.Forms.Panel();
            this.lblClearPin = new DevExpress.XtraEditors.LabelControl();
            this.lblWarning = new DevExpress.XtraEditors.LabelControl();
            this.lblTermsAndConditions = new DevExpress.XtraEditors.LabelControl();
            this.chkAccept = new DevExpress.XtraEditors.CheckEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblEmail = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.btnUserOne = new DevExpress.XtraEditors.CheckButton();
            this.btnUserTwo = new DevExpress.XtraEditors.CheckButton();
            this.pnlCredentials.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(29, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(260, 33);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Welcome to Clipboard";
            // 
            // btnLogIn
            // 
            this.btnLogIn.AllowFocus = false;
            this.btnLogIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogIn.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.Appearance.Options.UseFont = true;
            this.btnLogIn.Image = ((System.Drawing.Image)(resources.GetObject("btnLogIn.Image")));
            this.btnLogIn.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnLogIn.ImageToTextIndent = 15;
            this.btnLogIn.Location = new System.Drawing.Point(26, 464);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(277, 69);
            this.btnLogIn.TabIndex = 6;
            this.btnLogIn.Text = "Log In";
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // pnlCredentials
            // 
            this.pnlCredentials.Controls.Add(this.lblClearPin);
            this.pnlCredentials.Controls.Add(this.lblWarning);
            this.pnlCredentials.Controls.Add(this.lblTermsAndConditions);
            this.pnlCredentials.Controls.Add(this.btnLogIn);
            this.pnlCredentials.Controls.Add(this.chkAccept);
            this.pnlCredentials.Controls.Add(this.lblPassword);
            this.pnlCredentials.Controls.Add(this.txtPassword);
            this.pnlCredentials.Controls.Add(this.lblEmail);
            this.pnlCredentials.Controls.Add(this.txtEmail);
            this.pnlCredentials.Controls.Add(this.picUser);
            this.pnlCredentials.Location = new System.Drawing.Point(208, 89);
            this.pnlCredentials.MinimumSize = new System.Drawing.Size(325, 0);
            this.pnlCredentials.Name = "pnlCredentials";
            this.pnlCredentials.Size = new System.Drawing.Size(325, 550);
            this.pnlCredentials.TabIndex = 7;
            // 
            // lblClearPin
            // 
            this.lblClearPin.Appearance.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblClearPin.Appearance.Options.UseForeColor = true;
            this.lblClearPin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClearPin.Location = new System.Drawing.Point(252, 279);
            this.lblClearPin.Name = "lblClearPin";
            this.lblClearPin.Size = new System.Drawing.Size(51, 16);
            this.lblClearPin.TabIndex = 14;
            this.lblClearPin.Text = "Clear Pin";
            this.lblClearPin.Click += new System.EventHandler(this.lblClearPin_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Appearance.Options.UseForeColor = true;
            this.lblWarning.Location = new System.Drawing.Point(26, 408);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(61, 16);
            this.lblWarning.TabIndex = 13;
            this.lblWarning.Text = "lblWarning";
            // 
            // lblTermsAndConditions
            // 
            this.lblTermsAndConditions.Appearance.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTermsAndConditions.Appearance.Options.UseForeColor = true;
            this.lblTermsAndConditions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTermsAndConditions.Location = new System.Drawing.Point(178, 365);
            this.lblTermsAndConditions.Name = "lblTermsAndConditions";
            this.lblTermsAndConditions.Size = new System.Drawing.Size(125, 16);
            this.lblTermsAndConditions.TabIndex = 12;
            this.lblTermsAndConditions.Text = "Terms and Conditions";
            this.lblTermsAndConditions.Click += new System.EventHandler(this.lblTermsAndConditions_Click);
            // 
            // chkAccept
            // 
            this.chkAccept.Location = new System.Drawing.Point(26, 357);
            this.chkAccept.Name = "chkAccept";
            this.chkAccept.Properties.Caption = "Accept";
            this.chkAccept.Size = new System.Drawing.Size(106, 20);
            this.chkAccept.TabIndex = 11;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(26, 279);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(85, 16);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password / Pin";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(26, 299);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(277, 42);
            this.txtPassword.TabIndex = 9;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(26, 203);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(31, 16);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(26, 225);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(277, 42);
            this.txtEmail.TabIndex = 7;
            // 
            // picUser
            // 
            this.picUser.Image = ((System.Drawing.Image)(resources.GetObject("picUser.Image")));
            this.picUser.Location = new System.Drawing.Point(26, 23);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(277, 144);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUser.TabIndex = 6;
            this.picUser.TabStop = false;
            // 
            // btnUserOne
            // 
            this.btnUserOne.AllowFocus = false;
            this.btnUserOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUserOne.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserOne.Appearance.Options.UseFont = true;
            this.btnUserOne.Appearance.Options.UseTextOptions = true;
            this.btnUserOne.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.btnUserOne.Image = ((System.Drawing.Image)(resources.GetObject("btnUserOne.Image")));
            this.btnUserOne.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnUserOne.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnUserOne.ImageToTextIndent = 15;
            this.btnUserOne.Location = new System.Drawing.Point(15, 759);
            this.btnUserOne.Name = "btnUserOne";
            this.btnUserOne.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnUserOne.Size = new System.Drawing.Size(325, 64);
            this.btnUserOne.TabIndex = 23;
            this.btnUserOne.Tag = "btnUserOne";
            this.btnUserOne.Text = "...";
            // 
            // btnUserTwo
            // 
            this.btnUserTwo.AllowFocus = false;
            this.btnUserTwo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUserTwo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserTwo.Appearance.Options.UseFont = true;
            this.btnUserTwo.Appearance.Options.UseTextOptions = true;
            this.btnUserTwo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.btnUserTwo.Image = ((System.Drawing.Image)(resources.GetObject("btnUserTwo.Image")));
            this.btnUserTwo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnUserTwo.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnUserTwo.ImageToTextIndent = 15;
            this.btnUserTwo.Location = new System.Drawing.Point(15, 689);
            this.btnUserTwo.Name = "btnUserTwo";
            this.btnUserTwo.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnUserTwo.Size = new System.Drawing.Size(325, 64);
            this.btnUserTwo.TabIndex = 24;
            this.btnUserTwo.Tag = "btnUserTwo";
            this.btnUserTwo.Text = "...";
            // 
            // ucLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pnlCredentials);
            this.Controls.Add(this.btnUserTwo);
            this.Controls.Add(this.btnUserOne);
            this.Controls.Add(this.labelControl1);
            this.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.True;
            this.MinimumSize = new System.Drawing.Size(325, 617);
            this.Name = "ucLogIn";
            this.Size = new System.Drawing.Size(727, 840);
            this.pnlCredentials.ResumeLayout(false);
            this.pnlCredentials.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLogIn;
        private System.Windows.Forms.Panel pnlCredentials;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lblEmail;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private System.Windows.Forms.PictureBox picUser;
        private DevExpress.XtraEditors.LabelControl lblTermsAndConditions;
        private DevExpress.XtraEditors.CheckEdit chkAccept;
        private DevExpress.XtraEditors.LabelControl lblWarning;
        private DevExpress.XtraEditors.CheckButton btnUserOne;
        private DevExpress.XtraEditors.CheckButton btnUserTwo;
        private DevExpress.XtraEditors.LabelControl lblClearPin;
    }
}
