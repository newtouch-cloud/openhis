using System;
using System.Collections.Generic;

using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;

using JSViewer_MVC_Core.Services;


namespace JSViewer_MVC_Core.Implementation.Storage
{
	public interface ICustomStorage : IDisposable
	{
		Theme GetTheme(string themeId);
		IEnumerable<IThemeInfo> GetThemesList();

		byte[] GetImage(string imageId);
		IEnumerable<IImageInfo> GetImagesList();

		Report GetReport(string reportId);
		void SaveReport(string id, Report report, bool isTemporary = false);
		void DeleteReport(string id);
		IEnumerable<IReportInfo> GetReportsList();

		Resource GetResource(string resourceId);
	}
}
