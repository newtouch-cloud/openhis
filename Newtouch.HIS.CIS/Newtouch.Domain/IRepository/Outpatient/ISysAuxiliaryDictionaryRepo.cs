using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuxiliaryDictionaryRepo : IRepositoryBase<SysAuxiliaryDictionaryEntity>
    {
        /// <summary>
        /// 获取当前组织下的所有有效词典
        /// </summary>
        /// <returns></returns>
        IList<SysAuxiliaryDictionaryEntity> GetValidListByOrg(string orgId, bool withCache = false);

        /// <summary>
        /// 获取词典列表
        /// </summary>
        /// <returns></returns>
        List<SysAuxiliaryDictionaryEntity> GetListByOrg(string orgId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysAuxiliaryDictionaryEntity entity, string keyValue);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string keyValue);

    }
}
