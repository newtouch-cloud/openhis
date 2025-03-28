using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.Entity.QcItemManage
{
    [Table("Mr_Qc_Score")]
    public class MrQcScoreEntity : IEntity<MrQcScoreEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Zyh { get; set; }

        /// <summary>
        /// 病历ID
        /// </summary>
        //public string WritId { get; set; }

        /// <summary>
        /// 质控类型 
        /// </summary>
        public string BllxId { get; set; }

        /// <summary>
        /// 病历模板Id
        /// </summary>
        //public string BlmbId { get; set; }

        /// <summary>
        /// 病历名称
        /// </summary>
        public string Blmc { get; set; }
        /// <summary>
        /// 项目CODE
        /// </summary>
        public string ScoreCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ScoreMc { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public decimal? ScoreMcValue { get; set; }
        /// <summary>
        /// 缺陷数量
        /// </summary>
        public int? sl { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Px { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
