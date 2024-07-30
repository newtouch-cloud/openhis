using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysTCMSyndromeRepo : IRepositoryBase<SysTCMSyndromeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysTCMSyndromeEntity> GetCommonPagintionList(Pagination Pagination, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysTCMSyndromeEntity> GetPagintionList(string orgId, Pagination Pagination, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysTCMSyndromeEntity entity, int? keyValue);
    }
}
