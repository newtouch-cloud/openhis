
using SqlSugar;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// 基类接口,其他接口继承该接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region TEntity 操作
        /// <summary>
        /// 根据ID列表查询
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<TEntity> FindKey(object pkValue);

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAll();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> Add(TEntity model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Update(TEntity model);

        /// <summary>
        /// 根据id列表删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteByIds(object[] ids);

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);

        #region Query

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> FindAll(bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 取前 num 条数据
        /// </summary>
        /// <param name="num">取前几条</param>
        /// <returns></returns>
        public Task<List<TEntity>> getTakeList(int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 获取单表 分页数据
        /// </summary>
        /// <param name="skip">跳过几条</param>
        /// <param name="take">取几条</param>
        /// <param name="whereExp">跳过几条</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public Task<List<TEntity>> getPageList(int skip, int take, Expression<Func<TEntity, bool>> whereExp, Expression<Func<TEntity, object>> orderBy, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 获取符合条件的前 num 条数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="num">取前几条</param>
        /// <returns></returns> 
        public Task<List<TEntity>> getTakeList(Expression<Func<TEntity, bool>> where, int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据条件获取 单条数据 
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public Task<TEntity> getFirstOrDefault(Expression<Func<TEntity, bool>> where);


        /// <summary>
        /// 根据主键 In  查询
        /// </summary>
        /// <typeparam name="S">主键的类型</typeparam>
        /// <param name="list">主键 In 操作的结果集</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据指定列 In 查询
        /// </summary>
        /// <typeparam name="S">指定列的类型</typeparam>
        /// <param name="column">指定列</param>
        /// <param name="list">指定列 In 操作 的结果集</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByIn<S>(Expression<Func<TEntity, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据指定列 Not In (!Contain)查询
        /// </summary>
        /// <typeparam name="S">指定列类型</typeparam>
        /// <param name="list">Not In的结果集</param>
        /// <param name="field">指定列</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据条件 查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhere(Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 单个条件 根据 isWhere 判断 是否使用此条件进行查询
        /// </summary>
        /// <param name="isWhere">判断是否使用此查询条件的条件</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhereIF(bool isWhere, Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 多个条件 根据 wheres.value  判断是否使用 此 wheres.key 的条件
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByWhereIF(Dictionary<Expression<Func<TEntity, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 查询 指定列的值 在 start至end 之间的数据
        /// </summary>
        /// <param name="value">指定类</param>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <returns></returns>
        public Task<List<TEntity>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 判断是否存在这条记录
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public Task<bool> getIsAny(Expression<Func<TEntity, bool>> where);


        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="TEntity">要查询的表</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面容量</param>
        /// <param name="isWhere">是否需要条件查询</param>
        /// <param name="whereExp">查询条件</param>
        /// <param name="isOrderBy">是否需要排序条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public Task<List<TEntity>> getPageList<TEntity>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<TEntity, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 两表查询，
        /// </summary>
        /// <typeparam name="T1">左表</typeparam>
        /// <typeparam name="T2">右表</typeparam>
        /// <param name="joinExp">联表方式，联表字段（主外键关系）</param>
        /// <param name="selectExp">联表查询的结果</param>
        /// <param name="isWhere">是否需要查询条件</param>
        /// <param name="whereExp">条件查询</param>
        /// <param name="isOrderBy">是否需要排序</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public Task<List<object>> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 两表 分页查询，
        /// </summary>
        /// <typeparam name="T1">左表</typeparam>
        /// <typeparam name="T2">右表</typeparam>
        /// <param name="joinExp">联表方式，联表字段（主外键关系）</param>
        /// <param name="selectExp">联表查询的结果</param>
        /// <param name="isWhere">是否需要查询条件</param>
        /// <param name="whereExp">条件查询</param>
        /// <param name="isOrderBy">是否需要排序</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public Task<dynamic> getJoinPageList<T1, T2>(int pageIndex, int pageSize, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 三表连接 查询
        /// </summary>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <param name="joinExp">联表方式，联表字段（主外键关系）</param>
        /// <param name="selectExp">联表查询的结果</param>
        /// <param name="isWhere">是否需要查询条件</param>
        /// <param name="whereExp">查询条件</param>
        /// <param name="isOrderBy">是否需要排序条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public Task<dynamic> getJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 三表连接 分页 查询
        /// </summary>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <param name="joinExp">联表方式，联表字段（主外键关系）</param>
        /// <param name="selectExp">联表查询的结果</param>
        /// <param name="isWhere">是否需要查询条件</param>
        /// <param name="whereExp">查询条件</param>
        /// <param name="isOrderBy">是否需要排序条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public Task<dynamic> getJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 执行查询sql语句 ，返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public Task<List<TEntity>> getListBySql(string sql);


        /// <summary>
        /// 执行非查询sql语句，返回操作是否成功
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters);


        /// <summary>
        /// 执行查询sql语句，返回查询的结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public Task<List<TEntity>> getListBySqlQuery(string sql, params SugarParameter[] parameters);


        /// <summary>
        /// 执行查询sql语句，返回 第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public Task<object> getScalar(string sql, params SugarParameter[] parameters);



        #endregion
        #endregion

        #region DbMuti Query
        /// <summary>
        /// LINQ Where (choose db)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="where"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        Task<List<T>> getByWhere<T>(string db, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);
        #region ADO 
        /// <summary>
        /// SQL (choose db,choose T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<T>> getListBySqlQuery<T>(string db, string sql, List<DbParameter> parameters);
        #endregion
        /// <summary>
        /// LINQ两表联查 (choose db)
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="db"></param>
        /// <param name="joinExp"></param>
        /// <param name="selectExp"></param>
        /// <param name="isWhere"></param>
        /// <param name="whereExp"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        Task<List<object>> getJoinList<T1, T2>(string db, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);
        #endregion
    }
}

