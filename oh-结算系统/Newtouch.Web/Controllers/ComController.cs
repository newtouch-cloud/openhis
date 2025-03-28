using System.Collections.Generic;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ComController : ControllerBase
    {
        private readonly IItemDmnService _itemDmnService;
        private readonly ICommonDmnService _commonDmnService;
        private readonly IGuiAnOutpatientXnhApp _guiAnOutpatientXnhApp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetSelectItemsDetailListByItemCode(string code)
        {
            var data = _itemDmnService.GetItemsDetailListByOrgIdAndItemCode(this.OrganizeId, code, "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Code;
                treeModel.text = item.Name;
                treeModel.parentId = null;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        public ActionResult GetSysNowDate(string format)
        {
            try
            {
                return Content(System.DateTime.Now.ToString(format));
            }
            catch
            {
                return Content(System.DateTime.Now.ToString());
            }
        }

        /// <summary>
        /// 查询所有药品分类（非收费大类）
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMedicineClassificationList()
        {
            var list = _commonDmnService.GetMedicineClassificationList();
            return Content(list.ToJson());
        }

        /// <summary>
        /// 查询所有项目分类
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChargetItemClassificationList()
        {
            var list = _commonDmnService.GetChargetItemClassificationList(this.OrganizeId);
            return Content(list.ToJson());
        }

        #region 贵安新农合医保接口调用


        /// <summary>
        /// 接口调用窗口视图页
        /// </summary>
        /// <returns></returns>
        public ActionResult XnhInterfaceRequest()
        {
            return View();
        }

        /// <summary>
        /// 接口请求
        /// </summary>
        /// <param name="itn">接口名称  例：S02</param>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XnhInterfaceSend(string itn, string body)
        {
            string response;
            var result = _guiAnOutpatientXnhApp.XnhInterfaceSend(itn, body, OrganizeId, out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

		#endregion

		#region 重庆医保接口调用
	    /// <summary>
	    /// 接口调用窗口视图页
	    /// </summary>
	    /// <returns></returns>
	    public ActionResult CqYbInterfaceRequest()
	    {
		    return View();
	    }
		#endregion
	}
}