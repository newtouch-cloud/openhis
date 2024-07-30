/**** 定义与 ApiManage规则一致 ******/
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    /// <summary>
    /// 诊疗申请结果通知 Request
    /// </summary>
    public class TreatedApplyResultRequest
    {
        /// <summary>
        /// His 诊疗申请Id
        /// </summary>
        public string ApplyId { get; set; }
        /// <summary>
        /// 第三方诊疗申请流水号
        /// </summary>
        public string? Sqlsh { get; set; }
        /// <summary>
        /// 诊疗申请状态（HIS） EmunRemoteTreatedStu
        /// </summary>
        [Required(ErrorMessage = "诊疗申请状态不可为空")]
        public int ApplyStu { get; set; }
        /// <summary>
        /// 医生是否已确认 
        /// true 申请通过， false 申请驳回
        /// </summary>
        [Required(ErrorMessage = "医生确认结果不可为空")]
        public bool IsConfirm { get; set; }
        /// <summary>
        /// 会议号
        /// </summary>
        public string roomid { get; set; }
    }
    /// <summary>
    /// 获取云诊所患者病历 Request
    /// </summary>
    public class PatMedicalRecordRequest
    {
        /// <summary>
        /// His 诊疗申请Id
        /// </summary>
        [Required(ErrorMessage = "His 诊疗申请Id不可为空")]
        public string ApplyId { get; set; }
        /// <summary>
        /// 第三方诊疗申请流水号
        /// </summary>
        public string? Sqlsh { get; set; }
    }
    /// <summary>
    /// 获取云诊所患者病历 Response
    /// </summary>
    public class PatMedicalRecordResponse : OutpMedicalRecordVO
    {
        public string patid { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }
    }
    /// <summary>
    /// 向第三方发送HIS病历
    /// </summary>
    public class SendPatMedicalRecordRequest: OutpMedicalRecordVO
    {
        public string ApplyId { get; set; }

        public string Sqlsh { get; set; }
    }
}
