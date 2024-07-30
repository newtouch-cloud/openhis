using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.Repository
{
    /// <summary>
    /// 创 建：周珏琦
    /// 日 期：2019-01-28 13:27
    /// 描 述：手术安排表
    /// </summary>
    public class InpatientOperationArrangementRepo : RepositoryBase<InpatientOperationArrangementEntity>, IInpatientOperationArrangementRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public InpatientOperationArrangementRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public bool SubmitForm(InpatientOperationArrangementEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity != null)
                {
                    //properties
                    dbEntity.aprq = entity.aprq;
                    dbEntity.ssAddr = entity.ssAddr;
                    dbEntity.urgent = entity.urgent;
                    dbEntity.surgeonId = entity.surgeonId;
                    dbEntity.surgeonName = entity.surgeonName;
                    dbEntity.assistant = entity.assistant;
                    dbEntity.assistantName = entity.assistantName;
                    dbEntity.anesthesiaType = entity.anesthesiaType;
                    dbEntity.remark = entity.remark;
                    dbEntity.zt = entity.zt;
                    dbEntity.Modify(keyValue);
                    return this.Update(dbEntity) > 0;
                }
            }
            else
            {
                entity.Create(true);
                return this.Insert(entity) > 0;
            }
            return false;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}