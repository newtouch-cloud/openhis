using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    public class SysAccountRepo : RepositoryBase<SysAccountEntity>, ISysAccountRepo
    {
        public SysAccountRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 更新账户
        /// </summary>
        /// <returns></returns>
        public bool ModifyAcc(SysAccountEntity entity)
        {
            entity.Modify(entity.zhCode);
            entity.zt = "1";
            this.Update(entity);
            return true;
        }
    }
}
