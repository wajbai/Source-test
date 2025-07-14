//This class is used to convert RichEdit rtf content to HTML content 
// and get as Mail Message View collections for each merged record or sections in the RichEditControl 
// It inherits IUriProvider interface and implements CreateCssUri,  CreateImageUri

//1. All content would be converted as HTML Tags.
//2. All the styles like fonts, color, alingment etc are converted as inline CSS with in html content.
//3. All images in the rft content would be converted as steam and attached with in the Mail Message view as LinkedResource.

//so when we send mail, Rtf content with its styles and images will be sent as it is in the RichEditcontrol

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Office.Services;
using DevExpress.XtraRichEdit;
using DevExpress.Utils;
using System.Net.Mail;
using DevExpress.XtraRichEdit.Export;
using DevExpress.Office.Utils;
using System.Net.Mime;
using System.IO;
using DevExpress.XtraRichEdit.API.Native;

using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model
{
    public class RichEditMailMessageHTML : SystemBase, IUriProvider
    {
        readonly RichEditControl control;
        List<AttachementInfo> attachments;
        int imageId;

        public RichEditMailMessageHTML(RichEditControl control)
        {
            this.control = control;
        }

        /// <summary>
        /// This method is used to split sections or merged every records and gets its rtf content
        /// Convert to HTML content with its style and images
        /// Add into to Mail Message View Collections
        /// </summary>
        /// <returns></returns>
        public virtual AlternateViewCollection ConvertRTFtoHTMLVIEW()
        {
            AlternateViewCollection MessageHTMLViews = new MailMessage().AlternateViews;
            control.BeforeExport += OnBeforeExport;

            foreach (DevExpress.XtraRichEdit.API.Native.Section section in control.Document.Sections)
            {
                this.attachments = new List<AttachementInfo>();
                int start = section.Paragraphs[0].Range.Start.ToInt();
                int end = section.Paragraphs[section.Paragraphs.Count - 1].Range.End.ToInt() - start - 1;
                DocumentRange sectionRange = control.Document.CreateRange(start, end);
                string bodyhtml = control.Document.GetHtmlText(sectionRange, this);
                AlternateView view = AlternateView.CreateAlternateViewFromString(bodyhtml, Encoding.UTF8, MediaTypeNames.Text.Html);

                int count = attachments.Count;
                for (int i = 0; i < count; i++)
                {
                    AttachementInfo info = attachments[i];
                    LinkedResource resource = new LinkedResource(info.Stream, info.MimeType);
                    resource.ContentId = info.ContentId;
                    view.LinkedResources.Add(resource);
                }

                MessageHTMLViews.Add(view);
            }

            control.BeforeExport -= OnBeforeExport;
            return MessageHTMLViews;
        }

        /// <summary>
        /// Before Converts from rtf as HTML, set HtmlDocument Exporter Options and its html conding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBeforeExport(object sender, BeforeExportEventArgs e)
        {
            HtmlDocumentExporterOptions options = e.Options as HtmlDocumentExporterOptions;
            options.CssPropertiesExportType = DevExpress.XtraRichEdit.Export.Html.CssPropertiesExportType.Inline; 
            if (options != null)
            {
                options.Encoding = Encoding.UTF8;
            }
        }

        #region IUriProvider Members

        /// <summary>
        /// We use default CSS handling (as inline), so the CreateCssUri method always returns null.
        /// </summary>
        /// <param name="rootUri"></param>
        /// <param name="styleText"></param>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        public string CreateCssUri(string rootUri, string styleText, string relativeUri)
        {
            return String.Empty;
        }

        /// <summary>
        /// used to transform each document image into an object of the helper class - the AttachmentInfo class instance. 
        /// An instance of this class contains an image's name, type and the data stream. An image is identified by its name, 
        /// so this method returns a CID (Content-ID) URL containing the image name, to include a link to the image in the message body.
        /// </summary>
        /// <param name="rootUri"></param>
        /// <param name="image"></param>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
        {
            string imageName = String.Format("image{0}", imageId);
            imageId++;

            OfficeImageFormat imageFormat = GetActualImageFormat(image.RawFormat);
            Stream stream = new MemoryStream(image.GetImageBytes(imageFormat));
            string mediaContentType = OfficeImage.GetContentType(imageFormat);
            AttachementInfo info = new AttachementInfo(stream, mediaContentType, imageName);
            attachments.Add(info);

            return "cid:" + imageName;
        }

        /// <summary>
        /// This method is used to get action image
        /// </summary>
        /// <param name="_officeImageFormat"></param>
        /// <returns></returns>
        private OfficeImageFormat GetActualImageFormat(OfficeImageFormat _officeImageFormat)
        {
            if (_officeImageFormat == OfficeImageFormat.Exif ||
                _officeImageFormat == OfficeImageFormat.MemoryBmp)
                return OfficeImageFormat.Png;
            else
                return _officeImageFormat;
        }
    }
        #endregion


    /// <summary>
    /// To hold content images as attachments like Stream, MimeType and image image id
    /// </summary>
    public class AttachementInfo
    {
        Stream stream;
        string mimeType;
        string contentId;

        public AttachementInfo(Stream stream, string mimeType, string contentId)
        {
            this.stream = stream;
            this.mimeType = mimeType;
            this.contentId = contentId;
        }

        public Stream Stream { get { return stream; } }
        public string MimeType { get { return mimeType; } }
        public string ContentId { get { return contentId; } }
    }
}
