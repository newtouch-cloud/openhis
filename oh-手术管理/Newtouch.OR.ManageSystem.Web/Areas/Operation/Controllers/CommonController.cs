using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Redis;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    public class CommonController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        // GET: Operation/Common
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Getbqlist(string bqcode)
        {
            var bq = _CommonDmnService.GetBqlist(bqcode, OrganizeId, null);
            return Content(bq.ToJson());
        }

        public ActionResult GetOplist(string ssdm)
        {
            var op = _CommonDmnService.GetOplist(ssdm, OrganizeId);
            return Content(op.ToJson());
        }
        public ActionResult GetOperationAjax(string ssdm, string keyword)
        {
            string[] ssdmData = !string.IsNullOrWhiteSpace(ssdm) ? ssdm.Split(',') : null;
            return GetOperations(ssdmData, keyword);
        }
        public ActionResult GetOperations(string[] ssdmData, string keyword)
        {
            
             var dicCache = RedisHelper.Get<List<OperationDicVO>>(string.Format(CacheKey.OperationDic, OrganizeId));
            if (dicCache == null || dicCache.Count == 0)
            {
                var op = _CommonDmnService.GetOperations(ssdmData, OrganizeId);
                RedisHelper.Set(string.Format(CacheKey.OperationDic, OrganizeId), op);
                return Content(op.ToJson());
            }
            var result = new List<OperationDicVO>();
            if (ssdmData == null || ssdmData.Length == 0 || string.IsNullOrWhiteSpace(string.Join(",", ssdmData)))
            {
                result = dicCache.Take(100).ToList();
            }
            else if (ssdmData.Length == 1)
            {
                result = dicCache.Where(p => p.ssdm == ssdmData[0] || p.ssmc == ssdmData[0] || p.ssdm.Contains(ssdmData[0]) || p.ssmc.Contains(ssdmData[0])).ToList();
            }
            else
            {
                result = dicCache.Where(p => ssdmData.Contains(p.ssdm)).ToList();
            }
            return Content(result.ToJson());
        }

        //public ActionResult GetStafflist(string rygh)
        //{
        //    var ys = _CommonDmnService.GetStafflist(rygh, OrganizeId);
        //    return Content(ys.ToJson());
        //}
        public ActionResult GetStafflist(string Code)
        {
            var ys = _CommonDmnService.GetStafflist(Code, OrganizeId);
            return Content(ys.ToJson());
        }
        public ActionResult GetAneslist(string code)
        {
            var result = _CommonDmnService.GetAneslistlist(code, OrganizeId);
            return Content(result.ToJson());
        }
        public ActionResult GetRoomlist(string Code)
        {
            var result = _CommonDmnService.GetRoomlist(Code, OrganizeId);
            return Content(result.ToJson());
        }
        public ActionResult GetNotchGradelist(string Code)
        {
            var result = _CommonDmnService.GetNotchGradelist(Code, OrganizeId);
            return Content(result.ToJson());
        }
    }
}