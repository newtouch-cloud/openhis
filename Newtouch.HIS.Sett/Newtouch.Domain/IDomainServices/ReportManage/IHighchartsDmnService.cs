
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.ReportManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.ReportManage
{
    public interface IHighchartsDmnService
    {
        #region 门诊记账1.2 and 1.3版本，不适用多个治疗师和医院的记账逻辑
        /// <summary>
        /// 获取治疗师治疗总费用
        /// </summary>
        List<TherapistVisit> GetTherapistDischarge(string year, string orgId);

        /// <summary>
        /// 获取治疗师治疗总人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetTherapistVisit(string year, string orgId);

        /// <summary>
        /// 获取平均费用（总费用/总人次）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetTherapistavgDischarge(string year, string orgId);
        /// <summary>
        /// 获取治疗师平均治疗人次（总人次/总人数）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetTherapistavgVisit(string year, string orgId);

        /// <summary>
        /// 获取治疗师月度平均治疗人次（总人次/总人数）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistMonthVisit> GetMonthTherapistavgVisit(string year, string orgId, string month);

        /// <summary>
        /// 获取治疗师治疗总费用
        /// </summary>
        List<TherapistMonthCharge> GetMonthTherapistDischarge(string year, string orgId, string month);

        /// <summary>
        /// 获取治疗师月度治疗人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistMonthVisit> GetMonthTherapistVisit(string year, string orgId, string month);


        /// <summary>
        /// 获取月度平均费用（总费用/总人次）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistMonthCharge> GetMonthTherapistavgDischarge(string year, string orgId, string month);
        #endregion

        #region 门诊 
        /// <summary>
        /// 获取门诊就诊人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientVisitNumVO> GetOutpatientVisitNum(string orgId);

        /// <summary>
        /// 获取门诊就诊人次
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientVisitNumVO> GetOutpatientVisitPerNum(string orgId);


        /// <summary>
        /// 门诊收入统计
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientSCNumVO> GetOutpatientSalaryNum(string orgId);
        #endregion

        #region 住院
        /// <summary>
        /// 获取住院收入统计
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<InpatientSCNumVO> GetInpatientSalaryNum(string orgId);

        /// <summary>
        /// 获取住院新增人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientVisitNumVO> GetInpatientVisitNum(string orgId);

        /// <summary>
        /// 获取出院人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<InpatientVisitNumVO> GetInpatientDischargeNum(string orgId);

        /// <summary>
        /// 获取治疗师住院治疗总费用
        /// </summary>
        List<TherapistVisit> GetInTherapistDischarge(string year, string orgId);

        /// <summary>
        /// 获取治疗师住院治疗人次
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetInTherapistVisit(string year, string orgId);

        /// <summary>
        /// 获取治疗之住院平均费用（总费用/总人次）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetInTherapistavgDischarge(string year, string orgId);
        /// <summary>
        /// 获取治疗师住院治疗月度总费用
        /// </summary>
        List<TherapistMonthCharge> GetMonthInTherapistDischarge(string year, string orgId, string month);
        /// <summary>
        /// 获取治疗师住院治疗月度平均费用
        /// </summary>
        List<TherapistMonthCharge> GetMonthInTherapistavgDischarge(string year, string orgId, string month);
        /// <summary>
        /// 获取治疗师住院治疗月度总访问人次
        /// </summary>
        List<TherapistMonthVisit> GetMonthInTherapistVisit(string year, string month, string orgId);

        /// <summary>
        /// 获取治疗师住院治疗月度平均人次
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistMonthVisit> GetMonthInTherapistavgVisit(string year, string month, string orgId);

        /// <summary>
        /// 获取治疗师住院治疗平均人次
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetInTherapistavgVisit(string year, string orgId);
        #endregion

        #region Common
        /// <summary>
        /// 门诊住院收入统计对比
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SCNumBO GetSalaryNumCompared(string orgId);
        #endregion

    }
}
