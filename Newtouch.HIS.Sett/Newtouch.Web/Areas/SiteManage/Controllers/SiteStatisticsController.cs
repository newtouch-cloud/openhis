using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Areas.SiteManage.Controllers
{
    /// <summary>
    /// 站点统计
    /// </summary>
    public class SiteStatisticsController : ControllerBase
    {
        private readonly IGRSCostEarningDmnService _jgssCostEarningDmnService;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly Ijgss_EarningInfoRepo _jgss_EarningInfoRepo;
        private readonly Ijgss_AttachmentInfoRepo _jgss_AttachmentInfoRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        #region 收支统计审核

        /// <summary>
        /// 获取待审收支统计数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAwaitingTrialSiteStatisticsGridJson()
        {
            return Content("");
        }

        /// <summary>
        /// 审核 统计 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifySiteStatistics()
        {
            return View();
        }

        /// <summary>
        /// 审核 统计明细 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifySiteStatisticsDetail()
        {
            ViewBag.currentUserName = UserIdentity.UserName;
            return View();
        }

        #endregion

        #region 收支统计录入

        /// <summary>
        /// 获取主页数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSiteStatisticsGridJson()
        {
            return Content("");
        }

        /// <summary>
        /// 统计录入 视图
        /// </summary>
        /// <returns></returns>
        public override ActionResult Form()
        {
            var maxSize = ConfigurationHelper.GetAppConfigValue("jgzsMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            ViewData["maxSize"] = Convert.ToInt32(maxSize);
            var fcbl = _sysConfigRepo.GetIntValueByCode("jgssfdbl", this.OrganizeId);
            ViewData["jgssfdbl"] = fcbl;
            return View();
        }

        /// <summary>
        /// 统计录入 视图
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult RevenueForm(string keyValue)
        {
            return View();
        }

        /// <summary>
        /// 机构收支填报
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public ActionResult GetIndexGridJson(Pagination pagination, string siteId, string year, string month, string shzt, bool verify = false)
        {
            if (string.IsNullOrWhiteSpace(siteId))
            {
                throw new FailedException("缺少机构");
            }
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("缺少年份");
            }
            if (string.IsNullOrWhiteSpace(month))
            {
                throw new FailedException("缺少月份");
            }
            var list = new
            {
                rows = _jgssCostEarningDmnService.GetjgszInfoList(pagination, siteId, year, month, shzt, verify),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public ActionResult ViewChargeMoneyDetail()
        {
            return View();
        }

        public ActionResult GetMoneyDetailGridJson(string siteId, string year, string month, string type, string dlcode)
        {
            if (string.IsNullOrWhiteSpace(siteId))
            {
                throw new FailedException("缺少站点");
            }
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("缺少年份");
            }
            if (string.IsNullOrWhiteSpace(month))
            {
                throw new FailedException("缺少年份");
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new FailedException("缺少类型");
            }
            if (string.IsNullOrWhiteSpace(dlcode))
            {
                throw new FailedException("缺少大类");
            }
            //var list = new
            //{
            //    rows = _jgssCostEarningDmnService.GetMoneyDetailListV2(pagination, siteId, year, month, type, dlcode),
            //    total = pagination.total,
            //    page = pagination.page,
            //    records = pagination.records
            //};
            var list = _jgssCostEarningDmnService.GetMoneyDetailList(siteId, year, month, type, dlcode);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string site, string year, string month)
        {
            var data = _jgssCostEarningDmnService.GetCosttable(site, year, month);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srxx"></param>
        /// <param name="srxxList"></param>
        /// <param name="cbxxList"></param>
        /// <param name="keyvalue"></param>
        /// <param name="fjxx"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult savefile(string srxx, string srxxList, string cbxxList, string keyvalue, string fjxx)
        {
            var Serializer = new JavaScriptSerializer();
            var vo = new SiteCostEarningVO
            {
                srxx = Serializer.Deserialize<jgssEarningVO>(srxx),
                srxxList = Serializer.Deserialize<List<jgssEarningGridVO>>(srxxList),
                cbxxList = Serializer.Deserialize<List<jgssCostVO>>(cbxxList),
                fjxx = Serializer.Deserialize<List<jgssAttachmentVO>>(fjxx),
            };

            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {

                    string path = "";
                    _jgssCostEarningDmnService.UploadFile(Request.Files[i], out path);

                    jgssAttachmentVO javo = new jgssAttachmentVO()
                    {
                        ContentType = Request.Files[i].ContentType,
                        fjmc = Request.Files[i].FileName,
                        fjPath = path
                    };
                    vo.fjxx.Add(javo);

                }
            }
            _jgssCostEarningDmnService.submitEarningInfo(vo, keyvalue, OrganizeId);


            return Success();
        }

        public ActionResult Download(string id)
        {

            var earn = _jgss_AttachmentInfoRepo.FindEntity(p => p.Id == id && p.zt == "1");
            if (earn == null)
            {
                throw new FailedException("缺少机构收支信息");
            }
            if (string.IsNullOrWhiteSpace(earn.fjPath))
            {
                throw new FailedException("缺少附件路径");
            }
            if (string.IsNullOrWhiteSpace(earn.ContentType))
            {
                throw new FailedException("缺少附件类型");
            }
            //var configFileExportBaseDir = ConfigurationHelper.GetAppConfigValue("LocalFileBaseDir");
            //if (string.IsNullOrWhiteSpace(configFileExportBaseDir))
            //{
            //    configFileExportBaseDir = "D:\\";
            //}

            //1.指定要写到的文件目录及名称
            byte[] buffer = System.IO.File.ReadAllBytes(earn.fjPath);

            return File(buffer, earn.ContentType, System.IO.Path.GetFileName(earn.fjPath));

        }


        /// <summary>
        /// submit
        /// </summary>
        /// <param name="keyWord">SiteCostEarningVO </param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult savedata(jgssEarningVO srxx, List<jgssEarningGridVO> srxxList, List<jgssCostVO> cbxxList, string keyvalue)
        {

            SiteCostEarningVO vo = new SiteCostEarningVO();
            vo.srxx = srxx;
            vo.srxxList = srxxList;
            vo.cbxxList = cbxxList;

            _jgssCostEarningDmnService.submitEarningInfo(vo, keyvalue, OrganizeId);
            return Success();
        }

        /// <summary>
        /// get form json
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyWord)
        {
            var data = _jgssCostEarningDmnService.GetEarningInfo(keyWord, this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(data.srxx.siteId))
            {
                data.srxx.zdmc = _sysOrganizeDmnService.GetNameByOrgId(data.srxx.siteId);
            }
            return Success(data.ToJson());
        }

        /// <summary>
        /// delete data by keyValue
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _jgssCostEarningDmnService.deletess(keyValue);
            return Success();
        }

        #endregion

        #region 收支统计  highCharts

        /// <summary>
        /// 站点收支统计图标 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SiteStatisticsHighCharts()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 审核操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public ActionResult updateshzt(string id, string shzt)
        {
            string account = UserIdentity.UserCode;
            _jgss_EarningInfoRepo.updateshzt(id, shzt, account);
            return Success();

        }

        /// <summary>
        /// 当前登录人的组织机构
        /// </summary>
        /// <returns></returns>
        public ActionResult getCurrentOrgIds()
        {
            var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(UserIdentity.UserId);
            return Content(mediOrgList.ToJson());
        }

        /// <summary>
        /// 当前登录人的树组织机构
        /// </summary>
        /// <returns></returns>
        public ActionResult getCurrentTreeOrgIds()
        {
            var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(UserIdentity.UserId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in mediOrgList)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = null;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }



        public ActionResult getAllOrgIds()
        {

            var data = _sysOrganizeDmnService.GetValidListByParentOrg(OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 当前组织机构当前时间是否已存在过
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public ActionResult isAlreadyCollect(string site, string year, string month)
        {
            var earn = _jgss_EarningInfoRepo.FindEntity(p => p.siteId == site && p.year == year && p.month == month && p.zt == "1");
            if (earn != null)
            {
                return Content(true.ToJson());
            }
            return Content(false.ToJson());

        }

        /// <summary>
        /// 获取上个月的成本信息
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public ActionResult loadLastMonthcbxx(string site, string year, string month)
        {
            var data = _jgssCostEarningDmnService.getLastcbxx(site, year, month);
            return Content(data.ToJson());
        }
    }
}