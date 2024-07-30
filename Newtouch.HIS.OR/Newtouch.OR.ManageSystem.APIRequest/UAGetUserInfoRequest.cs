using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.API.Common;

namespace Newtouch.OR.ManageSystem.APIRequest
{
    /// <summary>
    /// 以访问令牌换取用户身份信息
    /// </summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string access_token { get; set; }
    }
}
