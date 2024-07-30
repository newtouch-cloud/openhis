using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SI52_Input:SqlBase
    {
        public string Id { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
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
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string lsh { get; set; }

        public string cardtype { get; set; }

        public string carddata { get; set; }

        public string personspectag { get; set; }

        public string yllb { get; set; }

        public string persontype { get; set; }

        public string gsrdh { get; set; }

        public string cyjsbz { get; set; }

        public string jsksrq { get; set; }

        public string jsjsrq { get; set; }

        public string zyts { get; set; }


        public string deptid { get; set; }

        public string jzdyh { get; set; }

        public string xsywlx { get; set; }

        public string jssqxh { get; set; }
    }

    public class Ybjk_SI52_MxzdhsInput : SqlBase
    {
        public string id { get; set; }

        public string mainid { get; set; }
        public string lsh { get; set; }

        public string zyh { get; set; }
        public string zt { get; set; }

        public decimal? totalexpense { get; set; }

        public decimal? ybjsfwfyze { get; set; }
        public decimal? fybjsfwfyze { get; set; }
    }

    public class Ybjk_SI52_ZdnosInput : SqlBase
    {
        public string id { get; set; }

        public string mainid { get; set; }

        public string zyh { get; set; }
        public string lsh { get; set; }

        public string zdno { get; set; }

        public string zdmc { get; set; }

        public int zt { get; set; }
    }
}
