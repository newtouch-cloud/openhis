using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository.OutpatientManage
{
    public class OutpatientRegistScheduleRepo : RepositoryBase<OutpatientRegistScheduleEntity>, IOutpatientRegistScheduleRepo
    {
        public OutpatientRegistScheduleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ghpbId"></param>
        public void SubmitForm(OutpatientRegistScheduleEntity entity, int? ghpbId)
        {
            if (ghpbId > 0)
            {
                entity.Modify(ghpbId);
                this.Update(entity);
            }
            else
            {
                entity.Create(true, EFDBBaseFuncHelper.GetInstance().GetNewPrimaryKeyInt(OutpatientRegistScheduleEntity.GetTableName()));
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int ghpbId, string orgId)
        {
            this.Delete(a => a.ghpbId == ghpbId && a.OrganizeId == orgId);
        }

    }
}
