using System;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Tools;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// SysLog系统日志
    /// </summary>
    public sealed class SysLogRepo : RepositoryBase<SysLogEntity>, ISysLogRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysLogRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// SysLog日志查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<SysLogEntity> GetPaginationList(Pagination pagination, string orgId, string queryJson)
        {
            var expression = ExtLinq.True<SysLogEntity>();
            expression = expression.And(t => t.TopOrganizeId == ConstantsBase.TopOrganizeId);
            if (!string.IsNullOrEmpty(orgId) && orgId != ConstantsBase.TopOrganizeId)
            {
                expression = expression.And(t => t.OrganizeId == orgId);
            }
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.Account.Contains(keyword));
            }
            if (!queryParam["timeType"].IsEmpty())
            {
                string timeType = queryParam["timeType"].ToString();
                DateTime startTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate();
                DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate().AddDays(1);
                switch (timeType)
                {
                    case "1":
                        break;
                    case "2":
                        startTime = DateTime.Now.AddDays(-7);
                        break;
                    case "3":
                        startTime = DateTime.Now.AddMonths(-1);
                        break;
                    case "4":
                        startTime = DateTime.Now.AddMonths(-3);
                        break;
                    default:
                        break;
                }
                expression = expression.And(t => t.Date >= startTime && t.Date <= endTime);
            }
            return this.FindList(expression, pagination);
        }
    }
}
