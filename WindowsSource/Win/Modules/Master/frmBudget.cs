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
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Globalization;
using System.Collections;

namespace ACPP.Modules.Master
{
    public partial class frmBudget : frmBase
    {
        ResultArgs resultArgs = null;
        int BudegetId = 0;
        DateTime DateFrom, DateTo;
        public frmBudget()
        {
            InitializeComponent();
        }
        public frmBudget(int BudegetId, DateTime DateFrom, DateTime DateTo)
            : this()
        {

            this.BudegetId = BudegetId;
            this.DateFrom = DateFrom;
            this.DateTo = DateTo;
        }

        private void LoadMappedLedgers()
        {
            try
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    budgetSystem.BudgetId = BudegetId;
                    budgetSystem.DateFrom = DateFrom.ToString();
                    budgetSystem.DateTo = DateTo.ToString();
                    resultArgs = budgetSystem.GetBindDataRandomSource();
                    DataTable dtBudget = resultArgs.DataSource.Table;
                    pgcBudget.DataSource = dtBudget;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            finally { }
        }

        private void frmBudget_Load(object sender, EventArgs e)
        {
            LoadMappedLedgers();
        }

        private void pgcBudget_CustomFieldSort(object sender, DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == pgfMonthName1.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "DURATION").ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "DURATION").ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }
        private void pgcBudget_CellDoubleClick(object sender, DevExpress.XtraPivotGrid.PivotCellEventArgs e)
        {

        }
    }
}