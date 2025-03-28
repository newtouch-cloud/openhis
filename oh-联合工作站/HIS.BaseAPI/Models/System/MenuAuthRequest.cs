using System.ComponentModel.DataAnnotations;

namespace HIS.BaseAPI.Models.System
{
    /// <summary>
    /// 角色授权菜单列表
    /// </summary>
    public class MenuAuthRequest
    {
        public List<string>? RoleIdList { get; set; }
        public bool IsRoot { get; set; } = false;
        public bool IsAdministrator { get; set; } = false;
        public string? DB { get; set; }
        public string? UserId { get; set; }
        /// <summary>
        /// 仅限有效菜单
        /// </summary>
        public bool ValidLimit { get; set; } = true;
        /// <summary>
        /// 应用Id
        /// </summary>
        public string? MenuAppId { get; set; }
    }
}
