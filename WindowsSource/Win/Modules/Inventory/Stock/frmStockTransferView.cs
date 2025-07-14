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
using Bosco.Model.Inventory.Stock;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockTransferView : frmFinanceBase
    {
        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        private int RowIndex = 0;
        #endregion

        #region Properties
        public int ProjectId
        {
            get
            {
                int ProId = 0;
                if (glkpProject.EditValue != null)
                {
                    ProId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                }
                return ProId;
            }

        }

        private int TransferredItemId
        {
            get
            {
                int TransferredId = 0;
                if (gvTransferView.RowCount > 0 && gvTransferView.GetFocusedRowCellValue(colTransferredItemId) != null)
                {
                    TransferredId = UtilityMember.NumberSet.ToInteger(gvTransferView.GetFocusedRowCellValue(colTransferredItemId).ToString());
                }
                return TransferredId;
            }
        }

        private int GetEditId
        {
            get
            {
                int EditId = 0;
                if (gvTransferView.RowCount > 0 && gvTransferView.GetFocusedRowCellValue(colEditId) != null)
                {
                    RowIndex = gvTransferView.FocusedRowHandle;
                    EditId = UtilityMember.NumberSet.ToInteger(gvTransferView.GetFocusedRowCellValue(colEditId).ToString());
                }
                return EditId;
            }
        }
        private int RecentProjectId { get; set; }
        private string RecentDate { get; set; }

        #endregion

        #region Constructor
        public frmStockTransferView()
        {
            InitializeComponent();
        }
        public frmStockTransferView(int Pid, string Rdate)
            : this()
        {
            RecentProjectId = Pid;
            RecentDate = Rdate;
        }
        #endregion

        #region Events
        private void frmStockTransferView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadDefaults();
            ApplyUserRights();
        }

        private void ucStockTransfer_AddClicked(object sender, EventArgs e)
        {
            ShowTransferredForm();
        }

        private void ucStockTransfer_EditClicked(object sender, EventArgs e)
        {
            //ShowEditForm();
        }

        private void gcTransferView_DoubleClick(object sender, EventArgs e)
        {
            // ShowEditForm();
        }

        private void ucStockTransfer_DeleteClicked(object sender, EventArgs e)
        {
            if (gvTransferView.RowCount != 0)
            {
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (StockItemTransferSystem ItemTransferred = new StockItemTransferSystem())
                    {
                        ItemTransferred.TransferredItemId = TransferredItemId;
                        resultArgs = ItemTransferred.DelelteTransferedItem();
                        if (resultArgs != null)
                        {
                            ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            FetchItemDetails();
                        }
                        else
                        {
                            ShowMessageBox(resultArgs.Message);
                        }
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void ucStockTransfer_RefreshClicked(object sender, EventArgs e)
        {
            FetchItemDetails();
        }

        private void ucStockTransfer_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcTransferView, this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_PRINT_CAPTION), PrintType.DT, gvTransferView, true);
        }

        private void ucStockTransfer_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchItemDetails();
            gvTransferView.FocusedRowHandle = RowIndex;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTransferView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTransferView, colStockName);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(deDateFrom.Text))
            {
                if (!string.IsNullOrEmpty(deDateTo.Text))
                {
                    if (DateTime.Compare(deDateFrom.DateTime, deDateTo.DateTime) <= 0)
                    {
                        FetchItemDetails();
                    }
                    else { ShowMessageBox("Date From should be less than Date To"); }
                }
                else { ShowMessageBox("Date To is empty"); }
            }
            else { ShowMessageBox("Date From is empty"); }
        }
        #endregion

        #region Methods
        private void LoadDefaults()
        {
            LoadDefaultDate();
            LoadProject();
            FetchItemDetails();
        }

        private void LoadDefaultDate()
        {
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.DateTime = deDateFrom.DateTime.AddYears(1).AddDays(-1);

            //deDateFrom.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //deDateFrom.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //deDateTo.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //deDateTo.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //deDateFrom.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false);
            //deDateTo.DateTime = deDateFrom.DateTime.AddYears(1).AddDays(-1);
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
                        glkpProject.EditValue = (RecentProjectId != 0) ? RecentProjectId : glkpProject.Properties.GetKeyValue(0);

                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowMessageBox(Ex.Message);
            }
            finally { }
        }

        private void FetchItemDetails()
        {
            try
            {
                using (StockItemTransferSystem fetchItemDetails = new StockItemTransferSystem())
                {
                    fetchItemDetails.ProjectId = ProjectId;
                    fetchItemDetails.DateFrom = deDateFrom.DateTime;
                    fetchItemDetails.DateTo = deDateTo.DateTime;
                    resultArgs = fetchItemDetails.FetchTransferredItemDetails();
                    if (resultArgs != null)
                    {
                        gcTransferView.DataSource = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        ShowMessageBox(resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void ShowEditForm()
        {
            if (gvTransferView.RowCount != 0)
            {
                if (GetEditId != 0)
                {
                    ShowTransferredForm(GetEditId);
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

        private void ShowTransferredForm(int EditId = 0)
        {
            try
            {
                using (frmStockTransferAdd frmStocktransferadd = new frmStockTransferAdd(glkpProject.Text, this.UtilityMember.DateSet.ToDate(RecentDate, false)))
                {
                    frmStocktransferadd.ProjectId = ProjectId;
                    frmStocktransferadd.EditId = EditId;
                    frmStocktransferadd.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmStocktransferadd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        #endregion

        private void frmStockTransferView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmStockTransferView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadDefaults();
        }

        /// <summary>
        /// Stock Views
        /// </summary>
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(StockItemTransfer.CreateStockItemTransfer);
            this.enumUserRights.Add(StockItemTransfer.EditStockItemTransfer);
            this.enumUserRights.Add(StockItemTransfer.DeleteStockItemTransfer);
            this.enumUserRights.Add(StockItemTransfer.ViewStockItemTransfer);
            this.ApplyUserRights(ucStockTransfer, enumUserRights, (int)Menus.StockItemTransfer);
        }
    }
}