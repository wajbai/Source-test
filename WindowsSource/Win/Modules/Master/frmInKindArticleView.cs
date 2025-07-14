/*Added By:Michael R
 *Added On:19/08/2013
 *Purpose : To have the details of the InKindArticle View form.
 *Modified On: 
 *Modified Purpose:
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Alerter;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;


namespace ACPP.Modules.Master
{
    public partial class frmInKindArticleView : frmFinanceBase
    {
        #region Variable Decelaration
        private ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor

        public frmInKindArticleView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int InKindArticleId = 0;
        private int inKindArticleId
        {
            get
            {

                RowIndex = gvInKindArticle.FocusedRowHandle;
                InKindArticleId = gvInKindArticle.GetFocusedRowCellValue(colArticleId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInKindArticle.GetFocusedRowCellValue(colArticleId).ToString()) : 0;
                //this.InKindArticleId = this.UtilityMember.NumberSet.ToInteger(gvInKindArticle.GetFocusedRowCellValue(colArticleId).ToString());
                return InKindArticleId;
            }
            set { this.InKindArticleId = value; }
        }

        #endregion

        #region Events For InKindArticle

        /// <summary>
        /// Load the details of in kind article.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmInKindArticleView_Load(object sender, EventArgs e)
        {
            ucToolBarInKindArticle.DisableDeleteButton = true;
            ucToolBarInKindArticle.DisableEditButton = true;
        }

        private void frmInKindArticleView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, false);
            FetchInKindArticleDetails();
        }

        /// <summary>
        /// Invoke from inkindArticleAdd to add new inkindarticle details. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarInKindArticle_AddClicked(object sender, EventArgs e)
        {
            ShowInKindArticelForm(InKindArticleId);
        }

        /// <summary>
        /// Edit the selected row in the grid control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarInKindArticle_EditClicked(object sender, EventArgs e)
        {
            ShowEditInKindArticle();
        }

        /// <summary>
        /// Fire when the grid is double clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvInKindArticle_DoubleClick(object sender, EventArgs e)
        {
            ShowEditInKindArticle();
        }

        /// <summary>
        /// Fire when the delete button is clicked to delete the selected row in the grid view control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarInKindArticle_DeleteClicked(object sender, EventArgs e)
        {
            DeleteInKindArticleDetails();
        }

        /// <summary>
        /// Print the whole grid control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarInKindArticle_PrintClicked(object sender, EventArgs e)
        {
            this.PrintGridViewDetails(gcInKindArticle, this.GetMessage(MessageCatalog.Master.InKindArticle.INKINDARTICLE_PRINT_CAPTION), PrintType.DT, gvInKindArticle);
        }

        /// <summary>
        /// Enable or Disable auto filter row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvInKindArticle.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvInKindArticle, colArticle);
            }
        }

        /// <summary>
        /// Update the row count. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvInKindArticle_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvInKindArticle.RowCount.ToString();
        }

        /// <summary>
        /// Refresh the grid once the record is updated or added to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchInKindArticleDetails();
            gvInKindArticle.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        ///Close the form when the close button is clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarInKindArticle_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// To refresh Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarInKindArticle_RefreshClicked(object sender, EventArgs e)
        {
            FetchInKindArticleDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Fetch Inkind Article details
        /// </summary>

        private void FetchInKindArticleDetails()
        {
            try
            {
                using (InKindArticleSystem inKindArticleSystem = new InKindArticleSystem())
                {
                    resultArgs = inKindArticleSystem.FetchInKindArticleDetails();
                    gcInKindArticle.DataSource = resultArgs.DataSource.Table;
                    gcInKindArticle.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Show Module pop up form for inkind article.
        /// </summary>
        /// <param name="InKindArticleId"></param>

        private void ShowInKindArticelForm(int InKindArticleId)
        {
            try
            {
                frmInKindArticleAdd frmInKindArticle = new frmInKindArticleAdd(InKindArticleId);
                frmInKindArticle.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmInKindArticle.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            this.InKindArticleId = 0;
        }

        /// <summary>
        /// Delete the selected row in the grid control.
        /// </summary>

        private void DeleteInKindArticleDetails()
        {
            try
            {
                if (inKindArticleId != 0)
                {
                    using (InKindArticleSystem inKindArticleSystem = new InKindArticleSystem())
                    {
                        if (gvInKindArticle.RowCount != 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = inKindArticleSystem.DeleteInKindArticleDetails(inKindArticleId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));

                                    FetchInKindArticleDetails();
                                }
                                else
                                {
                                    XtraMessageBox.Show(this.GetMessage(resultArgs.Message), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
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

        private void ShowEditInKindArticle()
        {
            if (inKindArticleId != 0)
            {
                if (gvInKindArticle.RowCount != 0)
                {
                    ShowInKindArticelForm(inKindArticleId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }
        #endregion

        private void frmInKindArticleView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmInKindArticleView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditInKindArticle();
        }
    }
}