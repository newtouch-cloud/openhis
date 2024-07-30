using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SN01_Mxxzy_Input: SqlBase
    {
        public string Id { get; set; }

        public string mzzyh { get; set; }
        public string mxzdh { get; set; }
        public string mainId { get; set; }
       
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }

        public int xh { get; set; }

        public string cfh { get; set; }

        public string deptid { get; set; }

        public string ksmc { get; set; }

        public string cfysh { get; set; }

        public string cfysxm { get; set; }

        public string fylb { get; set; }

        public string mxxmbm { get; set; }

        public string mxxmmc { get; set; }

        public string mxxmdw { get; set; }

        public decimal mxxmdj { get; set; }

        public decimal mxxmsl { get; set; }

        public decimal mxxmje { get; set; }

        public decimal mxxmjyfy { get; set; }

        public decimal mxxmybjsfwfy { get; set; }

        public string yyclpp { get; set; }

        public string zczh { get; set; }

        public string mxxmgg { get; set; }

        public string mxxmsyrq { get; set; }

        public string bxbz { get; set; }

        public string sftfbz { get; set; }

        public string jfbz { get; set; }

        public string sfxfmx { get; set; }
    }
}
