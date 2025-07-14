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
using System.ServiceModel;
using Bosco.Model.Dsync;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;

namespace ACPP.Modules.Dsync
{
    public partial class frmAvailableVouchersOtherProjectDate : frmFinanceBaseAdd
    {

        #region Declaration

        #endregion

        #region Constructor
        public frmAvailableVouchersOtherProjectDate()
        {
            InitializeComponent();
        }

        public frmAvailableVouchersOtherProjectDate(DataTable dtVouchersOtherProjectDate)
            : this()
        {
           gcAlreadyExistsVouchers.DataSource = dtVouchersOtherProjectDate;
        }

        #endregion

        #region Properties

        #endregion

        #region Events
        
        #endregion

        #region Methods
      
        #endregion

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAlreadyExistsVouchers.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAlreadyExistsVouchers, bgcLBProject);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvAlreadyExistsVouchers_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvAlreadyExistsVouchers.RowCount.ToString();
        }

       
        private void gvAlreadyExistsVouchers_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == bgcSerialNumber)
            {
                if (e.RowHandle >= 0)
                {
                    e.DisplayText = (e.RowHandle +1).ToString();
                }
            }
        }
               
       

        private void gvAlreadyExistsVouchers_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            BandedGridColumn column = sender as BandedGridColumn;
            if (column == bgcEmpty)
            {
                e.RowHeight = 15;
            }
        }

        private void btnExportXL_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvAlreadyExistsVouchers.RowCount > 0)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.FileName = "Available_Voucher(s).xlsx";
                    save.DefaultExt = ".xlsx";
                    save.Filter = "Excel(.xlsx)|*.xlsx";
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        DevExpress.XtraPrinting.XlsxExportOptions xlexportoptions = new DevExpress.XtraPrinting.XlsxExportOptions();
                        //xlexportoptions.RawDataMode = true;

                        gvAlreadyExistsVouchers.ExportToXlsx(save.FileName, xlexportoptions);
                        if (ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Budget.OPEN_THE_FILE), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            System.Diagnostics.Process.Start(save.FileName);
                        }

                    }
                }
                else
                {
                    this.ShowMessageBox("0 Voucher(s) to export");
                }
            }
            catch (IOException)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.FILE_OPEN_ALREADY_CLOSE));
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        
    }
}