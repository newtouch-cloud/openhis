using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 目录对照主表
    /// </summary>
    public class TTCataloguesComparisonMainRepo : RepositoryBase<TTCataloguesComparisonMainEntity>, ITTCataloguesComparisonMainRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public TTCataloguesComparisonMainRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取所有对照目录
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <returns></returns>
        public IList<TTCataloguesComparisonMainEntity> GetList(string orgId)
        {
            return this.IQueryable(p => p.OrganizeId == orgId).OrderBy(p => p.Code).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void SubmitForm(string keyValue, TTCataloguesComparisonMainEntity entity)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Modify(keyValue);
                this.Update(entity);
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
