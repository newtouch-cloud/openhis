using Newtouch.Core.Common;
using System.Web.Mvc;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// 出入库单操作
    /// </summary>
    public class OutOrInStorageBillController : ControllerBase
    {
        private readonly IKfCrkdjDmnService _kfCrkdjDmnService;
        private readonly IKfCrkdjRepo _crkdjRepo;
        private readonly IKfCrkmxRepo _crkmxRepo;
        private readonly IOutOrInStorageBillApprovalApp _outOrInStorageBillApprovalApp;

        #region 单据审核

        /// <summary>
        /// 单据审核
        /// </summary>
        /// <returns></returns>
        public ActionResult Approval()
        {
            return View();
        }

        /// <summary>
        /// 出入库单据 审核
        /// </summary>
        /// <param name="billApprovalDTO"></param>
        /// <returns></returns>
        public ActionResult SubmitReceiptApproval(BillApprovalDTO billApprovalDTO)
        {
            var result = _outOrInStorageBillApprovalApp.Approval(billApprovalDTO);
            return string.IsNullOrWhiteSpace(result) ? Success("操作成功") : Error(result);
        }

        /// <summary>
        /// 修改发票号
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public ActionResult UpdateFph(long crkmxId, string fph)
        {
            return _crkmxRepo.UpdateFph(crkmxId, fph) > 0 ? Success() : Error("修改发票号失败");
        }
        
        /// <summary>
        /// 打回申请 从待处理变成暂存，只针对外部入库
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        public ActionResult BackToTemporary(long crkId)
        {
            var result = _crkdjRepo.BackToTemporary(crkId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region 单据查询

        /// <summary>
        /// 单据查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 单据查询 主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public ActionResult SelectReceiptMainInfo(Pagination pagination, CrkdjSearchParamDTO param, string from = "query")
        {
            var allUseableDjlx = new[] {
                ((int)EnumOutOrInStorageBillType.Wbrk).ToString(),
                ((int)EnumOutOrInStorageBillType.Wbck).ToString(),
                ((int)EnumOutOrInStorageBillType.Zjck).ToString(),
                ((int)EnumOutOrInStorageBillType.Slck).ToString(),
                ((int)EnumOutOrInStorageBillType.Nbth).ToString(),
                ((int)EnumOutOrInStorageBillType.chukuzhikeshi).ToString()
            };
            switch (from)
            {
                case "query":
                    if (Constants.CurrentKf.currentKfLevel != 1)
                    {
                        allUseableDjlx = new[]
                        {
                            ((int)EnumOutOrInStorageBillType.Zjck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Slck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Nbth).ToString(),
                            ((int)EnumOutOrInStorageBillType.chukuzhikeshi).ToString()
                        };
                    }
                    break;
                case "approval":
                    allUseableDjlx = Constants.CurrentKf.currentKfLevel != 1 ? new[] {
                            ((int)EnumOutOrInStorageBillType.Zjck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Slck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Nbth).ToString() } :
                        new[] {
                            ((int)EnumOutOrInStorageBillType.Wbrk).ToString(),
                            ((int)EnumOutOrInStorageBillType.Wbck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Zjck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Nbth).ToString()
                        };
                    break;
            }
            param.organizeId = OrganizeId;
            var receiptMaininfoList = new
            {
                rows = _kfCrkdjDmnService.GetCrkdjMainList(pagination, param, allUseableDjlx, Constants.CurrentKf.currentKfId, from),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }

        /// <summary>
        /// 单据查询明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        public ActionResult SelectReceipDetailInfo(string crkId)
        {
            return Content(_kfCrkdjDmnService.GetCrkdjmxList(crkId, OrganizeId).ToJson());
        }

        /// <summary>
        /// 获取常用的审核状态
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryCommonAuditState()
        {
            var result = new object[] {
               new { id = "", text = "==全部==" },
               new { id = (int)EnumAuditState.Waiting, text = EnumAuditState.Waiting.GetDescription() },
               new { id = (int)EnumAuditState.Adopt, text = EnumAuditState.Adopt.GetDescription() },
               new { id = (int)EnumAuditState.Refuse, text = EnumAuditState.Refuse.GetDescription() },
               new { id = (int)EnumAuditState.Cancelled, text = EnumAuditState.Cancelled.GetDescription() },
               
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// 根据配送单号和供应商ID获取出入库单据明细
        /// </summary>
        /// <param name="deliveryNo"></param>
        /// <param name="gysId"></param>
        /// <returns></returns>
        public ActionResult QueryCrkmxByDeliveryNo(string deliveryNo, string gysId, string djh)
        {
            var result = _kfCrkdjDmnService.SelectCrkmxByDeliveryNo(deliveryNo, djh, Constants.CurrentKf.currentKfId, OrganizeId, gysId);
            return Content(result.ToJson());
        }

        #endregion

        #region 我的暂存

        /// <summary>
        /// 我的暂存 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MyTemporaryCrkdj()
        {
            return View();
        }

        /// <summary>
        /// 删除暂存单
        /// </summary>
        /// <param name="billApprovalDTO"></param>
        /// <returns></returns>
        public ActionResult DeleteTemporaryCrkdj(BillApprovalDTO billApprovalDTO)
        {
            var result = _kfCrkdjDmnService.DeleteCrkdj(billApprovalDTO.djId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// 根据配送单号获取暂存的单据明细
        /// </summary>
        /// <param name="deliveryNo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QueryTemporaryCrkmx(string deliveryNo, string djh)
        {
            var result = _kfCrkdjDmnService.SelectCrkmxByDeliveryNo(deliveryNo, djh, Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }

        #endregion
    }
}