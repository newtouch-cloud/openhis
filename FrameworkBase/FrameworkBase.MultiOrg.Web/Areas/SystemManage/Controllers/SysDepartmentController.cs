using Newtouch.Common;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 系统科室
    /// </summary>
    [AutoResolveIgnore]
    public class SysDepartmentController : OrgControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysDepartmentRepo"></param>
        public SysDepartmentController(ISysDepartmentRepo sysDepartmentRepo)
        {
            this._sysDepartmentRepo = sysDepartmentRepo;
        }

        /// <summary>
        /// 组织机构（医院）的 科室 下拉 数据源
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="mzzybz"></param>
        /// <param name="yjbz">1仅医技科室 0仅非医技科室 否则all</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeSelectJson(string organizeId
           , string mzzybz, bool? yjbz, string keyword)
        {
            if (string.IsNullOrEmpty(organizeId))
            {
                organizeId = this.OrganizeId;
                if (string.IsNullOrEmpty(organizeId))
                {
                    return null;
                }
            }
            var data = _sysDepartmentRepo.GetList(organizeId, "1");
            if (mzzybz == "1" || mzzybz == "2")
            {
                var byteMzzybz = byte.Parse(mzzybz);
                data = data.ToList().TreeWhere(p => p.mzzybz == 0 || p.mzzybz == byteMzzybz, parentId : "ParentId");
            }
            if (yjbz.HasValue)
            {
                if (yjbz.Value)
                {
                    //仅医技科室
                    data = data.ToList().TreeWhere(p => p.yjbz == true, parentId: "ParentId");
                }
                else
                {
                    //仅非医技科室
                    data = data.ToList().TreeWhere(p => p.yjbz == false, parentId: "ParentId");
                }
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.ToList().TreeWhere(p => p.Code.Contains(keyword) || p.Name.Contains(keyword)
                || p.py.Contains(keyword)
                , parentId: "ParentId");
            }

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

        /// <summary>
        /// 组织机构（医院）的 科室 数据源（非树）
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="mzzybz"></param>
        /// <param name="yjbz">1仅医技科室 0仅非医技科室 否则all</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSelectJson(string organizeId
           , string mzzybz, bool? yjbz, string keyword)
        {
            if (string.IsNullOrEmpty(organizeId))
            {
                organizeId = this.OrganizeId;
                if (string.IsNullOrEmpty(organizeId))
                {
                    return null;
                }
            }
            var data = _sysDepartmentRepo.GetList(organizeId, "1");
            if (mzzybz == "1" || mzzybz == "2")
            {
                var byteMzzybz = byte.Parse(mzzybz);
                data = data.Where(p => p.mzzybz == 0 || p.mzzybz == byteMzzybz).ToList();
            }
            if (yjbz.HasValue)
            {
                if (yjbz.Value)
                {
                    //仅医技科室
                    data = data.Where(p => p.yjbz == true).ToList();
                }
                else
                {
                    //仅非医技科室
                    data = data.Where(p => p.yjbz == false).ToList();
                }
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(p => p.Code.Contains(keyword) || p.Name.Contains(keyword)
                || p.py.Contains(keyword)).ToList();
            }

            return Content(data.ToJson());
        }

    }
}