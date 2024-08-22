using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCommercialInsuranceFilingRepo : RepositoryBase<SysCommercialInsuranceFilingEntity>, ISysCommercialInsuranceFilingRepo
    {
        public SysCommercialInsuranceFilingRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SubmitForm(SysCommercialInsuranceFilingEntity entity, string sbbabId)
        {
            if (!string.IsNullOrEmpty(sbbabId))
            {
                entity.Modify(sbbabId);
                Update(entity);
            }
            else
            {
                entity.Create(true, System.Guid.NewGuid().ToString());
                Insert(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sbbabId"></param>
        /// <param name="orgId"></param>
        public void DeleteForm(string sbbabId, string orgId)
        {
            this.Delete(a => a.sbbabId == sbbabId && a.OrganizeId == orgId);
        }
    }
}
