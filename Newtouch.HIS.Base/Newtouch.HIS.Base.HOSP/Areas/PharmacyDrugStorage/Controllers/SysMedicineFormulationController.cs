using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.PharmacyDrugStorage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineFormulationController : ControllerBase
    {
        private readonly ISysMedicineFormulationRepo _sysMedicineFormulationRepo;
        private readonly ISysMedicineDosageRepo _sysMedicineDosageRepo;
        private readonly ISysMedicineUsageRepo _sysMedicineUsageRepo;
        public SysMedicineFormulationController(ISysMedicineFormulationRepo sysMedicineFormulationRepo, ISysMedicineDosageRepo sysMedicineDosageRepo, ISysMedicineUsageRepo sysMedicineUsageRepo)
        {
            this._sysMedicineFormulationRepo = sysMedicineFormulationRepo;
            this._sysMedicineDosageRepo = sysMedicineDosageRepo;
            this._sysMedicineUsageRepo = sysMedicineUsageRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysMedicineFormulationRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                recodes = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = _sysMedicineFormulationRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitForm(SysMedicineFormulationEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.jxCode))
            {
                throw new FailedException("请填写剂型编码");
            }
            if (string.IsNullOrWhiteSpace(entity.jxmc))
            {
                throw new FailedException("请填写剂型名称");
            }
            _sysMedicineFormulationRepo.submitForm(entity, keyValue);
            return Success("操作成功");
        }


        public ActionResult CorrelationUsage() {
            return View();
        }
        /// <summary>
        /// 用法树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDosageTree(string jxCode)
        {
            var treeList = new List<TreeViewModel>();
            var yfCodeArr = new List<string>();
            if (!string.IsNullOrWhiteSpace(jxCode))
            {
                var jxList = _sysMedicineDosageRepo.IQueryable().FirstOrDefault(p => p.jxCode == jxCode && p.zt == "1");
                if (jxList!=null)
                {
                    yfCodeArr = (jxList.yfCode ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
   
            }
         
            #region 所有用法
            var curList = _sysMedicineUsageRepo.IQueryable().Where(p=>p.zt=="1").ToList();
                foreach (SysMedicineUsageEntity item in curList)
                {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.yfId.ToString();
                tree.text = item.yfmc;
                tree.value = item.yfCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = yfCodeArr.Count(p => p == item.yfCode);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
                #endregion
            return Content(treeList.TreeViewJson(null));
        }

        public ActionResult submitUsage(string jxCode, string yfCode) {
            _sysMedicineDosageRepo.submitUsage(jxCode,yfCode);
            return Success("操作成功");
        }
    }
}