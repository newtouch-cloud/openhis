using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.Entity.QcItemManage
{
    /// <summary>
    /// 质控病历申请
    /// </summary>
    [Table("Mr_Apply")]
    public class MrApplyEntity : IEntity<MrApplyEntity>
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 申请科室
        /// </summary>
        public string ApplyDept { get; set; }
        /// <summary>
        /// 床位
        /// </summary>
        public string Bed { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatName { get; set; }
        /// <summary>
        /// 申请医师
        /// </summary>
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// 申请类型 0：新建病历 1：修改病历
        /// </summary>
        public int ApplyType { get; set; }
        /// <summary>
        /// 病历名称
        /// </summary>
        public string MedicalName { get; set; }
        /// <summary>
        /// 质控完成时间
        /// </summary>
        public DateTime CompletionDate { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyDate { get; set; }
        /// <summary>
        /// 申请完成时间
        /// </summary>
        public DateTime ApplyCompletionDate { get; set; }
        /// <summary>
        /// 申请原因
        /// </summary>
        public string ApplyReason { get; set; }
        /// <summary>
        /// 申请状态 0：未批准 1：已审批
        /// </summary>
        public int ApplyStatus { get; set; }
        /// <summary>
        /// 批准人
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// 批准科室
        /// </summary>
        public string ApproverDept { get; set; }
        /// <summary>
        /// 批准日期
        /// </summary>
        public string ApprovalDate { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime? Ryrq { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? Cyrq { get; set; }
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
    }
}
