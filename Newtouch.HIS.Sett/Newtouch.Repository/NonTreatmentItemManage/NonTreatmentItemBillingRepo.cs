using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class NonTreatmentItemBillingRepo : RepositoryBase<NonTreatmentItemBillingEntity>, INonTreatmentItemBillingRepo
    {
        public NonTreatmentItemBillingRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        public void SaveBilling(List<NonTreatmentItemBillingEntity> entityList)
        {
            if (entityList == null || entityList.Count == 0)
            {
                return;
            }
            foreach (var item in entityList)
            {
                this.Insert(item);
            }
            
        }
    }
}
