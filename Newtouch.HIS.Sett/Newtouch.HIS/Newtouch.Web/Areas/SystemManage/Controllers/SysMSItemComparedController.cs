using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;
using Newtouch.Common.Operator;
using FrameworkBase.MultiOrg.Domain.IRepository;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMSItemComparedController : ControllerBase
    {
        private readonly ISysMSItemComparedApp _SysMSItemComparedApp;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;

        public ActionResult SysMSItemCompared()
        {
            return View();
        }

        public ActionResult SubmitForm(SysMedicalTechItemMappEntity entity, string keyValue)
        {
            _SysMSItemComparedApp.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(int keyValue)
        {

            _SysMSItemComparedApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 获取grid的值
        /// </summary>
        /// <returns></returns>
        public ActionResult GetdzGridJson(string keyword)
        {
            var data = _SysMSItemComparedApp.GetListBySearch(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 收费项目下拉框
        /// </summary>
        /// <returns></returns>
        [Obsolete("please use SysChargeItemController.GetChargeItemSelectData(string keyword)")]
        public ActionResult GetxmSelect()
        {
            return null;
        }

        /// <summary>
        /// 科室下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetksSelect()
        {
            var data = _sysDepartmentRepo.GetList(OperatorProvider.GetCurrent().OrganizeId, "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Code;
                treeModel.text = item.Name;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(Guid keyValue)
        {
            var entity = _SysMSItemComparedApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }

    }
}
