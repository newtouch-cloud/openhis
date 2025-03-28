using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineUnitRepo : IRepositoryBase<SysMedicineUnitEntity>
    {
        /// <summary>
        /// 根据关键字查询药品单位
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUnitEntity> GetValidList(string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUnitEntity> GetList(string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysMedicineUnitEntity entity, int? keyValue);
    }
}
