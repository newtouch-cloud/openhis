using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.HIS.DomainServices.OutOrInStoredOperate
{

    /// <summary>
    /// 直接出库
    /// </summary>
    public class DeliveryDirectDmnService : DmnServiceBase, IDeliveryDirectDmnService
    {
        private readonly IKcxxDmnService kcxxDmnService;
        private readonly ISysMedicineStorageIOReceiptRepo crkdj;
        private readonly ISysMedicineStorageIOReceiptDetailRepo crdjmx;

        public DeliveryDirectDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 提交直接出库
        /// </summary>
        /// <returns></returns>
        public string SubmitDeliveryDirect(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx)
        {
            var result = "";
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                foreach (var d in mx)
                {
                    var frozenStockResult = kcxxDmnService.FrozenStockReduceByDeliveryDirect(d.Ypdm, dj.OrganizeId, d.pc, d.Ph, d.Sl * d.Ckzhyz, dj.Ckbm);
                    if (!string.IsNullOrWhiteSpace(frozenStockResult))
                    {
                        return frozenStockResult;
                    }
                }
                crkdj.Insert(dj);
                crdjmx.Insert(mx);
                db.Commit();
            }
            return result;
        }
    }
}
