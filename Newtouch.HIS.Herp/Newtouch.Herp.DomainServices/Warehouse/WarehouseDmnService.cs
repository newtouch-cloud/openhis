using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using EFDbTransaction = Newtouch.Infrastructure.EF.EFDbTransaction;

namespace Newtouch.Herp.DomainServices.Warehouse
{
    /// <summary>
    /// 库房操作
    /// </summary>
    public class WarehouseDmnService : DmnServiceBase, IWarehouseDmnService
    {
        private readonly IKfWarehouseRepo _kfWarehouseRepo;

        public WarehouseDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// insert new warehouse information
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="userList"></param>
        /// <param name="deptList"></param>
        public void InsertWarehouse(KfWarehouseEntity wzEntity, List<RelWarehouseUserEntity> userList, List<RelWarehouseDeptEntity> deptList)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                if (wzEntity != null) db.Insert(wzEntity);
                if (deptList != null && deptList.Count > 0) db.Insert(deptList);
                if (userList != null && userList.Count > 0) db.Insert(userList);
                db.Commit();
            }
        }

        /// <summary>
        /// update warehouse information
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="deptList"></param>
        /// <param name="userList"></param>
        public void UpdateWarehouse(KfWarehouseEntity wzEntity, List<RelWarehouseUserEntity> userList, List<RelWarehouseDeptEntity> deptList)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                var dbwzEntity = _kfWarehouseRepo.FindEntity(p => p.Id == wzEntity.Id);
                dbwzEntity.address = wzEntity.address;
                dbwzEntity.level = wzEntity.level;
                dbwzEntity.name = wzEntity.name;
                dbwzEntity.parentId = wzEntity.parentId;
                dbwzEntity.py = wzEntity.py;
                dbwzEntity.remark = wzEntity.remark;
                dbwzEntity.px = wzEntity.px;
                dbwzEntity.zt = wzEntity.zt;
                dbwzEntity.isDefSyn = wzEntity.isDefSyn;
                dbwzEntity.LastModifierCode = wzEntity.LastModifierCode;
                dbwzEntity.LastModifyTime = wzEntity.LastModifyTime;
                db.Update(dbwzEntity);
                db.Delete<RelWarehouseDeptEntity>(d => d.warehouseId == dbwzEntity.Id);
                deptList.ForEach(p =>
                {
                    db.Insert(p);
                });
                db.Delete<RelWarehouseUserEntity>(d => d.warehouseId == dbwzEntity.Id);
                userList.ForEach(p =>
                {
                    db.Insert(p);
                });
                db.Commit();
            }
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<VKfWarehouseEntity> QueryWarehouseInfo(string keyWord, string organizeId, string zt = "1")
        {
            const string sql = @"
SELECT kfw.Id, kfw.name, kfw.[address], kfw.[level], kfw.remark, kfw.zt, kfw.isDefSyn,kfw.CreatorCode, kfw.CreateTime, kfw.LastModifierCode, kfw.LastModifyTime, kfw.OrganizeId
, STUFF(( SELECT    ',' + relu.userName
                          FROM dbo.rel_warehouseUser(NOLOCK) relu
                          WHERE relu.warehouseId = kfw.Id AND relu.OrganizeId=kfw.OrganizeId AND relu.zt='1'
                        FOR
                          XML PATH('')
                        ), 1, 1, '') admins
,STUFF(( SELECT    ',' + reld.deptName
                          FROM dbo.rel_warehouseDept(NOLOCK) reld
                          WHERE reld.warehouseId = kfw.Id AND reld.OrganizeId=kfw.OrganizeId AND reld.zt='1'
                        FOR
                          XML PATH('')
                        ), 1, 1, '') deptNames
FROM dbo.kf_warehouse(NOLOCK) kfw
WHERE kfw.OrganizeId=@OrganizeId
AND (kfw.zt=@zt OR ''=@zt ) 
AND (kfw.name LIKE '%'+@keyWord+'%' OR kfw.Id LIKE '%'+@keyWord+'%')
order by isnull(kfw.px, 99999)
";
            var param = new DbParameter[]
            {
                new SqlParameter("@keyWord", keyWord??""),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@zt", zt)
            };
            return FindList<VKfWarehouseEntity>(sql, param);
        }

        /// <summary>
        /// 删除库房
        /// </summary>
        /// <param name="id">库房Id</param>
        public void DeleteWarehouse(string id)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Delete<KfWarehouseEntity>(p => p.Id == id);
                db.Delete<RelProductWarehouseEntity>(p => p.warehouseId == id);
                db.Delete<RelWarehouseDeptEntity>(p => p.warehouseId == id);
                db.Delete<RelWarehouseUserEntity>(p => p.warehouseId == id);
                db.Commit();
            }
        }

        /// <summary>
        /// 获取库房管理员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VKfUserInfoEntity> GetKfUserInfo(string userId, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT s.gh, s.Name staffName, kf.Id kfId, kf.name kfName, kf.[level] kfLeve, ISNULL(d.Name, '') dutyName
FROM NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) us
INNER JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) s ON s.Id=us.StaffId AND s.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_StaffDuty(NOLOCK) sd ON sd.StaffId=s.Id AND sd.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Duty(NOLOCK) d ON d.Id=sd.DutyId AND d.zt='1'
INNER JOIN dbo.rel_warehouseUser(NOLOCK) rel ON rel.gh=s.gh AND rel.OrganizeId=s.OrganizeId AND rel.zt='1'
INNER JOIN dbo.kf_warehouse(NOLOCK) kf ON kf.Id=rel.warehouseId AND kf.OrganizeId=s.OrganizeId AND kf.zt='1'
WHERE s.OrganizeId=@OrganizeId
AND us.UserId=@UserId
AND us.zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@UserId", userId??""),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<VKfUserInfoEntity>(sql, param);
        }

        /// <summary>
        /// 获取库房同步物资
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="warehouseId">库房ID</param>
        /// <param name="wzlb">物资类别</param>
        /// <param name="keyWord">查询关键字</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VSyncProductEntity> GetSyncProductList(Pagination pagination, string warehouseId, string wzlb, string keyWord, string organizeId)
        {
            const string sql = @"
SELECT ISNULL(rel.Id,'') Id, lb.name wzlb, wz.Id productId, wz.name productName, wz.gg, wz.lsj, dw.name zxdw, rel.unitId, ISNULL(dw2.name,'') bmdw, ISNULL(gys.name, '') sccj, ISNULL(rel.zt, '0') zt
FROM dbo.wz_product(NOLOCK) wz
LEFT JOIN dbo.rel_productWarehouse(NOLOCK) rel ON rel.productId=wz.Id AND rel.OrganizeId=wz.OrganizeId AND rel.warehouseId=@warehouseId
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId AND gys.OrganizeId=rel.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) dw ON dw.Id=wz.minUnit
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) dw2 ON dw2.Id=rel.unitId
WHERE wz.OrganizeId=@OrganizeId
AND wz.zt='1' 
AND (wz.typeId=@lbId OR ''=@lbId)
AND (wz.name LIKE '%'+@keyWord+'%' OR wz.py LIKE '%'+@keyWord+'%' OR wz.Id=@keyword)
";
            var param = new DbParameter[]
            {
                new SqlParameter("@warehouseId", warehouseId??""),
                new SqlParameter("@lbId", wzlb??""),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@keyWord", keyWord)
            };
            return QueryWithPage<VSyncProductEntity>(sql, pagination, param);
        }

        /// <summary>
        /// 获取库房物资
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="lb"></param>
        /// <param name="kzbz"></param>
        /// <param name="zt"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public IList<VWarehouseProductEntity> GetWarehouseProducts(Pagination pagination, string keyWord, string lb, string kzbz, string zt
            , string organizeId, string warehouseId)
        {
            const string sql = @"
SELECT rpw.productId, wz.name wzmc, rpw.zt kzbz, wz.zt wzzt,wz.py, lb.name lb, wz.zxqds, ISNULL(CONVERT(NUMERIC(11,4),wz.lsj),0) lsj, dw.name zxdwmc
,wz.gg, wz.brand, gys.name sccj, wz.sflkc, wz.sffy, wz.sfgt,wz.gjybdm
FROM dbo.rel_productWarehouse(NOLOCK) rpw
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=rpw.productId AND wz.OrganizeId=rpw.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) dw ON dw.Id=wz.minUnit AND dw.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId AND gys.zt='1' AND gys.OrganizeId=rpw.OrganizeId
WHERE rpw.warehouseId=@warehouseId
AND rpw.OrganizeId=@OrganizeId
AND (wz.name LIKE '%'+ @keyWord +'%' OR wz.py LIKE '%'+ @keyWord +'%')
AND (wz.typeId=@lbId OR ''=@lbId)
AND (rpw.zt=@kzbz OR ''=@kzbz)
AND (wz.zt=@wzzt OR ''=@wzzt)
";
            var param = new DbParameter[]
            {
                new SqlParameter("@warehouseId", warehouseId ?? ""),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@lbId", lb ?? ""),
                new SqlParameter("@kzbz", kzbz ?? ""),
                new SqlParameter("@wzzt", zt ?? ""),
                new SqlParameter("@keyWord", keyWord??"")
            };
            return QueryWithPage<VWarehouseProductEntity>(sql, pagination, param);
        }
    }
}
