using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// 注册应用信息
    /// </summary>
    public class AuthAppVO
    {
        /// <summary>
        /// Desc:授权ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AuthId { get; set; }

        /// <summary>
        /// Desc:应用通用Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AppId { get; set; }

        /// <summary>
        /// Desc:应用名称
        /// Default:
        /// Nullable:False
        public string? AppName { get; set; }

        /// <summary>
        /// Desc:域名地址
        /// Default:
        /// Nullable:False
        /// </summary>   
        public string? Domain { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? Host { get; set; }

        /// <summary>
        /// Desc:授权类型 AuthType
        /// Default:Web
        /// Nullable:True
        /// </summary>    
        public int? AuthType { get; set; }
    }

    
}
