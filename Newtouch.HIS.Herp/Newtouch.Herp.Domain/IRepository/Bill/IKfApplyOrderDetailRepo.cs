using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 申领单明细
    /// </summary>
    public interface IKfApplyOrderDetailRepo : IRepositoryBase<KfApplyOrderDetailEntity>
    {

        /// <summary>
        /// 修改已发数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">部门单位数量，与表kf_applyOrderDetail.zhyz*sl=最小单位数量</param>
        /// <returns></returns>
        int UpdateYfsl(long id, int sl);

        /// <summary>
        /// 修改已发数量，并返回变更记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">部门单位数量，与表kf_applyOrderDetail.zhyz*sl=最小单位数量</param>
        /// <returns></returns>
        KfApplyOrderDetailEntity UpdateYfslAndSelectChangeRecord(long id, int sl);

        /// <summary>
        /// 获取申领单明细信息
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<KfApplyOrderDetailEntity> SelectData(string sldh, string organizeId);
    }
}