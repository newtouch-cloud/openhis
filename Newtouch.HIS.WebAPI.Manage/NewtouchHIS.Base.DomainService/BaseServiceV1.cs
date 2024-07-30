using NewtouchHIS.Base.Domain;
using SqlSugar;
using System.Data;
using System.Linq.Expressions;

namespace NewtouchHIS.Base.DomainService
{
    public class BaseServiceV1<T> : IBaseServiceV1<T> where T : class, new()
    {
        private readonly IBaseRepositoryV1<T> _baseRepository;
        public BaseServiceV1(IBaseRepositoryV1<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task<bool> Add(T model)
        {
            return _baseRepository.Add(model);
        }

        public Task<bool> AddColumns(T model, params string[] columns)
        {
            return _baseRepository.AddColumns(model, columns);
        }

        public Task<bool> AddColumnsByIgnoreColumns(T model, params string[] IgnoreColumns)
        {
            return _baseRepository.AddColumnsByIgnoreColumns(model, IgnoreColumns);
        }

        public Task<bool> AddRange(List<T> list)
        {
            return _baseRepository.AddRange(list);
        }

        public Task<T> AddReturnEntity(T model)
        {
            return _baseRepository.AddReturnEntity(model);
        }

        public Task<bool> AddReturnIdentity(T model)
        {
            return _baseRepository.AddReturnIdentity(model);
        }

        public Task<bool> Delete<S>(S key)
        {
            return _baseRepository.Delete<S>(key);
        }

        public Task<bool> DeleteRange<S>(params S[] keys)
        {
            return _baseRepository.DeleteRange<S>(keys);
        }

        public Task<bool> DeleteWhere(Expression<Func<T, bool>> where)
        {
            return _baseRepository.DeleteWhere(where);
        }

        public Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return _baseRepository.ExecuteCommandSql(sql, parameters);
        }

        public Task<List<T>> FindAll(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.FindAll(isOrderBy, orderBy, orderByType);
        }

        public Task<List<T>> getByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getByBetween(value, start, end, isOrderBy, orderBy, orderByType);
        }

        public Task<List<T>> getByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getByIn<S>(list, isOrderBy, orderBy, orderByType);
        }

        public Task<List<T>> getByIn<S>(Expression<Func<T, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getByIn<S>(column, list, isOrderBy, orderBy, orderByType);
        }

        public Task<List<T>> getByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getByNotIn<S>(list, field, isOrderBy, orderBy, orderByType);
        }

        public Task<T> getByPrimaryKey(object pkValue)
        {
            return _baseRepository.getByPrimaryKey(pkValue);
        }

        public Task<List<T>> getByWhere(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getByWhere(where, isOrderBy, orderBy, orderByType);
        }

        public Task<List<T>> getByWhereIF(bool isWhere, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getByWhereIF(isWhere, where, isOrderBy, orderBy, orderByType);
        }

        public Task<List<T>> getByWhereIF(Dictionary<Expression<Func<T, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getByWhereIF(wheres, isOrderBy, orderBy, orderByType);
        }

        public Task<T> getFirstOrDefault(Expression<Func<T, bool>> where)
        {
            return _baseRepository.getFirstOrDefault(where);
        }

        public Task<bool> getIsAny(Expression<Func<T, bool>> where)
        {
            return _baseRepository.getIsAny(where);
        }

        public Task<dynamic> getJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getJoinList<T1, T2>(joinExp, selectExp, isWhere, whereExp, isOrderBy, orderBy, orderByType);
        }

        public Task<dynamic> getJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getJoinList<T1, T2, T3>(joinExp, selectExp, isWhere, whereExp, isOrderBy, orderBy, orderByType);
        }

        public Task<dynamic> getJoinList<T1, T2, T3>(int pageIndex, int pageSize, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getJoinList<T1, T2, T3>(pageIndex, pageSize, joinExp, selectExp, isWhere, whereExp, isOrderBy, orderBy, orderByType); ;
        }

        public Task<dynamic> getJoinPageList<T1, T2>(int pageIndex, int pageSize, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getJoinPageList<T1, T2>(pageIndex, pageSize, joinExp, selectExp, isWhere, whereExp, isOrderBy, orderBy, orderByType);
        }

        public Task<List<T>> getListBySql(string sql)
        {
            return _baseRepository.getListBySql(sql);
        }

        public Task<List<T>> getListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return _baseRepository.getListBySqlQuery(sql, parameters);
        }

        public Task<List<T>> getPageList(int skip, int take, Expression<Func<T, bool>> whereExp, Expression<Func<T, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getPageList(skip, take, whereExp, orderBy, orderByType);
        }

        public Task<List<T1>> getPageList<T1>(int pageIndex, int pageSize, bool isWhere = false, Expression<Func<T1, bool>> whereExp = default, bool isOrderBy = false, Expression<Func<T1, object>> orderBy = default, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getPageList<T1>(pageIndex, pageSize, isWhere, whereExp, isOrderBy, orderBy, orderByType);
        }

        public Task<object> getScalar(string sql, params SugarParameter[] parameters)
        {
            return _baseRepository.getScalar(sql, parameters);
        }

        public Task<List<T>> getTakeList(int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getTakeList(num, isOrderBy, orderBy, orderByType);
        }

        public Task<List<T>> getTakeList(Expression<Func<T, bool>> where, int num, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _baseRepository.getTakeList(where, num, isOrderBy, orderBy, orderByType);
        }

        public Task<bool> Update(T model)
        {
            return _baseRepository.Update(model);
        }

        public Task<bool> Update(T model, Expression<Func<T, object>> expression)
        {
            return _baseRepository.Update(model, expression);
        }

        public Task<bool> UpdateColumns(T model, params string[] columns)
        {
            return _baseRepository.UpdateColumns(model, columns);
        }

        public Task<bool> UpdateColumns(T model, Expression<Func<T, object>> columns)
        {
            return _baseRepository.UpdateColumns(model, columns);
        }

        public Task<bool> UpdateColumnsByIgnoreColumns(T model, params string[] columns)
        {
            return _baseRepository.UpdateColumnsByIgnoreColumns(model, columns);
        }

        public Task<bool> UpdateColumnsByIgnoreColumns(T model, Expression<Func<T, object>> columns)
        {
            return _baseRepository.UpdateColumnsByIgnoreColumns(model, columns);
        }

        public Task<bool> UpdateIF(T model, Dictionary<Expression<Func<T, object>>, bool> dic)
        {
            return _baseRepository.UpdateIF(model, dic);
        }

        public Task<bool> UpdateNotNullColumns(T model, bool ignoreAllNullColumns, bool isOffIdentity = false, bool ignoreAllDefaultValue = false)
        {
            return _baseRepository.UpdateNotNullColumns(model, ignoreAllNullColumns, isOffIdentity, ignoreAllDefaultValue);
        }

        public Task<bool> UpdateRange(List<T> list)
        {
            return _baseRepository.UpdateRange(list);
        }

        public Task<DataTable> UseStoredProcedure(string procedureName, params SugarParameter[] parameters)
        {
            return _baseRepository.UseStoredProcedure(procedureName, parameters);
        }
    }
}
