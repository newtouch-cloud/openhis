using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 外部入库
    /// </summary>
    public interface IInStorageDmnService
    {
        /// <summary>
        /// 提交外部入库
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <returns></returns>
        string SubmitInStorage(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);

        /// <summary>
        /// 外部入库审核通过
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="kcxx">库存信息</param>
        /// <param name="auditor">审核员</param>
        /// <returns></returns>
        string InStorageAdopt(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStockInfoEntity> kcxx, string auditor);

        /// <summary>
        /// 外部入库 撤销审核
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="auditor"></param>
        /// <returns></returns>
        string InStorageCancel(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx, string auditor);
    }
}