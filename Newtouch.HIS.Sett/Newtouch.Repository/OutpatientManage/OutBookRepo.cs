using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.OutpatientManage
{
    public class OutBookRepo : RepositoryBase<OutBookRelEntity>, IOutBookRepo
    {
        public OutBookRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        //根据科室获取医生
        public IList<string> getStaffByKs(string ks,string orgId) {
            string sql = "select ys from [dbo].[mz_ghpb_rel_doc]  where 1=1 and zt='1' and OrganizeId=@orgId";
            //var para = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and ks=@ks ";
                //para.Add(new SqlParameter("@ks", ks));
            }
            //var result = this.FindList<OutBookRelEntity>(sql, para.ToArray()).FirstOrDefault();
            //return result;
            var result= this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@ks",ks),
                  new SqlParameter("@orgId",orgId)
            });
            return result;
        }

        //设置科室下状态为0
        public int updateZt(string ks, string CreateCode, DateTime CreateTime,string orgId) {
            string sql = "update mz_ghpb_rel_doc set zt='0',LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode where ks=@ks and OrganizeId=@orgId";
            SqlParameter[] para ={
                new SqlParameter("@LastModifyTime",Convert.ToDateTime(CreateTime)),
                new SqlParameter("@LastModifierCode",CreateCode ?? ""),
                new SqlParameter("@ks",ks ?? ""),
                 new SqlParameter("@orgId",orgId)
                };
            return this.ExecuteSqlCommand(sql, para);
        }

        public int SubmitForm(string orgId, string ks, string gh, string CreateCode, DateTime CreateTime) {
            if (this.IQueryable().Any(p => p.ks == ks && p.ys == gh))
            {//设置zt=1
                string sql = "update mz_ghpb_rel_doc set zt='1',LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode where ks=@ks and ys=@ys and OrganizeId=@orgId";
                SqlParameter[] para ={
                new SqlParameter("@LastModifyTime",Convert.ToDateTime(CreateTime)),
                new SqlParameter("@LastModifierCode",CreateCode ?? ""),
                new SqlParameter("@ks",ks ?? ""),
                new SqlParameter("@ys",gh ?? ""),
                new SqlParameter("@orgId",orgId)
                };
                return this.ExecuteSqlCommand(sql, para);
            }
            else {//新增
                var entity = new OutBookRelEntity();
                entity.OrganizeId = orgId;
                entity.ks = ks;
                entity.ys = gh;
                entity.zt = "1";
                entity.CreatorCode = CreateCode;
                entity.CreateTime = Convert.ToDateTime(CreateTime);
                return Insert(entity);
            }
        }

        public int UpdateForm(int keyValue,string orgId, string ks,string gh, string CreateCode, DateTime CreateTime)
        {
            if (this.IQueryable().Any(p => p.ks == ks && p.zt == "1"))
            {//更新
                //var dbEntity = this.FindEntity(ks);
                ////properties
                //dbEntity.ks = ks;
                //dbEntity.ys = gh;
                //dbEntity.zt = "1";
                //dbEntity.Modify(ks);
                //return Update(dbEntity);
                //var para = new List<SqlParameter>();
                //string sql = "update mz_ghpb_rel_doc set zt='1'";
                //if (gh != null) {
                //    sql += ",ys=@ys";
                //    para.Add(new SqlParameter("@ys", gh));
                //}
                //if (CreateCode != null)
                //{
                //    sql += ",LastModifierCode=@LastModifierCode";
                //    para.Add(new SqlParameter("@LastModifierCode", CreateCode));
                //}
                //if (CreateTime != null)
                //{
                //    sql += ",LastModifyTime=@LastModifyTime";
                //    para.Add(new SqlParameter("@LastModifyTime", CreateTime));
                //}
                //sql += " where ks=@ks";
                //para.Add(new SqlParameter("@ks", ks));

                string sql = "update mz_ghpb_rel_doc set ys=@ys,zt='1',LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode where ks=@ks and OrganizeId=@orgId";
                SqlParameter[] para ={
                new SqlParameter("@ys",gh ?? ""),
                new SqlParameter("@LastModifyTime",Convert.ToDateTime(CreateTime)),
                new SqlParameter("@LastModifierCode",CreateCode ?? ""),
                new SqlParameter("@ks",ks ?? ""),
                new SqlParameter("@orgId",orgId)
                };
                return this.ExecuteSqlCommand(sql, para);
            }
            else
            {//新增
                var entity = new OutBookRelEntity();
                entity.OrganizeId = orgId;
                entity.ks = ks;
                entity.ys = gh;
                entity.zt = "1";
                entity.CreatorCode = CreateCode;
                //entity.CreateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", CreateTime).ToDate();
                entity.CreateTime=Convert.ToDateTime(CreateTime);
                return Insert(entity);
                //return 0;
            }
        }
    }
}
