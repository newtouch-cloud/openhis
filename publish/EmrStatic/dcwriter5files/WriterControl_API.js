// 2025-1-14
/**
 * 编辑器自定义属性：
 * @UserDefinedOptions ["NotScrollWithCaret","true/false","为true时页面不再跟随光标跳转"]
 * @UserDefinedOptions ["FindReplaceNotFoundWrinText","true/false","为true时设置文案为:无法找到您所查找的内容"]
 * @UserDefinedOptions ["AutoMaticCorrectionRepeatID","true/false","为true时自动修正id"]
 * @UserDefinedOptions ["ClickCommentAreaFocusCaret","true/false","为true时编辑器属性点击批注部分编辑器不再自动获取焦点"]
 * @UserDefinedOptions ["DropdownListIsShowUsingSearchContent","true/false","设置搜索不到内容时，是否展示使用此文本按钮"]
 * @UserDefinedOptions ["PrintImageScale","Number","调用PrintAsImage时返回的图片质量,值越大质量越高(推荐5以内的数字)"]
 * @UserDefinedOptions ["MobileDisableAutoSoftKeyboard","true/false","移动端除了手动点击之外不会再获取焦点"]
 * @UserDefinedOptions ["MobileDisableSoftKeyboard","true/false","移动端是否需要禁止软键盘"]
 * @UserDefinedOptions ["ReadonlyAutoFocus","false/xandyaxis/yaxis","移动端表单模式下单击非输入域下不会主动让最近的输入域获取焦点,false和xandyaxis为控制x轴和y轴,yaxis为控制y轴"]
 * @UserDefinedOptions ["KnowledgeBaseOptions","KnowledgeBaseOptions","知识库的数据对象,必须是一个数组对象"]
 * @UserDefinedOptions ["IsDialogReadOnly","true/false","为true时设置所有属性对话框不能编辑"]
 * @UserDefinedOptions ["ShowElementViewHandle","true/false","为false时隐藏表格左上角的选中图标"]
 * @UserDefinedOptions ["NumberDisplayedInTibetan","true/false","为true时会把时间输入域，数值输入域，页码数值显示为藏文"]
 * @UserDefinedOptions ["DropdownListItemDeselectButton","true/false","默认值为true，为false时会隐藏下拉输入域的取消选中按钮"]
 * @UserDefinedOptions ["DropdownList_TwoColumnDisplay","true/false","为true时让单选下拉展示为两栏样式"]
 * @UserDefinedOptions ["FullWidthSpace","true/false","为true时正常半角space也会展示为全角空格"]
 * @UserDefinedOptions ["AlwaysDisplayCursor","true/false","为true时丢失焦点依据显示光标"]
 * @UserDefinedOptions ["PrintRuleOccupying","true/false","打印预览时，标尺是否占位。true:占位。false:不占位。默认为false"]
 * @UserDefinedOptions ["InputFileInnerListSourceNameTriggerQueryListItems","true/false","当静态下拉输入域中设置了字典来源（InnerListSourceName）并且带有静态选项（ListItems）时，激活输入域是否触发QueryListItems事件。true:触发，false:不触发，默认为false"]
 * @UserDefinedOptions ["PasteTablePreserveFormat","true/false","为true时当复制表格单元格数据并粘贴在单元格内的时候会保留表格样式而不是对单元格写入纯文本"]
 * @UserDefinedOptions ["ShowMediaOnMask", "true/false", "为true时点击音视频文件会扩展到全屏显示,通过关闭按钮或者esc键退出"]
 * @UserDefinedOptions ["DropDownListWidth", "number(单位:px)", "下拉列表宽度，数字类型，单位px"]
 * @UserDefinedOptions ["DropDownListMinWidth", "number(单位:px)", "下拉列表最小宽度，数字类型，单位px"]
 * @UserDefinedOptions ["DropDownListMaxWidth", "number(单位:px)", "下拉列表最大宽度，数字类型，单位px"]
 * @UserDefinedOptions ["DropDownListMaxHeight", "number(单位:px)", "下拉列表项最大高度，数字类型，单位px"]
 * @UserDefinedOptions ["DCDorpDownAllowKeyBoardEntry", "true/false", "下拉输入域是否允许键盘输入英文、数字等"]
 * @UserDefinedOptions ["TablehHeaderFixed", "true/false", "当普通视图模式下是否固定表头"]
 * @UserDefinedOptions ["IsUsePasteDiolog", "true/false", "ctrl+v粘贴时显示一个粘贴对话框，用于选择粘贴文本的处理方式"]
 * @UserDefinedOptions ["BoundsSelectionViewModeUseNewLogic", "true/false", "是否使用新逻辑的区域选择打印，和第四代编辑器类似"]
 * @UserDefinedOptions ["HeaderFooterSelect", "true/false", "打印预览时是否允许选中和复制页眉页脚内容"]
*/

/**
 * 编辑器事件：
 * @WriterEventFunction ["EventElementMouseDblClick","rootElement, args","元素的鼠标双击事件"]
 * @WriterEventFunction ["EventShowContextMenu","eventSender, args","右键菜单事件"]
 * @WriterEventFunction ["EventUpdateToolarState","eventSender, args","处理编辑器控件的更新工具条按钮状态事件"]
 * @WriterEventFunction ["EventBeforeCopy","e, copydata","复制前事件"]
 * @WriterEventFunction ["EventBeforePaste","copydata","粘贴前事件"]
 * @WriterEventFunction ["EventBeforePrint","printDoc","打印前事件"]
 * @WriterEventFunction ["EventAfterInsertSubDocuments","boolean","病程回调函数"]
 * @WriterEventFunction ["EventInsertObject","eventSender, eventArgs","拖拽事件"]
 * @WriterEventFunction ["EventCanInsertObject","eventSender, eventArgs","拖拽插入元素触发,配合EventInsertObject使用"]
 * @WriterEventFunction ["EventPageResize","args","编辑器缩放时触发"]
 * @WriterEventFunction ["EventOnError","message, source, lineno, colno, error","检测到错误时触发"]
 * @WriterEventFunction ["EventBeforeExecuteCommand","CommandName","获取命令的名称，可以阻止命令的执行"]
 * @WriterEventFunction ["EventSaveDocumentToStringValidate","validate","保存文档格式校验的错误回调"]
 * @WriterEventFunction ["QueryListItems","ele, args","设置下拉框数据，下拉输入域展开时触发"]
 * @WriterEventFunction ["EventKnowledgeBase","container","知识库自定义组件盒子，参数是知识库外壳，提供给客户自定义知识库样式逻辑等"]
 * @WriterEventFunction ["EventKnowledgeBaseOnClick","data","知识库的选择事件，参数是选中的知识库对象"]
 * @WriterEventFunction ["EventKnowledgeBaseSearchOnInput","text, callback","知识库的搜索事件，text是搜索文本，callback是回调函数用于渲染查找的数据内容，格式和知识库数据格式一样"]
 * @WriterEventFunction ["EventCanvasCountChange","rootElement","编辑器新增页或删除页事件"]
 * @WriterEventFunction ["EventAfterDocumentDraw","rootElement","编辑器渲染完成事件"]
 * @WriterEventFunction ["EventQueryTableListDataPageChanged","pageIndex,ChangedPageTabelData","下拉输入域表格分页"]
 * @WriterEventFunction ["EventQueryTableListDataSearch","searchText, TabelDataSearchForReset","下拉输入域表格查找"]
 * @WriterEventFunction ["EventWriterControlBlur","","编辑器失去焦点事件"]
 * @WriterEventFunction ["EventWriterControlFocus","","编辑器获得焦点事件"]
 * @WriterEventFunction ["EventPrintPreviewContextMenu","rootElement, eventArgs","打印预览时，右击触发的事件，可以用来制作打印预览的右键菜单"]
 * @WriterEventFunction ["SetPreviewContextMenu","rootElement, eventArgs","打印预览时，右击触发的事件，可以用来制作打印预览的右键菜单"]
 * @WriterEventFunction ["EventInsertMultipleCheckBoxOrRadioAfter","options","对话框插入一组多选或单选框后触发的事件"]
 * 
 * 
*/

/**
 * @name example_function
 * @type function （类型：function方法，Property属性）
 * @apinameZh 例子（中文释义）
 * @param ["参数1","number","参数释义","参数默认值","参数描述","是否必填"]
 * @returns ["返回值1","参数类型如：number、string、object...","返回值释义"]
 * @change ["接口修改时间如：2023-09-02","修改内容描述","修改人" ]
 * @classification file
 * @describe 本接口适用于...(接口描述)
 */

/***************************************************************/
/**
    关于接口注释-分类(classification)
    file: "文件",
    editformat: "编辑与格式",
    view: "视图",
    structuralelement: "结构化元素",
    table: "表格",
    subdoc: "病程",
    vestige: "痕迹",
    cursor: "光标",
    datasource: "数据源",
    attribute: "自定义属性",
    download: "下载",
    print: "打印"
 **/

"use strict";

import { DCBinaryReader, DCTools20221228 } from "./DCTools20221228.js";
import { WriterControl_Paint } from "./WriterControl_Paint.js";
import { WriterControl_Print } from "./WriterControl_Print.js";
import { WriterControl_Rule } from "./WriterControl_Rule.js";
import { WriterControl_UI } from "./WriterControl_UI.js";
import { WriterControl_IO } from "./WriterControl_IO.js";
import { WriterControl_Dialog } from "./WriterControl_Dialog.js";
import { WriterControl_Event } from "./WriterControl_Event.js";
import { PageContentDrawer } from "./PageContentDrawer.js";
import { TemperatureControl_Data, TemperatureControl_XMLToJSON } from "./TemperatureControl_Data.js";
import { WriterControl_DrawFu } from "./WriterControl_DrawFu.js";
import { TemperatureControl_Designer } from "./TemperatureControl_Designer.js";
import { jspdf } from "./WriterControl_DrawD3.js";
import { WriterControl_FontList } from "./WriterControl_FontList.js";
import { WriterControl_ToolBar } from "./WriterControl_ToolBar.js";
import { WriterControl_TrendChart } from "./WriterContorl_TrendChart.js";
import { WriterContorl_FlowChart } from "./WriterContorl_FlowChart.js";

/**
 * 用于接口调用时计算调用时间，并触发EventInterfaceLog事件
 * @param {HTMLElement} rootElement 编辑器根元素
 * @param {string} apiName 接口名称
 * @param {string} startTime 开始时间
 * @param {string} DefineProperty 是否为属性
 */
let DCEventInterfaceLogFunction = function (rootElement, apiName, startTime, DefineProperty = false) {
    var endTime = new Date().valueOf();
    var y = startTime.getFullYear();
    var m = startTime.getMonth() + 1;
    m = m < 10 ? ('0' + m) : m;
    var d = startTime.getDate();
    d = d < 10 ? ('0' + d) : d;
    var h = startTime.getHours();
    h = h < 10 ? ('0' + h) : h;
    var minute = startTime.getMinutes();
    minute = minute < 10 ? ('0' + minute) : minute;
    var second = startTime.getSeconds();
    second = second < 10 ? ('0' + second) : second;
    var newStartTime = y + '-' + m + '-' + d + ' ' + h + ':' + minute + ':' + second;
    if (rootElement == null || !rootElement.isEventInterfaceLogFunction) { return; }
    if (rootElement != null && !!rootElement.EventInterfaceLog && typeof (rootElement.EventInterfaceLog) == "function") {
        rootElement.EventInterfaceLog.call(rootElement, apiName, endTime - startTime.valueOf(), newStartTime, DefineProperty);
    }
};

/**
 * 用于判断以下模式：阅读、预览、续打、区域选择时，不能修改dom
 * @param {HTMLElement} rootElement 编辑器根元素
 */
let DCGetAllowOperateDOM = function (rootElement) {
    let flag = true;
    var ReadViewMode = rootElement.ReadViewMode;//阅读模式
    var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
    var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
    var RectInfo = rootElement.RectInfo;//区域选择
    //当前存在其中一个模式，即不可修改dom，返回false
    if (ReadViewMode || IsPrintPreview || ExtViewMode || RectInfo) {
        return false;
    }
    return flag;
};

export let WriterControl_API = {

    //************************************************************************************
    //************************************************************************************

    /**
    * 常规的初始化编辑器根元素，适用于编辑器和打印预览控件
    * @param {HTMLElement} rootElement 编辑器根元素
    * @param {any} refDCWriter
    */
    BindControlForCommon: function (rootElement, refDCWriter) {
        rootElement.__DCWriterReference = refDCWriter;
        /**
        * @name SetStringResources
        * @type Function
        * @classification file
        * @apinameZh 设置特定名称的字符串资源
        * @param ["json","object","字符串资源对象","","",true]
        */
        rootElement.SetStringResources = function (json) {
            var startTime = new Date();
            if (json != null) {
                window.__SetDCStringResourceValues(json);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetStringResources', startTime);
        };

        rootElement.ReportStaticFieldValues = function () {
            rootElement.__DCWriterReference.invokeMethod("ReportStaticFieldValues");
        };
        /**
         * @name SetFontSource
         * @type Function
         * @classification file
         * @apinameZh 设置字体文件名，该功能慎用，特别是PC浏览器。
         * @param ["strFontName","string","字体名称","","",true]
         * @param ["strUrl" ,"string" ,"字体文件下载地址" ,"",true]
         * @describe 只有确认客户端可能不支持指定的字体时才需要调用该功能。
         */
        rootElement.SetFontSource = function (strFontName, strUrl) {
            // 判断是否为移动端，如果是才执行
            if ('ontouchstart' in rootElement.ownerDocument.documentElement) {
                WriterControl_Paint.SetFontSource(strFontName, strUrl);
            }
        };
        /**
        * @name IsAPILogRecord
        * @type function
        * @apinameZh 编辑器是否正在记录API日志
        * @returns ["result","boolean","是否正在记录API日志"]
        * @change ["2023-09-14","改进API日志","yyf" ]
        * @describe 用于判断接口是否正在记录日志
        * @classification file
        */
        rootElement.IsAPILogRecord = function () { return WriterControl_UI.IsAPILogRecord(rootElement); };

        /**
         * @name StartAPILogRecord
         * @type function
         * @apinameZh 开始记录API日志的功能
         * @returns ["result","boolean","开始记录API日志是否成功"]
         * @change ["2023-09-14","改进API日志","yyf" ]
         * @classification file
         */
        rootElement.StartAPILogRecord = function () { return WriterControl_UI.StartAPILogRecord(rootElement); };
        /**
         * @name StopAPILogRecord
         * @type function
         * @apinameZh 停止记录API日志的功能，清空所有数据。
         * @returns ["result","boolean","停止记录API日志是否成功"]
         * @change ["2023-09-14","改进API日志","yyf" ]
         * @classification file
         */
        rootElement.StopAPILogRecord = function () { return WriterControl_UI.StopAPILogRecord(rootElement); };
        /**
         * @name ClearAPILogRecordData
         * @type function
         * @apinameZh 清空API日志数据
         * @returns ["result","boolean","清空API日志数据是否成功"]
         * @change ["2023-09-14","改进API日志","yyf" ]
         * @classification file
         */
        rootElement.ClearAPILogRecordData = function () { return WriterControl_UI.ClearAPILogRecordData(rootElement); };
        /**
         * @name DownloadAPIRecordData
         * @type function
         * @apinameZh 下载API日志数据
         * @returns ["result","boolean","下载API日志数据是否成功"]
         * @change ["2023-09-14","改进API日志","yyf" ]
         * @classification file
         */
        rootElement.DownloadAPIRecordData = function () { return WriterControl_UI.DownloadAPIRecordData(rootElement); };
        /**
       * @name GetloadAPIRecordData
       * @type function
       * @apinameZh 获取API日志数据
       * @param ["callBack","function","回调函数",null,"用于返回api日志",true]
       * @returns ["result","object","获取API日志数据"]
       * @change ["2024-03-28","增加接口","lixinyu" ]
       * @describe 移动设备无法下载API日志时,可以使用此接口获取日志数据
       * @classification file
       */
        rootElement.GetloadAPIRecordData = function (callBack = null) {
            if (callBack) {
                WriterControl_UI.DownloadAPIRecordData(rootElement, callBack);
            }
        };

        /**
         * @name PlayAPILogRecords
         * @type function
         * @apinameZh 播放API日志
         * @change ["2023-09-14","改进API日志","yyf" ]
         * @classification file
         */
        rootElement.PlayAPILogRecords = function () { return WriterControl_UI.PlayAPILogRecords(rootElement); };
        /**
        * @name PrintAsPDF
        * @type function
        * @apinameZh 打印为PDF文件
        * @param ["options","object","打印选项","","",true]
        * @param ["callBack","function","操作成功后的回调函数，参数为包含PDF的blob对象，如果未指定回调函数则下载文件。","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @classification print
        */
        rootElement.PrintAsPDF = function (options, callBack) {
            var startTime = new Date();
            var result = WriterControl_Print.PrintAsPDF(rootElement, options, callBack);
            DCEventInterfaceLogFunction(rootElement, 'PrintAsPDF', startTime);

            return result;
        };

        /**
         * @name FocusElementById
         * @type function
         * @apinameZh 让指定文档元素获得输入焦点
         * @param ["id","string","元素编号","null","元素编号的id",true]
         * @param ["scrollMode","string","跳转模式（不支持在预览控件中使用）","middle","top/middle/bottom",true]
         * @returns ["result","boolean","操作是否成功"]
         * @classification cursor
         * @change ["2024-09-02","增加第二个参数，用于指定滚动位置","lxy" ]
         */
        rootElement.FocusElementById = function (id, scrollMode = "Middle") {
            var startTime = new Date();
            if (rootElement.IsWriterPrintPreviewControlForWASM) {
                var info = rootElement.__DCWriterReference.invokeMethod("GetElementLayoutInfoByID", id);
                if (info != null) {
                    var pContainer = WriterControl_Print.GetPrintPrewViewPageContainer(rootElement);;
                    if (pContainer != null) {
                        var allPagesTop = 0;
                        for (var node = pContainer.firstChild; node != null; node = node.nextSibling) {
                            if (node.nodeName == "svg" && node.PageIndex == info.PageIndex) {
                                //加上目标页的Top值
                                allPagesTop += info.Top;
                                pContainer.scrollTo({
                                    top: allPagesTop,
                                    behavior: "smooth"
                                });
                                return true;
                            } else {
                                //计算目标页前面的页的滚动总高度
                                allPagesTop += parseFloat(node.clientHeight) + (parseFloat(node.style.marginTop) || 5) + (parseFloat(node.style.marginBottom) || 5) + (parseFloat(node.style.borderTopWidth) || 1) + (parseFloat(node.style.borderBottomWidth) || 1);
                            }
                        }
                    }
                }
            }
            else {
                // //处理在聚焦前把光标显示出来
                // if (rootElement.oldCaretOption) {
                //     WriterControl_UI.ShowCaret(
                //         rootElement.oldCaretOption.containerID,
                //         rootElement.oldCaretOption.intPageIndex,
                //         rootElement.oldCaretOption.intDX,
                //         rootElement.oldCaretOption.intDY,
                //         rootElement.oldCaretOption.intWidth,
                //         rootElement.oldCaretOption.intHeight,
                //         rootElement.oldCaretOption.bolVisible,
                //         rootElement.oldCaretOption.bolReadonly
                //     );
                // }

                //先给一个临时变量，用于记录跳转模式，并用于是否需要矫正位置
                rootElement.FocusElementByIdScrollMode = scrollMode;
                //在调用接口
                let result = rootElement.__DCWriterReference.invokeMethod("FocusElementById", id);
                if (!result) {
                    //接口调用失败时要及时删除此变量
                    if (rootElement.FocusElementByIdScrollMode) {
                        delete rootElement.FocusElementByIdScrollMode;
                    }
                }
                DCEventInterfaceLogFunction(rootElement, 'FocusElementById', startTime);
                return result;
            }
            return false;
        };
        /**
         * @name LocalPrintDocuments
         * @type function
         * @apinameZh 打印到前端服务器
         * @param ["参数1","object","options 打印选项JSON对象","","本参数需要注意。。。",true]
         * @param ["参数2","callBack","操作成功后的回调函数，参数为服务器返回值。"]
         * @param ["参数3","getprinter","启用获取打印机列表功能，利用监听程序获取本地打印机列表"]
         * @change ["2023-05-09","修改了接口名称","wyc" ]
         * @change ["2024-07-17","新增参数启用获取本地打印机列表","wyc" ]
         * @describe 必须设置编辑器控件的PrintServerPageUrl属性。
         * @returns ["result","boolean","操作是否成功"]
         * @classification print
         */
        rootElement.LocalPrintDocuments = function (options, callBack, getprinter = false) {
            var startTime = new Date();
            var result = WriterControl_Print.PrintToServer(rootElement, null, options, callBack, getprinter);
            DCEventInterfaceLogFunction(rootElement, 'LocalPrintDocuments', startTime);
            return result;
        };
        /**
         * @name PrintAsHtml
         * @type function
         * @apinameZh 打印为HTML文件
         * @param ["options","object","打印选项",null,"{isPrint 是否需要打印 默认下载当为true/'true'时打印，printCallback 值}",true]
         * @param ["callBack","function","操作成功后的回调函数，参数为包含HTML字符串，如果未指定回调函数则下载文件。",false]
         * @returns ["result","Boolen","操作是否成功"]
          * @classification print
         */
        rootElement.PrintAsHtml = function (options, callBack, isPrint) {
            var startTime = new Date();
            var result = WriterControl_Print.PrintAsHtml(rootElement, options, callBack);
            DCEventInterfaceLogFunction(rootElement, 'PrintAsHtml', startTime);
            return result;
        };

        /**
        * @name DownLoadFile
        * @type function
        * @apinameZh 下载文件
        * @param ["strFormat","string","文件格式","","",true]
        * @param ["strFileName","string","指定的文件名","","",true]
        * @param ["callBack","function","获得文件内容的回调函数","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @classification download
        */
        rootElement.DownLoadFile = function (strFormat, strFileName, callBack) {
            if (strFormat === 'txt') {
                strFormat = 'text';
            } else if (strFormat === 'longimag' || strFormat === 'longimage') {
                strFormat = 'longimg';
            }
            var startTime = new Date();
            let result = WriterControl_IO.DownLoadFile(rootElement, strFormat, strFileName, callBack);
            DCEventInterfaceLogFunction(rootElement, 'DownLoadFile', startTime);

            return result;
        };

        /**
        * @name GetZoomRate
        * @type function
        * @apinameZh 获取缩放比率
        * @returns ["result","number","缩放比率"]
        * @classification view
        */
        rootElement.GetZoomRate = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("get_ZoomRate");
            DCEventInterfaceLogFunction(rootElement, 'GetZoomRate', startTime);
            return result;
        };

        /**
        * @name SetZoomRate
        * @type function
        * @apinameZh 设置缩放比率
        * @param ["newZoomRate","function","新的缩放比率，必须在0.1到5之间","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @classification view
        */
        rootElement.SetZoomRate = function (newZoomRate) {
            var startTime = new Date();
            //只要调用这个方法，直接把自动缩放AutoZoom属性设为false
            var hasAutoZoom = rootElement.getAttribute('autozoom');
            if (hasAutoZoom != null && (hasAutoZoom == 'true' || hasAutoZoom == 'True')) {
                rootElement.setAttribute('autozoom', 'false');
                rootElement.style.removeProperty('width');
                rootElement.style.removeProperty('flex');
                rootElement.style.removeProperty('align-self');
            }
            let result = WriterControl_UI.EditorSetZoomRate(rootElement, newZoomRate);
            //判断区域选择是否存在
            if (rootElement.RectInfo) {
                if (rootElement.IsPrintPreview()) {
                    var pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                    if (pageContainer) {
                        rootElement.RectInfo.printPreviewFun(pageContainer, newZoomRate);
                    }
                } else {
                    rootElement.SetBoundsSelectionViewMode(false);
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'SetZoomRate', startTime);
            return result;
        };

        /**
       * @name SetPageLayoutMode
       * @type function
       * @apinameZh 设置页面排版模式
       * @param ["strMode","string","排版类型","","可以为SingleColumn,MultiColumn,Horizontal",true]
       * @describe 可以为SingleColumn,MultiColumn,Horizontal
       * @classification view
       */
        rootElement.SetPageLayoutMode = function (strMode) {
            var startTime = new Date();
            WriterControl_UI.EditorSetPageLayoutMode(rootElement, strMode);
            DCEventInterfaceLogFunction(rootElement, 'SetPageLayoutMode', startTime);
        };

        /**
        * @name MoveToPage
        * @type function
        * @apinameZh 滚动视图到指定页
        * @param ["intPageIndex","number","页码",null,"从0开始的页码",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-09-02","修改了接口名称","lxy" ]
        * @change ["2023-09-01","修改了接口名称1111","lxy" ]
        * @classification view
        */
        rootElement.MoveToPage = function (intPageIndex) {
            var startTime = new Date();
            let result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = WriterControl_UI.MoveToPage(rootElement, intPageIndex, true);
            }
            DCEventInterfaceLogFunction(rootElement, 'MoveToPage', startTime);
            return result;
        };

        /**
         * @name GetAllPageIndexs
         * @type function
         * @apinameZh 获得所有的页码数组
         * @returns ["result","Array","页码数组"]
          * @classification file
         */
        rootElement.GetAllPageIndexs = function () {
            var startTime = new Date();
            let result = WriterControl_UI.GetAllPageIndexs(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'GetAllPageIndexs', startTime);
            return result;
        };
        /**
        * @name PrintDocument
        * @type function
        * @apinameZh 打印整个文档
        * @param ["options","any","打印用的选项","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @classification print
        * @describe 五代区域选择打印不支持传递参数，直接调用ctl.PrintDocument()即可
        */
        rootElement.PrintDocument = function (options) {
            var startTime = new Date();
            let result = WriterControl_Print.Print(rootElement, options);
            DCEventInterfaceLogFunction(rootElement, 'PrintDocument', startTime);
            return result;
            //rootElement.__DCWriterReference.invokeMethod("PrintDocument");
        };

        /**
         * @name TemplatePrintingWithCells
         * @type function
         * @apinameZh HTML单元格套打；因为SVG无法确定之前打印的内容，所以单元格套打使用rootElement.GetPrintPreviewHTML获取的HTML内容进行套打
         * @classification print
         * @param ["doc","#document","打印的document","true","",""]
         * @param ["hiddenCellData","null || Array","需要隐藏的单元格数据","true","",""]
         * @param ["specifyPageIndexString","boolean","指定页号","true","",""]
         * @returns ["result","Array | false","打印过的单元格数据,或者取消隐藏"]
         */
        rootElement.TemplatePrintingWithCells = function (doc, hiddenCellData, EnableHide) {
            var result = WriterControl_Print.TemplatePrintingWithCells(doc, hiddenCellData, EnableHide);
            return result;
        };

        /**
         * @name SelectSubDocumentByID
         * @type function
         * @apinameZh 根据编号定位病程
         * @param ["id","string","病程id","","",true]
         * @returns ["result","boolean","操作是否成功"]
         * @classification subdoc
         */
        rootElement.SelectSubDocumentByID = function (id) {
            var startTime = new Date();
            let result = false;
            if (rootElement.IsPrintPreview() == false) {
                // 编辑模式
                result = rootElement.__DCWriterReference.invokeMethod("SelectSubDocumentByID", id);
                if (result) {
                    //zhangbin 20231122 DUWRITER5_0-1256问题需要定位是保证在开始位置
                    //判断当前输入域的位置如果在滚动条的最后
                    var pageContainer = rootElement.querySelector('[dctype="page-container"]');
                    //编辑器内部页面存在滚动
                    if (pageContainer && pageContainer.scrollHeight > pageContainer.clientHeight) {
                        //获取病程的属性 TopInOwnerPage  OwnerPageIndex
                        var subAttr = rootElement.GetElementProperties(id);
                        var allCanvas = pageContainer.querySelectorAll('canvas[dctype="page"]');
                        if (subAttr && typeof subAttr.OwnerPageIndex == "number") {
                            // 修复放大倍数影响定位病程【DUWRITER5_0-3017】
                            var ZoomRate = rootElement.GetZoomRate();
                            var thisPage = allCanvas[subAttr.OwnerPageIndex];
                            if (thisPage) {
                                var topInOwnerPage = parseFloat((subAttr.TopInOwnerPage / 300 * 96.00001209449 * ZoomRate).toFixed(2));
                                var leftInOwnerPage = parseFloat((subAttr.LeftInOwnerPage / 300 * 96.00001209449 * ZoomRate).toFixed(2));
                                pageContainer.scrollTo(thisPage.offsetLeft + leftInOwnerPage, thisPage.offsetTop + topInOwnerPage);
                            }
                        }
                    }
                }
            } else {
                // 打印预览模式
                var PrintPreviewPageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                if (PrintPreviewPageContainer && typeof (id) == "string") {
                    var AllSubDocumentIDs = rootElement.GetAllSubDocumentIDs();
                    // 添加SelectSubDocumentByID在打印预览模式下的定位病程问题【DUWRITER5_0-3967】
                    if (AllSubDocumentIDs.includes(id) == true) {
                        // 存在该ID的病程
                        var subNode = PrintPreviewPageContainer.querySelector("[id='" + id + "']");
                        var allSvg = PrintPreviewPageContainer.querySelectorAll('svg[dctype="page"]');
                        if (!subNode) {
                            // 修复SelectSubDocumentByID打印预览下定位未渲染病程的问题【备注：如果页面过多，第一次定位比较后面的病程，可能会慢】
                            for (var i = 0; i < allSvg.length; i++) {
                                var svgNode = allSvg[i];
                                if (svgNode && svgNode._isRendered == false) {
                                    // 获取到未渲染的svg页面，进行渲染svg元素
                                    WriterControl_Print.InnerDrawOnePage(svgNode, true);
                                    svgNode._isRendered = true;
                                    // 重新查找该ID的病程
                                    subNode = svgNode.querySelector("[id='" + id + "']");
                                }
                                if (subNode) {
                                    // 找到后，跳出循环
                                    break;
                                }
                            }
                        }
                        // // 获取病程的属性 OwnerPageIndex
                        // var subAttr = rootElement.GetElementProperties(id);
                        // if (subAttr && typeof subAttr.OwnerPageIndex == "number") {
                        //     /** 病程所在的svg元素 */
                        //     var thisPage = allSvg[subAttr.OwnerPageIndex];
                        //     // 目前不清楚获取到的svg元素是否是病程所在的元素
                        //     if (thisPage) {
                        //         if (!subNode || subNode.ownerSVGElement != thisPage) {
                        //             // 存在该ID的病程，但是暂时没有渲染出来 || 获取到该ID的病程不是第一个，是分页后面的
                        //             if (thisPage._isRendered == false) {
                        //                 // svg元素未渲染，进行渲染svg元素
                        //                 WriterControl_Print.InnerDrawOnePage(thisPage, true);
                        //                 thisPage._isRendered = true;
                        //             }
                        //             // 重新查找该ID的病程
                        //             subNode = thisPage.querySelector("[id='" + id + "']");
                        //         }
                        //     }
                        // }
                        // 如果子节点存在，则尝试滚动到该子节点的位置
                        if (subNode) {
                            try {
                                // 获取打印预览页面容器的矩形信息
                                var PrintPreviewPageContainerRect = PrintPreviewPageContainer.getBoundingClientRect();
                                // 获取子节点的矩形信息
                                var subNodeRect = subNode.getBoundingClientRect();
                                // 计算滚动到子节点所需的滚动条位置
                                var ScrollTopNumber = subNodeRect.top - PrintPreviewPageContainerRect.top + PrintPreviewPageContainer.scrollTop;
                                var ScrollLeftNumber = subNodeRect.left - PrintPreviewPageContainerRect.left + PrintPreviewPageContainer.scrollLeft;
                                // 滚动到计算的位置
                                PrintPreviewPageContainer.scrollTo(ScrollLeftNumber, ScrollTopNumber);
                                // 设置结果为成功
                                result = true;
                            } catch (error) {
                                // 如果发生错误，此处可以进行错误处理
                            }
                        }
                    } else {
                        // 不存在该ID的病程
                        result = false;
                    }
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'SelectSubDocumentByID', startTime);
            return result;
        };
        /**
         * @name GetCourseRecords
         * @type function
         * @apinameZh 兼容四代获取所有病程或指定编号的病程
         * @param ["id","string","病程编号","","可为空",false]
         * @returns ["result","string","指定编号的病程"]
         * @classification subdoc
         */
        rootElement.GetCourseRecords = function (id) {
            var startTime = new Date();
            let result = null;
            if (!id) {
                result = rootElement.__DCWriterReference.invokeMethod("GetCourseRecords", "");
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetCourseRecords", id);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetCourseRecords', startTime);
            return result;
        };

        /**
         * @name SetBoundsSelectionViewMode
         * @type function
         * @apinameZh 设置区域选择打印
         * @param ["boolean","boolean","是否开启区域选择打印","","",true]
         * @param ["boolean","boolean","开启反向选择遮盖选中的区域打印其他的","","",true]
         * @classification print
         * @change ["2024-5-11","扩展区域选择打印时按住shift键，可以选择多个区域进行打印","xym" ]
         * @change ["2024-6-19","新逻辑的区域选择打印，编辑器加上属性BoundsSelectionViewModeUseNewLogic=true","xym" ]
         */
        rootElement.SetBoundsSelectionViewMode = function (boolean, reverse) {
            return WriterControl_Print.SetBoundsSelectionViewMode(rootElement, boolean, reverse);
        };

        /**
         * @name SetBoundsSelectionViewModePro
         * @type function
         * @apinameZh 新逻辑的设置区域选择打印
         * @param ["boolean","boolean","是否开启区域选择打印","","",true]
         * @param ["boolean","boolean","开启反向选择遮盖选中的区域打印其他的","","",true]
         */
        rootElement.SetBoundsSelectionViewModePro = function (boolean, reverse) {
            return WriterControl_Print.SetBoundsSelectionViewModePro(rootElement, boolean, reverse);
        };
    },
    //*****************************************************************************************************
    //*****************************************************************************************************
    //*****************************************************************************************************
    //*****************************************************************************************************

    /**
     * 为打印预览控件而初始化根元素
     * @param {HTMLElement} rootElement 编辑器根元素
     * @param {any} refDCWriter
     */
    BindControlForWriterPrintPreviewControlForWASM: function (rootElement, refDCWriter) {
        rootElement.__DCWriterReference = refDCWriter;
        rootElement.IsWriterPrintPreviewControlForWASM = true;
        /**
         * 为打印预览添加文档
         * @param {object} parameters 混合合并的参数对象，定义同编辑器GetPrintPreviewHTML2的接口
         * @returns {boolean} 操作是否成功
         */
        rootElement.AddDocumentsByMixedFiles = function (parameters) {
            return WriterControl_IO.AddDocumentsByMixedFiles(rootElement, parameters);
        };
        /**
         * 为打印预览添加文档
         * @param {string} strFileContent 文件内容
         * @param {string} strFileFormat 文件格式
         * @param {boolean} useBase64 是否使用BASE64
         * @returns {boolean} 操作是否成功
         */
        rootElement.AddDocumentByText = function (strFileContent, strFileFormat, useBase64) {
            return WriterControl_IO.AddDocumentByText(rootElement, strFileContent, strFileFormat, useBase64);
        };
        /**
         * 执行多文档的打印预览
         * @param {any} options 打印选项
         */
        rootElement.InvalidatePreview = function (options) {
            WriterControl_Print.InvalidatePreview(rootElement, options);
        };

        /**
         * 为打印预览批量添加合并子文档
         * @param {object} options 对象参数规格同编辑器四代兼容接口AppendSubDocuments的参数对象
         * @returns {boolean} 操作是否成功
         * @change ["2024-9-10","创建API","wyc" ]
         */
        rootElement.AppendSubDocuments = function (options) {
            var tick = new Date().valueOf();
            rootElement.__LastLoadDocumentTime = new Date().valueOf();
            var result = rootElement.__DCWriterReference.invokeMethod("AppendSubDocumentsForPrintPreview", options);
            tick = new Date().valueOf() - tick;
            console.log("加载文档花费毫秒:" + tick);
            return result;
        };

        /**
        * 清空打印预览控件
        * */
        rootElement.ClearDocument = function () {
            var pages = WriterControl_UI.GetPageContainer(rootElement);
            if (pages != null) {
                while (pages.firstChild != null) {
                    pages.removeChild(pages.firstChild);
                }
            }
            rootElement.__DCWriterReference.invokeMethod("ClearDocumentForWriterPrintPreviewControl");
            //rootElement.__DCWriterReference.invokeMethod("PrintDocument");
        };

        rootElement.SetAutoZoom = function (callback, eventArgs, noAddAttr) {
        };
        rootElement.CollectrResizeCanvas = function (element) { };
        rootElement.IsPrintPreview = function () {
            return true;
        };
        /**
         * 设置文档的续打状态
         * @name SetJumpPrintMode
         * @type function
         * @apinameZh 打印预览控件中设置文档的续打状态
         * @classification print
         * @param ["params","boolean","打开或者关闭打印预览控件续打","","","true"]
         * @change ["2024-09-09","打印预览控件增加接口SetJumpPrintMode","xuyiming" ]
         * @returns ["result","boolean","操作是否成功"]
         * @describe <span style="color:red;">目前续打打印时没有隐藏页眉页脚，需要后续添加</span>。
         */
        rootElement.SetJumpPrintMode = function (isJump) {
            var startTime = new Date();
            // let result = rootElement.__DCWriterReference.invokeMethod("SetJumpPrintMode", isJump);
            let result = WriterControl_Print.SetJumpPrintModeInWriterPrintPreviewControlForWASM(rootElement, isJump);
            DCEventInterfaceLogFunction(rootElement, 'SetJumpPrintMode', startTime);
            return result;
        };

        /**
         * @name GetJumpPrintInfo
         * @type function
         * @apinameZh 打印预览控件中获取续打信息
         * @classification print
         * @change ["2024-09-09","打印预览控件增加接口GetJumpPrintInfo","xuyiming" ]
         * @returns ["result","object","pageindex打印开始页序号<br/>position打印开始位置<br/>endpageindex打印结束页序号<br/>endposition打印结束位置"]
         */
        rootElement.GetJumpPrintInfo = function () {
            var startTime = new Date();
            let result = {};
            if (rootElement && rootElement.JumpPrintInfo) {
                result = rootElement.JumpPrintInfo.Info;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetJumpPrintInfo', startTime);
            return result;
        };

        /**
        * @name SetJumpPrintInfo
        * @type function
        * @apinameZh 打印预览控件中设置续打信息
        * @classification print
        * @param ["params","object","续打信息","","包含：pageindex打印开始页序号<br/>position打印开始位置<br/>endpageindex打印结束页序号<br/>endposition打印结束位置","true"]
        * @change ["2024-09-09","打印预览控件增加接口SetJumpPrintInfo","xuyiming" ]
        * @returns ["result","Boolean","返回操作是否成功"]
        */
        rootElement.SetJumpPrintInfo = function (params) {
            var startTime = new Date();
            let result = false;
            if (rootElement && rootElement.JumpPrintInfo && typeof (rootElement.JumpPrintInfo.SetJumpPrintInfo) == "function") {
                result = rootElement.JumpPrintInfo.SetJumpPrintInfo(rootElement, params);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetJumpPrintInfo', startTime);
            return result;
        };

        /**
        * @name GetPrintPreviewPageCount
        * @type function
        * @apinameZh 获取打印预览控件的总页数
        * @classification print
        * @change ["2024-09-19","新增接口","lixinyu" ]
        * @returns ["PageCount","Number","返回打印预览控件的总页数"]
        */
        rootElement.GetPrintPreviewPageCount = function () {
            var startTime = new Date();
            let pageCount = rootElement.querySelectorAll('svg[dctype="page"]').length;
            DCEventInterfaceLogFunction(rootElement, 'GetPrintPreviewPageCount', startTime);
            return pageCount ? pageCount : 0;
        };


        //     /** 
        //    * 续打位置
        //    * @name JumpPrintPosition
        //    * @type {object.defineProperty}
        //    */
        //     Object.defineProperty(rootElement, "JumpPrintPosition", {
        //         get() {
        //             var startTime = new Date();
        //             let result = this.__DCWriterReference.invokeMethod("get_JumpPrintPosition");
        //             DCEventInterfaceLogFunction(rootElement, 'JumpPrintPosition', startTime, true);
        //             return result;
        //         },
        //         set(value) {
        //             var startTime = new Date();
        //             this.__DCWriterReference.invokeMethod("set_JumpPrintPosition", value);
        //             DCEventInterfaceLogFunction(rootElement, 'set_JumpPrintPosition', startTime, true);
        //         }
        //     });

        //     /**
        //      * 是否允许续打
        //     * @name EnableJumpPrint
        //     * @type {object.defineProperty}
        //     */
        //     Object.defineProperty(rootElement, "EnableJumpPrint", {
        //         get() {
        //             var startTime = new Date();
        //             let result = this.__DCWriterReference.invokeMethod("get_EnableJumpPrint");
        //             DCEventInterfaceLogFunction(rootElement, 'get_EnableJumpPrint', startTime, true);
        //             return result;
        //         },
        //         set(value) {
        //             var startTime = new Date();
        //             this.__DCWriterReference.invokeMethod("set_EnableJumpPrint", value);
        //             DCEventInterfaceLogFunction(rootElement, 'set_EnableJumpPrint', startTime, true);
        //         }
        //     });

        document.WriterPrintPreviewControl = rootElement;
        if (rootElement.ownerDocument !== document) {
            rootElement.ownerDocument.WriterPrintPreviewControl = rootElement;
        }
    },


    /**
     * 对根元素绑定一些方法供外面调用
     * @param {HTMLElement} rootElement 根元素对象
     * @param {object} refDCWriter DCWriterClass对象在JS中的代理
     */
    BindControlForWriterControlForWASM: function (rootElement, refDCWriter) {
        rootElement.__DCWriterReference = refDCWriter;
        rootElement.IsWriterPrintPreviewControlForWASM = false;

        /**
        * @name PrintByPDF
        * @type function
        * @apinameZh 打印为PDF文件
        * @param ["base64Data","string","PDF的base64数据流","","",true]
        * @returns ["result","null",""]
        * @change ["2024-12-10","增加接口","wyc" ]
        * @classification print
        */
        rootElement.PrintByPDF = function (base64Data) {
            var startTime = new Date();
            const blob = base64ToBlob(base64Data, 'application/pdf');
            const pdfUrl = createObjectURL(blob);
            var iframe = rootElement.ownerDocument.createElement('iframe');
            iframe.style.display = 'none';
            iframe.src = pdfUrl;
            rootElement.ownerDocument.body.appendChild(iframe);
            iframe.onload = function () {
                // 确保iframe加载完成
                iframe.contentWindow.print();
                iframe.contentWindow.onafterprint = function () {
                    rootElement.ownerDocument.body.removeChild(iframe);
                    // 释放URL对象
                    URL.revokeObjectURL(pdfUrl);
                };
            };
            function base64ToBlob(base64Data, contentType) {
                const byteCharacters = atob(base64Data); // 解码base64
                const byteArrays = [];

                for (let offset = 0; offset < byteCharacters.length; offset += 512) {
                    const slice = byteCharacters.slice(offset, offset + 512);

                    const byteNumbers = new Array(slice.length);
                    for (let i = 0; i < slice.length; i++) {
                        byteNumbers[i] = slice.charCodeAt(i);
                    }

                    const byteArray = new Uint8Array(byteNumbers);
                    byteArrays.push(byteArray);
                }
                return new Blob(byteArrays, { type: contentType });
            }
            function createObjectURL(blob) {
                return URL.createObjectURL(blob);
            }
            DCEventInterfaceLogFunction(rootElement, 'PrintByPDF', startTime);
            return;
        };

        /**
         * @name SetPageLabelText
         * @type function
         * @appnameZh 设置标签的指定页号的文本
         * @param ["element","object","元素的ID或NativeHandle","true","","false"]
         * @param ["pageindex","number","设置的页号","true","","false"]
         * @param ["text","string","指定页号的文本","true","","false"]
         * @classification view
         * @change ["2024-11-25","新增接口","wyc" ]
         * @returns ["result","boolean","代表函数是否被成功执行"]
         */
        rootElement.SetPageLabelText = function (element, pageindex, text) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("SetPageLabelText", element, pageindex, text);
            DCEventInterfaceLogFunction(rootElement, 'SetPageLabelText', startTime);
            return result;
        };

        /**
        * @name GetPageLabelText
        * @type function
        * @appnameZh 获取标签的指定页号的文本
        * @param ["element","object","元素的ID或NativeHandle","true","","false"]
        * @param ["pageindex","number","设置的页号","true","","false"]
        * @classification view
        * @change ["2024-11-25","新增接口","wyc" ]
        * @returns ["result","object","不传页号或者页号的文本不存在时传回全页号文本列表，否则传单个页号的文本"]
        */
        rootElement.GetPageLabelText = function (element, pageindex) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("GetPageLabelText", element);
            if (typeof (pageindex) === "number" && Array.isArray(result) === true) {
                for (var i = 0; i < result.length; i++) {
                    if (result[i].PageIndex === pageindex) {
                        return result[i].Text;
                    }
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'GetPageLabelText', startTime);
            return result;
        };

        /**
         * @name SetImagePageData
         * @type function
         * @appnameZh 设置标签的指定页号的文本
         * @param ["element","object","元素的ID或NativeHandle","true","","false"]
         * @param ["pageindex","number","设置的页号","true","","false"]
         * @param ["imgbase64str","string","指定页号的图片base64","true","","false"]
         * @classification view
         * @change ["2024-12-3","新增接口","wyc" ]
         * @returns ["result","boolean","代表函数是否被成功执行"]
         */
        rootElement.SetImagePageData = function (element, pageindex, imgbase64str) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("SetImagePageData", element, pageindex, imgbase64str);
            DCEventInterfaceLogFunction(rootElement, 'SetImagePageData', startTime);
            return result;
        };

        /**
        * @name GetImagePageData
        * @type function
        * @appnameZh 获取图片的指定页号的图片base64数据
        * @param ["element","object","元素的ID或NativeHandle","true","","false"]
        * @param ["pageindex","number","设置的页号","true","","false"]
        * @classification view
        * @change ["2024-12-3","新增接口","wyc" ]
        * @returns ["result","object","不传页号则传回全页号图片数据，否则传单个页号的图片数据"]
        */
        rootElement.GetImagePageData = function (element, pageindex) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("GetImagePageData", element);
            if (typeof (pageindex) === "number" && Array.isArray(result) === true) {
                for (var i = 0; i < result.length; i++) {
                    if (result[i].PageIndex === pageindex) {
                        return result[i].ImageDataBase64String;
                    }
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'GetImagePageData', startTime);
            return result;
        };

        //rootElement.TESTLOADTABLEDATA = function (tableid, dataobj) {
        //    var startTime = new Date();
        //    var result = rootElement.__DCWriterReference.invokeMethod("DOMTESTLOADTABLEDATA", tableid, dataobj);
        //    DCEventInterfaceLogFunction(rootElement, 'TESTLOADTABLEDATA', startTime);
        //    return result;
        //};



        /**
         * @name InvalidateAllView
         * @type function
         * @appnameZh 重新绘制所有内容
         * @classification view
         * @change ["2024-07-31","新增接口","yyf" ]
         */
        rootElement.InvalidateAllView = function () {
            WriterControl_Rule.InvalidateView(rootElement, "hrule");
            WriterControl_Rule.InvalidateView(rootElement, "vrule");
            WriterControl_Paint.InvalidateAllView(rootElement);
        };
        /**
         * @name getElementsByAttributes
         * @type function
         * @apinameZh 根据自定义属性信息获取文档元素相关的数据集
         * @classification file
         * @param ["options","object","参数对象","true","","false"]
         * @change ["2023-08-01","新增接口DUWRITER5_0-3009","wyc" ]
         * @returns ["result","json","返回JSON对象"]
         */
        rootElement.getElementsByAttributes = function (options) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("InnerGetElementsByAttributes", options);
            DCEventInterfaceLogFunction(rootElement, 'getElementsByAttributes', startTime);
            return result;
        };


        /**
        * @name getSupportFontFamilys
        * @type function
        * @apinameZh 获取支持的字体样式
        * @returns ["result","Array","字体样式数组"]
        * @change ["2023-6-14","增加接口","xxx" ]
        * @change ["2024-7-4","直接转发接口DUWRITER5_0-3048","wyc" ]
        * @classification file
        */
        rootElement.getSupportFontFamilys = function () {
            return WriterControl_FontList.GetFontNames();
        };


        /**
         * name StopJIEJIEPerformanceCounter
         * @type function
         * @apinameZh 输出性能调试清单（内部接口不对,接口文档中不解析name前面不加@符号的接口）
         * @param ["intSortMode","number","排序方式",0,"本参数需要注意。。。",true]
         * @returns ["result","string","调试输出信息"]
         * @classification file
         */
        rootElement.StopJIEJIEPerformanceCounter = function (intSortMode) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("StopJIEJIEPerformanceCounter", intSortMode);
            DCEventInterfaceLogFunction(rootElement, 'StopJIEJIEPerformanceCounter', startTime);
            return result;
        };

        /**
         * @name SetJumpPrintByElementID
         * @type function
         * @apinameZh 根据元素编号设置续打位置
         * @param ["参数1","string","续打开始元素编号","null","元素如果不存在则调用失败","true"]
         * @param ["参数2","string","续打结束元素编号","null","元素如果不存在则调用失败","true"]
         * @classification print
         * @describe 此接口在区域选择下不可以使用
         */
        rootElement.SetJumpPrintByElementID = function (startElementID, endElementID) {
            var startTime = new Date();
            var RectInfo = rootElement.RectInfo;//区域选择
            var startElement = rootElement.GetElementProperties(startElementID);
            var endElement = rootElement.GetElementProperties(endElementID);
            //判断开始元素和结束元素是否存在。RectInfo是否为区域选择模式
            if (startElement === null || endElement === null || RectInfo) {
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("SetJumpPrintByElementID", startElementID, endElementID);
            DCEventInterfaceLogFunction(rootElement, 'SetJumpPrintByElementID', startTime);
        };

        /**
        * @name PrintByHtml
        * @type function
        * @apinameZh 打印html的方法
        * @param ["htmlString","string","页面字符串",0,"是一个网页",true]
        * @param ["BeforePrintCallBackFunc","function","打印前回调函数",BeforePrintCallBackFunc(PrintIframeWin, PrintIframeDoc);PrintIframeWin:打印iframe的window;PrintIframeDoc:打印iframe的document","",false]
        * @classification print
        * @change ["2024-12-4","PrintByHtml添加第二个参数BeforePrintCallBackFunc，用来打印前处理内容","xym" ]
        */
        rootElement.PrintByHtml = function (htmlString, BeforePrintCallBackFunc) {
            var startTime = new Date();
            htmlString = rootElement.GetPrintNowHTML(htmlString);
            var PrintIframe = rootElement.ownerDocument.querySelector("iframe#printdoc");
            if (!PrintIframe) {
                var PrintIframe = rootElement.ownerDocument.createElement("iframe");
                PrintIframe.id = "printdoc";
                rootElement.ownerDocument.body.appendChild(PrintIframe);
            }
            PrintIframe.style.display = "none";
            //iframe1.style.cssText = 'position: absolute;top: 0;left: 0;width: 1000px;height: 600px;'
            var PrintIframeWin = PrintIframe.contentWindow;
            var PrintIframeDoc = PrintIframe.contentDocument || PrintIframe.contentWindow.document;
            // PrintIframeDoc.open();
            PrintIframeDoc.documentElement.innerHTML = htmlString;
            PrintIframeDoc.close();
            PrintIframeWin.onafterprint = function () {
                if (typeof (DCTools20221228.destroyIframe) == "function") {
                    DCTools20221228.destroyIframe(PrintIframe);
                } else {
                    PrintIframe.remove();
                }
            };
            if (typeof (BeforePrintCallBackFunc) == "function") {
                BeforePrintCallBackFunc(PrintIframeWin, PrintIframeDoc);
            }
            // 确保图片元素都渲染完成才进行打印，防止某些图片无法正常显示【DUWRITER5_0-3660】
            const images = PrintIframeDoc.querySelectorAll("img");
            if (images != null && images.length > 0) {
                // 定义一个变量来记录加载完成的图片数量
                let loadedImages = 0;
                // 为每个image元素的图片资源添加load,error事件监听器
                var CompleteFunc = function () {
                    // 当图片加载完成时，执行的操作
                    loadedImages++; // 增加加载完成的图片数量
                    if (loadedImages === images.length) {
                        // 当所有图片都加载完成时，执行的操作
                        // console.log('所有图片都加载完成');
                        PrintIframeWin.print();
                    }
                };
                images.forEach(function (image) {
                    image.onload = CompleteFunc;
                    image.onerror = CompleteFunc;
                    // 检查图片是否已经加载完成
                    if (image.complete && typeof (CompleteFunc) == "function") {
                        CompleteFunc();
                    }
                });
            } else {
                PrintIframeWin.print();
            }
            DCEventInterfaceLogFunction(rootElement, 'PrintByHtml', startTime);
        };

        /**
        * @name GetPrintNowHTML
        * @type function
        * @apinameZh 整理打印用的html的样式
        * @param ["HtmlString","string","Html字符串","","",true]
        * @returns ["result","string","调用成功返回一个html"]
        * @classification print
        * @change ["2024-6-19","修复GetPrintNowHTML会清除另外添加的样式的问题","xym" ]
        * @change ["2024-7-23","添加打印HTML时可以处理续打模式和区域选择模式，只支持当前文档","xym" ]
        */
        rootElement.GetPrintNowHTML = function (HtmlString) {
            var startTime = new Date();
            HtmlString = WriterControl_Print.GetPrintNowHTML(rootElement, HtmlString);
            DCEventInterfaceLogFunction(rootElement, 'GetPrintNowHTML', startTime);
            return HtmlString;
        };
        /**
        * @name GetCommandStatus
        * @type function
        * @apinameZh 查询编辑器命令状态
        * @param ["strCommandName","string","编辑器命令名称","","",true]
        * @returns ["result","any","状态信息"]
        * @describe
        * @classification file 
        */
        rootElement.GetCommandStatus = function (strCommandName) {
            var startTime = new Date();
            //[DUWRITER5_0-3620] lxy 20240924 增加转发医学表达式的命令
            if (strCommandName.toLowerCase().trim() == "insertmedicalexpression") {
                strCommandName = "InsertnewMedicalExpression";
            }
            let result = rootElement.__DCWriterReference.invokeMethod("GetCommandStatus", strCommandName);
            DCEventInterfaceLogFunction(rootElement, 'GetCommandStatus', startTime);
            return result;
        };
        /**
        * name RaiseEvent
        * @type function
        * @apinameZh 触发事件（内部接口不对,接口文档中不解析name前面不加@符号的接口）
        * @param ["strEventName","string","事件名称","","",true]
        * @param ["eventArgs","object","eventArgs","","",true]
        * @classification file 
        */
        rootElement.RaiseEvent = function (strEventName, eventArgs) {
            var startTime = new Date();
            WriterControl_Event.InnerRaiseEvent(rootElement, strEventName, eventArgs);
            DCEventInterfaceLogFunction(rootElement, 'RaiseEvent', startTime);

        };


        /**
        * @name SetAutoZoom
        * @type function
        * @apinameZh 设置自动缩放
        * @param ["callback","function","回调函数","","",true]
        * @param ["eventArgs","string","回调方法名称","","",true]
        * @param ["noAddAttr","boolean","是否给编辑器增加autozoom属性","","",true]
        * @describe 北大医信需求，希望不用手动给属性，通过方法设置自动缩放
        * @classification view
        */
        rootElement.SetAutoZoom = function (callback, eventArgs, noAddAttr) {
            var startTime = new Date();
            //判读页面是否存在autozoom
            //判断是否存在autozoom属性，如果存在需要撑满
            var hasAutoZoom = rootElement.getAttribute('autozoom');
            if (hasAutoZoom != 'true' && hasAutoZoom != 'True' && noAddAttr !== true) {
                rootElement.setAttribute('autozoom', 'true');
                hasAutoZoom = 'true';
            }
            if (hasAutoZoom != null && (hasAutoZoom == 'true' || hasAutoZoom == 'True')) {
                WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                    //判断父元素是否为flex布局
                    var parentEle = rootElement.parentElement;
                    if (parentEle) {
                        var isFlex = window.getComputedStyle && window.getComputedStyle(parentEle) ? window.getComputedStyle(parentEle).display : '';
                        if (isFlex == 'flex' && parentEle.children.length == 1) {
                            rootElement.style.width = "100%";
                            rootElement.style.flex = "1";
                            rootElement.style.alignSelf = "stretch";
                        }
                    }
                    //修改canvas的大小
                    var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
                    var hasCanvas = pageContainer.querySelectorAll('canvas[dctype=page]');
                    //判断是否为打印预览
                    if (rootElement.IsPrintPreview() === true) {
                        var hasPrint = rootElement.querySelector('[dctype=page-printpreview]');
                        if (hasPrint) {
                            pageContainer = hasPrint;
                            hasCanvas = hasPrint.querySelectorAll('svg[dctype=page]');
                        }
                    }
                    // 获取不到承载页面的盒子，不进行下面的代码，避免自动缩放报错
                    if (pageContainer == null) {
                        return;
                    }

                    if (hasCanvas.length > 0) {
                        var pageWidth = pageContainer.offsetWidth - 12;
                        var pageHeight = pageContainer.offsetHeight;
                        //获取到设置的宽度
                        var canvasWidth = Number(hasCanvas[0].getAttribute('native-width'));
                        var zoomRate = rootElement.GetZoomRate();
                        zoomRate = Math.floor((pageWidth / canvasWidth) * 10000) / 10000;
                        //判断是否存在滚动条
                        var allCanvasHeight = ((Number(hasCanvas[0].getAttribute('native-height')) + 12) * zoomRate) * hasCanvas.length;
                        if (pageHeight <= allCanvasHeight) {
                            pageWidth = pageWidth - 20;
                            zoomRate = Math.floor((pageWidth / canvasWidth) * 10000) / 10000;
                        }
                        if (zoomRate > 0) {
                            //获取到canvas的宽度,此处用这个是因为SetZoomRate中处理autozoom
                            WriterControl_UI.EditorSetZoomRate(rootElement, zoomRate);
                        }
                    }
                    typeof callback == 'function' ? callback(rootElement, eventArgs) : null;
                });
            } else {
                typeof callback == 'function' ? callback(rootElement, eventArgs) : null;
            }
            DCEventInterfaceLogFunction(rootElement, 'SetAutoZoom', startTime);
        };

        /**
         * @name SetWriterAutoZoom
         * @type function
         * @apinameZh 根据界面大小来缩放编辑器
         * @returns ["result","boolean","是否成功缩放"]
         * @classification view
         */
        rootElement.SetWriterAutoZoom = function () {
            var startTime = new Date();
            let result = WriterControl_UI.EditorSetWriterAutoZoom(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'SetWriterAutoZoom', startTime);
            return result;

        };

        /**
         * @name GetSectionPrintBoundsInfo
         * @type function
         * @apinameZh 获取打印的所有病程的id，生成数组，正常打印，续打打印，区域选择打印都支持
         * @change ["2024-08-24","【DUWRITER5_0-3346】新增接口","xym" ]
         * @returns ["result","array","打印的所有病程的id生成的数组"]
         * @classification print
         */
        rootElement.GetSectionPrintBoundsInfo = function () {
            var startTime = new Date();
            var subIDsArray = WriterControl_Print.GetSectionPrintBoundsInfo(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'GetSectionPrintBoundsInfo', startTime);
            return subIDsArray;
        };

        /**
         * @name isEventInterfaceLogFunction
         * @type Property
         * @apinameZh 控制日志的监听
         * @valueType Boolen
         * @usersModify 可修改
         * @change ["2023-09-07","增加了接口名称","lxy" ]
         * @describe 用于控制日志的监听，true：监听接口日志，false：关闭日志监听
         * @classification file
         */
        Object.defineProperty(rootElement, 'isEventInterfaceLogFunction', {
            value: true,      //默认打开监听日志
            writable: true, //控制属性是否可以被修改，默认值是false
        });

        /**
       * 视图模式
       * @name ViewMode
       * @type {object.defineProperty}
       */
        Object.defineProperty(rootElement, "ViewMode", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("GetSetControlViewMode", null);
                DCEventInterfaceLogFunction(rootElement, 'GetSetControlViewMode', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("GetSetControlViewMode", value);
                DCEventInterfaceLogFunction(rootElement, 'GetSetControlViewMode', startTime, true);
            }
        });

        /**
        * @name DocumentBody
        * @type Property
        * @apinameZh 获取文档分部的引用对象
        * @valueType Object
        * @usersModify 不可修改
        * @change ["2023-08-01","增加了接口名称","wyc" ]
        * @describe 例：{ "_id": 4}
        * @classification file
        */
        Object.defineProperty(rootElement, "DocumentBody", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("DCGetDocumentPart", "body");
                DCEventInterfaceLogFunction(rootElement, 'DocumentBody', startTime, true);
                return result;
            }
        });

        /**
       * @name DocumentHeader
       * @type Property
       * @apinameZh 获取文档页眉的引用对象
       * @valueType Object
       * @usersModify 不可修改
       * @change ["2023-08-01","增加了接口名称","wyc" ]
       * @describe 例：{ "_id": 4}
       * @classification file
       */
        Object.defineProperty(rootElement, "DocumentHeader", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("DCGetDocumentPart", "header");
                DCEventInterfaceLogFunction(rootElement, 'DocumentHeader', startTime, true);
                return result;
            }
        });
        /**
       * @name DocumentFooter
       * @type Property
       * @classification file
       * @apinameZh 获取文档页脚的引用对象
       * @valueType Object
       * @usersModify 不可修改
       * @change ["2023-08-01","增加了接口名称","wyc" ]
       * @describe 例：{ "_id": 4}
       */
        Object.defineProperty(rootElement, "DocumentFooter", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("DCGetDocumentPart", "footer");
                DCEventInterfaceLogFunction(rootElement, 'DocumentFooter', startTime, true);
                return result;
            }
        });


        /**
         * @name FocusedPageIndexBase0
         * @type Property
         * @classification cursor
         * @apinameZh 光标所在的页面索引
         * @valueType number
         * @usersModify 不可修改
         */

        Object.defineProperty(rootElement, "FocusedPageIndexBase0", {
            get() {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                let result = this.__DCWriterReference.invokeMethod("get_FocusedPageIndexBase0");
                DCEventInterfaceLogFunction(rootElement, 'FocusedPageIndexBase0', startTime, true);
                return result;
            }
        });

        /**
        * @name CurrentLineIndexInPage
        * @type Property
        * @classification cursor
        * @apinameZh 光标所在的页面行
        * @valueType number
        * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "CurrentLineIndexInPage", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentLineIndexInPage");
                DCEventInterfaceLogFunction(rootElement, 'CurrentLineIndexInPage', startTime, true);
                return result;
            }
        });

        /**
         * @name CurrentColumnIndex
         * @type Property
         * @classification cursor
         * @apinameZh 当前光标所在位置的第几列
         * @valueType number
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentColumnIndex", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentColumnIndex");
                DCEventInterfaceLogFunction(rootElement, 'CurrentColumnIndex', startTime, true);
                return result;
            }
        });

        /**
         * @name SelectionLength
         * @type Property
         * @classification view
         * @apinameZh 选中内容长度
         * @valueType number
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "SelectionLength", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_SelectionLength");
                DCEventInterfaceLogFunction(rootElement, 'SelectionLength', startTime, true);
                return result;
            }
        });

        /**
         * @name SelectionStartPosition
         * @type Property
         * @classification view
         * @apinameZh 选中内容开始位置
         * @valueType number
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "SelectionStartPosition", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_SelectionStartPosition");
                DCEventInterfaceLogFunction(rootElement, 'SelectionStartPosition', startTime, true);
                return result;
            }
        });

        /**
         * @name DocumentTitle
         * @type Property
         * @classification view
         * @apinameZh DocumentTitle
         * @valueType string
         * @usersModify 可修改
         */
        Object.defineProperty(rootElement, "DocumentTitle", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_DocumentTitle");
                DCEventInterfaceLogFunction(rootElement, 'DocumentTitle', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_DocumentTitle", value);
                DCEventInterfaceLogFunction(rootElement, 'set_DocumentTitle', startTime, true);
            }
        });

        /**
         * @name FileFormat
         * @type Property
         * @classification file
         * @apinameZh 获取文档类型
         * @valueType string
         * @usersModify 可修改
         * @param ["FileFormat","string","文档类型","XML","设置文档类型","是"]
         */
        Object.defineProperty(rootElement, "FileFormat", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_FileFormat");
                DCEventInterfaceLogFunction(rootElement, 'FileFormat', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_FileFormat", value);
                DCEventInterfaceLogFunction(rootElement, 'set_FileFormat', startTime, true);
            }
        });

        /**
         * @name Modified
         * @type Property
         * @classification file
         * @apinameZh 获取文档是否修改
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["FileFormat","Boolean","文档是否修改","false","设置文档是否修改","是"]
         */
        Object.defineProperty(rootElement, "Modified", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_Modified");
                DCEventInterfaceLogFunction(rootElement, 'Modified', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_Modified", value);
                DCEventInterfaceLogFunction(rootElement, 'set_Modified', startTime, true);
            }
        });

        /**
         * @name Readonly
         * @type Property
         * @classification file
         * @apinameZh 是否只读
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["FileFormat","Boolean","文档是否只读","false","设置文档是否只读","是"]
         */
        Object.defineProperty(rootElement, "Readonly", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_Readonly");
                DCEventInterfaceLogFunction(rootElement, 'Readonly', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_Readonly", value);
                DCEventInterfaceLogFunction(rootElement, 'set_Readonly', startTime, true);
            }
        });

        /**
         * @name ReadViewMode
         * @type Property
         * @classification view
         * @apinameZh 阅读视图模式
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["FileFormat","Boolean","文档是否阅读视图模式","false","设置文档是否阅读视图模式","是"]
         */
        Object.defineProperty(rootElement, "ReadViewMode", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_ReadViewMode");
                DCEventInterfaceLogFunction(rootElement, 'ReadViewMode', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                //在预览模式、区域选择、续打模式下不允许设置为阅读模式
                var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
                var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
                var RectInfo = rootElement.RectInfo;//区域选择
                if (IsPrintPreview || ExtViewMode || RectInfo) {
                    return false;
                }
                this.__DCWriterReference.invokeMethod("set_ReadViewMode", value);
                DCEventInterfaceLogFunction(rootElement, 'set_ReadViewMode', startTime, true);
                return true;
            }
        });

        /**
         * @name HeaderFooterReadonly
         * @type Property
         * @classification file
         * @apinameZh 页眉页脚只读
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["FileFormat","Boolean","文档是否页眉页脚只读","false","设置文档是否页眉页脚只读","是"]
         */
        Object.defineProperty(rootElement, "HeaderFooterReadonly", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_HeaderFooterReadonly");
                DCEventInterfaceLogFunction(rootElement, 'HeaderFooterReadonly', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_HeaderFooterReadonly", value);
                DCEventInterfaceLogFunction(rootElement, 'set_HeaderFooterReadonly', startTime, true);

            }
        });

        /**
         * @name AcceptDataFormats
         * @type Property
         * @classification file
         * @apinameZh 编辑器控件能接受的数据格式，包括粘贴操作和OLE拖拽操作获得的数据
         * @valueType string
         * @usersModify 可修改
         * @param ["示例：None","string","无任何格式","","设置数据格式","是"]
         * @param ["示例：Text","string","普通文本格式","","设置数据格式","是"]
         * @param ["示例：RTF","string","RTF","","设置数据格式","是"]
         * @param ["示例：Html","string","Html","","设置数据格式","是"]
         * @param ["示例：XML","string","XML","","设置数据格式","是"]
         * @param ["示例：Image","string","图片格式","","设置数据格式","是"]
         * @param ["示例：CommandFormat","string","编辑器命令格式","","设置数据格式","是"]
         * @param ["示例：FileSource","string","本地文件格式","","设置数据格式","是"]
         * @param ["示例：KBEntry","string","知识库节点格式","","设置数据格式","是"]
         * @param ["示例：All","string","支持所有标准格式","","设置数据格式","是"]
         */

        Object.defineProperty(rootElement, "AcceptDataFormats", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_AcceptDataFormats");
                DCEventInterfaceLogFunction(rootElement, 'AcceptDataFormats', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_AcceptDataFormats", value);
                DCEventInterfaceLogFunction(rootElement, 'set_AcceptDataFormats', startTime, true);

            }
        });

        /**
         * @name AllowDragContent
         * @type Property
         * @classification file
         * @apinameZh 能否直接拖拽文档内容
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["AllowDragContent","Boolean","文档能否直接拖拽文档内容","false","设置文档能否直接拖拽文档内容","是"]
         */
        Object.defineProperty(rootElement, "AllowDragContent", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_AllowDragContent");
                DCEventInterfaceLogFunction(rootElement, 'AllowDragContent', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_AllowDragContent", value);
                DCEventInterfaceLogFunction(rootElement, 'set_AllowDragContent', startTime, true);
            }
        });

        /**
         * @name AllowDrop
         * @type Property
         * @classification file
         * @apinameZh 控件是否可以接受用户拖放到它上面的数据
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["AllowDrop","Boolean","可以接受用户拖放到它上面的数据","false","设置可以接受用户拖放到它上面的数据","是"]
         */
        Object.defineProperty(rootElement, "AllowDrop", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_AllowDrop");
                DCEventInterfaceLogFunction(rootElement, 'AllowDrop', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_AllowDrop", value);
                DCEventInterfaceLogFunction(rootElement, 'set_AllowDrop', startTime, true);
            }
        });

        /**
        * @name CommentEditableWhenReadonly
        * @type Property
        * @classification file
        * @apinameZh 即使在只读状态下是否能编辑文档批注
        * @valueType Boolean
        * @usersModify 可修改
        * @param ["CommentEditableWhenReadonly","Boolean","在只读状态下是否能编辑文档批注","false","设置在只读状态下是否能编辑文档批注","是"]
        */
        Object.defineProperty(rootElement, "CommentEditableWhenReadonly", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CommentEditableWhenReadonly");
                DCEventInterfaceLogFunction(rootElement, 'CommentEditableWhenReadonly', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_CommentEditableWhenReadonly", value);
                DCEventInterfaceLogFunction(rootElement, 'set_CommentEditableWhenReadonly', startTime, true);
            }
        });
        /**
       * @name CommentVisibility
       * @type Property
       * @classification file
       * @apinameZh 是否显示文档批注
       * @valueType Boolean
       * @usersModify 可修改
       * @param ["CommentEditableWhenReadonly","Boolean","是否显示文档批注","false","设置是否显示文档批注","是"]
       */
        Object.defineProperty(rootElement, "CommentVisibility", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CommentVisibility");
                DCEventInterfaceLogFunction(rootElement, 'CommentVisibility', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_CommentVisibility", value);
                DCEventInterfaceLogFunction(rootElement, 'set_CommentVisibility', startTime, true);
            }
        });
        /**
      * @name ControlTypeName
      * @type Property
      * @classification file
      * @apinameZh 控件类型全名
      * @valueType Boolean
      * @usersModify 不可修改
      */
        Object.defineProperty(rootElement, "ControlTypeName", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_ControlTypeName");
                DCEventInterfaceLogFunction(rootElement, 'ControlTypeName', startTime, true);
                return result;
            }
        });

        /**
         * @name CurrentBold
         * @type Property
         * @classification view
         * @apinameZh 当前光标所在的粗体样式
         * @valueType Boolean
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentBold", {
            get() {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
                if (IsOperateDOM) {
                    let result = this.__DCWriterReference.invokeMethod("get_CurrentBold");
                    DCEventInterfaceLogFunction(rootElement, 'CurrentBold', startTime, true);
                    return result;
                }
                return null;
            }
        });

        /**
         * @name CurrentComment
         * @type Property
         * @classification file
         * @apinameZh 当前文档批注对象
         * @valueType object
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentComment", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentComment");
                DCEventInterfaceLogFunction(rootElement, 'CurrentComment', startTime, true);
                return result;
            }
        });

        /**
         * @name CurrentFontName
         * @type Property
         * @classification cursor
         * @apinameZh 当前光标所在的字体名称
         * @valueType string
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentFontName", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentFontName");
                DCEventInterfaceLogFunction(rootElement, 'CurrentFontName', startTime, true);
                return result;
            }
        });

        /**
         * @name CurrentFontSize
         * @type Property
         * @classification cursor
         * @apinameZh 当前光标所在的字体大小
         * @valueType number
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentFontSize", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentFontSize");
                DCEventInterfaceLogFunction(rootElement, 'CurrentFontSize', startTime, true);
                return result;
            }
        });

        /**
        * @name CurrentItalic
        * @type Property
        * @classification cursor
        * @apinameZh 当前光标所在的斜体样式
        * @valueType string
        * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "CurrentItalic", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentItalic");
                DCEventInterfaceLogFunction(rootElement, 'CurrentItalic', startTime, true);
                return result;
            }
        });
        /**
        * @name CurrentPageBorderColor
        * @type Property
        * @classification view
        * @apinameZh 背景颜色
        * @valueType string
        * @usersModify 可修改
        * @param ["CurrentPageBorderColor","string","背景颜色","null","需要设置的背景颜色","是"]
        */
        Object.defineProperty(rootElement, "CurrentPageBorderColor", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentPageBorderColor");
                DCEventInterfaceLogFunction(rootElement, 'CurrentPageBorderColor', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_CurrentPageBorderColor", value);
                DCEventInterfaceLogFunction(rootElement, 'set_CurrentPageBorderColor', startTime, true);
            }

        });

        /**
         * @name CurrentPageBorderColorString
         * @type Property
         * @classification view
         * @apinameZh 背景颜色字符串
         * @valueType string
         * @usersModify 可修改
         * @param ["CurrentPageBorderColorString","string","背景颜色字符串","null","需要设置的背景颜色字符串","是"]
         */
        Object.defineProperty(rootElement, "CurrentPageBorderColorString", {
            get() {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                let result = this.__DCWriterReference.invokeMethod("get_CurrentPageBorderColorString");
                DCEventInterfaceLogFunction(rootElement, 'CurrentPageBorderColorString', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                this.__DCWriterReference.invokeMethod("set_CurrentPageBorderColorString", value);
                DCEventInterfaceLogFunction(rootElement, 'set_CurrentPageBorderColorString', startTime, true);
            }
        });

        /**
         * @name CurrentParagraphAlign
         * @type Property
         * @classification view
         * @apinameZh 当前段落对齐方式
         * @valueType string
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentParagraphAlign", {
            get() {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
                if (IsOperateDOM) {
                    let result = this.__DCWriterReference.invokeMethod("get_CurrentParagraphAlign");
                    DCEventInterfaceLogFunction(rootElement, 'CurrentParagraphAlign', startTime, true);
                    return result;
                }
                return null;

            }
        });

        /**
         * @name CurrentStyle
         * @type Property
         * @classification view
         * @apinameZh 当前元素样式
         * @valueType string
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentStyle", {
            get() {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
                if (IsOperateDOM) {
                    let result = this.__DCWriterReference.invokeMethod("get_CurrentStyle");
                    DCEventInterfaceLogFunction(rootElement, 'CurrentStyle', startTime, true);
                    return result;
                }
                return null;
            }
        });

        /**
         * @name CurrentSubscript
         * @type Property
         * @classification cursor
         * @apinameZh 当前光标所在的下标样式
         * @valueType string
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentSubscript", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentSubscript");
                DCEventInterfaceLogFunction(rootElement, 'CurrentSubscript', startTime, true);
                return result;
            }
        });

        /**
         * @name CurrentSuperscript
         * @type Property
         * @classification cursor
         * @apinameZh 当前光标所在的上标样式
         * @valueType string
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentSuperscript", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentSuperscript");
                DCEventInterfaceLogFunction(rootElement, 'CurrentSuperscript', startTime, true);
                return result;
            }
        });

        /**
         * @name CurrentUnderline
         * @type Property
         * @classification cursor
         * @apinameZh 当前光标所在的下划线样式
         * @valueType string
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentUnderline", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentUnderline");
                DCEventInterfaceLogFunction(rootElement, 'CurrentUnderline', startTime, true);
                return result;
            }
        });

        /**
         * @name CurrentUser
         * @type Property
         * @classification file
         * @apinameZh 当前用户信息
         * @valueType object
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "CurrentUser", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_CurrentUser");
                DCEventInterfaceLogFunction(rootElement, 'CurrentUser', startTime, true);
                return result;
            }
        });

        /**
         * @name Document
         * @type Property
         * @classification file
         * @apinameZh 文档对象
         * @valueType object
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "Document", {
            //wyc20230704:修改前端获取文档对象的方法
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("GetDocument");
                DCEventInterfaceLogFunction(rootElement, 'Document', startTime, true);
                return result;
            },
            //set(value) { this.__DCWriterReference.invokeMethod("set_Document", value); }
        });

        /**
        * @name DocumentBehaviorOptions
        * @type Property
        * @classification file
        * @apinameZh 文档行为选项
        * @valueType object
        * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "DocumentBehaviorOptions", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_DocumentBehaviorOptions");
                DCEventInterfaceLogFunction(rootElement, 'DocumentBehaviorOptions', startTime, true);
                return result;
            }
        });

        /**
        * @name DocumentEditOptions
        * @type Property
        * @classification file
        * @apinameZh 文档编辑选项
        * @valueType object
        * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "DocumentEditOptions", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_DocumentEditOptions");
                DCEventInterfaceLogFunction(rootElement, 'DocumentEditOptions', startTime, true);
                return result;
            }
        });

        /**
        * @name DocumentSecurityOptions
        * @type Property
        * @classification file
        * @apinameZh 文档安全选项
        * @valueType object
        * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "DocumentSecurityOptions", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_DocumentSecurityOptions");
                DCEventInterfaceLogFunction(rootElement, 'DocumentSecurityOptions', startTime, true);
                return result;
            }
        });

        /**
       * @name DocumentViewOptions
       * @type Property
       * @classification file
       * @apinameZh 文档视图选项
       * @valueType object
       * @usersModify 不可修改
       */
        Object.defineProperty(rootElement, "DocumentViewOptions", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_DocumentViewOptions");
                DCEventInterfaceLogFunction(rootElement, 'DocumentViewOptions', startTime, true);
                return result;
            }
        });

        /**
          * @name DoubleClickToEditComment
          * @type Property
          * @classification file
          * @apinameZh 鼠标双击来编辑文档批注
          * @valueType object
          * @usersModify 可修改
          * @param ["DoubleClickToEditComment","Boolean","鼠标双击来编辑文档批注","true","设置鼠标双击来编辑文档批注","是"]
          */
        Object.defineProperty(rootElement, "DoubleClickToEditComment", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_DoubleClickToEditComment");
                DCEventInterfaceLogFunction(rootElement, 'DoubleClickToEditComment', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_DoubleClickToEditComment", value);
                DCEventInterfaceLogFunction(rootElement, 'set_DoubleClickToEditComment', startTime, true);
            }
        });

        /**
          * @name ExcludeKeywords
          * @type Property
          * @classification view
          * @apinameZh 违禁关键字
          * @valueType object
          * @usersModify 可修改
          * @param ["ExcludeKeywords","string","违禁关键字","","设置违禁关键字","是"]
          */
        Object.defineProperty(rootElement, "ExcludeKeywords", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_ExcludeKeywords");
                DCEventInterfaceLogFunction(rootElement, 'ExcludeKeywords', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_ExcludeKeywords", value);
                this.DocumentOptions.BehaviorOptions.ExcludeKeywords = value;
                this.ApplyDocumentOptions();
                DCEventInterfaceLogFunction(rootElement, 'set_ExcludeKeywords', startTime, true);
            }

        });

        /**
          * @name ForceShowCaret
          * @type Property
          * @classification cursor
          * @apinameZh 是否强制显示光标而不管控件是否获得输入焦点
          * @valueType object
          * @usersModify 可修改
          * @param ["ForceShowCaret","Boolean","强制显示光标而不管控件是否获得输入焦点","false","设置是否强制显示光标而不管控件是否获得输入焦点","是"]
          */
        Object.defineProperty(rootElement, "ForceShowCaret", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_ForceShowCaret");
                DCEventInterfaceLogFunction(rootElement, 'ForceShowCaret', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_ForceShowCaret", value);
                DCEventInterfaceLogFunction(rootElement, 'set_ForceShowCaret', startTime, true);
            }
        });

        /**
          * @name FormValuesArray
          * @type Property
          * @classification file
          * @apinameZh 表单数据组成的字符串数组
          * @valueType object
          * @usersModify 不可修改
          * @describe 表单数据组成的字符串数组，序号为偶数的元素为名称，序号为奇数的元素为数值。
        */
        Object.defineProperty(rootElement, "FormValuesArray", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_FormValuesArray");
                DCEventInterfaceLogFunction(rootElement, 'FormValuesArray', startTime, true);
                return result;
            }
        });

        /**
          * @name FormValuesString
          * @type Property
          * @classification file
          * @apinameZh 获得各个表单数据组成的字符串
          * @valueType string
          * @usersModify 不可修改
          * @describe 获得各个表单数据组成的字符串，采用“名称=值&名称=值&名称=值”的形式， 模仿HTML提交表单数据的字符串格式，遇到HTML特殊字符会进行转义处理。
        */
        Object.defineProperty(rootElement, "FormValuesString", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_FormValuesString");
                DCEventInterfaceLogFunction(rootElement, 'FormValuesString', startTime, true);
                return result;
            }
        });

        /**
          * @name FormValuesXml
          * @type Property
          * @classification file
          * @apinameZh XML格式的表单数据字典
          * @valueType string
          * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "FormValuesXml", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_FormValuesXml");
                DCEventInterfaceLogFunction(rootElement, 'FormValuesXml', startTime, true);
                return result;
            }
        });

        /**
          * @name GrayingDisabledHeaderFooter
          * @type Property
          * @classification view
          * @apinameZh 是否灰化显示不活跃的页眉页脚。默认true。
          * @valueType boolean
          * @usersModify 可修改
          * @param ["GrayingDisabledHeaderFooter","Boolean","灰化显示不活跃的页眉页脚","true","设置是否灰化显示不活跃的页眉页脚","是"]
        */
        Object.defineProperty(rootElement, "GrayingDisabledHeaderFooter", {
            get() {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                let result = this.__DCWriterReference.invokeMethod("get_GrayingDisabledHeaderFooter");
                DCEventInterfaceLogFunction(rootElement, 'GrayingDisabledHeaderFooter', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                this.__DCWriterReference.invokeMethod("set_GrayingDisabledHeaderFooter", value);
                DCEventInterfaceLogFunction(rootElement, 'set_GrayingDisabledHeaderFooter', startTime, true);
            }
        });

        /**
          * @name HeaderFooterFlagVisible
          * @type Property
          * @classification view
          * @apinameZh 是否显示页眉页脚标记
          * @valueType string
          * @usersModify 可修改
          * @param ["HeaderFooterFlagVisible","string","显示页眉页脚标记","None","设置是否显示页眉页脚标记","是"]
        */
        Object.defineProperty(rootElement, "HeaderFooterFlagVisible", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_HeaderFooterFlagVisible");
                DCEventInterfaceLogFunction(rootElement, 'HeaderFooterFlagVisible', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_HeaderFooterFlagVisible", value);
                DCEventInterfaceLogFunction(rootElement, 'set_HeaderFooterFlagVisible', startTime, true);
            }
        });


        /**
         * @name InsertMode
         * @type Property
         * @classification file
         * @apinameZh 当前是否处于插入模式
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["InsertMode","Boolean","当前是否处于插入模式","","设置当前是否处于插入模式","是"]
         * @describe 当前是否处于插入模式,若处于插入模式,则光标比较细,否则处于改写模式,光标比较粗
       */
        Object.defineProperty(rootElement, "InsertMode", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_InsertMode");
                DCEventInterfaceLogFunction(rootElement, 'InsertMode', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_InsertMode", value);
                DCEventInterfaceLogFunction(rootElement, 'set_InsertMode', startTime, true);
            }
        });

        /**
         * @name IsAdministrator
         * @type Property
         * @classification file
         * @apinameZh 以管理员模式运行
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["InsertMode","Boolean","是否以管理员模式运行","","设置是否以管理员模式运行","是"]
       */
        Object.defineProperty(rootElement, "IsAdministrator", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_IsAdministrator");
                DCEventInterfaceLogFunction(rootElement, 'IsAdministrator', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_IsAdministrator", value);
                DCEventInterfaceLogFunction(rootElement, 'set_IsAdministrator', startTime, true);
            }
        });

        /**
        * @name JumpPrint
        * @type Property
        * @classification print
        * @apinameZh 续打信息
        * @valueType object
        * @usersModify 可修改
        * @param ["JumpPrint","object","续打信息","","设置续打信息","是"]
      */
        // 返回的数据及释义:
        // {
        //     HasValidateInfo: 是否存在有效的信息;
        //     Mode: 续打模式，包含Disable禁止续打、Normal常规续打模式, 鼠标定位选择续打位置、Offset整体偏移续打模式，鼠标定位偏移续打位置、 Append增量续打模式，程序设置续打的页数和位置，第一页不忽略标题行;
        //     Enabled: 是否允许续打;
        //     Page: 发生续打的页面对象;
        //     PageIndex: 发生续打的页面号;
        //     NativePosition: 原始续打位置;
        //     Position: 实际使用的续打位置离续打页面顶端的距离;
        //     StartPositionMode: 续打位置模式，包含Position由Position属性明确指定续打位置、ElementTop指定文档元素的顶端位置、ElementBottom指定文档元素的低端位置;
        //     StartElement: 确定续打位置使用的文档元素对象。当StartPositionMode属性为ElementTop或ElementBottom时该属性才有效。
        //     EndPositionMode: 续打位置模式，包含Position由Position属性明确指定续打位置、ElementTop指定文档元素的顶端位置、ElementBottom指定文档元素的低端位置;
        //     EndElement: 确定续打位置使用的文档元素对象。当EndPositionMode属性为ElementTop或ElementBottom时该属性才有效。
        //     AbsPosition: 续打的绝对位置;
        //     NativeEndPosition: 原始续打结束位置, 如果小于等于Position值就无效;
        //     EndPosition: 实际使用的续打结束位置离续打页面顶端的距离, 如果小于等于Position值就无效;
        //     EndPageIndex: 续打结束位置处的页码;
        // }
        Object.defineProperty(rootElement, "JumpPrint", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_JumpPrint");
                DCEventInterfaceLogFunction(rootElement, 'JumpPrint', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_JumpPrint", value);
                DCEventInterfaceLogFunction(rootElement, 'set_JumpPrint', startTime, true);
            }
        });

        /**
         * @name JumpPrintEndPosition
         * @type Property
         * @classification print
         * @apinameZh 续打结束位置
         * @valueType number
         * @usersModify 可修改
         * @param ["JumpPrint","number","续打结束位置","","设置续打结束位置","是"]
         */
        Object.defineProperty(rootElement, "JumpPrintEndPosition", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_JumpPrintEndPosition");
                DCEventInterfaceLogFunction(rootElement, 'JumpPrintEndPosition', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_JumpPrintEndPosition", value);
                DCEventInterfaceLogFunction(rootElement, 'set_JumpPrintEndPosition', startTime, true);
            }
        });

        /**
         * @name JumpPrintPosition
         * @type Property
         * @classification print
         * @apinameZh 续打位置
         * @valueType number
         * @usersModify 可修改
         * @param ["JumpPrint","number","续打位置","","设置续打位置","是"]
         */
        Object.defineProperty(rootElement, "JumpPrintPosition", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_JumpPrintPosition");
                DCEventInterfaceLogFunction(rootElement, 'JumpPrintPosition', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_JumpPrintPosition", value);
                DCEventInterfaceLogFunction(rootElement, 'set_JumpPrintPosition', startTime, true);
            }
        });

        /** 
         * @name JumpPrintPositionForPrintPreview
         * @type Property
         * @classification print
         * @apinameZh 获得打印预览时的续打位置
         * @valueType number
         * @usersModify 不可修改
         */
        Object.defineProperty(rootElement, "JumpPrintPositionForPrintPreview", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_JumpPrintPositionForPrintPreview");
                DCEventInterfaceLogFunction(rootElement, 'JumpPrintPositionForPrintPreview', startTime, true);
                return result;
            }
        });

        /**
         * @name LastPrintPosition
         * @type Property
         * @classification print
         * @apinameZh 获得最后一次打印预览时的续打位置
         * @valueType number
         * @usersModify 不可修改
         * @describe 一般本属性和控件的JumpPrintPosition属性搭配使用.比如在打印后存储该属性值,下次打开文档后,再设置JumpPrintPosition属性值.能设置上次打印结束的位置为续打起<br />此属性与JumpPrintPositionForPrintPreview属性获取值一致
        */
        Object.defineProperty(rootElement, "LastPrintPosition", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_JumpPrintPositionForPrintPreview");
                DCEventInterfaceLogFunction(rootElement, 'JumpPrintPositionForPrintPreview', startTime, true);
                return result;
            }
        });

        /**
         * @name LastPrintResult
         * @type Property
         * @classification print
         * @apinameZh 最后一次打印结果
         * @valueType object
         * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "LastPrintResult", {
            get() {
                var startTime = new Date();
                let result = rootElement.__DCWriterReference.invokeMethod("GetPrintResult");
                DCEventInterfaceLogFunction(rootElement, 'LastPrintResult', startTime, true);
                return result;
            },
        });
        /**
        * @name ModifiedInputFields
        * @type Property
        * @classification file
        * @apinameZh 文档中包含的内容被修改的文本输入域列表对象
        * @usersModify 不可修改
        * @change ["2023-08-01","增加了接口名称","wyc" ]
        * @valueType Array
        */
        Object.defineProperty(rootElement, "ModifiedInputFields", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_ModifiedInputFields");
                DCEventInterfaceLogFunction(rootElement, 'ModifiedInputFields', startTime, true);
                return result;
            }
        });

        /**
        * @name MoveFocusHotKey
        * @type Property
        * @classification view
        * @apinameZh 移动焦点使用的快捷键
        * @valueType string
        * @usersModify 可修改
        * @param ["MoveFocusHotKey","string","移动焦点使用的快捷键","","设置移动焦点使用的快捷键","是"]
       */
        Object.defineProperty(rootElement, "MoveFocusHotKey", {
            get() {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                let result = this.__DCWriterReference.invokeMethod("get_MoveFocusHotKey");
                DCEventInterfaceLogFunction(rootElement, 'MoveFocusHotKey', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                this.__DCWriterReference.invokeMethod("set_MoveFocusHotKey", value);
                DCEventInterfaceLogFunction(rootElement, 'set_MoveFocusHotKey', startTime, true);
            }
        });

        /**
        * @name PageBackColorString
        * @type Property
        * @classification view
        * @apinameZh 页面背景颜色字符串
        * @valueType string
        * @usersModify 可修改
        * @param ["PageBackColorString","string","页面背景颜色字符串","","设置页面背景颜色字符串","是"]
       */
        Object.defineProperty(rootElement, "PageBackColorString", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_PageBackColorString");
                DCEventInterfaceLogFunction(rootElement, 'PageBackColorString', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                try {
                    this.__DCWriterReference.invokeMethod("set_PageBackColorString", value);
                    // 修复设置页面背景颜色(PageBackColorString)样式不能实时更新的问题【DUWRITER5_0-4089】
                    let cssNode = this.querySelector("#__dcpagecss" + this.id);
                    if (cssNode) {
                        const cssText = "\n .pagecss" + this.id + " { background-color: " + value + "; }";
                        cssNode.innerHTML += cssText;
                    }
                } catch (e) {
                }
                DCEventInterfaceLogFunction(rootElement, 'set_PageBackColorString', startTime, true);
            }
        });

        /**
         * @name PageBorderColorString
         * @type Property
         * @classification view
         * @apinameZh 页面边框颜色值
         * @valueType string
         * @usersModify 可修改
         * @param ["PageBorderColorString","string","页面边框颜色值","","设置页面边框颜色值","是"]
         */
        Object.defineProperty(rootElement, "PageBorderColorString", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_PageBorderColorString");
                DCEventInterfaceLogFunction(rootElement, 'PageBorderColorString', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                try {
                    this.__DCWriterReference.invokeMethod("set_PageBorderColorString", value);
                    // 修复设置页面边框颜色(PageBorderColorString)样式不能实时更新的问题【DUWRITER5_0-4089】
                    let cssNode = this.querySelector("#__dcpagecss" + this.id);
                    if (cssNode) {
                        const cssText = "\n .pagecss" + this.id + " { border-color: " + value + "; }";
                        cssNode.innerHTML += cssText;
                    }
                } catch (e) {
                }
                DCEventInterfaceLogFunction(rootElement, 'set_PageBorderColorString', startTime, true);

            }
        });

        /**
        * @name PageCount
        * @type Property
        * @classification view
        * @apinameZh 总页数
        * @valueType number
        * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "PageCount", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_PageCount");
                DCEventInterfaceLogFunction(rootElement, 'PageCount', startTime, true);
                return result;
            }
        });

        /**
         * @name PageIndex
         * @type Property
         * @classification view
         * @apinameZh 设置或返回从0开始的当前页号
         * @valueType string
         * @usersModify 可修改
         * @param ["PageIndex","number","当前页号","","设置当前页号","是"]
         */
        Object.defineProperty(rootElement, "PageIndex", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_PageIndex");
                DCEventInterfaceLogFunction(rootElement, 'PageIndex', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_PageIndex", value);
                DCEventInterfaceLogFunction(rootElement, 'set_PageIndex', startTime, true);
            }
        });

        /**
        * @name PageSettings
        * @type Property
        * @classification file
        * @apinameZh 页面设置
        * @valueType object
        * @usersModify 可修改
        * @param ["PageIndex","object","页面设置","","页面设置对象","是"]
        */
        Object.defineProperty(rootElement, "PageSettings", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_PageSettings");
                DCEventInterfaceLogFunction(rootElement, 'PageSettings', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_PageSettings", value);
                DCEventInterfaceLogFunction(rootElement, 'set_PageSettings', startTime, true);
            }
        });

        /**
       * @name PageTitlePosition
       * @type Property
       * @classification file
       * @apinameZh 页面标题位置
       * @valueType string
       * @usersModify 可修改
       * @param ["PageIndex","string","页面标题位置","","页面标题位置的字符串","是"]
       */
        Object.defineProperty(rootElement, "PageTitlePosition", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_PageTitlePosition");
                DCEventInterfaceLogFunction(rootElement, 'PageTitlePosition', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_PageTitlePosition", value);
                DCEventInterfaceLogFunction(rootElement, 'set_PageTitlePosition', startTime, true);
            }
        });

        /**
         * @name PositionInfoText
         * @type Property
         * @classification file
         * @apinameZh 表示当前插入点位置信息的字符串
         * @valueType string
         * @usersModify 可修改
         * @param ["PositionInfoText","string","插入点位置信息","","插入点位置信息的字符串","是"]
         */
        Object.defineProperty(rootElement, "PositionInfoText", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_PositionInfoText");
                DCEventInterfaceLogFunction(rootElement, 'PositionInfoText', startTime, true);
                return result;
            }
        });

        /**
        * @name RuleEnabled
        * @type Property
        * @classification view
        * @apinameZh 标尺是否可用
        * @valueType Boolean
        * @usersModify 可修改
        * @param ["RuleEnabled","Boolean","标尺是否可用","","设置标尺是否可用","是"]
        */
        Object.defineProperty(rootElement, "RuleEnabled", {
            get() {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                let result = this.__DCWriterReference.invokeMethod("get_RuleEnabled");
                DCEventInterfaceLogFunction(rootElement, 'RuleEnabled', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                if (IsOperateDOM === false) {
                    //阅读、预览、续打、区域选择，只读时不能使用此接口
                    return false;
                }
                this.__DCWriterReference.invokeMethod("set_RuleEnabled", value);
                DCEventInterfaceLogFunction(rootElement, 'set_RuleEnabled', startTime, true);
            }
        });

        /**
         * @name RuleVisible
         * @type Property
         * @classification view
         * @apinameZh 标尺是否可见
         * @valueType Boolean
         * @usersModify 可修改
         * @param ["RuleVisible","Boolean","标尺是否可见","","设置标尺是否可见","是"]
         * @describe 为了提高兼容性，默认不显示标尺。
         */
        Object.defineProperty(rootElement, "RuleVisible", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_RuleVisible");
                DCEventInterfaceLogFunction(rootElement, 'RuleVisible', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_RuleVisible", value);
                DCEventInterfaceLogFunction(rootElement, 'set_RuleVisible', startTime, true);
            }
        });

        /**
         * @name SelectedText
         * @type Property
         * @classification view
         * @apinameZh 文档中被选中的文字
         * @valueType string
         * @usersModify 可修改
         * @param ["SelectedText","string","文档中被选中的文字","","设置文档中被选中的文字","是"]
         */
        Object.defineProperty(rootElement, "SelectedText", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_SelectedText");
                DCEventInterfaceLogFunction(rootElement, 'SelectedText', startTime, true);
                return result;
            }
        });

        /**
         * @name Selection
         * @type Property
         * @classification view
         * @apinameZh 文档选择的部分
         * @valueType object
         * @usersModify 可修改
         * @change ["2023-07-25","改变写法","wyc" ]
         * @param ["Selection","object","文档选择的部分","","设置文档选择的部分","是"]
         */
        Object.defineProperty(rootElement, "Selection", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("GetSelection");
                DCEventInterfaceLogFunction(rootElement, 'Selection', startTime, true);
                return result;
            }
        });

        /**
        * @name ShowTooltip
        * @type Property
        * @classification view
        * @apinameZh 是否显示提示文本
        * @valueType Boolean
        * @usersModify 可修改
        * @param ["Selection","Boolean","是否显示提示文本","","设置是否显示提示文本","是"]
        */
        Object.defineProperty(rootElement, "ShowTooltip", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_ShowTooltip");
                DCEventInterfaceLogFunction(rootElement, 'ShowTooltip', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_ShowTooltip", value);
                DCEventInterfaceLogFunction(rootElement, 'set_ShowTooltip', startTime, true);
            }
        });


        /**
        * @name SubDocuments
        * @type Property
        * @classification subdoc
        * @apinameZh 文档中包含的子文档对象列表
        * @valueType Array
        * @usersModify 不可修改
        */
        Object.defineProperty(rootElement, "SubDocuments", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("GetAllSubDocuments");
                DCEventInterfaceLogFunction(rootElement, 'SubDocuments', startTime, true);
                return result;
            }
        });

        /**
        * @name Text
        * @type Property
        * @classification file
        * @apinameZh 控件数据
        * @valueType string
        * @usersModify 可修改
        * @param ["Text","string","控件数据","","设置控件数据","是"]
        */
        Object.defineProperty(rootElement, "Text", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_Text");
                DCEventInterfaceLogFunction(rootElement, 'Text', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_Text", value);
                DCEventInterfaceLogFunction(rootElement, 'set_Text', startTime, true);
            }
        });

        /**
        * @name XMLText
        * @type Property
        * @classification file
        * @apinameZh XML文本
        * @valueType string
        * @usersModify 可修改
        * @param ["XMLText","string","XML文本","","设置XML文本","是"]
        */
        Object.defineProperty(rootElement, "XMLText", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_XMLText");
                DCEventInterfaceLogFunction(rootElement, 'XMLText', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_XMLText", value);
                DCEventInterfaceLogFunction(rootElement, 'set_XMLText', startTime, true);
            }
        });

        /**
        * @name XMLTextForSave
        * @type Property
        * @classification file
        * @apinameZh 生成用于保存的XML字符串
        * @valueType string
        * @usersModify 可修改
        * @param ["XMLTextForSave","string","生成用于保存的XML字符串","","设置生成用于保存的XML字符串","是"]
        */
        Object.defineProperty(rootElement, "XMLTextForSave", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_XMLTextForSave");
                DCEventInterfaceLogFunction(rootElement, 'XMLTextForSave', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_XMLTextForSave", value);
                DCEventInterfaceLogFunction(rootElement, 'set_XMLTextForSave', startTime, true);
            }
        });

        /**
       * @name XMLTextUnFormatted
       * @type Property
       * @classification file
       * @apinameZh 未格式化的XML文本
       * @valueType string
       * @usersModify 可修改
       * @param ["XMLTextUnFormatted","string","未格式化的XML文本","","设置未格式化的XML文本","是"]
       */
        Object.defineProperty(rootElement, "XMLTextUnFormatted", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_XMLTextUnFormatted");
                DCEventInterfaceLogFunction(rootElement, 'XMLTextUnFormatted', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_XMLTextUnFormatted", value);
                DCEventInterfaceLogFunction(rootElement, 'set_XMLTextUnFormatted', startTime, true);
            }
        });

        /**
       * @name XMLTextWithDocumentState
       * @type Property
       * @classification file
       * @apinameZh 获得文档XML内容
       * @valueType string
       * @usersModify 可修改
       * @param ["XMLTextWithDocumentState","string","获得文档XML内容","","设置获得文档XML内容","是"]
       */
        Object.defineProperty(rootElement, "XMLTextWithDocumentState", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_XMLTextWithDocumentState");
                DCEventInterfaceLogFunction(rootElement, 'XMLTextWithDocumentState', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_XMLTextWithDocumentState", value);
                DCEventInterfaceLogFunction(rootElement, 'set_XMLTextWithDocumentState', startTime, true);
            }
        });


        /**
       * @name ExtViewMode
       * @type Property
       * @classification print
       * @apinameZh 获得控件的续打模式状态
       * @valueType string
       * @usersModify 可修改
       * @param ["ExtViewMode","string","获得控件的续打模式状态","","设置获得控件的续打模式状态","是"]
       */
        Object.defineProperty(rootElement, "ExtViewMode", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_ExtViewMode");
                DCEventInterfaceLogFunction(rootElement, 'ExtViewMode', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                this.__DCWriterReference.invokeMethod("set_ExtViewMode", value);
                DCEventInterfaceLogFunction(rootElement, 'set_ExtViewMode', startTime, true);
            }
        });

        /**
       * @name BodyText
       * @type Property
       * @classification file
       * @apinameZh XML文本
       * @valueType string
       * @usersModify 可修改
       * @param ["XMLText","string","XML文本","","设置XML文本","是"]
       */
        Object.defineProperty(rootElement, "BodyText", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_BodyText");
                DCEventInterfaceLogFunction(rootElement, 'BodyText', startTime, true);
                return result;
            }
        });

        /**
        * @name ScriptText
        * @type Property
        * @classification file
        * @apinameZh ScriptText
        * @valueType string
        * @usersModify 可修改
        * @param ["ScriptText","string","ScriptText文本","","设置ScriptText文本","是"]
        */
        Object.defineProperty(rootElement, "ScriptText", {
            get() {
                var startTime = new Date();
                let result = this.__DCWriterReference.invokeMethod("get_ScriptText");
                if (result) {
                    //解码base64
                    const binString = atob(result);
                    //为了防止带有中文特殊符号而导致的解码不正确问题
                    result = Uint8Array.from(binString, (m) => m.codePointAt(0));
                    result = new TextDecoder().decode(result);
                }
                DCEventInterfaceLogFunction(rootElement, 'ScriptText', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                if (value) {
                    //转成base64时，如果含有中文会报错，使用new TextEncoder在 UTF-8 和字符串的单字节表示之间进行转换
                    value = new TextEncoder().encode(value);
                    const binString = Array.from(value, (byte) =>
                        String.fromCodePoint(byte),
                    ).join("");
                    value = btoa(binString);
                    //"a Ā 𐀀 文 🦄"
                }
                this.__DCWriterReference.invokeMethod("set_ScriptText", value);
                DCEventInterfaceLogFunction(rootElement, 'set_ScriptText', startTime, true);
            }
        });


        /**
        * @name AppendSubDocument
        * @type function
        * @classification subdoc
        * @apinameZh 追加新的子文档对象
        * @param ["newSubDoc","object","子文档对象","","",true]
        * @describe 追加新的子文档对象,参数是一个对象
        * @describe 仅用于插入一个空的病程，需要配合加载病程文档的接口使用，比如：LoadSubDocumentFromString
        */
        rootElement.AppendSubDocument = function (newSubDoc) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读模式下阻止追加子文档
            if (readonly) {
                return false;
            }
            if (IsOperateDOM) {
                rootElement.__DCWriterReference.invokeMethod("AppendSubDocument", newSubDoc);
                DCEventInterfaceLogFunction(rootElement, 'AppendSubDocument', startTime);
            }
            return false;

        };

        /**
        * @name ClearContent
        * @type function
        * @classification editformat
        * @apinameZh 清空文档内容
        * @describe 本接口适用于清空文档内容
        */
        rootElement.ClearContent = function () {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("ClearContent");

                if (result) {
                    rootElement.ClearOldVisibleElements();
                }

                DCEventInterfaceLogFunction(rootElement, 'ClearContent', startTime);
            }
            return result;
        };


        /**
        * @name ClearUndo
        * @type function
        * @classification editformat
        * @apinameZh 清空重做
        * @describe 清空重做 / 撤销操作信息
        */
        rootElement.ClearUndo = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("ClearUndo");
            DCEventInterfaceLogFunction(rootElement, 'ClearUndo', startTime);
        };

        /**wyc20231106:修改参数传递机制
        * @name CommitContentUserTrace
        * @type function
        * @classification vestige
        * @apinameZh 提交指定容器元素中的用户痕迹信息
        * @param ["ELEMENT","object","文档元素对象",null,"{DCSoft.Writer.Dom.XTextElement}文档元素对象",true]
        * @returns ["result","boolean","操作是否修改了文档内容"]
        */
        rootElement.CommitContentUserTrace = function (element) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(element) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("CommitContentUserTrace2", element);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("CommitContentUserTrace", element);
            }
            DCEventInterfaceLogFunction(rootElement, 'CommitContentUserTrace', startTime);
            return result;
        };

        /**wyc20231106:修改参数传递机制
        * @name CommitContentUserTraceById
        * @type function
        * @classification vestige
        * @apinameZh 提交指定容器元素中的用户痕迹信息
        * @param ["ELEMENT","object","文档元素对象",null,"{DCSoft.Writer.Dom.XTextElement}文档元素对象",true]
        * @returns ["result","boolean","操作是否修改了文档内容"]
        */
        rootElement.CommitContentUserTraceById = function (element) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(element) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("CommitContentUserTrace2", element);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("CommitContentUserTrace", element);
            }
            DCEventInterfaceLogFunction(rootElement, 'CommitContentUserTraceById', startTime);
            return result;
        };

        /**
       * @name Copy
       * @type function
       * @classification editformat
       * @apinameZh 复制
       */
        rootElement.Copy = function () {
            var startTime = new Date();

            //让编辑器获取焦点
            //找到内部的textarea
            //var hasTextArea = rootElement.querySelector('textarea[dctype=dcinput]');
            //if (hasTextArea) {
            //    hasTextArea.focus();
            //}
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            if (IsPrintPreview === false) {
                rootElement.ownerDocument.execCommand('copy');
                console.log('copy');
            }

            DCEventInterfaceLogFunction(rootElement, 'Copy', startTime);
            //return rootElement.__DCWriterReference.invokeMethod("Copy");
        };


        /**
         * @name hasLocalPaste
         * @type function
         * @classification editformat
         * @apinameZh 
         */
        rootElement.hasLocalPaste = function () {
            var startTime = new Date();
            let result = false;
            if (navigator.clipboard && navigator.clipboard.write) {
                result = true;
            }
            DCEventInterfaceLogFunction(rootElement, 'hasLocalPaste', startTime);
            return result;
        };



        /**
       * @name Cut
       * @type function
       * @classification editformat
       * @apinameZh 剪切
       * @returns ["result","boolean","操作是否成功"]
       */
        rootElement.Cut = function () {
            var startTime = new Date();
            //var datas = '';
            //var ref9 = rootElement.__DCWriterReference;
            //if (ref9 != null) {
            //    datas = ref9.invokeMethod(
            //        "DoCut", false, false);
            //}
            //console.log(datas)
            //WriterControl_UI.SetClipboardData(datas, null, 'cut', rootElement);
            //return rootElement.__DCWriterReference.invokeMethod("Cut", showUI);
            rootElement.ownerDocument.execCommand('cut');
            DCEventInterfaceLogFunction(rootElement, 'Cut', startTime);

        };

        /**
        * @name DeleteSelection
        * @type function
        * @apinameZh 删除选择区域
        * @classification editformat
        * @param ["showUI","boolean","",0,"",true]
        */
        rootElement.DeleteSelection = function (showUI) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                rootElement.__DCWriterReference.invokeMethod("DeleteSelection", showUI);
                DCEventInterfaceLogFunction(rootElement, 'DeleteSelection', startTime);
            }
            return false;
        };

        /**
        * @name Dispose
        * @type function
        * @classification file
        * @apinameZh 清理所有正在使用的资源
        */
        // * @param { boolean } disposing 如果应释放托管资源，为 true；否则为 false。

        rootElement.Dispose = function () {
            if (rootElement.__DCDisposed == true) {
                return;// 控件已经被销毁了，无需重复操作。
            }
            if (rootElement.ownerDocument != null) {
                DCTools20221228.ConsoleWarring("南京都昌公司建议先从HTML-DOM中删除DIV元素{" + rootElement.id + "},然后调用它的Dispose()方法，不正确的调用次序容易导致错误。");
            }

            var startTime = new Date();
            //停用日志防止报错
            if (rootElement.IsAPILogRecord() == true) {
                rootElement.StopAPILogRecord();
            }
            if (rootElement.__DCWriterReference != null) {
                rootElement.__DCWriterReference.invokeMethod("Dispose");
                rootElement.__DCWriterReference = null;
                rootElement.resizeObserver.disconnect(rootElement);
            }
            while (rootElement.firstChild != null) {
                rootElement.removeChild(rootElement.firstChild);
            }
            if (window.__DCWriterControls != null) {
                for (var strKey in window.__DCWriterControls) {
                    var ctl2 = window.__DCWriterControls[strKey];
                    if (ctl2 == rootElement) {
                        window.__DCWriterControls[strKey] = null;
                        break;
                    }
                }
            }
            rootElement.__DCDisposed = true;
            DCEventInterfaceLogFunction(rootElement, 'Dispose', startTime);

        };

        /**
        * @name DocumentValueValidate
        * @type function
        * @classification file
        * @apinameZh 文档内容进行校验
        * @param ["id","object","病程或输入域的编号或handle对象","null","","false"]
        * @returns ["result","boolean","返回校验结果"]
        * @change ["2024-08-05","增加一个参数id，用于指定病程或输入域的编号或handle对象","lxy" ]
        */
        rootElement.DocumentValueValidate = function (id = null) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("DocumentValueValidate", id);
            DCEventInterfaceLogFunction(rootElement, 'DocumentValueValidate', startTime);
            return result;
        };

        /**
         * @name DocumentValueValidateWithCreateDocumentComments
         * @type function
         * @classification file
         * @apinameZh 文档内容进行校验(一页的大小只能排大约13条批注)
         * @returns ["result","Array","返回校验结果"]
         * @describe 阅读、预览、预览当前页。由于没有结构化数据，不能使用该接口;偏移续打时，由于排版位置发生变化，不能使用该接口;续打、区域选择打印，结构化数据没有改变，允许使用。
         */
        rootElement.DocumentValueValidateWithCreateDocumentComments = function () {
            var startTime = new Date();
            let result = false;
            var ReadViewMode = rootElement.ReadViewMode;//阅读模式
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            if (ReadViewMode === false && IsPrintPreview === false) {
                result = rootElement.__DCWriterReference.invokeMethod("DocumentValueValidateWithCreateDocumentComments");
                DCEventInterfaceLogFunction(rootElement, 'DocumentValueValidateWithCreateDocumentComments', startTime);
            }
            return result;
        };

        /**
        * @name ExecuteCommand
        * @type function
        * @classification file
        * @apinameZh 执行编辑器命令
        * @param ["参数1：strCommandName","string","命令名称","","本参数是控件命令的名称",true]
        * @param ["参数2：bolShowUI","boolean","是否显示用户界面","","用于判断是否显示用户界面",true]
        * @param ["参数2：strParameter","object","参数","","一些属性",true]
        * @returns ["result","string","执行结果"]
        */
        rootElement.ExecuteCommand = function (strCommandName, bolShowUI, strParameter) {
            var startTime = new Date();

            let result = rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", strCommandName, bolShowUI, strParameter);
            //wyc20240108:追加若干命令需要后续刷新前端文档选项的操作
            var cmdNamesForRefreshOptions = ["cleanviewmode", "complexviewmode"];
            strCommandName = strCommandName.toLocaleLowerCase();
            if (cmdNamesForRefreshOptions.indexOf(strCommandName) >= 0) {
                rootElement.refreshDocumentOptions();
            }
            //设置文档状态
            if (['table_mergecell', 'table_splitcellext'].indexOf(strCommandName) >= 0) {
                if (Boolean(result)) {
                    rootElement.Modified = true;
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'ExecuteCommand', startTime);
            return result;
        };

        /**
        * @name FlashElement
        * @type function
        * @classification file
        * @apinameZh 高亮度提示文档元素 
        * @param ["ELEMENT","object","文档元素对象",null,"{DCSoft.Writer.Dom.XTextElement}文档元素对象",true]
        * @param ["autoScroll","boolean","是否自动滚动",false,"",true]
        */
        rootElement.FlashElement = function (element, autoScroll) {
            var startTime = new Date();
            var strJson = null;
            //自动滚动到闪烁位置
            if (autoScroll) {
                //为了获取rootElement.oldCaretOption位置。先把光标定位到元素上
                rootElement.FocusElement(element);
                var pageContainer = rootElement.querySelector('[dctype="page-container"]');
                // //编辑器内部页面存在滚动
                if (pageContainer && pageContainer.scrollHeight > pageContainer.clientHeight) {
                    var oldCaretOption = rootElement.oldCaretOption;
                    var pageContainerAllCanvas = pageContainer.querySelectorAll('canvas[dctype="page"]');
                    if (pageContainerAllCanvas && pageContainerAllCanvas.length) {
                        for (var i = 0; i < pageContainerAllCanvas.length; i++) {
                            var itemCanvas = pageContainerAllCanvas[i];
                            if (i == oldCaretOption.intPageIndex) {
                                pageContainer.scrollTo(
                                    {
                                        left: 0,
                                        top: itemCanvas.offsetTop + oldCaretOption.intDY - 10,
                                        behavior: "instant"
                                    });
                            }
                        }
                    }
                }
            }
            setTimeout(function () {
                //闪烁视图
                strJson = rootElement.__DCWriterReference.invokeMethod("GetFlashElementInfo", element, autoScroll);
                if (strJson != null && strJson.length > 0) {
                    var objJson = JSON.parse(strJson);
                    // 根据JSON对象来闪烁指定区域
                    WriterControl_UI.FlashArea(rootElement, objJson);
                }
            }, 100);

            DCEventInterfaceLogFunction(rootElement, 'FlashElement', startTime);
        };

        /**
         * @name Focus
         * @type function
         * @classification cursor
         * @apinameZh 编辑器控件获得输入焦点
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.Focus = function () {
            var startTime = new Date();
            //处理在聚焦前把光标显示出来
            if (rootElement.oldCaretOption) {
                WriterControl_UI.ShowCaret(
                    rootElement.oldCaretOption.containerID,
                    rootElement.oldCaretOption.intPageIndex,
                    rootElement.oldCaretOption.intDX,
                    rootElement.oldCaretOption.intDY,
                    rootElement.oldCaretOption.intWidth,
                    rootElement.oldCaretOption.intHeight,
                    rootElement.oldCaretOption.bolVisible,
                    rootElement.oldCaretOption.bolReadonly,
                    false,
                    "rootElement.Focus"
                );
            }

            let result = rootElement.__DCWriterReference.invokeMethod("Focus");
            DCEventInterfaceLogFunction(rootElement, 'Focus', startTime);
            return result;
        };



        /**
         * @name FocusElement
         * @type function
         * @apinameZh 让指定文档元素获得输入焦点
         * @classification cursor
         * @param ["ELEMENT","object","文档元素的ID","null","文档元素的ID，或元素的后台.NET引用对象 ",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2023-07-25","补充更通用的定位元素接口","wyc" ]
         */
        rootElement.FocusElement = function (item) {
            if (item == null) {
                return false;
            }
            var startTime = new Date();
            let result = false;
            //处理在聚焦前把光标显示出来
            if (rootElement.oldCaretOption) {
                WriterControl_UI.ShowCaret(
                    rootElement.oldCaretOption.containerID,
                    rootElement.oldCaretOption.intPageIndex,
                    rootElement.oldCaretOption.intDX,
                    rootElement.oldCaretOption.intDY,
                    rootElement.oldCaretOption.intWidth,
                    rootElement.oldCaretOption.intHeight,
                    rootElement.oldCaretOption.bolVisible,
                    rootElement.oldCaretOption.bolReadonly,
                    false,
                    "rootElement.FocusElement"
                );
            }
            if (DCTools20221228.IsDotnetReferenceElement(item) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("FocusElement2", item);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("FocusElement", item);
            }
            DCEventInterfaceLogFunction(rootElement, 'FocusElement', startTime);
            return result;
        };

        /**
        * @name GetBindingDataSources
        * @type function
        * @classification datasource
        * @apinameZh 获得文档中绑定的数据源名称字符串列表
        * @returns ["result","string","数据源名称列表"]
        * @describe 获得文档中绑定的数据源名称字符串列表。各个名称之间用逗号分开
        */
        rootElement.GetBindingDataSources = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetBindingDataSources");
            DCEventInterfaceLogFunction(rootElement, 'GetBindingDataSources', startTime);
            return result;
        };
        /**
        * @name GetCheckedValueList
        * @type function
        * @classification structuralelement
        * @apinameZh 获得文档中所有的勾选的复选框元素的值
        * @param ["spliter","string","各个项目之间的分隔字符串","","",true]
        * @param ["includeCheckbox","boolean","是否包含复选框","","",true]
        * @param ["includeRadio","boolean","是否包含单选框","","",true]
        * @param ["includeElementID","boolean","是否包含元素ID号","","",true]
        * @param ["includeElementName","boolean","是否包含元素Name属性值","","",true]
        * @returns ["result","string","获得的字符串"]
        * @describe 例如调用 document.GetCheckedValueList(";", true, true, true, true) 返回类似字符串 “xbzw; 胸部正位; gpzw; 骨盆正位; fbww; 腹部卧位”
        */
        rootElement.GetCheckedValueList = function (spliter, includeCheckbox, includeRadio, includeElementID, includeElementName) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetCheckedValueList", spliter, includeCheckbox, includeRadio, includeElementID, includeElementName);
            DCEventInterfaceLogFunction(rootElement, 'GetCheckedValueList', startTime);
            return result;
        };

        /**
        * @name GetCommandNameList
        * @type function
        * @classification file
        * @apinameZh 获得所有支持的命令名称组成的字符串
        * @returns ["result","string","字符串列表"]
        * @describe 获得所有支持的命令名称组成的字符串，各个名称之间用逗号分开
        */
        rootElement.GetCommandNameList = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetCommandNameList");
            //[DUWRITER5_0-2825] 20240817 lxy 删除5代中不需要的命令
            let excludeCommandNames = [
                "Table_SplitCell",
                "Table_SetCellGridLine",
                "Table_CellGridLine",
                "SectionBorderBackgroundFormat",
                "Search",
                "SearchReplace",
                "RuleVisible",
                "PrintColor",
                "PrintBackColor",
                "ParagraphListStyle",
                "PageBorderBackgroundFormat",
                "InsertTDBarcode",
                "InsertBarcode",
                "InsertNewMedicalExpression",
                "InsertNewBarcode",
                "InsertElements"
            ];
            let allCommandNames = result.split(',');
            excludeCommandNames.forEach(item => {
                let findIndex = allCommandNames.findIndex(element => element.toLowerCase().trim() === item.toLowerCase().trim());
                if (findIndex !== -1) {
                    allCommandNames.splice(findIndex, 1);
                }
            });

            DCEventInterfaceLogFunction(rootElement, 'GetCommandNameList', startTime);
            return allCommandNames.join(',');
        };

        /**
        * @name GetCustomAttribute
        * @type function
        * @classification attribute
        * @apinameZh 获得自定义属性值
        * @param ["name","string","属性名","","自定义属性的名称",true]
        * @returns ["result","string","属性值"]
        */
        rootElement.GetCustomAttribute = function (name) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetCustomAttribute", name);
            DCEventInterfaceLogFunction(rootElement, 'GetCustomAttribute', startTime);
            return result;
        };

        /**
        * @name GetDataSourceBindingDescriptions
        * @type function
        * @classification datasource
        * @apinameZh 获得描述数据源绑定信息
        * @returns ["result","Array","{DCSoft.Writer.Dom.DataSourceBindingDescriptionList} 描述信息列表"]
        */
        rootElement.GetDataSourceBindingDescriptions = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDataSourceBindingDescriptions");
            DCEventInterfaceLogFunction(rootElement, 'GetDataSourceBindingDescriptions', startTime);
            return result;
        };

        /**
        * @name GetDataSourceBindingDescriptionsXML
        * @type function
        * @classification datasource
        * @apinameZh 获得描述数据源绑定信息的XML字符串
        * @returns ["result","string","描述数据源绑定信息的XML字符串"]
        */
        rootElement.GetDataSourceBindingDescriptionsXML = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDataSourceBindingDescriptionsXML");
            DCEventInterfaceLogFunction(rootElement, 'GetDataSourceBindingDescriptionsXML', startTime);
            return result;
        };
        /**
         * @name GetDetectValueBindingModifiedMessage
         * @type function
         * @classification datasource
         * @apinameZh 修改文档元素的相关信息
         * @returns ["result","Array","结果信息列表"]
         * @describe 检测数据源填充操作导致的修改文档元素的相关信息，但不真正填充数据源，不会修改文档内容。
         */
        rootElement.GetDetectValueBindingModifiedMessage = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDetectValueBindingModifiedMessage");
            DCEventInterfaceLogFunction(rootElement, 'GetDetectValueBindingModifiedMessage', startTime);
            return result;
        };

        /**
        * @name GetDocumentParameterEnabled
        * @type function
        * @classification file
        * @apinameZh 获得参数是否有效
        * @param ["parameterName","string","参数名","","",true]
        * @returns ["result","boolean","是否有效"]
        */
        rootElement.GetDocumentParameterEnabled = function (parameterName) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentParameterEnabled", parameterName);
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentParameterEnabled', startTime);
            return result;
        };

        /**
        * @name GetDocumentParameterValueXml
        * @type function
        * @classification file
        * @apinameZh 获得Xml格式的文档参数值
        * @param ["name","string","参数名","","",true]
        * @returns ["result","string","参数值"]
        */
        rootElement.GetDocumentParameterValueXml = function (name) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentParameterValueXml", name);
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentParameterValueXml', startTime);
            return result;
        };

        /**
        * @name GetDocumentParameterValue
        * @classification file
        * @type function
        * @apinameZh 获得文档参数值
        * @param ["name","string","参数名","","",true]
        * @returns ["result","object","获得文档参数值"]
        */
        rootElement.GetDocumentParameterValue = function (name) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentParameterValue", name);
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentParameterValue', startTime);
            return result;
        };

        /**
        * @name GetElementAttribute
        * @type function
        * @classification attribute
        * @apinameZh 获得指定文档元素的属性
        * @param ["ELEMENT","object","指定要查找的父元素的ID/NativeHandle/后台引用对象 ","","",true]
        * @param ["attributeName","string","属性名称 ","","",true]
        * @returns ["result","string","属性值"]
        */
        rootElement.GetElementAttribute = function (id, attributeName) {
            var startTime = new Date();
            let result = null;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementAttribute2", id, attributeName);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementAttribute", id, attributeName);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementAttribute', startTime);
            return result;
        };

        /**
        * @name GetElementById
        * @type function
        * @classification structuralelement
        * @apinameZh 获得指定ID号的文档元素对象
        * @param ["id","string","元素编号、或者是元素的NativeHandle ","","",true]
        * @returns ["result","object","{DCSoft.Writer.Dom.XTextElement} 找到的文档元素对象"]
        * @describe 获得指定ID号的文档元素对象, 查找时ID值区分大小写的。
        */
        rootElement.GetElementById = function (id) {
            var startTime = new Date();
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("DCGetElementProperties", id);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("DCGetElementProperties2", id);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementById', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("GetElementById", id);
        };

        /**
        * @name GetElementByIdExt
        * @type function
        * @apinameZh 获得指定ID号的文档元素对象
        * @classification structuralelement
        * @param ["id","string","子元素编号 ","","",true]
        * @param ["ELEMENT","object","指定要查找的父元素的ID/NativeHandle/后台引用对象 ","","",true]
        * @returns ["result","object","找到的文档元素对象的nativehandle"]
        * @describe 获得指定ID号的文档元素对象, 查找时ID值区分大小写的。
        * @change ["2023-05-26","返回后台的对象,可以指定父元素查找","wyc" ]
        */
        rootElement.GetElementByIdExt = function (id, specifyParent) {
            var startTime = new Date();
            let result = null;
            if (DCTools20221228.IsDotnetReferenceElement(specifyParent) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementById2", id, specifyParent);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementById", id, specifyParent);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementByIdExt', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("GetElementById", id);
        };

        /**
        * @name GetElementChecked
        * @type function
        * @apinameZh 获得单复选框文档元素的勾选状态
        * @classification structuralelement
        * @param ["id","string","文档元素编号","","",true]
        * @returns ["result","boolean","元素的勾选状态"]
        * @describe 获得单复选框文档元素的勾选状态, 如果没找到元素则返回false。
        */
        rootElement.GetElementChecked = function (id) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetElementChecked", id);
            DCEventInterfaceLogFunction(rootElement, 'GetElementChecked', startTime);
            return result;
        };

        /**
        * @name GetElementInnerXmlByID
        * @type function
        * @classification structuralelement
        * @apinameZh 返回元素外置内容的文档XML字符串
        * @param ["id","string","元素编号","","",true]
        * @param ["includeThis","Boolean","是否返回包含元素本身的XML","true","",true]
        * @returns ["result","string","XML字符串"]
        */
        rootElement.GetElementInnerXmlByID = function (id, includeThis = true) {
            var startTime = new Date();
            let result = "";
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementInnerXmlByID2", id, includeThis);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementInnerXmlByID", id, includeThis);
            }
            //result =DCEventInterfaceLogFunction(rootElement, 'GetElementInnerXmlByID', startTime);
            return result;
        };

        /**
        * @name GetElementInnerBase64XmlByID
        * @type function
        * @classification structuralelement
        * @apinameZh 返回元素Base64格式的XML字符串
        * @param ["id","string","元素编号","","",true]
        * @param ["includeThis","Boolean","是否返回包含元素本身的XML","true","",true]
        * @returns ["result","string","XML字符串"]
        * @change ["2024-04-22","增加接口","lxy" ]
        */
        rootElement.GetElementInnerBase64XmlByID = function (id, includeThis = true) {
            var startTime = new Date();
            let result = "";
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementInnerBase64XmlByID2", id, includeThis);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementInnerBase64XmlByID", id, includeThis);
            }
            //let result = rootElement.__DCWriterReference.invokeMethod("GetElementInnerBase64XmlByID", id, includeThis);
            DCEventInterfaceLogFunction(rootElement, 'GetElementInnerBase64XmlByID', startTime);
            return result;
        };

        /**
        * @name GetElementOuterXmlByID
        * @type function
        * @classification structuralelement
        * @apinameZh 返回元素内置内容的文档XML字符串
        * @param ["id","string","元素编号","","",true]
        * @returns ["result","string","XML字符串"]
        */
        rootElement.GetElementOuterXmlByID = function (id) {
            var startTime = new Date();
            let result = "";
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementOuterXmlByID2", id);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementOuterXmlByID", id);
            }
            //let result = rootElement.__DCWriterReference.invokeMethod("GetElementOuterXmlByID", id);
            DCEventInterfaceLogFunction(rootElement, 'GetElementOuterXmlByID', startTime);
            return result;
        };

        /**
        * @name GetElementProperty
        * @type function
        * @classification attribute
        * @apinameZh 获得文档元素的自定义属性值
        * @param ["id","string","元素编号","","",true]
        * @param ["name","string","属性名","","",true]
        * @returns ["result","string","属性值"]
        */
        rootElement.GetElementProperty = function (id, name) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("GetElementProperty", id, name);
            DCEventInterfaceLogFunction(rootElement, 'GetElementProperty', startTime);
            return result;
        };

        /**
        * @name GetElementsById
        * @type function
        * @classification structuralelement
        * @apinameZh 获得文档中所有指定编号的元素对象列表
        * @param ["id","string","元素编号","","",true]
        * @returns ["result","Array","{DCSoft.Writer.Dom.XTextElementList} 找到的元素对象列表"]
        * @describe 获得文档中所有指定编号的元素对象列表, 查找时ID值区分大小写的。
        */
        rootElement.GetElementsById = function (id) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetElementsById", id);
            DCEventInterfaceLogFunction(rootElement, 'GetElementsById', startTime);
            return result;
        };

        /**
        * @name GetElementsByName
        * @type function
        * @apinameZh 获得文档中指定name值的元素对象
        * @classification structuralelement
        * @param ["name","string","元素的Name","","",true]
        * @returns ["result","Array","找到的元素对象列表"]
        * @describe 查找时name值区分大小写的。
        */
        rootElement.GetElementsByName = function (name) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetElementsByName", name);
            DCEventInterfaceLogFunction(rootElement, 'GetElementsByName', startTime);
            return result;
        };

        /**
        * @name GetElementVisible
        * @type function
        * @apinameZh 获得文档元素的可见性
        * @classification structuralelement
        * @param ["id","string","元素编号","","",true]
        * @returns ["result","boolean","可见性"]
        */
        rootElement.GetElementVisible = function (id) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetElementVisible", id);
            DCEventInterfaceLogFunction(rootElement, 'GetElementVisible', startTime);
            return result;
        };

        /**
        * @name GetElementXMLFragmentByID
        * @type function
        * @apinameZh 获取包含数据的XML片段
        * @classification file
        * @param ["id","string","元素编号","","",true]
        * @param ["isIncludSelf","boolean","是否包含元素自身","false","",true]
        * @returns ["result","string","生成的XML片段字符串"]
        * @describe 返回包含数据的XML片段, 本函数返回的XML字符串可以作为编辑器控件或文档对象的函数CreateElementByXMLFragment()的参数。
        */
        rootElement.GetElementXMLFragmentByID = function (id, isIncludSelf = false) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetElementXMLFragmentByID", id, isIncludSelf);
            DCEventInterfaceLogFunction(rootElement, 'GetElementXMLFragmentByID', startTime);
            return result;
        };


        /**
        * @name GetFormValue
        * @type function
        * @apinameZh 获得表单数据
        * @classification structuralelement
        * @param ["name","string","字段名称",0,"",true]
        * @returns ["result","string","获得的表单数值"]
        */
        rootElement.GetFormValue = function (name) {
            var startTime = new Date();
            var result = null;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("GetFormValue", name);
                DCEventInterfaceLogFunction(rootElement, 'GetFormValue', startTime);
            }
            return result;
        };

        /**
       * @name GetHtmlText
       * @type function
       * @apinameZh 获得文档的Html文本代码
       * @classification file
       * @param ["IncludeSelectionOnly","boolean","是否只包含选择区域","","",true]
       * @returns ["result","string","文档Html文本"]
       */
        rootElement.GetHtmlText = function (IncludeSelectionOnly) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetHtmlText", IncludeSelectionOnly);
            DCEventInterfaceLogFunction(rootElement, 'GetHtmlText', startTime);
            return result;
        };

        /**
        * @name GetInputFieldInnerValue
        * @type function
        * @apinameZh 获得指定编号的输入域的InnerValue属性值。
        * @classification structuralelement
        * @param ["id","string","输入域编号","","",true]
        * @returns ["result","string","获得的属性值"]
        */
        rootElement.GetInputFieldInnerValue = function (id) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetInputFieldInnerValue", id);
            DCEventInterfaceLogFunction(rootElement, 'GetInputFieldInnerValue', startTime);
            return result;
        };



        /**
        * @name GetNavigateNodesString
        * @type function
        * @classification file
        * @apinameZh 获得导航节点字符串
        * @returns ["result","string","导航节点字符串"]
        */
        rootElement.GetNavigateNodesString = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetNavigateNodesString");
            DCEventInterfaceLogFunction(rootElement, 'GetNavigateNodesString', startTime);
            return result;
        };

        /**
        * @name GetNowDateTime
        * @type function
        * @classification editformat
        * @apinameZh 获得系统当前日期时间
        * @returns ["result","string","系统当前日期时间"]
        */
        rootElement.GetNowDateTime = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetNowDateTime");
            DCEventInterfaceLogFunction(rootElement, 'GetNowDateTime', startTime);
            return result;
        };
        /**
        * @name GetOptionValue
        * @type function
        * @classification file
        * @apinameZh 获得指定名称的选项数值,选项名称为“选项组名.选项名称”的格式
        * @param ["name","string","选项名称","","",true]
        * @returns ["result","string","选项数值"]
        * @describe 比如“ViewOptions.ShowParagraphFlag”。比如 string v = obj.GetOptionValue("ViewOptions.ShowParagraphFlag"); // 返回 "true"或"false"，string v2 = obj.GetOptionValue("ViewOptions.TagColorForNormalField");// 返回类似"#AAAAAA"等表示颜色值的字符串。
        */
        //  * 获得指定名称的选项数值, 选项名称为“选项组名.选项名称”的格式，比如“ViewOptions.ShowParagraphFlag”。
        // * 比如 string v = obj.GetOptionValue("ViewOptions.ShowParagraphFlag"); // 返回 "true"或"false"。
        // * string v2 = obj.GetOptionValue("ViewOptions.TagColorForNormalField");// 返回类似"#AAAAAA"等表示颜色值的字符串。
        rootElement.GetOptionValue = function (name) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetOptionValue", name);
            DCEventInterfaceLogFunction(rootElement, 'GetOptionValue', startTime);
            return result;
        };

        /**
        * name GetStyleIndexByString
        * @type function
        * @classification file
        * @apinameZh 获得样式在列表中的编号（内部接口不对客户开放，接口文档中不解析name前面不加@符号的接口）
        * @param ["styleString","string","样式字符串，比如“FontName: 宋体; FontSize: 9”","","",true]
        * @returns ["result","number","编号"]
        */
        rootElement.GetStyleIndexByString = function (styleString) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetStyleIndexByString", styleString);
            DCEventInterfaceLogFunction(rootElement, 'GetStyleIndexByString', startTime);
            return result;
        };

        /**
        * @name GetSubDoumentContentXmlByID
        * @type function
        * @classification subdoc
        * @apinameZh 获得指定编号的子文档内容XML字符串
        * @param ["id","string","子文档元素编号","","",true]
        * @returns ["result","string","获得的XML字符串"]
        */
        rootElement.GetSubDoumentContentXmlByID = function (id) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读下不允许修改
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("GetSubDoumentContentXmlByID", id);
            DCEventInterfaceLogFunction(rootElement, 'GetSubDoumentContentXmlByID', startTime);
            return result;
        };

        /**
        * @name GetSurplusLinesOfSpeifyPage
        * @type function
        * @classification file
        * @apinameZh 获得指定页的剩余空白行数
        * @param ["pageIndex","number","从1开始计算的页码号","","",true]
        * @param ["specifyLineHeight","number","指定的行高","","",true]
        * @returns ["result","number","剩余的空白行数"]
        */
        rootElement.GetSurplusLinesOfSpeifyPage = function (pageIndex, specifyLineHeight) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetSurplusLinesOfSpeifyPage", pageIndex, specifyLineHeight);
            DCEventInterfaceLogFunction(rootElement, 'GetSurplusLinesOfSpeifyPage', startTime);
            return result;
        };

        /**
        * @name GetTableCellText
        * @type function
        * @classification table
        * @apinameZh 获得表格单元格的文本内容
        * @param ["tableID","string","表格id","","",true]
        * @param ["rowIndex","number","从0开始计算的行号","","",true]
        * @param ["colIndex","number","从0开始计算的列号","","",true]
        * @returns ["result","string","单元格文本"]
        */
        rootElement.GetTableCellText = function (tableID, rowIndex, colIndex) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetTableCellText", tableID, rowIndex, colIndex);
            DCEventInterfaceLogFunction(rootElement, 'GetTableCellText', startTime);
            return result;
        };

        /**
       * @name InDesignMode
       * @type function
       * @classification file
       * @apinameZh 控件是否处于调试模式
       * @returns ["result","boolean","控件是否处于调试模式"]
       */
        rootElement.InDesignMode = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("InDesignMode");
            DCEventInterfaceLogFunction(rootElement, 'InDesignMode', startTime);
            return result;
        };

        /**
        * @name InsertSubDocuentAtCurrentPosition
        * @type function
        * @apinameZh 在当前位置处插入子文档元素
        * @classification subdoc
        * @param ["newSubDoc","object","要插入的子文档对象","null","{DCSoft.Writer.Dom.XTextSubDocumentElement}",true]
        * @param ["insertUp","boolean","插入位置","null","true:在上面插入；false:在下面插入",true]
        * @describe 仅用于插入一个空的病程，需要配合加载病程文档的接口使用，比如：LoadSubDocumentFromString
        */
        rootElement.InsertSubDocuentAtCurrentPosition = function (newSubDoc, insertUp) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读下不允许修改
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("InsertSubDocuentAtCurrentPosition", newSubDoc, insertUp);
            DCEventInterfaceLogFunction(rootElement, 'InsertSubDocuentAtCurrentPosition', startTime);
        };

        /**
        * @name IsValidateJSONString
        * @type function
        * @apinameZh 判断是否为合法的JSON字符串
        * @classification file
        * @param ["jsonstring","string","json字符串","","",true]
        * @returns ["result","boolean","是否合法"]
        * @change ["2023-07-24","增加接口","wyc" ]
        */
        rootElement.IsValidateJSONString = function (jsonstring) {
            var startTime = new Date();
            let result = false;
            try {
                var obj = JSON.parse(jsonstring);
                result = true;
            } catch { }
            DCEventInterfaceLogFunction(rootElement, 'IsValidateJSONString', startTime);
            return result;
        };

        /**
        * @name LoadDocumentFromBase64String
        * @type function
        * @apinameZh 以指定的格式从BASE64字符串加载文档内容
        * @classification file
        * @param ["text","string","BASE64字符串","","",true]
        * @param ["format","string","文件格式","","",true]
        * @param ["specifyLoadPart","string","指定位置","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.LoadDocumentFromBase64String = function (text, format, specifyLoadPart) {
            rootElement.CheckDisposed();
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                var args = {
                    WriterControl: rootElement,
                    Data: text,
                    Format: format,
                    SpecifyLoadPart: specifyLoadPart,
                    ErrorCallBack: null,
                    IsBase64String: true
                };
                result = WriterControl_IO.LoadDocumentFromString(args);
                if (result) {
                    rootElement.ClearOldVisibleElements();
                }
                DCEventInterfaceLogFunction(rootElement, 'LoadDocumentFromBase64String', startTime);

            }
            return result;
        };

        /**
        * @name LoadDocumentFromString
        * @type function
        * @apinameZh 从一个字符串中加载文档
        * @classification file
        * @param ["strData","string","文件内容","","","true"]
        * @param ["strFormat","string","文件格式","","","true"]
        * @param ["specifyLoadPart","string","指定位置","","","true"]
        * @param ["errorCallback","function","发生错误时的回调函数","","","true"]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2024-01-08","改进性能","袁永福" ]
        */
        rootElement.LoadDocumentFromString = function (strData, strFormat, specifyLoadPart, errorCallback = null) {
            rootElement.CheckDisposed();
            var startTime = new Date();
            var result = false;
            //模式判断
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            var RectInfo = rootElement.RectInfo;//区域选择
            //当前存在其中一个模式，即不可修改dom，返回false
            if (IsPrintPreview || ExtViewMode || RectInfo) {
                return false;
            }

            // if (strFormat === 'json' && rootElement.validateJSON(strData) == false) {
            //     if (typeof (errorCallback) == "function") {
            //         result = errorCallback.call(rootElement, false);
            //         DCEventInterfaceLogFunction(rootElement, 'LoadDocumentFromString', startTime);
            //         return result;
            //     }
            // }
            var args = {
                WriterControl: rootElement,
                Data: strData,
                Format: strFormat,
                SpecifyLoadPart: specifyLoadPart,
                ErrorCallBack: errorCallback,
                IsBase64String: false
            };
            WriterControl_IO.__LastXmlParserError = null;
            result = WriterControl_IO.LoadDocumentFromString(args);
            rootElement.ClearDocumentParameters();
            if (WriterControl_IO.__LastXmlParserError != null) {
                if (typeof (errorCallback) == "function") {
                    errorCallback.call(rootElement, WriterControl_IO.__LastXmlParserError);
                }
            }
            if (result) {
                rootElement.ClearOldVisibleElements();
            }
            DCEventInterfaceLogFunction(rootElement, 'LoadDocumentFromString', startTime);
            return result;
        };

        /**
        * @name IsValidateXML
        * @type function
        * @apinameZh 校验是否为xml格式
        * @classification file
        * @param ["xmlContent","string","xml字符串","","","true"]
        * @returns ["result","boolean","是否为xml格式"]
        */
        rootElement.IsValidateXML = function (xmlContent) {
            var startTime = new Date();
            const regex = /^<XTextDocument[\s\S]*<\/XTextDocument>$/;
            if (regex.test(xmlContent)) {
                //errorCode 0是xml正确，1是xml错误，2是无法验证
                var xmlDoc, errorMessage, errorCode = 0;
                var parser = new DOMParser();
                xmlDoc = parser.parseFromString(xmlContent, "text/xml");
                var error = xmlDoc.getElementsByTagName("parsererror");
                if (error.length > 0) {
                    if (xmlDoc.documentElement.nodeName == "parsererror") {
                        errorCode = 1;
                        errorMessage = xmlDoc.documentElement.childNodes[0].nodeValue;
                    } else {
                        errorCode = 1;
                        errorMessage = xmlDoc.getElementsByTagName("parsererror")[0].innerHTML;
                    }
                } else {
                    errorMessage = "格式正确";
                }

                DCEventInterfaceLogFunction(rootElement, 'IsValidateXML', startTime);
                return errorCode === 0 ? true : false;
            }
            return false;
        };

        /**
        * @name validateJSON
        * @type function
        * @apinameZh 校验是否为json格式
        * @classification file
        * @param ["jsonContent","string","json字符串","","",true]
        * @returns ["result","boolean","是否为json格式"]
        */
        rootElement.validateJSON = function (jsonContent) {
            var startTime = new Date();
            var validate = false;
            try {
                if (jsonContent && JSON.parse(jsonContent) && typeof JSON.parse(jsonContent) === 'object') {
                    validate = true;
                }
            } catch (error) {
                validate = false;
            }
            DCEventInterfaceLogFunction(rootElement, 'validateJSON', startTime);
            return validate;
        };


        /**
        * @name LockContentByElement
        * @type function
        * @classification file
        * @apinameZh 锁定文档元素的内容
        * @param ["element","object","文档元素，必须为容器类文档元素","","{DCSoft.Writer.Dom.XTextContainerElement }",true]
        * @param ["userID","string","用户id","","",true]
        * @param ["authoriseUserIDList","array","授权用户id列表","","",true]
        * @param ["logUndo","string","是否记录撤销操作信息","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2024-08-05","增加后台引用对象调用的判断","lxy" ]
        */
        rootElement.LockContentByElement = function (element, userID, authoriseUserIDList, logUndo) {
            var startTime = new Date();
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(element) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("LockContentByElementID2", element, userID, authoriseUserIDList, logUndo);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("LockContentByElementID", element, userID, authoriseUserIDList, logUndo);
            }
            DCEventInterfaceLogFunction(rootElement, 'LockContentByElement', startTime);
            return result;
        };

        /**
       * @name LockContentByElementID
       * @type function
       * @apinameZh 锁定文档元素的内容
       * @classification file
       * @param ["elementID","string","元素编号","","必须为一个容器类型的元素的编号",true]
       * @param ["userID","string","用户id","","",true]
       * @param ["authoriseUserIDList","string","授权用户id列表","","多个用户用英文逗号分割",true]
       * @param ["logUndo","string","是否记录撤销操作信息","","",true]
       * @returns ["result","boolean","操作是否成功"]
       * @change ["2024-08-05","增加后台引用对象调用的判断","lxy" ]
       * 
       */
        rootElement.LockContentByElementID = function (elementID, userID, authoriseUserIDList, logUndo) {
            var startTime = new Date();
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(elementID) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("LockContentByElementID2", elementID, userID, authoriseUserIDList, logUndo);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("LockContentByElementID", elementID, userID, authoriseUserIDList, logUndo);
            }
            DCEventInterfaceLogFunction(rootElement, 'LockContentByElementID', startTime);
            return result;
        };

        /**
      * @name LockContentByTableRow
      * @type function
      * @classification file
      * @apinameZh 锁定文档元素的内容
      * @param ["tableID","string","表格编号","","",true]
      * @param ["rowIndexBase","number","从0开始计算的表格行序号","","",true]
      * @param ["userID","string","锁定操作的用户ID","","",true]
      * @param ["authoriseUserIDList","array","授权用户id列表","","",true]
      * @param ["logUndo","string","是否记录撤销操作信息","","",true]
      * @returns ["result","boolean","操作是否成功"]
      */
        rootElement.LockContentByTableRow = function (tableID, rowIndexBase, userID, authoriseUserIDList, logUndo) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("LockContentByTableRow", tableID, rowIndexBase, userID, authoriseUserIDList, logUndo);
            DCEventInterfaceLogFunction(rootElement, 'LockContentByTableRow', startTime);
            return result;
        };

        /**
        * @name MoveDownOneLine
        * @type function
        * @apinameZh 移动光标到下一行
        * @classification cursor
        */
        rootElement.MoveDownOneLine = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("MoveDownOneLine");
            DCEventInterfaceLogFunction(rootElement, 'MoveDownOneLine', startTime);

        };

        /**
         * @name MoveEnd
         * @type function
         * @apinameZh 移动光标到行尾
         * @classification cursor
         */
        rootElement.MoveEnd = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("MoveEnd");
            DCEventInterfaceLogFunction(rootElement, 'MoveEnd', startTime);
        };

        /**
         * @name MoveHome
         * @type function
         * @apinameZh 移动光标到行首
         * @classification cursor
         */
        rootElement.MoveHome = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("MoveHome");
            DCEventInterfaceLogFunction(rootElement, 'MoveHome', startTime);
        };

        /**
         * @name MoveLeft
         * @type function
         * @apinameZh 向左移动光标
         * @classification cursor
         */
        rootElement.MoveLeft = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("MoveLeft");
            DCEventInterfaceLogFunction(rootElement, 'MoveLeft', startTime);
        };

        /**
         * @name MoveRight
         * @type function
         * @apinameZh 向右移动光标
         * @classification cursor
         */
        rootElement.MoveRight = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("MoveRight");
            DCEventInterfaceLogFunction(rootElement, 'MoveRight', startTime);
        };

        /**
         * @name MoveTo
         * @type function
         * @apinameZh 将插入点移动到指定位置
         * @classification cursor
         * @param ["target","object","移动的目标","","{DCSoft.Writer.MoveTarget}",true]
         */
        rootElement.MoveTo = function (target) {
            var startTime = new Date();
            //20240403 lixinyu 修复AfterCell不生效问题，防止客户使用报错(DUWRITER5_0-2195)
            if (target === 'AfterCell') {
                target = 'NextCell';
            }
            rootElement.__DCWriterReference.invokeMethod("MoveTo", target);
            DCEventInterfaceLogFunction(rootElement, 'MoveTo', startTime);
        };

        /**
        * @name MoveToElement
        * @type function
        * @apinameZh 将光标移动到指定元素
        * @param ["element","string","元素",null,"指定的元素的ID或NativeHandle",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2024-08-22","创建接口","wyc" ]
        * @classification view
        */
        rootElement.MoveToElement = function (element) {
            var startTime = new Date();
            let result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("MoveToElement", element);
            }
            DCEventInterfaceLogFunction(rootElement, 'MoveToElement', startTime);
            return result;
        };

        /**
        * @name MoveToClientPosition
        * @type function
        * @classification cursor
        * @apinameZh 移动当前光标位置到指定客户区坐标位置处
        * @param ["clientX","number","控件客户区X坐标，像素单位","","",true]
        * @param ["clientY","number","控件客户区Y坐标，像素单位","","",true]
        * @returns ["result","boolean","操作是否修改了插入点"]
        */
        rootElement.MoveToClientPosition = function (clientX, clientY) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("MoveToClientPosition", clientX, clientY);
            DCEventInterfaceLogFunction(rootElement, 'MoveToClientPosition', startTime);
            return result;
        };

        /**
        * @name MoveToPosition
        * @type function
        * @classification cursor
        * @apinameZh 移动光标到指定位置处
        * @param ["index","number","位置序号","","",true]
        */
        rootElement.MoveToPosition = function (index) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("MoveToPosition", index);
            DCEventInterfaceLogFunction(rootElement, 'MoveToPosition', startTime);
        };

        /**
        * @name MoveUpOneLine
        * @type function
        * @apinameZh 移动光标到上一行
        * @classification cursor
        */
        rootElement.MoveUpOneLine = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("MoveUpOneLine");
            DCEventInterfaceLogFunction(rootElement, 'MoveUpOneLine', startTime);
        };


        /**
         * @name Paste
         * @type function
         * @apinameZh 粘贴
         * @classification editformat
         * @param ["eventObject","object","粘贴事件对象","","",false]
         */
        rootElement.Paste = function (eventObject) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                WriterControl_UI.GetClipboardData(eventObject, rootElement);
                DCEventInterfaceLogFunction(rootElement, 'Paste', startTime);
            }
        };

        /**
         * @name PrintCurrentPage
         * @type function
         * @apinameZh 打印当前页
         * @classification print
         */
        rootElement.PrintCurrentPage = function () {
            var startTime = new Date();
            var RectInfo = rootElement.RectInfo;//区域选择
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            if (RectInfo || ExtViewMode) {
                return false;
            }
            WriterControl_Print.Print(rootElement, { PrintRange: "CurrentPage" });
            //rootElement.__DCWriterReference.invokeMethod("PrintCurrentPage");
            DCEventInterfaceLogFunction(rootElement, 'PrintCurrentPage', startTime);
        };

        /**
         * @name ClosePrintPreview
         * @type function
         * @apinameZh 关闭打印预览界面，恢复编辑界面
         * @classification print
         */
        rootElement.ClosePrintPreview = function () {
            var startTime = new Date();
            WriterControl_Print.ClosePrintPreview(rootElement, true);
            //判断是否为打印预览开启区域选择打印
            if (rootElement.RectInfo && rootElement.RectInfo.printPreviewOpen) {
                rootElement.SetBoundsSelectionViewMode(false);
            }
            // 关闭打印预览界面时，修正编辑界面的区域选择打印蒙版位置
            if (rootElement.RectInfo && rootElement.RectInfo.printPreviewOpen == false && typeof (rootElement.RectInfo.AdjustBoundsSelectionStyle) == "function") {
                rootElement.RectInfo.AdjustBoundsSelectionStyle();
            }
            //WriterControl_Paint.InvalidateAllView(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'ClosePrintPreview', startTime);
            //rootElement.RefreshDocument();
        };

        /**
         * @name LoadPrintPreview
         * @type function
         * @apinameZh 展示打印预览界面
         * @param ["options","any","选项","","",true]
         * @classification print
         */
        rootElement.LoadPrintPreview = function (options) {
            var startTime = new Date();
            // 自此处判断是否为还存在event队列事件未处理
            if (WriterControl_Task.__Tasks && WriterControl_Task.__Tasks.length > 0) {
                WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                    rootElement.LoadPrintPreview(options);
                });
                return;
            }
            WriterControl_Print.PrintPreview(rootElement, options);
            DCEventInterfaceLogFunction(rootElement, 'LoadPrintPreview', startTime);
        };

        /**
        * @name LoadPrintPreviewWithPermissionMark
        * @type function
        * @classification print
        * @apinameZh 加载带有权限标记的打印预览
        * @change ["2023-06-28","兼容四代接口","wyc" ]
        * @param ["options","any","选项","","",true]
        */
        rootElement.LoadPrintPreviewWithPermissionMark = function (options) {
            var startTime = new Date();
            if (typeof (options) === "object") {
                options.CleanMode = false;
            } else {
                options = new Object();
                options.CleanMode = false;
            }
            // 处理加载文档后直接调用留痕预览时标尺展示的问题
            rootElement.LoadPrintPreview(options);
            // WriterControl_Print.PrintPreview(rootElement, options);
            DCEventInterfaceLogFunction(rootElement, 'LoadPrintPreviewWithPermissionMark', startTime);
        };

        /**
        * @name IsPrintPreview
        * @type function
        * @classification print
        * @apinameZh 判断是否为打印预览
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.IsPrintPreview = function () {
            var startTime = new Date();
            var IsPrintPreview = false;
            //判断是否存在dctype="page-printpreview"
            if (rootElement != null) {
                var hasPrintPreview = rootElement.querySelector('[dctype=page-printpreview]');
                if (hasPrintPreview) {
                    IsPrintPreview = true;
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'IsPrintPreview', startTime);
            return IsPrintPreview;
        };

        rootElement.SetPageContainerVisible = function (bolVisible) {
            // 编辑模式下可以显示和隐藏
            // 打印预览模式下不能给客户使用
            if (rootElement.IsPrintPreview() == false) {
                return WriterControl_Print.SetPageContainerVisible(rootElement, bolVisible);
            }
        };

        /**
        * @name Redo
        * @type function
        * @apinameZh 重做
        * @classification editformat
        */
        rootElement.Redo = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                rootElement.__DCWriterReference.invokeMethod("Redo");
                DCEventInterfaceLogFunction(rootElement, 'Redo', startTime);
            }
        };

        /**
         * @name RefreshDocument
         * @type function
         * @apinameZh 刷新
         * @classification editformat
         */
        rootElement.RefreshDocument = function () {
            rootElement.CheckDisposed();
            // RefreshDocument和RefreshInnerView添加在阅读模式或者打印预览模式禁止调用，防止编辑器排版变化【DUWRITER5_0-3960】
            var ReadViewMode = rootElement.ReadViewMode;//阅读模式
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            if (ReadViewMode || IsPrintPreview) {
                //阅读预览模式下禁止调用
                return false;
            }
            var startTime = new Date();
            //wyc20230609:换个写法试试
            rootElement.__DCWriterReference.invokeMethod("RefreshInnerView", false);
            WriterControl_Paint.InvalidateAllViewForce(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'RefreshDocument', startTime);
        };

        /**
        * @name RefreshInnerView
        * @type function
        * @classification editformat
        * @apinameZh 刷新文档内部排版和分页。不更新用户界面。
        * @param ["fastMode","boolean","","","",true]
        */
        rootElement.RefreshInnerView = function (fastMode) {
            rootElement.CheckDisposed();
            // RefreshDocument和RefreshInnerView添加在阅读模式或者打印预览模式禁止调用，防止编辑器排版变化【DUWRITER5_0-3960】
            var ReadViewMode = rootElement.ReadViewMode;//阅读模式
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            if (ReadViewMode || IsPrintPreview) {
                //阅读预览模式下禁止调用
                return false;
            }
            var startTime = new Date();
            if (typeof (fastMode) !== "boolean") {
                fastMode = false;
            }
            rootElement.__DCWriterReference.invokeMethod("RefreshInnerView", fastMode);
            DCEventInterfaceLogFunction(rootElement, 'RefreshInnerView', startTime);
        };

        /**
        * @name RejectUserTrace
        * @type function
        * @apinameZh 拒绝对文档的修订
        * @classification editformat
        */
        rootElement.RejectUserTrace = function () {
            var startTime = new Date();
            var readonly = rootElement.Readonly;//只读
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            var RectInfo = rootElement.RectInfo;//区域选择
            if (readonly || ExtViewMode || RectInfo) {
                //只读、续打、区域选择时禁用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("RejectUserTrace");
            DCEventInterfaceLogFunction(rootElement, 'RejectUserTrace', startTime);
            return result;
        };

        /**
        * @name ResetFormValue
        * @type function
        * @apinameZh 重置表单元素默认值,需要刷新文档
        * @classification editformat
        * @returns ["result","boolean","是否导致文档内容发生改变"]
        */
        rootElement.ResetFormValue = function () {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("ResetFormValue");
                DCEventInterfaceLogFunction(rootElement, 'ResetFormValue', startTime);
            }
            return result;
        };

        /**
        * @name ResignCurrentElement
        * @type function
        * @apinameZh 对当前签名进行重新签名操作
        * @classification file
        * @returns ["result","boolean","是否导致文档内容发生改变"]
        */
        rootElement.ResignCurrentElement = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("ResignCurrentElement");
            DCEventInterfaceLogFunction(rootElement, 'ResignCurrentElement', startTime);
            return result;
        };

        /**
        * @name ResignCurrentElementSpecifyMode
        * @type function
        * @apinameZh 对当前签名进行指定模式的重新签名操作
        * @classification file
        * @param ["mode","string","指定的模式","","模式有：Default，Normal，SignHand",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.ResignCurrentElementSpecifyMode = function (mode) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("ResignCurrentElementSpecifyMode", mode);
            DCEventInterfaceLogFunction(rootElement, 'ResignCurrentElementSpecifyMode', startTime);
            return result;
        };

        /**
        * @name SaveDocumentToBase64String
        * @type function
        * @apinameZh 保存文档为BASE64字符串
        * @classification file
        * @param ["options","object","保存文档需要的一系列参数对象","","",true]
        * @param ["callBack","function","用于异步时返回数据","","",true]
        * @returns ["result","string","输出的BASE64字符串"]
        * @change ["2023-06-21","修改参数兼容四代接口","wyc" ]
        * @change ["2024-4-8","绕过某些情况保存文档公式结果算不出来的问题","wyc" ]
        */
        rootElement.SaveDocumentToBase64String = function (options, callBack) {
            var startTime = new Date();
            if ((typeof (options) === "string" && options === 'pdf') || (options && options.FileFormat && options.FileFormat === 'pdf')) {
                //wyc20230811:保存PDF的BASE64的功能改成走四代服务转发
                var filecontent = rootElement.SaveDocumentToString();
                var options = {
                    "files": [filecontent],
                    "resulttype": "Base64String"
                };
                let result = rootElement.GetPDFByFiles(options, callBack);
                DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToBase64String', startTime);
                return result;
            } else {
                if (typeof (options) === "string"
                    //wyc20240408:绕过某些情况保存文档公式结果算不出来的问题DUWRITER5_0-2230
                    || (options == null && callBack == null)
                ) {
                    var content = rootElement.__DCWriterReference.invokeMethod("SaveDocumentToBase64String", options);
                    content = content.replace('PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4K', '');
                    DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToBase64String', startTime);
                    return content;
                }
                if (typeof (options) !== "object") {
                    options = new Object();
                }
                options.SaveBase64String = true;
                callBack && callBack(rootElement.SaveDocumentToString(options));
                let result = rootElement.SaveDocumentToString(options);
                DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToBase64String', startTime);
                return result;
                //return rootElement.__DCWriterReference.invokeMethod("SaveDocumentToBase64String", options);
            }

        };

        /**
        * @name SaveDocumentToString
        * @type function
        * @classification file
        * @apinameZh 保存文件到一个字符串中
        * @param ["SAVETYPE","object","文档保存的一系列参数","","",true]
        * @returns ["result","string","文件内容的字符串"]
        * @change ["2023-06-21","修改参数兼容四代接口","wyc" ]
        * @change ["2024-03-27","改进大文档的性能","yyf"]
        */
        // { //文档保存的一系列参数
        //   FileFormat: "xml",
        //   CommitUserTrace: false,
        //   OutputFormatXML: false,
        //   EncodingName: "utf-8",
        //   SaveBase64String: "false",
        //   SpecifySavePart: null,
        //   ClearDataBindingContent: false,
        //   InsertLastTableRowToPageBottom: false,
        //   AttachedCustomAttributes: null, 
        //    }
        rootElement.SaveDocumentToString = function (options, textOptions = null) {
            var startTime = new Date();
            //var tick = new Date().valueOf();
            var content = null;
            if (options && options == 'text') {
                content = DCTools20221228.UnPackageStringValue(rootElement.__DCWriterReference.invokeMethod("SaveDocumentToText", textOptions));
            } else {
                content = DCTools20221228.UnPackageStringValue(rootElement.__DCWriterReference.invokeMethod("SaveDocumentToString", options));
            }
            if (options && (options == 'text' || options == 'json' || (typeof options.FileFormat == 'string' && options.FileFormat.toLowerCase() == 'json'))) {
                //判断json解析是否报错
                try {
                    JSON.parse(content);
                } catch (err) {
                    //判断是否为<开头
                    if (typeof content == 'string' && content.indexOf('<') == 0) {
                        var eleIndex = content.indexOf(`"Elements":
        [
        {"Type":"Header",`);
                        if (eleIndex < 0) {
                            eleIndex = content.indexOf(`"Elements":[{"Type":"Header",`);
                        }
                        if (eleIndex >= 0) {
                            var eleStr = '{' + content.substring(eleIndex);
                            var newContent = {};
                            newContent.Type = "DCDocument2022";
                            newContent.BodyText = rootElement.BodyText;
                            //此处使用这种写法是loaddocumentfromstring需要type顺序
                            content = JSON.parse(eleStr);
                            for (var attr in content) {
                                newContent[attr] = content[attr];
                            }
                            content = JSON.stringify(newContent);
                        }

                    }
                }
            }
            //判断是否为options && options.SaveBase64String 为true
            if (options && (options.SaveBase64String == 'true' || options.SaveBase64String == 'True' || options.SaveBase64String === true)) {
                content = content.replace('PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4K', '');
            }
            if ((options === 'xml' && !rootElement.IsValidateXML(content)) || (options === 'json' && !rootElement.validateJSON(content))) {
                if (!!rootElement.EventSaveDocumentToStringValidate && typeof (rootElement.EventSaveDocumentToStringValidate) == "function") {
                    rootElement.EventSaveDocumentToStringValidate.call(rootElement, false);
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToString', startTime);
            return content;
        };

        /**
        * @name GetLongImageData
        * @type function
        * @classification file
        * @apinameZh 获得文档的长图片数据
        * @param ["callBack","function","回调函数","","返回的参数是base64类型",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.GetLongImageData = function (callBack) {
            return WriterControl_IO.GetLongImageData(rootElement, callBack);
        };

        /**
         * @name ScrollIntoView
         * @type function
         * @apinameZh 滚动视图到node元素处
         * @classification view
         * @param ["id","string","元素id字符串","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.ScrollIntoView = function (id) {
            var startTime = new Date();
            if (!id || typeof (id) != "string") {
                return false;
            }
            let result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.FocusElementById(id);
            }
            DCEventInterfaceLogFunction(rootElement, 'ScrollIntoView', startTime);
            return result;
        };

        /**
         * @name ScrollToCaret
         * @type function
         * @apinameZh 滚动视图到光标位置
         * @classification view
         */
        rootElement.ScrollToCaret = function () {
            var startTime = new Date();
            //rootElement.__DCWriterReference.invokeMethod("ScrollToCaret");
            rootElement.Focus();
            DCEventInterfaceLogFunction(rootElement, 'ScrollToCaret', startTime);
        };

        /**
        * @name SelectAll
        * @type function
        * @apinameZh 选中文档所有内容
        * @classification file
        */
        rootElement.SelectAll = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("SelectAll");
            DCEventInterfaceLogFunction(rootElement, 'SelectAll', startTime);
        };

        /**
         * @name SelectContentByStartEndElement
         * @type function
         * @apinameZh 选择内容
         * @classification file
         * @param ["startElement","string","选择区域起始元素","","支持id",true]
         * @param ["endElement","string","选择区域终止元素","","支持id",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SelectContentByStartEndElement = function (startElement, endElement) {
            var startTime = new Date();
            let result = false;
            if (DCTools20221228.IsDotnetReferenceElement(startElement) === true && DCTools20221228.IsDotnetReferenceElement(endElement) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SelectContentByStartEndElement", startElement, endElement);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SelectContentByStartEndElementID", startElement, endElement);
            }
            DCEventInterfaceLogFunction(rootElement, 'SelectContentByStartEndElement', startTime);
            return result;
        };

        /**
         * @name SelectContentByStartEndIndex
         * @type function
         * @apinameZh 选择内容
         * @classification file
         * @param ["startContentIndex","number","选择区域起始编号","","",true]
         * @param ["endContentIndex","number","选择区域终止编号","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SelectContentByStartEndIndex = function (startContentIndex, endContentIndex) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SelectContentByStartEndIndex", startContentIndex, endContentIndex);
            DCEventInterfaceLogFunction(rootElement, 'SelectContentByStartEndIndex', startTime);
            return result;
        };

        /**
        * @name SelectElementById
        * @type function
        * @apinameZh 选中文档元素
        * @classification file
        * @param ["id","number","文档元素编号","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2024-6-6","扩展参数支持传入nativehandle/.net后台对象","wyc" ]
        */
        rootElement.SelectElementById = function (id) {
            var startTime = new Date();
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SelectElementById2", id);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SelectElementById", id);
            }
            DCEventInterfaceLogFunction(rootElement, 'SelectElementById', startTime);
            return result;
        };

        /**
        * @name SetCustomAttribute
        * @type function
        * @classification attribute
        * @apinameZh 设置自定义属性值
        * @param ["name","string","属性名","","",true]
        * @param ["Value","string","属性值","","",true]
        */
        rootElement.SetCustomAttribute = function (name, Value) {
            var startTime = new Date();
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("SetCustomAttribute", name, Value);
            DCEventInterfaceLogFunction(rootElement, 'SetCustomAttribute', startTime);
            return result;
        };

        /**
        * @name SetDocumentParameterEnabled
        * @type function
        * @apinameZh 设置参数是否有效
        * @classification file
        * @param ["parameterName","string","参数名","","",true]
        * @param ["enabled","boolean","是否有效","","",true]
        */
        rootElement.SetDocumentParameterEnabled = function (parameterName, enabled) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("SetDocumentParameterEnabled", parameterName, enabled);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentParameterEnabled', startTime);
        };

        /**
       * @name SetDocumentParameterValue
       * @type function
       * @apinameZh 设置文档参数值
       * @classification file
       * @param ["name","string","参数名","","",true]
       * @param ["Value","object","新的参数值","","",true]
       */
        rootElement.SetDocumentParameterValue = function (name, Value) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let isflag = true;
            //wyc20230814:允许绑单个字符串
            var typestr = typeof (Value);
            if (Array.isArray(Value) === true || typeof (Value) === "object" || typeof (Value) === "string") {
                //绑定的实体数据只允许数组/对象/字符串
                if (Array.isArray(Value) === true) {
                    for (var i = 0; i < Value.length; i++) {
                        let item = Value[i];
                        if (item instanceof Array) {
                            isflag = false;
                        } else {
                            //将Value中的换行标签<br/>替换为\n
                            if (item && item.value && typeof item.value === 'string') {
                                item.value = item.value.replace(/<br\s*\/?>|<\/br>/gi, "\n");
                            }

                        }
                    }
                } else {
                    // 单个对象时，将Value中的换行标签<br/>替换为\n
                    if (Value && Value.value && typeof Value.value === 'string') {
                        Value.value = Value.value.replace(/<br\s*\/?>|<\/br>/gi, "\n");
                    }
                }
            } else {
                isflag = false;
            }
            if (isflag === true) {
                isflag = rootElement.__DCWriterReference.invokeMethod("SetDocumentParameterValue", name, Value);
                DCEventInterfaceLogFunction(rootElement, 'SetDocumentParameterValue', startTime);
            } else {
                console.log("SetDocumentParameterValue参数格式有误，请检查");
            }

            return isflag;
        };

        /**
         * @name SetDocumentParameterValueXml
         * @type function
         * @classification file
         * @apinameZh 设置XML格式的文档参数值
         * @param ["name","string","参数名","","",true]
         * @param ["xmlText","string","参数值","","",true]
         */
        rootElement.SetDocumentParameterValueXml = function (name, xmlText) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("SetDocumentParameterValueXml", name, xmlText);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentParameterValueXml', startTime);
        };

        /**
         * @name SetDomImageByBase64String
         * @type function
         * @classification file
         * @apinameZh 设置DOM使用的图标
         * @param ["key","string","图标标号","","DCSoft.Writer.DCStdImageKey",true]
         * @param ["base64String","string","图片对象","","需要是base64String的图片对象",true]
         * @returns ["boolean","操作是否成功"]
         * @description "第一个参数key仅解析：RadioBoxUnChecked、RadioBoxChecked、CheckBoxUnChecked、CheckBoxChecked"
         */
        rootElement.SetDomImageByBase64String = function (key, base64String) {
            var startTime = new Date();
            var bolResult = rootElement.__DCWriterReference.invokeMethod("SetDomImageByBase64String", key, base64String);
            if (bolResult == true) {
                WriterControl_Paint.RefreshStandardImageList();
            }
            DCEventInterfaceLogFunction(rootElement, 'SetDomImageByBase64String', startTime);
            return bolResult;
        };

        /**
        * @name SetElementAttribute
        * @type function
        * @apinameZh 设置文档元素的属性值
        * @classification attribute
        * @param ["id","string","元素编号","","",true]
        * @param ["attributeName","string","属性名","","",true]
        * @param ["attributeValue","string","属性值","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetElementAttribute = function (id, attributeName, attributeValue) {
            var startTime = new Date();

            let result = false;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementAttribute2", id, attributeName, attributeValue);
            } else if (typeof (id) === "string" || typeof (id) === "number") {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementAttribute", id, attributeName, attributeValue);
            }
            //let result = rootElement.__DCWriterReference.invokeMethod("SetElementAttribute", id, attributeName, attributeValue);
            DCEventInterfaceLogFunction(rootElement, 'SetElementAttribute', startTime);
            return result;
        };

        /**
        * @name SetElementChecked
        * @type function
        * @apinameZh 设置单 / 复选框的勾选状态
        * @classification structuralelement
        * @param ["id","string","文档元素编号","","",true]
        * @param ["newChecked","boolean","新的勾选状态","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetElementChecked = function (id, newChecked) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SetElementChecked", id, newChecked);
            DCEventInterfaceLogFunction(rootElement, 'SetElementChecked', startTime);
            return result;
        };

        /**
        * @name SetElementProperty
        * @type function
        * @apinameZh 设置文档元素自定义属性
        * @classification attribute
        * @param ["id","string","文档元素编号","","",true]
        * @param ["name","string","属性名","","",true]
        * @param ["Value","string","属性值","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetElementProperty = function (id, name, Value) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SetElementProperty", id, name, Value);
            DCEventInterfaceLogFunction(rootElement, 'SetElementProperty', startTime);
            return result;
        };

        /**
        * @name SetElementText
        * @type function
        * @apinameZh 设置文档元素文本内容
        * @classification structuralelement
        * @param ["id","string","文档元素编号","","元素编号或元素的后台.NET引用对象",true]
        * @param ["text","string","文本值","","",true]
        * @param ["isFocusElementTextEnd","Boolean","是否同时将光标定位到文字后面","false","",false]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-11-21","增加了一个参数isFocusElementTextEnd","lixinyu" ]
        * @change ["2024-12-2","增加了一个参数isFocusContainer","lixinyu" ]
        */
        rootElement.SetElementText = function (id, text, isFocusElementTextEnd = false, isFocusContainer = true) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = false;
            let focusElement = null;
            //wyc20230724添加判断：
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementText", id, text, false, isFocusContainer);
                focusElement = rootElement.GetElementProperties(rootElement.CurrentElement());//获取设置文字的元素
            } else if (typeof (id) === "string" || typeof (id) === "number") {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementTextByID", id, text, false, isFocusContainer);
                focusElement = rootElement.GetElementProperties(id);//获取设置文字的元素
            }
            //光标定位到文字后方
            if (result && isFocusElementTextEnd && focusElement) {
                rootElement.FocusAdjacent('beforeEnd', focusElement);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetElementText', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("SetElementText", id, text);
        };


        /**
        * @name SetElementTextByID
        * @type function
        * @apinameZh 设置文档元素文本内容
        * @classification structuralelement
        * @param ["ELEMENT","object","文档元素编号","","元素编号或元素的后台.NET引用对象",true]
        * @param ["text","string","文本值,如果存在换行br标签会自动转为\/n换行","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2024-9-9","后台新增撤销操作","wyc" ]
        * @change ["2024-12-2","增加了一个参数isFocusContainer","lixinyu" ]
        */
        rootElement.SetElementTextByID = function (id, text, isAddTraces = false, isFocusContainer = true) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            if (text && text.length) {
                text = text.replace(/<br\s*\/?>|<\/br>/gi, "\n");
                console.log(text);
            }
            let result = false;
            //wyc20230724添加判断：
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementText", id, text, isAddTraces, isFocusContainer);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementTextByID", id, text, isAddTraces, isFocusContainer);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetElementTextByID', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("SetElementTextByID", id, text);
        };

        /**
        * @name SetElementVisible
        * @type function
        * @apinameZh 设置文档元素可见性
        * @classification structuralelement
        * @param ["id","string","文档元素编号","","元素编号或元素的后台.NET引用对象",true]
        * @param ["visible","boolean","可见性","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetElementVisible = function (id, visible) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SetElementVisible", id, visible);
            DCEventInterfaceLogFunction(rootElement, 'SetElementVisible', startTime);
            return result;
        };


        /**
        * @name SetInputFieldInnerValue
        * @type function
        * @classification structuralelement
        * @apinameZh 设置指定编号的输入域的InnerValue属性值。
        * @param ["id","string","输入域编号","","",true]
        * @param ["newValue","string","新的属性值","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetInputFieldInnerValue = function (id, newValue) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SetInputFieldInnerValue", id, newValue);
            DCEventInterfaceLogFunction(rootElement, 'SetInputFieldInnerValue', startTime);
            return result;
        };

        /**
        * @name SetInputFieldSelectedIndexs
        * @classification structuralelement
        * @type function
        * @apinameZh 设置输入域选择多个下拉项目
        * @param ["id","string","输入域编号","","",true]
        * @param ["indexs","string","下拉序号","","从0开始的下拉项目序号，各个序号之间用逗号分开",true]
        * @returns ["result","boolean","操作是否修改文档内容"]
        */
        rootElement.SetInputFieldSelectedIndexs = function (id, indexs) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetInputFieldSelectedIndexs", id, indexs);
            DCEventInterfaceLogFunction(rootElement, 'SetInputFieldSelectedIndexs', startTime);
            return result;
        };

        /**
        * @name SetOptionValue
        * @type function
        * @apinameZh 设置选项值
        * @classification file
        * @param ["name","string","选项名称","","",true]
        * @param ["Value","string","新的选项值","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @describe 选项名称为“选项组名.选项名称”的格式，比如“ViewOptions.ShowParagraphFlag”。比如 obj.SetOptionValue("ViewOptions.ShowParagraphFlag", "true");obj.SetOptionValue("ViewOptions.TagColorForNormalField", "#AAAAAA");
        */
        //   * 设置选项值, 选项名称为“选项组名.选项名称”的格式，比如“ViewOptions.ShowParagraphFlag”。
        // * 比如 obj.SetOptionValue("ViewOptions.ShowParagraphFlag", "true");
        // * obj.SetOptionValue("ViewOptions.TagColorForNormalField", "#AAAAAA");
        rootElement.SetOptionValue = function (name, Value) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetOptionValue", name, Value);
            DCEventInterfaceLogFunction(rootElement, 'SetOptionValue', startTime);
            return result;
        };

        /**
       * @name SetTableCellText
       * @type function
       * @classification table
       * @apinameZh 设置单元格文本值
       * @param ["tableID","string","表格编号","","","true"]
       * @param ["rowIndex","number","从0开始计算的行号","","","true"]
       * @param ["colIndex","number","从0开始计算的列号","","","true"]
       * @param ["newText","string","新的文本","","","true"]
       * @returns ["result","boolean","操作是否成功"]
       */
        rootElement.SetTableCellText = function (tableID, rowIndex, colIndex, newText) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetTableCellText", tableID, rowIndex, colIndex, newText);
            DCEventInterfaceLogFunction(rootElement, 'SetTableCellText', startTime);
            return result;
        };


        /**
      * @name ClearContainerTextByHandle
      * @type function
      * @classification table
      * @apinameZh 根据handle清空容器的内容
      * @param ["handleID","string","容器handle","","","true"]
      * @returns ["result","boolean","操作是否成功"]
      */
        rootElement.ClearContainerTextByHandle = function (handleID) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("ClearContainerTextByHandle", handleID);
            DCEventInterfaceLogFunction(rootElement, 'ClearContainerTextByHandle', startTime);
            return result;
        };


        /**
         * @name ShowAboutDialog
         * @type function
         * @apinameZh 显示关于对话框
         * @classification file
         * @param ["flag","boolean","是否弹出alert提示","true","","false"]
         * @returns ["result","json","参数为false时会返回json数据"]
         */
        rootElement.ShowAboutDialog = function (flag = true) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("ShowAboutDialog", flag);
            DCEventInterfaceLogFunction(rootElement, 'ShowAboutDialog', startTime);
            return result;
        };

        /**
        * @name UpdateDataSourceForView
        * @type function
        * @classification datasource
        * @apinameZh 将文档中的数据写入到数据源中
        * @describe 请改用WriteDataFromDocumentToDataSource().
        */
        rootElement.UpdateDataSourceForView = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("UpdateDataSourceForView");
            DCEventInterfaceLogFunction(rootElement, 'UpdateDataSourceForView', startTime);
            return result;
        };

        /**
        * @name UpdateViewForDataSource
        * @type function
        * @classification datasource
        * @apinameZh 将数据源中的数据写入到文档中
        * @describe 请改用WriteDataFromDataSourceToDocument().
        */
        rootElement.UpdateViewForDataSource = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("UpdateViewForDataSource");
            DCEventInterfaceLogFunction(rootElement, 'UpdateViewForDataSource', startTime);
            return result;
        };


        /**
        * @name UserLogin
        * @type function
        * @apinameZh 用户登录
        * @classification file
        * @param ["userID","string","用户编号","","",true]
        * @param ["userName","string","用户名","","",true]
        * @param ["permissionLevel","string","用户等级","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.UserLogin = function (userID, userName, permissionLevel) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("UserLogin", userID, userName, permissionLevel);
            //DUWRITER5_0-3715 lxy 20241014后台在登录后修改了文档选项，前端需要同步修改
            if (result) {
                rootElement.DocumentOptions.SecurityOptions.EnablePermission = true;
                rootElement.DocumentOptions.SecurityOptions.EnableLogicDelete = true;
                rootElement.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = true;
                rootElement.DocumentOptions.SecurityOptions.ShowPermissionMark = true;
                rootElement.DocumentOptions.SecurityOptions.ShowPermissionTip = true;
                rootElement.ApplyDocumentOptions();
            }
            DCEventInterfaceLogFunction(rootElement, 'UserLogin', startTime);
            return result;
        };

        /**
        * @name UserLoginByParameter
        * @type function
        * @apinameZh 根据参数进行用户登录
        * @classification file
        * @param ["userID","string","用户编号","","",true]
        * @param ["userName","string","用户名","","",true]
        * @param ["permissionLevel","string","用户等级","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.UserLoginByParameter = function (userID, userName, permissionLevel) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("UserLoginByParameter", userID, userName, permissionLevel);
            //DUWRITER5_0-3715 lxy 20241014后台在登录后修改了文档选项，前端需要同步修改
            if (result) {
                rootElement.DocumentOptions.SecurityOptions.EnablePermission = true;
                rootElement.DocumentOptions.SecurityOptions.EnableLogicDelete = true;
                rootElement.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = true;
                rootElement.DocumentOptions.SecurityOptions.ShowPermissionMark = true;
                rootElement.DocumentOptions.SecurityOptions.ShowPermissionTip = true;
                rootElement.ApplyDocumentOptions();
            }
            DCEventInterfaceLogFunction(rootElement, 'UserLoginByParameter', startTime);
            return result;
        };

        /**
        * @name UserLoginByUserLoginInfo
        * @type function
        * @classification file
        * @apinameZh 根据用户登录信息执行用户登录操作
        * @param ["loginInfo","object","登录信息","","DCSoft.Writer.Security.UserLoginInfo",true]
        * @param ["updateUI","boolean","是否更新用户界面","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.UserLoginByUserLoginInfo = function (loginInfo, updateUI) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("UserLoginByUserLoginInfo", loginInfo, updateUI);

            //后台在登录后修改了文档选项，前端需要同步修改
            if (result) {
                rootElement.DocumentOptions.SecurityOptions.EnablePermission = true;
                rootElement.DocumentOptions.SecurityOptions.EnableLogicDelete = true;
                rootElement.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = true;
                rootElement.DocumentOptions.SecurityOptions.ShowPermissionMark = true;
                rootElement.DocumentOptions.SecurityOptions.ShowPermissionTip = true;
                rootElement.ApplyDocumentOptions();
            }
            DCEventInterfaceLogFunction(rootElement, 'UserLoginByUserLoginInfo', startTime);
            //根据返回值判断是否要修改modified属性
            Boolean(result) && (rootElement.Modified = true);
            return result;
        };

        /**
        * @name WriteDataFromDataSourceToDocument
        * @type function
        * @classification datasource
        * @apinameZh 将数据源中的数据写入到文档中
        * @param ["dataset","object","绑定的数据集JSON对象","","",true]
        * @returns ["result","number","更新的数据点个数"]
        */
        rootElement.WriteDataFromDataSourceToDocument = function (dataset) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("WriteDataFromDataSourceToDocument", dataset);
            if (Boolean(result)) {
                // 根据返回值判断是否要修改modified属性
                rootElement.Modified = true;
                // 触发文档内容变化事件
                var opt = {
                    /** 触发事件类型 */
                    TriggerType: "WriteDataFromDataSourceToDocument"
                };
                WriterControl_Event.RaiseControlEvent(rootElement, "DocumentContentChanged", opt);
            }
            DCEventInterfaceLogFunction(rootElement, 'WriteDataFromDataSourceToDocument', startTime);
            return result;
        };

        /**
        * @name WriteDataFromDataSourceToDocumentSpecifyParameterNames
        * @type function
        * @classification datasource
        * @apinameZh 将指定名称的文档参数值填充到文档中
        * @param ["parameterNames","string","指定的文档参数名称","","各个名称之间用英文逗号分开。比如“姓名, 性别, 国籍”，如果为空则更新全部数据源。",true]
        * @returns ["result","number","绑定成功的个数，为0的情况就是都没有绑定成功"]
        */
        rootElement.WriteDataFromDataSourceToDocumentSpecifyParameterNames = function (parameterNames) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("WriteDataFromDataSourceToDocumentSpecifyParameterNames", parameterNames);
            DCEventInterfaceLogFunction(rootElement, 'WriteDataFromDataSourceToDocumentSpecifyParameterNames', startTime);
            return result;
        };

        /**
       * @name WriteDataFromDocumentToDataSource
       * @type function
       * @classification datasource
       * @apinameZh 将文档中的数据写入到数据源中
       * @returns ["result","number","更新的数据点个数"]
       */
        rootElement.WriteDataFromDocumentToDataSource = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("WriteDataFromDocumentToDataSource");
            DCEventInterfaceLogFunction(rootElement, 'WriteDataFromDocumentToDataSource', startTime);
            return result;
        };



        //以下为对接的添加


        /**
        * @name GetDocumentPageSettings
        * @type function
        * @apinameZh 获取文档的页面设置信息
        * @classification editformat
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.GetDocumentPageSettings = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentPageSettings");
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentPageSettings', startTime);
            return result;
        };

        /**
       * @name ChangeDocumentSettings
       * @type function
       * @apinameZh 设置页面设置
       * @classification editformat
       * @param ["settingsInfo","object","页面设置",0,"页面设置的属性对象",true]
       */
        rootElement.ChangeDocumentSettings = function (settingsInfo) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("ChangeDocumentSettings", settingsInfo);
            rootElement.Modified = true;
            DCEventInterfaceLogFunction(rootElement, 'ChangeDocumentSettings', startTime);
        };

        /**
       * @name DocumentSettingsDialog
       * @type function
       * @apinameZh 打开页面设置弹框
       * @classification editformat
       * @param ["options","object","页面设置参数","mull","",true]
       * @param ["callBack","function","回调函数","null","页面设置修改完成后的回调函数",true]
       */
        rootElement.DocumentSettingsDialog = function (options, callBack = null) {
            var startTime = new Date();
            WriterControl_Dialog.DocumentSettingsDialog(options, rootElement, callBack);
            DCEventInterfaceLogFunction(rootElement, 'DocumentSettingsDialog', startTime);
        };

        /**
       * @name SetDocumentGutter
       * @type function
       * @apinameZh 设置文档装订线
       * @classification editformat
       * @param ["gutterInfo","object","文档装订线参数","mull","",true]
       */
        rootElement.SetDocumentGutter = function (gutterInfo) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("SetDocumentGutter", gutterInfo);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentGutter', startTime);

        };

        /**
         * @name GetDocumentGutter
         * @type function
         * @apinameZh 获取文档装订线
         * @classification editformat
         * @returns ["result","object","文档装订线的参数"]
         */
        rootElement.GetDocumentGutter = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentGutter");
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentGutter', startTime);
            return result;
        };

        /**
        * @name DocumentGutterDialog
        * @type function
        * @apinameZh 打开文档装订线弹窗
        * @classification editformat
        * @param ["options","object","文档装订线参数","null","",true]
        */
        rootElement.DocumentGutterDialog = function (options) {
            var startTime = new Date();
            WriterControl_Dialog.DocumentGutterDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'DocumentGutterDialog', startTime);
        };

        /**
        * @name SetDocumentGridLine
        * @type function
        * @apinameZh 设置文档网格线
        * @classification editformat
        * @param ["gridLineInfo","object","文档网格线参数","","",true]
        * @describe 文档网格线的参数中有一个必要条件：<span style="color:#ff9632;">每页行数(GridNumInOnePage)必须大于2</span>
        */
        rootElement.SetDocumentGridLine = function (gridLineInfo) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("SetDocumentGridLine", gridLineInfo);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentGridLine', startTime);

        };

        /**
        * @name GetDocumentGridLine
        * @type function
        * @apinameZh 获取文档网格线
        * @classification editformat
        * @returns ["result","object","文档网格线"]
        */
        rootElement.GetDocumentGridLine = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentGridLine");
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentGridLine', startTime);
            return result;
        };

        /**
        * @name DocumentGridLineDialog
        * @type function
        * @apinameZh 打开文档网格线弹窗
        * @classification editformat
        * @param ["options","object","文档网格线参数","","",true]
        */
        rootElement.DocumentGridLineDialog = function (options) {
            var startTime = new Date();
            WriterControl_Dialog.DocumentGridLineDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'DocumentGridLineDialog', startTime);
        };

        /**
        * @name SetDocumentWatermark
        * @type function
        * @apinameZh 设置文档水印
        * @classification editformat
        * @param ["gridLineInfo","object","文档水印","","",true]
        */
        rootElement.SetDocumentWatermark = function (gridLineInfo) {
            var startTime = new Date();
            gridLineInfo = WriterControl_Dialog.checkWaterValue(gridLineInfo);
            console.log("rootElement.SetDocumentWatermark -> gridLineInfo", gridLineInfo);
            if (gridLineInfo == null) {
                return false;
            }
            //处理格式，保证后台不会报错
            rootElement.__DCWriterReference.invokeMethod("SetDocumentWatermark", gridLineInfo);
            WriterControl_Paint.UpdateViewForWaterMark(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentWatermark', startTime);

        };

        /**
        * @name GetDocumentWatermark
        * @type function
        * @apinameZh 获取文档水印
        * @classification editformat
        * @returns ["result","boolean","文档水印数据"]
        */
        rootElement.GetDocumentWatermark = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentWatermark");
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentWatermark', startTime);
            return result;
        };

        /**
        * @name SetDocumentTerminalText
        * @type function
        * @apinameZh 设置文档空白占位文本信息
        * @classification editformat
        * @param ["terminalTextInfo","object","文档空白占位文本信息","","",true]
        */
        rootElement.SetDocumentTerminalText = function (terminalTextInfo) {
            var startTime = new Date();
            if (terminalTextInfo == null) {
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("SetDocumentTerminalText", terminalTextInfo);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentTerminalText', startTime);
            return true;
        };

        /**
       * @name GetDocumentTerminalText
       * @type function
       * @apinameZh 获取文档空白占位文本信息
       * @classification editformat
       * @returns ["result","json","获取文档空白占位文本信息"]
       */
        rootElement.GetDocumentTerminalText = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentTerminalText");
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentTerminalText', startTime);
            return result;
        };

        /**
       * @name WatermarkDialog
       * @type function
       * @apinameZh 打开水印弹框
       * @classification editformat
       * @param ["options","object","水印参数","","",true]
       */
        rootElement.WatermarkDialog = function (options) {
            var startTime = new Date();
            WriterControl_Dialog.WatermarkDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'WatermarkDialog', startTime);
        };

        /**
        * @name CheckboxAndRadioDialog
        * @type function
        * @apinameZh 打开单复选框属性对话框
        * @classification structuralelement
        * @param ["options","object","单复选框参数","","",true]
        */
        rootElement.CheckboxAndRadioDialog = function (options) {
            var startTime = new Date();
            WriterControl_Dialog.CheckboxAndRadioDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'CheckboxAndRadioDialog', startTime);
        };

        /**
         * @name InputFieldDialog
         * @type function
         * @apinameZh 打开输入域属性属性对话框
         * @classification structuralelement
         * @param ["options","object","输入域属性","","",true]
         * @param ["isInsertMode","boolean","是否是插入输入域","false","",true]
         * @param ["callBack","function","输入域操作成功后的回调函数","null","",true]
         */
        rootElement.InputFieldDialog = function (options, isInsertMode = false, callBack = null) {
            var startTime = new Date();
            WriterControl_Dialog.InputFieldDialog(options, rootElement, isInsertMode, callBack);
            DCEventInterfaceLogFunction(rootElement, 'InputFieldDialog', startTime);
        };


        /**
         * @name ButtonDialog
         * @type function
         * @apinameZh 打开按钮属性属性对话框
         * @classification structuralelement
         * @param ["options","object","按钮属性","","",true]
         */
        rootElement.ButtonDialog = function (options) {
            var startTime = new Date();
            DCEventInterfaceLogFunction(rootElement, 'ButtonDialog', startTime);
            WriterControl_Dialog.ButtonDialog(options, rootElement);
        };

        /**
         * @name HorizontalLineDialog
         * @type function
         * @apinameZh 打开分割线属性对话框
         * @classification structuralelement
         * @param ["options","object","分割线属性","","",true]
         */
        rootElement.HorizontalLineDialog = function (options) {
            var startTime = new Date();
            WriterControl_Dialog.HorizontalLineDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'HorizontalLineDialog', startTime);
        };


        /**
        * @name PageNumberDialog
        * @type function
        * @apinameZh 打开页码属性对话框
        * @classification structuralelement
        * @param ["options","object","页码属性","","",true]
        */
        rootElement.PageNumberDialog = function (options) {
            var startTime = new Date();
            //var options = rootElement.CurrentElement('XTextButtonElement');
            WriterControl_Dialog.PageNumberDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'PageNumberDialog', startTime);

        };

        /**
       * @name LabelDialog
       * @type function
       * @apinameZh 打开文本标签属性对话框
       * @classification structuralelement
       * @param ["options","object","文本标签属性","","",true]
       */
        rootElement.LabelDialog = function (options) {
            var startTime = new Date();
            //var options = rootElement.CurrentElement('XTextButtonElement');
            WriterControl_Dialog.LabelDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'LabelDialog', startTime);

        };

        /**
       * @name QRCodeDialog
       * @type function
       * @apinameZh 打开二维码属性对话框
       * @classification structuralelement
       * @param ["options","object","二维码属性","","",true]
       */
        rootElement.QRCodeDialog = function (options) {
            var startTime = new Date();
            //var options = rootElement.CurrentElement('XTextButtonElement');
            WriterControl_Dialog.QRCodeDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'QRCodeDialog', startTime);

        };


        /**
         * @name BarCodeDialog
         * @type function
         * @apinameZh 打开条形码属性对话框
         * @classification structuralelement
         * @param ["options","object","条形码属性","","",true]
         */
        rootElement.BarCodeDialog = function (options) {
            var startTime = new Date();
            //var options = rootElement.CurrentElement('XTextButtonElement');
            WriterControl_Dialog.BarCodeDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'BarCodeDialog', startTime);

        };

        /**
         * @name FontSelectionDialog
         * @type function
         * @apinameZh 打开字体选择属性对话框
         * @classification structuralelement
         * @param ["options","object","字体选择属性","","",true]
         */
        rootElement.FontSelectionDialog = function (options) {
            var startTime = new Date();
            //var options = rootElement.CurrentElement('XTextButtonElement');
            WriterControl_Dialog.FontSelectionDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'FontSelectionDialog', startTime);

        };

        // （20240507 lxy 与售后确认过）以下医学表达式的接口在实际使用场景中都是使用ctl.DCExecuteCommand("insertmedicalexpression", true, options);
        // 所以暂时关闭以下弹框接口 
        /**
         * @name bordersShadingDialog
         * @type function
         * @classification table
         * @apinameZh 打开表格边框属性对话框
         * @classification structuralelement
         * @param ["options","object","表格边框属性","","",true]
         */
        rootElement.bordersShadingDialog = function (options) {
            var startTime = new Date();
            options = {
                ...options,
                title: "表格边框设置",
                type: "table"
            };
            //公用边框弹窗
            WriterControl_Dialog.borderShadingcellDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'bordersShadingDialog', startTime);
        };

        /**
         * @name borderShadingcellDialog
         * @type function
         * @apinameZh 打开单元格边框属性对话框
         * @classification structuralelement
         * @param ["options","object","单元格边框属性","","",true]
         */
        rootElement.borderShadingcellDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            options = {
                ...options,
                title: "单元格边框设置",
                type: "tableCell"
            };
            WriterControl_Dialog.borderShadingcellDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'borderShadingcellDialog', startTime);

        };

        /**
         * @name insertTableDialog
         * @type function
         * @classification table
         * @apinameZh 打开插入表格对话框
         * @classification structuralelement
         * @param ["options","object","插入表格","","",true]
         */
        rootElement.insertTableDialog = function (options, callBack) {
            var startTime = new Date();
            WriterControl_Dialog.insertTableDialog(options, rootElement, null, callBack);
            DCEventInterfaceLogFunction(rootElement, 'insertTableDialog', startTime);
        };

        /**
        * @name splitCellDialog
        * @type function
        * @apinameZh 打开拆分单元格对话框
        * @classification structuralelement
        * @param ["options","object","拆分单元格","","",true]
        */
        rootElement.splitCellDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.splitCellDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'splitCellDialog', startTime);
        };

        /**
         * @name EditDocumentCommentsDialog
         * @type function
         * @apinameZh 打开编辑文档批注对话框
         * @classification structuralelement
         * @param ["options","object","编辑文档批注","","",true]
         */
        rootElement.EditDocumentCommentsDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.EditDocumentCommentsDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'EditDocumentCommentsDialog', startTime);
        };

        /**
         * @name formModeDialog
         * @type function
         * @apinameZh 打开表单模式对话框
         * @classification structuralelement
         * @param ["options","object","表单模式","","",true]
         */
        rootElement.formModeDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.formModeDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'formModeDialog', startTime);
        };

        /**
         * @name contentProtectedModeDialog
         * @type function
         * @apinameZh 打开内容保护模式对话框
         * @classification structuralelement
         * @param ["options","object","内容保护模式","","",true]
         */
        rootElement.contentProtectedModeDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.contentProtectedModeDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'contentProtectedModeDialog', startTime);
        };

        /**
         * @name paragraphDialog
         * @type function
         * @apinameZh 打开段落对话框
         * @classification structuralelement
         * @param ["options","object","段落","","",true]
         */
        rootElement.paragraphDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.paragraphDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'paragraphDialog', startTime);
        };

        /**
        * @name tableDialog
        * @type function
        * @classification table
        * @apinameZh 打开表格属性对话框
        * @classification structuralelement
        * @param ["options","object","表格属性","","",true]
        */
        rootElement.tableDialog = function (options) {
            var startTime = new Date();
            WriterControl_Dialog.tableDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'tableDialog', startTime);
        };

        /**
       * @name tableCellDialog
       * @type function
       * @apinameZh 打开单元格属性对话框
       * @classification structuralelement
       * @param ["options","object","单元格属性","","",true]
       */
        rootElement.tableCellDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.tableCellDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'tableCellDialog', startTime);
        };

        /**
         * @name tableRowDialog
         * @type function
         * @apinameZh 打开单元行属性对话框
         * @classification structuralelement
         * @param ["options","object","单元行属性","","",true]
         */
        rootElement.tableRowDialog = function (options) {
            var startTime = new Date();
            WriterControl_Dialog.tableRowDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'tableRowDialog', startTime);
        };

        /**
         * @name cellGridlineDialog
         * @type function
         * @apinameZh 打开单元格网格线属性对话框
         * @classification structuralelement
         * @param ["options","object","单元格网格线属性","","",true]
         */
        rootElement.cellGridlineDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.cellGridlineDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'cellGridlineDialog', startTime);
        };

        /**
        * @name cellDiagonalLineDialog
        * @type function
        * @apinameZh 打开单元格斜分线属性对话框
        * @classification structuralelement
        * @param ["options","object","单元格斜分线属性","","",true]
        */
        rootElement.cellDiagonalLineDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.cellDiagonalLineDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'cellDiagonalLineDialog', startTime);
        };

        /**
        * @name imgEditDialog
        * @type function
        * @apinameZh 打开编辑图片对话框
        * @classification structuralelement
        * @param ["options","object","编辑图片","","",true]
        */
        rootElement.imgEditDialog = function (options) {
            var startTime = new Date();
            WriterControl_Dialog.imgEditDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'imgEditDialog', startTime);
        };



        /**
       * @name loginDialog
       * @type function
       * @apinameZh 打开用户登录对话框
       * @classification structuralelement
       * @param ["options","object","用户登录","","",true]
       */
        rootElement.loginDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.loginDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'loginDialog', startTime);
        };

        /**
         * @name SetSubDocumentUp
         * @type function
         * @apinameZh 病程上移
         * @classification subdoc
         * @param ["subID","string","病程id","","也可以为空，为空是将当前病程上移",true]
         */
        rootElement.SetSubDocumentUp = function (subID) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                let result = false;
                if (DCTools20221228.IsDotnetReferenceElement(subID) === true) {
                    result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentUp2", subID);
                } else {
                    result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentUp", subID);
                }
                //let result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentUp", subID);
                DCEventInterfaceLogFunction(rootElement, 'SetSubDocumentUp', startTime);
                return result;
            }
            return false;
        };


        /**
         * @name SetSubDocumentDown
         * @type function
         * @apinameZh 病程下移
         * @classification subdoc
         * @param ["subID","string","病程id","","也可以为空，为空是将当前病程上移",true]
         */
        rootElement.SetSubDocumentDown = function (subID) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                let result = false;
                if (DCTools20221228.IsDotnetReferenceElement(subID) === true) {
                    result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentDown2", subID);
                } else {
                    result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentDown", subID);
                }
                //let result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentDown", subID);
                DCEventInterfaceLogFunction(rootElement, 'SetSubDocumentDown', startTime);
                return result;
            }
            return false;
        };

        /**
         * @name ImageDialog
         * @type function
         * @apinameZh 图片属性对话框
         * @classification structuralelement
         * @param ["option","object","图片属性","","",true]
         */
        rootElement.ImageDialog = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            WriterControl_Dialog.ImageDialog(options, rootElement);
            DCEventInterfaceLogFunction(rootElement, 'ImageDialog', startTime);

        };

        /**
        * @name InsertSubDocuments
        * @type function
        * @apinameZh 在当前位置处插入病程
        * @classification subdoc
        * @param ["subDocumentOption","object","插入的病程对象属性","","","true"]
        * @param ["afterElement","ELEMENT","在指定病程后面插入病程，病程的id、NativeHandle、后台引用对象","null","","false"]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-06-08","添加四代兼容接口","wyc" ]
        * @describe 如：{ "Files": 批量生成子文档的文档数组 ,"Options": 批量生成子文档的文档选项数组,"Usebase64": 是否使用base64字符串加载, "FileFormat": 子文档加载字符串的文档格式，默认为"xml"}
        */
        rootElement.InsertSubDocuments = function (subDocumentOption, afterElement = null, isAfter = true) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                return false;
            }
            var result = false;
            var needRefresh = false;
            if (DCTools20221228.IsDotnetReferenceElement(afterElement) === true) {
                needRefresh = true;
                result = rootElement.__DCWriterReference.invokeMethod("InsertSubDocuments", subDocumentOption, afterElement, isAfter);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("InsertSubDocuments2", subDocumentOption, afterElement, isAfter);
            }

            if (result == true && rootElement) {
                if (needRefresh === true) {
                    var i = setTimeout(function () {
                        clearTimeout(i);
                        rootElement.RefreshDocument();
                    }, 300);
                }
                //var arr = [];
                //var Parameters = subDocumentOption.Parameters
                //if (Parameters && Parameters.length > 0) {
                //    for (var i = 0; i < Parameters.length; i++) {
                //        arr.push(Parameters[i].ID);
                //    }
                //}
                //判断是否为移动端
                var pageContainer = rootElement.querySelector('[dctype="page-container"]');
                if (DCTools20221228.IsReadonlyAutoFocus(rootElement)) {
                    //编辑器内部页面存在滚动
                    if (pageContainer && pageContainer.scrollHeight > pageContainer.clientHeight) {
                        //pageContainer.scrollTo(0, 0);
                        //获取病程的属性 TopInOwnerPage  OwnerPageIndex
                        var thisId = null;
                        if (subDocumentOption && Array.isArray(subDocumentOption.Options)) {
                            var lastOptins = subDocumentOption.Options.pop();
                            if (lastOptins && lastOptins.ID) {
                                thisId = lastOptins.ID;
                            }
                        }
                        if (thisId) {
                            WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                                rootElement.SelectSubDocumentByID(thisId);
                            });
                        }
                    }
                }

                if (!!rootElement.EventAfterInsertSubDocuments && typeof (rootElement.EventAfterInsertSubDocuments) == "function") {
                    rootElement.EventAfterInsertSubDocuments.call(rootElement);
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'InsertSubDocuments', startTime);
            return result;
        };
        /**
        * @name AppendSubDocuments
        * @type function
        * @classification subdoc
        * @apinameZh 文档末尾批量追加病程
        * @param ["subDocumentOption","object","插入的病程对象属性","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-06-08","添加四代兼容接口","wyc" ]
        * @describe 如：{ "Files": 批量生成子文档的文档数组 ,"Options": 批量生成子文档的文档选项数组,"Usebase64": 是否使用base64字符串加载, "FileFormat": 子文档加载字符串的文档格式，默认为"xml"}
        */
        rootElement.AppendSubDocuments = function (subDocumentOption) {
            var startTime = new Date();
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            var RectInfo = rootElement.RectInfo;//区域选择
            //当前存在其中一个模式，即不可修改dom，返回false
            if (IsPrintPreview || ExtViewMode || RectInfo) {
                return false;
            }

            var result = rootElement.__DCWriterReference.invokeMethod("AppendSubDocuments", subDocumentOption);
            if (result == true && rootElement) {
                //判断是否为移动端
                var pageContainer = rootElement.querySelector('[dctype="page-container"]');
                if (DCTools20221228.IsReadonlyAutoFocus(rootElement)) {
                    //编辑器内部页面存在滚动
                    if (pageContainer && pageContainer.scrollHeight > pageContainer.clientHeight) {
                        //pageContainer.scrollTo(0, 0);
                        //获取病程的属性 TopInOwnerPage  OwnerPageIndex
                        var thisId = null;
                        if (subDocumentOption && Array.isArray(subDocumentOption.Options)) {
                            var lastOptins = subDocumentOption.Options.pop();
                            if (lastOptins && lastOptins.ID) {
                                thisId = lastOptins.ID;
                            }
                        }
                        if (thisId) {
                            WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                                rootElement.SelectSubDocumentByID(thisId);
                            });
                        }
                    }
                }

                if (!!rootElement.EventAfterInsertSubDocuments && typeof (rootElement.EventAfterInsertSubDocuments) == "function") {
                    rootElement.EventAfterInsertSubDocuments.call(rootElement, result);
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'AppendSubDocuments', startTime);
            return result;
        };

        /**
        * @name CurrentSubDoc
        * @type function
        * @classification subdoc
        * @apinameZh 当前病程
        * @returns ["result","string","病程元素的ID"]
        */
        rootElement.CurrentSubDoc = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("CurrentSubDoc");
            DCEventInterfaceLogFunction(rootElement, 'CurrentSubDoc', startTime);
            return result;

        };

        /**
        * @name DeleteSubDoc
        * @type function
        * @classification subdoc
        * @apinameZh 删除指定编号的病程
        * @param ["id","string","病程编号","","",true]
        * @returns ["result","boolean","结果"]
        */
        rootElement.DeleteSubDoc = function (id) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读模式
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("DeleteSubDoc", id);
            DCEventInterfaceLogFunction(rootElement, 'DeleteSubDoc', startTime);
            return result;
        };

        /**
        * @name DeleteCurrentSubDoc
        * @type function
        * @classification subdoc
        * @apinameZh 删除当前病程
        * @returns ["result","boolean","结果"]
        */
        rootElement.DeleteCurrentSubDoc = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读模式
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("DeleteCurrentSubDoc");
            DCEventInterfaceLogFunction(rootElement, 'DeleteCurrentSubDoc', startTime);
            return result;
        };

        /**
        * @name SetCurrentSubDocumentReadOnly
        * @type function
        * @apinameZh 设置当前病程是否只读
        * @classification subdoc
        * @param ["isReadOnly","boolean","是否只读","","",true]
        * @param ["backgroundColorValue","string","背景色","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetCurrentSubDocumentReadOnly = function (isReadOnly, backgroundColorValue) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetCurrentSubDocumentReadOnly", isReadOnly, backgroundColorValue);
            DCEventInterfaceLogFunction(rootElement, 'SetCurrentSubDocumentReadOnly', startTime);
            return result;
        };

        /**
        * @name SetSubDocumentReadOnly
        * @type function
        * @apinameZh 设置指定病程是否只读
        * @classification subdoc
        * @param ["subID","string","编号","","",true]
        * @param ["isReadOnly","boolean","是否只读","","",true]
        * @param ["backgroundColorValue","string","背景色","","",true]
        * @returns ["result","boolean","结果"]
        */
        rootElement.SetSubDocumentReadOnly = function (subID, isReadOnly, backgroundColorValue) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentReadOnly", subID, isReadOnly, backgroundColorValue);
            DCEventInterfaceLogFunction(rootElement, 'SetSubDocumentReadOnly', startTime);
            return result;
        };

        /**
        * @name LoadSubDocumentFromString
        * @type function
        * @apinameZh 给已经存在的病程填充文档
        * @classification subdoc
        * @param ["options","object","参数","","",true]
        * @returns ["result","boolean","结果"]
        */
        rootElement.LoadSubDocumentFromString = function (options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                let result = rootElement.__DCWriterReference.invokeMethod("LoadSubDocumentFromString", options);
                DCEventInterfaceLogFunction(rootElement, 'LoadSubDocumentFromString', startTime);
                return result;
            }
        };

        /**
        * @name SaveSubDocumentToBase64String
        * @type function
        * @apinameZh 获取病程文档BASE64字符串
        * @classification subdoc
        * @param ["fileFormat","string","格式","","",true]
        * @param ["id","string","病程编号","","",true]
        * @returns ["result","string","病程内容"]
        */
        rootElement.SaveSubDocumentToBase64String = function (fileFormat, id) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SaveSubDocumentToBase64String", fileFormat, id);
            DCEventInterfaceLogFunction(rootElement, 'SaveSubDocumentToBase64String', startTime);
            return result;
        };


        /**
        * @name SaveSubDocumentToString
        * @type function
        * @apinameZh 获取病程文档BASE64字符串
        * @classification subdoc
        * @param ["options","object","参数","","",true]
        * @param ["callBack","function","保存PDF时必须指定的回调函数","","",true]
        * @change ["2024-06-13","针对保存出PDF内容做特殊处理，要求必须提供回调函数","wyc" ]
        * @returns ["result","string","病程内容"]
        */
        rootElement.SaveSubDocumentToString = function (options, callBack) {
            var startTime = new Date();
            var needProcessPDF = false;
            if (options.FileFormat === "pdf") {
                options.FileFormat = "xml";
                options.UseBase64 = false;
                needProcessPDF = true;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SaveSubDocumentToString", options);
            if (needProcessPDF === false || result == null) {
                if (typeof (callBack) === "function") {
                    callBack.call(rootElement, result);
                }
                DCEventInterfaceLogFunction(rootElement, 'SaveSubDocumentToString', startTime);
                return result;
            }
            if (typeof (result) === "object") {
                var sum = 0;
                var base64s = [];
                var okeys = Object.keys(result);
                var totalsum = okeys.length;
                if (totalsum == 0) {
                    if (typeof (callBack) === "function") {
                        callBack.call(rootElement, null);
                    }
                    return null;
                }
                var callBack2 = function (base64str) {
                    base64s.push(base64str);
                    sum = sum + 1;
                    console.log(sum);
                    if (sum == totalsum && typeof (callBack) === "function") {
                        callBack.call(rootElement, base64s);
                    }
                };
                for (var i in result) {
                    var options = {
                        files: [result[i]],
                        resulttype: "Base64String",
                        localmode: true
                    };
                    rootElement.GetPDFByFiles(options, callBack2);
                }

            } else {
                var options = {
                    files: [result],
                    resulttype: "Base64String",
                    localmode: options.LocalMode
                };
                result = rootElement.GetPDFByFiles(options, callBack);
            }
            DCEventInterfaceLogFunction(rootElement, 'SaveSubDocumentToString', startTime);
            return result;
        };

        /**
        * @name GetAllSubDocumentIDs
        * @type function
        * @apinameZh 获取文档中的全部病程ID
        * @classification subdoc
        * @returns ["result","Array","病程ID"]
        */
        rootElement.GetAllSubDocumentIDs = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetAllSubDocumentIDs");
            DCEventInterfaceLogFunction(rootElement, 'GetAllSubDocumentIDs', startTime);
            return result;
        };


        /**
         * @name SelectSubDocument
         * @type function
         * @classification subdoc
         * @apinameZh 根据文档中的序号定位病程
         * @param ["index","number","病程序号","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SelectSubDocument = function (index) {
            var startTime = new Date();
            var allSub = rootElement.GetCourseRecords();
            let result = false;
            if (index <= 1) {
                index = 1;
            } else if (index >= allSub.length) {
                index = allSub.length;
            }
            result = rootElement.__DCWriterReference.invokeMethod("SelectSubDocument", index);
            if (result) {
                //zhangbin 20231122 DUWRITER5_0-1256问题需要定位是保证在开始位置
                //判断当前输入域的位置如果在滚动条的最后
                var pageContainer = rootElement.querySelector('[dctype="page-container"]');
                //编辑器内部页面存在滚动
                if (pageContainer && pageContainer.scrollHeight > pageContainer.clientHeight) {
                    // 根据序号获取id
                    var id = allSub[index - 1]['ID'];
                    ///获取病程的属性 TopInOwnerPage  OwnerPageIndex
                    var subAttr = rootElement.GetElementProperties(id);
                    var allCanvas = pageContainer.querySelectorAll('canvas[dctype="page"]');
                    if (subAttr && typeof subAttr.OwnerPageIndex == "number") {
                        var thisPage = allCanvas[subAttr.OwnerPageIndex];
                        if (thisPage) {
                            var topInOwnerPage = parseFloat((subAttr.TopInOwnerPage / 300 * 96.00001209449).toFixed(2));
                            var leftInOwnerPage = parseFloat((subAttr.LeftInOwnerPage / 300 * 96.00001209449).toFixed(2));
                            pageContainer.scrollTo(thisPage.offsetLeft + leftInOwnerPage, thisPage.offsetTop + topInOwnerPage);
                        }
                    }

                }
            }
            DCEventInterfaceLogFunction(rootElement, 'SelectSubDocument', startTime);
            return result;
        };

        /**
        * @name SetSubDocumentState
        * @type function
        * @classification subdoc
        * @apinameZh 设置当前病程只读和颜色
        * @param ["editable","boolean","是否只读","","",true]
        * @param ["strStyle","string","颜色","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetSubDocumentState = function (editable, strStyle) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentState", editable, strStyle);
            DCEventInterfaceLogFunction(rootElement, 'SetSubDocumentState', startTime);
            return result;
        };

        /**
         * @name SubDocCrossPage
         * @type function
         * @classification subdoc
         * @apinameZh 设置病程是否跨页
         * @param ["parsubdoc","string","病程id","","",true]
         * @param ["isCrossams","Boolean","是否可以跨页","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SubDocCrossPage = function (parsubdoc, isCrossams) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SubDocCrossPage", parsubdoc, isCrossams);
            DCEventInterfaceLogFunction(rootElement, 'SubDocCrossPage', startTime);
            return result;
        };

        /**
        * @name SetExcludeKeywords
        * @type function
        * @classification file
        * @apinameZh 设置文档的违禁关键词
        * @param ["keys","string","违禁关键词","","",true]
        */
        rootElement.SetExcludeKeywords = function (keys) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                rootElement.__DCWriterReference.invokeMethod("SetExcludeKeywords", keys);
                rootElement.DocumentOptions.BehaviorOptions.ExcludeKeywords = keys;
                rootElement.ApplyDocumentOptions();
                DCEventInterfaceLogFunction(rootElement, 'SetExcludeKeywords', startTime);
            }
        };

        /**
         * @name GetExcludeKeywords
         * @type function
         * @classification file
         * @apinameZh 获取文档的违禁关键词
         * @returns ["result","string","获取到的文档违禁关键词"]
         */
        rootElement.GetExcludeKeywords = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetExcludeKeywords");
            DCEventInterfaceLogFunction(rootElement, 'GetExcludeKeywords', startTime);
            return result;
        };

        /**
         * @name setMouseDragScrollMode
         * @type function
         * @apinameZh 按下鼠标拖拽滚动
         * @classification vestige
         * @param ["bool","Boolean","是否开启鼠标拖拽选择","","",true]
         */
        rootElement.setMouseDragScrollMode = function (bool) {
            //添加全局属性
            if (typeof bool != 'boolean') {
                bool = true;
            }
            if (bool) {
                //清掉光标效果
                rootElement.MoveToSelectionSrart();
                //隐藏光标
                var txtArea = rootElement.querySelector('#txtEdit20221213');
                if (txtArea) {
                    txtArea.blur();
                }
            }
            rootElement.MouseDragScrollMode = bool;
        };


        /**
         * @name SelectionHtml
         * @type function
         * @apinameZh 获取选择的html
         * @classification file
         * @param ["nativeHtml","Boolean","是否包含页眉页脚","","",true]
         * @returns ["result","string","选择的html"]
         */
        rootElement.SelectionHtml = function (nativeHtml, callback) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SelectionHtml", nativeHtml);
            WriterControl_Paint.ApplyBitmapContentHtmlSrc(result, function (result) {
                if (typeof (callback) == "function") {
                    callback.call(rootElement, result);
                }
            });
            DCEventInterfaceLogFunction(rootElement, 'SelectionHtml', startTime);
            return result;
        };


        /**
         * @name SelectionHtml2
         * @type function
         * @apinameZh 获取选择的html
         * @classification file
         * @param ["containHeaderFooter","Boolean","是否包含页眉页脚","","",true]
         * @change ["2024-09-24","重写前端代码生成SVG的选中内容","wyc" ]
         * @change ["2024-10-09","精简前后端代码结构","wyc" ]
         * @returns ["result","string","选择的html"]
         */
        rootElement.SelectionHtml2 = function (containHeaderFooter) {
            var startTime = new Date();

            var hasselection = rootElement.__DCWriterReference.invokeMethod(
                "HasSelection");
            if (hasselection !== true) {
                return null;
            }

            WriterControl_Rule.SuppressPaintRule = true;
            let CreatePrintOptions =
            {
                PrintRange: "Selection",//打印被选中的内容
                PrintHeaderFooter: containHeaderFooter
            };
            var result = rootElement.__DCWriterReference.invokeMethod("SelectionHtml2", CreatePrintOptions);
            if (typeof (result) !== "object" || result == null) {
                return null;
            }

            var pageContainer = rootElement.ownerDocument.createElement("DIV");

            for (var i = 0; i < result.SVGS.length; i++) {
                var div = rootElement.ownerDocument.createElement("DIV");
                div.style.pageBreakAfter = "always";
                if (i !== result.SVGS.length - 1) {
                    div.style.pageBreakInside = "avoid";
                }
                var element = rootElement.ownerDocument.createElementNS("http://www.w3.org/2000/svg", "svg");
                element.style.width = result.Width + "px";
                element.style.height = result.Height + "px";
                element.style.overflow = "hidden";
                element.setAttribute("width", result.Width + "px");
                element.setAttribute("height", result.Height + "px");
                element.innerHTML = result.SVGS[i].toString();
                div.appendChild(element);
                pageContainer.appendChild(div);
            }
            //console.log(result);
            WriterControl_Rule.SuppressPaintRule = false;
            var str = "<html><body>" + pageContainer.outerHTML + "</body></html>";
            DCEventInterfaceLogFunction(rootElement, 'SelectionHtml2', startTime);
            return str;
        };

        /**
        * @name SelectionText
        * @type function
        * @apinameZh 获取选择的文本
        * @classification file
        * @param ["clearBorder","Boolean","是否包含页眉页脚","","",true]
        * @returns ["result","string","选择的文本"]
        */
        rootElement.SelectionText = function (clearBorder) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SelectionText", clearBorder);
            DCEventInterfaceLogFunction(rootElement, 'SelectionText', startTime);
            return result;
        };

        /**
        * @name SelectionXml
        * @type function
        * @apinameZh 获取选择的XML
        * @classification file
        * @param ["containHeaderFooter","Boolean","是否包含页眉页脚","","",true]
        * @returns ["result","string","选择的XML"]
        */
        rootElement.SelectionXml = function (containHeaderFooter) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SelectionXml", containHeaderFooter);
            DCEventInterfaceLogFunction(rootElement, 'SelectionXml', startTime);
            return result;
        };

        /**
       * @name SelectionJson
       * @type function
       * @apinameZh 获取选择的json
       * @classification file
       * @param ["containHeaderFooter","Boolean","是否包含页眉页脚","","",true]
       * @returns ["result","string","选择的json"]
       */
        rootElement.SelectionJson = function (containHeaderFooter) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SelectionJson", containHeaderFooter);
            DCEventInterfaceLogFunction(rootElement, 'SelectionJson', startTime);
            return result;
        };

        /**
         * @name InsertJson
         * @type function
         * @apinameZh 在当前位置插入json内容
         * @classification file
         * @param ["content","object","json内容","","",true]
         * @returns ["result","number","返回字符长度"]
         */
        rootElement.InsertJson = function (content) {
            var startTime = new Date();
            let result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("InsertJson", content);
                DCEventInterfaceLogFunction(rootElement, 'InsertJson', startTime);
            }
            return result;
        };

        /**
        * @name InsertContentByInputID
        * @type function
        * @apinameZh 在指定的输入域内插入指定格式文档
        * @classification structuralelement
        * @param ["content","string","内容","","",true]
        * @param ["format","string","格式","","",true]
        * @param ["elementID","string","元素id","","",true]
        * @param ["clearold","Boolean","是否清空原有的内容","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.InsertContentByInputID = function (content, format, elementID, clearold, withTrackInfos = false) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("InsertContentByInputID", content, format, elementID, clearold, withTrackInfos);
            DCEventInterfaceLogFunction(rootElement, 'InsertContentByInputID', startTime);
            return result;
        };

        /**
       * @name InsertContentByTableCellD
       * @type function
       * @classification table
       * @apinameZh 在指定的单元格内插入指定格式文档
       * @param ["content","string","内容","","",true]
       * @param ["format","string","格式","","",true]
       * @param ["tableID","string","表格id","","",true]
       * @param ["rowIndex","number","行下标","","",true]
       * @param ["colIndex","number","列下标","","",true]
       * @param ["clearold","Boolean","是否清空原有的内容","","",true]
       * @returns ["result","boolean","操作是否成功"]
       */
        rootElement.InsertContentByTableCellD = function (content, format, tableID, rowIndex, colIndex, clearold) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("InsertContentByTableCellD", content, format, tableID, rowIndex, colIndex, clearold);
            DCEventInterfaceLogFunction(rootElement, 'InsertContentByTableCellD', startTime);
            return result;
        };


        /**
      * @name DCExecuteCommandDialog
      * @type function
      * @classification file
      * @apinameZh 执行编辑器命令对话框
      * @change ["2023-05-09","和四代接口保持兼容","wyc" ]
      * @returns ["result","boolean","操作是否成功"]
      */
        rootElement.DCExecuteCommandDialog = function () {
            var startTime = new Date();
            let result = WriterControl_Dialog.DCExecuteCommandDialog(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'DCExecuteCommandDialog', startTime);
            return result;
        };

        /**
         * @name AllLineHeight
         * @type function
         * @classification editformat
         * @apinameZh 设置全文行间距
         * @param ["value","number","行间距","","",true]
         */
        rootElement.AllLineHeight = function (value) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'alllineheight', false, value);
            DCEventInterfaceLogFunction(rootElement, 'AllLineHeight', startTime);
        };

        /**
         * @name ComplexViewMode
         * @type function
         * @classification vestige
         * @apinameZh 留痕模式
         */
        rootElement.ComplexViewMode = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'ComplexViewMode', false, null);
            DCEventInterfaceLogFunction(rootElement, 'ComplexViewMode', startTime);
        };

        /**
         * @name ClearAllUserTrace
         * @type function
         * @classification vestige
         * @apinameZh 清除所有痕迹
         * @describe 清除痕迹时会将被标记为逻辑删除的内容给去掉逻辑删除的标记，可以显示出来。请在管理员模式下使用。
         */
        rootElement.ClearAllUserTrace = function () {
            var startTime = new Date();
            var readonly = rootElement.Readonly;//只读
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            var RectInfo = rootElement.RectInfo;//区域选择
            if (readonly || ExtViewMode || RectInfo) {
                //只读、续打、区域选择时禁用此接口
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'ClearAllUserTrace', false, null);
            DCEventInterfaceLogFunction(rootElement, 'ClearAllUserTrace', startTime);
        };

        /**
         * @name ClearUserTrace
         * @type function
         * @apinameZh 清除当前用户痕迹
         * @classification vestige
         */
        rootElement.ClearUserTrace = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'ClearUserTrace', false, null);
                DCEventInterfaceLogFunction(rootElement, 'ClearUserTrace', startTime);
            }
        };

        /**
         * @name CommitUserTrace
         * @type function
         * @apinameZh 提交用户痕迹
         * @classification vestige
         * @describe 删除所有被逻辑删除的元素。请在管理员模式下使用。
         */
        rootElement.CommitUserTrace = function () {
            var startTime = new Date();
            var readonly = rootElement.Readonly;//只读
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            var RectInfo = rootElement.RectInfo;//区域选择
            if (readonly || ExtViewMode || RectInfo) {
                //只读、续打、区域选择时禁用此接口
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'CommitUserTrace', false, null);
            DCEventInterfaceLogFunction(rootElement, 'CommitUserTrace', startTime);
        };

        /**
        * @name CleanViewMode
        * @type function
        * @classification file
        * @apinameZh 清洁模式
        */
        rootElement.CleanViewMode = function () {
            var startTime = new Date();
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            var RectInfo = rootElement.RectInfo;//区域选择
            if (ExtViewMode || RectInfo) {
                //续打、区域选择时禁用此接口
                return false;
            }
            rootElement.ExecuteCommand("CleanViewMode", false, null);
            //rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'CleanViewMode', false, null);
            DCEventInterfaceLogFunction(rootElement, 'CleanViewMode', startTime);
        };

        /**
        * @name TableDeleteTable
        * @type function
        * @apinameZh 删除表格
        * @classification table
        */
        rootElement.TableDeleteTable = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Table_DeleteTable', false, null);
            DCEventInterfaceLogFunction(rootElement, 'TableDeleteTable', startTime);
        };

        /**
        * @name TableDeleteRow
        * @type function
        * @apinameZh 删除行
        * @classification table
        */
        rootElement.TableDeleteRow = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Table_DeleteRow', false, null);
            DCEventInterfaceLogFunction(rootElement, 'TableDeleteRow', startTime);
        };

        /**
        * @name TableDeleteColumn
        * @type function
        * @apinameZh 删除列
        * @classification table
        */
        rootElement.TableDeleteColumn = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Table_DeleteColumn', false, null);
            DCEventInterfaceLogFunction(rootElement, 'TableDeleteColumn', startTime);
        };

        /**
        * @name TableInsertRowUp
        * @type function
        * @apinameZh 在上面插入行
        * @classification table
        */
        rootElement.TableInsertRowUp = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Table_InsertRowUp', false, null);
            DCEventInterfaceLogFunction(rootElement, 'TableInsertRowUp', startTime);
        };

        /**
         * @name TableInsertRowDown
         * @type function
         * @apinameZh 在下面插入行
         * @classification table
         */
        rootElement.TableInsertRowDown = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Table_InsertRowDown', false, null);
            DCEventInterfaceLogFunction(rootElement, 'TableInsertRowDown', startTime);
        };

        /**
        * @name TableInsertColumnLeft
        * @type function
        * @apinameZh 在左面插入列
        * @classification table
        */
        rootElement.TableInsertColumnLeft = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Table_InsertColumnLeft', false, null);
            DCEventInterfaceLogFunction(rootElement, 'TableInsertColumnLeft', startTime);
        };

        /**
         * @name TableInsertColumnRight
         * @type function
         * @apinameZh 在右面插入列
         * @classification table
         */
        rootElement.TableInsertColumnRight = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Table_InsertColumnRight', false, null);
            DCEventInterfaceLogFunction(rootElement, 'TableInsertColumnRight', startTime);
        };

        /**
         * @name TableMergeCell
         * @type function
         * @apinameZh 合并单元格
         * @classification table
         */
        rootElement.TableMergeCell = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Table_MergeCell', false, null);
            rootElement.Modified = true;
            DCEventInterfaceLogFunction(rootElement, 'TableMergeCell', startTime);
        };

        /**
         * @name ShowBackgroundCellID
         * @type function
         * @apinameZh 背景编号
         * @classification table
         */
        rootElement.ShowBackgroundCellID = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'ShowBackgroundCellID', false, null);
            DCEventInterfaceLogFunction(rootElement, 'ShowBackgroundCellID', startTime);
        };
        /**
         * @name TableCellAlign
         * @type function
         * @apinameZh 表格对齐方式
         * @classification table
         * @param ["Parameter","string","对齐方式","","本参数需要注意。。。",true]
         * @describe 枚举：AlignTopLeft：顶端左对齐, AlignTopRight：顶端右对齐, AlignTopCenter：顶端中间对齐, AlignMiddleLeft：垂直居中水平左对齐, AlignMiddleRight：垂直居中水平右对齐, AlignMiddleCenter：垂直居中水平中间对齐, AlignBottomLeft：底端左对齐, AlignBottomRight：底端右对齐, AlignBottomCenter：底端中间对齐
         */
        rootElement.TableCellAlign = function (Parameter) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", Parameter, false, null);
            DCEventInterfaceLogFunction(rootElement, 'DCExecuteCommandDialog', startTime);
        };

        /**
         * @name DCExecuteCommand
         * @type function
         * @apinameZh 执行编辑器命令 
         * @classification file
         * @param ["strCommandName","string","命令名称","","",true]
         * @param ["bolShowUI","boolean","是否显示用户界面","","",true]
         * @param ["Parameter","object","参数","","",true]
         * @param ["callback","function","回调函数","","",true]
         * @returns ["result","boolean","执行结果"]
         * @change ["2023-05-09","和四代接口保持兼容","wyc" ]
         */
        rootElement.DCExecuteCommand = function (strCommandName, bolShowUI, Parameter, callback = null) {
            var startTime = new Date();
            // 兼容四代接口EventBeforeExecuteCommand，获取命令的名称，可以阻止命令的执行 lxy20230714
            if (!!rootElement.EventBeforeExecuteCommand && typeof (rootElement.EventBeforeExecuteCommand) == "function") {
                let isCancel = rootElement.EventBeforeExecuteCommand.call(rootElement, strCommandName);
                if (isCancel) {
                    return;
                }
            }
            //如果是插入元素，判断是否需要后端自动更正ID。
            if (Parameter && Parameter !== null && typeof Parameter === 'object') {
                var AutoMaticCorrectionRepeatID = rootElement.getAttribute('AutoMaticCorrectionRepeatID') || '';
                Parameter['AutoMaticCorrectionRepeatID'] = (AutoMaticCorrectionRepeatID && AutoMaticCorrectionRepeatID === 'true') ? true : false;
            }
            strCommandName += "";

            //wyc20240108:追加若干命令需要后续刷新前端文档选项的操作
            var cmdNamesForRefreshOptions = ["cleanviewmode", "complexviewmode"];
            strCommandName = strCommandName.toLocaleLowerCase();

            var result = false;
            switch (strCommandName.toLocaleLowerCase()) {
                case "spechars":
                    var pattern = /<(.*)>.*<\/\1>|<(.*) \/>|<(.*)\/>/;
                    if (pattern.test(Parameter)) {
                        alert(window.__DCSR.SpecharsHasHtml);
                    }
                    return rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", 'Spechars', bolShowUI, Parameter);
                    break;
                case "allfontname"://兼容四代的设置全文的字体
                    return rootElement.__DCWriterReference.invokeMethod("AllFontName", Parameter);
                    break;
                case "allfontsize"://兼容四代的设置全文的字体大小
                    if (Parameter === null || Parameter === undefined || Parameter === "") {
                        return;
                    }
                    var allfontsize = Parameter ? parseFloat(Parameter) : Parameter;
                    return rootElement.__DCWriterReference.invokeMethod("AllFontSize", allfontsize);
                    break;
                case "date"://后端取的时间有问题，需要在前端取
                    var date = new Date();
                    var year = date.getFullYear();//年
                    var month = showTime(date.getMonth() + 1);//月
                    var day = showTime(date.getDate());//日
                    var str = year + "-" + month + "-" + day;
                    return rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", "InsertString", bolShowUI, str);
                    break;
                case "time"://后端取的时间有问题，需要在前端取
                    var date = new Date();
                    var hours = showTime(date.getHours());//小时
                    var minutes = showTime(date.getMinutes());//分钟
                    var second = showTime(date.getSeconds());//秒
                    var str = hours + ":" + minutes + ":" + second;
                    return rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", "InsertString", bolShowUI, str);
                    break;
                case "fileopen":
                    var file = rootElement.ownerDocument.createElement('input');
                    file.setAttribute('id', 'dcInputFile');
                    file.setAttribute('type', 'file');
                    file.setAttribute('accept', '.xml,.json,.rtf,.html,.htm,.odt,.ofd');
                    file.style.cssText = 'position: relative;left: -2000px; ';
                    rootElement.appendChild(file);
                    file.click();
                    //file文件选中事件
                    file.onchange = function () {
                        var fileList = this.files;
                        if (fileList.length > 0) {
                            // console.log(fileList[0]);
                            var reader = new FileReader();
                            //wyc20230613改变写法
                            var isodt = fileList[0].name.toLowerCase().endsWith("odt") === true;
                            var isofd = fileList[0].name.toLowerCase().endsWith("ofd") === true;
                            if (isodt || isofd) {
                                reader.readAsDataURL(fileList[0]);
                            } else {
                                reader.readAsText(fileList[0]);
                            }
                            //reader.readAsDataURL(fileList[0]);
                            reader.onload = function (e) {
                                //获取到文件内容
                                if (isodt == true) {
                                    result = rootElement.LoadDocumentFromBinary(e.target.result, "odt");
                                }
                                else if (isofd == true) {
                                    result = rootElement.LoadDocumentFromBinary(e.target.result, "ofd");
                                }
                                else {
                                    result = WriterControl_IO.LoadDocumentFromString({ WriterControl: rootElement, Data: e.target.result });// rootElement.__DCWriterReference.invokeMethod("LoadDocumentFromString", e.target.result, null, null);
                                }
                                if (WriterControl_Task.__Tasks && WriterControl_Task.__Tasks.length > 0) {
                                    WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                                        WriterControl_Event.RaiseControlEvent(rootElement, "EventAfterDocumentDraw");
                                    });
                                    // 添加FileOpen后的回调函数【DUWRITER5_0-2744】
                                    if (!!callback && typeof (callback) === "function") {
                                        callback.call(rootElement);
                                    }
                                    return;
                                } else {
                                    WriterControl_Event.RaiseControlEvent(rootElement, "EventAfterDocumentDraw");
                                }
                                ////表示为文档正常加载
                                //if (result === true) {
                                //    rootElement.RefreshDocument();
                                //}

                                if (result) {
                                    rootElement.ClearOldVisibleElements();
                                }
                                // 添加FileOpen后的回调函数【DUWRITER5_0-2744】
                                if (!!callback && typeof (callback) === "function") {
                                    callback.call(rootElement);
                                }
                                //rootElement.LoadDocumentFromString(e.target.result, 'xml');
                            };
                        }
                    };
                    //在编辑器的window重新获取焦点时,确保点击取消或X时能正确删除file
                    window.addEventListener('focus', function () {
                        setTimeout(function () {
                            file.remove();
                        }, 100);
                    }, { once: true });
                    return result;
                    break;
                case "copy":
                    //var datas = '';
                    //var ref9 = rootElement.__DCWriterReference;
                    //if (ref9 != null) {
                    //    datas = ref9.invokeMethod(
                    //        "DoCopy", false);
                    //}
                    //WriterControl_UI.SetClipboardData(datas, null, 'copy', rootElement);
                    //document.execCommand('copy');
                    rootElement.isCopyAsText = false;
                    rootElement.Copy();
                    break;
                case "copyastext":
                    rootElement.isCopyAsText = true;
                    rootElement.Copy();
                    break;
                case "paste":
                    /*WriterControl_UI.GetClipboardData(null, rootElement);*/
                    rootElement.isPasteAsText = false;
                    rootElement.Paste();
                    break;
                case "pasteastext":
                    rootElement.isPasteAsText = true;
                    rootElement.Paste(Parameter);
                    break;
                case 'cut':
                    rootElement.Cut();
                    break;
                case "documentdefaultfont":
                    if (!Parameter) {
                        return false;
                    }
                    //默认字体
                    var result = rootElement.__DCWriterReference.invokeMethod("DocumentDefaultFont", Parameter);
                    result && (rootElement.Modified = true);
                    return result;
                    break;
                case "fontborder":
                    //字符边框
                    return rootElement.__DCWriterReference.invokeMethod("Fontborder");
                    break;
                case "documentvaluevalidatewithcreatedocumentcomments":
                    //批注式文档校验                    
                    return rootElement.__DCWriterReference.invokeMethod("DocumentValueValidateWithCreateDocumentComments");
                    break;
                case "filenew":
                    //新建的方法
                    rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                    return WriterControl_Paint.UpdateViewForWaterMark(rootElement);
                    break;
                case "insertmediaelement":
                    ////在此处处理视频文件是为了写入视频id保证能正常的获取和删除
                    //if (!Parameter || !(new RegExp('^http|https*').test(Parameter.FileName))) {
                    //    console.log('插入视频只支持网络路径http/https')
                    //    break;
                    //}
                    if (!Parameter) {
                        return;
                    }
                    if (!(Parameter && Parameter.ID)) {
                        Parameter['ID'] = 'media' + Date.now();
                    }
                    rootElement.closeScreenChange = true;
                    setTimeout(() => {
                        rootElement.closeScreenChange = false;
                    }, 1000);
                    rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                    //if (bolShowUI) {
                    //    WriterControl_Dialog.MediaDialog(Parameter, rootElement);
                    //}
                    break;
                //当前表格前插入回车
                case "insertparagraphbeforetable":
                    var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "insertparagraphbeforetable", bolShowUI, Parameter);
                    Boolean(result) && (rootElement.Modified = true);
                    return result;
                    break;
                case "showformbutton":
                    var result = rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", "ShowFormButton", bolShowUI, Parameter);
                    rootElement.DocumentOptions.ViewOptions.ShowFormButton = result;
                    rootElement.ApplyDocumentOptions();
                    return result;
                    break;
                case "headerbottomlinevisible":
                    var result = rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", "HeaderBottomLineVisible", bolShowUI, Parameter);
                    return result;
                    break;
                case "movetoposition"://移动光标位置
                    var result = rootElement.__DCWriterReference.invokeMethod("MoveToPosition", Parameter);
                    return result;
                    break;
                case "letterspacing"://设置字符间距
                    var result = rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", strCommandName, bolShowUI, Parameter);
                    return result;
                    break;

                default:
                    // 显示用户界面
                    if (bolShowUI == true) {
                        var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
                        switch (strCommandName.toLocaleLowerCase()) {
                            case "aboutcontrol":
                                rootElement.__DCWriterReference.invokeMethod("ShowAboutDialog", true);
                                break;
                            case "executecommand":
                                WriterControl_Dialog.DCExecuteCommandDialog(rootElement);
                                break;
                            case "charactercircle":
                                WriterControl_Dialog.CharacterCircleDialog(Parameter, rootElement, true);
                                break;
                            case "insertspecifycharacter":
                                WriterControl_Dialog.InsertSpecifyCharacterDialog(Parameter, rootElement, true);
                                break;
                            case "paragraphformat":
                                //段落对话框
                                WriterControl_Dialog.paragraphDialog(Parameter, rootElement, true);
                                break;
                            case "tableproperties":
                                WriterControl_Dialog.tableDialog(Parameter, rootElement, true);
                                break;
                            case "tablerowproperties":
                                WriterControl_Dialog.tableRowDialog(Parameter, rootElement, true);
                                break;
                            case "tablecolumnproperties":
                                WriterControl_Dialog.tableColumnDialog(Parameter, rootElement, true);
                                break;
                            case "tablecellproperties":
                                WriterControl_Dialog.tableCellDialog(Parameter, rootElement, true);
                                break;
                            case "table_splitcellext":
                                WriterControl_Dialog.splitCellDialog(Parameter, rootElement, true);
                                break;
                            case "insertcomment":
                                WriterControl_Dialog.EditDocumentCommentsDialog(Parameter, rootElement, true);
                                break;
                            case "font":
                                //字体
                                WriterControl_Dialog.FontSelectionDialog(Parameter, rootElement, false);
                                break;
                            case "inserttable":
                                WriterControl_Dialog.insertTableDialog(Parameter, rootElement, true, callback);
                                break;

                            case "formviewmode":
                                //阅读、预览、续打、区域选择都不能使用
                                if (IsOperateDOM) {
                                    //表单模式
                                    WriterControl_Dialog.formModeDialog(Parameter, rootElement, false);
                                }
                                break;

                            case "contentprotect":
                                //阅读、预览、续打、区域选择都不能使用
                                if (IsOperateDOM) {
                                    //内容保护
                                    WriterControl_Dialog.contentProtectedModeDialog(Parameter, rootElement, false);
                                }
                                break;
                            case "insertcheckboxorradio":
                                WriterControl_Dialog.InsertMultipleCheckBoxOrRadioDialog(Parameter, rootElement);
                                break;
                            case "insertlabelelement":
                                WriterControl_Dialog.LabelDialog(Parameter, rootElement, true);
                                break;
                            case "insertlabel":
                                WriterControl_Dialog.LabelDialog(Parameter, rootElement, true);
                                break;
                            case "inserthorizontalline":
                                // 水平线
                                WriterControl_Dialog.HorizontalLineDialog(Parameter, rootElement, true);
                                break;
                            case "insertpageinfoelement":
                                WriterControl_Dialog.PageNumberDialog(Parameter, rootElement, true);
                                break;
                            case "insertbutton":
                                // 按钮
                                WriterControl_Dialog.ButtonDialog(Parameter, rootElement, true);
                                break;
                            case "inserttdbarcodeelement":
                                WriterControl_Dialog.QRCodeDialog(Parameter, rootElement, true);
                                break;
                            case "insertbarcodeelement":
                                WriterControl_Dialog.BarCodeDialog(Parameter, rootElement, true);
                                break;
                            case "insertimage":
                            case "dcinsertimage":
                                var file = rootElement.ownerDocument.createElement("input");
                                file.setAttribute("id", "dcInputFile");
                                file.setAttribute("type", "file");
                                file.setAttribute("accept", "image/*");
                                file.style.display = 'none';
                                rootElement.appendChild(file);
                                file.click();
                                //file文件选中事件
                                file.onchange = function () {
                                    var imgFile;
                                    if (this.files) {
                                        imgFile = this.files[0];
                                    }
                                    if (imgFile) {
                                        var reader = new FileReader();
                                        reader.readAsDataURL(imgFile);
                                        reader.onload = function () {
                                            //创建一个图片获取到默认的宽高
                                            var imgEle = new Image();
                                            imgEle.src = reader.result;
                                            imgEle.onload = function () {
                                                var newsrc = reader.result;
                                                //wyc20230915:取消转换PNG，让图片元素的RawFormat体现其真实的格式信息
                                                //if (imgFile.type != 'image/png') {
                                                //    newsrc = changeImageTypeToPNG(imgEle)
                                                //}

                                                //优先使用传入的宽高值，如果没有宽高值，则使用图片的原始宽高
                                                Parameter = {
                                                    Width: Parameter && Parameter.Width ? Parameter.Width : imgEle.width,
                                                    Height: Parameter && Parameter.Height ? Parameter.Height : imgEle.height,
                                                    Src: newsrc,
                                                    //AutoFitImageSize: true
                                                };

                                                ReturnBase64FromSrc(Parameter, function (Parameter) {
                                                    rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, false, Parameter);
                                                });

                                                imgEle.remove();
                                                if (window.outerWidth - window.innerWidth > 100 ||
                                                    window.outerHeight - window.innerHeight > 100) {
                                                    //清除光标样式
                                                    WriterControl_UI.HideCaret(rootElement);
                                                }
                                                file.remove();
                                            };
                                        };
                                    }
                                };
                                //在编辑器的window重新获取焦点时,确保点击取消或X时能正确删除file
                                //window.addEventListener('focus', function () {
                                //    setTimeout(function () {
                                //        file.remove();
                                //    }, 100);
                                //}, { once: true });
                                break;

                            case "insertmedicalexpression":
                                WriterControl_Dialog.MedicalExpressionDialog(Parameter, rootElement, bolShowUI);
                                break;
                            case "insertinputfield":
                                WriterControl_Dialog.InputFieldDialog(Parameter, rootElement, bolShowUI, callback);
                                break;
                            case "color":
                                WriterControl_Dialog.ColorDialog(Parameter, rootElement, true);
                                break;
                            case "backcolor":
                                WriterControl_Dialog.BackColorDialog(Parameter, rootElement, true);
                                break;
                            case "elementproperties":
                                //属性对话框
                                var typename = rootElement.__DCWriterReference.invokeMethod("GetCurrentElementTypeName");//当前元素的类型名称
                                if (Parameter || (typename != null && typename != "")) {
                                    var ele = rootElement.CurrentElement(typename);
                                    var element = rootElement.GetElementProperties(ele);//rootElement.CurrentElement(typename);
                                    if (typename == "xtextinputfieldelement") {
                                        WriterControl_Dialog.InputFieldDialog(element, rootElement, false, callback);
                                        return true;
                                    }
                                    if (typename == "xtextradioboxelement") {
                                        WriterControl_Dialog.CheckboxAndRadioDialog(element, rootElement, ele);
                                        return true;
                                    }
                                    if (typename == "xtextcheckboxelement") {
                                        WriterControl_Dialog.CheckboxAndRadioDialog(element, rootElement, ele);
                                        return true;
                                    }
                                    if (typename == "xtextnewbarcodeelement") {
                                        WriterControl_Dialog.BarCodeDialog(element, rootElement, false, ele);
                                        return true;
                                    }
                                    if (typename == "xtexttdbarcodeelement") {
                                        WriterControl_Dialog.QRCodeDialog(element, rootElement, false, ele);
                                        return true;
                                    }
                                    if (typename == "xtextlabelelement") {
                                        WriterControl_Dialog.LabelDialog(element, rootElement, false, ele);
                                        return true;
                                    }
                                    if (typename == "xtexthorizontallineelement") {
                                        WriterControl_Dialog.HorizontalLineDialog(element, rootElement, false, ele);
                                        return true;
                                    }
                                    if (typename == "xtextimageelement") {
                                        WriterControl_Dialog.ImageDialog(element, rootElement, ele);
                                        return true;
                                    }
                                    if (typename == "xtextpageinfoelement") {
                                        WriterControl_Dialog.PageNumberDialog(element, rootElement, false, ele);
                                        return true;
                                    }
                                    if (typename == "xtextbuttonelement") {
                                        WriterControl_Dialog.ButtonDialog(element, rootElement, false, ele);
                                        return true;
                                    }
                                    if (typename == "xtextnewmedicalexpressionelement") {
                                        WriterControl_Dialog.MedicalExpressionDialog(element, rootElement, false, ele);
                                        return true;
                                    }
                                    if (typename == "xtextmediaelement") {
                                        WriterControl_Dialog.MediaDialog(element, rootElement, ele);
                                        return true;
                                    }
                                    if (typename == "xtextcontrolhostelement") {
                                        WriterControl_Dialog.InsertControlHostDialog(element, rootElement, false, ele);
                                        return true;
                                    }
                                }
                                return false;
                            case "normalviewmode":
                                WriterControl_Paint.RemoveAllPageElements(rootElement);
                                return rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "NormalViewMode", bolShowUI, Parameter);
                            case "pageviewmode":
                                WriterControl_Paint.RemoveAllPageElements(rootElement);
                                return rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "PageViewMode", bolShowUI, Parameter);
                            //转成大写
                            case "touppercase":
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "touppercase", bolShowUI, Parameter);
                                result && (rootElement.Modified = true);
                                break;
                            //转成小写
                            case "tolowercase":
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "tolowercase", bolShowUI, Parameter);
                                result && (rootElement.Modified = true);
                                break;
                            case "underlinestyle":
                                WriterControl_Dialog.UnderlineStyleDialog(Parameter, rootElement, true);
                                break;
                            //有序列表
                            case "insertorderedlist":
                                Parameter = Parameter ? Parameter : null;
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "insertorderedlist", false, Parameter);
                                return result;
                                break;
                            //插入图表
                            case "insertchartelement":
                                if (!Parameter) {
                                    return false;
                                }
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "insertchartelement", false, Parameter);
                                return result;
                                break;

                            //插入html
                            case "inserthtml":
                                if (!Parameter) {
                                    return false;
                                }
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "inserthtml", false, Parameter);
                                return result;
                                break;
                            case "printbackcolor"://打印背景色
                                WriterControl_Dialog.PrintBackColorDialog(Parameter, rootElement, true);
                                break;
                            case "printcolor"://打印字体色
                                WriterControl_Dialog.PrintColorDialog(Parameter, rootElement, true);
                                break;
                            //插入ControlHost
                            case "insertcontrolhost":
                                WriterControl_Dialog.InsertControlHostDialog(Parameter, rootElement, true);
                                return result;

                            default:
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                                if (cmdNamesForRefreshOptions.indexOf(strCommandName) >= 0) {
                                    rootElement.refreshDocumentOptions();
                                }
                                return result;
                                break;
                        }
                    } else {
                        //此处为需要改用ExecuteCommand的接口
                        switch (strCommandName.toLocaleLowerCase()) {
                            case "charactercircle":
                                rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", strCommandName, bolShowUI, Parameter);
                                break;
                            case "contentprotect":
                                rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", strCommandName, bolShowUI, Parameter);
                                break;
                            case "deletecomment":
                                var result = rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", "deletecomment", bolShowUI, Parameter);
                                Boolean(result) && (rootElement.Modified = true);
                                return result;
                                break;
                            //当前表格前插入回车
                            case "insertparagraphbeforetable":
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "insertparagraphbeforetable", bolShowUI, Parameter);
                                Boolean(result) && (rootElement.Modified = true);
                                return result;
                                break;
                            //转成大写
                            case "touppercase":
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "touppercase", bolShowUI, Parameter);
                                result && (rootElement.Modified = true);
                                break;
                            //转成小写
                            case "tolowercase":
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "tolowercase", bolShowUI, Parameter);
                                result && (rootElement.Modified = true);
                                break;
                            case "formatbrush":
                                rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                                rootElement.Modified = true;
                                break;
                            case "insertimage":
                            case "dcinsertimage":
                                //当没有参数时，不做任何操作
                                if (!Parameter) {
                                    return;
                                }
                                ReturnBase64FromSrc(Parameter, function (options) {
                                    rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, false, options);
                                });
                                break;
                            case "colorforfieldtextcolor"://设置选中文本字体颜色，包括输入域的字体颜色
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                                break;
                            case "formviewmode":
                                if (rootElement.FormView() == Parameter) {
                                    return Parameter;
                                }

                                var result = rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", strCommandName, false, Parameter);
                                rootElement.DocumentOptions.BehaviorOptions.FormView = result;
                                rootElement.ApplyDocumentOptions();//更改选项的值
                                break;
                            case "inserttable":
                            case "insertcheckboxorradio":
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                                // 触发文档内容变化事件
                                var opt = {
                                    /** 触发事件类型 */
                                    TriggerType: strCommandName.toLocaleLowerCase()
                                };
                                WriterControl_Event.RaiseControlEvent(rootElement, "DocumentContentChanged", opt);
                                return result;
                                break;
                            case "fontsize":
                                //去掉前导后导空格
                                if (typeof (Parameter) === "number") {
                                    Parameter = Parameter.toString();
                                }
                                Parameter = Parameter.trim();
                                if (Parameter) {
                                    var fontsizeObject = {
                                        "大特号": "63",
                                        "特号": "54",
                                        "初号": "42",
                                        "小初": "36",
                                        "一号": "26.25",
                                        "小一": "24",
                                        "二号": "21.75",
                                        "小二": "18",
                                        "三号": "15.75",
                                        "小三": "15",
                                        "四号": "14.25",
                                        "小四": "12",
                                        "五号": "10.5",
                                        "小五": "9",
                                        "六号": "7.5",
                                        "小六": "6.75",
                                        "七号": "5.25",
                                        "八号": "4.5",
                                    };
                                    var fontsizeObjectKeys = Object.keys(fontsizeObject);
                                    var fontsizeNumber = fontsizeObjectKeys.includes(Parameter) ? fontsizeObject[Parameter] : Parameter;
                                    var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, fontsizeNumber);
                                    return result;
                                }
                                break;
                            case "insertpageinfoelement":
                                //判断是否存在NumberDisplayedInTibetan属性
                                var hasNumberDisplayedInTibetan = rootElement.getAttribute("numberdisplayedintibetan");
                                if (typeof hasNumberDisplayedInTibetan == "string" && hasNumberDisplayedInTibetan.toLowerCase() == "true") {
                                    //获取最新的页面个数并更新
                                    var allIndex = rootElement.PageCount;
                                    var tibetanString = ["༠", "༡", "༢", "༣", "༤", "༥", "༦", "༧", "༨", "༩"];
                                    //循环数值
                                    var newSpecifyPageIndexTextList = "";
                                    for (var z = 1; z <= allIndex; z++) {
                                        if (newSpecifyPageIndexTextList != "") {
                                            newSpecifyPageIndexTextList += ",";
                                        }
                                        var stringArr = String(z).split("");
                                        stringArr.forEach(item => {
                                            item = Number(item);
                                            newSpecifyPageIndexTextList += tibetanString[item];
                                        });
                                    }
                                    Parameter.SpecifyPageIndexTextList = newSpecifyPageIndexTextList;
                                }
                                rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                                break;
                            case "moveto":
                                //20240403 lixinyu 修复AfterCell不生效问题，防止客户使用报错(DUWRITER5_0-2195)
                                if (Parameter === 'AfterCell') {
                                    Parameter = 'NextCell';
                                };
                                rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", strCommandName, bolShowUI, Parameter);
                                break;
                            case "insertlabelelement":
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "insertlabelelement", bolShowUI, Parameter);
                                return result;
                                break;
                                break;
                            case "insertlabel":
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "insertlabelelement", bolShowUI, Parameter);
                                return result;
                                break;
                            case "printbackcolor"://打印背景色
                                var result = rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", "printbackcolor", bolShowUI, Parameter);
                                return result;
                            case "printcolor"://打印字体色
                                var result = rootElement.__DCWriterReference.invokeMethod("ExecuteCommand", "printcolor", bolShowUI, Parameter);
                                rootElement.DocumentOptions.ViewOptions.BothBlackWhenPrint = false;
                                rootElement.ApplyDocumentOptions();
                                return result;
                            case "color"://字体色
                            case "backcolor"://字体色
                                if (Parameter && typeof Parameter == "string") {
                                    if (/^#[0-9A-Fa-f]{8}$/.test(Parameter)) {
                                        let red = parseInt(Parameter.slice(1, 3), 16);
                                        let green = parseInt(Parameter.slice(3, 5), 16);
                                        let blue = parseInt(Parameter.slice(5, 7), 16);
                                        let alpha = parseInt(Parameter.slice(7, 9), 16) / 255;
                                        Parameter = `rgba(${red}, ${green}, ${blue}, ${alpha})`;
                                    }
                                }
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                                return result;
                            default:
                                var result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", strCommandName, bolShowUI, Parameter);
                                if (cmdNamesForRefreshOptions.indexOf(strCommandName) >= 0) {
                                    rootElement.refreshDocumentOptions();
                                }
                                return result;
                                break;
                        }
                    }
                    break;
            }
            DCEventInterfaceLogFunction(rootElement, 'DCExecuteCommand_' + strCommandName, startTime);


            //封装时间不够补0
            function showTime(t) {
                var time;
                time = t >= 10 ? t : '0' + t;
                return time;
            }

            //转换图片格式
            function changeImageTypeToPNG(img) {
                var canvas = rootElement.ownerDocument.createElement('canvas');
                canvas.width = img.width;
                canvas.height = img.height;
                canvas.getContext('2d').drawImage(img, 0, 0);
                var src = canvas.toDataURL('image/png');
                canvas.remove();
                return src;
            }
        };

        /**
         * @name ManuallyLoadingfonts
         * @type function
         * @apinameZh 手动加载宋体字体并写入编辑器
         * @classification file
         * @param ["callback","string","错误的回调函数","","",true]
         */
        rootElement.ManuallyLoadingfonts = function (callBack) {
            var strUrl = `${window.strBasePath}?wasmres=fonts/simsun_old.ttf&ver=${window.strAppVersion}&flag=261861&wasmrootpath=${decodeURIComponent(window.strBasePath)}`;
            var xhr = new XMLHttpRequest();
            xhr.open("GET", strUrl, true);
            xhr.responseType = "blob";
            xhr.onload = function () {
                if (this.status == 200) {
                    // 操作成功
                    try {
                        var blob = this.response;
                        DCTools20221228.blobToArrayBuffer(blob, buffer => {
                            var buffer2 = new Uint8Array(buffer);
                            DotNet.invokeMethod(
                                window.DCWriterEntryPointAssemblyName,
                                "SetFontFileContent",
                                "宋体",
                                "宋体",
                                buffer2);
                        });
                    } catch (err) {
                        callBack(err, strUrl, errorEvent);
                    }
                }
            };
            xhr.onerror = function (err) {
                callBack(err, strUrl, errorEvent);
            };
            xhr.send();

            function errorEvent(blob) {
                DCTools20221228.blobToArrayBuffer(blob, buffer => {
                    var buffer2 = new Uint8Array(buffer);
                    DotNet.invokeMethod(
                        window.DCWriterEntryPointAssemblyName,
                        "SetFontFileContent",
                        "宋体",
                        "宋体",
                        buffer2);
                });
            }
        };

        /**
        * @name GetElementProperties
        * @type function
        * @apinameZh 获取指定ID的元素的属性
        * @classification structuralelement
        * @param ["ELEMENT","object","要修改的前端引用元素","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-05-12","和四代接口保持兼容","wyc" ]
        */
        rootElement.GetElementProperties = function (element) {
            var startTime = new Date();
            let result = null;
            if (element === null) {
                console.log("GetElementProperties:element为空");
                //debugger;
                DCEventInterfaceLogFunction(rootElement, 'GetElementProperties', startTime);
                return null;
            }
            if (typeof (element) === "object" && typeof (element.serializeAsArg) === "function") {
                result = rootElement.__DCWriterReference.invokeMethod("DCGetElementProperties", element);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("DCGetElementProperties2", element);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementProperties', startTime);
            return result;
        };


        /**
         * @name GetDCWriterVersion
         * @type function
         * @apinameZh 获取版本号
         * @classification structuralelement
         * @returns ["result","string","版本号字符串"]
         * @change ["2024-09-30","创建API","wyc" ]
         */
        rootElement.GetDCWriterVersion = function () {
            return rootElement.__DCWriterReference.invokeMethod("GetDCWriterVersion");
        };

        /**
         * @name SetElementProperties
         * @type function
         * @apinameZh 设置指定ID的元素的属性
         * @classification structuralelement
         * @param ["element","object","要修改的前端引用元素","","",true]
         * @param ["options","object","元素的属性集合对象","","",true]
         * @param ["isrefresh","boolean","是否设置完属性后刷新","刷新","",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2023-05-12","和四代接口保持兼容","wyc" ]
         * @change ["2024-08-19","添加刷新参数","wyc" ]
         * @describe （PrintVisibilityExpression、VisibleExpression）可见性表达式放在PropertyExpressions对象中生效。
         */
        rootElement.SetElementProperties = function (element, options, isrefresh = true) {
            var startTime = new Date();
            let result = false;
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            var ReadViewMode = rootElement.ReadViewMode;//阅读模式
            if (IsPrintPreview || ReadViewMode) {
                return result;
            }
            if (DCTools20221228.IsDotnetReferenceElement(element) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("DCSetElementProperties", element, options, isrefresh);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("DCSetElementProperties2", element, options, isrefresh);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetElementProperties', startTime);
            return result;
        };

        /**
         * @name InsertImageByJSONText
         * @type function
         * @apinameZh 使用JSON参数插入图片
         * @classification structuralelement
         * @param ["options","object","元素的属性集合对象","","",true]
         * @param ["callback","function","回调函数","","",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2023-05-15","和四代接口保持兼容","wyc" ]
         * @describe options 可以是单个对象，也可以是对象数组，单个对象内属性包括( ID、Src、Width、Height、SaveContentInFile)
         */
        rootElement.InsertImageByJSONText = function (options, callback) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = null;
            var opt = options;
            if (typeof (options) == "string") {
                try {
                    opt = JSON.parse(options);
                }
                catch (e) { opt = null; }
            }
            if (opt != null) {
                ReturnBase64FromSrc(opt, function (opt) {
                    result = rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "InsertImage", false, opt);
                });
            }
            if (typeof (callback) == "function") {
                setTimeout(() => {
                    callback.call(rootElement, result);
                });
            }
            DCEventInterfaceLogFunction(rootElement, 'InsertImageByJSONText', startTime);
            return result;
        };

        /**
         *将网络图片转换为base64
         *@param {string} 图片地址
         *@return {string} base64字符串
         */
        function ReturnBase64FromSrc(opt, callback) {
            if (Array.isArray(opt)) {
                //如果为数组
                Promise.all(function* () {
                    for (var i = 0; i < opt.length; i++) {
                        yield new Promise(resolve => {
                            if (opt[i].Src && new RegExp('^http|https*').test(opt[i].Src)) {
                                // console.log(opt[i].width);
                                let width = opt[i].width || 240;
                                let height = opt[i].height || 120;
                                var img = new Image();
                                img.src = opt[i].Src;
                                //是否需要保持长宽比
                                if (opt && opt.KeepWidthHeightRate) {
                                    let aspectRatio = img.width / img.height;
                                    if (width / height > aspectRatio) {
                                        width = height * aspectRatio;
                                    } else {
                                        height = width / aspectRatio;
                                    }
                                }
                                img.setAttribute('crossorigin', 'anonymous');
                                img.setAttribute('targetIndex', i);
                                img.onload = function () {
                                    //将地址写入canvas
                                    var canvas = rootElement.ownerDocument.createElement('canvas');
                                    canvas.width = width;
                                    canvas.height = height;
                                    canvas.getContext('2d').drawImage(img, 0, 0, width, height);
                                    var tarOpt = opt[img.getAttribute('targetIndex')];
                                    tarOpt.Src = canvas.toDataURL('image/png');
                                    canvas.remove();
                                    resolve(tarOpt);
                                };
                            } else {
                                resolve(opt[i]);
                            }
                        });
                    }
                }()).then(result => {
                    callback(result);
                });
            } else {
                //如果为非数组
                if (opt.Src && new RegExp('^http|https*').test(opt.Src)) {
                    var img = new Image();
                    img.src = opt.Src;
                    img.setAttribute('crossorigin', 'anonymous');
                    img.onload = function () {
                        var canvas = rootElement.ownerDocument.createElement('canvas');
                        canvas.width = img.width;
                        canvas.height = img.height;
                        canvas.getContext('2d').drawImage(img, 0, 0, img.width, img.height);
                        opt.Src = canvas.toDataURL('image/png');
                        canvas.remove();
                        callback(opt);
                    };
                } else {
                    callback(opt);
                }
            }
            ////判断是否为网络图片
            //if (src && new RegExp('^http|https*').test(src)) {
            //    var img = rootElement.ownerDocument.createElement('img');
            //    img.src = src;
            //    img.onload = function () {
            //        //将地址写入canvas
            //        var canvas = rootElement.ownerDocument.createElement('canvas');
            //        canvas.getContext('2d').drawImage(img, 0, 0);
            //        var src = canvas.toDataURL('image/png');
            //        canvas.remove();
            //        callback(src);
            //    }
            //}
        };

        /**
         * @name NewComment
         * @type function
         * @apinameZh 兼容四代插入批注
         * @classification structuralelement
         * @param ["commentInfo","object","批注信息","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.NewComment = function (commentInfo) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("NewComment", commentInfo);
            DCEventInterfaceLogFunction(rootElement, 'NewComment', startTime);
            return result;
        };

        /**
         * @name getCommentList
         * @type function
         * @apinameZh 兼容四代获取批注列表
         * @classification structuralelement
         * @returns ["result","Array","批注列表信息"]
         */
        rootElement.getCommentList = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("getCommentList");
            DCEventInterfaceLogFunction(rootElement, 'getCommentList', startTime);
            return result;
        };

        /**
        * @name ActiveCommentByIndex
        * @type function
        * @apinameZh 批注定位
        * @classification structuralelement
        * @param ["index","number","批注在列表中的序号，从0开始","","",true]
        */
        rootElement.ActiveCommentByIndex = function (index) {
            var startTime = new Date();
            //非空非数字判断
            if (isNaN(index)) {
                return false;
            }
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            rootElement.FocusElementByIdScrollMode = "Middle";
            let result = rootElement.__DCWriterReference.invokeMethod("ActiveCommentByIndex", index);
            DCEventInterfaceLogFunction(rootElement, 'ActiveCommentByIndex', startTime);
            return result;
        };


        /**
         * @name setCommentContent
         * @type function
         * @apinameZh 兼容四代修改指定批注内容
         * @classification structuralelement
         * @param ["index","number","批注下标","","",true]
         * @param ["newContent","string","批注文本","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.setCommentContent = function (index, newContent) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("setCommentContent", index, newContent);
            DCEventInterfaceLogFunction(rootElement, 'setCommentContent', startTime);
            return result;
        };

        /**
         * @name DeleteComment
         * @type function
         * @classification structuralelement
         * @apinameZh 兼容四代删除指定批注内容
         * @param ["index","number","批注序号","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.DeleteComment = function (index) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("DeleteComment", index);
            Boolean(result) && (rootElement.Modified = true);
            //WriterControl_Paint.commentListLength = WriterControl_Paint.commentListLength ? WriterControl_Paint.commentListLength - 1 : 0;
            DCEventInterfaceLogFunction(rootElement, 'DeleteComment', startTime);
            return result;
        };

        /**
         * @name GetCurrentComment
         * @type function
         * @apinameZh 获取当前批注
         * @classification structuralelement
         * @returns ["result","boolean","执行结果"]
         */
        rootElement.GetCurrentComment = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("GetCurrentComment");
            DCEventInterfaceLogFunction(rootElement, 'GetCurrentComment', startTime);
            return result;
        };

        /**
         * @name SetCurrentComment
         * @type function
         * @apinameZh 设置当前批注
         * @classification structuralelement
         * @param ["parameter","string","parameter","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SetCurrentComment = function (parameter) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SetCurrentComment", parameter);
            DCEventInterfaceLogFunction(rootElement, 'SetCurrentComment', startTime);
            return result;
        };

        /**
         * @name CurrentTableCell
         * @type function
         * @apinameZh 兼容四代获取当前单元格
         * @classification table
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.CurrentTableCell = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("CurrentTableCell");
            DCEventInterfaceLogFunction(rootElement, 'CurrentTableCell', startTime);
            return result;
        };

        /**
         * @name GetTableCellGridLineInfo
         * @type function
         * @classification table
         * @apinameZh 兼容四代获取单元格的网格线设置
         * @param ["parameter","object","网格线属性","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.GetTableCellGridLineInfo = function (parameter) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetTableCellGridLineInfo", parameter);
            DCEventInterfaceLogFunction(rootElement, 'GetTableCellGridLineInfo', startTime);
            return result;
        };

        /**
        * @name SetTableCellGridLineInfo
        * @type function
        * @apinameZh 兼容四代设置单元格网格线
        * @classification table
        * @param ["cell","object","单元格","","",true]
        * @param ["settings","object","单元格网格线","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2024-04-29","兼容各种传入参数","wyc" ]
        */
        rootElement.SetTableCellGridLineInfo = function (cell, settings) {
            var startTime = new Date();
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(cell) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableCellGridLineInfo2", cell, settings);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableCellGridLineInfo", cell, settings);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetTableCellGridLineInfo', startTime);
            return result;
        };

        /**
       * @name SetTableBorder
       * @type function
       * @apinameZh 兼容四代设置表格的边框
       * @classification table
       * @param ["table","object","表格","","",true]
       * @param ["settings","object","表格的边框属性","","",true]
       */
        rootElement.SetTableBorder = function (table, settings) {
            //wyc20230707采用新写法
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                return false;
            }
            var prop = rootElement.GetElementProperties(table);
            if (prop != null && Array.isArray(prop.Cells) === true) {
                var opt = {
                    Style: settings
                };
                for (var i = 0; i < prop.Cells.length; i++) {
                    rootElement.SetElementProperties(prop.Cells[i], opt);
                }
                rootElement.SetElementProperties(table, opt);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetTableBorder', startTime);
            //return rootElement.__DCWriterReference.invokeMethod("SetTableBorder", table, settings);
        };

        /**
        * @name SetTableCellBorder
        * @type function
        * @apinameZh 兼容四代设置单元格的边框
        * @classification table
        * @param ["cell","object","单元格","","",true]
        * @param ["settings","object","单元格的边框属性","","",true]
        */
        rootElement.SetTableCellBorder = function (cell, settings) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                return false;
            }

            //判断目标元素是否为单元格
            var cell = rootElement.GetElementProperties(cell);
            if (cell && cell.TypeName && cell.TypeName === "XTextTableCellElement") {
                //wyc20230710采用新写法
                var opt = {
                    Style: settings
                };
                rootElement.SetElementProperties(cell, opt);
            }

            //return rootElement.__DCWriterReference.invokeMethod("SetTableCellBorder", cell, settings);
            DCEventInterfaceLogFunction(rootElement, 'SetTableCellBorder', startTime);

        };

        /**
       * @name BeginFormatBrush
       * @type function
       * @apinameZh 兼容四代格式刷
       * @classification editformat
       */
        rootElement.BeginFormatBrush = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("BeginFormatBrush");
            DCEventInterfaceLogFunction(rootElement, 'BeginFormatBrush', startTime);
        };

        /**
         * @name Undo
         * @type function
         * @apinameZh 兼容四代撤销
         * @classification editformat
         */
        rootElement.Undo = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                rootElement.__DCWriterReference.invokeMethod("Undo");
                DCEventInterfaceLogFunction(rootElement, 'Undo', startTime);
            }
        };

        /**
        * @name ParagraphFormat
        * @type function
        * @apinameZh 段落格式接口
        * @classification editformat
        * @param ["paragraphFormatParameter","object","段落格式属性","","","true"]
        */
        rootElement.ParagraphFormat = function (paragraphFormatParameter) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                rootElement.__DCWriterReference.invokeMethod("ParagraphFormat", paragraphFormatParameter);
                DCEventInterfaceLogFunction(rootElement, 'ParagraphFormat', startTime);
            }

        };

        /**
         * @name AboutDialog
         * @type function
         * @apinameZh 兼容四代关于接口
         * @classification file
         * @param ["flag","boolean","是否弹出alert提示","true","","false"]
         * @returns ["result","json","参数为false时会返回json数据"]
         */
        rootElement.AboutDialog = function (flag = true) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("ShowAboutDialog", flag);
            DCEventInterfaceLogFunction(rootElement, 'AboutDialog', startTime);
            return result;
        };

        /**
         * @name AboutControl
         * @type function
         * @apinameZh 兼容四代关于接口
         * @classification file
         * @param ["flag","boolean","是否弹出alert提示","true","","false"]
         * @returns ["result","json","参数为false时会返回json数据"]
         */
        rootElement.AboutControl = function (flag = true) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("ShowAboutDialog", flag);
            DCEventInterfaceLogFunction(rootElement, 'AboutControl', startTime);
            return result;
        };

        /**
        * @name AppendBody
        * @type function
        * @classification file
        * @apinameZh 兼容第四代接口正文追加内容
        * @param ["xmlContent","string","xml内容","","",true]
        */
        rootElement.AppendBody = function (xmlContent) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//阅读、预览、续打、区域选择时，不能修改dom
            if (IsOperateDOM) {
                rootElement.__DCWriterReference.invokeMethod("AppendBody", xmlContent);
            }
            DCEventInterfaceLogFunction(rootElement, 'AppendBody', startTime);
        };

        /**
        * @name ClearAllFields
        * @type function
        * @classification structuralelement
        * @param ["isClearAllFields","boolean","是否同时清空输入域内的图片","false","","false"]
        * @apinameZh 兼容第四代接口清空所有输入域
        */
        rootElement.ClearAllFields = function (isClearAllFields = false) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("ClearAllFields", isClearAllFields);
            DCEventInterfaceLogFunction(rootElement, 'ClearAllFields', startTime);
        };

        /**
         * @name ClearAllParameterValue
         * @type function
         * @classification datasource
         * @apinameZh 兼容第四代接口清空所有绑定数据源的输入域的值和文本、单选框的选择状态、复选框的选择状态
         */
        rootElement.ClearAllParameterValue = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("ClearAllParameterValue");
            rootElement.Modified = true;
            DCEventInterfaceLogFunction(rootElement, 'ClearAllParameterValue', startTime);
        };

        /**
         * @name ClearDocumentBody
         * @type function
         * @apinameZh 兼容第四代接口清空正文内容
         * @classification file
         */
        rootElement.ClearDocumentBody = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;//只读
            var formView = rootElement.FormView();
            //阅读、预览、续打、区域选择，只读，表单模式下都不能修改dom
            if (IsOperateDOM && (readonly === false) && (formView === 'Disable')) {
                rootElement.__DCWriterReference.invokeMethod("ClearDocumentBody");
                DCEventInterfaceLogFunction(rootElement, 'ClearDocumentBody', startTime);
            }
        };

        /**
         * @name CommitDocumentUserTrace
         * @type function
         * @apinameZh 兼容第四代接口提交用户痕迹信息
         * @classification vestige
         */
        rootElement.CommitDocumentUserTrace = function () {
            var startTime = new Date();
            var readonly = rootElement.Readonly;//只读
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            var RectInfo = rootElement.RectInfo;//区域选择
            if (readonly || ExtViewMode || RectInfo) {
                //只读、续打、区域选择时禁用此接口
                return false;
            }
            rootElement.__DCWriterReference.invokeMethod("CommitDocumentUserTrace");
            DCEventInterfaceLogFunction(rootElement, 'CommitDocumentUserTrace', startTime);
        };

        /**
         * @name CurrentCheckboxOrRadio
         * @type function
         * @apinameZh 获取当前单选框或复选框
         * @classification structuralelement
         * @returns ["result","object","单选框或复选框"]
         */
        rootElement.CurrentCheckboxOrRadio = function () {
            var startTime = new Date();
            let result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//阅读、预览、续打、区域选择时，不能修改dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("CurrentCheckboxOrRadio");
            }
            DCEventInterfaceLogFunction(rootElement, 'CurrentCheckboxOrRadio', startTime);
            return result;
        };

        /**
         * @name CurrentInputField
         * @type function
         * @apinameZh 获取当前输入域
         * @classification structuralelement
         * @returns ["result","object","当前输入域"]
         */
        rootElement.CurrentInputField = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("CurrentInputField");
            DCEventInterfaceLogFunction(rootElement, 'CurrentInputField', startTime);
            return result;
        };

        /**
         * @name CurrentTable
         * @type function
         * @apinameZh 获取当前表格
         * @classification table
         * @returns ["result","object","当前表格"]
         */
        rootElement.CurrentTable = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("CurrentTable");
            DCEventInterfaceLogFunction(rootElement, 'CurrentTable', startTime);
            return result;
        };

        /**
         * @name CurrentTableRow
         * @type function
         * @classification table
         * @apinameZh 获取当前表格行
         * @returns ["result","object","当前表格行"]
         */
        rootElement.CurrentTableRow = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("CurrentTableRow");
            DCEventInterfaceLogFunction(rootElement, 'CurrentTableRow', startTime);
            return result;
        };

        /**
         * @name DocumentSelection
         * @type function
         * @apinameZh 获取当前选择内容
         * @classification file
         * @param ["format","string","数据格式","","",true]
         * @returns ["result","any","当前选择内容"]
         */
        rootElement.DocumentSelection = function (format) {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//阅读、预览、续打、区域选择时，不能修改dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("DocumentSelection", format);
            }
            DCEventInterfaceLogFunction(rootElement, 'DocumentSelection', startTime);
            return result;
        };

        /**
         * @name FocusAdjacent
         * @type function
         * @apinameZh 移动输入焦点到指定地点
         * @classification cursor
         * @param ["sWhere","string","焦点位置","","光标移动到:beforeEnd,光标移动到:afterBegin",true]
         * @param ["element","object","元素","","",true]
         * @returns ["result","any","当前选择内容"]
         */
        rootElement.FocusAdjacent = function (sWhere, element) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = null;
            //wyc20230710:改成兼容.NET后台引用对象
            if (typeof (element) === "object" && typeof (element.serializeAsArg) === "function") {
                result = rootElement.__DCWriterReference.invokeMethod("FocusAdjacent2", sWhere, element);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("FocusAdjacent", sWhere, element);
            }
            DCEventInterfaceLogFunction(rootElement, 'FocusAdjacent', startTime);
            return result;
        };

        /**
        * @name CollectrResizeCanvas
        * @type function
        * @apinameZh (不对外暴露) 此方法用于处理canvas上的resizeObserver监听,并处理EventPageResize事件
        * @param ["element","object","修改的元素","","",true]
        */
        rootElement.CollectrResizeCanvas = function (element) {
            var startTime = new Date();
            if (rootElement != null && typeof rootElement.EventPageResize == 'function') {
                if (!rootElement.DCResizeCanvas) {
                    rootElement.DCResizeCanvas = [];
                }
                rootElement.DCResizeCanvas.push(element);
                clearTimeout(rootElement.ResizeTime);
                rootElement.ResizeTime = setTimeout(function () {
                    rootElement.EventPageResize(rootElement.DCResizeCanvas);
                    rootElement.DCResizeCanvas = [];
                    clearTimeout(rootElement.ResizeTime);
                    delete rootElement.ResizeTime;
                    clearTimeout(rootElement.ResizeHandleViewScroll);
                    rootElement.ResizeHandleViewScroll = setTimeout(function () {
                        var pContainer = WriterControl_UI.GetPageContainer(rootElement);
                        WriterControl_Rule.HandleViewScroll(pContainer);
                        delete rootElement.ResizeHandleViewScroll;
                    }, 20);
                    DCEventInterfaceLogFunction(rootElement, 'CollectrResizeCanvas', startTime);
                }, 10);
            }
        };

        /**
       * @name DCPaste
       * @type function
       * @apinameZh 执行粘贴操作
       * @classification editformat
       * @returns ["result","boolean","操作是否成功"]
       */
        rootElement.DCPaste = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                let result = rootElement.__DCWriterReference.invokeMethod("DCPaste");
                DCEventInterfaceLogFunction(rootElement, 'DCPaste', startTime);
                return result;
            }
            return false;
        };

        /**
       * @name GetElementTextByID
       * @type function
       * @apinameZh 获取指定编号的元素的文本
       * @classification structuralelement
       * @param ["id","object","元素","","元素ID/NativeHandle/后台.NET引用对象",true]
       * @param ["options","object","属性","","{ SpecifyParent: 'subdocid', IncludeBackgroundText: false, IncludeBorderText: false, IncludeHiddenText: false, IncludeLabelUnitText: false, IncludeLogicDeletedContent: false }",false]
       * @returns ["result","boolean","操作是否成功"]
       * @describe 该接口支持参数 document.WriterControl.GetElementTextByID('txtAge', {SpecifyParent: 'subdocid', IncludeBackgroundText: false, IncludeBorderText: false, IncludeHiddenText: false, IncludeLabelUnitText: false, IncludeLogicDeletedContent: false });，IncludeBackgroundText表示是否包含背景文字，IncludeBorderText是否保存边框文字，IncludeHiddenText是否包含隐藏的文本，IncludeLabelUnitText是否包含单位文本，IncludeLogicDeletedContent是否包含逻辑删除内容
       * @change ["2024-5-6","扩展options参数支持指定查找元素的父容器","wyc" ]
       */
        rootElement.GetElementTextByID = function (id, options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = null;
            //wyc20230726:新增直接传元素后台引用 对象
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementTextByID2", id, options);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementTextByID", id, options);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementTextByID', startTime);
            return result;
        };

        /**
        * @name GetAllInputFields
        * @type function
        * @classification structuralelement
        * @apinameZh 获取所有的输入域
        * @param ["excludeReadonly2","Boolean","是否排除只读元素","","",true]
        * @param ["excludeHiddenElement2","Boolean","是否排除隐藏元素","","",true]
        * @param ["specifyRootElement","string","指定位置如：body，header，footer","","",true]
        * @param ["nestNode","string","nestNode","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.GetAllInputFields = function (excludeReadonly2, excludeHiddenElement2, specifyRootElement, nestNode) {
            var startTime = new Date();
            if (typeof (specifyRootElement) === "string") {
                var str = specifyRootElement.toLowerCase();
                if (str === "body") {
                    specifyRootElement = rootElement.DocumentBody;
                } else if (str === "header") {
                    specifyRootElement = rootElement.DocumentHeader;
                } else if (str === "footer") {
                    specifyRootElement = rootElement.DocumentFooter;
                }
            }
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(specifyRootElement) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetAllInputFields", excludeReadonly2, excludeHiddenElement2, specifyRootElement, nestNode);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetAllInputFields2", excludeReadonly2, excludeHiddenElement2, specifyRootElement, nestNode);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetAllInputFields', startTime);
            return result;
        };

        /**
       * @name getFontObject
       * @type function
       * @apinameZh 获取当前字体
       * @classification editformat
       * @returns ["result","object","执行结果"]
       */
        rootElement.getFontObject = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("getFontObject");
            DCEventInterfaceLogFunction(rootElement, 'getFontObject', startTime);
            return result;
        };

        /**
        * @name setFontObject
        * @type function
        * @apinameZh 设置当前字体
        * @classification editformat
        * @param ["parameter","object","当前字体","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.setFontObject = function (parameter) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                let result = rootElement.__DCWriterReference.invokeMethod("setFontObject", parameter);
                DCEventInterfaceLogFunction(rootElement, 'setFontObject', startTime);
                return result;
            }
            return false;
        };

        /**
       * @name getFontSize
       * @type function
       * @apinameZh 获取当前字体大小
       * @classification editformat
       * @returns ["result","number","当前字体大小"]
       */
        rootElement.getFontSize = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                let result = rootElement.__DCWriterReference.invokeMethod("getFontSize");
                DCEventInterfaceLogFunction(rootElement, 'getFontSize', startTime);
                return result;
            }
            return null;

        };

        /**
         * @name GetDocumentInfos
         * @type function
         * @apinameZh 获取当前文档信息
         * @classification editformat
         * @returns ["result","object","当前文档信息"]
         */
        rootElement.GetDocumentInfos = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentInfos");
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentInfos', startTime);
            return result;
        };

        /**
        * @name GetLabelElementContactSettings
        * @type function
        * @apinameZh 获取标签的链接信息
        * @classification structuralelement
        * @param ["parameter","object","参数","","",true]
        * @returns ["result","object","标签的链接信息"]
        */
        rootElement.GetLabelElementContactSettings = function (parameter) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetLabelElementContactSettings", parameter);
            DCEventInterfaceLogFunction(rootElement, 'GetLabelElementContactSettings', startTime);
            return result;
        };

        /**
        * @name GetElementPrintVisibility
        * @type function
        * @apinameZh 获取元素的PrintVisibility属性
        * @classification structuralelement
        * @param ["parameter","object","元素","","",true]
        * @returns ["result","object","元素的PrintVisibility属性"]
        */
        rootElement.GetElementPrintVisibility = function (parameter) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetElementPrintVisibility", parameter);
            DCEventInterfaceLogFunction(rootElement, 'GetElementPrintVisibility', startTime);
            return result;
        };

        /**
        * @name SetDocumentInfos
        * @type function
        * @apinameZh 设置文档的DocumentInfo
        * @classification file
        * @param ["parameter","object","文档的DocumentInfo","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetDocumentInfos = function (parameter) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SetDocumentInfos", parameter);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentInfos', startTime);
            return result;
        };

        /**
       * @name CurrentElement
       * @type function
       * @apinameZh 获取当前元素信息
       * @classification structuralelement
       * @param ["typename","string","元素的类型","","",true]
       * @returns ["result","object","当前元素信息"]
       */
        rootElement.CurrentElement = function (typename) {
            var startTime = new Date();
            let result = '';
            //处理当为表单模式且存在不自动获取焦点时控制currentElement的返回值
            if (DCTools20221228.IsReadonlyAutoFocus(rootElement, true)) {
                var hasTextArea = rootElement.querySelector("#txtEdit20221213");
                if (hasTextArea == null) {
                    return result;
                }
            }

            if (typename != null && typename != "") {
                result = rootElement.__DCWriterReference.invokeMethod("CurrentElement", typename);
            } else {
                var currenttypename = rootElement.__DCWriterReference.invokeMethod("GetCurrentElementTypeName");//当前元素的类型名称
                //if (currenttypename != null && currenttypename != "")
                {
                    result = rootElement.__DCWriterReference.invokeMethod("CurrentElement", currenttypename);
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'CurrentElement', startTime);
            return result;
        };

        /**
         * @name GetDocumentUserTrackInfos
         * @type function
         * @apinameZh 获取痕迹列表信息
         * @classification vestige
         * @param ["parameter","boolean","是否是清洁模式","","",true]
         * @returns ["result","Array","列表"]
         */
        rootElement.GetDocumentUserTrackInfos = function (parameter) {
            var startTime = new Date();

            let isClearMode = false;
            if (parameter == null) {
                isClearMode = false;//非清洁模式
            }
            else if (parameter == "true" || parameter == true) {
                isClearMode = true;//清洁模式
            }
            else if (parameter === "false" || parameter === false) {
                isClearMode = false;//非清洁模式
            }
            else {
                isClearMode = false;//非清洁模式
            }
            var list = rootElement.__DCWriterReference.invokeMethod("GetDocumentUserTrackInfos", isClearMode);
            if (list != null && list.length > 0) {
                for (var iCount = 0; iCount < list.length; iCount++) {
                    var item = list[iCount];
                    item.Focus = function () {
                        if (typeof (item.NativeHandle) == "number") {
                            rootElement.__DCWriterReference.invokeMethod(
                                "FocusElementByNativeHandle",
                                item.NativeHandle);
                        }
                    };
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentUserTrackInfos', startTime);

            return list;
        };

        /**
         * @name NavigateByUserTrackInfo
         * @type function
         * @apinameZh 定位痕迹列表信息
         * @classification vestige
         * @param ["handle","number","定位位置","","",true]
         * @returns ["result","Boolean","操作是否成功"]
         */
        rootElement.NavigateByUserTrackInfo = function (handle) {
            var startTime = new Date();
            var ReadViewMode = rootElement.ReadViewMode;//阅读模式
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            if (ReadViewMode || IsPrintPreview) {
                //阅读预览模式下禁止调用
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("NavigateByUserTrackInfo", handle);
            DCEventInterfaceLogFunction(rootElement, 'NavigateByUserTrackInfo', startTime);
            return result;
        };

        /**
         * @name GetTableElementById
         * @type function
         * @classification table
         * @apinameZh 获取表格信息
         * @param ["id","string","表格id","","",true]
         * @returns ["result","object","表格信息"]
         */
        rootElement.GetTableElementById = function (id) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读模式
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("GetTableElementById", id);
            DCEventInterfaceLogFunction(rootElement, 'GetTableElementById', startTime);
            return result;
        };

        /**
        * @name SetTableElementPoperties
        * @type function
        * @apinameZh 设置表格属性
        * @classification table
        * @param ["elementID","string","表格id","","",true]
        * @param ["parameter","object","属性","","",true]
        * @returns ["result","Boolean","操作是否成功"]
        */
        rootElement.SetTableElementPoperties = function (elementID, parameter) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读模式
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            let result = rootElement.SetElementProperties(elementID, parameter);
            DCEventInterfaceLogFunction(rootElement, 'SetTableElementPoperties', startTime);
            return result;
        };

        /**
        * @name SetTableRowElementPoperties
        * @type function
        * @apinameZh 设置表格行属性，建议使用SetElementProperties
        * @classification table
        * @param ["elementID","string","表格id","","",true]
        * @param ["rowIndex","number","行下标","","",true]
        * @param ["parameter","object","属性","","",true]
        * @returns ["result","Boolean","操作是否成功"]
        */
        rootElement.SetTableRowElementPoperties = function (elementID, rowIndex, parameter) {
            var startTime = new Date();
            let result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableRowElementPoperties", elementID, rowIndex, parameter);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetTableRowElementPoperties', startTime);
            return result;
        };

        /**
       * @name SetTableCellElementPoperties
       * @type function
       * @apinameZh 设置表格单元格属性
       * @classification table
       * @param ["elementID","string","表格id","","",true]
       * @param ["rowIndex","number","行下标","","",true]
       * @param ["columnIndex","number","列下标","","",true]
       * @param ["parameter","object","属性","","",true]
       * @returns ["result","Boolean","操作是否成功"]
       */
        rootElement.SetTableCellElementPoperties = function (elementID, rowIndex, columnIndex, parameter) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读模式
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("SetTableCellElementPoperties", elementID, rowIndex, columnIndex, parameter);
            DCEventInterfaceLogFunction(rootElement, 'SetTableCellElementPoperties', startTime);
            return result;
        };


        /**
       * @name getModified
       * @type function
       * @apinameZh 获取文档是否被修改
       * @classification file
       * @returns ["result","Boolean","操作是否成功"]
       * @change ["2023-05-24","增加与四代编辑器兼容的接口","wyc" ]
       */
        rootElement.getModified = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("getModified");
            DCEventInterfaceLogFunction(rootElement, 'getModified', startTime);
            return result;
        };

        /**
       * @name resetModified
       * @type function
       * @apinameZh 重置文档修改状态
       * @classification file
       * @param ["parameter","Boolean","文档的修改状态","","true或false",true]
       * @returns ["result","Boolean","操作是否成功"]
       * @change ["2023-05-24","增加与四代编辑器兼容的接口","wyc" ]
       */
        rootElement.resetModified = function (parameter) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("resetModified", parameter);
            DCEventInterfaceLogFunction(rootElement, 'resetModified', startTime);
            return result;
        };

        /**
       * @name ResetDocumentModified
       * @type function
       * @apinameZh 重置文档修改状态
       * @classification file
       * @returns ["result","Boolean","操作是否成功"]
       * @change ["2023-05-24","增加与四代编辑器兼容的接口","wyc" ]
       */
        rootElement.ResetDocumentModified = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("resetModified", false);
            DCEventInterfaceLogFunction(rootElement, 'ResetDocumentModified', startTime);
            return result;
        };

        /**
       * @name SetControlReadonly
       * @type function
       * @apinameZh 设置控件只读状态
       * @classification file
       * @param ["parameter","boolean","是否只读","","",true]
       * @returns ["result","Boolean","操作是否成功"]
       * @change ["2023-05-24","增加与四代编辑器兼容的接口","wyc" ]
       */
        rootElement.SetControlReadonly = function (parameter) {
            var startTime = new Date();
            let result = null;
            var isreadonly = null;
            if (parameter === "true" || parameter === true) {
                isreadonly = true;
            } else if (parameter === "false" || parameter === false) {
                isreadonly = false;
            }
            parameter = isreadonly;
            result = rootElement.__DCWriterReference.invokeMethod("setControlReadonly", parameter);
            DCEventInterfaceLogFunction(rootElement, 'SetControlReadonly', startTime);
            return result;
        };

        /**
         * @name InsertXmlString
         * @type function
         * @apinameZh 插入xml字符串
         * @classification file
         * @param ["parameter","string","要插入的文档内容xml字符串","","五代接口只接收一个参数其它的忽略",true]
         * @returns ["result","Boolean","操作是否成功"]
         * @change ["2023-05-24","增加与四代编辑器兼容的接口","wyc" ]
        */
        rootElement.InsertXmlString = function (parameter) {
            var startTime = new Date();
            let result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;//只读
            //阅读、预览、续打、区域选择，只读都不能修改dom
            if (IsOperateDOM && (readonly === false)) {
                var formView = rootElement.FormView();
                var CurrentElement = rootElement.CurrentElement();
                // 表单模式下仅允许给输入域赋值
                if (formView !== 'Disable') {
                    if (CurrentElement) {
                        var properties = rootElement.GetElementProperties(rootElement.CurrentElement());
                        if (properties.TypeName && properties.TypeName === "XTextInputFieldElement") {
                            result = rootElement.__DCWriterReference.invokeMethod("insertXmlString", parameter);
                        }
                    }
                    return result;
                }
                result = rootElement.__DCWriterReference.invokeMethod("insertXmlString", parameter);
            }
            DCEventInterfaceLogFunction(rootElement, 'InsertXmlString', startTime);
            return result;
        };

        /**
         * @name InsertXmlBase64String
         * @type function
         * @apinameZh 兼容四代的接口插入XML
         * @classification file
         * @param ["parameter","string","要插入的文档内容xml字符串","","五代接口只接收一个参数其它的忽略",true]
         * @returns ["result","Boolean","操作是否成功"]
         * @change ["2023-05-24","增加与四代编辑器兼容的接口","wyc" ]
         */
        rootElement.InsertXmlBase64String = function (parameter) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;//只读
            var result = false;
            //阅读、预览、续打、区域选择，只读都不能插入XML
            if (IsOperateDOM && (readonly === false)) {
                var formView = rootElement.FormView();
                var CurrentElement = rootElement.CurrentElement();
                // 表单模式下仅允许给输入域赋值
                if (formView !== 'Disable') {
                    if (CurrentElement) {
                        var properties = rootElement.GetElementProperties(rootElement.CurrentElement());
                        if (properties.TypeName && properties.TypeName === "XTextInputFieldElement") {
                            result = rootElement.__DCWriterReference.invokeMethod("InsertXmlBase64String", parameter);
                        }
                    }
                    return result;
                }
                result = rootElement.__DCWriterReference.invokeMethod("InsertXmlBase64String", parameter);
            }
            DCEventInterfaceLogFunction(rootElement, 'InsertXmlBase64String', startTime);
            return result;
        };

        /**
         * @name InsertXmlById
         * @type function
         * @classification file
         * @apinameZh 对指定ID插入XML文档片断
         * @param ["obj","object","要插入的文档内容xml片段属性","","{ file: arr,//xml文档 format: xml,//xml 文档格式base64: false,//是否是base64字符串logundo: false, //是否记录撤销操作position:{start:在目标容器的头部插入，end:在目标容器的尾部的段落符号前插入,append:在目标容器元素的尾部后追加（因为末尾元素通常是段落符号，追加效果一般显示为另起一行）}}",true]
         * @param ["id","string","要插入的容器元素的ID","","",true]
         * @returns ["result","Boolean","操作是否成功"]
         * @change ["2023-05-25","增加与四代编辑器兼容的接口","wyc" ]
         * @change ["2024-10-24","position修改end新增append","wyc" ]
         */
        rootElement.InsertXmlById = function (obj, id) {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement); //阅读、预览、续打、区域选择时返回false
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("InsertXmlById", obj, id);
            }
            DCEventInterfaceLogFunction(rootElement, 'InsertXmlById', startTime);
            return result;
        };


        /**
         * @name SaveToOFD
         * @type function
         * @classification file
         * @apinameZh 对指定文档保存成ofd格式
         * @param ["options","object","函数参数","","{ file: arr,//xml文档 format: xml,//xml 文档格式base64: false,//是否是base64字符串logundo: false, //是否记录撤销操作position:{start:在目标容器的头部插入，end:在目标容器的尾部的段落符号前插入,append:在目标容器元素的尾部后追加（因为末尾元素通常是段落符号，追加效果一般显示为另起一行）}}",true]
         * @param ["callback","function","回调函数","","",null]
         * @returns ["result","string","保存的OFD字符串"]
         * @change ["2024-10-30","新增保存夹带参数的OFD字符串DUWRITER5_0-3780","wyc"]
         */
        rootElement.SaveToOFD = function (options, callBack) {
            var startTime = new Date();
            var strFileName = (options && options.filename != null) ? options.filename : startTime.toString();
            var documentxml = rootElement.__DCWriterReference.invokeMethod("PrepareXmlForSavingToOFD", options);
            if (documentxml == null) {
                return false;
            }
            var result = WriterControl_Print.SaveLocalPDF(
                {
                    RootElement: rootElement,
                    DocumentsXml: documentxml,
                    FileName: strFileName + ".ofd",
                    CallBack: callBack,
                    ForOFD: true
                }
            );
            DCEventInterfaceLogFunction(rootElement, 'SaveToOFD', startTime);
            return result;
        };

        /**
         * @name LoadDocumentFromMixString
         * @type function
         * @classification file
         * @apinameZh 分别提供页眉，文档体，页脚的XML来组合成一个文档进行加载
         * @param ["options","object","合成的文档属性","","",true]
         * @change ["2023-05-26","增加与四代编辑器兼容的接口","wyc" ]
         */
        rootElement.LoadDocumentFromMixString = function (options) {
            rootElement.CheckDisposed();
            var startTime = new Date();

            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                var tick = new Date().valueOf();
                // 删除所有的页面图形元素
                var cnode = rootElement.firstChild;
                while (cnode != null) {
                    if (cnode.nodeName == "CANVAS" && cnode.getAttribute("dctype") == "page") {
                        var tempNode = cnode;
                        cnode = cnode.nextSibling;
                        rootElement.removeChild(tempNode);
                    }
                    else {
                        cnode = cnode.nextSibling;
                    }
                }
                rootElement.__WaterMarkData = null;
                WriterControl_Print.ClosePrintPreview(rootElement, false);
                rootElement.__LastLoadDocumentTime = new Date().valueOf();
                var result = null;
                result = rootElement.__DCWriterReference.invokeMethod("loadDocumentFromMixString", options);
                rootElement.TempElementForDoubleBuffer = null;
                WriterControl_Rule.InvalidateView(rootElement, "hrule");
                WriterControl_Rule.InvalidateView(rootElement, "vrule");
                WriterControl_Paint.InvalidateAllView(rootElement);
                tick = new Date().valueOf() - tick;
                WriterControl_Paint.UpdateViewForWaterMark(rootElement);
                DCEventInterfaceLogFunction(rootElement, 'LoadDocumentFromMixString', startTime);
            }
            return false;
        };

        /**
         * @name getHtmlByXMLBase64String
         * @type function
         * @classification file
         * @apinameZh 给定文档XML的BASE64字符串转换成HTML
         * @param ["base64string","string","加载的base64字符串","","",true]
         * @change ["2023-05-26","增加与四代编辑器兼容的接口","wyc" ]
         * @change ["2024-11-4","合并简化前端接口","wyc" ]
         */
        rootElement.getHtmlByXMLBase64String = function (base64string, callBack) {
            var startTime = new Date();
            if (typeof (base64string) !== "string") {
                return null;
            }
            var options = {
                Files: [base64string],
                base64: true
            };
            var result = rootElement.getHtmlByFiles(options, callBack);
            //let result = rootElement.__DCWriterReference.invokeMethod("getHtmlByXMLBase64String", base64string);
            DCEventInterfaceLogFunction(rootElement, 'getHtmlByXMLBase64String', startTime);
            return result;
        };

        /**
         * @name getHtmlByXMLString
         * @type function
         * @classification file
         * @apinameZh 给定文档XML字符串转换成HTML
         * @param ["XMLstring","string","加载的XML字符串","","",true]
         * @change ["2024-11-4","合并简化前端接口","wyc" ]
         * @returns ["result","string","文档的HTML"]
         */
        rootElement.getHtmlByXMLString = function (xmlstring, callBack) {
            var startTime = new Date();
            if (typeof (xmlstring) !== "string") {
                return null;
            }
            var options = {
                Files: [xmlstring],
                base64: false
            };
            var result = rootElement.getHtmlByFiles(options, callBack);
            //let result = rootElement.__DCWriterReference.invokeMethod("getHtmlByXMLString", xmlstring);
            DCEventInterfaceLogFunction(rootElement, 'getHtmlByXMLString', startTime);
            return result;
        };

        /**
         * @name GetElementsByTypeName
         * @type function
         * @classification structuralelement
         * @apinameZh 获取指定的元素类型列表
         * @param ["elementTypeName","string","元素类型名称","","",true]
         * @param ["specifyParent","string/int/obj","指定父元素的ID或nativehandle或.NET引用对象","","",true]
         * @returns ["result","string","文档的HTML"]
         * @describe 例如：querySelectorAll('*[dctype=XTextInputFieldElement]')、querySelectorAll('XTextInputFieldElement')
         */
        rootElement.GetElementsByTypeName = function (elementTypeName, specifyParent) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(specifyParent) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementsByTypeName2", elementTypeName, specifyParent);
            } else if (specifyParent) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementsByTypeName3", elementTypeName, specifyParent);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementsByTypeName", elementTypeName);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementsByTypeName', startTime);
            return result;
        };


        /**
         * @name getHtmlByFiles
         * @type function
         * @classification file
         * @apinameZh 给定若干XML作为子文档合并后转换成预览下的HTML
         * @param ["options","object","合并属性","","",true]
         * @returns ["result","string","文档的HTML"]
         * @change ["2023-05-29","增加与四代编辑器兼容的接口","wyc" ]
         * @change ["2024-04-24","新增回调函数参数","wyc" ]
        */
        rootElement.getHtmlByFiles = function (options, callBack) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("getHtmlByFiles", options);
            WriterControl_Paint.ApplyBitmapContentHtmlSrc(result, function (strResultHtml) {
                if (typeof (callBack) == "function") {
                    callBack.call(rootElement, strResultHtml);
                }
            });
            DCEventInterfaceLogFunction(rootElement, 'getHtmlByFiles', startTime);
            return result;
        };


        /**
        * @name GetPrintPreviewHTMLByMixedFiles
        * @type function
        * @classification file
        * @apinameZh 打印预览的html
        * @param ["options","object","属性","","",true]
        * @returns ["result","string","文档的HTML"]
        * @change ["2023-06-01","处理GetPrintPreviewHTMLByMixedFiles","zhangbin" ]
        * @change ["2024-06-03","添加参数支持合并前分别绑定数据","wyc"]
       */
        //  options: {
        //    Files: [
        //        [],
        //        [],
        //        []
        //    ],
        //    BindingDataXMLs: [],
        //    UseBase64: "false"
        //    WaterMark: null
        //} 
        rootElement.GetPrintPreviewHTMLByMixedFiles = function (options, callback) {
            ////先给默认值
            //var printOptions = {
            //    files: [],
            //    format: "xml",
            //    base64: options.UseBase64 ? options.UseBase64 : 'false',
            //    megedoc: "true",
            //    modefile: "",
            //    splitmode: "none"
            //}
            //if (options.Files && Array.isArray(options.Files)) {
            //    for (var file = 0; file < options.Files.length; file++) {
            //        //解析二维数组
            //        var innerFile = options.Files[file];
            //        if (innerFile && Array.isArray(innerFile) && innerFile.length > 0) {
            //            if (file == 0) {
            //                printOptions.modefile = innerFile[0];
            //            }
            //            for (var innerfile = 0; innerfile < innerFile.length; innerfile++) {
            //                printOptions.files.push(innerFile[innerfile]);
            //            }
            //        }
            //    }
            //}

            //wyc20230701:直接转发
            var startTime = new Date();
            let result = rootElement.GetPrintPreviewHTML(options, callback);
            DCEventInterfaceLogFunction(rootElement, 'GetPrintPreviewHTMLByMixedFiles', startTime);
            return result;
        };


        /**
       * @name PrintPreviewByHtml
       * @type function
       * @apinameZh 处理打印的html
       * @classification print
       * @param ["printHtml","string","打印的html","","",true]
       */
        rootElement.PrintPreviewByHtml = function (printHtml) {
            var startTime = new Date();
            WriterControl_Print.PrintPreview(rootElement, undefined, printHtml);
            DCEventInterfaceLogFunction(rootElement, 'PrintPreviewByHtml', startTime);
        };

        /**
         * @name GetCurrentElementTypeName
         * @type function
         * @classification structuralelement
         * @apinameZh 获取当前选择的元素类型名称，为属性对话框准备接口
         * @returns ["result","string","文档的元素类型名称"]
         */
        rootElement.GetCurrentElementTypeName = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("GetCurrentElementTypeName");
            DCEventInterfaceLogFunction(rootElement, 'GetCurrentElementTypeName', startTime);
            return result;
        };

        /**
        * @name GetBehaviorOptions
        * @type function
        * @apinameZh 获取文档行为选项
        * @classification file
        * @returns ["result","string","选项"]
        */
        rootElement.GetBehaviorOptions = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetBehaviorOptions");
            DCEventInterfaceLogFunction(rootElement, 'GetBehaviorOptions', startTime);
            return result;
        };

        /**
        * @name GetEditOptions
        * @type function
        * @apinameZh 获取文档编辑选项
        * @classification file
        * @returns ["result","object","选项"]
        */
        rootElement.GetEditOptions = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetEditOptions");
            DCEventInterfaceLogFunction(rootElement, 'GetEditOptions', startTime);
            return result;
        };

        /**
        * @name GetSecurityOptions
        * @type function
        * @apinameZh 获取文档安全选项
        * @classification file
        * @returns ["result","object","选项"]
        */
        rootElement.GetSecurityOptions = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetSecurityOptions");
            DCEventInterfaceLogFunction(rootElement, 'GetSecurityOptions', startTime);
            return result;
        };

        /**
        * @name GetViewOptions
        * @type function
        * @apinameZh 获取文档视图选项
        * @classification file
        * @returns ["result","object","选项"]
        */
        rootElement.GetViewOptions = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetViewOptions");
            DCEventInterfaceLogFunction(rootElement, 'GetViewOptions', startTime);
            return result;
        };

        /**
        * @name Search
        * @type function
        * @apinameZh 添加查找接口
        * @classification file
        * @param ["options","object","查找属性","","",true]
        * @returns ["result","number","返回次数"]
        */
        // options: {
        //     "searchstring": string,//要查找的字符串
        //         "enablereplacestring": "false",//是否启用替换
        //             "replacestring": "false",//要替换的字符串
        //                 "backward": "true"//是否向后替换
        //     "ignorecase": "true"//是否区分大小写
        //     "logundo": "true"//记录撤销操作信息
        //     "excludebackgroundtext": "true"//忽略掉背景文字
        //     "SearchID": "true"//是否限制为查询元素编号
        // }
        rootElement.Search = function (options) {
            var startTime = new Date();
            var result = false;
            //var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            //if (IsOperateDOM) {
            result = rootElement.__DCWriterReference.invokeMethod("Search", options);
            DCEventInterfaceLogFunction(rootElement, 'Search', startTime);
            //}
            return result;
        };

        /**
         * @name Reaplace
         * @type function
         * @apinameZh 添加查找接口
         * @classification file
         * @param ["options","object","查找替换的属性","","",true]
         * @returns ["result","number","返回次数"]
         */
        // options: {
        //     "searchstring": string,//要查找的字符串
        //         "enablereplacestring": "false",//是否启用替换
        //             "replacestring": "false",//要替换的字符串
        //                 "backward": "true"//是否向后替换
        //     "ignorecase": "true"//是否区分大小写
        //     "logundo": "true"//记录撤销操作信息
        //     "excludebackgroundtext": "true"//忽略掉背景文字
        //     "SearchID": "true"//是否限制为查询元素编号
        // }
        rootElement.Reaplace = function (options) {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("Reaplace", options);
                DCEventInterfaceLogFunction(rootElement, 'Reaplace', startTime);
            }
            return result;
        };

        /**
        * @name ReplaceAll
        * @type function
        * @apinameZh 添加替换所有接口
        * @classification file
        * @param ["options","object","替换所有的属性","","",true]
        * @returns ["result","number","返回次数"]
        */
        // options: {
        //     "searchstring": string,//要查找的字符串
        //         "enablereplacestring": "false",//是否启用替换
        //             "replacestring": "false",//要替换的字符串
        //                 "backward": "true"//是否向后替换
        //     "ignorecase": "true"//是否区分大小写
        //     "logundo": "true"//记录撤销操作信息
        //     "excludebackgroundtext": "true"//忽略掉背景文字
        //     "SearchID": "true"//是否限制为查询元素编号
        // }
        rootElement.ReplaceAll = function (options) {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("ReplaceAll", options);
                DCEventInterfaceLogFunction(rootElement, 'ReplaceAll', startTime);
            }
            return result;
        };


        /**
        * @name SearchAndReplaceDialog
        * @type function
        * @apinameZh 弹出查找&替换设置对话框
        * @classification file
        * @param ["options","object","查找替换属性","","",true]
        * @returns ["result","number","返回次数"]
        */
        rootElement.SearchAndReplaceDialog = function (options) {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM) {
                result = WriterControl_Dialog.SearchAndReplaceDialog(options, rootElement);
                DCEventInterfaceLogFunction(rootElement, 'SearchAndReplaceDialog', startTime);
            }
            return result;
        };

        /**
         * @name ReportExceptionDialog
         * @type function
         * @classification file
         * @apinameZh 查看错误信息弹框
         */
        rootElement.ReportExceptionDialog = function () {
            var startTime = new Date();
            let result = WriterControl_Dialog.ReportExceptionDialog(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'ReportExceptionDialog', startTime);
            return result;
        };

        /**
         * @name ApplyDocumentOptions
         * @type function
         * @apinameZh 应用文档选项
         * @classification file
         * @change ["2023-06-01","增加与四代编辑器兼容的接口","wyc" ]
         * @change ["2023-11-27","修改ApplyDocumentOptions的C#接口","yyf" ]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.ApplyDocumentOptions = function () {
            var startTime = new Date();
            var obj = rootElement.__DCWriterReference.invokeMethod("ApplyDocumentOptions", rootElement.DocumentOptions);;
            if (obj === true) {

                WriterControl_Paint.InvalidateAllView(rootElement);
                //console.log(rootElement.DocumentOptions);

                rootElement.refreshDocumentOptions();
            }
            //rootElement.DocumentOptions = obj;
            DCEventInterfaceLogFunction(rootElement, 'ApplyDocumentOptions', startTime);
            return obj;
        };

        /**
         * @name refreshDocumentOptions
         * @type function
         * @classification file
         * @apinameZh 初始化前端JS结构中的文档选项
         * @change ["2023-06-02","初始化前端JS结构中的文档选项","wyc" ]
         * @change ["2023-11-27","修改GetDocumentOptions接口","yyf" ]
         */
        rootElement.refreshDocumentOptions = function () {
            var startTime = new Date();
            var options = rootElement.__DCWriterReference.invokeMethod("GetDocumentOptions");
            rootElement.DocumentOptions = options;
            DCEventInterfaceLogFunction(rootElement, 'refreshDocumentOptions', startTime);
        };

        /**
         * @name SetFieldDropListItemByValue
         * @type function
         * @apinameZh 兼容四代，指定下拉列表的默认选择项
         * @classification structuralelement
         * @change ["2023-06-02","初始化前端JS结构中的文档选项","wyc" ]
         * @param ["id","string","元素id","","",true]
         * @param ["valuestring","string","值","","",true]
         */
        rootElement.SetFieldDropListItemByValue = function (id, valuestring) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = false;

            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetFieldDropListItemByValue2", id, valuestring);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetFieldDropListItemByValue", id, valuestring);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetFieldDropListItemByValue', startTime);
            return result;
        };

        /**
         * @name FormView
         * @type function
         * @classification file
         * @apinameZh 获取当前的表单模式类型
         */
        rootElement.FormView = function () {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//阅读、预览、续打、区域选择时，不能修改dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("FormView");
            }
            DCEventInterfaceLogFunction(rootElement, 'FormView', startTime);
            return result;
        };

        /**
        * @name ProtectType
        * @type function
        * @apinameZh 当前内容保护状态
        * @classification file
        * @returns ["result","string","内容保护状态"]
        */
        rootElement.ProtectType = function () {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//阅读、预览、续打、区域选择时，不能修改dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("ProtectType");
            }
            DCEventInterfaceLogFunction(rootElement, 'ProtectType', startTime);
            return result;
        };

        /**
        * @name SetElementCheckedByID
        * @type function
        * @classification structuralelement
        * @apinameZh 设置指定编号的单选框或复选框是否选中
        * @param ["id","string","元素id","","",true]
        * @param ["isChecked","boolean","是否选中","","",true]
        * @param ["options","object","痕迹信息","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetElementCheckedByID = function (id, isChecked) {
            var startTime = new Date();
            let result = false;
            var currentElement = rootElement.GetElementProperties(id);
            //元素的可用属性为ture下，可以修改元素的勾选状态
            if (currentElement && currentElement.Enabled) {
                var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//获取当前是否可以修改dom
                if (IsOperateDOM) {
                    result = rootElement.__DCWriterReference.invokeMethod("SetElementCheckedByID", id, isChecked);
                    DCEventInterfaceLogFunction(rootElement, 'SetElementCheckedByID', startTime);
                }
            }

            return result;
        };


        /**
        * @name SetElementCustomAttributes
        * @type function
        * @classification attribute
        * @apinameZh 设置指定元素的自定义属性
        * @param ["ELEMENT","any","元素id","","指定元素的后台引用对象，也可以是元素ID字符串，也可以元素属性对象",true]
        * @param ["options","object","表示为键值对象形式的JSON对象","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-06-13","兼容四代接口","wyc" ]
        */
        rootElement.SetElementCustomAttributes = function (element, options) {
            var startTime = new Date();
            let result = null;
            //wyc20231102:转发接口
            //if (typeof (element) === "object" && typeof (element.serializeAsArg) === "function") {
            //    result = rootElement.__DCWriterReference.invokeMethod("SetElementCustomAttributes2", element, options);
            //} else {
            //    result = rootElement.__DCWriterReference.invokeMethod("SetElementCustomAttributes", element, options);
            //}
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var opt = {
                Attributes: options
            };
            result = rootElement.SetElementProperties(element, opt);
            DCEventInterfaceLogFunction(rootElement, 'SetElementCustomAttributes', startTime);
            return result;
        };

        /**
        * @name GetElementCustomAttributes
        * @type function
        * @classification attribute
        * @apinameZh 获取指定元素的自定义属性
        * @param ["ELEMENT","any","元素id","","指定元素的后台引用对象，也可以是元素ID字符串，也可以元素属性对象",true]
        * @returns ["result","object","自定义属性"]
        * @change ["2023-06-13","兼容四代接口","wyc" ]
        */
        rootElement.GetElementCustomAttributes = function (element) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = null;
            //wyc20231102:转发接口
            //if (typeof (element) === "object" && typeof (element.serializeAsArg) === "function") {
            //    result = rootElement.__DCWriterReference.invokeMethod("GetElementCustomAttributes2", element);
            //} else {
            //    result = rootElement.__DCWriterReference.invokeMethod("GetElementCustomAttributes", element);
            //}           
            var obj = rootElement.GetElementProperties(element);
            if (obj != null) {
                result = obj.Attributes;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementCustomAttributes', startTime);
            return result;
        };

        /**
       * @name SetElementInnerValueStringByID
       * @type function
       * @apinameZh 兼容四代设置元素InnerValue文本
       * @classification structuralelement
       * @param ["id","any","元素id","","元素ID字符串",true]
       * @param ["newValue","string","新的值","","",true]
       * @param ["newText","string","新的文本","","",true]
       * @returns ["result","boolean","操作是否成功"]
       */
        rootElement.SetElementInnerValueStringByID = function (id, newValue, newText) {
            var startTime = new Date();
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementInnerValueStringByID2", id, newValue, newText);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementInnerValueStringByID", id, newValue, newText);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetElementInnerValueStringByID', startTime);
            return result;
        };

        /**
       * @name GetElementInnerValueStringByID
       * @type function
       * @classification structuralelement
       * @apinameZh 兼容四代获取元素InnerValue文本
       * @param ["id","any","元素id","","元素ID字符串",true]
       * @returns ["result","string","元素InnerValue文本"]
       */
        rootElement.GetElementInnerValueStringByID = function (id) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetElementInnerValueStringByID", id);
            DCEventInterfaceLogFunction(rootElement, 'GetElementInnerValueStringByID', startTime);
            return result;
        };

        /**
        * @name GetCurrentParagraphStyle
        * @type function
        * @apinameZh 获取当前段落的样式信息
        * @classification file
        * @returns ["result","string","段落的样式信息"]
        */
        rootElement.GetCurrentParagraphStyle = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetCurrentParagraphStyle");
            DCEventInterfaceLogFunction(rootElement, 'GetCurrentParagraphStyle', startTime);
            return result;
        };

        /**
        * @name SetCurrentParagraphStyle
        * @type function
        * @apinameZh 设置当前段落的样式信息
        * @classification file
        * @param ["parameter","object","样式对象","","样式对象",true]
        * @returns ["result","boolean","操作是否成功"]
        * @describe 段落样式，不解析字体格式
        */
        rootElement.SetCurrentParagraphStyle = function (parameter) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetCurrentParagraphStyle", parameter);
            DCEventInterfaceLogFunction(rootElement, 'SetCurrentParagraphStyle', startTime);
            return result;
        };

        /**
         * @name GetTableAttribute
         * @type function
         * @apinameZh 兼容四代获取表格的自定义属性
         * @classification table
         * @param ["tableId","string","表格id","","",true]
         * @returns ["result","string","指定编号的病程"]
         */
        rootElement.GetTableAttribute = function (table) {
            var startTime = new Date();
            let result = null;
            //wyc20230710:更新写法
            var obj = rootElement.GetElementProperties(table);
            if (typeof (obj) === "object") {
                result = obj.Attributes;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetTableAttribute', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("GetElementCustomAttributes", table);
        };

        /**
        * @name SetTableAttribute
        * @type function
        * @apinameZh 兼容四代设置表格的自定义属性
        * @classification table
        * @param ["tableId","string","表格id","","",true]
        * @param ["option","object","自定义对象","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetTableAttribute = function (table, option) {
            //wyc20230710:直接转发请求
            var startTime = new Date();
            let result = rootElement.SetElementCustomAttributes(table, option);
            DCEventInterfaceLogFunction(rootElement, 'SetTableAttribute', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("SetElementCustomAttributes", table, option);
        };

        /**
       * @name GetTableRowAttribute
       * @type function
       * @apinameZh 兼容四代获取表格行的自定义属性
       * @classification table
       * @param ["row","object","表格行对象ctl.CurrentTableRow()","","",true]
       * @returns ["result","object","表格行自定义属性"]
       * @change ["2024-04-28","增加一个是否为表格行的判断","李新宇" ]
       */
        rootElement.GetTableRowAttribute = function (row) {
            var startTime = new Date();
            let result = null;
            //wyc20230710:更新写法
            var obj = rootElement.GetElementProperties(row);
            if (obj && obj.TypeName && obj.TypeName === "XTextTableRowElement") {
                result = obj.Attributes ? obj.Attributes : null;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetTableRowAttribute', startTime);
            return result;
        };

        /**
         * @name SetTableRowAttribute
         * @type function
         * @apinameZh 兼容四代设置表格行的自定义属性
         * @classification table
         * @param ["row","object","表格行对象","","",true]
         * @param ["option","object","自定义对象","","",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2024-04-28","增加一个是否为表格行的判断","李新宇" ]
         */
        rootElement.SetTableRowAttribute = function (row, option) {
            //wyc20230710:直接转发请求
            var startTime = new Date();
            let result = null;
            var obj = rootElement.GetElementProperties(row);
            if (obj && obj.TypeName && obj.TypeName === "XTextTableRowElement") {
                result = rootElement.SetElementCustomAttributes(row, option);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetTableRowAttribute', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("SetTableRowAttribute", row, option);
        };

        /**
        * @name GetTableCellAttribute
        * @type function
        * @apinameZh 兼容四代获取单元格的自定义属性
        * @classification table
        * @param ["cell","object","单元格对象ctl.CurrentTableCell()","","",true]
        * @returns ["result","object","自定义属性"]
        * @change ["2024-04-28","增加一个是否为单元格的判断","李新宇" ]
        */
        rootElement.GetTableCellAttribute = function (cell) {
            var startTime = new Date();
            let result = null;
            var obj = rootElement.GetElementProperties(cell);
            if (obj && obj.TypeName && obj.TypeName === "XTextTableCellElement") {
                result = obj.Attributes ? obj.Attributes : null;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetTableCellAttribute', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("GetTableCellAttribute", cell);
        };

        /**
       * @name SetTableCellAttribute
       * @type function
       * @apinameZh 兼容四代设置单元格的自定义属性
       * @classification table
       * @param ["cell","object","单元格对象ctl.CurrentTableCell()","","",true]
       * @param ["option","object","自定义对象","","",true]
       * @returns ["result","boolean","操作是否成功"]
       * @change ["2024-04-28","增加一个是否为单元格的判断","李新宇" ]
       */
        rootElement.SetTableCellAttribute = function (cell, option) {
            //wyc20230710:直接转发请求
            var startTime = new Date();
            let result = null;
            var obj = rootElement.GetElementProperties(cell);
            if (obj && obj.TypeName && obj.TypeName === "XTextTableCellElement") {
                result = rootElement.SetElementCustomAttributes(cell, option);
            }

            DCEventInterfaceLogFunction(rootElement, 'SetTableCellAttribute', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("SetTableCellAttribute", cell, option);
        };

        /**
      * @name SetElementContentReadonly
      * @type function
      * @apinameZh 设置元素是否为只读
      * @classification structuralelement
      * @param ["element","object","元素对象","","",true]
      * @param ["breadonly","object","是否只读","","True/False/Inherit",true]
      * @returns ["result","boolean","操作是否成功"]
      */
        rootElement.SetElementContentReadonly = function (element, breadonly) {
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            if (breadonly == true || (typeof breadonly === 'String' && breadonly.toLocaleLowerCase() == 'true')) {
                breadonly = 'True';
            } else if (breadonly == false || (typeof breadonly === 'String' && breadonly.toLocaleLowerCase() == 'false')) {
                breadonly = 'False';
            } else if (breadonly.toLocaleLowerCase() == 'inherit') {
                breadonly = 'Inherit';
            }
            //wyc20230727:直接转发需求
            var opt = {
                ContentReadonly: breadonly
            };
            var startTime = new Date();
            let result = rootElement.SetElementProperties(element, opt);
            DCEventInterfaceLogFunction(rootElement, 'SetElementContentReadonly', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("SetElementContentReadonly", element, breadonly);
        };

        /**
         * @name SetElementVisibility
         * @type function
         * @apinameZh 设置元素是否展示
         * @classification structuralelement
         * @param ["element","object","元素对象","","",true]
         * @param ["visible","object","是否隐藏","","true/false布尔值或者字符串的布尔值",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SetElementVisibility = function (element, visible) {
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }

            //wyc20230727:直接转发需求
            var opt = {
                Visible: visible
            };
            var startTime = new Date();
            let result = rootElement.SetElementProperties(element, opt);
            DCEventInterfaceLogFunction(rootElement, 'SetElementVisibility', startTime);
            return result;
            //return rootElement.__DCWriterReference.invokeMethod("SetElementVisibility", element, visible);
        };

        /**
       * @name removeSpecifywidth
       * @type function
       * @classification structuralelement
       * @apinameZh 兼容四代清除输入域的宽度，恢复初始值,大小写和四代一致
       * @param ["element","object","元素对象","","",true]
       * @returns ["result","boolean","操作是否成功"]
       */
        rootElement.removeSpecifywidth = function (element) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("RemoveSpecifywidth", element);
            DCEventInterfaceLogFunction(rootElement, 'removeSpecifywidth', startTime);
            return result;
        };

        /**
      * @name GetAllCheckboxOrRadio
      * @type function
      * @classification structuralelement
      * @apinameZh 兼容四代接口，获取全部或指定名称的单选框或复选框
      * @param ["type","string","类型名称：radio或checkbox","","",true]
      * @param ["name","string","指定的元素的name属性（可传空，表示获取所有）","","",true]
      * @returns ["result","boolean","操作是否成功"]
      */
        rootElement.GetAllCheckboxOrRadio = function (type, name) {
            var startTime = new Date();
            let result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//阅读、预览、续打、区域选择时，不能修改dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("GetAllCheckboxOrRadio", type, name);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetAllCheckboxOrRadio', startTime);
            return result;
        };

        /**
        * @name CASignature
        * @type function
        * @apinameZh 设置签名
        * @classification file
        * @param ["options","object","签名属性","","",true]
        * @param ["callback","function","回调函数","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-06-13","兼容四代接口","wyc" ]
        */
        // options {
        // "id": "img1",
        // "base64Img": picturebase64string,
        // "scope": "InputField",
        // "userID": "zhangsan",
        // "userName": "zhangsan",
        // "imageInFrontOfText": "false",
        // "clientName": "(local)",
        // "width": "80",
        // "height": "30",
        // "providerName": "1" //1:软签 2：诺安签名
        // "specifycontainer": null //为签名图片插入的父容器的id或nativehandle
        // }
        rootElement.CASignature = function (options, callback) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读下不允许修改
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("CASignature", options);
            if (typeof (callback) == "function") {
                callback.call(rootElement, result);
            }
            DCEventInterfaceLogFunction(rootElement, 'CASignature', startTime);
            return result;
        };


        /**
         * @name CAReSignature
         * @type function
         * @apinameZh 对元素进行签名
         * @classification structuralelement
         * @param ["options","object","元素id","","例：{id: img1 }",true]
         * @param ["callback","function","回调函数","","",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2023-07-03","兼容四代接口","wyc" ]
         */
        rootElement.CAReSignature = function (options, callback) {
            var startTime = new Date();
            if (typeof (options) === "object") {
                options["ReSign"] = true;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("CASignature", options);
            if (typeof (callback) == "function") {
                callback.call(rootElement, result);
            }
            DCEventInterfaceLogFunction(rootElement, 'CAReSignature', startTime);
            return result;
        };

        /**
       * @name GetPDFByFiles
       * @type function
       * @apinameZh 获取pdf文件
       * @classification file
       * @param ["options","object","自定义属性","","",true]
       * @returns ["result","boolean","操作是否成功"]
       * @change ["2023-06-19","兼容四代接口","wyc" ]
       * @change ["2024-05-22","修复DownloadFile模式下下载的PDF无法打开问题","xym" ]
       */
        rootElement.GetPDFByFiles = function (options, callBack) {
            var startTime = new Date();
            if (options != null && options.localmode === true || options.localmode === "true") {
                return rootElement.GetPDFByFiles2 && rootElement.GetPDFByFiles2(options, callBack);
            }
            //wyc20230625:重写逻辑准备调用第四代接口
            var asycinvoke = false;
            var downloadstr = "false";
            var forceblack = "false";
            if (options != null && options.resulttype === "DownloadFile") {
                //asycinvoke = false;
                downloadstr = "true";
            }
            if (options != null && options.forceblacktextcolor === true || options.forceblacktextcolor === "true") {
                forceblack = "true";
            }
            var strServicePageUrl = DCTools20221228.GetServicePageUrl(rootElement);
            if (strServicePageUrl == null || strServicePageUrl.length == 0) {
                console.error("DCWriter:未配置ServicePageUrl,无法执行GetPDFByFiles.");
                return false;
            }
            // 此处对应的服务器代码在 DCWriterForASPNET\Writer\Controls\Web\WC_WASM.cs
            var strUrl = strServicePageUrl + "?wasm=getpdfbyfiles&dcbid2022=" + DCTools20221228.GetClientID() +
                "&forceblack=" + forceblack +
                "&downloadstr=" + downloadstr;

            var postData = rootElement.__DCWriterReference.invokeMethod("InnerGetPDFByFilesData", options);
            var xhr = new XMLHttpRequest();
            var resultstr = null;
            xhr.open("POST", strUrl, asycinvoke);
            //xhr.responseType = "blob";
            xhr.onload = async function () {
                if (this.status == 200) {
                    resultstr = this.response;
                    if (options == null || options.resulttype !== "DownloadFile") {
                        if (typeof (callBack) == "function") {
                            callBack.call(rootElement, resultstr);
                        }
                    } else {
                        // 添加GetPDFByFiles方法可以自定义下载PDF名称的功能【DUWRITER5_0-4082】
                        var d = new Date();
                        var strFileName = d.getFullYear() + "" + ((d.getMonth() + 1) >= 10 ? (d.getMonth() + 1) : '0' + (d.getMonth() + 1)) + "" + d.getDate() + "" + d.getHours() + "" + d.getMinutes() + "" + d.getSeconds();
                        if (options && typeof options.filename === "string" && options.filename.length > 0) {
                            strFileName = options.filename;
                        }
                        //var a = document.createElement('a');
                        //a.style.display = 'none';
                        //a.href = window.URL.createObjectURL(resultstr);
                        //a.download = strFileName
                        //// 将a标签添加到body中是为了更好的兼容性，谷歌浏览器可以不用添加
                        //document.body.appendChild(a);
                        //a.click();
                        //// 移除
                        //document.body.removeChild(a);
                        //// 释放url
                        //window.URL.revokeObjectURL(a.href);
                        // if (resultstr.indexOf("data:application/pdf;base64,") != 0) {
                        //     resultstr = "data:application/pdf;base64," + resultstr;
                        // }
                        // 将BASE64字符串转换为一个字节数组
                        var PDFArrayBuffer = null;
                        try {
                            PDFArrayBuffer = DCTools20221228.FromBase64String(resultstr);
                        }
                        catch {
                            PDFArrayBuffer = resultstr;
                        }
                        DCTools20221228.DownloadAsFile(PDFArrayBuffer, "application/pdf", strFileName + '.pdf');
                        resultstr = true;
                    }
                }
            };
            xhr.send(postData);
            DCEventInterfaceLogFunction(rootElement, 'GetPDFByFiles', startTime);
            return resultstr;
        };

        /**
        * @name GetPDFByFiles2
        * @type function
        * @apinameZh 获取pdf文件
        * @classification file
        * @param ["options","object","自定义属性","","",true]
        * @param ["callBack","function","回调函数，如果返回的是BASE64则必须指定","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-06-19","兼容四代接口","wyc" ]
        * @change ["2024-05-22","支持options参数中returntype或者resulttype，有一个属性等于'DownloadFile'时下载PDF文件，否则返回BASE64","xym" ]
        * @change ["2024-11-4","新增支持数据源绑定和夹带元素坐标信息返回","wyc" ]
        */
        rootElement.GetPDFByFiles2 = function (options, callBack) {
            var startTime = new Date();
            /** 是否需要下载PDF文件 */
            var ISDownLoadFile = options && (options.returntype == "DownloadFile" || options.resulttype == "DownloadFile");
            if (ISDownLoadFile == false && typeof (callBack) !== "function") {
                console.log("返回base64时必须指定回调函数");
                return false;
            }
            var resultobj = rootElement.__DCWriterReference.invokeMethod("InnerGetPDFByFilesData2", options);

            if (resultobj == null || resultobj.Files == null || Array.isArray(resultobj.Files) === false || resultobj.Files.length == 0) {
                return false;
            }
            var resultInfo = resultobj.Coordinates;
            var postData = resultobj.Files;
            var callBack2 = null;
            var fileName = options.filename;
            if (ISDownLoadFile == false) {
                callBack2 = function (str) {
                    DCconvertBinaryToBase64(str)
                        .then((base64String) => {
                            if (typeof (callBack) === "function") {
                                callBack(base64String);
                            }
                        })
                        .catch((error) => {
                            console.error(error);
                            resultstr = false;
                        });
                };
            }
            var forceblack = false;
            if (options != null && options.forceblacktextcolor === true || options.forceblacktextcolor === "true") {
                forceblack = true;
            }
            WriterControl_Print.SaveLocalPDF(
                {
                    RootElement: rootElement,
                    FileName: fileName + ".pdf",
                    CallBack: callBack2,
                    DocumentsXml: postData,
                    ForceBlack: forceblack
                });
            DCEventInterfaceLogFunction(rootElement, 'GetPDFByFiles', startTime);
            return resultInfo;
        };




        /**
        * @name SetLabelElementContactSettings
        * @type function
        * @classification structuralelement
        * @apinameZh 专门设置标签元素的字符连接模式属性的接口
        * @param ["element","object","元素","","需要设置的标签元素的ID、元素属性列表或后台.NET引用对象",true]
        * @param ["options","object","标签元素的字符连接属性","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-06-19","兼容四代接口","wyc" ]
        */
        // options: {
        // "ContactAction": "Normal"//连接模式 
        // "AttributeNameForContactAction": "科室",//连接字符串的来源自定义名称
        // "LinkTextForContactAction": "-",//各连接字符串的分隔符
        // }
        rootElement.SetLabelElementContactSettings = function (element, options) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读下不允许修改
            if (IsOperateDOM === false || readonly) {
                return false;
            }
            var result = rootElement.SetElementProperties(element, options);
            DCEventInterfaceLogFunction(rootElement, 'SetLabelElementContactSettings', startTime);
            return result;
        };

        /**
        * @name getModifiedElements
        * @type function
        * @classification structuralelement
        * @apinameZh 兼容四代获取改变的元素ID列表
        * @param ["typename","string","元素类型字符串","","",true]
        * @returns ["result","array","修改元素列表"]
        */
        rootElement.getModifiedElements = function (typename) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("GetModifiedElements", typename);
            DCEventInterfaceLogFunction(rootElement, 'getModifiedElements', startTime);
            return result;
        };

        /**
        * @name DeleteElement
        * @type function
        * @apinameZh 删除指定的元素
        * @classification structuralelement
        * @param ["ELEMENT","object","元素ID","","元素ID或元素本身的后台.NET引用对象",true]
        * @param ["isLogundo","Boolean","是否记录撤销,可以不传，默认为false(仅在第一个参数为id字符串时生效)","false","",true]
        * @returns ["result","array","修改元素列表"]
        * @change ["2023-07-24","新增接口","wyc" ]
        * @change ["2024-08-1","增加一个参数isLogundo,是否记录撤销","lixinyu" ]
        */
        rootElement.DeleteElement = function (parameter, isLogundo = false) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            if (IsOperateDOM === false) {
                return false;
            }
            let result = false;
            if (DCTools20221228.IsDotnetReferenceElement(parameter) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("DeleteElement", parameter);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("DeleteElementByID", parameter, isLogundo);
            }
            DCEventInterfaceLogFunction(rootElement, 'DeleteElement', startTime);
            return result;
        };

        /**
        * @name DeleteElementByID
        * @type function
        * @apinameZh 删除指定编号的元素
        * @classification structuralelement
        * @param ["parameter","string","id","","",true]
        * @param ["isTrackInfo","Boolean","是否留痕,可以不传，默认为false","","",true]
        * @param ["isLogundo","Boolean","是否记录撤销,可以不传，默认为false","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2024-04-30","增加一个参数isTrackInfo，是否留痕","lixinyu" ]
        * @change ["2024-08-1","增加一个参数isLogundo,是否记录撤销","lixinyu" ]
        */
        rootElement.DeleteElementByID = function (id, isTrackInfo = false, isLogundo = false) {
            var startTime = new Date();
            let result = false;
            if (isTrackInfo) {
                result = rootElement.DeleteElementByIDWithTrackInfos(id);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("DeleteElementByID", id, isLogundo);
            }
            DCEventInterfaceLogFunction(rootElement, 'DeleteElementByID', startTime);
            return result;
        };

        /**
         * @name DeleteElementByIDWithTrackInfos
         * @type function
         * @apinameZh 删除指定编号的元素，并带有痕迹
         * @classification vestige
         * @param ["parameter","string","id","","",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2024-04-30","增加接口","lixinyu" ]
         */
        rootElement.DeleteElementByIDWithTrackInfos = function (id) {
            var startTime = new Date();
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("DeleteElementByIDWithTrackInfos", id);
            DCEventInterfaceLogFunction(rootElement, 'DeleteElementByIDWithTrackInfos', startTime);
            return result;
        };

        /**
       * @name AutoFixTableWidth
       * @type function
       * @apinameZh 自动修复表格列宽使其自适应容器宽度
       * @classification table
       * @param ["tableElement","string","表格id","","为空则引用当前表格元素",false]
       * @param ["fixself","boolean","修正自身","","指定是否只修正表格自己",false]
       * @returns ["result","boolean","操作是否成功"]
       * @change ["2023-06-27","增加接口","wyc" ]
       */
        rootElement.AutoFixTableWidth = function (tableElement, fixself) {
            var startTime = new Date();
            let result = null;
            if (typeof (fixself) !== "boolean") {
                fixself = false;
            }
            if (typeof (tableElement) === "object" && typeof (tableElement.serializeAsArg) === "function") {
                result = rootElement.__DCWriterReference.invokeMethod("AutoFixTableWidth2", tableElement, fixself);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("AutoFixTableWidth", tableElement, fixself);
            }
            DCEventInterfaceLogFunction(rootElement, 'AutoFixTableWidth', startTime);
            return result;
        };

        /**
         * @name GetPrintResult
         * @type function
         * @classification print
         * @apinameZh 获取最后一次的打印信息
         * @returns ["result","any","最后一次的打印信息"]
         */
        rootElement.GetPrintResult = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetPrintResult");
            DCEventInterfaceLogFunction(rootElement, 'GetPrintResult', startTime);
            return result;
        };
        /**
         * @name GetLastPageInfo
         * @type function
         * @classification print
         * @apinameZh 获取最后一次的打印信息
         * @returns ["result","any","最后一次的打印信息"]
         */
        rootElement.GetLastPageInfo = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetLastPageInfo");
            DCEventInterfaceLogFunction(rootElement, 'GetLastPageInfo', startTime);
            return result;
        };
        /**
        * @name GetDocumentPageNum
        * @type function
        * @classification file
        * @apinameZh 兼容第四代获取文档的总页数
        * @returns ["result","any","文档的总页数"]
        */
        rootElement.GetDocumentPageNum = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentPageNum");
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentPageNum', startTime);
            return result;
        };

        /**
         * @name GetPrintPreviewHTML
         * @type function
         * @apinameZh 获取打印预览的HTML兼容四代接口
         * @classification print
         * @param ["options","object","对象属性",null,"",true]
         * @param ["callback","function","回调函数","","",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2023-06-29","兼容四代接口","wyc" ]
         */
        // options: {
        // "Files": arr,//二维数组对象，允许混合合并，若为空则预览当前文档
        // "InsertLastTableRowToPageBottom": "false"//预览时将表格插入到页底
        // "WaterMark": null//预览时附加的水印对象
        // "OutputMedicalUsingHtml" : false //使用HTML标记输出医学表达式而不使用图片
        // "CommitUserTrace" : false //是否获取提交痕迹后的HTML
        // "PageIndexs" : "2,6-7,9" //要打印的页号，从0开始
        // BindingDataXMLs: arr2, 字符串数组，提供绑定各个文档数据源的XML。此数组数量必须与files数量相等，若单个绑定为空可以传""字符串
        // }
        rootElement.GetPrintPreviewHTML = function (options, callBack) {
            ////wyc20240112全部改成五代后台生成HTML不再依赖四代服务了。
            //return rootElement.GetPrintPreviewHTML2(options, callBack);
            var startTime = new Date();
            //wyc20230625:重写逻辑准备调用第四代接口
            var strServicePageUrl = DCTools20221228.GetServicePageUrl(rootElement);
            if (strServicePageUrl == null || strServicePageUrl.length == 0) {
                console.error("DCWriter:未配置ServicePageUrl,无法执行GetPrintPreviewHTML.");
                DCEventInterfaceLogFunction(rootElement, 'GetPrintPreviewHTML', startTime);
                return false;
            }
            // 此处对应的服务器代码在 DCWriterForASPNET\Writer\Controls\Web\WC_WASM.cs
            var strUrl = strServicePageUrl + "?wasm=getprintpreviewhtml&dcbid2022=" + DCTools20221228.GetClientID();
            //wyc20230911:天王盖地虎
            if (options && (options.OutputMedicalUsingHtml === true || options.OutputMedicalUsingHtml === "true")) {
                strUrl = strUrl + "&processmedicaladvanced=true";
            }
            if (options && typeof (options.PageIndexs) === "string" && options.PageIndexs.length > 0) {
                strUrl = strUrl + "&pageindexs=" + options.PageIndexs;;
            }
            ////////////////////////
            var postData = rootElement.__DCWriterReference.invokeMethod("InnerGetPrintPreviewHtmlData", options);
            var xhr = new XMLHttpRequest();
            var resultstr = null;
            var hascallback = typeof (callBack) === "function";
            xhr.open("POST", strUrl, hascallback);
            //xhr.responseType = "blob";
            xhr.onload = async function () {
                if (this.status == 200) {
                    resultstr = this.response;

                    //------------------------------------- start
                    //此处为当存在配置PreserveBackgroundTextWhenPrint时给html一个字体颜色设置并给!importent
                    if (rootElement && rootElement.DocumentOptions) {
                        var needPreserveBackground = rootElement.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint;
                        if (needPreserveBackground === true) {
                            //找打body 直接替换字符串
                            resultstr = resultstr.replace('</head', '<style>#dcRootCenter{color: #000 !important}</style></head"');
                        }
                    }
                    //--------------------------------------  end
                    if (typeof (callBack) == "function") {
                        callBack.call(rootElement, resultstr);
                    }
                    return;
                }
            };
            xhr.send(postData);
            //DCEventInterfaceLogFunction(rootElement, 'GetPrintPreviewHTML', startTime)

            return resultstr;
        };


        /**
        * @name GetPrintPreviewHTML2
        * @type function
        * @apinameZh 获取打印预览的HTML的接口
        * @classification print
        * @param ["options","object","对象属性",null,"",true]
        * @param ["callback","function","回调函数","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-10-24","新增接口","wyc" ]
        * @change ["2024-1-26","补充图片处理流程","wyc" ]
        * @change ["2024-06-03","添加参数支持合并前分别绑定数据","wyc"]
        * @change ["2024-09-13","添加UnifiedHeaderFooterFile参数对所有合并文档提供统一的页眉页脚","wyc"]
        * @describe 参数同GetPrintPreviewHTML，但直接从五代获取HTML不再需要连接四代服务，必须使用callBack接收处理后的HTML否则会有内容缺失问题
        */
        rootElement.GetPrintPreviewHTML2 = function (options, callBack) {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            //wyc20240718: 获取打印预览HTML这一块不会修改文档DOM，不需要此判断
            //if (IsOperateDOM){
            result = rootElement.__DCWriterReference.invokeMethod("InnerGetPrintPreviewHtmlData2", options);
            WriterControl_Paint.ApplyBitmapContentHtmlSrc(result, function (strResultHtml) {
                if (typeof (callBack) == "function") {
                    callBack.call(rootElement, strResultHtml);
                }
            });
            DCEventInterfaceLogFunction(rootElement, 'GetPrintPreviewHTML2', startTime);
            // }


            return result;
        };

        /**
        * @name GetPrintPreviewHTML3
        * @type function
        * @apinameZh 获取打印预览的SVG格式的HTML的接口
        * @classification print
        * @param ["options","object","对象属性",null,"",true]
        * @param ["callback","function","回调函数","","",true]
        * @returns ["result","string","html字符串"]
        * @change ["2024-9-20","新增接口","wyc" ]
        * @describe 参数同GetPrintPreviewHTML2，获取SVG格式的打印预览合并HTML
        */
        rootElement.GetPrintPreviewHTML3 = function (options, callback) {
            var startTime = new Date();
            var result = WriterControl_Print.GetPrintPreviewSVGHTML(options, rootElement, callback);
            if (typeof (callback) == "function") {
                callback.call(rootElement, result);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetPrintPreviewHTML3', startTime);
            return result;
        };

        /**
         * @name GetXmlContent
         * @type function
         * @classification file
         * @apinameZh 兼容四代接口获取xml内容
         * @returns ["result","string","xml内容"]
         */
        rootElement.GetXmlContent = function () {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//阅读、预览、续打、区域选择时，不能修改dom
            if (IsOperateDOM) {
                result = this.SaveDocumentToString("XML");
            }
            DCEventInterfaceLogFunction(rootElement, 'GetXmlContent', startTime);
            return result;
        };

        /**
        * @name SaveBodyDocumentToString
        * @type function
        * @classification file
        * @apinameZh 兼容四代接口，保存文档的正文
        * @param ["fileFormat","string","数据格式","","",true]
        * @returns ["result","string","保存文档的正文"]
        */
        rootElement.SaveBodyDocumentToString = function (fileFormat) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SaveBodyDocumentToString", fileFormat);
            DCEventInterfaceLogFunction(rootElement, 'SaveBodyDocumentToString', startTime);
            return result;
        };

        /**
        * @name SetJumpPrintMode
        * @type function
        * @classification print
        * @apinameZh 兼容四代接口，续打模式
        * @param ["setValue","Boolean","值","","",true]
        */
        rootElement.SetJumpPrintMode = function (setValue) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("DCExecuteCommand", "JumpPrintMode", false, setValue);
            DCEventInterfaceLogFunction(rootElement, 'SetJumpPrintMode', startTime);

        };

        /**
       * @name SetElementPrintVisibility
       * @type function
       * @apinameZh 兼容四代接口，设置元素的是否打印的属性
       * @classification print
       * @param ["id","string","元素id","","",true]
       * @param ["visible","string","属性字符串","","",true]
       */
        rootElement.SetElementPrintVisibility = function (parameter, visible) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("SetElementPrintVisibility", parameter, visible);
            DCEventInterfaceLogFunction(rootElement, 'SetElementPrintVisibility', startTime);
        };

        /**
         * @name SetTableColumnVisible
         * @type function
         * @apinameZh 兼容四代接口，设置表格列隐藏
         * @classification table
         * @param ["tableElement","object","表格对象","","",true]
         * @param ["columnIndex","number","列下标","","",true]
         * @param ["visible","Boolean","属性是否可见","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SetTableColumnVisible = function (tableElement, columnIndex, visible) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetTableColumnVisible", tableElement, columnIndex, visible);
            DCEventInterfaceLogFunction(rootElement, 'SetTableColumnVisible', startTime);
            return result;
        };

        /**
        * @name GetSelectTableAndCell
        * @type function
        * @classification table
        * @apinameZh 为表格复制粘贴获取当前选择的表格和单元格
        * @returns ["result","object","选择的表格和单元格"]
        */
        rootElement.GetSelectTableAndCell = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetSelectTableAndCell");
            DCEventInterfaceLogFunction(rootElement, 'GetSelectTableAndCell', startTime);
            return result;
        };

        /**
        * @name SaveDocumentToStringAsync
        * @type function
        * @classification file
        * @apinameZh 兼容四代接口SaveDocumentToStringAsync
        * @param ["fileFormat","string","数据格式","","",true]
        * @returns ["result","string","文档内容"]
        */
        rootElement.SaveDocumentToStringAsync = function (fileFormat) {
            var startTime = new Date();
            let result = DCTools20221228.UnPackageStringValue(rootElement.__DCWriterReference.invokeMethod("SaveDocumentToString", fileFormat));
            DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToStringAsync', startTime);
            return result;
        };

        /**
        * @name GetAbsBoundsInDocument
        * @type function
        * @apinameZh 获取元素位置兼容四代接口
        * @classification structuralelement
        * @param ["ELEMENT","object","元素","","可以传元素ID/元素属性列表/元素后台.NET引用",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-07-04","兼容四代接口","wyc" ]
        */
        rootElement.GetAbsBoundsInDocument = function (element) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = null;
            if (typeof (rootElement.GetElementProperties) !== "function") {
                result = null;
            } else {
                var properties = rootElement.GetElementProperties(element);
                var obj = {
                    Left: properties.Left,
                    Top: properties.Top,
                    AbsLeft: properties.AbsLeft,
                    AbsTop: properties.AbsTop,
                    Width: properties.Width,
                    Height: properties.Height,
                    Right: properties.Left + properties.Width,
                    Bottom: properties.Top + properties.Height,
                    TopInOwnerPage: properties.TopInOwnerPage,
                    LeftInOwnerPage: properties.LeftInOwnerPage
                };
                result = obj;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetAbsBoundsInDocument', startTime);
            return result;
        };

        /**
        * @name SetTableHeight
        * @type function
        * @apinameZh 批量设置表格行高度
        * @classification table
        * @param ["id","string","表格id","","",true]
        * @param ["newHeight","number","表格行高度，像素单位","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-07-04","兼容四代接口","wyc" ]
        */
        rootElement.SetTableHeight = function (element, newHeight) {
            var startTime = new Date();
            if (typeof (rootElement.GetElementProperties) !== "function") {
                DCEventInterfaceLogFunction(rootElement, 'SetTableHeight', startTime);
                return false;
            }
            var properties = rootElement.GetElementProperties(element);
            if (Array.isArray(properties.RowsHeight) === false) {
                DCEventInterfaceLogFunction(rootElement, 'SetTableHeight', startTime);
                return false;
            }
            var height = 0;
            if (typeof (newHeight) === "string") {
                var str = newHeight.replace("px", "");
                height = parseFloat(str);
                height = height * 3.125;//像素转三百分之一英寸
            } else if (typeof (newHeight) === "number") {
                height = newHeight * 3.125;
            }
            if (height == 0) {
                DCEventInterfaceLogFunction(rootElement, 'SetTableHeight', startTime);
                return false;
            }
            var arr = new Array();
            for (var i = 1; i <= properties.RowsHeight.length; i++) {
                arr.push(height);
            }
            var opt = {
                RowsHeight: arr
            };
            let result = rootElement.SetElementProperties(element, opt);
            DCEventInterfaceLogFunction(rootElement, 'SetTableHeight', startTime);
            return result;
        };


        /**
        * @name GetTableCell
        * @type function
        * @classification table
        * @apinameZh 获取给定表格内的指定行列的单元格后台引用.NET对象
        * @param ["arguments1","string","表格id","","",true]
        * @param ["rowIndex","number","行的下标","","",true]
        * @param ["ColIndex","number","列的下标","","",false]
        * @returns ["result","object","单元格"]
        * @change ["2023-07-04","新增接口","wyc" ]
        */
        rootElement.GetTableCell = function () {
            var startTime = new Date();
            var result = null;
            if (arguments && arguments.length) {
                if (arguments.length === 3 &&
                    typeof (arguments[0]) === "string" &&
                    typeof (arguments[1]) === "number" &&
                    typeof (arguments[2]) === "number") {
                    result = rootElement.__DCWriterReference.invokeMethod("GetTableCell", arguments[0], arguments[1], arguments[2]);
                } else if (arguments.length === 2 &&
                    typeof (arguments[0]) === "string" &&
                    typeof (arguments[1]) === "string") {
                    result = rootElement.__DCWriterReference.invokeMethod("GetTableCell2", arguments[0], arguments[1]);
                }
                DCEventInterfaceLogFunction(rootElement, 'GetTableCell', startTime);
                return result;
            }
        };

        /**
       * @name GetDocumentCustomAttributes
       * @type function
       * @apinameZh 获取文档自定义属性
       * @classification attribute
       * @returns ["result","object","自定义属性"]
       * @change ["2023-07-05","兼容四代接口","wyc" ]
       */
        rootElement.GetDocumentCustomAttributes = function () {
            var startTime = new Date();
            var result = null;
            if (typeof (rootElement.GetElementProperties) !== "function" || rootElement.Document == null) {
                DCEventInterfaceLogFunction(rootElement, 'GetDocumentCustomAttributes', startTime);
                return null;
            }
            var obj = rootElement.GetElementProperties(rootElement.Document);
            if (obj.Attributes) {
                result = obj.Attributes;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentCustomAttributes', startTime);
            return result;
        };

        /**
       * @name RemoveDocumentCustomAttributes
       * @type function
       * @apinameZh 删除文档指定名称的自定义属性
       * @classification attribute
       * @param ["name","string","表示要删除的自定义属性的键名称","","",true]
       * @returns ["result","boolean","操作是否成功"]
       * @change ["2023-12-12","删除指定名称的自定义属性","wyc" ]
       */
        rootElement.RemoveDocumentCustomAttributes = function (name) {
            var startTime = new Date();
            var result = null;
            if (typeof (rootElement.GetElementProperties) !== "function" ||
                typeof (rootElement.SetElementProperties) !== "function" || rootElement.Document == null ||
                name == null || name.length === 0) {
                return false;
            }
            var attr = rootElement.GetDocumentCustomAttributes();
            if (attr == null) {
                return false;
            }
            attr[name] = undefined;
            var options = {
                Attributes: attr
            };
            result = rootElement.SetElementProperties(rootElement.Document, options);
            DCEventInterfaceLogFunction(rootElement, 'RemoveDocumentCustomAttributes', startTime);
            return result;
        };

        /**
      * @name SetDocumentCustomAttributes
      * @type function
      * @apinameZh 设置文档自定义属性
      * @classification attribute
      * @param ["attr","object","表示自定义属性的前端键-值对象","","",true]
      * @returns ["result","boolean","操作是否成功"]
      * @change ["2023-07-05","兼容四代接口","wyc" ]
      */
        rootElement.SetDocumentCustomAttributes = function (attr) {
            var startTime = new Date();
            var result = null;
            if (typeof (rootElement.SetElementProperties) !== "function" || rootElement.Document == null) {
                return false;
            }
            var options = {
                Attributes: attr
            };
            result = rootElement.SetElementProperties(rootElement.Document, options);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentCustomAttributes', startTime);
            return result;
        };

        /**
        * @name SetDocumentGlobalJavaScript
        * @type function
        * @apinameZh 设置文档自定义属性
        * @classification attribute
        * @param ["scriptstring","string","要设置的脚本字符串","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-07-05","兼容四代接口","wyc" ]
        */
        rootElement.SetDocumentGlobalJavaScript = function (scriptstring) {
            var startTime = new Date();
            var result = null;
            if (typeof (rootElement.SetElementProperties) !== "function" || rootElement.Document == null) {
                return false;
            }
            var options = {
                GlobalJavaScript: scriptstring
            };
            result = rootElement.SetElementProperties(rootElement.Document, options);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentGlobalJavaScript', startTime);
            return result;
        };

        /**
        * @name GetDocumentGlobalJavaScript
        * @type function
        * @apinameZh 获取文档自定义属性
        * @classification attribute
        * @returns ["result","object","自定义属性"]
        * @change ["2023-07-05","兼容四代接口","wyc" ]
        */
        rootElement.GetDocumentGlobalJavaScript = function () {
            var startTime = new Date();
            var result = null;
            if (typeof (rootElement.SetElementProperties) !== "function" || rootElement.Document == null) {
                return false;
            }
            var options = rootElement.GetElementProperties(rootElement.Document);
            if (options != null) {
                result = options.GlobalJavaScript;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentGlobalJavaScript', startTime);
            return result;
        };

        /**
        * @name GetTableRow
        * @type function
        * @classification table
        * @apinameZh 获取给定表格内的指定行号的表格行后台.NET对象引用
        * @param ["tableID","string","表格id","","",true]
        * @param ["rowIndex","number","行下标","","",true]
        * @returns ["result","object","表格行.NET对象引用"]
        * @change ["2023-07-05","新增接口","wyc" ]
        */
        rootElement.GetTableRow = function () {
            var startTime = new Date();
            var result = null;
            if (arguments.length === 2 &&
                typeof (arguments[0]) === "string" &&
                typeof (arguments[1]) === "number") {
                result = rootElement.__DCWriterReference.invokeMethod("GetTableRow", arguments[0], arguments[1]);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetTableRow', startTime);
            return result;
        };

        /**
        * @name GetChartElementDataByID
        * @type function
        * @apinameZh 获取拆线图元素的数据集对象
        * @classification structuralelement
        * @param ["element","object","折线图元素的ID/属性集/后台.NET引用","","CurrentElement('XTextCharElement')",true]
        * @returns ["result","object","数据集对象"]
        * @change ["2023-07-05","新增接口","wyc" ]
        */
        rootElement.GetChartElementDataByID = function (element) {
            var startTime = new Date();
            var result = null;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            if (typeof (rootElement.SetElementProperties) !== "function") {
                return false;
            }
            var options = rootElement.GetElementProperties(element);
            if (options != null) {
                result = options.ChartDatas;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetChartElementDataByID', startTime);
            return result;
        };

        /**
         * @name SetChartElementDataByID
         * @type function
         * @apinameZh 设置拆线图元素的数据集对象
         * @classification structuralelement
         * @param ["element","object","折线图元素的ID/属性集/后台.NET引用","","",true]
         * @param ["chartdata","object","折线图元素的数据集对象","","",true]
         * @returns ["result","object","数据集对象"]
         * @change ["2023-07-05","兼容四代接口","wyc" ]
         */
        rootElement.SetChartElementDataByID = function (element, chartdata) {
            var startTime = new Date();
            var result = null;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            if (typeof (rootElement.SetElementProperties) !== "function") {
                return false;
            }
            var options = {
                ChartDatas: chartdata
            };
            result = rootElement.SetElementProperties(element, options);
            DCEventInterfaceLogFunction(rootElement, 'SetChartElementDataByID', startTime);
            return result;
        };

        /**
        * @name GetNotSupportFontNames
        * @type function
        * @apinameZh 文档中包含不支持的字体
        * @classification file
        * @returns ["result","string","包含不支持的字体"]
        * @change ["2023-07-14","新增接口","wyc" ]
        */
        rootElement.GetNotSupportFontNames = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("GetNotSupportFontNames");
            DCEventInterfaceLogFunction(rootElement, 'GetNotSupportFontNames', startTime);
            return result;
        };


        /**
       * @name CurrentTableColumn
       * @type function
       * @classification table
       * @apinameZh 获取当前表格列对象
       * @returns ["result","object","当前表格列对象"]
       * @change ["2023-07-20","新增接口","wyc" ]
       */
        rootElement.CurrentTableColumn = function () {
            var startTime = new Date();
            var result = rootElement.CurrentElement("XTextTableColumnElement");
            DCEventInterfaceLogFunction(rootElement, 'CurrentTableColumn', startTime);
            return result;
        };

        /**
       * @name ExecuteAllEffectExpressions
       * @type function
       * @apinameZh 手动执行文档中的全部表达式
       * @classification file
       * @param ["CallbackFunction","function","回调函数","","手动执行文档中的全部表达式后执行的函数，主要是防止会刷新导致的空白",false]
       * @returns ["result","Boolean","操作是否成功"]
       * @change ["2023-07-25","新增接口","wyc" ]
       */
        rootElement.ExecuteAllEffectExpressions = function (CallbackFunction) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("ExecuteAllEffectExpressions");
            // ExecuteAllEffectExpressions接口添加回调函数【DUWRITER5_0-3153】
            if (WriterControl_Task.__Tasks && WriterControl_Task.__Tasks.length > 0) {
                WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                    if (typeof (CallbackFunction) === "function") {
                        CallbackFunction(result);
                    }
                    DCEventInterfaceLogFunction(rootElement, 'ExecuteAllEffectExpressions', startTime);
                });
                return result;
            } else {
                if (typeof (CallbackFunction) === "function") {
                    CallbackFunction(result);
                }
                DCEventInterfaceLogFunction(rootElement, 'ExecuteAllEffectExpressions', startTime);
            }
            return result;
        };


        /**
         * @name BindingColumnExpandingTables
         * @type function
         * @classification table
         * @apinameZh 易联众的横向扩展列数据的表格数据源绑定专属接口
         * @param ["bindname","string","绑定的数据源名称","","模板中必须有一个表格的数据源名称被设置成这个",true]
         * @param ["datas","object","绑定的数据集","","",true]
         * @param ["newpage","Boolean","扩展的表格是否需要另起一页","","",true]
         * @returns ["result","Boolean","操作是否成功"]
         * @change ["2023-07-26","新增接口","wyc" ]
         */
        /**
        * 易联众的横向扩展列数据的表格数据源绑定专属接口
        * 数据集传数组，格式如下，模板只需制作一个表格，后台会根据数组大小自动扩展表格
        * [
        *   {
        *     AAA: [
        *            {
        *               CCC: "YYY",
        *               DDD: "ZZZ"
        *            }
        *          ]
        *     BBB: "XXX"
        *   }
        * ]
        * AAA：表格绑定起始列设置的数据源绑定路径
        * BBB：表格绑定起始列之前的列中若有需要单独绑值的单元格设置的绑定路径
        * CCC、DDD：表格绑定起始列中单元格设置的绑定路径
        */
        rootElement.BindingColumnExpandingTables = function (bindname, datas, newpage) {
            var startTime = new Date();
            var neednewpage = newpage === true || newpage === "true" ? true : false;
            var result = rootElement.__DCWriterReference.invokeMethod("BindingColumnExpandingTables", bindname, datas, neednewpage);
            DCEventInterfaceLogFunction(rootElement, 'BindingColumnExpandingTables', startTime);
            return result;
        };

        /**
         * @name GetDataSourceBindingDescriptionsJSON
         * @type function
         * @classification datasource
         * @apinameZh 获取文档数据源绑定信息的JSON对象
         * @returns ["result","json","文档数据源绑定信息的JSON对象"]
         * @change ["2023-07-27","新增接口","wyc" ]
         */
        rootElement.GetDataSourceBindingDescriptionsJSON = function () {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("GetDataSourceBindingDescriptionsJSON");
            DCEventInterfaceLogFunction(rootElement, 'GetDataSourceBindingDescriptionsJSON', startTime);
            return result;
        };

        /**
        * @name DCFormTransmission
        * @type function
        * @classification structuralelement
        * @apinameZh 获取文档结构化数据的四代BS兼容接口
        * @param ["options","object","获取属性","","",true]
        * @param ["userinfo","object","留痕用户信息对象","","",true]
        * @returns ["result","json","获取文档结构化数据"]
        * @change ["2023-07-27","新增接口","wyc" ]
        * @change ["2024-09-09","新增UsingCurrentDocument参数","wyc" ]
        */
        // options {
        // FileContentXML: xmlfortest,//1.需要加载处理的原文档XML字符串
        // IsUseBase64: false,//2.指定加载和保存的字符串是否是BASE64字符串
        // IsBindingData: false,//3. 指定加载文档后是否要做将数据绑定到文档的处理
        // Datas: dataobj, //4. 若要绑定数据在这里指定绑定文档所需要的数据集
        // IsReturnFileContent: true, //5.加载并绑定处理完文档后是否将文档保存成字符串返回前端
        // IsReturnStruct: true, //6.加载并绑定处理完文档后是否返回加载文档的前端JSON结构
        // OutputElementInnerXML: false, //是否输出元素自身的XML  20220620新增
        // NestedMode: true  //是否输出元素嵌套关系
        // ReturnMergedContent: false //是否返回绑定带痕迹的文档字符串
        // UsingCurrentDocument: false //当FileContentXML为空时是否引用编辑器当前的文档，默认为false
        //  }
        // userinfo {
        // userid: "zs",//留痕需要的用户ID信息
        // username: "张三",//留痕需要的用户名称信息
        // permissionlevel: "0",//留痕需要的用户权限等级号
        // clientname: "pc", //留痕需要的客户端名称信息
        //  }
        rootElement.DCFormTransmission = function (options, userinfo) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var result = false;
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("DCFormTransmission", options, userinfo);
                DCEventInterfaceLogFunction(rootElement, 'DCFormTransmission', startTime);
            }
            return result;
        };


        /**
        * @name DCFormTransmission2
        * @type function
        * @classification structuralelement
        * @apinameZh 获取文档结构化数据的四代BS兼容接口
        * @param ["options","object","获取属性","","",true]
        * @param ["userinfo","object","留痕用户信息对象","","",true]
        * @returns ["result","json","获取文档结构化数据"]
        * @change ["2024-08-01","新增四代转发接口","wyc" ]
        */
        rootElement.DCFormTransmission2 = function (options, userinfo, callBack) {
            var startTime = new Date();
            var strServicePageUrl = DCTools20221228.GetServicePageUrl(rootElement);
            if (strServicePageUrl == null || strServicePageUrl.length == 0) {
                console.error("DCWriter:未配置ServicePageUrl,无法执行GetPrintPreviewHTML.");
                DCEventInterfaceLogFunction(rootElement, 'GetPrintPreviewHTML', startTime);
                return false;
            }
            // 此处对应的服务器代码在 DCWriterForASPNET\Writer\Controls\Web\WC_WASM.cs
            var strUrl = strServicePageUrl + "?wasm=dcformtransmission&dcbid2022=" + DCTools20221228.GetClientID();

            var usebase64 = options.IsUseBase64 === "true" || options.IsUseBase64 === true ? "true" : "false";
            var returnstruct = options.IsReturnStruct === "true" || options.IsReturnStruct === true ? "true" : "false";
            var returnfilecontent = options.IsReturnFileContent === "true" || options.IsReturnFileContent === true ? "true" : "false";
            var bindingdata = options.IsBindingData === "true" || options.IsBindingData === true ? "true" : "false";
            var outputxml = options.OutputElementInnerXML === "true" || options.OutputElementInnerXML === true ? "true" : "false";
            var nestedmode = options.NestedMode === "true" || options.NestedMode === true ? "true" : "false";
            var needmerge = options.ReturnMergedContent === "true" || options.ReturnMergedContent === true ? "true" : "false";

            strUrl = strUrl +
                "&usebase64=" + usebase64 +
                "&returnstruct=" + returnstruct +
                "&returnfilecontent=" + returnfilecontent +
                "&outputxml=" + outputxml +
                "&nestedmode=" + nestedmode +
                "&needmerge=" + needmerge +
                "&bindingdata=" + bindingdata + "&tick=" + new Date().valueOf();

            var opts = {
                FileContentXML: options.FileContentXML,
                BindingData: options.Datas,
                UserInfo: userinfo
            };

            var postData = rootElement.__DCWriterReference.invokeMethod("DCFormTransmission2", opts);
            var xhr = new XMLHttpRequest();
            var resultstr = null;
            var hascallback = typeof (callBack) === "function";
            xhr.open("POST", strUrl, hascallback);
            //xhr.responseType = "blob";
            xhr.onload = async function () {
                if (this.status == 200) {
                    resultstr = this.response;
                    var resultobj = JSON.parse(this.response);
                    var temp = resultobj.Structure.toString();
                    resultobj.Structure = temp.length === 0 ? null : JSON.parse(temp);
                    resultstr = resultobj;
                    if (typeof (callBack) == "function") {
                        callBack.call(rootElement, resultstr);
                    }
                    return;
                }
            };
            xhr.send(postData);

            DCEventInterfaceLogFunction(rootElement, 'DCFormTransmission2', startTime);
            return resultstr;
        };

        /**
         * @name EditorRefreshContainerView
         * @type function
         * @apinameZh 手动刷新某个容器元素
         * @classification structuralelement
         * @param ["obj","number","指定容器元素的后台.NET引用对象","","获取后台引用对象：document.WriterControl.GetElementByIdExt('姓名2')",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2023-08-01","新增接口","wyc" ]
         */
        rootElement.EditorRefreshContainerView = function (obj) {
            var startTime = new Date();
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(obj) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("EditorRefreshContainerView", obj);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("EditorRefreshContainerView2", obj);
            }
            DCEventInterfaceLogFunction(rootElement, 'EditorRefreshContainerView', startTime);
            return result;
        };


        /**
         * @name MergeDocumentByFileContent
         * @type function
         * @apinameZh 合并对比文档内容
         * @classification file
         * @param ["obj","object","合并文档对象","","",true]
         * @returns ["result","xml","返回合并带痕迹的文档XML"]
         * @change ["2024-03-11","改成五代本地接口实现","wyc" ]
         */
        // obj {
        //     OldUserID:
        //     OldUserName:
        //     OldSaveTime:
        //     OldPermissionLevel:
        //     OldFileContent:
        //     OldClientName:
        //     NewUserID:
        //     NewUserName:
        //     NewSaveTime:
        //     NewPermissionLevel:
        //     NewFileContent:
        //     NewClientName:
        //     FileFormat:设成"base64"可让后台将filecontent当作BASE64加载
        //     ReturnHTML: 第五代中若为false则返回合并文档的XML字符串，否则返回预览HTML
        //     ReturnUserTrackInfoList: 是否返回痕迹列表，需搭配ReturnHTML=true操作
        // }
        rootElement.MergeDocumentByFileContent = function (obj, callBack) {
            var startTime = new Date();
            if (obj == null) {
                return false;
            }
            var strServicePageUrl = DCTools20221228.GetServicePageUrl(rootElement);
            if (strServicePageUrl == null || strServicePageUrl.length == 0) {
                console.error("DCWriter:未配置ServicePageUrl,无法执行MergeDocumentByFileContent.");
                return false;
            }
            // 此处对应的服务器代码在 DCWriterForASPNET\Writer\Controls\Web\WC_WASM.cs
            var returnhtml = (obj.ReturnHTML === true || obj.ReturnHTML === "true") ? "true" : "false";
            var strUrl = strServicePageUrl + "?wasm=dcmergedocument&dcbid2022=" + DCTools20221228.GetClientID() + "&returnhtml=" + returnhtml;
            var postData = rootElement.__DCWriterReference.invokeMethod("InnerGetMergeDocumentByFileContentData", obj);
            var xhr = new XMLHttpRequest();
            var resultstr = null;
            var hascallback = typeof (callBack) === "function";
            xhr.open("POST", strUrl, hascallback);
            //xhr.responseType = "blob";
            xhr.onload = async function () {
                if (this.status == 200) {
                    resultstr = this.response;

                    if (obj.ReturnUserTrackInfoList === true && obj.ReturnHTML === true) {
                        var divv = document.createElement("div");
                        divv.innerHTML = resultstr;
                        var input = divv.querySelector("#_usertrackinfoobjstringforprint");
                        if (input != null && input.value != null && input.value.length > 0) {
                            resultstr = JSON.parse(input.value);
                        }
                    }

                    if (typeof (callBack) == "function") {
                        callBack.call(rootElement, resultstr);
                    }
                    return;
                }
            };
            xhr.send(postData);
            //DCEventInterfaceLogFunction(rootElement, 'MergeDocumentByFileContent', startTime)


            return resultstr;
        };

        /**
         * @name MergeDocumentByFileContent2
         * @type function
         * @apinameZh 合并对比文档内容
         * @classification file
         * @param ["obj","object","合并文档对象","","",true]
         * @returns ["result","xml","返回合并带痕迹的文档XML"]
         * @change ["2024-03-11","改成五代本地接口实现","wyc" ]
         */
        // obj {
        //     OldUserID:
        //     OldUserName:
        //     OldSaveTime:
        //     OldPermissionLevel:
        //     OldFileContent:
        //     OldClientName:
        //     NewUserID:
        //     NewUserName:
        //     NewSaveTime:
        //     NewPermissionLevel:
        //     NewFileContent:
        //     NewClientName:
        //     FileFormat:设成"base64"可让后台将filecontent当作BASE64加载
        //     ReturnHTML: 第五代中若为false则返回合并文档的XML字符串，否则返回预览HTML
        //     ReturnUserTrackInfoList: 是否返回对比后的痕迹列表，需要配合ReturnHTML为true使用
        // }
        rootElement.MergeDocumentByFileContent2 = function (obj, callBack) {
            var startTime = new Date();
            if (typeof (obj) !== "object") {
                return null;
            }
            var postData = rootElement.__DCWriterReference.invokeMethod("InnerGetMergeDocumentByFileContentData2", obj);
            var resultstr = JSON.parse(JSON.stringify(postData));
            if (obj.ReturnHTML === true) {
                WriterControl_Paint.ApplyBitmapContentHtmlSrc(resultstr, function (strResultHtml) {
                    if (obj.ReturnUserTrackInfoList === true) {
                        var divv = document.createElement("div");
                        divv.innerHTML = strResultHtml;
                    }


                    if (typeof (callBack) == "function") {
                        callBack.call(rootElement, strResultHtml);
                    }
                });
            } else {
                if (typeof (callBack) == "function") {
                    callBack.call(rootElement, postData);
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'MergeDocumentByFileContent2', startTime);
            return resultstr;
        };



        /**
        * @name EditorSetContentStyle
        * @type function
        * @apinameZh 设置指定容器内容的样式和对齐方式等
        * @classification structuralelement
        * @param ["container","object","容器元素","","指定容器元素的后台.NET引用对象或元素的ID",true]
        * @param ["style","string","样式","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-08-10","增加接口","wyc" ]
        */
        rootElement.EditorSetContentStyle = function (container, style) {
            var startTime = new Date();
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(container) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("EditorSetContentStyle2", container, style);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("EditorSetContentStyle", container, style);;
            }
            DCEventInterfaceLogFunction(rootElement, 'EditorSetContentStyle', startTime);
            return result;
        };


        /**
        * @name SetSelectTableCellBorder
        * @type function
        * @apinameZh 为选择的单元格设置边框
        * @classification table
        * @param ["settings","object","边框对象","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetSelectTableCellBorder = function (settings) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("SetSelectTableCellBorder", settings);
            DCEventInterfaceLogFunction(rootElement, 'SetSelectTableCellBorder', startTime);
            return result;
        };

        /**
         * @name1 SetTableCellTextExtByHandle
         * @type function
         * @apinameZh 为选择多个单元格后，粘贴赋值(目前只有dc内部使用)
         * @classification table
         * @param ["cell","number","单元格NativeHandle","","",true]
         * @param ["newText","string","文本","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SetTableCellTextExtByHandle = function (cell, newText) {
            var startTime = new Date();
            var result = false;
            var result = rootElement.__DCWriterReference.invokeMethod("SetTableCellTextExtByHandle", cell, newText);
            DCEventInterfaceLogFunction(rootElement, 'SetTableCellTextExtByHandle', startTime);
            return result;
        };

        /**
        * @name SetChildElements
        * @type function
        * @apinameZh 插入给定的元素列表到某个容器内
        * @classification structuralelement
        * @param ["targetEle","object","元素","","指定容器元素的后台.NET引用对象或元素的ID",true]
        * @param ["newEles","object","指定插入的元素对象集合","","格式参考四代BS编程文档",true]
        * @param ["options","string","插入位置","","afterBegin 开始位置 beforeEnd 最后位置,beforebegin:前面插入, index 坐标",true]
        * @param ["isRefresh","Boolean","是否刷新","true","默认值是true",false]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetChildElements = function (targetEle, newEles, options, isRefresh = true, isAddTrackInfos = false) {
            var startTime = new Date();
            var result = false;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//获取当前是否可以修改dom
            var currentElement = rootElement.GetElementProperties(targetEle);//当前元素
            //判断可以修改dom，并且当前元素存在，才能继续执行接口
            if (IsOperateDOM && currentElement) {
                //判断是否存在音视频元素
                if (Array.isArray(newEles)) {
                    newEles.forEach(item => {
                        var allKeys = Object.keys(item);
                        if (allKeys && allKeys[0]) {
                            var thisKey = allKeys[0];
                            if (typeof thisKey == "sting") {
                                thisKey = thisKey.toLowerCase().trim();

                            }
                            if (thisKey == "media") {
                                if (!item[thisKey].ID) {
                                    item[thisKey].ID = 'media' + Date.now();
                                }
                                rootElement.closeScreenChange = true;
                                setTimeout(() => {
                                    rootElement.closeScreenChange = false;
                                }, 1000);
                            }
                        }
                    });
                }
                if (DCTools20221228.IsDotnetReferenceElement(targetEle) === true) {
                    result = rootElement.__DCWriterReference.invokeMethod("SetChildElements2", targetEle, newEles, options, isRefresh, isAddTrackInfos);
                } else {
                    result = rootElement.__DCWriterReference.invokeMethod("SetChildElements3", targetEle, newEles, options, isRefresh, isAddTrackInfos);
                }
                DCEventInterfaceLogFunction(rootElement, 'SetChildElements', startTime);

            }
            return result;
        };

        /**
        * @name InsertElements
        * @type function
        * @apinameZh 插入给定的元素列表到当前位置
        * @classification structuralelement
        * @param ["newEles","object","指定插入的元素对象集合","","格式参考四代BS编程文档",true]
        * @param ["isRefresh","Boolean","是否刷新","true","默认值是true",false]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2024-02-29","新增接口","wyc" ]
        */
        rootElement.InsertElements = function (newEles, isRefresh = true) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("SetChildElements", null, newEles, null);
            DCEventInterfaceLogFunction(rootElement, 'InsertElements', startTime);
            return result;
        };

        /**
        * @name FocusSubElement
        * @type function
        * @apinameZh 在病程记录中定位指定的输入域
        * @classification subdoc
        * @param ["element","object","病程记录对象","","",true]
        * @returns ["inputID","string","输入域编号"]
        * @change ["2023-09-26","改造写法直接转发功能","wyc" ]
        */
        rootElement.FocusSubElement = function (element, inputID) {
            var startTime = new Date();
            //wyc20230926:改造写法直接转发功能
            var inputElement = rootElement.GetElementByIdExt(inputID, element);
            DCEventInterfaceLogFunction(rootElement, 'FocusSubElement', startTime);
            return rootElement.FocusElement && rootElement.FocusElement(inputElement);
        };

        /**
        * @name CanInsertAtCurrentPosition
        * @type function
        * @classification file
        * @apinameZh 判断当前位置能否插入内容
        * @returns ["result","boolean","当前位置能否插入内容"]
        */

        rootElement.CanInsertAtCurrentPosition = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = false;
            result = rootElement.__DCWriterReference.invokeMethod("CanInsertAtCurrentPosition");
            DCEventInterfaceLogFunction(rootElement, 'CanInsertAtCurrentPosition', startTime);
            return result;
        };

        /**
        * @name CanInsertElementAtCurrentPosition
        * @type function
        * @apinameZh 判断当前能否插入指定类型的元素
        * @classification structuralelement
        * @param ["typeaName","string","元素类型的名称","","",true]
        * @returns ["result","boolean","当前能否插入指定类型的元素"]
        */
        rootElement.CanInsertElementAtCurrentPosition = function (typeaName) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = false;
            result = rootElement.__DCWriterReference.invokeMethod("CanInsertElementAtCurrentPosition", typeaName);
            DCEventInterfaceLogFunction(rootElement, 'CanInsertElementAtCurrentPosition', startTime);
            return result;
        };


        /**
        * @name SetElementContentLock
        * @type function
        * @apinameZh 设置元素的内容锁定相关属性
        * @classification structuralelement
        * @param ["element","object","文档元素的ID/后台引用对象/NativeHandle","","",true]
        * @param ["obj","object","内容锁定属性对象","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-08-29","兼容四代接口","wyc" ]
        */
        // 内容锁定属性对象 {
        //   * OwnerUserID: "", //容器元素所有者用户ID
        //   * CreationTime: "",  //创建时间
        //   * AuthorisedUserIDList: ""    //允许编辑元素的用户ID列表，用英文逗号分隔
        //         * }
        rootElement.SetElementContentLock = function (element, obj) {
            var opt = {
                ContentLock: obj
            };
            return rootElement.SetElementProperties && rootElement.SetElementProperties(element, opt);
        };


        /**
         * @name GetNextFocusFieldElement
         * @type function
         * @apinameZh  获取指定输入域的下一个定位焦点的输入域元素
         * @classification structuralelement
         * @param ["element","object","文档元素的ID/后台引用对象/NativeHandle","","",true]
         * @returns ["result","boolean","指定输入域的下一个定位焦点"]
         * @change ["2023-08-31","新增接口","wyc" ]
        */
        rootElement.GetNextFocusFieldElement = function (element) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(element) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetNextFocusElement2", element);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetNextFocusElement", element);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetNextFocusFieldElement', startTime);
            return result;
        };


        /**
        * @name HighlightTableRowColumn
        * @type function
        * @apinameZh 使用指定的背景色高亮化表格指定的行或列
        * @classification table
        * @param ["tableElement","object","表格元素的ID/后台引用对象/NativeHandle","","",true]
        * @param ["rowIndex","number","需要亮化的行的行号","-1","",false]
        * @param ["colIndex","number","需要亮化的行的列号","-1","",false]
        * @param ["colorString","string","亮化使用的背景色","透明","",false]
        * @param ["cancelUnset","string","未亮化的行或列是否恢复背景色为透明色","true","",false]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-09-04","新增接口","wyc" ]
        */
        rootElement.HighlightTableRowColumn = function (
            tableElement,
            rowIndex = -1,
            colIndex = -1,
            colorString = "Transparent",
            cancelUnset = true) {

            var startTime = new Date();
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(tableElement) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("HighlightTableRowColumn2", tableElement, rowIndex, colIndex, colorString, cancelUnset);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("HighlightTableRowColumn", tableElement, rowIndex, colIndex, colorString, cancelUnset);
            }
            DCEventInterfaceLogFunction(rootElement, 'HighlightTableRowColumn', startTime);
            return result;
        };

        /**
        * @name SetStandardImage
        * @type function
        * @apinameZh 设置单复选框自定义样式
        * @classification structuralelement
        * @param ["strKey","string","样式","","样式类型在demo中查看",true]
        * @param ["bsImage","string","图片的base64","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        // * None = 无效编号
        // * CheckBoxChecked = 勾选状态的复选框, 必须为16 * 16的图片格式。
        // * CheckBoxUnchecked = 不是勾选状态的复选框, 必须为16 * 16的图片格式。
        // * RadioBoxChecked = 勾选状态的单选框, 必须为16 * 16的图片格式。
        // * RadioBoxUnchecked = 不是勾选状态的单选框, 必须为16 * 16的图片格式。
        // * ParagraphFlagLeftToRight = 从左到右时的段落符号, 必须为9 * 12的图片格式。
        // * ParagraphFlagRightToLeft = 从右到左时的段落符号, 必须为9 * 12的图片格式。
        // * Linebreak = 换行符号, 必须为9 * 12的图片格式。
        // * DragHandle = 拖拽内容使用的把柄, 必须为13 * 13的图片格式。
        // * collapse = 收缩, 必须为16 * 16的图片格式。
        // * Expand = 展开, 必须为16 * 16的图片格式。
        rootElement.SetStandardImage = function (strKey, bsImage) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }

            var result = false;
            result = rootElement.__DCWriterReference.invokeMethod("SetStandardImage", strKey, bsImage);
            WriterControl_Paint.RefreshStandardImageList();
            DCEventInterfaceLogFunction(rootElement, 'SetStandardImage', startTime);
            return result;
        };

        /**
        * @name GetSelectionStartIndex
        * @type function
        * @apinameZh 返回选择内容的开始位置
        * @classification file
        * @returns ["result","string","位置"]
        */
        rootElement.GetSelectionStartIndex = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("GetSelectionStartIndex");
            DCEventInterfaceLogFunction(rootElement, 'GetSelectionStartIndex', startTime);
            return result;
        };

        /**
       * @name GetSelectionEndIndex
       * @type function
       * @apinameZh 返回选择内容的结束位置
       * @classification file
       * @returns ["result","string","位置"]
       */
        rootElement.GetSelectionEndIndex = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("GetSelectionEndIndex");
            DCEventInterfaceLogFunction(rootElement, 'GetSelectionEndIndex', startTime);
            return result;
        };

        /**
       * @name GetCurrentSubDocumentInfo
       * @type function
       * @apinameZh 返回当前病程的信息
       * @classification subdoc
       * @returns ["result","string","病程的信息"]
       */
        rootElement.GetCurrentSubDocumentInfo = function () {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("GetCurrentSubDocumentInfo");
            DCEventInterfaceLogFunction(rootElement, 'GetCurrentSubDocumentInfo', startTime);
            return result;
        };


        /**
       * @name FocusTableRow
       * @type function
       * @apinameZh 对当前表格行进行反色提示
       * @classification table
       * @change ["2023-09-13","兼容四代接口","wyc" ]
       * @param ["color","string","颜色","","",true]
       * @returns ["result","boolean","操作是否成功"]
       */
        rootElement.FocusTableRow = function (color) {
            var startTime = new Date();
            var row = rootElement.CurrentTableRow();
            if (!row) {
                return;
            }
            var prop = rootElement.GetElementProperties(row);
            if (!prop) {
                return;
            }
            var result = rootElement.HighlightTableRowColumn(
                prop.OwnerTable,
                prop.RowIndex,
                -1,
                color,
                true);
            DCEventInterfaceLogFunction(rootElement, 'FocusTableRow', startTime);
            return result;
        };

        /**
        * @name GetChildElements
        * @type function
        * @apinameZh 获取指定容器元素的内部子元素列表
        * @classification structuralelement
        * @param ["targetEle","object","目标容器对象","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-09-13","兼容四代接口","wyc" ]
        */
        rootElement.GetChildElements = function (targetEle) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var prop = rootElement.GetElementProperties(targetEle);
            DCEventInterfaceLogFunction(rootElement, 'GetChildElements', startTime);
            return (prop && prop.Elements) ? prop.Elements : null;
        };

        /**
        * @name SetCheckboxOrRadioTextAndValue
        * @type function
        * @classification structuralelement
        * @apinameZh 兼容四代接口，修改单选框/复选框的文本和值
        * @param ["element","object","元素","","参数可以是id字符串，也可以是后台的.NET对象（CurrentElement(xtextradioboxelement)、GetElementProperties(radio)）",true]
        * @param ["options","object","属性","","{ text: 修改的text值, value: 修改的value值, } 这种对象形式",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetCheckboxOrRadioTextAndValue = function (element, options) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("SetCheckboxOrRadioTextAndValue", element, options);
            DCEventInterfaceLogFunction(rootElement, 'SetCheckboxOrRadioTextAndValue', startTime);
            return result;
        };


        /**
         * @name LoadDocumentFromString2
         * @classification file
         * @type function
         * @apinameZh 特殊的加载文档接口
         * @param ["options","object","加载文档参数对象","","",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2023-09-14","兼容四代接口","wyc" ]
         * @change ["2024-01-08","改进性能","袁永福" ]
         * @change ["2024-04-28","加载文档接口在预览、续打、区域选择时禁止调用此接口","李新宇" ]
         */
        // options 加载文档参数对象{
        //     FileFormat:
        //     UseBase64String:
        //     FileContent:
        //     LoadPrintPreivew:
        //     DataBindingXML:
        //     CommitUserTrace:
        //     AppendSubDocuments:
        //     LoadFromUrl:
        // }
        rootElement.LoadDocumentFromString2 = function (options) {
            rootElement.CheckDisposed();

            //20240428 lixinyu 加载文档接口在预览、续打、区域选择时禁止调用(DUWRITER5_0-2400)
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            var ExtViewMode = ['Normal', 0].indexOf(rootElement.ExtViewMode) == -1;//续打模式
            var RectInfo = rootElement.RectInfo;//区域选择
            //当前存在其中一个模式，即不可修改dom，返回false
            if (IsPrintPreview || ExtViewMode || RectInfo) {
                return false;
            }

            var startTime = new Date();
            //20240326 lixinyu 判断是否为网络路径(DUWRITER5_0-2049)
            if (options && options.LoadFromUrl && options.LoadFromUrl.length) {
                var xhr = new XMLHttpRequest();
                xhr.open('GET', options.LoadFromUrl, false);
                xhr.onreadystatechange = function () {
                    if (xhr.readyState === 4 && xhr.status === 200) {
                        options['FileContent'] = xhr.response;
                        delete options.LoadFromUrl;
                    }
                };
                xhr.send();
            }
            var result = rootElement.__DCWriterReference.invokeMethod("LoadDocumentFromString2", options);
            if (result) {
                rootElement.ClearOldVisibleElements();
                //[DUWRITER5_0-3617] 20240919 lxy js调用LoadDocumentFromString2后，需要重新绘制一遍水印
                WriterControl_Paint.UpdateViewForWaterMark(rootElement);
            }

            DCEventInterfaceLogFunction(rootElement, 'LoadDocumentFromString2', startTime);
            return result;
        };

        rootElement.LoadDocumentFromBinary = function (bsContent, strFormat) {
            rootElement.CheckDisposed();
            var startTime = new Date();
            ////20240326 lixinyu 判断是否为网络路径(DUWRITER5_0-2049)
            //if (options && options.LoadFromUrl && options.LoadFromUrl.length) {
            //    var xhr = new XMLHttpRequest();
            //    xhr.open('GET', options.LoadFromUrl, false);
            //    xhr.onreadystatechange = function () {
            //        if (xhr.readyState === 4 && xhr.status === 200) {
            //            options['FileContent'] = xhr.response;
            //            delete options.LoadFromUrl;
            //        }
            //    };
            //    xhr.send();
            //}
            if (strFormat != null && strFormat.toLowerCase() == "ofd") {
                var strXml = WriterControl_IO.GetXmlStringFromOFD(bsContent);
                var bolResult2 = false;
                if (strXml != null && strXml.length > 0) {
                    bolResult2 = WriterControl_IO.LoadDocumentFromString(
                        { WriterControl: rootElement, Data: strXml });
                }
                else {
                    throw windows.__DCSR.PromptNotDCWriterOFD;
                }
                DCEventInterfaceLogFunction(rootElement, 'LoadDocumentFromBinary', startTime);
                return bolResult2;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("LoadDocumentFromBinary", bsContent, strFormat);
            WriterControl_Rule.InvalidateView(rootElement, "hrule");
            WriterControl_Rule.InvalidateView(rootElement, "vrule");

            if (result) {
                rootElement.ClearOldVisibleElements();
            }

            DCEventInterfaceLogFunction(rootElement, 'LoadDocumentFromBinary', startTime);
            return result;
        };

        /**
        * @name SynchroServerTimeByParameters
        * @type function
        * @apinameZh 同步服务器时间
        * @classification editformat
        * @param ["year","string","年","","",true]
        * @param ["month","string","月","","",true]
        * @param ["day","string","日","","",true]
        * @param ["hour24","string","时，24制","","",true]
        * @param ["minute","string","分","","",true]
        * @param ["second","string","秒","","",true]
        * @describe <br /> 本函数是SynchroServerTime(DateTime serverTime)的另外一个版本。用于对不支持DateTime类型的编程语言的支持。<br />本函数不会修改本地计算机时钟，而是修改本软件内部维护的一个虚拟时钟。<br />虚拟时钟会依赖本地计算机时钟。<br />不过同步服务器时间后，如果本机时钟修改了，<br />则需要重新调用本函数来同步服务器时间。如果不重新同步则会采用本机时刻。<br />因此建议定期（比如一分钟）来调用本函数同步服务器时间<br />
        */
        rootElement.SynchroServerTimeByParameters = function (year, month, day, hour24, minute, second) {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("SynchroServerTimeByParameters", year, month, day, hour24, minute, second);
            DCEventInterfaceLogFunction(rootElement, 'SynchroServerTimeByParameters', startTime);
        };

        /**
        * @name GetRootElementsByTypeName
        * @type function
        * @classification structuralelement
        * @apinameZh 获取根目录的指定类型的元素，注意和GetElementsByTypeName区别
        * @param ["elementTypeName","string","元素类型的全名称","","",true]
        * @param ["part","string","指定类型","body","枚举：body、header、footer、all",true]
        * @returns ["result","object","元素"]
        */
        rootElement.GetRootElementsByTypeName = function (elementTypeName, part) {
            if (part == null || part == '' || part == undefined) {
                part = 'body';
            }
            elementTypeName = elementTypeName.toLowerCase();
            part = part.toLowerCase();
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetRootElementsByTypeName", elementTypeName, part);
            DCEventInterfaceLogFunction(rootElement, 'GetRootElementsByTypeName', startTime);
            return result;
        };

        /**
        * @name GetSelectionNativeStartIndex
        * @type function
        * @classification file
        * @apinameZh 获取选择内容的Native开始位置
        * @returns ["result","any","位置"]
        */
        rootElement.GetSelectionNativeStartIndex = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("GetSelectionNativeStartIndex");
            DCEventInterfaceLogFunction(rootElement, 'GetSelectionNativeStartIndex', startTime);
            return result;
        };


        /**
        * @name GetSelectionNativeLength
        * @type function
        * @classification file
        * @apinameZh 获取选择内容的Native长度
        * @returns ["result","number","长度"]
        */
        rootElement.GetSelectionNativeLength = function () {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("GetSelectionNativeLength");
            DCEventInterfaceLogFunction(rootElement, 'GetSelectionNativeLength', startTime);
            return result;
        };

        /**
        * @name ResetModifiedSpecifyElement
        * @type function
        * @classification structuralelement
        * @apinameZh 重置制定元素（后台引用对象）的修改状态
        * @param ["element","object","元素的后台引用对象","","一般接收CurrentInputField接口的的返回值",true]
        * @returns ["result","any","修改状态"]
        */
        rootElement.ResetModifiedSpecifyElement = function (element) {
            var startTime = new Date();
            let result = false;
            //wyc20230926:修改写法
            var opt = {
                Modified: false
            };
            result = rootElement.SetElementProperties(element, opt);
            //if (DCTools20221228.IsDotnetReferenceElement(element) === true) {
            //    result = rootElement.__DCWriterReference.invokeMethod("ResetModifiedSpecifyElement", element);
            //} 
            DCEventInterfaceLogFunction(rootElement, 'ResetModifiedSpecifyElement', startTime);
            return result;
        };

        /**
        * @name ResetModifiedSpecifyElementByID
        * @type function
        * @apinameZh 重置制定元素（指定ID）的修改状态
        * @classification structuralelement
        * @param ["id","string","元素的ID","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.ResetModifiedSpecifyElementByID = function (id) {
            var startTime = new Date();
            let result = false;
            if (id == null || id == undefined) {
                id = '';
            }
            result = rootElement.__DCWriterReference.invokeMethod("ResetModifiedSpecifyElementByID", id);
            DCEventInterfaceLogFunction(rootElement, 'ResetModifiedSpecifyElementByID', startTime);
            return result;
        };

        /**
        * @name GetElementCoordinateByID
        * @type function
        * @apinameZh 兼容四代获取坐标位置
        * @classification structuralelement
        * @param ["ids","any","元素id或者数组","","",true]
        * @param ["ispreviewmode","any","兼容参数，未使用","","",true]
        * @param ["specifyxml","any","为空则使用当前文档","","",true]
        * @returns ["result","object","坐标"]
        */
        rootElement.GetElementCoordinateByID = function (ids, ispreviewmode, specifyxml) {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetElementCoordinateByID", ids, ispreviewmode, specifyxml);
            DCEventInterfaceLogFunction(rootElement, 'GetElementCoordinateByID', startTime);
            return result;
        };

        /**
        * @name GetDocumentSpecifyPageImages
        * @type function
        * @apinameZh 兼容四代获取指定页码的图片数据
        * @classification structuralelement
        * @param ["options","object","获取参数属性","","json格式包含两个参数：ShowMarginLine是否显示页面编辑线；SpecifyPageIndexes指定的页码数",true]
        * @param ["callBack","function","回调函数","","用于接收接口返回的base64字符串",true]
        * @param ["required","Function","成功获得数据后的回调函数","","回调函数参数是一个字符串数组，为图片格式的数据",true]
        */
        rootElement.GetDocumentSpecifyPageImages = function (options, callBack) {
            var startTime = new Date();
            var bsData = rootElement.__DCWriterReference.invokeMethod("GetDocumentSpecifyPageImages", options);
            if (bsData != null && bsData.length > 10) {
                var reader = new DCBinaryReader(bsData);
                var totalPageCount = reader.ReadInt16();
                var cavas = rootElement.ownerDocument.createElement("CANVAS");
                var imgDatas = new Array();
                var outputPageCount = 0;
                function DrawOnePage() {
                    var bsPage = reader.ReadByteArray();
                    if (bsPage != null && bsPage.length > 0) {
                        var pageReader = new DCBinaryReader(bsPage);
                        if (pageReader.ReadByte() != 133) {
                            // 文件头不对
                            return;
                        }
                        var pageWidth = pageReader.ReadInt16();
                        var pageHeight = pageReader.ReadInt16();
                        cavas.width = pageWidth;
                        cavas.height = pageHeight;
                        var ctx = cavas.getContext("2d");
                        ctx.clearRect(0, 0, pageWidth, pageHeight);
                        ctx.resetTransform();
                        if (typeof (ctx.reset) == "function") {
                            ctx.reset();
                        }
                        var drawer = new PageContentDrawer(cavas, pageReader);
                        drawer.EventAfterDraw = function () {
                            outputPageCount++;
                            var strData = cavas.toDataURL("image/png", 1);
                            imgDatas.push(strData);
                            if (outputPageCount == totalPageCount) {
                                callBack && callBack(imgDatas);
                            }
                            else {
                                DrawOnePage();
                            }
                        };
                        drawer.AddToTask();
                    }
                }
                DrawOnePage();
            }
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentSpecifyPageImages', startTime);
        };

        /**
         * @name PrintAsImage
         * @type function
         * @apinameZh 获取指定页码的打印下图片数据
         * @classification print
         * @param ["pageArr","array","页码数组","","需要获取的页码数组成的数组(为空值时返回所有)",true]
         * @param ["callBack","function","回调函数","","用于接收接口返回的base64字符串",true]
         */
        rootElement.PrintAsImage = function (pageArr, callBack) {
            if (WriterControl_Task.__Tasks && WriterControl_Task.__Tasks.length > 0) {
                WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                    rootElement.PrintAsImage(pageArr, callBack);
                });
                return;
            }

            var startTime = new Date();
            var options = null;
            if (pageArr) {
                if (Array.isArray(pageArr)) {
                    pageArr = pageArr.join(',');
                }
                options = {
                    PrintRange: "SomePages",
                    SpecifyPageIndexs: pageArr,
                    CleanMode: true
                };
            }
            var printImageScale = rootElement.getAttribute("printimagescale");
            printImageScale = printImageScale ? parseFloat(printImageScale) : 1;
            try {
                // 获取到是否为续打模式
                var jumpPrint = rootElement.JumpPrint;
                var oldJumpPrintPosition = 0;
                var oldJumpPrintEndPosition = 0;
                if (jumpPrint.Mode == "Normal") {
                    oldJumpPrintPosition = rootElement.JumpPrintPosition;
                    oldJumpPrintEndPosition = rootElement.JumpPrintEndPosition;
                    rootElement.JumpPrintPosition = 0;
                    rootElement.JumpPrintEndPosition = 0;
                }
                // 修复目前svg生成图片失效的问题【DUWRITER5_0-3802】
                var strCode = rootElement.__DCWriterReference.invokeMethod("GetPageIndexWidthHeightForPrint", true, options, false);
                if (strCode) {
                    var datas = JSON.parse(strCode);
                    var imgDatas = [];
                    var loadedImages = 0;
                    for (var iCount = 0; iCount < datas.length; iCount++) {
                        var pageInfo = datas[iCount];
                        var element = rootElement.ownerDocument.createElementNS("http://www.w3.org/2000/svg", "svg");
                        element.setAttribute("width", pageInfo.Width * printImageScale + "px");
                        element.setAttribute("height", pageInfo.Height * printImageScale + "px");
                        element.setAttribute("viewBox", "0 0 " + pageInfo.Width + " " + pageInfo.Height);
                        // 存储当前页面的索引
                        element.Index = iCount;
                        element.PageIndex = pageInfo.PageIndex;
                        WriterControl_Print.InnerDrawOnePage(element, false, rootElement);
                        svgToImage(element, function (dataURL, svgElement) {
                            loadedImages++;
                            imgDatas[svgElement.Index] = dataURL;
                            svgElement.remove();
                            // 等待所有图片都转好
                            if (loadedImages == datas.length) {
                                // 转成图片
                                if (!!callBack && typeof (callBack) == "function") {
                                    callBack(imgDatas);
                                }
                                imgDatas = null;
                            }
                        });
                    }// for
                }
                if (jumpPrint.Mode == "Normal") {
                    if (oldJumpPrintPosition) {
                        rootElement.JumpPrintPosition = oldJumpPrintPosition;
                    }
                    if (oldJumpPrintEndPosition) {
                        rootElement.JumpPrintEndPosition = oldJumpPrintEndPosition;
                    }
                }
                if (rootElement.IsPrintPreview() == false) {
                    // 打印先渲染页面展示，再进行打印【DUWRITER5_0-3379】
                    rootElement.__DCWriterReference.invokeMethod("RefreshViewAfterPrint", true);
                }
            } catch (error) {
                console.log(error);
            }
            function svgToImage(svgElement, callback) {
                // 将SVG元素转换为字符串
                var serializer = new XMLSerializer();
                var svgString = serializer.serializeToString(svgElement);

                // 使用DOMParser来确保SVG格式正确，并添加命名空间
                var parser = new DOMParser();
                var doc = parser.parseFromString(svgString, "image/svg+xml");
                var svgWithNS = doc.documentElement;

                // 将SVG字符串转换为Blob对象
                var blob = new Blob([svgWithNS.outerHTML], { type: "image/svg+xml;charset=utf-8" });

                // 创建一个URL对象指向该Blob对象
                var url = URL.createObjectURL(blob);

                // 创建一个Image对象并加载该URL
                var img = new Image();
                img.onload = function () {
                    // 将图片绘制到canvas上
                    var canvas = document.createElement("canvas");
                    var ctx = canvas.getContext("2d");
                    canvas.width = img.width;
                    canvas.height = img.height;
                    ctx.drawImage(img, 0, 0);

                    // 将canvas转换为图片
                    var dataURL = canvas.toDataURL("image/png");

                    // 调用回调函数，传递生成的图片
                    callback(dataURL, svgElement);
                };
                img.src = url;
            }

            // //获取所有的canvas
            // var strCode = rootElement.__DCWriterReference.invokeMethod(
            //     "GetPageIndexWidthHeightForPrint",
            //     true,
            //     options,
            //     false);
            // try {
            //     var datas = JSON.parse(strCode);
            //     if (datas && Array.isArray(datas)) {
            //         var allCanvas = [];
            //         var imgDatas = [];
            //         //修改ZoomRate
            //         var oldZoomRate = rootElement.__DCWriterReference.invokeMethod("get_ZoomRate");
            //         for (var i = 0; i < datas.length; i++) {
            //             var pageInfo = datas[i];
            //             var element = rootElement.ownerDocument.createElement("CANVAS");
            //             element.__PageInfo = pageInfo;
            //             element.PageIndex = pageInfo.PageIndex;
            //             element.setAttribute("native-width", pageInfo.Width);
            //             element.setAttribute("native-height", pageInfo.Height);
            //             WriterControl_Paint.SetPageElementSize(rootElement, element);
            //             element.width = pageInfo.Width * printImageScale;
            //             element.height = pageInfo.Height * printImageScale;
            //             allCanvas.push(element);
            //             rootElement.__DCWriterReference.invokeMethod(
            //                 "SetViewZoomRate",
            //                 printImageScale);
            //             WriterControl_Print.InnerDrawOnePage(element, true, rootElement);

            //         }
            //         if (jumpPrint.Mode == "Normal") {
            //             if (oldJumpPrintPosition) {
            //                 rootElement.JumpPrintPosition = oldJumpPrintPosition;
            //             }
            //             if (oldJumpPrintEndPosition) {
            //                 rootElement.JumpPrintEndPosition = oldJumpPrintEndPosition;
            //             }

            //         }
            //         WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
            //             rootElement.__DCWriterReference.invokeMethod(
            //                 "SetViewZoomRate",
            //                 oldZoomRate);
            //             //转成图片
            //             if (allCanvas && allCanvas.length > 0) {
            //                 for (var z = 0; z < allCanvas.length; z++) {
            //                     var canvas = allCanvas[z];
            //                     //console.log(canvas);
            //                     var imgStr = canvas.toDataURL("image/png", 1);
            //                     imgDatas.push(imgStr);
            //                     canvas.remove();
            //                 }
            //                 callBack && callBack(imgDatas);
            //                 imgDatas = null;
            //                 rootElement.__DCWriterReference.invokeMethod("RefreshViewAfterPrint", true);
            //             }
            //         });
            //     }
            // } catch (err) { console.log(err); }

            DCEventInterfaceLogFunction(rootElement, 'PrintAsImage', startTime);
        };

        /**
        * @name SetTableCellPadding
        * @type function
        * @apinameZh 兼容四代设置单元格的内边距SetTableCellSettings
        * @classification table
        * @param ["cellElement","object","表格对象","","CurrentTableCell返回值",true]
        * @param ["options","object","参数","","paddingLeft左内边距、<br/>paddingTop上内边距、<br/>paddingRight右内编辑、<br/>paddingBottom下内边距，<br/>单位可能是百分之一英寸",true]
        * @returns ["result","boolean","操作是否成功"]
        * @describe 注意接口名称为防止歧义，已修改为SetTableCellPadding
        */
        rootElement.SetTableCellPadding = function (cellElement, options) {
            var startTime = new Date();
            let result = null;

            if (DCTools20221228.IsDotnetReferenceElement(cellElement) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableCellPadding", cellElement, options);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableCellPadding2", cellElement, options);
            }
            //result = rootElement.__DCWriterReference.invokeMethod("SetTableCellPadding", cellElement, options);
            DCEventInterfaceLogFunction(rootElement, 'SetTableCellPadding', startTime);
            return result;
        };

        /**
        * @name ShowFooterLine
        * @type function
        * @apinameZh 在页脚的开头显示水平线的功能
        * @classification file
        */
        rootElement.ShowFooterLine = function () {
            var startTime = new Date();
            let result = null;
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//阅读、预览、续打、区域选择时，不能修改dom
            if (IsOperateDOM) {
                result = rootElement.__DCWriterReference.invokeMethod("ShowFooterLine");
            }
            DCEventInterfaceLogFunction(rootElement, 'ShowFooterLine', startTime);
            return result;
        };


        /**
         * @name UpdateAllRuleView
         * @type function
         * @apinameZh 将WriterControl_Rule.InvalidateAllView()封装为api供外部调用
         * @returns ["result","boolean","操作是否成功"]
         * @classification file
         */
        rootElement.UpdateAllRuleView = function () {
            var startTime = new Date();
            let result = null;
            result = WriterControl_Rule.InvalidateAllView(rootElement);
            DCEventInterfaceLogFunction(rootElement, 'UpdateAllRuleView', startTime);
            return result;
        };


        /**
       * @name SetSubDocumentBorderByID
       * @type function
       * @apinameZh 设置指定病程的边框
       * @classification subdoc
       * @param ["id","string","病程编号","","",true]
       * @param ["options","object","边框属性对象","","<br />borderleft:是否显示左侧边框<br />bordertop:是否显示上部边框<br />borderright:是否显示右侧边框<br />borderbottom:是否显示下部边框<br />borderwidth:边框宽度<br />bordercolor:边框颜色<br />borderstyle:线型，来源于枚举DashStyle<br />paddingleft:左内边距<br />paddingtop:上内边距<br />paddingright:右内边距<br />paddingbottom:下内边距<br />",true]
       * @param ["isSetNextTop","Boolean","是否需要设置下一个病程的上边框","","",true]
       * @param ["isExcludeOthersBorder","Boolean","是否取消其他病程的边框","","",true]
       * @param ["isAllRefresh","Boolean","是否全文刷新","true","",true]
       * @returns ["result","boolean","操作是否成功"]
       * @change ["2024-01-29","增加一个参数：是否全文刷新，默认值是true","liixnyu" ]
       */
        rootElement.SetSubDocumentBorderByID = function (id, options, isSetNextTop, isExcludeOthersBorder, isAllRefresh = true) {
            var startTime = new Date();
            if (options == null) {
                return false;
            }
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("SetSubDocumentBorderByID", id, options, isSetNextTop, isExcludeOthersBorder, isAllRefresh);
            DCEventInterfaceLogFunction(rootElement, 'SetSubDocumentBorderByID', startTime);
            return result;
        };


        /**
        * @name ShowAllSubDocumentBorder
        * @type function
        * @apinameZh 是否显示所有病程的边框
        * @classification subdoc
        * @param ["isShow","boolean","是否显示","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.ShowAllSubDocumentBorder = function (isShow) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);//判断是否可用操作dom
            var readonly = rootElement.Readonly;//只读模式下阻止追加子文档
            if (IsOperateDOM || readonly === false) {
                let result = false;
                result = rootElement.__DCWriterReference.invokeMethod("ShowAllSubDocumentBorder", isShow);
                DCEventInterfaceLogFunction(rootElement, 'ShowAllSubDocumentBorder', startTime);
                return result;
            }
            return false;
        };

        /**
        * @name ReturnLastCaretPosition
        * @type function
        * @apinameZh 返回最近的一次光标展示位置
        * @classification cursor
        * @returns ["result","object","当前光标的位置信息"]
        * @describe  <br />返回当前光标的位置信息  <br />canvasElement:光标所在的canvas元素<br />intPageIndex:光标所在的canvas在编辑器的下标<br />intDX:相对于canvas元素的x偏移量<br />intDY:相对于canvas元素的y偏移量
        */
        rootElement.ReturnLastCaretPosition = function () {
            var pageElement = WriterControl_Paint.GetCanvasElementByPageIndex(rootElement, rootElement.oldCaretOption.intPageIndex);
            return {
                canvasElement: pageElement,
                intPageIndex: rootElement.oldCaretOption.intPageIndex,
                intDX: rootElement.oldCaretOption.intDX,
                intDY: rootElement.oldCaretOption.intDY,
            };
        };


        /**
        * @name EditorRefreshViewElement
        * @type function
        * @apinameZh 刷新指定元素
        * @classification structuralelement
        * @param ["ELEMENT","object","指定元素的ID/NativeHandle/后台引用对象","","",true]
        */
        rootElement.EditorRefreshViewElement = function (tagElement) {
            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(tagElement) === true) {
                rootElement.__DCWriterReference.invokeMethod("EditorRefreshViewElementByElement", tagElement);
            } else {
                rootElement.__DCWriterReference.invokeMethod("EditorRefreshViewElementByID", tagElement);
            }
            DCEventInterfaceLogFunction(rootElement, 'EditorRefreshViewElement', startTime);
        };

        /**
       * @name MoveToSelectionSrart
       * @type function
       * @classification cursor
       * @apinameZh 光标定位在选择内容的最前面
       */
        rootElement.MoveToSelectionSrart = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("MoveToSelectionSrart");
            DCEventInterfaceLogFunction(rootElement, 'MoveToSelectionSrart', startTime);
        };

        /**
        * @name MoveToTableCellTag
        * @type function
        * @apinameZh 定位单元格的相对位置
        * @classification cursor
        * @param ["obj","object","相对数据","","<br />tableid:表格ID，字符串类型<br />rowindex:行序号，数字类型<br />colindex:列序号，数字类型<br />tag:位置,字符串类型：CellHome单元格内部前面、CellEnd单元格内部后面、PreCell前一个单元格、AfterCell后一个单元格、BeforeTable表格前、AfterTable表格后<br />",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.MoveToTableCellTag = function (options) {
            var startTime = new Date();
            if (options == null) {
                return false;
            }
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("MoveToTableCellTag", options);
            DCEventInterfaceLogFunction(rootElement, 'MoveToTableCellTag', startTime);
            return result;
        };


        /**
        * @name GetAllSubdocumentsBorderAndBackgroundColor
        * @type function
        * @apinameZh 获取所有病程的所有边框和背景颜色信息
        * @classification subdoc
        * @param ["isClear","boolean","是否需要清除背景色和边框","","",true]
        * @returns ["result","object","返回json数组，包含Bold：是否显示边框；LetterSpacing：边框Spacing；BorderStyle：边框样式；BorderWidth：边框宽度；BackgroundColor：背景颜色；BorderLeft：是否显示左边框；BorderLeftColor：左边框颜色；BorderRight：是否显示有边框；BorderRightColor：右边框颜色；BorderTop：是否显示上边框；BorderTopColor：上边框颜色；BorderBottom：是否显示下边框；BorderBottomColor：下边框颜色；"]
        * @describe 一般把返回值传给 SetAllSubdocumentsBorderAndBackgroundColor
        */
        rootElement.GetAllSubdocumentsBorderAndBackgroundColor = function (isClear) {
            var startTime = new Date();
            if (isClear == null) {
                isClear = true;
            }
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetAllSubdocumentsBorderAndBackgroundColor", isClear);
            DCEventInterfaceLogFunction(rootElement, 'GetAllSubdocumentsBorderAndBackgroundColor', startTime);
            return result;
        };

        /**
       * @name SetAllSubdocumentsBorderAndBackgroundColor
       * @type function
       * @classification subdoc
       * @apinameZh 设置所有病程的所有边框和背景颜色信息
       * @param ["options","object","json数组","null","json数组，包含Bold：是否显示边框；LetterSpacing：边框Spacing；BorderStyle：边框样式；BorderWidth：边框宽度；BackgroundColor：背景颜色；BorderLeft：是否显示左边框；BorderLeftColor：左边框颜色；BorderRight：是否显示有边框；BorderRightColor：右边框颜色；BorderTop：是否显示上边框；BorderTopColor：上边框颜色；BorderBottom：是否显示下边框；BorderBottomColor：下边框颜色；",true]
       * @returns ["result","boolean","操作是否成功"]
       * @describe 参数来源于GetAllSubdocumentsBorderAndBackgroundColor
       */
        rootElement.SetAllSubdocumentsBorderAndBackgroundColor = function (options) {
            var startTime = new Date();
            let result = false;
            let formatKeyArr = ["letterspacing", "borderstyle", "borderwidth", "backgroundcolor", "borderleft", "borderleftcolor", "borderright", "borderrightcolor", "bordertop", "bordertopcolor", "borderbottom", "borderbottomcolor"];

            if (Array.isArray(options)) {
                for (var i = 0; i < options.length; i++) {
                    Object.keys(options[i]).forEach(item => {
                        if (item.toLowerCase() !== 'id') {
                            if (typeof options[i][item] === 'number' || typeof options[i][item] === 'boolean' && formatKeyArr.indexOf(item.toLowerCase()) > -1) {
                                options[i][item] = `${options[i][item]}`;
                            }
                        }
                    });
                }
            } else {
                Object.keys(options).forEach(item => {
                    if (item.toLowerCase() !== 'id') {
                        if (typeof options[item] === 'number' || typeof options[item] === 'boolean' && formatKeyArr.indexOf(item.toLowerCase()) > -1) {
                            options[item] = `${options[item]}`;
                        }
                    }
                });
            }

            result = rootElement.__DCWriterReference.invokeMethod("SetAllSubdocumentsBorderAndBackgroundColor", options);
            DCEventInterfaceLogFunction(rootElement, 'SetAllSubdocumentsBorderAndBackgroundColor', startTime);
            return result;
        };

        /**
        * @name InComplexViewMode
        * @type function
        * @classification vestige
        * @apinameZh 判断是否处于留痕模式
        * @returns ["result","boolean","是否处于留痕模式"]
        */
        rootElement.InComplexViewMode = function () {
            var startTime = new Date();
            var ReadViewMode = rootElement.ReadViewMode;//阅读模式
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            if (ReadViewMode || IsPrintPreview) {
                //阅读预览模式下禁止调用
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("InComplexViewMode");
            DCEventInterfaceLogFunction(rootElement, 'InComplexViewMode', startTime);
            return result;
        };

        /**
       * @name InCleanViewMode
       * @type function
       * @classification vestige
       * @apinameZh 判断是否处于清洁视图模式
       * @returns ["result","boolean","是否处于清洁视图模式"]
       */
        rootElement.InCleanViewMode = function () {
            var startTime = new Date();
            var ReadViewMode = rootElement.ReadViewMode;//阅读模式
            var IsPrintPreview = rootElement.IsPrintPreview();//预览模式
            if (ReadViewMode || IsPrintPreview) {
                //阅读预览模式下禁止调用
                return false;
            }
            let result = rootElement.__DCWriterReference.invokeMethod("InCleanViewMode");
            DCEventInterfaceLogFunction(rootElement, 'InCleanViewMode', startTime);
            return result;
        };

        /**
        * @name InsertRowsToPageBottomByAfterRowHasNewPage
        * @type function
        * @apinameZh 在指定行下面插入空行,并延伸到页面底端 
        * @classification file
        * @param ["id","string","表格编号","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.InsertRowsToPageBottomByAfterRowHasNewPage = function (id) {
            var startTime = new Date();
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("InsertRowsToPageBottomByAfterRowHasNewPage", id);
            DCEventInterfaceLogFunction(rootElement, 'InsertRowsToPageBottomByAfterRowHasNewPage', startTime);
            return result;
        };


        /**
        * @name IsRowHasNewPage
        * @type function
        * @classification table
        * @apinameZh 判断表格是否存在换页
        * @param ["id","string","表格编号","","",true]
        * @returns ["result","boolean","是否存在换页"]
        */
        rootElement.IsRowHasNewPage = function (id) {
            var startTime = new Date();
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("IsRowHasNewPage", id);
            DCEventInterfaceLogFunction(rootElement, 'IsRowHasNewPage', startTime);
            return result;
        };

        /**
        * @name SelectCurrentElement
        * @type function
        * @apinameZh 选中当前元素
        * @classification structuralelement
        * @returns ["result","boolean","是否选中"]
        */
        rootElement.SelectCurrentElement = function () {
            var startTime = new Date();
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("SelectCurrentElement");
            DCEventInterfaceLogFunction(rootElement, 'SelectCurrentElement', startTime);
            return result;
        };

        /**
        * @name WriteDataFromDataSourceToDocumentSpecifyContainer
        * @type function
        * @apinameZh 对指定容器元素绑定数据源
        * @classification datasource
        * @param ["element","string","容器元素的ID或nativehandle"]
        * @param ["parameterNames","string","指定绑定的数据源名称"]
        * @param ["dataobj","object","指定绑定的数据集对象"]
        * @returns ["result","number","绑定数据的个数"]
        * @change ["2023-11-21","创建API","wyc" ]
        * @change ["2024-7-22","新增dataobj参数","wyc" ]
        */
        rootElement.WriteDataFromDataSourceToDocumentSpecifyContainer = function (element, parameterNames, dataobj) {
            var startTime = new Date();
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("WriteDataFromDataSourceToDocumentSpecifyContainer", element, parameterNames, dataobj);
            DCEventInterfaceLogFunction(rootElement, 'WriteDataFromDataSourceToDocumentSpecifyContainer', startTime);
            return result;
        };

        /**
        * @name InsertXmlStringByStyle
        * @type function
        * @apinameZh 通过指定的样式在当前位置插入XML
        * @classification file
        * @param ["parameter","string","要插入的文档内容xml字符串","","五代接口只接收一个参数其它的忽略",true]
        * @param ["styleObj","object","样式属性对象","","<br />Bold:是否粗体<br />ColorString:字体颜色字符串<br />FontName:字体名称字符串<br />FontSize:字体大小<br />",true]
        * @param ["isSetNextTop","Boolean","是否需要设置下一个病程的上边框","","",true]
        * @returns ["result","boolean","操作是否成功"]
        * @change ["2023-11-22","创建API","刘帅" ]
        */
        rootElement.InsertXmlStringByStyle = function (parameter, styleObj) {
            var startTime = new Date();
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("InsertXmlStringByStyle", parameter, styleObj);
            DCEventInterfaceLogFunction(rootElement, 'InsertXmlStringByStyle', startTime);
            return result;
        };

        /**
        * @name InsertXmlByIdUseCurrentStyle
        * @type function
        * @classification file
        * @apinameZh 在指定的输入域内插入文档,并使用当前插入点的样式
        * @param ["obj","object","要插入的文档内容xml片段属性","","{ file: arr,//xml文档 format: xml,//xml 文档格式base64: false,//是否是base64字符串logundo: false, //是否记录撤销操作position:{start:在目标容器的头部插入，end:在目标容器的尾部插入}}",true]
        * @param ["id","string","要插入的容器元素的ID","","",true]
        * @returns ["result","Boolean","操作是否成功"]
        * @change ["2023-11-22","增加与四代编辑器兼容的接口","刘帅" ]
        */
        rootElement.InsertXmlByIdUseCurrentStyle = function (obj, id) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("InsertXmlByIdUseCurrentStyle", obj, id);
            DCEventInterfaceLogFunction(rootElement, 'InsertXmlByIdUseCurrentStyle', startTime);
            return result;
        };

        /**
        * @name GetDocumentDomStructure
        * @type function
        * @apinameZh 解析文档的DOM结构层次
        * @classification file
        * @param ["xmlString","string","文档内容，允许为空，空表示使用当前文档","","",false]
        * @param ["format","string","文档格式，允许为空，空表示使用XML格式","","",false]
        * @returns ["result","object","文档的DOM结构对象"]
        * @change ["2023-11-23","GetDocumentDomStructure","刘帅" ]
        */
        rootElement.GetDocumentDomStructure = function (xmlString, format) {
            var startTime = new Date();
            if (xmlString == null) {
                xmlString = "";
            }
            if (format == null) {
                format = "";
            }
            let result = rootElement.__DCWriterReference.invokeMethod("GetDocumentDomStructure", xmlString, format);

            console.log("GetDocumentDomStructure", result);

            DCEventInterfaceLogFunction(rootElement, 'GetDocumentDomStructure', startTime);
            return result;
        };

        /**
         * @name SetSelectTableCellGridLineInfo
         * @type function
         * @apinameZh 设置选中多个单元格网格线
         * @param ["options","object","网格线属性"]
         * @returns ["result","Boolean","是否成功"]
         * @classification table
         * @change ["2023-11-28","增加同时设置选中多个单元格网格线的方法","lxy" ]
         */
        rootElement.SetSelectTableCellGridLineInfo = function (options) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("SetSelectTableCellGridLineInfo", options);
            DCEventInterfaceLogFunction(rootElement, 'SetSelectTableCellGridLineInfo', startTime);
            return result;
        };

        /**
        * @name GetSelectTableCells
        * @type function
        * @apinameZh 获取所有选择的单元格
        * @returns ["result","Array","多选的单元格数组对象"]
        * @classification table
        * @change ["2023-11-28","获取多选的单元格数组对象","lxy" ]
        */
        rootElement.GetSelectTableCells = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetSelectTableCells");
            DCEventInterfaceLogFunction(rootElement, 'GetSelectTableCells', startTime);
            return result;
        };

        /**
        * @name AverageTableRows
        * @type function
        * @apinameZh 平均分布各行
        * @returns ["result","Boolean","平均分布各行"]
        * @classification table
        * @change ["2023-11-28","增加平均分布各行","lxy" ]
        */
        rootElement.AverageTableRows = function (options = '') {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("AverageTableRows", options);
            DCEventInterfaceLogFunction(rootElement, 'AverageTableRows', startTime);
            result && rootElement.RefreshDocument();
            return result;
        };

        /**
        * @name AverageTableColumns
        * @type function
        * @apinameZh 平均分布各列
        * @returns ["result","Boolean","平均分布各列"]
        * @classification table
        * @change ["2023-11-28","增加平均分布各列","lxy" ]
        */
        rootElement.AverageTableColumns = function (options = '') {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("AverageTableColumns", options);
            DCEventInterfaceLogFunction(rootElement, 'AverageTableColumns', startTime);
            result && rootElement.RefreshDocument();
            return result;
        };

        /**
        * @name FocusNextInput
        * @type function
        * @apinameZh 聚焦下一个输入域的接口
        * @param ["isEditor","Boolean","参数表示在聚焦时，是否启动编辑激活功能","false","在编辑激活时，如果是下拉或时间输入域，会自动显示下拉列表或时间选择框",false]
        * @returns ["result","Boolean","操作是否成功"]
        * @classification structuralelement
        * @change ["2023-11-28","增加聚焦下一个输入域的接口","lxy" ]
        */
        rootElement.FocusNextInput = function (isEditor = false) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("FocusNextInput", isEditor);
            DCEventInterfaceLogFunction(rootElement, 'FocusNextInput', startTime);
            return result;
        };

        /**
       * @name FocusPreviousInput
       * @type function
       * @apinameZh 聚焦前一个输入域的接口
       * @param ["isEditor","Boolean","参数表示在聚焦时，是否启动编辑激活功能","false","在编辑激活时，如果是下拉或时间输入域，会自动显示下拉列表或时间选择框",false]
       * @returns ["result","Boolean","操作是否成功"]
       * @classification structuralelement
       * @change ["2023-12-15","增加聚焦前一个输入域的接口","wyc" ]
       */
        rootElement.FocusPreviousInput = function (isEditor = false) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("FocusPreviousInput", isEditor);
            DCEventInterfaceLogFunction(rootElement, 'FocusPreviousInput', startTime);
            return result;
        };

        /**
        * @name ClearDocumentParameters
        * @type function
        * @apinameZh 清除文档数据源绑定参数
        * @classification file
        * @returns ["result","Boolean","操作是否成功"]
        * @change ["2023-11-29","创建API","wyc" ]
        */
        rootElement.ClearDocumentParameters = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("ClearDocumentParameters");
            DCEventInterfaceLogFunction(rootElement, 'ClearDocumentParameters', startTime);
            return result;
        };

        /**
        * @name GetCurrentLineElements
        * @type function
        * @apinameZh 获取当前行的元素
        * @classification file
        * @returns ["result","Array","当前行元素"]
        * @change ["2023-11-30","增加接口","lxy" ]
        */
        rootElement.GetCurrentLineElements = function () {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("GetCurrentLineElements");
            DCEventInterfaceLogFunction(rootElement, 'GetCurrentLineElements', startTime);
            return result;
        };

        /**
        * @name getHtmlByFileWithSource
        * @type function
        * @apinameZh 获取带数据源绑定后的预览HTML兼容四代接口
        * @classification datasource
        * @param ["options","object","{modeIsBase64:;xmlMode:;dataSourceName:;dataSource:;format:;}"]
        * @returns ["result","string","返回的预览HTML"]
        * @change ["2023-12-1","获取带数据源绑定后的预览HTML","wyc" ]
        */
        rootElement.getHtmlByFileWithSource = function (options) {
            var startTime = new Date();
            let result = rootElement.__DCWriterReference.invokeMethod("getHtmlByFileWithSource", options);
            DCEventInterfaceLogFunction(rootElement, 'getHtmlByFileWithSource', startTime);
            return result;
        };

        /**
       * @name GetElementTextByIDPro
       * @type function
       * @apinameZh 返回内容中带上标或下标的文本
       * @classification file
       * @param ["id","object","元素","","元素ID/NativeHandle/后台.NET引用对象",true]
       * @param ["includeBorderText","Boolean","是否包含前后边框","false","",false]
       * @param ["includeLabelUnitText","Boolean","是否包含标签和单位","false","",false]
       * @param ["ignoreDeleteElement","Boolean","是否忽略逻辑删除的元素","true","",false]
       * @param ["ignoreVisibleElement","Boolean","是否忽略隐藏的元素","true","",false]
       * @returns ["result","string","返回带上标或下标的文本"]
       * @change ["2023-12-7","增加返回内容中带上标或下标的文本接口","lxy" ]
       * @describe 不包含逻辑删除和隐藏的元素
       */
        rootElement.GetElementTextByIDPro = function (id, includeBorderText = false, includeLabelUnitText = false, ignoreDeleteElement = true, ignoreVisibleElement = true) {
            var startTime = new Date();
            let result = null;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementTextByIDPro2", id, includeBorderText, includeLabelUnitText, ignoreDeleteElement, ignoreVisibleElement);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetElementTextByIDPro", id, includeBorderText, includeLabelUnitText, ignoreDeleteElement, ignoreVisibleElement);
            }
            if (result && result.length) {
                result = result.replace(/<\/sup><sup>/g, "").replace(/<\/sub><sub>/g, "");
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementTextByIDPro', startTime);
            return result;
        };


        /**
       * @name InsertXmlByElementTag
       * @type function
       * @apinameZh 在指定元素前/后插入内容
       * @classification file
       * @param ["id","object","元素","","元素ID/NativeHandle/后台.NET引用对象",true]
       * @param ["content","string","XML或JSON内容","","",true]
       * @param ["format","string","文档格式，支持XML或JSON","","",true]
       * @param ["tag","string","表示位置","","支持after和befor",true]
       * @returns ["result","string","返回带上标或下标的文本"]
       * @change ["2023-12-7","增加在指定元素前/后插入内容接口","lxy" ]
       */
        rootElement.InsertXmlByElementTag = function (id, content, format, tag) {
            var startTime = new Date();
            let result = null;
            console.log(DCTools20221228.IsDotnetReferenceElement(id) === true);
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("InsertXMLbyEementTag2", id, content, format, tag);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("InsertXMLbyEementTag", id, content, format, tag);
            }

            DCEventInterfaceLogFunction(rootElement, 'InsertXmlByElementTag', startTime);
            return result;
        };

        /**
      * @name InsertXmlByElementTag
      * @type function
      * @apinameZh 在指定元素前/后插入内容
      * @classification file
      * @param ["id","object","元素","","元素ID/NativeHandle/后台.NET引用对象",true]
      * @param ["content","string","XML或JSON内容","","",true]
      * @param ["format","string","文档格式，支持XML或JSON","","",true]
      * @param ["tag","string","表示位置","","支持after和befor",true]
      * @returns ["result","string","返回带上标或下标的文本"]
      * @change ["2023-12-7","增加在指定元素前/后插入内容接口","lxy" ]
      */
        rootElement.InsertXmlByElementTag = function (id, content, format, tag) {
            var startTime = new Date();
            let result = null;
            console.log(DCTools20221228.IsDotnetReferenceElement(id) === true);
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("InsertXMLbyEementTag2", id, content, format, tag);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("InsertXMLbyEementTag", id, content, format, tag);
            }

            DCEventInterfaceLogFunction(rootElement, 'InsertXmlByElementTag', startTime);
            return result;
        };

        /**
      * @name BeginEditElementValue
      * @type function
      * @apinameZh 弹出输入域下拉框
      * @classification file
      * @param ["id","object","元素","","元素ID/NativeHandle/后台.NET引用对象",true]
      * @returns ["result","boolean","是否执行成功"]
      * @change ["2023-12-18","创建API","wyc" ]
      */
        rootElement.BeginEditElementValue = function (id) {
            var startTime = new Date();
            let result = false;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("BeginEditElementValue2", id);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("BeginEditElementValue", id);
            }
            DCEventInterfaceLogFunction(rootElement, 'BeginEditElementValue', startTime);
            return result;
        };

        /**
    * @name GetSpecifyContainerParameterValue
    * @type function
    * @apinameZh 直接获取指定容器元素的数据源回填数据集对象
    * @classification file
    * @param ["id","object","元素","","元素ID/NativeHandle",true]
    * @returns ["result","object","获取的数据集对象"]
    * @change ["2023-12-18","创建API","wyc" ]
    */
        rootElement.GetSpecifyContainerParameterValue = function (id) {
            var startTime = new Date();
            let result = false;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                //暂时不支持.NET后台引用对象
                //result = rootElement.__DCWriterReference.invokeMethod("BeginEditElementValue2", id);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetSpecifyContainerParameterValue", id);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetSpecifyContainerParameterValue', startTime);
            return result;
        };

        /**
      * @name SetElementTextByIDPro
      * @type function
      * @apinameZh 返回内容中带上标或下标的文本
      * @classification file
      * @param ["Element","object","元素","","元素ID/NativeHandle/后台.NET引用对象",true]
      * @param ["newText","string","新的文本内容","false","",true]
      * @param ["ignoreDeleteElement","Boolean","是否忽略逻辑删除的元素","true","",true]
      * @param ["ignoreVisibleElement","Boolean","是否忽略隐藏的元素","true","",true]
      * @returns ["result","string","返回是否成功"]
      */
        rootElement.SetElementTextByIDPro = function (id, newText, ignoreDeleteElement = true, ignoreVisibleElement = true) {
            var startTime = new Date();
            let result = null;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementTextByIDPro2", id, newText);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetElementTextByIDPro", id, newText, ignoreDeleteElement, ignoreVisibleElement);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetElementTextByIDPro', startTime);
            return result;
        };

        /**
        * @name SplitTableCellForRowLine
        * @type function
        * @apinameZh 根据当前行的网格线数量拆分单元格
        * @classification table
        * @param ["tableID","string","表格编号","","",true]
        * @param ["rowIndex","number","行的序号","","",true]
        * @param ["colIndex","number","列的序号","","",true]
        * @param ["isAllCell","boolean","是否是从指定行开始整列都拆分","","",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SplitTableCellForRowLine = function (tableID, rowIndex, colIndex, isAllCell) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            var readonly = rootElement.Readonly;
            if (IsOperateDOM === false || readonly) {
                //阅读、预览、续打、区域选择，只读时不能使用此接口
                return false;
            }
            var result = rootElement.__DCWriterReference.invokeMethod("SplitTableCellForRowLine", tableID, rowIndex, colIndex, isAllCell);
            DCEventInterfaceLogFunction(rootElement, 'SplitTableCellForRowLine', startTime);
            return result;
        };

        /**
         * @name UpdateDocumentAllStyle
         * @type function
         * @apinameZh 修改全文样式的功能
         * @classification file
         * @param ["style","object","style的样式对象","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.UpdateDocumentAllStyle = function (style) {
            var startTime = new Date();
            var result = rootElement.__DCWriterReference.invokeMethod("UpdateDocumentAllStyle", style);
            DCEventInterfaceLogFunction(rootElement, 'UpdateDocumentAllStyle', startTime);
            return result;
        };

        /**
         * @name ApplyCurrentEditorCallBack
         * @type function
         * @apinameZh 应用当前编辑器回调
         * @param ["newText","string","新的文本内容","","null",true]
         * @param ["newValue","string","新的文本值","","null",true]
         * @returns ["result","boolean","操作是否成功"]
         * @change ["2024-01-24","增加接口，解决时间输入域和数字输入域无法触发文档内容改变事件的问题","liixnyu" ]
         */
        rootElement.ApplyCurrentEditorCallBack = function (newText = null, newValue = null, oldText = null, containerID) {
            var startTime = new Date();
            containerID = containerID ? containerID : rootElement.id;
            var ctlRef = DCTools20221228.GetDCWriterReference(containerID);
            var result = ctlRef.invokeMethod("ApplyCurrentEditorCallBack", newText, newValue, oldText);
            // 触发文档内容变化事件
            var opt = {
                /** 触发事件类型 */
                TriggerType: "ApplyCurrentEditorCallBack"
            };
            WriterControl_Event.RaiseControlEvent(rootElement, "DocumentContentChanged", opt);
            DCEventInterfaceLogFunction(rootElement, 'ApplyCurrentEditorCallBack', startTime);
            return result;
        };

        /**
     * @name GetInputTextByIDIgnoreChildInput
     * @type function
     * @apinameZh 获取嵌套输入域的值，并忽略自输入域
     * @classification file
     * @param ["ELEMENT","object","文档元素对象",null,"{DCSoft.Writer.Dom.XTextElement}文档元素对象",true]
     * @param ["includeBorderText","boolean","是否包含边框","","",true]
     * @param ["includeLabelUnitText","Boolean","是否包含单位标签","","",true]
     * @param ["ignoreLogicDelete","Boolean","是否忽略逻辑删除掉的输入域","","",true]
     * @param ["ignoreVisible","Boolean","是否忽略隐藏的输入域","","",true]
     * @returns ["result","string","返回是否成功"]
     * @change ["2024-01-30","增加接口","liixnyu" ]
     * @describe 当第一个参数ELEMENT是后台引用对象时，接口只会读取前三个参数（ELEMENT, includeBorderText, includeLabelUnitText）
     */
        rootElement.GetInputTextByIDIgnoreChildInput = function (id, includeBorderText = false, includeLabelUnitText = false, ignoreLogicDelete = true, ignoreVisible = true) {
            var startTime = new Date();
            let result = null;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetInputTextByIDIgnoreChildInput2", id, includeBorderText, includeLabelUnitText);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetInputTextByIDIgnoreChildInput", id, includeBorderText, includeLabelUnitText, ignoreLogicDelete, ignoreVisible);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetInputTextByIDIgnoreChildInput', startTime);
            return result;
        };
        /**
          * @name SetInputTextByIDIgnoreChildInput
          * @type function
          * @apinameZh 在指定输入域的开始或结束位置插入文本,忽略子元素
          * @classification file
          * @param ["ELEMENT","object","文档元素对象",null,"{DCSoft.Writer.Dom.XTextElement}文档元素对象",true]
          * @param ["isWhere","string","指定的位置，包含start和end","","",true]
          * @param ["isClear","Boolean","是否清空内容，忽略子元素","","",true]
          * @param ["newText","Boolean","新的文本","","",true]
          * @returns ["result","string","返回是否成功"]
          * @change ["2024-01-30","增加接口","liixnyu" ]
          */
        rootElement.SetInputTextByIDIgnoreChildInput = function (id, isWhere, isClear, newText) {
            var startTime = new Date();
            let result = null;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetInputTextByIDIgnoreChildInput2", id, isWhere, isClear, newText);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetInputTextByIDIgnoreChildInput", id, isWhere, isClear, newText);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetInputTextByIDIgnoreChildInput', startTime);
            return result;
        };

        /**
         * @name SetCourseRecordByID
         * @type function
         * @apinameZh 根据编号修改病程信息
         * @classification file
         * @param ["id","string","病程对象的id","","",true]
         * @param ["options","onject","病程对象","","",true]
         * @param ["isRefresh","Boolean","是否全文刷新","","",true]
         * @returns ["result","string","返回是否成功"]
         * @change ["2024-01-30","增加接口","liixnyu" ]
         */
        rootElement.SetCourseRecordByID = function (id, options, isRefresh) {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("SetCourseRecordByID", id, options, isRefresh);
            DCEventInterfaceLogFunction(rootElement, 'SetCourseRecordByID', startTime);
            return result;
        };

        /**
         * @name SaveDocumentToPdfBase64String
         * @type function
         * @apinameZh 将文档保存成pdf的base64字符串
         * @classification file
         * @param ["callBack","function","回调函数，用于接收base64字符串","","",true]
         * @change ["2024-03-6","增加接口","lixinyu" ]
         */
        rootElement.SaveDocumentToPdfBase64String = function (callBack) {
            var startTime = new Date();
            WriterControl_Print.SaveLocalPDF(
                {
                    RootElement: rootElement,
                    CallBack: function (data) {
                        //将二进制转换为base64
                        DCconvertBinaryToBase64(data)
                            .then((base64String) => {
                                callBack(base64String);
                                DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToPdfBase64String', startTime);
                            })
                            .catch((error) => {
                                console.error(error);
                                DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToPdfBase64String', startTime);
                            });
                    }
                }
            );
        };

        /**
         * @name DownLoadFontsForLocalPDF
         * @type function
         * @apinameZh 为了SaveDocumentToPdfBase64String提高下载速度，可以提前下载本地PDF所需字体的异步函数。
         * @classification file
         * @param ["callBack","function","回调函数，可以提示当前文档下载PDF所需要的字体,第一个参数有值时是报错信息","","",true]
         * @change ["2024-06-29","增加接口","xuyiming" ]
         */
        rootElement.DownLoadFontsForLocalPDF = function (callBack) {
            // // 记录开始时间，用于后续计算下载所需的时间。
            // var startTime = new Date();
            // // 调用WriterControl_Print中的函数来下载本地PDF所需字体。
            // // 使用回调函数来记录操作完成的时间。
            // WriterControl_Print.DownLoadFontsForLocalPDF(rootElement, function () {
            //     if (!!callBack && typeof (callBack) == "function") {
            //         callBack();
            //     }
            //     // 下载完成后，调用DCEventInterfaceLogFunction记录操作开始和结束时间。
            //     DCEventInterfaceLogFunction(rootElement, 'DownLoadFontsForLocalPDF', startTime);
            // });

            // 【DUWRITER5_0-3028】
            // 定义一个Promise封装的下载函数
            function downloadFontsForLocalPDF(rootElement) {
                return new Promise((resolve, reject) => {
                    try {
                        // 尝试下载字体
                        WriterControl_Print.DownLoadFontsForLocalPDF(rootElement, function () {
                            // 成功下载后调用resolve
                            resolve();
                        });
                    } catch (error) {
                        // 下载过程中出现异常，调用reject
                        reject(error);
                    }
                });
            }
            // 记录开始时间
            const startTime = new Date();
            // 使用Promise重构异步操作
            downloadFontsForLocalPDF(rootElement).then(() => {
                // 成功回调
                if (!!callBack && typeof callBack === "function") {
                    callBack();
                }
                // 记录日志
                DCEventInterfaceLogFunction(rootElement, 'DownLoadFontsForLocalPDF', startTime);
            }).catch((error) => {
                // 错误处理
                // console.error("下载字体失败:", error);
                if (!!callBack && typeof callBack === "function") {
                    callBack(error); // 将错误传递给回调函数处理
                }
            });
        };

        //将二进制转换为base64的方法
        function DCconvertBinaryToBase64(binaryData) {
            return new Promise((resolve, reject) => {
                try {
                    // 创建一个Blob对象
                    var blob = new Blob([binaryData], { type: 'application/pdf' });

                    // 创建一个FileReader对象
                    var reader = new FileReader();

                    // 读取Blob对象
                    reader.readAsDataURL(blob);

                    // 当读取完成时
                    reader.onload = function () {
                        // 将结果以Base64字符串的形式输出
                        var base64String = reader.result.split(',')[1];
                        resolve(base64String);
                    };
                } catch (error) {
                    reject(error);
                }
            });
        }

        /**
         * @name TestLoadDocument
         * @type function
         * @apinameZh 编辑器生成的xml数据或者json数据进行判断数据格式是否正常
         * @classification file
         * @param ["content","string","文档字符串","","",true]
         * @param ["format","string","文档格式","","",true]
         * @param ["isBase64","boolean","是否为base64字符串","","",true]
         * @returns ["result","string","返回是否成功"]
         * @change ["2024-03-06","增加接口","liixnyu" ]
         */
        rootElement.TestLoadDocument = function (content, format, isBase64 = false) {
            var startTime = new Date();
            let result = null;
            format = format.toLocaleLowerCase();
            if (format === 'xml' && isBase64 === false) {
                result = rootElement.IsValidateXML(content);
                if (result) {
                    result = rootElement.__DCWriterReference.invokeMethod("TestLoadDocument", content, format, isBase64);
                }
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("TestLoadDocument", content, format, isBase64);
            }
            DCEventInterfaceLogFunction(rootElement, 'TestLoadDocument', startTime);
            return result;
        };

        /**
          * @name GetCanModify
          * @type function
          * @apinameZh 判断指定元素能否修改
          * @classification file
          * @param ["ELEMENT","object","文档元素对象",null,"{DCSoft.Writer.Dom.XTextElement}文档元素对象",true]
          * @returns ["result","string","返回是否成功"]
          * @change ["2024-03-14","增加接口","liixnyu" ]
          */
        rootElement.GetCanModify = function (id) {
            var startTime = new Date();
            let result = false;
            if (DCTools20221228.IsDotnetReferenceElement(id) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("GetCanModify2", id);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("GetCanModify", id);
            }
            DCEventInterfaceLogFunction(rootElement, 'GetCanModify', startTime);
            return result;
        };

        /**
         * @name GetSubDocumentDomStructure
         * @type function
         * @apinameZh 获取到某一个病程记录中所有的结构化数据
         * @classification file
         * @param ["xmlString","string","病程的文档","null","不传默认为当前文档","false"]
         * @param ["format","string","病程文档格式","xml","不传默认为xml格式","false"]
         * @param ["sundocId","string","病程的某一ID","","必传，表示病程的ID。文档中不允许重复病程ID，如果重复，获取第一个","true"]
         * @returns ["result","array","返回病程中所有的结构化元素"]
         * @change ["2024-04-11","增加接口","liixnyu" ]
         */
        rootElement.GetSubDocumentDomStructure = function (xmlString = null, format = null, sundocId) {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetSubDocumentDomStructure", xmlString, format, sundocId);
            DCEventInterfaceLogFunction(rootElement, 'GetSubDocumentDomStructure', startTime);
            return result;
        };

        /**
         * @name SetDocumentPartProperties
         * @type function
         * @apinameZh 设置页眉、正文、页脚属性的功能
         * @classification file
         * @param ["part","string","包含body正文、header页眉、footer页脚。","全文档","如果为空字符串，表示页眉、正文、页脚一起设置。","false"]
         * @param ["parameter","object","设置的属性","null","attributes自定义属性、contentreadonly表示是否只读（包含True,False,Inherit）、accepttab是否接受Tab、visible是否可见、printvisibility打印是否可见、acceptchildelementtypes2可接受的数据格式（包含None、Text 、Field 、InputField 、Table 、TableRow、TableColumn 、TableCell 、Object 、LineBreak 、PageBreak 、ParagraphFlag 、CheckRadioBox 、CheckBox 、Image 、Document 、Button、Container 、All）、deleteable是否允许删除","false"]
         * @param ["sundocId","string","病程的某一ID","","必传，表示病程的ID。文档中不允许重复病程ID，如果重复，获取第一个","true"]
         * @change ["2024-04-16","增加接口","liixnyu" ]
         */
        rootElement.SetDocumentPartProperties = function (part, parameter) {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("SetDocumentPartProperties", part, parameter);
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentPartProperties', startTime);
            return result;
        };

        /**
         * @name GetJumpPrintInfo
         * @type function
         * @apinameZh 获取续打信息
         * @classification print
         * @change ["2024-04-25","增加接口","liixnyu" ]
         * @returns ["result","object","<br/>enabled是否启用<br/>mode打印模式，参数包含Disable,Normal,Offset,Append<br/>pageindex打印开始页序号<br/>position打印开始位置<br/>endpageindex打印结束页序号<br/>endposition打印结束位置"]
         */
        rootElement.GetJumpPrintInfo = function () {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetJumpPrintInfo");
            DCEventInterfaceLogFunction(rootElement, 'GetJumpPrintInfo', startTime);
            return result;
        };

        /**
        * @name SetJumpPrintInfo
        * @type function
        * @apinameZh 设置续打信息
        * @classification print
        * @param ["params","object","续打信息","","包含：enabled是否启用<br/>mode打印模式，参数包含Disable,Normal,Offset,Append<br/>pageindex打印开始页序号<br/>position打印开始位置<br/>endpageindex打印结束页序号<br/>endposition打印结束位置","true"]
        * @change ["2024-04-25","增加接口","liixnyu" ]
        * @returns ["result","Boolean","返回操作是否成功"]
        * @describe <span style="color:red;">需要注意的是：如果想修改enabled属性，只需要传enabled自身即可。例：{enabled:false}</span>。
        */
        rootElement.SetJumpPrintInfo = function (params) {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("SetJumpPrintInfo", params);
            rootElement.RefreshInnerView();
            DCEventInterfaceLogFunction(rootElement, 'SetJumpPrintInfo', startTime);
            return result;
        };

        /**
         * @name GetJumpPrintInfoForPrintPreview
         * @type function
         * @apinameZh 获取续打在打印预览下的信息
         * @classification print
         * @change ["2024-04-25","增加接口","liixnyu" ]
         * @returns ["result","object","<br/>enabled是否启用<br/>mode打印模式，参数包含Disable,Normal,Offset,Append<br/>pageindex打印开始页序号<br/>position打印开始位置<br/>endpageindex打印结束页序号<br/>endposition打印结束位置"]
         */
        rootElement.GetJumpPrintInfoForPrintPreview = function () {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetJumpPrintInfoForPrintPreview");
            DCEventInterfaceLogFunction(rootElement, 'GetJumpPrintInfoForPrintPreview', startTime);
            return result;
        };


        /**
        * @name SetJumpPrintInfoForPrintPreview
        * @type function
        * @apinameZh 设置打印预览下的续打信息
        * @classification print
        * @param ["params","object","续打信息","","包含：enabled是否启用<br/>pageindex打印开始页序号<br/>position打印开始位置<br/>endpageindex打印结束页序号<br/>endposition打印结束位置","true"]
        * @change ["2024-04-25","增加接口","liixnyu" ]
        * @returns ["result","Boolean","返回操作是否成功"]
        * @describe <span style="color:red;">需要注意的是：如果想修改enabled属性，只需要传enabled自身即可。例：{enabled:false}</span>。打印预览下不支持修改mode属性
        */
        rootElement.SetJumpPrintInfoForPrintPreview = function (params) {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("SetJumpPrintInfoForPrintPreview", params);
            rootElement.RefreshInnerView();
            DCEventInterfaceLogFunction(rootElement, 'SetJumpPrintInfoForPrintPreview', startTime);
            return result;
        };

        /**
       * @name GetElementTextByIDInSub
       * @type function
       * @apinameZh 获取指定病程下的指定元素的文本
       * @classification GetElementTextByIDInSub
       * @param ["id","string","元素","","元素ID",true]
       * @param ["options","object","属性","","{ IncludeBackgroundText: false, IncludeBorderText: false, IncludeHiddenText: false, IncludeLabelUnitText: false, IncludeLogicDeletedContent: false }",true]
       * @param ["subID","string","病程","","病程ID",true]
       * @returns ["result","string","元素的文本"]
       * @describe 该接口支持参数 document.WriterControl.GetElementTextByIDInSub('txtAge', { IncludeBackgroundText: false, IncludeBorderText: false, IncludeHiddenText: false, IncludeLabelUnitText: false, IncludeLogicDeletedContent: false },'subid1');，IncludeBackgroundText表示是否包含背景文字，IncludeBorderText是否保存边框文字，IncludeHiddenText是否包含隐藏的文本，IncludeLabelUnitText是否包含单位文本，IncludeLogicDeletedContent是否包含逻辑删除内容
       * 
       */
        rootElement.GetElementTextByIDInSub = function (id, options, subID) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择不能使用此接口
                return false;
            }
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetElementTextByIDInSub", id, options, subID);

            DCEventInterfaceLogFunction(rootElement, 'GetElementTextByIDInSub', startTime);
            return result;
        };

        /**
       * @name SetElementTextByIDInSub
       * @type function
       * @apinameZh 设置文档元素文本内容
       * @classification SetElementTextByIDInSub
       * @param ["id","string","文档元素编号","","元素编号",true]
       * @param ["text","string","文本值","","",true]
       * @param ["subID","string","病程编号","","病程编号",true]
       * @param ["withTrackInfos","Boolean","是否留痕","","是否留痕",true]
       * @returns ["result","boolean","操作是否成功"]
       */
        rootElement.SetElementTextByIDInSub = function (id, text, subID, withTrackInfos) {
            var startTime = new Date();
            var IsOperateDOM = DCGetAllowOperateDOM(rootElement);
            if (IsOperateDOM === false) {
                //阅读、预览、续打、区域选择不能使用此接口
                return false;
            }
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("SetElementTextByIDInSub", id, text, subID, withTrackInfos);

            DCEventInterfaceLogFunction(rootElement, 'SetElementTextByIDInSub', startTime);
            return result;
        };

        /**
       * @name GetStandardImage
       * @type function
       * @apinameZh 获取系统的内置图标数据
       * @classification GetStandardImage
       * @param ["id","string","图标的枚举数据","","图标的枚举数据",true]
       * @returns ["result","string","图片base64数据"]
       */
        rootElement.GetStandardImage = function (key) {
            var startTime = new Date();
            let result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetStandardImage", key);
            DCEventInterfaceLogFunction(rootElement, 'GetStandardImage', startTime);
            return result;
        };

        /**
       * @name SetRegisterCode
       * @type function
       * @apinameZh 动态设置注册码
       * @classification SetRegisterCode
       * @param ["code","string","注册码","","注册码",true]
       * @returns ["result","bool","是否执行完成"]
       */
        rootElement.SetRegisterCode = function (code) {
            var startTime = new Date();
            if (code == null) {
                code = "";
            }
            let result = false;
            result = rootElement.__DCWriterReference.invokeMethod("SetRegisterCode", code);
            DCEventInterfaceLogFunction(rootElement, 'SetRegisterCode', startTime);
            return result;
        };

        /**
       * @name NavigateByNodeID
       * @type function
       * @apinameZh 定位到指定目录位置
       * @classification file
       * @param ["id","string","目录id","true","","false"]
       */
        rootElement.NavigateByNodeID = function (id) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("NavigateByNodeID", id);
            var divCaret = rootElement.ownerDocument.getElementById("divCaret20221213");
            if (divCaret.style && divCaret.style && divCaret.style.display === 'none') {
                rootElement.Focus();
            }
            DCEventInterfaceLogFunction(rootElement, 'ShowTemperatureAboutDialog', startTime);
            return result;
        };

        /**
       * @name SortTableRowsByDateTime
       * @type function
       * @apinameZh 对表格指定列按照日期时间排序
       * @classification file
       * @param ["tableID","string","表格id","true","","false"]
       * @param ["startRowIndex","string","开始的表格行的序号。表格行从0开始排序号。","true","","false"]
       * @param ["endRowIndex","string","结束的表格行的序号。当为-1时表示到最后一行。","true","","false"]
       * @param ["dateColunmn","string","日期列的序号。表格列从0开始排序号。","true","","false"]
       * @param ["timeColumn","string","时间列的序号。表格列从0开始排序号。","true","","false"]
       */
        rootElement.SortTableRowsByAttributes = function (tableID, startRowIndex, endRowIndex, options, order) {
            var startTime = new Date();
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(tableID) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SortTableRowsByAttributes2", tableID, startRowIndex, endRowIndex, options, order);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SortTableRowsByAttributes", tableID, startRowIndex, endRowIndex, options, order);
            }

            DCEventInterfaceLogFunction(rootElement, 'SortTableRowsByAttributes', startTime);
            return result;
        };

        /**
      * @name RemoveElementAttributes
      * @type function
      * @apinameZh 移除指定的元素的指定的自定义属性
      * @classification file
      * @param ["element","string","元素id","true","","false"]
      * @param ["options","string","自定义属性列表。","true","","false"]
      */
        rootElement.RemoveElementAttributes = function (element, options) {
            var startTime = new Date();
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(element) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("RemoveElementAttributes2", element, options);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("RemoveElementAttributes", element, options);
            }
            DCEventInterfaceLogFunction(rootElement, 'RemoveElementAttributes', startTime);
            return result;
        };

        /**
         * @name SetParagraphStyleByElement
         * @type function
         * @apinameZh 移除指定的元素的指定的自定义属性
         * @classification file
         * @param ["element","string","元素id","true","","false"]
         * @param ["options","string","style。","true","","false"]
         */
        rootElement.SetParagraphStyleByElement = function (element, options) {
            var startTime = new Date();
            var result = false;
            if (DCTools20221228.IsDotnetReferenceElement(element) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetParagraphStyleByElement2", element, options);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetParagraphStyleByElement", element, options);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetParagraphStyleByElement', startTime);
            return result;
        };

        /**
        * @name DisplayAllVisibleElement
        * @type function
        * @apinameZh 显示所有的隐藏的元素
        */
        rootElement.DisplayAllVisibleElement = function (visileBackgroundColor = "yellow", runtimeVisibleBackgroundColor = "yellow") {
            var startTime = new Date();
            var result = false;
            result = rootElement.__DCWriterReference.invokeMethod("DisplayAllVisibleElement", visileBackgroundColor, runtimeVisibleBackgroundColor);
            DCEventInterfaceLogFunction(rootElement, 'DisplayAllVisibleElement', startTime);
            return result;
        };

        /**
        * @name HiddenAllVisibleElement
        * @type function
        * @apinameZh 隐藏所有的之前隐藏的元素
        */
        rootElement.HiddenAllVisibleElement = function () {
            var startTime = new Date();
            var result = false;
            result = rootElement.__DCWriterReference.invokeMethod("HiddenAllVisibleElement");
            DCEventInterfaceLogFunction(rootElement, 'HiddenAllVisibleElement', startTime);
            return result;
        };
        /**
       * @name ClearOldVisibleElements
       * @type function
       * @apinameZh 清空缓存数据
       */
        rootElement.ClearOldVisibleElements = function () {
            var startTime = new Date();
            rootElement.__DCWriterReference.invokeMethod("ClearOldVisibleElements");
            DCEventInterfaceLogFunction(rootElement, 'ClearOldVisibleElements', startTime);
        };


        /**
         * @name setToolBarVisibility
         * @type function
         * @apinameZh 设置是否显示工具条
         * @classification view
         * @param ["visibilityFlag","bool","工具条是否显示","","","false"]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SetToolBarVisibility = function (visible) {
            var startTime = new Date();
            let result = true;
            if (visible) {
                WriterControl_ToolBar.ShowSelf(rootElement);
            } else {
                WriterControl_ToolBar.HiddenSelf(rootElement);
            }
            DCEventInterfaceLogFunction(rootElement, 'setToolBarVisibility', startTime);
            return result;
        };



        /**
         * @name setDataWithDataSources
         * @type function
         * @apinameZh 数据源绑定新接口 DUWRITER5_0-3003
         * @classification view
         * @param ["parent","object","父元素容器","","","null"]
         * @param ["jsonobj","object","绑定的数据集对象","","","null"]
         * @returns ["result","number","成功处理的个数"]
         */
        rootElement.setDataWithDataSources = function (parent, jsonobj) {
            var startTime = new Date();
            if (typeof (jsonobj) !== "object") {
                return;
            }
            var desclist = rootElement.GetDataSourceBindingDescriptionsJSON();
            function processobj(dataobj, key) {
                var childObj = dataobj[key];
                if (typeof (childObj) !== "object" || childObj === null) {
                    return;
                }
                if (Object.keys(childObj).length === 2 && childObj["Text"] && childObj["Value"]) {
                    //找到绑定TEXT/VALUE的地方了，需要特殊处理，假设文档中只设置了bindingpath,需要自己补一个pathfortext
                    for (var j = 0; j < desclist.length; j++) {
                        var item = desclist[j];
                        if (item.BindingPath === key) {
                            if (item.BindingPathForText != null && item.BindingPathForText.length > 0) {
                                dataobj[item.BindingPathForText] = dataobj[key].Text;
                                dataobj[key] = dataobj[key].Value;
                            } else {
                                var pathnamefortext = key + "_FORTEXT";
                                var opt = {
                                    ValueBinding: {
                                        BindingPathForText: pathnamefortext
                                    }
                                };
                                rootElement.SetElementProperties(item.Element, opt);
                                dataobj[pathnamefortext] = dataobj[key].Text;
                                dataobj[key] = dataobj[key].Value;
                            }
                        }
                    }


                } else {
                    for (var i in childObj) {
                        processobj(childObj, i);
                    }
                }
            }

            for (var i in jsonobj) {
                processobj(jsonobj, i);
            }
            var result = null;
            if (parent != null) {
                result = rootElement.WriteDataFromDataSourceToDocumentSpecifyContainer(parent, null, jsonobj);
            } else {
                result = rootElement.WriteDataFromDataSourceToDocument(jsonobj);
            }
            DCEventInterfaceLogFunction(rootElement, 'setDataWithDataSources', startTime);
            return result;
        };

        /**
         * @name getDataWithDataSources
         * @type function
         * @apinameZh 数据源提取新接口 DUWRITER5_0-3003
         * @classification view
         * @param ["parent","object","父元素容器","","","null"]
         * @param ["specifyNames","array","指定提取的数据源名称集合","","","null"]
         * @returns ["result","object","数据源提取出的数据集对象"]
         */
        rootElement.getDataWithDataSources = function (parent, specifyNames) {
            var startTime = new Date();
            var jsonobj = rootElement.GetSpecifyContainerParameterValue(parent);
            if (jsonobj == null) {
                return null;
            }
            var desclist = rootElement.GetDataSourceBindingDescriptionsJSON();

            function processobj(dataobj, key) {
                var childObj = dataobj[key];
                if (typeof (childObj) === "object" && childObj != null &&
                    (Object.keys(childObj).length === 2 && childObj["Text"] && childObj["Value"]) === false) {
                    for (var i in childObj) {
                        processobj(childObj, i);
                    }
                }
                if (typeof (childObj) !== "string") {
                    return;
                }

                for (var j = 0; j < desclist.length; j++) {
                    var item = desclist[j];
                    if (item.BindingPath === key && item.BindingPathForText != null &&
                        dataobj[item.BindingPathForText] != null) {
                        var obj = {
                            Text: dataobj[item.BindingPathForText],
                            Value: dataobj[key]
                        };
                        dataobj[key] = obj;
                        dataobj[item.BindingPathForText] = undefined;
                        return;
                    }
                }

            }

            var legalParaNames = [];
            if (Array.isArray(specifyNames) === true) {
                legalParaNames = specifyNames;
            } else if (typeof (specifyNames) === "string") {
                legalParaNames = specifyNames.split(',');
            }
            var resultobj = new Object();
            for (var i in jsonobj) {
                if (legalParaNames.length == 0 || (legalParaNames.length > 0 && legalParaNames.indexOf(i) >= 0)) {
                    processobj(jsonobj, i);
                    resultobj[i] = jsonobj[i];
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'getDataWithDataSources', startTime);
            return resultobj;
        };

        /**
         * @name GetInputFieldsByInputNameInSub
         * @type function
         * @apinameZh 获取指定病程下指定名称的输入域
         * @classification subdoc
         * @param ["subID","string","病程的编号","true","",""]
         * @param ["fieldName","string","输入域的name","true","",""]
         * @returns ["result","array","指定病程下指定名称的输入域集合"]
         */
        rootElement.GetInputFieldsByInputNameInSub = function (subID, fieldName) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetInputFieldsByInputNameInSub", subID, fieldName);
            DCEventInterfaceLogFunction(rootElement, 'getDataWithDataSources', startTime);
            return result;
        };

        /**
         * @name SortRootContent
         * @type function
         * @apinameZh 对文档根元素列表进行排序
         * @classification subdoc
         * @param ["sortFunction","function","排序的规则函数","true","",""]
         * @returns ["result","bool","是否修改完成"]
         */
        rootElement.SortRootContent = function (sortFunction) {
            // [DUWRITER5_0-3714] 20241023 lxy 保留一次痕迹的文档选项
            var securityOptionsArray = {
                "EnablePermission": null,
                "EnableLogicDelete": null,
                "ShowLogicDeletedContent": null,
                "ShowPermissionMark": null,
                "ShowPermissionTip": null,
            };
            Object.keys(securityOptionsArray).forEach(function (key) {
                var value = rootElement.DocumentOptions.SecurityOptions[key];
                securityOptionsArray[key] = value;
            });

            // [DUWRITER5_0-3714] 20241023 lxy  禁用痕迹的文档选项 防止排序记录到痕迹中
            Object.keys(securityOptionsArray).forEach(function (key) {
                rootElement.DocumentOptions.SecurityOptions[key] = false;
            });
            rootElement.ApplyDocumentOptions();

            var list = new Array();
            var nodes = rootElement.SubDocuments;
            for (var iCount = 0; iCount < nodes.length; iCount++) {
                list.push(rootElement.GetElementProperties(nodes[iCount]));

            }
            list.sort(sortFunction);
            var changed = false;
            for (var iCount = 0; iCount < list.length; iCount++) {
                if (list[iCount].NativeHandle != nodes[iCount]) {
                    changed = true;
                    break;
                }
            }

            if (changed == true) {
                //对病程进行移动
                for (var i = 0; i < list.length; i++) {
                    //跟新nodes顺序
                    nodes = rootElement.SubDocuments;
                    //找到目标id旧的index
                    var oldIndex = nodes.indexOf(list[i].NativeHandle);
                    while (oldIndex > i) {
                        //移动元素
                        rootElement.SetSubDocumentUp(list[i].ID);
                        oldIndex--;
                    }
                }

                //while (rootNode.firstChild != null) {
                //    rootNode.removeChild(rootNode.firstChild);
                //}
                //for (var iCount = 0; iCount < list.length; iCount++) {
                //    rootNode.appendChild(list[iCount]);
                //}

                // [DUWRITER5_0-3714] 20241023 lxy 恢复痕迹为排序前的状态
                Object.keys(securityOptionsArray).forEach(function (key) {
                    rootElement.DocumentOptions.SecurityOptions[key] = securityOptionsArray[key];
                });
                rootElement.ApplyDocumentOptions();

                return true;
            }
            else {
                return false;
            }
        };

        /**
         * @name SetElementCheckedByName
         * @type function
         * @apinameZh 通过name值给一组单选框或者复选框进行勾选或取消勾选
         * @classification structuralelement
         * @param ["name","string","单复选框的name","true","",""]
         * @param ["tagValues","string","单复选框的value值集合，用逗号分隔","true","",""]
         */
        rootElement.SetElementCheckedByName = function (name, tagValues) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("SetElementCheckedByName", name, tagValues);
            DCEventInterfaceLogFunction(rootElement, 'SetElementCheckedByName', startTime);
            return result;
        };

        /**
         * @name SetTableRowUp
         * @type function
         * @apinameZh 表格行上移
         * @classification structuralelement
         * @param ["tableID","ELEMENT","表格id、NativeHandle或Element对象","true","",""]
         * @param ["rowIndex","number","行序号，从0开始","true","",""]
         */
        rootElement.SetTableRowUp = function (tableID, rowIndex) {
            var startTime = new Date();
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(tableID) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableRowUp2", tableID, rowIndex);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableRowUp", tableID, rowIndex);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetTableRowUp', startTime);
            return result;
        };

        /**
         * @name SetTableRowDown
         * @type function
         * @apinameZh 表格行下移
         * @classification structuralelement
         * @param ["tableID","ELEMENT","表格id、NativeHandle或Element对象","true","",""]
         * @param ["rowIndex","number","行序号，从0开始","true","",""]
         */
        rootElement.SetTableRowDown = function (tableID, rowIndex) {
            var startTime = new Date();
            var result = null;
            if (DCTools20221228.IsDotnetReferenceElement(tableID) === true) {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableRowDown2", tableID, rowIndex);
            } else {
                result = rootElement.__DCWriterReference.invokeMethod("SetTableRowDown", tableID, rowIndex);
            }
            DCEventInterfaceLogFunction(rootElement, 'SetTableRowDown', startTime);
            return result;
        };

        /**
         * @name GetLineNumForPages
         * @type function
         * @apinameZh 获得每页所拥有的行数组成的JSON数组字符串
         * @classification structuralelement
         * @param ["specifyPageIndexString","string","指定页号","true","",""]
         * @change ["2024-08-24","新增指定页号并返回更多行对象的属性","wyc" ]
         * @returns ["result","string","获得每页所拥有的行数组成的JSON数组字符串"]
         */
        rootElement.GetLineNumForPages = function (specifyPageIndexString) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetLineNumForPages", specifyPageIndexString);
            DCEventInterfaceLogFunction(rootElement, 'GetLineNumForPages', startTime);
            return result;
        };

        /**
         * @name CreateTrendChartSvg
         * @type function
         * @apinameZh 创建护理记录单趋势图svg
         * @classification file
         */
        rootElement.CreateTrendChartSvg = function () {
            WriterControl_TrendChart.CreateTrendChart(rootElement);
        };

        /**
         * @name TrendChartDataSourceToDocument
         * @type function
         * @apinameZh 对护理记录单进行赋值
         * @classification file
         */
        rootElement.TrendChartDataSourceToDocument = function (data) {
            WriterControl_TrendChart.TrendChartDataSourceToDocument(rootElement, data);
        };
        /**
         * @name InsertImageSetTableRowAttr
         * @type function
         * @apinameZh 根据当前输入域对当前页所有的表格行设置属性
         * @classification file
         */
        rootElement.InsertImageSetTableRowAttr = function () {
            WriterControl_TrendChart.InsertImageSetTableRowAttr(rootElement);
        };

        /**
         * @name SetDefaultFont
         * @type function
         * @apinameZh 设置特点字符范围使用特定的字体
         * @classification 
         * @param ["startCharCode","number","开始字符值","true","",""]
         * @param ["endCharCode","number","结束字符值","true","",""]
         * @param ["strFontName","string","字体名称","true","",""]
         */
        rootElement.SetDefaultFont = function (startCharCode, endCharCode, strFontName) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("SetDefaultFont", startCharCode, endCharCode, strFontName);
            DCEventInterfaceLogFunction(rootElement, 'SetDefaultFont', startTime);
            return result;
        };

        /**
         * @name GetAllContentStyles
         * @type function
         * @apinameZh 获取文档中所有的格式，不包含默认格式
         * @classification 
         */
        rootElement.GetAllContentStyles = function () {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("GetAllContentStyles");
            DCEventInterfaceLogFunction(rootElement, 'GetAllContentStyles', startTime);
            return result;
        };
        /**
        * @name SetContentStylesByIndex
        * @type function
        * @apinameZh 设置特点字符范围使用特定的字体
        * @classification 
        * @param ["styleIndex","number","格式列表中的序号，具有唯一性","true","",""]
        * @param ["styleElement","json","样式的json对象，和设置元素样式相同的格式","true","",""]
        */
        rootElement.SetContentStylesByIndex = function (styleIndex, styleElement) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod("SetContentStylesByIndex", styleIndex, styleElement);
            DCEventInterfaceLogFunction(rootElement, 'SetContentStylesByIndex', startTime);
            return result;
        };


        //rootElement.refreshDocumentOptions();
        document.WriterControl = rootElement;
        if (rootElement.ownerDocument !== document) {
            rootElement.ownerDocument.WriterControl = rootElement;
        }

    },


    /**
     * 对根元素绑定一些方法供外面调用
     * @param {object} rootElement 后台文档对象的前端引用
     */
    BindDCWriterDocument: function (rootElement) {


        /**
         * @name SetElementProperties
         * @type function
         * @apinameZh 设置指定ID的元素的属性
         * @classification structuralelement
         * @param ["elementid","object","要修改的元素ID","","",true]
         * @param ["options","object","元素的属性集合对象","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.SetElementProperties = function (elementid, options) {
            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMSetElementProperties", elementid, options, false);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'SetElementProperties', startTime);
            return result;
        };

        /**
         * @name GetElementProperties
         * @type function
         * @apinameZh 设置指定ID的元素的属性
         * @classification structuralelement
         * @param ["elementid","object","要修改的元素ID","","",true]
         * @returns ["result","boolean","操作是否成功"]
         */
        rootElement.GetElementProperties = function (elementid) {
            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMGetElementProperties", elementid);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementProperties', startTime);
            return result;
        };

        /**
        * @name SetChildElements
        * @type function
        * @apinameZh 插入给定的元素列表到某个容器内
        * @classification structuralelement
        * @param ["targetEle","object","元素","","指定容器元素的ID",true]
        * @param ["newEles","object","指定插入的元素对象集合","","格式参考四代BS编程文档",true]
        * @param ["options","string","插入位置","","afterBegin 开始位置 beforeEnd 最后位置,beforebegin:前面插入, index 坐标",true]
        * @returns ["result","boolean","操作是否成功"]
        */
        rootElement.SetChildElements = function (targetEle, newEles, options) {
            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMSetChildElements", targetEle, newEles, options);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'SetChildElements', startTime);
            return result;
        };

        /**
        * @name SetDocumentParameterValue
        * @type function
        * @classification file
        * @param { string } name 数据源名称。
        * @param { object } Value 数据集实体对象。
        * @apinameZh 纯DOM设置数据源绑定
        */
        rootElement.SetDocumentParameterValue = function (name, Value) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMSetDocumentParameterValue", name, Value);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'SetDocumentParameterValue', startTime);
            return result;
        };

        /**
        * @name GetDocumentParameterValue
        * @type function
        * @classification file
        * @param { string } name 数据源名称。
        * @apinameZh 纯DOM提取数据源
        */
        rootElement.GetDocumentParameterValue = function (name) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMGetDocumentParameterValue", name);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'GetDocumentParameterValue', startTime);
            return result;
        };

        /**
        * @name WriteDataFromDocumentToDataSource
        * @type function
        * @classification file
        * @param { string } name 数据源名称。
        * @apinameZh 纯DOM提取数据源
        */
        rootElement.WriteDataFromDocumentToDataSource = function () {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMWriteDataFromDocumentToDataSource");
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'WriteDataFromDocumentToDataSource', startTime);
            return result;
        };

        /**
        * @name WriteDataFromDataSourceToDocument
        * @type function
        * @classification file
        * @param { object } jsonObj 绑定的实体数据集对象。
        * @apinameZh 纯DOM绑定数据源
        */
        rootElement.WriteDataFromDataSourceToDocument = function (jsonObj) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMWriteDataFromDataSourceToDocument", jsonObj);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'WriteDataFromDataSourceToDocument', startTime);
            return result;
        };

        /**
        * @name LoadDocumentFromString
        * @type function
        * @classification file
        * @param { string } xmlstring 加载的文档XML字符串。
        * @apinameZh 加载文档字符串
        */
        rootElement.LoadDocumentFromString = function (xmlstring) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return false;
            }
            var result = false;
            //result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "LoadDCWriterControlDocumentFromString",
            //    rootElement, xmlstring);            
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                result = WriterControl_IO.LoadDocumentFromString({
                    WriterControl: rootElement.OwnerControl,
                    Data: xmlstring
                });
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'LoadDocumentFromString', startTime);
            return result;
        };

        /**
        * @name SaveDocumentToString
        * @type function
        * @classification file
        * @param { boolean } dispose 加载的文档XML字符串。
        * @apinameZh 将文档保存成字符串
        */
        rootElement.SaveDocumentToString = function (dispose = false) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return false;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "SaveDCWriterControlDocumentToString",
                rootElement);
            if (dispose === true) {
                rootElement.Dispose();
            }
            DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToString', startTime);
            return result;
        };

        /**
       * @name Dispose
       * @type function
       * @classification file
       * @apinameZh 释放文档对象资源
       */
        rootElement.Dispose = function () {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return false;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DisposeDCWriterControlDocument", rootElement);

            if (result === true) {
                if (rootElement.OwnerControl != null) {
                    rootElement.OwnerControl.parentElement.removeChild(rootElement.OwnerControl);
                }
                rootElement = null;
            }
            DCEventInterfaceLogFunction(rootElement, 'Dispose', startTime);
            return result;
        };

        /**
        * @name SetTableRowData
        * @type function
        * @classification file
        * @param { string } tableid 要处理的表格ID。
        * @param { number } rowindex 要处理的表格内行号，从0开始。
        * @param { object } rowdata 给表格行赋值的数据对象。
        * @apinameZh 将文档保存成字符串
        */
        rootElement.SetTableRowData = function (tableid, rowindex, rowdata) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return false;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMSetTableRowData",
                rootElement, tableid, rowindex, rowdata);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToString', startTime);
            return result;
        };

        /**
        * @name GetTableRowData
        * @type function
        * @classification file
        * @param { string } tableid 要处理的表格ID。
        * @param { number } rowindex 要处理的表格内行号，从0开始。
        * @apinameZh 获取表格指定行的数据
        */
        rootElement.GetTableRowData = function (tableid, rowindex) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMGetTableRowData",
                rootElement, tableid, rowindex);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToString', startTime);
            return result;
        };

        /**
        * @name GetTableData
        * @type function
        * @classification file
        * @param { string } tableid 要处理的表格ID。
        * @apinameZh 获取表格指定行的数据
        */
        rootElement.GetTableData = function (tableid) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMGetTableData",
                rootElement, tableid);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'SaveDocumentToString', startTime);
            return result;
        };


        /**
        * @name GetElementTextByID
        * @type function
        * @classification file
        * @param { string } elementid 要获取文本的元素ID。
        * @param { object } gettextargs 获取文本的参数对象。
        * @apinameZh 获取文档给定ID的元素的文本
        */
        rootElement.GetElementTextByID = function (elementid, gettextargs) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMGetElementTextByID",
                rootElement, elementid, gettextargs);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'GetElementTextByID', startTime);
            return result;
        };

        /**
        * @name SetElementTextByID
        * @type function
        * @classification file
        * @param { string } elementid 要设置文本的元素ID。
        * @param { string } text 设置元素的文本。
        * @apinameZh 设置文档给定ID的元素的文本
        */
        rootElement.SetElementTextByID = function (elementid, text) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMSetElementTextByID",
                rootElement, elementid, text);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'SetElementTextByID', startTime);
            return result;
        };

        /**
        * @name DeleteTableRow
        * @type function
        * @classification file
        * @param { string } tableid 要处理的表格ID。
        * @param { number } rowindex 要处理的表格内行号，从0开始。
        * @apinameZh 获取表格指定行的数据
        */
        rootElement.DeleteTableRow = function (tableid, rowindex) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMDeleteDocumentTableRow",
                rootElement, tableid, rowindex);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'DeleteTableRow', startTime);
            return result;
        };

        /**
        * @name InsertTableRow
        * @type function
        * @classification file
        * @param { string } tableid 要处理的表格ID。
        * @param { number } rowindex 要处理的表格内行号，从0开始。
        * @param { boolean } insertdown 要处理的表格内行号，从0开始。
        * @apinameZh 获取表格指定行的数据
        */
        rootElement.InsertTableRow = function (tableid, rowindex, insertdown = true) {

            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var result = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMInsertDocumentTableRow",
                rootElement, tableid, rowindex, insertdown);
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'InsertTableRow', startTime);
            return result;
        };


        /**
        * @name GetDocumentSpecifyPageImages
        * @type function
        * @apinameZh 兼容四代获取指定页码的图片数据
        * @classification structuralelement
        * @param ["options","object","获取参数属性","","json格式包含两个参数：ShowMarginLine是否显示页面编辑线；SpecifyPageIndexes指定的页码数",true]
        * @param ["callBack","function","回调函数","","用于接收接口返回的base64字符串",true]
        * @param ["required","Function","成功获得数据后的回调函数","","回调函数参数是一个字符串数组，为图片格式的数据",true]
        */
        rootElement.GetSpecifyPageImages = function (options, callBack) {
            var startTime = new Date();
            if (DCTools20221228.IsDotnetReferenceElement(rootElement) === false) {
                return null;
            }
            var bsData = DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "DOMGetSpecifyPageImages",
                rootElement, options);
            if (bsData != null && bsData.length > 10) {
                var reader = new DCBinaryReader(bsData);
                var totalPageCount = reader.ReadInt16();
                var cavas = document.createElement("CANVAS");
                var imgDatas = new Array();
                var outputPageCount = 0;
                function DrawOnePage() {
                    var bsPage = reader.ReadByteArray();
                    if (bsPage != null && bsPage.length > 0) {
                        var pageReader = new DCBinaryReader(bsPage);
                        if (pageReader.ReadByte() != 133) {
                            // 文件头不对
                            return;
                        }
                        var pageWidth = pageReader.ReadInt16();
                        var pageHeight = pageReader.ReadInt16();
                        cavas.width = pageWidth;
                        cavas.height = pageHeight;
                        var ctx = cavas.getContext("2d");
                        ctx.clearRect(0, 0, pageWidth, pageHeight);
                        ctx.resetTransform();
                        if (typeof (ctx.reset) == "function") {
                            ctx.reset();
                        }
                        var drawer = new PageContentDrawer(cavas, pageReader);
                        drawer.EventAfterDraw = function () {
                            outputPageCount++;
                            var strData = cavas.toDataURL("image/png", 1);
                            imgDatas.push(strData);
                            if (outputPageCount == totalPageCount) {
                                callBack && callBack(imgDatas);
                            }
                            else {
                                DrawOnePage();
                            }
                        };
                        drawer.AddToTask();
                    }
                }
                DrawOnePage();
            }
            if (rootElement.OwnerControl != null && rootElement.OwnerControl.style) {
                rootElement.OwnerControl.style.display = "none";
            }
            DCEventInterfaceLogFunction(rootElement, 'GetSpecifyPageImages', startTime);
        };
    },


    /**
     * 对时间轴根元素绑定一些方法供外面调用
     * @param {HTMLElement} rootElement 根元素对象
     * @param {object} refDCWriter DCWriterClass对象在JS中的代理
     */
    BindControlForTemperatureControlForWASM: function (rootElement, refDCWriter) {
        rootElement.__DCWriterReference = refDCWriter;
        rootElement.IsWriterPrintPreviewControlForWASM = false;

        /**
        * @name RemoteLoadTemperatureDocumentFromXMLString
        * @type function
        * @classification file
        * @param { string } xmlstr 加载的时间轴文档XML字符串
        * @param { function } callBack 回调函数，若设置，则异步请求四代服务，否则同步请求
        * @apinameZh 利用四代服务加载时间轴文档XML转换成JSON结构
        */
        rootElement.RemoteLoadTemperatureDocumentFromXMLString = function (xmlstr, callBack) {
            var startTime = new Date();
            var strServicePageUrl = DCTools20221228.GetServicePageUrl(rootElement);
            if (strServicePageUrl == null || strServicePageUrl.length == 0) {
                console.error("DCWriter:未配置ServicePageUrl,无法执行LoadTemperatureDocumentFromXMLString");
                DCEventInterfaceLogFunction(rootElement, 'LoadTemperatureDocumentFromXMLString', startTime);
                return false;
            }
            var strUrl = strServicePageUrl + "?dctimelineloaddocumentfromfrontend=1&sessionname=1&advancedmode=true&serviceflag=fdjia8324";
            var postData = "loaddocumentinfo=" + encodeURIComponent(xmlstr);
            var xhr = new XMLHttpRequest();
            var resultobj = null;
            var hascallback = typeof (callBack) === "function";
            xhr.open("POST", strUrl, hascallback);
            xhr.onload = function () {
                if (this.status == 200) {
                    //debugger;
                    var tempobj = JSON.parse(this.response);
                    if (tempobj.success === "true") {
                        resultobj = JSON.parse(tempobj.result);
                        if (typeof (callBack) == "function") {
                            callBack.call(rootElement, resultobj);
                        }
                    }
                    return;
                }
            };
            xhr.send(postData);
            return resultobj;
        };

        /**
        * @name RemoteSaveTemperatureDocumentToXMLString
        * @type function
        * @classification file
        * @param { object } jsonobj 时间轴文档的前端JSON结构
        * @param { function } callBack 回调函数，若设置，则异步请求四代服务，否则同步请求
        * @apinameZh 利用四代服务将前端时间轴文档JSON对象保存成XML字符串
        */
        rootElement.RemoteSaveTemperatureDocumentToXMLString = function (jsonobj, callBack) {
            var startTime = new Date();
            var strServicePageUrl = DCTools20221228.GetServicePageUrl(rootElement);
            if (strServicePageUrl == null || strServicePageUrl.length == 0) {
                console.error("DCWriter:未配置ServicePageUrl,无法执行LoadTemperatureDocumentFromXMLString");
                DCEventInterfaceLogFunction(rootElement, 'LoadTemperatureDocumentFromXMLString', startTime);
                return false;
            }
            var strUrl = strServicePageUrl + "?dctimelinesavedocumenttofrontend=1&sessionname=1&advancedmode=true&serviceflag=fdjia8324";
            var postData = "temperaturedocument=" + encodeURIComponent(JSON.stringify(jsonobj));
            var xhr = new XMLHttpRequest();
            var resultobj = null;
            var hascallback = typeof (callBack) === "function";
            xhr.open("POST", strUrl, hascallback);
            xhr.onload = function () {
                if (this.status == 200) {
                    //debugger;
                    var tempobj = null;
                    try {
                        tempobj = JSON.parse(this.response);
                    }
                    catch (e) {
                        console.log(this.response);
                    }
                    if (tempobj != null && tempobj.success === "true") {
                        resultobj = tempobj.result;
                        if (typeof (callBack) == "function") {
                            callBack.call(rootElement, resultobj);
                        }
                    }
                    return;
                }
            };
            xhr.send(postData);
            return resultobj;
        };

        /**
        * @name Dispose
        * @type function
        * @classification file
        * @param { boolean } disposing 如果应释放托管资源，为 true；否则为 false。
        * @apinameZh 清理所有正在使用的资源
        */
        rootElement.Dispose = function () {
            if (rootElement.__DCDisposed == true) {
                return;// 控件已经被销毁了，无需重复操作。
            }
            if (rootElement.ownerDocument != null) {
                DCTools20221228.ConsoleWarring("南京都昌公司建议先从HTML-DOM中删除DIV元素{" + rootElement.id + "},然后调用它的Dispose()方法，不正确的调用次序容易导致错误。");
            }
            var startTime = new Date();
            if (rootElement.__DCWriterReference != null) {
                rootElement.__DCWriterReference.invokeMethod("Dispose");
                rootElement.__DCWriterReference = null;
                rootElement.resizeObserver.disconnect(rootElement);
            }
            while (rootElement.firstChild != null) {
                rootElement.removeChild(rootElement.firstChild);
            }
            if (window.__DCWriterControls != null) {
                for (var strKey in window.__DCWriterControls) {
                    var ctl2 = window.__DCWriterControls[strKey];
                    if (ctl2 == rootElement) {
                        window.__DCWriterControls[strKey] = null;
                        break;
                    }
                }
            }
            rootElement.__DCDisposed = true;
            DCEventInterfaceLogFunction(rootElement, 'Dispose', startTime);

        };

        /**
        * @name SetZoomRate
        * @type function
        * @classification file
        * @param { number } 新的缩放值 
        * @apinameZh 修改页面大小
        */
        rootElement.SetZoomRate = function (newZoomRate) {
            WriterControl_DrawFu.SetZoomRate(rootElement, newZoomRate);
        };

        /**
        * @name MoveProjectUpAndDown
        * @type function
        * @classification file
        * @param { object|string } id 对应文本标签属性对象或者UID值
        * @apinameZh 对项目进行上移下移
         */
        rootElement.MoveProjectUpAndDown = function (id, direction) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能移动项目");
                return false;
            }
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = d.UID;
                } else {
                    id = null;
                }
            }
            //获取到元素
            if (!id) {
                return false;
            }
            direction = parseInt(direction);
            if (isNaN(direction)) {
                return false;
            }
            //循环拿到所有的
            var allType = ["HeaderLabels", "HeaderLines", "FooterLines", "YAxisInfos"];
            var config = rootElement.DocumentOptions.DefaultData;
            for (var i = 0; i < allType.length; i++) {
                var projectArr = config.Config[allType[i]];
                for (var j = 0; j < projectArr.length; j++) {
                    //判断是否存在UID或者name值
                    var thisProject = projectArr[j];
                    if (thisProject.UID == id || thisProject.Name == id) {
                        var lastIndex = j + direction;
                        //判断位置
                        if (lastIndex < 0) {
                            lastIndex = 0;
                        } else if (lastIndex > projectArr.length - 1) {
                            lastIndex = projectArr.length - 1;
                        }
                        //删除原位置
                        var remoevProject = projectArr.splice(j, 1);
                        projectArr.splice(lastIndex, 0, remoevProject[0]);

                        WriterControl_DrawFu.CreateTemperatureInit(rootElement, config, "EventTemperatureMoveProjectUpAndDown");
                        rootElement.InnerRaiseEvent("EventMoveProjectUpAndDown", thisProject, allType[i]);
                        return thisProject;
                    }
                }
            }

            return false;
        };

        /**
        * @name TemperatureFileNew
        * @type function
        * @classification file
        * @apinameZh 创建一个空白的时间轴页面
        */
        rootElement.TemperatureFileNew = function () {
            rootElement.isFileNew = true;
            WriterControl_DrawFu.CreateTemperatureInit(rootElement);
            rootElement.isFileNew = false;
        };

        /**
        * @name LoadTemperatureDocumentFromString
        * @type function
        * @classification file
        * @param { string } str 时间轴文档xml字符串。
        * @param { boolean } isLoadData 是否初始化加载数据  默认为true
        * @change ["2024-08-06","新增直接从JSON字符串加载","wyc" ]
        * @change ["2024-09-06","修改为直接用四代服务远端加载不再需要五代后台","wyc" ]
        * @apinameZh 加载时间轴文档xml，返回四代时间轴文档的BS前端JSON数据对象
        */
        rootElement.LoadTemperatureDocumentFromString = function (str, isLoadData = false) {
            var startTime = new Date();
            let res = null;
            //wyc20240806:判断是JSON字符串则直接解析
            if (str.startsWith && (str.startsWith('{') === true || str.startsWith('[') === true)) {
                res = JSON.parse(str);
            } else {
                res = TemperatureControl_XMLToJSON.GetJsonData(str);
            }
            rootElement.setAttribute("pageindex", "1");
            if (isLoadData === true) {
                res.Values = [];
            }

            WriterControl_DrawFu.CreateTemperatureInit(rootElement, res, "EventTemperatureDocumentLoad");

            DCEventInterfaceLogFunction(rootElement, 'LoadTemperatureDocumentFromString', startTime);
            return true;
        };

        /**
        * @name LoadTemperatureDocumentFromFile
        * @type function
        * @classification file
        * @param { boolean } isLoadData 是否初始化加载数据  默认为true
        * @apinameZh 打开本地时间轴文档xml
        */
        rootElement.LoadTemperatureDocumentFromFile = function (isSaveData) {
            var file = document.createElement('input');
            file.setAttribute('id', 'dcInputFile');
            file.setAttribute('type', 'file');
            file.setAttribute('accept', '.xml,.json,.rtf,.html,.htm,.odt');
            file.style.display = 'none';
            rootElement.appendChild(file);
            file.click();
            //file文件选中事件
            file.onchange = function () {
                var fileList = this.files;
                if (fileList.length > 0) {
                    //console.log(fileList[0]);
                    var reader = new FileReader();
                    reader.readAsText(fileList[0], "UFT-8");
                    reader.onload = function (e) {
                        //获取到文件内容
                        var strFileContent = e.target.result;
                        rootElement.LoadTemperatureDocumentFromString(strFileContent, isSaveData);
                    };
                }
            };
        };

        /**
        * @name SaveTemperatureDocumentToString
        * @type function
        * @classification file
        * @param { string } format 保存格式，默认为xml
        * @change ["2024-08-06","新增直接保存成JSON字符串","wyc" ]
        * @apinameZh 将BS前端的时间轴文档JSON数据对象保存成时间轴文档XML字符串
        */
        //* @param { boolean } isSaveData 是否需要单独保存数据
        rootElement.SaveTemperatureDocumentToString = function (format, callBack) {
            var startTime = new Date();
            var jsonobj = rootElement.DocumentOptions.DefaultData;
            //wyc20240806:新增保存成JSON字符串
            let res1 = null;
            if (format === "json") {
                res1 = JSON.stringify(jsonobj);
                if (typeof (callBack) == "function") {
                    callBack.call(rootElement, res1);
                }
            } else {
                //res1 = rootElement.__DCWriterReference.invokeMethod(window.DCWriterEntryPointAssemblyName, "SaveTemperatureDocumentToString", jsonobj);
                var srvurl = rootElement.getAttribute("servicepageurl");
                if ((srvurl == null || srvurl.length == 0) &&
                    window.__DCResourceBasePath != null &&
                    window.__DCResourceBasePath.indexOf) {
                    var index = window.__DCResourceBasePath.indexOf("?wasmres");
                    if (index > 0) {
                        var substr = window.__DCResourceBasePath.substring(0, index);
                        rootElement.setAttribute("servicepageurl", substr);
                    }
                }
                res1 = rootElement.RemoteSaveTemperatureDocumentToXMLString(jsonobj, callBack);
            }
            DCEventInterfaceLogFunction(rootElement, 'SaveTemperatureDocumentToString', startTime);
            return res1;
        };

        /**
        * @name SaveTemperatureDocumentToFile
        * @param { string } filename 文件名
        * @param { string } format 保存格式，默认为xml
        * @apinameZh 下载本地xml文件
        */
        // * @param { string } 文件名
        // * @param { strFormat } 文件类型
        rootElement.SaveTemperatureDocumentToFile = function (filename, format = 'xml') {
            format = format.trim().toLowerCase();
            var fileSuffix = format === 'xml' ? 'xml' : 'json';//文件格式
            if (format === 'xml') {
                var responseText = rootElement.SaveTemperatureDocumentToString(rootElement.DocumentOptions.DefaultData);
            } else if (format === 'json') {
                var responseText = JSON.stringify(rootElement.DocumentOptions.DefaultData);
            }
            var a = document.createElement('a');
            a.style.display = 'none';
            a.href = window.URL.createObjectURL(new Blob([responseText], { type: format === 'xml' ? "text/xml" : "application/json" }));
            var d = new Date();
            a.download = filename ? filename : d.getFullYear() + "" + ((d.getMonth() + 1) >= 10 ? (d.getMonth() + 1) : '0' + (d.getMonth() + 1)) + "" + d.getDate() + "" + d.getHours() + "" + d.getMinutes() + "" + d.getSeconds(); + '.' + fileSuffix;
            // 将a标签添加到body中是为了更好的兼容性，谷歌浏览器可以不用添加
            document.body.appendChild(a);
            a.click();
            // 移除
            a.remove();
            // 释放url
            window.URL.revokeObjectURL(a.href);
        };

        /**
        * @name SaveTemperatureDocumentToPDF
        * @type function
        * @classification file
        * @apinameZh 保存为pdf数据
        */
        rootElement.SaveTemperatureDocumentToPDF = function (callback) {
            var aa = new Date().getTime();
            //拿到页面宽度
            var pageSetting = rootElement.DocumentOptions.CalculationData;
            //初始化jsPDF实例
            const pdf = new jspdf.jsPDF('p', 'px', [pageSetting.PaperWidth, pageSetting.PaperHeight]);
            //获取到所有的svg页面
            var svgContainer = rootElement.querySelector("[dctype=page-container]");
            svgContainer = svgContainer.cloneNode(true);
            var allSvg = svgContainer.querySelectorAll("[dctype=page]");
            //清除样式
            for (var i = 0; i < allSvg.length; i++) {
                var cloneSvg = allSvg[i];
                cloneSvg.style.border = "none";
                var typesign = cloneSvg.querySelector("[dctype=typesign]");
                typesign.remove();
                var canvas = document.createElement('canvas');
                canvas.width = pageSetting.PaperWidth;
                canvas.height = pageSetting.PaperHeight;

                // 绘制Canvas
                var ctx = canvas.getContext('2d');
                var data = (new XMLSerializer()).serializeToString(cloneSvg);
                var DOMURL = window.URL || window.webkitURL || window;
                var img = new Image();
                var svgBlob = new Blob([data], { type: 'image/svg+xml;charset=utf-8' });
                var url = DOMURL.createObjectURL(svgBlob);
                img.index = i;
                img.onload = function () {
                    ctx.drawImage(this, 0, 0);
                    DOMURL.revokeObjectURL(url);

                    // 转换Canvas为PNG图片
                    var png = canvas.toDataURL('image/png');
                    if (this.index != 0) {
                        pdf.addPage();
                    }
                    //console.log(png)
                    pdf.addImage(png, 'png', 0, 0, pageSetting.PaperWidth, pageSetting.PaperHeight);// x, y, 宽, 高
                    if (this.index == allSvg.length - 1) {
                        var base64 = pdf.output("datauristring");
                        base64 = base64.replace(/filename=generated.pdf;/, "");
                        callback(base64);
                        //base64 = base64.substring(28);
                        //base64 = base64.replace(/[\n\r]/g, '');
                        //var raw = window.atob(base64);
                        //var rawLength = raw.length;
                        //var uint8Array = new Uint8Array(rawLength);
                        //for (var i = 0; i < rawLength; i++) {
                        //    uint8Array[i] = raw.charCodeAt(i);
                        //}
                        //base64 = new Blob([uint8Array], { type: 'application/pdf' });
                        //base64 = URL.createObjectURL(base64);
                        //var embed = document.createElement("embed");
                        //embed.setAttribute("src", base64);
                        //embed.setAttribute("type", "application/pdf");
                        //embed.style.width = "100%";
                        //embed.style.height = "100%";
                        //var myWindow = window.open('', '');
                        //myWindow.document.body.style.margin = 0;
                        //myWindow.document.body.style.padding = 0;
                        //myWindow.document.body.appendChild(embed);
                        //myWindow.focus();
                    }
                };
                img.src = url;
            }
        };

        /**
        * @name PrintTemperatureDocument
        * @type function
        * @classification file
        * @param { object } args 打印相关配置
        *                        PrintMode: "Normal" | "OddPage" | "EvenPage",// 默认Normal,打印模式，为一个字符串，可以为 Normal,OddPage,EvenPage。这里的页码是从1开始计算的
        *                        SpecifyPageIndexs: "1,3,6-11,12",//默认空，打印指定页码列表，页码从1开始计算，各个项目之间用逗号分开，如果项目中间有个横杠，表示一个页码范围
        *                        FromPage: 1, // 默认1，从1开始计算的打印开始页码
        *                        ToPage: 2,//默认为总页数
        *                                    
        * @apinameZh 下载本地xml文件
        */
        rootElement.PrintTemperatureDocument = function (args) {

            //[DUWRITER5_0-3731] 20241021 lxy 单页模式下支持打印多页
            var viewmode = rootElement.getAttribute("viewmode");
            var isSinglePage = '';//先保留一下状态
            var isPageindex = rootElement.getAttribute("pageindex");
            //判断是否为单页模式
            if (typeof viewmode == "string" && (viewmode.toLowerCase().trim() == "singlepage")) {
                isSinglePage = true;
                // //判断是否为打印多页
                // var isPrintMode = (args.PrintMode == "OddPage" || args.PrintMode == "EvenPage");
                // var isSpecifyPageIndexs = args.SpecifyPageIndexs && args.SpecifyPageIndexs.length > 0;
                // var isFromToPage = args.FromPage || args.ToPage;
                // //奇偶页、指定页码、指定页范围时，先设置成全部展示
                // if (isPrintMode || isSpecifyPageIndexs || isFromToPage) {
                // //打印指定页时，先设置成全部展示
                rootElement.SetTemperatureViewMode("page");
                // }
            }


            var iframe = rootElement.ownerDocument.getElementById(rootElement.id + "_IFrame_Print");
            if (iframe == null) {
                iframe = rootElement.ownerDocument.createElement("iframe");
                iframe.id = rootElement.id + "_IFrame_Print";
                iframe.style.position = "absolute";
                rootElement.appendChild(iframe);
                iframe.style.width = rootElement.offsetWidth + "px";
                iframe.style.height = rootElement.offsetHeight + "px";
                iframe.style.left = "0px";
                iframe.style.top = "0px";// (rootElement.offsetTop + 600) + "px";
                iframe.style.border = "1px solid blue";
                iframe.style.display = "";
                iframe.style.backgroundColor = "white";
                iframe.style.zIndex = 10000;
            }
            iframe.style.display = 'none';
            var targetDocument = iframe.contentDocument;
            targetDocument.open();
            targetDocument.write("");
            targetDocument.close();

            var styleElement = targetDocument.createElement("STYLE");
            styleElement.innerText = "@page{margin-left: 0px; margin-top: 0px; margin-right: 0px; margin-bottom: 0px;}";
            targetDocument.head.appendChild(styleElement);
            targetDocument.body.style.margin = "0px";
            targetDocument.title = " ";

            //获取到页面中的所有svg元素
            var allSvg = rootElement.querySelectorAll("[dctype=page]");
            if (allSvg && allSvg.length > 0) {
                var SpecifyPageNum = [];
                var isPage = rootElement.getAttribute("viewmode");
                if (typeof isPage == "string" && (isPage.toLowerCase().trim() == "page" || isPage.toLowerCase().trim() == "temperature")) {
                    isPage = null;
                }

                if (args && !isPage) {
                    if (args.PrintMode == "OddPage" || args.PrintMode == "EvenPage") {
                        for (var i = 1; i <= allSvg.length; i++) {
                            if (args.PrintMode == "OddPage") {
                                if (i % 2 == 1) {
                                    SpecifyPageNum.push(i);
                                }
                            } else if (args.PrintMode == "EvenPage") {
                                if (i % 2 == 0) {
                                    SpecifyPageNum.push(i);
                                }
                            }
                        }
                    } else if (args.SpecifyPageIndexs && args.SpecifyPageIndexs.length > 0) {
                        var thisIndex = args.SpecifyPageIndexs.split(",");
                        if (thisIndex && thisIndex.length > 0) {
                            for (var i = 0; i < thisIndex.length; i++) {
                                //先判断是否存在-
                                var pageIndex = thisIndex[i];
                                //是否存在数值
                                if (pageIndex.indexOf("-") >= 0) {
                                    //底部的数值
                                    var lastIndex = pageIndex.split("-");
                                    if (lastIndex && lastIndex.length == 2) {
                                        var firstLastIndex = parseInt(lastIndex[0]);
                                        var secondLastIndex = parseInt(lastIndex[1]);
                                        if (!isNaN(firstLastIndex) && !isNaN(secondLastIndex)) {
                                            if (secondLastIndex < firstLastIndex) {
                                                secondLastIndex = secondLastIndex + firstLastIndex;
                                                firstLastIndex = secondLastIndex - firstLastIndex;
                                                secondLastIndex = secondLastIndex - firstLastIndex;
                                            } else if (firstLastIndex == secondLastIndex) {
                                                SpecifyPageNum.push(firstLastIndex);
                                                continue;
                                            }
                                            for (var j = firstLastIndex; j <= secondLastIndex; j++) {
                                                SpecifyPageNum.push(j);
                                            }
                                        }
                                    }
                                } else {
                                    pageIndex = parseInt(pageIndex);
                                    if (!isNaN(pageIndex)) {
                                        SpecifyPageNum.push(pageIndex);
                                    }
                                }
                            }
                        }

                    } else if (args.FromPage || args.ToPage) {
                        if (args.FromPage) {
                            args.FromPage = parseInt(args.FromPage);
                            if (isNaN(args.FromPage)) {
                                args.FromPage = 0;
                            }
                        } else {
                            args.FromPage = 0;
                        }
                        if (args.ToPage) {
                            args.ToPage = parseInt(args.ToPage);
                            if (isNaN(args.ToPage)) {
                                args.ToPage = allSvg.length;
                            }
                        } else {
                            args.ToPage = allSvg.length;
                        }

                        if (args.FromPage > args.ToPage) {
                            args.FromPage = args.FromPage + args.ToPage;
                            args.ToPage = args.FromPage - args.ToPage;
                            args.FromPage = args.FromPage - args.ToPage;
                        } else if (args.FromPage == args.ToPage) {
                            SpecifyPageNum.push(args.FromPage);
                        }
                        for (var j = args.FromPage; j <= args.ToPage; j++) {
                            SpecifyPageNum.push(j);
                        }
                    }
                }
                if (SpecifyPageNum.length == 0) {
                    SpecifyPageNum = [...new Array(allSvg.length).keys()];
                    SpecifyPageNum.forEach((item, index) => {
                        SpecifyPageNum[index] = item + 1;
                    });
                }
                if (SpecifyPageNum.length > 0) {
                    //去重
                    SpecifyPageNum = Array.from(new Set(SpecifyPageNum));
                    //排序
                    SpecifyPageNum = SpecifyPageNum.sort((a, b) => {
                        return a - b;
                    });
                }
                for (var i = 1; i <= allSvg.length; i++) {
                    if (SpecifyPageNum.indexOf(i) < 0) {
                        continue;
                    }
                    var cloneSvg = allSvg[i - 1].cloneNode(true);
                    var hasText = cloneSvg.querySelector("[dctype='typesign']");
                    cloneSvg.style.border = "none";
                    cloneSvg.style.margin = "0px";
                    hasText && hasText.remove();
                    targetDocument.body.appendChild(cloneSvg);
                    //targetDocument.body.appendChild(targetDocument.createElement("BR"));
                }
            }
            iframe.contentWindow.onafterprint = function (e) {
                //console.log("打印完成", e);
                rootElement.InnerRaiseEvent("EventTemperatureAfterPrint", targetDocument);
                iframe.style.display = "none";
            };

            //打印前事件
            var result = rootElement.InnerRaiseEvent("EventTemperatureBeforePrint", targetDocument);
            if (result === false) {
                return iframe;
            }
            iframe.contentWindow.print();

            //[DUWRITER5_0-3731] 20241021 lxy 恢复单页展示
            if (isSinglePage) {
                rootElement.SetTemperatureViewMode("singlepage");
                rootElement.SetTemperaturePageIndex(isPageindex || '1');
            }
            return iframe;
        };

        /**
         * @name GetDocumentConfigProperties
         * @type function 
         * @classification file
         * @apinameZh 获取全局属性
         */
        rootElement.GetDocumentConfigProperties = function () {
            var config = rootElement.DocumentOptions.DefaultData.Config;
            config = JSON.parse(JSON.stringify(config));
            //将数据配置外数据清空
            var deleteArr = ["TagString", "HeaderLabels", "HeaderLines", "FooterLines", "YAxisInfos", "Labels"];
            deleteArr.forEach(item => {
                delete config[item];
            });
            return config;
        };

        /**
         * @name SetDocumentConfigProperties
         * @type function 
         * @classification file
         * @param { object } options 修改后的时间轴全局属性对象
         * @apinameZh 设置全局属性
         */
        rootElement.SetDocumentConfigProperties = function (option) {
            //如果不存在option直接退出
            if (!option || typeof option != "object" || Object.keys(option).length == 0) {
                return;
            }
            //对option内的HeaderLabels,HeaderLines,FooterLines,Labels,YAxisInfos进行处理
            var ignoreArr = ["HeaderLabels", "HeaderLines", "FooterLines", "Labels", "YAxisInfos"];
            for (var i = 0; i < ignoreArr.length; i++) {
                if (option[ignoreArr[i]]) {
                    delete option[ignoreArr[i]];
                }
            }
            //设置属性
            var config = {};
            if (rootElement && rootElement.DocumentOptions) {
                config = rootElement.DocumentOptions.DefaultData;

                for (var i in option) {
                    if (i == "PageSettings") {
                        var pageSetting = option[i];
                        for (var j in pageSetting) {
                            config.Config[i][j] = pageSetting[j];
                        }
                    } else {
                        config.Config[i] = option[i];
                    }

                }
            } else {
                config.Config = option;
            }
            //console.log(config);
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, config, "EventTemperatureUpdateDocumentConfig");
            rootElement.InnerRaiseEvent("EventStructureChanged", config.Config, "DocumentConfig");
        };

        /**
         * @name GetInternalProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应属性对象或者UID值
         * @param { string } type 对应内部对象的名称
         * @apinameZh 从编辑器内部获取对应对象的属性
         */
        rootElement.GetInternalProperties = function (id, type) {
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            if (!rootElement.DocumentOptions) {
                return;
            }
            var allLabel = [];
            if (type) {
                allLabel = rootElement.DocumentOptions.DefaultData.Config[type];
            } else {
                var allItem = ["HeaderLabels", "HeaderLines", "FooterLines", "YAxisInfos", "Labels"];
                allLabel = [];
                for (var i = 0; i < allItem.length; i++) {
                    //如果返回的不是数组则直接进入下一个循环
                    var thisItem = rootElement.DocumentOptions.DefaultData.Config[allItem[i]];
                    if (thisItem && !Array.isArray(thisItem)) {
                        continue;
                    }
                    allLabel = [...allLabel, ...thisItem];
                }
            }
            if (allLabel && allLabel.length > 0) {
                //返回所有的元素
                if (typeof id == "string") {
                    //找到对应的UID
                    for (var i = 0; i < allLabel.length; i++) {
                        if (allLabel[i].UID == id) {
                            return allLabel[i];
                        }
                    }
                } else if (id == null) {
                    return allLabel;
                }
            }
            return;
        };


        /**
         * @name GetLabelProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应文本标签属性对象或者UID值
         * @apinameZh 获取全部文本标签属性或者对应UID文本标签属性
         */
        rootElement.GetLabelProperties = function (id) {
            return rootElement.GetInternalProperties(id, "Labels");
        };

        /**
         * @name SetLabelProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应文本标签属性对象或者UID值
         * @param { object } option 修改后的文本标签属性对象
         * @apinameZh 修改对应UID文本标签属性
         */
        rootElement.SetLabelProperties = function (id, option) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能删除文本标签");
                return;
            }
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            //如果不存在option直接退出
            if (!id || !option) {
                return;
            }
            if (!rootElement || !rootElement.DocumentOptions || !rootElement.DocumentOptions.DefaultData) {
                return;
            }
            var hasChange = null;
            var changeData = null;
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData.Config && defaultData.Config.Labels && defaultData.Config.Labels.length > 0) {
                var label = defaultData.Config.Labels;
                hasChange = label.find((data, index) => {
                    if (data.UID == id) {
                        changeData = data;
                        for (var i in option) {
                            data[i] = option[i];
                        }
                        label[index] = data;
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
            rootElement.InnerRaiseEvent("EventStructureChanged", changeData, "Labels");
        };

        /**
         * @name AddLabel
         * @type function 
         * @classification file
         * @param { object } option 新增的文本标签属性对象
         * @apinameZh 新增文本标签属性
         */
        rootElement.AddLabel = function (option) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能新增文本标签");
                return;
            }
            //判断option是否存在
            if (!option || typeof option != 'object') {
                return;
            }
            var newLable = rootElement.NewTextLabel();
            for (var i in option) {
                newLable[i] = option[i];
            }
            //找到全局的config写入
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData) {
                var labels = defaultData.Config.Labels;
                labels.push(newLable);
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureAddLabels", newLable);
            rootElement.InnerRaiseEvent("EventStructureChanged", newLable, "Labels");
            return newLable;
        };

        /**
         * @name RemoveLabel
         * @type function 
         * @classification file
         * @param { boolean|object } id 对应文本标签属性对象或者UID值
         * @apinameZh 删除对应UID文本标签属性
         */
        rootElement.RemoveLabel = function (id) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能删除文本标签");
                return;
            }
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            //如果不存在option直接退出
            if (!id) {
                return;
            }
            var hasChange = null;
            var removeData = null;
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData.Config && defaultData.Config.Labels && defaultData.Config.Labels.length > 0) {
                var labels = defaultData.Config.Labels;
                hasChange = labels.find((data, index) => {
                    if (data.UID == id) {
                        removeData = data;
                        labels.splice(index, 1);
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureRemoveLabels", hasChange);
            rootElement.InnerRaiseEvent("EventStructureChanged", removeData, "Labels");
            return hasChange;
        };

        /**
         * @name GetHeaderLabelProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应眉栏属性对象或者UID值
         * @apinameZh 获取全部眉栏属性或者对应UID眉栏属性
         */
        rootElement.GetHeaderLabelProperties = function (id) {
            return rootElement.GetInternalProperties(id, "HeaderLabels");
        };

        /**
         * @name SetHeaderLabelProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应眉栏属性对象或者UID值
         * @param { object } option 修改后的眉栏属性对象
         * @apinameZh 修改对应UID眉栏属性
         */
        rootElement.SetHeaderLabelProperties = function (id, option) {
            //判断是否为设计模式
            //var dcType = rootElement.getAttribute("dctype");
            //if (dcType != "DCTemperatureDesignControlForWASM") {
            //    console.log("需要在设计模式下才能修改眉栏属性");
            //    return;
            //}
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            //如果不存在option直接退出
            if (!id || !option) {
                return;
            }
            if (!rootElement || !rootElement.DocumentOptions || !rootElement.DocumentOptions.DefaultData) {
                return;
            }
            var hasChange = null;
            var changeData = null;
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData.Config && defaultData.Config.HeaderLabels && defaultData.Config.HeaderLabels.length > 0) {
                var headerLabels = defaultData.Config.HeaderLabels;
                hasChange = headerLabels.find((data, index) => {
                    if (data.UID == id) {
                        changeData = data;
                        for (var i in option) {
                            data[i] = option[i];
                        }
                        headerLabels[index] = data;
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
            rootElement.InnerRaiseEvent("EventStructureChanged", changeData, "HeaderLabels");
        };

        /**
         * @name SetHeaderLabelChangeDate
         * @type function 
         * @param { object } options 键值对，键为目标小标题的UID，值为转科日期数组
         * @apinameZh 设置眉栏的转科转床信息
         */
        rootElement.SetHeaderLabelChangeDate = function (options) {
            if (options && Object.keys(options).length > 0) {
                var optionsKey = Object.keys(options);
                var changeData = [];

                var HeaderLabels = rootElement.DocumentOptions.DefaultData.Config.HeaderLabels;
                if (HeaderLabels && HeaderLabels.length) {
                    HeaderLabels.forEach(item => {
                        if (item.UID.indexOf(optionsKey) >= 0) {
                            changeData.push(item);
                        }
                    });
                }

                if (rootElement.DocumentOptions.DefaultData && rootElement.DocumentOptions.DefaultData.Config) {
                    rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate'] = JSON.parse(JSON.stringify(options));
                }
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
            rootElement.InnerRaiseEvent("EventStructureChanged", changeData, "HeaderLabels");
            return true;

        };

        /**
        * @name SetHeaderLabelChangeDateByUID
        * @type function 
        * @param { string } uid 目标小标题的UID
        * @param { object } options 键值对，键为目标小标题的UID，值为转科日期数组
        * @apinameZh 设置眉栏的转科转床信息
        */
        rootElement.SetHeaderLabelChangeDateByUID = function (uid, options) {
            if (options && Object.keys(options).length > 0) {
                var changeData = [];
                var HeaderLabels = rootElement.DocumentOptions.DefaultData.Config.HeaderLabels;
                if (HeaderLabels && HeaderLabels.length) {
                    HeaderLabels.forEach(item => {
                        if (item.UID == uid) {
                            changeData.push(item);
                        }
                    });
                }

                if (rootElement.DocumentOptions.DefaultData && rootElement.DocumentOptions.DefaultData.Config) {
                    if (!rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate']) {
                        rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate'] = {};
                    }
                    rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate'][uid] = {};
                    rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate'][uid] = JSON.parse(JSON.stringify(options));
                }
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
            rootElement.InnerRaiseEvent("EventStructureChanged", changeData, "HeaderLabels");
            return true;

        };
        /**
        * @name RemoveHeaderLabelChangeDateByUID
        * @type function 
        * @param { string } uid 目标小标题的UID
        * @apinameZh 删除单个眉栏的转科转床信息
        */
        rootElement.RemoveHeaderLabelChangeDateByUID = function (uid) {
            if (!uid) {
                return false;
            }
            var HeaderLabels = rootElement.DocumentOptions.DefaultData.Config.HeaderLabels;

            if (rootElement.DocumentOptions.DefaultData && rootElement.DocumentOptions.DefaultData.Config) {
                if (!rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate']) {
                    return false;
                }
                if (!rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate'][uid]) {
                    return false;
                }
                delete rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate'][uid];
            }

            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
            rootElement.InnerRaiseEvent("EventStructureChanged", HeaderLabels, "HeaderLabels");
            return true;

        };

        /**
        * @name RemoveHeaderLabelChangeDate
        * @type function 
        * @apinameZh 删除所有的眉栏转科转床信息
        */
        rootElement.RemoveHeaderLabelChangeDate = function () {
            var HeaderLabels = rootElement.DocumentOptions.DefaultData.Config.HeaderLabels;
            if (rootElement.DocumentOptions.DefaultData && rootElement.DocumentOptions.DefaultData.Config) {
                if (!rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate']) {
                    return false;
                }
                rootElement.DocumentOptions.DefaultData.Config['HeaderLabelChangeDate'] = null;

            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
            rootElement.InnerRaiseEvent("EventStructureChanged", HeaderLabels, "HeaderLabels");
            return true;

        };

        /**
         * @name AddHeaderLabel
         * @type function 
         * @classification file
         * @param { object } option 新增的眉栏属性对象
         * @apinameZh 新增眉栏属性
         */
        rootElement.AddHeaderLabel = function (option) {
            //判断是否为设计模式
            //var dcType = rootElement.getAttribute("dctype");
            //if (dcType != "DCTemperatureDesignControlForWASM") {
            //    console.log("需要在设计模式下才能新增眉栏");
            //    return;
            //}
            //判断option是否存在
            if (!option || typeof option != 'object') {
                return;
            }
            var newLable = rootElement.NewHeaderLabel();
            for (var i in option) {
                newLable[i] = option[i];
            }
            //找到全局的config写入
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData) {
                var headerLabels = defaultData.Config.HeaderLabels;
                headerLabels.push(newLable);
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureAddHeaderLabel", newLable);
            rootElement.InnerRaiseEvent("EventStructureChanged", newLable, "HeaderLabels");
            return newLable;
        };

        /**
         * @name RemoveHeaderLabel
         * @type function 
         * @classification file
         * @param { boolean|object } id 对应眉栏属性对象或者UID值
         * @apinameZh 删除对应UID眉栏属性
         */
        rootElement.RemoveHeaderLabel = function (id) {
            //判断是否为设计模式
            //var dcType = rootElement.getAttribute("dctype");
            //if (dcType != "DCTemperatureDesignControlForWASM") {
            //    console.log("需要在设计模式下才能删除眉栏");
            //    return;
            //}
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            //如果不存在option直接退出
            if (!id) {
                return;
            }
            var hasChange = null;
            var removeData = null;
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData.Config && defaultData.Config.HeaderLabels && defaultData.Config.HeaderLabels.length > 0) {
                var headerLabels = defaultData.Config.HeaderLabels;
                hasChange = headerLabels.find((data, index) => {
                    if (data.UID == id) {
                        removeData = data;
                        headerLabels.splice(index, 1);
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureRemoveHeaderLabel", hasChange);
            rootElement.InnerRaiseEvent("EventStructureChanged", removeData, "HeaderLabels");
            return hasChange;
        };

        /**
         * @name GetGeneralItemProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应一般项目属性对象或者UID值
         * @apinameZh 获取全部一般项目属性或者对应UID一般项目属性
         */
        rootElement.GetGeneralItemProperties = function (id) {
            return rootElement.GetInternalProperties(id, "HeaderLines");
        };

        /**
         * @name SetGeneralItemProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应一般项目属性对象或者UID值
         * @param { object } option 修改后的一般项目属性对象
         * @apinameZh 修改对应UID一般项目属性
         */
        rootElement.SetGeneralItemProperties = function (id, option) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能修改一般项目属性");
                return;
            }
            rootElement.SetHeaderFooterLineProperties(id, option, "HeaderLines");
        };

        /**
         * @name AddGeneralItem
         * @type function 
         * @classification file
         * @param { object } option 新增一般项目属性对象
         * @apinameZh 新增一般项目属性
         */
        rootElement.AddGeneralItem = function (option) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能新增一般项目");
                return;
            }
            //判断option是否存在
            if (!option || typeof option != 'object') {
                return;
            }
            var newLable = rootElement.NewTitleLine();
            for (var i in option) {
                newLable[i] = option[i];
            }
            //找到全局的config写入
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData) {
                var headerLines = defaultData.Config.HeaderLines;
                headerLines.push(newLable);
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureAddHeaderLines", newLable);
            rootElement.InnerRaiseEvent("EventStructureChanged", newLable, "HeaderLines");
            return newLable;
        };

        /**
         * @name RemoveGeneralItem
         * @type function 
         * @classification file
         * @param { boolean|object } id 对应一般项目属性对象或者UID值
         * @apinameZh 删除对应UID一般项目属性
         */
        rootElement.RemoveGeneralItem = function (id) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能删除一般项目");
                return;
            }
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            //如果不存在option直接退出
            if (!id) {
                return;
            }
            var hasChange = null;
            var removeData = null;
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData.Config && defaultData.Config.HeaderLines && defaultData.Config.HeaderLines.length > 0) {
                var headerLines = defaultData.Config.HeaderLines;
                hasChange = headerLines.find((data, index) => {
                    if (data.UID == id) {
                        removeData = data;
                        headerLines.splice(index, 1);
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureRemoveHeaderLines", hasChange);
            rootElement.InnerRaiseEvent("EventStructureChanged", removeData, "HeaderLines");
            return hasChange;
        };

        /**
         * @name GetSpecialItemsProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应特殊项目属性对象或者UID值
         * @apinameZh 获取全部特殊项目属性或者对应UID特殊项目属性
         */
        rootElement.GetSpecialItemsProperties = function (id) {
            return rootElement.GetInternalProperties(id, "FooterLines");
        };

        /**
         * @name SetSpecialItemsProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应特殊项目属性对象或者UID值
         * @param { object } option 修改后的特殊项目属性对象
         * @apinameZh 修改对应UID特殊项目属性接口
         */
        rootElement.SetSpecialItemsProperties = function (id, option) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能修改特殊项目属性");
                return;
            }
            rootElement.SetHeaderFooterLineProperties(id, option, "FooterLines");
        };

        /**
         * @name AddSpecialItems
         * @type function 
         * @classification file
         * @param { object } option 新增特殊项目属性对象
         * @apinameZh 新增特殊项目属性
         */
        rootElement.AddSpecialItems = function (option) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能新增特殊项目");
                return;
            }
            //判断option是否存在
            if (!option || typeof option != 'object') {
                return;
            }
            var newLable = rootElement.NewTitleLine();
            for (var i in option) {
                newLable[i] = option[i];
            }
            //找到全局的config写入
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData) {
                var footerLines = defaultData.Config.FooterLines;
                footerLines.push(newLable);
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureAddFooterLines", newLable);
            rootElement.InnerRaiseEvent("EventStructureChanged", newLable, "FooterLines");
            return newLable;
        };

        /**
         * @name RemoveSpecialItems
         * @type function 
         * @classification file
         * @param { boolean|object } id 对应特殊项目属性对象或者UID值
         * @apinameZh 删除对应UID特殊项目属性
         */
        rootElement.RemoveSpecialItems = function (id) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能删除特殊项目");
                return;
            }
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            //如果不存在option直接退出
            if (!id) {
                return;
            }
            var hasChange = null;
            var removeData = null;
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData.Config && defaultData.Config.FooterLines && defaultData.Config.FooterLines.length > 0) {
                var footerLines = defaultData.Config.FooterLines;
                hasChange = footerLines.find((data, index) => {
                    if (data.UID == id) {
                        removeData = data;
                        footerLines.splice(index, 1);
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureRemoveFooterLines", hasChange);
            rootElement.InnerRaiseEvent("EventStructureChanged", removeData, "FooterLines");
            return hasChange;
        };

        /**
         * @name SetHeaderFooterLineProperties
         * @type function 
         * @classification file
         * @param { object } options 时间轴页眉页脚属性统一接口
         * @apinameZh 页眉页脚属性统一接口
         */
        rootElement.SetHeaderFooterLineProperties = function (id, option, type) {
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            //如果不存在option直接退出
            if (!id || !option || typeof option != "object" || Object.keys(option).length == 0) {
                return;
            }
            if (!rootElement || !rootElement.DocumentOptions || !rootElement.DocumentOptions.DefaultData) {
                return;
            }
            var defaultData = rootElement.DocumentOptions.DefaultData;
            var headerFooterArr = [];
            if (defaultData.Config.HeaderLines) {
                headerFooterArr = [...headerFooterArr, ...defaultData.Config.HeaderLines];
            }
            if (defaultData.Config.FooterLines) {
                headerFooterArr = [...headerFooterArr, ...defaultData.Config.FooterLines];
            }
            var hasChange = null;
            var changeData = null;
            if (headerFooterArr && headerFooterArr.length > 0) {
                //修改属性
                hasChange = headerFooterArr.find((data, index) => {
                    if (data.UID == id) {
                        changeData = data;
                        for (var i in option) {
                            data[i] = option[i];
                        }
                        headerFooterArr[index] = data;
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            //console.log(rootElement.DocumentOptions.DefaultData);
            //console.log(config);
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateHeaderFooterLineProperties");
            rootElement.InnerRaiseEvent("EventStructureChanged", changeData, type);
        };
        /**
        * @name SetFooterLinesChangeDate
        * @type function 
        * @classification file
        * @param { object } options 特殊项目的修改数据
        * @apinameZh 特殊项目的修改数据
        */
        rootElement.SetFooterLinesChangeDate = function (options) {
            if (options && Object.keys(options).length > 0) {
                var FooterLines = rootElement.DocumentOptions.DefaultData.Config.FooterLines;

                if (rootElement.DocumentOptions.DefaultData && rootElement.DocumentOptions.DefaultData.Config) {
                    rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate'] = {};
                    rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate'] = JSON.parse(JSON.stringify(options));
                }
                WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
                rootElement.InnerRaiseEvent("EventStructureChanged", FooterLines, "FooterLines");
                return true;
            }
            return false;
        };

        /**
         * @name SetFooterLinesChangeDateByUID
         * @type function 
         * @classification file
         * @param { string } nameStr 对应特殊项目属性对象或者UID值
         * @param { object } options 特殊项目的修改数据
         * @apinameZh 特殊项目的单个修改数据
         */
        rootElement.SetFooterLinesChangeDateByUID = function (nameStr, options) {
            if (options && Object.keys(options).length > 0) {
                var changeData = [];
                var FooterLines = rootElement.DocumentOptions.DefaultData.Config.FooterLines;
                if (FooterLines && FooterLines.length) {
                    FooterLines.forEach(item => {
                        if (item.Name == nameStr) {
                            changeData.push(item);
                        }
                    });
                }

                if (rootElement.DocumentOptions.DefaultData && rootElement.DocumentOptions.DefaultData.Config) {
                    if (!rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate']) {
                        rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate'] = {};
                    }
                    rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate'][nameStr] = {};
                    rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate'][nameStr] = JSON.parse(JSON.stringify(options));
                }
                WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
                rootElement.InnerRaiseEvent("EventStructureChanged", changeData, "FooterLines");
                return true;
            }
            return false;
        };

        /**
       * @name RemoveFooterLinesChangeDateByUID
       * @type function 
       * @classification file
       * @param { string } nameStr 对应特殊项目属性对象或者UID值
       * @apinameZh 删除对应特殊项目的单个修改数据
       */
        rootElement.RemoveFooterLinesChangeDateByUID = function (nameStr) {
            if (!nameStr) {
                return false;
            }
            var changeData = [];
            var FooterLines = rootElement.DocumentOptions.DefaultData.Config.FooterLines;
            if (FooterLines && FooterLines.length) {
                FooterLines.forEach(item => {
                    if (item.Name == nameStr) {
                        changeData.push(item);
                    }
                });
            }

            if (rootElement.DocumentOptions.DefaultData && rootElement.DocumentOptions.DefaultData.Config) {
                if (!rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate']) {
                    return false;
                }
                if (!rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate'][nameStr]) {
                    return false;
                }
                delete rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate'][nameStr];

                WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
                rootElement.InnerRaiseEvent("EventStructureChanged", changeData, "FooterLines");
                return true;
            }

            return false;
        };

        /**
      * @name RemoveFooterLinesChangeDateByUID
      * @type function 
      * @classification file
      * @param { string } nameStr 对应特殊项目属性对象或者UID值
      * @apinameZh 删除对应特殊项目的单个修改数据
      */
        rootElement.RemoveFooterLinesChangeDate = function () {
            var FooterLines = rootElement.DocumentOptions.DefaultData.Config.FooterLines;
            if (rootElement.DocumentOptions.DefaultData && rootElement.DocumentOptions.DefaultData.Config) {
                if (!rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate']) {
                    return false;
                }
                delete rootElement.DocumentOptions.DefaultData.Config['FooterLinesChangeDate'];
                WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateLabelProperties");
                rootElement.InnerRaiseEvent("EventStructureChanged", FooterLines, "FooterLines");
                return true;
            }
            return false;
        };

        /**
         * @name GetYAxisInfosProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应体征项目属性对象或者UID值
         * @apinameZh 获取全部体征项目属性或者对应UID体征项目属性
         */
        rootElement.GetYAxisInfosProperties = function (id) {
            return rootElement.GetInternalProperties(id, "YAxisInfos");
        };

        /**
         * @name SetYYAxisProperties
         * @type function 
         * @classification file
         * @param { object|string } id 对应体征项目属性对象或者UID值
         * @param { object } option 修改后的体征项目属性对象
         * @apinameZh 修改对应UID体征项目属性接口
         */
        rootElement.SetYAxisInfosProperties = function (id, option) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能修改体征项目");
                return;
            }
            //如果不存在option直接退出
            if (!id || !option) {
                return;
            }
            if (!rootElement || !rootElement.DocumentOptions || !rootElement.DocumentOptions.DefaultData) {
                return;
            }
            var hasChange = null;
            var changeData = null;
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData.Config && defaultData.Config.YAxisInfos && defaultData.Config.YAxisInfos.length > 0) {
                var yAxios = defaultData.Config.YAxisInfos;
                hasChange = yAxios.find((data, index) => {
                    if (data.UID == id || data.Name == id) {
                        changeData = data;
                        for (var i in option) {
                            data[i] = option[i];
                        }
                        yAxios[index] = data;
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateYAxisInfosProperties");
            rootElement.InnerRaiseEvent("EventStructureChanged", changeData, "YAxisInfos");
        };

        /**
         * @name AddYAxisInfos
         * @type function 
         * @classification file
         * @param { object } option 新增体征项目属性对象
         * @apinameZh 新增体征项目属性
         */
        rootElement.AddYAxisInfos = function (option) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能新增体征项目");
                return;
            }
            //判断option是否存在
            if (!option || typeof option != 'object') {
                return;
            }
            var newLable = rootElement.NewYAxis();
            for (var i in option) {
                newLable[i] = option[i];
            }
            //找到全局的config写入
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData) {
                var yAxisInfos = defaultData.Config.YAxisInfos;
                yAxisInfos.push(newLable);
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureAddYAxisInfos", newLable);
            rootElement.InnerRaiseEvent("EventStructureChanged", newLable, "YAxisInfos");
            return newLable;
        };

        /**
         * @name RemoveYAxisInfos
         * @type function 
         * @classification file
         * @param { boolean|object } id 对应体征项目属性对象或者UID值
         * @apinameZh 删除对应UID体征项目属性
         */
        rootElement.RemoveYAxisInfos = function (id) {
            //判断是否为设计模式
            var dcType = rootElement.getAttribute("dctype");
            if (dcType != "DCTemperatureDesignControlForWASM") {
                console.log("需要在设计模式下才能删除体征项目");
                return;
            }
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else {
                    id = null;
                }
            }
            //如果不存在option直接退出
            if (!id) {
                return;
            }
            var hasChange = null;
            var removeData = null;
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (defaultData.Config && defaultData.Config.YAxisInfos && defaultData.Config.YAxisInfos.length > 0) {
                var yAxisInfos = defaultData.Config.YAxisInfos;
                hasChange = yAxisInfos.find((data, index) => {
                    if (data.UID == id) {
                        removeData = data;
                        yAxisInfos.splice(index, 1);
                        return true;
                    }
                });
            }
            if (!hasChange) {
                return;
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureRemoveYAxisInfos", hasChange);
            rootElement.InnerRaiseEvent("EventStructureChanged", removeData, "YAxisInfos");
            return hasChange;
        };

        /**
         * @name GetGeneralItemValue
         * @type function 
         * @classification file
         * @param { object|string } id 对应一般项目属性对象或者UID值
         * @apinameZh 获取所有一般项目值接口
         */
        rootElement.GetGeneralItemValue = function (id) {
            return rootElement.GetTemperatureDocumentValue(id, "HeaderLines");
        };

        /**
         * @name SetGeneralItemValue
         * @type function 
         * @classification file
         * @param { object|string } id 对应一般项目属性对象或者UID值
         * @param { object } option 修改后的一般项目属性对象
         * @apinameZh 修改一般项目值接口
         */
        rootElement.SetGeneralItemValue = function (id, option) {
            return rootElement.SetTemperatureDocumentValue(id, option);
        };




        /**
         * @name GetSpecialItemsValue
         * @type function 
         * @classification file
         * @param { object|string } id 对应特殊项目属性对象或者UID值
         * @apinameZh 获取所有特殊项目值接口
         */
        rootElement.GetSpecialItemsValue = function (id) {
            return rootElement.GetTemperatureDocumentValue(id, "FooterLines");
        };

        /**
         * @name SetSpecialItemsValue
         * @type function 
         * @classification file
         * @param { object|string } id 对应特殊项目属性对象或者UID值
         * @param { object } option 修改后的特殊项目属性对象
         * @apinameZh 修改特殊项目值接口
         */
        rootElement.SetSpecialItemsValue = function (id, option) {
            return rootElement.SetTemperatureDocumentValue(id, option);
        };

        /**
         * @name GetYAxisInfosValue
         * @type function 
         * @classification file
         * @param { object|string } id 对应体征项目属性对象或者UID值
         * @apinameZh 获取所有体征项目值接口
         */
        rootElement.GetYAxisInfosValue = function (id) {
            return rootElement.GetTemperatureDocumentValue(id, "YAxisInfos");
        };

        /**
         * @name SetYAxisInfosValue
         * @type function 
         * @classification file
         * @param { object|string } id 对应体征项目属性对象或者UID值
         * @param { object } option 修改后的体征项目属性对象
         * @apinameZh 修改体征项目值接口
         */
        rootElement.SetYAxisInfosValue = function (id, option) {
            return rootElement.SetTemperatureDocumentValue(id, option);
        };

        /**
         * @name DeleteYAxisInfosValue
         * @param { object|string } name 对应体征项目属性对象或者UID值
         * @param { string } time 对应体征项目属性值的时间
         * @param { object } option 修改后的体征项目属性对象
         * @apinameZh 删除体征项目值接口
         */
        rootElement.DeleteYAxisInfosValue = function (name, time) {
            if (!name || !time) {
                return false;
            }
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (!defaultData.Config || !defaultData.Config.YAxisInfos || defaultData.Config.YAxisInfos.length == 0) {
                return false;
            }
            var Values = defaultData.Values;
            if (Values && Values.length > 0) {
                var targetYAxisValue = null;
                for (var i = 0; i < Values.length; i++) {
                    if (Values[i].Name == name.trim()) {
                        targetYAxisValue = Values[i];
                        break;
                    }
                }
                if (targetYAxisValue && targetYAxisValue.Datas && targetYAxisValue.Datas.length > 0) {
                    var findTime = new Date(time).getTime();//要删除的时间
                    var Datas = targetYAxisValue.Datas;
                    for (var i = 0; i < Datas.length; i++) {
                        var dataTime = Datas[i].Time && new Date(Datas[i].Time).getTime() || 0;
                        if (dataTime == findTime) {
                            Datas.splice(i, 1);
                            break;
                        }
                    }
                    WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureValue");
                    return true;
                }
            }

        };



        /**
         * @name GetTemperatureDocumentValue
         * @type function 
         * @classification file
         * @param { string|object } id 时间轴id值或者UID属性
         * @param { string } type 时间轴项目类型
         * @apinameZh 获取时间轴数据
         */
        rootElement.GetTemperatureDocumentValue = function (id, type) {
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else if (id.Name) {
                    id = id.Name;
                } else {
                    id = null;
                }
            }
            if (!rootElement || !rootElement.DocumentOptions || !rootElement.DocumentOptions.DefaultData) {
                return;
            }
            var defaultData = rootElement.DocumentOptions.DefaultData;
            if (!defaultData.Values) {
                return;
            }
            //如果存在id值,优先考虑uid
            var allType = ["HeaderLines", "FooterLines", "YAxisInfos"];
            //如果不存在id值,直接返回所有的数值
            if (id == null) {
                if (type && allType.indexOf(type) >= 0) {
                    //获取对应的项目
                    var thisLabel = defaultData.Config[type];
                    var allName = [];
                    for (var i = 0; i < thisLabel.length; i++) {
                        if (thisLabel[i].Name) {
                            allName.push(thisLabel[i].Name);
                        }
                    }
                    //获取所有的返回值
                    var result = [];
                    if (allName.length > 0) {
                        for (var i = 0; i < defaultData.Values.length; i++) {
                            var thisValue = defaultData.Values[i];
                            if (allName.indexOf(thisValue.Name) >= 0) {
                                result.push(thisValue);
                                continue;
                            }
                        }
                    }
                    return result;
                } else {
                    return defaultData.Values;
                }
            }
            var hasName = null;
            for (var i = 0; i < allType.length; i++) {
                if (hasName) {
                    break;
                }
                var thisLabel = defaultData.Config[allType[i]];
                if (thisLabel && thisLabel.length > 0) {
                    for (var j = 0; j < thisLabel.length; j++) {
                        if (thisLabel[j].UID == id) {
                            hasName = thisLabel[j].Name;
                            break;
                        } else if (thisLabel[j].Name == id) {
                            hasName = thisLabel[j].Name;
                            break;
                        }
                    }
                }
            }
            var result = null;
            if (hasName) {
                for (var i = 0; i < defaultData.Values.length; i++) {
                    var thisValue = defaultData.Values[i];
                    if (thisValue.Name == hasName) {
                        result = thisValue;
                        break;
                    }
                }
            }
            return result;
        };

        /**
         * @name SetTemperatureDocumentValue
         * @type function 
         * @classification file
         * @param { string|object } id 时间轴id值或者UID属性
         * @param { object|array } options 时间轴数值数组
         * @apinameZh 设置时间轴数据
         */
        rootElement.SetTemperatureDocumentValue = function (id, option) {
            try {
                if (typeof id == 'object') {
                    //判断是否存在UID属性
                    if (id.UID) {
                        id = id.UID;
                    } else if (id.Name) {
                        id = id.Name;
                    } else {
                        id = null;
                    }
                }
                //如果不存在option直接退出
                if (!id) {
                    return false;
                }
                if (!rootElement || !rootElement.DocumentOptions || !rootElement.DocumentOptions.DefaultData) {
                    return false;
                }
                var defaultData = rootElement.DocumentOptions.DefaultData;

                //如果没有属性直接退回
                if (!option) {
                    return;
                }

                //如果存在id值,优先考虑uid
                var allType = ["HeaderLines", "FooterLines", "YAxisInfos"];
                var hasName = null;
                var thisType = null;
                var thisStyle = null;
                for (var i = 0; i < allType.length; i++) {
                    thisType = allType[i];
                    if (hasName) {
                        break;
                    }
                    var thisLabel = defaultData.Config[allType[i]];
                    if (thisLabel && thisLabel.length > 0) {
                        for (var j = 0; j < thisLabel.length; j++) {
                            if (thisLabel[j].UID == id) {
                                hasName = thisLabel[j].Name;
                                thisStyle = thisLabel[j].Style;
                                break;
                            } else if (thisLabel[j].Name == id) {
                                hasName = thisLabel[j].Name;
                                thisStyle = thisLabel[j].Style;
                                break;
                            }
                        }
                    }
                }
                option = JSON.parse(JSON.stringify(option));
                //自此出修改正option的值
                if (!Array.isArray(option)) {
                    option = [option];
                }
                for (var i = 0; i < option.length; i++) {
                    //如果时间不存在直接返回
                    if (!option[i].Time) {
                        continue;
                    }
                    //if (!hasName && !option[i].Value) {
                    //    continue;
                    //}
                    //判断是否是否合法
                    var isDate = new Date(option[i].Time).getTime();
                    if (isNaN(isDate)) {
                        option[i].Time = null;
                    }
                    var newOption = rootElement.NewValuePoint();
                    for (var j in option[i]) {
                        newOption[j] = option[i][j];
                    }
                    option[i] = newOption;
                }

                if (hasName) {
                    changeDate(hasName, option);
                } else {
                    defaultData.Values.push({
                        Name: id,
                        Datas: option
                    });
                }
                WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureValue");
                return true;
            } catch (err) {
                return false;
            }

            function changeDate() {
                //如果存在Value
                if (defaultData.Values && defaultData.Values.length > 0) {
                    //存在相同的值
                    var hasValue = defaultData.Values.find((data, index) => {
                        //存在相同的Name值
                        if (data.Name == hasName) {
                            //存在值
                            if (data.Datas) {
                                //循环写入数据
                                option.forEach((newData, newIndex) => {
                                    if (newData.Time) {
                                        var hasChange = false;
                                        data.Datas.forEach((oldData, oldIndex) => {
                                            if (newData.Time == oldData.Time) {
                                                //判断是否为Text
                                                if (thisType == "YAxisInfos") {
                                                    if (thisStyle == "Text") {
                                                        for (var i in newData) {
                                                            if (i == "Text") {
                                                                continue;
                                                            }
                                                            oldData[i] = newData[i];
                                                        }
                                                        oldData.Text += `&dc&${newData.Text}`; //用特殊标记表示
                                                        hasChange = true;
                                                        return;
                                                    }
                                                    if (!newData.Value && newData.Value !== 0) {
                                                        data.Datas.splice(oldIndex, 1);
                                                    } else {
                                                        for (var i in newData) {
                                                            oldData[i] = newData[i];
                                                        }
                                                    }
                                                    hasChange = true;
                                                } else {
                                                    if (!newData.Text && (!newData.Value && newData.Value !== 0)) {
                                                        data.Datas.splice(oldIndex, 1);
                                                    } else {
                                                        for (var i in newData) {
                                                            oldData[i] = newData[i];
                                                        }
                                                    }
                                                    hasChange = true;
                                                }
                                            }
                                        });
                                        if (!hasChange) {
                                            data.Datas.push(newData);
                                        }
                                    }
                                });
                            } else {
                                //不存在值
                                data.Datas = option;
                            }
                            return true;
                        }
                    });
                    if (!hasValue) {
                        defaultData.Values.push({
                            Name: hasName,
                            Datas: option
                        });
                    }
                } else {
                    if (defaultData.Values == null) {
                        defaultData.Values = [];
                    }
                    defaultData.Values.push({
                        Name: hasName,
                        Datas: option
                    });
                }

            }
        };

        /**
         * @name RemoveTemperatureDocumentValue
         * @param { string|object } id 时间轴id值或者UID属性
         * @param { object|array } options 时间轴数值数组（默认值为null，表示删除所有数据）
         * @apinameZh 删除时间轴数据，如果不传参数则清空所有值
         */
        rootElement.RemoveTemperatureDocumentValue = function (id, option = null) {
            try {
                if (typeof id == 'object') {
                    //判断是否存在UID属性
                    if (id.UID) {
                        id = id.UID;
                    } else if (id.Name) {
                        id = id.Name;
                    } else {
                        id = null;
                    }
                }
                if (!rootElement || !rootElement.DocumentOptions || !rootElement.DocumentOptions.DefaultData) {
                    return false;
                }
                var defaultData = rootElement.DocumentOptions.DefaultData;

                //如果不存在option直接退出
                if (!id && !option) {
                    //清空整个value
                    defaultData.Values = [];
                    WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureValue");
                    return true;
                }
                if (!id) {
                    return false;
                }

                //如果存在id值,优先考虑uid
                var allType = ["HeaderLines", "FooterLines", "YAxisInfos"];
                var hasName = null;
                for (var i = 0; i < allType.length; i++) {
                    if (hasName) {
                        break;
                    }
                    var thisLabel = defaultData.Config[allType[i]];
                    if (thisLabel && thisLabel.length > 0) {
                        for (var j = 0; j < thisLabel.length; j++) {
                            if (thisLabel[j].UID == id) {
                                hasName = thisLabel[j].Name;
                                break;
                            } else if (thisLabel[j].Name == id) {
                                hasName = thisLabel[j].Name;
                                break;
                            }
                        }
                    }
                }

                if (hasName) {
                    //如果存在Value
                    if (defaultData.Values && defaultData.Values.length > 0) {
                        //存在相同的值
                        var hasValue = defaultData.Values.find((data, index) => {
                            //存在相同的Name值
                            if (data.Name == hasName) {
                                //存在值
                                if (data.Datas) {
                                    if (option) {
                                        option = JSON.parse(JSON.stringify(option));
                                        //自此出修改正option的值
                                        if (!Array.isArray(option)) {
                                            option = [option];
                                        }
                                        for (var i = 0; i < option.length; i++) {
                                            //如果时间不存在直接返回
                                            if (!option[i].Time) {
                                                continue;
                                            }
                                            //if (!hasName && !option[i].Value) {
                                            //    continue;
                                            //}
                                            //判断是否是否合法
                                            var isDate = new Date(option[i].Time).getTime();
                                            if (isNaN(isDate)) {
                                                option[i].Time = null;
                                            }
                                            var newOption = rootElement.NewValuePoint();
                                            for (var j in option[i]) {
                                                newOption[j] = option[i][j];
                                            }
                                            option[i] = newOption;
                                        }
                                        //循环写入数据
                                        option.forEach((newData, newIndex) => {
                                            if (newData.Time) {
                                                var hasChange = false;
                                                data.Datas.forEach((oldData, oldIndex) => {
                                                    if (newData.Time == oldData.Time || (newData.ID && (newData.ID == oldData.ID))) {
                                                        data.Datas.splice(oldIndex, 1);
                                                        hasChange = true;
                                                    }
                                                });
                                                if (!hasChange) {
                                                    data.Datas.push(newData);
                                                }
                                            }
                                        });
                                    } else {
                                        data.Datas = [];
                                    }
                                }
                            }
                        });
                    }
                }
                WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureValue");
                return true;
            } catch (err) {
                return false;
            }
        };

        /**
         * @name GetTemperatureViewMode
         * @type function 
         * @classification file
         * @apinameZh 获取时间轴显示模式
         */
        rootElement.GetTemperatureViewMode = function () {
            return rootElement.DocumentOptions.DefaultData.ViewMode;
        };

        /**
         * @name SetTemperatureViewMode
         * @type function 
         * @classification file
         * @apinameZh 设置时间轴显示模式
         */
        rootElement.SetTemperatureViewMode = function (type) {
            if (type && typeof type == "string") {
                type == type.toLowerCase().trim();
                if (type == "singlepage" || type == "temperature" || type == "page") {
                    var pageindex = rootElement.getAttribute("pageindex");
                    if (!pageindex || pageindex == "0") {
                        rootElement.setAttribute("pageindex", 1);
                    }
                    rootElement.setAttribute("viewmode", type);
                    rootElement.DocumentOptions.DefaultData.ViewMode = type;
                    //此处刷新数据
                    WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateViewMode");
                }
            }
        };

        /**
         * @name GetTemperatureNumOfPages
         * @type function 
         * @classification file
         * @apinameZh 获取时间轴总页数
         */
        rootElement.GetTemperatureNumOfPages = function () {
            return rootElement.DocumentOptions.DefaultData.NumOfPages;
        };

        /**
         * @name GetTemperaturePageIndex
         * @type function 
         * @classification file
         * @apinameZh 分页展示时获取时间轴当前页
         */
        rootElement.GetTemperaturePageIndex = function () {
            return rootElement.DocumentOptions.DefaultData.PageIndex;
        };

        /**
         * @name SetTemperaturePageIndex
         * @type function 
         * @classification file
         * @apinameZh 分页展示时设置时间轴当前页
         */
        rootElement.SetTemperaturePageIndex = function (index) {
            if (index && typeof index == "string") {
                index = parseInt(index);
                if (isNaN(index)) {
                    index == 1;
                }
            }
            if (typeof index == "number") {
                rootElement.setAttribute("pageindex", index);

                var calculationData = rootElement.DocumentOptions.CalculationData;
                var svgEle = rootElement.DocumentOptions.SVGElement;

                //视图模式
                // var viewMode = calculationData.GetViewMode ? calculationData.GetViewMode.toLowerCase().trim() : "page";
                var viewMode = rootElement.getAttribute("viewmode") && rootElement.getAttribute("viewmode").toLowerCase().trim();
                if (!viewMode) {
                    viewMode = calculationData.GetViewMode ? calculationData.GetViewMode.toLowerCase().trim() : "page";
                }
                //判断是否需要滚动页面
                if (viewMode !== "singlepage") {
                    //非单页模式下需要滚动页面
                    var thisSvg = svgEle[index - 1];
                    if (thisSvg) {
                        thisSvg = thisSvg.page.node();
                        if (thisSvg) {
                            var totleHeight = 0;
                            for (var i = 1; i < index; i++) {
                                totleHeight += svgEle[i].page.node().clientHeight + 5 + 2;
                            }
                            rootElement.querySelector('[dctype=page-container]').scrollTo(0, totleHeight);
                        }
                    }
                } else {
                    //此处刷新数据
                    WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdatePageIndex");
                }
            }
        };

        /**
         * @name DesignerMode
         * @type function 
         * @classification file
         * @param { boolean } bool true为设计模式,false为正常模式
         * @apinameZh 设置时间轴显示模式
         */
        rootElement.DesignerMode = function (bool) {
            //获取编辑器dctype
            var rootType = rootElement.getAttribute("dctype");
            if (!rootType) {
                return false;
            }
            if (bool === true) {
                //本身就是设计模式直接退出
                if (rootType == "DCTemperatureDesignControlForWASM") {
                    return true;
                }
                rootElement.setAttribute("dctype", "DCTemperatureDesignControlForWASM");
            } else {
                if (rootType == "DCTemperatureControlForWASM") {
                    return true;
                }
                rootElement.setAttribute("dctype", "DCTemperatureControlForWASM");
            }
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateDesignerMode");
            return true;
        };

        /**
         * @name GetTemperatureDocumentStructure
         * @type function 
         * @classification file
         * @apinameZh 获取当前加载文档的导航结构
         */
        rootElement.GetTemperatureDocumentStructure = function () {
            var defaultData = rootElement.DocumentOptions.DefaultData;
            //最先把全局配置写入
            var result = {
                DocumentConfig: []
            };
            if (defaultData && defaultData.Config) {
                var allItem = ["HeaderLabels", "HeaderLines", "FooterLines", "YAxisInfos", "Labels"];
                for (var i = 0; i < allItem.length; i++) {
                    var thisItem = allItem[i];
                    result[thisItem] = [];
                    var thisLabel = defaultData.Config[allItem[i]];
                    if (thisLabel && thisLabel.length > 0) {
                        for (var j = 0; j < thisLabel.length; j++) {
                            result[thisItem][j] = {
                                Name: thisLabel[j].Name,
                                UID: thisLabel[j].UID,
                                Title: thisLabel[j].Title
                            };
                        }
                    }
                }
            }
            return result;
        };

        /**
         * @name LocationStructure
         * @type function 
         * @classification file
         * @apinameZh 获取当前元素在文档中的位置
         */
        rootElement.LocationStructure = function (uid) {
            if (typeof id == 'object') {
                //判断是否存在UID属性
                if (id.UID) {
                    id = id.UID;
                } else if (id.Name) {
                    id = id.Name;
                } else {
                    id = null;
                }
            }
            WriterControl_DrawFu.MoveBorderRect(rootElement, uid);
        };

        /**
        * @name InnerRaiseEvent
        * @type function
        * @classification file
        * @apinameZh 编辑器内部事件触发
        */
        rootElement.InnerRaiseEvent = function (eventName, args, type) {
            if (eventName && typeof rootElement[eventName] == "function") {
                var result = rootElement[eventName].call(rootElement, rootElement, args, type);
                return result;
            }
            return;
        };

        rootElement.NewTemperatureDocument = function () {
            return TemperatureControl_Data.NewTemperatureDocument();
        };
        rootElement.NewHeaderLabel = function () {
            return TemperatureControl_Data.NewHeaderLabel();
        };
        rootElement.NewPageSettings = function () {
            return TemperatureControl_Data.NewPageSettings();
        };
        rootElement.NewTemperatureDocumentConfig = function () {
            return TemperatureControl_Data.NewTemperatureDocumentConfig();
        };
        rootElement.NewTextLabel = function () {
            return TemperatureControl_Data.NewTextLabel();
        };
        rootElement.NewTitleLine = function () {
            return TemperatureControl_Data.NewTitleLine();
        };
        rootElement.NewValuePoint = function () {
            return TemperatureControl_Data.NewValuePoint();
        };
        rootElement.NewYAxis = function () {
            return TemperatureControl_Data.NewYAxis();
        };

        /**
        * @name ReturnDefaultValue
        * @type function
        * @classification file
        * @apinameZh f返回默认值
        */
        // * @param { object } jsonobj 时间轴文档在前端的JSON结构。
        rootElement.ReturnDefaultValue = function () {
            var defaultValue = TemperatureControl_Data.NewTemperatureDocument();
            defaultValue.Config.PageSettings = TemperatureControl_Data.NewPageSettings();
            return defaultValue;
        };

        /**
        * @name ShowAboutDialog
        * @type function
        * @apinameZh 显示关于对话框
        * @classification file
        * @param ["flag","boolean","是否弹出alert提示","true","","false"]
        * @returns ["result","json","参数为false时会返回json数据"]
        */
        rootElement.ShowAboutDialog = function (flag = true) {
            var startTime = new Date();
            var result = null;
            result = rootElement.__DCWriterReference.invokeMethod(window.DCWriterEntryPointAssemblyName, "ShowTemperatureAboutDialog", flag);
            DCEventInterfaceLogFunction(rootElement, 'ShowTemperatureAboutDialog', startTime);
            return result;
        };

        /**
         * @name ShowDesignerDialog
         * @type function
         * @apinameZh 显示设计器对话框
         * @classification file
         * @param ["xml","string","设计器要展示的xml数据","true","","false"]
         * @returns ["result","json","参数为false时会返回json数据"]
         */
        rootElement.ShowDesignerDialog = function (xml) {
            var startTime = new Date();
            if (!xml) {
                xml = JSON.stringify(rootElement.DocumentOptions.DefaultData);
            }
            var result = TemperatureControl_Designer.initDesignerDiv(rootElement, xml);
            DCEventInterfaceLogFunction(rootElement, 'ShowDesignerDialog', startTime);
            return result;
        };

        /**
         * @name SetPageSettings
         * @type function
         * @apinameZh 页面设置
         * @classification file
         * @param ["pagesettings","object","设计器页面设置","true","","false"]
         * @returns ["result","boolean","true成功，false失败"]
         */
        rootElement.SetPageSettings = function (pageSettings) {
            if (!pageSettings) { return false; }
            var startTime = new Date();
            try {
                rootElement.DocumentOptions.DefaultData.Config.PageSettings = pageSettings;
            } catch (error) {
                return false;
            }
            DCEventInterfaceLogFunction(rootElement, 'SetPageSettings', startTime);
            return true;
        };

        /**
         * @name GetPageSettings
         * @type function
         * @apinameZh 获取页面设置
         * @classification file
         * @returns ["pagesettings","Object","页面设置属性对象"]
         */
        rootElement.GetPageSettings = function () {
            var startTime = new Date();
            let pagesettings = null;
            if (rootElement.DocumentOptions.DefaultData.Config.PageSettings) {
                pagesettings = rootElement.DocumentOptions.DefaultData.Config.PageSettings;
            }
            DCEventInterfaceLogFunction(rootElement, 'GetPageSettings', startTime);
            return pagesettings;
        };

        /**
        * @name SetTableData
        * @type function
        * @apinameZh 设置体温单表格数据
        * @classification file
        * @param ["tableId","array","表格id","true","","false"]
        * @param ["tableData","array","表格数据","true","","false"]
        */
        rootElement.SetTableData = function (tableId, tableData = []) {
            var startTime = new Date();
            if (tableData && Array.isArray(tableData)) {
                if (rootElement.DocumentOptions.DefaultData.BottomTableGroups) {
                    if (rootElement.DocumentOptions.DefaultData.BottomTableGroups[tableId]) {
                        rootElement.DocumentOptions.DefaultData.BottomTableGroups[tableId]['tableData'] = tableData;
                    }
                    //单独重绘表格
                    WriterControl_DrawFu.CreateTemperatureInit(rootElement, rootElement.DocumentOptions.DefaultData, "EventTemperatureUpdateDocumentConfig");
                }
            }
            DCEventInterfaceLogFunction(rootElement, 'SetTableData', startTime);
            return true;
        };

        /**
         * @name GetTablesDataSource
         * @type function
         * @apinameZh 获取指定表格的数据绑定参数
         * @classification file
         * @param ["tableId","array","表格id","true","","false"]
         */
        rootElement.GetTableData = function (tableId = null) {
            if (!tableId) {
                return null;
            }
            var BottomTableGroups = rootElement.DocumentOptions.DefaultData.BottomTableGroups || null;
            if (!BottomTableGroups) {
                return null;
            }
            if (tableId) {
                var tableData = BottomTableGroups[tableId] && BottomTableGroups[tableId]['tableData'];
                if (tableData && tableData.length > 0) {
                    return tableData;
                }
            }
            return null;

        };

        /**
       * @name GetTablesDataSource
       * @type function
       * @apinameZh 获取指定表格的数据绑定参数
       * @classification file
       * @param ["tableId","array","表格id","true","","false"]
       */
        rootElement.GetTablesDataSource = function (tableId = null) {
            var BottomTableGroups = rootElement.DocumentOptions.DefaultData.BottomTableGroups || null;
            if (!BottomTableGroups) {
                return null;
            }
            if (tableId) {
                var tableColumns = BottomTableGroups[tableId] && BottomTableGroups[tableId]['tableColumns'];
                if (tableColumns && tableColumns.length > 0) {
                    var sourceObj = {};
                    getTableSource(tableColumns, sourceObj);
                    return sourceObj;
                }
            } else {
                var allTable = Object.keys(BottomTableGroups);
                if (allTable && allTable.length > 0) {
                    var resultArray = [];
                    for (var i = 0; i < allTable.length; i++) {
                        var table = BottomTableGroups[allTable[i]];
                        if (table && table.tableColumns && table.tableColumns.length > 0) {
                            var sourceObj = {};
                            getTableSource(table.tableColumns, sourceObj);
                            resultArray.push(sourceObj);
                        }
                    }
                    return resultArray;
                }
            }

            function getTableSource(columns, sourceObj) {
                for (var i = 0; i < columns.length; i++) {
                    var column = columns[i];
                    if (column && column.prop) {
                        sourceObj[column.prop] = '';
                    } else if (column && column.children && column.children.length > 0) {
                        getTableSource(column.children, sourceObj);
                    }
                }
            }


            return null;

        };



        /**
        * @name TemperatureRegisterCode
        * @type Property
        * @classification file
        * @apinameZh 获取文档注册信息
        * @valueType string
        */
        Object.defineProperty(rootElement, "TemperatureRegisterCode", {
            get() {
                var startTime = new Date();
                let result = rootElement.__DCWriterReference.invokeMethod(window.DCWriterEntryPointAssemblyName, "get_TemperatureRegisterCode");
                DCEventInterfaceLogFunction(rootElement, 'TemperatureRegisterCode', startTime, true);
                return result;
            },
            set(value) {
                var startTime = new Date();
                rootElement.__DCWriterReference.invokeMethod(window.DCWriterEntryPointAssemblyName, "set_TemperatureRegisterCode", value);
                //更新绘制注册信息
                rootElement.TemperatureRegisterCode && WriterControl_DrawFu.ChangeAboutMessageText(rootElement, rootElement.TemperatureRegisterCode);
                DCEventInterfaceLogFunction(rootElement, 'set_TemperatureRegisterCode', startTime, true);
            },
            configurable: true,//允许属性被删除或修改
        });

        //rootElement.refreshDocumentOptions();
        document.TemperatureWriterControl = rootElement;
        if (rootElement.ownerDocument !== document) {
            rootElement.ownerDocument.TemperatureWriterControl = rootElement;
        }
    },

    /**
    * 对产程图元素绑定一些方法供外面调用
    * @param {HTMLElement} rootElement 根元素对象
    * @param {object} refDCWriter DCWriterClass对象在JS中的代理
    */
    BindControlForFlowControlForWASM: function (rootElement, refDCWriter) {
        /**
         * @name InnerRaiseEvent
         * @type function
         * @apinameZh 编辑器内部事件触发
         */
        rootElement.InnerRaiseEvent = function (eventName, args, type) {
            if (eventName && typeof rootElement[eventName] == "function") {
                var result = rootElement[eventName].call(rootElement, rootElement, args, type);
                return result;
            }
            return;
        };

        /**     
        * @name LoadFlowDocumentFromJson
        * @param {object} data 产程图所有数据
        * @apinameZh 根据json绘制产程图
        */
        rootElement.LoadFlowDocumentFromJson = function (data) {
            if (data) {
                try {
                    if (typeof data === 'string') {
                        data = JSON.parse(data);
                    }
                } catch (error) {
                    console.error(error);
                    return false;
                }
                //单独重绘表格
                WriterContorl_FlowChart.CreateFlowControlInit(rootElement, data, 'EventFlowDocumentLoad');
                return true;
            }
            return false;
        };


        /**     
        * @name LoadFlowDocumentFromJsonFile
        * @param {object} data 产程图所有数据
        * @apinameZh 根据json格式的字符串文件绘制产程图
        */
        rootElement.LoadFlowDocumentFromJsonFile = function () {
            var file = rootElement.ownerDocument.createElement('input');
            file.setAttribute('id', 'dcInputFile');
            file.setAttribute('type', 'file');
            file.setAttribute('accept', '.json');
            file.style.cssText = 'position: relative;left: -2000px; ';
            rootElement.appendChild(file);
            file.click();
            //file文件选中事件
            file.onchange = function () {
                var fileList = this.files;
                if (fileList.length > 0) {
                    console.log(fileList[0]);
                    //转成json对象
                    var reader = new FileReader();
                    reader.readAsText(fileList[0]);
                    reader.onload = function (e) {
                        var jsonData = JSON.parse(e.target.result);
                        rootElement.LoadFlowDocumentFromJson(jsonData);
                    };
                }
            };
        };

        /**
         * @name SetFlowYAxisValues
         * @param {object} data 产程图Y轴数据{name:valueArray}
         * @apinameZh 设置所有的数据值
         */
        rootElement.SetFlowYAxisValues = function (data) {
            if (data && Object.keys(data) && Object.keys(data).length > 0) {
                var dataKeys = Object.keys(data);
                for (var i = 0; i < dataKeys.length; i++) {
                    var key = dataKeys[i];
                    var value = data[key];
                    rootElement.DocumentOptions.DefaultData.Values[key] = JSON.parse(JSON.stringify(value));
                }
                WriterContorl_FlowChart.ChangeValue(rootElement);
                return true;
            }
            return false;
        };

        /**     
        * @name SetFlowYAxisValuesByName
        * @param {string} dataName 产程图Y轴Name
        * @param {array|object} data 产程图Y轴对应Name的值。（两种方式：1.传一个值对象，会追加到原有的数组中。2.传一个数组，会直接替换原有的数组。）
        * @apinameZh 设置指定Y轴的属性值
        */
        rootElement.SetFlowYAxisValuesByName = function (dataName, data) {
            if (dataName !== 'AttachedTableData') {//防止从此接口设置表格的值
                if (data && Array.isArray(data)) {
                    //数组直接替换
                    rootElement.DocumentOptions.DefaultData.Values[dataName] = JSON.parse(JSON.stringify(data));
                } else if (typeof data === 'object' && Object.keys(data).length > 0) {
                    //对象会被追加
                    var Values = rootElement.DocumentOptions.DefaultData.Values;
                    console.log(Values, dataName, '========');
                    if (Values[dataName] && Array.isArray(Values[dataName])) {
                        //判断当前传入的数据是否带有结束标记
                        if (data && data.IsEndValue) {
                            // // 存在结束标记，则与已存在结束点对比时间，如果时间大于已存在的结束点，则删除已存在的结束点
                            Values[dataName].forEach(item => {
                                if (item && item.IsEndValue) {
                                    // var itemTime = item.Time ? new Date(item.Time).getTime() : 0;
                                    // var dataTime = data.Time ? new Date(data.Time).getTime() : 0;;
                                    // if (dataTime >= itemTime) {
                                    delete item.IsEndValue;
                                    // }
                                }
                            });
                        }

                        //存入数据
                        Values[dataName].push(data);
                    } else {

                        Values[dataName] = [data];
                    }
                }
                // WriterContorl_FlowChart.CreateFlowControlInit(rootElement, rootElement.DocumentOptions.DefaultData, 'EventFChangeFlowYAxisValues');

                WriterContorl_FlowChart.ChangeValue(rootElement);
                return true;
            }
            return false;
        };


        /**     
        * @name SetFlowAttachedTableDataValues
        * @param {array|object} data 设置产程图表格部分的值。（两种方式：1.传一个值对象，会追加到原有的数组中。2.传一个数组，会直接替换原有的数组。）
        * @apinameZh 设置产程图表格值
        */
        rootElement.SetFlowAttachedTableDataValues = function (data) {
            if (data && Array.isArray(data)) {
                //数组直接替换
                rootElement.DocumentOptions.DefaultData.Values['AttachedTableData'] = JSON.parse(JSON.stringify(data));
            } else if (data && typeof data === 'object' && Object.keys(data).length > 0) {
                //对象会被追加
                var Values = rootElement.DocumentOptions.DefaultData.Values;
                if (Values['AttachedTableData'] && Array.isArray(Values['AttachedTableData'])) {
                    for (var i = 0; i < Values['AttachedTableData'].length; i++) {
                        var item = Values['AttachedTableData'][i];
                        if (item && item.Time) {
                            if (data && data.Time && item.Time === data.Time) {
                                //存在相同时间，则替换
                                Values['AttachedTableData'][i] = data;
                                break;
                            }
                        }
                    }
                    Values['AttachedTableData'].push(data);
                } else {
                    Values['AttachedTableData'] = [data];
                }
            }

            WriterContorl_FlowChart.ChangeValue(rootElement);
            return true;
        };


        /**     
         * @name setFlowLabelByName
         * @type function
         * @param {string} labelName   标签名称
         * @param {object} labelValue  标签属性值
         * @apinameZh 设置文本标签的属性
         */
        rootElement.setFlowLabelByName = function (labelName, labelValue) {
            if (labelName && labelValue && typeof labelName === 'string' && typeof labelValue === 'object') {
                var labels = rootElement.DocumentOptions.DefaultData.Config.Labels;
                if (labels && Array.isArray(labels)) {
                    for (var i = 0; i < labels.length; i++) {
                        if ((labels[i] && labels[i].Name) === labelName) {
                            var newLabelKeys = Object.keys(labelValue);
                            for (var j = 0; j < newLabelKeys.length; j++) {
                                labels[i][newLabelKeys[j]] = labelValue[newLabelKeys[j]];
                            }
                            break;
                        }
                    }
                }
            }
            WriterContorl_FlowChart.CreateFlowControlInit(rootElement, rootElement.DocumentOptions.DefaultData, 'EventFChangeFlowLabel');
            return true;
        };


        /**     
        * @name setFlowHeaderLabelByName
        * @param {string} labelName   标签名称
        * @param {object} labelValue  标签属性值
        * @apinameZh 设置顶部页眉标签的属性
        */
        rootElement.setFlowHeaderLabelByName = function (labelName, labelValue) {
            if (labelName && labelValue && typeof labelName === 'string' && typeof labelValue === 'object') {
                var HeaderLabels = rootElement.DocumentOptions.DefaultData.Config.HeaderLabels;
                if (HeaderLabels && Array.isArray(HeaderLabels)) {
                    for (var i = 0; i < HeaderLabels.length; i++) {
                        if ((HeaderLabels[i] && HeaderLabels[i].Name) === labelName) {
                            var newLabelKeys = Object.keys(labelValue);
                            for (var j = 0; j < newLabelKeys.length; j++) {
                                HeaderLabels[i][newLabelKeys[j]] = labelValue[newLabelKeys[j]];
                            }
                            break;
                        }
                    }
                }
            }
            WriterContorl_FlowChart.CreateFlowControlInit(rootElement, rootElement.DocumentOptions.DefaultData, 'EventFChangeFlowHeaderLabel');
            return true;
        };

        /**     
        * @name setFlowYAxisHeaderLabelByName
        * @param {string} labelName   标签名称
        * @param {object} labelValue  标签属性值
        * @apinameZh 设置Y轴区域内页眉标签的属性
        */
        rootElement.setFlowYAxisHeaderLabelByName = function (labelName, labelValue) {
            if (labelName && labelValue && typeof labelName === 'string' && typeof labelValue === 'object') {
                var HeaderLabels = rootElement.DocumentOptions.DefaultData.Config.YAxis.XAxisForHeaderLableList;
                if (HeaderLabels && Array.isArray(HeaderLabels)) {
                    for (var i = 0; i < HeaderLabels.length; i++) {
                        if ((HeaderLabels[i] && HeaderLabels[i].Name) === labelName) {
                            var newLabelKeys = Object.keys(labelValue);
                            for (var j = 0; j < newLabelKeys.length; j++) {
                                HeaderLabels[i][newLabelKeys[j]] = labelValue[newLabelKeys[j]];
                            }
                            break;
                        }
                    }
                }
            }
            WriterContorl_FlowChart.CreateFlowControlInit(rootElement, rootElement.DocumentOptions.DefaultData, 'EventFChangeFlowHeaderLabel');
            return true;
        };


        /**     
        * @name setFlowAttchedTableHeaderLabelByName
        * @param {string} labelName   标签名称
        * @param {object} labelValue  标签属性值
        * @apinameZh 设置表格区域内页眉标签的属性
        */
        rootElement.setFlowAttchedTableHeaderLabelByName = function (labelName, labelValue) {
            if (labelName && labelValue && typeof labelName === 'string' && typeof labelValue === 'object') {
                var HeaderLabels = rootElement.DocumentOptions.DefaultData.Config.AttachedTable.AttachedTableHeaderLabels;
                if (HeaderLabels && Array.isArray(HeaderLabels)) {
                    for (var i = 0; i < HeaderLabels.length; i++) {
                        if ((HeaderLabels[i] && HeaderLabels[i].Name) === labelName) {
                            var newLabelKeys = Object.keys(labelValue);
                            for (var j = 0; j < newLabelKeys.length; j++) {
                                HeaderLabels[i][newLabelKeys[j]] = labelValue[newLabelKeys[j]];
                            }
                            break;
                        }
                    }
                }
            }
            WriterContorl_FlowChart.CreateFlowControlInit(rootElement, rootElement.DocumentOptions.DefaultData, 'EventFChangeFlowHeaderLabel');
            return true;
        };

        /**     
         * @name ResetFlowValues
         * @apinameZh 清空Y轴和表格处所有的值
         */
        rootElement.ResetFlowValues = function () {
            rootElement.DocumentOptions.DefaultData.Values = {};
            WriterContorl_FlowChart.ChangeValue(rootElement);
            return true;
        };

        /**
        * @name SaveFlowDocumentToJson
        * @return {object} 产程图所有数据
        * @apinameZh 保存为json数据
        */
        rootElement.SaveFlowDocumentToJson = function () {
            if (rootElement.DocumentOptions.DefaultData) {
                return JSON.stringify(rootElement.DocumentOptions.DefaultData);
            }
            return null;
        };

        /**
        * @name SaveFlowDocumentToJsonFile
        * @return {object} 产程图所有数据
        * @apinameZh 保存为json格式的字符串为本地文件
        */
        rootElement.SaveFlowDocumentToJsonFile = function () {
            var DefaultData = rootElement.DocumentOptions.DefaultData;
            if (rootElement.DocumentOptions.DefaultData) {
                var jsonStr = JSON.stringify(DefaultData);
                var blob = new Blob([jsonStr], { type: 'text/plain;charset=utf-8' });
                var fileName = "FlowDocument.json";
                if (navigator.msSaveBlob) {
                    navigator.msSaveBlob(blob, fileName);
                } else {
                    var link = document.createElement('a');
                    link.href = URL.createObjectURL(blob);
                    link.download = fileName;
                    link.click();
                }
                return true;
            }
            return false;
        };

        /**
        * @name SaveFlowDocumentToPDF
        * @param {function} callback 回调函数，参数为pdf的base64数据
        * @apinameZh 保存为pdf数据
        */
        rootElement.SaveFlowDocumentToPDF = function (callback) {
            //拿到页面宽度
            var pageSetting = rootElement.DocumentOptions.CalculationData.Config.PageSettings;
            //初始化jsPDF实例
            const pdf = new jspdf.jsPDF('p', 'px', [pageSetting.PaperWidth, pageSetting.PaperHeight]);
            //获取到所有的svg页面
            var svgContainer = rootElement.querySelector("[dctype=page-container]");
            svgContainer = svgContainer.cloneNode(true);
            var allSvg = svgContainer.querySelectorAll("[dctype=page]");
            //清除样式
            for (var i = 0; i < allSvg.length; i++) {
                var cloneSvg = allSvg[i];
                cloneSvg.style.border = "none";
                var typesign = cloneSvg.querySelector("[dctype=typesign]");
                typesign.remove();
                var canvas = document.createElement('canvas');
                // var scale = 1;
                canvas.width = pageSetting.PaperWidth;
                canvas.height = pageSetting.PaperHeight;
                // 绘制Canvas
                var ctx = canvas.getContext('2d');
                // ctx.scale(scale, scale);

                var data = (new XMLSerializer()).serializeToString(cloneSvg);
                var DOMURL = window.URL || window.webkitURL || window;
                var img = new Image();
                var svgBlob = new Blob([data], { type: 'image/svg+xml;charset=utf-8' });
                var url = DOMURL.createObjectURL(svgBlob);
                img.index = i;
                img.onload = function () {
                    ctx.drawImage(this, 0, 0);
                    DOMURL.revokeObjectURL(url);

                    // 转换Canvas为PNG图片
                    var png = canvas.toDataURL('image/png;quality=0.95');
                    if (this.index != 0) {
                        pdf.addPage();
                    }
                    //console.log(png)
                    pdf.addImage(png, 'png', 0, 0, pageSetting.PaperWidth, pageSetting.PaperHeight);// x, y, 宽, 高
                    if (this.index == allSvg.length - 1) {
                        var base64 = pdf.output("datauristring");
                        base64 = base64.replace(/filename=generated.pdf;/, "");
                        callback(base64);

                    }
                };
                img.src = url;
            }
        };

        /**
        * @name PrintFlowDocument
        * @apinameZh 打印
        */
        rootElement.PrintFlowDocument = function () {
            var iframe = rootElement.ownerDocument.getElementById(rootElement.id + "_IFrame_Print");
            if (iframe == null) {
                iframe = rootElement.ownerDocument.createElement("iframe");
                iframe.id = rootElement.id + "_IFrame_Print";
                iframe.style.position = "absolute";
                rootElement.appendChild(iframe);
                iframe.style.width = rootElement.offsetWidth + "px";
                iframe.style.height = rootElement.offsetHeight + "px";
                iframe.style.left = "0px";
                iframe.style.top = "0px";// (rootElement.offsetTop + 600) + "px";
                iframe.style.border = "1px solid blue";
                iframe.style.display = "";
                iframe.style.backgroundColor = "white";
                iframe.style.zIndex = 10000;
            }
            iframe.style.display = 'none';
            var targetDocument = iframe.contentDocument;
            targetDocument.open();
            targetDocument.write("");
            targetDocument.close();

            var styleElement = targetDocument.createElement("STYLE");
            styleElement.innerText = "@page{margin:0;padding:0;}";
            targetDocument.head.appendChild(styleElement);
            targetDocument.body.style.margin = "0px";
            targetDocument.title = " ";

            var allSvg = rootElement.querySelectorAll("[dctype=page]");
            if (allSvg && allSvg.length > 0) {
                for (var i = 1; i <= allSvg.length; i++) {
                    var cloneSvg = allSvg[i - 1].cloneNode(true);
                    var hasText = cloneSvg.querySelector("[dctype='typesign']");
                    cloneSvg.style.border = "none";
                    cloneSvg.style.margin = "0px";
                    hasText && hasText.remove();
                    targetDocument.body.appendChild(cloneSvg);
                }
            }
            iframe.contentWindow.onafterprint = function (e) {
                //console.log("打印完成", e);
                rootElement.InnerRaiseEvent("EventFlowAfterPrint", targetDocument);
                iframe.style.display = "none";
            };

            //打印前事件
            var result = rootElement.InnerRaiseEvent("EventFlowBeforePrint", targetDocument);
            if (result === false) {
                return iframe;
            }
            iframe.contentWindow.print();

            return iframe;
        };



        document.FlowWriterControl = rootElement;
        if (rootElement.ownerDocument !== document) {
            rootElement.ownerDocument.FlowWriterControl = rootElement;
        }
    },
    /**
    * 用于回收编辑器根元素的属性方法，方便GC释放
    * @param {HTMLElement} rootElement 编辑器根元素
    */
    DisposeDCWriterDocument: function (rootElement) {
        if (rootElement) {
            for (var prop in rootElement) {
                if (typeof rootElement[prop] === "function" && !rootElement[prop].toString().includes("[native code]")) {
                    rootElement[prop] = null;
                } else {
                    delete rootElement[prop];
                }
            }
            rootElement.remove();
        }
    },
};
