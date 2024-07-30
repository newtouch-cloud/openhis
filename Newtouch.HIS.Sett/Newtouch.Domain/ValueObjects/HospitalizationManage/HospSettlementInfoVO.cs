using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class HospSettlementInfoVO
    {
        public int jsnm { get; set; }
        public string zyh { get; set; }
        public string xm { get; set; }
        public string brxzmc { get; set; }
        public DateTime? ryrq { get; set; }
        public DateTime? cyrq { get; set; }
        public string fph { get; set; }
        public decimal zje { get; set; }
        public decimal? xjzf { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string xb { get; set; }
        public string zjh { get; set; }
        public string csrq { get; set; }
        public string isxsr { get; set; }
        public string mz { get; set; }
        public string zzys { get; set; }
        public string gz { get; set; }
        public string lyfs { get; set; }
        public string cyzd { get; set; }
        public string jtdz { get; set; }
    }

    /// <summary>
    /// 分类费用
    /// </summary>
    public class HospSettlementClassificationFeeVO
    {
        public string dlmc { get; set; }
        public string dlcode { get; set; }

        public decimal je { get; set; }

        /// <summary>
        /// 收费日期（yyyy-MM-dd）
        /// </summary>
        public string sfrq { get; set; }

    }

}
