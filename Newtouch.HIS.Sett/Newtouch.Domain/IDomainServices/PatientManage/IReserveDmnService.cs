using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 预交金
    /// </summary>
    public interface IReserveDmnService
    {

        /// <summary>
        /// 保存账户事务
        /// </summary>
        /// <param name="vo"></param>
        void PatAccDB(PatAccDataVO vo, SysAccountEntity zh, FinanceReceiptEntity cwsj, string type);

        /// <summary>
        /// 获取住院管理》账户管理》获取账户收支记录信息
        /// </summary>
        /// <returns></returns>
        List<PatAccPayVO> GetAccPayInfo(int zh, string orgId, string zhxz);
    }
}
