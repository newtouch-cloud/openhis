using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.RecordManage.Controllers
{
    public class MrFeeItemController : OrgControllerBase
	{
		private readonly IMrFeeRepo _MrFeeRepo;
		private readonly IMrFeeRelDmnService _MrFeeRelDmnService;
		private readonly IMrFeeRelRepo _MrFeeRelRepo;

		// GET: RecordManage/MrFeeItem
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult getFeeTree() {
			//var treeList = new List<TreeViewModel>();
			//var idList =new List<string>();
			//idList.Add("101");
			//idList.Add("102");
			//idList.Add("103");

			//TreeViewModel tree1 = new TreeViewModel();
			//tree1.id = "1";
			//tree1.text = "1text";
			//tree1.value = "1";
			//tree1.parentId = null;
			//tree1.isexpand = true;
			//tree1.complete = true;
			//tree1.showcheck = true;
			//tree1.checkstate = 0;
			//tree1.hasChildren = true;
			//tree1.Ex1 = "p";
			//treeList.Add(tree1);

			//for (var i = 0; i < idList.Count; i++) {
			//	TreeViewModel tree = new TreeViewModel();
			//	tree.id = idList[i];
			//	tree.text = idList[i]+"text";
			//	tree.value = idList[i];
			//	tree.parentId = "1";
			//	tree.isexpand = true;
			//	tree.complete = true;
			//	tree.showcheck = true;
			//	tree.checkstate = 0;
			//	tree.hasChildren = true;
			//	tree.Ex1 = "p";
			//	treeList.Add(tree);
			//}

			//TreeViewModel tree3 = new TreeViewModel();
			//tree3.id = "10101";
			//tree3.text = "10101text";
			//tree3.value = "10101";
			//tree3.parentId = "101";
			//tree3.isexpand = true;
			//tree3.complete = true;
			//tree3.showcheck = true;
			//tree3.checkstate = 0;
			//tree3.hasChildren = false;
			//tree3.Ex1 = "p";
			//treeList.Add(tree3);

			//return Content(treeList.TreeViewJson(null));

			var treeList = new List<Common.TreeViewModel>();
			var feeList= _MrFeeRepo.GetAllFeeList(OrganizeId);
			foreach(var list in feeList) {
				Common.TreeViewModel tree = new Common.TreeViewModel();
				tree.id = list.Code;
				tree.text = list.Name;
				tree.value = list.Id;
				tree.parentId =list.ParentCode;
				tree.isexpand = true;
				tree.complete = true;
				tree.showcheck = false;
				tree.checkstate = 0;
				tree.hasChildren = hasChild(list.Code,feeList);
				tree.Ex1 = "c";
				treeList.Add(tree);
			}
			return Content(treeList.TreeViewJson(null));
		}

		private bool hasChild(string code,IList<bafeeEntity> feeList) {
			var flag = false;
			foreach (var obj in feeList) {
				if (obj.ParentCode == code) {
					flag=true;
					break;
				}
			}
			return flag;
		}

		//树选择显示项目列表
		public ActionResult GetPagintionListById(Pagination pagination, string id)
		{
			var pat = _MrFeeRelDmnService.GetPagintionListById(pagination, OrganizeId, id);

			var data = new
			{
				rows = pat,
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		//显示未分类项目
		public ActionResult GetPagintionItem(Pagination pagination, string code, string keyword,string hissfdl) {

			var pat = _MrFeeRelDmnService.GetPagintionItem(pagination, OrganizeId, keyword,hissfdl,code);

			var data = new
			{
				rows = pat,
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		public int SubmitForm(bafeeRelEntity entity)
		{
			if (string.IsNullOrWhiteSpace(entity.OrganizeId))
			{
				entity.OrganizeId = OrganizeId;
			}
			entity.zt = "1";
			entity.CreateTime = Convert.ToDateTime(DateTime.Now);
			entity.CreatorCode = UserIdentity.UserName;
			_MrFeeRelRepo.SaveForm(entity);
			return 1;
		}

		public ActionResult Save(IList<itemEntity> list,string code,string hissfdl)
		{
			foreach (var obj in list)
			{
				bafeeRelEntity rel = new bafeeRelEntity();
				rel.sfxm = obj.sfxmCode;
				rel.sfxmmc = obj.sfxmmc;
				rel.feetypecode = code;
				SubmitForm(rel);
			}
			return Success("操作成功。");
		}

        public ActionResult SavebyHISsfdl(string code,string hissfdl)
        {
            _MrFeeRelDmnService.SaveRelbyHissfdl(code, hissfdl, this.UserIdentity.rygh,OrganizeId);
            return Success("操作成功。");
        }
        public ActionResult SavebySfxm(string code,string ids)
        {
            _MrFeeRelDmnService.SaveRelbyhissfxm(code, ids, this.UserIdentity.rygh,OrganizeId);
            return Success("操作成功。");
        }
		public ActionResult DeleteForm(string keyValue)
		{
			_MrFeeRelRepo.DeleteForm(keyValue);
			return Success("操作成功。");
		}

	}
}