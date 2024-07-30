using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;


namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 药品盘点明细 xt_yp_pdxxmx
    /// </summary>
    public class SysMedicineInventoryDetailRepo : RepositoryBase<SysMedicineInventoryDetailEntity>, ISysMedicineInventoryDetailRepo
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysMedicineInventoryDetailRepo(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }

        /// <summary>
        /// 盘点保存时 变更数量
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        public void UpdateSlBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                foreach (var item in inventoryInfoList)
                {
                    var entity = db.FindEntity<SysMedicineInventoryDetailEntity>(item.pdmxId);
                    entity.Sjsl = item.sjsl;
                    entity.Modify();
                    db.Update(entity);
                }
                db.Commit();
            }
        }

    }
}
