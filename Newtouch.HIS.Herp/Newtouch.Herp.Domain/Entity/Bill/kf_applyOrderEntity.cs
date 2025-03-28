using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 出入库单据
    /// </summary>
    [Table("kf_applyOrder")]
    public class KfApplyOrderEntity : IEntity<KfApplyOrderEntity>
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
        /// 单据类型 1-科室申领；2-库房申领
        /// </summary>
        public int applyType { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// 入库部门
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// 出库部门
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// 0:待处理；1:审核通过；2:审核不通过；3:配送；4:部分完成；5:完成；6:拒收
        /// </summary>
        public int applyProcess { get; set; }

        /// <summary>
        /// 状态 0:作废；1.有效
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
