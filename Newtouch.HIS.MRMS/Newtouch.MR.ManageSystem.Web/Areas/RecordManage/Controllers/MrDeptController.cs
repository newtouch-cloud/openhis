using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Newtouch.MR.ManageSystem.Web.Areas.RecordManage.Controllers
{
    public class MrDeptController :  OrgControllerBase
    {

#pragma warning disable CS0649 // Field 'MrDeptController._MrdicdeptRepo' is never assigned to, and will always have its default value null
        private readonly IMrdicdeptRepo _MrdicdeptRepo;
#pragma warning restore CS0649 // Field 'MrDeptController._MrdicdeptRepo' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'MrDeptController._MrDeptDmnService' is never assigned to, and will always have its default value null
        private readonly IMrDeptDmnService _MrDeptDmnService;
#pragma warning restore CS0649 // Field 'MrDeptController._MrDeptDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'MrDeptController._MrreldeptRepo' is never assigned to, and will always have its default value null
        private readonly IMrreldeptRepo _MrreldeptRepo;
#pragma warning restore CS0649 // Field 'MrDeptController._MrreldeptRepo' is never assigned to, and will always have its default value null

        // GET: RecordManage/MrDept
#pragma warning disable CS0114 // 'MrDeptController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        public ActionResult Index()
#pragma warning restore CS0114 // 'MrDeptController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        {
            return View();
        }
#pragma warning disable CS0114 // 'MrDeptController.Form()' hides inherited member 'BaseController.Form()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        public ActionResult Form()
#pragma warning restore CS0114 // 'MrDeptController.Form()' hides inherited member 'BaseController.Form()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        {
            return View();
        }

        public ActionResult GetPagintionList(Pagination pagination, string keyword)
        {
            //object[] oplist = new[]{
            //    new{
            //        group="",
            //        name="放射室",
            //        Code="07"
            //    },
            //    new {
            //        group="病案科室一",
            //        name="妇产科",
            //        Code="00000005"
            //    },
            //    new{
            //        group="病案科室二",
            //        name="收费室",
            //        Code="00000009"
            //    },
            //    new{
            //        group="病案科室三",
            //        name="药房",
            //        Code="00000003"
            //    },

            //};
            var oplist = _MrDeptDmnService.GetPaginationList(pagination, OrganizeId);

            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        //获取病案科室下拉框内容
        public ActionResult GetDicDeptList(Pagination pagination, string keyword)
        {
            var oplist = _MrdicdeptRepo.GetDicDeptList(OrganizeId);
            var groupOptions = "";
            groupOptions += "<select>";
            groupOptions += "<option value=0>==请选择==</option>";
            foreach (var op in oplist)
            {
                groupOptions += "<option value="+op.ksbm + ">" + op.ksmc + "</option>";
            }
            groupOptions+= "</select>";
            return Content(groupOptions);

        }

        //新增 更新 病案科室his科室关系表
        public ActionResult SubmitForm(string hisdept, string hisdeptname, string baksId) {
            MrreldeptEntity entity= new MrreldeptEntity();
            entity.hisdept = hisdept;
            entity.hisdeptname = hisdeptname;
            entity.baksId = baksId;
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            entity.CreateTime = Convert.ToDateTime(DateTime.Now);
            entity.CreatorCode= UserIdentity.UserName;
            _MrreldeptRepo.Save(entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 分页获取病案列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetPagintionDicDeptList(Pagination pagination,string keyword) {
            var orgId = OrganizeId;
            var oplist = _MrdicdeptRepo.GetPagintionDicDeptList(pagination,orgId, keyword);
            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增/修改 病案信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitDicDept(MrdicdeptEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _MrdicdeptRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }  

        /// <summary>
        /// 查询单条病案信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _MrdicdeptRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除病案信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteData(string keyValue)
        {
            _MrdicdeptRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
    }
}