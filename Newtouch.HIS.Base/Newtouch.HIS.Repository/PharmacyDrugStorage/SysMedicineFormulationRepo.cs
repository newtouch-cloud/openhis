using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineFormulationRepo : RepositoryBase<SysMedicineFormulationEntity>, ISysMedicineFormulationRepo
    {
        public SysMedicineFormulationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据查询药品剂型
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineFormulationEntity> GetValidList(string keyword = null)
        {
            var sql = @"select * from xt_ypjx(nolock) where zt = '1' and (jxCode like @searchKeyword or jxmc like @searchKeyword or py like @searchKeyword) order by py asc";
            return this.FindList<SysMedicineFormulationEntity>(sql, new SqlParameter[] {
                new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineFormulationEntity> GetPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_ypjx(nolock) where zt='1'
and (jxCode like @searchKeyword or jxmc like @searchKeyword or py like @searchKeyword)";
            return this.QueryWithPage<SysMedicineFormulationEntity>(sql, pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysMedicineFormulationEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.jxId != keyValue && p.jxCode == entity.jxCode))
                {
                    throw new FailedException("剂型编码不能重复！");
                }
                if (this.IQueryable().Any(p => p.jxId != keyValue && p.jxmc == entity.jxmc))
                {
                    throw new FailedException("剂型名称不能重复！");
                }

                SysMedicineFormulationEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.jxId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysMedicineFormulationEntity.GetTableName(), oldEntity.jxId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.jxCode == entity.jxCode))
                {
                    throw new FailedException("剂型编码不能重复！");
                }
                if (this.IQueryable().Any(p => p.jxmc == entity.jxmc))
                {
                    throw new FailedException("剂型名称不能重复！");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

     
    }
}
