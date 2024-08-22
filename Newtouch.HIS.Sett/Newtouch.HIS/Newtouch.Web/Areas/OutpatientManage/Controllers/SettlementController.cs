using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Tools.Excel;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SettlementController : ControllerBase
    {
        private readonly IOutPatChargeDmnService _outChargeDmnService;
        private readonly IOutPatientChargeQueryDmnService _outPatienChargeQueryDmnService;

        #region 结算查询
        public ActionResult SettQuery()
        {
            return View();
        }

        public ActionResult GetSettlementGridJson(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, bool? containsTF = true)
        {
            IList<OutpatientSettlementVO> list;
            var data = new
            {
                rows = (list = _outChargeDmnService.GetSettlementList(pagination, this.OrganizeId, keyword, kssj, jssj, containsTF.Value)),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            var jsnms = string.Join(",", list.Select(p => p.jsnm).Distinct());
            if (!string.IsNullOrWhiteSpace(jsnms))
            {
                var zffsList = _outPatienChargeQueryDmnService.GetSettZffsResultList(this.OrganizeId, jsnms);
                foreach (var js in list)
                {
                    js.zffsmcstr = string.Join(",", zffsList.Where(p => p.jsnm == js.jsnm).Select(p => p.xjzffsmc));
                }
            }

            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult SettlementExportExcel(string keyword, DateTime? kssj, DateTime? jssj, int colStanWidth, bool? containsTF = true)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var cols = WebHelper.GetCookie("ExportExcelCols");
            if (!string.IsNullOrWhiteSpace(cols))
            {
                cols = System.Web.HttpUtility.UrlDecode(cols);
                WebHelper.RemoveCookie("ExportExcelCols");
            }
            if (string.IsNullOrWhiteSpace(cols))
            {
                throw new FailedException("未指定导出列");
            }
            var pagination = new Pagination();
            pagination.sidx = "jsrq desc"; //时间升序
            pagination.rows = 65536 - 1;    //Excel最大行数
            pagination.page = 1;    //第一页把所有都查出来
            var list = _outChargeDmnService.GetSettlementList(pagination, orgId, keyword, kssj, jssj, containsTF.Value);
            
            //长度限制？
            var jsnms = string.Join(",", list.Select(p => p.jsnm).Distinct());
            if (!string.IsNullOrWhiteSpace(jsnms))
            {
                var zffsList = _outPatienChargeQueryDmnService.GetSettZffsResultList(this.OrganizeId, jsnms);
                foreach (var js in list)
                {
                    js.zffsmcstr = string.Join(",", zffsList.Where(p => p.jsnm == js.jsnm).Select(p => p.xjzffsmc));
                }
            }

            var colList = cols.ToObject<IList<ExcelColumn>>();
            var sheet = new ExcelSheet()
            {
                Title = "门诊结算一览表",
                columns = colList,
            };
            sheet.columns.Where(p => p.Name == "jsrq").ToList().ForEach(p =>
            {
                p.DateTimeFormat = "yyyy-MM-dd";
            });
            sheet.columns.Where(p => p.Name == "xb").ToList().ForEach(t => t.FuncGetCellValue = (obj) =>
            {
                if (obj == null)
                {
                    return "";
                }
                return obj.ToString().Equals("1") ? "男" : (obj.ToString().Equals("2") ? "女" : "");
            });
            sheet.columns.Where(p => p.Name == "jszt").ToList().ForEach(t => t.FuncGetCellValue = (obj) =>
            {
                if (obj == null)
                {
                    return "";
                }
                return obj.ToString().Equals("2") ? "退费" : (obj.ToString().Equals("1") ? "收费" : "");
            });
            sheet.columns.Where(p => p.Name == "jszje" || p.Name == "jsxjzf" || p.Name == "YBFY" || p.Name == "JBZF" || p.Name == "GBZF").ToList().ForEach(t => t.FuncByFullObjectGetCellValue = (obj, fullObj) =>
            {
                if (obj == null)
                {
                    return "0.00";
                }
                var val = decimal.Parse(obj.ToString());
                var str = val.ToString("0.00");

                var fullObjVO = fullObj as OutpatientSettlementVO;
                if (fullObjVO.jszt == 2 && val > 0)
                {
                    str = '-' + str;
                }

                return str;
            });
            sheet.columns.ToList().ForEach(p =>
            {
                p.WidthTimes = (double)p.Width / colStanWidth;
                p.Width = 0;    //Width都置为0
            });

            var path = DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff") + ".xls";

            var filePath = CommmHelper.GetLocalFilePath("\\Excel导出\\门诊结算" + path);

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

        #endregion
    }
}
