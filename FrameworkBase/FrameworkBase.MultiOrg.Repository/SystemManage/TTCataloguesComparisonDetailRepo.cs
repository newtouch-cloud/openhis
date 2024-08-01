using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 目录对照子表
    /// </summary>
    public class TTCataloguesComparisonDetailRepo : RepositoryBase<TTCataloguesComparisonDetailEntity>, ITTCataloguesComparisonDetailRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public TTCataloguesComparisonDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mainId"></param>
        /// <returns></returns>
        public IList<TTCataloguesComparisonDetailEntity> GetListByMainId(string keyword, string mainId)
        {
            return this.IQueryable(p => p.MainId == mainId).OrderBy(p => p.Code).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void SubmitForm(string keyValue, TTCataloguesComparisonDetailEntity entity)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.MainId == entity.MainId && p.Id != keyValue))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.MainId == entity.MainId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }



    }
}
