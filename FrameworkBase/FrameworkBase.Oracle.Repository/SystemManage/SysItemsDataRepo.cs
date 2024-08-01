using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Oracle.ManagedDataAccess.Client;

namespace FrameworkBase.Oracle.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-20 13:04
    /// 描 述：字典项
    /// </summary>
    public sealed class SysItemsDataRepo : RepositoryBase<SysItemsDataEntity>, ISysItemsDataRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysItemsDataRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysItemsDataEntity> GetList(string itemId, string keyword)
        {
            var sql = new StringBuilder();
            sql.Append("select * from \"Sys_ItemsDetail\" where 1 = 1");
            List<OracleParameter> pars = null;
            pars = new List<OracleParameter>();
            sql.Append(" and \"ItemId\" = :itemId");
            pars.Add(new OracleParameter(":itemId", itemId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (\"Name\" like :keyword or \"Code\" like :keyword)");
                pars.Add(new OracleParameter(":keyword", "%" + keyword.Trim() + "%"));
            }
            return this.FindList<SysItemsDataEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(SysItemsDataEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var isRepeated = this.IQueryable().Any(p => p.ItemId == entity.ItemId && p.Code == entity.Code && p.Id != keyValue);
                if (isRepeated)
                {
                    throw new FailedException("编码不可重复");
                }

                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.Code = entity.Code;
                dbEntity.Name = entity.Name;
                dbEntity.px = entity.px;
                dbEntity.Description = entity.Description;
                dbEntity.zt = entity.zt;

                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                var isRepeated = this.IQueryable().Any(p => p.ItemId == entity.ItemId && p.Code == entity.Code);
                if (isRepeated)
                {
                    throw new FailedException("编码不可重复");
                }

                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}