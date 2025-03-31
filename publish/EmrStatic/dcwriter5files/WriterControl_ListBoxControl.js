//*************************************************************************
//* 项目名称：
//* 当前版本: V 5.3.1
//* 开始时间: 20230601
//* 开发者: 
//* 重要描述: 创建输入域的单选多选下拉列表HTML
//*************************************************************************
//* 最后更新时间: 2023-11-28 16:38:53
//* 最后更新人: xuyiming
//*************************************************************************
"use strict";

export let WriterControl_ListBoxControl = {
    /** 多选框不选择时展示的图片 */
    NoCheckedSvg: '<svg style="vertical-align: baseline;" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="16" height="16"><path d="M4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3ZM5 5V19H19V5H5Z" fill="rgba(0,0,0,0.6)"></path></svg>',
    /** 多选框选择时展示的图片 */
    CheckedSvg: '<svg style="vertical-align: baseline;" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="16" height="16"><path d="M4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3ZM5 5V19H19V5H5ZM11.0026 16L6.75999 11.7574L8.17421 10.3431L11.0026 13.1716L16.6595 7.51472L18.0737 8.92893L11.0026 16Z" fill="rgba(0,0,0,0.6)"></path></svg>',
    /** 获取列表格式化字符串
     * @param {Object} data {includeText:"",includeValue:"",excludeText:"",excludeValue:""}
     */
    GetListValueFormatString: function (inputNode, data) {
        var resultData = {
            Text: data.includeText,
            Value: data.includeValue,
        };
        // inputNode为空
        if (!inputNode) return resultData;
        // 有[includelist],无[excludelist]
        var ListValueFormatString = inputNode.ListValueFormatString;//列表格式化字符串
        // 不是字符串，返回
        if (typeof (ListValueFormatString) != "string") return resultData;
        // 目前特殊处理列表格式化字符串是【有无列表】时的情况和【有[includelist],无[excludelist]】一致【DUWRITER5_0-3861】
        if (ListValueFormatString == "有无列表") {
            ListValueFormatString = "有[includelist],无[excludelist]";
        }
        // 没有存在包含和不包含项目
        if (ListValueFormatString.indexOf("[includelist]") == -1 || ListValueFormatString.indexOf("[excludelist]") == -1) {
            return resultData;
        }
        // replace(/\[includelist\]/g,"")//包含替换
        // replace(/\[excludelist\]/g,"")//不包含替换
        resultData.Text = ListValueFormatString.replace(/\[includelist\]/g, data.includeText).replace(/\[excludelist\]/g, data.excludeText);
        resultData.Value = ListValueFormatString.replace(/\[includelist\]/g, data.includeValue).replace(/\[excludelist\]/g, data.excludeValue);
        if (resultData.Text.slice(0, 2) == "有,") {
            resultData.Text = resultData.Text.slice(2);
        }
        if (resultData.Value.slice(0, 2) == "有,") {
            resultData.Value = resultData.Value.slice(2);
        }
        if (resultData.Text.slice(-2) == ",无") {
            resultData.Text = resultData.Text.slice(0, resultData.Text.length - 2);
        }
        if (resultData.Value.slice(-2) == ",无") {
            resultData.Value = resultData.Value.slice(0, resultData.Value.length - 2);
        }
        return resultData;
    },

    /** 反编译列表格式化字符串
     * @param {Object} inputNode 输入域属性对象
     * @param {Object} data Text和Value
     */
    DecompilationListValueFormatString: function (inputNode, data) {
        var resultData = {
            Text: data.Text,
            Value: data.Value,
        };
        // inputNode为空
        if (!inputNode) return resultData;
        // 有[includelist],无[excludelist]
        var ListValueFormatString = inputNode.ListValueFormatString;//列表格式化字符串
        // 不是字符串，返回
        if (typeof (ListValueFormatString) != "string") return resultData;
        // 目前特殊处理列表格式化字符串是【有无列表】时的情况和【有[includelist],无[excludelist]】一致【DUWRITER5_0-3861】
        if (ListValueFormatString == "有无列表") {
            ListValueFormatString = "有[includelist],无[excludelist]";
        }
        // 没有存在包含和不包含项目
        if (ListValueFormatString.indexOf("[includelist]") == -1 || ListValueFormatString.indexOf("[excludelist]") == -1) {
            return resultData;
        }
        var NowText = data.Value || data.Text;//现在的值
        var FormatStr = ListValueFormatString;
        // 第一个字在什么地方
        var OneCharIndex = ListValueFormatString.indexOf(NowText.slice(0, 1));
        if (OneCharIndex > -1) {
            FormatStr = ListValueFormatString.slice(OneCharIndex);
        }
        // 判断当前是否有选中的项
        var listArr1 = FormatStr.split("[");
        var listArr = [];
        for (var i = 0; i < listArr1.length; i++) {
            var listArr2 = listArr1[i].split("]");
            for (var j = 0; j < listArr2.length; j++) {
                if (listArr2[j] != "") {
                    listArr.push(listArr2[j]);
                }
            }
        }
        resultData = {
            Text: GetIncludeText(resultData.Text),
            Value: GetIncludeText(resultData.Value),
        };
        function GetIncludeText(str) {
            if (str && str !== '') {
                var textIndexArr = [];
                for (var i = 0; i < listArr.length; i++) {
                    if (str.indexOf(listArr[i]) != -1) {
                        textIndexArr.splice(i, 0, [str.indexOf(listArr[i]), listArr[i]]);
                    }
                }
                var textArr = [];
                for (var i = 0; i < textIndexArr.length; i++) {
                    if (textIndexArr[0][0] != 0) {
                        textArr.push(str.substring(0, textIndexArr[0][0]));
                    }
                    textArr.push(str.substring(textIndexArr[i][0], textIndexArr[i][0] + textIndexArr[i][1].length));
                    if (i + 1 < textIndexArr.length) {
                        textArr.push(str.substring(textIndexArr[i][0] + textIndexArr[i][1].length, textIndexArr[i + 1][0]));
                    } else if (i + 1 == textIndexArr.length) {
                        textArr.push(str.substring(textIndexArr[i][0] + textIndexArr[i][1].length));
                    }
                }
                var obj = {};
                for (var i = 0; i < listArr.length; i++) {
                    obj[listArr[i]] = textArr[i];
                }
                return obj["includelist"] || "";
            }
            return "";
        }
        return resultData;
    },

    /** 创建下拉文本的方法
     * @param {Object} listItems 输入域下拉列表对象
     * @param {Function} callBack 点击回调函数
     * @param {*} rootElement 编辑器元素本身
     * @param {*} divContainer 下拉元素本身
     * @param {string} oldText 输入域旧文本值
     * @param {string} oldValue 输入域旧Value值
     * @param {Object} currentInputProps 当前输入域属性对象
     * @param {Object} args 参数
     */
    CreateListBoxControl: function (listItems, callBack, rootElement, divContainer, oldText, oldValue, currentInputProps, args) {
        // 处理格式化字符串
        var FormatData = this.DecompilationListValueFormatString(currentInputProps, {
            Text: oldText,
            Value: oldValue ? oldValue : oldText
        });
        oldText = FormatData.Text, oldValue = FormatData.Value;
        /** 下拉列表盒子 */
        var listBox = null;
        if (listItems != null && listItems.length > 0) {
            // 获取用户设置的下拉大小和字体
            var DropdownListFontSize = 12, DropdownListFontName = "auto";
            // 添加判断避免报错
            if (rootElement && rootElement.DocumentOptions && rootElement.DocumentOptions.ViewOptions) {
                // 下拉列表字体大小
                // 添加自动缩放下拉列表字体大小属性(AutoZoomDropdownListFontSize)的支持，比例是12*缩放比率【DUWRITER5_0-2698】
                if (rootElement.DocumentOptions.ViewOptions.DropdownListFontSize > 0) {
                    DropdownListFontSize = rootElement.DocumentOptions.ViewOptions.DropdownListFontSize;
                } else if (rootElement.DocumentOptions.ViewOptions.AutoZoomDropdownListFontSize == true) {
                    // 自动缩放下拉列表字体大小
                    // 扩展：当设置了DropdownListFontSize>0时，本属性不起作用。
                    DropdownListFontSize = parseInt(12 * rootElement.GetZoomRate() * 100) / 100;
                }
                // 下拉列表字体名称
                DropdownListFontName = rootElement.DocumentOptions.ViewOptions.DropdownListFontName || "auto";
            }

            //用户自定义下拉框最大高度：
            let DropDownListMaxHeight = rootElement.getAttribute("DropDownListMaxHeight") || null;
            DropDownListMaxHeight = DropDownListMaxHeight ? (parseInt(DropDownListMaxHeight) * rootElement.GetZoomRate()) : null;
            //下拉项最大高度的样式，18为默认行高
            let DropDownListMaxHeightCss = `
                display: -webkit-box;
                -webkit-line-clamp:${Math.floor(DropDownListMaxHeight / (DropdownListFontSize * 1.2))};
                -webkit-box-orient: vertical;
                overflow: hidden;
                text-overflow: ellipsis;
                max-height: ${DropDownListMaxHeight}px;
                white-space: normal;
                word-break: break-all;
            `;

            // 样式元素
            var styleDom = rootElement.ownerDocument.createElement("style");
            styleDom.innerHTML = `
                .dcListBox .dc_ListItem,.dc_titleDiv[TwoColumnDisplay=true]{text-align:left;display:flex;align-items: center;width:100%;font-size:${DropdownListFontSize}px; font-family:${DropdownListFontName}; min-height: 30px;background-color: transparent;padding: 0;overflow: hidden;white-space: nowrap;cursor: pointer;border-bottom: 1px solid #eee;box-sizing: border-box;}
                .dcListBox .dc_ListItem:hover,.dcListBox .dc_ListItem[moveli=true]{background-color:#eaf2ff!important;color:#000000!important;outline:1px solid #b7d2ff!important;}
                /** 标题的样式 */
                .dc_titleDiv[TwoColumnDisplay=true]{border-bottom:1px solid #000;font-weight:700;background-color:#EDEEE1;flex: none;}
                .dc_titleDiv[TwoColumnDisplay=true] div[dc-data]{padding:2px 5px;}
                .dc_titleDiv[TwoColumnDisplay=true] div[dc-data=text]{width:40%;}
                .dc_titleDiv[TwoColumnDisplay=true] div[dc-data=value]{width:60%;}
                /** 下面是表格类型的样式 */
                .dcListBox[TwoColumnDisplay=true]{border:1px solid #000;border-radius: inherit;}
                .dcListBox[TwoColumnDisplay=true] div[dc-data]{padding:2px 5px;}
                /** 兼容两列下拉时的自定义高度样式 */
                .dcListBox[TwoColumnDisplay=true] div[dc-data=text]{width:40%;${DropDownListMaxHeight ? DropDownListMaxHeightCss : "overflow: hidden;white-space: nowrap;text-overflow: ellipsis;"}}
                .dcListBox[TwoColumnDisplay=true] div[dc-data=value]{width:60%;${DropDownListMaxHeight ? DropDownListMaxHeightCss : "overflow: hidden;white-space: nowrap;text-overflow: ellipsis;"}}
                /** 兼容单列下拉时的自定义高度样式 */
                .dcListBox .dc_ListItemText{${DropDownListMaxHeight ? DropDownListMaxHeightCss : "display:inline-block;width:100%;overflow: hidden;white-space: nowrap;text-overflow: ellipsis;"}}
                `;
            divContainer.appendChild(styleDom);
            // 添加属性DropdownList_TwoColumnDisplay="true"让单选下拉展示为两栏样式
            // 下拉列表展示的样式是否为两栏展示类型
            var isTwoColumnDisplay = rootElement.getAttribute("DropdownList_TwoColumnDisplay") == "true";
            //var isTwoColumnDisplay = false;
            // 创建外层包裹元素
            listBox = rootElement.ownerDocument.createElement("div");
            listBox.setAttribute("class", "dcListBox");
            listBox.style.cssText = "list-style:none;margin:0;positon:absoulte;background-color: #ffffff;color: #444;";
            if (isTwoColumnDisplay == false || !currentInputProps) {
                // 在此处判断是否为分组互斥
                var oldGroupText = null;
                // 循环解析listItems;
                for (var i = 0; i < listItems.length; i++) {
                    var item = listItems[i];
                    // 原本文本和值,itemShowText展示的文本
                    var nativeText, nativeValue, itemShowText;
                    if (typeof (item) == "string") {
                        // 数据是字符串
                        nativeText = item ? item : "";
                        nativeValue = nativeText;
                        itemShowText = nativeText;
                    } else {
                        nativeText = item.Text ? item.Text : "";
                        nativeValue = item.Value ? item.Value : nativeText;
                        itemShowText = item.TextInList ? item.TextInList : nativeText;
                    }
                    /** 存储列表项的元素 */
                    var ListItemBox = rootElement.ownerDocument.createElement("div");
                    // 设置class名称
                    ListItemBox.className = "dc_ListItem";
                    // 添加样式
                    ListItemBox.style.cssText = "padding: 5px 20px 5px 10px;";
                    // 赋值内容
                    ListItemBox.setAttribute("native-text", nativeText);
                    // value值
                    ListItemBox.setAttribute("value", nativeValue);
                    // 下拉展示内容
                    ListItemBox.innerHTML = `<span class="dc_ListItemText" title="${itemShowText}">${itemShowText}</span>`;
                    // 如果oldValue或者oldText存在,给上样式并将下拉滚动到对应位置
                    if (oldValue != null && oldText != null && nativeValue == oldValue && nativeText == oldText) {
                        ListItemBox.style.backgroundColor = '#eaf2ff';
                        ListItemBox.style.color = '#000000';
                        ListItemBox.style.outline = '1px solid #b7d2ff';
                        ListItemBox.style.position = 'relative';
                        ListItemBox.setAttribute("targetLi", true);

                        //[DUWRITER5_0-1657]lxy20240109：给单选列表增加一个删除选中项功能
                        //[DUWRITER5_0-1907]lxy20240201:增加自定义属性DropdownListItemDeselectButton，是否展示取消选中按钮。
                        var DropdownListItemDeselectButton = rootElement.getAttribute('DropdownListItemDeselectButton');

                        //为了不影响已经在使用此功能的客户， 默认情况下是展示按钮的。除非用户设置值为false才不展示按钮
                        if (DropdownListItemDeselectButton !== 'false') {
                            var closeSpan = rootElement.ownerDocument.createElement("span");
                            closeSpan.innerHTML = '&times;';
                            closeSpan.title = '取消选择';
                            closeSpan.style.cssText = `display: inline-block;
                                        position: absolute;
                                        right: 2px;
                                        font-size: 12px;
                                        border-radius: 10px;
                                        top: 50%;
                                        margin-top: -7px;
                                        line-height: 14px;
                                        text-align: center;
                                        cursor: pointer;
                                        color: rgb(74 137 220);`;
                            ListItemBox.appendChild(closeSpan);
                            closeSpan.addEventListener('click', function (e) {
                                e.stopPropagation();// 阻止冒泡
                                // 设置为不选择内容
                                rootElement.SetElementProperties(currentInputProps.NativeHandle, { InnerValue: null, text: null });
                                // 关闭单选下拉
                                var hasDropDown = rootElement.querySelector('#divDropdownContainer20230111');
                                hasDropDown.CloseDropdown('true');
                                // 触发文档内容变化事件
                                var opt = {
                                    /** 触发事件类型 */
                                    TriggerType: "ListBoxControlClearContent"
                                };
                                WriterControl_Event.RaiseControlEvent(rootElement, "DocumentContentChanged", opt);
                            });
                        }
                    }
                    //判断是否为分组互斥
                    if (currentInputProps && currentInputProps.RepulsionForGroup) {
                        item.Group = item.Group == null ? '' : item.Group;
                        ListItemBox.setAttribute('dc_group', item.Group);
                        ListItemBox.style.borderBottom = "none";
                        //判断是否为已存在元素
                        if (oldGroupText != null) {
                            if (item.Group == oldGroupText) {
                                //如果是相同group
                                listBox.appendChild(ListItemBox);
                            } else {
                                //如果是不同group
                                var hasSameGroup = listBox.querySelectorAll(`[dc_group="${item.Group}"]`);
                                if (hasSameGroup && hasSameGroup.length > 0) {
                                    //存在相同的group
                                    hasSameGroup[hasSameGroup.length - 1].after(ListItemBox);
                                } else {
                                    //不存在相同的group
                                    //创建一个分割线
                                    var groupLine = rootElement.ownerDocument.createElement("span");
                                    groupLine.style.cssText = "display:flex;heigth:0px;width:100%;border-top:2px solid #000;line-height:0px;margin: 1px 0px;padding: 0px;";
                                    groupLine.setAttribute('dcignore', '1');
                                    listBox.appendChild(groupLine);
                                    listBox.appendChild(ListItemBox);
                                }
                            }
                        } else {
                            listBox.appendChild(ListItemBox);
                        }
                        oldGroupText = item.Group;
                    } else {
                        // 列表项元素插入到整体包裹元素
                        listBox.appendChild(ListItemBox);
                    }
                };
            } else {
                // 下拉列表展示的样式新增加一种展示方式,类似两栏展示类型
                // 设置表示两栏展示的属性
                listBox.setAttribute("TwoColumnDisplay", "true");
                // 添加标题
                /** 存储标题的元素 */
                var titleBox = rootElement.ownerDocument.createElement("div");
                var Text_Title = "代码值", Value_Title = "代码标题";
                // 设置class名称
                titleBox.className = "dc_titleDiv";
                // 设置过滤掉的属性
                titleBox.setAttribute("dcignore", 1);
                // 设置表示两栏展示的属性
                titleBox.setAttribute("TwoColumnDisplay", "true");
                // 填充标题元素
                titleBox.innerHTML = "<div dc-data='text' title='" + Text_Title + "'>" + Text_Title + "</div><div dc-data='value' title='" + Value_Title + "'>" + Value_Title + "</div>";
                listBox.appendChild(titleBox);
                for (var i = 0; i < listItems.length; i++) {
                    var item = listItems[i];
                    // 原本文本和值
                    var nativeText, nativeValue;
                    if (typeof (item) == "string") {
                        // 数据是字符串
                        nativeText = item ? item : "";
                        nativeValue = nativeText;
                    } else {
                        nativeText = item.Text ? item.Text : "";
                        nativeValue = item.Value ? item.Value : "";
                    }
                    /** 存储列表项的元素 */
                    var ListItemBox = rootElement.ownerDocument.createElement("div");
                    // 设置class名称
                    ListItemBox.className = "dc_ListItem";
                    // 赋值内容
                    ListItemBox.setAttribute("native-text", nativeText);
                    // value值
                    ListItemBox.setAttribute("value", nativeValue ? nativeValue : nativeText);
                    // 填充列表项元素
                    ListItemBox.innerHTML = "<div dc-data='text' title='" + nativeText + "'>" + nativeText + "</div><div dc-data='value' title='" + nativeValue + "'>" + nativeValue + "</div>";
                    // 如果oldValue或者oldText存在,给上样式并将下拉滚动到对应位置
                    if (oldValue != null && oldText != null && nativeValue == oldValue && nativeText == oldText) {
                        ListItemBox.style.backgroundColor = '#eaf2ff';
                        ListItemBox.style.color = '#000000';
                        ListItemBox.style.outline = '1px solid #b7d2ff';
                        ListItemBox.style.position = 'relative';
                        ListItemBox.setAttribute("targetLi", true);

                        //[DUWRITER5_0-1907]lxy20240201:增加自定义属性DropdownListItemDeselectButton，是否展示取消选中按钮。
                        var DropdownListItemDeselectButton = rootElement.getAttribute('DropdownListItemDeselectButton');
                        //为了不影响已经在使用此功能的客户， 默认情况下是展示按钮的。除非用户设置值为false才不展示按钮
                        if (DropdownListItemDeselectButton !== 'false') {
                            //[DUWRITER5_0-1657]lxy20240109：给单选列表增加一个删除选中项功能
                            var closeSpan = rootElement.ownerDocument.createElement("span");
                            closeSpan.innerHTML = '&times;';
                            closeSpan.title = '取消选择';
                            closeSpan.style.cssText = `display: inline-block;
                                        position: absolute;
                                        right: 2px;
                                        font-size: 12px;
                                        border-radius: 10px;
                                        top: 50%;
                                        margin-top: -7px;
                                        line-height: 14px;
                                        text-align: center;
                                        cursor: pointer;
                                        color: rgb(74 137 220);`;
                            ListItemBox.appendChild(closeSpan);
                            closeSpan.addEventListener('click', function (e) {
                                e.stopPropagation();// 阻止冒泡
                                // 设置为不选择内容
                                rootElement.SetElementProperties(currentInputProps.NativeHandle, { InnerValue: null, text: null });
                                // 关闭单选下拉
                                var hasDropDown = rootElement.querySelector('#divDropdownContainer20230111');
                                hasDropDown.CloseDropdown('true');
                                // 触发文档内容变化事件
                                var opt = {
                                    /** 触发事件类型 */
                                    TriggerType: "ListBoxControlClearContent"
                                };
                                WriterControl_Event.RaiseControlEvent(rootElement, "DocumentContentChanged", opt);
                            });
                        }
                    }
                    // 列表项元素插入到整体包裹元素
                    listBox.appendChild(ListItemBox);
                };
            }
            //对listBox进行判断
            listBox.addEventListener('click', function (e) {
                WriterControl_ListBoxControl.CheckListBox(e, callBack, currentInputProps, listBox, rootElement);
            });
            // 修复单选下拉展示为两栏样式时无法滚动的问题
            // listBox.addEventListener('mousewheel', function (e) {
            //     var delta = (e.wheelDelta && (e.wheelDelta > 0 ? 1 : -1)) || // chrome & ie
            //         (e.detail && (e.detail > 0 ? -1 : 1)); // firefox
            //     if (this.scrollHeight > (this.innerHeight || this.clientHeight)) {
            //         if (this.scrollTop == 0 && delta == 1) {
            //             return;
            //         }
            //         if (this.scrollHeight - (this.scrollTop + this.clientHeight) == 0 && delta == -1) {
            //             return;
            //         }
            //         // 阻止事件冒泡,允许上下滑动
            //         if (e.stopPropagation) {
            //             e.stopPropagation();
            //         } else {
            //             e.cancelBubble = true;
            //         }
            //     }
            // });
            // listBox.addEventListener('mouseover', function (e) {
            //     var _target = e.srcElement ? e.srcElement : e.target;
            //     var allli = listBox.querySelectorAll('div');
            //     allli.forEach(function (item) {
            //         if (item == _target) {
            //             item.style.backgroundColor = '#eaf2ff';
            //             item.style.color = '#000000';
            //             item.style.outline = '1px solid #b7d2ff';
            //             item.setAttribute('moveLi', 'true');
            //         } else {
            //             item.style.backgroundColor = '#ffffff';
            //             item.style.color = '#444';
            //             item.style.outline = 'none';
            //             item.removeAttribute('moveLi', 'true');
            //         }
            //     });
            // });
            //listBox.addEventListener('mouseout', function (e) {
            //    e.target.style.backgroundColor = '#ffffff';
            //    e.target.style.color = '#444';
            //    e.target.style.outline = 'none';
            //});
        }
        // 给下拉框插入搜索框
        WriterControl_ListBoxControl.CreateDropdownCodeArea(listBox, rootElement, divContainer, currentInputProps, args);
        return listBox;
    },

    /**
     * 单选下拉触发事件
     * @param {object} e 事件Event
     * @param {Function} callBack 回调函数
     * @param {Object} currentInputProps 当前输入域属性对象
     * @param {Element} listBox 当前下拉内容元素
     * @param {Element} rootElement 编辑器对象
     */
    CheckListBox: function (e, callBack, currentInputProps, listBox, rootElement) {
        var _target = e.srcElement ? e.srcElement : e.target ? e.target : e;
        if (_target == null) {
            return;
        }
        while (_target != listBox && _target.hasAttribute("native-text") == false) {
            _target = _target.parentElement || _target.parentNode;
        }
        if (_target != null && _target.hasAttribute("native-text")) {
            if (!!callBack && typeof (callBack) == "function") {
                var AssignmentText = _target.getAttribute("native-text");// 赋值的Text值
                var AssignmentValue = _target.getAttribute("value");// 赋值的Value值
                if (currentInputProps && currentInputProps.ListValueFormatString) {// 存在格式化字符串
                    // 修复存在格式化字符串单选列表没有“无”后面内容的问题【DUWRITER5_0-3861】
                    var AllListItems = listBox.querySelectorAll("div[native-text]");// 所有的下拉列表
                    var ListValueSeparatorChar = currentInputProps.ListValueSeparatorChar || ",";
                    var excludeText = "", excludeValue = "";
                    for (var i = 0; i < AllListItems.length; i++) {
                        if (AllListItems[i] != _target) {
                            if (excludeText != "") {
                                excludeText += ListValueSeparatorChar;
                            }
                            excludeText += AllListItems[i].getAttribute("native-text");
                            if (excludeValue != "") {
                                excludeValue += ListValueSeparatorChar;
                            }
                            excludeValue += AllListItems[i].getAttribute("value");
                        }
                    }
                    var opt = {
                        includeText: AssignmentText,
                        includeValue: AssignmentValue,
                        excludeText: excludeText,
                        excludeValue: excludeValue
                    };
                    // 获取当前的格式化文本数据
                    var FormatStrData = WriterControl_ListBoxControl.GetListValueFormatString(currentInputProps, opt);
                    callBack.call(_target, FormatStrData.Text, FormatStrData.Value, rootElement);
                } else {
                    callBack.call(_target, AssignmentText, AssignmentValue, rootElement);
                }
                /** 是否启动快速辅助录入模式 */
                var FastInputMode = false;
                if (rootElement && rootElement.DocumentOptions && rootElement.DocumentOptions.BehaviorOptions) {
                    FastInputMode = rootElement.DocumentOptions.BehaviorOptions.FastInputMode;
                }
                if (FastInputMode) {
                    var nextInput = rootElement.GetNextFocusFieldElement(currentInputProps.NativeHandle);
                    if (nextInput) {
                        rootElement.FocusElement(nextInput);
                    }
                }
            }
        }
    },

    /**
     * 创建多选下拉框的方法
     * @param {Object} listItems 输入域下拉列表对象
     * @param {Function} callBack 点击回调函数
     * @param {*} rootElement 编辑器元素本身
     * @param {*} divContainer 下拉元素本身
     * @param {string} oldText 输入域旧文本值
     * @param {string} oldValue 输入域旧Value值
     * @param {Object} currentInputProps 当前输入域属性对象
     * @param {Object} args 参数
     * @returns 
     */
    CreateMultiSelectControl: function (listItems, rootElement, divContainer, oldText, oldValue, currentInputProps, args) {
        var FormatData = this.DecompilationListValueFormatString(currentInputProps, {
            Text: oldText,
            Value: oldValue
        });
        oldText = FormatData.Text, oldValue = FormatData.Value;
        var meunDiv = null;
        if (listItems != null && Array.isArray(listItems)) {
            meunDiv = rootElement.ownerDocument.createElement('div');
            meunDiv.setAttribute('id', 'MultiSelectControl');
            //meunDiv.innerHTML = `<div class="MultiSelect-line"></div>`;
            //min-width: 144px;
            // 获取用户设置的下拉大小和字体
            var DropdownListFontSize = 12, DropdownListFontName = 'auto';
            // 添加判断避免报错
            if (rootElement && rootElement.DocumentOptions && rootElement.DocumentOptions.ViewOptions) {
                // 下拉列表字体大小
                // 添加自动缩放下拉列表字体大小属性(AutoZoomDropdownListFontSize)的支持，比例是12*缩放比率【DUWRITER5_0-2698】
                if (rootElement.DocumentOptions.ViewOptions.DropdownListFontSize > 0) {
                    DropdownListFontSize = rootElement.DocumentOptions.ViewOptions.DropdownListFontSize;
                } else if (rootElement.DocumentOptions.ViewOptions.AutoZoomDropdownListFontSize == true) {
                    // 自动缩放下拉列表字体大小
                    // 扩展：当设置了DropdownListFontSize>0时，本属性不起作用。
                    DropdownListFontSize = parseInt(12 * rootElement.GetZoomRate() * 100) / 100;
                }
                // 下拉列表字体名称
                DropdownListFontName = rootElement.DocumentOptions.ViewOptions.DropdownListFontName || "auto";
            }
            //用户自定义下拉框最大高度：
            let DropDownListMaxHeight = rootElement.getAttribute("DropDownListMaxHeight") || null;
            DropDownListMaxHeight = DropDownListMaxHeight ? (parseInt(DropDownListMaxHeight) * rootElement.GetZoomRate()) : null;
            //下拉项最大高度的样式，18为默认行高
            let DropDownListMaxHeightCss = `
                display: -webkit-box;
               -webkit-line-clamp:${Math.floor(DropDownListMaxHeight / (DropdownListFontSize * 1.2))};
                -webkit-box-orient: vertical;
                overflow: hidden;
                text-overflow: ellipsis;
                max-height: ${DropDownListMaxHeight}px;
                white-space: normal;
                text-align: left;
                word-break: break-all;
            `;

            // 样式元素
            var ContextMenuCss = rootElement.ownerDocument.createElement('style');
            ContextMenuCss.setAttribute('id', 'MultiSelectCss');
            ContextMenuCss.innerHTML = `
                #MultiSelectControl{
                    margin: 0;
                    border-width: 1px;
                    border-style: solid;
                    background-color: #fafafa;
                    border-color: #ddd;
                    color: #444;
                    box-shadow: rgb(204, 204, 204) 2px 2px 3px;
                }
                #MultiSelectControl .MultiSelect-line{
                    position: absolute;
                    left: 26px;
                    top: 0;
                    height: 100%;
                    font-size: 1px;
                    border-left: 1px solid #ccc;
                    border-right: 1px solid #fff;
                }
                #MultiSelectControl .MultiSelect-item{
                    position: relative;
                    white-space: nowrap;
                    cursor: pointer;
                    margin: 0px;
                    padding: 0px;
                    overflow: hidden;
                    border-width: 1px;
                    border-style: solid;
                    border-color: transparent;
                    border-bottom: 1px solid rgb(238, 238, 238);
                }
                #MultiSelectControl .MultiSelect-item:hover,
                #MultiSelectControl .MultiSelect-item[moveli=true]{
                    background-color:#eaf2ff!important;
                    color:#000000!important;
                    outline:1px solid #b7d2ff!important;
                }
                #MultiSelectControl .MultiSelect-item .MultiSelect-text{
                    float: left;
                    padding-left: 28px;
                    padding: 5px 0px 5px 28px;
                    width: 100%;
                    font-size: ${DropdownListFontSize}px;
                    font-family: ${DropdownListFontName};
                    box-sizing: border-box;
                    min-height: 28px;
                    display: flex;
                    align-items: center;
                }
                #MultiSelectControl .MultiSelect-item .MultiSelect-text .dc_ListItemText{
                    ${DropDownListMaxHeight ? DropDownListMaxHeightCss : `
                    display: inline-block;
                    width: 100%;
                    overflow: hidden;
                    white-space: nowrap;
                    text-overflow: ellipsis;
                    text-align: left;`}
                }
                #MultiSelectControl .MultiSelect-icon{
                    position: absolute;
                    width: 16px;
                    height: 16px;
                    left: 2px;
                    top: 50%;
                    margin-top: -8px;
                }
                #MultiSelectControl .MultiSelect-sep{
                    margin: 3px 0px 3px 25px;
                    font-size: 1px;
                    border-top: 1px solid #ccc;
                    border-bottom: 1px solid #fff;
                }`;
            divContainer.appendChild(ContextMenuCss);

            var separatorChar = currentInputProps.ListValueSeparatorChar ? currentInputProps.ListValueSeparatorChar : ',';//列表项目分割字符
            if (Array.isArray(listItems) && listItems.length > 0) {
                // 解析oldText和oldValue
                // 给默认值
                // 修改多选项选中逻辑为先判断Value属性，没有Value属性时再判断Text属性【DUWRITER5_0-3974】
                /** 是否通过Value属性进行判断 */
                var IsCheckValue = true;
                /** 判断需要的数组 */
                var CheckArray = [];
                if (oldValue) {
                    CheckArray = oldValue.split(separatorChar);
                } else {
                    IsCheckValue = false;
                    CheckArray = oldText.split(separatorChar);
                }
                // 下拉搜索中‘使用此文本’按钮可以展示时，将已插入的文本作为下拉项展示
                if (rootElement.getAttribute("DropdownListIsShowUsingSearchContent") == "true") {
                    //多选：使用搜索的内容为下拉项的处理，将已使用的搜索内容以下拉选项的形式展示，便于用户选中和取消
                    var diyTextArr = currentInputProps.Text ? currentInputProps.Text.split(',') : [];
                    var ListItemText = listItems && listItems.length ? listItems.map(item => item.Text) : [];
                    for (var i = 0; i < diyTextArr.length; i++) {
                        if (ListItemText.indexOf(diyTextArr[i]) === -1) {
                            //isUserDefinedDomHidden 用于隐藏用户自定义插入的下拉选项值
                            listItems.push({ "Text": diyTextArr[i], "Value": diyTextArr[i], isUserDefinedDomHidden: true });
                        }
                    }
                }

                // 在此处判断是否为分组互斥
                var oldGroupText = null;
                //根据listItems显示元素
                for (var option = 0; option < listItems.length; option++) {
                    //if (typeof listItems[option] == 'object') {
                    var itemEle = rootElement.ownerDocument.createElement('div');
                    itemEle.setAttribute('class', 'MultiSelect-item');
                    //isUserDefinedDomHidden用于隐藏用户自定义插入的下拉选项值
                    itemEle.style.cssText = `min-height: 30px;display:${listItems[option].isUserDefinedDomHidden ? 'none;' : 'block'}`;
                    if (listItems[option].isUserDefinedDomHidden) {
                        itemEle.setAttribute('isUserDefinedDomHidden', "true");
                        itemEle.setAttribute('dcignore', '1');
                    }
                    var nativeText = '',//赋值内容
                        innerText = '',//下拉展示内容
                        innerValue = '';//value值
                    if (typeof (listItems[option]) == "string") {
                        innerText = nativeText = innerValue = listItems[option] || "";
                    } else {
                        nativeText = listItems[option].Text || "";
                        innerText = listItems[option].TextInList ? listItems[option].TextInList : (listItems[option].Text || "");
                        innerValue = listItems[option].Value ? listItems[option].Value : listItems[option].Text ? listItems[option].Text : "";
                    }
                    // 判断是否为已选择的值
                    var isChecked = false;
                    if (IsCheckValue) {
                        isChecked = CheckArray.includes(innerValue);
                    } else {
                        isChecked = CheckArray.includes(nativeText);
                    }
                    /** 多选框的Html值 */
                    var MultiSelectIconHtml = isChecked ? WriterControl_ListBoxControl.CheckedSvg : WriterControl_ListBoxControl.NoCheckedSvg;
                    itemEle.innerHTML = `
                        <div class="MultiSelect-text" native-text="${nativeText}" value="${innerValue}">
                            <span class="dc_ListItemText" title="${innerText}">${innerText}</span>
                        </div>
                        <div class="MultiSelect-icon">${MultiSelectIconHtml}</div>
                    `;
                    // 判断是否为已选择的值
                    if (isChecked) {
                        itemEle.setAttribute('targetLi', 'true');
                    }
                    // 判断是否为分组互斥
                    if (currentInputProps.RepulsionForGroup) {
                        listItems[option].Group = listItems[option].Group == null ? '' : listItems[option].Group;
                        itemEle.setAttribute('dc_group', listItems[option].Group);
                        itemEle.style.borderBottom = "none";
                        //判断是否为已存在元素
                        if (oldGroupText != null) {
                            if (listItems[option].Group == oldGroupText) {
                                //如果是相同group
                                meunDiv.appendChild(itemEle);
                            } else {
                                //如果是不同group
                                var hasSameGroup = meunDiv.querySelectorAll(`[dc_group="${listItems[option].Group}"]`);
                                if (hasSameGroup && hasSameGroup.length > 0) {
                                    //存在相同的group
                                    hasSameGroup[hasSameGroup.length - 1].after(itemEle);
                                } else {
                                    //不存在相同的group
                                    //创建一个分割线
                                    var groupLine = rootElement.ownerDocument.createElement("span");
                                    groupLine.style.cssText = "display:flex;heigth:0px;width:100%;border-top:2px solid #000;line-height:0px;margin: 1px 0px;padding: 0px;";
                                    groupLine.setAttribute('dcignore', '1');
                                    meunDiv.appendChild(groupLine);
                                    meunDiv.appendChild(itemEle);
                                }
                            }
                        } else {
                            meunDiv.appendChild(itemEle);
                        }
                        oldGroupText = listItems[option].Group;
                    } else {
                        // 列表项元素插入到整体包裹元素
                        meunDiv.appendChild(itemEle);
                    }
                }
                //找到所有的下拉框并给上样式
                var alllargetLi = meunDiv.querySelectorAll('[targetLi=true]');
                if (alllargetLi && alllargetLi.length > 0) {
                    var lastLi = alllargetLi[alllargetLi.length - 1];
                    lastLi.style.backgroundColor = '#eaf2ff';
                    lastLi.style.color = '#000000';
                    lastLi.style.outline = '1px solid #b7d2ff';
                }
            }

            //对listBox进行判断
            meunDiv.addEventListener('click', function (e) {
                WriterControl_ListBoxControl.CheckMultiSelect(e, meunDiv, separatorChar, currentInputProps, rootElement);
            });
        }
        // 给下拉框插入搜索框
        WriterControl_ListBoxControl.CreateDropdownCodeArea(meunDiv, rootElement, divContainer, currentInputProps, args);
        return meunDiv;
    },

    /**
     * 多选列表点击事件
     * @param {*} e 事件Event
     * @param {*} meunDiv 多选下拉列表在的Div元素
     * @param {String} separatorChar 列表项目分割字符
     * @param {Object} currentInputProps 当前输入域属性对象
     * @param {*} rootElement 编辑器元素本身
     * @returns 
     */
    CheckMultiSelect: function (e, meunDiv, separatorChar, currentInputProps, rootElement) {
        var _target = e.srcElement ? e.srcElement : e.target ? e.target : e;
        if (_target != null && _target != meunDiv) {
            if (!_target.getAttribute || _target.getAttribute('class') != 'MultiSelect-item') {
                if (_target.getAttribute('class') == 'MultiSelect-line') {
                    return;
                } else if (_target.nodeName == 'INPUT') {
                    e.stopPropagation();
                    e.preventDefault();
                }
                //查找到MultiSelect-item元素
                while (_target) {
                    if (_target.getAttribute && _target.getAttribute('class') == 'MultiSelect-item') {
                        break;
                    }
                    _target = _target.parentElement;
                }
            }
            //在此处进行数据拼接
            if (_target != null && _target.getAttribute && _target.getAttribute('class') == 'MultiSelect-item') {
                //获取到元素并拼接
                var hasChecked = _target.getAttribute('targetLi');
                //如果存在选中则清除页面数据,没有就添加
                if (hasChecked != null) {
                    var iconEle = _target.querySelector('.MultiSelect-icon');
                    _target.removeAttribute('targetLi');
                    iconEle.innerHTML = WriterControl_ListBoxControl.NoCheckedSvg;
                } else {
                    var iconEle = _target.querySelector('.MultiSelect-icon');
                    _target.setAttribute('targetLi', 'true');
                    iconEle.innerHTML = WriterControl_ListBoxControl.CheckedSvg;
                }

                if (currentInputProps.RepulsionForGroup) {
                    // 判断所有的选中项
                    var allChecked = meunDiv.querySelectorAll(`[targetLi="true"]`);
                    if (allChecked && allChecked.length > 0) {
                        for (var check = 0; check < allChecked.length; check++) {
                            if (allChecked[check].getAttribute('dc_group') != _target.getAttribute('dc_group')) {
                                var iconEle = allChecked[check].querySelector('.MultiSelect-icon');
                                allChecked[check].removeAttribute('targetLi');
                                iconEle.innerHTML = WriterControl_ListBoxControl.NoCheckedSvg;
                            }
                        }
                    }
                }
                // 以下是赋值的代码
                //获取到所有的值
                var MultiSelectItems = meunDiv.querySelectorAll(".MultiSelect-item");
                var opt = {
                    includeText: "",
                    includeValue: "",
                    excludeText: "",
                    excludeValue: ""
                };
                // 修改顺序代码【暂时没有】
                for (var i = 0; i < MultiSelectItems.length; i++) {
                    if (MultiSelectItems[i] && MultiSelectItems[i].getAttribute('isuserdefineddomhidden') == "true") {
                        continue;
                    }
                    var textEle = MultiSelectItems[i].querySelector('.MultiSelect-text');//存储值的元素
                    var LsTxt = textEle.getAttribute("native-text");//Text值
                    var LsValue = textEle.getAttribute("value");//value值
                    if (MultiSelectItems[i].hasAttribute("targetLi")) {//选择的项
                        if (opt.includeText != "") {
                            opt.includeText += separatorChar;
                        }
                        opt.includeText += LsTxt;
                        if (opt.includeValue != "") {
                            opt.includeValue += separatorChar;
                        }
                        opt.includeValue += LsValue;
                    } else {//未选择的项
                        if (opt.excludeText != "") {
                            opt.excludeText += separatorChar;
                        }
                        opt.excludeText += LsTxt;
                        if (opt.excludeValue != "") {
                            opt.excludeValue += separatorChar;
                        }
                        opt.excludeValue += LsValue;
                    }
                }
                var FormatStrData = {
                    Text: opt.includeText,
                    Value: opt.includeValue
                };
                if (currentInputProps && currentInputProps.ListValueFormatString) {//存在格式化字符串
                    FormatStrData = WriterControl_ListBoxControl.GetListValueFormatString(currentInputProps, opt);
                }
                // rootElement.CurrentMultiSelect = rootElement.CurrentMultiSelect ? rootElement.CurrentMultiSelect : currentInput
                // 通过NativeHandle可以赋值
                //if ('ontouchstart' in rootElement.ownerDocument.documentElement) {
                //    rootElement.CloseDropdown = true;
                //}
                rootElement.SetElementProperties(currentInputProps.NativeHandle, {
                    Text: FormatStrData.Text,
                    InnerValue: FormatStrData.Value
                }, true);
                //rootElement.CloseDropdown = false;
                // rootElement.CurrentMultiSelect = rootElement.CurrentInputField();
                // 触发文档内容变化事件
                var opt = {
                    /** 触发事件类型 */
                    TriggerType: "ApplyCurrentEditorCallBack"
                };
                WriterControl_Event.RaiseControlEvent(rootElement, "DocumentContentChanged", opt);
                //if (!!callBack && typeof (callBack) == "function") {
                //    callBack.call(newText, newValue);
                //}
            }
        }
    },

    /**
     * 创建搜索框,并且展示下拉的方法
     * @param {*} listBox 下拉列表承载元素
     * @param {*} rootElement 编辑器元素本身
     * @param {*} divContainer 下拉元素本身
     * @param {Object} currentInputProps 当前输入域属性对象
     * @param {*} args 参数
     * @returns 
     */
    CreateDropdownCodeArea: function (listBox, rootElement, divContainer, currentInputProps, args) {
        if (listBox == null || rootElement == null || divContainer == null || currentInputProps == null || args == null) {
            return;
        }
        // MinCountForDropdownListSpellCodeArea: 显示下拉列表中拼音码区域的最小项目个数，默认值是4
        var hasMinCount = rootElement.DocumentOptions.BehaviorOptions.MinCountForDropdownListSpellCodeArea || 4;
        var listItems = listBox.querySelectorAll("[native-text]");
        // 判断是否存在下拉数量判断
        if (hasMinCount && hasMinCount <= listItems.length) {
            //在此之前先插入一个搜索框
            var searchSpan = rootElement.ownerDocument.createElement('span');
            searchSpan.setAttribute('class', 'dropdownCodeArea');
            searchSpan.style.cssText = `
                    position: relative;
                    border: 1px solid #95B8E7;
                    background-color: #fff;
                    vertical-align: middle;
                    display: inline-block;
                    overflow: hidden;
                    white-space: nowrap;
                    margin: 0;
                    padding: 0;
                    box-sizing: border-box;
                    width: 100%;
                    flex: none;`;
            var searchInput = rootElement.ownerDocument.createElement('input');
            //在此处可以设置搜索框的背景文字
            if (rootElement.EventGetSearchInputPlaceholder != null && typeof rootElement.EventGetSearchInputPlaceholder == 'function') {
                var placeholder = rootElement.EventGetSearchInputPlaceholder();
                searchInput.placeholder = placeholder || '';
            }
            searchInput.style.cssText = `
                    font-size: 12px;
                    border: 0;
                    margin: 0;
                    padding: 4px;
                    white-space: normal;
                    vertical-align: top;
                    outline-style: none;
                    resize: none;
                    padding-top: 0px;
                    padding-bottom: 0px;
                    height: 24px;
                    line-height: 24px;
                    box-sizing: border-box;
                    width: 100%;`;
            searchSpan.appendChild(searchInput);
            /** 设置搜索不到内容时，是否展示使用此文本按钮 */
            var IsShowUsingSearchContent = rootElement.getAttribute('DropdownListIsShowUsingSearchContent');
            IsShowUsingSearchContent = IsShowUsingSearchContent && (IsShowUsingSearchContent === 'true');
            var fillButton = null;
            if (IsShowUsingSearchContent) {
                //当没有搜索内容时，展示填充按钮
                fillButton = rootElement.ownerDocument.createElement('div');
                fillButton.innerHTML = '使用此文本';
                fillButton.title = '点击将输入域内容设置为搜索内容';
                fillButton.style.cssText = 'display:none;';//填充按钮默认不展示
                searchSpan.appendChild(fillButton);
                fillButton.onclick = function () {
                    //重新获取一下searchInput，解决在某些情况下获取不到searchInput的问题[DUWRITER5_0-3224]
                    searchInput = searchSpan.querySelector('input');
                    var changeValue = {
                        Text: '',
                        InnerValue: ''
                    };
                    if (currentInputProps.InnerMultiSelect) {
                        //再多选情况下，始终获取最新的输入域值
                        var currentElement = rootElement.GetElementProperties(rootElement.CurrentElement());
                        changeValue = {
                            Text: currentElement.Text ? currentElement.Text + ',' + searchInput.value : searchInput.value,
                            InnerValue: currentElement.InnerValue ? currentElement.InnerValue + ',' + searchInput.value : searchInput.value,
                        };
                        rootElement.SetElementProperties(currentInputProps, changeValue);
                    } else {
                        changeValue = {
                            Text: searchInput.value,
                            InnerValue: searchInput.value
                        };
                        rootElement.SetElementProperties(currentInputProps, changeValue);
                    }
                    // 触发文档内容变化事件
                    var opt = {
                        /** 触发事件类型 */
                        TriggerType: "ApplyCurrentEditorCallBack"
                    };
                    WriterControl_Event.RaiseControlEvent(rootElement, "DocumentContentChanged", opt);
                    //关闭下拉
                    var hasDropDown = rootElement.querySelector('#divDropdownContainer20230111');
                    hasDropDown.CloseDropdown('true');
                    return true;
                };
            }

            //让下拉框获取焦点
            searchInput.onfocus = function () {
                // 清除编辑器光标
                WriterControl_UI.HideCaret(rootElement);
            };
            searchInput.onkeydown = function (e) {
                WriterControl_ListBoxControl.ChangeListBoxCheck(rootElement, e, currentInputProps);
            };
            var eventObj = {
                Value: '',
                ElementID: currentInputProps.ID,
                ListSourceName: currentInputProps.InnerListSourceName,
                NewListItems: [],
                CurrentInputProps: currentInputProps,
                AddResultItemByTextValue: function (text1, value1) {
                    eventObj.NewListItems.push({
                        Text: text1,
                        Value: value1
                    });
                },
                //文本改变后重新显示的数据
                ChangeSpellCode: function () {
                    if (eventObj.NewListItems != null && eventObj.NewListItems.length > 0) {
                        listBox.innerHTML = '';
                        if (currentInputProps != null && (currentInputProps.InnerMultiSelect === true || currentInputProps.InnerMultiSelect == 'true')) {
                            for (var i = 0; i < eventObj.NewListItems.length; i++) {
                                var itemEle = rootElement.ownerDocument.createElement('div');
                                itemEle.setAttribute('class', 'MultiSelect-item');
                                itemEle.style.cssText = 'height: 30px;';
                                listBox.appendChild(itemEle);
                                var innerText = eventObj.NewListItems[i].Text ? eventObj.NewListItems[i].Text : '';;
                                var innerValue = eventObj.NewListItems[i].Value ? eventObj.NewListItems[i].Value : '';
                                itemEle.innerHTML = `
                                            <div class="MultiSelect-text" style="height: 30px; line-height: 30px;"  native-text="${innerText}" value="${innerValue}">${innerText}</div>
                                            <div class="MultiSelect-icon">${WriterControl_ListBoxControl.NoCheckedSvg}</div>
                                        `;
                            }
                        } else {
                            for (var i = 0; i < eventObj.NewListItems.length; i++) {
                                var liBox = rootElement.ownerDocument.createElement('div');
                                listBox.appendChild(liBox);
                                /*liBox.style.cssText = 'height: 30px;line-height: 30px;background-color: transparent;padding: 0 10px;overflow: hidden;white-space: nowrap;cursor: pointer;border-bottom: 1px solid #eee;font-size: 12px';*/
                                liBox.outerHTML = `<div class="dc_ListItem" native-text="${eventObj.NewListItems[i].Text ? eventObj.NewListItems[i].Text : ''}" value="${eventObj.NewListItems[i].Value}" style="padding: 0px 10px;">${eventObj.NewListItems[i].Text ? eventObj.NewListItems[i].Text : ''}</div>`;
                                // liBox.setAttribute('value', eventObj.NewListItems[i].Value ? eventObj.NewListItems[i].Value : '');

                            };
                        }
                        //如果存在此方法，不再需要搜索框使用此文本
                        searchInput.changeCode = true;
                        fillButton && (fillButton.style.cssText = 'display:none;');
                        if (!('ontouchstart' in rootElement.ownerDocument.documentElement)) {
                            if (containerDiv.scrollHeight > containerDiv.clientHeight) {
                                containerDiv.onwheel = function (e) {
                                    e.stopPropagation();
                                };
                            }
                            searchInput.focus();
                        }
                    }
                }
            };
            //输入内容改变的时候
            searchInput.oninput = function (e) {
                //在此处处理listBox
                if (e.target.value && e.target.value.length > 0) {
                    var allLi = null;
                    if (currentInputProps != null && (currentInputProps.InnerMultiSelect === true || currentInputProps.InnerMultiSelect == 'true')) {
                        allLi = listBox.querySelectorAll('.MultiSelect-item');
                    } else {
                        allLi = listBox.querySelectorAll('.dc_ListItem');
                    }

                    if (allLi != null && allLi.length > 0) {
                        var isHasSearch = false;//用于判断是否查找到结果
                        for (var i = 0; i < allLi.length; i++) {
                            if (allLi[i].innerText.indexOf(e.target.value) < 0) {
                                allLi[i].style.display = 'none';
                            } else {
                                isHasSearch = true;
                                allLi[i].style.display = '';
                            }
                        }
                    }

                    //未搜索的内容将填充按钮展示出来
                    if (isHasSearch === false) {
                        if (fillButton) {
                            fillButton.style.cssText = 'display:block;cursor:pointer;font-size:14px;color: #606266;border: 1px solid;padding: 4px 0;';
                            // containerDiv.style.maxHeight = 'calc(250px - 45px)';
                        }

                    } else {
                        if (fillButton) {
                            fillButton.style.cssText = 'display:none;';
                            // containerDiv.style.maxHeight = 'calc(250px - 26px)';
                        }

                    }
                } else {
                    // 展示所有下拉列表时，不展示填充按钮
                    if (fillButton) {
                        fillButton.style.cssText = 'display:none;';
                        // containerDiv.style.maxHeight = 'calc(250px - 26px)';
                    }
                    // 修复下拉框中搜索框内容删除为空时不展示全部下拉项的问题【DUWRITER5_0-2646】
                    var allLi = null;
                    if (currentInputProps != null && (currentInputProps.InnerMultiSelect === true || currentInputProps.InnerMultiSelect == 'true')) {
                        allLi = listBox.querySelectorAll('.MultiSelect-item');
                    } else {
                        allLi = listBox.querySelectorAll('.dc_ListItem');
                    }
                    if (allLi != null && allLi.length > 0) {
                        //将数据全部显示
                        for (var i = 0; i < allLi.length; i++) {
                            allLi[i].style.display = '';
                        }
                    }
                }
                eventObj.NewListItems = [];
                //在此处不调用callback的话将数据再次写入元素
                if (rootElement.EventChangeSearchInputSpellCode != null && typeof rootElement.EventChangeSearchInputSpellCode == 'function') {
                    eventObj.Value = e.target.value;
                    rootElement.EventChangeSearchInputSpellCode(eventObj);
                }


            };
            divContainer.appendChild(searchSpan);
            divContainer.style.removeProperty('border-radius');
            //在此处创建一个包裹元素来先显示滚动条
            var containerDiv = rootElement.ownerDocument.createElement('div');
            containerDiv.appendChild(listBox);
            containerDiv.setAttribute('class', 'listBoxContainer');
            containerDiv.style.cssText += "position: relative;overflow-y: auto;overscroll-behavior: contain;height:100%;flex:1;";

            divContainer.appendChild(containerDiv);
            // divContainer.style.width = 'auto';
            divContainer.ShowDropdown();
            if (!('ontouchstart' in rootElement.ownerDocument.documentElement)) {
                //防止下拉存在滚动条时，鼠标滚轮事件不生效
                // containerDiv.addEventListener('wheel', function (e) {
                //     e.stopPropagation(); // 停止事件传播
                //     return true; // 继续执行默认事件
                // });
                searchInput.focus();
            }

            // 判断宽度
            // var liWidth = divContainer.offsetWidth;
            // if (Number(liWidth) > 0) {
            //     searchInput.style.width = (liWidth - 2) + 'px';
            // }
            var hasTargetLi = listBox.querySelectorAll('[targetLi]');
            if (hasTargetLi != null && hasTargetLi.length > 0) {
                hasTargetLi = hasTargetLi[hasTargetLi.length - 1];
                containerDiv.scrollTo(0, hasTargetLi.offsetTop);
            }

        } else {
            divContainer.appendChild(listBox);
            // divContainer.style.width = 'auto';
            divContainer.ShowDropdown();
            var hasTargetLi = listBox.querySelectorAll('[targetLi]');
            if (hasTargetLi != null && hasTargetLi.length > 0) {
                hasTargetLi = hasTargetLi[hasTargetLi.length - 1];
                divContainer.scrollTo(0, hasTargetLi.offsetTop);
            }
        }
        // 修复下拉弹框没有搜索框时两栏展示标题未固定在下拉弹框顶部的问题
        // 把两栏展示的标题提出
        var first_listitem = listBox.firstChild;
        if (first_listitem && first_listitem.getAttribute("TwoColumnDisplay") == "true" && first_listitem.className == "dc_titleDiv") {
            var parentNode = listBox.parentNode;
            parentNode.insertBefore(first_listitem, listBox);
            // 给标题添加边框
            first_listitem.style.border = "1px solid #000";
            // 给包裹元素添加样式
            parentNode.style.display = "flex";
            parentNode.style.flexDirection = "column";
            // parentNode.style.cssText = "display:flex;flex-direction: column;";
            // 给列表盒子添加滚动条
            listBox.style.overflowY = "auto";
        }
        //用户自定义下拉框最大高度：
        let DropDownListMaxHeight = rootElement.getAttribute("DropDownListMaxHeight") || null;
        DropDownListMaxHeight = DropDownListMaxHeight ? parseInt(DropDownListMaxHeight) : null;
        if (!DropDownListMaxHeight && listItems.length > 6) {
            //当没有设置固定高度时：
            //DUWRITER5_0-2970 20240702 lxy 重新计算下拉框的高度，修改下拉输入域的高度防止文字展示一半：
            var divContainerHeight = 0;
            var MultiSelectControl = divContainer.querySelector('#MultiSelectControl');
            var dcListBox = divContainer.querySelector('.dcListBox');
            var selectChildren = [];//下拉选项
            var dc_dropdown_box = null;//选择项父级盒子
            if (MultiSelectControl && MultiSelectControl.children) {
                //多选
                selectChildren = MultiSelectControl.children;
                dc_dropdown_box = MultiSelectControl;
            } else if (dcListBox && dcListBox.children) {
                //单选
                selectChildren = dcListBox.children;
                dc_dropdown_box = dcListBox;
            }

            var SelectItemNum = 0;
            //多选的高度，定义一次展示6个子元素，包括分组横线的高度
            for (var i = 0; i < selectChildren.length; i++) {
                var item = selectChildren[i];
                if (SelectItemNum < 6) {
                    //分组横线
                    if (item.nodeName === 'SPAN') {
                        divContainerHeight += 2;
                    }
                    //每一个下拉
                    if (item.nodeName === 'DIV') {
                        SelectItemNum += 1;//用于计数
                    }
                    divContainerHeight += item.offsetHeight;
                }
            }

            //设置父级样式
            if (dc_dropdown_box.parentElement.className.indexOf('listBoxContainer') != -1) {
                //判断是否增加双列展示时的表头高度
                var dc_titleDiv = divContainer.querySelector('.dc_titleDiv');
                if (dc_titleDiv) {
                    divContainerHeight += dc_titleDiv.offsetHeight;
                }
            }
            //判断是否需要增加搜索框的高度
            var searchInput = divContainer.querySelector('.dropdownCodeArea');
            if (searchInput) {
                divContainerHeight += searchInput.offsetHeight;
            }

            //判断是否需要增加"使用此文本"的高度
            if (fillButton) {
                divContainerHeight += fillButton.offsetHeight;
            }


            //[DUWRITER5_0-3273]获取输入域是否是从上方弹出的，输入域从上方弹出，则需要重新计算一下高度，防止弹出位置过高
            var dc_up = divContainer.getAttribute("dc_up");
            if (dc_up === 'true') {
                //先保留一下原本高度，用于解决在下拉框展示在输入域顶部时，下拉高度过高问题
                var divContainerOldHeight = divContainer.style.maxHeight || divContainer.style.height;
                divContainerOldHeight = parseInt(divContainerOldHeight) || 0;
                // 获取原有的top值
                var divContainerOldTop = divContainer.style.top || 0;
                divContainerOldTop = parseInt(divContainerOldTop) || 0;
                //设置新的高度
                if (divContainerOldHeight > divContainerHeight) {
                    divContainer.style.top = (divContainerOldTop + (divContainerOldHeight - divContainerHeight)) + 'px';
                }
            }

            //设置整个容器高度
            divContainer.style.maxHeight = divContainerHeight + 'px';
            divContainer.style.overflowY = 'auto';
        }


    },

    /**
     * 下拉输入域选中触发事件
     * @param {*} rootElement 编辑器元素本身
     * @param {*} e 事件Event
     * @param {Object} currentInputProps 当前输入域属性对象
     * @returns 
     */
    ChangeListBoxCheck: function (rootElement, e, currentInputProps) {
        // 判断是否存在下拉输入域是否显示
        var hasDropDown = rootElement.querySelector("#divDropdownContainer20230111");
        //var txtEdit = rootElement.querySelector("#txtEdit20221213");
        if (hasDropDown == null) {
            return false;
        }
        if (hasDropDown.style.display != "none" || hasDropDown.thisApi != null) {
            // 下拉框显示或者时间选择框展示
            // Esc 退出
            if (e.keyCode == 27) {
                hasDropDown.CloseDropdown("true");
                return true;
            }
        }
        if (hasDropDown.style.display != "none") {
            // 下拉框显示时
            //if (!('ontouchstart' in document.documentElement)) {
            //    var isPassword = txtEdit.getAttribute('type');
            //    if (isPassword != 'password') {
            //        txtEdit.setAttribute('type', 'password');
            //    }
            //}
            // 判断是否为下拉输入域
            var hasListBox = hasDropDown.querySelector('.dcListBox,#MultiSelectControl');
            if (!hasListBox) {
                // 不是下拉输入域
                return;
            }

            //判断是否为tab键
            if (e.keyCode == 9) {
                //获取到输入域
                var inputEle = rootElement.CurrentInputField();
                if (inputEle) {
                    var inputAttr = rootElement.GetElementProperties(inputEle);
                    if (inputAttr && inputAttr.MoveFocusHotKey == "Tab") {
                        //判断是否存在跳转
                        hasDropDown.CloseDropdown("true");
                        //跳转到下一个输入域
                        rootElement.FocusNextInput();
                    }
                }
                e.stopPropagation();
                e.preventDefault();
                return true;
            }

            /** 是否是多选下拉 */
            var isMultiSelect = false;
            /** 是否是快捷辅助录入下拉列表 */
            var isAssistListBox = false;
            if (hasListBox.id == "MultiSelectControl") {
                // 是多选下拉
                isMultiSelect = true;
            } else if (hasListBox.getAttribute("box-type") == "AssistListBox") {
                // 是快捷辅助录入下拉
                isAssistListBox = true;
            }
            // 最先判读是否存在moveli,moveli表示是当前选中的内容
            var hasTargetLi = hasListBox.querySelectorAll("[moveli]");
            if (hasTargetLi && hasTargetLi.length == 0) {
                // 查找下拉元素内部是否存在targetli属性
                hasTargetLi = hasListBox.querySelectorAll("[targetli]:not([isuserdefineddomhidden])");
            }
            /** 上下移动的元素列表数组 */
            var moveLis = [];
            /** 现在需要选中的列表项 */
            var NowMoveLi;
            /** 当前选中的元素在sz列表中的索引 */
            var NextLiIndex = 0;
            for (var i = 0; i < hasListBox.children.length; i++) {
                var item = hasListBox.children[i];
                // 过滤元素
                if (item.getAttribute("dcignore") != "1") {
                    moveLis.push(item);
                }
                if (hasTargetLi && hasTargetLi.length > 0) {
                    if (item == hasTargetLi[hasTargetLi.length - 1]) {
                        NextLiIndex = moveLis.length - 1;
                    }
                }
            }
            // 处理上下选中项的情况
            if (e.keyCode == 38 || e.keyCode == 40) {
                if (hasTargetLi && hasTargetLi.length > 0) {
                    // 存在选中元素
                    if (e.keyCode == 38) {
                        // 方向键--上
                        NextLiIndex--;
                        if (NextLiIndex < 0) {
                            NextLiIndex = 0;
                        }
                    } else if (e.keyCode == 40) {
                        // 方向键--下
                        NextLiIndex++;
                        if (NextLiIndex >= moveLis.length) {
                            NextLiIndex = moveLis.length - 1;
                        }
                    }
                }
                NowMoveLi = moveLis[NextLiIndex];
            }

            if (NowMoveLi) {
                // 设置选中样式
                for (var i = 0; i < moveLis.length; i++) {
                    if (moveLis[i] == NowMoveLi) {
                        moveLis[i].setAttribute("moveli", "true");
                    } else {
                        moveLis[i].removeAttribute("moveli");
                    }
                }
                // 处理是否需要滚动
                function isElementInView(element, scrollElement) {
                    var rect = element.getBoundingClientRect();
                    var parentRect = scrollElement.getBoundingClientRect();
                    // 检查元素是否在父元素的边界内
                    return (
                        rect.top >= parentRect.top &&
                        rect.left >= parentRect.left &&
                        rect.bottom <= parentRect.bottom &&
                        rect.right <= parentRect.right
                    );
                }
                if (isElementInView(NowMoveLi, hasDropDown) == false) {
                    // 选中的列表项不在视图中，需要滚动
                    // 获取可以滚动的元素
                    var scrollElement = NowMoveLi;
                    while (scrollElement.scrollHeight <= (scrollElement.innerHeight || scrollElement.clientHeight) && scrollElement != hasDropDown) {
                        scrollElement = scrollElement.parentNode;
                    }
                    scrollElement.scrollTop = NowMoveLi.offsetTop;
                }
                // 修复进入快捷辅助录入下拉列表时上下方向键无法选中下拉项的问题
                return true;
            }
            // 获取属性列表
            currentInputProps = currentInputProps ? currentInputProps : rootElement.GetElementProperties(rootElement.CurrentInputField());
            /** 是否启动快速辅助录入模式 */
            var FastInputMode = false;
            if (rootElement && rootElement.DocumentOptions && rootElement.DocumentOptions.BehaviorOptions) {
                FastInputMode = rootElement.DocumentOptions.BehaviorOptions.FastInputMode;
            }
            /** 是否移动光标 */
            var needChangeFocus = false;
            var divCaret = rootElement.querySelector('#divCaret20221213');
            var isShowCaret = true;
            if (!divCaret || divCaret.style.display == "none") {
                isShowCaret = false;
            }
            /** 当前光标的输入域 */
            var currentInputField = rootElement.CurrentInputField();
            if (FastInputMode === true && isShowCaret == false) {
                // 快速辅助录入模式
                if (e.shiftKey == false && e.ctrlKey == false && e.altKey == false) {
                    if (e.keyCode == 13 || e.keyCode == 9) {
                        // 获取输入域
                        var thisAttr = rootElement.GetElementProperties(currentInputField);
                        if (thisAttr) {
                            needChangeFocus = (e.keyCode == 13 && thisAttr.MoveFocusHotKey == "Enter") || (e.keyCode == 9 && thisAttr.MoveFocusHotKey == "Tab");
                        }
                    }
                };
            }

            // Enter
            if (e.keyCode == 13) {
                NowMoveLi = moveLis[NextLiIndex];
                if (isMultiSelect == false) {
                    // 单选下拉列表
                    // 兼容四代接口EventAfterEnter
                    if (rootElement.EventAfterEnter != null && typeof (rootElement.EventAfterEnter) == "function") {
                        e.preventDefault();
                        return rootElement.EventAfterEnter(NowMoveLi);
                    }

                }
                if (NowMoveLi && typeof (NowMoveLi.click) == "function") {
                    // 模拟点击事件
                    NowMoveLi.click();
                }
                if (NowMoveLi == null) {
                    // 当前未选中列表项
                    // 判断是否有触发焦点快捷键，移动光标到下一个输入域
                    if (needChangeFocus) {
                        var nextInput = rootElement.GetNextFocusFieldElement(currentInputField);
                        if (nextInput) {
                            rootElement.FocusElement(nextInput);
                        }
                        e.returnValue = false;
                    } else {
                        hasDropDown.CloseDropdown();
                        var content = rootElement.CurrentElement("xtextcontainerelement");
                        rootElement.FocusElement(content);
                    }
                }
                e.stopPropagation();
                e.preventDefault();
                return true;
            }
            //tab
            if (e.keyCode == 9) {
                //判断是否有触发焦点快捷键，移动光标到下一个输入域
                if (needChangeFocus) {
                    var nextInput = rootElement.GetNextFocusFieldElement(currentInputField);
                    if (nextInput) {
                        rootElement.FocusElement(nextInput);
                    }
                    e.returnValue = false;
                }
            }
            // 兼容EventAfterSpace
            // Space 空格
            if (e.keyCode == 32) {
                if (rootElement.EventAfterSpace != null && typeof (rootElement.EventAfterSpace) == "function") {
                    rootElement.EventAfterSpace();
                }
            }
            if (isAssistListBox) {
                // 快捷辅助录入下拉列表,进行正常删除
                // 修复进入快捷辅助录入下拉列表时无法进行正常输入删除的问题【DUWRITER5_0-3076】
                return false;
            }
            return true;
        }
        return false;
    },
};