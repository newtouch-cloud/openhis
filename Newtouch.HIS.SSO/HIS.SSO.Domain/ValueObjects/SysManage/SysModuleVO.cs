using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.SSO.Domain.SysManage
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class SysModuleVO
    {
        public string? Id { get; set; }
        /// <summary>
        /// 组织机构Id（可选，null时为所有组织机构公用）
        /// </summary>
        public string? OrganizeId { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        /// <returns></returns>
        public string? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string? Name { get; set; }
        /// <summary>
        /// EnName
        /// </summary>
        /// <returns></returns>
        public string? EnName { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public string? Code { get; set; }
        /// <summary>
        /// 应用Id，如CIS、Sett
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        public string? Icon { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public string? UrlAddress { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        /// <returns></returns>
        public string? Target { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        public string? Description { get; set; }
    }
}
