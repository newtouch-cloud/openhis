using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatientAccountRevenueAndExpenseRepo : IRepositoryBase<SysPatientAccountRevenueAndExpenseEntity>
    {
        /// <summary>
        /// 根据住院号、预交金账户 获取 收支记录 列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<SysPatientAccountRevenueAndExpenseEntity> GetListByZyhAndYjjzh(string zyh, int zh, string orgId);

        /// <summary>
        /// 根据账户 获取账户最后一条记录（为获取账户余额）
        /// add by HLF
        /// </summary>
        /// <param name="zh"></param>
        /// <returns></returns>
        SysPatientAccountRevenueAndExpenseEntity GetLastJL(int zh, string orgId);

        /// <summary>
        /// 添加账户收支记录
        /// </summary>
        /// <param name="sysPatPayInfoEntity"></param>
        void AddPayInfo(SysPatientAccountRevenueAndExpenseEntity sysPatPayInfoEntity, string orgId, string curUserCode, string curUserName);

    }
}
