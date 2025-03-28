using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.IDomainServices
{
    public interface IOPRegisterDmnService
    {
        //RegistrationListVO getRegistrationInfo(string ArrangeId, string sqzt);
        //IList<ArrangeRegVO> GetPagintionList(Pagination pagination, string keyword, string bq, string djzt);
        RegistrationListVO getRegistrationInfo(string OrganizeId,string ArrangeId, string sqzt);
        IList<ArrangeRegVO> GetPagintionList(Pagination pagination, string OrganizeId,string keyword, string bq, string djzt);
        IList<RegistrationListVO> GetPatOpRegistList(string ksrq, string jsrq, string zyh, string djzt, string orgId);
    }
}
