/************************************************************************************************************************
 *                                              Form Name  :frmCroppingImage.cs
 *                                              Purpose    :All the events is fired for cropping and drawing outline
 *                                                          for cropping image and resizing the image base on the 
 *                                                          percentage 
 *                                              Author     : Carmel Raj M
 *                                              Created On :22-Oct-2013
 * 
 * **********************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using DevExpress.XtraEditors.ViewInfo;

namespace ACPP.Modules.User_Management
{
    public partial class frmCroppingImage : frmFinanceBaseAdd
    {
        #region Variables
        int XAxis; int YAxis; int CropWidth = 190; int CropHeight = 210;
        Size OriginalImageSize; Size ModifiedImageSize;
        Bitmap OriginalImage;
        ImageProcessing crop = new ImageProcessing();
        #endregion

        #region Properties
        public static Bitmap AssignImage { set; get; }
        #endregion

        #region Default Constructor
        public frmCroppingImage()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmCroppingImage_Load(object sender, EventArgs e)
        {
            this.Text = GetMessage(MessageCatalog.Cropping.CROPPING_TITLE);
            peUserPhoto.Image = OriginalImage = AssignImage;
            OriginalImageSize = new Size(AssignImage.Width, AssignImage.Height);
            seChangeSize.Value = 100;
            ModifiedImageSize = new Size(0, 0);
            SetResizeInfo();
        }

        private void frmCroppingImage_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(crop.CurrentBitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, Convert.ToInt32(crop.CurrentBitmap.Width * 1.0), Convert.ToInt32(crop.CurrentBitmap.Height * 1.0)));
        }

        private void peUserPhoto_MouseMove(object sender, MouseEventArgs e)
        {
            double zoom = Convert.ToDouble(peUserPhoto.Properties.ZoomPercent) / 100;
            int w = peUserPhoto.Image.Width; int h = peUserPhoto.Image.Height;
            PictureEditViewInfo vi = peUserPhoto.GetViewInfo() as PictureEditViewInfo;
            XAxis = (int)((e.X - vi.PictureStartX) / zoom) - 91;
            YAxis = (int)((e.Y - vi.PictureStartY) / zoom) - 78;

            Image test = crop.DrawOutCropArea(AssignImage, XAxis, YAxis, CropWidth, CropHeight);
            peUserPhoto.Image = test;

        }

        private void frmCroppingImage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyValue)
                {
                    case 32: //Space
                        if (ShowConfirmationMessage(GetMessage(MessageCatalog.Cropping.CROPPING_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            int PicBoxPrevWidth = pePreviewPhoto.Width;
                            int PicBoxPrevHeight = pePreviewPhoto.Height;
                            pePreviewPhoto.Image = ImageProcessing.ResizeImage(crop.Crop(AssignImage, XAxis, YAxis, CropWidth, CropHeight), ref PicBoxPrevWidth, ref PicBoxPrevHeight);
                            pePreviewPhoto.Width = PicBoxPrevWidth;
                            pePreviewPhoto.Height = PicBoxPrevHeight;
                            CropWidth = 190;
                            CropHeight = 210;
                        }
                        break;
                    case 219: //219 indicate Opened bracket
                        if (CanSelect)
                        {
                            CropWidth = (CropWidth < 217) ? CropWidth += 3 : CropWidth;
                            CropHeight = (CropHeight < 235) ? CropHeight += 3 : CropHeight;
                            Image ResizeOut = crop.DrawOutCropArea(AssignImage, XAxis, YAxis, CropWidth, CropHeight);
                            peUserPhoto.Image = ResizeOut;
                        }
                        break;
                    case 221: //221 indicates Closed bracket
                        CropWidth = (CropWidth > 140) ? CropWidth -= 3 : CropWidth;
                        CropHeight = (CropHeight > 160) ? CropHeight -= 3 : CropHeight;
                        Image ResizeIn = crop.DrawOutCropArea(AssignImage, XAxis, YAxis, CropWidth, CropHeight);
                        peUserPhoto.Image = ResizeIn;
                        break;
                }
            }

            catch (Exception ee)
            {
                if (ee.Message.Contains(GetMessage(MessageCatalog.Cropping.CROPPING_EXCEPTION)))
                    ShowMessageBox(GetMessage(MessageCatalog.Cropping.CROPPING_EXCEPTION_MESSAGE));
                else
                    ShowMessageBox(ee.Message);
            }
        }

        private void btnCropSelect_Click(object sender, EventArgs e)
        {
            AssignPreviewImage();
        }
                
        private void seChangeSize_EditValueChanged(object sender, EventArgs e)
        {
            int percentage = 0;
            try
            {
                percentage = UtilityMember.NumberSet.ToInteger(seChangeSize.Value.ToString());
                ModifiedImageSize = new Size((OriginalImageSize.Width * percentage) / 100, (OriginalImageSize.Height * percentage) / 100);
                SetResizeInfo();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Bitmap bm_source = new Bitmap(OriginalImage);
            // Make a bitmap for the result.
            if (ModifiedImageSize.Width > 0 && ModifiedImageSize.Height > 0 && ModifiedImageSize.Width < 1925 && ModifiedImageSize.Height < 1085)
            {

                Bitmap bm_dest = new Bitmap(Convert.ToInt32(ModifiedImageSize.Width), Convert.ToInt32(ModifiedImageSize.Height));
                // Make a Graphics object for the result Bitmap.
                Graphics gr_dest = Graphics.FromImage(bm_dest);
                // Copy the source image into the destination bitmap.
                gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1);
                // Display the result.
                OriginalImage = bm_dest;
                peUserPhoto.Image = AssignImage = OriginalImage;
                peUserPhoto.Width = bm_dest.Width;
                peUserPhoto.Height = bm_dest.Height;
            }
            else
                ShowMessageBox(GetMessage(MessageCatalog.Cropping.CROPPING_IMAGE_RESIZE));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        private void SetResizeInfo()
        {
            lblOriginalSize.Text = OriginalImageSize.ToString();
            lblModifiedSize.Text = ModifiedImageSize.ToString();
        }

        private void AssignPreviewImage()
        {
            ImageProcessing.FinalCroppedImage = (Bitmap)pePreviewPhoto.Image;
            this.Close();
        }

        #endregion

       

    }
}
