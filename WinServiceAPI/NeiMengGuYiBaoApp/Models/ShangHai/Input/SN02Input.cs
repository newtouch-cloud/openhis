using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Input
{
    public class SN02Input:InputBase
    {
        /// <summary>
        /// 凭证类别
        /// </summary>
        public string cardtype { get; set; }

        /// <summary>
        /// 凭证码
        /// </summary>
        public string carddata { get; set; }
        /// <summary>
        /// 明细账单号
        /// </summary>
        public string mxzdh { get; set; }
    }
}
