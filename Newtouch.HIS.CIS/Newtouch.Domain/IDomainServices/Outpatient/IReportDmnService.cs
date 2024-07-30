using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto.Outpatient.API;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ViewModels;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReportDmnService
    {
        /// <summary>
        /// 门诊处方单
        /// </summary>
        /// <param name="cfId"></param>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        string GetCFDjson(string cfId, string mzh, string orgId);

        string GetJYDjson(string cfId, string mzh, string orgId);

        string GetJCDjson(string cfId, string mzh, string orgId);

        string GetWHDjson(string cfId, string mzh, string orgId);

        string GetZSDjson(string cfId, string mzh, string orgId);

        /// <summary>
        /// 门诊处方单
        /// </summary>
        /// <param name="cfId"></param>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        string GetZCYCFjson(string cfId, string mzh, string orgId);
        /// <summary>
        /// 报表列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="rptname"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<ReportConfigVO> ReportList(Pagination pagination, string orgId, string rptname, string keyword);
        /// <summary>
        /// 报表配置详情
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        ReportConfigVO ReportConfInfo(string orgId, string keyValue);
        void ReportAdd(ReportConfigVO ety, string keyValue, string rygh);
        void ReportDelete(string keyValue, string rygh, string orgId);
    }
}
