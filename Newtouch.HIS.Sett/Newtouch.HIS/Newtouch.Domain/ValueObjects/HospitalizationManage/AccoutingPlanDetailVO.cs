using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class AccoutingPlanDetailVO
    {

        public string zxId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 门诊住院号
        /// </summary>
        public string mzzyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        public decimal sl { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? zxsj { get; set; }
        public string kflb { get; set; }
        public string zls { get; set; }
        public string zlsgh { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        public string zt { get; set; }
        public decimal je { get; set; }
        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// 治疗日期
        /// </summary>
        public DateTime? zlrq { get; set; }

        /// <summary>
        /// 创建时间（系统执行时间）
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}
