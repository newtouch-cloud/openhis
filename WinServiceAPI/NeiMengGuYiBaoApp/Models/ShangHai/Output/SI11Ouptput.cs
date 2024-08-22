using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Output
{
    public class SI11Ouptput:OutputBase
    {
        public string cardtype { get; set; }

        public string cardid { get; set; }

        public string personname { get; set; }

        public string personspectag { get; set; }

        public string accountattr { get; set; }

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

        public string jssqxh { get; set; }

        public string jlc { get; set; }

        public decimal jfje { get; set; }
    }
}
