using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

namespace Bosco.Utility
{

    public class clsBaseControl
    {
        private Control control;

        private string sCleanValue;
        //following are the properites used to get the values that are assigned to it.
        public Control Control
        {
            get
            {
                return control;
            }
        }
        public string CleanValue
        {
            get
            {
                return sCleanValue;
            }
        }
        // It establishes the controls and uses its current value as "clean"
        public clsBaseControl(Control ctl)
        {
            if (clsBaseControl.IsControlTypeSupported(ctl))
            {
                control = ctl;
            }
            else// if the control type is not supported, throw an exception
            {
                throw new NotSupportedException(string.Format("The control type for '{0}' is not supported by the ControlDirty class.", ctl.Name));
            }
        }
        // method to establish the the current control value as "clean"
        public void EstablishValueAsClean(int i)
        {
            sCleanValue = GetControlCurrentValue(i);
        }
        // compare the current values. If the current values are same with the previous value then the form is clean. Else the vaules are changed and return the values
        public bool DetermineIfDirty(int i)
        {
            return (string.Compare(sCleanValue, GetControlCurrentValue(i), false) != 0);
        }
        // list of controls supported
        public static bool IsControlTypeSupported(Control ctlControl)
        {
            // list of types supported
            if (ctlControl is DevExpress.XtraEditors.LookUpEdit) return true;
            else if (ctlControl is DevExpress.XtraEditors.TextEdit) return true;
            else if (ctlControl is DevExpress.XtraEditors.DateEdit) return true;
            else if (ctlControl is DevExpress.XtraEditors.ComboBoxEdit) return true;
            else if (ctlControl is DevExpress.XtraEditors.CheckEdit) return true;
            else if (ctlControl is DevExpress.XtraEditors.CheckedComboBoxEdit) return true;
            else if (ctlControl is DevExpress.XtraEditors.MemoEdit) return true;

            else if (ctlControl is DevExpress.XtraGrid.GridControl) return true;
            else if (ctlControl is DevExpress.XtraEditors.PictureEdit) return true;
            //else if (ctlControl is DevExpress.XtraEditors.RadioGroup) return true;
            else return false;
        }

        // It is to determine the current value as a string of theControls used in the form
        private string GetControlCurrentValue(int ip)
        {

            if (control is DevExpress.XtraEditors.LookUpEdit)
                return (control as DevExpress.XtraEditors.LookUpEdit).Text.ToString().Trim();
            else if (control is DevExpress.XtraEditors.MemoEdit)
                return (control as DevExpress.XtraEditors.MemoEdit).Text.ToString().Trim();
            else if (control is DevExpress.XtraEditors.TextEdit)
            {
                if (ip == 0)
                {
                    return (control as DevExpress.XtraEditors.TextEdit).Text.ToString().Trim();
                }
                else
                {
                    if (control.Visible == true)
                        return (control as DevExpress.XtraEditors.TextEdit).Text.ToString().Trim();
                    else
                        return "";
                }
            }
            else if (control is DevExpress.XtraEditors.DateEdit)
                return (control as DevExpress.XtraEditors.DateEdit).Text.ToString().Trim();
            else if (control is DevExpress.XtraEditors.ComboBoxEdit)
                return (control as DevExpress.XtraEditors.ComboBoxEdit).Text.ToString().Trim();
            else if (control is DevExpress.XtraEditors.CheckEdit)
                return (control as DevExpress.XtraEditors.CheckEdit).Checked.ToString().Trim();
            //else if (control is DevExpress.XtraEditors.RadioGroup)
            //    return (control as DevExpress.XtraEditors.RadioGroup).SelectedIndex.ToString();
            else if (control is DevExpress.XtraEditors.CheckedComboBoxEdit)
                return (control as DevExpress.XtraEditors.CheckedComboBoxEdit).Text.ToString().Trim();
            else if (control is DevExpress.XtraGrid.GridControl)
            {
                StringBuilder val = new StringBuilder();
                if ((control as DevExpress.XtraGrid.GridControl).Visible)
                {
                    DataTable dt = (DataTable)((control as DevExpress.XtraGrid.GridControl).DataSource);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                                {
                                    if (!dt.Columns[j].ToString().Contains("Patient"))
                                        val.AppendFormat("{0};", dt.Rows[i][j].ToString().Trim());
                                }
                            }
                        }
                    }
                }
                return val.ToString();
            }
            else if (control is DevExpress.XtraEditors.PictureEdit)
            {
                return (control as DevExpress.XtraEditors.PictureEdit).Text.ToString().Trim();
            }
            else
                return "";
            // ... add additional types as desired ...

        }

    }
    public class clsControlDirtyCollection : List<clsBaseControl>
    {
        public clsControlDirtyCollection(Form frm)
            : base()
        {
            AddControlsFromForm(frm); // Add the controls from a Form
        }
        // Add the controls from a Form 
        public void AddControlsFromForm(Form frm)
        {
            AddControlsFromCollection(frm.Controls);
        }
        // if the control is supported for dirty tracking, add it and check for the inner collection
        public void AddControlsFromCollection(Control.ControlCollection coll)
        {
            foreach (Control c in coll)
            {
                if (clsBaseControl.IsControlTypeSupported(c))
                    this.Add(new clsBaseControl(c));

                if (c.HasChildren)
                    AddControlsFromCollection(c.Controls);
            }
        }
        // get the list of controls that are dirty
        public List<Control> GetListOfDirtyControls(int i)
        {
            List<Control> list = new List<Control>();
            foreach (clsBaseControl c in this)
            {
                if (c.DetermineIfDirty(i))
                    list.Add(c.Control);
            }
            return list;
        }
        // make the tracked controls as clean
        public void MarkAllControlsAsClean(int i)
        {
            foreach (clsBaseControl c in this)
                c.EstablishValueAsClean(i);
        }
    }
    public class DirtyControls
    {
        private XtraForm objForm;
        private clsControlDirtyCollection objControl;
        // it is used to check whether the form is clean or dirty;
        public bool IsDirty
        {
            get
            {
                List<Control> dirtyControls = objControl.GetListOfDirtyControls(0);
                return (dirtyControls.Count > 0);
            }
        }
        // it is used to check whether the form is clean or dirty;for hidden controls also
        public bool IsDirty1
        {
            get
            {
                List<Control> dirtyControls = objControl.GetListOfDirtyControls(1);
                return (dirtyControls.Count > 0);
            }
        }
        //// access the list of controls when the values are changed
        //public List<Control> GetListOfDirtyControls()
        //{
        //    return objControl.GetListOfDirtyControls();
        //}
        // set the value in the  the form as "clean" with whatever current control  values exist
        public void MarkAsClean()
        {
            objControl.MarkAllControlsAsClean(0);
        }
        // set the value in the  the form as "clean" with whatever current control  values exist,for hidden controls 
        public void MarkAsClean1()
        {
            objControl.MarkAllControlsAsClean(1);
        }
        // assign the controls to track
        public DirtyControls(XtraForm frm)
        {
            objForm = frm;
            objControl = new clsControlDirtyCollection(frm);
        }

        public DialogResult CloseForm(string sTitle)
        {
            EditResLocalizer.Active = new MyEditLocalizer();
            DialogResult drSaveConfirm = DialogResult.None;
            drSaveConfirm = XtraMessageBox.Show("Data on this page has been changed.\nDo you want to save changes?", sTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            EditResLocalizer.Active = null;
            return drSaveConfirm;
        }
        public class MyEditLocalizer : EditResLocalizer
        {
            public override string GetLocalizedString(StringId id)
            {
                if (id == StringId.XtraMessageBoxYesButtonText)
                    return "Save";
                else if (id == StringId.XtraMessageBoxNoButtonText)
                    return "Don't Save";
                else /*if (id== StringId.XtraMessageBoxCancelButtonText )*/
                    return "Cancel";
            }
        }
    }
}
