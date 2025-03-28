using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.Domain.DTO.InterfaceSync
{
    public class ResponseBase
    {
        public ResponseResultCode code { get; set; }
        //
        // 摘要:
        //     网关返回码描述
        public string msg { get; set; }
        //
        // 摘要:
        //     业务返回码
        public string sub_code { get; set; }
        //
        // 摘要:
        //     业务返回码描述
        public string sub_msg { get; set; }
        //
        // 摘要:
        //     响应数据（ResponseResultCode.SUCCESS时的返回数据）
        public object data { get; set; }
        public int rowcount { get; set; }
    }
}
