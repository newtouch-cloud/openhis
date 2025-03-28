using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCityAreaApp : ISysCityAreaApp
    {
        private readonly ISysAreaRepo _sysFielRepository;
        //other Repositories or DomainServices

        public SysCityAreaApp(ISysAreaRepo sysFielRepository)
        {
            this._sysFielRepository = sysFielRepository;
        }
    }
}
