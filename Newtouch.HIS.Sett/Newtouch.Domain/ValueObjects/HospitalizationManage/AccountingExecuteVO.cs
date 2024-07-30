using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    /// <summary>
    /// 查询执行结果 门诊住院通用
    /// </summary>
    public class AccountingExecuteVO
    {
        /// <summary>
        /// 执行记录Id
        /// </summary>
        public string zxjlId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 患者姓名
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
        public string zt { get; set; }

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

        /// <summary>
        /// 数量（单次治疗量换算过来的）
        /// </summary>
        public decimal? sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlsmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zlrq { get; set; }

        public string Creater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfzt { get; set; }
    }
}
