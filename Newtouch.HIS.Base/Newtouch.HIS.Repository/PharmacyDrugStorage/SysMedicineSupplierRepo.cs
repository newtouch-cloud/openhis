using System;
using System.Collections.Generic;
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using Newtouch.Core.Common.Exceptions;
using System.Linq;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineSupplierRepo : RepositoryBase<SysMedicineSupplierEntity>, ISysMedicineSupplierRepo
    {
        public SysMedicineSupplierRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        
        public IList<SysMedicineSupplierEntity> GetPagintionList(string OrganizeId, Pagination Pagination, string keyword = null)
        {
            var Sql = @"select * from xt_ypgys where OrganizeId=@OrganizeId 
and (isnull(@keyword,'')=''or gysmc like @searchkeyword or gysCode like @searchkeyword or py like @searchkeyword)";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@keyword", keyword ?? ""),
                new SqlParameter("@searchkeyword","%"+(keyword??"")+"%"),
            };
            return this.QueryWithPage<SysMedicineSupplierEntity>(Sql, Pagination, par);
        }

        public void SubmitForm(SysMedicineSupplierEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.gysId != keyValue
                && p.gysCode == entity.gysCode))
                {
                    throw new FailedException("编号不能重复！");
                }

                SysMedicineSupplierEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.gysId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysMedicineSupplierEntity.GetTableName(), oldEntity.gysId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId &&
                p.gysCode == entity.gysCode))
                {
                    throw new FailedException("编号不能重复！");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

    }
}
