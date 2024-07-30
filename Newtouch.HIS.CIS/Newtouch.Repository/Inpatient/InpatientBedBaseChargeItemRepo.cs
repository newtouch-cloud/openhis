using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    public class InpatientBedLevelChargeItemRepo : RepositoryBase<InpatientBedLevelChargeItemEntity>, IInpatientBedLevelChargeItemRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public InpatientBedLevelChargeItemRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(InpatientBedLevelChargeItemEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string levelCode,string orgId)
        {
            this.Delete(p => p.Id == levelCode && p.zt=="1"&&p.OrganizeId==orgId);
        }

    }
}