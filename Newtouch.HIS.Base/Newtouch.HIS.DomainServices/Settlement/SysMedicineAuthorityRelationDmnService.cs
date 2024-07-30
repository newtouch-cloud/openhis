using System;
using System.Collections.Generic;
using Newtouch.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.Settlement;
using Newtouch.HIS.Domain.Entity.Settlement;
using System.Linq;

namespace Newtouch.HIS.DomainServices.Settlement
{
    public class SysMedicineAuthorityRelationDmnService : DmnServiceBase, ISysMedicineAuthorityRelationDmnService
    {
        public SysMedicineAuthorityRelationDmnService(IBaseDatabaseFactory databaseFactory)
       : base(databaseFactory)
        {
        }
        public IList<SysMedicineAuthorityRelationVO> GetGridQx(Pagination pagination, string gh,string orgId,  string keyword = null)
        {
            var sql = @"select a.qxCode,a.qxmc,c.name,c.DepartmentCode,d.name departmentname,b.* from  xt_qxkz a
left join xt_qxkz_rel b
on a.qxId=b.qxId and a.organizeId=b.organizeId and a.zt=b.zt
left join Sys_Staff c
on b.gh=c.gh and b.organizeId=c.organizeId
left join Sys_Department d
on c.DepartmentCode=d.code
where 
b.gh=@gh 
and b.OrganizeId = @orgId
";
            if (!string.IsNullOrWhiteSpace(keyword)) {
                sql += " and(a.qxCode like @searchKeyword or a.qxmc like @searchKeyword)";
            }
            var pars = new List<SqlParameter>() {
                new SqlParameter("@gh", gh),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") 
        };
            return this.QueryWithPage<SysMedicineAuthorityRelationVO>(sql, pagination, pars.ToArray());
        }

        public IList<SysMedicineAuthorityRelationVO> GetListBygh(string gh,string orgId,string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(gh))
            {
                return null;
            }
            var sql = @"select a.qxCode,a.qxmc,c.name,c.DepartmentCode,d.name departmentName,b.* from  xt_qxkz a
left join xt_qxkz_rel b
on a.qxId = b.qxId and a.organizeId = b.organizeId and a.zt = b.zt
left join Sys_Staff c
on b.gh = c.gh and b.organizeId = c.organizeId
left join Sys_Department d
on c.DepartmentCode=d.code
where
b.gh = @gh
and b.OrganizeId = @orgId
";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and(a.qxCode like @searchKeyword or a.qxmc like @searchKeyword)";
            }
            return this.FindList<SysMedicineAuthorityRelationVO>(sql, new[] { new SqlParameter("@gh", gh),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")  });
        }

        
        public void UpdateAuthority(string gh, string organizeId,string keyword,string[] AuthorityList)
        {
            if (string.IsNullOrWhiteSpace(gh) || AuthorityList == null || AuthorityList.Count() == 0)
            {
                return;
            }
            //权限list
            var authorityLists = new List<SysMedicineAuthorityRelationEntity>();
            foreach (var item in AuthorityList.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
            {
                var entity = new SysMedicineAuthorityRelationEntity();
                entity.Create(true);
                entity.qxId = item;
                entity.gh = gh;
                entity.OrganizeId = organizeId;
                entity.zt = "1";
                authorityLists.Add(entity);
            }

            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var oldAuthorityList = db.IQueryable<SysMedicineAuthorityRelationEntity>().Where(p => p.gh == gh).ToList();
                //var oldAuthorityList = GetListBygh(gh, organizeId);
                for (int i = 0; i < authorityLists.Count; i++)
                {
                    if (oldAuthorityList.Any(p => p.qxId == authorityLists[i].qxId))
                    {
                        oldAuthorityList.Remove(oldAuthorityList.Where(p => p.qxId == authorityLists[i].qxId).First());
                        continue;
                    }
                    db.Insert(authorityLists[i]);
                }
                foreach (var item in oldAuthorityList)
                {
                    db.Delete(item);
                }
                db.Commit();
            }

        }

        /// <summary>
        /// 获取系统人员 分页列表
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysStaffVO> GetStaffPaginationList(Pagination pagination, string OrganizeId, string keyword = null)
        {
            var sql = @"select dept.Name DepartmentName, org.Name OrganizeName, staff.* from Sys_Staff staff
left join Sys_Department dept
on dept.Code= staff.DepartmentCode and dept.OrganizeId = staff.OrganizeId
left join Sys_Organize org
on org.Id = staff.OrganizeId
where
staff.organizeId = @OrganizeId
and (@keyword = '' or staff.Name = @keyword or staff.Name like @searchkeyword or staff.py = @keyword or staff.py like @searchkeyword or gh = @keyword or gh like @searchkeyword or dept.Name = @keyword or dept.Name like @searchkeyword)";

            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@keyword",keyword ?? ""),
                new SqlParameter("@searchkeyword","%" + keyword + "%" )
            };

            return this.QueryWithPage<SysStaffVO>(sql, pagination, par);
        }

    }
}
