using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService
{
    /// <summary>
    /// 系统角色及授权
    /// 各系统通用功能，DBContext选择为主库,建议引用
    /// </summary>
    public class SysRoleDmnService : BaseDmnService<SysRoleEntity>, ISysRoleDmnService
    {
        /// <summary>
        /// 应用需配置主库
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");
        public SysRoleDmnService()
        {
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
        }

        public async Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId)
        {
            var baseUserRole = await GetByWhere<SysUserRoleEntity>(p => p.UserId == userId && p.zt == "1");
            var baseRoleIds = baseUserRole.Select(p => p.RoleId);
            return await GetByWhere(p => baseRoleIds.Contains(p.Id) && p.OrganizeId == orgId && p.zt == "1");

        }
        public async Task<bool> IsHospAdmin(string userId, string orgId)
        {
            var baseUserRole = await GetByWhere<SysUserRoleEntity>(p => p.UserId == userId && p.zt == "1");
            var baseRoleIds = baseUserRole.Select(p => p.RoleId);
            var result = await GetByWhere(p => baseRoleIds.Contains(p.Id) && p.OrganizeId == orgId && p.zt == "1");
            if (result.Any(p => p.Code == "HospAdministrator"))
            {
                return true;
            }
            return false;
        }
        public async Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId, string db)
        {
            var userRole = await GetByWhere<SysUserRoleEntity>(db, p => p.UserId == userId && p.zt == "1");
            var roleIds = userRole.Select(p => p.RoleId);
            return await GetByWhere<SysRoleEntity>(db, p => roleIds.Contains(p.Id) && p.OrganizeId == orgId && p.zt == "1");
        }
        /// <summary>
        /// 删除系统角色
        /// </summary>
        /// <param name="keyValue"></param>
        public async Task DeleteForm(string keyValue)
        {
            var result = await GetByWhere<SysRoleEntity>(DBEnum.UnionDb.ToString(), p => p.zt == "1" && p.Id == keyValue);
            if (result == null)
            {
                throw new FailedException("删除失败，查询无信息");
            }
            SysRoleEntity sysRoleEntity = result[0];
            if (sysRoleEntity.Code == "Administrator")
            {
                throw new FailedException("编码Administrator为系统保留，不能删除");
            }
            await Delete<SysRoleEntity>(sysRoleEntity);
            var sql = "delete from [Newtouch_Union]..Sys_RoleAuthorize where RoleId=@RoleId";

            await ExecuteCommandSql(sql, new SugarParameter[] {
                new SugarParameter("@RoleId",sysRoleEntity.Id) });
        }

        /// <summary>
        /// 提交新建、更新 实体
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        /// <param name="isAdministrator"></param>
        public async Task SubmitForm(SysRoleEntity roleEntity, List<Domain.Entity.SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue, string UserCode, string orgId)
        {
            if (roleEntity.Code == "Administrator")
            {
                if (string.IsNullOrWhiteSpace(keyValue)
                    || (!string.IsNullOrEmpty(keyValue) && roleEntity.OrganizeId != "*"))
                {
                    throw new FailedException("编码Administrator为系统保留");
                }
            }
            var result = await GetByWhere<SysRoleEntity>(DBEnum.UnionDb.ToString(), p => p.zt == "1" && p.Id == keyValue);
            if (result != null && result.Count > 0 && keyValue != null && keyValue != "")
            {
                if (result[0] != null)
                {
                    SysRoleEntity sysRoleEntity = result[0];
                    sysRoleEntity.Name = roleEntity.Name;
                    sysRoleEntity.Code = roleEntity.Code;
                    sysRoleEntity.px = roleEntity.px;
                    sysRoleEntity.zt = "1";
                    sysRoleEntity.Description = roleEntity.Description;
                    sysRoleEntity.ModifiedEntity(orgId, UserCode, false);
                    await Update<SysRoleEntity>(sysRoleEntity);
                }
            }
            else
            {
                await Add<SysRoleEntity>(roleEntity);
            }
            //var RoleAuthorize = await GetFirstOrDefaultWithAttr<SysRoleAuthorizeEntity>(p => p.zt == "1" && p.RoleId == roleEntity.Id);
            var sql = "delete from [Newtouch_Union]..Sys_RoleAuthorize where RoleId=@RoleId";

            await ExecuteCommandSql(sql, new SugarParameter[] {
                new SugarParameter("@RoleId",keyValue) });
            for (int i = 0; i < roleAuthorizeEntitys.Count; i++)
            {
                await Add(roleAuthorizeEntitys[i]);
            }
        }
        public async Task<List<SysRoleEntity>> GetRoleList(string orgId)
        {
            return await GetByWhere(p => p.OrganizeId == orgId && p.zt == "1");
        }
        /// <summary>
        /// 组织机构 角色列表 带 分页
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<SysRoleEntity>> GetPagintionList(string orgId, string? keyword = null)
        {
            var data = await GetByWhere<SysRoleEntity>(p => p.zt == "1" && p.OrganizeId == orgId);
            return data;
        }

        public async Task<SysRoleEntity> GetForm(string keyword)
        {
            var data = await GetByWhere<SysRoleEntity>(p => p.Id == keyword);
            SysRoleEntity sysRoleEntity = data[0];
            return sysRoleEntity;
        }
    }
}
