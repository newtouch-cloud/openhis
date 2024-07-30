using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// warehouse operation
    /// </summary>
    public class WarehouseApp : AppBase, IWarehouseApp
    {
        private readonly IRelWarehouseDeptRepo relWarehouseDeptRepo;
        private readonly IKfWarehouseRepo kfWarehouseRepo;
        private readonly IWarehouseDmnService warehouseDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly FrameworkBase.MultiOrg.Domain.IRepository.ISysStaffRepo _sysStaffRepo;
        private readonly IRelProductWarehouseRepo _relProductWarehouseRepo;
        private readonly IWzProductRepo _wzProductRepo;

        /// <summary>
        /// submit warehouse maintenance form
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="staffghs"></param>
        /// <param name="departmentIds"></param>
        /// <param name="keyWord"></param>
        public void SubmitForm(KfWarehouseEntity wzEntity, string[] staffghs, string[] departmentIds, string keyWord)
        {
            var tempWarehouseId = Guid.NewGuid().ToString();
            var staffs = AssembleRelWarehouseUserEntity(string.IsNullOrWhiteSpace(keyWord) ? tempWarehouseId : keyWord, staffghs);
            var depts = AssembleRelwarehouseDeptEntity(string.IsNullOrWhiteSpace(keyWord) ? tempWarehouseId : keyWord, departmentIds);
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                wzEntity.Create(true, tempWarehouseId);
                warehouseDmnService.InsertWarehouse(wzEntity, staffs, depts);
            }
            else
            {
                wzEntity.Modify(keyWord);
                warehouseDmnService.UpdateWarehouse(wzEntity, staffs, depts);
            }
        }

        /// <summary>
        /// assemble RelWarehouseUserEntity
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="staffghs"></param>
        /// <returns></returns>
        private List<RelWarehouseUserEntity> AssembleRelWarehouseUserEntity(string keyWord, string[] staffghs)
        {
            var result = new List<RelWarehouseUserEntity>();
            if (staffghs == null || staffghs.Length == 0
                || (staffghs.Length == 1 && string.IsNullOrWhiteSpace(staffghs[0]))) return result;
            var staff = _sysStaffRepo.GetValidStaffListByOrganizeId(OrganizeId);
            result.AddRange(from gh in staffghs
                            let user = staff.FirstOrDefault(p => p.gh == gh) ?? new FrameworkBase.MultiOrg.Domain.Entity.SysStaffVEntity()
                            select new RelWarehouseUserEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                gh = gh,
                                userName = user.Name,
                                OrganizeId = OrganizeId,
                                zt = "1",
                                CreateTime = DateTime.Now,
                                CreatorCode = UserIdentity.UserCode,
                                warehouseId = keyWord
                            });

            return result;
        }

        /// <summary>
        /// assemble RelwarehouseDeptEntity
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="departmentIds"></param>
        /// <returns></returns>
        private List<RelWarehouseDeptEntity> AssembleRelwarehouseDeptEntity(string keyWord, string[] departmentIds)
        {
            var result = new List<RelWarehouseDeptEntity>();
            if (departmentIds == null || departmentIds.Length == 0
                || (departmentIds.Length == 1 && string.IsNullOrWhiteSpace(departmentIds[0]))) return result;
            var depts = _sysDepartmentRepo.GetList(OrganizeId, "1").ToList();
            result.AddRange(from deptId in departmentIds
                            let dept = depts.FirstOrDefault(p => p.Id == deptId) ?? new SysDepartmentVEntity()
                            select new RelWarehouseDeptEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                deptId = deptId,
                                deptName = dept.Name,
                                OrganizeId = OrganizeId,
                                zt = "1",
                                warehouseId = keyWord,
                                CreateTime = DateTime.Now,
                                CreatorCode = UserIdentity.UserCode
                            });
            return result;
        }

        #region  库房物资同步

        /// <summary>
        /// 同步库房物资
        /// </summary>
        /// <param name="productIds">物资ID</param>
        /// <param name="opereateType">0:添加  1：删除</param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId">库房</param>
        /// <returns></returns>
        public bool FreshWhAndwzRelList(List<string> productIds, int opereateType, string organizeId, string warehouseId)
        {
            if (productIds == null || productIds.Count <= 0) return true;
            var rowCount = 0;
            while (rowCount < productIds.Count)
            {
                var tmp = productIds.Take(50).ToList();
                rowCount += 50;
                switch (opereateType)
                {
                    case 0:// insert
                        return AddProductInWarehouse(tmp, organizeId, warehouseId) > 0;
                    case 1:// delete
                        return DeleteWarehouseProduct(tmp, organizeId, warehouseId) > 0;
                }
            }
            return true;
        }

        /// <summary>
        /// 库房添加新物资
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        private int AddProductInWarehouse(List<string> productIds, string organizeId, string warehouseId)
        {
            var targetPro = AssembleTargetProduct(productIds, organizeId);
            if (targetPro.Count <= 0) return 0;
            var warehouse = kfWarehouseRepo.FindEntity(p => p.Id == warehouseId && p.OrganizeId == organizeId && p.zt == ((int)Enumzt.Enable).ToString());
            if (warehouse == null) return 0;
            var targetRel = new List<RelProductWarehouseEntity>();
            var existPro = _relProductWarehouseRepo.IQueryable(p => p.warehouseId == warehouseId && p.OrganizeId == OrganizeId).Select(q => q.productId).ToList();
            targetPro.ForEach(p =>
            {
                if (existPro.Contains(p.Id)) return;
                var item = new RelProductWarehouseEntity
                {
                    OrganizeId = organizeId,
                    productId = p.Id,
                    productName = p.name,
                    warehouseId = warehouseId,
                    warehouseName = warehouse.name,
                    unitId = p.minUnit,
                    zt = ((int)Enumzt.Enable).ToString()
                };
                item.Create(true);
                targetRel.Add(item);
            });
            return targetRel.Count > 0 ? _relProductWarehouseRepo.Insert(targetRel) : 0;
        }

        /// <summary>
        /// 删除库房物资
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        private int DeleteWarehouseProduct(List<string> productIds, string organizeId, string warehouseId)
        {
            var warehouse = kfWarehouseRepo.FindEntity(p => p.Id == warehouseId && p.OrganizeId == organizeId && p.zt == ((int)Enumzt.Enable).ToString());
            if (warehouse == null) return 0;
            var allRelPro = _relProductWarehouseRepo.IQueryable(q => q.warehouseId == warehouseId && q.OrganizeId == OrganizeId).ToList();
            if (allRelPro.Count <= 0) return 0;
            var targetRel = new List<RelProductWarehouseEntity>();
            var existPro = _relProductWarehouseRepo.IQueryable(p => p.warehouseId == warehouseId && p.OrganizeId == OrganizeId).Select(q => q.productId).ToList();
            allRelPro.ForEach(p =>
            {
                if (productIds.Contains(p.productId) && !targetRel.Contains(p) && existPro.Contains(p.productId))
                {
                    targetRel.Add(p);
                }
            });
            return targetRel.Count > 0 ? _relProductWarehouseRepo.BatchDelete(targetRel) : 0;
        }

        /// <summary>
        /// 组装目标物资
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        private List<WzProductEntity> AssembleTargetProduct(List<string> productIds, string organizeId)
        {
            var allPro = _wzProductRepo.IQueryable(p => p.OrganizeId == organizeId && p.zt == ((int)Enumzt.Enable).ToString()).ToList();
            var result = new List<WzProductEntity>();
            if (allPro.Count > 0 && productIds.Count > 0)
            {
                allPro.ForEach(p =>
                {
                    if (productIds.Contains(p.Id) && !result.Contains(p))
                    {
                        result.Add(p);
                    }
                });
            }
            return result;
        }

        #endregion
    }
}
