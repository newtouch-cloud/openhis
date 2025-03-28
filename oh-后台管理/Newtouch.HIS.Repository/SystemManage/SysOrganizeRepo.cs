using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Tools;
using System.Data.SqlClient;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysOrganizeRepo : RepositoryBase<SysOrganizeEntity>, ISysOrganizeRepository
    {
        public SysOrganizeRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取当前组织下的所有组织
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId).ToList();
        }

        /// <summary>
        /// 获取当前组织下的所有组织
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetValidListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId && p.zt == "1").ToList();
        }

        /// <summary>
        /// 获取组织下的所有有效组织（parent）
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVO> GetValidListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM Sys_Organize
              WHERE Id = @parentOrgId  and zt = '1'  --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN Sys_Organize ON cteTree.Id= Sys_Organize.ParentId and Sys_Organize.zt = '1') 
        SELECT  *
          FROM cteTree 

";
            return this.FindList<SysOrganizeVO>(sql, new SqlParameter[] {
                new SqlParameter("@parentOrgId",parentOrgId)
            });
        }

        /// <summary>
        /// 获取组织下的所有组织（parent）（包括无效的）
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVO> GetListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM Sys_Organize
              WHERE Id = @parentOrgId  --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN Sys_Organize ON cteTree.Id= Sys_Organize.ParentId) 
        SELECT  *
          FROM cteTree 

";
            return this.FindList<SysOrganizeVO>(sql, new SqlParameter[] {
                new SqlParameter("@parentOrgId",parentOrgId)
            });
        }

        /// <summary>
        /// 获取顶级组织机构列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysOrganizeEntity> GetPagintionListByTopOrg(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from Sys_Organize
where ParentId is null and
(Name = @keyword or Name like @searchkeyword or Code = @keyword or Code like @searchkeyword)";
            return this.QueryWithPage<SysOrganizeEntity>(sql, pagination, new SqlParameter[] {
                new SqlParameter("@keyword", keyword ?? ""),
                new SqlParameter("@searchkeyword", "%" + (keyword??"") + "%")
            });
        }

        /// <summary>
        /// 获取顶级组织机构列表 包括无效的
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetTopOrgList()
        {
            var sql = @"select * from Sys_Organize where ParentId is null";
            return this.FindList<SysOrganizeEntity>(sql);
        }

        /// <summary>
        /// 获取顶级组织机构列表
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetValidTopOrgList()
        {
            var sql = @"select * from Sys_Organize where zt = '1' and ParentId is null";
            return this.FindList<SysOrganizeEntity>(sql);
        }

        /// <summary>
        /// 提交新建、更新 实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysOrganizeEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (string.IsNullOrWhiteSpace(entity.TopOrganizeId))
                {
                    throw new FailedException("数据异常，未指定顶级组织机构");
                }
                var isCodeRepeated = string.IsNullOrWhiteSpace(entity.ParentId)
                    ? this.IQueryable().Any(p => p.ParentId == null
                        && p.Code == entity.Code && p.Id != keyValue)   //顶级机构中不能有重复code
                    : this.IQueryable().Any(p => p.TopOrganizeId == entity.TopOrganizeId
                        && p.Code == entity.Code && p.Id != keyValue)   //某一顶级机构下 的所有 不能有重复
                    ;
                if (isCodeRepeated)
                {
                    throw new FailedException("编码不可重复");
                }

                SysOrganizeEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysOrganizeEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                entity.Create(true);
                if (string.IsNullOrWhiteSpace(entity.ParentId) && string.IsNullOrWhiteSpace(entity.TopOrganizeId))
                {
                    entity.ParentId = null;
                    entity.TopOrganizeId = entity.Id;   //此为顶级机构
                }
                var isCodeRepeated = string.IsNullOrWhiteSpace(entity.ParentId)
                    ? this.IQueryable().Any(p => p.ParentId == null
                        && p.Code == entity.Code)   //顶级机构中不能有重复code
                    : this.IQueryable().Any(p => p.TopOrganizeId == entity.TopOrganizeId
                        && p.Code == entity.Code)   //某一顶级机构下 的所有 不能有重复
                    ;
                if (isCodeRepeated)
                {
                    throw new FailedException("编码不可重复");
                }

                this.Insert(entity);
            }
        }

        /// <summary>
        /// 根据OrgId获取Code
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCodeById(string orgId)
        {
            var sql = "select Code from Sys_Organize where Id = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}


