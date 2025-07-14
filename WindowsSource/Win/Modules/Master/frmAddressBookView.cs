using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Master
{
    public partial class frmAuditorBook : frmBase   
    {
        #region VariableDeclaration
        private int RowIndex = 0;
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmAuditorBook()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int addressId = 0;
        public int AddressBookId
        {
            get
            {
                RowIndex = crdAuditorBook.FocusedRowHandle;
                return this.UtilityMember.NumberSet.ToInteger(crdAuditorBook.GetFocusedRowCellValue(colDonorId).ToString());
            }
            set
            {
                addressId = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the Auditor Details 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddressView_Load(object sender, EventArgs e)
        {
            FetchDonor();
        }

        /// <summary>
        /// Add New Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarAddressView_AddClicked(object sender, EventArgs e)
        {
            RowIndex = 0;
            ShowForm((int)AddNewRow.NewRow);
        }
      
        /// <summary>
        /// Edit the Auditor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarAddressView_EditClicked(object sender, EventArgs e)
        {
            ShowEditAddressForm();
        }

        /// <summary>
        /// Edit the Auditor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdAuditorBook_DoubleClick(object sender, EventArgs e)
        {
            ShowEditAddressForm();
        }

        /// <summary>
        /// To enable the Autofilter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged_1(object sender, EventArgs e)
        {
           // crdAuditorBook.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
        }
        /// <summary>
        /// To Show Count in the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAuditorBookView_RowCountChanged(object sender, EventArgs e)
        {
            //lblRecordCount.Text = crdAuditorBook.RowCount.ToString();
        }

        /// <summary>
        /// To Print the Auditor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarAddressView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAuditorBookView, this.GetMessage(MessageCatalog.Master.AddressBook.ADDRESS_PRINT_CAPTION),PrintType.DT);
        }

        /// <summary>
        /// To delete the Auditor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarAddressView_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                using (AddressBookSystem addressSystem = new AddressBookSystem())
                {
                    if (crdAuditorBook.RowCount != 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            resultArgs = addressSystem.DeleteAddressDetails(AddressBookId);
                            if (resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.AddressBook.ADDRESS_DELETE_SUCCESS));
                                FetchDonor();
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
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
        /// To Select the Index of records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rgAddressBookConstruct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (AddressBookSystem addressBookSystem = new AddressBookSystem())
                {
                    if (rgAddressBookConstruct.SelectedIndex == 0)
                    {
                        crdAuditorBook.CardCaptionFormat = "Donor";
                        resultArgs = addressBookSystem.FetchDonorDetails();
                        gcAuditorBookView.DataSource = resultArgs.DataSource.Table;

                    }
                    else if (rgAddressBookConstruct.SelectedIndex == 1)
                    {
                        crdAuditorBook.CardCaptionFormat = "Auditor";
                        resultArgs = addressBookSystem.FetchAuditorDetails();
                        gcAuditorBookView.DataSource = resultArgs.DataSource.Table;
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
        /// To Close the Auditor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarAddressView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// To fetch the selected Details
        /// </summary>
        public void FetchDonor()
        {
            try
            {
                using (AddressBookSystem addressBookSystem = new AddressBookSystem())
                {
                  resultArgs = addressBookSystem.FetchDonorDetails();
                  gcAuditorBookView.DataSource = resultArgs.DataSource.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To show form based on the Id
        /// </summary>
        /// <param name="AuditorId"></param>
        private void ShowForm(int AuditorId)
        {
            try
            {
                frmAddressBook frmAddressAdd = new frmAddressBook(AuditorId);
                frmAddressAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAddressAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To refresh the Grid after adding and editing details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchDonor();
            crdAuditorBook.FocusedRowHandle = RowIndex;
        }
        
       

        /// <summary>
        /// To show the edit details 
        /// </summary>
        public void ShowEditAddressForm()
        {
            if (crdAuditorBook.RowCount !=0)
            {
                ShowForm(AddressBookId);
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }
        #endregion
    }
}