using Newtouch.HIS.Domain.Entity;

using System.Data;
using Newtouch.Infrastructure;
using System.Linq;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    public class SysPatientChargeAdditionalRepo : RepositoryBase<SysPatientChargeAdditionalEntity>, ISysPatientChargeAdditionalRepo
    {
        public SysPatientChargeAdditionalRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity, string orgId)
        {
            this.Delete(t => t.brsffjbh == sysPatiChargeAddEntity.brsffjbh && t.OrganizeId == orgId);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity, int? keyValue, string orgId)
        {
            if (keyValue > 0)
            {
                sysPatiChargeAddEntity.Modify(keyValue);
                this.Update(sysPatiChargeAddEntity);
            }
            else
            {
                sysPatiChargeAddEntity.OrganizeId = orgId;
                sysPatiChargeAddEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_brsffj"));
                this.Insert(sysPatiChargeAddEntity);
            }

        }


        public SysPatientChargeAdditionalEntity GetFWFBL (string brxz, string dl, string sfxm, string orgId)
        {
            SysPatientChargeAdditionalEntity entity = new SysPatientChargeAdditionalEntity();
            if (dl=="" || dl==null) {
                entity = this.IQueryable().Where(p => p.brxz == brxz && p.sfxm == sfxm && p.zt == "1" && p.OrganizeId == orgId).FirstOrDefault();
            } 
            else
            {
                entity = this.IQueryable().Where(p => p.dl == dl && p.brxz == brxz && p.sfxm == sfxm && p.zt == "1" && p.OrganizeId == orgId).FirstOrDefault();
            } 
            return entity;
        }

        /// <summary>
        /// 获取所有有效数据
        /// </summary>
        /// <returns></returns>
        public List<SysPatientChargeAdditionalEntity> SelectALLEffectiveList(string orgId)
        {
            return this.IQueryable().Where(a => a.zt == "1" && a.OrganizeId == orgId).ToList();
        }
    }
}
