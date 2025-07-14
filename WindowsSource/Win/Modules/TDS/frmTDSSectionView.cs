using System;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.TDS;

namespace ACPP.Modules.TDS
{
    public partial class frmTDSSectionView : frmFinanceBase
    {

        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region constructor
        public frmTDSSectionView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int tdsID = 0;
        private int TDSId
        {
            get
            {
                RowIndex = gvTDSSection.FocusedRowHandle;
                tdsID = gvTDSSection.GetFocusedRowCellValue(colTDS_Section_Id) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSSection.GetFocusedRowCellValue(colTDS_Section_Id).ToString()) : 0;
                return tdsID;
            }
            set
            {
                this.tdsID = value;
            }
        }
        #endregion

        #region Events
        private void frmTDSSectionView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmTDSSectionView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchTDSSectionDetails();
        }

        private void ucToolBarTDSSection_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        private void ucToolBarTDSSection_EditClicked(object sender, EventArgs e)
        {
            ShowEditTDSSectionForm();
        }

        private void ucToolBarTDSSection_DeleteClicked(object sender, EventArgs e)
        {
            DeleteTDSSectionDetails();
        }

        private void ucToolBarTDSSection_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBarTDSSection_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcTDSSection, "TDS Sections", PrintType.DT, gvTDSSection, true);
            PrintGridViewDetails(gcTDSSection,this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_SECTION_PRINT_CAPTION), PrintType.DT, gvTDSSection, true);
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchTDSSectionDetails();
            gvTDSSection.FocusedRowHandle = RowIndex;
        }

        private void ucToolBarTDSSection_RefreshClicked(object sender, EventArgs e)
        {
            FetchTDSSectionDetails();
        }



        private void gvTDSSection_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvTDSSection.RowCount.ToString();
        }

        private void frmTDSSectionView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvTDSSection_DoubleClick(object sender, EventArgs e)
        {
            ShowEditTDSSectionForm();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Fetch the details of country.
        /// </summary>
        private void FetchTDSSectionDetails()
        {
            try
            {
                using (TDSSectionSystem Sectionsystem = new TDSSectionSystem())
                {
                    resultArgs = Sectionsystem.FetchTDSSectionDetails();
                    gcTDSSection.DataSource = resultArgs.DataSource.Table;
                    gcTDSSection.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(TDSSection.CreateTDSSection);
            this.enumUserRights.Add(TDSSection.EditTDSSection);
            this.enumUserRights.Add(TDSSection.DeleteTDSSection);
            this.enumUserRights.Add(TDSSection.PrintTDSSection);
            this.enumUserRights.Add(TDSSection.ViewTDSSection);
            this.ApplyUserRights(ucToolBarTDSSection, enumUserRights, (int)Menus.TDSSection);
        }
        /// <summary>
        /// To Delete the CountryDetails
        /// </summary>
        private void DeleteTDSSectionDetails()
        {
            try
            {
                if (gvTDSSection.RowCount > 0)
                {
                    if (TDSId > 0)
                    {
                        if (IsActiveTDSSection() == 0)
                        {
                            using (TDSSectionSystem TDSSystem = new TDSSectionSystem())
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    resultArgs = TDSSystem.DeleteTDSSectionDetails(tdsID);
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Country.COUNTRY_DELETE_SUCCESS));
                                        FetchTDSSectionDetails();
                                    }
                                }
                            }
                        }
                        else
                        {
                            //this.ShowMessageBox("This Section is mapped with Nature of Payments.Cannot delete.");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_SECTION_DELETE_INFO));
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
        /// Invoke the country form based on the id. 
        /// </summary>
        private void ShowForm(int TDSid)
        {
            try
            {
                frmTDSSectionAdd frmAddTDSSection = new frmTDSSectionAdd(TDSid);
                frmAddTDSSection.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAddTDSSection.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        /// It gets the CountryId
        /// </summary>
        public void ShowEditTDSSectionForm()
        {
            if (this.isEditable)
            {
                if (gvTDSSection.RowCount > 0)
                {
                    if (TDSId > 0)
                    {
                        ShowForm(TDSId);
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

        private int IsActiveTDSSection()
        {
            int Count = 0;
            using (TDSSectionSystem tdsSection = new TDSSectionSystem())
            {
                tdsSection.TDS_section_Id = TDSId;
                Count = tdsSection.CheckTDSSection();
            }
            return Count;
        }
        #endregion

        private void frmTDSSectionView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditTDSSectionForm();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTDSSection.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTDSSection, colName);
            }
        }

    }
}