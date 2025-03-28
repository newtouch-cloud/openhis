using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IMoneyUpperLimitReminderDmnService
    {
        /// <summary>
        /// 金额上限查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="reminderType"></param>
        /// <returns></returns>
        IList<MoneyUpperLimitReminderSelectVO> GetAllList(Pagination pagination, string orgId, string keyword, string reminderType, string sxtxId = null);

        /// <summary>
        /// 获取记账人员 记账信息（本月）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="deptCode"></param>
        /// <param name="jfjesx"></param>
        /// <param name="yjfzje"></param>
        /// <returns></returns>
        void GetStaffjfjeInfo(string orgId, string gh, string deptCode, out decimal jfjesx, out decimal yjfzje);

    }
}
