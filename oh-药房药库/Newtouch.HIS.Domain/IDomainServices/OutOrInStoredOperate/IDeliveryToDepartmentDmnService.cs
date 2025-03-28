using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 科室发药
    /// </summary>
    public interface IDeliveryToDepartmentDmnService
    {
        /// <summary>
        /// 提交科室发药
        /// </summary>
        /// <returns></returns>
        string SubmitDeliveryToDepartment(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);
    }
}