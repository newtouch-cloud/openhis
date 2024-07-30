using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InterfaceSync
{
    public class ResponseAPIBase
    {
        /// <summary>
        /// 业务返回码
        /// </summary>
        public ResponseResultCodeAPI code { get; set; }

        /// <summary>
        /// 业务返回码描述
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 响应数据（ResponseResultCode.SUCCESS时的返回数据）
        /// </summary>
        public object data { get; set; }
    }
    /// <summary>
    /// 过渡类 仅用于调用his内部Api
    /// </summary>
    public class ResponseBaseOld : ResponseBase
    {

        public string sub_code { get; set; }
        public string sub_msg { get; set; }
    }


    public enum ResponseResultCodeAPI
    {
        /// <summary>
        /// 默认值
        /// </summary>
        Default = -1,
        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS = 10000,

        /// <summary>
        /// 服务器内部错误（程序发生异常）
        /// </summary>
        ERROR = 20000,

        /// <summary>
        /// 业务处理失败:	具体失败原因参见接口返回的错误码    
        /// </summary>
        FAIL = 40004
    }
}
