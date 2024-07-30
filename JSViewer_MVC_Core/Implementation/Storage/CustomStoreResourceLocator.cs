using System;
using System.IO;
using System.Net;

using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;

using JSViewer_MVC_Core.Implementation.CustomStore;
//using JSViewer_MVC_Core.Implementation.CustomStore.DataSets;
using JSViewer_MVC_Core.Implementation.CustomStore.Images;
using JSViewer_MVC_Core.Implementation.CustomStore.Reports;

namespace JSViewer_MVC_Core.Implementation.Storage
{
	public class CustomStoreResourceLocator : ResourceLocator
	{
		private ICustomStorage _db;

		public CustomStoreResourceLocator(ICustomStorage database)
		{
			_db = database;
		}

		public override Resource GetResource(ResourceInfo resourceInfo)
		{
			// Check if there is external resource request
			if (IsURL(resourceInfo.Name))
			{
				var request = WebRequest.Create(resourceInfo.Name);
				var response = request.GetResponse();

				return new Resource(response.GetResponseStream(), null);
			}

			// Search non-null resource in all existing database collections
			var resource = _db.GetResource(resourceInfo.Name);
			return resource;
		}

		/// <summary>
		/// Creates new <see cref="Stream" /> from supported resource object.
		/// </summary>
		/// <param name="obj">Resource object</param>
		/// <returns><see cref="Stream"/> containing input resource object.</returns>
		/// <remarks>
		/// Supported resource types: 
		/// <list type="bullet">
		///		<item><see cref="DataSets.Dataset"/></item>
		///		<item><see cref="Images.ImageInfo"/></item>
		///		<item><see cref="Reports.ReportInfo"/></item>
		///		<item><see cref="Templates.Template"/></item>
		/// </list>
		/// </remarks>
		public static Stream ToStream(object obj)
		{
			var val = obj.GetType().GetProperty("Content")?.GetValue(obj);
			var bytes = val as byte[];

			if (bytes is null)
				return null;

			return new MemoryStream(bytes);
		}

		private static bool IsURL(string uri)
		{
			return Uri.TryCreate(uri, UriKind.Absolute, out var result)
				&& (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
		}

		public static Type GetResourceType(string id)
		{
			var ext = Path.GetExtension(id);

			if (ext.EndsWith(".rdlx-theme"))
				return typeof(Theme);

			if (CustomStoreService.MimeTypeByExtension.ContainsKey(ext))
				return typeof(ImageInfo);

			if (ext == ".rdl" || ext == ".rdlx" || ext == ".rpx")
				return typeof(ReportInfo);

			return null;//typeof(Dataset);
		}
	}
}
