using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    /// <summary>
    /// 门诊病历
    /// </summary>
    public interface IOutpMedicalRecordService:IScopedDependency
    {

        /// <summary>
        /// 患者门诊病历 by mzh
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<OutpMedicalRecordVO> OutpMedicalRecordByMzh(string mzh, string orgId);
        /// <summary>
        /// 患者病历处方信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<OutpPrescriptionDataVO>> OutpPrescriptionDataByMzh(string mzh, string orgId);
    }
}
