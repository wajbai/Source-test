using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model;
using Bosco.DAO.Schema;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmInsuranceView : frmFinanceBase
    {
        #region VariableDeclaration
        private int RowIndex = 0;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Property
        private int ProjectId { get; set; }
        private string ProjectName { get; set; }
        private DataSet dsInsuranceDetaisl { get; set; }
        private DataTable dtInsuranceMaster { get; set; }
        private DataTable dtInsuranceHistory { get; set; }
        public int mode { get; set; }

        // private int insdetailId = 0;
        public int InsuranceDetailId
        {
            //get
            //{
            //    RowIndex = gvInsurance.FocusedRowHandle;
            //    insdetailId = gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId).ToString()) : 0;
            //    return insdetailId;
            //}
            //set
            //{
            //    insdetailId = value;
            //}
            get;
            set;
        }

        private int ItemdetailId = 0;
        public int ItemDetailId
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                ItemdetailId = gvInsurance.GetFocusedRowCellValue(gcItemDetailsId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(gcItemDetailsId).ToString()) : 0;
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
                assetitem = gvInsurance.GetFocusedRowCellValue(colAssetItem) != null ? gvInsurance.GetFocusedRowCellValue(colAssetItem).ToString() : string.Empty;
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

        private string renewalDate = string.Empty;
        public string RenewalDate
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                renewalDate = gvInsurance.GetFocusedRowCellValue(colReneDate) != null ? gvInsurance.GetFocusedRowCellValue(colReneDate).ToString() : string.Empty;
                return renewalDate;
            }
            set
            {
                renewalDate = value;
            }
        }
        private string amount = string.Empty;
        public string Amount
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                amount = gvInsurance.GetFocusedRowCellValue(colAmount) != null ? gvInsurance.GetFocusedRowCellValue(colAmount).ToString() : string.Empty;
                return amount;
            }
            set
            {
                amount = value;
            }
        }
        #region Constructor
        public frmInsuranceView()
        {
            InitializeComponent();
        }
        public frmInsuranceView(int ProjectId, string ProjectDate)
            : this()
        {

        }
        #endregion



        #endregion

        #region Events

        /// <summary>
        /// load  the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInsuranceView_Load(object sender, EventArgs e)
        {
            LoadProject();
            ProjectId = glkpProjects.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString()) : 0;
            ProjectName = glkpProjects.EditValue != null ? glkpProjects.Properties.GetDisplayText(glkpProjects.EditValue) : string.Empty;
            chkAll.Checked = true;
            LoadInsuranceDetails();
            LoadDefaults();
        }

        /// <summary>
        /// add the Insuance Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_AddClicked(object sender, EventArgs e)
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
                    LoadDefaults();
                    ShowForm();
                }
            }
            else
            {
                //this.ShowMessageBox("No Record to Create Insurance Details");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_NORECORD_CREATE_INSURANCE));
            }
        }

        /// <summary>
        /// Edit the Insurance Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            //mode = mode = (int)AssetInsurance.Update;
            //ShowForm();
            if (InsuranceDetailId > 0)
            {
                mode = (int)AssetInsurance.Update;
                ShowForm();
            }
            else
            {
                mode = (int)AssetInsurance.Create;
                ShowForm();
            }
        }

        /// <summary>
        /// Edit the Insurance Details while press Enter Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInsuranceView_EnterClicked(object sender, EventArgs e)
        {
            //mode = mode = (int)AssetInsurance.Update;
            //ShowForm();

            if (InsuranceDetailId > 0)
            {
                mode = (int)AssetInsurance.Update;
                ShowForm();
            }
            else
            {
                mode = (int)AssetInsurance.Create;
                ShowForm();
            }
        }

        /// <summary>
        /// Delete the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                using (AssetItemSystem assetitemSystem = new AssetItemSystem())
                {
                    if (InsuranceDetailId > 0)
                        DeleteInsHistoryDetail();
                    else
                        //this.ShowMessageBox("No Renewal History is available to Delete the Records");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_NORENEWAL_HISTORY_AVAIL_INFO));
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
        }

        /// <summary>
        /// close the Insurance Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// check the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvInsurance.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvInsurance, colRenewalDate);
            }
        }

        /// <summary>
        /// row count the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsurance_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvInsurance.RowCount.ToString();
        }

        /// <summary>
        /// Edit the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpProjects_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
        }

        /// <summary>
        /// Print the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcInsurance, this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_RENEW_PRINT_CAPTION), PrintType.DS, gvInsurance, true);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load the Insurance Details
        /// </summary>
        private void LoadInsuranceDetails()
        {
            try
            {
                using (InsuranceRenewSystem InsuanceRenew = new InsuranceRenewSystem())
                {
                    InsuanceRenew.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
                    dsInsuranceDetaisl = InsuanceRenew.LoadFetchInsuranceDetails();

                    if (dsInsuranceDetaisl.Tables.Count > (int)YesNo.Yes)
                    {
                        dtInsuranceMaster = dsInsuranceDetaisl.Tables[0];
                        dtInsuranceHistory = dsInsuranceDetaisl.Tables[1];
                    }
                    if (dsInsuranceDetaisl.Tables.Count != (int)YesNo.No)
                    {
                        gcInsurance.DataSource = dsInsuranceDetaisl = dsInsuranceDetaisl;
                        gcInsurance.DataMember = "Master";
                        gcInsurance.RefreshDataSource();
                        LoadDefaults();
                    }
                    else
                    {
                        gcInsurance.DataSource = null;
                        gcInsurance.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Apply the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadInsuranceDetails();
        }

        /// <summary>
        /// To change the Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsurance_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InsuranceDetailId = gvInsurance.GetFocusedRowCellValue(colInsDetailid) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsDetailid).ToString()) : 0;
            LoadDefaults();
            if (gvInsurance.FocusedRowHandle >= 0)
            {
                DataRow dr = gvInsurance.GetDataRow(gvInsurance.FocusedRowHandle);
                if (dr != null)
                {
                    int Status = this.UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString());

                    if (Status == (int)YesNo.No)
                    {
                        ucToolBar1.DisableAddButton = false;
                    }
                    else if (Status == (int)YesNo.Yes)
                    {
                        ucToolBar1.DisableAddButton = true;
                    }
                }
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
                    glkpProjects.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProjects, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        //   this.glkpProjects.EditValueChanged -= new System.EventHandler(this.glkpProjects_EditValueChanged);
                        //   glkpProjects.EditValue = (ProjectId != 0) ? ProjectId : glkpProjects.Properties.GetKeyValue(0);
                        //  this.glkpProjects.EditValueChanged += new System.EventHandler(this.glkpProjects_EditValueChanged);
                        ProjectId = this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
                        glkpProjects.EditValue = ProjectId;
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
        /// Delete the Details
        /// </summary>
        private void DeleteInsHistoryDetail()
        {
            try
            {
                if (gvInsurance.RowCount != 0)
                {
                    using (InsuranceRenewSystem RenewSystem = new InsuranceRenewSystem())
                    {
                        if (this.ShowConfirmationMessage("Are you sure to delete Insurance History?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            RenewSystem.InsDetailId = RenewSystem.ItemDetailId = gvInsurance.GetFocusedRowCellValue(gcItemDetailsId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(gcItemDetailsId).ToString()) : 0;

                            resultArgs = RenewSystem.DeleteInsuranceVoucherByItemDetailId(); // Added By Praveento delete the Voucher Details
                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs = RenewSystem.DeleteInsuranceByItemDetailById(RenewSystem.InsDetailId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                }
                                LoadInsuranceDetails();
                            }
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

        /// <summary>
        /// Show the forms of Add Screen
        /// </summary>
        /// <param name="InsDetailId"></param>
        private void ShowInsurence(int InsDetailId)
        {
            frmRenewInsuranceVoucherAdd RenewIns = new frmRenewInsuranceVoucherAdd(InsDetailId, ItemDetailId, AssetItemDetail, AssetIdDetail, mode, RenewalDate, Amount);
            RenewIns.Projectid = ProjectId;
            RenewIns.ProjectName = ProjectName;
            RenewIns.UpdateHeld += new EventHandler(OnUpdateHeld);
            RenewIns.ShowDialog();
        }

        /// <summary>
        /// refresh the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadInsuranceDetails();
            gvInsurance.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// double click the Insurance Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsurance_DoubleClick(object sender, EventArgs e)
        {
            InsuranceDetailId = gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId).ToString()) : 0;
            if (ucToolBar1.DisableAddButton)
            {
                if (InsuranceDetailId > 0)
                {
                    mode = (int)AssetInsurance.Update;
                    ShowForm();
                }
                else
                {
                    mode = (int)AssetInsurance.Create;
                    ShowForm();
                }
            }
            else
            {
                //this.ShowMessageBoxWarning("Insurance cannot be created/renewed for the sold/disposed items");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Asset.Insurance.INS_CANNOT_CREATE_SOLD_DISPOSE_DONATE_ITEM));
            }
        }

        /// <summary>
        /// refresh the Load Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            LoadInsuranceDetails();
        }

        /// <summary>
        /// load the caption of Insurance Details
        /// </summary>
        private void LoadDefaults()
        {
            if (InsuranceDetailId == 0)
            {
                ucToolBar1.ChangeAddCaption = AssetInsurance.Create.ToString();
                mode = (int)AssetInsurance.Create;
            }
            else
            {
                ucToolBar1.ChangeAddCaption = AssetInsurance.Renew.ToString();
                mode = (int)AssetInsurance.Renew;
            }
        }

        /// <summary>
        /// Edit the Insurance Details
        /// </summary>
        private void ShowForm()
        {
            if (gvInsurance.RowCount > 0)
            {
                if (mode == (int)AssetInsurance.Create)
                {
                    if (InsuranceDetailId > 0)
                        ShowInsurence(InsuranceDetailId);
                    else
                        ShowInsurence(InsuranceDetailId);
                }
                else if (mode == (int)AssetInsurance.Renew)
                {
                    if (InsuranceDetailId > 0)
                        ShowInsurence(InsuranceDetailId);
                    else
                        ShowInsurence(InsuranceDetailId);
                }
                else if (mode == (int)AssetInsurance.Update)
                {
                    if (InsuranceDetailId > 0)
                        ShowInsurence(InsuranceDetailId);
                    else
                        //this.ShowMessageBox("No Record to Edit Insurance Details");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.INS_NORECORD_EDIT_INFO));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }
        #endregion

        private void glkpProjects_EditValueChanged_1(object sender, EventArgs e)
        {
            ProjectId = glkpProjects.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString()) : 0;
            ProjectName = glkpProjects.EditValue != null ? glkpProjects.Properties.GetDisplayText(glkpProjects.EditValue) : string.Empty;
        }

        private void gvInsurance_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int Status = 0;
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                DataRow drRow = View.GetDataRow(e.RowHandle);
                if (drRow != null)
                {
                    Status = this.UtilityMember.NumberSet.ToInteger(drRow["STATUS"].ToString());
                    if (Status == (int)YesNo.No)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                    }
                }
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                dtDateAsOn.EditValue = null;
                dtDateAsOn.Enabled = false;
            }
            else
            {
                dtDateAsOn.Enabled = true;
            }
        }

        private void gvHistory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            InsuranceDetailId = gridView.GetFocusedRowCellValue(colInsDetailid) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colInsDetailid).ToString()) : 0;
            int RowHandle = (gcInsurance.FocusedView as GridView).FocusedRowHandle;
            if (RowHandle >= 0)
            {
                DataRow dr = gvHistory.GetDataRow(RowHandle);
                if (dr != null)
                {
                    int Status = this.UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString());
                    if (Status == (int)YesNo.No)
                    {
                        ucToolBar1.DisableAddButton = false;
                    }
                    else if (Status == (int)YesNo.Yes)
                    {
                        ucToolBar1.DisableAddButton = true;
                    }
                }
            }
        }

        private void gvHistory_RowStyle(object sender, RowStyleEventArgs e)
        {
            int Status = 0;
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                DataRow drRow = View.GetDataRow(e.RowHandle);
                if (drRow != null)
                {
                    Status = this.UtilityMember.NumberSet.ToInteger(drRow["STATUS"].ToString());
                    if (Status == (int)YesNo.No)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                    }
                }
            }
        }

        private void ucToolBar1_RenewClicked(object sender, EventArgs e)
        {
            //mode = mode = (int)AssetInsurance.Update;
            //ShowForm();
            if (InsuranceDetailId > 0)
            {
                mode = (int)AssetInsurance.Renew;
                ShowForm();
            }
            else
            {
                mode = (int)AssetInsurance.Create;
                ShowForm();
            }
        }

        private void gvHistory_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            InsuranceDetailId = gridView.GetFocusedRowCellValue(colInsDetailid) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colInsDetailid).ToString()) : 0;
            if (InsuranceDetailId > 0)
            {
                mode = (int)AssetInsurance.Update;
                ShowForm();
            }
            else
            {
                mode = (int)AssetInsurance.Create;
                ShowForm();
            }
        }
    }
}