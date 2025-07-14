using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using System.Data;
using PAYROLL.Modules;

namespace Bosco.Report.ReportObject
{
    public partial class PayDeduction : Bosco.Report.Base.ReportBase
    {
        #region Variables
        public ResultArgs resultArgs = new ResultArgs();
        public clsPayrollBase PayrollBase = new clsPayrollBase();
        #endregion

        #region Constructor
        public PayDeduction()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        public void SetDeductionSource()
        {
            resultArgs = GetReportSource();
            DataView dvDayBook = resultArgs.DataSource.TableView;

            if (dvDayBook != null && dvDayBook.Count != 0)
            {
                //dvDayBook.RowFilter = "Type IN ()";// AND Component <> 'Name' AND Component <> 'Designation' AND Component <> 'NET PAY'";
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
                resultArgs = PayrollBase.RpPayslip("1");
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion

    }
}
