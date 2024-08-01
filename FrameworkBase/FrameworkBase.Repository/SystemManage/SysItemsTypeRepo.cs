using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using Newtouch.Core.Common.Exceptions;

namespace FrameworkBase.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-20 13:03
    /// 描 述：字典分类
    /// </summary>
    public sealed class SysItemsTypeRepo : RepositoryBase<SysItemsTypeEntity>, ISysItemsTypeRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysItemsTypeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysItemsTypeEntity> GetList(string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_Items(nolock) where 1 = 1");
            List<SqlParameter> pars = null;
            pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (Name like @keyword or Code like @keyword)");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.FindList<SysItemsTypeEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 获取有效分类列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysItemsTypeEntity> GetValidList(string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_Items(nolock) where 1 = 1 and zt = '1'");
            List<SqlParameter> pars = null;
            pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (Name like @keyword or Code like @keyword)");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.FindList<SysItemsTypeEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(SysItemsTypeEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编号不可重复");
                }

                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.ParentId = entity.ParentId;
                dbEntity.Name = entity.Name;
                dbEntity.Code = entity.Code;
                dbEntity.Description = entity.Description;
                dbEntity.CreateTime = entity.CreateTime;
                dbEntity.px = entity.px;
                dbEntity.zt = entity.zt;

                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code))
                {
                    throw new FailedException("编号不可重复");
                }

                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}