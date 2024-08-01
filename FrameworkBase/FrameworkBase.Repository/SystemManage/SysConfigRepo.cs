using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FrameworkBase.Repository
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public sealed class SysConfigRepo : RepositoryBase<SysConfigEntity>, ISysConfigRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysConfigRepo(IDefaultDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysConfigEntity GetByCode(string code)
        {
            return this.IQueryable().Where(a => a.Code == code && a.zt == "1").FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetValueByCode(string code)
        {
            return this.IQueryable().Where(a => a.Code == code && a.zt == "1").Select(p => p.Value).FirstOrDefault() ?? "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetIntValueByCode(string code, int defaultValue = 0)
        {
            var val = GetValueByCode(code);
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
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool? GetBoolValueByCode(string code, bool? defaultValue = null)
        {
            var val = GetValueByCode(code);
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
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysConfigEntity> GetList(string keyword)
        {
            var sql = @"select * from Sys_Config(nolock) where (Code like @searchKeyword or Name like @searchKeyword) order by Code asc";
            return this.FindList<SysConfigEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysConfigEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.Code == entity.Code))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}
