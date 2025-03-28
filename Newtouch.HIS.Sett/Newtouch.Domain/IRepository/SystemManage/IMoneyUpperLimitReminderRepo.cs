using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMoneyUpperLimitReminderRepo : IRepositoryBase<MoneyUpperLimitReminderEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sxtxId"></param>
        void SubmitForm(MoneyUpperLimitReminderEntity entity, string userCode, string sxtxId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ybbabId"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string orgId, string sxtxId);
    }
}
