using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 物资批次信息
    /// </summary>
    public class VProductBatchInfoEntity
    {
        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 库存数量，最小单位
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 可用库存（kcsl-djsl），最小单位
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// 单位库存，部门单位
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime yxq { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// 部门单位进价
        /// </summary>
        public decimal bmjj { get; set; }

        /// <summary>
        /// 最小单位进价
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// 进价总额
        /// </summary>
        public decimal jjzje { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 进价单位单价
        /// </summary>
        public string jjdwdj { get; set; }
    }
}
