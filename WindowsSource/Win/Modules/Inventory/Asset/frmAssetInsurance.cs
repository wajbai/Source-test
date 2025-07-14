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

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetInsurance : frmBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        AppSchemaSet Appschema = new AppSchemaSet();
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmAssetInsurance()
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
        private int insDetailId = 0;
        public int InsDetailId
        {
            get
            {
                RowIndex = gvInsurance.FocusedRowHandle;
                insDetailId = gvInsurance.GetFocusedRowCellValue(colInsDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsDetailId).ToString()) : 0;
                return insDetailId;
            }
            set
            {
                insDetailId = value;
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
        #endregion

        #region Events

        /// <summary>
        /// Load Insurance Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInsuranceVouView_Load(object sender, EventArgs e)
        {
            LoadAssetInsuranceDetails();
            LoadProject();
            LoadDefaults();
        }

        /// <summary>
        /// To Add the form the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInsuranceView_AddClicked(object sender, EventArgs e)
        {
            LoadAssetDetails();
            ShowInsurence((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Print the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInsuranceView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcInsurance, this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_RENEW_PRINT_CAPTION), PrintType.DS, gvInsurance, true);
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
        /// Editvalue Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectName = glkpProject.SelectedText.ToString();
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
        }

        /// <summary>
        /// Asset Item Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvInsurance.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvInsurance, colassetItem);
            }
        }

        /// <summary>
        /// History Details count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsuranceHistory_RowCountChanged(object sender, EventArgs e)
        {
            lblRenewalHistoryCount.Text = gvInsuranceHistory.RowCount.ToString();
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

        /// <summary>
        /// Asset Details count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsurance_RowCountChanged(object sender, EventArgs e)
        {
            lblAssetItemCount.Text = gvInsurance.RowCount.ToString();
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
                    resultArgs = renewSystem.FetchInsuranceDetails();
                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcInsurance.DataSource = resultArgs.DataSource.Table;
                        gcInsurance.RefreshDataSource();
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
            if (gvInsurance.RowCount > 0)
            {
                ucInsuranceView.ChangeAddCaption = AssetInsurance.Renew.ToString();
            }
            else
            {
                ucInsuranceView.ChangeAddCaption = AssetInsurance.Create.ToString();
            }

        }
        /// <summary>
        /// Show the forms of Add Screen
        /// </summary>
        /// <param name="InsDetailId"></param>
        private void ShowInsurence(int InsDetailId)
        {
            frmRenewInsurance RenewIns = new frmRenewInsurance(InsDetailId, AssetItem, AssetId, PolicyNo);
            RenewIns.UpdateHeld += new EventHandler(OnUpdateHeld);
            RenewIns.ShowDialog();
        }

        /// <summary>
        /// load the Asset Details
        /// </summary>
        private void LoadAssetDetails()
        {
            using (InsuranceRenewSystem InsRenew = new InsuranceRenewSystem())
            {
                resultArgs = InsRenew.LoadAssetDetailsById(InsItemId);
                if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    AssetItem = resultArgs.DataSource.Table.Rows[0][this.Appschema.AppSchema.ASSETItem.ASSET_ITEMColumn.ColumnName].ToString();
                    AssetId = resultArgs.DataSource.Table.Rows[0][this.Appschema.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName].ToString();
                    PolicyNo = resultArgs.DataSource.Table.Rows[0]["POLICY_NO"].ToString();
                }
            }
        }


        /// <summary>
        /// Edit the Insurance Details
        /// </summary>
        private void ShowForm()
        {
            if (gvInsurance.RowCount > 0)
            {
                if (InsItemId > 0)
                {
                    ShowInsurence(InsItemId);
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
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
                using (InsuranceRenewSystem Renewsystem = new InsuranceRenewSystem())
                {
                    int InsuranceDetailId = gvInsurance.GetFocusedRowCellValue(colItemDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colItemDetailId).ToString()) : 0;
                    resultArgs = Renewsystem.LoadHistoryDetailsById(InsuranceDetailId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcInsuranceHistory.DataSource = resultArgs.DataSource.Table;
                        gcInsuranceHistory.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }
    }
}