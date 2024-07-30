using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.SystemManage
{
    public class SysStaffSignatureDmnService : DmnServiceBase, ISysStaffSignatureDmnService
    {
        private readonly ISysStaffSignatureRepo _sysStaffSignatureRepo;

        public SysStaffSignatureDmnService(IBaseDatabaseFactory databaseFactory,
            ISysStaffSignatureRepo sysStaffSignatureRepo) : base(databaseFactory)
        {
            this._sysStaffSignatureRepo = sysStaffSignatureRepo;
        }
    }
}
