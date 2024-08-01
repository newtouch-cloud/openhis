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
    /// 日 期：2017-11-17 16:08
    /// 描 述：系统岗位
    /// </summary>
    public sealed class SysDutyRepo : RepositoryBase<SysDutyEntity>, ISysDutyRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysDutyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        public IList<SysDutyEntity> GetList(string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append("select * from \"Sys_Duty\" where 1 = 1");
            List<OracleParameter> pars = null;
            pars = new List<OracleParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (\"Name\" like :keyword or \"Code\" like :keyword)");
                pars.Add(new OracleParameter(":keyword", "%" + keyword.Trim() + "%"));
            }
            return this.FindList<SysDutyEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 获取有效实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        public IList<SysDutyEntity> GetValidList(string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append("select * from \"Sys_Duty\" where 1 = 1 and \"zt\" = '1'");
            List<OracleParameter> pars = null;
            pars = new List<OracleParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (\"Name\" like :keyword or \"Code\" like :keyword)");
                pars.Add(new OracleParameter(":keyword", "%" + keyword.Trim() + "%"));
            }
            return this.FindList<SysDutyEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(SysDutyEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编号不可重复");
                }

                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.Name = entity.Name;
                dbEntity.Code = entity.Code;
                dbEntity.Description = entity.Description;
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