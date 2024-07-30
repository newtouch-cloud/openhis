using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Output
{
    public class SJ21Output:OutputBase
    {
        public string cardtype { get; set; }

        public string cardid { get; set; }

        public string personname { get; set; }

        public string accountattr { get; set; }

        public string curaccountamt { get; set; }

        public string hisaccountamt { get; set; }
    }
}
