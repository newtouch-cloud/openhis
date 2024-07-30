using System.Collections;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 物资收费项目关联
    /// </summary>
    public interface IRelProductAndsfxmDmnService
    {

        /// <summary>
        /// 查询物资收费项目对照管理
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sfdlCode"></param>
        /// <param name="productTypeId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<RelProductAndsfxmVo> SelectProductAndsfxm(Pagination pagination, string sfdlCode, string productTypeId, string organizeId, string zt);

        /// <summary>
        /// 查询物资收费项目对照管理
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        RelProductAndsfxmVo SelectProductAndsfxm(string relId, string organizeId);
    }
}