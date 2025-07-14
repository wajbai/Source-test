/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace Bosco.Utility.CommonMemberSet
{
    #region Grid Set

    public class GridSetMember
    {
        /*public string GetGridItem(System.Web.UI.WebControls.GridView gridView, string keyName,
               string checkControlId, SelectionType selectType)
        {
            string selectedIds = "";
            GetGridItem(gridView, keyName, checkControlId, selectType, out selectedIds);

            return selectedIds;
        }

        public List<string> GetGridItem(System.Web.UI.WebControls.GridView gridView, string keyName,
               string checkControlId, SelectionType selectType, out string selectedIds)
        {
            List<string> idList = new List<string>();
            string id = "";
            selectedIds = "";

            foreach (GridViewRow rowGrid in gridView.Rows)
            {
                id = gridView.DataKeys[rowGrid.RowIndex][keyName].ToString();
                CheckBox chkSelect = rowGrid.FindControl(checkControlId) as CheckBox;

                if (selectType == SelectionType.All || (selectType == SelectionType.Selected && chkSelect.Checked == true))
                {
                    idList.Add(id);
                    selectedIds += ((selectedIds != "") ? DelimiterBase.Comma : "") + id.ToString();
                }
                else if (selectType == SelectionType.Deselected && chkSelect.Checked == false)
                {
                    idList.Add(id);
                    selectedIds += ((selectedIds != "") ? DelimiterBase.Comma : "") + id.ToString();
                }
            }
            return idList;
        }

        public void SelectGridItem(System.Web.UI.WebControls.GridView gridView, string checkControlId, bool isSelectAll)
        {
            foreach (GridViewRow rowGrid in gridView.Rows)
            {
                CheckBox chkSelect = rowGrid.FindControl(checkControlId) as CheckBox;
                chkSelect.Checked = (isSelectAll) ? true : false;
            }
        }*/
        private NumberSetMember numberSet = null;
        public NumberSetMember NumberSet
        {
            get { if (numberSet == null) numberSet = new NumberSetMember(); return numberSet; }
        }

        /// <summary>
        /// On 19/10/2023, To attach copy operation in the grid
        /// </summary>
        /// <param name="grid"></param>
        public void AttachGridContextMenu(GridControl grid)
        {
            ContextMenuStrip context = new System.Windows.Forms.ContextMenuStrip();
            ToolStripItem toolmnu = context.Items.Add("Copy Value", global::Bosco.Utility.Properties.Resources.copy);
            toolmnu.ImageAlign = ContentAlignment.MiddleCenter;
            toolmnu.Tag = 0;
            toolmnu.ToolTipText = "Copy Selected/Active cell value";
            toolmnu.Click += new EventHandler(toolmnu_Click);

            toolmnu = context.Items.Add("Copy Row", global::Bosco.Utility.Properties.Resources.copy2);
            toolmnu.ImageAlign = ContentAlignment.MiddleCenter;
            toolmnu.Tag = 1;
            toolmnu.ToolTipText = "Copy Selected/Active row values";
            toolmnu.Click += new EventHandler(toolmnu_Click);

            toolmnu = context.Items.Add("Copy all Data", global::Bosco.Utility.Properties.Resources.copy1);
            toolmnu.ImageAlign = ContentAlignment.MiddleCenter;
            toolmnu.Tag = 2;
            toolmnu.ToolTipText = "Copy entire grid data";
            toolmnu.Click += new EventHandler(toolmnu_Click);
            context.Items.Add("-");

            toolmnu = context.Items.Add("Export Excel", global::Bosco.Utility.Properties.Resources.save);
            toolmnu.ImageAlign = ContentAlignment.MiddleCenter;
            toolmnu.Tag = 3;
            toolmnu.ToolTipText = "Export to Excel";
            toolmnu.Click += new EventHandler(toolmnu_Click);
            grid.ContextMenuStrip = context;

            /*if (grid.FocusedView != null)
            {
                GridView gv = grid.FocusedView as GridView;
                if (gv.RowCount > 0)
                {
                    grid.ContextMenuStrip = context;
                }
            }*/
        }

        /// <summary>
        /// On 19/10/2023, To attach copy operation events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolmnu_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ToolStripMenuItem mnutoolstrip = sender as ToolStripMenuItem;
                    Int32 options = numberSet.ToInteger(mnutoolstrip.Tag.ToString());
                    ContextMenuStrip contxmnu = mnutoolstrip.GetCurrentParent() as ContextMenuStrip;

                    if (contxmnu.SourceControl != null)
                    {
                        GridControl grd = contxmnu.SourceControl as GridControl;
                        if (grd.FocusedView != null)
                        {
                            GridView gv = grd.FocusedView as GridView;
                            Application.DoEvents();
                            switch (options)
                            {
                                case 0: //Copy focsued cell value
                                    {
                                        Clipboard.Clear();
                                        if (gv.FocusedColumn != null)
                                        {
                                            GridColumn gc = gv.FocusedColumn;
                                            string txt = "";
                                            if (gv.GetFocusedRowCellValue(gc) != null && !string.IsNullOrEmpty(gv.GetFocusedRowCellValue(gc).ToString()))
                                            {
                                                txt = gv.GetFocusedRowCellValue(gc).ToString();
                                                Clipboard.SetText(txt);
                                            }
                                        }
                                        break;
                                    }
                                case 1: //Copy focused row data
                                    {
                                        if (gv.GetFocusedRow() != null)
                                        {
                                            gv.CopyToClipboard();
                                        }
                                        break;
                                    }
                                case 2: //Copy focused full data
                                    {
                                        Int32 focusedrow = gv.FocusedRowHandle;
                                        gv.OptionsSelection.MultiSelect = true;
                                        gv.SelectAll();
                                        gv.CopyToClipboard();
                                        gv.OptionsSelection.MultiSelect = false;
                                        gv.FocusedRowHandle = focusedrow;
                                        break;
                                    }
                                case 3: // Export to xL
                                    {
                                        SaveFileDialog mySaving = new SaveFileDialog();
                                        mySaving.Title = "Export Excel File To";
                                        string Filename = "Report_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                                        Filename = Filename.Replace("-", "");
                                        mySaving.FileName = Filename;
                                        //or .xlsx *.xlsx
                                        mySaving.Filter = "Excel 97-2003 WorkBook|*.xls|Excel WorkBook|*.xlsx";
                                        if (mySaving.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                        {
                                            string file = mySaving.FileName;
                                            string Extension = Path.GetExtension(file);

                                            if (Extension.Equals(".xls"))
                                            {
                                                gv.ExportToXls(file);
                                            }
                                            else
                                            {
                                                grd.Text = "List of Vouchers";
                                                DevExpress.XtraPrinting.XlsxExportOptions advOptions = new DevExpress.XtraPrinting.XlsxExportOptions();
                                                advOptions.SheetName = "Exported from Data Grid";
                                                gv.ExportToXlsx(file);
                                            }
                                        }

                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
                //this.ShowMessageBoxError(err.Message);
            }
        }
    }

    #endregion
}
