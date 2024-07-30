using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysWardBedRepo : RepositoryBase<SysWardBedEntity>, ISysWardBedRepo
    {
        public SysWardBedRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SubmitForm(SysWardBedEntity entity, int? cwId)
        {
            if (cwId != null && cwId > 0)
            {
                //Code重复判断
                if (this.IQueryable().Any(a => a.cwCode == entity.cwCode && a.cwId != cwId && a.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }

                SysWardBedEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(cwId.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.cwId = cwId.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysWardBedEntity.GetTableName(), oldEntity.cwId.ToString());
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(a => a.cwCode == entity.cwCode && a.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteForm(int cwId, string orgId)
        {
            this.Delete(a => a.cwId == cwId && a.OrganizeId == orgId);
        }

        /// <summary>
        /// 根据床位占用情况
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="sfzy"></param>
        public void UpdateOccupyByCode(string code, string orgId, bool sfzy)
        {
            var entity = this.IQueryable(p => p.cwCode == code && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
            if (entity != null)
            {
                entity.sfzy = sfzy;
                entity.Modify();
                this.Update(entity);
            }
        }

    }
}
