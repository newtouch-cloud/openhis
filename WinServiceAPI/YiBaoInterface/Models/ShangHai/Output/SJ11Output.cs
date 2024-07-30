using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Output
{
    public class SJ11Output:OutputBase
    {
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
