using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    public interface ISysBodyPartsRepo : IRepositoryBase<SysBodyPartsEntity>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        List<SysBodyPartsEntity> GetListByOrg(string orgId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysBodyPartsEntity entity, string keyValue);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);
    }
}
