using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices.SystemManage
{
    /// <summary>
    /// 工作流
    /// </summary>
    public class WorkFlowDmnService : DmnServiceBase, IWorkFlowDmnService
    {
        public WorkFlowDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}
