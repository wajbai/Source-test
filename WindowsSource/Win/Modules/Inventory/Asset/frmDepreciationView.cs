using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmDepreciationView : frmFinanceBase
    {
        #region Event Declearation
        public event EventHandler UpdateHeld;
        #endregion
       
        # region Variable Declaration
        private int RowIndex = 0;
        private int Depreciation_Id = 0;
        #endregion

        #region Constructors
        public frmDepreciationView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int depreciation_Id
        {
            get
            {
                RowIndex = gvDepreciationView.FocusedRowHandle;
                Depreciation_Id = gvDepreciationView.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvDepreciationView.GetFocusedRowCellValue(colId).ToString()) : 0;
                return Depreciation_Id;
            }
            set
            {
                Depreciation_Id = value;
            }
        }
        #endregion
        
        #region  Events

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadDepreciationDetails();
            gvDepreciationView.FocusedRowHandle = RowIndex;
        }

        private void ucDepriciationView_AddClicked(object sender, EventArgs e)
        {
            frmDepreciationMethodsAdd depreciationMethods = new frmDepreciationMethodsAdd();
            depreciationMethods.UpdateHeld += new EventHandler(OnUpdateHeld);
            depreciationMethods.ShowDialog();
        }

        private void ucDepriciationView_EditClicked(object sender, EventArgs e)
        {
            ShowDepreciationEdit();
        }

        private void ucDepriciationView_DeleteClicked(object sender, EventArgs e)
        {
            DepreciationDetailsDelete();
        }

        private void ucDepriciationView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucDepriciationView_RefreshClicked(object sender, EventArgs e)
        {
            LoadDepreciationDetails();
        }

        private void ucDepriciationView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcDepreciationView, this.GetMessage(MessageCatalog.Asset.Depreciation.DEPRECIATION_PRINT_CAPTION), PrintType.DT, gvDepreciationView, true);
        }

        private void gcDepreciationView_DoubleClick(object sender, EventArgs e)
        {
            ShowDepreciationEdit();
        }

        private void gvDepreciationView_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvDepreciationView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDepreciationView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDepreciationView, colName);
            }
        }

        private void frmDepreciationView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        #endregion 

        #region Methods

        /// <summary>
        /// Load Depreciation details.
        /// </summary>
        private void frmDepreciationView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true, true);
            LoadDepreciationDetails();
        }
        /// <summary>
        /// Load Depreciation details.
        /// </summary>
        public void LoadDepreciationDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                using (AssetDepreciationSystem DepreciationSystem = new AssetDepreciationSystem())
                {
                    resultArgs = DepreciationSystem.FetchAll();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcDepreciationView.DataSource = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        gcDepreciationView.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }
        /// <summary>
        /// Delete the Depreciation details.
        /// </summary>
        private void DepreciationDetailsDelete()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvDepreciationView.RowCount != 0)
                {
                    if (depreciation_Id != 0)
                    {
                        using (AssetDepreciationSystem depreciationsystem = new AssetDepreciationSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = depreciationsystem.DeleteDepreciation(Depreciation_Id);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadDepreciationDetails();
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
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            {}
        }

        private void ShowDepreciationEdit()
        {
            
                if (gvDepreciationView.RowCount > 0)
                {
                    if (depreciation_Id > 0)
                    {
                        showDepreciationform(Depreciation_Id);
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

        /// <summary>
        /// Show Depreciation Form
        /// </summary>
        private void showDepreciationform(int DepreciationId)
        {
            try
            {
                frmDepreciationMethodsAdd depreciationmethods = new frmDepreciationMethodsAdd(DepreciationId);
                depreciationmethods.UpdateHeld += new EventHandler(OnUpdateHeld);
                depreciationmethods.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally 
            { }        
        }
        #endregion
    }
}