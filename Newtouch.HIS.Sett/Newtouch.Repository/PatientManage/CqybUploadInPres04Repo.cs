using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.PatientManage
{
	public class CqybUploadInPres04Repo : RepositoryBase<CqybUploadInPres04Entity>, ICqybUploadInPres04Repo
	{
		public CqybUploadInPres04Repo(IDefaultDatabaseFactory databaseFactory)
			: base(databaseFactory)
		{
		}
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="hospItemFeeEntity"></param>
		/// <param name="keyValue"></param>
		public void SaveCqybUploadInPres(List<CqybUploadInPres04Entity> entityList)
		{
			if (entityList != null && entityList.Count > 0)
			{
				foreach (var entity in entityList)
				{
                    var oEnt = this.FindEntity(p => p.cfh == entity.cfh && p.pch == entity.pch && p.OrganizeId == entity.OrganizeId && p.zymzh == entity.zymzh && p.zt == "1");
                    if (oEnt != null)
                    {
                        oEnt.zt = "0";
                        oEnt.Modify();
                        this.Update(oEnt);
                    }
                    entity.Create();
                    this.Insert(entity);
                }
			}
		}
	}
}
