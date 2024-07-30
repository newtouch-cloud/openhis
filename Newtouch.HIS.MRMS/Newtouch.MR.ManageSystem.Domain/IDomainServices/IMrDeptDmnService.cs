using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface IMrDeptDmnService
    {
        IList<MrDeptVO> GetPaginationList(Pagination pagination, string orgId);
    }
}
