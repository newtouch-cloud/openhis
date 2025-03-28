using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院药品计费 药品 的 详细信息
    /// </summary>
    public class HospMedicinFeeDetailVO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int jfbbh { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 收费大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 收费大类 名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 减免金额（不用 乘 数量）
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        /// 减免比例
        /// </summary>
        public decimal jmbl { get; set; }

        /// <summary>
        /// 自负比例
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary>
        public string ybbm { get; set; }

        /// <summary>
        /// 金额 = 单价 * 数量
        /// </summary>
        public decimal je
        {
            get
            {
                return this.dj * this.sl;
            }
        }

    }
}
