using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.Inventory;
using ACPP.Modules.Data_Utility;

namespace ACPP.Modules.Inventory
{
    public partial class frmCustodiansView : frmFinanceBase
    {
        #region Variable Declearation
        private int RowIndex = 0;
        ResultArgs resultArgs = null;
        FinanceModule FrmModule;
        int custodiansid = 0;
        #endregion

        #region Constructors
        public frmCustodiansView()
        {
            InitializeComponent();
        }

        public frmCustodiansView(FinanceModule module)
            : this()
        {
            FrmModule = module;
        }
        #endregion

        #region Properties
        public int CustodiansId
        {
            get
            {
                RowIndex = gvCustodiansView.FocusedRowHandle;
                custodiansid = gvCustodiansView.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvCustodiansView.GetFocusedRowCellValue(colId).ToString()) : 0;
                return custodiansid;
            }
            set
            {
                custodiansid = value;
            }
        }
        #endregion

        #region Events

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvCustodiansView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvCustodiansView, gcColName);
            }
        }

        private void frmCustodiansView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadCustodiansDetails();
            if (FrmModule.ToString().Equals("Asset"))
                ApplyUserRights();
            else
                ApplyStockUserRights();
        }

        private void gcCustodiansView_DoubleClick(object sender, EventArgs e)
        {
            ShowCustodiansform();
        }

        private void frmCustodiansView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvCustodiansView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvCustodiansView.RowCount.ToString();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadCustodiansDetails();
            gvCustodiansView.FocusedRowHandle = RowIndex;
        }

        private void ucCustodiansView_DeleteClicked(object sender, EventArgs e)
        {
            DeleteCustodiansDetails();
        }

        private void ucCustodiansView_AddClicked(object sender, EventArgs e)
        {
            showCustodiansform((int)AddNewRow.NewRow);
        }

        private void ucCustodiansView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucCustodiansView_EditClicked(object sender, EventArgs e)
        {
            ShowCustodiansform();
        }

        private void ucCustodiansView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcCustodiansView, this.GetMessage(MessageCatalog.Asset.AssetCustodians.ASSETCUSTODIANS_PRINT_CAPTION), PrintType.DT, gvCustodiansView, true);
        }

        private void ucCustodiansView_RefreshClicked(object sender, EventArgs e)
        {
            LoadCustodiansDetails();
        }

        private void frmCustodiansView_EnterClicked(object sender, EventArgs e)
        {
            ShowCustodiansform();
        }

        #endregion

        #region Methods
        public void LoadCustodiansDetails()
        {
            try
            {
                using (CustodiansSystem CustodiansSystem = new CustodiansSystem())
                {
                    resultArgs = CustodiansSystem.FetchAllCustodiansDetails();
                    if (resultArgs.Success)
                    {
                        gcCustodiansView.DataSource = resultArgs.DataSource.Table;
                        gcCustodiansView.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Custodian.CreateCustodian);
            this.enumUserRights.Add(Custodian.EditCustodian);
            this.enumUserRights.Add(Custodian.DeleteCustodian);
            this.enumUserRights.Add(Custodian.ViewCustodian);
            this.ApplyUserRights(ucCustodiansView, enumUserRights, (int)Menus.Custodian);
        }


        private void ApplyStockUserRights()
        {
            this.enumUserRights.Add(StockCustodian.CreateStockCustodian);
            this.enumUserRights.Add(StockCustodian.EditStockCustodian);
            this.enumUserRights.Add(StockCustodian.DeleteStockCustodian);
            this.enumUserRights.Add(StockCustodian.ViewStockCustodian);
            this.ApplyUserRights(ucCustodiansView, enumUserRights, (int)Menus.StockCustodian);
        }
        public void DeleteCustodiansDetails()
        {
            try
            {
                if (gvCustodiansView.RowCount > 0)
                {
                    if (CustodiansId > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            if (MappedCustodian())
                            {
                                using (CustodiansSystem objCustodiansSystem = new CustodiansSystem())
                                {
                                    objCustodiansSystem.CustodiansId = CustodiansId;

                                    resultArgs = objCustodiansSystem.DeleteCustodiansDetails();
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        LoadCustodiansDetails();
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_FAILURE));
                                    }
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
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private bool MappedCustodian()
        {
            bool ismapped = true;
            int count = 0;
            using (CustodiansSystem custodian = new CustodiansSystem())
            {
                custodian.CustodiansId = CustodiansId;
                count = custodian.FetchMappedCustodian();
                if (count > 0)
                {
                    ismapped = false;
                    this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Common.COST_CENTRE_COST_CATEGORY_UNMAP));
                }
            }
            return ismapped;
        }

        private void ShowCustodiansform()
        {
            if (this.isEditable)
            {
                if (gvCustodiansView.RowCount != 0)
                {
                    if (CustodiansId > 0)
                    {
                        showCustodiansform(CustodiansId);
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

        private void showCustodiansform(int custudianId)
        {
            try
            {
                frmCustodiansAdd frmcustudians = new frmCustodiansAdd(custudianId);
                frmcustudians.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmcustudians.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        #endregion

        private void ucCustodiansView_DownloadExcel(object sender, EventArgs e)
        {
            using (frmExcelSupport excelSupport = new frmExcelSupport("Custudian", MasterImport.Custudian))
            {
                excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelSupport.ShowDialog();
            }
        }

        private void frmCustodiansView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            LoadCustodiansDetails();
        }
    }
}
