using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("WF_ProcessTransitionHistory")]
    public class WF_ProcessTransitionHistoryEntity : IEntity<WF_ProcessTransitionHistoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FromNodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ToNodeId { get; set; }

        /// <summary>
        /// 转化状态 0正常，1不同意   2驳回
        /// </summary>
        public int TransitionSate { get; set; }

        /// <summary>
        /// 0运行中,1运行结束,2被召回,3不同意,4被驳回
        /// </summary>
        public int IsFinish { get; set; }

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
