using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Dsync;
using Bosco.Utility;

namespace ACPP.Modules.Dsync
{
    public partial class frmExportMastersToSubBranch : frmFinanceBaseAdd
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmExportMastersToSubBranch()
        {
            InitializeComponent();
        }
        #endregion

        #region property
        public DataTable dtMappedProjects { get; set; }
        #endregion

        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExportMastersToSubBranch_Load(object sender, EventArgs e)
        {
            FetchBranchList();
            FetchMappedProjects();
           // LoadProjects();
        }

        private void glkpSubBranches_EditValueChanged(object sender, EventArgs e)
        {
            FetchMappedProjects();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                using (SubBranchSystem subBranchsystem = new SubBranchSystem())
                {
                    int[] SelectedProjects = GetCheckedProjects();
                    if (SelectedProjects.Count() > 0)
                    {
                        subBranchsystem.SelectedProjectList = SelectedProjects;
                        if (glkpSubBranches.EditValue != null)
                        {
                            subBranchsystem.BranchId = UtilityMember.NumberSet.ToInteger(glkpSubBranches.EditValue.ToString());
                        }
                        this.ShowWaitDialog();
                        resultArgs = subBranchsystem.ExportMasterstoSubBranch();

                        if (resultArgs.Success)
                        {
                            resultArgs = ExportXmlOffline(resultArgs.DataSource.TableSet);
                            if (resultArgs.Success)
                            {
                                //this.ShowSuccessMessage("Masters exported successfully");
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_MASTERS_SUCCESS_INFO));
                            }
                            else
                            {
                                this.ShowMessageBoxError(resultArgs.Message);
                            }
                        }
                        this.CloseWaitDialog();
                    }
                }
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProjects.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
        }
        private void gvProjects_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (gvProjects.GetRowCellValue(gvProjects.FocusedRowHandle, colBalance) != null
            //    && (UInt32)gvProjects.GetRowCellValue(gvProjects.FocusedRowHandle, colStatus) == 1
            //    && View.f)
            //{
            //    e.Cancel = true;
            //    this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.UNMAP_CASH_LEDGER));
            //}
            ////DataTable dtUnmapProjects = gvProjects.DataSource as DataTable;
            ////if (dtUnmapProjects != null && dtUnmapProjects.Rows.Count > 0)
            ////{
            ////    foreach (DataRow dr in dtUnmapProjects.Rows)
            ////    {
            ////        if (this.UtilityMember.NumberSet.ToInteger(dr["MAPPED_STATUS"].ToString()) == 1 && dr["HAS_BALANCE"] != null)
            ////        {
            ////            e.Cancel = true;
            ////        }
            ////    }
            ////}

        }
        #endregion

        #region Methods
        public void LoadProjects()
        {
            using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
            {
                ResultArgs resultArgs = vouchersystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    gcProjects.DataSource = resultArgs.DataSource.Table;
                    gcProjects.RefreshDataSource();
                }
            }
        }
        private void FetchMappedProjects()
        {
            using (SubBranchSystem subBranchsystem = new SubBranchSystem())
            {
                if (glkpSubBranches.EditValue != null)
                {
                    subBranchsystem.BranchId = this.UtilityMember.NumberSet.ToInteger(glkpSubBranches.EditValue.ToString());
                }
                resultArgs = subBranchsystem.FetchMappedProjects();
                if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    
                    gcProjects.DataSource = resultArgs.DataSource.Table;
                    gcProjects.RefreshDataSource();
                    AssignMappedProjects();
                    //DataView dvMappedProjects = new DataView(resultArgs.DataSource.Table);
                    //dvMappedProjects.RowFilter = String.Format("STATUS IN({0})", 1);
                    //if (dvMappedProjects != null && dvMappedProjects.Count > 0)
                    //{
                    //    foreach(dvMappedProjects
                    //}
                }
            }

        }
        private void AssignMappedProjects()
        {
            DataTable dtProjects = gcProjects.DataSource as DataTable;
            if (dtProjects != null && dtProjects.Rows.Count > 0)
            {
                gvProjects.ClearSelection();
                foreach (DataRow dr in dtProjects.Rows)
                {
                    if (this.UtilityMember.NumberSet.ToInteger(dr["MAPPED_STATUS"].ToString()) == 1 )
                    {
                        int Index = dtProjects.Rows.IndexOf(dr);
                        gvProjects.SelectRow(Index);
                    }
                }
               // gvProjects.SelectAll();
            }
        }


        private void FetchBranchList()
        {
            try
            {
                using (SubBranchSystem subbranchsystem = new SubBranchSystem())
                {
                    ResultArgs resultArgs = subbranchsystem.FetchBranchList();
                    if (resultArgs.Success)
                    {
                        glkpSubBranches.Properties.DataSource = resultArgs.DataSource.Table;
                        glkpSubBranches.Properties.DisplayMember = "BRANCH_OFFICE_NAME";
                        glkpSubBranches.Properties.ValueMember = "BRANCH_OFFICE_ID";
                        glkpSubBranches.EditValue = glkpSubBranches.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private bool IsValidInput()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(glkpSubBranches.Text))
            {
                //this.ShowMessageBoxError("Sub Branch is not selected.");
                this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_MASTERS_BRANCH_NOT_SELECT_INFO));
                isValid = false;
            }
            else if (gvProjects.RowCount == 0)
            {
                //this.ShowMessageBoxError("No project is available to export.");
                this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_MASTERS_NO_PROJECT_AVAIL_INFO));
                isValid = false;
            }
            else if (gvProjects.SelectedRowsCount == 0)
            {
                //this.ShowMessageBoxError("Atleast a project must be selected to export.");
                this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_MASTERS_ATLEAST_PROJECT_SELECT_INFO));
                isValid = false;
            }

            return isValid;
        }

        private int[] GetCheckedProjects()
        {
            int[] SelectedIds = gvProjects.GetSelectedRows();
            int[] sCheckedProjects = new int[SelectedIds.Count()];
            int ArrayIndex = 0;
            if (SelectedIds.Count() > 0)
            {
                foreach (int RowIndex in SelectedIds)
                {
                    DataRow drProject = gvProjects.GetDataRow(RowIndex);
                    if (drProject != null)
                    {
                        sCheckedProjects[ArrayIndex] = UtilityMember.NumberSet.ToInteger(drProject["PROJECT_ID"].ToString());
                        ArrayIndex++;
                    }
                }
            }
            return sCheckedProjects;
        }

        private ResultArgs ExportXmlOffline(DataSet dsMasters)
        {
            resultArgs.Success = false;
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Xml Files|.xml";
                //saveDialog.Title = "Export Vouchers";
                saveDialog.Title = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHERS_INFO);
                saveDialog.FileName = "Sub_Branch_Masters_" + DateTime.Now.Ticks.ToString() + ".xml";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    XMLConverter.WriteToXMLFile(dsMasters, saveDialog.FileName);
                    resultArgs.Success = true;
                }
                else
                {
                    //resultArgs.Message = "Exporting Masters has been cancelled";
                    resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_MASTERS_CANCELL_INFO);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void gvProjects_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            
            //GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            //info.SelectorInfo.State = ObjectState.Disabled;
            
        }

    }
}