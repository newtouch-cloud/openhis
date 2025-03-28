using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices.SystemManage
{
    public class SysPatiChargeLogicDmnService : DmnServiceBase, ISysPatiChargeLogicDmnService
    {
        public SysPatiChargeLogicDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
