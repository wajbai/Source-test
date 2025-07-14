using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.ASSET;
using Bosco.Model.Asset;
using Bosco.Model;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmUnitofMeasureView : frmBase
    {
        #region Variable Declearation
        private int RowIndex = 0;
        ResultArgs resultArgs = null;
        int unitid = 0;
        private FinanceModule financeModule { get; set; }
        private Bosco.Model.AssetStockProduct.IMeasure Imeasure = null;
        #endregion

        #region Constructors
        public frmUnitofMeasureView(FinanceModule module)
        {
            financeModule = module;
            Imeasure = AssetStockFactory.GetUnitOfMeasureInstance(module);
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int UnitId
        {
            get
            {
                RowIndex = gvMeasureofunits.FocusedRowHandle;
                unitid = gvMeasureofunits.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvMeasureofunits.GetFocusedRowCellValue(colId).ToString()) : 0;
                return unitid;
            }
            set
            {
                unitid = value;
            }
        }
        #endregion

        #region Events

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadUnitOfMeassures();
            gvMeasureofunits.FocusedRowHandle = RowIndex;
        }

        private void ucMeasureofUnits_RefreshClicked(object sender, EventArgs e)
        {
            LoadUnitOfMeassures();
        }

        private void gvMeasureofunits_DoubleClick(object sender, EventArgs e)
        {
            showUnitOfMeasureform(UnitId);
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMeasureofunits.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvMeasureofunits, colSymbol);
            }
        }

        private void frmUnitofMeasureView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvMeasureofunits_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvMeasureofunits.RowCount.ToString();
        }

        private void frmUnitofMeasureView_EnterClicked(object sender, EventArgs e)
        {
            ShowUnitOfMeasure();
        }

        private void ucMeasureofUnits_AddClicked(object sender, EventArgs e)
        {
            showUnitOfMeasureform((int)AddNewRow.NewRow);
        }

        private void ucMeasureofUnits_EditClicked(object sender, EventArgs e)
        {
            ShowUnitOfMeasure();
        }

        private void ucMeasureofUnits_DeleteClicked(object sender, EventArgs e)
        {
            DeleteUnitOfMeasure();
        }

        private void ucMeasureofUnits_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucMeasureofUnits_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcMeasureofunits, this.GetMessage(MessageCatalog.Master.Bank.BANK_PRINT_CAPTION), PrintType.DT, gvMeasureofunits, true);
        }

        private void frmUnitofMeasureView_Load(object sender, EventArgs e)
        {
            LoadUnitOfMeassures();
        }

        #endregion

        #region Methods
        public void LoadUnitOfMeassures()
        {
            try
            {
                resultArgs = Imeasure.FetchMeasureDetails();
                if (resultArgs.Success)
                {
                    gcMeasureofunits.DataSource = resultArgs.DataSource.Table;
                    gcMeasureofunits.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        public void DeleteUnitOfMeasure()
        {
            try
            {
                if (gvMeasureofunits.RowCount > 0)
                {
                    if (UnitId > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Imeasure.unitId = UnitId;
                            resultArgs = Imeasure.DeleteMeasureDetails();
                            if (resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                LoadUnitOfMeassures();
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_FAILURE));
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
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void ShowUnitOfMeasure()
        {
            if (gvMeasureofunits.RowCount != 0)
            {
                if (UnitId > 0)
                {
                    showUnitOfMeasureform(UnitId);
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

        private void showUnitOfMeasureform(int UnitId)
        {
            try
            {
                frmUnitofMeasureAdd unitOfMeasure = new frmUnitofMeasureAdd(UnitId, financeModule);
                unitOfMeasure.UpdateHeld += new EventHandler(OnUpdateHeld);
                unitOfMeasure.ShowDialog();
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

