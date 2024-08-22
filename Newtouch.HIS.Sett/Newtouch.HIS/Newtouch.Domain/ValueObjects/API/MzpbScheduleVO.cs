using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MzpbScheduleVO
    {
        public string ScheduId { get; set; }
        public string ghpbId { get; set; }
        /// <summary>
        /// 就诊日期
        /// </summary>
        public string OutDate { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
        public string czks { get; set; }
        public string czksmc { get; set; }
        /// <summary>
        /// mjzbz 门诊类型
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// 排班描述
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 时间段 0无 1全天 2上午 3下午
        /// </summary>
        public string Period { get; set; }
        public string PeriodDesc { get; set; }
        /// <summary>
        /// 号源总数
        /// </summary>
        public int? TotalNum { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        public decimal? RegFee { get; set; }
        /// <summary>
        /// 是否开放预约
        /// </summary>
        public string IsBook { get; set; }
        /// <summary>
        /// 挂号项目
        /// </summary>
        public string ghlx { get; set; }
        public string ghxmmc { get; set; }
        /// <summary>
        /// 诊疗项目
        /// </summary>
        public string zlxm { get; set; }
        public string zlxmmc { get; set; }
        /// <summary>
        /// 星期
        /// </summary>
        public int? weekdd { get; set; }
        public string IsCancel { get; set; }
        public string CancelReason { get; set; }
        
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// 剩余号源
        /// </summary>
        public int? BookNum { get; set; }
    }
}
