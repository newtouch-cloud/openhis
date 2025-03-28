using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    public class AccessIdentity
    {
        public string RegCode { get; set; }
        /// <summary>
        /// 注册名称
        /// </summary>
        public string RegName { get; set; }

        /// <summary>
        /// 权限等级 AuthorizedLevEnum
        /// </summary>
        public string AuthorizedLev { get; set; }
        /// <summary>
        /// 授权期限 AuthorizedPeriod
        /// </summary>
        public string AuthorizedPeriod { get; set; }
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string accesskey { get; set; }
        public List<AccessAuth> authlist { get; set; }
        //
        // 摘要:
        //     应用Id
        public string AppId { get; set; }
        //
        // 摘要:
        //     令牌类型（用户类型）
        public string TokenType { get; set; }
        //
        // 摘要:
        //     用户Id
        public string UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        //
        // 摘要:
        //     用户账户
        public string Account { get; set; }
        public string OrganizeId { get; set; }
    }
}
