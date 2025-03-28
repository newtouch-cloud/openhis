using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：周珏琦
    /// 日 期：2019-02-11 15:37
    /// 描 述：手术安排
    /// </summary>
    [Table("zy_OperationArrangement")]
    public class InpatientOperationArrangementEntity : IEntity<InpatientOperationArrangementEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 临时医嘱id
        /// </summary>
        public string lsyzid { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// 安排日期
        /// </summary>
        /// <returns></returns>
        public DateTime aprq { get; set; }
        /// <summary>
        /// 手术地址
        /// </summary>
        /// <returns></returns>
        public string ssAddr { get; set; }
        /// <summary>
        /// 紧急
        /// </summary>
        /// <returns></returns>
        public string urgent { get; set; }
        /// <summary>
        /// 手术医生ID
        /// </summary>
        /// <returns></returns>
        public string surgeonId { get; set; }
        /// <summary>
        /// 手术医生姓名
        /// </summary>
        public string surgeonName { get; set; }
        /// <summary>
        /// 助手ID
        /// </summary>
        /// <returns></returns>
        public string assistant { get; set; }
        /// <summary>
        /// 助手姓名
        /// </summary>
        public string assistantName { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public string anesthesiaType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string remark { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        public string zt { get; set; }
    }
}