using Newtouch.Common;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

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