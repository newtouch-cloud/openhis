using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 一些不知道往哪放的sql查询，放在此处
    /// </summary>
    public interface ICommonDmnService
    {
        /// <summary>
        /// 获取就诊人数（门诊和住院）
        /// </summary>
        VisitNumBO GetVisitNum(string configmethod, string orgId = null);

        /// <summary>
        /// 获取今天之前10周就诊人次详情
        /// </summary>
        /// <param name="month"></param>
        /// <param name="isAdministrator"></param>
        /// <param name="orgId"></param>
        /// <param name="topOrgId"></param>
        MonthVisitNumBO GetWeekNum(string configmethod, string orgId);

        /// <summary>
        /// 获取最后10周日期list
        /// </summary>
        /// <returns></returns>
        List<LastWeekInfo> GetLastWeek();

        /// <summary>
        /// 贵州医保,通过交易流水号获取交易验证码
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        string Gzyb_GetJyyzmByJylsh(string jylsh, string OrgId);

        /// <summary>
        /// 获取药品分类list（非收费大类）
        /// </summary>
        /// <returns></returns>
        IList<SysMedicineClassificationVEntity> GetMedicineClassificationList();

        /// <summary>
        /// 获取项目分类list
        /// </summary>
        /// <returns></returns>
        IList<SysChargeCategoryVEntity> GetChargetItemClassificationList(string orgId);
        /// <summary>
        /// 获取科室绑定的病区
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>

        IList<SysDepartmentWardRelationVO> GetWardbyDept(string orgId, string ks, string keyword);

    }
}
