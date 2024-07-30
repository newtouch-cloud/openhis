using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// DmnService base
    /// </summary>
    public class DmnServiceBase : EFDBBase
    {
        /// <summary>
        /// 是否为private readonly I 解析实例
        /// </summary>
        private static readonly bool? PrfAutoResolve;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DmnServiceBase()
        {
            PrfAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve_DmnService") ?? ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve");
        }

        /// <summary>
        /// 构造函数（在这里给prf ioc赋值的）
        /// </summary>
        /// <param name="databaseFactory"></param>
        public DmnServiceBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            if (PrfAutoResolve ?? true)
            {
                //利用IOC为private readonly field赋值
                this.PRFAutoResolve();
            }
        }

        /// <summary>
        /// 构造函数（在这里给prf ioc赋值的）
        /// </summary>
        /// <param name="databaseFactory"></param>
        /// <param name="needIoc"></param>
        public DmnServiceBase(IDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory)
        {
            if (needIoc)
            {
                //利用IOC为private readonly field赋值
                this.PRFAutoResolve();
            }
        }

        #region no log

        /// <summary>
        /// 查询Sql，返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T FirstOrDefaultNoLog<T>(string sql)
        {
            return _dataContext.Database.SqlQuery<T>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 查询Sql，返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T FirstOrDefaultNoLog<T>(string sql, DbParameter[] parameters)
        {
            if (parameters == null)
            {
                return FirstOrDefaultNoLog<T>(sql);
            }
            return _dataContext.Database.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        /// <summary>
        /// 对数据库执行给定的 DDL/DML 命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommandNoLog(string sql, params DbParameter[] parameters)
        {
            int result;
            result = parameters == null ? _dataContext.Database.ExecuteSqlCommand(sql) : _dataContext.Database.ExecuteSqlCommand(sql, parameters);
            return result;
        }

        /// <summary>
        /// 查询Sql，返回实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> FindListNoLog<T>(string sql)
        {
            var result = _dataContext.Database.SqlQuery<T>(sql).ToList<T>();
            return result;
        }

        /// <summary>
        /// 查询Sql，返回实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> FindListNoLog<T>(string sql, DbParameter[] parameters)
        {
            if (parameters == null)
            {
                return FindListNoLog<T>(sql);
            }
            return _dataContext.Database.SqlQuery<T>(sql, parameters).ToList();
        }
        #endregion
    }
}