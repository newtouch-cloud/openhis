using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System;
using Newtouch.Core.Common.Exceptions;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserStaffRepo : RepositoryBase<SysUserStaffEntity>, ISysUserStaffRepo
    {
        public SysUserStaffRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffIds"></param>
        public void submitUserStaff(string userId, string staffIds)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var staffIdArr = (staffIds??"").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct();
                foreach (var staffId in staffIdArr)
                {
                    if (db.FindEntity<SysUserStaffEntity>(p => p.UserId != userId && p.StaffId == staffId) != null) {
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
