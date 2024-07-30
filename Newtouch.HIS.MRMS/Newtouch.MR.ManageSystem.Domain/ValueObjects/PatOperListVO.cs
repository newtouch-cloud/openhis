using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class PatOperListVO
    {
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
        /// SSOrder
        /// </summary>
        /// <returns></returns>
        public int SSOrder { get; set; }

        public string ssxh { get; set; }
        /// <summary>
        /// 手术及操作编码
        /// </summary>
        /// <returns></returns>
        public string SSJCZBM { get; set; }

        /// <summary>
        /// 手术及操作日期
        /// </summary>
        /// <returns></returns>
        public DateTime SSJCZRQ { get; set; }

        /// <summary>
        /// 手术级别
        /// </summary>
        /// <returns></returns>
        public string SSJB { get; set; }

        /// <summary>
        /// 手术及操作名称
        /// </summary>
        /// <returns></returns>
        public string SSJCZMC { get; set; }

        /// <summary>
        /// 术者
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// I助
        /// </summary>
        /// <returns></returns>
        public string YZ { get; set; }

        /// <summary>
        /// II助
        /// </summary>
        /// <returns></returns>
        public string EZ { get; set; }

        /// <summary>
        /// 切口等级
        /// </summary>
        /// <returns></returns>
        public string QKDJ { get; set; }

        /// <summary>
        /// 切口愈合类别
        /// </summary>
        /// <returns></returns>
        public string QKYHLB { get; set; }
        /// <summary>
        /// 切口愈合描述
        /// </summary>
        public string QKYHDJ { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public string MZFS { get; set; }

        /// <summary>
        /// 麻醉医师
        /// </summary>
        /// <returns></returns>
        public string MZYS { get; set; }
        public string OrganizeId { get; set; }
    }
}
