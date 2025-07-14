/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Data;



namespace Payroll.Utility.CommonMemberSet
{
    #region Combo Set

    public class ComboSetMember
    {
        public bool SelectComboItem(ComboBox dropDownCombo, string value, bool isValue)
        {
            bool isItem = false;

            if (isValue)
            {
                dropDownCombo.SelectedValue = value;
                isItem = (dropDownCombo.SelectedIndex > 0);
            }
            else
            {
                dropDownCombo.SelectedText = value;
                isItem = (dropDownCombo.SelectedIndex > 0);
            }

            return isItem;
        }

        public void BindDataCombo(ComboBox dropDownCombo, object dataSource, string listField, string valueField)
        {
            BindDataCombo(dropDownCombo, dataSource, listField, valueField, false, "");
        }

        private void BindDataCombo(ComboBox dropDownCombo, object dataSource, string listField, string valueField, bool isAddEmptyItem, string emptyItemName)
        {
            dropDownCombo.DataSource = dataSource;
            dropDownCombo.DisplayMember = listField;
            dropDownCombo.ValueMember = valueField;

            if (isAddEmptyItem)
            {
                //ComboBoxite
                dropDownCombo.Items.Insert(0, emptyItemName);
                //dropDownCombo.ite

                //System.Web.UI.WebControls.ListItem lstSelect = new System.Web.UI.WebControls.ListItem(emptyItemName, "");
                //dropDownCombo.Items.Insert(0, lstSelect);
            }
        }

        public void BindGridLookUpCombo(DevExpress.XtraEditors.GridLookUpEdit dropDownCombo, object dataSource, string listField, string valueField)
        {
            BindGridLookUpCombo(dropDownCombo, dataSource, listField, valueField, false, "");
        }

        public void BindGridLookUpComboEmptyItem(DevExpress.XtraEditors.GridLookUpEdit dropDownCombo, object dataSource, string listField, string valueField, bool isAddEmptyItem, string emptyItemName)
        {
            BindGridLookUpCombo(dropDownCombo, dataSource, listField, valueField, isAddEmptyItem, emptyItemName);
        }

        public DataTable AddEmptyItem(DataTable dtSource, string ValueField, string DisplayField, string EmptyItem)
        {
            DataRow dr = null;
            if (dtSource.Columns.Contains(ValueField) && dtSource.Columns.Contains(DisplayField))
            {
                dr = dtSource.NewRow();
                dr[ValueField] = 0;
                dr[DisplayField] = EmptyItem;
                dtSource.Rows.InsertAt(dr, 0);
            }
            return dtSource;
        }

        private void BindGridLookUpCombo(DevExpress.XtraEditors.GridLookUpEdit dropDownCombo, object dataSource, string listField, string valueField, bool isAddEmptyItem, string emptyItemName)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataSource;
            if (isAddEmptyItem)
            {
                DataRow dr = null;
                if (dt.Columns.Contains(valueField) && dt.Columns.Contains(listField))
                {
                    dr = dt.NewRow();
                    dr[valueField] = 0;
                    dr[listField] = "<--All-->";
                    dt.Rows.InsertAt(dr, 0);
                }
            }

            dropDownCombo.Properties.DataSource = dt;
            dropDownCombo.Properties.DisplayMember = listField;
            dropDownCombo.Properties.ValueMember = valueField;
        }



        public void BindGridLookUpCombo(DevExpress.XtraEditors.GridLookUpEdit dropDownCombo, object dataSource, string listField, string valueField, bool IsSelectAll)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataSource;
            if (IsSelectAll)
            {
                //dropDownCombo.
            }
            dropDownCombo.Properties.DataSource = dataSource;
            dropDownCombo.Properties.DisplayMember = listField;
            dropDownCombo.Properties.ValueMember = valueField;

        }

        public void BindRepositoryItemGridLookUpEdit(DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpEdit, object dataSource, string listField, string valueField)
        {
            rglkpEdit.DataSource = dataSource;
            rglkpEdit.DisplayMember = listField;
            rglkpEdit.ValueMember = valueField;
        }

        public void BindLookUpEditCombo(DevExpress.XtraEditors.LookUpEdit dropDownCombo, object dataSource, string listField, string valueField)
        {
            BindLookUpEditCombo(dropDownCombo, dataSource, listField, valueField, false, "");
        }

        private void BindLookUpEditCombo(DevExpress.XtraEditors.LookUpEdit dropDownCombo, object dataSource, string listField, string valueField, bool isAddEmptyItem, string emptyItemName)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataSource;
            dropDownCombo.Properties.DataSource = dataSource;
            dropDownCombo.Properties.DisplayMember = listField;
            dropDownCombo.Properties.ValueMember = valueField;

            if (isAddEmptyItem)
            {
            }
        }
        public void BindDataList(DevExpress.XtraEditors.ListBoxControl listBox, object dataSource, string listField, string valueField)
        {
            BindDataList(listBox, dataSource, listField, valueField, false);
        }

        public void BindDataList(DevExpress.XtraEditors.ListBoxControl listBox, object dataSource, string listField, string valueField, bool isAllNeeded)
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
    }
    #endregion
}
