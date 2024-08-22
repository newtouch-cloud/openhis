using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.TherapeutistManage.Controllers
{
    public class TherapistAdviceController : ControllerBase
    {
        private readonly ITherapistSuggestionDmnService _TherapistSuggestionDmnService;

        // GET: TherapeutistManage/TherapistAdvice
        //public ActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveData( List<TherapistSuggestionEntity> mxList,string delRowIds, string blh,string brlx,string mzzyh)
        {
            if (mxList!=null &&mxList.Count>0)
            {
                foreach (var item in mxList)
                {
                    item.blh = blh;
                    item.brlx = brlx;
                    item.mzzyh = mzzyh;
                }
            }
            var delIds = Newtouch.Tools.Json.ToList<string>(delRowIds);
            string orgId = this.OrganizeId;

            _TherapistSuggestionDmnService.SaveData(mxList, delIds, orgId);
            return Success();
        }

        public ActionResult GetPatZLJYInfo(string mzzyh,string brlx)
        {
            var resultDto = new TherapistSuggestionDto()
            {
                MzZyPatInfoDto=null,
                TherapistAdviceDto = null
            };
            var patInfoList = _TherapistSuggestionDmnService.GetMzZyPatInfo(mzzyh, this.OrganizeId, brlx);
            if (patInfoList !=null && patInfoList.Count>0)
            {
                resultDto.MzZyPatInfoDto = patInfoList.FirstOrDefault();
                resultDto.TherapistAdviceDto= _TherapistSuggestionDmnService.GetZLJYList(mzzyh, this.OrganizeId, brlx);
                return Success("提交成功", resultDto);
            }
            return Error("查询失败");
        }
        
    }
}