using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysReportDmnService
    {
        IList<ReortListVO> GetReportTree();
        IList<ReortListVO> GetReportConcreteTree(int reportID);
        ReortMXListVO GetReportMXTree(string ReportId);
        string SubmitForm(ReortMXListVO reortMXListVO,string Type);
        string ReportStop(string ReportId,string ReportStatus);
        string ReportDel(string ReportId);
        string ReportDelMain(string ReportId);
        string SubmitFormMain(ReortMXListVO reortMXListVO);
        ReortMXListVO GetReportMXData(string ReportId);
    }
}
