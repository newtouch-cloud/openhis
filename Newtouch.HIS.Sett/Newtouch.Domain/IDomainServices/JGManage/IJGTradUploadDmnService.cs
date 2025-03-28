using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.OutputDto.DRGManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.Domain.DTO.OutputDto.JGManage;

namespace Newtouch.HIS.Domain.IDomainServices.JGManage
{
    public interface IJGTradUploadDmnService
    {
        List<JGUploadDto> GetList(string orgId, string kssj, string jssj, string scqk, string zyh,string tradiNumber);
        int JGUpload(string orgId, List<JGUploadDto> list,string tradiNumber);
        List<JGYPUploadDto> GetListYp(string orgId, string kssj, string jssj, string scqk, string tradiNumber);
        List<JGsqszUploadDto> GetListsqsz(string orgId, string kssj, string jssj, string scqk, string zyh, string tradiNumber);
		List<JGmxlist> Getmxlist(string orgid, string zyh, string type);
    }
}
