using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.Repository
{
    /// <summary>
    /// 门诊输液药品信息
    /// </summary>
    public class MzsyypxxRepo : RepositoryBase<MzsyypxxEntity>, IMzsyypxxRepo
    {
        public MzsyypxxRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public void Exec(List<long> syIds, string OrganizeId)
        {
            if (syIds != null || syIds.Count > 0)
            {
                var successCount = 0;
                syIds.ForEach(p =>
                {
                    var entity = FindEntity(n => n.Id == p);
                    if (entity != null)
                    {
                        entity.yzxcs = entity.yzxcs + 1;
                        entity.Modify();
                        successCount += Update(entity);
                    }
                });
            }
        }

        public void CanCelExec(List<long> syIds, string OrganizeId)
        {
            if (syIds != null || syIds.Count > 0)
            {
                var successCount = 0;
                syIds.ForEach(p =>
                {
                    var entity = FindEntity(n => n.Id == p);
                    if (entity != null)
                    {
                        entity.yzxcs = entity.yzxcs > 0 ? entity.yzxcs - 1 : 0;
                        entity.Modify();
                        successCount += Update(entity);
                    }
                });
            }

        }
    }
}
