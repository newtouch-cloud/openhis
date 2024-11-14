using System.Collections.Generic;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IDomainServices.Inpatient;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class WardMaintenanceController : OrgControllerBase
    {
        // GET: NurseManage/WardMaintenance
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IWardMaintenanceDmnService _WardMaintenanceDmnService;
        private readonly IPatientRyDiagnosisApp _patientRyDiagnosisApp;
		private readonly IInpatientBedCardRepo _InpatientBedCardRepo;
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly ISysTCMSyndromeRepo _sysTcmsyndromeRepo;
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
        public ActionResult DispensingQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }
        public ActionResult GetDispensingItems(Pagination pagination, string kssj, string jssj)
        {
            var reqdata = new DispensingMXRequestDto
            {
                kssj = kssj,
                jssj = jssj,
                OrganizeId = OrganizeId
            };
            var data = new
            {
                rows = _WardMaintenanceDmnService.GetDispensings(pagination, reqdata),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };

            return Content(data.ToJson());
        }
        public ActionResult GetDispensingItemsMX(Pagination pagination, string bysj)
        {
            var data = new
            {
                rows = _WardMaintenanceDmnService.GetDispensingMXs(pagination,bysj),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
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
        public ActionResult SavaBindItems(List<BedItemsVO> mxList, string bedCode,string zyh)
        {
            var saveItemsDto = new WardMaintenanceRequestDto
            {
                bedCode = bedCode,
                OrganizeId = OrganizeId
            };
            _WardMaintenanceDmnService.SaveBedItems(mxList, saveItemsDto, zyh);
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
        public ActionResult BedBindingCostForm(string bedCode,string zyh)
        {
            ViewBag.bedCode = bedCode;
            ViewBag.zyh = zyh;
            return View();
        }

        /// <summary>
        /// 获取病区费用  
        /// </summary>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        public ActionResult GetBedItems(string bedCode)
        {
            var reqdata = new WardMaintenanceRequestDto
            {
                bedCode = bedCode,
                OrganizeId = OrganizeId
            };
            var xmList = _WardMaintenanceDmnService.GetBedItems(reqdata);

            return Content(xmList.ToJson());
        }

        public ActionResult OutAreaForm(string zyh)
        {
            ViewBag.zyh = zyh;
            ViewBag.isopenPriorReview = _sysConfigRepo.GetValueByCode("OpenPriorReview", OrganizeId);//是否开启事前审核接口
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
		/// 住院床卡
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public ActionResult InHospitalBedCard(string zyh)
		{
			ViewBag.zyh = zyh;
			return View();
		}

		/// <summary>
		/// 住院床卡打印
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public ActionResult InHospitalBedCardPrint(string zyh)
		{
			ViewBag.zyh = zyh;
			return View();
		}

		/// <summary>
		/// 获取病区费用
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public ActionResult GetBedDocs(string zyh)
        {
            var reqdata = new patRequestDto
            {
                zyh = zyh,
                OrganizeId = OrganizeId
            };
            var xmList = _WardMaintenanceDmnService.GetBedDocs(reqdata);

            return Content(xmList.ToJson());
        }

        /// <summary>
        /// 获取出区诊断
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetPatDiagnosis(string zyh)
        {
            var reqdata = new patRequestDto
            {
                zyh = zyh,
                OrganizeId = OrganizeId
            };
            var zdList = _WardMaintenanceDmnService.GetPatDiagnosis(reqdata);

            return Content(zdList.ToJson());
        }

        /// <summary>
        /// 获取入院诊断
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetPatRyDiagnosis(string zyh)
        {
            var result = _patientRyDiagnosisApp.PatientRyDiagnosisQuery(zyh, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// 中西医诊断
        /// </summary>
        /// <returns></returns>
        public ActionResult GetryzdSelect(string ryzd, string ybnhlx,string zdlb=null)
        {
            if (string.IsNullOrEmpty(ybnhlx))
            {
                ybnhlx = null;
            }
            List<ZDSelect> zdlist = new List<ZDSelect>();
            IList<SysDiagnosisVEntity> data = new List<SysDiagnosisVEntity>();
            IList<SysTCMSyndromeVEntity> zyzhdata = new List<SysTCMSyndromeVEntity>();
            if (zdlb == "ZYZH")//中医症候
            {
                zyzhdata = _sysTcmsyndromeRepo.GetList(this.OrganizeId, ryzd);
            }
            else {
                data = _sysDiagnosisRepo.GetList(this.OrganizeId, ryzd, zdlb, ybnhlx);
            }
            foreach (SysTCMSyndromeVEntity item in zyzhdata)
            {
                ZDSelect a = new ZDSelect();
                a.zdbh = item.zhCode;
                a.icd10 = item.zhCode == null ? "" : item.zhCode;
                a.zdmc = item.zhmc;
                a.py = item.py;
                a.zdnm = item.zhId;
                zdlist.Add(a);
            }
            foreach (SysDiagnosisVEntity item in data)
            {
                ZDSelect a = new ZDSelect();
                a.zdbh = item.zdCode;
                a.icd10 = item.icd10 == null ? "" : item.icd10;
                a.zdmc = item.zdmc;
                a.py = item.py;
                a.zdnm = item.zdId;
                zdlist.Add(a);
            }
            return Content(zdlist.ToJson());
        }
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SavaBindDocs(List<BedDocsVO> mxList, string zyh)
        {
            var saveDocsDto = new patRequestDto
            {
                zyh = zyh,
                OrganizeId = OrganizeId
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
            var saveDiagnosisDto = new patBedFeeRequestDto
            {
                zyh = patOutAreaVO.zyh,
                OrganizeId = OrganizeId,
                rq = patOutAreaVO.cqsj,
                user=UserIdentity.rygh
            };
            var isOutAreaMsg = _WardMaintenanceDmnService.GetPatIsOutArea(saveDiagnosisDto);
            if (isOutAreaMsg.Split('|')[0] == "F")
            {
                return Error(isOutAreaMsg.Split('|')[1]);
            }
            var patOutMsg = _WardMaintenanceDmnService.SavaPatDiagnosis(patOutAreaVO, saveDiagnosisDto);
            return patOutMsg.Split('|')[0] == "T" ? Success("保存成功。") : Error(patOutMsg.Split('|')[1]);
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
        public ActionResult InAreaDiagnoosisForm(string zyh,string brxz)
        {
            ViewBag.brxz = brxz;
            ViewBag.zyh = zyh;
            return View();
        }

        /// <summary>
        /// 出区诊断--患者出区
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult OutAreaDiagnoosisForm(string zyh, string brxz)
        {
            ViewBag.brxz = brxz;
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
            var saveDiagnosisDto = new patBedFeeRequestDto
            {
                zyh = patOutAreaVO.zyh,
                OrganizeId = this.OrganizeId,
                //rq = patOutAreaVO.cqsj,
                rq = patOutAreaVO.zdrq
            };
            var patOutMsg = _WardMaintenanceDmnService.SavaOutPatDiagnosis(patOutAreaVO, saveDiagnosisDto);
            return patOutMsg.Split('|')[0] == "T" ? Success("保存成功。") : Error(patOutMsg.Split('|')[1]);
        }

        /// <summary>
        /// 保存入区诊断
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <returns></returns>
        public ActionResult SavaInPatDiagnosis(PatInAreaVO patInAreaVo)
        {
            var result = _patientRyDiagnosisApp.SavaInPatDiagnosis(patInAreaVo, OrganizeId, UserIdentity.UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

		/// <summary>
		/// 保存出区诊断List
		/// </summary>
		/// <param name="patOutAreaVO"></param>
		/// <returns></returns>
		public ActionResult SavaPatDxList(List<InpatientPatientDiagnosisEntity> PatDxList)
		{
			//var patOutMsg="";
			//foreach (var PatDxEntity in PatDxList) {

			//	patOutMsg=_WardMaintenanceDmnService.SavaPatDxEntity(PatDxEntity);
			//}
			var OrganizeId = this.OrganizeId;
			var patOutMsg = _WardMaintenanceDmnService.SavaPatDxList(PatDxList, this.OrganizeId);
			return patOutMsg.Split('|')[0] == "T" ? Success("保存成功。") : Error(patOutMsg.Split('|')[1]);
		}


		/// <summary>
		/// 保存住院床卡
		/// </summary>
		//[HttpPost]
		//[HandlerAjaxOnly]
		public ActionResult SavaBedCard(InpatientBedCardEntity entity, string zyh)
		{
			entity.OrganizeId = OrganizeId;
			entity.zyh = zyh;
			_InpatientBedCardRepo.SubmitForm(entity, zyh);
			return Success("保存成功。");
		}

	}

}