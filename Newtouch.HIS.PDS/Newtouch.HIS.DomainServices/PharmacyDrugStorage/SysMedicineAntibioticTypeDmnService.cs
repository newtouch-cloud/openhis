using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SysMedicineAntibioticTypeDmnService : DmnServiceBase, ISysMedicineAntibioticTypeDmnService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysMedicineAntibioticTypeDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }


        public SysMedicineAntibioticTypeVO GetById(string orgId, string Id)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_kssType where OrganizeId=@orgId";
            var parList = new List<SqlParameter>() { };
            parList.Add(new SqlParameter("@orgId", orgId ?? ""));
            if (!string.IsNullOrEmpty(Id))
            {
                strSql += " and Id = @Id";
                parList.Add(new SqlParameter("@Id", Id));
            }
            return this.FirstOrDefault<SysMedicineAntibioticTypeVO>(strSql, parList.ToArray());
        }


        /// <summary>
        /// 根据组织机构,级别,获取抗生素信息列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineAntibioticTypeVO> GetValidListByOrg(string OrganizeId, string Level, string parentId)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }
            var sql = @"select Id ,OrganizeId ,typeName ,typelevel ,qxjb ,qxjbmc ,
                            parentId ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode ,zt 
                        from [NewtouchHIS_Base].dbo.xt_kssType where zt='1' and OrganizeId=@OrganizeId
                        and typelevel=@Level ";
            if (string.IsNullOrWhiteSpace(parentId))
            {
                sql += " and (parentId IS NULL or parentId='' )";
            }
            else
            {
                sql += " and parentId = @parentId ";
            }
            sql += " order by typeName asc";
            return this.FindList<SysMedicineAntibioticTypeVO>(sql, new SqlParameter[] {
                    new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@Level", Level),
                new SqlParameter("@parentId", string.IsNullOrWhiteSpace(parentId) ? string.Empty : parentId) });
        }


        public IList<SysMedicineAntibioticTypeVO> GetListByParentId(string OrganizeId, string parentId)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }
            var sql = @"select Id ,OrganizeId ,typeName ,typelevel ,qxjb ,qxjbmc ,
                            parentId ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode ,zt 
                        from [NewtouchHIS_Base].dbo.xt_kssType where zt='1' and OrganizeId=@OrganizeId ";
            if (string.IsNullOrWhiteSpace(parentId))
            {
                sql += " and parentId IS NULL ";
            }
            else
            {
                sql += " and parentId = @parentId ";
            }
            sql += " order by typeName asc";
            return this.FindList<SysMedicineAntibioticTypeVO>(sql, new SqlParameter[] {
                    new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@parentId", string.IsNullOrWhiteSpace(parentId) ? string.Empty : parentId) });
        }

        /// <summary>
        /// 提交抗生素类别信息
        /// </summary>
        /// <param name="entity"></param>
        public void SubmitForm(SysMedicineAntibioticTypeVO entity)
        {

            if (!string.IsNullOrEmpty(entity.Id))
            {
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"  UPDATE [NewtouchHIS_Base].dbo.xt_kssType
SET OrganizeId=@OrganizeId,typeName=@typeName,typelevel=@typelevel,qxjb=@qxjb,qxjbmc=@qxjbmc,parentId=@parentId,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode,zt=@zt
where Id=@Id");
                        var paraList = new DbParameter[]
                        {
                        new SqlParameter("@Id", entity.Id),
                        new SqlParameter("@OrganizeId", entity.OrganizeId),
                        new SqlParameter("@typeName", entity.typeName),
                        new SqlParameter("@typelevel", entity.typelevel),
                        new SqlParameter("@qxjb", entity.qxjb==null?"":entity.qxjb),
                        new SqlParameter("@qxjbmc", entity.qxjbmc==null?"":entity.qxjbmc),
                        new SqlParameter("@parentId", entity.parentId==null?"":entity.parentId),
                        new SqlParameter("@LastModifyTime", DateTime.Now),
                        new SqlParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode),
                        new SqlParameter("@zt", entity.zt),
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        db.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
            else
            {
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"insert into [NewtouchHIS_Base].dbo.xt_kssType
                                  (Id,OrganizeId ,typeName,typelevel,qxjb,qxjbmc,parentId,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt)
                                  values (newid(),@OrganizeId,@typeName,@typelevel,@qxjb,@qxjbmc,@parentId,@CreateTime,@CreatorCode,null,null,@zt);");
                        var paraList = new DbParameter[]
                        {
                        new SqlParameter("@OrganizeId", entity.OrganizeId),
                        new SqlParameter("@typeName", entity.typeName),
                        new SqlParameter("@typelevel", entity.typelevel),
                        new SqlParameter("@qxjb", entity.qxjb==null?"":entity.qxjb),
                        new SqlParameter("@qxjbmc", entity.qxjbmc==null?"":entity.qxjbmc),
                        new SqlParameter("@parentId", entity.parentId==null?"":entity.parentId),
                        new SqlParameter("@CreateTime", DateTime.Now),
                        new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        new SqlParameter("@zt", entity.zt),
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        db.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
        }
    }
}
