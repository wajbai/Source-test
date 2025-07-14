/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace Payroll.Utility.CommonMemberSet
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
    }

    #endregion
}
