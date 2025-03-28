using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_bmypxx
    /// </summary>
    public class SysPharmacyDepartmentMedicineRepo : RepositoryBase<SysPharmacyDepartmentMedicineEntity>, ISysPharmacyDepartmentMedicineRepo
    {
        public SysPharmacyDepartmentMedicineRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取药品库位
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetYpkw(string ypCode, string yfbmCode, string orgId)
        {
            var sql = "select Ypkw from xt_yp_bmypxx where Ypdm = @ypCode and OrganizeId = @OrganizeId and yfbmCode = @yfbmCode";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@ypCode", ypCode)
                ,new SqlParameter("@OrganizeId", orgId)
                ,new SqlParameter("@yfbmCode", yfbmCode)});
        }

        /// <summary>
        /// 入库部门是否有权限使用该药
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="rkbm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public bool CheckRkbmOwnMedicine(string yp, string rkbm, string organizeId)
        {
            return IQueryable(p => p.OrganizeId == organizeId && p.Ypdm == yp && p.yfbmCode == rkbm).Any();
        }

        /// <summary>
        /// 根据药编码和药房部门编码获取本部门药品信息
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        public IList<SysPharmacyDepartmentMedicineEntity> SelectData(string ypCode, string organizeId, string yfbmCode)
        {
            const string sql = @"
SELECT * FROM dbo.xt_yp_bmypxx(NOLOCK)
WHERE Ypdm=@ypCode
AND OrganizeId=@OrganizeId 
AND yfbmCode=@yfbmCode
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@OrganizeId",organizeId ),
                new SqlParameter("@yfbmCode", yfbmCode),
            };
            return FindList<SysPharmacyDepartmentMedicineEntity>(sql, param);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="bmypId"></param>
        /// <param name="zt"></param>
        /// <param name="lastModifierCode"></param>
        /// <param name="lastModifyTime"></param>
        /// <returns></returns>
        public int UpdateZt(string bmypId, string zt, string lastModifierCode, DateTime lastModifyTime)
        {
            const string sql = @"
UPDATE dbo.xt_yp_bmypxx 
SET zt=@zt, LastModifyTime=@LastModifyTime, LastModifierCode=@LastModifierCode 
WHERE bmypId=@bmypId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@bmypId",bmypId ),
                new SqlParameter("@zt",zt ),
                new SqlParameter("@LastModifyTime", lastModifyTime),
                new SqlParameter("@LastModifierCode",lastModifierCode )
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="bmypId"></param>
        /// <returns></returns>
        public int DeleteItem(string bmypId)
        {
            const string sql = @"DELETE FROM dbo.xt_yp_bmypxx WHERE bmypId=@bmypId ";
            var param = new DbParameter[]
            {
                new SqlParameter("@bmypId",bmypId )
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 根据药品代码修改本部门药品信息
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="zt"></param>
        /// <param name="organizeId"></param>
        /// <param name="lastModifierCode"></param>
        /// <param name="lastModifyTime"></param>
        /// <returns></returns>
        public int UpdateZt(string ypCode, string zt, string organizeId, string lastModifierCode, DateTime lastModifyTime)
        {
            const string sql = @"
UPDATE dbo.xt_yp_bmypxx 
SET zt=@zt, LastModifyTime=@LastModifyTime, LastModifierCode=@LastModifierCode 
WHERE Ypdm=@ypCode
AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypCode",ypCode ),
                new SqlParameter("@zt",zt ),
                new SqlParameter("@OrganizeId",organizeId ),
                new SqlParameter("@LastModifyTime", lastModifyTime),
                new SqlParameter("@LastModifierCode",lastModifierCode )
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int DeleteItem(string ypCode, string organizeId)
        {
            const string sql = @"DELETE FROM dbo.xt_yp_bmypxx WHERE Ypdm=@ypCode AND OrganizeId=@OrganizeId ";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypCode",ypCode ),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}
