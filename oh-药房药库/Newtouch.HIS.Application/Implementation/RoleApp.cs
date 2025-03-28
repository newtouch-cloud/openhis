using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 角色App
    /// </summary>
    public class RoleApp : AppBase, IRoleApp
    {
        private readonly ISysRoleRepo _roleRepository;
        private readonly ISysModuleRepo _moduleRepository;
        private readonly ISysModuleButtonRepo _moduleButtonRepository;
        private readonly ISysRoleDmnService _sysRoleDmnService;


        /// <summary>
        /// 组织机构 角色列表 带 分页
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysRoleEntity> GetPagintionList(string topOrganizeId, Pagination pagination, string keyword = null)
        {
            var expression = ExtLinq.True<SysRoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Code.Contains(keyword));
            }
            expression = expression.And(t => t.TopOrganizeId == topOrganizeId);
            expression = expression.And(t => t.zt == "1");
            return _roleRepository.FindList(expression, pagination).OrderBy(p => p.px).ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysRoleEntity GetForm(string keyValue)
        {
            return _roleRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            _sysRoleDmnService.DeleteForm(keyValue);
        }

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysRoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
            }
            else
            {
                roleEntity.Create(true);
            }
            var moduledata = _moduleRepository.IQueryable().ToList();
            var buttondata = _moduleButtonRepository.IQueryable().ToList();
            List<SysRoleAuthorizeEntity> roleAuthorizeEntityList = new List<SysRoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                SysRoleAuthorizeEntity roleAuthorizeEntity = new SysRoleAuthorizeEntity();
                roleAuthorizeEntity.Create(true);
                roleAuthorizeEntity.RoleId = roleEntity.Id;
                roleAuthorizeEntity.ItemId = itemId;
                roleAuthorizeEntity.zt = "1";   //始终1 有效
                if (moduledata.Find(t => t.Id == itemId) != null)
                {
                    roleAuthorizeEntity.ItemType = 1;
                }
                else if (buttondata.Find(t => t.Id == itemId) != null)
                {
                    roleAuthorizeEntity.ItemType = 2;
                }
                roleAuthorizeEntityList.Add(roleAuthorizeEntity);
            }
            _sysRoleDmnService.SubmitForm(roleEntity, roleAuthorizeEntityList, keyValue);
        }

    }
}
