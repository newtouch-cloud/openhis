using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicalInsuranceFilingRepo : RepositoryBase<SysMedicalInsuranceFilingEntity>, ISysMedicalInsuranceFilingRepo
    {
        public SysMedicalInsuranceFilingRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SubmitForm(SysMedicalInsuranceFilingEntity entity, string ybbabId)
        {
            if (!string.IsNullOrEmpty(ybbabId))
            {
                entity.Modify();
                this.Update(entity);
            }
            else
            {
                var oldentity = this.IQueryable().Where(a => a.zt == "1" && a.OrganizeId == entity.OrganizeId && a.patId == entity.patId).OrderByDescending(a=>a.CreateTime).FirstOrDefault();
                if (oldentity != null)
                {
                    if (entity.ksrq <= oldentity.jsrq)
                    {
                        throw new FailedCodeException("NOTICE_THE_FILING_RECORD_OF_THE_PATIENT_IS_ALREADY_PRESENT_IN_THIS_TIME_INTERVAL");
                    }
                    else
                    {
                        entity.Create(true, System.Guid.NewGuid().ToString());
                        this.Insert(entity);
                    }
                }
                else
                {
                    entity.Create(true, System.Guid.NewGuid().ToString());
                    this.Insert(entity);
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string ybbabId, string orgId)
        {
            this.Delete(a => a.ybbabId == ybbabId && a.OrganizeId == orgId);
        }

    }
}
