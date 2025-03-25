using DevComponents.DotNetBar;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FieldTool.BLL;
using FieldTool.Bsi.Helpers;
using FieldTool.Constants;
using FieldTool.Constants.Logging;
using FieldTool.Constants.Models.CB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    public class frmMainMyWorkHelper
    {
        #region Delegate and event declarations

        internal delegate void AuditDeletedHandler(object sender, AuditEventArgs e);

        internal delegate void ValidateButtonClickHandler(object sender, GridEventArgs e);

        internal event AuditDeletedHandler AuditDeleted;

        internal event ValidateButtonClickHandler ValidateButtonClick;

        #endregion Delegate and event declarations

        #region Registered controls

        private LabelItem _statusBar;
        private SheetView _myWorkGrid;

        #endregion Registered controls

        #region Constructore

        internal frmMainMyWorkHelper(LabelItem statusBar, SheetView myWorkGrid)
        {
            this._statusBar = statusBar;
            this._myWorkGrid = myWorkGrid;
        }

        #endregion Constructore

        #region Event callers

        private void OnAuditDeleted(Audit audit)
        {
            if (this.AuditDeleted != null)
            {
                this.AuditDeleted(this, new AuditEventArgs(audit));
            }
        }

        private void OnValidateButtonClick(int rowIndex)
        {
            if (this.ValidateButtonClick != null)
            {
                this.ValidateButtonClick(this, new GridEventArgs(rowIndex));
            }
        }

        #endregion Event callers

        #region Exposed class methods (static)

        internal void DeleteSelectedAudit(ILookupService lookupService, string deletedBy)
        {
            FarPoint.Win.Spread.Column colCompleteCheck = this._myWorkGrid.GetColumnFromTag(null, "Complete");

            if ((bool)this._myWorkGrid.Cells[this._myWorkGrid.ActiveRowIndex, colCompleteCheck.Index].Value)
            {
                MessageBox.Show("You cannot delete an audit marked as completed.", "Delete Audit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                object o = this._myWorkGrid.ActiveRow.Tag;
                TaskList t = o as TaskList;

                if (t != null)
                {
                    string msg = String.Format("WARNING!\n\nThis will permanently delete the selected audit and all of its related data (company, contacts, buildings, walkthroughs, etc.).\n\n{0}\n\n" +
                        "Basically, everything you've entered for this audit using Efficiency Clipboard.\n\n" +
                        "YOU CANNOT RECOVER THIS DATA!\n\n" +
                        "Are you absolutly sure that you want to delete this audit?\n\n", t.AuditName);

                    DialogResult confirmDelete = MessageBox.Show(msg, "Delete Selected Audit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (confirmDelete == DialogResult.Yes)
                    {
                        Building.BuildingCollection buildings = DataStore.GetBuildingsByAudit(t.AuditID);

                        if (buildings.Count > 0)
                        {
                            string buildingList = "";
                            foreach (Building b in buildings)
                            {
                                buildingList += b.BuildingName + "\n";
                            }

                            msg = String.Format("This audit has these buildings associated with it:\n\n{0}\n\nThese buildings contain all of the data that you collected and entered using Efficiency Clipboard.\n\n" +
                                "If you delete this audit, ALL of this information will be permanently deleted and unrecoverable!\n\n" +
                                "Are you sure you want to continue?", buildingList);

                            confirmDelete = MessageBox.Show(msg, "Delete Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        }

                        if (confirmDelete == DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            _statusBar.Text = "DELETING AUDIT...";

                            Audit deletedAudit = DataStore.GetAudit(t.AuditID);

                            // save record of audit before deleting
                            Company company = DataStore.GetCompanyByAudit(deletedAudit);
                            UploadReturn report = default(UploadReturn);
                            msg = String.Empty;
                            try
                            {
                                String issues = "Issues Found:";
                                bool issuesFound = false;

                                if (company != null)
                                {
                                    _statusBar.Text = "SAVE TO CLOUD...";
                                    msg += "\nTry Saving record";
                                    report = DataStore.SaveToDB(new List<Company> { company }, lookupService, company.Program, company.CompanyId, t.AuditID, deletedBy, true);
                                    msg += "\nMarked as deleted? " + report.IsDeleted;
                                }
                                if (report.Errors.Count > 0)
                                {
                                    msg += "\nWARNING: Encountered Errors in Upload: " + string.Join(",", report.Errors);
                                    issues += "\n - Could not upload project data to cloud.";
                                    issuesFound = true;
                                }
                                if (report.BackupUri == null)
                                {
                                    msg += "\nWARNING: No backup url found";
                                    issues += "\n - No backup url found.";
                                    issuesFound = true;
                                }
                                else
                                {
                                    msg = "\nUploaded to " + report.BackupUri.AbsoluteUri;
                                }

                                if (issuesFound)
                                {
                                    string warningMsg = "The following issues were found\n\n" + issues + "\n\nDo you wish to continue deleting this project?";
                                    confirmDelete = MessageBox.Show(warningMsg, "Delete Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    if (confirmDelete == DialogResult.Yes) issuesFound = false;
                                }

                                if (!issuesFound)
                                {
                                    _statusBar.Text = "DELETE FROM FILE...";
                                    DataStore.DeleteCompany(t.AuditID, true);
                                    this.OnAuditDeleted(deletedAudit);
                                    msg = "\nDeleted from data file";
                                    MessageBox.Show(msg, "Delete Audit");
                                }
                                else
                                {
                                    MessageBox.Show(msg, "Delete Audit");
                                }
                            }
                            catch (Exception ex)
                            {
                                msg += "\nDeleting Audit failed";
                                Lg.FatalError(ex, msg);
                                _statusBar.Text = "FAILED!";
                                MessageBox.Show(msg, "Delete Audit");
                            }
                            finally
                            {
                                _statusBar.Text = "READY";
                            }

                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
        }

        public static Company UploadLocalFiles(string userEmail, ILookupService lookupService, Company company, LabelItem lbl)
        {
            foreach (Audit audit in company.Audits)
            {
                if (audit != default(Audit) && audit.AuditStatus == AuditStatus.COMPLETE)
                {
                    foreach (Building building in audit.Buildings)
                    {
                        foreach (EquipmentMaster eq in building.Equipments)
                        {
                            if (!String.IsNullOrEmpty(eq.ImageFilePath))
                            {
                                // if the upload
                                FileInfo f = new FileInfo(eq.ImageFilePath);
                                var msg = $"ATTEMPT Upload EquipmentImage[{f.FullName}]";
                                lbl.Text = msg;
                                Lg.Info(msg);
                                if (String.IsNullOrEmpty(eq.RestoreUrl))
                                {
                                    if (f.Exists)
                                    {
                                        var uploadResult = lookupService.UploadAuditFile(audit.ProgramId, company.CompanyId, audit.Id, userEmail, f.FullName, LookupServiceConstants.UploadFileType.EQUIPMENT_IMAGE);
                                        uploadResult.ValidateOrDie();
                                        eq.RestoreUrl = uploadResult.BackupUri.AbsoluteUri;

                                        msg = $"COMPLETED Upload EquipmentImage[{f.FullName}] : backed uploaded to [{eq.RestoreUrl}]";
                                        lbl.Text = msg;
                                        Lg.Info(msg);
                                    }
                                    else
                                    {
                                        msg = $"SKIPPED Upload EquipmentImage[{f.FullName}] : already uploaded to [{eq.RestoreUrl}]";
                                        lbl.Text = msg;
                                        Lg.Info(msg);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return company;
        }

        internal void LoadMyWorkGrid()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!File.Exists(DataStore.XmlDataFile) || new FileInfo(DataStore.XmlDataFile).Length == 0 || DataStore.GetAllCompanies().Count == 0)
            {
                this._myWorkGrid.Rows.Clear();
            }
            else
            {
                TaskList.MyTaskCollection newFormat = new TaskList.MyTaskCollection();

                MyTasks.CompanyCollection xmlData = null;
                using (TextReader reader = new StreamReader(DataStore.XmlDataFile))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(MyTasks.CompanyCollection));
                    object obj = deserializer.Deserialize(reader);
                    xmlData = (MyTasks.CompanyCollection)obj;
                }

                if (xmlData != null && xmlData.Company != null)
                {
                    foreach (MyTasks.CompanyCollectionCompany company in xmlData.Company)
                    {
                        if (company.Audits != null && company.Audits.Audit != null)
                        {
                            newFormat.obj.Add(new TaskList(company));
                        }
                    }
                }

                string filePath = DataStore.XmlMyTasksFile;
                string directory = DataStore.XmlMyTasksDirectory;

                if (!File.Exists(filePath))
                {
                    Directory.CreateDirectory(directory);
                    File.Create(filePath).Close();
                }

                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    using (TextWriter ssw = TextWriter.Synchronized(writer))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(TaskList.MyTaskCollection));//was company collection
                        s.Serialize(ssw, newFormat);
                    }
                }

                using (DataSet ds = new DataSet())
                {
                    ds.ReadXml(filePath);   // Will be in app.config

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        try
                        {
                            this._myWorkGrid.AutoGenerateColumns = false;

                            using (DataTable dt = ds.Tables[0])
                            {
                                this._myWorkGrid.DataSource = dt;
                            }

                            FarPoint.Win.Spread.Column colAccountNumber = this._myWorkGrid.GetColumnFromTag(null, "AccountNumber");
                            FarPoint.Win.Spread.Column colAccountName = this._myWorkGrid.GetColumnFromTag(null, "AccountName");
                            FarPoint.Win.Spread.Column colAddress = this._myWorkGrid.GetColumnFromTag(null, "Address");
                            FarPoint.Win.Spread.Column colPhone = this._myWorkGrid.GetColumnFromTag(null, "Phone");
                            FarPoint.Win.Spread.Column colScheduleDate = this._myWorkGrid.GetColumnFromTag(null, "ScheduleDate");
                            FarPoint.Win.Spread.Column colScheduleTime = this._myWorkGrid.GetColumnFromTag(null, "ScheduleTime");
                            FarPoint.Win.Spread.Column colAuditStatus = this._myWorkGrid.GetColumnFromTag(null, "AuditStatus");
                            FarPoint.Win.Spread.Column colAuditId = this._myWorkGrid.GetColumnFromTag(null, "AuditId");
                            FarPoint.Win.Spread.Column colCompleteCheck = this._myWorkGrid.GetColumnFromTag(null, "Complete");
                            FarPoint.Win.Spread.Column colValidateCheck = this._myWorkGrid.GetColumnFromTag(null, "Validate");

                            this._myWorkGrid.Columns[colAccountNumber.Index].DataField = "AccountNumber";
                            this._myWorkGrid.Columns[colAccountName.Index].DataField = "AccountName";
                            this._myWorkGrid.Columns[colAddress.Index].DataField = "Address";
                            this._myWorkGrid.Columns[colPhone.Index].DataField = "Phone";
                            this._myWorkGrid.Columns[colScheduleDate.Index].DataField = "ScheduleDate";
                            this._myWorkGrid.Columns[colScheduleTime.Index].DataField = "ScheduleTime";
                            this._myWorkGrid.Columns[colAuditStatus.Index].DataField = "AuditStatus";
                            this._myWorkGrid.Columns[colAuditId.Index].DataField = "AuditID";

                            foreach (TaskList t in newFormat.obj)
                            {
                                for (int j = 0; j < this._myWorkGrid.Rows.Count; j++)
                                {
                                    // SMM, 8/28/2015: If this row's audit status is complete, check the Completed box.
                                    if (this._myWorkGrid.Cells[j, colAuditStatus.Index].Value != null
                                        && (this._myWorkGrid.Cells[j, colAuditStatus.Index].Value.ToString() == Constants.AuditStatus.COMPLETE))
                                    {
                                        this._myWorkGrid.Cells[j, colCompleteCheck.Index].Value = true;
                                    }
                                    else
                                    {
                                        this._myWorkGrid.Cells[j, colCompleteCheck.Index].Value = false;
                                    }

                                    if (t.AuditID == this._myWorkGrid.Cells[j, colAuditId.Index].Text)
                                    {
                                        // Only unvalidated audits should have a checkbox.
                                        if (t.IsValidated)
                                        {
                                            GeneralCellType cell = new GeneralCellType();
                                            cell.ReadOnly = true;
                                            this._myWorkGrid.Cells[j, colValidateCheck.Index].CellType = cell;
                                            this._myWorkGrid.Cells[j, colValidateCheck.Index].Text = "Valid";
                                            this._myWorkGrid.Cells[j, colValidateCheck.Index].Locked = true;
                                            Font f = new Font("Segoe UI", 12, FontStyle.Bold);
                                            this._myWorkGrid.Cells[j, colValidateCheck.Index].Font = f;
                                        }

                                        this._myWorkGrid.Rows[j].Tag = t;
                                        // Goto next task.
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        this._myWorkGrid.Rows.Clear();
                    }
                } // using ds
            }

            Cursor.Current = Cursors.Default;
        }

        internal void MyWorkGridClickHelper(bool isColumnHeader, int rowIndex, int columnIndex)
        {
            Cursor.Current = Cursors.WaitCursor;

            FarPoint.Win.Spread.Column colCompleteCheck = this._myWorkGrid.GetColumnFromTag(null, "Complete");
            FarPoint.Win.Spread.Column colValidate = this._myWorkGrid.GetColumnFromTag(null, "Validate");

            if (isColumnHeader)
            {
                if (this._myWorkGrid.GetColumnSortIndicator(columnIndex) == FarPoint.Win.Spread.Model.SortIndicator.Ascending)
                {
                    this._myWorkGrid.SortRows(columnIndex, false, true);
                }
                else
                {
                    this._myWorkGrid.SortRows(columnIndex, true, true);
                }
            }
            else
            {
                if (columnIndex == colCompleteCheck.Index)
                {
                    object o = this._myWorkGrid.Rows[rowIndex].Tag;

                    if (o != null)
                    {
                        TaskList t = o as TaskList;

                        if (t != null)
                        {
                            if (!t.IsValidated)
                            {
                                MessageBox.Show("You cannot complete this audit until the customer has been validated.", "Validate Customer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this._myWorkGrid.Cells[this._myWorkGrid.ActiveRowIndex, colCompleteCheck.Index].Value = false;
                            }
                            else if (!String.IsNullOrWhiteSpace(t.AuditID))
                            {
                                bool auditStatus = !(bool)this._myWorkGrid.Cells[rowIndex, colCompleteCheck.Index].Value;
                                string completeStatus = (auditStatus ? AuditStatus.COMPLETE : AuditStatus.INCOMPLETE);
                                DataStore.UpdateAuditStatus(t.AuditID, completeStatus, true);
                            }
                        }
                    }
                }
                else if (columnIndex == colValidate.Index)
                {
                    if (this._myWorkGrid.Cells[rowIndex, columnIndex].Text != "Valid")
                    {
                        this.OnValidateButtonClick(rowIndex);
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }

        internal static void UpdateContactInfo(Audit audit, Contact contact)
        {
            if (audit != null && contact != null)
            {
                audit.CompanyContact = contact.ToString("FN");
            }
        }

        #endregion Exposed class methods (static)
    }
}