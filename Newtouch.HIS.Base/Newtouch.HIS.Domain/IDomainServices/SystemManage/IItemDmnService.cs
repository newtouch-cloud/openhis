using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 字典、字典项 DmnService
    /// </summary>
    public interface IItemDmnService
    {
        /// <summary>
        /// 获取有效字典项 列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId">医疗机构，null仅查共享的</param>
        /// <returns></returns>
        IList<SysItemsDetailEntity> GetValidListByItemCode(string code,string keyword, string orgId = null);

    }
}
