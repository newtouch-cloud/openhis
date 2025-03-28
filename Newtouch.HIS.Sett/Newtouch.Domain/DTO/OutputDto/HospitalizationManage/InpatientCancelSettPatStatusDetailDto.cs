using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;

namespace Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage
{
    public class InpatientCancelSettPatStatusDetailDto
    {
        /// <summary>
        /// 住院病人基本信息
        /// </summary>
        public InpatientSettPatInfoVO InpatientSettPatInfo { get; set; }

        /// <summary>
        /// 最后一次（未退的）结算
        /// </summary>
        public HospSettlementEntity LastUnCancelledSett { get; set; }

        /// <summary>
        /// 医保费用相关
        /// </summary>
        public HospSettlementGAYBFeeEntity YbFee { get; set; }
        /// <summary>
        /// 贵安新农合交易相关
        /// </summary>

        public HospSettlementGAXNHFeeEntity XnhFee { get; set; }
		///// <summary>
		///// 重庆医保结算
		///// </summary>
	 //   public CqybSett05Entity CqYbFee { get; set; }
	}
}
