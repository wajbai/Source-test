//This is Custom control which is extended from build-in devexpress XtraGrid 
// with the following extended features (mainly used for multi check box selection). 
// As build-in devexpress xtragrid does not retain selected rows when sorting or filter.

//1. Retain selections when Sorting/Filtering
//2. Return selected rows as datatable

//Whenever user selected one row, store selected row's index into one local arry variable
//End of every selection or mouse down event, reset grid selection and select rows based on the values in local array
//RestoreSelection: is used to retain or reselect rows and store selected rows into the one local datatable variable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Bosco.Utility.Controls
{
    public partial class ucXtraGridMultiExtended : DevExpress.XtraGrid.GridControl
    {
        private string MultiCheckBoxColName = "DX$CheckboxSelectorColumn";  //by default check box column name
        private List<int> SelectedRows = new List<int>();   //store selected rows 
        private DataTable selecteddataSource = new DataTable("SelectedRows");    //selected rows as datatable

        public enum Order
        {
            Default,
            Option1,
            Option2,
            Option3
        }

        [DisplayName("ucSortOrder"), Description("Get/Set Sort order in grid"), Category("Data")]
        public Order SortOrder { get; set; }

        /// <summary>
        /// Attach SelectedDataSoure to this custom control
        /// </summary>
        [DisplayName("ucSelectedDataSoure"), Description("Get selected rows as datatable"), Category("Data")]
        public DataTable SelectedDataSource
        {
            get { return selecteddataSource; }
        }

        /// <summary>
        /// Set selected rows for already selected
        /// </summary>
        [DisplayName("ucSelectRow"), Description("Set selected row"), Category("Data")]
        public Int32 SelectRow
        {
            set
            {
                SelectedRows.Add(value);
            }
        }


        public ucXtraGridMultiExtended()
        {
            InitializeComponent();
        }

        private void ucXtraGridMultiExtended_Load(object sender, EventArgs e)
        {
            //get default grid view
            GridView gvMultiGridView = this.MainView as GridView;

            //initialise SelectedDataSource from gridview datasource
            if (gvMultiGridView.DataSource != null)
            {
                selecteddataSource = (gvMultiGridView.DataSource as DataView).ToTable().Clone();

                if (SelectedRows.Count > 0)
                {
                    RestoreSelection(gvMultiGridView);
                }
            }

            //Attach all events -------------------------------------------------------------------------------------------------------------------
            gvMultiGridView.OptionsSelection.MultiSelect = true;
            gvMultiGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gvMultiGridView.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            gvMultiGridView.ColumnFilterChanged += new EventHandler(gvMultiGridView_ColumnFilterChanged);
            gvMultiGridView.MouseDown += new MouseEventHandler(gvMultiGridView_MouseDown);
            gvMultiGridView.MouseUp += new MouseEventHandler(gvMultiGridView_MouseUp);
            gvMultiGridView.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(gvMultiGridView_SelectionChanged);
            //-------------------------------------------------------------------------------------------------------------------------------------
        }




        /// <summary>
        /// This is based event for this extented grid, it will store selected rows into local array variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMultiGridView_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.Column != null && hi.Column.FieldName.ToUpper() == MultiCheckBoxColName.ToUpper())
            {
                if (!hi.InRow)
                {
                    bool allSelected = view.DataController.Selection.Count == view.DataRowCount;
                    if (!allSelected)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            int sourceHandle = view.GetDataSourceRowIndex(i);
                            if (!SelectedRows.Contains(sourceHandle))
                                SelectedRows.Add(sourceHandle);
                        }
                    }
                    else SelectedRows.Clear();
                }
                else
                {
                    int sourceHandle = view.GetDataSourceRowIndex(hi.RowHandle);
                    if (!SelectedRows.Contains(sourceHandle))
                        SelectedRows.Add(sourceHandle);
                    else
                        SelectedRows.Remove(sourceHandle);
                }
            }
            RestoreSelection(view);
        }

        /// <summary>
        /// reset user selection, selection would be doing in RestoreSelection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMultiGridView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Refresh)
            {
                GridView gvMultiGridView = this.MainView as GridView;
                gvMultiGridView.UnselectRow(gvMultiGridView.FocusedRowHandle);
            }
        }

        /// <summary>
        /// When Filter, sort, Reset selected rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMultiGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            RestoreSelection(view);
        }

        /// <summary>
        /// When Filter, sort, Reset selected rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMultiGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            RestoreSelection(sender as GridView);
        }

        /// <summary>
        /// This method is used to reset grid view selections
        /// select rows based on local variable (these values are stored when user select)
        /// and add datarows into selecteddatasource datatable to maintain selected datasource
        /// </summary>
        /// <param name="view"></param>
        private void RestoreSelection(GridView view)
        {
            BeginInvoke(new Action(() =>
            {
                view.ClearSelection();
                selecteddataSource.Clear();
                for (int i = 0; i < SelectedRows.Count; i++)
                {
                    int rowhandle = view.GetRowHandle(SelectedRows[i]);
                    view.SelectRow(rowhandle);

                    DataRow dr = view.GetDataRow(rowhandle);
                    selecteddataSource.ImportRow(dr);
                }
            }));
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }


    }
}
