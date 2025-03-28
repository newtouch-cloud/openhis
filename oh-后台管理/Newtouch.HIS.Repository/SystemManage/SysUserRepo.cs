using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserRepo : RepositoryBase<SysUserEntity>, ISysUserRepo
    {
        public SysUserRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 返回一个实体
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public SysUserEntity GetEntity(string topOrgId, string account)
        {
            var entity = this.IQueryable().Where(a => a.TopOrganizeId == topOrgId && a.zt == "1" && a.Account == account).FirstOrDefault();
            return entity;
        }

    }
}
