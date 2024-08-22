using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class SysPatInfoResp
    {
        public int patid { get; set; }
        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 病人最近的性质xt_brxz.brxz
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 本院磁卡号（包括医保离休病人），或者医保卡号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        public string xb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dwdz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dh { get; set; }
        public string kh { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string klx { get; set; }
    }
}
