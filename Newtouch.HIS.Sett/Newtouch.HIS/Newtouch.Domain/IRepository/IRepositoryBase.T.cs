/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryBase<TEntity> : Newtouch.Infrastructure.EF.IRepositoryBase<TEntity>
        where TEntity : class, new()
    {

    }
}
