using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCSoft.Writer;
using DCSoft.Writer.Controls;
using System.Drawing;

namespace DCControl
{
    public class DCWebControl
    {
        private WebWriterControlEngine dcWControl;
        public WebWriterControlEngine DCWControl 
        {
            get { return dcWControl; }
            set { dcWControl = value; }
        }

        public DCWebControl()
        { 

        }

         public WebWriterControlEngine Init(string _regcode)
        {            
            DCWControl = new WebWriterControlEngine();
            DCWControl.SetRegisterCode (_regcode);
            DCWControl.Options.ControlName = "myWriterControl";          
            //DCWControl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = false;
            //数据校验不通过时候的背景色
            //DCWControl.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = Color.Yellow;
            //DCWControl.DocumentOptions.BehaviorOptions.AcceptDataFormats = WriterDataFormats.Text;
            
            //DCWControl.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
            //用Tab键在各个输入域之间切换
            //DCWControl.DocumentOptions.BehaviorOptions.MoveFocusHotKey = MoveFocusHotKeys.Tab;           
            //文档中显示段落符号的标记
            //DCWControl.DocumentOptions.ViewOptions.ShowParagraphFlag = false;            
            //是否显示输入域状态标签
            //DCWControl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
            //是否关闭加密显示
            //DCWControl.DocumentOptions.ViewOptions.EnableEncryptView = true;           
            //是否显示输入域状态标签
            //DCWControl.Options.BackgroundTextOutputMode = DCBackgroundTextOutputMode.Underline;
            //内容的呈现方式
            DCWControl.Options.ContentRenderMode = WebWriterControlRenderMode.NormalHtmlEditable;

            //DCWControl.Options.ReferencePathForDebug = "dcwriterreference";
            //DCControl.Options.ReferencePath = "dcwriterreference";
            //DCWControl.Options.WorkspaceBackColorString = "#B1CAEB";
            //DCWControl.Options.WorkspaceBackColor = ColorTranslator.FromHtml("#f2f2f2");//背景色     
            DCWControl.BorderStyle = "1px solid black";//边框样式
            DCWControl.DocumentOptions.ViewOptions.FieldBackColor = Color.Transparent;
            //DCWControl.DocumentOptions.ViewOptions.FieldBackColorValue = "Transparent";
            DCWControl.DocumentOptions.ViewOptions.FieldFocusedBackColor = Color.White;
            //DCWControl.DocumentOptions.ViewOptions.FieldFocusedBackColorValue = "White";
            //DCWControl.Options.ServicePageURL = "/MedicalRecordManage/MedicalRecord/HandleDCWriterServicePage";
            DCWControl.Options.IndentHtmlCode = false;
            DCWControl.DocumentOptions.BehaviorOptions.ParagraphFlagFollowTableOrSection = true;// 取消插入表格后空行显示
            DCWControl.Options.ImageDataEmbedInHtml = true;// 打印显示医学表达式
            DCWControl.Options.ClientContextMenuType = WebClientContextMenuType.SystemDefault;// 显示系统默认右键菜单
            //DCWControl.DocumentOptions.BehaviorOptions.OutputFieldBorderTextToContentText = false;           
            //留痕相关设置
            DCWControl.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = true;
            DCWControl.DocumentOptions.SecurityOptions.ShowPermissionMark = true;
            DCWControl.DocumentOptions.SecurityOptions.ShowPermissionTip = true;

            DCWControl.Options.UseDCWriterMiniJS = DCBooleanValueHasDefault.Default;//是否启用被压缩的DCWriterMini.js文件
            DCWControl.Options.CompressSessionData = true;//是否压缩服务器端的会话数据
            DCWControl.Options.CurrentLoadOptions = WebWriterControlLoadDocumentOptions.CompressSessionData;//压缩服务器端会话数据    
            
            return DCWControl;
        }
    }
}
