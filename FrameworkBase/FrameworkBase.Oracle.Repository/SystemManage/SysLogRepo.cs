using System.Collections.Generic;
using System;
using Newtouch.Core.Common;
using Newtouch.Tools;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;

namespace FrameworkBase.Oracle.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统日志
    /// </summary>
    public sealed class SysLogRepo : RepositoryBase<SysLogEntity>, ISysLogRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysLogRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<SysLogEntity> GetPaginationList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<SysLogEntity>();
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