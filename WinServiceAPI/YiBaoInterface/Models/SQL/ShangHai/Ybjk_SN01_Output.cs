using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SN01_Output: SqlBase
    {
        public string Id { get; set; }
        /// <summary>
        /// 门诊号/住院号
        /// </summary>
        public string mzzyh { get; set; }
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 状态操作员
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// 状态日期
        /// </summary>
        public DateTime zt_rq { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }

        public string mxzdh { get; set; }
        public decimal zfzje { get; set; }
        public decimal ybzje { get; set; }
        public string jylsh { get; set; }
    }

    public class Ybjk_SN01_MxXms_Output: SqlBase
    {
        public string id { get; set; }
        public string mzzyh { get; set; }
        public string mxzdh { get; set; }

        public int zt { get; set; }

        public string mainId { get; set; }

        public int xh { get; set; }

        public string clbz { get; set; }

        public string fhxx { get; set; }

        public string bxbz { get; set; }

        public decimal mxxmje { get; set; }

        public decimal mxxmjyfy { get; set; }

        public decimal mxxmybjsfwfy { get; set; }
        public decimal? zfbl { get; set; }
        public decimal? zfje { get; set; }
        public decimal? fyxj { get; set; }
    }
}
