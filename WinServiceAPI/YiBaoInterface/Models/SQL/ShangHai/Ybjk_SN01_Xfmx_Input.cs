using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SN01_Xfmx_Input: SqlBase
    {
        public string Id { get; set; }

        public string mainid { get; set; }
        public string mzzyh { get; set; }
        public string mxzdh { get; set; }

        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }

        public int xh { get; set; }

        public string mxxflsh { get; set; }

        public string mxxmbmzl { get; set; }

        public string mxxmmczl { get; set; }

        public string mxxmdwzl { get; set; }

        public decimal mxxmdjzl { get; set; }

        public decimal mxxmslzl { get; set; }

        public decimal mxxmjezl { get; set; }

        public string bxbzzl { get; set; }

        public string yyclppzl { get; set; }

        public string zczhzl { get; set; }

        public string mxxmggzl { get; set; }

        public string sftfbzzl { get; set; }

        public string mxxmsyrqzl { get; set; }
    }
}
