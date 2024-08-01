using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 中医证候
    /// </summary>
    public interface ISysTCMSyndromeRepo : IRepositoryBase<SysTCMSyndromeVEntity>
    {
        /// <summary>
        /// 根据关键字模糊查找（有效的）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysTCMSyndromeVEntity> GetList(string orgId, string keyword);

        /// <summary>
        /// 根据编码查找实体（可能无效）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        SysTCMSyndromeVEntity GetEntityByCode(string orgId, string code);
    }
}
