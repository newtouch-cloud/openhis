using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysConfigApp : ISysConfigApp
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;

        /// <summary>
        /// 
        /// </summary>
        public SysConfigApp(ISysConfigRepo sysConfigRepo, ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
        }

    }
}
