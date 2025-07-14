using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmInsurancePlanView : frmFinanceBase
    {
        # region Variable Declaration
        private int RowIndex = 0;
        private int insuranceTypeId = 0;
        #endregion

        #region Construction
        public frmInsurancePlanView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int InsuranceTypeId
        {
            get
            {
                RowIndex = gvInsuranceTypeView.FocusedRowHandle;
                insuranceTypeId = gvInsuranceTypeView.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsuranceTypeView.GetFocusedRowCellValue(colId).ToString()) : 0;
                return insuranceTypeId;
            }
        }
        #endregion

        #region Events
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadInsuranceDetails();
            gvInsuranceTypeView.FocusedRowHandle = RowIndex;
        }
        private void ucInsuranceType_AddClicked(object sender, EventArgs e)
        {
            ShowInsuranceDetails((int)AddNewRow.NewRow);
        }

        private void ucInsuranceType_DeleteClicked(object sender, EventArgs e)
        {
            DeleteInsuranceDetails();
        }

        private void ucInsuranceType_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucInsuranceType_EditClicked(object sender, EventArgs e)
        {
            EditInsuranceDetails();
        }

        private void ucInsuranceType_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcInsuranceTypeView, this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_PRINT_CAPTION), PrintType.DT, gvInsuranceTypeView, true);
        }

        private void ucInsuranceType_RefreshClicked(object sender, EventArgs e)
        {
            LoadInsuranceDetails();
        }
        private void gcInsuranceTypeView_DoubleClick(object sender, EventArgs e)
        {
            EditInsuranceDetails();
        }

        private void gvInsuranceTypeView_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvInsuranceTypeView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvInsuranceTypeView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvInsuranceTypeView, colCompany);
            }
        }

        private void frmInsuranceTypeView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmInsuranceTypeView_EnterClicked(object sender, EventArgs e)
        {
            EditInsuranceDetails();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load Insurance details.
        /// </summary>
        private void frmInsuranceTypeView_Load(object sender, EventArgs e)
        {
            LoadInsuranceDetails();
            SetTitle();
        }

        public void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Asset.Insurance.INS_VIEW_CAPTION);
        }
        /// <summary>
        /// Load Insurance details.
        /// </summary>
        private void LoadInsuranceDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                using (AssetInsurancePlanSystem InsuranceSystem = new AssetInsurancePlanSystem())
                {
                    resultArgs = InsuranceSystem.FetchAllInsuranceDetials();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcInsuranceTypeView.DataSource = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        gcInsuranceTypeView.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        /// <summary>
        /// Load Insurance details for Edit.
        /// </summary>
        private void EditInsuranceDetails()
        {
            if (gvInsuranceTypeView.RowCount != 0)
            {
                if (InsuranceTypeId > 0)
                {
                    ShowInsuranceDetails(InsuranceTypeId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        /// <summary>
        /// Show Insurance details.
        /// </summary>
        private void ShowInsuranceDetails(int Insurance_Id)
        {
            try
            {
                frmInsuranceAddPlan Insuranceform = new frmInsuranceAddPlan(Insurance_Id);
                Insuranceform.UpdateHeld += new EventHandler(OnUpdateHeld);
                Insuranceform.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Delete Insurance details.
        /// </summary>
        private void DeleteInsuranceDetails()
        {
            try
            {
                ResultArgs resultArgs = null;

                if (gvInsuranceTypeView.RowCount != 0)
                {
                    if (InsuranceTypeId > 0)
                    {
                        using (AssetInsurancePlanSystem InsuranceSystem = new AssetInsurancePlanSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = InsuranceSystem.DeleteInsuranceDetails(InsuranceTypeId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadInsuranceDetails();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        #endregion
    }
}
