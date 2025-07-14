using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.TDS;

namespace ACPP.Modules.TDS
{
    public partial class frmNatureofPaymentsView : frmFinanceBase
    {
        #region Variable Declarations
        private int RowIndex = 0;
        ResultArgs resultArgs = null;
        #endregion

        #region Constructors
        public frmNatureofPaymentsView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int naturepaymentid = 0;
        private int NaturePaymentId
        {
            get
            {

                RowIndex = gvNoPayments.FocusedRowHandle;
                naturepaymentid = gvNoPayments.GetFocusedRowCellValue(gcolNaturepaymentId) != null ? this.UtilityMember.NumberSet.ToInteger(gvNoPayments.GetFocusedRowCellValue(gcolNaturepaymentId).ToString()) : 0;
                return naturepaymentid;
            }
            set
            {
                naturepaymentid = value;
            }
        }
        #endregion

        #region Events
        private void ucNatureofPayments_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }
        /// <summary>
        /// Invoke Auditor form to edit the payment information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucNatureofPayments_EditClicked(object sender, EventArgs e)
        {
            showEditForm();
        }

        private void frmNatureofPaymentsView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmNatureofPaymentsView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchNatureofPaymentDetails();
        }

        private void frmNatureofPaymentsView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = chkShowFilter.Checked ? false : true;
        }

        /// <summary>
        /// To enable auto filter row in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvNoPayments.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvNoPayments, gcolPaymentName);
            }
        }
        /// <summary>
        /// To show the number of records available in the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvNoPayments_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvNoPayments.RowCount.ToString();
        }
        /// <summary>
        /// To delete the selected payment information from the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param
        private void ucNatureofPayments_DeleteClicked(object sender, EventArgs e)
        {
            DeleteDetails();
        }

        private void gvNoPayments_DoubleClick(object sender, EventArgs e)
        {
            showEditForm();
        }

        private void ucNatureofPayments_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucNatureofPayments_RefreshClicked(object sender, EventArgs e)
        {
            FetchNatureofPaymentDetails();
        }

        private void ucNatureofPayments_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcNatureofPayments, this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_NATUREPAYMENT_PRINT_CAPTION), PrintType.DT, gvNoPayments);
        }


        #endregion

        #region Methods
        private void ShowForm(int naturepaymentid)
        {
            try
            {
                frmNatureofPayments frmnatureofpayment = new frmNatureofPayments(naturepaymentid);
                frmnatureofpayment.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmnatureofpayment.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        public void showEditForm()
        {
            if (gvNoPayments.RowCount != 0)
            {
                if (NaturePaymentId != 0)
                {
                    ShowForm(NaturePaymentId);
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

        private void FetchNatureofPaymentDetails()
        {
            try
            {
                using (NatureofPaymentsSystem paymentsystem = new NatureofPaymentsSystem())
                {
                    resultArgs = paymentsystem.FetchNatureofPaymentsSections();
                    gcNatureofPayments.DataSource = resultArgs.DataSource.Table;
                    gcNatureofPayments.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void DeleteDetails()
        {
            try
            {
                if (gvNoPayments.RowCount != 0)
                {
                    if (NaturePaymentId != 0)
                    {
                        if (IsActiveNatureNOP() == 0)
                        {
                            using (NatureofPaymentsSystem paymentSystem = new NatureofPaymentsSystem())
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    paymentSystem.NatureofPaymentId = NaturePaymentId;
                                    resultArgs = paymentSystem.DeleteNatureofpaymentDetails();
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        FetchNatureofPaymentDetails();
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox("Cannot delete.Voucher is made for this Nature of Payment.");
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
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private int IsActiveNatureNOP()
        {
            int Count = 0;
            using (NatureofPaymentsSystem NaturePayments = new NatureofPaymentsSystem())
            {
                NaturePayments.NatureofPaymentId = NaturePaymentId;
                Count = NaturePayments.IsActiveNOP();
            }
            return Count;
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchNatureofPaymentDetails();
            gvNoPayments.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// Check internet connection 
        /// </summary>
        /// <returns></returns>
        public bool CheckInternetConnection()
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

            System.Net.NetworkInformation.PingReply pingStatus = ping.Send("www.google.com");

            if (pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(TDSNatureofPayments.CreateNatureofPayments);
            this.enumUserRights.Add(TDSNatureofPayments.EditNatureofPayments);
            this.enumUserRights.Add(TDSNatureofPayments.DeleteNatureofPayments);
            this.enumUserRights.Add(TDSNatureofPayments.PrintNatureofPayments);
            this.enumUserRights.Add(TDSNatureofPayments.ViewNatureofPayments);
            this.ApplyUserRights(ucNatureofPayments, enumUserRights, (int)Menus.TDSNatureofPayments);
        }
        #endregion

        private void frmNatureofPaymentsView_EnterClicked(object sender, EventArgs e)
        {
            showEditForm();
        }

    }
}