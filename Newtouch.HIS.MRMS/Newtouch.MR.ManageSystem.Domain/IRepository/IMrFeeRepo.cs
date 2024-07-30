using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    public interface IMrFeeRepo
    {
        int SubmitForm(bafeeVO entity, string keyValue,string OrganizeId,bafeeVO oldEntity);
        int DeleteForm(string keyValue);
		IList<bafeeEntity> GetAllFeeList(string orgId);

	}
}
