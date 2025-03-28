using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.NonTreatmentItemManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.NonTreatmentItemManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.NonTreatmentItemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NonTreatmentItemController : ControllerBase
    {
        private readonly INonTreatmentItemDmnService _nonTreatmentItemDmnService;
        private readonly INonTreatmentItemBillingRepo _nonTreatmentItemBillingRepo;
        private readonly ISysConfigRepo _sysConfigRepo;


        // GET: NonTreatmentItemManage/NonTreatmentItemDmnServices
        public ActionResult Bookkeeping()
        {
            //是否显示住院号
            var IsShowZyh = _sysConfigRepo.GetValueByCode("NonTreatItem_ShowZyh", this.OrganizeId);
            ViewBag.IsShowZyh = IsShowZyh;
            return View();
        }
        public ActionResult BookkeepingQuery()
        {           
            //是否显示住院号
            var IsShowZyh = _sysConfigRepo.GetValueByCode("NonTreatItem_ShowZyh", this.OrganizeId);
            ViewBag.IsShowZyh = IsShowZyh;
            return View();
        }
        public ActionResult BookkeepingRefund()
        {
            //是否显示住院号
            var IsShowZyh = _sysConfigRepo.GetValueByCode("NonTreatItem_ShowZyh", this.OrganizeId);
            ViewBag.IsShowZyh = IsShowZyh;
            return View();
        }

        public ActionResult RefundDetailConfirmForm()
        {
            return View();
        }

        #region 项目记账
        /// <summary>
        /// 根据病历号获取patId和姓名
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ActionResult SelectPatientInfoByblhOrzyh(string blh, string zyh, string xm)
        {
            var data = _nonTreatmentItemDmnService.SelectPatientInfoByblhOrzyh(this.OrganizeId, blh, zyh, xm);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存记账
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveBilling(string data)
        {
            var jzList = Tools.Json.ToList<NonTreatmentItemBillingEntity>(data);
            List<NonTreatmentItemBillingEntity> billingList = new List<NonTreatmentItemBillingEntity>();
            foreach (var item in jzList)
            {
                NonTreatmentItemBillingEntity entity = new NonTreatmentItemBillingEntity();
                entity.jfbId = Guid.NewGuid().ToString();
                entity.OrganizeId = this.OrganizeId;
                entity.zyh = item.zyh;
                entity.sfxmCode = item.sfxmCode;
                entity.dlCode = item.dlCode;
                entity.sl = item.sl;
                entity.dj = item.dj;
                entity.je = item.je;
                entity.blh = item.blh;
                entity.patId = item.patId;
                entity.xm = item.xm;
                entity.sfxmCode = item.sfxmCode;
                entity.smry = item.smry;
                entity.smksCode = item.smksCode;
                entity.tjr = item.tjr;
                entity.cxry = null;
                entity.cxrq = null;
                entity.cxjfbId = null;
                entity.jzrq = item.jzrq;
                entity.zt = "1";
                entity.px = null;
                entity.Create();
                billingList.Add(entity);
            }
            _nonTreatmentItemBillingRepo.SaveBilling(billingList);
            return Success();
        }
        #endregion

        #region 记账查询
        //收费分类数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectChargeCategoryJson()
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var data = _nonTreatmentItemDmnService.GetChargeCategoryTreeList(orgId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                    ? data.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault()
                    : null;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectNonTreatmentItemList(Pagination pagination, string sfdl, string keyword, string smry, string smks, DateTime? kssj, DateTime? jssj,string kehu, string zyh, string blh)
        {
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                pagination.sidx = pagination.sidx + ", CreateTime desc";
            }
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var list = new
            {
                rows = _nonTreatmentItemDmnService.SelectNonTreatmentItemList(pagination, sfdl, keyword, smry, smks, kssj, jssj, orgId, kehu, zyh, blh),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region 记账退费
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="smry"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult SelectRefundItemList(Pagination pagination, string zyh, string blh, string keyword, string smry, string kehu, DateTime? kssj, DateTime? jssj)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var list = new
            {
                rows = _nonTreatmentItemDmnService.SelectRefundItemList(pagination, zyh, blh, keyword, smry, kehu, kssj, jssj, orgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 保存退费
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveRefund(string data)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var jzList = Tools.Json.ToList<NonTreatmentItemBillingInfoVO>(data);
            List<NonTreatmentItemBillingEntity> billingList = new List<NonTreatmentItemBillingEntity>();
            foreach (var item in jzList)
            {
                NonTreatmentItemBillingEntity entity = new NonTreatmentItemBillingEntity();
                entity.jfbId = Guid.NewGuid().ToString();
                entity.OrganizeId = orgId;
                entity.zyh = item.zyh;
                entity.sfxmCode = item.sfxmCode;
                entity.dlCode = item.dlCode;
                entity.sl = 0-item.tsl.Value;    //退的数量
                entity.dj = item.dj.Value;    //退的数量
                entity.je = 0-item.tje.Value;    //退的金额
                entity.cxry = OperatorProvider.GetCurrent().UserCode;   //撤销人员
                entity.cxrq = DateTime.Now;    //撤销日期
                entity.cxjfbId = item.jfbId;    //撤销对应的jfbId
                entity.blh = item.blh;
                entity.patId = item.patId;
                entity.xm = item.xm;
                entity.sfxmCode = item.sfxmCode;
                entity.smry = item.smry;
                entity.smksCode = item.smksCode;
                entity.tjr = item.tjr;
                entity.jzrq = item.jzrq;
                entity.zt = "1";
                entity.px = null;
                entity.Create();
                billingList.Add(entity);
            }
            _nonTreatmentItemBillingRepo.SaveBilling(billingList);
            return Success();
        }

        #endregion
    }
}