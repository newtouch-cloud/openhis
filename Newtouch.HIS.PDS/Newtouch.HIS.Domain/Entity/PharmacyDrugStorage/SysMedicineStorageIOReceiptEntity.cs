using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品出入库单据
    /// </summary>
    [Table("xt_yp_crkdj")]
    public class SysMedicineStorageIOReceiptEntity : IEntity<SysMedicineStorageIOReceiptEntity>
    {
        /// <summary>
        /// 出入库ID
        /// </summary>
        [Key]
        public string crkId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 单据类型 
        /// 1：药品入库  单据号YPRKD111111111111
        /// 2：外部出库  单据号WBCKD111111111111
        /// 3：直接出库  单据号NBFYD111111111111
        /// 4：申领出库  单据号SLCKD111111111111
        /// 5：内部发药退回    单据号NBFYTHD111111111111
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string Pdh { get; set; }

        /// <summary>
        /// 入库部门
        /// </summary>
        public string Rkbm { get; set; }

        /// <summary>
        /// 出库部门
        /// </summary>
        public string Ckbm { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? Rksj { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime? Cksj { get; set; }

        /// <summary>
        /// 入库操作员
        /// </summary>
        public string Rkczy { get; set; }

        /// <summary>
        /// 出库操作员
        /// </summary>
        public string Ckczy { get; set; }

        /// <summary>
        /// 来自 NewtouchHIS_Base.dbo.xt_ypcrkfs表的方式代码 
        /// </summary>
        public string Crkfsdm { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? Czsj { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? Sqsj { get; set; }

        /// <summary>
        /// 审核操作员
        /// </summary>
        public string Shczy { get; set; }

        /// <summary>
        /// 0:未审核 1:已通过 2：未通过
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 状态 1-有效  0-无效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改人员
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
