using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Queue
{
    public class QueueInfo
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public decimal ScheduId{get;set;}
        public string pbId { get; set; }
        public string ywbz { get; set; }
        public string ywlx { get; set; }
        public int patid { get; set; }
        public string ywlsh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string brxz { get; set; }
        public string brly { get; set; }
        public string ks { get; set; }
        public string ys { get; set; }
        public string kh { get; set; }
        public string nlshow { get; set; }
        public string pbdesc { get; set; }
        public string czks { get; set; }
        public string czksmc { get; set; }
        public string czys { get; set; }
        public string czysxm { get; set; }
        public string Period { get; set; }
        public string PeriodDesc { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
        public decimal? fee { get; set; }
        public int queno { get; set; }
        /// <summary>
        /// 是否当前叫号
        /// </summary>
        public int iscalling { get; set; }
        /// <summary>
        /// 叫号状态 1 已签到 2 已叫号  3 已过号  4 已应答（叫号结束）
        /// </summary>
        public int calledstu { get; set; }
        /// <summary>
        /// 是否过号
        /// </summary>
        public int ispassed { get; set; }
        /// <summary>
        /// 叫号次数
        /// </summary>
        public int calledtimes { get; set; }
        public string memo { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime qdsj { get; set; }
    }
}
