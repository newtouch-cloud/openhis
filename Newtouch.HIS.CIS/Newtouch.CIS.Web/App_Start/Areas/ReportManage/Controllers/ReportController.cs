using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportController : OrgControllerBase
    {
        /// <summary>
        /// 康复处方单
        /// </summary>
        /// <returns></returns>
        public ActionResult RehabPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 常规项目处方单
        /// </summary>
        /// <returns></returns>
        public ActionResult RegularItemPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 康复治疗单
        /// </summary>
        /// <returns></returns>
        public ActionResult RehabPresTreatmentReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 西药处方单
        /// </summary>
        /// <returns></returns>
        public ActionResult WMPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 中药处方单
        /// </summary>
        /// <returns></returns>
        public ActionResult TCMPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 病历单
        /// </summary>
        /// <returns></returns>
        public ActionResult MRReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 长期医嘱打印
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintCqyzReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 临时医嘱打印
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintLsyzReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 医嘱执行后，打印药品单
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintYzYpReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 检验申请单
        /// </summary>
        /// <returns></returns>

        public ActionResult PrintJyReport()
        {
            ReportingServiceCom();
            return View();
        }
        //检查申请单
        public ActionResult PrintJcReport()
        {
            ReportingServiceCom();
            return View();
        }
        //全院抗生素药物使用金额排名
        public ActionResult PrintKssJePMReport()
        {
            ReportingServiceCom();
            return View();
        }
        //全院抗生素药物使用量排名
        public ActionResult PrintKssSlPMReport()
        {
            ReportingServiceCom();
            return View();
        }
        //门诊抗菌药物处方比例
        public ActionResult PrintKssCfPropReport()
        {
            ReportingServiceCom();
            return View();
        }

         
        #region private methods

        /// <summary>
        /// 
        /// </summary>
        private void ReportingServiceCom()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

        #endregion
    }
}