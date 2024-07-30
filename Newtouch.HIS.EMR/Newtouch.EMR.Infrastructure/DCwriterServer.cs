using DCSoft.Writer.Controls;
using Newtouch.Core.Common.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Infrastructure
{
    public class DCwriterServer
    {
        private WebWriterControlEngine dcWControl;
        public static string EditorRegCode = ConfigurationHelper.GetAppConfigValue("EditorRegCode");
        public WebWriterControlEngine DCWControl
        {
            get { return dcWControl; }
            set { dcWControl = value; }
        }

        public DCwriterServer()
        {
        }

        public WebWriterControlEngine Init()
        {
            DCWControl = new WebWriterControlEngine();
            DCWControl.SetRegisterCode(EditorRegCode);
            DCWControl.Options.ControlName = "myWriterControl";//生成前端标签时的ID
            DCWControl.Options.ContentRenderMode = WebWriterControlRenderMode.NormalHtmlEditable;
            DCWControl.Options.ServicePageURL = "/YzPreView/MoreHandleDCWriterServicePage";
            DCWControl.Options.ImageDataEmbedInHtml = true;//图片与表达式显示    
            DCWControl.BorderStyle = "1px solid black";//边框样式
            DCWControl.Options.WorkspaceBackColor = ColorTranslator.FromHtml("#7cb9e8");//背景色  
            DCWControl.DocumentOptions.ViewOptions.FieldBackColor = Color.Transparent;
            DCWControl.DocumentOptions.ViewOptions.FieldFocusedBackColor = Color.White;
            return DCWControl;
        }
        public WebWriterControlEngine MedicalTemplate()
        {
            DCWControl = new WebWriterControlEngine();
            DCWControl.SetRegisterCode(EditorRegCode);
            DCWControl.Options.ControlName = "myWriterControl";//生成前端标签时的ID
            DCWControl.Options.ContentRenderMode = WebWriterControlRenderMode.NormalHtmlEditable;
            DCWControl.Options.ServicePageURL = "/MedRecordTemplate/MoreHandleDCWriterServicePage";
            DCWControl.Options.ImageDataEmbedInHtml = true;//图片与表达式显示    
            DCWControl.BorderStyle = "1px solid black";//边框样式
            DCWControl.Options.WorkspaceBackColor = ColorTranslator.FromHtml("#7cb9e8");//背景色  
            DCWControl.DocumentOptions.ViewOptions.FieldBackColor = Color.Transparent;
            DCWControl.DocumentOptions.ViewOptions.FieldFocusedBackColor = Color.White;
            return DCWControl;
        }
    }
}
