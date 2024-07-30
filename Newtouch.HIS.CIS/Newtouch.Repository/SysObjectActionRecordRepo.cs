using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.Repository
{
    /// <summary>
    /// 通用对象操作记录
    /// </summary>
    public class SysObjectActionRecordRepo : RepositoryBase<SysObjectActionRecordEntity>, ISysObjectActionRecordRepo
    {
        public SysObjectActionRecordRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 新增操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Add(SysObjectActionRecordEntity entity)
        {
            entity.Create(true);

            this.Insert(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectKey"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysObjectActionRecordVO> GetListByKey(string objectKey, string orgId)
        {
            var sql = @"select a. objectType,  a.actionType, a.objectKey, a.zhczsj, b.result
from 
(
select objectType, actionType, objectKey
, max(CreateTime) zhczsj
from xt_dxczjl(nolock)
where OrganizeId = @orgId and zt = '1' and objectKey = @objectKey
group by objectType, actionType, objectKey
) a
left join xt_dxczjl(nolock) b
on a.objectType = b.objectType and a.actionType = b.actionType
and a.objectKey = b.objectKey and a.zhczsj = b.CreateTime and b.OrganizeId = @orgId
and b.zt = '1'";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@objectKey", objectKey));
            return this.FindList<SysObjectActionRecordVO>(sql, pars.ToArray());
        }

    }
}
