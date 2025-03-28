using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage
{
    public class InpatientSettleItemBO
    {
        /// <summary>
        /// 治疗项目计费表
        /// </summary>
        public IList<TreatmentItemFeeDetailVO> TreatmentItemList { get; set; }

        /// <summary>
        /// 药品计费表
        /// </summary>
        public IList<DrugFeeDetailVO> DrugList { get; set; }

        /// <summary>
        /// 非治疗项目计费表
        /// </summary>
        public IList<NonTreatmentItemFeeDetailVO> Non_treatmentItemList { get; set; }
    }
}
