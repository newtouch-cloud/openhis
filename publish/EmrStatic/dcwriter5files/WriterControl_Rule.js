"use strict";

import { DCTools20221228 } from "./DCTools20221228.js";
import { PageContentDrawer } from "./PageContentDrawer.js";
import { WriterControl_Paint } from "./WriterControl_Paint.js";
import { WriterControl_Task } from "./WriterControl_Task.js";
import { WriterControl_UI } from "./WriterControl_UI.js";

/**处理文档标尺相关的模块 */
export let WriterControl_Rule = {
    /**
     * 执行视图滚动事件处理
     * @param {HTMLDivElement} pageContainer 页面容器对象
     */
    HandleViewScroll: function (pageContainer) {
        if (pageContainer && pageContainer.parentNode) {
            WriterControl_Rule.InvalidateView(pageContainer.parentNode, "hrule");
            WriterControl_Rule.InvalidateView(pageContainer.parentNode, "vrule");
        }
    },
    /**
     * 更新标尺可见性
     * @param {string | HTMLDivElement} containerID 容器元素对象
     * @returns {Array} 两个标尺对象数组，如果不显示标尺，则返回空引用。
     */
    UpdateRuleVisible: function (containerID) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null) {
            return null;
        }
        var result = new Array();
        var bolRuleVisible = rootElement.__DCWriterReference.invokeMethod("get_RuleVisible");
        if (bolRuleVisible != (DCTools20221228.GetChildNodeByDCType(rootElement, "hrule") != null)) {
            // 标尺状态不一致，需要进行更新
            var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
            if (bolRuleVisible == true) {
                // 创建标尺对象
                var hrule = rootElement.ownerDocument.createElement("CANVAS");
                hrule.setAttribute("dctype", "hrule");
                //在此处判断是否需要展示
                hrule.style.display = "block";
                if (Array.isArray(rootElement.ruleVisible) && rootElement.ruleVisible[0].toLowerCase().trim() == 'false') {
                    hrule.style.display = "none";
                }
                hrule.width = screen.width;
                hrule.height = 24;
                hrule.style.backgroundColor = rootElement.__DCWriterReference.invokeMethod("GetRuleBackColorString");
                rootElement.insertBefore(hrule, pageContainer);

                var vrule = rootElement.ownerDocument.createElement("CANVAS");
                vrule.setAttribute("dctype", "vrule");
                vrule.style.display = "block";
                if (Array.isArray(rootElement.ruleVisible) && rootElement.ruleVisible[1].toLowerCase().trim() == 'false') {
                    vrule.style.display = "none";
                }
                vrule.style.float = "left";
                vrule.width = 24;
                //vrule.height = screen.height;
                vrule.height = rootElement.offsetHeight - 24; //当存在多个上下叠加的编辑器时，会出现标尺过高导致下一个编辑器无法正常出现的问题
                vrule.style.backgroundColor = hrule.style.backgroundColor;
                rootElement.insertBefore(vrule, pageContainer);
                rootElement.style.overflow = "clip";

                pageContainer.style.height = "calc( 100% - 24px)";

                // 绘制标尺图形
                // 修复编辑器初始化标尺未绘制的问题【DUWRITER5_0-3638】
                // 如果存在任务，则等待任务完成再绘制标尺
                if (WriterControl_Task.__Tasks.length > 0) {
                    WriterControl_Task.AddCallbackForCompletedAllTasks(function () {
                        WriterControl_Rule.DrawRuleContent(rootElement, hrule);
                        WriterControl_Rule.DrawRuleContent(rootElement, vrule);
                    });
                } else {
                    WriterControl_Rule.DrawRuleContent(rootElement, hrule);
                    WriterControl_Rule.DrawRuleContent(rootElement, vrule);
                }

                var funcMouseEvent = function (e) {
                    // 处理鼠标事件
                    if (rootElement.__DCWriterReference != null) {
                        if (rootElement.__DCWriterReference.invokeMethod("get_RuleEnabled") == false) {
                            // 标尺不可用
                            return;
                        }
                        var strResult = rootElement.__DCWriterReference.invokeMethod(
                            "RuleHandleMouseEvent",
                            this.getAttribute("dctype"),
                            e.type,
                            e.altKey,
                            e.shiftKey,
                            e.ctrlKey,
                            e.offsetX,
                            e.offsetY,
                            e.buttons,
                            e.detail);
                        if (strResult != null && strResult.length > 0) {
                            if (strResult == "capturemouse") {
                                if (this.setCapture) {
                                    this.setCapture(true);
                                }
                            }
                            else if (strResult.indexOf(",") >= 0) {
                                var item9s = strResult.split(",");
                                this.title = item9s[0];
                                this.style.cursor = item9s[1];
                            }
                        }
                    }
                };
                hrule.onmousedown = funcMouseEvent;
                hrule.onmousemove = funcMouseEvent;
                hrule.onmouseup = funcMouseEvent;
                hrule.ondblclick = funcMouseEvent;

                vrule.onmousedown = funcMouseEvent;
                vrule.onmousemove = funcMouseEvent;
                vrule.onmouseup = funcMouseEvent;
                vrule.ondblclick = funcMouseEvent;
                rootElement.__DCWriterReference.invokeMethod("UpdateTextCaretWithoutScroll");
                return [hrule, vrule];
            }
            else {
                // 删除标尺对象
                DCTools20221228.RemoveChildByDCType(rootElement, "hrule");
                DCTools20221228.RemoveChildByDCType(rootElement, "vrule");
                pageContainer.style.height = "";
                rootElement.style.overflow = "auto";
                rootElement.__DCWriterReference.invokeMethod("UpdateTextCaretWithoutScroll");
                return null;
            }
        }
        else if (bolRuleVisible == true) {
            // 返回标尺对象
            return [DCTools20221228.GetChildNodeByDCType(rootElement, "hrule"),
            DCTools20221228.GetChildNodeByDCType(rootElement, "vrule")];
        }
        else {
            return null;
        }
    },

    /**
     * 声明标尺控件视图无效，需要重新绘制
     * @param {string | HTMLElement} containerID 容器元素对象
     * @param {string} ruleName 标尺名称
     */
    InvalidateView: function (containerID, ruleName) {
        var rule = DCTools20221228.GetChildNodeByDCType(containerID, ruleName);
        if (rule != null) {
            WriterControl_Rule.DrawRuleContent(containerID, rule);
        }
    },
    /**
     * 声明所有标尺控件视图无效，需要重新绘制
     * @param {string | HTMLElement} containerID 容器元素对象
     */
    InvalidateAllView: function (containerID) {
        var rule = DCTools20221228.GetChildNodeByDCType(containerID, "hrule");
        if (rule != null) {
            WriterControl_Rule.DrawRuleContent(containerID, rule);
        }
        rule = DCTools20221228.GetChildNodeByDCType(containerID, "vrule");
        if (rule != null) {
            WriterControl_Rule.DrawRuleContent(containerID, rule);
        }
    },
    SuppressPaintRule: false,
    /**
     * 绘制标尺元素
     * @param {HTMLElement} rootElement 根元素对象
     * @param {HTMLCanvasElement} ruleElement 标尺元素对象
     */
    DrawRuleContent: function (rootElement, ruleElement) {
        if (this.SuppressPaintRule === true) {
            return;
        }
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null || rootElement.__DCDisposed == true) {
            return;
        }
        //var currentPage = WriterControl_UI.GetCurrentPageElement(rootElement);


        //var pages = WriterControl_UI.GetPageCanvasElements(rootElement);
        //var firstPage = pages.length > 0 ? pages[0] : null;
        var strRuleName = ruleElement.getAttribute("dctype");
        //if (firstPage != null && strRuleName == "hrule") {
        //    if (ruleElement.width != firstPage.width) {
        //        ruleElement.width = firstPage.width;
        //    }
        //}
        //else if (firstPage != null && strRuleName == "vrule") {
        //    if (ruleElement.height != firstPage.height) {
        //        ruleElement.height = firstPage.height;
        //    }
        //}
        if (strRuleName == "vrule") {
            var vheight = rootElement.offsetHeight - 26;
            if (ruleElement.height <= vheight) {
                ruleElement.height = vheight;
            }
            //ruleElement.height = rootElement.offsetHeight - 24;
        }
        var drawer = new PageContentDrawer(ruleElement);
        drawer.AllowClip = false;
        drawer.SetClearRectangle(0, 0, ruleElement.width, ruleElement.height);
        drawer.EventQueryCodes = function () {
            var positionOffset = 0;
            var viewSize = strRuleName == "hrule" ? ruleElement.width : ruleElement.height;
            //区分打印控件和编辑器            
            if (rootElement.IsWriterPrintPreviewControlForWASM === true) {
                //打印控件下，直接使用pageContainer的scrollLeft和scrollTop
                let pageContainer = rootElement.querySelector('div[dctype="page-container"]');
                let pageContainerFirstSvg = pageContainer.firstChild;
                let pageContainerRect = pageContainer && pageContainer.getBoundingClientRect();
                let pageContainerFirstSvgRect = pageContainerFirstSvg && pageContainerFirstSvg.getBoundingClientRect();

                if (pageContainer === null) {
                    return;
                }
                if (strRuleName == "hrule") {
                    var offsetLeft = pageContainerFirstSvgRect.left - pageContainerRect.left;
                    // 水平标尺
                    positionOffset = offsetLeft + 25 - pageContainer.scrollLeft;
                    viewSize = pageContainer.offsetWidth;
                }
                else if (strRuleName == "vrule") {
                    var offsetTop = pageContainerFirstSvgRect.top - pageContainerRect.top;

                    // 垂直标尺
                    positionOffset = offsetTop - pageContainer.scrollTop + 0;
                    viewSize = pageContainer.offsetHeight;
                }

            } else {
                var curPage = null;
                curPage = WriterControl_UI.GetCurrentPageElement(rootElement);
                if (curPage != null) {
                    if (strRuleName == "hrule") {
                        // 水平标尺
                        positionOffset = curPage.offsetLeft + 25 - curPage.parentNode.scrollLeft;
                        viewSize = curPage.offsetWidth;
                        //drawer.SetClearRectangle(0, 0, viewSize, ruleElement.height);
                    }
                    else if (strRuleName == "vrule") {
                        positionOffset = curPage.offsetTop - curPage.parentNode.scrollTop + 0;
                        viewSize = curPage.offsetHeight;
                        //drawer.SetClearRectangle(0, 0, ruleElement.width, viewSize);
                    }
                }
            }
            if (rootElement.__DCDisposed == true) {
                return null;
            }
            var strCode = rootElement.__DCWriterReference.invokeMethod(
                "PaintRuleControl",
                strRuleName,
                positionOffset,
                viewSize);
            return strCode;
        };
        drawer.CanEatTask = function (otherTask) {
            if (this.CanvasElement == otherTask.CanvasElement) {
                return true;
            }
            return false;
        };
        drawer.AddToTask();
    },
    /**
     * 设置是否显示标尺
     * @param {string | HTMLElement} containerID 容器元素
     * @param {boolean} bolVisible 是否显示标尺
     * @returns {Array} 标尺元素对象
     */
    SetRuleVisible: function (containerID, bolVisible) {
        var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        if (rootElement == null || rootElement.__DCDisposed == true) {
            return null;
        }
        // var pages = WriterControl_UI.GetPageCanvasElements(containerID);
        // var firstPage = pages.length > 0 ? pages[0] : null;

        // var bolVisible = rootElement.__DCWriterReference.invokeMethod("get_RuleVisible");
        WriterControl_Rule.UpdateRuleVisible(containerID);
        // if (bolVisible == false) {
        //     // 隐藏标尺
        //     var div = DCTools20221228.GetChildNodeByDCType(rootElement, "rule-contaienr");
        //     if (div != null) {
        //         rootElement.removeChild(div);
        //     }
        //     return null;
        // }
        // // 显示标尺
        // var divContainer = DCTools20221228.GetChildNodeByDCType(rootElement, "rule-container");
        // if (divContainer == null) {
        //     // 创建标尺
        //     divContainer = rootElement.ownerDocument.createElement("DIV");
        //     divContainer.setAttribute("dctype", "rule-container");
        //     divContainer.style.backgroundColor = rootElement.__DCWriterReference.invokeMethod("GetRuleBackColorString");
        //     divContainer.style.height = "24px";
        //     divContainer.style.minWidth = "fit-content";
        //     divContainer.style.marginLeft = "5px";
        //     divContainer.style.marginRight = "5px";
        //     divContainer.style.top = "0px";
        //     divContainer.style.position = "sticky";
        //     divContainer.style.textAlign = "center";
        //     if (rootElement.firstChild == null) {
        //         rootElement.appendChild(divContainer);
        //     }
        //     else {
        //         rootElement.insertBefore(divContainer, rootElement.firstChild);
        //     }
        //     var hrule = rootElement.ownerDocument.createElement("CANVAS");
        //     hrule.setAttribute("dctype", "hrule");
        //     if (firstPage != null) {
        //         hrule.width = firstPage.width;
        //     }
        //     hrule.height = 24;
        //     divContainer.appendChild(hrule);
        //     WriterControl_Rule.DrawRuleContent(rootElement, hrule);

        //     var vrule = rootElement.ownerDocument.createElement("CANVAS");
        //     vrule.setAttribute("dctype", "vrule");
        //     vrule.style.display = "block";
        //     vrule.style.backgroundColor = hrule.style.backgroundColor;
        //     vrule.width = 24;
        //     if (firstPage != null) {
        //         vrule.height = rootElement.clientHeight;
        //     }
        //     divContainer.appendChild(vrule);
        //     WriterControl_Rule.DrawRuleContent(rootElement, vrule);
        // }
    }

};