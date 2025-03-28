using NewtouchHIS.Base.Repository;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services;
using SqlSugar;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace NewtouchHIS.Base.DomainService
{
    [Obsolete("功能即将下线，请更换基类为 BaseDmnService")]
    public class BaseServices<TEntity> where TEntity : class, new()
    {
        public BusinessConfig sysConfig = ConfigInitHelper.SysConfig;
        /// <summary>
        /// 上下文为实体Attr 归属库
        /// </summary>
        public SugarRepository<TEntity> baseDal => new SugarRepository<TEntity>();


        #region Identity
        /// <summary>
        /// 当前Api用户信息
        /// </summary>
        public IIdentityCache CurrentIdentity { get; set; }
        #endregion
        public BaseServices()
        {
            CurrentIdentity = NullIdentityCache.Instance;//空实现
        }
        public void DbConnection()
        {

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
            return Expression.Lambda<Func<T, bool>>(resultExpression, t); //要用变量 var exp=
        }

    }
}
