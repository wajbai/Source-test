using System;
using System.Data;

using Bosco.Utility;
using Bosco.Model.Setting;


namespace ACPP.Modules
{
    public partial class TDSSettings  : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        DataTable dtSetting = new DataTable();
        //DataTable dtUISetting = null;
        DataView dtSettings = new DataView();
        #endregion

        #region Property
        #endregion

        #region Constructors
        public TDSSettings()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void BindValues()
        {
            ISetting isetting;
            isetting = new GlobalSetting();
            ResultArgs resultArg = isetting.FetchSettingDetails(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));
            if (resultArg.Success && resultArg.DataSource.TableView != null && resultArg.DataSource.TableView.Count != 0)
            {
                dtSettings = resultArg.DataSource.TableView;
                dtSetting = dtSettings.ToTable();

                foreach (DataRow drsetting in dtSetting.Rows)
                {
                    TDSSetting SettingName = (TDSSetting)Enum.Parse(typeof(TDSSetting), drsetting["Name"].ToString());
                    string Value = drsetting["Value"].ToString();

                    switch (SettingName)
                    {
                        case TDSSetting.TDSEnabled:
                            {
                                chkTDS.Checked = Value == "1" ? true : false;
                                break;
                            }
                        case TDSSetting.EnableBookingAtPayment:
                            {
                                chkTDSBooking.Checked = Value == "1" ? true : false;
                                break;
                            }
                    }
                }
            }
        }

        private void SaveGlobalSetting()
        {
            TDSSetting setting = new TDSSetting();
            DataView dvSetting = null;
            dvSetting = this.UtilityMember.EnumSet.GetEnumDataSource(setting, Sorting.Ascending);
            dtSetting = dvSetting.ToTable();
            if (dtSetting != null)
            {
                dtSetting.Columns.Add("Value", typeof(string));
                dtSetting.Columns.Add("UserId", typeof(string));
                for (int i = 0; i < dtSetting.Rows.Count; i++)
                {
                    TDSSetting SettingName = (TDSSetting)Enum.Parse(typeof(TDSSetting), dtSetting.Rows[i][1].ToString());
                    string Value = "";
                    switch (SettingName)
                    {

                        case TDSSetting.TDSEnabled:
                            {
                                Value = chkTDS.Checked == true ? "1" : "0";
                                break;
                            }
                        case TDSSetting.EnableBookingAtPayment:
                            {
                                Value = chkTDSBooking.Checked == true ? "1" : "0";
                                break;
                            }
                    }
                    dtSetting.Rows[i][2] = Value;
                    dtSetting.Rows[i][3] = this.LoginUser.LoginUserId;
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void ApplySetting()
        {
            ISetting isetting;
            isetting = new GlobalSetting();
            ResultArgs resultArg = isetting.FetchSettingDetails(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));
            if (resultArg.Success && resultArg.DataSource.TableView != null && resultArg.DataSource.TableView.Count != 0)
            {
                dtSettings = resultArg.DataSource.TableView;
                this.UIAppSetting.UISettingInfo = resultArg.DataSource.TableView;
            }
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ISetting isetting;
                SaveGlobalSetting();
                if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) == 1)
                {
                    isetting = new GlobalSetting();
                    resultArgs = isetting.SaveSetting(dtSetting);
                }
                else
                {
                    isetting = new UISetting();
                    resultArgs = isetting.SaveSetting(dtSetting);
                }
                if (resultArgs.Success)
                {
                    ApplySetting();
                    //this.ShowSuccessMessage("Saved");
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_TDS_SETTING_SUCCESS_INFO));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void TDSSettings_Load(object sender, EventArgs e)
        {
            BindValues();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}