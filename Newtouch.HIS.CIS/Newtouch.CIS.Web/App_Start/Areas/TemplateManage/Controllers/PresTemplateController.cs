using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    public class PresTemplateController : OrgControllerBase
    {
        // GET: TemplateManage/PresTemplate

        private readonly IPresTemplateRepo _presTemplateRepo;
        private readonly IPresTemplateDetailRepo _presTemplateDetailRepo;
        private readonly IPresTemplateDmnService _presTemplateDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        /// <summary>
        /// 康复
        /// </summary>
        /// <returns></returns>
        public ActionResult RehabForm()
        {
            return View();
        }

        /// <summary>
        /// 常规项目处方
        /// </summary>
        /// <returns></returns>
        public ActionResult RegularItemForm()
        {
            return View();
        }

        /// <summary>
        /// 西药
        /// </summary>
        /// <returns></returns>
        public ActionResult WMForm()
        {
            return View();
        }

        /// <summary>
        /// 中药
        /// </summary>
        /// <returns></returns>
        public ActionResult TCMForm()
        {
            return View();
        }

        /// <summary>
        /// 处方模板树
        /// </summary>
        /// <param name="mblx"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        public ActionResult GetTreeList(int mblx, int cflx, int? expandCflx)
        {
            var treeList = new List<TreeViewModel>();

            if (cflx == 0)
            {
                if (_sysConfigRepo.GetBoolValueByCode("openKfcf", this.OrganizeId) == true)
                {
                    treeList.AddRange(GetStaticTreeList(mblx, (int)EnumCflx.RehabPres, expandCflx));
                }
                if (_sysConfigRepo.GetBoolValueByCode("openCgxmcf", this.OrganizeId) == true)
                {
                    treeList.AddRange(GetStaticTreeList(mblx, (int)EnumCflx.RegularItemPres, expandCflx));
                }
                treeList.AddRange(GetStaticTreeList(mblx, (int)EnumCflx.WMPres, expandCflx));
                treeList.AddRange(GetStaticTreeList(mblx, (int)EnumCflx.TCMPres, expandCflx));
            }
            else
            {
                treeList.AddRange(GetStaticTreeList(mblx, cflx, expandCflx));
            }


            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 处方模板 明细列表
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public ActionResult PresTemplateDetail()
        {
            //医保控制code
            ViewBag.ControlbrxzCode = _sysConfigRepo.GetValueByCode("ControlbrxzCode", OrganizeId);
            return View();
        }

        /// <summary>
        /// 查询模板明细 json
        /// </summary>
        /// <param name="mbId"></param>
        /// <param name="mxIdStr"></param>
        /// <returns></returns>
        public ActionResult SelectPresTemplateDetailByMbId(string mbId,  string mxIdStr)
        {
            var data = _presTemplateDmnService.SelectPresDetailByMbId(mbId, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveData(PresTemplateEntity mbObj, List<PresTemplateDetailEntity> mxList)
        {
            if (mbObj.mblx == (int)EnumCfMbLx.department)
            {
                mbObj.ksCode = this.UserIdentity.DepartmentCode;
            }
            else if (mbObj.mblx == (int)EnumCfMbLx.personal)
            {
                mbObj.ysgh = this.UserIdentity.rygh;
            }

            //模板表
            mbObj.OrganizeId = this.OrganizeId;

            var mbId = _presTemplateDmnService.SaveData(mbObj, mxList);

            return Success(null, mbId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mblx"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        private List<TreeViewModel> GetStaticTreeList(int mblx, int cflx, int? expandCflx = null)
        {
            var cflxmc = ((EnumCflx)cflx).GetDescription();

            var treeList = new List<TreeViewModel>();

            //处方模板
            treeList.Add(new TreeViewModel()
            {
                id = cflx.ToString(),
                value = cflx.ToString(),
                text = cflxmc,
                parentId = null,
                hasChildren = true,
                isexpand = (expandCflx ?? 0) != 0 ? (cflx == expandCflx ? true : false) : true,
                complete = true,
            });

            //处方模板明细
            var data = _presTemplateRepo.IQueryable().Where(a => a.OrganizeId == this.OrganizeId && a.mblx == mblx && a.cflx == cflx && a.zt == "1").Select(a => new { a.mbmc, a.cflx, a.mbId, a.CreateTime, a.LastModifyTime }).OrderByDescending(a => a.CreateTime).ThenByDescending(a => a.LastModifyTime).ToList();
            foreach (var item in data)
            {
                treeList.Add(new TreeViewModel()
                {
                    id = "",   //模板Id
                    value = item.cflx.ToString(),
                    text = item.mbmc,
                    parentId = item.cflx.ToString(),
                    hasChildren = false,
                    isexpand = false,
                    complete = true,
                    Ex1 = item.mbId
                });
            }
            return treeList;
        }
        public ActionResult XzyyForm()
        {
            return View();
        }

    }
}