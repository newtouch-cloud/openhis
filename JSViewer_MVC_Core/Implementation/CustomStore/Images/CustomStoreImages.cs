using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;

using JSViewer_MVC_Core.Services;


namespace JSViewer_MVC_Core.Implementation.CustomStore
{
    public partial class CustomStoreService : ICustomStoreService
    {
        internal static readonly IDictionary<string, string> MimeTypeByExtension = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            {".bmp", "image/bmp"},
            {".dib", "image/bmp"},
            {".emf", "image/x-emf"},
            {".gif", "image/gif"},
            {".ico", "image/x-icon"},
            {".jfif", "image/pjpeg"},
            {".jpe", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".pbm", "image/x-portable-bitmap"},
            {".pct", "image/pict"},
            {".pic", "image/pict"},
            {".pict", "image/pict"},
            {".png", "image/png"},
            {".pnz", "image/png"},
            {".svg", "image/svg+xml" },
            {".tif", "image/tiff"},
            {".tiff", "image/tiff"},
            {".wmf", "image/x-wmf"},
            {".xbm", "image/x-xbitmap"},
        };

        #region IResourcesService implementation

        public IImageInfo[] GetImagesList()
        {
            return _db.GetImagesList()
                      .ToArray();
        }

        public byte[] GetImage(string id, out string mimeType)
        {
            var imageId = Uri.UnescapeDataString(id);
            var image = _db.GetImage(id);

            if (image is null)
                throw new ImageNotFoundException();

            var extension = Path.GetExtension(imageId);
            var isValidExtension = MimeTypeByExtension.TryGetValue(extension, out mimeType);

            // Use 'application/octet-stream' type when the image extension is unknown
            mimeType = isValidExtension ? mimeType : "application/octet-stream";

            return image;
        }

        #endregion
    }
}