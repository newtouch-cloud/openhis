using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class PrepareMedicineController : OrgControllerBase
    {

        private readonly IPrepareMedicineApp _prepareMedicineApp;
        private readonly ISysUserDmnService sysuserDmnService;
        private readonly IPrepareMedicineDmnService _prepareMedicineDmnService;

        // GET: NurseManage/PrepareMedicine
        public ActionResult Index()
        {
            return View();
        }

        #region 科室备药损益
        public ActionResult ReportLossAndProfit()
        {
            return View();
        }

        /// <summary>
        /// 获取新的损益单号
        /// </summary>
        /// <returns></returns>
        public ActionResult initialSYDH(string djmc)
        {
            var sydh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo(djmc, Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(sydh);
        }

        /// <summary>
        /// 根据类型加载损益原因
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        public ActionResult GetLossProfitReasonListByType(string sylx)
        {
            var list = _prepareMedicineApp.GetLossProfitReasonListByType(sylx);
            var obj = list.Select(a => new { syyyId = a.syyyId, syyy = a.syyy }).ToJson();
            return Content(obj);
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
        /// 查询损益药品list
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public ActionResult SelectLossAndProfitMedicineList(string inputCode)
        {
            var list = _prepareMedicineApp.SelectLossAndProfitMedicineList(inputCode);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 提交报损报益
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public ActionResult SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        {
            if (syxx == null || syxx.Length <= 0) return Error("请传入损益明细");
            var organizeId = OrganizeId;
            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var userCode = OperatorProvider.GetCurrent().UserCode;
            var syIds = "";
            Parallel.ForEach(syxx, item =>
            {
                item.syId = Guid.NewGuid().ToString();
                item.OrganizeId = organizeId;
                item.yfbmCode = yfbmCode;
                item.CreatorCode = userCode;
                item.CreateTime = DateTime.Now;
                item.zt = "1";
                item.Bgsj = DateTime.Now;
                syIds += item.syId + ",";
            });
            var result = _prepareMedicineApp.SubmitReportLossAndProfit(syxx);
            return string.IsNullOrWhiteSpace(result) ? Success(syIds) : Error(result);
        }

        ///// <summary>
        ///// 报损报溢 （保存）
        ///// </summary>
        ///// <param name="syxx"></param>
        ///// <returns></returns>
        //public ActionResult SaveReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        //{
        //    var syxxList = syxx != null && syxx.Length > 0 ? syxx.ToList() : new List<SysMedicineProfitLossEntity>();
        //    var profitLossEntityList = new List<YpSyxxVo>();
        //    if (syxxList.Count > 0)
        //    {
        //        syxxList.ForEach(p =>
        //        {
        //            profitLossEntityList.Add(new YpSyxxVo
        //            {
        //                Bgsj = DateTime.Now,
        //                Djh = p.Djh,
        //                Jj = p.Jj,
        //                Lsj = p.Lsj,
        //                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
        //                pc = p.pc,
        //                Pfj = p.Pfj,
        //                Ph = p.Ph,
        //                remark = p.remark,
        //                Sykc = p.Sykc,
        //                Sysl = p.Sysl,
        //                Syyy = p.Syyy,
        //                yfbmCode = Constants.CurrentYfbm.yfbmCode,
        //                Yklsj = p.Yklsj,
        //                Ykpfj = p.Ykpfj,
        //                Ypdm = p.Ypdm,
        //                Yxq = p.Yxq,
        //                Zhyz = p.Zhyz,
        //                Zrr = p.Zrr
        //            });
        //        });
        //    }
        //    else
        //    {
        //        throw new FailedException("损益信息不能为空");
        //    }
        //    var result = _medicineDmnService.SaveReportLossAndProfit(profitLossEntityList);
        //    return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        //}

        #endregion

        #region 科室备药损益查询

        /// <summary>
        /// 科室备药退回
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportLossAndProfitQuery()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }


        /// <summary>
        /// 报损报溢查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            var list = new
            {
                rows = _prepareMedicineApp.SelectLossAndProditInfoList(pagination, startTime, endTime, inputCode, syyy, syqk),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }


        /// <summary>
        /// 批发价总金额、零售价总金额查询
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="syyy">损益原因</param>
        /// <param name="inputCode">关键字</param>
        /// <param name="syqk">损益情况</param>
        /// <returns></returns>
        public ActionResult ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            return Content(_prepareMedicineApp.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk).ToJson());
        }
        #endregion

        #region 科室备药退回

        /// <summary>
        /// 科室备药退回
        /// </summary>
        /// <returns></returns>
        public ActionResult PrepareMedicineReturn()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }

        /// <summary>
        /// 新建退回申请单
        /// </summary>
        /// <returns></returns>
        public ActionResult PrepareMedicineReturnAdd() {
            return View();
        }

        /// <summary>
        /// 保存科室备药退回
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult SubmitKsbyth(BythDjInfoDTO Djnr)
        {
            OperatorModel user = this.UserIdentity;
            var data = _prepareMedicineDmnService.PrepareMedicineReturnSubmit(user, this.OrganizeId, Djnr);
            return Success(data);
        }

        /// <summary>
        /// 科室备药申请单：药品信息绑定
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbm"></param>
        /// <returns></returns>
        public ActionResult DrugAndStockSearch(string keyword, string yfbm)
        {
            var result = _prepareMedicineDmnService.GetDrugAndStock(yfbm, keyword, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// 科室备药药品填充查询
        /// </summary>
        /// <param name="ypcodestr"></param>
        /// <param name="yfbmstr"></param>
        /// <returns></returns>
        public ActionResult ApplyDrugsSearch(string ypcodestr, string yfbmstr)
        {
            var result = _prepareMedicineDmnService.GetApplyDrugAndStock(ypcodestr, yfbmstr, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// 科室备药退回查询列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="xm"></param>
        /// <param name="bqCode"></param>
        /// <param name="ypmc"></param>
        /// <param name="cw"></param>
        /// <param name="zyh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetPrepareMedicineReturnGridJson(Pagination pagination, string thzt,  DateTime kssj, DateTime jssj)
        {
            var tt = _prepareMedicineDmnService.GetPrepareMedicineReturnGridJson(pagination, thzt, kssj, jssj, OrganizeId);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        /// <summary>
        /// 单据明细
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public ActionResult QueryPrepareMedicine(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicine(djId, this.OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 备药单据提交
        /// </summary>
        /// <param name="djmc"></param>
        /// <returns></returns>
        public ActionResult UpdatePrepareMedicineReturn(PrapareMedicineReturnEntity entity)
        {
            OperatorModel user = this.UserIdentity;
            entity.OrganizeId = this.OrganizeId;
            entity.LastModifierCode = user.UserId;
            entity.LastModifyTime= DateTime.Now.Date;
            entity.tjsj = DateTime.Now.Date;
            var data = _prepareMedicineDmnService.UpdatePrepareMedicineReturn( entity);
            return Success(data);
        }
        /// <summary>
        /// 根据ID查询单据列表
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>

        public ActionResult QueryPrepareMedicineReturnbyId(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicineReturnbyId(djId, this.OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// 根据ID查询单据明细
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public ActionResult QueryPrepareMedicineReturnMXbyId(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicineReturnMXbyId(djId, this.OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// 获取药品库存数量
        /// </summary>
        /// <param name="ypbm"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="yfbm"></param>
        /// <returns></returns>
        public ActionResult BydjQueryKykc(string ypbm, string pc, string ph, string yfbm)
        {
            var result = new
            {
                kysl = _prepareMedicineDmnService.BydjQueryKykc(ypbm, pc, ph, yfbm, this.OrganizeId)
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="Djh"></param>
        /// <returns></returns>
        public ActionResult Bydjback(string Djh)
        {
            OperatorModel user = this.UserIdentity;
            var data = _prepareMedicineDmnService.PrepareMedicineReturnback(Djh, OrganizeId, user);
            return Success(data);
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="ById"></param>
        /// <returns></returns>
        public ActionResult Bydjdelete(string ById)
        {
            OperatorModel user = this.UserIdentity;
            var data = _prepareMedicineDmnService.PrepareMedicineReturndelete(ById, OrganizeId, user);
            return Success(data);
        }
        #endregion

        #region 药品使用查询
        public ActionResult MedicineUseQuery()
        {
            //ViewBag.OrgId = this.OrganizeId;
            //ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }

        //药品信息
        public ActionResult GetypGridJson(Pagination pagination, MedicineInfoParam model, string keyword, string organizeId)
        {
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            model.ypzt = model.ypzt ?? "";
            model.syzt = model.syzt ?? "";
            var data = new
            {
                rows = _prepareMedicineDmnService.GetMedicineInfoListV2(pagination, model, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion
    }
}