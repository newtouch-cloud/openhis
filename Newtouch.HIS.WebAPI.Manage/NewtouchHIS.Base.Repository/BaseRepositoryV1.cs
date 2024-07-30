using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System.Data;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.Repository
{
    public class BaseRepositoryV1<T> :IBaseRepositoryV1<T> where T : class, new()
    {
        public SqlSugarClient context;
        public BaseRepositoryV1(SqlSugarClient db)
        {
            context = db;
        }

        #region Add
        /// <summary>
        /// 增加单条数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>操作是否成功</returns>
        public async Task<bool> Add(T model)
        {
            return await context.Insertable<T>(model).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 增加多条数据
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns>操作是否成功</returns>
        public async Task<bool> AddRange(List<T> list)
        {
            return await context.Insertable<T>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 添加单条数据，并返回 自增列
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public async Task<bool> AddReturnIdentity(T model)
        {
            return await context.Insertable<T>(model).ExecuteReturnIdentityAsync() > 0;//ExecuteReturnBigIdentity();//4.5.0.2 +  long
        }

        /// <summary>
        /// 增加单条数据 ，并返回 实体
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public async Task<T> AddReturnEntity(T model)
        {
            return await context.Insertable<T>(model).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// 只添加指定列
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">指定要添加的列</param>
        /// <returns></returns>
        public async Task<bool> AddColumns(T model, params string[] columns)
        {
            return await context.Insertable<T>(model).InsertColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 不插入指定列
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="IgnoreColumns">要忽略的列</param>
        /// <returns></returns>
        public async Task<bool> AddColumnsByIgnoreColumns(T model, params string[] IgnoreColumns)
        {
            return await context.Insertable<T>(model)
                .IgnoreColumns(IgnoreColumns).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region Delete

        /// <summary>
        /// 根据主键删除，并返回操作是否成功
        /// </summary>
        /// <typeparam name="S">主键的类型</typeparam>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public async Task<bool> Delete<S>(S key)
        {
            return await context.Deleteable<T>().In(key).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据主键删除，并返回操作是否成功
        /// </summary>
        /// <typeparam name="S">主键类型</typeparam>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public async Task<bool> DeleteRange<S>(params S[] keys)
        {
            return await context.Deleteable<T>().In(keys).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据条件删除，并返回操作是否成功
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<bool> DeleteWhere(Expression<Func<T, bool>> where)
        {
            return await context.Deleteable<T>().Where(where).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region Update

        /// <summary>
        /// 根据主键更新 ，返回操作是否成功
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model)
        {
            return await context.Updateable<T>(model).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据主键更新，返回操作是否成功
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns></returns>
        public async Task<bool> UpdateRange(List<T> list)
        {
            return await context.Updateable<T>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据指定 列条件 更新 ，并返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="expression">列条件 例如： t=>t.id>5 </param>
        public async Task<bool> Update(T model, Expression<Func<T, object>> expression)
        {
            return await context.Updateable<T>(model).WhereColumns(expression).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据主键，更新指定列,返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">要更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumns(T model, params string[] columns)
        {
            return await context.Updateable<T>(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据主键，更新指定列,返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">要更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumns(T model, Expression<Func<T, object>> columns)
        {
            return await context.Updateable<T>(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据主键 ， 忽略更新指定列，返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">不更新的 忽略列</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumnsByIgnoreColumns(T model, params string[] columns)
        {
            return await context.Updateable<T>(model).IgnoreColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据主键 ， 忽略更新指定列，返回操作是否成功
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="columns">不更新的 忽略列</param>
        /// <returns></returns>
        public async Task<bool> UpdateColumnsByIgnoreColumns(T model, Expression<Func<T, object>> columns)
        {
            return await context.Updateable<T>(model).IgnoreColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据主键更新 列
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="ignoreAllNullColumns">是否 NULL的列不更新</param>
        /// <param name="isOffIdentity">是否忽略 自增列</param>
        /// <param name="ignoreAllDefaultValue">是否 忽略默认值列</param>
        /// <returns></returns>
        public async Task<bool> UpdateNotNullColumns(T model, bool ignoreAllNullColumns, bool isOffIdentity = false, bool ignoreAllDefaultValue = false)
        {
            return await context.Updateable<T>()
                .IgnoreColumns(ignoreAllNullColumns: true, isOffIdentity: false, ignoreAllDefaultValue: false)
                .ExecuteCommandAsync() > 0;
        }

        //4.6.0.7 联表更新

        /// <summary>
        /// //根据不同条件执行更新不同的列 
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="dic">条件（key：要更新的列，value：条件是否更新此列）</param>
        /// <returns></returns>
        public async Task<bool> UpdateIF(T model, Dictionary<Expression<Func<T, object>>, bool> dic)
        {
            var able = context.Updateable<T>(model);
            foreach (var item in dic)
            {
                able.UpdateColumnsIF(item.Value, item.Key);// s=>s.name  ture
            }

            return await able.ExecuteCommandAsync() > 0;
        }

        #endregion

        #region Query

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> FindAll(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 取前 num 条数据
        /// </summary>
        /// <param name="num">取前几条</param>
        /// <returns></returns>
        public async Task<List<T>> getTakeList(int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// 获取单表 分页数据
        /// </summary>
        /// <param name="skip">跳过几条</param>
        /// <param name="take">取几条</param>
        /// <param name="whereExp">跳过几条</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="orderByType">排序类型（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<List<T>> getPageList(int skip, int take, Expression<Func<T, bool>> whereExp, Expression<Func<T, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Skip(skip).Take(take).OrderBy(orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 获取符合条件的前 num 条数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="num">取前几条</param>
        /// <returns></returns> 
        public async Task<List<T>> getTakeList(Expression<Func<T, bool>> where, int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="pkValue">主键</param>
        /// <returns></returns>
        public async Task<T> getByPrimaryKey(object pkValue)
        {
            return await context.Queryable<T>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// 根据条件获取 单条数据 
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<T> getFirstOrDefault(Expression<Func<T, bool>> where)
        {
            return await context.Queryable<T>().FirstAsync(where);//查询单条没有数据返回NULL, Single超过1条会报错，First不会
        }

        /// <summary>
        /// 根据主键 In  查询
        /// </summary>
        /// <typeparam name="S">主键的类型</typeparam>
        /// <param name="list">主键 In 操作的结果集</param>
        /// <returns></returns>
        public async Task<List<T>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().In<S>(list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 根据指定列 In 查询
        /// </summary>
        /// <typeparam name="S">指定列的类型</typeparam>
        /// <param name="column">指定列</param>
        /// <param name="list">指定列 In 操作 的结果集</param>
        /// <returns></returns>
        public async Task<List<T>> getByIn<S>(Expression<Func<T, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().In<S>(column, list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 根据指定列 Not In (!Contain)查询
        /// </summary>
        /// <typeparam name="S">指定列类型</typeparam>
        /// <param name="list">Not In的结果集</param>
        /// <param name="field">指定列</param>
        /// <returns></returns>
        public async Task<List<T>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(t => !SqlFunc.ContainsArray(list, field)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 根据条件 查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhere(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 单个条件 根据 isWhere 判断 是否使用此条件进行查询
        /// </summary>
        /// <param name="isWhere">判断是否使用此查询条件的条件</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhereIF(bool isWhere, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().WhereIF(isWhere, where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 多个条件 根据 wheres.value  判断是否使用 此 wheres.key 的条件
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns></returns>
        public async Task<List<T>> getByWhereIF(Dictionary<Expression<Func<T, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            var able = context.Queryable<T>();
            foreach (var item in wheres)
            {
                able.WhereIF(item.Value, item.Key);
            }
            return await able.OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 查询 指定列的值 在 start至end 之间的数据
        /// </summary>
        /// <param name="value">指定类</param>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <returns></returns>
        public async Task<List<T>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().Where(it => SqlFunc.Between(value, start, end)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 判断是否存在这条记录
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<bool> getIsAny(Expression<Func<T, bool>> where)
        {
            return await context.Queryable<T>().AnyAsync(where);
        }

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
        public async Task<List<T>> getPageList<T>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<T, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T>().WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).ToPageListAsync(pageIndex, pageSize);
        }

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
        public async Task<dynamic> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await context.Queryable<T1, T2>(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

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
        public async Task<dynamic> getJoinPageList<T1, T2>(int pageIndex, int pageSize, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await context.Queryable<T1, T2>(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToPageListAsync(pageIndex, pageSize);
        }

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
        public async Task<dynamic> getJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T1, T2, T3>(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

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
        public async Task<dynamic> getJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await context.Queryable<T1, T2, T3>(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy)
                .Select(selectExp).ToPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 执行查询sql语句 ，返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public async Task<List<T>> getListBySql(string sql)
        {
            return await context.SqlQueryable<T>(sql).ToListAsync();
        }

        /// <summary>
        /// 执行非查询sql语句，返回操作是否成功
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// 执行查询sql语句，返回查询的结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<List<T>> getListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.SqlQueryAsync<T>(sql, parameters);
        }

        /// <summary>
        /// 执行查询sql语句，返回 第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<object> getScalar(string sql, params SugarParameter[] parameters)
        {
            return await context.Ado.GetScalarAsync(sql, parameters);
        }


        #endregion

        #region Other


        /// <summary>
        /// 使用存储过程，返回结果集
        /// </summary>
        /// <param name="procedureName">存储过程名称 </param>
        /// <param name="parameters">参数，支持 output</param>
        /// <returns></returns>
        public async Task<DataTable> UseStoredProcedure(string procedureName, params SugarParameter[] parameters)
        {
            return await context.Ado.UseStoredProcedure().GetDataTableAsync(procedureName, parameters);
        }

        /// <summary>
        /// 使用事务 ，无返回值
        /// </summary>
        /// <param name="action">执行动作</param>
        /// <param name="errorCallBack">错误回调</param>
        public DbResult<bool> UseTran(Action action, Action<Exception> errorCallBack)
        {
            return context.Ado.UseTran(action, errorCallBack); ;
        }

        /// <summary>
        /// 使用事务，有返回值
        /// </summary>
        /// <typeparam name="S">返回值类型</typeparam>
        /// <param name="func">执行动作</param>
        /// <param name="errorCallBack">错误回调</param>
        public DbResult<S> UseTran<S>(Func<S> func, Action<Exception> errorCallBack)
        {
            return context.Ado.UseTran(func, errorCallBack);
        }

        #endregion

    }
}