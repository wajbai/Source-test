/*Added By:Michael R
 *Added On:07/08/2013
 *Purpose : To have the details of the bank.
 *Modified On: 
 *Modified Purpose:
 * */
using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;


namespace ACPP.Modules.Master
{
    public partial class frmBankView : frmFinanceBase
    {
        #region Variable Decelartion

        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Construtor

        public frmBankView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int BankId = 0;
        private int bankId
        {
            get
            {
                RowIndex = gvBankView.FocusedRowHandle;
                BankId = gvBankView.GetFocusedRowCellValue(colBankID) != null ? this.UtilityMember.NumberSet.ToInteger(gvBankView.GetFocusedRowCellValue(colBankID).ToString()) : 0;
                return BankId;
            }
            set
            {
                BankId = value;
            }
        }
        #endregion

        #region Events For Bank

        /// <summary>
        /// Load the details of bank.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmBankView_Load(object sender, EventArgs e)
        {
            SetRights();
            ApplyUserRights();
        }

        private void frmBankView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false,true);
            FetchBankDetails();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Bank.CreateBank);
            this.enumUserRights.Add(Bank.EditBank);
            this.enumUserRights.Add(Bank.DeleteBank);
            this.enumUserRights.Add(Bank.PrintBank);
            this.enumUserRights.Add(Bank.ViewBank);
            this.ApplyUserRights(ucToolBarBankView, enumUserRights, (int)Menus.Bank);
        }

        /// <summary>
        /// Add new bank details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarBankView_AddClicked(object sender, EventArgs e)
        {
            ShowBankForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Edit the  Bank Details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarBankView_EditClicked(object sender, EventArgs e)
        {
            ShowEditBankForm();
        }

        /// <summary>
        /// Edit the bank details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvBankView_DoubleClick(object sender, EventArgs e)
        {
            ShowEditBankForm();
        }

        /// <summary>
        /// To delete the bank details based on the bank id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarBankView_DeleteClicked(object sender, EventArgs e)
        {
            DeleteBankDetails();
        }

        /// <summary>
        /// Print the bank details 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarBankView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcBankView, this.GetMessage(MessageCatalog.Master.Bank.BANK_PRINT_CAPTION), PrintType.DT, gvBankView, true);
        }

        /// <summary>
        /// To show the record count .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvBankView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvBankView.RowCount.ToString();
        }

        /// <summary>
        /// Enable and Disable auto filter row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvBankView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvBankView, colBankName);
            }
        }

        /// <summary>
        /// Refresh the grid after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchBankDetails();
            gvBankView.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// Close the bank view screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarBankView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBankView_RefreshClicked(object sender, EventArgs e)
        {
            FetchBankDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load bank details.
        /// </summary>

        private void FetchBankDetails()
        {
            try
            {
                using (BankSystem bankSystem = new BankSystem())
                {
                    resultArgs = bankSystem.FetchBankDetails();
                    gcBankView.DataSource = resultArgs.DataSource.Table;
                    gcBankView.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Show Module popup bank form based on the id.
        /// </summary>

        private void ShowBankForm(int BankId)
        {
            try
            {
                frmBankAdd frmBank_Add = new frmBankAdd(BankId);
                frmBank_Add.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmBank_Add.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Delete Bank Details
        /// </summary>

        private void DeleteBankDetails()
        {
            try
            {

                if (gvBankView.RowCount != 0)
                {
                    if (bankId != 0)
                    {
                        using (BankSystem bankSystem = new BankSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = bankSystem.DeleteBankDetails(bankId);
                                if (resultArgs.Success)
                                {
                                    using (UserSystem chequesetting = new UserSystem())
                                    {
                                        chequesetting.DeleteChequeSetting(bankId);
                                    }
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchBankDetails();
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

        public void ShowEditBankForm()
        {
            if (this.isEditable)
            {
                if (gvBankView.RowCount != 0)
                {
                    if (bankId != 0)
                    {
                        ShowBankForm(bankId);
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

        private void SetRights()
        {
            int AccessRights = 0;
            using (MasterRightsSystem masterRightsSystem = new MasterRightsSystem())
            {
                masterRightsSystem.MasterName = this.Text;
                AccessRights = masterRightsSystem.MasterRights();
                if (AccessRights == (int)MasterRights.ReadOnly)
                {
                    // ucToolBarBankView.VisibleDeleteButton = ucToolBarBankView.VisibleAddButton = ucToolBarBankView.VisibleEditButton = BarItemVisibility.Never;
                }
            }
        }
        #endregion

        private void frmBankView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmBankView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditBankForm();
        }




    }
}