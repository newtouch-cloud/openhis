using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineUsageRepo : IRepositoryBase<SysMedicineUsageEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
       void SubmitForm(SysMedicineUsageEntity entity, int? yfId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUsageEntity> GetPagintionList(Pagination pagination, string keyword = null);

        /// <summary>
        /// 获得所有列表
        /// </summary>
        List<SysMedicineUsageEntity> GetMedicineUsageList(string keyword = null);

        /// <summary>
        /// 修改form
        /// </summary>
        SysMedicineUsageEntity GetMedicineUsageEntity(int? yfId);
        
        /// <summary>
        /// 删除
        /// </summary>
        void DeleteForm(int yfId);
    }
}
