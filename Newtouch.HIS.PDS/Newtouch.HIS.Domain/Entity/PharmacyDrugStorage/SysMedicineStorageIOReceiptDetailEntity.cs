using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品出入库明细
    /// </summary>
    [Table("xt_yp_crkmx")]
    public class SysMedicineStorageIOReceiptDetailEntity : IEntity<SysMedicineStorageIOReceiptDetailEntity>
    {
        /// <summary>
        /// 出入库明细ID
        /// </summary>
        [Key]
        public string crkmxId { get; set; }

        /// <summary>
        /// 来自表 XT_YP_CRKDJK的主键
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// 申领单明细ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime? Kprq { get; set; }

        /// <summary>
        /// 到票日期
        /// </summary>
        public DateTime? Dprq { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// 批发价 与zhyz和sl对应
        /// </summary>
        public decimal Pfj { get; set; }

        /// <summary>
        /// 零售价 与zhyz和sl对应
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// 药库批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Zje { get; set; }

        /// <summary>
        /// 数量 部门单位数量
        /// </summary>
        public int Sl { get; set; }

        /// <summary>
        /// 入库转换因子
        /// </summary>
        public int Rkzhyz { get; set; }

        /// <summary>
        /// 入库部门库存
        /// </summary>
        public int Rkbmkc { get; set; }

        /// <summary>
        /// 入库单位（单位名称）
        /// </summary>
        public string rkdw { get; set; }

        /// <summary>
        /// 出库转换因子
        /// </summary>
        public int Ckzhyz { get; set; }

        /// <summary>
        /// 出库部门库存
        /// </summary>
        public int Ckbmkc { get; set; }

        /// <summary>
        /// 出库单位（单位名称）
        /// </summary>
        public string ckdw { get; set; }

        /// <summary>
        /// 外观
        /// </summary>
        public string Wg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zbbz { get; set; }

        /// <summary>
        /// 进库注册证
        /// </summary>
        public string jkzcz { get; set; }

        /// <summary>
        /// 合格证明
        /// </summary>
        public string hgzm { get; set; }

        /// <summary>
        /// 验收结果
        /// </summary>
        public string ysjg { get; set; }

        /// <summary>
        /// 退货原因
        /// </summary>
        public string Thyy { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string Cljg { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// 扣率
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        /// 进价 药库单位进价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// 产地
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 进价总金额 药库单位进价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? pfjze { get; set; }

    }
}
