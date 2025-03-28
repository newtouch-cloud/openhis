using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysApplicationRepo : IRepositoryBase<SysApplicationEntity>
    {
        /// <summary>
        /// 获取列表（包括无效的）
        /// </summary>
        /// <returns></returns>
        IList<SysApplicationEntity> GetList();

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        IList<SysApplicationEntity> GetValidList();

        /// <summary>
        /// 提交新建、更新 实体
        /// </summary>
        /// <returns></returns>
        void SubmitForm(SysApplicationEntity entity, string keyValue);

        /// <summary>
        /// 返回一个实体
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        SysApplicationEntity GetEntity(string appId);
    }
}
