using Autofac;
using Newtonsoft.Json;
using Newtouch.Core.Redis;
using Newtouch.HIS.API.Common.Models;
using Newtouch.Infrastructure;
using System;
using System.Web;

namespace Newtouch.HIS.Sett.API
{
    /// <summary>
    /// API Controll 基类
    /// </summary>
    public class ApiControllerBase<TControllerType> : FrameworkBase.API.ApiControllerBase<TControllerType>
    {
        public ApiControllerBase(IComponentContext com)
            : base(com)
        {
        }

        /// <summary>
        /// 用户身份
        /// </summary>
        public UserIdentity UserIdentity
        {
            get
            {
                return this.Identity as UserIdentity;
            }
        }
        /// <summary>
        /// 获取请求来源
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public string GetBookTerminal(string AppID)
        {
            if (AppID.Contains("SelfTerminal"))
            {
                return EnumMzghly.SelfTerminal.ToString();
            }
            return EnumMzghly.His.ToString();
        }

        
    }

}
