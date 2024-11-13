using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.InputDto
{
    public class BasyZdDto
    {
        public string Id { get; set; }

        /// <summary>
        /// BAH
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }

        /// <summary>
        /// ZDOrder
        /// </summary>
        /// <returns></returns>
        public int ZDOrder { get; set; }

        /// <summary>
        /// JBDM
        /// </summary>
        /// <returns></returns>
        public string JBDM { get; set; }

        /// <summary>
        /// JBMC
        /// </summary>
        /// <returns></returns>
        public string JBMC { get; set; }

        /// <summary>
        /// RYBQ
        /// </summary>
        /// <returns></returns>
        public string RYBQ { get; set; }

        /// <summary>
        /// RYBQMS
        /// </summary>
        /// <returns></returns>
        public string RYBQMS { get; set; }

        /// <summary>
        /// CYQK
        /// </summary>
        /// <returns></returns>
        public string CYQK { get; set; }

        /// <summary>
        /// CYQKMS
        /// </summary>
        /// <returns></returns>
        public string CYQKMS { get; set; }

        public string zt { get; set; }
        /// <summary>
        /// 1主要诊断 2其他诊断 3 中医主病 4 中医主证
        /// </summary>
        public string ZDLX { get; set; }
        /// <summary>
        /// WM:西医 TCM中医
        /// </summary>
        public string ZDLB { get; set; }
    }
}
