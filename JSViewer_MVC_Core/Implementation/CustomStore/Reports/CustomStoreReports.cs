using System;
using System.Linq;

using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;
using JSViewer_MVC_Core.Services;
using JSViewer_MVC_Core.Models;

namespace JSViewer_MVC_Core.Implementation.CustomStore
{
    public partial class CustomStoreService : ICustomStoreService
    {
        #region IResourcesService implementation

        public IReportInfo[] GetReportsList()
        {
            return _db.GetReportsList()
                      .ToArray();
        }

        public Report GetReport(string id)
        {
            return _db.GetReport(id);
        }


        public string SaveReport(string id, Report report, bool isTemporary = false)
        {
            var reportId = Uri.UnescapeDataString(id);
            report.Name = reportId;

            var reportName = isTemporary ? Util.GenerateTempReportName() : reportId;
            reportId = string.Format("{0}{1}", isTemporary ? Util.TEMP_SUFFIX + "." : string.Empty, reportName);

            _db.SaveReport(reportName, report, isTemporary);
            return reportId;
        }

        public string UpdateReport(string id, Report report)
        {
            return SaveReport(id, report);
        }

        public void DeleteReport(string id)
        {
            _db.DeleteReport(id);
        }

        #endregion
    }
}
