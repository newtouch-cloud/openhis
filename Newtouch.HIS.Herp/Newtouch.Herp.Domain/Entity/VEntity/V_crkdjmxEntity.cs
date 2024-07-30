using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 出入库单据明细
    /// </summary>
    public class VCrkdjmxEntity
    {
        /// <summary>
        /// 出入库明细ID
        /// </summary>
        public long crkmxId { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 数量，带单位
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 零售总额
        /// </summary>
        public decimal ljze { get; set; }

        /// <summary>
        /// 进价总额
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// 进销差价
        /// </summary>
        public decimal jxcj { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }
    }
}
