using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_crkmx
    /// </summary>
    public class SysMedicineStorageIOReceiptDetailRepo : RepositoryBase<SysMedicineStorageIOReceiptDetailEntity>, ISysMedicineStorageIOReceiptDetailRepo
    {
        public SysMedicineStorageIOReceiptDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 修改发票号
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public int UpdateFph(string crkmxId, string fph)
        {
            var mx = FindEntity(p => p.crkmxId == crkmxId);
            if (mx != null) mx.Fph = fph;
            mx.Modify();
            return Update(mx);
        }
    }
}
