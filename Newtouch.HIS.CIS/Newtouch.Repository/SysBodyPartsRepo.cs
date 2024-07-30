using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.Repository
{
    public class SysBodyPartsRepo : RepositoryBase<SysBodyPartsEntity>, ISysBodyPartsRepo
    {
        private readonly ICache _cache;

        public SysBodyPartsRepo(IDefaultDatabaseFactory databaseFactory
            , ICache cache)
            : base(databaseFactory)
        {
            _cache = cache;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<SysBodyPartsEntity> GetListByOrg(string orgId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysBodyPartsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.bwCode == entity.bwCode && p.Id != keyValue))
                {
                    throw new FailedException("编号不可重复");
                }

                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.bwCode == entity.bwCode))
                {
                    throw new FailedException("编号不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
            //刷新缓存
            _cache.Remove(string.Format(CacheKey.ValidSysBodyPartsSetKey_, entity.OrganizeId));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgId"></param>
        public void DeleteForm(string keyValue)
        {
            var entity = this.FindEntity(keyValue);
            this.Delete(entity);
        }

    }
}
