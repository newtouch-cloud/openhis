using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Application.Interface.HospitalizationManage;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.PatientManage.Controllers
{
    public class PatientCenterController : ControllerBase
    {
        private readonly IPatientCenterDmnService _patientCenterDmnService;
        private readonly IDischargeSettleApp _dischargeSettleApp;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        // GET: PatientManage/PatientCenter
        public ActionResult SiteMap()
        {
            return View();
        }
        
        public ActionResult HospFeeInfo()
        {
            return View();
        }
        public ActionResult HospFeeInfoV2()
        {
            return View();
        }
        public ActionResult HospFeeItemShow()
        {
            return View();
        }
        public ActionResult HospFeeItemDetailShow()
        {
            return View();
        }

        public ActionResult HospAccountInfo()
        {
            return View();
        }
        public ActionResult HospMediInsureInfo()
        {
            return View();
        }
        public ActionResult HospSettInfo()
        {
            return View();
        }
        public ActionResult HospDiagInfo()
        {
            return View();
        }

        #region 患者信息
        public ActionResult PatientChange()
        {
            return View();
        }
        public ActionResult PatSearchInfo(Pagination pagination, string keyword, string xm,string zyh,string mzh,string ywlx)
        {
            object json=null;
            var list = _patientCenterDmnService.PatientBasic(zyh, mzh, keyword, null, OrganizeId,ywlx);
            if (list != null && list.zyinfolist != null && list.zyinfolist.Count > 0)
            {
                json = list.zyinfolist;
            }
            else if (list != null && list.mzinfolist != null && list.mzinfolist.Count > 0)
            {
                json = list.mzinfolist;
            }
            else if (list != null && list.basic != null && list.basic.Count > 0)
            {
                json = list.basic;
            }
            
            var data = new
            {
                rows = json,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public ActionResult PatZyInfo(string zyh, string patid)
        {
            var patvo = _patientCenterDmnService.InHospitalHistory(patid, zyh, OrganizeId).FirstOrDefault();
            if (patvo != null && (patvo.zybz == ((int)EnumZYBZ.Bqz).ToString() || patvo.zybz == ((int)EnumZYBZ.Zq).ToString()))
            {
                patvo.zyts = DateTimeHelper.GetInHospDays(Convert.ToDateTime(patvo.rysj), DateTime.Now);
            }
            else if (patvo != null && (patvo.zybz == ((int)EnumZYBZ.Djz).ToString()|| patvo.zybz == ((int)EnumZYBZ.Ycy).ToString()))
            {
                patvo.zyts = DateTimeHelper.GetInHospDays(Convert.ToDateTime(patvo.rysj), Convert.ToDateTime(patvo.cqsj));
            }
            var data = _patientCenterDmnService.InHospitalDoctorInfo(zyh, OrganizeId, ref patvo);
            return Content(data.ToJson());
        }

        public ActionResult GetDefaultZyh(string zyh)
        {
            HospPatientBasicInfoEntity zybrjbxx = new HospPatientBasicInfoEntity();
            if (string.IsNullOrWhiteSpace(zyh))
            {
                zybrjbxx = _hospPatientBasicInfoRepo.FindEntity(p => p.zybz == ((int)EnumZYBZ.Bqz).ToString() && p.zt == "1" && p.OrganizeId == OrganizeId);
            }
            else
            {
                zybrjbxx = _hospPatientBasicInfoRepo.FindEntity(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == OrganizeId);
            }
            //zybrjbxx.zyh = "01309";
            //zybrjbxx.patid = 1027462;
            return Content(zybrjbxx.ToJson());
        }

        public ActionResult PatZySettInfo(string zyh,string jsxz)
        {
            var data = _patientCenterDmnService.InHospitalSett(zyh, jsxz, OrganizeId);
            return Content(data.ToJson());
        }
        public ActionResult PatZySettInfobyJsnm(string jsnm, string settid)
        {
            var data = _patientCenterDmnService.InHospitalSettbyJsnm(jsnm, settid, OrganizeId);
            return Content(data.ToJson());
        }
        #endregion


        #region 费用账单


        public ActionResult GetGoldCollectList(string zyh)
        {

            var resultDto = new InpatientSettPatStatusDetailDto()
            { 
                InpatientSettPatInfo = null,
                ////InpatientSettleItemBO = settleItemsBo
                GroupFeeVOList = _dischargeSettleDmnService.GetHospGroupFeeVOList(zyh, this.OrganizeId, ""),
            };
            // return Success("", resultDto);

            return Content(resultDto.GroupFeeVOList.ToJson());
        }
        /// <summary>
        /// 获取计费表费用
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="group">1 sfdl 2 sfxm 3 sfxm+jfrq 4 无</param>
        /// <param name="sfdl"></param>
        /// <param name="sfxm"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetFeeInfoGridList(string zyh, string group, string sfdl, string sfxm, string orgId, string kssj, string jssj)
        {
            var data = _patientCenterDmnService.GetPatjfbInfo(zyh, group, sfdl, sfxm, OrganizeId,kssj,jssj);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 带模糊查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetFeeInfobySfdl(string zyh, string group, string sfdl, string sfxm, string orgId, string kssj, string jssj,string keyword)
        {
            var dto = _patientCenterDmnService.GetPatjfbInfo(zyh, group, sfdl, sfxm, OrganizeId, kssj, jssj, keyword);
            return Content(dto.ToJson());
        }
        #endregion


        #region 预交金

        public ActionResult GetAdvancePayment(string zyh) {
            var data = _patientCenterDmnService.GetAdvancePayment(zyh, this.OrganizeId);
            return Content(data.ToJson());
        }

        #endregion

        #region 诊断
        public ActionResult GetZdGridList( string bah, string zyh)
        {
            var list = _patientCenterDmnService.GetDiagLsit( OrganizeId, zyh);
            //if (list.Count == 0)
            //{
            //    list = _patientCenterDmnService.GetPatHisZDInfo( zyh, OrganizeId, 2);
            //}
            //var data = new
            //{
            //    rows = list,
            //    total = pagination.total,
            //    page = pagination.page,
            //    records = pagination.records
            //};
            return Content(list.ToJson());
        }


        #endregion

        #region 医保相关
        public ActionResult GetMedInsurPreSettGrid(Pagination page,string zyh)
        {
            var data = _patientCenterDmnService.MedInsurPreSettList(zyh, OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult GetPreSettbyId(string presettid)
        {
            if (string.IsNullOrWhiteSpace(presettid))
            {
                return Content("");
            }
            var data = _patientCenterDmnService.PreSettbyId(presettid);
            return Content(data.ToJson());
        }

        public ActionResult GetMedInsurSettbyId(string settid)
        {
            var data = _patientCenterDmnService.MedInsurSettbyId(settid);
            return Content(data.ToJson());
        }

        #endregion


    }
}