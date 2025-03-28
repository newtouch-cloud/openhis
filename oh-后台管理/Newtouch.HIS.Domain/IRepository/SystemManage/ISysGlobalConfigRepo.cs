using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysGlobalConfigRepo : IRepositoryBase<SysGlobalConfigEntity>
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysGlobalConfigEntity> GetList(string keyword);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysGlobalConfigEntity GetForm(string keyValue);

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysGlobalConfigEntity entity, string keyValue);
    }
}
