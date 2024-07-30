using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class MzZyBarCodeRepo : RepositoryBase<MzZyBarCodeEntity>, IMzZyBarCodeRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public MzZyBarCodeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(MzZyBarCodeEntity entity)
        {
            try
            {
                entity.Create(true);
                this.Insert(entity);
            }
            catch (Exception er) { }
        }
    }
}
