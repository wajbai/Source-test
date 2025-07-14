using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using PAYROLL.Modules;

using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class PayEarning : ReportBase
    {
        #region Variables
       public ResultArgs resultArgs = new ResultArgs();
        public clsPayrollBase PayrollBase = new clsPayrollBase();
        #endregion

        #region Constructor
        public PayEarning()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        public void SetEarningSource()
        {
            resultArgs = GetReportSource();
            DataView dvDayBook = resultArgs.DataSource.TableView;

            if (dvDayBook != null && dvDayBook.Count != 0)
            {
                //xrDesignation.Text = dvDayBook.ToTable().Rows[0]["DESIGNATION"].ToString();
                //xrName.Text = dvDayBook.ToTable().Rows[0]["NAME"].ToString();

                dvDayBook.RowFilter =  " Component <> 'Name' AND Component <> 'Designation' AND Component <> 'NET PAY'";
                dvDayBook.Table.TableName = "PAYWAGES";
                this.DataSource = dvDayBook;
                this.DataMember = dvDayBook.Table.TableName;
            }
            else
            {
                this.DataSource = null;
            }
            dvDayBook.RowFilter = "";
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                resultArgs = PayrollBase.RpPayslip("0,2");
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void xrTableCell3_SummaryRowChanged(object sender, EventArgs e)
        {

        }
    }
}
