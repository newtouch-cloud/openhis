using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class DaySettleDetailVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 本次结算金额
        /// </summary>
        public decimal bcje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastJsTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fphs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fphBack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ybzfje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal qtzfje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzzybz { get; set; }
    }
}
