using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.API
{
    public class MrApplyWritingVo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string PatName { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string Zyh { get; set; }
        /// <summary>
        /// 床号
        /// </summary>
        public string Bed { get; set; }
        /// <summary>
        /// 申请科室
        /// </summary>
        public string ApplyDeptName { get; set; }
        /// <summary>
        /// 申请医师
        /// </summary>
        public string ApplyDoctorName { get; set; }
        /// <summary>
        /// 申请科室代码
        /// </summary>
        public string ApplyDept { get; set; }
        /// <summary>
        /// 申请医师代码
        /// </summary>
        public string ApplyDoctor { get; set; }
        /// <summary>
        /// 申请类型 1：修改病历 0：新建病历
        /// </summary>
        public string ApplyType { get; set; }
        /// <summary>
        /// 病历名称
        /// </summary>
        public string MedicalName { get; set; }
        /// <summary>
        /// 质控完成时间
        /// </summary>
        public DateTime CompletionDate { get; set; }
        /// <summary>
        /// 申请完成时间
        /// </summary>
        public DateTime ApplyCompletionDate { get; set; }
        /// <summary>
        /// 申请原因
        /// </summary>
        public string ApplyReason { get; set; }
        public DateTime? Ryrq { get; set; }
        public DateTime? Cyrq { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string bllx { get; set; }
    }
    public class ApplyWritingResp
    {
        public string Id { get; set; }
        public int ApplyStatus { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproverDept { get; set; }
        public string Approver { get; set; }
    }
}
