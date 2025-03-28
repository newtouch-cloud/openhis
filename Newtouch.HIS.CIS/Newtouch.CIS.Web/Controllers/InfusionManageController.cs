using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Application.Interface;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels;
using Newtouch.Domain.ViewModels.Outpatient;
using Autofac;
using Newtouch.HIS.Web.Core.Attributes;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// 输液管理 移动端
    /// </summary>
    public class InfusionManageController : Controller
    {
        private readonly IMzsyypxxRepo _mzsyypxxRepo;
        private readonly IInfusionApp _infusionApp;

        public InfusionManageController(IMzsyypxxRepo mzsyypxxRepo, IInfusionApp infusionApp)
        {
            _mzsyypxxRepo = mzsyypxxRepo;
            _infusionApp = infusionApp;
        }

        /// <summary>
        /// 输液信息查询
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Query(string tags, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(tags))
            {
                return View();
            }
            var ids = tags.Split(',');
            if (ids.Length == 0)
            {
                return View();
            }
            var tmpids = ids.Select(item => Convert.ToInt64(item)).Where(i => i != 0).ToList();
            var drugs = _infusionApp.MzsyxxQuery(tmpids, organizeId);
            ViewBag.drugs = drugs ?? new List<MzsyypxxVO>();
            ViewBag.patInfo = new InfusionExecVO();
            if (drugs != null && drugs.Count > 0)
            {
                ViewBag.patInfo.kh = drugs[0].kh;
                ViewBag.patInfo.patientName = drugs[0].xm;
                ViewBag.patInfo.seatNum = drugs[0].seatNum;
                ViewBag.patInfo.remark = drugs[0].remark;
                ViewBag.patInfo.dispenser = drugs[0].dispenser;
                ViewBag.patInfo.dispenserName = drugs[0].dispenserName;
                ViewBag.patInfo.executor = drugs[0].executor;
                ViewBag.patInfo.executorName = drugs[0].executorName;
                ViewBag.patInfo.sykssj = drugs[0].sykssj;
                ViewBag.patInfo.syjssj = drugs[0].syjssj;
            }
            return View();
        }
    }
}