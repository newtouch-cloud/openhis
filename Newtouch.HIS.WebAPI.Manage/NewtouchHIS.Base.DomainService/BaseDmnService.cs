using NewtouchHIS.Base.Repository;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Attributes;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace NewtouchHIS.Base.DomainService
{
    /// <summary>
    /// 继承DBContext,对service 开放ISqlSugarClient 接口
    /// 支持 base.Context 变更数据库
    /// 默认 base.Context 为TEntity 绑定的TenantAttr
    /// 注：增、删、改 禁止跨库，禁止用sql语句，如需则新增service，通过base.Context 指定目标数据库
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseDmnService<TEntity> : SugarRepository<TEntity> where TEntity : class, new()
    {
        public BusinessConfig sysConfig = ConfigInitHelper.SysConfig;
        protected string? GetAppDB(string appId)
        {
            if (string.IsNullOrWhiteSpace(appId) || sysConfig.AppAPIHostName == null)
            {
                return null;
            }
            PropertyInfo propertyInfo = sysConfig.AppAPIHostName!.GetPropertyByValue(appId);
            if (propertyInfo == null)
            {
                return null;
            }
            return propertyInfo.GetCustomAttribute<DbTagAttribute>()?._dbId?.ToString();
        }

        /// <summary>
        /// 非空校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ValidRequired<T>(T t, out string? errMsg)
        {
            errMsg = string.Empty;
            bool valid = true;
            var props = t!.GetType()!.GetProperties().Where(p => p.CustomAttributes.Any());
            foreach (var p in props)
            {
                if (p.CustomAttributes!.Where(x => x.AttributeType.Name.Contains("Required")).Any()
                    && (p.GetValue(t) == null || string.IsNullOrWhiteSpace(p.GetValue(t)?.ToString())))
                {
                    errMsg += $"[{p.Name}]";
                    if (valid)
                    {
                        valid = false;
                    }
                }
            }
            errMsg = string.IsNullOrWhiteSpace(errMsg) ? errMsg : $"请补充必要信息:{errMsg}";
            return valid;
        }
        /// <summary>
        /// 枚举值校验 
        /// 若要提示明确 需添加 DescriptionAttribute
        /// </summary>
        /// <param name="enumList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool ValidEnum(Dictionary<Type, int> enumList, out string? errMsg)
        {
            errMsg = string.Empty;
            bool valid = true;
            foreach (var d in enumList)
            {
                var type = d.Key;
                var typeDesc = type.GetCustomAttributes<DescriptionAttribute>();
                if (!Enum.IsDefined(type, d.Value))
                {

                    errMsg += $"{typeDesc?.FirstOrDefault()?.Description}[{d.Value}]";
                    if (valid)
                    {
                        valid = false;
                    }
                }
            }
            errMsg = string.IsNullOrWhiteSpace(errMsg) ? errMsg : $"枚举信息取值异常:{errMsg}";
            return valid;
        }
        /// <summary>
        /// 验证枚举value有效性 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="errmsg">错误提示</param>
        /// <returns></returns>
        public bool ValidEnum<TEnum>(int value) where TEnum : Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 自动组装where 查询（根据 T 的Db 特性查询）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>       
        public Expression<Func<T, bool>> AssembledWhereEqual<T, TQueryParams>(TQueryParams entity)
            where T : class, new()
        {
            var t = Expression.Parameter(typeof(T), "t");
            var trueExpression = Expression.Constant(true);
            var resultExpression = Expression.AndAlso(trueExpression, trueExpression);

            var type = typeof(TQueryParams).GetProperties().Where(p => p.GetValue(entity) != null);
            foreach (var prop in type)
            {
                var v = prop.GetValue(entity);
                if (v != null && !string.IsNullOrWhiteSpace(v.ToString()))
                {
                    var paramValue = Expression.Constant(v, prop.PropertyType);
                    var entityProperty = Expression.Property(t, prop.Name);
                    var p = Expression.Convert(paramValue, entityProperty.Type);
                    var equalExpression = Expression.Equal(entityProperty, p);
                    resultExpression = Expression.AndAlso(resultExpression, equalExpression);
                }

            }
            return Expression.Lambda<Func<T, bool>>(resultExpression, t);
        }

        #region 联表查询
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
        public async Task<List<TResp>> GetJoinList<T1, T2, TResp>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinList<T1, T2, T3, TResp>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinList<T1, T2, T3, T4, TResp>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, T4, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, T4, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, T4, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinListWithAttr<T1, T2, TResp>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinListWithAttr<T1, T2, T3, TResp>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        public async Task<List<TResp>> GetJoinListWithAttr<T1, T2, T3, T4, TResp>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, T4, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, T4, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, T4, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        #endregion

        #region 分页查询
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
        public async Task<PageResponseRow<List<T>>> GetPageList<T>(int offset, int limit, bool isWhere = false, Expression<Func<T, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable<T>().WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<T>> { rows = result, total = totalNumber };
        }
        /// <summary>
        ///  两表 分页查询
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="totalNumber"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="joinExp"></param>
        /// <param name="selectExp"></param>
        /// <param name="isWhere"></param>
        /// <param name="whereExp"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public async Task<PageResponseRow<List<TResp>>> GetJoinPageList<T1, T2, TResp>(int offset, int limit, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType)
                .WhereIF(isWhere, whereExp)
                .Select(selectExp)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
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
        public async Task<PageResponseRow<List<TResp>>> GetJoinPageList<T1, T2, T3, TResp>(int offset, int limit, Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable(joinExp)
                .WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy)
                .Select(selectExp)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }
        public async Task<PageResponseRow<List<TResp>>> GetJoinPageList<T1, T2, T3, T4, TResp>(int offset, int limit, Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, T4, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, T4, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, T4, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable(joinExp)
                .WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy)
                .Select(selectExp)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }
        public async Task<PageResponseRow<List<TResp>>> GetJoinOrderbyPageList<T1, T2, T3, T4, TResp>(int offset, int limit, string orderby, OrderByType orderbyType, Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, T4, TResp>> selectExp, bool isWhere = false, Expression<Func<T1, T2, T3, T4, bool>> whereExp = null)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable(joinExp)
                .WhereIF(isWhere, whereExp)
                .Select(selectExp)
                .MergeTable()
                .OrderByPropertyName(orderby, orderbyType)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }
        #endregion

        #region 无实体
        /// <summary>
        /// 联表查询（未定义实体表）
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="mainTbName">主表名</param>
        /// <param name="mainTbAs">主表 as 别名</param>
        /// <param name="joinInfoParameters"></param>
        /// <param name="selectExp"></param>
        /// <param name="isWhere"></param>
        /// <param name="whereExp"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        //public async Task<List<TResp>> GetTableJoinList<TResp>(string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, Expression<Func<TResp>> selectExp, bool isWhere = false, Expression<Func<TResp, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        //{
        //    return await Context.Queryable<TResp>().AS(mainTbName, mainTbAs)
        //        .AddJoinInfo(joinInfoParameters)
        //        .Select<TResp>(selectExp).WhereIF(isWhere, whereExp)
        //        .OrderByIF(isOrderBy, orderBy, orderByType)
        //        .ToListAsync();
        //}
        //public async Task<List<TResp>> GetTableJoinList<TResp>(string db,string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, Expression<Func<TResp>> selectExp, bool isWhere = false, Expression<Func<TResp, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        //{
        //    return await dbTran.GetConnectionScope(db).Queryable<TResp>().AS(mainTbName, mainTbAs)
        //        .AddJoinInfo(joinInfoParameters)
        //        .Select<TResp>(selectExp).WhereIF(isWhere, whereExp)
        //        .OrderByIF(isOrderBy, orderBy, orderByType)
        //        .ToListAsync();
        //}
        public async Task<List<TResp>> GetTableJoinList<TResp>(string db,string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, List<SelectModel> selectExp, bool isWhere = false, ObjectFuncModel whereFunc = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScope(db).Queryable<TResp>().AS(mainTbName, mainTbAs)
                .AddJoinInfo(joinInfoParameters)
                .Select<TResp>(selectExp).Where(whereFunc)
                .OrderByIF(isOrderBy, orderBy, orderByType)
                .ToListAsync();
        }
        /// <summary>
        /// 联表分页查询（未定义实体表）
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="totalNumber"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="mainTbName"></param>
        /// <param name="mainTbAs"></param>
        /// <param name="joinInfoParameters"></param>
        /// <param name="selectExp"></param>
        /// <param name="isWhere"></param>
        /// <param name="whereExp"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public PageResponseRow<List<TResp>> GetTableJoinPageList<TResp>(ref int totalNumber, int offset, int limit, string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, Expression<Func<TResp>> selectExp, bool isWhere = false, Expression<Func<TResp, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            var result = Context.Queryable<TResp>().AS(mainTbName, mainTbAs)
                .AddJoinInfo(joinInfoParameters)
                .Select<TResp>(selectExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType)
                .ToPageList(PageIndexOffset(offset, limit), limit, ref totalNumber);
            return new PageResponseRow<List<TResp>> { rows = result, total = totalNumber };
        }
        public async Task<PageResponseRow<List<TResp>>> GetTableJoinPageList<TResp>(int offset, int limit, string mainTbName, string mainTbAs, List<JoinInfoParameter> joinInfoParameters, Expression<Func<TResp>> selectExp, bool isWhere = false, Expression<Func<TResp, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<TResp, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> totalNumber = 0;
            var result = await Context.Queryable<TResp>().AS(mainTbName, mainTbAs)
                .AddJoinInfo(joinInfoParameters)
                .Select<TResp>(selectExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType)
                .ToPageListAsync(PageIndexOffset(offset, limit), limit, totalNumber);
            return new PageResponseRow<List<TResp>> { total = totalNumber, rows = result };
        }
        #endregion

        #region private
        private int PageIndexOffset(int offset, int limit)
        {
            if (offset > 0 && limit > 0)
            {
                return (offset / limit) + 1;
            }
            return 1;
        }


        #endregion
    }
}
