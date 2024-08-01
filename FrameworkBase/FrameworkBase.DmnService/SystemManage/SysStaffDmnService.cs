using System.Collections.Generic;
using System.Linq;
using Newtouch.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Common.Security;
using Newtouch.Tools;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Infrastructure;
using FrameworkBase.Domain.ValueObjects;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using Newtouch.Core.Common.Exceptions;

namespace FrameworkBase.DmnService
{
    /// <summary>
    /// 人员相关
    /// </summary>
    public sealed class SysStaffDmnService : DmnServiceBase, ISysStaffDmnService
    {
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysUserRepo _sysUserRepo;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysStaffDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 添加、更新 系统人员
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <param name="asLoginUser">作为登录用户（创建系统用户）</param>
        public void SubmitForm(SysStaffEntity entity, string keyValue, bool asLoginUser)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (_sysStaffRepo.IQueryable().Any(p => p.Id != keyValue
                    && p.gh == entity.gh))  //同一医院内不能有gh一样的
                {
                    throw new FailedException("工号不可重复");
                }

                SysStaffEntity oldEntity = null;   //变更前Entity
                oldEntity = _sysStaffRepo.FindEntity(keyValue);
                _sysStaffRepo.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                _sysStaffRepo.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysStaffEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                //新增用户 开事物
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    if (db.IQueryable<SysStaffEntity>().Any(p => p.gh == entity.gh))
                    //同一医院内不能有gh一样的
                    {
                        throw new FailedException("工号不可重复");
                    }
                    entity.Create(true);
                    db.Insert(entity);

                    if (asLoginUser)
                    {
                        if (!string.IsNullOrWhiteSpace(entity.gh))
                        {
                            var accont = entity.gh;

                            //创建系统账户

                            if (!_sysUserRepo.IQueryable().Any(p => p.Account == accont))
                            {
                                var userEntity = new SysUserEntity()    //
                                {
                                    Account = accont,
                                };
                                userEntity.Create(true);
                                var userLogOnEntity = new SysUserLogOnEntity()  //用户登录
                                {
                                    UserPassword = ConstantsBase.LogonDefaultPassword,
                                };
                                userLogOnEntity.UserId = userEntity.Id;
                                userLogOnEntity.UserSecretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
                                userLogOnEntity.UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.UserPassword, 32).ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
                                userLogOnEntity.Create(true);
                                var userStaffEntity = new SysUserStaffEntity()  //人员用户 关联关系
                                {
                                    UserId = userEntity.Id,
                                    StaffId = entity.Id
                                };
                                userStaffEntity.Create(true);
                                db.Insert(userEntity);
                                db.Insert(userLogOnEntity);
                                db.Insert(userStaffEntity);

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
                        }
                    }

                    db.Commit();
                }
            }
        }

        /// <summary>
        /// 更新人员岗位
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="dutyIdList"></param>
        public void UpdateStaffDuty(string staffId, string[] dutyIdList)
        {
            //岗位list
            var dutyLists = new List<SysStaffDutyEntity>();
            foreach (var item in dutyIdList.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
            {
                var entity = new SysStaffDutyEntity();
                entity.Create(true);
                entity.StaffId = staffId;
                entity.DutyId = item;
                entity.zt = "1";
                dutyLists.Add(entity);
            }

            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var oldDutyList = db.IQueryable<SysStaffDutyEntity>().Where(p => p.StaffId == staffId).ToList();
                for (int i = 0; i < dutyLists.Count; i++)
                {
                    if (oldDutyList.Any(p => p.DutyId == dutyLists[i].DutyId))
                    {
                        oldDutyList.Remove(oldDutyList.Where(p => p.DutyId == dutyLists[i].DutyId).First());
                        continue;
                    }
                    db.Insert(dutyLists[i]);
                }
                foreach (var item in oldDutyList)
                {
                    db.Delete(item);
                }
                db.Commit();
            }

        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysStaffVO> GetPaginationStaffList(Pagination pagination, string keyword)
        {
            var sql = new StringBuilder();
            sql.Append(@"select a.*, b.Name DepartmentName from Sys_Staff(nolock) a 
left join Sys_Department(nolock) b
on a.DepartmentCode = b.Code
where 1 = 1");
            List<SqlParameter> pars = null;
            pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (a.Name like @keyword or a.gh like @keyword)");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<SysStaffVO>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

    }
}
