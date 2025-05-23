﻿using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.OutpatientManage
{
	public class CqybSett05Repo : RepositoryBase<CqybSett05Entity>, ICqybSett05Repo
	{
		public CqybSett05Repo(IDefaultDatabaseFactory databaseFactory)
			: base(databaseFactory)
		{
		}
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="hospItemFeeEntity"></param>
		/// <param name="keyValue"></param>
		public void SaveCqybSett(CqybSett05Entity Entity, string keyValue)
		{
			if (!string.IsNullOrWhiteSpace(keyValue))
			{
				Entity.Modify(keyValue);
				Update(Entity);
			}
			else
			{
				Entity.Create();
				Entity.zt = "1";
                this.Insert(Entity);
			}
		}
	}
}
