using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService
{
    public class UserRoleAuthDmnService : BaseDmnService<SysRoleAuthorizeEntity>, IUserRoleAuthDmnService
    {
        /// <summary>
        /// 应用需配置主库
        /// 增删改建议引用
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");
        public UserRoleAuthDmnService()
        {
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
        }
        /// <summary>
        /// 获取角色授权列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<SysRoleAuthorizeEntity>> GetValidList(string roleId)
        {
            var data = await GetByWhere<SysRoleAuthorizeEntity>(p => p.zt == "1" && p.RoleId == roleId);
            return data;
        }
        /// <summary>
        /// 获取角色 关联 用户（org）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<List<FirstSecond>> GetCurUserIdListByRoleId(string roleId)
        {
            var data = await GetJoinList<SysUserRoleEntity, SysRoleEntity, FirstSecond>(
                (a, b) => new JoinQueryInfos(JoinType.Inner, a.RoleId == b.Id), (a, b) => new FirstSecond { First = a.UserId, Second = b.OrganizeId },
                true, (a, b) => b.Id == roleId && a.zt == "1");
            return data;
        }
        /// <summary>
        /// 保存 角色 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        public async void submitRoleUser(string roleId, List<FirstSecond> userList, string usercode)
        {
            var roleEntity = await GetByWhere<SysRoleEntity>(p => p.Id == roleId);
            if (roleEntity == null)
            {
                throw new FailedException("错误的提交，角色查询有误");
            }
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    if (user.Second != roleEntity[0].OrganizeId)
                    {
                        throw new FailedException("错误的提交，用户与角色组织机构不对应");
                    }
                }
            }
            var SysUserRole = await GetByWhere<SysUserRoleEntity>(p => p.RoleId == roleId);
            if (SysUserRole != null && SysUserRole.Count > 0)
            {
                foreach (var item in SysUserRole)
                {
                    //先 del
                    await Delete<SysUserRoleEntity>(item);
                }
            }
            //再添加
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    var userRoleEntity = new SysUserRoleEntity();
                    userRoleEntity.Id = Guid.NewGuid().ToString();
                    userRoleEntity.RoleId = roleId;
                    userRoleEntity.UserId = user.First;
                    userRoleEntity.NewEntity(usercode);
                    await Add(userRoleEntity);
                }
            }
        }

        public async Task<bool> UpdateUserRole(string userId, string orgId, List<string> roleIdList, string usercode)
        {
            var orgRoleIdList = (await GetByWhere<SysRoleEntity>(p => p.OrganizeId == orgId && p.zt == "1")).Select(p => p.Id);
            var userRoleEntities = roleIdList.Select(a => new SysUserRoleEntity
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                RoleId = a,
                CreateTime = DateTime.Now,
                CreatorCode = usercode,
                zt = "1"
            }).Where(p => orgRoleIdList.Contains(p.RoleId)).ToList();
            try
            {
                var oldRoleList = await GetByWhere<SysUserRoleEntity>(p => p.UserId == userId && orgRoleIdList.Contains(p.RoleId));

                await dbTran.BeginTranAsync();
                oldRoleList.ForEach(old =>
                {
                    old.ModifiedEntity(usercode, true);
                });
                var oldDelete = dbTran.GetConnectionScope(_mainDB).Updateable(oldRoleList).ExecuteCommand();
                if (oldDelete <= 0)
                {
                    throw new FailedException("原角色关系解除失败");
                }
                //新增角色关系
                var newRoles = dbTran.GetConnectionScope(_mainDB).Insertable<SysUserRoleEntity>(userRoleEntities).ExecuteCommand();
                if (newRoles <= 0)
                {
                    throw new FailedException("角色关系绑定失败");
                }
                await dbTran.CommitTranAsync();
                return true;
            }
            catch (Exception ex)
            {
                await dbTran.RollbackTranAsync();
                throw new FailedException($"角色关系绑定失败：{ex.Message}");

            }

        }
    }
}
