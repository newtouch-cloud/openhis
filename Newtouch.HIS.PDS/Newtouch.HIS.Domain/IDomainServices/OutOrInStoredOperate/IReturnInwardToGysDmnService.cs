using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 外部出库
    /// </summary>
    public interface IReturnInwardToGysDmnService
    {
        /// <summary>
        /// 提交外部出库
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <returns></returns>
        string SubmitReturnInwardToGys(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);
    }
}