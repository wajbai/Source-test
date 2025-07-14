using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Threading;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars.Ribbon;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.Model;
using Bosco.Model.UIModel;
using Bosco.Model.Setting;
using DevExpress.XtraGrid.Views.Grid;
using ACPP.Modules.TDS;
using Bosco.Model.TDS;
using Bosco.Utility.ConfigSetting;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmNetworkingSettings : frmFinanceBaseAdd
    {
        #region Declaration
        DataTable dtSetting = null;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public frmNetworkingSettings()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private bool ValidateNetworkingSettings()
        {
            bool isValidSetting = true;
            if (string.IsNullOrEmpty(txtThanksgivingSub.Text.Trim()))
            {
                //this.ShowMessageBox("Thanksgiving subject is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorNetworkSettings.NETWORKING_SETTING_THANKSGIVING_SUBJECT_EMPTY));
                isValidSetting = false;
                txtThanksgivingSub.Focus();
            }
            else if (string.IsNullOrEmpty(txtAppealSub.Text.Trim()))
            {
                //this.ShowMessageBox("Appeal subject is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorNetworkSettings.NETWORKING_SETTING_APPEAL_SUBJECT_EMPTY));

                isValidSetting = false;
                txtAppealSub.Focus();
            }
            else if (string.IsNullOrEmpty(txtWeddingSub.Text.Trim()))
            {
                //this.ShowMessageBox("Wedding subject is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorNetworkSettings.NETWORKING_SETTING_WEDDING_SUBJECT_EMPTY));

                isValidSetting = false;
                txtWeddingSub.Focus();
            }
            else if (string.IsNullOrEmpty(txtBirthdaySub.Text.Trim()))
            {
                //this.ShowMessageBox("Birthday subject is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorNetworkSettings.NETWORKING_SETTING_BIRTHDAY_SUBJECT_EMPTY));

                isValidSetting = false;
                txtBirthdaySub.Focus();
            }
            //if (string.IsNullOrEmpty(txtServerName.Text.Trim()))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.NetworkingSettings.NETWORKING_SETTINGS_SERVER_EMPTY));
            //    isValidSetting = false;
            //    txtServerName.Focus();
            //}
            ////else if (string.IsNullOrEmpty(glkBankAccount.Text))
            ////{
            ////    this.ShowMessageBox(this.GetMessage(MessageCatalog.Settings.SETTING_FOREIGN_BANKACCOUNT));
            ////    isValidSetting = false;
            ////    glkBankAccount.Focus();
            ////}
            //else if (string.IsNullOrEmpty(txtPort.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.NetworkingSettings.NETWORKING_SETTINGS_PORT_EMPTY));
            //    isValidSetting = false;
            //    txtPort.Focus();
            //}
            //else if (string.IsNullOrEmpty(txtSMTPUsername.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.NetworkingSettings.NETWORKING_SETTINGS_USERNAME_EMPTY));
            //    isValidSetting = false;
            //    txtSMTPUsername.Focus();
            //}
            //else if (string.IsNullOrEmpty(txtSMTPPassword.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.NetworkingSettings.NETWORKING_SETTINGS_PASSWORD_EMPTY));
            //    isValidSetting = false;
            //    txtSMTPPassword.Focus();
            //}
            //else if (string.IsNullOrEmpty(txtSMSUsername.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.NetworkingSettings.NETWORKING_SETTINGS_USERNAME_EMPTY));
            //    isValidSetting = false;
            //    txtSMSUsername.Focus();
            //}
            //else if (string.IsNullOrEmpty(txtSMSPassKey.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.NetworkingSettings.NETWORKING_SETTINGS_PASSKEY_EMPTY));
            //    isValidSetting = false;
            //    txtSMSPassKey.Focus();
            //}
            return isValidSetting;
        }
        private void SaveNetworkingSetting()
        {
            NetworkingSetting setting = new NetworkingSetting();
            DataView dvSetting = null;
            ISetting isetting;
            SimpleEncrypt.SimpleEncDec objencrypt = new SimpleEncrypt.SimpleEncDec();

            dvSetting = this.UtilityMember.EnumSet.GetEnumDataSource(setting, Sorting.Ascending);
            dtSetting = dvSetting.ToTable();
            try
            {
                if (dtSetting != null)
                {
                    dtSetting.Columns.Add("Value", typeof(string));
                    for (int i = 0; i < dtSetting.Rows.Count; i++)
                    {
                        NetworkingSetting SettingName = (NetworkingSetting)Enum.Parse(typeof(NetworkingSetting), dtSetting.Rows[i][1].ToString());
                        string Value = "";
                        switch (SettingName)
                        {
                            //case NetworkingSetting.ServerName:
                            //    {
                            //        Value = txtServerName.Text.Trim();
                            //        break;
                            //    }
                            //case NetworkingSetting.Port:
                            //    {
                            //        Value = txtPort.Text.Trim();
                            //        break;
                            //    }
                            //case NetworkingSetting.SMTPUsername:
                            //    {
                            //        Value = txtSMTPUsername.Text.Trim();
                            //        break;
                            //    }
                            //case NetworkingSetting.SMTPPassword:
                            //    {
                            //        Value = objencrypt.EncryptString(txtSMTPPassword.Text.Trim());
                            //        break;
                            //    }
                            case NetworkingSetting.ThanksGivingSubject:
                                {
                                    Value = txtThanksgivingSub.Text.Trim();
                                    break;
                                }
                            case NetworkingSetting.AppealSubject:
                                {
                                    Value = txtAppealSub.Text.Trim();
                                    break;
                                }
                            case NetworkingSetting.WeddingdaySubject:
                                {
                                    Value = txtWeddingSub.Text.Trim();
                                    break;
                                }
                            case NetworkingSetting.BirthdaySubject:
                                {
                                    Value = txtBirthdaySub.Text.Trim();
                                    break;
                                }
                            //case NetworkingSetting.SMSUserName:
                            //    {
                            //        Value = txtSMSUsername.Text.Trim();
                            //        break;
                            //    }
                            //case NetworkingSetting.SMSPassKey:
                            //    {
                            //        Value = objencrypt.EncryptString(txtSMSPassKey.Text.Trim());
                            //        break;
                            //    }
                            //case NetworkingSetting.SenderId:
                            //    {
                            //        Value = txtSenderId.Text.Trim();
                            //        break;
                            //    }
                        }
                        dtSetting.Rows[i][2] = Value;
                    }
                    if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) == 1)
                    {
                        isetting = new GlobalSetting();
                        resultArgs = isetting.SaveSetting(dtSetting);
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateNetworkingSettings())
                {
                    SaveNetworkingSetting();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNetworkingSettings_Load(object sender, EventArgs e)
        {
            this.Height = this.Height - (lcgSMTP.Height + lcgSMSConfiguration.Height) + 5;
            this.CenterToScreen();
            txtThanksgivingSub.Text = this.AppSetting.ThanksGivingSubject;
            txtAppealSub.Text = this.AppSetting.AppealSubject;
            txtWeddingSub.Text = this.AppSetting.WeddingdaySubject;
            txtBirthdaySub.Text = this.AppSetting.BirthdaySubject;
        }
    }
}