using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectionCategoryRepo : RepositoryBase<InspectionCategoryEntity>, IInspectionCategoryRepo
    {
        public InspectionCategoryRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<InspectionCategoryEntity> GetListByOrg(string orgId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(InspectionCategoryEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.dlCode == entity.dlCode && p.Id != keyValue))
                {
                    throw new FailedException("编号不可重复");
                }
                
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.dlCode == entity.dlCode))
                {
                    throw new FailedException("编号不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
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
