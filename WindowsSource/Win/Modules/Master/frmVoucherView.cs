/************************************************************************************************************************
 *                                              Form Name  :frmVoucherView.cs
 *                                              Purpose    :To view the voucher details and manipulate 
 *                                                           operations of Add,Edit,Delet,Print
 *                                              Author     : Carmel Raj M
 *                                              Created On :02-Sep-2013
 * 
 * **********************************************************************************************************************/
using System;
using System.Windows.Forms;

using Bosco.Model.UIModel.Master;
using Bosco.Utility;

namespace ACPP.Modules.Master
{
    public partial class frmVoucherView : frmFinanceBase
    {
        #region Varaiables
        private int RowIndex = 0;
        ResultArgs resultArgs = null;
        #endregion

        #region Property

        private int _VoucherId = 0;
        private int VoucherId
        {
            get
            {
                RowIndex = gvVoucherDetails.FocusedRowHandle;
                _VoucherId = gvVoucherDetails.GetFocusedRowCellValue(gvColVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvVoucherDetails.GetFocusedRowCellValue(gvColVoucherId).ToString()) : 0;
                return _VoucherId;
            }
            set
            {
                _VoucherId = value;
            }
        }

        #endregion

        #region Default Constructor

        public frmVoucherView()
        {
            InitializeComponent();

        }

        #endregion

        #region Methods

        private void ShowVoucherForm(int VoucherId = 0)
        {
            try
            {
                using (frmVoucherAdd frmVoucherAdd = new frmVoucherAdd(VoucherId))
                {
                    frmVoucherAdd.UpdateHeld += new EventHandler(this.OnUpdateHeld);
                    frmVoucherAdd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }

        }

        private void ShowEditForm()
        {
            if (this.isEditable)
            {
                if (gvVoucherDetails.RowCount != 0)
                {
                    if (VoucherId != 0)
                    {
                        ShowVoucherForm(VoucherId);
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

        private void DeleteVoucher()
        {
            try
            {

                if (gvVoucherDetails.RowCount != 0)
                {
                    if (VoucherId != 0)
                    {
                        if (VoucherId != (int)DefaultVoucherTypes.Receipt && VoucherId != (int)DefaultVoucherTypes.Payment && VoucherId != (int)DefaultVoucherTypes.Contra && VoucherId != (int)DefaultVoucherTypes.Journal)
                        {
                            using (VoucherSystem voucherSystem = new VoucherSystem())
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    voucherSystem.VoucherId = VoucherId;
                                    resultArgs = voucherSystem.DeleteVoucherDetails();
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        FetchVoucherDetails();
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_DEFAULT_CANNOT_DELETE));
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
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
        }

        private void FetchVoucherDetails()
        {
            try
            {
                using (VoucherSystem voucherSystem = new VoucherSystem())
                {
                    ResultArgs resultArgs = voucherSystem.FetchVoucerDetail();
                    gcVoucherDetails.DataSource = resultArgs.DataSource.Table;
                    gcVoucherDetails.RefreshDataSource();
                }
            }
            catch (Exception e)
            {
                MessageRender.ShowMessage(e.Message);
            }
            finally { }
        }

        #endregion

        #region Events

        private void frmVoucherView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true, true);
            //FetchVoucherDetails();
            ApplyUserRights();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Voucher.CreateVoucher);
            this.enumUserRights.Add(Voucher.EditVoucher);
            this.enumUserRights.Add(Voucher.DeleteVoucher);
            this.enumUserRights.Add(Voucher.PrintVoucher);
            this.enumUserRights.Add(Voucher.ViewVoucher);
            this.ApplyUserRights(ucToolBarVoucher, this.enumUserRights, (int)Menus.VoucherNumberDefinition);
        }

        private void ucToolBarVoucher_AddClicked(object sender, EventArgs e)
        {
            ShowVoucherForm();
        }

        private void ucToolBarVoucher_EditClicked(object sender, EventArgs e)
        {
            ShowEditForm();
        }

        private void gcVoucherDetails_DoubleClick(object sender, EventArgs e)
        {
            ShowEditForm();
        }

        private void ucToolBarVoucher_DeleteClicked(object sender, EventArgs e)
        {
            DeleteVoucher();
        }

        private void ucToolBarVoucher_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBarVoucher_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcVoucherDetails, this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_PRINT_CAPTION), PrintType.DT, gvVoucherDetails, true);
        }

        private void gvVoucherDetails_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCountResult.Text = gvVoucherDetails.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvVoucherDetails.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvVoucherDetails, gvColVoucherName);
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchVoucherDetails();
            gvVoucherDetails.FocusedRowHandle = RowIndex;
        }
        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarVoucher_RefreshClicked(object sender, EventArgs e)
        {
            FetchVoucherDetails();
        }
        #endregion

        private void frmVoucherView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmVoucherView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditForm();
        }

        private void frmVoucherView_Activated(object sender, EventArgs e)
        {
            FetchVoucherDetails();
        }
    }
}