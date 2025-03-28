using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.SystemManage
{
    public class Sh_YbfyxzblRepo: RepositoryBase<Sh_YbfyxzblEntity>, ISh_YbfyxzblRepo
    {
        public Sh_YbfyxzblRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public void SubmitForm(Sh_YbfyxzblEntity entity, string ksCode)
        {
            throw new NotImplementedException();
        }
    }
}
