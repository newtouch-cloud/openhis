using DCSoft.Writer;
using DCSoft.Writer.Controls;
using DCSoft.Writer.Dom;
using FrameworkBase.MultiOrg.Web;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.DomainServices;
using Newtouch.EMR.Infrastructure;
using Newtouch.EMR.Repository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.EMR.Web.Areas.SystemManage.Controllers
{
   
    public class YzPreViewController : OrgControllerBase//Controller
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IBlmblbRepo _blmbRepo;


        [ValidateInput(false)]
        public ActionResult MoreHandleDCWriterServicePage()
        {
            return new MoreActionResult();
        }

        public ActionResult YzPreView()
        {
            DCwriterServer dcControl = new DCwriterServer();
            dcControl.Init();
            WebWriterControlEngine eng = dcControl.DCWControl;//这是上一步创建的引擎 
            ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            eng.Dispose();
            return View();
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
            private void My_ReadFileContent(object eventSender, WriterReadFileContentEventArgs args)
            {
                if (!string.IsNullOrWhiteSpace(args.FileName))
                {
                    string filename = args.FileName;
                    string ml = AppDomain.CurrentDomain.BaseDirectory;

                    ml = ml.Substring(0, ml.Length - 1);
                    string xml = System.IO.File.ReadAllText($"{ml}{filename}");

                    byte[] bs = System.IO.File.ReadAllBytes($"{ml}{filename}");
                    args.ResultBinary = bs;
                    
                    //args.Message = "加载以后的返回信息";//传递给前端的信息 
                    args.Handled = true;
                }

            }
            /// <summary>
            /// 保存模板
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="args"></param>
            private void My_SaveFileContent(object sender, DCSoft.Writer.WriterSaveFileContentEventArgs args)
            {
                string filename = "xxx.xml";
                string ml = AppDomain.CurrentDomain.BaseDirectory;
                if (!System.IO.Directory.Exists(ml + "emrfile"))
                {
                    System.IO.Directory.CreateDirectory(ml + "emrfile");
                }
                System.IO.File.WriteAllText($"{ml}emrfile\\{filename}", args.Document.InnerXML);
            }

        }

        #region 医嘱续打
        public ActionResult GetZyCqYzData(string zyh, string orgId, string isSign)
        {
            var data = _CommonDmnService.GetPrintCqyzData(zyh, orgId, isSign);
            return Content(data.ToJson());
        }

        public ActionResult GetZyLsYzData(string zyh, string orgId, string isSign)
        {
            var data = _CommonDmnService.GetPrintLsyzData(zyh, orgId, isSign);
            return Content(data.ToJson());
        }

        public ActionResult GetInpatInfo(string zyh, string orgId)
        {
            orgId= orgId==null? this.OrganizeId:orgId;
            var data = _CommonDmnService.GetInpatInfo(zyh, orgId);
            return Content(data.ToJson());
        }
        #endregion

        #region 护理单打印
    
        public ActionResult GetHljldData(string zyh,string hljb)
        {
            var data = _CommonDmnService.GetPrintHljlData(zyh, this.OrganizeId, hljb);
            return Content(data.ToJson());
        }

        public ActionResult GetPrintTempleate(string templateName)
        {
            var data = _blmbRepo.FindEntity(p=>p.mbmc== templateName && p.OrganizeId==this.OrganizeId &&p.zt=="1");
            return Content(data.ToJson()); ;
        }
        #endregion
    }
}