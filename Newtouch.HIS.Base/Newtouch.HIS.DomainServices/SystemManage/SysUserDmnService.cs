using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Security;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 用户相关DmnService
    /// </summary>
    public class SysUserDmnService : DmnServiceBase, ISysUserDmnService
    {
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserLogOnRepository _sysUserLogOnRepository;
        private readonly ISysRoleRepo _sysRoleRepository;
        private readonly ISysUserRoleRepo _sysUserRoleRepository;

        public SysUserDmnService(IBaseDatabaseFactory databaseFactory
            , ISysUserRepo sysUserRepo, ISysUserLogOnRepository sysUserLogOnRepository
            , ISysRoleRepo sysRoleRepository, ISysUserRoleRepo sysUserRoleRepository)
            : base(databaseFactory)
        {
            this._sysUserRepo = sysUserRepo;
            this._sysUserLogOnRepository = sysUserLogOnRepository;
            this._sysRoleRepository = sysRoleRepository;
            this._sysUserRoleRepository = sysUserRoleRepository;
        }

        /// <summary>
        /// 提交新建、更新 系统用户
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="roleList"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                SysUserEntity oldEntity = null;   //变更前Entity

                if (!string.IsNullOrEmpty(keyValue))
                {
                    if (_sysUserRepo.IQueryable().Any(p => p.TopOrganizeId == userEntity.TopOrganizeId && p.Id != keyValue
                        && p.Account == userEntity.Account))
                    {
                        throw new FailedException("登录账号不可重复");
                    }

                    oldEntity = _sysUserRepo.FindEntity(t => t.Id == keyValue);
                    _sysUserRepo.DetacheEntity(oldEntity);

                    userEntity.Modify(keyValue);

                    db.Update(userEntity);
                }
                else
                {
                    if (_sysUserRepo.IQueryable().Any(p => p.TopOrganizeId == userEntity.TopOrganizeId
       && p.Account == userEntity.Account))
                    {
                        throw new FailedException("登录账号不可重复");
                    }

                    userEntity.Create(true);

                    userLogOnEntity.UserId = userEntity.Id;
                    userLogOnEntity.UserSecretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
                    userLogOnEntity.UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.UserPassword, 32).ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
                    userLogOnEntity.Create(true);
                    db.Insert(userEntity);
                    db.Insert(userLogOnEntity);
                }

                db.Commit();

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, userEntity, SysUserEntity.GetTableName(), oldEntity.Id);
                }
            }
        }

        /// <summary>
        /// 获取系统（登录）用户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysUserVO> GetPagintionList(Pagination pagination, string OrganizeId, string keyword = null)
        {
            var sb = new StringBuilder();
            var pars = new List<SqlParameter>();
            sb.Append(@"select a.Id,a.Account,c.gh,c.Name,a.CreateTime,a.zt,c.[Description]
,org.Id OrganizeId,org.Name OrganizeName, dept.Name DepartmentName
,logon.Locked
,a.CreatorCode
,a.LastModifyTime
,a.LastModifierCode
,a.px
from Sys_User a
left join Sys_UserStaff b
on a.Id = b.UserId
left join Sys_Staff c
on b.StaffId = c.Id
left join Sys_Organize org
on c.OrganizeId = org.Id
left join Sys_Department dept
on c.DepartmentCode = dept.Code and c.OrganizeId = dept.OrganizeId
left join Sys_UserLogOn logon
on a.Id = logon.UserId
where 1 = 1
 ");
            if (OrganizeId == Constants.TopOrganizeId)  //如果是顶级组织机构
            {
                //显示所有用户
                sb.Append(" and a.TopOrganizeId = @topOrganizeId and a.Account != 'admin' ");
                pars.Add(new SqlParameter("@topOrganizeId", OrganizeId));
            }
            else
            {
                //仅显示该机构的用户
                sb.Append(" and c.OrganizeId = @OrganizeId and c.Id is not null ");
                pars.Add(new SqlParameter("@OrganizeId", OrganizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars.Add(new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%"));
                sb.Append(" and (a.Account like @searchkeyword or c.Name like @searchkeyword or c.gh like @searchkeyword or c.MobilePhone like @searchkeyword)");
            }

            return this.QueryWithPage<SysUserVO>(sb.ToString(), pagination, pars.ToArray());
        }

        /// <summary>
        /// 根据系统（登录）用户Id 获取 系统人员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysStaffEntity GetStaffByUserId(string userId)
        {
            var sql = @"select * from Sys_Staff a
left join Sys_User b
on a.Id = b.StaffId
where b.Id = @userId";

            return this.FirstOrDefault<SysStaffEntity>(sql, new SqlParameter[] {
                new SqlParameter("@userId", userId ?? "")
            });
        }

        /// <summary>
        /// 获取系统人员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysUserVO GetSysUserByUserId(string userId)
        {
            //            var sql = @"select a.*, b.Name, b.gh, b.DepartmentCode from Sys_User a
            //left join Sys_Staff b
            //on a.StaffId = b.Id
            //where a.Id = @userId";
            var sql = @"select a.* from Sys_User a
where a.Id = @userId";

            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@userId", userId)
            };

            return this.FirstOrDefault<SysUserVO>(sql, par);
        }

        /// <summary>
        /// 获取隶属于角色 的 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        public IList<RoleUserVO> GetPagintionUserListByRoleId(Pagination pagination, string gh, string name, string roleId, string topOrganizeId)
        {
            var sql = @"select distinct c.Id UserId, d.gh, d.Name UserName,d.Gender, d.DepartmentCode, d.OrganizeId
from Sys_UserRole a
left join Sys_Role b
on a.RoleId = b.Id
left join Sys_User c
on a.UserId = c.Id
left join Sys_Staff d
on c.StaffId = d.Id
where b.Id is not null and c.Id is not null and d.Id is not null
and a.RoleId = @roleId
and (isnull(@gh,'') = '' or d.gh like @searchgh)
and (isnull(@name,'') = '' or d.Name like @searchname)
and c.TopOrganizeId = @topOrganizeId and d.TopOrganizeId = @topOrganizeId";

            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@roleId", roleId ?? "")
                ,new SqlParameter("@topOrganizeId", topOrganizeId)
                ,new SqlParameter("@gh", gh ?? "")
                ,new SqlParameter("@searchgh", "%"+(gh??"")+"%")
                ,new SqlParameter("@name", name ?? "")
                ,new SqlParameter("@searchname", "%"+(name??"")+"%")
            };

            return this.QueryWithPage<RoleUserVO>(sql, pagination, par);
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        public void CreateUser(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Insert(userEntity);
                db.Insert(userLogOnEntity);
                db.Commit();
            }
        }

        /// <summary>
        /// 根据UserId获取关联的系统人员列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysStaffEntity> GetStaffListByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            var sql = @"select distinct b.* from Sys_UserStaff a WITH(NOLOCK)
left join Sys_Staff b WITH(NOLOCK)
on a.StaffId = b.Id
where a.UserId = @userId";
            return this.FindList<SysStaffEntity>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        /// <summary>
        /// 根据OrganizeId获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = @"select c.Id UserId, c.Account UserCode, a.Id StaffId, a.gh gh, a.DepartmentCode, a.Name  
from Sys_Staff a
left join Sys_UserStaff b
on b.StaffId = a.Id
left join Sys_User c
on b.UserId = c.Id
where 
a.zt = '1' and b.zt = '1' and c.zt = '1'
and c.TopOrganizeId = @topOrgId
and c.Id is not null
and a.OrganizeId = @orgId";
            return this.FindList<SysUserStaffVO>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@topOrgId", Constants.TopOrganizeId) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetLanguageTypeByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            var sql = "select LanguageType from Sys_User(nolock) where Id = @userId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        /// <summary>
        /// 根据员工Id获取所有岗位
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="StaffId"></param>
        /// <returns></returns>
        public IList<string> GetDutyListByStaffId(string orgId, string StaffId)
        {
            var sql = @"select c.Code from Sys_StaffDuty a
left join Sys_Staff b
on a.StaffId = b.Id
left join Sys_Duty c
on a.DutyId = c.Id
where b.Id = @StaffId and b.OrganizeId = @orgId
and a.zt = '1' and b.zt = '1' and c.zt = '1'";
            return this.FindList<string>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@StaffId", StaffId) });
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        public void RevisePassword(string userPassword, string userId)
        {
            var secretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
            var password = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), secretkey).ToLower(), 32).ToLower();

            //这里不直接操作表 就没法弄了，不能通过视图
            this.ExecuteSqlCommand("update sys_userlogon set UserSecretkey = @secretkey, UserPassword = @password where UserId = @userId and zt = '1'", new[] {
                new SqlParameter("@userId", userId)
                ,new SqlParameter("@secretkey", secretkey)
                ,new SqlParameter("@password", password)
            });
        }

        public string GetYbdmByGh(string rygh)
        {
            string sql = @"  select gjybdm from NewtouchHIS_Base..Sys_Staff where gh=@rygh";

            return FirstOrDefault<string>(sql, new[] { new SqlParameter("@rygh", rygh) });
        }
    }
}
