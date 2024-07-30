using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SI52_Output:SqlBase
    {
        public string Id { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }


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

        public string lsh { get; set; }


        public string jssqxh { get; set; }

        public decimal totalexpense { get; set; }

        public decimal qfdzhzfs { get; set; }

        public decimal tcdzhzfs { get; set; }

        public decimal fjdzhzfs { get; set; }

        public decimal qfdxjzfs { get; set; }

        public decimal tcdxjzfs { get; set; }

        public decimal fjdxjzfs { get; set; }

        public decimal tczfs { get; set; }

        public decimal fjzfs { get; set; }

        public decimal curaccountamt { get; set; }

        public decimal hisaccountamt { get; set; }

        public decimal ybjsfwfyze { get; set; }

        public decimal fybjsfwfyze { get; set; }

        public decimal jfje { get; set; }

        public decimal? tcdgjzhzfs { get; set; }
        public decimal? fjdgjzhzfs { get; set; }
        public decimal? qfdgjzhzfs { get; set; }
    }
}
