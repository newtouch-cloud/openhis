using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage.Controllers
{
    public class OutpatientController : OrgControllerBase
    {
        private readonly IOutpatientDmnService _outpatientDmnService;
        // GET: MedicalRecordManage/Outpatient
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult PatList(Pagination pagination,string mjzbz,string keyword)
        {
            var chargeQueryList = new
            {
                rows = _outpatientDmnService.GetTreatingOrTreatedList(pagination, OrganizeId,Convert.ToInt32( mjzbz), this.UserIdentity.rygh, null, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(chargeQueryList.ToJson());
        }

        #region
        public ActionResult MedRecordTreeEditJson(string keyValue, string bllx, string mzh)
        {
            var pat = _outpatientDmnService.GetPatMzbymzh(OrganizeId,mzh,this.UserIdentity.rygh);
            MedRecordTreeEditVO data = new MedRecordTreeEditVO();
            if (pat != null)
            {
                data.bllxId = keyValue;
                data.mzh = pat.mzh;
                data.xm = pat.xm;
                data.birth = pat.csny;
                data.brxzmc = pat.brxzmc;
                data.sex = pat.xb;
                data.bllx = bllx;
                data.mbqx = (int)Enummbqx.pub;


                data.brjs = pat.xm + " / " + pat.xb;
                data.brjs += " / " + "年龄" + " / " + pat.brxzmc + " / " + pat.mzh;

            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取病历树
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GetMedRecordTree(string mzh)
        {
            var data = _outpatientDmnService.GetOutPatMedRecordTree(this.OrganizeId, mzh, this.UserIdentity.rygh);
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.parentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.parentId == null ? null : item.parentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();

                treeList.Add(treeModel);

            }
            return Content(treeList.TreeGridJson(null));
        }
        #endregion
    }
}