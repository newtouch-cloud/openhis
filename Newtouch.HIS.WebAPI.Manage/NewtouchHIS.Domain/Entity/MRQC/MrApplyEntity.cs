using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.MRQC
{
    ///<summary>
    /// 病历申请
    ///</summary>
    [Tenant(DBEnum.MrQcDb)]
    [SugarTable("Mr_Apply", "MrApplyEntity")]
    public class MrApplyEntity: IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// 申请科室
        /// </summary>
        [Required(ErrorMessage = "ApplyDept不可为空")]
        public string ApplyDept { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        [Required(ErrorMessage = "Zyh不可为空")]
        public string Zyh { get; set; }
        /// <summary>
        /// 床位
        /// </summary>
        [Required(ErrorMessage = "Bed不可为空")]
        public string Bed { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        [Required(ErrorMessage = "PatName不可为空")]
        public string PatName { get; set; }
        /// <summary>
        /// 申请医师
        /// </summary>
        [Required(ErrorMessage = "ApplyDoctor不可为空")]
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// 申请类型 0：新建病历 1：修改病历
        /// </summary>
        [Required(ErrorMessage = "ApplyType不可为空")]
        public int ApplyType { get; set; }
        /// <summary>
        /// 病历名称
        /// </summary>
        [Required(ErrorMessage = "MedicalName不可为空")]
        public string MedicalName { get; set; }
        /// <summary>
        /// 质控完成时间
        /// </summary>
        [Required(ErrorMessage = "CompletionDate不可为空")]
        public DateTime? CompletionDate { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        [Required(ErrorMessage = "ApplyDate不可为空")]
        public DateTime? ApplyDate { get; set; }
        /// <summary>
        /// 申请完成时间
        /// </summary>
        [Required(ErrorMessage = "ApplyCompletionDate不可为空")]
        public DateTime? ApplyCompletionDate { get; set; }
        /// <summary>
        /// 申请原因
        /// </summary>
        [Required(ErrorMessage = "ApplyReason不可为空")]
        public string ApplyReason { get; set; }
        /// <summary>
        /// 申请状态 0：未批准 1：已审批
        /// </summary>
        [Required(ErrorMessage = "ApplyStatus不可为空")]
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
        public DateTime? ApprovalDate { get; set; }
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

    }
}
