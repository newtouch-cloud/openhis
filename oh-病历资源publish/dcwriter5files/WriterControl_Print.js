// 打印相关函数
"use strict";

import { DCTools20221228 } from "./DCTools20221228.js";
import { DCBinaryReader } from "./DCTools20221228.js";
import { PageContentDrawer } from "./PageContentDrawer.js";
import { WriterControl_Paint } from "./WriterControl_Paint.js";
import { WriterControl_Task } from "./WriterControl_Task.js";
import { WriterControl_Event } from "./WriterControl_Event.js";
import { WriterControl_UI } from "./WriterControl_UI.js";
import { WriterControl_Rule } from "./WriterControl_Rule.js";


/** 打印相关模块 */
export let WriterControl_Print = {
    /**
     * 保存本地PDF文件
     * @param {object } options 参数选项
     * @param {string | HTMLElement} [options.RootElement] 编辑器元素对象或者编号
     * @param {string} [options.FileName] 建议的文件名
     * @param {Function} [options.CallBack] 接送生成的文件的二进制内容的回调函数,如果为空则默认执行下载操作。
     * @param {string | Array } [options.DocumentsXml] 特别要指定的文档XML字符串或者数组，如果为空则采用当前编辑的文档。
     * @param { boolean } [options.ForceBlack ] 强制为纯黑色文档。
     * @param {boolean} [options.ForOFD] 为true输出OFD文件，为false输出PDF文件
     * @returns
     * @change ["2024-7-12","修改参数类型","yyf" ]
    */
    SaveLocalPDF: function (options) {// rootElement, strFileName, callBack, documentsXml, forceBlack = false) {
        if (options == null) {
            // 参数为空
            return;
        }
        var rootElement = DCTools20221228.GetOwnerWriterControl(options.RootElement);
        if (rootElement == null) {
            if (typeof (options.CallBack) == "function") {
                options.CallBack(null);
            }
            return;
        }
        var bolForOFD = options.ForOFD == true;
        var pdfTasks = rootElement.__DCWriterReference.invokeMethod("PrepareForSaveAsLocalPDF", options.DocumentsXml, bolForOFD);
        var taskResultValues = new Array();
        var startTime = new Date();
        function GenPDFFile() {
            var resultData = null;
            if (bolForOFD == true) {
                WriterControl_Event.InnerRaiseEvent(rootElement, "StatusTextChanged", "正在本地生成OFD文件[" + options.FileName + "]...");
            }
            else {
                WriterControl_Event.InnerRaiseEvent(rootElement, "StatusTextChanged", "正在本地生成PDF文件[" + options.FileName + "]...");
            }
            var bolForceBlack = options.ForceBlack;
            if (typeof (bolForceBlack) != "boolean") {
                bolForceBlack = false;
            }
            resultData = rootElement.__DCWriterReference.invokeMethod("SaveAsLocalPDF", taskResultValues, bolForceBlack, bolForOFD);
            if (resultData != null) {
                var ticks = new Date().valueOf() - startTime.valueOf();
                var strMsg = "DCWriter生成PDF/OFD文件大小" + DCTools20221228.FormatByteSize(resultData.length) + "，耗时" + ticks + "毫秒。";
                WriterControl_Event.InnerRaiseEvent(rootElement, "StatusTextChanged", strMsg);
                if (typeof options.CallBack == "function") {
                    //WriterControl_Event.InnerRaiseEvent(rootElement, "StatusTextChanged", "");
                    options.CallBack(resultData);
                }
                else {
                    DCTools20221228.DownloadAsFile(resultData, "application/pdf", options.FileName);
                }
            }
            else {
                WriterControl_Event.InnerRaiseEvent(rootElement, "StatusTextChanged", "DCWriter生成PDF失败。");
            }
        }
        if (pdfTasks == null) {
            // 没有任何要准备的，立刻生成PDF文件
            GenPDFFile();
        }
        else {
            // 定义执行任务的函数
            function ExecuteNextTask() {
                if (pdfTasks.length == 0) {
                    // 任务执行完毕
                    GenPDFFile();
                    return;
                }
                var curTask = pdfTasks.shift();
                if (typeof (curTask) == "string" && curTask.indexOf("data:image") == 0) {
                    // 图片格式转换为JPG格式
                    var img = new Image();
                    img.src = curTask;
                    img.onload = function () {
                        var canvas = document.createElement("canvas");
                        canvas.width = img.width;
                        canvas.height = img.height;
                        var ctx = canvas.getContext("2d");
                        ctx.fillStyle = "white";
                        ctx.fillRect(0, 0, canvas.width, canvas.height);
                        ctx.drawImage(img, 0, 0);
                        var dataUrl = canvas.toDataURL("image/jpeg", 1.0);
                        taskResultValues.push(dataUrl);
                        ExecuteNextTask();
                    };
                }
                else if (typeof (curTask) == "string" && curTask.indexOf("docback20240314#") == 0) {
                    // 创建文档背景图片
                    var index55 = curTask.indexOf(";");
                    var strIndex55 = curTask.substring(16, index55);
                    var docIndex55 = parseInt(strIndex55);
                    var strData55 = curTask.substring(index55 + 1);
                    var bsData5 = DCTools20221228.FromBase64String(strData55);

                    var reader = new DCBinaryReader(bsData5);
                    var pageWidth = reader.ReadInt32();
                    var pageHeight = reader.ReadInt32();
                    var tempElement = rootElement.ownerDocument.createElement("canvas");
                    tempElement.width = pageWidth;
                    tempElement.height = pageHeight;
                    tempElement.style.backgroundColor = "white";
                    var ctx4 = tempElement.getContext("2d");
                    ctx4.fillStyle = "white";
                    ctx4.fillRect(0, 0, tempElement.width, tempElement.height);

                    var drawer = new PageContentDrawer(tempElement, reader);
                    drawer.DocumentIndex = docIndex55;
                    drawer.EventAfterDraw = function () {
                        var strData3 = tempElement.toDataURL("image/jpeg", 1);
                        rootElement.__DCWriterReference.invokeMethod("SetPDFBackgroundImage", this.DocumentIndex, strData3);
                        tempElement.remove();
                        ExecuteNextTask();
                    };
                    drawer.AddToTask();
                }
                //else if (curTask == bgDatas){
                //    // 创建背景图片
                //    var reader = new DCBinaryReader(curTask);
                //    var pageWidth = reader.ReadInt32();
                //    var pageHeight = reader.ReadInt32();
                //    var tempElement = rootElement.ownerDocument.createElement("canvas");
                //    tempElement.width = pageWidth;
                //    tempElement.height = pageHeight;
                //    tempElement.style.backgroundColor = "white";
                //    var ctx4 = tempElement.getContext("2d");
                //    ctx4.fillStyle = "white";
                //    ctx4.fillRect(0, 0, tempElement.width, tempElement.height);

                //    var drawer = new PageContentDrawer(tempElement, reader);
                //    drawer.EventAfterDraw = function () {
                //        var strData3 = tempElement.toDataURL("image/jpeg", 1);
                //        rootElement.__DCWriterReference.invokeMethod("SetPDFBackgroundImage", strData3);
                //        tempElement.remove();
                //        ExecuteNextTask();
                //    };
                //    drawer.AddToTask();
                //}
                else {
                    function DownloadFontFile(fontTask) {
                        if (fontTask.FileName == null || fontTask.FileName.length == 0) {
                            //throw fontTask.Name +"|" + window.__DCSR.PromptNotSupportFont;
                            //return;
                            fontTask.FileName = "simsun_old.ttf";
                            fontTask.Name = "宋体";
                            fontTask.KeyValue = '宋体';
                        }
                        var strUrl = DCTools20221228.GetResourceUrl("fonts/" + fontTask.FileName);
                        var xhr = new XMLHttpRequest();
                        xhr.open("GET", strUrl, true);
                        xhr.responseType = "blob";
                        xhr.onload = function () {
                            if (this.status == 200) {
                                // 操作成功
                                var blob = this.response;
                                if (blob == null || blob.size < 100) {
                                    console.warn(window.__DCSR.FailToDownLoadFontFileForPDFOFD + strUrl);
                                    rootElement.__DCWriterReference.invokeMethod("CancelSaveAsLocalPDF");
                                }
                                else {
                                    DCTools20221228.blobToArrayBuffer(blob, buffer => {
                                        var buffer2 = new Uint8Array(buffer);
                                        DotNet.invokeMethod(
                                            window.DCWriterEntryPointAssemblyName,
                                            "SetFontFileContent",
                                            fontTask.KeyValue,
                                            fontTask.Name,
                                            buffer2);
                                        //尝试释放buffer
                                        buffer2 = null;
                                        buffer = null;
                                        ExecuteNextTask();
                                    });
                                }
                                //尝试释放blob
                                //debugger;
                                blob = null;
                            }
                        };
                        xhr.onerror = function (err) {
                            console.warn(window.__DCSR.FailToDownLoadFontFileForPDFOFD + strUrl);
                            rootElement.__DCWriterReference.invokeMethod("CancelSaveAsLocalPDF");
                        };
                        WriterControl_Event.InnerRaiseEvent(rootElement, "StatusTextChanged", "下载字体文件[" + strUrl + "]");
                        xhr.send();
                    }
                    // 下载字体文件
                    if (window.__LocalFontsErrorFlag != true && window.queryLocalFonts) {
                        try {
                            //var state = await navigator.permissions.query({
                            //    name: "local-fonts"
                            //});
                            //console.log(state)
                            //if (state.state == "prompt") {
                            //    WriterControl_Print.__LocalFontsErrorFlag = true;
                            //    //console.warn(ext);
                            //    ExecuteNextTask();
                            //} else {
                            // 尝试获取本地字体数据
                            window.queryLocalFonts().then(function (list2) {
                                var bolMatch = false;
                                if (list2.length > 0 && list2[0].blob) {
                                    for (const item2 of list2) {
                                        if (item2.family == curTask.Name || item2.fullName == curTask.Name) {
                                            var bolBold = item2.style.indexOf("Bold") >= 0 || item2.style.indexOf("Black") >= 0;
                                            var bolItalic = item2.style.indexOf("Italic") >= 0;
                                            if (curTask.Bold == bolBold && curTask.Italic == bolItalic) {
                                                bolMatch = true;
                                                WriterControl_Event.InnerRaiseEvent(rootElement, "StatusTextChanged", "读取字体数据[" + item2.fullName + "]");
                                                item2.blob().then((data) => {
                                                    DCTools20221228.blobToArrayBuffer(data, function (buf) {
                                                        var buffer2 = new Uint8Array(buf);
                                                        DotNet.invokeMethod(
                                                            window.DCWriterEntryPointAssemblyName,
                                                            "SetFontFileContent",
                                                            curTask.KeyValue,
                                                            curTask.Name,
                                                            buffer2);
                                                        ExecuteNextTask();
                                                    });
                                                });
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (bolMatch == false) {
                                    DownloadFontFile(curTask);
                                }
                            }).catch(function (ext2) {
                                window.__LocalFontsErrorFlag = true;
                                console.warn(ext2);
                                ExecuteNextTask();
                            });
                        }
                        catch (ext) {
                            window.__LocalFontsErrorFlag = true;
                            console.warn(ext);
                            ExecuteNextTask();
                        }
                    }
                    else {
                        // 通过网络下载字体文件
                        DownloadFontFile(curTask);
                    }
                }
            };
            ExecuteNextTask();
        }
    },

    /**
     * 下载PDF本地字体
     * 此函数用于分析给定XML文档中引用的所有字体，并将这些字体下载到本地，以确保在生成PDF时可以正确渲染文本。
     * 它接受一个根元素、一个回调函数和一个XML文档作为参数。
     * 
     * @param {Object} rootElement - XML文档的根元素，用于开始分析字体引用。
     * @param {function} callBack - 字体下载完成后调用的回调函数。
     * @param {string} documentsXml - 包含文档元数据和样式信息的XML字符串。
     */
    DownLoadFontsForLocalPDF: function (rootElement, callBack, documentsXml) {
        // 获取文档根元素，并通过特定方法处理，为后续操作准备必要的元素引用
        var rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        // 检查根元素是否获取成功，如果未成功，则终止后续操作
        if (rootElement == null) {
            return;
        }
        // 调用特定方法，准备将文档保存为本地PDF，返回可能包含字体任务的数组
        var pdfTasks = rootElement.__DCWriterReference.invokeMethod("PrepareForSaveAsLocalPDF", documentsXml, false);
        // 检查返回的数组是否有效，如果无效，则终止后续操作
        if (!pdfTasks || Array.isArray(pdfTasks) == false || pdfTasks.length == 0) {
            return;
        }
        // 过滤出所有有效的字体对象，这些对象必须是对象类型、非空、且具有Name属性
        var FontsArray = pdfTasks.filter(function (item) {
            return typeof (item) === "object" && item !== null && item.Name !== null;
        });

        // 遍历过滤后的字体数组，下载每个字体文件
        // 如果当前字体是数组中的最后一个，调用回调函数处理下载完成后的逻辑
        for (var i = 0; i < FontsArray.length; i++) {
            if (i == FontsArray.length - 1) {
                DownloadFontFile(FontsArray[i], callBack);
            } else {
                DownloadFontFile(FontsArray[i]);
            }
        }
        /**
         * 下载字体文件并设置到DotNet环境中。
         * @param {Object} fontTask - 字体任务对象，包含字体文件名、字体名称和键值。
         * @param {Function} ExecuteNextTask - 任务完成后的回调函数，可选。
         */
        function DownloadFontFile(fontTask, ExecuteNextTask) {
            // 检查字体文件名是否为空，如果为空，则默认为宋体。
            if (fontTask.FileName == null || fontTask.FileName.length == 0) {
                //throw fontTask.Name + "|" + window.__DCSR.PromptNotSupportFont;
                //return;
                fontTask.FileName = "simsun_old.ttf";
                fontTask.Name = "宋体";
                fontTask.KeyValue = '宋体';
            }
            // 构建字体文件的URL。
            var strUrl = DCTools20221228.GetResourceUrl("fonts/" + fontTask.FileName);
            // 创建XMLHttpRequest对象用于下载字体文件。
            var xhr = new XMLHttpRequest();
            // 配置请求为异步获取字体文件。
            xhr.open("GET", strUrl, true);
            // 设置响应类型为blob，以便后续处理二进制数据。
            xhr.responseType = "blob";
            // 当请求成功时，处理响应的blob数据。
            xhr.onload = function () {
                // 检查请求是否成功。
                if (this.status == 200) {
                    // 获取到的字体文件blob对象。
                    var blob = this.response;
                    // 将blob数据转换为ArrayBuffer，以便于传递给DotNet方法。
                    DCTools20221228.blobToArrayBuffer(blob, buffer => {
                        // 创建Uint8Array视图，用于表示ArrayBuffer。
                        var buffer2 = new Uint8Array(buffer);
                        // 调用DotNet方法，设置字体文件内容。
                        DotNet.invokeMethod(
                            window.DCWriterEntryPointAssemblyName,
                            "SetFontFileContent",
                            fontTask.KeyValue,
                            fontTask.Name,
                            buffer2);
                        // 如果存在下一个任务函数，则调用它。
                        if (!!ExecuteNextTask && typeof (ExecuteNextTask) === "function") {
                            ExecuteNextTask();
                        }
                    });
                }
            };
            // 当请求出错时，调用ExecuteNextTask函数。
            xhr.onerror = function (err) {
                if (!!ExecuteNextTask && typeof (ExecuteNextTask) === "function") {
                    ExecuteNextTask();
                }
            };
            // 触发状态文本改变的事件，通知下载字体文件开始。
            WriterControl_Event.InnerRaiseEvent(rootElement, "StatusTextChanged", "下载字体文件[" + strUrl + "]");
            // 发送请求，开始下载字体文件。
            xhr.send();
        }
    },

    /**
     * 创建Print()/PrintPreview()函数使用的参数对象
     * @returns
     */
    CreatePrintOptions: function () {
        return {
            NotPrintWatermark: true | false | null,// 默认为false,不打印水印
            PrintTableCellBorder: true | false | null,// 默认为true,是否打印表格单元格边框
            CleanMode: true | false | null,// 默认为空，是否为整洁打印模式，可选值为true:整洁打印，false:留痕打印，空：采用编辑器当前的留痕显示设置。
            PrintRange: "AllPages" | "Selection" | "SomePages" | "CurrentPage",// 默认AllPages,打印范围，为一个字符串，可以为 AllPages,Selection,SomePages,CurrentPage
            PrintMode: "Normal" | "OddPage" | "EvenPage",// 默认Normal,打印模式，为一个字符串，可以为 Normal,OddPage,EvenPage。这里的页码是从0开始计算的。
            Collate: true,// 默认false,是否为逐份打印，为一个布尔值。需要本地打印才有效果
            Copies: 1,// 默认1,打印份数，为一个整数。需要本地打印才有效果
            FromPage: 0, // 默认0，从0开始计算的打印开始页码，只有PrintRange=SomePages时本设置才有效
            ToPage: 1,//默认为总页数, 从0开始计算的打印结束开始页码，只有PrintRange=SomePages时本设置才有效
            SpecifyPageIndexs: "1,3,6-11,12",//默认空，打印指定页码列表，页码从0开始计算，各个项目之间用逗号分开，如果项目中间有个横杠，表示一个页码范围
            BodyLayoutOffset: 0,// 默认为0，正文偏移续打的纵向偏移量。当该值大于0，则续打设置无效。
            PageIndexFix: 0,// 默认为0，打印出来的页码值的修正量。
            JumpPrintStartElementID: null,// 续打开始处的文档元素编号，若该属性值有效，则续打开始位置为该指定ID的元素的上边缘。
            JumpPrintEndElementID: null,// 续打结束处元素编号，若该属性值有效，则结束处续打的开始位置为指定ID的元素的下边缘。
            JumpPrint: { // 续打信息
                PageIndex: 0,// 从0开始计算的续打开始的页码
                Position: 0,// 开始续打的位置
                EndPageIndex: 0,// 从0开始计算的下端续打的页码
                EndPosition: 0// 下端续打的位置
            }
        };
    },

    /** 打印预览控件的属性文档视图 
     * @param {any} options 打印设置
     */
    InvalidatePreview: function (containerID, options) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement != null) {

            //1. 此方法仅开放在编辑器dctype为"WriterPrintPreviewControlForWASM"的控件上。
            //2. 当WriterPrintPreviewControlForWASM时，IsWriterPrintPreviewControlForWASM为true。
            //3. 所以此判断是无效的，先注释了 20240918 lxy[DUWRITER5_0 - 3585]。
            // if (rootElement.IsWriterPrintPreviewControlForWASM == false) {
            //     this.ClosePrintPreview(rootElement, false);
            //     var tick = new Date().valueOf();
            //     // 删除所有的页面图形元素
            //     var cnode = rootElement.firstChild;
            //     while (cnode != null) {
            //         console.log(cnode.nodeName, '=============');
            //         if (cnode.nodeName == "CANVAS" && cnode.getAttribute("dctype") == "page") {
            //             var tempNode = cnode;
            //             cnode = cnode.nextSibling;
            //             rootElement.removeChild(tempNode);
            //         }
            //         else if (cnode.nodeName == "DIV" && cnode.getAttribute("dctype") == "page-container") {
            //             while (cnode.firstChild != null) {
            //                 cnode.removeChild(cnode.firstChild);
            //             }
            //             break;
            //         }
            //         else {
            //             cnode = cnode.nextSibling;
            //         }
            //     }
            // }
            WriterControl_Print.PrintPreview(rootElement, options);
            //rootElement.__DCWriterReference.invokeMethod("InvalidatePreviewForWriterPrintPreviewControl", options);
            //rootElement.TempElementForDoubleBuffer = null;
            //WriterControl_Rule.InvalidateView(rootElement, "hrule");
            //WriterControl_Rule.InvalidateView(rootElement, "vrule");
            //WriterControl_Paint.InvalidateAllView(rootElement);
            //tick = new Date().valueOf() - tick;
            //WriterControl_Paint.UpdateViewForWaterMark(rootElement);
            //console.log("加载打印预览花费毫秒:" + tick);
        }
    },
    /**
     * 获得打印预览的页面所在的容器元素
     * @param {string | HTMLElement } strContainerID 编辑器控件编号或者对象
     * @returns { HTMLElement } 容器元素对象
     */
    GetPrintPrewViewPageContainer: function (strContainerID) {
        var div = DCTools20221228.GetChildNodeByDCType(strContainerID, "page-printpreview");
        return div;
    },
    /**
     * 是否处于打印预览状态
     * @param {string | HTMLElement} continaerID
     * @returns {boolean} 是否处于打印预览状态
     */
    IsInPrintPreview: function (continaerID) {
        var div = DCTools20221228.GetChildNodeByDCType(continaerID, "page-printpreview");
        return div != null && DCTools20221228.IsNullOrEmptyString(div.style.display);
    },
    /**
     * 关闭打印预览
     * @param {string | HTMLDivElement} containerID 根元素
     * @param {boolean} bolRefreshView  是否恢复文档内容排版
     */
    ClosePrintPreview: function (containerID, bolRefreshView) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement != null) {
            var div = DCTools20221228.GetChildNodeByDCType(rootElement, "page-printpreview");
            if (div != null && DCTools20221228.IsNullOrEmptyString(div.style.display)) {
                // 删除打印预览的部件
                rootElement.removeChild(div);
                if (bolRefreshView == true) {
                    // 恢复文档排版
                    rootElement.__DCWriterReference.invokeMethod("RefreshViewAfterPrint", true);
                }
                // 显示编辑视图
                WriterControl_Print.SetPageContainerVisible(rootElement, true);
            }
        }
    },
    /**
     * 设置编辑页面容器的可见性
     * @param {string | HTMLDivElement} containerID 根元素
     * @param {boolean} bolVisible - 是否可见
     */
    SetPageContainerVisible: function (containerID, bolVisible) {
        // 检查bolVisible是否为布尔类型
        if (typeof bolVisible !== "boolean") {
            console.error("bolVisible must be a boolean");
            return false;
        }
        // 获取容器的根元素
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        // 如果根元素不存在，输出错误信息并返回
        if (!rootElement) {
            console.error("rootElement not found for containerID:", containerID);
            return false;
        }
        // 获取标尺打印预览是否占位
        var PrintRuleOccupying = rootElement.getAttribute("PrintRuleOccupying") == "true";// 打印预览下，标尺是否占位。
        // 显示或隐藏容器
        for (var element = rootElement.firstChild; element != null; element = element.nextElementSibling) {
            // 如果元素为空，跳过
            if (!element) {
                continue;
            }
            // 如果元素节点类型不是元素节点，跳过
            if (element.nodeType != 1) {
                continue;
            }
            // 如果元素是样式表，跳过
            if (element.nodeName == "STYLE") {
                // 跳过样式表
                continue;
            }
            // 获取元素的dctype属性
            var DctypeStr = element.getAttribute("dctype");
            // 获取是否为标尺元素
            var isRule = (element.nodeName === "CANVAS") && (DctypeStr === "hrule" || DctypeStr === "vrule");
            // 当前为标尺元素，并且设置了打印时标尺隐藏占位
            if (isRule && PrintRuleOccupying) {
                // 显示隐藏标尺元素
                element.style.visibility = bolVisible ? "visible" : "hidden";
            } else {
                if (bolVisible == true) {
                    // 显示元素
                    element.style.display = element.__display_back;
                } else {
                    // 隐藏元素
                    if (element.style.display != "none") {
                        element.__display_back = element.style.display;
                        element.style.display = "none";
                    }
                }
            }
        }
        return true;
    },
    /**
     * 打印到服务器
     * @param {any} containerID 编辑器容器元素
     * @param {any} options 打印选项
     * @param {any} callBack 操作成功的回调函数
     */
    PrintToServer: function (containerID, strPrintServicePageUrl, options, callBack, getprinter) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return false;
        }
        if (strPrintServicePageUrl == null || strPrintServicePageUrl.length == 0) {
            strPrintServicePageUrl = rootElement.getAttribute("printservicepageurl");
        }
        if (strPrintServicePageUrl == null || strPrintServicePageUrl.length == 0) {
            console.log("DCWriter:未指定打印服务器的地址。");
            return false;
        }
        //wyc20240717:
        if (getprinter === true) {
            strPrintServicePageUrl = strPrintServicePageUrl + "?getprinter=true";
        }
        //wyc20230519把所有内容藏到options里，使用PrintPageRange属性名为了和四代兼容
        if (options == null) {
            options = new Object();
            options.PrintPageRange = "all";
        }
        //wyc20240723:本地打印不需要GetRuntimePageIndexString了全注掉
        //if (options.PrintPageRange == null || options.PrintPageRange.length == 0) {
        //    options.PrintPageRange = WriterControl_Print.GetRuntimePageIndexString(rootElement, options);
        //}
        //var strPageIndexString = WriterControl_Print.GetRuntimePageIndexString(rootElement, options);
        var strJsonOptions = options == null ? null : JSON.stringify(options);
        var postData = rootElement.__DCWriterReference.invokeMethod("GetDataForPrintToServer", strJsonOptions);
        if (postData == null || postData.buffer == null) {
            return false;
        }
        rootElement.__DCWriterReference.invokeMethod("RefreshViewAfterPrint", true);
        if (jQuery.support) {
            jQuery.support.cors = true;
        }
        jQuery.ajax(
            strPrintServicePageUrl,
            {
                async: true,
                data: postData.buffer,
                method: "POST",
                type: "POST",
                processData: false,
                crossDomain: true,
                xhrFields: {
                    withCredentials: false
                },
                success: function (response) {
                    // 处理成功的响应
                    console.log("localprint成功", response);
                },
                error: function (xhr, status, error) {
                    // 处理失败的响应
                    //console.log("localprint失败", error);
                    if (callBack != null && typeof (callBack) == "function") {
                        callBack.call(rootElement, false);
                    }
                }
            })
            .done(function (data, textStatus, jqXHR) {
                if (callBack != null && typeof (callBack) == "function") {
                    callBack.call(rootElement, data);
                }
            });
        return true;
    },
    /**
     * 打印成PDF格式
     * @param {string | HTMLDivElement} containerID 编辑器容器元素
     * @param {any} options 打印选项
     * @param {Function} callBack 操作成功后的回调函数，回调函数的参数为一个blob对象。如果回调函数为空则下载文件。
     * @returns 操作是否成功
     */
    PrintAsPDF: function (containerID, options, callBack) {
        return WriterControl_Print.InnerPrintAsFile(containerID, options, callBack, "pdf");
    },
    /**
     * 打印成HTML格式
     * @param {string | HTMLDivElement} containerID 编辑器容器元素
     * @param {any} options 打印选项
     * @param {Function} callBack 操作成功后的回调函数，回调函数的参数为html字符串。如果回调函数为空则下载文件。
     * @change ["2024-1-25","修改内嵌图片的处理，采用回调函数获得计算结果，不直接返回计算结果。","yyf" ]
     */
    PrintAsHtml: function (containerID, options, callBack) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement != null) {
            var strHtml = rootElement.__DCWriterReference.invokeMethod("GetPrintHtml", options);
            if (strHtml != null && strHtml.length > 0) {
                WriterControl_Paint.ApplyBitmapContentHtmlSrc(strHtml, function (strResultHtml) {
                    if (typeof (callBack) == "function") {
                        callBack.call(rootElement, strResultHtml);
                    }
                    else {
                        //var bytes = DCTools20221228.EncodeString("utf-8", strHtml);
                        // var bytes = new Uint8Array(strResultHtml);
                        // 修复PrintAsHtml不传参数直接下载的html为0字节的问题【DUWRITER5_0-3070】
                        WriterControl_Print.downloadElementPDFAndHTML(new Blob([strResultHtml]), "printhtml", rootElement);
                    }
                });
            }
            //if (strHtml != null && strHtml.length > 0) {
            //    if (typeof (callBack) == "function") {
            //        callBack.call(rootElement, strHtml);
            //    }
            //    else {
            //        //var bytes = DCTools20221228.EncodeString("utf-8", strHtml);
            //        var bytes = new Uint8Array(strHtml);
            //        WriterControl_Print.downloadElementPDFAndHTML(new Blob([bytes]), "printhtml", rootElement);
            //    }
            //}
            //return strHtml;
        }
        //return null;
        //return WriterControl_Print.InnerPrintAsFile(containerID, options, callBack, "printhtml");
    },
    GetRuntimePageIndexString: function (rootElement, options) {
        // 触发准备打印事件
        WriterControl_Event.InnerRaiseEvent(rootElement, "EventPreparePrint", options);
        // 获得实际打印输出的页码列表
        var strCode = rootElement.__DCWriterReference.invokeMethod("GetPageIndexWidthHeightForPrint", true, options, false);
        var datas = JSON.parse(strCode);
        var pageCount = datas.length; // datas.length / 3;
        var strResult = "";
        for (var iCount = 0; iCount < pageCount; iCount++) {
            if (strResult.length > 0) {
                strResult += ",";
            }
            strResult += iCount.toString();
        }
        //var runtimePageIndexs = WriterControl_Print.GetRuntimePageIndexArray(options, pageCount, rootElement);
        //var strResult = "";
        //if (runtimePageIndexs != null && runtimePageIndexs.length > 0) {
        //    for (var iCount = 0; iCount < runtimePageIndexs.length; iCount++) {
        //        if (strResult.length > 0) {
        //            strResult += ",";
        //        }
        //        strResult += runtimePageIndexs[iCount];
        //    }
        //}
        return strResult;
    },
    InnerPrintAsFile: function (containerID, options, callBack, strFormat) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return false;
        }
        var strServicePageUrl = DCTools20221228.GetServicePageUrl(rootElement);
        if (strServicePageUrl == null || strServicePageUrl.length == 0) {
            console.error("DCWriter:未配置ServicePageUrl,无法执行PrintAsFile.");
            return false;
        }
        // 此处对应的服务器代码在 DCWriterForASPNET\Writer\Controls\Web\WC_WASM.cs
        var strUrl = strServicePageUrl + "?wasm=downloadfile&format=" + strFormat + "&dcbid2022=" + DCTools20221228.GetClientID();
        // 追加要打印的页码列表
        var strPageIndexString = WriterControl_Print.GetRuntimePageIndexString(rootElement, options);
        if (strPageIndexString != null && strPageIndexString.length > 0) {
            strUrl = strUrl + "&pages=" + strPageIndexString;
        }
        var postData = rootElement.__DCWriterReference.invokeMethod("InnerForDownloadFile");
        // 恢复文档排版
        rootElement.__DCWriterReference.invokeMethod("RefreshViewAfterPrint", true);
        let that = this;
        var xhr = new XMLHttpRequest();
        xhr.open("POST", strUrl, true);
        xhr.responseType = "blob";
        xhr.onload = async function () {
            if (this.status == 200) {
                var blob = this.response;
                if (typeof (callBack) == "function") {
                    if (strFormat == 'printhtml') {
                        var newHtml = await blob.text();
                        // 执行回调函数
                        callBack.call(rootElement, newHtml);
                    } else if (strFormat == 'pdf') {
                        //执行把blob转base64
                        var reader = new FileReader();
                        reader.readAsDataURL(blob);
                        reader.onload = function (e) {
                            var result = e.target.result.substring(28);
                            callBack.call(rootElement, result);
                        };
                    }
                } else {
                    if (strFormat == 'printhtml') {
                        blob.text().then(res => {
                            if (res.indexOf('charset=gb2312') !== -1) {
                                let str2 = res.replace('charset=gb2312', 'charset=utf-8');
                                //console.log(str2); // hello javascript
                                var newblob = new Blob([str2]);
                                that.downloadElementPDFAndHTML(newblob, 'printhtml', rootElement);
                            } else {
                                that.downloadElementPDFAndHTML(blob, 'printhtml', rootElement);
                            }
                        });
                    }
                    if (strFormat == "pdf") {
                        that.downloadElementPDFAndHTML(blob, 'pdf', rootElement);
                    }
                }
            }
        };
        xhr.send(postData);
        return true;
    },
    /**
     * 打印为html、pdf的公共方法
     * @param {blob} blob 二进制对象
     * @param {string} strFormat 打印的格式
     */
    downloadElementPDFAndHTML: function (blob, strFormat, rootElement) {
        let downloadElement = rootElement.ownerDocument.createElement("a");
        let href = window.URL.createObjectURL(blob); //创建下载的链接
        downloadElement.href = href;
        if (strFormat == "pdf") {
            downloadElement.download = "PrintForPDF_" + new Date().valueOf() + ".pdf"; //下载后文件名
        }
        else if (strFormat == 'printhtml') {
            downloadElement.download = "PrintForHtml_" + new Date().valueOf() + ".html"; //下载后文件名
        }
        rootElement.ownerDocument.body.appendChild(downloadElement);
        downloadElement.click(); //点击下载
        rootElement.ownerDocument.body.removeChild(downloadElement); //下载完成移除元素
        window.URL.revokeObjectURL(href); //释放掉blob对象
    },
    /**
     * 重新绘制打印预览中所有的内容
     * @param {string | HTMLElement} containerID
     */
    PrintPreviewInvalidateAllView: function (containerID) {
        //var pageContainer = DCTools20221228.GetChildNodeByDCType(rootElement, "page-printpreview");
        //if (pageContainer != null) {
        //    for (var node = pageContainer.firstChild; node != null; node = node.nextSibling) {
        //        if (node.nodeName.toLowerCase() == "svg" && node._isRendered == true) {
        //            while (node.firstChild != null) {
        //                node.removeChild(node.firstChild);
        //            }
        //            WriterControl_Print.InnerDrawOnePage(node, true, null);
        //        }
        //    }
        //}
        ////// 目前svg打印预览不需要重新渲染【WriterControl_Print.PrintPreviewInvalidateAllView】【DUWRITER5_0-3310】
        ////return;
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        var pageContainer = DCTools20221228.GetChildNodeByDCType(rootElement, "page-printpreview");
        if (pageContainer != null && DCTools20221228.IsNullOrEmptyString(pageContainer.style.display)) {
            for (var node2 = pageContainer.firstChild; node2 != null; node2 = node2.nextSibling) {
                if (node2.nodeName.toLowerCase() == "svg") {
                    while (node2.firstChild != null) {
                        node2.removeChild(node2.firstChild);
                    }
                    node2._isRendered = false;
                }
            }
            pageContainer.__DoPageContainerScroll && pageContainer.__DoPageContainerScroll.call(pageContainer);
        }
    },
    /**
     * 为缩放而更新打印预览
     * @param {string | HTMLElement} containerID
     */
    UpdateZoomRateForPrintPreview: function (containerID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        var pageContainer = DCTools20221228.GetChildNodeByDCType(rootElement, "page-printpreview");
        if (pageContainer != null && DCTools20221228.IsNullOrEmptyString(pageContainer.style.display)) {
            for (var node2 = pageContainer.firstChild; node2 != null; node2 = node2.nextSibling) {
                if (node2.nodeName == "CANVAS") {
                    WriterControl_Paint.SetPageElementSize(rootElement, node2);
                    node2._isRendered = false;
                } else if (node2.nodeName == "svg") {
                    // 处理svg打印预览的缩放【DUWRITER5_0-3312】
                    WriterControl_Paint.SetPageElementSize(rootElement, node2);
                }
            }
            pageContainer.__DoPageContainerScroll.call(pageContainer);
        }
    },
    /**
     * 打印预览
     * @param {string | HTMLDivElement} containerID 根节点对象
     * @param {any} options 选项
     * @param {string} eleString 元素的数组 zhangbin 20230601
     */
    PrintPreview: function (containerID, options, eleString) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return false;
        }
        if (rootElement.IsWriterPrintPreviewControlForWASM == false) {
            // 修复打印预览模式下还可以调用打印预览的问题【DUWRITER5_0-3318】
            // 当目前是打印预览模式时，就不需要进行再次渲染
            if (rootElement.IsPrintPreview() == true) {
                return false;
            }
        }
        //var data22 = rootElement.__DCWriterReference.invokeMethod("GetWatermarkGraphicsDataForPrint");
        //if (data22 != null && data22.length > 0) {
        //    // 水印无法在SVG中直接绘制，则事先准备好水印图片
        //    var reader = new DCBinaryReader(data22);
        //    var pageWidth = reader.ReadInt32();
        //    var pageHeight = reader.ReadInt32();
        //    var tempElement = rootElement.ownerDocument.createElement("canvas");
        //    tempElement.width = pageWidth;
        //    tempElement.height = pageHeight;
        //    var drawer = new PageContentDrawer(tempElement, reader);
        //    drawer.TypeName = "UpdateViewForWaterMark";
        //    drawer.EventAfterDraw = function () {
        //        var strImageData = tempElement.toDataURL("image/png");
        //        tempElement.remove();
        //        rootElement.__DCWriterReference.invokeMethod("SetWatermarkImageDataForPrint", strImageData);
        //        WriterControl_Print.PrintPreview(rootElement, options, eleString);
        //    };
        //    drawer.AddToTask();
        //    return true;
        //}

        // 隐藏编辑视图
        WriterControl_Print.SetPageContainerVisible(rootElement, false);

        // 处理滚动事件,懒加载打印预览内容【DUWRITER5_0-3310】
        function DoPageContainerScroll() {
            var DivRect;
            // 此处修改nextSibling为nextElementSibling处理nextSibling为text的情况
            for (var element = this.firstChild; element != null; element = element.nextElementSibling) {
                var lowNodeName = element.nodeName.toLowerCase();
                if ((lowNodeName == "canvas" || lowNodeName == "svg") && element.getAttribute("dctype") == "page") {
                    // 已经渲染完成的无需重新渲染
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
                    //var drawer = new PageContentDrawer(element, null);
                    //drawer.PageIndex = element.PageIndex;
                    //drawer.EventQueryCodes = function (drawer2) {
                    //    // 获得该页面的绘图代码
                    //    this.CanvasElement._isRendered = true;
                    //    var strCodePage = rootElement.__DCWriterReference.invokeMethod(
                    //        "PaintPageForPrint",
                    //        drawer2.PageIndex,
                    //        true);
                    //    return strCodePage;
                    //};
                    //drawer.AddToTask();
                }
            }//for
        };

        //var editorContainer = DCTools20221228.GetChildNodeByDCType(rootElement, "page-container");
        var pageContainer = DCTools20221228.GetChildNodeByDCType(rootElement, "page-printpreview");
        if (pageContainer == null) {
            pageContainer = rootElement.ownerDocument.createElement("DIV");
            pageContainer.setAttribute("dctype", "page-printpreview");
            //pageContainer.style.setProperty('position', 'relative', 'important');
            //pageContainer.style.margin = "10px 10px 10px 10px";
            pageContainer.style.height = "100%";
            pageContainer.style.overflow = "auto";
            //pageContainer.style.backgroundColor = "#ffffff";
            pageContainer.style.textAlign = "center";
            pageContainer.style.position = "relative";
            //使用动画开启硬件加速，使打印预览效果更流畅
            pageContainer.style.transform = "translate3d(0,0,0)";
            pageContainer.style['-moz-transform'] = "translate3d(0,0,0)";
            pageContainer.style['-webkit-transform'] = "translate3d(0,0,0)";
            //对pageContainer添加事件监听
            pageContainer.addEventListener('contextmenu', function (e) {
                //修改为svg打印预览
                //[DUWRITER5_0-3585] 20240918 lxy 修复svg打印预览右键菜单问题
                if (e.target != null) {
                    if (typeof (rootElement.EventPrintPreviewContextMenu) == "function"
                        || typeof (rootElement.SetPreviewContextMenu) == "function") {
                        //判断是否为文本或者输入域或者表格
                        if (typeof (rootElement.EventPrintPreviewContextMenu) == "function") {
                            // 获取pageContainer元素的位置
                            var elementPosition = this.getBoundingClientRect();
                            // 计算偏移量
                            var offsetX = e.clientX - elementPosition.left + this.scrollLeft;
                            var offsetY = e.clientY - elementPosition.top + this.scrollTop;
                            // 修改EventPrintPreviewContextMenu事件中参数X，Y表示基于打印预览页面的位置；OriginallyEvent是默认的右键事件的Event【DUWRITER5_0-3907】
                            rootElement.EventPrintPreviewContextMenu(rootElement, {
                                ElementType: null,//svg的打印预览时，无法获取到当前元素的类型。 且e.target可能为子元素text
                                PageElement: e.target,
                                TypeName: "快捷菜单信息",
                                X: offsetX,
                                Y: offsetY,
                                clientX: e.clientX,
                                clientY: e.clientY,
                                OriginallyEvent: e
                            });
                        } else if (typeof (rootElement.SetPreviewContextMenu) == "function") {
                            rootElement.SetPreviewContextMenu(rootElement, {
                                ElementType: null,//svg的打印预览时，无法获取到当前元素的类型。 且e.target可能为子元素text
                                PageElement: e.target,
                                TypeName: "快捷菜单信息",
                                X: e.offsetX,
                                Y: e.offsetY,
                                clientX: e.clientX,
                                clientY: e.clientY
                            });
                        }
                    }
                }
            });
            pageContainer.addEventListener('mousedown', function (e) {
                //在页面中是否存在dcContextMenu
                var hasContextMenu = rootElement.querySelector('#dcContextMenu');
                if (hasContextMenu) {
                    hasContextMenu.remove();
                }
                //判断rootElement.MouseDragScrollMode是否为true，鼠标拖拽滚动
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
            });
            //用于改善打印预览的滚动体验【DUWRITER5_0-3310】
            pageContainer.addEventListener("wheel", WriterControl_UI.PageContainerOnWheelFunc);
        }
        else {
            while (pageContainer.firstChild != null) {
                pageContainer.removeChild(pageContainer.firstChild);
            }
        }
        pageContainer.style.display = "";
        rootElement.appendChild(pageContainer);

        // 打印预览模式下，需要修复打印预览界面的高度【DUWRITER5_0-2724】
        var hruleNode = rootElement.querySelector("canvas[dctype='hrule']");//获取标尺元素
        if (hruleNode != null && hruleNode.offsetHeight > 0) {
            // 标尺占位时的处理
            pageContainer.style.height = "calc(100% - " + hruleNode.offsetHeight + "px)";//设置高度
        } else {
            pageContainer.style.height = "100%";
        }
        //// xuyiming 添加打印预览前事件【EventBeforePrintPreview】
        //if (rootElement.EventBeforePrintPreview != null && typeof (rootElement.EventBeforePrintPreview) == "function") {
        //    rootElement.EventBeforePrintPreview(rootElement);
        //}
        //如果eleArr存在在此处单独处理对元素page-printpreview元素进行赋值
        if (eleString) {
            pageContainer.innerHTML = eleString;
        } else {
            pageContainer.onscroll = DoPageContainerScroll;
            // 触发准备打印事件
            WriterControl_Event.InnerRaiseEvent(rootElement, "EventPreparePrint", options);
            // 这里返回一个JSON数组，长度是要打印页数的四倍。四个整数一小组，第一个是页码，第二个是页宽，第三个是页高，第四个无意义。
            var strCode = rootElement.__DCWriterReference.invokeMethod(
                "GetPageIndexWidthHeightForPrint",
                true,
                options,
                false);
            if (strCode == null || strCode.length == 0) {
                // 无数据
                return;
            }
            var datas = JSON.parse(strCode);
            //var pageCount = datas.length / 4;
            //var strBKImageData = null;
            //if (rootElement.__BackgroundImageElement != null) {
            //    strBKImageData = rootElement.__BackgroundImageElement.toDataURL();
            //}

            //计算出样式区别
            var pageBorderColor = rootElement.getAttribute("pagebordercolor");
            var pagelayoutmode = rootElement.getAttribute('pagelayoutmode');
            for (var iCount = 0; iCount < datas.length; iCount++) {
                var pageInfo = datas[iCount];
                var element = rootElement.ownerDocument.createElementNS("http://www.w3.org/2000/svg", "svg");
                //var element = rootElement.ownerDocument.createElement("CANVAS");
                //if (element.nodeName.toLowerCase() == "svg") {
                //element.setAttribute("xmlns", "http://www.w3.org/2000/svg");
                element.setAttribute("width", pageInfo.Width + "px");
                element.setAttribute("height", pageInfo.Height + "px");
                //}
                element.setAttribute("dctype", "page");
                element.setAttribute("PageIndex", pageInfo.PageIndex);
                element.PageIndex = pageInfo.PageIndex;
                element.setAttribute("native-width", pageInfo.Width);
                element.setAttribute("native-height", pageInfo.Height);
                element.__PageInfo = pageInfo;
                //element.width = pageInfo.Width;
                //element.height = pageInfo.Height;
                //var bkImg = datas[iCount * 4 + 3];

                element.style.border = `1px solid ${pageBorderColor ? pageBorderColor : "black"}`;
                element.style.backgroundColor = "white";
                element.style.verticalAlign = "top";
                element.style.boxSizing = "content-box";
                //判断编辑器是否为单栏展示,如果是则设置为块级元素并且设置为居中显示
                if (typeof pagelayoutmode == 'string' && pagelayoutmode.trim().toLowerCase() == 'singlecolumn') {
                    element.style.margin = "5px auto";
                    element.style.display = 'block';
                } else {
                    element.style.margin = "5px";
                    element.style.display = 'inline-block';
                }
                WriterControl_Paint.SetPageElementSize(rootElement, element);
                //if (bkImg == 1 && strBKImageData != null) {
                //    jQuery(element).addClass(rootElement.__BKImgStyleName)
                //element.style.backgroundImage = "url(" + strBKImageData + ")";
                //}
                element.innerHTML = "<text x='50' y='50'>" + window.__DCSR.Waitting + "</text>";
                element._isRendered = false;
                element.onclick = function (eve) {
                    if (rootElement.__DCDisposed == true) {
                        return;
                    }
                    var strPageList = rootElement.__DCWriterReference.invokeMethod(
                        "WASMPrintPreviewHandleMouseClick",
                        this.__PageInfo.PageIndex,
                        eve.offsetX,
                        eve.offsetY,
                        eve.ctrlKey);
                    var pageIndexs = DCTools20221228.ParseInt32Values(strPageList);
                    if (pageIndexs != null && pageIndexs.length == 2) {
                        for (var node2 = this.parentNode.firstChild; node2 != null; node2 = node2.nextSibling) {
                            var lowNodeName = node2.nodeName.toLowerCase();
                            if (lowNodeName == "canvas" || lowNodeName == "svg") {
                                if (node2.PageIndex >= pageIndexs[0]
                                    && node2.PageIndex <= pageIndexs[1]) {
                                    WriterControl_Print.InnerDrawOnePage(node2, true);
                                    //DCTools20221228.ClearCanvasElementContent(node2);
                                    //node2._isRendered = false;
                                }
                            }
                        }
                        //DoPageContainerScroll.call(this.parentNode);
                    }
                };
                pageContainer.appendChild(element);
            }
            //在此处执行判断是否需要执行区域选择打印
            if (options == null && rootElement.RectInfo && typeof rootElement.RectInfo.printPreviewFun == "function") {
                rootElement.RectInfo.printPreviewFun(pageContainer);
            }
            DoPageContainerScroll.call(pageContainer);
            pageContainer.__DoPageContainerScroll = DoPageContainerScroll;
        }
        // 修复打印预览无法触发EventAfterPrintPreview事件的问题
        if (rootElement.EventAfterPrintPreview != null
            && typeof (rootElement.EventAfterPrintPreview) == "function") {
            rootElement.EventAfterPrintPreview(rootElement);
        }
        // WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
        //     if (rootElement.EventAfterPrintPreview != null
        //         && typeof (rootElement.EventAfterPrintPreview) == "function") {
        //         rootElement.EventAfterPrintPreview(rootElement);
        //     }
        // });
    },

    ///**
    // * 为打印预览而填充页面信息
    // * @param {any} containerID
    // * @param {any} strCodes
    // */
    //FillPagesForPreview: function (containerID, strCodes) {
    //    var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
    //    if (rootElement == null) {
    //        return null;
    //    }
    //},
    /**
     * 获得打印用的内置框架元素对象
     * @param {string} containerID 容器元素对象
     * @param {boolean} autoCreate 是否自动创建
     * @returns {HTMLIFrameElement} 内置框架对象
     */
    GetIFrame: function (containerID, autoCreate) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return null;
        }
        var result = rootElement.ownerDocument.getElementById(rootElement.id + "_IFrame_Print");
        if (result == null) {
            if (autoCreate == false) {
                return null;
            }
            result = rootElement.ownerDocument.createElement("iframe");
            result.id = rootElement.id + "_IFrame_Print";
            result.style.position = "absolute";
        }
        rootElement.appendChild(result);
        result.style.width = rootElement.offsetWidth + "px";
        result.style.height = rootElement.offsetHeight + "px";
        result.style.left = "0px";
        result.style.top = "0px";// (rootElement.offsetTop + 600) + "px";
        result.style.border = "1px solid blue";
        result.style.display = "";
        result.style.backgroundColor = "white";
        result.style.zIndex = 10000;
        return result;
    },
    /**
     * 绘制单页内容的内部方法
     * 
     * 此方法负责在指定的元素内绘制单页视图，主要用于打印预览的页面呈现或者打印等功能
     * 
     * @param {HTMLElement} element - 需要在其中绘制页面的DOM元素
     * @param {boolean} bolPrintPreview - 是否获取打印预览的内容
     * @param {Object} oldCtl - 旧的控制元素，用于在更新或重新绘制时进行引用
     */
    InnerDrawOnePage: function (element, bolPrintPreview, oldCtl) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(element);
        if (rootElement == null) {
            if (oldCtl) {
                rootElement = oldCtl;
            } else {
                return;
            }
        }
        rootElement.style.shapeRendering = "crispEdges";
        //if (element.nodeName.toLowerCase() == "svg") {
        // 采用SVG模式进行打印预览
        var strSVG = rootElement.__DCWriterReference.invokeMethod(
            "PaintPageForPrintUseSVG",
            element.PageIndex,
            bolPrintPreview);
        if (strSVG != null) {
            // 为了点击续打时，svg元素没有渲染时需要渲染时重新添加续打蒙版【DUWRITER5_0-3424】
            if (element.JumpMarkNodeStr) {
                strSVG += element.JumpMarkNodeStr;
                delete element.JumpMarkNodeStr;
            }
            element.innerHTML = strSVG;
            element._isRendered = true;
            /** 是否允许选中页眉页脚内容 */
            var isHeaderFooterSelect = rootElement.getAttribute("HeaderFooterSelect");
            if (typeof (isHeaderFooterSelect) == "string" && isHeaderFooterSelect.toLowerCase() == "true") {
                // 设置页眉页脚元素不可选【DUWRITER5_0-4108】
                var HeaderAndFooterNodes = element.querySelectorAll("#divXTextDocumentHeaderForFirstPageElement,#divXTextDocumentHeaderElement,#divXTextDocumentFooterForFirstPageElement,#divXTextDocumentFooterElement");
                for (var iCount = 0; iCount < HeaderAndFooterNodes.length; iCount++) {
                    var node = HeaderAndFooterNodes[iCount];
                    if (node) {
                        node.setAttribute("user-select", "none");
                    }
                }
            }
            var intCount45 = 10;
            for (var node45 = element.lastChild;
                node45 != null && intCount45 > 0;
                node45 = node45.previousSibling, intCount45--) {
                if (node45.nodeName == "rect" && node45.getAttribute("dctype") == "contentheight") {
                    var intContentHeight = parseInt(node45.getAttribute("value"));
                    element.__ContentHeight = intContentHeight;
                    // 修复SVG打印时每一页多出一页的问题【DUWRITER5_0-3320】
                    // svg打印时不需要增加高度
                    // if (bolPrintPreview == false) {
                    //     // 不是打印预览，而是打印
                    //     element.setAttribute("height", (intContentHeight + 2) + "px");
                    // }
                    break;
                }
            }
        }
        return;
        //}
        //if (bolPrintPreview == false ) {
        //    // 处于打印模式下，用全白色填充背景
        //    var ctx = element.getContext("2d");
        //    ctx.fillStyle = "white";
        //    ctx.fillRect(0, 0, element.width, element.height);
        //}
        //var pageInfo = element.__PageInfo;
        //if (pageInfo != null && pageInfo.PageSpan != null && pageInfo.PageSpan.length > 1) {
        //    // 拼接打印
        //    var zoomRate = rootElement.__DCWriterReference.invokeMethod("get_ZoomRate");
        //    var baseZoomRate = rootElement.__DCWriterReference.invokeMethod("get_WASMBaseZoomRate");
        //    if (bolPrintPreview == false) {
        //        baseZoomRate = 1;
        //        zoomRate = 1;
        //    }
        //    var pageSpan = pageInfo.PageSpan;
        //    var tempElement = WriterControl_Print.__TempCanvasElement;
        //    if (tempElement == null) {
        //        tempElement = rootElement.ownerDocument.createElement("canvas");
        //        WriterControl_Print.__TempCanvasElement = tempElement;
        //    }
        //    tempElement.width = element.width * zoomRate * baseZoomRate;
        //    tempElement.height = pageSpan[0] * zoomRate * baseZoomRate;
        //    for (var iCount = 1; iCount < pageSpan.length; iCount++) {
        //        var pi = pageSpan[iCount];
        //        var drawer = new PageContentDrawer(tempElement);
        //        drawer.PageIndex = pi;
        //        drawer.TopPos = pageSpan[0] * (iCount - 1) * zoomRate * baseZoomRate;
        //        drawer.EventQueryCodes = function () {
        //            if (rootElement.__DCDisposed == true) {
        //                return null;// 控件已经被销毁了，无法打印
        //            }
        //            DCTools20221228.ClearCanvasElementContent(tempElement);
        //            return rootElement.__DCWriterReference.invokeMethod(
        //                "PaintPageForPrint",
        //                this.PageIndex,
        //                bolPrintPreview);
        //        };
        //        drawer.EventAfterDraw = function () {
        //            if (element._isRendered == true) {
        //                DCTools20221228.ClearCanvasElementContent(element);
        //            }
        //            var ctx = element.getContext("2d");
        //            ctx.drawImage(tempElement, 0, this.TopPos);
        //            DCTools20221228.ClearCanvasElementContent(tempElement);
        //            element._isRendered = true;
        //        };
        //        drawer.AddToTask();
        //    }
        //}
        //else {
        //    var drawer = new PageContentDrawer(element, null);
        //    drawer.PageIndex = element.PageIndex;
        //    drawer.EventQueryCodes = function (drawer2) {
        //        // 获得该页面的绘图代码
        //        var strCodePage = rootElement.__DCWriterReference.invokeMethod(
        //            "PaintPageForPrint",
        //            drawer2.PageIndex,
        //            bolPrintPreview);
        //        if (this.CanvasElement._isRendered == true) {
        //            DCTools20221228.ClearCanvasElementContent(this.CanvasElement);
        //        }
        //        this.CanvasElement._isRendered = true;
        //        return strCodePage;
        //    };
        //    drawer.AddToTask();
        //}
    },
    /**
     * 打印
     * @param {string | HTMLElement} containerID 容器元素编号
     * @param {any} options 打印参数
     * @returns {boolean} 操作是否成功，但打印是异步操作，函数虽然返回，但打印还在继续。
     */
    Print: function (containerID, options) {
        //console.log('print');
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return false;
        }
        //// 修复直接在编辑模式下打印时水印未打印的问题【DUWRITER5_0-3163】
        //var data22 = rootElement.__DCWriterReference.invokeMethod("GetWatermarkGraphicsDataForPrint");
        //if (data22 != null && data22.length > 0) {
        //    // 水印无法在SVG中直接绘制，则事先准备好水印图片
        //    var reader = new DCBinaryReader(data22);
        //    var pageWidth = reader.ReadInt32();
        //    var pageHeight = reader.ReadInt32();
        //    var tempElement = rootElement.ownerDocument.createElement("canvas");
        //    tempElement.width = pageWidth;
        //    tempElement.height = pageHeight;
        //    var drawer = new PageContentDrawer(tempElement, reader);
        //    drawer.TypeName = "UpdateViewForWaterMark";
        //    drawer.EventAfterDraw = function () {
        //        var strImageData = tempElement.toDataURL("image/png");
        //        tempElement.remove();
        //        rootElement.__DCWriterReference.invokeMethod("SetWatermarkImageDataForPrint", strImageData);
        //        WriterControl_Print.Print(rootElement, options);
        //    };
        //    drawer.AddToTask();
        //    return true;
        //}
        if (options && options.PrintRange == "Selection") {
            if (rootElement.__DCWriterReference.invokeMethod("HasSelection") === false) {
                console.log("编辑器未选中数据");
                return;
            }
            // 在此处修改页眉下划线配置
            WriterControl_Print.oldShowHeaderBottomLine = rootElement.DocumentOptions.ViewOptions.ShowHeaderBottomLine;
            if (WriterControl_Print.oldShowHeaderBottomLine === true) {
                rootElement.DocumentOptions.ViewOptions.ShowHeaderBottomLine = false;
                rootElement.ApplyDocumentOptions();
            }
        }
        var iframe = WriterControl_Print.GetIFrame(containerID, true);
        iframe.style.display = 'none';

        if (iframe == null) {
            return false;
        }

        // var bkImage = null;
        // if (rootElement.__WaterMarkData != null && rootElement.__WaterMarkData.length > 0) {
        //     bkImage = rootElement.ownerDocument.createElement("img");
        //     bkImage.src = rootElement.__WaterMarkData;
        // }
        var targetDocument = iframe.contentDocument;
        targetDocument.open();
        targetDocument.write("");
        targetDocument.close();
        var previewContainer = DCTools20221228.GetChildNodeByDCType(rootElement, "page-printpreview");
        var isFromPrintPreview = previewContainer != null && DCTools20221228.IsNullOrEmptyString(previewContainer.style.display);
        if (isFromPrintPreview == true) {
            // 从打印预览开始进行打印
        } else {
            // 从文档编辑状态开始进行打印，触发准备打印事件
            WriterControl_Event.InnerRaiseEvent(rootElement, "EventPreparePrint", options);
        }
        var strCode = null;
        var bolIsWriterPrintPreviewControlForWASM = rootElement.getAttribute("dctype") == "WriterPrintPreviewControlForWASM";
        if (bolIsWriterPrintPreviewControlForWASM) {
            // 打印预览控件
            strCode = rootElement.__DCWriterReference.invokeMethod("GetPageIndexWidthHeightForWriterPrintPreviewControl", false, options);
        } else {
            // 编辑器控件
            if (isFromPrintPreview == true) {
                strCode = rootElement.__DCWriterReference.invokeMethod("GetPageIndexWidthHeightForPrintFromPrintPreview", options);
            }
            else {
                strCode = rootElement.__DCWriterReference.invokeMethod("GetPageIndexWidthHeightForPrint", false, options, false);
            }
        }
        if (strCode == null || strCode.length == 0) {
            // 没有获得任何数据
            return;
        }
        var datas = JSON.parse(strCode);
        var div = targetDocument.createElement("DIV");
        targetDocument.body.appendChild(div);
        var strPageStyle = datas.shift();// 页面样式
        var divZoom = datas.shift();// 缩放比例
        //div.style.zoom = divZoom; // 这里缩小显示被放大的内容，用于改进打印输出的精细度。
        //div.style.transform = "scale(" + divZoom + ")";// 用于兼容Firefox
        var styleElement = targetDocument.createElement("STYLE");
        styleElement.innerText = strPageStyle;
        targetDocument.head.appendChild(styleElement);
        targetDocument.body.style.margin = "0px";
        targetDocument.title = " ";
        // 获得实际打印输出的页码列表
        // var isFirstPage = true;
        //console.log('pageCount')
        for (var iCount = 0; iCount < datas.length; iCount++) {
            var pageInfo = datas[iCount];
            // if (isFirstPage == false) {
            //     div.appendChild(targetDocument.createElement("BR"));
            // } else {
            //     isFirstPage = false;
            // }
            //var element = targetDocument.createElement("CANVAS");
            var element = targetDocument.createElementNS("http://www.w3.org/2000/svg", "svg");
            //var element = rootElement.ownerDocument.createElement("CANVAS");
            //if (element.nodeName.toLowerCase() == "svg") {
            //element.setAttribute("xmlns", "http://www.w3.org/2000/svg");
            element.setAttribute("width", pageInfo.Width + "px");
            element.setAttribute("height", pageInfo.Height + "px");
            //}
            element.__PageInfo = pageInfo;
            element.PageIndex = pageInfo.PageIndex;
            //element.width = pageInfo.Width;
            //element.height = pageInfo.Height;
            //var bolBKImg = datas[iCount * 4 + 3];
            //element.style.pageBreakAfter = "always";
            element.style.pageBreakInside = "avoid";
            element.style.display = "block";
            //element.style.border = "1px solid black";
            div.appendChild(element);
            WriterControl_Print.InnerDrawOnePage(element, false);
        }//for
        //返回打印的html
        if (rootElement != null
            && rootElement.EventBeforePrintToGetHtml != null
            && typeof rootElement.EventBeforePrintToGetHtml == 'function') {
            rootElement.PrintAsHtml(null, null, {
                isPrint: true,
                printCallBack: function (html, printFun) {
                    rootElement.EventBeforePrintToGetHtml(html);
                }
            });
        }
        // 打印先渲染页面展示，再进行打印【DUWRITER5_0-3379】
        if (bolIsWriterPrintPreviewControlForWASM == false && isFromPrintPreview == false) {
            rootElement.__DCWriterReference.invokeMethod("RefreshViewAfterPrint", true);
        }
        // 在此处将是否需要页眉下划线修改回来
        if (WriterControl_Print.oldShowHeaderBottomLine === true) {
            rootElement.DocumentOptions.ViewOptions.ShowHeaderBottomLine = true;
            rootElement.ApplyDocumentOptions();
        }
        var printFun = function () {
            if (options == null && rootElement.RectInfo && typeof rootElement.RectInfo.printPreviewFun == "function") {
                rootElement.RectInfo.printPreviewFun(div, null, "print");
            }
            // 打印预览控件支持续打【DUWRITER5_0-3424】
            if (bolIsWriterPrintPreviewControlForWASM == true && rootElement.JumpPrintInfo && typeof (rootElement.JumpPrintInfo.AdjustJumpPrintMark) == "function") {
                rootElement.JumpPrintInfo.AdjustJumpPrintMark(div, true);
            }

            if (options != null && typeof (options.CompletedCallback) == "function") {
                iframe.contentWindow.onafterprint = function (e) {
                    var divWASM = rootElement.ownerDocument.querySelectorAll("[dctype='WriterControlForWASM']");
                    if (divWASM && divWASM.length > 0) {
                        divWASM = Array.from(divWASM);
                        for (var i = 0; i < divWASM.length; i++) {
                            divWASM[i].RefreshInnerView();
                        }
                    }
                    // !! 这里有个问题，用户按下取消，本事件也会触发
                    // 执行打印完毕事件
                    options.CompletedCallback.call(rootElement, rootElement);
                    iframe.contentDocument.close();
                    iframe.style.display = "none";
                    // 修改打印后销毁打印iframe，DebugMode="true"进入调试模式时不销毁【DUWRITER5_0-3554】
                    if (rootElement && rootElement.getAttribute("DebugMode") != "true") {
                        DCTools20221228.destroyIframe(iframe);
                    }
                };
            } else {
                // 修改打印后销毁打印iframe，DebugMode="true"进入调试模式时不销毁【DUWRITER5_0-3554】
                if (rootElement && rootElement.getAttribute("DebugMode") != "true") {
                    iframe.contentWindow.onafterprint = function (e) {
                        DCTools20221228.destroyIframe(iframe);
                    };
                }
            }
            //在此处处理打印前事件
            if (rootElement != null && rootElement.EventBeforePrint != null
                && typeof rootElement.EventBeforePrint == 'function') {
                rootElement.EventBeforePrint(targetDocument);
            }
            ////在打印前替换element为图片
            //var allCanvas = targetDocument.querySelectorAll("canvas");
            //if (allCanvas && allCanvas.length > 0) {
            //    for (var z = 0; z < allCanvas.length; z++) {
            //        var canvas = allCanvas[z];
            //        //console.log(canvas);
            //        var imgStr = canvas.toDataURL("image/png", 1);
            //        var img = new Image();
            //        img.src = imgStr;
            //        canvas.parentNode.insertBefore(img, canvas);
            // canvas.remove();
            //    }
            //}
            //console.log(img);
            //img.onload = function () {

            // 如果不需要打印水印
            if (rootElement.IsWriterPrintPreviewControlForWASM && options && options.NotPrintWatermark == true) {
                // 获取iframe内部文档
                var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
                // 获取SVG元素
                var iframeDoc_svg = iframeDoc.getElementsByTagName('svg');
                for (var i = 0; i < iframeDoc_svg.length; i++) {
                    let svg = iframeDoc_svg[i];
                    // 获取SVG元素的第二个子元素
                    var secondChild = svg.children[1];
                    // 判断第二个子元素是否是image元素
                    if (secondChild.tagName === 'image' || secondChild.tagName === 'pattern') {
                        // 删除第二个子元素
                        secondChild.parentNode.removeChild(secondChild);
                    }
                }
            }

            // 所有绘图任务完成，进行打印
            // 获取iframe内部文档
            var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
            // 确保svg图片元素都渲染完成才进行打印，防止某些图片无法正常显示【DUWRITER5_0-3554】
            // 获取所有的SVG image元素和html的img元素
            const images = iframeDoc.querySelectorAll("svg image, img");
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
                        iframe.contentWindow.print();
                    }
                };
                images.forEach(function (image) {
                    let img;
                    if (image.nodeName == "IMG") {
                        img = image;
                    } else {
                        const href = image.href.baseVal; // 获取image元素引用的图片URL
                        img = new Image(); // 创建一个新的Image对象
                        img.src = href; // 设置Image对象的src属性
                    }
                    img.onload = CompleteFunc;
                    img.onerror = CompleteFunc;
                    // 检查图片是否已经加载完成
                    if (img.complete && typeof (CompleteFunc) == "function") {
                        CompleteFunc();
                    }
                });
            } else {
                iframe.contentWindow.print();
            }

            // if (bolIsWriterPrintPreviewControlForWASM == false && isFromPrintPreview == false) {
            //     rootElement.__DCWriterReference.invokeMethod("RefreshViewAfterPrint", true);
            // }
            // //在此处将是否需要页眉下划线修改回来
            // if (WriterControl_Print.oldShowHeaderBottomLine === true) {
            //     rootElement.DocumentOptions.ViewOptions.ShowHeaderBottomLine = true;
            //     rootElement.ApplyDocumentOptions();
            // }
            //}

            //rootElement.removeChild(iframe);
            // 销毁打印过的文档
            //iframe.contentDocument.open();
            //iframe.contentDocument.write("");
            //iframe.contentDocument.close();
            //iframe.style.display = "none";
        };
        // 如果有任务正在运行，则等待任务完成后再打印
        // 因为之前是CANVAS，需要绘制，默认有任务需要处理，SVG是同步的无需等待
        // 修复打印预览模式下时打印不直接出来的问题
        if (WriterControl_Task.__Tasks && WriterControl_Task.__Tasks.length > 0) {
            WriterControl_Task.AddCallbackForCompletedAllTasks(printFun);
        } else {
            printFun();
        }
        return true;
    },
    /**
     * 通过预览的HTML将其转换为可以打印的HTML字符串
     * 添加打印HTML时可以处理续打模式和区域选择模式，只支持当前文档【DUWRITER5_0-2939】
     * @param {string | HTMLElement}} containerID - 容器的ID，用于定位页面上的特定区域。
     * @param {string} HtmlString - 页面上要打印的HTML内容。
     * @returns {string} 可以打印的HTML字符串
     */
    GetPrintNowHTML: function (containerID, HtmlString) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (!rootElement || typeof (HtmlString) != "string") {
            return HtmlString;
        }
        let domparser = new DOMParser();
        let doc = domparser.parseFromString(HtmlString, "text/html");
        var dcRootCenter = doc.querySelector("#dcRootCenter");
        // GetPrintNowHTML添加判断避免传入字符串不是DCWRITER正常html
        if (!dcRootCenter) {
            return HtmlString;
        }
        /** 是否是打印预览模式 */
        var isIsPrintPreview = rootElement.IsPrintPreview();
        // =============================处理续打模式START=============================
        var JumpPrintData;
        if (isIsPrintPreview == true) {
            JumpPrintData = rootElement.GetJumpPrintInfoForPrintPreview();
        } else {
            JumpPrintData = rootElement.GetJumpPrintInfo();
        }
        if (JumpPrintData && JumpPrintData.Mode == "Normal" && (JumpPrintData.PageIndex >= 0 || JumpPrintData.EndPageIndex >= 0)) {
            // 续打模式需要处理
            var StartPageIndex = JumpPrintData.PageIndex;
            var EndPageIndex = JumpPrintData.EndPageIndex;
            var DocumentBodyProps = rootElement.GetElementProperties(rootElement.DocumentBody);
            /** 正文距离该页面的顶部的距离 */
            var DocumentBodyTop = 0;
            if (DocumentBodyProps) {
                DocumentBodyTop = DocumentBodyProps.TopInOwnerPage;
            }
            /** 续打开始位置 */
            let StartPosition = parseInt((DocumentBodyTop + JumpPrintData.Position) / 300 * 96.00001209449);
            /** 续打结束位置 */
            let EndPosition = parseInt((DocumentBodyTop + JumpPrintData.EndPosition) / 300 * 96.00001209449);
            var PageDivs = doc.querySelectorAll("#dcRootCenter > div[pageindex]");
            if (PageDivs && PageDivs.length > 0) {
                for (var index = 0; index < PageDivs.length; index++) {
                    var PageDiv = PageDivs[index];
                    if (!PageDiv) {
                        continue;
                    }
                    var PageIndex = Number(PageDiv.getAttribute("pageindex"));
                    // 获取是否隐藏页眉页脚,但是要占位
                    var IsNeedHiddenHeaderAndFooterNodes = false;
                    if (StartPageIndex >= 0) {
                        if (StartPageIndex > PageIndex) {
                            // 当前页面全部删除
                            PageDiv.remove();
                        } else if (StartPageIndex == PageIndex && JumpPrintData.Position > 0) {
                            // 当前页面需要隐藏上边部分内容
                            // 1.隐藏正文内容
                            PageDiv.style.position = "relative";
                            var StartMaskNode = doc.createElement("div");
                            StartMaskNode.style.cssText = "position:absolute;top:0;left:0;width:100%;background:#fff;";
                            StartMaskNode.style.height = StartPosition + "px";
                            PageDiv.appendChild(StartMaskNode);
                            // 2.隐藏页眉页脚,但是要占位，合并代码执行
                            IsNeedHiddenHeaderAndFooterNodes = true;
                        }
                    }
                    if (EndPageIndex >= 0) {
                        if (EndPageIndex < PageIndex) {
                            // 当前页面全部删除
                            PageDiv.remove();
                        } else if (EndPageIndex == PageIndex && JumpPrintData.EndPosition > 0) {
                            // 当前页面需要隐藏下边部分内容
                            // 1.隐藏正文内容
                            PageDiv.style.position = "relative";
                            var EndMaskNode = doc.createElement("div");
                            EndMaskNode.style.cssText = "position:absolute;bottom:0;left:0;width:100%;background:#fff;";
                            EndMaskNode.style.height = "calc(100% - " + EndPosition + "px)";
                            PageDiv.appendChild(EndMaskNode);
                            // 2.隐藏页眉页脚,但是要占位，合并代码执行
                            IsNeedHiddenHeaderAndFooterNodes = true;
                        }
                    }
                    if (IsNeedHiddenHeaderAndFooterNodes == true) {
                        // 隐藏页眉页脚,但是要占位
                        var HeaderAndFooterNodes = PageDiv.querySelectorAll("#divXTextDocumentHeaderElement,#divXTextDocumentHeaderForFirstPageElement,#divXTextDocumentFooterElement,#divXTextDocumentFooterForFirstPageElement");
                        if (HeaderAndFooterNodes && HeaderAndFooterNodes.length > 0) {
                            for (var i = 0; i < HeaderAndFooterNodes.length; i++) {
                                HeaderAndFooterNodes[i].style.visibility = "hidden";
                            }
                        }
                    }
                }
            }
        }
        // ==============================处理续打模式END==============================
        // =============================处理区域选择模式START=============================
        var RectInfo = rootElement.RectInfo;
        if (RectInfo && typeof (RectInfo.UseHTMLPrintFunc) == "function") {
            RectInfo.UseHTMLPrintFunc(doc);
        }
        // ==============================处理区域选择模式END==============================
        // 判断是否为横向
        var IsLandscape = false;// 默认不是横向
        var DcDocInfoStr = doc.body.getAttribute("dcdocinfo");
        if (DcDocInfoStr) {
            DcDocInfoStr = DcDocInfoStr.replace(/&quot;/g, "\"");
            try {
                var obj = JSON.parse(DcDocInfoStr);
                var PageLandscape = obj.PageSettings.Landscape;
                if (typeof (PageLandscape) != "boolean") {
                    PageLandscape += "";//强制修改成字符串
                    if (typeof (PageLandscape.toLowerCase) == "function" && PageLandscape.toLowerCase() == "true") {
                        IsLandscape = true;
                    }
                }
                PageLandscape = null;
            } catch (error) {

            }
        }
        // 清除需要打印隐藏的元素
        var allHiddenEle = dcRootCenter.querySelectorAll('.hiddenforprint');
        if (allHiddenEle && allHiddenEle.length > 0) {
            for (var index = 0; index < allHiddenEle.length; index++) {
                allHiddenEle[index].remove();
            }
        }
        // 处理页面中的承载整体内容的表格
        var AllContentDcTables = dcRootCenter.querySelectorAll("table[id='dctable_AllContent']");
        if (AllContentDcTables && AllContentDcTables.length > 0) {
            for (var index = 0; index < AllContentDcTables.length; index++) {
                var table = AllContentDcTables[index];
                if (table.getAttribute("haspageborder") != "true") {
                    // 没有页面边框，则设置表格边框为空。
                    table.style.border = "0px none white";
                }
                // 对于现代浏览器，直接设置borderRadius为""即可清除
                table.style.borderRadius = "";
                // 考虑到极旧的浏览器兼容性，尝试移除attribute
                // 这一步在大多数现代浏览器中是不必要的，但为了保持最大程度的兼容性保留
                try {
                    table.style.removeAttribute("border-radius");
                } catch (error) {

                }
                // 清除高度
                var contentRow;
                if (table.rows) {
                    contentRow = table.rows[0];
                } else if (table.firstChild.nodeName == "TD") {
                    contentRow = table.firstChild;
                } else if (table.firstChild.nodeName == "TBODY") {
                    contentRow = table.firstChild.firstChild;
                }
                if (contentRow != null) {
                    contentRow.removeAttribute("height");
                }
                for (let iCount2 = 0; iCount2 < contentRow.childNodes.length; iCount2++) {
                    let tdNode = contentRow.childNodes[iCount2];
                    if (tdNode.nodeName === "TD" && tdNode.id === "dcGlobalRootElement") {
                        tdNode.style.width = ""; // 清空宽度
                        tdNode.removeAttribute("width"); // 移除属性
                    }
                }
            }
        }
        doc.body.style.cssText = "padding-left:1px;padding-top:0px;padding-right:0px;padding-bottom:0px;margin:0px";
        // 设置样式
        var styleNode = doc.createElement("style");
        var styleNodeHTML = "P{margin:0px}.hiddenforprint{display:none;}";
        styleNodeHTML += "@page{margin-left:0cm;margin-top:0cm;margin-right:0cm;margin-bottom:0cm;";
        if (IsLandscape) {
            styleNodeHTML += "size: landscape;";
        }
        styleNodeHTML += "}";
        styleNode.innerHTML = styleNodeHTML;
        doc.head.appendChild(styleNode);

        // 清除dcRootCenter元素，保留内部
        while (dcRootCenter.firstChild) {
            doc.body.appendChild(dcRootCenter.firstChild);
        }
        dcRootCenter.remove();
        var HTMLStr = doc.documentElement.outerHTML;
        domparser = null; // 断开引用，允许垃圾回收
        // console.log("🚀 ~ doc:", doc);
        return HTMLStr;
    },

    /**
     * 设置区域选择打印
     * 将区域选择的代码移动到WriterControl_Print.js中
     * @param {string | HTMLElement} containerID 容器元素编号或者编辑器元素
     * @param {boolean} boolean 是否开启区域选择打印,默认为true
     * @param {boolean} reverse 开启反向选择遮盖选中的区域打印其他的,默认为true
     * @returns 
     */
    SetBoundsSelectionViewMode: function (containerID, boolean, reverse) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return;
        }
        if (rootElement.getAttribute("BoundsSelectionViewModeUseNewLogic") == "true") {
            // 使用新逻辑的区域选择打印
            WriterControl_Print.SetBoundsSelectionViewModePro(rootElement, boolean, reverse);
            return;
        }
        // 如果页面中已经开启区域选择打印，并且再次调用了开启区域选择打印，就需要禁止第二次开启
        if (boolean === true && rootElement.RectInfo) {
            return;
        }
        // 开启
        if (boolean === true) {
            // var allMaskCanvas = [];
            if (!rootElement.RectInfo) {
                rootElement.RectInfo = {
                    showCanvas: [], //被选中的canvas
                    coordinate: [], //坐标   0是起始位置   1是结束位置  2是宽   3是高
                    mousedown: false, //是否被按下
                    allCanvas: [], //所有的canvas元素
                    scale: 1, //缩放比例
                    reverse: reverse,//是否反向选择
                    AdjustBoundsSelectionStyle: AdjustBoundsSelectionStyle,// 修正区域选择打印蒙版位置
                    printPreviewOpen: false,
                    printPreviewFun: PrintPreviewFun,
                    UseHTMLPrintFunc: UseHTMLPrintFunc,// 使用HTML打印
                    SectionPrintBoundsInfoFunc: SectionPrintBoundsInfoFunc// 获取打印区域信息
                };
            }
            var allPagesNode;
            /** 获取需要处理的元素对象 */
            var pageContainer = rootElement.querySelector('[dctype="page-container"]');
            /** 是否是打印预览模式 */
            var isPrintPreview = rootElement.IsPrintPreview();
            //找到所有的页面元素
            if (isPrintPreview) {
                pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                // 设置区域选择打印时打印预览内容无法选中
                pageContainer.style.userSelect = "none";
                rootElement.RectInfo.printPreviewOpen = true;
                // 打印预览模式下获取svg元素
                allPagesNode = pageContainer.querySelectorAll('svg[dctype="page"]');
            } else {
                // 编辑模式下获取canvas元素
                allPagesNode = pageContainer.querySelectorAll('canvas[dctype="page"]');
            }
            //循环添加蒙版
            for (var i = 0; i < allPagesNode.length; i++) {
                var thisPage = allPagesNode[i];
                var maskCanvas = rootElement.ownerDocument.createElement("canvas");
                // 使用 Array.forEach 来遍历属性，避免使用相同的索引变量 i
                Array.from(thisPage.attributes).forEach((attr) => {
                    maskCanvas.setAttribute(attr.name, attr.value);
                });
                maskCanvas.setAttribute("dctype", "maskPage");
                maskCanvas.setAttribute("pageIndex", i);
                maskCanvas.setAttribute("draggable", "false");
                maskCanvas.style.position = "absolute";
                // 修复存在图片水印，开启区域选择打印时页面展示不全的问题【DUWRITER5_0-3163】
                maskCanvas.style.background = "transparent";
                // 确保padding和border不被包含在定义的width和height之内
                maskCanvas.style.boxSizing = "content-box";
                pageContainer.insertBefore(maskCanvas, thisPage);
                clearRect(maskCanvas);
                maskCanvas.addEventListener("mousedown", downEvent);
                maskCanvas.addEventListener("mousemove", moveEvent);
                rootElement.RectInfo.allCanvas.push(maskCanvas);
            }
            rootElement.RectInfo.AdjustBoundsSelectionStyle();
        } else {
            //清掉所有的canvas
            if (rootElement.RectInfo && rootElement.RectInfo.allCanvas) {
                for (var y = rootElement.RectInfo.allCanvas.length - 1; y >= 0; y--) {
                    var maskCanvas = rootElement.RectInfo.allCanvas[y];
                    maskCanvas.removeEventListener("mousedown", downEvent);
                    maskCanvas.removeEventListener("mousemove", moveEvent);
                    maskCanvas.remove();
                }
                delete rootElement.RectInfo;
            }
            /** 是否是打印预览模式 */
            var isPrintPreview = rootElement.IsPrintPreview();
            // 找到所有的页面元素
            if (isPrintPreview) {
                var pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                if (pageContainer) {
                    // 取消区域选择打印时还原打印预览内容可以选中
                    pageContainer.style.userSelect = "";
                }
            }
        }

        //按下操作
        function downEvent(e) {
            /**按下shift键，不清除之前的选择 */
            var PressTheShift = e.shiftKey;
            e.stopPropagation();
            if (PressTheShift == false) {
                // 此处清空样式
                if (rootElement.RectInfo.allCanvas && rootElement.RectInfo.allCanvas.length > 0) {
                    for (var z = 0; z < rootElement.RectInfo.allCanvas.length; z++) {
                        var thisRectCanvas = rootElement.RectInfo.allCanvas[z];
                        clearRect(thisRectCanvas);
                    }
                }
                // 清空数据
                rootElement.RectInfo.showCanvas = [];
                rootElement.RectInfo.coordinate = [];
            }
            // 存储当前点击数据
            rootElement.RectInfo.showCanvas.push(e.target);
            rootElement.RectInfo.coordinate.push([e.offsetX, e.offsetY, 0, 0]);
            //通过native-width和width计算比例
            var nativeWidth = parseFloat(e.target.getAttribute('native-width'));
            rootElement.RectInfo.scale = parseFloat((e.target.width / nativeWidth).toFixed(2));
            rootElement.RectInfo.mousedown = true;
            //开始鼠标按下
            rootElement.ownerDocument.addEventListener("mouseup", upEvent);
        };
        //移动操作
        function moveEvent(e) {
            /**按下shift键，不清除之前的选择 */
            // var PressTheShift = e.shiftKey;
            e.stopPropagation();
            if (rootElement.RectInfo && rootElement.RectInfo.mousedown) {
                // 获取到目前被选中的canvas
                var selectedCanvas = rootElement.RectInfo.showCanvas[rootElement.RectInfo.showCanvas.length - 1];
                // 获取到目前被选中的canvas的坐标
                var selectedCanvasCoordinate = rootElement.RectInfo.coordinate[rootElement.RectInfo.coordinate.length - 1];
                if (e.target != selectedCanvas) {
                    // 选中选中了新的页面，进行下面的处理
                    // 记录旧数据
                    var oldselectedCanvas = selectedCanvas,
                        oldselectedCanvasCoordinate = selectedCanvasCoordinate;
                    selectedCanvas = e.target;
                    selectedCanvasCoordinate = [];
                    // 选中了新的页面，将新的页面覆盖当前选中的页面
                    rootElement.RectInfo.showCanvas.pop();
                    rootElement.RectInfo.showCanvas.push(selectedCanvas);
                    // 将新的页面的坐标添加到坐标数组中
                    rootElement.RectInfo.coordinate.pop();
                    rootElement.RectInfo.coordinate.push(selectedCanvasCoordinate);
                    // 通过native-width和width计算比例
                    var nativeWidth = parseFloat(e.target.getAttribute('native-width'));
                    rootElement.RectInfo.scale = parseFloat((e.target.width / nativeWidth).toFixed(2));
                    // 计算原点位置
                    var baseY = parseFloat(oldselectedCanvas.style.top);
                    var baseX = parseFloat(oldselectedCanvas.style.left);
                    var thisOffsetY = parseFloat(selectedCanvas.style.top);
                    var thisOffsetX = parseFloat(selectedCanvas.style.left);
                    // 在按下元素下面
                    if (baseX == thisOffsetX && baseY > thisOffsetY) {
                        selectedCanvasCoordinate[0] = oldselectedCanvasCoordinate[0];
                        selectedCanvasCoordinate[1] = selectedCanvas.offsetHeight;
                    }
                    // 在按下元素上面
                    if (baseX == thisOffsetX && baseY < thisOffsetY) {
                        selectedCanvasCoordinate[0] = oldselectedCanvasCoordinate[0];
                        selectedCanvasCoordinate[1] = 0;
                    }
                    // 在按下元素左边
                    if (baseX > thisOffsetX && baseY == thisOffsetY) {
                        selectedCanvasCoordinate[0] = selectedCanvas.offsetWidth;
                        selectedCanvasCoordinate[1] = oldselectedCanvasCoordinate[1];
                    }
                    // 在按下元素右边
                    if (baseX < thisOffsetX && baseY == thisOffsetY) {
                        selectedCanvasCoordinate[0] = 0;
                        selectedCanvasCoordinate[1] = oldselectedCanvasCoordinate[1];
                    }
                    // 在按下元素左上
                    if (baseX > thisOffsetX && baseY > thisOffsetY) {
                        selectedCanvasCoordinate[0] = selectedCanvas.offsetWidth;
                        selectedCanvasCoordinate[1] = selectedCanvas.offsetHeight;
                    }
                    // 在按下元素右上
                    if (baseX < thisOffsetX && baseY > thisOffsetY) {
                        selectedCanvasCoordinate[0] = 0;
                        selectedCanvasCoordinate[1] = selectedCanvas.offsetHeight;
                    }
                    // 在按下元素左下
                    if (baseX > thisOffsetX && baseY < thisOffsetY) {
                        selectedCanvasCoordinate[0] = selectedCanvas.offsetWidth;
                        selectedCanvasCoordinate[1] = 0;
                    }
                    // 在按下元素右下
                    if (baseX < thisOffsetX && baseY < thisOffsetY) {
                        selectedCanvasCoordinate[0] = 0;
                        selectedCanvasCoordinate[1] = 0;
                    }
                }
                var w = e.offsetX - selectedCanvasCoordinate[0];
                var y = e.offsetY - selectedCanvasCoordinate[1];
                selectedCanvasCoordinate[2] = w;
                selectedCanvasCoordinate[3] = y;
                refreshCanvas();
            }
        };
        //弹起操作
        function upEvent() {
            //关闭此操作
            rootElement.RectInfo.mousedown = false;
            rootElement.ownerDocument.removeEventListener("mouseup", upEvent);
        }
        // 刷新选中区域
        function refreshCanvas() {
            if (!rootElement.RectInfo) {
                return;
            }
            for (var i = 0; i < rootElement.RectInfo.allCanvas.length; i++) {
                clearRect(rootElement.RectInfo.allCanvas[i]);
            }
            var ShowCanvas = rootElement.RectInfo.showCanvas;
            var Coordinates = rootElement.RectInfo.coordinate;
            for (var i = 0; i < ShowCanvas.length; i++) {
                var Canvas = ShowCanvas[i];
                var Coordinate = Coordinates[i];
                var ctxStyle = Canvas.getContext('2d');
                ctxStyle.strokeStyle = "rgba(255,0,0,0.8)";
                /*var devicePixelRatio = rootElement.RectInfo.scale && rootElement.RectInfo.scale != devicePixelRatio ? rootElement.RectInfo.scale : window.devicePixelRatio;*/
                var styleWidth = Canvas.style.width;
                if (styleWidth == "") {
                    Canvas.style.width = Canvas.clientWidth + 'px';
                }
                var devicePixelRatio = Number(Canvas.width / parseFloat(Canvas.style.width).toFixed(2));
                //var devicePixelRatio = window.devicePixelRatio;
                //if (devicePixelRatio != 1) {
                //    devicePixelRatio = rootElement.RectInfo.scale;
                //    if (rootElement.RectInfo.scale != 1) {

                //    }
                //}
                ctxStyle.strokeRect(Coordinate[0] * devicePixelRatio, Coordinate[1] * devicePixelRatio, Coordinate[2] * devicePixelRatio, Coordinate[3] * devicePixelRatio);
                if (rootElement.RectInfo.reverse) {
                    ctxStyle.fillStyle = "rgba(200,200,200,0.5)";
                    ctxStyle.fillRect(Coordinate[0] * devicePixelRatio, Coordinate[1] * devicePixelRatio, Coordinate[2] * devicePixelRatio, Coordinate[3] * devicePixelRatio);
                } else {
                    ctxStyle.clearRect(Coordinate[0] * devicePixelRatio, Coordinate[1] * devicePixelRatio, Coordinate[2] * devicePixelRatio, Coordinate[3] * devicePixelRatio);
                }
            }
        }
        //清除样式
        function clearRect(canvas) {
            var ctxStyle = canvas.getContext('2d');
            ctxStyle.fillStyle = "rgba(200,200,200,0.5)";
            if (rootElement.RectInfo.reverse) {
                ctxStyle.fillStyle = "rgba(233,233,233,0.3)";
            }
            ctxStyle.clearRect(0, 0, canvas.width, canvas.height);
            ctxStyle.fillRect(0, 0, canvas.width, canvas.height);
        }
        //打印预览事件
        function PrintPreviewFun(pageContainer, setZoomRate, type) {
            if (setZoomRate) {
                rootElement.RectInfo.printPreviewOpen = true;
            } else {
                setZoomRate = 1;
            }
            var Canvas = rootElement.RectInfo.showCanvas[0];
            if (!Canvas) {
                rootElement.SetBoundsSelectionViewMode(false);
                return;
            }
            var styleWidth = Canvas.style.width;
            if (styleWidth == "") {
                Canvas.style.width = Canvas.clientWidth + "px";
            }
            var devicePixelRatio = Number(Canvas.width / parseFloat(Canvas.style.width).toFixed(2));
            setZoomRate = setZoomRate * devicePixelRatio;
            //找到具体使用的canvas
            if (rootElement.RectInfo && rootElement.RectInfo.showCanvas) {
                //判断是否存在之前的数据
                var hasOldMask = pageContainer.querySelectorAll('canvas[dctype="PrintMaskPage"]');
                if (hasOldMask.length > 0) {
                    for (var y = 0; y < hasOldMask.length; y++) {
                        hasOldMask[y].remove();
                    }
                } else {
                    hasOldMask = pageContainer.querySelectorAll('canvas[dctype="maskPage"]');
                    if (hasOldMask.length > 0) {
                        rootElement.SetBoundsSelectionViewMode(false);
                        return;
                    }
                }
                //留下的坐标
                var leaveIndex = [];
                for (var i = 0; i < rootElement.RectInfo.showCanvas.length; i++) {
                    leaveIndex.push(Number(rootElement.RectInfo.showCanvas[i].getAttribute("pageindex")));
                }
                /** 缩放倍数 */
                // var pageZoom = Number(pageContainer.style.zoom);
                // pageZoom = pageZoom ? pageZoom * rootElement.RectInfo.scale : 1;
                var pageZoom = type == "print" ? rootElement.RectInfo.scale : 1;
                // var dctypeName = pageContainer.getAttribute('dctype');
                // if (dctypeName) {
                //     var printPageNodeList = pageContainer.querySelectorAll('canvas[dctype="page"]');
                // } else {
                //     var printPageNodeList = pageContainer.querySelectorAll('canvas');
                // }
                var printPageNodeList = pageContainer.querySelectorAll("svg");
                printPageNodeList = Array.prototype.slice.call(printPageNodeList);
                if (!rootElement.RectInfo.reverse) {
                    for (var z = printPageNodeList.length - 1; z >= 0; z--) {
                        var hasIndex = leaveIndex.indexOf(z);
                        if (hasIndex < 0) {
                            printPageNodeList[z].remove();
                            printPageNodeList.splice(z, 1, null);
                        }
                    }
                }
                //再次循环是因为多栏显示时的位置问题
                for (var z = 0; z < printPageNodeList.length; z++) {
                    if (rootElement.RectInfo.reverse && leaveIndex.indexOf(z) < 0) {
                        continue;
                    }
                    var thisPageNode = printPageNodeList[z];
                    if (thisPageNode == null) {
                        continue;
                    }
                    // 创建遮罩canvas
                    var maskCanvas = rootElement.ownerDocument.createElement("canvas");
                    // 使用 Array.forEach 来遍历属性，避免使用相同的索引变量 i
                    Array.from(thisPageNode.attributes).forEach((attr) => {
                        maskCanvas.setAttribute(attr.name, attr.value);
                    });
                    maskCanvas.setAttribute("dctype", "PrintMaskPage");
                    maskCanvas.setAttribute("pageIndex", i);
                    maskCanvas.style.position = "absolute";
                    maskCanvas.style.background = "transparent";
                    // 确保padding和border不被包含在定义的width和height之内
                    maskCanvas.style.boxSizing = "content-box";
                    var ctxStyle = maskCanvas.getContext('2d');
                    if (rootElement.RectInfo.reverse) {
                        ctxStyle.fillStyle = "rgba(255,255,255)";
                        ctxStyle.clearRect(0, 0, maskCanvas.width, maskCanvas.height);
                        for (var leaveI = 0; leaveI < leaveIndex.length; leaveI++) {
                            if (leaveIndex[leaveI] == z) {
                                var thisCoordinate = rootElement.RectInfo.coordinate[leaveI];
                                ctxStyle.fillRect(thisCoordinate[0] / pageZoom * setZoomRate, thisCoordinate[1] / pageZoom * setZoomRate, thisCoordinate[2] / pageZoom * setZoomRate, thisCoordinate[3] / pageZoom * setZoomRate);
                            }
                        }
                    } else {
                        ctxStyle.fillStyle = "rgba(255,255,255)";
                        ctxStyle.clearRect(0, 0, maskCanvas.width, maskCanvas.height);
                        ctxStyle.fillRect(0, 0, maskCanvas.width, maskCanvas.height);
                        for (var leaveI = 0; leaveI < leaveIndex.length; leaveI++) {
                            if (leaveIndex[leaveI] == z) {
                                var thisCoordinate = rootElement.RectInfo.coordinate[leaveI];
                                ctxStyle.clearRect(thisCoordinate[0] / pageZoom * setZoomRate, thisCoordinate[1] / pageZoom * setZoomRate, thisCoordinate[2] / pageZoom * setZoomRate, thisCoordinate[3] / pageZoom * setZoomRate);
                            }
                        }
                    }
                    if (type == "print") {
                        //创建div包裹元素然后插入子元素防止top错位
                        var containerDiv = document.createElement("div");
                        containerDiv.style.cssText = "position: relative;overflow: hidden;";
                        thisPageNode.parentNode.insertBefore(containerDiv, thisPageNode);
                        containerDiv.appendChild(thisPageNode);
                        // maskCanvas.style.top = "0px";
                        // maskCanvas.style.left = "0px";
                        // 区域选择打印时使用Canvas生成的图片，避免Canvas打印完全覆盖的问题（chrome93版本）【DUWRITER5_0-4018】
                        var imgSrc = maskCanvas.toDataURL("image/png", 1);
                        var imgNode = document.createElement("img");
                        imgNode.src = imgSrc;
                        imgNode.style.cssText = "position:absolute;top:0;left:0;";
                        containerDiv.appendChild(imgNode);
                        // pageContainer = containerDiv;
                    } else {
                        thisPageNode.parentNode.insertBefore(maskCanvas, thisPageNode);
                    }
                }
                if (type != "print") {
                    // 非打印时需要处理模板位置
                    // 处理多栏展示情况下，编辑模式下区域选择打印或者反向区域选择打印第二页，开启打印预览，蒙版区域位置不对的问题【DUWRITER5_0-2828】
                    rootElement.RectInfo.AdjustBoundsSelectionStyle();
                }
                // if(!dctypeName){
                //     rootElement.SetBoundsSelectionViewMode(false)
                // }
            }
        }
        /**
         * 使用HTML打印函数
         * @param {Document} doc - 要打印的文档对象
         * 
         * 此函数用于处理特定文档的打印逻辑。它首先验证传入的文档对象是否有效，
         * 然后检查是否有配置信息指示哪些元素应该被打印。
         * 如果文档无效或没有指定要打印的元素，则函数将直接返回，
         * 不执行任何打印操作。
         */
        function UseHTMLPrintFunc(doc) {
            // 验证传入的doc是否为有效的Document对象
            if (!doc || doc.nodeName != "#document") {
                return;
            }
            // 检查是否有配置信息指示哪些元素应该被打印
            if (!rootElement.RectInfo || !rootElement.RectInfo.showCanvas || rootElement.RectInfo.showCanvas.length == 0) {
                return;
            }
            // 留下的坐标
            var leaveIndex = [];
            var pageZoom = rootElement.GetZoomRate();
            // var setZoomRate;
            // 获取到第一个有值的rootElement.RectInfo.showCanvas
            for (var i = 0; i < rootElement.RectInfo.showCanvas.length; i++) {
                if (rootElement.RectInfo.showCanvas[i]) {
                    var Canvas = rootElement.RectInfo.showCanvas[i];
                    leaveIndex.push(Number(Canvas.getAttribute('pageindex')));
                    // if (setZoomRate === undefined) {
                    //     setZoomRate = Number(parseFloat(Canvas.getAttribute("native-width")).toFixed(2) / Canvas.width);
                    // }
                }
            }
            /** 打印的div数组 */
            var PageDivs = doc.querySelectorAll("#dcRootCenter > div[pageindex]");
            if (PageDivs && PageDivs.length > 0) {
                for (var i = 0; i < PageDivs.length; i++) {
                    var PageDiv = PageDivs[i];
                    var PageIndex = Number(PageDiv.getAttribute("pageindex"));
                    var showCanvas = rootElement.RectInfo.allCanvas[PageIndex];
                    if (leaveIndex.indexOf(PageIndex) < 0) {
                        if (!rootElement.RectInfo.reverse) {
                            PageDiv.remove();
                        }
                        continue;
                    }
                    var canvas = doc.createElement("canvas");
                    canvas.width = showCanvas.getAttribute("native-width");
                    canvas.height = showCanvas.getAttribute("native-height");
                    // 绘制蒙版画布
                    var ctxStyle = canvas.getContext("2d");
                    if (!ctxStyle) {
                        continue;
                    }
                    if (rootElement.RectInfo.reverse) {
                        ctxStyle.fillStyle = "rgba(255,255,255)";
                        for (var leaveI = 0; leaveI < leaveIndex.length; leaveI++) {
                            if (leaveIndex[leaveI] == i) {
                                var thisCoordinate = rootElement.RectInfo.coordinate[leaveI];
                                ctxStyle.fillRect(thisCoordinate[0] / pageZoom, thisCoordinate[1] / pageZoom, thisCoordinate[2] / pageZoom, thisCoordinate[3] / pageZoom);
                            }
                        }
                    } else {
                        ctxStyle.fillStyle = "rgba(255,255,255)";
                        ctxStyle.fillRect(0, 0, canvas.width, canvas.height);
                        for (var leaveI = 0; leaveI < leaveIndex.length; leaveI++) {
                            if (leaveIndex[leaveI] == i) {
                                var thisCoordinate = rootElement.RectInfo.coordinate[leaveI];
                                ctxStyle.clearRect(thisCoordinate[0] / pageZoom, thisCoordinate[1] / pageZoom, thisCoordinate[2] / pageZoom, thisCoordinate[3] / pageZoom);
                            }
                        }
                    }
                    PageDiv.style.position = "relative";
                    PageDiv.style.overflow = "hidden";
                    var imgSrc = canvas.toDataURL("image/png", 1);
                    var imgNode = doc.createElement("img");
                    imgNode.src = imgSrc;
                    imgNode.style.cssText = "position:absolute;top:0;left:0;";
                    PageDiv.appendChild(imgNode);
                }
            }
        }
        /**
         * 获取区域选择打印时的打印病程的id值
         * 目前只用在WriterControl_Print.GetSectionPrintBoundsInfo
         * @param {HTMLElement} rootElement - 所有段落元素的父容器，函数将在此容器内寻找所有段落元素
         * @param {Function} SectionPrintBoundsInfo - 一个函数类型参数，用于处理并展示段落的边界信息
         */
        function SectionPrintBoundsInfoFunc(rootElement, SectionPrintBoundsInfo) {
            let subIDsArray = new Array();
            // 检查是否有配置信息指示哪些元素应该被打印
            if (!rootElement.RectInfo || !rootElement.RectInfo.showCanvas || rootElement.RectInfo.showCanvas.length == 0) {
                for (var i = 0; i < SectionPrintBoundsInfo.length; i++) {
                    var SectionPrintBoundInfo = SectionPrintBoundsInfo[i];
                    if (subIDsArray.indexOf(SectionPrintBoundInfo.ID) > -1) {
                        // 跳过已经添加的病程
                        continue;
                    }
                    subIDsArray.push(SectionPrintBoundInfo.ID);
                }
                return subIDsArray;
            }
            // 留下的坐标
            var leaveIndex = [];
            var setZoomRate;
            // 获取到第一个有值的rootElement.RectInfo.showCanvas
            for (var i = 0; i < rootElement.RectInfo.showCanvas.length; i++) {
                if (rootElement.RectInfo.showCanvas[i]) {
                    var Canvas = rootElement.RectInfo.showCanvas[i];
                    leaveIndex.push(Number(Canvas.getAttribute('pageindex')));
                    if (setZoomRate === undefined) {
                        setZoomRate = Number(parseFloat(Canvas.getAttribute("native-width")).toFixed(2) / Canvas.width);
                    }
                }
            }
            for (var i = 0; i < SectionPrintBoundsInfo.length; i++) {
                var SectionPrintBoundInfo = SectionPrintBoundsInfo[i];
                var PageIndex = SectionPrintBoundInfo.PageIndex;
                if (subIDsArray.indexOf(SectionPrintBoundInfo.ID) > -1) {
                    // 跳过已经添加的病程
                    continue;
                }
                if (leaveIndex.indexOf(PageIndex) < 0) {
                    // 没有选中的内容
                    if (rootElement.RectInfo.reverse) {
                        // 反向选择
                        subIDsArray.push(SectionPrintBoundInfo.ID);
                    }
                    continue;
                }
                if (rootElement.RectInfo.reverse) {
                    // 反向选择
                    // 当前病程全部的面积
                    const totalArea = SectionPrintBoundInfo.Width * SectionPrintBoundInfo.Height;
                    // 选择区域的存储数组
                    var SelectionArray = [];
                    function handleOverlap(area1) {
                        // 判断是否有重叠
                        var isPush = true;
                        for (var j = 0; j < SelectionArray.length; j++) {
                            var area2 = SelectionArray[j];
                            // 判断是否包含在内部
                            if (area1.left >= area2.left && area1.top >= area2.top && area1.left + area1.width <= area2.left + area2.width && area1.top + area1.height <= area2.top + area2.height) {
                                // 包含在内部
                                isPush = false;
                                break;
                            }
                            const overlapX = Math.max(0, Math.min(area1.left + area1.width, area2.left + area2.width) - Math.max(area1.left, area2.left));
                            const overlapY = Math.max(0, Math.min(area1.top + area1.height, area2.top + area2.height) - Math.max(area1.top, area2.top));
                            // 如果有重叠，则分割成四个子区域
                            if (overlapX > 0 && overlapY > 0) {
                                var subAreas;
                                if (area1.left >= area2.left) {
                                    // 左重叠
                                    subAreas = [
                                        { left: area1.left, top: area1.top, width: overlapX, height: overlapY },
                                        { left: area1.left + overlapX, top: area1.top, width: area1.width - overlapX, height: overlapY },
                                        { left: area1.left, top: area1.top + overlapY, width: overlapX, height: area1.height - overlapY },
                                        { left: area1.left + overlapX, top: area1.top + overlapY, width: area1.width - overlapX, height: area1.height - overlapY }
                                    ];
                                } else {
                                    // 右重叠
                                    subAreas = [
                                        { left: area1.left, top: area1.top, width: area1.width - overlapX, height: overlapY },
                                        { left: area1.left + area1.width - overlapX, top: area1.top, width: overlapX, height: overlapY },
                                        { left: area1.left, top: area1.top + overlapY, width: area1.width - overlapX, height: area1.height - overlapY },
                                        { left: area1.left + area1.width - overlapX, top: area1.top + overlapY, width: overlapX, height: area1.height - overlapY }
                                    ];
                                }
                                subAreas = subAreas.filter(subArea => subArea.width > 0 && subArea.height > 0);
                                for (var k = 0; k < subAreas.length; k++) {
                                    handleOverlap(subAreas[k]);
                                }
                                isPush = false;
                                break;
                            }
                        }
                        if (isPush && area1.width > 0 && area1.height > 0) {
                            SelectionArray.push(area1);
                        }
                    }
                    for (var leaveI = 0; leaveI < leaveIndex.length; leaveI++) {
                        if (leaveIndex[leaveI] == PageIndex) {
                            var thisCoordinate = rootElement.RectInfo.coordinate[leaveI];
                            // left top width height
                            var _left = thisCoordinate[0] * setZoomRate;
                            var _top = thisCoordinate[1] * setZoomRate;
                            var _width = thisCoordinate[2] * setZoomRate;
                            var _height = thisCoordinate[3] * setZoomRate;
                            // 判断左右有没有选择到文档部分
                            if (_left + _width <= SectionPrintBoundInfo.Left || _left >= SectionPrintBoundInfo.Left + SectionPrintBoundInfo.Width) {
                                continue;
                            }
                            // 判断上下有没有选择到文档部分
                            if (_top + _height <= SectionPrintBoundInfo.Top || _top >= SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height) {
                                continue;
                            }
                            if (_left < SectionPrintBoundInfo.Left) {
                                // 左边框超出病程
                                _width -= SectionPrintBoundInfo.Left - _left;
                                _left = SectionPrintBoundInfo.Left;
                            }
                            if (_left + _width > SectionPrintBoundInfo.Left + SectionPrintBoundInfo.Width) {
                                // 右边框超出病程
                                var sxwidth = _left + _width - (SectionPrintBoundInfo.Left + SectionPrintBoundInfo.Width);
                                _width -= sxwidth;
                            }
                            if (_top < SectionPrintBoundInfo.Top) {
                                // 上边框超出病程
                                _height -= SectionPrintBoundInfo.Top - _top;
                                _top = SectionPrintBoundInfo.Top;
                            }
                            if (_top + _height > SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height) {
                                // 下边框超出病程
                                var syheight = _top + _height - (SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height);
                                _height -= syheight;
                            }
                            var area = { left: _left, top: _top, width: _width, height: _height };
                            handleOverlap(area);
                        }
                    }
                    // 获取面积
                    var totalSelectionArea = 0;
                    for (let k = 0; k < SelectionArray.length; k++) {
                        var selectionArea = SelectionArray[k].width * SelectionArray[k].height;
                        totalSelectionArea += selectionArea;
                    }
                    if (totalArea - totalSelectionArea > 0) {
                        // 存在可以打印的内容
                        subIDsArray.push(SectionPrintBoundInfo.ID);
                    }
                } else {
                    // 正常选择
                    for (var leaveI = 0; leaveI < leaveIndex.length; leaveI++) {
                        if (leaveIndex[leaveI] == PageIndex) {
                            var thisCoordinate = rootElement.RectInfo.coordinate[leaveI];
                            // left top width height
                            var _left = thisCoordinate[0] * setZoomRate;
                            var _top = thisCoordinate[1] * setZoomRate;
                            var _width = thisCoordinate[2] * setZoomRate;
                            var _height = thisCoordinate[3] * setZoomRate;
                            // 判断左右有没有选择到文档部分
                            if (_left + _width <= SectionPrintBoundInfo.Left || _left >= SectionPrintBoundInfo.Left + SectionPrintBoundInfo.Width) {
                                continue;
                            }
                            // 判断上下有没有选择到文档部分
                            if (_top + _height <= SectionPrintBoundInfo.Top || _top >= SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height) {
                                continue;
                            }
                            subIDsArray.push(SectionPrintBoundInfo.ID);
                        }
                    }
                }
            }
            return subIDsArray;
        }
        // 调整区域选择蒙版样式
        function AdjustBoundsSelectionStyle() {
            var NeedAdjustCanvass = this.allCanvas;
            if (this.printPreviewOpen == false && rootElement.IsPrintPreview() == true) {
                // 处理编辑模式下区域选择打印或者反向区域选择打印，开启打印预览，编辑器的宽度变化，导致蒙版区域位置不对的问题【DUWRITER5_0-2828】
                // 区域选择打印是编辑模式下，但是目前编辑器模式为打印预览，需要调整区域不一样
                var pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                if (pageContainer) {
                    NeedAdjustCanvass = pageContainer.querySelectorAll("[dctype='PrintMaskPage']");
                }
                if (!NeedAdjustCanvass || NeedAdjustCanvass.length == 0) {
                    NeedAdjustCanvass = this.allCanvas;
                }
            }
            if (!NeedAdjustCanvass) {
                return;
            }
            var DivRect;
            for (var i = 0; i < NeedAdjustCanvass.length; i++) {
                // 蒙版的canvas
                var maskPageCanvas = NeedAdjustCanvass[i];
                // 页面展示的canvas或者svg
                var pageNode = maskPageCanvas.nextElementSibling;
                if (maskPageCanvas.offsetWidth == 0 || maskPageCanvas.offsetHeight == 0) {
                    // 界面被隐藏
                    return;
                }
                if (DivRect == null) {
                    DivRect = maskPageCanvas.parentNode.getBoundingClientRect();
                }
                var PageNodeRect = pageNode.getBoundingClientRect();
                // 获取页面元素的所有样式
                var pageNodeStyles = GetNodeStyles(pageNode);
                var _top = maskPageCanvas.parentNode.scrollTop + PageNodeRect.top - DivRect.top - (parseFloat(pageNodeStyles.marginTop) | 0);
                var _left = maskPageCanvas.parentNode.scrollLeft + PageNodeRect.left - DivRect.left;
                var strMode = rootElement.getAttribute("pagelayoutmode");
                strMode = strMode ? strMode.trim().toLocaleLowerCase() : "";
                if (strMode != "singlecolumn") {
                    // 不是单栏展示
                    _left -= (parseFloat(pageNodeStyles.marginLeft) | 0);
                }
                maskPageCanvas.style.top = _top + "px";
                maskPageCanvas.style.left = _left + "px";
            }
            /** 获取元素所有的样式对象
            * @param {node} node 元素
            * @return {object} 元素样式对象
            */
            function GetNodeStyles(node) {
                // 兼容IE和火狐谷歌等的写法
                var computedStyle = window.getComputedStyle && window.getComputedStyle(node, null) || node.currentStyle;
                return computedStyle || {};
            }
        }
    },

    /**
     * 新逻辑的设置区域选择打印
     * 将区域选择的代码移动到WriterControl_Print.js中
     * @param {string | HTMLElement} containerID 容器元素编号或者编辑器元素
     * @param {boolean} boolean 是否开启区域选择打印,默认为true
     * @param {boolean} reverse 开启反向选择遮盖选中的区域打印其他的,默认为true
     * @returns 
     */
    SetBoundsSelectionViewModePro: function (containerID, boolean, reverse) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return;
        }
        // 如果页面中已经开启区域选择打印，并且再次调用了开启区域选择打印，就需要禁止第二次开启
        if (boolean === true && rootElement.RectInfo) {
            return;
        }
        if (rootElement.RectInfo) {
            // 移除事件监听,防止重复添加
            var pageContainer = rootElement.RectInfo.pageContainer;
            if (pageContainer) {
                if (typeof (pageContainer.removeEventListener) === "function" && typeof (rootElement.RectInfo.HandleMouseForBoundsSelectionViewMode) === "function") {
                    // 安全地移除事件监听器
                    ["mousedown", "mousemove", "mouseup"].forEach(function (event) {
                        pageContainer.removeEventListener(event, rootElement.RectInfo.HandleMouseForBoundsSelectionViewMode);
                    });
                }
                // 删除移动的div
                var div = pageContainer.querySelector("#" + rootElement.RectInfo.moveData.wId);
                if (div) {
                    div.remove();
                }
            }
        }
        // 开启
        if (boolean === true) {
            if (!rootElement.RectInfo) {
                rootElement.RectInfo = {
                    showCanvas: [], // 被选中的canvas
                    coordinate: [], // 坐标   0是起始位置 1是结束位置  2是宽   3是高
                    /** 所有的canvas元素 */
                    allCanvas: [],
                    pageContainer: null,
                    /** 选择框的数据 */
                    moveData: {
                        wId: "yddiv",//移动的div的id
                        startX: 0,//开始位置x
                        startY: 0,//开始位置y
                        retcLeft: "0px",//距离左边位置
                        retcTop: "0px",//距离顶部位置
                        retcHeight: "0px",//选择区域高度
                        retcWidth: "0px",//选择区域宽度
                    },
                    /** 缩放比例 */
                    scale: 1,
                    /** 是否反向选择 */
                    reverse: reverse,
                    /** 是否鼠标按下 */
                    isMouseDown: false,
                    /** 当前模式是否为打印预览 */
                    printPreviewOpen: false,
                    /**
                     * 清除画布
                     * @param {Element} canvas 画布元素
                     */
                    clearRect: function (canvas) {
                        if (!canvas || canvas.nodeName != "CANVAS") {
                            return;
                        }
                        var ctxStyle = canvas.getContext("2d");
                        if (!ctxStyle) {
                            return;
                        }
                        ctxStyle.fillStyle = "rgba(200,200,200,0.5)";
                        if (this.reverse) {
                            ctxStyle.fillStyle = "rgba(233,233,233,0.3)";
                        }
                        ctxStyle.clearRect(0, 0, canvas.width, canvas.height);
                        ctxStyle.fillRect(0, 0, canvas.width, canvas.height);
                    },
                    /**
                     * 刷新画布的选择区域
                     * @returns 
                     */
                    refreshCanvas: function () {
                        if (!rootElement.RectInfo) {
                            return;
                        }
                        for (var i = 0; i < rootElement.RectInfo.allCanvas.length; i++) {
                            rootElement.RectInfo.clearRect(rootElement.RectInfo.allCanvas[i]);
                        }
                        var ShowCanvas = rootElement.RectInfo.showCanvas;
                        var Coordinates = rootElement.RectInfo.coordinate;
                        for (var i = 0; i < ShowCanvas.length; i++) {
                            var Canvas = ShowCanvas[i];
                            // 存放选择的坐标数组
                            var CoordinateArray = Coordinates[i];
                            if (!Canvas || !CoordinateArray || CoordinateArray.length == 0) {
                                // 没有选择
                                continue;
                            }
                            var ctxStyle = Canvas.getContext("2d");
                            // 更改形状的轮廓线颜色
                            ctxStyle.strokeStyle = "rgba(255,0,0,0.8)";
                            var styleWidth = Canvas.style.width;
                            if (styleWidth == "") {
                                // 获取canvas的实际宽度
                                Canvas.style.width = Canvas.clientWidth + "px";
                            }
                            /** 缩放比例 */
                            var devicePixelRatio = Number(Canvas.width / parseFloat(Canvas.style.width).toFixed(2));
                            /*var devicePixelRatio = rootElement.RectInfo.scale && rootElement.RectInfo.scale != devicePixelRatio ? rootElement.RectInfo.scale : window.devicePixelRatio;*/
                            //var devicePixelRatio = window.devicePixelRatio;
                            //if (devicePixelRatio != 1) {
                            //    devicePixelRatio = rootElement.RectInfo.scale;
                            //    if (rootElement.RectInfo.scale != 1) {

                            //    }
                            //}
                            for (var j = 0; j < CoordinateArray.length; j++) {
                                var CoordinateX = CoordinateArray[j].left * devicePixelRatio;
                                var CoordinateY = CoordinateArray[j].top * devicePixelRatio;
                                var CoordinateWidth = CoordinateArray[j].width * devicePixelRatio;
                                var CoordinateHeight = CoordinateArray[j].height * devicePixelRatio;
                                // 绘制矩形
                                ctxStyle.strokeRect(CoordinateX, CoordinateY, CoordinateWidth, CoordinateHeight);
                                if (rootElement.RectInfo.reverse) {
                                    // 反向选择
                                    ctxStyle.fillStyle = "rgba(200,200,200,0.5)";
                                    ctxStyle.fillRect(CoordinateX, CoordinateY, CoordinateWidth, CoordinateHeight);
                                } else {
                                    ctxStyle.clearRect(CoordinateX, CoordinateY, CoordinateWidth, CoordinateHeight);
                                }
                            }
                        }
                    },
                    /**
                     * 修正区域选择打印蒙版位置
                     * @returns 
                     */
                    AdjustBoundsSelectionStyle: function () {
                        var NeedAdjustCanvass = this.allCanvas;
                        if (this.printPreviewOpen == false && rootElement.IsPrintPreview() == true) {
                            // 处理编辑模式下区域选择打印或者反向区域选择打印，开启打印预览，编辑器的宽度变化，导致蒙版区域位置不对的问题【DUWRITER5_0-2828】
                            // 区域选择打印是编辑模式下，但是目前编辑器模式为打印预览，需要调整区域不一样
                            var pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                            if (pageContainer) {
                                NeedAdjustCanvass = pageContainer.querySelectorAll('[dctype="PrintMaskPage"]');
                            }
                            if (!NeedAdjustCanvass || NeedAdjustCanvass.length == 0) {
                                NeedAdjustCanvass = this.allCanvas;
                            }
                        }
                        if (!NeedAdjustCanvass) {
                            return;
                        }
                        var DivRect;
                        for (var i = 0; i < NeedAdjustCanvass.length; i++) {
                            // 蒙版的canvas
                            var maskPageCanvas = NeedAdjustCanvass[i];
                            // 页面展示的canvas或者svg
                            var pageNode = maskPageCanvas.nextElementSibling;
                            if (maskPageCanvas.offsetWidth == 0 || maskPageCanvas.offsetHeight == 0) {
                                // 界面被隐藏
                                return;
                            }
                            if (DivRect == null) {
                                DivRect = maskPageCanvas.parentNode.getBoundingClientRect();
                            }
                            var PageNodeRect = pageNode.getBoundingClientRect();
                            // 获取页面元素的所有样式
                            var pageNodeStyles = GetNodeStyles(pageNode);
                            var _top = maskPageCanvas.parentNode.scrollTop + PageNodeRect.top - DivRect.top - (parseFloat(pageNodeStyles.marginTop) | 0);
                            var _left = maskPageCanvas.parentNode.scrollLeft + PageNodeRect.left - DivRect.left;
                            var strMode = rootElement.getAttribute("pagelayoutmode");
                            strMode = strMode ? strMode.trim().toLocaleLowerCase() : "";
                            if (strMode != "singlecolumn") {
                                // 不是单栏展示
                                _left -= (parseFloat(pageNodeStyles.marginLeft) | 0);
                            }
                            maskPageCanvas.style.top = _top + "px";
                            maskPageCanvas.style.left = _left + "px";
                        }
                        /** 获取元素所有的样式对象
                        * @param {node} node 元素
                        * @return {object} 元素样式对象
                        */
                        function GetNodeStyles(node) {
                            // 兼容IE和火狐谷歌等的写法
                            var computedStyle = window.getComputedStyle && window.getComputedStyle(node, null) || node.currentStyle;
                            return computedStyle || {};
                        }
                    },
                    /**
                     * 打印预览或者打印时需要触发的方法
                     * @param {Element} pageContainer 
                     * @param {Number} setZoomRate 缩放比例
                     * @param {string} type  类型 默认：打印预览 print：打印
                     */
                    printPreviewFun: function (pageContainer, setZoomRate, type) {
                        if (!pageContainer) {
                            return;
                        }
                        if (!rootElement.RectInfo || !rootElement.RectInfo.showCanvas || rootElement.RectInfo.showCanvas.length == 0) {
                            rootElement.SetBoundsSelectionViewMode(false);
                            return;
                        }
                        // 判断是否存在之前的数据
                        var hasOldMask = pageContainer.querySelectorAll('canvas[dctype="PrintMaskPage"]');
                        // 存在清除之前的数据
                        if (hasOldMask.length > 0) {
                            for (var y = 0; y < hasOldMask.length; y++) {
                                hasOldMask[y].remove();
                            }
                        } else {
                            hasOldMask = pageContainer.querySelectorAll('canvas[dctype="maskPage"]');
                            if (hasOldMask.length > 0) {
                                rootElement.SetBoundsSelectionViewMode(false);
                                return;
                            }
                        }
                        if (setZoomRate) {
                            // 设置为正在打印预览模式
                            rootElement.RectInfo.printPreviewOpen = true;
                        } else {
                            setZoomRate = 1;
                        }
                        // 获取到第一个有值的rootElement.RectInfo.showCanvas
                        for (var i = 0; i < rootElement.RectInfo.showCanvas.length; i++) {
                            if (rootElement.RectInfo.showCanvas[i]) {
                                var Canvas = rootElement.RectInfo.showCanvas[i];
                                var styleWidth = Canvas.style.width;
                                if (styleWidth == "") {
                                    Canvas.style.width = Canvas.clientWidth + "px";
                                }
                                var devicePixelRatio = Number(Canvas.width / parseFloat(Canvas.style.width).toFixed(2));
                                setZoomRate = setZoomRate * devicePixelRatio;
                                break;
                            }
                        }
                        /** 当前页面容器缩放比例 */
                        // var pageZoom = Number(pageContainer.style.zoom);
                        // pageZoom = pageZoom ? pageZoom * rootElement.RectInfo.scale : 1;
                        var pageZoom = type == "print" ? rootElement.RectInfo.scale : 1;
                        /** 打印的元素数组，原本的内容元素，之前是Canvas，现在是svg，不是蒙版Canvas */
                        var printPageNodeList = pageContainer.querySelectorAll("svg");
                        // var dctypeName = pageContainer.getAttribute("dctype");// 当前页面容器的dctype属性值
                        // if (dctypeName) {
                        //     printPageNodeList = pageContainer.querySelectorAll('canvas[dctype="page"]');
                        // } else {
                        //     printPageNodeList = pageContainer.querySelectorAll('canvas');
                        // }
                        // 转成数组
                        printPageNodeList = Array.prototype.slice.call(printPageNodeList);
                        for (var i = 0; i < printPageNodeList.length; i++) {
                            // 判断是否存在需要打印的元素
                            var printPageNode = printPageNodeList[i];
                            if (!printPageNode || !printPageNode.parentNode) {
                                continue;
                            }
                            var showCanvas = rootElement.RectInfo.showCanvas[i];
                            if (!showCanvas) {
                                // 不存在选择的逻辑
                                if (rootElement.RectInfo.reverse) {
                                    // 反向选择,不需要蒙版覆盖，走下面的逻辑
                                } else {
                                    // 正向选择时,不需要打印,移除
                                    printPageNode.remove();
                                }
                                continue;
                            }
                            // 创建遮罩canvas
                            var maskCanvas = rootElement.ownerDocument.createElement("canvas");
                            // 使用 Array.forEach 来遍历属性，避免使用相同的索引变量 i
                            Array.from(printPageNode.attributes).forEach((attr) => {
                                maskCanvas.setAttribute(attr.name, attr.value);
                            });
                            // 设置遮罩canvas属性
                            maskCanvas.setAttribute("dctype", "PrintMaskPage");
                            maskCanvas.setAttribute("pageIndex", i);
                            // 设置遮罩canvas样式
                            maskCanvas.style.position = "absolute";
                            maskCanvas.style.background = "transparent";
                            // 确保padding和border不被包含在定义的width和height之内
                            maskCanvas.style.boxSizing = "content-box";
                            // 绘制蒙版画布
                            var ctxStyle = maskCanvas.getContext("2d");
                            if (!ctxStyle) {
                                continue;
                            }
                            /** 所有当前页面的选择区域索引数组 */
                            var CoordinateArray = rootElement.RectInfo.coordinate[i];
                            // 清除画布所有的内容
                            ctxStyle.clearRect(0, 0, maskCanvas.width, maskCanvas.height);
                            // 填充颜色为白色
                            ctxStyle.fillStyle = "rgba(255,255,255)";
                            if (rootElement.RectInfo.reverse) {
                                // 反向选择的逻辑
                                // 将选择区域填充为白色，不打印
                                for (var j = 0; j < CoordinateArray.length; j++) {
                                    var CoordinateX = CoordinateArray[j].left / pageZoom * setZoomRate;
                                    var CoordinateY = CoordinateArray[j].top / pageZoom * setZoomRate;
                                    var CoordinateWidth = CoordinateArray[j].width / pageZoom * setZoomRate;
                                    var CoordinateHeight = CoordinateArray[j].height / pageZoom * setZoomRate;
                                    // 填充为白色
                                    ctxStyle.fillRect(CoordinateX, CoordinateY, CoordinateWidth, CoordinateHeight);
                                }
                            } else {
                                // 全部填充为白色
                                ctxStyle.fillRect(0, 0, maskCanvas.width, maskCanvas.height);
                                // 将选择区域清除内容，只打印选择区域
                                for (var j = 0; j < CoordinateArray.length; j++) {
                                    var CoordinateX = CoordinateArray[j].left / pageZoom * setZoomRate;
                                    var CoordinateY = CoordinateArray[j].top / pageZoom * setZoomRate;
                                    var CoordinateWidth = CoordinateArray[j].width / pageZoom * setZoomRate;
                                    var CoordinateHeight = CoordinateArray[j].height / pageZoom * setZoomRate;
                                    // 清除区域内容
                                    ctxStyle.clearRect(CoordinateX, CoordinateY, CoordinateWidth, CoordinateHeight);
                                }
                            }
                            if (type == "print") {
                                // 打印时的逻辑
                                // 创建div包裹元素然后插入子元素防止top错位
                                var containerDiv = document.createElement("div");
                                containerDiv.style.cssText = "position: relative;overflow: hidden;";
                                // 将div包裹元素插入到当前canvas前面，然后将当前canvas移到div包裹元素中
                                printPageNode.parentNode.insertBefore(containerDiv, printPageNode);
                                containerDiv.appendChild(printPageNode);
                                // 设置遮罩canvas样式
                                // maskCanvas.style.top = "0px";
                                // maskCanvas.style.left = "0px";
                                // 区域选择打印时使用Canvas生成的图片，避免Canvas打印完全覆盖的问题（chrome93版本）【DUWRITER5_0-4018】
                                var imgSrc = maskCanvas.toDataURL("image/png", 1);
                                var imgNode = document.createElement("img");
                                imgNode.src = imgSrc;
                                imgNode.style.cssText = "position:absolute;top:0;left:0;";
                                containerDiv.appendChild(imgNode);
                            } else {
                                // 插入蒙版画布
                                printPageNode.parentNode.insertBefore(maskCanvas, printPageNode);
                            }
                        }
                        if (type != "print") {
                            // 打印预览时的逻辑,修正区域选择打印蒙版位置
                            rootElement.RectInfo.AdjustBoundsSelectionStyle();
                        }
                    },
                    /**
                     * 使用HTML打印函数
                     * @param {Document} doc - 要打印的文档对象
                     * 
                     * 此函数用于处理特定文档的打印逻辑。它首先验证传入的文档对象是否有效，
                     * 然后检查是否有配置信息指示哪些元素应该被打印。
                     * 如果文档无效或没有指定要打印的元素，则函数将直接返回，
                     * 不执行任何打印操作。
                     */
                    UseHTMLPrintFunc: function (doc) {
                        // 验证传入的doc是否为有效的Document对象
                        if (!doc || doc.nodeName != "#document") {
                            return;
                        }
                        // 检查是否有配置信息指示哪些元素应该被打印
                        if (!rootElement.RectInfo || !rootElement.RectInfo.showCanvas || rootElement.RectInfo.showCanvas.length == 0) {
                            return;
                        }
                        var setZoomRate = 1 / (rootElement.GetZoomRate());
                        // 获取到第一个有值的rootElement.RectInfo.showCanvas
                        // for (var i = 0; i < rootElement.RectInfo.showCanvas.length; i++) {
                        //     if (rootElement.RectInfo.showCanvas[i]) {
                        //         var Canvas = rootElement.RectInfo.showCanvas[i];
                        //         // var devicePixelRatio = Number(parseFloat(Canvas.getAttribute("native-width")).toFixed(2) / Canvas.width);
                        //         // setZoomRate = setZoomRate * devicePixelRatio;
                        //         break;
                        //     }
                        // }
                        /** 打印的div数组 */
                        var PageDivs = doc.querySelectorAll("#dcRootCenter > div[pageindex]");
                        if (PageDivs && PageDivs.length > 0) {
                            for (var i = 0; i < PageDivs.length; i++) {
                                var PageDiv = PageDivs[i];
                                var showCanvas = rootElement.RectInfo.showCanvas[i];
                                if (!showCanvas) {
                                    // 不存在选择的逻辑
                                    if (rootElement.RectInfo.reverse) {
                                        // 反向选择,不需要蒙版覆盖，走下面的逻辑
                                    } else {
                                        // 正向选择时,不需要打印,移除
                                        PageDiv.remove();
                                    }
                                    continue;
                                }
                                var canvas = doc.createElement("canvas");
                                canvas.width = showCanvas.getAttribute("native-width");
                                canvas.height = showCanvas.getAttribute("native-height");
                                // 绘制蒙版画布
                                var ctxStyle = canvas.getContext("2d");
                                if (!ctxStyle) {
                                    continue;
                                }
                                /** 所有当前页面的选择区域索引数组 */
                                var CoordinateArray = rootElement.RectInfo.coordinate[i];
                                // 填充颜色为白色
                                ctxStyle.fillStyle = "rgba(255,255,255)";
                                if (rootElement.RectInfo.reverse) {
                                    // 反向选择的逻辑
                                    // 将选择区域填充为白色，不打印
                                    for (var j = 0; j < CoordinateArray.length; j++) {
                                        var CoordinateX = CoordinateArray[j].left * setZoomRate;
                                        var CoordinateY = CoordinateArray[j].top * setZoomRate;
                                        var CoordinateWidth = CoordinateArray[j].width * setZoomRate;
                                        var CoordinateHeight = CoordinateArray[j].height * setZoomRate;
                                        // 填充为白色
                                        ctxStyle.fillRect(CoordinateX, CoordinateY, CoordinateWidth, CoordinateHeight);
                                    }
                                } else {
                                    // 全部填充为白色
                                    ctxStyle.fillRect(0, 0, canvas.width, canvas.height);
                                    // 将选择区域清除内容，只打印选择区域
                                    for (var j = 0; j < CoordinateArray.length; j++) {
                                        var CoordinateX = CoordinateArray[j].left * setZoomRate;
                                        var CoordinateY = CoordinateArray[j].top * setZoomRate;
                                        var CoordinateWidth = CoordinateArray[j].width * setZoomRate;
                                        var CoordinateHeight = CoordinateArray[j].height * setZoomRate;
                                        // 清除区域内容
                                        ctxStyle.clearRect(CoordinateX, CoordinateY, CoordinateWidth, CoordinateHeight);
                                    }
                                }
                                PageDiv.style.position = "relative";
                                PageDiv.style.overflow = "hidden";
                                var imgSrc = canvas.toDataURL("image/png", 1);
                                var imgNode = doc.createElement("img");
                                imgNode.src = imgSrc;
                                imgNode.style.cssText = "position:absolute;top:0;left:0;";
                                PageDiv.appendChild(imgNode);
                            }
                        }
                    },
                    /**
                     * 获取区域选择打印时的打印病程的id值
                     * 目前只用在WriterControl_Print.GetSectionPrintBoundsInfo
                     * @param {HTMLElement} rootElement - 所有段落元素的父容器，函数将在此容器内寻找所有段落元素
                     * @param {Function} SectionPrintBoundsInfo - 一个函数类型参数，用于处理并展示段落的边界信息
                     */
                    SectionPrintBoundsInfoFunc: function (rootElement, SectionPrintBoundsInfo) {
                        let subIDsArray = new Array();
                        // 检查是否有配置信息指示哪些元素应该被打印
                        if (!rootElement.RectInfo || !rootElement.RectInfo.showCanvas || rootElement.RectInfo.showCanvas.length == 0) {
                            for (var i = 0; i < SectionPrintBoundsInfo.length; i++) {
                                var SectionPrintBoundInfo = SectionPrintBoundsInfo[i];
                                if (subIDsArray.indexOf(SectionPrintBoundInfo.ID) > -1) {
                                    // 跳过已经添加的病程
                                    continue;
                                }
                                subIDsArray.push(SectionPrintBoundInfo.ID);
                            }
                            return subIDsArray;
                        }
                        var setZoomRate = 1;
                        // 获取到第一个有值的rootElement.RectInfo.showCanvas
                        for (var i = 0; i < rootElement.RectInfo.showCanvas.length; i++) {
                            if (rootElement.RectInfo.showCanvas[i]) {
                                var Canvas = rootElement.RectInfo.showCanvas[i];
                                var devicePixelRatio = Number(parseFloat(Canvas.getAttribute("native-width")).toFixed(2) / Canvas.width);
                                setZoomRate = setZoomRate * devicePixelRatio;
                                break;
                            }
                        }
                        for (var i = 0; i < SectionPrintBoundsInfo.length; i++) {
                            var SectionPrintBoundInfo = SectionPrintBoundsInfo[i];
                            var PageIndex = SectionPrintBoundInfo.PageIndex;
                            if (subIDsArray.indexOf(SectionPrintBoundInfo.ID) > -1) {
                                // 跳过已经添加的病程
                                continue;
                            }
                            var showCanvas = rootElement.RectInfo.showCanvas[PageIndex];
                            if (!showCanvas) {
                                // 不存在选择的逻辑
                                if (rootElement.RectInfo.reverse) {
                                    // 反向选择
                                    subIDsArray.push(SectionPrintBoundInfo.ID);
                                }
                                continue;
                            }
                            /** 所有当前页面的选择区域索引数组 */
                            var CoordinateArray = rootElement.RectInfo.coordinate[PageIndex];
                            if (rootElement.RectInfo.reverse) {
                                // 反向选择
                                // 当前病程全部的面积
                                const totalArea = SectionPrintBoundInfo.Width * SectionPrintBoundInfo.Height;
                                // 选择区域的存储数组
                                var SelectionArray = [];
                                function handleOverlap(area1) {
                                    // 判断是否有重叠
                                    var isPush = true;
                                    for (var j = 0; j < SelectionArray.length; j++) {
                                        var area2 = SelectionArray[j];
                                        // 判断是否包含在内部
                                        if (area1.left >= area2.left && area1.top >= area2.top && area1.left + area1.width <= area2.left + area2.width && area1.top + area1.height <= area2.top + area2.height) {
                                            // 包含在内部
                                            isPush = false;
                                            break;
                                        }
                                        const overlapX = Math.max(0, Math.min(area1.left + area1.width, area2.left + area2.width) - Math.max(area1.left, area2.left));
                                        const overlapY = Math.max(0, Math.min(area1.top + area1.height, area2.top + area2.height) - Math.max(area1.top, area2.top));
                                        // 如果有重叠，则分割成四个子区域
                                        if (overlapX > 0 && overlapY > 0) {
                                            var subAreas;
                                            if (area1.left >= area2.left) {
                                                // 左重叠
                                                subAreas = [
                                                    { left: area1.left, top: area1.top, width: overlapX, height: overlapY },
                                                    { left: area1.left + overlapX, top: area1.top, width: area1.width - overlapX, height: overlapY },
                                                    { left: area1.left, top: area1.top + overlapY, width: overlapX, height: area1.height - overlapY },
                                                    { left: area1.left + overlapX, top: area1.top + overlapY, width: area1.width - overlapX, height: area1.height - overlapY }
                                                ];
                                            } else {
                                                // 右重叠
                                                subAreas = [
                                                    { left: area1.left, top: area1.top, width: area1.width - overlapX, height: overlapY },
                                                    { left: area1.left + area1.width - overlapX, top: area1.top, width: overlapX, height: overlapY },
                                                    { left: area1.left, top: area1.top + overlapY, width: area1.width - overlapX, height: area1.height - overlapY },
                                                    { left: area1.left + area1.width - overlapX, top: area1.top + overlapY, width: overlapX, height: area1.height - overlapY }
                                                ];
                                            }
                                            subAreas = subAreas.filter(subArea => subArea.width > 0 && subArea.height > 0);
                                            for (var k = 0; k < subAreas.length; k++) {
                                                handleOverlap(subAreas[k]);
                                            }
                                            isPush = false;
                                            break;
                                        }
                                    }
                                    if (isPush && area1.width > 0 && area1.height > 0) {
                                        SelectionArray.push(area1);
                                    }
                                }
                                for (var j = 0; j < CoordinateArray.length; j++) {
                                    var CoordinateX = CoordinateArray[j].left * setZoomRate;
                                    var CoordinateY = CoordinateArray[j].top * setZoomRate;
                                    var CoordinateWidth = CoordinateArray[j].width * setZoomRate;
                                    var CoordinateHeight = CoordinateArray[j].height * setZoomRate;
                                    // 判断左右有没有选择到文档部分
                                    if (CoordinateX + CoordinateWidth <= SectionPrintBoundInfo.Left || CoordinateX >= SectionPrintBoundInfo.Left + SectionPrintBoundInfo.Width) {
                                        continue;
                                    }
                                    // 判断上下有没有选择到文档部分
                                    if (CoordinateY + CoordinateHeight <= SectionPrintBoundInfo.Top || CoordinateY >= SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height) {
                                        continue;
                                    }
                                    if (CoordinateX < SectionPrintBoundInfo.Left) {
                                        // 左边框超出病程
                                        CoordinateWidth -= SectionPrintBoundInfo.Left - CoordinateX;
                                        CoordinateX = SectionPrintBoundInfo.Left;
                                    }
                                    if (CoordinateX + CoordinateWidth > SectionPrintBoundInfo.Left + SectionPrintBoundInfo.Width) {
                                        // 右边框超出病程
                                        var sxwidth = CoordinateX + CoordinateWidth - (SectionPrintBoundInfo.Left + SectionPrintBoundInfo.Width);
                                        CoordinateWidth -= sxwidth;
                                    }
                                    if (CoordinateY < SectionPrintBoundInfo.Top) {
                                        // 上边框超出病程
                                        CoordinateHeight -= SectionPrintBoundInfo.Top - CoordinateY;
                                        CoordinateY = SectionPrintBoundInfo.Top;
                                    }
                                    if (CoordinateY + CoordinateHeight > SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height) {
                                        // 下边框超出病程
                                        var syheight = CoordinateY + CoordinateHeight - (SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height);
                                        CoordinateHeight -= syheight;
                                    }
                                    var area = { left: CoordinateX, top: CoordinateY, width: CoordinateWidth, height: CoordinateHeight };
                                    handleOverlap(area);
                                }
                                // 获取面积
                                var totalSelectionArea = 0;
                                for (let k = 0; k < SelectionArray.length; k++) {
                                    var selectionArea = SelectionArray[k].width * SelectionArray[k].height;
                                    totalSelectionArea += selectionArea;
                                }
                                if (totalArea - totalSelectionArea > 0) {
                                    // 存在可以打印的内容
                                    subIDsArray.push(SectionPrintBoundInfo.ID);
                                }
                            } else {
                                // 正常选择
                                for (var j = 0; j < CoordinateArray.length; j++) {
                                    var CoordinateX = CoordinateArray[j].left * setZoomRate;
                                    var CoordinateY = CoordinateArray[j].top * setZoomRate;
                                    var CoordinateWidth = CoordinateArray[j].width * setZoomRate;
                                    var CoordinateHeight = CoordinateArray[j].height * setZoomRate;
                                    // 判断左右有没有选择到文档部分
                                    if (CoordinateX + CoordinateWidth <= SectionPrintBoundInfo.Left || CoordinateX >= SectionPrintBoundInfo.Left + SectionPrintBoundInfo.Width) {
                                        continue;
                                    }
                                    // 判断上下有没有选择到文档部分
                                    if (CoordinateY + CoordinateHeight <= SectionPrintBoundInfo.Top || CoordinateY >= SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height) {
                                        continue;
                                    }
                                    subIDsArray.push(SectionPrintBoundInfo.ID);
                                }
                            }
                        }
                        return subIDsArray;
                    },
                    /**
                     * 鼠标按下事件
                     * @param {Event} e 
                     */
                    HandleMouseForBoundsSelectionViewMode: function (ev) {
                        if (ev == null) {
                            ev = window.event;
                        }
                        if (ev == null) {
                            return false;
                        }
                        /** 按下shift键，不清除之前的选择 */
                        var PressTheShift = ev.shiftKey;
                        var RectInfo = rootElement.RectInfo;
                        if (!RectInfo || !RectInfo.pageContainer || !RectInfo.moveData) {
                            return;
                        }
                        /** 编辑器的存放页面的容器 */
                        var pageContainer = RectInfo.pageContainer;
                        // 获取页面容器的垂直滚动位置
                        var scrollTop = pageContainer.scrollTop;
                        // 获取页面容器的水平滚动位置
                        var scrollLeft = pageContainer.scrollLeft;
                        switch (ev.type) {
                            case "mousedown":
                                RectInfo.isMouseDown = true;
                                try {
                                    // 获取元素对于页面左上角的位置
                                    var rect = pageContainer.getBoundingClientRect();
                                    RectInfo.moveData.startX = ev.clientX - rect.left + scrollLeft;
                                    RectInfo.moveData.startY = ev.clientY - rect.top + scrollTop;
                                    // 选择框
                                    var div = pageContainer.querySelector("#" + RectInfo.moveData.wId);
                                    if (!div) {
                                        var div = document.createElement("div");
                                        div.id = RectInfo.moveData.wId;
                                        pageContainer.appendChild(div);
                                    }
                                    div.className = "div";
                                    // 设置样式
                                    div.style.cssText = "position:absolute;border:1px dashed blue;width:0px;height:0px;left:0px;top:0px;overflow:hidden;";
                                    // 设置选择框的位置
                                    div.style.marginLeft = RectInfo.moveData.startX + "px";
                                    div.style.marginTop = RectInfo.moveData.startY + "px";
                                } catch (e) {
                                    // alert(e);
                                    RectInfo.isMouseDown = false;
                                }
                                break;
                            case "mousemove":
                                if (RectInfo.isMouseDown == false) {
                                    return;
                                }
                                try {
                                    // 获取元素对于页面左上角的位置
                                    var rect = pageContainer.getBoundingClientRect();
                                    // 初始化拖动对象的起始坐标
                                    var startX = RectInfo.moveData.startX;
                                    var startY = RectInfo.moveData.startY;
                                    // 计算鼠标当前在元素内的水平和垂直位置
                                    var clientX = ev.clientX - rect.left;
                                    var clientY = ev.clientY - rect.top;
                                    // 根据起始坐标和当前鼠标位置，计算元素应该被拖动到的左上角位置
                                    // 保证元素不会出现在负坐标区域
                                    RectInfo.moveData.retcLeft = (startX - clientX - scrollLeft > 0 ? clientX + scrollLeft : startX) + "px";
                                    // 根据起始坐标和当前鼠标位置，计算元素应该被拖动到的顶部位置
                                    // 保证元素不会出现在负坐标区域
                                    RectInfo.moveData.retcTop = (startY - clientY - scrollTop > 0 ? clientY + scrollTop : startY) + "px";
                                    // 计算元素的高度，确保高度为正数
                                    RectInfo.moveData.retcHeight = Math.abs(startY - clientY - scrollTop) + "px";
                                    // 计算元素的宽度，确保宽度为正数
                                    RectInfo.moveData.retcWidth = Math.abs(startX - clientX - scrollLeft) + "px";
                                    // 选择框元素
                                    var div = pageContainer.querySelector("#" + RectInfo.moveData.wId);
                                    // 设置选择框的位置和宽度高度
                                    div.style.marginLeft = RectInfo.moveData.retcLeft;
                                    div.style.marginTop = RectInfo.moveData.retcTop;
                                    div.style.width = RectInfo.moveData.retcWidth;
                                    div.style.height = RectInfo.moveData.retcHeight;
                                } catch (e) {
                                    // alert(e);
                                    RectInfo.isMouseDown = false;
                                }
                                break;
                            case "mouseup":
                                RectInfo.isMouseDown = false;
                                // 选择框元素
                                var div = pageContainer.querySelector("#" + RectInfo.moveData.wId);
                                if (!div) {
                                    return;
                                }
                                // 使用严格等于进行判断
                                if (!PressTheShift) {
                                    // 未按下shift键，清除之前的选择
                                    RectInfo.showCanvas = [];
                                    RectInfo.coordinate = [];
                                }

                                // 获取选择框的坐标
                                var initial_rect = div.getBoundingClientRect();

                                // 循环所有的蒙版
                                for (var i = 0; i < RectInfo.allCanvas.length; i++) {
                                    // 因为需要跨页选择，所以选择框的值需要刷新
                                    var retcTop = initial_rect.top;
                                    var retcLeft = initial_rect.left;
                                    var retcWidth = initial_rect.width;
                                    var retcHeight = initial_rect.height;
                                    // 蒙版canvas
                                    var maskPageCanvas = RectInfo.allCanvas[i];
                                    // 获取蒙版canvas的坐标
                                    var rect = maskPageCanvas.getBoundingClientRect();
                                    var maskPageCanvasTop = rect.top;
                                    var maskPageCanvasLeft = rect.left;
                                    var maskPageCanvasWidth = rect.width;
                                    var maskPageCanvasHeight = rect.height;

                                    // 判断选择框和canvas是否有交集: 左 || 右 || 上 || 下
                                    if (
                                        maskPageCanvasLeft >= retcLeft + retcWidth ||
                                        maskPageCanvasLeft + maskPageCanvasWidth <= retcLeft ||
                                        maskPageCanvasTop >= retcTop + retcHeight ||
                                        maskPageCanvasTop + maskPageCanvasHeight <= retcTop
                                    ) {
                                        continue;
                                    }

                                    // 更新选择框尺寸以适应canvas边界
                                    // 左边框超出
                                    if (retcLeft < maskPageCanvasLeft) {
                                        retcWidth -= maskPageCanvasLeft - retcLeft;
                                        retcLeft = maskPageCanvasLeft;
                                    }
                                    // 右边框超出
                                    if (retcLeft + retcWidth > maskPageCanvasLeft + maskPageCanvasWidth) {
                                        retcWidth -= retcLeft + retcWidth - maskPageCanvasLeft - maskPageCanvasWidth;
                                    }
                                    // 上边框超出
                                    if (retcTop < maskPageCanvasTop) {
                                        retcHeight -= maskPageCanvasTop - retcTop;
                                        retcTop = maskPageCanvasTop;
                                    }
                                    // 下边框超出
                                    if (retcTop + retcHeight > maskPageCanvasTop + maskPageCanvasHeight) {
                                        retcHeight -= retcTop + retcHeight - maskPageCanvasTop - maskPageCanvasHeight;
                                    }

                                    // 如果当前页面在范围中，则添加到显示的数组中
                                    RectInfo.showCanvas[i] = maskPageCanvas;

                                    // 初始化coordinate数组
                                    if (!Array.isArray(RectInfo.coordinate[i])) {
                                        RectInfo.coordinate[i] = [];
                                    }

                                    // 存储当前选中区域
                                    RectInfo.coordinate[i].push({
                                        left: retcLeft - maskPageCanvasLeft,// 选择区域距离正文左上角的左边的距离
                                        top: retcTop - maskPageCanvasTop,// 选择区域距离正文左上角的顶端的距离
                                        width: retcWidth,// 选择区域的宽度
                                        height: retcHeight,// 选择区域的高度
                                    });
                                }
                                RectInfo.refreshCanvas();// 刷新画布的选择区域
                                // 删除选择框
                                div.remove();
                                break;
                        }
                        // 取消事件冒泡
                        if (ev && ev.stopPropagation) {
                            // 因此它支持W3C的stopPropagation()方法 
                            ev.stopPropagation();
                        } else {
                            // 否则，我们需要使用IE的方式来取消事件冒泡 
                            window.event.cancelBubble = true;
                        }
                    }
                };
            }
            var allPagesNode;
            /** 获取需要处理的元素对象 */
            var pageContainer = rootElement.querySelector('[dctype="page-container"]');
            /** 是否是打印预览模式 */
            var isPrintPreview = rootElement.IsPrintPreview();
            // 找到所有的页面元素
            if (isPrintPreview) {
                pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                // 设置区域选择打印时打印预览内容无法选中
                pageContainer.style.userSelect = "none";
                // 存储当前是否为打印预览模式
                rootElement.RectInfo.printPreviewOpen = true;
                // 打印预览模式下获取svg元素
                allPagesNode = pageContainer.querySelectorAll('svg[dctype="page"]');
            } else {
                // 编辑模式下获取canvas元素
                allPagesNode = pageContainer.querySelectorAll('canvas[dctype="page"]');
            }
            rootElement.RectInfo.pageContainer = pageContainer;
            // 设置缩放比例
            rootElement.RectInfo.scale = rootElement.GetZoomRate();
            // if (allPagesNode[0]) {
            //     var nativeWidth = parseFloat(allPagesNode[0].getAttribute('native-width'));
            //     rootElement.RectInfo.scale = parseFloat((allPagesNode[0].width / nativeWidth).toFixed(2));
            // }
            // 循环添加蒙版
            for (var i = 0; i < allPagesNode.length; i++) {
                var thisPage = allPagesNode[i];
                // 创建蒙版元素
                var maskCanvas = rootElement.ownerDocument.createElement("canvas");
                // 使用 Array.forEach 来遍历属性，避免使用相同的索引变量 i
                Array.from(thisPage.attributes).forEach((attr) => {
                    maskCanvas.setAttribute(attr.name, attr.value);
                });
                // 设置蒙版属性
                maskCanvas.setAttribute("dctype", "maskPage");
                maskCanvas.setAttribute("pageIndex", i);
                maskCanvas.setAttribute("draggable", "false");
                // 设置蒙版样式
                maskCanvas.style.position = "absolute";
                // 修复存在图片水印，开启区域选择打印时页面展示不全的问题【DUWRITER5_0-3163】
                maskCanvas.style.background = "transparent";
                // 确保padding和border不被包含在定义的width和height之内
                maskCanvas.style.boxSizing = "content-box";
                // 插入蒙版
                pageContainer.insertBefore(maskCanvas, thisPage);
                // 清除画布
                rootElement.RectInfo.clearRect(maskCanvas);
                // 填充蒙版数组
                rootElement.RectInfo.allCanvas.push(maskCanvas);
            }
            // 调整区域选择蒙版样式
            rootElement.RectInfo.AdjustBoundsSelectionStyle();
            // 绑定事件
            if (typeof (pageContainer.addEventListener) === "function" && typeof (rootElement.RectInfo.HandleMouseForBoundsSelectionViewMode) === "function") {
                // 安全地绑定事件监听器
                ["mousedown", "mousemove", "mouseup"].forEach(function (event) {
                    pageContainer.addEventListener(event, rootElement.RectInfo.HandleMouseForBoundsSelectionViewMode);
                });
            }
        } else {
            // 清掉所有的canvas
            if (rootElement.RectInfo && rootElement.RectInfo.allCanvas) {
                for (var y = rootElement.RectInfo.allCanvas.length - 1; y >= 0; y--) {
                    var maskCanvas = rootElement.RectInfo.allCanvas[y];
                    if (maskCanvas) {
                        maskCanvas.remove();
                    }
                }
                delete rootElement.RectInfo;
            }
            /** 是否是打印预览模式 */
            var isPrintPreview = rootElement.IsPrintPreview();
            //找到所有的页面元素
            if (isPrintPreview) {
                var pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                if (pageContainer) {
                    // 取消区域选择打印时还原打印预览内容可以选中
                    pageContainer.style.userSelect = "";
                }
            }
        }
    },

    /**
     * 获取打印的所有病程的id，生成数组，正常打印，续打打印，区域选择打印都支持
     * @param {string | HTMLElement} containerID 容器元素编号或者编辑器元素
     * @returns 
     */
    GetSectionPrintBoundsInfo: function (containerID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return;
        }
        let subIDsArray = new Array();
        let SectionPrintBoundsInfo = rootElement.__DCWriterReference.invokeMethod("GetSectionPrintBoundsInfo");
        if (!SectionPrintBoundsInfo || SectionPrintBoundsInfo.length == 0) {
            return subIDsArray;
        }
        // 开始判断续打
        let JumpPrintData;
        let isIsPrintPreview = rootElement.IsPrintPreview();
        if (isIsPrintPreview == true) {
            JumpPrintData = rootElement.GetJumpPrintInfoForPrintPreview();
        } else {
            JumpPrintData = rootElement.GetJumpPrintInfo();
        }
        function toPixel(num) {
            // return parseFloat((parseFloat(num) / 300 * 96.00001209449).toFixed(2));
            return parseInt((parseFloat(num) / 300 * 96.00001209449));
        }
        // 续打模式
        if (JumpPrintData && JumpPrintData.Mode == "Normal" && (JumpPrintData.PageIndex >= 0 || JumpPrintData.EndPageIndex >= 0)) {
            // 判断续打是否存在需要打印的内容
            if (JumpPrintData.PageIndex < JumpPrintData.EndPageIndex) {
                // 续打模式下，如果续打开始页码小于续打结束页码，则返回空数组
                return subIDsArray;
            } else if (JumpPrintData.PageIndex == JumpPrintData.EndPageIndex) {
                // 续打开始页码等于续打结束页码
                if (JumpPrintData.Position <= JumpPrintData.EndPosition) {
                    // 续打开始位置小于等于续打结束位置,说明没有需要打印的内容
                    return subIDsArray;
                }
            }
            var DocumentBodyProps = rootElement.GetElementProperties(rootElement.DocumentBody);
            /** 正文距离该页面的顶部的距离 */
            var DocumentBodyTop = 0;
            if (DocumentBodyProps) {
                DocumentBodyTop = DocumentBodyProps.TopInOwnerPage;
            }
            /** 续打开始位置 */
            let StartPosition = toPixel(DocumentBodyTop + JumpPrintData.Position);
            /** 续打结束位置 */
            let EndPosition = toPixel(DocumentBodyTop + JumpPrintData.EndPosition);
            for (var i = 0; i < SectionPrintBoundsInfo.length; i++) {
                var SectionPrintBoundInfo = SectionPrintBoundsInfo[i];
                if (subIDsArray.indexOf(SectionPrintBoundInfo.ID) > -1) {
                    // 跳过已经添加的病程
                    continue;
                }
                var Top = SectionPrintBoundInfo.Top;
                var Bottom = SectionPrintBoundInfo.Top + SectionPrintBoundInfo.Height;
                // 首先判断续打开始位置
                if (JumpPrintData.PageIndex >= 0) {
                    if (SectionPrintBoundInfo.PageIndex < JumpPrintData.PageIndex) {
                        // 当前病程所在页面小于续打开始的页面，直接跳过
                        continue;
                    }
                    if (SectionPrintBoundInfo.PageIndex == JumpPrintData.PageIndex) {
                        // 在续打开始界面上
                        if (Bottom <= StartPosition) {
                            // 开始位置不在病程上
                            continue;
                        }
                    }
                }
                // 判断续打结束位置
                if (JumpPrintData.EndPageIndex >= 0) {
                    if (SectionPrintBoundInfo.PageIndex > JumpPrintData.EndPageIndex) {
                        // 当前病程所在页面大于续打结尾的页面，直接跳过
                        continue;
                    }
                    if (SectionPrintBoundInfo.PageIndex == JumpPrintData.EndPageIndex) {
                        // 在续打结束界面上
                        if (Top >= EndPosition) {
                            // 结束位置不在病程上
                            continue;
                        }
                    }
                }
                subIDsArray.push(SectionPrintBoundInfo.ID);
            }
            return subIDsArray;
        }
        // 区域选择打印模式
        if (rootElement.RectInfo && typeof (rootElement.RectInfo.SectionPrintBoundsInfoFunc) == "function") {
            return rootElement.RectInfo.SectionPrintBoundsInfoFunc(rootElement, SectionPrintBoundsInfo);
        }
        // 正常模式下，直接返回所有病程
        for (var i = 0; i < SectionPrintBoundsInfo.length; i++) {
            var SectionPrintBoundInfo = SectionPrintBoundsInfo[i];
            if (subIDsArray.indexOf(SectionPrintBoundInfo.ID) > -1) {
                // 跳过已经添加的病程
                continue;
            }
            subIDsArray.push(SectionPrintBoundInfo.ID);
        }
        return subIDsArray;
    },
    /**
     * 打印预览控件中设置续打模式
     * @param {string | HTMLElement} containerID 容器元素编号或者编辑器元素
     * @param {boolean} isJump - 指定是否开启续打模式，true表示开启，false表示关闭
     */
    SetJumpPrintModeInWriterPrintPreviewControlForWASM: function (containerID, isJump) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return false;
        }
        if (rootElement.IsWriterPrintPreviewControlForWASM == false) {
            // 不是打印预览控件，直接返回
            return false;
        }
        var pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
        if (pageContainer == null) {
            return false;
        }
        if (rootElement.JumpPrintInfo) {
            if (typeof (pageContainer.removeEventListener) === "function" && typeof (rootElement.JumpPrintInfo.HandleClickForJumpPrintMode) === "function") {
                // 安全地移除事件监听器
                pageContainer.removeEventListener("click", rootElement.JumpPrintInfo.HandleClickForJumpPrintMode);
            }
            // 清除上次的蒙版
            const JumpMarksNodes = pageContainer.querySelectorAll("rect[dctype='startjumpmark'], rect[dctype='endjumpmark'], rect[dctype='alljumpmark']");
            // 使用 forEach 进行迭代
            Array.from(JumpMarksNodes).forEach(node => {
                node.remove();
            });
        }
        if (isJump == true) {
            if (!rootElement.JumpPrintInfo) {
                rootElement.JumpPrintInfo = {
                    Info: {
                        /** 续打开始页码 */
                        PageIndex: 0,
                        /** 续打开始位置 */
                        Position: 0,
                        /** 续打结束页码 */
                        EndPageIndex: -1,
                        /** 续打结束位置 */
                        EndPosition: 0,
                    },
                    /**
                     * 创建一个SVG矩形元素
                     * 
                     * 此函数用于在SVG命名空间内创建一个矩形元素，并为其设置位置、大小、填充色、透明度以及一个自定义属性dctype
                     * 它提供了在网页上绘制简单SVG图形的能力，使得在JavaScript中处理图形更为灵活方便
                     * 
                     * @param {number} x - 矩形的起始x坐标，决定了矩形在SVG中的水平位置
                     * @param {number} y - 矩形的起始y坐标，决定了矩形在SVG中的垂直位置
                     * @param {number} width - 矩形的宽度，决定了矩形在水平方向上的大小
                     * @param {number} height - 矩形的高度，决定了矩形在垂直方向上的大小
                     * @param {string} fill - 矩形的填充颜色，可以是CSS支持的任何颜色值，如'#RRGGBB'或颜色名称
                     * @param {number} opacity - 矩形的填充透明度，范围为0到1，0为完全透明，1为完全不透明
                     * @param {string} dctype - 一个自定义属性，用于存储或标识矩形的类型或用途此属性可根据需要自定义
                     * @returns {SVGRectElement} 返回创建的SVG矩形元素，以便于进一步操作或添加到SVG文档中
                     */
                    createSVGRect: function (x, y, width, height, fill, opacity, dctype) {
                        // 创建SVG命名空间下的矩形元素
                        const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect");
                        // 设置矩形的位置、大小、填充等属性
                        rect.setAttribute("x", x);
                        rect.setAttribute("y", y);
                        rect.setAttribute("width", width);
                        rect.setAttribute("height", height);
                        rect.setAttribute("fill", fill);
                        rect.setAttribute("fill-opacity", opacity);
                        rect.setAttribute("dctype", dctype);
                        // 返回构建的SVG矩形元素
                        return rect;
                    },
                    /**
                     * 鼠标点击事件
                     * @param {Event} e 
                     */
                    HandleClickForJumpPrintMode: function (e) {
                        var srcElement = e.srcElement || e.target;
                        if (srcElement == null) {
                            return;
                        }
                        var rootElement = DCTools20221228.GetOwnerWriterControl(srcElement);
                        if (rootElement == null) {
                            return;
                        }
                        var pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                        if (pageContainer == null) {
                            return false;
                        }
                        // 确保点击的是svg元素
                        var NowClickSvgNode = srcElement;
                        while (NowClickSvgNode && NowClickSvgNode.nodeName != "svg" && NowClickSvgNode != this) {
                            NowClickSvgNode = NowClickSvgNode.parentNode;
                        }
                        if (NowClickSvgNode == null || NowClickSvgNode.nodeName != "svg") {
                            return;
                        }
                        /** 当前点击的svg页码值 */
                        var NowClickPageIndex = parseInt(NowClickSvgNode.getAttribute("PageIndex"));
                        var JumpPrintInfo = rootElement.JumpPrintInfo;
                        /** 编辑器的放大倍数 */
                        var zoomRate = rootElement.GetZoomRate();
                        zoomRate = parseFloat(zoomRate);
                        // 确保放大倍数是数字
                        if (isNaN(zoomRate)) {
                            zoomRate = 1;
                        }
                        var NowClickSvg_rect = NowClickSvgNode.getBoundingClientRect();
                        var clickPosition = (e.clientY - NowClickSvg_rect.top) / zoomRate;

                        if (e.ctrlKey == true) {
                            // 反向续打(ctrl + 点击)
                            JumpPrintInfo.Info.EndPageIndex = NowClickPageIndex;
                            JumpPrintInfo.Info.EndPosition = clickPosition;
                        } else {
                            // 正常续打(点击)
                            JumpPrintInfo.Info.PageIndex = NowClickPageIndex;
                            JumpPrintInfo.Info.Position = clickPosition;
                            JumpPrintInfo.Info.EndPageIndex = -1;
                            JumpPrintInfo.Info.EndPosition = 0;
                        }
                        JumpPrintInfo.AdjustJumpPrintMark(pageContainer, false);
                    },
                    /**
                     * 设置并更新续打信息
                     * @param {string | HTMLElement} containerID 容器元素编号或者编辑器元素
                     * @param {object} NewJumpPrintInfo - 包含新的续打信息的对象，应包含与打印续打相关的信息如页面索引和位置
                     * @returns {boolean} - 函数执行成功返回true，否则返回false
                     */
                    SetJumpPrintInfo: function (containerID, NewJumpPrintInfo) {
                        // 检查NewJumpPrintInfo是否为对象类型，否则返回false
                        if (typeof (NewJumpPrintInfo) != "object") {
                            return false;
                        }
                        // 获取打印预览容器的所有者写入控制元素
                        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);;
                        if (rootElement == null) {
                            return false;
                        }
                        // 在所有者写入控制元素中查找打印预览的页面容器
                        var pageContainer = rootElement.querySelector('[dctype="page-printpreview"]');
                        if (pageContainer == null) {
                            return false;
                        }
                        // 获取旧的跳转打印信息对象
                        var OldJumpPrintInfo = rootElement.JumpPrintInfo;
                        if (OldJumpPrintInfo == null || OldJumpPrintInfo.Info == null) {
                            return false;
                        }
                        // 确定是否需要调整续打蒙版
                        var NeedAdjustJumpPrintMark = false;
                        // 遍历并更新关键跳转打印信息字段
                        ["PageIndex", "Position", "EndPageIndex", "EndPosition"].forEach(function (key) {
                            if (Object.prototype.hasOwnProperty.call(OldJumpPrintInfo.Info, key) && Object.prototype.hasOwnProperty.call(NewJumpPrintInfo, key)) {
                                var val;
                                // 对于页面索引类型的字段，将值解析为整数
                                if (key == "PageIndex" || key == "EndPageIndex") {
                                    val = parseInt(NewJumpPrintInfo[key]);
                                } else {
                                    // 对于其他字段，将值解析为浮点数
                                    val = parseFloat(NewJumpPrintInfo[key]);
                                }
                                // 如果解析的值不是NaN，则更新旧的打印信息对象
                                if (isNaN(val) == false && OldJumpPrintInfo.Info[key] != val) {
                                    OldJumpPrintInfo.Info[key] = val;
                                    // 需要调整续打蒙版
                                    NeedAdjustJumpPrintMark = true;
                                }
                            }
                        });
                        if (NeedAdjustJumpPrintMark == true) {
                            // 需要调整续打蒙版
                            OldJumpPrintInfo.AdjustJumpPrintMark(pageContainer, false);
                        }
                        return true;
                    },
                    /**
                     * 调整续打蒙版
                     * 
                     * 此函数用于在页面容器中调整元素的续打蒙版
                     * 根据是否为打印模式，调整页面中需要打印的元素的续打蒙版
                     * 
                     * @param {HTMLElement} pageContainer - 页面的容器元素
                     * @param {boolean} isPrint - 是否为打印模式
                     */
                    AdjustJumpPrintMark: function (pageContainer, isPrint) {
                        if (pageContainer == null) {
                            return;
                        }
                        // var rootElement = DCTools20221228.GetOwnerWriterControl(pageContainer);
                        // if (rootElement == null) {
                        //     return;
                        // }
                        var Info = this.Info;
                        var StartPageIndex = Info.PageIndex,
                            StartPosition = Info.Position,
                            EndPageIndex = Info.EndPageIndex,
                            EndPosition = Info.EndPosition;
                        // 检查是否没有指定续打的起始或结束位置
                        if (PageIndex < 0 && Position < 0 && EndPageIndex < 0 && EndPosition < 0) {
                            // 没有续打，清除续打蒙版
                            const JumpMarksNodes = pageContainer.querySelectorAll("rect[dctype='startjumpmark'], rect[dctype='endjumpmark'], rect[dctype='alljumpmark']");
                            // 使用 forEach 进行迭代
                            Array.from(JumpMarksNodes).forEach(node => {
                                node.remove();
                            });
                            return;
                        }
                        // 获取页面容器中的所有svg节点
                        var svgNodes = pageContainer.querySelectorAll("svg");
                        // 遍历所有svg节点
                        for (var PageIndex = 0; PageIndex < svgNodes.length; PageIndex++) {
                            var svgNode = svgNodes[PageIndex];
                            // 移除所有标记
                            delete svgNode.JumpMarkNodeStr;
                            var JumpMarkNodeStr = "";
                            /** svg的宽度 */
                            var NativeWidth = parseFloat(svgNode.getAttribute("native-width")) || parseFloat(svgNode.getAttribute("width"));
                            /** svg的高度 */
                            var NativeHeight = parseFloat(svgNode.getAttribute("native-height")) || parseFloat(svgNode.getAttribute("height"));
                            // <rect x="0" y="0" width="793.9199" height="931.1999" fill="#0000FF" fill-opacity="0.39215686274509803"></rect>
                            // 获取续打蒙版
                            var StartJumpMarkNode = svgNode.querySelector("rect[dctype='startjumpmark']");
                            var EndJumpMarkNode = svgNode.querySelector("rect[dctype='endjumpmark']");
                            var AllJumpMarkNode = svgNode.querySelector("rect[dctype='alljumpmark']");
                            if ((StartPageIndex >= 0 && PageIndex < StartPageIndex) || (EndPageIndex >= 0 && PageIndex > EndPageIndex)) {
                                // 覆盖整个页面
                                if (StartJumpMarkNode) {
                                    StartJumpMarkNode.remove();
                                }
                                if (EndJumpMarkNode) {
                                    EndJumpMarkNode.remove();
                                }
                                if (isPrint == true) {
                                    // 隐藏不需要打印的页面
                                    svgNode.style.display = "none";
                                } else {
                                    if (AllJumpMarkNode == null) {
                                        AllJumpMarkNode = this.createSVGRect(0, 0, NativeWidth, NativeHeight, "#0000FF", "0.39215686274509803", "alljumpmark");
                                        svgNode.appendChild(AllJumpMarkNode);
                                    } else {
                                        AllJumpMarkNode.setAttribute("width", NativeWidth);
                                        AllJumpMarkNode.setAttribute("height", NativeHeight);
                                    }
                                    // 缓存,让svg在渲染的时候,不会被覆盖
                                    JumpMarkNodeStr += AllJumpMarkNode.outerHTML;
                                    if (JumpMarkNodeStr && svgNode._isRendered == false) {
                                        // svg没有渲染的时候,才添加
                                        svgNode.JumpMarkNodeStr = JumpMarkNodeStr;
                                    }
                                }
                                continue;
                            }
                            if (AllJumpMarkNode) {
                                AllJumpMarkNode.remove();
                            }
                            // 处理续打
                            if (StartPageIndex >= 0 && PageIndex == StartPageIndex) {
                                if (StartPosition > NativeHeight) {
                                    // 续打位置超过当前页高度，则将高度设置为当前页高度
                                    StartPosition = NativeHeight;
                                }
                                if (StartJumpMarkNode == null) {
                                    StartJumpMarkNode = this.createSVGRect(0, 0, NativeWidth, StartPosition, "#0000FF", "0.39215686274509803", "startjumpmark");
                                    svgNode.appendChild(StartJumpMarkNode);
                                } else {
                                    StartJumpMarkNode.setAttribute("height", StartPosition);
                                }
                                if (isPrint == true) {
                                    // 打印时，需要使用白色背景覆盖内容
                                    StartJumpMarkNode.setAttribute("fill", "#FFFFFF");
                                    StartJumpMarkNode.setAttribute("fill-opacity", "1");
                                } else {
                                    // 缓存,让svg在渲染的时候,不会被覆盖
                                    JumpMarkNodeStr += StartJumpMarkNode.outerHTML;
                                }
                            } else {
                                if (StartJumpMarkNode) {
                                    StartJumpMarkNode.remove();
                                }
                            }
                            // 处理反向续打
                            if (EndPageIndex >= 0 && PageIndex == EndPageIndex) {
                                if (EndPosition > NativeHeight) {
                                    // 续打位置超过当前页高度，则将高度设置为当前页高度
                                    EndPosition = NativeHeight;
                                }
                                var EndJumpMarkNodeHeight = NativeHeight - EndPosition;
                                if (EndJumpMarkNode == null) {
                                    EndJumpMarkNode = this.createSVGRect(0, EndPosition, NativeWidth, EndJumpMarkNodeHeight, "#0000FF", "0.39215686274509803", "endjumpmark");
                                    svgNode.appendChild(EndJumpMarkNode);
                                } else {
                                    EndJumpMarkNode.setAttribute("y", EndPosition);
                                    EndJumpMarkNode.setAttribute("height", EndJumpMarkNodeHeight);
                                }
                                if (isPrint == true) {
                                    // 打印时，需要使用白色背景覆盖内容
                                    EndJumpMarkNode.setAttribute("fill", "#FFFFFF");
                                    EndJumpMarkNode.setAttribute("fill-opacity", "1");
                                } else {
                                    // 缓存,让svg在渲染的时候,不会被覆盖
                                    JumpMarkNodeStr += EndJumpMarkNode.outerHTML;
                                }
                            } else {
                                if (EndJumpMarkNode) {
                                    EndJumpMarkNode.remove();
                                }
                            }
                            if (JumpMarkNodeStr && svgNode._isRendered == false) {
                                // svg没有渲染的时候,才添加
                                svgNode.JumpMarkNodeStr = JumpMarkNodeStr;
                            }
                        }
                    }
                };
            }
            // 初始化数据
            // rootElement.JumpPrintInfo.rootElement = rootElement;
            rootElement.JumpPrintInfo.Info = {
                /** 续打开始页码 */
                PageIndex: 0,
                /** 续打开始位置 */
                Position: 0,
                /** 续打结束页码 */
                EndPageIndex: -1,
                /** 续打结束位置 */
                EndPosition: 0,
            };
            // 绑定事件
            if (typeof (pageContainer.addEventListener) === "function" && typeof (rootElement.JumpPrintInfo.HandleClickForJumpPrintMode) === "function") {
                // 安全地绑定事件监听器
                pageContainer.addEventListener("click", rootElement.JumpPrintInfo.HandleClickForJumpPrintMode);
            }
        } else {
            delete rootElement.JumpPrintInfo;
        }
        return true;
    },
    /**
     * 获取混合合并的SVG格式的预览HTML
     * 
     * @param {object} options - 合并预览HTML的参数，规格同前端GetPrintPreviewHTML2的参数
     * @param {HTMLElement} ctl - 前端编辑器对象
     * @param {function} callback - 回调函数
     */
    GetPrintPreviewSVGHTML: function (options, ctl, callback) {

        WriterControl_Rule.SuppressPaintRule = true;

        var result = ctl.__DCWriterReference.invokeMethod("InnerGetPrintPreviewHtmlData3", options);
        if (result === false) {
            return null;
        }

        var pageContainer = ctl.ownerDocument.createElement("DIV");
        //pageContainer.setAttribute("dctype", "page-printpreview");
        //pageContainer.style.height = "100%";
        //pageContainer.style.overflow = "auto";
        //pageContainer.style.textAlign = "center";
        //pageContainer.style.position = "relative";
        //pageContainer.style.transform = "translate3d(0,0,0)";
        //pageContainer.style['-moz-transform'] = "translate3d(0,0,0)";
        //pageContainer.style['-webkit-transform'] = "translate3d(0,0,0)";
        //pageContainer.style.display = "";

        //var pageBorderColor = ctl.getAttribute("pagebordercolor");
        //var pagelayoutmode = ctl.getAttribute('pagelayoutmode');
        
        var isLandscape = false;
        for (var i = 0; i < result.SVGS.length; i++) {
            var div = ctl.ownerDocument.createElement("DIV");
            div.style.pageBreakAfter = "always";
            if (i !== result.SVGS.length - 1) {
                div.style.pageBreakInside = "avoid";
            }
            var element = ctl.ownerDocument.createElementNS("http://www.w3.org/2000/svg", "svg");
            isLandscape = result.Width > result.Height;
            element.style.width = result.Width + "px";
            element.style.height = result.Height + "px";
            element.style.overflow = "hidden";
            element.setAttribute("width", result.Width + "px");
            element.setAttribute("height", result.Height + "px");
            //element.setAttribute("native-width", result.Width);
            //element.setAttribute("native-height", result.Height);
            //element.style.border = `1px solid ${pageBorderColor ? pageBorderColor : "black"}`;
            //element.style.backgroundColor = "white";
            //element.style.verticalAlign = "top";
            //element.style.boxSizing = "content-box";
            ////判断编辑器是否为单栏展示,如果是则设置为块级元素并且设置为居中显示
            //if (typeof pagelayoutmode == 'string' && pagelayoutmode.trim().toLowerCase() == 'singlecolumn') {
            //    element.style.margin = "5px auto";
            //    element.style.display = 'block';
            //} else {
            //    element.style.margin = "5px";
            //    element.style.display = 'inline-block';
            //}
            //WriterControl_Paint.SetPageElementSize(ctl, element);
            element.innerHTML = result.SVGS[i].toString();
            div.appendChild(element);
            pageContainer.appendChild(div);
        }
        console.log(result);
        //ctl.__DCWriterReference.invokeMethod("RefreshViewAfterPrint", true);
        WriterControl_Rule.SuppressPaintRule = false;

        var ps = "@page{margin-left:0cm;margin-top:0cm;margin-right:0cm;margin-bottom:0cm;";
        if (isLandscape) {
            ps += "size: landscape;";
        }
        ps += "}";
        var htmlheader = "<html><head><style>" + ps + " </style></head>";
        var str = htmlheader + "<body style='margin: 0;'>" + pageContainer.innerHTML + "</body></html>";
        return str;

        
    },
    /**
    * 遍历待预览打印的文档，当关键节点出现的时候，触发回调
    * @param {*} doc
    * @returns
    */
    walkThroughPreviewDoc: function (doc, onPage, onCell) {
        if (!doc || doc.nodeName != "#document") {
            return;
        }
        var pageHtml = doc.querySelectorAll("body > [pageindex]");
        if (!pageHtml || pageHtml.length == 0) {
            pageHtml = doc.querySelectorAll("body > center#dcRootCenter > [pageindex]");
        }
        if (pageHtml && pageHtml.length > 0) {
            for (var i = 0; i < pageHtml.length; i++) {
                var pageDOM = pageHtml[i];
                var jqPageDOM = $(pageDOM);
                var pageId = jqPageDOM.attr("pageindex");
                onPage && onPage(pageId, pageDOM)

                // 遍历html节点，生成本页内会被打印的节点
                var tableDOM = pageDOM.querySelectorAll('[dctype="XTextTableElement"]');
                if (tableDOM.length > 1) {
                    // 处理表格表头重复出现问题
                    tableDOM = tableDOM[tableDOM.length - 1];
                } else {
                    tableDOM = tableDOM[0];
                }
                if (tableDOM) {
                    var trs = tableDOM.querySelectorAll("tr");
                    // 开始遍历所有表格行
                    for (var m = 0; m < trs.length; m++) {
                        var trDOM = trs[m];
                        var tds = trDOM.querySelectorAll("td");
                        // 开始遍历一行内的所有单元格
                        for (var j = 0; j < tds.length; j++) {
                            var tdDOM = tds[j];
                            onCell && onCell(pageId, m, j, tdDOM)
                        }
                    }
                } else {
                    console.error("解析 page:" + pageId + "的tableDOM失败!");
                }
            }
        }
    },

    /**
     * 比较新旧打印状态，给出本次需要新增的打印状态部分
     * @param {*} oldStatus 老状态
     * @param {*} newStatus 新状态
     * @returns 本次需要新增的打印状态部分
     */
    comparePrintStatus: function (oldStatus, newStatus) {
        if (!oldStatus || !newStatus) {
            // console.error('新旧状态都不能为空');
            return undefined;
        } else {
            // 增量打印状态，只需要关注每个页面
            var printCell = [];
            // 先解除当前新老状态与原来对象的引用，防止状态污染
            var oldPages = JSON.parse(JSON.stringify(oldStatus));
            var newPages = JSON.parse(JSON.stringify(newStatus));
            for (var i = 0; i < newPages.length; i++) {
                // 处理每一页
                var newPage = newPages[i];
                var oldPage = oldPages[i];
                if (oldPage) {
                    var newPageCells = newPage.printCells;
                    var oldPageCells = oldPage.printCells;
                    var newCells = [];
                    var oldPageCellsText = [];//存储老页面的数组字符串，用来对比
                    for (var m = 0; m < oldPageCells.length; m++) {
                        oldPageCellsText.push(JSON.stringify(oldPageCells[m]));
                    }
                    // 根据新页面的打印状态，去比对老状态，得出增量cell
                    for (var n = 0; n < newPageCells.length; n++) {
                        var stringText = JSON.stringify(newPageCells[n]);
                        if ($.inArray(stringText, oldPageCellsText) == -1) {
                            newCells.push(JSON.parse(stringText));
                        }
                    }
                    // 如果当前页有新增打印的cell，则构建当前页打印状态任务对象
                    if (newCells.length > 0) {
                        // 因为老页面打印过，所以本次新页面无需打印网格和页眉页脚
                        newPage.printed = true;
                        newPage.printCells = newCells;
                        printCell.push(newPage);
                    }
                } else {
                    // 当前页面为新增加的，直接打印
                    newPage.printed = false;
                    printCell.push(newPage);
                }
            }
            return printCell;
        }
    },
    /**
    * 打印单元格
    * @param {*} doc 打印的document
    * @param {*} hiddenCellData 需要隐藏的单元格数据 []
    * @param {boolean} EnableHide 是否启动隐藏
    * @returns {*} CellData 打印过的单元格数据 []
    */
    TemplatePrintingWithCells: function (doc, hiddenCellData, EnableHide) {
        if (!doc || doc.nodeName != "#document") {
            return false;
        }
        if (typeof (EnableHide) != "boolean") {
            EnableHide = true;
        }
        if (EnableHide == false) {
            var styleDom = doc.querySelector("style#TemplatePrintingWithCellsStyle");
            if (styleDom) {
                styleDom.innerHTML = "";
            }
            return false;
        }
        // 存储当前打印状态
        var CellData = [];
        WriterControl_Print.walkThroughPreviewDoc(doc, function (pageId, pageDOM) {
            // 发现新page的回调
            var page = { pageId: pageId, printCells: [], printed: false };
            CellData.push(page);
        }, function (pageId, cellRow, cellCol, tdDOM) {
            var page = CellData.filter(function (p) {
                return p.pageId == pageId;
            })[0];
            var txt = tdDOM.innerText;
            var ensp_reg = new RegExp(String.fromCharCode(8194), "g");
            txt = txt.replace(ensp_reg, "");
            if (txt && txt.length > 0) {
                page && page.printCells.push([cellRow, cellCol]);
            }
        });
        var printCell = WriterControl_Print.comparePrintStatus(hiddenCellData, CellData);
        if (printCell) {
            // console.log("cellprint -> printCell", printCell)
            var printedPage = {};//需要处理的页 {0:true}
            // 处理dom结构，用来打印
            WriterControl_Print.walkThroughPreviewDoc(doc, function (pageId, pageDOM) {
                // 清除之前的样式
                pageDOM.classList.remove("hiddenPage");
                pageDOM.classList.remove("printedPage");
                // 获取到当前页的打印数据
                var page = printCell.filter(function (p) {
                    return p.pageId == pageId;
                })[0];
                // 数据中没有当前页
                if (!page) {
                    pageDOM.classList.add("hiddenPage");
                } else {
                    if (page.printed == true) {
                        // 之前已打印，需要处理
                        printedPage[pageId] = true;
                        pageDOM.classList.add("printedPage");
                    }
                }
            }, function (pageId, cellRow, cellCol, tdDOM) {
                // 清除之前的样式
                tdDOM.classList.remove("printCell");
                tdDOM.classList.remove("hiddenCell");
                // 处理已打印页
                if (printedPage[pageId]) {
                    var page = printCell.filter(function (p) {
                        return p.pageId == pageId;
                    })[0];
                    var pageCells = page.printCells;
                    if (pageCells.length > 0) {
                        var pageCellsText = [];//存储页面的数组字符串，用来对比
                        for (var m = 0; m < pageCells.length; m++) {
                            pageCellsText.push(JSON.stringify(pageCells[m]));
                        }
                        var stringText = JSON.stringify([cellRow, cellCol]);
                        if ($.inArray(stringText, pageCellsText) > -1) {
                            //需要打印
                            tdDOM.classList.add("printCell");
                        } else {
                            //不需要打印
                            tdDOM.classList.add("hiddenCell");
                        }
                    }
                }
            });
            /**
             * hiddenPage 直接隐藏的页
             * printedPage 已经打印的页面，需要处理页眉页脚等
             * printCell 需要打印的单元格
             * hiddenCell 需要隐藏的单元格
             */
            var styleDom = doc.querySelector("style#TemplatePrintingWithCellsStyle");
            if (!styleDom) {
                var styleDom = doc.createElement("style");
                styleDom.id = "TemplatePrintingWithCellsStyle";
                doc.head.appendChild(styleDom);
            }
            var styleStr = "";
            //  styleStr += "td[part='rightmargin']{display: table-cell!important;}";
            styleStr += ".hiddenPage{display: none!important;}";//处理隐藏的页
            styleStr += ".printedPage [dcpart='pagecontent'] > * {visibility: hidden!important;} .printedPage #divAllContainer{visibility: visible!important;}";//处理页眉页脚
            styleStr += ".printedPage table,.printedPage tr,.printedPage td{border-color: transparent!important;}";//处理边框
            styleStr += ".printedPage td{visibility: hidden!important;}.printedPage td.printCell{visibility: visible!important;}";//处理单元格
            styleDom.innerHTML = styleStr;
        }
        return CellData;
    }
};