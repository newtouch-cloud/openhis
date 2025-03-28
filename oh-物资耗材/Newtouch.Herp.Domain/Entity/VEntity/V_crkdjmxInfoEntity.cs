using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 出入库单据明细
    /// </summary>
    public class VCrkdjmxInfoEntity
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// 批发号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 进价
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// 最小单位进价
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string unitName { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 总金额  进价
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 现有库存
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// 入库部门库存 
        /// </summary>
        public int rkbmkc { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 最小单位名称
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// 最小单位ID
        /// </summary>
        public string zxdwId { get; set; }
                
        /// <summary>
        /// 最小单位转化因子
        /// </summary>
        public int zxdwzhyz { get; set; }

        /// <summary>
        /// 部门单位名称
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// 部门单位Id
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// 部门单位转化因子
        /// </summary>
        public int bmdwzhyz { get; set; }

        /// <summary>
        /// 最小零售价
        /// </summary>
        public decimal minlsj { get; set; }

        /// <summary>
        /// 出库部门
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// 可用库存数量
        /// </summary>
        public int kykcsl { get; set; }
        /// <summary>
        /// 国家医保代码
        /// </summary>
        public string gjybdm { get; set; }
    }
}
