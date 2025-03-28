using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_ypsyyy
    /// </summary>
    public interface ISysMedicineProfitLossReasonRepo : IRepositoryBase<SysMedicineProfitLossReasonEntity>
    {
        /// <summary>
        /// 根据损益类型查询损益原因list
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx);
        IList<SysMedicineProfitLossReasonEntity> GetPagintionList(Pagination pagination, string keyword);
        void SubmitForm(SysMedicineProfitLossReasonEntity SyyyDTO, string keyValue);
        void DeleteForm(string keyValue);
    }
}
