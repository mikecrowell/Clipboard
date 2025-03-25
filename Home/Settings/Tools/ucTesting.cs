using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard.UI._Helper;
using System.Reflection;
using Clipboard.Helper.Utilities;
using FieldTool.BLL.ClipboardConfiguration;
using FieldTool.BLL;
using FieldTool.DAL.DataProvider;
using FieldTool.DAL.DTO;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;

namespace Clipboard.UI.Home.Settings.Tools
{
    public partial class ucTesting : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTesting()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //Test();
                RepairDataFile();
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }


        private void Test()
        {
            var tst = new TestClass();
            tst.MyString = "Duke Energy";
            tst.MyInt = 20;
            tst.Name = "David";

            var other = new TestClass();
            other.MyString = "PSE";
            other.Amount = 100;
            other.Name = "Karl";
            

            //tst = ReflectionHelper.SetNoneDefaultFields<TestClass>(tst, other, new TestClass());


            var config = new UserClipboardConfig();
            var clientConfig = new UserClipboardConfig();
            clientConfig.Theme.SkinName = "Something Else";

            config = ReflectionHelper.SetNoneDefaultFields<UserClipboardConfig>(config, clientConfig, new UserClipboardConfig());

        }

        public void RepairDataFile()
        {
            var _companies = new CompanyDTO.CompanyCollection();
            var _dis = new DIDTO.CompanyCollection();
            var disAndCompanies = GetDataFromFile(@"C:\Users\kgrittner\Desktop\tst\data.xml");

            try
            {
                _companies = new CompanyDTO.CompanyCollection();
                _companies.Items.AddRange(disAndCompanies.ItemsCompany);
                _dis = new DIDTO.CompanyCollection();
                _dis.Items.AddRange(disAndCompanies.ItemsDI);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private DIandCompanyDTO.CompanyCollection Repair(string path)
        {
            try
            {
                List<string> lines = File.ReadAllLines(path).Reverse().ToList();
                string allData = File.ReadAllText(path);
                int count = 0;

                while (count < lines.Count())
                {
                    string txt = lines[0];
                    if (txt.Contains("</DI>") || txt.Contains("</Company>"))
                    {
                        break;
                    }
                    else
                    {
                        lines.RemoveAt(0);
                    }

                    count++;
                }

                lines.Reverse();
                lines.Add("</CompanyCollection>");

                string data = String.Join(String.Empty, lines.ToArray());


                using (TextReader sr = new StringReader(data))
                {
                    XmlSerializer gen = new XmlSerializer(typeof(DIandCompanyDTO.CompanyCollection));
                    try
                    {
                        object o = gen.Deserialize(sr);
                        DIandCompanyDTO.CompanyCollection result = new DIandCompanyDTO.CompanyCollection();
                        result = (DIandCompanyDTO.CompanyCollection)o;
                        return result;
                    }
                    catch
                    {
                        throw new Exception("I was able to read the file but not repair it. You'll need to submit a ticket for assistance.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public DIandCompanyDTO.CompanyCollection GetDataFromFile(string dataFilePath)
        {
            DIandCompanyDTO.CompanyCollection result = new DIandCompanyDTO.CompanyCollection();

            if (File.Exists(dataFilePath) && new FileInfo(dataFilePath).Length > 0)
            {
                using (TextReader reader = new StreamReader(dataFilePath))
                {
                    XmlSerializer gen = new XmlSerializer(typeof(DIandCompanyDTO.CompanyCollection));

                    try
                    {
                        object o = gen.Deserialize(reader);
                        result = (DIandCompanyDTO.CompanyCollection)o;
                    }
                    catch (InvalidOperationException ex)
                    {
                        DialogResult dialogResult = MessageBox.Show("It looks like your data file was corrupted. I can attempt to repair it for you or you can submit a ticket for assistance", "Repair data file", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DialogResult dialogResult2 = MessageBox.Show("This may result in the loss of data on the last project in the list would you still like to continue?", "Repair data file", MessageBoxButtons.YesNo);
                            if (dialogResult2 == DialogResult.Yes)
                            {
                                return Repair(dataFilePath);
                            }
                        }

                        // Invalid XML.
                        throw new XmlException("The data file is not in the correct format.  You may need to download a new version." +
                                             "\n\nIf the problem persists, please contact the System Administrator.", ex);
                    }
                }
            }

            return result;
        }







    }



    public class TestClass
    {
        public int Amount = 5;
        public string Name = "Hello Kitty";
        public string Tag = "Projects";
        public bool ShowThis = false;
        public bool ShowThat = true;
        public int OtherAmount = 0;
        public string MyString { get; set; }
        public int MyInt { get; set; }
        public float MyFloat { get; set; }
        public bool MyBool { get; set; }
    }














}
