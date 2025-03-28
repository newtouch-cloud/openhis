using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.Settlement;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeCategoryTypeRelationRepo : IRepositoryBase<SysChargeCategoryTypeRelationEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<GetSfDlMc> GetList(string orgId, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysChargeCategoryTypeRelationEntity entity, string keyValue);

    }
}
