using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.OutpatientManage
{
    public interface IOutBookDateRepo
    {
        int UpdateDate(OutBookArrangeVO entity, int ghpbId, string weekdd, string orgId, string User, DateTime Time,int ghpbIdNew);
    }
}
