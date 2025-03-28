using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 就诊信息
    /// </summary>
    public interface ITreatmentDmnService
    {
        /// <summary>
        /// 查询就诊信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TreatmentVo> SelectTreatmentEntities(string mzh, string orgId);

        TreatEntityObj SelectTreatmentEntitie(string mzh, string orgId);
        /// <summary>
        /// 查询患者就诊信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="xm"></param>
        /// <param name="jzlx"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        IList<PatientVisitInfoVO> GetPatientVisitList(Pagination pagination, string kh, string xm, string jzlx, string jszt, DateTime? kssj, DateTime? jssj, string orgId);
        /// <summary>
        /// 获取住院信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        InPatientInfoVO GetInPatientInfo(string zyh, string orgId);
    }
}