using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysItemsRepository : IRepositoryBase<SysItemsEntity>
    {
        /// <summary>
        /// 获取字典分类（包括无效的）
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetList();

        /// <summary>
        /// 获取有效字典分类
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetValidList();

        /// <summary>
        /// 提交新建、更新 实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysItemsEntity entity, string keyValue);

    }
}
