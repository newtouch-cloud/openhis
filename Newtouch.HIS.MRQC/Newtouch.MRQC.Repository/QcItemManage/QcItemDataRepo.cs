using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using Newtouch.MRQC.Domain.IRepository.QcItemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Repository.QcItemManage
{
    public class QcItemDataRepo : RepositoryBase<QcItemDataEntity>, IQcItemDataRepo
    {
        public QcItemDataRepo(IDefaultDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }

        public void DeleteForm(int Id, string orgId)
        {
            this.Delete(a => a.Id == Id && a.OrganizeId == orgId);
        }
    }
}
