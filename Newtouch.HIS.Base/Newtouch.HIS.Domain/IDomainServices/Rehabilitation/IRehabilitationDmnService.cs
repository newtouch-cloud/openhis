using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.Rehabilitation;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.Rehabilitation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehabilitationDmnService
    {
        /// <summary>
        /// 获得所有列表
        /// </summary>
        List<RehabChargeItemVO> GetRehabChargeItemList(string keyword, string OrganizeId);

        /// <summary>
        /// 修改form
        /// </summary>
        RehabChargeItemVO GetRehabChargeItemEntity(string sfxmId, string OrganizeId);

        /// <summary>
        /// 获得所有列表(康复收费项目对照)
        /// </summary>
        List<RehabChargeItemComparisonVO> GetRehabChargeItemComparisonList(string keyword, string OrganizeId);

        /// <summary>
        /// 修改form(康复收费项目对照)
        /// </summary>
        RehabChargeItemComparisonVO GetRehabChargeItemComparisonEntity(string sfxmdzId,string OrganizeId);

        /// <summary>
        /// 收费项目检索
        /// </summary>
        IList<SysChargeItemVO> GetHisBindSelect(string keyword, string orgId);
    }
}
