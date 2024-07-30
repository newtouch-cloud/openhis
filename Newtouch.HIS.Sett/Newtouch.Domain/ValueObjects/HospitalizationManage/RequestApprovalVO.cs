using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
   /// <summary>
   /// 查询住院医保未审批明细返回对象
   /// </summary>
    public class RequestApprovalVO
    {
        /// <summary>
        /// 费用状态
        /// </summary>
        public string fyzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }

        public int jfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 门诊住院号
        /// </summary>
        public DateTime createtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        public decimal? zje { get; set; }

        public string zyh { get; set; }

        public string jylsh { get; set; }

        public string cfh { get; set; }
    }
}
