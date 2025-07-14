using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model;
using Bosco.DAO.Schema;
using Bosco.Model.Inventory.Asset;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Base;
using AcMEDSync.Model;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ACPP.Modules.Master;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAMCRenewalView : frmFinanceBase
    {
        #region Varible Declaration
        DataSet dsAMCRenewal = new DataSet();
        ResultArgs resultArgs = new ResultArgs();
        private int RowIndex;
        #endregion

        #region Constructor
        public frmAMCRenewalView()
        {
            InitializeComponent();
        }
        public frmAMCRenewalView(int ProjectId, string ProjectDate)
            : this()
        {

        }
        #endregion

        #region Properties
        private DataSet dsRenewal { get; set; }
        private DataTable dtAMCRenewalMaster { get; set; }
        private DataTable dtAMCRenewalAMCItems { get; set; }
        private DataTable dtAMCRenewalHistory { get; set; }
        public int mode { get; set; }
        private int amcid = 0;
        private int AMCId
        {
            set
            {
                amcid = value;
            }
            get
            {
                RowIndex = gvAMC.FocusedRowHandle;
                amcid = gvAMC.GetFocusedRowCellValue(colMasterAmcId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAMC.GetFocusedRowCellValue(colMasterAmcId).ToString()) : 0;
                return amcid;
            }
        }

        /// <summary>
        /// To get the AMC id from Renewal History screen
        /// </summary>
        private int HistoryAMCID
        {
            get;
            set;
        }

        private int _RenewalId = 0;
        private int AMCRenewalID
        {
            get;
            set;
        }
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
        #endregion

        #region Methods

        public void SetTitle()
        {
                this.Text = this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_VIEW_CAPTION);
        }

        private void LoadAMCDetails()
        {
            try
            {
                using (AssetAMCRenewalSystem AssetAMCRenewal = new AssetAMCRenewalSystem())
                {
                    AssetAMCRenewal.ProjectId = ProjectId;
                    dsAMCRenewal = AssetAMCRenewal.LoadAMCRenewalDetails();

                    if (dsAMCRenewal.Tables.Count > (int)YesNo.Yes)
                    {
                        dtAMCRenewalMaster = dsAMCRenewal.Tables[0];
                        dtAMCRenewalAMCItems = dsAMCRenewal.Tables[1];
                    }
                    if (dsAMCRenewal.Tables.Count != (int)YesNo.No)
                    {
                        gcAMC.DataSource = dsRenewal = dsAMCRenewal;
                        gcAMC.DataMember = FDRenewalCaption.Master.ToString();
                        gcAMC.RefreshDataSource();
                    }
                    if (dsAMCRenewal.Tables.Count == 0)
                    {
                        gcAMC.DataSource = null;
                        gcAMC.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        //private void LoadAmcRenewalHistory(int amcId)
        //{
        //    try
        //    {
        //        using (AssetAMCRenewalSystem assetamcrenewalSystem = new AssetAMCRenewalSystem())
        //        {
        //            assetamcrenewalSystem.AMCId = amcId;
        //            dtAMCRenewalHistory = assetamcrenewalSystem.FetchAMCRenewalHistory().DataSource.Table;
        //            if (dtAMCRenewalHistory != null && dtAMCRenewalHistory.Rows.Count > 0)
        //            {
        //                dtAMCRenewalHistory.Columns.Add("STATUS", typeof(int));
        //              //  gcAMCRenewal.DataSource = dtAMCRenewalHistory;
        //                gvAMCRenewal.FocusedColumn = colPeriodFrom;
        //            }
        //            else
        //            {
        //                //gcAMCRenewal.DataSource = null;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
        //    }
        //    finally { }
        //}
        /// <summary>
        /// Show the forms of Add Screen
        /// </summary>
        /// <param name="InsDetailId"></param>
        private void ShowAMCRenewal(int AmcId, int mode, int RenewalId)
        {
            frmAMCRenewAdd RenewIns = new frmAMCRenewAdd(AmcId, mode, RenewalId);
            RenewIns.UpdateHeld += new EventHandler(OnUpdateHeld);
            RenewIns.ShowDialog();
        }

        private void DeleterenewalHistory()
        {
            try
            {
                using (AssetAMCRenewalSystem AmcRenewalSyatem = new AssetAMCRenewalSystem())
                {
                    AmcRenewalSyatem.AMCId = HistoryAMCID;
                    AmcRenewalSyatem.AmcRenewalId = AMCRenewalID;
                    resultArgs = AmcRenewalSyatem.DeleteRenewalHistoryByamcRenewalId();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        //  this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                        DataView dvpro = resultArgs.DataSource.Table.AsDataView();
                        dvpro.RowFilter = "PROJECT_ID=" + ProjectId + "";
                        bool isProjectavail = false;
                        if (dvpro.ToTable().Rows.Count > 0)
                        {
                            isProjectavail = true;
                        }
                        int DefaultPrId = this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
                        glkpProject.EditValue = (ProjectId != 0 && isProjectavail) ? ProjectId : DefaultPrId;
                        // this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
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
        #endregion

        #region Events
        private void frmAMCRenewalView_Load(object sender, EventArgs e)
        {
            try
            {
                LoadProject();
                //LoadDefaults();
                LoadAMCDetails();
                SetTitle();
                // LoadAmcRenewalHistory(AMCId);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucAMCRenewal_AddClicked(object sender, EventArgs e)
        {
            frmAMCRenewAdd renewal = new frmAMCRenewAdd();
           // renewal.ProjectId = this.ProjectId;
            renewal.UpdateHeld += new EventHandler(OnUpdateHeld);
            renewal.ShowDialog();
        }

        private void gvAMC_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                int amcId = gvAMC.GetFocusedRowCellValue(colMasterAmcId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAMC.GetFocusedRowCellValue(colMasterAmcId).ToString()) : 0;
                //LoadAmcRenewalHistory(amcId);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvAMCDetails_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int amcId = gridView.GetFocusedRowCellValue(colAMCId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colAMCId).ToString()) : 0;
                // LoadAmcRenewalHistory(amcId);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvAMCRenewal_RowStyle(object sender, RowStyleEventArgs e)
        {
            //try
            //{
            //    int AMCDays = this.UtilityMember.NumberSet.ToInteger(UIAppSetting.ShowAMCRenewalAlert);
            //    int CalculatedDays = 0;
            //    string AMCDDate = string.Empty;
            //    DateTime currentdate = DateTime.Today;
            //    AMCDDate = gvAMCRenewal.GetRowCellDisplayText(e.RowHandle, gvAMCRenewal.Columns["AMC_TO"].ToString());
            //    DateTime dtAMCDate = this.UtilityMember.DateSet.ToDate(AMCDDate, false);
            //    if (e.RowHandle >= 0)
            //    {
            //        if (currentdate < dtAMCDate)
            //        {
            //            CalculatedDays = (dtAMCDate - currentdate).Days;
            //            if (AMCDays == CalculatedDays)
            //            {
            //                e.Appearance.BackColor = Color.Salmon;
            //                e.Appearance.BackColor2 = Color.SeaShell;
            //                gvAMCRenewal.SetRowCellValue(e.RowHandle, "STATUS", "Active");
            //            }
            //        }
            //        else
            //        {
            //            gvAMCRenewal.SetRowCellValue(e.RowHandle, "STATUS", "Closed");
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            //}
            //finally { }
        }

        private void gvAMCDetails_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int amcId = gridView.GetFocusedRowCellValue(colAMCId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colAMCId).ToString()) : 0;
                //LoadAmcRenewalHistory(amcId);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvAMC_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int amcId = gridView.GetFocusedRowCellValue(colAMCId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colAMCId).ToString()) : 0;
                //LoadAmcRenewalHistory(amcId);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAMC.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            gvAMCDetails.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAMC, colAMCGroup);
            }
        }
        private void gvAMCDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                using (AssetAMCRenewalSystem assetamcSystem = new AssetAMCRenewalSystem())
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    int amcId = gridView.GetFocusedRowCellValue(colMasterAmcId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colMasterAmcId).ToString()) : 0;
                    int renewalId = assetamcSystem.GetmaxAMCRenewalIdByAMCId(AMCId);
                    ShowAMCRenewal(amcId, (int)AssetAmc.Edit, renewalId);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvAMC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                using (AssetAMCRenewalSystem assetamcSystem = new AssetAMCRenewalSystem())
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    int amcId = gridView.GetFocusedRowCellValue(colMasterAmcId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colMasterAmcId).ToString()) : 0;
                    int renewalId = assetamcSystem.GetmaxAMCRenewalIdByAMCId(AMCId);
                    ShowAMCRenewal(amcId, (int)AssetAmc.Edit, renewalId);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucAMCRenewal_RefreshClicked(object sender, EventArgs e)
        {
            LoadAMCDetails();
            // LoadAmcRenewalHistory(AMCId);
        }
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadAMCDetails();
            //  LoadAmcRenewalHistory(AMCId);
            gvAMC.FocusedRowHandle = RowIndex;
        }
        private void ucAMCRenewal_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                using (AssetAMCRenewalSystem Assetamcsystem = new AssetAMCRenewalSystem())
                {
                    int amcId = gvAMC.GetFocusedRowCellValue(colAMCId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAMC.GetFocusedRowCellValue(colAMCId).ToString()) : 0;
                    Assetamcsystem.AMCId = amcId;
                    DataTable dtAMCVouchers = Assetamcsystem.FetchVoucherIdbyAmcId().DataSource.Table;
                    int RenewalCount = Assetamcsystem.FetchRenewalHistoryCount();
                    HistoryAMCID = (gcAMC.FocusedView as GridView).GetFocusedRowCellValue(colRenewalAmcId) != null ? this.UtilityMember.NumberSet.ToInteger((gcAMC.FocusedView as GridView).GetFocusedRowCellValue(colRenewalAmcId).ToString()) : 0;
                    AMCRenewalID = (gcAMC.FocusedView as GridView).GetFocusedRowCellValue(colAMCRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger((gcAMC.FocusedView as GridView).GetFocusedRowCellValue(colAMCRenewalId).ToString()) : 0;
                    Assetamcsystem.VoucherId = (gcAMC.FocusedView as GridView).GetFocusedRowCellValue(colVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger((gcAMC.FocusedView as GridView).GetFocusedRowCellValue(colVoucherId).ToString()) : 0;
                    //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                        if (HistoryAMCID != 0 && gcAMC.FocusedView.Name == "gvAMCRenewal")
                        {
                            
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (RenewalCount == 1)
                                {
                                    if (dtAMCVouchers!=null && dtAMCVouchers.Rows.Count>0)
                                    {
                                        foreach (DataRow dr in dtAMCVouchers.Rows)
                                        {
                                            Assetamcsystem.VoucherId =this.UtilityMember.NumberSet.ToInteger( dr["VOUCHER_ID"].ToString());
                                            resultArgs = Assetamcsystem.DeleteAMCHistoryDetails();
                                        }
                                        
                                    }
                                    
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = Assetamcsystem.DeleteAMCItemMapping();
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = Assetamcsystem.DeleteAMCMasterDetails();

                                        }
                                    }
                                }
                                else
                                {
                                    DeleterenewalHistory();
                                }
                            }
                        }
                        else if (gcAMC.FocusedView.Name == "gvAMC")
                        {
                            //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            //{
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_RENEWAL_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    if (dtAMCVouchers != null && dtAMCVouchers.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dtAMCVouchers.Rows)
                                        {
                                            Assetamcsystem.VoucherId = this.UtilityMember.NumberSet.ToInteger(dr["VOUCHER_ID"].ToString());
                                            resultArgs = Assetamcsystem.DeleteAMCHistoryDetails();
                                        }
                                       
                                    }
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = Assetamcsystem.DeleteAMCItemMapping();
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = Assetamcsystem.DeleteAMCMasterDetails();
                                        }
                                    }
                                }
                            //}
                        }
                   // }
                    LoadAMCDetails();
                    //LoadAmcRenewalHistory(AMCId);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucAMCRenewal_EditClicked(object sender, EventArgs e)
        {
            //int renewalId=0;
            using (AssetAMCRenewalSystem assetamcSystem = new AssetAMCRenewalSystem())
            {
                if (!gcAMC.FocusedView.Name.Equals(gvAMCRenewal.Name))
                {
                    //DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    //AMCRenewalID = gridView.GetFocusedRowCellValue(colAMCRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colAMCRenewalId).ToString()) : 0;
                    AMCRenewalID = assetamcSystem.GetmaxAMCRenewalIdByAMCId(AMCId);
                }
                if (AMCId > 0)
                {
                    ShowAMCRenewal(AMCId, (int)AssetAmc.Edit, AMCRenewalID);
                }
            }
        }

        private void rbtnRenewalHistoryDelete_Click(object sender, EventArgs e)
        {
            DeleterenewalHistory();
            LoadAMCDetails();
            //  LoadAmcRenewalHistory(AMCId);
        }

        private void rbtnRenewalHistoryDelete_DoubleClick(object sender, EventArgs e)
        {
            DeleterenewalHistory();
            LoadAMCDetails();
            //LoadAmcRenewalHistory(AMCId);
        }

        private void rbtnAMCRenewalHistoryEdit_DoubleClick(object sender, EventArgs e)
        {
            using (AssetAMCRenewalSystem assetamcSystem = new AssetAMCRenewalSystem())
            {
                int renewalId = this.UtilityMember.NumberSet.ToInteger(gvAMCRenewal.GetFocusedRowCellValue(colAMCRenewalId).ToString());
                ShowAMCRenewal(HistoryAMCID, (int)AssetAmc.Edit, renewalId);
            }
        }

        private void rbtnAMCRenewalHistoryEdit_Click(object sender, EventArgs e)
        {
            using (AssetAMCRenewalSystem assetamcSystem = new AssetAMCRenewalSystem())
            {
                int renewalId = this.UtilityMember.NumberSet.ToInteger(gvAMCRenewal.GetFocusedRowCellValue(colAMCRenewalId).ToString());
                ShowAMCRenewal(HistoryAMCID, (int)AssetAmc.Edit, renewalId);
            }
        }

        private void gvAMC_RowCountChanged(object sender, EventArgs e)
        {
            lblAMCRowCount.Text = gvAMC.RowCount.ToString();
        }

        private void ucAMCRenewal_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucAMCRenewal_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAMC, this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_PRINT_CAPTION), PrintType.DS, gvAMC, true);
        }

        private void ucAMCRenewal_RenewClicked(object sender, EventArgs e)
        {
            using (AssetAMCRenewalSystem assetamcSystem = new AssetAMCRenewalSystem())
            {
                int renewalId = assetamcSystem.GetmaxAMCRenewalIdByAMCId(AMCId);
                if (AMCId > 0)
                {
                    ShowAMCRenewal(AMCId, (int)AssetAmc.Renew, 0);
                }
            }
        }
        private void frmAMCRenewalView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            try
            {
                LoadAMCDetails();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    LoadAMCDetails();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            //}
            //finally { }
        }
        private void frmAMCRenewalView_EnterClicked(object sender, EventArgs e)
        {
            using (AssetAMCRenewalSystem assetamcSystem = new AssetAMCRenewalSystem())
            {
                int renewalId = (!gvAMCRenewal.IsFocusedView) ? assetamcSystem.GetmaxAMCRenewalIdByAMCId(AMCId) : this.UtilityMember.NumberSet.ToInteger(gvAMCRenewal.GetFocusedRowCellValue(colAMCRenewalId).ToString());
                if (AMCId > 0)
                {
                    ShowAMCRenewal(AMCId, (int)AssetAmc.Edit, renewalId);
                }
            }
        }

        private void gvAMCRenewal_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                using (AssetAMCRenewalSystem assetamcSystem = new AssetAMCRenewalSystem())
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    int amcId = gridView.GetFocusedRowCellValue(colMasterAmcId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colMasterAmcId).ToString()) : 0;
                    int renewalId = gridView.GetFocusedRowCellValue(colAMCRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colAMCRenewalId).ToString()) : 0;
                    ShowAMCRenewal(amcId, (int)AssetAmc.Edit, renewalId);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvAMCRenewal_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            AMCRenewalID = gridView.GetFocusedRowCellValue(colAMCRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colAMCRenewalId).ToString()) : 0;
        }

        private void frmAMCRenewalView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }

        #endregion
    }
}