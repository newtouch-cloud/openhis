using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineClassificationRepo : IRepositoryBase<SysMedicineClassificationEntity>
    {
        /// <summary>
        /// 根据关键字查询药品分类
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysMedicineClassificationEntity> GetValidList(string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineClassificationEntity> GetPagintionList(Pagination pagination, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysMedicineClassificationEntity entity, int? keyValue);

    }
}
