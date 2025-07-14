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
using Bosco.Model.ASSET;
using Bosco.Model.Asset;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmInsuranceTypeView : frmBase
    {
        # region Variable Declaration
        private int RowIndex = 0;
        private int Insurance_Id = 0;
        #endregion

        #region Construction
        public frmInsuranceTypeView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int insurance_Id
        {
            get
            {
                RowIndex = gvInsuranceTypeView.FocusedRowHandle;
                Insurance_Id = gvInsuranceTypeView.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsuranceTypeView.GetFocusedRowCellValue(colId).ToString()) : 0;
                return Insurance_Id;
            }
            set
            {
                Insurance_Id = value;
            }
        }
        #endregion

        #region Events
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadInsuranceDetaisl();
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
           PrintGridViewDetails(gcInsuranceTypeView, this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_PRINT_CAPTION), PrintType.DT,gvInsuranceTypeView, true);
        }
        private void ucInsuranceType_RefreshClicked(object sender, EventArgs e)
        {
            LoadInsuranceDetaisl();
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
                this.SetFocusRowFilter(gvInsuranceTypeView, colName);
            }
        }

        private void frmInsuranceTypeView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load Insurance details.
        /// </summary>
        private void frmInsuranceTypeView_Load(object sender, EventArgs e)
        {
            LoadInsuranceDetaisl();
        }

        /// <summary>
        /// Load Insurance details.
        /// </summary>
        private void LoadInsuranceDetaisl()
        {
            try
            {
                ResultArgs resultArgs = null;
                using (AssetInsuranceTypeSystem InsuranceSystem = new AssetInsuranceTypeSystem())
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
            if (gvInsuranceTypeView.RowCount!= 0)
            {
                if (insurance_Id != 0)
                {
                    ShowInsuranceDetails(Insurance_Id);
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
                frmInsuranceTypeAdd Insuranceform = new frmInsuranceTypeAdd(Insurance_Id);
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
                    if (insurance_Id != 0)
                    {
                        using (AssetInsuranceTypeSystem InsuranceSystem = new AssetInsuranceTypeSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = InsuranceSystem.DeleteInsuranceDetails(Insurance_Id);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadInsuranceDetaisl(); 
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
