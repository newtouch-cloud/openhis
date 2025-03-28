using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 通用对象操作记录
    /// </summary>
    public interface ISysObjectActionRecordRepo : IRepositoryBase<SysObjectActionRecordEntity>
    {
        /// <summary>
        /// 新增操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(SysObjectActionRecordEntity entity);

        /// <summary>
        /// 根据键获取List
        /// </summary>
        /// <param name="objectKey"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysObjectActionRecordVO> GetListByKey(string objectKey, string orgId);

    }
}

