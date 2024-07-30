using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysItemsRepo : IRepositoryBase<SysItemsEntity>
    {
        /// <summary>
        /// 获取字典分类（包括无效的）
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetList(string keyword = null);

    }
}
