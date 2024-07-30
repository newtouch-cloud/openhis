using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SJ11_Output:SqlBase
    {
        public string Id { get; set; }
        /// <summary>
        /// 门诊住院号
        /// </summary>
        public string mzzyh { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }
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

        public string cardtype { get; set; }


        public string cardid { get; set; }

        public string jzdyh { get; set; }

        public string personname { get; set; }

        public string sfzh { get; set; }

        public string rysx { get; set; }

        public string gzqk { get; set; }

        public string zcyymc { get; set; }

        public string startdate { get; set; }

        public string enddate { get; set; }

        public string lsh { get; set; }

        public decimal curaccountamt { get; set; }

        public decimal hisaccountamt { get; set; }
    }
}
