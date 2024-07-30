using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 出入库单据
    /// </summary>
    public class KfCrkdjRepo : RepositoryBase<KfCrkdjEntity>, IKfCrkdjRepo
    {
        public KfCrkdjRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        
        /// <summary>
        /// 打回申请 从待处理变成暂存，只针对外部入库
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        public string BackToTemporary(long crkId)
        {
            var entity = FindEntity(p => p.Id == crkId);
            if (entity == null) return "未找到指定单据";
            if (entity.auditState.Equals(((int)EnumAuditState.Waiting).ToString()))
            {
                entity.auditState = ((int)EnumAuditState.Temporary).ToString();
                entity.Modify();
                return Update(entity) > 0 ? "" : "修改审核状态失败";
            }
            return "该操作只针对待入库的单据";
        }
    }
}
