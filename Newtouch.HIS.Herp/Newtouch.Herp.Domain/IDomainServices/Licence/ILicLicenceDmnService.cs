using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 证照维护
    /// </summary>
    public interface ILicLicenceDmnService
    {
        /// <summary>
        /// 获取证照类型
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        IList<VLicLicenceTypeEntity> GetLicenceTypeList(Pagination pagination, string keyWord = "");

        /// <summary>
        /// 获取证照列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="belongedId"></param>
        /// <param name="licenceTypeId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VLicLicencesEntity> GetLicenceList(Pagination pagination, string keyWord, string belongedId, string licenceTypeId, string organizeId);
    }
}