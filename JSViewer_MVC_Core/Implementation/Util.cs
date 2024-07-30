using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Xml.Linq;
using System.Xml.XPath;

using GrapeCity.Documents.Imaging;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Utilities;

namespace JSViewer_MVC_Core.Implementation
{
	public static class Util
	{
		public const string TEMP_SUFFIX = "tmp";

		/// <summary>
		/// Resizes the input image to 128x128 by default.
		/// Used to display a thumbnail in the image list. 
		/// </summary>
		/// <param name="image">Image represented as an array of bytes.</param>
		/// <param name="size">Size to resize. 128x128 by default.</param>
		/// <returns>The content of the thumbnail, represented as bytes.</returns>
		public static byte[] GetImageThumbnail(byte[] image, Size? thumbnailSize = null)
		{
			using var stream = new MemoryStream(image);
			using var original = new GcBitmap(stream);

			var size = thumbnailSize ?? new Size(128, 128);
			var thumbnail = original.Resize(size.Width, size.Height);
			using var thumbnailStream = new MemoryStream();

			thumbnail.SaveAsPng(thumbnailStream);
			return thumbnailStream.ToArray();
		}

		/// <summary>
		/// Gets the name of the collection that contains the report and report name.
		/// </summary>
		/// <param name="id">Report ID</param>
		/// <returns><see cref="Tuple"/> with collection and report names.</returns>
		public static (string collection, string reportName) GetCollectionAndName(string id, string defaultCollection = "reports")
		{
			var reportId = Uri.UnescapeDataString(id);
			var isTemporary = reportId.StartsWith(TEMP_SUFFIX);
			var collection = isTemporary ? TEMP_SUFFIX : defaultCollection;
			var reportName = isTemporary ? reportId.Substring(TEMP_SUFFIX.Length + 1) : reportId;

			return (collection, reportName);
		}

		/// <summary>
		/// Extract report type from report.
		/// </summary>
		/// <param name="report">Report</param>
		/// <returns>Report type string (FPL/CPL).</returns>
		public static string GetReportType(Report report)
		{
			var reportXml = ReportConverter.ToXml(report);
			using var stream = new MemoryStream(reportXml);
			var root = XElement.Load(stream);

			var path = "Body/ReportItems/FixedPage";
			var xPath = string.Join("/", path.Split('/').Select(e => $"*[local-name() = '{e}']"));
			var fixedPageNode = root.XPathSelectElement(xPath);

			return fixedPageNode != null ? "FPL" : "CPL";
		}

		public static string GenerateTempReportName()
		{
			return $"{Guid.NewGuid():D}.rdlx";
		}
	}
}
