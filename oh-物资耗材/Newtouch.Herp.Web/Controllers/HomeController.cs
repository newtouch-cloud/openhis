﻿using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : FrameworkBase.MultiOrg.Web.Controllers.HomeController
    {
        private readonly IHomeApp _homeApp;
        private readonly IStorageManageDmnService _storageManageDmnService;

        /// <summary>
        /// about
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// 库房切换
        /// </summary>
        /// <returns></returns>
        public ActionResult KfChange()
        {
            ViewBag.KfList = Constants.CurrentKf.kfList;
            return View();
        }

        /// <summary>
        /// 切换库房
        /// </summary>
        /// <param name="kfId">库房ID</param>
        /// <param name="kfName">库房名称</param>
        /// <param name="kfLeve">库房等级</param>
        /// <returns></returns>
        public ActionResult SwithKf(string kfId, string kfName, int kfLeve)
        {
            var opr = Constants.CurrentKf;
            opr.currentKfId = kfId;
            opr.currentKfName = kfName;
            opr.currentKfLevel = kfLeve;
            Constants.SetCurrentKf(opr.userId, opr);
            return Success("切换成功");
        }

        #region 首页待办

        /// <summary>
        /// 获取待处理html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNeedDealDiv()
        {
            var result = _homeApp.AssembleNeedDealHtml();
            return Success("", result);
        }

        /// <summary>
        /// 获取带处理总数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPendingCount()
        {
            return Content(_homeApp.GetPendingCount().ToJson());
        }

        #endregion

        #region 统计

        /// <summary>
        /// 进销存统计
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPsiByKf()
        {
            var result = new MonthlySummaryDTO[3];
            result[0] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetCkCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            result[1] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetRkCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            result[2] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetSyCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            return Content(result.ToJson());
        }

        /// <summary>
        /// 按单据类型获取入库总数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRkCountByLx()
        {
            var result = _storageManageDmnService.GetRkCountByLx(Constants.CurrentKf.currentKfId, OrganizeId) ?? new List<ClassificationStatisticsEntity>();
            _homeApp.TransformRkCount(result);
            return Content(result.ToJson());
        }
        #endregion
    }
}