using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 处方待结算信息 mzh+sfxm分组计费
    /// </summary>
    public class PresSettleInfoVO : ChargeRightDto
    {
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string kh { get; set; }

    }
    /// <summary>
    /// 住院待结算信息
    /// </summary>
    public class ZyPresSettleInfoVO : HospFeeChargeCategoryGroupDetailVO
    {
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

    }
}
