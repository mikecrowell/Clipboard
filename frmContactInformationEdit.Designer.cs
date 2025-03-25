namespace FieldTool.UI {
    partial class frmContactInformationEdit {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContactInformationEdit));
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.txtPhoneNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblExtension = new DevComponents.DotNetBar.LabelX();
            this.txtPhoneExtension = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtEmail = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtContactMedium = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblContactMedium = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.AccessibleDescription = " ";
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(409, 1);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(41, 41);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.Symbol = "";
            this.btnOk.SymbolSize = 25F;
            this.btnOk.TabIndex = 5;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(467, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(41, 41);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.Symbol = "";
            this.btnCancel.SymbolSize = 25F;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.AutoSelectAll = true;
            this.txtPhoneNumber.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPhoneNumber.Border.Class = "TextBoxBorder";
            this.txtPhoneNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.ForeColor = System.Drawing.Color.Black;
            this.txtPhoneNumber.Location = new System.Drawing.Point(5, 6);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.PreventEnterBeep = true;
            this.txtPhoneNumber.Size = new System.Drawing.Size(132, 26);
            this.txtPhoneNumber.TabIndex = 0;
            // 
            // lblExtension
            // 
            // 
            // 
            // 
            this.lblExtension.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblExtension.Location = new System.Drawing.Point(136, 9);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(26, 20);
            this.lblExtension.TabIndex = 1;
            this.lblExtension.Text = "ext.";
            this.lblExtension.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtPhoneExtension
            // 
            this.txtPhoneExtension.AutoSelectAll = true;
            this.txtPhoneExtension.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPhoneExtension.Border.Class = "TextBoxBorder";
            this.txtPhoneExtension.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPhoneExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneExtension.ForeColor = System.Drawing.Color.Black;
            this.txtPhoneExtension.Location = new System.Drawing.Point(165, 6);
            this.txtPhoneExtension.Name = "txtPhoneExtension";
            this.txtPhoneExtension.PreventEnterBeep = true;
            this.txtPhoneExtension.Size = new System.Drawing.Size(58, 26);
            this.txtPhoneExtension.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.AutoSelectAll = true;
            this.txtEmail.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtEmail.Border.Class = "TextBoxBorder";
            this.txtEmail.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.Black;
            this.txtEmail.Location = new System.Drawing.Point(5, 6);
            this.txtEmail.MaxLength = 200;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PreventEnterBeep = true;
            this.txtEmail.Size = new System.Drawing.Size(219, 26);
            this.txtEmail.TabIndex = 0;
            // 
            // txtContactMedium
            // 
            this.txtContactMedium.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtContactMedium.Border.Class = "TextBoxBorder";
            this.txtContactMedium.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtContactMedium.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactMedium.ForeColor = System.Drawing.Color.Black;
            this.txtContactMedium.Location = new System.Drawing.Point(273, 6);
            this.txtContactMedium.Name = "txtContactMedium";
            this.txtContactMedium.PreventEnterBeep = true;
            this.txtContactMedium.Size = new System.Drawing.Size(101, 26);
            this.txtContactMedium.TabIndex = 4;
            // 
            // lblContactMedium
            // 
            // 
            // 
            // 
            this.lblContactMedium.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblContactMedium.Location = new System.Drawing.Point(230, 8);
            this.lblContactMedium.Name = "lblContactMedium";
            this.lblContactMedium.Size = new System.Drawing.Size(37, 23);
            this.lblContactMedium.TabIndex = 3;
            this.lblContactMedium.Text = "Type:";
            this.lblContactMedium.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // frmContactInformationEdit
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(520, 45);
            this.ControlBox = false;
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblContactMedium);
            this.Controls.Add(this.txtContactMedium);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtPhoneExtension);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContactInformationEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmContactInformationEdit_FormClosing);
            this.Load += new System.EventHandler(this.frmContactInformationEdit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhoneNumber;
        private DevComponents.DotNetBar.LabelX lblExtension;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhoneExtension;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEmail;
        private DevComponents.DotNetBar.Controls.TextBoxX txtContactMedium;
        private DevComponents.DotNetBar.LabelX lblContactMedium;
    }
}