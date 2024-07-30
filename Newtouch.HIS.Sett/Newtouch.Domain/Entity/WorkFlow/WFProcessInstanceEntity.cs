using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("WF_ProcessInstance")]
    public class WF_ProcessInstanceEntity : IEntity<WF_ProcessInstanceEntity>
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
        public string CustomName { get; set; }

        /// <summary>
        /// 枚举 1普通 9重要
        /// </summary>
        public int wfLevel { get; set; }

        /// <summary>
        /// 当前节点标示
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PreviousId { get; set; }

        /// <summary>
        /// 当前节点（ActivityId）的处理人
        /// </summary>
        public string MarkerList { get; set; }

        /// <summary>
        /// 0运行中,1运行结束,2被召回,3不同意,4被驳回
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

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
