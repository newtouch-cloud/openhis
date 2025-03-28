using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatientChargeRangeRepo : RepositoryBase<SysPatientChargeRangeEntity>, ISysPatientChargeRangeRepo
    {
        public SysPatientChargeRangeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysPatientChargeRangeEntity GetForm(int keyValue, string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId && a.brsffwbh == keyValue).FirstOrDefault();
        }
        public void DeleteForm(int keyValue, string orgId)
        {
            this.Delete(t => t.brsffwbh == keyValue && t.OrganizeId == orgId);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysPatiChargeRangeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientChargeRangeEntity sysPatiChargeRangeEntity, int? keyValue, string orgId)
        {
            if (keyValue > 0)
            {
                sysPatiChargeRangeEntity.Modify(keyValue);
                this.Update(sysPatiChargeRangeEntity);
            }
            else
            {
                sysPatiChargeRangeEntity.OrganizeId = orgId;
                sysPatiChargeRangeEntity.Create(true, EFDBBaseFuncHelper.GetInstance().GetNewPrimaryKeyInt(SysPatientChargeRangeEntity.GetTableName()));
                this.Insert(sysPatiChargeRangeEntity);
            }

        }
    }
}


