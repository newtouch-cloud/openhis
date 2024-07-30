using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Tools;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.DomainServices.Inpatient;
using Newtouch.Domain.IDomainServices.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Common.Web;
using Newtouch.Domain.DTO.InputDto;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class WardMaintenanceController : OrgControllerBase
    {
        // GET: NurseManage/WardMaintenance
        private readonly IWardMaintenanceDmnService _WardMaintenanceDmnService;
        public ActionResult WardWardRoomBedMaintenance()
        {
            return View();
        }

        /// <summary>
        /// 房间维护
        /// </summary>
        /// <returns></returns>
        public ActionResult RoomForm()
        {
            return View();
        }

        /// <summary>
        /// 附加属性
        /// </summary>
        /// <returns></returns>
        public ActionResult AttributesPlusBedForm()
        {
            return View();
        }

        /// <summary>
        /// Room Form Submit
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitRoomForm(string keyValue)
        {
            return null;
        }
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SavaBindItems(List<BedItemsVO> mxList, string bedCode)
        {
            WardMaintenanceRequestDto saveItemsDto = new WardMaintenanceRequestDto() {
                bedCode = bedCode,
                OrganizeId = this.OrganizeId
            };
            _WardMaintenanceDmnService.SaveBedItems(mxList, saveItemsDto);
            return Success("保存成功。");
        }

        /// <summary>
        /// delete room
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteRoomForm(string keyValue)
        {
            return null;
        }

        public ActionResult BedForm(string bedNo)
        {
            return View();
        }

        /// <summary>
        /// delete bed
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteBedForm(string keyValue)
        {
            return null;
        }

        /// <summary>
        /// 床位费绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult BedBindingCostForm(string bedCode)
        {
            ViewBag.bedCode = bedCode;
            return View();
        }

        /// <summary>
        /// 获取病区费用  
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetBedItems(string bedCode)
        {
            WardMaintenanceRequestDto reqdata = new WardMaintenanceRequestDto
            {
                bedCode = bedCode,
                OrganizeId = this.OrganizeId
            };
            List<BedItemsVO> xmList = _WardMaintenanceDmnService.GetBedItems(reqdata);

            return Content(xmList.ToJson());
        }

        public ActionResult OutAreaForm(string zyh)
        {
            ViewBag.zyh = zyh;
            return View();
        }

        /// <summary>
        /// 医生绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult DoctorBindingForm(string zyh)
        {
            ViewBag.zyh = zyh;
            return View();
        }

        /// <summary>
        /// 获取病区费用
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetBedDocs(string zyh)
        {
            patRequestDto reqdata = new patRequestDto
            {
                zyh = zyh,
                OrganizeId = this.OrganizeId
            };
            List<BedDocsVO> xmList = _WardMaintenanceDmnService.GetBedDocs(reqdata);

            return Content(xmList.ToJson());
        }

        public ActionResult GetPatDiagnosis(string zyh)
        {
            patRequestDto reqdata = new patRequestDto
            {
                zyh = zyh,
                OrganizeId = this.OrganizeId
            };
            List<PatDiagnosisVO> zdList = _WardMaintenanceDmnService.GetPatDiagnosis(reqdata);

            return Content(zdList.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SavaBindDocs(List<BedDocsVO> mxList, string zyh)
        {
            patRequestDto saveDocsDto = new patRequestDto()
            {
                zyh = zyh,
                OrganizeId = this.OrganizeId
            };
            _WardMaintenanceDmnService.SaveBedDocs(mxList, saveDocsDto);
            return Success("保存成功。");
        }
        /// <summary>
        /// 患者出区
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <returns></returns>
        public ActionResult SavaPatDiagnosis(PatOutAreaInfoVO patOutAreaVO)
        {
            patBedFeeRequestDto saveDiagnosisDto = new patBedFeeRequestDto()
            {
                zyh = patOutAreaVO.zyh,
                OrganizeId = this.OrganizeId,
                rq = patOutAreaVO.cqsj
            };
            string isOutAreaMsg = _WardMaintenanceDmnService.GetPatIsOutArea(saveDiagnosisDto);
            if (isOutAreaMsg.Split('|')[0].ToString() == "F")
            {
                return Error(isOutAreaMsg.Split('|')[1].ToString());
            }
            string patOutMsg = _WardMaintenanceDmnService.SavaPatDiagnosis(patOutAreaVO, saveDiagnosisDto);
            if (patOutMsg.Split('|')[0].ToString() == "T")
            {
                return Success("保存成功。");
            }
            else
            {
                return Error(patOutMsg.Split('|')[1].ToString());
            }
        }

        public ActionResult AttributesPlusRoomForm(string keyWord)
        {
            return View();
        }
        /// <summary>
        /// 出区诊断
        /// </summary>
        /// <returns></returns>
        public ActionResult OutAreaDiagnoosis()
        {
            return View();
        }
        /// <summary>
        /// 出区诊断--患者出区
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult OutAreaDiagnoosisForm(string zyh)
        {
            ViewBag.zyh = zyh;
            return View();
        }
        /// <summary>
        /// 保存 出区诊断--患者出区
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <returns></returns>
        public ActionResult SavaOutPatDiagnosis(PatOutAreaVO patOutAreaVO)
        {
            patBedFeeRequestDto saveDiagnosisDto = new patBedFeeRequestDto()
            {
                zyh = patOutAreaVO.zyh,
                OrganizeId = this.OrganizeId,
                //rq = patOutAreaVO.cqsj,
                rq = patOutAreaVO.zdrq
            };
            string patOutMsg = _WardMaintenanceDmnService.SavaOutPatDiagnosis(patOutAreaVO, saveDiagnosisDto);
            if (patOutMsg.Split('|')[0].ToString() == "T")
            {
                return Success("保存成功。");
            }
            else
            {
                return Error(patOutMsg.Split('|')[1].ToString());
            }
        }

    }

}