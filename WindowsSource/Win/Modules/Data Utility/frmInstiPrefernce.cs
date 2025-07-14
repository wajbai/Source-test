using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Model.UIModel.Master;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using ACPP.Modules.User_Management;
using System.IO;
using Bosco.Model.UIModel;
using System.Reflection;
using DevExpress.Utils.Menu;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmInstiPrefernce : frmFinanceBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        SimpleEncrypt.SimpleEncDec objenc = new SimpleEncrypt.SimpleEncDec();
        DXPopupMenu menu;

        public byte[] ReportLogo { get; set; }
        string sPath;
        #endregion

        public frmInstiPrefernce()
        {
            InitializeComponent();
        }

        #region Methods
        /// <summary> 
        /// To Get the Institute Details
        /// </summary>
        public void GetInstituteDetails()
        {
            try
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    resultArgs = userSystem.FetchLogo();
                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                       ReportLogo = (byte[])resultArgs.DataSource.Table.Rows[0][userSystem.AppSchema.User.LOGOColumn.ColumnName];
                    }
                    peLogo.Image = ReportLogo == null ? global::ACPP.Properties.Resources.Default_Photo : ImageProcessing.ByteArrayToImage(ReportLogo);
                    this.AppSetting.AcMeERPLogo = ReportLogo == null ? ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo) : ReportLogo;
                }

                txtInstituteName.Text = SettingProperty.Current.InstituteName;
                txtContactPerson.Text = SettingProperty.Current.ContactPerson;
                meAddress.Text = SettingProperty.Current.Address;
                txtPlace.Text = SettingProperty.Current.Place;
                txtPhone.Text = SettingProperty.Current.Phone;
                txtState.Text = SettingProperty.Current.State;
                txtFax.Text = SettingProperty.Current.Fax;
                txtCountry.Text = SettingProperty.Current.CountryInfo;
                txtEmail.Text = SettingProperty.Current.Email;
                txtPincode.Text = SettingProperty.Current.PinCode;
                txtURL.Text = SettingProperty.Current.URL;
                txtLicenseNo.Text = objenc.DecryptString(SettingProperty.Current.LicenseKeyNumber);
                txtSubsritontill.Text =
                    !string.IsNullOrEmpty(SettingProperty.Current.LicenseKeyExprDate) ?
                    this.UtilityMember.DateSet.ToDate(SettingProperty.Current.LicenseKeyExprDate, false).ToShortDateString() : string.Empty;

                txtBranchLocations.Text = txtBranchLocations.ToolTip = AppSetting.BranchLocations;
                lcLocation.Text = AppSetting.Location;
                txtMultiDB.Text = (AppSetting.MultiLocation==1?"Yes": "No");
                txtLockMaster.Text = (AppSetting.LockMasters == 1 ? "Yes" : "No");
                txtMapLedger.Text = (AppSetting.MapHeadOfficeLedger == 1 ? "Yes" : "No");
                txtAllowMultiCurrency.Text = (AppSetting.AllowMultiCurrency == 1 ? "Yes" : "No");
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        #region Events
        private void frmInstiPrefernce_Load(object sender, EventArgs e)
        {
            GetInstituteDetails();
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            byte[] Logo = null;
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.ico";
            if (DialogResult.OK == file.ShowDialog())
            {
                Bitmap Selectimage = new Bitmap(file.FileName);
                peLogo.Image = Selectimage;
                if (peLogo.Image == null)
                {
                    this.AppSetting.InstituteLogo = Logo = ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo as Bitmap);
                    this.AppSetting.AcMeERPLogo = ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo as Bitmap);
                }
                else
                {
                   //this.AppSetting.InstituteLogo = Logo = ImageProcessing.ImageToByteArray(Selectimage);
                   btnBrowseLogo_ClickExtracted();
                }
            }
        }

        private void btnBrowseLogo_ClickExtracted()
        {
            using (UserSystem userSystem = new UserSystem())
            {
                userSystem.ReportLogo = peLogo.Image == null ? null : ImageProcessing.ImageToByteArray(peLogo.Image as Bitmap); // ImageProcessing.ImageToByteArray(Selectimage);
                resultArgs = userSystem.DeleteLogo();
                if (resultArgs.Success && peLogo.Image != null)
                {
                    resultArgs = userSystem.SaveLogo();
                }
                if (resultArgs.Success)
                {
                    resultArgs = userSystem.FetchLogo();
                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        ReportLogo = (byte[])resultArgs.DataSource.Table.Rows[0][userSystem.AppSchema.User.LOGOColumn.ColumnName];
                    }
                    else
                    {
                        ReportLogo = null;
                    }
                                        
                    peLogo.Image = ReportLogo == null ? global::ACPP.Properties.Resources.Default_Photo : ImageProcessing.ByteArrayToImage(ReportLogo);
                    this.AppSetting.AcMeERPLogo = ReportLogo == null ? ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo) : ReportLogo;  // ImageProcessing.ImageToByteArray(peLogo.Image as Bitmap);
                }
            }
        }

        private void DeleteLogo()
        {
            this.AppSetting.InstituteLogo = ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo as Bitmap);
            this.AppSetting.AcMeERPLogo = ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo as Bitmap);
            peLogo.Image = null;
            btnBrowseLogo_ClickExtracted();
        }
        
        private void peLogo_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit edit = sender as PictureEdit;
            if (menu == null)
            {
                PropertyInfo info = typeof(PictureEdit).GetProperty("Menu", BindingFlags.NonPublic | BindingFlags.Instance);
                menu = info.GetValue(edit, null) as DXPopupMenu;
                foreach (DXMenuItem item in menu.Items)
                {
                    item.Visible = false;
                    if (item.Caption == "Delete")
                    {
                        item.Visible = true;
                        item.Click += new EventHandler(item_Click);
                    }
                }
            }  
        }

        private void item_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to delete Institute Logo ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                DeleteLogo();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to delete Institute Logo ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                DeleteLogo();
            }
        }



    }
}