using System.Collections.Generic;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;
using Newtouch.Herp.Infrastructure.Common;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// 报损报溢
    /// </summary>
    public class ProfitAndLossController : ControllerBase
    {
        private readonly IKcSyyyRepo _kcSyyyRepo;
        private readonly IKcSyxxDmnService _kcSyxxDmnService;
        private readonly ISysUserDmnService sysuserDmnService;
        private readonly IStockInventoryApp _stockInventoryApp;

        #region 损益原因维护

        /// <summary>
        /// 损益原因
        /// </summary>
        /// <returns></returns>
        public ActionResult Syyy()
        {
            return View();
        }

        /// <summary>
        /// 损益原因信息查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetSyyyGridJson(Pagination pagination, string keyWord = "")
        {
            var data = new
            {
                rows = _kcSyyyRepo.FindList(p => "" == keyWord || p.syyy.Contains(keyWord.Trim()), pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 损益原因 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SyyyForm()
        {
            return View();
        }

        /// <summary>
        /// 删除损益原因
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteSyyyForm(string keyValue)
        {
            return _kcSyyyRepo.DeleteSyyyById(keyValue) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// get Syyy information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetSyyyFormJson(string keyWord)
        {
            var syyy = _kcSyyyRepo.FindEntity(p => p.Id == keyWord);
            return Content(syyy.ToJson());
        }

        /// <summary>
        /// Submit SyyyForm
        /// </summary>
        /// <param name="kcSyyyEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult SubmitSyyyForm(KcSyyyEntity kcSyyyEntity, string keyWord)
        {
            kcSyyyEntity.zt = kcSyyyEntity.zt == "true" ? "1" : "0";
            return _kcSyyyRepo.SubmitForm(kcSyyyEntity, keyWord) > 0 ? Success("操作成功") : Error("操作失败");
        }

        /// <summary>
        /// 获取损益原因下拉
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLossProfitReasonListByType(string sybz = "")
        {
            return Content(_kcSyyyRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString() && (p.sybz == sybz || "" == sybz)).ToJson());
        }

        #endregion

        #region 损益查询

        /// <summary>
        /// 损益查询
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProfitAndLoss()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 获取损益明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult SelectLossAndProditInfoList(Pagination pagination, LossAndProditSearchDTO param)
        {
            var list = new
            {
                rows = _kcSyxxDmnService.SelectLossAndProditInfoList(pagination, param, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region Generate profit or loss

        /// <summary>
        /// 获取报损报溢单据号
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateDjh()
        {
            return Content(ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.Bsby.GetDescription()));
        }


        /// <summary>
        /// 获取责任人list
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public ActionResult GetZRRList(string inputCode)
        {
            var list = sysuserDmnService.GetStaffListByOrg(OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 损益提交
        /// </summary>
        /// <param name="plist"></param>
        /// <returns></returns>
        public ActionResult LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist)
        {
            var result = _stockInventoryApp.LossAndProfitSubmit(plist, Constants.CurrentKf.currentKfId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion
    }
}