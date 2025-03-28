using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysItemsRepo : RepositoryBase<SysItemsEntity>, ISysItemsRepository
    {
        public SysItemsRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取字典分类（包括无效的）
        /// </summary>
        /// <returns></returns>
        public IList<SysItemsEntity> GetList()
        {
            var query = this.IQueryable();
            return query.OrderByDescending(t => t.CreateTime).ToList();
        }

        /// <summary>
        /// 获取有效字典分类
        /// </summary>
        /// <returns></returns>
        public IList<SysItemsEntity> GetValidList()
        {
            return this.IQueryable().Where(p => p.zt == "1").OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// 提交新建、更新 实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysItemsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编码不可重复");
                }

                SysItemsEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysItemsEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
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


