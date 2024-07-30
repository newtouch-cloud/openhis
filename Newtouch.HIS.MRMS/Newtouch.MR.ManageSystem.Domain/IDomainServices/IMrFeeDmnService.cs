using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface IMrFeeDmnService
    {
        IList<bafeeVO> GetPaginationList(Pagination pagination, string orgId, string keyword);
        IList<feeSelVO> GetFeeOne(string orgId);
        IList<feeSelVO> GetFeeTwo(string orgId, string parentCode);
        bafeeVO GetFormJson(string keyValue,int index);
    }
}
