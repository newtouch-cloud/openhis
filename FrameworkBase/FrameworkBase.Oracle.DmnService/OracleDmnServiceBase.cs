using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FrameworkBase.Oracle.DmnService
{
    /// <summary>
    /// DmnService基类
    /// </summary>
    public abstract class DmnServiceBase : EFDBBase
    {
        /// <summary>
        /// 是否为private readonly I 解析实例
        /// </summary>
        private static bool? _PRFAutoResolve = null;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DmnServiceBase()
        {
            _PRFAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve_DmnService");
            if (_PRFAutoResolve == null)
            {
                _PRFAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public DmnServiceBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            if (_PRFAutoResolve ?? true)
            {
                //利用IOC为private readonly field赋值
                this.PRFAutoResolve();
            }
        }

        #region override/new 覆盖基类的方法

        /// <summary>
        /// 重写支持Oracle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pagination"></param>
        /// <param name="parameters"></param>
        /// <param name="countHighPerformance"></param>
        /// <param name="countSql"></param>
        /// <returns></returns>
        public override IList<T> QueryWithPage<T>(string sql, Pagination pagination, DbParameter[] parameters = null, bool countHighPerformance = true, string countSql = null)
        {
            if (parameters == null)
            {
                return QueryWithPage<T>(sql, pagination, countHighPerformance: countHighPerformance, countSql: countSql, parameters: new DbParameter[0]);
            }

            var logger = DependencyDefaultInstanceResolver.GetInstance<Newtouch.Core.Common.Interface.ILogger>();
            try
            {
                var st = new Stopwatch();
                st.Start();

                if (string.IsNullOrWhiteSpace(pagination.sidx))
                {
                    throw new Exception("pagination.sidx is required");
                }
                var sortby = pagination.sidx;
                if (!string.IsNullOrWhiteSpace(pagination.sord) && pagination.sord.ToUpper() != "ASC")
                {
                    if (!sortby.Contains(",") && !sortby.Contains(" "))
                    {
                        sortby += " " + pagination.sord;
                    }
                }

                IList<T> list = null;

                #region 组装sql查询语句
                int startIndex = (pagination.page - 1) * pagination.rows + 1;
                int endIndex = pagination.page * pagination.rows;
                string pagedSql = string.Format("with queryData as ({0}) " +
                "SELECT * FROM (SELECT * " +
                " FROM (SELECT ROW_NUMBER() OVER ( ORDER BY JJ.{1}) AS RN, JJ.* " +
                " FROM queryData JJ" +
                " ) " +
                "WHERE RN <= {2} ) TB " +
                "WHERE TB.RN >= {3} " +
                "ORDER BY TB.RN", sql, getSordBy(sortby), endIndex, startIndex);

                list = _dataContext.Database.SqlQuery<T>(pagedSql, parameters).ToList();
                #endregion

                //总数sql
                string cntSql = null;
                if (!string.IsNullOrWhiteSpace(countSql))
                {
                    cntSql = countSql;
                }
                if (string.IsNullOrWhiteSpace(cntSql))
                {
                    cntSql = string.Format("with queryData as ({0}) select count(1) from queryData", sql);
                }

                if (parameters.Count() > 0)
                {
                    var cloneSqlParameterList = new DbParameter[parameters.Count()];
                    for (int i = 0; i < parameters.Count(); i++)
                    {
                        DbParameter pp = (DbParameter)((ICloneable)parameters[i]).Clone();
                        cloneSqlParameterList[i] = pp;
                    }

                    pagination.records = _dataContext.Database.SqlQuery<int>(cntSql, cloneSqlParameterList).FirstOrDefault();
                }
                else
                {
                    pagination.records = _dataContext.Database.SqlQuery<int>(cntSql).FirstOrDefault();
                }

                if (logger != null)
                {
                    //logger.InfoFormat("执行base.QueryWithPage，Sql片段：{0}，耗时：{1}ms", sql.Length > 60 ? sql.Substring(0, 60) : sql, st.ElapsedMilliseconds);
                }

                return list;
            }
            catch (Exception ex)
            {
                if (logger != null)
                {
                    logger.InfoFormat("执行base.QueryWithPage，Sql片段：{0}，Sql执行异常，参数：{1}", sql.Length > 60 ? sql.Substring(0, 60) : sql
                        , SerializeExecutedDbParameters(parameters));
                }
                throw ex;   //全局会记
            }
        }

        /// <summary>
        /// 获取sql参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string SerializeExecutedDbParameters(DbParameter[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return null;
            }
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var par in parameters)
            {
                sb.Append("{");
                sb.Append(par.ParameterName);
                sb.Append(":");
                sb.Append(par.Value == null ? "null" : par.Value.ToString());
                sb.Append("}");
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);    //删除最后一个逗号
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// 获取sql参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string SerializeExecutedSqlParameters(SqlParameter[] parameters)
        {
            return SerializeExecutedDbParameters(parameters);
        }

        /// <summary>
        /// 组装sord by 
        /// </summary>
        /// <param name="sord"></param>
        /// <returns></returns>
        private string getSordBy(string sord)
        {
            if (string.IsNullOrWhiteSpace(sord))
                return "";
            else {
                string sordbyStr = string.Empty;
                string[] array = sord.Split(',');
                foreach (var item in array)
                {
                    string[] itemArray = item.Split(' ');
                    sordbyStr += string.Format(" \"{0}\" {1},", itemArray[0], itemArray.Length <= 1? "":itemArray[1]);
                }
                if (sordbyStr.Length > 0) {
                    sordbyStr = sordbyStr.Substring(0, sordbyStr.Length - 1);
                }
                return sordbyStr;
            }
        }
        #endregion

    }
}
