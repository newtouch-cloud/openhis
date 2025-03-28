
using Microsoft.Data.SqlClient;
using SqlSugar;
using System.Data.Common;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Domain
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        ///// <summary>
        ///// 根据ID列表删除
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //Task<bool> DeleteByIds(object[] ids);

        ///// <summary>
        ///// 根据ID删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> DeleteById(string id);

        ///// <summary>
        ///// 添加实体
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //Task<long> Add(TEntity model);

        ///// <summary>
        ///// 更新实体
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>

        //Task<bool> Update(TEntity model);


        Task<dynamic> GetJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);
        Task<dynamic> GetJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);
        

        #region Ado
        /// <summary>
        /// 执行非查询sql语句，返回操作是否成功
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters);
        /// <summary>
        /// 执行查询sql语句，返回查询的结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListBySqlQuery(string sql, params DbParameter[] parameters);
        /// <summary>
        /// 执行查询sql语句，返回 第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        //Task<object> GetScalar(string sql, params SugarParameter[] parameters);

        #endregion

        #region DbMuti Query
        #endregion
    }
}
