using System;

namespace Clipboard.UI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraSpellChecker.SpellCheckerOpenOfficeDictionary spellCheckerOpenOfficeDictionary1 = new DevExpress.XtraSpellChecker.SpellCheckerOpenOfficeDictionary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.spellChecker1 = new DevExpress.XtraSpellChecker.SpellChecker();
            this.SuspendLayout();
            // 
            // spellChecker1
            // 
            this.spellChecker1.CheckAsYouTypeOptions.CheckControlsInParentContainer = true;
            this.spellChecker1.Culture = new System.Globalization.CultureInfo("en-US");
            spellCheckerOpenOfficeDictionary1.AlphabetPath = "C:\\TFS\\FieldTool\\Dev\\Emperor\\Bugs\\General_Bugs\\Clipboard.DataFiles\\dat\\Dictionary" +
    "\\en_US.dic";
            spellCheckerOpenOfficeDictionary1.CacheKey = null;
            spellCheckerOpenOfficeDictionary1.Culture = new System.Globalization.CultureInfo("en-US");
            spellCheckerOpenOfficeDictionary1.DictionaryPath = "C:\\TFS\\FieldTool\\Dev\\Emperor\\Bugs\\General_Bugs\\Clipboard.DataFiles\\dat\\Dictionary" +
    "\\american.xlg";
            spellCheckerOpenOfficeDictionary1.Encoding = System.Text.Encoding.GetEncoding(1252);
            spellCheckerOpenOfficeDictionary1.GrammarPath = "C:\\TFS\\FieldTool\\Dev\\Emperor\\Bugs\\General_Bugs\\Clipboard.DataFiles\\dat\\Dictionary" +
    "\\en_US.aff";
            this.spellChecker1.Dictionaries.Add(spellCheckerOpenOfficeDictionary1);
            this.spellChecker1.ParentContainer = this;
            this.spellChecker1.SpellCheckMode = DevExpress.XtraSpellChecker.SpellCheckMode.AsYouType;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1182, 972);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Visual Studio 2013 Dark";
            this.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.True;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clipboard";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraSpellChecker.SpellChecker spellChecker1;
    }
}