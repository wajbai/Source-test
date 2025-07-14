using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.IO;
using System.Data;
using DevExpress.XtraReports.UI.PivotGrid;

namespace Bosco.Report.ReportObject // This is for me to test some cases
{
    public partial class TestReports : Report.Base.ReportHeaderBase
    {

        public TestReports()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            ConstructDatatable();
            base.ShowReport();
        }

        public void ConstructDatatable()
        {
            // Create a new DataTable
            DataTable dt = new DataTable("SalesData");

            // Define columns
            dt.Columns.Add("Category", typeof(string)); // Row Area
            dt.Columns.Add("Year", typeof(int)); // Columns Area
            dt.Columns.Add("Region", typeof(string)); // Filter Area
            dt.Columns.Add("Sales", typeof(decimal)); // Data Area

            // Add rows to the DataTable
            dt.Rows.Add("Electronics", 1, "North America", 10000.00m);
            dt.Rows.Add("Electronics", 1, "North America", 12000.00m);
            dt.Rows.Add("Electronics", 2, "Europe", 9000.00m);
            dt.Rows.Add("Clothing", 2, "North America", 8000.00m);
            dt.Rows.Add("Clothing", 3, "North America", 9000.00m);
            dt.Rows.Add("Clothing", 3, "Europe", 7500.00m);

            this.xrPivotGrid1.DataSource = dt;
            this.xrPivotGrid1.DataMember = dt.TableName;
        }

        private void xrPivotGrid1_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.DisplayText == "Grand Total")
            {
                e.DisplayText = "Chinna Total";
            }
        }

        private void xrPivotGrid1_PrintFieldValue(object sender, CustomExportFieldValueEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Field.FieldName != null && e.Field.FieldName == "Category")
                {
                    if (e.Value.ToString() == "Clothing")
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                }
            }
            e.Appearance.BackColor = Color.Blue;
        }
    }
}
