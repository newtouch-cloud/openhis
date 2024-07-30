using FrameworkBase.MultiOrg.Web;
using Newtonsoft.Json;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.InterfaceSync;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices.Clinic;
using Newtouch.Domain.IRepository.Clinic;
using Newtouch.Domain.ValueObjects.Clinic;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.ClinicManage.Controllers
{
    public class ClinicApplyController : OrgControllerBase
    {
        public readonly static string OuterMediaMeettingAPIHost = ConfigurationManager.AppSettings["OuterMediaMeettingAPIHost"];//api中心站点访问appid

        private readonly IClinicDmnService _clinicDmnService;
        private readonly IClinicApplyRepo _clinicApplyRepo;

        // GET: ClinicManage/ClinicApply
        public ActionResult Index()
        {
            return View();
        }

        #region 查询门诊预约记录
        public ActionResult GetClinicApplyGridJson(Pagination pagination, string xm, string zjh, string kssj, string jssj, string ksCode, string ysgh, int sqzt)
        {
            var data = new
            {
                rows = _clinicDmnService.GetClinicApplyGridJson(pagination, this.OrganizeId, xm, zjh, kssj, jssj, ksCode, ysgh, sqzt,this.UserIdentity.UserCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult Updatesqzt(string id, int sqzt)
        {
            _clinicApplyRepo.Updatesqzt(id, sqzt, this.OrganizeId);
            return Success("操作成功。");
        }

        public ActionResult Accept(ClinicApplyInfoVO applyInfo)
        {
            var czks = "00000080";//远程诊疗科室编号
            #region 1.挂号
            var ghpbObj = _clinicDmnService.getClinicScheduleId(this.OrganizeId, czks); //获取远程诊疗的挂号排班
            if (ghpbObj == null)
            {
                throw new FailedException("未找到排班信息");
            }
            var scheduId = ghpbObj.ScheduId;//获取远程诊疗排班Id
            var regFree = ghpbObj.RegFee;//获取挂号费用
            var payLsh = "yczl" + EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zl_zflsh", OrganizeId, "{0:D6}", true); ;//自动获取支付流水号
            var ghDataObj = new
            {
                kh = applyInfo.kh,
                ScheduId = scheduId,
                ghxz = "0",//自费
                PayWay = "3",//支付宝
                PayFee = regFree,//0元
                PayLsh = payLsh
            };
            var ghReqObj = new
            {
                Data = ghDataObj,
                AppId = AuthenManageHelper.accessAppId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstrgh = AuthenManageHelper.HttpPost<OutpatientRegistVO>(ghReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Outpatient/OutpatientRegist", this.UserIdentity);
            if (!(outstrgh.msg == "" || outstrgh.msg == "挂号成功"))
            {
                throw new FailedException(outstrgh.msg);
            }
            #endregion

            //2.调申请会议室接口
            //string token = AuthenManageHelper.GetToken();
            var hysqDataObj = new
            {
                usercode = this.UserIdentity.UserCode,
                username = this.UserIdentity.UserName,
                applyId = applyInfo.Id,//自费
                roomReset = "true",//默认false，当会议超时需重新开始会议时 传值： true
                roomid = applyInfo.mettingId,
                mzh = outstrgh.data.Mzh
            };
            var hysqReqObj = new
            {
                Data = hysqDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrMetting = AuthenManageHelper.HttpPost<MeetingResponseVO>(hysqReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/RemoteTreated/DoctorMeetingApply", this.UserIdentity);

            if (!(outstrMetting.msg == "" || outstrMetting.msg == "请求成功"))
            {
                throw new FailedException(outstrMetting.msg);
            }
            var roompath = outstrMetting.data.roompath;
            var mettingUrl = OuterMediaMeettingAPIHost + roompath;

            //获取诊所Token
            var tokenReqObj = new
            {
                Data = "",
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrToken = AuthenManageHelper.HttpPost<ClinicTokenResponseVO>(tokenReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/GetClinicToken", this.UserIdentity);

            if (!(outstrToken.msg == "" || outstrToken.msg == "请求成功"))
            {
                throw new FailedException(outstrToken.msg);
            }

            //3.会议号通知云诊所接口
            var sqjgDataObj = new
            {
                ApplyId= applyInfo.Id,
                Sqlsh = applyInfo.sqlsh,
                ApplyStu = (int)Emunzlzt.jzz,
                IsConfirm = "True",  //医生是否同意申请 True同意 false拒绝
                roomid = outstrMetting.data.roomid,
            };
            var sqjgReqObj = new
            {
                Data = sqjgDataObj,
                AppId = AuthenManageHelper.appId,
                Sqlsh=applyInfo.sqlsh,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstrSqjg = AuthenManageHelper.HttpPost<bool>(sqjgReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/TreatedApplyResult", this.UserIdentity);

            if (!(outstrSqjg.msg == "" || outstrSqjg.msg == "请求成功"))
            {
                throw new FailedException(outstrSqjg.msg);
            }

            var result = new
            {
                mettingUrl = mettingUrl,
                data = outstrMetting.data
            };
            return Content(result.ToJson());
            //return Success("操作成功。");
        }


        public ActionResult Back(ClinicApplyInfoVO applyInfo)
        {
            //1.更新退回状态
            _clinicApplyRepo.Updatesqzt(applyInfo.Id, (int)Emunzlzt.yth, this.OrganizeId);

            //2.获取诊所Token
            var tokenReqObj = new
            {
                Data = "",
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrToken = AuthenManageHelper.HttpPost<ClinicTokenResponseVO>(tokenReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/GetClinicToken", this.UserIdentity);

            if (!(outstrToken.msg == "" || outstrToken.msg == "请求成功"))
            {
                throw new FailedException(outstrToken.msg);
            }

            //3.会议号通知云诊所接口
            var sqjgDataObj = new
            {
                ApplyId = applyInfo.Id,
                Sqlsh = applyInfo.sqlsh,
                ApplyStu = (int)Emunzlzt.yth,
                IsConfirm = "false",  //医生是否同意申请 True同意 false拒绝
                roomid = "",
            };
            var sqjgReqObj = new
            {
                Data = sqjgDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrSqjg = AuthenManageHelper.HttpPost<bool>(sqjgReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/TreatedApplyResult", this.UserIdentity);

            if (!(outstrSqjg.msg == "" || outstrSqjg.msg == "请求成功"))
            {
                throw new FailedException(outstrSqjg.msg);
            }
            return Success("操作成功");
        }

        public ActionResult EnterMetting(ClinicApplyInfoVO applyInfo) {
            //获取token
            //string token = AuthenManageHelper.GetToken();

            //2.调申请会议室接口
            //string token = AuthenManageHelper.GetToken();
            var hysqDataObj = new
            {
                usercode = this.UserIdentity.UserCode,
                username = this.UserIdentity.UserName,
                applyId = applyInfo.Id,//自费
                roomid = applyInfo.mettingId,
            };
            var hysqReqObj = new
            {
                Data = hysqDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            //var outstrMetting = AuthenManageHelper.HttpPost<ApiManageResponse<MeetingResponseVO>>(hysqReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/RemoteTreated/DoctorMeetingApply", this.UserIdentity);
            var outstrMetting = AuthenManageHelper.HttpPost<MeetingResponseVO>(hysqReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/RemoteTreated/UserJoinMeetingApply", this.UserIdentity);

            if (!(outstrMetting.msg == "" || outstrMetting.msg == "请求成功"))
            {
                throw new FailedException(outstrMetting.msg);
            }
            var roompath = outstrMetting.data.roompath;
            var mettingUrl = OuterMediaMeettingAPIHost + roompath;
            
            var result = new
            {
                mettingUrl = mettingUrl,
                data = outstrMetting.data
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// 结束就诊
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult yczlEndOpetration(string Id, TreatmentEntity jzObject, MedicalRecordEntity blObject)
        {
            var applyInfo = _clinicApplyRepo.GetYczl(Id, this.OrganizeId);
            //1.推送病历
            //获取云诊所患者病历
            //var  blDataObj = new
            //{
            //    ApplyId = Id,
            //    Sqlsh = applyInfo.sqlsh,
            //};
            //var blReqObj = new
            //{
            //    Data = blDataObj,
            //    AppId = AuthenManageHelper.appId,
            //    OrganizeId = OrganizeId,
            //    Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            //};
            //var outstrbl = AuthenManageHelper.HttpPost<PatMedicalRecordVO>(blReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/GetClinicPatMedicalRecord", this.UserIdentity);

            //if (!(outstrbl.msg == "" || outstrbl.msg == "请求成功"))
            //{
            //    throw new FailedException(outstrbl.msg);
            //}
            //SendPatMedicalRecordVO cfblDataObj = new SendPatMedicalRecordVO()
            //{
            //    ApplyId = Id,
            //    Sqlsh = applyInfo.sqlsh,
            //    blId = outstrbl.data == null ? null : outstrbl.data.blId,
            //    OrganizeId = outstrbl.data == null ? null : outstrbl.data.OrganizeId,
            //    mzh = outstrbl.data == null ? null : outstrbl.data.mzh,
            //    mjzbz = outstrbl.data == null ? null : outstrbl.data.mjzbz,
            //    blh = outstrbl.data == null ? null : outstrbl.data.blh,
            //    tizhong = outstrbl.data == null ? null : outstrbl.data.tizhong,
            //    tiwen = outstrbl.data == null ? null : outstrbl.data.tiwen,
            //    maibo = outstrbl.data == null ? null : outstrbl.data.maibo,
            //    xuetangclfs = outstrbl.data == null ? null : outstrbl.data.xuetangclfs,
            //    xuetang = outstrbl.data == null ? null : outstrbl.data.xuetang,
            //    shengao = outstrbl.data == null ? null : outstrbl.data.shengao,
            //    shousuoya = outstrbl.data == null ? null : outstrbl.data.shousuoya,
            //    shuzhangya = outstrbl.data == null ? null : outstrbl.data.shuzhangya,
            //    huxi = outstrbl.data == null ? null : outstrbl.data.huxi,
            //    hy = outstrbl.data == null ? null : outstrbl.data.hy,
            //    zs = outstrbl.data == null ? null : outstrbl.data.zs,
            //    fbsj = outstrbl.data == null ? null : outstrbl.data.fbsj,
            //    xbs = outstrbl.data == null ? null : outstrbl.data.xbs,
            //    jws = outstrbl.data == null ? null : outstrbl.data.jws,
            //    ct = outstrbl.data == null ? null : outstrbl.data.ct,
            //    clfa = outstrbl.data == null ? null : outstrbl.data.clfa,
            //    fzjc = outstrbl.data == null ? null : outstrbl.data.fzjc,
            //    yjs = outstrbl.data == null ? null : outstrbl.data.yjs,
            //    gms = outstrbl.data == null ? null : outstrbl.data.gms,
            //};

            //推送处方病历
            SendPatMedicalRecordVO cfblDataObj = new SendPatMedicalRecordVO();
            cfblDataObj.ApplyId = Id;
            cfblDataObj.Sqlsh = applyInfo.sqlsh;
            cfblDataObj.OrganizeId = jzObject.OrganizeId;
            cfblDataObj.mzh = jzObject.mzh;
            cfblDataObj.mjzbz = jzObject.mjzbz;
            cfblDataObj.blh = jzObject.blh;
            cfblDataObj.tizhong = jzObject.tizhong;
            cfblDataObj.tiwen = jzObject.tiwen;
            cfblDataObj.maibo = jzObject.maibo;
            cfblDataObj.xuetangclfs = jzObject.xuetangclfs;
            cfblDataObj.xuetang = jzObject.xuetang;
            cfblDataObj.shengao = jzObject.shengao;
            cfblDataObj.shousuoya = jzObject.shousuoya;
            cfblDataObj.shuzhangya = jzObject.shuzhangya;
            cfblDataObj.huxi = jzObject.huxi;
            cfblDataObj.hy = jzObject.hy;
            cfblDataObj.zs = blObject.zs;
            cfblDataObj.fbsj = blObject.fbsj;
            cfblDataObj.xbs = blObject.xbs;
            cfblDataObj.jws = blObject.jws;
            cfblDataObj.ct = blObject.ct;
            cfblDataObj.clfa = blObject.clfa;
            cfblDataObj.fzjc = blObject.fzjc;
            cfblDataObj.yjs = blObject.yjs;
            cfblDataObj.gms = blObject.gms;

                var cfblReqObj = new
                {
                    Data = cfblDataObj,
                    AppId = AuthenManageHelper.appId,
                    OrganizeId = OrganizeId,
                    Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var outstrcfbl = AuthenManageHelper.HttpPost<string>(cfblReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/SendHisMedicalRecord", this.UserIdentity);
            
            //if (!(outstrcfbl.msg == "" || outstrcfbl.msg == "请求成功"))
            //{
            //    throw new FailedException(outstrcfbl.msg);
            //}

            //2.推送处方
            var cfDataObj = new
            {
                ApplyId = Id,
                Sqlsh = applyInfo.sqlsh,
                mzh=applyInfo.mzh
            };
            var cfReqObj = new
            {
                Data = cfDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstrcf = AuthenManageHelper.HttpPost<string>(cfReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/SendHisOutpPrescriptionData", this.UserIdentity);

            //if (!(outstrcf.msg == "" || outstrcf.msg == "请求成功"))
            //{
            //    throw new FailedException(outstrcf.msg);
            //}

            //3.发送结束通知
            var sqjgDataObj = new
            {
                ApplyId = Id,
                Sqlsh = applyInfo.sqlsh,
                ApplyStu = (int)Emunzlzt.yjs,
                IsConfirm = "True",  //医生是否同意申请 True同意 false拒绝
                roomid = "",
            };
            var sqjgReqObj = new
            {
                Data = sqjgDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrSqjg = AuthenManageHelper.HttpPost<bool>(sqjgReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/TreatedApplyResult", this.UserIdentity);

            if (!(outstrSqjg.msg == "" || outstrSqjg.msg == "请求成功"))
            {
                throw new FailedException(outstrSqjg.msg);
            }
            //3.会议室发送信息
            //4.更新申请状态
            _clinicApplyRepo.Updatesqzt(Id, (int)Emunzlzt.yjs, this.OrganizeId);

            return Success("操作成功");
            //return Content(result.ToJson());
        }
    }
}