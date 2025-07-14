using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ACPP;
using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using System.Runtime.InteropServices;
using DevExpress.XtraBars;
using Bosco.Model.UIModel.Master;


namespace ACPP.Modules.Master
{
    public partial class frmStateView : frmFinanceBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constrctor
        public frmStateView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int StateId = 0;
        private int StateID
        {
            get
            {
                RowIndex = gvState.FocusedRowHandle;
                StateId = gvState.GetFocusedRowCellValue(colStateId) != null ? this.UtilityMember.NumberSet.ToInteger(gvState.GetFocusedRowCellValue(colStateId).ToString()) : 0;
                return StateId;
            }
            set
            {
                StateId = value;
            }
        }
        #endregion

        #region Events For Bank

        private void ucToolBar1_AddClicked_1(object sender, EventArgs e)
        {
            ShowStateForm((int)AddNewRow.NewRow);
        }

        private void ucToolBar1_EditClicked_1(object sender, EventArgs e)
        {
            ShowStateEditForm();
        }

        private void ucToolBar1_DeleteClicked_1(object sender, EventArgs e)
        {
            DeleteStateFormDetails();
        }

        private void frmStateView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmStateView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false,true);
            FetchStateDetails();
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchStateDetails();
        }

        private void ucToolBar1_PrintClicked_1(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcState, this.GetMessage(MessageCatalog.Master.State.STATE_PRINT_CAPTION), PrintType.DT, gvState, true);
        }

        private void gvState_DoubleClick(object sender, EventArgs e)
        {
            ShowStateEditForm();
        }
        private void gvState_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCountResult.Text = gvState.RowCount.ToString();
        }
        #endregion
        /// <summary>
        ///
        /// </summary>
        #region Methods
        private void FetchStateDetails()
        {
            try
            {
                using (StateSystem stateSystem = new StateSystem())
                {
                    resultArgs = stateSystem.FetchStateDetails();
                    gcState.DataSource = resultArgs.DataSource.Table;
                    gcState.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ShowStateForm(int StateId)
        {
            try
            {
                frmStateAdd frmState_Add = new frmStateAdd(StateId);
                frmState_Add.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmState_Add.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchStateDetails();
            gvState.FocusedRowHandle = RowIndex;
        }

        private void DeleteStateFormDetails()
        {
            try
            {

                if (gvState.RowCount != 0)
                {
                    if (StateID != 0)
                    {
                        using (StateSystem bankSystem = new StateSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = bankSystem.DeleteStateDetails(StateID);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchStateDetails();
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

        public void ShowStateEditForm()
        {
            if (gvState.RowCount != 0)
            {
                if (StateID != 0)
                {
                    ShowStateForm(StateID);
                }

                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                }

            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvState.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvState, colState);
            }
        }

        private void ApplyUserRights()
        {
                this.enumUserRights.Add(State.CreateState);
                this.enumUserRights.Add(State.EditState);
                this.enumUserRights.Add(State.DeleteState);
                this.enumUserRights.Add(State.PrintState);
                this.enumUserRights.Add(State.ViewState);
                this.ApplyUserRights(ucToolBar1, this.enumUserRights, (int)Menus.BudgetAnnual);

        }
        #endregion
    }
}


