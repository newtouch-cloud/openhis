using FrameworkBase.MultiOrg.Web;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.Domain.IRepository.Purchase;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using static Newtouch.Herp.Domain.DTO.OutputDto.OutputYY156;

namespace Newtouch.Herp.Web.Areas.PurchaseManage.Controllers
{
    public class ReturnedMaterialsController :   OrgControllerBase
    {

        private readonly IReturnedRepo _returnedRepo;
        private readonly IReturnedDetailRepo _returnedDetailRepo;
        private readonly IPurchaseDmnService _purchaseDmnService;
        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;

        #region 退货单内容
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="cgEntity"></param>
        /// <param name="cgmxList"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitPurchase(ReturnedEntity thEntity, List<ReturnedDetailEntity> thmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            thEntity.OrganizeId = this.OrganizeId;
            thEntity.JLS = thmxList.Count;
            // 1.新增或更新采购列表
            var thId = _returnedRepo.SubmitForm(thEntity, keyValue);
            //2.删除采购明细
            if (keyValue != null)
            {
                _returnedDetailRepo.DeleteItem(thId, this.OrganizeId);
            }
            //3.添加采购明细
            foreach (var cgmx in thmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _returnedDetailRepo.InsertItem(cgmx, thId);
            }
            return Success();
        }
        public ActionResult SubmitAndUploadPurchase(ReturnedEntity thEntity, List<ReturnedDetailEntity> thmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            thEntity.OrganizeId = this.OrganizeId;
            thEntity.JLS = thmxList.Count;
            // 1.新增或更新采购列表
            var thId = _returnedRepo.SubmitForm(thEntity, keyValue);
            //2.删除采购明细
            if (keyValue != null)
            {
                _returnedDetailRepo.DeleteItem(thId, this.OrganizeId);
            }
            //3.添加采购明细
            foreach (var cgmx in thmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _returnedDetailRepo.InsertItem(cgmx, thId);
            }
            //4.采购订单退货与传报
            PurchaseUpload(thId, 2);
            return Success();
        }
        #endregion
        /// <summary>
        /// 分页查询退药单数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj)
        {
            var tt = _returnedRepo.GetReturnedGridJson(pagination, kssj, jssj, OrganizeId);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); 
        }


        public ActionResult QueryPurchaseReturnbyId(string thId)
        {
            var result = _purchaseDmnService.QueryPurchaseReturnbyId(thId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult QueryPurchaseReturnDetailbyId(string thId)
        {
            var result = _purchaseDmnService.QueryPurchaseReturnDetailbyId(thId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult PurchaseReturnDelete(string thId)
        {
            _returnedRepo.PurchaseReturnDelete(thId, this.OrganizeId);
            return Success();
        }
        public ActionResult PurchaseStateUpdate(string thId, int thdzt)
        {
            _returnedRepo.PurchaseReturnStateUpdate(thId, thdzt, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// 采购退货单单传报
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUpload(string thId, int ddzt)
        {
            //1.上传至阳光采购平台 YY113
            var ddbh=_sunPurchaseDmnService.Purchase_YY113(this.OrganizeId, thId);
            //2.获取YY113的订单编号  (YY113落地表)
            //var ddbh = "00001";
            //3.更新ddbh
            _returnedRepo.PurchaseDdbhUpdate(thId, ddbh, this.OrganizeId);
            //4.订单填报确认 YY114
            _sunPurchaseDmnService.Purchase_YY114(this.OrganizeId, thId);
            //更新传报状态
            _returnedRepo.PurchaseReturnStateUpdate(thId, ddzt, this.OrganizeId);
            return Success();
        }
    }
}