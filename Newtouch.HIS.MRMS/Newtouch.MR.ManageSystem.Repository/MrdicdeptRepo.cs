using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Newtouch.Core.Common;
using System.Linq;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 13:45
    /// 描 述：病案科室
    /// </summary>
    public class MrdicdeptRepo : RepositoryBase<MrdicdeptEntity>, IMrdicdeptRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public MrdicdeptRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(MrdicdeptEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.ksbm == entity.ksbm && p.Id != keyValue && p.zt == "1"))
                {
                    throw new FailedException("编号不可重复");
                }
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.ksbm = entity.ksbm;
                dbEntity.ksmc = entity.ksmc;

                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.ksbm == entity.ksbm))
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

        /// <summary>
        /// 获取下拉框中的病案科室列表
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<MrdicdeptEntity> GetDicDeptList(string organizeId)
        {
            string sql = @"select * from mr_dic_dept(nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            var result = this.FindList<MrdicdeptEntity>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId)
            });
            return result;
        }

        /// <summary>
        /// 分页获取病案列表
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<MrdicdeptEntity> GetPagintionDicDeptList(Pagination pagination, string organizeId, string keyword)
        {

            var sql = new StringBuilder();
            sql.Append("select * from mr_dic_dept(nolock) where zt != 0");
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
                sql.Append(" or ksbm like @keyword");
                sql.Append(" or ksmc like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<MrdicdeptEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }
    }
}