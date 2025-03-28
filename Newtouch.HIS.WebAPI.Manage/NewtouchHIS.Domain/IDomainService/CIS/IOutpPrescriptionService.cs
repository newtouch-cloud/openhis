using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    public interface IOutpPrescriptionService:IScopedDependency
    {
        /// <summary>
        /// 获取患者病历处方信息 by jzId
        /// </summary>
        /// <param name="jzId"></param>
        /// <returns></returns>
        Task<List<OutpPrescriptionDataVO>> GetPresDatabyJzId(string jzId);
    }
}
