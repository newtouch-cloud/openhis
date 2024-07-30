using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Common;
using System;
using System.Data.SqlClient;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineUsageRepo : RepositoryBase<SysMedicineUsageEntity>, ISysMedicineUsageRepo
    {
        public SysMedicineUsageRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        public void SubmitForm(SysMedicineUsageEntity entity, int? yfId)
        {
            if (yfId!=null && yfId > 0)
            {
                //Code重复判断
                if (this.IQueryable().Any(a => a.yfCode == entity.yfCode && a.yfId != yfId))
                {
                    throw new FailedException("编码不可重复");
                }

                SysMedicineUsageEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(yfId.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.yfId = yfId.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysMedicineUsageEntity.GetTableName(), oldEntity.yfId.ToString());
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(a => a.yfCode == entity.yfCode))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create();
                this.Insert(entity);

            }

        }

        /// <summary>
        /// 获得所有列表
        /// </summary>
        public List<SysMedicineUsageEntity> GetMedicineUsageList(string keyword = null)
        {
            List<SysMedicineUsageEntity> list = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                list = this.IQueryable().Where(a => a.yfmc.Contains(keyword) || a.py.Contains(keyword) || a.yfCode.Contains(keyword)).ToList();
            }
            else
            {
                list = this.IQueryable().ToList();
            }
            return list;
        }

        /// <summary>
        /// 修改form
        /// </summary>
        public SysMedicineUsageEntity GetMedicineUsageEntity(int? yfId)
        {
            var entity = this.FindEntity(yfId);
            return entity;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteForm(int yfId)
        {
            this.Delete(a => a.yfId == yfId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineUsageEntity> GetPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = "select * from xt_ypyf(nolock) where  1=1";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (yfCode like @searchKeyword or yfmc like @searchKeyword or py like @searchKeyword)";
                pars.Add(new SqlParameter("@searchKeyword", "%" + keyword + "%"));
            }
            return this.QueryWithPage<SysMedicineUsageEntity>(sql, pagination, pars.ToArray());
        }
        
    }
}
