using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.HIS.DomainServices.OutOrInStoredOperate
{
    /// <summary>
    /// 科室发药
    /// </summary>
    public class DeliveryToDepartmentDmnService : DmnServiceBase, IDeliveryToDepartmentDmnService
    {
        private readonly IKcxxDmnService kcxxDmnService;
        private readonly ISysMedicineStorageIOReceiptRepo crkdj;
        private readonly ISysMedicineStorageIOReceiptDetailRepo crdjmx;

        public DeliveryToDepartmentDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 提交科室发药
        /// </summary>
        /// <returns></returns>
        public string SubmitDeliveryToDepartment(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx)
        {
            var result = "";
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                foreach (var d in mx)
                {
                    var frozenStockResult = kcxxDmnService.SubtractStock(d.Ypdm, d.Sl * d.Ckzhyz, d.pc, d.Ph, dj.Ckbm, dj.OrganizeId);
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
