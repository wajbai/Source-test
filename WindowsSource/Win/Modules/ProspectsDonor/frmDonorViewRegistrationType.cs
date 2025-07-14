using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.Donor;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorViewRegistrationType : frmFinanceBase
    {
        #region Constructor
        public frmDonorViewRegistrationType()
        {
            InitializeComponent();
        }
        #endregion

        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Properties
        private int RegTypeid = 0;
        public int RegTypeID
        {
            get
            {
                RowIndex = gvRegistrationtype.FocusedRowHandle;
                RegTypeid = gvRegistrationtype.GetFocusedRowCellValue(colRegistrationTypeID) != null ? this.UtilityMember.NumberSet.ToInteger(gvRegistrationtype.GetFocusedRowCellValue(colRegistrationTypeID).ToString()) : 0;
                return RegTypeid;
            }
            set
            {
                RegTypeid = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Load Donor Registration Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDonorViewRegistrationType_Load(object sender, EventArgs e)
        {
            LoadRegistraitonType();
            SetTitle();
        }

        /// <summary>
        /// Show Filter the view form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilet_CheckedChanged(object sender, EventArgs e)
        {
            gvRegistrationtype.OptionsView.ShowAutoFilterRow = chkShowFilet.Checked;
            if (chkShowFilet.Checked)
            {
                this.SetFocusRowFilter(gvRegistrationtype, colRegistrationType);
            }
        }

        /// <summary>
        /// Row count the gird form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRegistrationtype_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvRegistrationtype.RowCount.ToString();
        }

        /// <summary>
        /// Add the Registration Type Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegistrationToolBar_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegistrationToolBar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// To click the button for Edit the Registration Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegistrationToolBar_EditClicked(object sender, EventArgs e)
        {
            ShowForm(RegTypeID);
        }

        /// <summary>
        /// Double click on the gird to edit the value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRegistrationtype_DoubleClick(object sender, EventArgs e)
        {
            ShowForm(RegTypeID);
        }

        /// <summary>
        /// Delete the Registration Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegistrationToolBar_DeleteClicked(object sender, EventArgs e)
        {
            DeleteRegistrationType();
        }

        /// <summary>
        /// Load the Registration Type Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegistrationToolBar_RefreshClicked(object sender, EventArgs e)
        {
            LoadRegistraitonType();
        }

        /// <summary>
        /// Print the Registration Type Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegistrationToolBar_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcRegistrationType, "Donor Registration Type", PrintType.DS, gvRegistrationtype, true);
            PrintGridViewDetails(gcRegistrationType,this.GetMessage(MessageCatalog.Networking.NetworkingDonorRegistrationType.NETWORKING_DONOR_REGISTRATION_PRINT_CAPTION), PrintType.DS, gvRegistrationtype, true);
        }

        private void frmDonorViewRegistrationType_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }

        #endregion

        #region Methods
        /// <summary>
        /// After Save Load the Registration Type Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadRegistraitonType();
        }

        /// <summary>
        /// Show form for Edit
        /// </summary>
        /// <param name="RegTypeID"></param>
        private  void ShowForm(int RegTypeID)
        {
            try
            {
                frmDonorRegistrationType frmDonorRegistrationType = new frmDonorRegistrationType(RegTypeID);
                frmDonorRegistrationType.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmDonorRegistrationType.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Networking.NetworkingDonorRegistrationType.NETWORKING_DONOR_REGISTRATION_VIEW_CAPTION);
        }

        /// <summary>
        /// Load Registration Type Details
        /// </summary>
        private void LoadRegistraitonType()
        {
            try
            {
                using (DonorRegistrationTypeSystem donorRegistrationType = new DonorRegistrationTypeSystem())
                {
                    resultArgs = donorRegistrationType.FetchAllRegistrationTypeDetails();
                    gcRegistrationType.DataSource = resultArgs.DataSource.Table;
                    gcRegistrationType.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Delete the Registration Type
        /// </summary>
        private void DeleteRegistrationType()
        {
            try
            {
                if (gvRegistrationtype.RowCount > 0 && gvRegistrationtype != null)
                {
                    if (RegTypeID > 0)
                    {
                        using (DonorRegistrationTypeSystem donorRegistrationType = new DonorRegistrationTypeSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                donorRegistrationType.RegTypeID = RegTypeID;
                                resultArgs = donorRegistrationType.DeleteRegistrationTypeDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadRegistraitonType();
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
        #endregion
    }
}