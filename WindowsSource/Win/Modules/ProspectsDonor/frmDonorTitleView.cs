using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.Donor;


namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorTitleView :frmFinanceBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors
        public frmDonorTitleView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int titleId = 0;
        public int TitleID
        {
            get
            {
                RowIndex = gvDonorTitleView.FocusedRowHandle;
                titleId = gvDonorTitleView.GetFocusedRowCellValue(colTitleId) != null ? this.UtilityMember.NumberSet.ToInteger(gvDonorTitleView.GetFocusedRowCellValue(colTitleId).ToString()) : 0;
                return titleId;
            }
            set
            {
                titleId = value;
            }
        }

        #endregion
        
        #region Methods
        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Networking.NetworkingDonorTitle.NETWORKING_DONOR_TITLE_VIEW_CAPTION);
        }

        private void ShowDonorTitleForm(int titleId)
        {
            try
            {
                frmDonorTitleAdd donorTitle_Add = new frmDonorTitleAdd(titleId);
                donorTitle_Add.UpdateHeld += new EventHandler(OnUpdateHeld);
                donorTitle_Add.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
       
        public void EditDonorTitle()
        {
            try
            {
                if (gvDonorTitleView.RowCount != 0)
                {
                    if (TitleID != 0)
                    {
                        ShowDonorTitleForm(TitleID);
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
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void DeleteDonorTitle()
        {
            try
            {

                if (gvDonorTitleView.RowCount != 0)
                {
                    if (TitleID != 0)
                    {
                        using (DonorTitleSystem donorTitleSystem = new DonorTitleSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                donorTitleSystem.TitleId =TitleID ;
                                resultArgs = donorTitleSystem.DeleteDonorTitle();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadDonorTitleDetails();
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
        /// <summary>
        /// To Load the records        /// </summary>
        private void LoadDonorTitleDetails()
        {
            try
            {
                using (DonorTitleSystem donorTitleSystem = new DonorTitleSystem())
                {
                    resultArgs = donorTitleSystem.FetchDonorTitleDetails();
                    gcDonorTitleView.DataSource = resultArgs.DataSource.Table;
                    gcDonorTitleView.RefreshDataSource();
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
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadDonorTitleDetails();
            gvDonorTitleView.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// To add the records
        /// </summary>
        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowDonorTitleForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// To Midify the records
        /// </summary>
        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            EditDonorTitle();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcDonorTitleView, "Donor Title Details", PrintType.DT, gvDonorTitleView, true);
            PrintGridViewDetails(gcDonorTitleView,this.GetMessage(MessageCatalog.Networking.NetworkingDonorTitle.NETWORKING_DONOR_TITLE_PRINT_CAPTION), PrintType.DT, gvDonorTitleView, true);
        }

        /// <summary>
        /// To Delete the record
        /// </summary>

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            DeleteDonorTitle();
        }

        /// <summary>
        /// To Refresh the records
        /// </summary>
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            LoadDonorTitleDetails();
        }

        /// <summary>
        /// To Modify the records
        /// </summary>
        private void gvDonorTitleView_DoubleClick(object sender, EventArgs e)
        {
            EditDonorTitle();
        }

        /// <summary>
        /// To count the records
        /// </summary>
        private void gvDonorTitleView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvDonorTitleView.RowCount.ToString();
        }

        /// <summary>
        /// To Filter the records
        /// </summary>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDonorTitleView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDonorTitleView, colTitle);
            }
        }
        /// <summary>
        /// To Fetch the records
        /// </summary>
        private void frmDonorTitleView_Load(object sender, EventArgs e)
        {
            LoadDonorTitleDetails();
            SetTitle();
        }

        /// <summary>
        /// To close the Form
        /// </summary>
        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDonorTitleView_Enter(object sender, EventArgs e)
        {
            LoadDonorTitleDetails();
        }

        private void frmDonorTitleView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false,true);
        }

        private void frmDonorTitleView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        #endregion

        private void gcDonorTitleView_Click(object sender, EventArgs e)
        {

        }
    }
}