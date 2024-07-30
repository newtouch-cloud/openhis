using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using System;

namespace Newtouch.HIS.Application.Interface.HospitalizationManage
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInpatientRefundApp
    {
        /// <summary>
        /// 获取(计费和已退合计的)计费明细（包括治疗项目和非治疗项目）
        /// </summary>
        InpatientSettPatStatusDetailDto InpatientRefundQuery(string zyh, DateTime? kssj, DateTime? jssj, string ks = null, string xmlb = null, string xmmc = null);
     }
}
