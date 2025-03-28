using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SysConfigRepo : RepositoryBase<SysConfigEntity>, ISysConfigRepo
    {
        private readonly ICache _cache;

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        /// <param name="cache"></param>
        public SysConfigRepo(IDefaultDatabaseFactory databaseFactory, ICache cache)
            : base(databaseFactory)
        {
            _cache = cache;
        }

        /// <summary>
        /// 通过code和组织机构代码获取配置信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysConfigEntity GetByCode(string code, string orgId)
        {
            return IQueryable().FirstOrDefault(a => a.Code == code && a.OrganizeId == orgId && a.zt == "1");
        }

        /// <summary>
        /// 通过code和组织机构代码获取配置信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetValueByCode(string code, string orgId)
        {
            return IQueryable().Where(a => a.Code == code && a.OrganizeId == orgId && a.zt == "1").Select(p => p.Value).FirstOrDefault() ?? "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetIntValueByCode(string code, string orgId, int defaultValue = 0)
        {
            var val = GetValueByCode(code, orgId);
            if (val == null)
            {
                return defaultValue;
            }
            int returnVal;
            return int.TryParse(val, out returnVal) ? returnVal : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null)
        {
            var val = GetValueByCode(code, orgId);
            switch (val)
            {
                case "1":
                case "0":
                    return val == "1";
                case null:
                    return defaultValue;
            }
            bool returnVal;
            return bool.TryParse(val, out returnVal) ? returnVal : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysConfigEntity> GetList(string keyword, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            const string sql = @"select * from Sys_Config(nolock) where OrganizeId = @orgId and (Code like @searchKeyword or Name like @searchKeyword) order by Code asc";
            return FindList<SysConfigEntity>(sql, new DbParameter[] {
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
            return FindEntity(keyValue);
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
                if (IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Modify(keyValue);
                Update(entity);
            }
            else
            {
                //Code重复判断
                if (IQueryable().Any(p => p.Code == entity.Code && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true);
                Insert(entity);
            }
        }

        /// <summary>
        /// 更改配置
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <param name="orgId"></param>
        public void SetValueByCode(string code, string value, string orgId)
        {
            var paraList = new object[]
            {
                new SqlParameter("@value", value),
                new SqlParameter("@code", code), //user_code));
                new SqlParameter("@orgId", orgId)
            };
            _dataContext.Database.ExecuteSqlCommand(@"UPDATE dbo.Sys_Config SET Value = @value WHERE Code = @code AND OrganizeId = @orgId ", paraList);
        }

    }
}