using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.PrintDto;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian;
using Newtouch.HIS.Proxy.guian.DTO.S03;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Domain.IRepository.PatientManage;

namespace Newtouch.HIS.Web.Areas.PatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HospiterResController : ControllerBase
    {
        private readonly ISysPatBasicInfoApp _SysPatBasicInfoApp;
        private readonly ISysCardApp _SysCardApp;
        private readonly ISysCardRepo _sysCardRepository;
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        private readonly ISysPatAccApp _SysPatAccApp;
        private readonly IAccountManageApp _AccountManageApp;
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOutPatChargeApp _outPatChargeApp;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly IRefundDmnService _refundDmnService;
        private readonly IHospPatientBasicInfoRepo _hosPatBasicInfoRepository;
        private readonly IGuianRybl21OutInfoRepo _guianRybl21OutInfoRepo;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IBaseGaXnhS30Repo _daseGaXnhS30Repo;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        private readonly ICommonDmnService _commonDmnService;
        private readonly ISysPatientNatureRepo _SysPatiNatureRepo;
        private readonly IG_yb_daysrg_trt_list_bRepo _iG_yb_daysrg_trt_list_bRepo;
        #region 新农合 

        private readonly IGuianXnhS04InfoRepo _iGuianXnhS04InfoRepo;
        #endregion
        #region 页面初始化

        public override ActionResult Index()
        {
            ViewBag.IsEditPatientMedicalRecord = _sysConfigRepo.GetBoolValueByCode("PatientMedicalRecord_able", this.OrganizeId, false);
            return base.Index();
        }
        public ActionResult Register()
        {

            ViewBag.zyhpz = _sysConfigRepo.GetBoolValueByCode("xt_zyh", OrganizeId, true).Value ? "ON" : "OFF";
            //是否用卡号搜索，默认OFF，即默认要用病历号搜索
            ViewBag.searchpz = _sysConfigRepo.GetBoolValueByCode("register_search", OrganizeId, false).Value ? "ON" : "OFF";
            ViewBag.isDiagnosisRequired = _sysConfigRepo.GetBoolValueByCode("Inpatient_Registe_Diagnosis_Required", this.OrganizeId);
            ViewBag.CurUserCode = UserIdentity.UserCode;
            ViewBag.CurUserName = UserIdentity.UserName;
            return View();
        }

        public ActionResult MZRegister()
        {
            return View();
        }

        public ActionResult PatientBasic(string keyValue, string mzh = null)
        {
            if (string.IsNullOrWhiteSpace(keyValue) && !string.IsNullOrWhiteSpace(mzh))
            {
                var orgId = this.OrganizeId;
                var patid = _outpatientRegistRepo.IQueryable(p => p.mzh == mzh && p.OrganizeId == orgId).Select(p => p.patid).FirstOrDefault();
                if (patid > 0)
                {
                    var url = Request.RawUrl;
                    url = url.Replace("mzh=" + mzh, "keyValue=" + patid);
                    return Redirect(url);
                }
                else
                {
                    return Content("缺少参数");
                }
            }
            ViewBag.blhpz = _sysConfigRepo.GetBoolValueByCode("xt_blh", OrganizeId, false).Value ? "ON" : "OFF";
            ViewBag.zjpz = _sysConfigRepo.GetBoolValueByCode("xt_zj", OrganizeId, true).Value ? "ON" : "OFF";
            ViewBag.khpz = _sysConfigRepo.GetBoolValueByCode("xt_kh", OrganizeId, true).Value ? "ON" : "OFF";
            return View();
        }

        public ActionResult inpatientRegister()
        {
            ViewBag.mzhpz = _sysConfigRepo.GetBoolValueByCode("xt_mzh", OrganizeId, true).Value ? "ON" : "OFF";
            ViewBag.IsDefaultDepart = _sysConfigRepo.GetByCode("IsDefaultDepart", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("IsDefaultDepart", OrganizeId).Value;
            ViewBag.IsDefaultDoc = _sysConfigRepo.GetByCode("IsDefaultDoc", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("IsDefaultDoc", OrganizeId).Value;
            ViewBag.IsOpenSfqr = _sysConfigRepo.GetValueByCode("xt_isopen_sfqr", OrganizeId) == "ON";
            //门诊登记完成后，跳转页面配置
            ViewBag.TZPZ_OutpatientRegister = _sysConfigRepo.GetByCode("TZPZ_OutpatientRegister", OrganizeId) == null ? "" : _sysConfigRepo.GetByCode("TZPZ_OutpatientRegister", OrganizeId).Value;
            return View();
        }

        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelRegister()
        {
            return View();
        }
        /// <summary>
        /// 医保病人基本信息、账户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult YbPatientBasicInfo()
        {
            return View();
        }
        
        /// <summary>
        /// 医保目录下载 查询 字典
        /// </summary>
        /// <returns></returns>
        public ActionResult MedicalInsuranceCatalogue()
        {
            return View();
        }

        //加载数据
        public ActionResult CatalogueData(Pagination pagination, string tbname, string key)
        {
            var datapan = new
            {
                rows = _iG_yb_daysrg_trt_list_bRepo.Get_G_yb_mluCommon_Info(pagination, tbname, key),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(datapan.ToJson());
        }

        public ActionResult GetVer(string tbname)
        {

            switch (tbname)
            {
                case "1301":
                    tbname = "G_yb_wm_tcmpat_info_b";
                    break;
                case "1302":
                    tbname = "G_yb_tcmherb_info_b";
                    break;
                case "1303":
                    tbname = "G_yb_selfprep_info_b";
                    break;
                case "1305":
                    tbname = "G_yb_trt_serv_b";
                    break;
                case "1306":
                    tbname = "G_yb_mcs_info_b";
                    break;
                case "1307":
                    tbname = "G_yb_diag_list_b";
                    break;
                case "1308":
                    tbname = "G_yb_oprn_std_b";
                    break;
                case "1309":
                    tbname = "G_yb_opsp_dise_list_b";
                    break;
                case "1310":
                    tbname = "G_yb_dise_setl_list_b";
                    break;
                case "1311":
                    tbname = "G_yb_daysrg_trt_list_b";
                    break;
                case "1313":
                    tbname = "G_yb_tmor_mpy_b";
                    break;
                case "1314":
                    tbname = "G_yb_tcm_diag_b";
                    break;
                case "1315":
                    tbname = "G_yb_tcmsymp_type_b";
                    break;
                case "1320":
                    tbname = "G_yb_zypfklmu_list_b";
                    break;
                case "1321":
                    tbname = "G_yb_ylfuxm_new_list_b";
                    break;
                default:
                    break;
            }
            var data = _iG_yb_daysrg_trt_list_bRepo.Header(tbname.Trim()) ?? "0000";
            return Content(data == "null" ? "0000" : data);
        }

        public ActionResult YibaoInterfaceCancel()
        {
            return View();
        }
        #endregion

        #region 病人管理
        /// <summary>
        /// 病人管理查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="xlbrbz"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination,string zjh)
        {
            var data = new
            {
                rows = _PatientBasicInfoDmnService.GetList(pagination, this.OrganizeId,zjh),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion

        #region 医保异常交易
        public ActionResult GetCqybGridJson(Pagination pagination, string isjs, string lx,string keyword)
        {
            var data = new
            {
                rows = _PatientBasicInfoDmnService.GetYbCancelList(pagination, isjs, lx, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        #endregion

        #region 一卡通日志
        public ActionResult ModifyLog()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetModifyLog(Pagination pagination, string xm)
        {

            var data = new
            {
                rows = _sysPatientBasicInfoRepo.GetModifyLog(pagination, this.OrganizeId, xm),
                pagination.total,
                pagination.page,
                pagination.records
            };

            return Content(data.ToJson());
        }

        public ActionResult LogDetailsView()
        {

            return View();
        }

        public ActionResult GetDetailsData(string Id)
        {
            var data = _sysPatientBasicInfoRepo.GetDetailsData(Id, this.OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult CardManagement()
        {
            ViewBag.khpz = _sysConfigRepo.GetBoolValueByCode("xt_kh", OrganizeId, true).Value ? "ON" : "OFF";
            return View();
        }

        /// <summary>
        /// 卡管理加载数据
        /// </summary>
        /// <param name="patId">patId</param>
        /// <returns></returns>
        public ActionResult GetCardNoInfo(string patId)
        {
            var data = _sysPatientBasicInfoRepo.GetCardNoInfo(patId, this.OrganizeId, "");

            return Content(data.ToJson());
        }

        public ActionResult SubmitCard(SysHosBasicInfoVO PatientBasicAndCardInfoVO)
        {
            var data = _sysPatientBasicInfoRepo.GetCardNoInfo(PatientBasicAndCardInfoVO.patid.ToString(), this.OrganizeId, PatientBasicAndCardInfoVO.cardtype).Where(p=>p.CardType!=((int)EnumCardType.XNK).ToString()).ToList();
            SysCardEntity newCardEntity = null;
            newCardEntity = _sysCardRepository.GetCardEntity(PatientBasicAndCardInfoVO.cardtype, PatientBasicAndCardInfoVO.kh, this.OrganizeId);
            if (data.Count > 0)
            {
                return Success("相同类型不能添加多张卡");
            }
            //验卡号重复使用
            if (newCardEntity != null)
            {
                throw new FailedException("卡号已存在");
            }
            else
            {
                _sysPatientBasicInfoRepo.SubmitCard(PatientBasicAndCardInfoVO, this.OrganizeId);
                return Success("操作成功");
            }
        }

        public ActionResult CardVoids(string CardId)
        {

            _sysPatientBasicInfoRepo.CardVoids(CardId, this.OrganizeId, UserIdentity.UserCode);
            return Success("作废成功");
        }


        #endregion

        #region 住院登记
        //public ActionResult GetWardbyDept(string ks,string keyword)
        //{
        //    var data = _commonDmnService.GetWardbyDept(OrganizeId, ks, keyword);
        //    return Content(data.ToJson());
        //}

        /// <summary>
        /// 新建登记 保存卡号和病人基本信息
        /// </summary>
        /// <param name="PatientBasicAndCardInfoVO"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysHosBasicInfoVO PatientBasicAndCardInfoVO)
        {
            _PatientBasicInfoDmnService.SavePatBasicCardInfo(PatientBasicAndCardInfoVO, this.OrganizeId, UserIdentity.UserCode);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue,string zjh)
        {
            var _vo = _PatientBasicInfoDmnService.GetSyspatientFormJson(keyValue, OrganizeId,zjh);//GetPatientBasicByPatid(keyValue, this.OrganizeId);
            var data = _vo.ToJson();
            return Content(data);
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetryzdSelect(string ryzd,string ybnhlx)
        {
            if (string.IsNullOrEmpty(ybnhlx))
            {
                ybnhlx = null;
            }
            var data = _sysDiagnosisRepo.GetList(this.OrganizeId, ryzd, "WM", ybnhlx);
            var zydata = _sysDiagnosisRepo.GetList(this.OrganizeId, ryzd, "TCM", ybnhlx);
            List<ZDSelect> zdlist = new List<ZDSelect>();
            foreach (SysDiagnosisVEntity item in zydata)
            {
                ZDSelect a = new ZDSelect();
                a.zdbh = item.zdCode;
                a.icd10 = item.icd10 == null ? "" : item.icd10;
                a.zdmc = item.zdmc;
                a.py = item.py;
                a.zdnm = item.zdId;
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
        /// 检验自费卡是否正常
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zjh"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult CheckCardState(string kh, string zyh)
        {
            var blh = "";
            //是否用卡号搜索，默认OFF，即默认要用病历号搜索
            var searchpz = _sysConfigRepo.GetBoolValueByCode("register_search", OrganizeId, false).Value ? "ON" : "OFF";
            if (searchpz == "ON" && !string.IsNullOrWhiteSpace(kh))
            {
                //var patid = _SysCardApp.GetPatidByCardNo(kh);
                //blh = _hospPatientBasicInfoRepo.IQueryable().FirstOrDefault(p => p.kh == kh).blh;
                var patid = _SysCardApp.GetPatidByCardNo(kh);
                if (!string.IsNullOrWhiteSpace(patid))
                {
                    var intPatid = Convert.ToInt32(patid);
                    blh = _sysPatientBasicInfoRepo.IQueryable().FirstOrDefault(p => p.patid == intPatid).blh;
                }
            }
            else if (!string.IsNullOrWhiteSpace(zyh))
            {
                blh = _hospPatientBasicInfoRepo.GetBlhByZyh(zyh, OrganizeId);
            }
            //var result = false;
            //var _vo = _PatientBasicInfoDmnService.GetPatBasicCardInfo(patid, out result, OrganizeId).FirstOrDefault();
            //var data = _vo.ToJson();
            return Success(null, blh);
        }

        /// <summary>
        /// 获取最新卡号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNewCardNoAndBLH(bool khsc, bool blhsc)
        {
            var vo = new SysHosBasicInfoVO();
            var khpz = _sysConfigRepo.GetBoolValueByCode("xt_kh", OrganizeId, true).Value ? "ON" : "OFF";
            if (khpz == "ON" && khsc)
            {
                vo.kh = _SysCardApp.GetCardSerialNo();
            }
            var blhpz = _sysConfigRepo.GetBoolValueByCode("xt_blh", OrganizeId, false).Value ? "ON" : "OFF";
            if (blhpz == "ON" && blhsc)
            {
                vo.blh = _SysPatBasicInfoApp.Getblh();
            }
            return Content(vo.ToJson());
        }

        /// <summary>
        /// 住院登记点击保存时操作
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ActionResult SaveSysHosBasicInfo(SysHosBasicInfoVO vo,bool isybjy)
        {
            var yssfz = new CqybGjbmInfoVo();
            string res;
            string zyh = _PatientBasicInfoDmnService.SaveSysBasicAccountInfo(vo, OrganizeId, out res);
            if (isybjy)
            {
                yssfz = _outPatientSettleDmnService.GetDepartmentDoctorIdC(OrganizeId, vo.ks, vo.doctor);
            }
            //更新报警额
            if (!string.IsNullOrWhiteSpace(vo.bje.ToString())) {
                _PatientBasicInfoDmnService.updateZybrxxkExpandBje(zyh, OrganizeId,  vo.bje);
            }
            var data = new
            {
                zyh = zyh,
                yszfz= yssfz,
            };
            return Success(res, data);
        }

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult CancelAdmission(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("没有住院信息，不能取消入院！");
            }
            if (_PatientBasicInfoDmnService.CheckHasFee(zyh, this.OrganizeId))
            {
                throw new FailedException("有未结算的住院费用记录，不能取消入院。");
            }
            if (_PatientBasicInfoDmnService.BoolRuQu(zyh, this.OrganizeId))
            {
                throw new FailedException("该病人已在病区中，不能取消入院！");
            }
            _PatientBasicInfoDmnService.CancelAdmission(zyh, this.OrganizeId);
            return Success("取消入院成功！");
        }
	    public ActionResult CheckCancelAdmission(string zyh)
	    {
		    if (string.IsNullOrWhiteSpace(zyh))
		    {
			    throw new FailedException("没有住院信息，不能取消入院！");
		    }
		    if (_PatientBasicInfoDmnService.CheckHasFee(zyh, this.OrganizeId))
		    {
			    throw new FailedException("有未结算的住院费用记录，不能取消入院。");
		    }
            if (_PatientBasicInfoDmnService.BoolRuQu(zyh, this.OrganizeId))
            {
                throw new FailedException("该病人已在病区中，不能取消入院！");
            }
            return Success("取消入院成功！");
	    }
		/// <summary>
		/// 根据patid获取住院信息
		/// </summary>
		/// <param name="patid"></param>
		/// <returns></returns>
		public ActionResult GetinpatientInfo(string patid)
        {
            bool result;
            var _vo = _PatientBasicInfoDmnService.GetPatBasicCardInfo(patid, out result, OrganizeId)[0];
            if (!string.IsNullOrWhiteSpace(_vo.zyh))
            {
              var S04entity=  _iGuianXnhS04InfoRepo.FindEntity(p=>p.zyh==_vo.zyh&&p.OrganizeId==OrganizeId);
                if (S04entity!=null)
                {
                    _vo.inpId = S04entity.inpId;
                }
            }
            var data = _vo.ToJson();
            return Content(data);
        }
        #endregion

        #region 打印

        /// <summary>
        /// 住院基本信息打印
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        public ActionResult PrinyZYInfo(ZYInfoVO print)
        {
            if (print.patid == null)
            {
                return Error("病人基本信息不全，无法打印住院信息");
            }
            var xtInfo = _SysPatBasicInfoApp.GetInfoByPatid(print.patid);
            print.phone = xtInfo.phone;
            print.webchat = xtInfo.wechat;
            print.email = xtInfo.email;
            switch (xtInfo.zjlxfs)
            {
                case "1":
                    xtInfo.zjlxfs = "电话";
                    break;
                case "2":
                    xtInfo.zjlxfs = "手机";
                    break;
                case "3":
                    xtInfo.zjlxfs = "微信";
                    break;
                case "4":
                    xtInfo.zjlxfs = "邮箱";
                    break;
                default:
                    break;
            }
            switch (print.hy)
            {
                case "0":
                    print.hy = "未婚";
                    break;
                case "1":
                    print.hy = "已婚";
                    break;

            }
            switch (print.lxrgx)
            {
                case "0":
                    print.lxrgx = "其他";
                    break;
                case "1":
                    print.lxrgx = "夫妻";
                    break;
                case "2":
                    print.lxrgx = "父子";
                    break;
                case "3":
                    print.lxrgx = "母子";
                    break;
                case "4":
                    print.lxrgx = "父女";
                    break;
                case "5":
                    print.lxrgx = "兄弟";
                    break;
                case "6":
                    print.lxrgx = "姐弟";
                    break;
            }
            switch (print.lxrgx2)
            {
                case "0":
                    print.lxrgx2 = "其他";
                    break;
                case "1":
                    print.lxrgx2 = "夫妻";
                    break;
                case "2":
                    print.lxrgx2 = "父子";
                    break;
                case "3":
                    print.lxrgx2 = "母子";
                    break;
                case "4":
                    print.lxrgx2 = "父女";
                    break;
                case "5":
                    print.lxrgx2 = "兄弟";
                    break;
                case "6":
                    print.lxrgx2 = "姐弟";
                    break;
            }
            _SysPatBasicInfoApp.PrinZYInfo(print);
            return Success("打印成功！");
        }

        /// <summary>
        /// 打印腕带
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        public ActionResult PrintWDInfo(WDInfoVO print)
        {

            if (string.IsNullOrWhiteSpace(print.patid) || print == null)
            {
                return Error("病人基本信息不全，无法打印腕带");
            }
            print.Flag = "1";
            _SysPatBasicInfoApp.PrintWDInfo(print);
            return Success("打印成功！");
        }
        #endregion

        #region 门诊登记
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult InpatientSubmit(OutpatientRegistEntity OutpatientRegistEntity, int? keyValue)
        {
            OutpatientRegistEntity.ghf = 0m;
            OutpatientRegistEntity.zlf = 0m;
            OutpatientRegistEntity.ckf = 0m;
            OutpatientRegistEntity.gbf = 0m;

            string mzh;
            var mzhpz = _sysConfigRepo.GetBoolValueByCode("xt_mzh", OrganizeId, true).Value ? "ON" : "OFF";

            var res = _outpatientRegistRepo.SubmitForm(OutpatientRegistEntity, keyValue, mzhpz, OrganizeId, this.UserIdentity.UserCode, out mzh);
            _sysPatientBasicInfoRepo.UpdatelxrInfo(OutpatientRegistEntity.patid.ToString(), OrganizeId, OutpatientRegistEntity.lxr, OutpatientRegistEntity.lxrgx, OutpatientRegistEntity.lxrdh, UserIdentity.UserCode);
            return Success(res, mzh);
        }

        public ActionResult Updatebrjbxx(string kh, string zjh, string cbdbm, string cblb, string grbh, string xzlx)
        {
            _sysPatientBasicInfoRepo.Updateybxx(kh,zjh,cbdbm,cblb,grbh,xzlx,OrganizeId, UserIdentity.UserCode);
            return Success();
        }

        public ActionResult UpdateZt(string patId,string isqy)
        {
            _sysPatientBasicInfoRepo.UpdateZt(patId, OrganizeId, isqy, UserIdentity.UserCode);
            return Success();
        }

        /// <summary>
        /// 病人查询浮层
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult PatSearchInfo(string keyword, string orgId)
        {
            var OrganizeId = orgId;
            if (string.IsNullOrWhiteSpace(orgId))
            {
                OrganizeId = this.OrganizeId;
            }
            var list = _refundDmnService.GetBasicInfoSearchListInRegister(keyword, OrganizeId);
            return Content(list.ToJson());
        }
        /// <summary>
        /// 门诊住院预交金患者浮层
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult PatYjjSearchInfo(string keyword, string orgId, string type)
        {
            var OrganizeId = orgId;
            if (string.IsNullOrWhiteSpace(orgId))
            {
                OrganizeId = this.OrganizeId;
            }
            var list = _refundDmnService.GetZyMzYjjPatientSearch(keyword, OrganizeId,type);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 判断是否重复挂号
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public ActionResult AllowRegh(string blh)
        {
            var result = _outpatientRegistRepo.AllowRegh(blh, OrganizeId);
            return Content(result.ToJson());
        }
        #endregion
        public ActionResult Getjzjsxx(string type,string keyword, string grbh)
        {
            var list = _refundDmnService.Getjzjsxx(type,keyword,grbh,this.OrganizeId);
            return Content(list.ToJson());

        }
        /// <summary>
        /// 根据病历号 或 卡号 查询病人基本信息
        /// </summary>
        /// <param name="blh">病历号</param>
        /// <param name="kh">卡号</param>
        /// <param name="zjh"></param>
        /// <param name="cardType">卡类型</param>
        /// <returns></returns>
        public ActionResult GetOutpatientBasicInfo(string blh, string kh, string zjh, string cardType, string CardId, string ly=null)
        {
            var patChargeInfo = _outPatChargeApp.GetOutPatBasicInfoInRegister(blh, kh, zjh, cardType, ly,CardId);
            return Success("提交成功", patChargeInfo);
        }

        public ActionResult valiPatZjh(string zjh)
        {
            //var patChargeInfo = _outPatChargeApp.GetOutPatBasicInfoInRegister(blh, kh, zjh, cardType, ly, CardId);
            //return Success("提交成功", patChargeInfo);
            return null;
        }

        #region 贵安医保数据

        public ActionResult GetGuianRyblOutInfoByZyh(string zyh)
        {
            var _vo = _guianRybl21OutInfoRepo.GetInfoByZyh(zyh, OrganizeId);
            var data = _vo.ToJson();
            return Content(data);
        }

        public ActionResult SaveGuianRyblOutInfo(GuianRybl21OutInfoEntity entity)
        {
            entity.OrganizeId = OrganizeId;
            if (_guianRybl21OutInfoRepo.FindEntity(p =>
                    p.OrganizeId == entity.OrganizeId && p.prm_ykc010 == entity.prm_ykc010) != null)
                return Success(_guianRybl21OutInfoRepo.Update(entity) > 0 ? "1" : "0");
            else
                return Success(_guianRybl21OutInfoRepo.Insert(entity) > 0 ? "1" : "0");
        }

        #endregion 贵安医保数据

        #region 贵安新农合

        public ActionResult XNHParticipantsList()
        {
            return View();
        }

        public ActionResult XNHfamilyrequestParForm()
        {
            return View();
        }

        public ActionResult S03submit(S03RequestDTO request)
        {
            var response = CommonProxy.GetInstance(OrganizeId).S03(request);
            return Success("", response.ToJson());
        }

        /// <summary>
        /// 根据身份证号判断是否初诊
        /// </summary>
        /// <param name="sfzh"></param>
        /// <returns></returns>
        public ActionResult ValidateFirstVisit(string sfzh, string xm)
        {
            var entity = _sysPatientBasicInfoRepo.FindEntity(p => p.zjlx == ((int)EnumZJLX.sfz).ToString()
                                                                  && p.zjh == sfzh
                                                                  && p.xm == xm
                                                                  && p.OrganizeId == OrganizeId
                                                                  && p.zt == "1" );
            return Success("", entity);
        }

        /// <summary>
        /// 根据个人编码获取患者基本信息
        /// </summary>
        /// <param name="xnhgrbm">新农合个人编码</param>
        /// <returns></returns>
        //public ActionResult GetPatientBasicInfo(string xnhgrbm)
        //{
        //    var entity = _sysPatientBasicInfoRepo.FindEntity(p => p.xnhgrbm == xnhgrbm
        //                                                          && p.OrganizeId == OrganizeId
        //                                                          && p.zt == "1");
        //    return Success("", entity);
        //}

        /// <summary>
        /// 获取卡号信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="brxz"></param>
        /// <returns></returns>
        public ActionResult GetkhInfoBybrxz(int patid, string brxz)
        {
            var cardtype = "";
            if (string.IsNullOrWhiteSpace(brxz))
            {
                throw new FailedException("缺少病人性质");
            }
            if (patid == 0)
            {
                throw new FailedException("缺少病人编码");
            }
            switch (brxz)
            {
                //医保
                case "1":
                    cardtype = ((int)EnumCardType.YBJYK).ToString();
                    break;
                //新农合
                case "8":
                    cardtype = ((int)EnumCardType.XNHJYK).ToString();
                    break;
                default:
                    cardtype = ((int)EnumCardType.XNK).ToString();
                    break;
            }


            var khentity = _sysCardRepository.FindEntity(p => p.patid == patid && p.OrganizeId == OrganizeId && p.zt == "1" && p.CardType == cardtype);
            return Success("", khentity);
        }
        /// <summary>
        /// 新农合S04 入院办理
        /// </summary>
        /// <param name="VO"></param>
        /// <returns></returns>
        public ActionResult S04submit(SysHosBasicInfoVO VO) {
            var S04par = _PatientBasicInfoDmnService.ComposeS04par(VO, OrganizeId);
            var resp = _PatientBasicInfoDmnService.S04submit(S04par, OrganizeId);
            if (resp.state)
            {

                    GuianXnhS04InfoEntity S06entity = null;
                    S06entity = _iGuianXnhS04InfoRepo.FindEntity(p => p.zyh == VO.zyh && p.OrganizeId == OrganizeId);
                    string keyvalue = null;
                    if (S06entity != null)
                    {
                        keyvalue = S06entity.Id;
                    }
                    else
                    {
                        S06entity = new GuianXnhS04InfoEntity();
                        S06entity.Id = System.Guid.NewGuid().ToString();
                    }

                    S06entity.zyh = VO.zyh;
                    S06entity.OrganizeId = OrganizeId;
                    S06entity.xnhgrbm = VO.xnhgrbm;
                    S06entity.xnhylzh = VO.xnhylzh;
                    S06entity.inpId = resp.data.inpId;
                    S06entity.Create(true);
                    _iGuianXnhS04InfoRepo.SubmitForm(S06entity, keyvalue);
            }
            return Content(resp.ToJson());
        }
        public ActionResult XnhCancelRybl(string zyh)
        {
            #region 拉取S30目录对照
            //string startDate = "2012-01-28 12:43:27 081";
            //for (int i = 0; i < 100; i++)
            //{
            //    var request = new Newtouch.HIS.Proxy.guian.DTO.S30.S30RequestDTO
            //    {
            //        startDate = startDate
            //    };
            //    var response = CommonProxy.GetInstance("9eb11c59-9b09-4bbf-834e-e324aa30230c").S30(request);
            //    if (response.state)
            //    {
            //        var list = response.data;
            //        BaseGaXnhS30Entity entity = null;
            //        foreach (var item in list)
            //        {
            //            entity = new BaseGaXnhS30Entity();
            //            item.MapperTo(entity);
            //            entity.OrganizeId = this.OrganizeId;
            //            entity.zt = "1";
            //            entity.CreatorCode = "root";
            //            entity.CreateTime = DateTime.Now;
            //            entity.Id = Guid.NewGuid().ToString();
            //            _daseGaXnhS30Repo.Insert(entity);

            //        }
            //        int ss = Convert.ToInt32(entity.datetime.Substring(entity.datetime.Length - 2, 2)) + 1;
            //        string ssss = "";
            //        if (ss.ToString().Length == 1)
            //        {
            //            ssss = "0" + ss.ToString();
            //        }
            //        else
            //        {
            //            ssss = ss.ToString();
            //        }
            //        entity.datetime = entity.datetime.Substring(0, entity.datetime.Length - 2) + ssss;
            //        startDate = entity.datetime;
            //    }
            //    else
            //    {
            //        break;
            //    }

            //}

            //return Content("");
            #endregion

            if (string.IsNullOrWhiteSpace(zyh))
            {
                var data = new { state = "0", message = "没有住院信息，不能取消入院！" };
                return Content(data.ToJson());
            }
            if (_PatientBasicInfoDmnService.CheckHasFee(zyh, this.OrganizeId))
            {
                var data = new { state = "0", message = "有未结算的住院费用记录，不能取消入院。" };
                return Content(data.ToJson());
            }
            InpatientSettXnhPatInfoVO patinfo = _dischargeSettleDmnService.GetInpatientSettXnhPatInfo(zyh, this.OrganizeId);
            if (patinfo != null && !string.IsNullOrEmpty(patinfo.inpId))
            {
                if (_sysConfigRepo.GetBoolValueByCode("HOSP_INTERFACE_WITH_CPOE",this.OrganizeId) == true)
                {
                    //仅新入院可以取消入院登记
                    if (!(patinfo.zybz == ((int)EnumZYBZ.Xry).ToString()))
                    {
                        if (patinfo.zybz == ((int)EnumZYBZ.Bqz).ToString())
                        {
                            var data = new { state = "0", message = "请先在医生站取消入区，才能取消入院"};
                            return Content(data.ToJson());
                        }
                        else
                        {
                            var data = new { state = "0", message = "当前为" + ((EnumZYBZ)Convert.ToInt32(patinfo.zybz)).GetDescription() + "状态，不能取消入院" };
                            return Content(data.ToJson());
                        }
                    }
                }
                S05RequestDTO S05ReqDTO = new S05RequestDTO()
                {
                    inpId = patinfo.inpId,
                    areaCode = "",
                    isTransProvincial = "0",
                    cancelCause = ""
                };
                var S05ResDto = _PatientBasicInfoDmnService.S05submit(S05ReqDTO, OrganizeId);
                if (S05ResDto.state)
                {
                    var data = new {state = "1", message = "取消入院成功"};
                    return Content(data.ToJson());
                }
                else
                {
                    var data = new { state = "0", message = "取消入院失败！农合接口返回错误：" + S05ResDto.message };
                    return Content(data.ToJson());
                }
            }
            else
            {
                var data = new { state = "0", message = "取消入院失败！错误：住院号" + zyh + "获取患者信息失败！" };
                return Content(data.ToJson());
            }

        }

        /// <summary>
        /// 新农合S06 入院办理信息修改
        /// </summary>
        /// <param name="VO"></param>
        /// <returns></returns>
        public ActionResult S06submit(SysHosBasicInfoVO VO) {
            var S06par = _PatientBasicInfoDmnService.ComposeS06par(VO, OrganizeId);
            var S06Res= _PatientBasicInfoDmnService.S06submit(S06par, OrganizeId);
            if (!S06Res.state)
            {
                return Error("农合接口返回失败：" + S06Res.message);
            }
            GuianXnhS04InfoEntity S06entity = null;
            S06entity = _iGuianXnhS04InfoRepo.FindEntity(p => p.zyh == VO.zyh && p.OrganizeId == OrganizeId);
            string keyvalue = null;
            if (S06entity != null)
            {
                keyvalue = S06entity.Id;
            }
            else {
                S06entity = new GuianXnhS04InfoEntity();
                S06entity.Id = System.Guid.NewGuid().ToString();
            }

            S06entity.zyh = VO.zyh;
            S06entity.OrganizeId = OrganizeId;
            S06entity.xnhgrbm = VO.xnhgrbm;
            S06entity.xnhylzh = VO.xnhylzh;
            S06entity.inpId = VO.inpId;
            S06entity.Create(true);
            _iGuianXnhS04InfoRepo.SubmitForm(S06entity, keyvalue);
            return Success();
        }

        /// <summary>
        /// 验证新农合接口对照
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult validateTTentity(string code) {
           var data= _PatientBasicInfoDmnService.validateTTentity(OrganizeId,code);
            return Content(data.ToJson());
        }
		#endregion

		#region 重庆医保
	    public ActionResult GetCQjzdjInfo(string zyh)
	    {
            var model = _PatientBasicInfoDmnService.GetCQjzdjInfo(zyh, this.OrganizeId);
            model.operatorId = this.UserIdentity.rygh;
            model.operatorName = this.UserIdentity.UserName;
            model.orgId = this.OrganizeId;
            return Content(model.ToJson());
	    }
        public ActionResult UpdateCqybOut02(string zyh)
        {
            
            _PatientBasicInfoDmnService.UpdateCqybOut02(zyh,this.OrganizeId);
            return Success();
        }
        #endregion

        #region 医保业务
        public ActionResult ValidateYbFirstVisit(string sfzh, string xm,string kh=null,string jzpzlx=null)
        {

            var entity = _PatientBasicInfoDmnService.ValidateFirstVisit(sfzh,xm,this.OrganizeId,kh,jzpzlx);
            return Success("", entity);
        }
        #endregion

        #region 病人性质
        /// <summary>
        /// 根据险种类型获取病人性质
        /// </summary>
        /// <param name="xzlx"></param>
        /// <returns></returns>
        public ActionResult GetBrxzbyxzlx(string xzlx)
        {
            List<SysPatientNatureEntity> brxzlist = new List<SysPatientNatureEntity>();
            if (string.IsNullOrWhiteSpace(xzlx))
                brxzlist = _SysPatiNatureRepo.IQueryable(p => p.zt == "1" && p.OrganizeId == OrganizeId).ToList();
            else
                brxzlist = _SysPatiNatureRepo.IQueryable(p => p.insutype == xzlx && p.zt == "1" && p.OrganizeId == OrganizeId).ToList();
            return Content(brxzlist.ToJson());
        }
        public ActionResult Getzdinfo()
        {
            var data = _PatientBasicInfoDmnService.GetksZzdList(OrganizeId);
            return Content(data.ToJson());
        }

        #endregion
    }
}
