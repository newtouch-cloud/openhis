using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class MoneyUpperLimitReminderRepo : RepositoryBase<MoneyUpperLimitReminderEntity>, IMoneyUpperLimitReminderRepo
    {
        public MoneyUpperLimitReminderRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sxtxId"></param>
        public void SubmitForm(MoneyUpperLimitReminderEntity entity, string userCode, string sxtxId)
        {
            if (!string.IsNullOrEmpty(sxtxId))
            {
                entity.Modify();
                this.Update(entity);
            }
            else
            {
                var oldentitylist = this.IQueryable().Where(a => a.zt == "1" && a.OrganizeId == entity.OrganizeId && a.ks == entity.ks && a.ys == entity.ys && a.reminderType == entity.reminderType).ToList();
                if (oldentitylist != null && oldentitylist.Count > 0)
                {
                    throw new FailedCodeException("NOTICE_THE_SAME_TYPE_OF_REMINDER_THE_DEPARTMENT_UNDER_THE_DOCTOR_CAN_NOT_BE_REPEATED_MAINTENANCE");
                }
                entity.sxtxId = Guid.NewGuid().ToString();
                entity.CreatorCode = userCode;
                entity.CreateTime = DateTime.Now;
                this.Insert(entity);
            }
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ybbabId"></param>
        /// <param name="orgId"></param>
        public void DeleteForm(string orgId, string sxtxId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return;
            }
            this.Delete(a => a.sxtxId == sxtxId && a.OrganizeId == orgId);
        }
        

    }
}
