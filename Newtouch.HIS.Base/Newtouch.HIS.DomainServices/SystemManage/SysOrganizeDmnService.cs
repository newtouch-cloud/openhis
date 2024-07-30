using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 组织机构DmnService
    /// </summary>
    public class SysOrganizeDmnService : DmnServiceBase, ISysOrganizeDmnService
    {
        private readonly ISysOrganizeRepository _sysOrganizeRepository;

        public SysOrganizeDmnService(IBaseDatabaseFactory databaseFactory
            , ISysOrganizeRepository sysOrganizeRepository)
            : base(databaseFactory)
        {
            this._sysOrganizeRepository = sysOrganizeRepository;
        }

        /// <summary>
        /// 获取组织机构已授权的应用列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysApplicationEntity> GetAuthedAppListByTopOrgId(string topOrgId)
        {
            var sql = @"select * from sys_application
where Id in 
(
select ApplicationId from Sys_OrganizeApplication where ApplicationId <> '*' and OrganizeId = '*' and zt = '1'
union
select ApplicationId from Sys_OrganizeApplication where OrganizeId in (select Id from Sys_Organize where TopOrganizeId = @topOrgId and zt = '1') and zt = '1'
)";
            return this.FindList<SysApplicationEntity>(sql, new SqlParameter[] {
                new SqlParameter("@topOrgId", topOrgId)
            });
        }

        /// <summary>
        /// 是否含有下级机构，有下级机构返回true
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsHasLowerOrganize(string orgId)
        {
            var Sql = @"select 1 from Sys_Organize where ParentId = @orgId";
            return this.FirstOrDefault<int>(Sql, new[] { new SqlParameter("@orgId", orgId) }) == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsHospital(string orgId)
        {
            var sql = "select CategoryCode from Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Hospital";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsClinic(string orgId)
        {
            var sql = "select CategoryCode from Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Clinic";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsMedicalOrganize(string orgId)
        {
            var sql = "select CategoryCode from Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Hospital" || code == "Clinic";
        }

        /// <summary>
        /// 获取Org名称
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetNameByOrgId(string orgId)
        {
            var sql = @"select Name from Sys_Organize where Id = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

        /// <summary>
        /// 获取 UserId 对应的 医疗机构 List
        /// </summary>
        /// <returns></returns>
        public IList<SysOrganizeEntity> GetMedicalOrganizeListByUserId(string userId)
        {
            var sql = @"select distinct c.* from Sys_UserStaff a
left join Sys_Staff b
on a.StaffId = b.Id
left join Sys_Organize c
on b.OrganizeId = c.Id

where a.zt = '1' and b.zt = '1' and c.zt = '1' and c.Id is not null and a.UserId = @userId
--and (c.CategoryCode = 'Hospital' or c.CategoryCode = 'Clinic')";
            return this.FindList<SysOrganizeEntity>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        #region 科室

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        public List<SysDepartmentVO> GetListByOrg(string organizeId)
        {
            var sql = @"select de.Name gjksmc,org.Name OrganizeName,dept.* 
from Sys_Department dept
left join Sys_Organize org on org.Id = dept.OrganizeId
left join Sys_Items it on it.Name like '%医保科室%'
left join Sys_ItemsDetail de  on it.id = de.ItemId and de.OrganizeId= dept.OrganizeId  and de.zt=1 and dept.ybksbm=de.Code
where dept.OrganizeId = @OrganizeId
order by dept.CreateTime desc";
            return this.FindList<SysDepartmentVO>(sql, new[] { new SqlParameter("@OrganizeId", organizeId ?? "") });
        }

        /// <summary>
        /// 更新科室关联病区
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="wardList">病区Code List</param>
        /// <param name="OrganizeId"></param>
        public void UpdateDepartmentWard(string deptId, string[] wardList, string OrganizeId)
        {
            var wards = new List<SysDepartmentWardRelationEntity>();
            foreach (var item in wardList.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
            {
                var entity = new SysDepartmentWardRelationEntity();
                entity.Create(true);
                entity.DepartmentId = deptId;
                entity.DepartmentCode = "";
                entity.bqCode = item;
                entity.OrganizeId = OrganizeId;
                entity.zt = "1";
                wards.Add(entity);
            }

            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var deptCode = db.IQueryable<SysDepartmentEntity>(d => d.Id == deptId).FirstOrDefault().Code;
                var oldList = db.IQueryable<SysDepartmentWardRelationEntity>().Where(p => p.DepartmentId == deptId).ToList();
                for (int i = 0; i < wards.Count; i++)
                {
                    if (oldList.Any(p => p.bqCode == wards[i].bqCode))
                    {
                        oldList.Remove(oldList.Where(p => p.bqCode == wards[i].bqCode).First());
                        continue;
                    }
                    wards[i].DepartmentCode = deptCode;
                    db.Insert(wards[i]);
                }
                foreach (var item in oldList)
                {
                    db.Delete(item);
                }
                db.Commit();
            }
        }

        #endregion

    }
}
