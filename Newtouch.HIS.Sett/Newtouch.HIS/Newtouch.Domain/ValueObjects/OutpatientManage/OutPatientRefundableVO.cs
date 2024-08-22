using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊退费 Query
    /// </summary>
    public class OutPatientRefundableVO
    {

        /// <summary>
        /// 
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? sl { get; set; }

        /// <summary>
        /// 可退数量
        /// </summary>
        public decimal? ktsl { get; set; }
        public decimal? tsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jsmxje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sqdzt { get; set; }

        public string cflxmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 计费类型 1药品 2项目
        /// </summary>
        public int feeType { get; set; }

        /// <summary>
        /// 药品的成组号
        /// </summary>
        public string czh { get; set; }


        public int? cfnm { get; set; }
        public string sfxmCode { get; set; }
        public string sfdlCode { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }

        /// <summary>
        /// 项目内码（未关联处方）
        /// </summary>
        public int? xmnm { get; set; }

        /// <summary>
        /// 药品明细主键
        /// </summary>
        public int? cfmxId { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string ysmc { get; set; }

        public DateTime klsj { get; set; }

        /// <summary>
        /// 是治疗计划，且未执行状态
        /// </summary>
        public bool? zljhwzx { get; set; }

        public string jslx { get; set; }
        public int ghnm { get; set; }
        /// <summary>
        /// 收费组套ID
        /// </summary>
        public string ztId { get; set; }
        /// <summary>
        /// 组套数量
        /// </summary>
        public int? ztsl { get; set; }
        /// <summary>
        /// 组套名称
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        /// 收费模板编号
        /// </summary>
        public string sfmb { get; set; }
    }
}
