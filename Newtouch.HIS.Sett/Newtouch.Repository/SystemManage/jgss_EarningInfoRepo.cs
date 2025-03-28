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
    public class jgss_EarningInfoRepo : RepositoryBase<jgss_EarningInfoEntity>, Ijgss_EarningInfoRepo
    {
        public jgss_EarningInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 更新审核状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shzt"></param>
        public void updateshzt(string id,string shzt,string shr = null) {
           
            var entity = this.FindEntity(id);
            entity.shzt = shzt;
            entity.shr = shr;
            entity.shsj = DateTime.Now;
            this.Update(entity, new List<string>() { "shzt", "shr", "shsj" });
        }

    }
}


