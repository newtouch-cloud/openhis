using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.Inpatient
{
    public class QhdZnshSqtxRepo : RepositoryBase<QhdZnshSqtxEntity>, IQhdZnshSqtxRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public QhdZnshSqtxRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(QhdZnshSqtxEntity entity,out string rzid, string keyValue=null)
        {
            var keyId = "";
            try {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var dbEntity = this.FindEntity(keyValue);
                    dbEntity.XmlResponse = entity.XmlResponse;
                    dbEntity.Modify(keyValue);
                    this.Update(dbEntity);
                    keyId = dbEntity.Id;
                }
                else
                {
                    entity.Create(true);
                    this.Insert(entity);
                    keyId = entity.Id;
                }
               
            }
            catch (Exception er) { }
            rzid = keyId;
        }
    }
}
