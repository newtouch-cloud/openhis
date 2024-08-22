using Newtouch.Tools;
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common.Operator;
using Newtouch.Tools.Net;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Application.SystemSecurity
{
    /// <summary>
    /// 
    /// </summary>
    public class LogApp: ILogApp
    {
        private readonly ISysLogRepository _logRepository;

        public LogApp(ISysLogRepository logRepository)
        {
            this._logRepository = logRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<SysLogEntity> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<SysLogEntity>();
            expression = expression.And(t => t.TopOrganizeId == Constants.TopOrganizeId);
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
            return _logRepository.FindList(expression, pagination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keepTime"></param>
        public void RemoveLog(string keepTime)
        {
            DateTime operateTime = DateTime.Now;
            if (keepTime == "7")            //保留近一周
            {
                operateTime = DateTime.Now.AddDays(-7);
            }
            else if (keepTime == "1")       //保留近一个月
            {
                operateTime = DateTime.Now.AddMonths(-1);
            }
            else if (keepTime == "3")       //保留近三个月
            {
                operateTime = DateTime.Now.AddMonths(-3);
            }
            var expression = ExtLinq.True<SysLogEntity>();
            expression = expression.And(t => t.TopOrganizeId == Constants.TopOrganizeId);
            expression = expression.And(t => t.Date <= operateTime);
            _logRepository.Delete(expression);
        }

    }
}
