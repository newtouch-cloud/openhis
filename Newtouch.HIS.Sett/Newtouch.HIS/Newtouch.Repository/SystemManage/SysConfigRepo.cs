using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtouch.Common.Exceptions;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysConfigRepo : RepositoryBase<SysConfigEntity>, ISysConfigRepo
    {
        public SysConfigRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysConfigEntity GetByCode(string code, string orgId)
        {
            return this.IQueryable().Where(a => a.Code == code && a.OrganizeId == orgId && a.zt == "1").FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public string GetValueByCode(string code, string orgId)
        {
            return this.IQueryable().Where(a => a.Code == code && a.OrganizeId == orgId && a.zt == "1").Select(p => p.Value).FirstOrDefault() ?? "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dm"></param>
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
        /// 
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null)
        {
            var val = GetValueByCode(code, orgId);
            if (val == "1" || val == "0")
            {
                return val == "1";
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
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysConfigEntity> GetList(string keyword, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            var sql = @"select * from Sys_Config(nolock) where OrganizeId = @orgId and (Code like @searchKeyword or Name like @searchKeyword) order by Code asc";
            return this.FindList<SysConfigEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@orgId", organizeId),
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysConfigEntity GetForm(string keyValue)
        {
            return this.FindEntity(keyValue);
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
        }

    }
}
