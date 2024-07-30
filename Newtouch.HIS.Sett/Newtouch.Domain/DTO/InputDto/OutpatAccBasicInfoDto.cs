using Newtouch.HIS.Domain.BusinessObjects;
using System;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    public class OutpatAccBasicInfoDto
    {
        public string xm { get; set; }
        public string xb { get; set; }
        public DateTime csny { get; set; }
        public string zjh { get; set; }
        public string zjlx { get; set; }
        public string blh { get; set; }
        public int patid { get; set; }
        public string brxz { get; set; }
        public string jsr { get; set; }
        public string cfh { get; set; }
        public string mzh { get; set; }
    }

    public class BasicInfoDto2018
    {
        public string xm { get; set; }
        public string xb { get; set; }
        public DateTime csny { get; set; }
        public string zjh { get; set; }
        public string zjlx { get; set; }
        public string blh { get; set; }
        public int patid { get; set; }
        public string brxz { get; set; }
        public string mzh { get; set; }
        public int ghnm { get; set; }
        public string ys { get; set; }
        public string kh { get; set; }


        public DateTime? sfrq { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// 是否是欠费预结
        /// </summary>
        public bool isQfyj { get; set; }

    }
}
