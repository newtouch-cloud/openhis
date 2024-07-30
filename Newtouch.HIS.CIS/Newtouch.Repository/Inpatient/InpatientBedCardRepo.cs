using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IRepository.Inpatient;
using System.Linq;

namespace Newtouch.Repository.Inpatient
{
	public class InpatientBedCardRepo : RepositoryBase<InpatientBedCardEntity>, IInpatientBedCardRepo
	{
		/// <summary>
	 /// 默认构造函数
	 /// </summary>
	 /// <param name="databaseFactory">EF上下文工厂</param>
		public InpatientBedCardRepo(IDefaultDatabaseFactory databaseFactory)
			: base(databaseFactory)
		{

		}

		/// <summary>
		/// 保存表单（新增、修改）
		/// </summary>
		/// <param name="entity">实体对象</param>
		/// <param name="keyValue">主键值</param>
		public bool SubmitForm(InpatientBedCardEntity entity, string zyh)
		{
			if (!string.IsNullOrEmpty(zyh))
			{
				var dbEntity = this.IQueryable().FirstOrDefault(p => p.OrganizeId == entity.OrganizeId && p.zyh == entity.zyh && p.zt == "1");
				if (dbEntity != null)

				//var dbEntity = this.FindEntity(keyValue);
				//if (dbEntity != null)
				{
					//properties

					dbEntity.zrhs = entity.zrhs;
					dbEntity.zrzz = entity.zrzz;
					dbEntity.gms = entity.gms;
					dbEntity.fbsx = entity.fbsx;
					dbEntity.sjys = entity.sjys;
					dbEntity.hsz = entity.hsz;
					dbEntity.kzr = entity.kzr;
					dbEntity.xh = entity.xh;
					dbEntity.jb = entity.jb;
					dbEntity.zt = "1";
					dbEntity.Modify(dbEntity.Id);
					return this.Update(dbEntity) > 0;
				}
				else
				{
					entity.Create(true);
					return this.Insert(entity) > 0;
				}
			}
			
			return false;
		}

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="keyValue">主键值</param>
		public void DeleteForm(string keyValue)
		{
			this.Delete(p => p.Id == keyValue);
		}

	}
}
