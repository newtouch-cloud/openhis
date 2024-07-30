using System.Collections.Generic;
using System.Linq;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure.Model;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Common;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [HandlerLogin]
    public class HomeController : FrameworkBase.MultiOrg.Web.Controllers.HomeController
    {
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly IPyDmnService _pyDmnService;
        private readonly IMedicineApp _medicineApp;
        private readonly IHomePageStatisticsApp _homePageStatisticsApp;

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Index()
        {
            var cookieLoginFlag = WebHelper.GetCookie(Constants.AppId + "_" + OperatorProvider.GetCurrent().UserCode + "_" + "LoginFlag");
            ViewBag.cookieLoginFlag = cookieLoginFlag;
            return base.Index();
        }

        /// <summary>
        /// 用户切换药房
        /// </summary>
        /// <returns></returns>
        public ActionResult SetUserPharmacyForm()
        {            
            ViewBag.yfbmList = _sysUserDmnService.GetYfbmListByUserId(OperatorProvider.GetCurrent().UserId, OperatorProvider.GetCurrent().OrganizeId);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opr"></param>
        public override void SwithOrgSuccessOprBeforeSave(OperatorModel opr)
        {
            //更新用户药房部分
            if (ConfigurationHelper.GetAppConfigBoolValue("Is_UserYfbmRelated") == true && opr.OrganizeId != null)
            {
                //当前要操作的药房部门
                if (!string.IsNullOrWhiteSpace(opr.UserId) && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId);
                }
                if (opr.yfbmCodeList != null && opr.yfbmCodeList.Count > 0)
                {
                    //当前要操作的药房部门    //在OrgId关联了多个yfbm怎么办
                    var curYfbmCode = opr.yfbmCodeList.FirstOrDefault();
                    var curYfbmObj = _sysPharmacyDepartmentRepo.GetUserYfbmByCode(curYfbmCode, opr.OrganizeId);
                    if (curYfbmObj != null)
                    {
                        Constants.SetCurrentYfbm(opr.UserId, new LoginUserCurrentYfbmModel()
                        {
                            yfbmCode = curYfbmCode,
                            yfbmjb = curYfbmObj.yfbmjb,
                            mzzybz = curYfbmObj.mzzybz,
                            yfbmmc = curYfbmObj.yfbmmc
                        });
                    }                    
                }
                else
                {
                    Constants.SetCurrentYfbm(opr.UserId, null);  //移除
                }
            }

            //切换药房部门
            WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_IsHospAdministrator", opr.IsHospAdministrator.ToString().ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Default()
        {
            return View();
        }

        /// <summary>
        /// 获取待处理html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNeedDealDiv()
        {
            var result = _homePageStatisticsApp.AssembleNeedDealHtml();
            return Success("", result);
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// 获取带处理总数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPendingCount()
        {
            return Content(_homePageStatisticsApp.GetPendingCount().ToJson());
        }

        /// <summary>
        /// 获取门诊月处方发药次
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFyCountVoByYfbm()
        {
            var result = new MonthlySummaryDO[2];
            result[0] = _medicineApp.GetFyCountVoByYfbm(_pyDmnService.GetMzFyCountVoByYfbm());
            result[1] = _medicineApp.GetFyCountVoByYfbm(_pyDmnService.GetZyFyCountVoByYfbm());
            return Content(result.ToJson());
        }

        /// <summary>
        /// 获取门诊月处方发药次
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFyCountBydl()
        {
            var result = _medicineApp.GetFyCountBydl() ?? new List<FyClassificationStatistics>();
            return Content(result.ToJson());
        }

        /// <summary>
        /// 切换登陆用户药房
        /// </summary>
        /// <param name="kfId">库房ID</param>
        /// <param name="kfName">库房名称</param>
        /// <param name="kfLeve">库房等级</param>
        /// <returns></returns>
        public ActionResult SetUserPharmacy(string yfbmCode, string mzzybz, string yfbmjb, string yfbmmc)
        {
            var opr = Constants.CurrentYfbm;
            opr.yfbmCode = yfbmCode;
            opr.yfbmjb = yfbmjb;
            opr.mzzybz = mzzybz;
            opr.yfbmmc = yfbmmc;
            Constants.SetCurrentYfbm(OperatorProvider.GetCurrent().UserId, opr);
            return Success("切换成功");
        }

		#region 消息提醒
        public ActionResult getmesquery()
		{
            string yfbm= Constants.CurrentYfbm.yfbmCode;
            string kcyjz = SysConfigReader.String("GET_YFKCYG");//库存预警值
            int gqyjz = -SysConfigReader.Int("GET_YFGQYG");//过期预警值
            var retdata = _sysUserDmnService.MSGQuery(yfbm,this.OrganizeId, gqyjz, kcyjz);
            int gqyjcount=0,kcyjcount=0;
			if (retdata!=null)
			{
                kcyjcount=retdata.Where(p => p.typeas == "1").Count();
                gqyjcount = retdata.Where(p => p.typeas == "2").Count();
            }
            var data =new {
                rows = retdata,
                kcyj = kcyjcount,
                gqyj= gqyjcount
            };
            return Content(data.ToJson());
		}
        #endregion


        /// <summary>
        /// 获取业务字段的随机产生值（自增+Format）
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgIdIsStar"></param>
        /// <param name="topOrgIdIsStar"></param>
        /// <param name="initFormat"></param>
        /// <param name="initFieldLength"></param>
        /// <returns></returns>
        public ActionResult GetNewFieldUniqueValue(string fieldName, bool? orgIdIsStar = null, bool? topOrgIdIsStar = null, string orgId = null, string topOrgId = null, string initFormat = "", int initFieldLength = 0)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                if (orgIdIsStar.HasValue)
                {
                    orgId = orgIdIsStar.Value ? "*" : OperatorProvider.GetCurrent().OrganizeId;
                }
            }
            if (string.IsNullOrWhiteSpace(topOrgId))
            {
                if (topOrgIdIsStar.HasValue)
                {
                    topOrgId = topOrgIdIsStar.Value ? "*" : Constants.TopOrganizeId;
                }
            }
            if (string.IsNullOrWhiteSpace(initFormat) && initFieldLength > 0)
            {
                initFormat = "{0:D" + initFieldLength + "}";
            }
            string value = null;
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(topOrgId) || initFormat == null)
            {
                value = null;
            }
            //else if(orgId == topOrgId)
            //{
            //    value = null;   //???????????????这样合适么
            //}
            else
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }

    }
}