using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class PharmacyInfoVEntity
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }
    }
}
