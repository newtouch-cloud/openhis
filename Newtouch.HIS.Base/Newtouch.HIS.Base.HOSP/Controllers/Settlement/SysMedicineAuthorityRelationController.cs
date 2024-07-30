using Newtouch.HIS.Domain.IRepository.Settlement;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.Settlement;
using Newtouch.Common;

namespace Newtouch.HIS.Base.HOSP.Controllers.Settlement
{
    public class SysMedicineAuthorityRelationController : ControllerBase
    {
        private readonly ISysMedicineAuthorityRelationRepo _sysMedicineAuthorityRelationRepo;
        private readonly ISysMedicineAuthorityRelationDmnService _sysMedicineAuthorityRelationDmnService;
        private readonly ISysStaffDmnService _sysStaffDmnService;
        private readonly ISysMedicineAuthorityRepo _sysMedicineAuthorityRepo;

        public SysMedicineAuthorityRelationController(ISysMedicineAuthorityRelationRepo sysMedicineAuthorityRelationRepo, ISysMedicineAuthorityRelationDmnService sysMedicineAuthorityRelationDmnService, ISysOrganizeDmnService SysOrganizeDmnService, ISysStaffDmnService sysStaffDmnService, ISysMedicineAuthorityRepo sysMedicineAuthorityRepo)
        {
            this._sysMedicineAuthorityRelationRepo = sysMedicineAuthorityRelationRepo;
            //this._SysOrganizeDmnService = SysOrganizeDmnService;
            this._sysMedicineAuthorityRelationDmnService = sysMedicineAuthorityRelationDmnService;
            this._sysStaffDmnService = sysStaffDmnService;
            this._sysMedicineAuthorityRepo = sysMedicineAuthorityRepo;
        }

        // GET: SysMedicineAuthorityRelation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            return View();
        }

		

		/// <summary>
		/// 获取列表
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public ActionResult GetGridJson(Pagination pagination, string organizeId, string keyword)
        {
            pagination.sidx = "gh asc";
            pagination.sord = "asc";
            var StaffRepo = _sysMedicineAuthorityRelationDmnService.GetStaffPaginationList(pagination,organizeId, keyword);
            var data = new
            {
                rows = StaffRepo,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        //public ActionResult GetGridJson(Pagination pagination, string organizeId, string keyword)
        //{
        //    var StaffRepo = _sysStaffRepo.GetStaffList(organizeId, keyword);
        //    List<SysMedicineAuthorityRelationVO> List = new List<SysMedicineAuthorityRelationVO>();
        //    SysMedicineAuthorityRelationVO obj = new SysMedicineAuthorityRelationVO();
        //    foreach (var repo in StaffRepo)
        //    {

        //        obj = repo.MapperTo<SysStaffEntity, SysMedicineAuthorityRelationVO>();
        //        var staff = _sysStaffRepo.FindEntity(p => p.gh == obj.gh && p.OrganizeId == OrganizeId && p.zt == "1");
        //        obj.name = staff.Name;
        //        //obj.qxnames= _sysMedicineAuthorityRelationRepo.FindEntity(p => p.gh == obj.gh && p.OrganizeId == OrganizeId && p.zt == "1")
        //        //obj.qxnames=  //通过工号获取所有的权限名称  逗号分割

        //        //var qxkzRelList = _sysMedicineAuthorityRelationRepo.FindEntity(p => p.gh == obj.gh && p.OrganizeId == OrganizeId && p.zt == "1");
        //        //foreach (var qxkzRel in qxkzRelList) {
        //        //    var qxid = qxkzRel.qxId;
        //        //    //通过id获取权限名称

        //        //}
        //        List.Add(obj);
        //    }
        //    var data = new
        //    {
        //        rows = List,
        //        total = pagination.total,
        //        page = pagination.page,
        //        records = pagination.records
        //    };
        //    return Content(data.ToJson());
        //}

        public ActionResult GetGridQx(Pagination pagination, string gh ,string organizeId, string keyword)
        {
            pagination.sidx = "gh asc";
            pagination.sord = "asc";
            var list = _sysMedicineAuthorityRelationDmnService.GetGridQx(pagination, gh,organizeId, keyword);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysMedicineAuthorityRelationRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysMedicineAuthorityRelationEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicineAuthorityRelationEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            _sysMedicineAuthorityRelationRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        public ActionResult GetAuthorityList(string gh,string organizeId,string keyword)
        {
            var AuthorityList = _sysMedicineAuthorityRepo.GetValidList(organizeId,keyword);  //获取有效权限列表
            var treeList = new List<TreeViewModel>();
            var authorityCurrentEntityList = new List<SysMedicineAuthorityRelationVO>();
            if (!string.IsNullOrWhiteSpace(gh))
            {
                authorityCurrentEntityList = _sysMedicineAuthorityRelationDmnService.GetListBygh(gh,organizeId, keyword).ToList();
            }
            //TreeViewModel tree0 = new TreeViewModel();
            //tree0.id = "0";
            //tree0.text = "分类";
            //tree0.value = "qxkz";
            //tree0.parentId = null;
            //tree0.isexpand = true;
            //tree0.complete = true;
            //tree0.showcheck = true;
            ////tree1.checkstate = 1;
            //tree0.hasChildren = true;
            //treeList.Add(tree0);

            TreeViewModel tree1 = new TreeViewModel();
            tree1.id = "1";
            tree1.text = "特殊药品";
            tree1.value = "tsypbz";
            tree1.parentId = null;
            tree1.isexpand = true;
            tree1.complete = true;
            tree1.showcheck = true;
            //tree1.checkstate = 1;
            tree1.hasChildren = true;
            treeList.Add(tree1);

            TreeViewModel tree2 = new TreeViewModel();
            tree2.id = "2";
            tree2.text = "收费大类";
            tree2.value = "sfdlCode";
            tree2.parentId = null;
            tree2.isexpand = true;
            tree2.complete = true;
            tree2.showcheck = true;
            //tree2.checkstate = 1;
            tree2.hasChildren = true;
            treeList.Add(tree2);

            TreeViewModel tree3 = new TreeViewModel();
            tree3.id = "3";
            tree3.text = "抗生素";
            tree3.value = "kssCode";
            tree3.parentId = null;
            tree3.isexpand = true;
            tree3.complete = true;
            tree3.showcheck = true;
            //tree3.checkstate = 1;
            tree3.hasChildren = true;
            treeList.Add(tree3);

            foreach (var item in AuthorityList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.qxId;
                tree.text = item.qxmc;
                tree.value = item.qxCode;
                tree.parentId = item.rel_lxcode;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorityCurrentEntityList.Count(t => t.qxId == item.qxId);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateAuthority(string gh, string organizeId,string keyword,string AuthorityList)
        {
            _sysMedicineAuthorityRelationRepo.UpdateAuthority(gh,organizeId, (AuthorityList ?? "").Split(','));
            return Success("操作成功。");
        }

    }
}