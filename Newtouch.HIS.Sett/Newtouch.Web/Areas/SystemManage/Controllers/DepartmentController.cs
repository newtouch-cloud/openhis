using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Newtouch.Common.Exceptions;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DepartmentController : ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;

        //组织机构（医院） 科室 下拉 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string organizeId)
        {
            if (string.IsNullOrEmpty(organizeId))
            {
                organizeId = this.OrganizeId;
                if (string.IsNullOrEmpty(organizeId))
                {
                    return null;
                }
            }
            var data = _sysDepartmentRepo.GetList(organizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Code;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId == null ? null :
                    data.Where(p => p.Id == item.ParentId).Select(p => p.Code).FirstOrDefault();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }
    }
}