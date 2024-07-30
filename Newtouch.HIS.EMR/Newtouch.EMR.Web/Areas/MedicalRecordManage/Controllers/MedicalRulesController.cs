using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage.Controllers
{
    public class MedicalRulesController : OrgControllerBase
    {
        private readonly IMrWritingRulesRepo _mrwritingrulesRepo;
        private readonly Ibl_bllxRepo _bllxRepo;
        private readonly IMedicalRulesDmnService _medicalRulesDmnService;
        // GET: MedicalRecordManage/MedicalRules
        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 病历类型树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBllxTree()
        {
            var treedata = _bllxRepo.IQueryable(p=>p.zt=="1" && p.OrganizeId==this.OrganizeId).OrderBy(p=>p.bllx).ToList();

            var treeList = new List<TreeGridModel>();
            foreach (var item in treedata)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = treedata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId == null ? null : item.ParentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetRulesList(string keyword)
        {
            var data = _mrwritingrulesRepo.IQueryable().Where(p => p.Bllx == keyword && p.OrganizeId == this.OrganizeId && p.zt == "1").OrderBy(p => p.Px).ToList();

            return Content(data.ToJson());
        }
        /// <summary>
        /// 明细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetItemMXData(string Id)
        {
            var data = _mrwritingrulesRepo.IQueryable(p => p.Id == Id && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList();

            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ZkxmMXListVO"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(RulesEntityVo rulesvo)
        {
            _medicalRulesDmnService.SubmitForm(rulesvo);
            return Success();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult RulesDel(string Id)
        {
            _mrwritingrulesRepo.DeleteForm(Id, this.OrganizeId);
            return Success();
        }
    }
}