using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceiptPaymentRecord : Bosco.Report.Base.ReportHeaderBase
    {
        public MultiAbstractReceiptPaymentRecord()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            this.ReportSubTitle = ReportProperty.Current.ProjectTitle;

            if (((MultiAbstractReceipt)xrSubReceipts.ReportSource) != null)
            {
                ((ReportHeaderBase)xrSubReceipts.ReportSource).HideReportTitle = false;
                ((ReportHeaderBase)xrSubReceipts.ReportSource).HideReportSubTitle = false;
                ((ReportHeaderBase)xrSubReceipts.ReportSource).HideInstitute = false;
                ((ReportHeaderBase)xrSubReceipts.ReportSource).HidePageInfo = false;
                ((ReportHeaderBase)xrSubReceipts.ReportSource).HidePrintDate = false;
                ((MultiAbstractReceipt)xrSubReceipts.ReportSource).BindMultiReceiptSource();
            }
            if (((MultiAbstractPaymentRecord)xrSubPayment.ReportSource) != null)
            {
                ((ReportHeaderBase)xrSubPayment.ReportSource).HideInstitute = false;
                ((ReportHeaderBase)xrSubPayment.ReportSource).HideReportTitle = false;
                ((ReportHeaderBase)xrSubPayment.ReportSource).HideReportSubTitle = false;
                ((ReportHeaderBase)xrSubPayment.ReportSource).HidePageInfo = false;
                ((ReportHeaderBase)xrSubPayment.ReportSource).HidePrintDate = false;
                ((MultiAbstractPaymentRecord)xrSubPayment.ReportSource).BindMultiPaymentSource();
            }
            base.ShowReport();
        }
    }
}
