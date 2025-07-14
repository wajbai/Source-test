using System;
using DevExpress.XtraEditors.Controls;

using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Bosco.Model.Setting;
using ACPP.Modules.Master;

namespace ACPP.Modules.User_Management
{
    public partial class frmAddNewUser : frmFinanceBaseAdd
    {
        #region Variables
        private int UserId = 0;
        private int DisableColoums = 0;
        private int ValidateUserId = 0;
        frmSettings Setting = new frmSettings();
        public event EventHandler UpdateHeld;
        bool IsUserPhotoDefault = false;
        ResultArgs resultArgs;
        #endregion

        #region Constructor
        public frmAddNewUser()
        {
            InitializeComponent();
        }
        public frmAddNewUser(int UserId, int UserProfile = 0)
            : this()
        {
            this.UserId = UserId;
            DisableColoums = UserProfile;
        }
        #endregion

        #region Events
        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            this.Text = SetTitle();
            LoadUserRole();
            AssignValues();
            ApplyUserRights();
        }

        private void ApplyUserRights()
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                if (CommonMethod.ApplyUserRights((int)UserRole.CreateUserRole) == 0)
                {
                    lkpUserRole.Properties.Buttons[1].Visible = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lkpUserRole_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmUserRoleAdd frmUser = new frmUserRoleAdd();
                    frmUser.ShowDialog();
                    if (frmUser.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadUserRole();
                        if (frmUser.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmUser.ReturnValue.ToString()) > 0)
                        {
                            lkpUserRole.EditValue = this.UtilityMember.NumberSet.ToInteger(frmUser.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateUserInput())
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    ResultArgs resultArgs;
                    SimpleEncrypt.SimpleEncDec objencrypt = new SimpleEncrypt.SimpleEncDec();
                    userSystem.FirstName = txtFirstName.Text.Trim();
                    userSystem.LastName = txtLastName.Text.Trim();
                    userSystem.UserName = txtUserName.Text.Trim();
                    userSystem.Gender = rgGender.SelectedIndex == 0 ? rgGender.SelectedIndex : (rgGender.SelectedIndex == 1 ? rgGender.SelectedIndex : rgGender.SelectedIndex);
                    userSystem.UserId = ValidateUserId = this.UserId.Equals(0) ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : this.UserId;
                    userSystem.Password = objencrypt.EncryptString(txtPassword.Text.Trim());
                    userSystem.RoleId = UtilityMember.NumberSet.ToInteger(lkpUserRole.EditValue.ToString());
                    userSystem.Address = txtmeAddress.Text.Trim();
                    userSystem.Email = txtEmailAddress.Text.Trim();
                    userSystem.MobileNo = txtContactNumber.Text.Trim();
                    userSystem.Notes = meNotes.Text;
                    userSystem.UserPhoto = IsUserPhotoDefault ? null : peUserPhoto.Image == null ? null : ImageProcessing.ImageToByteArray(CompressImage(peUserPhoto.Image as Bitmap));
                    resultArgs = userSystem.SaveUser();
                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        ClearControls();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }

                        ISetting isetting;
                        if (ValidateUserId == 0)
                        {
                            Setting.LoadTheme();
                            Setting.BindValues();
                            Setting.SaveUISetting(this.UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()));
                            isetting = new UISetting();
                            resultArgs = isetting.SaveSetting(Setting.dtUISetting);
                            if (resultArgs.Success)
                            {
                                isetting.ApplySetting();
                            }
                        }
                    }
                }
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtUserName);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPassword);
        }

        private void txtRetypePassword_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRetypePassword);
        }

        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtFirstName);
        }

        private void peUserPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (DialogResult.OK == file.ShowDialog())
            {
                Bitmap Selectimage = new Bitmap(file.FileName);
                //frmCroppingImage.AssignImage = Selectimage;
                //ImageProcessing RefCrop = new ImageProcessing();
                //frmCroppingImage cropImage = new frmCroppingImage();
                //cropImage.ShowDialog();
                //if (ImageProcessing.FinalCroppedImage != null)
                //{
                //    int Width = peUserPhoto.Width;
                //    int Height = peUserPhoto.Height;
                //    peUserPhoto.Image = ImageProcessing.ResizeImage(ImageProcessing.FinalCroppedImage, ref Width, ref Height);
                //    peUserPhoto.Width = Width;
                //    peUserPhoto.Height = Height;
                //}

                //Added by Carmel Raj on July-07-2015
                //Purpose: Create thumbnail Image of 139x169 fixed size
                Bitmap bmpOut = new Bitmap(136, 169);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, 136, 169);
                g.DrawImage(Selectimage, 0, 0, 136, 169);

                peUserPhoto.Image = bmpOut;
                IsUserPhotoDefault = false;
            }

        }

        #endregion

        #region Methods
        public void LoadUserRole()
        {
            using (ManageSecuritySystem manageSecurityManagement = new ManageSecuritySystem())
            {
                bool includeallroles = (DisableColoums == (int)UserVisibleOptions.DisableRights);
                resultArgs = manageSecurityManagement.FetchUserRoles(includeallroles);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(lkpUserRole, resultArgs.DataSource.Table, manageSecurityManagement.AppSchema.UserRole.USERROLEColumn.ColumnName, manageSecurityManagement.AppSchema.UserRole.USERROLE_IDColumn.ColumnName);
                    lkpUserRole.EditValue = lkpUserRole.Properties.GetKeyValue(0);
                }
            }
        }

        private bool ValidateUserInput()
        {
            bool IsSuccess = true;
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_FIRST_NAME_EMPTY));
                SetBorderColor(txtFirstName);
                txtFirstName.Focus();
                IsSuccess = false;
            }
            else if (string.IsNullOrEmpty(txtUserName.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_NAME_EMPTY));
                SetBorderColor(txtUserName);
                txtUserName.Focus();
                IsSuccess = false;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_PASSWORD_EMPTY));
                SetBorderColor(txtPassword);
                txtPassword.Focus();
                IsSuccess = false;
            }
            else if (string.IsNullOrEmpty(txtRetypePassword.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_CONFIRM_PASSWORD_EMPTY));
                SetBorderColor(txtRetypePassword);
                txtRetypePassword.Focus();
                IsSuccess = false;
            }
            else if (!txtPassword.Text.Equals(txtRetypePassword.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_NEW_PASSWORD_MISMATCH));
                SetBorderColor(txtPassword);
                txtRetypePassword.Text = txtPassword.Text = string.Empty;
                txtPassword.Focus();
                IsSuccess = false;
            }
            else
            {
                IsSuccess = IsValidEmail(txtEmailAddress.Text);
                if (!IsSuccess)
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                    txtEmailAddress.Focus();
                }
            }
            return IsSuccess;
        }

        private void ClearControls()
        {
            if (UserId.Equals(0))
            {
                txtUserName.Text = txtContactNumber.Text = meNotes.Text = txtPassword.Text = txtmeAddress.Text = txtContactNumber.Text = txtEmailAddress.Text =
                txtFirstName.Text = txtLastName.Text = txtRetypePassword.Text = string.Empty;
                rgGender.SelectedIndex = 0;
                peUserPhoto.Image = global::ACPP.Properties.Resources.Default_Photo;
            }
            txtFirstName.Focus();
        }

        private void AssignValues()
        {
            try
            {
                if (!UserId.Equals(0))
                {
                    SimpleEncrypt.SimpleEncDec objencrypt = new SimpleEncrypt.SimpleEncDec();
                    using (UserSystem userSystem = new UserSystem(UserId))
                    {
                        txtFirstName.Text = userSystem.FirstName;
                        txtLastName.Text = userSystem.LastName;
                        txtUserName.Text = userSystem.UserName;
                        txtPassword.Text = txtRetypePassword.Text = objencrypt.DecryptString(userSystem.Password);
                        rgGender.SelectedIndex = userSystem.Gender;
                        txtmeAddress.Text = userSystem.Address;
                        txtContactNumber.Text = userSystem.MobileNo;
                        lkpUserRole.EditValue = userSystem.RoleId;
                        txtEmailAddress.Text = userSystem.Email;
                        peUserPhoto.Image = userSystem.UserPhoto == null || userSystem.UserPhoto.Length == 0 ? global::ACPP.Properties.Resources.Default_Photo : ImageProcessing.ByteArrayToImage(userSystem.UserPhoto);
                        IsUserPhotoDefault = userSystem.UserPhoto == null || userSystem.UserPhoto.Length == 0 ? true : false;
                        meNotes.Text = userSystem.Notes;

                        if (DisableColoums == (int)UserVisibleOptions.DisableRights)
                        {
                            txtUserName.Enabled = false;
                            lkpUserRole.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageRender.ShowMessage(e.Message, true);
            }
            finally { }
        }

        private string SetTitle()
        {
            return UserId.Equals(0) ? this.GetMessage(MessageCatalog.User.USER_ADD_CAPTION) : this.GetMessage(MessageCatalog.User.USER_EDIT_CAPTION);
        }

        /// <summary>
        /// Compressing Bitmap images 
        /// </summary>
        /// <param name="image">UnCompress Bitmap Image</param>
        /// <returns>Compressed Bitmap Image</returns>
        //  Added by Carmel Raj M on July-07-2015
        private Bitmap CompressImage(Bitmap image)
        {
            long ImageSize;
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            Encoder myEncoder = Encoder.Quality;

            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(myEncoder, 50L);
            encoderParameters.Param[0] = encoderParameter;
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, jpgEncoder, encoderParameters);
            ImageSize = memoryStream.Length;
            image = (Bitmap)Image.FromStream(memoryStream);
            ImageSize = memoryStream.Length;
            return image;
        }

        //  Added by Carmel Raj M on July-07-2015
        private ImageCodecInfo GetEncoder(ImageFormat Format)
        {
            ImageCodecInfo Encoder = null;
            ImageCodecInfo[] Codec = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in Codec)
            {
                if (codec.FormatID == Format.Guid)
                {
                    Encoder = codec;
                }
            }
            return Encoder;
        }

        #endregion
    }
}