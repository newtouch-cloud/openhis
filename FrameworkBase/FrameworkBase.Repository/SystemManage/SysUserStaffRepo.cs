using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Infrastructure;
using Newtouch.Core.Common.Exceptions;

namespace FrameworkBase.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:20
    /// 描 述：用户人员对照表
    /// </summary>
    public sealed class SysUserStaffRepo : RepositoryBase<SysUserStaffEntity>, ISysUserStaffRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysUserStaffRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取关联人员Id列表 根据UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<string> GetStaffIdListByUserId(string userId)
        {
            var sql = "select StaffId from Sys_UserStaff(nolock) where UserId = @UserId and zt = '1'";
            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@UserId",userId)
            });
        }

        /// <summary>
        /// 提交 用户关联人员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffIds"></param>
        public void SubmitUserStaff(string userId, string staffIds)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var staffIdArr = (staffIds ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct();
                foreach (var staffId in staffIdArr)
                {
                    if (db.FindEntity<SysUserStaffEntity>(p => p.UserId != userId && p.StaffId == staffId) != null)
                    {
                        throw new FailedException("系统人员存在重复绑定");
                    }
                }
                //先 del
                db.Delete<SysUserStaffEntity>(p => p.UserId == userId);
                //再添加
                foreach (var staffId in staffIdArr)
                {
                    var userStaffentity = new SysUserStaffEntity();
                    userStaffentity.Id = Guid.NewGuid().ToString();
                    userStaffentity.UserId = userId;
                    userStaffentity.StaffId = staffId;
                    userStaffentity.Create();
                    db.Insert(userStaffentity);
                }
                db.Commit();
            }
        }

    }
}