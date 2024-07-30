using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Interface;
using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class Pacs_ExamBodyPartsRepo : RepositoryBase<Pacs_ExamBodyPartsEntity>, IPacs_ExamBodyPartsRepo
    {
        private readonly ICache _cache;

        public Pacs_ExamBodyPartsRepo(IDefaultDatabaseFactory databaseFactory
            , ICache cache)
            : base(databaseFactory)
        {
            _cache = cache;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<Pacs_ExamBodyPartsEntity> GetListByOrg(string orgId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(Pacs_ExamBodyPartsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.jcbwCode == entity.jcbwCode && p.Id != keyValue))
                {
                    throw new FailedException("部位编号不可重复");
                }

                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.jcbwCode == entity.jcbwCode))
                {
                    throw new FailedException("部位编号不可重复");
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
        public void DeleteForm(string keyValue)
        {
            var entity = this.FindEntity(keyValue);
            this.Delete(entity);
        }
    }
}
