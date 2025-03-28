using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.SSO.Domain.Model.SysManage
{
    public class ShorcutMenuModel
    {
        public string MenuName { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 系统appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 快捷菜单url
        /// </summary>
        public string returnUrl { get; set; }

        public string MenuUrl { get; set; }
        /// <summary>
        /// 令牌token
        /// </summary>
        public string access_token { get; set; }

        public string OrganizeId { get; set; }
        public string CreatorCode { get; set; }
    }
}
