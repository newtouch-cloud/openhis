using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Common;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysNationRepo : RepositoryBase<SysNationEntity>, ISysNationRepo
    {
        public SysNationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysNationEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysNationEntity> GetList(string keyword)
        {
            var sql = "select * from xt_mz(nolock) where 1 = 1";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (mzCode like @searchKeyword or mzmc like @searchKeyword)";
                pars.Add(new SqlParameter("@searchKeyword", "%" + keyword + "%"));
            }
            sql += " order by CreateTime desc";
            return this.FindList<SysNationEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysNationEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.mzCode == entity.mzCode && p.mzId != keyValue))
                {
                    throw new FailedException("编码不可重复");
                }

                SysNationEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.mzId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysNationEntity.GetTableName(), oldEntity.mzId.ToString());
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.mzCode == entity.mzCode))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create();
                this.Insert(entity);
            }
        }
    }
}
