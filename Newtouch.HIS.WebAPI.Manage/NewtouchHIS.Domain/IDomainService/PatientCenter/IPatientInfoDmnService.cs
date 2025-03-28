using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Lib.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewtouchHIS.Domain.Enum.HisEnum;

namespace NewtouchHIS.Domain.IDomainService.PatientCenter
{
    public interface IPatientInfoDmnService:IScopedDependency
    {
        /// <summary>
        /// 患者基本信息查询（基础）
        /// </summary>
        /// <param name="patIndex"></param>
        /// <param name="brxz">优先获取卡类型信息</param>
        /// <returns></returns>
        Task<SysPatInfoVO> PatientQuery(SysPatIndexVO patIndex, EnumBrxz? brxz);
        /// <summary>
        /// 根据Patid 查询患者信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<SysPatInfoVO> GetPatientbyPatid(int patid, string orgId);
        /// <summary>
        /// 查询患者就诊卡信息 List
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <param name="cardNo"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        Task<List<SysPatCardIndexVO>> GetPatientCard(int patid, string orgId, string? cardNo = null, string? cardType = null);
    }
}
