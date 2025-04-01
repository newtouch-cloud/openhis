//* 开始时间:
//* 开发者:
//* 重要描述:
//*************************************************************************
//* 最后更新时间: 2025-01-16
//* 最后更新人: xuyiming
//*************************************************************************


"use strict";

import { DCTools20221228 } from "./DCTools20221228.js";
import { WriterControl_Paint } from "./WriterControl_Paint.js";
import { WriterControl_CalculateControl } from "./WriterControl_CalculateControl.js";
import { WriterControl_DateTimeControl } from "./WriterControl_DateTimeControl.js";
import { WriterControl_ListBoxControl } from "./WriterControl_ListBoxControl.js";
import { WriterControl_Rule } from "./WriterControl_Rule.js";
import { WriterControl_Task } from "./WriterControl_Task.js";
import { WriterControl_Event } from "./WriterControl_Event.js";
import { WriterControl_Print } from "./WriterControl_Print.js";
import { WriterControl_KnowledgeBase } from "./WriterControl_KnowledgeBase.js";
import { WriterControl_QueryListTableControl } from "./WriterControl_QueryListTableControl.js";
import { WriterControl_Dialog } from "./WriterControl_Dialog.js";
import { WriterControl_IO } from "./WriterControl_IO.js";

export let WriterControl_UI = {
    /**
     * 检查文档批注列表，删除无效的批注
     * @param {string} rootElement 编辑器编号
     * @param {string} strHashCodeList 有效的文档批注的哈希编号数组JSON字符串
     */
    CheckDocumentComentList: function (rootElement, strHashCodeList) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return;
        }
        var idList = null;
        if (strHashCodeList != null && strHashCodeList.length > 0) {
            idList = JSON.parse(strHashCodeList);
        }
        var divs = rootElement.querySelectorAll("div[dctype='DocumentComment']");
        if (divs != null && divs.length > 0) {
            for (var div of divs) {
                if (idList == null || idList.indexOf(div.CommentHashCode) < 0) {
                    div.remove();
                }
            }
        }
    },
    ///**
    // * 计算文档批注对象的以像素为单位的显示高度
    // * @param {any} rootElement
    // * @param {any} options
    // */
    //CalcuteDocumentCommentHeight: function (rootElement, options) {
    //    rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
    //    if (rootElement == null) {
    //        return;
    //    }
    //    options.Height = 0;
    //    // 触发事件让用户自己来计算文档批注的显示高度。
    //    WriterControl_Event.InnerRaiseEvent(rootElement, "EventCalcuteDocumentCommentHeight", options);
    //    // 计算结果放置在Height属性中。
    //    return options.Height;
    //},
    /**
     * 创建自定义的文档批注内容
     * @param {string} rootElement 编辑器编号
     * @param {any} options 参数，成员参考DocumentCommentManager.cs中的ExecuteLayout()函数。
     * @returns {string} 文档批注对象的像素高度
     */
    BuildDocumentCommentContent: function (rootElement, options) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return null;
        }
        var pc = WriterControl_UI.GetPageContainer(rootElement);
        if (pc == null) {
            return null;
        }
        var doc = rootElement.ownerDocument;
        var newCommentOptionsJsonText = JSON.stringify(options);
        /** 自定义批注承载DOM元素 */
        var div = doc.getElementById("dcm$" + options.HashCode);
        if (div == null) {
            // 创建新的文档批注元素
            div = doc.createElement("div");
            // 设置文档批注元素的ID属性，以便在页面上唯一标识这个文档批注元素
            div.id = "dcm$" + options.HashCode;
            div.setAttribute("dctype", "DocumentComment");
            // 设置文档批注元素样式
            div.style.position = "absolute";
            div.style.display = "block";
            // 设置浮动位置，避免div在页面上显示，还有防止影响页面布局
            div.style.left = "-10000px";
            div.style.top = "-10000px";
            div.style.overflow = "clip";
            div.style.boxSizing = "border-box";
            // 创建文档批注元素内容容器，用来缩放编辑器时缩放自定义批注内容
            var divContent = doc.createElement("div");
            divContent.setAttribute("dctype", "DocumentCommentContent");
            if (typeof (options.ZoomRate) == "number") {
                divContent.style.zoom = options.ZoomRate;
            }
            div.appendChild(divContent);
            div.divContent = divContent;
            pc.appendChild(div);
        }
        // 处理自定义批注元素{第一次创建}和{数据更新}的情况
        if (!div.LastCommentOptionsJsonText || div.LastCommentOptionsJsonText != newCommentOptionsJsonText) {
            // 更新最后一次的批注元素数据
            div.LastCommentOptionsJsonText = JSON.stringify(options);
            div.WriterControl = rootElement;
            div.CommentSettings = options;
            div.CommentHashCode = options.HashCode;
            div.onclick = function (e3) {
                // 检查当前元素、WriterControl属性和WriterControl的__DCWriterReference属性是否存在
                if (!this || !this.WriterControl || !this.WriterControl.__DCWriterReference) {
                    // 如果上述任一属性不存在，则返回false终止函数执行
                    return false;
                }
                // 选中文档批注元素
                this.WriterControl.__DCWriterReference.invokeMethod("SelectDocumentCommentByHashCode", this.CommentHashCode);
            };
            // 更新文档批注元素宽度
            div.style.width = (options.Width * options.ZoomRate) + "px";
            // div.style.height = options.Height + "px";
            // div.style.boxSizing = "border-box";
            // 给options添加属性和方法
            options.Handled = false;
            options.TargetElement = div;
            // 给options添加删除文档批注内容的方法
            // 根据哈希编号删除文档批注对象 DeleteDocumentCommentByHashCode
            options.DeleteComment = function (CommentHashCode) {
                CommentHashCode = typeof (CommentHashCode) !== "number" ? this.HashCode : CommentHashCode;
                if (typeof (CommentHashCode) !== "number") {
                    return false;
                }
                const TargetElement = this.TargetElement;
                if (!TargetElement || !TargetElement.WriterControl || !TargetElement.WriterControl.__DCWriterReference) {
                    return false;
                }
                return TargetElement.WriterControl.__DCWriterReference.invokeMethod("DeleteDocumentCommentByHashCode", CommentHashCode);
            }.bind(options);
            // 触发事件来让用户自己来填充文档批注的内容
            //if (options.IsInternal == true) {// 测试代码
            //    options.Handled = true;// 测试代码
            //    div.innerText = options.Title + ":" + options.Text;// 测试代码
            //    div.style.height = "200px";// options.Height = 300;// 测试代码
            //}
            if (typeof (options.ZoomRate) == "number") {
                div.divContent.style.zoom = options.ZoomRate;
            }
            WriterControl_Event.InnerRaiseEvent(rootElement, "EventBuildDocumentCommentContent", options);
            if (options.Handled == false) {
                // 用户没处理好事件，则采用编辑器默认的方式来排版和显示这个文档批注
                div.remove();
                return null;
            }
        }
        // div.divContent.style.zoom = 1;
        div.style.height = "auto";
        if (div.offsetHeight > 0) {
            return (div.offsetHeight / window.devicePixelRatio).toString();
        } else {
            return options.Height.toString();
        }
    },
    /**
     * 显示文档批注对象，此前已经调用过了BuildDocumentCommentContent(),内容已经生成好了。
     * @param {string | HTMLElement} rootElement 编辑器对象
     * @param {any} options 参数对象
     */
    ShowDocumentComment: function (rootElement, options) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null || rootElement.__DCWriterReference == null) {
            return false;
        }
        if (typeof options != "object" || options.HashCode == null) {
            return false;
        }
        var doc = rootElement.ownerDocument;
        /** 批注自定义元素 */
        var div = doc.getElementById("dcm$" + options.HashCode);
        if (div == null) {
            return false;
        }
        /** 批注所在页面 */
        var page = WriterControl_Paint.GetCanvasElementByPageIndex(rootElement, options.PageIndex);
        if (page == null) {
            return false;
        }
        options.TargetElement = null;
        var strLastJsonText = JSON.stringify(options) + "|" + page.offsetLeft + "|" + page.offsetTop;
        if (div.LastJsonText == strLastJsonText) {
            // 数据未发生改变，则不处理，提高性能
            return true;
        }
        div.LastJsonText = strLastJsonText;
        //// console.log(options,"options");
        ///** 浏览器或者系统缩放率 */
        //var baseZoomRate = rootElement.__DCWriterReference.invokeMethod("get_WASMBaseZoomRate");
        ///** 编辑器页面缩放率 */
        //var ZoomRate = rootElement.__DCWriterReference.invokeMethod("get_ZoomRate");
        var _left = page.offsetLeft + options.Left;
        var _top = page.offsetTop + options.Top;
        var _width = options.Width;
        var _height = options.Height;
        //// 修改成自定义批注偏移量和宽度高度只留下两位小数
        //var _left = page.offsetLeft + parseInt(parseFloat(options.Left) / baseZoomRate * 100) / 100,
        //    _top = page.offsetTop + parseInt(parseFloat(options.Top) / baseZoomRate * 100) / 100,
        //    _width = parseInt(parseFloat(options.Width) / baseZoomRate * 100) / 100,
        //    _height = parseInt(parseFloat(options.Height) / baseZoomRate * 100) / 100;
        //// var _left = page.offsetLeft + parseInt(parseFloat(options.Left) * 100) / 100,
        ////     _top = page.offsetTop + parseInt(parseFloat(options.Top) * 100) / 100,
        ////     _width = parseInt(parseFloat(options.Width) * 100) / 100,
        ////     _height = parseInt(parseFloat(options.Height) * 100) / 100;
        //// console.log("left,top,width,height", options.Left, options.Top, options.Width, options.Height);
        // 设置批注的位置和大小
        div.style.position = "absolute";
        div.style.display = "block";
        div.style.left = _left + "px";
        div.style.top = _top + "px";
        div.style.width = _width + "px";
        div.style.height = _height + "px";
        // div.style.height = _height + "px";
        if (typeof (options.ZoomRate) == "number" && div.divContent != null) {
            div.divContent.style.zoom = options.ZoomRate;
        }
        // if (options.IsSelected) {
        //     // 批注是否被选择
        //     div.style.border = "2px solid " + options.BorderColor;
        // } else {
        //     div.style.border = "1px solid " + options.BorderColor;
        // }
        // 自定义批注默认存在边框
        div.style.border = "1px solid " + options.BorderColor;
        // 自定义批注选中时显示更粗的边框，使用阴影来显示更粗的边框，避免影响高度
        div.style.boxShadow = options.IsSelected ? ("0 0 0 1px " + options.BorderColor) : "";
        div.style.borderRadius = "5px";
        //div.style.backgroundColor = options.BackColor;
        div.CommentValue = options;
        div.CommentHashCode = options.HashCode;
        div.WriterControl = rootElement;
        options.TargetElement = div;
        return true;
    },
    /**
     * 修正所有自定义文档批注的位置
     * @param {string | HTMLElement} rootElement 编辑器对象
     * @param {} 
     */
    AdjustCustomDocumentCommentStyle: function (rootElement) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return false;
        }
        const DocumentCommentDivs = rootElement.querySelectorAll("div[dctype='DocumentComment']");
        DocumentCommentDivs.forEach(function (DocumentCommentDiv) {
            WriterControl_UI.ShowDocumentComment(DocumentCommentDiv, DocumentCommentDiv.CommentValue);
        });
        return true;
    },
    /**
     * 用于存储防抖对象，防止连续按下Backspace键时，触发多次弹出对话框的删除操作。
    */
    DebounceTimerQueryDeleteRowOfColumns: null,
    /**
     * 显示一个对话框，执行删除表格行或者表格列的操作，本操作是由Backspace操作而引发的。
     * 对话框应该显示“删除表格行，删除表格列，取消操作”三个操作选项。由于浏览器标准的
     * 对话框不支持这种设置，因此需要自定义对话框，而且是异步操作。
     * @param {编辑器对象} rootElement
     * @returns
     */
    QueryDeleteRowOfColumns: function () {
        // 记录防抖，每次触发都重新计时
        if (this.DebounceTimerQueryDeleteRowOfColumns) {
            clearTimeout(this.DebounceTimerQueryDeleteRowOfColumns);
        }

        var that = this;
        that.DebounceTimerQueryDeleteRowOfColumns = setTimeout(() => {
            //编辑器
            var rootElement = WriterControl_UI.GetCurrentWriterControl();
            if (!rootElement) {
                that.DebounceTimerQueryDeleteRowOfColumns = null;
                return;
            }
            //调用删除行列的对话框
            WriterControl_Dialog.DeleteRowOfColumnsDialog(rootElement, function () {
                that.DebounceTimerQueryDeleteRowOfColumns = null;
            });
        }, 300);

    },

    /**
     * 根据HTML元素来创建文档对象
     * @param {string | HTMLElement } rootElement 编辑器控件对象
     * @param {string} strReason 创建理由
    * @param {HTMLElement} rootHtmlElement HTML根对象
     */
    BuildDocumentByHtmlElement: function (rootElement, strReason, rootHtmlElement) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            // 未指定编辑器控件
            return;
        }
        if (rootHtmlElement = null) {
            return;
        }
        var bd = rootElement.__DCWriterReference.invokeMethod("CreateDocumentBuilder", strReason);
        function BuildElement(builder, rootHtmlElement) {
            for (var node = rootHtmlElement.firstChild; node != null; node = node.nextSibling) {
                if (node.nodeType == 3) {
                    // 纯文本节点
                    builder.invokeMethod("AddTextNode", node.nodeValue);
                }
                else if (node.getAttribute) {
                    var strAttributeNames = new Array();
                    var strAttributeValues = new Array();
                    var attrLen = node.attributes.length;
                    for (var iCount = 0; iCount < attrLen; iCount++) {
                        var attr = node.attributes[iCount];
                        strAttributeNames.push(attr.localName);
                        strAttributeValues.push(attr.nodeValue);
                    }
                    // var strCssNames = new Array();
                    // var strCssValues = new Array();
                    // var cssStyles = window.getComputedStyle(node);
                }
            }
        };
        BuildElement(bd, rootHtmlElement);
        DCTools20221228.DisposeInstance(bd);
    },
    /**
     * 高亮度闪烁指定区域，当高亮度闪烁文档中的多个文档元素时，这个闪烁区域数组可能有多个区域信息。
     * @param {string | HTMLElement} rootElement 编辑器控件对象
     * @param {Array | string} areaList 闪烁区域数组
     */
    FlashArea: function (rootElement, areaList) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            // 未找到编辑器控件元素
            return;
        }
        if (typeof (areaList) == "string") {
            areaList = JSON.parse(areaList);
        }
        //rootElement.oldFlashArea = [];
        var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
        //获取到当前页的canvas元素
        if (areaList && areaList.length > 0) {
            var allCanvas = rootElement.querySelectorAll('[dctype="page"]');
            if (allCanvas && allCanvas.length > 0) {
                for (var i = 0; i < areaList.length; i++) {
                    var thisarea = areaList[i];
                    thisarea.Index = 0;
                    var hasArea = pageContainer.querySelector("[dctype=falshArea]");
                    if (hasArea) {
                        hasArea.remove();
                    }
                    //创建一个div做遮罩层
                    var div = document.createElement("div");
                    div.setAttribute("dctype", "falshArea");
                    div.style.position = "absolute";
                    var thisCanvas = allCanvas[thisarea.PageIndex];
                    div.style.width = thisarea.Width + "px";
                    div.style.height = thisarea.Height + "px";
                    div.style.top = thisarea.Top + thisCanvas.offsetTop + 2 + "px";
                    div.style.left = thisarea.Left + thisCanvas.offsetLeft + 2 + "px";
                    pageContainer.appendChild(div);

                    (function (thisarea, div) {
                        thisarea.intervalTime = setInterval(function () {
                            thisarea.Index++;
                            if (thisarea.Index % 2 == 0) {
                                div.style.backgroundColor = "";
                            } else {
                                div.style.backgroundColor = 'rgba(151,92,245,0.6)';
                            }
                            if (thisarea.Index >= 7) {
                                clearInterval(thisarea.intervalTime);
                                div.remove();
                                thisarea = null;
                            }
                        }, 100);
                    })(thisarea, div);
                }
            }
        }
    },


    /**
     * 下载API日志数据
     * @param {string | HTMLElement} rootElement 编辑器对象
     * @param {function} callBack 回调函数
     * @returns {boolean} 操作是否完成
     */
    DownloadAPIRecordData: function (rootElement, callBack = null) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return false;
        }
        var nativeRef = rootElement.__DCWriterReference;
        if (nativeRef != null && nativeRef.__Records != null) {
            var strJson = JSON.stringify(nativeRef.__Records);
            //20240328 lxy 适配获取API日志数据的接口（DUWRITER5_0-2164）
            if (callBack) {
                return callBack(strJson);
            }
            //var obj2 = JSON.parse(strJson);
            //console.log(obj2);
            var blob = new Blob([strJson]);
            let downloadElement = rootElement.ownerDocument.createElement("a");
            let href = window.URL.createObjectURL(blob); //创建下载的链接
            downloadElement.href = href;
            downloadElement.charset = "utf-8";
            downloadElement.type = "text/json";
            downloadElement.download = "APIRecord_" + DCTools20221228.FormatDateTime(new Date(), "yyyy_MM_dd_HH_mm_ss") + ".json";
            rootElement.ownerDocument.body.appendChild(downloadElement);
            downloadElement.click(); //点击下载
            rootElement.ownerDocument.body.removeChild(downloadElement); //下载完成移除元素
            window.URL.revokeObjectURL(href); //释放掉blob对象
            return true;
        }
        DCTools20221228.ConsoleWarring("DCWriter控件[" + rootElement.id + "]未启用API日志记录功能。");
        return false;
    },
    APILogRecordDebugPrint: function (rootElement, txt) {
        console.log("%cAPI日志:" + txt, "color:blue");
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return;
        }
        var nativeRef = rootElement.__DCWriterReference;
        if (nativeRef != null && nativeRef.DebugPrint != null) {
            nativeRef.DebugPrint(txt);
        }
    },
    /**
     * 判断编辑器是否正在记录API日志
     * @param {string | HTMLElement} rootElement 编辑器元素对象
    * @returns { boolean } 是否正在记录
     */
    IsAPILogRecord: function (rootElement) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return false;
        }
        var nativeRef = rootElement.__DCWriterReference;
        return nativeRef != null && nativeRef.__BaseInstance != null;
    },
    APILogRecordJSMethod: function (rootElement, jsFuncName, args) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return false;
        }
        var nativeRef = rootElement.__DCWriterReference;
        if (nativeRef == null || nativeRef.__BaseInstance == null) {
            return false;
        }
        if (args == null || args.length == 0) {
            nativeRef.__Records.push({
                Tick: new Date().valueOf() - nativeRef._StartLogTick,
                Name: "@JavaScript",
                Method: jsFuncName
            });
        }
        else {
            nativeRef.__Records.push({
                Tick: new Date().valueOf() - nativeRef._StartLogTick,
                Name: "@JavaScript",
                Method: jsFuncName,
                Args: args
            });
        }
        return true;
    },
    /**
     * 启用API日志记录功能
     * @param {string | HTMLElement} rootElement 编辑器元素对象
     * @param {boolean} fromBuildControl 是否从创建控件时调用的
    * @returns { boolean } 操作是否成功
     */
    StartAPILogRecord: function (rootElement, fromBuildControl) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return false;
        }
        var nativeRef = rootElement.__DCWriterReference;
        if (nativeRef == null) {
            return false;
        }
        if (nativeRef.__BaseInstance != null) {
            // 已经启用了本功能
            return false;
        }
        var item = new WriterControlExtPackage(rootElement, nativeRef);
        item.FromBuildControl = fromBuildControl;
        window.__CurrentDCWriterAPIRecorder = item;
        rootElement.__DCWriterReference = item;
        DCTools20221228.ConsoleWarring("DCWriter控件[" + rootElement.id + "]启用API日志记录功能，本功能会消耗很多内存,减慢运行速度，谨慎使用。");
        WriterControl_Paint.InvalidateAllView(rootElement);
        return true;
    },
    /**
     * 停用API日志记录功能
     * @param {string | HTMLElement} rootElement 编辑器元素对象
     * @returns { boolean} 操作是否成功
     */
    StopAPILogRecord: function (rootElement) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return false;
        }
        var nativeRef = rootElement.__DCWriterReference;
        if (nativeRef != null && nativeRef.__BaseInstance != null) {
            rootElement.__DCWriterReference = nativeRef.__BaseInstance;
            nativeRef.__Records = null;
            window.__CurrentDCWriterAPIRecorder = null;
            console.log("DCWriter控件[" + rootElement.id + "]停用API日志记录功能。");
            WriterControl_Paint.InvalidateAllView(rootElement);
            return true;
        }
        return false;
    },
    /**
     * 清空API日志记录的数据
     * @param {string | HTMLElement} rootElement 编辑器元素对象
     * @returns { boolean} 操作是否成功
     */
    ClearAPILogRecordData: function (rootElement) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return false;
        }
        var nativeRef = rootElement.__DCWriterReference;
        if (nativeRef != null && nativeRef.__Records != null) {
            var records = nativeRef.__Records;
            records.splice(0, records.length);
            return true;
        }
        return false;
    },
    /**
     * 执行API日志
     * @param {string | HTMLElement} rootElement 编辑器元素或者编号
     */
    PlayAPILogRecords: function (rootElement, playSpeed) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return;
        }
        let file = rootElement.ownerDocument.createElement('input');
        file.setAttribute('id', 'dcAPILogJsonFile');
        file.setAttribute('type', 'file');
        file.setAttribute('accept', '.json');
        file.style.display = 'none';
        rootElement.appendChild(file);
        //file文件选中事件
        file.onchange = function () {
            let fileList = this.files;
            if (fileList.length > 0) {
                let reader = new FileReader();
                reader.readAsText(fileList[0], "UFT-8");
                reader.onload = function (e) {
                    //获取到文件内容
                    let strFileContent = e.target.result;
                    window.setTimeout(function () {
                        if (file.parentNode == rootElement.ownerDocument.body) {
                            rootElement.ownerDocument.body.removeChild(file);
                        }
                        var records = JSON.parse(strFileContent);
                        if (records == null || records.length <= 1) {
                            // 没有数据
                            return;
                        }
                        var headerRecord = records.shift(0);
                        if (headerRecord.Format != "20231023") {
                            console.log("文件头不对，不是可识别的API日志文件格式。");
                            return;
                        }
                        var nativeRef = rootElement.__DCWriterReference;
                        if (nativeRef.__Records != null) {
                            // 退出API日志模式。
                            nativeRef.__Records.splice(0, nativeRef.__Records.length);
                            rootElement.__DCWriterReference = nativeRef.__BaseInstance;
                            nativeRef = rootElement.__DCWriterReference;
                            console.log("DCWrter退出API录像模式。");
                        }
                        if (typeof (headerRecord.DevicePixelRatio) == "number") {
                            if (window.devicePixelRatio != headerRecord.DevicePixelRatio) {
                                alert("日志中的window.devicePixelRatio=" + headerRecord.DevicePixelRatio + "，当前值为:" + window.devicePixelRatio + ",可能需要设置显示器缩放比率才能更准确的复刻动作。");
                            }
                        }
                        rootElement.__PlayingAPILogRecord = true; // 标记控件正在播放API日志，暂时禁止所有事件
                        nativeRef.invokeMethod("BeginPlayAPILogRecord");
                        var recordCount = 0;
                        var dtmPlay = new Date();
                        var allRecordCount = records.length;
                        var lastTick = 0;
                        var localPlaySpeed = 10;
                        function PlayFirstRecord(records, nativeRef) {
                            if (records.length == 0) {
                                return;
                            }
                            if (new Date().valueOf() - dtmPlay.valueOf() > 1000) {
                                dtmPlay = new Date();
                                console.log("%cDCWriter正在播放第" + recordCount + "/" + allRecordCount + "条API记录...", "color:blue");
                            }
                            var record = records.shift(0);
                            if (records.length > 0 && typeof (record.Tick) == "number" && records[0] != null) {
                                var nextTick = records[0].Tick;
                                if (typeof (nextTick) == "number") {
                                    localPlaySpeed = nextTick - record.Tick;
                                    if (localPlaySpeed < 10) {
                                        localPlaySpeed = 10;
                                    }
                                    else if (localPlaySpeed > 100) {
                                        localPlaySpeed = 100;
                                    }
                                }
                            }
                            if (record.Name == "@LoadDocumentFromString@") {
                                var strXmlContent = record.Content;
                                WriterControl_IO.LoadDocumentFromString({
                                    WriterControl: rootElement,
                                    Data: strXmlContent
                                });
                            }
                            else if (record.Name == "@JavaScript") {
                                if (record.Method == "ShowCaret") {
                                    var args6 = record.Args;
                                    WriterControl_UI.ShowCaret(
                                        rootElement,
                                        args6.intPageIndex,
                                        args6.intDX,
                                        args6.intDY,
                                        args6.intWidth,
                                        args6.intHeight,
                                        args6.bolVisible,
                                        args6.bolReadonly,
                                        args6.bolScrollToView,
                                        "PlayAPILogRecord");
                                }
                            }
                            else if (record.Name == "#DebugPrint") {
                                console.log("%cAPI调试信息:" + record.Message, "color:blue");
                            }
                            else if (record.Args == null) {
                                nativeRef.invokeMethod(record.Name); // 无参数
                            }
                            else {
                                // 有参数
                                var args = record.Args;
                                for (var argCount = 0; argCount < args.length; argCount++) {
                                    var item = args[argCount];
                                    if (typeof (item) == "string" && item.startsWith("__Uint8Array:")) {
                                        var strBase64 = item.substring(13);
                                        var bsData = DCTools20221228.FromBase64String(strBase64);
                                        args[argCount] = bsData;
                                    }
                                }
                                nativeRef.invokeMethod(record.Name, ...args);
                            }
                            recordCount++;
                            if (records.length > 0) {
                                if (typeof (playSpeed) == "number") {
                                    window.setTimeout(PlayFirstRecord, playSpeed, records, nativeRef);
                                }
                                else {
                                    window.setTimeout(PlayFirstRecord, localPlaySpeed, records, nativeRef);
                                }
                            }
                            else {
                                console.log("%c结束###DCWriter共播放了" + recordCount + "条API记录。", "color:red");
                                rootElement.__PlayingAPILogRecord = false; // 标记控件结束播放API日志，启用所有事件
                            }
                        };
                        PlayFirstRecord(records, nativeRef);
                    }, 100);
                };
            }
        };
        file.click();
        //在编辑器的window重新获取焦点时,确保点击取消或X时能正确删除file
        window.addEventListener('focus', function () {
            setTimeout(function () {
                if (file.parentNode == rootElement) {
                    rootElement.removeChild(file);
                }
            }, 100);
        }, { once: true });
    },
    /**
     * 重新加载承载的控件
     * @param {string} strContainerID 编辑器控件编号
     */
    ReloadHostControls: function (strContainerID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(strContainerID);
        if (rootElement == null) {
            return;
        }
        //if (rootElement.getAttribute("dctype") != "WriterControlForWASM") {
        //    // 不是编辑器控件，不处理
        //    return;
        //}
        var pageContainer = null;
        if (rootElement.getAttribute("dctype") == "WriterControlForWASM" || rootElement.getAttribute("dctype") == "WriterPrintPreviewControlForWASM") {
            // 编辑器控件对象
            if (WriterControl_Print.IsInPrintPreview(rootElement) == true) {
                // 处于打印预览模式
                pageContainer = WriterControl_Print.GetPrintPrewViewPageContainer(rootElement);
            }
            else {
                pageContainer = WriterControl_UI.GetPageContainer(rootElement);
            }
        }
        else {

        }
        if (pageContainer == null) {
            // 未找到容器元素，退出
        }
        window.setTimeout(function () {
            var infos = rootElement.__DCWriterReference.invokeMethod("GetHostControlInfos");
            /** 所有的控件元素 */
            var allElements = pageContainer.querySelectorAll("[dctype='HostControl']");
            allElements = Array.prototype.slice.call(allElements);
            if (infos != null && infos.length > 0) {
                for (var info of infos) {
                    // 首先找到对应的HTML元素
                    var curElement = null;
                    for (var index88 = 0; index88 < allElements.length; index88++) {
                        var element = allElements[index88];
                        if (element.ObjectElementInstanceIndex == info.ObjectElementInstanceIndex) {
                            curElement = element;
                            allElements.splice(index88, 1);
                            break;
                        }
                    }
                    var page = null;
                    for (var curPage = pageContainer.firstChild; curPage != null; curPage = curPage.nextSibling) {
                        if (curPage.nodeName == "CANVAS"
                            && curPage.PageIndex == info.PageIndex) {
                            page = curPage;
                            break;
                        }
                    }
                    //var page = WriterControl_Paint.GetCanvasElementByPageIndex(rootElement, info.PageIndex);
                    if (page == null) {
                        if (curElement != null) {
                            allElements.push(curElement);
                        }
                        continue;
                    }
                    var strControlTypeName = info.TypeFullName;
                    if (strControlTypeName != null) {
                        strControlTypeName = strControlTypeName.trim().toLowerCase();
                    }
                    if (curElement == null) {
                        // 没有找到元素则创建元素
                        if (strControlTypeName == "dcsoft.writer.controls.mediaplayercontroler") {
                            // 处理插入音视频使用FileSystemName时无法播放的问题【DUWRITER5_0-2420】
                            var mediaEleProps = rootElement.GetElementProperties(info.ElementID);
                            if (mediaEleProps && !info.Parameter) {
                                info.Parameter = mediaEleProps.FileSystemName;
                            }
                            curElement = CreateVideoEle(rootElement, info);
                        }
                        else if (strControlTypeName == "iframe") {
                            curElement = rootElement.ownerDocument.createElement("iframe");
                            curElement.src = info.Text;
                        }
                        else if (strControlTypeName == "div") {
                            curElement = rootElement.ownerDocument.createElement("div");
                            curElement.innerHTML = info.Text;
                        }
                        else if (strControlTypeName == "svg") {
                            curElement = rootElement.ownerDocument.createElement("svg");
                            curElement.id = info.ElementID;
                            curElement.innerHTML = info.Text;
                        }
                        // 触发事件，让用户自定义处理控件元素加载后的处理
                        info.HtmlElement = curElement;
                        info.WriterControl = rootElement;
                        if (curElement != null) {
                            if (info.ElementID != null) {
                                curElement.id = info.ElementID;
                            }
                            curElement.style.width = info.Width + "px";
                            curElement.style.height = info.Height + "px";
                        }
                        WriterControl_Event.RaiseControlEvent(rootElement, "EventCreateHostControl", info);
                        // 用户可能会自己创建控件元素，则使用用户创建的元素
                        curElement = info.HtmlElement;
                        if (curElement != null) {
                            pageContainer.insertBefore(curElement, pageContainer.firstChild);
                        }
                    }//if
                    if (curElement == null) {
                        // 未能创建元素，则下一个
                        continue;
                    }
                    curElement.ObjectElementInstanceIndex = info.ObjectElementInstanceIndex;
                    curElement.setAttribute("dctype", "HostControl");
                    curElement.setAttribute("TypeFullName", info.TypeFullName);
                    curElement.style.position = "absolute";
                    curElement.style.zIndex = 1000;
                    curElement.style.border = "1px solid black";
                    curElement.style.left = (page.offsetLeft + info.Left + 1) + "px";
                    curElement.style.top = (page.offsetTop + info.Top + 1) + "px";
                    curElement.style.width = info.Width + "px";
                    curElement.style.height = info.Height + "px";
                }//for
            }
            // 删除剩下的所有无主的承载元素
            allElements.forEach((item) => item.remove());
        }, 10);

        ////所有执行过播放的元素，通过里面的ObjectElementInstanceIndex判断
        ////var infos = rootElement.__DCWriterReference.invokeMethod("GetHostControlInfos");
        //setTimeout(function () {
        //    //整理之前的视频元素
        //    var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
        //    if (pageContainer == null) {
        //        return;
        //    }

        //    //判断是否存在全局展示属性
        //    var hasShowMediaAttr = rootElement.getAttribute("showmediaonmask");
        //    if (typeof hasShowMediaAttr == "string" && hasShowMediaAttr.toLowerCase().trim() == "true") {
        //        if (rootElement.closeScreenChange) {
        //            return;
        //        }

        //        var hasMask = rootElement.querySelector("#dcHostContorlMask");
        //        //判断是否当前选中为音视频
        //        var mediaEle = rootElement.GetElementProperties(rootElement.CurrentElement());
        //        if (mediaEle && mediaEle.TypeName == "XTextMediaElement") {
        //            if (!hasMask) {
        //                //创建遮罩层
        //                hasMask = document.createElement("div");
        //                rootElement.appendChild(hasMask);
        //                hasMask.id = "dcHostContorlMask";
        //                hasMask.style.width = "100%";
        //                hasMask.style.height = "100%";
        //                hasMask.style.position = "fixed";
        //                hasMask.style.zIndex = 1000;
        //                hasMask.style.top = "0";
        //                hasMask.style.left = '0';
        //                hasMask.style.overflow = "hidden";
        //                hasMask.style.backgroundColor = "rgba(0,0,0,0.3)";

        //            }
        //            var hasCreate = false;
        //            //找到对应的infos
        //            for (var i = 0; i < infos.length; i++) {
        //                var eleInfo = infos[i];
        //                if (eleInfo.ElementID == mediaEle.ID) {
        //                    eleInfo.Parameter = mediaEle.FileSystemName ? mediaEle.FileSystemName : eleInfo.Parameter;
        //                    var htmlElement = CreateEle(eleInfo, true, mediaEle, hasMask);

        //                    hasMask.appendChild(htmlElement);
        //                    htmlElement.style.width = "800px";
        //                    htmlElement.style.height = "600px";
        //                    htmlElement.style.transform = "translate(-50%, -50%)";
        //                    htmlElement.style.left = "50%";
        //                    htmlElement.style.top = "50%";
        //                    hasCreate = true;
        //                    break;
        //                }
        //            }

        //            if (!hasCreate) {
        //                hasMask.remove();
        //            }

        //        }
        //    } else {
        //        //获取到所有的在播放视频
        //        var allVideo = pageContainer.querySelectorAll('[dctype="HostControl"]');
        //        allVideo = Array.prototype.slice.call(allVideo);

        //        //循环所有的infos
        //        for (var i = 0; i < infos.length; i++) {
        //            var eleInfo = infos[i];

        //            var hasVideo = null;
        //            var thisIndex = null;
        //            if (allVideo && allVideo.length != 0) {
        //                hasVideo = allVideo.find((video, index) => {
        //                    if (video.__ObjectElementInstanceIndex == eleInfo.ObjectElementInstanceIndex) {
        //                        thisIndex = index;
        //                        return true;
        //                    }
        //                });
        //            }

        //            if (hasVideo != null && thisIndex != null) {
        //                //如果存在视频元素,只修改位置
        //                //console.log(hasVideo);
        //                UpdatePosition(eleInfo, hasVideo);
        //                //从数组中移除
        //                allVideo.splice(thisIndex, 1);
        //            } else {
        //                //插入新的视频元素
        //                //获取到编辑器内的视频元素
        //                var targetMediaEle = rootElement.CurrentElement('xtextmediaelement');
        //                //获取到当前选中元素
        //                var targetEleAttr = null;
        //                if (targetMediaEle != null) {
        //                    //判断是否存在StringTag
        //                    targetEleAttr = rootElement.GetElementProperties(targetMediaEle);
        //                } else {
        //                    return;
        //                }
        //                eleInfo.Parameter = targetEleAttr.FileSystemName ? targetEleAttr.FileSystemName : eleInfo.Parameter;

        //                var htmlElement = CreateEle(eleInfo);
        //                pageContainer.insertBefore(htmlElement, pageContainer.firstChild);
        //                //在此处写入监听事件
        //                //htmlElement.onkeydown = function (e) {
        //                //    //console.log(e);
        //                //    if (e.keyCode == '8' || e.keyCode == '46') {
        //                //        htmlElement.remove();
        //                //    }
        //                //};
        //                if (htmlElement != null) {
        //                    htmlElement.setAttribute("dctype", "HostControl");
        //                    htmlElement.__ObjectElementInstanceIndex = eleInfo.ObjectElementInstanceIndex;
        //                    UpdatePosition(eleInfo, htmlElement);
        //                    var content = rootElement.CurrentElement("xtextcontainerelement");
        //                    rootElement.FocusElement(content);
        //                }

        //            }

        //        }
        //        //删除不被使用的video
        //        allVideo.forEach((item) => item.remove());
        //    }
        //}, 10);
        ////更新定位
        //function UpdatePosition(eleInfo, htmlElement) {
        //    var page = WriterControl_Paint.GetCanvasElementByPageIndex(rootElement, eleInfo.PageIndex);
        //    if (page == null) {
        //        return;
        //    }
        //    var hasCanvasEle = rootElement.querySelector("[oeii='" + eleInfo.ObjectElementInstanceIndex + "']");// rootElement.GetElementById(eleInfo.ElementID);
        //    //判断页面是否存在此元素
        //    if (hasCanvasEle != null) {
        //        htmlElement.style.left = (page.offsetLeft + eleInfo.Left + 1) + "px";
        //        htmlElement.style.top = (page.offsetTop + eleInfo.Top + 1) + "px";
        //        htmlElement.style.width = eleInfo.Width + "px";
        //        htmlElement.style.height = eleInfo.Height + "px";
        //    } else {
        //        //不存在直接删除
        //        htmlElement.remove();
        //    }
        //};
        //创建元素的方法
        function CreateVideoEle(rootElement, eleInfo, type, mediaEle, hasMask) {
            //只需要在此处判断是音频文件还是视频文件
            var allVideoType = ['mpg', 'mpeg', 'avi', 'rm', 'rmvb', 'mov', 'wmv', 'asf', 'dat', 'mp4'];
            var allAudioType = ['mp3', 'wma', 'rm', 'wav', 'mid', 'ape', 'flac', 'ogg'];
            //截取最后的后缀
            var lastSuffix = eleInfo.Parameter ? eleInfo.Parameter.split('.') : [];
            lastSuffix = lastSuffix[lastSuffix.length - 1];
            var htmlElement = null;
            if (allVideoType.includes(lastSuffix)) {
                htmlElement = rootElement.ownerDocument.createElement("video");
            } else if (allAudioType.includes(lastSuffix)) {
                htmlElement = document.createElement("audio");
            }
            if (htmlElement == null) {
                return;
            }
            htmlElement.id = eleInfo.ElementID;
            htmlElement.setAttribute("dctype", "");
            htmlElement.style.border = "1px solid black";
            htmlElement.style.backgroundColor = "#FFFFFF";
            htmlElement.style.position = "absolute";
            htmlElement.zIndex = 1000;
            htmlElement.src = eleInfo.Parameter;
            htmlElement.controls = true;
            htmlElement.autoplay = true;
            htmlElement.loop = true;

            if (type) {
                //创建删除的按钮
                var deleteButton = document.createElement("div");
                hasMask.appendChild(deleteButton);
                deleteButton.id = "dc_MediaCloseButton";
                deleteButton.style.width = "30px";
                deleteButton.style.height = "30px";
                deleteButton.style.position = "absolute";
                deleteButton.style.zIndex = 1001;
                deleteButton.style.transform = "translate(360px, -290px)";
                deleteButton.style.left = "50%";
                deleteButton.style.top = "50%";
                deleteButton.style.textAlign = "center";
                deleteButton.style.lineHeight = "2";
                deleteButton.style.display = "none";
                //deleteButton.innerText = "X";
                var style = document.createElement("style");
                document.head.appendChild(style);
                var sheet = style.sheet;
                //支持非IE的现代浏览器
                sheet.insertRule("#dc_MediaCloseButton::before{ position: absolute;content: '';width: 6px;height: 30px;background: #222;transform: rotate(45deg);top: calc(50% - 6px);left: 50 %;}");
                sheet.insertRule("#dc_MediaCloseButton::after{ position: absolute;content: '';width: 6px;height: 30px;background: #222;transform: rotate(-45deg);top: calc(50% - 6px);left: 50 %;}");

                //进入全屏关闭全屏
                htmlElement.onfullscreenchange = function () {
                    const isFullScreen = document.fullScreen || document.mozFullScreen || document.webkitIsFullScreen;
                    if (!isFullScreen) {
                        //htmlElement.onfullscreenchange = null;
                        ////  退出全屏
                        //console.log('退出全屏')
                        //htmlElement.pause();
                        //htmlElement.removeAttribute("src");
                        //htmlElement.load();
                        ////删除元素
                        //htmlElement.remove();
                        //hasMask.remove();
                        //document.head.removeChild(style);
                        //var content = rootElement.CurrentElement("xtextcontainerelement");
                        //rootElement.FocusElement(content);
                        //将光标移到音频元素之外
                        rootElement.closeScreenChange = true;
                        setTimeout(() => {
                            rootElement.closeScreenChange = false;
                        }, 1000);
                    }
                };
                //移动显示样式
                hasMask.addEventListener("mouseover", function (e) {
                    var target = e.target;
                    if (target.id == "dc_MediaCloseButton" || target.nodeName == "VIDEO" || target.nodeName == "AUDIO") {
                        deleteButton.style.display = "block";
                    } else {
                        deleteButton.style.display = "none";
                    }

                });
                //点击关闭按钮
                deleteButton.addEventListener("click", function () {
                    htmlElement.pause();
                    htmlElement.removeAttribute("src");
                    htmlElement.load();
                    htmlElement.onfullscreenchange = null;
                    //删除元素
                    htmlElement.remove();
                    hasMask.remove();
                    style.remove();
                    var content = rootElement.CurrentElement("xtextcontainerelement");
                    rootElement.FocusElement(content);
                });

                //监听esc
                document.body.addEventListener("keydown", keydownEvent);
            }

            function keydownEvent(e) {
                if (e.key == "Escape") {
                    document.body.removeEventListener("keydown", keydownEvent);
                    htmlElement.pause();
                    htmlElement.removeAttribute("src");
                    htmlElement.load();
                    htmlElement.onfullscreenchange = null;
                    htmlElement.remove();
                    hasMask.remove();
                    style.remove();
                    var content = rootElement.CurrentElement("xtextcontainerelement");
                    rootElement.FocusElement(content);
                }
            }

            return htmlElement;
        }
    },
    /**
     * 获得所有的页码数组
     * @param {string | HTMLElement} strContainerID 编辑器控件对象
     * @returns {Array} 页码数组
     */
    GetAllPageIndexs: function (strContainerID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(strContainerID);
        if (rootElement != null && rootElement.IsWriterPrintPreviewControlForWASM == true) {
            // 打印预览控件
            var pageContainer = DCTools20221228.GetChildNodeByDCType(rootElement, "page-printpreview");
            var result2 = new Array();
            if (pageContainer != null) {
                for (var node = pageContainer.firstChild; node != null; node = node.nextSibling) {
                    if (node.nodeName.toLowerCase() == "svg") {
                        result2.push(node.PageIndex);
                    }
                }
            }
            return result2;
        }
        var pages = WriterControl_UI.GetPageCanvasElements(strContainerID);
        if (pages != null) {
            var result = new Array(pages.length);
            for (var iCount = 0; iCount < pages.length; iCount++) {
                result[iCount] = pages[iCount].PageIndex;
            }
            return result;
        }
        return [];
    },
    /**
     * 滚动视图到指定页
     * @param {string} strContainerID 编辑器编号
     * @param {number} intPageIndex 指定的从0开始的页码
     * @returns {boolean} 操作是否滚动了视图
     */
    MoveToPage: function (strContainerID, intPageIndex, bolUseClick) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(strContainerID);
        if (rootElement == null) {
            return null;
        }
        intPageIndex = parseInt(intPageIndex);
        var pages = WriterControl_UI.GetPageCanvasElements(rootElement);
        if (pages != null && intPageIndex >= 0) {
            for (var iCount = 0; iCount < pages.length; iCount++) {
                var page = pages[iCount];
                if (page.PageIndex == intPageIndex) {
                    var c = page.offsetParent;
                    c.scrollTo({ left: page.offsetLeft, top: page.offsetTop, behavior: "smooth" });
                    if (bolUseClick == true) {
                        page.click();
                    }
                    //page.scrollIntoView(true);
                    return true;
                }
            }
            return true;
        }
        return false;
    },
    /**
    * 设置缩放比率
    * @param {number} newZoomRate 新的缩放比率，必须在0.1到5之间
    * @returns {boolean} 操作是否修改缩放比率
    */
    EditorSetZoomRate: function (rootElement, newZoomRate) {
        if (WriterControl_UI.__RaisingEventZoomChanged == true) {
            // 避免递归调用
            return;
        }
        newZoomRate = Number(newZoomRate);
        var oldZoomRate = rootElement.__DCWriterReference.invokeMethod("get_ZoomRate");
        var newZoomRate = rootElement.__DCWriterReference.invokeMethod(
            "SetViewZoomRate",
            newZoomRate);
        if (newZoomRate != oldZoomRate) {
            //if (rootElement.FunctionHandle_OnScroll) {
            //    window.clearTimeout(rootElement.FunctionHandle_OnScroll);
            //    delete rootElement.FunctionHandle_OnScroll
            //}
            WriterControl_Task.ClearTask();
            var elements = WriterControl_UI.GetPageCanvasElements(rootElement);
            for (var iCount = 0; iCount < elements.length; iCount++) {
                var element = elements[iCount];
                WriterControl_Paint.SetPageElementSize(rootElement, element);
                element._isRendered = false;
                //var ctx = element.getContext("2d");
                //ctx.clearRect(0, 0, element.width, element.height);
            }
            WriterControl_Paint.HandleScrollView(rootElement);
            //rootElement.FunctionHandle_OnScroll = window.setTimeout(
            //    WriterControl_Paint.HandleScrollView, 50, rootElement);
            rootElement.__DCWriterReference.invokeMethod("UpdateTextCaretWithoutScroll");
            WriterControl_Paint.UpdateViewForWaterMark(rootElement);
            WriterControl_Rule.InvalidateAllView(rootElement);
            WriterControl_UI.ReloadHostControls(rootElement);
            WriterControl_Print.UpdateZoomRateForPrintPreview(rootElement);
            // 触发事件
            WriterControl_UI.__RaisingEventZoomChanged = true;
            WriterControl_Event.RaiseControlEvent(
                rootElement,
                "EventZoomChanged",
                {
                    OldZoomRate: oldZoomRate,
                    ZoomRate: newZoomRate
                });
            WriterControl_UI.__RaisingEventZoomChanged = false;

            WriterControl_UI.TableHeaderFixed(rootElement);
            return true;
        }
        return false;
    },

    /** 根据界面大小来缩放编辑器
    * @param {node} rootElement 编辑器元素
    * @return {boolean} 是否成功缩放
    */
    EditorSetWriterAutoZoom: function (rootElement) {
        if (!rootElement || typeof (rootElement.SetZoomRate) != "function") {
            return false;
        }
        var IsPrintPreview = rootElement.IsPrintPreview();//是否是打印预览模式
        var SelectStr = IsPrintPreview ? "[dctype='page-printpreview']" : "[dctype='page-container']";
        // 正文元素
        var PageDiv = rootElement.querySelector(SelectStr);
        if (!PageDiv) {
            return false;
        }
        // 让滚动条显示
        PageDiv.style.overflow = "scroll";
        // 第一个页面canvas元素
        var canvasNode = IsPrintPreview ? PageDiv.querySelector('svg[dctype="page"]') : PageDiv.querySelector('canvas[dctype="page"]');
        if (!canvasNode) {
            return false;
        }
        // 获取canvas元素所有样式
        var canvasStyles = GetNodeStyles(canvasNode);
        // canvas元素宽度
        var canvasWidth = canvasNode.clientWidth;
        // 防止页面开始就有缩放，所以获取原本的宽度
        if (canvasNode.hasAttribute("native-width")) {
            canvasWidth = ChangeIntoNumber(canvasNode.getAttribute("native-width"));
        }
        // 添加边框左右宽度
        canvasWidth += ChangeIntoNumber(canvasStyles.borderLeftWidth) + ChangeIntoNumber(canvasStyles.borderRightWidth);
        // 添加外边距左右宽度
        canvasWidth += ChangeIntoNumber(canvasStyles.marginLeft) + ChangeIntoNumber(canvasStyles.marginRight);
        // 缩放的比例
        var zoomNumber = PageDiv.clientWidth / canvasWidth;
        // 是否需要处理缩放比例小数位
        zoomNumber = parseInt(zoomNumber * 100) / 100;// 取小数点后面两位
        // zoomNumber = ChangeIntoNumber(zoomNumber.toFixed(5));
        // 开始缩放
        var SetZoomRateResult = WriterControl_UI.EditorSetZoomRate(rootElement, zoomNumber);
        // 还原之前的样式
        PageDiv.style.overflow = "auto";

        // 返回缩放结构
        return SetZoomRateResult;

        /** 获取元素所有的样式对象
        * @param {node} node 元素
        * @return {object} 元素样式对象
        */
        function GetNodeStyles(node) {
            // 兼容IE和火狐谷歌等的写法
            var computedStyle = window.getComputedStyle && window.getComputedStyle(node, null) || node.currentStyle;
            return computedStyle || {};
        }

        /** 将内容变成数值类型
        * @param {*} str 需要转化的内容
        * @return {number} NUMBER 数值
        */
        function ChangeIntoNumber(str) {
            var NUMBER = str;
            if (typeof (str) != "number") {
                NUMBER = parseFloat(str);
            }
            if (isNaN(NUMBER) == true) {
                NUMBER = 0;
            }
            return NUMBER;
        }
    },


    /**设置页面排版模式,可以为SingleColumn,MultiColumn,Horizontal
     * @param {string} strMode 排版类型，可以为SingleColumn,MultiColumn,Horizontal。
     */
    EditorSetPageLayoutMode: function (rootElement, strMode) {
        if (strMode == null || strMode.length == 0) {
            strMode = "MultiColumn";
        }
        if (rootElement.__DCWriterReference.invokeMethod("GetNormalViewMode") == true) {
            // 普通视图模式,强制设置为单栏模式
            if (strMode.trim().toLowerCase() != "singlecolumn") {
                console.warn("普通视图模式只支持单栏显示,不支持:" + strMode);
            }
            strMode = "SingleColumn";
        }
        rootElement.setAttribute("pagelayoutmode", strMode);
        var pages = WriterControl_UI.GetPageContainer(rootElement);

        //[DUWRITER5_0-3585] 20240918 lxy 兼容svg打印预览模式视图模式调整
        var printPage = rootElement.querySelector("[dctype='page-printpreview']");
        if (printPage && printPage.style.display != "none") {
            pages = printPage;
        }

        if (pages != null) {
            strMode = strMode.trim().toLocaleLowerCase();
            if (strMode == "horizontal") {
                pages.style.whiteSpace = "nowrap";
                pages.style.textAlign = "";
                var clientHeight = pages.clientHeight;
                var maxHeight = 0;
                for (var element = pages.firstChild;
                    element != null;
                    element = element.nextSibling) {
                    if (element.nodeName == "CANVAS" || element.nodeName == "svg") {
                        element.style.display = "inline-block";
                        var nh = parseInt(element.getAttribute("native-height"));
                        if (nh > maxHeight) {
                            maxHeight = nh;
                        }
                    }
                }
                var zoomRate = (pages.offsetHeight - 35) / (maxHeight + 4);
                rootElement.SetZoomRate(zoomRate);
            }
            else {
                pages.style.whiteSpace = "";
                pages.style.textAlign = "center";
                for (var element = pages.firstChild;
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
            // 修正区域选择打印蒙版位置
            if (rootElement.RectInfo && typeof (rootElement.RectInfo.AdjustBoundsSelectionStyle) == "function") {
                rootElement.RectInfo.AdjustBoundsSelectionStyle();
            }
            WriterControl_Task.AddTask(function () {
                WriterControl_Paint.HandleScrollView(rootElement);
                rootElement.__DCWriterReference.invokeMethod("UpdateTextCaretWithoutScroll");
            });
            //WriterControl_Paint.HandleScrollView(rootElement);
            //window.setTimeout(function () {
            //    rootElement.__DCWriterReference.invokeMethod("UpdateTextCaretWithoutScroll");
            //}, 50);
        }
    },


    /**
     * 触发更新工具条按钮状态事件
     * @param {string | HTMLElement} containerID 控件对象
     */
    RaiseEventUpdateToolarState: function (containerID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);// DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement != null) {
            var task = {
                TypeName: "ToolBoxButtonState",
                Priority: 100,// 优先级很低的
                WaitingTime: 500,// 需要等待500毫秒才执行任务
                OwnerWriterControl: rootElement,
                // 判断能否吞并其他任务
                CanEatTask: function (otherTask) {
                    if (otherTask.TypeName == "ToolBoxButtonState"
                        && otherTask.OwnerWriterControl == this.OwnerWriterControl) {
                        return true;
                    }
                },
                // 执行任务
                RunTask: function (thisTask) {
                    try {
                        this.OwnerWriterControl.RaiseEvent("EventUpdateToolarState");
                    }
                    catch (err) {
                        console.error(err);
                    }
                }
            };
            WriterControl_Task.AddTask(task);
        }
    },
    /**
     * 获得指定编号的容器元素下的所有的画布元素
     * @param {string} containerElementID 容器元素编号
     * @returns {Array} 画布元素列表
     */
    GetPageCanvasElements: function (containerElementID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerElementID);
        var pageContainer = WriterControl_UI.GetPageContainer(containerElementID);
        if (pageContainer != null) {
            var node = pageContainer.firstChild;
            var result = new Array();
            while (node != null) {
                if ((node.nodeName == "CANVAS" || node.nodeName == "svg") && node.getAttribute("dctype") == "page") {
                    result.push(node);
                } else if (rootElement.IsWriterPrintPreviewControlForWASM === true && node.nodeName == "svg" && node.getAttribute("dctype") == "page") {
                    result.push(node);
                }
                node = node.nextSibling;
            }
            return result;
        }
        return null;
        //throw "未找到指定容器元素:GetPageCanvasElements";
    },

    /**
     * 获得页面元素容器对象
     * @param {string | HTMLElement} rootID 根节点名称
     * @returns { HTMLDivElement} 容器元素对象
     */
    GetPageContainer: function (rootID) {
        if (rootID != null && rootID.getAttribute) {
            if (rootID.getAttribute("dctype") == "page-container") {
                return rootID;
            }
            else if (rootID.getAttribute("dctype") == "page") {
                return rootID.parentNode;
            }
        }
        let rootElement = DCTools20221228.GetOwnerWriterControl(rootID);// DCTools20221228.GetOwnerWriterControl(rootID);
        if (rootElement == null) {
            return null;
        }
        if (rootElement.__DCDisposed == true) {
            return null;
        }
        let div = DCTools20221228.GetChildNodeByDCType(rootElement, "page-container");
        if (div == null) {
            div = rootElement.ownerDocument.createElement("DIV");
            div.setAttribute("dctype", "page-container");
            div.style.setProperty('position', 'relative', 'important');
            div.style.textAlign = 'center';
            div.style.height = "100%";
            div.style.overflow = "auto";
            div._rootElementId = rootElement.id;
            div.addEventListener('wheel', WriterControl_UI.PageContainerOnWheelFunc);


            // div.onwheel = function (e) {
            //     if (!e.currentTarget._rootElementId) {
            //         return
            //     }
            //     let rootElement=window.DCTools20221228.GetOwnerWriterControl(e.currentTarget._rootElementId)
            //     let innerDiv=e.currentTarget
            //     //在pc端如果存在下拉框则禁止页面滚动
            //     if (!('ontouchstart' in rootElement.ownerDocument.documentElement)) {
            //         let dropdown = rootElement.querySelector('#divDropdownContainer20230111');
            //         if (dropdown != null && dropdown.style.display != 'none') {
            //             e.stopPropagation();
            //             e.preventDefault();
            //             return false;
            //         }

            //     }
            //     if (innerDiv.style.whiteSpace != null
            //         && innerDiv.style.whiteSpace.toLocaleLowerCase() == "nowrap"
            //         && e.ctrlKey == false) {
            //         // X,Y滚动量置换
            //         innerDiv.scrollBy(e.deltaY, e.deltaX);
            //         e.stopPropagation();
            //         e.preventDefault();
            //     }
            // };

            div.addEventListener('scroll', WriterControl_UI.PageContainerOnscrollFunc);
            // div.onscroll = function (e) {
            //     //如果是普通视图模式
            //     if (rootElement.ViewMode == "CompressPage") {
            //         //console.log(e);
            //         //判断是否存在表头固定展示的方法
            //         var hasTableheaderfixed = rootElement.getAttribute("tableheaderfixed");
            //         //console.log(hasTableheaderfixed);
            //         //存在表格头固定属性
            //         if (typeof hasTableheaderfixed == "string" && hasTableheaderfixed.toLowerCase().trim() == "true") {
            //             //var allTable = rootElement.GetElementsByTypeName("XTextTableElement");
            //             //var allTable = rootElement.GetElementProperties(rootElement.CurrentTable());
            //             var allTable = rootElement.__DCWriterReference.invokeMethod("GetTablesForFixedHeader");
            //             //allTable = [allTable];
            //             //console.log(allTable)
            //             if (allTable && allTable.length > 0) {
            //                 //获取所有的canvas元素
            //                 var allCanvas = this.querySelectorAll("[dctype=page]");
            //                 //获取页面滚动位置
            //                 var scrollTop = this.scrollTop;
            //                 var zoomRate = rootElement.GetZoomRate();
            //                 //获取表格头的位置
            //                 for (var i = 0; i < allTable.length; i++) {
            //                     var thisTable = allTable[i];
            //                     var tableTop = parseFloat((thisTable.TopInOwnerPage / 300 * 96.00001209449).toFixed(2)) * zoomRate;
            //                     var tableLeft = parseFloat((thisTable.LeftInOwnerPage / 300 * 96.00001209449).toFixed(2)) * zoomRate;
            //                     //加上页面的位置
            //                     var tableTop2 = tableTop + allCanvas[thisTable.OwnerPageIndex].offsetTop;
            //                     //为非第一页时存在60的计算差此处减掉
            //                     //tableTop2 = tableTop2 - (thisTable.OwnerPageIndex > 0 ? 60 : 0);
            //                     var thisTableHeight = parseFloat((thisTable.Height / 300 * 96.00001209449).toFixed(2)) * zoomRate;
            //                     //获取到当前表格所有的表格头
            //                     var allRow = thisTable.Rows;
            //                     var tableHeaderRow = [];
            //                     var allTableRowHeight = 0;
            //                     for (var j = 0; j < allRow.length; j++) {
            //                         var thisRow = allRow[j];
            //                         thisRow = rootElement.GetElementProperties(thisRow);
            //                         if (thisRow.HeaderStyle === true) {
            //                             tableHeaderRow.push(thisRow);
            //                             allTableRowHeight += parseFloat((thisRow.Height / 300 * 96.00001209449).toFixed(2));
            //                         } else {
            //                             break;
            //                         }
            //                     }
            //                     allTableRowHeight = allTableRowHeight * zoomRate + 5;

            //                     if (scrollTop >= tableTop2 && scrollTop <= (tableTop2 + thisTableHeight - allTableRowHeight)) {
            //                         //var a = new Date().getTime();
            //                         //if (scrollTop >= tableTop ) {
            //                         if (this.Tableheaderfixed != thisTable.NativeHandle) {
            //                             this.Tableheaderfixed = thisTable.NativeHandle;

            //                             if (!this.htmlDiv) {
            //                                 this.htmlDiv = document.createElement("canvas");
            //                                 this.htmlDiv.style.position = "absolute";
            //                                 this.htmlDiv.style.backgroundColor = "#fff";
            //                                 div.insertAdjacentElement("afterend", this.htmlDiv);
            //                             }

            //                             var tableWidth = parseFloat((thisRow.Width / 300 * 96.00001209449).toFixed(2)) * zoomRate;
            //                             this.htmlDiv.setAttribute("width", tableWidth + 'px');
            //                             this.htmlDiv.setAttribute("height", allTableRowHeight + 'px');

            //                             var che2d = this.htmlDiv.getContext('2d');
            //                             che2d.drawImage(allCanvas[thisTable.OwnerPageIndex], tableLeft, tableTop, tableWidth, allTableRowHeight, 0, 0, tableWidth, allTableRowHeight);

            //                             this.htmlDiv.style.top = div.offsetTop - 5 + 'px';
            //                             this.htmlDiv.style.left = div.offsetLeft + allCanvas[thisTable.OwnerPageIndex].offsetLeft + tableLeft + 1 + 'px';
            //                             //console.log(thisTable)

            //                             //var oldScrollAttr = rootElement.getAttribute("notscrollwithcaret");
            //                             //rootElement.setAttribute("notscrollwithcaret", "true");

            //                             //var b = new Date().getTime();
            //                             //console.log(b - a);
            //                             //排序
            //                             //tableHeaderRow = tableHeaderRow.sort(function (a, b) { return a - b });
            //                             //rootElement.SelectContentByStartEndElement(tableHeaderRow[0][0], tableHeaderRow[0][tableHeaderRow[0].length - 1]);

            //                             //if (tableHeaderRow[0]) {
            //                             //    rootElement.SelectContentByStartEndElement(tableHeaderRow[0][0], tableHeaderRow[0][tableHeaderRow[0].length - 1]);
            //                             //    var select = rootElement.Selection;
            //                             //    var allSelectCells = select.Cells;
            //                             //    if (allSelectCells[0] != tableHeaderRow[0][0] || allSelectCells[allSelectCells.length - 1] != tableHeaderRow[tableHeaderRow.length - 1][tableHeaderRow[tableHeaderRow.length - 1].length - 1]) {
            //                             //        rootElement.SelectContentByStartEndElement(tableHeaderRow[0][0], tableHeaderRow[tableHeaderRow.length - 1][tableHeaderRow[tableHeaderRow.length - 1].length - 1]);
            //                             //    }
            //                             //}

            //                             //if (tableHeaderRow && tableHeaderRow.length > 0) {
            //                             //    rootElement.SelectContentByStartEndElement(tableHeaderRow[0], tableHeaderRow[tableHeaderRow.length - 1]);
            //                             //}

            //                             //创建一个html定位在div的开始位置
            //                             //if (!this.htmlDiv) {
            //                             //    this.htmlDiv = document.createElement("div");
            //                             //    this.htmlDiv.style.position = "absolute";
            //                             //    this.htmlDiv.style.backgroundColor = "#fff";
            //                             //    div.insertAdjacentElement("afterend", this.htmlDiv);
            //                             //}
            //                             //var c = new Date().getTime();
            //                             //console.log(c - b);

            //                             //var ccccc = rootElement.SelectionHtml();

            //                             //var d = new Date().getTime();
            //                             //console.log(d - c);

            //                             //this.htmlDiv.innerHTML = ccccc

            //                             //var content = rootElement.CurrentElement("xtextcontainerelement");
            //                             //rootElement.FocusElement(content);

            //                             //if (oldScrollAttr == null) {
            //                             //    rootElement.removeAttribute("notscrollwithcaret");
            //                             //} else {
            //                             //    rootElement.setAttribute("notscrollwithcaret", oldScrollAttr);
            //                             //}
            //                             ////找到所有的子元素
            //                             //var allChild = this.htmlDiv.childNodes;
            //                             //for (var z = 0; z < allChild.length; z++) {
            //                             //    if (allChild[z] && allChild[z].nodeName != "TABLE") {
            //                             //        allChild[z].remove();
            //                             //        z--;
            //                             //    }
            //                             //}

            //                             //var e = new Date().getTime();
            //                             //console.log(e - d);

            //                             ////找到所有的td
            //                             //var allTd = this.htmlDiv.querySelectorAll("td");
            //                             //for (var z = 0; z < allTd.length; z++) {
            //                             //    allTd[z].style.border = "1px solid #000"
            //                             //    allTd[z].style.textAlign = "center";
            //                             //}

            //                             //this.htmlDiv.style.top = div.offsetTop + 'px';
            //                             //this.htmlDiv.style.left = div.offsetLeft + allCanvas[thisTable.OwnerPageIndex].offsetLeft + parseFloat((thisTable.LeftInOwnerPage / 300 * 96.00001209449).toFixed(2)) + 'px';

            //                             //var f = new Date().getTime();
            //                             //console.log(f - e);
            //                         }
            //                         break;
            //                     }
            //                     if (i == allTable.length - 1) {
            //                         this.Tableheaderfixed = null;
            //                         if (this.htmlDiv) {
            //                             this.htmlDiv.remove();
            //                             this.htmlDiv = null;
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //     }

            //     WriterControl_Paint.AddTaskHandleScrollView(rootElement);
            //     //WriterControl_Paint.HandleScrollView(rootElement);
            //     WriterControl_Rule.HandleViewScroll(this);

            //     var dropdown = rootElement.querySelector('#divDropdownContainer20230111');

            //     //当rootElement尺寸发生改变时.关闭下拉
            //     if (dropdown != null && dropdown.style.display != 'none') {
            //         if ('ontouchstart' in rootElement.ownerDocument.documentElement) {
            //             dropdown.CloseDropdown();
            //         }
            //     }

            //     //如果存在知识库页面滚动时则关闭知识库
            //     var divknowledgeBase = rootElement.querySelector('#divknowledgeBase20240103');
            //     if (divknowledgeBase && divknowledgeBase.DistroyKnowledgeBase) {
            //         divknowledgeBase.DistroyKnowledgeBase();
            //     }
            //     // //关闭表格下拉输入域
            //     // var dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
            //     // if (dropdownTable && dropdownTable.CloseDropdownTable) {
            //     //     dropdownTable.CloseDropdownTable();
            //     // }


            //     //if (caret != null) {
            //     //    caret.style.display = 'none';
            //     //    clearInterval(caret.handleTimer)
            //     //}
            //     //获取到scrollTop
            //     //当前滚动的juli
            //     if (rootElement.GetPageNumberForView && typeof rootElement.GetPageNumberForView == 'function') {
            //         var scrollTop = div.scrollTop;
            //         //可视区域高度
            //         var visibleAreaHeight = div.clientHeight;
            //         var allCanvas = div.querySelectorAll('canvas');
            //         var maxIndex = 0;
            //         var maxHeight = 0;
            //         var maxCanvas = allCanvas[0];
            //         for (var i = 0; i < allCanvas.length; i++) {
            //             var thisCanvas = allCanvas[i];
            //             if (thisCanvas) {
            //                 //获取到这个canvas的位置  当多少位置时可以被看见
            //                 var thisTop = thisCanvas.offsetTop - 5;
            //                 var thisHeight = thisCanvas.clientHeight;
            //                 //判断是否进入可视区域
            //                 if (thisTop <= scrollTop + visibleAreaHeight && thisTop + thisHeight >= scrollTop) {
            //                     var overTop = (scrollTop - thisTop) > 0 ? (scrollTop - thisTop) : 0;
            //                     var overBottom = (thisTop + thisHeight - (scrollTop + visibleAreaHeight)) > 0 ? (thisTop + thisHeight - (scrollTop + visibleAreaHeight)) : 0;
            //                     //算出可视区域高度
            //                     var thisVisible = thisHeight - overTop - overBottom;
            //                     if (thisVisible >= maxHeight) {
            //                         maxHeight = thisVisible;
            //                         maxIndex = thisCanvas.PageIndex;
            //                         maxCanvas = thisCanvas;
            //                     }
            //                 }
            //             }
            //         }
            //         rootElement.GetPageNumberForView(maxIndex + 1, maxCanvas);
            //     }
            // };
            div.addEventListener('resize', WriterControl_UI.PageContainerOnresizeFunc);
            // div.onresize = function () {
            //     WriterControl_Rule.HandleViewScroll(this);

            //     var dropdown = rootElement.querySelector('#divDropdownContainer20230111');
            //     //当rootElement尺寸发生改变时.关闭下拉
            //     if (dropdown != null) {
            //         dropdown.CloseDropdown();
            //     }

            //     //关闭表格下拉输入域
            //     var dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
            //     if (dropdownTable && dropdownTable.CloseDropdownTable) {
            //         dropdownTable.CloseDropdownTable();
            //     }

            //     WriterControl_UI.HideCaret(rootElement);
            // };
            rootElement.appendChild(div);
            rootElement.PageContainer = div;
        }

        if (rootElement.ViewMode != "CompressPage") {
            //判断是否存在div.
            if (div.htmlDiv) {
                div.htmlDiv.remove();
                div.htmlDiv = null;
                div.minCell = null;
                div.maxCell = null;
            }
        }
        return div;
    },

    /**判断是否正在显示下拉列表,本函数被DCWriterClass.IsDropdownControlVisible()调用
     * @returns {boolean} 是否正在显示下拉列表
     */
    IsDropdownControlVisible: function () {
        var div = document.getElementById("divDropdownContainer20230111");
        if (div == null) {
            return false;
        }
        if (div.style.display == "none") {
            // 时间下拉时原本的下拉会隐藏，所以需要特殊判断
            if (div.thisApi) {
                return true;
            }
            return false;
        }
        return true;
        return div != null && div.style.display != "none";
    },
    /**关闭下拉列表,本函数被DCWriterClass.CloseDropdownControl()调用 */
    CloseDropdownControl: function () {
        var div = document.getElementById("divDropdownContainer20230111");
        if (div != null && div.style.display != "none") {
            div.CloseDropdown();
        }
    },
    /**
     * 获得下拉列表容器元素
     * @param {string} containerID 容器元素编号
     * @param {number} intPageIndex 页码
     * @param {number} intLeft 在页面元素的坐标  离元素所在canvas左边的高度，非编辑器左边的高度
     * @param {number} intTop 在页面元素的坐标   离元素所在canvas顶部的高度，非编辑器顶部的高度
     * @param {number} intHeight 相关的文档元素的高度   目标输入域的高度，非文档的高度
     * @param {boolean} autoCreate 若不存在是否自动创建元素
     * @returns {HTMLDivElement} 获得的容器元素对象
     */
    GetDropdownContainer: function (containerID, intPageIndex, intLeft, intTop, intHeight, autoCreate) {

        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        var pageElement = WriterControl_Paint.GetCanvasElementByPageIndex(containerID, intPageIndex);
        if (pageElement == null) {
            throw "未找到页面元素";
        }
        // var currentInput = rootElement.GetElementProperties(rootElement.CurrentInputField());
        // if (currentInput && currentInput.SpecifyWidth) {
        //     var currentPositionObject = rootElement.GetAbsBoundsInDocument(rootElement.CurrentElement());//当前元素位置
        //     if (currentPositionObject.LeftInOwnerPage) {
        //         console.log('=============intLeft', intLeft);
        //         console.log('=============pageElement.offsetLeft', pageElement.offsetLeft);
        //         console.log('currentPositionObject.LeftInOwnerPage:', currentPositionObject.LeftInOwnerPage);
        //         console.log('currentPositionObject:', currentPositionObject);

        //     }
        // }

        var div = rootElement.ownerDocument.getElementById("divDropdownContainer20230111");
        if (div == null) {
            if (autoCreate == false) {
                //  不允许自动创建，则退出
                return null;
            }

            var cssText = `#divDropdownContainer20230111::-webkit-scrollbar{width: 8px;}
                           #divDropdownContainer20230111::-webkit-scrollbar-track{background-color: #ddd;}
                           #divDropdownContainer20230111::-webkit-scrollbar-thumb{background-color: #999;border-radius: 7px;}
                           #divDropdownContainer20230111 div::-webkit-scrollbar{width: 8px}
                           #divDropdownContainer20230111 div::-webkit-scrollbar-track{background-color: #ddd;}
                           #divDropdownContainer20230111 div::-webkit-scrollbar-thumb{background-color: #999;border-radius: 7px;}
                           #divDropdownContainer20230111{overscroll-behavior: contain}`;
            WriterControl_UI.CustomCSSString(rootElement.ownerDocument, cssText);
            div = rootElement.ownerDocument.createElement("DIV");
            //判断是存在
            div.id = "divDropdownContainer20230111";
            //div.style.border = "2px solid black";
            //div.style.backgroundColor ="#dddddd";
            //zhangbin 20230423 修改下拉框的样式
            // div.style.borderRadius = "10px";
            div.style.position = "absolute";
            div.style.display = "none";
            div.style.maxHeight = "250px";
            div.style.overflowY = 'auto';
            div.style.userSelect = 'none';
            div.style.boxShadow = '0 2px 8px 2px rgba(68, 73, 77, 0.16)';
            div.style.backgroundColor = "#fff";
            //div.style.width = "200px";
            //div.style.height = "200px";
            //rootelement在tab页切换的时候可能出现不准确的问题，在此处重新再获取一遍rootElement
            div.ShowDropdown = function () {
                var div = this,
                    rootElement = div;
                while (rootElement.getAttribute('dctype') != 'WriterControlForWASM') {
                    rootElement = rootElement.parentElement;
                }
                if (rootElement.mobileMousePosition == "mobileMousePosition") {
                    return true;
                }

                // 避免上一个时间选择界面未关闭
                if (div.thisApi && div.thisApi.theTool && typeof (div.thisApi.theTool.hidePanel) == "function") {
                    div.thisApi.theTool.hidePanel();
                    div.thisApi = null;
                }
                if ('ontouchstart' in rootElement.ownerDocument.documentElement) {
                    rootElement.ownerDocument.activeElement.blur();
                }
                //找到最后一个子元素
                var lastChild = div.children[div.children.length - 1];
                if (lastChild) {
                    var zoomRate = rootElement.GetZoomRate();
                    lastChild.style.zoom = zoomRate < 1 ? zoomRate : 1;
                }
                var currentInput = rootElement.GetElementProperties(rootElement.CurrentInputField());
                var pageElement = DCTools20221228.GetOwnerWriterControl(div.checkPageContainerID).querySelector('[dctype=page-container]');
                //console.log('ShowDropdown');
                //为了防止移动端展示不全此时处理left的展示位置

                //div.style.top = (div.__PageElement.offsetTop + div.__Top + div.__Height + 6) + "px";
                div.style.top = 0 + 'px';
                div.style.left = 0 + 'px';
                div.style.zIndex = 100001;
                // DUWRITER5_0-1820:兼容下拉输入域激活模式是单击，双击输入域不会触发元素双击事件EventElementMouseDblClick的问题
                //setTimeout(function () {
                div.style.display = "block";
                //})
                //[DUWRITER5_0-3273]解决定位在输入域上方时，过高的问题。先清空一遍弹出在输入域上方的记录
                div.removeAttribute('dc_up');
                //目标位置
                var targetTopPosition = (div.__PageElement.offsetTop + div.__Top + div.__Height);
                //距离编辑器底部的位置，判断底部是否能放下下拉
                var downGap = pageElement.clientHeight + pageElement.scrollTop - targetTopPosition;
                //如果能放下
                if (downGap >= div.offsetHeight) {
                    //div.style.top = (div.__Top - pageElement.scrollTop + pageElement.offsetTop + div.__Height + 12) + "px";
                    div.style.top = targetTopPosition + 3 + "px";
                    //如果不能放下
                } else if (downGap < div.offsetHeight) {
                    //判断距离编辑器头部是否留有大小
                    var upGap = targetTopPosition - div.__Height - pageElement.scrollTop;
                    //可以放下
                    if (upGap >= div.offsetHeight) {
                        div.style.top = upGap - div.offsetHeight + pageElement.scrollTop + 'px';
                        div.setAttribute('dc_up', 'true');//[DUWRITER5_0-3273]用于记录是否是从上方弹出的
                    } else {
                        //判断是否为下拉输入域
                        if (currentInput && currentInput.InnerEditStyle == "DropdownList") {
                            //判断是否存在页面高度小于下拉框高度的
                            if (downGap > upGap) {
                                div.style.top = targetTopPosition + "px";
                                div.style.maxHeight = downGap + 'px';
                                //是否存在搜索框
                                var hasSearchSpan = div.querySelector('.dropdownCodeArea');
                                var hasListBoxContainer = div.querySelector('.listBoxContainer');
                                if (hasListBoxContainer) {
                                    hasListBoxContainer.style.maxHeight = downGap - (hasSearchSpan ? 26 : 0) + 'px';
                                }
                            } else {
                                div.style.maxHeight = upGap + 'px';
                                var hasSearchSpan = div.querySelector('.dropdownCodeArea');
                                var hasListBoxContainer = div.querySelector('.listBoxContainer');
                                if (hasListBoxContainer) {
                                    hasListBoxContainer.style.maxHeight = upGap - (hasSearchSpan ? 26 : 0) + 'px';
                                }
                                div.style.top = upGap - div.offsetHeight + pageElement.scrollTop + 'px';
                                div.setAttribute('dc_up', 'true');//[DUWRITER5_0-3273]用于记录是否是从上方弹出的
                            }
                        } else {
                            //如果两边都放不下,则判断哪边间距大放在哪边
                            //if (downGap > upGap) {
                            //    div.style.top = targetTopPosition + "px";
                            //} else {
                            //    div.style.top = upGap - div.offsetHeight + pageElement.scrollTop + 'px';
                            // div.setAttribute('dc_up', 'true');//[DUWRITER5_0-3273]用于记录是否是从上方弹出的
                            //}
                            //滚动视图直到让下拉框展示全
                            //判断pageElement的高度是否超过输入域和弹窗高度和
                            if (pageElement.clientHeight >= (div.__Height + div.clientHeight)) {
                                //可以装下
                                //判断放在输入域下是否能展示全
                                if (pageElement.scrollHeight - targetTopPosition >= div.clientHeight) {
                                    //可以放下
                                    //计算差值
                                    var scrollHeight = div.offsetHeight - downGap;
                                    pageElement.scrollTo(pageElement.scrollLeft, pageElement.scrollTop + scrollHeight);
                                    div.style.top = targetTopPosition + 3 + 'px';
                                } else {
                                    //放不下
                                    pageElement.scrollTo(pageElement.scrollLeft, targetTopPosition - pageElement.offsetHeight);
                                    div.style.top = targetTopPosition - div.__Height - div.offsetHeight + 'px';
                                }
                            } else {
                                //装不下
                                //缩小显示
                                //滚动视图直到让下拉框展示全
                                if (currentInput && (currentInput.InnerEditStyle == "DateTime" || currentInput.InnerEditStyle == "Date" || currentInput.InnerEditStyle == "Time")) {
                                    //判断pageElement的高度是否超过输入域和弹窗高度和
                                    var clientHeight = div.clientHeight;
                                    //判断放在输入域下是否能展示全
                                    if (pageElement.scrollHeight - targetTopPosition >= clientHeight) {
                                        //计算差值
                                        var scrollHeight = targetTopPosition - div.__Height;
                                        pageElement.scrollTo(pageElement.scrollLeft, scrollHeight);
                                        //div.style.top = (targetTopPosition + 3) - (div.offsetHeight *  ((1 - scale) * 0.5)) + 'px';
                                        div.style.top = (targetTopPosition + 3) + 'px';
                                    } else {
                                        pageElement.scrollTo(pageElement.scrollLeft, targetTopPosition - pageElement.offsetHeight);
                                        //div.style.top = (targetTopPosition - div.__Height - clientHeight) - (div.offsetHeight * ((1 - scale) * 0.5)) + 'px';
                                        div.style.top = (targetTopPosition - div.__Height - div.clientHeight) + 'px';
                                    }
                                } else {
                                    //如果两边都放不下,则判断哪边间距大放在哪边
                                    if (downGap > upGap) {
                                        div.style.top = targetTopPosition + "px";
                                    } else {
                                        div.style.top = upGap - div.offsetHeight + pageElement.scrollTop + 'px';
                                        div.setAttribute('dc_up', 'true');//[DUWRITER5_0-3273]用于记录是否是从上方弹出的
                                    }
                                }
                            }
                        }
                    }
                }
                // 处理下拉弹框的左侧位置偏移，避免下拉弹框展示不全和导致横向滚动条变化的问题【DUWRITER5_0-3635】
                /** 下拉弹框的左侧位置偏移 */
                var targetLeftPosition = div.__PageElement.offsetLeft + div.__Left;
                /** 编辑器编辑模式div的展示宽度，不包括滚动条宽度 */
                var pageElementWidth = pageElement.clientWidth;
                /** 下拉弹框的宽度 */
                var divWidth = div.offsetWidth;
                /** 下拉弹框的最小宽度 */
                var divMinWidth = parseFloat(div.style.minWidth) || 0;
                // 判断下拉弹框是否可以完全展示
                if (divWidth <= pageElementWidth || (divMinWidth > 0 && divMinWidth <= pageElementWidth)) {
                    if (targetLeftPosition < pageElement.scrollLeft) {
                        // 左超出
                        targetLeftPosition = pageElement.scrollLeft;
                    }
                    if ((targetLeftPosition + divWidth) > (pageElement.scrollLeft + pageElementWidth)) {
                        // 右超出
                        if (divMinWidth > 0 && (targetLeftPosition + divMinWidth) > (pageElement.scrollLeft + pageElementWidth)) {
                            // 宽度小于最小宽度，则直接显示最小宽度
                            targetLeftPosition = pageElement.scrollLeft + pageElementWidth - divMinWidth;
                        } else {
                            targetLeftPosition = pageElement.scrollLeft + pageElementWidth - divWidth;
                        }
                    }
                }
                div.style.left = targetLeftPosition + "px";
                if (rootElement && rootElement.DropdownControlPress && typeof (rootElement.DropdownControlPress) == "function") {
                    rootElement.DropdownControlPress.call(rootElement, currentInput, div);
                }
            };
            div.CloseDropdown = function (need, currentInput) {
                var div = this,
                    rootElement = this;
                while (rootElement.getAttribute('dctype') != 'WriterControlForWASM') {
                    rootElement = rootElement.parentNode;
                }
                // 关闭时间选择界面
                if (div.thisApi && div.thisApi.theTool && typeof (div.thisApi.theTool.hidePanel) == "function") {
                    div.thisApi.theTool.hidePanel();
                    div.thisApi = null;
                }
                //var hasMultiSelectControl = div.querySelector('#MultiSelectControl');
                //if ((need || hasMultiSelectControl) && typeof rootElement.EventAfterFieldContentEdit == 'function') {
                //    var targetInput = rootElement.CurrentInputField();
                //    if (targetInput) {
                //        var inputAttr = rootElement.GetElementProperties(targetInput);
                //        WriterControl_Event.RaiseControlEvent(rootElement, 'EventAfterFieldContentEdit', inputAttr);
                //    }
                //}
                //清空内部的数据
                div.innerHTML = '';
                //div.style.removeProperty('max-height');
                //div.style.removeProperty('min-width');
                //div.style.removeProperty('overflow-y');
                div.style.removeProperty('height');
                //div.style.removeProperty('width');
                div.style.display = "none";
                div.style.maxHeight = "250px";
                //if (!('ontouchstart' in document.documentElement)) {
                //    //判断是否让输入域只读
                //    var txtEdit = rootElement.querySelector('#txtEdit20221213');
                //    //判断是否为只读状态禁止输入法
                //    var isReadonly = rootElement.Readonly;
                //    if (txtEdit && isReadonly && txtEdit.getAttribute('type') != 'password') {
                //        txtEdit.setAttribute('type', 'password');
                //    } else if (!isReadonly && txtEdit.getAttribute('type') != 'text') {
                //        txtEdit.setAttribute('type', 'text');
                //    }
                //}
                // 触发 EventCloseDropdown 事件
                var fc = div.EventCloseDropdown;
                div.EventCloseDropdown = null;
                if (typeof (fc) == "function") {
                    fc.call(div, div);
                }
                //zhangbin 20231103 存在关闭输入域下拉框后光标位置不在输入域内而在其他位置，此处判断一下并移动位置
                if (currentInput) {
                    var newCurrentInput = rootElement.GetElementProperties(rootElement.CurrentInputField());
                    if (currentInput.NativeHandle != newCurrentInput.NativeHandle) {
                        rootElement.FocusAdjacent("beforeEnd", currentInput);
                    }
                }
            };
            div.onmousedown = function (e) {
                e.cancelBubble = true;
            };
            // 修复下拉弹框中滚动不了的问题【DUWRITER5_0-3644】
            if (!('ontouchstart' in rootElement.ownerDocument.documentElement)) {
                div.addEventListener("mousewheel", function (e) {
                    try {
                        let scrollElement = e.srcElement || e.target;
                        while (scrollElement && scrollElement !== this && (scrollElement.scrollHeight <= (scrollElement.innerHeight || scrollElement.clientHeight) || window.getComputedStyle(scrollElement).getPropertyValue("overflow-y") == "hidden")) {
                            scrollElement = scrollElement.parentElement;
                        }
                        let delta = (e.wheelDelta && (e.wheelDelta > 0 ? 1 : -1)) || // chrome & ie
                            (e.detail && (e.detail > 0 ? -1 : 1)); // firefox
                        if (scrollElement) {
                            const scrollTop = scrollElement.scrollTop;
                            const scrollHeight = scrollElement.scrollHeight;
                            const height = scrollElement.clientHeight;
                            if ((delta > 0 && scrollTop <= delta) || (delta < 0 && scrollHeight - height - scrollTop <= -1 * delta)) {
                                scrollElement.scrollTop = delta > 0 ? 0 : scrollHeight;
                                e.stopPropagation();
                                e.preventDefault();
                            }
                        }
                    } catch (error) {
                        console.error("Error in mousewheel event handler:", error);
                    }
                });
            }
        } else {
            // 清空已有内容
            while (div.firstChild != null) {
                div.removeChild(div.firstChild);
            }
        }
        div.checkPageContainerID = containerID;
        div.__PageElement = pageElement;
        div.__Left = intLeft;
        div.__Top = intTop;
        div.__Height = intHeight;
        if (div.parentNode != pageElement.parentNode) {
            pageElement.parentNode.appendChild(div);
        }

        //[DUWRITER5_0-3136] 20240715 lxy 增加支持用户自定义下拉框的宽度
        //获取放大倍数
        var zoomRate = rootElement.GetZoomRate();

        //用户自定义下拉框宽度：DropDownListWidth
        var DropDownListWidth = rootElement.getAttribute("DropDownListWidth");
        DropDownListWidth = (DropDownListWidth && parseInt(DropDownListWidth)) || null;
        //当下拉为自定义宽度并且为单列展示时 宽度
        if (DropDownListWidth) {
            div.style.width = (DropDownListWidth * zoomRate) + "px";
        } else {
            div.style.width = "auto";
        }

        //用户自定义下拉框最小宽度：
        var DropDownListMinWidth = rootElement.getAttribute("DropDownListMinWidth");
        DropDownListMinWidth = (DropDownListMinWidth && parseInt(DropDownListMinWidth)) || null;
        if (DropDownListMinWidth) {
            div.style.minWidth = (DropDownListMinWidth * zoomRate) + "px";
        } else {
            div.style.minWidth = "100px";
        }

        //用户自定义下拉框最大宽度：
        var DropDownListMaxWidth = rootElement.getAttribute("DropDownListMaxWidth");
        DropDownListMaxWidth = (DropDownListMaxWidth && parseInt(DropDownListMaxWidth)) || null;
        if (DropDownListMaxWidth) {
            div.style.maxWidth = (DropDownListMaxWidth * zoomRate) + "px";
        }

        return div;
    },

    /**
     * 对页面添加一些自定义的样式
     * */
    CustomCSSString: function (doc, newText) {
        if (doc == null) {
            doc = document;
        }
        var dcHead = doc.head;
        //判断是否存在此css样式
        var hasCustomCss = dcHead.querySelector('#DCCustomCSSString');
        var cssText = '';
        if (hasCustomCss != null) {
            cssText = hasCustomCss.innerText;
        } else {
            //创建样式
            hasCustomCss = doc.createElement('style');
            hasCustomCss.setAttribute('id', 'DCCustomCSSString');
            dcHead.appendChild(hasCustomCss);
        }
        cssText += newText;
        hasCustomCss.innerHTML = cssText;

    },
    /**
     * 显示快捷辅助录入下拉列表
     * @param {string} containerID 编辑器元素编号
     * @param {number} intPageIndex
     * @param {number} intLeft
     * @param {number} intTop
     * @param {number} intHeight
     * @param {string} strPretext 前置文本
     * @param {string} strContainerElementID 容器文档元素编号
     * @param {string} strContainerElementName 容器文档元素名称
     * @param {any} containerElementRef 容器元素引用对象
     * @param {number} stateVersion 状态版本号
     */
    ShowAssistListBoxControl: function (
        containerID,
        intPageIndex,
        intLeft,
        intTop,
        intHeight,
        strPreText,
        strContainerElementID,
        strContainerElementName,
        containerElementRef,
        stateVersion) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (WriterControl_Event.HasControlEvent(rootElement, "EventQueryAssistInputItems") == false) {
            // 没有查询列表项目的事件，则不处理。
            WriterControl_UI.CloseDropdownControl();
        }

        function funcInnerShowAssitListBoxControl(lstItems) {
            // 数据为空时不弹出列表
            if (lstItems == null || lstItems.length == 0) {
                WriterControl_UI.CloseDropdownControl();
                return;
            }
            // 查询到快捷辅助录入的列表信息,弹出列表
            var divContainer = WriterControl_UI.GetDropdownContainer(
                containerID,
                intPageIndex,
                intLeft,
                intTop,
                intHeight,
                true);
            if (divContainer == null) {
                return;
            }
            // 辅助录入项的点击事件
            var AssistListCallBack = function (strNewText) {
                WriterControl_UI.CloseDropdownControl();
                rootElement.__DCWriterReference.invokeMethod(
                    "ApplyAssistStringContent",
                    stateVersion,
                    strNewText);
            };
            var listBox = WriterControl_ListBoxControl.CreateListBoxControl(lstItems, AssistListCallBack, rootElement, divContainer);
            // 此处将 args4.Items中的内容填充到列表中，并调整大小
            divContainer.appendChild(listBox);
            // 给下拉框赋，用来表示是快捷辅助录入的下拉
            listBox.setAttribute("box-type", "AssistListBox");
            // 下拉框展示
            divContainer.ShowDropdown();
            // 下拉框滚动位置复原到顶部
            divContainer.scrollTop = 0;
            // 下拉框样式
            divContainer.style.minWidth = "200px";
            divContainer.style.height = "300px";
        };
        var editorStateVersion = rootElement.__DCWriterReference.invokeMethod("GetEditStateVersion");
        // 存在快捷辅助录入事件,则创建参数对象并触发事件
        var args4 = {
            PreWord: strPreText,
            Async: false,
            Cancel: false,
            Handled: false,
            Items: new Array(),
            ContainerElementID: strContainerElementID,
            ContainerElementName: strContainerElementName,
            GetItem: function (index) { return this.Items[index]; },
            AddItem: function (newItem) { this.Items.push(newItem); },
            /** 填充列表数据的操作执行完毕，可以弹出列表了*/
            Complete: function () {
                // 下拉列表数据填充完毕后的事件处理
                if (args4.Items != null
                    && args4.Items.length > 0
                    && editorStateVersion == rootElement.__DCWriterReference.invokeMethod("GetEditStateVersion")) {
                    // 编辑器的状态未改变，则弹出用户界面
                    funcInnerShowAssitListBoxControl(args4.Items);
                } else {
                    // 快捷辅助录入没有列表时，关闭下拉
                    WriterControl_UI.CloseDropdownControl();
                }
            }
        };
        // 使用定时器来进行隔离，避免JS错误导致WASM错误。
        window.setTimeout(function () {
            WriterControl_Event.RaiseControlEvent(rootElement, "EventQueryAssistInputItems", args4);
            if (args4.Async == true) {
                // 异步操作
                return;
            }
            else {
                if (args4.Items != null && args4.Items.length > 0) {
                    funcInnerShowAssitListBoxControl(args4.Items);
                } else {
                    WriterControl_UI.CloseDropdownControl();
                }
            }
        }, 1);
    },
    /**
     * 开始使用列表控件来编辑输入域的值
     * @param {string} containerID 编辑器编号
     * @param {number} intPageIndex 页码
     * @param {number} intLeft 文档元素坐标
     * @param {number} intTop 文档元素坐标
     * @param {number} intHeight 文档元素高度
     * @param {Array} listItems 下拉列表项目，为DCSoft.Writer.Data.ListItemCollection的JSON模式
     * @param {any} args 参数
     */
    BeginEditValueUseListBoxControl: function (
        containerID,
        intPageIndex,
        intLeft,
        intTop,
        intHeight,
        listItems,
        args) {
        var ctl = DCTools20221228.GetOwnerWriterControl(containerID);
        ctl.CheckDisposed();
        args.Handled = false;
        args.Cancel = false;
        if (listItems == null) {
            listItems = new Array();
        }


        //20240418 lxy DUWRITER5_0-2335 当下拉输入域没有设置动态下拉，但是有字典来源和静态下拉值时，不触发QueryListItems，默认展示自身静态下拉
        // 带有静态下拉
        var hasListItem = listItems && listItems.length;
        // 带有字典来源
        var hasListSourceName = typeof args.ListSourceName == "string" && args.ListSourceName.trim().length > 0;
        //获取自定义属性激活输入域时，是否触发QueryListItems事件
        var ListSourceNameTriggerQueryListItems = ctl.getAttribute('InputFileInnerListSourceNameTriggerQueryListItems');
        ListSourceNameTriggerQueryListItems = ['true', true].indexOf(ListSourceNameTriggerQueryListItems) !== -1;
        //如果触发QueryListItems事件，则不需要展示已有数据
        if (hasListSourceName && Boolean(hasListItem) && ListSourceNameTriggerQueryListItems) {
            args.Items = [];
        } else {
            args.Items = listItems;
        }
        var isDropDownTableListData = false;
        args.AddTableListData = function (tableData = null) {
            if (tableData) {

                WriterControl_QueryListTableControl.InitDropDownInputTableList(containerID, tableData, intPageIndex, intLeft, intTop, intHeight);
                isDropDownTableListData = true;
                return false;
            }
        };

        args.AddResultItem = function (item) {
            args.Items.push(item);
        };
        args.AddResultItemByTextValue = function (strText, strValue) {
            args.Items.push({ Text: strText, Value: strValue });
        };
        args.AddResultItemByTextValueTextInList = function (strText, strValue, strTextInList) {
            args.Items.push({ Text: strText, Value: strValue, TextInList: strTextInList });
        };
        args.WriterControl = ctl;

        //添加判断,在此处处理是否为动态下拉
        /** 当前输入域 */
        // DUWRITER5_0-1514，袁永福 2023-12-25 新增Get_JS_BeginEditValueUseListBoxControl_Field()用于精确的获取关联的输入域元素
        var targetInput = ctl.__DCWriterReference.invokeMethod("Get_JS_BeginEditValueUseListBoxControl_Field");//.CurrentInputField();
        /** 当前输入域的属性对象 */
        var targetInputProps = ctl.GetElementProperties(targetInput);
        args.ElementAttributes = targetInputProps.Attributes;

        function InnerShowListBoxControl() {
            var divContainer = WriterControl_UI.GetDropdownContainer(containerID, intPageIndex, intLeft, intTop, intHeight, true);
            if (divContainer == null) {
                return;
            }

            var callBack = function (strNewText, strNewInnerValue, rootElement) {
                if (strNewInnerValue != args.OldValue || strNewText != args.OldText) {
                    var ctlRef = DCTools20221228.GetDCWriterReference(containerID);
                    var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
                    if (ctlRef != null) {
                        rootElement.ApplyCurrentEditorCallBack(strNewText, strNewInnerValue, null, containerID);
                        //用于省市县级联输入域的内容清空
                        if (targetInputProps.DynamicListItems) {
                            if (targetInputProps.Attributes && targetInputProps.Attributes.childElement) {
                                childEleArr(targetInputProps.Attributes.childElement);
                            }
                        }
                    }
                } else {
                    //让输入域获取焦点
                    rootElement.FocusElement(targetInputProps.NativeHandle);
                }
                DCTools20221228.DisposeInstance(targetInputProps.NativeHandle);
                divContainer.CloseDropdown('true', targetInputProps);
            };
            var childEleArr = function (id) {
                ctl.SetElementTextByID(id, '');
                var childEle = ctl.GetElementProperties(ctl.GetElementById(id));
                if (childEle && childEle.Attributes.childElement) {
                    childEleArr(childEle.Attributes.childElement);
                }
            };
            //不知道fieldSettings的作用.暂不处理
            //在此处左下拉开启前事件
            //if (typeof ctl.EventBeforeFieldContentEdit == "function") {
            //    WriterControl_Event.RaiseControlEvent(ctl, 'EventBeforeFieldContentEdit', targetInput);
            //    //在此处获取一次targetInput的新值
            //    if (targetInputProps.InnerText != args.OldText) {
            //        args.OldText = targetInputProps.InnerText;
            //    }
            //    if (targetInputProps.InnerValue != args.OldValue) {
            //        args.OldValue = targetInputProps.InnerValue;
            //    }
            //}

            //判断是否为多选,此处判断是否为多选在处理下拉是因为保证在页面能显示出东西
            if (targetInputProps != null && (targetInputProps.InnerMultiSelect === true || targetInputProps.InnerMultiSelect == 'true')) {
                //为了多选下拉，此处处理
                // ctl.CurrentMultiSelect = targetInput;
                WriterControl_ListBoxControl.CreateMultiSelectControl(args.Items, ctl, divContainer, args.OldText, args.OldValue, targetInputProps, args);
            } else {
                WriterControl_ListBoxControl.CreateListBoxControl(args.Items, callBack, ctl, divContainer, args.OldText, args.OldValue, targetInputProps, args);
            }
            // 需要特殊样式
            divContainer.style.display = "flex";
            divContainer.style.flexDirection = "column";
        };

        //20240418 lxy DUWRITER5_0-2335 当下拉输入域没有设置动态下拉，但是有字典来源和静态下拉值时，不触发QueryListItems，默认展示自身静态下拉
        //满足一下三个条件其中一个，即可触发QueryListItems事件：
        // 1.动态下拉  
        // 2.带有字典来源并且静态下拉没有值  
        // 3.带有字典来源并且静态下拉有值并设置了InputFileInnerListSourceNameTriggerQueryListItems为true
        if (args.DynamicListItems == true || (hasListSourceName && Boolean(hasListItem) === false) || (hasListSourceName && Boolean(hasListItem) && ListSourceNameTriggerQueryListItems)) {
            // 用户是动态加载下拉列表的，则需要触发事件
            var editorStateVersion = ctl.__DCWriterReference.invokeMethod("GetEditStateVersion");
            args.Completed = function () {
                // 下拉列表数据填充完毕后的事件处理
                if (editorStateVersion == ctl.__DCWriterReference.invokeMethod("GetEditStateVersion")) {
                    // 编辑器的状态未改变，则弹出用户界面
                    InnerShowListBoxControl();
                }
            };
            // 触发编辑器控件的QueryListItems事件，如果是以异步方式加载数据，记得完成后调用args.Completed()
            //var oldLength = args.Items.length;
            args.Async = false;
            args.Cancel = false;
            // 使用定时器来进行隔离，避免JS错误导致WASM错误。
            window.setTimeout(function () {
                //触发动态下拉
                WriterControl_Event.InnerRaiseEvent(ctl, "QueryListItems", args);
                //触发自定义下拉
                var divContainer = WriterControl_UI.GetDropdownContainer(containerID, intPageIndex, intLeft, intTop, intHeight, true);
                if (divContainer) {
                    // 判断是否存在自定义下拉事件
                    if (ctl.EventDropdownEditor && typeof ctl.EventDropdownEditor == 'function') {
                        var dropDownResult = ctl.EventDropdownEditor(divContainer);
                        if (dropDownResult) {
                            divContainer.innerHTML = null;
                        } else {
                            divContainer.ShowDropdown();
                            return;
                        }
                    }
                }

                if (args.Cancel == true) {
                    // 用户取消操作
                    return;
                }
                if (args.Async == true) {
                    // 用户指定为异步模式，则等待
                    return;
                } else if (args.Items != null) {
                    if (args.Items.length > 0) {
                        // 已经填充了列表内容，则立即显示出来
                        args.Completed = null;
                        InnerShowListBoxControl();
                    } else if (args.Items.length == 0) {
                        // 获取到当前输入域的本身的值
                        if (targetInputProps.ListItems != null) {
                            listItems = targetInputProps.ListItems;
                            if (!isDropDownTableListData) {
                                InnerShowListBoxControl();
                            }
                        }
                    }

                }
            }, 1);
        } else {

            // 静态下拉列表，直接弹出用户界面
            InnerShowListBoxControl();
        }
    },

    /**
     * 显示数字计算器界面
     * @param {string} containerID 编辑器编号
     * @param {number} intPageIndex 页码
     * @param {number} intLeft 文档元素在页面中的坐标
     * @param {number} intTop 文档元素在页面中的坐标
     * @param {number} intHeight 文档元素高度
     * @param {number} dblInputValue 当前数值
     */
    ShowCalculateControl: function (containerID, intPageIndex, intLeft, intTop, intHeight, dblInputValue) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        rootElement.CheckDisposed();
        var targetInput = rootElement.CurrentInputField();
        var inputAttr = rootElement.GetElementProperties(targetInput);
        var divContainer = WriterControl_UI.GetDropdownContainer(containerID, intPageIndex, intLeft, intTop, intHeight, true);
        if (divContainer == null) {
            return;
        }
        //在此处左下拉开启前事件
        //if (typeof rootElement.EventBeforeFieldContentEdit == "function") {
        //    WriterControl_Event.RaiseControlEvent(rootElement, 'EventBeforeFieldContentEdit', inputAttr);
        //    //在此处获取一次targetInput的新值
        //    if (inputAttr.InnerValue != dblInputValue) {
        //        dblInputValue = inputAttr.InnerValue;
        //    }
        //}
        if (isNaN(dblInputValue) || dblInputValue == null) {
            dblInputValue = 0;
        }
        // 往divContainer列表填内容，设置它的宽度高度
        //点击确认的回调
        var callBack = function (newInputValue) {
            // 用户确认操作后执行函数
            var ctlRef = DCTools20221228.GetDCWriterReference(containerID);
            var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
            if (ctlRef != null) {
                //使用此方法会导致EventBeforeFieldContentEdit触发顺序问题暂时修改
                rootElement.ApplyCurrentEditorCallBack(newInputValue, null, null, containerID);
            }
            divContainer.CloseDropdown('true');
        };
        //调用创建数字计算器的方法
        var calculateEle = WriterControl_CalculateControl.CreateCalculateControl(dblInputValue, callBack);
        divContainer.appendChild(calculateEle);
        divContainer.style.width = '220px';
        divContainer.ShowDropdown();
    },

    /**
     * 显示时间日期选择界面
     * @param {string} containerID 元素编号
     * @param {number} intPageIndex 页码
     * @param {number} intLeft 文档元素在页面中的坐标
     * @param {number} intTop 文档元素在页面中的坐标
     * @param {number} intHeight 文档元素高度
     * @param {Date} dtmInputValue 当前数值
     * @param {number} intStyle 显示样式，2=选择日期(不含时分秒)，3=选择精确到秒的时间日期，4=精确到分钟的时间日期，5=时间(不含年月日)
     */
    ShowDateTimeControl: function (containerID, intPageIndex, intLeft, intTop, intHeight, dtmInputValue, intStyle) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        rootElement.CheckDisposed();
        var targetInput = rootElement.CurrentInputField();
        var inputAttr = rootElement.GetElementProperties(targetInput);
        var divContainer = WriterControl_UI.GetDropdownContainer(containerID, intPageIndex, intLeft, intTop, intHeight, true);
        if (divContainer == null) {
            return;
        }
        //在此处左下拉开启前事件
        //if (typeof rootElement.EventBeforeFieldContentEdit == "function") {
        //    WriterControl_Event.RaiseControlEvent(rootElement, 'EventBeforeFieldContentEdit', inputAttr);
        //    //在此处获取一次targetInput的新值
        //    if (inputAttr.InnerValue != dtmInputValue) {
        //        dtmInputValue = inputAttr.InnerValue;
        //    }
        //}
        var callBack = function (newInputValue) {
            divContainer.CloseDropdown('true');
            if (divContainer.thisApi) {
                divContainer.thisApi = null;
            }
            var ctlRef = DCTools20221228.GetDCWriterReference(containerID);
            var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
            if (ctlRef != null) {
                newInputValue = newInputValue ? newInputValue.replace(/T/g, ' ') : '';
                var hasNumberDisplayedInTibetan = rootElement.getAttribute("numberdisplayedintibetan");
                var oldInputValue = null;
                if (typeof hasNumberDisplayedInTibetan == "string" && hasNumberDisplayedInTibetan.toLowerCase() == "true") {
                    //替换文本为藏文
                    var tibetanString = ["༠", "༡", "༢", "༣", "༤", "༥", "༦", "༧", "༨", "༩", "ལོ་", "ཟླ་བ།", "ཉི་མ།", "དུས", "སྐར་མ་", "སྐར་ཆ་"];
                    //分解字符串
                    var valueArr = newInputValue.split("");
                    var newValue = "";
                    for (var i = 0; i < valueArr.length; i++) {
                        var text = valueArr[i];
                        if (text == " ") {
                            newValue += text;
                        } else if (text == "年") {
                            newValue += tibetanString[10];
                        } else if (text == "月") {
                            newValue += tibetanString[11];
                        } else if (text == "日") {
                            newValue += tibetanString[12];
                        } else if (text == "时") {
                            newValue += tibetanString[13];
                        } else if (text == "分") {
                            newValue += tibetanString[14];
                        } else if (text == "秒") {
                            newValue += tibetanString[15];
                        } else {
                            var number = Number(text);
                            if (!isNaN(number)) {
                                newValue += tibetanString[number];
                            } else {
                                newValue += text;
                            }
                        }
                    }
                    oldInputValue = newInputValue;
                    newInputValue = newValue;
                }
                rootElement.ApplyCurrentEditorCallBack(newInputValue, null, oldInputValue, containerID);
                //if (newInputValue != '') {
                //    rootElement.ApplyCurrentEditorCallBack(newInputValue, null, oldInputValue, containerID);
                //    //rootElement.SetElementTextByID(targetInput, newInputValue);
                //} else {
                //    rootElement.SetElementTextByID(targetInput, '');
                //}
            }
        };
        // 修复时间下拉展示不全的问题【DUWRITER5_0-3442】
        divContainer.style.width = '230px';
        // divContainer.style.textAlign = 'left';
        switch (intStyle) {
            case 2:
                divContainer.style.maxHeight = '275px';
                divContainer.style.height = '275px';
                break;
            case 3:
                divContainer.style.maxHeight = "311px";
                divContainer.style.height = '311px';
                break;
            case 4:
                divContainer.style.maxHeight = "311px";
                divContainer.style.height = '311px';
                break;
            case 5:
                divContainer.style.maxHeight = "88px";
                divContainer.style.height = '88px';
                break;
            default:
                divContainer.style.maxHeight = "311px";
                divContainer.style.height = '311px';
                break;
        }
        // 往divContainer列表填内容，设置它的宽度高度
        var result = divContainer.ShowDropdown();
        if (!result) {
            WriterControl_DateTimeControl.CreateDateTimeControl(divContainer, dtmInputValue, rootElement, intStyle, callBack);
        }
        divContainer.style.display = 'none';
        // divContainer.style.removeProperty('text-align');
    },
    /**
     * 显示音频或者视频
     * @param {string} strContainerID 编辑器元素编号
     * @param {string} strElementID 文档元素编号
     * @param {string} strSource 音视频来源地址
     * @param {number} intPageIndex 页码
     * @param {number} intLeft 左端位置
     * @param {number} intTop 顶端位置
     * @param {number} intWidth 宽度
     * @param {number} intHeight 高度
     */
    ShowVideoElement: function (strContainerID, strElementID, strSource, intPageIndex, intLeft, intTop, intWidth, intHeight) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(strContainerID);
        var targetElement = rootElement.ownerDocument.getElementById(strElementID);

    },

    CreateHostControlElement: function (strContainerID, strElementID, intPageIndex, intLeft, intTop, intWidth, intHeight) {
        var pageElement = WriterControl_Paint.GetCanvasElementByPageIndex(strContainerID, intPageIndex);
        if (pageElement == null) {
            return null;
        }

    },

    /** 清空系统剪切板 */
    ClearClipboard: function () {
        //这里使用SetClipboardData方法并传入空值
        WriterControl_UI.SetClipboardData(['text/html', '']);
    },

    /**
     * 处理表格复制的问题
     * @param {string} data 粘贴的数据 
     * @param {any} rootElement 
     * @returns
     */
    PasteDataFromTableToTable: function (data, rootElement) {
        var hasAttr = rootElement.getAttribute("PasteTablePreserveFormat");
        if (typeof hasAttr == "string" && hasAttr.toLowerCase().trim() == "true") {
            return true;
        }
        if (data != null && data.length > 0) {
            var hasHtml = data.indexOf('text/html');
            if (hasHtml >= 0) {
                var htmlString = data[hasHtml + 1];
                var DCHTML = rootElement.ownerDocument.createElement('html');
                DCHTML.innerHTML = htmlString;
                //查找是否存在护理记录单XTextNewMedicalExpressionElement
                var hasMedicalExpression = DCHTML.querySelectorAll("[dctype=XTextNewMedicalExpressionElement],[dctype=XTextNewBarcodeElement],[dctype=XTextTDBarcodeElement]");
                if (hasMedicalExpression && hasMedicalExpression.length > 0) {
                    //清掉src防止报错
                    for (var i = 0; i < hasMedicalExpression.length; i++) {
                        hasMedicalExpression[i].src = "";
                    }
                }
                //拿到body
                var hasBody = DCHTML.querySelector('body');
                if (hasBody) {
                    var targetTable = null;
                    //找到子元素
                    if (hasBody.children != null) {
                        //判断是否存在表格,表格外层存在p标签在html解析时会被分割成上下两个，此处删除
                        var hasTables = hasBody.querySelectorAll('[dctype=XTextTableElement]');
                        for (var i = 0; i < hasTables.length; i++) {
                            var thisTable = hasTables[i];
                            //找到后一个元素
                            var nextP = thisTable.nextElementSibling;
                            if (nextP && nextP.nodeName == "P" && nextP.children && nextP.children.length == 0) {
                                nextP.remove();
                                var prevP = thisTable.previousElementSibling;
                                if (prevP && prevP.nodeName == "P" && prevP.children && prevP.children.length == 0) {
                                    prevP.remove();
                                }
                            }
                        }
                        if (hasBody.children.length == 1 && hasBody.children[0].nodeName == 'TABLE') {
                            targetTable = hasBody.children[0];
                        } else {
                            //找到表格元素并判断是否唯一且无兄弟元素且父元素的父元素为body
                            var hasTable = hasBody.querySelectorAll('table');
                            if (hasTable != null && hasTable.length == 1) {
                                hasTable = hasTable[0];
                                if (hasTable.nextElementSibling == null && hasTable.previousElementSibling == null) {
                                    targetTable = hasTable;
                                }
                            }
                        }
                    }
                    if (targetTable) {
                        //判断是否存在选中单元格的情况
                        //先拿到数据并拼接成二维数组
                        var dataArr = [];
                        //获取到所有的表格行
                        var allRow = targetTable.rows;
                        for (var row = 0; row < allRow.length; row++) {
                            var targetRow = allRow[row];
                            var rowArr = [];
                            dataArr.push(rowArr);
                            // 获取到所有的单元格
                            var allCell = targetRow.cells;
                            for (var cell = 0; cell < allCell.length; cell++) {
                                var targetCell = allCell[cell];
                                var text = targetCell.textContent;
                                text = text.replace(/ /g, "");
                                text = text.replace(/^\s*|\s*$/g, "");
                                rowArr.push(text);
                            }
                        }
                        if (dataArr.length > 0) {
                            var selectCell = rootElement.GetSelectTableAndCell();
                            //[DUWRITER5_0-4070] lxy 修复选中多个单元格复制后，粘贴进一个单元格，输入域变成纯文本的问题
                            // if (selectCell.length == 2) {
                            //     //循环所有的dataArr;
                            //     //找到行列最大值
                            //     var maxRow = selectCell[1].RowsCount, maxCol = selectCell[1].ColumnsCount, thisRow = selectCell[0].RowIndex, thisCol = selectCell[0].ColIndex;
                            //     for (var i = 0; i < dataArr.length; i++) {
                            //         //找到目标行
                            //         if (thisRow + i > maxRow) {
                            //             break;
                            //         }
                            //         for (var j = 0; j < dataArr[i].length; j++) {
                            //             if (thisCol + j > maxCol) {
                            //                 break;
                            //             }
                            //             // console.log("粘贴单元格：" + selectCell[0].CellNativeHandle + " ，内容：" + dataArr[i][j]);
                            //             rootElement.SetTableCellTextExtByHandle(selectCell[0].CellNativeHandle, dataArr[i][j]);
                            //         }
                            //     }
                            //     rootElement.RefreshInnerView();//粘贴后刷新视图，解决粘贴后表格内容不显示的问题
                            //     //[DUWRITER5_0-3781] 20241105 lxy 重置光标到粘贴的单元格结尾处
                            //     var lastCell = selectCell[selectCell.length - 1];
                            //     if (lastCell && lastCell.TableID !== null) {
                            //         rootElement.MoveToTableCellTag({
                            //             "tableid": lastCell.TableID,
                            //             "rowindex": lastCell.RowIndex,
                            //             "colindex": lastCell.ColIndex,
                            //             "tag": "CellEnd"//CellHome单元格内部前面、CellEnd单元格内部后面、PreCell前一个单元格、AfterCell后一个单元格、BeforeTable表格前、AfterTable表格后
                            //         });
                            //     } else if (lastCell && lastCell.CellNativeHandle !== null) {
                            //         rootElement.FocusElement(lastCell.CellNativeHandle);
                            //     }
                            //     return false;
                            // } else
                            if (selectCell.length > 2) {
                                //[DUWRITER5_0-3982]20211216 lxy 存在多个选中单元格的情况,防止将元素元素粘贴为纯文本的情况，要先走PasteDataToSelectCells
                                let regex = /<\?xml\s+[^\?]*\?>/;//用于匹配对应的xml内容
                                let pasteXml = data.find(item => item.match(regex));
                                if (pasteXml) {
                                    //如果返回值为true，则不再执行后续的粘贴逻辑
                                    let IsPasteDataToSelectCells = rootElement.__DCWriterReference.invokeMethod("PasteDataToSelectCells", pasteXml);
                                    if (IsPasteDataToSelectCells) {
                                        return false;
                                    }
                                }

                                //存在选中的情况
                                //获取表格
                                var thisRow = selectCell[0].RowIndex, thisCol = selectCell[0].ColIndex;
                                //var thisTable = selectCell[selectCell.length - 1];
                                for (var i = 0; i < selectCell.length; i++) {
                                    var setCell = selectCell[i];
                                    if (!setCell || setCell.RowIndex == null) {
                                        selectCell.splice(i, 1);
                                        i--;
                                        continue;
                                    }
                                    var setRowIndex = (setCell.RowIndex - thisRow) % dataArr.length;
                                    var setColIndex = (setCell.ColIndex - thisCol) % dataArr[setRowIndex].length;
                                    //[DUWRITER5_0-3781] 20241105 lxy 优化选中多个单元格复制粘贴卡顿的问题
                                    // console.time("SetTableCellTextExt");
                                    rootElement.SetTableCellTextExtByHandle(setCell.CellNativeHandle, dataArr[setRowIndex][setColIndex]);
                                    // console.timeEnd("SetTableCellTextExt");
                                    // rootElement.SetTableCellTextExt(CurrentTable, setCell.RowIndex, setCell.ColIndex, dataArr[setRowIndex][setColIndex]);

                                }
                                rootElement.RefreshInnerView();//粘贴后刷新视图，解决粘贴后表格内容不显示的问题

                                //[DUWRITER5_0-3781] 20241105 lxy 重置光标到粘贴的单元格结尾处
                                var lastCell = selectCell[selectCell.length - 1];
                                if (lastCell && lastCell.TableID !== null) {
                                    rootElement.MoveToTableCellTag({
                                        "tableid": lastCell.TableID,
                                        "rowindex": lastCell.RowIndex,
                                        "colindex": lastCell.ColIndex,
                                        "tag": "CellEnd"//CellHome单元格内部前面、CellEnd单元格内部后面、PreCell前一个单元格、AfterCell后一个单元格、BeforeTable表格前、AfterTable表格后
                                    });
                                } else if (lastCell && lastCell.CellNativeHandle !== null) {
                                    rootElement.FocusElement(lastCell.CellNativeHandle);
                                }

                                return false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    },

    /**
     * 粘贴时判断文本是否为word或者纯文本并修改
     * @param {array} datalist 需要判断的文本
     * @returns {array} 替换后的文本
     */
    PasteDateFromUTF16: function (datalist, rootElement) {
        //文本存在并为数组
        if (datalist && Array.isArray(datalist)) {
            //判断html
            var hasHTMLString = datalist.indexOf("text/html");
            if (hasHTMLString >= 0) {
                var xmlStringText = datalist[hasHTMLString + 1];
                //查找是否存在dcxmlshow
                var div = document.createElement("div");
                div.innerHTML = xmlStringText;
                //查找是否存在护理记录单XTextNewMedicalExpressionElement
                var hasMedicalExpression = div.querySelectorAll("[dctype=XTextNewMedicalExpressionElement],[dctype=XTextNewBarcodeElement],[dctype=XTextTDBarcodeElement]");
                if (hasMedicalExpression && hasMedicalExpression.length > 0) {
                    //清掉src防止报错
                    for (var i = 0; i < hasMedicalExpression.length; i++) {
                        hasMedicalExpression[i].src = "";
                    }
                }
                // 存在text/html时数据是纯文本的时候,设置成text/plain
                if (div.children.length == 0 && div.childNodes.length == 1) {
                    datalist[hasHTMLString] = "text/plain";
                    return datalist;
                }
                var hasDcxmlshow = div.querySelector("[dcxmlshow]");
                if (hasDcxmlshow) {
                    //找到是否存在DCWriterXML20240201
                    var hasXml = datalist.indexOf("DCWriterXML20240201");
                    if (hasXml >= 0) {
                        datalist[hasXml + 1] = hasDcxmlshow.getAttribute("dcxmlshow");
                    } else {
                        datalist.push("DCWriterXML20240201");
                        datalist.push(hasDcxmlshow.getAttribute("dcxmlshow"));
                    }
                    hasDcxmlshow.remove();
                    datalist[hasHTMLString + 1] = div.innerHTML;
                }
                var hasdcattrclipboarddata = div.querySelector("[dcattrclipboarddata]");
                if (hasdcattrclipboarddata) {
                    var thisArr = hasdcattrclipboarddata.getAttribute("dcattrclipboarddata");
                    thisArr = JSON.parse(thisArr);
                    //找到html
                    datalist = thisArr;
                }
                // var hasIpp = xmlStringText.indexOf(`ipp="nll"`);
                // if (hasIpp >= 0) {
                //     xmlStringText = xmlStringText.replace(/<div ipp="nll"|<div ipp="ll"/g, "<span");
                //     xmlStringText = xmlStringText.replace(/<\/div>/g, "</span>");
                //     datalist[hasHTMLString + 1] = xmlStringText;
                // }
                if (rootElement.DCSingleClearStyleFollowPrevious === true) {
                    var fontObj = rootElement.getFontObject();
                    if (!rootElement.__DCWriterReference.invokeMethod("HasSelection")) {
                        var select = rootElement.Selection;
                        rootElement.SelectContentByStartEndIndex(select.AbsStartIndex, select.AbsStartIndex);
                        fontObj = rootElement.getFontObject();
                        // var content = rootElement.CurrentElement("xtextcontainerelement");
                        // rootElement.FocusElement(content);
                        rootElement.MoveToPosition(select.AbsStartIndex);
                    }
                    var div = document.createElement("div");
                    div.innerHTML = xmlStringText;
                    var allSpan = div.querySelectorAll("*");
                    for (var j = 0; j < allSpan.length; j++) {
                        allSpan[j].style.cssText = `font-family:${fontObj.FontFamily};font-size:${fontObj.FontSize}pt;font-weight:${fontObj.Bold ? "bold" : "normal"};font-style:${fontObj.Italic ? "italic" : "normal"};text-decoration:${fontObj.Strikeout ? "line-through" : ""};`; //${fontObj.Underline ? "underline" : ""}
                    }

                    xmlStringText = div.innerHTML;
                    datalist[hasHTMLString + 1] = xmlStringText;
                    //rootElement.DCSingleClearStyleFollowPrevious = false;
                }
            } else {
                // 本身就是纯文本了，不需要特殊处理
                // if (datalist.length == 2 && datalist[0] == "text/plain") {
                //     //跟随前文样式
                //     if (rootElement.DCSingleClearStyleFollowPrevious === true) {
                //         var fontObj = rootElement.getFontObject();
                //         // if (!rootElement.__DCWriterReference.invokeMethod("HasSelection")) {
                //         //     var select = rootElement.Selection;
                //         //     rootElement.SelectContentByStartEndIndex(select.AbsStartIndex, select.AbsStartIndex);
                //         //     fontObj = rootElement.getFontObject();
                //         //     // var content = rootElement.CurrentElement("xtextcontainerelement");
                //         //     // rootElement.FocusElement(content);
                //         //     rootElement.MoveToPosition(select.AbsStartIndex);
                //         // }
                //         //合并一个html写入
                //         var span = document.createElement("span");
                //         span.innerText = datalist[1];
                //         span.style.cssText = `font-family:${fontObj.FontFamily};font-size:${fontObj.FontSize}pt;font-weight:${fontObj.Bold ? "bold" : "normal"};font-style:${fontObj.Italic ? "italic" : "normal"};text-decoration:${fontObj.Strikeout ? "line-through" : ""};`
                //         datalist.push("text/html");
                //         datalist.push(span.outerHTML);
                //         //rootElement.DCSingleClearStyleFollowPrevious = false;
                //     }
                // }
            }
            //自此处处理不可删除不可编辑
            var hasXmlString = datalist.indexOf("DCWriterXML20240201");
            if (hasXmlString >= 0) {
                var xmlStringText = datalist[hasXmlString + 1];
                var newXmlStringText = xmlStringText.replace(/<Deleteable>false<\/Deleteable>|<UserEditable>false<\/UserEditable>/g, "");
                datalist[hasXmlString + 1] = newXmlStringText;
                //在此处清除样式
                if (rootElement.DCSingleClearStyleFollowPrevious === true) {
                    //找打styles标签删除
                    var stylesStartIndex = newXmlStringText.indexOf("<ContentStyles>");
                    var stylesEndIndex = newXmlStringText.indexOf("</ContentStyles>");
                    if (stylesStartIndex >= 0 && stylesEndIndex >= 0) {
                        var fontObj = rootElement.getFontObject();
                        if (!rootElement.__DCWriterReference.invokeMethod("HasSelection")) {
                            var select = rootElement.Selection;
                            rootElement.SelectContentByStartEndIndex(select.AbsStartIndex, select.AbsStartIndex);
                            fontObj = rootElement.getFontObject();
                            // var content = rootElement.CurrentElement("xtextcontainerelement");
                            // rootElement.FocusElement(content);
                            rootElement.MoveToPosition(select.AbsStartIndex);
                        }
                        //替换文本
                        var newContentStyles = `<ContentStyles>
                                                    <Default xsi:type="DocumentContentStyle">
                                                        <FontName>${fontObj.FontFamily}</FontName>
                                                        <FontSize>${fontObj.FontSize}</FontSize>
                                                        <Bold>${fontObj.Bold}</Bold>
                                                        <Italic>${fontObj.Italic}</Italic>
                                                        <Underline>${fontObj.Underline}</Underline>
                                                        <Strikeout>${fontObj.Strikeout}</Strikeout>
                                                    </Default>
                                                </ContentStyles>`;
                        newXmlStringText = newXmlStringText.substring(0, stylesStartIndex) + newContentStyles + newXmlStringText.substring(stylesEndIndex + 16);
                    }
                    datalist[hasXmlString + 1] = newXmlStringText;
                }
                if (newXmlStringText.length != xmlStringText.length || rootElement.DCClearStyleFollowPrevious === true) {
                    //找到html和rtf删除
                    var rtfIndex = datalist.indexOf("text/rtf");
                    var htmlIndex = datalist.indexOf("text/html");
                    if (rtfIndex >= 0) {
                        datalist[rtfIndex] = null;
                        datalist[rtfIndex + 1] = null;
                    }
                    if (htmlIndex >= 0) {
                        datalist[htmlIndex] = null;
                        datalist[htmlIndex + 1] = null;
                    }
                }
            }
            rootElement.DCSingleClearStyleFollowPrevious = false;
            rootElement.PasteNoUserText = false;
            //自此出对文档进行处理,获取
            var getAcceptDataFormats = rootElement.DocumentOptions.BehaviorOptions.AcceptDataFormats;
            if (getAcceptDataFormats) {
                //分割数据
                var allFormats = getAcceptDataFormats.split(',');
                allFormats.forEach((item, index) => {
                    allFormats[index] = item ? item.toLowerCase().trim() : item;
                });
                if (allFormats.indexOf("none") >= 0) {
                    datalist = [];
                    return datalist;
                } else if (allFormats.indexOf("all") < 0) {
                    //循环数组
                    for (var i = datalist.length - 2; i >= 0; i -= 2) {
                        var thisType = datalist[i];
                        if (thisType == "text/plain") {
                            thisType = "text";
                        } else if (thisType == "text/rtf") {
                            thisType = "rtf";
                        } else if (thisType == "text/html") {
                            thisType = "html";
                        } else if (thisType == "DCWriterXML20240201") {
                            thisType = "xml";
                        }
                        if (allFormats.indexOf(thisType) < 0) {
                            datalist.splice(i, 2);
                        }
                    }
                }
            }
            //假设为纯文本
            if (datalist.length == 2 && datalist[0] == 'text/plain') {
                var newTXT = '';
                //对生僻字进行判断
                for (var char = 0; char < datalist[1].length; char++) {
                    newTXT += DCTools20221228.changeUseUTF16(datalist[1][char]);
                }
                datalist[1] = newTXT;
                return datalist;
            } else {
                //判断html是否能找到为word; schemas-microsoft-com:office:word
                var htmlIndex = datalist.indexOf('text/html');
                //存在html
                if (htmlIndex >= 0) {
                    var htmlString = datalist[htmlIndex + 1];
                    var isWord = htmlString.indexOf('schemas-microsoft-com:office:word');
                    //存在
                    if (isWord >= 0) {
                        var newdatalist = [];
                        //循环数据并重写datalist
                        for (var dataType = 0; dataType < datalist.length; dataType += 2) {
                            if (datalist[dataType] == 'text/plain') {
                                var newTXT = '';
                                //对生僻字进行判断
                                for (var char = 0; char < datalist[dataType + 1].length; char++) {
                                    newTXT += DCTools20221228.changeUseUTF16(datalist[dataType + 1][char]);
                                }
                                newdatalist.push('text/plain');
                                newdatalist.push(newTXT);
                            } else if (datalist[dataType] == 'text/html') {
                                //获取正确的字符串
                                var startIndex = datalist[dataType + 1].indexOf('\x3C!--StartFragment-->');
                                var lastIndex = datalist[dataType + 1].indexOf('\x3C!--EndFragment-->');
                                if (startIndex >= 0 && lastIndex >= 0) {
                                    var needChangeString = datalist[dataType + 1].substring((startIndex + 20), lastIndex);

                                    var newTXT = '';
                                    //对生僻字进行判断
                                    for (var char = 0; char < needChangeString.length; char++) {
                                        newTXT += DCTools20221228.changeUseUTF16(needChangeString[char]);
                                    }

                                    // //替换p标签
                                    if (newTXT.indexOf("<p class=MsoNormal") >= 0) {
                                        //newTXT = newTXT.replace(/<p class=MsoNormal>|<p class=MsoNormal >/g, "");
                                        var lasIndex = newTXT.lastIndexOf("<p class=MsoNormal");
                                        //找到第一个>
                                        var firstSymbol = newTXT.indexOf(">", lasIndex);
                                        //存在结束标签
                                        if (firstSymbol >= 0) {
                                            var deleteLength = firstSymbol - lasIndex + 1;
                                            //if (lasIndex < 0) {
                                            //    lasIndex = newTXT.lastIndexOf("<p class=MsoNormal >");
                                            //    deleteLength = "<p class=MsoNormal >".length;
                                            //}
                                            if (lasIndex >= 0) {
                                                newTXT = newTXT.substring(0, lasIndex) + newTXT.substring(lasIndex + deleteLength);
                                            }
                                            lasIndex = newTXT.lastIndexOf("</p>");
                                            if (lasIndex >= 0) {
                                                newTXT = newTXT.substring(0, lasIndex) + newTXT.substring(lasIndex + 4);
                                            }
                                        }
                                        //newTXT = newTXT.replace(/<\/p>/g, "<br/>");
                                    }
                                    newTXT = datalist[dataType + 1].substring(0, startIndex + 20) + newTXT + datalist[dataType + 1].substring(lastIndex);
                                    newTXT = newTXT.replace(/\r\n$|\n$|\r$/, "");

                                    newdatalist.push('text/html');
                                    newdatalist.push(newTXT);
                                } else {
                                    newdatalist.push('text/html');
                                    newdatalist.push(datalist[dataType + 1]);
                                }
                            }
                        }
                        return newdatalist;
                    } else {
                        // //替换p标签
                        if (htmlString.indexOf(`<p class="MsoNormal"`) >= 0) {
                            //htmlString = htmlString.replace(/<p class="MsoNormal">/g, "");
                            //var lasIndex = htmlString.lastIndexOf("</p>");
                            //if (lasIndex >= 0) {
                            //    htmlString = htmlString.substring(0, lasIndex) + htmlString.substring(lasIndex + 4)
                            //}
                            //htmlString = htmlString.replace(/<\/p>/g, "<br/>");
                            var lasIndex = htmlString.lastIndexOf(`<p class="MsoNormal"`);
                            //找到第一个>
                            var firstSymbol = htmlString.indexOf(">", lasIndex);
                            //存在结束标签
                            if (firstSymbol >= 0) {
                                var deleteLength = firstSymbol - lasIndex + 1;
                                //if (lasIndex < 0) {
                                //    lasIndex = newTXT.lastIndexOf("<p class=MsoNormal >");
                                //    deleteLength = "<p class=MsoNormal >".length;
                                //}
                                if (lasIndex >= 0) {
                                    htmlString = htmlString.substring(0, lasIndex) + htmlString.substring(lasIndex + deleteLength);
                                }
                                lasIndex = htmlString.lastIndexOf("</p>");
                                if (lasIndex >= 0) {
                                    htmlString = htmlString.substring(0, lasIndex) + htmlString.substring(lasIndex + 4);
                                }
                            }
                            datalist[htmlIndex + 1] = htmlString;
                        }
                        return datalist;
                    }
                } else {
                    return datalist;
                }
            }
        } else {
            return datalist;
        }
    },

    /**
     * 粘贴时判断文本是否为编辑器生成的打印预览的HTML并修改
     * @param {array} datalist 需要判断的文本
     * @returns {array} 替换后的文本
     */
    PasteDataFroDcWriterHtml: function (dataList, rootElement) {
        if (!dataList && !Array.isArray(dataList)) {
            return dataList;
        }
        // 如果存在text/html,并且html包括ipp='nll' ipp='ll',需要特殊处理
        var htmlIndex = dataList.indexOf('text/html');
        if (htmlIndex >= 0) {
            var cleanHTML = dataList[htmlIndex + 1];
            const contentContainer = document.createElement('div');
            contentContainer.innerHTML = cleanHTML;
            //查找是否存在护理记录单XTextNewMedicalExpressionElement
            var hasMedicalExpression = contentContainer.querySelectorAll("[dctype=XTextNewMedicalExpressionElement],[dctype=XTextNewBarcodeElement],[dctype=XTextTDBarcodeElement]");
            if (hasMedicalExpression && hasMedicalExpression.length > 0) {
                //清掉src防止报错
                for (var i = 0; i < hasMedicalExpression.length; i++) {
                    hasMedicalExpression[i].src = "";
                }
            }
            var elementsWithIppAttribute = contentContainer.querySelectorAll('div[ipp]');
            if (elementsWithIppAttribute.length > 0 && elementsWithIppAttribute.length == contentContainer.childNodes.length) {
                const fragment = document.createDocumentFragment();
                elementsWithIppAttribute.forEach(function (divElement, index) {
                    const spanElement = document.createElement('span');
                    // 使用 Array.forEach 来遍历属性，避免使用相同的索引变量 i
                    Array.from(divElement.attributes).forEach((attr) => {
                        spanElement.setAttribute(attr.name, attr.value);
                    });
                    while (divElement.firstChild) {
                        spanElement.appendChild(divElement.firstChild);
                    }
                    divElement.parentNode.insertBefore(spanElement, divElement);
                    divElement.parentNode.removeChild(divElement);
                    fragment.appendChild(spanElement);
                    if (spanElement.getAttribute('ipp') === 'll' && index < elementsWithIppAttribute.length - 1) {
                        // 表示有回车
                        // 这个是保证text/html获取的是换行
                        const brElement = document.createElement('br');
                        // brElement.setAttribute('dcpf', '1');
                        // 这个是保证text/plain获取的是换行
                        const CarriageReturnNode = document.createTextNode('\r\n');
                        fragment.appendChild(brElement);
                        fragment.appendChild(CarriageReturnNode);
                    }
                });
                // 清除最后一个换行
                if (fragment.lastChild && fragment.lastChild.nodeType === 3 && fragment.lastChild.textContent == "\r\n") {
                    fragment.removeChild(fragment.lastChild);
                }
                contentContainer.appendChild(fragment);
                // 更新 text/plain 的逻辑，保持不变
                var textIndex = dataList.indexOf('text/plain');
                if (textIndex >= 0) {
                    dataList[textIndex + 1] = contentContainer.innerText;
                }
                dataList[htmlIndex + 1] = contentContainer.innerHTML;
                // console.log("🚀 ~ dataList:", dataList);
            }
        }
        return dataList;
    },

    /**获得系统剪切板中所有的内容
     * @returns {Array} 内容组成的数组，偶数位为数据格式的名称，奇数位是数据内容
     * */
    GetClipboardData: async function (e, rootElement) {
        if (rootElement == null || rootElement.__DCDisposed == true) {
            return null;
        }
        var oldFontName = null;
        //var rootElement = WriterControl_UI.GetCurrentWriterControl();
        // 触发EventBeforePaste
        function triggerEventBeforePaste(rootElement, dataList, onlyAttr) {
            // 添加粘贴拦截事件【EventBeforePaste】
            if (!rootElement || !rootElement.EventBeforePaste) {
                return;
            }
            if (typeof (rootElement.EventBeforePaste) != "function") {
                return;
            }
            if (onlyAttr) {
                rootElement.EventBeforePaste(dataList, e);
            } else {
                //在此处记录DCSingleClearStyleFollowPrevious的值
                var oldPreviousValue = rootElement.DCSingleClearStyleFollowPrevious;
                //表示进入了Paste事件
                var result = rootElement.EventBeforePaste(dataList, e);
                if (rootElement.PasteNoUserText === true) {
                    rootElement.isPasteAsText = false;
                    rootElement.DCSingleClearStyleFollowPrevious = oldPreviousValue;
                }
                if (result === false) {
                    return false;
                }
            }
        }
        //zhangbin 20230424
        var clipboardData = '';
        var dataList = [];
        var hasDCAttr = false;
        var isOfficeWord = false;
        var isOfficeExecl = false;
        //判断是否为粘贴事件触发
        if (e != null && e.clipboardData != null) {
            for (const type of e.clipboardData.types) {
                //根据剪切板中的数据类型解析数据
                clipboardData = e.clipboardData.getData(type);

                if (clipboardData == null || clipboardData.length == 0) {
                    continue;
                }
                if (type == 'text/html') {
                    //判断是否为word
                    isOfficeWord = clipboardData.indexOf('schemas-microsoft-com:office:word') >= 0 ? true : false;
                    isOfficeExecl = clipboardData.indexOf('schemas-microsoft-com:office:excel') >= 0 ? true : false;
                    var DCAttrHTML = rootElement.ownerDocument.createElement('html');
                    DCAttrHTML.innerHTML = clipboardData;
                    var hasBody = DCAttrHTML.querySelector('body');
                    if (hasBody) {
                        var thisDiv = hasBody.querySelector('[DCAttrClipboardData]');
                        if (thisDiv) {
                            hasDCAttr = true;
                            try {
                                var DCAttrClipboardData = thisDiv.getAttribute('DCAttrClipboardData');
                                thisDiv.remove();
                                if (DCAttrClipboardData) {
                                    dataList = JSON.parse(DCAttrClipboardData);
                                } else {
                                    throw new Error('');
                                }
                                //在此处判断是否存在image元素
                                if (Array.isArray(dataList)) {
                                    for (var list = 0; list < dataList.length; list += 2) {
                                        if (dataList[list].indexOf('image/') == 0) {
                                            dataList = [dataList[list], dataList[list + 1]];
                                            break;
                                        }
                                    }
                                }
                            } catch (err) {
                                dataList.push(type);
                                dataList.push(clipboardData);
                                continue;
                            }
                            break;
                        }

                    }
                    //在ie下复制内容会出现多余的head和table标签,此处格式化文档并重新写入数据
                    //最先判断是否为ie下,暂时只对ie进行操作
                    if (clipboardData.indexOf('<HTML>') == 0) {
                        //查找字符串
                        var startIndex = clipboardData.indexOf('\x3C!--StartFragment-->');
                        var lastIndex = clipboardData.indexOf('\x3C!--EndFragment-->');
                        clipboardData = '<html><body>' + clipboardData.substring((startIndex + 20), lastIndex) + '</body></html >';
                    }
                }
                dataList.push(type);
                dataList.push(clipboardData);
            }
            if (!rootElement.isPasteAsText) {
                //存在文件信息
                if (!hasDCAttr && e.clipboardData.files.length > 0) {
                    dataList = [];
                    Promise.all(function* () {
                        for (const file of e.clipboardData.files) {
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
                        var imageData = [];
                        results.forEach(data => {
                            if (data.type.indexOf('image/') == 0) {
                                imageData.push(data.type);
                                imageData.push(data.result);
                            } else {
                                dataList.push(data.type);
                                dataList.push(data.result);
                            }
                        });
                        if (triggerEventBeforePaste(rootElement, dataList) == false) {
                            return false;
                        }
                        //此处不再使用粘贴接口使用插入图片的接口,因为粘贴出来的图片不保存
                        if (imageData.length > 0 && !isOfficeExecl) {
                            var imgArr = new Array();
                            for (var i = 0; i < imageData.length; i += 2) {
                                var options = {
                                    SaveContentInFile: true,
                                    Src: imageData[i + 1].split(',')[1]
                                };
                                imgArr.push(options);
                            }
                            //修改DocumentOptions.BehaviorOptions.AcceptDataFormats="Text"
                            var getAcceptDataFormats = rootElement.DocumentOptions.BehaviorOptions.AcceptDataFormats;
                            if (getAcceptDataFormats) {
                                //分割数据
                                var allFormats = getAcceptDataFormats.split(',');
                                allFormats.forEach((item, index) => {
                                    allFormats[index] = item ? item.toLowerCase().trim() : item;
                                });
                                if (allFormats.indexOf("all") >= 0 || allFormats.indexOf("image") >= 0) {
                                    rootElement.InsertImageByJSONText(imgArr);
                                }
                            }
                        } else {
                            var needPaste = WriterControl_UI.PasteDataFromTableToTable(dataList, rootElement);
                            if (needPaste) {
                                dataList = WriterControl_UI.PasteDateFromUTF16(dataList, rootElement);
                                //暂时删除掉rtf
                                if (Array.isArray(dataList)) {
                                    var hasRft = dataList.indexOf('text/rtf');
                                    if (hasRft >= 0) {
                                        dataList.splice(hasRft, 2);
                                    }
                                }
                                //调用后台数据
                                var ref9 = rootElement.__DCWriterReference;
                                if (ref9 != null) {
                                    ref9.invokeMethod(
                                        "DoPaste",
                                        dataList);
                                }
                            }
                        }
                    });
                    return;
                }
                //if (dataList.length > 0) {
                //    ////暂时删除掉rtf
                //    //if (Array.isArray(dataList)) {
                //    //    var hasRft = dataList.indexOf('text/rtf');
                //    //    if (hasRft >= 0) {
                //    //        dataList.splice(hasRft, 2);
                //    //    }
                //    //}
                //    dataList = WriterControl_UI.PasteDateFromUTF16(dataList, rootElement);
                //    //调用后台数据
                //    var ref9 = rootElement.__DCWriterReference;
                //    if (ref9 != null) {
                //        if (rootElement.getAttribute("IsUsePasteDiolog") === "true" && WriterControl_UI.PasteDiolog(rootElement, dataList) != false) {
                //            // WriterControl_UI.PasteDiolog(rootElement, dataList);
                //        } else {
                //            ref9.invokeMethod("DoPaste", dataList);
                //        }
                //        if (oldFontName) {
                //            rootElement.DCExecuteCommand('FontName', false, oldFontName);
                //        }
                //    }
                //}
            }


            if (triggerEventBeforePaste(rootElement, dataList) == false) {
                return false;
            }


            //如果clipboardData能读取到数据
            if (dataList.length > 0) {
                dataList = WriterControl_UI.PasteDataFroDcWriterHtml(dataList, rootElement);
                //在此处判断是否为纯文本粘贴
                if (rootElement.isPasteAsText) {
                    //将datas重置为只有text/plain
                    var textIndex = dataList.indexOf('text/plain');
                    dataList = [dataList[textIndex], dataList[textIndex + 1]];
                }
                var needPaste = WriterControl_UI.PasteDataFromTableToTable(dataList, rootElement);
                if (needPaste) {
                    dataList = WriterControl_UI.PasteDateFromUTF16(dataList, rootElement);
                    ////暂时删除掉rtf
                    //if (Array.isArray(dataList)) {
                    //    var hasRft = dataList.indexOf('text/rtf');
                    //    if (hasRft >= 0) {
                    //        dataList.splice(hasRft, 2);
                    //    }
                    //}
                    //调用后台数据
                    var ref9 = rootElement.__DCWriterReference;
                    if (ref9 != null) {
                        if (rootElement.getAttribute("IsUsePasteDiolog") === "true" && WriterControl_UI.PasteDiolog(rootElement, dataList) != false) {
                            // WriterControl_UI.PasteDiolog(rootElement, dataList);
                        } else {
                            ref9.invokeMethod("DoPaste", dataList);
                        }
                        if (oldFontName) {
                            rootElement.DCExecuteCommand('FontName', false, oldFontName);
                        }
                    }
                    //rootElement.RefreshDocument()
                }
            }
        } else if (navigator.clipboard) {
            //先判断是否为firefox还是google
            var userAgent = window.navigator.userAgent;
            if (userAgent.indexOf("Chrome") > -1) {
                if (!navigator.clipboard.read) {
                    console.log('没有读取剪贴版的权限,如果是http协议，请更新到https协议使用');
                    var result = true;
                    if (typeof rootElement.EventErrorPasteReutrn == "function") {
                        result = rootElement.EventErrorPasteReutrn();
                    }
                    if (result !== false) {
                        getloadStorage();
                    }
                    //直接获取浏览器的localstorage
                    //getloadStorage();
                    return false;
                } else {
                    const { state } = await navigator.permissions.query({
                        name: "clipboard-read",
                    });
                    if (state == 'denied') {
                        console.log("读取剪贴板权限被阻止，需要用户手动开启");
                        var result = true;
                        if (typeof rootElement.EventErrorPasteReutrn == "function") {
                            result = rootElement.EventErrorPasteReutrn();
                        }
                        if (result !== false) {
                            getloadStorage();
                        }
                        //getloadStorage();
                        return false;
                    }
                }
            } else if (userAgent.indexOf("Firefox") > -1) {
                //判断是否存在readText
                if (!navigator.clipboard.read) {
                    //控制台打印让客户开启设置
                    console.log('从版本90开始：此功能落后于dom.events.asyncClipboard.read/dom.events.asyncClipboard.clipboardItem首选项（需要设置为true）。要更改Firefox中的首选项，请访问about:config。脚注Firefox只支持使用“clipboardRead”扩展权限在浏览器扩展中读取剪贴板。由于没有剪贴板权限此处只能粘贴编辑器内部复制内容');
                    var result = true;
                    if (typeof rootElement.EventErrorPasteReutrn == "function") {
                        result = rootElement.EventErrorPasteReutrn();
                    }
                    if (result !== false) {
                        getloadStorage();
                    }
                    return false;
                }
            }
            try {
                const clipboardItems = await navigator.clipboard.read();
                for (const clipboardItem of clipboardItems) {
                    for (const type of clipboardItem.types) {
                        const blob = await clipboardItem.getType(type);
                        if (!blob) {
                            continue;
                        }
                        if (dataList.length > 0 && triggerEventBeforePaste(rootElement, dataList, true) == false) {
                            return false;
                        }
                        if (!rootElement.isPasteAsText && clipboardItem.types.length == 1 && type.indexOf('image/') == 0) {
                            new Promise(resolve => {
                                var reader = new FileReader();
                                reader.readAsDataURL(blob);
                                reader.onload = function (e) {
                                    resolve(this);
                                };
                            }).then(result => {
                                dataList.push(type);
                                dataList.push(result.result);

                                //此处不再使用粘贴接口使用插入图片的接口,因为粘贴出来的图片不保存
                                //如果clipboardData能读取到数据
                                if (dataList.length > 0) {
                                    var options = {
                                        SaveContentInFile: true,
                                        Src: dataList[1].split(',')[1]
                                    };
                                    rootElement.DCExecuteCommand("InsertImage", false, options);
                                    rootElement.RefreshDocument();
                                }
                                return;
                            });
                        } else {
                            clipboardData = await blob.text();
                            if (type == 'text/html') {
                                //判断是否为word
                                isOfficeWord = clipboardData.indexOf('schemas-microsoft-com:office:word') >= 0 ? true : false;
                                isOfficeExecl = clipboardData.indexOf('schemas-microsoft-com:office:excel') >= 0 ? true : false;
                                //获取到保存的数据并解析再次赋值
                                var DCAttrHTML = rootElement.ownerDocument.createElement('html');
                                DCAttrHTML.innerHTML = clipboardData;
                                //找到body
                                var hasBody = DCAttrHTML.querySelector('body');
                                if (hasBody) {
                                    var thisDiv = hasBody.querySelector('[DCAttrClipboardData]');
                                    if (thisDiv) {
                                        hasDCAttr = true;
                                        try {
                                            var DCAttrClipboardData = thisDiv.getAttribute('DCAttrClipboardData');
                                            thisDiv.remove();
                                            if (DCAttrClipboardData) {
                                                dataList = JSON.parse(DCAttrClipboardData);
                                            } else {
                                                throw new Error('');
                                            }
                                            //在此处判断是否存在image元素
                                            if (Array.isArray(dataList)) {
                                                for (var list = 0; list < dataList.length; list += 2) {
                                                    if (dataList[list].indexOf('image/') == 0) {
                                                        dataList = [dataList[list], dataList[list + 1]];
                                                        break;
                                                    }
                                                }
                                            }
                                        } catch (err) {
                                            dataList.push(type);
                                            dataList.push(clipboardData);
                                            continue;
                                        }
                                        break;
                                    } else {
                                        //判断是否存在图片并为本地地址
                                        var allImg = hasBody.querySelectorAll('img');
                                        //解析是否存在本地图片
                                        var allLocalImg = [];
                                        if (allImg && allImg.length > 0) {
                                            for (var img = 0; img < allImg.length; img++) {
                                                var thisImg = allImg[img];
                                                if (thisImg.src) {
                                                    thisImg.src = thisImg.src.trim();
                                                    if (thisImg.src.indexOf('file:///') == 0) {
                                                        allLocalImg.push(thisImg.src);
                                                        thisImg.remove();
                                                    }
                                                }
                                            }
                                        }
                                        if (allLocalImg.length > 0) {
                                            clipboardData = DCAttrHTML.outerHTML;
                                            WriterControl_Event.InnerRaiseEvent(rootElement, "EventErrorGetClipboard", allLocalImg);
                                        }
                                        dataList.push(type);
                                        dataList.push(clipboardData);
                                    }
                                } else {
                                    dataList.push(type);
                                    dataList.push(clipboardData);
                                }
                            } else {
                                dataList.push(type);
                                dataList.push(clipboardData);
                            }
                        }
                    }
                }
            } catch (err) {
                // console.log(err);
                getloadStorage();
                return false;
            }

            if (triggerEventBeforePaste(rootElement, dataList) == false) {
                return false;
            }
            //如果clipboardData能读取到数据
            if (dataList.length > 0) {
                //判断是否只存在纯文本
                if (dataList.length == 2 && dataList[0] == "text/plain") {
                    //去localstorage中找数据
                    try {
                        var DCAttrClipboardData = localStorage.getItem('dcClipboardData');
                        if (DCAttrClipboardData) {
                            var oldDataList = JSON.parse(DCAttrClipboardData);
                            var hasText = oldDataList.indexOf("text/plain");
                            if (hasText >= 0) {
                                hasText = oldDataList[hasText + 1];
                                if (hasText === dataList[1]) {
                                    dataList = oldDataList;
                                }
                            }
                        }
                    } catch (err) {
                        console.log(err);
                    }
                }
                dataList = WriterControl_UI.PasteDataFroDcWriterHtml(dataList, rootElement);
                //如果是word此处清除多余的换行符
                var textIndex = dataList.indexOf('text/plain');
                var thisText = dataList[textIndex + 1];
                //截取最后两个字
                dataList[textIndex + 1] = thisText.replace(/\r\n$|\n$|\r$/, "");
                //在此处判断是否为纯文本粘贴
                if (rootElement.isPasteAsText) {
                    //将datas重置为只有text/plain
                    var textIndex = dataList.indexOf('text/plain');
                    dataList = [dataList[textIndex], dataList[textIndex + 1]];
                }
                var needPaste = WriterControl_UI.PasteDataFromTableToTable(dataList, rootElement);
                if (needPaste) {
                    dataList = WriterControl_UI.PasteDateFromUTF16(dataList, rootElement);
                    //暂时删除掉rtf
                    if (Array.isArray(dataList)) {
                        var hasRft = dataList.indexOf('text/rtf');
                        if (hasRft >= 0) {
                            dataList.splice(hasRft, 2);
                        }
                    }
                    //调用后台数据
                    var ref9 = rootElement.__DCWriterReference;
                    if (ref9 != null) {
                        ref9.invokeMethod("DoPaste", dataList);
                        if (oldFontName) {
                            rootElement.DCExecuteCommand('FontName', false, oldFontName);
                        }
                    }
                }
            }
        } else {
            //没有权限,给个提示
            console.log('没有读取剪贴版的权限,如果是http协议，请更新到https协议使用');
            var result = true;
            //直接获取浏览器的localstorage
            if (typeof rootElement.EventErrorPasteReutrn == "function") {
                result = rootElement.EventErrorPasteReutrn();
            }
            if (result !== false) {
                getloadStorage();
            }
        }

        function getloadStorage() {
            try {
                var DCAttrClipboardData = localStorage.getItem('dcClipboardData');
                if (DCAttrClipboardData) {
                    dataList = JSON.parse(DCAttrClipboardData);
                    if (dataList) {
                        var textIndex = dataList.indexOf('text/html');
                        var HtmlStr = dataList[textIndex + 1];
                        var DCAttrHTML = rootElement.ownerDocument.createElement('html');
                        DCAttrHTML.innerHTML = HtmlStr;
                        var hasBody = DCAttrHTML.querySelector('body');
                        if (hasBody) {
                            var thisDiv = hasBody.querySelector('[DCAttrClipboardData]');
                            if (thisDiv) {
                                hasDCAttr = true;
                                try {
                                    var DCAttrClipboardData = thisDiv.getAttribute('DCAttrClipboardData');
                                    thisDiv.remove();
                                    if (DCAttrClipboardData) {
                                        dataList = JSON.parse(DCAttrClipboardData);
                                    } else {
                                        throw new Error('');
                                    }
                                    //在此处判断是否存在image元素
                                    if (Array.isArray(dataList)) {
                                        for (var list = 0; list < dataList.length; list += 2) {
                                            if (dataList[list].indexOf('image/') == 0) {
                                                dataList = [dataList[list], dataList[list + 1]];
                                                break;
                                            }
                                        }
                                    }
                                } catch (err) {
                                }
                            }
                        }
                    }
                } else {
                    throw new Error('');
                }
                //在此处判断是否存在image元素
                if (!rootElement.isPasteAsText && Array.isArray(dataList)) {
                    for (var list = 0; list < dataList.length; list += 2) {
                        if (dataList[list].indexOf('image/') == 0) {
                            dataList = [dataList[list], dataList[list + 1]];
                            break;
                        }
                    }
                }
            } catch (err) {
                console.log(err);
            }
            //触发粘贴前事件
            if (triggerEventBeforePaste(rootElement, dataList) == false) {
                return false;
            }
            //如果clipboardData能读取到数据
            if (dataList && dataList.length > 0) {
                //在此处判断是否为纯文本粘贴
                if (rootElement.isPasteAsText) {
                    //将datas重置为只有text/plain
                    var textIndex = dataList.indexOf('text/plain');
                    dataList = [dataList[textIndex], dataList[textIndex + 1]];
                }
                var needPaste = WriterControl_UI.PasteDataFromTableToTable(dataList, rootElement);
                if (needPaste) {
                    dataList = WriterControl_UI.PasteDateFromUTF16(dataList, rootElement);
                    //暂时删除掉rtf
                    if (Array.isArray(dataList)) {
                        var hasRft = dataList.indexOf('text/rtf');
                        if (hasRft >= 0) {
                            dataList.splice(hasRft, 2);
                        }
                    }
                    //调用后台数据
                    var ref9 = rootElement.__DCWriterReference;
                    if (ref9 != null) {
                        ref9.invokeMethod("DoPaste", dataList);
                        if (oldFontName) {
                            rootElement.DCExecuteCommand('FontName', false, oldFontName);
                        }
                    }
                }
            }
        }
    },
    /** 设置系统剪切板的内容
     * @param {Array} datas 数据内容，偶数位为数据格式的名称，奇数位是数据内容
     * @param {Object} e 事件对象,当处于复制或粘贴事件中使用时,可以通过事件对象自带的setDate方法,不需要权限就能操作剪切板
     * @param 数据格式名称默认使用MIMEMIME 类型 text/plain text/html mage/jpeg image/png
     */
    SetClipboardData: async function (datas, e, eventType, rootElement) {
        // console.log("SetClipboardData");
        // var rootElement = WriterControl_UI.GetCurrentWriterControl();
        // xuyiming 添加复制拦截事件【EventBeforeCopy】
        if (rootElement == null || rootElement.__DCDisposed == true) {
            return false;
        }
        var eventCallBack = null;
        if (eventType == 'copy') {
            if (rootElement.EventBeforeCopy != null && typeof (rootElement.EventBeforeCopy) == "function") {
                eventCallBack = rootElement.EventBeforeCopy;
            }
        } else if (eventType == 'cut') {
            if (rootElement.EventBeforeCut != null && typeof (rootElement.EventBeforeCut) == "function") {
                eventCallBack = rootElement.EventBeforeCut;
            }
        }
        //在此处处理是否为需要为存文本
        if (rootElement.isCopyAsText && Array.isArray(datas)) {
            //将datas重置为只有text/plain
            var textIndex = datas.indexOf('text/plain');
            datas = [datas[textIndex], datas[textIndex + 1]];
        }

        if (eventCallBack != null && typeof (eventCallBack) == "function") {
            var eventData = {
                datas: datas,
                SetClipboardData: WriterControl_UI.SetClipboardData
            };
            var result = eventCallBack(e, eventData);
            if (result === false) {
                return false;
            }
        }

        //datas为空或没有值的时候
        if (datas == null || !Array.isArray(datas)) {
            return;
        }
        if (e != null && e.clipboardData != null) {
            //用户替换特殊字符保证剪贴板的html文本格式正确
            var oldUTF16Symbol = [];
            var newUTF16Symbol = [];
            // 2023-6-3 yyf 简单粗暴的将数据塞入剪切板
            for (var iCount = 0; iCount < datas.length; iCount += 2) {
                //直接判断是否存在特殊字符,此处暂时笼统判断
                if (datas[iCount] == 'text/plain') {
                    var textString = datas[iCount + 1];
                    //拆解字符串
                    var textArr = [...textString];
                    if (textArr.length != textString.length) {
                        //存在特殊字符,此处对特殊字符做处理需要对比并替换文本
                        for (var txt = 0; txt < textArr.length; txt++) {
                            var thisTxt = textArr[txt];
                            if (thisTxt.length == 2) {
                                var utf16Symbol = `&#${thisTxt[0]};&#${thisTxt[1]};`;
                                var newText = DCTools20221228.changeUseUTF16(utf16Symbol, true);
                                if (utf16Symbol != newText) {
                                    oldUTF16Symbol.push(newText.hexadecimal);
                                    newUTF16Symbol.push(newText.newCode);
                                }
                            }
                        }
                    }
                }

                if (datas[iCount] == 'text/html') {
                    //datas = JSON.stringify(datas);
                    //找到body，写入到body的自定义属性中去
                    var DCAttrHTML = rootElement.ownerDocument.createElement('html');
                    //解析字符串并替换
                    if (oldUTF16Symbol && oldUTF16Symbol.length > 0) {
                        for (var oldText = 0; oldText < oldUTF16Symbol.length; oldText++) {
                            var reg = new RegExp(oldUTF16Symbol[oldText], 'gi');
                            datas[iCount + 1] = datas[iCount + 1].replace(reg, newUTF16Symbol[oldText]);
                        }
                    }
                    DCAttrHTML.innerHTML = datas[iCount + 1];
                    //查找是否存在护理记录单XTextNewMedicalExpressionElement
                    var hasMedicalExpression = DCAttrHTML.querySelectorAll("[dctype=XTextNewMedicalExpressionElement],[dctype=XTextNewBarcodeElement],[dctype=XTextTDBarcodeElement]");
                    if (hasMedicalExpression && hasMedicalExpression.length > 0) {
                        //清掉src防止报错
                        for (var i = 0; i < hasMedicalExpression.length; i++) {
                            hasMedicalExpression[i].src = "";
                        }
                    }
                    //找到body
                    var hasBody = DCAttrHTML.querySelector('body');
                    if (!hasBody) {
                        hasBody = rootElement.ownerDocument.createElement('body');
                        DCAttrHTML.appendChild(hasBody);
                    }
                    //if (hasBody.children.length == 0) {
                    //    hasBody.innerHTML = '<div><div>'
                    //}
                    var DCAttrDiv = rootElement.ownerDocument.createElement('div');
                    //判断hasbody去掉/r/n /n空格后是否为空白
                    var hasTextLength = hasBody.innerText.replace(/\/r\/n|\r\n|\/r|\r|\/n|\n/g, '').trim();
                    if (hasTextLength.length == 0) {
                        hasBody.innerHTML = '';
                        DCAttrDiv = rootElement.ownerDocument.createElement('span');
                        //解析是否存在imagedatabase64string
                        var hasBase64ImageFirstIndex = datas[iCount + 1].indexOf('&lt;ImageDataBase64String&gt;');
                        if (hasBase64ImageFirstIndex >= 0) {
                            var hasBase64ImageLastIndex = datas[iCount + 1].indexOf('&lt;/ImageDataBase64String&gt;');
                            var base64Image = datas[iCount + 1].substring(hasBase64ImageFirstIndex + 29, hasBase64ImageLastIndex);
                            DCAttrDiv.innerHTML = `<img src="data:image/png;base64,${base64Image.trim()}"/>`;
                        } else if (datas.indexOf('image/png') >= 0) {
                            DCAttrDiv.innerHTML = `<img src="${(datas[datas.indexOf('image/png') + 1])}"/>`;
                        } else {
                            //在元素中插入text/plain
                            var hasPlain = datas.indexOf('text/plain');
                            if (hasPlain >= 0) {
                                var text = datas[hasPlain + 1];
                                text = text.replace(/\/r\/n|\r\n|\/r|\r|\/n|\n/g, "<br/>");
                                DCAttrDiv.innerHTML = text;
                            }
                        }
                        hasBody.appendChild(DCAttrDiv);
                    }
                    //DCAttrDiv.style.display = 'none';
                    //判断是否存在表格,表格外层存在p标签在html解析时会被分割成上下两个，此处删除
                    var hasTables = hasBody.querySelectorAll('[dctype=XTextTableElement]');
                    //如果存在table那此处的html结构可能存在问题,使用SelectionHtml修改
                    var selectionhtml = rootElement.SelectionHtml();
                    var SelectDiv = document.createElement("div");
                    SelectDiv.innerHTML = selectionhtml;
                    var allSelectTable = SelectDiv.querySelectorAll('[dctype=XTextTableElement]');
                    if (!hasTables || !allSelectTable || allSelectTable.length != hasTables.length) {
                        allSelectTable = [];
                    }
                    for (var i = 0; i < hasTables.length; i++) {
                        var thisTable = hasTables[i];
                        //找到后一个元素
                        var nextP = thisTable.nextElementSibling;
                        if (nextP && nextP.nodeName == "P" && nextP.children && nextP.children.length == 0) {
                            nextP.remove();
                            var prevP = thisTable.previousElementSibling;
                            if (prevP && prevP.nodeName == "P" && prevP.children && prevP.children.length == 0) {
                                prevP.remove();
                            }
                        }
                        if (allSelectTable[i]) {
                            thisTable.outerHTML = allSelectTable[i].outerHTML;
                        }

                    }
                    if (DCAttrDiv.nodeName == 'DIV') {
                        hasBody.appendChild(DCAttrDiv);
                    }
                    DCAttrDiv.setAttribute('DCAttrClipboardData', JSON.stringify(datas));
                    datas[iCount + 1] = hasBody.innerHTML;
                    // if (DCAttrDiv.nodeName == 'DIV') {
                    //     //替换html的数据
                    //     datas[iCount + 1] = DCAttrHTML.outerHTML;
                    // } else {
                    //     datas[iCount + 1] = hasBody.innerHTML;
                    // }
                }

            }
            //直接保存到localstorage中
            try {
                for (var j = 0; j < datas.length; j += 2) {
                    e.clipboardData.setData(datas[j], datas[j + 1]);
                }
                localStorage.setItem('dcClipboardData', JSON.stringify(datas));
            } catch (err) {
                console.log(err);
                localStorage.removeItem('dcClipboardData');
            }
        } else {
            try {
                localStorage.setItem('dcClipboardData', JSON.stringify(datas));
            } catch (err) {
                console.log(err);
                localStorage.removeItem('dcClipboardData');
            }
        }
        rootElement.isCopyAsText = false;
    },
    //
    ///**
    // * 获得根元素对象
    // * @param {string} containerID 容器元素编号
    // * @returns {HTMLElement} 获得的根元素对象
    // */
    //GetRootElement: function (containerID) {
    //    if (containerID != null && containerID.nodeName) {
    //        var pe = containerID;
    //        while (pe != null) {
    //            if (pe.getAttribute) {
    //                if (pe.getAttribute("dctype") == "WriterPrintPreviewControlForWASM"
    //                    || pe.getAttribute("dctype") == "WriterControlForWASM") {
    //                    return pe;
    //                }
    //            }
    //            pe = pe.parentNode;
    //        }
    //    }
    //    else {
    //        return document.getElementById(containerID);
    //    }
    //},

    ///**
    // * 获得指定名称的事件处理函数
    // * @param {string} containerID 容器元素对象编号
    // * @param {string} strEventName 事件名称
    // * @returns {Function} 事件处理函数对象
    // */
    //GetControlEventHandler: function (containerID, strEventName) {
    //    var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
    //    var list = new Array();
    //    var func = window["WriterControl_" + strEventName];
    //    if (typeof (func) == "function") {
    //        // 获得全局性事件函数
    //        list.push(func);
    //    }
    //    func = rootElement[strEventName];
    //    if (typeof (func) == "function") {
    //        // 获得直接绑定的事件函数
    //        list.push(func);
    //    }
    //    else {
    //        // 按照属性名来获得事件函数
    //        var name2 = rootElement.getAttribute(strEventName);
    //        if (name2 != null && name2.length > 0 && typeof (window[name2]) == "function") {
    //            list.push(window[name2]);
    //        }
    //    }
    //    if (list.length == 0) {
    //        return null;
    //    }
    //    else if (list.length == 1) {
    //        return list[0];
    //    }
    //    else {
    //        return list;
    //    }

    //    //var ctl = DCTools20221228.GetOwnerWriterControl(containerID);
    //    //if (ctl != null) {
    //    //    var handler = ctl[strEventName];
    //    //    if (typeof (handler) == "function") {
    //    //        return handler;
    //    //    }
    //    //    var attrName = ctl.getAttribute(strEventName);
    //    //    if (attrName != null && attrName.length > 0) {
    //    //        var h2 = window[attrName];
    //    //        if (typeof (h2) == "function") {
    //    //            return h2;
    //    //        }
    //    //    }
    //    //}
    //    //return null;
    //},
    /**
     * 设置元素的鼠标光标信息
     * @param {string} containerID 容器元素编号
     * @param {Number} intPageIndex 页码
     * @param {string} cursor 光标名称
     */
    SetElementCursor: function (containerID, intPageIndex, cursor) {
        var element = WriterControl_Paint.GetCanvasElementByPageIndex(containerID, intPageIndex);
        if (element != null) {
            element.style.cursor = cursor;
            var p = element.parentNode;
            var element2 = DCTools20221228.GetChildNodeByDCType(p, "caret");
            if (element2 != null) {
                element2.style.cursor = cursor;
            }
            element2 = DCTools20221228.GetChildNodeByDCType(p, "dcinput");
            if (element2 != null) {
                element2.style.cursor = cursor;
            }
        }
    },
    /**
     * 设置提示文本
     * @param {string} containerID 容器元素编号
     * @param {string} strText 提示文本
     */
    SetToolTip: function (containerID, strText) {
        var element = DCTools20221228.GetOwnerWriterControl(containerID);
        if (element != null) {
            element.title = strText;
        }
    },

    // 显示颜色对话框
    ShowColorDialog: function (defaultColor, callBack) {
    },
    /**
     * 显示一个消息框
     * @param {string} strText 消息文本
     * @param {string} strCaption 标题文本
     * @param {string} strButtons 按钮设置
     * @param {string} strIcon 光标设置
     * @param {string} strDefaultButton 默认按钮设置
     * @returns {string} 用户确认的状态
     */
    ShowMessageBox: function (strText, strCaption, strButtons, strIcon, strDefaultButton) {
        // strButtons: OK = 0, OKCancel = 1, AbortRetryIgnore = 2, YesNoCancel = 3, YesNo = 4, RetryCancel = 5
        // DialogResult : None = 0, OK = 1, Cancel = 2, Abort = 3, Retry = 4, Ignore = 5, Yes = 6, No = 7
        if (strButtons == "0") {
            window.alert(strText);
        }
        else if (strButtons == "1") {
            if (window.confirm(strText) == true) {
                return "1";
            }
            else {
                return "2";
            }
        }
        else if (strButtons == "2") {
            if (window.confirm(strText) == true) {
                return "3";
            }
            else {
                return "4";
            }
        }
        else if (strButtons == "3" || strButtons == "4") {
            if (window.confirm(strText) == true) {
                return "6";
            }
            else {
                return "7";
            }
        }
        else if (strButtons == "5") {
            if (window.confirm(strText) == true) {
                return "4";
            }
            else {
                return "2";
            }
        }
        else {
            window.alert(strText);
        }
        return null;
    },


    /**
     * 显示一个输入框
     * @param {string} strTitle 标题文本
     * @param {string} strDefaultValue 默认值
     * @returns {string} 用户输入的文本
     */
    WindowPrompt: function (strTitle, strDefaultValue) {
        return window.prompt(strTitle, strDefaultValue);
    },

    /**
     * 判断是否是预览控件
     * @param {string} containerID 容器元素编号
     */
    IsPrintPreview: function (containerID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement) {
            return rootElement.IsPrintPreview();
        }
        return false;
    },
    IsFocused: function (strID) {
        if (document.hasFocus() == false) {
            return false;
        }
        var element = document.getElementById(strID);
        if (element == null) {
            return false;
        }
        return true;
    },
    /**
     * 滚动视图
     * @param {string} containerID 容器元素编号
     * @param {number} intPageIndex 页码
     * @param {number} intDX 坐标值
     * @param {number} intDY 坐标值
     * @param {number} intWidth 坐标值
     * @param {number} intHeight 坐标值
     * @param {string} strScrollStyle 滚动模式
     */
    ScrollToView: function (containerID, intPageIndex, intDX, intDY, intWidth, intHeight, strScrollStyle) {
        if (WriterControl_Paint.IsDrawingReversibleShape(containerID) == true) {
            // 正在绘制可逆图形，不滚动视图
            return;
        }
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        // 获取是否使用的FocusElementById滚动
        if (rootElement && rootElement.FocusElementByIdScrollMode) {
            WriterControl_UI.ScrollToFocusElement(containerID, intPageIndex, intDX, intDY, intWidth, intHeight, strScrollStyle);
            return;
        }


        //DCTools20221228.EnsureNativeReference(containerID);
        var element = WriterControl_Paint.GetCanvasElementByPageIndex(containerID, intPageIndex);
        if (element != null) {
            var parent = window;
            if (parent != null) {
                var x = element.offsetLeft + intDX + 1;
                var y = element.offsetTop + intDY + 1;
                var clientLeft = 0;
                var clientTop = 0;
                var clientWidth = 0;
                var clientHeight = 0;
                if (parent == window) {
                    clientLeft = window.scrollX;
                    clientTop = window.scrollY;
                    clientWidth = window.innerWidth;
                    clientHeight = window.innerHeight;
                }
                else {
                    clientLeft = - element.scrollLeft;
                    clientTop = - element.scrollTop;
                    clientWidth = element.clientWidth;
                    clientHeight = element.clientHeight;
                }
                //strScrollStyle = "Top";
                switch (strScrollStyle) {
                    case "Normal":
                        var dx = 0;
                        var dy = 0;
                        if (x < clientLeft) {
                            dx = x - clientLeft - 3;
                        }
                        if (x + intWidth > clientLeft + clientWidth) {
                            dx = x + intWidth - clientLeft - clientWidth + 3;
                        }
                        if (y < clientTop) {
                            dy = y - clientTop - 5;
                        }
                        if (y + intHeight > clientTop + clientHeight) {
                            dy = y + intHeight - clientTop - clientHeight + 5;
                        }
                        if (dx != 0 || dy != 0) {
                            if (parent == window) {
                                //window.scrollTo(clientLeft + dx, clientTop + dy);
                                //window.scrollX += dx;
                                //window.scrollY += dy;
                            }
                            else {
                                parent.scrollLeft += dx;
                                parent.scrollTop += dy;
                            }
                        }
                        return;
                    case "Top":
                        if (parent == window) {
                            window.scrollTo(window.scrollX, y);
                            //window.scrollY = y;
                        }
                        else {
                            parent.scrollTop = - y;
                        }
                        break;
                    case "Middle":
                        if (parent == window) {
                            window.scrollY = y + clientHeight / 2;
                        }
                        else {
                            parent.scrollTop = - y - clientHeight / 2;
                        }
                        break;
                    case "Bottom":
                        if (parent == window) {
                            window.scrollY = y + clientHeight;
                        }
                        else {
                            parent.scrollTop = -y - clientHeight;
                        }
                        break;
                }
            }
        }
    },
    /**
         * 滚动视图到光标位置
         * @param {string} containerID 容器元素编号
         * @param {number} intPageIndex 页码
         * @param {number} intDX 坐标值
         * @param {number} intDY 坐标值
         * @param {number} intWidth 坐标值
         * @param {number} intHeight 坐标值
         * @param {string} strScrollStyle 滚动模式
         */
    ScrollToFocusElement: function (containerID, intPageIndex, intDX, intDY, intWidth, intHeight, strScrollStyle) {
        // console.log(containerID, intPageIndex, intDX, intDY, intWidth, intHeight, strScrollStyle);
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (!rootElement) {
            return false;
        }
        // console.log(rootElement.querySelector('#divCaret20221213').innerHeight);
        //获取滚动模式
        strScrollStyle = rootElement.FocusElementByIdScrollMode.toLowerCase();
        //目标CANVAS
        var element = WriterControl_Paint.GetCanvasElementByPageIndex(containerID, intPageIndex);
        // 当前页canvas距离pagecontainer的顶部距离
        var elementToTopHeight = element.offsetTop;
        //目标位置滚动到顶部需要的总距离
        var targetTop = elementToTopHeight + intDY;
        var pageContainer = rootElement.querySelector('[dctype=page-container]');
        //编辑器可是区域宽度计算
        var pageContainerWidth = pageContainer.clientWidth;
        var pageContainerHeight = pageContainer.clientHeight;
        var targerScrollX = 0;
        if (pageContainerWidth < (intDX + intWidth)) {
            targerScrollX = (intDX + intWidth) - pageContainerWidth;
        }

        // if (pageContainer && pageContainer.scrollTop) {
        //判断编辑器父节点是否带有Y轴滚动条
        var RootElementParentNodeScrollTop = (rootElement && rootElement.parentNode && rootElement.parentNode.scrollTop) || 0;

        switch (strScrollStyle) {
            case "top":
                //纵向滚动
                pageContainer.scrollTop = targetTop - RootElementParentNodeScrollTop;
                //判断是否需要轴横向滚动
                if (targerScrollX) {
                    pageContainer.scrollLeft = targerScrollX;
                }
                break;
            case "middle":
                //纵向滚动
                var parentNodeClientHeight = 0;
                if (rootElement && rootElement.parentNode) {
                    parentNodeClientHeight = rootElement.parentNode.clientHeight;
                }
                var scrollToTotal = (parentNodeClientHeight / 2) + (intHeight / 2);

                pageContainer.scrollTop = targetTop - RootElementParentNodeScrollTop - scrollToTotal;
                //判断是否需要轴横向滚动
                if (targerScrollX) {
                    pageContainer.scrollLeft = targerScrollX;
                }
                break;
            case "bottom":
                //纵向滚动
                var parentNodeClientHeight = 0;
                if (rootElement && rootElement.parentNode) {
                    parentNodeClientHeight = rootElement.parentNode.clientHeight;
                }
                //子元素是否超出父元素并都存在滚动条
                if (parentNodeClientHeight < pageContainerHeight) {
                    pageContainer.scrollTop = targetTop - RootElementParentNodeScrollTop - parentNodeClientHeight + (intHeight + 10);
                } else {
                    pageContainer.scrollTop = targetTop + (intHeight + 10) - pageContainerHeight;
                }

                //判断是否需要轴横向滚动
                if (targerScrollX) {
                    pageContainer.scrollLeft = targerScrollX;
                }
                break;
        }
        delete rootElement.FocusElementByIdScrollMode;
    },

    /**
     * 设置光标
     * @param {string} containerID 容器元素编号
     * @param {number} intPageIndex 页码
     * @param {number} intDX 光标位置
     * @param {number} intDY 光标位置
     * @param {number} intWidth 宽度
     * @param {number} intHeight 高度
     * @param {boolean} bolVisible 是否可见
     * @param {boolean} bolReadonly 是否为只读模式
     * @param {boolean} bolScrollToView 是否滚动视图
     * @param {string} callSource 调用来源
     */
    ShowCaret: function (
        containerID,
        intPageIndex,
        intDX,
        intDY,
        intWidth,
        intHeight,
        bolVisible,
        bolReadonly,
        bolScrollToView,
        callSource) {
        // console.log("设置光标" + intPageIndex + "," + intDX + "," + intDY + " " + new Date());
        //获取当前编辑器
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return;
        }

        var strRuntimeID = typeof (containerID) == "string" ? containerID : rootElement.id;
        if (rootElement.__DCDisposed == true || rootElement.Readonly || rootElement.ReadViewMode) {
            // 控件已经被销毁，或者是只读模式，或者是阅读模式
            rootElement.oldCaretOption = {
                strRuntimeID,
                intPageIndex,
                intDX,
                intDY,
                intWidth,
                intHeight,
                bolVisible,
                bolReadonly,
                bolScrollToView
            };
            // 添加当控件已经被销毁，或者是只读模式，或者是阅读模式时，隐藏光标
            WriterControl_UI.HideCaret(rootElement);
            return;
        }

        //是否为续打模式
        var isJumpPrint = rootElement.JumpPrint ? rootElement.JumpPrint.Mode : null;
        if (isJumpPrint && isJumpPrint == "Normal") {
            return;
        }
        if (callSource != "PlayAPILogRecord") {
            // API记录
            WriterControl_UI.APILogRecordJSMethod(rootElement, "ShowCaret", {
                "containerID": containerID,
                "intPageIndex": intPageIndex,
                "intDX": intDX,
                "intDY": intDY,
                "intWidth": intWidth,
                "intHeight": intHeight,
                "bolVisible": bolVisible,
                "bolReadonly": bolReadonly,
                "bolScrollToView": bolScrollToView,
                "callSource": callSource
            });
        }
        var hasContextMenu = rootElement.querySelector("#dcContextMenu");
        // if (rootElement && rootElement.querySelector("#dcContextMenu")) {
        //     hasContextMenu = rootElement.querySelector("#dcContextMenu");
        // }
        if (hasContextMenu) {
            hasContextMenu.remove();
        };

        //开启一个回调函数表示光标位置的改变
        if (typeof rootElement.EventCaretPositionChange == "function") {
            rootElement.EventCaretPositionChange(rootElement);
        }

        var strTextID = "txtEdit20221213";

        var txtEdit = rootElement.querySelector("#" + strTextID);

        //此处在页面上查找不准确，现在从body中查找
        //if (rootElement && rootElement.ownerDocument) {
        //    var allTxtEdit = rootElement.ownerDocument.querySelectorAll("#" + strTextID);
        //    console.log(allTxtEdit);
        //    if (allTxtEdit && allTxtEdit.length > 0) {
        //        for (var i = 0; i < allTxtEdit.length; i++) {
        //            allTxtEdit[i].remove();
        //        }

        //    }
        //}
        // if (rootElement && rootElement.querySelector("#" + strTextID)) {
        //     txtEdit = rootElement.querySelector("#" + strTextID);
        // }
        var strDIVID = "divCaret20221213";
        var divCaret = rootElement.querySelector("#" + strDIVID);
        // if (rootElement && rootElement.querySelector("#" + strDIVID)) {
        //     divCaret = rootElement.querySelector("#" + strDIVID);
        // }

        if (rootElement.ClickCommentAreaFocusCaret == "true") {
            if (txtEdit) {
                try {
                    txtEdit.remove();
                } catch (err) { }
            }
            WriterControl_UI.HideCaret(rootElement);
            return;
        }


        //保留上次点击的元素，只有输入域有值，其他元素值为null,兼容时代接口EventFieldOnBlur
        let prevCaretOption = WriterControl_UI.prevCaretOption;
        //保留一次光标旧数据
        let oldCaretOption = rootElement.oldCaretOption;
        //在此次对光标位置进行一次保存
        rootElement.oldCaretOption = { strRuntimeID, intPageIndex, intDX, intDY, intWidth, intHeight, bolVisible, bolReadonly };

        var hasEditorNoObtainFocus = rootElement.EditorNoObtainFocus;
        //在最开始进行是否判断最近调用的方法是否需要获取到焦点
        if (hasEditorNoObtainFocus != null && typeof hasEditorNoObtainFocus == "string") {
            hasEditorNoObtainFocus = hasEditorNoObtainFocus.toLowerCase().trim();
            if (hasEditorNoObtainFocus == "always" || hasEditorNoObtainFocus == "mobiledisableautosoftkeyboard") {
                return;
            }
            if (hasEditorNoObtainFocus == "once") {
                delete rootElement.EditorNoObtainFocus;
                return;
            }
            if (hasEditorNoObtainFocus == "clicktochangecursor" || hasEditorNoObtainFocus == "keydowntochangecursor" || hasEditorNoObtainFocus == "codetochangecursor") {
                //当是移动端并存在MobileDisableAutoSoftKeyboard=true
                var hasDisableSoftKeyboard = rootElement.getAttribute("mobiledisableautosoftkeyboard");
                if (hasDisableSoftKeyboard == "true" && "ontouchstart" in rootElement.ownerDocument.documentElement) {
                    WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                        rootElement.EditorNoObtainFocus = "MobileDisableAutoSoftKeyboard";
                    });
                }
            }
        } else {
            //当是移动端并存在MobileDisableAutoSoftKeyboard=true
            var hasDisableSoftKeyboard = rootElement.getAttribute("mobiledisableautosoftkeyboard");
            if (hasDisableSoftKeyboard == "true" && "ontouchstart" in rootElement.ownerDocument.documentElement) {
                rootElement.EditorNoObtainFocus = "MobileDisableAutoSoftKeyboard";
                return;
            }
        }

        DCTools20221228.EnsureNativeReference(rootElement);
        var currentElement = rootElement.CurrentInputField();
        // if (rootElement && rootElement.CurrentInputField()) {
        //     currentElement = rootElement.CurrentInputField();
        // }

        //在移动端判断点击位置和当前的光标位置是否有大的区别，如果超出20且设置了为不自动获取焦点则退出
        if ("ontouchstart" in rootElement.ownerDocument.documentElement) {
            //判断是否为表单模式
            if (rootElement.DocumentOptions.BehaviorOptions.FormView == "Strict" || rootElement.FormView() == "Strict") {
                var hasReadonlyAutoFocus = rootElement.getAttribute("readonlyautofocus");
                if (rootElement.mobileMousePosition && typeof rootElement.mobileMousePosition == "object") {
                    if (typeof hasReadonlyAutoFocus == "string") {
                        hasReadonlyAutoFocus = hasReadonlyAutoFocus.toLowerCase().trim();
                        var needReturnFocus = false;
                        //判断是否存在不自动聚焦的设置 
                        if (hasReadonlyAutoFocus == "false" || hasReadonlyAutoFocus == "xandyaxis") {
                            if (Math.abs(rootElement.mobileMousePosition.y - intDY) > 20 || Math.abs(rootElement.mobileMousePosition.x - intDX) > 10) {
                                needReturnFocus = true;
                            }
                            //} else if (hasReadonlyAutoFocus == "xaxis" && Math.abs(rootElement.mobileMousePosition.x - intDX) > 10) {
                            //    needReturnFocus = true;
                        } else if (hasReadonlyAutoFocus == "yaxis" && Math.abs(rootElement.mobileMousePosition.y - intDY) > 20) {
                            needReturnFocus = true;
                        }

                        if (needReturnFocus) {
                            rootElement.mobileMousePosition = "mobileMousePosition";
                            rootElement.ownerDocument.defaultView.top.focus();
                            //移动选中到输入的后面
                            //rootElement.MoveEnd();
                            if (divCaret) {
                                WriterControl_UI.HideCaret(rootElement);
                                //var activeEle = rootElement.ownerDocument.hasFocus();

                                //在此处处理关闭下拉操作
                                //if (!activeEle) {
                                var dropdownDiv = rootElement.querySelector("#divDropdownContainer20230111");
                                if (dropdownDiv) {
                                    dropdownDiv.CloseDropdown();
                                    // 关闭时间选择界面,添加判断避免错误
                                    // if (dropdownDiv.thisApi && dropdownDiv.thisApi.theTool && typeof (dropdownDiv.thisApi.theTool.hidePanel) == "function") {
                                    //     dropdownDiv.thisApi.theTool.hidePanel();
                                    // }
                                }
                                //关闭表格下拉输入域
                                var dropdownTable = rootElement.querySelector("#DCTableControl20240625151400");
                                if (dropdownTable && dropdownTable.CloseDropdownTable) {
                                    dropdownTable.CloseDropdownTable();
                                }


                                //}
                                if (!rootElement.LoadRemoveChild && txtEdit) {
                                    try {
                                        txtEdit.remove();
                                    } catch (err) { }
                                }
                            }
                            return;
                        }
                    }
                } else if (typeof rootElement.mobileMousePosition == "string") {
                    return;
                }
            }
            rootElement.mobileMousePosition = null;
        }


        //防止EventFieldOnBlur多次触发，并且点击的元素不是输入域
        if (JSON.stringify(rootElement.oldCaretOption) !== JSON.stringify(oldCaretOption) && currentElement == null) {

            //上次点击元素为输入域
            if (prevCaretOption != null) {
                // 获取上次点击的输入域的属性
                let currentEleFileInputOptions = rootElement.GetElementProperties(prevCaretOption);
                if (rootElement.EventFieldOnBlur != null && typeof rootElement.EventFieldOnBlur == "function") {
                    rootElement.EventFieldOnBlur(currentEleFileInputOptions);
                }
            }
        }

        //获取内部的dctype="page-container"
        var pageContainer = WriterControl_UI.GetPageContainer(rootElement);// rootElement.querySelector('[dctype=page-container]')
        var pageElement = WriterControl_Paint.GetCanvasElementByPageIndex(containerID, intPageIndex);
        var backgroundColorStr = "rgba(0,0,0,0.8)";
        var caretLeftNum = 0;
        var caretTopNum = 0;
        var readonlyColor = null;
        // 自定义光标的宽度,颜色,右偏移量,只读下颜色展示,下偏移量【CaretCss="5,red,3，transparent,3"】
        var CaretCss = rootElement.getAttribute("caretcss");
        // if (rootElement && rootElement.getAttribute("caretcss")) {
        //     CaretCss = rootElement.getAttribute("caretcss");
        // }
        if (CaretCss && CaretCss.length > 0) {
            CaretCss = CaretCss.split(/,(?=\S)/);
            if (CaretCss[0]) {
                var num = CaretCss[0].trim();
                num = parseFloat(num);
                if (isNaN(num) == false) {
                    intWidth = num;
                }
            }
            if (CaretCss[1]) {
                backgroundColorStr = CaretCss[1].trim();
            }
            if (CaretCss[2]) {
                var leftNum = CaretCss[2].trim();
                leftNum = parseFloat(leftNum);
                if (isNaN(leftNum) == false) {
                    caretLeftNum = leftNum;
                }
            }
            if (CaretCss[3]) {
                readonlyColor = CaretCss[3].trim();
            }
            if (CaretCss[4]) {
                var topNum = CaretCss[4].trim();
                topNum = parseFloat(topNum);
                if (isNaN(topNum) == false) {
                    caretTopNum = topNum;
                }
            }

            //var _index = CaretCss.indexOf(",");
            //var num = CaretCss;
            //if (_index > -1) {
            //    num = CaretCss.slice(0, _index);
            //    var bgc = CaretCss.slice(_index + 1);
            //    if (bgc) {
            //        backgroundColorStr = bgc;
            //    }
            //}
            //if (num) {
            //    num = parseFloat(num);
            //    if (isNaN(num) == false) {
            //        intWidth = num;
            //    }
            //}

        }

        if (divCaret == null) {
            divCaret = rootElement.ownerDocument.createElement("DIV");
            divCaret.id = strDIVID;
            divCaret.style.position = "absolute";
            divCaret.style.zIndex = 1000;
            divCaret.style.backgroundColor = backgroundColorStr;
            divCaret.style.pointerEvents = "none";
            divCaret.style.display = "none";
            divCaret.setAttribute("dctype", "caret");
            var ces = WriterControl_UI.GetPageCanvasElements(containerID);
            if (ces != null && ces.length > 0) {
                var p9 = ces[0].parentNode;
                p9.insertBefore(divCaret, p9.firstChild);
            }
            else {
                rootElement.ownerDocument.body.insertBefore(divCaret, rootElement.ownerDocument.body.firstChild);
            }
        }
        if (bolReadonly && readonlyColor) {
            divCaret.style.backgroundColor = readonlyColor;
        } else {
            divCaret.style.backgroundColor = backgroundColorStr;
        }

        if (bolVisible) {
            // 显示光标
            if (divCaret.style.display == "none") {
                //console.log('showCaret');

                //获取到所有的还在闪烁的showCaret添加上display:none
                var allCaret = rootElement.ownerDocument.querySelectorAll("div[id=divCaret20221213]");
                //循环判断是否是其他的光标
                for (var caret = 0; caret < allCaret.length; caret++) {
                    var thisCaret = allCaret[caret];
                    if (thisCaret != divCaret) {
                        //判断是否显示
                        var hasDisplay = thisCaret.style.display;
                        if (hasDisplay != null) {
                            //判断前一个元素是否为输入域
                            var prevEle = thisCaret.previousSibling;
                            if (prevEle && prevEle.nodeName == "TEXTAREA") {
                                try {
                                    prevEle.remove();
                                } catch (err) { }

                            }
                            WriterControl_UI.HideCaret(rootElement, thisCaret);
                        }
                    }
                }
                divCaret.style.removeProperty("display");
                //在每次做定时器时都先清除一下之前的设置,防止出现光标跳的过快情况
                clearInterval(divCaret.handleTimer);
                divCaret.func = function () {
                    var divCaret = rootElement.querySelector("#" + strDIVID);
                    if (divCaret == null) {
                        return;
                    }
                    if (divCaret.style.display == "none") {
                        WriterControl_UI.HideCaret(rootElement, divCaret);
                        return;
                    }
                    if (divCaret.parentNode && divCaret.parentNode.style.display == "none") {
                        // 父节点不可见，则不后续处理。
                        return;
                    }
                    if (divCaret.style.display != "none") {
                        if (divCaret.style.visibility == "hidden") {
                            divCaret.style.visibility = "visible";
                        }
                        else {
                            divCaret.style.visibility = "hidden";
                        }
                    }
                    if (txtEdit && rootElement.ownerDocument.activeElement != txtEdit
                        && WriterControl_Paint.IsDrawingReversibleShape(rootElement) == false) {
                        txtEdit.focus();
                    }
                };
                divCaret.handleTimer = window.setInterval(divCaret.func, 530);

                //DUWRITER5_0-3309增加编辑器获取焦点事件
                if (rootElement && rootElement.EventWriterControlFocus && typeof rootElement.EventWriterControlFocus === 'function') {
                    rootElement.EventWriterControlFocus.call(rootElement);
                }

            }
            // 判断是否为设计模式
            var isDesignMode = rootElement.InDesignMode();
            if (pageElement != null) {
                // txtEdit.__DCWriterReference = pageElement.parentNode.parentNode.__DCWriterReference;

                //var textTop = 0;
                ////拿到当前页面的文本的top值,加入计算
                //var thisTextTop = rootElement.GetElementProperties(rootElement.CurrentElement("XTextCharElement"));
                //if (thisTextTop && thisTextTop.Top) {
                //    textTop = thisTextTop.Top / 4 * 3;
                //}
                //var newLeft = (pageElement.offsetLeft + intDX + 1) + caretLeftNum + (isDesignMode ? 3 : 0) + "px";
                //var newTop = (pageElement.offsetTop + intDY + 1) + caretTopNum + (isDesignMode ? 5 : 0) + 2 + "px";

                //if (DCTools20221228.GetChildNodeByDCType(rootElement, "hrule") != null) {
                //    // 显示的标尺
                //    newLeft += pageElement.parentNode.offsetLeft;
                //    newTop += pageElement.parentNode.offsetTop;
                //}
                divCaret.style.width = intWidth + "px";
                divCaret.style.height = intHeight + "px";
                //有客户提出光标有宽度时过度靠右，计算left的时候会尽量让光标居中
                //if (intWidth > 1) {
                //    newLeft = parseFloat(newLeft) - (Math.floor(intWidth / 2) + 1) + 'px';
                //}
                //divCaret.style.left = newLeft;
                //divCaret.style.top = newTop;
                var rs = (window.getComputedStyle && window.getComputedStyle(pageElement)) || null;
                var BorderLeftWidth = 0;
                var BorderTopWidth = 0;
                if (rs && rs.getPropertyValue) {
                    BorderLeftWidth = parseFloat((rs.getPropertyValue("border-left-width"))) || 0;
                    BorderTopWidth = parseFloat((rs.getPropertyValue("border-top-width"))) || 0;
                }
                divCaret.style.left = (pageElement.offsetLeft + BorderLeftWidth + intDX - (parseInt(parseFloat(intWidth) / 2))) + "px";
                divCaret.style.top = (pageElement.offsetTop + BorderTopWidth + intDY) + "px";

                //divCaret.style.left = (pageElement.offsetLeft + intDX + (isDesignMode ? 4 : 0)) + caretLeftNum + "px";
                //divCaret.style.top = (pageElement.offsetTop + intDY + (isDesignMode ? 4 : 0)) + caretTopNum + "px";
                divCaret.style.visibility = "visible";
            }
        }

        if ("ontouchstart" in rootElement.ownerDocument.documentElement) {
            var hasMobileDisableSoftKeyboard = rootElement.getAttribute("MobileDisableSoftKeyboard");
            if (typeof hasMobileDisableSoftKeyboard == "string" && hasMobileDisableSoftKeyboard.toLowerCase().trim() == "true") {
                return;
            }
        }

        if (txtEdit == null) {
            txtEdit = rootElement.ownerDocument.createElement("textarea");
            txtEdit.setAttribute("autocomplete", "off");
            txtEdit.setAttribute("dctype", "dcinput");
            txtEdit._rootElementId = rootElement.id;
            txtEdit.id = strTextID;
            txtEdit.style.position = "absolute";
            txtEdit.style.zIndex = 1001;
            txtEdit.style.backgroundColor = "#ffffff";
            txtEdit.style.display = "none";
            txtEdit.style.width = "0px";
            //txtEdit.style.border = "1px solid red";
            txtEdit.style.border = "none";
            txtEdit.style.resize = "none";
            txtEdit.style.overflow = "hidden";
            txtEdit.style.outline = "none";
            txtEdit.style.padding = "0px";
            txtEdit.style.margin = "0px";
            txtEdit.style.lineHeight = "1";
            txtEdit.style.opacity = "0";
            txtEdit.style.pointerEvents = "none";
            // 插入到光标div的前面
            divCaret.parentNode.insertBefore(txtEdit, divCaret);


            // 绑定事件
            txtEdit.addEventListener("keydown", WriterControl_UI.TextEditKeyDownFunc);

            txtEdit.addEventListener("keypress", WriterControl_UI.TextEditKeyPressFunc);

            txtEdit.addEventListener("keyup", WriterControl_UI.TextEditKeyUpFunc);

            txtEdit.hasCompositionstart = false;
            //中文输入开始时的回调
            txtEdit.addEventListener("compositionstart", WriterControl_UI.TextEditCompositionStartFunc);

            //中文输入时的回调
            txtEdit.addEventListener("compositionupdate", WriterControl_UI.TextEditCompositionUpdateFunc);

            //中文输入域结束后的回调
            txtEdit.addEventListener("compositionend", WriterControl_UI.TextEditCompositionEndFunc);

            txtEdit.addEventListener("input", WriterControl_UI.TextEditInputFunc);
            // 修改成防抖函数，用于优化文本编辑控件的焦点事件处理，避免递归【DUWRITER5_0-3684】
            // txtEdit.addEventListener("focus", WriterControl_UI.DebounceTextEditFocusFunc);
            txtEdit.addEventListener("focus", WriterControl_UI.TextEditFocusFunc);
            txtEdit.addEventListener("blur", WriterControl_UI.TextEditBlurFunc);

            //txtEdit.addEventListener('focus', function () {
            //    //当存在下拉框时，对光标定位进行只读操作，防止移动端软键盘的出现
            //    if ("ontouchstart" in rootElement.ownerDocument.documentElement) {
            //        var dropdown = rootElement.querySelector('#divDropdownContainer20230111');
            //        console.log(rootElement.CloseDropdown)
            //        if (dropdown != null && rootElement.CloseDropdown) {
            //            this.setAttribute('readonly', true);
            //            rootElement.CloseDropdown = false;
            //            setTimeout(() => {
            //                this.removeAttribute('readonly');
            //            },100)
            //        }
            //    }
            //})

            //rootElement.ownerDocument.body.addEventListener('copy', divCaret.txtEditCopy);

            //粘贴不能必须选中再执行,需要未选中情况下也能执行
            txtEdit.addEventListener("paste", WriterControl_UI.TextEditPasteFunc);
        }


        if (bolVisible) {
            // 显示光标
            //if (divCaret.style.display == "none") {
            //    //console.log('showCaret');
            //    //获取到所有的还在闪烁的showCaret添加上display:none
            //    var allCaret = rootElement.ownerDocument.querySelectorAll('div[id=divCaret20221213]');
            //    //循环判断是否是其他的光标
            //    for (var caret = 0; caret < allCaret.length; caret++) {
            //        var thisCaret = allCaret[caret];
            //        if (thisCaret != divCaret) {
            //            //判断是否显示
            //            var hasDisplay = thisCaret.style.display
            //            if (hasDisplay != null) {
            //                thisCaret.style.display = 'none';
            //                clearInterval(thisCaret.handleTimer)
            //            }
            //        }
            //    }
            //    divCaret.style.removeProperty('display');
            //    //在每次做定时器时都先清除一下之前的设置,防止出现光标跳的过快情况
            //    clearInterval(divCaret.handleTimer)
            //    divCaret.func = function () {
            //        var divCaret = rootElement.querySelector('#' + strDIVID);
            //        if (divCaret == null) {
            //            return;
            //        }
            //        if (divCaret.parentNode && divCaret.parentNode.style.display == "none") {
            //            // 父节点不可见，则不后续处理。
            //            return;
            //        }
            //        if (divCaret.style.display != "none") {
            //            if (divCaret.style.visibility == "hidden") {
            //                divCaret.style.visibility = "visible";
            //            }
            //            else {
            //                divCaret.style.visibility = "hidden";
            //            }
            //        }
            //        if (rootElement.ownerDocument.activeElement != txtEdit
            //            && WriterControl_Paint.IsDrawingReversibleShape(rootElement) == false) {
            //            txtEdit.focus();
            //        }
            //    };
            //    divCaret.handleTimer = window.setInterval(divCaret.func, 530);

            //}
            //var pageElement = WriterControl_Paint.GetCanvasElementByPageIndex(containerID, intPageIndex);

            txtEdit.__DCWriterReference = null;
            if (pageElement != null) {
                txtEdit.__DCWriterReference = pageElement.parentNode.parentNode.__DCWriterReference;

                //var newLeft = (pageElement.offsetLeft + intDX + 1) + caretLeftNum + "px";
                //var newTop = (pageElement.offsetTop + intDY + 2) + "px";
                ////if (DCTools20221228.GetChildNodeByDCType(rootElement, "hrule") != null) {
                ////    // 显示的标尺
                ////    newLeft += pageElement.parentNode.offsetLeft;
                ////    newTop += pageElement.parentNode.offsetTop;
                ////}
                //divCaret.style.width = intWidth + "px";
                //divCaret.style.height = intHeight + "px";
                ////有客户提出光标有宽度时过度靠右，计算left的时候会尽量让光标居中
                //if (intWidth > 1) {
                //    newLeft = parseFloat(newLeft) - (Math.floor(intWidth / 2) + 1) + 'px';
                //}
                //divCaret.style.left = newLeft;
                //divCaret.style.top = newTop;
                //divCaret.style.visibility = "visible";

                var rs = (window.getComputedStyle && window.getComputedStyle(pageElement)) || null;
                var BorderLeftWidth = 0;
                var BorderTopWidth = 0;
                if (rs && rs.getPropertyValue) {
                    BorderLeftWidth = parseFloat((rs.getPropertyValue("border-left-width"))) || 0;
                    BorderTopWidth = parseFloat((rs.getPropertyValue("border-top-width"))) || 0;
                }
                txtEdit.style.left = (pageElement.offsetLeft + intDX + BorderLeftWidth - (parseInt(parseFloat(intWidth) / 2))) + caretLeftNum + "px";
                txtEdit.style.top = (pageElement.offsetTop + intDY + BorderTopWidth) + caretTopNum + "px";
                //修改让文本能展示完全
                txtEdit.style.height = (rootElement.CurrentFontSize * 1.5) + "px";
                if (typeof divCaret.func == "function") {
                    divCaret.ShowCaretTimeoutFunc = window.setTimeout(function () {
                        txtEdit.style.fontSize = rootElement.CurrentFontSize + "pt";
                        txtEdit.style.fontFamily = rootElement.CurrentFontName;
                        divCaret.func();
                        clearTimeout(divCaret.ShowCaretTimeoutFunc);
                        divCaret.ShowCaretTimeoutFunc = null;
                    }, 200);
                }
                //页面是否跟随光标跳转
                //if (bolScrollToView != false) {
                let hasNotScrollWithCaret = rootElement.getAttribute("notscrollwithcaret");
                if (typeof hasNotScrollWithCaret == "string" && hasNotScrollWithCaret.toLowerCase() == "true") {
                    if (txtEdit.style.display == "none") {
                        WriterControl_UI.CaretWithinVisibleArea(rootElement);
                        txtEdit.focus();
                    }
                } else {
                    if (bolScrollToView === true) {
                        //判断是否在浏览器可视区域内
                        var topGap = pageContainer.scrollTop;
                        var bottomGap = pageContainer.scrollTop + pageContainer.offsetHeight;
                        if (pageElement.offsetTop + intDY < topGap) {
                            pageContainer.scrollTo(0, pageElement.offsetTop + intDY - 50);
                        } else if (bottomGap - (pageElement.offsetTop + intDY) <= intHeight) {
                            //当不在可是区域内时
                            var dy3 = pageElement.offsetTop
                                + intDY - pageContainer.offsetHeight
                                + intHeight + 50;
                            pageContainer.scrollTo(0, dy3);
                        }
                        txtEdit.style.display = "";
                        txtEdit.focus();
                    }
                    //if (txtEdit.style.display == "none") {
                    txtEdit.style.display = "";
                    txtEdit.focus();
                    //}
                }
                //}
                //if (rootElement.touchMove) {
                //    txtEdit.setAttribute('readonly', true);
                //} else {
                //    txtEdit.removeAttribute('readonly');
                //}

                //console.log(rootElement.CloseDropdown);


                //在此处处理是否光标位置的是输入域
                let newCaretOption = JSON.stringify(rootElement.oldCaretOption);
                oldCaretOption = JSON.stringify(oldCaretOption);
                //防止EventFieldOnFocus多次触发
                if (JSON.stringify(newCaretOption) !== JSON.stringify(oldCaretOption)) {
                    var currentEle = rootElement.CurrentInputField();
                    WriterControl_UI.prevCaretOption = currentEle;
                    if (currentEle != null) {
                        let currentEleFileInputOptions = rootElement.GetElementProperties(currentEle);
                        if (rootElement.EventFieldOnFocus != null && typeof rootElement.EventFieldOnFocus == 'function') {
                            rootElement.EventFieldOnFocus(currentEleFileInputOptions);
                        }
                    }
                }
            }

            var isReadonly = rootElement.Readonly;
            if (isReadonly) {
                divCaret.style.display = "none";
            }

        }
        else if (divCaret.style.display != "none") {
            divCaret.style.display = "none";
            //window.clearInterval(divCaret.handleTimer);
            //delete divCaret.handleTimer;
        }

        if ("ontouchstart" in rootElement.ownerDocument.documentElement) {
            //判断是否为移动端且存在MobileDisableAutoSoftKeyboard属性
            var hasDisableSoftKeyboard = rootElement.getAttribute("mobiledisableautosoftkeyboard");
            if (hasDisableSoftKeyboard == "true") {
                //此处只做输入域下的情况，其他暂时不做处理获取获取元素不准确
                // var typeName = rootElement.GetCurrentElementTypeName();
                if (rootElement.DocumentOptions.BehaviorOptions.FormView == "Strict" || rootElement.FormView() == "Strict") {
                    if (currentElement) {
                        var eleAttr = rootElement.GetElementProperties(currentElement);
                        if ((eleAttr && eleAttr.InnerEditStyle != "Text") || rootElement.touchMove) {
                            try {
                                //在此处判断是否为输入域
                                txtEdit.onblur = null;
                                txtEdit.remove();
                            } catch (err) { }

                        }
                    }
                }
            }
        }
    },

    /**
     * 隐藏光标
     * @param {string | HTMLElement} rootElement 编辑器对象
     */
    HideCaret: function (rootElement, thisCaret) {
        var divCaret = rootElement.querySelector('#divCaret20221213');
        var alwaysDisplayCursor = rootElement.getAttribute("AlwaysDisplayCursor");
        if (thisCaret) {
            thisCaret.style.display = "none";
            window.clearInterval(thisCaret.handleTimer);
        }
        if (divCaret && (typeof alwaysDisplayCursor != "string" || alwaysDisplayCursor.toLowerCase().trim() != "true")) {
            divCaret.style.display = "none";
            window.clearInterval(divCaret.handleTimer);
        }
        // if(divCaret&&divCaret.func){
        //     divCaret.func=null
        // }
    },

    /**
     * 判断光标位置是否在视图可视区域内
     */
    CaretWithinVisibleArea: function (rootElement) {
        //获取pageContainer
        // 检查根元素是否存在
        if (rootElement) {
            // 查询页面容器元素
            var pageContainer = rootElement.querySelector('[dctype="page-container"]');
            // 查询文本编辑器元素
            var txtEdit = rootElement.querySelector('#txtEdit20221213');
            // 检查页面容器是否可滚动且文本编辑器存在
            if (pageContainer && pageContainer.scrollHeight > pageContainer.clientHeight && txtEdit) {
                // 计算页面容器的滚动位置和高度
                var TopHeight = pageContainer.scrollTop;
                var BottomHeight = TopHeight + pageContainer.offsetHeight;
                // 计算文本编辑器的顶部位置
                var caretTop = parseFloat(txtEdit.style.top) + txtEdit.offsetHeight;
                // 检查文本编辑器是否在可视区域内
                if (caretTop < BottomHeight && caretTop > TopHeight) {
                    // 如果文本编辑器被隐藏，则显示它
                    if (txtEdit.style.display == "none") {
                        txtEdit.style.display = "";
                    }
                } else {
                    // 如果文本编辑器不在可视区域内，则返回false
                    return false;
                }
            } else {
                // 如果页面容器不可滚动或文本编辑器不存在，则显示文本编辑器
                // 修复NotScrollWithCaret='true'导致的编辑器在没有滚动条的时候无法编辑的问题【DUWRITER5_0-3084】
                if (txtEdit && txtEdit.style.display == "none") {
                    txtEdit.style.display = "";
                }
                // return false;
            }
        } else {
            // 如果根元素不存在，则返回false
            return false;
        }
    },

    /**获得当前编辑控件对象
     * @returns {HTMLElement} 编辑器控件容器元素
     * */
    GetCurrentWriterControl: function (callback, rootElement) {
        //var strDIVID = "divCaret20221213";
        //var divCaret = document.querySelectorAll(strDIVID);
        //if (divCaret != null) {
        //    // 向上查找父节点，找出编辑器对象
        //    for (var iCount = 0; iCount < 3; iCount++) {
        //        divCaret = divCaret.parentNode;
        //        if (divCaret != null && divCaret.getAttribute("dctype") == "WriterControlForWASM") {
        //            return divCaret;
        //        }
        //    }
        //}
        //return null;
        var showWriterControl = null;
        var allShowWriterControl = [];
        //zhangbin 20230705 此处处理，判断当前没有被隐藏的是那个元素
        if (rootElement) {
            var divWASM = rootElement.ownerDocument.body.querySelectorAll("[dctype='WriterControlForWASM']");
        } else {
            var divWASM = document.querySelectorAll("[dctype='WriterControlForWASM']");
        }
        for (var icount = 0; icount < divWASM.length; icount++) {
            var element = divWASM[icount];
            if (element.getClientRects) {
                var rects = element.getClientRects();
                if (rects != null && rects.length != 0) {
                    if (showWriterControl == null) {
                        showWriterControl = element;
                    }
                    allShowWriterControl.push(element);
                }
            }
        }
        if (typeof callback == 'function') {
            callback(allShowWriterControl);
        }
        return showWriterControl;
    },
    /**获得当前页面图形对象
     * @param {string | HTMLDivElement } containerID 容器元素对象编号
     * @returns { HTMLCanvasElement } 页面图形对象 */
    GetCurrentPageElement: function (containerID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement != null) {
            if (rootElement.__DCDisposed == true) {
                return null;
            }
            var pi = rootElement.__DCWriterReference.invokeMethod("get_PageIndex");
            if (pi >= 0) {
                return WriterControl_Paint.GetCanvasElementByPageIndex(rootElement, pi);
            }
        }
        return null;
    },

    //wyc20230518
    /** 直接执行给定的JS代码
     * @param {string} jsstring js代码字符串 
    * @param { boolean } needdecode 是否需要解码后再执行 */
    RaiseJavaScriptBlock: function (jsstring, needdecode) {
        var jscode = jsstring;
        if (needdecode === true) {
            jscode = decodeURIComponent(jsstring);
        }
        if (window.execScript) {
            window.execScript(jscode);
        } else {
            window.eval(jscode);
        }
    },

    /**
     * 添加IsUsePasteDiolog属性为true时ctrl+v粘贴时显示一个粘贴对话框，用于选择粘贴文本的处理方式【DUWRITER5_0-2925】
     * @param {HTMLElement} rootElement - 文档的根元素。
     * @param {Array} dataList - 粘贴板数据列表。
     * @returns {boolean} 如果成功显示对话框则返回true，否则返回false。
     */
    PasteDiolog: function (rootElement, dataList) {
        // 检查参数是否有效
        if (!rootElement || !dataList) {
            return false;
        }
        // 检查是否允许使用粘贴对话框
        if (rootElement.getAttribute("IsUsePasteDiolog") !== "true") {
            return false;
        }
        // 获取光标指示器元素
        var caret = rootElement.querySelector('#divCaret20221213');
        if (!caret) {
            return false;
        }
        // 如果数据列表仅包含纯文本，则直接处理，不显示对话框
        if (Array.isArray(dataList) && dataList.includes('text/plain') && dataList.length == 2) {
            return false;
        }
        // 获取光标位置信息
        var PosObj = rootElement.ReturnLastCaretPosition();
        var pageIndex = PosObj.intPageIndex,
            x = PosObj.intDX + caret.offsetWidth,
            y = PosObj.intDY + caret.offsetHeight;
        // 根据光标位置创建下拉框容器    
        var divContainer = WriterControl_UI.GetDropdownContainer(rootElement, pageIndex, x, y, 0, true);
        if (!divContainer) {
            return false;
        }
        // 定义下拉框中的选项
        var ListData = [
            {
                Text: "粘贴-保留源格式",
                type: "keep"
            },
            {
                Text: "粘贴-合并格式",
                type: "merge"
            },
            {
                Text: "粘贴-只保留文本",
                type: "text"
            }
        ];
        // 创建并添加样式
        var styleDom = document.createElement("style");
        styleDom.innerHTML = `.dc_ListItem{text-align:left;display:flex;align-items: center;font-size:12px;width:100%; min-height: 30px;background-color: transparent;padding: 0 5px;overflow: hidden;white-space: nowrap;cursor: pointer;border-bottom: 1px solid #eee;box-sizing: border-box;}
                    .dc_ListItem:hover{background-color:#eaf2ff!important;color:#000000!important;outline:1px solid #b7d2ff!important;}`;
        divContainer.appendChild(styleDom);
        // 创建外层包裹元素
        var listBox = document.createElement("div");
        listBox.setAttribute("class", "dcListBox");
        listBox.style.cssText = "margin:0;padding:2px;background-color: #ffffff;color: #444;";
        // 添加选项到列表容器
        for (var i = 0; i < ListData.length; i++) {
            var item = document.createElement("div");
            item.className = "dc_ListItem";
            item.innerText = ListData[i].Text;
            item.setAttribute("data-type", ListData[i].type);
            // 绑定点击事件处理函数
            item.onclick = function () {
                var isChangeOpt = false;
                var oldCaretOption = rootElement.DocumentOptions.BehaviorOptions.AutoClearTextFormatWhenPasteOrInsertContent;

                // 提取设置选项的逻辑为函数，以减少重复代码
                function setBehaviorOption(value) {
                    if (oldCaretOption !== value) {
                        isChangeOpt = true;
                        rootElement.DocumentOptions.BehaviorOptions.AutoClearTextFormatWhenPasteOrInsertContent = value;
                        rootElement.ApplyDocumentOptions();
                    }
                }

                // 根据选项类型执行相应的操作
                switch (this.getAttribute("data-type")) {
                    case "keep":
                        setBehaviorOption(false); // 使用提取的函数简化逻辑
                        break;
                    case "merge":
                        setBehaviorOption(true); // 使用提取的函数简化逻辑
                        break;
                    case "text":
                        // 如果数据列表包含纯文本，调整数据列表
                        if (Array.isArray(dataList) && dataList.includes('text/plain')) {
                            const textIndex = dataList.indexOf('text/plain');
                            dataList = [dataList[textIndex], dataList[textIndex + 1]];
                        } else {
                            // console.error("dataList is invalid for text type operation."); // 适当的错误处理
                        }
                        break;
                    default:
                        return;
                }
                // 调用方法处理粘贴操作
                var ref9 = rootElement.__DCWriterReference;
                if (ref9 !== null && ref9 !== undefined) {
                    ref9.invokeMethod("DoPaste", dataList);
                }

                // 如果选项被改变，则恢复原选项
                if (isChangeOpt) {
                    rootElement.DocumentOptions.BehaviorOptions.AutoClearTextFormatWhenPasteOrInsertContent = oldCaretOption;
                    rootElement.ApplyDocumentOptions();
                }
            };
            listBox.appendChild(item);
        }
        divContainer.appendChild(listBox);
        // 下拉框展示
        divContainer.ShowDropdown();
    },

    TextEditKeyDownFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //兼容FocusNextInput接口，键盘右键离开时间输入域，收起时间下拉
        var cxcalendar = rootElement.querySelectorAll('.cxcalendar.show')[0];
        if (cxcalendar) {
            cxcalendar.classList.remove('show');
            cxcalendar.removeAttribute('style');
        }

        // //关闭表格下拉输入域
        // var dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
        // if (dropdownTable && dropdownTable.CloseDropdownTable) {
        //     dropdownTable.CloseDropdownTable();
        // }

        //在此处处理按键事件
        if (rootElement != null && rootElement.ondocumentkeydown != null && typeof rootElement.ondocumentkeydown == 'function') {
            e['handle'] = true;//增加一个属性，用于判断keydown事件是否继续向下执行，用于兼容tab键返回上一个输入域。
            rootElement.ondocumentkeydown(e);
        };
        //false：不执行，true：继续向下执行
        if (e.handle === false) {
            return;
        }

        if (e && e.code && e.code === 'F2') {
            //f2时创建知识库
            if (WriterControl_KnowledgeBase && WriterControl_KnowledgeBase.CreateKnowledgeBase) {
                WriterControl_KnowledgeBase.CreateKnowledgeBase(rootElement);
            }
        }
        //[DUWRITER5_0-2966]20240624 lxy客户想在有下拉框的情况下依旧可以键盘输入，增加了一个属性控制键盘输入
        ////判断是否为存在下拉框,如果存在调用禁止其他操作
        var hasList = WriterControl_ListBoxControl.ChangeListBoxCheck(rootElement, e);

        if (hasList && e.key != "Tab") {

            //DCDorpDownAllowKeyBoardEntry判断是否允许键盘输入,默认值为false，不允许键盘输入
            var DCDorpDownAllowKeyBoardEntry = rootElement.getAttribute('DCDorpDownAllowKeyBoardEntry') || 'false';
            DCDorpDownAllowKeyBoardEntry = DCDorpDownAllowKeyBoardEntry.toLowerCase().trim() === 'true' || DCDorpDownAllowKeyBoardEntry === true;


            if (DCDorpDownAllowKeyBoardEntry === false || DCDorpDownAllowKeyBoardEntry === "false") {
                e.preventDefault();
                e.stopPropagation();
                return false;
            }
        };


        //if (!('ontouchstart' in document.documentElement)) {
        //    //判断是否为ctrl+c  ctrl+x单独控制是为了能正确触发copy和cut
        //    if (e.ctrlKey && (e.keyCode == 88 || e.keyCode == 67)) {
        //        this.setAttribute('type', 'text');
        //    } else {
        //        //判断是否为只读状态禁止输入法
        //        var isReadonly = rootElement.Readonly;
        //        //判断是否为元素只读
        //        if (isReadonly && this.getAttribute('type') != 'password') {
        //            this.setAttribute('type', 'password');
        //            return;
        //        } else if (!isReadonly && this.getAttribute('type') != 'text') {
        //            this.setAttribute('type', 'text');
        //        }
        //    }
        //}

        //if (e.keyCode == 8 || e.keyCode == 46) {
        //    var selectionHtml = rootElement.SelectionHtml();
        //    //获取到选中的html，解析是否存在视频文件
        //    var hasVideoDiv = document.createElement('div');
        //    hasVideoDiv.innerHTML = selectionHtml;
        //    var hasVideo = hasVideoDiv.querySelectorAll('video');
        //    if (hasVideo && hasVideo.length > 0) {
        //        //查找所有的vedio元素
        //        for (var video = 0; video < hasVideo.length; video++) {
        //            var thisId = hasVideo[video].id;
        //            WriterControl_UI.DeleteId = WriterControl_UI.DeleteId ? WriterControl_UI.DeleteId : [];
        //            WriterControl_UI.DeleteId.push(thisId);
        //        }
        //    }
        //}
        if (e.shiftKey == true && e.ctrlKey == false && e.altKey == false && e.keyCode == 123) {
            // shift + F12 触发执行编辑器命令对话框
            return rootElement.DCExecuteCommandDialog();
        };
        /** 是否启动快速辅助录入模式 */
        // var FastInputMode = false;
        // if (rootElement && rootElement.DocumentOptions && rootElement.DocumentOptions.BehaviorOptions) {
        //     FastInputMode = rootElement.DocumentOptions.BehaviorOptions.FastInputMode;
        // }
        // if (FastInputMode && hasList != true && e.shiftKey == false && e.ctrlKey == false && e.altKey == false) {
        //     if (e.keyCode == 13 || e.keyCode == 9) {
        //         //获取输入域
        //         var currentInput = rootElement.CurrentInputField();
        //         if (currentInput) {
        //             /** 是否移动光标 */
        //             var needChangeFocus = false;
        //             // 获取输入域
        //             var thisAttr = rootElement.GetElementProperties(currentInput);
        //             if (thisAttr) {
        //                 needChangeFocus = (e.keyCode == 13 && thisAttr.MoveFocusHotKey == "Enter") || (e.keyCode == 9 && thisAttr.MoveFocusHotKey == "Tab");
        //             }
        //             if (needChangeFocus) {
        //                 //移动光标到下一个输入域
        //                 var nextInput = rootElement.GetNextFocusFieldElement(currentInput);
        //                 if (nextInput) {
        //                     rootElement.FocusElement(nextInput);
        //                 }
        //                 e.stopPropagation();
        //                 e.preventDefault();
        //                 e.returnValue = false;
        //                 return;
        //             }
        //         }
        //     }
        // };
        if (e.shiftKey == false && e.ctrlKey == true && e.altKey == false && e.keyCode == 70) {
            // ctrl + F 触发查找替换对话框
            rootElement.SearchAndReplaceDialog();
        };
        if (e.shiftKey == false && e.ctrlKey == true && e.altKey == false && (e.keyCode == 81 || e.keyCode == 82 || e.keyCode == 85)) {
            // (ctrl + R || ctrl + Q ||Ctrl+U) 禁止触发默认事件
            e.stopPropagation();
            e.preventDefault();
            e.returnValue = false;
        };
        if (e.ctrlKey === true && e.keyCode == 88) {
            //无法主动触发oncut事件只能先监听keydown手动调用
            rootElement.ownerDocument.execCommand('cut');
        };

        if (e.keyCode != 229) {
            var ref9 = this.__DCWriterReference;
            rootElement.EditorNoObtainFocus = 'keydownochangecursor';
            if (ref9 != null && ref9.invokeMethod(
                "EditorHandleKeyEvent",
                "keydown",
                e.altKey,
                e.shiftKey,
                e.ctrlKey,
                e.code,
                e.key) == true) {
                if (e.keyCode == 9) {
                    // 处理Tab键,
                    ref9.invokeMethod("EditorHandleKeyPress", "\t");
                }
                e.stopPropagation();
                e.preventDefault();
                e.returnValue = false;
                //return false;
            }
        };

        if (e.key == "F12" || e.key == "Control") {
            // 不处理一些特殊键
            return;
        };
    },
    TextEditKeyPressFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        // console.log('@@KeyPressFunc',rootElement);
        //在此处处理按键事件
        if (rootElement != null && rootElement.ondocumentkeypress != null && typeof rootElemenondocumentkeypress == 'function') {
            rootElement.ondocumentkeypress(e);
        }
        rootElement.EditorNoObtainFocus = 'keydownochangecursor';
        //if (this.getAttribute('type') == 'password') {
        //    e.stopPropagation();
        //    e.preventDefault();
        //    e.returnValue = false;
        //    return;
        //}
        var newText = e.key;
        var hasFullWidthSpace = rootElement.getAttribute("fullwidthspace");
        //处理空格问题
        if (e.keyCode == 32 && (e.shiftKey == true || typeof hasFullWidthSpace == "string" && hasFullWidthSpace.toLowerCase().trim() == "true")) {
            //替换文本为全角空格
            newText = "　 ";
        }
        if (e.keyCode != 229) {
            var ref9 = this.__DCWriterReference;
            //var oldFontName = null;
            //判断是否藏文
            //if (new RegExp("[\u0F00-\u0FFF]+").test(e.key)) {
            //    oldFontName = rootElement.CurrentFontName;
            //    rootElement.DCExecuteCommand('FontName', false, 'Microsoft Himalaya');
            //}
            if (ref9 != null && ref9.invokeMethod(
                "EditorHandleKeyPress",
                newText) == true) {
                e.stopPropagation();
                e.preventDefault();
                e.returnValue = false;
                //if (oldFontName) {
                //    rootElement.DCExecuteCommand('FontName', false, oldFontName);
                //}
                return false;
            }
        }

    },
    TextEditKeyUpFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //let rootElement=DCTools20221228.GetOwnerWriterControl(e.currentTarget._rootElementId)
        // console.log('@@KeyUpFunc',rootElement);

        var isReadonly = rootElement.Readonly;
        if (isReadonly) {
            var divCaret = rootElement.querySelector('#divCaret20221213');
            divCaret.style.display = 'none';
            e.stopPropagation();
            e.preventDefault();
            e.returnValue = false;
            return;
        }

        //在此处处理按键事件
        if (rootElement != null && rootElement.ondocumentkeyup != null && typeof rootElement.ondocumentkeyup == 'function') {
            rootElement.ondocumentkeyup(e);
        }

        if (e.key == "F12" || e.key == "Control") {
            // 不处理一些特殊键
            return;
        }
        //if (DotNet.invokeMethod(
        //    window.EntryPointAssemblyName,
        //    "EditorHandleKeyEvent",
        //    "keyup",
        //    e.altKey,
        //    e.shiftKey,
        //    e.ctrlKey,
        //    e.key,
        //    e.keyCode) == true)
        {
            e.stopPropagation();
            e.preventDefault();
            e.returnValue = false;
            return false;
        }

    },
    TextEditCompositionStartFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //let rootElement=DCTools20221228.GetOwnerWriterControl(e.currentTarget._rootElementId) 
        //添加属性用于控制是否触发了中文事件
        e.target.hasCompositionstart = true;
        let divCaret = rootElement.querySelector('#divCaret20221213');
        this.value = "";
        //显示文本域
        this.style.opacity = '1';
        divCaret.style.display = 'none';
    },

    TextEditCompositionUpdateFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //let rootElement=DCTools20221228.GetOwnerWriterControl(e.currentTarget._rootElementId) 
        //判断文本是否为空值,如果是将模拟的输入域隐藏
        if (e && e.data == "") {
            //清空输入域的文本
            this.value = null;
            this.style.width = '0px';
            this.style.padding = "0px";
            //隐藏文本域
            this.style.opacity = '0';
            //删除计算宽度的span
            //创建元素计算输入文本长度
            var updateWidthCalc = rootElement.ownerDocument.getElementById('dc_updateWidthCalc');
            if (updateWidthCalc != null) {
                updateWidthCalc.parentNode.removeChild(updateWidthCalc);
            }
            //显示模拟光标
            let divCaret = rootElement.querySelector('#divCaret20221213');
            divCaret.style.removeProperty('display');
            //结束中文事件
            this.hasCompositionstart = false;
            return;
        }
        //创建元素计算输入文本长度
        var updateWidthCalc = rootElement.ownerDocument.getElementById('dc_updateWidthCalc');
        //判断是否已存在
        if (updateWidthCalc == null) {
            updateWidthCalc = rootElement.ownerDocument.createElement('span');
        }
        updateWidthCalc.setAttribute('id', 'dc_updateWidthCalc');
        //确保字体和文档显示一样
        updateWidthCalc.style.cssText = 'position:absolute; word-wrap: normal;left:         -10000px;font-family:' + rootElement.CurrentFontName + ';font-size:' + rootElement.CurrentFontSize + 'pt;';
        let pageContainer = WriterControl_UI.GetPageContainer(rootElement);
        pageContainer.appendChild(updateWidthCalc);
        //判断currentTextline中的文本长度              
        updateWidthCalc.innerText = e.data;
        var spanWidth = updateWidthCalc.offsetWidth + parseFloat(this.style.height);
        //修改文本域的宽度
        //this.style.width = spanWidth + 'px';
        //this.style.padding = "3px";
        //updateWidthCalc.parentNode.removeChild(updateWidthCalc)
        //微软输入法最后会把文本写在光标的落点,这样写会导致文本突然的出现
        if (rootElement.oldCaretOption.bolReadonly) {
            this.style.width = '0px';
            this.value = "";
        } else {
            //修改文本域的宽度
            this.style.width = spanWidth + 'px';
            this.style.padding = "3px";
        }
    },
    TextEditCompositionEndFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //let rootElement=DCTools20221228.GetOwnerWriterControl(e.currentTarget._rootElementId) 

        if (e.data == "") {
            e.stopPropagation();
            e.preventDefault();
            return false;
        }
        //由于存在微软输入法在正常输入后鼠标点击其他位置会造成文本和实际输入不一致的问题
        if (e.data != this.value && this.value.indexOf(e.data) == 0) {
            var reg = new RegExp(e.data);
            this.value = this.value.replace(reg, "");
        }
        //此处跟新写法，当存在中文’的时候直接清掉文本
        if (this.value.indexOf("'") >= 0) {
            this.value = "";
        }
        //将中文插入到当前选中行中
        var txt = this.value;
        var newTXT = '';
        //对生僻字进行判断
        for (var char = 0; char < txt.length; char++) {
            newTXT += DCTools20221228.changeUseUTF16(txt[char]);
        }
        rootElement.EditorNoObtainFocus = 'keydownochangecursor';
        //console.log(txt)
        var ref9 = this.__DCWriterReference;
        if (ref9 != null) {
            ref9.invokeMethod(
                "EditorInsertStringFromUI",
                newTXT);
        }
        //清空输入域的文本
        this.value = null;
        this.style.width = '0px';
        this.style.padding = "0px";
        //隐藏文本域
        this.style.opacity = '0';
        //删除计算宽度的span
        //创建元素计算输入文本长度
        var updateWidthCalc = rootElement.ownerDocument.getElementById('dc_updateWidthCalc');
        if (updateWidthCalc != null) {
            updateWidthCalc.parentNode.removeChild(updateWidthCalc);
        }
        let divCaret = rootElement.querySelector('#divCaret20221213');
        //显示模拟光标
        divCaret.style.removeProperty('display');
        //结束中文事件
        this.hasCompositionstart = false;
    },
    TextEditInputFunc: function (e) {

        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }

        // //[DUWRITER5_0-2966] 20250210 lxy 修复输入法输入中文时，不能被禁用的问题
        // //DCDorpDownAllowKeyBoardEntry判断是否允许键盘输入,默认值为false，不允许键盘输入
        // var DCDorpDownAllowKeyBoardEntry = rootElement.getAttribute('DCDorpDownAllowKeyBoardEntry') || 'false';
        // DCDorpDownAllowKeyBoardEntry = DCDorpDownAllowKeyBoardEntry.toLowerCase().trim() === 'true' || DCDorpDownAllowKeyBoardEntry === true;
        // if (this.hasCompositionstart && !DCDorpDownAllowKeyBoardEntry) {
        //     const chineseRegex = /[\u4e00-\u9fa5]/;
        //     if (chineseRegex.test(e.data)) {
        //         e.stopPropagation();
        //         e.preventDefault();
        //         e.returnValue = false;
        //         this.value = '';
        //         return false;
        //     }
        // }

        //let rootElement=DCTools20221228.GetOwnerWriterControl(e.currentTarget._rootElementId) 
        //判读是否为只读
        let isReadonly = rootElement.Readonly;
        // 回车时判断是否下拉展示,如果回车让输入域下拉展示时，不应该在编辑器插入换行
        // 修复Enter键无法直接触发下拉选择项的问题【DUWRITER5_0-3296】
        let DropdownControlVisibleEnter = e.inputType == 'insertLineBreak' && WriterControl_UI.IsDropdownControlVisible() == true;
        if (!isReadonly && e.inputType != 'insertFromDrop' && DropdownControlVisibleEnter == false) {
            if (!this.hasCompositionstart) {
                let txt = this.value;
                let newTXT = '';
                //对生僻字进行判断
                for (var char = 0; char < txt.length; char++) {
                    newTXT += DCTools20221228.changeUseUTF16(txt[char]);
                }
                rootElement.EditorNoObtainFocus = 'keydownochangecursor';
                //console.log(txt)
                let ref9 = this.__DCWriterReference;
                if (ref9 != null) {
                    ref9.invokeMethod(
                        "EditorInsertStringFromUI",
                        newTXT);
                }
                //清空输入域的文本
                this.value = null;
            }
        }
    },
    /**
     * 文本编辑文本框焦点处理函数
     * 当文本编辑区域获得焦点时，确保光标可见，确保编辑器浏览器标签页切换回来时光标依然显示【DUWRITER5_0-3436】
     * @param {Event} e - 焦点事件对象
     */
    TextEditFocusFunc: function (e) {
        // 检查事件对象及其当前目标是否存在,确保this是文本编辑文本框
        if (!e || !this || this.nodeName != "TEXTAREA" || this.id != "txtEdit20221213") {
            return;
        }
        // 根据当前目标的根元素ID获取根元素
        let rootElement = DCTools20221228.GetOwnerWriterControl(this);
        // 如果根元素不存在，则直接返回
        if (!rootElement) {
            return;
        }
        // // 定义光标DIV的ID
        // let strDIVID = "divCaret20221213";
        // // 在根元素中获取光标DIV元素
        // let divCaret = rootElement.querySelector("#" + strDIVID);
        // // 判断光标是否已经显示
        // let isShowCaret = divCaret && divCaret.style.display != "none";
        // // 如果光标未显示且根元素存在Focus方法，则调用该方法
        // if (isShowCaret === false && rootElement.Focus && typeof (rootElement.Focus) === "function") {
        //     rootElement.Focus();
        // }
        if (rootElement.oldCaretOption && WriterControl_UI && typeof (WriterControl_UI.ShowCaret) == "function") {
            // 修改抢光标问题，涉及问题为【DUWRITER5_0-3825，DUWRITER5_0-3806，DUWRITER5_0-3801，DUWRITER5_0-3684】
            // 避免重复调用，暂时解除事件的绑定
            this.removeEventListener("focus", WriterControl_UI.TextEditFocusFunc);
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
                "TextEditFocusFunc"
            );
            this.addEventListener("focus", WriterControl_UI.TextEditFocusFunc);
        }
    },
    /**
     * 当文本编辑区域失去焦点时触发的函数
     * @param {Event} e - 事件对象，包含事件相关信息
     */
    TextEditBlurFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //let rootElement=DCTools20221228.GetOwnerWriterControl(e.currentTarget._rootElementId) 
        WriterControl_UI.HideCaret(rootElement);

        //DUWRITER5_0-3309增加编辑器失去焦点事件
        if (rootElement && rootElement.EventWriterControlBlur && typeof rootElement.EventWriterControlBlur === 'function') {
            rootElement.EventWriterControlBlur.call(rootElement);
        }

        //编辑器丢失焦点同时清空选中
        //if (rootElement.__DCWriterReference.invokeMethod("HasSelection") === true) {
        //    rootElement.FocusElement(rootElement.CurrentElement("xtextcontainerelement"));
        //}
        let activeEle = rootElement.ownerDocument.hasFocus();
        //在此处处理关闭下拉操作
        if (!activeEle) {
            let dropdownDiv = rootElement.querySelector('#divDropdownContainer20230111');
            if (dropdownDiv) {
                dropdownDiv.CloseDropdown();
            }
            //关闭表格下拉输入域
            let dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
            if (dropdownTable && dropdownTable.CloseDropdownTable) {
                dropdownTable.CloseDropdownTable();
            }

        }
        if (DCTools20221228.IsReadonlyAutoFocus(rootElement, true) == false) {
            if (!rootElement.LoadRemoveChild && this) {
                try {
                    // this.remove();
                } catch (err) { }

            }
        }
    },
    TextEditPasteFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //let rootElement = DCTools20221228.GetOwnerWriterControl(e.currentTarget.parentElement) 
        rootElement.isPasteAsText = false;
        WriterControl_UI.GetClipboardData(e, rootElement);
        e.stopPropagation();
        e.preventDefault();
        e.returnValue = false;
        return false;
    },
    PageContainerOnWheelFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //let rootElement = window.DCTools20221228.GetOwnerWriterControl(e.currentTarget.parentElement)
        let innerDiv = e.currentTarget;
        //在pc端如果存在下拉框则禁止页面滚动
        if (!('ontouchstart' in rootElement.ownerDocument.documentElement)) {
            let dropdown = rootElement.querySelector('#divDropdownContainer20230111');
            let srcElement = e.srcElement || e.target;
            if (dropdown != null && dropdown.style.display != 'none' && !(dropdown == srcElement || dropdown.contains(srcElement))) {
                e.stopPropagation();
                e.preventDefault();
                return false;
            }

        }
        if (innerDiv.style.whiteSpace != null
            && innerDiv.style.whiteSpace.toLocaleLowerCase() == "nowrap"
            && e.ctrlKey == false) {
            // X,Y滚动量置换
            innerDiv.scrollBy(e.deltaY, e.deltaX);
            e.stopPropagation();
            e.preventDefault();
        }
    },
    PageContainerOnscrollFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        if (rootElement.IsWriterPrintPreviewControlForWASM === true) {
            //如果是打印预览则只需要处理懒加载
            console.log("如果是打印预览则需要处理懒加载");
            var DivRect;
            // 此处修改nextSibling为nextElementSibling处理nextSibling为text的情况
            for (var element = this.firstChild; element != null; element = element.nextElementSibling) {
                var lowNodeName = element.nodeName.toLowerCase();
                if (lowNodeName == "svg" && element.getAttribute("dctype") == "page") {
                    // 已经渲染完成的无需重新渲染
                    // console.log(element._isRendered, '=============element._isRendered == true');                 
                    if (element._isRendered == true) {
                        continue;
                    }
                    // 获取打印预览盒子的DOMRect对象
                    if (DivRect == null) {
                        DivRect = this.getBoundingClientRect();
                    }
                    // 获取元素的DOMRect对象
                    var PageRect = element.getBoundingClientRect();
                    if (DivRect.top > PageRect.bottom || DivRect.bottom < PageRect.top) {
                        // 不在可视区域中
                        continue;
                    }
                    // 【?】目前会被渲染卡一下
                    WriterControl_Print.InnerDrawOnePage(element, true);

                    element._isRendered = true;
                }
            }
            // WriterControl_Rule.HandleViewScroll(this);
        } else {
            //let rootElement = window.DCTools20221228.GetOwnerWriterControl(e.currentTarget.parentElement)
            let div = e.currentTarget;
            WriterControl_UI.TableHeaderFixed(rootElement);

            WriterControl_Paint.AddTaskHandleScrollView(rootElement);
            // WriterControl_Paint.HandleScrollView(rootElement);
            WriterControl_Rule.HandleViewScroll(this);

            var dropdown = rootElement.querySelector('#divDropdownContainer20230111');

            //当rootElement尺寸发生改变时.关闭下拉
            if (dropdown != null && dropdown.style.display != 'none') {
                if ('ontouchstart' in rootElement.ownerDocument.documentElement) {
                    dropdown.CloseDropdown();
                }
            }

            //如果存在知识库页面滚动时则关闭知识库
            var divknowledgeBase = rootElement.querySelector('#divknowledgeBase20240103');
            if (divknowledgeBase && divknowledgeBase.DistroyKnowledgeBase) {
                divknowledgeBase.DistroyKnowledgeBase();
            }

            // //关闭表格下拉输入域
            // var dropdownTable = rootElement.querySelector(`#DCTableControl20240625151400`);
            // if (dropdownTable && dropdownTable.CloseDropdownTable) {
            //     dropdownTable.CloseDropdownTable();
            // }


            //if (caret != null) {
            //    caret.style.display = 'none';
            //    clearInterval(caret.handleTimer)
            //}
            //获取到scrollTop
            //当前滚动的juli
            if (rootElement.GetPageNumberForView && typeof rootElement.GetPageNumberForView == 'function') {
                var scrollTop = div.scrollTop;
                //可视区域高度
                var visibleAreaHeight = div.clientHeight;
                var allCanvas = null;
                if (rootElement.IsWriterPrintPreviewControlForWASM) {
                    allCanvas = div.querySelectorAll('svg');
                } else {
                    allCanvas = div.querySelectorAll('canvas');
                }
                var maxIndex = 0;
                var maxHeight = 0;
                var maxCanvas = allCanvas[0];

                for (var i = 0; i < allCanvas.length; i++) {
                    var thisCanvas = allCanvas[i];
                    if (thisCanvas) {
                        //获取到这个canvas的位置  当多少位置时可以被看见
                        var thisTop = thisCanvas.offsetTop - 5;
                        var thisHeight = thisCanvas.clientHeight;
                        //判断是否进入可视区域
                        if (thisTop <= scrollTop + visibleAreaHeight && thisTop + thisHeight >= scrollTop) {
                            var overTop = (scrollTop - thisTop) > 0 ? (scrollTop - thisTop) : 0;
                            var overBottom = (thisTop + thisHeight - (scrollTop + visibleAreaHeight)) > 0 ? (thisTop + thisHeight - (scrollTop + visibleAreaHeight)) : 0;
                            //算出可视区域高度
                            var thisVisible = thisHeight - overTop - overBottom;
                            if (thisVisible >= maxHeight) {
                                maxHeight = thisVisible;
                                maxIndex = thisCanvas.PageIndex;
                                maxCanvas = thisCanvas;
                            }
                        }
                    }
                }
                rootElement.GetPageNumberForView(maxIndex + 1, maxCanvas);
            }
        }
    },

    TableHeaderFixed: function (rootElement, div) {
        //如果是普通视图模式
        if (rootElement.ViewMode == "CompressPage") {
            //判断是否存在div
            if (div == null) {
                div = rootElement.PageContainer;
            }
            //判断是否存在表头固定展示的方法
            var hasTableheaderfixed = rootElement.getAttribute("tableheaderfixed");
            //存在表格头固定属性
            if (typeof hasTableheaderfixed == "string" && hasTableheaderfixed.toLowerCase().trim() == "true") {
                var allTable = rootElement.__DCWriterReference.invokeMethod("GetTablesForFixedHeader");
                if (allTable && allTable.length > 0) {
                    //获取页面滚动位置
                    var allCanvas = div.querySelectorAll("[dctype=page]");
                    //计算高度
                    var zoom = rootElement.GetZoomRate();
                    var scrollTop = div.scrollTop;
                    var tableTop = allTable[0].TopInOwnerPage / 300 * 96.00001209449;
                    var tableTop2 = tableTop + allCanvas[0].offsetTop * zoom;
                    var tableLeft = allTable[0].LeftInOwnerPage / 300 * 96.00001209449;

                    if (scrollTop >= tableTop2) {
                        //获取到当前表格所有的表格头
                        var allRow = allTable[0].Rows;
                        var minCell = null;
                        var maxLeft = 0;
                        var maxCell = null;
                        //计算开始和结束的cell的位置
                        for (var j = 0; j < allRow.length; j++) {
                            var thisRow = allRow[j];
                            thisRow = rootElement.GetElementProperties(thisRow);
                            if (thisRow.HeaderStyle === true) {
                                var thisRowCell = thisRow.Cells;
                                //获取左边距
                                if (j == 0) {
                                    minCell = thisRowCell[0];
                                }
                                var thisCell = rootElement.GetElementProperties(thisRowCell[thisRowCell.length - 1]);
                                if (thisCell.LeftInOwnerPage >= maxLeft && !thisCell.IsOverrided) {
                                    maxLeft = thisCell.LeftInOwnerPage;
                                    maxCell = thisRowCell[thisRowCell.length - 1];
                                }
                            } else {
                                break;
                            }
                        }
                        if (!div.htmlDiv) {
                            div.htmlDiv = document.createElement("div");
                            div.htmlDiv.style.position = "absolute";
                            div.htmlDiv.style.backgroundColor = "#fff";
                            div.insertAdjacentElement("beforeend", div.htmlDiv);
                        }
                        if (div.minCell != minCell && div.maxCell != maxCell) {
                            rootElement.SelectContentByStartEndElement(minCell, maxCell);
                            div.minCell = minCell;
                            div.maxCell = maxCell;
                            div.htmlDiv.innerHTML = rootElement.SelectionHtml();

                            rootElement.MoveEnd();
                            //找点所有的非table，删除
                            var allChild = div.htmlDiv.childNodes;
                            for (var z = 0; z < allChild.length; z++) {
                                if (allChild[z].nodeName != "TABLE") {
                                    allChild[z].remove();
                                    z--;
                                }
                            }
                            var allTd = div.htmlDiv.querySelectorAll("td");
                            for (var z = 0; z < allTd.length; z++) {
                                allTd[z].style.border = "1px solid #000";
                                allTd[z].style.textAlign = "center";
                            }


                        }
                        div.htmlDiv.style.top = scrollTop + 'px';
                        div.htmlDiv.style.left = (tableLeft * zoom) + allCanvas[0].offsetLeft + 0.5 + 'px';
                        if (div.htmlDiv.childNodes) {
                            div.htmlDiv.childNodes[0].style.zoom = zoom;
                        }
                    } else {
                        if (div.htmlDiv) {
                            div.htmlDiv.remove();
                            div.htmlDiv = null;
                            div.minCell = null;
                            div.maxCell = null;
                        }
                    }


                }
            }

        }
    },

    PageContainerOnresizeFunc: function (e) {
        if (!e || !e.currentTarget || !e.currentTarget._rootElementId) {
            return;
        }
        let rootElement = e.currentTarget.ownerDocument.getElementById(e.currentTarget._rootElementId);
        if (!rootElement) {
            return;
        }
        //let rootElement = window.DCTools20221228.GetOwnerWriterControl(e.currentTarget.parentElement)
        WriterControl_Rule.HandleViewScroll(this);

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
    },
    /**
     * 处理文档体鼠标点击事件
     * 当用户在文档体上点击时，此函数会被调用
     * 主要功能是关闭可能打开的下拉菜单，并处理一些特定情况下的逻辑
     * 
     * @param {Object} e - 事件对象，包含鼠标点击的相关信息
     */
    OwnerDocumentBodyMouseDownFunc: function (e) {
        // this == body
        // 获取所有属于WriterControl的WASM的div元素
        var AllWASMDiv = this.querySelectorAll("[dctype='WriterControlForWASM']");
        // 获取事件源元素，兼容不同浏览器
        var srcElement = e.srcElement || e.target;
        var rootElement = DCTools20221228.GetOwnerWriterControl(srcElement);
        if (rootElement) {
            this.ownerDocument.WriterControl = rootElement;
        }
        // 遍历所有找到的WASM div元素
        for (var i = 0; i < AllWASMDiv.length; i++) {
            var wasmDiv = AllWASMDiv[i];
            // 查找是否存在下拉菜单的容器
            var dropdown = wasmDiv.querySelector('#divDropdownContainer20230111');
            // 当存在下拉菜单时执行关闭下拉的逻辑
            if (dropdown != null) {
                // 特殊情况处理：当下拉菜单是时间选择器时，点击其内部元素不关闭下拉
                if (dropdown.thisApi && dropdown.thisApi.theTool && dropdown.thisApi.theTool.dom) {
                    // 判断点击的元素是否为时间选择器的遮罩层，如果是则关闭下拉
                    if (dropdown.thisApi.theTool.dom.maskBg == srcElement) {
                        dropdown.CloseDropdown();
                    }
                } else {
                    // 非特殊情况，直接关闭下拉菜单
                    dropdown.CloseDropdown();
                }
            }
            // 修复鼠标点击滚动条时编辑器光标丢失问题【DUWRITER5_0-3436】
            // 鼠标点击时，不需要隐藏光标
            // WriterControl_UI.HideCaret(wasmDiv);
        }
    }
};

// // 检查 DCTools20221228 和 WriterControl_UI 是否存在，并且它们的相关函数是否为函数类型
// if (DCTools20221228 && typeof DCTools20221228.DebounceFn === "function" &&
//     WriterControl_UI && typeof WriterControl_UI.TextEditFocusFunc === "function") {
//     // 使用 Debounce 函数优化 TextEditFocusFunc 函数
//     WriterControl_UI.DebounceTextEditFocusFunc = DCTools20221228.DebounceFn(WriterControl_UI.TextEditFocusFunc, 300);
// }

/** 编辑器控件的扩展包装 (内部使用)*/
export class WriterControlExtPackage {
    constructor(rootElement, baseInstance) {
        this.__RootElement = rootElement;
        this.__BaseInstance = baseInstance;
        this._Logging = false;
        this.__Records = new Array();
    }
    LogStaticMethod(e, ...t) {
    }
    /**
     * 输出调试文本信息
     * @param {string} msg 文本信息
     */
    DebugPrint(msg) {
        var record = new Object();
        record.Index = this.__Records.length;
        record.Tick = new Date().valueOf() - this._StartLogTick;
        record.Name = "#DebugPrint";
        record.Message = msg;
        this.__Records.push(record);
    }
    invokeMethod(e, ...t) {
        if (e == "PlayAPILogRecords") {
            // 正在记录API，则不再添加新记录
            return this.__BaseInstance.invokeMethod(e, ...t);
        }
        else {
            this._Logging = true;
            var objResult = null;
            try {
                var tick = new Date().valueOf();
                this.__BaseInstance.invokeMethod("ClearLastErrorMessage");
                objResult = this.__BaseInstance.invokeMethod(e, ...t);
                var strErrorMessage = this.__BaseInstance.invokeMethod("GetLastErrorMessage");
                tick = new Date().valueOf() - tick;
                if (this.__Records == null) {
                    this.__Records = [];
                }
                if (this.__Records.length == 0) {
                    // 第一个记录，记下一些状态信息
                    var info = new Object();
                    info.Format = "20231023";
                    info.DCWriterVersion = this.__RootElement.getAttribute("dcversion");
                    info.ControlID = this.__RootElement.id;
                    info.DevicePixelRatio = window.devicePixelRatio;
                    info.FullScreen = window.fullScreen;
                    info.WindowSize = window.innerWidth + "*" + window.innerHeight;
                    info.IsInFrame = window.frameElement != null;
                    info.Location = window.location;
                    info.Name = window.name;
                    info.Navigator = window.navigator.userAgent;
                    info.Screen = window.screen.width + "*" + window.screen.height;
                    info.Title = document.title;
                    info.StartTime = new Date();
                    this.__Records.push(info);
                    this._StartLogTick = new Date().valueOf();
                    if (this.FromBuildControl != true) {
                        // 不是从创建控件的时候开始调用的，则记录编辑器属性
                        var attrs = this.__RootElement.attributes;
                        for (var attrIndex = 0; attrIndex < attrs.length; attrIndex++) {
                            var attrItem = attrs[attrIndex];
                            var record2 = new Object();
                            record2.Tick = 0;
                            record2.Name = "LoadConfigByHtmlAttribute";
                            record2.TickSpan = 0;
                            record2.Args = [attrItem.localName, attrItem.nodeValue];
                            this.__Records.push(record2);
                        }//for
                        var opts = this.__BaseInstance.invokeMethod("GetDocumentOptions");
                        this.__Records.push(
                            {
                                Tick: 0,
                                Name: "ApplyDocumentOptions",
                                TickSpan: 0,
                                Args: [opts]
                            }
                        );
                        var strXmlContent = this.__BaseInstance.invokeMethod("SaveDocumentToString", "xml");
                        if (strXmlContent != null && strXmlContent.length > 0) {
                            this.__Records.push({
                                Tick: 0,
                                Name: "@LoadDocumentFromString@",
                                TickSpan: 0,
                                Content: strXmlContent
                            });
                        }
                    }
                }
                var record = new Object();
                record.RecordIndex = this.__Records.length;
                record.Tick = new Date().valueOf() - this._StartLogTick; // 执行的时刻
                record.Name = e; // 接口名称
                record.TickSpan = tick; // 接口耗时
                if (strErrorMessage != null && strErrorMessage.length > 0) {
                    record.Error = strErrorMessage; // 相关的错误信息
                }
                if (t != null && t.length > 0) {
                    var pArgs = new Array();
                    // 以下记录参数值
                    for (var iCount = 0; iCount < t.length; iCount++) {
                        var item = t[iCount];
                        if (DCTools20221228.IsDotnetReferenceElement(item)) {
                            // 参数是一个DOTNET对象引用，本API不记录。
                            return objResult;
                        }
                        if (item instanceof Uint8Array) {
                            pArgs.push("__Uint8Array:" + DCTools20221228.ToBase64String(item));
                        }
                        else {
                            pArgs.push(t[iCount]);
                        }
                    }
                    record.Args = pArgs;
                }
                var strResultType = typeof (objResult);
                if (strResultType != "undefined") {
                    if (strResultType == "boolean"
                        || strResultType == "number") {
                        record.Result = objResult;
                    }
                    else if (strResultType == "string") {
                        var len4 = objResult.length;
                        if (len4 > 30) {
                            record.Result = "[" + objResult.length + "]" + objResult.substring(0, 30);
                        }
                        else {
                            record.Result = objResult;
                        }
                    }
                    else {
                        record.ResultType = strResultType;
                    }
                }
                this.__Records.push(record);
            }
            finally {
                this._Logging = false;
            }
            return objResult;
        }
    }
    ///** 清空API日志数据 */
    //ClearAPIRecordData() {
    //    if (this.__Records != null) {
    //        this.__Records.splice(0, this.__Records.length);
    //    }
    //}
    ///**
    // * 下载API记录的数据到本地文件
    // */
    //DownloadAPIRecordData() {
    //    if (this.__Records == null) {
    //        return;
    //    }
    //    //console.log(this.__Records);
    //    var strJson = JSON.stringify(this.__Records);
    //    var obj2 = JSON.parse(strJson);
    //    //console.log(obj2);
    //    var blob = new Blob([strJson]);
    //    let downloadElement = document.createElement("a");
    //    let href = window.URL.createObjectURL(blob); //创建下载的链接
    //    downloadElement.href = href;
    //    downloadElement.charset = "utf-8";
    //    downloadElement.type = "text/json";
    //    downloadElement.download = "APIRecord" + (new Date().valueOf()) + ".json";
    //    document.body.appendChild(downloadElement);
    //    downloadElement.click(); //点击下载
    //    document.body.removeChild(downloadElement); //下载完成移除元素
    //    window.URL.revokeObjectURL(href); //释放掉blob对象
    //}
    invokeMethodAsync(e, ...t) {
        // 异步方法不记录
        return this.__BaseInstance.invokeMethodAsync(e, ...t);
    }
    dispose() {
        i(this.__Records != null);
        {
            delete this.__Records;
        }
        if (this.__BaseInstance != null) {
            this.__BaseInstance.dispose();
            this.__BaseInstance = null;
        }
        this.__RootElement = null;
    }
    serializeAsArg() {
        return this.__BaseInstance.serializeAsArg();
    }
}