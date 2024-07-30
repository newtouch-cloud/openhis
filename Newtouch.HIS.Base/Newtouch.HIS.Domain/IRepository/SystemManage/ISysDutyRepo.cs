using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDutyRepo : IRepositoryBase<SysDutyEntity>
    {
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        IList<SysDutyEntity> GetValidList();

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysDutyEntity> GetList(string keyword);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysDutyEntity GetForm(string keyValue);

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysDutyEntity entity, string keyValue);

    }
}
