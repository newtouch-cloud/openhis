using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.PatientManage
{
    public class CqybUpdateMedicalInput03Repo: RepositoryBase<CqybUpdateMedicalInput03Entity>, ICqybUpdateMedicalInput03Repo
    {
        public CqybUpdateMedicalInput03Repo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public void SaveCqybUpdateMedicalS03InReg(CqybUpdateMedicalInput03Entity Entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                Entity.Modify(keyValue);
                Update(Entity);
            }
            else
            {
                var entity = this.FindEntity(p => p.zymzh == Entity.zymzh && p.OrganizeId == Entity.OrganizeId && p.zt == "1");
                if (entity != null)
                {
                    entity.Modify();
                    entity.zt = "0";
                    this.Update(entity);
                }
                Entity.Create();
                this.Insert(Entity);
            }
        }
    }
}
