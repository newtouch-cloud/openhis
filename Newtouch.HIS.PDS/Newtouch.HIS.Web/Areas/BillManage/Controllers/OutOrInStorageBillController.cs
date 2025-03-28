using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// 出入库单据管理
    /// </summary>
    public class OutOrInStorageBillController : ControllerBase
    {
        private readonly IApplyDmnService applyDmnService;

        #region 审核

        /// <summary>
        /// 单据审核 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Approval()
        {
            return View();
        }
        public ActionResult DrugPreparation()
        {
            return View();
        }
        public ActionResult OutOrder()
		{
			return View();
		}

		/// <summary>
		/// Submit Receipt Approval
		/// </summary>
		/// <param name="param"></param>
		/// <returns></returns>
		public ActionResult SubmitReceiptApproval(OutOrInStorageBillApprovalDTO param)
        {
            param.auditor = UserIdentity.UserCode;
            var result = new OutOrInStorageBillApprovalProcess(param).Process();
            return result.IsSucceed ? Success("操作成功") : Error(result.ResultMsg);
        }

        /// <summary>
        /// Submit Receipt Approval
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult SubmitReceiptApprovalBatch(List<OutOrInStorageBillApprovalDTO> param)
        {
            var auditor = UserIdentity.UserCode;
            Parallel.ForEach(param, item => { item.auditor = auditor; });
            var result = new StringBuilder();
            param.ForEach(p =>
            {
                var processResult = new OutOrInStorageBillApprovalProcess(p).Process();
                if (!processResult.IsSucceed)
                {
                    result.AppendLine(processResult.ResultMsg);
                }

            });
            return result.Length <= 0 ? Success("操作成功") : Error(result.ToString());
        }
        #endregion

        #region 查询

        /// <summary>
        /// 单据查询 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 出入库单据明细查询
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        [HttpGet]
        [Core.Attributes.HandlerAuthorizeIgnore]
        public ActionResult QueryOutOrInStorageBillDetail(string crkId, int djlx)
        {
            var result = applyDmnService.SelectOutOrInStorageBillDetail(crkId, djlx);
            return Content(result.ToJson());
        }
        public ActionResult SelectDrupreparationInfoMX(string byid)
        {
            var result = applyDmnService.SelectDrupreparationInfoMX(byid);
            return Content(result.ToJson());
        }
        #endregion
    }
}