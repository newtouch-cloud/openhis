using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.Core.Common;
using System.Threading.Tasks;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;

namespace Newtouch.HIS.Application.Interface.PatientManage
{
    public interface IInpatientAccountManageApp
    {
        InpatientReserveDto GetHosPatAccInfo(string zyh);
        InpatientReserveDto GetHosPatAccInfo(string zyh, string zhxz);
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="szid"></param>
        /// <returns></returns>
        bool PayDepositPost(DeposDto dto, out string szid);
        /// <summary>
        /// 取款
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool ReturnDepositPost(DeposDto dto);
        /// <summary>
        /// 余额劝退
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool RefundAccount(DeposDto dto);
        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        decimal GetzhYe(string zyh);

        /// <summary>
        /// 获取患者账户收支记录
        /// </summary>
        /// <returns></returns>
        List<InpatientPatAccPayVO> GetPayList(int zh, string zhxz);
        string GetFinRepSJPZH();

    }
}
