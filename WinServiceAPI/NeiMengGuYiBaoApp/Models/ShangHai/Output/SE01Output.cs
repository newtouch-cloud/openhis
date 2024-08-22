using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Output
{
    public class SE01Output:OutputBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string idno { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>

        public string idtype { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>

        public string ecToken { get; set; }
    }
}
