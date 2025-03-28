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
    public class SysNationalityRepo : RepositoryBase<SysNationalityEntity>, ISysNationalityRepo
    {
        public SysNationalityRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysNationalityEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysNationalityEntity> GetList(string keyword)
        {
            var sql = "select * from xt_gj(nolock) where 1 = 1";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (gjCode like @searchKeyword or gjmc like @searchKeyword)";
                pars.Add(new SqlParameter("@searchKeyword", "%" + keyword + "%"));
            }
            sql += " order by CreateTime desc";
            return this.FindList<SysNationalityEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysNationalityEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.gjCode == entity.gjCode && p.gjId != keyValue))
                {
                    throw new FailedException("编码不可重复");
                }

                SysNationalityEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.gjId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysNationalityEntity.GetTableName(), oldEntity.gjId.ToString());
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.gjCode == entity.gjCode))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create();
                this.Insert(entity);
            }
        }
    }
}
