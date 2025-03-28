using DCSoft.Writer;
using DCSoft.Writer.Controls;
using System.Drawing;

namespace Newtouch.EMR.DCWriter
{
    public class WebWriterContorl
    {
        private WebWriterControlEngine webControl;
        public WebWriterContorl() { }

        public WebWriterControlEngine WebWriter
        {
            get { return webControl; }
            set { webControl = value; }
        }

        public WebWriterControlEngine Init()
        {
            WebWriter = new WebWriterControlEngine();

            WebWriter.Options.ControlName = "myWriterControl";
            WebWriter.DocumentOptions.ViewOptions.ShowInputFieldStateTag = false;
            //数据校验不通过时候的背景色
            WebWriter.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = Color.Yellow;
            WebWriter.DocumentOptions.BehaviorOptions.AcceptDataFormats = WriterDataFormats.Text;

            WebWriter.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
            //用Tab键在各个输入域之间切换
            WebWriter.DocumentOptions.BehaviorOptions.MoveFocusHotKey = MoveFocusHotKeys.Tab;
            //文档中显示段落符号的标记
            WebWriter.DocumentOptions.ViewOptions.ShowParagraphFlag = false;
            //是否显示输入域状态标签
            WebWriter.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
            //是否关闭加密显示
            WebWriter.DocumentOptions.ViewOptions.EnableEncryptView = true;
            //是否显示输入域状态标签
            //WebWriter.Options.BackgroundTextOutputMode = DCBackgroundTextOutputMode.Underline;
            //内容的呈现方式
            WebWriter.Options.ContentRenderMode = WebWriterControlRenderMode.NormalHtmlEditable;

            //DCControl.Options.ReferencePath = "dcwriterreference";
            WebWriter.Options.WorkspaceBackColorString = "#B1CAEB";
            WebWriter.Options.WorkspaceBackColor = ColorTranslator.FromHtml("#f2f2f2");//背景色     
            WebWriter.BorderStyle = "1px solid black";//边框样式
            WebWriter.Options.ServicePageURL = "/MedicalRecord/HandleDCWriterServicePage";
            WebWriter.Options.IndentHtmlCode = false;
            WebWriter.DocumentOptions.BehaviorOptions.ParagraphFlagFollowTableOrSection = true;// 取消插入表格后空行显示
            WebWriter.Options.ImageDataEmbedInHtml = true;// 打印显示医学表达式
            WebWriter.Options.ClientContextMenuType = WebClientContextMenuType.SystemDefault;// 显示系统默认右键菜单
            WebWriter.DocumentOptions.BehaviorOptions.OutputFieldBorderTextToContentText = false;
            //留痕相关设置
            WebWriter.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = true;
            WebWriter.DocumentOptions.SecurityOptions.ShowPermissionMark = true;
            WebWriter.DocumentOptions.SecurityOptions.ShowPermissionTip = true;

            WebWriter.Options.UseDCWriterMiniJS = DCBooleanValueHasDefault.Default;//是否启用被压缩的DCWriterMini.js文件
            WebWriter.Options.CompressSessionData = true;//是否压缩服务器端的会话数据
            WebWriter.Options.CurrentLoadOptions = WebWriterControlLoadDocumentOptions.CompressSessionData;//压缩服务器端会话数据    

            return WebWriter;
        }

    }
}
