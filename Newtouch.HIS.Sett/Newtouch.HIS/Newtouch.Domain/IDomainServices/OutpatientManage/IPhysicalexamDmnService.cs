using Newtouch.HIS.Domain.DTO;
using System;

namespace Newtouch.HIS.Domain.IDomainServices.OutpatientManage
{
    public interface  IPhysicalexamDmnService
    {
         Tuple<string, int> SubmitPhysicalexamForm(AddPhysicalexamDto dto, string orgId, string userCode);
    }
}
