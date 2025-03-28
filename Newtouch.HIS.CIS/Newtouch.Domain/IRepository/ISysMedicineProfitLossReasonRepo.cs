using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
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
