using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.NonTreatmentItemManage
{
    [NotMapped]
    public class NonTreatmentItemBillingInfoVO: NonTreatmentItemBillingEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string sfxmName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string smksName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string smryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 退的数量
        /// </summary>
        public int? tsl { get; set; }

        /// <summary>
        /// 退的金额
        /// </summary>
        public decimal? tje { get; set; }

        /// <summary>
        /// 售卖时间
        /// </summary>
        public DateTime? SaleTime { get; set; }

        /// <summary>
        /// 退费时间
        /// </summary>
        public DateTime? RefundTime { get; set; }

        /// <summary>
        /// 退费人员
        /// </summary>
        public string tfryName { get; set; }
    }
}
