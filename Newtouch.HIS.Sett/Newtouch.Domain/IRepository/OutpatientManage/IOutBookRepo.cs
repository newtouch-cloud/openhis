using Newtouch.HIS.Domain.Entity.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.OutpatientManage
{
    public interface IOutBookRepo
    {
        int UpdateForm(int keyValue,string orgId,string ks, string gh,string CreateCode, DateTime CreateTime);
        IList<string> getStaffByKs(string ks);
        int updateZt(string ks, string CreateCode, DateTime CreateTime);
        int SubmitForm(string orgId, string ks, string gh, string CreateCode, DateTime CreateTime);
    }
}
