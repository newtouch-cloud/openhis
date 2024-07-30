using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtonsoft.Json;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.InpatientVitalSigns;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// 住院患者生命体征
    /// </summary>
    public class InpatientVitalSignsController : OrgControllerBase
    {
        private readonly IInpatientVitalSignsRepo _inpatientVitalSignsRepo;
        private readonly IInpatientPatientDmnService _inpatientPatientDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        public ActionResult NursingInput() {
            ViewBag.MutipatientNursingInputFlag = _sysConfigRepo.GetIntValueByCode("MutipatientNursingInputFlag", OrganizeId, 0);
            return View();
        }
        /// <summary>
        /// 护理查询
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryIndex()
        {
            return View();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string zyh)
        {
            var data = new
            {
                rows = _inpatientVitalSignsRepo.GetPaginationList(pagination, this.OrganizeId, kssj, jssj, zyh),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 护理查询（按日期时间排序）
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetTemperatureChartJson(DateTime? kssj, DateTime? jssj, string zyh)
        {
            if (!kssj.HasValue || !jssj.HasValue)
            {
                throw new FailedException("HLCX_DATE_REQUIRED", "请选择日期");
            }
            if ((jssj.Value.Date - kssj.Value.Date).TotalDays > 30)
            {
                throw new FailedException("HLCX_DATE_OUTOFRANGE", "日期范围不能超过30天");
            }
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("HLCX_ZYH_REQUIRED", "请选择住院号");
            }
            var patInfo = _inpatientPatientDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            var ValidList = _inpatientVitalSignsRepo.GetValidList(this.OrganizeId, kssj, jssj, zyh);
            //var treeList = new List<ValidListVO>();
            //for (int i = 0; i < ValidList.Count(); i++)
            //{
            //    var rqpar = ValidList.Where(p => p.rq == ValidList[i].rq);
            //    var timeslist = new List<times>();
            //    foreach (var item in rqpar)
            //    {
            //        var entity = new times
            //        {
            //            hx = item.hx,
            //            mb = item.mb,
            //            sj = item.sj,
            //            tw = item.tw,
            //            xysz = item.xysz,
            //            xyxz = item.xyxz
            //        };
            //        timeslist.Add(entity);
            //    }
            //    treeList.Add(new ValidListVO { rq = ValidList[i].rq, times = timeslist });
            //}
            //var data = new
            //{
            //    patInfo = patInfo,
            //    list = treeList
            //};

            //20190529回滚至之前的
            var data = new
            {
                patInfo = patInfo,
                list = ValidList
            };

            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _inpatientVitalSignsRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 提交保存护理录入
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(InpatientVitalSignsEntity entity, string keyValue)
        {
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            _inpatientVitalSignsRepo.SubmitForm(entity, keyValue);
            return Success("保存成功。");
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _inpatientVitalSignsRepo.DeleteForm(keyValue);
            return Success("作废成功。");
        }

        /// <summary>
        /// 体温单
        /// </summary>
        /// <returns></returns>
        public ActionResult TemperatureChartIndex()
        {
            return View();
        }

        /// <summary>
        /// 体温单 全屏
        /// </summary>
        /// <returns></returns>
        public ActionResult FullScreenTemperatureChartIndex()
        {
            return View();
        }

        #region 秦皇岛护理录入版本
        public ActionResult GetNursingInputGridJson(string bq,string sj,DateTime rq) {
           var data= _inpatientPatientDmnService.GetInPatSearchList(OrganizeId,bq,rq,sj, ((int)EnumZYBZ.Bqz).ToString());
            return Content(data.ToJson());
        }

        public ActionResult submitmutiple(List<InpatientVitalSignsEntity> entityList,int? sjd,DateTime rq,string flag)
        {
            _inpatientPatientDmnService.submitmutiple(entityList,OrganizeId,sjd,rq,flag);
            return Success();
        }
        #endregion

    }
}