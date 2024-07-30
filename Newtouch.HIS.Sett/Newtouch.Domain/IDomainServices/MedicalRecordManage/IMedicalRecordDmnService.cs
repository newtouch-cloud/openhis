using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 病案首页Drg分组测算
    /// </summary>
    public interface IMedicalRecordDmnService
    {
        /// <summary>
        /// 根据住院获取住院病案首页的诊断数据
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<MedicalRecordDiagnosisVO> GetAllMedicalRecordDiagnosisVOList(string zyh, string orgId);
        /// <summary>
        /// 根据住院获取住院病案首页的手术数据
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<MedicalRecordOperationVO> GetAllMedicalRecordOperationVOList(string zyh, string orgId);
        /// <summary>
        /// 调整病案首页的诊断数据的顺序
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        string UpdatePatDiagnosisOrder(string zdIds, string orgId);
        /// <summary>
        /// 调整病案首页的手术数据的顺序
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        string UpdatePatOperationOrder(string ssIds, string orgId);
        PatInformationVO GetPatInformationList(string zyh, string orgId);

        List<MedicalRecordOperationVO> GetAllOpVOList(string ssmc, string orgId);
        List<MedicalRecordPatVO> GetPatMedicalRecordList(string zyh, string kssj, string jssj, string orgId);
    }

}
