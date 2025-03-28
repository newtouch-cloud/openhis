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
    /// 收费项目
    /// </summary>
    public class SysChargeItemBaseRepo : RepositoryBase<SysChargeItemBaseEntity>, ISysChargeItemBaseRepo
    {
        public SysChargeItemBaseRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeItemBaseEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysChargeItemBaseEntity> GetList(string keyValue, string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId).OrderBy(a => a.px).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeItemBaseEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.sfxmCode == entity.sfxmCode && p.sfxmId != keyValue.Value && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }

                SysChargeItemBaseEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.sfxmId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysChargeItemEntity.GetTableName(), oldEntity.sfxmId.ToString());
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.sfxmCode == entity.sfxmCode && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 医保同步修改最后上传时间
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        public bool YibaoUpload(int sfxmId, out string error)
        {
            try
            {
                var ypsxEntity = this.IQueryable().Where(p => p.sfxmId == sfxmId).FirstOrDefault();
                if (ypsxEntity == null)
                {
                    error = "HIS中查无此收费项目";
                    return false;
                }
                ypsxEntity.LastYBUploadTime = System.DateTime.Now;
                this.Update(ypsxEntity);
                error = "";
                return true;
            }
            catch
            {
                error = "HIS更新药品同步医保时间失败";
                return false;
            }
        }
    }
}
