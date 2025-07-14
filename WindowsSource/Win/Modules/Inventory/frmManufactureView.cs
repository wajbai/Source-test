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
using Bosco.Model;
using Bosco.Model.Inventory;

namespace ACPP.Modules.Inventory
{
    public partial class frmManufactureView : frmBase
    {
        #region Constructor
        public frmManufactureView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        ResultArgs resultArgs = null;
        private int manufactureId = 0;
        private int RowIndex = 0;

        public int ManufactureId
        {
            get
            {
                RowIndex = gvManufacture.FocusedRowHandle;
                manufactureId = gvManufacture.GetFocusedRowCellValue(colManufactureId) != null ? this.UtilityMember.NumberSet.ToInteger(gvManufacture.GetFocusedRowCellValue(colManufactureId).ToString()) : 0;
                return manufactureId;
            }
            set
            {
                manufactureId = value;
            }
        }
        #endregion

        #region Events

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowManufacture((int)AddNewRow.NewRow);
        }

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            DeleteManufacture();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            ShowEditManufacture();
        }

        private void gcManufacture_DoubleClick(object sender, EventArgs e)
        {
            ShowEditManufacture();
        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            LoadManufactureDetails();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcManufacture, "Manufacture Details", PrintType.DT, gvManufacture, true);
        }

        protected virtual void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadManufactureDetails();
        }
        private void frmManufactureView_Load(object sender, EventArgs e)
        {
            LoadManufactureDetails();
        }

        #endregion

        #region Methods

        public void LoadManufactureDetails()
        {
            using (ManufactureSystem manufactureSystem = new ManufactureSystem())
            {
                resultArgs = manufactureSystem.FetchManufactureDetails();
                if (resultArgs.Success && resultArgs != null)
                {
                    gcManufacture.DataSource = resultArgs.DataSource.Table;
                    gcManufacture.RefreshDataSource();
                }
            }
        }

        private void ShowManufacture(int ManufactureId)
        {
            try
            {
                frmManufacture manufactureAdd = new frmManufacture(ManufactureId);
                manufactureAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                manufactureAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        private void DeleteManufacture()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvManufacture.RowCount != 0)
                {
                    if (ManufactureId != 0)
                    {
                        using (ManufactureSystem manufactureSystem = new ManufactureSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = manufactureSystem.DeleteManufactureDetails(ManufactureId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadManufactureDetails();
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
            { }
        }

        private void ShowEditManufacture()
        {
            if (gvManufacture.RowCount != 0)
            {
                if (ManufactureId != 0)
                {
                    ShowManufacture(ManufactureId);
                }
                else
                {
                    //if (!chkShowFilter.Checked)
                    //{
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                    //}
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        #endregion

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvManufacture.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvManufacture, colName);
            }
        }

        private void gvManufacture_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvManufacture.RowCount.ToString();
        }
    }
}