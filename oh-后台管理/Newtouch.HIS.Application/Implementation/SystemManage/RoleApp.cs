using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Core.Common;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Application.Implementation.SystemManage
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

        public RoleApp(ISysRoleRepo roleRepository, ISysModuleRepo moduleRepository,
            ISysModuleButtonRepo moduleButtonRepository, ISysRoleDmnService sysRoleDmnService)
        {
            this._roleRepository = roleRepository;
            this._moduleRepository = moduleRepository;
            this._moduleButtonRepository = moduleButtonRepository;
            this._sysRoleDmnService = sysRoleDmnService;
        }

        /// <summary>
        /// 组织机构 角色列表 带 分页
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysRoleEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null)
        {
            var expression = ExtLinq.True<SysRoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Code.Contains(keyword));
            }
            //||IsAdministrator 修改自己所在角色的权限 是有点不合适
            if (orgId == Constants.TopOrganizeId && (this.UserIdentity.IsRoot || this.UserIdentity.IsAdministrator))
            {
                expression = expression.And(t => t.OrganizeId == orgId);
                expression = expression.Or(t => t.OrganizeId == "*");  //带上系统管理员Administrator
            }
            else
            {
                expression = expression.And(t => t.OrganizeId == orgId);
            }
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
            //主要是为了唯一主键
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
            }
            else
            {
                roleEntity.Create(true);
            }

            List<SysRoleAuthorizeEntity> roleAuthorizeEntityList = new List<SysRoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                SysRoleAuthorizeEntity roleAuthorizeEntity = new SysRoleAuthorizeEntity();
                roleAuthorizeEntity.Create(true);
                roleAuthorizeEntity.RoleId = roleEntity.Id;
                roleAuthorizeEntity.ItemId = itemId;
                roleAuthorizeEntity.zt = "1";   //始终1 有效
                roleAuthorizeEntityList.Add(roleAuthorizeEntity);
            }
            _sysRoleDmnService.SubmitForm(roleEntity, roleAuthorizeEntityList, keyValue);
        }

    }
}
