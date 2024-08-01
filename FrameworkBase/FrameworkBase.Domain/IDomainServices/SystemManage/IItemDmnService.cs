using FrameworkBase.Domain.Entity;
using System.Collections.Generic;

namespace FrameworkBase.Domain.IDomainServices
{
    /// <summary>
    /// 字典相关
    /// </summary>
    public interface IItemDmnService
    {
        /// <summary>
        /// 根据分类Code获取字典项
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysItemsDataEntity> GetValidListByItemCode(string code);

    }
}
