/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Data;


namespace Payroll.Utility.CommonMemberSet
{
    #region List Set

    public class ListSetMember
    {
        //public string GetCheckListItem(ListBox listBox,
        //    SelectionType selectType, string delimiter, bool isValue, out int count)
        //{
        //    string selectedIds = "";
        //    count = 0;

        //    foreach (object item in listBox.Items)
        //    {
        //        if (selectType == SelectionType.All || (selectType == SelectionType.Selected && item.Selected == true))
        //        {
        //            if (isValue)
        //            {
        //                selectedIds += ((selectedIds != "") ? delimiter : "") + item.Value;
        //            }
        //            else
        //            {
        //                selectedIds += ((selectedIds != "") ? delimiter + " " : "") + item.Text;
        //            }
        //            count++;
        //        }
        //        else if (selectType == SelectionType.Deselected && item.Selected == false)
        //        {
        //            if (isValue)
        //            {
        //                selectedIds += ((selectedIds != "") ? delimiter : "") + item.Value;
        //            }
        //            else
        //            {
        //                selectedIds += ((selectedIds != "") ? delimiter + " " : "") + item.Text;
        //            }
        //            count++;
        //        }
        //    }
        //    return selectedIds;
        //}

        //public void SelectCheckListItem(CheckBoxList chkListBox,
        //    string selectedItems, string delimiter, bool isValue)
        //{
        //    string[] aSelectedItems = selectedItems.Split(delimiter.ToCharArray());

        //    foreach (string item in aSelectedItems)
        //    {

        //        if (isValue && chkListBox.Items.FindByValue(item) != null)
        //        {
        //            chkListBox.Items.FindByValue(item).Selected = true;
        //        }
        //        else if (chkListBox.Items.FindByText(item) != null)
        //        {
        //            chkListBox.Items.FindByText(item).Selected = true;
        //        }
        //    }
        //}

        //public bool SelectListItem(ListBox listBox, string value, bool isValue)
        //{
        //    bool isItem = false;

        //    if (isValue)
        //    {
        //        ListItem item = listBox.Items.FindByValue(value);
        //        if (item != null)
        //        {
        //            listBox.SelectedIndex = -1;
        //            item.Selected = true;
        //            isItem = true;
        //        }
        //    }
        //    else
        //    {
        //        ListItem item = listBox.Items.FindByText(value);
        //        if (item != null)
        //        {
        //            listBox.SelectedIndex = -1;
        //            item.Selected = true;
        //            isItem = true;
        //        }
        //    }

        //    return isItem;
        //}

        public void BindDataList(ListBox listBox, object dataSource, string listField, string valueField)
        {
            BindDataList(listBox, dataSource, listField, valueField, false);
        }

        public void BindDataList(ListBox listBox, object dataSource, string listField, string valueField, bool isAllNeeded)
        {

            listBox.DisplayMember = listField;
            listBox.ValueMember = valueField;
            listBox.DataSource = dataSource;

            //if (isAllNeeded)
            //{
            //    ListItem lstSelect = new ListItem(DefaultItemBase.AllWithLine, "0");
            //    listBox.Items.Insert(0, lstSelect);
            //}
        }
        public void BindCombo(ComboBox comboBox, object dataSource, string listField, string valueField)
        {
            BindComboBox(comboBox, dataSource, listField, valueField, false);
        }

        public void BindComboBox(ComboBox comboBox, object dataSource, string listField, string valueField, bool isAllNeeded)
        {

            DataTable dtSource = (DataTable)dataSource;
            if (isAllNeeded)
            {
                DataRow row = dtSource.NewRow();
                row[valueField] = 0;
                row[listField] = "All";
                dtSource.Rows.InsertAt(row, 0);
                dtSource.AcceptChanges();
            }
            comboBox.DisplayMember = listField;
            comboBox.ValueMember = valueField;
            comboBox.DataSource = dtSource;

            //if (isAllNeeded)
            //{
            //    ComboBox lstSelect = new ComboBox();
            //    lstSelect.Items.Insert(0, "All");
            //    comboBox.Items.Insert(0, lstSelect);
            //}
        }
        public void BindComboWithSpace(ComboBox comboBox, DataView dataSource, string listField, string valueField, bool isAllNeeded)
        {

            DataTable dtSource = dataSource.Table;
            if (isAllNeeded)
            {
                DataRow row = dtSource.NewRow();
                row[valueField] = 0;
                row[listField] = " ";
                dtSource.Rows.InsertAt(row, 0);
                dtSource.AcceptChanges();
            }
            comboBox.DisplayMember = listField;
            comboBox.ValueMember = valueField;
            comboBox.DataSource = dtSource;

            //if (isAllNeeded)
            //{
            //    ComboBox lstSelect = new ComboBox();
            //    lstSelect.Items.Insert(0, "All");
            //    comboBox.Items.Insert(0, lstSelect);
            //}
        }
        //public static void fillList(ListBox lst, object strSql, string strDisplayMember, string strValueMember)
        //{

        //    // resultArgs.DataSource.Table.TableName = "list";
        //    lst.DisplayMember = strDisplayMember;
        //    lst.ValueMember = strValueMember;
        //    lst.DataSource = resultArgs.DataSource.Table;
        //}

        //public void BindDataCheckList(CheckBoxList checkBoxList, object dataSource, string listField, string valueField)
        //{
        //    checkBoxList.DataSource = dataSource;
        //    checkBoxList.DataTextField = listField;
        //    checkBoxList.DataValueField = valueField;
        //    checkBoxList.DataBind();

        //} 
    }

    #endregion
}

