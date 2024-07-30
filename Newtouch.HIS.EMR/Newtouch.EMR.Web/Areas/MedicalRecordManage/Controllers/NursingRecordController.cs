using Newtouch.EMR.Domain.Entity;

using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;
using System.Collections.Generic;
using Newtouch.Common;
using Newtouch.EMR.Domain.IRepository;
using System.Linq;
using System;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Domain.ValueObjects;
namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{
    /// <summary>
    /// 创 建：mhz
    /// 日 期：2023-04-24 15:46
    /// 描 述：护理记录录入
    /// </summary>
    public class NursingRecordController : OrgControllerBase
    {
        private readonly IMedicalRecordDmnService _medicalRecordDmnService;
        private readonly INursingRecordDmnService _nursingRecordDmnService;
        private readonly INursingRecordWZDmnService _nursingRecordWZDmnService;
        private readonly INursingRecordSSDmnService _nursingRecordSSDmnService;
        /// <summary>
        /// 护理查询
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }
        public override ActionResult Form()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public  ActionResult WZForm()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public ActionResult SSForm()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public ActionResult OpenOutForm()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public ActionResult OpenPutForm()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public ActionResult GetPatWard(string zyh)
        {
            var wardTree = _nursingRecordDmnService.GetPatList(OrganizeId, zyh);
            return Content(wardTree.ToJson());
        }
        public ActionResult SubmitSrl(string srlstr,string zyh,string rq,string sj,string bllx)
        {
            var orgId = OrganizeId;
            _nursingRecordDmnService.DeleteSrl(orgId, zyh, rq, sj, bllx, UserIdentity.UserCode);
            for (int i = 0; i < srlstr.Split(',').Length; i++)
            {
                _nursingRecordDmnService.SubmitSrl(srlstr.Split(',')[i].Split('|')[0], srlstr.Split(',')[i].Split('|')[1], srlstr.Split(',')[i].Split('|')[2], srlstr.Split(',')[i].Split('|')[3], orgId, zyh, rq, sj, bllx, UserIdentity.UserCode);
            }
            return Success("操作成功。");
        }
        public ActionResult SubmitScl(string srlstr, string zyh, string rq, string sj, string bllx)
        {
            var orgId = OrganizeId;
            _nursingRecordDmnService.DeleteScl(orgId, zyh, rq, sj, bllx, UserIdentity.UserCode);
            for (int i = 0; i < srlstr.Split(',').Length; i++)
            {
                _nursingRecordDmnService.SubmitScl(srlstr.Split(',')[i].Split('|')[0], srlstr.Split(',')[i].Split('|')[1], srlstr.Split(',')[i].Split('|')[2], srlstr.Split(',')[i].Split('|')[3], orgId, zyh, rq, sj, bllx, UserIdentity.UserCode);
            }
            return Success("操作成功。");
        }
        /// <summary>
        /// 获取病区人员执行树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPatWardTree(string zyzt, string keyword)
        {

            var wardTree = _nursingRecordDmnService.GetPatTree(OrganizeId, zyzt, keyword);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();

                foreach (InpWardPatTreeVO itempat in patInfo)
                {
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    treepat.text = itempat.zyh + "-" + itempat.hzxm;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treepat.Ex2 = itempat.rqrq;
                    treepat.Ex3 = itempat.cqrq;
                    treepat.Ex4 = itempat.hzxm;
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
            return Content(treeList.TreeViewJson(null));
        }
        /// <summary>
        /// 提交保存护理录入
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(bl_hljl_ybEntity entity, string keyValue)
        {
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            _nursingRecordDmnService.SubmitForm(entity, keyValue);
            return Success("保存成功。");
        }
        /// <summary>
        /// 提交保存护理录入
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitFormWZ(bl_hljl_wzEntity entity, string keyValue)
        {
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            _nursingRecordWZDmnService.SubmitForm(entity, keyValue);
            return Success("保存成功。");
        }
        /// <summary>
        /// 提交保存护理录入
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitFormSS(bl_hljl_ssEntity entity, string keyValue)
        {
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            _nursingRecordSSDmnService.SubmitForm(entity, keyValue);
            return Success("保存成功。");
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string zyh, string wardCode, string isShowDelete,string hllx)
        {
            if (hllx == "1")
            {
                var content = _nursingRecordWZDmnService.GetPaginationListWZ(pagination, this.OrganizeId, kssj, jssj, zyh, wardCode, isShowDelete);
                return Content(content.ToJson());
            }
            else if (hllx == "2")
            {
                var content = _nursingRecordSSDmnService.GetPaginationListSS(pagination, this.OrganizeId, kssj, jssj, zyh, wardCode, isShowDelete);
                return Content(content.ToJson());

            }
            else {
                var rows = _nursingRecordDmnService.GetPaginationList(pagination, this.OrganizeId, kssj, jssj, zyh, wardCode, isShowDelete);
                return Content(rows.ToJson());
            }
        }
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _nursingRecordDmnService.FindEntity(keyValue);
            return Content(data.ToJson());
        }
        public ActionResult GetFormJsonWZ(string keyValue)
        {
            var data = _nursingRecordWZDmnService.FindEntity(keyValue);
            return Content(data.ToJson());
        }
        public ActionResult GetFormJsonSS(string keyValue)
        {
            var data = _nursingRecordSSDmnService.FindEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _nursingRecordDmnService.DeleteForm(keyValue);
            return Success("作废成功。");
        }
        public ActionResult GetSrlScl(string zyh, string rq, string sj, string bllx)
        {
            var wardTree = _nursingRecordDmnService.GetSrlScl(OrganizeId, zyh, rq, sj, bllx);
            return Content(wardTree.ToJson());
        }
        public ActionResult GetScl(string zyh, string rq, string sj, string bllx)
        {
            var wardTree = _nursingRecordDmnService.GetScl(OrganizeId, zyh, rq, sj, bllx);
            return Content(wardTree.ToJson());
        }
        public ActionResult Getzyhrq(string zyh)
        {
            var patInfo = _nursingRecordDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            string ryrq = "";
            if (patInfo != null && patInfo.ryrq != null)
            {
                ryrq = patInfo.ryrq.ToString();
            }
            return Content(ryrq);
        }
        public ActionResult Getzyhcqrq(string zyh)
        {
            var patInfo = _nursingRecordDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            string cqrq = "";
            if (patInfo != null && patInfo.cqrq != null && patInfo.cqrq.ToString() != "")
            {
                cqrq = patInfo.cqrq.ToString();
            }
            return Content(cqrq);
        }
    }
}