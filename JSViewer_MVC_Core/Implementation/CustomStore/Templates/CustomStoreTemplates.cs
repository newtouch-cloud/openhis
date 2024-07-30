using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

using GrapeCity.ActiveReports.Aspnetcore.Designer.Utilities;

using JSViewer_MVC_Core.Services;
using JSViewer_MVC_Core.Implementation.CustomStore.Templates;


namespace JSViewer_MVC_Core.Implementation.CustomStore
{
    public partial class CustomStoreService : ICustomStoreService
    {
        private const string THUMBNAIL_NAME = "template_thumbnail";

        #region IResourcesService implementation

        public byte[] GetTemplate(string id)
        {
            var report = _db.GetTemplate(id);

            if (report is null)
                throw new FileNotFoundException();

            // Cut out thumbnail from embedded images, if it exist
            var thumbnail = report.EmbeddedImages.FirstOrDefault(image => image.Name == THUMBNAIL_NAME);
            if (thumbnail != null)
                report.EmbeddedImages.Remove(thumbnail);

            return ReportConverter.ToJson(report);
        }

        public TemplateInfo[] GetTemplatesList()
        {
            var templatesList = _db.GetTemplatesList()
                                   .ToArray();

            return templatesList;
        }

        public TemplateThumbnail GetTemplateThumbnail(string id)
        {
            var template = _db.GetTemplate(id);

            if (template is null)
                throw new FileNotFoundException();

            return ExtractThumbnail(ReportConverter.ToXml(template));
        }

        #endregion

        /// <summary>
        /// Extract a thumbnail from embedded images.
        /// </summary>
        /// <param name="template"><see cref="Template"/> content represented by byte array.</param>
        /// <returns><see cref="TemplateThumbnail" /> containing the extracted thumbnail.</returns>
        public static TemplateThumbnail ExtractThumbnail(byte[] template)
        {
            using var stream = new MemoryStream(template);
            var xTemplate = XElement.Load(stream);

            var thmumbnailElement = xTemplate.XPathSelectElement($"*[local-name() = 'EmbeddedImages']/*[local-name() = 'EmbeddedImage' and @Name='{ THUMBNAIL_NAME }']");
            if (thmumbnailElement == null)
                throw new FileNotFoundException();

            return new TemplateThumbnail
            {
                Data = thmumbnailElement.XPathSelectElement("*[local-name() = 'ImageData']")?.Value ?? string.Empty,
                MIMEType = thmumbnailElement.XPathSelectElement("*[local-name() = 'MIMEType']")?.Value ?? string.Empty,
            };
        }
    }
}