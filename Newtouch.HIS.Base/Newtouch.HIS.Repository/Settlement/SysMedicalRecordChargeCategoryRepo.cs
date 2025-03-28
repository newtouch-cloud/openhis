using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
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
    /// 病案收费大类
    /// </summary>
    public class SysMedicalRecordChargeCategoryRepo : RepositoryBase<SysMedicalRecordChargeCategoryEntity>, ISysMedicalRecordChargeCategoryRepo
    {
        public SysMedicalRecordChargeCategoryRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicalRecordChargeCategoryEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysMedicalRecordChargeCategoryEntity> GetList(string orgId, string keyword = null)
        {
            var sql = @"select * from xt_basfdl(nolock) where OrganizeId = @orgId
and (dlCode like @searchKeyword or dlmc like @searchKeyword or py like @searchKeyword)
order by CreateTime desc";

            return this.FindList<SysMedicalRecordChargeCategoryEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysMedicalRecordChargeCategoryEntity> GetValidList(string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1").OrderByDescending(a => a.dlId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysMedicalRecordChargeCategoryEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.dlCode == entity.dlCode && p.dlId != keyValue.Value && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }

                SysMedicalRecordChargeCategoryEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.dlId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysMedicalRecordChargeCategoryEntity.GetTableName(), oldEntity.dlId.ToString());
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.dlCode == entity.dlCode && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create();
                this.Insert(entity);
            }
        }
    }
}
