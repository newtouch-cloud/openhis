using System;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// 下拉列表物资信息
    /// </summary>
    public class VSelProductInfoVO
    {
        /// <summary>
        /// 物资ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织结构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 库存 最小单位
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 可用库存 最小单位
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// 部门单位数量（带单位）
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// 最小单位零售价
        /// </summary>
        public decimal minlsj { get; set; }

        /// <summary>
        /// 部门单位零售价
        /// </summary>
        public decimal bmlsj { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 部门单位转化因子
        /// </summary>
        public int bmdwzhyz { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 生产商Id
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// 生产商名称
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// 最小单位ID
        /// </summary>
        public string zxdwId { get; set; }

        /// <summary>
        /// 最小单位名称
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// 部门单位ID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// 部门单位名称
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public string lbId { get; set; }
    }


    /// <summary>
    /// 物资批次信息
    /// </summary>
    public class VProductBatchInfoVO
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


    public class RelWarehouseVO
    {
        public string ID { get; set; }
        public string OrganizeId { get; set; }
        public string name { get; set; }
        public string py { get; set; }
        public string isDefSyn { get; set; }
    }
}
