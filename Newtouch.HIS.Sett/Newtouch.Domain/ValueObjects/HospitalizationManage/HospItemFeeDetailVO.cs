using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院项目计费 项目 的 详细信息
    /// </summary>
    public class HospItemFeeDetailVO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int jfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 服务费单价
        /// </summary>
        public decimal fwfdj { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 收费项目
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }

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
        /// 减免金额（不 乘 数量）
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
        /// 综合材料编号
        /// </summary>
        public int? clzhxmbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybbm { get; set; }

        /// <summary>
        /// 金额 = (服务费单价+单价)*数量
        /// </summary>
        public decimal je
        {
            get
            {
                return (this.dj + this.fwfdj) * this.sl;
            }
        }


    }

}
