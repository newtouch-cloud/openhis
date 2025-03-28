using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage
{
    public class InpatientSettPatStatusDetailDto
    {
        /// <summary>
        /// 住院病人基本信息
        /// </summary>
        public InpatientSettPatInfoVO InpatientSettPatInfo { get; set; }

        /// <summary>
        /// 住院计费项目明细（包括治疗项目和非治疗项目）
        /// </summary>
        public InpatientSettleItemBO InpatientSettleItemBO { get; set; }

        /// <summary>
        /// ChargeCategoryGropuFee
        /// </summary>
        public IList<HospFeeChargeCategoryGroupVO> GroupFeeVOList { get; set; }

        public InpatIentFeeSumVo InpatIentFeeSum { get; set; }

    }
}
