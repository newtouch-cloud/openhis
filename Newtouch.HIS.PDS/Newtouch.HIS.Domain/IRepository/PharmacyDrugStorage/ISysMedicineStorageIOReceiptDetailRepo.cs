using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_crkmx
    /// </summary>
    public interface ISysMedicineStorageIOReceiptDetailRepo : IRepositoryBase<SysMedicineStorageIOReceiptDetailEntity>
    {
        /// <summary>
        /// 修改发票号
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        int UpdateFph(string crkmxId, string fph);
    }
}
