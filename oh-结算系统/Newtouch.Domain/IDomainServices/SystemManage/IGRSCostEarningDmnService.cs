using System.Collections.Generic;
using System.Web;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IGRSCostEarningDmnService
    {
        /// <summary>
        /// 添加成本信息list
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="srxxId"></param>
        void AddCostList(List<jgssCostVO> vo, string srxxId);

        /// <summary>
        /// 站点收支统计表-收入信息详情
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        List<jgssSiteCostEarningVO> GetCosttable(string site, string year, string month);


        /// <summary>
        /// 添加收支
        /// </summary>
        /// <param name="vo"></param>
        void submitEarningInfo(SiteCostEarningVO vo, string keyvalue, string orgId);
        List<jgssxxGridVO> GetjgszInfoList(Pagination pagination, string siteId, string year, string month, string shzt, bool verify);

        List<jgszchargemoneyVO> GetMoneyDetailList(Pagination pagination, string siteId, string year, string month, string type, string dlcode);

        /// <summary>
        /// 查看站点收费金额详情
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="type"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        IList<jgszchargemoneyVO> GetMoneyDetailList(string siteId, string year, string month, string type, string dlCode);

        /// <summary>
        /// 查看站点收支统计表
        /// </summary>
        /// <param name="srxxId"></param>
        /// <returns></returns>
        SiteCostEarningVO GetEarningInfo(string srxxId, string orgId);

        /// <summary>
        /// 获取机构实收和GRS实收
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        jgssandgrsssVO Getss(string site, string year, string month);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="srxxId"></param>
        void deletess(string srxxId);

        string UploadFile(HttpPostedFileBase file, out string parth);

        /// <summary>
        /// 获取上个月的成本信息
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        List<jgssCostVO> getLastcbxx(string site, string year, string month);
    }
}
