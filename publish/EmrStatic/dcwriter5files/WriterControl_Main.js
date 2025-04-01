"use strict";

import { WriterControl_UI } from "./WriterControl_UI.js";
import { WriterControl_Paint } from "./WriterControl_Paint.js";
import { DCTools20221228 } from "./DCTools20221228.js";
import { WriterControl_API } from "./WriterControl_API.js";
import { WriterControl_Task } from "./WriterControl_Task.js";
import { WriterControl_Rule } from "./WriterControl_Rule.js";
import { WriterControl_Event } from "./WriterControl_Event.js";
import { WriterControl_Dialog } from "./WriterControl_Dialog.js";
import { WriterControl_DOMPackage } from "./WriterControl_DOMPackage.js";
import { WriterControl_DateTimeControl } from "./WriterControl_DateTimeControl.js";
import { WriterControl_EF } from "./WriterControl_EF.js";
import { WriterControl_DrawFu } from "./WriterControl_DrawFu.js";
import { WriterControl_ToolBar } from "./WriterControl_ToolBar.js";
import { WriterContorl_FlowChart } from "./WriterContorl_FlowChart.js";

export let WriterControl_Main = {

    /**初始化默认的字符串资源 */
    InitDefaultResourceStrings: function () {
        /**
        * 设置字符串资源值的全局函数
        * @param {Object} json 字符串对象
        */
        window.__SetDCStringResourceValues = function (json) {
            DotNet.invokeMethod(
                window.DCWriterEntryPointAssemblyName,
                "SetDCSRValues",
                json);
        };
        // 定义标准的字符串资源
        window.__DCSR = {
            FixLayoutForPrint :"文档处于打印或打印预览时无法修改文档内容排版。",
            FailToDownLoadFontFileForPDFOFD: "下载字体文件错误，无法创建PDF/OFD文件。",
            NotSupportWriteHtml: "本版本不支持输出HTML数据。",
            PromptDeleteRowOrColumn: "请确认操作：【是】删除表格行,【否】删除表格列，【取消】取消本次删除操作。",
            Waitting: "请等待...",
            PromptNotDCWriterOFD: "这不是都昌编辑器生成的OFD文件，无法打开。",
            PromptNotSupportFont: "DCWriter遇到不支持的字体,请联系软件供应商更新软件来支持这个字体，并将字体文件(ttc或ttf)复制到服务器的fonts子目录下。",
            CannotChangeSystemIcon: "不能修改名称以'System'开头的系统内部图标。",
            AdministratorMode: "管理员运行模式",
            AllFileFilter: "所有文件|*.*",
            AllowEditPageInfo: "允许中途修改页码",
            AllowFixTableRowColumnSize: "固定表格的行高和列宽",
            AllowUserChangeRows: "允许用户插入或删除表格行",
            ArgumentOutOfRange_Name_Value_Max_Min: "参数“{0}”值为“{1}”，超出范围，最大值“{2}”，最小值“{3}”。",
            AssistInsert: "快捷辅助录入",
            AutoFitPageSize: "整体缩放适应纸张大小",
            AutoLineViewMode: "自动换行视图模式",
            AutoSave: "自动保存模式",
            AutoSaveLocal: "基于本地文件系统的自动保存",
            AutoTranslateForCharacter: "字符自动转换",
            AutoZoom: "视图自动缩放",
            BackgroundColor: "背景色",
            BackgroundMode: "后台运行模式",
            BadParameterValueType_Name_Type_Value: "参数“{0}”数据类型错误，类型“{1}”，数据\"{3}\"。",
            Barcode: "一维条码",
            BeginReadFile_Name: "开始读取文件“{0}”。",
            BeginWriteFile_Name_Bytes: "开始写文件“{0}”，数据字节长度{1}。",
            BothEncryptView: "内容加密显示",
            BulletedList: "段落列表项目样式",
            ButtonElement: "按钮元素",
            ButtonNewText: "按钮",
            By: "由",
            CADisabledTip: "CA功能被禁止掉了，可以设置DocumentOptions.SecurityOptions.CAMode选项来启用本功能。",
            CanNotContains_Text: "不能包含“{0}”。",
            CheckBoxElement: "勾选框",
            CheckBoxEventExpressions: "勾选框事件表达式",
            CheckBoxMultiline: "勾选框文本多行模式",
            CheckBoxValueBinding: "勾选框数据源绑定",
            CheckBoxVisualStyle: "勾选框显示样式设置",
            CheckMRIDForbitWhenFail: "跨病历复制内容时禁止复制",
            CheckMRIDNoLimit: "跨病历复制内容时不限制",
            CheckMRIDPromptForbitWhenFail: "跨病历复制内容时提示禁止",
            CheckMRIDWarringWhenFail: "跨病历复制内容时警告",
            CheckRequired_Name: "单/复选框组“{0}”必须有勾选项。",
            CircleChar: "圆形文字套圈",
            CleanFormat: "清除文本格式",
            CleanViewMode: "整洁视图模式",
            ClearUserTrace: "批量删除留痕",
            ClipboardErrorMessage: " 可能是360等安全软件实时监控系统剪切板所造成的。",
            CloneCellComplete: "表格行复制（完全复制）",
            CloneCellContentWithClearField: "表格行复制（保留结构复制）",
            CloneCellDefault: "表格行复制（清空内容复制）",
            CodabarError: "Codabar条码长度不得小于3，而且开头和结尾是字符'A'或'B'或'C'或'D'。",
            Code11Error: "Code11条码只能包含数字和字符'-'。",
            Code128InvaliData: "Code128:错误的数据或格式.",
            Code39InvaliData: "Code39条码数据错误（可尝试Code39Extended条码）。",
            Code93InvaliData: "Code93:错误的数据,包含不支持的字符.",
            ComplexViewMode: "留痕视图模式",
            ContentProtectByUser: "基于用户角色的编辑权限",
            ContentProtection: "元素内容保护",
            ControlEventMessage: "启用事件消息",
            ControlReadonly: "文档内容只读",
            Copy: "复制",
            CopySource: "内容自动复制",
            CoreSystemAlert: "南京都昌信息科技有限公司 提醒",
            CreatorTipFormatString: "{DisplaySavedTime:yyyy-MM-dd HH:mm:ss}由{Name}创建",
            CtrlClickToLink: "按住Ctrl并鼠标单击可访问链接。",
            CurrentDocument: "本文档",
            CustomContextMenu: "自定义快捷菜单",
            CustomHandleError: "自定义报错处理",
            CustomMessageBox: "自定义消息框",
            CustomShapeElement: "自定义绘制图形",
            CustomTraceVisualization: "留痕样式设置",
            Cut: "剪贴",
            DataObjectRange_Application: "限制为只能复制本进程的数据",
            DataObjectRange_OS: "允许复制其他进程数据",
            DataObjectRange_SingleWriterControl: "限制为只能复制本控件的数据",
            DCSignInputInfo: "电子签名功能",
            DCSoftTestVersion: "南京都昌科技内部测试版",
            DefaultFont: "默认字体",
            DelayLoadWhenExpand: "文档节收缩展开",
            Delete: "删除",
            DeleteElement_Content: "删除”{0}“",
            DeleteElements_Count: "删除{0}个元素",
            DeleterTipFormatString: "{DisplaySavedTime:yyyy-MM-dd HH:mm:ss}由{Name}删除",
            DeleteTableRowOrColumn: "删除表格列或行",
            DeleteRegister: "注册标记无法删除!!!",
            DisplayFormat: "数值显示格式",
            DocCompareResult_TickSpan_Num: "文档内容对比操作耗时{0}毫秒，发现{1}处不同。",
            DocumentBackgroundImage: "背景图片",
            DocumentComment: "批注",
            DocumentGridLine: "文档网格线",
            DocumentNavigator: "文档导航",
            Downloading_URL: "正在下载“{0}”...",
            EAN13InvaliCountry: "EAN13:错误的国家代码。",
            EAN13InvaliData: "EAN13条码只能包含12或13个数字字符。",
            EAN8InvaliData: "EAN8:错误的数据，只能包含7或8个数字字符。",
            EditControlReadonly: "编辑器控件是只读的。",
            EditWithUserTrace: "留痕书写",
            ElementAttributes: "自定义属性",
            ElementEvent: "元素事件",
            ElementIDForEditableDependent_SrcID_TargetID_Result: "元素“{0}”联动设置元素“{1}”的ContentReadonly值为“{2}”。",
            ElementType_AccountingNumber: "会计数字",
            ElementType_Barcode: "条码",
            ElementType_Block: "文件块 ",
            ElementType_Body: "文档正文",
            ElementType_Char: "字符",
            ElementType_CheckBox: "复选框",
            ElementType_Comment: "文档批注",
            ElementType_ContentLink: "内容链接",
            ElementType_ControlHost: "控件宿主",
            ElementType_Document: "文档",
            ElementType_Footer: "页脚",
            ElementType_FooterForFirstPage: "首页页脚",
            ElementType_Header: "页眉",
            ElementType_HeaderForFirstPage: "首页页眉",
            ElementType_HL: "水平线",
            ElementType_Image: "图片",
            ElementType_InputField: "输入域",
            ElementType_Label: "文本标签",
            ElementType_LineBreak: "断行符",
            ElementType_Media: "多媒体",
            ElementType_PageBreak: "分页符",
            ElementType_PageInfo: "页码",
            ElementType_ParagraphFlag: "段落符号",
            ElementType_RadioBox: "单选框",
            ElementType_Sign: "锁定符",
            ElementType_Table: "表格",
            ElementType_TableCell: "单元格",
            ElementType_TableColumn: "表格列",
            ElementType_TableRow: "表格行",
            ElementType_TDBarcode: "二维条码",
            EmphasisMark: "着重号",
            EncryptProviderNotFind: "未找到数据加密器：{0}。",
            ExcludeKeyword_Keyword: "出现违禁关键字“{0}”。",
            ExcludeKeywords: "违禁关键字",
            ExcludeRange_Range: "文本不包含在列表“{0}”。",
            EXP_DeciduousTeech: "乳牙牙位图",
            EXP_DiseasedTeethBottom: "病变下牙牙位图",
            EXP_DiseasedTeethTop: "病变上牙牙位图",
            EXP_FetalHeart: "胎心图",
            EXP_LightPosition: "光定位图",
            EXP_MenstrualHistory: "月经史",
            EXP_PainIndex: "疼痛指数图",
            EXP_PDTeech: "PD牙位图",
            EXP_PermanentTeethBitmap: "恒牙牙位图",
            EXP_Pupil: "瞳位图",
            ExtParagraphListStyle: "扩展段落列表样式",
            Fail: "失败",
            FailToAcceptChildElement_Parent_Child: "容器元素{0}不能接受子元素{0}。",
            FieldBackgroundText: "背景文本",
            FieldBorderText: "边框文本",
            FieldLabelText: "标签文本",
            FieldLinkList: "联动下拉列表",
            FieldSpecifyWidth: "固定宽度",
            FieldUnitText: "单位文本",
            FieldUserEditable: "用户不能直接编辑内容",
            FileBlockElement: "文档块",
            FilterValue: "数据过滤器",
            FirstLineIndent: "首行缩进量",
            Font: "字体",
            FontSize: "大小",
            FontStyle: "字体样式（粗体斜体下划线删除线）",
            Footer: "页脚",
            FooterForFirstPage: "首页页脚",
            ForbidEmpty: "数据不能为空。",
            ForeColor: "文本颜色",
            FormatBrush: "格式刷",
            FormViewMode: "表单视图模式",
            FreeEdit: "自由录入文本",
            GlobalEventTemplate: "全局事件模板",
            GridLinePreviewText: "DCWriter是南京都昌信息科技有限公司自主研发的专业的电子病历文档编辑器。",
            Gutter: "装订线",
            Header: "页眉",
            HeaderFooterDifferentFirstPage: "首页页眉页脚不同",
            HeaderForFirstPage: "首页页眉",
            HeaderRowStyle: "标题行模式",
            HiddenTableBorderJumpPrintPage: "续打页不打印单元格边框",
            HideHeaderFooter: "隐藏特定页数的页眉页脚",
            HorizontalLine: "横线元素",
            HTML_Preview: "HTML预览",
            HTML_Read: "读取HTML文件",
            HTML_Write: "保存HTML文件",
            HtmlFileFilter: "Html文件(*.htm,*.html)|*.htm;*.html",
            I25InvaliData: "I25:错误的数据，必须为偶数个数字字符。",
            IDRepeat_ID: "发现重复的元素ID值“{0}”。",
            ImageAdditionShape: "图片矢量图标记",
            ImageKeepWidthHeightRate: "设置固定长宽比",
            ImagePDF_Write: "Image.PDF格式",
            ImageResize: "图片拖拉缩放.",
            ImageSmoothZoom: "图片平滑缩放",
            ImageSource: "设置图片来源",
            ImageSurroundingsText: "设置图文混排",
            ImageWaterMark: "图片水印",
            Input: "输入",
            InputAttributeTableDelete: "请最少保留一行单元格",
            InsertElement_Content: "插入“{0}”",
            InsertElements_Count: "插入{0}个元素",
            InsertImage: "插入图片",
            InsertOCXControl: "插入OCX控件",
            InsertTableRowOrColumn: "插入表格列行",
            InsertWin32Control: "根据Win32句柄插入控件",
            InsertWinFormControl: "插入WinForm.NET控件",
            InsertWPFControl: "插入WPF控件",
            InvaliBarcodeStyle: "错误的条码样式.",
            InvalidateCommandName_Name_SimiliarNames: "错误的命令“{0}”，系统支持类似的命令有“{1}”",
            InvalidatePageSettings: "无效的页面设置，请仔细调整文档页面设置。",
            ISBNInvaliData: "ISBN:错误的数据，必须为9，10，12或13个数字，可能需要以“978”开头。",
            Items_Count: "有{0}个项目。",
            JAN13InvaliData: "JAN13:必须为数字，而且要“49”开头。",
            JS_DocumentFooter: "页脚",
            JS_DocumentFooterForFirstPage: "首页页脚",
            JS_DocumentHeadeForFirstPage: "首页页眉",
            JS_DocumentHeader: "页眉",
            JS_PageBreak: "分页符",
            JsonFilter: "*.json(试用)|*.json",
            JumpPrint: "续打",
            KBLibrary: "插入知识库",
            LabelAutoSize: "文本标签自动大小",
            LabelContactAction: "文本标签病程记录连接模式",
            LabelMultiLine: "文本标签多行显示",
            LabelNewText: "标签文本",
            LessThanMinLength_Length: "文本过短，不得少于 {0} 个字符。",
            LessThanMinValue_Value: "小于最小值 {0}。",
            LimitedFunction: "受限功能",
            LimitPasteTextLength: "粘贴文字字数限制",
            LimitPasteType: "可粘贴内容格式限制",
            LineInfo_PageIndex_LineIndex_ColumnIndex: "第{0}页 第{1}行 第{2}列。",
            LineSpacing: "行间距",
            ListEditMode: "下拉列表",
            LoadComplete_Size: "加载完成，共加载了{0}。",
            Loading_FileName: "正在加载文件“{0}”...",
            Loading_FileName_Format: "正在以 {1} 格式加载文件“{0}”...",
            LoadListItems_ProviderType_Name_Num: "使用“{0}”成功为“{1}”加载了{2}个列表项目。",
            LockFlagElement: "锁定标记元素",
            LogicalDeleteUserTrace: "留痕逻辑删除",
            MaxInputLength: "最大输入字符数",
            MD5ErrorForDecrypt: "加密数据时遇到MD5编码校验不通过的错误。",
            MegerCell: "合并拆分单元格",
            MHT_Write: "输出MHT格式",
            MirrorViewForCrossPage: "跨页内容镜像",
            MissProperty_Name: "没能找到属性“{0}”。",
            MoreThanMaxDemicalDigits: "小数位数超过上限，上限为{0}。",
            MoreThanMaxLength_Length: "文本过长，不得超过 {0} 个字符。",
            MoreThanMaxValue_Value: "超过最大值 {0}。",
            MSIInvaliData: "MSI:必须全部为数字字符。",
            MulitiPageContent: "多页不同内容",
            MultilevelAccessControl: "多级权限控制",
            MultiLevelTraceVisualization: "多级痕迹可视化设置",
            MultiLineAutoFixFontSize: "多行缩小字体自动填充",
            MustDateTimeType: "必须为日期时间格式。",
            MustDateType: "必须为日期格式。",
            MustInteger: "必须为整数。",
            MustMatch_Expression: "必须符合“{0}”格式。",
            MustNumeric: "必须为数值。",
            MustTimeType: "必须为时间格式。",
            NeedSetOwnerDocument: "需要首先设置OwnerDocument属性值。",
            NoDocument: "无文档。",
            NoImage: "没有图片",
            NoneEncryptView: "不加密显示",
            NormalViewMode: "普通视图模式",
            NotSupportInThisVersion_Name: "当前版本不支持功能“{0}”。",
            NotSupportSerialize_Format: "不支持以“{0}”格式进行存储。",
            NotSupportSerializeText_Format: "不支持以纯文本格式存储为“{0}”文件格式。",
            NotSupportTransparentEncrypt: "软件授权不包含透明加密功能，请联系软件供应商。",
            NotSupportWrite_Format: "不支持保存“{0}”格式的文件。",
            NumberedList: "数字列表样式",
            NumicalEditMode: "数字输入框",
            OffsetJumpPrint: "偏移续打",
            OLEDrag: "OLE拖拽",
            OwnerDocumentNUll: "文档元素尚未属于某个文档，无法执行操作。",
            PageBorderBackgroundFormat: "页面边框和底纹",
            PageBottomMargin: "下页边距",
            PageBreak: "分页符",
            PageIndexsForPrintBackgroundImage: "指定页码打印背景图片及水印",
            PageInfoAdvancedStyle: "高级显示样式",
            PageInfoAutoHeight: "自动高度",
            PageInfoElement: "基础功能",
            PageLandscape: "打印方向",
            PageLeftMargin: "左页边距",
            PageMargin: "页边距",
            PageRightMargin: "右页边距",
            PageSettingsBase: "纸张设置",
            PageStateLocked: "当前文档的分页状态被锁定，无法执行重新分页操作。请不要此时调用RefreshPages()/RefreshDocument()/UpdateDocumentView()/EditorRefreshView()等容易导致重新分页的函数。",
            PageTopMargin: "上页边距",
            PageViewMode: "页面视图模式",
            ParagraphFirstLineIndent: "首行缩进",
            ParagraphLeftIndent: "左缩进",
            ParagraphSpacing: "段落间距",
            PartialEncryptView: "内容加密显示",
            Paste: "粘贴",
            PDF_Write: "保存PDF格式",
            POSPrinter: "支持POS打印机",
            PostnetError: "Postnet条码文本必须是长度为5、6、9或11的数字。",
            PrintBoundSelection: "区域选择打印",
            PrintClean: "整洁打印",
            PrintMultiDocuments: "多文档合并模式",
            PrintMultiDocumentsMixed: "多文档混合模式",
            PrintPreview: "打印预览",
            PrintPreviewMultiColumn: "双页并排显示",
            PrintPreviewZoom: "打印预览缩放",
            PrintSelection: "打印被选择内容",
            PrintSettingForNotChecked: "勾选框打印状态设置",
            PrintSingleDocument: "单文档打印模式",
            PrintSpecifyPages: "打印指定页",
            PrintTrace: "留痕打印",
            PrintWithManualDuplex: "手动双面打印",
            PromptCannotEditComment_AuthorID: "不能编辑这个文档批注，因为该批注是”{0}“创建的。",
            PromptDisableOSClipboardData: "程序禁止从外部获得数据。",
            PromptForbitPasteMRID_ID_SourceID: "警告：当前文档关联的编号为“{0}”，而要粘贴的内容关联的编号为“{1}”，根据规范，禁止执行本次操作。",
            PromptJumpStartForSearch: "已到达文档的结尾处，是否继续从开始处搜索？",
            PromptMaxTextLengthForPaste_Length: "系统设置为粘贴或插入内容时不能接收超过{0}个字符。",
            PromptProtectedContent: "有内容受到保护，操作受到限制或无法执行。",
            PromptRejectFormat_Format: "系统被设定为拒绝“{0}”格式的数据。",
            PropertyCannotHasParameter_Name: "属性“{0}”不能有参数。",
            PropertyIsReadonly_Name: "属性“{0}”是只读的。",
            PropertyValueExpressions: "元素属性值表达式",
            QRCode: "二维码",
            ReadonlyCannotAcceptElementType_ParentType_ChildType: "{0}类型的元素不能接受{1}类型的子元素。",
            ReadonlyCanNotDeleteBackgroundText: "不能删除输入域的背景文本。",
            ReadonlyCanNotDeleteBorderElement: "不能删除输入域边界元素。",
            ReadonlyCanNotDeleteLastParagraphFlag: "任何时候都不能删除最后一个段落符号。",
            ReadonlyContainerReadonly: "容器元素设置为内容只读。",
            ReadonlyContentLocked: "内容被锁定。",
            ReadonlyContentProtect: "由于强制设置了内容保护而导致内容只读。",
            ReadonlyElementMarkUndeleteable_ID: "元素“{0}”被标记为不能删除。",
            ReadonlyFormViewMode: "由于控件处于表单模式而导致文档元素只读。",
            ReadonlyForSameLevelContent: "同等级的用户内容是只读的。",
            ReadonlyInputFieldUserEditable_ID: "输入域[{0}]的内容设置为不能直接修改。",
            ReadonlyLogicDeleted: "内容已经被逻辑删除了，无法再次删除。",
            ReadonlyLowPermissionLevel_CurLevel_NeedLevel: "权限等级不够，当前等级为{0}，所需等级{1}。",
            ReadonlyPermission: "由于授权控制而导致内容只读。",
            RecommentDocumentGridLine: "本命令已经淘汰了，请使用页面设置对话框中的文档网格线功能，或者设置document.PageSettings.DocumentGridLine属性。",
            RectangleChar: "矩形文字套圈",
            Redo: "重做",
            RequiredChecked: "必勾项设置",
            RowExistInTable: "表格行已经在表格中了。",
            RTF_Read: "读取RTF格式",
            RTF_Write: "保存RTF格式",
            RTFFileFilter: "RTF文件(*.rtf)|*.rtf",
            S25InvaliData: "S25:必须全部为数字字符。",
            SaveLongImage: "文档内容整体保存为长图片",
            SavePageImage: "文档内容分页保存为图片",
            ScreenSnapshort: "插入截屏",
            ScriptItems_Count: "{0}个脚本项目。",
            SearchReplaceNotFound: "无法找到您所查找的内容",
            SectionBorderBackgroundFormat: "设置边框和底纹",
            SectionElement: "插入文档节",
            SetCustomIcon: "自定义内部图标",
            SetResourceString: "自定义字符串资源",
            ShowFormButton: "显示表单小按钮",
            ShowInputFieldStateTag: "输入域内容状态标记",
            SingleLineAutoFixFontSize: "单行缩小字体自动填充",
            SpecifyPaste: "选择性粘贴",
            SpecifyRowHeight: "表格行固定高度",
            SpecharsHasHtml: "您传入的字符串中带有html标签。注：html标签不会生效",
            SubfieldMode: "自动分栏",
            SupSubscript: "上下标",
            SynchroServerTime: "服务器端时间同步",
            SystemAlert: "系统提示",
            SystemInternalError: "系统内部错误",
            TableCellDataSource: "单元格数据源绑定",
            TableCellGridLine: "单元格网格线",
            TableCellSlantSplitLineStyle: "单元格斜线",
            TableCellValueValidate: "单元格内容校验",
            TableCellVertialAlign: "单元格垂直对齐方式",
            TableElement: "表格基本功能",
            TableRowClolumnMastBePositiveInteger: "表格行数和列数必须为正整数，且不能为空",
            TableRowCanSplitByPageLine: "设置同行内容跨页",
            TableRowDataSource: "表格行数据源绑定",
            TableRowNewPageMode: "表格行设置强制分页",
            TerminalLine: "页面空白占位斜线",
            TerminalLine2: "页面空白占位反斜线",
            TerminalLineS: "页面空白占位S线",
            TerminalText: "页面空白占位字符",
            TextEditMode: "输入域文本输入模式",
            TextMustNotNull: "条码文本不得为空.",
            TextWaterMark: "文字水印",
            TimeEditMode: "输入域时间输入模式",
            TimeLineViewMode: "时间轴视图模式",
            TipTitle: "系统提示",
            TitleLevel: "大纲",
            TooltipText: "提示文本",
            TransparentEncrypt: "透明加密",
            TXT_Read: "读取TXT格式",
            TXT_Write: "保存TXT格式",
            TXTFileFilter: "TXT文件(*.txt)|*.txt",
            Undo: "撤销",
            UnknowDecryptError: "未知原因导致数据解密失败。",
            UPCAInvaliCountry: "UPCA:错误的国家代码。",
            UPCAInvaliData: "UPCA:必须为12个数字字符。",
            UPCEInvaliData: "UPCE:必须为8或12个数字字符,可能需要以0或1开头。",
            UPCS2InvaliData: "UPCS2:必须为2个数字。",
            UPCS5InvaliData: "UPCS5:必须为5个数字。",
            UserDeleteable: "用户不能直接删除",
            UserHandleReadFileEvent_FileName_Result: "用户处理了读文件\"{0}\"的事件，返回了{1}。",
            UserHandleWriteFileEvent_Result: "用户处理了写文件的事件，返回了{0}。",
            UserTrackList: "留痕列表",
            ValueBinding: "数据源绑定",
            ValueExpression: "数值表达式",
            ValueInvalidate: "数据校验错误",
            ValueInvalidate_Source_Value_Result: "对象“{0}”内容为“{1}”，数据校验错误“{2}”。",
            ValueValidate: "数据校验",
            ValueValidateFail: "数据校验失败.",
            ValueValidateOK: "数据校验成功.",
            ValueValidateWithCreateDocumentComments: "带文档批注的数据校验",
            VBScript: "VB或JS脚本",
            VideoElement: "视频元素",
            ViewXMLSource: "查看XML源代码功能",
            WarringPasteMRID_ID_SourceID: "警告：当前文档关联的编号为“{0}”，而要粘贴的内容关联的编号为“{1}”，根据规范，不建议执行本次操作，是否继续？",
            WatermarkRepeat: "水印重复展示",
            WhereToCopy: "复制到何处？",
            WhereToMove: "移动到何处？",
            XML_Read: "读取XML格式",
            XML_Write: "保存XML格式",
            XML2022Filter: "2022版XML文件(试用)|*.xml",
            XMLFilter: "XML文件|*.xml",
            XTextChartElement: "饼图及图表",
            Zoom: "视图无级缩放",
            ZOrderStyle: "文字之前或之后印章图片",
        };
        window.__SetDCStringResourceValues(window.__DCSR);
    },

    /**
     * DCWriter软件C#模块全部加载完毕，然后自动的初始化所有编辑器控件对象
     */
    StartGlobal: function () {
        // 设置默认字体
        DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "SetDefaultFont", 30, 128, "Microsoft Sans Serif");
        DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "SetDefaultFont", 0xf00, 0xfff, "Microsoft Himalaya");
        DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "SetDefaultFont", 19968,40869, "宋体");
        DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "SetDefaultFont", 8731, 8731, "Segoe UI Symbol");

        //DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "SetDefaultFont", 32, 65510, "Arial Unicode MS");

        // 设置替换字符
        DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "AddReplaceCharsForLoad", 10004/* ✔ */, 8730 /*√ */);

        DotNet.invokeMethod(
            window.DCWriterEntryPointAssemblyName,
            "SetGlobalServicePageUrl",
            DCTools20221228.GetServicePageUrl(null),
            DCTools20221228.GetClientID());

        WriterControl_Paint.RefreshStandardImageList();
        if (window.queryLocalFonts) {
            try {
                // 尝试获取本地字体名称列表
                window.queryLocalFonts().then(function (list2) {
                    if (list2.length > 0) {
                        var localFontNames = new Array();
                        for (const item2 of list2) {
                            if (localFontNames.indexOf(item2.family) == false) {
                                localFontNames.push(item2.family);
                            }
                        }
                        window.__DCLocalFontNames = localFontNames;
                    }
                    else {
                        window.__LocalFontsErrorFlag = true;
                    }
                }).catch((err) => {
                    window.__LocalFontsErrorFlag = true;
                    console.log(err);
                });
            }
            catch (ext) {
                window.__LocalFontsErrorFlag = true;
            }
        }
        window.__DCWriter5FullLoaded = true;
        var divs = document.querySelectorAll("div[dctype='WriterControlForWASM'],div[dctype='WriterPrintPreviewControlForWASM']");
        if (divs != null && divs.length > 0) {
            for (var iCount = 0; iCount < divs.length; iCount++) {
                var div = divs[iCount];
                if (div.__DCWriterReference == null) {
                    WriterControl_Event.RaiseControlEvent(div, 'EventBeforeCreateControl');
                    //判断是否存在属性autoCreateControl = false  DUWRITER5_0-861
                    var hasAutoCreateControlAttr = div.getAttribute("autocreatecontrol");//获取DIV元素上的autocreatecontrol属性的值
                    if (hasAutoCreateControlAttr != null) {
                        hasAutoCreateControlAttr = hasAutoCreateControlAttr.trim().toLowerCase();
                    }
                    // !== 不同类型不比较，且无结果，同类型才比较；
                    // 下面的判断表示autocreatecontrol属性不等于'false'还有'False'并且没有AboutControl时自动创建编辑器
                    if (hasAutoCreateControlAttr != 'false' && !div.AboutControl) {
                        WriterControl_Main.CreateWriterControlForWASM(div);
                    }
                }
            }
        }
        var temperatureDivs = document.querySelectorAll("div[dctype = 'DCTemperatureControlForWASM']");
        window.StartGlobal = true;
        if (temperatureDivs != null && temperatureDivs.length > 0) {
            for (var iCount = 0; iCount < temperatureDivs.length; iCount++) {
                var div = temperatureDivs[iCount];
                if (div.__DCWriterReference == null) {
                    div.CreateTemperatureControlForWASM = WriterControl_Main.CreateWriterControlForWASM;
                    if (typeof div.LoadStartGlobal == "function") {
                        div.LoadStartGlobal(div);
                    }
                }
            }
        }

        //产程图
        var flowDivs = document.querySelectorAll("div[dctype='DCFlowControlForWASM']");
        window.StartGlobal = true;
        if (flowDivs != null && flowDivs.length > 0) {
            for (var iCount = 0; iCount < flowDivs.length; iCount++) {
                var div = flowDivs[iCount];
                if (div.__DCWriterReference == null) {
                    console.log("创建流程控件");
                    div.CreateFlowControlForWASM = WriterControl_Main.CreateWriterControlForWASM;
                    if (typeof div.LoadStartGlobal == "function") {
                        div.LoadStartGlobal(div);
                    }
                }
            }
        }

        // 触发全局的DCWriter5Started事件
        var handler = window["DCWriter5Started"];
        if (typeof (handler) == "function") {
            handler();
        }

        //在此处创建后台编辑器文档对象的方法 wyc20240710
        window.CreateDCWriterControlDocument = function () {
            var div = document.createElement("div");
            var tempid = "hiddenDCWriterControlForDom";
            div.id = tempid;
            div.setAttribute("dctype", "WriterControlForWASM");
            div.setAttribute("style", "display:none");
            document.body.appendChild(div);
            WriterControl_Main.CreateWriterControlForWASM(tempid);
            var doc = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "CreateDCWriterControlDocument", div.__DCWriterReference);
            if (doc != null) {
                WriterControl_API.BindDCWriterDocument(doc);
                doc.OwnerControl = div;
            }
            //document.body.removeChild(div);
            return doc;

        };
    },

    /**
     * 创建编辑器实例
     * @param {string | HTMLDivElement} strContainerID 容器HTML元素编号或者对象
     */
    CreateWriterControlForWASM: function (strContainerID, type) {
        if (window.__DCWriter5FullLoaded != true) {
            // 编辑器创建失败
            if (!!window.WriterControl_OnLoadError && typeof (window.WriterControl_OnLoadError) == "function") {
                window.WriterControl_OnLoadError.call(strContainerID, strContainerID);
            }
            throw "DCWriter5的功能模块尚未全部加载，暂时无法创建编辑器控件。";
        }
        var strRuntimeID = strContainerID;
        if (typeof strContainerID == "object") {
            strRuntimeID = strContainerID.id;
            // 编辑器承载的元素可能在iframe中
            if (window.__DCWriterControls == null) {
                window.__DCWriterControls = new Array();
            }
            if (strContainerID.ownerDocument != document) {
                strRuntimeID = "!" + new Date().valueOf() + Math.random();
                window.__DCWriterControls[strRuntimeID] = strContainerID;
            }
            else if (DCTools20221228.IsNullOrEmptyString(strRuntimeID)) {
                // 没有提供ID值，则使用内部ID值
                strRuntimeID = "DC_" + new Date().valueOf() + Math.random();
                window.__DCWriterControls[strRuntimeID] = strContainerID;
            }
        }
        var rootElement = DCTools20221228.GetOwnerWriterControl(strContainerID);
        if (rootElement == null) {
            // 编辑器创建失败
            if (!!window.WriterControl_OnLoadError && typeof (window.WriterControl_OnLoadError) == "function") {
                window.WriterControl_OnLoadError.call(strContainerID, strContainerID);
            }
            return null;
        }

        //在此处进行RuleVisible值的判断
        rootElement.ruleVisible = rootElement.getAttribute('rulevisible');
        if (rootElement.ruleVisible != null && typeof rootElement.ruleVisible == 'string' && rootElement.ruleVisible.indexOf(',') > 0) {
            //对ruleVisible进行解析
            rootElement.ruleVisible = rootElement.ruleVisible.split(',');
            if (rootElement.ruleVisible && rootElement.ruleVisible.length == 2) {
                if (rootElement.ruleVisible[0].toLowerCase().trim() == 'false' && rootElement.ruleVisible[1].toLowerCase().trim() == 'false') {
                    rootElement.setAttribute('rulevisible', 'false');
                } else {
                    rootElement.setAttribute('rulevisible', 'true');
                }
            }
        }

        ////zhangbin 20230927 在此处记录所有通过此方法初始化的rootElement
        //if(typeof rootElement == 'object'){
        //    if(Array.isArray(DCTools20221228.CreateWriterControlArr) === false){
        //        DCTools20221228.CreateWriterControlArr = [];
        //    }
        //    DCTools20221228.CreateWriterControlArr.push(rootElement);
        //}
        //if (rootElement.id == null || rootElement.id.length == 0) {
        //    rootElement.id = "dcwriter_" + new Date().valueOf();
        //}
        rootElement.__BKImgStyleName = "__dcbkimg_" + parseInt(Math.random() * 1000000);
        DCTools20221228.LogTick("初始化控件" + rootElement.id);

        try {
            //存储加载文档花费毫秒，用于提供给性能页面
            let indexPerformanceTiming = {};
            if (window.localStorage.getItem('indexPerformanceTiming')) {
                indexPerformanceTiming = {
                    ...JSON.parse(window.localStorage.getItem('indexPerformanceTiming'))
                };
            }
            indexPerformanceTiming['myWriterControl'] = {
                ...(indexPerformanceTiming.myWriterControl || {}),
                [rootElement.id]: {
                    startTime: (new Date()).valueOf(),
                }
            };
            window.localStorage.setItem('indexPerformanceTiming', JSON.stringify(indexPerformanceTiming));
        } catch (error) {

        }

        /** 编辑器是否已经创建过了*/
        var rootElementIsCreated = rootElement.__DCWriterReference != null;

        if (type == "TemperatureControl") {
            //在此处直接判断是否为时间轴
            if (rootElement.getAttribute("dctype") == "DCTemperatureControlForWASM") {
                WriterControl_API.BindControlForTemperatureControlForWASM(rootElement, DotNet);
                rootElement.InnerRaiseEvent('EventTemperatureContorolOnLoad');
                rootElement._ctl = DotNet.invokeMethod(
                    window.DCWriterEntryPointAssemblyName,
                    "CreateWriterControlForWASM",
                    "d9671F95322F4A1A987BEF0DBC6B8B28");
                window.CreateTemperatureInit = rootElement.CreateTemperatureInit = WriterControl_DrawFu.CreateTemperatureInit;
                WriterControl_DrawFu.CreateTemperatureInit(rootElement);
                return;
            }
        } else if (type == "DCFlowControlForWASM") {
            //此处创建产程图
            if (rootElement.getAttribute("dctype") == "DCFlowControlForWASM") {
                WriterControl_API.BindControlForFlowControlForWASM(rootElement, DotNet);
                rootElement.InnerRaiseEvent('EventFlowContorolOnLoad');
                rootElement._ctl = DotNet.invokeMethod(
                    window.DCWriterEntryPointAssemblyName,
                    "CreateWriterControlForWASM",
                    "d9671F95322F4A1A987BEF0DBC6B8B28");
                window.CreateFlowControlInit = rootElement.CreateFlowControlInit = WriterContorl_FlowChart.CreateFlowControlInit;
                WriterContorl_FlowChart.CreateFlowControlInit(rootElement);
                return;
            }
        }

        var nativeControl = DotNet.invokeMethod(
            window.DCWriterEntryPointAssemblyName,
            "CreateWriterControlForWASM",
            strRuntimeID);
        if (nativeControl == null) {
            // 编辑器创建失败
            if (!!window.WriterControl_OnLoadError && typeof (window.WriterControl_OnLoadError) == "function") {
                window.WriterControl_OnLoadError.call(rootElement, rootElement);
            }
            return null;
        }
        rootElement.__DCWriterReference = nativeControl;
        nativeControl.invokeMethod("set_WASMBaseZoomRate", window.devicePixelRatio);
        rootElement.CheckDisposed = function () {
            if (rootElement.__DCDisposed == true) {
                throw "DCWriter编辑器控件{" + rootElement.id + "}已经被销毁了，无法使用。";
            }
        };
        if (rootElement.getAttribute("enabledlogapi") == "true") {
            // 记录API调用
            WriterControl_UI.StartAPILogRecord(rootElement,true);
            //rootElement.__DCWriterReference = new WriterControlExtPackage(rootElement, nativeControl);
            //rootElement.__DCWriterReference.EnabledLogAPI = true;
            //rootElement.DownloadAPIRecordData = function () {
            //    rootElement.__DCWriterReference.DownloadAPIRecordData();
            //};
            //rootElement.ClearAPIRecordData = function () {
            //    rootElement.__DCWriterReference.ClearAPIRecordData();
            //};
            //console.log("%cDCWriter启用API日志记录功能，本功能会消耗很多内存,减慢运行速度，谨慎使用。","color:white;background:red;padding:2px 6px;border-radius:3px;");
        }
        //else {
        //    rootElement.__DCWriterReference = nativeControl;
        //    rootElement.ClearAPIRecordData = rootElement.DownloadAPIRecordData = function () {
        //        window.alert("必须设置元素的EnabledLogAPI属性值为true才能启用本功能。")
        //    };
        //    //rootElement.ClearAPIRecordData = function () {
        //    //    rootElement.__DCWriterReference.ClearAPIRecordData();
        //    //};
        //}
        //var opts = ctl.invokeMethod("get_DocumentOptions");
        //var jsonTxt = JSON.stringify(opts);
        while (rootElement.firstChild != null) {
            rootElement.removeChild(rootElement.firstChild);
        }
        var strProductVersion = nativeControl.invokeMethod("GetProductVersion");
        //rootElement.setAttribute("dctype", "WriterControlForWASM");
        rootElement.setAttribute("dcversion", strProductVersion);
        console.log("DCWriter5软件发布时间:" + strProductVersion);
        if (rootElementIsCreated == false) {
            // 没有创建时，直接创建
            if (rootElement.getAttribute("dctype") == "WriterPrintPreviewControlForWASM") {
                WriterControl_API.BindControlForCommon(rootElement, rootElement.__DCWriterReference);
                // 添加打印预览控件的成员
                WriterControl_API.BindControlForWriterPrintPreviewControlForWASM(rootElement, rootElement.__DCWriterReference);
            } else {
                WriterControl_API.BindControlForCommon(rootElement, rootElement.__DCWriterReference);
                // 添加编辑器控件的成员
                WriterControl_API.BindControlForWriterControlForWASM(rootElement, rootElement.__DCWriterReference);
            }
        }

        // 加载系统配置
        for (var iCount = 0; iCount < rootElement.attributes.length; iCount++) {
            var attr = rootElement.attributes[iCount];
            rootElement.__DCWriterReference.invokeMethod(
                "LoadConfigByHtmlAttribute",
                attr.name,
                attr.value);
        }
        rootElement.DocumentOptions = nativeControl.invokeMethod("GetDocumentOptions");
        ////wyc20230701:补充刷新文档选项
        //if (typeof (rootElement.refreshDocumentOptions) === "function") {
        //    rootElement.refreshDocumentOptions.call(rootElement);
        //}

        //WriterControl_DateTimeControl.CreateCalendarCss(rootElement);

        //rootElement.removeAttribute("registercode");
        //zhangbin 20230201 判断是否存在自定义高度,如不存在,默认设置600px 
        if (rootElement.style.height == '') {
            rootElement.style.height = '100%';
        }
        //// 销毁控件
        //rootElement.DiposeControl = function () {
        //    if (rootElement.__DCWriterReference != null) {
        //        rootElement.__DCWriterReference.dispose();
        //        rootElement.__DCWriterReference = null;
        //    }
        //};
        // rootElement.style.overflowY = 'auto';

        // 移除事件监听,防止重复添加
        rootElement.ownerDocument.body.removeEventListener('mousedown', WriterControl_UI.OwnerDocumentBodyMouseDownFunc);
        // 此处的方法用于处理关闭下拉和光标的
        rootElement.ownerDocument.body.addEventListener('mousedown', WriterControl_UI.OwnerDocumentBodyMouseDownFunc);

        //禁用浏览器默认的右键
        rootElement.addEventListener('contextmenu', function (e) {
            e.stopPropagation();
            e.preventDefault();
            e.returnValue = false;
            return false;
        });

        rootElement.ownerDocument.body.oncut = function (e) {
            // 修复存在多个编辑器时无法剪切的问题【DUWRITER5_0-3618】
            var srcElement = e.srcElement || e.target;
            var rootElement = this.ownerDocument.WriterControl || DCTools20221228.GetOwnerWriterControl(srcElement);
            if (rootElement == null) {
                return;
            }
            // 20240313 [DUWRITER5_0-2031] lxy如果属性对话框存在，则不执行编辑器的剪切
            var dc_dialogContainer = rootElement.ownerDocument.getElementById('dc_dialogContainer');
            if (dc_dialogContainer) {
                return true;
            }
            //在document中判断同组是否存在选中，如果存在清除并将选中也清除
            var rootEle = rootElement;
            // var allShowEle = null;
            // //判断所在的编辑器是否被展示
            // var showEle = WriterControl_UI.GetCurrentWriterControl(function (allEle) {
            //     allShowEle = allEle;
            // }, rootElement);

            // if (allShowEle != null && allShowEle.length > 0) {
            //     if (allShowEle.indexOf(rootEle) < 0) {
            //         //如果不再展示 的元素中
            //         if (rootEle != showEle) {
            //             rootEle = showEle;
            //         }
            //     }
            // };
            if (rootEle == null || rootEle.ownerDocument.documentElement.contains(rootEle) == false) {
                // 编辑器元素不在文档中时，复制剪切走默认逻辑【DUWRITER5_0-2449】
                return;
            }
            //判断window上是否存在选中，如果存在，直接执行
            var sel = window.getSelection();
            //判断是否存在divCaret元素
            if (sel.isCollapsed === true && rootEle.__DCWriterReference.invokeMethod("HasSelection") == true) {
                var datas = '';
                var ref9 = rootEle.__DCWriterReference;
                if (ref9 != null) {
                    datas = ref9.invokeMethod(
                        "DoCut", false, false);
                }
                WriterControl_UI.SetClipboardData(datas, e, 'cut', rootEle);
                e.stopPropagation();
                e.preventDefault();
                e.returnValue = false;
            }
        };

        rootElement.ownerDocument.body.oncopy = function (e) {
            var srcElement = e.srcElement || e.target;
            var rootElement = this.ownerDocument.WriterControl || DCTools20221228.GetOwnerWriterControl(srcElement);
            if (rootElement == null) {
                return;
            }
            // 20240313 [DUWRITER5_0-2031] lxy如果属性对话框存在，则不执行编辑器的复制
            var dc_dialogContainer = rootElement.ownerDocument.getElementById('dc_dialogContainer');
            if (dc_dialogContainer) {
                return true;
            }
            //在document中判断同组是否存在选中，如果存在清除并将选中也清除
            var rootEle = rootElement;
            // var allShowEle = null;
            // //判断所在的编辑器是否被展示
            // WriterControl_UI.GetCurrentWriterControl(function (allEle) {
            //     allShowEle = allEle;
            // }, rootElement);

            // if (allShowEle != null && allShowEle.length > 0) {
            //     //循环allShowEle
            //     for (var i = 0; i < allShowEle.length; i++) {
            //         var isEle = allShowEle[i];
            //         if (isEle.__DCWriterReference.invokeMethod("HasSelection") == true) {
            //             rootEle = isEle;
            //             break;
            //         }
            //     }
            //     //if (allShowEle.indexOf(rootEle) < 0) {
            //     //    //如果不存再选中的元素
            //     //    rootEle = allShowEle[0];
            //     //}
            // };
            if (rootEle == null || rootEle.ownerDocument.documentElement.contains(rootEle) == false) {
                // 编辑器元素不在文档中时，复制剪切走默认逻辑【DUWRITER5_0-2449】
                return;
            }
            // 判断window上是否存在选中，如果存在，直接执行
            var sel = window.getSelection();
            // 在打印预览中只选中编辑器文本时处理复制逻辑，在其他场景下不处理【DUWRITER5_0-3894】
            if (rootEle.IsPrintPreview() == true) {
                // 打印预览控件
                var PrintPrewViewPageContainer = DCTools20221228.GetChildNodeByDCType(rootEle, "page-printpreview");
                if (sel && sel.isCollapsed === false && sel.rangeCount == 1 && typeof (sel.getRangeAt) == "function") {
                    var range = sel.getRangeAt(0);
                    if (range && range.commonAncestorContainer && typeof (range.cloneContents) == "function") {
                        var commonAncestorContainer = range.commonAncestorContainer;
                        if (PrintPrewViewPageContainer.contains(commonAncestorContainer) == true) {
                            // 打印预览控件触发EventBeforeCopy事件,data目前值为{IsPrintPreview: true}【DUWRITER5_0-3931】
                            if (rootEle.EventBeforeCopy != null && typeof (rootEle.EventBeforeCopy) == "function") {
                                var data = {
                                    IsPrintPreview: true
                                };
                                var CoptResult = rootElement.EventBeforeCopy(e, data);
                                if (CoptResult == false) {
                                    e.stopPropagation();
                                    e.preventDefault();
                                    return false;
                                }
                            }
                            /** 复制的内容 */
                            var clipboardText;
                            /** 剪切板数据对象 */
                            var clipboardData = e.clipboardData || window.clipboardData;
                            var clonedRange = range.cloneContents();
                            var divNode = document.createElement("div");
                            divNode.appendChild(clonedRange);
                            // 过滤掉不需要的内容
                            var removeNodes = divNode.querySelectorAll("[user-select='none'],style");
                            if (removeNodes.length > 0) {
                                for (var i = 0; i < removeNodes.length; i++) {
                                    var removeNode = removeNodes[i];
                                    removeNode.parentNode.removeChild(removeNode);
                                }
                            }
                            var textNodes = divNode.querySelectorAll("text");
                            if (textNodes.length > 0) {
                                var NowY = textNodes[0].getAttribute("y");
                                var text = "";
                                for (var i = 0; i < textNodes.length; i++) {
                                    var textNode = textNodes[i];
                                    var textNodeY = textNode.getAttribute("y");
                                    // 判断text标签是否在一行中
                                    // 如果不在一行中，则换行
                                    if (NowY != textNodeY) {
                                        NowY = textNodeY;
                                        text += "\n";
                                    }
                                    text += textNode.innerHTML.replace(/&nbsp;/g," ");
                                }
                                if (text != "") {
                                    clipboardText = text;
                                }
                            }

                            //在这里再加一个判读用于复制了一段字符串的情况
                            var hasText = divNode.childNodes;
                            if (hasText.length > 0) {
                                try {
                                    var newText = "";
                                    hasText.forEach(item => {
                                        if (item.nodeName != "#text") {
                                            throw new Error("");
                                        }
                                    })
                                    //纯文本的情况下执行此操作
                                    newText = divNode.innerHTML.replace(/&nbsp;/g, " ");
                                    if (newText != "" && !clipboardText) {
                                        clipboardText = newText;
                                    }
                                } catch (err) {

                                }
                            }
                            if (clipboardText && clipboardData) {
                                clipboardData.setData("text/plain", clipboardText);
                                divNode = null;
                                e.preventDefault();
                                return false;
                            }
                            divNode = null;
                        }
                    }
                }
            }

            if (sel && sel.isCollapsed === true && rootEle.__DCWriterReference.invokeMethod("HasSelection") == true) {
                //存在选中
                var datas = '';
                var ref9 = rootEle.__DCWriterReference;
                if (ref9 != null) {
                    datas = ref9.invokeMethod(
                        "DoCopy", false);
                }
                WriterControl_UI.SetClipboardData(datas, e, 'copy', rootEle);
                e.stopPropagation();
                e.preventDefault();
                e.returnValue = false;
            }
        };

        //开启监听
        /**
         * 
         * @param {any} message 错误文本
         * @param {any} source  错误所在资源
         * @param {any} lineno  错误所在行
         * @param {any} colno   错误所在列
         * @param {any} error   详细信息
         */
        window.onerror = function (message, source, lineno, colno, error) {
            //var hasFrameWork = false;
            //在此处判断source是否存在_framework
            //if (source != null && typeof source == 'string') {
            //    if (source.indexOf('_framework') >= 0) {
            //        hasFrameWork = true;
            //    }
            //}
            if (rootElement.EventOnError && typeof rootElement.EventOnError == 'function') {
                var options = {
                    message,
                    source,
                    lineno,
                    colno,
                    error
                };
                var needLogError = rootElement.EventOnError(options);
                if (needLogError === false) {
                    //这里为处理控制台实现显示错误，默认显示
                    return true;
                }
            }
        };

        //当整个窗口失去焦点的时候也需要失去焦点
        //判断是否为移动端
        if (!('ontouchstart' in rootElement.ownerDocument.documentElement)) {
            window.addEventListener('onblur', function () {
                var dropdown = rootElement.querySelector('#divDropdownContainer20230111');
                if (dropdown != null) {
                    dropdown.CloseDropdown();
                }
                //关闭表格下拉输入域
                var dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
                if (dropdownTable && dropdownTable.CloseDropdownTable) {
                    dropdownTable.CloseDropdownTable();
                }


                WriterControl_UI.HideCaret(rootElement);
            });
        };

        //zhangbin 20230607 当最外层包裹的div元素大小改变的监听事件
        rootElement.resizeObserver = new ResizeObserver(entries => {
            if (!('ontouchstart' in rootElement.ownerDocument.documentElement)) {
                var dropdown = rootElement.querySelector('#divDropdownContainer20230111');
                //当rootElement尺寸发生改变时.关闭下拉
                if (dropdown != null) {
                    dropdown.CloseDropdown();
                }
                //关闭表格下拉输入域
                var dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
                if (dropdownTable && dropdownTable.CloseDropdownTable) {
                    dropdownTable.CloseDropdownTable();
                }
                WriterControl_UI.HideCaret(rootElement);
                //在此处判断最外层包裹大小改变并处理标尺位置
                WriterControl_Rule.InvalidateView(rootElement, "hrule");
                WriterControl_Rule.InvalidateView(rootElement, "vrule");
                //此处判断如果元素在页面不显示则不执行大小改变监听
                if (rootElement.getClientRects) {
                    if (rootElement.getClientRects().length > 0) {
                        rootElement.SetAutoZoom(WriterControl_Event.InnerRaiseEvent, 'EventDocumentResize', true);
                    }
                } else {
                    rootElement.SetAutoZoom(WriterControl_Event.InnerRaiseEvent, 'EventDocumentResize', true);
                }
                if (rootElement.IsWriterPrintPreviewControlForWASM != true) {
                    WriterControl_Paint.HandleScrollView(rootElement, true);
                    WriterControl_UI.ReloadHostControls(rootElement);
                }
                //判断区域选择是否存在//判断是否为打印预览
                //if (rootElement.RectInfo && !rootElement.IsPrintPreview()) {
                //    rootElement.SetBoundsSelectionViewMode(false);
                //}
                // 在编辑器元素大小改变时修正区域选择打印蒙版位置
                if (rootElement.RectInfo && typeof (rootElement.RectInfo.AdjustBoundsSelectionStyle) == "function") {
                    rootElement.RectInfo.AdjustBoundsSelectionStyle();
                }
                // 在编辑器元素大小改变时修正自定义批注的位置
                if (rootElement && WriterControl_UI && typeof (WriterControl_UI.AdjustCustomDocumentCommentStyle) == "function") {
                    WriterControl_UI.AdjustCustomDocumentCommentStyle(rootElement);
                }
            }
            //WriterControl_Event.InnerRaiseEvent(rootElement, "EventDocumentResize");
        });
        rootElement.resizeObserver.disconnect(rootElement);
        //确保该元素身上只有一个事件监听
        rootElement.resizeObserver.observe(rootElement);

        rootElement.addEventListener("mousewheel", function (e) {
            if (e.altKey == false && e.ctrlKey == true && e.shiftKey == false) {
                // Ctrl+鼠标滚动则进行缩放操作
                var elements = WriterControl_UI.GetPageCanvasElements(this);
                var zoomRate = rootElement.__DCWriterReference.invokeMethod("get_ZoomRate");
                //for (var iCount = 0; iCount < elements.length; iCount++) {
                //    var element = elements[iCount];
                //    if (element.hasAttribute("native-width")) {
                //        zoomRate = parseFloat(element.width)
                //            / parseFloat(element.getAttribute("native-width"));
                //        break;
                //    }
                //}
                //if (isNaN(zoomRate)) {
                //    zoomRate = 1;
                //}
                var newZoomRate = zoomRate;
                if (e.wheelDelta > 0 || e.detail < 0) {
                    // 向上滚动
                    newZoomRate = zoomRate + 0.05;
                }
                else {
                    // 向下滚动
                    newZoomRate = zoomRate - 0.05;
                }
                rootElement.SetZoomRate(newZoomRate);
                e.preventDefault && e.preventDefault();
                return false;
            }
        }, false);

        rootElement.__DCWriterReference.invokeMethod(
            "Start",
            DCTools20221228.GetServicePageUrl(rootElement),
            DCTools20221228.GetClientID());

        window.setTimeout(async function () {
            //在此处加载时间控件样式
            WriterControl_DateTimeControl.CreateCalendarCss(rootElement);
            WriterControl_Event.RaiseControlEvent(rootElement, "OnLoad");
            //非预览控件时，再加载标尺
            if (rootElement.IsWriterPrintPreviewControlForWASM !== true) {
                rootElement.__DCWriterReference.invokeMethod("CheckForLoadDefaultDocument");
                WriterControl_Rule.UpdateRuleVisible(rootElement);
            } else {
                ////预览控件时，不创建标尺但需要占位
                //let pageContainer = rootElement.querySelector('div[dctype="page-container"]');
                //if (pageContainer && pageContainer.style) {
                //    pageContainer.style.paddingLeft = "24px";
                //    pageContainer.style.paddingTop = "24px";
                //    pageContainer.style.boxSizing = "border-box";
                //}
            }
            if (!rootElement.AboutControl || !rootElement.firstChild) {
                // 编辑器创建失败
                if (!!window.WriterControl_OnLoadError && typeof (window.WriterControl_OnLoadError) == "function") {
                    window.WriterControl_OnLoadError.call(rootElement, rootElement);
                }
            }
            if (navigator.clipboard && navigator.clipboard.read) {
                try {
                    const { state } = await navigator.permissions.query({
                        name: "clipboard-read",
                    });
                    if (state == 'prompt' || state == 'denied') {
                        await navigator.clipboard.read();
                    }
                } catch (err) {
                    //console.log(err)
                }
            }
            if (rootElement.attributes['showtoolbar'] && rootElement.attributes['showtoolbar'].value == 'true') {
                WriterControl_ToolBar.CreateToolBarControl(rootElement);
            }
        }, 1);

        //以下为编辑器父元素开启监听模式
        const rootElementParentNode = rootElement.parentNode; // 控件的父元素
        // 释放观察者模式的函数
        const disconnectObserver = () => {
            observer.disconnect();
            console.log('观察模式被释放');
        };
        // 监听执行的函数
        const handleMutation = (mutation) => {
            //判断变化的节点是否为子元素
            if (mutation.type === 'childList') {
                //判断删除节点中是否有编辑器dom
                const removedNodes = mutation.removedNodes;
                if (removedNodes.length > 0) {
                    for (let i = 0; i < removedNodes.length; i++) {
                        const item = removedNodes[i];
                        if (item && item.AboutControl) {
                            var AutoDispose = item.getAttribute('AutoDispose');
                            if (AutoDispose === 'true' || AutoDispose === true) {
                                item.Dispose && item.Dispose();
                            }
                        }
                    }
                }
                const childList = mutation.target.children;
                //没有子节点时释放观察者模式
                if (childList.length === 0) {
                    return disconnectObserver();
                }

                let flag = false;//用于标记剩余子节点中是否含有编辑器dom
                for (let i = 0; i < childList.length; i++) {
                    if (childList[i] && childList[i].AboutControl) {
                        flag = true;
                        break;
                    }
                }
                (flag === false) && disconnectObserver(); //子节点没有编辑器dom释放观察者模式
            }
        };
        const observer = new MutationObserver(function (mutationsList) {
            //此处方法会触发两遍，其中一次是子节点被脱离父节点，另一次是子节点被完全删除。这是 MutationObserver 的工作方式，
            for (const mutation of mutationsList) {
                handleMutation(mutation);
            }
        });
        observer.observe(rootElementParentNode, { childList: true, subtree: false });// subtree:是否监听多层子节点
    }
};

window.WriterControl_Main = WriterControl_Main;
window.WriterControl_Paint = WriterControl_Paint;
window.WriterControl_UI = WriterControl_UI;
window.WriterControl_Task = WriterControl_Task;
window.WriterControl_Rule = WriterControl_Rule;
window.WriterControl_Event = WriterControl_Event;
window.DCTools20221228 = DCTools20221228;
window.WriterControl_Dialog = WriterControl_Dialog;
window.WriterControl_DOMPackage = WriterControl_DOMPackage;

window.CreateWriterControlForWASM = WriterControl_Main.CreateWriterControlForWASM;
//window.CreateTemperatureControlForWASM = WriterControl_Main.CreateWriterControlForWASM;
window.WriterControl_EF = WriterControl_EF;
window.DisposeDCWriterDocument = WriterControl_API.DisposeDCWriterDocument;
// 入口点程序集名称
window.DCWriterEntryPointAssemblyName = "DCSoft.WASM";// "Microsoft.AspNetCore.Components.WebAssembly";// "DCSoft.WASM";
window.DCWriterStaticInvokeMethod = function (e, ...t) {
    //if (window.__CurrentDCWriterAPIRecorder != null && typeof (window.__CurrentDCWriterAPIRecorder.LogStaticMethod) == "function") {

    //}
    DotNet.invokeMethod(
        window.DCWriterEntryPointAssemblyName,
        e,
        ...t);
};