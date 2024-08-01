using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public sealed class SysConfigRepo : RepositoryBase<SysConfigEntity>, ISysConfigRepo
    {
        private readonly ICache _cache;
        private const string listCacheKey = "set:systemconfig_{0}";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        /// <param name="cache"></param>
        public SysConfigRepo(IDefaultDatabaseFactory databaseFactory
            , ICache cache)
            : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// 根据Code获取配置Entity（with缓存）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysConfigVO GetByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(code))
            {
                return null;
            }
            var list = GetOrgCachedConfigList(orgId);
            var vo = list.Where(p => p.Code == code).FirstOrDefault();
            if (vo == null)
            {
                //已违反框架分层，但又不得不这样，
                //因为SysConfigVO GetByCode(string code, string orgId)
                //已在N出使用
                var tepmlateEntity = new SystemConfigTemplateRepo((IDefaultDatabaseFactory)this._databaseFactory, this._cache).GetByCode(code);
                if (tepmlateEntity != null)
                {
                    vo = new SysConfigVO()
                    {
                        Code = code,
                        Value = tepmlateEntity.DefaultVal,
                    };
                }
            }
            return vo;
        }

        /// <summary>
        /// 根据Code获取Value
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetValueByCode(string code, string orgId)
        {
            var entity = GetByCode(code, orgId);
            if (entity != null)
            {
                return entity.Value;
            }
            return "";
        }

        /// <summary>
        /// 根据Code获取Value Int
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetIntValueByCode(string code, string orgId, int defaultValue = 0)
        {
            var val = GetValueByCode(code, orgId);
            if (val != null)
            {
                int returnVal;
                if (int.TryParse(val, out returnVal))
                {
                    return returnVal;
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 根据Code获取Value Decimal
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public decimal GetDecimalValueByCode(string code, string orgId, decimal defaultValue = 0)
        {
            var val = GetValueByCode(code, orgId);
            if (val != null)
            {
                decimal returnVal;
                if (decimal.TryParse(val, out returnVal))
                {
                    return returnVal;
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 根据Code获取Value Bool
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null)
        {
            var val = GetValueByCode(code, orgId);
            if (val == "1" || val == "0")
            {
                return val == "1";
            }
            else if (!string.IsNullOrWhiteSpace(val) && val.ToString().ToLower() == "off")
            {
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(val) && val.ToString().ToLower() == "on")
            {
                return true;
            }
            else if (val != null)
            {
                bool returnVal;
                if (bool.TryParse(val, out returnVal))
                {
                    return returnVal;
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取列表（检索查询）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysConfigEntity> GetList(string keyword, string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = @"select * from Sys_Config(nolock) where OrganizeId = @orgId and (Code like @searchKeyword or Name like @searchKeyword) order by Name asc";
            return this.FindList<SysConfigEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@orgId", orgId),
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                });
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysConfigEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
            //清除缓存
            var key = string.Format(listCacheKey, entity.OrganizeId);
            _cache.Remove(key);
        }

        /// <summary>
        /// 修改配置的值（不存在则新增）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void UpdateValue(string code, string orgId, string name, string value)
        {
            var entity = this.IQueryable().FirstOrDefault(p => p.Code == code && p.OrganizeId == orgId);
            if (entity != null)
            {
                entity.Value = value;
                entity.Modify();
                this.Update(entity);
            }
            else
            {
                entity = new SysConfigEntity();
                entity.OrganizeId = orgId;
                entity.Code = orgId;
                entity.Name = name;
                entity.Value = value;
                entity.zt = "1";
                entity.Create(true);
                this.Insert(entity);
            }
            //清除缓存
            var key = string.Format(listCacheKey, entity.OrganizeId);
            _cache.Remove(key);
        }

        #region private methods

        /// <summary>
        /// 获取组织机构动态参数配置列表（with缓存）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysConfigVO> GetOrgCachedConfigList(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return new List<SysConfigVO>();
            }
            var key = string.Format(listCacheKey, orgId);
            var list = _cache.Get<IList<SysConfigVO>>(key, () =>
            {
                var sql = @"select Code, Value 
from Sys_Config(nolock)
where OrganizeId = @orgId and zt = '1'";
                return this.FindList<SysConfigVO>(sql, new[] { new SqlParameter("@orgId", orgId) });
            });
            return list;
        }

        #endregion

    }
}
