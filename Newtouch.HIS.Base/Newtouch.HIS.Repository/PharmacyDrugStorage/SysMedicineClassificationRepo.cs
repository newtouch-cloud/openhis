using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineClassificationRepo : RepositoryBase<SysMedicineClassificationEntity>, ISysMedicineClassificationRepo
    {
        public SysMedicineClassificationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据关键字查询药品大分类
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineClassificationEntity> GetValidList(string keyword = null)
        {
            var sql = @"select * from xt_ypfl(nolock) where zt = '1' and (ypflCode like @searchKeyword or ypflmc like @searchKeyword or py like @searchKeyword) order by CreateTime desc";
            return this.FindList<SysMedicineClassificationEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineClassificationEntity> GetPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_ypfl(nolock) where zt='1'
and (ypflCode like @searchKeyword or ypflmc like @searchKeyword or py like @searchKeyword)";
            return this.QueryWithPage<SysMedicineClassificationEntity>(sql, pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysMedicineClassificationEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.ypflId != keyValue && p.ypflCode == entity.ypflCode))
                {
                    throw new FailedException("编码不能重复！");
                }
                if (this.IQueryable().Any(p => p.ypflId != keyValue && p.ypflmc == entity.ypflmc))
                {
                    throw new FailedException("名称不能重复！");
                }

                SysMedicineClassificationEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.ypflId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysMedicineClassificationEntity.GetTableName(), oldEntity.ypflId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.ypflCode == entity.ypflCode))
                {
                    throw new FailedException("编码不能重复！");
                }
                if (this.IQueryable().Any(p => p.ypflmc == entity.ypflmc))
                {
                    throw new FailedException("名称不能重复！");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

    }
}
