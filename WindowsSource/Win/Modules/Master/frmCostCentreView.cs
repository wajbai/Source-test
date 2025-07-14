using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;


namespace ACPP.Modules.Master
{
    public partial class frmCostCentreView : frmFinanceBase
    {
        #region Variable Decelartion
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructors

        public frmCostCentreView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int CostCentreId = 0;
        private int costCenterId
        {
            get
            {

                RowIndex = gvCostCentre.FocusedRowHandle;
                CostCentreId = gvCostCentre.GetFocusedRowCellValue(colCostCentreID) != null ? this.UtilityMember.NumberSet.ToInteger(gvCostCentre.GetFocusedRowCellValue(colCostCentreID).ToString()) : 0;
                return CostCentreId;
            }
            set
            {
                CostCentreId = value;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Load the cost center details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmCostCentreView_Load(object sender, EventArgs e)
        {
            ucToolBarCostCentre.DisableDeleteButton = true;
            ucToolBarCostCentre.DisableEditButton = true;
            ApplyUserRights();
        }

        private void frmCostCentreView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchCostCentreDetails();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(CostCentre.CreateCostCentre);
            this.enumUserRights.Add(CostCentre.EditCostCentre);
            this.enumUserRights.Add(CostCentre.DeleteCostCentre);
            this.enumUserRights.Add(CostCentre.PrintCostCentre);
            this.enumUserRights.Add(CostCentre.ViewCostCentre);
            this.ApplyUserRights(ucToolBarCostCentre, enumUserRights, (int)Menus.CostCentre);
        }

        /// <summary>
        /// Fire When the add button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_AddClicked(object sender, EventArgs e)
        {
            ShowCostCenterForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Edit the cost center form based on the cost center id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_EditClicked(object sender, EventArgs e)
        {
            ShowCostCenterForm();
        }

        /// <summary>
        /// Fires when grid is double clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gcCostCentre_DoubleClick(object sender, EventArgs e)
        {
            ShowCostCenterForm();
        }

        /// <summary>
        /// Delete the cost center details based on the cost center id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_DeleteClicked(object sender, EventArgs e)
        {
            try
            {

                if (gvCostCentre.RowCount != 0)
                {
                    if (costCenterId != 0)
                    {
                        using (CostCentreSystem costCentreSystem = new CostCentreSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = costCentreSystem.DeleteCostCentreDetails(costCenterId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchCostCentreDetails();
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
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Print the cost center details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcCostCentre, this.GetMessage(MessageCatalog.Master.CostCentre.COST_CENTER_PRINT_CAPTION), PrintType.DT, gvCostCentre);
        }

        /// <summary>
        /// Enable or Disable Show Filter 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvCostCentre.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvCostCentre, colCostCentreName);
            }
        }

        /// <summary>
        /// Set record counts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvCostCentre_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvCostCentre.RowCount.ToString();
        }

        /// <summary>
        /// Refresh the grie after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchCostCentreDetails();
            gvCostCentre.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// Close the cost center form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarCostCentre_RefreshClicked(object sender, EventArgs e)
        {
            FetchCostCentreDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load the cost center details
        /// </summary>

        private void FetchCostCentreDetails()
        {
            try
            {
                using (CostCentreSystem costCenterSystem = new CostCentreSystem())
                {
                    resultArgs = costCenterSystem.FetchCostCentreDetails();
                    if (resultArgs.Success)
                    {
                        gcCostCentre.DataSource = resultArgs.DataSource.Table;
                        gcCostCentre.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Show Cost center Form based on the id.
        /// </summary>
        /// <param name="costCenterId"></param>

        private void ShowCostCenterForm(int CostCenterId)
        {
            try
            {
                frmCostCentreAdd frmCostCenter = new frmCostCentreAdd(CostCenterId);
                frmCostCenter.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmCostCenter.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            //this.CostCentreId = 0;
        }

        private void ShowCostCenterForm()
        {
            if (this.isEditable)
            {
                if (gvCostCentre.RowCount != 0)
                {
                    if (costCenterId != 0)
                    {
                        ShowCostCenterForm(costCenterId);
                    }
                    else
                    {
                        if (!chkShowFilter.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }
        #endregion

        private void frmCostCentreView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmCostCentreView_EnterClicked(object sender, EventArgs e)
        {
            ShowCostCenterForm();
        }
    }
}