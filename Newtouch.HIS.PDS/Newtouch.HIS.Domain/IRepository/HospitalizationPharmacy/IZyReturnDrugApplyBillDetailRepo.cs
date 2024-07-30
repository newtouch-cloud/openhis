using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 住院退药申请单明细
    /// </summary>
    public interface IZyReturnDrugApplyBillDetailRepo : IRepositoryBase<ZyReturnDrugApplyBillDetailEntity>
    {
        /// <summary>
        /// 根据退药申请Id获取退药申请明细
        /// </summary>
        /// <param name="rabId"></param>
        /// <returns></returns>
        List<ZyReturnDrugApplyBillDetailEntity> SelectData(string rabId);
    }
}