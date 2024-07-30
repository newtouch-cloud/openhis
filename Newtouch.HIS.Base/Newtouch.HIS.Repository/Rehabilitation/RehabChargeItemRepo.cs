using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RehabChargeItemRepo : RepositoryBase<RehabChargeItemEntity>, IRehabChargeItemRepo
    {
        public RehabChargeItemRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        public void SubmitForm(RehabChargeItemEntity entity, string sfxmId)
        {
            if (!string.IsNullOrEmpty(sfxmId))
            {
                //Code重复判断
                if (this.IQueryable().Any(a => a.Code == entity.Code && a.sfxmId != sfxmId))
                {
                    throw new FailedException("编码不可重复");
                }

                RehabChargeItemEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(sfxmId);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.sfxmId = sfxmId;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, RehabChargeItemEntity.GetTableName(), oldEntity.sfxmId.ToString());
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(a => a.Code == entity.Code))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true, System.Guid.NewGuid());
                this.Insert(entity);

            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string sfxmId, string OrganizeId)
        {
            this.Delete(a => a.sfxmId == sfxmId && a.OrganizeId == OrganizeId);
        }


    }
}
