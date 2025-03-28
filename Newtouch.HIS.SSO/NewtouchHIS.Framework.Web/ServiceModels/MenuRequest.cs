using NewtouchHIS.Base.Domain.ValueObjects;

namespace NewtouchHIS.Framework.Web.ServiceModels
{
    /// <summary>
    /// 角色授权菜单列表
    /// </summary>
    public class MenuAuthRequest
    {
        public List<string> RoleIdList { get; set; }
        public bool IsRoot { get; set; } = false;
        public bool IsAdministrator { get; set; } = false;
        public string? DB { get; set; }
        public string? UserId { get; set; }
        /// <summary>
        /// （菜单）业务系统AppId
        /// </summary>
        public string? MenuAppId { get; set; }
        /// <summary>
        /// 仅限有效菜单
        /// </summary>
        public bool ValidLimit { get; set; } = true;
        /// <summary>
        /// 是否显示已同步
        /// </summary>
        public bool? ShowSync { get; set; }=false;
        
    }
    /// <summary>
    /// 新增菜单
    /// </summary>
    public class MenuAddRequest : SysModuleVO
    {
        public string user { get; set; }
    }
}
