using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSViewer_MVC_Core.OutputModels
{
    /// <summary>
    /// 请求返回结果
    /// </summary>
    public class WqsjResponse
    {
        /// <summary>
        /// 错误提示内容，可直接显示给用户
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 信息类别 WqsjResponseCode
        /// </summary>
        public WqsjResponseCode Code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 返回数据集
        /// </summary>
        public object Data { get; set; }


        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="successFlag">成功标志</param>
        /// <param name="promptMsg">提示信息</param>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public static WqsjResponse ToResponse(bool successFlag, string promptMsg, object data = null)
        {
            if (!successFlag) return new WqsjResponse()
            {
                Code = WqsjResponseCode.ServiceError,
                Error = promptMsg,
                Data = data,
                Msg = promptMsg
            };
            else
                return new WqsjResponse()
                {
                    Code = WqsjResponseCode.Success,
                    Data = data,
                    Msg = promptMsg
                };
        }
    }
}
