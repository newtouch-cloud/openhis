using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    public class NB_INTERFACE_MZJS_REQ
    {
        /// <summary>
        /// 卡编号
        /// </summary>
        public string kbh { get; set; }

        /// <summary>
        /// 门诊流水号(代表一张门诊收费发票的唯一流水号))
        /// </summary>
        public string mzlsh { get; set; }

        /// <summary>
        /// 医院代码
        /// </summary>
        public string yydm { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string czydm { get; set; }

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string czymc { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }
    }
}
