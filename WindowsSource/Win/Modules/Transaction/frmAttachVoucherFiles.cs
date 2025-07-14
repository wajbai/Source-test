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
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors.Controls;
using Bosco.Model.Transaction;
using System.IO;
using Bosco.Utility.ConfigSetting;
using System.Drawing.Imaging;
using DevExpress.XtraBars;

namespace ACPP.Modules.Transaction
{
    public partial class frmAttachVoucherFiles : frmFinanceBaseAdd
    {
       #region Variables
        private Int32 VoucherId = 0;
        private bool IsViewVoucherFiles = true;
        DataTable dtAttachedBaseVoucherFiles = new DataTable();

        DataTable dtVoucherPdfFiles = new DataTable();
        DataTable dtVoucherImageFiles = new DataTable(); 
       #endregion

       #region Properties
        
        private string VoucherPdfActualFileName
        {
            get
            {
                string rtn = string.Empty;
                rtn = gvPdfVouchers.GetFocusedRowCellValue(colActualFileName) != null ? gvPdfVouchers.GetFocusedRowCellValue(colActualFileName).ToString() : "";
                return rtn;
            }
        }

        private string VoucherPdfFileName
        {
            get
            {
                string rtn = string.Empty;
                rtn = gvPdfVouchers.GetFocusedRowCellValue(colFileName) != null ? gvPdfVouchers.GetFocusedRowCellValue(colFileName).ToString() : "";
                return rtn;
            }
        }

        private string VoucherFileRefPath
        {
            get
            {
                string rtn = string.Empty;
                rtn = gvPdfVouchers.GetFocusedRowCellValue(colRefPath) != null ? gvPdfVouchers.GetFocusedRowCellValue(colRefPath).ToString() : "";
                return rtn;
            }
        }
        
        #endregion

        #region Default Constructor
        public frmAttachVoucherFiles()
        {
            InitializeComponent();
        }

        public frmAttachVoucherFiles(Int32 voucherId, DataTable dtattachedvoucherfiles):this()
        {
            IsViewVoucherFiles = false;
            VoucherId = voucherId;
            dtAttachedBaseVoucherFiles = dtattachedvoucherfiles;

            SetDefault();            
            LoadVoucherFiles();
            ShowVoucherFileDetails();
        }

        public frmAttachVoucherFiles(Int32 voucherId):this()
        {
            IsViewVoucherFiles = true;
            VoucherId = voucherId;
            SetDefault();
            if (voucherId > 0)
            {
                using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
                {
                    ResultArgs resultarg =  vsystem.FillVoucherDetails(voucherId);
                    if (resultarg.Success)
                    {
                        VoucherId = voucherId;
                        dtAttachedBaseVoucherFiles = vsystem.dtVoucherFiles;
                    }
                }
            }
            
            LoadVoucherFiles();
            ShowVoucherFileDetails();
        }

        #endregion
               
        #region Events
        private void picAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            try
            {
                file.Filter = "Image Files (*.Bmp, *.Jpg, *.Jpeg, *.Jpe, *.Jfif, *.Png, *.Gif) | *.Bmp; *.Jpg; *.Jpeg; *.Jpe; *.Jfif; *.Png; *.Gif |" +
                               "Pdf Files (*.Pdf) | *.Pdf";
                if (DialogResult.OK == file.ShowDialog())
                {
                    var length = new System.IO.FileInfo(file.FileName).Length;
                    //if (length > 500000)
                    //{
                    //    this.ShowMessageBox("Selected Voucher Image file size has exceeded its max limit of 5KB");
                    //}
                    ///*else if (imgsliderVoucherImageSlider.Images.Count == 2)
                    //{
                    //    this.ShowMessageBox("Only 2 Images alone can be attached per Voucher");
                    //}*/
                    //else
                    //{
                    if (Path.GetExtension(file.FileName) == ".pdf")
                    {
                        byte[] selectedArray = File.ReadAllBytes(file.FileName);
                        AttachVoucherLocalFiles(dtVoucherPdfFiles, Path.GetFileName(file.FileName), Path.GetFileName(file.FileName), selectedArray, string.Empty, file.FileName);
                        gcPdfVouchers.DataSource = dtVoucherPdfFiles;
                    }
                    else
                    {
                        Bitmap Selectimage = new Bitmap(file.FileName);
                        Selectimage = CompressImage(Selectimage);
                        byte[] selectedimagearray = ImageProcessing.ImageToByteArray(Selectimage);
                        Int32 idx = imgsliderVoucherImageSlider.Images.Add(Selectimage);
                        imgsliderVoucherImageSlider.Images[idx].Tag = Path.GetFileName(file.FileName);
                                                
                        //For Temp, to handle unhandled expection for tmp prupose, have to check and remove
                        imgsliderVoucherImageSlider.SetCurrentImageIndex(idx);
                        imgsliderVoucherImageSlider.SlideLast();
                                                
                        AttachVoucherLocalFiles(dtVoucherImageFiles, Path.GetFileName(file.FileName), Path.GetFileName(file.FileName), selectedimagearray, string.Empty, file.FileName);
                    }

                    ShowVoucherFileDetails();
                    //}
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }

        private void picRemoveCurrentImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (imgsliderVoucherImageSlider.CurrentImage != null)
                {
                    if (this.ShowConfirmationMessage("Are you sure to remove current image from the list ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        Int32 imageindex = imgsliderVoucherImageSlider.GetCurrentImageIndex();
                        if (dtVoucherImageFiles.Rows.Count >= imageindex)
                        {
                            dtVoucherImageFiles.Rows.RemoveAt(imageindex);
                        }

                        imgsliderVoucherImageSlider.Images.Remove(imgsliderVoucherImageSlider.CurrentImage);

                    }

                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                foreach (DataRow dr in dtVoucherImageFiles.Rows)
                {
                    string filename = dr[vouchersystem.AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName].ToString();
                    string fileactualname = dr[vouchersystem.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName].ToString();
                    string refpath = dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILE_REF_PATHColumn.ColumnName].ToString();
                    Byte[] BytePdffile = null;
                    if (File.Exists(refpath) && dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] != DBNull.Value)
                    {
                        BytePdffile = (Byte[])dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName];
                    }

                    ResultArgs result = vouchersystem.AttachVoucherFiles(VoucherId, fileactualname, fileactualname, BytePdffile, "", refpath);
                }
                
                //For All Pdfs
                if (gcPdfVouchers.DataSource != null)
                {
                    DataTable dtPdfFiles = dtVoucherPdfFiles as DataTable;
                    foreach (DataRow dr in dtPdfFiles.Rows)
                    {
                        string filename = dr[vouchersystem.AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName].ToString();
                        string fileactualname = dr[vouchersystem.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName].ToString();
                        string refpath = dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILE_REF_PATHColumn.ColumnName].ToString();
                        Byte[] BytePdffile = null;
                        if (File.Exists(refpath) && dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] != DBNull.Value)
                        {
                            BytePdffile = (Byte[])dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName];
                        }

                        ResultArgs result = vouchersystem.AttachVoucherFiles(VoucherId, fileactualname, fileactualname, BytePdffile, "", refpath);
                    }
                }
                
                DataTable dtVoucherImages = vouchersystem.dtVoucherFiles;
                this.ReturnValue = dtVoucherImages;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnReloadImages_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.ShowConfirmationMessage("Are you sure to Load Voucher Files from Backup ? ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.OK)
                {
                    this.ShowWaitDialog("Downloading Voucher Files form Backup");
                    string VoucherUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_VoucherFiles);
                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                    {
                        foreach (DataRow dr in dtAttachedBaseVoucherFiles.Rows)
                        {
                            string filename = dr[vouchersystem.AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName].ToString();
                            string fileactualname = dr[vouchersystem.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName].ToString();
                            string filepath = Path.Combine(VoucherUploadPath, filename);
                            string remark = dr[vouchersystem.AppSchema.VoucherMaster.REMARKColumn.ColumnName].ToString();
                            byte[] voucherimgArray = null;
                            using (AcMEERPFTP ftp = new AcMEERPFTP())
                            {
                                ftp.DownloadDataByWebClient(filename, filepath);
                            }

                            if (File.Exists(filepath))
                            {
                                if (Path.GetExtension(filename) == ".pdf")
                                {
                                    voucherimgArray = File.ReadAllBytes(filepath);
                                    //AttachVoucherLocalFiles(dtVoucherPdfFiles, filename, fileactualname, voucherimgArray, remark, filepath);
                                }
                                else
                                {
                                    voucherimgArray = ImageProcessing.ImageToByteArray(filepath);
                                    Image img = ImageProcessing.ByteArrayToImage(voucherimgArray);
                                    img.Tag = fileactualname;

                                    //Image img = (Image)dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_IMAGEColumn.ColumnName];
                                    //byte[] voucherimgArray = (byte[])dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_IMAGEColumn.ColumnName];
                                    //Image img = ImageProcessing.ByteArrayToImage(voucherimgArray);
                                    //img.Tag = fileactualnmae;

                                    imgsliderVoucherImageSlider.Images.Add(img);

                                    //AttachVoucherLocalFiles(dtVoucherImageFiles, filename, fileactualname, voucherimgArray, remark, filepath);
                                }
                            }

                            dr.BeginEdit();
                            dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] = voucherimgArray;
                            dr.EndEdit();
                        }
                    }
                    LoadVoucherFiles();
                    ShowVoucherFileDetails();
                    this.CloseWaitDialog();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }

        private void imgsliderVoucherImageSlider_CurrentImageIndexChanged(object sender, ImageSliderCurrentImageIndexChangedEventArgs e)
        {
            ShowVoucherFileDetails();

            var imageSlider = sender as DevExpress.XtraEditors.Controls.ImageSlider;

        }

        private void rbtnPdfVoucherView_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ShowPdfViewer();
        }

        private void rbtnDeletePdfVoucher_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (!string.IsNullOrEmpty(VoucherPdfActualFileName) && gvPdfVouchers.FocusedRowHandle >= 0)
            {
                if (this.ShowConfirmationMessage("Are you sure to remove Voucher current Pdf file from the list ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    gvPdfVouchers.DeleteRow(gvPdfVouchers.FocusedRowHandle);
                }

            }
        }

        private void PicBack_Click(object sender, EventArgs e)
        {
            ShowVoucherFileDetails();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pdfViewer_PopupMenuShowing(object sender, DevExpress.XtraPdfViewer.PdfPopupMenuShowingEventArgs e)
        {

            foreach (BarButtonItemLink barlnk in e.Menu.ItemLinks)
            {
                if (barlnk.Caption.ToUpper() == "DOCUMENT PROPERTIES")
                {
                    barlnk.Visible = false;
                    break;
                }

            }

        }

        private void gcPdfVouchers_DoubleClick(object sender, EventArgs e)
        {
            ShowPdfViewer();
        }

        #endregion

        #region Methods

        private void SetDefault()
        {
            lblNoImageFiles.Text = string.Empty;
            lblNoPdfFiles.Text = string.Empty;
            lblNoImageFiles.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            lcImageActualFileName.Visibility = lcRemoveCurrentImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            imgsliderVoucherImageSlider.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.MiddleCenter;
            lcPdfViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcImageFileNotAvailable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            lcReloadFiles.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (IsVoucherFilesExists()) //&& !IsViewVoucherFiles
            {
                lcReloadFiles.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        
        private void LoadVoucherFiles()
        {
            dtVoucherPdfFiles.Rows.Clear();
            dtVoucherImageFiles.Rows.Clear();
            imgsliderVoucherImageSlider.Images.Clear();
            bool localfilesnotfound = false;
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                foreach (DataRow dr in dtAttachedBaseVoucherFiles.Rows)
                {
                    string filename = dr[vouchersystem.AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName].ToString();
                    string fileactualnmae = dr[vouchersystem.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName].ToString();
                    string remark = dr[vouchersystem.AppSchema.VoucherMaster.REMARKColumn.ColumnName].ToString();
                    //Image img = (Image)dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_IMAGEColumn.ColumnName];

                    byte[] voucherimgArray = null;
                    if (dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] == DBNull.Value)
                    {
                        localfilesnotfound = true;
                    }

                    string LocalRefPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_VoucherFiles);
                    LocalRefPath = Path.Combine(LocalRefPath, filename);
                                        
                    if (Path.GetExtension(fileactualnmae) == ".pdf")
                    {
                        if (dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] != DBNull.Value)
                        {
                            voucherimgArray = (byte[])dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName];
                        }
                        AttachVoucherLocalFiles(dtVoucherPdfFiles, filename, fileactualnmae, voucherimgArray, remark, LocalRefPath);
                    }
                    else
                    {
                        Image img = ACPP.Properties.Resources.info;
                        voucherimgArray = ImageProcessing.ImageToByteArray(ACPP.Properties.Resources.info);
                        if (dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] != DBNull.Value)
                        {
                            voucherimgArray = (byte[])dr[vouchersystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName];
                            img = ImageProcessing.ByteArrayToImage(voucherimgArray);
                        }
                        else
                        {
                            LocalRefPath = string.Empty;
                        }
                        
                        img.Tag = fileactualnmae;
                        imgsliderVoucherImageSlider.Images.Add(img);
                        AttachVoucherLocalFiles(dtVoucherImageFiles, filename, fileactualnmae, voucherimgArray, remark, LocalRefPath);
                    }
                }
            }

            gcPdfVouchers.DataSource = dtVoucherPdfFiles;

            if (localfilesnotfound)
            {
                this.ShowMessageBox("Few Attached Voucher file(s) are not available in local Acme.erp path, click \"Reload Files\" to get from Backup");
            }
        }

        private void ShowVoucherFileDetails()
        {
            try
            {
                lblImageActualFileName.Text = " ";
                lcImageActualFileName.Visibility = lcRemoveCurrentImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcgIamgeView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcPdfViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                colDeleteVoucherPdf.Visible = true;
                if (imgsliderVoucherImageSlider.CurrentImage != null && imgsliderVoucherImageSlider.GetCurrentImageIndex() >= 0)
                {
                    Int32 imageindex = imgsliderVoucherImageSlider.GetCurrentImageIndex();

                    lblImageActualFileName.Text = (imgsliderVoucherImageSlider.Images[imageindex].Tag != null ?
                               imgsliderVoucherImageSlider.Images[imageindex].Tag.ToString() : string.Empty);

                    lcImageActualFileName.Visibility = lcRemoveCurrentImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    lcImageFileNotAvailable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    if (dtVoucherImageFiles.Rows.Count > 0)
                    {
                        using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
                        {
                            string path = dtVoucherImageFiles.Rows[imageindex][vsystem.AppSchema.VoucherMaster.VOUCHER_FILE_REF_PATHColumn.ColumnName].ToString();
                            if (!File.Exists(path))
                            {
                                lcImageFileNotAvailable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            }
                        }
                    }

                }

                lblNoImageFiles.Text = imgsliderVoucherImageSlider.Images.Count.ToString();
                lblNoPdfFiles.Text = dtVoucherPdfFiles.Rows.Count.ToString();

                if (IsViewVoucherFiles)
                {
                    lcBack.Visibility = lcTitleSpace.Visibility = lcTitle.Visibility = lcAddImage.Visibility =
                        lcRemoveCurrentImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    colDeleteVoucherPdf.Visible = false;

                }
            }
            catch (System.IndexOutOfRangeException Indexerr) 
            {
                ////For Temp, to handle unhandled expection for tmp prupose, have to check and remove for moving to last image in image slider
                string msg = Indexerr.Message;

            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }

        private void ShowPdfViewer()
        {
            if (!string.IsNullOrEmpty(VoucherPdfFileName) && !string.IsNullOrEmpty(VoucherFileRefPath)  && gvPdfVouchers.FocusedRowHandle >= 0)
            {

                if (File.Exists(VoucherFileRefPath))
                {
                    lcPdfViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcgIamgeView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    colDeleteVoucherPdf.Visible = false;
                    pdfViewer.LoadDocument(VoucherFileRefPath);
                }
                else
                {
                    MessageRender.ShowMessage("File is not available in Local");
                }
            }
        }

        private ResultArgs AttachVoucherLocalFiles(DataTable dtLocalFiles, string FileName, string ActualFileName, byte[] newAttachVoucherFile, string Remark, string RefPath)
        {
            ResultArgs resultArgs = new ResultArgs();

            try
            {
                using (VoucherTransactionSystem vtranssystem = new VoucherTransactionSystem())
                {
                    if (dtLocalFiles.Columns.Count == 0)
                    {
                        dtLocalFiles.Columns.Add(vtranssystem.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName, typeof(Int32));
                        dtLocalFiles.Columns.Add(vtranssystem.AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName, typeof(string));
                        dtLocalFiles.Columns.Add(vtranssystem.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName, typeof(string));
                        dtLocalFiles.Columns.Add(vtranssystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName, typeof(byte[]));
                        dtLocalFiles.Columns.Add(vtranssystem.AppSchema.VoucherMaster.REMARKColumn.ColumnName, typeof(string));
                        dtLocalFiles.Columns.Add(vtranssystem.AppSchema.VoucherMaster.VOUCHER_FILE_REF_PATHColumn.ColumnName, typeof(string));
                    }

                    DataRow drVoucherFileRow = dtLocalFiles.NewRow();
                    drVoucherFileRow[vtranssystem.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName] = VoucherId;
                    drVoucherFileRow[vtranssystem.AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName] = FileName;
                    drVoucherFileRow[vtranssystem.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName] = ActualFileName;
                    drVoucherFileRow[vtranssystem.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] = newAttachVoucherFile;
                    drVoucherFileRow[vtranssystem.AppSchema.VoucherMaster.REMARKColumn.ColumnName] = Remark;
                    drVoucherFileRow[vtranssystem.AppSchema.VoucherMaster.VOUCHER_FILE_REF_PATHColumn.ColumnName] = RefPath;

                    dtLocalFiles.Rows.Add(drVoucherFileRow);
                    resultArgs.DataSource.Data = dtLocalFiles;

                    resultArgs.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        private bool IsVoucherFilesExists()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (VoucherTransactionSystem vtranssystem = new VoucherTransactionSystem())
                {
                    resultarg = vtranssystem.IsExistsVoucherFiles(VoucherId);
                    if (resultarg.Success)
                    {
                        resultarg.Success = resultarg.RowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.ToString();
            }
            
            return resultarg.Success;
        }

        private Bitmap CompressImage(Bitmap image)
        {
            long ImageSize;
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

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
