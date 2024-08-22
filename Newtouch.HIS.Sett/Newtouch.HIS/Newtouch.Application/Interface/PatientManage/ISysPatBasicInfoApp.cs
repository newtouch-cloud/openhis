using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.PrintDto;
using Newtouch.HIS.Sett.Request;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatBasicInfoApp
    {
        /// <summary>
        /// （病历号由系统自动生成）获取最新病历号
        /// </summary>
        /// <returns></returns>
        string Getblh();

        /// <summary>
        /// 获取病人性质列表
        /// </summary>
        /// <returns></returns>
        object GetBRXZList(string orgId);

        /// <summary>
        /// 打印住院信息
        /// </summary>
        /// <param name="info"></param>
        void PrinZYInfo(ZYInfoVO info);

        /// <summary>
        /// 打印腕带
        /// </summary>
        /// <param name="info"></param>
        void PrintWDInfo(WDInfoVO info);

        /// <summary>
        /// 修改入院诊断
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void ModifyRyDiagnosis(ModifyRyDiagnosisRequestDTO request);
























        /// <summary>
        /// 根据patid获取病人基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        SysPatientBasicInfoEntity GetInfoByPatid(string patid);
    }
}
