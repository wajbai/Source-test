using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


using Bosco.Utility;
using Bosco.Model.Setting;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using ACPP.Modules.Data_Utility;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.Utils.Menu;
using System.Reflection;


namespace ACPP.Modules
{
    public partial class frmRolesWithSignSetting : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        private frmFinanceBaseAdd parentfrm;
        #endregion

        #region Property
        private Int32 ProjectId
        {
            get
            {
                Int32 id = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                return id;
            }

        }
        #endregion

        #region Constructors
        public frmRolesWithSignSetting(frmFinanceBaseAdd fromParent)
        {
            InitializeComponent();
            parentfrm = fromParent;
        }

        #endregion

        #region Methods

        private void LoadDefaults()
        {
            lcgSignatureFY.Text = "Signature with Role details for " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom,false).ToString("MMM yyyy") + " - " +
                                                                     UtilityMember.DateSet.ToDate(this.AppSetting.YearTo,false).ToString("MMM yyyy") ;
            LoadProject(glkpProject);

            LoadSignDetails();

            //Show Approved sing images only for cmf congregation's branches
            lcgSign4.Visibility = lcgSign5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.Height = 335;//300;
            if (AppSetting.IS_CMF_CONGREGATION || AppSetting.IS_BSG_CONGREGATION) //On 28/02/2024, To set sign details for montfort pune
            {
                lcgSign4.Visibility = lcgSign5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lcgSign4.Enabled = lcgSign5.Enabled = false;
                this.Height = 450;
                this.CenterToScreen();
            }
            else
            {
                if (parentfrm != null)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(parentfrm.Left, parentfrm .Top+ 120);
                }
            }
            
        }

        private void LoadSignDetails()
        {
            using (UISetting uisetting = new UISetting())
            {
               ResultArgs resultArgs =  uisetting.FetchReportSignDetails();

               if (resultArgs.Success && resultArgs.DataSource.Table != null)
               {
                   DataTable dtAcYearSignDetails = resultArgs.DataSource.Table;
                   dtAcYearSignDetails.DefaultView.RowFilter =  uisetting.AppSchema.ReportSign.PROJECT_IDColumn.ColumnName + "=" + ProjectId;
                   dtAcYearSignDetails = dtAcYearSignDetails.DefaultView.ToTable();
                   txtRoleName1.Text = txtRole1.Text = string.Empty;
                   txtRoleName2.Text = txtRole2.Text = string.Empty;
                   txtRoleName3.Text = txtRole3.Text = string.Empty;
                   //txtRoleName4.Text = txtRole4.Text = string.Empty;
                   //txtRoleName5.Text = txtRole5.Text = string.Empty;
                   
                   txtSignNoate.Text = string.Empty;
                   cbSignNoteLocation.SelectedIndex = 0;
                   comboBoxEdit1.SelectedIndex = 1;
                   chkHideSignNoteInFooter.Checked = false;
                   picSign1.Image = picSign2.Image = picSign3.Image = null; 

                   if (dtAcYearSignDetails.DefaultView.Count > 0)
                   {
                       foreach (DataRow dr in dtAcYearSignDetails.Rows)
                       {
                           Int32 signorder =  UtilityMember.NumberSet.ToInteger(dr[uisetting.AppSchema.ReportSign.SIGN_ORDERColumn.ColumnName].ToString());
                           string rolename = dr[uisetting.AppSchema.ReportSign.ROLE_NAMEColumn.ColumnName].ToString().Trim();
                           string role = dr[uisetting.AppSchema.ReportSign.ROLEColumn.ColumnName].ToString().Trim();
                           byte[] signimage = null;
                           if (dr[uisetting.AppSchema.ReportSign.SIGN_IMAGEColumn.ColumnName] != null && dr[uisetting.AppSchema.ReportSign.SIGN_IMAGEColumn.ColumnName] != DBNull.Value)
                           {
                               signimage = (byte[])dr[uisetting.AppSchema.ReportSign.SIGN_IMAGEColumn.ColumnName];
                           }

                           Int32 HideRequireSignNote = UtilityMember.NumberSet.ToInteger(dr[uisetting.AppSchema.ReportSign.HIDE_REQUIRE_SIGN_NOTEColumn.ColumnName].ToString());
                           string signnote = dr[uisetting.AppSchema.ReportSign.SIGN_NOTEColumn.ColumnName].ToString().Trim();
                           Int32 signnotelocation = UtilityMember.NumberSet.ToInteger(dr[uisetting.AppSchema.ReportSign.SIGN_NOTE_LOCATIONColumn.ColumnName].ToString());
                           Int32 signnotealignment = UtilityMember.NumberSet.ToInteger(dr[uisetting.AppSchema.ReportSign.SIGN_NOTE_ALIGNMENTColumn.ColumnName].ToString());
                           if (HideRequireSignNote == 1)
                           {
                               chkHideSignNoteInFooter.Checked = true;
                           }

                           if (!string.IsNullOrEmpty(signnote))
                           {
                               txtSignNoate.Text = signnote;
                               cbSignNoteLocation.SelectedIndex = signnotelocation;
                               comboBoxEdit1.SelectedIndex = signnotealignment;
                           }

                           switch(signorder)
                           {
                               case 1:
                                   txtRoleName1.Text = rolename;
                                   txtRole1.Text = role;
                                   if (signimage != null)
                                   {
                                       picSign1.Image = ImageProcessing.ByteArrayToImage(signimage);
                                   }
                                   break;
                               case 2:
                                   txtRoleName2.Text = rolename;
                                   txtRole2.Text = role;
                                   if (signimage != null)
                                   {
                                       picSign2.Image = ImageProcessing.ByteArrayToImage(signimage);
                                   }
                                   break;
                               case 3:
                                   txtRoleName3.Text = rolename;
                                   txtRole3.Text = role;
                                   if (signimage != null)
                                   {
                                       picSign3.Image = ImageProcessing.ByteArrayToImage(signimage);
                                   }
                                   break;
                               case 4:
                                   txtRoleName4.Text = rolename;
                                   txtRole4.Text = role;
                                   if (signimage != null)
                                   {
                                       picSign4.Image = ImageProcessing.ByteArrayToImage(signimage);
                                   }
                                   break;
                               case 5:
                                   txtRoleName5.Text = rolename;
                                   txtRole5.Text = role;
                                   if (signimage != null)
                                   {
                                       picSign5.Image = ImageProcessing.ByteArrayToImage(signimage);
                                   }
                                   break;
                           }

                       }
                   }

               }
            }

            txtRoleName1.Select();
            txtRoleName1.Focus();
        }
        #endregion

        #region Events

        private void frmFinanceSetting_Load(object sender, EventArgs e)
        {
            LoadDefaults();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (UISetting uisetting = new UISetting())
                {
                    DataTable dtSignDetails = getSignDetails();
                    Int32 hideReportSignNote = (chkHideSignNoteInFooter.Checked?1:0);
                    ResultArgs resultarg = uisetting.SaveSignDetails(dtSignDetails, ProjectId, hideReportSignNote, txtSignNoate.Text, 
                        comboBoxEdit1.SelectedIndex, cbSignNoteLocation.SelectedIndex);

                    if (resultarg.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        LoadSignDetails();
                    }
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
        #endregion

        private void btnSign1_Click(object sender, EventArgs e)
        {
            SetSignImage(1);
        }

        private void btnSign3_Click(object sender, EventArgs e)
        {
            SetSignImage(2);
        }

        private void btnSign2_Click(object sender, EventArgs e)
        {
            SetSignImage(3);
        }

        private void SetSignImage(Int32 signnumber)
        {
            //string sign = Path.Combine(AcmeerpInstalledPath, "Sign" + signnumber + ".jpg");
            OpenFileDialog openfileSign = new OpenFileDialog();
            //openfileSign.InitialDirectory = AcmeerpInstalledPath;
            openfileSign.Title = "Select Sign Image (Width <= 800 and Height <= 260)";

            //Filter the filedialog, so that it will show only the mentioned format images
            openfileSign.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.ico";

            if (openfileSign.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string signSelected = openfileSign.FileName;
                if (!string.IsNullOrEmpty(signSelected))
                {
                    //Bitmap Selectimage = new Bitmap(signSelected);
                    //Bitmap signimage = new Bitmap(350, 200);
                    //Graphics g = Graphics.FromImage(signimage);
                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //g.FillRectangle(Brushes.White, 0, 0, 350, 200);
                    //g.DrawImage(Selectimage, 0, 0, 350, 200);

                    Bitmap signimage = new Bitmap(signSelected);
                    FileInfo file = new FileInfo(signSelected);
                    double sizeInBytes = file.Length;
                    double filesize = Math.Round(sizeInBytes / 1024);
                    var imageHeight = signimage.Height;
                    var imageWidth = signimage.Width;

                    if (filesize > 50)
                    {
                        MessageRender.ShowMessage("Sign Image file size big, please select a file less than or equal 50 KB");
                    }
                    else if (imageWidth > 800 || imageHeight > 260)
                    {
                        MessageRender.ShowMessage("Sign Image file size must be (Width is <=800 and Height is <=260)");
                    }
                    else
                    {
                        byte[] byteSignImage = ImageProcessing.ImageToByteArray(signimage);
                        if (signnumber == 1)
                            picSign1.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                        else if (signnumber == 2)
                            picSign3.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                        else if (signnumber == 3)
                            picSign2.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                    }
                }
            }
        }

        private void LoadProject(GridLookUpEdit lkpProject)
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = AppSetting.YearFrom;
                    ResultArgs resultArgs = mappingSystem.FetchProjectsLookup();
                    lkpProject.Properties.DataSource = null;

                    Int32 projectId = (lkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(lkpProject.EditValue.ToString()) : 0;
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(lkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName, true, "-For Common-");
                        lkpProject.EditValue = (lkpProject.Properties.GetDisplayValueByKeyValue(projectId) != null ? projectId : lkpProject.Properties.GetKeyValue(0));
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private DataTable getSignDetails()
        {
            DataTable dtSignSetting = new DataTable();
            using (UISetting setting = new UISetting())
            {
                dtSignSetting = setting.AppSchema.ReportSign;

                //Sign 1
                DataRow dr = dtSignSetting.NewRow();
                dr[setting.AppSchema.ReportSign.SIGN_ORDERColumn] = 1;
                dr[setting.AppSchema.ReportSign.ROLE_NAMEColumn] = txtRoleName1.Text.Trim();
                dr[setting.AppSchema.ReportSign.ROLEColumn] = txtRole1.Text.Trim();
                dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = null;
                if (picSign1.Image != null)
                {
                    dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = ImageProcessing.ImageToByteArray(picSign1.Image as Bitmap);
                }
                dtSignSetting.Rows.Add(dr);

                //Sign 2
                dr = dtSignSetting.NewRow();
                dr[setting.AppSchema.ReportSign.SIGN_ORDERColumn] = 2;
                dr[setting.AppSchema.ReportSign.ROLE_NAMEColumn] = txtRoleName2.Text.Trim();
                dr[setting.AppSchema.ReportSign.ROLEColumn] = txtRole2.Text.Trim();
                dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = null;
                if (picSign2.Image != null)
                {
                    dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = ImageProcessing.ImageToByteArray(picSign2.Image as Bitmap);
                }
                dtSignSetting.Rows.Add(dr);

                //Sign 3
                dr = dtSignSetting.NewRow();
                dr[setting.AppSchema.ReportSign.SIGN_ORDERColumn] = 3;
                dr[setting.AppSchema.ReportSign.ROLE_NAMEColumn] = txtRoleName3.Text.Trim();
                dr[setting.AppSchema.ReportSign.ROLEColumn] = txtRole3.Text.Trim();
                dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = null;
                if (picSign3.Image != null)
                {
                    dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = ImageProcessing.ImageToByteArray(picSign3.Image as Bitmap);
                }
                dtSignSetting.Rows.Add(dr);

                //Approved Sign 4 and Sign 5 ------------------------------------------------------------------------------------------------
                if ((AppSetting.IS_BSG_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "BSGESP") ||
                   (AppSetting.IS_CMF_CONGREGATION && AppSetting.HeadofficeCode.ToUpper() == "CMFNED") )
                {
                    //Sign 4
                    dr = dtSignSetting.NewRow();
                    dr[setting.AppSchema.ReportSign.SIGN_ORDERColumn] = 4;
                    dr[setting.AppSchema.ReportSign.ROLE_NAMEColumn] = txtRoleName4.Text.Trim();
                    dr[setting.AppSchema.ReportSign.ROLEColumn] = txtRole4.Text.Trim();
                    dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = null;
                    if (picSign4.Image != null)
                    {
                        dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = ImageProcessing.ImageToByteArray(picSign4.Image as Bitmap);
                    }
                    dtSignSetting.Rows.Add(dr);

                    //Sign 5
                    dr = dtSignSetting.NewRow();
                    dr[setting.AppSchema.ReportSign.SIGN_ORDERColumn] = 5;
                    dr[setting.AppSchema.ReportSign.ROLE_NAMEColumn] = txtRoleName5.Text.Trim();
                    dr[setting.AppSchema.ReportSign.ROLEColumn] = txtRole5.Text.Trim();
                    dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = null;
                    if (picSign5.Image != null)
                    {
                        dr[setting.AppSchema.ReportSign.SIGN_IMAGEColumn] = ImageProcessing.ImageToByteArray(picSign5.Image as Bitmap);
                    }
                    dtSignSetting.Rows.Add(dr);
                }
                //---------------------------------------------------------------------------------------------------------------------------

            }
            return dtSignSetting;
        }

        private void HideContextMenu(PictureEdit picedit)
        {
            DXPopupMenu menu = null;
            if (menu == null)
            {
                PropertyInfo info = typeof(PictureEdit).GetProperty("Menu", BindingFlags.NonPublic | BindingFlags.Instance);
                menu = info.GetValue(picedit, null) as DXPopupMenu;
                foreach (DXMenuItem item in menu.Items)
                {
                    if (item.Caption.ToUpper() == "LOAD" || item.Caption.ToUpper() == "SAVE")
                    {
                        item.Visible = false;
                    }
                }
            }
        }

        //private bool ValidSignDetails()
        //{
        //    bool Rtn = false;

        //    Rtn = (!string.IsNullOrEmpty(txtRole1.Text) || !string.IsNullOrEmpty(txtRoleName1.Text) || picSign1.Image != null
        //                || !string.IsNullOrEmpty(txtRole2.Text) || !string.IsNullOrEmpty(txtRoleName2.Text) || picSign2.Image != null
        //                || !string.IsNullOrEmpty(txtRole3.Text) || !string.IsNullOrEmpty(txtRoleName3.Text) || picSign3.Image != null
        //                //|| !string.IsNullOrEmpty(txtRole4.Text.Trim()) || !string.IsNullOrEmpty(Name4) || ReportProperty.Current.Sign4Image != null
        //                //|| !string.IsNullOrEmpty(txtRole5.Text.Trim()) || !string.IsNullOrEmpty(Name5) || ReportProperty.Current.Sign5Image != null
        //                );

        //    if (Rtn)
        //    {
        //        //1. Check Valid roles (must be unique)
        //        if (!string.IsNullOrEmpty(txtRole1.Text.Trim()))
        //        {
        //            Rtn = !(txtRole1.Text == txtRole2.Text) && !(txtRole1.Text == txtRole3.Text);
        //        }

        //        if (!string.IsNullOrEmpty(txtRole2.Text.Trim()) && Rtn)
        //        {
        //            Rtn = !(txtRole2.Text == txtRole1.Text) && !(txtRole2.Text == txtRole3.Text);
        //        }

        //        if (!string.IsNullOrEmpty(txtRole3.Text.Trim()) && Rtn)
        //        {
        //            Rtn = !(txtRole3.Text == txtRole1.Text) && !(txtRole3.Text == txtRole2.Text);
        //        }
        //        if (!Rtn)
        //        {
        //            MessageRender.ShowMessage("Role should not be repeated for Current Finance Year");
        //            txtRole1.Focus();
        //        }
        //    }
        //    else
        //    {
        //        MessageRender.ShowMessage("Set Signature Details");
        //        txtRole1.Focus();
        //    }
        //    return Rtn;
        //}

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadSignDetails();
        }

        private void picSign1_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

        private void picSign2_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

        private void picSign3_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

    }
}