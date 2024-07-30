using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.OutpatientManage
{
    public interface IOutBookScheduleService
    {
        IList<OutBookScheduleVO> GetPagintionList(Pagination pagination, string orgId, DateTime time);
    }
}
