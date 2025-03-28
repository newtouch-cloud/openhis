using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeCategoryBaseRepo : IRepositoryBase<SysChargeCategoryBaseEntity>
    {
        /// <summary>
        /// 获取有效
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeCategoryBaseEntity> GetValidList(string orgId);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeCategoryBaseEntity> GetList(string orgId);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeCategoryBaseEntity GetForm(int keyValue);

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysChargeCategoryBaseEntity entity, int? keyValue);
    }
}
