using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.SystemManage
{
    public interface ISysConsultDmnService
    {
        IList<SysExpertVO> GetExpertListByDept(string orgId, string ksCode);
        string GetNameByEpxert(string orgId, string gh);
    }
}
