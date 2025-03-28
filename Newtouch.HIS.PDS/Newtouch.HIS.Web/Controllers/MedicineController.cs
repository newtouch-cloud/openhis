using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.Tools.Excel;

namespace Newtouch.HIS.Web.Controllers
{
	/// <summary>
	/// xt_yp_ls_syxx 药品Controller
	/// </summary>
	public class MedicineController : ControllerBase
	{
		private readonly IMedicineInfoDmnService _medicineInfoDmnService;
		private readonly IHandOutMedicineDmnService _handOutMedicineDmnService;
		private readonly IMedicineApp _medicineApp;
		private readonly IMedicineDmnService _medicineDmnService;
		private readonly IPharmacyDrugStorageApp _pharmacyDrugStorageApp;
		private readonly IfyDmnService _fyDmnService;
		private readonly ISysStaffRepo _sysStaffRepo;
		private readonly ISysUserDmnService sysuserDmnService;
		private readonly ISysMedicineSupplierRepo isyMedicineSupplierRepo;
		private readonly IDrugStorageApp _drugStorageApp;

		/// <summary>
		/// 系统药品
		/// </summary>
		/// <returns></returns>
		public ActionResult SysMedicine()
		{
			return View();
		}

		public ActionResult MedicineInfo()
		{
			return View();
		}

		public ActionResult ExpiredDrugs()
		{
			return View();
		}

		/// <summary>
		/// 本部门药品信息
		/// </summary>
		/// <returns></returns>
		public ActionResult GetypGridJson(Pagination pagination, MedicineInfoParam model, string keyword, string organizeId)
		{
			ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
			model.ypzt = model.ypzt ?? "";
			model.syzt = model.syzt ?? "";
			var data = new
			{
				rows = _medicineInfoDmnService.GetMedicineInfoListV2(pagination, model, this.OrganizeId),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// 同步部门药品
		/// </summary>
		/// <returns></returns>
		public ActionResult SyncDeptDrug()
		{
			return View();
		}

		/// <summary>
		/// 盈亏查询
		/// </summary>
		/// <returns></returns>
		public ActionResult PriceAdjustmentProfitAndLossQuery()
		{
			return View();
		}

		/// <summary>
		///退货申请
		/// </summary>
		/// <returns></returns>
		public ActionResult RequestOfReturn()
		{
			return View();
		}

		/// <summary>
		/// 同步
		/// </summary>
		/// <param name="yp"></param>
		/// <param name="opereateType">0:添加  1：删除</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult FreshList(string[] yp, int opereateType)
		{
			var result = _medicineInfoDmnService.FreshList(yp, opereateType, OrganizeId, Constants.CurrentYfbm.yfbmCode);
			return string.IsNullOrWhiteSpace(result) ? Success("同步成功") : Error(result);
		}

		/// <summary>
		/// 获取同步候选药品
		/// </summary>
		/// <returns></returns>
		public ActionResult GettbypGridJson(Pagination pagination, MedicineInfoParam model)
		{
			var data = new
			{
				rows = _medicineInfoDmnService.GetTbMedicineInfoList(pagination, model, OrganizeId),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// 控制,取消控制
		/// </summary>
		/// <param name="ypCode"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public ActionResult ControlMedicine(string ypCode, string type)
		{
			_medicineInfoDmnService.ControlMedicine(ypCode, type, OrganizeId, Constants.CurrentYfbm.yfbmCode);
			return Success("操作成功");
		}

		/// <summary>
		/// 退药部门
		/// </summary>
		/// <param name="xtYpLsNbfymxk"></param>
		/// <param name="tybm"></param>
		/// <param name="tydh"></param>
		/// <param name="fyfs"></param>
		/// <returns></returns>
		public ActionResult ExecHandOutMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string tybm, string tydh, string fyfs)
		{
			var data = _handOutMedicineDmnService.RequestOfReturnMedicine(xtYpLsNbfymxk, tybm, tydh, fyfs, 5);
			return string.IsNullOrWhiteSpace(data) ? Success() : Error(data);
		}

		/// <summary>
		/// 系统药品供应商 数据源
		/// </summary>
		/// <returns></returns>
		public ActionResult MedicineSupplierList(string keyword)
		{
			var result = isyMedicineSupplierRepo.GetGysList(keyword, OrganizeId);
			return Content(result.ToJson());
		}

		/// <summary>
		/// 获取药品特殊属性
		/// </summary>
		/// <returns></returns>
		public ActionResult GetMedicineTSSXList()
		{
			var list = _pharmacyDrugStorageApp.GetDrugSpecialPropertiesList();
			return Content(list.ToJson());
		}

		/// <summary>
		/// 获取收货部门
		/// </summary>
		/// <returns></returns>
		public ActionResult GetMeidicineSHBMList()
		{
			var list = _fyDmnService.GetMeidicineSHBMList();
			return Content(list.ToJson());
		}

		#region 报损报溢

		/// <summary>
		/// 报损报溢
		/// </summary>
		/// <returns></returns>
		public ActionResult ReportLossAndProfit()
		{
			return View();
		}

		/// <summary>
		/// 获取新的损益单号
		/// </summary>
		/// <returns></returns>
		public ActionResult initialSYDH()
		{
			var sydh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("报损报溢单", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
			return Content(sydh);
		}

		/// <summary>
		/// 根据类型加载损益原因
		/// </summary>
		/// <param name="sylx"></param>
		/// <returns></returns>
		public ActionResult GetLossProfitReasonListByType(string sylx)
		{
			var list = _medicineApp.GetLossProfitReasonListByType(sylx);
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
			var list = _medicineInfoDmnService.GetStaffListByOrg(OrganizeId);
			return Content(list.ToJson());
		}

		/// <summary>
		/// 查询损益药品list
		/// </summary>
		/// <param name="inputCode"></param>
		/// <returns></returns>
		public ActionResult SelectLossAndProfitMedicineList(string inputCode)
		{
			return Content(_medicineApp.SelectLossAndProfitMedicineList(inputCode).ToJson());
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
			var result = _drugStorageApp.SubmitReportLossAndProfit(syxx);
			return string.IsNullOrWhiteSpace(result) ? Success(syIds) : Error(result);
		}

		/// <summary>
		/// 报损报溢 （保存）
		/// </summary>
		/// <param name="syxx"></param>
		/// <returns></returns>
		public ActionResult SaveReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
		{
			var syxxList = syxx != null && syxx.Length > 0 ? syxx.ToList() : new List<SysMedicineProfitLossEntity>();
			var profitLossEntityList = new List<YpSyxxVo>();
			if (syxxList.Count > 0)
			{
				syxxList.ForEach(p =>
				{
					profitLossEntityList.Add(new YpSyxxVo
					{
						Bgsj = DateTime.Now,
						Djh = p.Djh,
						Jj = p.Jj,
						Lsj = p.Lsj,
						OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
						pc = p.pc,
						Pfj = p.Pfj,
						Ph = p.Ph,
						remark = p.remark,
						Sykc = p.Sykc,
						Sysl = p.Sysl,
						Syyy = p.Syyy,
						yfbmCode = Constants.CurrentYfbm.yfbmCode,
						Yklsj = p.Yklsj,
						Ykpfj = p.Ykpfj,
						Ypdm = p.Ypdm,
						Yxq = p.Yxq,
						Zhyz = p.Zhyz,
						Zrr = p.Zrr
					});
				});
			}
			else
			{
				throw new FailedException("损益信息不能为空");
			}
			var result = _medicineDmnService.SaveReportLossAndProfit(profitLossEntityList);
			return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
		}

		#endregion

		#region 报损报溢查询
		/// <summary>
		/// 报损报溢查询
		/// </summary>
		/// <returns></returns>
		public ActionResult ReportLossAndProfitQuery()
		{
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
				rows = _medicineApp.SelectLossAndProditInfoList(pagination, startTime, endTime, inputCode, syyy, syqk),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(list.ToJson());
		}
		/// <summary>
		/// 盘点明细查询
		/// </summary>
		/// <returns></returns>
		public ActionResult InventoryQureyInfoList(Pagination pagination, string startTime, string endTime, string syyy, string inputCode, int syqk)
		{
			var list = new
			{
				rows = _medicineApp.InventoryQureyInfo(pagination, startTime, endTime, inputCode, syyy, syqk),
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
			return Content(_medicineApp.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk).ToJson());
		}


		#endregion
		/// <summary>
		/// 盘盈盘亏查询
		/// </summary>
		/// <returns></returns>
		public ActionResult InventoryQureyReport()
		{
			return View();
		}
		#region 调价盈亏查询

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="Srm"></param>
		/// <param name="yfbmCode"></param>
		/// <param name="Lkc"></param>
		/// <returns></returns>
		public ActionResult SelectPriceAdjustmentProfitLossList(Pagination pagination, DateTime? startTime, DateTime? endTime, string Srm, string yfbmCode, string Lkc)
		{
			var boolLkc = Lkc == "1";
			var allUseableYfbmCodes = string.Join(",", OperatorProvider.GetCurrent().yfbmCodeList.ToArray());
			//const string allUseableYfbmCodes = "'yjk','mzyf','zyyf'";
			var list = new
			{
				rows = _medicineDmnService.SelectPriceAdjustmentProfitLossList(pagination, startTime, endTime, Srm, yfbmCode, boolLkc, allUseableYfbmCodes),
				tatal = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(list.ToJson());
		}
		#endregion
		public ActionResult ExcelGet(bool? isContainFilter, int colStanWidth, string cols, DateTime? startTime, DateTime? endTime, string Srm, string yfbmCode, string Lkc, string yfykmc)
		{
			var orgId = this.OrganizeId;
			if (string.IsNullOrEmpty(orgId))
			{
				throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
			}
			if (string.IsNullOrWhiteSpace(cols))
			{
				cols = WebHelper.GetCookie("ExportExcelCols");
				if (!string.IsNullOrWhiteSpace(cols))
				{
					cols = System.Web.HttpUtility.UrlDecode(cols);
					WebHelper.RemoveCookie("ExportExcelCols");
				}
			}
			if (string.IsNullOrWhiteSpace(cols))
			{
				throw new FailedException("未指定导出列");
			}
			var pagination = new Pagination();
			pagination.sidx = "Tjsj desc"; //时间升序
			pagination.rows = 65536 - 1;    //Excel最大行数
			pagination.page = 1;    //第一页把所有都查出来
			var boolLkc = Lkc == "1";
			var allUseableYfbmCodes = string.Join(",", OperatorProvider.GetCurrent().yfbmCodeList.ToArray());
			var list = _medicineDmnService.SelectPriceAdjustmentProfitLossList(pagination, startTime, endTime, Srm, yfbmCode, boolLkc, allUseableYfbmCodes);
			var colList = cols.ToObject<IList<ExcelColumn>>();
			var sheet = new ExcelSheet()
			{
				Title = "调价盈亏查询",
				columns = colList,
			};
			sheet.columns.Where(p => p.Name == "mzzybz").ToList().ForEach(t => t.FuncGetCellValue = (obj) =>
			{
				return obj.ToString() == "0" ? "通用" : obj.ToString() == "1" ? "门诊" : "住院";
			});
			sheet.columns.Where(p => p.Name == "zt").ToList().ForEach(t => t.FuncGetCellValue = (obj) =>
			 {
				 return obj.ToString() == "0" ? "无效" : "有效";
			 });
			//sheet.columns.Where(p => p.Name == "tjsyze").ToList().ForEach(t =>
			//{
			//    t.NumberDigits = 2;
			//});
			sheet.columns.ToList().ForEach(p =>
			{
				p.WidthTimes = (double)p.Width / colStanWidth;
				p.Width = 0;    //Width都置为0
			});

			var path = DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff") + "调价盈亏查询" + ".xls";

			var filePath = CommmHelper.GetLocalFilePath("\\Excel导出\\调价盈亏查询导出" + path, "D:\\");

			if (isContainFilter == true)
			{
				//筛选条件
				var filterDict = new Dictionary<string, string>();
				if (startTime.HasValue)
				{
					filterDict.Add("开始日期", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
				}
				if (endTime.HasValue)
				{
					filterDict.Add("结束日期", endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
				}
				if (!string.IsNullOrWhiteSpace(Srm))
				{
					filterDict.Add("关键字", Srm);
				}
				if (!string.IsNullOrWhiteSpace(yfykmc))
				{
					filterDict.Add("药房药库", yfykmc);
				}
				if (!string.IsNullOrWhiteSpace(Lkc))
				{
					filterDict.Add("显示零库存记录", Lkc == "0" ? "显示" : "不显示");
				}
				if (filterDict.Count > 0)
				{
					sheet.filters = filterDict;
				}
			}

			var rest = list.ToExcel(filePath, sheet);

			if (rest)
			{
				return File(filePath, "application/x-xls", path.Replace("\\", ""));
			}
			else
			{
				return Content("文件导出失败，请返回列表页重试");
			}
		}


		#region 获取过期药品
		public ActionResult GetExpiredDrugsGridJson(Pagination pagination, MedicineInfoParam model)
		{
			var data = new
			{
				rows = _medicineInfoDmnService.GetExpiredDrugsData(pagination, model, this.OrganizeId),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}
		#endregion
	}
}