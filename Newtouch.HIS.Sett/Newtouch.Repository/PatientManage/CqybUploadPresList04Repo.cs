using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Repository.PatientManage
{
    public class CqybUploadPresList04Repo : RepositoryBase<CqybUploadPresList04Entity>, ICqybUploadPresList04Repo
	{
		public CqybUploadPresList04Repo(IDefaultDatabaseFactory databaseFactory)
			: base(databaseFactory)
		{
		}
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="hospItemFeeEntity"></param>
		/// <param name="keyValue"></param>
		public void SaveCqybUploadPresList(List<CqybUploadPresList04Entity> entityList, string jytype,string zymzh, string orgId)
		{
			if (entityList != null && entityList.Count>0)
			{
				foreach (var entity in entityList)
				{
					entity.OrganizeId = orgId;
                    entity.jytype = jytype;
					entity.zymzh = zymzh;
                    entity.Create();
					this.Insert(entity);
				}
				
			}
		}
	}
}
