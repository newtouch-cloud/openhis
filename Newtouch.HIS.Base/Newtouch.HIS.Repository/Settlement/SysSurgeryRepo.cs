using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.HIS.Domain.IRepository.Settlement;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.Settlement
{
	public class SysSurgeryRepo : RepositoryBase<SurgeryEntity>, ISysSurgeryRepo
	{
		public SysSurgeryRepo(IBaseDatabaseFactory databaseFactory)
		 : base(databaseFactory)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="OrganizeId"></param>
		/// <param name="Pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<SurgeryEntity> SurgeryGetGridJson(Pagination pagination, string keyword = null)
		{
			var sql = @"select * from xt_ssm(nolock) where  (ssm like @searchKeyword or ssmc like @searchKeyword or pym like @searchKeyword )";
			return this.QueryWithPage<SurgeryEntity>(sql, pagination, new SqlParameter[] {
					new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="OrganizeId"></param>
		/// <param name="Pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public SurgeryEntity SurgeryUpGetGridJson(int keyword)
		{
			var sql = @"select * from xt_ssm(nolock) where id=@searchKeyword";
			return this.FirstOrDefault<SurgeryEntity>(sql, new SqlParameter[] {
					new SqlParameter("@searchKeyword",keyword )
			});
		}

		public void SubmitForm(SurgeryEntity entity, int? keyValue)
		{
			if (keyValue.HasValue && keyValue.Value > 0)
			{
				var isRepeated = this.IQueryable().Any(p => p.ssm == entity.ssm && p.id != keyValue);
				
				if (isRepeated)
				{
					throw new FailedException("编码不可重复");
				}

				SurgeryEntity oldEntity = null;   //变更前Entity
				oldEntity = this.FindEntity(keyValue.Value);
				this.DetacheEntity(oldEntity);

				entity.Modify();
				entity.id = keyValue.Value;
				this.Update(entity);

				if (oldEntity != null)
				{
					AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SurgeryEntity.GetTableName(), oldEntity.id.ToString());
				}
			}
			else
			{
				var isRepeated = this.IQueryable().Any(p => p.ssm == entity.ssm);
					
				if (isRepeated)
				{
					throw new FailedException("编码不可重复");
				}
				this.Insert(entity);
			}
		}
	}
}
