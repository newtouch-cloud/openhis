using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.SysManage
{
    public class XtjyjcExecRepo : RepositoryBase<XtjyjcExecEntity>, IXtjyjcExecRepo
    {
        public XtjyjcExecRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        public void SubmitForm(XtjyjcExecEntity entity, string keyValue)
        {

        }
        public void DeleteForm(string keyValue)
        {
            var entity = this.FindEntity(keyValue);
            this.Delete(entity);
        }
    }
}
