using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    public interface IMrFeeRelRepo
    {
        IList<bafeeRelEntity> GetPagintionList(Pagination pagination, string keyword, string organizeId);
		int SaveForm(bafeeRelEntity entity);
		int DeleteForm(string keyValue);
	}
}
