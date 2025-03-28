using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.OutputDto.JGManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.JGManage
{
    public interface IJGUploadDmnService
    {
        List<JGUploadDto> GetList(Pagination pagination, string orgId, string kssj, string jssj,string scqk,string zyh);
        int JGUpload(string orgId,List<JGUploadDto> list);
    }
}
