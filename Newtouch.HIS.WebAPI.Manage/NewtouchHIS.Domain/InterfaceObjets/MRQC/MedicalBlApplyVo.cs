using NewtouchHIS.Base.Domain.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.MRQC
{
    public class MedicalBlApplyVo
    {
        public string? Id { get; set; }
        [StringLength(50, ErrorMessage = "OrganizeId长度限制为50")]
        public string? OrganizeId { get; set; }
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
        /// 入院日期
        /// </summary>
        public DateTime? Ryrq { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? Cyrq { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreatorCode { get; set; }
        //public DateTime? LastModifyTime { get; set; }
        //public string? LastModifierCode { get; set; }
        //public string? zt { get; set; }
    }

    public class MedicalBlApplyResponse
    { 
        public string Id { get; set; }
        public int ApplyStatus { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproverDept { get; set; }
        public string Approver { get; set; }
    }

}
