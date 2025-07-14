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
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using Bosco.Model.Transaction;
using ACPP.Modules;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules.Master
{
    public partial class frmMapBulkCostcentre : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        public string CheckSelected = "SEL";
        DialogResult MapCCDialogResult = DialogResult.Cancel;
        #endregion

        #region Constructor
        public frmMapBulkCostcentre()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private string ProjectIds
        {
            get
            {
                List<object> selecteditems = chkListCCProject.Properties.Items.GetCheckedValues();
                string selectedprojects = string.Empty;
                foreach (object item in selecteditems)
                {
                    selectedprojects += item.ToString() + ",";
                }
                selectedprojects = selectedprojects.TrimEnd(',');
                return selectedprojects;
            }
            set
            {
                chkListCCProject.SetEditValue(value);
            }
        }
        #endregion

        #region Events
        private void frmLedgerOptions_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvCostCentre.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvCostCentre, gcolAvailableCostCentreName);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidInput())
                {
                    if (resultArgs.Success)
                    {
                        this.CloseWaitDialog();
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
                this.CloseWaitDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void gvLedgerOption_RowCountChanged(object sender, EventArgs e)
        {
            lblAvailableRecordCount.Text = gvCostCentre.RowCount.ToString();
        }
        #endregion

        #region Methods
        private void SetDefaults()
        {
            //On 18/11/2022, To show Ledger based on CC mapping -------------------------------------------------------------------
            lcCCLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.AppSetting.CostCeterMapping == 1)
            {
                lcCCLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                chkSelectAllCC.Top = gcCostCentre.Top + 6;
            }
            //------------------------------------------------------------------------------------------------------------------------

            LoadProject();
            LoadCCLedgerByProject();
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = AppSetting.YearFrom;
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    chkListCCProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        chkListCCProject.Properties.DataSource = resultArgs.DataSource.Table;
                        chkListCCProject.Properties.DisplayMember = mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName;
                        chkListCCProject.Properties.ValueMember= mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadCCLedgerByProject()
        {
            Int32 PrevLedgerId = 0;
            try
            {
                if (this.AppSetting.CostCeterMapping == 1)
                {
                    using (MappingSystem mappingSystem = new MappingSystem())
                    {
                        resultArgs = mappingSystem.LoadLedgerByProjectIds(ProjectIds);
                        glkpCCLedger.Properties.DataSource = null;
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null)
                        {
                            PrevLedgerId = glkpCCLedger.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpCCLedger.EditValue.ToString()) : 0;
                            DataTable dtProjectLedger = resultArgs.DataSource.Table;
                            dtProjectLedger.DefaultView.RowFilter = mappingSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName + " = " + ((int)SetDefaultValue.DefaultValue);
                            dtProjectLedger = dtProjectLedger.DefaultView.ToTable();
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCCLedger, dtProjectLedger, mappingSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, mappingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                            glkpCCLedger.EditValue = null;

                            if (PrevLedgerId > 0)
                            {
                                if (glkpCCLedger.Properties.GetIndexByKeyValue(PrevLedgerId) >= 0)
                                {
                                    glkpCCLedger.EditValue = PrevLedgerId;
                                }
                                else
                                {
                                    glkpCCLedger.EditValue = glkpCCLedger.Properties.GetKeyValue(0);
                                }
                            }
                            else
                            {
                                glkpCCLedger.EditValue = glkpCCLedger.Properties.GetKeyValue(0);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void BindCosteCenter()
        {
            Int32 ledgerid = 0;
            try
            {
                if (!string.IsNullOrEmpty(ProjectIds))
                {
                    using (MappingSystem mappingSystem = new MappingSystem())
                    {
                        if (this.AppSetting.CostCeterMapping == 1)
                        {
                            ledgerid = glkpCCLedger.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpCCLedger.EditValue.ToString()) : 0;
                        }

                        resultArgs = mappingSystem.FetchMappedCostCenterByProjectLedger(ProjectIds, ledgerid);
                        gcCostCentre.DataSource = null;
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null)
                        {
                            DataTable dt = resultArgs.DataSource.Table;

                            dt.DefaultView.Sort = CheckSelected + " DESC," + mappingSystem.AppSchema.CostCentre.COST_CENTRE_NAMEColumn;
                            dt = dt.DefaultView.ToTable();
                            gcCostCentre.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private bool IsValidInput()
        {
            bool rtn = false;

            return rtn;
        }

              

        private void glkpCCLedger_EditValueChanged(object sender, EventArgs e)
        {
            BindCosteCenter();
        }
        #endregion

        private void chkListCCProject_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AppSetting.CostCeterMapping == 1)
            {
                LoadCCLedgerByProject();
            }
            else
            {
                BindCosteCenter();
            }
        }

        private void gvCostCentre_RowCountChanged(object sender, EventArgs e)
        {
            ShowMAppedCCCounts();
        }

        private void rbchkSelect_Click(object sender, EventArgs e)
        {
           
        }

        private void ShowMAppedCCCounts()
        {
            lblAvailableRecordCount.Text = "0";
            lblMappedCCRecordcount.Text = "0";
            if (gcCostCentre.DataSource != null)
            {
                DataTable dt = (gcCostCentre.DataSource as DataTable).DefaultView.ToTable();
                lblAvailableRecordCount.Text = dt.Rows.Count.ToString();
                dt.DefaultView.RowFilter = "SEL=1";
                lblMappedCCRecordcount.Text = dt.DefaultView.Count.ToString();
            }
        }

        private void gcCostCentre_Click(object sender, EventArgs e)
        {
           
        }

        private void rbchkSelect_EditValueChanged(object sender, EventArgs e)
        {
            if (gcCostCentre.DataSource != null)
            {
                CheckEdit chk = (sender as CheckEdit);
                if (chk.Checked==false)
                {
                    Int32 CCTransExits = UtilityMember.NumberSet.ToInteger(gvCostCentre.GetFocusedRowCellValue(gcolCCTransExists).ToString());
                    if (CCTransExits == 1)
                    {
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_TRANSACTION_MADE_ALREADY));
                        chk.Checked = true;
                    }
                }

                if (gvCostCentre.PostEditor())
                    gvCostCentre.UpdateCurrentRow();
                ShowMAppedCCCounts();
            }
        }

        private void chkSelectAllCC_CheckedChanged(object sender, EventArgs e)
        {
            bool IsAnyUnMapped = false;
            if (gcCostCentre.DataSource != null)
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    DataTable dt = gcCostCentre.DataSource as DataTable;
                    foreach (DataRow dr in dt.Rows)
                    {
                        Int32 CCTransExits = UtilityMember.NumberSet.ToInteger(dr[gcolCCTransExists.FieldName].ToString());
                        dr.BeginEdit();
                        dr[mappingSystem.AppSchema.CostCentre.SELColumn.ColumnName] = ((chkSelectAllCC.Checked || CCTransExits == 1) ? 1 : 0);
                        if (!IsAnyUnMapped  && !chkSelectAllCC.Checked|| CCTransExits == 1)
                        {
                            IsAnyUnMapped = true;
                        }
                        dr.EndEdit();
                    }
                    dt.AcceptChanges();

                    if (!chkSelectAllCC.Checked && IsAnyUnMapped)
                    {
                        ShowMessageBox("Transaction is made for few of the Item(s), those item(s) cannot be unmapped."); 
                    }

                    dt.DefaultView.Sort = CheckSelected + " DESC," + mappingSystem.AppSchema.CostCentre.COST_CENTRE_NAMEColumn;
                    dt = dt.DefaultView.ToTable();
                    gcCostCentre.DataSource = dt;

                    ShowMAppedCCCounts();
                }
            }
        }

    }
}