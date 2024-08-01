using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Common;
using System.Linq;
using System.Collections.Generic;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using Newtouch.Core.Common;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:08
    /// 描 述：系统岗位
    /// </summary>
    [AutoResolveIgnore]
    public class SysDutyController : BaseController
    {
        private readonly ISysDutyRepo _sysDutyRepo;
        private readonly ISysStaffDutyRepo _sysStaffDutyRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysDutyRepo"></param>
        /// <param name="sysStaffDutyRepo"></param>
        public SysDutyController(ISysDutyRepo sysDutyRepo
            , ISysStaffDutyRepo sysStaffDutyRepo)
        {
            this._sysDutyRepo = sysDutyRepo;
            this._sysStaffDutyRepo = sysStaffDutyRepo;
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var list = _sysDutyRepo.GetList(keyword);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysDutyRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysDutyEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysDutyRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 岗位（树）   （人员关联岗位）
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
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

    }
}