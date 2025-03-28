using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Interface;
using Newtouch.Domain.Entity;
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
    public class AuxiliaryDictionaryRepo : RepositoryBase<SysAuxiliaryDictionaryEntity>, IAuxiliaryDictionaryRepo
    {

        private readonly ICache _cache;
        public AuxiliaryDictionaryRepo(IDefaultDatabaseFactory databaseFactory, ICache cache)
            : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// 获取当前组织下的所有有效词典
        /// </summary>
        /// <returns></returns>
        public IList<SysAuxiliaryDictionaryEntity> GetValidListByOrg(string orgId, bool withCache = false)
        {
            Func<IList<SysAuxiliaryDictionaryEntity>> ac = () =>
            {
                var sql = "select * from xt_fzcd(nolock) where zt = '1' and OrganizeId = @orgId";
                return this.FindList<SysAuxiliaryDictionaryEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
            };
            if (!withCache)
            {
                return ac.Invoke();
            }
            var key = string.Format(CacheKey.ValidSysAuxiliaryDictionaryListSetKey, orgId);
            var list = _cache.Get(key, ac);
            return list;
        }

        /// <summary>
        /// 获取词典列表
        /// </summary>
        /// <returns></returns>
        public List<SysAuxiliaryDictionaryEntity> GetListByOrg(string orgId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysAuxiliaryDictionaryEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编号不可重复");
                }

                SysAuxiliaryDictionaryEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.Code == entity.Code))
                {
                    throw new FailedException("编号不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
            var key = string.Format(CacheKey.ValidSysAuxiliaryDictionaryListSetKey, entity.OrganizeId);
            _cache.Remove(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            var entity = this.FindEntity(keyValue);
            if (entity != null)
            {
                var list = this.IQueryable().Where(a => a.OrganizeId == entity.OrganizeId && a.ParentId == entity.Id).ToList();
                foreach (var item in list)
                {
                    item.ParentId = null;
                    item.Modify();
                    this.Update(item);
                }
            }
            this.Delete(entity);
        }

    }
}
