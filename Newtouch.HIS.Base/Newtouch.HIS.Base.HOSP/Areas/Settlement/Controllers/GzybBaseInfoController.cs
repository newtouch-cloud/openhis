using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using System;
using Newtouch.Core.Common;
using Newtouch.Tools;

namespace Newtouch.HIS.Base.HOSP.Areas.Settlement.Controllers
{
    public class GzybBaseInfoController : Controller
    {
        private readonly IGzybBaseInfoDmnService _IGzybBaseInfoDmnService;
        private readonly IGzybCodeContrastRepo _IGzybCodeContrastRepo;
        public GzybBaseInfoController(IGzybCodeContrastRepo _iGzybCodeContrastRepo
            , IGzybBaseInfoDmnService _igzybBaseInfoDmnService)
        {
            this._IGzybCodeContrastRepo = _iGzybCodeContrastRepo;
            this._IGzybBaseInfoDmnService = _igzybBaseInfoDmnService;
        }
        /// <summary>
        /// 贵州医保基础数据页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MedicalInsuranceCatalogue()
        {
            return View();
        }

        public ActionResult GetGzybItemCode_MaxLsh()
        {
            try
            {
                int maxlsh = _IGzybBaseInfoDmnService.GetItemCodeMaxLsh();
                return Content(maxlsh.ToString());
            }
            catch
            {
                return Content(string.Empty);
            }
        }

        public ActionResult SaveCodeConstrast(List<GzybCodeContrastEntity> data)
        {
            bool success = true;
            foreach (GzybCodeContrastEntity info in data)
            {
                if (!_IGzybCodeContrastRepo.Exist(info.aaa100, info.aaa102))
                {
                    var loginInfo = OperatorProvider.GetCurrent();
                    info.jdry = loginInfo.UserCode;
                    info.jdrq = DateTime.Now;
                    info.zt = "1";
                    if (!_IGzybCodeContrastRepo.InsertInfo(info))
                    {
                        success = false;
                        break;
                    }
                }
            }
            return Content(success ? "1" : "0");
        }

        #region 医保目录相关
        /// <summary>
        /// 获取目录下载最新版本号
        /// </summary>
        /// <param name="tbname"></param>
        /// <returns></returns>
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
                default:
                    break;
            }
            var data = new
            {
                bbh =  _IGzybBaseInfoDmnService.Header(tbname.Trim()) ?? "0"
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取下载数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="tbname"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult CatalogueData(Pagination pagination, string tbname, string key)
        {
            pagination.sidx = "VER desc";
            pagination.sord = "asc";
            var datapan = new
            {
                rows = _IGzybBaseInfoDmnService.Get_G_yb_mluCommon_Info(pagination, tbname, key),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(datapan.ToJson());
        }

        #endregion


    }
}