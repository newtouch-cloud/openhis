using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using System.Data.SqlClient;
using Newtouch.Common;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysGlobalConfigRepo : RepositoryBase<SysGlobalConfigEntity>, ISysGlobalConfigRepo
    {
        public SysGlobalConfigRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public SysGlobalConfigEntity GetForm(string keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysGlobalConfigEntity> GetList(string keyword)
        {
            var sql = "select * from Sys_GlobalConfig(nolock) where 1 = 1";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Name like @searchKeyword or Code like @searchKeyword)";
                pars.Add(new SqlParameter("@searchKeyword", "%" + keyword + "%"));
            }
            sql += " order by CreateTime desc";
            return this.FindList<SysGlobalConfigEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<SysGlobalConfigEntity> GetValidList()
        {
            var sql = "select * from Sys_GlobalConfig(nolock) where zt = '1'";
            return this.FindList<SysGlobalConfigEntity>(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysGlobalConfigEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编码不可重复");
                }

                SysGlobalConfigEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysGlobalConfigEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }
    }
}
