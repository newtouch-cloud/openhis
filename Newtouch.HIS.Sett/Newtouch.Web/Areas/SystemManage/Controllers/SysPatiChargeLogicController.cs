using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeLogicController : ControllerBase
    {
        private readonly ISysPatiChargeLogicApp _SysPatiChargeLogicApp;
        private readonly ISysPatiNatureApp _SysPatiNatureApp;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;

        #region 病人收费算法

        /// <summary>
        /// 获取病人收费算法grid值
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ogrId"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetsfGridJson(Pagination pagination, string keyword, string ogrId)
        {
            var data = new
            {
                rows = _SysPatiChargeLogicApp.GetList(pagination, keyword, ogrId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public ActionResult SysPatiChargeLogic()
        {
            return View();
        }


        public ActionResult SubmitForm(PatiChargeLogicVO PatiChargeLogicVO, string keyValue)
        {
            _SysPatiChargeLogicApp.SubmitForm(PatiChargeLogicVO, keyValue,OrganizeId);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(int keyValue)
        {
            _SysPatiChargeLogicApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 修改信息时，把选中行对象带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _SysPatiChargeLogicApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }


        /// <summary>
        /// 病人性质下拉框 
        /// 病人收费算法病人性质下拉框已不再使用，
        /// 该方法在收费减免处也使用到，如果减免修改了病人性质展现方式，不再使用该方法，请记得清除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetbrxzSelect()
        {
            var treeList = _SysPatiNatureApp.GetbrxzSelect("");
            return Content(treeList);
        }

        /// <summary>
        /// 大类下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetdlSelect()
        {
            var data = _sysChargeCategoryRepo.GetList(this.OrganizeId, zt: "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 根据大类获取项目code
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        public ActionResult GetSFXMItemInfoByDlCode(string keyword, string dlCode)
        {
            var data = _SysPatiChargeLogicApp.GetSFXMItemInfoByDlCode(keyword, dlCode, this.OrganizeId);
            return Content(data.ToJson());
        }

        #endregion
    }
}
