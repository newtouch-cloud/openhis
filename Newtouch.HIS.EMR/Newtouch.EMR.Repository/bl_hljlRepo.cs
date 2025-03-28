using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using System;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-08-30 11:31
    /// 描 述：住院患者信息
    /// </summary>
    public class bl_hljlRepo : RepositoryBase<bl_hljlEntity>, Ibl_hljlRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public bl_hljlRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }


        public bl_hljlEntity bl_hljlGetByID(string ID)
        {

            return this.FindEntity(p => p.Id == ID);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(bl_hljlEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.IsLock = entity.IsLock;
                dbEntity.blxtmc_hj = entity.blxtmc_hj;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.IsLock = 0;
                entity.Create(true);
                this.Insert(entity);
            }
        }
    }
}