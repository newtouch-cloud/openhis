using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Utilities;
using GrapeCity.ActiveReports.PageReportModel;

using JSViewer_MVC_Core.Bll;
using JSViewer_MVC_Core.Bll.OpenSource;
using JSViewer_MVC_Core.Implementation.CustomStore.Images;
using JSViewer_MVC_Core.Implementation.CustomStore.Reports;
using JSViewer_MVC_Core.Implementation.CustomStore.Themes;
using JSViewer_MVC_Core.Models;
using JSViewer_MVC_Core.Services;

using LiteDB;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSViewer_MVC_Core.Implementation.Storage
{
	public class LiteDB : ICustomStorage
	{
		private const string IMAGES = "images";
		private const string THEMES = "themes";
		private const string REPORTS = "reports";

		private LiteDatabase Lite;

		public SysReportTemplateBll TemplateBll { get; set; }
        public SysReportTemplatesBll  KyTemplateBll { get; set; }


        public LiteDB(string databasePath, SysReportTemplateBll templateBll, SysReportTemplatesBll kytemplateBll)
		{
			Lite = new LiteDatabase(databasePath);
			TemplateBll = templateBll;
            KyTemplateBll = kytemplateBll;
        }

        public void Dispose()
		{
			Lite.Dispose();
		}

		public byte[] GetImage(string imageId)
		{
			return Lite.GetCollection<ImageInfo>(IMAGES)
					  .FindById(imageId)
					  .Content;
		}

		public IEnumerable<IImageInfo> GetImagesList()
		{
			var imagesList = Lite
				.GetCollection<ImageInfo>(IMAGES)
				.FindAll()
				.Select(img =>
				{
					img.Name = Uri.UnescapeDataString(img.Id);
					img.Thumbnail = Util.GetImageThumbnail(img.Content);

					return img;
				});

			return imagesList;
		}

		public Report GetReport(string reportId)
		{
			var report=new Report();

            var bs = "ky";
            if (bs == "ky")
			{
                report = KyTemplateBll.GetReport(reportId);
            }
			else {
                report = TemplateBll.GetReport(reportId);
            }
			if (report == null)
			{
                var (collection, reportName) = Util.GetCollectionAndName(reportId, REPORTS);
                var reportItem = Lite.GetCollection<ReportInfo>(collection)
                                      .FindById(reportName);

                if (reportItem is null)
                    return null;

                report = ReportConverter.FromXML(reportItem.Content);
            }

			// Define our own resource locator for correct work with report resources (images, themes and so on)
			report.Site = new ReportSite(new CustomStoreResourceLocator(this));
			return report;
		}

		public void SaveReport(string reportId, Report report, bool isTemporary = false)
		{

			var reportXml = ReportConverter.ToXml(report);
			var collection = isTemporary ? Util.TEMP_SUFFIX : REPORTS;

			Lite
				.GetCollection<ReportInfo>(collection)
				.Upsert(new ReportInfo
				{
					Id = reportId,
					Name = reportId,
					Content = reportXml,
					Type = Util.GetReportType(report),
				});

			System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
			String content = utf8.GetString(reportXml);
            var bs = "ky";
			if (bs == "ky")
			{
                KyTemplateBll.SaveReport(reportId, content);
            }
			else {
                TemplateBll.SaveReport(reportId, content);
            }
			
		}

		public void DeleteReport(string id)
		{
			var (collection, reportName) = Util.GetCollectionAndName(id);

			Lite.GetCollection<ReportInfo>(collection)
				 .Delete(reportName);
		}

		public IEnumerable<IReportInfo> GetReportsList()
		{
			return Lite.GetCollection<ReportInfo>(REPORTS)
						.FindAll();
		}

		public Theme GetTheme(string themeId)
		{
			var bson = Lite.GetCollection(THEMES)
							.FindById(themeId);

			if (bson is null)
				return null;

			return Lite.Mapper.Deserialize<Theme>(bson);
		}

		public IEnumerable<IThemeInfo> GetThemesList()
		{
			var themes = Lite.GetCollection(THEMES);
			var themesList = themes
				.FindAll()
				.Select(bson =>
				{
					var theme = Lite.Mapper.Deserialize<Theme>(bson);
					var themeId = bson["_id"];

					return new ThemeInfo(theme)
					{
						Id = themeId,
						Title = Path.GetFileNameWithoutExtension(themeId),
					};
				});

			return themesList;
		}

		public Resource GetResource(string id)
		{
			var bson = Lite.GetCollectionNames()
						.Select(c => Lite.GetCollection(c).FindById(id))
						.FirstOrDefault(r => r != null);

			if (bson == null)
				return new Resource();

			var type = CustomStoreResourceLocator.GetResourceType(id);
			var result = Lite.Mapper.Deserialize(type, bson);

			// Special case for themes due to different storage format in the database
			var theme = result as Theme;
			if (theme != null)
				return new Resource(theme.ToStream(), null);

			return new Resource(CustomStoreResourceLocator.ToStream(result), null);
		}
	}
}
