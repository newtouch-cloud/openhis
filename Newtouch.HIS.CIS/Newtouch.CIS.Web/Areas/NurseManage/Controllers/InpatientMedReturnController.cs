using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Utils;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class InpatientMedReturnController : OrgControllerBase
    {
        // GET: NurseManage/InpatientMedReturn
        private readonly IInpatientMedReturnDmnService _InpatientMedReturnDmnService;

        private readonly IDrug_withdrawalzy_tyjlRepo _IDrug_withdrawalzy_tyjlRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        public ActionResult GetGridJson(string patInfo, string keyword, string kssj, string jssj)
        {
            IList<InpatientMedicineGrantDto> patList = null;
            if (patInfo != null && patInfo != "")
            {
                string ReturnZcyMed = _sysConfigRepo.GetValueByCode("EnableReturnZcyMed", this.OrganizeId);
                patList = _InpatientMedReturnDmnService.GetPatMedReturnList(patInfo, keyword, kssj, jssj, this.OrganizeId, ReturnZcyMed);
            }
            return Content(patList.ToJson());
        }
        /// <summary>
        /// 获取病区人员发药树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPatWardTree(string keyword)
        {
            string ReturnZcyMed = _sysConfigRepo.GetValueByCode("EnableReturnZcyMed", this.OrganizeId);
            string staffId = this.UserIdentity.StaffId;
            var wardTree = _InpatientMedReturnDmnService.GetPatTree(keyword, staffId, this.OrganizeId, ReturnZcyMed);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });

            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();
                var NewPatInfo = patInfo.OrderBy(p => p.BedNo);
                foreach (InpWardPatTreeVO itempat in NewPatInfo)
                { 
                    string gender = itempat.sex == "1" ? "男" : "女";
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    //床号 + 姓名(住院天数)+住院号 + 年龄 +性别
                    treepat.text = itempat.BedNo + "-"  + itempat.hzxm + "(" + itempat.inHosDays + "天)" + "-" + itempat.zyh+ "-" + itempat.nl + "岁-" + gender;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            }
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
        }
        
        public ActionResult MedReturnSubmit(string medIds)
        {
            OperatorModel user = this.UserIdentity;
            if (!String.IsNullOrWhiteSpace(medIds))
            {
                string tyxh = _InpatientMedReturnDmnService.MedReturnSubmit(user, medIds);
                //return Content(new { tyxh = tyxh }.ToJson());
                return Success("退药成功", tyxh);
            }
            else
            {
                return Error("请选择药品");
            }

        }




        //--------------------------------------以下是：医嘱退药查询--------------------------------------

        public ActionResult Drug_withdrawal() {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }

        public ActionResult Griddata(string patInfo, string keyword, string kssj, string jssj) {
            var kongzhi = "";
            if (patInfo != null && patInfo != "")
            {
                var data = _IDrug_withdrawalzy_tyjlRepo.Griddata(patInfo, keyword, kssj, jssj, this.OrganizeId);
                

                return Content(data.ToJson());
            }
            else {
                return Content(kongzhi);
            }
        }

        
        public ActionResult Treecx(string keyword) {
            

            string staffId = this.UserIdentity.StaffId;
            var wardTree = _IDrug_withdrawalzy_tyjlRepo.treecx(keyword, this.UserIdentity.StaffId, this.OrganizeId);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });
             
            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();
                var NewPatInfo = patInfo.OrderBy(p => p.BedNo);
                foreach (GrugTreezsVO itempat in NewPatInfo)
                {
                    string gender = itempat.sex == "1" ? "男" : "女";
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    treepat.text = itempat.BedNo + "-" + itempat.hzxm + "(" + itempat.inHosDays + "天)" + "-" +itempat.zyh + "-" + itempat.nl + "岁-" + gender;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            }
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
            
            
        }

        
    }
}