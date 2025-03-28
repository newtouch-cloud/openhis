using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysStaffSignatureRepo : IRepositoryBase<SysStaffSignatureEntity>
    {
        void SubmitFor(SysStaffSignatureEntity entity);
    }
}
