using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain
{
    /// <summary>
    /// 药品出入库方式
    /// </summary>
    [Table("xt_ypcrkfs")]
    public class XtypcrkfsEntity : IEntity<XtypcrkfsEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int crkfsId { get; set; }

        /// <summary>
        /// 出入库方式代码
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// 出入库方式名称
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// 出入库备注 0:入库  1：出库
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// 状态 1：有效  0：无效
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
        /// 修改着
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 修改时间    
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}