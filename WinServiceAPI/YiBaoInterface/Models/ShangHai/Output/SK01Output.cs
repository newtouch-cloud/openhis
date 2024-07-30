using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Output
{
    public class SK01Output:OutputBase
    {
        public string cardtype { get; set; }

        public string cardid { get; set; }

        public string personname { get; set; }

        public string accountattr { get; set; }

        public string translsh { get; set; }

        public decimal curaccount { get; set; }

        public decimal hisaccount { get; set; }

        public decimal zfcash { get; set; }

        public decimal tchisaccount { get; set; }

        public decimal tccash { get; set; }

        public decimal tc { get; set; }

        public decimal dffjhisaccount { get; set; }

        public decimal dffjcash { get; set; }

        public decimal dffj { get; set; }

        public decimal curaccountamt { get; set; }

        public decimal hisaccountamt { get; set; }
    }
}
