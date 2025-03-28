using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Redis;
using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    public class TransferController : ControllerBase
    {
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysMedicineStorageIOReceiptRepo _djRepo;
        private readonly ISysMedicineStorageIOReceiptDetailRepo _djmxRepo;
        private readonly IApplyDmnService _applyDmnService;
        private readonly IDrugStorageDmnService _drugStorageDmnService;
        private readonly IHandOutMedicineDmnService _handOutMedicineDmnService;
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacy;

        /// <summary>
        /// 申请调拨 
        /// GET: OutOrInStoredManage/Transfer/Apply
        /// </summary>
        /// <returns></returns>
        public ActionResult Apply()
        {
            return View();
        }

        public ActionResult GenerateNewDjh()
        {
            const string _prefix = "DBSQD";
            var numbStr = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var numbInt = Convert.ToInt64(numbStr);
            var newDjh = $"{_prefix}{numbStr}";
            while (RedisHelper.Exists(newDjh))
            {
                numbInt += 1;
                newDjh = $"{_prefix}{numbInt}";
            }
            RedisHelper.StringSet(newDjh, numbInt.ToString(), (DateTime.Now.AddMinutes(2) - DateTime.Now));

            return Content(newDjh);
        }

        /// <summary>
        /// 获取药房列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetYfbmList(string keyword)
        {
            var data = _pharmacyDrugStorageDmnService.GetYfbmList(keyword, OrganizeId);
            if (data == null || data.Count == 0)
            {
                return Content((new List<object>()).ToJson());
            }

            var targetCode = data.Where(p => p.yfbmCode != Constants.CurrentYfbm.yfbmCode)?.Select(p => new { p.yfbmCode, p.yfbmmc }).ToList();
            return Content(targetCode.ToJson());
        }

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="djInfoDTO"></param>
        /// <returns></returns>
        public ActionResult SubmitApply(DjInfoDTO djInfoDTO)
        {
            #region 出入库单据

            var dj = new SysMedicineStorageIOReceiptEntity
            {
                crkId = Guid.NewGuid().ToString(),
                OrganizeId = OrganizeId,
                djlx = (int)EnumDanJuLX.shenqingdiaobo,
                Pdh = djInfoDTO.djh,
                Ckbm = djInfoDTO.ckbm,
                Rkbm = Constants.CurrentYfbm.yfbmCode,
                Sqsj = DateTime.Now,
                Czsj = DateTime.Now,
                Crkfsdm = "12",
                zt = "1",
                shzt = ((int)EnumDjShzt.WaitingApprove).ToString(),
                CreateTime = DateTime.Now,
                CreatorCode = UserIdentity.UserCode
            };
            var djmx = new List<SysMedicineStorageIOReceiptDetailEntity>();
            foreach (var item in djInfoDTO.mx)
            {
                item.sldmxId = Guid.NewGuid().ToString();
                djmx.Add(new SysMedicineStorageIOReceiptDetailEntity
                {
                    crkmxId = Guid.NewGuid().ToString(),
                    Ypdm = item.ypdm,
                    Sl = item.sl,
                    Rkbmkc = 0,
                    Rkzhyz = (int)item.rkzhyz,
                    Ckbmkc = (int)item.ckbmkc,
                    Ckzhyz = (int)item.ckzhyz,
                    crkId = dj.crkId,
                    jj = item.jj,
                    Lsj = item.lsj,
                    pc = "",
                    Ph = "",
                    Pfj = item.pfj,
                    sldmxId = item.sldmxId,
                    Yklsj = item.yklsj,
                    Ykpfj = item.ykpfj,
                    Yxq = null,
                    Zje = (decimal)item.zxdwjj * item.sl * (int)item.rkzhyz,
                    zt = "1",
                    CreateTime = DateTime.Now,
                    CreatorCode = dj.CreatorCode,
                });
            }
            if (djmx.IsEmpty())
            {
                return Error("请录入调拨药品明细");
            }
            if (_djRepo.Insert(dj) <= 0)
            {
                return Error("保存申请调拨单失败");
            }

            _djmxRepo.Insert(djmx);

            #endregion

            #region 申领单

            djInfoDTO.djlx = (int)EnumSldlx.shenqingdiaobodan;
            djInfoDTO.rkbm = Constants.CurrentYfbm.yfbmCode;
            var result = new ApplyProcess(djInfoDTO).Process();
            if (!result.IsSucceed)
            {
                _applyDmnService.Cancel(dj.crkId, UserIdentity.UserCode);
            }

            #endregion

            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }

        /// <summary>
        /// 调拨入库
        /// GET: OutOrInStoredManage/Transfer/Warehousing
        /// </summary>
        /// <returns></returns>
        public ActionResult Warehousing()
        {
            return View();
        }

        /// <summary>
        /// 获取申领主表信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh">申领单号</param>
        /// <param name="fybm">出库部门</param>
        /// <param name="ffzt">发药状态</param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <returns></returns>
        public ActionResult RequestInfo(string sldh, string fybm, string ffzt, DateTime startDate, DateTime endDate)
        {
            ffzt = string.IsNullOrWhiteSpace(ffzt) ? "-1" : ffzt;
            var rows = _drugStorageDmnService.GetTransferOrders(sldh, Constants.CurrentYfbm.yfbmCode, fybm, ffzt, startDate, endDate);
            return Content(rows.ToJson());
        }

        /// <summary>
        /// 获取申领单药品信息
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        public ActionResult RequestMedicineInfo(string sldId)
        {
            if (sldId == "undefined")
            {
                sldId = "";
            }
            var data = _drugStorageDmnService.GetSldInfo(sldId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 调拨入库
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult ExecHandOutMedicineByReq(Dictionary<string, List<SysMedicineReDetailVO>> rkdmx)
        {
            var res = _handOutMedicineDmnService.TransferWarehousing(rkdmx, Constants.CurrentYfbm.yfbmCode, UserIdentity.UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(res) ? Success() : Error(res);
        }

        /// <summary>
        /// 获取要申领的药品列表
        /// </summary>
        /// <param name="rkbm"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DrugStockSearch(string keyword, string yfbmCode)
        {
            var result = _drugStorageDmnService.GetDrugStock(yfbmCode, Constants.CurrentYfbm.yfbmCode, keyword, OrganizeId);
            return Content(result.ToJson());
        }
    }
}