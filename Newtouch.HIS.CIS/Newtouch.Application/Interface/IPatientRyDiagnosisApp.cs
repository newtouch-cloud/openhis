using System.Collections.Generic;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.ValueObjects.Inpatient;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// 患者入院诊断
    /// </summary>
    public interface IPatientRyDiagnosisApp
    {
        /// <summary>
        /// 获取入院诊断信息
        /// </summary>
        /// <param name="zhy"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<PatientRyDiagnosisDto> PatientRyDiagnosisQuery(string zhy, string organizeId);

        /// <summary>
        /// 保存入院诊断
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        string SavaInPatDiagnosis(PatInAreaVO patInAreaVo, string organizeId, string userCode);
    }
}