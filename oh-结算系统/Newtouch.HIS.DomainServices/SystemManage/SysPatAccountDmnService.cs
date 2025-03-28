using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 系统病人账户 相关
    /// </summary>
    public class SysPatAccountDmnService : DmnServiceBase, ISysPatAccountDmnService
    {
        public SysPatAccountDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


    }
}
