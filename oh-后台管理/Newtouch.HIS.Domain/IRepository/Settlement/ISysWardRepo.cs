using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.Settlement;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysWardRepo : IRepositoryBase<SysWardEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysWardEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword);

        IList<Getcz> Selectzccx(string organizeId, int xz);


        /// <summary>
        /// 更新病区信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysWardEntity entity, int? keyValue);

        /// <summary>
        /// 获取所有病区列表
        /// </summary>
        List<SysWardEntity> SelectWardList(string orgId);
    }
}
