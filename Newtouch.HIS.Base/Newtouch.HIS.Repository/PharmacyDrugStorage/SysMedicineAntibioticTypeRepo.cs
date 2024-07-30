using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineAntibioticTypeRepo : RepositoryBase<SysMedicineAntibioticTypeEntity>, ISysMedicineAntibioticTypeRepo
    {
        public SysMedicineAntibioticTypeRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据组织机构,级别,获取抗生素信息列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineAntibioticTypeEntity> GetValidListByOrg(string OrganizeId, string Level, string parentId)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }
            var sql = @"select Id ,OrganizeId ,typeName ,typelevel ,qxjb ,qxjbmc ,
                            parentId ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode ,zt 
                        from xt_kssType where zt='1' and OrganizeId=@OrganizeId
                        and typelevel=@Level ";
            if (string.IsNullOrWhiteSpace(parentId))
            {
                sql += " and parentId IS NULL ";
            }
            else
            {
                sql += " and parentId = @parentId ";
            }
            sql += " order by typeName asc";
            return this.FindList<SysMedicineAntibioticTypeEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@Level", Level),
                new SqlParameter("@parentId", string.IsNullOrWhiteSpace(parentId) ? string.Empty : parentId) });
        }


        public IList<SysMedicineAntibioticTypeEntity> GetListByParentId(string OrganizeId, string parentId)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }
            var sql = @"select Id ,OrganizeId ,typeName ,typelevel ,qxjb ,qxjbmc ,
                            parentId ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode ,zt 
                        from xt_kssType where zt='1' and OrganizeId=@OrganizeId ";
            if (string.IsNullOrWhiteSpace(parentId))
            {
                sql += " and parentId IS NULL ";
            }
            else
            {
                sql += " and parentId = @parentId ";
            }
            sql += " order by typeName asc";
            return this.FindList<SysMedicineAntibioticTypeEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@parentId", string.IsNullOrWhiteSpace(parentId) ? string.Empty : parentId) });
        }

        /// <summary>
        /// 提交抗生素类别信息
        /// </summary>
        /// <param name="entity"></param>
        public void SubmitForm(SysMedicineAntibioticTypeEntity entity)
        {

            if (!string.IsNullOrEmpty(entity.Id))
            {
                var dbEntity = this.FindEntity(p => p.Id == entity.Id && p.OrganizeId == entity.OrganizeId);
                dbEntity.typeName = entity.typeName;
                dbEntity.typelevel = entity.typelevel;
                dbEntity.qxjb = entity.qxjb;
                dbEntity.qxjbmc = entity.qxjbmc;
                dbEntity.zt = entity.zt;
                dbEntity.Modify(entity.Id);
                this.Update(dbEntity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}
