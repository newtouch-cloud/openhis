using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.SystemManage
{
    public class SysStaffSignatureRepo : RepositoryBase<SysStaffSignatureEntity>, ISysStaffSignatureRepo
    {
        public SysStaffSignatureRepo(IBaseDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }

        public void SubmitFor(SysStaffSignatureEntity entity)
        {
            if (entity!=null)
            {
                var enty = this.IQueryable().Where(p => p.StaffId == entity.StaffId && p.OrganizeId==entity.OrganizeId).FirstOrDefault();
                
                if (enty != null)
                {
                    enty.ImageData = entity.ImageData;
                    enty.ImageName = entity.ImageName;
                    enty.ImageTpye = entity.ImageTpye;
                    enty.ImageUrl = entity.ImageUrl;
                    enty.ImagePrefix = entity.ImagePrefix;
                    enty.Modify();
                    this.Update(enty);
                }
                else
                {
                    entity.Create(true);
                    this.Insert(entity);
                }
            }
        }
    }
}
