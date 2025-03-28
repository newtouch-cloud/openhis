using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 患者基本信息管理
    /// </summary>
    public interface IPatientBaseInfoApp
    {
        /// <summary>
        /// 补充患者基本信息
        /// </summary>
        /// <param name="patientBaseInfo"></param>
        /// <returns></returns>
        string SupplementMzPatientBaseInfo(SupplementPatientBaseInfoRequest patientBaseInfo);
    }
}