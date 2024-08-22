using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MzpbScheduleResponse
    {
        /// <summary>
        /// 就诊日期
        /// </summary>
        public DateTime OutDate { get; set; }
        public int ScheduId { get; set; }

        public string Doctor { get; set; }
        public string DoctorName { get; set; }
        public string OutDept { get; set; }
        public string OutDeptName { get; set; }
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
        
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        /// <summary>
        /// 挂号费
        /// </summary>
        public decimal? RegFee { get; set; }
        /// <summary>
        /// 挂号费
        /// </summary>
        public decimal? GhFee { get; set; }
        /// <summary>
        /// 诊疗费
        /// </summary>
        public decimal? ZLFee { get; set; }
        /// <summary>
        /// 挂号项目
        /// </summary>
        public string Ghlx { get; set; }
        public string GhxmName { get; set; }
        /// <summary>
        /// 诊疗项目
        /// </summary>
        public string Zlxm { get; set; }
        public string ZlxmName { get; set; }
        /// <summary>
        /// 星期
        /// </summary>
        public int? Weekdd { get; set; }
        /// <summary>
        /// 号源总数
        /// </summary>
        public int? TotalNum { get; set; }
        /// <summary>
        /// 剩余号源
        /// </summary>
        public int? BookNum { get; set; }
    }
}
