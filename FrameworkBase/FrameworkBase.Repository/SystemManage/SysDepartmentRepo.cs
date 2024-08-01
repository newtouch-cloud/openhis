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
    /// 日 期：2017-11-17 15:56
    /// 描 述：系统科室
    /// </summary>
    public sealed class SysDepartmentRepo : RepositoryBase<SysDepartmentEntity>, ISysDepartmentRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysDepartmentRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        public IList<SysDepartmentEntity> GetList(string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_Department(nolock) where 1 = 1");
            List<SqlParameter> pars = null;
            return this.FindList<SysDepartmentEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 获取有效科室列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        public IList<SysDepartmentEntity> GetValidList(string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_Department(nolock) where 1 = 1 and zt = '1'");
            List<SqlParameter> pars = null;
            return this.FindList<SysDepartmentEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(SysDepartmentEntity entity, string keyValue)
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
                dbEntity.py = entity.py;
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

        /// <summary>
        /// 根据Code获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetNameByCode(string code)
        {
            var sql = "select Name from Sys_Department(nolock) where Code = @code";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@code", code) });
        }

    }
}