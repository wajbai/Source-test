using System;
using System.Windows.Forms;

using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.Master
{
    public partial class frmCostCentreCategoryView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        private int EditCostCentreCategoryView = 0;
        #endregion

        #region Constructor
        public frmCostCentreCategoryView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int CostCentreCategoryId = 0;
        public int costCentreCategoryId
        {
            get
            {
                RowIndex = gvCostCentreCategory.FocusedRowHandle;
                CostCentreCategoryId = gvCostCentreCategory.GetFocusedRowCellValue(colCostCategoryId) != null ? this.UtilityMember.NumberSet.ToInteger(gvCostCentreCategory.GetFocusedRowCellValue(colCostCategoryId).ToString()) : 0;
                return CostCentreCategoryId;
            }
            set
            {
                CostCentreCategoryId = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// To Get the Project Catogory Details
        /// </summary>
        public void GetCostCentreCategoryDetails()
        {
            try
            {
                using (CostCentreCategorySystem Costcentrecategorysystem = new CostCentreCategorySystem())
                {
                    resultArgs = Costcentrecategorysystem.FetchCostCentreCatogoryDetails();
                    gcCostCentreCategory.DataSource = resultArgs.DataSource.Table;
                    gcCostCentreCategory.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }


        private void ApplyUserRights()
        {
            this.enumUserRights.Add(CostCentreCategory.CreateCostCentreCategory);
            this.enumUserRights.Add(CostCentreCategory.EditCostCentreCategory);
            this.enumUserRights.Add(CostCentreCategory.DeleteCostCentreCategory);
            this.enumUserRights.Add(CostCentreCategory.PrintCostCentreCategory);
            this.enumUserRights.Add(CostCentreCategory.ViewCostCentreCategory);
            this.ApplyUserRights(ucToolBarCostCentreCategory, this.enumUserRights, (int)Menus.CostCentreCategory);
        }
        /// <summary>
        /// To Show form based on the Selection
        /// </summary>
        /// <param name="projectCategoryId"></param>
        public void ShowForm(int CostCentreCategoryId)
        {
            try
            {
                frmCostCentreCategoryAdd frmCategory = new frmCostCentreCategoryAdd(CostCentreCategoryId);
                frmCategory.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmCategory.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// To refresh the Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {

            GetCostCentreCategoryDetails();
            gvCostCentreCategory.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// To set the Id of Project Category
        /// </summary>
        public void EditCostCentreCategory()
        {
            try
            {
                if (this.isEditable)
                {
                    if (gvCostCentreCategory.RowCount != 0)
                    {
                        if (costCentreCategoryId != 0)
                        {
                            ShowForm(costCentreCategoryId);
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
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        #region Events
        private void frmCostCentreCategoryView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmCostCentreCategoryView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            GetCostCentreCategoryDetails();
        }

        private void ucToolBarCostCentreCategory_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        private void ucToolBarCostCentreCategory_EditClicked(object sender, EventArgs e)
        {
            EditCostCentreCategory();
        }

        private void gvCostCentreCategory_DoubleClick(object sender, EventArgs e)
        {
            EditCostCentreCategory();
        }

        private void ucToolBarCostCentreCategory_DeleteClicked(object sender, EventArgs e)
        {
            try
            {

                if (gvCostCentreCategory.RowCount != 0)
                {
                    if (costCentreCategoryId != 0)
                    {
                        using (CostCentreCategorySystem Costcentrecategorysystem = new CostCentreCategorySystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (Costcentrecategorysystem.CheckCostcentreCostcategoryExist(costCentreCategoryId) == 0)
                                {
                                    resultArgs = Costcentrecategorysystem.DeleteCostCentreCatogoryDetails(costCentreCategoryId);
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        GetCostCentreCategoryDetails();
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Common.COST_CENTRE_COST_CATEGORY_UNMAP));
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

        private void ucToolBarCostCentreCategory_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcCostCentreCategory, this.GetMessage(MessageCatalog.Master.CostCentreCategory.COST_CENTRE_CATEGORY_PRINT_CAPTION), PrintType.DT, gvCostCentreCategory);
        }

        private void gvCostCentreCategory_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvCostCentreCategory.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvCostCentreCategory.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvCostCentreCategory, colCostCentrecategoryName);
            }
        }

        private void ucToolBarCostCentreCategory_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBarCostCentreCategory_RefreshClicked(object sender, EventArgs e)
        {
            GetCostCentreCategoryDetails();
        }
        #endregion

        private void frmCostCentreCategoryView_EnterClicked(object sender, EventArgs e)
        {
            EditCostCentreCategory();
        }
    }
}