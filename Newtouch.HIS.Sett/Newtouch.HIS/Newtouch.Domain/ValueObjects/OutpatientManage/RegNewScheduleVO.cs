using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class RegNewScheduleVO
    {
        /// <summary>
        /// 排班ID
        /// </summary>
        public decimal ghpbId { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// 出诊科室
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 出诊科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 门诊类型
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 出诊时间段 1：全天 2：上午 3：下午
        /// </summary>
        public string Period { get; set; }
        /// <summary>
        /// 出诊描述
        /// </summary>
        public string PeriodDesc { get; set; }
        /// <summary>
        /// 号源数
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 剩余号源
        /// </summary>
        public int YYNum { get; set; }
        /// <summary>
        /// 出诊开始时间
        /// </summary>
        public string PeriodStart { get; set; }
        /// <summary>
        /// 出诊结算时间
        /// </summary>
        public string PeriodEnd { get; set; }
        /// <summary>
        /// 挂号项目
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// 治疗项目
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// 周几
        /// </summary>
        public string weekdd { get; set; }
        /// <summary>
        /// 挂号项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 治疗项目名称
        /// </summary>
        public string zlxmmc { get; set; }
        /// <summary>
        /// 排班描述
        /// </summary>
        public string pbdesc { get; set; }
        /// <summary>
        /// 对应老HIS科室编码
        /// </summary>
        public string ybksbm { get; set; }
        /// <summary>
        /// 出诊日期
        /// </summary>
        public DateTime? OutDate { get; set; }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal? RegFee { get; set; }
        public string IsBook { get; set; }
    }

    public class MzghbookScheduleVO
    {
        /// <summary>
        /// 预约状态 
        /// </summary>
        public string yyzt { get; set; }
        /// <summary>
        /// 排班日程ID
        /// </summary>
        public decimal ScheduId { get; set; }
        /// <summary>
        /// 出诊科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 出诊科室
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 挂号项目Code
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// 治疗项目Code
        /// </summary>
        public string zlxm { get; set; }

        public string sfxmmc { get; set; }
        public string zlxmmc { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public int QueueNo { get; set; }

        public string ysgh { get; set; }
        public string ysxm { get; set; }
        /// <summary>
        /// 出诊日期
        /// </summary>
        public DateTime OutDate { get; set; }
        public string PeriodDesc { get; set; }
        /// <summary>
        /// 门诊类型
        /// </summary>
        public string Regtype { get; set; }
        /// <summary>
        /// 排班标题
        /// </summary>
        public string Title { get; set; }
        public string AppId { get; set; }
        public int BookId { get; set; }  

        public string xm { get; set; }
        public string xb { get; set; }
        public string kh { get; set; }
        public string zjh { get; set; }
        public string mzh { get; set; }
        public decimal RegFee { get; set; }
    }
}
