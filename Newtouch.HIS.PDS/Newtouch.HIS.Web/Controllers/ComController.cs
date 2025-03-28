using Newtouch.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 公共请求
    /// </summary>
    public class ComController : ControllerBase
    {
        private readonly IItemDmnService _itemDmnService;

        /// <summary>
        /// 获取下拉框内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetSelectItemsDetailListByItemCode(string code)
        {
            var data = _itemDmnService.GetItemsDetailListByOrgIdAndItemCode(this.OrganizeId, code, "1");
            var treeList = data.Select(item => new TreeSelectModel
            {
                id = item.Code,
                text = item.Name,
                parentId = null
            }).ToList();
            return Content(treeList.TreeSelectJson(null));
        }
    }
}