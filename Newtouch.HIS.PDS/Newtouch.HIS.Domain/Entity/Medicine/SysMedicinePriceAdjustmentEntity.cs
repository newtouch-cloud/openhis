using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品调价
    /// </summary>
    [Table("xt_yptj")]
    public class SysMedicinePriceAdjustmentEntity : IEntity<SysMedicinePriceAdjustmentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string yptjId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal pfj { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// 原批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ypfj { get; set; }

        /// <summary>
        /// 原零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ylsj { get; set; }

        /// <summary>
        /// 审核标志 0:未审核 1:已审核 2:已拒绝 3.已撤销
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 修改类型
        /// </summary>
        public string xglx { get; set; }

        /// <summary>
        /// 调整时间
        /// </summary>
        public DateTime tzsj { get; set; }

        /// <summary>
        /// 调整文件
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime zxsj { get; set; }

        /// <summary>
        /// 调整操作人
        /// </summary>
        public string tzczy { get; set; }

        /// <summary>
        /// 审操作人
        /// </summary>
        public string shczy { get; set; }

        /// <summary>
        /// 执行操作人
        /// </summary>
        public string zxczy { get; set; }

        /// <summary>
        /// 执行标志    0:未执行 1:已执行
        /// </summary>
        public string zxbz { get; set; }

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
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
