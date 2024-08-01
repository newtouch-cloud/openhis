using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.Core.Common;
using System.Text;
using Newtouch.Core.Common.Security;
using Newtouch.Core.Common.Utils;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.ValueObjects;
using Newtouch.Tools;
using Newtouch.Common;
using System;
using Newtouch.Core.Common.Exceptions;

namespace FrameworkBase.DmnService
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

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SysUserDmnService()
        {
            _loginFailedTimesLimit_Minutes = ConfigurationHelper.GetAppConfigIntValue("LoginFailedTimesLimit_Minutes", defaultValue: _loginFailedTimesLimit_Minutes);

            _loginFailedTimesLimit_Count = ConfigurationHelper.GetAppConfigIntValue("LoginFailedTimesLimit_Count", defaultValue: _loginFailedTimesLimit_Count);
        }

        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserLogOnRepo _sysUserLogOnRepo;
        private readonly ISysLogRepo _sysLogRepo;

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
        public SysUserEntity CheckLogin(string username, string password)
        {
            var userEntity = _sysUserRepo.FindEntity(t => t.Account == username && t.zt == "1");
            if (userEntity != null)
            {
                var userLogOnEntity = _sysUserLogOnRepo.FindEntity(p => p.UserId == userEntity.Id);
                if (userLogOnEntity.Locked.HasValue && userLogOnEntity.Locked.Value)
                {
                    //不提供 自动解锁 功能
                    throw new FailedException(string.Format("密码连续输错超过{0}次,账号已被锁定", _loginFailedTimesLimit_Count));
                }
                string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
                if (dbPassword == userLogOnEntity.UserPassword)
                {
                    var lastVisitTime = DateTime.Now;
                    int LogOnCount = (userLogOnEntity.LogOnCount).ToInt() + 1;
                    if (userLogOnEntity.LastVisitTime != null)
                    {
                        userLogOnEntity.PreviousVisitTime = userLogOnEntity.LastVisitTime.ToDate();
                    }
                    userLogOnEntity.LastVisitTime = lastVisitTime;
                    userLogOnEntity.LogOnCount = LogOnCount;
                    _sysUserLogOnRepo.Update(userLogOnEntity);
                    return userEntity;
                }
                else
                {
                    var startTime = DateTime.Now.AddMinutes(-_loginFailedTimesLimit_Minutes);
                    //取M分钟内的 最新的N-1条登录日志记录
                    var loginLogList = _sysLogRepo.IQueryable().Where(p => p.Type == "Login" && p.Account == username && p.CreateTime > startTime).OrderByDescending(p => p.CreateTime).Take(_loginFailedTimesLimit_Count - 1).Select(p => p.Result).ToList();
                    if (loginLogList.Count == _loginFailedTimesLimit_Count - 1 && !loginLogList.Any(p => p.HasValue && p.Value))  //如果是N-1条，且没有登录成功的记录
                    {
                        //到了最大错误次数了，锁定之
                        //更新状态至‘锁定’
                        //更新BASE.SYS_USERLOGON
                        userLogOnEntity.Locked = true;
                        userLogOnEntity.Modify();
                        _sysUserLogOnRepo.Update(userLogOnEntity);
                        throw new FailedException(string.Format("密码连续输错{0}次,账号被锁定", _loginFailedTimesLimit_Count));
                    }
                    else
                    {
                        throw new FailedException("密码不正确，请重新输入");
                    }
                }
            }
            else
            {
                throw new FailedException("账户不存在，请重新输入");
            }
        }

        /// <summary>
        /// 根据UserId获取关联的系统人员列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysStaffEntity> GetStaffListByUserId(string userId)
        {
            var sql = @"select b.* from Sys_UserStaff(nolock) a
left join Sys_Staff(nolock) b
on a.StaffId = b.Id and b.zt = '1'
where a.UserId = @userId and a.zt = '1'";
            return this.FindList<SysStaffEntity>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        /// <summary>
        /// 获取系统（登录）用户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysUserVO> GetPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append(@"select a.Id,a.Account,c.gh,c.Name
,a.CreateTime,a.zt,a.CreatorCode,a.LastModifyTime,a.LastModifierCode,a.px
,c.[Description] Description
, dept.Name DepartmentName
,logon.Locked
from Sys_User(nolock) a
left join Sys_UserStaff(nolock) b
on a.Id = b.UserId
left join Sys_Staff(nolock) c
on b.StaffId = c.Id
left join Sys_Department(nolock) dept
on c.DepartmentCode = dept.Code
left join Sys_UserLogOn(nolock) logon
on a.Id = logon.UserId
where 1 = 1");
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (c.Name like @keyword or a.Account like @keyword or c.gh like @keyword)");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }

            return this.QueryWithPage<SysUserVO>(sql.ToString(), pagination, pars.ToArray());
        }

        /// <summary>
        /// 提交新建、更新 系统用户
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                SysUserEntity oldEntity = null;   //变更前Entity

                if (!string.IsNullOrEmpty(keyValue))
                {
                    if (_sysUserRepo.IQueryable().Any(p => p.Id != keyValue
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
                    if (_sysUserRepo.IQueryable().Any(p => p.Account == userEntity.Account))
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

                    //绑定默认角色
                    var defaultRoleRoleCode = ConfigurationHelper.GetAppConfigValue("CreateUser_Default_RoleCode");
                    if (!string.IsNullOrWhiteSpace(defaultRoleRoleCode))
                    {
                        var defaultRoleEntity = db.IQueryable<SysRoleEntity>().Where(p => p.Code == defaultRoleRoleCode && p.zt == "1").FirstOrDefault();
                        if (defaultRoleEntity != null)
                        {
                            var userRoleEntity = new SysUserRoleEntity()
                            {
                                UserId = userEntity.Id,
                                RoleId = defaultRoleEntity.Id,
                            };
                            userRoleEntity.Create(true);
                            db.Insert(userRoleEntity);
                        }
                    }
                }

                db.Commit();

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, userEntity, SysUserEntity.GetTableName(), oldEntity.Id);
                }
            }
        }

        /// <summary>
        /// 获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <returns></returns>
        public IList<SysUserStaffVO> GetSatffVOList()
        {
            var sql = @"select c.Id UserId, c.Account UserCode, a.Id StaffId, a.gh gh, a.DepartmentCode, a.Name  
from Sys_Staff(nolock) a
left join Sys_UserStaff(nolock) b
on b.StaffId = a.Id
left join Sys_User(nolock) c
on b.UserId = c.Id
where 
a.zt = '1' and b.zt = '1' and c.zt = '1'
and c.Id is not null";
            return this.FindList<SysUserStaffVO>(sql);
        }

    }
}
