namespace Clipboard.UI.Home.Help
{
    partial class ucConflictResolver
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
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucConflictResolver));
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            this.tileBar1 = new DevExpress.XtraBars.Navigation.TileBar();
            this.tileBarGroup2 = new DevExpress.XtraBars.Navigation.TileBarGroup();
            this.btnSearchForConflicts = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.btnResolveConflicts = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.txtStatus = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tileBar1
            // 
            this.tileBar1.AllowDrag = false;
            this.tileBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tileBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tileBar1.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileBar1.Groups.Add(this.tileBarGroup2);
            this.tileBar1.Location = new System.Drawing.Point(3, 73);
            this.tileBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tileBar1.MaxId = 7;
            this.tileBar1.Name = "tileBar1";
            this.tileBar1.Padding = new System.Windows.Forms.Padding(22, 0, 22, 7);
            this.tileBar1.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.TouchScrollBar;
            this.tileBar1.Size = new System.Drawing.Size(682, 100);
            this.tileBar1.TabIndex = 7;
            this.tileBar1.Text = "tileBar1";
            // 
            // tileBarGroup2
            // 
            this.tileBarGroup2.Items.Add(this.btnSearchForConflicts);
            this.tileBarGroup2.Items.Add(this.btnResolveConflicts);
            this.tileBarGroup2.Name = "tileBarGroup2";
            // 
            // btnSearchForConflicts
            // 
            this.btnSearchForConflicts.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement3.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement3.Image")));
            tileItemElement3.Text = "Search for conflicts";
            this.btnSearchForConflicts.Elements.Add(tileItemElement3);
            this.btnSearchForConflicts.Id = 0;
            this.btnSearchForConflicts.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Wide;
            this.btnSearchForConflicts.Name = "btnSearchForConflicts";
            // 
            // btnResolveConflicts
            // 
            this.btnResolveConflicts.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement4.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement4.Image")));
            tileItemElement4.Text = "Resolve Conflicts";
            this.btnResolveConflicts.Elements.Add(tileItemElement4);
            this.btnResolveConflicts.Id = 1;
            this.btnResolveConflicts.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Wide;
            this.btnResolveConflicts.Name = "btnResolveConflicts";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 16.2F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(19, 22);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 37);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Conflicts";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 16.2F);
            this.btnBack.Appearance.Options.UseFont = true;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBack.ImageToTextIndent = 5;
            this.btnBack.Location = new System.Drawing.Point(29, 797);
            this.btnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(643, 90);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(29, 178);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Properties.Appearance.Options.UseFont = true;
            this.txtStatus.Properties.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(643, 593);
            this.txtStatus.TabIndex = 10;
            // 
            // ucConflictResolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.tileBar1);
            this.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.True;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucConflictResolver";
            this.Size = new System.Drawing.Size(699, 912);
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TileBar tileBar1;
        private DevExpress.XtraBars.Navigation.TileBarGroup tileBarGroup2;
        private DevExpress.XtraBars.Navigation.TileBarItem btnSearchForConflicts;
        private DevExpress.XtraBars.Navigation.TileBarItem btnResolveConflicts;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.MemoEdit txtStatus;
    }
}
