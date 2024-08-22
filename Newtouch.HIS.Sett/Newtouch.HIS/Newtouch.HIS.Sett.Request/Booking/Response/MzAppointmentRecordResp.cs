using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class MzAppointmentRecordListResp
    {
        public string Dept { get; set; }
        public string RegType { get; set; }
        public int QueueNo { get; set; }
        public string PatName { get; set; }
        public string Gender { get; set; }
        public int BookID { get; set; }
        public string BookStatus { get; set; }
        public DateTime OutDate { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal? RegFee { get; set; }
    }

    public class MzAppointmentRecordResp
    {
        /// <summary>
        /// 预约号
        /// </summary>
        /// <returns></returns>
        public int BookId { get; set; }

        /// <summary>
        /// 预约状态 1 已约 2 已挂号 3 已取消预约 EnumMzyyzt
        /// </summary>
        /// <returns></returns>
        public string BookStatus { get; set; }

        /// <summary>
        /// 门诊排班日程
        /// </summary>
        /// <returns></returns>
        public int ScheduId { get; set; }

        /// <summary>
        /// brxz 挂号时 自费 医保
        /// </summary>
        /// <returns></returns>
        public string Ghxz { get; set; }

        public string PatName { get; set; }
        public string Gnder { get; set; }
        public string Lxdh { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        /// <returns></returns>
        public string CardNo { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        /// <returns></returns>
        public string Dept { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        /// <returns></returns>
        public string Doctor { get; set; }

        /// <summary>
        /// 就诊日期
        /// </summary>
        /// <returns></returns>
        public DateTime OutDate { get; set; }

        /// <summary>
        /// 挂号类型 门诊 急诊 专家
        /// </summary>
        /// <returns></returns>
        public string RegType { get; set; }

        public string RegName { get; set; }
        /// <summary>
        /// 时间段 0无 1全天 2上午 3下午
        /// </summary>
        /// <returns></returns>
        public int? Period { get; set; }

        /// <summary>
        /// PeriodStart
        /// </summary>
        /// <returns></returns>
        public string PeriodStart { get; set; }

        /// <summary>
        /// PeriodEnd
        /// </summary>
        /// <returns></returns>
        public string PeriodEnd { get; set; }

        /// <summary>
        /// QueueNo
        /// </summary>
        /// <returns></returns>
        public int QueueNo { get; set; }

        /// <summary>
        /// RegFee
        /// </summary>
        /// <returns></returns>
        public decimal? RegFee { get; set; }

        /// <summary>
        /// 已付费用
        /// </summary>
        /// <returns></returns>
        public decimal? PayFee { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>
        /// <returns></returns>
        public string PayLsh { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        /// <returns></returns>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// CancelReason
        /// </summary>
        /// <returns></returns>
        public string CancelReason { get; set; }

        /// <summary>
        /// CancelTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// mzh
        /// </summary>
        /// <returns></returns>
        public string Mzh { get; set; }

        /// <summary>
        /// ghrq
        /// </summary>
        /// <returns></returns>
        public DateTime? Ghrq { get; set; }

    }
}
