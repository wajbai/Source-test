/************************************************************************************************************************
 *                                              Form Name  :ImageProcessing.cs
 *                                              Purpose    :Cropping of image and Converting Image to byte array
 *                                                          and converting byte array to Image
 *                                              Author     : Carmel Raj M
 *                                              Created On :23-Oct-2013
 * 
 * **********************************************************************************************************************/
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;

namespace Bosco.Utility
{
    public class ImageProcessing
    {
        #region Variables
        Bitmap CroppedImage;
        private Bitmap _currentBitmap;
        Bitmap prImage;
        #endregion

        #region Properties
        public Bitmap CurrentBitmap
        {
            get
            {
                if (_currentBitmap == null)
                    _currentBitmap = new Bitmap(1, 1);
                return _currentBitmap;
            }
            set { _currentBitmap = value; }
        }
        public static Bitmap FinalCroppedImage { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Cropping the image
        /// </summary>
        /// <param name="image">DrawnoutCropArea image</param>
        /// <param name="xPosition">XAxis of Mouse</param>
        /// <param name="yPosition">YAxis of Mouse </param>
        /// <param name="width">Width of the Image to be cropped</param>
        /// <param name="height">Height of the Image to be cropped</param>
        /// <returns>Cropped Image</returns>
        public Image Crop(Bitmap image, int xPosition, int yPosition, int width, int height)
        {
            Bitmap temp = (Bitmap)CroppedImage;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (xPosition + width > image.Width)
                width = CroppedImage.Width - xPosition;
            if (yPosition + height > image.Height)
                height = CroppedImage.Height - yPosition;
            Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
            return (Bitmap)bmap.Clone(rect, bmap.PixelFormat);
        }

        /// <summary>
        /// Drawing Cropping area 
        /// </summary>
        /// <param name="image">Original image as Bitmap</param>
        /// <param name="xPosition">XAxis of Mouse</param>
        /// <param name="yPosition">YAxis of Mouse </param>
        /// <param name="width">Width of the Image to be drawn</param>
        /// <param name="height">Height of the Image to be drawn</param>
        /// <returns>Image with Drawnout crop Area</returns>
        public Image DrawOutCropArea(Bitmap image, int xPosition, int yPosition, int width, int height)
        {
            prImage = image;
            Bitmap bmap = (Bitmap)image.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            Brush cBrush = new Pen(Color.FromArgb(150, Color.White)).Brush;
            Rectangle rect1 = new Rectangle(0, 0, bmap.Width, yPosition);
            Rectangle rect2 = new Rectangle(0, yPosition, xPosition, height);
            Rectangle rect3 = new Rectangle(0, (yPosition + height), bmap.Width, bmap.Height);
            Rectangle rect4 = new Rectangle((xPosition + width), yPosition, (bmap.Width - xPosition - width), height);
            gr.FillRectangle(cBrush, rect1);
            gr.FillRectangle(cBrush, rect2);
            gr.FillRectangle(cBrush, rect3);
            gr.FillRectangle(cBrush, rect4);
            CroppedImage = (Bitmap)bmap.Clone();
            return bmap;
        }

        /// <summary>
        /// To convert Bitmap image to byarray
        /// </summary>
        /// <param name="imageIn">Bitmap Image</param>
        /// <returns>Byte array of the image</returns>
        public static byte[] ImageToByteArray(Bitmap imageIn)
        {
            byte[] data = null;
            bool EmptyBufferFound = false;
            try
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, ImageFormat.Jpeg);
                data = ms.GetBuffer();
                //Modified by Carmel Raj M on July-07-2015
                //Purpose: Removing trailing null bytes 
                data = ms.GetBuffer().Reverse().SkipWhile(record =>
                    {
                        if (EmptyBufferFound) return false;
                        if (record == 0x00) return true; else EmptyBufferFound = true; return false;
                    }).Reverse().ToArray();

                ms.Close();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return data;
        }

        /// <summary>
        /// To convert byte array to Image
        /// </summary>
        /// <param name="byteArrayIn">Byte array</param>
        /// <returns>Bitmap image</returns>
        public static Bitmap ByteArrayToImage(byte[] byteArrayIn)
        {
            Image ReturnImage = null;
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                ReturnImage = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                //  MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return (Bitmap)ReturnImage;
        }

        /// <summary>
        /// Convert Byte array from Image Path
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(string ImageFilePath)
        {
            byte[] data = null;
            bool EmptyBufferFound = false;
            try
            {
                if (File.Exists(ImageFilePath))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Image imageIn = Image.FromFile(ImageFilePath);
                        imageIn.Save(ms, ImageFormat.Jpeg);
                        data = ms.GetBuffer();
                        //Modified by Carmel Raj M on July-07-2015
                        //Purpose: Removing trailing null bytes 
                        data = ms.GetBuffer().Reverse().SkipWhile(record =>
                        {
                            if (EmptyBufferFound) return false;
                            if (record == 0x00) return true; else EmptyBufferFound = true; return false;
                        }).Reverse().ToArray();
                        imageIn.Dispose();
                        ms.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return data;
        }

        /// <summary>
        /// Resizing the image according to the Picture Control
        /// </summary>
        /// <param name="Photo">Image</param>
        /// <param name="Width">Width of the Picture Edit control</param>
        /// <param name="Height">Height of the Picture Edit control</param>
        /// <returns> resized image</returns>
        public static Image ResizeImage(Image Photo, ref int Width, ref int Height)
        {
            Bitmap bm_source = new Bitmap(Photo);
            // Make a bitmap for the result.
            Bitmap bm_dest = new Bitmap(Convert.ToInt32(Width), Convert.ToInt32(Height));
            // Make a Graphics object for the result Bitmap.
            Graphics gr_dest = Graphics.FromImage(bm_dest);
            // Copy the source image into the destination bitmap.
            gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1);
            // Display the result.
            Width = bm_dest.Width;
            Height = bm_dest.Height;
            return bm_dest;
        }
        #endregion

    }
}
