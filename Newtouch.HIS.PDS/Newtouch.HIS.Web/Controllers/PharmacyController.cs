using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using System.Web.Mvc;
using Newtouch.Common.Operator;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 药房Controller
    /// </summary>
    public class PharmacyController : ControllerBase
    {
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly IDepartmentMedicineInfoDmnService _departmentMedicineInfoDmnService;
        private readonly IRequisitionDmnService _requisitionDmnService;


        /// <summary>
        /// 内部申领
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionForm()
        {
            return View();
        }

        /// <summary>
        /// 初始化单据号
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionGetNewDjh()
        {
            var data = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("内部申领单", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(data);
        }

        /// <summary>
        /// 内部申领 发药部门 Sele
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionFybmSele(string keyword)
        {
            var data = _sysPharmacyDepartmentApp.GetTheUpperYkbmCodeList(keyword, Constants.CurrentYfbm.yfbmCode, OperatorProvider.GetCurrent().OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 内部申领 输入码 Sele
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionSrmSele(string keyword, string fybm)
        {
            if (string.IsNullOrWhiteSpace(fybm))
            {
                return null;
            }
            var data = _departmentMedicineInfoDmnService.GetRequisitionDepartmentMedicineSeleVOList((keyword ?? "").Trim(), fybm, OperatorProvider.GetCurrent().OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 内部申领 提交表单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitRequisition(string sldh, string fybmCode, IList<RequisitionDepartmentMedicineSubmitItemVO> slmxList)
        {
            if (string.IsNullOrWhiteSpace(sldh))
            {
                throw new FailedException("缺少申领单号");
            }
            if (string.IsNullOrWhiteSpace(fybmCode))
            {
                throw new FailedException("请选择发药部门");
            }
            if (slmxList == null || slmxList.Count == 0)
            {
                throw new FailedException("未提交明细数据（非正确格式）");
            }
            _requisitionDmnService.SubmitRequisition(sldh, fybmCode, Constants.CurrentYfbm.yfbmCode, slmxList, OperatorProvider.GetCurrent().OrganizeId);

            return Success();
        }


        /// <summary>
        /// 申领单查询（药房部门发出去的）
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionSearchIndex()
        {
            return View();
        }

        /// <summary>
        /// 申领出库 申领单 数据源
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sldh"></param>
        /// <param name="slbm"></param>
        /// <param name="ckbm"></param>
        /// <param name="ffzt"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public ActionResult RequisitionGridJson(Pagination pagination, DateTime? startDate, DateTime? endDate, string sldh, string slbm, string ckbm, int? ffzt, string from = "query")
        {
            var curYfbm = Constants.CurrentYfbm;
            var allUseableFfzt = "0,1,2,3";
            switch (@from)
            {
                case "query":
                    slbm = curYfbm.yfbmCode;
                    break;
                case "slck":
                    ckbm = curYfbm.yfbmCode;
                    allUseableFfzt = "0,1";
                    break;
                default:
                    return null;
            }

            var receiptMaininfoList = new
            {
                rows = _requisitionDmnService.RequisitionSearch(pagination, OperatorProvider.GetCurrent().OrganizeId, startDate, endDate
                , sldh, slbm, ckbm, ffzt, allUseableFfzt),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }

        /// <summary>
        /// 申领出库 申领单 明细数据 数据源
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        public ActionResult RequisitionDetailList(string sldId)
        {
            var list = _requisitionDmnService.RequisitionDetailListBySlId(sldId, OperatorProvider.GetCurrent().OrganizeId);
            return list == null ? Error("获取申领单明细失败") : Success(null, list);
        }


        /// <summary>
        /// 请求获取当前部门 的 ypCode药品 的 库存量
        /// </summary>
        /// <param name="ypCode">药品代码</param>
        /// <param name="fybm">发药部门</param>
        /// <returns></returns>
        public ActionResult GetYpKcslAndYpdw(string ypCode, string fybm)
        {
            var data = _departmentMedicineInfoDmnService.GetYpKcslAndYpdw(Constants.CurrentYfbm.yfbmCode, ypCode, OperatorProvider.GetCurrent().OrganizeId, fybm) ?? new DepartmentMedicineStockUnitVO();
            return Success(null, data);
        }

    }
}