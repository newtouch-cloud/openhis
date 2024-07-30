using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Repository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{

    public class DietaryAdviceController : OrgControllerBase
    {
        private readonly IInpatientDietBaseRepo _inpatientDietBaseRepo;
        private readonly IInpatientDietSfxmdyRepo _inpatientDietSfxmdyRepo;
        private readonly IDietaryAdviceDmnService _iDietaryAdviceDmnService;
        public ActionResult DietBase() {
            return View();
        }

        public ActionResult GetGridList(Pagination pagination, string lb, string keyword) {
            var data = new
            {
                rows = _inpatientDietBaseRepo.GetGridList(pagination, lb, keyword, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public ActionResult GetFormJson(string keyvalue) {
            var data = _inpatientDietBaseRepo.FindEntity(p => p.Id == keyvalue && p.zt == "1");
            return Content(data.ToJson());
        }

        public ActionResult submitForm(DAFormVO vo, string keyvalue)
        {
            InpatientDietBaseEntity entity = new InpatientDietBaseEntity();
            entity = vo.MapperTo(entity);
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            entity.bdsfxm = vo.bdsfxm.ToBool();
            if (!string.IsNullOrWhiteSpace(vo.ParentId))
            {
                entity.ParentId = vo.ParentId.Substring(0, vo.ParentId.Length - 1);
            }
            _inpatientDietBaseRepo.SubmitForm(entity, keyvalue);
            return Success();
        }

        public ActionResult GetSearchType() {
            var treeList = new List<TreeViewModel>();
            var dflList = _inpatientDietBaseRepo.IQueryable().Where(p => p.DietType == "00" && p.zt == "1");
            foreach (var item in dflList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Id;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        public ActionResult GetSSXH(string from=null) {
            var data = _inpatientDietBaseRepo.IQueryable().Where(p => p.DietType == ((int)EnumSSLB.ssxh).ToString() && p.zt == "1");
            if (from=="kyz")
            {
                data= _inpatientDietBaseRepo.IQueryable().Where(p => (p.DietType == ((int)EnumSSLB.ssxh).ToString()||(p.DietType== ((int)EnumSSLB.lx).ToString()&&p.ParentId==null)) && p.zt == "1");
            }
            return Content(data.ToJson());
        }

        public ActionResult GetGridMXList(string Id)
        {
            var data = _iDietaryAdviceDmnService.GetmxList(Id,OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult DeleteForm(string keyvalue ) {
            _inpatientDietBaseRepo.DeleteForm(keyvalue);
            return Success();
        }

        public ActionResult SaveData(List<DAMXFormVO>  gridMxData, List<string> deldata) {
            _iDietaryAdviceDmnService.SubmitService(OrganizeId, gridMxData, deldata);
            return Success();
        }

        public ActionResult GetYSLB(string Id) {
            var data = _inpatientDietBaseRepo.IQueryable().Where(p => p.ParentId.Contains(Id) && p.zt == "1" && p.OrganizeId == OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult GetyszsList(string keyword)
        {
            var data=  _inpatientDietBaseRepo.IQueryable().Where(p=>p.zt=="1"&&p.OrganizeId==this.OrganizeId&&p.DietType== ((int)EnumSSLB.tsyq).ToString());
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(p => p.DietBigType == keyword);
            }
            return Content(data.ToJson());
        }
    }
}