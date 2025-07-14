using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Data.Filtering;
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;

namespace Bosco.Utility.Base
{
    public partial class frmFilter : frmBase
    {
        public frmFilter(GridControl grid)
        {
            InitializeComponent();
            //GridLocalizer.Active = new MyGridLocalizer();

            GridView grdView = grid.MainView as GridView;
            filterctl.SourceControl = grid;
            filterctl.FilterColumns.Clear(); //clear preloaed columns 

            filterctl.TabStop = true;
            //Hide NOT, OR operator
            //filterctl.ShowGroupCommandsIcon = true;
            filterctl.ShowOperandTypeIcon = true;
            filterctl.ShowToolTips = true;
            filterctl.PopupMenuShowing += new DevExpress.XtraEditors.Filtering.PopupMenuShowingEventHandler(filterctl_PopupMenuShowing);
            
            
            //Load all visible columns into Fitler control, based on its properties
            foreach (GridColumn dc in grdView.Columns)
            {
                if (dc.Visible && dc.FieldName.ToUpper() != "SELECT")
                {
                    Type columndatatype = dc.ColumnType;
                    RepositoryItem repitem = new RepositoryItemTextEdit();
                    if (columndatatype == typeof(DateTime))
                    {
                        repitem = new RepositoryItemDateEdit();
                    }

                    UnboundFilterColumn column = new UnboundFilterColumn(dc.Caption, dc.FieldName, columndatatype, repitem, FilterColumnClauseClass.String);
                    filterctl.FilterColumns.Add(column);
                }
            }
            
            if (grdView.ActiveFilterCriteria != null)
            {
                filterctl.FilterString = grdView.ActiveFilterCriteria.ToString();
            }
            grdView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            grdView.OptionsFilter.AllowFilterEditor = false;
        }
                
        //Hide NOT, OR operator
        private void filterctl_PopupMenuShowing(object sender, DevExpress.XtraEditors.Filtering.PopupMenuShowingEventArgs e)
        {
            //Hide NOT, OR operator
            if (e.MenuType == FilterControlMenuType.Group)
            {
                for (int i = e.Menu.Items.Count - 1; i >= 0; i--)
                {
                    if (e.Menu.Items[i].Caption == Localizer.Active.GetLocalizedString(StringId.FilterGroupNotAnd) ||
                        e.Menu.Items[i].Caption == Localizer.Active.GetLocalizedString(StringId.FilterGroupNotOr))
                    {
                        e.Menu.Items.RemoveAt(i);
                    }
                }
            }
        }

        private void frmFilter_Activated(object sender, EventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            filterctl.ApplyFilter();
            this.Close();
            
            //if (filterctl.FilterCriteria.IsNotNull)
            //{
            //    filterctleditor.ApplyFilter();
            //    this.Close();
            //}
            //else
            //{
            //    MessageRender.ShowMessage("Invalid condition, Check and reform the filter condition");
            //    filterctleditor.Focus();
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            filterctl.FilterString = string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class MyGridLocalizer : GridLocalizer
    {
        public override string GetLocalizedString(GridStringId id)
        {
            switch (id)
            {
                case GridStringId.FindControlFindButton:
                    return "My Find Text";
                case GridStringId.FindControlClearButton:
                    return "My Clear Text";
                case GridStringId.FilterPanelCustomizeButton:
                    return "My Edit Filter";
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
}