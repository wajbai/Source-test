using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model.TDS;

namespace ACPP
{
    public partial class frmCheck : frmFinanceBaseAdd
    {
        public frmCheck()
        {
            InitializeComponent();
        }


        private void frmCheck_Load(object sender, EventArgs e)
        {
            // FetchBankDetails();
            string d = "2014-04-01 00:00:00";
            DateTime vdate = this.UtilityMember.DateSet.ToDate(d,false);
        }


        private void LoadTaxDetails()
        {
            using (DeducteeTaxSystem tax = new DeducteeTaxSystem())
            {
                ResultArgs resultArgs = tax.FetchDeducteeTaxDetails();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    gridControl1.DataSource = resultArgs.DataSource.Table;
                }
            }
        }

        private void FetchBankDetails()
        {
            try
            {
                using (BankSystem bankSystem = new BankSystem())
                {
                    ResultArgs resultArgs = bankSystem.FetchBankDetails();
                    gridControl1.DataSource = resultArgs.DataSource.Table;
                    gridControl1.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void rbtnSave_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if ((e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph) && e.Button.Index == 0)
            {
                MessageBox.Show(e.Button.Caption);
            }
            else
            {
                MessageBox.Show(e.Button.Caption);
            }
        }

        private void rbtnSave_Click(object sender, EventArgs e)
        {

        }

        private void layoutView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (layoutView1.FocusedColumn == colSave && Convert.ToInt32(layoutView1.GetFocusedRowCellValue(colId))==192)
            //{
            //    rbtnSave.Buttons[0].Enabled = false;
            //}

        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (rbtnSave.Buttons[0].Caption == "Save" && Convert.ToInt32(gridView1.GetFocusedRowCellValue(colId)) == 192)
            {
                e.Cancel = true;
            }
        }
    }
}