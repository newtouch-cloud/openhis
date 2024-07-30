using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DutyController : ControllerBase
    {
        private readonly ISysDutyRepo _sysDutyRepo;
        private readonly ISysStaffDutyRepo _sysStaffDutyRepo;

        public DutyController(ISysDutyRepo sysDutyRepo, ISysStaffDutyRepo sysStaffDutyRepo)
        {
            this._sysDutyRepo = sysDutyRepo;
            this._sysStaffDutyRepo = sysStaffDutyRepo;
        }

        public ActionResult GetDutyList(string staffId)
        {
            var dutyList = _sysDutyRepo.GetValidList();  //获取有效岗位列表
            var treeList = new List<TreeViewModel>();
            var staffCurrentDutyEntityList = new List<SysStaffDutyEntity>();
            if (!string.IsNullOrWhiteSpace(staffId))
            {
                staffCurrentDutyEntityList = _sysStaffDutyRepo.GetListByStaffId(staffId).ToList();
            }
            foreach (var item in dutyList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = staffCurrentDutyEntityList.Count(t => t.DutyId == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysDutyRepo.GetList(keyword);
            return Content(data.ToJson());
        }

    }
}