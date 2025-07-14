using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using Bosco.Model.UIModel;
using AcMEDSync.Model;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Transaction
{
    public partial class frmNegativeBalanceHistory : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        private int projectId = 0;
        public int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }
        private DateTime balancedate = DateTime.Now;
        private DateTime BalanceDate
        {
            set
            {
                balancedate = value;
            }
            get
            {
                return balancedate;
            }
        }
        #endregion

        #region Constructor
        public frmNegativeBalanceHistory()
        {
            InitializeComponent();
        }
        public frmNegativeBalanceHistory(int BalProjectId, DateTime dtVoucherDate)
            : this()
        {
            ProjectId = BalProjectId;
            BalanceDate = dtVoucherDate;
        }
        #endregion

        #region Events
        private void frmNegativeBalanceHistory_Load(object sender, EventArgs e)
        {
          
            LoadProject();
            deMonth.DateTime = BalanceDate;
            GetClosingBalance();
            deMonth.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deMonth.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }

        private void glkProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkProject.EditValue.ToString());
            GetClosingBalance();
        }

        private void gvNegativeBalance_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvNegativeBalance.RowCount.ToString();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            GetClosingBalance();
        }

        private void deMonth_EditValueChanged(object sender, EventArgs e)
        {
            balancedate = deMonth.DateTime;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNegativeBalanceHistory_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvNegativeBalance.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvNegativeBalance, colVoucherDate);
            }
        }
        #endregion

        #region Methods
        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkProject.EditValue = (ProjectId != 0) ? ProjectId : glkProject.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public void GetClosingBalance()
        {
            using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
            {
                voucherTransactionSystem.VoucherDate = BalanceDate;
                voucherTransactionSystem.ProjectId = projectId;
                resultArgs = voucherTransactionSystem.FetchCashBankNegativeBalanceHistory();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcNegativeBalance.DataSource = resultArgs.DataSource.Table;
                    gcNegativeBalance.RefreshDataSource();
                }
                else
                {
                    gcNegativeBalance.DataSource = null;
                }
            }
        }

        #endregion

        private void glkProject_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }
    }
}