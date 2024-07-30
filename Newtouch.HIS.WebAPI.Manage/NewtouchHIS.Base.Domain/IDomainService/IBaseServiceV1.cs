using SqlSugar;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Domain
{
    public interface IBaseServiceV1<T> where T : class, new()
    {
        #region Add
        /// <summary>
        /// 增加单条数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>操作是否成功</returns>
        public Task<bool> Add(T model);

        /// <summary>
        /// 增加多条数据
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns>操作是否成功</returns>
        public Task<bool> AddRange(List<T> list);


        /// <summary>
        /// 添加单条数据，并返回 自增列
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public Task<bool> AddReturnIdentity(T model);

        /// <summary>
        /// 增加单条数据 ，并返回 实体
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public Task<T> AddReturnEntity(T model);


        /// <summary>
        /// 只添加指定列
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">指定要添加的列</param>
        /// <returns></returns>
        public Task<bool> AddColumns(T model, params string[] columns);


        /// <summary>
        /// 不插入指定列
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="IgnoreColumns">要忽略的列</param>
        /// <returns></returns>
        public Task<bool> AddColumnsByIgnoreColumns(T model, params string[] IgnoreColumns);


        #endregion

        #region Delete

        /// <summary>
        /// 根据主键删除，并返回操作是否成功
        /// </summary>
        /// <typeparam name="S">主键的类型</typeparam>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public Task<bool> Delete<S>(S key);


        /// <summary>
        /// 根据主键删除，并返回操作是否成功
        /// </summary>
        /// <typeparam name="S">主键类型</typeparam>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public Task<bool> DeleteRange<S>(params S[] keys);


        /// <summary>
        /// 根据条件删除，并返回操作是否成功
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public Task<bool> DeleteWhere(Expression<Func<T, bool>> where);


        #endregion

        #region Update

        /// <summary>
        /// 根据主键更新 ，返回操作是否成功
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> Update(T model);


        /// <summary>
        /// 根据主键更新，返回操作是否成功
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns></returns>
        public Task<bool> UpdateRange(List<T> list);


        /// <summary>
        /// 根据指定 列条件 更新 ，并返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="expression">列条件 例如： t=>t.id>5 </param>
        public Task<bool> Update(T model, Expression<Func<T, object>> expression);


        /// <summary>
        /// 根据主键，更新指定列,返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">要更新的列</param>
        /// <returns></returns>
        public Task<bool> UpdateColumns(T model, params string[] columns);


        /// <summary>
        /// 根据主键，更新指定列,返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">要更新的列</param>
        /// <returns></returns>
        public Task<bool> UpdateColumns(T model, Expression<Func<T, object>> columns);


        /// <summary>
        /// 根据主键 ， 忽略更新指定列，返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">不更新的 忽略列</param>
        /// <returns></returns>
        public Task<bool> UpdateColumnsByIgnoreColumns(T model, params string[] columns);


        /// <summary>
        /// 根据主键 ， 忽略更新指定列，返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">不更新的 忽略列</param>
        /// <returns></returns>
        public Task<bool> UpdateColumnsByIgnoreColumns(T model, Expression<Func<T, object>> columns);

        /// <summary>
        /// 根据主键更新 列
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="ignoreAllNullColumns">是否 NULL的列不更新</param>
        /// <param name="isOffIdentity">是否忽略 自增列</param>
        /// <param name="ignoreAllDefaultValue">是否 忽略默认值列</param>
        /// <returns></returns>
        public Task<bool> UpdateNotNullColumns(T model, bool ignoreAllNullColumns, bool isOffIdentity = false, bool ignoreAllDefaultValue = false);


        //4.6.0.7 联表更新

        /// <summary>
        /// //根据不同条件执行更新不同的列 
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="dic">条件（key：要更新的列，value：条件是否更新此列）</param>
        /// <returns></returns>
        public Task<bool> UpdateIF(T model, Dictionary<Expression<Func<T, object>>, bool> dic);


        #endregion

        #region Query

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> FindAll(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 取前 num 条数据
        /// </summary>
        /// <param name="num">取前几条</param>
        /// <returns></returns>
        public Task<List<T>> getTakeList(int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 获取单表 分页数据
        /// </summary>
        /// <param name="skip">跳过几条</param>
        /// <param name="take">取几条</param>
        /// <param name="whereExp">跳过几条</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public Task<List<T>> getPageList(int skip, int take, Expression<Func<T, bool>> whereExp, Expression<Func<T, object>> orderBy, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 获取符合条件的前 num 条数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="num">取前几条</param>
        /// <returns></returns> 
        public Task<List<T>> getTakeList(Expression<Func<T, bool>> where, int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="pkValue">主键</param>
        /// <returns></returns>
        public Task<T> getByPrimaryKey(object pkValue);


        /// <summary>
        /// 根据条件获取 单条数据 
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public Task<T> getFirstOrDefault(Expression<Func<T, bool>> where);


        /// <summary>
        /// 根据主键 In  查询
        /// </summary>
        /// <typeparam name="S">主键的类型</typeparam>
        /// <param name="list">主键 In 操作的结果集</param>
        /// <returns></returns>
        public Task<List<T>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据指定列 In 查询
        /// </summary>
        /// <typeparam name="S">指定列的类型</typeparam>
        /// <param name="column">指定列</param>
        /// <param name="list">指定列 In 操作 的结果集</param>
        /// <returns></returns>
        public Task<List<T>> getByIn<S>(Expression<Func<T, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据指定列 Not In (!Contain)查询
        /// </summary>
        /// <typeparam name="S">指定列类型</typeparam>
        /// <param name="list">Not In的结果集</param>
        /// <param name="field">指定列</param>
        /// <returns></returns>
        public Task<List<T>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据条件 查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public Task<List<T>> getByWhere(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 单个条件 根据 isWhere 判断 是否使用此条件进行查询
        /// </summary>
        /// <param name="isWhere">判断是否使用此查询条件的条件</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public Task<List<T>> getByWhereIF(bool isWhere, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 多个条件 根据 wheres.value  判断是否使用 此 wheres.key 的条件
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns></returns>
        public Task<List<T>> getByWhereIF(Dictionary<Expression<Func<T, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 查询 指定列的值 在 start至end 之间的数据
        /// </summary>
        /// <param name="value">指定类</param>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <returns></returns>
        public Task<List<T>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 判断是否存在这条记录
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public Task<bool> getIsAny(Expression<Func<T, bool>> where);


        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="T">要查询的表</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面容量</param>
        /// <param name="isWhere">是否需要条件查询</param>
        /// <param name="whereExp">查询条件</param>
        /// <param name="isOrderBy">是否需要排序条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public Task<List<T>> getPageList<T>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<T, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<dynamic> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc);


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
        public Task<List<T>> getListBySql(string sql);


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
        public Task<List<T>> getListBySqlQuery(string sql, params SugarParameter[] parameters);


        /// <summary>
        /// 执行查询sql语句，返回 第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public Task<object> getScalar(string sql, params SugarParameter[] parameters);



        #endregion

        #region Other


        ///// <summary>
        ///// 使用存储过程，返回结果集
        ///// </summary>
        ///// <param name="procedureName">存储过程名称 </param>
        ///// <param name="parameters">参数，支持 output</param>
        ///// <returns></returns>
        //public Task<DataTable> UseStoredProcedure(string procedureName, params SugarParameter[] parameters);


        ///// <summary>
        ///// 使用事务 ，无返回值
        ///// </summary>
        ///// <param name="action">执行动作</param>
        ///// <param name="errorCallBack">错误回调</param>
        //public Task<DbResult<bool>> UseTran(Action action, Action<Exception> errorCallBack);


        ///// <summary>
        ///// 使用事务，有返回值
        ///// </summary>
        ///// <typeparam name="S">返回值类型</typeparam>
        ///// <param name="func">执行动作</param>
        ///// <param name="errorCallBack">错误回调</param>
        //public Task<DbResult<S>> UseTran<S>(Func<S> func, Action<Exception> errorCallBack);


        #endregion
    }
}
