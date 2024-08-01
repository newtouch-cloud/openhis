using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 系统配置模板
    /// </summary>
    public class SystemConfigTemplateRepo : RepositoryBase<SysConfigTemplateEntity>, ISystemConfigTemplateRepo
    {
        private readonly ICache _cache;
        private const string listCacheKey = "set:systemconfigtemplate";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        /// <param name="cache"></param>
        public SystemConfigTemplateRepo(IDefaultDatabaseFactory databaseFactory
            , ICache cache)
            : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// 根据Code获取模板Entity（with缓存）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysConfigTemplateEntity GetByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }
            var list = GetCachedConfigList();
            var entity = list.Where(p => p.Code == code).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// 获取公共模板列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysConfigTemplateEntity> GetListTmp(string keyword)
        {
            string sql = @"select Id,Code,Name,Memo,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,px,DefaultVal
                            from sys_configtemplate with(nolock)
                            ";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " where CHARINDEX(@keyword,Name)>0 or CHARINDEX(@keyword,Code)>0 ";
            }
            sql += " order by Code asc ";

            IList<SysConfigTemplateEntity> list = this.FindList<SysConfigTemplateEntity>(sql, new SqlParameter[] { new SqlParameter("@keyword", keyword) });
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysConfigTemplateEntity> GetTemplateInfo(string keyValue)
        {
            string sql = @"select Id,Code,Name,Memo,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,px
                            from sys_configtemplate with(nolock)
                            where Id=@Id ";
            IList<SysConfigTemplateEntity> list = this.FindList<SysConfigTemplateEntity>(sql, new SqlParameter[] { new SqlParameter("@Id", keyValue) });
            return list;
        }
        /// <summary>
        /// 新增/修改公共模板
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitFormTmp(SysConfigTemplateEntity entity, string keyValue)
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
            //清除缓存
            var key = string.Format(listCacheKey);
            _cache.Remove(key);
        }

        #region private methods

        /// <summary>
        /// 获取模板列表（with缓存）
        /// </summary>
        /// <returns></returns>
        public IList<SysConfigTemplateEntity> GetCachedConfigList()
        {
            var key = listCacheKey;
            var list = _cache.Get<IList<SysConfigTemplateEntity>>(key, () =>
            {
                var sql = @"select * from Sys_ConfigTemplate(nolock) where zt = '1'";
                return this.FindList<SysConfigTemplateEntity>(sql);
            });
            return list;
        }

        #endregion

    }
}
