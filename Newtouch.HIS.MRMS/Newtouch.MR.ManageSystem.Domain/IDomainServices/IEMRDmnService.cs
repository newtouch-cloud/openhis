using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface IEMRDmnService
    {
        //IList<PatListVO> GetPatList(Pagination pagination, string keyword, string zyh, string ysgh, string OrgId, int type);
        IList<PatMedRecordTreeVO> GetPatMedRecordTree(string OrgId, string zyh, string rygh);
        IList<PatListVO> GetPatList(Pagination pagination, string keyword, string zyh,
            string cyts, string blzt, string ysgh, string OrgId, int type);
		IList<PatListVO> GetPagintionList(Pagination pagination, string keyword, string zyh,
			string cyts, string blzt, string ysgh, string OrgId, int type, string kssj, string jssj);
        ZybrjbxxVO GetZyPatInfobyzyh(string orgId, string zyh);

    }
}
