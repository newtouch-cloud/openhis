using HIS.SSO.Domain.Entity.SysManage;
using HIS.SSO.Domain.Model.SysManage;
using HIS.SSO.Domain.ValueObjects.SysManage;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.SSO.Domain.IDomainServices
{
	public  interface IShortcutMenuDmnService : IScopedDependency
	{
		Task<List<SysShortcutMenuEntity>> GetShortcutMenuList(string userCode, string orgId);

		Task<BusResult<string>> SaveShortcutMenu(ShorcutMenuModel request);

		Task<List<HomeDataTotalVo>> GetDutyTotalList(string dutycode, DateTime tjrq, string usercode, string orgId, bool isAdmin);


	}
}
