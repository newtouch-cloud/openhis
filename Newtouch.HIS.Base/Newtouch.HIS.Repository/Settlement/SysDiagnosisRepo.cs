using System;
using System.Collections.Generic;
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using System.Data.SqlClient;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.Settlement;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysDiagnosisRepo : RepositoryBase<SysDiagnosisEntity>, ISysDiagnosisRepo
    {
        public SysDiagnosisRepo(IBaseDatabaseFactory databaseFactory)
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
        public IList<SysDiagnosisEntity> GetCommonPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_zd(nolock) where (OrganizeId = '*')
and (zdCode like @searchKeyword or zdmc like @searchKeyword or py like @searchKeyword or icd10 like @searchKeyword)";
            return this.QueryWithPage<SysDiagnosisEntity>(sql, pagination, new SqlParameter[] {
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
		public IList<SysDiagnosisEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_zd(nolock) where (OrganizeId = '*' or OrganizeId = @orgId) 
and (zdCode like @searchKeyword or zdmc like @searchKeyword or py like @searchKeyword or icd10 like @searchKeyword)";
            return this.QueryWithPage<SysDiagnosisEntity>(sql, pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    ,new SqlParameter("@orgId", orgId)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysDiagnosisEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var isRepeated = entity.OrganizeId == "*"
                    ? this.IQueryable().Any(p =>
                        p.zdCode == entity.zdCode && p.zdId != keyValue)
                    : this.IQueryable().Any(p => (p.OrganizeId == entity.OrganizeId || p.OrganizeId == "*")
                         && p.zdCode == entity.zdCode && p.zdId != keyValue)
                         ;
                if (isRepeated)
                {
                    throw new FailedException("编码不可重复");
                }

                SysDiagnosisEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.zdId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysDiagnosisEntity.GetTableName(), oldEntity.zdId.ToString());
                }
            }
            else
            {
                var isRepeated = entity.OrganizeId == "*"
                    ? this.IQueryable().Any(p =>
                        p.zdCode == entity.zdCode)
                    : this.IQueryable().Any(p => (p.OrganizeId == entity.OrganizeId || p.OrganizeId == "*")
                         && p.zdCode == entity.zdCode)
                         ;
                if (isRepeated)
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

    }
}
