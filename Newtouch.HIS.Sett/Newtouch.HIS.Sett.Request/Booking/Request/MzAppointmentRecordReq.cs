using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class MzAppointmentRecordReq: BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "BookId is required")]
        public string BookId { get; set; }
        public string Lxdh { get; set; }
        public string CardNo { get; set; }
        /// <summary>
        /// 预约取消原因
        /// </summary>
        public string CancelReason { get; set; }
    }

    public class MzAppointmentRecordListReq : BookingReqBaseDto
    {
        public string CardNo { get; set; }
        public string IDCard { get; set; }
        public string RegType { get; set; }
        public DateTime? OutDate { get; set; }
        public string Dept { get; set; }
        public string DeptName { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatName { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string ksrq { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string jsrq { get; set; }
        /// <summary>
        /// 预约状态
        /// </summary>
        public int? yyzt { get; set; }
    }
}
