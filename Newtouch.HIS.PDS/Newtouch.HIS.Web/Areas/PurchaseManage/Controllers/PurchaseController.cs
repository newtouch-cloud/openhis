using FrameworkBase.MultiOrg.Web;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository.Purchase;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.PurchaseManage.Controllers
{
    public class PurchaseController : OrgControllerBase
    {
        // GET: PurchaseManage/Purchase

        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;
        private readonly IPurchaseLocationRepo _purchaseLocationRepo;

        #region 页面
        public ActionResult Index()
        {
            return View();
        }

        //配送点
        public ActionResult LocationIndex()
        {
            return View();
        }

        //配送点新增
        public ActionResult LocationForm()
        {
            return View();
        }
        #endregion


        #region 配送点

        //查询配送点
        public ActionResult QueryLocation()
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).ToList();


            return Content(result.ToJson());
        }

        //分页查询配送点
        public ActionResult GetLocationGridJson(Pagination pagination)
        {
            var tt = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1").OrderByDescending(p => p.CreateTime).ToList();

            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        //配送点详情 (浮层)
        public ActionResult QueryLocationbyId(string Id)
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.Id == Id && p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            return Content(result.ToJson());
        }

        public ActionResult SubmitLocation(PurchaseLocationEntity entity)
        {
            _purchaseLocationRepo.SubmitForm(entity, this.OrganizeId, entity.Id);
            return Success();
        }

        //配送点传报
        public ActionResult LocationUpload(string Id, int psdzt, string czlx)
        {
            //1.上传至阳光采购平台 YY001
            var result = _sunPurchaseDmnService.Purchase_YY001(this.OrganizeId, Id, czlx);
            //2.更新传报状态 (2 已传报)
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }

        //配送点作废
        public ActionResult LocationDelete(string Id, int psdzt, string czlx)
        {
            //1.阳光采购平台作废
            var result = _sunPurchaseDmnService.Purchase_YY001(this.OrganizeId, Id, czlx);
            //2.HIS作废配送点 ( 更新配送点状态 psdzt 3已作废 )
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }
        #endregion


    }
}