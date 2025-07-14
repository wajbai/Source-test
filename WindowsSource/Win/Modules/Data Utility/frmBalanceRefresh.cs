using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.Business;
using ACPP.Modules.Master;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using AcMEDSync.Model;
using System.Threading.Tasks;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmBalanceRefresh : frmFinanceBaseAdd
    {

        #region Variables
        ResultArgs resultArgs = null;
        int ProjectId;
        bool IsAutoRefresh = false;
        DateTime RefreshStartDate;
        #endregion

        public frmBalanceRefresh()
        {
            InitializeComponent();
        }

        public frmBalanceRefresh(int ProjectId, DateTime RefreshStartDate, bool IsAutoRefresh = false)
            : this()
        {
            this.ProjectId = ProjectId;
            this.IsAutoRefresh = IsAutoRefresh;
            this.RefreshStartDate = RefreshStartDate;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (glkpProject.EditValue != null)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        balanceSystem.ProjectId = UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                        balanceSystem.VoucherDate = deDateFrom.DateTime.ToShortDateString();
                        this.ShowWaitDialog();
                        ResultArgs result = balanceSystem.UpdateBulkTransBalance();
                        if (result.Success)
                            ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.BALANCE_UPDATED));

                    }
                    //BalanceSystem balanceSystem = new BalanceSystem();
                    //balanceSystem.ProjectId = UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    //balanceSystem.VoucherDate = deDateFrom.DateTime.ToShortDateString();
                    //this.ShowWaitDialog();
                    //ResultArgs result = balanceSystem.UpdateBulkTransBalance();
                    //if (result.Success)
                    //    ShowSuccessMessage("Balance Updated Successfully");
                }
            }
            catch (Exception ee)
            {
                ShowMessageBox(ee.Message);
            }
            finally { this.CloseWaitDialog(); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBalanceRefresh_Load(object sender, EventArgs e)
        {
            LoadProject();
            if (IsAutoRefresh)
            {
                deDateFrom.DateTime = RefreshStartDate;
                glkpProject.EditValue = ProjectId;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                deDateFrom.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }
        #region Methods
        private void LoadProject()
        {
            try
            {
                using (ProjectSystem loadprojects = new ProjectSystem())
                {
                    deDateFrom.DateTime = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
                    resultArgs = loadprojects.FetchProjectlistDetails();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            DataTable dtProject = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table, loadprojects.AppSchema.Project.PROJECT_IDColumn.ColumnName, loadprojects.AppSchema.Project.PROJECTColumn.ColumnName);
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, dtProject, loadprojects.AppSchema.Project.PROJECTColumn.ColumnName, loadprojects.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                            glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                        }
                    }
                    else
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PROJECT_IS_NOT_CREATED), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            ShowProjectForm();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void ShowProjectForm()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                frmProject.ShowDialog();
                if (frmProject.DialogResult == DialogResult.Yes)
                {
                    LoadProject();
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }
        #endregion
    }
}
