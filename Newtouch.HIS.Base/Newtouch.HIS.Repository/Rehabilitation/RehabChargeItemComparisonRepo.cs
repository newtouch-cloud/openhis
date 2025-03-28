using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RehabChargeItemComparisonRepo : RepositoryBase<RehabChargeItemComparisonEntity>, IRehabChargeItemComparisonRepo
    {
        public RehabChargeItemComparisonRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SubmitForm(RehabChargeItemComparisonEntity entity, string sfxmdzId)
        {
            if (!string.IsNullOrEmpty(sfxmdzId))
            {
                entity.Modify(sfxmdzId);
                this.Update(entity);
            }
            else
            {
                entity.Create(true, System.Guid.NewGuid());
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string sfxmdzId, string OrganizeId)
        {
            this.Delete(a => a.sfxmdzId == sfxmdzId && a.OrganizeId == OrganizeId);
        }


    }
}
