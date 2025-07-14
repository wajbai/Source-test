using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraPrinting;
using Bosco.DAO.Schema;

namespace ACPP.Modules.Master
{
    public partial class frmDonorBook : frmbaseAdd
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmDonorBook()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int donorId = 0;
        private int DonorId
        {
            get
            {
                RowIndex = gvDonorBook.FocusedRowHandle;
                return donorId = this.UtilityMember.NumberSet.ToInteger(gvDonorBook.GetFocusedRowCellValue(colDonorId).ToString());
            }
            set
            {
                donorId = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the Donor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDonorBook_Load(object sender, EventArgs e)
        {
            FetchDonorDetails();
        }

        /// <summary>
        /// Add New Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarDonorBook_AddClicked(object sender, EventArgs e)
        {
            ShowDonorForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Edit the Donor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarDonorBook_EditClicked(object sender, EventArgs e)
        {
            ShowDonorEditForm();
        }

        /// <summary>
        /// Edit the Donor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarDonorBook_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                using (DonorAuditorSystem donorSystem = new DonorAuditorSystem())
                {
                    if (gvDonorBook.RowCount != 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            resultArgs = donorSystem.DeleteDonorAuditorDetails(DonorId);
                            if (resultArgs.Success)
                            {
                                ucToolBarDonorBook.lblRecordDelete.Text = this.GetMessage(MessageCatalog.Master.Donor.DONOR_DELETE_SUCCESS);
                                FetchDonorDetails();
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
        /// Print the Donor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarDonorBook_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcDonorBookView, this.GetMessage(MessageCatalog.Master.Donor.DONOR_PRINT_CAPTION));
        }

        /// <summary>
        /// To enable the Autofilter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDonorBook.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
        }
        /// <summary>
        /// Edit the Donor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDonorBook_DoubleClick(object sender, EventArgs e)
        {
            ShowDonorEditForm();
        }

        /// <summary>
        /// To show record in the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDonorBook_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvDonorBook.RowCount.ToString();
        }

        /// <summary>
        /// Close the Donor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarDonorBook_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// To refresh the Grid after adding and editing the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchDonorDetails();
            gvDonorBook.FocusedRowHandle = RowIndex;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To load the Details
        /// </summary>
        private void FetchDonorDetails()
        {
            try
            {
                using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem())
                {
                    resultArgs = donaudSystem.FetchDonorDetails();
                    gcDonorBookView.DataSource = resultArgs.DataSource.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Edit Donor details based on the Id
        /// </summary>
        private void ShowDonorEditForm()
        {
            if (gvDonorBook.RowCount != 0)
            {
                ShowDonorForm(DonorId);
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        /// <summary>
        /// To show Donor based on the Id
        /// </summary>
        /// <param name="donaudId"></param>
        private void ShowDonorForm(int donaudId)
        {
            try
            {
                frmAddressBook frmAddressAdd = new frmAddressBook(donaudId);
                frmAddressAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAddressAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion
    }
}
