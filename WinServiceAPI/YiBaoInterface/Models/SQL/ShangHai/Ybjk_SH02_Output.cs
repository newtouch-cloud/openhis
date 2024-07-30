using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SH02_Output: SqlBase
    {
        public string Id { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }


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

        public string lsh { get; set; }

        public string jssqxh { get; set; }

        public string jmjsbz { get; set; }

        public decimal totalexpense { get; set; }

        public decimal curaccountpay { get; set; }

        public decimal hisaccountpay { get; set; }

        public decimal zfdxjzfs { get; set; }

        public decimal zfdlnzhzfs { get; set; }

        public decimal tcdzhzfs { get; set; }

        public decimal tcdxjzfs { get; set; }

        public decimal tczfs { get; set; }

        public decimal fjdzhzfs { get; set; }

        public decimal fjdxjzfs { get; set; }

        public decimal fjzfs { get; set; }

        public decimal curaccountamt { get; set; }

        public decimal hisaccountamt { get; set; }

        public decimal ybjsfwfyze { get; set; }

        public decimal fybjsfwfyze { get; set; }

        public string jlc { get; set; }

        public decimal jfje { get; set; }
        /// <summary>
        /// 分类自负金额
        /// </summary>
        public decimal? zfje { get; set; }
        public decimal? fjdgjzhzfs { get; set; }
        public decimal? zfdgjzhzfs { get; set; }
        public decimal? gjzhzfs { get; set; }
        public decimal? tcdgjzhzfs { get; set; }
    }
}
