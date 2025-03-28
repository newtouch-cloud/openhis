using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// 预约日期
    /// </summary>
    public class SysBespeakRegisterDateTimeVO
    {
        /// <summary>
        /// 临时主键
        /// </summary>
        public string tmpId { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public string regDate { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public string week { get; set; }

        /// <summary>
        /// 预约时间段
        /// </summary>
        public List<SysBespeakRegisterTimeVO> regTimes { get; set; }
    }

    /// <summary>
    /// 预约时间
    /// </summary>
    public class SysBespeakRegisterTimeVO
    {
        /// <summary>
        /// 临时主键
        /// </summary>
        public string tmpId { get; set; }

        /// <summary>
        /// xt_bespeakRegister表ID
        /// </summary>
        public string tabId { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime regBeginTime { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime regEndTime { get; set; }

        /// <summary>
        /// 预约时间  regBeginTime和regEndTime结合
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// 最大预约人数
        /// </summary>
        public int bespeakMaxCount { get; set; }
    }
}
