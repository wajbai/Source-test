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
using Bosco.Model.UIModel.Master;
using Bosco.Model.UIModel;
using DevExpress.XtraBars;

namespace ACPP.Modules.Master
{
    public partial class StatisticsTypeView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion
        #region Property
        private int statisticstypeid = 0;
        public int StatisticsTypeId
        {
            get
            {
                RowIndex = gvStatisticsTypeView.FocusedRowHandle;
                StatisticsTypeId = gvStatisticsTypeView.GetFocusedRowCellValue(colStatisticsTypeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvStatisticsTypeView.GetFocusedRowCellValue(colStatisticsTypeId).ToString()) : 0;
                return statisticstypeid;
            }
            set
            {
                statisticstypeid = value;
            }
        }
        #endregion

        public StatisticsTypeView()
        {
            InitializeComponent();
        }

        #region Methods
        /// <summary>
        /// Load the Purpose Details
        /// </summary>
        public void GetStatisticsTypeDetails()
        {
            try
            {
                using (StatisticsTypeSystem statisticstypeSystemSystem = new StatisticsTypeSystem())
                {
                    resultArgs = statisticstypeSystemSystem.FetchStatisticsTypeAll();
                    gcStatisticsTypeView.DataSource = resultArgs.DataSource.Table;
                    gcStatisticsTypeView.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Show Popup Purpose form based on the Id
        /// </summary>
        /// <param name="purposeId"></param>
        public void ShowForm(int statisticstypeId)
        {
            try
            {
                StatisticsTypeAdd frmstatisticstype = new StatisticsTypeAdd(statisticstypeId);
                frmstatisticstype.UpdataHeld += new EventHandler(OnUpdateHeld);
                frmstatisticstype.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Refresh the Purpose Grid after adding and editing the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            GetStatisticsTypeDetails();
            gvStatisticsTypeView.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// To Identify the Id and Show the form
        /// </summary>
        public void EditStatisticsTypeForm()
        {
            try
            {
                if (gvStatisticsTypeView.RowCount != 0)
                {
                    ShowForm(StatisticsTypeId);
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

        private void SetRights()
        {
            int AccessRights = 0;
            using (MasterRightsSystem masterRightsSystem = new MasterRightsSystem())
            {
                masterRightsSystem.MasterName = this.Text;
                AccessRights = masterRightsSystem.MasterRights();
                if (AccessRights == (int)MasterRights.ReadOnly)
                {
                    ucStatisticsTypeToolBar.VisibleDeleteButton = ucStatisticsTypeToolBar.VisibleAddButton = ucStatisticsTypeToolBar.VisibleEditButton = BarItemVisibility.Never;
                }
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Budget.CreateBudget);
            this.enumUserRights.Add(Budget.EditBudget);
            this.enumUserRights.Add(Budget.DeleteBudget);
            this.enumUserRights.Add(Budget.PrintBudget);
            this.enumUserRights.Add(Budget.ViewBudget);
            this.ApplyUserRights(this.ucStatisticsTypeToolBar, this.enumUserRights, (int)Menus.Budget);
        }
        #endregion

        private void StatisticsTypeView_Load(object sender, EventArgs e)
        {
            //SetRights();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyUserRights();
            }
        }

        private void StatisticsTypeView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, false);
            GetStatisticsTypeDetails();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStatisticsTypeView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvStatisticsTypeView, colStatisticsType);
            }
        }

        private void gvStatisticsTypeView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvStatisticsTypeView.RowCount.ToString();
        }

        private void ucStatisticsTypeToolBar_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        private void ucStatisticsTypeToolBar_EditClicked(object sender, EventArgs e)
        {
            EditStatisticsTypeForm();
        }

        private void ucStatisticsTypeToolBar_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                using (StatisticsTypeSystem StatisticsTypeSystem = new StatisticsTypeSystem())
                {
                    if (gvStatisticsTypeView.RowCount != 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            resultArgs = StatisticsTypeSystem.DeleteStatisticsType(StatisticsTypeId);
                            if (resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.StatisticsType.STATISTICSTYPE_DELETE_SUCCESS));
                                GetStatisticsTypeDetails();
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

        private void ucStatisticsTypeToolBar_PrintClicked(object sender, EventArgs e)
        {
            // PrintGridViewDetails(gcStatisticsTypeView, this.GetMessage(MessageCatalog.Master.StatisticsType.STATISTICSTYPE_PRINT_CAPTION), PrintType.DT, gvStatisticsTypeView);
            PrintGridViewDetails(gcStatisticsTypeView,this.GetMessage(MessageCatalog.Master.StatisticsType.STATISTICSTYPE_PRINT_CAPTION) , PrintType.DT, gvStatisticsTypeView);
        }

        private void ucStatisticsTypeToolBar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucStatisticsTypeToolBar_RefreshClicked(object sender, EventArgs e)
        {
            gvStatisticsTypeView.ActiveFilter.Clear();
            GetStatisticsTypeDetails();
        }

        private void gcStatisticsTypeView_DoubleClick(object sender, EventArgs e)
        {
            EditStatisticsTypeForm();
        }
    }
}