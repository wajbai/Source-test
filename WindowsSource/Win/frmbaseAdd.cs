using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using Bosco.Utility;
using DevExpress.Utils;
using System.Text.RegularExpressions;
using ACPP.Modules.Transaction;
using DevExpress.XtraEditors.Controls;
using ACPP.Modules.Master;

namespace ACPP
{
    public partial class frmBaseAdd : frmBase
    {
        public frmBaseAdd()
        {
            InitializeComponent();
        }

        #region CommonInterface Properties
        public DateEdit dtBaseDate;
        public GridLookUpEdit dtBaseProject;
        public const string PROJECTTAG = "PROJECT";
        public const string DATETAG = "DATE";

        //This variable is used to return any value (may be recent added id or item or anything ) from add form
        public object ReturnValue;
        public DialogResult ReturnDialog = DialogResult.Cancel;

        #endregion

        //protected string ResponseMsg
        //{
        //    set
        //    {
        //        lblResponseMsg.Text = value;
        //    }
        //}

        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForLookUpEdit(LookUpEdit lkpEdit)
        {
            lkpEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(lkpEdit.EditValue.ToString()) ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForComboBoxEdit(ComboBoxEdit cboEdit)
        {
            cboEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(cboEdit.EditValue.ToString()) ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForCheckListBox(CheckedListBoxControl clstEdit)
        {
            clstEdit.Appearance.BorderColor = clstEdit.Items.Count == 0 ? Color.Red : Color.Empty;
        }


        protected void SetBorderColorForGridLookUpEdit(GridLookUpEdit glkpEdit)
        {
            glkpEdit.Properties.Appearance.BorderColor = glkpEdit.Text == string.Empty ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForDateTimeEdit(DateEdit dteEdit)
        {
            dteEdit.Properties.Appearance.BorderColor = dteEdit.Text == string.Empty && dteEdit.DateTime == DateTime.MinValue ? Color.Red : Color.Empty;
        }

        /*protected void ShowMessageBox(string Msg)
        {
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }*/

        protected override void ShowSuccessMessageOnToolTip(string Message)
        {
            ToolTipController toolTipControllerMessage = new ToolTipController();
            ToolTipControllerShowEventArgs args = toolTipControllerMessage.CreateShowArgs();
            args.ToolTip = Message;
            args.IconType = ToolTipIconType.Information;
            args.ToolTipType = ToolTipType.SuperTip;
            args.ToolTipLocation = ToolTipLocation.TopCenter;
            toolTipControllerMessage.ShowHint(args, this);
            toolTipControllerMessage.AutoPopDelay = 5000;
        }

        protected bool IsValidEmail(string EmailAddress)
        {
            bool IsValid = true;
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                var r = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");

                IsValid = r.IsMatch(EmailAddress);
            }
            return IsValid;
        }

        protected bool IsValidURL(string url)
        {
            bool IsValid = true;
            if (!string.IsNullOrEmpty(url))
            {
                var r = new Regex(@"http(s)://((([0-9a-zA-Z]([0-9a-zA-Z\-]*[0-9a-zA-Z])?\.)*[a-zA-Z]([0-9a-zA-Z\-]*[0-9a-zA-Z])?)|([0-2]?\d?\d\.[0-2]?\d?\d\.[0-2]?\d?\d\.[0-2]?\d?\d))?/[/a-zA-Z0-9$\-_.+!*'(),?:@&=]*");

                IsValid = r.IsMatch(url);
            }
            return IsValid;
        }

        private void frmBaseAdd_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void frmBaseAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
            }
        }

        private void frmBaseAdd_Load(object sender, EventArgs e)
        {
            SetEventHandlers(this);
        }
        /// <summary>
        /// Event occurs when the enter event fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessEnter(object sender, System.EventArgs e)
        {
            ((TextEdit)sender).BackColor = Color.LightBlue;
        }
        /// <summary>
        /// Event occurs when the Leave event fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessLeave(object sender, System.EventArgs e)
        {
            ((TextEdit)sender).BackColor = Color.Empty;
        }
        /// <summary>
        /// To find the focused control of its inherited form
        /// </summary>
        /// <param name="ctrlContainer"></param>
        private void SetEventHandlers(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl != null)
                {
                    if (ctrl is TextEdit)
                    {
                        ctrl.Enter += new System.EventHandler(this.ProcessEnter);
                        ctrl.Leave += new System.EventHandler(this.ProcessLeave);
                    }
                    if (ctrl is DateEdit && dtBaseDate == null)
                    {
                        dtBaseDate = ctrl as DateEdit;
                    }
                    if (ctrl is GridLookUpEdit && ctrl.Tag == "PR")
                    {
                        dtBaseProject = ctrl as GridLookUpEdit;
                    }
                    if (ctrl.HasChildren)
                    {
                        SetEventHandlers(ctrl);
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == Keys.F3)
            {
                TriggerChildControl(DATETAG);
            }
            if (KeyData == Keys.F5)
            {
                TriggerChildControl(PROJECTTAG);
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        public void TriggerChildControl(string Type)
        {
            switch (Type)
            {
                case PROJECTTAG:
                    if (dtBaseProject != null)
                    {
                        frmProjectSelection frmprojectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod,
                            UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId), this.AppSetting.UserProject);
                        frmprojectSelection.ShowDialog();
                        if (frmprojectSelection.ProjectName != string.Empty && frmprojectSelection.DialogResult == DialogResult.OK)
                        {
                            dtBaseProject.EditValue = this.AppSetting.UserProjectId;
                        }
                    }
                    break;
                case DATETAG:
                    if (dtBaseDate != null)
                    {
                        dtBaseDate.Focus();
                        dtBaseDate.Select();
                    }
                    break;
            }
        }
    }
}
