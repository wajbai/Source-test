using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;
using ACPP.Modules.Master;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.Transaction;
using DevExpress.XtraBars;
using Bosco.Utility.ConfigSetting;
using ACPP.Modules.Dsync;

namespace ACPP.Modules.Transaction
{
    public partial class frmProjectSelection : frmFinanceBaseAdd
    {
        #region Variables
        private ResultArgs resultArgs;
        public int ProjectId = 0;
        public string ProjectName = "";
        public string RecentVoucherDate = "";
        public int SelectionTye = 0;
        ProjectSelection enableProjectSelectionMethod;
        DialogResult result = DialogResult.Cancel;
        public DefaultVoucherTypes VoucherTypes;
        private const string colVoucherDate = "VOUCHER_DATE";

        private bool assignRecentDate = true;
        #endregion

        #region Properties
        public string SetVoucherAddCaption { set { rgVoucherGroup.Properties.Items[0].Description = value; } }

        public string SetVoucherViewCaption { set { rgVoucherGroup.Properties.Items[1].Description = value; } }
        #endregion

        public frmProjectSelection()
        {
            InitializeComponent();
            assignRecentDate = true;
        }

        public frmProjectSelection(ProjectSelection projectSelection)
            : this()
        {
            enableProjectSelectionMethod = projectSelection;
        }

        public frmProjectSelection(ProjectSelection projectSelection, int projId, string ProjName)
            : this()
        {
            enableProjectSelectionMethod = projectSelection;
            ProjectId = projId;
            ProjectName = ProjName;
        }

        private void frmProjectSelection_Load(object sender, EventArgs e)
        {
            lblVoucherGroup.Visibility = enableProjectSelectionMethod == ProjectSelection.EnableVoucherSelectionMethod ? LayoutVisibility.Always : LayoutVisibility.Never;
            LoadImportMasters();
            LoadProject();
            // LoadSelectionType();
            if (lstProjectName.ItemCount > 0)
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                lstProjectName.SelectedValue = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId) > 0) ? this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId) : ProjectId;


                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime RecentVouDate = UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false);
                DateTime dtyearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                detVoucherDate.DateTime = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom >= dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                if (detVoucherDate.DateTime < dtyearfrom)
                {
                    detVoucherDate.DateTime = dtyearfrom;
                    if (dtyearfrom < dtbookbeginfrom)
                    {
                        detVoucherDate.DateTime = dtbookbeginfrom;
                    }
                }

                detVoucherDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                detVoucherDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                //if (detVoucherDate.DateTime < dtyearfrom || detVoucherDate.DateTime > dtyearTo)
                //{
                //    detVoucherDate.DateTime = dtyearfrom > dtbookbeginfrom ? dtyearfrom : dtbookbeginfrom;
                //}
            }
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                rgVoucherGroup.Properties.Items[0].Enabled = true;
            }
        }

        private void ApplyRights()
        {
            try
            {
                if (VoucherTypes == DefaultVoucherTypes.Receipt)
                {
                    if (CommonMethod.ApplyUserRights((int)Receipt.CreateReceiptVoucher) != 0)
                    {
                        rgVoucherGroup.Properties.Items[0].Enabled = true;
                    }
                }
                else if (DefaultVoucherTypes.Payment == VoucherTypes)
                {
                    if (CommonMethod.ApplyUserRights((int)Payment.CreatePaymentVoucher) != 0)
                    {
                        rgVoucherGroup.Properties.Items[0].Enabled = true;
                    }
                }
                else if (VoucherTypes == DefaultVoucherTypes.Contra)
                {
                    if (CommonMethod.ApplyUserRights((int)Contra.CreateContraVoucher) != 0)
                    {
                        rgVoucherGroup.Properties.Items[0].Enabled = true;
                    }
                }
                else if (DefaultVoucherTypes.Journal == VoucherTypes)
                {
                    if (CommonMethod.ApplyUserRights((int)Journal.CreateJournalVoucher) != 0)
                    {
                        rgVoucherGroup.Properties.Items[0].Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally {
                if (!rgVoucherGroup.Properties.Items[0].Enabled)
                {
                    rgVoucherGroup.SelectedIndex = 1;
                }
            }
        }

        private void LoadProject()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.ProjectClosedDate = detVoucherDate.Text;
                resultArgs = mappingSystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    Int32 Prevprojectid = lstProjectName.GetItemValue(lstProjectName.SelectedIndex) != null ? this.UtilityMember.NumberSet.ToInteger(lstProjectName.GetItemValue(lstProjectName.SelectedIndex).ToString()) : this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
                    lstProjectName.ValueMember = mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName;
                    lstProjectName.DisplayMember = mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName;
                    lstProjectName.DataSource = resultArgs.DataSource.Table;
                    lstProjectName.SelectedValue = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId) > 0) ? this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId) : Prevprojectid;

                }
                else
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyUserRights((int)Project.CreateProject) != 0)
                        {
                            //if (this.ShowConfirmationMessage("Projects are not available. Do you want to create the Project?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Transaction.PROJECT_SELECTION_CREATE_PROJECT), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                if (this.AppSetting.LockMasters == (int)YesNo.No)
                                {
                                    frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                                    frmProject.ShowDialog();
                                    //if (frmProject.DialogResult == DialogResult.Cancel)
                                    //{
                                    //    LoadProject();
                                    //}
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                                }
                            }
                            else
                                this.Close();
                        }
                    }
                    else
                    {
                        //if (this.ShowConfirmationMessage("Projects are not available. Do you want to create the Project?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Transaction.PROJECT_SELECTION_CREATE_PROJECT), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            if (this.AppSetting.LockMasters == (int)YesNo.No)
                            {
                                frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                                frmProject.ShowDialog();
                                //if (frmProject.DialogResult == DialogResult.Cancel)
                                //{
                                //    LoadProject();
                                //}
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                            }
                        }
                        else
                            this.Close();
                    }

                }
            }
        }

        private void LoadImportMasters()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.ProjectClosedDate = detVoucherDate.Text;
                resultArgs = mappingSystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.RowsAffected == 0)
                {
                    //if (this.ShowConfirmationMessage("Masters are not available. Do you want to import masters from Portal?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Transaction.PROJECT_SELECTION_IMPORT_MASTER_FROM_PORTAL), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        frmPortalUpdates frmPortal = new frmPortalUpdates(PortalUpdates.ImportMasters);
                        frmPortal.ShowDialog();
                    }
                    else
                        this.Close();
                }
            }
        }

        //private void LoadSelectionType()
        //{
        //    TransSelectionType trasType = new TransSelectionType();
        //    DataView dvReceiptType = this.UtilityMember.EnumSet.GetEnumDataSource(trasType, Sorting.Ascending);
        //    glkpSelection.Properties.DataSource = dvReceiptType.ToTable();
        //    glkpSelection.Properties.DisplayMember = "Name";
        //    glkpSelection.Properties.ValueMember = "Id";
        //    glkpSelection.EditValue= glkpSelection.Properties.GetKeyValue(0);
        //}

        private void btnOk_Click(object sender, EventArgs e)
        {
            ShowTransactionForm();
        }

        private void ShowTransactionForm()
        {
            try
            {
                if (lstProjectName.ItemCount > 0)
                {
                    DialogResult = DialogResult.OK;
                    ProjectId = this.UtilityMember.NumberSet.ToInteger(lstProjectName.GetItemValue(lstProjectName.SelectedIndex).ToString());
                    ProjectName = lstProjectName.GetItemText(lstProjectName.SelectedIndex);
                    RecentVoucherDate = detVoucherDate.Text;
                    SelectionTye = rgVoucherGroup.SelectedIndex;

                    if (ProjectId > 0)
                    {
                        AssignProjectId();
                        getProjectIds();
                        SettingProperty.ActiveProjectId = ProjectId;
                    }


                    // this.LoginUser.LoginUserProjectId = ProjectId;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private bool HasFormInMDI(string formName)
        {
            bool hasForm = false;

            foreach (Form frmActive in this.MdiChildren)
            {
                hasForm = (frmActive.Name.ToLower() == formName.ToLower());

                if (hasForm)
                {
                    frmActive.Select();
                    break;
                }

                frmActive.Select();
            }

            return hasForm;
        }


        private void CloseFormInMDI(string formName)
        {
            bool hasForm = false;
            foreach (Form frmActive in this.MdiChildren)
            {
                hasForm = (frmActive.Name.ToLower() == formName.ToLower());

                if (hasForm)
                {
                    frmActive.Close();
                    break;
                }

                //  frmActive.Select();
            }

            //return hasForm;
        }


        private void lstProjectName_DoubleClick(object sender, EventArgs e)
        {
            ShowTransactionForm();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (NumberSystem numberSystem = new NumberSystem())
            {
                // simpleButton1.Text = numberSystem.getNewNumber(NumberFormat.VoucherNumber, "14", (int)(VoucherTransType.Receipt));    
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AssignProjectId()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PROJECT_ID");
            dt.Columns.Add("PROJECT");
            dt.Columns.Add("VOUCHER_DATE");
            DataRow dr = dt.NewRow();
            dr["PROJECT_ID"] = ProjectId;
            dr["PROJECT"] = ProjectName;
            dr["VOUCHER_DATE"] = detVoucherDate.Text;
            dt.Rows.Add(dr);
            this.AppSetting.UserProjectInfor = dt.DefaultView;
        }

        private void getProjectIds()
        {
            string ProjectId = "";
            DataTable dtProject = lstProjectName.DataSource as DataTable;
            if (dtProject != null && dtProject.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProject.Rows)
                {
                    ProjectId += dr["PROJECT_ID"].ToString() + ",";
                }
                this.AppSetting.UserAllProjectId = ProjectId.TrimEnd(',');
            }
        }

        //private void SetVoucherDate()
        //{
        //     DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        //        DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        //    using (VoucherTransactionSystem VoucherTransactionSystem = new VoucherTransactionSystem())
        //    {  
        //        RecentVoucherDate = VoucherTransactionSystem.FetchLastVoucherDate(ProjectId, dtyearfrom, dtYearTo);
        //    }
        //    detVoucherDate.DateTime = (!string.IsNullOrEmpty(RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : dtyearfrom;
        //}
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                detVoucherDate.Focus();
            }
            if (KeyData == (Keys.F5))
            {
                lstProjectName.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void lstProjectName_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (lstProjectName.SelectedIndex == e.Index)
            {
                e.Appearance.Font = new Font(lstProjectName.Font, FontStyle.Bold);
            }
        }

        private void rgVoucherGroup_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }
        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == (Keys.Alt | Keys.E))
                {
                    if (VoucherTypes == DefaultVoucherTypes.Receipt)
                    {
                        if (CommonMethod.ApplyUserRights((int)Forms.CreateReceiptVoucher) != 0)
                        {
                            ShowTransactionForm();
                        }
                    }
                    else if (DefaultVoucherTypes.Payment == VoucherTypes)
                    {
                        if (CommonMethod.ApplyUserRights((int)Forms.CreatePaymentVoucher) != 0)
                        {
                            ShowTransactionForm();
                        }
                    }
                    else if (VoucherTypes == DefaultVoucherTypes.Contra)
                    {
                        if (CommonMethod.ApplyUserRights((int)Forms.CreateContraVoucher) != 0)
                        {
                            ShowTransactionForm();
                        }
                    }
                    else if (DefaultVoucherTypes.Journal == VoucherTypes)
                    {
                        if (CommonMethod.ApplyUserRights((int)Forms.CreateJournalVoucher) != 0)
                        {
                            ShowTransactionForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void frmProjectSelection_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void lstProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (assignRecentDate)
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                int pid = lstProjectName.GetItemValue(lstProjectName.SelectedIndex) != null ? this.UtilityMember.NumberSet.ToInteger(lstProjectName.GetItemValue(lstProjectName.SelectedIndex).ToString()) : 0;
                ApplyRecentPrjectDetails(pid);
                detVoucherDate.DateTime = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;

                if (detVoucherDate.DateTime < dtyearfrom)
                {
                    detVoucherDate.DateTime = dtyearfrom;
                    if (dtyearfrom < dtbookbeginfrom)
                    {
                        detVoucherDate.DateTime = dtbookbeginfrom;
                    }
                }
            }
            
        }
        private void ApplyRecentPrjectDetails(int proid)
        {
            try
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    accountingSystem.YearFrom = this.AppSetting.YearFrom;
                    accountingSystem.YearTo = this.AppSetting.YearTo;
                    resultArgs = accountingSystem.FetchRecentVoucherDate(proid);
                    if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            if (string.IsNullOrEmpty(dr[colVoucherDate].ToString()))
                            {
                                dr[colVoucherDate] = dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                            }
                        }
                        this.AppSetting.UserProjectInfor = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void detVoucherDate_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects-------
            assignRecentDate = false;
            LoadProject();
            assignRecentDate = true;
            //------------------------------------------
        }
    }
}