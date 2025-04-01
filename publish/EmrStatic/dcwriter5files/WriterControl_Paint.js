"use strict";

// 这里定义编辑器内容绘制代码 
// 南京都昌信息科技有限公司 2023-3-22

import { PageContentDrawer } from "./PageContentDrawer.js";
import { DCTools20221228 } from "./DCTools20221228.js";
import { WriterControl_UI } from "./WriterControl_UI.js";
import { DCBinaryReader } from "./DCTools20221228.js";
import { WriterControl_Task } from "./WriterControl_Task.js";
import { WriterControl_Print } from "./WriterControl_Print.js";
import { WriterControl_Rule } from "./WriterControl_Rule.js";


export let WriterControl_Paint = {
    /**
     * 获得转换矩阵值
     * @param {Number} a 旧矩阵值
     * @param {Number} b 旧矩阵值
     * @param {Number} c 旧矩阵值
     * @param {Number} d 旧矩阵值
     * @param {Number} e 旧矩阵值
     * @param {Number} f 旧矩阵值
     * @param {Number} operType 操作类型
     * @param {Number} value1 操作数据
     * @param {Number} value2 操作数据
     * @returns {string} 转换后的矩阵值
     */
    GetMatrixElements: function (a, b, c, d, e, f, operType, value1, value2) {
        var element = WriterControl_Paint.__MatrixElement;
        if (element == null) {
            element = document.createElement("CANVAS");
            WriterControl_Paint.__MatrixElement = element;
            //element.__DataView = new DataView(new ArrayBuffer(24));
        }
        var ctx = element.getContext("2d");
        ctx.setTransform(a, b, c, d, e, f);
        if (operType == 0) {
            ctx.translate(value1, value2);
        }
        else if (operType == 1) {
            ctx.scale(value1, value2);
        }
        else if (operType == 2) {
            ctx.rotate(value1);
        }
        else {
            throw "不支持的操作";
        }
        var mv = ctx.getTransform();
        return mv.a + "|" + mv.b + "|" + mv.c + "|" + mv.d + "|" + mv.e + "|" + mv.f;
    },
    /**
     * 创建图片
     * @param {Uint8Array} bsData 字节数组
     * @returns {string} 任务编号
     */
    BuildBitmapSrc: function (bsData) {
        if (bsData == null || bsData.length <= 4) {
            return null;
        }
        var reader = new DCBinaryReader(bsData);
        if (reader.ReadByte() != 133) {
            // 文件头不对
            return null;
        }
        var tasks = window.__DCBuiltBitmapTasks;
        if (tasks == null) {
            tasks = new Array();
            window.__DCBuiltBitmapTasks = tasks;
            tasks.IDCounter = 0;
        }
        var strTaskID = "[dcbbmp20240125$" + (tasks.IDCounter++) + "]";
        var imgWidth = reader.ReadInt16();
        var imgHeight = reader.ReadInt16();
        var temp = document.createElement("CANVAS");
        temp.width = imgWidth;
        temp.height = imgHeight;
        var drawer = new PageContentDrawer(temp, reader);
        drawer.DoubleBuffer = false;
        drawer.TaskID = strTaskID;
        drawer.EventAfterDraw = function () {
            var strData = this.CanvasElement.toDataURL();
            this.CanvasElement.remove();
            var f2 = tasks[this.TaskID];
            tasks[this.TaskID] = strData;
        };
        tasks[strTaskID] = drawer;
        drawer.AddToTask();
        return strTaskID;
    },
    /**
     * 应用HTML中的图片地址
     * @param {String} strHtml HTML字符串
     * @param {Function} callBack 转换后的回调函数
     */
    ApplyBitmapContentHtmlSrc: function (strHtml, callBack) {
        if (typeof (callBack) != "function") {
            // 参数不对，直接返回
            return;
        }
        if (strHtml == null || strHtml.length == 0 || strHtml.indexOf("[dcbbmp20240125$") < 0) {
            // 无需转换，直接返回
            callBack(strHtml);
            return;
        }
        var tasks = window.__DCBuiltBitmapTasks;
        if (tasks == null) {
            // 无需处理图片数据
            callBack(strHtml);
            return;
        }
        var bolHasTask = false;
        for (var strID in tasks) {
            var strData = tasks[strID];
            if (strHtml.indexOf(strID) >= 0) {
                if (typeof (strData) == "string") {
                    strHtml = strHtml.replace(strID, strData);
                    tasks[strID] = null;
                }
                else if (strData instanceof PageContentDrawer) {
                    bolHasTask = true;
                    break;
                }
            }
        }
        if (bolHasTask) {
            WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                for (var strID in tasks) {
                    var strData = tasks[strID];
                    if (strHtml.indexOf(strID) >= 0) {
                        if (typeof (strData) == "string") {
                            strHtml = DCTools20221228.StringReplaceAll(strID, strData, strHtml);
                            // strHtml = strHtml.replaceAll(strID, strData);
                        }
                        delete tasks[strID];
                    }
                }
                callBack(strHtml);
            });
        }
        else {
            callBack(strHtml);
        }
    },

    /**
     * 设置字体文件下载地址
     * @param {string} strFontName 字体名称
     * @param {string} strFontUrl 字体文件下载地址
     */
    SetFontSource: function (strFontName, strFontUrl) {
        if (strFontName == null || strFontName.length == 0) {
            return;
        }
        if (strFontUrl == null || strFontUrl.length == 0) {
            return;
        }
        if (document.fonts == null || typeof (document.fonts.forEach) != "function") {
            DCTools20221228.ConsoleWarring("DCWriter警告:本浏览器不支持SetFontSource功能。");
            return;
        }
        var strAgent = navigator.userAgent;
        DCTools20221228.ConsoleWarring("DCWriter警告:SetFontSource功能会显著增长网络流量和内存占用，非必要不要使用。");
        var info = DCTools20221228.ParseFontString(strFontName);
        if (info == null || info.Name == null || info.Name.length == 0) {
            return;
        }
        if (window.__DCFontSourceList == null) {
            window.__DCFontSourceList = new Array();
        }
        window.__DCFontSourceList[info.FullName] = strFontUrl;
    },
    /**
     * 确保指定的字体安装了
     * @param {string} strFontName 字体名称
     */
    EnsureFontInstall: function (strFontName) {
        if (strFontName == null || strFontName.length == 0) {
            return;
        }

    },

    /**
     * 初始化TTF文件系统
     * @param {any} strContainerID
     * @returns
     */
    InitTTFSystem: function (strContainerID, strAssemblyName) {
        if (window.__DC_TTFFiles == null) {
            var rootElement = DCTools20221228.GetOwnerWriterControl(strContainerID);
            if (rootElement == null) {
                return null;
            }
            // 获得基础路径
            var strBaseUrl = rootElement.getAttribute("TTFPath");
            if (strBaseUrl == null) {
                strBaseUrl = window.__DCWriterBaseUrl;
                if (strBaseUrl == null || strBaseUrl.length == 0) {
                    strBaseUrl = "_framework/Fonts/";
                }
                else {
                    strBaseUrl = DCTools20221228.FixResourceBasePath(strBaseUrl) + "Fonts/";
                }
            }
            else {
                strBaseUrl = DCTools20221228.FixResourceBasePath(strBaseUrl);
            }

            if (strBaseUrl == null || strBaseUrl.length == 0) {
                // 没有找到基础路径
                return null;
            }
            jQuery.ajax(
                strBaseUrl + "font-list.json",
                { async: true, method: "GET", type: "GET" }).done(function (data, textStatus, jqXHR) {
                    var fontList = data;
                    if (typeof (data) == "string") {
                        fontList = JSON.parse(data);
                    }
                    fontList.BaseUrl = strBaseUrl;
                    for (var iCount = 0; iCount < fontList.length; iCount++) {
                        // 注册TTF字体文件信息
                        var fontItem = fontList[iCount];
                        DotNet.invokeMethod(
                            window.DCWriterEntryPointAssemblyName,
                            "RegisterTTF",
                            fontItem.FontName,
                            strBaseUrl + fontItem.FileName);
                    }
                    window.__DC_TTFFiles = fontList;
                });
        }
    },

    DownloadTTFFile: function (strContainerID, strFontName) {
        if (window.__DC_TTFFiles != null) {

        }
    },

    BuildBackgroundImageAndWaterMark: function (strContainerID) {

    },

    /**
     * 获取指定模式下的单元格斜分线Base64图片数据 wyc20240409
     * @param {string} mode 单元格斜分线的模式
     * @param {number} width 单元格的宽度
     * @param {number} height 单元格的高度
     * @param {number} color 单元格框线的颜色
     */
    GetCellDiagonalBase64Image(mode, _width, _height, _color) {
        if (!mode || !_width || !_height) return;// 如果输入参数缺失，则返回
        var temporary_canvas = document.createElement('canvas');// 创建一个临时的canvas用于绘制斜分线
        var temporary_ctx = temporary_canvas.getContext('2d');// 获取临时canvas的上下文
        temporary_canvas.width = _width;// 设置临时canvas的宽度
        temporary_canvas.height = _height;// 设置临时canvas的高度
        temporary_ctx.strokeStyle = _color; //设置框线颜色
        switch (mode) {
            case "TopLeftOneLine":
                // 从左上角到右下角[0, 0]=>[_width, _height]
                temporary_ctx.moveTo(0, 0);
                temporary_ctx.lineTo(_width, _height);
                temporary_ctx.stroke();
                break;
            case "TopLeftTwoLines":
                // 从左上角到右边中心位置[0, 0]=>[_width, _height / 2]
                temporary_ctx.moveTo(0, 0);
                temporary_ctx.lineTo(_width, _height / 2);
                temporary_ctx.stroke();
                // 从左上角到下边中心位置[0, 0]=>[_width / 2, _height]
                temporary_ctx.moveTo(0, 0);
                temporary_ctx.lineTo(_width / 2, _height);
                temporary_ctx.stroke();
                break;
            case "TopRightOneLine":
                // 从右上角到左下角[_width, 0]=>[0, _height]
                temporary_ctx.moveTo(_width, 0);
                temporary_ctx.lineTo(0, _height);
                temporary_ctx.stroke();
                break;
            case "TopRightTwoLines":
                // 从右上角到左边中心位置[_width, 0]=>[0, _height / 2]
                temporary_ctx.moveTo(_width, 0);
                temporary_ctx.lineTo(0, _height / 2);
                temporary_ctx.stroke();
                // 从右上角到下边中心位置[_width, 0]=>[0, _width / 2, _height]
                temporary_ctx.moveTo(_width, 0);
                temporary_ctx.lineTo(_width / 2, _height);
                temporary_ctx.stroke();
                break;
            case "BottomRightOneLine":
                // 从右下角到左上角[_width, _height]=>[0, 0]
                temporary_ctx.moveTo(_width, _height);
                temporary_ctx.lineTo(0, 0);
                temporary_ctx.stroke();
                break;
            case "BottomRigthTwoLines":
                // 从右下角到左边中心位置[_width, _height]=>[0, _height / 2]
                temporary_ctx.moveTo(_width, _height);
                temporary_ctx.lineTo(0, _height / 2);
                temporary_ctx.stroke();
                // 从右下角到上边中心位置[_width, _height]=>[_width / 2, 0]
                temporary_ctx.moveTo(_width, _height);
                temporary_ctx.lineTo(_width / 2, 0);
                temporary_ctx.stroke();
                break;
            case "BottomLeftOneLine":
                // 从左下角到右上角[0, _height]=>[_width, 0]
                temporary_ctx.moveTo(0, _height);
                temporary_ctx.lineTo(_width, 0);
                temporary_ctx.stroke();
                break;
            case "BottomLeftTwoLines":
                // 从左下角到右边中心位置[0, _height]=>[_width, _height / 2]
                temporary_ctx.moveTo(0, _height);
                temporary_ctx.lineTo(_width, _height / 2);
                temporary_ctx.stroke();
                // 从左下角到上边中心位置[0, _height]=>[_width / 2, 0]
                temporary_ctx.moveTo(0, _height);
                temporary_ctx.lineTo(_width / 2, 0);
                temporary_ctx.stroke();
                break;
            default:
                return null;
                break;
        }
        // 返回临时canvas的Base64图片数据
        var resultstr = temporary_canvas.toDataURL();
        //试图立刻释放canvas
        temporary_canvas.width = temporary_canvas.height = 0;
        temporary_ctx.fillRect(0, 0, 0, 0);
        if (resultstr && resultstr.length && resultstr.length > 0) {
            return resultstr;
        }
        return null;
    },


    /**
     * 绘制BMP图像内容
     * @param {string} strCanvasElementID 使用的画布对象元素编号
     * @param {number} bmpWidth 图片宽度
     * @param {number} bmpHeight 图片高度
     * @param {Uint8Array} bsData 图片绘图数据
     * @returns 画布元素编号
     */
    DrawBitmapContent: function (strCanvasElementID, bmpWidth, bmpHeight, bsData) {
        var element = null;
        if (strCanvasElementID == null || strCanvasElementID.length == 0) {
            strCanvasElementID = "img_" + new Date().valueOf() + "_" + Math.random();
            element = document.createElement(strCanvasElementID);
            element.id = strCanvasElementID;
        }
        else {
            element = document.getElementById(strCanvasElementID);
        }
        if (element == null) {
            element = document.createElement("CANVANS");
            element.width = bmpWidth;
            element.height = bmpHeight;
        }
        var drawer = new PageContentDrawer(element, bsData);
        drawer.AddToTask();
        return strCanvasElementID;
    },
    /**
     * 获得文档的背景图片数据
     * @param {string | HTMLElement} rootElement 编辑器对象
     * @param {Function} callBack 获得背景图片数据的回调函数
     * @returns { boolean} 操作是否成功
     */
    GetDocumentBackgroundImageData: function (rootElement, callBack) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return false;
        }
        var data2 = rootElement.__DCWriterReference.invokeMethod("GetDefaultBackgroundGraphicsData");
        if (data2 != null && data2.length > 0) {
            var reader = new DCBinaryReader(data2);
            var pageWidth = reader.ReadInt32();
            var pageHeight = reader.ReadInt32();
            var tempElement = rootElement.ownerDocument.createElement("canvas");
            tempElement.width = pageWidth;
            tempElement.height = pageHeight;
            var drawer = new PageContentDrawer(tempElement, reader);
            drawer.TypeName = "UpdateViewForWaterMark";
            drawer.EventAfterDraw = function () {
                var strImageData = tempElement.toDataURL("image/png");
                tempElement.remove();
                callBack(strImageData);
            };
            drawer.AddToTask();
            return true;
        }
        return false;
    },
    /**
    * 为水印而更新显示
    * @param {HTMLElement} rootElement 根对象
    */
    UpdateViewForWaterMark: function (rootElement) {
        rootElement.__BackgroundImageElement = null;
        if (rootElement.getAttribute("dctype") == "WriterPrintPreviewControlForWASM") {
            // 打印预览控件不处理
            return;
        }
        //rootElement.__BackgroundDrawer = null;
        if (rootElement.__BackgroundImageElement != null) {

        }
        var styleElement = DCTools20221228.GetChildNodeByDCType(rootElement, "style_bkimg");
        var data2 = rootElement.__DCWriterReference.invokeMethod("GetDefaultBackgroundGraphicsData");
        if (data2 != null && data2.length > 0) {
            var reader = new DCBinaryReader(data2);
            var pageWidth = reader.ReadInt32();
            var pageHeight = reader.ReadInt32();
            var tempElement = rootElement.ownerDocument.createElement("canvas");
            tempElement.width = pageWidth;
            tempElement.height = pageHeight;
            var drawer = new PageContentDrawer(tempElement, reader);
            drawer.TypeName = "UpdateViewForWaterMark";
            drawer.EventAfterDraw = function () {
                rootElement.__BackgroundImageElement = tempElement;
                var styleElement2 = DCTools20221228.GetChildNodeByDCType(rootElement, "style_bkimg");
                if (styleElement2 == null) {
                    styleElement2 = rootElement.ownerDocument.createElement("style");
                    styleElement2.setAttribute("dctype", "style_bkimg");
                    rootElement.insertBefore(styleElement2, rootElement.firstChild);
                }
                if (rootElement.__DCWriterReference.invokeMethod("GetNormalViewMode") == true) {
                    // 对于普通视图模式，则背景图片重复平铺
                    styleElement2.innerText = "." + rootElement.__BKImgStyleName + "{background-repeat:repeat;background-image:url(" + tempElement.toDataURL("image/png") + ")}";
                }
                else {
                    styleElement2.innerText = "." + rootElement.__BKImgStyleName + "{background-repeat:no-repeat;background-image:url(" + tempElement.toDataURL("image/png") + ")}";
                }
                tempElement.remove();
            };
            drawer.AddToTask();
        }
        else if (styleElement != null) {
            rootElement.removeChild(styleElement);
        }
    },


    /**
     * 绘制可逆图形
     * @param {string} containerID 容器元素编号
     * @param {number} intPageIndex 页码
     * @param {number} intX1  区域坐标
     * @param {number} intY1 区域坐标
     * @param {number} intX2 区域坐标
     * @param {number} intY2 区域坐标
     * @param {number} intType 图形类型
     */
    ReversibleDraw: function (containerID, intPageIndex, intX1, intY1, intX2, intY2, intType) {
        var pageElement = WriterControl_Paint.GetCanvasElementByPageIndex(containerID, intPageIndex);
        if (pageElement != null) {
            intX1 = intX1 + pageElement.offsetLeft;
            intY1 = intY1 + pageElement.offsetTop;
            intX2 = intX2 + pageElement.offsetLeft;
            intY2 = intY2 + pageElement.offsetTop;
            var div = WriterControl_Paint.GetReversibleDiv(containerID);
            if (div.parentNode != pageElement.parentNode) {
                pageElement.parentNode.appendChild(div);
            }
            div.__PageElement = pageElement;
            //div.__EventCancel = function () {
            //    DCTools20221228.GetOwnerWriterControl(containerID).invokeMethod("FinishedMouseCapture");
            //};
            div.style.display = "";
            if (intType == 0) { // 直线
                div.style.border = "none";
                div.style.backgroundColor = "rgba( 0 , 0 , 255, 0.6)";
                if (intX1 == intX2) {
                    // 竖线
                    div.style.left = intX1 + "px";
                    div.style.top = intY1 + "px";
                    div.style.width = "3px";
                    div.style.height = (intY2 - intY1) + "px";
                    div.style.cursor = "col-resize";
                }
                else if (intY1 == intY2) {
                    // 横线
                    div.style.left = intX1 + "px";
                    div.style.top = intY1 + "px";
                    div.style.width = (intX2 - intX1) + "px";
                    div.style.height = "3px";
                    div.style.cursor = "row-resize";
                }
                else {
                    // 斜线
                }
            }
            else if (intType == 1) {//矩形边界
                div.style.border = "1px solid blue";
                div.style.backgroundColor = "transparent";
                div.style.left = intX1 + "px";
                div.style.top = intY1 + "px";
                div.style.width = (intX2 - intX1) + "px";
                div.style.height = (intY2 - intY1) + "px";
                div.style.cursor = "nwse-resize";
            }
            else if (intType == 2) {// 椭圆形边界

            }
            else if (intType == 3) { // 矩形区域
                div.style.border = "none";
                div.style.backgroundColor = "rgba( 0 , 0 , 255, 0.6)";
                div.style.left = intX1 + "px";
                div.style.top = intY1 + "px";
                div.style.width = (intX2 - intX1) + "px";
                div.style.height = (intY2 - intY1) + "px";
                div.style.cursor = "nwse-resize";
            }
        }
    },
    /** 关闭可逆图形 */
    CloseReversibleShape: function (containerID) {
        var div = WriterControl_Paint.GetReversibleDiv(containerID);
        if (div != null) {
            div.__PageElement = null;
            div.style.display = "none";
            div._StartX = -1000;
            div._StartY = -1000;
            div._FinishedCallback = null;
        }
    },
    /** 判断是否正在绘制可逆图形
     * @returns {boolean} 是否正在绘制可逆图形
     * */
    IsDrawingReversibleShape: function (containerID) {
        var div = WriterControl_Paint.GetReversibleDiv(containerID, false);
        return div != null && div.style.display != "none";
    },
    GetReversibleDiv: function (containerID, bolAutoCreate) {
        var doc = document;
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement != null) {
            doc = rootElement.ownerDocument;
        }
        var div = doc.getElementById("divReversible20230104");
        if (div == null && bolAutoCreate != false) {
            div = doc.createElement("DIV");
            div.id = "divReversible20230104";
            div.style.border = "1px solid black";
            div.style.backgroundColor = "rgba(0,0,255,0.8)";
            div.style.position = "absolute";
            div.style.zIndex = 100000;
            div.style.display = "none";
            doc.body.appendChild(div);
            var funcMouseEvent = function (e) {
                var page = this.__PageElement;
                if (page != null && page.parentNode.parentNode.__DCWriterReference != null) {
                    page.parentNode.parentNode.__DCWriterReference.invokeMethod(
                        "EditorHandleMouseEvent",
                        page.PageIndex,
                        e.type,
                        e.altKey,
                        e.shiftKey,
                        e.ctrlKey,
                        e.offsetX + this.offsetLeft - page.offsetLeft,
                        e.offsetY + this.offsetTop - page.offsetTop,
                        e.buttons,
                        e.detail);
                }
            };
            div.addEventListener('mousemove', funcMouseEvent);
            div.addEventListener('mouseup', funcMouseEvent);
        }


        //if (containerID != null && containerID.length > 0) {
        //    var ce = document.getElementById(containerID);
        //    if (ce != null) {
        //        if (div.parentNode != ce) {
        //            ce.appendChild(div);
        //        }
        //    }
        //}
        return div;
    },
    AddTaskHandleScrollView: function (containerID, allowDrawAll) {
        var task = {
            TypeName: "TaskHandleScrollView",
            OwnerWriterControl: DCTools20221228.GetOwnerWriterControl(containerID),
            CanEatTask: function (task2) {
                return task2.TypeName == this.TypeName
                    && task2.OwnerWriterControl == this.OwnerWriterControl;

            },
            RunTask: function () {
                WriterControl_Paint.HandleScrollView(this.OwnerWriterControl, allowDrawAll);
            }
        };
        WriterControl_Task.AddTask(task);
    },
    /**
     * 处理视图滚动事件
     * @param {string} containerID 容器元素编号
     */
    HandleScrollView: function (containerID, allowDrawAll) {
        if (containerID == null) {
            return;
        }
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return;
        }
        if (rootElement.__DCDisposed == true) {
            return;
        }
        DCTools20221228.EnsureNativeReference(rootElement);
        var isWriterPrintPreviewControlForWASM = rootElement.getAttribute("dctype") == "WriterPrintPreviewControlForWASM";
        var canvas = WriterControl_UI.GetPageCanvasElements(rootElement);
        if (canvas != null && canvas.length > 0) {
            for (var pageIndex = 0; pageIndex < canvas.length; pageIndex++) {
                var element = canvas[pageIndex];
                if (DCTools20221228.IsInVisibleArea(element) == true) {
                    if (element._isRendered != true) {
                        //var tickLoad = new Date().valueOf();
                        //tickLoad = new Date().valueOf() - tickLoad;
                        //var tickRender = new Date().valueOf();
                        //预览控件走自己的滚动逻辑ui.js=>PageContainerOnscrollFunc
                        if (isWriterPrintPreviewControlForWASM !== true) {
                            var drawer = new PageContentDrawer(element);
                            //drawer.TypeName = "DrawPageForHandleScrollView";
                            drawer.PageIndex = pageIndex;
                            drawer.AllowClip = true;
                            if (element._NeedClear == true) {
                                // 如果该页面需要全部绘制，则设置双缓冲
                                drawer.EnableDoubleBuffer(0, 0, element.width, element.height);
                            }
                            drawer.CanEatTask = function (otherTask) {
                                // 判断能否吞并其它绘图任务
                                if (this.TypeName == otherTask.TypeName
                                    && this.CanvasElement == otherTask.CanvasElement) {
                                    return true;
                                }
                                return false;
                            };
                            drawer.EventQueryCodes = function () {
                                var e9 = this.CanvasElement;
                                if (DCTools20221228.IsInVisibleArea(e9) == false) {
                                    // 不在可视区域，不绘制
                                    return null;
                                }
                                while (e9 != null) {
                                    if (e9.__DCWriterReference != null) {
                                        var strData = e9.__DCWriterReference.invokeMethod(
                                            "RenderPageContent",
                                            this.PageIndex);
                                        return strData;
                                    }
                                    e9 = e9.parentNode;
                                }
                                return null;
                            };
                            drawer.EventAfterDraw = function () {
                                // 绘制完毕后设置元素状态
                                this.CanvasElement._isRendered = true;
                                this.CanvasElement._NeedClear = false;
                            };
                            drawer.AddToTask();
                        }
                        //tickRender = new Date().valueOf() - tickRender;
                        //console.log("加载图形数据毫秒:" + tickLoad + ",绘图毫秒:" + tickRender);
                    }
                    //else {
                    //    WriterControl_Paint.UpdatePageInvalidateView(element);
                    //}
                }
            }
        }
        if (isWriterPrintPreviewControlForWASM == false) {
            WriterControl_Paint.NeedUpdateView(rootElement, allowDrawAll);
        }

        var hasNotScrollWithCaret = rootElement.getAttribute('notscrollwithcaret');
        if (typeof hasNotScrollWithCaret == 'string' && hasNotScrollWithCaret.toLowerCase() == 'true') {
            WriterControl_UI.CaretWithinVisibleArea(rootElement);
        }
    },

    /**
     * 清空画布对象的内容
     * @param {HTMLCanvasElement} element 画布元素对象
     */
    ClearCanvasElement: function (element) {
        //var ctx = element.getContext("2d");
        element._isRendered = false;
        if (DCTools20221228.IsInVisibleArea(element) == false) {
            DCTools20221228.ClearCanvasElementContent(element);
            //var ctx = element.getContext("2d");
            //if (typeof (ctx.reset) == "function") {
            //    ctx.reset();
            //} else {
            //    ctx.clearRect(0, 0, element.width, element.height);
            //}
            element._NeedClear = false;
        }
        else {
            element._NeedClear = true;
        }
        //ctx.font = "20pt 宋体";
        //ctx.fillText("正在加载内容...", 10, 30);
    },
    /**
     * 判断标准图标列表是否就绪
     * @returns
     */
    IsStandardImageListReady: function () {
        var imgs = window.__DCStandardImageList;
        if (imgs != null && imgs.length > 0) {
            for (var imgIndex = 0; imgIndex < imgs.length; imgIndex++) {
                var img2 = imgs[imgIndex];
                if (img2 != null && img2.complete == false) {
                    return false;
                }
            }
            return true;
        }
        return false;
    },

    /** 刷新标准图标列表 */
    RefreshStandardImageList: function () {
        window.__DCStandardImageList = null;
        var imgDatas = DotNet.invokeMethod(
            window.DCWriterEntryPointAssemblyName,
            "GetStandardImageData");
        if (imgDatas != null && imgDatas.length > 0) {
            var imgList = new Array();
            for (var iCount = 0; iCount < imgDatas.length; iCount++) {
                var strImage = imgDatas[iCount];
                if (strImage != null && strImage.length > 0) {
                    var imgElemnent = document.createElement("IMG");// new Image();
                    imgElemnent.loading = "eager";
                    imgElemnent.src = strImage;
                    imgList.push(imgElemnent);
                    imgElemnent.onload = function () {
                        // 创建一个画布并设置大小与图片相同
                        this.onload = null;
                        var canvas = document.createElement("canvas");
                        canvas.width = this.width;
                        canvas.height = this.height;
                        // 获取2D渲染上下文
                        var ctx = canvas.getContext("2d");
                        // 将图片绘制到画布上
                        ctx.drawImage(this, 0, 0);
                        // 获取画布上的所有像素数据
                        var imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);
                        var data = imageData.data;
                        // 遍历像素数据，将每个像素的alpha通道（透明度）设置为0
                        for (var i = 0; i < data.length; i += 4) {
                            if (data[i] == 255 && data[i + 1] == 255 && data[i + 2] == 255) {
                                data[i + 3] = 0;
                            }
                        }
                        ctx.clearRect(0, 0, canvas.width, canvas.height);
                        // 将更新后的像素数据放回画布
                        ctx.putImageData(imageData, 0, 0);
                        //var strImage2 = canvas.toDataURL("image/png");
                        //imgElemnent.src = strImage2;
                        var imgIndex = window.__DCStandardImageList.indexOf(this);
                        if (imgIndex >= 0) {
                            window.__DCStandardImageList.splice(imgIndex, 1, canvas);
                        }
                        //var bsData = canvas.toBlob("image/png");
                        //var imgIndex = window.__DCStandardImageList.indexOf(imgElemnent);
                        //if (imgIndex >= 0) {
                        //    createImageBitmap(bsData).then(function (response) {
                        //        window.__DCStandardImageList.splice(imgIndex, 1, response);
                        //    });
                        //}
                    };

                }
                else {
                    imgList.push(null);
                }
            }
            window.__DCStandardImageList = imgList;
        }
    },
    /**
     * 声明所有的页面内容视图无效，需要全部重新绘制
     * @param {string | HTMLDivElement} containerID 容器元素编号
     */
    InvalidateAllView: function (containerID) {
        DCTools20221228.EnsureNativeReference(containerID);
        var pages = WriterControl_UI.GetPageCanvasElements(containerID);
        if (pages != null) {
            for (var iCount = 0; iCount < pages.length; iCount++) {
                var element = pages[iCount];
                if (element._isRendered == true) {
                    element._isRendered = false;
                    WriterControl_Paint.ClearCanvasElement(element);
                    //var ctx = element.getContext("2d");
                    //ctx.reset();
                    //element._isRendered = false;
                    //element._NeedClear = false;
                }
            }
            WriterControl_Task.ClearTask(
                "PageContentDrawer",
                DCTools20221228.GetOwnerWriterControl(containerID));
            WriterControl_Task.AddTask(function () {
                WriterControl_Paint.HandleScrollView(containerID, true);
            });
            //window.setTimeout(WriterControl_Paint.HandleScrollView, 50, containerID, true);
        }
    },
    /**
     * 强制声明所有的页面内容视图无效，需要全部重新绘制
     * @param {string | HTMLDivElement} containerID 容器元素编号
     */
    InvalidateAllViewForce: function (containerID) {
        var pages = WriterControl_UI.GetPageCanvasElements(containerID);
        if (pages != null) {
            for (var iCount = 0; iCount < pages.length; iCount++) {
                var element = pages[iCount];
                element._isRendered = false;
                element._NeedClear = false;
                var ctx = element.getContext("2d");
                if (typeof (ctx.reset) == "function") {
                    ctx.reset();
                } else {
                    ctx.clearRect(0, 0, element.width, element.height);
                }
            }
            WriterControl_Task.AddTask(function () {
                WriterControl_Paint.HandleScrollView(containerID, true);
            });
        }
    },
    /**接到通知需要更新用户界面 */
    NeedUpdateView: function (strContainerID, bolAllowDrawAll) {
        DCTools20221228.EnsureNativeReference(strContainerID);
        // 注释多余重新绘制所有打印预览的代码
        // var rootElement = DCTools20221228.GetOwnerWriterControl(strContainerID);
        // if (rootElement != null && WriterControl_Print.IsInPrintPreview(rootElement) == true) {
        //     // 处于打印预览模式，则重新绘制所有打印预览的内容
        //     WriterControl_Print.PrintPreviewInvalidateAllView(rootElement);
        // }
        var task = {
            OwnerWriterControl: DCTools20221228.GetOwnerWriterControl(strContainerID),
            TypeName: "NeedUpdateView",
            AllowDrawAll: bolAllowDrawAll,
            CanEatTask: function (task2) {
                if (task2.TypeName == this.TypeName) {
                    if (task2.OwnerWriterControl == this.OwnerWriterControl) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                return false;// return task2.TypeName == this.TypeName && task2.ContainerID == this.ContainerID;
            },
            RunTask: function () {
                var elements = WriterControl_UI.GetPageCanvasElements(this.OwnerWriterControl);
                if (elements == null || elements.length == 0) {
                    return;
                }
                var drawer = new PageContentDrawer();
                drawer.OwnerWriterControl = this.OwnerWriterControl;
                drawer.AllowDrawAll = this.AllowDrawAll;
                drawer.OnDeleted = function (src) {
                };
                drawer.EventQueryCodes = function () {
                    var ctl = DCTools20221228.GetOwnerWriterControl(this.OwnerWriterControl);
                    if (ctl.__DCDisposed == true) {
                        return null;
                    }
                    var strPageIndexs = "";
                    for (var iCount = 0; iCount < elements.length; iCount++) {
                        var element = elements[iCount];
                        if (DCTools20221228.IsInVisibleArea(element) == true) {
                            strPageIndexs = strPageIndexs + "," + element.PageIndex;
                        }
                    }
                    var codes = ctl.__DCWriterReference.invokeMethod("GetInvalidateViewData", strPageIndexs);
                    if (codes == null || codes.length == 0) {
                        return null;
                    }
                    if (codes[0] == 0xfe) {
                        if (this.AllowDrawAll != false) {
                            // 判断出需要重新绘制所有的内容，不在这里进行绘制
                            // var thisTypeName = this.TypeName;
                            WriterControl_Task.ClearTask(this.TypeName, ctl);
                            WriterControl_Paint.InvalidateAllView(ctl);
                            WriterControl_Print.PrintPreviewInvalidateAllView(ctl);
                        }
                        return null;
                    }
                    var reader = new DCBinaryReader(codes);
                    if (reader.ReadBoolean()) {
                        // 还有其他区域需要进行绘制
                        this.RootElement = ctl;
                        this.EventAfterDraw = function () {
                            WriterControl_Paint.NeedUpdateView(this.RootElement, true);
                        };
                    }

                    var vPageIndex = reader.ReadInt16(); // 读取页码
                    this.CanvasElement = elements[vPageIndex];
                    if (this.CanvasElement == null) {
                        // 页码不对
                        return null;
                    }
                    if (this.CanvasElement._isRendered == false) {
                        // 该页面从未绘制，则在这里不绘制部分内容，而是留到以后绘制整页。
                        return null;
                    }
                    ////记录下旧的批注数量
                    //var ctl = DCTools20221228.GetOwnerWriterControl(this.ContainerID);
                    ////在此处获取一次批注的数量
                    //var newLength = ctl.getCommentList();
                    //newLength = newLength ? newLength.length : 0;
                    //if (WriterControl_Paint.commentListLength && WriterControl_Paint.commentListLength > newLength) {
                    //    ctl.RefreshDocument();
                    //    if (newLength == 0) {
                    //        if (ctl.oldCaretOption) {
                    //            WriterControl_UI.ShowCaret(
                    //                ctl.oldCaretOption.containerID,
                    //                ctl.oldCaretOption.intPageIndex,
                    //                ctl.oldCaretOption.intDX,
                    //                ctl.oldCaretOption.intDY,
                    //                ctl.oldCaretOption.intWidth,
                    //                ctl.oldCaretOption.intHeight,
                    //                ctl.oldCaretOption.bolVisible,
                    //                ctl.oldCaretOption.bolReadonly
                    //            )
                    //        }
                    //    }
                    //    WriterControl_Paint.commentListLength = newLength;
                    //    return null;
                    //}
                    //WriterControl_Paint.commentListLength = newLength;

                    if (this.CanvasElement._NeedClear == true) {
                        this.CanvasElement._NeedClear = false;
                        this.SetClearRectangle(0, 0, this.CanvasElement.width, this.CanvasElement.height);
                    }
                    var doubleBuffer = reader.ReadBoolean();
                    var vLeft = reader.ReadInt32();
                    var vTop = reader.ReadInt32();
                    var vWidth = reader.ReadInt32();
                    var vHeight = reader.ReadInt32();
                    if (doubleBuffer) {
                        // 用缓冲区减少闪烁
                        if (ctl.TempElementForDoubleBuffer == null) {
                            ctl.TempElementForDoubleBuffer = this.CanvasElement.ownerDocument.createElement("CANVAS");
                            ctl.TempElementForDoubleBuffer.style.boxSizing = "content-box";
                        }
                        //if (this.CanvasElement.computedStyleMap) {
                        //    var rs = this.CanvasElement.computedStyleMap();
                        //    var v99 = rs.get("border");
                        //    ctl.TempElementForDoubleBuffer.style.border = v99;
                        //}
                        this.TempElementForDoubleBuffer = ctl.TempElementForDoubleBuffer;
                        var ctx3 = this.TempElementForDoubleBuffer.getContext("2d");
                        ctx3.clearRect(vLeft, vTop, vWidth, vHeight);
                        this.EnableDoubleBuffer(vLeft, vTop, vWidth, vHeight);
                        this.AllowClip = true;
                    }
                    else {
                        // 不使用双缓冲
                        this.SetClearRectangle(vLeft, vTop, vWidth, vHeight);
                        this.AllowClip = true;
                    }

                    return reader;
                };
                drawer.EventAfterDraw = function () {
                    this.CanvasElement._isRendered = true;
                };
                drawer.AddToTask();
            }
        };
        WriterControl_Task.AddTask(task);
    },
    /**
     * 根据页码获得画布对象
     * @param {string} containerID 容器元素编号
     * @param {Number} intPageIndex
     * @returns {HTMLCanvasElement} 画布对象
     */
    GetCanvasElementByPageIndex: function (containerID, intPageIndex) {
        var es = WriterControl_UI.GetPageCanvasElements(containerID);

        if (es != null && es.length > 0 && intPageIndex >= 0) {
            for (var iCount = 0; iCount < es.length; iCount++) {
                if (es[iCount].PageIndex == intPageIndex) {
                    return es[iCount];
                }
            }
            return null;
            //return es[intPageIndex];
        }
        else {
            return null;
        }
    },
    /**
     * 设置元素大小
     * @param {HTMLElement} rootElement 根元素对象
     * @param {HTMLCanvasElement} element 画布元素
     */
    SetPageElementSize: function (rootElement, element, changezoom, length) {
        if (rootElement.__DCDisposed == true) {
            return;
        }
        var baseZoomRate = rootElement.__DCWriterReference.invokeMethod("get_WASMBaseZoomRate");
        var zoomRate = rootElement.__DCWriterReference.invokeMethod("get_ZoomRate");
        //判断标尺是否存在
        var ruleVisible = rootElement.ruleVisible;
        var hasRule = 0;
        if (ruleVisible == "true") {
            hasRule = 24;
        }
        //判断是否存在修改大小的方法
        var hasAutoZoom = rootElement.getAttribute("autozoom");
        var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
        var hasCanvas = pageContainer.querySelectorAll('canvas[dctype=page]');
        if (typeof hasAutoZoom == "string" && hasAutoZoom.toLowerCase().trim() == "true" && changezoom && hasCanvas.length == length) {
            if (hasCanvas.length > 0) {
                var pageWidth = pageContainer.offsetWidth - 12;
                var pageHeight = pageContainer.offsetHeight;
                //判断是否存在标尺
                if (ruleVisible) {
                    var hrule = rootElement.querySelector('[dctype="hrule"]');
                    var vrule = rootElement.querySelector('[dctype="vrule"]');
                    if (!hrule && !vrule) {
                        var pageWidth = pageContainer.clientWidth - 12 - hasRule;
                        var pageHeight = pageContainer.clientHeight - hasRule;
                    }
                }
                //获取到设置的宽度
                var canvasWidth = Number(element.getAttribute('native-width'));
                //var zoomRate = rootElement.GetZoomRate();
                zoomRate = Math.floor(((pageWidth) / canvasWidth) * 10000) / 10000;
                //判断是否存在滚动条
                var allCanvasHeight = ((Number(element.getAttribute('native-height')) + 12) * zoomRate) * hasCanvas.length;
                if (pageHeight <= allCanvasHeight) {
                    pageWidth = pageWidth - 20;
                    zoomRate = Math.floor((pageWidth / canvasWidth) * 10000) / 10000;
                }
                if (zoomRate <= 0) {
                    zoomRate = rootElement.__DCWriterReference.invokeMethod("get_ZoomRate");
                }
                WriterControl_UI.EditorSetZoomRate(rootElement, zoomRate);
            }
        }
        var w = parseInt(element.getAttribute("native-width"));
        var h = parseInt(element.getAttribute("native-height"));
        if (element.nodeName == "CANVAS") {
            element.width = Math.ceil(w * zoomRate * baseZoomRate);
            element.height = Math.ceil(h * zoomRate * baseZoomRate);
        } else {
            // 处理svg打印预览的缩放【DUWRITER5_0-3312】
            // 修改svg打印预览不被浏览器和系统的缩放影响，修复打印预览界面显示过大的问题
            element.setAttribute("width", Math.ceil(w * zoomRate) + "px");
            element.setAttribute("height", Math.ceil(h * zoomRate) + "px");
            element.setAttribute("viewBox", "0 0 " + w + " " + h);
        }
        if (baseZoomRate == 1) {
            element.style.width = "";
        } else {
            var rs = (window.getComputedStyle && window.getComputedStyle(element)) || null;
            var boxSizingStr = '';
            if (rs && rs.getPropertyValue) {
                boxSizingStr = rs.getPropertyValue("box-sizing");
            }
            var sizeFix = 0;
            // 修复浏览器缩放或者系统屏幕缩放时编辑器文字模糊问题【WriterControl_Paint.SetPageElementSize】
            if (boxSizingStr == "border-box") {
                // width 和 height 属性包括内容，内边距和边框，但不包括外边距
                var w1 = '';
                var w2 = '';
                if (rs && rs.getPropertyValue) {
                    w1 = rs.getPropertyValue("border-left-width");
                    w2 = rs.getPropertyValue("border-right-width");
                }
                if (isNaN(w1)) {
                    sizeFix += parseFloat(w1);
                }
                if (isNaN(w2)) {
                    sizeFix += parseFloat(w2);
                }
            }
            element.style.width = ((element.width / baseZoomRate) + sizeFix) + "px";
        }
        //element.style.height = (Math.round(h * zoomRate) + sizeFix) + "px";
    },
    /**
     * 删除所有的页元素
     * @param {string | HTMLElement} rootElement
     */
    RemoveAllPageElements: function (rootElement) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement != null) {
            var elements = WriterControl_UI.GetPageCanvasElements(rootElement);
            if (elements != null && elements.length > 0) {
                for (var iCount = 0; iCount < elements.length; iCount++) {
                    elements[iCount].remove();
                }
            }
        }
    },
    /**
     * 更新页面框架
     * @param {string} containerID 容器元素编号
     * @param {string} strCodes 页面框架的JSON字符串
     */
    UpdatePages: function (containerID, strCodes) {

        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            throw "UpdatePages:未找到元素" + containerID;
            //rootElement = document.body;
        }
        if (rootElement.__DCDisposed == true) {
            return;
        }
        DCTools20221228.EnsureNativeReference(rootElement);
        var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
        if (pageContainer == null) {
            throw "页面容器元素未找到";
        }
        var backScrollLeft = pageContainer.scrollLeft;
        var backScrollTop = pageContainer.scrollTop;
        var pageLayoutChanged = false;
        var elements = WriterControl_UI.GetPageCanvasElements(rootElement);
        var codes = JSON.parse(strCodes);
        var styleElement = rootElement.ownerDocument.getElementById("__dcpagecss" + rootElement.id);
        if (styleElement != null) {
            styleElement.parentNode.removeChild(styleElement);
        }
        styleElement = rootElement.ownerDocument.createElement("STYLE");
        styleElement.setAttribute("dctype", "pagecss");
        styleElement.id = "__dcpagecss" + rootElement.id;
        styleElement.innerText = " .pagecss" + rootElement.id + "{" + codes[0] + "}";
        codes.shift();
        if (rootElement == document.body) {
            rootElement.appendChild(styleElement);
        }
        else if (rootElement.firstChild != null) {
            rootElement.insertBefore(styleElement, rootElement.firstChild);
        }
        else {
            rootElement.appendChild(styleElement);
        }
        var zoomRate = codes[0];
        codes.shift();
        var targetElementsCount = codes.length;
        //当会存在codes数组长度为2的情况，此处不能进行删除，保证存在一个canvas元素 
        if (targetElementsCount != 0 && elements.length > targetElementsCount) {
            // 画布元素过多，删除多余的
            for (var iCount = elements.length - 1; iCount >= targetElementsCount; iCount--) {
                elements[iCount].parentNode.removeChild(elements[iCount]);
                pageLayoutChanged = true;
            }
        }
        var elementChanged = false;
        var bolForceRefresh = false;
        if (window.devicePixelRatio != rootElement.___DCLastdevicePixelRatio) {
            // 屏幕分辨率改变了，强制刷新视图
            bolForceRefresh = true;
            rootElement.___DCLastdevicePixelRatio = window.devicePixelRatio;
            pageLayoutChanged = true;
        }
        var baseZoomRate = window.devicePixelRatio;
        rootElement.__DCWriterReference.invokeMethod("set_WASMBaseZoomRate", baseZoomRate);
        var bolNormalViewMode = rootElement.__DCWriterReference.invokeMethod("GetNormalViewMode");
        for (var iCount = 0; iCount < codes.length; iCount++) {
            var pageInfo = codes[iCount];
            if (iCount < elements.length) {
                // 遇到已有的元素
                var element = elements[iCount];
                if (bolNormalViewMode) {
                    element.style.marginTop = iCount == 0 ? "" : "1px";
                    element.style.marginBottom = iCount == codes.length - 1 ? "" : "0px";
                    element.style.borderTop = iCount == 0 ? "" : "1px dotted black";
                    element.style.borderBottom = iCount == codes.length - 1 ? "" : "none";
                }
                element.__PageInfo = pageInfo;
                element.PageIndex = pageInfo.PageIndex;
                if (pageInfo.Background == true) {
                    jQuery(element).addClass(rootElement.__BKImgStyleName);
                }
                else {
                    jQuery(element).removeClass(rootElement.__BKImgStyleName);
                }
                // 修复修改已有的页面元素的盒子模型导致页面模糊的问题【DUWRITER5_0-3431】
                // element.style.boxSizing = "content-box";
                if (bolForceRefresh == true
                    || parseInt(element.getAttribute("native-width")) != pageInfo.Width
                    || parseInt(element.getAttribute("native-height")) != pageInfo.Height) {
                    // 大小发生改变
                    pageLayoutChanged = true;
                    element.setAttribute("native-width", pageInfo.Width);
                    element.setAttribute("native-height", pageInfo.Height);
                    var imgBack = null;
                    if (bolNormalViewMode == true && element._isRendered == true) {
                        var ctx = element.getContext("2d");
                        imgBack = ctx.getImageData(0, 0, element.width, element.height);
                    }
                    WriterControl_Paint.SetPageElementSize(rootElement, element);
                    if (imgBack != null) {
                        var ctx2 = element.getContext("2d");
                        ctx.putImageData(imgBack, 0, 0);
                        imgBack = null;
                    }
                    else {
                        //WriterControl_Paint.ClearCanvasElement(element);
                        element._isRendered = false;
                    }
                    element._NeedClear = false;
                    elementChanged = true;
                }
            }
            else {
                // 需要新增元素
                pageLayoutChanged = true;
                var element = null;
                //[DUWRITER5_0-3321]20240808 lxy 预览控件下，直接创建为svg
                if (rootElement.IsWriterPrintPreviewControlForWASM === true) {
                    element = rootElement.ownerDocument.createElementNS("http://www.w3.org/2000/svg", "svg");
                } else {
                    element = rootElement.ownerDocument.createElement("CANVAS");
                }
                element.draggable = DCTools20221228.ParseBoolean(rootElement.getAttribute("AllowDragContent"), false);
                element.RootElement = rootElement;
                element.__PageInfo = pageInfo;
                element._isRendered = false;
                element._NeedClear = false;
                element.setAttribute("width", pageInfo.Width + "px");
                element.setAttribute("height", pageInfo.Height + "px");
                //class类改成自定义属性添加类名，防止svg报错（scg无法修改className ）
                if (pageInfo.Background == true) {
                    element.setAttribute("class", "pagecss" + rootElement.id + " " + rootElement.__BKImgStyleName);
                } else {
                    element.setAttribute("class", "pagecss" + rootElement.id);
                }

                pageContainer.appendChild(element);
                element.PageIndex = pageInfo.PageIndex;
                element.setAttribute("dctype", "page");
                element.setAttribute("UNSELECTABLE", "on");
                element.setAttribute("native-width", pageInfo.Width);
                element.setAttribute("native-height", pageInfo.Height);

                if (bolNormalViewMode == true) {
                    // 压缩页面视图模式
                    element.style.marginTop = iCount == 0 ? "" : "0px";
                    element.style.marginBottom = iCount == codes.length - 1 ? "" : "0px";
                    element.style.borderTop = iCount == 0 ? "" : "1px dotted black";
                    element.style.borderBottom = iCount == codes.length - 1 ? "" : "none";
                }
                element.style.boxSizing = "content-box";
                // var resizeObserver = new ResizeObserver(entries => {
                //     let rootElement=entries[0].target.RootElement
                //     if (rootElement&&rootElement.CollectrResizeCanvas) {
                //         rootElement.CollectrResizeCanvas(entries[0].target);
                //     }
                // });
                var resizeObserver = new ResizeObserver(WriterControl_Paint.OnResizeCallback);
                resizeObserver.disconnect(element);
                //确保该元素身上只有一个事件监听
                resizeObserver.observe(element);
                var pageCSSString = rootElement.getAttribute("pagecssstring");
                if (pageCSSString != null && pageCSSString.length > 0) {
                    element.setAttribute("style", pageCSSString);
                }
                //if (iCount == codes.length - 1) {
                WriterControl_Paint.SetPageElementSize(rootElement, element, true, codes.length);
                //}
                if (rootElement.IsWriterPrintPreviewControlForWASM == true) {
                    element.onclick = function () {
                        var ctl = DCTools20221228.GetOwnerWriterControl(this);
                        if (this != ctl.CurrentPageElement) {
                            ctl.__DCWriterReference.invokeMethod("set_PageIndex", this.PageIndex);
                            WriterControl_Rule.InvalidateView(ctl, "hrule");
                            WriterControl_Rule.InvalidateView(ctl, "vrule");
                            if (ctl.CurrentPageElement != null) {
                                ctl.CurrentPageElement.style.borderColor = "";
                            }
                            ctl.CurrentPageElement = this;
                            this.style.borderColor = "blue";
                        }
                    };
                    element.oncontextmenu = function (e) {
                        // 处理快捷菜单事件
                        var re2 = DCTools20221228.GetOwnerWriterControl(this);
                        re2.RaiseEvent(
                            "EventShowContextMenu",
                            {
                                TypeName: "WriterPrintPreviewControlForWASM",
                                PageElement: this,
                                X: e.offsetX,
                                Y: e.offsetY
                            });
                        e.stopPropagation();
                        e.preventDefault();
                        e.returnValue = false;
                        return false;
                    };
                }
                else {
                    //设置放大的比例
                    var funcMouseEvent = function (e) {
                        let rootElement = e.target.RootElement;
                        //let pageContainer = WriterControl_UI.GetPageContainer(rootElement);
                        let pageContainer = rootElement.PageContainer;
                        // 处理鼠标事件
                        // 修复在不同的域（跨域）间尝试设置属性报错问题【DUWRITER5_0-3471】
                        // if (e.type == "mousedown" && window.top != null) {
                        //     window.top.__DCDragData20240724 = null;
                        // }
                        if (WriterControl_UI.IsDropdownControlVisible() == true) {
                            //判断是否为双击事件
                            if (e.type != "dblclick") {
                                return;
                            }
                        }
                        if (e.type == "click") {
                            // 单击编辑器时取消其他位置的选择内容【DUWRITER5_0-4144】
                            // 暂时注释DUWRITER5_0-4144该代码
                            // var e_window = e.view;
                            // if (e_window && typeof e_window.getSelection == "function") {
                            //     var sel = e_window.getSelection();
                            //     if (sel && sel.isCollapsed == false && typeof sel.removeAllRanges == "function") {
                            //         // 取消所有的选择
                            //         sel.removeAllRanges();
                            //     }
                            // }
                            if (rootElement.MouseDragScrollMode === true) {
                                return;
                            }
                            //进行点击判断，是否为主动获取焦点
                            rootElement.EditorNoObtainFocus = 'clicktochangecursor';
                            //20231108 zhangbin 此处修改是因为存在编辑器失去焦点后，再次点击编辑器可能存在ShowCaret不触发的问题，此处判断如果不触发就手动调用
                            //判断是否存在选中
                            if (rootElement.GetCurrentElementTypeName() == "xtextinputfieldelement") {
                                if (rootElement.mobileMousePosition == null || typeof rootElement.mobileMousePosition == "object") {
                                    if (rootElement.__DCWriterReference.invokeMethod("HasSelection") == false) {
                                        //因为无法判读是否为按钮,此处判断是否在光标在可是区域
                                        var topGap = pageContainer.scrollTop;
                                        var bottomGap = pageContainer.scrollTop + pageContainer.offsetHeight;
                                        if (rootElement.oldCaretOption) {
                                            if (e.currentTarget.offsetTop + rootElement.oldCaretOption.intDY >= topGap && bottomGap - (e.currentTarget.offsetTop + rootElement.oldCaretOption.intDY) <= rootElement.oldCaretOption.intHeight) {
                                                rootElement.Focus();
                                            }
                                        }
                                    }
                                }
                            }
                            //鼠标续打模式下单击关掉右键菜单,适配枚举：Normal开发环境，0生产环境。
                            //20240326 lixinyu 增加判断条件：只读模式下单击关闭右键菜单（DUWRITER5_0-2123）
                            if (['Normal', 0].indexOf(rootElement.ExtViewMode) == -1 || rootElement.Readonly) {
                                var hasContextMenu = rootElement.querySelector('#dcContextMenu');
                                if (hasContextMenu) {
                                    hasContextMenu.remove();
                                }
                            }

                            //关闭知识库
                            var divknowledgeBase = rootElement.querySelector('#divknowledgeBase20240103');
                            if (divknowledgeBase && divknowledgeBase.DistroyKnowledgeBase) {
                                divknowledgeBase.DistroyKnowledgeBase();
                            }
                            //关闭表格下拉输入域
                            var dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
                            if (dropdownTable && dropdownTable.CloseDropdownTable) {
                                dropdownTable.CloseDropdownTable();
                            }
                            // 添加点击事件onDocumentClickBeforeEditorHandle,在ondocumentclick和WriterControl_EventButtonClick事件之前执行【DUWRITER5_0-3344】
                            if (rootElement && rootElement.onDocumentClickBeforeEditorHandle && typeof (rootElement.onDocumentClickBeforeEditorHandle) == "function") {
                                rootElement.onDocumentClickBeforeEditorHandle.call(rootElement, e);
                            }
                        } else if (e.type == "dblclick") {
                            if (rootElement.MouseDragScrollMode === true) {
                                return;
                            }
                            // 添加双击事件ondocumentdblclick
                            if (rootElement && rootElement.ondocumentdblclick && typeof (rootElement.ondocumentdblclick) == "function") {
                                var result = rootElement.ondocumentdblclick.call(rootElement, e);
                                if (result === false || result === 'false' || result === 'False') {
                                    return;
                                }
                            }
                            // 元素双击事件
                            var typename = rootElement.GetCurrentElementTypeName();//当前选择的元素类型名称
                            if (typename == "xtextnewmedicalexpressionelement") {
                                let ele = rootElement.CurrentElement('xtextnewmedicalexpressionelement');
                                let options = rootElement.GetElementProperties(ele);
                                //判断视图是否为只读模式
                                if (rootElement.Readonly === false) {
                                    if (options.ContentReadonly != true && options.ContentReadonly != "True" && options.ContentReadonly != "true") {
                                        // 医学表达式
                                        rootElement.DCExecuteCommand("elementproperties", true);
                                        return;
                                    }
                                }

                            } else if (typename == "xtextimageelement") {
                                let ele = rootElement.CurrentElement('xtextimageelement');
                                let options = rootElement.GetElementProperties(ele);
                                //判断视图是否为只读模式
                                if (rootElement.Readonly === false) {
                                    if (options.ContentReadonly != true && options.ContentReadonly != "True" && options.ContentReadonly != "true") {
                                        if (options.EnableEditImageAdditionShape) { //判断图片是否允许编辑
                                            rootElement.imgEditDialog(options, rootElement);
                                            return;
                                        }
                                    }
                                }
                            }
                            else {

                            }
                        } else if (e.type == 'mousedown') {
                            // 修改document.WriterControl编辑器为当前编辑器
                            rootElement.ownerDocument.WriterControl = rootElement;
                            //判断rootElement.MouseDragScrollMode是否为true
                            if (rootElement.MouseDragScrollMode === true) {
                                //对整个编辑器进行绑定
                                pageContainer.removeEventListener('mousemove', mousemoveEvent);
                                pageContainer.addEventListener('mousemove', mousemoveEvent);
                                window.removeEventListener('mouseup', mouseupEvent);
                                window.addEventListener('mouseup', mouseupEvent);

                                function mousemoveEvent(e) {
                                    var newX = -(1 * e.movementX) + pageContainer.scrollLeft;
                                    var newY = -(1 * e.movementY) + pageContainer.scrollTop;
                                    pageContainer.scrollTo(newX, newY);
                                }

                                function mouseupEvent(e) {
                                    pageContainer.removeEventListener('mousemove', mousemoveEvent);
                                    window.removeEventListener('mouseup', mouseupEvent);
                                    mousemoveEvent = null;
                                    mouseupEvent = null;
                                }
                                return;
                            }

                            rootElement.mobileMousePosition = null;
                            //此处这个写法是为了表单模式下设置不自动获取焦点后能阻止输入域相应
                            if (DCTools20221228.IsReadonlyAutoFocus(rootElement, true)) {
                                rootElement.mobileMousePosition = {
                                    x: e.offsetX,
                                    y: e.offsetY,
                                };
                            }
                            var dropdownDiv = rootElement.querySelector('#divDropdownContainer20230111');
                            if (dropdownDiv) {
                                dropdownDiv.CloseDropdown();
                            }

                            //关闭表格下拉输入域
                            var dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
                            if (dropdownTable && dropdownTable.CloseDropdownTable) {
                                dropdownTable.CloseDropdownTable();
                            }


                            //进行点击判断，是否为主动获取焦点
                            rootElement.EditorNoObtainFocus = 'clicktochangecursor';

                            pageContainer.needScrollView = true;
                            pageContainer.nowY = e.clientY - pageContainer.getBoundingClientRect().top + pageContainer.scrollTop;//存储当前的坐标
                            // pageContainer.mouseupTpScrollView = function (e) {
                            //     pageContainer.needScrollView = false;
                            //     window.removeEventListener('mouseup', pageContainer.mouseupTpScrollView);
                            //     delete pageContainer.mouseupTpScrollView;
                            // };
                            //使用iframe的情况下，鼠标抬起事件要放到iframe父元素中
                            if (rootElement.ownerDocument === window.document) {
                                window.optionRootElementId = rootElement.id;
                                window.mouseupTpScrollView = function (e) {
                                    if (window.optionRootElementId) {
                                        let rootElement = DCTools20221228.GetOwnerWriterControl(window.optionRootElementId);
                                        if (rootElement) {
                                            let pageContainer = WriterControl_UI.GetPageContainer(rootElement);
                                            pageContainer.needScrollView = false;
                                        }
                                    }
                                    window.removeEventListener('mouseup', this.mouseupTpScrollView);
                                    delete window.mouseupTpScrollView;
                                    delete window.optionRootElementId;

                                };
                                window.removeEventListener('mouseup', window.mouseupTpScrollView);
                                window.addEventListener('mouseup', window.mouseupTpScrollView);
                            } else {
                                rootElement.ownerDocument.optionRootElementId = rootElement.id;
                                rootElement.ownerDocument.mouseupTpScrollView = function (e) {
                                    if (this.optionRootElementId) {
                                        let rootEle = this.getElementById(this.optionRootElementId);
                                        if (rootEle) {
                                            let pageContainer = WriterControl_UI.GetPageContainer(rootEle);
                                            pageContainer.needScrollView = false;
                                        }
                                    }
                                    this.removeEventListener('mouseup', this.mouseupTpScrollView);
                                    delete this.mouseupTpScrollView;
                                    delete this.optionRootElementId;

                                };
                                rootElement.ownerDocument.removeEventListener('mouseup', rootElement.ownerDocument.mouseupTpScrollView);
                                rootElement.ownerDocument.addEventListener('mouseup', rootElement.ownerDocument.mouseupTpScrollView);
                            }



                            if (rootElement != null && rootElement.ondocumentmousedown != null && typeof rootElement.ondocumentmousedown == 'function') {
                                rootElement.ondocumentmousedown(e);
                            }
                        } else if (e.type == 'mouseup') {
                            if (rootElement.MouseDragScrollMode === true) {
                                return;
                            }

                            pageContainer.needScrollView = false;
                            if (pageContainer.mouseupTpScrollView) {
                                window.removeEventListener('mouseup', pageContainer.mouseupTpScrollView);
                            }
                            if (rootElement != null && rootElement.ondocumentmouseup != null && typeof rootElement.ondocumentmouseup == 'function') {
                                rootElement.ondocumentmouseup(e);
                            }
                        } else if (e.type == 'mousemove') {
                            if (rootElement.MouseDragScrollMode === true) {
                                return;
                            }

                            //在此处进行判断,只要到边界的时候进行视图滚动
                            if (pageContainer.needScrollView) {
                                var y = e.clientY - pageContainer.getBoundingClientRect().top;//获取鼠标到顶部的距离
                                var offset_y = y + pageContainer.scrollTop - pageContainer.nowY;//和之前的位置内容对比
                                if (Math.abs(offset_y) >= 10) {
                                    // 和之前相比需要偏移10像素才可以
                                    if (y <= 50) {
                                        pageContainer.scrollTo({
                                            top: pageContainer.scrollTop - 20,
                                            behavior: "smooth"
                                        });
                                    } else if (pageContainer.clientHeight - y <= 50) {
                                        pageContainer.scrollTo({
                                            top: pageContainer.scrollTop + 20,
                                            behavior: "smooth"
                                        });
                                        // pageContainer.scrollTo(pageContainer.scrollLeft, pageContainer.scrollTop + 10);
                                    }
                                }
                            }
                            if (rootElement != null && rootElement.ondocumentmousemove != null && typeof rootElement.ondocumentmousemove == 'function') {
                                rootElement.ondocumentmousemove(e);
                            }
                        }

                        if (e.type == 'mousedown' || e.type == 'mousemove') {
                            //判断是否点击在批注位置丢失焦点
                            var hasClickCommentAreaFocusCaret = rootElement.getAttribute('clickcommentareafocuscaret');
                            if (typeof hasClickCommentAreaFocusCaret == 'string' && hasClickCommentAreaFocusCaret.toLowerCase() == "true" && e.target && e.target.nodeName == 'CANVAS' && e.target.getAttribute('dctype') == 'page') {
                                //是否存在批注
                                if (rootElement.getCommentList()) {
                                    //获取到页面宽度
                                    var pageWidth = rootElement.GetDocumentPageSettings();
                                    var zoomRate = rootElement.GetZoomRate();
                                    pageWidth = Math.round((pageWidth.PaperWidthInCM - pageWidth.RightMarginInCM) / 2.54 * 96 * zoomRate);
                                    if (e.offsetX > pageWidth) {
                                        if (e.type == 'mousedown') {
                                            rootElement.ClickCommentAreaFocusCaret = "true";
                                            WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                                                delete rootElement.ClickCommentAreaFocusCaret;
                                            });
                                        } else if (e.type == 'mousemove') {
                                            return;
                                        }
                                    } else {
                                        delete rootElement.ClickCommentAreaFocusCaret;
                                    }
                                }
                            }
                        }

                        var es2 = WriterControl_UI.GetPageCanvasElements(this.parentNode);
                        for (var pageIndex = 0; pageIndex < es2.length; pageIndex++) {
                            if (es2[pageIndex] == this) {
                                if (rootElement.__DCWriterReference != null) {
                                    //在此处判断是否为mac os 
                                    var button = e.buttons;
                                    if (navigator.userAgent.indexOf("Mac OS X") >= 0) {
                                        if (e.which == 2) {
                                            button = 4;
                                        } else if (e.which == 3) {
                                            button = 2;
                                        } else if (e.which == 1) {
                                            button = 1;
                                        }
                                    }
                                    rootElement.__DCWriterReference.invokeMethod(
                                        "EditorHandleMouseEvent",
                                        pageIndex,
                                        e.type,
                                        e.altKey,
                                        e.shiftKey,
                                        e.ctrlKey,
                                        e.offsetX,
                                        e.offsetY,
                                        button,
                                        e.detail);
                                }
                                if (rootElement.__DCWriterReference.invokeMethod("Is_WaittingForDragStart") == false) {
                                    e.stopPropagation();
                                    e.preventDefault();
                                    e.returnValue = false;
                                }
                                else if (e.type == "mousedown") {
                                    // 准备鼠标拖拽使用的图片
                                    this.__DragImage = null;
                                    var strImgBounds = rootElement.__DCWriterReference.invokeMethod("GetPageClientSelectionBounds", this.PageIndex);
                                    if (strImgBounds != null && strImgBounds.length > 5) {
                                        var strItems = strImgBounds.split(",");
                                        var intLeft = parseInt(strItems[0]);
                                        var intTop = parseInt(strItems[1]);
                                        var intWidth = parseInt(strItems[2]);
                                        var intHeight = parseInt(strItems[3]);
                                        var ctx = this.getContext("2d");
                                        var imgData = ctx.getImageData(intLeft, intTop, intWidth, intHeight);
                                        if (imgData.data.length < 400000) {
                                            // 太大的图片不处理。
                                            var data4 = imgData.data;
                                            var data4Len = data4.length;
                                            for (var dataIndex = 0; dataIndex < data4Len; dataIndex += 4) {
                                                if (data4[dataIndex + 3] != 0) {
                                                    var r = data4[dataIndex];
                                                    var g = data4[dataIndex + 1];
                                                    var b = data4[dataIndex + 2];
                                                    if (r == g && g == b) {
                                                        // 纯灰色，则不可能是被选中区域
                                                        data4[dataIndex + 3] = 0;
                                                    }
                                                    else if (r > 210 || g > 210 || b < 30) {
                                                        data4[dataIndex + 3] = 0;
                                                    }
                                                }
                                            }
                                        }
                                        var temp4 = document.createElement("CANVAS");
                                        temp4.width = intWidth;
                                        temp4.height = intHeight;
                                        var ctx4 = temp4.getContext("2d");
                                        ctx4.putImageData(imgData, 0, 0);
                                        var strImgeData = temp4.toDataURL();
                                        temp4.remove();
                                        var temp5 = document.createElement("IMG");
                                        temp5.width = intWidth;
                                        temp5.height = intHeight;
                                        temp5.SourceLeft = intLeft;
                                        temp5.SourceTop = intTop;
                                        temp5.src = strImgeData;
                                        this.__DragImage = temp5;
                                        //console.log("创建拖拽图片" + new Date().toString());
                                        //this.__DragImage.draggable = true;
                                        //this.parentNode.appendChild(this.__DragImage);
                                    }
                                }
                                break;
                            }
                        }

                        // 修改点击事件触发位置,防止点击单复选框时，当前元素依旧指向上一次点击位置
                        if (e.type == "click") {
                            // 添加点击事件ondocumentclick
                            if (rootElement && rootElement.ondocumentclick && typeof (rootElement.ondocumentclick) == "function") {
                                rootElement.ondocumentclick.call(rootElement, e);
                            }
                            //单复选框勾选事件
                            if (rootElement.CurrentElement('xtextcheckboxelement') || rootElement.CurrentElement('xtextradioboxelement')) {
                                if (rootElement && rootElement.onElementCheckclick && typeof (rootElement.onElementCheckclick) == "function") {
                                    rootElement.onElementCheckclick.call(rootElement);
                                }
                            }
                        }
                    };
                    element.onmousedown = funcMouseEvent;
                    element.onmousemove = funcMouseEvent;
                    element.onmouseup = funcMouseEvent;
                    element.onclick = funcMouseEvent;
                    element.ondblclick = funcMouseEvent;

                    //再次此处新增处理
                    if ('ontouchstart' in rootElement.ownerDocument.documentElement) {
                        element.ontouchstart = touchstart;
                        element.ontouchmove = touchmove;
                        element.ontouchend = touchend;
                    } else {
                        element.style.touchAction = "pan-y";
                        element.onpointerdown = function (e) {
                            if (e.pointerType == "touch") {
                                touchstart(e, "pointer");
                            }
                        };
                        element.onpointermove = function (e) {
                            if (e.pointerType == "touch") {
                                touchmove(e, "pointer");
                            }
                        };
                        element.onpointerup = function (e) {
                            if (e.pointerType == "touch") {
                                touchend(e);
                            }
                        };
                    }
                    function touchstart(e, type) {
                        // let rootElement=e.target.RootElement
                        //如果已经存在选中,清掉
                        if (rootElement.__DCWriterReference.invokeMethod("HasSelection") == true) {
                            // element.oncontextmenu(e);
                            rootElement.FocusElement(rootElement.CurrentElement("xtextcontainerelement"));
                        }
                        rootElement.touchMove = false;
                        clearTimeout(element.touchStartTime);
                        document.body.style.userSelect = "none";
                        rootElement.mobileMousePosition = null;
                        //开始记时，超过一秒没有移动才开始滚动
                        element.touchStartTime = setTimeout(() => {
                            var es2 = WriterControl_UI.GetPageCanvasElements(e.target.parentNode);
                            for (var pageIndex = 0; pageIndex < es2.length; pageIndex++) {
                                if (es2[pageIndex] == e.target) {
                                    if (rootElement.__DCWriterReference != null) {
                                        rootElement.touchMove = true;
                                        if (navigator.userAgent.indexOf("Mac OS X") >= 0) {
                                            document.body.style.pointerEvents = "none";
                                            document.body.style.webkitUserSelect = "none";
                                        }
                                        //进行点击判断，是否为主动获取焦点
                                        rootElement.EditorNoObtainFocus = 'clicktochangecursor';
                                        rootElement.mobileMousePosition = {
                                            x: type ? parseInt(e.offsetX) : parseInt(e.touches[0].clientX - e.target.getBoundingClientRect().x),
                                            y: type ? parseInt(e.offsetY) : parseInt(e.touches[0].clientY - e.target.getBoundingClientRect().y)
                                        };
                                        rootElement.__DCWriterReference.invokeMethod(
                                            "EditorHandleMouseEvent",
                                            pageIndex,
                                            "mousedown",
                                            e.altKey,
                                            e.shiftKey,
                                            e.ctrlKey,
                                            rootElement.mobileMousePosition.x,
                                            rootElement.mobileMousePosition.y,
                                            1,
                                            1);
                                    }
                                    e.stopPropagation();
                                    e.preventDefault();
                                    //e.returnValue = false;
                                    break;
                                }
                            }
                        }, 1000);

                    };
                    function touchmove(e, type) {
                        if (!type) {
                            clearTimeout(element.touchStartTime);
                            document.body.style.removeProperty('user-select');
                            if (navigator.userAgent.indexOf("Mac OS X") >= 0) {
                                document.body.style.removeProperty("pointer-events");
                                document.body.style.removeProperty("-webkit-user-select");
                            }
                        }
                        //允许移动
                        if (rootElement.touchMove) {
                            if (type) {
                                clearTimeout(element.touchStartTime);
                                document.body.style.removeProperty('user-select');
                                if (navigator.userAgent.indexOf("Mac OS X") >= 0) {
                                    document.body.style.removeProperty("pointer-events");
                                    document.body.style.removeProperty("-webkit-user-select");
                                }
                            }
                            var es2 = WriterControl_UI.GetPageCanvasElements(e.target.parentNode);
                            for (var pageIndex = 0; pageIndex < es2.length; pageIndex++) {
                                if (es2[pageIndex] == e.target) {
                                    if (rootElement.__DCWriterReference != null) {
                                        rootElement.__DCWriterReference.invokeMethod(
                                            "EditorHandleMouseEvent",
                                            pageIndex,
                                            "mousemove",
                                            e.altKey,
                                            e.shiftKey,
                                            e.ctrlKey,
                                            type ? parseInt(e.offsetX) : parseInt(e.touches[0].clientX - e.target.getBoundingClientRect().x),
                                            type ? parseInt(e.offsetY) : parseInt(e.touches[0].clientY - e.target.getBoundingClientRect().y),
                                            1,
                                            e.detail);
                                    }
                                    e.stopPropagation();
                                    e.preventDefault();
                                    //e.returnValue = false;
                                    break;
                                }
                            }
                        }
                    };
                    function touchend(e) {
                        clearTimeout(element.touchStartTime);
                        document.body.style.removeProperty('user-select');
                        if (navigator.userAgent.indexOf("Mac OS X") >= 0) {
                            document.body.style.removeProperty("pointer-events");
                            document.body.style.removeProperty("-webkit-user-select");
                        }
                        if (rootElement.__DCWriterReference.invokeMethod("HasSelection") == true) {
                            element.oncontextmenu(e);
                        }
                        //获取模拟输入域,此处设为清除只读是为了软键盘的弹出
                        //var textEle = rootElement.querySelector('[dctype=dcinput]');
                        //if (textEle) {
                        //    textEle.removeAttribute('readonly');
                        //}
                        //if (rootElement.touchMove && rootElement.__DCWriterReference.invokeMethod("HasSelection") == true) {
                        //if (rootElement.__DCWriterReference.invokeMethod("HasSelection") == true) {
                        //rootElement.touchMove = false;
                        //textEle.blur();
                        //WriterControl_UI.ShowCaret(
                        //    rootElement.oldCaretOption.containerID,
                        //    rootElement.oldCaretOption.intPageIndex,
                        //    rootElement.oldCaretOption.intDX,
                        //    rootElement.oldCaretOption.intDY,
                        //    rootElement.oldCaretOption.intWidth,
                        //    rootElement.oldCaretOption.intHeight,
                        //    rootElement.oldCaretOption.bolVisible,
                        //    rootElement.oldCaretOption.bolReadonly
                        //)
                        //textEle.focus();
                        //}
                        //rootElement.touchMove = false;
                    };
                    element.oncontextmenu = function (e) {
                        // 处理快捷菜单事件
                        var re2 = DCTools20221228.GetOwnerWriterControl(e.currentTarget);
                        var typeName = re2.__DCWriterReference.invokeMethod("GetContextMenuTypeName");
                        if (typeName != null && typeName.length > 0) {
                            re2.RaiseEvent(
                                "EventShowContextMenu",
                                {
                                    TypeName: "快捷菜单信息",
                                    PageElement: e.currentTarget,
                                    ElementType: typeName,
                                    X: e.offsetX,
                                    Y: e.offsetY
                                });
                            e.stopPropagation();
                            e.preventDefault();
                            e.returnValue = false;
                        }
                        return false;
                    };
                    element.ondragstart = function (e5) {
                        // 处理鼠标开始拖拽内容事件
                        var datas = rootElement.__DCWriterReference.invokeMethod("EditorStartDragSelection", this.PageIndex, e5.offsetX, e5.offsetY);
                        if (datas == null || datas.length == 0) {
                            // 取消鼠标拖拽操作
                            e5.stopPropagation();
                            e5.preventDefault();
                            e5.returnValue = false;
                        }
                        else {
                            // 开始执行鼠标拖拽内容事件
                            //var localRawID = new Date().valueOf().toString();
                            e5.dataTransfer.effectAllowed = datas.shift();
                            //e5.dataTransfer.setdata(localRawID, "1");
                            for (var iCount = 0; iCount < datas.length; iCount += 2) {
                                e5.dataTransfer.setData(datas[iCount], datas[iCount + 1]);
                            }
                            //// 在浏览器对象中缓存数据
                            //if (window.top != null) {
                            //    datas.splice(1, 0, localRawID);
                            //    window.top.__DCDragData20240724 = datas;
                            //}
                            //if (e5.ctrlKey || rootElement.__DCWriterReference.invokeMethod("get_Readonly") == true ) {
                            //    e5.dataTransfer.dropEffect = "copy";
                            //}
                            //else {
                            //    e5.dataTransfer.dropEffect = "copyMove";
                            //}
                            if (this.__DragImage != null) {
                                //this.parentNode.appendChild(this.__DragImage);
                                //var img = this.__DragImage;
                                //img.style.position = "absolute";
                                //img.style.left = img.SourceLeft + "px";
                                //img.style.top = img.SourceTop + "px";
                                //this.parentNode.appendChild(img);
                                //e5.dataTransfer.setDragImage(
                                //    this.__DragImage, 10, 10);
                                //img.remove();
                                //img.remove();
                                e5.dataTransfer.setDragImage(
                                    this.__DragImage,
                                    e5.offsetX - this.__DragImage.SourceLeft,
                                    e5.offsetY - this.__DragImage.SourceTop);
                                this.__DragImage = null;
                            }
                            window._DCCurrentDragSourceElement = rootElement;
                        }
                    };

                    // 

                    var funcDrag = function (e) {


                        // 处理鼠标拖拽事件
                        if (WriterControl_UI.IsDropdownControlVisible() == true) {
                            return;
                        }
                        var es2 = WriterControl_UI.GetPageCanvasElements(this.parentNode);
                        if (rootElement.__DCWriterReference == null) {
                            // 无法进行操作
                            return;
                        }
                        if (rootElement.__DCWriterReference.invokeMethod("get_Readonly") == true) {
                            // 只读的控件不处理
                            //e.stopPropagation();
                            //e.preventDefault();
                            //e.returnValue = false;
                            return;
                        }
                        var strLastEventInfo = e.type + "=" + e.offsetX + "-" + e.offsetY;
                        if (this.__LastDragEventInfo == strLastEventInfo) {
                            // 防止事件频繁触发
                            e.dataTransfer.dropEffect = this.__LastDropEffect;
                            e.stopPropagation();
                            e.preventDefault();
                            e.returnValue = false;
                            return;
                        }
                        this.__LastDragEventInfo = strLastEventInfo;
                        var srcDatas = e.dataTransfer;
                        if (srcDatas == null
                            || srcDatas.types == null
                            || srcDatas.types.length == 0) {
                            // 没有获取数据
                            return;
                        }
                        //wyc20240426:前端编辑器新增ForceCopyDragContent属性强制拖放时复制内容而不是移动
                        var tempstr = rootElement.getAttribute("ForceCopyDragContent");
                        var forcedragcopy = tempstr === "true" || tempstr === true;

                        var strCachedDragDataID = null;
                        for (const strDataType of srcDatas.types) {
                            if (strDataType.substring(0, 5) == "dcid_") {
                                if (rootElement.__DCWriterReference.invokeMethod("IsCachedDragData", strDataType) == true) {
                                    // 已经有内置缓存的数据，则立即执行
                                    var strResult = rootElement.__DCWriterReference.invokeMethod(
                                        "EditorHandleDragEvent",
                                        [strDataType, "1"],
                                        this.PageIndex,
                                        e.type,
                                        e.altKey,
                                        e.shiftKey,
                                        forcedragcopy === true ? true : e.ctrlKey,
                                        e.offsetX,
                                        e.offsetY,
                                        e.dataTransfer.effectAllowed);
                                    if (strResult != null) {
                                        e.dataTransfer.dropEffect = strResult;
                                    }
                                    this.__LastDropEffect = strResult;
                                    e.stopPropagation();
                                    e.preventDefault();
                                    e.returnValue = false;
                                    return;
                                }
                                strCachedDragDataID = strDataType;
                                break;
                            }
                        }
                        var listData = [];
                        //解析数据分解成字符串数组
                        //存在文件信息
                        if (srcDatas.files.length > 0) {
                            Promise.all(function* () {
                                for (const file of srcDatas.files) {
                                    yield new Promise(resolve => {
                                        var reader = new FileReader();
                                        reader.readAsDataURL(file);
                                        reader.type = file.type;
                                        reader.onload = function (e) {
                                            resolve(this);
                                        };
                                    });
                                }
                            }()).then(results => {
                                results.forEach(data => {
                                    listData.push(data.type, data.result);
                                });
                                if (listData.length > 0) {
                                    if (strCachedDragDataID != null) {
                                        if (rootElement.__DCWriterReference.invokeMethod(
                                            "SetCachedDragData",
                                            strCachedDragDataID,
                                            listData) == true) {
                                            listData = [strCachedDragDataID, "1"];
                                        }
                                    }
                                    var strResult = rootElement.__DCWriterReference.invokeMethod(
                                        "EditorHandleDragEvent",
                                        listData,
                                        this.PageIndex,
                                        e.type,
                                        e.altKey,
                                        e.shiftKey,
                                        forcedragcopy === true ? true : e.ctrlKey,
                                        e.offsetX,
                                        e.offsetY,
                                        e.dataTransfer.effectAllowed);
                                    if (strResult != null) {
                                        e.dataTransfer.dropEffect = strResult;
                                    }
                                }
                                return;
                            });
                        } else {
                            for (const type of srcDatas.types) {
                                //根据剪切板中的数据类型解析数据
                                var clipboardData = srcDatas.getData(type);
                                listData.push(type);
                                listData.push(clipboardData);
                            }
                            if (strCachedDragDataID != null) {
                                if (rootElement.__DCWriterReference.invokeMethod(
                                    "SetCachedDragData",
                                    strCachedDragDataID,
                                    listData) == true) {
                                    listData = [strCachedDragDataID, "1"];
                                }
                            }
                            var strResult = rootElement.__DCWriterReference.invokeMethod(
                                "EditorHandleDragEvent",
                                listData,
                                this.PageIndex,
                                e.type,
                                e.altKey,
                                e.shiftKey,
                                forcedragcopy === true ? true : e.ctrlKey,
                                e.offsetX,
                                e.offsetY,
                                e.dataTransfer.effectAllowed);
                            if (strResult != null) {
                                e.dataTransfer.dropEffect = strResult;
                            }
                        }
                        e.stopPropagation();
                        e.preventDefault();
                        e.returnValue = false;
                    };
                    element.ondragenter = funcDrag;
                    element.ondragleave = funcDrag;
                    element.ondragover = funcDrag;
                    element.ondrop = funcDrag;

                    //element.ondragend = funcDrag;
                    element.ondragend = function (e6) {
                        this.style.cursor = "default";
                        var rootElement2 = DCTools20221228.GetOwnerWriterControl(this);
                        if (rootElement2 != null) {
                            rootElement2.__DCWriterReference.invokeMethod("ClearCachedDragData");
                            if (e6.dataTransfer.dropEffect == "move") {
                                // 移动数据，则将选中的内容删掉
                                rootElement2.__DCWriterReference.invokeMethod("EditorDeleteSelectionForDragingSelection");
                            }
                        }
                    };
                }
                //WriterControl_Paint.ClearCanvasElement(element);
                element._isRendered = false;
                element._NeedClear = false;
                elementChanged = true;

                //判断是否存在NumberDisplayedInTibetan属性有则手动处理
                //判断是否存在NumberDisplayedInTibetan属性
                var hasNumberDisplayedInTibetan = rootElement.getAttribute("numberdisplayedintibetan");
                if (typeof hasNumberDisplayedInTibetan == "string" && hasNumberDisplayedInTibetan.toLowerCase() == "true") {
                    var allPageInfo = rootElement.GetElementsByTypeName("xtextpageinfoelement");
                    if (Array.isArray(allPageInfo)) {
                        //获取最新的页面个数并更新
                        var allIndex = rootElement.PageCount;
                        var tibetanString = ["༠", "༡", "༢", "༣", "༤", "༥", "༦", "༧", "༨", "༩"];
                        for (var i = 0; i < allPageInfo.length; i++) {
                            var parameter = allPageInfo[i];
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
                            parameter.SpecifyPageIndexTextList = newSpecifyPageIndexTextList;
                            rootElement.SetElementProperties(parameter.NativeHandle, parameter);
                        }
                    }
                }

            }
        }

        if (elements.length != targetElementsCount) {
            WriterControl_Event.RaiseControlEvent(rootElement, "EventAfterPageCountChange", WriterControl_UI.GetPageCanvasElements(rootElement));
        }

        if (elementChanged == true) {
            // 更新排版模式
            var strMode = rootElement.getAttribute("pagelayoutmode");
            if (strMode != null) {
                strMode = strMode.trim().toLocaleLowerCase();
                if (strMode == "singlecolumn"
                    || strMode == "multicolumn") {
                    for (var element = pageContainer.firstChild;
                        element != null;
                        element = element.nextSibling) {
                        if (element.nodeName == "CANVAS" || element.nodeName == "svg") {
                            if (strMode == "singlecolumn") {
                                element.style.display = "block";
                                element.style.margin = "5px auto";
                            }
                            else {
                                element.style.display = "inline-block";
                                element.style.margin = "5px 5px";
                            }
                        }
                    }//for
                }
            }
        }
        if (pageLayoutChanged == true) {
            pageContainer.scrollLeft = backScrollLeft;
            pageContainer.scrollTop = backScrollTop;
        }

        //打印控件时，默认加载一下可视区域的svg内容
        if (rootElement.IsWriterPrintPreviewControlForWASM == true) {
            // 创建一个新的scroll事件
            var scrollEvent = new Event('scroll', {
                bubbles: true,
                cancelable: true
            });
            // 手动触发scroll事件
            pageContainer.dispatchEvent(scrollEvent);
        }

        // 元素发生改变，则需要绘制可见元素
        WriterControl_Task.AddTask(function () {
            if (typeof (rootElement.RefreshDocument) == "function") {
                WriterControl_Paint.InvalidateAllView(rootElement);
                //rootElement.RefreshDocument();
            } else {

                WriterControl_Paint.HandleScrollView(containerID, true);
            }

            WriterControl_Rule.InvalidateAllView(containerID);

            if (elements.length > 0 && targetElementsCount > 0) {
                if (typeof rootElement.EventCanvasCountChange == "function") {
                    rootElement.EventCanvasCountChange(rootElement);
                }
            }

        });
    },
    /**
     * 页面尺寸发生变化时的回调函数
     * @param {dom} e 调用dom
     */
    OnResizeCallback: function (e) {
        let rootElement = e[0].target.RootElement;
        if (rootElement && rootElement.CollectrResizeCanvas) {
            rootElement.CollectrResizeCanvas(e[0].target);
        }
    },
};