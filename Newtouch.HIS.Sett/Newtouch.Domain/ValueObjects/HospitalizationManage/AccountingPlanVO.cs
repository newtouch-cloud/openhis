using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    /// <summary>
    /// 查询记账计划 门诊住院通用
    /// </summary>
    public class AccountingPlanVO
    {
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
        /// 
        /// </summary>
        public DateTime? startDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zxzt { get; set; }

        /// <summary>
        /// 门诊住院号
        /// </summary>
        public string mzzyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zcs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? yzxcs { get; set; }

        /// <summary>
        /// 剩余次数
        /// </summary>
        public int? sycs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 最后系统执行时间（在我们页面上操作‘执行’的时间）
        /// </summary>
        public DateTime? zhxtzxsj { get; set; }

        /// <summary>
        /// 数量（单次治疗量换算过来的）
        /// </summary>
        public int? sl { get; set; }
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// 医嘱性质 1临时 2长期
        /// </summary>
        public string yzxz { get; set; }
    }
}
