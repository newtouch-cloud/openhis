using DCSoft.Writer;
using DCSoft.Writer.Controls;
using FrameworkBase.MultiOrg.Web;
using Newtonsoft.Json;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.MRQC.Domain.IDomainServices.QcBlzkManage;
using Newtouch.MRQC.Domain.IRepository.QcItemManage;
using Newtouch.MRQC.Domain.ValueObjects;
using Newtouch.MRQC.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Areas.QualityControlManage.Controllers
{
    public class MedicalCenterController :OrgControllerBase// ControllerBase
    {
        private readonly IQcItemDataRepo _qcitemdataRepo;
        private readonly IQcBlzkDmnService _qcblzkDmn;
        public static string BlTemplatePath = ConfigurationHelper.GetAppConfigValue("BlTemplatePath");

        private static WebWriterControlEngine engine;
        // GET: QualityControlManage/MedicalCenter
        public override ActionResult Index()
        {
            DCwriterServer dcControl = new DCwriterServer();
            dcControl.Init();
            WebWriterControlEngine eng = dcControl.DCWControl;//这是上一步创建的引擎 
            //eng.Options.ContentRenderMode = WebWriterControlRenderMode.PagePreviewHtml;
            ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            eng.Dispose();
            return View();
        }

        public ActionResult PreView()
        {
            return View();
        }
        public ActionResult MessageQuery()
        {
            return View();
        }
        public ActionResult ScoreFrom()
        {
            return View();
        }

        public ActionResult QcMessageManage()
        {
            return View();
        }
        #region DC病历文书

        [ValidateInput(false)]
        public ActionResult MoreHandleDCWriterServicePage()
        {
            return new MoreActionResult();
        }

        private class MoreActionResult : ActionResult
        {
            public MoreActionResult()
            {
            }
            public override void ExecuteResult(ControllerContext context)
            {
                WebWriterControlServicePageExecuter executer = new WebWriterControlServicePageExecuter();
                executer.CrossDomainSettings.Enabled = true;//是否跨域
                                                            // executer.EventSaveFileContent = new DCSoft.Writer.WriterSaveFileContentEventHandler(My_SaveFileContent);//事件
                executer.EventReadFileContent = new DCSoft.Writer.WriterReadFileContentEventHandler(My_ReadFileContent);
              
                if (executer.HandleService())
                {
                    return;
                }
            }
            /// <summary>
            /// 加载模板
            /// </summary>
            /// <param name="eventSender"></param>
            /// <param name="args"></param>
            public void My_ReadFileContent(object eventSender, WriterReadFileContentEventArgs args)
            {
                if (!string.IsNullOrWhiteSpace(args.FileName))
                {
                    try
                    {
                        string filename = args.FileName;
                        if (filename.Substring(0, 1) == "~")
                        {
                            filename = filename.Substring(1);
                        }
                        string ml = BlTemplatePath;//AppDomain.CurrentDomain.BaseDirectory;

                        ml = ml.Substring(0, ml.Length - 1);
                        string xml = System.IO.File.ReadAllText($"{ml}{filename}");

                        byte[] bs = System.IO.File.ReadAllBytes($"{ml}{filename}");
                        args.ResultBinary = bs;

                    }
                    catch (Exception e) {
                        args.Message = "未找到病历";//传递给前端的信息 
                    }
                    args.Handled = true;
                }

            }
        }

        #endregion

        #region 质控中心接口调用
        public ActionResult GetPatList(string brbz,DateTime ksrq,DateTime jsrq,string srz)
        {
            string token = AuthenManageHelper.GetToken();
            var reqObj = new
            {
                Data = new
                {
                    brbz = brbz,
                    ksrq = ksrq,
                    jsrq = jsrq,
                    srz = srz
                },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            AuthenTokenHelper apiResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(AuthenManageHelper.HttpPost(reqObj.ToJson(),
                AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalCenterPatInfo", token));
            
            var data = JsonConvert.DeserializeObject<List<PatientDataVo>>(apiResp.BusData.data.ToString());
            return Content(data.ToJson());
        }
        /// <summary>
        /// 诊断列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetMedicalCenterDiag(string zyh)
        {
            string token = AuthenManageHelper.GetToken();
            var reqObj = new
            {
                Data = new { zyh = zyh },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            AuthenTokenHelper apiResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(AuthenManageHelper.HttpPost(reqObj.ToJson(),
                AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalCenterDiaglist", token));

            var data = JsonConvert.DeserializeObject<List<MedicalHomeDiagVo>>(apiResp.BusData.data.ToString());
            return Content(data.ToJson());
        }
        /// <summary>
        /// 病历文书tree 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetMedicalCenterBlTree(string zyh)
        {
            string token = AuthenManageHelper.GetToken();
            var reqObj = new
            {
                Data = new { zyh = zyh },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            AuthenTokenHelper apiResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(AuthenManageHelper.HttpPost(reqObj.ToJson(),
                AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalCenterBlwsTree", token));

            var data = JsonConvert.DeserializeObject<List<MedRecordTree>>(apiResp.BusData.data.ToString());
            var treeList = new List<TreeGridModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                    treeModel.id = item.Id.ToString();
                    treeModel.isLeaf = hasChildren;
                    treeModel.parentId = item.ParentId == null ? null : item.ParentId.ToString();
                    treeModel.expanded = hasChildren;
                    treeModel.entityJson = item.ToJson();
                    treeList.Add(treeModel);

                }
            }
            return Content(treeList.TreeGridJson(null));
        }

        public ActionResult GetDoctorsAdviceRecord(string zyh,string yzlx)
        {
            var reqObj = new
            {
                Data = new { zyh = zyh,yzlx=yzlx },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<List<DoctorAdviceRecordVo>>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalDoctorsAdvice/GetDoctorAdviceRecore", this.UserIdentity);
            return Content(outstr.data.ToJson());
        }
        /// <summary>
        /// 门诊住院检验检查申请单
        /// </summary>
        /// <param name="jzh"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="reportType"></param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        public ActionResult GetInspectionExaminationRecore(string jzh,DateTime ksrq, DateTime jsrq,string reportType, string mzzybz)
        {
            var reqObj = new
            {
                Data = new { jzh = jzh, ksrq = ksrq ,jsrq=jsrq,reportType=reportType,mzzybz=mzzybz},
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<List<MedicalInspectionExaminationVo>>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalDoctorsAdvice/GetInspectionExaminationRecore", this.UserIdentity);
            return Content(outstr.data.ToJson());
        }

        #endregion

        #region 质控消息
        public ActionResult GetTyzkxm(string bllx,string keyword)
        {
            var data = _qcitemdataRepo.IQueryable(p => p.BlmbId == bllx && p.OrganizeId == this.OrganizeId && p.zt == "1" && p.Name.Contains(keyword)).ToList();
            var d = data.ToJson();
            return Content(data.ToJson());
        }
        public ActionResult SendMsg(MsgSendVo sendvo,string zyysgh)
        {
            var blwszk = "ZKNoticeBl"+ this.UserIdentity.UserCode;//病历质控消息发送组模板
            var defaultNoticeContent = "姓名:{0},住院号:{1},床号:{2}，科室:{3},医生:{4},的病历质控异常,病历类型:{5},病历文书:{6},反馈内容:{7},问题等级:{8},请及时处理,限期处理时间:{9}";
            var noticeGroup = new
            {
                GroupTag = blwszk,
                GroupName = "病历文书质控组消息",
                GroupDesc = "质控系统病历文书专属",
                GroupYwlx = (int)GroupYwlxEnum.None,
                NoticeRange = (int)MsgNoticeRangeEnum.User,
                DefaultTitle = "您有新的病历质控消息待查收",
                DefaultContent = defaultNoticeContent,
                DefaultContentData="",
                CreatorCode=this.UserIdentity.UserCode
            };
            var reqObj = new
            {
                Data = noticeGroup,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var NoticeGroupId = "";
            var checknoticeGroup = AuthenManageHelper.HttpPost<List<MsgNoticeGroupVo>>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MsgQueue/GetNoticeGroup", this.UserIdentity);
            if (checknoticeGroup.data!=null && checknoticeGroup.data.Count > 0)
            {
                NoticeGroupId = checknoticeGroup.data[0].Id;
            }
            else
            {
                var addnoticeGroup = AuthenManageHelper.HttpPost<string>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MsgQueue/NewNoticeGroup", this.UserIdentity);
                if (addnoticeGroup.code == 10000 && !string.IsNullOrWhiteSpace(addnoticeGroup.data))
                {
                    NoticeGroupId = addnoticeGroup.data;
                }
                else {
                    throw new FailedException("发送消息失败："+ addnoticeGroup.msg);
                }
            }
            defaultNoticeContent = "姓名:{xm},住院号:{jzh},床号:{ch}，科室:{ks},医生:{zyys},的病历质控异常,质控日期:{zkrq},病历类型:{zklx},病历文书:{hzwd},反馈内容:{fknr},问题等级:{wtdj},请及时处理,限期处理时间:{qxclsj}";
            var NoticeQueue = new {
                SendFrom = this.UserIdentity.UserCode,
                NoticeGroupId= NoticeGroupId,
                GroupYwlx = (int)GroupYwlxEnum.None,
                NoticeRange = (int)MsgNoticeRangeEnum.User,
                Title = "您有新的病历文书质控消息待查收",
                Content= defaultNoticeContent,
                ContentData= sendvo.ToJson(),
                CreatorCode= this.UserIdentity.UserCode,
                Recipient = zyysgh
            };
            var NoticereqObj = new
            {
                Data = NoticeQueue,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var NewNoticeResult = AuthenManageHelper.HttpPost<List<string>>(NoticereqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MsgQueue/NewNotice", this.UserIdentity);

            return Success(NewNoticeResult.code== 10000 ? "质控消息发送成功":NewNoticeResult.msg);
        }
        public ActionResult MessageSendQuery(string status,DateTime ksrq,DateTime jsrq)
        {
            var data = GetMessageSendData(status,ksrq,jsrq);
            return Content(data.ToJson());
        }
        public ActionResult GetDclMsgJson(string status, DateTime ksrq, DateTime jsrq)
        {
            var msgvo = GetMessageSendData(status, ksrq, jsrq);
            List<string> data = new List<string>();
            if (msgvo != null)
                data = msgvo.Where(t => t.GroupTag.Contains("ZKNoticeBl")).Select(p => p.ContentData).ToList();
            var treeList = new List<TreeGridModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    treeModel.id = Guid.NewGuid().ToString();
                    treeModel.isLeaf = false;
                    treeModel.parentId =null;
                    treeModel.expanded = false;
                    treeModel.entityJson = item;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeGridJson(null));
        }
        public List<MsgNoticeQueueVo> GetMessageSendData(string status, DateTime ksrq, DateTime jsrq)
        {
            var reqObj = new
            {
                Data = new { NoticeStu = status, ksrq = ksrq, jsrq = jsrq, appId = AuthenManageHelper.appId, },
                AppId = AuthenManageHelper.appId,

                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<List<MsgNoticeQueueVo>>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MsgQueue/NoticeQuery", this.UserIdentity);
            var data = outstr.data != null ? outstr.data.OrderByDescending(p => p.CreateTime).ToList() : outstr.data;
            return data;
        }
        #endregion

        #region 评分
        public ActionResult Getybbxbldata(string bllx,string zyh)
        {
            var list = _qcblzkDmn.Getybbxbldata(bllx, this.OrganizeId, zyh);
            return Content(list.ToJson());
        }
        /// <summary>
        /// 保存评分
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="blId"></param>
        /// <param name="bllx"></param>
        /// <param name="blmc"></param>
        /// <returns></returns>
        public ActionResult SaveScoreDate(List<ScoreEntity> entity ,string bllx,string blmc,string zyh)
        {
            _qcblzkDmn.SaveScoreDate(entity,this.OrganizeId,this.UserIdentity.UserCode,bllx,blmc,zyh);
            return Success();
        }
        #endregion
    }
}