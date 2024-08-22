using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    public class NB_INTERFACE_MZSS_REP
    {
        /// <summary>
        /// 发票总额
        /// </summary>
        public string fpze { get; set; }

        /// <summary>
        /// 可报总额
        /// </summary>
        public string kbze { get; set; }

        /// <summary>
        /// 自费金额
        /// </summary>
        public string zfje { get; set; }

        /// <summary>
        /// 自付总额
        /// </summary>
        public string zfze { get; set; }

        /// <summary>
        /// 一次补偿总额
        /// </summary>
        public string zbcze { get; set; }

        /// <summary>
        /// 二次可报总额
        /// </summary>
        public string qkbze { get; set; }

        /// <summary>
        /// 二次补偿总额
        /// </summary>
        public string qbcze { get; set; }

        /// <summary>
        /// 救助总额
        /// </summary>
        public string jzze { get; set; }

        /// <summary>
        /// 民政救助金额
        /// </summary>
        public string mzjzje { get; set; }

        /// <summary>
        /// 总补偿额
        /// </summary>
        public string bcze { get; set; }

    }
}
