using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class GuianXnhS04InfoRepo : RepositoryBase<GuianXnhS04InfoEntity>, IGuianXnhS04InfoRepo
    {
        public GuianXnhS04InfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
       public void SubmitForm(GuianXnhS04InfoEntity Entity, string keyValue) {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                Entity.Modify(keyValue);
                Update(Entity);
            }
            else
            {
                Entity.Create(true, System.Guid.NewGuid().ToString());
                this.Insert(Entity);
            }
        }
    }
}
