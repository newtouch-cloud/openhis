using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    public class ResponseBase
    {
        /// <summary>
        /// 接口返回代码
        /// </summary>
        public string code { get; set; }
        public string message { get; set; }
        /// <summary>
        /// 接口响应数据
        /// </summary>
        public string data { get; set; }
    }
}
