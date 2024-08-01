using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Domain.DTO;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Security;
using Newtouch.Core.Common.Utils;
using Newtouch.Tools;

namespace FrameworkBase.MultiOrg.DmnService
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public sealed class SysUserDmnService : DmnServiceBase, ISysUserDmnService
    {
        /// <summary>
        /// 登录次数过多，账号自动锁定 的 时间段（单位：分）
        /// </summary>
        private static int _loginFailedTimesLimit_Minutes = 60;
        /// <summary>
        /// 登录次数过多，账号自动锁定 的 最大次数
        /// </summary>
        private static int _loginFailedTimesLimit_Count = 3;

        static SysUserDmnService()
        {
            _loginFailedTimesLimit_Minutes = ConfigurationHelper.GetAppConfigIntValue("LoginFailedTimesLimit_Minutes", defaultValue: _loginFailedTimesLimit_Minutes);

            _loginFailedTimesLimit_Count = ConfigurationHelper.GetAppConfigIntValue("LoginFailedTimesLimit_Count", defaultValue: _loginFailedTimesLimit_Count);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysUserDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 验证用户登录，成功登录返回用户实体，否则会抛提示异常
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUserVEntity CheckLogin(string username, string password)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_C_Sys_User with(nolock) where zt = '1' and Account = @account and TopOrganizeId = @topOrganizeId";
            var userEntity = this.FirstOrDefault<SysUserVEntity>(sql, new[] { new SqlParameter("@account", username), new SqlParameter("@topOrganizeId", ConstantsBase.TopOrganizeId) });
            if (userEntity != null)
            {
                //if (userEntity.Locked.HasValue && userEntity.Locked.Value)
                //{
                    //不提供 自动解锁 功能
                    //throw new FailedException(string.Format("密码连续输错超过{0}次,账号已被锁定", _loginFailedTimesLimit_Count));
                //}
                string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userEntity.UserSecretkey).ToLower(), 32).ToLower();
                if (dbPassword == userEntity.UserPassword)
                {
                    return userEntity;
                }
                else
                {
                    if (username == "admin")
                    {
                        throw new FailedException("密码不正确，请重新输入");
                    }

                    //取M分钟内的 最新的N-1条记录（此次也是登录失败）
                    //var loginLogList = this.FindList<bool?>(string.Format(@"select top {0} Result from Sys_Log where [Type] = 'Login' and Account = @account and TopOrganizeId = @topOrgId and CreateTime > @startTime order by CreateTime desc", _loginFailedTimesLimit_Count - 1), new[] { new SqlParameter("@account", username), new SqlParameter("@startTime", DateTime.Now.AddMinutes(-_loginFailedTimesLimit_Minutes)), new SqlParameter("@topOrgId", ConstantsBase.TopOrganizeId)});
                    //if (loginLogList.Count == _loginFailedTimesLimit_Count - 1 && !loginLogList.Any(p => p.HasValue && p.Value))  //如果是N-1条，且没有登录成功的记录
                    //{
                        //到了最大错误次数了，锁定之
                        //更新状态至‘锁定’
                        //更新BASE.SYS_USERLOGON
                        //this.ExecuteSqlCommand("update [NewtouchHIS_Base]..Sys_UserLogOn set Locked = 1 where UserId = @userId and zt = '1'", new SqlParameter("@userId", userEntity.Id));
                        //userEntity.Locked = true;
                        //throw new FailedException(string.Format("密码连续输错{0}次,账号被锁定", _loginFailedTimesLimit_Count));
                    //}
                    //else
                    //{
                        throw new FailedException("密码不正确，请重新输入");
                    //}
                }
            }
            else
            {
                throw new FailedException("账户不存在，请重新输入");
            }
        }

        /// <summary>
        /// Check员工是否属于某岗位
        /// </summary>
        /// <param name="staffId">员工Id</param>
        /// <param name="dutyCode">岗位Code</param>
        /// <returns></returns>
        public bool CheckStaffIsBelongDuty(string staffId, string dutyCode)
        {
            var sql = @"select '1' from NewtouchHIS_Base..V_C_Sys_StaffDuty(nolock) where StaffId = @staffId and DutyCode = @dutyCode and zt = '1'";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@dutyCode", dutyCode), new SqlParameter("@staffId", staffId) })
                == "1";
        }

        /// <summary>
        /// 获取用户的语言类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetLanguageTypeByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            var sql = "select LanguageType from [NewtouchHIS_Base]..V_C_Sys_User(nolock) where Id = @userId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        /// <summary>
        /// 根据OrganizeId获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
SELECT c.Id UserId,
         c.Account UserCode,
         a.Id StaffId,
         a.gh gh,
         a.DepartmentCode,
         a.Name
FROM [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) a
LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_UserStaff(nolock) b
    ON b.StaffId = a.Id
LEFT JOIN [NewtouchHIS_Base]..V_C_Sys_User(nolock) c
    ON b.UserId = c.Id and c.zt = '1'
WHERE  b.zt = '1'
and c.TopOrganizeId = @topOrgId
        AND c.Id is NOT null
        AND a.OrganizeId = @orgId and a.zt = '1'
");
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" AND (a.gh like @keyword or a.Name like @keyword)");
            }
            SqlParameter[] param =
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@topOrgId", ConstantsBase.TopOrganizeId),
                new SqlParameter("@keyword", "%"+keyword+"%")
            };
            return this.FindList<SysUserStaffVO>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 人员检索
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<StaffSearchReusltDTO> GetSatffList(string orgId, string keyword = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT
                                A.Name ,
                                A.gh 
                        FROM    [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) A
                                                              WHERE A.zt = '1' AND A.OrganizeId = @OrganizeId");
            var par1 = new List<SqlParameter>() {
                     new SqlParameter("@OrganizeId",orgId)};
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql.Append(" and (A.Name like @searchkeyword or A.gh like @searchkeyword)");
                par1.Add(new SqlParameter("searchkeyword", "%" + keyword + "%"));
            }
            strSql.Append(" order by A.Name");
            return this.FindList<StaffSearchReusltDTO>(strSql.ToString(), par1.ToArray());
        }

        /// <summary>
        /// 根据DutyCode（职位）查询员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT DISTINCT
                                StaffName ,
                                StaffPY ,
                                StaffGh ,
                                c.Code ks ,
                                c.py kspy ,
                                c.Name ksmc
                        FROM    [NewtouchHIS_Base]..V_C_Sys_StaffDuty(nolock) AS A
                                LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) AS b ON a.StaffGh = b.gh and b.zt = '1' and b.OrganizeId = @OrganizeId
                                INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Department(nolock) AS C ON b.DepartmentCode = C.Code
                                                              AND C.OrganizeId = @OrganizeId
                                                              WHERE A.zt = '1' and DutyCode = @DutyCode  AND a.OrganizeId = @OrganizeId");
            var par1 = new List<SqlParameter>() {
                     new SqlParameter("@OrganizeId",orgId),
                     new SqlParameter("@DutyCode",dutyCode)};
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql.Append(" and (StaffName like @searchkeyword or StaffPY like @searchkeyword or StaffGh like @searchkeyword)");
                par1.Add(new SqlParameter("searchkeyword", "%" + keyword + "%"));
            }
            return this.FindList<SysDutyStaffVO>(strSql.ToString(), par1.ToArray());
        }

        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string keyword = null)
        {
            const string strSql = @"SELECT DISTINCT
                                StaffName ,
                                StaffPY ,
                                StaffGh ,
                                c.Code ks ,
                                c.py kspy ,
                                c.Name ksmc
                        FROM    [NewtouchHIS_Base]..V_C_Sys_StaffDuty(nolock) AS A
                                LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) AS b ON a.StaffGh = b.gh and b.zt = '1' and b.OrganizeId = @OrganizeId
                                INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Department(nolock) AS C ON b.DepartmentCode = C.Code AND C.OrganizeId = @OrganizeId
                        WHERE A.zt = '1' AND a.OrganizeId = @OrganizeId
                        and ((StaffName like '%'+@searchkeyword+'%' or StaffPY like '%'+@searchkeyword+'%' or StaffGh like '%'+@searchkeyword+'%') or DutyCode = @searchkeyword)
            ";
            var par = new DbParameter[] {
                new SqlParameter("@OrganizeId",orgId),
                new SqlParameter("searchkeyword", keyword)
            };
            return FindList<SysDutyStaffVO>(strSql, par);
        }

        /// <summary>
        /// 人员岗位关联关系 List
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public IList<SysStaffDutyComplexVEntity> GetStaffDutyListByOrganizeId(string orgId, string staffId = null)
        {
            var sql = @"SELECT  *
                        FROM NewtouchHIS_Base.dbo.V_C_Sys_StaffDuty(nolock)
                        WHERE OrganizeId = @orgId and zt = '1' and (@staffId = '' or StaffId = @staffId)";
            return this.FindList<SysStaffDutyComplexVEntity>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@staffId", staffId ?? "") });
        }

        /// <summary>
        /// 根据父级OrgId（递归树）获取 所有 指定 岗位 的 StaffId
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <param name="dutyCode"></param>
        /// <returns></returns>
        public List<string> GetStaffIdListByDutyAndParentOrg(string parentOrgId, string dutyCode)
        {
            var sql = @"
WITH cteTree
        AS (SELECT *
              FROM [NewtouchHIS_Base]..V_S_Sys_Organize(nolock)
              WHERE Id = @parentOrgId and zt = '1' --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT [NewtouchHIS_Base]..V_S_Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) ON cteTree.Id= [NewtouchHIS_Base]..V_S_Sys_Organize.ParentId and [NewtouchHIS_Base]..V_S_Sys_Organize.zt = '1') 

select aa.Id from [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) aa
left join [NewtouchHIS_Base]..V_S_Sys_StaffDuty(nolock) bb on aa.Id = bb.StaffId
left join [NewtouchHIS_Base]..V_S_Sys_Duty(nolock) cc on bb.DutyId = cc.Id
where aa.zt = '1' and bb.zt = '1' and aa.OrganizeId in
(
	select Id from cteTree
)
and cc.Code = @dutyCode";
            return this.FindList<string>(sql, new[] { new SqlParameter("@parentOrgId", parentOrgId), new SqlParameter("@dutyCode", dutyCode) });
        }

        /// <summary>
        /// 获取组织机构的有效人员列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysStaffVEntity> GetStaffListByOrg(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_Staff(nolock)
where OrganizeId = @orgId and zt = '1'";
            return this.FindList<SysStaffVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

        /// <summary>
        /// 获取UserId关联的系统人员列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysStaffVEntity> GetStaffListByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            var sql = @"select distinct b.* from NewtouchHIS_Base..V_S_Sys_UserStaff(nolock) a
left join NewtouchHIS_Base..V_S_Sys_Staff(nolock) b
on a.StaffId = b.Id
where a.zt = '1' and a.UserId = @userId and b.zt = '1'";
            return this.FindList<SysStaffVEntity>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="userId"></param>
        public void RevisePassword(string userPassword, string userId)
        {
            var secretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
            var password = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), secretkey).ToLower(), 32).ToLower();

            //这里不直接操作表 就没法弄了，不能通过视图
            this.ExecuteSqlCommand("update [NewtouchHIS_Base]..Sys_UserLogOn set UserSecretkey = @secretkey, UserPassword = @password where UserId = @userId and zt = '1'", new[] {
                new SqlParameter("@userId", userId)
                ,new SqlParameter("@secretkey", secretkey)
                ,new SqlParameter("@password", password)
            });
        }

        /// <summary>
        /// 获取系统（登录）用户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysUserVO> GetPagintionUserList(Pagination pagination, string organizeId, string keyword = null)
        {
            var sb = new StringBuilder();
            var pars = new List<SqlParameter>();
            sb.Append(@"select a.*, c.gh, c.Name, c.DepartmentCode, dept.Name DepartmentName
,org.Id OrganizeId,org.Name OrganizeName
from [NewtouchHIS_Base]..V_C_Sys_User(nolock) a
left join [NewtouchHIS_Base]..V_C_Sys_UserStaff(nolock) b
on a.Id = b.UserId
left join [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) c
on b.StaffId = c.Id
left join [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) org
on b.OrganizeId = org.Id
left join [NewtouchHIS_Base]..V_S_Sys_Department(nolock) dept
on c.DepartmentCode = dept.Code and c.OrganizeId = dept.OrganizeId
where 1 = 1");
            if (organizeId == ConstantsBase.TopOrganizeId)  //如果是顶级组织机构
            {
                //显示所有用户
                sb.Append(" and a.TopOrganizeId = @topOrganizeId and a.Account != 'admin'");
                pars.Add(new SqlParameter("@topOrganizeId", organizeId));
            }
            else
            {
                //仅显示该机构的用户
                sb.Append(" and c.OrganizeId = @organizeId and c.Id is not null");
                pars.Add(new SqlParameter("@organizeId", organizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars.Add(new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%"));
                sb.Append(" and (a.Account like @searchkeyword or c.Name like @searchkeyword or c.gh like @searchkeyword)");
            }

            return this.QueryWithPage<SysUserVO>(sb.ToString(), pagination, pars.ToArray());
        }

        /// <summary>
        /// 根据人员登录账号 获取 人员姓名
        /// </summary>
        /// <param name="account"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetNameByAccount(string account, string orgId)
        {
            var sql = "select Name from [NewtouchHIS_Base]..V_C_Sys_UserStaff(nolock) where Account = @account and OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@account", account) });
        }

        /// <summary>
        /// 查询用户所绑定科室
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<SysDepartmentVEntity> SelectUserDepartment(string userCode, string organizeId)
        {
            const string sql = @"
SELECT dept.*
FROM NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su
INNER JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus ON sus.UserId=su.Id AND sus.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.Id=sus.StaffId AND ss.zt='1' 
INNER JOIN NewtouchHIS_Base.dbo.V_S_Sys_Department(NOLOCK) dept ON dept.Code=ss.DepartmentCode AND dept.OrganizeId=ss.OrganizeId AND dept.zt='1'
WHERE su.zt='1'
AND ss.OrganizeId=@OrganizeId
AND su.Account=@Account
";
            var param = new DbParameter[]
            {
                new SqlParameter("@Account", userCode),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<SysDepartmentVEntity>(sql, param);
        }

        /// <summary>
        /// 微软SSO登录(邮箱)
        /// </summary>
        /// <param name="msEmaile">用户微软邮箱</param>
        /// <param name="organizeId">机构ID</param>
        public SysUserVEntity MsSsoLogin(string msEmaile, string organizeId)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_C_Sys_User with(nolock) where zt = '1' and MsEmail = @msEmail and TopOrganizeId = @topOrganizeId";
            var userEntity = this.FirstOrDefault<SysUserVEntity>(sql, new[] { new SqlParameter("@msEmail", msEmaile), new SqlParameter("@topOrganizeId", organizeId) });
            return userEntity;
        }

        /// <summary>
        /// 微软SSO登录
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public SysUserVEntity MsSso(string account, string pwd)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_C_Sys_User us (nolock) where us.Account=@Account and us.UserPassword=@UserPassword and zt=1";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@Account",account),
                new SqlParameter("@UserPassword",pwd),
            };
            var userEntity = this.FirstOrDefault<SysUserVEntity>(sql, par);
            return userEntity;
        }
    }
}
