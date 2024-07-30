using System;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class MoneyUpperLimitReminderSelectVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string sxtxId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ysGh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? jesx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int reminderType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
