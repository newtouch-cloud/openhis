using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Linq;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-09-20 14:48
    /// 描 述：模板权限控制表
    /// </summary>
    public class BlmbqxkzRepo : RepositoryBase<BlmbqxkzEntity>, IBlmbqxkzRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public BlmbqxkzRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(BlmbqxkzEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                if (dbEntity != null)
                {
                    dbEntity.ctrlLevel = entity.ctrlLevel;
                    dbEntity.ctrlLevelDesc = entity.ctrlLevelDesc;
                    dbEntity.dutyCode = entity.dutyCode;
                    dbEntity.dutyName = entity.dutyName;
                    dbEntity.zt = entity.zt;
                    dbEntity.Modify(keyValue);
                    this.Update(dbEntity);
                }
                else
                {
                    entity.Create(true);
                    this.Insert(entity);
                }
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
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        public void UpdateCtrlLevelByMbId(string mbId, BlmbqxkzEntity entity)
        {
            if (entity == null )
            {
                return;
            }

            var list = this.IQueryable().Where(p => p.mbId == mbId).OrderBy(p => p.CreateTime).ToList();

            foreach(var dbEntity in list)
            {
                if(dbEntity.dutyCode == "Doctor" || dbEntity.dutyCode == "Nurse")
                {
                    continue;
                }

                dbEntity.ctrlLevel = entity.ctrlLevel;
                dbEntity.ctrlLevelDesc = entity.ctrlLevelDesc;
                 this.Update(dbEntity);
            }
        }
    }
}