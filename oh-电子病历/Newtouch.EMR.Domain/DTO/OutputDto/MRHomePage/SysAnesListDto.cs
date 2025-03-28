using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRHomePage
{
    public class SysAnesListDto
    {
        public string OrganizeId { get; set; }
        /// <summary>
        /// 麻醉编码
        /// </summary>
        public string AnesCode { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string AnesName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        public string Aneszjm { get; set; }
    }
}
