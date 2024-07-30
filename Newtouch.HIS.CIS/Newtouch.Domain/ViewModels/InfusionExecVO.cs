using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// 注射患者信息
    /// </summary>
    public class InfusionExecVO
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 座/床号
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// 配药师工号
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// 配药师姓名
        /// </summary>
        public string dispenserName { get; set; }

        /// <summary>
        /// 执行者工号
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// 执行者姓名
        /// </summary>
        public string executorName { get; set; }

        /// <summary>
        /// 输液开始时间
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// 输液结束时间
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

    }
}
