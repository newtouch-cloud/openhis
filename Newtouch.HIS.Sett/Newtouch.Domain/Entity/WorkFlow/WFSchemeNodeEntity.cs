using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("WF_SchemeNode")]
    public class WF_SchemeNodeEntity : IEntity<WF_SchemeNodeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemeInfoCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemeInfoVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 0所有人,1指定成员
        /// </summary>
        public bool AuthorizeType { get; set; }

        /// <summary>
        /// 枚举 开始节点、发起节点、一般节点、或分节点、或并节点、结束节点
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? zdzxbz { get; set; }

        /// <summary>
        /// 枚举 1 不同意（流程结束） 2 驳回至上一步 3 驳回至发起节点
        /// </summary>
        public int? htlx { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
