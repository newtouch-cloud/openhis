using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class jgss_AttachmentInfoRepo : RepositoryBase<jgss_AttachmentInfoEntity>, Ijgss_AttachmentInfoRepo
    {
        public jgss_AttachmentInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(jgss_AttachmentInfoEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                jgss_AttachmentInfoEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}


