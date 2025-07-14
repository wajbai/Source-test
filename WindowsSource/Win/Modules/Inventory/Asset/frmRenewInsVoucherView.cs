using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model;
using Bosco.DAO.Schema;
using DevExpress.XtraGrid;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmRenewInsVoucherView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        AppSchemaSet Appschema = new AppSchemaSet();
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmRenewInsVoucherView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string AssetItem { get; set; }
        public string AssetId { get; set; }
        public string PolicyNo { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int mode { get; set; }

        private int insurancedetailid = 0;
        public int InsuranceDetailId
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                insurancedetailid = gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId).ToString()) : 0;
                return insurancedetailid;
            }
            set
            {
                insurancedetailid = value;
            }
        }
        private int insItemDetailId = 0;
        public int HistoryItemDetailId
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                insItemDetailId = gvInsurance.GetFocusedRowCellValue(colInsDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsDetailId).ToString()) : 0;
                return insItemDetailId;
            }
            set
            {
                insItemDetailId = value;
            }
        }

        private int insItemId = 0;
        public int InsItemId
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                insItemId = gvInsurance.GetFocusedRowCellValue(colItemId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colItemId).ToString()) : 0;
                return insItemId;
            }
            set
            {
                insItemId = value;
            }
        }

        private int ItemdetailId = 0;
        public int ItemDetailId
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                ItemdetailId = gvInsurance.GetFocusedRowCellValue(colItemDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colItemDetailId).ToString()) : 0;
                return ItemdetailId;
            }
            set
            {
                ItemdetailId = value;
            }
        }

        private string assetitem = string.Empty;
        public string AssetItemDetail
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                assetitem = gvInsurance.GetFocusedRowCellValue(colassetItem) != null ? gvInsurance.GetFocusedRowCellValue(colassetItem).ToString() : string.Empty;
                return assetitem;
            }
            set
            {
                assetitem = value;
            }
        }

        private string assetid = string.Empty;
        public string AssetIdDetail
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                assetid = gvInsurance.GetFocusedRowCellValue(colAssetId) != null ? gvInsurance.GetFocusedRowCellValue(colAssetId).ToString() : string.Empty;
                return assetid;
            }
            set
            {
                assetid = value;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Load Insurance Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInsuranceVouView_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadDefaults();
            ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            LoadAssetInsuranceDetails();
        }

        /// <summary>
        /// To Add the form the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInsuranceView_AddClicked(object sender, EventArgs e)
        {
            if (gvInsurance.RowCount > 0)
            {
                if (InsuranceDetailId == 0)
                {
                    LoadDefaults();
                    ShowInsurence((int)AddNewRow.NewRow);
                }
                else
                {
                    ShowForm();
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_CREATE_RENEW_CONFIRMATION));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsurance_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        /// <summary>
        /// Print the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInsuranceView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcInsurance, this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_RENEW_PRINT_CAPTION), PrintType.DT, gvInsurance, true);
        }

        /// <summary>
        /// Close the forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInsuranceView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// refresh the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInsuranceView_RefreshClicked(object sender, EventArgs e)
        {
            LoadAssetInsuranceDetails();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load the Asset Details
        /// </summary>
        public void LoadAssetInsuranceDetails()
        {
            try
            {
                using (InsuranceRenewSystem renewSystem = new InsuranceRenewSystem())
                {
                    renewSystem.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    int InsDetailId = gvInsurance.GetFocusedRowCellValue(colItemDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colItemDetailId).ToString()) : 0;
                    //  resultArgs = renewSystem.LoadFetchInsuranceDetails();
                    if (resultArgs != null)
                    {
                        gcInsurance.DataSource = resultArgs.DataSource.Table;
                        gcInsurance.RefreshDataSource();
                        LoadHistoryDetailsById(InsDetailId);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// This is to Load the Projects
        /// </summary>
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
                        this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                        glkpProject.EditValue = (ProjectId != 0) ? ProjectId : glkpProject.Properties.GetKeyValue(0);
                        this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
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

        /// <summary>
        /// load the caption of Insurance Details
        /// </summary>
        private void LoadDefaults()
        {
            if (gvInsuranceHistory.RowCount > 0)
            {
                ucInsuranceView.ChangeAddCaption = AssetInsurance.Renew.ToString();
                mode = (int)AssetInsurance.Renew;
            }
            else
            {
                ucInsuranceView.ChangeAddCaption = AssetInsurance.Create.ToString();
                mode = (int)AssetInsurance.Create;
            }
        }

        /// <summary>
        /// Show the forms of Add Screen
        /// </summary>
        /// <param name="InsDetailId"></param>
        private void ShowInsurence(int InsDetailId)
        {
            //frmRenewInsuranceVoucherAdd RenewIns = new frmRenewInsuranceVoucherAdd(InsDetailId, InsItemId, ItemDetailId, AssetItemDetail, AssetIdDetail, PolicyNo, mode, gvInsuranceHistory.RowCount);
            //RenewIns.UpdateHeld += new EventHandler(OnUpdateHeld);
            //RenewIns.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadAssetInsuranceDetails();
            gvInsurance.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// Edit the Insurance Details
        /// </summary>
        private void ShowForm()
        {
            if (gvInsurance.RowCount > 0)
            {
                if (InsuranceDetailId > 0)
                {
                    ShowInsurence(InsuranceDetailId);
                }
                else
                {
                    ShowInsurence(InsuranceDetailId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }
        #endregion

        /// <summary>
        /// Focus the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsurance_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                int InsuranceDetailId = gvInsurance.GetFocusedRowCellValue(colItemDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colItemDetailId).ToString()) : 0;
                LoadHistoryDetailsById(InsuranceDetailId);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InsuranceDetailId"></param>
        private void LoadHistoryDetailsById(int InsuranceDetailId)
        {
            using (InsuranceRenewSystem InsuranceRenew = new InsuranceRenewSystem())
            {
                //  resultArgs = InsuranceRenew.LoadHistoryDetailsById(InsuranceDetailId);
                if (resultArgs.Success)
                {
                    gcInsuranceHistory.DataSource = resultArgs.DataSource.Table;
                    gcInsuranceHistory.RefreshDataSource();
                    LoadDefaults();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkHistoryShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvInsuranceHistory.OptionsView.ShowAutoFilterRow = chkHistoryShowFilter.Checked;
            if (chkHistoryShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvInsuranceHistory, colRenewalDate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsuranceHistory_RowCountChanged(object sender, EventArgs e)
        {
            lblHistoryCount.Text = gvInsuranceHistory.RowCount.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            LoadAssetInsuranceDetails();
        }

        /// <summary>
        /// check chagned
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilterAvailable_CheckedChanged(object sender, EventArgs e)
        {
            gvInsurance.OptionsView.ShowAutoFilterRow = chkShowFilterAvailable.Checked;
            if (chkShowFilterAvailable.Checked)
            {
                this.SetFocusRowFilter(gvInsurance, colassetItem);
            }
        }

        /// <summary>
        /// count the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsurance_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvInsurance.RowCount.ToString();
        }

        private void ucInsuranceView_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                using (AssetItemSystem assetitemSystem = new AssetItemSystem())
                {
                    DeleteInsHistoryDetail();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
        }

        /// <summary>
        /// Delete the History Details();
        /// </summary>
        private void DeleteInsHistoryDetail()
        {
            try
            {
                if (gvInsuranceHistory.RowCount != 0)
                {
                    using (InsuranceRenewSystem RenewSystem = new InsuranceRenewSystem())
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (XtraMessageBox.Show("Do you Delete all History Details?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                // resultArgs = RenewSystem.DeleteAllInsuranceDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                }
                            }
                            LoadAssetInsuranceDetails();
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ucInsuranceView_EditClicked(object sender, EventArgs e)
        {
            mode = mode = (int)AssetInsurance.Update;
            ShowForm();
        }
    }
}