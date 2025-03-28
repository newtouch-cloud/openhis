using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;
using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NewtouchHIS.Base.Repository
{
    /// <summary>
    /// SqlSugar 单例仓储+多库
    /// </summary>
    public class SugarRepository<TEntity> : SimpleClient<TEntity> where TEntity : class, new()
    {
        protected ITenant dbTran => SqlSugarDbContext.Db;//多租户事务、GetConnection、IsAnyConnection等功能
        public SugarRepository()
        {
            //单例模式
            base.Context = SqlSugarDbContext.Db.GetConnectionScopeWithAttr<TEntity>();//根据实体特性获取子Db
        }

        public SugarRepository(string db)
        {
            //单例模式
            base.Context = SqlSugarDbContext.Db.GetConnection(db);//根据实体特性获取子Db
        }

        /// <summary>
        /// 扩展方法，自带方法不能满足的时候可以添加新方法
        /// </summary>
        /// <returns></returns>
        public List<TEntity> CommQuery(string json)
        {
            //base.Context.Queryable<T>().ToList();可以拿到SqlSugarClient 做复杂操作
            return null;
        }
        #region 主库不一致实体表操作
        /// <summary>
        /// 当前程序库调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => Context.Insertable<T>(model).ExecuteCommand());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return i;
        }
        /// <summary>
        /// 当前程序库调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Delete<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => Context.Deleteable<T>(model).ExecuteCommand());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return i;
        }
        public async Task<bool> Update<T>(T model) where T : class, new()
        {
            //这种方式会以主键为条件
            var i = await Task.Run(() => Context.Updateable<T>(model).ExecuteCommand());
            return i > 0;
        }
        #endregion
        #region 主库一致实体表操作
        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add(TEntity model)
        {
            var i = await Task.Run(() => Context.Insertable(model).ExecuteCommand());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return i;
        }
        public async Task<long> AddRange(List<TEntity> model)
        {
            var i = await Task.Run(() => Context.Insertable(model).ExecuteCommand());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return i;
        }
        /// <summary>
        /// 根据ID列表删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(() => Context.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            var i = await Task.Run(() => Context.Deleteable<TEntity>().In(id).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity model)
        {
            //这种方式会以主键为条件
            var i = await Task.Run(() => Context.Updateable(model).ExecuteCommand());
            return i > 0;
        }
        /// <summary>
        /// 批量更新实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRange(List<TEntity> model)
        {
            //这种方式会以主键为条件
            var i = await Task.Run(() => Context.Updateable(model).ExecuteCommand());
            return i > 0;
        }

        #region Query
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="pkValue">主键</param>
        /// <returns></returns>
        public async Task<TEntity> FindKey(object pkValue)
        {
            return await Context.Queryable<TEntity>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll()
        {
            return await Task.Run(() => Context.Queryable<TEntity>().ToListAsync());
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll(bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 取前 num 条数据
        /// </summary>
        /// <param name="num">取前几条</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetTakeList(int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
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
        public async Task<List<TEntity>> GetPageList(int skip, int take, Expression<Func<TEntity, bool>> whereExp, Expression<Func<TEntity, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Skip(skip).Take(take).OrderBy(orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 获取符合条件的前 num 条数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="num">取前几条</param>
        /// <returns></returns> 
        public async Task<List<TEntity>> GetTakeList(Expression<Func<TEntity, bool>> where, int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// 根据条件获取 单条数据 
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Queryable<TEntity>().FirstAsync(where);//查询单条没有数据返回NULL, Single超过1条会报错，First不会
        }

        /// <summary>
        /// 根据主键 In  查询
        /// </summary>
        /// <typeparam name="S">主键的类型</typeparam>
        /// <param name="list">主键 In 操作的结果集</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().In<S>(list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 根据指定列 In 查询
        /// </summary>
        /// <typeparam name="S">指定列的类型</typeparam>
        /// <param name="column">指定列</param>
        /// <param name="list">指定列 In 操作 的结果集</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByIn<S>(Expression<Func<TEntity, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().In<S>(column, list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 根据指定列 Not In (!Contain)查询
        /// </summary>
        /// <typeparam name="S">指定列类型</typeparam>
        /// <param name="list">Not In的结果集</param>
        /// <param name="field">指定列</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(t => !SqlFunc.ContainsArray(list, field)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 根据条件 查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByWhere(Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }
        /// <summary>
        /// Where 同库表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByWhere<T>(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 单个条件 根据 isWhere 判断 是否使用此条件进行查询
        /// </summary>
        /// <param name="isWhere">判断是否使用此查询条件的条件</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByWhereIF(bool isWhere, Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().WhereIF(isWhere, where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 多个条件 根据 wheres.value  判断是否使用 此 wheres.key 的条件
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByWhereIF(Dictionary<Expression<Func<TEntity, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            var able = Context.Queryable<TEntity>();
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
        public async Task<List<TEntity>> GetByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(it => SqlFunc.Between(value, start, end)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 判断是否存在这条记录
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<bool> GetIsAny(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Queryable<TEntity>().AnyAsync(where);
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
        public async Task<List<object>> GetJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
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
        public async Task<dynamic> GetJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

       

        /// <summary>
        /// 执行查询sql语句 ，返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListBySql(string sql)
        {
            return await Context.SqlQueryable<TEntity>(sql).ToListAsync();
        }

        #region Ado 
        /// <summary>
        /// 执行非查询sql语句，返回操作是否成功
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// 执行查询sql语句，返回查询的结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.SqlQueryAsync<TEntity>(sql, parameters);
        }

        /// <summary>
        /// 执行查询sql语句，返回 第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<object> GetScalar(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.GetScalarAsync(sql, parameters);
        }

        #endregion

        #endregion
        #endregion

        #region 根据表 DB Attr特性查询 适用于与主库同库非主体实体表

        #region Add Update Delete
        /// <summary>
        /// [DB Attr]写入实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> AddWithAttr<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Insertable(model).ExecuteCommand());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return i;
        }

        /// <summary>
        /// [DB Attr]更新实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateWithAttr<T>(T model) where T : class, new()
        {
            //这种方式会以主键为条件
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Updateable(model).ExecuteCommand());
            return i > 0;
        }
        /// <summary>
        /// [DB Attr]根据ID列表删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsWithAttr<T>(object[] ids) where T : class, new()
        {
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Deleteable<T>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// [DB Attr]根据ID删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdWithAttr<T>(object id) where T : class, new()
        {
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Deleteable<T>().In(id).ExecuteCommand());
            return i > 0;
        }
        #endregion

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="pkValue">主键</param>
        /// <returns></returns>
        public async Task<T> FindKeyWithAttr<T>(object pkValue)
        {
            return await Context.Queryable<T>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<List<T>> FindAllWithAttr<T>()
        {
            return await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().ToListAsync());
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> FindAllWithAttr<T>(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }
        /// <summary>
        /// [DB Attr]根据条件获取 单条数据 
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<T> GetFirstOrDefaultWithAttr<T>(Expression<Func<T, bool>> where)
        {
            return await dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().Where(where).FirstAsync();//查询单条没有数据返回NULL, Single超过1条会报错，First不会
        }
        /// <summary>
        ///  根据条件 查询（根据 T 的Db 特性查询）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByWhereWithAttr<T>(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// 两表查询，(根据 T1 的Db 特性查询)
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
        public async Task<List<object>> GetJoinListWithAttr<T1, T2>(string db, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        #endregion

        #region DbMuti Query 
        /// <summary>
        /// 根据条件 查询 (choose db)
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<List<T>> GetByWhere<T>(string db, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScope(db).Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }


        /// <summary>
        /// 两表查询，(choose db)
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
        public async Task<List<object>> GetJoinList<T1, T2>(string db, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScope(db).Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

        #region Ado 不推荐
        /// <summary>
        /// 执行非查询sql语句，返回操作是否成功(choose db)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string db, string sql, params SugarParameter[] parameters)
        {
            return await dbTran.GetConnectionScope(db).Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// 执行查询sql语句，返回查询的结果集(choose db)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<List<T>> GetListBySqlQuery<T>(string db, string sql, List<DbParameter> parameters)
        {
            List<SugarParameter> sugarPara = new List<SugarParameter>();
            parameters.ForEach(p =>
            {
                sugarPara.Add(new SugarParameter(p.ParameterName, p.Value));
            });
            return await dbTran.GetConnectionScope(db).Ado.SqlQueryAsync<T>(sql, sugarPara);
        }

        /// <summary>
        /// 执行查询sql语句，返回 第一行第一列(choose db)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数化</param>
        /// <returns></returns>
        public async Task<object> GetScalar(string db, string sql, params SugarParameter[] parameters)
        {
            return await dbTran.GetConnectionScope(db).Ado.GetScalarAsync(sql, parameters);
        }

        #endregion
        #endregion

        
    }
}
