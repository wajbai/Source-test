using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.Dsync;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Setting;
using System.Configuration;
using System.IO;
using AcMEDSync.Model;

namespace ACPP.Modules.Dsync
{
    public partial class frmDataSyncStatus : frmFinanceBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        CommonMethod method = new CommonMethod();
        ImportVoucherSystem importVoucher = new ImportVoucherSystem();
        DataSet dsVoucher = new DataSet();
        private string FilePath { get; set; }

        #endregion

        #region constructor
        public frmDataSyncStatus()
        {
            InitializeComponent();
        }


        #endregion

        #region Properties
        private int dsyncId;
        private int DsyncStatusId
        {
            set
            {
                dsyncId= value;
            }
            get
            {
                dsyncId = gvStatus.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvStatus.GetFocusedRowCellValue(colId).ToString()) : 0;
                return dsyncId;
            }
        }
        #endregion
        #region Methods
        private void FetchDataSyncStatus()
        {
            try
            {
                using (SubBranchSystem subbranchsystem = new SubBranchSystem())
                {
                    resultArgs = subbranchsystem.FetchDsyncStatus();
                    if (resultArgs.Success && resultArgs.DataSource.Data != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcStatus.DataSource = resultArgs.DataSource.Table;
                        gcStatus.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        #endregion

        #region Events
        private void frmDataSyncStatus_Load(object sender, EventArgs e)
        {
            txtPath.Enabled = false;
            FetchDataSyncStatus();
        }
        private void gvStatus_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvStatus.RowCount.ToString();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                AcMELog.WriteLog("Welcome to Acme.erp Data Sync...");
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Filter = "XML Files (.xml)|*.xml";
                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = txtPath.Text = opendialog.FileName;
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPath.Text))
                {
                    using (SubBranchSystem subBranch = new SubBranchSystem())
                    {
                        resultArgs = subBranch.UploadVoucherFile(FilePath);
                        if (resultArgs.Success)
                        {
                            resultArgs = SynchronizeVouchers(FilePath);
                        }

                        if (resultArgs.Success)
                        {
                            FetchDataSyncStatus();
                            //this.ShowSuccessMessage("Sub Branch Vouchers Synchronized Successfully.");
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.DATA_SYNCH_SUB_BRANCH_VOUCHERS_FILE_SELECT));
                        }
                        else
                        {
                            this.CloseWaitDialog();
                            this.ShowMessageBoxError(resultArgs.Message);
                        }
                    }
                }
                else
                {
                    //this.ShowMessageBox("Kindly Select Sub Branch Voucher File and try");
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.DATA_SYNCH_SUB_BRANCH_VOUCHERS_SYNCH_SUCCESS));
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private ResultArgs SynchronizeVouchers(string FilePath)
        {
            importVoucher.SynchronizeHeld += new EventHandler(importVoucher_SynchronizeHeld);
            resultArgs= importVoucher.ImportVouchers(FilePath);
            return resultArgs;
        }

        void importVoucher_SynchronizeHeld(object sender, EventArgs e)
        {
            if (resultArgs.Success)
            {
                this.CloseWaitDialog();
                this.ShowWaitDialog(importVoucher.Caption);
            }
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStatus.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvStatus, colbranchOfficeCode);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FetchDataSyncStatus();
        }
        #endregion

        private void gvStatus_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (DsyncStatusId > 0)
            {
                using (SubBranchSystem subBranch = new SubBranchSystem(DsyncStatusId))
                {
                    txtRemarks.Text = subBranch.Remarks;
                }
            }
        }

        private void frmDataSyncStatus_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
    }
}