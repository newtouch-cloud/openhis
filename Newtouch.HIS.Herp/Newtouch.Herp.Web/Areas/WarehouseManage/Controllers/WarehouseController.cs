using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.WarehouseManage.Controllers
{
    /// <summary>
    /// 库房管理
    /// </summary>
    public class WarehouseController : ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IRelWarehouseDeptRepo _relWarehouseDeptRepo;
        private readonly FrameworkBase.MultiOrg.Domain.IRepository.ISysStaffRepo _sysStaffRepo;
        private readonly IRelWarehouseUserRepo _relWarehouseUserRepo;
        private readonly IWarehouseApp _warehouseApp;
        private readonly IWarehouseDmnService _warehouseDmnService;
        private readonly IKfWarehouseRepo _kfWarehouseRepo;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IRelProductWarehouseRepo _relProductWarehouseRepe;
        private readonly IWzProductDmnService _wzProductDmnService;

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                _warehouseDmnService.DeleteWarehouse(keyValue);
            }
            return Success("删除成功。");
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWarehouseGridJson(string keyWord)
        {
            var data = _warehouseDmnService.QueryWarehouseInfo("", OrganizeId);
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                data = data.ToList().TreeWhere(t => t.name.Contains(keyWord), parentId: "ParentId");
            }
            var treeList = (from item in data
                            let hasChildren = data.Count(t => t.parentId == item.Id) != 0
                            select new TreeGridModel
                            {
                                id = item.Id,
                                isLeaf = hasChildren,
                                parentId = item.parentId,
                                expanded = hasChildren,
                                entityJson = item.ToJson()
                            }).ToList();
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyWord)
        {
            var warehouse = _kfWarehouseRepo.FindEntity(p => p.Id == keyWord && p.OrganizeId == OrganizeId);
            return Content(warehouse.ToJson());
        }

        /// <summary>
        /// 获取科室
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetDeptmentSelecotrTree(string keyWord)
        {
            var depts = _sysDepartmentRepo.GetList(OrganizeId, ((int)Enumzt.Enable).ToString());
            var treeList = new List<TreeViewModel>();
            if (depts == null || depts.Count <= 0) return Content(treeList.TreeViewJson(null));
            var deptWareRel = _relWarehouseDeptRepo.GetListByWarehouseId(keyWord, OrganizeId) ?? new List<RelWarehouseDeptEntity>();
            depts.ToList().ForEach(p =>
            {
                treeList.Add(new TreeViewModel
                {
                    id = p.Id,
                    text = p.Name,
                    value = p.Id,
                    parentId = p.ParentId,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    hasChildren = true,
                    checkstate = deptWareRel.Any(q => q.deptId == p.Id) ? 1 : 0
                });
            });
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetStaffSelecotrTree(string keyWord)
        {
            var staff = _sysStaffRepo.GetValidStaffListByOrganizeId(OrganizeId);
            var treeList = new List<TreeViewModel>();
            if (staff == null || staff.Count <= 0) return Content(treeList.TreeViewJson(null));
            var staffWareRel = _relWarehouseUserRepo.GetListByWarehouseId(keyWord, OrganizeId) ?? new List<RelWarehouseUserEntity>();
            staff.ToList().ForEach(p =>
            {
                treeList.Add(new TreeViewModel
                {
                    id = p.gh,
                    text = p.Name + "(" + p.gh + ")",
                    value = p.gh,
                    parentId = null,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    hasChildren = true,
                    checkstate = staffWareRel.Any(q => q.gh == p.gh) ? 1 : 0
                });
            });
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 获取上级
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPatientTreeSelectJson()
        {
            var warehouse = _kfWarehouseRepo.IQueryable(p => p.OrganizeId == OrganizeId && p.zt == ((int)Enumzt.Enable).ToString());
            var treeList = new List<TreeSelectModel>();
            foreach (var item in warehouse)
            {
                var treeModel = new TreeSelectModel
                {
                    id = item.Id,
                    text = item.name,
                    parentId = item.parentId
                };
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// 库房维护 表单提交
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="staffghs"></param>
        /// <param name="departmentIds"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(KfWarehouseEntity wzEntity, string staffghs, string departmentIds, string keyWord)
        {
            wzEntity.zt = wzEntity.zt == "true" ? "1" : "0";
            wzEntity.isDefSyn = wzEntity.isDefSyn == "true" ? "1" : "0";
            wzEntity.OrganizeId = OrganizeId;
            _warehouseApp.SubmitForm(wzEntity, staffghs.Split(','), departmentIds.Split(','), keyWord);
            return Success("操作成功", null);
        }

        #region 同步物资

        /// <summary>
        /// 同步物资
        /// </summary>
        /// <returns></returns>
        public ActionResult SynceProduct()
        {
            return View();
        }

        /// <summary>
        /// 获取同步库房物资信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="wId">库房ID</param>
        /// <param name="keyWord"></param>
        /// <param name="wzlb"></param>
        /// <returns></returns>
        public ActionResult GettbwzGridJson(Pagination pagination, string wId, string keyWord, string wzlb)
        {
            var data = new
            {
                rows = _warehouseDmnService.GetSyncProductList(pagination, wId, wzlb, keyWord, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 控制物资
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="wId">库房ID</param>
        /// <param name="op">操作 1：取消控制  0：控制 </param>
        /// <returns></returns>
        public ActionResult ControlWz(string relId, string wId, string op = "")
        {
            var rel = _relProductWarehouseRepe.FindEntity(p => p.Id == relId && p.OrganizeId == OrganizeId && p.warehouseId == wId);
            rel.zt = op == ((int)Enumzt.Enable).ToString() ? ((int)Enumzt.Enable).ToString() : ((int)Enumzt.Disable).ToString();
            rel.Modify();
            return _relProductWarehouseRepe.Update(rel) > 0 ? Success("控制成功") : Error("控制失败");
        }

        /// <summary>
        /// 更新库房物资关联关系
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="opereateType">0:添加  1：删除</param>
        /// <param name="keyWord">目标库房</param>
        /// <returns></returns>
        public ActionResult FreshWhAndwzRelList(string[] productIds, int opereateType, string keyWord)
        {
            var list = productIds.ToList();
            var result = _warehouseApp.FreshWhAndwzRelList(list, opereateType, OrganizeId, keyWord);
            //return result ? Success("操作成功") : Error("操作失败");
            return Success("操作成功");
        }

        #endregion

        #region 维护本部门物资单位

        public ActionResult ProductUnit(string relId)
        {
            return View();
        }

        /// <summary>
        /// 获取库房指定物资单信息
        /// </summary>
        /// <param name="relId"></param>
        /// <returns></returns>
        public ActionResult GetKfWzUnit(string relId)
        {
            return Content(_relProductWarehouseRepe.FindEntity(p => p.Id == relId && p.OrganizeId == OrganizeId).ToJson());
        }

        /// <summary>
        /// 获取该物资所有单位
        /// </summary>
        /// <param name="relId"></param>
        /// <returns></returns>
        public ActionResult GetUnitByProId(string relId)
        {
            return Content(_wzProductDmnService.GetProductAndUnitByProId(relId, OrganizeId).ToJson());
        }

        /// <summary>
        /// 修改库房物资单位
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ActionResult UpdateKfWzUnit(string relId, string unitId)
        {
            var entity = _relProductWarehouseRepe.FindEntity(p => p.Id == relId && p.OrganizeId == OrganizeId);
            if (entity == null) return Error("未找到指定库房物资信息");
            entity.unitId = unitId;
            entity.Modify();
            return _relProductWarehouseRepe.Update(entity) > 0 ? Success("操作成功") : Error("操作失败");
        }
        #endregion

        /// <summary>
        /// 获取部门物资类别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBmWzLb()
        {
            var result = _wzProductDmnService.GetBmWzLb(Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }


        #region 本部门物资查询

        /// <summary>
        /// 部门物资视图
        /// </summary>
        /// <returns></returns>
        public ActionResult WarehouseProductQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 获取库房物资
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWarehouseProducts(Pagination pagination, string keyWord, string lb, string kzbz, string wzzt)
        {
            var data = new
            {
                rows = _warehouseDmnService.GetWarehouseProducts(pagination, keyWord, lb, kzbz, wzzt, OrganizeId, Constants.CurrentKf.currentKfId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        #endregion
    }
}