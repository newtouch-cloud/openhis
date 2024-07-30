using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Text;
using System.Data.SqlClient;
using Newtouch.Core.Common.Exceptions;
using System.Linq;
namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：切口等级
    /// </summary>
    public class ORNotchGradeRepo : RepositoryBase<ORNotchGradeEntity>, IORNotchGradeRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public ORNotchGradeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        public IList<ORNotchGradeEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId)
        {
            var sql = new StringBuilder();
            sql.Append("select * from OR_NotchGrade(nolock) where zt != 0");
            List<SqlParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and organizeid = @organizeId");
                pars.Add(new SqlParameter("@organizeId", organizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or Code like @keyword");
                sql.Append(" or Name like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<ORNotchGradeEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(ORNotchGradeEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue && p.zt == "1"))
                {
                    throw new FailedException("编号不可重复");
                }
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.Code = entity.Code;
                dbEntity.Name = entity.Name;

                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code))
                {
                    throw new FailedException("编号不可重复");
                }
                entity.Create(true);
                return Insert(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public int DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }
    }
}