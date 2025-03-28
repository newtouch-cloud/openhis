using FrameworkBase.MultiOrg.Web;
using Newtonsoft.Json;
using Newtouch.Common;
using Newtouch.Core.Redis;
using Newtouch.MRQC.Domain.IDomainServices.QcBlzkManage;
using Newtouch.MRQC.Domain.IRepository.QcItemManage;
using Newtouch.MRQC.Domain.ValueObjects;
using Newtouch.MRQC.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Areas.QualityControlManage.Controllers
{
    public class SysItemController : ControllerBase
    {
        private readonly IQcItemDataRepo _qcitemdataRepo;
        private readonly IQcBlzkDmnService _qcblzkdmnservice;

        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Form()
        {
            return base.Form();
        }
        /// <summary>
        /// 质控项目:病历模板类型树
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetQcBllxMbTree(string keyword)
        {
            //string token = AuthenManageHelper.GetToken();
            var reqObj = new
            {
                Data = "",
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<BllxmbRecordsVo>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalbllxTreeRecord", this.UserIdentity);
            //var outstr = AuthenManageHelper.HttpPost(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalbllxTreeRecord", token);
            //AuthenTokenHelper apiResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(outstr);
            //var data = JsonConvert.DeserializeObject<BllxmbRecordsVo>(apiResp.BusData.data.ToString()); 
            var treeList = new List<TreeGridModel>();
            foreach (var item in outstr.data.bllxmbRecord)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = outstr.data.bllxmbRecord.Count(t => t.parentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.parentId == null ? null : item.parentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }
        /// <summary>
        /// 质控项目维护
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetZkxmList(string keyword)
        {
            var data = _qcitemdataRepo.IQueryable().Where(p => p.BlmbId== keyword && p.OrganizeId==this.OrganizeId && p.zt == "1").OrderBy(p => p.Px).ToList();

            return Content(data.ToJson());
        }
        public ActionResult GetItemMXData(int Id)
        {
            var data = _qcitemdataRepo.IQueryable(p => p.Id == Id && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList();

            return Content(data.ToJson());
        }

        public ActionResult SubmitForm(ZkxmEntityVo ZkxmMXListVO)
        {
            _qcblzkdmnservice.SubmitForm(ZkxmMXListVO);
            return Success();
        }
        public ActionResult ZkDetailDel(int Id)
        {
            _qcitemdataRepo.DeleteForm(Id, this.OrganizeId);
            return Success();
        }
    }
}