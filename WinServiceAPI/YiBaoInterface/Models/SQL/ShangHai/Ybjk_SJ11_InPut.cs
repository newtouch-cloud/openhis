using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SJ11_InPut:SqlBase
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

        public string carddata { get; set; }

        public string deptid { get; set; }

        public string djtype { get; set; }

        public string djno { get; set; }

        public string startdate { get; set; }

        public string enddate { get; set; }

        public string dbxm { get; set; }

        public string zd { get; set; }

        public string wtrxm { get; set; }

        public string wtrsfzh { get; set; }

        public string yy { get; set; }

        public string des { get; set; }

        public string dbzl { get; set; }

        public string ysxm { get; set; }

        public string ysgh { get; set; }
    }
}
