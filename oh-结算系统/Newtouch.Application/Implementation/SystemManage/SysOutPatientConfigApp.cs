using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysOutPatientConfigApp : ISysOutPatientConfigApp
    {
        private readonly ISysConfigOutpaientRepo _sysOutPatientConfigRepository;
        //other Repositories or DomainServices

        public SysOutPatientConfigApp(ISysConfigOutpaientRepo sysOutPatientConfigRepository)
        {
            this._sysOutPatientConfigRepository = sysOutPatientConfigRepository;
        }

    }
}
