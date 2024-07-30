using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Output
{
    public class SN01Output:OutputBase
    {
        /// <summary>
        /// 明细账单号
        /// </summary>
        public string mxzdh { get; set; }
        /// <summary>
        /// 明细项目计算结果循环体
        /// </summary>
        public List<MxXmsOut> mxxms { get; set; }
    }
    public class MxXmsOut
    {
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
