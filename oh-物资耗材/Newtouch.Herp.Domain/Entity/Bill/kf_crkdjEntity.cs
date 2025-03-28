using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 出入库单据
    /// </summary>
    [Table("kf_crkdj")]
    public class KfCrkdjEntity : IEntity<KfCrkdjEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 1.外部入库；2.外部出库；3.直接出库；4.申领出库；5.内部退货
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// 票单号
        /// </summary>
        public string Pdh { get; set; }

        /// <summary>
        /// 出库部门
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// 入库部门
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime? cksj { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? rksj { get; set; }

        /// <summary>
        /// 入库操作员
        /// </summary>
        public string rkczy { get; set; }

        /// <summary>
        /// 出库操作员
        /// </summary>
        public string ckczy { get; set; }

        /// <summary>
        /// 出入库方式
        /// </summary>
        public string crkfs { get; set; }

        /// <summary>
        /// 审核操作员
        /// </summary>
        public string shczy { get; set; }

        /// <summary>
        /// 0:待审核；1.通过；2.不通过
        /// </summary>
        public string auditState { get; set; }

        /// <summary>
        /// 配送单号
        /// </summary>
        public string deliveryNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
