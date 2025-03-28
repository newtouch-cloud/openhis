using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 病历申请记录
    /// </summary>
    [Table("Mr_BlApplyRecord")]
    public class MrBlApplyRecordEntity : IEntity<MrBlApplyRecordEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }
        /// <summary>
        /// 病历模板
        /// </summary>
        /// <returns></returns>
        public string bllx { get; set; }
        /// <summary>
        /// 病历名称
        /// </summary>
        /// <returns></returns>
        public string blmc { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        /// <returns></returns>
        public DateTime? sqsj { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        /// <returns></returns>
        public DateTime? wcsj { get; set; }
        /// <summary>
        /// 质控完成时间
        /// </summary>
        /// <returns></returns>
        public DateTime? zkwcsj { get; set; }

        /// <summary>
        /// 审批状态 0:待审批 1：审批通过  2：退回
        /// </summary>
        public int applyStatus { get; set; }
        /// <summary>
        /// 申请成功返回号
        /// </summary>
        public string applyReturnNo { get; set; }
        /// <summary>
        /// 审批部门、科室
        /// </summary>
        public string ApproveDept { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string ApprovePerson { get; set; }
        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApproveDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? Px { get; set; }
        /// <summary>
        /// brxm
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
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
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
    }
}
