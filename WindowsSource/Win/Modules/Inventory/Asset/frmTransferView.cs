 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACPP.Modules.Inventory.Asset;
using Bosco.Model.Inventory.Asset;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.DAO;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Asset.Transactions
{
    public partial class frmTransferView : frmFinanceBase
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        int RowIndex = 0;
        int transferID = 0;
        int itemID = 0;
        #endregion

        #region Constructors
        public frmTransferView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int TransferId
        {
            get
            {
                RowIndex = gvTransferView.FocusedRowHandle;
                transferID = gvTransferView.GetFocusedRowCellValue(colTransferId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransferView.GetFocusedRowCellValue(colTransferId).ToString()) : 0;
                return transferID;
            }
            set
            {
                transferID = value;
            }
        }

        public int ItemId
        {
            get
            {
                RowIndex = gvTransferSubView.FocusedRowHandle;
                itemID = gvTransferView.GetFocusedRowCellValue(colItemid) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransferView.GetFocusedRowCellValue(colItemid).ToString()) : 0;
                return itemID;
            }
            set
            {
                itemID = value;
            }
        }

        #endregion

        #region Events

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadTransferDetails();
        }

        private void frmTransferView_Load(object sender, EventArgs e)
        {
            LoadProjectsDetails();
            LoadTransferDetails();
            deFromDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTodate.DateTime = deFromDate.DateTime.AddMonths(1).AddDays(-1);
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTransferView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTransferView, colAssetName);
            }
        }

        private void frmTransferView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvTransferView_RowCountChanged(object sender, EventArgs e)
        {
            lblNumbercount.Text = gvTransferView.RowCount.ToString();
        }

        private void ucTransfer_AddClicked(object sender, EventArgs e)
        {
            ShowTransferForm((int)AddNewRow.NewRow);
        }

        private void ucTransfer_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucTransfer_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcTransferView, this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_PRINT_CAPTION), PrintType.DS, gvTransferView, true);
        }

        private void ucTransfer_RefreshClicked(object sender, EventArgs e)
        {
            LoadTransferDetails();
        }

        #endregion

        #region Methods
        public void LoadTransferDetails()
        {
            try
            {
                DataSet dsAssetTransfer = new DataSet();
                using (TransferVoucherSystem transferSystem = new TransferVoucherSystem())
                {
                    transferSystem.FromDate = deFromDate.DateTime;
                    transferSystem.ToDate = deTodate.DateTime;
                    dsAssetTransfer = transferSystem.FetchAllTransferDetails();
                    if (dsAssetTransfer != null && dsAssetTransfer.Tables.Count > 0)
                    {
                        gcTransferView.DataSource = dsAssetTransfer;
                        gcTransferView.DataMember = "Master";
                        gcTransferView.RefreshDataSource();
                    }
                    else
                    {
                        gcTransferView.DataSource = null;
                        gcTransferView.RefreshDataSource();
                    }
                    gvTransferView.FocusedRowHandle = 0;
                    gvTransferView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void ShowTransferEdit()
        {
            if (gvTransferView.RowCount != 0)
            {
                if (TransferId != 0)
                {
                    ShowTransferForm(TransferId);
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

        private void ShowTransferForm(int transferID)
        {
            try
            {
                int ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
                string ProjectName = glkpProjects.Text.ToString();
                frmTransfer TransferAdd = new frmTransfer(transferID, ProjectId, ProjectName);
                TransferAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                TransferAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        private void DeleteTransferDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvTransferView.RowCount != 0)
                {
                    if (transferID != 0)
                    {
                        using (TransferVoucherSystem transferSystem = new TransferVoucherSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                transferSystem.TransferId = TransferId;
                                resultArgs = transferSystem.DeleteTranferDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadTransferDetails();
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

        public void LoadProjectsDetails()
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
                        glkpProjects.EditValue = glkpProjects.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        #endregion

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadTransferDetails();
        }

        private void gcTransferView_Click(object sender, EventArgs e)
        {

        }
    }
}
