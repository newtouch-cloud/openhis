using System;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    public class OutpatientLogQueryVO
    {
        public string jzId { get; set; }
        public string mzh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        //public int age { get; set; }
        public string sfzh { get; set; }
        public string ContactNum { get; set; }
        public string ghksmc { get; set; }
        public string zdmc { get; set; }
       // public string zyzdmc { get; set; }
        public string cfz { get; set; }
        public string zs { get; set; }
        public string xueya { get; set; }
        public string xuetang { get; set; }
        public decimal? tiwen { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string zy { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string dz { get; set; }
        public string fbsj { get; set; }
        public string jzsj { get; set; }
        /// <summary>
        /// 现病史
        /// </summary>
        public string xbs { get; set; }

        public string clfa { get; set; }
        public DateTime createtime { get; set; }
        public string jzys { get; set; }
        public string jzxm { get; set; }
        public string dhhm { get; set; }
        public string jzysmc { get; set; }
        public string fzjc { get; set; }
        public string nlshow { get; set; }
		public string xuetangclfs { get; set; }
		public string dqxuetang { get; set; }
    }
}
