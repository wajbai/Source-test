/*  Class Name      : frmCountryView
 *  Purpose         : To view the Country Browse Screen
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraBars;

namespace ACPP.Modules.Master
{
    public partial class frmCountryView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmCountryView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int countryId = 0;
        private int CountryId
        {
            get
            {
                RowIndex = gvCountry.FocusedRowHandle;
                return gvCountry.GetFocusedRowCellValue(colCountryId) != null ? this.UtilityMember.NumberSet.ToInteger(gvCountry.GetFocusedRowCellValue(colCountryId).ToString()) : 0;
            }
            set
            {
                this.countryId = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the country details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCountryView_Load(object sender, EventArgs e)
        {
            // SetRights();
            ApplyUserRights();
        }

        private void frmCountryView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchCountryDetails();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Conutry.CreateCountry);
            this.enumUserRights.Add(Conutry.EditCountry);
            this.enumUserRights.Add(Conutry.DeleteCountry);
            this.enumUserRights.Add(Conutry.PrintCountry);
            this.enumUserRights.Add(Conutry.ViewCountry);
            this.ApplyUserRights(ucToolBarCountryView, enumUserRights, (int)Menus.Country);
        }

        /// <summary>
        /// Add new country details 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarCountryView_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Edit the country details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarCountryView_EditClicked(object sender, EventArgs e)
        {
            ShowEditCountryForm();
        }

        /// <summary>
        /// Delete the country details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarCountryView_DeleteClicked(object sender, EventArgs e)
        {
            DeleteCountryDetails();
        }

        /// <summary>
        /// Edit the country details. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcCountry_DoubleClick(object sender, EventArgs e)
        {
            ShowEditCountryForm();
        }

        /// <summary>
        ///  To enable auto filter row in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvCountry.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvCountry, colCountry);
            }

        }

        /// <summary>
        /// Set row count for grid controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCountry_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvCountry.RowCount.ToString();
        }

        /// <summary>
        /// Print the details of country. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarCountryView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcCountry, this.GetMessage(MessageCatalog.Master.Country.COUNTRY_PRINT_CAPTION), PrintType.DT, gvCountry, true);
        }

        /// <summary>
        /// Close the country form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarCountryView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Refresh the grid control after editnig and adding the records.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchCountryDetails();
            gvCountry.FocusedRowHandle = RowIndex;
        }
        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarCountryView_RefreshClicked(object sender, EventArgs e)
        {
            FetchCountryDetails();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Fetch the details of country.
        /// </summary>
        private void FetchCountryDetails()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryDetails();
                    gcCountry.DataSource = resultArgs.DataSource.Table;
                    gcCountry.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Delete the CountryDetails
        /// </summary>
        private void DeleteCountryDetails()
        {
            try
            {

                if (gvCountry.RowCount != 0)
                {
                    if (CountryId != 0)
                    {
                        using (CountrySystem countrySystem = new CountrySystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = countrySystem.DeleteCountryDetails(CountryId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Country.COUNTRY_DELETE_SUCCESS));
                                    FetchCountryDetails();
                                }
                                else
                                {
                                    this.ShowMessageBoxError(resultArgs.Message);
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
        /// Invoke the country form based on the id. 
        /// </summary>
        private void ShowForm(int countryId)
        {
            try
            {
                frmCountry frmAddCountry = new frmCountry(countryId);
                frmAddCountry.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAddCountry.ShowDialog();
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
        public void ShowEditCountryForm()
        {
            if (this.isEditable)
            {
                if (gvCountry.RowCount != 0)
                {
                    if (CountryId != 0)
                    {
                        ShowForm(CountryId);
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
                    ucToolBarCountryView.VisibleDeleteButton = ucToolBarCountryView.VisibleAddButton = ucToolBarCountryView.VisibleEditButton = BarItemVisibility.Never;
                }
            }
        }
        #endregion

        private void frmCountryView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmCountryView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditCountryForm();
        }

    }
}