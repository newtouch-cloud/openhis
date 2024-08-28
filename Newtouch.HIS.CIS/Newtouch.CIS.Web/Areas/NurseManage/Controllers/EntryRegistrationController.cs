using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IDomainServices.Inpatient;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class EntryRegistrationController : OrgControllerBase
    {
        private readonly IEntryRegistrationDmnService _ientryRegistrationDmnService;
        private readonly IInpatientPatientInfoRepo _iInpatientPatientInfoRepo;
        private readonly IInpatientBedUseRecordRepo _iInpatientBedUseRecordRepo;
		private readonly IWardMaintenanceDmnService _WardMaintenanceDmnService;
		// GET: NurseManage/EntryRegistration
		public ActionResult Index()
        {
            return View();
        }

        public ActionResult EntryAreaFrom(string ysgh,string ysmc,string zyh,string type,string ryrq)
        {
            ViewBag.ysgh = ysgh;
            ViewBag.ysmc = ysmc;
            ViewBag.zyh = zyh;
            ViewBag.ryrq = ryrq;
            ViewBag.type = type;
            return View();
        }
        /// <summary>
        /// 患者入区
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="rqrq"></param>
        /// <param name="wzjb"></param>
        /// <param name="cwCode"></param>
        /// <returns></returns>
        public ActionResult EnterArea(EntryRegistrationVO enterArea)
        {
            var reqInarea = new patInAreaRequestDto
            {
                zyh = enterArea.zyh,
                OrganizeId = this.OrganizeId,
                rq = enterArea.rqrq,
                wzjb = enterArea.wzjb,
                cwCode = enterArea.cwCode,
                ysgh = enterArea.ysgh,
                ysmc = enterArea.ysmc,
                bq = enterArea.bqdm,
                user=UserIdentity.rygh
            };
            string retMsg = "F|";

            //新入区 床位是否已占用
            if (_ientryRegistrationDmnService.ValidationBedIsUse( this.OrganizeId, enterArea.cwCode, enterArea.bqdm) == true)
            {
                retMsg= "F|该床位已被占用";
            }
            else { 
            
                //新入区患者，调用接口获取患者信息；转区患者直接修改信息
                if (enterArea.qqlx =="0")
                {
                    retMsg= _ientryRegistrationDmnService.SavePatInArea(reqInarea);
                }
                else
                {
                    var patChangeAreaRequestDto = new patChangeAreaRequestDto
                    {
                        zyh = enterArea.zyh,
                        OrganizeId = this.OrganizeId,
                        rqrq = enterArea.rqrq,
                        cwCode= enterArea.cwCode,
                        wzjb= enterArea.wzjb,
                        ysgh=enterArea.ysgh,
                        ysmc=enterArea.ysmc,
                        lastcwCode= enterArea.lastcwCode,
                        WardCode= enterArea.bqdm,
                        DeptCode= enterArea.DeptCode,
                        user=UserIdentity.rygh                        
                    };
                    retMsg = _ientryRegistrationDmnService.SaveChangeAreaPatInfo(patChangeAreaRequestDto);
                }
            }
            //如果成功，则产生床位绑定相关费用
            if (retMsg.Split('|')[0].ToString()=="T")
            {
                patBedFeeRequestDto req = new patBedFeeRequestDto()
                {
                    zyh = enterArea.zyh,
                    rq = enterArea.rqrq,
                    OrganizeId = this.OrganizeId,
                    user = UserIdentity.rygh
                };
                _ientryRegistrationDmnService.AddPatBedItemFee(req);
            }
            else
            {
                return Error(retMsg.Split('|')[1].ToString());
            }
            return Success();
        }
        /// <summary>
        /// 患者转床
        /// </summary>
        /// <param name="syxh"></param>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        public ActionResult ChangeBed(string zyh, string bedCode, string wardCode)
        {
            patBedRequestDto reqDto = new patBedRequestDto()
            {
                zyh = zyh,
                bedCode = bedCode,
                OrganizeId = this.OrganizeId,
                user=UserIdentity.rygh
            };
            string retMsg = "F|";
            //转床床位是否被占用
            if (_ientryRegistrationDmnService.ValidationBedIsUse(this.OrganizeId, bedCode, wardCode) == true)
            {
                retMsg = "F|该床位已被占用";
            }
            else { 
                retMsg= _ientryRegistrationDmnService.ChangeBed(reqDto);
            }
            if (retMsg.Split('|')[0].ToString() == "T")
            {
                patBedFeeRequestDto req = new patBedFeeRequestDto()
                {
                    zyh = zyh,
                    rq = DateTime.Now,
                    OrganizeId = this.OrganizeId,
                    user = UserIdentity.rygh
                };
                _ientryRegistrationDmnService.AddPatBedItemFee(req);
            }
            else
            {
                return Error(retMsg.Split('|')[1].ToString());
            }
            return Success();
        }
        /// <summary>
        /// 更新危重级别
        /// </summary>
        /// <param name="enterArea"></param>
        /// <returns></returns>
        public ActionResult UpdateWzjb(string zyh,string wzjb)
        {
            string retMsg = "F|";
            retMsg = _ientryRegistrationDmnService.UpdatePatInfo(zyh,wzjb,this.OrganizeId);
            if (retMsg.Split('|')[0].ToString() != "T")
            {
                return Error(retMsg.Split('|')[1].ToString());
            }
            return Success();
        }
        /// <summary>
        /// 获取患者列表
        /// </summary>
        /// <param name="bqdm"></param>
        /// <param name="qqlx"></param>
        /// <returns></returns>
        public ActionResult GetPatList(string bqdm,string qqlx)
        {
            Pagination pagination = new Pagination()
            {
                page = 1,
                rows = 10000,
            };
            var reqObj = new
            {
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                //暂获取一月之内的登记信息
                lastUpdateTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd hh:mm:ss"),//"2018-04-01 11:11:11",
                zybz ="0",
                bqCode=bqdm,
                pagination = pagination
            };
            List<NewPatInfoVO> apiRespList = new List<NewPatInfoVO>();
            if (qqlx=="0")
            {
                //拉取待入区患者信息
                var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<InPatientQueryResponseVO>>(
                    "/api/patient/InPatientQuery", reqObj);
                if (apiResp.data != null)
                {
                    foreach (var item in apiResp.data.list)
                    {
                        NewPatInfoVO jzObj = new NewPatInfoVO();
                        jzObj.zyh = item.zyh;
                        jzObj.xb = item.sexValue;
                        jzObj.blh = item.blh;
                        jzObj.xm = item.xm;
                        jzObj.ryrq = item.ryrq == null ? "" : item.ryrq.ToString("yyyy-MM-dd HH:mm:ss");//.Substring(0, 10);
                        jzObj.brxzmc = item.brxzmc;
                        jzObj.nl = item.nl == null ? "" : item.nl;
                        jzObj.nlshow = item.nlshow == "0" ? "" : item.nlshow;
                        jzObj.zd = item.zzdmc;
                        jzObj.ysgh = item.ys;
                        jzObj.ysmc = item.ysxm;
                        apiRespList.Add(jzObj);
                    }
                }
            }
            else   //转区患者
            {
                apiRespList=_ientryRegistrationDmnService.GetPatChangeArea(bqdm);
            }
            
            var list = new
            {
                rows = apiRespList,
                total = apiRespList.Count,
                page = 1
                //records = apiResp.data.pagination.records
                //total = apiResp.data.pagination.total,
                //page = apiResp.data.pagination.page,
                //records = apiResp.data.pagination.records
            };
            return Content(list.ToJson());
        }
        /// <summary>
        /// 获取床位信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cwlb">全部 空床等</param>
        /// <returns></returns>
        public ActionResult GetBedList(string bqdm, string cwlb)
        {
            BedInfoViewRequestVO bedInfoReq = new BedInfoViewRequestVO {
                bqCode = bqdm,
                OrganizeId = this.OrganizeId,
                sfzy = cwlb
            };
            List<BedInfoViewResponseVO> cwList = _ientryRegistrationDmnService.GetCwInfoViewList(bedInfoReq);
            if (cwlb=="1")
            {
                cwList = cwList.Where(x => x.sfzy == false).ToList();
            }
            var list = new
            {
                rows = cwList
            };
            return Content(list.ToJson());
        }

        public ActionResult PatCancelEntry(string zyh)
        {
            patRequestDto saveDiagnosisDto = new patRequestDto()
            {
                zyh = zyh,
                OrganizeId = this.OrganizeId,
                user=UserIdentity.rygh
            };
            string isInAreaMsg = _ientryRegistrationDmnService.GetPatIsCancelInArea(saveDiagnosisDto);
            if (isInAreaMsg.Split('|')[0].ToString() == "F")
            {
                return Error(isInAreaMsg.Split('|')[1].ToString());
            }
            string isCalcelInAreaMsg = _ientryRegistrationDmnService.PatCancelInArea( saveDiagnosisDto);
            if (isCalcelInAreaMsg.Split('|')[0].ToString() == "F")
            {
                return Error(isCalcelInAreaMsg.Split('|')[1].ToString());
            }
            return Success("保存成功。");
        }

        public ActionResult ChangeAreaForm(string zyh)
        {
            ViewBag.zyh = zyh;
            return View();
        }
        /// <summary>
        /// 患者转区
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        public ActionResult SaveChangeArea(string zyh, string bqdm)
        {
            patRequestDto saveDiagnosisDto = new patRequestDto()
            {
                zyh = zyh,
                OrganizeId = this.OrganizeId,
                user=UserIdentity.rygh
            };
            string isOutAreaMsg = _ientryRegistrationDmnService.GetPatIsChangeArea(saveDiagnosisDto);
            if (isOutAreaMsg.Split('|')[0].ToString() == "F")
            {
                return Error(isOutAreaMsg.Split('|')[1].ToString());
            }
            string ChangeMsg= _ientryRegistrationDmnService.SaveChangeArea(saveDiagnosisDto, bqdm);
            if (ChangeMsg.Split('|')[0].ToString() == "F")
            {
                return Error(ChangeMsg.Split('|')[1].ToString());
            }
            return Success("保存成功。");
        }

        /// <summary>
        /// 出区召回
        /// </summary>
        /// <returns></returns>
        public ActionResult RecallOutArea()
        {
            return View();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">关键字</param>
        /// <param name="ksrq">开始日期</param>
        /// <param name="jsrq">结束日期</param>
        /// <param name="bqdm">病区代码</param>
        /// <returns></returns>

        public ActionResult GetOutAreaPatlist(Pagination pagination, string keyword, DateTime? ksrq, DateTime? jsrq,string bqdm)
        {
            var list = new
            {
                rows = _ientryRegistrationDmnService.GetOutAreaPatlist(pagination, keyword, ksrq, jsrq, bqdm,OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        /// <summary>
        /// 出区召回
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="bqdm">病区代码</param>
        /// <returns></returns>
        public ActionResult SaveRecallOutArea(string zyh,string bqdm)
        {
            string outAreaMsg = _ientryRegistrationDmnService.SaveRecallOutArea(zyh,bqdm);
            if (outAreaMsg.Split('|')[0].ToString() == "F")
            {
                return Error(outAreaMsg.Split('|')[1].ToString());
            }
            return Success("召回成功");

        }
        /// <summary>
        /// 在病区病人信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        public ActionResult GetINAreaPatlist(Pagination pagination, string keyword, string bqdm)
        {
            var list = new
            {
                rows = _ientryRegistrationDmnService.GetINAreaPatlist(pagination, keyword, bqdm,this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

		public ActionResult GetPatInfo(string zyh) {
			var patInfo=_iInpatientPatientInfoRepo.GetByZyh(this.OrganizeId,zyh);
			return Content(patInfo.ToJson()); ;
		}

		/// <summary>
		/// 获取住院床卡
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public ActionResult GetBedCard(string zyh)
		{
			var patInfo = _WardMaintenanceDmnService.GetBedCard( zyh, this.OrganizeId);
			return Content(patInfo.ToJson()); ;
		}
        public ActionResult getZyPatInfo(string zyh)
        {
            var patInfo = _WardMaintenanceDmnService.GetZyPatInfo(zyh,this.OrganizeId);
            return Content(patInfo.ToJson());
        }



        #region 获取病区


        public ActionResult TreeViewdata() {
            var wardTree = _iInpatientPatientInfoRepo.TreeViewdata(this.OrganizeId);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).ToList();

                //foreach (InpatientAreaVO itempat in patInfo)
                //{
                //    TreeViewModel treepat = new TreeViewModel();
                //    treepat.id = itempat.zyh;
                //    treepat.text = itempat.zyh + "-" + itempat.hzxm;
                //    treepat.value = itempat.zyh;
                //    treepat.parentId = item.bqCode;
                //    treepat.isexpand = false;
                //    treepat.complete = true;
                //    treepat.showcheck = true;
                //    treepat.checkstate = 0;
                //    treepat.hasChildren = false;
                //    treepat.Ex1 = "c";
                //    treeList.Add(treepat);
                //}

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "c";
                treeList.Add(tree);
            }
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
        }

        #endregion




        #region 医嘱续打
        public ActionResult GetZyPatContinuePrint(string zyh, string yzlx)
        {
            var patPrintInfo = _WardMaintenanceDmnService.GetZyPatContinuePrint(zyh, this.OrganizeId,yzlx);
            return Content(patPrintInfo.ToJson());
        }
        public ActionResult GetZyPatContinuePrintPage(string zyh, string yzlx,string xh,string page)
        {
            var patPrintInfo = _WardMaintenanceDmnService.GetZyPatContinuePrintPage(zyh, this.OrganizeId, yzlx,xh,page);
            return Content(patPrintInfo.ToJson());
        }

        #endregion

    }
}