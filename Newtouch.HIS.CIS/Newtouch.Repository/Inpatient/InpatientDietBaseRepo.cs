using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using System.Linq;

namespace Newtouch.Repository
{
    
    public class InpatientDietBaseRepo : RepositoryBase<InpatientDietBaseEntity>, IInpatientDietBaseRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public InpatientDietBaseRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(InpatientDietBaseEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //var dbEntity = this.FindEntity(keyValue);
                //dbEntity.Modify(keyValue);
                //this.Update(dbEntity);
                //entity.Modify(keyValue);
                //this.Update(entity);
                InpatientDietBaseEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        public  IList<InpatientDietBaseEntity> GetGridList(Pagination pagination, string lb, string keyword,string orgId) {
            var rntlist = new List<InpatientDietBaseEntity>();
            IList<SqlParameter> parlist = new List<SqlParameter>();
            var sql = new StringBuilder();
            sql.Append(@"SELECT  Id ,
                        OrganizeId ,
                        Name ,
                        DietType ,
                        DietGroup ,
                        DietBigType ,
                        py ,
                        ParentId ,
                        bdsfxm ,
                        CreateTime ,
                        CreatorCode,
                        LastModifyTime,
                        LastModifierCode,
                        zt
                FROM    zy_DietBase where 1=1 ");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (name like @keyword or py like @keyword)");
                parlist.Add(new SqlParameter("@keyword", "%"+keyword+"%"));
            }
            if (!string.IsNullOrWhiteSpace(lb))
            {
                sql.Append(" and DietBigType = @lb");
                parlist.Add(new SqlParameter("@lb", lb));
            }
            sql.Append(" and OrganizeId = @orgId");
            parlist.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<InpatientDietBaseEntity>(sql.ToString(), pagination, parlist.ToArray());
        }
    }
}