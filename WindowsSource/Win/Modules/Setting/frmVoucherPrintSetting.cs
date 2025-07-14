using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.Setting;
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules
{
    public partial class frmVoucherPrintSetting : frmFinanceBaseAdd
    {
        DefaultVoucherTypes voucherType = DefaultVoucherTypes.Receipt;

        /// <summary>
        /// Get and Assign Legal entity properties
        /// </summary>
        private string LegalEntityProperties
        {
            get
            {
                string legalproperties = string.Empty;
                if (voucherType == DefaultVoucherTypes.Receipt)
                {
                    chkListShowLegalDetails.RefreshEditValue();
                    List<object> selecteditems = chkListShowLegalDetails.Properties.Items.GetCheckedValues();

                    foreach (object item in selecteditems)
                    {
                        legalproperties += item.ToString() + ",";
                    }
                    legalproperties = legalproperties.TrimEnd(',');
                }
                //legalproperties = string.Empty;
                return legalproperties;
            }
            set
            {
                chkListShowLegalDetails.SetEditValue(value);
            }
        }

        
        public frmVoucherPrintSetting()
        {
            InitializeComponent();               
        }

        public frmVoucherPrintSetting(DefaultVoucherTypes defaultVoucherType):base()
        {
            voucherType = defaultVoucherType;
        }

        private void frmVoucherPrintSetting_Load(object sender, EventArgs e)
        {
            //Load default vouchers
            DefaultVoucherTypes defaultvouchertype = new DefaultVoucherTypes();
            DataTable dt = this.UtilityMember.EnumSet.GetEnumDataSource(defaultvouchertype, Sorting.None).ToTable();
            this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpVoucherType, dt, EnumColumns.Name.ToString(), EnumColumns.Id.ToString());
            
            if (dt.Rows.Count >0 )
            {
                lkpVoucherType.Text = voucherType.ToString();
            }

            //On 02/03/2021, to hide Row1, Row2 and Row3 Voucher Print settings and will have common Sign Details from Finance Settings) 
            lcgSign1.Visibility = lcgSign2.Visibility = lcgSign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //for Temp
            //lcShowCostCentre.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.Height = 265;// 230;
            this.CenterToScreen();
        }

        private void lkpVoucherType_EditValueChanged(object sender, EventArgs e)
        {
            lcListShowLegalDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if ((DefaultVoucherTypes)Enum.Parse(typeof(DefaultVoucherTypes), lkpVoucherType.Text) == DefaultVoucherTypes.Receipt)
            {
                LoadLegalEntityProperties();
                lcListShowLegalDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            lcShowCostCentre.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if ((DefaultVoucherTypes)Enum.Parse(typeof(DefaultVoucherTypes), lkpVoucherType.Text) == DefaultVoucherTypes.Receipt ||
                (DefaultVoucherTypes)Enum.Parse(typeof(DefaultVoucherTypes), lkpVoucherType.Text) == DefaultVoucherTypes.Payment ||
                (DefaultVoucherTypes)Enum.Parse(typeof(DefaultVoucherTypes), lkpVoucherType.Text) == DefaultVoucherTypes.Journal)
            {
                lcShowCostCentre.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            //Load selected voucher type's print setting
            LoadVoucherPrintSetting();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //Save selected vooucher type's printer setting
            SaveVoucherPrintSetting();
            this.Close();
        }

        /// <summary>
        /// Get selected voucher type's print setting from database
        /// </summary>
        private void LoadVoucherPrintSetting()
        {
            try
            {
                using (UISetting setting = new UISetting())
                {
                    DefaultVoucherTypes vouchertype =DefaultVoucherTypes.Receipt;
                    if (!string.IsNullOrEmpty(lkpVoucherType.Text))
                    {
                        vouchertype  = (DefaultVoucherTypes)Enum.Parse(typeof(DefaultVoucherTypes), lkpVoucherType.Text);
                    }
                    
                    rgbReportTitle.SelectedIndex = 0;
                    chkShowCostCentre.Checked = false;
                    //GEt selected voucher type's print setting
                    ResultArgs resultArgs = setting.FetchVoucherPrintSetting(vouchertype);
                    if (resultArgs.Success)
                    {
                        DataTable dt = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dt.Rows)
                        {
                            string settingname = dr[setting.AppSchema.Settings.SETTING_NAMEColumn.ColumnName].ToString();
                            string settingvalue = dr[setting.AppSchema.Settings.VALUEColumn.ColumnName].ToString();
                            AcmeReportSetting voucerprintsetting = (AcmeReportSetting)this.UtilityMember.EnumSet.GetEnumItemType(typeof(AcmeReportSetting), settingname);
                            switch (voucerprintsetting)
                            {
                                case AcmeReportSetting.VoucherPrintSign1Row1:
                                    txtSign1Row1.Text = settingvalue;
                                    break;
                                case AcmeReportSetting.VoucherPrintSign1Row2:
                                    txtSign1Row2.Text = settingvalue;
                                    break;
                                case AcmeReportSetting.VoucherPrintSign2Row1:
                                    txtSign2Row1.Text = settingvalue;
                                    break;
                                case AcmeReportSetting.VoucherPrintSign2Row2:
                                    txtSign2Row2.Text = settingvalue;
                                    break;
                                case AcmeReportSetting.VoucherPrintSign3Row1:
                                    txtSign3Row1.Text = settingvalue;
                                    break;
                                case AcmeReportSetting.VoucherPrintSign3Row2:
                                    txtSign3Row2.Text = settingvalue;
                                    break;
                                case AcmeReportSetting.VoucherPrintCaptionBold:
                                    chkCaptionFontStyle.Checked = (settingvalue == "1");
                                    break;
                                case AcmeReportSetting.VoucherPrintValueBold:
                                    chkValueBold.Checked = (settingvalue == "1");
                                    break;
                                case AcmeReportSetting.VoucherPrintShowLogo:
                                    chkLogo.Checked = (settingvalue == "1");
                                    break;
                                case AcmeReportSetting.VoucherPrintProject:
                                    chkProject.Checked = (settingvalue == "1");
                                    break;
                                case AcmeReportSetting.VoucherPrintReportTitleType:
                                    {
                                        if (settingvalue == "0" || settingvalue == "1")
                                        {
                                            rgbReportTitle.SelectedIndex = UtilityMember.NumberSet.ToInteger(settingvalue);
                                        }
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintReportTitleAddress:
                                    {
                                        if (settingvalue == "0" || settingvalue == "1")
                                        {
                                            rgbAddress.SelectedIndex = UtilityMember.NumberSet.ToInteger(settingvalue);
                                        }
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintIncludeSigns:
                                    {
                                        //Set Include sign details
                                        chkIncludeSign1.Checked = chkIncludeSign2.Checked = chkIncludeSign3.Checked = false;
                                        if (!String.IsNullOrEmpty(settingvalue))
                                        {
                                            string[] includesign = settingvalue.Split(',');
                                            foreach (string sign in includesign)
                                            {
                                                switch (sign)
                                                {
                                                    case "1":
                                                        chkIncludeSign1.Checked = true;
                                                        break;
                                                    case "2":
                                                        chkIncludeSign2.Checked = true;
                                                        break;
                                                    case "3":
                                                        chkIncludeSign3.Checked = true;
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintLegalEntityDetails:
                                    {
                                        LegalEntityProperties = settingvalue;
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintShowCostCentre:
                                    chkShowCostCentre.Checked = (settingvalue == "1");
                                    break;
                                case AcmeReportSetting.VoucherPrintHideVoucherReceiptNo:
                                    chkHideVoucherReceiptNo.Checked = (settingvalue == "1");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Save selected voucher type's print setting
        /// </summary>
        private void SaveVoucherPrintSetting()
        {
            try
            {
                AcmeReportSetting setting = new AcmeReportSetting();
                DataView dvSetting = null;
                dvSetting = this.UtilityMember.EnumSet.GetEnumDataSource(setting, Sorting.None);
                DataTable dtSetting = dvSetting.ToTable();
                using (UISetting settingsystem = new UISetting())
                {
                    if (dtSetting != null)
                    {
                        dtSetting.Columns.Add(settingsystem.AppSchema.Setting.ValueColumn.ColumnName, typeof(string));
                        dtSetting.Columns.Add("ReportId", typeof(string));
                        for (int i = 0; i < dtSetting.Rows.Count; i++)
                        {
                            AcmeReportSetting SettingName = (AcmeReportSetting)Enum.Parse(typeof(AcmeReportSetting), dtSetting.Rows[i][1].ToString());
                            string Value = "";
                            switch (SettingName)
                            {
                                case AcmeReportSetting.VoucherPrintSign1Row1:
                                    {
                                        Value = txtSign1Row1.Text;
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintSign1Row2:
                                    {
                                        Value = txtSign1Row2.Text;
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintSign2Row1:
                                    {
                                        Value = txtSign2Row1.Text;
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintSign2Row2:
                                    {
                                        Value = txtSign2Row2.Text;
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintSign3Row1:
                                    {
                                        Value = txtSign3Row1.Text;
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintSign3Row2:
                                    {
                                        Value = txtSign3Row2.Text;
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintCaptionBold:
                                    {
                                        Value = chkCaptionFontStyle.Checked ? "1" : "0";
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintValueBold:
                                    {
                                        Value = chkValueBold.Checked ? "1" : "0";
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintShowLogo:
                                    {
                                        Value = chkLogo.Checked ? "1" : "0";
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintProject:
                                    {
                                        Value = chkProject.Checked ? "1" : "0";
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintReportTitleType:
                                    {
                                        Value = rgbReportTitle.SelectedIndex.ToString();
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintReportTitleAddress:
                                    {
                                        Value = rgbAddress.SelectedIndex.ToString();
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintIncludeSigns:
                                    {
                                        Value = string.Empty;
                                        Value = chkIncludeSign1.Checked ? "1" : string.Empty;
                                        Value += (chkIncludeSign2.Checked ? (string.IsNullOrEmpty(Value) ? "" : ",") + "2" : string.Empty);
                                        Value += (chkIncludeSign3.Checked ? (string.IsNullOrEmpty(Value) ? "" : ",") + "3" : string.Empty);
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintLegalEntityDetails:
                                    {
                                        Value = string.Empty;
                                        Value = LegalEntityProperties;
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintShowCostCentre:
                                    {
                                        Value = "0";
                                        if (lcShowCostCentre.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                                        {
                                            Value = chkShowCostCentre.Checked ? "1" : "0";
                                        }
                                        break;
                                    }
                                case AcmeReportSetting.VoucherPrintHideVoucherReceiptNo:
                                    {
                                        Value = "0";
                                        if (lcHideVoucherReceiptNo.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                                        {
                                            Value = chkHideVoucherReceiptNo.Checked ? "1" : "0";
                                        }
                                        break;
                                    }
                            }
                            dtSetting.Rows[i][settingsystem.AppSchema.Setting.ValueColumn.ColumnName] = Value;
                            dtSetting.Rows[i]["ReportId"] = lkpVoucherType.Text;
                        }

                        ResultArgs resultarg = settingsystem.SaveVoucherPrintSetting(dtSetting);
                        if (resultarg.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        }
                        else
                        {
                            this.ShowSuccessMessage(resultarg.Message);
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally
            {
            }
        }

        private void LoadLegalEntityProperties()
        {
            using (LegalEntitySystem legalentity = new LegalEntitySystem())
            {
                using (CommonMethod cm = new CommonMethod())
                {
                    ResultArgs result = cm.FetchLegalEntityProperties();
                    if (result.Success)
                    {
                        DataTable dtLegalEntityProperties = result.DataSource.Table;
                        chkListShowLegalDetails.Properties.DataSource = dtLegalEntityProperties;
                        chkListShowLegalDetails.Properties.ValueMember = legalentity.AppSchema.LegalEntity.LEGALENTITY_FIELD_NAMEColumn.ColumnName;
                        chkListShowLegalDetails.Properties.DisplayMember = legalentity.AppSchema.LegalEntity.LEGALENTITY_DISPLAY_NAMEColumn.ColumnName;
                    }
                }
            }
        }

    }
}