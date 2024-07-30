using Newtouch.Common;
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
    public class SysApplicationRepo : RepositoryBase<SysApplicationEntity>, ISysApplicationRepo
    {
        public SysApplicationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取列表（包括无效的）
        /// </summary>
        /// <returns></returns>
        public IList<SysApplicationEntity> GetList()
        {
            return this.IQueryable().ToList();
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public IList<SysApplicationEntity> GetValidList()
        {
            return this.IQueryable().Where(p => p.zt == "1").ToList();
        }

        /// <summary>
        /// 提交新建、更新 实体
        /// </summary>
        /// <returns></returns>
        public void SubmitForm(SysApplicationEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                SysApplicationEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysApplicationEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 返回一个实体
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public SysApplicationEntity GetEntity(string appId)
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                return null;
            }
            var entity = this.IQueryable().Where(a => a.Id == appId).FirstOrDefault();
            return entity;
        }

    }
}
