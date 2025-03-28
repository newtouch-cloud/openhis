"use strict";
/**二进制数据读取器 */
export class DCBinaryReader {
    /**
     * 初始化对象
     * @param { Uint8Array} bsBuffer
     * @param {number} startPosition
     */
    constructor(bsBuffer, startPosition) {
        this.TypeName = "DCBinaryReader";
        this.DataBuffer = bsBuffer;
        this.View = new DataView(bsBuffer.buffer);
        if (typeof startPosition == "number") {
            this.Position = startPosition;
        }
        else {
            this.Position = 0;
        }
        this._DataLength = bsBuffer.length;
    }
    ReadBoolean() {
        var v = this.DataBuffer[this.Position++];
        return v == 1;
    }
    ReadByte() {
        return this.DataBuffer[this.Position++];
    }
    ReadInt16() {
        var v = this.View.getInt16(this.Position);
        this.Position += 2;
        return v;
    }
    ReadInt32() {
        var v = this.View.getInt32(this.Position);
        this.Position += 4;
        return v;
    }
    ReadSingle() {
        var v = this.View.getFloat32(this.Position);
        this.Position += 4;
        return v;
    }
    ReadByteArray() {
        if (this.Position >= this.View.byteLength - 4) {
            console.log("out range$$$");
            return null;
        }
        var len = this.View.getInt32(this.Position);
        this.Position += 4;
        if (len == 0) {
            return null;
        }
        else if (len < 0) {
            throw "错误的长度:" + len;
        }
        else {
            var temp = this.DataBuffer.slice(this.Position, this.Position + len);
            this.Position += len;
            return temp;
        }
    }
    ReadString() {
        var len = this.View.getInt32(this.Position);
        this.Position += 4;
        if (len == 0) {
            return null;
        }
        else if (len < 0) {
            throw "错误的长度:" + len;
        }
        else {
            var temp = this.DataBuffer.slice(this.Position, this.Position + len);
            this.Position += len;
            var decoder = new TextDecoder();
            return decoder.decode(temp);
        }
    }
    ReadChar() {
        var code = this.View.getInt32(this.Position);
        this.Position += 4;
        return String.fromCharCode(code);
    }
    get EOF() {
        return this.Position >= this._DataLength;
    }
};

try {
    //var sc = document.currentScript;

    if (import.meta) {
        var strUrl = import.meta.url;
        if (strUrl != null && strUrl.length > 0) {
            if (strUrl.indexOf("DCTools20221228.js") >= 0) {
                window.__DCResourceBasePath = strUrl.replace("DCTools20221228.js", "{0}");
            }
        }
    }
}
catch (ext) {

}

/** 通用操作模块 */
export let DCTools20221228 = {
    //SupportNodeNames: ["p", "span", "table", "tr", "td","col","colgroup", "input","tbody",
    //    "textarea", "select", "div","b","sub","sup","video","image","button",
    //    "title", "hr", "u", "i", "li", "h1", "h2", "h3", "h4", "h5", "font"],
    // 支持的样式名称,这个数值必须和DCSoft.WASM.WASMDocumentBuilder.CSSStyleIndexs枚举类型保持
    // 数量和顺序的完全一致
    SupportStyleNames:
        ["visibility", "position", "padding-top", "padding-right",
            "padding-left", "padding-bottom", "list-style-type", "line-height",
            "font-weight", "font-style", "font-size", "font-family", "display",
            "color", "border-top-width", "border-top-style", "border-top-color",
            "border-right-width", "border-right-style", "border-right-color",
            "border-left-width", "border-left-style", "border-left-color",
            "background-color",
            "border-bottom-width", "border-bottom-style", "border-bottom-color",
            "border-style", "border-width", "border-color", "border", "vertical-align", "width"
        ],
    StringConvert: function (Value) {
        var resultStr = "";
        if (Value && Value.length) {
            for (var i = 0; i < Value.length; i++) {
                var asciicode = Value.charAt(i).charCodeAt(); //循环转为ASCII
                if (asciicode == 160) {
                    asciicode = 32; //空格160转为32
                }
                resultStr = resultStr + String.fromCharCode(asciicode);
            }
            return resultStr;
        }
        return Value;
    },
    strToDate: function (str, format) {
        var isNum = isNaN(parseInt(str)) === false;
        if (str.length === 4 && isNum === true) {
            var str1 = str.substring(0, 2);
            var str2 = str.substring(2, 4);
            if (format === "HH:mm") {
                str = str1 + ":" + str2;
            } else if (format === "MM-dd") {
                str = str1 + "-" + str2;
            }
        } else if (str.length === 6 && isNum === true) {
            if (format === "yyyy-MM") {
                var str1 = str.substring(0, 4);
                var str2 = str.substring(4, 6);
                str = str1 + "-" + str2;
            } else if (format === "HH:mm:ss") {
                var str1 = str.substring(0, 2);
                var str2 = str.substring(2, 4);
                var str3 = str.substring(4, 6);
                str = str1 + ":" + str2 + ":" + str3;
            }
        } else if (str.length === 8 && isNum === true && format === "yyyy-MM-dd") {
            var str1 = str.substring(0, 4);
            var str2 = str.substring(4, 6);
            var str3 = str.substring(6, 8);
            str = str1 + "-" + str2 + "-" + str3;
        } else if (str.length === 14 && isNum === true && format === "yyyy-MM-dd HH:mm:ss") {
            var str1 = str.substring(0, 4);
            var str2 = str.substring(4, 6);
            var str3 = str.substring(6, 8);
            var str4 = str.substring(8, 10);
            var str5 = str.substring(10, 12);
            var str6 = str.substring(12, 14);
            str = str1 + "-" + str2 + "-" + str3 + " " + str4 + ":" + str5 + ":" + str6;
        }
        if (str.slice(0, 5) == "02/29" || str.slice(0, 5) == "02-29") {
            // 遇到2月29号，是闰年，需要特殊处理
            str = "2000/02/29" + str.slice(5);
        }
        // 20220810 xym 修复日期问题，当前字符串可以转为时间格式时直接转【ValueFormater.strToDate】
        var str_date = new Date(str);
        if (str_date != "Invalid Date") {
            return str_date;
        }
        if (str.indexOf("/") == -1) {//只有时间
            str = new Date().toLocaleDateString() + "T" + str;//20200803 修复时间输入域输出格式无效问题
        }
        str = str.replace("T", " ");//处理yyyy-MM-ddThh:mm:ss的情况
        str = str.replace(/\s/, " ");//处理yyyy-MM-ddThh:mm:ss的情况
        var tempStrs = str.split(" "); //空格分隔
        var dateStrs = tempStrs[0].split("/"); //年月日分组
        var year = parseInt(dateStrs[0], 10);
        var month = parseInt(dateStrs[1], 10) - 1; //月份需要减一
        var day = parseInt(dateStrs[2], 10);

        var hour = "0"; //数组长度为1，缺少时分秒
        var minute = "0";
        var second = "0";

        if (tempStrs.length > 1) {
            var timeStrs = tempStrs[1].split(":"); //时分秒分组
            hour = parseInt(timeStrs[0], 10);

            if (timeStrs.length >= 2) {
                minute = parseInt(timeStrs[1], 10);
            }
            //xym 20201228 修复BS版本时间控件“yyyy年MM月dd日 HH时mm分ss秒”格式，选择日期带秒，光标离开输入域秒为00的问题
            if (timeStrs.length >= 3 && timeStrs[2]) {
                second = parseInt(timeStrs[2], 10);
            }
        }

        if (isNaN(year) == true
            || isNaN(month) == true
            || isNaN(day) == true
            || isNaN(hour) == true
            || isNaN(minute) == true
            || isNaN(second) == true) {
            return null; // 不能转换为时间
        }
        var date = new Date(year, month, day, hour, minute, second);
        return date;
    },
    HtmlToJson: function (strHtml) {
        if (strHtml == null || strHtml.length == 0) {
            return null;
        }
        var rootDiv = document.createElement("iframe");
        rootDiv.style.width = "1px";
        rootDiv.style.height = "1px";
        rootDiv.style.position = "absolute";
        document.body.appendChild(rootDiv);
        rootDiv.contentDocument.write(strHtml);
        rootDiv.contentDocument.close();
        var styleIndexs = new Array();
        var rootResult = { childNodes: [], Styles: [] };
        function CompressTextWhitespace(strText) {
            var len2 = strText.length;
            for (var iCount = 0; iCount < len2; iCount++) {
                var c = strText.charCodeAt(iCount);
                if (c == 32 || c == 10 || c == 13 || c == 8) {
                    // 遇到空白字符，则进行压缩
                    var strResultText = "";
                    var lastISWhite = false;
                    for (iCount = 0; iCount < len2; iCount++) {
                        var c = strText.charCodeAt(iCount);
                        if (c == 32 || c == 10 || c == 13 || c == 8) {
                            // 空白字符
                            if (lastISWhite == true) {
                                continue;
                            }
                            else {
                                lastISWhite = true;
                                strResultText += " ";
                            }
                        }
                        else {
                            lastISWhite = false;
                            strResultText += strText.charAt(iCount);
                        }
                    }
                    return strResultText;
                }
            }
            return strText;
        }
        function BuildJson(htmlElement, resultList) {
            for (var node = htmlElement.firstChild; node != null; node = node.nextSibling) {
                var nodeType = node.nodeType;
                if (nodeType == 3) {
                    // 纯文本节点
                    var strText = node.nodeValue;
                    if (strText == null || strText.length == 0) {
                        continue;
                    }
                    if (strText.trim().length == 0) {
                        // 完全空白的文本
                        var nextNode = node.nextSibling;
                        if (nextNode != null && nextNode.nodeType == 1) {
                            // 夹杂在XML元素中间的纯文本节点，则忽略掉。
                            continue;
                        }
                        var preNode = node.previousSibling;
                        if (preNode != null && preNode.nodeType == 1) {
                            // 夹杂在XML元素中间的纯文本节点，则忽略掉。
                            continue;
                        }
                    }
                    strText = CompressTextWhitespace(strText);
                    if (strText.length > 0) {
                        resultList.push({ nodeName: "#text", Value: strText });
                    }
                }
                else if (nodeType == 1
                    && node.getAttribute) {
                    // HTML元素
                    var strNodeName = node.nodeName.toLowerCase();
                    if (strNodeName == "pre") {
                        resultList.push({ nodeName: "pre", Value: node.nodeValue });
                        continue;
                    }
                    var jsonElement = new Object();
                    jsonElement.nodeName = strNodeName;
                    var strID = node.id;
                    if (strID != null && strID.length > 0) {
                        if (strID.length > 4 && strID.substr(0, 4) == "$dc$") {

                        }
                        else {
                            jsonElement.id = strID;
                        }
                    }
                    var strStyleKey = node.className + " " + node.getAttribute("style");
                    strStyleKey = strStyleKey.trim();
                    if (strStyleKey.length > 0 && strStyleKey != "null") {
                        // 指明样式
                        var si = styleIndexs[strStyleKey];
                        if (typeof (si) != "number") {
                            // 计算样式编号
                            var rsObj = new Object();
                            var addCount = 0;
                            if (node.classList && node.classList.length > 0) {
                                var sheets = node.ownerDocument.styleSheets;
                                for (var iCount = 0; iCount < node.classList.length; iCount++) {
                                    var item3 = "." + node.classList[iCount];
                                    for (var sheet of sheets) {
                                        for (var rule of sheet.cssRules) {
                                            if (rule.selectorText == item3) {

                                            }
                                        }
                                    }
                                }
                            }
                            if (node.attributeStyleMap) {
                                var ars = node.attributeStyleMap;
                                if (ars != null && ars.size > 0) {
                                    ars.forEach(function (item9, index9) {
                                        if (DCTools20221228.SupportStyleNames.indexOf(index9) >= 0 && item9.length > 0) {
                                            rsObj[index9] = item9[0].toString();
                                            addCount++;
                                        }
                                    });
                                }
                            }
                            //wyc20241022:火狐不支持attributeStyleMap得自己拆分
                            else {
                                var stylestr = node.getAttribute("style");
                                if (stylestr != null && stylestr.split) {
                                    var groups = stylestr.split(';');
                                    for (var i = 0; i < groups.length; i++) {
                                        var stylename = groups[i].split(':')[0];
                                        var stylevalue = groups[i].split(':')[1];
                                        if (DCTools20221228.SupportStyleNames.indexOf(stylename) >= 0) {
                                            rsObj[stylename] = stylevalue.toString();
                                            addCount++;
                                        }
                                    }
                                }
                            }
                            if (addCount > 0) {
                                si = rootResult.Styles.length;
                                rootResult.Styles.push(rsObj);
                            }
                            else {
                                si = -1;
                            }
                            styleIndexs[strStyleKey] = si;
                        }
                        if (si >= 0) {
                            jsonElement.StyleIndex = si;
                        }
                    }

                    if (strNodeName == "textarea" || strNodeName == "button") {
                        jsonElement.Value = node.innerText;
                    }
                    else {
                        //var strDCType = node.getAttribute("dctype");
                        //if (strDCType != null && strDCType.length > 0) {
                        //    jsonElement.nodeName = strDCType;
                        //}
                        if (node.firstChild != null) {
                            if (node.firstChild == node.lastChild && node.firstChild.nodeType == 3) {
                                // 只有一个纯文本节点,压缩存储
                                var strText4 = CompressTextWhitespace(node.firstChild.nodeValue);
                                if (strText4 != null && strText4.length > 0) {
                                    jsonElement.TextContent = strText4;
                                }
                            }
                            else {
                                //if (strNodeName == "table") {
                                //    var s = 2;
                                //}
                                jsonElement.childNodes = new Array();
                                BuildJson(node, jsonElement.childNodes);
                                if (jsonElement.childNodes.length == 0) {
                                    delete jsonElement.childNodes;
                                }
                            }
                        }
                        var attributes = node.attributes;
                        if (attributes != null && attributes.length > 0) {
                            var attrLen = attributes.length;
                            for (var iCount = 0; iCount < attrLen; iCount++) {
                                var attr = attributes[iCount];
                                var attrName = attr.localName.toLowerCase();
                                if (attrName != "nodeName"
                                    && attrName != "StyleIndex"
                                    && attrName != "childNodes"
                                    && attrName != "class"
                                    && attrName != "style"
                                    && attrName != "id"
                                    && attrName != "cellid") {
                                    jsonElement[attrName] = attr.nodeValue;
                                }
                            }
                        }
                    }
                    if (strNodeName == "image"
                        || strNodeName == "col"
                        || strNodeName == "input"
                        || strNodeName == "button"
                        || strNodeName == "select") {
                        //wyc20241018:改用style.width
                        //if (node.scrollWidth > 0) {
                        //    jsonElement.width = node.scrollWidth;
                        //    jsonElement.height = node.scrollHeight;
                        //}
                        //else {
                        //    jsonElement.width = node.style.width;//offsetWidth;
                        //    jsonElement.height = node.style.height;// offsetHeight;
                        //}
                        if (node.style.width > 0) {
                            jsonElement.width = node.style.width;//offsetWidth;
                            jsonElement.height = node.style.height;// offsetHeight;
                        }
                        else {
                            jsonElement.width = node.offsetWidth;
                            jsonElement.height = node.offsetHeight;
                        }
                    }
                    resultList.push(jsonElement);
                }
            }
        };
        BuildJson(rootDiv.contentDocument.body, rootResult.childNodes);
        rootDiv.remove();
        var strJson = JSON.stringify(rootResult);
        return strJson;
    },

    /**
     * 字符串资源值
     * @param {string} strName 字符串资源名称
     * @returns {string} 字符串资源值
     */
    GetStringResourceValue: function (strName) {
        if (window.__DCSR != null) {
            var v = window.__DCSR[strName];
            return v;
        }
        return null;
    },
    /**
     * 获得图片的大小
     * @param {uint8Aarry} bsData 图片数据
     * @returns {string} 图片大小
     */
    GetImageSize: function (vIndex, bsData) {
        if (bsData == null || bsData.length == 0) {
            return null;
        }
        createImageBitmap(new Blob([bsData])).then(function (img) {
            if (img != null) {
                DotNet.invokeMethod(
                    window.DCWriterEntryPointAssemblyName,
                    "SetBmpSize",
                    vIndex,
                    img.width,
                    img.height);
                img.close();
            }
        }).catch(function (err) {
            DotNet.invokeMethod(
                window.DCWriterEntryPointAssemblyName,
                "SetBmpSize",
                vIndex,
                0,
                0);
            console.warn(err);
        });
    },
    /**
     * 将HTML元素转换为一个PNG图片元素,图片大小就是HTML元素的大小
     * @param {HTMLElement} domElement HTML元素
     * @param {Function} callBack 获得图片后的回调函数
     */
    ConvertHtmlToPNGImage: function (domElement, callBack) {
    },
    /**
     * 判断是否具有ZIP文件头
     * @param {string | ArrayBuffer} data
     * @returns { boolean} 是否具有ZIP文件头
     */
    HasZIPFileHeader: function (data) {
        if (data == null || data.length < 8) {
            return false;
        }
        if (typeof (data) == "string") {
            if (data.substring(0, 8) == "UEsDBBQA") {
                // 认为字符串是BASE64格式
                return true;
            }
        }
        else {
            if (data[0] == 0x50 && data[1] == 0x4b && data[2] == 0x3 && data[3] == 0x4) {
                return true;
            }
        }
        return false;
    },
    /**
     * 包装大的字符串值
     * @param {String} strValue
     * @returns {String} 包装后的字符串值
     */
    PackageBigStringValue: function (strValue) {
        window.__DCWriter_BigString = strValue;
        return "$bs20240327";
    },
    /**
     * 解包大字符串值
     * @param {String} strValue 原始字符串值
     * @returns {String} 解包后的字符串值
     */
    UnPackageStringValue: function (strValue) {
        if (strValue == "$bs20240327") {
            var v = window.__DCWriter_BigString;
            window.__DCWriter_BigString = null;
            return v;
        }
        return strValue;
    },
    /**
     * 获得资源文件的下载地址
     * @param {string} strName 资源名称
     * @returns {string} 下载地址
     */
    GetResourceUrl: function (strName) {
        if (strName == null || strName.length == 0) {
            return null;
        }
        strName = DCTools20221228.StringReplaceAll("\\", "/", strName);
        // strName = strName.replaceAll("\\", "/");
        var strResourceBasePath = window.__DCResourceBasePath;
        if (strResourceBasePath == null || strResourceBasePath.length == 0) {
            strResourceBasePath = "./framework/";
        }
        var strRuntimeUrl = null;
        if (strResourceBasePath.indexOf("{0}") > 0) {
            strRuntimeUrl = strResourceBasePath.replace("{0}", strName);
        }
        else {
            strRuntimeUrl = strResourceBasePath + strName;
        }
        return strRuntimeUrl;
    },
    /**
     * 将对象解析为一个布尔值
     * @param {string} strValue 字符串
     * @param {boolean} bolDefaultValue 默认值
     * @returns {boolean} 解析结果
     */
    ParseBoolean: function (strValue, bolDefaultValue) {
        if (typeof (strValue) == "boolean") {
            return strValue;
        }
        if (typeof (strValue) == "string") {
            strValue = strValue.trim().toLowerCase();
            if (strValue == "true" || strValue == "yes") {
                return true;
            }
            else if (strValue == "false" || strValue == "no") {
                return false;
            }
        }
        if (typeof (bolDefaultValue) == "boolean") {
            return bolDefaultValue;
        }
        else {
            return false;
        }
    },
    /**
     * 创建透明图片
     * @param {Uint8Array} bsData 原始图片数据
     * @param {Number} colorR 透明色的R值
     * @param {Number} colorG 透明色的G值
     * @param {Number} colorB 透明色的B值
     * @returns { Uint8Array} 创建好的透明图片数据
     */
    MakeImageTransparent: function (bsData, colorR, colorG, colorB) {
        var img = new Image();
        img.src = bsData;
        //如果直接给图片元素可以跳过这步加载操作执行同步代码
        img.onload = function () {
            // 创建一个画布并设置大小与图片相同
            var canvas = document.createElement("canvas");
            canvas.width = img.width;
            canvas.height = img.height;
            // 获取2D渲染上下文
            var ctx = canvas.getContext("2d");
            // 将图片绘制到画布上
            ctx.drawImage(img, 0, 0);
            // 获取画布上的所有像素数据
            var imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);
            var data = imageData.data;
            // 遍历像素数据，将每个像素的alpha通道（透明度）设置为0
            for (var i = 3; i < data.length; i += 4) {
                if (data[i - 1] == 255 && data[i - 2] == 255 && data[i - 3] == 255) {
                    data[i] = 0; // Alpha通道值
                }
            }
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            // 将更新后的像素数据放回画布
            ctx.putImageData(imageData, 0, 0);
            var dataURL = canvas.toDataURL("image/png");
            //清掉canvas节约内存
            img.remove();
            canvas.remove();
            return dataURL;
        };
    },
    /**
     * 输出一个命令行文本
     * @param {string} msg
     */
    ConsoleWriteLine: function (msg) {
        console.log(msg);
    },
    /**
     * 作为文件下载二进制数据
     * @param {Uint8Array |Blob} bsData
     */
    DownloadAsFile: function (bsData, strContentType, strFileName) {
        var blob = null;
        if (bsData instanceof Blob) {
            blob = bsData;
        }
        else if (typeof (bsData) == "string") {
            var en = new TextEncoder();
            var bs2 = en.encode(bsData);
            blob = new Blob([bs2], { type: strContentType });
        }
        else {
            blob = new Blob([bsData], { type: strContentType });
        }
        let downloadElement = document.createElement("a");
        let href = window.URL.createObjectURL(blob); //创建下载的链接
        downloadElement.href = href;
        //console.log(file.name, "文件名");
        downloadElement.download = strFileName;// file.name; //下载后文件名
        document.body.appendChild(downloadElement);
        downloadElement.click(); //点击下载
        document.body.removeChild(downloadElement); //下载完成移除元素
        window.URL.revokeObjectURL(href); //释放掉blob对象
    },
    ///*** 获得所有的可用字体的名称 */
    //GetAllFontNames: function () {
    //    if (typeof (window.queryLocalFonts) == "function") {
    //        var result = new Array();
    //        window.queryLocalFonts().then(function (datas) {
    //            for (const data in datas) {
    //                result.push(data.family);
    //            }
    //        });
    //        return result;
    //    }
    //    return null;
    //},
    /**
     * 格式化输出字节大小
     * @param {number} byteSize
     * @returns {string} 字节大小字符串
     */
    FormatByteSize: function (byteSize) {
        if (typeof (byteSize) != "number") {
            return "";
        }
        const _PBSIZE = 1024 * 1024 * 1024 * 1024;
        const _GBSIZE = 1024 * 1024 * 1024;
        const _MBSIZE = 1024 * 1024;
        const _KBSIZE = 1024;

        var v = byteSize;
        var unit = null;
        if (byteSize > _PBSIZE) {
            v = v * 100 / _PBSIZE;
            unit = "PB";
        }
        else if (byteSize > _GBSIZE) {
            v = v * 100 / _GBSIZE;
            unit = "GB";
        }
        else if (byteSize > _MBSIZE) {
            v = v * 100 / _MBSIZE;
            unit = "MB";
        }
        else if (byteSize > _KBSIZE) {
            v = v * 100 / _KBSIZE;
            unit = "KB";
        }
        else {
            return byteSize.toString() + "B";
        }
        v = Math.round(v);
        var mod = Math.round(v % 100);
        v = Math.round((v - mod) / 100);
        if (v > 10) {
            mod = Math.round(mod / 10);
        }
        if (mod == 0) {
            return v.toString() + unit;
        }
        else {
            return v.toString() + '.' + mod.toString() + unit;
        }
    },

    /**
     * 解析字体设置字符串
     * @param {string} strText 字符串
     * @returns {object} 字体信息对象
     */
    ParseFontString: function (strText) {
        if (strText == null || strText.length == 0) {
            return null;
        }
        var result = { Name: null, Bold: false, Italic: false, Size: null };
        var items = strText.split(" ");
        for (var itemIndex = 0; itemIndex < items.length; itemIndex++) {
            var item = items[itemIndex].trim();
            if (item.length == 0) {
                continue;
            }
            if (item.toLowerCase() == "bold") {
                result.Bold = true;
            }
            else if (item.toLowerCase() == "italic") {
                result.Italic = true;
                continue;
            }
            else if (item.length >= 3
                && "0123456789.".indexOf(item.charAt(0)) >= 0
                && item.substring(item.length - 2) == "pt") {
                // 表示字体大小
                result.Size = item;
                continue;
            }
            else {
                // 字体名称
                result.Name = item;
            }
        }
        result.FullName = result.Name;
        if (result.Italic) {
            result.FullName = "italic " + result.FullName;
        }
        if (result.Bold) {
            result.FullName = "bold " + result.FullName;
        }
        return result;
    },
    /**
     * 判断指定的文本是否符合正则表达式
     * @param {string} strRegex 正则表达式
     * @param {string} strText 输入的文本
     * @returns {boolean} 是否符合文本
     */
    IsMatchRegex: function (strRegex, strText) {
        if (strRegex == null || strRegex.length == 0) {
            return true;
        }
        var reg = new RegExp(strRegex);
        var bolResult = reg.test(strText);
        return bolResult;
    },
    /**
     * 将对象转换为布尔值
     * @param {any} v 对象值
     * @param {any} defaultValue 默认值
     * @returns {boolean} 转换后的布尔值
     */
    ParseBoolean: function (v, defaultValue) {
        if (typeof (v) == "boolean") {
            return v;
        }
        if (typeof (v) == "string") {
            v = v.trim().toLowerCase();
            if (v == "true" || v == "yes" || v == "on") {
                return true;
            }
            else if (v == "false" || v == "no" || v == "off") {
                return false;
            }
        }
        if (typeof (defaultValue) == "boolean") {
            return defaultValue;
        }
        else {
            return false;
        }
    },
    /**
     * 判断是否为一个空白字符串
     * @param {String} str
     * @returns {Boolean} 是否为空白字符串
     */
    IsNullOrEmptyString: function (str) {
        return str == null || str.length == 0;
    },
    /**
     * 清空一个画布全部内容
     * @param {HTMLCanvasElement} element
     */
    ClearCanvasElementContent: function (element) {
        if (element != null && element.nodeName == "CANVAS") {
            var ctx = element.getContext("2d");
            if (typeof (ctx.reset) == "function") {
                ctx.reset();
            } else {
                ctx.clearRect(0, 0, element.width, element.height);
            }
        }
    },
    /**
     * 将一个逗号分隔字符串解释成一个整数数组，若解析失败则返回空引用
     * @param {string} strText
     * @returns {Array} 整数数组
     */
    ParseInt32Values: function (strText) {
        if (strText != null && strText.length > 0) {
            var list = new Array();
            var strItems = strText.split(',');
            for (var iCount = 0; iCount < strItems.length; iCount++) {
                var v2 = parseInt(strItems[iCount]);
                if (isNaN(v2) == false) {
                    list.push(v2);
                }
            }
            if (list.length > 0) {
                return list;
            }
        }
        return null;
    },
    /**
     * 命令行输出警告信息
     * @param {string} strText 信息文本
     */
    ConsoleWarring: function (strText) {
        console.log("%c" + strText, "color:black;background:yellow;padding:2px 6px;border-radius:3px;");
    },
    /**
     * 格式化时间日期
     * @param {Date} dtm 时间日期值
     * @param {string} strFormat 格式化字符串
     * @returns {string} 输出文本
     */
    FormatDateTime: function (dtm, strFormat) {
        return DotNet.invokeMethod(
            window.DCWriterEntryPointAssemblyName,
            "FormatDateTime",
            dtm, strFormat);
    },
    /**
     * 确保底层的编辑器引用存在
     * @param {HTMLElement} rootElement
     */
    EnsureNativeReference: function (rootElement) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement != null && rootElement.__DCWriterReference == null) {
            var nativeControl = DotNet.invokeMethod(
                window.DCWriterEntryPointAssemblyName,
                "GetWriterControlForWASM",
                rootElement.id);
            rootElement.__DCWriterReference = nativeControl;
        }
    },


    DisposeInstance: function (obj) {
        if (obj != null && typeof (obj.dispose) == "function") {
            obj.dispose();
        }
    },
    /**
     * 执行GZIP压缩
     * @param {Uint8Array} bsInput 要压缩的数据
     * @returns { Uint8Array} 压缩后的数据
     */
    GZipCompress: async function (bsInput) {
        // 参考 https://developer.mozilla.org/zh-CN/docs/Web/API/Compression_Streams_API
        //throw "请在这里添加代码GZipCompress";
        //此处bsInput为uint8array
        return Compression(true, bsInput);
    },
    /**
     * 执行GZIP解压缩
     * @param {Uint8Array} bsInput 压缩的数据
     * @returns { Uint8Array} 解压缩后的数据
     */
    GZipDecompress: async function (bsInput) {
        // 参考 https://developer.mozilla.org/zh-CN/docs/Web/API/Compression_Streams_API
        //throw "请在这里添加代码GZipDecompress";
        //return Compression(false, bsInput);
        var result = await new Promise((resolve, reject) => {
            compression(true, bsInput, resolve);
        });

    },

    /**
     * 执行GZIP压缩的通用写法
     * @param {Boolean} bool true为压缩，false为解压缩
     * @returns { Uint8Array} uint8Aarry 加/解压缩后的数据
     */
    Compression: async function (bool, uint8Aarry, resolve) {
        var readableStream = new ReadableStream({
            start(controller) {
                controller.enqueue(uint8Aarry);
                controller.close();
            }
        });
        if (bool) {
            readableStream = readableStream.pipeThrough(new CompressionStream("gzip"));
        } else {
            readableStream = readableStream.pipeThrough(new DecompressionStream("gzip"));
        }

        var chunks = [];
        var reader = readableStream.getReader();
        function read() {
            reader.read()
                .then(({ value, done }) => {
                    if (done) {
                        var totalLength = 0;
                        for (var arr of chunks) {
                            totalLength += arr.length;
                        }
                        var result = new Uint8Array(totalLength);
                        var offset = 0;
                        for (var arr of chunks) {
                            result.set(arr, offset);
                            offset += arr.length;
                        }
                        resolve(result);
                        return;
                    }
                    chunks.push(new Uint8Array(value));
                    read();
                })
                .catch();
        }
        read();
    },

    /**
     * 报告系统异常
     * @param {string} strSourceName 来源名称
     * @param {string} strExceptionMessage 异常消息
     * @param {string} strExceptionString 异常的详细内容
     * @param {number} intLevel 等级
     */
    ReportException: function (strSourceName, strExceptionMessage, strExceptionString, intLevel) {

        var showWriterControl = null;
        var divWASM = document.querySelectorAll("[dctype='WriterControlForWASM']");
        for (var icount = 0; icount < divWASM.length; icount++) {
            var element = divWASM[icount];
            if (element.getClientRects) {
                var rects = element.getClientRects();
                if (rects != null /*&& rects.length != 0*/) {
                    showWriterControl = element;
                }
            }
        }
        var versionstring = "";
        if (showWriterControl != null && typeof (showWriterControl.GetDCWriterVersion) === "function") {
            versionstring = showWriterControl.GetDCWriterVersion.call();
            strExceptionMessage = "version" + versionstring + " | " + strExceptionMessage;
        }
        if (showWriterControl != null) {
            showWriterControl.EventReportException && showWriterControl.EventReportException(strSourceName, strExceptionMessage, strExceptionString, intLevel);
        }
        // 此处添加错误的处理代码 
        let option = { strSourceName, strExceptionMessage, strExceptionString, intLevel };
        if (window.ReportExceptionArr) {
            window.ReportExceptionArr.push(option);
        } else {
            window['ReportExceptionArr'] = [option];
        }
        if (strSourceName === 'TestLoadDocument') {
            //TestLoadDocument接口校验出错的时候，阻止浏览器控制台抛出报错
            return;
        }
        console.error(strExceptionMessage);
        console.error(strExceptionString);
    },
    ///**
    // * 获得指定类型的子元素
    // * @param {string | HTMLElement} rootElement 根元素或者编号
    // * @param {string} strElementName HTML子元素的标签名称
    // * @param {string} strDCTypeName 类型名称
    // * @returns { HTMLElement} 获得的子元素对象
    // */
    //CreateOrGetChildNodeByDCType(rootElement, strElementName, strDCTypeName) {
    //    if (rootElement == null || strElementName == null) {
    //        return null;
    //    }
    //    if (typeof (rootElement) == "string") {
    //        rootElement = document.getElementById(rootElement);
    //    }
    //    if (rootElement == null) {
    //        return null;
    //    }
    //    if (rootElement.firstChild != null) {
    //        for (var item = rootElement.firstChild; item != null; item = item.nextSibling) {
    //            if (item.getAttribute && item.getAttribute("dctype") == strDCTypeName
    //                && item.nodeName.toLocaleLowerCase() == strElementName.toLocaleLowerCase()) {
    //                return item;
    //            }
    //        }
    //        var newNode = document.createElement(strElementName);

    //    }
    //    return null;
    //},

    /** 获得服务器页面地址 
     * @param {HTMLElement} rootElement 编辑器控件元素对象
     * @returns {string} 服务器页面地址
     */
    GetServicePageUrl: function (rootElement) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement != null && rootElement.getAttribute) {
            var url = rootElement.getAttribute("servicepageurl");
            if (url != null && url.length > 0) {
                if (url == "null" || url == "undefined") {
                    return null;
                }
                return DCTools20221228.FixServicePageUrl(url);
            }
        }
        return DCTools20221228.FixServicePageUrl(window.__DCWriterServicePageUrl);
    },
    JS_Eval: function (strScript) {
        return eval(strScript);
    },

    /** wyc20230724
     * 判断传入的前端参数是否是.NET后台引用对象
     * @param {element}  前端JS变量
     * @returns { boolean} 获得的子元素对象
     */
    IsDotnetReferenceElement: function (element) {
        if (element != null && typeof (element) === "object" && typeof (element.serializeAsArg) === "function") {
            return true;
        }
        return false;
    },
    /**
     * 删除指定类型的子节点
     * @param {string | HTMLElement} rootElement 根节点
     * @param {string} strDCTypeName 类型名称
     * @returns { boolean} 是否删除了节点
     */
    RemoveChildByDCType: function (rootElement, strDCTypeName) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement != null && rootElement.firstChild != null) {
            for (var item = rootElement.firstChild; item != null; item = item.nextSibling) {
                if (item.getAttribute && item.getAttribute("dctype") == strDCTypeName) {
                    rootElement.removeChild(item);
                    return true;
                }
            }
        }
        return false;
    },
    /**
     * 获得指定类型的子元素
     * @param {string | HTMLElement} rootElement 根元素或者编号
     * @param {string} strDCTypeName 类型名称
     * @returns { HTMLElement} 获得的子元素对象
     */
    GetChildNodeByDCType: function (rootElement, strDCTypeName) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement != null && rootElement.firstChild != null) {
            if (rootElement.__DCDisposed == true) {
                return null;
            }
            for (var item = rootElement.firstChild; item != null; item = item.nextSibling) {
                if (item.getAttribute && item.getAttribute("dctype") == strDCTypeName) {
                    return item;
                }
            }
        }
        return null;
    },
    /**
     * 获得指定类型的子元素的列表
     * @param {string | HTMLElement} rootElement 根元素或者编号
     * @param {string} strDCTypeName 类型名称
     * @returns { Array} 获得的子元素对象列表
     */
    GetChildNodeListByDCType: function (rootElement, strDCTypeName) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement != null && rootElement.firstChild != null) {
            if (rootElement.__DCDisposed == true) {
                return null;
            }
            var list = new Array();
            for (var item = rootElement.firstChild; item != null; item = item.nextSibling) {
                if (item.getAttribute && item.getAttribute("dctype") == strDCTypeName) {
                    list.push(item);
                }
            }
            if (list.length > 0) {
                return list;
            }
        }
        return null;
    },
    /**
     * 判断浏览器是否支持指定名称的字体
     * @param {string} strFontName 字体名称
     * @returns {boolean} 是否支持该字体
     */
    IsSupportFontName: function (strFontName) {
        if (strFontName == null || strFontName.length == 0) {
            return false;
        }
        try {
            var table = window.___DCWriterSupportFonts20230707;
            if (table == null) {
                table = new Object();
                // 弄一个全局的缓存区
                window.___DCWriterSupportFonts20230707 = table;
            }
            localStorage.removeItem("___DCWriterSupportFonts20230707");
        } catch (e) {
            //此处存在iframe跨域的风险，更换写法为localstorage设置1小时有效期
            var table = localStorage.getItem('___DCWriterSupportFonts20230707');
            if (table == null) {
                table = new Object();
                table.writerTime = Date.now();
                // 弄一个全局的缓存区
                localStorage.setItem("___DCWriterSupportFonts20230707", JSON.stringify(table));
            } else {
                //获取并计算时间
                table = JSON.parse(table);
                var writerTime = parseFloat(table.writerTime);
                var nowTime = Date.now();
                if (nowTime - writerTime > 3600000) {
                    localStorage.removeItem("___DCWriterSupportFonts20230707");
                    table = new Object();
                    table.writerTime = nowTime;
                    // 弄一个全局的缓存区
                    localStorage.setItem("___DCWriterSupportFonts20230707", JSON.stringify(table));
                }
            }
        }
        if (table[strFontName] == true) {
            return true;
        }
        else if (table[strFontName] == false) {
            return false;
        }
        else {
            var bolResult = false;
            var defaultFontFamily = 'Arial';
            if (strFontName.toLowerCase() == defaultFontFamily.toLowerCase()) {
                bolResult = true;
            }
            else {
                var defaultLetter = 'a';
                var defaultFontSize = 100;

                // 使用该字体绘制的canvas
                var width = 100, height = 100;
                var canvas = document.createElement('canvas');
                var context = canvas.getContext('2d', { willReadFrequently: true });
                canvas.width = width;
                canvas.height = height;
                // 全局一致的绘制设定
                context.textAlign = 'center';
                context.fillStyle = 'black';
                context.textBaseline = 'middle';
                var getFontData = function (fontFamily) {
                    // console.log(fontFamily);
                    // 清除
                    context.clearRect(0, 0, width, height);
                    // 设置字体
                    context.font = defaultFontSize + 'px ' + fontFamily + ', ' + defaultFontFamily;
                    // console.log(context.font);
                    context.fillText(defaultLetter, width / 2, height / 2);
                    var data = [];
                    try {
                        data = context.getImageData(0, 0, width, height).data;
                    } catch (error) {

                    }
                    return [].slice.call(data).filter(function (value) {
                        return value != 0;
                    });
                };

                bolResult = getFontData(defaultFontFamily).join('') !== getFontData(strFontName).join('');
            }
            table[strFontName] = bolResult;
            //更新localstorage中的值
            try {
                var hasLocalStorage = localStorage.getItem('___DCWriterSupportFonts20230707');
                if (hasLocalStorage) {
                    var oldTable = JSON.parse(hasLocalStorage);
                    for (var t in table) {
                        oldTable[t] = table[t];
                    }
                    localStorage.setItem('___DCWriterSupportFonts20230707', JSON.stringify(oldTable));
                }
            } catch (e) { }
            return bolResult;
        }
    },
    /**
     * 获得HTML元素所在的编辑器控件对象
     * @param {string | HTMLElement} elementID 元素对象或者编号
     * @returns {HTMLDivElement} 获得的编辑器控件DOM元素对象
     */
    GetOwnerWriterControl: function (elementID) {
        if (elementID == null) {
            return null;
        }
        var element = elementID;
        if (typeof (elementID) == "string") {
            element = document.getElementById(elementID);
            if (element == null && window.__DCWriterControls != null) {
                element = window.__DCWriterControls[elementID];
            }
        }
        if (element != null && element.parentNode) {
            while (element != null && element.getAttribute) {
                var strDCType = element.getAttribute("dctype");
                if (strDCType == "WriterControlForWASM"
                    || strDCType == "WriterPrintPreviewControlForWASM"
                    || strDCType == "DCTemperatureControlForWASM"
                    || strDCType == "DCFlowControlForWASM") {
                    return element;
                }
                element = element.parentNode;
                if (element != null && element.nodeName == "#document") {
                    if (element.defaultView != null
                        && element.defaultView.frameElement != null) {
                        var e9 = element.defaultView.frameElement.parentNode;
                        if (e9 != null && e9.getAttribute) {
                            var strDCType = e9.getAttribute("dctype");
                            if (strDCType == "WriterControlForWASM"
                                || strDCType == "WriterPrintPreviewControlForWASM"
                                || strDCType == "DCTemperatureControlForWASM"
                                || strDCType == "DCFlowControlForWASM") {
                                return e9;
                            }
                        }
                    }
                    return null;
                }
            }
        }
        return null;
    },
    /**
     * 采用DES算法进行解密
     * @param {Uint8Array} bsData 要解密的数据
    * @param {Uint8Array} bsKey 8字节长度的密钥
    * @param {Uint8Array} bsIV 8字节长度的向量
    * @returns {Uint8Array} 解密后的数据
    */
    DESDecrypt: function (bsData, bsKey, bsIv = bsKey) {
        let keyHex = DCTools20221228.CryptoJS.enc.u8array.parse(bsKey);
        let ivHex = DCTools20221228.CryptoJS.enc.u8array.parse(bsIv);
        let decryptedWordArray = DCTools20221228.CryptoJS.enc.u8array.parse(bsData);
        let decrypted = DCTools20221228.CryptoJS.DES.decrypt(
            decryptedWordArray.toString(DCTools20221228.CryptoJS.enc.Base64),
            keyHex,
            {
                iv: ivHex,
                mode: DCTools20221228.CryptoJS.mode.CBC,
                padding: DCTools20221228.CryptoJS.pad.Pkcs7
            }
        );
        let deuint8Value = DCTools20221228.CryptoJS.enc.u8array.stringify(decrypted);
        return deuint8Value;
    },

    /**
     * 采用DES算法进行加密
     * @param {Uint8Array} bsData 原始数据
     * @param {Uint8Array} bsKey 8字节长度的密钥
     * @param {Uint8Array} bsIV 8字节长度的向量
     * @returns {Uint8Array} 加密后的数据
     */
    DESEncrypt: function (bsData, bsKey, bsIv = bsKey) {
        let keyHex = DCTools20221228.CryptoJS.enc.u8array.parse(bsKey);
        let ivHex = DCTools20221228.CryptoJS.enc.u8array.parse(bsIv);
        let encryptedWordArray = DCTools20221228.CryptoJS.enc.u8array.parse(bsData);
        let encrypted = DCTools20221228.CryptoJS.DES.encrypt(
            encryptedWordArray,
            keyHex,
            {
                iv: ivHex,
                mode: DCTools20221228.CryptoJS.mode.CBC,
                padding: DCTools20221228.CryptoJS.pad.Pkcs7
            }
        );
        let uint8Value = DCTools20221228.CryptoJS.enc.u8array.stringify(encrypted.ciphertext);
        return uint8Value;
    },

    /**
     * 获得CANVAS元素的图像内容
     * @param {HTMLCanvasElement} element CANVAS元素对象
     * @param {Function} callBack 获得图片数据后的回调函数
     */
    GetCanvasImageData: function (element, callBack) {
        //在此处将canvas处理为image数据传回后台
        var base64Url = element.toDataURL('image/png', 1);
        let arr = base64Url.split(','), // base64拆分数据 头部为图片格式信息
            mime = 'image/png', // 图片格式,可写死为image/png arr[0].match(/:(.*?);/)[1]
            bstr = atob(arr[1]), // 将base64解码
            n = bstr.length, // 图片真实大小
            u8arr = new Uint8Array(n);
        // Uint8Array 数组类型表示一个8位无符号整型数组，
        // 创建时内容被初始化为0。
        // 创建完后，可以以对象的方式或使用数组下标索引的方式引用数组中的元素。
        while (n--) {
            u8arr[n] = bstr.charCodeAt(n); // 获取图片每一位字符的Unicode
        }
        // 生成新文件
        let newBlob = new Blob([u8arr], {
            type: mime
        });
        callBack(newBlob);
    },
    /**
     * 修正资源基础路径
     * @param {string} strBasePath 资源路径
     * @returns {string} 修正后的路径
     */
    FixResourceBasePath: function (strBasePath) {
        strBasePath = DCTools20221228.FixServicePageUrl(strBasePath);
        if (strBasePath != null && strBasePath.length > 0) {
            strBasePath = strBasePath.trim();
            if (strBasePath.length > 0) {
                if (strBasePath.substring(strBasePath.length - 1) != "/") {
                    strBasePath = strBasePath + "/";
                }
                return strBasePath;
            }
        }
        return null;
    },
    /**
     * 修正服务器页面地址
     * @param {string} strUrl 原始地址
     * @returns {string} 修正后的地址
     */
    FixServicePageUrl: function (strUrl) {
        if (strUrl != null && strUrl.length > 0) {
            strUrl = strUrl.trim();
            var index = strUrl.indexOf("?");
            if (index > 0) {
                strUrl = strUrl.substring(0, index).trim();
            }
        }
        return strUrl;
    },
    /**
     * 将对象转换为一个JSON字符串
     * @param {any} object 对象
     * @returns {string} JSON字符串
     */
    GetJsonText: function (obj) {
        if (obj == null) {
            return null;
        }
        else {
            return JSON.stringify(obj);
        }
    },
    /**
     * 获得本系统和标准时间的差别
     * @returns 时差
     */
    GetNow: function () {
        return new Date();
    },
    /**
     * 获得本系统和标准时间的差别
     * @returns 时差
     */
    GetTimezoneOffset: function () {
        return new Date().getTimezoneOffset();
    },
    /**判断元素在可滚动的容器中是否处于可见状态
     * @param {HTMLElement} element 文档元素
     * @returns {boolean} 是否可见
     * */
    IsInVisibleArea: function (element) {
        var sp = element.offsetParent;
        if (sp == null) {
            return true;
        }
        if (element.offsetTop + element.offsetHeight > sp.scrollTop
            && element.offsetTop - sp.scrollTop < sp.clientHeight) {
            if (element.offsetLeft + element.offsetWidth > sp.scrollLeft
                && element.offsetLeft - sp.scrollLeft < sp.clientWidth) {
                return true;
            }
        }
        return false;
    },
    /**
     * 将一个字节数组转换为BASE64字符串
     * @param {Uint8Array} bsData 字节数组
     * @returns {string} BASE64字符串
     */
    ToBase64String: function (bsData) {
        let CHUNK_SIZE = 0X8000;
        let index = 0;
        let length = bsData.length;
        var strResult = "";
        var slice;
        while (index < length) {
            slice = bsData.subarray(index, Math.min(index + CHUNK_SIZE, length));
            strResult += String.fromCharCode.apply(null, slice);
            index += CHUNK_SIZE;
        }
        var strBase64 = window.btoa(strResult);
        return strBase64;
    },
    /**
     * 将BASE64字符串转换为一个字节数组
     * @param {string} strBase64String BASE64字符串
     * @returns {Uint8Array} 字节数组
     */
    FromBase64String: function (strBase64String) {
        //var tick = new Date().valueOf();
        var result = window.atob(strBase64String);
        //var len1 = strBase64String.length;
        var len2 = result.length;
        var result9 = new Uint8Array(result.length);
        for (var iCount = 0; iCount < len2; iCount++) {
            result9[iCount] = result.charCodeAt(iCount);
        }
        //var tick2 = new Date().valueOf();
        //DotNet.invokeMethod(
        //    window.EntryPointAssemblyName,
        //    "SetResult_FromBase64String",
        //    result9);
        //tick2 = new Date().valueOf() - tick2;

        //tick = new Date().valueOf() - tick;
        return result9;

        //var a3 = Array.from(result);
        //var r2 = new Uint8Array(a3);
        //var asa = Uint16Array.from(result);
        //return result;
    },
    /**
     * 修复服务器页面地址
     * @param {string} strUrl 服务器页面地址
     * @returns {string} 修正后的服务器页面地址
     */
    CleanServicePageUrl: function (strUrl) {
        if (strUrl != null && strUrl.length > 0) {
            strUrl = strUrl.trim();
            var index = strUrl.indexOf("?");
            if (index > 0) {
                strUrl = strUrl.substr(0, index).trim();
            }
            return strUrl;
        }
        return null;
    },
    /**
     * 执行字符解码
     * @param {string} encodingName 编码名称
     * @param {ArrayBuffer} bsData 二进制数据
     * @returns {string} 编码生成的字符串
     */
    DecodeString: function (encodingName, bsData) {
        var decoder = new TextDecoder(encodingName);
        return decoder.decode(bsData);
    },
    ///**
    // * 执行字符编码
    // * @param {string} encodingName 编码名称
    // * @param {string} strData 字符串数据
    // * @returns {Uint8Array} 编码生成的字节数组
    // */
    //EncodeString: function (encodingName, strData, callback) {  //暂时无法实现同步
    //    //var encoder = new TextEncoder();// 目前只支持UTF8格式，需要扩展
    //    //var bs = encoder.encode(strData);
    //    //return bs;
    //    new Promise((resolve) => {
    //        DCTools20221228._encode(strData, encodingName, (data) => {
    //            resolve(data);
    //        })
    //    }).then(result => {
    //        //将文本替换为uint8Array
    //        result = new TextEncoder().encode(result);
    //        callback(result);
    //    })
    //},

    //_encode: function (str, charset, callback) {
    //    //创建form通过accept-charset做encode
    //    var form = document.createElement('form');
    //    form.method = 'get';
    //    form.style.display = 'none';
    //    form.acceptCharset = charset;
    //    if (document.all) {
    //        //如果是IE那么就调用document.charset方法
    //        window.oldCharset = document.charset;
    //        document.charset = charset;
    //    }
    //    var input = document.createElement('input');
    //    input.type = 'hidden';
    //    input.name = 'str';
    //    input.value = str;
    //    form.appendChild(input);
    //    form.target = '__encode__iframe__'; // 指定提交的目标的iframe
    //    document.body.appendChild(form);
    //    //隐藏iframe截获提交的字符串
    //    // if (!window['__encode__iframe__']) {
    //    var iframe;
    //    if (document.all) {
    //        try {
    //            iframe = document.createElement('<iframe name="__encode__iframe__"></iframe>');
    //        } catch (e) {
    //            iframe = document.createElement('iframe');
    //            iframe.setAttribute('name', '__encode__iframe__');
    //        }
    //    } else {
    //        iframe = document.createElement('iframe');
    //        iframe.setAttribute('name', '__encode__iframe__');
    //    }
    //    iframe.style.display = 'none';
    //    iframe.width = "0";
    //    iframe.height = "0";
    //    iframe.scrolling = "no";
    //    iframe.allowtransparency = "true";
    //    iframe.frameborder = "0";
    //    iframe.src = 'about:blank'; // 设置为空白
    //    document.body.appendChild(iframe);
    //    // }
    //    window.__encode__iframe__callback__ = function (str) {
    //        callback(str);
    //        form.parentNode.removeChild(form);
    //        iframe.parentNode.removeChild(iframe);
    //        if (document.all) {
    //            document.charset = window.oldCharset;
    //        }
    //    }
    //    //设置回调编码页面的地址，这里需要用户修改
    //    form.action = window.location.href;
    //    form.submit();
    //},
    /**
     * 以同步方式从指定位置下载二进制数据
     * @param {string} strUrl URL地址
     * @returns {ArrayBuffer} 下载的数据
     */
    HttpDownloadString: function (strUrl) {
        var strResult = null;
        var xhr = new XMLHttpRequest();
        xhr.responseType = "blob";
        xhr.open("GET", strUrl, false);
        xhr.onload = function () {
            if (this.status == 200) {
                strResult = this.responseText;
            }
        };
        xhr.send();
        return strResult;
    },

    /**
     * 以同步方式从指定位置下载二进制数据
     * @param {string} strUrl URL地址
     * @returns {ArrayBuffer} 下载的数据
     */
    HttpDownload: function (strUrl) {
        var bsResult = null;
        var xhr = new XMLHttpRequest();
        xhr.open("GET", strUrl, false);
        xhr.responseType = "blob";
        xhr.onload = function () {
            if (this.status == 200) {
                var blob = this.response;
                bsResult = blob.buffer;
            }
        };
        xhr.send();
        return bsResult;
    },
    /**
     * 执行HTTP传输任务
     * @param {string} strURL 服务器页面地址
     * @param {string} strMethod HTTP方法
     * @param {string} strPostData 要发送的数据
     * @param {boolean} bolAsync 是否为异步模式
     * @returns {string} 服务器返回的内容
     */
    HttpSend: function (strURL, strMethod, strPostData, bolAsync) {
        if (typeof (jQuery) == "undefined") {
            throw "系统错误：系统未安装JQuery组件，无法执行任务";
        }
        var args = { async: bolAsync, data: strPostData, method: strMethod, type: strMethod, processData: false };
        var result = null;
        jQuery.ajax(strURL, args).done(function (data, textStatus, jqXHR) {
            result = data;
        });
        return result;
    },

    /**
     * 执行HTTP同步二进制传输任务
     * @param {string} strURL 服务器页面地址
     * @param {string} strMethod HTTP方法
     * @param {ArrayBuffer} bsData 要发送的数据
     * @returns {ArrayBuffer} 服务器返回的内容
     */
    HttpSendBinary: function (strURL, strMethod, bsData) {
        var bsResult = null;
        var xhr = new XMLHttpRequest();
        xhr.open(strMethod, strURL, true);
        xhr.responseType = "blob";
        xhr.onload = function () {
            if (this.status == 200) {
                var blob = this.response;
                bsResult = blob.buffer;
            }
        };
        xhr.send(bsData);
        return bsResult;
    },

    ///**
    // * 获得指定类型的子节点
    // * @param {string | HTMLElement } containerID 容器元素编号
    // * @param {string} strDCType  类型
    // * @returns { HTMLElement} 找到的子节点对象
    // */
    //GetChildNodeByDCType: function (containerID, strDCType) {
    //    var c = DCTools20221228.GetContainerElement(containerID);
    //    if (c != null) {
    //        var node = c.firstChild;
    //        while (node != null) {
    //            if (node.getAttribute && node.getAttribute("dctype") == strDCType) {
    //                return node;
    //            }
    //            node = node.nextSibling;
    //        }
    //    }
    //    return null;
    //},
    /**
     * 记录时刻信息
     * @param {string} strName 事件名称
     */
    LogTick: function (strName) {
        //var win = window.top;
        var win = window;
        if (typeof (win.__LogTick) != "function") {
            win.__Tick_Start = new Date().valueOf();
            win.__Tick_Last = new Date().valueOf();
            win.__LogTick = function (strName) {
                var tick = new Date().valueOf();
                console.log(strName + " \t| 时刻:" + (tick - win.__Tick_Start) + ",距离上次时刻:" + (tick - win.__Tick_Last));
                win.__Tick_Last = tick;
            };
        }
        win.__LogTick(strName);
    },

    /**
     * 获取chrome版本
     * @returns {string} version 版本
     */
    GetChromium: function () {
        var re = new RegExp('Chrome/(.+?) ');
        var version = re.exec(navigator.userAgent);
        if (version) {
            return parseInt(version[1]);
        } else {
            return null;
        }
    },

    ///**
    // * 执行GZIP的解压缩
    // * @param {any} inputData 二进制数据
    // * @returns {any} 解压缩后的数据
    //*/
    //GDecompress: async function (inputData) {
    //    if (DecompressionStream) {
    //        const ds = new DecompressionStream('gzip');
    //        const decompressedStream = blob.stream().pipeThrough(ds);
    //        return await new Response(decompressedStream).blob();
    //    }
    //    else {
    //        return null;
    //    }
    //},
    ///**
    // * 将BASE64字符串转换为二进制
    // * @param {string} strBase64String BASE64字符串
    // * @returns {any} 转换后的结果
    // */
    //FromBase64String: function (strBase64String) {
    //    return atob(strBase64String);
    //},
    ///**
    // * 将二进制转换为BASE64字符串
    // * @param {any} strData 二进制数据
    // * @returns {string} BASE64字符串
    // */
    //ToBase64String: function (strData) {
    //    return btoa(strData);
    //},
    ///**
    // * 根据编号获得容器元素对象
    // * @param {string | HTMLElement} containerID
    // * @returns {HTMLElement} 容器元素对象
    // */
    //GetContainerElement: function (containerID) {
    //    if (typeof (containerID) == "string") {
    //        // 根据编号查找元素
    //        return document.getElementById(containerID);
    //    }
    //    else if (containerID.nodeName) {
    //        // 传入的就是一个元素
    //        if (containerID.nodeName == "CANVAS") {
    //            return containerID.parentNode;
    //        }
    //        else {
    //            return containerID;
    //        }
    //    }
    //    return null;
    //},
    /**
     * 获得指定的编辑器的后端引用对象
     * @param {string} containerID
     * @returns {any} 引用的对象
     */
    GetDCWriterReference: function (containerID) {
        var element = DCTools20221228.GetOwnerWriterControl(containerID);
        if (element != null) {
            return element.__DCWriterReference;
        }
        else {
            return null;
        }
    },

    ///**
    // * 获得并发锁,如果锁被其他人占用，则一直等待
    // * @param {string} strName 锁的名称
    // */
    //GetLock: function (strName) {
    //    var tick = new Date().valueOf();
    //    DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "GetLock", strName);
    //    tick = new Date().valueOf() - tick;
    //    //console.log("获得锁 " + strName + " 耗时 " + tick + "毫秒.");
    //},
    ///**
    // * 释放并发锁
    // * @param {string} strName 锁的名称
    // */
    //ReleaseLock: function (strName) {
    //    DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "ReleaseLock", strName);
    //},
    //// 
    ///**
    // * 试图获得并发锁，如果失败则立刻返回。
    // * @param {string} strName
    // * @returns {boolean} 是否获得并发锁
    // */
    //TryGetLock: function (strName) {
    //    return DotNet.invokeMethod(window.DCWriterEntryPointAssemblyName, "TryGetLock", strName);
    //},

    /** 获得客户端编号
     * @returns {string} 编号
     * */
    GetClientID: function () {
        return Fingerprint2.GetMyFingerprint();
    },

    /**
     * 对生僻字进行编码改变
     * @param {string} str 需要替换的文本
     * @returns {string} 替换后的文本
     */
    changeUseUTF16: function (str, changeSymbol) {
        //需要替换的文本在utf16中的编码格式
        var utf16Code = [
            57659, 57505, 57828, 57806, 57768, 57820, 57697, 57802, 57753, 57801,
            57760, 57761, 57426, 57432, 57446, 57448, 57449, 57451, 57452, 57453,
            57456, 57458, 57459, 57467, 57468, 57469, 57474, 57475, 57476, 57480,
            57484, 57485, 57489, 57490, 57497, 57498, 57499, 57500, 57501, 57528,
            57529, 57531, 57539, 57541, 57568, 57569, 57583, 57595, 57608, 57612,
            57641, 57648,
            57796, 57703, 57704, 57727, 57728, 57729, 57730, 57741, 57781, 57506,
            57530, 57562, 57636, 57637, 57640, 57679, 57680, 57711, 57652, 57714,
            57591, 57632, 57653, 57718, 57582, 57649, 57650, 57712, 57694, 57708,
            57513, 57590, 57477, 57512, 57579, 57599, 57621, 57808, 57675, 57425,
            57575, 57630, 57576, 57578, 57692, 57719, 57586, 57756, 57779, 57691,
            57763, 57493, 57438, 57669, 57483, 57778, 57740, 57467, 57540, 57614,
            57683, 57689, 57563, 57795, 57687, 57577, 57553, 57717, 57496, 57435,
            57688, 57625, 57618, 57665, 57701, 57547
        ];
        //需要替换的文本在html中能展示文本
        var newString = [
            '𧿹', '𨚕', '𫄟', '𬙋', '𫗴', '𤭢', '𨁂', '𨟠', '𤩄', '𧒽',
            '䗛', '𧎥', '𬎆', '𬍛', '𬿕', '𫸩', '𨚓', '𬌗', '𭴊', '𠇔',
            '𣡽', '𤞞', '𧊅', '𠳐', '㳌', '㙃', '𩇕', '𠙶', '𨙮', '𨙸',
            '𢖳', '𬣞', '𫭟', '𫭢', '𨚔', '𣲘', '𣲗', '𬇘', '𬇙', '𡛟',
            '𪥰', '𩧬', '鿍', '𦰡', '𤤸', '𫭼', '𨈓', '𡝐', '䓬', '𣆳',
            '㻓', '𢯺',
            '䎖', '𬶋', '𬶍', '𫚕', '𬶐', '𬶏', '𩽾', '𩾃', '𬸪', '𫘜',
            '𬳵', '𬳽', '𬴂', '𫘦', '𫘧', '𬴃', '𫘨', '𫘬', '𬃊', '𣗋',
            '𨺙', '𬯎', '𬷕', '𥔲', '𫮃', '𪣻', '𡎚', '𡐓', '𫫇', '𫌀',
            '𬨂', '𬒈', '𬣙', '𦭜', '𬬿', '𫄨', '䝙', '𬺓', '𬊤', '𬘭',
            '𪨶', '𬤊', '𬬸', '𬬹', '𫐓', '𫚖', '𫗧', '𫟦', '𬭼', '𬨎',
            '𪩘', '𫇭', '𬀩', '𬱟', '𬇕', '𨱔', '𬭩', '𠳐', '𬜬', '𬱖',
            '𤧛', '䃅', '𬘩', '𦒍', '𬪩', '𬬻', '𬬭', '𬒗', '𫵷', '𬕂',
            '𬒔', '𬇹', '𫟹', '𬭎', '𬭚', '荿'
        ];
        //会被html错误解析为uft16码元的字符
        var utf16Symbol = [
            "&#\ud85f;&#\udff9;", "&#\ud861;&#\ude95;", "&#\ud86c;&#\udd1f;", "&#\ud871;&#\ude4b;", "&#\ud86d;&#\uddf4;", "&#\ud852;&#\udf62;", "&#\ud860;&#\udc42;", "&#\ud861;&#\udfe0;", "&#\ud852;&#\ude44;", "&#\ud85d;&#\udcbd;",
            "䗛;", "&#\ud85c;&#\udfa5;", "&#\ud870;&#\udf86;", "&#\ud870;&#\udf5b;", "&#\ud873;&#\udfd5;", "&#\ud86f;&#\ude29;", "&#\ud861;&#\ude93;", "&#\ud870;&#\udf17;", "&#\ud877;&#\udd0a;", "&#\ud840;&#\uddd4;",
            "&#\ud84e;&#\udc7d;", "&#\ud851;&#\udf9e;", "&#\ud85c;&#\ude85;", "&#\ud843;&#\udcd0;", "㳌;", "㙃;", "&#\ud864;&#\uddd5;", "&#\ud841;&#\ude76;", "&#\ud861;&#\ude6e;", "&#\ud861;&#\ude78;",
            "&#\ud849;&#\uddb3;", "&#\ud872;&#\udcde;", "&#\ud86e;&#\udf5f;", "&#\ud86e;&#\udf62;", "&#\ud861;&#\ude94;", "&#\ud84f;&#\udc98;", "&#\ud84f;&#\udc97;", "&#\ud870;&#\uddd8;", "&#\ud870;&#\uddd9;", "&#\ud845;&#\udedf;",
            "&#\ud86a;&#\udd70;", "&#\ud866;&#\uddec;", "鿍;", "&#\ud85b;&#\udc21;", "&#\ud852;&#\udd38;", "&#\ud86e;&#\udf7c;", "&#\ud860;&#\ude13;", "&#\ud845;&#\udf50;", "䓬;", "&#\ud84c;&#\uddb3;",
            "㻓;", "&#\ud84a;&#\udffa;",
            "䎖;", "&#\ud873;&#\udd8b;", "&#\ud873;&#\udd8d;", "&#\ud86d;&#\ude95;", "&#\ud873;&#\udd90;", "&#\ud873;&#\udd8f;", "&#\ud867;&#\udf7e;", "&#\ud867;&#\udf83;", "&#\ud873;&#\ude2a;", "&#\ud86d;&#\ude1c;",
            "&#\ud873;&#\udcf5;", "&#\ud873;&#\udcfd;", "&#\ud873;&#\udd02;", "&#\ud86d;&#\ude26;", "&#\ud86d;&#\ude27;", "&#\ud873;&#\udd03;", "&#\ud86d;&#\ude28;", "&#\ud86d;&#\ude2c;", "&#\ud870;&#\udcca;", "&#\ud84d;&#\uddcb;",
            "&#\ud863;&#\ude99;", "&#\ud872;&#\udfce;", "&#\ud873;&#\uddd5;", "&#\ud855;&#\udd32;", "&#\ud86e;&#\udf83;", "&#\ud86a;&#\udcfb;", "&#\ud844;&#\udf9a;", "&#\ud845;&#\udc13;", "&#\ud86e;&#\udec7;", "&#\ud86c;&#\udf00;",
            "&#\ud872;&#\ude02;", "&#\ud871;&#\udc88;", "&#\ud872;&#\udcd9;", "&#\ud85a;&#\udf5c;", "&#\ud872;&#\udf3f;", "&#\ud86c;&#\udd28;", "䝙;", "&#\ud873;&#\ude93;", "&#\ud870;&#\udea4;", "&#\ud871;&#\ude2d;",
            "&#\ud86a;&#\ude36;", "&#\ud872;&#\udd0a;", "&#\ud872;&#\udf38;", "&#\ud872;&#\udf39;", "&#\ud86d;&#\udc13;", "&#\ud86d;&#\ude96;", "&#\ud86d;&#\udde7;", "&#\ud86d;&#\udfe6;", "&#\ud872;&#\udf7c;", "&#\ud872;&#\ude0e;",
            "&#\ud86a;&#\ude58;", "&#\ud86c;&#\udded;", "&#\ud870;&#\udc29;", "&#\ud873;&#\udc5f;", "&#\ud870;&#\uddd5;", "&#\ud863;&#\udc54;", "&#\ud872;&#\udf69;", "&#\ud843;&#\udcd0;", "&#\ud871;&#\udf2c;", "&#\ud873;&#\udc56;",
            "&#\ud852;&#\udddb;", "䃅;", "&#\ud871;&#\ude29;", "&#\ud859;&#\udc8d;", "&#\ud872;&#\udea9;", "&#\ud872;&#\udf3b;", "&#\ud872;&#\udf2d;", "&#\ud871;&#\udc97;", "&#\ud86f;&#\udd77;", "&#\ud871;&#\udd42;",
            "&#\ud871;&#\udc94;", "&#\ud870;&#\uddf9;", "&#\ud86d;&#\udff9;", "&#\ud872;&#\udf4e;", "&#\ud872;&#\udf5a;", "荿;"
        ];
        var hexadecimal = [
            "&#xd85f;&#xdff9;", "&#xd861;&#xde95;", "&#xd86c;&#xdd1f;", "&#xd871;&#xde4b;", "&#xd86d;&#xddf4;", "&#xd852;&#xdf62;", "&#xd860;&#xdc42;", "&#xd861;&#xdfe0;", "&#xd852;&#xde44;", "&#xd85d;&#xdcbd;",
            "䗛;", "&#xd85c;&#xdfa5;", "&#xd870;&#xdf86;", "&#xd870;&#xdf5b;", "&#xd873;&#xdfd5;", "&#xd86f;&#xde29;", "&#xd861;&#xde93;", "&#xd870;&#xdf17;", "&#xd877;&#xdd0a;", "&#xd840;&#xddd4;",
            "&#xd84e;&#xdc7d;", "&#xd851;&#xdf9e;", "&#xd85c;&#xde85;", "&#xd843;&#xdcd0;", "㳌;", "㙃;", "&#xd864;&#xddd5;", "&#xd841;&#xde76;", "&#xd861;&#xde6e;", "&#xd861;&#xde78;",
            "&#xd849;&#xddb3;", "&#xd872;&#xdcde;", "&#xd86e;&#xdf5f;", "&#xd86e;&#xdf62;", "&#xd861;&#xde94;", "&#xd84f;&#xdc98;", "&#xd84f;&#xdc97;", "&#xd870;&#xddd8;", "&#xd870;&#xddd9;", "&#xd845;&#xdedf;",
            "&#xd86a;&#xdd70;", "&#xd866;&#xddec;", "鿍;", "&#xd85b;&#xdc21;", "&#xd852;&#xdd38;", "&#xd86e;&#xdf7c;", "&#xd860;&#xde13;", "&#xd845;&#xdf50;", "䓬;", "&#xd84c;&#xddb3;",
            "㻓;", "&#xd84a;&#xdffa;",
            "䎖;", "&#xd873;&#xdd8b;", "&#xd873;&#xdd8d;", "&#xd86d;&#xde95;", "&#xd873;&#xdd90;", "&#xd873;&#xdd8f;", "&#xd867;&#xdf7e;", "&#xd867;&#xdf83;", "&#xd873;&#xde2a;", "&#xd86d;&#xde1c;",
            "&#xd873;&#xdcf5;", "&#xd873;&#xdcfd;", "&#xd873;&#xdd02;", "&#xd86d;&#xde26;", "&#xd86d;&#xde27;", "&#xd873;&#xdd03;", "&#xd86d;&#xde28;", "&#xd86d;&#xde2c;", "&#xd870;&#xdcca;", "&#xd84d;&#xddcb;",
            "&#xd863;&#xde99;", "&#xd872;&#xdfce;", "&#xd873;&#xddd5;", "&#xd855;&#xdd32;", "&#xd86e;&#xdf83;", "&#xd86a;&#xdcfb;", "&#xd844;&#xdf9a;", "&#xd845;&#xdc13;", "&#xd86e;&#xdec7;", "&#xd86c;&#xdf00;",
            "&#xd872;&#xde02;", "&#xd871;&#xdc88;", "&#xd872;&#xdcd9;", "&#xd85a;&#xdf5c;", "&#xd872;&#xdf3f;", "&#xd86c;&#xdd28;", "䝙;", "&#xd873;&#xde93;", "&#xd870;&#xdea4;", "&#xd871;&#xde2d;",
            "&#xd86a;&#xde36;", "&#xd872;&#xdd0a;", "&#xd872;&#xdf38;", "&#xd872;&#xdf39;", "&#xd86d;&#xdc13;", "&#xd86d;&#xde96;", "&#xd86d;&#xdde7;", "&#xd86d;&#xdfe6;", "&#xd872;&#xdf7c;", "&#xd872;&#xde0e;",
            "&#xd86a;&#xde58;", "&#xd86c;&#xdded;", "&#xd870;&#xdc29;", "&#xd873;&#xdc5f;", "&#xd870;&#xddd5;", "&#xd863;&#xdc54;", "&#xd872;&#xdf69;", "&#xd843;&#xdcd0;", "&#xd871;&#xdf2c;", "&#xd873;&#xdc56;",
            "&#xd852;&#xdddb;", "䃅;", "&#xd871;&#xde29;", "&#xd859;&#xdc8d;", "&#xd872;&#xdea9;", "&#xd872;&#xdf3b;", "&#xd872;&#xdf2d;", "&#xd871;&#xdc97;", "&#xd86f;&#xdd77;", "&#xd871;&#xdd42;",
            "&#xd871;&#xdc94;", "&#xd870;&#xddf9;", "&#xd86d;&#xdff9;", "&#xd872;&#xdf4e;", "&#xd872;&#xdf5a;", "荿;"
        ];
        //需要替换的文本在html中能展示的编码
        var newCode = [
            163833, 165525, 176415, 181835, 177652, 150370, 163906, 165856, 150084, 160957,
            17883, 160677, 181126, 181083, 184277, 179753, 165523, 181015, 187658, 131540,
            145533, 149406, 160389, 134352, 15564, 13891, 168405, 132726, 165486, 165496,
            140723, 182494, 179039, 179042, 165524, 146584, 146583, 180696, 180697, 136927,
            174448, 170476, 40909, 158753, 149816, 179068, 164371, 137040, 17644, 143795,
            16083, 142330,
            17302, 183691, 183693, 177813, 183696, 183695, 171902, 171907, 183850, 177692,
            183541, 183549, 183554, 177702, 177703, 183555, 177704, 177708, 180426, 144843,
            167577, 183246, 183765, 152882, 179075, 174331, 136090, 136211, 178887, 176896,
            182786, 181384, 182489, 158556, 183103, 176424, 18265, 183955, 180900, 181805,
            174646, 182538, 183096, 183097, 177171, 177814, 177639, 178150, 178150, 182798,
            174680, 176621, 180265, 183391, 180693, 166996, 183145, 134352, 182060, 183382,
            149979, 16581, 181801, 156813, 182953, 183099, 183085, 181399, 179575, 181570,
            181396, 180729, 178169, 183118, 183130, 33663
        ];

        if (changeSymbol && str) {
            var hasSymbol = utf16Symbol.indexOf(str);
            if (hasSymbol >= 0) {
                str = `&#${newCode[hasSymbol]};`;
                return {
                    newCode: str,
                    hexadecimal: hexadecimal[hasSymbol]
                };
            } else {
                return str;
            }

        }

        var charCode = str.codePointAt(0);
        var hasIndex = utf16Code.indexOf(charCode);
        if (hasIndex >= 0) {
            return newString[hasIndex];
        } else {
            return str;
        }
    },
    /**
     * 判断是否支持移动端表单模式下单击非输入域下不会主动让最近的输入域获取焦点【DUWRITER5_0-3179】
     * @param {HTMLElement} rootElement - 待检查的根元素。
     * @param {boolean} StrictFromViewMode - 是否需要判断是否处于严格从视图模式，默认为false。
     * @returns {boolean} 如果属性需要生效，为true。
     */
    IsReadonlyAutoFocus: function (rootElement, StrictFromViewMode = false) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null || rootElement.__DCWriterReference == null) {
            return false;
        }
        /** 是否是表单模式 */
        var isFormView = rootElement.DocumentOptions.BehaviorOptions.FormView == 'Strict' || rootElement.FormView() == "Strict";
        if (StrictFromViewMode == true && isFormView == false) {
            return false;
        }
        if ('ontouchstart' in rootElement.ownerDocument.documentElement) {
            var hasReadonlyautofocus = rootElement.getAttribute("readonlyautofocus");
            if (typeof hasReadonlyautofocus == "string") {
                hasReadonlyautofocus = hasReadonlyautofocus.toLowerCase().trim();
                var agreeArr = ["false", "yaxis", "xandyaxis"];
                if (agreeArr.indexOf(hasReadonlyautofocus) >= 0) {
                    return true;
                }
            }
        }
        return false;
    },
    /** 
    * 销毁iframe，释放iframe所占用的内存。
    * @param iframe 须要销毁的iframe
    */
    destroyIframe: function (iframe) {
        if (!iframe) {
            return;
        }
        //把iframe指向空白页面，这样能够释放大部份内存。 
        iframe.src = 'about:blank';
        try {
            iframe.contentWindow.document.write('');
            iframe.contentWindow.document.clear();
        } catch (e) { }
        //把iframe从页面移除 
        iframe.parentNode.removeChild(iframe);
    },
    /**
     * 将颜色字符串转换为十六进制格式
     * @param {string} color - 输入的颜色字符串，可以是RGB格式，十六进制格式或者颜色名称
     * @returns {string} - 转换后的十六进制颜色字符串
     */
    colorToHex: function (color) {
        if (typeof (color) != "string") {
            return color;
        }
        if (color.startsWith('rgb(')) {
            // 解析 RGB 值
            let [r, g, b] = color.match(/[\d]+/g).map(Number);
            // 转换为十六进制格式
            return `#${r.toString(16).padStart(2, '0')}${g.toString(16).padStart(2, '0')}${b.toString(16).padStart(2, '0')}`;
        } else if (color.startsWith('#')) {
            // 如果已经是十六进制，则直接返回
            return color;
        } else {
            // 对于颜色名称，可以使用一个映射表或者内置的 `getComputedStyle`
            const canvas = document.createElement('canvas');
            const ctx = canvas.getContext('2d');
            ctx.fillStyle = color;
            return ctx.fillStyle;
        }
    },
    /**
     * 全局替换字符串函数
     * 
     * 该函数旨在替换掉inThis字符串中所有匹配replaceThis的部分，将其替换成withThis
     * replaceAll不兼容chrome85以下浏览器,全部替换成DCTools20221228.StringReplaceAll【DUWRITER5_0-3718】
     * 
     * @param {string} replaceThis 要被替换的字符串，被视为一个模式
     * @param {string} withThis 替换后的内容
     * @param {string} inThis 需要进行替换操作的原始字符串
     * @returns {string} 替换操作完成后的字符串
     */
    StringReplaceAll: function (replaceThis, withThis, inThis) {
        // 检查所有参数确保它们都是字符串类型，否则直接返回原始字符串inThis
        if (typeof (replaceThis) != "string" || typeof (replaceThis) != "string" || typeof (replaceThis) != "string") {
            return inThis;
        }
        // 如果inThis拥有replaceAll方法，则直接使用该方法进行替换并返回结果
        if (typeof (inThis.replaceAll) == "function") {
            return inThis.replaceAll(replaceThis, withThis);
        }
        // 转义 withThis 中的 $ 字符，因为$在正则表达式中是特殊字符，在字符串替换时需避免错误的替换行为
        withThis = withThis.replace(/\$/g, "$$$$");
        // 安全地转义 replaceThis 中的所有特殊字符，以便在创建正则表达式时不会产生问题
        const escapedReplaceThis = replaceThis.replace(/([\/\,\!\\\^\$\{\}\[\]\(\)\.\*\+\?\|<>\-\&])/g, "\\$&");
        // 创建正则表达式，使用全局搜索标志"g"，以替换inThis中所有匹配replaceThis的部分
        const regex = new RegExp(escapedReplaceThis, "g");
        // 替换所有匹配项，并返回替换操作完成后的字符串
        return inThis.replace(regex, withThis);
    },
    /**
    * 创建一个防抖函数
    * 防抖函数用于限制函数调用的频率，即在一定时间内，如果函数被多次调用，只执行最后一次调用
    * 这对于防止频繁触发事件（如窗口resize、输入框keyup等）非常有用
    * 
    * @param {Function} func 要延迟执行的函数
    * @param {number} delay 延迟的时间，单位为毫秒
    * @returns {Function} 返回一个新的防抖函数
    */
    DebounceFn: function (func, delay) {
        // 用于存储定时器ID的变量，以便在需要时清除定时器
        var debounceTimerId;
        // 返回一个新的函数，该函数将在被调用时执行防抖逻辑
        return function () {
            // 保存当前上下文和参数，以便在定时器回调中使用
            const context = this;
            const args = arguments;
            // 清除任何现有的定时器，以防止之前的调用尚未到期就触发新的调用
            clearTimeout(debounceTimerId);
            // 设置一个新的定时器，定时器到期后将执行原始函数
            debounceTimerId = setTimeout(function () {
                func.apply(context, args);
            }, delay);
        };
    },
    /** 
     * 将HTML字符串转换为PNG图像
     * @param {String} HtmlString - 需要转换为PNG图像的HTML字符串
     * @param {Object} options - 渲染选项
     * @param {String} options.bgcolor - 背景颜色，任何有效的CSS颜色值。
     * @param {Number} options.width - 在渲染前应用到节点的宽度，默认宽度应为300px。
     * @param {Number} options.height - 在渲染前应用到节点的高度，如果未设置高度，否则应为指定高度的 2:1 宽高比。
     * @param {Number} options.scale - 一个乘数，在渲染前放大画布以减少模糊图像，默认为1.0。
     * @param {Function} callBackFunc - 不使用返回值可以使用的回调函数，参数为PNG图像的 data URL
     * @return {Promise} - 返回一个 Promise 对象，该对象在成功时解析为PNG图像的 data URL。
     * */
    ConvertHTMLToPNGImage: function (HtmlString, options, callBackFunc) {
        if (typeof (HtmlString) !== "string") {
            throw new Error("ConvertHTMLToPNGImage第一个参数必须是字符串。");
        }
        // 提取宽度和高度选项
        options = options || {};
        let width = options.width;
        let height = options.height;
        // 根据宽度和高度选项生成 SVG 中 foreignObject 的尺寸属性
        const foreignObjectSizing =
            (isDimensionMissing(width) ? ' width="100%"' : ` width="${width}"`) +
            (isDimensionMissing(height) ? ' height="100%"' : ` height="${height}"`);
        // 根据宽度和高度选项生成 SVG 的尺寸属性    
        const svgSizing =
            (isDimensionMissing(width) ? '' : ` width="${width}"`) +
            (isDimensionMissing(height) ? '' : ` height="${height}"`);
        // 构造 SVG 字符串，其中包含 HTML 内容    
        var svgString = `data:image/svg+xml;charset=utf-8,<svg xmlns="http://www.w3.org/2000/svg"${svgSizing}><foreignObject${foreignObjectSizing}><div xmlns="http://www.w3.org/1999/xhtml">${escapeXhtml(HtmlString)}</div></foreignObject></svg>`;
        // 将 SVG 字符串转换为图像
        return makeImage(svgString).then(function (image) {
            // 获取缩放选项，如果未设置则默认为 1
            const scale = typeof options.scale !== 'number' ? 1 : options.scale;
            // 创建新的画布
            const canvas = newCanvas(options, scale);
            const ctx = canvas.getContext('2d');
            // 禁用图像平滑处理
            ctx.msImageSmoothingEnabled = false;
            ctx.imageSmoothingEnabled = false;
            // 如果图像存在，则绘制到画布上
            if (image) {
                ctx.scale(scale, scale);
                ctx.drawImage(image, 0, 0);
            }
            return canvas;
        }).then(function (canvas) {
            // 将画布转换为数据 URL 并调用回调函数
            var dataURL = canvas.toDataURL();
            if (!!callBackFunc && typeof callBackFunc === 'function') {
                callBackFunc(dataURL);
            }
            return dataURL;
        });
        // 转义 XHTML 特殊字符
        function escapeXhtml(string) {
            return string.replace(/%/g, '%25').replace(/#/g, '%23').replace(/\n/g, '%0A');
        }
        // 检查数值是否为 NaN 或小于等于 0
        function isDimensionMissing(value) {
            return isNaN(value) || value <= 0;
        }
        // 创建图像
        function makeImage(uri) {
            return new Promise(function (resolve, reject) {
                // 创建要插入到该包装器中的 Image 元素
                const image = new Image();
                // image.crossOrigin = 'use-credentials';
                image.onload = function () {
                    if (window && window.requestAnimationFrame) {
                        // 为了绕过一个 Firefox 的 Bug（webcompat/web-bugs#119834），
                        // 我们需要等待一个额外的帧才能安全地读取图像数据。
                        window.requestAnimationFrame(function () {
                            resolve(image);
                        });
                    } else {
                        // 如果我们没有 window 或 requestAnimationFrame 函数，则立即继续。
                        resolve(image);
                    }
                };
                image.onerror = (error) => {
                    reject(error);
                };
                image.src = uri;
            });
        }
        // 创建新的画布
        function newCanvas(options, scale) {
            let width = options.width;
            let height = options.height;
            // 根据 https://www.w3.org/TR/CSS2/visudet.html#inline-replaced-width，
            // 默认宽度应为300px，如果未设置高度，否则应为指定高度的 2:1 宽高比。
            if (isDimensionMissing(width)) {
                width = isDimensionMissing(height) ? 300 : height * 2.0;
            }
            if (isDimensionMissing(height)) {
                height = width / 2.0;
            }
            const canvas = document.createElement('canvas');
            canvas.width = width * scale;
            canvas.height = height * scale;
            // 如果设置了背景色选项，则填充画布背景色
            if (options.bgcolor) {
                const ctx = canvas.getContext('2d');
                ctx.fillStyle = options.bgcolor;
                ctx.fillRect(0, 0, canvas.width, canvas.height);
            }
            return canvas;
        }
    },
    /**
     * blob.arrayBuffer的兼容性写法
     * @param {any} blob
     * @param {any} callback
     */
    blobToArrayBuffer: function (blob, callback) {
        if (blob.arrayBuffer) {
            blob.arrayBuffer().then(callback).catch(function (error) {
                console.error('Error parsing Blob to ArrayBuffer:', error);
            });
        } else {
            // 使用旧的方法兼容不支持arrayBuffer的浏览器
            let fileReader = new FileReader();
            fileReader.onload = function (event) {
                callback(event.target.result);
            };
            fileReader.onerror = function (error) {
                console.error('Error parsing Blob to ArrayBuffer:', error);
            };
            fileReader.readAsArrayBuffer(blob);
        }
    }
};

/*
* Fingerprintjs2 2.1.0 - Modern & flexible browser fingerprint library v2
* https://github.com/Valve/fingerprintjs2
* Copyright (c) 2015 Valentin Vasilyev (valentin.vasilyev@outlook.com)
* Licensed under the MIT (http://www.opensource.org/licenses/mit-license.php) license.
*
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
* AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
* IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
* ARE DISCLAIMED. IN NO EVENT SHALL VALENTIN VASILYEV BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
* THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
/* global define */
(function (name, context, definition) {
    'use strict';
    if (context != window) {
        context = window;
    }
    context[name] = definition();
    // if (typeof window !== 'undefined' && typeof define === 'function' && define.amd) { define(definition) } else if (typeof module !== 'undefined' && module.exports) { module.exports = definition() } else if (context.exports) { context.exports = definition() } else { context[name] = definition() }
})('Fingerprint2', this, function () {
    'use strict';

    /// MurmurHash3 related functions

    //
    // Given two 64bit ints (as an array of two 32bit ints) returns the two
    // added together as a 64bit int (as an array of two 32bit ints).
    //
    var x64Add = function (m, n) {
        m = [m[0] >>> 16, m[0] & 0xffff, m[1] >>> 16, m[1] & 0xffff];
        n = [n[0] >>> 16, n[0] & 0xffff, n[1] >>> 16, n[1] & 0xffff];
        var o = [0, 0, 0, 0];
        o[3] += m[3] + n[3];
        o[2] += o[3] >>> 16;
        o[3] &= 0xffff;
        o[2] += m[2] + n[2];
        o[1] += o[2] >>> 16;
        o[2] &= 0xffff;
        o[1] += m[1] + n[1];
        o[0] += o[1] >>> 16;
        o[1] &= 0xffff;
        o[0] += m[0] + n[0];
        o[0] &= 0xffff;
        return [(o[0] << 16) | o[1], (o[2] << 16) | o[3]];
    };

    //
    // Given two 64bit ints (as an array of two 32bit ints) returns the two
    // multiplied together as a 64bit int (as an array of two 32bit ints).
    //
    var x64Multiply = function (m, n) {
        m = [m[0] >>> 16, m[0] & 0xffff, m[1] >>> 16, m[1] & 0xffff];
        n = [n[0] >>> 16, n[0] & 0xffff, n[1] >>> 16, n[1] & 0xffff];
        var o = [0, 0, 0, 0];
        o[3] += m[3] * n[3];
        o[2] += o[3] >>> 16;
        o[3] &= 0xffff;
        o[2] += m[2] * n[3];
        o[1] += o[2] >>> 16;
        o[2] &= 0xffff;
        o[2] += m[3] * n[2];
        o[1] += o[2] >>> 16;
        o[2] &= 0xffff;
        o[1] += m[1] * n[3];
        o[0] += o[1] >>> 16;
        o[1] &= 0xffff;
        o[1] += m[2] * n[2];
        o[0] += o[1] >>> 16;
        o[1] &= 0xffff;
        o[1] += m[3] * n[1];
        o[0] += o[1] >>> 16;
        o[1] &= 0xffff;
        o[0] += (m[0] * n[3]) + (m[1] * n[2]) + (m[2] * n[1]) + (m[3] * n[0]);
        o[0] &= 0xffff;
        return [(o[0] << 16) | o[1], (o[2] << 16) | o[3]];
    };
    //
    // Given a 64bit int (as an array of two 32bit ints) and an int
    // representing a number of bit positions, returns the 64bit int (as an
    // array of two 32bit ints) rotated left by that number of positions.
    //
    var x64Rotl = function (m, n) {
        n %= 64;
        if (n === 32) {
            return [m[1], m[0]];
        } else if (n < 32) {
            return [(m[0] << n) | (m[1] >>> (32 - n)), (m[1] << n) | (m[0] >>> (32 - n))];
        } else {
            n -= 32;
            return [(m[1] << n) | (m[0] >>> (32 - n)), (m[0] << n) | (m[1] >>> (32 - n))];
        }
    };
    //
    // Given a 64bit int (as an array of two 32bit ints) and an int
    // representing a number of bit positions, returns the 64bit int (as an
    // array of two 32bit ints) shifted left by that number of positions.
    //
    var x64LeftShift = function (m, n) {
        n %= 64;
        if (n === 0) {
            return m;
        } else if (n < 32) {
            return [(m[0] << n) | (m[1] >>> (32 - n)), m[1] << n];
        } else {
            return [m[1] << (n - 32), 0];
        }
    };
    //
    // Given two 64bit ints (as an array of two 32bit ints) returns the two
    // xored together as a 64bit int (as an array of two 32bit ints).
    //
    var x64Xor = function (m, n) {
        return [m[0] ^ n[0], m[1] ^ n[1]];
    };
    //
    // Given a block, returns murmurHash3's final x64 mix of that block.
    // (`[0, h[0] >>> 1]` is a 33 bit unsigned right shift. This is the
    // only place where we need to right shift 64bit ints.)
    //
    var x64Fmix = function (h) {
        h = x64Xor(h, [0, h[0] >>> 1]);
        h = x64Multiply(h, [0xff51afd7, 0xed558ccd]);
        h = x64Xor(h, [0, h[0] >>> 1]);
        h = x64Multiply(h, [0xc4ceb9fe, 0x1a85ec53]);
        h = x64Xor(h, [0, h[0] >>> 1]);
        return h;
    };

    //
    // Given a string and an optional seed as an int, returns a 128 bit
    // hash using the x64 flavor of MurmurHash3, as an unsigned hex.
    //
    var x64hash128 = function (key, seed) {
        key = key || '';
        seed = seed || 0;
        var remainder = key.length % 16;
        var bytes = key.length - remainder;
        var h1 = [0, seed];
        var h2 = [0, seed];
        var k1 = [0, 0];
        var k2 = [0, 0];
        var c1 = [0x87c37b91, 0x114253d5];
        var c2 = [0x4cf5ad43, 0x2745937f];
        for (var i = 0; i < bytes; i = i + 16) {
            k1 = [((key.charCodeAt(i + 4) & 0xff)) | ((key.charCodeAt(i + 5) & 0xff) << 8) | ((key.charCodeAt(i + 6) & 0xff) << 16) | ((key.charCodeAt(i + 7) & 0xff) << 24), ((key.charCodeAt(i) & 0xff)) | ((key.charCodeAt(i + 1) & 0xff) << 8) | ((key.charCodeAt(i + 2) & 0xff) << 16) | ((key.charCodeAt(i + 3) & 0xff) << 24)];
            k2 = [((key.charCodeAt(i + 12) & 0xff)) | ((key.charCodeAt(i + 13) & 0xff) << 8) | ((key.charCodeAt(i + 14) & 0xff) << 16) | ((key.charCodeAt(i + 15) & 0xff) << 24), ((key.charCodeAt(i + 8) & 0xff)) | ((key.charCodeAt(i + 9) & 0xff) << 8) | ((key.charCodeAt(i + 10) & 0xff) << 16) | ((key.charCodeAt(i + 11) & 0xff) << 24)];
            k1 = x64Multiply(k1, c1);
            k1 = x64Rotl(k1, 31);
            k1 = x64Multiply(k1, c2);
            h1 = x64Xor(h1, k1);
            h1 = x64Rotl(h1, 27);
            h1 = x64Add(h1, h2);
            h1 = x64Add(x64Multiply(h1, [0, 5]), [0, 0x52dce729]);
            k2 = x64Multiply(k2, c2);
            k2 = x64Rotl(k2, 33);
            k2 = x64Multiply(k2, c1);
            h2 = x64Xor(h2, k2);
            h2 = x64Rotl(h2, 31);
            h2 = x64Add(h2, h1);
            h2 = x64Add(x64Multiply(h2, [0, 5]), [0, 0x38495ab5]);
        }
        k1 = [0, 0];
        k2 = [0, 0];
        switch (remainder) {
            case 15:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 14)], 48));
            // fallthrough
            case 14:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 13)], 40));
            // fallthrough
            case 13:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 12)], 32));
            // fallthrough
            case 12:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 11)], 24));
            // fallthrough
            case 11:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 10)], 16));
            // fallthrough
            case 10:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 9)], 8));
            // fallthrough
            case 9:
                k2 = x64Xor(k2, [0, key.charCodeAt(i + 8)]);
                k2 = x64Multiply(k2, c2);
                k2 = x64Rotl(k2, 33);
                k2 = x64Multiply(k2, c1);
                h2 = x64Xor(h2, k2);
            // fallthrough
            case 8:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 7)], 56));
            // fallthrough
            case 7:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 6)], 48));
            // fallthrough
            case 6:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 5)], 40));
            // fallthrough
            case 5:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 4)], 32));
            // fallthrough
            case 4:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 3)], 24));
            // fallthrough
            case 3:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 2)], 16));
            // fallthrough
            case 2:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 1)], 8));
            // fallthrough
            case 1:
                k1 = x64Xor(k1, [0, key.charCodeAt(i)]);
                k1 = x64Multiply(k1, c1);
                k1 = x64Rotl(k1, 31);
                k1 = x64Multiply(k1, c2);
                h1 = x64Xor(h1, k1);
            // fallthrough
        }
        h1 = x64Xor(h1, [0, key.length]);
        h2 = x64Xor(h2, [0, key.length]);
        h1 = x64Add(h1, h2);
        h2 = x64Add(h2, h1);
        h1 = x64Fmix(h1);
        h2 = x64Fmix(h2);
        h1 = x64Add(h1, h2);
        h2 = x64Add(h2, h1);
        return ('00000000' + (h1[0] >>> 0).toString(16)).slice(-8) + ('00000000' + (h1[1] >>> 0).toString(16)).slice(-8) + ('00000000' + (h2[0] >>> 0).toString(16)).slice(-8) + ('00000000' + (h2[1] >>> 0).toString(16)).slice(-8);
    };

    var defaultOptions = {
        preprocessor: null,
        audio: {
            timeout: 1000,
            // On iOS 11, audio context can only be used in response to user interaction.
            // We require users to explicitly enable audio fingerprinting on iOS 11.
            // See https://stackoverflow.com/questions/46363048/onaudioprocess-not-called-on-ios11#46534088
            excludeIOS11: true
        },
        fonts: {
            swfContainerId: 'fingerprintjs2',
            swfPath: 'flash/compiled/FontList.swf',
            userDefinedFonts: [],
            extendedJsFonts: false
        },
        screen: {
            // To ensure consistent fingerprints when users rotate their mobile devices
            detectScreenOrientation: true
        },
        plugins: {
            sortPluginsFor: [/palemoon/i],
            excludeIE: false
        },
        extraComponents: [],
        excludes: {
            // Unreliable on Windows, see https://github.com/Valve/fingerprintjs2/issues/375
            'enumerateDevices': true,
            // devicePixelRatio depends on browser zoom, and it's impossible to detect browser zoom
            'pixelRatio': true,
            // DNT depends on incognito mode for some browsers (Chrome) and it's impossible to detect incognito mode
            'doNotTrack': true,
            // uses js fonts already
            'fontsFlash': true
        },
        NOT_AVAILABLE: 'not available',
        ERROR: 'error',
        EXCLUDED: 'excluded'
    };

    var each = function (obj, iterator) {
        if (Array.prototype.forEach && obj.forEach === Array.prototype.forEach) {
            obj.forEach(iterator);
        } else if (obj.length === +obj.length) {
            for (var i = 0, l = obj.length; i < l; i++) {
                iterator(obj[i], i, obj);
            }
        } else {
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    iterator(obj[key], key, obj);
                }
            }
        }
    };

    var map = function (obj, iterator) {
        var results = [];
        // Not using strict equality so that this acts as a
        // shortcut to checking for `null` and `undefined`.
        if (obj == null) {
            return results;
        }
        if (Array.prototype.map && obj.map === Array.prototype.map) { return obj.map(iterator); }
        each(obj, function (value, index, list) {
            results.push(iterator(value, index, list));
        });
        return results;
    };

    var extendSoft = function (target, source) {
        if (source == null) { return target; }
        var value;
        var key;
        for (key in source) {
            value = source[key];
            if (value != null && !(Object.prototype.hasOwnProperty.call(target, key))) {
                target[key] = value;
            }
        }
        return target;
    };

    // https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/enumerateDevices
    var enumerateDevicesKey = function (done, options) {
        if (!isEnumerateDevicesSupported()) {
            return done(options.NOT_AVAILABLE);
        }
        navigator.mediaDevices.enumerateDevices().then(function (devices) {
            done(devices.map(function (device) {
                return 'id=' + device.deviceId + ';gid=' + device.groupId + ';' + device.kind + ';' + device.label;
            }));
        })
            .catch(function (error) {
                done(error);
            });
    };

    var isEnumerateDevicesSupported = function () {
        return (navigator.mediaDevices && navigator.mediaDevices.enumerateDevices);
    };
    // Inspired by and based on https://github.com/cozylife/audio-fingerprint
    var audioKey = function (done, options) {
        var audioOptions = options.audio;
        if (audioOptions.excludeIOS11 && navigator.userAgent.match(/OS 11.+Version\/11.+Safari/)) {
            // See comment for excludeUserAgent and https://stackoverflow.com/questions/46363048/onaudioprocess-not-called-on-ios11#46534088
            return done(options.EXCLUDED);
        }

        var AudioContext = window.OfflineAudioContext || window.webkitOfflineAudioContext;

        if (AudioContext == null) {
            return done(options.NOT_AVAILABLE);
        }

        var context = new AudioContext(1, 44100, 44100);

        var oscillator = context.createOscillator();
        oscillator.type = 'triangle';
        oscillator.frequency.setValueAtTime(10000, context.currentTime);

        var compressor = context.createDynamicsCompressor();
        each([
            ['threshold', -50],
            ['knee', 40],
            ['ratio', 12],
            ['reduction', -20],
            ['attack', 0],
            ['release', 0.25]
        ], function (item) {
            if (compressor[item[0]] !== undefined && typeof compressor[item[0]].setValueAtTime === 'function') {
                compressor[item[0]].setValueAtTime(item[1], context.currentTime);
            }
        });

        oscillator.connect(compressor);
        compressor.connect(context.destination);
        oscillator.start(0);
        context.startRendering();

        var audioTimeoutId = setTimeout(function () {
            console.warn('Audio fingerprint timed out. Please report bug at https://github.com/Valve/fingerprintjs2 with your user agent: "' + navigator.userAgent + '".');
            context.oncomplete = function () { };
            context = null;
            return done('audioTimeout');
        }, audioOptions.timeout);

        context.oncomplete = function (event) {
            var fingerprint;
            try {
                clearTimeout(audioTimeoutId);
                fingerprint = event.renderedBuffer.getChannelData(0)
                    .slice(4500, 5000)
                    .reduce(function (acc, val) { return acc + Math.abs(val); }, 0)
                    .toString();
                oscillator.disconnect();
                compressor.disconnect();
            } catch (error) {
                done(error);
                return;
            }
            done(fingerprint);
        };
    };
    var UserAgent = function (done) {
        done(navigator.userAgent);
    };
    var webdriver = function (done, options) {
        done(navigator.webdriver == null ? options.NOT_AVAILABLE : navigator.webdriver);
    };
    var languageKey = function (done, options) {
        done(navigator.language || navigator.userLanguage || navigator.browserLanguage || navigator.systemLanguage || options.NOT_AVAILABLE);
    };
    var colorDepthKey = function (done, options) {
        done(window.screen.colorDepth || options.NOT_AVAILABLE);
    };
    var deviceMemoryKey = function (done, options) {
        done(navigator.deviceMemory || options.NOT_AVAILABLE);
    };
    var pixelRatioKey = function (done, options) {
        done(window.devicePixelRatio || options.NOT_AVAILABLE);
    };
    var screenResolutionKey = function (done, options) {
        done(getScreenResolution(options));
    };
    var getScreenResolution = function (options) {
        var resolution = [window.screen.width, window.screen.height];
        if (options.screen.detectScreenOrientation) {
            resolution.sort().reverse();
        }
        return resolution;
    };
    var availableScreenResolutionKey = function (done, options) {
        done(getAvailableScreenResolution(options));
    };
    var getAvailableScreenResolution = function (options) {
        if (window.screen.availWidth && window.screen.availHeight) {
            var available = [window.screen.availHeight, window.screen.availWidth];
            if (options.screen.detectScreenOrientation) {
                available.sort().reverse();
            }
            return available;
        }
        // headless browsers
        return options.NOT_AVAILABLE;
    };
    var timezoneOffset = function (done) {
        done(new Date().getTimezoneOffset());
    };
    var timezone = function (done, options) {
        if (window.Intl && window.Intl.DateTimeFormat) {
            done(new window.Intl.DateTimeFormat().resolvedOptions().timeZone);
            return;
        }
        done(options.NOT_AVAILABLE);
    };
    var sessionStorageKey = function (done, options) {
        done(hasSessionStorage(options));
    };
    var localStorageKey = function (done, options) {
        done(hasLocalStorage(options));
    };
    var indexedDbKey = function (done, options) {
        done(hasIndexedDB(options));
    };
    var addBehaviorKey = function (done) {
        // body might not be defined at this point or removed programmatically
        done(!!(document.body && document.body.addBehavior));
    };
    var openDatabaseKey = function (done) {
        done(!!window.openDatabase);
    };
    var cpuClassKey = function (done, options) {
        done(getNavigatorCpuClass(options));
    };
    var platformKey = function (done, options) {
        done(getNavigatorPlatform(options));
    };
    var doNotTrackKey = function (done, options) {
        done(getDoNotTrack(options));
    };
    var canvasKey = function (done, options) {
        if (isCanvasSupported()) {
            done(getCanvasFp(options));
            return;
        }
        done(options.NOT_AVAILABLE);
    };
    var webglKey = function (done, options) {
        if (isWebGlSupported()) {
            done(getWebglFp());
            return;
        }
        done(options.NOT_AVAILABLE);
    };
    var webglVendorAndRendererKey = function (done) {
        if (isWebGlSupported()) {
            done(getWebglVendorAndRenderer());
            return;
        }
        done();
    };
    var adBlockKey = function (done) {
        done(getAdBlock());
    };
    var hasLiedLanguagesKey = function (done) {
        done(getHasLiedLanguages());
    };
    var hasLiedResolutionKey = function (done) {
        done(getHasLiedResolution());
    };
    var hasLiedOsKey = function (done) {
        done(getHasLiedOs());
    };
    var hasLiedBrowserKey = function (done) {
        done(getHasLiedBrowser());
    };
    // flash fonts (will increase fingerprinting time 20X to ~ 130-150ms)
    var flashFontsKey = function (done, options) {
        // we do flash if swfobject is loaded
        if (!hasSwfObjectLoaded()) {
            return done('swf object not loaded');
        }
        if (!hasMinFlashInstalled()) {
            return done('flash not installed');
        }
        if (!options.fonts.swfPath) {
            return done('missing options.fonts.swfPath');
        }
        loadSwfAndDetectFonts(function (fonts) {
            done(fonts);
        }, options);
    };
    // kudos to http://www.lalit.org/lab/javascript-css-font-detect/
    var jsFontsKey = function (done, options) {
        // a font will be compared against all the three default fonts.
        // and if it doesn't match all 3 then that font is not available.
        var baseFonts = ['monospace', 'sans-serif', 'serif'];

        var fontList = [
            'Andale Mono', 'Arial', 'Arial Black', 'Arial Hebrew', 'Arial MT', 'Arial Narrow', 'Arial Rounded MT Bold', 'Arial Unicode MS',
            'Bitstream Vera Sans Mono', 'Book Antiqua', 'Bookman Old Style',
            'Calibri', 'Cambria', 'Cambria Math', 'Century', 'Century Gothic', 'Century Schoolbook', 'Comic Sans', 'Comic Sans MS', 'Consolas', 'Courier', 'Courier New',
            'Geneva', 'Georgia',
            'Helvetica', 'Helvetica Neue',
            'Impact',
            'Lucida Bright', 'Lucida Calligraphy', 'Lucida Console', 'Lucida Fax', 'LUCIDA GRANDE', 'Lucida Handwriting', 'Lucida Sans', 'Lucida Sans Typewriter', 'Lucida Sans Unicode',
            'Microsoft Sans Serif', 'Monaco', 'Monotype Corsiva', 'MS Gothic', 'MS Outlook', 'MS PGothic', 'MS Reference Sans Serif', 'MS Sans Serif', 'MS Serif', 'MYRIAD', 'MYRIAD PRO',
            'Palatino', 'Palatino Linotype',
            'Segoe Print', 'Segoe Script', 'Segoe UI', 'Segoe UI Light', 'Segoe UI Semibold', 'Segoe UI Symbol',
            'Tahoma', 'Times', 'Times New Roman', 'Times New Roman PS', 'Trebuchet MS',
            'Verdana', 'Wingdings', 'Wingdings 2', 'Wingdings 3'
        ];

        if (options.fonts.extendedJsFonts) {
            var extendedFontList = [
                'Abadi MT Condensed Light', 'Academy Engraved LET', 'ADOBE CASLON PRO', 'Adobe Garamond', 'ADOBE GARAMOND PRO', 'Agency FB', 'Aharoni', 'Albertus Extra Bold', 'Albertus Medium', 'Algerian', 'Amazone BT', 'American Typewriter',
                'American Typewriter Condensed', 'AmerType Md BT', 'Andalus', 'Angsana New', 'AngsanaUPC', 'Antique Olive', 'Aparajita', 'Apple Chancery', 'Apple Color Emoji', 'Apple SD Gothic Neo', 'Arabic Typesetting', 'ARCHER',
                'ARNO PRO', 'Arrus BT', 'Aurora Cn BT', 'AvantGarde Bk BT', 'AvantGarde Md BT', 'AVENIR', 'Ayuthaya', 'Bandy', 'Bangla Sangam MN', 'Bank Gothic', 'BankGothic Md BT', 'Baskerville',
                'Baskerville Old Face', 'Batang', 'BatangChe', 'Bauer Bodoni', 'Bauhaus 93', 'Bazooka', 'Bell MT', 'Bembo', 'Benguiat Bk BT', 'Berlin Sans FB', 'Berlin Sans FB Demi', 'Bernard MT Condensed', 'BernhardFashion BT', 'BernhardMod BT', 'Big Caslon', 'BinnerD',
                'Blackadder ITC', 'BlairMdITC TT', 'Bodoni 72', 'Bodoni 72 Oldstyle', 'Bodoni 72 Smallcaps', 'Bodoni MT', 'Bodoni MT Black', 'Bodoni MT Condensed', 'Bodoni MT Poster Compressed',
                'Bookshelf Symbol 7', 'Boulder', 'Bradley Hand', 'Bradley Hand ITC', 'Bremen Bd BT', 'Britannic Bold', 'Broadway', 'Browallia New', 'BrowalliaUPC', 'Brush Script MT', 'Californian FB', 'Calisto MT', 'Calligrapher', 'Candara',
                'CaslonOpnface BT', 'Castellar', 'Centaur', 'Cezanne', 'CG Omega', 'CG Times', 'Chalkboard', 'Chalkboard SE', 'Chalkduster', 'Charlesworth', 'Charter Bd BT', 'Charter BT', 'Chaucer',
                'ChelthmITC Bk BT', 'Chiller', 'Clarendon', 'Clarendon Condensed', 'CloisterBlack BT', 'Cochin', 'Colonna MT', 'Constantia', 'Cooper Black', 'Copperplate', 'Copperplate Gothic', 'Copperplate Gothic Bold',
                'Copperplate Gothic Light', 'CopperplGoth Bd BT', 'Corbel', 'Cordia New', 'CordiaUPC', 'Cornerstone', 'Coronet', 'Cuckoo', 'Curlz MT', 'DaunPenh', 'Dauphin', 'David', 'DB LCD Temp', 'DELICIOUS', 'Denmark',
                'DFKai-SB', 'Didot', 'DilleniaUPC', 'DIN', 'DokChampa', 'Dotum', 'DotumChe', 'Ebrima', 'Edwardian Script ITC', 'Elephant', 'English 111 Vivace BT', 'Engravers MT', 'EngraversGothic BT', 'Eras Bold ITC', 'Eras Demi ITC', 'Eras Light ITC', 'Eras Medium ITC',
                'EucrosiaUPC', 'Euphemia', 'Euphemia UCAS', 'EUROSTILE', 'Exotc350 Bd BT', 'FangSong', 'Felix Titling', 'Fixedsys', 'FONTIN', 'Footlight MT Light', 'Forte',
                'FrankRuehl', 'Fransiscan', 'Freefrm721 Blk BT', 'FreesiaUPC', 'Freestyle Script', 'French Script MT', 'FrnkGothITC Bk BT', 'Fruitger', 'FRUTIGER',
                'Futura', 'Futura Bk BT', 'Futura Lt BT', 'Futura Md BT', 'Futura ZBlk BT', 'FuturaBlack BT', 'Gabriola', 'Galliard BT', 'Gautami', 'Geeza Pro', 'Geometr231 BT', 'Geometr231 Hv BT', 'Geometr231 Lt BT', 'GeoSlab 703 Lt BT',
                'GeoSlab 703 XBd BT', 'Gigi', 'Gill Sans', 'Gill Sans MT', 'Gill Sans MT Condensed', 'Gill Sans MT Ext Condensed Bold', 'Gill Sans Ultra Bold', 'Gill Sans Ultra Bold Condensed', 'Gisha', 'Gloucester MT Extra Condensed', 'GOTHAM', 'GOTHAM BOLD',
                'Goudy Old Style', 'Goudy Stout', 'GoudyHandtooled BT', 'GoudyOLSt BT', 'Gujarati Sangam MN', 'Gulim', 'GulimChe', 'Gungsuh', 'GungsuhChe', 'Gurmukhi MN', 'Haettenschweiler', 'Harlow Solid Italic', 'Harrington', 'Heather', 'Heiti SC', 'Heiti TC', 'HELV',
                'Herald', 'High Tower Text', 'Hiragino Kaku Gothic ProN', 'Hiragino Mincho ProN', 'Hoefler Text', 'Humanst 521 Cn BT', 'Humanst521 BT', 'Humanst521 Lt BT', 'Imprint MT Shadow', 'Incised901 Bd BT', 'Incised901 BT',
                'Incised901 Lt BT', 'INCONSOLATA', 'Informal Roman', 'Informal011 BT', 'INTERSTATE', 'IrisUPC', 'Iskoola Pota', 'JasmineUPC', 'Jazz LET', 'Jenson', 'Jester', 'Jokerman', 'Juice ITC', 'Kabel Bk BT', 'Kabel Ult BT', 'Kailasa', 'KaiTi', 'Kalinga', 'Kannada Sangam MN',
                'Kartika', 'Kaufmann Bd BT', 'Kaufmann BT', 'Khmer UI', 'KodchiangUPC', 'Kokila', 'Korinna BT', 'Kristen ITC', 'Krungthep', 'Kunstler Script', 'Lao UI', 'Latha', 'Leelawadee', 'Letter Gothic', 'Levenim MT', 'LilyUPC', 'Lithograph', 'Lithograph Light', 'Long Island',
                'Lydian BT', 'Magneto', 'Maiandra GD', 'Malayalam Sangam MN', 'Malgun Gothic',
                'Mangal', 'Marigold', 'Marion', 'Marker Felt', 'Market', 'Marlett', 'Matisse ITC', 'Matura MT Script Capitals', 'Meiryo', 'Meiryo UI', 'Microsoft Himalaya', 'Microsoft JhengHei', 'Microsoft New Tai Lue', 'Microsoft PhagsPa', 'Microsoft Tai Le',
                'Microsoft Uighur', 'Microsoft YaHei', 'Microsoft Yi Baiti', 'MingLiU', 'MingLiU_HKSCS', 'MingLiU_HKSCS-ExtB', 'MingLiU-ExtB', 'Minion', 'Minion Pro', 'Miriam', 'Miriam Fixed', 'Mistral', 'Modern', 'Modern No. 20', 'Mona Lisa Solid ITC TT', 'Mongolian Baiti',
                'MONO', 'MoolBoran', 'Mrs Eaves', 'MS LineDraw', 'MS Mincho', 'MS PMincho', 'MS Reference Specialty', 'MS UI Gothic', 'MT Extra', 'MUSEO', 'MV Boli',
                'Nadeem', 'Narkisim', 'NEVIS', 'News Gothic', 'News GothicMT', 'NewsGoth BT', 'Niagara Engraved', 'Niagara Solid', 'Noteworthy', 'NSimSun', 'Nyala', 'OCR A Extended', 'Old Century', 'Old English Text MT', 'Onyx', 'Onyx BT', 'OPTIMA', 'Oriya Sangam MN',
                'OSAKA', 'OzHandicraft BT', 'Palace Script MT', 'Papyrus', 'Parchment', 'Party LET', 'Pegasus', 'Perpetua', 'Perpetua Titling MT', 'PetitaBold', 'Pickwick', 'Plantagenet Cherokee', 'Playbill', 'PMingLiU', 'PMingLiU-ExtB',
                'Poor Richard', 'Poster', 'PosterBodoni BT', 'PRINCETOWN LET', 'Pristina', 'PTBarnum BT', 'Pythagoras', 'Raavi', 'Rage Italic', 'Ravie', 'Ribbon131 Bd BT', 'Rockwell', 'Rockwell Condensed', 'Rockwell Extra Bold', 'Rod', 'Roman', 'Sakkal Majalla',
                'Santa Fe LET', 'Savoye LET', 'Sceptre', 'Script', 'Script MT Bold', 'SCRIPTINA', 'Serifa', 'Serifa BT', 'Serifa Th BT', 'ShelleyVolante BT', 'Sherwood',
                'Shonar Bangla', 'Showcard Gothic', 'Shruti', 'Signboard', 'SILKSCREEN', 'SimHei', 'Simplified Arabic', 'Simplified Arabic Fixed', 'SimSun', 'SimSun-ExtB', 'Sinhala Sangam MN', 'Sketch Rockwell', 'Skia', 'Small Fonts', 'Snap ITC', 'Snell Roundhand', 'Socket',
                'Souvenir Lt BT', 'Staccato222 BT', 'Steamer', 'Stencil', 'Storybook', 'Styllo', 'Subway', 'Swis721 BlkEx BT', 'Swiss911 XCm BT', 'Sylfaen', 'Synchro LET', 'System', 'Tamil Sangam MN', 'Technical', 'Teletype', 'Telugu Sangam MN', 'Tempus Sans ITC',
                'Terminal', 'Thonburi', 'Traditional Arabic', 'Trajan', 'TRAJAN PRO', 'Tristan', 'Tubular', 'Tunga', 'Tw Cen MT', 'Tw Cen MT Condensed', 'Tw Cen MT Condensed Extra Bold',
                'TypoUpright BT', 'Unicorn', 'Univers', 'Univers CE 55 Medium', 'Univers Condensed', 'Utsaah', 'Vagabond', 'Vani', 'Vijaya', 'Viner Hand ITC', 'VisualUI', 'Vivaldi', 'Vladimir Script', 'Vrinda', 'Westminster', 'WHITNEY', 'Wide Latin',
                'ZapfEllipt BT', 'ZapfHumnst BT', 'ZapfHumnst Dm BT', 'Zapfino', 'Zurich BlkEx BT', 'Zurich Ex BT', 'ZWAdobeF'];
            fontList = fontList.concat(extendedFontList);
        }

        fontList = fontList.concat(options.fonts.userDefinedFonts);

        // remove duplicate fonts
        fontList = fontList.filter(function (font, position) {
            return fontList.indexOf(font) === position;
        });

        // we use m or w because these two characters take up the maximum width.
        // And we use a LLi so that the same matching fonts can get separated
        var testString = 'mmmmmmmmmmlli';

        // we test using 72px font size, we may use any size. I guess larger the better.
        var testSize = '72px';

        var h = document.getElementsByTagName('body')[0];

        // div to load spans for the base fonts
        var baseFontsDiv = document.createElement('div');

        // div to load spans for the fonts to detect
        var fontsDiv = document.createElement('div');

        var defaultWidth = {};
        var defaultHeight = {};

        // creates a span where the fonts will be loaded
        var createSpan = function () {
            var s = document.createElement('span');
            /*
             * We need this css as in some weird browser this
             * span elements shows up for a microSec which creates a
             * bad user experience
             */
            s.style.position = 'absolute';
            s.style.left = '-9999px';
            s.style.fontSize = testSize;

            // css font reset to reset external styles
            s.style.fontStyle = 'normal';
            s.style.fontWeight = 'normal';
            s.style.letterSpacing = 'normal';
            s.style.lineBreak = 'auto';
            s.style.lineHeight = 'normal';
            s.style.textTransform = 'none';
            s.style.textAlign = 'left';
            s.style.textDecoration = 'none';
            s.style.textShadow = 'none';
            s.style.whiteSpace = 'normal';
            s.style.wordBreak = 'normal';
            s.style.wordSpacing = 'normal';

            s.innerHTML = testString;
            return s;
        };

        // creates a span and load the font to detect and a base font for fallback
        var createSpanWithFonts = function (fontToDetect, baseFont) {
            var s = createSpan();
            s.style.fontFamily = "'" + fontToDetect + "'," + baseFont;
            return s;
        };

        // creates spans for the base fonts and adds them to baseFontsDiv
        var initializeBaseFontsSpans = function () {
            var spans = [];
            for (var index = 0, length = baseFonts.length; index < length; index++) {
                var s = createSpan();
                s.style.fontFamily = baseFonts[index];
                baseFontsDiv.appendChild(s);
                spans.push(s);
            }
            return spans;
        };

        // creates spans for the fonts to detect and adds them to fontsDiv
        var initializeFontsSpans = function () {
            var spans = {};
            for (var i = 0, l = fontList.length; i < l; i++) {
                var fontSpans = [];
                for (var j = 0, numDefaultFonts = baseFonts.length; j < numDefaultFonts; j++) {
                    var s = createSpanWithFonts(fontList[i], baseFonts[j]);
                    fontsDiv.appendChild(s);
                    fontSpans.push(s);
                }
                spans[fontList[i]] = fontSpans; // Stores {fontName : [spans for that font]}
            }
            return spans;
        };

        // checks if a font is available
        var isFontAvailable = function (fontSpans) {
            var detected = false;
            for (var i = 0; i < baseFonts.length; i++) {
                detected = (fontSpans[i].offsetWidth !== defaultWidth[baseFonts[i]] || fontSpans[i].offsetHeight !== defaultHeight[baseFonts[i]]);
                if (detected) {
                    return detected;
                }
            }
            return detected;
        };

        // create spans for base fonts
        var baseFontsSpans = initializeBaseFontsSpans();

        // add the spans to the DOM
        h.appendChild(baseFontsDiv);

        // get the default width for the three base fonts
        for (var index = 0, length = baseFonts.length; index < length; index++) {
            defaultWidth[baseFonts[index]] = baseFontsSpans[index].offsetWidth; // width for the default font
            defaultHeight[baseFonts[index]] = baseFontsSpans[index].offsetHeight; // height for the default font
        }

        // create spans for fonts to detect
        var fontsSpans = initializeFontsSpans();

        // add all the spans to the DOM
        h.appendChild(fontsDiv);

        // check available fonts
        var available = [];
        for (var i = 0, l = fontList.length; i < l; i++) {
            if (isFontAvailable(fontsSpans[fontList[i]])) {
                available.push(fontList[i]);
            }
        }

        // remove spans from DOM
        h.removeChild(fontsDiv);
        h.removeChild(baseFontsDiv);
        done(available);
    };
    var pluginsComponent = function (done, options) {
        if (isIE()) {
            if (!options.plugins.excludeIE) {
                done(getIEPlugins(options));
            } else {
                done(options.EXCLUDED);
            }
        } else {
            done(getRegularPlugins(options));
        }
    };
    var getRegularPlugins = function (options) {
        if (navigator.plugins == null) {
            return options.NOT_AVAILABLE;
        }

        var plugins = [];
        // plugins isn't defined in Node envs.
        for (var i = 0, l = navigator.plugins.length; i < l; i++) {
            if (navigator.plugins[i]) { plugins.push(navigator.plugins[i]); }
        }

        // sorting plugins only for those user agents, that we know randomize the plugins
        // every time we try to enumerate them
        if (pluginsShouldBeSorted(options)) {
            plugins = plugins.sort(function (a, b) {
                if (a.name > b.name) { return 1; }
                if (a.name < b.name) { return -1; }
                return 0;
            });
        }
        return map(plugins, function (p) {
            var mimeTypes = map(p, function (mt) {
                return [mt.type, mt.suffixes];
            });
            return [p.name, p.description, mimeTypes];
        });
    };
    var getIEPlugins = function (options) {
        var result = [];
        if ((Object.getOwnPropertyDescriptor && Object.getOwnPropertyDescriptor(window, 'ActiveXObject')) || ('ActiveXObject' in window)) {
            var names = [
                'AcroPDF.PDF', // Adobe PDF reader 7+
                'Adodb.Stream',
                'AgControl.AgControl', // Silverlight
                'DevalVRXCtrl.DevalVRXCtrl.1',
                'MacromediaFlashPaper.MacromediaFlashPaper',
                'Msxml2.DOMDocument',
                'Msxml2.XMLHTTP',
                'PDF.PdfCtrl', // Adobe PDF reader 6 and earlier, brrr
                'QuickTime.QuickTime', // QuickTime
                'QuickTimeCheckObject.QuickTimeCheck.1',
                'RealPlayer',
                'RealPlayer.RealPlayer(tm) ActiveX Control (32-bit)',
                'RealVideo.RealVideo(tm) ActiveX Control (32-bit)',
                'Scripting.Dictionary',
                'SWCtl.SWCtl', // ShockWave player
                'Shell.UIHelper',
                'ShockwaveFlash.ShockwaveFlash', // flash plugin
                'Skype.Detection',
                'TDCCtl.TDCCtl',
                'WMPlayer.OCX', // Windows media player
                'rmocx.RealPlayer G2 Control',
                'rmocx.RealPlayer G2 Control.1'
            ];
            // starting to detect plugins in IE
            result = map(names, function (name) {
                try {
                    // eslint-disable-next-line no-new
                    new window.ActiveXObject(name);
                    return name;
                } catch (e) {
                    return options.ERROR;
                }
            });
        } else {
            result.push(options.NOT_AVAILABLE);
        }
        if (navigator.plugins) {
            result = result.concat(getRegularPlugins(options));
        }
        return result;
    };
    var pluginsShouldBeSorted = function (options) {
        var should = false;
        for (var i = 0, l = options.plugins.sortPluginsFor.length; i < l; i++) {
            var re = options.plugins.sortPluginsFor[i];
            if (navigator.userAgent.match(re)) {
                should = true;
                break;
            }
        }
        return should;
    };
    var touchSupportKey = function (done) {
        done(getTouchSupport());
    };
    var hardwareConcurrencyKey = function (done, options) {
        done(getHardwareConcurrency(options));
    };
    var hasSessionStorage = function (options) {
        try {
            return !!window.sessionStorage;
        } catch (e) {
            return options.ERROR; // SecurityError when referencing it means it exists
        }
    };

    // https://bugzilla.mozilla.org/show_bug.cgi?id=781447
    var hasLocalStorage = function (options) {
        try {
            return !!window.localStorage;
        } catch (e) {
            return options.ERROR; // SecurityError when referencing it means it exists
        }
    };
    var hasIndexedDB = function (options) {
        try {
            return !!window.indexedDB;
        } catch (e) {
            return options.ERROR; // SecurityError when referencing it means it exists
        }
    };
    var getHardwareConcurrency = function (options) {
        if (navigator.hardwareConcurrency) {
            return navigator.hardwareConcurrency;
        }
        return options.NOT_AVAILABLE;
    };
    var getNavigatorCpuClass = function (options) {
        return navigator.cpuClass || options.NOT_AVAILABLE;
    };
    var getNavigatorPlatform = function (options) {
        if (navigator.platform) {
            return navigator.platform;
        } else {
            return options.NOT_AVAILABLE;
        }
    };
    var getDoNotTrack = function (options) {
        if (navigator.doNotTrack) {
            return navigator.doNotTrack;
        } else if (navigator.msDoNotTrack) {
            return navigator.msDoNotTrack;
        } else if (window.doNotTrack) {
            return window.doNotTrack;
        } else {
            return options.NOT_AVAILABLE;
        }
    };
    // This is a crude and primitive touch screen detection.
    // It's not possible to currently reliably detect the  availability of a touch screen
    // with a JS, without actually subscribing to a touch event.
    // http://www.stucox.com/blog/you-cant-detect-a-touchscreen/
    // https://github.com/Modernizr/Modernizr/issues/548
    // method returns an array of 3 values:
    // maxTouchPoints, the success or failure of creating a TouchEvent,
    // and the availability of the 'ontouchstart' property

    var getTouchSupport = function () {
        var maxTouchPoints = 0;
        var touchEvent;
        if (typeof navigator.maxTouchPoints !== 'undefined') {
            maxTouchPoints = navigator.maxTouchPoints;
        } else if (typeof navigator.msMaxTouchPoints !== 'undefined') {
            maxTouchPoints = navigator.msMaxTouchPoints;
        }
        try {
            document.createEvent('TouchEvent');
            touchEvent = true;
        } catch (_) {
            touchEvent = false;
        }
        var touchStart = 'ontouchstart' in window;
        return [maxTouchPoints, touchEvent, touchStart];
    };
    // https://www.browserleaks.com/canvas#how-does-it-work

    var getCanvasFp = function (options) {
        var result = [];
        // Very simple now, need to make it more complex (geo shapes etc)
        var canvas = document.createElement('canvas');
        canvas.width = 2000;
        canvas.height = 200;
        canvas.style.display = 'inline';
        var ctx = canvas.getContext('2d');
        // detect browser support of canvas winding
        // http://blogs.adobe.com/webplatform/2013/01/30/winding-rules-in-canvas/
        // https://github.com/Modernizr/Modernizr/blob/master/feature-detects/canvas/winding.js
        ctx.rect(0, 0, 10, 10);
        ctx.rect(2, 2, 6, 6);
        result.push('canvas winding:' + ((ctx.isPointInPath(5, 5, 'evenodd') === false) ? 'yes' : 'no'));

        ctx.textBaseline = 'alphabetic';
        ctx.fillStyle = '#f60';
        ctx.fillRect(125, 1, 62, 20);
        ctx.fillStyle = '#069';
        // https://github.com/Valve/fingerprintjs2/issues/66
        if (options.dontUseFakeFontInCanvas) {
            ctx.font = '11pt Arial';
        } else {
            ctx.font = '11pt no-real-font-123';
        }
        ctx.fillText('Cwm fjordbank glyphs vext quiz, \ud83d\ude03', 2, 15);
        ctx.fillStyle = 'rgba(102, 204, 0, 0.2)';
        ctx.font = '18pt Arial';
        ctx.fillText('Cwm fjordbank glyphs vext quiz, \ud83d\ude03', 4, 45);

        // canvas blending
        // http://blogs.adobe.com/webplatform/2013/01/28/blending-features-in-canvas/
        // http://jsfiddle.net/NDYV8/16/
        ctx.globalCompositeOperation = 'multiply';
        ctx.fillStyle = 'rgb(255,0,255)';
        ctx.beginPath();
        ctx.arc(50, 50, 50, 0, Math.PI * 2, true);
        ctx.closePath();
        ctx.fill();
        ctx.fillStyle = 'rgb(0,255,255)';
        ctx.beginPath();
        ctx.arc(100, 50, 50, 0, Math.PI * 2, true);
        ctx.closePath();
        ctx.fill();
        ctx.fillStyle = 'rgb(255,255,0)';
        ctx.beginPath();
        ctx.arc(75, 100, 50, 0, Math.PI * 2, true);
        ctx.closePath();
        ctx.fill();
        ctx.fillStyle = 'rgb(255,0,255)';
        // canvas winding
        // http://blogs.adobe.com/webplatform/2013/01/30/winding-rules-in-canvas/
        // http://jsfiddle.net/NDYV8/19/
        ctx.arc(75, 75, 75, 0, Math.PI * 2, true);
        ctx.arc(75, 75, 25, 0, Math.PI * 2, true);
        ctx.fill('evenodd');

        if (canvas.toDataURL) { result.push('canvas fp:' + canvas.toDataURL()); }
        return result;
    };
    var getWebglFp = function () {
        var gl;
        var fa2s = function (fa) {
            gl.clearColor(0.0, 0.0, 0.0, 1.0);
            gl.enable(gl.DEPTH_TEST);
            gl.depthFunc(gl.LEQUAL);
            gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
            return '[' + fa[0] + ', ' + fa[1] + ']';
        };
        var maxAnisotropy = function (gl) {
            var ext = gl.getExtension('EXT_texture_filter_anisotropic') || gl.getExtension('WEBKIT_EXT_texture_filter_anisotropic') || gl.getExtension('MOZ_EXT_texture_filter_anisotropic');
            if (ext) {
                var anisotropy = gl.getParameter(ext.MAX_TEXTURE_MAX_ANISOTROPY_EXT);
                if (anisotropy === 0) {
                    anisotropy = 2;
                }
                return anisotropy;
            } else {
                return null;
            }
        };

        gl = getWebglCanvas();
        if (!gl) { return null; }
        // WebGL fingerprinting is a combination of techniques, found in MaxMind antifraud script & Augur fingerprinting.
        // First it draws a gradient object with shaders and convers the image to the Base64 string.
        // Then it enumerates all WebGL extensions & capabilities and appends them to the Base64 string, resulting in a huge WebGL string, potentially very unique on each device
        // Since iOS supports webgl starting from version 8.1 and 8.1 runs on several graphics chips, the results may be different across ios devices, but we need to verify it.
        var result = [];
        var vShaderTemplate = 'attribute vec2 attrVertex;varying vec2 varyinTexCoordinate;uniform vec2 uniformOffset;void main(){varyinTexCoordinate=attrVertex+uniformOffset;gl_Position=vec4(attrVertex,0,1);}';
        var fShaderTemplate = 'precision mediump float;varying vec2 varyinTexCoordinate;void main() {gl_FragColor=vec4(varyinTexCoordinate,0,1);}';
        var vertexPosBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, vertexPosBuffer);
        var vertices = new Float32Array([-0.2, -0.9, 0, 0.4, -0.26, 0, 0, 0.732134444, 0]);
        gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);
        vertexPosBuffer.itemSize = 3;
        vertexPosBuffer.numItems = 3;
        var program = gl.createProgram();
        var vshader = gl.createShader(gl.VERTEX_SHADER);
        gl.shaderSource(vshader, vShaderTemplate);
        gl.compileShader(vshader);
        var fshader = gl.createShader(gl.FRAGMENT_SHADER);
        gl.shaderSource(fshader, fShaderTemplate);
        gl.compileShader(fshader);
        gl.attachShader(program, vshader);
        gl.attachShader(program, fshader);
        gl.linkProgram(program);
        gl.useProgram(program);
        program.vertexPosAttrib = gl.getAttribLocation(program, 'attrVertex');
        program.offsetUniform = gl.getUniformLocation(program, 'uniformOffset');
        gl.enableVertexAttribArray(program.vertexPosArray);
        gl.vertexAttribPointer(program.vertexPosAttrib, vertexPosBuffer.itemSize, gl.FLOAT, !1, 0, 0);
        gl.uniform2f(program.offsetUniform, 1, 1);
        gl.drawArrays(gl.TRIANGLE_STRIP, 0, vertexPosBuffer.numItems);
        try {
            result.push(gl.canvas.toDataURL());
        } catch (e) {
            /* .toDataURL may be absent or broken (blocked by extension) */
        }
        result.push('extensions:' + (gl.getSupportedExtensions() || []).join(';'));
        result.push('webgl aliased line width range:' + fa2s(gl.getParameter(gl.ALIASED_LINE_WIDTH_RANGE)));
        result.push('webgl aliased point size range:' + fa2s(gl.getParameter(gl.ALIASED_POINT_SIZE_RANGE)));
        result.push('webgl alpha bits:' + gl.getParameter(gl.ALPHA_BITS));
        result.push('webgl antialiasing:' + (gl.getContextAttributes().antialias ? 'yes' : 'no'));
        result.push('webgl blue bits:' + gl.getParameter(gl.BLUE_BITS));
        result.push('webgl depth bits:' + gl.getParameter(gl.DEPTH_BITS));
        result.push('webgl green bits:' + gl.getParameter(gl.GREEN_BITS));
        result.push('webgl max anisotropy:' + maxAnisotropy(gl));
        result.push('webgl max combined texture image units:' + gl.getParameter(gl.MAX_COMBINED_TEXTURE_IMAGE_UNITS));
        result.push('webgl max cube map texture size:' + gl.getParameter(gl.MAX_CUBE_MAP_TEXTURE_SIZE));
        result.push('webgl max fragment uniform vectors:' + gl.getParameter(gl.MAX_FRAGMENT_UNIFORM_VECTORS));
        result.push('webgl max render buffer size:' + gl.getParameter(gl.MAX_RENDERBUFFER_SIZE));
        result.push('webgl max texture image units:' + gl.getParameter(gl.MAX_TEXTURE_IMAGE_UNITS));
        result.push('webgl max texture size:' + gl.getParameter(gl.MAX_TEXTURE_SIZE));
        result.push('webgl max varying vectors:' + gl.getParameter(gl.MAX_VARYING_VECTORS));
        result.push('webgl max vertex attribs:' + gl.getParameter(gl.MAX_VERTEX_ATTRIBS));
        result.push('webgl max vertex texture image units:' + gl.getParameter(gl.MAX_VERTEX_TEXTURE_IMAGE_UNITS));
        result.push('webgl max vertex uniform vectors:' + gl.getParameter(gl.MAX_VERTEX_UNIFORM_VECTORS));
        result.push('webgl max viewport dims:' + fa2s(gl.getParameter(gl.MAX_VIEWPORT_DIMS)));
        result.push('webgl red bits:' + gl.getParameter(gl.RED_BITS));
        result.push('webgl renderer:' + gl.getParameter(gl.RENDERER));
        result.push('webgl shading language version:' + gl.getParameter(gl.SHADING_LANGUAGE_VERSION));
        result.push('webgl stencil bits:' + gl.getParameter(gl.STENCIL_BITS));
        result.push('webgl vendor:' + gl.getParameter(gl.VENDOR));
        result.push('webgl version:' + gl.getParameter(gl.VERSION));

        try {
            // Add the unmasked vendor and unmasked renderer if the debug_renderer_info extension is available
            var extensionDebugRendererInfo = gl.getExtension('WEBGL_debug_renderer_info');
            if (extensionDebugRendererInfo) {
                result.push('webgl unmasked vendor:' + gl.getParameter(extensionDebugRendererInfo.UNMASKED_VENDOR_WEBGL));
                result.push('webgl unmasked renderer:' + gl.getParameter(extensionDebugRendererInfo.UNMASKED_RENDERER_WEBGL));
            }
        } catch (e) { /* squelch */ }

        if (!gl.getShaderPrecisionFormat) {
            return result;
        }

        each(['FLOAT', 'INT'], function (numType) {
            each(['VERTEX', 'FRAGMENT'], function (shader) {
                each(['HIGH', 'MEDIUM', 'LOW'], function (numSize) {
                    each(['precision', 'rangeMin', 'rangeMax'], function (key) {
                        var format = gl.getShaderPrecisionFormat(gl[shader + '_SHADER'], gl[numSize + '_' + numType])[key];
                        if (key !== 'precision') {
                            key = 'precision ' + key;
                        }
                        var line = ['webgl ', shader.toLowerCase(), ' shader ', numSize.toLowerCase(), ' ', numType.toLowerCase(), ' ', key, ':', format].join('');
                        result.push(line);
                    });
                });
            });
        });
        return result;
    };
    var getWebglVendorAndRenderer = function () {
        /* This a subset of the WebGL fingerprint with a lot of entropy, while being reasonably browser-independent */
        try {
            var glContext = getWebglCanvas();
            var extensionDebugRendererInfo = glContext.getExtension('WEBGL_debug_renderer_info');
            return glContext.getParameter(extensionDebugRendererInfo.UNMASKED_VENDOR_WEBGL) + '~' + glContext.getParameter(extensionDebugRendererInfo.UNMASKED_RENDERER_WEBGL);
        } catch (e) {
            return null;
        }
    };
    var getAdBlock = function () {
        var ads = document.createElement('div');
        ads.innerHTML = '&nbsp;';
        ads.className = 'adsbox';
        var result = false;
        try {
            // body may not exist, that's why we need try/catch
            document.body.appendChild(ads);
            result = document.getElementsByClassName('adsbox')[0].offsetHeight === 0;
            document.body.removeChild(ads);
        } catch (e) {
            result = false;
        }
        return result;
    };
    var getHasLiedLanguages = function () {
        // We check if navigator.language is equal to the first language of navigator.languages
        // navigator.languages is undefined on IE11 (and potentially older IEs)
        if (typeof navigator.languages !== 'undefined') {
            try {
                var firstLanguages = navigator.languages[0].substr(0, 2);
                if (firstLanguages !== navigator.language.substr(0, 2)) {
                    return true;
                }
            } catch (err) {
                return true;
            }
        }
        return false;
    };
    var getHasLiedResolution = function () {
        return window.screen.width < window.screen.availWidth || window.screen.height < window.screen.availHeight;
    };
    var getHasLiedOs = function () {
        var userAgent = navigator.userAgent.toLowerCase();
        var oscpu = navigator.oscpu;
        var platform = navigator.platform.toLowerCase();
        var os;
        // We extract the OS from the user agent (respect the order of the if else if statement)
        if (userAgent.indexOf('windows phone') >= 0) {
            os = 'Windows Phone';
        } else if (userAgent.indexOf('win') >= 0) {
            os = 'Windows';
        } else if (userAgent.indexOf('android') >= 0) {
            os = 'Android';
        } else if (userAgent.indexOf('linux') >= 0 || userAgent.indexOf('cros') >= 0) {
            os = 'Linux';
        } else if (userAgent.indexOf('iphone') >= 0 || userAgent.indexOf('ipad') >= 0) {
            os = 'iOS';
        } else if (userAgent.indexOf('mac') >= 0) {
            os = 'Mac';
        } else {
            os = 'Other';
        }
        // We detect if the person uses a mobile device
        var mobileDevice = (('ontouchstart' in window) ||
            (navigator.maxTouchPoints > 0) ||
            (navigator.msMaxTouchPoints > 0));

        if (mobileDevice && os !== 'Windows Phone' && os !== 'Android' && os !== 'iOS' && os !== 'Other') {
            return true;
        }

        // We compare oscpu with the OS extracted from the UA
        if (typeof oscpu !== 'undefined') {
            oscpu = oscpu.toLowerCase();
            if (oscpu.indexOf('win') >= 0 && os !== 'Windows' && os !== 'Windows Phone') {
                return true;
            } else if (oscpu.indexOf('linux') >= 0 && os !== 'Linux' && os !== 'Android') {
                return true;
            } else if (oscpu.indexOf('mac') >= 0 && os !== 'Mac' && os !== 'iOS') {
                return true;
            } else if ((oscpu.indexOf('win') === -1 && oscpu.indexOf('linux') === -1 && oscpu.indexOf('mac') === -1) !== (os === 'Other')) {
                return true;
            }
        }

        // We compare platform with the OS extracted from the UA
        if (platform.indexOf('win') >= 0 && os !== 'Windows' && os !== 'Windows Phone') {
            return true;
        } else if ((platform.indexOf('linux') >= 0 || platform.indexOf('android') >= 0 || platform.indexOf('pike') >= 0) && os !== 'Linux' && os !== 'Android') {
            return true;
        } else if ((platform.indexOf('mac') >= 0 || platform.indexOf('ipad') >= 0 || platform.indexOf('ipod') >= 0 || platform.indexOf('iphone') >= 0) && os !== 'Mac' && os !== 'iOS') {
            return true;
        } else {
            var platformIsOther = platform.indexOf('win') < 0 &&
                platform.indexOf('linux') < 0 &&
                platform.indexOf('mac') < 0 &&
                platform.indexOf('iphone') < 0 &&
                platform.indexOf('ipad') < 0;
            if (platformIsOther !== (os === 'Other')) {
                return true;
            }
        }

        return typeof navigator.plugins === 'undefined' && os !== 'Windows' && os !== 'Windows Phone';
    };
    var getHasLiedBrowser = function () {
        var userAgent = navigator.userAgent.toLowerCase();
        var productSub = navigator.productSub;

        // we extract the browser from the user agent (respect the order of the tests)
        var browser;
        if (userAgent.indexOf('firefox') >= 0) {
            browser = 'Firefox';
        } else if (userAgent.indexOf('opera') >= 0 || userAgent.indexOf('opr') >= 0) {
            browser = 'Opera';
        } else if (userAgent.indexOf('chrome') >= 0) {
            browser = 'Chrome';
        } else if (userAgent.indexOf('safari') >= 0) {
            browser = 'Safari';
        } else if (userAgent.indexOf('trident') >= 0) {
            browser = 'Internet Explorer';
        } else {
            browser = 'Other';
        }

        if ((browser === 'Chrome' || browser === 'Safari' || browser === 'Opera') && productSub !== '20030107') {
            return true;
        }

        // eslint-disable-next-line no-eval
        var tempRes = eval.toString().length;
        if (tempRes === 37 && browser !== 'Safari' && browser !== 'Firefox' && browser !== 'Other') {
            return true;
        } else if (tempRes === 39 && browser !== 'Internet Explorer' && browser !== 'Other') {
            return true;
        } else if (tempRes === 33 && browser !== 'Chrome' && browser !== 'Opera' && browser !== 'Other') {
            return true;
        }

        // We create an error to see how it is handled
        var errFirefox;
        try {
            // eslint-disable-next-line no-throw-literal
            throw 'a';
        } catch (err) {
            try {
                err.toSource();
                errFirefox = true;
            } catch (errOfErr) {
                errFirefox = false;
            }
        }
        return errFirefox && browser !== 'Firefox' && browser !== 'Other';
    };
    var isCanvasSupported = function () {
        var elem = document.createElement('canvas');
        return !!(elem.getContext && elem.getContext('2d'));
    };
    var isWebGlSupported = function () {
        // code taken from Modernizr
        if (!isCanvasSupported()) {
            return false;
        }

        var glContext = getWebglCanvas();
        return !!window.WebGLRenderingContext && !!glContext;
    };
    var isIE = function () {
        if (navigator.appName === 'Microsoft Internet Explorer') {
            return true;
        } else if (navigator.appName === 'Netscape' && /Trident/.test(navigator.userAgent)) { // IE 11
            return true;
        }
        return false;
    };
    var hasSwfObjectLoaded = function () {
        return typeof window.swfobject !== 'undefined';
    };
    var hasMinFlashInstalled = function () {
        return window.swfobject.hasFlashPlayerVersion('9.0.0');
    };
    var addFlashDivNode = function (options) {
        var node = document.createElement('div');
        node.setAttribute('id', options.fonts.swfContainerId);
        document.body.appendChild(node);
    };
    var loadSwfAndDetectFonts = function (done, options) {
        var hiddenCallback = '___fp_swf_loaded';
        window[hiddenCallback] = function (fonts) {
            done(fonts);
        };
        var id = options.fonts.swfContainerId;
        addFlashDivNode();
        var flashvars = { onReady: hiddenCallback };
        var flashparams = { allowScriptAccess: 'always', menu: 'false' };
        window.swfobject.embedSWF(options.fonts.swfPath, id, '1', '1', '9.0.0', false, flashvars, flashparams, {});
    };
    var getWebglCanvas = function () {
        var canvas = document.createElement('canvas');
        var gl = null;
        try {
            gl = canvas.getContext('webgl') || canvas.getContext('experimental-webgl');
        } catch (e) { /* squelch */ }
        if (!gl) { gl = null; }
        return gl;
    };

    var components = [
        { key: 'userAgent', getData: UserAgent },
        { key: 'webdriver', getData: webdriver },
        { key: 'language', getData: languageKey },
        { key: 'colorDepth', getData: colorDepthKey },
        { key: 'deviceMemory', getData: deviceMemoryKey },
        { key: 'pixelRatio', getData: pixelRatioKey },
        { key: 'hardwareConcurrency', getData: hardwareConcurrencyKey },
        { key: 'screenResolution', getData: screenResolutionKey },
        { key: 'availableScreenResolution', getData: availableScreenResolutionKey },
        { key: 'timezoneOffset', getData: timezoneOffset },
        { key: 'timezone', getData: timezone },
        { key: 'sessionStorage', getData: sessionStorageKey },
        { key: 'localStorage', getData: localStorageKey },
        { key: 'indexedDb', getData: indexedDbKey },
        { key: 'addBehavior', getData: addBehaviorKey },
        { key: 'openDatabase', getData: openDatabaseKey },
        { key: 'cpuClass', getData: cpuClassKey },
        { key: 'platform', getData: platformKey },
        { key: 'doNotTrack', getData: doNotTrackKey },
        { key: 'plugins', getData: pluginsComponent },
        { key: 'canvas', getData: canvasKey },
        { key: 'webgl', getData: webglKey },
        { key: 'webglVendorAndRenderer', getData: webglVendorAndRendererKey },
        { key: 'adBlock', getData: adBlockKey },
        { key: 'hasLiedLanguages', getData: hasLiedLanguagesKey },
        { key: 'hasLiedResolution', getData: hasLiedResolutionKey },
        { key: 'hasLiedOs', getData: hasLiedOsKey },
        { key: 'hasLiedBrowser', getData: hasLiedBrowserKey },
        { key: 'touchSupport', getData: touchSupportKey },
        { key: 'fonts', getData: jsFontsKey, pauseBefore: true },
        { key: 'fontsFlash', getData: flashFontsKey, pauseBefore: true },
        { key: 'audio', getData: audioKey },
        { key: 'enumerateDevices', getData: enumerateDevicesKey }
    ];

    var Fingerprint2 = function (options) {
        throw new Error("'new Fingerprint()' is deprecated, see https://github.com/Valve/fingerprintjs2#upgrade-guide-from-182-to-200");
    };

    Fingerprint2.get = function (options, callback) {
        if (!callback) {
            callback = options;
            options = {};
        } else if (!options) {
            options = {};
        }
        extendSoft(options, defaultOptions);
        options.components = options.extraComponents.concat(components);

        var keys = {
            data: [],
            addPreprocessedComponent: function (key, value) {
                if (typeof options.preprocessor === 'function') {
                    value = options.preprocessor(key, value);
                }
                keys.data.push({ key: key, value: value });
            }
        };

        var i = -1;
        var chainComponents = function (alreadyWaited) {
            i += 1;
            if (i >= options.components.length) { // on finish
                callback(keys.data);
                return;
            }
            var component = options.components[i];

            if (options.excludes[component.key]) {
                chainComponents(false); // skip
                return;
            }

            if (!alreadyWaited && component.pauseBefore) {
                i -= 1;
                setTimeout(function () {
                    chainComponents(true);
                }, 1);
                return;
            }

            try {
                component.getData(function (value) {
                    keys.addPreprocessedComponent(component.key, value);
                    chainComponents(false);
                }, options);
            } catch (error) {
                // main body error
                keys.addPreprocessedComponent(component.key, String(error));
                chainComponents(false);
            }
        };

        chainComponents(false);
    };

    Fingerprint2.getPromise = function (options) {
        return new Promise(function (resolve, reject) {
            Fingerprint2.get(options, resolve);
        });
    };

    Fingerprint2.getV18 = function (options, callback) {
        if (callback == null) {
            callback = options;
            options = {};
        }
        return Fingerprint2.get(options, function (components) {
            var newComponents = [];
            for (var i = 0; i < components.length; i++) {
                var component = components[i];
                if (component.value === (options.NOT_AVAILABLE || 'not available')) {
                    newComponents.push({ key: component.key, value: 'unknown' });
                } else if (component.key === 'plugins') {
                    newComponents.push({
                        key: 'plugins',
                        value: map(component.value, function (p) {
                            var mimeTypes = map(p[2], function (mt) {
                                if (mt.join) { return mt.join('~'); }
                                return mt;
                            }).join(',');
                            return [p[0], p[1], mimeTypes].join('::');
                        })
                    });
                } else if (['canvas', 'webgl'].indexOf(component.key) !== -1) {
                    newComponents.push({ key: component.key, value: component.value.join('~') });
                } else if (['sessionStorage', 'localStorage', 'indexedDb', 'addBehavior', 'openDatabase'].indexOf(component.key) !== -1) {
                    if (component.value) {
                        newComponents.push({ key: component.key, value: 1 });
                    } else {
                        // skip
                        continue;
                    }
                } else {
                    if (component.value) {
                        newComponents.push(component.value.join ? { key: component.key, value: component.value.join(';') } : component);
                    } else {
                        newComponents.push({ key: component.key, value: component.value });
                    }
                }
            }
            var murmur = x64hash128(map(newComponents, function (component) { return component.value; }).join('~~~'), 31);
            callback(murmur, newComponents);
        });
    };
    //自定义参数
    Fingerprint2.getMyComponents = function (options) {
        extendSoft(options, defaultOptions);
        options.components = options.extraComponents.concat(components);
        var keys = {
            data: [],
            addPreprocessedComponent: function (key, value) {
                if (typeof options.preprocessor === 'function') {
                    value = options.preprocessor(key, value);
                }
                keys.data.push({ key: key, value: value });
            }
        };
        for (var i = 0; i < options.components.length; i++) {
            var component = options.components[i];
            if (options.excludes[component.key]) {
                continue;
            }
            try {
                component.getData(function (value) {
                    keys.addPreprocessedComponent(component.key, value);
                }, options);
            } catch (error) {
                // main body error
                keys.addPreprocessedComponent(component.key, String(error));
            }
        }
        return keys.data;
    };
    //获取指纹
    Fingerprint2.GetMyFingerprint = function () {
        var options = {
            excludes: {
                enumerateDevices: true,
                audio: true,
                canvas: true,
                webgl: true
            }
        };
        var MyFins;// 指纹
        var components = Fingerprint2.getMyComponents(options);
        var values = components.map(function (component) {
            return component.value;
        });
        MyFins = Fingerprint2.x64hash128(values.join(''), 31);// 指纹
        return MyFins;
    };
    Fingerprint2.x64hash128 = x64hash128;
    Fingerprint2.VERSION = '2.1.0';
    return Fingerprint2;
});

//对外暴露的整合写法，此处不用"object" == typeof exports ? module.exports = exports = e() : "function" == typeof define && define.amd ? define([], e) : DCTools20221228.CryptoJS = e()
(function (t, e) { DCTools20221228.CryptoJS = e(); })(DCTools20221228, function () {
    var t, e, r, i, n, o, s, c, a, h, l = l || function (t, e) { var r; if ("undefined" != typeof window && window.crypto && (r = window.crypto), !r && "undefined" != typeof window && window.msCrypto && (r = window.msCrypto), !r && "undefined" != typeof global && global.crypto && (r = global.crypto), !r && "function" == typeof require) try { r = require("crypto"); } catch (t) { } var i = function () { if (r) { if ("function" == typeof r.getRandomValues) try { return r.getRandomValues(new Uint32Array(1))[0]; } catch (t) { } if ("function" == typeof r.randomBytes) try { return r.randomBytes(4).readInt32LE(); } catch (t) { } } throw new Error("Native crypto module could not be used to get secure random number."); }, n = Object.create || function () { function t() { } return function (e) { var r; return t.prototype = e, r = new t, t.prototype = null, r; }; }(), o = {}, s = o.lib = {}, c = s.Base = { extend: function (t) { var e = n(this); return t && e.mixIn(t), e.hasOwnProperty("init") && this.init !== e.init || (e.init = function () { e.$super.init.apply(this, arguments); }), e.init.prototype = e, e.$super = this, e; }, create: function () { var t = this.extend(); return t.init.apply(t, arguments), t; }, init: function () { }, mixIn: function (t) { for (var e in t) t.hasOwnProperty(e) && (this[e] = t[e]); t.hasOwnProperty("toString") && (this.toString = t.toString); }, clone: function () { return this.init.prototype.extend(this); } }, a = s.WordArray = c.extend({ init: function (t, r) { t = this.words = t || [], this.sigBytes = r != e ? r : 4 * t.length; }, toString: function (t) { return (t || l).stringify(this); }, concat: function (t) { var e = this.words, r = t.words, i = this.sigBytes, n = t.sigBytes; if (this.clamp(), i % 4) for (var o = 0; o < n; o++) { var s = r[o >>> 2] >>> 24 - o % 4 * 8 & 255; e[i + o >>> 2] |= s << 24 - (i + o) % 4 * 8; } else for (o = 0; o < n; o += 4)e[i + o >>> 2] = r[o >>> 2]; return this.sigBytes += n, this; }, clamp: function () { var e = this.words, r = this.sigBytes; e[r >>> 2] &= 4294967295 << 32 - r % 4 * 8, e.length = t.ceil(r / 4); }, clone: function () { var t = c.clone.call(this); return t.words = this.words.slice(0), t; }, random: function (t) { for (var e = [], r = 0; r < t; r += 4)e.push(i()); return new a.init(e, t); } }), h = o.enc = {}, l = h.Hex = { stringify: function (t) { for (var e = t.words, r = t.sigBytes, i = [], n = 0; n < r; n++) { var o = e[n >>> 2] >>> 24 - n % 4 * 8 & 255; i.push((o >>> 4).toString(16)), i.push((15 & o).toString(16)); } return i.join(""); }, parse: function (t) { for (var e = t.length, r = [], i = 0; i < e; i += 2)r[i >>> 3] |= parseInt(t.substr(i, 2), 16) << 24 - i % 8 * 4; return new a.init(r, e / 2); } }, f = h.Latin1 = { stringify: function (t) { for (var e = t.words, r = t.sigBytes, i = [], n = 0; n < r; n++) { var o = e[n >>> 2] >>> 24 - n % 4 * 8 & 255; i.push(String.fromCharCode(o)); } return i.join(""); }, parse: function (t) { for (var e = t.length, r = [], i = 0; i < e; i++)r[i >>> 2] |= (255 & t.charCodeAt(i)) << 24 - i % 4 * 8; return new a.init(r, e); } }, u = h.Utf8 = { stringify: function (t) { try { return decodeURIComponent(escape(f.stringify(t))); } catch (t) { throw new Error("Malformed UTF-8 data"); } }, parse: function (t) { return f.parse(unescape(encodeURIComponent(t))); } }, d = s.BufferedBlockAlgorithm = c.extend({ reset: function () { this._data = new a.init, this._nDataBytes = 0; }, _append: function (t) { "string" == typeof t && (t = u.parse(t)), this._data.concat(t), this._nDataBytes += t.sigBytes; }, _process: function (e) { var r, i = this._data, n = i.words, o = i.sigBytes, s = this.blockSize, c = 4 * s, h = o / c; h = e ? t.ceil(h) : t.max((0 | h) - this._minBufferSize, 0); var l = h * s, f = t.min(4 * l, o); if (l) { for (var u = 0; u < l; u += s)this._doProcessBlock(n, u); r = n.splice(0, l), i.sigBytes -= f; } return new a.init(r, f); }, clone: function () { var t = c.clone.call(this); return t._data = this._data.clone(), t; }, _minBufferSize: 0 }), p = (s.Hasher = d.extend({ cfg: c.extend(), init: function (t) { this.cfg = this.cfg.extend(t), this.reset(); }, reset: function () { d.reset.call(this), this._doReset(); }, update: function (t) { return this._append(t), this._process(), this; }, finalize: function (t) { t && this._append(t); var e = this._doFinalize(); return e; }, blockSize: 16, _createHelper: function (t) { return function (e, r) { return new t.init(r).finalize(e); }; }, _createHmacHelper: function (t) { return function (e, r) { return new p.HMAC.init(t, r).finalize(e); }; } }), o.algo = {}); return o; }(Math); return function () { function t(t, e, r) { for (var n = [], o = 0, s = 0; s < e; s++)if (s % 4) { var c = r[t.charCodeAt(s - 1)] << s % 4 * 2, a = r[t.charCodeAt(s)] >>> 6 - s % 4 * 2, h = c | a; n[o >>> 2] |= h << 24 - o % 4 * 8, o++; } return i.create(n, o); } var e = l, r = e.lib, i = r.WordArray, n = e.enc; n.Base64 = { stringify: function (t) { var e = t.words, r = t.sigBytes, i = this._map; t.clamp(); for (var n = [], o = 0; o < r; o += 3)for (var s = e[o >>> 2] >>> 24 - o % 4 * 8 & 255, c = e[o + 1 >>> 2] >>> 24 - (o + 1) % 4 * 8 & 255, a = e[o + 2 >>> 2] >>> 24 - (o + 2) % 4 * 8 & 255, h = s << 16 | c << 8 | a, l = 0; l < 4 && o + .75 * l < r; l++)n.push(i.charAt(h >>> 6 * (3 - l) & 63)); var f = i.charAt(64); if (f) for (; n.length % 4;)n.push(f); return n.join(""); }, parse: function (e) { var r = e.length, i = this._map, n = this._reverseMap; if (!n) { n = this._reverseMap = []; for (var o = 0; o < i.length; o++)n[i.charCodeAt(o)] = o; } var s = i.charAt(64); if (s) { var c = e.indexOf(s); -1 !== c && (r = c); } return t(e, r, n); }, _map: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=" }; }(), function (t) { function e(t, e, r, i, n, o, s) { var c = t + (e & r | ~e & i) + n + s; return (c << o | c >>> 32 - o) + e; } function r(t, e, r, i, n, o, s) { var c = t + (e & i | r & ~i) + n + s; return (c << o | c >>> 32 - o) + e; } function i(t, e, r, i, n, o, s) { var c = t + (e ^ r ^ i) + n + s; return (c << o | c >>> 32 - o) + e; } function n(t, e, r, i, n, o, s) { var c = t + (r ^ (e | ~i)) + n + s; return (c << o | c >>> 32 - o) + e; } var o = l, s = o.lib, c = s.WordArray, a = s.Hasher, h = o.algo, f = []; (function () { for (var e = 0; e < 64; e++)f[e] = 4294967296 * t.abs(t.sin(e + 1)) | 0; })(); var u = h.MD5 = a.extend({ _doReset: function () { this._hash = new c.init([1732584193, 4023233417, 2562383102, 271733878]); }, _doProcessBlock: function (t, o) { for (var s = 0; s < 16; s++) { var c = o + s, a = t[c]; t[c] = 16711935 & (a << 8 | a >>> 24) | 4278255360 & (a << 24 | a >>> 8); } var h = this._hash.words, l = t[o + 0], u = t[o + 1], d = t[o + 2], p = t[o + 3], _ = t[o + 4], v = t[o + 5], y = t[o + 6], g = t[o + 7], B = t[o + 8], w = t[o + 9], k = t[o + 10], S = t[o + 11], m = t[o + 12], x = t[o + 13], b = t[o + 14], H = t[o + 15], A = h[0], z = h[1], C = h[2], D = h[3]; A = e(A, z, C, D, l, 7, f[0]), D = e(D, A, z, C, u, 12, f[1]), C = e(C, D, A, z, d, 17, f[2]), z = e(z, C, D, A, p, 22, f[3]), A = e(A, z, C, D, _, 7, f[4]), D = e(D, A, z, C, v, 12, f[5]), C = e(C, D, A, z, y, 17, f[6]), z = e(z, C, D, A, g, 22, f[7]), A = e(A, z, C, D, B, 7, f[8]), D = e(D, A, z, C, w, 12, f[9]), C = e(C, D, A, z, k, 17, f[10]), z = e(z, C, D, A, S, 22, f[11]), A = e(A, z, C, D, m, 7, f[12]), D = e(D, A, z, C, x, 12, f[13]), C = e(C, D, A, z, b, 17, f[14]), z = e(z, C, D, A, H, 22, f[15]), A = r(A, z, C, D, u, 5, f[16]), D = r(D, A, z, C, y, 9, f[17]), C = r(C, D, A, z, S, 14, f[18]), z = r(z, C, D, A, l, 20, f[19]), A = r(A, z, C, D, v, 5, f[20]), D = r(D, A, z, C, k, 9, f[21]), C = r(C, D, A, z, H, 14, f[22]), z = r(z, C, D, A, _, 20, f[23]), A = r(A, z, C, D, w, 5, f[24]), D = r(D, A, z, C, b, 9, f[25]), C = r(C, D, A, z, p, 14, f[26]), z = r(z, C, D, A, B, 20, f[27]), A = r(A, z, C, D, x, 5, f[28]), D = r(D, A, z, C, d, 9, f[29]), C = r(C, D, A, z, g, 14, f[30]), z = r(z, C, D, A, m, 20, f[31]), A = i(A, z, C, D, v, 4, f[32]), D = i(D, A, z, C, B, 11, f[33]), C = i(C, D, A, z, S, 16, f[34]), z = i(z, C, D, A, b, 23, f[35]), A = i(A, z, C, D, u, 4, f[36]), D = i(D, A, z, C, _, 11, f[37]), C = i(C, D, A, z, g, 16, f[38]), z = i(z, C, D, A, k, 23, f[39]), A = i(A, z, C, D, x, 4, f[40]), D = i(D, A, z, C, l, 11, f[41]), C = i(C, D, A, z, p, 16, f[42]), z = i(z, C, D, A, y, 23, f[43]), A = i(A, z, C, D, w, 4, f[44]), D = i(D, A, z, C, m, 11, f[45]), C = i(C, D, A, z, H, 16, f[46]), z = i(z, C, D, A, d, 23, f[47]), A = n(A, z, C, D, l, 6, f[48]), D = n(D, A, z, C, g, 10, f[49]), C = n(C, D, A, z, b, 15, f[50]), z = n(z, C, D, A, v, 21, f[51]), A = n(A, z, C, D, m, 6, f[52]), D = n(D, A, z, C, p, 10, f[53]), C = n(C, D, A, z, k, 15, f[54]), z = n(z, C, D, A, u, 21, f[55]), A = n(A, z, C, D, B, 6, f[56]), D = n(D, A, z, C, H, 10, f[57]), C = n(C, D, A, z, y, 15, f[58]), z = n(z, C, D, A, x, 21, f[59]), A = n(A, z, C, D, _, 6, f[60]), D = n(D, A, z, C, S, 10, f[61]), C = n(C, D, A, z, d, 15, f[62]), z = n(z, C, D, A, w, 21, f[63]), h[0] = h[0] + A | 0, h[1] = h[1] + z | 0, h[2] = h[2] + C | 0, h[3] = h[3] + D | 0; }, _doFinalize: function () { var e = this._data, r = e.words, i = 8 * this._nDataBytes, n = 8 * e.sigBytes; r[n >>> 5] |= 128 << 24 - n % 32; var o = t.floor(i / 4294967296), s = i; r[15 + (n + 64 >>> 9 << 4)] = 16711935 & (o << 8 | o >>> 24) | 4278255360 & (o << 24 | o >>> 8), r[14 + (n + 64 >>> 9 << 4)] = 16711935 & (s << 8 | s >>> 24) | 4278255360 & (s << 24 | s >>> 8), e.sigBytes = 4 * (r.length + 1), this._process(); for (var c = this._hash, a = c.words, h = 0; h < 4; h++) { var l = a[h]; a[h] = 16711935 & (l << 8 | l >>> 24) | 4278255360 & (l << 24 | l >>> 8); } return c; }, clone: function () { var t = a.clone.call(this); return t._hash = this._hash.clone(), t; } }); o.MD5 = a._createHelper(u), o.HmacMD5 = a._createHmacHelper(u); }(Math), t = l, e = t.lib, r = e.WordArray, i = e.Hasher, n = t.algo, o = [], s = n.SHA1 = i.extend({ _doReset: function () { this._hash = new r.init([1732584193, 4023233417, 2562383102, 271733878, 3285377520]); }, _doProcessBlock: function (t, e) { for (var r = this._hash.words, i = r[0], n = r[1], s = r[2], c = r[3], a = r[4], h = 0; h < 80; h++) { if (h < 16) o[h] = 0 | t[e + h]; else { var l = o[h - 3] ^ o[h - 8] ^ o[h - 14] ^ o[h - 16]; o[h] = l << 1 | l >>> 31; } var f = (i << 5 | i >>> 27) + a + o[h]; f += h < 20 ? 1518500249 + (n & s | ~n & c) : h < 40 ? 1859775393 + (n ^ s ^ c) : h < 60 ? (n & s | n & c | s & c) - 1894007588 : (n ^ s ^ c) - 899497514, a = c, c = s, s = n << 30 | n >>> 2, n = i, i = f; } r[0] = r[0] + i | 0, r[1] = r[1] + n | 0, r[2] = r[2] + s | 0, r[3] = r[3] + c | 0, r[4] = r[4] + a | 0; }, _doFinalize: function () { var t = this._data, e = t.words, r = 8 * this._nDataBytes, i = 8 * t.sigBytes; return e[i >>> 5] |= 128 << 24 - i % 32, e[14 + (i + 64 >>> 9 << 4)] = Math.floor(r / 4294967296), e[15 + (i + 64 >>> 9 << 4)] = r, t.sigBytes = 4 * e.length, this._process(), this._hash; }, clone: function () { var t = i.clone.call(this); return t._hash = this._hash.clone(), t; } }), t.SHA1 = i._createHelper(s), t.HmacSHA1 = i._createHmacHelper(s), function (t) { var e = l, r = e.lib, i = r.WordArray, n = r.Hasher, o = e.algo, s = [], c = []; (function () { function e(e) { for (var r = t.sqrt(e), i = 2; i <= r; i++)if (!(e % i)) return !1; return !0; } function r(t) { return 4294967296 * (t - (0 | t)) | 0; } for (var i = 2, n = 0; n < 64;)e(i) && (n < 8 && (s[n] = r(t.pow(i, .5))), c[n] = r(t.pow(i, 1 / 3)), n++), i++; })(); var a = [], h = o.SHA256 = n.extend({ _doReset: function () { this._hash = new i.init(s.slice(0)); }, _doProcessBlock: function (t, e) { for (var r = this._hash.words, i = r[0], n = r[1], o = r[2], s = r[3], h = r[4], l = r[5], f = r[6], u = r[7], d = 0; d < 64; d++) { if (d < 16) a[d] = 0 | t[e + d]; else { var p = a[d - 15], _ = (p << 25 | p >>> 7) ^ (p << 14 | p >>> 18) ^ p >>> 3, v = a[d - 2], y = (v << 15 | v >>> 17) ^ (v << 13 | v >>> 19) ^ v >>> 10; a[d] = _ + a[d - 7] + y + a[d - 16]; } var g = h & l ^ ~h & f, B = i & n ^ i & o ^ n & o, w = (i << 30 | i >>> 2) ^ (i << 19 | i >>> 13) ^ (i << 10 | i >>> 22), k = (h << 26 | h >>> 6) ^ (h << 21 | h >>> 11) ^ (h << 7 | h >>> 25), S = u + k + g + c[d] + a[d], m = w + B; u = f, f = l, l = h, h = s + S | 0, s = o, o = n, n = i, i = S + m | 0; } r[0] = r[0] + i | 0, r[1] = r[1] + n | 0, r[2] = r[2] + o | 0, r[3] = r[3] + s | 0, r[4] = r[4] + h | 0, r[5] = r[5] + l | 0, r[6] = r[6] + f | 0, r[7] = r[7] + u | 0; }, _doFinalize: function () { var e = this._data, r = e.words, i = 8 * this._nDataBytes, n = 8 * e.sigBytes; return r[n >>> 5] |= 128 << 24 - n % 32, r[14 + (n + 64 >>> 9 << 4)] = t.floor(i / 4294967296), r[15 + (n + 64 >>> 9 << 4)] = i, e.sigBytes = 4 * r.length, this._process(), this._hash; }, clone: function () { var t = n.clone.call(this); return t._hash = this._hash.clone(), t; } }); e.SHA256 = n._createHelper(h), e.HmacSHA256 = n._createHmacHelper(h); }(Math), function () { function t(t) { return t << 8 & 4278255360 | t >>> 8 & 16711935; } var e = l, r = e.lib, i = r.WordArray, n = e.enc; n.Utf16 = n.Utf16BE = { stringify: function (t) { for (var e = t.words, r = t.sigBytes, i = [], n = 0; n < r; n += 2) { var o = e[n >>> 2] >>> 16 - n % 4 * 8 & 65535; i.push(String.fromCharCode(o)); } return i.join(""); }, parse: function (t) { for (var e = t.length, r = [], n = 0; n < e; n++)r[n >>> 1] |= t.charCodeAt(n) << 16 - n % 2 * 16; return i.create(r, 2 * e); } }; n.Utf16LE = { stringify: function (e) { for (var r = e.words, i = e.sigBytes, n = [], o = 0; o < i; o += 2) { var s = t(r[o >>> 2] >>> 16 - o % 4 * 8 & 65535); n.push(String.fromCharCode(s)); } return n.join(""); }, parse: function (e) { for (var r = e.length, n = [], o = 0; o < r; o++)n[o >>> 1] |= t(e.charCodeAt(o) << 16 - o % 2 * 16); return i.create(n, 2 * r); } }; }(), function () { if ("function" == typeof ArrayBuffer) { var t = l, e = t.lib, r = e.WordArray, i = r.init, n = r.init = function (t) { if (t instanceof ArrayBuffer && (t = new Uint8Array(t)), (t instanceof Int8Array || "undefined" != typeof Uint8ClampedArray && t instanceof Uint8ClampedArray || t instanceof Int16Array || t instanceof Uint16Array || t instanceof Int32Array || t instanceof Uint32Array || t instanceof Float32Array || t instanceof Float64Array) && (t = new Uint8Array(t.buffer, t.byteOffset, t.byteLength)), t instanceof Uint8Array) { for (var e = t.byteLength, r = [], n = 0; n < e; n++)r[n >>> 2] |= t[n] << 24 - n % 4 * 8; i.call(this, r, e); } else i.apply(this, arguments); }; n.prototype = r; } }(), function (t) { function e(t, e, r) { return t ^ e ^ r; } function r(t, e, r) { return t & e | ~t & r; } function i(t, e, r) { return (t | ~e) ^ r; } function n(t, e, r) { return t & r | e & ~r; } function o(t, e, r) { return t ^ (e | ~r); } function s(t, e) { return t << e | t >>> 32 - e; } var c = l, a = c.lib, h = a.WordArray, f = a.Hasher, u = c.algo, d = h.create([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 7, 4, 13, 1, 10, 6, 15, 3, 12, 0, 9, 5, 2, 14, 11, 8, 3, 10, 14, 4, 9, 15, 8, 1, 2, 7, 0, 6, 13, 11, 5, 12, 1, 9, 11, 10, 0, 8, 12, 4, 13, 3, 7, 15, 14, 5, 6, 2, 4, 0, 5, 9, 7, 12, 2, 10, 14, 1, 3, 8, 11, 6, 15, 13]), p = h.create([5, 14, 7, 0, 9, 2, 11, 4, 13, 6, 15, 8, 1, 10, 3, 12, 6, 11, 3, 7, 0, 13, 5, 10, 14, 15, 8, 12, 4, 9, 1, 2, 15, 5, 1, 3, 7, 14, 6, 9, 11, 8, 12, 2, 10, 0, 4, 13, 8, 6, 4, 1, 3, 11, 15, 0, 5, 12, 2, 13, 9, 7, 10, 14, 12, 15, 10, 4, 1, 5, 8, 7, 6, 2, 13, 14, 0, 3, 9, 11]), _ = h.create([11, 14, 15, 12, 5, 8, 7, 9, 11, 13, 14, 15, 6, 7, 9, 8, 7, 6, 8, 13, 11, 9, 7, 15, 7, 12, 15, 9, 11, 7, 13, 12, 11, 13, 6, 7, 14, 9, 13, 15, 14, 8, 13, 6, 5, 12, 7, 5, 11, 12, 14, 15, 14, 15, 9, 8, 9, 14, 5, 6, 8, 6, 5, 12, 9, 15, 5, 11, 6, 8, 13, 12, 5, 12, 13, 14, 11, 8, 5, 6]), v = h.create([8, 9, 9, 11, 13, 15, 15, 5, 7, 7, 8, 11, 14, 14, 12, 6, 9, 13, 15, 7, 12, 8, 9, 11, 7, 7, 12, 7, 6, 15, 13, 11, 9, 7, 15, 11, 8, 6, 6, 14, 12, 13, 5, 14, 13, 13, 7, 5, 15, 5, 8, 11, 14, 14, 6, 14, 6, 9, 12, 9, 12, 5, 15, 8, 8, 5, 12, 9, 12, 5, 14, 6, 8, 13, 6, 5, 15, 13, 11, 11]), y = h.create([0, 1518500249, 1859775393, 2400959708, 2840853838]), g = h.create([1352829926, 1548603684, 1836072691, 2053994217, 0]), B = u.RIPEMD160 = f.extend({ _doReset: function () { this._hash = h.create([1732584193, 4023233417, 2562383102, 271733878, 3285377520]); }, _doProcessBlock: function (t, c) { for (var a = 0; a < 16; a++) { var h = c + a, l = t[h]; t[h] = 16711935 & (l << 8 | l >>> 24) | 4278255360 & (l << 24 | l >>> 8); } var f, u, B, w, k, S, m, x, b, H, A, z = this._hash.words, C = y.words, D = g.words, E = d.words, R = p.words, M = _.words, F = v.words; S = f = z[0], m = u = z[1], x = B = z[2], b = w = z[3], H = k = z[4]; for (a = 0; a < 80; a += 1)A = f + t[c + E[a]] | 0, A += a < 16 ? e(u, B, w) + C[0] : a < 32 ? r(u, B, w) + C[1] : a < 48 ? i(u, B, w) + C[2] : a < 64 ? n(u, B, w) + C[3] : o(u, B, w) + C[4], A |= 0, A = s(A, M[a]), A = A + k | 0, f = k, k = w, w = s(B, 10), B = u, u = A, A = S + t[c + R[a]] | 0, A += a < 16 ? o(m, x, b) + D[0] : a < 32 ? n(m, x, b) + D[1] : a < 48 ? i(m, x, b) + D[2] : a < 64 ? r(m, x, b) + D[3] : e(m, x, b) + D[4], A |= 0, A = s(A, F[a]), A = A + H | 0, S = H, H = b, b = s(x, 10), x = m, m = A; A = z[1] + B + b | 0, z[1] = z[2] + w + H | 0, z[2] = z[3] + k + S | 0, z[3] = z[4] + f + m | 0, z[4] = z[0] + u + x | 0, z[0] = A; }, _doFinalize: function () { var t = this._data, e = t.words, r = 8 * this._nDataBytes, i = 8 * t.sigBytes; e[i >>> 5] |= 128 << 24 - i % 32, e[14 + (i + 64 >>> 9 << 4)] = 16711935 & (r << 8 | r >>> 24) | 4278255360 & (r << 24 | r >>> 8), t.sigBytes = 4 * (e.length + 1), this._process(); for (var n = this._hash, o = n.words, s = 0; s < 5; s++) { var c = o[s]; o[s] = 16711935 & (c << 8 | c >>> 24) | 4278255360 & (c << 24 | c >>> 8); } return n; }, clone: function () { var t = f.clone.call(this); return t._hash = this._hash.clone(), t; } }); c.RIPEMD160 = f._createHelper(B), c.HmacRIPEMD160 = f._createHmacHelper(B); }(Math), function () { var t = l, e = t.lib, r = e.Base, i = t.enc, n = i.Utf8, o = t.algo; o.HMAC = r.extend({ init: function (t, e) { t = this._hasher = new t.init, "string" == typeof e && (e = n.parse(e)); var r = t.blockSize, i = 4 * r; e.sigBytes > i && (e = t.finalize(e)), e.clamp(); for (var o = this._oKey = e.clone(), s = this._iKey = e.clone(), c = o.words, a = s.words, h = 0; h < r; h++)c[h] ^= 1549556828, a[h] ^= 909522486; o.sigBytes = s.sigBytes = i, this.reset(); }, reset: function () { var t = this._hasher; t.reset(), t.update(this._iKey); }, update: function (t) { return this._hasher.update(t), this; }, finalize: function (t) { var e = this._hasher, r = e.finalize(t); e.reset(); var i = e.finalize(this._oKey.clone().concat(r)); return i; } }); }(), function () { var t = l, e = t.lib, r = e.Base, i = e.WordArray, n = t.algo, o = n.SHA1, s = n.HMAC, c = n.PBKDF2 = r.extend({ cfg: r.extend({ keySize: 4, hasher: o, iterations: 1 }), init: function (t) { this.cfg = this.cfg.extend(t); }, compute: function (t, e) { for (var r = this.cfg, n = s.create(r.hasher, t), o = i.create(), c = i.create([1]), a = o.words, h = c.words, l = r.keySize, f = r.iterations; a.length < l;) { var u = n.update(e).finalize(c); n.reset(); for (var d = u.words, p = d.length, _ = u, v = 1; v < f; v++) { _ = n.finalize(_), n.reset(); for (var y = _.words, g = 0; g < p; g++)d[g] ^= y[g]; } o.concat(u), h[0]++; } return o.sigBytes = 4 * l, o; } }); t.PBKDF2 = function (t, e, r) { return c.create(r).compute(t, e); }; }(), function () { var t = l, e = t.lib, r = e.Base, i = e.WordArray, n = t.algo, o = n.MD5, s = n.EvpKDF = r.extend({ cfg: r.extend({ keySize: 4, hasher: o, iterations: 1 }), init: function (t) { this.cfg = this.cfg.extend(t); }, compute: function (t, e) { for (var r, n = this.cfg, o = n.hasher.create(), s = i.create(), c = s.words, a = n.keySize, h = n.iterations; c.length < a;) { r && o.update(r), r = o.update(t).finalize(e), o.reset(); for (var l = 1; l < h; l++)r = o.finalize(r), o.reset(); s.concat(r); } return s.sigBytes = 4 * a, s; } }); t.EvpKDF = function (t, e, r) { return s.create(r).compute(t, e); }; }(), function () { var t = l, e = t.lib, r = e.WordArray, i = t.algo, n = i.SHA256, o = i.SHA224 = n.extend({ _doReset: function () { this._hash = new r.init([3238371032, 914150663, 812702999, 4144912697, 4290775857, 1750603025, 1694076839, 3204075428]); }, _doFinalize: function () { var t = n._doFinalize.call(this); return t.sigBytes -= 4, t; } }); t.SHA224 = n._createHelper(o), t.HmacSHA224 = n._createHmacHelper(o); }(), function (t) { var e = l, r = e.lib, i = r.Base, n = r.WordArray, o = e.x64 = {}; o.Word = i.extend({ init: function (t, e) { this.high = t, this.low = e; } }), o.WordArray = i.extend({ init: function (e, r) { e = this.words = e || [], this.sigBytes = r != t ? r : 8 * e.length; }, toX32: function () { for (var t = this.words, e = t.length, r = [], i = 0; i < e; i++) { var o = t[i]; r.push(o.high), r.push(o.low); } return n.create(r, this.sigBytes); }, clone: function () { for (var t = i.clone.call(this), e = t.words = this.words.slice(0), r = e.length, n = 0; n < r; n++)e[n] = e[n].clone(); return t; } }); }(), function (t) { var e = l, r = e.lib, i = r.WordArray, n = r.Hasher, o = e.x64, s = o.Word, c = e.algo, a = [], h = [], f = []; (function () { for (var t = 1, e = 0, r = 0; r < 24; r++) { a[t + 5 * e] = (r + 1) * (r + 2) / 2 % 64; var i = e % 5, n = (2 * t + 3 * e) % 5; t = i, e = n; } for (t = 0; t < 5; t++)for (e = 0; e < 5; e++)h[t + 5 * e] = e + (2 * t + 3 * e) % 5 * 5; for (var o = 1, c = 0; c < 24; c++) { for (var l = 0, u = 0, d = 0; d < 7; d++) { if (1 & o) { var p = (1 << d) - 1; p < 32 ? u ^= 1 << p : l ^= 1 << p - 32; } 128 & o ? o = o << 1 ^ 113 : o <<= 1; } f[c] = s.create(l, u); } })(); var u = []; (function () { for (var t = 0; t < 25; t++)u[t] = s.create(); })(); var d = c.SHA3 = n.extend({ cfg: n.cfg.extend({ outputLength: 512 }), _doReset: function () { for (var t = this._state = [], e = 0; e < 25; e++)t[e] = new s.init; this.blockSize = (1600 - 2 * this.cfg.outputLength) / 32; }, _doProcessBlock: function (t, e) { for (var r = this._state, i = this.blockSize / 2, n = 0; n < i; n++) { var o = t[e + 2 * n], s = t[e + 2 * n + 1]; o = 16711935 & (o << 8 | o >>> 24) | 4278255360 & (o << 24 | o >>> 8), s = 16711935 & (s << 8 | s >>> 24) | 4278255360 & (s << 24 | s >>> 8); var c = r[n]; c.high ^= s, c.low ^= o; } for (var l = 0; l < 24; l++) { for (var d = 0; d < 5; d++) { for (var p = 0, _ = 0, v = 0; v < 5; v++) { c = r[d + 5 * v]; p ^= c.high, _ ^= c.low; } var y = u[d]; y.high = p, y.low = _; } for (d = 0; d < 5; d++) { var g = u[(d + 4) % 5], B = u[(d + 1) % 5], w = B.high, k = B.low; for (p = g.high ^ (w << 1 | k >>> 31), _ = g.low ^ (k << 1 | w >>> 31), v = 0; v < 5; v++) { c = r[d + 5 * v]; c.high ^= p, c.low ^= _; } } for (var S = 1; S < 25; S++) { c = r[S]; var m = c.high, x = c.low, b = a[S]; b < 32 ? (p = m << b | x >>> 32 - b, _ = x << b | m >>> 32 - b) : (p = x << b - 32 | m >>> 64 - b, _ = m << b - 32 | x >>> 64 - b); var H = u[h[S]]; H.high = p, H.low = _; } var A = u[0], z = r[0]; A.high = z.high, A.low = z.low; for (d = 0; d < 5; d++)for (v = 0; v < 5; v++) { S = d + 5 * v, c = r[S]; var C = u[S], D = u[(d + 1) % 5 + 5 * v], E = u[(d + 2) % 5 + 5 * v]; c.high = C.high ^ ~D.high & E.high, c.low = C.low ^ ~D.low & E.low; } c = r[0]; var R = f[l]; c.high ^= R.high, c.low ^= R.low; } }, _doFinalize: function () { var e = this._data, r = e.words, n = (this._nDataBytes, 8 * e.sigBytes), o = 32 * this.blockSize; r[n >>> 5] |= 1 << 24 - n % 32, r[(t.ceil((n + 1) / o) * o >>> 5) - 1] |= 128, e.sigBytes = 4 * r.length, this._process(); for (var s = this._state, c = this.cfg.outputLength / 8, a = c / 8, h = [], l = 0; l < a; l++) { var f = s[l], u = f.high, d = f.low; u = 16711935 & (u << 8 | u >>> 24) | 4278255360 & (u << 24 | u >>> 8), d = 16711935 & (d << 8 | d >>> 24) | 4278255360 & (d << 24 | d >>> 8), h.push(d), h.push(u); } return new i.init(h, c); }, clone: function () { for (var t = n.clone.call(this), e = t._state = this._state.slice(0), r = 0; r < 25; r++)e[r] = e[r].clone(); return t; } }); e.SHA3 = n._createHelper(d), e.HmacSHA3 = n._createHmacHelper(d); }(Math), function () { function t() { return o.create.apply(o, arguments); } var e = l, r = e.lib, i = r.Hasher, n = e.x64, o = n.Word, s = n.WordArray, c = e.algo, a = [t(1116352408, 3609767458), t(1899447441, 602891725), t(3049323471, 3964484399), t(3921009573, 2173295548), t(961987163, 4081628472), t(1508970993, 3053834265), t(2453635748, 2937671579), t(2870763221, 3664609560), t(3624381080, 2734883394), t(310598401, 1164996542), t(607225278, 1323610764), t(1426881987, 3590304994), t(1925078388, 4068182383), t(2162078206, 991336113), t(2614888103, 633803317), t(3248222580, 3479774868), t(3835390401, 2666613458), t(4022224774, 944711139), t(264347078, 2341262773), t(604807628, 2007800933), t(770255983, 1495990901), t(1249150122, 1856431235), t(1555081692, 3175218132), t(1996064986, 2198950837), t(2554220882, 3999719339), t(2821834349, 766784016), t(2952996808, 2566594879), t(3210313671, 3203337956), t(3336571891, 1034457026), t(3584528711, 2466948901), t(113926993, 3758326383), t(338241895, 168717936), t(666307205, 1188179964), t(773529912, 1546045734), t(1294757372, 1522805485), t(1396182291, 2643833823), t(1695183700, 2343527390), t(1986661051, 1014477480), t(2177026350, 1206759142), t(2456956037, 344077627), t(2730485921, 1290863460), t(2820302411, 3158454273), t(3259730800, 3505952657), t(3345764771, 106217008), t(3516065817, 3606008344), t(3600352804, 1432725776), t(4094571909, 1467031594), t(275423344, 851169720), t(430227734, 3100823752), t(506948616, 1363258195), t(659060556, 3750685593), t(883997877, 3785050280), t(958139571, 3318307427), t(1322822218, 3812723403), t(1537002063, 2003034995), t(1747873779, 3602036899), t(1955562222, 1575990012), t(2024104815, 1125592928), t(2227730452, 2716904306), t(2361852424, 442776044), t(2428436474, 593698344), t(2756734187, 3733110249), t(3204031479, 2999351573), t(3329325298, 3815920427), t(3391569614, 3928383900), t(3515267271, 566280711), t(3940187606, 3454069534), t(4118630271, 4000239992), t(116418474, 1914138554), t(174292421, 2731055270), t(289380356, 3203993006), t(460393269, 320620315), t(685471733, 587496836), t(852142971, 1086792851), t(1017036298, 365543100), t(1126000580, 2618297676), t(1288033470, 3409855158), t(1501505948, 4234509866), t(1607167915, 987167468), t(1816402316, 1246189591)], h = []; (function () { for (var e = 0; e < 80; e++)h[e] = t(); })(); var f = c.SHA512 = i.extend({ _doReset: function () { this._hash = new s.init([new o.init(1779033703, 4089235720), new o.init(3144134277, 2227873595), new o.init(1013904242, 4271175723), new o.init(2773480762, 1595750129), new o.init(1359893119, 2917565137), new o.init(2600822924, 725511199), new o.init(528734635, 4215389547), new o.init(1541459225, 327033209)]); }, _doProcessBlock: function (t, e) { for (var r = this._hash.words, i = r[0], n = r[1], o = r[2], s = r[3], c = r[4], l = r[5], f = r[6], u = r[7], d = i.high, p = i.low, _ = n.high, v = n.low, y = o.high, g = o.low, B = s.high, w = s.low, k = c.high, S = c.low, m = l.high, x = l.low, b = f.high, H = f.low, A = u.high, z = u.low, C = d, D = p, E = _, R = v, M = y, F = g, P = B, W = w, O = k, U = S, I = m, K = x, X = b, L = H, j = A, N = z, T = 0; T < 80; T++) { var q, Z, V = h[T]; if (T < 16) Z = V.high = 0 | t[e + 2 * T], q = V.low = 0 | t[e + 2 * T + 1]; else { var G = h[T - 15], J = G.high, $ = G.low, Q = (J >>> 1 | $ << 31) ^ (J >>> 8 | $ << 24) ^ J >>> 7, Y = ($ >>> 1 | J << 31) ^ ($ >>> 8 | J << 24) ^ ($ >>> 7 | J << 25), tt = h[T - 2], et = tt.high, rt = tt.low, it = (et >>> 19 | rt << 13) ^ (et << 3 | rt >>> 29) ^ et >>> 6, nt = (rt >>> 19 | et << 13) ^ (rt << 3 | et >>> 29) ^ (rt >>> 6 | et << 26), ot = h[T - 7], st = ot.high, ct = ot.low, at = h[T - 16], ht = at.high, lt = at.low; q = Y + ct, Z = Q + st + (q >>> 0 < Y >>> 0 ? 1 : 0), q += nt, Z = Z + it + (q >>> 0 < nt >>> 0 ? 1 : 0), q += lt, Z = Z + ht + (q >>> 0 < lt >>> 0 ? 1 : 0), V.high = Z, V.low = q; } var ft = O & I ^ ~O & X, ut = U & K ^ ~U & L, dt = C & E ^ C & M ^ E & M, pt = D & R ^ D & F ^ R & F, _t = (C >>> 28 | D << 4) ^ (C << 30 | D >>> 2) ^ (C << 25 | D >>> 7), vt = (D >>> 28 | C << 4) ^ (D << 30 | C >>> 2) ^ (D << 25 | C >>> 7), yt = (O >>> 14 | U << 18) ^ (O >>> 18 | U << 14) ^ (O << 23 | U >>> 9), gt = (U >>> 14 | O << 18) ^ (U >>> 18 | O << 14) ^ (U << 23 | O >>> 9), Bt = a[T], wt = Bt.high, kt = Bt.low, St = N + gt, mt = j + yt + (St >>> 0 < N >>> 0 ? 1 : 0), xt = (St = St + ut, mt = mt + ft + (St >>> 0 < ut >>> 0 ? 1 : 0), St = St + kt, mt = mt + wt + (St >>> 0 < kt >>> 0 ? 1 : 0), St = St + q, mt = mt + Z + (St >>> 0 < q >>> 0 ? 1 : 0), vt + pt), bt = _t + dt + (xt >>> 0 < vt >>> 0 ? 1 : 0); j = X, N = L, X = I, L = K, I = O, K = U, U = W + St | 0, O = P + mt + (U >>> 0 < W >>> 0 ? 1 : 0) | 0, P = M, W = F, M = E, F = R, E = C, R = D, D = St + xt | 0, C = mt + bt + (D >>> 0 < St >>> 0 ? 1 : 0) | 0; } p = i.low = p + D, i.high = d + C + (p >>> 0 < D >>> 0 ? 1 : 0), v = n.low = v + R, n.high = _ + E + (v >>> 0 < R >>> 0 ? 1 : 0), g = o.low = g + F, o.high = y + M + (g >>> 0 < F >>> 0 ? 1 : 0), w = s.low = w + W, s.high = B + P + (w >>> 0 < W >>> 0 ? 1 : 0), S = c.low = S + U, c.high = k + O + (S >>> 0 < U >>> 0 ? 1 : 0), x = l.low = x + K, l.high = m + I + (x >>> 0 < K >>> 0 ? 1 : 0), H = f.low = H + L, f.high = b + X + (H >>> 0 < L >>> 0 ? 1 : 0), z = u.low = z + N, u.high = A + j + (z >>> 0 < N >>> 0 ? 1 : 0); }, _doFinalize: function () { var t = this._data, e = t.words, r = 8 * this._nDataBytes, i = 8 * t.sigBytes; e[i >>> 5] |= 128 << 24 - i % 32, e[30 + (i + 128 >>> 10 << 5)] = Math.floor(r / 4294967296), e[31 + (i + 128 >>> 10 << 5)] = r, t.sigBytes = 4 * e.length, this._process(); var n = this._hash.toX32(); return n; }, clone: function () { var t = i.clone.call(this); return t._hash = this._hash.clone(), t; }, blockSize: 32 }); e.SHA512 = i._createHelper(f), e.HmacSHA512 = i._createHmacHelper(f); }(), function () { var t = l, e = t.x64, r = e.Word, i = e.WordArray, n = t.algo, o = n.SHA512, s = n.SHA384 = o.extend({ _doReset: function () { this._hash = new i.init([new r.init(3418070365, 3238371032), new r.init(1654270250, 914150663), new r.init(2438529370, 812702999), new r.init(355462360, 4144912697), new r.init(1731405415, 4290775857), new r.init(2394180231, 1750603025), new r.init(3675008525, 1694076839), new r.init(1203062813, 3204075428)]); }, _doFinalize: function () { var t = o._doFinalize.call(this); return t.sigBytes -= 16, t; } }); t.SHA384 = o._createHelper(s), t.HmacSHA384 = o._createHmacHelper(s); }(), l.lib.Cipher || function (t) { var e = l, r = e.lib, i = r.Base, n = r.WordArray, o = r.BufferedBlockAlgorithm, s = e.enc, c = (s.Utf8, s.Base64), a = e.algo, h = a.EvpKDF, f = r.Cipher = o.extend({ cfg: i.extend(), createEncryptor: function (t, e) { return this.create(this._ENC_XFORM_MODE, t, e); }, createDecryptor: function (t, e) { return this.create(this._DEC_XFORM_MODE, t, e); }, init: function (t, e, r) { this.cfg = this.cfg.extend(r), this._xformMode = t, this._key = e, this.reset(); }, reset: function () { o.reset.call(this), this._doReset(); }, process: function (t) { return this._append(t), this._process(); }, finalize: function (t) { t && this._append(t); var e = this._doFinalize(); return e; }, keySize: 4, ivSize: 4, _ENC_XFORM_MODE: 1, _DEC_XFORM_MODE: 2, _createHelper: function () { function t(t) { return "string" == typeof t ? m : w; } return function (e) { return { encrypt: function (r, i, n) { return t(i).encrypt(e, r, i, n); }, decrypt: function (r, i, n) { return t(i).decrypt(e, r, i, n); } }; }; }() }), u = (r.StreamCipher = f.extend({ _doFinalize: function () { var t = this._process(!0); return t; }, blockSize: 1 }), e.mode = {}), d = r.BlockCipherMode = i.extend({ createEncryptor: function (t, e) { return this.Encryptor.create(t, e); }, createDecryptor: function (t, e) { return this.Decryptor.create(t, e); }, init: function (t, e) { this._cipher = t, this._iv = e; } }), p = u.CBC = function () { function e(e, r, i) { var n, o = this._iv; o ? (n = o, this._iv = t) : n = this._prevBlock; for (var s = 0; s < i; s++)e[r + s] ^= n[s]; } var r = d.extend(); return r.Encryptor = r.extend({ processBlock: function (t, r) { var i = this._cipher, n = i.blockSize; e.call(this, t, r, n), i.encryptBlock(t, r), this._prevBlock = t.slice(r, r + n); } }), r.Decryptor = r.extend({ processBlock: function (t, r) { var i = this._cipher, n = i.blockSize, o = t.slice(r, r + n); i.decryptBlock(t, r), e.call(this, t, r, n), this._prevBlock = o; } }), r; }(), _ = e.pad = {}, v = _.Pkcs7 = { pad: function (t, e) { for (var r = 4 * e, i = r - t.sigBytes % r, o = i << 24 | i << 16 | i << 8 | i, s = [], c = 0; c < i; c += 4)s.push(o); var a = n.create(s, i); t.concat(a); }, unpad: function (t) { var e = 255 & t.words[t.sigBytes - 1 >>> 2]; t.sigBytes -= e; } }, y = (r.BlockCipher = f.extend({ cfg: f.cfg.extend({ mode: p, padding: v }), reset: function () { var t; f.reset.call(this); var e = this.cfg, r = e.iv, i = e.mode; this._xformMode == this._ENC_XFORM_MODE ? t = i.createEncryptor : (t = i.createDecryptor, this._minBufferSize = 1), this._mode && this._mode.__creator == t ? this._mode.init(this, r && r.words) : (this._mode = t.call(i, this, r && r.words), this._mode.__creator = t); }, _doProcessBlock: function (t, e) { this._mode.processBlock(t, e); }, _doFinalize: function () { var t, e = this.cfg.padding; return this._xformMode == this._ENC_XFORM_MODE ? (e.pad(this._data, this.blockSize), t = this._process(!0)) : (t = this._process(!0), e.unpad(t)), t; }, blockSize: 4 }), r.CipherParams = i.extend({ init: function (t) { this.mixIn(t); }, toString: function (t) { return (t || this.formatter).stringify(this); } })), g = e.format = {}, B = g.OpenSSL = { stringify: function (t) { var e, r = t.ciphertext, i = t.salt; return e = i ? n.create([1398893684, 1701076831]).concat(i).concat(r) : r, e.toString(c); }, parse: function (t) { var e, r = c.parse(t), i = r.words; return 1398893684 == i[0] && 1701076831 == i[1] && (e = n.create(i.slice(2, 4)), i.splice(0, 4), r.sigBytes -= 16), y.create({ ciphertext: r, salt: e }); } }, w = r.SerializableCipher = i.extend({ cfg: i.extend({ format: B }), encrypt: function (t, e, r, i) { i = this.cfg.extend(i); var n = t.createEncryptor(r, i), o = n.finalize(e), s = n.cfg; return y.create({ ciphertext: o, key: r, iv: s.iv, algorithm: t, mode: s.mode, padding: s.padding, blockSize: t.blockSize, formatter: i.format }); }, decrypt: function (t, e, r, i) { i = this.cfg.extend(i), e = this._parse(e, i.format); var n = t.createDecryptor(r, i).finalize(e.ciphertext); return n; }, _parse: function (t, e) { return "string" == typeof t ? e.parse(t, this) : t; } }), k = e.kdf = {}, S = k.OpenSSL = { execute: function (t, e, r, i) { i || (i = n.random(8)); var o = h.create({ keySize: e + r }).compute(t, i), s = n.create(o.words.slice(e), 4 * r); return o.sigBytes = 4 * e, y.create({ key: o, iv: s, salt: i }); } }, m = r.PasswordBasedCipher = w.extend({ cfg: w.cfg.extend({ kdf: S }), encrypt: function (t, e, r, i) { i = this.cfg.extend(i); var n = i.kdf.execute(r, t.keySize, t.ivSize); i.iv = n.iv; var o = w.encrypt.call(this, t, e, n.key, i); return o.mixIn(n), o; }, decrypt: function (t, e, r, i) { i = this.cfg.extend(i), e = this._parse(e, i.format); var n = i.kdf.execute(r, t.keySize, t.ivSize, e.salt); i.iv = n.iv; var o = w.decrypt.call(this, t, e, n.key, i); return o; } }); }(), l.mode.CFB = function () { function t(t, e, r, i) { var n, o = this._iv; o ? (n = o.slice(0), this._iv = void 0) : n = this._prevBlock, i.encryptBlock(n, 0); for (var s = 0; s < r; s++)t[e + s] ^= n[s]; } var e = l.lib.BlockCipherMode.extend(); return e.Encryptor = e.extend({ processBlock: function (e, r) { var i = this._cipher, n = i.blockSize; t.call(this, e, r, n, i), this._prevBlock = e.slice(r, r + n); } }), e.Decryptor = e.extend({ processBlock: function (e, r) { var i = this._cipher, n = i.blockSize, o = e.slice(r, r + n); t.call(this, e, r, n, i), this._prevBlock = o; } }), e; }(), l.mode.ECB = (c = l.lib.BlockCipherMode.extend(), c.Encryptor = c.extend({ processBlock: function (t, e) { this._cipher.encryptBlock(t, e); } }), c.Decryptor = c.extend({ processBlock: function (t, e) { this._cipher.decryptBlock(t, e); } }), c), l.pad.AnsiX923 = { pad: function (t, e) { var r = t.sigBytes, i = 4 * e, n = i - r % i, o = r + n - 1; t.clamp(), t.words[o >>> 2] |= n << 24 - o % 4 * 8, t.sigBytes += n; }, unpad: function (t) { var e = 255 & t.words[t.sigBytes - 1 >>> 2]; t.sigBytes -= e; } }, l.pad.Iso10126 = { pad: function (t, e) { var r = 4 * e, i = r - t.sigBytes % r; t.concat(l.lib.WordArray.random(i - 1)).concat(l.lib.WordArray.create([i << 24], 1)); }, unpad: function (t) { var e = 255 & t.words[t.sigBytes - 1 >>> 2]; t.sigBytes -= e; } }, l.pad.Iso97971 = { pad: function (t, e) { t.concat(l.lib.WordArray.create([2147483648], 1)), l.pad.ZeroPadding.pad(t, e); }, unpad: function (t) { l.pad.ZeroPadding.unpad(t), t.sigBytes--; } }, l.mode.OFB = (a = l.lib.BlockCipherMode.extend(), h = a.Encryptor = a.extend({ processBlock: function (t, e) { var r = this._cipher, i = r.blockSize, n = this._iv, o = this._keystream; n && (o = this._keystream = n.slice(0), this._iv = void 0), r.encryptBlock(o, 0); for (var s = 0; s < i; s++)t[e + s] ^= o[s]; } }), a.Decryptor = h, a), l.pad.NoPadding = { pad: function () { }, unpad: function () { } }, function (t) { var e = l, r = e.lib, i = r.CipherParams, n = e.enc, o = n.Hex, s = e.format; s.Hex = { stringify: function (t) { return t.ciphertext.toString(o); }, parse: function (t) { var e = o.parse(t); return i.create({ ciphertext: e }); } }; }(), function () { var t = l, e = t.lib, r = e.BlockCipher, i = t.algo, n = [], o = [], s = [], c = [], a = [], h = [], f = [], u = [], d = [], p = []; (function () { for (var t = [], e = 0; e < 256; e++)t[e] = e < 128 ? e << 1 : e << 1 ^ 283; var r = 0, i = 0; for (e = 0; e < 256; e++) { var l = i ^ i << 1 ^ i << 2 ^ i << 3 ^ i << 4; l = l >>> 8 ^ 255 & l ^ 99, n[r] = l, o[l] = r; var _ = t[r], v = t[_], y = t[v], g = 257 * t[l] ^ 16843008 * l; s[r] = g << 24 | g >>> 8, c[r] = g << 16 | g >>> 16, a[r] = g << 8 | g >>> 24, h[r] = g; g = 16843009 * y ^ 65537 * v ^ 257 * _ ^ 16843008 * r; f[l] = g << 24 | g >>> 8, u[l] = g << 16 | g >>> 16, d[l] = g << 8 | g >>> 24, p[l] = g, r ? (r = _ ^ t[t[t[y ^ _]]], i ^= t[t[i]]) : r = i = 1; } })(); var _ = [0, 1, 2, 4, 8, 16, 32, 64, 128, 27, 54], v = i.AES = r.extend({ _doReset: function () { if (!this._nRounds || this._keyPriorReset !== this._key) { for (var t = this._keyPriorReset = this._key, e = t.words, r = t.sigBytes / 4, i = this._nRounds = r + 6, o = 4 * (i + 1), s = this._keySchedule = [], c = 0; c < o; c++)c < r ? s[c] = e[c] : (l = s[c - 1], c % r ? r > 6 && c % r == 4 && (l = n[l >>> 24] << 24 | n[l >>> 16 & 255] << 16 | n[l >>> 8 & 255] << 8 | n[255 & l]) : (l = l << 8 | l >>> 24, l = n[l >>> 24] << 24 | n[l >>> 16 & 255] << 16 | n[l >>> 8 & 255] << 8 | n[255 & l], l ^= _[c / r | 0] << 24), s[c] = s[c - r] ^ l); for (var a = this._invKeySchedule = [], h = 0; h < o; h++) { c = o - h; if (h % 4) var l = s[c]; else l = s[c - 4]; a[h] = h < 4 || c <= 4 ? l : f[n[l >>> 24]] ^ u[n[l >>> 16 & 255]] ^ d[n[l >>> 8 & 255]] ^ p[n[255 & l]]; } } }, encryptBlock: function (t, e) { this._doCryptBlock(t, e, this._keySchedule, s, c, a, h, n); }, decryptBlock: function (t, e) { var r = t[e + 1]; t[e + 1] = t[e + 3], t[e + 3] = r, this._doCryptBlock(t, e, this._invKeySchedule, f, u, d, p, o); r = t[e + 1]; t[e + 1] = t[e + 3], t[e + 3] = r; }, _doCryptBlock: function (t, e, r, i, n, o, s, c) { for (var a = this._nRounds, h = t[e] ^ r[0], l = t[e + 1] ^ r[1], f = t[e + 2] ^ r[2], u = t[e + 3] ^ r[3], d = 4, p = 1; p < a; p++) { var _ = i[h >>> 24] ^ n[l >>> 16 & 255] ^ o[f >>> 8 & 255] ^ s[255 & u] ^ r[d++], v = i[l >>> 24] ^ n[f >>> 16 & 255] ^ o[u >>> 8 & 255] ^ s[255 & h] ^ r[d++], y = i[f >>> 24] ^ n[u >>> 16 & 255] ^ o[h >>> 8 & 255] ^ s[255 & l] ^ r[d++], g = i[u >>> 24] ^ n[h >>> 16 & 255] ^ o[l >>> 8 & 255] ^ s[255 & f] ^ r[d++]; h = _, l = v, f = y, u = g; } _ = (c[h >>> 24] << 24 | c[l >>> 16 & 255] << 16 | c[f >>> 8 & 255] << 8 | c[255 & u]) ^ r[d++], v = (c[l >>> 24] << 24 | c[f >>> 16 & 255] << 16 | c[u >>> 8 & 255] << 8 | c[255 & h]) ^ r[d++], y = (c[f >>> 24] << 24 | c[u >>> 16 & 255] << 16 | c[h >>> 8 & 255] << 8 | c[255 & l]) ^ r[d++], g = (c[u >>> 24] << 24 | c[h >>> 16 & 255] << 16 | c[l >>> 8 & 255] << 8 | c[255 & f]) ^ r[d++]; t[e] = _, t[e + 1] = v, t[e + 2] = y, t[e + 3] = g; }, keySize: 8 }); t.AES = r._createHelper(v); }(), function () {
        function t(t, e) { var r = (this._lBlock >>> t ^ this._rBlock) & e; this._rBlock ^= r, this._lBlock ^= r << t; }
        function e(t, e) { var r = (this._rBlock >>> t ^ this._lBlock) & e; this._lBlock ^= r, this._rBlock ^= r << t; } var r = l, i = r.lib, n = i.WordArray, o = i.BlockCipher, s = r.algo, c = [57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36, 63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4], a = [14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32], h = [1, 2, 4, 6, 8, 10, 12, 14, 15, 17, 19, 21, 23, 25, 27, 28], f = [{ 0: 8421888, 268435456: 32768, 536870912: 8421378, 805306368: 2, 1073741824: 512, 1342177280: 8421890, 1610612736: 8389122, 1879048192: 8388608, 2147483648: 514, 2415919104: 8389120, 2684354560: 33280, 2952790016: 8421376, 3221225472: 32770, 3489660928: 8388610, 3758096384: 0, 4026531840: 33282, 134217728: 0, 402653184: 8421890, 671088640: 33282, 939524096: 32768, 1207959552: 8421888, 1476395008: 512, 1744830464: 8421378, 2013265920: 2, 2281701376: 8389120, 2550136832: 33280, 2818572288: 8421376, 3087007744: 8389122, 3355443200: 8388610, 3623878656: 32770, 3892314112: 514, 4160749568: 8388608, 1: 32768, 268435457: 2, 536870913: 8421888, 805306369: 8388608, 1073741825: 8421378, 1342177281: 33280, 1610612737: 512, 1879048193: 8389122, 2147483649: 8421890, 2415919105: 8421376, 2684354561: 8388610, 2952790017: 33282, 3221225473: 514, 3489660929: 8389120, 3758096385: 32770, 4026531841: 0, 134217729: 8421890, 402653185: 8421376, 671088641: 8388608, 939524097: 512, 1207959553: 32768, 1476395009: 8388610, 1744830465: 2, 2013265921: 33282, 2281701377: 32770, 2550136833: 8389122, 2818572289: 514, 3087007745: 8421888, 3355443201: 8389120, 3623878657: 0, 3892314113: 33280, 4160749569: 8421378 }, { 0: 1074282512, 16777216: 16384, 33554432: 524288, 50331648: 1074266128, 67108864: 1073741840, 83886080: 1074282496, 100663296: 1073758208, 117440512: 16, 134217728: 540672, 150994944: 1073758224, 167772160: 1073741824, 184549376: 540688, 201326592: 524304, 218103808: 0, 234881024: 16400, 251658240: 1074266112, 8388608: 1073758208, 25165824: 540688, 41943040: 16, 58720256: 1073758224, 75497472: 1074282512, 92274688: 1073741824, 109051904: 524288, 125829120: 1074266128, 142606336: 524304, 159383552: 0, 176160768: 16384, 192937984: 1074266112, 209715200: 1073741840, 226492416: 540672, 243269632: 1074282496, 260046848: 16400, 268435456: 0, 285212672: 1074266128, 301989888: 1073758224, 318767104: 1074282496, 335544320: 1074266112, 352321536: 16, 369098752: 540688, 385875968: 16384, 402653184: 16400, 419430400: 524288, 436207616: 524304, 452984832: 1073741840, 469762048: 540672, 486539264: 1073758208, 503316480: 1073741824, 520093696: 1074282512, 276824064: 540688, 293601280: 524288, 310378496: 1074266112, 327155712: 16384, 343932928: 1073758208, 360710144: 1074282512, 377487360: 16, 394264576: 1073741824, 411041792: 1074282496, 427819008: 1073741840, 444596224: 1073758224, 461373440: 524304, 478150656: 0, 494927872: 16400, 511705088: 1074266128, 528482304: 540672 }, { 0: 260, 1048576: 0, 2097152: 67109120, 3145728: 65796, 4194304: 65540, 5242880: 67108868, 6291456: 67174660, 7340032: 67174400, 8388608: 67108864, 9437184: 67174656, 10485760: 65792, 11534336: 67174404, 12582912: 67109124, 13631488: 65536, 14680064: 4, 15728640: 256, 524288: 67174656, 1572864: 67174404, 2621440: 0, 3670016: 67109120, 4718592: 67108868, 5767168: 65536, 6815744: 65540, 7864320: 260, 8912896: 4, 9961472: 256, 11010048: 67174400, 12058624: 65796, 13107200: 65792, 14155776: 67109124, 15204352: 67174660, 16252928: 67108864, 16777216: 67174656, 17825792: 65540, 18874368: 65536, 19922944: 67109120, 20971520: 256, 22020096: 67174660, 23068672: 67108868, 24117248: 0, 25165824: 67109124, 26214400: 67108864, 27262976: 4, 28311552: 65792, 29360128: 67174400, 30408704: 260, 31457280: 65796, 32505856: 67174404, 17301504: 67108864, 18350080: 260, 19398656: 67174656, 20447232: 0, 21495808: 65540, 22544384: 67109120, 23592960: 256, 24641536: 67174404, 25690112: 65536, 26738688: 67174660, 27787264: 65796, 28835840: 67108868, 29884416: 67109124, 30932992: 67174400, 31981568: 4, 33030144: 65792 }, { 0: 2151682048, 65536: 2147487808, 131072: 4198464, 196608: 2151677952, 262144: 0, 327680: 4198400, 393216: 2147483712, 458752: 4194368, 524288: 2147483648, 589824: 4194304, 655360: 64, 720896: 2147487744, 786432: 2151678016, 851968: 4160, 917504: 4096, 983040: 2151682112, 32768: 2147487808, 98304: 64, 163840: 2151678016, 229376: 2147487744, 294912: 4198400, 360448: 2151682112, 425984: 0, 491520: 2151677952, 557056: 4096, 622592: 2151682048, 688128: 4194304, 753664: 4160, 819200: 2147483648, 884736: 4194368, 950272: 4198464, 1015808: 2147483712, 1048576: 4194368, 1114112: 4198400, 1179648: 2147483712, 1245184: 0, 1310720: 4160, 1376256: 2151678016, 1441792: 2151682048, 1507328: 2147487808, 1572864: 2151682112, 1638400: 2147483648, 1703936: 2151677952, 1769472: 4198464, 1835008: 2147487744, 1900544: 4194304, 1966080: 64, 2031616: 4096, 1081344: 2151677952, 1146880: 2151682112, 1212416: 0, 1277952: 4198400, 1343488: 4194368, 1409024: 2147483648, 1474560: 2147487808, 1540096: 64, 1605632: 2147483712, 1671168: 4096, 1736704: 2147487744, 1802240: 2151678016, 1867776: 4160, 1933312: 2151682048, 1998848: 4194304, 2064384: 4198464 }, { 0: 128, 4096: 17039360, 8192: 262144, 12288: 536870912, 16384: 537133184, 20480: 16777344, 24576: 553648256, 28672: 262272, 32768: 16777216, 36864: 537133056, 40960: 536871040, 45056: 553910400, 49152: 553910272, 53248: 0, 57344: 17039488, 61440: 553648128, 2048: 17039488, 6144: 553648256, 10240: 128, 14336: 17039360, 18432: 262144, 22528: 537133184, 26624: 553910272, 30720: 536870912, 34816: 537133056, 38912: 0, 43008: 553910400, 47104: 16777344, 51200: 536871040, 55296: 553648128, 59392: 16777216, 63488: 262272, 65536: 262144, 69632: 128, 73728: 536870912, 77824: 553648256, 81920: 16777344, 86016: 553910272, 90112: 537133184, 94208: 16777216, 98304: 553910400, 102400: 553648128, 106496: 17039360, 110592: 537133056, 114688: 262272, 118784: 536871040, 122880: 0, 126976: 17039488, 67584: 553648256, 71680: 16777216, 75776: 17039360, 79872: 537133184, 83968: 536870912, 88064: 17039488, 92160: 128, 96256: 553910272, 100352: 262272, 104448: 553910400, 108544: 0, 112640: 553648128, 116736: 16777344, 120832: 262144, 124928: 537133056, 129024: 536871040 }, { 0: 268435464, 256: 8192, 512: 270532608, 768: 270540808, 1024: 268443648, 1280: 2097152, 1536: 2097160, 1792: 268435456, 2048: 0, 2304: 268443656, 2560: 2105344, 2816: 8, 3072: 270532616, 3328: 2105352, 3584: 8200, 3840: 270540800, 128: 270532608, 384: 270540808, 640: 8, 896: 2097152, 1152: 2105352, 1408: 268435464, 1664: 268443648, 1920: 8200, 2176: 2097160, 2432: 8192, 2688: 268443656, 2944: 270532616, 3200: 0, 3456: 270540800, 3712: 2105344, 3968: 268435456, 4096: 268443648, 4352: 270532616, 4608: 270540808, 4864: 8200, 5120: 2097152, 5376: 268435456, 5632: 268435464, 5888: 2105344, 6144: 2105352, 6400: 0, 6656: 8, 6912: 270532608, 7168: 8192, 7424: 268443656, 7680: 270540800, 7936: 2097160, 4224: 8, 4480: 2105344, 4736: 2097152, 4992: 268435464, 5248: 268443648, 5504: 8200, 5760: 270540808, 6016: 270532608, 6272: 270540800, 6528: 270532616, 6784: 8192, 7040: 2105352, 7296: 2097160, 7552: 0, 7808: 268435456, 8064: 268443656 }, { 0: 1048576, 16: 33555457, 32: 1024, 48: 1049601, 64: 34604033, 80: 0, 96: 1, 112: 34603009, 128: 33555456, 144: 1048577, 160: 33554433, 176: 34604032, 192: 34603008, 208: 1025, 224: 1049600, 240: 33554432, 8: 34603009, 24: 0, 40: 33555457, 56: 34604032, 72: 1048576, 88: 33554433, 104: 33554432, 120: 1025, 136: 1049601, 152: 33555456, 168: 34603008, 184: 1048577, 200: 1024, 216: 34604033, 232: 1, 248: 1049600, 256: 33554432, 272: 1048576, 288: 33555457, 304: 34603009, 320: 1048577, 336: 33555456, 352: 34604032, 368: 1049601, 384: 1025, 400: 34604033, 416: 1049600, 432: 1, 448: 0, 464: 34603008, 480: 33554433, 496: 1024, 264: 1049600, 280: 33555457, 296: 34603009, 312: 1, 328: 33554432, 344: 1048576, 360: 1025, 376: 34604032, 392: 33554433, 408: 34603008, 424: 0, 440: 34604033, 456: 1049601, 472: 1024, 488: 33555456, 504: 1048577 }, { 0: 134219808, 1: 131072, 2: 134217728, 3: 32, 4: 131104, 5: 134350880, 6: 134350848, 7: 2048, 8: 134348800, 9: 134219776, 10: 133120, 11: 134348832, 12: 2080, 13: 0, 14: 134217760, 15: 133152, 2147483648: 2048, 2147483649: 134350880, 2147483650: 134219808, 2147483651: 134217728, 2147483652: 134348800, 2147483653: 133120, 2147483654: 133152, 2147483655: 32, 2147483656: 134217760, 2147483657: 2080, 2147483658: 131104, 2147483659: 134350848, 2147483660: 0, 2147483661: 134348832, 2147483662: 134219776, 2147483663: 131072, 16: 133152, 17: 134350848, 18: 32, 19: 2048, 20: 134219776, 21: 134217760, 22: 134348832, 23: 131072, 24: 0, 25: 131104, 26: 134348800, 27: 134219808, 28: 134350880, 29: 133120, 30: 2080, 31: 134217728, 2147483664: 131072, 2147483665: 2048, 2147483666: 134348832, 2147483667: 133152, 2147483668: 32, 2147483669: 134348800, 2147483670: 134217728, 2147483671: 134219808, 2147483672: 134350880, 2147483673: 134217760, 2147483674: 134219776, 2147483675: 0, 2147483676: 133120, 2147483677: 2080, 2147483678: 131104, 2147483679: 134350848 }], u = [4160749569, 528482304, 33030144, 2064384, 129024, 8064, 504, 2147483679], d = s.DES = o.extend({ _doReset: function () { for (var t = this._key, e = t.words, r = [], i = 0; i < 56; i++) { var n = c[i] - 1; r[i] = e[n >>> 5] >>> 31 - n % 32 & 1; } for (var o = this._subKeys = [], s = 0; s < 16; s++) { var l = o[s] = [], f = h[s]; for (i = 0; i < 24; i++)l[i / 6 | 0] |= r[(a[i] - 1 + f) % 28] << 31 - i % 6, l[4 + (i / 6 | 0)] |= r[28 + (a[i + 24] - 1 + f) % 28] << 31 - i % 6; l[0] = l[0] << 1 | l[0] >>> 31; for (i = 1; i < 7; i++)l[i] = l[i] >>> 4 * (i - 1) + 3; l[7] = l[7] << 5 | l[7] >>> 27; } var u = this._invSubKeys = []; for (i = 0; i < 16; i++)u[i] = o[15 - i]; }, encryptBlock: function (t, e) { this._doCryptBlock(t, e, this._subKeys); }, decryptBlock: function (t, e) { this._doCryptBlock(t, e, this._invSubKeys); }, _doCryptBlock: function (r, i, n) { this._lBlock = r[i], this._rBlock = r[i + 1], t.call(this, 4, 252645135), t.call(this, 16, 65535), e.call(this, 2, 858993459), e.call(this, 8, 16711935), t.call(this, 1, 1431655765); for (var o = 0; o < 16; o++) { for (var s = n[o], c = this._lBlock, a = this._rBlock, h = 0, l = 0; l < 8; l++)h |= f[l][((a ^ s[l]) & u[l]) >>> 0]; this._lBlock = a, this._rBlock = c ^ h; } var d = this._lBlock; this._lBlock = this._rBlock, this._rBlock = d, t.call(this, 1, 1431655765), e.call(this, 8, 16711935), e.call(this, 2, 858993459), t.call(this, 16, 65535), t.call(this, 4, 252645135), r[i] = this._lBlock, r[i + 1] = this._rBlock; }, keySize: 2, ivSize: 2, blockSize: 2 }); r.DES = o._createHelper(d); var p = s.TripleDES = o.extend({ _doReset: function () { var t = this._key, e = t.words; if (2 !== e.length && 4 !== e.length && e.length < 6) throw new Error("Invalid key length - 3DES requires the key length to be 64, 128, 192 or >192."); var r = e.slice(0, 2), i = e.length < 4 ? e.slice(0, 2) : e.slice(2, 4), o = e.length < 6 ? e.slice(0, 2) : e.slice(4, 6); this._des1 = d.createEncryptor(n.create(r)), this._des2 = d.createEncryptor(n.create(i)), this._des3 = d.createEncryptor(n.create(o)); }, encryptBlock: function (t, e) { this._des1.encryptBlock(t, e), this._des2.decryptBlock(t, e), this._des3.encryptBlock(t, e); }, decryptBlock: function (t, e) { this._des3.decryptBlock(t, e), this._des2.encryptBlock(t, e), this._des1.decryptBlock(t, e); }, keySize: 6, ivSize: 2, blockSize: 2 }); r.TripleDES = o._createHelper(p);
    }(), function () { function t() { for (var t = this._S, e = this._i, r = this._j, i = 0, n = 0; n < 4; n++) { e = (e + 1) % 256, r = (r + t[e]) % 256; var o = t[e]; t[e] = t[r], t[r] = o, i |= t[(t[e] + t[r]) % 256] << 24 - 8 * n; } return this._i = e, this._j = r, i; } var e = l, r = e.lib, i = r.StreamCipher, n = e.algo, o = n.RC4 = i.extend({ _doReset: function () { for (var t = this._key, e = t.words, r = t.sigBytes, i = this._S = [], n = 0; n < 256; n++)i[n] = n; n = 0; for (var o = 0; n < 256; n++) { var s = n % r, c = e[s >>> 2] >>> 24 - s % 4 * 8 & 255; o = (o + i[n] + c) % 256; var a = i[n]; i[n] = i[o], i[o] = a; } this._i = this._j = 0; }, _doProcessBlock: function (e, r) { e[r] ^= t.call(this); }, keySize: 8, ivSize: 0 }); e.RC4 = i._createHelper(o); var s = n.RC4Drop = o.extend({ cfg: o.cfg.extend({ drop: 192 }), _doReset: function () { o._doReset.call(this); for (var e = this.cfg.drop; e > 0; e--)t.call(this); } }); e.RC4Drop = i._createHelper(s); }(), l.mode.CTRGladman = function () { function t(t) { if (255 == (t >> 24 & 255)) { var e = t >> 16 & 255, r = t >> 8 & 255, i = 255 & t; 255 === e ? (e = 0, 255 === r ? (r = 0, 255 === i ? i = 0 : ++i) : ++r) : ++e, t = 0, t += e << 16, t += r << 8, t += i; } else t += 1 << 24; return t; } function e(e) { return 0 === (e[0] = t(e[0])) && (e[1] = t(e[1])), e; } var r = l.lib.BlockCipherMode.extend(), i = r.Encryptor = r.extend({ processBlock: function (t, r) { var i = this._cipher, n = i.blockSize, o = this._iv, s = this._counter; o && (s = this._counter = o.slice(0), this._iv = void 0), e(s); var c = s.slice(0); i.encryptBlock(c, 0); for (var a = 0; a < n; a++)t[r + a] ^= c[a]; } }); return r.Decryptor = i, r; }(), function () { function t() { for (var t = this._X, e = this._C, r = 0; r < 8; r++)s[r] = e[r]; e[0] = e[0] + 1295307597 + this._b | 0, e[1] = e[1] + 3545052371 + (e[0] >>> 0 < s[0] >>> 0 ? 1 : 0) | 0, e[2] = e[2] + 886263092 + (e[1] >>> 0 < s[1] >>> 0 ? 1 : 0) | 0, e[3] = e[3] + 1295307597 + (e[2] >>> 0 < s[2] >>> 0 ? 1 : 0) | 0, e[4] = e[4] + 3545052371 + (e[3] >>> 0 < s[3] >>> 0 ? 1 : 0) | 0, e[5] = e[5] + 886263092 + (e[4] >>> 0 < s[4] >>> 0 ? 1 : 0) | 0, e[6] = e[6] + 1295307597 + (e[5] >>> 0 < s[5] >>> 0 ? 1 : 0) | 0, e[7] = e[7] + 3545052371 + (e[6] >>> 0 < s[6] >>> 0 ? 1 : 0) | 0, this._b = e[7] >>> 0 < s[7] >>> 0 ? 1 : 0; for (r = 0; r < 8; r++) { var i = t[r] + e[r], n = 65535 & i, o = i >>> 16, a = ((n * n >>> 17) + n * o >>> 15) + o * o, h = ((4294901760 & i) * i | 0) + ((65535 & i) * i | 0); c[r] = a ^ h; } t[0] = c[0] + (c[7] << 16 | c[7] >>> 16) + (c[6] << 16 | c[6] >>> 16) | 0, t[1] = c[1] + (c[0] << 8 | c[0] >>> 24) + c[7] | 0, t[2] = c[2] + (c[1] << 16 | c[1] >>> 16) + (c[0] << 16 | c[0] >>> 16) | 0, t[3] = c[3] + (c[2] << 8 | c[2] >>> 24) + c[1] | 0, t[4] = c[4] + (c[3] << 16 | c[3] >>> 16) + (c[2] << 16 | c[2] >>> 16) | 0, t[5] = c[5] + (c[4] << 8 | c[4] >>> 24) + c[3] | 0, t[6] = c[6] + (c[5] << 16 | c[5] >>> 16) + (c[4] << 16 | c[4] >>> 16) | 0, t[7] = c[7] + (c[6] << 8 | c[6] >>> 24) + c[5] | 0; } var e = l, r = e.lib, i = r.StreamCipher, n = e.algo, o = [], s = [], c = [], a = n.Rabbit = i.extend({ _doReset: function () { for (var e = this._key.words, r = this.cfg.iv, i = 0; i < 4; i++)e[i] = 16711935 & (e[i] << 8 | e[i] >>> 24) | 4278255360 & (e[i] << 24 | e[i] >>> 8); var n = this._X = [e[0], e[3] << 16 | e[2] >>> 16, e[1], e[0] << 16 | e[3] >>> 16, e[2], e[1] << 16 | e[0] >>> 16, e[3], e[2] << 16 | e[1] >>> 16], o = this._C = [e[2] << 16 | e[2] >>> 16, 4294901760 & e[0] | 65535 & e[1], e[3] << 16 | e[3] >>> 16, 4294901760 & e[1] | 65535 & e[2], e[0] << 16 | e[0] >>> 16, 4294901760 & e[2] | 65535 & e[3], e[1] << 16 | e[1] >>> 16, 4294901760 & e[3] | 65535 & e[0]]; this._b = 0; for (i = 0; i < 4; i++)t.call(this); for (i = 0; i < 8; i++)o[i] ^= n[i + 4 & 7]; if (r) { var s = r.words, c = s[0], a = s[1], h = 16711935 & (c << 8 | c >>> 24) | 4278255360 & (c << 24 | c >>> 8), l = 16711935 & (a << 8 | a >>> 24) | 4278255360 & (a << 24 | a >>> 8), f = h >>> 16 | 4294901760 & l, u = l << 16 | 65535 & h; o[0] ^= h, o[1] ^= f, o[2] ^= l, o[3] ^= u, o[4] ^= h, o[5] ^= f, o[6] ^= l, o[7] ^= u; for (i = 0; i < 4; i++)t.call(this); } }, _doProcessBlock: function (e, r) { var i = this._X; t.call(this), o[0] = i[0] ^ i[5] >>> 16 ^ i[3] << 16, o[1] = i[2] ^ i[7] >>> 16 ^ i[5] << 16, o[2] = i[4] ^ i[1] >>> 16 ^ i[7] << 16, o[3] = i[6] ^ i[3] >>> 16 ^ i[1] << 16; for (var n = 0; n < 4; n++)o[n] = 16711935 & (o[n] << 8 | o[n] >>> 24) | 4278255360 & (o[n] << 24 | o[n] >>> 8), e[r + n] ^= o[n]; }, blockSize: 4, ivSize: 2 }); e.Rabbit = i._createHelper(a); }(), l.mode.CTR = function () { var t = l.lib.BlockCipherMode.extend(), e = t.Encryptor = t.extend({ processBlock: function (t, e) { var r = this._cipher, i = r.blockSize, n = this._iv, o = this._counter; n && (o = this._counter = n.slice(0), this._iv = void 0); var s = o.slice(0); r.encryptBlock(s, 0), o[i - 1] = o[i - 1] + 1 | 0; for (var c = 0; c < i; c++)t[e + c] ^= s[c]; } }); return t.Decryptor = e, t; }(), function () { function t() { for (var t = this._X, e = this._C, r = 0; r < 8; r++)s[r] = e[r]; e[0] = e[0] + 1295307597 + this._b | 0, e[1] = e[1] + 3545052371 + (e[0] >>> 0 < s[0] >>> 0 ? 1 : 0) | 0, e[2] = e[2] + 886263092 + (e[1] >>> 0 < s[1] >>> 0 ? 1 : 0) | 0, e[3] = e[3] + 1295307597 + (e[2] >>> 0 < s[2] >>> 0 ? 1 : 0) | 0, e[4] = e[4] + 3545052371 + (e[3] >>> 0 < s[3] >>> 0 ? 1 : 0) | 0, e[5] = e[5] + 886263092 + (e[4] >>> 0 < s[4] >>> 0 ? 1 : 0) | 0, e[6] = e[6] + 1295307597 + (e[5] >>> 0 < s[5] >>> 0 ? 1 : 0) | 0, e[7] = e[7] + 3545052371 + (e[6] >>> 0 < s[6] >>> 0 ? 1 : 0) | 0, this._b = e[7] >>> 0 < s[7] >>> 0 ? 1 : 0; for (r = 0; r < 8; r++) { var i = t[r] + e[r], n = 65535 & i, o = i >>> 16, a = ((n * n >>> 17) + n * o >>> 15) + o * o, h = ((4294901760 & i) * i | 0) + ((65535 & i) * i | 0); c[r] = a ^ h; } t[0] = c[0] + (c[7] << 16 | c[7] >>> 16) + (c[6] << 16 | c[6] >>> 16) | 0, t[1] = c[1] + (c[0] << 8 | c[0] >>> 24) + c[7] | 0, t[2] = c[2] + (c[1] << 16 | c[1] >>> 16) + (c[0] << 16 | c[0] >>> 16) | 0, t[3] = c[3] + (c[2] << 8 | c[2] >>> 24) + c[1] | 0, t[4] = c[4] + (c[3] << 16 | c[3] >>> 16) + (c[2] << 16 | c[2] >>> 16) | 0, t[5] = c[5] + (c[4] << 8 | c[4] >>> 24) + c[3] | 0, t[6] = c[6] + (c[5] << 16 | c[5] >>> 16) + (c[4] << 16 | c[4] >>> 16) | 0, t[7] = c[7] + (c[6] << 8 | c[6] >>> 24) + c[5] | 0; } var e = l, r = e.lib, i = r.StreamCipher, n = e.algo, o = [], s = [], c = [], a = n.RabbitLegacy = i.extend({ _doReset: function () { var e = this._key.words, r = this.cfg.iv, i = this._X = [e[0], e[3] << 16 | e[2] >>> 16, e[1], e[0] << 16 | e[3] >>> 16, e[2], e[1] << 16 | e[0] >>> 16, e[3], e[2] << 16 | e[1] >>> 16], n = this._C = [e[2] << 16 | e[2] >>> 16, 4294901760 & e[0] | 65535 & e[1], e[3] << 16 | e[3] >>> 16, 4294901760 & e[1] | 65535 & e[2], e[0] << 16 | e[0] >>> 16, 4294901760 & e[2] | 65535 & e[3], e[1] << 16 | e[1] >>> 16, 4294901760 & e[3] | 65535 & e[0]]; this._b = 0; for (var o = 0; o < 4; o++)t.call(this); for (o = 0; o < 8; o++)n[o] ^= i[o + 4 & 7]; if (r) { var s = r.words, c = s[0], a = s[1], h = 16711935 & (c << 8 | c >>> 24) | 4278255360 & (c << 24 | c >>> 8), l = 16711935 & (a << 8 | a >>> 24) | 4278255360 & (a << 24 | a >>> 8), f = h >>> 16 | 4294901760 & l, u = l << 16 | 65535 & h; n[0] ^= h, n[1] ^= f, n[2] ^= l, n[3] ^= u, n[4] ^= h, n[5] ^= f, n[6] ^= l, n[7] ^= u; for (o = 0; o < 4; o++)t.call(this); } }, _doProcessBlock: function (e, r) { var i = this._X; t.call(this), o[0] = i[0] ^ i[5] >>> 16 ^ i[3] << 16, o[1] = i[2] ^ i[7] >>> 16 ^ i[5] << 16, o[2] = i[4] ^ i[1] >>> 16 ^ i[7] << 16, o[3] = i[6] ^ i[3] >>> 16 ^ i[1] << 16; for (var n = 0; n < 4; n++)o[n] = 16711935 & (o[n] << 8 | o[n] >>> 24) | 4278255360 & (o[n] << 24 | o[n] >>> 8), e[r + n] ^= o[n]; }, blockSize: 4, ivSize: 2 }); e.RabbitLegacy = i._createHelper(a); }(), l.pad.ZeroPadding = { pad: function (t, e) { var r = 4 * e; t.clamp(), t.sigBytes += r - (t.sigBytes % r || r); }, unpad: function (t) { var e = t.words, r = t.sigBytes - 1; for (r = t.sigBytes - 1; r >= 0; r--)if (e[r >>> 2] >>> 24 - r % 4 * 8 & 255) { t.sigBytes = r + 1; break; } } }, l.enc.u8array = { stringify: function (t) { let e = t.words, r = t.sigBytes, i = new Uint8Array(r); for (let t = 0; t < r; t++) { let r = e[t >>> 2] >>> 24 - t % 4 * 8 & 255; i[t] = r; } return i; }, parse: function (t) { let e = t.length, r = []; for (let i = 0; i < e; i++)r[i >>> 2] |= (255 & t[i]) << 24 - i % 4 * 8; return l.lib.WordArray.create(r, e); } }, l;
});


// 图片编辑外部js=>fabric.js
(function (name, definition) {
    window[name] = definition.call(window);
})("fabric", function () {
    var fabric = fabric || { version: "5.3.0" }; if ("undefined" != typeof exports ? exports.fabric = fabric : "function" == typeof define && define.amd && define([], function () { return fabric; }), "undefined" != typeof document && "undefined" != typeof window) document instanceof ("undefined" != typeof HTMLDocument ? HTMLDocument : Document) ? fabric.document = document : fabric.document = document.implementation.createHTMLDocument(""), fabric.window = window; else { var jsdom = require("jsdom"), virtualWindow = new jsdom.JSDOM(decodeURIComponent("%3C!DOCTYPE%20html%3E%3Chtml%3E%3Chead%3E%3C%2Fhead%3E%3Cbody%3E%3C%2Fbody%3E%3C%2Fhtml%3E"), { features: { FetchExternalResources: ["img"] }, resources: "usable" }).window; fabric.document = virtualWindow.document, fabric.jsdomImplForWrapper = require("jsdom/lib/jsdom/living/generated/utils").implForWrapper, fabric.nodeCanvas = require("jsdom/lib/jsdom/utils").Canvas, fabric.window = virtualWindow, DOMParser = fabric.window.DOMParser; } function resizeCanvasIfNeeded(t) { var e = t.targetCanvas, i = e.width, r = e.height, n = t.destinationWidth, s = t.destinationHeight; i === n && r === s || (e.width = n, e.height = s); } function copyGLTo2DDrawImage(t, e) { var i = t.canvas, r = e.targetCanvas, n = r.getContext("2d"); n.translate(0, r.height), n.scale(1, -1); var s = i.height - r.height; n.drawImage(i, 0, s, r.width, r.height, 0, 0, r.width, r.height); } function copyGLTo2DPutImageData(t, e) { var i = e.targetCanvas.getContext("2d"), r = e.destinationWidth, n = e.destinationHeight, s = r * n * 4, o = new Uint8Array(this.imageBuffer, 0, s), a = new Uint8ClampedArray(this.imageBuffer, 0, s); t.readPixels(0, 0, r, n, t.RGBA, t.UNSIGNED_BYTE, o); var c = new ImageData(a, r, n); i.putImageData(c, 0, 0); } fabric.isTouchSupported = "ontouchstart" in fabric.window || "ontouchstart" in fabric.document || fabric.window && fabric.window.navigator && 0 < fabric.window.navigator.maxTouchPoints, fabric.isLikelyNode = "undefined" != typeof Buffer && "undefined" == typeof window, fabric.SHARED_ATTRIBUTES = ["display", "transform", "fill", "fill-opacity", "fill-rule", "opacity", "stroke", "stroke-dasharray", "stroke-linecap", "stroke-dashoffset", "stroke-linejoin", "stroke-miterlimit", "stroke-opacity", "stroke-width", "id", "paint-order", "vector-effect", "instantiated_by_use", "clip-path"], fabric.DPI = 96, fabric.reNum = "(?:[-+]?(?:\\d+|\\d*\\.\\d+)(?:[eE][-+]?\\d+)?)", fabric.commaWsp = "(?:\\s+,?\\s*|,\\s*)", fabric.rePathCommand = /([-+]?((\d+\.\d+)|((\d+)|(\.\d+)))(?:[eE][-+]?\d+)?)/gi, fabric.reNonWord = /[ \n\.,;!\?\-]/, fabric.fontPaths = {}, fabric.iMatrix = [1, 0, 0, 1, 0, 0], fabric.svgNS = "http://www.w3.org/2000/svg", fabric.perfLimitSizeTotal = 2097152, fabric.maxCacheSideLimit = 4096, fabric.minCacheSideLimit = 256, fabric.charWidthsCache = {}, fabric.textureSize = 2048, fabric.disableStyleCopyPaste = !1, fabric.enableGLFiltering = !0, fabric.devicePixelRatio = fabric.window.devicePixelRatio || fabric.window.webkitDevicePixelRatio || fabric.window.mozDevicePixelRatio || 1, fabric.browserShadowBlurConstant = 1, fabric.arcToSegmentsCache = {}, fabric.boundsOfCurveCache = {}, fabric.cachesBoundsOfCurve = !0, fabric.forceGLPutImageData = !1, fabric.initFilterBackend = function () { return fabric.enableGLFiltering && fabric.isWebglSupported && fabric.isWebglSupported(fabric.textureSize) ? (console.log("max texture size: " + fabric.maxTextureSize), new fabric.WebglFilterBackend({ tileSize: fabric.textureSize })) : fabric.Canvas2dFilterBackend ? new fabric.Canvas2dFilterBackend : void 0; }, "undefined" != typeof document && "undefined" != typeof window && (window.fabric = fabric), function () { function r(t, e) { if (this.__eventListeners[t]) { var i = this.__eventListeners[t]; e ? i[i.indexOf(e)] = !1 : fabric.util.array.fill(i, !1); } } function n(t, e) { var i = function () { e.apply(this, arguments), this.off(t, i); }.bind(this); this.on(t, i); } fabric.Observable = { fire: function (t, e) { if (!this.__eventListeners) return this; var i = this.__eventListeners[t]; if (!i) return this; for (var r = 0, n = i.length; r < n; r++)i[r] && i[r].call(this, e || {}); return this.__eventListeners[t] = i.filter(function (t) { return !1 !== t; }), this; }, on: function (t, e) { if (this.__eventListeners || (this.__eventListeners = {}), 1 === arguments.length) for (var i in t) this.on(i, t[i]); else this.__eventListeners[t] || (this.__eventListeners[t] = []), this.__eventListeners[t].push(e); return this; }, once: function (t, e) { if (1 === arguments.length) for (var i in t) n.call(this, i, t[i]); else n.call(this, t, e); return this; }, off: function (t, e) { if (!this.__eventListeners) return this; if (0 === arguments.length) for (t in this.__eventListeners) r.call(this, t); else if (1 === arguments.length && "object" == typeof t) for (var i in t) r.call(this, i, t[i]); else r.call(this, t, e); return this; } }; }(), fabric.Collection = { _objects: [], add: function () { if (this._objects.push.apply(this._objects, arguments), this._onObjectAdded) for (var t = 0, e = arguments.length; t < e; t++)this._onObjectAdded(arguments[t]); return this.renderOnAddRemove && this.requestRenderAll(), this; }, insertAt: function (t, e, i) { var r = this._objects; return i ? r[e] = t : r.splice(e, 0, t), this._onObjectAdded && this._onObjectAdded(t), this.renderOnAddRemove && this.requestRenderAll(), this; }, remove: function () { for (var t, e = this._objects, i = !1, r = 0, n = arguments.length; r < n; r++)-1 !== (t = e.indexOf(arguments[r])) && (i = !0, e.splice(t, 1), this._onObjectRemoved && this._onObjectRemoved(arguments[r])); return this.renderOnAddRemove && i && this.requestRenderAll(), this; }, forEachObject: function (t, e) { for (var i = this.getObjects(), r = 0, n = i.length; r < n; r++)t.call(e, i[r], r, i); return this; }, getObjects: function (e) { return void 0 === e ? this._objects.concat() : this._objects.filter(function (t) { return t.type === e; }); }, item: function (t) { return this._objects[t]; }, isEmpty: function () { return 0 === this._objects.length; }, size: function () { return this._objects.length; }, contains: function (e, t) { return -1 < this._objects.indexOf(e) || !!t && this._objects.some(function (t) { return "function" == typeof t.contains && t.contains(e, !0); }); }, complexity: function () { return this._objects.reduce(function (t, e) { return t += e.complexity ? e.complexity() : 0; }, 0); } }, fabric.CommonMethods = { _setOptions: function (t) { for (var e in t) this.set(e, t[e]); }, _initGradient: function (t, e) { !t || !t.colorStops || t instanceof fabric.Gradient || this.set(e, new fabric.Gradient(t)); }, _initPattern: function (t, e, i) { !t || !t.source || t instanceof fabric.Pattern ? i && i() : this.set(e, new fabric.Pattern(t, i)); }, _setObject: function (t) { for (var e in t) this._set(e, t[e]); }, set: function (t, e) { return "object" == typeof t ? this._setObject(t) : this._set(t, e), this; }, _set: function (t, e) { this[t] = e; }, toggle: function (t) { var e = this.get(t); return "boolean" == typeof e && this.set(t, !e), this; }, get: function (t) { return this[t]; } }, function (s) { var o = Math.sqrt, a = Math.atan2, c = Math.pow, h = Math.PI / 180, i = Math.PI / 2; fabric.util = { cos: function (t) { if (0 === t) return 1; switch (t < 0 && (t = -t), t / i) { case 1: case 3: return 0; case 2: return -1; }return Math.cos(t); }, sin: function (t) { if (0 === t) return 0; var e = 1; switch (t < 0 && (e = -1), t / i) { case 1: return e; case 2: return 0; case 3: return -e; }return Math.sin(t); }, removeFromArray: function (t, e) { var i = t.indexOf(e); return -1 !== i && t.splice(i, 1), t; }, getRandomInt: function (t, e) { return Math.floor(Math.random() * (e - t + 1)) + t; }, degreesToRadians: function (t) { return t * h; }, radiansToDegrees: function (t) { return t / h; }, rotatePoint: function (t, e, i) { var r = new fabric.Point(t.x - e.x, t.y - e.y), n = fabric.util.rotateVector(r, i); return new fabric.Point(n.x, n.y).addEquals(e); }, rotateVector: function (t, e) { var i = fabric.util.sin(e), r = fabric.util.cos(e); return { x: t.x * r - t.y * i, y: t.x * i + t.y * r }; }, createVector: function (t, e) { return new fabric.Point(e.x - t.x, e.y - t.y); }, calcAngleBetweenVectors: function (t, e) { return Math.acos((t.x * e.x + t.y * e.y) / (Math.hypot(t.x, t.y) * Math.hypot(e.x, e.y))); }, getHatVector: function (t) { return new fabric.Point(t.x, t.y).multiply(1 / Math.hypot(t.x, t.y)); }, getBisector: function (t, e, i) { var r = fabric.util.createVector(t, e), n = fabric.util.createVector(t, i), s = fabric.util.calcAngleBetweenVectors(r, n), o = s * (0 === fabric.util.calcAngleBetweenVectors(fabric.util.rotateVector(r, s), n) ? 1 : -1) / 2; return { vector: fabric.util.getHatVector(fabric.util.rotateVector(r, o)), angle: s }; }, projectStrokeOnPoints: function (l, u, f) { var d = [], g = u.strokeWidth / 2, p = u.strokeUniform ? new fabric.Point(1 / u.scaleX, 1 / u.scaleY) : new fabric.Point(1, 1), v = function (t) { var e = g / Math.hypot(t.x, t.y); return new fabric.Point(t.x * e * p.x, t.y * e * p.y); }; return l.length <= 1 || l.forEach(function (t, e) { var i, r, n = new fabric.Point(t.x, t.y); 0 === e ? (r = l[e + 1], i = f ? v(fabric.util.createVector(r, n)).addEquals(n) : l[l.length - 1]) : e === l.length - 1 ? (i = l[e - 1], r = f ? v(fabric.util.createVector(i, n)).addEquals(n) : l[0]) : (i = l[e - 1], r = l[e + 1]); var s, o, a = fabric.util.getBisector(n, i, r), c = a.vector, h = a.angle; if ("miter" === u.strokeLineJoin && (s = -g / Math.sin(h / 2), o = new fabric.Point(c.x * s * p.x, c.y * s * p.y), Math.hypot(o.x, o.y) / g <= u.strokeMiterLimit)) return d.push(n.add(o)), void d.push(n.subtract(o)); s = -g * Math.SQRT2, o = new fabric.Point(c.x * s * p.x, c.y * s * p.y), d.push(n.add(o)), d.push(n.subtract(o)); }), d; }, transformPoint: function (t, e, i) { return i ? new fabric.Point(e[0] * t.x + e[2] * t.y, e[1] * t.x + e[3] * t.y) : new fabric.Point(e[0] * t.x + e[2] * t.y + e[4], e[1] * t.x + e[3] * t.y + e[5]); }, makeBoundingBoxFromPoints: function (t, e) { if (e) for (var i = 0; i < t.length; i++)t[i] = fabric.util.transformPoint(t[i], e); var r = [t[0].x, t[1].x, t[2].x, t[3].x], n = fabric.util.array.min(r), s = fabric.util.array.max(r) - n, o = [t[0].y, t[1].y, t[2].y, t[3].y], a = fabric.util.array.min(o); return { left: n, top: a, width: s, height: fabric.util.array.max(o) - a }; }, invertTransform: function (t) { var e = 1 / (t[0] * t[3] - t[1] * t[2]), i = [e * t[3], -e * t[1], -e * t[2], e * t[0]], r = fabric.util.transformPoint({ x: t[4], y: t[5] }, i, !0); return i[4] = -r.x, i[5] = -r.y, i; }, toFixed: function (t, e) { return parseFloat(Number(t).toFixed(e)); }, parseUnit: function (t, e) { var i = /\D{0,2}$/.exec(t), r = parseFloat(t); switch (e || (e = fabric.Text.DEFAULT_SVG_FONT_SIZE), i[0]) { case "mm": return r * fabric.DPI / 25.4; case "cm": return r * fabric.DPI / 2.54; case "in": return r * fabric.DPI; case "pt": return r * fabric.DPI / 72; case "pc": return r * fabric.DPI / 72 * 12; case "em": return r * e; default: return r; } }, falseFunction: function () { return !1; }, getKlass: function (t, e) { return t = fabric.util.string.camelize(t.charAt(0).toUpperCase() + t.slice(1)), fabric.util.resolveNamespace(e)[t]; }, getSvgAttributes: function (t) { var e = ["instantiated_by_use", "style", "id", "class"]; switch (t) { case "linearGradient": e = e.concat(["x1", "y1", "x2", "y2", "gradientUnits", "gradientTransform"]); break; case "radialGradient": e = e.concat(["gradientUnits", "gradientTransform", "cx", "cy", "r", "fx", "fy", "fr"]); break; case "stop": e = e.concat(["offset", "stop-color", "stop-opacity"]); }return e; }, resolveNamespace: function (t) { if (!t) return fabric; var e, i = t.split("."), r = i.length, n = s || fabric.window; for (e = 0; e < r; ++e)n = n[i[e]]; return n; }, loadImage: function (t, e, i, r) { if (t) { var n = fabric.util.createImage(), s = function () { e && e.call(i, n, !1), n = n.onload = n.onerror = null; }; n.onload = s, n.onerror = function () { fabric.log("Error loading " + n.src), e && e.call(i, null, !0), n = n.onload = n.onerror = null; }, 0 !== t.indexOf("data") && null != r && (n.crossOrigin = r), "data:image/svg" === t.substring(0, 14) && (n.onload = null, fabric.util.loadImageInDom(n, s)), n.src = t; } else e && e.call(i, t); }, loadImageInDom: function (t, e) { var i = fabric.document.createElement("div"); i.style.width = i.style.height = "1px", i.style.left = i.style.top = "-100%", i.style.position = "absolute", i.appendChild(t), fabric.document.querySelector("body").appendChild(i), t.onload = function () { e(), i.parentNode.removeChild(i), i = null; }; }, enlivenObjects: function (t, e, n, s) { var o = [], i = 0, r = (t = t || []).length; function a() { ++i === r && e && e(o.filter(function (t) { return t; })); } r ? t.forEach(function (i, r) { i && i.type ? fabric.util.getKlass(i.type, n).fromObject(i, function (t, e) { e || (o[r] = t), s && s(i, t, e), a(); }) : a(); }) : e && e(o); }, enlivenObjectEnlivables: function (e, n, t) { var s = fabric.Object.ENLIVEN_PROPS.filter(function (t) { return !!e[t]; }); fabric.util.enlivenObjects(s.map(function (t) { return e[t]; }), function (i) { var r = {}; s.forEach(function (t, e) { r[t] = i[e], n && (n[t] = i[e]); }), t && t(r); }); }, enlivenPatterns: function (t, e) { function i() { ++n === s && e && e(r); } var r = [], n = 0, s = (t = t || []).length; s ? t.forEach(function (t, e) { t && t.source ? new fabric.Pattern(t, function (t) { r[e] = t, i(); }) : (r[e] = t, i()); }) : e && e(r); }, groupSVGElements: function (t, e, i) { var r; return t && 1 === t.length ? (void 0 !== i && (t[0].sourcePath = i), t[0]) : (e && (e.width && e.height ? e.centerPoint = { x: e.width / 2, y: e.height / 2 } : (delete e.width, delete e.height)), r = new fabric.Group(t, e), void 0 !== i && (r.sourcePath = i), r); }, populateWithProperties: function (t, e, i) { if (i && Array.isArray(i)) for (var r = 0, n = i.length; r < n; r++)i[r] in t && (e[i[r]] = t[i[r]]); }, createCanvasElement: function () { return fabric.document.createElement("canvas"); }, copyCanvasElement: function (t) { var e = fabric.util.createCanvasElement(); return e.width = t.width, e.height = t.height, e.getContext("2d").drawImage(t, 0, 0), e; }, toDataURL: function (t, e, i) { return t.toDataURL("image/" + e, i); }, createImage: function () { return fabric.document.createElement("img"); }, multiplyTransformMatrices: function (t, e, i) { return [t[0] * e[0] + t[2] * e[1], t[1] * e[0] + t[3] * e[1], t[0] * e[2] + t[2] * e[3], t[1] * e[2] + t[3] * e[3], i ? 0 : t[0] * e[4] + t[2] * e[5] + t[4], i ? 0 : t[1] * e[4] + t[3] * e[5] + t[5]]; }, qrDecompose: function (t) { var e = a(t[1], t[0]), i = c(t[0], 2) + c(t[1], 2), r = o(i), n = (t[0] * t[3] - t[2] * t[1]) / r, s = a(t[0] * t[2] + t[1] * t[3], i); return { angle: e / h, scaleX: r, scaleY: n, skewX: s / h, skewY: 0, translateX: t[4], translateY: t[5] }; }, calcRotateMatrix: function (t) { if (!t.angle) return fabric.iMatrix.concat(); var e = fabric.util.degreesToRadians(t.angle), i = fabric.util.cos(e), r = fabric.util.sin(e); return [i, r, -r, i, 0, 0]; }, calcDimensionsMatrix: function (t) { var e = void 0 === t.scaleX ? 1 : t.scaleX, i = void 0 === t.scaleY ? 1 : t.scaleY, r = [t.flipX ? -e : e, 0, 0, t.flipY ? -i : i, 0, 0], n = fabric.util.multiplyTransformMatrices, s = fabric.util.degreesToRadians; return t.skewX && (r = n(r, [1, 0, Math.tan(s(t.skewX)), 1], !0)), t.skewY && (r = n(r, [1, Math.tan(s(t.skewY)), 0, 1], !0)), r; }, composeMatrix: function (t) { var e = [1, 0, 0, 1, t.translateX || 0, t.translateY || 0], i = fabric.util.multiplyTransformMatrices; return t.angle && (e = i(e, fabric.util.calcRotateMatrix(t))), (1 !== t.scaleX || 1 !== t.scaleY || t.skewX || t.skewY || t.flipX || t.flipY) && (e = i(e, fabric.util.calcDimensionsMatrix(t))), e; }, resetObjectTransform: function (t) { t.scaleX = 1, t.scaleY = 1, t.skewX = 0, t.skewY = 0, t.flipX = !1, t.flipY = !1, t.rotate(0); }, saveObjectTransform: function (t) { return { scaleX: t.scaleX, scaleY: t.scaleY, skewX: t.skewX, skewY: t.skewY, angle: t.angle, left: t.left, flipX: t.flipX, flipY: t.flipY, top: t.top }; }, isTransparent: function (t, e, i, r) { 0 < r && (r < e ? e -= r : e = 0, r < i ? i -= r : i = 0); var n, s = !0, o = t.getImageData(e, i, 2 * r || 1, 2 * r || 1), a = o.data.length; for (n = 3; n < a && !1 !== (s = o.data[n] <= 0); n += 4); return o = null, s; }, parsePreserveAspectRatioAttribute: function (t) { var e, i = "meet", r = t.split(" "); return r && r.length && ("meet" !== (i = r.pop()) && "slice" !== i ? (e = i, i = "meet") : r.length && (e = r.pop())), { meetOrSlice: i, alignX: "none" !== e ? e.slice(1, 4) : "none", alignY: "none" !== e ? e.slice(5, 8) : "none" }; }, clearFabricFontCache: function (t) { (t = (t || "").toLowerCase()) ? fabric.charWidthsCache[t] && delete fabric.charWidthsCache[t] : fabric.charWidthsCache = {}; }, limitDimsByArea: function (t, e) { var i = Math.sqrt(e * t), r = Math.floor(e / i); return { x: Math.floor(i), y: r }; }, capValue: function (t, e, i) { return Math.max(t, Math.min(e, i)); }, findScaleToFit: function (t, e) { return Math.min(e.width / t.width, e.height / t.height); }, findScaleToCover: function (t, e) { return Math.max(e.width / t.width, e.height / t.height); }, matrixToSVG: function (t) { return "matrix(" + t.map(function (t) { return fabric.util.toFixed(t, fabric.Object.NUM_FRACTION_DIGITS); }).join(" ") + ")"; }, removeTransformFromObject: function (t, e) { var i = fabric.util.invertTransform(e), r = fabric.util.multiplyTransformMatrices(i, t.calcOwnMatrix()); fabric.util.applyTransformToObject(t, r); }, addTransformToObject: function (t, e) { fabric.util.applyTransformToObject(t, fabric.util.multiplyTransformMatrices(e, t.calcOwnMatrix())); }, applyTransformToObject: function (t, e) { var i = fabric.util.qrDecompose(e), r = new fabric.Point(i.translateX, i.translateY); t.flipX = !1, t.flipY = !1, t.set("scaleX", i.scaleX), t.set("scaleY", i.scaleY), t.skewX = i.skewX, t.skewY = i.skewY, t.angle = i.angle, t.setPositionByOrigin(r, "center", "center"); }, sizeAfterTransform: function (t, e, i) { var r = t / 2, n = e / 2, s = [{ x: -r, y: -n }, { x: r, y: -n }, { x: -r, y: n }, { x: r, y: n }], o = fabric.util.calcDimensionsMatrix(i), a = fabric.util.makeBoundingBoxFromPoints(s, o); return { x: a.width, y: a.height }; }, mergeClipPaths: function (t, e) { var i = t, r = e; i.inverted && !r.inverted && (i = e, r = t), fabric.util.applyTransformToObject(r, fabric.util.multiplyTransformMatrices(fabric.util.invertTransform(i.calcTransformMatrix()), r.calcTransformMatrix())); var n = i.inverted && r.inverted; return n && (i.inverted = r.inverted = !1), new fabric.Group([i], { clipPath: r, inverted: n }); }, hasStyleChanged: function (t, e, i) { return i = i || !1, t.fill !== e.fill || t.stroke !== e.stroke || t.strokeWidth !== e.strokeWidth || t.fontSize !== e.fontSize || t.fontFamily !== e.fontFamily || t.fontWeight !== e.fontWeight || t.fontStyle !== e.fontStyle || t.textBackgroundColor !== e.textBackgroundColor || t.deltaY !== e.deltaY || i && (t.overline !== e.overline || t.underline !== e.underline || t.linethrough !== e.linethrough); }, stylesToArray: function (t, e) { t = fabric.util.object.clone(t, !0); for (var i = e.split("\n"), r = -1, n = {}, s = [], o = 0; o < i.length; o++)if (t[o]) for (var a = 0; a < i[o].length; a++) { r++; var c = t[o][a]; if (c && 0 < Object.keys(c).length) fabric.util.hasStyleChanged(n, c, !0) ? s.push({ start: r, end: r + 1, style: c }) : s[s.length - 1].end++; n = c || {}; } else r += i[o].length; return s; }, stylesFromArray: function (t, e) { if (!Array.isArray(t)) return t; for (var i = e.split("\n"), r = -1, n = 0, s = {}, o = 0; o < i.length; o++)for (var a = 0; a < i[o].length; a++)r++, t[n] && t[n].start <= r && r < t[n].end && (s[o] = s[o] || {}, s[o][a] = Object.assign({}, t[n].style), r === t[n].end - 1 && n++); return s; } }; }("undefined" != typeof exports ? exports : this), function () { var A = Array.prototype.join, T = { m: 2, l: 2, h: 1, v: 1, c: 6, s: 4, q: 4, t: 2, a: 7 }, w = { m: "l", M: "L" }; function Z(t, e, i, r) { var n = Math.atan2(e, t), s = Math.atan2(r, i); return n <= s ? s - n : 2 * Math.PI - (n - s); } function d(t, e, i) { for (var r = i[1], n = i[2], s = i[3], o = i[4], a = i[5], c = function (t, e, i, r, n, s, o) { var a = Math.PI, c = o * a / 180, h = fabric.util.sin(c), l = fabric.util.cos(c), u = 0, f = 0, d = -l * t * .5 - h * e * .5, g = -l * e * .5 + h * t * .5, p = (i = Math.abs(i)) * i, v = (r = Math.abs(r)) * r, m = g * g, b = d * d, y = p * v - p * m - v * b, _ = 0; if (y < 0) { var x = Math.sqrt(1 - y / (p * v)); i *= x, r *= x; } else _ = (n === s ? -1 : 1) * Math.sqrt(y / (p * m + v * b)); var C = _ * i * g / r, S = -_ * r * d / i, T = l * C - h * S + .5 * t, w = h * C + l * S + .5 * e, O = Z(1, 0, (d - C) / i, (g - S) / r), k = Z((d - C) / i, (g - S) / r, (-d - C) / i, (-g - S) / r); 0 === s && 0 < k ? k -= 2 * a : 1 === s && k < 0 && (k += 2 * a); for (var P, E, A, D, j, M, F, I, L, R, B, X, W, Y, H, z, G, U = Math.ceil(Math.abs(k / a * 2)), V = [], N = k / U, q = 8 / 3 * Math.sin(N / 4) * Math.sin(N / 4) / Math.sin(N / 2), K = O + N, J = 0; J < U; J++)V[J] = (P = O, E = K, A = l, D = h, j = i, M = r, F = T, I = w, L = q, R = u, B = f, X = fabric.util.cos(P), W = fabric.util.sin(P), Y = fabric.util.cos(E), H = fabric.util.sin(E), ["C", R + L * (-A * j * W - D * M * X), B + L * (-D * j * W + A * M * X), (z = A * j * Y - D * M * H + F) + L * (A * j * H + D * M * Y), (G = D * j * Y + A * M * H + I) + L * (D * j * H - A * M * Y), z, G]), u = V[J][5], f = V[J][6], O = K, K += N; return V; }(i[6] - t, i[7] - e, r, n, o, a, s), h = 0, l = c.length; h < l; h++)c[h][1] += t, c[h][2] += e, c[h][3] += t, c[h][4] += e, c[h][5] += t, c[h][6] += e; return c; } function g(t, e, i, r) { return Math.sqrt((i - t) * (i - t) + (r - e) * (r - e)); } function p(h, l, u, f, d, g, p, v) { return function (t) { var e, i, r, n, s = (n = t) * n * n, o = 3 * (r = t) * r * (1 - r), a = 3 * (i = t) * (1 - i) * (1 - i), c = (1 - (e = t)) * (1 - e) * (1 - e); return { x: p * s + d * o + u * a + h * c, y: v * s + g * o + f * a + l * c }; }; } function v(n, s, o, a, c, h, l, u) { return function (t) { var e = 1 - t, i = 3 * e * e * (o - n) + 6 * e * t * (c - o) + 3 * t * t * (l - c), r = 3 * e * e * (a - s) + 6 * e * t * (h - a) + 3 * t * t * (u - h); return Math.atan2(r, i); }; } function m(a, c, h, l, u, f) { return function (t) { var e, i, r, n = (r = t) * r, s = 2 * (i = t) * (1 - i), o = (1 - (e = t)) * (1 - e); return { x: u * n + h * s + a * o, y: f * n + l * s + c * o }; }; } function b(n, s, o, a, c, h) { return function (t) { var e = 1 - t, i = 2 * e * (o - n) + 2 * t * (c - o), r = 2 * e * (a - s) + 2 * t * (h - a); return Math.atan2(r, i); }; } function y(t, e, i) { var r, n, s = { x: e, y: i }, o = 0; for (n = 1; n <= 100; n += 1)r = t(n / 100), o += g(s.x, s.y, r.x, r.y), s = r; return o; } function h(t, e) { for (var i, r, n, s = 0, o = 0, a = t.iterator, c = { x: t.x, y: t.y }, h = .01, l = t.angleFinder; o < e && 1e-4 < h;)i = a(s), n = s, e < (r = g(c.x, c.y, i.x, i.y)) + o ? (s -= h, h /= 2) : (c = i, s += h, o += r); return i.angle = l(n), i; } function l(t) { for (var e, i, r, n, s = 0, o = t.length, a = 0, c = 0, h = 0, l = 0, u = [], f = 0; f < o; f++) { switch (r = { x: a, y: c, command: (e = t[f])[0] }, e[0]) { case "M": r.length = 0, h = a = e[1], l = c = e[2]; break; case "L": r.length = g(a, c, e[1], e[2]), a = e[1], c = e[2]; break; case "C": i = p(a, c, e[1], e[2], e[3], e[4], e[5], e[6]), n = v(a, c, e[1], e[2], e[3], e[4], e[5], e[6]), r.iterator = i, r.angleFinder = n, r.length = y(i, a, c), a = e[5], c = e[6]; break; case "Q": i = m(a, c, e[1], e[2], e[3], e[4]), n = b(a, c, e[1], e[2], e[3], e[4]), r.iterator = i, r.angleFinder = n, r.length = y(i, a, c), a = e[3], c = e[4]; break; case "Z": case "z": r.destX = h, r.destY = l, r.length = g(a, c, h, l), a = h, c = l; }s += r.length, u.push(r); } return u.push({ length: s, x: a, y: c }), u; } fabric.util.joinPath = function (t) { return t.map(function (t) { return t.join(" "); }).join(" "); }, fabric.util.parsePath = function (t) { var e, i, r, n, s, o = [], a = [], c = fabric.rePathCommand, h = "[-+]?(?:\\d*\\.\\d+|\\d+\\.?)(?:[eE][-+]?\\d+)?\\s*", l = "(" + h + ")" + fabric.commaWsp, u = "([01])" + fabric.commaWsp + "?", f = new RegExp(l + "?" + l + "?" + l + u + u + l + "?(" + h + ")", "g"); if (!t || !t.match) return o; for (var d, g = 0, p = (s = t.match(/[mzlhvcsqta][^mzlhvcsqta]*/gi)).length; g < p; g++) { n = (e = s[g]).slice(1).trim(), a.length = 0; var v = e.charAt(0); if (d = [v], "a" === v.toLowerCase()) for (var m; m = f.exec(n);)for (var b = 1; b < m.length; b++)a.push(m[b]); else for (; r = c.exec(n);)a.push(r[0]); b = 0; for (var y = a.length; b < y; b++)i = parseFloat(a[b]), isNaN(i) || d.push(i); var _ = T[v.toLowerCase()], x = w[v] || v; if (d.length - 1 > _) for (var C = 1, S = d.length; C < S; C += _)o.push([v].concat(d.slice(C, C + _))), v = x; else o.push(d); } return o; }, fabric.util.makePathSimpler = function (t) { var e, i, r, n, s, o, a = 0, c = 0, h = t.length, l = 0, u = 0, f = []; for (i = 0; i < h; ++i) { switch (r = !1, (e = t[i].slice(0))[0]) { case "l": e[0] = "L", e[1] += a, e[2] += c; case "L": a = e[1], c = e[2]; break; case "h": e[1] += a; case "H": e[0] = "L", e[2] = c, a = e[1]; break; case "v": e[1] += c; case "V": e[0] = "L", c = e[1], e[1] = a, e[2] = c; break; case "m": e[0] = "M", e[1] += a, e[2] += c; case "M": a = e[1], c = e[2], l = e[1], u = e[2]; break; case "c": e[0] = "C", e[1] += a, e[2] += c, e[3] += a, e[4] += c, e[5] += a, e[6] += c; case "C": s = e[3], o = e[4], a = e[5], c = e[6]; break; case "s": e[0] = "S", e[1] += a, e[2] += c, e[3] += a, e[4] += c; case "S": "C" === n ? (s = 2 * a - s, o = 2 * c - o) : (s = a, o = c), a = e[3], c = e[4], e[0] = "C", e[5] = e[3], e[6] = e[4], e[3] = e[1], e[4] = e[2], e[1] = s, e[2] = o, s = e[3], o = e[4]; break; case "q": e[0] = "Q", e[1] += a, e[2] += c, e[3] += a, e[4] += c; case "Q": s = e[1], o = e[2], a = e[3], c = e[4]; break; case "t": e[0] = "T", e[1] += a, e[2] += c; case "T": "Q" === n ? (s = 2 * a - s, o = 2 * c - o) : (s = a, o = c), e[0] = "Q", a = e[1], c = e[2], e[1] = s, e[2] = o, e[3] = a, e[4] = c; break; case "a": e[0] = "A", e[6] += a, e[7] += c; case "A": r = !0, f = f.concat(d(a, c, e)), a = e[6], c = e[7]; break; case "z": case "Z": a = l, c = u; }r || f.push(e), n = e[0]; } return f; }, fabric.util.getSmoothPathFromPoints = function (t, e) { var i, r = [], n = new fabric.Point(t[0].x, t[0].y), s = new fabric.Point(t[1].x, t[1].y), o = t.length, a = 1, c = 0, h = 2 < o; for (e = e || 0, h && (a = t[2].x < s.x ? -1 : t[2].x === s.x ? 0 : 1, c = t[2].y < s.y ? -1 : t[2].y === s.y ? 0 : 1), r.push(["M", n.x - a * e, n.y - c * e]), i = 1; i < o; i++) { if (!n.eq(s)) { var l = n.midPointFrom(s); r.push(["Q", n.x, n.y, l.x, l.y]); } n = t[i], i + 1 < t.length && (s = t[i + 1]); } return h && (a = n.x > t[i - 2].x ? 1 : n.x === t[i - 2].x ? 0 : -1, c = n.y > t[i - 2].y ? 1 : n.y === t[i - 2].y ? 0 : -1), r.push(["L", n.x + a * e, n.y + c * e]), r; }, fabric.util.getPathSegmentsInfo = l, fabric.util.getBoundsOfCurve = function (t, e, i, r, n, s, o, a) { var c; if (fabric.cachesBoundsOfCurve && (c = A.call(arguments), fabric.boundsOfCurveCache[c])) return fabric.boundsOfCurveCache[c]; var h, l, u, f, d, g, p, v, m = Math.sqrt, b = Math.min, y = Math.max, _ = Math.abs, x = [], C = [[], []]; l = 6 * t - 12 * i + 6 * n, h = -3 * t + 9 * i - 9 * n + 3 * o, u = 3 * i - 3 * t; for (var S = 0; S < 2; ++S)if (0 < S && (l = 6 * e - 12 * r + 6 * s, h = -3 * e + 9 * r - 9 * s + 3 * a, u = 3 * r - 3 * e), _(h) < 1e-12) { if (_(l) < 1e-12) continue; 0 < (f = -u / l) && f < 1 && x.push(f); } else (p = l * l - 4 * u * h) < 0 || (0 < (d = (-l + (v = m(p))) / (2 * h)) && d < 1 && x.push(d), 0 < (g = (-l - v) / (2 * h)) && g < 1 && x.push(g)); for (var T, w, O, k = x.length, P = k; k--;)T = (O = 1 - (f = x[k])) * O * O * t + 3 * O * O * f * i + 3 * O * f * f * n + f * f * f * o, C[0][k] = T, w = O * O * O * e + 3 * O * O * f * r + 3 * O * f * f * s + f * f * f * a, C[1][k] = w; C[0][P] = t, C[1][P] = e, C[0][P + 1] = o, C[1][P + 1] = a; var E = [{ x: b.apply(null, C[0]), y: b.apply(null, C[1]) }, { x: y.apply(null, C[0]), y: y.apply(null, C[1]) }]; return fabric.cachesBoundsOfCurve && (fabric.boundsOfCurveCache[c] = E), E; }, fabric.util.getPointOnPath = function (t, e, i) { i || (i = l(t)); for (var r = 0; 0 < e - i[r].length && r < i.length - 2;)e -= i[r].length, r++; var n, s = i[r], o = e / s.length, a = s.command, c = t[r]; switch (a) { case "M": return { x: s.x, y: s.y, angle: 0 }; case "Z": case "z": return (n = new fabric.Point(s.x, s.y).lerp(new fabric.Point(s.destX, s.destY), o)).angle = Math.atan2(s.destY - s.y, s.destX - s.x), n; case "L": return (n = new fabric.Point(s.x, s.y).lerp(new fabric.Point(c[1], c[2]), o)).angle = Math.atan2(c[2] - s.y, c[1] - s.x), n; case "C": case "Q": return h(s, e); } }, fabric.util.transformPath = function (t, n, e) { return e && (n = fabric.util.multiplyTransformMatrices(n, [1, 0, 0, 1, -e.x, -e.y])), t.map(function (t) { for (var e = t.slice(0), i = {}, r = 1; r < t.length - 1; r += 2)i.x = t[r], i.y = t[r + 1], i = fabric.util.transformPoint(i, n), e[r] = i.x, e[r + 1] = i.y; return e; }); }; }(), function () { var o = Array.prototype.slice; function i(t, e, i) { if (t && 0 !== t.length) { var r = t.length - 1, n = e ? t[r][e] : t[r]; if (e) for (; r--;)i(t[r][e], n) && (n = t[r][e]); else for (; r--;)i(t[r], n) && (n = t[r]); return n; } } fabric.util.array = { fill: function (t, e) { for (var i = t.length; i--;)t[i] = e; return t; }, invoke: function (t, e) { for (var i = o.call(arguments, 2), r = [], n = 0, s = t.length; n < s; n++)r[n] = i.length ? t[n][e].apply(t[n], i) : t[n][e].call(t[n]); return r; }, min: function (t, e) { return i(t, e, function (t, e) { return t < e; }); }, max: function (t, e) { return i(t, e, function (t, e) { return e <= t; }); } }; }(), function () { function o(t, e, i) { if (i) if (!fabric.isLikelyNode && e instanceof Element) t = e; else if (e instanceof Array) { t = []; for (var r = 0, n = e.length; r < n; r++)t[r] = o({}, e[r], i); } else if (e && "object" == typeof e) for (var s in e) "canvas" === s || "group" === s ? t[s] = null : e.hasOwnProperty(s) && (t[s] = o({}, e[s], i)); else t = e; else for (var s in e) t[s] = e[s]; return t; } fabric.util.object = { extend: o, clone: function (t, e) { return o({}, t, e); } }, fabric.util.object.extend(fabric.util, fabric.Observable); }(), function () { function n(t, e) { var i = t.charCodeAt(e); if (isNaN(i)) return ""; if (i < 55296 || 57343 < i) return t.charAt(e); if (55296 <= i && i <= 56319) { if (t.length <= e + 1) throw "High surrogate without following low surrogate"; var r = t.charCodeAt(e + 1); if (r < 56320 || 57343 < r) throw "High surrogate without following low surrogate"; return t.charAt(e) + t.charAt(e + 1); } if (0 === e) throw "Low surrogate without preceding high surrogate"; var n = t.charCodeAt(e - 1); if (n < 55296 || 56319 < n) throw "Low surrogate without preceding high surrogate"; return !1; } fabric.util.string = { camelize: function (t) { return t.replace(/-+(.)?/g, function (t, e) { return e ? e.toUpperCase() : ""; }); }, capitalize: function (t, e) { return t.charAt(0).toUpperCase() + (e ? t.slice(1) : t.slice(1).toLowerCase()); }, escapeXml: function (t) { return t.replace(/&/g, "&amp;").replace(/"/g, "&quot;").replace(/'/g, "&apos;").replace(/</g, "&lt;").replace(/>/g, "&gt;"); }, graphemeSplit: function (t) { var e, i = 0, r = []; for (i = 0; i < t.length; i++)!1 !== (e = n(t, i)) && r.push(e); return r; } }; }(), function () { var s = Array.prototype.slice, o = function () { }, i = function () { for (var t in { toString: 1 }) if ("toString" === t) return !1; return !0; }(), a = function (t, r, n) { for (var e in r) e in t.prototype && "function" == typeof t.prototype[e] && -1 < (r[e] + "").indexOf("callSuper") ? t.prototype[e] = function (i) { return function () { var t = this.constructor.superclass; this.constructor.superclass = n; var e = r[i].apply(this, arguments); if (this.constructor.superclass = t, "initialize" !== i) return e; }; }(e) : t.prototype[e] = r[e], i && (r.toString !== Object.prototype.toString && (t.prototype.toString = r.toString), r.valueOf !== Object.prototype.valueOf && (t.prototype.valueOf = r.valueOf)); }; function c() { } function h(t) { for (var e = null, i = this; i.constructor.superclass;) { var r = i.constructor.superclass.prototype[t]; if (i[t] !== r) { e = r; break; } i = i.constructor.superclass.prototype; } return e ? 1 < arguments.length ? e.apply(this, s.call(arguments, 1)) : e.call(this) : console.log("tried to callSuper " + t + ", method not found in prototype chain", this); } fabric.util.createClass = function () { var t = null, e = s.call(arguments, 0); function i() { this.initialize.apply(this, arguments); } "function" == typeof e[0] && (t = e.shift()), i.superclass = t, i.subclasses = [], t && (c.prototype = t.prototype, i.prototype = new c, t.subclasses.push(i)); for (var r = 0, n = e.length; r < n; r++)a(i, e[r], t); return i.prototype.initialize || (i.prototype.initialize = o), (i.prototype.constructor = i).prototype.callSuper = h, i; }; }(), function () { var n = !!fabric.document.createElement("div").attachEvent, e = ["touchstart", "touchmove", "touchend"]; fabric.util.addListener = function (t, e, i, r) { t && t.addEventListener(e, i, !n && r); }, fabric.util.removeListener = function (t, e, i, r) { t && t.removeEventListener(e, i, !n && r); }, fabric.util.getPointer = function (t) { var e, i, r = t.target, n = fabric.util.getScrollLeftTop(r), s = (i = (e = t).changedTouches) && i[0] ? i[0] : e; return { x: s.clientX + n.left, y: s.clientY + n.top }; }, fabric.util.isTouchEvent = function (t) { return -1 < e.indexOf(t.type) || "touch" === t.pointerType; }; }(), function () { var t = fabric.document.createElement("div"), e = "string" == typeof t.style.opacity, i = "string" == typeof t.style.filter, r = /alpha\s*\(\s*opacity\s*=\s*([^\)]+)\)/, s = function (t) { return t; }; e ? s = function (t, e) { return t.style.opacity = e, t; } : i && (s = function (t, e) { var i = t.style; return t.currentStyle && !t.currentStyle.hasLayout && (i.zoom = 1), r.test(i.filter) ? (e = .9999 <= e ? "" : "alpha(opacity=" + 100 * e + ")", i.filter = i.filter.replace(r, e)) : i.filter += " alpha(opacity=" + 100 * e + ")", t; }), fabric.util.setStyle = function (t, e) { var i = t.style; if (!i) return t; if ("string" == typeof e) return t.style.cssText += ";" + e, -1 < e.indexOf("opacity") ? s(t, e.match(/opacity:\s*(\d?\.?\d*)/)[1]) : t; for (var r in e) if ("opacity" === r) s(t, e[r]); else { var n = "float" === r || "cssFloat" === r ? void 0 === i.styleFloat ? "cssFloat" : "styleFloat" : r; i.setProperty(n, e[r]); } return t; }; }(), function () { var e = Array.prototype.slice; var t, c, i, r, n = function (t) { return e.call(t, 0); }; try { t = n(fabric.document.childNodes) instanceof Array; } catch (t) { } function s(t, e) { var i = fabric.document.createElement(t); for (var r in e) "class" === r ? i.className = e[r] : "for" === r ? i.htmlFor = e[r] : i.setAttribute(r, e[r]); return i; } function h(t) { for (var e = 0, i = 0, r = fabric.document.documentElement, n = fabric.document.body || { scrollLeft: 0, scrollTop: 0 }; t && (t.parentNode || t.host) && ((t = t.parentNode || t.host) === fabric.document ? (e = n.scrollLeft || r.scrollLeft || 0, i = n.scrollTop || r.scrollTop || 0) : (e += t.scrollLeft || 0, i += t.scrollTop || 0), 1 !== t.nodeType || "fixed" !== t.style.position);); return { left: e, top: i }; } t || (n = function (t) { for (var e = new Array(t.length), i = t.length; i--;)e[i] = t[i]; return e; }), c = fabric.document.defaultView && fabric.document.defaultView.getComputedStyle ? function (t, e) { var i = fabric.document.defaultView.getComputedStyle(t, null); return i ? i[e] : void 0; } : function (t, e) { var i = t.style[e]; return !i && t.currentStyle && (i = t.currentStyle[e]), i; }, i = fabric.document.documentElement.style, r = "userSelect" in i ? "userSelect" : "MozUserSelect" in i ? "MozUserSelect" : "WebkitUserSelect" in i ? "WebkitUserSelect" : "KhtmlUserSelect" in i ? "KhtmlUserSelect" : "", fabric.util.makeElementUnselectable = function (t) { return void 0 !== t.onselectstart && (t.onselectstart = fabric.util.falseFunction), r ? t.style[r] = "none" : "string" == typeof t.unselectable && (t.unselectable = "on"), t; }, fabric.util.makeElementSelectable = function (t) { return void 0 !== t.onselectstart && (t.onselectstart = null), r ? t.style[r] = "" : "string" == typeof t.unselectable && (t.unselectable = ""), t; }, fabric.util.setImageSmoothing = function (t, e) { t.imageSmoothingEnabled = t.imageSmoothingEnabled || t.webkitImageSmoothingEnabled || t.mozImageSmoothingEnabled || t.msImageSmoothingEnabled || t.oImageSmoothingEnabled, t.imageSmoothingEnabled = e; }, fabric.util.getById = function (t) { return "string" == typeof t ? fabric.document.getElementById(t) : t; }, fabric.util.toArray = n, fabric.util.addClass = function (t, e) { t && -1 === (" " + t.className + " ").indexOf(" " + e + " ") && (t.className += (t.className ? " " : "") + e); }, fabric.util.makeElement = s, fabric.util.wrapElement = function (t, e, i) { return "string" == typeof e && (e = s(e, i)), t.parentNode && t.parentNode.replaceChild(e, t), e.appendChild(t), e; }, fabric.util.getScrollLeftTop = h, fabric.util.getElementOffset = function (t) { var e, i, r = t && t.ownerDocument, n = { left: 0, top: 0 }, s = { left: 0, top: 0 }, o = { borderLeftWidth: "left", borderTopWidth: "top", paddingLeft: "left", paddingTop: "top" }; if (!r) return s; for (var a in o) s[o[a]] += parseInt(c(t, a), 10) || 0; return e = r.documentElement, void 0 !== t.getBoundingClientRect && (n = t.getBoundingClientRect()), i = h(t), { left: n.left + i.left - (e.clientLeft || 0) + s.left, top: n.top + i.top - (e.clientTop || 0) + s.top }; }, fabric.util.getNodeCanvas = function (t) { var e = fabric.jsdomImplForWrapper(t); return e._canvas || e._image; }, fabric.util.cleanUpJsdomNode = function (t) { if (fabric.isLikelyNode) { var e = fabric.jsdomImplForWrapper(t); e && (e._image = null, e._canvas = null, e._currentSrc = null, e._attributes = null, e._classList = null); } }; }(), function () { function c() { } fabric.util.request = function (t, e) { e || (e = {}); var i, r, n = e.method ? e.method.toUpperCase() : "GET", s = e.onComplete || function () { }, o = new fabric.window.XMLHttpRequest, a = e.body || e.parameters; return o.onreadystatechange = function () { 4 === o.readyState && (s(o), o.onreadystatechange = c); }, "GET" === n && (a = null, "string" == typeof e.parameters && (i = t, r = e.parameters, t = i + (/\?/.test(i) ? "&" : "?") + r)), o.open(n, t, !0), "POST" !== n && "PUT" !== n || o.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"), o.send(a), o; }; }(), fabric.log = console.log, fabric.warn = console.warn, function () { var t = fabric.util.object.extend, i = fabric.util.object.clone, e = []; function r() { return !1; } function n(t, e, i, r) { return -i * Math.cos(t / r * (Math.PI / 2)) + i + e; } fabric.util.object.extend(e, { cancelAll: function () { var t = this.splice(0); return t.forEach(function (t) { t.cancel(); }), t; }, cancelByCanvas: function (e) { if (!e) return []; var t = this.filter(function (t) { return "object" == typeof t.target && t.target.canvas === e; }); return t.forEach(function (t) { t.cancel(); }), t; }, cancelByTarget: function (t) { var e = this.findAnimationsByTarget(t); return e.forEach(function (t) { t.cancel(); }), e; }, findAnimationIndex: function (t) { return this.indexOf(this.findAnimation(t)); }, findAnimation: function (e) { return this.find(function (t) { return t.cancel === e; }); }, findAnimationsByTarget: function (e) { return e ? this.filter(function (t) { return t.target === e; }) : []; } }); var s = fabric.window.requestAnimationFrame || fabric.window.webkitRequestAnimationFrame || fabric.window.mozRequestAnimationFrame || fabric.window.oRequestAnimationFrame || fabric.window.msRequestAnimationFrame || function (t) { return fabric.window.setTimeout(t, 1e3 / 60); }, o = fabric.window.cancelAnimationFrame || fabric.window.clearTimeout; function x() { return s.apply(fabric.window, arguments); } fabric.util.animate = function (e) { e || (e = {}); var b, y = !1, _ = function () { var t = fabric.runningAnimations.indexOf(b); return -1 < t && fabric.runningAnimations.splice(t, 1)[0]; }; return b = t(i(e), { cancel: function () { return y = !0, _(); }, currentValue: "startValue" in e ? e.startValue : 0, completionRate: 0, durationRate: 0 }), fabric.runningAnimations.push(b), x(function (t) { var o, a = t || +new Date, c = e.duration || 500, h = a + c, l = e.onChange || r, u = e.abort || r, f = e.onComplete || r, d = e.easing || n, g = "startValue" in e && 0 < e.startValue.length, p = "startValue" in e ? e.startValue : 0, v = "endValue" in e ? e.endValue : 100, m = e.byValue || (g ? p.map(function (t, e) { return v[e] - p[e]; }) : v - p); e.onStart && e.onStart(), function t(e) { o = e || +new Date; var i = h < o ? c : o - a, r = i / c, n = g ? p.map(function (t, e) { return d(i, p[e], m[e], c); }) : d(i, p, m, c), s = g ? Math.abs((n[0] - p[0]) / m[0]) : Math.abs((n - p) / m); if (b.currentValue = g ? n.slice() : n, b.completionRate = s, b.durationRate = r, !y) { if (!u(n, s, r)) return h < o ? (b.currentValue = g ? v.slice() : v, b.completionRate = 1, b.durationRate = 1, l(g ? v.slice() : v, 1, 1), f(v, 1, 1), void _()) : (l(n, s, r), void x(t)); _(); } }(a); }), b.cancel; }, fabric.util.requestAnimFrame = x, fabric.util.cancelAnimFrame = function () { return o.apply(fabric.window, arguments); }, fabric.runningAnimations = e; }(), function () { function c(t, e, i) { var r = "rgba(" + parseInt(t[0] + i * (e[0] - t[0]), 10) + "," + parseInt(t[1] + i * (e[1] - t[1]), 10) + "," + parseInt(t[2] + i * (e[2] - t[2]), 10); return r += "," + (t && e ? parseFloat(t[3] + i * (e[3] - t[3])) : 1), r += ")"; } fabric.util.animateColor = function (t, e, i, n) { var r = new fabric.Color(t).getSource(), s = new fabric.Color(e).getSource(), o = n.onComplete, a = n.onChange; return n = n || {}, fabric.util.animate(fabric.util.object.extend(n, { duration: i || 500, startValue: r, endValue: s, byValue: s, easing: function (t, e, i, r) { return c(e, i, n.colorEasing ? n.colorEasing(t, r) : 1 - Math.cos(t / r * (Math.PI / 2))); }, onComplete: function (t, e, i) { if (o) return o(c(s, s, 0), e, i); }, onChange: function (t, e, i) { if (a) { if (Array.isArray(t)) return a(c(t, t, 0), e, i); a(t, e, i); } } })); }; }(), function () { function o(t, e, i, r) { return t < Math.abs(e) ? (t = e, r = i / 4) : r = 0 === e && 0 === t ? i / (2 * Math.PI) * Math.asin(1) : i / (2 * Math.PI) * Math.asin(e / t), { a: t, c: e, p: i, s: r }; } function a(t, e, i) { return t.a * Math.pow(2, 10 * (e -= 1)) * Math.sin((e * i - t.s) * (2 * Math.PI) / t.p); } function n(t, e, i, r) { return i - s(r - t, 0, i, r) + e; } function s(t, e, i, r) { return (t /= r) < 1 / 2.75 ? i * (7.5625 * t * t) + e : t < 2 / 2.75 ? i * (7.5625 * (t -= 1.5 / 2.75) * t + .75) + e : t < 2.5 / 2.75 ? i * (7.5625 * (t -= 2.25 / 2.75) * t + .9375) + e : i * (7.5625 * (t -= 2.625 / 2.75) * t + .984375) + e; } fabric.util.ease = { easeInQuad: function (t, e, i, r) { return i * (t /= r) * t + e; }, easeOutQuad: function (t, e, i, r) { return -i * (t /= r) * (t - 2) + e; }, easeInOutQuad: function (t, e, i, r) { return (t /= r / 2) < 1 ? i / 2 * t * t + e : -i / 2 * (--t * (t - 2) - 1) + e; }, easeInCubic: function (t, e, i, r) { return i * (t /= r) * t * t + e; }, easeOutCubic: function (t, e, i, r) { return i * ((t = t / r - 1) * t * t + 1) + e; }, easeInOutCubic: function (t, e, i, r) { return (t /= r / 2) < 1 ? i / 2 * t * t * t + e : i / 2 * ((t -= 2) * t * t + 2) + e; }, easeInQuart: function (t, e, i, r) { return i * (t /= r) * t * t * t + e; }, easeOutQuart: function (t, e, i, r) { return -i * ((t = t / r - 1) * t * t * t - 1) + e; }, easeInOutQuart: function (t, e, i, r) { return (t /= r / 2) < 1 ? i / 2 * t * t * t * t + e : -i / 2 * ((t -= 2) * t * t * t - 2) + e; }, easeInQuint: function (t, e, i, r) { return i * (t /= r) * t * t * t * t + e; }, easeOutQuint: function (t, e, i, r) { return i * ((t = t / r - 1) * t * t * t * t + 1) + e; }, easeInOutQuint: function (t, e, i, r) { return (t /= r / 2) < 1 ? i / 2 * t * t * t * t * t + e : i / 2 * ((t -= 2) * t * t * t * t + 2) + e; }, easeInSine: function (t, e, i, r) { return -i * Math.cos(t / r * (Math.PI / 2)) + i + e; }, easeOutSine: function (t, e, i, r) { return i * Math.sin(t / r * (Math.PI / 2)) + e; }, easeInOutSine: function (t, e, i, r) { return -i / 2 * (Math.cos(Math.PI * t / r) - 1) + e; }, easeInExpo: function (t, e, i, r) { return 0 === t ? e : i * Math.pow(2, 10 * (t / r - 1)) + e; }, easeOutExpo: function (t, e, i, r) { return t === r ? e + i : i * (1 - Math.pow(2, -10 * t / r)) + e; }, easeInOutExpo: function (t, e, i, r) { return 0 === t ? e : t === r ? e + i : (t /= r / 2) < 1 ? i / 2 * Math.pow(2, 10 * (t - 1)) + e : i / 2 * (2 - Math.pow(2, -10 * --t)) + e; }, easeInCirc: function (t, e, i, r) { return -i * (Math.sqrt(1 - (t /= r) * t) - 1) + e; }, easeOutCirc: function (t, e, i, r) { return i * Math.sqrt(1 - (t = t / r - 1) * t) + e; }, easeInOutCirc: function (t, e, i, r) { return (t /= r / 2) < 1 ? -i / 2 * (Math.sqrt(1 - t * t) - 1) + e : i / 2 * (Math.sqrt(1 - (t -= 2) * t) + 1) + e; }, easeInElastic: function (t, e, i, r) { var n = 0; return 0 === t ? e : 1 == (t /= r) ? e + i : (n || (n = .3 * r), -a(o(i, i, n, 1.70158), t, r) + e); }, easeOutElastic: function (t, e, i, r) { var n = 0; if (0 === t) return e; if (1 == (t /= r)) return e + i; n || (n = .3 * r); var s = o(i, i, n, 1.70158); return s.a * Math.pow(2, -10 * t) * Math.sin((t * r - s.s) * (2 * Math.PI) / s.p) + s.c + e; }, easeInOutElastic: function (t, e, i, r) { var n = 0; if (0 === t) return e; if (2 == (t /= r / 2)) return e + i; n || (n = r * (.3 * 1.5)); var s = o(i, i, n, 1.70158); return t < 1 ? -.5 * a(s, t, r) + e : s.a * Math.pow(2, -10 * (t -= 1)) * Math.sin((t * r - s.s) * (2 * Math.PI) / s.p) * .5 + s.c + e; }, easeInBack: function (t, e, i, r, n) { return void 0 === n && (n = 1.70158), i * (t /= r) * t * ((n + 1) * t - n) + e; }, easeOutBack: function (t, e, i, r, n) { return void 0 === n && (n = 1.70158), i * ((t = t / r - 1) * t * ((n + 1) * t + n) + 1) + e; }, easeInOutBack: function (t, e, i, r, n) { return void 0 === n && (n = 1.70158), (t /= r / 2) < 1 ? i / 2 * (t * t * ((1 + (n *= 1.525)) * t - n)) + e : i / 2 * ((t -= 2) * t * ((1 + (n *= 1.525)) * t + n) + 2) + e; }, easeInBounce: n, easeOutBounce: s, easeInOutBounce: function (t, e, i, r) { return t < r / 2 ? .5 * n(2 * t, 0, i, r) + e : .5 * s(2 * t - r, 0, i, r) + .5 * i + e; } }; }(), function (t) { "use strict"; var C = t.fabric || (t.fabric = {}), p = C.util.object.extend, f = C.util.object.clone, v = C.util.toFixed, S = C.util.parseUnit, c = C.util.multiplyTransformMatrices, m = { cx: "left", x: "left", r: "radius", cy: "top", y: "top", display: "visible", visibility: "visible", transform: "transformMatrix", "fill-opacity": "fillOpacity", "fill-rule": "fillRule", "font-family": "fontFamily", "font-size": "fontSize", "font-style": "fontStyle", "font-weight": "fontWeight", "letter-spacing": "charSpacing", "paint-order": "paintFirst", "stroke-dasharray": "strokeDashArray", "stroke-dashoffset": "strokeDashOffset", "stroke-linecap": "strokeLineCap", "stroke-linejoin": "strokeLineJoin", "stroke-miterlimit": "strokeMiterLimit", "stroke-opacity": "strokeOpacity", "stroke-width": "strokeWidth", "text-decoration": "textDecoration", "text-anchor": "textAnchor", opacity: "opacity", "clip-path": "clipPath", "clip-rule": "clipRule", "vector-effect": "strokeUniform", "image-rendering": "imageSmoothing" }, b = { stroke: "strokeOpacity", fill: "fillOpacity" }, y = "font-size", _ = "clip-path"; function x(t, e, i, r) { var n, s = Array.isArray(e); if ("fill" !== t && "stroke" !== t || "none" !== e) { if ("strokeUniform" === t) return "non-scaling-stroke" === e; if ("strokeDashArray" === t) e = "none" === e ? null : e.replace(/,/g, " ").split(/\s+/).map(parseFloat); else if ("transformMatrix" === t) e = i && i.transformMatrix ? c(i.transformMatrix, C.parseTransformAttribute(e)) : C.parseTransformAttribute(e); else if ("visible" === t) e = "none" !== e && "hidden" !== e, i && !1 === i.visible && (e = !1); else if ("opacity" === t) e = parseFloat(e), i && void 0 !== i.opacity && (e *= i.opacity); else if ("textAnchor" === t) e = "start" === e ? "left" : "end" === e ? "right" : "center"; else if ("charSpacing" === t) n = S(e, r) / r * 1e3; else if ("paintFirst" === t) { var o = e.indexOf("fill"), a = e.indexOf("stroke"); e = "fill"; -1 < o && -1 < a && a < o ? e = "stroke" : -1 === o && -1 < a && (e = "stroke"); } else { if ("href" === t || "xlink:href" === t || "font" === t) return e; if ("imageSmoothing" === t) return "optimizeQuality" === e; n = s ? e.map(S) : S(e, r); } } else e = ""; return !s && isNaN(n) ? e : n; } function e(t) { return new RegExp("^(" + t.join("|") + ")\\b", "i"); } function T(t, e) { var i, r, n, s, o = []; for (n = 0, s = e.length; n < s; n++)i = e[n], r = t.getElementsByTagName(i), o = o.concat(Array.prototype.slice.call(r)); return o; } function w(t, e) { var i, r = !0; return (i = n(t, e.pop())) && e.length && (r = function (t, e) { var i, r = !0; for (; t.parentNode && 1 === t.parentNode.nodeType && e.length;)r && (i = e.pop()), t = t.parentNode, r = n(t, i); return 0 === e.length; }(t, e)), i && r && 0 === e.length; } function n(t, e) { var i, r, n = t.nodeName, s = t.getAttribute("class"), o = t.getAttribute("id"); if (i = new RegExp("^" + n, "i"), e = e.replace(i, ""), o && e.length && (i = new RegExp("#" + o + "(?![a-zA-Z\\-]+)", "i"), e = e.replace(i, "")), s && e.length) for (r = (s = s.split(" ")).length; r--;)i = new RegExp("\\." + s[r] + "(?![a-zA-Z\\-]+)", "i"), e = e.replace(i, ""); return 0 === e.length; } function O(t, e) { var i; if (t.getElementById && (i = t.getElementById(e)), i) return i; var r, n, s, o = t.getElementsByTagName("*"); for (n = 0, s = o.length; n < s; n++)if (e === (r = o[n]).getAttribute("id")) return r; } C.svgValidTagNamesRegEx = e(["path", "circle", "polygon", "polyline", "ellipse", "rect", "line", "image", "text"]), C.svgViewBoxElementsRegEx = e(["symbol", "image", "marker", "pattern", "view", "svg"]), C.svgInvalidAncestorsRegEx = e(["pattern", "defs", "symbol", "metadata", "clipPath", "mask", "desc"]), C.svgValidParentsRegEx = e(["symbol", "g", "a", "svg", "clipPath", "defs"]), C.cssRules = {}, C.gradientDefs = {}, C.clipPaths = {}, C.parseTransformAttribute = function () { function b(t, e, i) { t[i] = Math.tan(C.util.degreesToRadians(e[0])); } var y = C.iMatrix, t = C.reNum, e = C.commaWsp, _ = "(?:" + ("(?:(matrix)\\s*\\(\\s*(" + t + ")" + e + "(" + t + ")" + e + "(" + t + ")" + e + "(" + t + ")" + e + "(" + t + ")" + e + "(" + t + ")\\s*\\))") + "|" + ("(?:(translate)\\s*\\(\\s*(" + t + ")(?:" + e + "(" + t + "))?\\s*\\))") + "|" + ("(?:(scale)\\s*\\(\\s*(" + t + ")(?:" + e + "(" + t + "))?\\s*\\))") + "|" + ("(?:(rotate)\\s*\\(\\s*(" + t + ")(?:" + e + "(" + t + ")" + e + "(" + t + "))?\\s*\\))") + "|" + ("(?:(skewX)\\s*\\(\\s*(" + t + ")\\s*\\))") + "|" + ("(?:(skewY)\\s*\\(\\s*(" + t + ")\\s*\\))") + ")", i = new RegExp("^\\s*(?:" + ("(?:" + _ + "(?:" + e + "*" + _ + ")*)") + "?)\\s*$"), r = new RegExp(_, "g"); return function (t) { var v = y.concat(), m = []; if (!t || t && !i.test(t)) return v; t.replace(r, function (t) { var e, i, r, n, s, o, a, c, h, l, u, f, d = new RegExp(_).exec(t).filter(function (t) { return !!t; }), g = d[1], p = d.slice(2).map(parseFloat); switch (g) { case "translate": f = p, (u = v)[4] = f[0], 2 === f.length && (u[5] = f[1]); break; case "rotate": p[0] = C.util.degreesToRadians(p[0]), s = v, o = p, a = C.util.cos(o[0]), c = C.util.sin(o[0]), l = h = 0, 3 === o.length && (h = o[1], l = o[2]), s[0] = a, s[1] = c, s[2] = -c, s[3] = a, s[4] = h - (a * h - c * l), s[5] = l - (c * h + a * l); break; case "scale": e = v, r = (i = p)[0], n = 2 === i.length ? i[1] : i[0], e[0] = r, e[3] = n; break; case "skewX": b(v, p, 2); break; case "skewY": b(v, p, 1); break; case "matrix": v = p; }m.push(v.concat()), v = y.concat(); }); for (var e = m[0]; 1 < m.length;)m.shift(), e = C.util.multiplyTransformMatrices(e, m[0]); return e; }; }(); var k = new RegExp("^\\s*(" + C.reNum + "+)\\s*,?\\s*(" + C.reNum + "+)\\s*,?\\s*(" + C.reNum + "+)\\s*,?\\s*(" + C.reNum + "+)\\s*$"); function P(t) { if (!C.svgViewBoxElementsRegEx.test(t.nodeName)) return {}; var e, i, r, n, s, o, a = t.getAttribute("viewBox"), c = 1, h = 1, l = t.getAttribute("width"), u = t.getAttribute("height"), f = t.getAttribute("x") || 0, d = t.getAttribute("y") || 0, g = t.getAttribute("preserveAspectRatio") || "", p = !a || !(a = a.match(k)), v = !l || !u || "100%" === l || "100%" === u, m = p && v, b = {}, y = "", _ = 0, x = 0; if (b.width = 0, b.height = 0, b.toBeParsed = m, p && (f || d) && t.parentNode && "#document" !== t.parentNode.nodeName && (y = " translate(" + S(f) + " " + S(d) + ") ", s = (t.getAttribute("transform") || "") + y, t.setAttribute("transform", s), t.removeAttribute("x"), t.removeAttribute("y")), m) return b; if (p) return b.width = S(l), b.height = S(u), b; if (e = -parseFloat(a[1]), i = -parseFloat(a[2]), r = parseFloat(a[3]), n = parseFloat(a[4]), b.minX = e, b.minY = i, b.viewBoxWidth = r, b.viewBoxHeight = n, v ? (b.width = r, b.height = n) : (b.width = S(l), b.height = S(u), c = b.width / r, h = b.height / n), "none" !== (g = C.util.parsePreserveAspectRatioAttribute(g)).alignX && ("meet" === g.meetOrSlice && (h = c = h < c ? h : c), "slice" === g.meetOrSlice && (h = c = h < c ? c : h), _ = b.width - r * c, x = b.height - n * c, "Mid" === g.alignX && (_ /= 2), "Mid" === g.alignY && (x /= 2), "Min" === g.alignX && (_ = 0), "Min" === g.alignY && (x = 0)), 1 === c && 1 === h && 0 === e && 0 === i && 0 === f && 0 === d) return b; if ((f || d) && "#document" !== t.parentNode.nodeName && (y = " translate(" + S(f) + " " + S(d) + ") "), s = y + " matrix(" + c + " 0 0 " + h + " " + (e * c + _) + " " + (i * h + x) + ") ", "svg" === t.nodeName) { for (o = t.ownerDocument.createElementNS(C.svgNS, "g"); t.firstChild;)o.appendChild(t.firstChild); t.appendChild(o); } else (o = t).removeAttribute("x"), o.removeAttribute("y"), s = o.getAttribute("transform") + s; return o.setAttribute("transform", s), b; } function s(t, e) { var i = "xlink:href", r = O(t, e.getAttribute(i).slice(1)); if (r && r.getAttribute(i) && s(t, r), ["gradientTransform", "x1", "x2", "y1", "y2", "gradientUnits", "cx", "cy", "r", "fx", "fy"].forEach(function (t) { r && !e.hasAttribute(t) && r.hasAttribute(t) && e.setAttribute(t, r.getAttribute(t)); }), !e.children.length) for (var n = r.cloneNode(!0); n.firstChild;)e.appendChild(n.firstChild); e.removeAttribute(i); } C.parseSVGDocument = function (t, i, e, r) { if (t) { !function (t) { for (var e = T(t, ["use", "svg:use"]), i = 0; e.length && i < e.length;) { var r = e[i], n = r.getAttribute("xlink:href") || r.getAttribute("href"); if (null === n) return; var s, o, a, c, h = n.slice(1), l = r.getAttribute("x") || 0, u = r.getAttribute("y") || 0, f = O(t, h).cloneNode(!0), d = (f.getAttribute("transform") || "") + " translate(" + l + ", " + u + ")", g = e.length, p = C.svgNS; if (P(f), /^svg$/i.test(f.nodeName)) { var v = f.ownerDocument.createElementNS(p, "g"); for (o = 0, c = (a = f.attributes).length; o < c; o++)s = a.item(o), v.setAttributeNS(p, s.nodeName, s.nodeValue); for (; f.firstChild;)v.appendChild(f.firstChild); f = v; } for (o = 0, c = (a = r.attributes).length; o < c; o++)"x" !== (s = a.item(o)).nodeName && "y" !== s.nodeName && "xlink:href" !== s.nodeName && "href" !== s.nodeName && ("transform" === s.nodeName ? d = s.nodeValue + " " + d : f.setAttribute(s.nodeName, s.nodeValue)); f.setAttribute("transform", d), f.setAttribute("instantiated_by_use", "1"), f.removeAttribute("id"), r.parentNode.replaceChild(f, r), e.length === g && i++; } }(t); var n, s, o = C.Object.__uid++, a = P(t), c = C.util.toArray(t.getElementsByTagName("*")); if (a.crossOrigin = r && r.crossOrigin, a.svgUid = o, 0 === c.length && C.isLikelyNode) { var h = []; for (n = 0, s = (c = t.selectNodes('//*[name(.)!="svg"]')).length; n < s; n++)h[n] = c[n]; c = h; } var l = c.filter(function (t) { return P(t), C.svgValidTagNamesRegEx.test(t.nodeName.replace("svg:", "")) && !function (t, e) { for (; t && (t = t.parentNode);)if (t.nodeName && e.test(t.nodeName.replace("svg:", "")) && !t.getAttribute("instantiated_by_use")) return !0; return !1; }(t, C.svgInvalidAncestorsRegEx); }); if (!l || l && !l.length) i && i([], {}); else { var u = {}; c.filter(function (t) { return "clipPath" === t.nodeName.replace("svg:", ""); }).forEach(function (t) { var e = t.getAttribute("id"); u[e] = C.util.toArray(t.getElementsByTagName("*")).filter(function (t) { return C.svgValidTagNamesRegEx.test(t.nodeName.replace("svg:", "")); }); }), C.gradientDefs[o] = C.getGradientDefs(t), C.cssRules[o] = C.getCSSRules(t), C.clipPaths[o] = u, C.parseElements(l, function (t, e) { i && (i(t, a, e, c), delete C.gradientDefs[o], delete C.cssRules[o], delete C.clipPaths[o]); }, f(a), e, r); } } }; var h = new RegExp("(normal|italic)?\\s*(normal|small-caps)?\\s*(normal|bold|bolder|lighter|100|200|300|400|500|600|700|800|900)?\\s*(" + C.reNum + "(?:px|cm|mm|em|pt|pc|in)*)(?:\\/(normal|" + C.reNum + "))?\\s+(.*)"); p(C, { parseFontDeclaration: function (t, e) { var i = t.match(h); if (i) { var r = i[1], n = i[3], s = i[4], o = i[5], a = i[6]; r && (e.fontStyle = r), n && (e.fontWeight = isNaN(parseFloat(n)) ? n : parseFloat(n)), s && (e.fontSize = S(s)), a && (e.fontFamily = a), o && (e.lineHeight = "normal" === o ? 1 : o); } }, getGradientDefs: function (t) { var e, i = T(t, ["linearGradient", "radialGradient", "svg:linearGradient", "svg:radialGradient"]), r = 0, n = {}; for (r = i.length; r--;)(e = i[r]).getAttribute("xlink:href") && s(t, e), n[e.getAttribute("id")] = e; return n; }, parseAttributes: function (i, t, e) { if (i) { var r, n, s, o = {}; void 0 === e && (e = i.getAttribute("svgUid")), i.parentNode && C.svgValidParentsRegEx.test(i.parentNode.nodeName) && (o = C.parseAttributes(i.parentNode, t, e)); var a = t.reduce(function (t, e) { return (r = i.getAttribute(e)) && (t[e] = r), t; }, {}), c = p(function (t, e) { var i = {}; for (var r in C.cssRules[e]) if (w(t, r.split(" "))) for (var n in C.cssRules[e][r]) i[n] = C.cssRules[e][r][n]; return i; }(i, e), C.parseStyleAttribute(i)); a = p(a, c), c[_] && i.setAttribute(_, c[_]), n = s = o.fontSize || C.Text.DEFAULT_SVG_FONT_SIZE, a[y] && (a[y] = n = S(a[y], s)); var h, l, u, f = {}; for (var d in a) l = x(h = (u = d) in m ? m[u] : u, a[d], o, n), f[h] = l; f && f.font && C.parseFontDeclaration(f.font, f); var g = p(o, f); return C.svgValidParentsRegEx.test(i.nodeName) ? g : function (t) { for (var e in b) if (void 0 !== t[b[e]] && "" !== t[e]) { if (void 0 === t[e]) { if (!C.Object.prototype[e]) continue; t[e] = C.Object.prototype[e]; } if (0 !== t[e].indexOf("url(")) { var i = new C.Color(t[e]); t[e] = i.setAlpha(v(i.getAlpha() * t[b[e]], 2)).toRgba(); } } return t; }(g); } }, parseElements: function (t, e, i, r, n) { new C.ElementsParser(t, e, i, r, n).parse(); }, parseStyleAttribute: function (t) { var i, r, n, e = {}, s = t.getAttribute("style"); return s && ("string" == typeof s ? (i = e, s.replace(/;\s*$/, "").split(";").forEach(function (t) { var e = t.split(":"); r = e[0].trim().toLowerCase(), n = e[1].trim(), i[r] = n; })) : function (t, e) { var i, r; for (var n in t) void 0 !== t[n] && (i = n.toLowerCase(), r = t[n], e[i] = r); }(s, e)), e; }, parsePointsAttribute: function (t) { if (!t) return null; var e, i, r = []; for (e = 0, i = (t = (t = t.replace(/,/g, " ").trim()).split(/\s+/)).length; e < i; e += 2)r.push({ x: parseFloat(t[e]), y: parseFloat(t[e + 1]) }); return r; }, getCSSRules: function (t) { var a, c, e = t.getElementsByTagName("style"), h = {}; for (a = 0, c = e.length; a < c; a++) { var i = e[a].textContent; "" !== (i = i.replace(/\/\*[\s\S]*?\*\//g, "")).trim() && i.split("}").filter(function (t) { return t.trim(); }).forEach(function (t) { var e = t.split("{"), i = {}, r = e[1].trim().split(";").filter(function (t) { return t.trim(); }); for (a = 0, c = r.length; a < c; a++) { var n = r[a].split(":"), s = n[0].trim(), o = n[1].trim(); i[s] = o; } (t = e[0].trim()).split(",").forEach(function (t) { "" !== (t = t.replace(/^svg/i, "").trim()) && (h[t] ? C.util.object.extend(h[t], i) : h[t] = C.util.object.clone(i)); }); }); } return h; }, loadSVGFromURL: function (t, n, i, r) { t = t.replace(/^\n\s*/, "").trim(), new C.util.request(t, { method: "get", onComplete: function (t) { var e = t.responseXML; if (!e || !e.documentElement) return n && n(null), !1; C.parseSVGDocument(e.documentElement, function (t, e, i, r) { n && n(t, e, i, r); }, i, r); } }); }, loadSVGFromString: function (t, n, e, i) { var r = (new C.window.DOMParser).parseFromString(t.trim(), "text/xml"); C.parseSVGDocument(r.documentElement, function (t, e, i, r) { n(t, e, i, r); }, e, i); } }); }("undefined" != typeof exports ? exports : this), fabric.ElementsParser = function (t, e, i, r, n, s) { this.elements = t, this.callback = e, this.options = i, this.reviver = r, this.svgUid = i && i.svgUid || 0, this.parsingOptions = n, this.regexUrl = /^url\(['"]?#([^'"]+)['"]?\)/g, this.doc = s; }, function (t) { t.parse = function () { this.instances = new Array(this.elements.length), this.numElements = this.elements.length, this.createObjects(); }, t.createObjects = function () { var i = this; this.elements.forEach(function (t, e) { t.setAttribute("svgUid", i.svgUid), i.createObject(t, e); }); }, t.findTag = function (t) { return fabric[fabric.util.string.capitalize(t.tagName.replace("svg:", ""))]; }, t.createObject = function (t, e) { var i = this.findTag(t); if (i && i.fromElement) try { i.fromElement(t, this.createCallback(e, t), this.options); } catch (t) { fabric.log(t); } else this.checkIfDone(); }, t.createCallback = function (i, r) { var n = this; return function (t) { var e; n.resolveGradient(t, r, "fill"), n.resolveGradient(t, r, "stroke"), t instanceof fabric.Image && t._originalElement && (e = t.parsePreserveAspectRatioAttribute(r)), t._removeTransformMatrix(e), n.resolveClipPath(t, r), n.reviver && n.reviver(r, t), n.instances[i] = t, n.checkIfDone(); }; }, t.extractPropertyDefinition = function (t, e, i) { var r = t[e], n = this.regexUrl; if (n.test(r)) { n.lastIndex = 0; var s = n.exec(r)[1]; return n.lastIndex = 0, fabric[i][this.svgUid][s]; } }, t.resolveGradient = function (t, e, i) { var r = this.extractPropertyDefinition(t, i, "gradientDefs"); if (r) { var n = e.getAttribute(i + "-opacity"), s = fabric.Gradient.fromElement(r, t, n, this.options); t.set(i, s); } }, t.createClipPathCallback = function (t, e) { return function (t) { t._removeTransformMatrix(), t.fillRule = t.clipRule, e.push(t); }; }, t.resolveClipPath = function (t, e) { var i, r, n, s, o = this.extractPropertyDefinition(t, "clipPath", "clipPaths"); if (o) { n = [], r = fabric.util.invertTransform(t.calcTransformMatrix()); for (var a = o[0].parentNode, c = e; c.parentNode && c.getAttribute("clip-path") !== t.clipPath;)c = c.parentNode; c.parentNode.appendChild(a); for (var h = 0; h < o.length; h++)i = o[h], this.findTag(i).fromElement(i, this.createClipPathCallback(t, n), this.options); o = 1 === n.length ? n[0] : new fabric.Group(n), s = fabric.util.multiplyTransformMatrices(r, o.calcTransformMatrix()), o.clipPath && this.resolveClipPath(o, c); var l = fabric.util.qrDecompose(s); o.flipX = !1, o.flipY = !1, o.set("scaleX", l.scaleX), o.set("scaleY", l.scaleY), o.angle = l.angle, o.skewX = l.skewX, o.skewY = 0, o.setPositionByOrigin({ x: l.translateX, y: l.translateY }, "center", "center"), t.clipPath = o; } else delete t.clipPath; }, t.checkIfDone = function () { 0 == --this.numElements && (this.instances = this.instances.filter(function (t) { return null != t; }), this.callback(this.instances, this.elements)); }; }(fabric.ElementsParser.prototype), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}); function i(t, e) { this.x = t, this.y = e; } e.Point ? e.warn("fabric.Point is already defined") : (e.Point = i).prototype = { type: "point", constructor: i, add: function (t) { return new i(this.x + t.x, this.y + t.y); }, addEquals: function (t) { return this.x += t.x, this.y += t.y, this; }, scalarAdd: function (t) { return new i(this.x + t, this.y + t); }, scalarAddEquals: function (t) { return this.x += t, this.y += t, this; }, subtract: function (t) { return new i(this.x - t.x, this.y - t.y); }, subtractEquals: function (t) { return this.x -= t.x, this.y -= t.y, this; }, scalarSubtract: function (t) { return new i(this.x - t, this.y - t); }, scalarSubtractEquals: function (t) { return this.x -= t, this.y -= t, this; }, multiply: function (t) { return new i(this.x * t, this.y * t); }, multiplyEquals: function (t) { return this.x *= t, this.y *= t, this; }, divide: function (t) { return new i(this.x / t, this.y / t); }, divideEquals: function (t) { return this.x /= t, this.y /= t, this; }, eq: function (t) { return this.x === t.x && this.y === t.y; }, lt: function (t) { return this.x < t.x && this.y < t.y; }, lte: function (t) { return this.x <= t.x && this.y <= t.y; }, gt: function (t) { return this.x > t.x && this.y > t.y; }, gte: function (t) { return this.x >= t.x && this.y >= t.y; }, lerp: function (t, e) { return void 0 === e && (e = .5), e = Math.max(Math.min(1, e), 0), new i(this.x + (t.x - this.x) * e, this.y + (t.y - this.y) * e); }, distanceFrom: function (t) { var e = this.x - t.x, i = this.y - t.y; return Math.sqrt(e * e + i * i); }, midPointFrom: function (t) { return this.lerp(t); }, min: function (t) { return new i(Math.min(this.x, t.x), Math.min(this.y, t.y)); }, max: function (t) { return new i(Math.max(this.x, t.x), Math.max(this.y, t.y)); }, toString: function () { return this.x + "," + this.y; }, setXY: function (t, e) { return this.x = t, this.y = e, this; }, setX: function (t) { return this.x = t, this; }, setY: function (t) { return this.y = t, this; }, setFromPoint: function (t) { return this.x = t.x, this.y = t.y, this; }, swap: function (t) { var e = this.x, i = this.y; this.x = t.x, this.y = t.y, t.x = e, t.y = i; }, clone: function () { return new i(this.x, this.y); } }; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var f = t.fabric || (t.fabric = {}); function d(t) { this.status = t, this.points = []; } f.Intersection ? f.warn("fabric.Intersection is already defined") : (f.Intersection = d, f.Intersection.prototype = { constructor: d, appendPoint: function (t) { return this.points.push(t), this; }, appendPoints: function (t) { return this.points = this.points.concat(t), this; } }, f.Intersection.intersectLineLine = function (t, e, i, r) { var n, s = (r.x - i.x) * (t.y - i.y) - (r.y - i.y) * (t.x - i.x), o = (e.x - t.x) * (t.y - i.y) - (e.y - t.y) * (t.x - i.x), a = (r.y - i.y) * (e.x - t.x) - (r.x - i.x) * (e.y - t.y); if (0 !== a) { var c = s / a, h = o / a; 0 <= c && c <= 1 && 0 <= h && h <= 1 ? (n = new d("Intersection")).appendPoint(new f.Point(t.x + c * (e.x - t.x), t.y + c * (e.y - t.y))) : n = new d; } else n = new d(0 === s || 0 === o ? "Coincident" : "Parallel"); return n; }, f.Intersection.intersectLinePolygon = function (t, e, i) { var r, n, s, o, a = new d, c = i.length; for (o = 0; o < c; o++)r = i[o], n = i[(o + 1) % c], s = d.intersectLineLine(t, e, r, n), a.appendPoints(s.points); return 0 < a.points.length && (a.status = "Intersection"), a; }, f.Intersection.intersectPolygonPolygon = function (t, e) { var i, r = new d, n = t.length; for (i = 0; i < n; i++) { var s = t[i], o = t[(i + 1) % n], a = d.intersectLinePolygon(s, o, e); r.appendPoints(a.points); } return 0 < r.points.length && (r.status = "Intersection"), r; }, f.Intersection.intersectPolygonRectangle = function (t, e, i) { var r = e.min(i), n = e.max(i), s = new f.Point(n.x, r.y), o = new f.Point(r.x, n.y), a = d.intersectLinePolygon(r, s, t), c = d.intersectLinePolygon(s, n, t), h = d.intersectLinePolygon(n, o, t), l = d.intersectLinePolygon(o, r, t), u = new d; return u.appendPoints(a.points), u.appendPoints(c.points), u.appendPoints(h.points), u.appendPoints(l.points), 0 < u.points.length && (u.status = "Intersection"), u; }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var h = t.fabric || (t.fabric = {}); function l(t) { t ? this._tryParsingColor(t) : this.setSource([0, 0, 0, 1]); } function u(t, e, i) { return i < 0 && (i += 1), 1 < i && (i -= 1), i < 1 / 6 ? t + 6 * (e - t) * i : i < .5 ? e : i < 2 / 3 ? t + (e - t) * (2 / 3 - i) * 6 : t; } h.Color ? h.warn("fabric.Color is already defined.") : (h.Color = l, h.Color.prototype = { _tryParsingColor: function (t) { var e; t in l.colorNameMap && (t = l.colorNameMap[t]), "transparent" === t && (e = [255, 255, 255, 0]), e || (e = l.sourceFromHex(t)), e || (e = l.sourceFromRgb(t)), e || (e = l.sourceFromHsl(t)), e || (e = [0, 0, 0, 1]), e && this.setSource(e); }, _rgbToHsl: function (t, e, i) { t /= 255, e /= 255, i /= 255; var r, n, s, o = h.util.array.max([t, e, i]), a = h.util.array.min([t, e, i]); if (s = (o + a) / 2, o === a) r = n = 0; else { var c = o - a; switch (n = .5 < s ? c / (2 - o - a) : c / (o + a), o) { case t: r = (e - i) / c + (e < i ? 6 : 0); break; case e: r = (i - t) / c + 2; break; case i: r = (t - e) / c + 4; }r /= 6; } return [Math.round(360 * r), Math.round(100 * n), Math.round(100 * s)]; }, getSource: function () { return this._source; }, setSource: function (t) { this._source = t; }, toRgb: function () { var t = this.getSource(); return "rgb(" + t[0] + "," + t[1] + "," + t[2] + ")"; }, toRgba: function () { var t = this.getSource(); return "rgba(" + t[0] + "," + t[1] + "," + t[2] + "," + t[3] + ")"; }, toHsl: function () { var t = this.getSource(), e = this._rgbToHsl(t[0], t[1], t[2]); return "hsl(" + e[0] + "," + e[1] + "%," + e[2] + "%)"; }, toHsla: function () { var t = this.getSource(), e = this._rgbToHsl(t[0], t[1], t[2]); return "hsla(" + e[0] + "," + e[1] + "%," + e[2] + "%," + t[3] + ")"; }, toHex: function () { var t, e, i, r = this.getSource(); return t = 1 === (t = r[0].toString(16)).length ? "0" + t : t, e = 1 === (e = r[1].toString(16)).length ? "0" + e : e, i = 1 === (i = r[2].toString(16)).length ? "0" + i : i, t.toUpperCase() + e.toUpperCase() + i.toUpperCase(); }, toHexa: function () { var t, e = this.getSource(); return t = 1 === (t = (t = Math.round(255 * e[3])).toString(16)).length ? "0" + t : t, this.toHex() + t.toUpperCase(); }, getAlpha: function () { return this.getSource()[3]; }, setAlpha: function (t) { var e = this.getSource(); return e[3] = t, this.setSource(e), this; }, toGrayscale: function () { var t = this.getSource(), e = parseInt((.3 * t[0] + .59 * t[1] + .11 * t[2]).toFixed(0), 10), i = t[3]; return this.setSource([e, e, e, i]), this; }, toBlackWhite: function (t) { var e = this.getSource(), i = (.3 * e[0] + .59 * e[1] + .11 * e[2]).toFixed(0), r = e[3]; return t = t || 127, i = Number(i) < Number(t) ? 0 : 255, this.setSource([i, i, i, r]), this; }, overlayWith: function (t) { t instanceof l || (t = new l(t)); var e, i = [], r = this.getAlpha(), n = this.getSource(), s = t.getSource(); for (e = 0; e < 3; e++)i.push(Math.round(.5 * n[e] + .5 * s[e])); return i[3] = r, this.setSource(i), this; } }, h.Color.reRGBa = /^rgba?\(\s*(\d{1,3}(?:\.\d+)?\%?)\s*,\s*(\d{1,3}(?:\.\d+)?\%?)\s*,\s*(\d{1,3}(?:\.\d+)?\%?)\s*(?:\s*,\s*((?:\d*\.?\d+)?)\s*)?\)$/i, h.Color.reHSLa = /^hsla?\(\s*(\d{1,3})\s*,\s*(\d{1,3}\%)\s*,\s*(\d{1,3}\%)\s*(?:\s*,\s*(\d+(?:\.\d+)?)\s*)?\)$/i, h.Color.reHex = /^#?([0-9a-f]{8}|[0-9a-f]{6}|[0-9a-f]{4}|[0-9a-f]{3})$/i, h.Color.colorNameMap = { aliceblue: "#F0F8FF", antiquewhite: "#FAEBD7", aqua: "#00FFFF", aquamarine: "#7FFFD4", azure: "#F0FFFF", beige: "#F5F5DC", bisque: "#FFE4C4", black: "#000000", blanchedalmond: "#FFEBCD", blue: "#0000FF", blueviolet: "#8A2BE2", brown: "#A52A2A", burlywood: "#DEB887", cadetblue: "#5F9EA0", chartreuse: "#7FFF00", chocolate: "#D2691E", coral: "#FF7F50", cornflowerblue: "#6495ED", cornsilk: "#FFF8DC", crimson: "#DC143C", cyan: "#00FFFF", darkblue: "#00008B", darkcyan: "#008B8B", darkgoldenrod: "#B8860B", darkgray: "#A9A9A9", darkgrey: "#A9A9A9", darkgreen: "#006400", darkkhaki: "#BDB76B", darkmagenta: "#8B008B", darkolivegreen: "#556B2F", darkorange: "#FF8C00", darkorchid: "#9932CC", darkred: "#8B0000", darksalmon: "#E9967A", darkseagreen: "#8FBC8F", darkslateblue: "#483D8B", darkslategray: "#2F4F4F", darkslategrey: "#2F4F4F", darkturquoise: "#00CED1", darkviolet: "#9400D3", deeppink: "#FF1493", deepskyblue: "#00BFFF", dimgray: "#696969", dimgrey: "#696969", dodgerblue: "#1E90FF", firebrick: "#B22222", floralwhite: "#FFFAF0", forestgreen: "#228B22", fuchsia: "#FF00FF", gainsboro: "#DCDCDC", ghostwhite: "#F8F8FF", gold: "#FFD700", goldenrod: "#DAA520", gray: "#808080", grey: "#808080", green: "#008000", greenyellow: "#ADFF2F", honeydew: "#F0FFF0", hotpink: "#FF69B4", indianred: "#CD5C5C", indigo: "#4B0082", ivory: "#FFFFF0", khaki: "#F0E68C", lavender: "#E6E6FA", lavenderblush: "#FFF0F5", lawngreen: "#7CFC00", lemonchiffon: "#FFFACD", lightblue: "#ADD8E6", lightcoral: "#F08080", lightcyan: "#E0FFFF", lightgoldenrodyellow: "#FAFAD2", lightgray: "#D3D3D3", lightgrey: "#D3D3D3", lightgreen: "#90EE90", lightpink: "#FFB6C1", lightsalmon: "#FFA07A", lightseagreen: "#20B2AA", lightskyblue: "#87CEFA", lightslategray: "#778899", lightslategrey: "#778899", lightsteelblue: "#B0C4DE", lightyellow: "#FFFFE0", lime: "#00FF00", limegreen: "#32CD32", linen: "#FAF0E6", magenta: "#FF00FF", maroon: "#800000", mediumaquamarine: "#66CDAA", mediumblue: "#0000CD", mediumorchid: "#BA55D3", mediumpurple: "#9370DB", mediumseagreen: "#3CB371", mediumslateblue: "#7B68EE", mediumspringgreen: "#00FA9A", mediumturquoise: "#48D1CC", mediumvioletred: "#C71585", midnightblue: "#191970", mintcream: "#F5FFFA", mistyrose: "#FFE4E1", moccasin: "#FFE4B5", navajowhite: "#FFDEAD", navy: "#000080", oldlace: "#FDF5E6", olive: "#808000", olivedrab: "#6B8E23", orange: "#FFA500", orangered: "#FF4500", orchid: "#DA70D6", palegoldenrod: "#EEE8AA", palegreen: "#98FB98", paleturquoise: "#AFEEEE", palevioletred: "#DB7093", papayawhip: "#FFEFD5", peachpuff: "#FFDAB9", peru: "#CD853F", pink: "#FFC0CB", plum: "#DDA0DD", powderblue: "#B0E0E6", purple: "#800080", rebeccapurple: "#663399", red: "#FF0000", rosybrown: "#BC8F8F", royalblue: "#4169E1", saddlebrown: "#8B4513", salmon: "#FA8072", sandybrown: "#F4A460", seagreen: "#2E8B57", seashell: "#FFF5EE", sienna: "#A0522D", silver: "#C0C0C0", skyblue: "#87CEEB", slateblue: "#6A5ACD", slategray: "#708090", slategrey: "#708090", snow: "#FFFAFA", springgreen: "#00FF7F", steelblue: "#4682B4", tan: "#D2B48C", teal: "#008080", thistle: "#D8BFD8", tomato: "#FF6347", turquoise: "#40E0D0", violet: "#EE82EE", wheat: "#F5DEB3", white: "#FFFFFF", whitesmoke: "#F5F5F5", yellow: "#FFFF00", yellowgreen: "#9ACD32" }, h.Color.fromRgb = function (t) { return l.fromSource(l.sourceFromRgb(t)); }, h.Color.sourceFromRgb = function (t) { var e = t.match(l.reRGBa); if (e) { var i = parseInt(e[1], 10) / (/%$/.test(e[1]) ? 100 : 1) * (/%$/.test(e[1]) ? 255 : 1), r = parseInt(e[2], 10) / (/%$/.test(e[2]) ? 100 : 1) * (/%$/.test(e[2]) ? 255 : 1), n = parseInt(e[3], 10) / (/%$/.test(e[3]) ? 100 : 1) * (/%$/.test(e[3]) ? 255 : 1); return [parseInt(i, 10), parseInt(r, 10), parseInt(n, 10), e[4] ? parseFloat(e[4]) : 1]; } }, h.Color.fromRgba = l.fromRgb, h.Color.fromHsl = function (t) { return l.fromSource(l.sourceFromHsl(t)); }, h.Color.sourceFromHsl = function (t) { var e = t.match(l.reHSLa); if (e) { var i, r, n, s = (parseFloat(e[1]) % 360 + 360) % 360 / 360, o = parseFloat(e[2]) / (/%$/.test(e[2]) ? 100 : 1), a = parseFloat(e[3]) / (/%$/.test(e[3]) ? 100 : 1); if (0 === o) i = r = n = a; else { var c = a <= .5 ? a * (o + 1) : a + o - a * o, h = 2 * a - c; i = u(h, c, s + 1 / 3), r = u(h, c, s), n = u(h, c, s - 1 / 3); } return [Math.round(255 * i), Math.round(255 * r), Math.round(255 * n), e[4] ? parseFloat(e[4]) : 1]; } }, h.Color.fromHsla = l.fromHsl, h.Color.fromHex = function (t) { return l.fromSource(l.sourceFromHex(t)); }, h.Color.sourceFromHex = function (t) { if (t.match(l.reHex)) { var e = t.slice(t.indexOf("#") + 1), i = 3 === e.length || 4 === e.length, r = 8 === e.length || 4 === e.length, n = i ? e.charAt(0) + e.charAt(0) : e.substring(0, 2), s = i ? e.charAt(1) + e.charAt(1) : e.substring(2, 4), o = i ? e.charAt(2) + e.charAt(2) : e.substring(4, 6), a = r ? i ? e.charAt(3) + e.charAt(3) : e.substring(6, 8) : "FF"; return [parseInt(n, 16), parseInt(s, 16), parseInt(o, 16), parseFloat((parseInt(a, 16) / 255).toFixed(2))]; } }, h.Color.fromSource = function (t) { var e = new l; return e.setSource(t), e; }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var l = t.fabric || (t.fabric = {}), o = ["e", "se", "s", "sw", "w", "nw", "n", "ne", "e"], s = ["ns", "nesw", "ew", "nwse"], n = {}, f = "left", d = "top", g = "right", p = "bottom", c = "center", S = { top: p, bottom: d, left: g, right: f, center: c }, v = l.util.radiansToDegrees, T = Math.sign || function (t) { return (0 < t) - (t < 0) || +t; }; function a(t, e) { var i = t.angle + v(Math.atan2(e.y, e.x)) + 360; return Math.round(i % 360 / 45); } function h(t, e) { var i = e.transform.target, r = i.canvas, n = l.util.object.clone(e); n.target = i, r && r.fire("object:" + t, n), i.fire(t, e); } function w(t, e) { var i = e.canvas, r = t[i.uniScaleKey]; return i.uniformScaling && !r || !i.uniformScaling && r; } function O(t) { return t.originX === c && t.originY === c; } function k(t, e, i) { var r = t.lockScalingX, n = t.lockScalingY; return !(!r || !n) || (!(e || !r && !n || !i) || (!(!r || "x" !== e) || !(!n || "y" !== e))); } function u(t, e, i, r) { return { e: t, transform: e, pointer: { x: i, y: r } }; } function m(c) { return function (t, e, i, r) { var n = e.target, s = n.getCenterPoint(), o = n.translateToOriginPoint(s, e.originX, e.originY), a = c(t, e, i, r); return n.setPositionByOrigin(o, e.originX, e.originY), a; }; } function b(s, o) { return function (t, e, i, r) { var n = o(t, e, i, r); return n && h(s, u(t, e, i, r)), n; }; } function P(t, e, i, r, n) { var s = t.target, o = s.controls[t.corner], a = s.canvas.getZoom(), c = s.padding / a, h = s.toLocalPoint(new l.Point(r, n), e, i); return h.x >= c && (h.x -= c), h.x <= -c && (h.x += c), h.y >= c && (h.y -= c), h.y <= c && (h.y += c), h.x -= o.offsetX, h.y -= o.offsetY, h; } function y(t) { return t.flipX !== t.flipY; } function _(t, e, i, r, n) { if (0 !== t[e]) { var s = n / t._getTransformedDimensions()[r] * t[i]; t.set(i, s); } } function x(t, e, i, r) { var n, s = e.target, o = s._getTransformedDimensions(0, s.skewY), a = P(e, e.originX, e.originY, i, r), c = Math.abs(2 * a.x) - o.x, h = s.skewX; c < 2 ? n = 0 : (n = v(Math.atan2(c / s.scaleX, o.y / s.scaleY)), e.originX === f && e.originY === p && (n = -n), e.originX === g && e.originY === d && (n = -n), y(s) && (n = -n)); var l = h !== n; if (l) { var u = s._getTransformedDimensions().y; s.set("skewX", n), _(s, "skewY", "scaleY", "y", u); } return l; } function C(t, e, i, r) { var n, s = e.target, o = s._getTransformedDimensions(s.skewX, 0), a = P(e, e.originX, e.originY, i, r), c = Math.abs(2 * a.y) - o.y, h = s.skewY; c < 2 ? n = 0 : (n = v(Math.atan2(c / s.scaleY, o.x / s.scaleX)), e.originX === f && e.originY === p && (n = -n), e.originX === g && e.originY === d && (n = -n), y(s) && (n = -n)); var l = h !== n; if (l) { var u = s._getTransformedDimensions().x; s.set("skewY", n), _(s, "skewX", "scaleX", "x", u); } return l; } function E(t, e, i, r, n) { n = n || {}; var s, o, a, c, h, l, u = e.target, f = u.lockScalingX, d = u.lockScalingY, g = n.by, p = w(t, u), v = k(u, g, p), m = e.gestureScale; if (v) return !1; if (m) o = e.scaleX * m, a = e.scaleY * m; else { if (s = P(e, e.originX, e.originY, i, r), h = "y" !== g ? T(s.x) : 1, l = "x" !== g ? T(s.y) : 1, e.signX || (e.signX = h), e.signY || (e.signY = l), u.lockScalingFlip && (e.signX !== h || e.signY !== l)) return !1; if (c = u._getTransformedDimensions(), p && !g) { var b = Math.abs(s.x) + Math.abs(s.y), y = e.original, _ = b / (Math.abs(c.x * y.scaleX / u.scaleX) + Math.abs(c.y * y.scaleY / u.scaleY)); o = y.scaleX * _, a = y.scaleY * _; } else o = Math.abs(s.x * u.scaleX / c.x), a = Math.abs(s.y * u.scaleY / c.y); O(e) && (o *= 2, a *= 2), e.signX !== h && "y" !== g && (e.originX = S[e.originX], o *= -1, e.signX = h), e.signY !== l && "x" !== g && (e.originY = S[e.originY], a *= -1, e.signY = l); } var x = u.scaleX, C = u.scaleY; return g ? ("x" === g && u.set("scaleX", o), "y" === g && u.set("scaleY", a)) : (!f && u.set("scaleX", o), !d && u.set("scaleY", a)), x !== u.scaleX || C !== u.scaleY; } n.scaleCursorStyleHandler = function (t, e, i) { var r = w(t, i), n = ""; if (0 !== e.x && 0 === e.y ? n = "x" : 0 === e.x && 0 !== e.y && (n = "y"), k(i, n, r)) return "not-allowed"; var s = a(i, e); return o[s] + "-resize"; }, n.skewCursorStyleHandler = function (t, e, i) { var r = "not-allowed"; if (0 !== e.x && i.lockSkewingY) return r; if (0 !== e.y && i.lockSkewingX) return r; var n = a(i, e) % 4; return s[n] + "-resize"; }, n.scaleSkewCursorStyleHandler = function (t, e, i) { return t[i.canvas.altActionKey] ? n.skewCursorStyleHandler(t, e, i) : n.scaleCursorStyleHandler(t, e, i); }, n.rotationWithSnapping = b("rotating", m(function (t, e, i, r) { var n = e, s = n.target, o = s.translateToOriginPoint(s.getCenterPoint(), n.originX, n.originY); if (s.lockRotation) return !1; var a, c = Math.atan2(n.ey - o.y, n.ex - o.x), h = Math.atan2(r - o.y, i - o.x), l = v(h - c + n.theta); if (0 < s.snapAngle) { var u = s.snapAngle, f = s.snapThreshold || u, d = Math.ceil(l / u) * u, g = Math.floor(l / u) * u; Math.abs(l - g) < f ? l = g : Math.abs(l - d) < f && (l = d); } return l < 0 && (l = 360 + l), l %= 360, a = s.angle !== l, s.angle = l, a; })), n.scalingEqually = b("scaling", m(function (t, e, i, r) { return E(t, e, i, r); })), n.scalingX = b("scaling", m(function (t, e, i, r) { return E(t, e, i, r, { by: "x" }); })), n.scalingY = b("scaling", m(function (t, e, i, r) { return E(t, e, i, r, { by: "y" }); })), n.scalingYOrSkewingX = function (t, e, i, r) { return t[e.target.canvas.altActionKey] ? n.skewHandlerX(t, e, i, r) : n.scalingY(t, e, i, r); }, n.scalingXOrSkewingY = function (t, e, i, r) { return t[e.target.canvas.altActionKey] ? n.skewHandlerY(t, e, i, r) : n.scalingX(t, e, i, r); }, n.changeWidth = b("resizing", m(function (t, e, i, r) { var n = e.target, s = P(e, e.originX, e.originY, i, r), o = n.strokeWidth / (n.strokeUniform ? n.scaleX : 1), a = O(e) ? 2 : 1, c = n.width, h = Math.abs(s.x * a / n.scaleX) - o; return n.set("width", Math.max(h, 0)), c !== h; })), n.skewHandlerX = function (t, e, i, r) { var n, s = e.target, o = s.skewX, a = e.originY; return !s.lockSkewingX && (0 === o ? n = 0 < P(e, c, c, i, r).x ? f : g : (0 < o && (n = a === d ? f : g), o < 0 && (n = a === d ? g : f), y(s) && (n = n === f ? g : f)), e.originX = n, b("skewing", m(x))(t, e, i, r)); }, n.skewHandlerY = function (t, e, i, r) { var n, s = e.target, o = s.skewY, a = e.originX; return !s.lockSkewingY && (0 === o ? n = 0 < P(e, c, c, i, r).y ? d : p : (0 < o && (n = a === f ? d : p), o < 0 && (n = a === f ? p : d), y(s) && (n = n === d ? p : d)), e.originY = n, b("skewing", m(C))(t, e, i, r)); }, n.dragHandler = function (t, e, i, r) { var n = e.target, s = i - e.offsetX, o = r - e.offsetY, a = !n.get("lockMovementX") && n.left !== s, c = !n.get("lockMovementY") && n.top !== o; return a && n.set("left", s), c && n.set("top", o), (a || c) && h("moving", u(t, e, i, r)), a || c; }, n.scaleOrSkewActionName = function (t, e, i) { var r = t[i.canvas.altActionKey]; return 0 === e.x ? r ? "skewX" : "scaleY" : 0 === e.y ? r ? "skewY" : "scaleX" : void 0; }, n.rotationStyleHandler = function (t, e, i) { return i.lockRotation ? "not-allowed" : e.cursorStyle; }, n.fireEvent = h, n.wrapWithFixedAnchor = m, n.wrapWithFireEvent = b, n.getLocalPoint = P, l.controlsUtils = n; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), f = e.util.degreesToRadians, i = e.controlsUtils; i.renderCircleControl = function (t, e, i, r, n) { r = r || {}; var s, o = this.sizeX || r.cornerSize || n.cornerSize, a = this.sizeY || r.cornerSize || n.cornerSize, c = void 0 !== r.transparentCorners ? r.transparentCorners : n.transparentCorners, h = c ? "stroke" : "fill", l = !c && (r.cornerStrokeColor || n.cornerStrokeColor), u = e, f = i; t.save(), t.fillStyle = r.cornerColor || n.cornerColor, t.strokeStyle = r.cornerStrokeColor || n.cornerStrokeColor, a < o ? (s = o, t.scale(1, a / o), f = i * o / a) : o < a ? (s = a, t.scale(o / a, 1), u = e * a / o) : s = o, t.lineWidth = 1, t.beginPath(), t.arc(u, f, s / 2, 0, 2 * Math.PI, !1), t[h](), l && t.stroke(), t.restore(); }, i.renderSquareControl = function (t, e, i, r, n) { r = r || {}; var s = this.sizeX || r.cornerSize || n.cornerSize, o = this.sizeY || r.cornerSize || n.cornerSize, a = void 0 !== r.transparentCorners ? r.transparentCorners : n.transparentCorners, c = a ? "stroke" : "fill", h = !a && (r.cornerStrokeColor || n.cornerStrokeColor), l = s / 2, u = o / 2; t.save(), t.fillStyle = r.cornerColor || n.cornerColor, t.strokeStyle = r.cornerStrokeColor || n.cornerStrokeColor, t.lineWidth = 1, t.translate(e, i), t.rotate(f(n.angle)), t[c + "Rect"](-l, -u, s, o), h && t.strokeRect(-l, -u, s, o), t.restore(); }; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var p = t.fabric || (t.fabric = {}); p.Control = function (t) { for (var e in t) this[e] = t[e]; }, p.Control.prototype = { visible: !0, actionName: "scale", angle: 0, x: 0, y: 0, offsetX: 0, offsetY: 0, sizeX: null, sizeY: null, touchSizeX: null, touchSizeY: null, cursorStyle: "crosshair", withConnection: !1, actionHandler: function () { }, mouseDownHandler: function () { }, mouseUpHandler: function () { }, getActionHandler: function () { return this.actionHandler; }, getMouseDownHandler: function () { return this.mouseDownHandler; }, getMouseUpHandler: function () { return this.mouseUpHandler; }, cursorStyleHandler: function (t, e) { return e.cursorStyle; }, getActionName: function (t, e) { return e.actionName; }, getVisibility: function (t, e) { var i = t._controlsVisibility; return i && void 0 !== i[e] ? i[e] : this.visible; }, setVisibility: function (t) { this.visible = t; }, positionHandler: function (t, e) { return p.util.transformPoint({ x: this.x * t.x + this.offsetX, y: this.y * t.y + this.offsetY }, e); }, calcCornerCoords: function (t, e, i, r, n) { var s, o, a, c, h = n ? this.touchSizeX : this.sizeX, l = n ? this.touchSizeY : this.sizeY; if (h && l && h !== l) { var u = Math.atan2(l, h), f = Math.sqrt(h * h + l * l) / 2, d = u - p.util.degreesToRadians(t), g = Math.PI / 2 - u - p.util.degreesToRadians(t); s = f * p.util.cos(d), o = f * p.util.sin(d), a = f * p.util.cos(g), c = f * p.util.sin(g); } else { f = .7071067812 * (h && l ? h : e); d = p.util.degreesToRadians(45 - t); s = a = f * p.util.cos(d), o = c = f * p.util.sin(d); } return { tl: { x: i - c, y: r - a }, tr: { x: i + s, y: r - o }, bl: { x: i - s, y: r + o }, br: { x: i + c, y: r + a } }; }, render: function (t, e, i, r, n) { switch ((r = r || {}).cornerStyle || n.cornerStyle) { case "circle": p.controlsUtils.renderCircleControl.call(this, t, e, i, r, n); break; default: p.controlsUtils.renderSquareControl.call(this, t, e, i, r, n); } } }; }("undefined" != typeof exports ? exports : this), function () { function C(t, e) { var i, r, n, s, o = t.getAttribute("style"), a = t.getAttribute("offset") || 0; if (a = (a = parseFloat(a) / (/%$/.test(a) ? 100 : 1)) < 0 ? 0 : 1 < a ? 1 : a, o) { var c = o.split(/\s*;\s*/); for ("" === c[c.length - 1] && c.pop(), s = c.length; s--;) { var h = c[s].split(/\s*:\s*/), l = h[0].trim(), u = h[1].trim(); "stop-color" === l ? i = u : "stop-opacity" === l && (n = u); } } return i || (i = t.getAttribute("stop-color") || "rgb(0,0,0)"), n || (n = t.getAttribute("stop-opacity")), r = (i = new fabric.Color(i)).getAlpha(), n = isNaN(parseFloat(n)) ? 1 : parseFloat(n), n *= r * e, { offset: a, color: i.toRgb(), opacity: n }; } var m = fabric.util.object.clone; fabric.Gradient = fabric.util.createClass({ offsetX: 0, offsetY: 0, gradientTransform: null, gradientUnits: "pixels", type: "linear", initialize: function (e) { e || (e = {}), e.coords || (e.coords = {}); var t, i = this; Object.keys(e).forEach(function (t) { i[t] = e[t]; }), this.id ? this.id += "_" + fabric.Object.__uid++ : this.id = fabric.Object.__uid++, t = { x1: e.coords.x1 || 0, y1: e.coords.y1 || 0, x2: e.coords.x2 || 0, y2: e.coords.y2 || 0 }, "radial" === this.type && (t.r1 = e.coords.r1 || 0, t.r2 = e.coords.r2 || 0), this.coords = t, this.colorStops = e.colorStops.slice(); }, addColorStop: function (t) { for (var e in t) { var i = new fabric.Color(t[e]); this.colorStops.push({ offset: parseFloat(e), color: i.toRgb(), opacity: i.getAlpha() }); } return this; }, toObject: function (t) { var e = { type: this.type, coords: this.coords, colorStops: this.colorStops, offsetX: this.offsetX, offsetY: this.offsetY, gradientUnits: this.gradientUnits, gradientTransform: this.gradientTransform ? this.gradientTransform.concat() : this.gradientTransform }; return fabric.util.populateWithProperties(this, e, t), e; }, toSVG: function (t, e) { var i, r, n, s, o = m(this.coords, !0), a = (e = e || {}, m(this.colorStops, !0)), c = o.r1 > o.r2, h = this.gradientTransform ? this.gradientTransform.concat() : fabric.iMatrix.concat(), l = -this.offsetX, u = -this.offsetY, f = !!e.additionalTransform, d = "pixels" === this.gradientUnits ? "userSpaceOnUse" : "objectBoundingBox"; if (a.sort(function (t, e) { return t.offset - e.offset; }), "objectBoundingBox" === d ? (l /= t.width, u /= t.height) : (l += t.width / 2, u += t.height / 2), "path" === t.type && "percentage" !== this.gradientUnits && (l -= t.pathOffset.x, u -= t.pathOffset.y), h[4] -= l, h[5] -= u, s = 'id="SVGID_' + this.id + '" gradientUnits="' + d + '"', s += ' gradientTransform="' + (f ? e.additionalTransform + " " : "") + fabric.util.matrixToSVG(h) + '" ', "linear" === this.type ? n = ["<linearGradient ", s, ' x1="', o.x1, '" y1="', o.y1, '" x2="', o.x2, '" y2="', o.y2, '">\n'] : "radial" === this.type && (n = ["<radialGradient ", s, ' cx="', c ? o.x1 : o.x2, '" cy="', c ? o.y1 : o.y2, '" r="', c ? o.r1 : o.r2, '" fx="', c ? o.x2 : o.x1, '" fy="', c ? o.y2 : o.y1, '">\n']), "radial" === this.type) { if (c) for ((a = a.concat()).reverse(), i = 0, r = a.length; i < r; i++)a[i].offset = 1 - a[i].offset; var g = Math.min(o.r1, o.r2); if (0 < g) { var p = g / Math.max(o.r1, o.r2); for (i = 0, r = a.length; i < r; i++)a[i].offset += p * (1 - a[i].offset); } } for (i = 0, r = a.length; i < r; i++) { var v = a[i]; n.push("<stop ", 'offset="', 100 * v.offset + "%", '" style="stop-color:', v.color, void 0 !== v.opacity ? ";stop-opacity: " + v.opacity : ";", '"/>\n'); } return n.push("linear" === this.type ? "</linearGradient>\n" : "</radialGradient>\n"), n.join(""); }, toLive: function (t) { var e, i, r, n = fabric.util.object.clone(this.coords); if (this.type) { for ("linear" === this.type ? e = t.createLinearGradient(n.x1, n.y1, n.x2, n.y2) : "radial" === this.type && (e = t.createRadialGradient(n.x1, n.y1, n.r1, n.x2, n.y2, n.r2)), i = 0, r = this.colorStops.length; i < r; i++) { var s = this.colorStops[i].color, o = this.colorStops[i].opacity, a = this.colorStops[i].offset; void 0 !== o && (s = new fabric.Color(s).setAlpha(o).toRgba()), e.addColorStop(a, s); } return e; } } }), fabric.util.object.extend(fabric.Gradient, { fromElement: function (t, e, i, r) { var n = parseFloat(i) / (/%$/.test(i) ? 100 : 1); n = n < 0 ? 0 : 1 < n ? 1 : n, isNaN(n) && (n = 1); var s, o, a, c, h, l, u, f, d, g, p, v = t.getElementsByTagName("stop"), m = "userSpaceOnUse" === t.getAttribute("gradientUnits") ? "pixels" : "percentage", b = t.getAttribute("gradientTransform") || "", y = [], _ = 0, x = 0; for ("linearGradient" === t.nodeName || "LINEARGRADIENT" === t.nodeName ? (s = "linear", o = { x1: (l = t).getAttribute("x1") || 0, y1: l.getAttribute("y1") || 0, x2: l.getAttribute("x2") || "100%", y2: l.getAttribute("y2") || 0 }) : (s = "radial", o = { x1: (h = t).getAttribute("fx") || h.getAttribute("cx") || "50%", y1: h.getAttribute("fy") || h.getAttribute("cy") || "50%", r1: 0, x2: h.getAttribute("cx") || "50%", y2: h.getAttribute("cy") || "50%", r2: h.getAttribute("r") || "50%" }), a = v.length; a--;)y.push(C(v[a], n)); return c = fabric.parseTransformAttribute(b), u = o, f = r, d = m, Object.keys(u).forEach(function (t) { "Infinity" === (g = u[t]) ? p = 1 : "-Infinity" === g ? p = 0 : (p = parseFloat(u[t], 10), "string" == typeof g && /^(\d+\.\d+)%|(\d+)%$/.test(g) && (p *= .01, "pixels" === d && ("x1" !== t && "x2" !== t && "r2" !== t || (p *= f.viewBoxWidth || f.width), "y1" !== t && "y2" !== t || (p *= f.viewBoxHeight || f.height)))), u[t] = p; }), "pixels" === m && (_ = -e.left, x = -e.top), new fabric.Gradient({ id: t.getAttribute("id"), type: s, coords: o, colorStops: y, gradientUnits: m, gradientTransform: c, offsetX: _, offsetY: x }); } }); }(), function () { "use strict"; var n = fabric.util.toFixed; fabric.Pattern = fabric.util.createClass({ repeat: "repeat", offsetX: 0, offsetY: 0, crossOrigin: "", patternTransform: null, initialize: function (t, i) { if (t || (t = {}), this.id = fabric.Object.__uid++, this.setOptions(t), !t.source || t.source && "string" != typeof t.source) i && i(this); else { var r = this; this.source = fabric.util.createImage(), fabric.util.loadImage(t.source, function (t, e) { r.source = t, i && i(r, e); }, null, this.crossOrigin); } }, toObject: function (t) { var e, i, r = fabric.Object.NUM_FRACTION_DIGITS; return "string" == typeof this.source.src ? e = this.source.src : "object" == typeof this.source && this.source.toDataURL && (e = this.source.toDataURL()), i = { type: "pattern", source: e, repeat: this.repeat, crossOrigin: this.crossOrigin, offsetX: n(this.offsetX, r), offsetY: n(this.offsetY, r), patternTransform: this.patternTransform ? this.patternTransform.concat() : null }, fabric.util.populateWithProperties(this, i, t), i; }, toSVG: function (t) { var e = "function" == typeof this.source ? this.source() : this.source, i = e.width / t.width, r = e.height / t.height, n = this.offsetX / t.width, s = this.offsetY / t.height, o = ""; return "repeat-x" !== this.repeat && "no-repeat" !== this.repeat || (r = 1, s && (r += Math.abs(s))), "repeat-y" !== this.repeat && "no-repeat" !== this.repeat || (i = 1, n && (i += Math.abs(n))), e.src ? o = e.src : e.toDataURL && (o = e.toDataURL()), '<pattern id="SVGID_' + this.id + '" x="' + n + '" y="' + s + '" width="' + i + '" height="' + r + '">\n<image x="0" y="0" width="' + e.width + '" height="' + e.height + '" xlink:href="' + o + '"></image>\n</pattern>\n'; }, setOptions: function (t) { for (var e in t) this[e] = t[e]; }, toLive: function (t) { var e = this.source; if (!e) return ""; if (void 0 !== e.src) { if (!e.complete) return ""; if (0 === e.naturalWidth || 0 === e.naturalHeight) return ""; } return t.createPattern(e, this.repeat); } }); }(), function (t) { "use strict"; var o = t.fabric || (t.fabric = {}), a = o.util.toFixed; o.Shadow ? o.warn("fabric.Shadow is already defined.") : (o.Shadow = o.util.createClass({ color: "rgb(0,0,0)", blur: 0, offsetX: 0, offsetY: 0, affectStroke: !1, includeDefaultValues: !0, nonScaling: !1, initialize: function (t) { for (var e in "string" == typeof t && (t = this._parseShadow(t)), t) this[e] = t[e]; this.id = o.Object.__uid++; }, _parseShadow: function (t) { var e = t.trim(), i = o.Shadow.reOffsetsAndBlur.exec(e) || []; return { color: (e.replace(o.Shadow.reOffsetsAndBlur, "") || "rgb(0,0,0)").trim(), offsetX: parseFloat(i[1], 10) || 0, offsetY: parseFloat(i[2], 10) || 0, blur: parseFloat(i[3], 10) || 0 }; }, toString: function () { return [this.offsetX, this.offsetY, this.blur, this.color].join("px "); }, toSVG: function (t) { var e = 40, i = 40, r = o.Object.NUM_FRACTION_DIGITS, n = o.util.rotateVector({ x: this.offsetX, y: this.offsetY }, o.util.degreesToRadians(-t.angle)), s = new o.Color(this.color); return t.width && t.height && (e = 100 * a((Math.abs(n.x) + this.blur) / t.width, r) + 20, i = 100 * a((Math.abs(n.y) + this.blur) / t.height, r) + 20), t.flipX && (n.x *= -1), t.flipY && (n.y *= -1), '<filter id="SVGID_' + this.id + '" y="-' + i + '%" height="' + (100 + 2 * i) + '%" x="-' + e + '%" width="' + (100 + 2 * e) + '%" >\n\t<feGaussianBlur in="SourceAlpha" stdDeviation="' + a(this.blur ? this.blur / 2 : 0, r) + '"></feGaussianBlur>\n\t<feOffset dx="' + a(n.x, r) + '" dy="' + a(n.y, r) + '" result="oBlur" ></feOffset>\n\t<feFlood flood-color="' + s.toRgb() + '" flood-opacity="' + s.getAlpha() + '"/>\n\t<feComposite in2="oBlur" operator="in" />\n\t<feMerge>\n\t\t<feMergeNode></feMergeNode>\n\t\t<feMergeNode in="SourceGraphic"></feMergeNode>\n\t</feMerge>\n</filter>\n'; }, toObject: function () { if (this.includeDefaultValues) return { color: this.color, blur: this.blur, offsetX: this.offsetX, offsetY: this.offsetY, affectStroke: this.affectStroke, nonScaling: this.nonScaling }; var e = {}, i = o.Shadow.prototype; return ["color", "blur", "offsetX", "offsetY", "affectStroke", "nonScaling"].forEach(function (t) { this[t] !== i[t] && (e[t] = this[t]); }, this), e; } }), o.Shadow.reOffsetsAndBlur = /(?:\s|^)(-?\d+(?:\.\d*)?(?:px)?(?:\s?|$))?(-?\d+(?:\.\d*)?(?:px)?(?:\s?|$))?(\d+(?:\.\d*)?(?:px)?)?(?:\s?|$)(?:$|\s)/); }("undefined" != typeof exports ? exports : this), function () { "use strict"; if (fabric.StaticCanvas) fabric.warn("fabric.StaticCanvas is already defined."); else { var n = fabric.util.object.extend, t = fabric.util.getElementOffset, h = fabric.util.removeFromArray, a = fabric.util.toFixed, s = fabric.util.transformPoint, o = fabric.util.invertTransform, i = fabric.util.getNodeCanvas, r = fabric.util.createCanvasElement, e = new Error("Could not initialize `canvas` element"); fabric.StaticCanvas = fabric.util.createClass(fabric.CommonMethods, { initialize: function (t, e) { e || (e = {}), this.renderAndResetBound = this.renderAndReset.bind(this), this.requestRenderAllBound = this.requestRenderAll.bind(this), this._initStatic(t, e); }, backgroundColor: "", backgroundImage: null, overlayColor: "", overlayImage: null, includeDefaultValues: !0, stateful: !1, renderOnAddRemove: !0, controlsAboveOverlay: !1, allowTouchScrolling: !1, imageSmoothingEnabled: !0, viewportTransform: fabric.iMatrix.concat(), backgroundVpt: !0, overlayVpt: !0, enableRetinaScaling: !0, vptCoords: {}, skipOffscreen: !0, clipPath: void 0, _initStatic: function (t, e) { var i = this.requestRenderAllBound; this._objects = [], this._createLowerCanvas(t), this._initOptions(e), this.interactive || this._initRetinaScaling(), e.overlayImage && this.setOverlayImage(e.overlayImage, i), e.backgroundImage && this.setBackgroundImage(e.backgroundImage, i), e.backgroundColor && this.setBackgroundColor(e.backgroundColor, i), e.overlayColor && this.setOverlayColor(e.overlayColor, i), this.calcOffset(); }, _isRetinaScaling: function () { return 1 < fabric.devicePixelRatio && this.enableRetinaScaling; }, getRetinaScaling: function () { return this._isRetinaScaling() ? Math.max(1, fabric.devicePixelRatio) : 1; }, _initRetinaScaling: function () { if (this._isRetinaScaling()) { var t = fabric.devicePixelRatio; this.__initRetinaScaling(t, this.lowerCanvasEl, this.contextContainer), this.upperCanvasEl && this.__initRetinaScaling(t, this.upperCanvasEl, this.contextTop); } }, __initRetinaScaling: function (t, e, i) { e.setAttribute("width", this.width * t), e.setAttribute("height", this.height * t), i.scale(t, t); }, calcOffset: function () { return this._offset = t(this.lowerCanvasEl), this; }, setOverlayImage: function (t, e, i) { return this.__setBgOverlayImage("overlayImage", t, e, i); }, setBackgroundImage: function (t, e, i) { return this.__setBgOverlayImage("backgroundImage", t, e, i); }, setOverlayColor: function (t, e) { return this.__setBgOverlayColor("overlayColor", t, e); }, setBackgroundColor: function (t, e) { return this.__setBgOverlayColor("backgroundColor", t, e); }, __setBgOverlayImage: function (r, t, n, s) { return "string" == typeof t ? fabric.util.loadImage(t, function (t, e) { if (t) { var i = new fabric.Image(t, s); (this[r] = i).canvas = this; } n && n(t, e); }, this, s && s.crossOrigin) : (s && t.setOptions(s), (this[r] = t) && (t.canvas = this), n && n(t, !1)), this; }, __setBgOverlayColor: function (t, e, i) { return this[t] = e, this._initGradient(e, t), this._initPattern(e, t, i), this; }, _createCanvasElement: function () { var t = r(); if (!t) throw e; if (t.style || (t.style = {}), void 0 === t.getContext) throw e; return t; }, _initOptions: function (t) { var e = this.lowerCanvasEl; this._setOptions(t), this.width = this.width || parseInt(e.width, 10) || 0, this.height = this.height || parseInt(e.height, 10) || 0, this.lowerCanvasEl.style && (e.width = this.width, e.height = this.height, e.style.width = this.width + "px", e.style.height = this.height + "px", this.viewportTransform = this.viewportTransform.slice()); }, _createLowerCanvas: function (t) { t && t.getContext ? this.lowerCanvasEl = t : this.lowerCanvasEl = fabric.util.getById(t) || this._createCanvasElement(), fabric.util.addClass(this.lowerCanvasEl, "lower-canvas"), this._originalCanvasStyle = this.lowerCanvasEl.style, this.interactive && this._applyCanvasStyle(this.lowerCanvasEl), this.contextContainer = this.lowerCanvasEl.getContext("2d"); }, getWidth: function () { return this.width; }, getHeight: function () { return this.height; }, setWidth: function (t, e) { return this.setDimensions({ width: t }, e); }, setHeight: function (t, e) { return this.setDimensions({ height: t }, e); }, setDimensions: function (t, e) { var i; for (var r in e = e || {}, t) i = t[r], e.cssOnly || (this._setBackstoreDimension(r, t[r]), i += "px", this.hasLostContext = !0), e.backstoreOnly || this._setCssDimension(r, i); return this._isCurrentlyDrawing && this.freeDrawingBrush && this.freeDrawingBrush._setBrushStyles(this.contextTop), this._initRetinaScaling(), this.calcOffset(), e.cssOnly || this.requestRenderAll(), this; }, _setBackstoreDimension: function (t, e) { return this.lowerCanvasEl[t] = e, this.upperCanvasEl && (this.upperCanvasEl[t] = e), this.cacheCanvasEl && (this.cacheCanvasEl[t] = e), this[t] = e, this; }, _setCssDimension: function (t, e) { return this.lowerCanvasEl.style[t] = e, this.upperCanvasEl && (this.upperCanvasEl.style[t] = e), this.wrapperEl && (this.wrapperEl.style[t] = e), this; }, getZoom: function () { return this.viewportTransform[0]; }, setViewportTransform: function (t) { var e, i, r, n = this._activeObject, s = this.backgroundImage, o = this.overlayImage; for (this.viewportTransform = t, i = 0, r = this._objects.length; i < r; i++)(e = this._objects[i]).group || e.setCoords(!0); return n && n.setCoords(), s && s.setCoords(!0), o && o.setCoords(!0), this.calcViewportBoundaries(), this.renderOnAddRemove && this.requestRenderAll(), this; }, zoomToPoint: function (t, e) { var i = t, r = this.viewportTransform.slice(0); t = s(t, o(this.viewportTransform)), r[0] = e, r[3] = e; var n = s(t, r); return r[4] += i.x - n.x, r[5] += i.y - n.y, this.setViewportTransform(r); }, setZoom: function (t) { return this.zoomToPoint(new fabric.Point(0, 0), t), this; }, absolutePan: function (t) { var e = this.viewportTransform.slice(0); return e[4] = -t.x, e[5] = -t.y, this.setViewportTransform(e); }, relativePan: function (t) { return this.absolutePan(new fabric.Point(-t.x - this.viewportTransform[4], -t.y - this.viewportTransform[5])); }, getElement: function () { return this.lowerCanvasEl; }, _onObjectAdded: function (t) { this.stateful && t.setupState(), t._set("canvas", this), t.setCoords(), this.fire("object:added", { target: t }), t.fire("added"); }, _onObjectRemoved: function (t) { this.fire("object:removed", { target: t }), t.fire("removed"), delete t.canvas; }, clearContext: function (t) { return t.clearRect(0, 0, this.width, this.height), this; }, getContext: function () { return this.contextContainer; }, clear: function () { return this.remove.apply(this, this.getObjects()), this.backgroundImage = null, this.overlayImage = null, this.backgroundColor = "", this.overlayColor = "", this._hasITextHandlers && (this.off("mouse:up", this._mouseUpITextHandler), this._iTextInstances = null, this._hasITextHandlers = !1), this.clearContext(this.contextContainer), this.fire("canvas:cleared"), this.renderOnAddRemove && this.requestRenderAll(), this; }, renderAll: function () { var t = this.contextContainer; return this.renderCanvas(t, this._objects), this; }, renderAndReset: function () { this.isRendering = 0, this.renderAll(); }, requestRenderAll: function () { return this.isRendering || (this.isRendering = fabric.util.requestAnimFrame(this.renderAndResetBound)), this; }, calcViewportBoundaries: function () { var t = {}, e = this.width, i = this.height, r = o(this.viewportTransform); return t.tl = s({ x: 0, y: 0 }, r), t.br = s({ x: e, y: i }, r), t.tr = new fabric.Point(t.br.x, t.tl.y), t.bl = new fabric.Point(t.tl.x, t.br.y), this.vptCoords = t; }, cancelRequestedRender: function () { this.isRendering && (fabric.util.cancelAnimFrame(this.isRendering), this.isRendering = 0); }, renderCanvas: function (t, e) { var i = this.viewportTransform, r = this.clipPath; this.cancelRequestedRender(), this.calcViewportBoundaries(), this.clearContext(t), fabric.util.setImageSmoothing(t, this.imageSmoothingEnabled), this.fire("before:render", { ctx: t }), this._renderBackground(t), t.save(), t.transform(i[0], i[1], i[2], i[3], i[4], i[5]), this._renderObjects(t, e), t.restore(), !this.controlsAboveOverlay && this.interactive && this.drawControls(t), r && (r.canvas = this, r.shouldCache(), r._transformDone = !0, r.renderCache({ forClipping: !0 }), this.drawClipPathOnCanvas(t)), this._renderOverlay(t), this.controlsAboveOverlay && this.interactive && this.drawControls(t), this.fire("after:render", { ctx: t }); }, drawClipPathOnCanvas: function (t) { var e = this.viewportTransform, i = this.clipPath; t.save(), t.transform(e[0], e[1], e[2], e[3], e[4], e[5]), t.globalCompositeOperation = "destination-in", i.transform(t), t.scale(1 / i.zoomX, 1 / i.zoomY), t.drawImage(i._cacheCanvas, -i.cacheTranslationX, -i.cacheTranslationY), t.restore(); }, _renderObjects: function (t, e) { var i, r; for (i = 0, r = e.length; i < r; ++i)e[i] && e[i].render(t); }, _renderBackgroundOrOverlay: function (t, e) { var i = this[e + "Color"], r = this[e + "Image"], n = this.viewportTransform, s = this[e + "Vpt"]; if (i || r) { if (i) { t.save(), t.beginPath(), t.moveTo(0, 0), t.lineTo(this.width, 0), t.lineTo(this.width, this.height), t.lineTo(0, this.height), t.closePath(), t.fillStyle = i.toLive ? i.toLive(t, this) : i, s && t.transform(n[0], n[1], n[2], n[3], n[4], n[5]), t.transform(1, 0, 0, 1, i.offsetX || 0, i.offsetY || 0); var o = i.gradientTransform || i.patternTransform; o && t.transform(o[0], o[1], o[2], o[3], o[4], o[5]), t.fill(), t.restore(); } r && (t.save(), s && t.transform(n[0], n[1], n[2], n[3], n[4], n[5]), r.render(t), t.restore()); } }, _renderBackground: function (t) { this._renderBackgroundOrOverlay(t, "background"); }, _renderOverlay: function (t) { this._renderBackgroundOrOverlay(t, "overlay"); }, getCenter: function () { return { top: this.height / 2, left: this.width / 2 }; }, getCenterPoint: function () { return new fabric.Point(this.width / 2, this.height / 2); }, centerObjectH: function (t) { return this._centerObject(t, new fabric.Point(this.getCenterPoint().x, t.getCenterPoint().y)); }, centerObjectV: function (t) { return this._centerObject(t, new fabric.Point(t.getCenterPoint().x, this.getCenterPoint().y)); }, centerObject: function (t) { var e = this.getCenterPoint(); return this._centerObject(t, e); }, viewportCenterObject: function (t) { var e = this.getVpCenter(); return this._centerObject(t, e); }, viewportCenterObjectH: function (t) { var e = this.getVpCenter(); return this._centerObject(t, new fabric.Point(e.x, t.getCenterPoint().y)), this; }, viewportCenterObjectV: function (t) { var e = this.getVpCenter(); return this._centerObject(t, new fabric.Point(t.getCenterPoint().x, e.y)); }, getVpCenter: function () { var t = this.getCenterPoint(), e = o(this.viewportTransform); return s(t, e); }, _centerObject: function (t, e) { return t.setPositionByOrigin(e, "center", "center"), t.setCoords(), this.renderOnAddRemove && this.requestRenderAll(), this; }, toDatalessJSON: function (t) { return this.toDatalessObject(t); }, toObject: function (t) { return this._toObjectMethod("toObject", t); }, toDatalessObject: function (t) { return this._toObjectMethod("toDatalessObject", t); }, _toObjectMethod: function (t, e) { var i = this.clipPath, r = { version: fabric.version, objects: this._toObjects(t, e) }; return i && !i.excludeFromExport && (r.clipPath = this._toObject(this.clipPath, t, e)), n(r, this.__serializeBgOverlay(t, e)), fabric.util.populateWithProperties(this, r, e), r; }, _toObjects: function (e, i) { return this._objects.filter(function (t) { return !t.excludeFromExport; }).map(function (t) { return this._toObject(t, e, i); }, this); }, _toObject: function (t, e, i) { var r; this.includeDefaultValues || (r = t.includeDefaultValues, t.includeDefaultValues = !1); var n = t[e](i); return this.includeDefaultValues || (t.includeDefaultValues = r), n; }, __serializeBgOverlay: function (t, e) { var i = {}, r = this.backgroundImage, n = this.overlayImage, s = this.backgroundColor, o = this.overlayColor; return s && s.toObject ? s.excludeFromExport || (i.background = s.toObject(e)) : s && (i.background = s), o && o.toObject ? o.excludeFromExport || (i.overlay = o.toObject(e)) : o && (i.overlay = o), r && !r.excludeFromExport && (i.backgroundImage = this._toObject(r, t, e)), n && !n.excludeFromExport && (i.overlayImage = this._toObject(n, t, e)), i; }, svgViewportTransformation: !0, toSVG: function (t, e) { t || (t = {}), t.reviver = e; var i = []; return this._setSVGPreamble(i, t), this._setSVGHeader(i, t), this.clipPath && i.push('<g clip-path="url(#' + this.clipPath.clipPathId + ')" >\n'), this._setSVGBgOverlayColor(i, "background"), this._setSVGBgOverlayImage(i, "backgroundImage", e), this._setSVGObjects(i, e), this.clipPath && i.push("</g>\n"), this._setSVGBgOverlayColor(i, "overlay"), this._setSVGBgOverlayImage(i, "overlayImage", e), i.push("</svg>"), i.join(""); }, _setSVGPreamble: function (t, e) { e.suppressPreamble || t.push('<?xml version="1.0" encoding="', e.encoding || "UTF-8", '" standalone="no" ?>\n', '<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" ', '"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">\n'); }, _setSVGHeader: function (t, e) { var i, r = e.width || this.width, n = e.height || this.height, s = 'viewBox="0 0 ' + this.width + " " + this.height + '" ', o = fabric.Object.NUM_FRACTION_DIGITS; e.viewBox ? s = 'viewBox="' + e.viewBox.x + " " + e.viewBox.y + " " + e.viewBox.width + " " + e.viewBox.height + '" ' : this.svgViewportTransformation && (i = this.viewportTransform, s = 'viewBox="' + a(-i[4] / i[0], o) + " " + a(-i[5] / i[3], o) + " " + a(this.width / i[0], o) + " " + a(this.height / i[3], o) + '" '), t.push("<svg ", 'xmlns="http://www.w3.org/2000/svg" ', 'xmlns:xlink="http://www.w3.org/1999/xlink" ', 'version="1.1" ', 'width="', r, '" ', 'height="', n, '" ', s, 'xml:space="preserve">\n', "<desc>Created with Fabric.js ", fabric.version, "</desc>\n", "<defs>\n", this.createSVGFontFacesMarkup(), this.createSVGRefElementsMarkup(), this.createSVGClipPathMarkup(e), "</defs>\n"); }, createSVGClipPathMarkup: function (t) { var e = this.clipPath; return e ? (e.clipPathId = "CLIPPATH_" + fabric.Object.__uid++, '<clipPath id="' + e.clipPathId + '" >\n' + this.clipPath.toClipPathSVG(t.reviver) + "</clipPath>\n") : ""; }, createSVGRefElementsMarkup: function () { var s = this; return ["background", "overlay"].map(function (t) { var e = s[t + "Color"]; if (e && e.toLive) { var i = s[t + "Vpt"], r = s.viewportTransform, n = { width: s.width / (i ? r[0] : 1), height: s.height / (i ? r[3] : 1) }; return e.toSVG(n, { additionalTransform: i ? fabric.util.matrixToSVG(r) : "" }); } }).join(""); }, createSVGFontFacesMarkup: function () { var t, e, i, r, n, s, o, a, c = "", h = {}, l = fabric.fontPaths, u = []; for (this._objects.forEach(function t(e) { u.push(e), e._objects && e._objects.forEach(t); }), o = 0, a = u.length; o < a; o++)if (e = (t = u[o]).fontFamily, -1 !== t.type.indexOf("text") && !h[e] && l[e] && (h[e] = !0, t.styles)) for (n in i = t.styles) for (s in r = i[n]) !h[e = r[s].fontFamily] && l[e] && (h[e] = !0); for (var f in h) c += ["\t\t@font-face {\n", "\t\t\tfont-family: '", f, "';\n", "\t\t\tsrc: url('", l[f], "');\n", "\t\t}\n"].join(""); return c && (c = ['\t<style type="text/css">', "<![CDATA[\n", c, "]]>", "</style>\n"].join("")), c; }, _setSVGObjects: function (t, e) { var i, r, n, s = this._objects; for (r = 0, n = s.length; r < n; r++)(i = s[r]).excludeFromExport || this._setSVGObject(t, i, e); }, _setSVGObject: function (t, e, i) { t.push(e.toSVG(i)); }, _setSVGBgOverlayImage: function (t, e, i) { this[e] && !this[e].excludeFromExport && this[e].toSVG && t.push(this[e].toSVG(i)); }, _setSVGBgOverlayColor: function (t, e) { var i = this[e + "Color"], r = this.viewportTransform, n = this.width, s = this.height; if (i) if (i.toLive) { var o = i.repeat, a = fabric.util.invertTransform(r), c = this[e + "Vpt"] ? fabric.util.matrixToSVG(a) : ""; t.push('<rect transform="' + c + " translate(", n / 2, ",", s / 2, ')"', ' x="', i.offsetX - n / 2, '" y="', i.offsetY - s / 2, '" ', 'width="', "repeat-y" === o || "no-repeat" === o ? i.source.width : n, '" height="', "repeat-x" === o || "no-repeat" === o ? i.source.height : s, '" fill="url(#SVGID_' + i.id + ')"', "></rect>\n"); } else t.push('<rect x="0" y="0" width="100%" height="100%" ', 'fill="', i, '"', "></rect>\n"); }, sendToBack: function (t) { if (!t) return this; var e, i, r, n = this._activeObject; if (t === n && "activeSelection" === t.type) for (e = (r = n._objects).length; e--;)i = r[e], h(this._objects, i), this._objects.unshift(i); else h(this._objects, t), this._objects.unshift(t); return this.renderOnAddRemove && this.requestRenderAll(), this; }, bringToFront: function (t) { if (!t) return this; var e, i, r, n = this._activeObject; if (t === n && "activeSelection" === t.type) for (r = n._objects, e = 0; e < r.length; e++)i = r[e], h(this._objects, i), this._objects.push(i); else h(this._objects, t), this._objects.push(t); return this.renderOnAddRemove && this.requestRenderAll(), this; }, sendBackwards: function (t, e) { if (!t) return this; var i, r, n, s, o, a = this._activeObject, c = 0; if (t === a && "activeSelection" === t.type) for (o = a._objects, i = 0; i < o.length; i++)r = o[i], 0 + c < (n = this._objects.indexOf(r)) && (s = n - 1, h(this._objects, r), this._objects.splice(s, 0, r)), c++; else 0 !== (n = this._objects.indexOf(t)) && (s = this._findNewLowerIndex(t, n, e), h(this._objects, t), this._objects.splice(s, 0, t)); return this.renderOnAddRemove && this.requestRenderAll(), this; }, _findNewLowerIndex: function (t, e, i) { var r, n; if (i) for (n = (r = e) - 1; 0 <= n; --n) { if (t.intersectsWithObject(this._objects[n]) || t.isContainedWithinObject(this._objects[n]) || this._objects[n].isContainedWithinObject(t)) { r = n; break; } } else r = e - 1; return r; }, bringForward: function (t, e) { if (!t) return this; var i, r, n, s, o, a = this._activeObject, c = 0; if (t === a && "activeSelection" === t.type) for (i = (o = a._objects).length; i--;)r = o[i], (n = this._objects.indexOf(r)) < this._objects.length - 1 - c && (s = n + 1, h(this._objects, r), this._objects.splice(s, 0, r)), c++; else (n = this._objects.indexOf(t)) !== this._objects.length - 1 && (s = this._findNewUpperIndex(t, n, e), h(this._objects, t), this._objects.splice(s, 0, t)); return this.renderOnAddRemove && this.requestRenderAll(), this; }, _findNewUpperIndex: function (t, e, i) { var r, n, s; if (i) for (n = (r = e) + 1, s = this._objects.length; n < s; ++n) { if (t.intersectsWithObject(this._objects[n]) || t.isContainedWithinObject(this._objects[n]) || this._objects[n].isContainedWithinObject(t)) { r = n; break; } } else r = e + 1; return r; }, moveTo: function (t, e) { return h(this._objects, t), this._objects.splice(e, 0, t), this.renderOnAddRemove && this.requestRenderAll(); }, dispose: function () { return this.isRendering && (fabric.util.cancelAnimFrame(this.isRendering), this.isRendering = 0), this.forEachObject(function (t) { t.dispose && t.dispose(); }), this._objects = [], this.backgroundImage && this.backgroundImage.dispose && this.backgroundImage.dispose(), this.backgroundImage = null, this.overlayImage && this.overlayImage.dispose && this.overlayImage.dispose(), this.overlayImage = null, this._iTextInstances = null, this.contextContainer = null, this.lowerCanvasEl.classList.remove("lower-canvas"), fabric.util.setStyle(this.lowerCanvasEl, this._originalCanvasStyle), delete this._originalCanvasStyle, this.lowerCanvasEl.setAttribute("width", this.width), this.lowerCanvasEl.setAttribute("height", this.height), fabric.util.cleanUpJsdomNode(this.lowerCanvasEl), this.lowerCanvasEl = void 0, this; }, toString: function () { return "#<fabric.Canvas (" + this.complexity() + "): { objects: " + this._objects.length + " }>"; } }), n(fabric.StaticCanvas.prototype, fabric.Observable), n(fabric.StaticCanvas.prototype, fabric.Collection), n(fabric.StaticCanvas.prototype, fabric.DataURLExporter), n(fabric.StaticCanvas, { EMPTY_JSON: '{"objects": [], "background": "white"}', supports: function (t) { var e = r(); if (!e || !e.getContext) return null; var i = e.getContext("2d"); if (!i) return null; switch (t) { case "setLineDash": return void 0 !== i.setLineDash; default: return null; } } }), fabric.StaticCanvas.prototype.toJSON = fabric.StaticCanvas.prototype.toObject, fabric.isLikelyNode && (fabric.StaticCanvas.prototype.createPNGStream = function () { var t = i(this.lowerCanvasEl); return t && t.createPNGStream(); }, fabric.StaticCanvas.prototype.createJPEGStream = function (t) { var e = i(this.lowerCanvasEl); return e && e.createJPEGStream(t); }); } }(), fabric.BaseBrush = fabric.util.createClass({ color: "rgb(0, 0, 0)", width: 1, shadow: null, strokeLineCap: "round", strokeLineJoin: "round", strokeMiterLimit: 10, strokeDashArray: null, limitedToCanvasSize: !1, _setBrushStyles: function (t) { t.strokeStyle = this.color, t.lineWidth = this.width, t.lineCap = this.strokeLineCap, t.miterLimit = this.strokeMiterLimit, t.lineJoin = this.strokeLineJoin, t.setLineDash(this.strokeDashArray || []); }, _saveAndTransform: function (t) { var e = this.canvas.viewportTransform; t.save(), t.transform(e[0], e[1], e[2], e[3], e[4], e[5]); }, _setShadow: function () { if (this.shadow) { var t = this.canvas, e = this.shadow, i = t.contextTop, r = t.getZoom(); t && t._isRetinaScaling() && (r *= fabric.devicePixelRatio), i.shadowColor = e.color, i.shadowBlur = e.blur * r, i.shadowOffsetX = e.offsetX * r, i.shadowOffsetY = e.offsetY * r; } }, needsFullRender: function () { return new fabric.Color(this.color).getAlpha() < 1 || !!this.shadow; }, _resetShadow: function () { var t = this.canvas.contextTop; t.shadowColor = "", t.shadowBlur = t.shadowOffsetX = t.shadowOffsetY = 0; }, _isOutSideCanvas: function (t) { return t.x < 0 || t.x > this.canvas.getWidth() || t.y < 0 || t.y > this.canvas.getHeight(); } }), fabric.PencilBrush = fabric.util.createClass(fabric.BaseBrush, { decimate: .4, drawStraightLine: !1, straightLineKey: "shiftKey", initialize: function (t) { this.canvas = t, this._points = []; }, needsFullRender: function () { return this.callSuper("needsFullRender") || this._hasStraightLine; }, _drawSegment: function (t, e, i) { var r = e.midPointFrom(i); return t.quadraticCurveTo(e.x, e.y, r.x, r.y), r; }, onMouseDown: function (t, e) { this.canvas._isMainEvent(e.e) && (this.drawStraightLine = e.e[this.straightLineKey], this._prepareForDrawing(t), this._captureDrawingPath(t), this._render()); }, onMouseMove: function (t, e) { if (this.canvas._isMainEvent(e.e) && (this.drawStraightLine = e.e[this.straightLineKey], (!0 !== this.limitedToCanvasSize || !this._isOutSideCanvas(t)) && this._captureDrawingPath(t) && 1 < this._points.length)) if (this.needsFullRender()) this.canvas.clearContext(this.canvas.contextTop), this._render(); else { var i = this._points, r = i.length, n = this.canvas.contextTop; this._saveAndTransform(n), this.oldEnd && (n.beginPath(), n.moveTo(this.oldEnd.x, this.oldEnd.y)), this.oldEnd = this._drawSegment(n, i[r - 2], i[r - 1], !0), n.stroke(), n.restore(); } }, onMouseUp: function (t) { return !this.canvas._isMainEvent(t.e) || (this.drawStraightLine = !1, this.oldEnd = void 0, this._finalizeAndAddPath(), !1); }, _prepareForDrawing: function (t) { var e = new fabric.Point(t.x, t.y); this._reset(), this._addPoint(e), this.canvas.contextTop.moveTo(e.x, e.y); }, _addPoint: function (t) { return !(1 < this._points.length && t.eq(this._points[this._points.length - 1]) || (this.drawStraightLine && 1 < this._points.length && (this._hasStraightLine = !0, this._points.pop()), this._points.push(t), 0)); }, _reset: function () { this._points = [], this._setBrushStyles(this.canvas.contextTop), this._setShadow(), this._hasStraightLine = !1; }, _captureDrawingPath: function (t) { var e = new fabric.Point(t.x, t.y); return this._addPoint(e); }, _render: function (t) { var e, i, r = this._points[0], n = this._points[1]; if (t = t || this.canvas.contextTop, this._saveAndTransform(t), t.beginPath(), 2 === this._points.length && r.x === n.x && r.y === n.y) { var s = this.width / 1e3; r = new fabric.Point(r.x, r.y), n = new fabric.Point(n.x, n.y), r.x -= s, n.x += s; } for (t.moveTo(r.x, r.y), e = 1, i = this._points.length; e < i; e++)this._drawSegment(t, r, n), r = this._points[e], n = this._points[e + 1]; t.lineTo(r.x, r.y), t.stroke(), t.restore(); }, convertPointsToSVGPath: function (t) { var e = this.width / 1e3; return fabric.util.getSmoothPathFromPoints(t, e); }, _isEmptySVGPath: function (t) { return "M 0 0 Q 0 0 0 0 L 0 0" === fabric.util.joinPath(t); }, createPath: function (t) { var e = new fabric.Path(t, { fill: null, stroke: this.color, strokeWidth: this.width, strokeLineCap: this.strokeLineCap, strokeMiterLimit: this.strokeMiterLimit, strokeLineJoin: this.strokeLineJoin, strokeDashArray: this.strokeDashArray }); return this.shadow && (this.shadow.affectStroke = !0, e.shadow = new fabric.Shadow(this.shadow)), e; }, decimatePoints: function (t, e) { if (t.length <= 2) return t; var i, r = this.canvas.getZoom(), n = Math.pow(e / r, 2), s = t.length - 1, o = t[0], a = [o]; for (i = 1; i < s - 1; i++)n <= Math.pow(o.x - t[i].x, 2) + Math.pow(o.y - t[i].y, 2) && (o = t[i], a.push(o)); return a.push(t[s]), a; }, _finalizeAndAddPath: function () { this.canvas.contextTop.closePath(), this.decimate && (this._points = this.decimatePoints(this._points, this.decimate)); var t = this.convertPointsToSVGPath(this._points); if (this._isEmptySVGPath(t)) this.canvas.requestRenderAll(); else { var e = this.createPath(t); this.canvas.clearContext(this.canvas.contextTop), this.canvas.fire("before:path:created", { path: e }), this.canvas.add(e), this.canvas.requestRenderAll(), e.setCoords(), this._resetShadow(), this.canvas.fire("path:created", { path: e }); } } }), fabric.CircleBrush = fabric.util.createClass(fabric.BaseBrush, { width: 10, initialize: function (t) { this.canvas = t, this.points = []; }, drawDot: function (t) { var e = this.addPoint(t), i = this.canvas.contextTop; this._saveAndTransform(i), this.dot(i, e), i.restore(); }, dot: function (t, e) { t.fillStyle = e.fill, t.beginPath(), t.arc(e.x, e.y, e.radius, 0, 2 * Math.PI, !1), t.closePath(), t.fill(); }, onMouseDown: function (t) { this.points.length = 0, this.canvas.clearContext(this.canvas.contextTop), this._setShadow(), this.drawDot(t); }, _render: function () { var t, e, i = this.canvas.contextTop, r = this.points; for (this._saveAndTransform(i), t = 0, e = r.length; t < e; t++)this.dot(i, r[t]); i.restore(); }, onMouseMove: function (t) { !0 === this.limitedToCanvasSize && this._isOutSideCanvas(t) || (this.needsFullRender() ? (this.canvas.clearContext(this.canvas.contextTop), this.addPoint(t), this._render()) : this.drawDot(t)); }, onMouseUp: function () { var t, e, i = this.canvas.renderOnAddRemove; this.canvas.renderOnAddRemove = !1; var r = []; for (t = 0, e = this.points.length; t < e; t++) { var n = this.points[t], s = new fabric.Circle({ radius: n.radius, left: n.x, top: n.y, originX: "center", originY: "center", fill: n.fill }); this.shadow && (s.shadow = new fabric.Shadow(this.shadow)), r.push(s); } var o = new fabric.Group(r); o.canvas = this.canvas, this.canvas.fire("before:path:created", { path: o }), this.canvas.add(o), this.canvas.fire("path:created", { path: o }), this.canvas.clearContext(this.canvas.contextTop), this._resetShadow(), this.canvas.renderOnAddRemove = i, this.canvas.requestRenderAll(); }, addPoint: function (t) { var e = new fabric.Point(t.x, t.y), i = fabric.util.getRandomInt(Math.max(0, this.width - 20), this.width + 20) / 2, r = new fabric.Color(this.color).setAlpha(fabric.util.getRandomInt(0, 100) / 100).toRgba(); return e.radius = i, e.fill = r, this.points.push(e), e; } }), fabric.SprayBrush = fabric.util.createClass(fabric.BaseBrush, { width: 10, density: 20, dotWidth: 1, dotWidthVariance: 1, randomOpacity: !1, optimizeOverlapping: !0, initialize: function (t) { this.canvas = t, this.sprayChunks = []; }, onMouseDown: function (t) { this.sprayChunks.length = 0, this.canvas.clearContext(this.canvas.contextTop), this._setShadow(), this.addSprayChunk(t), this.render(this.sprayChunkPoints); }, onMouseMove: function (t) { !0 === this.limitedToCanvasSize && this._isOutSideCanvas(t) || (this.addSprayChunk(t), this.render(this.sprayChunkPoints)); }, onMouseUp: function () { var t = this.canvas.renderOnAddRemove; this.canvas.renderOnAddRemove = !1; for (var e = [], i = 0, r = this.sprayChunks.length; i < r; i++)for (var n = this.sprayChunks[i], s = 0, o = n.length; s < o; s++) { var a = new fabric.Rect({ width: n[s].width, height: n[s].width, left: n[s].x + 1, top: n[s].y + 1, originX: "center", originY: "center", fill: this.color }); e.push(a); } this.optimizeOverlapping && (e = this._getOptimizedRects(e)); var c = new fabric.Group(e); this.shadow && c.set("shadow", new fabric.Shadow(this.shadow)), this.canvas.fire("before:path:created", { path: c }), this.canvas.add(c), this.canvas.fire("path:created", { path: c }), this.canvas.clearContext(this.canvas.contextTop), this._resetShadow(), this.canvas.renderOnAddRemove = t, this.canvas.requestRenderAll(); }, _getOptimizedRects: function (t) { var e, i, r, n = {}; for (i = 0, r = t.length; i < r; i++)n[e = t[i].left + "" + t[i].top] || (n[e] = t[i]); var s = []; for (e in n) s.push(n[e]); return s; }, render: function (t) { var e, i, r = this.canvas.contextTop; for (r.fillStyle = this.color, this._saveAndTransform(r), e = 0, i = t.length; e < i; e++) { var n = t[e]; void 0 !== n.opacity && (r.globalAlpha = n.opacity), r.fillRect(n.x, n.y, n.width, n.width); } r.restore(); }, _render: function () { var t, e, i = this.canvas.contextTop; for (i.fillStyle = this.color, this._saveAndTransform(i), t = 0, e = this.sprayChunks.length; t < e; t++)this.render(this.sprayChunks[t]); i.restore(); }, addSprayChunk: function (t) { this.sprayChunkPoints = []; var e, i, r, n, s = this.width / 2; for (n = 0; n < this.density; n++) { e = fabric.util.getRandomInt(t.x - s, t.x + s), i = fabric.util.getRandomInt(t.y - s, t.y + s), r = this.dotWidthVariance ? fabric.util.getRandomInt(Math.max(1, this.dotWidth - this.dotWidthVariance), this.dotWidth + this.dotWidthVariance) : this.dotWidth; var o = new fabric.Point(e, i); o.width = r, this.randomOpacity && (o.opacity = fabric.util.getRandomInt(0, 100) / 100), this.sprayChunkPoints.push(o); } this.sprayChunks.push(this.sprayChunkPoints); } }), fabric.PatternBrush = fabric.util.createClass(fabric.PencilBrush, { getPatternSrc: function () { var t = fabric.util.createCanvasElement(), e = t.getContext("2d"); return t.width = t.height = 25, e.fillStyle = this.color, e.beginPath(), e.arc(10, 10, 10, 0, 2 * Math.PI, !1), e.closePath(), e.fill(), t; }, getPatternSrcFunction: function () { return String(this.getPatternSrc).replace("this.color", '"' + this.color + '"'); }, getPattern: function (t) { return t.createPattern(this.source || this.getPatternSrc(), "repeat"); }, _setBrushStyles: function (t) { this.callSuper("_setBrushStyles", t), t.strokeStyle = this.getPattern(t); }, createPath: function (t) { var e = this.callSuper("createPath", t), i = e._getLeftTopCoords().scalarAdd(e.strokeWidth / 2); return e.stroke = new fabric.Pattern({ source: this.source || this.getPatternSrcFunction(), offsetX: -i.x, offsetY: -i.y }), e; } }), function () { var h = fabric.util.getPointer, u = fabric.util.degreesToRadians, l = fabric.util.isTouchEvent; for (var t in fabric.Canvas = fabric.util.createClass(fabric.StaticCanvas, { initialize: function (t, e) { e || (e = {}), this.renderAndResetBound = this.renderAndReset.bind(this), this.requestRenderAllBound = this.requestRenderAll.bind(this), this._initStatic(t, e), this._initInteractive(), this._createCacheCanvas(); }, uniformScaling: !0, uniScaleKey: "shiftKey", centeredScaling: !1, centeredRotation: !1, centeredKey: "altKey", altActionKey: "shiftKey", interactive: !0, selection: !0, selectionKey: "shiftKey", altSelectionKey: null, selectionColor: "rgba(100, 100, 255, 0.3)", selectionDashArray: [], selectionBorderColor: "rgba(255, 255, 255, 0.3)", selectionLineWidth: 1, selectionFullyContained: !1, hoverCursor: "move", moveCursor: "move", defaultCursor: "default", freeDrawingCursor: "crosshair", notAllowedCursor: "not-allowed", containerClass: "canvas-container", perPixelTargetFind: !1, targetFindTolerance: 0, skipTargetFind: !1, isDrawingMode: !1, preserveObjectStacking: !1, snapAngle: 0, snapThreshold: null, stopContextMenu: !1, fireRightClick: !1, fireMiddleClick: !1, targets: [], enablePointerEvents: !1, _hoveredTarget: null, _hoveredTargets: [], _initInteractive: function () { this._currentTransform = null, this._groupSelector = null, this._initWrapperElement(), this._createUpperCanvas(), this._initEventListeners(), this._initRetinaScaling(), this.freeDrawingBrush = fabric.PencilBrush && new fabric.PencilBrush(this), this.calcOffset(); }, _chooseObjectsToRender: function () { var t, e, i, r = this.getActiveObjects(); if (0 < r.length && !this.preserveObjectStacking) { e = [], i = []; for (var n = 0, s = this._objects.length; n < s; n++)t = this._objects[n], -1 === r.indexOf(t) ? e.push(t) : i.push(t); 1 < r.length && (this._activeObject._objects = i), e.push.apply(e, i); } else e = this._objects; return e; }, renderAll: function () { !this.contextTopDirty || this._groupSelector || this.isDrawingMode || (this.clearContext(this.contextTop), this.contextTopDirty = !1), this.hasLostContext && (this.renderTopLayer(this.contextTop), this.hasLostContext = !1); var t = this.contextContainer; return this.renderCanvas(t, this._chooseObjectsToRender()), this; }, renderTopLayer: function (t) { t.save(), this.isDrawingMode && this._isCurrentlyDrawing && (this.freeDrawingBrush && this.freeDrawingBrush._render(), this.contextTopDirty = !0), this.selection && this._groupSelector && (this._drawSelection(t), this.contextTopDirty = !0), t.restore(); }, renderTop: function () { var t = this.contextTop; return this.clearContext(t), this.renderTopLayer(t), this.fire("after:render"), this; }, _normalizePointer: function (t, e) { var i = t.calcTransformMatrix(), r = fabric.util.invertTransform(i), n = this.restorePointerVpt(e); return fabric.util.transformPoint(n, r); }, isTargetTransparent: function (t, e, i) { if (t.shouldCache() && t._cacheCanvas && t !== this._activeObject) { var r = this._normalizePointer(t, { x: e, y: i }), n = Math.max(t.cacheTranslationX + r.x * t.zoomX, 0), s = Math.max(t.cacheTranslationY + r.y * t.zoomY, 0); return fabric.util.isTransparent(t._cacheContext, Math.round(n), Math.round(s), this.targetFindTolerance); } var o = this.contextCache, a = t.selectionBackgroundColor, c = this.viewportTransform; return t.selectionBackgroundColor = "", this.clearContext(o), o.save(), o.transform(c[0], c[1], c[2], c[3], c[4], c[5]), t.render(o), o.restore(), t.selectionBackgroundColor = a, fabric.util.isTransparent(o, e, i, this.targetFindTolerance); }, _isSelectionKeyPressed: function (e) { return Array.isArray(this.selectionKey) ? !!this.selectionKey.find(function (t) { return !0 === e[t]; }) : e[this.selectionKey]; }, _shouldClearSelection: function (t, e) { var i = this.getActiveObjects(), r = this._activeObject; return !e || e && r && 1 < i.length && -1 === i.indexOf(e) && r !== e && !this._isSelectionKeyPressed(t) || e && !e.evented || e && !e.selectable && r && r !== e; }, _shouldCenterTransform: function (t, e, i) { var r; if (t) return "scale" === e || "scaleX" === e || "scaleY" === e || "resizing" === e ? r = this.centeredScaling || t.centeredScaling : "rotate" === e && (r = this.centeredRotation || t.centeredRotation), r ? !i : i; }, _getOriginFromCorner: function (t, e) { var i = { x: t.originX, y: t.originY }; return "ml" === e || "tl" === e || "bl" === e ? i.x = "right" : "mr" !== e && "tr" !== e && "br" !== e || (i.x = "left"), "tl" === e || "mt" === e || "tr" === e ? i.y = "bottom" : "bl" !== e && "mb" !== e && "br" !== e || (i.y = "top"), i; }, _getActionFromCorner: function (t, e, i, r) { if (!e || !t) return "drag"; var n = r.controls[e]; return n.getActionName(i, n, r); }, _setupCurrentTransform: function (t, e, i) { if (e) { var r = this.getPointer(t), n = e.__corner, s = e.controls[n], o = i && n ? s.getActionHandler(t, e, s) : fabric.controlsUtils.dragHandler, a = this._getActionFromCorner(i, n, t, e), c = this._getOriginFromCorner(e, n), h = t[this.centeredKey], l = { target: e, action: a, actionHandler: o, corner: n, scaleX: e.scaleX, scaleY: e.scaleY, skewX: e.skewX, skewY: e.skewY, offsetX: r.x - e.left, offsetY: r.y - e.top, originX: c.x, originY: c.y, ex: r.x, ey: r.y, lastX: r.x, lastY: r.y, theta: u(e.angle), width: e.width * e.scaleX, shiftKey: t.shiftKey, altKey: h, original: fabric.util.saveObjectTransform(e) }; this._shouldCenterTransform(e, a, h) && (l.originX = "center", l.originY = "center"), l.original.originX = c.x, l.original.originY = c.y, this._currentTransform = l, this._beforeTransform(t); } }, setCursor: function (t) { this.upperCanvasEl.style.cursor = t; }, _drawSelection: function (t) { var e = this._groupSelector, i = new fabric.Point(e.ex, e.ey), r = fabric.util.transformPoint(i, this.viewportTransform), n = new fabric.Point(e.ex + e.left, e.ey + e.top), s = fabric.util.transformPoint(n, this.viewportTransform), o = Math.min(r.x, s.x), a = Math.min(r.y, s.y), c = Math.max(r.x, s.x), h = Math.max(r.y, s.y), l = this.selectionLineWidth / 2; this.selectionColor && (t.fillStyle = this.selectionColor, t.fillRect(o, a, c - o, h - a)), this.selectionLineWidth && this.selectionBorderColor && (t.lineWidth = this.selectionLineWidth, t.strokeStyle = this.selectionBorderColor, o += l, a += l, c -= l, h -= l, fabric.Object.prototype._setLineDash.call(this, t, this.selectionDashArray), t.strokeRect(o, a, c - o, h - a)); }, findTarget: function (t, e) { if (!this.skipTargetFind) { var i, r, n = this.getPointer(t, !0), s = this._activeObject, o = this.getActiveObjects(), a = l(t), c = 1 < o.length && !e || 1 === o.length; if (this.targets = [], c && s._findTargetCorner(n, a)) return s; if (1 < o.length && !e && s === this._searchPossibleTargets([s], n)) return s; if (1 === o.length && s === this._searchPossibleTargets([s], n)) { if (!this.preserveObjectStacking) return s; i = s, r = this.targets, this.targets = []; } var h = this._searchPossibleTargets(this._objects, n); return t[this.altSelectionKey] && h && i && h !== i && (h = i, this.targets = r), h; } }, _checkTarget: function (t, e, i) { if (e && e.visible && e.evented && e.containsPoint(t)) { if (!this.perPixelTargetFind && !e.perPixelTargetFind || e.isEditing) return !0; if (!this.isTargetTransparent(e, i.x, i.y)) return !0; } }, _searchPossibleTargets: function (t, e) { for (var i, r, n = t.length; n--;) { var s = t[n], o = s.group ? this._normalizePointer(s.group, e) : e; if (this._checkTarget(o, s, e)) { (i = t[n]).subTargetCheck && i instanceof fabric.Group && (r = this._searchPossibleTargets(i._objects, e)) && this.targets.push(r); break; } } return i; }, restorePointerVpt: function (t) { return fabric.util.transformPoint(t, fabric.util.invertTransform(this.viewportTransform)); }, getPointer: function (t, e) { if (this._absolutePointer && !e) return this._absolutePointer; if (this._pointer && e) return this._pointer; var i, r = h(t), n = this.upperCanvasEl, s = n.getBoundingClientRect(), o = s.width || 0, a = s.height || 0; o && a || ("top" in s && "bottom" in s && (a = Math.abs(s.top - s.bottom)), "right" in s && "left" in s && (o = Math.abs(s.right - s.left))), this.calcOffset(), r.x = r.x - this._offset.left, r.y = r.y - this._offset.top, e || (r = this.restorePointerVpt(r)); var c = this.getRetinaScaling(); return 1 !== c && (r.x /= c, r.y /= c), i = 0 === o || 0 === a ? { width: 1, height: 1 } : { width: n.width / o, height: n.height / a }, { x: r.x * i.width, y: r.y * i.height }; }, _createUpperCanvas: function () { var t = this.lowerCanvasEl.className.replace(/\s*lower-canvas\s*/, ""), e = this.lowerCanvasEl, i = this.upperCanvasEl; i ? i.className = "" : (i = this._createCanvasElement(), this.upperCanvasEl = i), fabric.util.addClass(i, "upper-canvas " + t), this.wrapperEl.appendChild(i), this._copyCanvasStyle(e, i), this._applyCanvasStyle(i), this.contextTop = i.getContext("2d"); }, getTopContext: function () { return this.contextTop; }, _createCacheCanvas: function () { this.cacheCanvasEl = this._createCanvasElement(), this.cacheCanvasEl.setAttribute("width", this.width), this.cacheCanvasEl.setAttribute("height", this.height), this.contextCache = this.cacheCanvasEl.getContext("2d"); }, _initWrapperElement: function () { this.wrapperEl = fabric.util.wrapElement(this.lowerCanvasEl, "div", { class: this.containerClass }), fabric.util.setStyle(this.wrapperEl, { width: this.width + "px", height: this.height + "px", position: "relative" }), fabric.util.makeElementUnselectable(this.wrapperEl); }, _applyCanvasStyle: function (t) { var e = this.width || t.width, i = this.height || t.height; fabric.util.setStyle(t, { position: "absolute", width: e + "px", height: i + "px", left: 0, top: 0, "touch-action": this.allowTouchScrolling ? "manipulation" : "none", "-ms-touch-action": this.allowTouchScrolling ? "manipulation" : "none" }), t.width = e, t.height = i, fabric.util.makeElementUnselectable(t); }, _copyCanvasStyle: function (t, e) { e.style.cssText = t.style.cssText; }, getSelectionContext: function () { return this.contextTop; }, getSelectionElement: function () { return this.upperCanvasEl; }, getActiveObject: function () { return this._activeObject; }, getActiveObjects: function () { var t = this._activeObject; return t ? "activeSelection" === t.type && t._objects ? t._objects.slice(0) : [t] : []; }, _onObjectRemoved: function (t) { t === this._activeObject && (this.fire("before:selection:cleared", { target: t }), this._discardActiveObject(), this.fire("selection:cleared", { target: t }), t.fire("deselected")), t === this._hoveredTarget && (this._hoveredTarget = null, this._hoveredTargets = []), this.callSuper("_onObjectRemoved", t); }, _fireSelectionEvents: function (e, i) { var r = !1, n = this.getActiveObjects(), s = [], o = []; e.forEach(function (t) { -1 === n.indexOf(t) && (r = !0, t.fire("deselected", { e: i, target: t }), o.push(t)); }), n.forEach(function (t) { -1 === e.indexOf(t) && (r = !0, t.fire("selected", { e: i, target: t }), s.push(t)); }), 0 < e.length && 0 < n.length ? r && this.fire("selection:updated", { e: i, selected: s, deselected: o }) : 0 < n.length ? this.fire("selection:created", { e: i, selected: s }) : 0 < e.length && this.fire("selection:cleared", { e: i, deselected: o }); }, setActiveObject: function (t, e) { var i = this.getActiveObjects(); return this._setActiveObject(t, e), this._fireSelectionEvents(i, e), this; }, _setActiveObject: function (t, e) { return this._activeObject !== t && (!!this._discardActiveObject(e, t) && (!t.onSelect({ e: e }) && (this._activeObject = t, !0))); }, _discardActiveObject: function (t, e) { var i = this._activeObject; if (i) { if (i.onDeselect({ e: t, object: e })) return !1; this._activeObject = null; } return !0; }, discardActiveObject: function (t) { var e = this.getActiveObjects(), i = this.getActiveObject(); return e.length && this.fire("before:selection:cleared", { target: i, e: t }), this._discardActiveObject(t), this._fireSelectionEvents(e, t), this; }, dispose: function () { var t = this.wrapperEl; return this.removeListeners(), t.removeChild(this.upperCanvasEl), t.removeChild(this.lowerCanvasEl), this.contextCache = null, this.contextTop = null, ["upperCanvasEl", "cacheCanvasEl"].forEach(function (t) { fabric.util.cleanUpJsdomNode(this[t]), this[t] = void 0; }.bind(this)), t.parentNode && t.parentNode.replaceChild(this.lowerCanvasEl, this.wrapperEl), delete this.wrapperEl, fabric.StaticCanvas.prototype.dispose.call(this), this; }, clear: function () { return this.discardActiveObject(), this.clearContext(this.contextTop), this.callSuper("clear"); }, drawControls: function (t) { var e = this._activeObject; e && e._renderControls(t); }, _toObject: function (t, e, i) { var r = this._realizeGroupTransformOnObject(t), n = this.callSuper("_toObject", t, e, i); return this._unwindGroupTransformOnObject(t, r), n; }, _realizeGroupTransformOnObject: function (e) { if (e.group && "activeSelection" === e.group.type && this._activeObject === e.group) { var i = {}; return ["angle", "flipX", "flipY", "left", "scaleX", "scaleY", "skewX", "skewY", "top"].forEach(function (t) { i[t] = e[t]; }), fabric.util.addTransformToObject(e, this._activeObject.calcOwnMatrix()), i; } return null; }, _unwindGroupTransformOnObject: function (t, e) { e && t.set(e); }, _setSVGObject: function (t, e, i) { var r = this._realizeGroupTransformOnObject(e); this.callSuper("_setSVGObject", t, e, i), this._unwindGroupTransformOnObject(e, r); }, setViewportTransform: function (t) { this.renderOnAddRemove && this._activeObject && this._activeObject.isEditing && this._activeObject.clearContextTop(), fabric.StaticCanvas.prototype.setViewportTransform.call(this, t); } }), fabric.StaticCanvas) "prototype" !== t && (fabric.Canvas[t] = fabric.StaticCanvas[t]); }(), function () { var r = fabric.util.addListener, n = fabric.util.removeListener, s = { passive: !1 }; function d(t, e) { return t.button && t.button === e - 1; } fabric.util.object.extend(fabric.Canvas.prototype, { mainTouchId: null, _initEventListeners: function () { this.removeListeners(), this._bindEvents(), this.addOrRemove(r, "add"); }, _getEventPrefix: function () { return this.enablePointerEvents ? "pointer" : "mouse"; }, addOrRemove: function (t, e) { var i = this.upperCanvasEl, r = this._getEventPrefix(); t(fabric.window, "resize", this._onResize), t(i, r + "down", this._onMouseDown), t(i, r + "move", this._onMouseMove, s), t(i, r + "out", this._onMouseOut), t(i, r + "enter", this._onMouseEnter), t(i, "wheel", this._onMouseWheel), t(i, "contextmenu", this._onContextMenu), t(i, "dblclick", this._onDoubleClick), t(i, "dragover", this._onDragOver), t(i, "dragenter", this._onDragEnter), t(i, "dragleave", this._onDragLeave), t(i, "drop", this._onDrop), this.enablePointerEvents || t(i, "touchstart", this._onTouchStart, s), "undefined" != typeof eventjs && e in eventjs && (eventjs[e](i, "gesture", this._onGesture), eventjs[e](i, "drag", this._onDrag), eventjs[e](i, "orientation", this._onOrientationChange), eventjs[e](i, "shake", this._onShake), eventjs[e](i, "longpress", this._onLongPress)); }, removeListeners: function () { this.addOrRemove(n, "remove"); var t = this._getEventPrefix(); n(fabric.document, t + "up", this._onMouseUp), n(fabric.document, "touchend", this._onTouchEnd, s), n(fabric.document, t + "move", this._onMouseMove, s), n(fabric.document, "touchmove", this._onMouseMove, s); }, _bindEvents: function () { this.eventsBound || (this._onMouseDown = this._onMouseDown.bind(this), this._onTouchStart = this._onTouchStart.bind(this), this._onMouseMove = this._onMouseMove.bind(this), this._onMouseUp = this._onMouseUp.bind(this), this._onTouchEnd = this._onTouchEnd.bind(this), this._onResize = this._onResize.bind(this), this._onGesture = this._onGesture.bind(this), this._onDrag = this._onDrag.bind(this), this._onShake = this._onShake.bind(this), this._onLongPress = this._onLongPress.bind(this), this._onOrientationChange = this._onOrientationChange.bind(this), this._onMouseWheel = this._onMouseWheel.bind(this), this._onMouseOut = this._onMouseOut.bind(this), this._onMouseEnter = this._onMouseEnter.bind(this), this._onContextMenu = this._onContextMenu.bind(this), this._onDoubleClick = this._onDoubleClick.bind(this), this._onDragOver = this._onDragOver.bind(this), this._onDragEnter = this._simpleEventHandler.bind(this, "dragenter"), this._onDragLeave = this._simpleEventHandler.bind(this, "dragleave"), this._onDrop = this._onDrop.bind(this), this.eventsBound = !0); }, _onGesture: function (t, e) { this.__onTransformGesture && this.__onTransformGesture(t, e); }, _onDrag: function (t, e) { this.__onDrag && this.__onDrag(t, e); }, _onMouseWheel: function (t) { this.__onMouseWheel(t); }, _onMouseOut: function (e) { var i = this._hoveredTarget; this.fire("mouse:out", { target: i, e: e }), this._hoveredTarget = null, i && i.fire("mouseout", { e: e }); var r = this; this._hoveredTargets.forEach(function (t) { r.fire("mouse:out", { target: i, e: e }), t && i.fire("mouseout", { e: e }); }), this._hoveredTargets = []; }, _onMouseEnter: function (t) { this._currentTransform || this.findTarget(t) || (this.fire("mouse:over", { target: null, e: t }), this._hoveredTarget = null, this._hoveredTargets = []); }, _onOrientationChange: function (t, e) { this.__onOrientationChange && this.__onOrientationChange(t, e); }, _onShake: function (t, e) { this.__onShake && this.__onShake(t, e); }, _onLongPress: function (t, e) { this.__onLongPress && this.__onLongPress(t, e); }, _onDragOver: function (t) { t.preventDefault(); var e = this._simpleEventHandler("dragover", t); this._fireEnterLeaveEvents(e, t); }, _onDrop: function (t) { return this._simpleEventHandler("drop:before", t), this._simpleEventHandler("drop", t); }, _onContextMenu: function (t) { return this.stopContextMenu && (t.stopPropagation(), t.preventDefault()), !1; }, _onDoubleClick: function (t) { this._cacheTransformEventData(t), this._handleEvent(t, "dblclick"), this._resetTransformEventData(t); }, getPointerId: function (t) { var e = t.changedTouches; return e ? e[0] && e[0].identifier : this.enablePointerEvents ? t.pointerId : -1; }, _isMainEvent: function (t) { return !0 === t.isPrimary || !1 !== t.isPrimary && ("touchend" === t.type && 0 === t.touches.length || (!t.changedTouches || t.changedTouches[0].identifier === this.mainTouchId)); }, _onTouchStart: function (t) { t.preventDefault(), null === this.mainTouchId && (this.mainTouchId = this.getPointerId(t)), this.__onMouseDown(t), this._resetTransformEventData(); var e = this.upperCanvasEl, i = this._getEventPrefix(); r(fabric.document, "touchend", this._onTouchEnd, s), r(fabric.document, "touchmove", this._onMouseMove, s), n(e, i + "down", this._onMouseDown); }, _onMouseDown: function (t) { this.__onMouseDown(t), this._resetTransformEventData(); var e = this.upperCanvasEl, i = this._getEventPrefix(); n(e, i + "move", this._onMouseMove, s), r(fabric.document, i + "up", this._onMouseUp), r(fabric.document, i + "move", this._onMouseMove, s); }, _onTouchEnd: function (t) { if (!(0 < t.touches.length)) { this.__onMouseUp(t), this._resetTransformEventData(), this.mainTouchId = null; var e = this._getEventPrefix(); n(fabric.document, "touchend", this._onTouchEnd, s), n(fabric.document, "touchmove", this._onMouseMove, s); var i = this; this._willAddMouseDown && clearTimeout(this._willAddMouseDown), this._willAddMouseDown = setTimeout(function () { r(i.upperCanvasEl, e + "down", i._onMouseDown), i._willAddMouseDown = 0; }, 400); } }, _onMouseUp: function (t) { this.__onMouseUp(t), this._resetTransformEventData(); var e = this.upperCanvasEl, i = this._getEventPrefix(); this._isMainEvent(t) && (n(fabric.document, i + "up", this._onMouseUp), n(fabric.document, i + "move", this._onMouseMove, s), r(e, i + "move", this._onMouseMove, s)); }, _onMouseMove: function (t) { !this.allowTouchScrolling && t.preventDefault && t.preventDefault(), this.__onMouseMove(t); }, _onResize: function () { this.calcOffset(); }, _shouldRender: function (t) { var e = this._activeObject; return !!(!!e != !!t || e && t && e !== t) || (e && e.isEditing, !1); }, __onMouseUp: function (t) { var e, i = this._currentTransform, r = this._groupSelector, n = !1, s = !r || 0 === r.left && 0 === r.top; if (this._cacheTransformEventData(t), e = this._target, this._handleEvent(t, "up:before"), d(t, 3)) this.fireRightClick && this._handleEvent(t, "up", 3, s); else { if (d(t, 2)) return this.fireMiddleClick && this._handleEvent(t, "up", 2, s), void this._resetTransformEventData(); if (this.isDrawingMode && this._isCurrentlyDrawing) this._onMouseUpInDrawingMode(t); else if (this._isMainEvent(t)) { if (i && (this._finalizeCurrentTransform(t), n = i.actionPerformed), !s) { var o = e === this._activeObject; this._maybeGroupObjects(t), n || (n = this._shouldRender(e) || !o && e === this._activeObject); } var a, c; if (e) { if (a = e._findTargetCorner(this.getPointer(t, !0), fabric.util.isTouchEvent(t)), e.selectable && e !== this._activeObject && "up" === e.activeOn) this.setActiveObject(e, t), n = !0; else { var h = e.controls[a], l = h && h.getMouseUpHandler(t, e, h); l && l(t, i, (c = this.getPointer(t)).x, c.y); } e.isMoving = !1; } if (i && (i.target !== e || i.corner !== a)) { var u = i.target && i.target.controls[i.corner], f = u && u.getMouseUpHandler(t, e, h); c = c || this.getPointer(t), f && f(t, i, c.x, c.y); } this._setCursorFromEvent(t, e), this._handleEvent(t, "up", 1, s), this._groupSelector = null, this._currentTransform = null, e && (e.__corner = 0), n ? this.requestRenderAll() : s || this.renderTop(); } } }, _simpleEventHandler: function (t, e) { var i = this.findTarget(e), r = this.targets, n = { e: e, target: i, subTargets: r }; if (this.fire(t, n), i && i.fire(t, n), !r) return i; for (var s = 0; s < r.length; s++)r[s].fire(t, n); return i; }, _handleEvent: function (t, e, i, r) { var n = this._target, s = this.targets || [], o = { e: t, target: n, subTargets: s, button: i || 1, isClick: r || !1, pointer: this._pointer, absolutePointer: this._absolutePointer, transform: this._currentTransform }; "up" === e && (o.currentTarget = this.findTarget(t), o.currentSubTargets = this.targets), this.fire("mouse:" + e, o), n && n.fire("mouse" + e, o); for (var a = 0; a < s.length; a++)s[a].fire("mouse" + e, o); }, _finalizeCurrentTransform: function (t) { var e = this._currentTransform, i = e.target, r = { e: t, target: i, transform: e, action: e.action }; i._scaling && (i._scaling = !1), i.setCoords(), (e.actionPerformed || this.stateful && i.hasStateChanged()) && this._fire("modified", r); }, _onMouseDownInDrawingMode: function (t) { this._isCurrentlyDrawing = !0, this.getActiveObject() && this.discardActiveObject(t).requestRenderAll(); var e = this.getPointer(t); this.freeDrawingBrush.onMouseDown(e, { e: t, pointer: e }), this._handleEvent(t, "down"); }, _onMouseMoveInDrawingMode: function (t) { if (this._isCurrentlyDrawing) { var e = this.getPointer(t); this.freeDrawingBrush.onMouseMove(e, { e: t, pointer: e }); } this.setCursor(this.freeDrawingCursor), this._handleEvent(t, "move"); }, _onMouseUpInDrawingMode: function (t) { var e = this.getPointer(t); this._isCurrentlyDrawing = this.freeDrawingBrush.onMouseUp({ e: t, pointer: e }), this._handleEvent(t, "up"); }, __onMouseDown: function (t) { this._cacheTransformEventData(t), this._handleEvent(t, "down:before"); var e = this._target; if (d(t, 3)) this.fireRightClick && this._handleEvent(t, "down", 3); else if (d(t, 2)) this.fireMiddleClick && this._handleEvent(t, "down", 2); else if (this.isDrawingMode) this._onMouseDownInDrawingMode(t); else if (this._isMainEvent(t) && !this._currentTransform) { var i = this._pointer; this._previousPointer = i; var r = this._shouldRender(e), n = this._shouldGroup(t, e); if (this._shouldClearSelection(t, e) ? this.discardActiveObject(t) : n && (this._handleGrouping(t, e), e = this._activeObject), !this.selection || e && (e.selectable || e.isEditing || e === this._activeObject) || (this._groupSelector = { ex: this._absolutePointer.x, ey: this._absolutePointer.y, top: 0, left: 0 }), e) { var s = e === this._activeObject; e.selectable && "down" === e.activeOn && this.setActiveObject(e, t); var o = e._findTargetCorner(this.getPointer(t, !0), fabric.util.isTouchEvent(t)); if (e.__corner = o, e === this._activeObject && (o || !n)) { this._setupCurrentTransform(t, e, s); var a = e.controls[o], c = (i = this.getPointer(t), a && a.getMouseDownHandler(t, e, a)); c && c(t, this._currentTransform, i.x, i.y); } } this._handleEvent(t, "down"), (r || n) && this.requestRenderAll(); } }, _resetTransformEventData: function () { this._target = null, this._pointer = null, this._absolutePointer = null; }, _cacheTransformEventData: function (t) { this._resetTransformEventData(), this._pointer = this.getPointer(t, !0), this._absolutePointer = this.restorePointerVpt(this._pointer), this._target = this._currentTransform ? this._currentTransform.target : this.findTarget(t) || null; }, _beforeTransform: function (t) { var e = this._currentTransform; this.stateful && e.target.saveState(), this.fire("before:transform", { e: t, transform: e }); }, __onMouseMove: function (t) { var e, i; if (this._handleEvent(t, "move:before"), this._cacheTransformEventData(t), this.isDrawingMode) this._onMouseMoveInDrawingMode(t); else if (this._isMainEvent(t)) { var r = this._groupSelector; r ? (i = this._absolutePointer, r.left = i.x - r.ex, r.top = i.y - r.ey, this.renderTop()) : this._currentTransform ? this._transformObject(t) : (e = this.findTarget(t) || null, this._setCursorFromEvent(t, e), this._fireOverOutEvents(e, t)), this._handleEvent(t, "move"), this._resetTransformEventData(); } }, _fireOverOutEvents: function (t, e) { var i = this._hoveredTarget, r = this._hoveredTargets, n = this.targets, s = Math.max(r.length, n.length); this.fireSyntheticInOutEvents(t, e, { oldTarget: i, evtOut: "mouseout", canvasEvtOut: "mouse:out", evtIn: "mouseover", canvasEvtIn: "mouse:over" }); for (var o = 0; o < s; o++)this.fireSyntheticInOutEvents(n[o], e, { oldTarget: r[o], evtOut: "mouseout", evtIn: "mouseover" }); this._hoveredTarget = t, this._hoveredTargets = this.targets.concat(); }, _fireEnterLeaveEvents: function (t, e) { var i = this._draggedoverTarget, r = this._hoveredTargets, n = this.targets, s = Math.max(r.length, n.length); this.fireSyntheticInOutEvents(t, e, { oldTarget: i, evtOut: "dragleave", evtIn: "dragenter" }); for (var o = 0; o < s; o++)this.fireSyntheticInOutEvents(n[o], e, { oldTarget: r[o], evtOut: "dragleave", evtIn: "dragenter" }); this._draggedoverTarget = t; }, fireSyntheticInOutEvents: function (t, e, i) { var r, n, s, o = i.oldTarget, a = o !== t, c = i.canvasEvtIn, h = i.canvasEvtOut; a && (r = { e: e, target: t, previousTarget: o }, n = { e: e, target: o, nextTarget: t }), s = t && a, o && a && (h && this.fire(h, n), o.fire(i.evtOut, n)), s && (c && this.fire(c, r), t.fire(i.evtIn, r)); }, __onMouseWheel: function (t) { this._cacheTransformEventData(t), this._handleEvent(t, "wheel"), this._resetTransformEventData(); }, _transformObject: function (t) { var e = this.getPointer(t), i = this._currentTransform; i.reset = !1, i.shiftKey = t.shiftKey, i.altKey = t[this.centeredKey], this._performTransformAction(t, i, e), i.actionPerformed && this.requestRenderAll(); }, _performTransformAction: function (t, e, i) { var r = i.x, n = i.y, s = e.action, o = !1, a = e.actionHandler; a && (o = a(t, e, r, n)), "drag" === s && o && (e.target.isMoving = !0, this.setCursor(e.target.moveCursor || this.moveCursor)), e.actionPerformed = e.actionPerformed || o; }, _fire: fabric.controlsUtils.fireEvent, _setCursorFromEvent: function (t, e) { if (!e) return this.setCursor(this.defaultCursor), !1; var i = e.hoverCursor || this.hoverCursor, r = this._activeObject && "activeSelection" === this._activeObject.type ? this._activeObject : null, n = (!r || !r.contains(e)) && e._findTargetCorner(this.getPointer(t, !0)); n ? this.setCursor(this.getCornerCursor(n, e, t)) : (e.subTargetCheck && this.targets.concat().reverse().map(function (t) { i = t.hoverCursor || i; }), this.setCursor(i)); }, getCornerCursor: function (t, e, i) { var r = e.controls[t]; return r.cursorStyleHandler(i, r, e); } }); }(), function () { var f = Math.min, d = Math.max; fabric.util.object.extend(fabric.Canvas.prototype, { _shouldGroup: function (t, e) { var i = this._activeObject; return i && this._isSelectionKeyPressed(t) && e && e.selectable && this.selection && (i !== e || "activeSelection" === i.type) && !e.onSelect({ e: t }); }, _handleGrouping: function (t, e) { var i = this._activeObject; i.__corner || (e !== i || (e = this.findTarget(t, !0)) && e.selectable) && (i && "activeSelection" === i.type ? this._updateActiveSelection(e, t) : this._createActiveSelection(e, t)); }, _updateActiveSelection: function (t, e) { var i = this._activeObject, r = i._objects.slice(0); i.contains(t) ? (i.removeWithUpdate(t), this._hoveredTarget = t, this._hoveredTargets = this.targets.concat(), 1 === i.size() && this._setActiveObject(i.item(0), e)) : (i.addWithUpdate(t), this._hoveredTarget = i, this._hoveredTargets = this.targets.concat()), this._fireSelectionEvents(r, e); }, _createActiveSelection: function (t, e) { var i = this.getActiveObjects(), r = this._createGroup(t); this._hoveredTarget = r, this._setActiveObject(r, e), this._fireSelectionEvents(i, e); }, _createGroup: function (t) { var e = this._objects, i = e.indexOf(this._activeObject) < e.indexOf(t) ? [this._activeObject, t] : [t, this._activeObject]; return this._activeObject.isEditing && this._activeObject.exitEditing(), new fabric.ActiveSelection(i, { canvas: this }); }, _groupSelectedObjects: function (t) { var e, i = this._collectObjects(t); 1 === i.length ? this.setActiveObject(i[0], t) : 1 < i.length && (e = new fabric.ActiveSelection(i.reverse(), { canvas: this }), this.setActiveObject(e, t)); }, _collectObjects: function (e) { for (var t, i = [], r = this._groupSelector.ex, n = this._groupSelector.ey, s = r + this._groupSelector.left, o = n + this._groupSelector.top, a = new fabric.Point(f(r, s), f(n, o)), c = new fabric.Point(d(r, s), d(n, o)), h = !this.selectionFullyContained, l = r === s && n === o, u = this._objects.length; u-- && !((t = this._objects[u]) && t.selectable && t.visible && (h && t.intersectsWithRect(a, c, !0) || t.isContainedWithinRect(a, c, !0) || h && t.containsPoint(a, null, !0) || h && t.containsPoint(c, null, !0)) && (i.push(t), l));); return 1 < i.length && (i = i.filter(function (t) { return !t.onSelect({ e: e }); })), i; }, _maybeGroupObjects: function (t) { this.selection && this._groupSelector && this._groupSelectedObjects(t), this.setCursor(this.defaultCursor), this._groupSelector = null; } }); }(), fabric.util.object.extend(fabric.StaticCanvas.prototype, { toDataURL: function (t) { t || (t = {}); var e = t.format || "png", i = t.quality || 1, r = (t.multiplier || 1) * (t.enableRetinaScaling ? this.getRetinaScaling() : 1), n = this.toCanvasElement(r, t); return fabric.util.toDataURL(n, e, i); }, toCanvasElement: function (t, e) { t = t || 1; var i = ((e = e || {}).width || this.width) * t, r = (e.height || this.height) * t, n = this.getZoom(), s = this.width, o = this.height, a = n * t, c = this.viewportTransform, h = (c[4] - (e.left || 0)) * t, l = (c[5] - (e.top || 0)) * t, u = this.interactive, f = [a, 0, 0, a, h, l], d = this.enableRetinaScaling, g = fabric.util.createCanvasElement(), p = this.contextTop; return g.width = i, g.height = r, this.contextTop = null, this.enableRetinaScaling = !1, this.interactive = !1, this.viewportTransform = f, this.width = i, this.height = r, this.calcViewportBoundaries(), this.renderCanvas(g.getContext("2d"), this._objects), this.viewportTransform = c, this.width = s, this.height = o, this.calcViewportBoundaries(), this.interactive = u, this.enableRetinaScaling = d, this.contextTop = p, g; } }), fabric.util.object.extend(fabric.StaticCanvas.prototype, { loadFromJSON: function (t, i, e) { if (t) { var r = "string" == typeof t ? JSON.parse(t) : fabric.util.object.clone(t), n = this, s = r.clipPath, o = this.renderOnAddRemove; return this.renderOnAddRemove = !1, delete r.clipPath, this._enlivenObjects(r.objects, function (e) { n.clear(), n._setBgOverlay(r, function () { s ? n._enlivenObjects([s], function (t) { n.clipPath = t[0], n.__setupCanvas.call(n, r, e, o, i); }) : n.__setupCanvas.call(n, r, e, o, i); }); }, e), this; } }, __setupCanvas: function (t, e, i, r) { var n = this; e.forEach(function (t, e) { n.insertAt(t, e); }), this.renderOnAddRemove = i, delete t.objects, delete t.backgroundImage, delete t.overlayImage, delete t.background, delete t.overlay, this._setOptions(t), this.renderAll(), r && r(); }, _setBgOverlay: function (t, e) { var i = { backgroundColor: !1, overlayColor: !1, backgroundImage: !1, overlayImage: !1 }; if (t.backgroundImage || t.overlayImage || t.background || t.overlay) { var r = function () { i.backgroundImage && i.overlayImage && i.backgroundColor && i.overlayColor && e && e(); }; this.__setBgOverlay("backgroundImage", t.backgroundImage, i, r), this.__setBgOverlay("overlayImage", t.overlayImage, i, r), this.__setBgOverlay("backgroundColor", t.background, i, r), this.__setBgOverlay("overlayColor", t.overlay, i, r); } else e && e(); }, __setBgOverlay: function (e, t, i, r) { var n = this; if (!t) return i[e] = !0, void (r && r()); "backgroundImage" === e || "overlayImage" === e ? fabric.util.enlivenObjects([t], function (t) { n[e] = t[0], i[e] = !0, r && r(); }) : this["set" + fabric.util.string.capitalize(e, !0)](t, function () { i[e] = !0, r && r(); }); }, _enlivenObjects: function (t, e, i) { t && 0 !== t.length ? fabric.util.enlivenObjects(t, function (t) { e && e(t); }, null, i) : e && e([]); }, _toDataURL: function (e, i) { this.clone(function (t) { i(t.toDataURL(e)); }); }, _toDataURLWithMultiplier: function (e, i, r) { this.clone(function (t) { r(t.toDataURLWithMultiplier(e, i)); }); }, clone: function (e, t) { var i = JSON.stringify(this.toJSON(t)); this.cloneWithoutData(function (t) { t.loadFromJSON(i, function () { e && e(t); }); }); }, cloneWithoutData: function (t) { var e = fabric.util.createCanvasElement(); e.width = this.width, e.height = this.height; var i = new fabric.Canvas(e); this.backgroundImage ? (i.setBackgroundImage(this.backgroundImage.src, function () { i.renderAll(), t && t(i); }), i.backgroundImageOpacity = this.backgroundImageOpacity, i.backgroundImageStretch = this.backgroundImageStretch) : t && t(i); } }), function (t) { "use strict"; var x = t.fabric || (t.fabric = {}), e = x.util.object.extend, s = x.util.object.clone, r = x.util.toFixed, i = x.util.string.capitalize, a = x.util.degreesToRadians, n = !x.isLikelyNode; x.Object || (x.Object = x.util.createClass(x.CommonMethods, { type: "object", originX: "left", originY: "top", top: 0, left: 0, width: 0, height: 0, scaleX: 1, scaleY: 1, flipX: !1, flipY: !1, opacity: 1, angle: 0, skewX: 0, skewY: 0, cornerSize: 13, touchCornerSize: 24, transparentCorners: !0, hoverCursor: null, moveCursor: null, padding: 0, borderColor: "rgb(178,204,255)", borderDashArray: null, cornerColor: "rgb(178,204,255)", cornerStrokeColor: null, cornerStyle: "rect", cornerDashArray: null, centeredScaling: !1, centeredRotation: !0, fill: "rgb(0,0,0)", fillRule: "nonzero", globalCompositeOperation: "source-over", backgroundColor: "", selectionBackgroundColor: "", stroke: null, strokeWidth: 1, strokeDashArray: null, strokeDashOffset: 0, strokeLineCap: "butt", strokeLineJoin: "miter", strokeMiterLimit: 4, shadow: null, borderOpacityWhenMoving: .4, borderScaleFactor: 1, minScaleLimit: 0, selectable: !0, evented: !0, visible: !0, hasControls: !0, hasBorders: !0, perPixelTargetFind: !1, includeDefaultValues: !0, lockMovementX: !1, lockMovementY: !1, lockRotation: !1, lockScalingX: !1, lockScalingY: !1, lockSkewingX: !1, lockSkewingY: !1, lockScalingFlip: !1, excludeFromExport: !1, objectCaching: n, statefullCache: !1, noScaleCache: !0, strokeUniform: !1, dirty: !0, __corner: 0, paintFirst: "fill", activeOn: "down", stateProperties: "top left width height scaleX scaleY flipX flipY originX originY transformMatrix stroke strokeWidth strokeDashArray strokeLineCap strokeDashOffset strokeLineJoin strokeMiterLimit angle opacity fill globalCompositeOperation shadow visible backgroundColor skewX skewY fillRule paintFirst clipPath strokeUniform".split(" "), cacheProperties: "fill stroke strokeWidth strokeDashArray width height paintFirst strokeUniform strokeLineCap strokeDashOffset strokeLineJoin strokeMiterLimit backgroundColor clipPath".split(" "), colorProperties: "fill stroke backgroundColor".split(" "), clipPath: void 0, inverted: !1, absolutePositioned: !1, initialize: function (t) { t && this.setOptions(t); }, _createCacheCanvas: function () { this._cacheProperties = {}, this._cacheCanvas = x.util.createCanvasElement(), this._cacheContext = this._cacheCanvas.getContext("2d"), this._updateCacheCanvas(), this.dirty = !0; }, _limitCacheSize: function (t) { var e = x.perfLimitSizeTotal, i = t.width, r = t.height, n = x.maxCacheSideLimit, s = x.minCacheSideLimit; if (i <= n && r <= n && i * r <= e) return i < s && (t.width = s), r < s && (t.height = s), t; var o = i / r, a = x.util.limitDimsByArea(o, e), c = x.util.capValue, h = c(s, a.x, n), l = c(s, a.y, n); return h < i && (t.zoomX /= i / h, t.width = h, t.capped = !0), l < r && (t.zoomY /= r / l, t.height = l, t.capped = !0), t; }, _getCacheCanvasDimensions: function () { var t = this.getTotalObjectScaling(), e = this._getTransformedDimensions(0, 0), i = e.x * t.scaleX / this.scaleX, r = e.y * t.scaleY / this.scaleY; return { width: i + 2, height: r + 2, zoomX: t.scaleX, zoomY: t.scaleY, x: i, y: r }; }, _updateCacheCanvas: function () { var t = this.canvas; if (this.noScaleCache && t && t._currentTransform) { var e = t._currentTransform.target, i = t._currentTransform.action; if (this === e && i.slice && "scale" === i.slice(0, 5)) return !1; } var r, n, s = this._cacheCanvas, o = this._limitCacheSize(this._getCacheCanvasDimensions()), a = x.minCacheSideLimit, c = o.width, h = o.height, l = o.zoomX, u = o.zoomY, f = c !== this.cacheWidth || h !== this.cacheHeight, d = this.zoomX !== l || this.zoomY !== u, g = f || d, p = 0, v = 0, m = !1; if (f) { var b = this._cacheCanvas.width, y = this._cacheCanvas.height, _ = b < c || y < h; m = _ || (c < .9 * b || h < .9 * y) && a < b && a < y, _ && !o.capped && (a < c || a < h) && (p = .1 * c, v = .1 * h); } return this instanceof x.Text && this.path && (m = g = !0, p += this.getHeightOfLine(0) * this.zoomX, v += this.getHeightOfLine(0) * this.zoomY), !!g && (m ? (s.width = Math.ceil(c + p), s.height = Math.ceil(h + v)) : (this._cacheContext.setTransform(1, 0, 0, 1, 0, 0), this._cacheContext.clearRect(0, 0, s.width, s.height)), r = o.x / 2, n = o.y / 2, this.cacheTranslationX = Math.round(s.width / 2 - r) + r, this.cacheTranslationY = Math.round(s.height / 2 - n) + n, this.cacheWidth = c, this.cacheHeight = h, this._cacheContext.translate(this.cacheTranslationX, this.cacheTranslationY), this._cacheContext.scale(l, u), this.zoomX = l, this.zoomY = u, !0); }, setOptions: function (t) { this._setOptions(t), this._initGradient(t.fill, "fill"), this._initGradient(t.stroke, "stroke"), this._initPattern(t.fill, "fill"), this._initPattern(t.stroke, "stroke"); }, transform: function (t) { var e = this.group && !this.group._transformDone || this.group && this.canvas && t === this.canvas.contextTop, i = this.calcTransformMatrix(!e); t.transform(i[0], i[1], i[2], i[3], i[4], i[5]); }, toObject: function (t) { var e = x.Object.NUM_FRACTION_DIGITS, i = { type: this.type, version: x.version, originX: this.originX, originY: this.originY, left: r(this.left, e), top: r(this.top, e), width: r(this.width, e), height: r(this.height, e), fill: this.fill && this.fill.toObject ? this.fill.toObject() : this.fill, stroke: this.stroke && this.stroke.toObject ? this.stroke.toObject() : this.stroke, strokeWidth: r(this.strokeWidth, e), strokeDashArray: this.strokeDashArray ? this.strokeDashArray.concat() : this.strokeDashArray, strokeLineCap: this.strokeLineCap, strokeDashOffset: this.strokeDashOffset, strokeLineJoin: this.strokeLineJoin, strokeUniform: this.strokeUniform, strokeMiterLimit: r(this.strokeMiterLimit, e), scaleX: r(this.scaleX, e), scaleY: r(this.scaleY, e), angle: r(this.angle, e), flipX: this.flipX, flipY: this.flipY, opacity: r(this.opacity, e), shadow: this.shadow && this.shadow.toObject ? this.shadow.toObject() : this.shadow, visible: this.visible, backgroundColor: this.backgroundColor, fillRule: this.fillRule, paintFirst: this.paintFirst, globalCompositeOperation: this.globalCompositeOperation, skewX: r(this.skewX, e), skewY: r(this.skewY, e) }; return this.clipPath && !this.clipPath.excludeFromExport && (i.clipPath = this.clipPath.toObject(t), i.clipPath.inverted = this.clipPath.inverted, i.clipPath.absolutePositioned = this.clipPath.absolutePositioned), x.util.populateWithProperties(this, i, t), this.includeDefaultValues || (i = this._removeDefaultValues(i)), i; }, toDatalessObject: function (t) { return this.toObject(t); }, _removeDefaultValues: function (e) { var i = x.util.getKlass(e.type).prototype; return i.stateProperties.forEach(function (t) { "left" !== t && "top" !== t && (e[t] === i[t] && delete e[t], Array.isArray(e[t]) && Array.isArray(i[t]) && 0 === e[t].length && 0 === i[t].length && delete e[t]); }), e; }, toString: function () { return "#<fabric." + i(this.type) + ">"; }, getObjectScaling: function () { if (!this.group) return { scaleX: this.scaleX, scaleY: this.scaleY }; var t = x.util.qrDecompose(this.calcTransformMatrix()); return { scaleX: Math.abs(t.scaleX), scaleY: Math.abs(t.scaleY) }; }, getTotalObjectScaling: function () { var t = this.getObjectScaling(), e = t.scaleX, i = t.scaleY; if (this.canvas) { var r = this.canvas.getZoom(), n = this.canvas.getRetinaScaling(); e *= r * n, i *= r * n; } return { scaleX: e, scaleY: i }; }, getObjectOpacity: function () { var t = this.opacity; return this.group && (t *= this.group.getObjectOpacity()), t; }, _set: function (t, e) { var i = "scaleX" === t || "scaleY" === t, r = this[t] !== e, n = !1; return i && (e = this._constrainScale(e)), "scaleX" === t && e < 0 ? (this.flipX = !this.flipX, e *= -1) : "scaleY" === t && e < 0 ? (this.flipY = !this.flipY, e *= -1) : "shadow" !== t || !e || e instanceof x.Shadow ? "dirty" === t && this.group && this.group.set("dirty", e) : e = new x.Shadow(e), this[t] = e, r && (n = this.group && this.group.isOnACache(), -1 < this.cacheProperties.indexOf(t) ? (this.dirty = !0, n && this.group.set("dirty", !0)) : n && -1 < this.stateProperties.indexOf(t) && this.group.set("dirty", !0)), this; }, setOnGroup: function () { }, getViewportTransform: function () { return this.canvas && this.canvas.viewportTransform ? this.canvas.viewportTransform : x.iMatrix.concat(); }, isNotVisible: function () { return 0 === this.opacity || !this.width && !this.height && 0 === this.strokeWidth || !this.visible; }, render: function (t) { this.isNotVisible() || this.canvas && this.canvas.skipOffscreen && !this.group && !this.isOnScreen() || (t.save(), this._setupCompositeOperation(t), this.drawSelectionBackground(t), this.transform(t), this._setOpacity(t), this._setShadow(t, this), this.shouldCache() ? (this.renderCache(), this.drawCacheOnCanvas(t)) : (this._removeCacheCanvas(), this.dirty = !1, this.drawObject(t), this.objectCaching && this.statefullCache && this.saveState({ propertySet: "cacheProperties" })), t.restore()); }, renderCache: function (t) { t = t || {}, this._cacheCanvas && this._cacheContext || this._createCacheCanvas(), this.isCacheDirty() && (this.statefullCache && this.saveState({ propertySet: "cacheProperties" }), this.drawObject(this._cacheContext, t.forClipping), this.dirty = !1); }, _removeCacheCanvas: function () { this._cacheCanvas = null, this._cacheContext = null, this.cacheWidth = 0, this.cacheHeight = 0; }, hasStroke: function () { return this.stroke && "transparent" !== this.stroke && 0 !== this.strokeWidth; }, hasFill: function () { return this.fill && "transparent" !== this.fill; }, needsItsOwnCache: function () { return !("stroke" !== this.paintFirst || !this.hasFill() || !this.hasStroke() || "object" != typeof this.shadow) || !!this.clipPath; }, shouldCache: function () { return this.ownCaching = this.needsItsOwnCache() || this.objectCaching && (!this.group || !this.group.isOnACache()), this.ownCaching; }, willDrawShadow: function () { return !!this.shadow && (0 !== this.shadow.offsetX || 0 !== this.shadow.offsetY); }, drawClipPathOnCache: function (t, e) { if (t.save(), e.inverted ? t.globalCompositeOperation = "destination-out" : t.globalCompositeOperation = "destination-in", e.absolutePositioned) { var i = x.util.invertTransform(this.calcTransformMatrix()); t.transform(i[0], i[1], i[2], i[3], i[4], i[5]); } e.transform(t), t.scale(1 / e.zoomX, 1 / e.zoomY), t.drawImage(e._cacheCanvas, -e.cacheTranslationX, -e.cacheTranslationY), t.restore(); }, drawObject: function (t, e) { var i = this.fill, r = this.stroke; e ? (this.fill = "black", this.stroke = "", this._setClippingProperties(t)) : this._renderBackground(t), this._render(t), this._drawClipPath(t, this.clipPath), this.fill = i, this.stroke = r; }, _drawClipPath: function (t, e) { e && (e.canvas = this.canvas, e.shouldCache(), e._transformDone = !0, e.renderCache({ forClipping: !0 }), this.drawClipPathOnCache(t, e)); }, drawCacheOnCanvas: function (t) { t.scale(1 / this.zoomX, 1 / this.zoomY), t.drawImage(this._cacheCanvas, -this.cacheTranslationX, -this.cacheTranslationY); }, isCacheDirty: function (t) { if (this.isNotVisible()) return !1; if (this._cacheCanvas && this._cacheContext && !t && this._updateCacheCanvas()) return !0; if (this.dirty || this.clipPath && this.clipPath.absolutePositioned || this.statefullCache && this.hasStateChanged("cacheProperties")) { if (this._cacheCanvas && this._cacheContext && !t) { var e = this.cacheWidth / this.zoomX, i = this.cacheHeight / this.zoomY; this._cacheContext.clearRect(-e / 2, -i / 2, e, i); } return !0; } return !1; }, _renderBackground: function (t) { if (this.backgroundColor) { var e = this._getNonTransformedDimensions(); t.fillStyle = this.backgroundColor, t.fillRect(-e.x / 2, -e.y / 2, e.x, e.y), this._removeShadow(t); } }, _setOpacity: function (t) { this.group && !this.group._transformDone ? t.globalAlpha = this.getObjectOpacity() : t.globalAlpha *= this.opacity; }, _setStrokeStyles: function (t, e) { var i = e.stroke; i && (t.lineWidth = e.strokeWidth, t.lineCap = e.strokeLineCap, t.lineDashOffset = e.strokeDashOffset, t.lineJoin = e.strokeLineJoin, t.miterLimit = e.strokeMiterLimit, i.toLive ? "percentage" === i.gradientUnits || i.gradientTransform || i.patternTransform ? this._applyPatternForTransformedGradient(t, i) : (t.strokeStyle = i.toLive(t, this), this._applyPatternGradientTransform(t, i)) : t.strokeStyle = e.stroke); }, _setFillStyles: function (t, e) { var i = e.fill; i && (i.toLive ? (t.fillStyle = i.toLive(t, this), this._applyPatternGradientTransform(t, e.fill)) : t.fillStyle = i); }, _setClippingProperties: function (t) { t.globalAlpha = 1, t.strokeStyle = "transparent", t.fillStyle = "#000000"; }, _setLineDash: function (t, e) { e && 0 !== e.length && (1 & e.length && e.push.apply(e, e), t.setLineDash(e)); }, _renderControls: function (t, e) { var i, r, n, s = this.getViewportTransform(), o = this.calcTransformMatrix(); r = void 0 !== (e = e || {}).hasBorders ? e.hasBorders : this.hasBorders, n = void 0 !== e.hasControls ? e.hasControls : this.hasControls, o = x.util.multiplyTransformMatrices(s, o), i = x.util.qrDecompose(o), t.save(), t.translate(i.translateX, i.translateY), t.lineWidth = 1 * this.borderScaleFactor, this.group || (t.globalAlpha = this.isMoving ? this.borderOpacityWhenMoving : 1), this.flipX && (i.angle -= 180), t.rotate(a(this.group ? i.angle : this.angle)), e.forActiveSelection || this.group ? r && this.drawBordersInGroup(t, i, e) : r && this.drawBorders(t, e), n && this.drawControls(t, e), t.restore(); }, _setShadow: function (t) { if (this.shadow) { var e, i = this.shadow, r = this.canvas, n = r && r.viewportTransform[0] || 1, s = r && r.viewportTransform[3] || 1; e = i.nonScaling ? { scaleX: 1, scaleY: 1 } : this.getObjectScaling(), r && r._isRetinaScaling() && (n *= x.devicePixelRatio, s *= x.devicePixelRatio), t.shadowColor = i.color, t.shadowBlur = i.blur * x.browserShadowBlurConstant * (n + s) * (e.scaleX + e.scaleY) / 4, t.shadowOffsetX = i.offsetX * n * e.scaleX, t.shadowOffsetY = i.offsetY * s * e.scaleY; } }, _removeShadow: function (t) { this.shadow && (t.shadowColor = "", t.shadowBlur = t.shadowOffsetX = t.shadowOffsetY = 0); }, _applyPatternGradientTransform: function (t, e) { if (!e || !e.toLive) return { offsetX: 0, offsetY: 0 }; var i = e.gradientTransform || e.patternTransform, r = -this.width / 2 + e.offsetX || 0, n = -this.height / 2 + e.offsetY || 0; return "percentage" === e.gradientUnits ? t.transform(this.width, 0, 0, this.height, r, n) : t.transform(1, 0, 0, 1, r, n), i && t.transform(i[0], i[1], i[2], i[3], i[4], i[5]), { offsetX: r, offsetY: n }; }, _renderPaintInOrder: function (t) { "stroke" === this.paintFirst ? (this._renderStroke(t), this._renderFill(t)) : (this._renderFill(t), this._renderStroke(t)); }, _render: function () { }, _renderFill: function (t) { this.fill && (t.save(), this._setFillStyles(t, this), "evenodd" === this.fillRule ? t.fill("evenodd") : t.fill(), t.restore()); }, _renderStroke: function (t) { if (this.stroke && 0 !== this.strokeWidth) { if (this.shadow && !this.shadow.affectStroke && this._removeShadow(t), t.save(), this.strokeUniform && this.group) { var e = this.getObjectScaling(); t.scale(1 / e.scaleX, 1 / e.scaleY); } else this.strokeUniform && t.scale(1 / this.scaleX, 1 / this.scaleY); this._setLineDash(t, this.strokeDashArray), this._setStrokeStyles(t, this), t.stroke(), t.restore(); } }, _applyPatternForTransformedGradient: function (t, e) { var i, r = this._limitCacheSize(this._getCacheCanvasDimensions()), n = x.util.createCanvasElement(), s = this.canvas.getRetinaScaling(), o = r.x / this.scaleX / s, a = r.y / this.scaleY / s; n.width = o, n.height = a, (i = n.getContext("2d")).beginPath(), i.moveTo(0, 0), i.lineTo(o, 0), i.lineTo(o, a), i.lineTo(0, a), i.closePath(), i.translate(o / 2, a / 2), i.scale(r.zoomX / this.scaleX / s, r.zoomY / this.scaleY / s), this._applyPatternGradientTransform(i, e), i.fillStyle = e.toLive(t), i.fill(), t.translate(-this.width / 2 - this.strokeWidth / 2, -this.height / 2 - this.strokeWidth / 2), t.scale(s * this.scaleX / r.zoomX, s * this.scaleY / r.zoomY), t.strokeStyle = i.createPattern(n, "no-repeat"); }, _findCenterFromElement: function () { return { x: this.left + this.width / 2, y: this.top + this.height / 2 }; }, _assignTransformMatrixProps: function () { if (this.transformMatrix) { var t = x.util.qrDecompose(this.transformMatrix); this.flipX = !1, this.flipY = !1, this.set("scaleX", t.scaleX), this.set("scaleY", t.scaleY), this.angle = t.angle, this.skewX = t.skewX, this.skewY = 0; } }, _removeTransformMatrix: function (t) { var e = this._findCenterFromElement(); this.transformMatrix && (this._assignTransformMatrixProps(), e = x.util.transformPoint(e, this.transformMatrix)), this.transformMatrix = null, t && (this.scaleX *= t.scaleX, this.scaleY *= t.scaleY, this.cropX = t.cropX, this.cropY = t.cropY, e.x += t.offsetLeft, e.y += t.offsetTop, this.width = t.width, this.height = t.height), this.setPositionByOrigin(e, "center", "center"); }, clone: function (t, e) { var i = this.toObject(e); this.constructor.fromObject ? this.constructor.fromObject(i, t) : x.Object._fromObject("Object", i, t); }, cloneAsImage: function (t, e) { var i = this.toCanvasElement(e); return t && t(new x.Image(i)), this; }, toCanvasElement: function (t) { t || (t = {}); var e = x.util, i = e.saveObjectTransform(this), r = this.group, n = this.shadow, s = Math.abs, o = (t.multiplier || 1) * (t.enableRetinaScaling ? x.devicePixelRatio : 1); delete this.group, t.withoutTransform && e.resetObjectTransform(this), t.withoutShadow && (this.shadow = null); var a, c, h, l, u = x.util.createCanvasElement(), f = this.getBoundingRect(!0, !0), d = this.shadow, g = { x: 0, y: 0 }; d && (c = d.blur, a = d.nonScaling ? { scaleX: 1, scaleY: 1 } : this.getObjectScaling(), g.x = 2 * Math.round(s(d.offsetX) + c) * s(a.scaleX), g.y = 2 * Math.round(s(d.offsetY) + c) * s(a.scaleY)), h = f.width + g.x, l = f.height + g.y, u.width = Math.ceil(h), u.height = Math.ceil(l); var p = new x.StaticCanvas(u, { enableRetinaScaling: !1, renderOnAddRemove: !1, skipOffscreen: !1 }); "jpeg" === t.format && (p.backgroundColor = "#fff"), this.setPositionByOrigin(new x.Point(p.width / 2, p.height / 2), "center", "center"); var v = this.canvas; p.add(this); var m = p.toCanvasElement(o || 1, t); return this.shadow = n, this.set("canvas", v), r && (this.group = r), this.set(i).setCoords(), p._objects = [], p.dispose(), p = null, m; }, toDataURL: function (t) { return t || (t = {}), x.util.toDataURL(this.toCanvasElement(t), t.format || "png", t.quality || 1); }, isType: function (t) { return 1 < arguments.length ? Array.from(arguments).includes(this.type) : this.type === t; }, complexity: function () { return 1; }, toJSON: function (t) { return this.toObject(t); }, rotate: function (t) { var e = ("center" !== this.originX || "center" !== this.originY) && this.centeredRotation; return e && this._setOriginToCenter(), this.set("angle", t), e && this._resetOrigin(), this; }, centerH: function () { return this.canvas && this.canvas.centerObjectH(this), this; }, viewportCenterH: function () { return this.canvas && this.canvas.viewportCenterObjectH(this), this; }, centerV: function () { return this.canvas && this.canvas.centerObjectV(this), this; }, viewportCenterV: function () { return this.canvas && this.canvas.viewportCenterObjectV(this), this; }, center: function () { return this.canvas && this.canvas.centerObject(this), this; }, viewportCenter: function () { return this.canvas && this.canvas.viewportCenterObject(this), this; }, getLocalPointer: function (t, e) { e = e || this.canvas.getPointer(t); var i = new x.Point(e.x, e.y), r = this._getLeftTopCoords(); return this.angle && (i = x.util.rotatePoint(i, r, a(-this.angle))), { x: i.x - r.x, y: i.y - r.y }; }, _setupCompositeOperation: function (t) { this.globalCompositeOperation && (t.globalCompositeOperation = this.globalCompositeOperation); }, dispose: function () { x.runningAnimations && x.runningAnimations.cancelByTarget(this); } }), x.util.createAccessors && x.util.createAccessors(x.Object), e(x.Object.prototype, x.Observable), x.Object.NUM_FRACTION_DIGITS = 2, x.Object.ENLIVEN_PROPS = ["clipPath"], x.Object._fromObject = function (t, e, i, r) { var n = x[t]; e = s(e, !0), x.util.enlivenPatterns([e.fill, e.stroke], function (t) { void 0 !== t[0] && (e.fill = t[0]), void 0 !== t[1] && (e.stroke = t[1]), x.util.enlivenObjectEnlivables(e, e, function () { var t = r ? new n(e[r], e) : new n(e); i && i(t); }); }); }, x.Object.__uid = 0); }("undefined" != typeof exports ? exports : this), function () { var a = fabric.util.degreesToRadians, l = { left: -.5, center: 0, right: .5 }, u = { top: -.5, center: 0, bottom: .5 }; fabric.util.object.extend(fabric.Object.prototype, { translateToGivenOrigin: function (t, e, i, r, n) { var s, o, a, c = t.x, h = t.y; return "string" == typeof e ? e = l[e] : e -= .5, "string" == typeof r ? r = l[r] : r -= .5, "string" == typeof i ? i = u[i] : i -= .5, "string" == typeof n ? n = u[n] : n -= .5, o = n - i, ((s = r - e) || o) && (a = this._getTransformedDimensions(), c = t.x + s * a.x, h = t.y + o * a.y), new fabric.Point(c, h); }, translateToCenterPoint: function (t, e, i) { var r = this.translateToGivenOrigin(t, e, i, "center", "center"); return this.angle ? fabric.util.rotatePoint(r, t, a(this.angle)) : r; }, translateToOriginPoint: function (t, e, i) { var r = this.translateToGivenOrigin(t, "center", "center", e, i); return this.angle ? fabric.util.rotatePoint(r, t, a(this.angle)) : r; }, getCenterPoint: function () { var t = new fabric.Point(this.left, this.top); return this.translateToCenterPoint(t, this.originX, this.originY); }, getPointByOrigin: function (t, e) { var i = this.getCenterPoint(); return this.translateToOriginPoint(i, t, e); }, toLocalPoint: function (t, e, i) { var r, n, s = this.getCenterPoint(); return r = void 0 !== e && void 0 !== i ? this.translateToGivenOrigin(s, "center", "center", e, i) : new fabric.Point(this.left, this.top), n = new fabric.Point(t.x, t.y), this.angle && (n = fabric.util.rotatePoint(n, s, -a(this.angle))), n.subtractEquals(r); }, setPositionByOrigin: function (t, e, i) { var r = this.translateToCenterPoint(t, e, i), n = this.translateToOriginPoint(r, this.originX, this.originY); this.set("left", n.x), this.set("top", n.y); }, adjustPosition: function (t) { var e, i, r = a(this.angle), n = this.getScaledWidth(), s = fabric.util.cos(r) * n, o = fabric.util.sin(r) * n; e = "string" == typeof this.originX ? l[this.originX] : this.originX - .5, i = "string" == typeof t ? l[t] : t - .5, this.left += s * (i - e), this.top += o * (i - e), this.setCoords(), this.originX = t; }, _setOriginToCenter: function () { this._originalOriginX = this.originX, this._originalOriginY = this.originY; var t = this.getCenterPoint(); this.originX = "center", this.originY = "center", this.left = t.x, this.top = t.y; }, _resetOrigin: function () { var t = this.translateToOriginPoint(this.getCenterPoint(), this._originalOriginX, this._originalOriginY); this.originX = this._originalOriginX, this.originY = this._originalOriginY, this.left = t.x, this.top = t.y, this._originalOriginX = null, this._originalOriginY = null; }, _getLeftTopCoords: function () { return this.translateToOriginPoint(this.getCenterPoint(), "left", "top"); } }); }(), function () { var h = fabric.util, l = h.degreesToRadians, a = h.multiplyTransformMatrices, u = h.transformPoint; h.object.extend(fabric.Object.prototype, { oCoords: null, aCoords: null, lineCoords: null, ownMatrixCache: null, matrixCache: null, controls: {}, _getCoords: function (t, e) { return e ? t ? this.calcACoords() : this.calcLineCoords() : (this.aCoords && this.lineCoords || this.setCoords(!0), t ? this.aCoords : this.lineCoords); }, getCoords: function (t, e) { return i = this._getCoords(t, e), [new fabric.Point(i.tl.x, i.tl.y), new fabric.Point(i.tr.x, i.tr.y), new fabric.Point(i.br.x, i.br.y), new fabric.Point(i.bl.x, i.bl.y)]; var i; }, intersectsWithRect: function (t, e, i, r) { var n = this.getCoords(i, r); return "Intersection" === fabric.Intersection.intersectPolygonRectangle(n, t, e).status; }, intersectsWithObject: function (t, e, i) { return "Intersection" === fabric.Intersection.intersectPolygonPolygon(this.getCoords(e, i), t.getCoords(e, i)).status || t.isContainedWithinObject(this, e, i) || this.isContainedWithinObject(t, e, i); }, isContainedWithinObject: function (t, e, i) { for (var r = this.getCoords(e, i), n = e ? t.aCoords : t.lineCoords, s = 0, o = t._getImageLines(n); s < 4; s++)if (!t.containsPoint(r[s], o)) return !1; return !0; }, isContainedWithinRect: function (t, e, i, r) { var n = this.getBoundingRect(i, r); return n.left >= t.x && n.left + n.width <= e.x && n.top >= t.y && n.top + n.height <= e.y; }, containsPoint: function (t, e, i, r) { var n = this._getCoords(i, r), s = (e = e || this._getImageLines(n), this._findCrossPoints(t, e)); return 0 !== s && s % 2 == 1; }, isOnScreen: function (t) { if (!this.canvas) return !1; var e = this.canvas.vptCoords.tl, i = this.canvas.vptCoords.br; return !!this.getCoords(!0, t).some(function (t) { return t.x <= i.x && t.x >= e.x && t.y <= i.y && t.y >= e.y; }) || (!!this.intersectsWithRect(e, i, !0, t) || this._containsCenterOfCanvas(e, i, t)); }, _containsCenterOfCanvas: function (t, e, i) { var r = { x: (t.x + e.x) / 2, y: (t.y + e.y) / 2 }; return !!this.containsPoint(r, null, !0, i); }, isPartiallyOnScreen: function (t) { if (!this.canvas) return !1; var e = this.canvas.vptCoords.tl, i = this.canvas.vptCoords.br; return !!this.intersectsWithRect(e, i, !0, t) || this.getCoords(!0, t).every(function (t) { return (t.x >= i.x || t.x <= e.x) && (t.y >= i.y || t.y <= e.y); }) && this._containsCenterOfCanvas(e, i, t); }, _getImageLines: function (t) { return { topline: { o: t.tl, d: t.tr }, rightline: { o: t.tr, d: t.br }, bottomline: { o: t.br, d: t.bl }, leftline: { o: t.bl, d: t.tl } }; }, _findCrossPoints: function (t, e) { var i, r, n, s = 0; for (var o in e) if (!((n = e[o]).o.y < t.y && n.d.y < t.y || n.o.y >= t.y && n.d.y >= t.y || (n.o.x === n.d.x && n.o.x >= t.x ? r = n.o.x : (0, i = (n.d.y - n.o.y) / (n.d.x - n.o.x), r = -(t.y - 0 * t.x - (n.o.y - i * n.o.x)) / (0 - i)), r >= t.x && (s += 1), 2 !== s))) break; return s; }, getBoundingRect: function (t, e) { var i = this.getCoords(t, e); return h.makeBoundingBoxFromPoints(i); }, getScaledWidth: function () { return this._getTransformedDimensions().x; }, getScaledHeight: function () { return this._getTransformedDimensions().y; }, _constrainScale: function (t) { return Math.abs(t) < this.minScaleLimit ? t < 0 ? -this.minScaleLimit : this.minScaleLimit : 0 === t ? 1e-4 : t; }, scale: function (t) { return this._set("scaleX", t), this._set("scaleY", t), this.setCoords(); }, scaleToWidth: function (t, e) { var i = this.getBoundingRect(e).width / this.getScaledWidth(); return this.scale(t / this.width / i); }, scaleToHeight: function (t, e) { var i = this.getBoundingRect(e).height / this.getScaledHeight(); return this.scale(t / this.height / i); }, calcLineCoords: function () { var t = this.getViewportTransform(), e = this.padding, i = l(this.angle), r = h.cos(i) * e, n = h.sin(i) * e, s = r + n, o = r - n, a = this.calcACoords(), c = { tl: u(a.tl, t), tr: u(a.tr, t), bl: u(a.bl, t), br: u(a.br, t) }; return e && (c.tl.x -= o, c.tl.y -= s, c.tr.x += s, c.tr.y -= o, c.bl.x -= s, c.bl.y += o, c.br.x += o, c.br.y += s), c; }, calcOCoords: function () { var t = this._calcRotateMatrix(), e = this._calcTranslateMatrix(), i = this.getViewportTransform(), r = a(i, e), n = a(r, t), s = (n = a(n, [1 / i[0], 0, 0, 1 / i[3], 0, 0]), this._calculateCurrentDimensions()), o = {}; return this.forEachControl(function (t, e, i) { o[e] = t.positionHandler(s, n, i); }), o; }, calcACoords: function () { var t = this._calcRotateMatrix(), e = this._calcTranslateMatrix(), i = a(e, t), r = this._getTransformedDimensions(), n = r.x / 2, s = r.y / 2; return { tl: u({ x: -n, y: -s }, i), tr: u({ x: n, y: -s }, i), bl: u({ x: -n, y: s }, i), br: u({ x: n, y: s }, i) }; }, setCoords: function (t) { return this.aCoords = this.calcACoords(), this.lineCoords = this.group ? this.aCoords : this.calcLineCoords(), t || (this.oCoords = this.calcOCoords(), this._setCornerCoords && this._setCornerCoords()), this; }, _calcRotateMatrix: function () { return h.calcRotateMatrix(this); }, _calcTranslateMatrix: function () { var t = this.getCenterPoint(); return [1, 0, 0, 1, t.x, t.y]; }, transformMatrixKey: function (t) { var e = "_", i = ""; return !t && this.group && (i = this.group.transformMatrixKey(t) + e), i + this.top + e + this.left + e + this.scaleX + e + this.scaleY + e + this.skewX + e + this.skewY + e + this.angle + e + this.originX + e + this.originY + e + this.width + e + this.height + e + this.strokeWidth + this.flipX + this.flipY; }, calcTransformMatrix: function (t) { var e = this.calcOwnMatrix(); if (t || !this.group) return e; var i = this.transformMatrixKey(t), r = this.matrixCache || (this.matrixCache = {}); return r.key === i ? r.value : (this.group && (e = a(this.group.calcTransformMatrix(!1), e)), r.key = i, r.value = e); }, calcOwnMatrix: function () { var t = this.transformMatrixKey(!0), e = this.ownMatrixCache || (this.ownMatrixCache = {}); if (e.key === t) return e.value; var i = this._calcTranslateMatrix(), r = { angle: this.angle, translateX: i[4], translateY: i[5], scaleX: this.scaleX, scaleY: this.scaleY, skewX: this.skewX, skewY: this.skewY, flipX: this.flipX, flipY: this.flipY }; return e.key = t, e.value = h.composeMatrix(r), e.value; }, _getNonTransformedDimensions: function () { var t = this.strokeWidth; return { x: this.width + t, y: this.height + t }; }, _getTransformedDimensions: function (t, e) { void 0 === t && (t = this.skewX), void 0 === e && (e = this.skewY); var i, r, n, s = 0 === t && 0 === e; if (this.strokeUniform ? (r = this.width, n = this.height) : (r = (i = this._getNonTransformedDimensions()).x, n = i.y), s) return this._finalizeDimensions(r * this.scaleX, n * this.scaleY); var o = h.sizeAfterTransform(r, n, { scaleX: this.scaleX, scaleY: this.scaleY, skewX: t, skewY: e }); return this._finalizeDimensions(o.x, o.y); }, _finalizeDimensions: function (t, e) { return this.strokeUniform ? { x: t + this.strokeWidth, y: e + this.strokeWidth } : { x: t, y: e }; }, _calculateCurrentDimensions: function () { var t = this.getViewportTransform(), e = this._getTransformedDimensions(); return u(e, t, !0).scalarAdd(2 * this.padding); } }); }(), fabric.util.object.extend(fabric.Object.prototype, { sendToBack: function () { return this.group ? fabric.StaticCanvas.prototype.sendToBack.call(this.group, this) : this.canvas && this.canvas.sendToBack(this), this; }, bringToFront: function () { return this.group ? fabric.StaticCanvas.prototype.bringToFront.call(this.group, this) : this.canvas && this.canvas.bringToFront(this), this; }, sendBackwards: function (t) { return this.group ? fabric.StaticCanvas.prototype.sendBackwards.call(this.group, this, t) : this.canvas && this.canvas.sendBackwards(this, t), this; }, bringForward: function (t) { return this.group ? fabric.StaticCanvas.prototype.bringForward.call(this.group, this, t) : this.canvas && this.canvas.bringForward(this, t), this; }, moveTo: function (t) { return this.group && "activeSelection" !== this.group.type ? fabric.StaticCanvas.prototype.moveTo.call(this.group, this, t) : this.canvas && this.canvas.moveTo(this, t), this; } }), function () { function f(t, e) { if (e) { if (e.toLive) return t + ": url(#SVGID_" + e.id + "); "; var i = new fabric.Color(e), r = t + ": " + i.toRgb() + "; ", n = i.getAlpha(); return 1 !== n && (r += t + "-opacity: " + n.toString() + "; "), r; } return t + ": none; "; } var i = fabric.util.toFixed; fabric.util.object.extend(fabric.Object.prototype, { getSvgStyles: function (t) { var e = this.fillRule ? this.fillRule : "nonzero", i = this.strokeWidth ? this.strokeWidth : "0", r = this.strokeDashArray ? this.strokeDashArray.join(" ") : "none", n = this.strokeDashOffset ? this.strokeDashOffset : "0", s = this.strokeLineCap ? this.strokeLineCap : "butt", o = this.strokeLineJoin ? this.strokeLineJoin : "miter", a = this.strokeMiterLimit ? this.strokeMiterLimit : "4", c = void 0 !== this.opacity ? this.opacity : "1", h = this.visible ? "" : " visibility: hidden;", l = t ? "" : this.getSvgFilter(), u = f("fill", this.fill); return [f("stroke", this.stroke), "stroke-width: ", i, "; ", "stroke-dasharray: ", r, "; ", "stroke-linecap: ", s, "; ", "stroke-dashoffset: ", n, "; ", "stroke-linejoin: ", o, "; ", "stroke-miterlimit: ", a, "; ", u, "fill-rule: ", e, "; ", "opacity: ", c, ";", l, h].join(""); }, getSvgSpanStyles: function (t, e) { var i = "; ", r = t.fontFamily ? "font-family: " + (-1 === t.fontFamily.indexOf("'") && -1 === t.fontFamily.indexOf('"') ? "'" + t.fontFamily + "'" : t.fontFamily) + i : "", n = t.strokeWidth ? "stroke-width: " + t.strokeWidth + i : "", s = (r = r, t.fontSize ? "font-size: " + t.fontSize + "px" + i : ""), o = t.fontStyle ? "font-style: " + t.fontStyle + i : "", a = t.fontWeight ? "font-weight: " + t.fontWeight + i : "", c = t.fill ? f("fill", t.fill) : "", h = t.stroke ? f("stroke", t.stroke) : "", l = this.getSvgTextDecoration(t); return l && (l = "text-decoration: " + l + i), [h, n, r, s, o, a, l, c, t.deltaY ? "baseline-shift: " + -t.deltaY + "; " : "", e ? "white-space: pre; " : ""].join(""); }, getSvgTextDecoration: function (e) { return ["overline", "underline", "line-through"].filter(function (t) { return e[t.replace("-", "")]; }).join(" "); }, getSvgFilter: function () { return this.shadow ? "filter: url(#SVGID_" + this.shadow.id + ");" : ""; }, getSvgCommons: function () { return [this.id ? 'id="' + this.id + '" ' : "", this.clipPath ? 'clip-path="url(#' + this.clipPath.clipPathId + ')" ' : ""].join(""); }, getSvgTransform: function (t, e) { var i = t ? this.calcTransformMatrix() : this.calcOwnMatrix(); return 'transform="' + fabric.util.matrixToSVG(i) + (e || "") + '" '; }, _setSVGBg: function (t) { if (this.backgroundColor) { var e = fabric.Object.NUM_FRACTION_DIGITS; t.push("\t\t<rect ", this._getFillAttributes(this.backgroundColor), ' x="', i(-this.width / 2, e), '" y="', i(-this.height / 2, e), '" width="', i(this.width, e), '" height="', i(this.height, e), '"></rect>\n'); } }, toSVG: function (t) { return this._createBaseSVGMarkup(this._toSVG(t), { reviver: t }); }, toClipPathSVG: function (t) { return "\t" + this._createBaseClipPathSVGMarkup(this._toSVG(t), { reviver: t }); }, _createBaseClipPathSVGMarkup: function (t, e) { var i = (e = e || {}).reviver, r = e.additionalTransform || "", n = [this.getSvgTransform(!0, r), this.getSvgCommons()].join(""), s = t.indexOf("COMMON_PARTS"); return t[s] = n, i ? i(t.join("")) : t.join(""); }, _createBaseSVGMarkup: function (t, e) { var i, r, n = (e = e || {}).noStyle, s = e.reviver, o = n ? "" : 'style="' + this.getSvgStyles() + '" ', a = e.withShadow ? 'style="' + this.getSvgFilter() + '" ' : "", c = this.clipPath, h = this.strokeUniform ? 'vector-effect="non-scaling-stroke" ' : "", l = c && c.absolutePositioned, u = this.stroke, f = this.fill, d = this.shadow, g = [], p = t.indexOf("COMMON_PARTS"), v = e.additionalTransform; return c && (c.clipPathId = "CLIPPATH_" + fabric.Object.__uid++, r = '<clipPath id="' + c.clipPathId + '" >\n' + c.toClipPathSVG(s) + "</clipPath>\n"), l && g.push("<g ", a, this.getSvgCommons(), " >\n"), g.push("<g ", this.getSvgTransform(!1), l ? "" : a + this.getSvgCommons(), " >\n"), i = [o, h, n ? "" : this.addPaintOrder(), " ", v ? 'transform="' + v + '" ' : ""].join(""), t[p] = i, f && f.toLive && g.push(f.toSVG(this)), u && u.toLive && g.push(u.toSVG(this)), d && g.push(d.toSVG(this)), c && g.push(r), g.push(t.join("")), g.push("</g>\n"), l && g.push("</g>\n"), s ? s(g.join("")) : g.join(""); }, addPaintOrder: function () { return "fill" !== this.paintFirst ? ' paint-order="' + this.paintFirst + '" ' : ""; } }); }(), function () { var n = fabric.util.object.extend, r = "stateProperties"; function s(e, t, i) { var r = {}; i.forEach(function (t) { r[t] = e[t]; }), n(e[t], r, !0); } fabric.util.object.extend(fabric.Object.prototype, { hasStateChanged: function (t) { var e = "_" + (t = t || r); return Object.keys(this[e]).length < this[t].length || !function t(e, i, r) { if (e === i) return !0; if (Array.isArray(e)) { if (!Array.isArray(i) || e.length !== i.length) return !1; for (var n = 0, s = e.length; n < s; n++)if (!t(e[n], i[n])) return !1; return !0; } if (e && "object" == typeof e) { var o, a = Object.keys(e); if (!i || "object" != typeof i || !r && a.length !== Object.keys(i).length) return !1; for (n = 0, s = a.length; n < s; n++)if ("canvas" !== (o = a[n]) && "group" !== o && !t(e[o], i[o])) return !1; return !0; } }(this[e], this, !0); }, saveState: function (t) { var e = t && t.propertySet || r, i = "_" + e; return this[i] ? (s(this, i, this[e]), t && t.stateProperties && s(this, i, t.stateProperties), this) : this.setupState(t); }, setupState: function (t) { var e = (t = t || {}).propertySet || r; return this["_" + (t.propertySet = e)] = {}, this.saveState(t), this; } }); }(), function () { var n = fabric.util.degreesToRadians; fabric.util.object.extend(fabric.Object.prototype, { _findTargetCorner: function (t, e) { if (!this.hasControls || this.group || !this.canvas || this.canvas._activeObject !== this) return !1; var i, r, n, s = t.x, o = t.y, a = Object.keys(this.oCoords), c = a.length - 1; for (this.__corner = 0; 0 <= c; c--)if (n = a[c], this.isControlVisible(n) && (r = this._getImageLines(e ? this.oCoords[n].touchCorner : this.oCoords[n].corner), 0 !== (i = this._findCrossPoints({ x: s, y: o }, r)) && i % 2 == 1)) return this.__corner = n; return !1; }, forEachControl: function (t) { for (var e in this.controls) t(this.controls[e], e, this); }, _setCornerCoords: function () { var t = this.oCoords; for (var e in t) { var i = this.controls[e]; t[e].corner = i.calcCornerCoords(this.angle, this.cornerSize, t[e].x, t[e].y, !1), t[e].touchCorner = i.calcCornerCoords(this.angle, this.touchCornerSize, t[e].x, t[e].y, !0); } }, drawSelectionBackground: function (t) { if (!this.selectionBackgroundColor || this.canvas && !this.canvas.interactive || this.canvas && this.canvas._activeObject !== this) return this; t.save(); var e = this.getCenterPoint(), i = this._calculateCurrentDimensions(), r = this.canvas.viewportTransform; return t.translate(e.x, e.y), t.scale(1 / r[0], 1 / r[3]), t.rotate(n(this.angle)), t.fillStyle = this.selectionBackgroundColor, t.fillRect(-i.x / 2, -i.y / 2, i.x, i.y), t.restore(), this; }, drawBorders: function (r, t) { t = t || {}; var e = this._calculateCurrentDimensions(), i = this.borderScaleFactor, n = e.x + i, s = e.y + i, o = void 0 !== t.hasControls ? t.hasControls : this.hasControls, a = !1; return r.save(), r.strokeStyle = t.borderColor || this.borderColor, this._setLineDash(r, t.borderDashArray || this.borderDashArray), r.strokeRect(-n / 2, -s / 2, n, s), o && (r.beginPath(), this.forEachControl(function (t, e, i) { t.withConnection && t.getVisibility(i, e) && (a = !0, r.moveTo(t.x * n, t.y * s), r.lineTo(t.x * n + t.offsetX, t.y * s + t.offsetY)); }), a && r.stroke()), r.restore(), this; }, drawBordersInGroup: function (t, e, i) { i = i || {}; var r = fabric.util.sizeAfterTransform(this.width, this.height, e), n = this.strokeWidth, s = this.strokeUniform, o = this.borderScaleFactor, a = r.x + n * (s ? this.canvas.getZoom() : e.scaleX) + o, c = r.y + n * (s ? this.canvas.getZoom() : e.scaleY) + o; return t.save(), this._setLineDash(t, i.borderDashArray || this.borderDashArray), t.strokeStyle = i.borderColor || this.borderColor, t.strokeRect(-a / 2, -c / 2, a, c), t.restore(), this; }, drawControls: function (r, n) { n = n || {}, r.save(); var s, o, t = this.canvas.getRetinaScaling(); return r.setTransform(t, 0, 0, t, 0, 0), r.strokeStyle = r.fillStyle = n.cornerColor || this.cornerColor, this.transparentCorners || (r.strokeStyle = n.cornerStrokeColor || this.cornerStrokeColor), this._setLineDash(r, n.cornerDashArray || this.cornerDashArray), this.setCoords(), this.group && (s = this.group.calcTransformMatrix()), this.forEachControl(function (t, e, i) { o = i.oCoords[e], t.getVisibility(i, e) && (s && (o = fabric.util.transformPoint(o, s)), t.render(r, o.x, o.y, n, i)); }), r.restore(), this; }, isControlVisible: function (t) { return this.controls[t] && this.controls[t].getVisibility(this, t); }, setControlVisible: function (t, e) { return this._controlsVisibility || (this._controlsVisibility = {}), this._controlsVisibility[t] = e, this; }, setControlsVisibility: function (t) { for (var e in t || (t = {}), t) this.setControlVisible(e, t[e]); return this; }, onDeselect: function () { }, onSelect: function () { } }); }(), fabric.util.object.extend(fabric.StaticCanvas.prototype, { FX_DURATION: 500, fxCenterObjectH: function (e, t) { var i = function () { }, r = (t = t || {}).onComplete || i, n = t.onChange || i, s = this; return fabric.util.animate({ target: this, startValue: e.left, endValue: this.getCenterPoint().x, duration: this.FX_DURATION, onChange: function (t) { e.set("left", t), s.requestRenderAll(), n(); }, onComplete: function () { e.setCoords(), r(); } }); }, fxCenterObjectV: function (e, t) { var i = function () { }, r = (t = t || {}).onComplete || i, n = t.onChange || i, s = this; return fabric.util.animate({ target: this, startValue: e.top, endValue: this.getCenterPoint().y, duration: this.FX_DURATION, onChange: function (t) { e.set("top", t), s.requestRenderAll(), n(); }, onComplete: function () { e.setCoords(), r(); } }); }, fxRemove: function (e, t) { var i = function () { }, r = (t = t || {}).onComplete || i, n = t.onChange || i, s = this; return fabric.util.animate({ target: this, startValue: e.opacity, endValue: 0, duration: this.FX_DURATION, onChange: function (t) { e.set("opacity", t), s.requestRenderAll(), n(); }, onComplete: function () { s.remove(e), r(); } }); } }), fabric.util.object.extend(fabric.Object.prototype, { animate: function () { if (arguments[0] && "object" == typeof arguments[0]) { var t, e, i = [], r = []; for (t in arguments[0]) i.push(t); for (var n = 0, s = i.length; n < s; n++)t = i[n], e = n !== s - 1, r.push(this._animate(t, arguments[0][t], arguments[1], e)); return r; } return this._animate.apply(this, arguments); }, _animate: function (r, t, n, s) { var o, a = this; t = t.toString(), n = n ? fabric.util.object.clone(n) : {}, ~r.indexOf(".") && (o = r.split(".")); var e = -1 < a.colorProperties.indexOf(r) || o && -1 < a.colorProperties.indexOf(o[1]), i = o ? this.get(o[0])[o[1]] : this.get(r); "from" in n || (n.from = i), e || (t = ~t.indexOf("=") ? i + parseFloat(t.replace("=", "")) : parseFloat(t)); var c = { target: this, startValue: n.from, endValue: t, byValue: n.by, easing: n.easing, duration: n.duration, abort: n.abort && function (t, e, i) { return n.abort.call(a, t, e, i); }, onChange: function (t, e, i) { o ? a[o[0]][o[1]] = t : a.set(r, t), s || n.onChange && n.onChange(t, e, i); }, onComplete: function (t, e, i) { s || (a.setCoords(), n.onComplete && n.onComplete(t, e, i)); } }; return e ? fabric.util.animateColor(c.startValue, c.endValue, c.duration, c) : fabric.util.animate(c); } }), function (t) { "use strict"; var s = t.fabric || (t.fabric = {}), o = s.util.object.extend, r = s.util.object.clone, i = { x1: 1, x2: 1, y1: 1, y2: 1 }; function e(t, e) { var i = t.origin, r = t.axis1, n = t.axis2, s = t.dimension, o = e.nearest, a = e.center, c = e.farthest; return function () { switch (this.get(i)) { case o: return Math.min(this.get(r), this.get(n)); case a: return Math.min(this.get(r), this.get(n)) + .5 * this.get(s); case c: return Math.max(this.get(r), this.get(n)); } }; } s.Line ? s.warn("fabric.Line is already defined") : (s.Line = s.util.createClass(s.Object, { type: "line", x1: 0, y1: 0, x2: 0, y2: 0, cacheProperties: s.Object.prototype.cacheProperties.concat("x1", "x2", "y1", "y2"), initialize: function (t, e) { t || (t = [0, 0, 0, 0]), this.callSuper("initialize", e), this.set("x1", t[0]), this.set("y1", t[1]), this.set("x2", t[2]), this.set("y2", t[3]), this._setWidthHeight(e); }, _setWidthHeight: function (t) { t || (t = {}), this.width = Math.abs(this.x2 - this.x1), this.height = Math.abs(this.y2 - this.y1), this.left = "left" in t ? t.left : this._getLeftToOriginX(), this.top = "top" in t ? t.top : this._getTopToOriginY(); }, _set: function (t, e) { return this.callSuper("_set", t, e), void 0 !== i[t] && this._setWidthHeight(), this; }, _getLeftToOriginX: e({ origin: "originX", axis1: "x1", axis2: "x2", dimension: "width" }, { nearest: "left", center: "center", farthest: "right" }), _getTopToOriginY: e({ origin: "originY", axis1: "y1", axis2: "y2", dimension: "height" }, { nearest: "top", center: "center", farthest: "bottom" }), _render: function (t) { t.beginPath(); var e = this.calcLinePoints(); t.moveTo(e.x1, e.y1), t.lineTo(e.x2, e.y2), t.lineWidth = this.strokeWidth; var i = t.strokeStyle; t.strokeStyle = this.stroke || t.fillStyle, this.stroke && this._renderStroke(t), t.strokeStyle = i; }, _findCenterFromElement: function () { return { x: (this.x1 + this.x2) / 2, y: (this.y1 + this.y2) / 2 }; }, toObject: function (t) { return o(this.callSuper("toObject", t), this.calcLinePoints()); }, _getNonTransformedDimensions: function () { var t = this.callSuper("_getNonTransformedDimensions"); return "butt" === this.strokeLineCap && (0 === this.width && (t.y -= this.strokeWidth), 0 === this.height && (t.x -= this.strokeWidth)), t; }, calcLinePoints: function () { var t = this.x1 <= this.x2 ? -1 : 1, e = this.y1 <= this.y2 ? -1 : 1, i = t * this.width * .5, r = e * this.height * .5; return { x1: i, x2: t * this.width * -.5, y1: r, y2: e * this.height * -.5 }; }, _toSVG: function () { var t = this.calcLinePoints(); return ["<line ", "COMMON_PARTS", 'x1="', t.x1, '" y1="', t.y1, '" x2="', t.x2, '" y2="', t.y2, '" />\n']; } }), s.Line.ATTRIBUTE_NAMES = s.SHARED_ATTRIBUTES.concat("x1 y1 x2 y2".split(" ")), s.Line.fromElement = function (t, e, i) { i = i || {}; var r = s.parseAttributes(t, s.Line.ATTRIBUTE_NAMES), n = [r.x1 || 0, r.y1 || 0, r.x2 || 0, r.y2 || 0]; e(new s.Line(n, o(r, i))); }, s.Line.fromObject = function (t, e) { var i = r(t, !0); i.points = [t.x1, t.y1, t.x2, t.y2], s.Object._fromObject("Line", i, function (t) { delete t.points, e && e(t); }, "points"); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var s = t.fabric || (t.fabric = {}), o = s.util.degreesToRadians; s.Circle ? s.warn("fabric.Circle is already defined.") : (s.Circle = s.util.createClass(s.Object, { type: "circle", radius: 0, startAngle: 0, endAngle: 360, cacheProperties: s.Object.prototype.cacheProperties.concat("radius", "startAngle", "endAngle"), _set: function (t, e) { return this.callSuper("_set", t, e), "radius" === t && this.setRadius(e), this; }, toObject: function (t) { return this.callSuper("toObject", ["radius", "startAngle", "endAngle"].concat(t)); }, _toSVG: function () { var t, e = (this.endAngle - this.startAngle) % 360; if (0 === e) t = ["<circle ", "COMMON_PARTS", 'cx="0" cy="0" ', 'r="', this.radius, '" />\n']; else { var i = o(this.startAngle), r = o(this.endAngle), n = this.radius; t = ['<path d="M ' + s.util.cos(i) * n + " " + s.util.sin(i) * n, " A " + n + " " + n, " 0 ", +(180 < e ? "1" : "0") + " 1", " " + s.util.cos(r) * n + " " + s.util.sin(r) * n, '" ', "COMMON_PARTS", " />\n"]; } return t; }, _render: function (t) { t.beginPath(), t.arc(0, 0, this.radius, o(this.startAngle), o(this.endAngle), !1), this._renderPaintInOrder(t); }, getRadiusX: function () { return this.get("radius") * this.get("scaleX"); }, getRadiusY: function () { return this.get("radius") * this.get("scaleY"); }, setRadius: function (t) { return this.radius = t, this.set("width", 2 * t).set("height", 2 * t); } }), s.Circle.ATTRIBUTE_NAMES = s.SHARED_ATTRIBUTES.concat("cx cy r".split(" ")), s.Circle.fromElement = function (t, e) { var i, r = s.parseAttributes(t, s.Circle.ATTRIBUTE_NAMES); if (!("radius" in (i = r) && 0 <= i.radius)) throw new Error("value of `r` attribute is required and can not be negative"); r.left = (r.left || 0) - r.radius, r.top = (r.top || 0) - r.radius, e(new s.Circle(r)); }, s.Circle.fromObject = function (t, e) { s.Object._fromObject("Circle", t, e); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var i = t.fabric || (t.fabric = {}); i.Triangle ? i.warn("fabric.Triangle is already defined") : (i.Triangle = i.util.createClass(i.Object, { type: "triangle", width: 100, height: 100, _render: function (t) { var e = this.width / 2, i = this.height / 2; t.beginPath(), t.moveTo(-e, i), t.lineTo(0, -i), t.lineTo(e, i), t.closePath(), this._renderPaintInOrder(t); }, _toSVG: function () { var t = this.width / 2, e = this.height / 2; return ["<polygon ", "COMMON_PARTS", 'points="', [-t + " " + e, "0 " + -e, t + " " + e].join(","), '" />']; } }), i.Triangle.fromObject = function (t, e) { return i.Object._fromObject("Triangle", t, e); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var r = t.fabric || (t.fabric = {}), e = 2 * Math.PI; r.Ellipse ? r.warn("fabric.Ellipse is already defined.") : (r.Ellipse = r.util.createClass(r.Object, { type: "ellipse", rx: 0, ry: 0, cacheProperties: r.Object.prototype.cacheProperties.concat("rx", "ry"), initialize: function (t) { this.callSuper("initialize", t), this.set("rx", t && t.rx || 0), this.set("ry", t && t.ry || 0); }, _set: function (t, e) { switch (this.callSuper("_set", t, e), t) { case "rx": this.rx = e, this.set("width", 2 * e); break; case "ry": this.ry = e, this.set("height", 2 * e); }return this; }, getRx: function () { return this.get("rx") * this.get("scaleX"); }, getRy: function () { return this.get("ry") * this.get("scaleY"); }, toObject: function (t) { return this.callSuper("toObject", ["rx", "ry"].concat(t)); }, _toSVG: function () { return ["<ellipse ", "COMMON_PARTS", 'cx="0" cy="0" ', 'rx="', this.rx, '" ry="', this.ry, '" />\n']; }, _render: function (t) { t.beginPath(), t.save(), t.transform(1, 0, 0, this.ry / this.rx, 0, 0), t.arc(0, 0, this.rx, 0, e, !1), t.restore(), this._renderPaintInOrder(t); } }), r.Ellipse.ATTRIBUTE_NAMES = r.SHARED_ATTRIBUTES.concat("cx cy rx ry".split(" ")), r.Ellipse.fromElement = function (t, e) { var i = r.parseAttributes(t, r.Ellipse.ATTRIBUTE_NAMES); i.left = (i.left || 0) - i.rx, i.top = (i.top || 0) - i.ry, e(new r.Ellipse(i)); }, r.Ellipse.fromObject = function (t, e) { r.Object._fromObject("Ellipse", t, e); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var s = t.fabric || (t.fabric = {}), o = s.util.object.extend; s.Rect ? s.warn("fabric.Rect is already defined") : (s.Rect = s.util.createClass(s.Object, { stateProperties: s.Object.prototype.stateProperties.concat("rx", "ry"), type: "rect", rx: 0, ry: 0, cacheProperties: s.Object.prototype.cacheProperties.concat("rx", "ry"), initialize: function (t) { this.callSuper("initialize", t), this._initRxRy(); }, _initRxRy: function () { this.rx && !this.ry ? this.ry = this.rx : this.ry && !this.rx && (this.rx = this.ry); }, _render: function (t) { var e = this.rx ? Math.min(this.rx, this.width / 2) : 0, i = this.ry ? Math.min(this.ry, this.height / 2) : 0, r = this.width, n = this.height, s = -this.width / 2, o = -this.height / 2, a = 0 !== e || 0 !== i, c = .4477152502; t.beginPath(), t.moveTo(s + e, o), t.lineTo(s + r - e, o), a && t.bezierCurveTo(s + r - c * e, o, s + r, o + c * i, s + r, o + i), t.lineTo(s + r, o + n - i), a && t.bezierCurveTo(s + r, o + n - c * i, s + r - c * e, o + n, s + r - e, o + n), t.lineTo(s + e, o + n), a && t.bezierCurveTo(s + c * e, o + n, s, o + n - c * i, s, o + n - i), t.lineTo(s, o + i), a && t.bezierCurveTo(s, o + c * i, s + c * e, o, s + e, o), t.closePath(), this._renderPaintInOrder(t); }, toObject: function (t) { return this.callSuper("toObject", ["rx", "ry"].concat(t)); }, _toSVG: function () { return ["<rect ", "COMMON_PARTS", 'x="', -this.width / 2, '" y="', -this.height / 2, '" rx="', this.rx, '" ry="', this.ry, '" width="', this.width, '" height="', this.height, '" />\n']; } }), s.Rect.ATTRIBUTE_NAMES = s.SHARED_ATTRIBUTES.concat("x y rx ry width height".split(" ")), s.Rect.fromElement = function (t, e, i) { if (!t) return e(null); i = i || {}; var r = s.parseAttributes(t, s.Rect.ATTRIBUTE_NAMES); r.left = r.left || 0, r.top = r.top || 0, r.height = r.height || 0, r.width = r.width || 0; var n = new s.Rect(o(i ? s.util.object.clone(i) : {}, r)); n.visible = n.visible && 0 < n.width && 0 < n.height, e(n); }, s.Rect.fromObject = function (t, e) { return s.Object._fromObject("Rect", t, e); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var o = t.fabric || (t.fabric = {}), a = o.util.object.extend, r = o.util.array.min, n = o.util.array.max, c = o.util.toFixed, e = o.util.projectStrokeOnPoints; o.Polyline ? o.warn("fabric.Polyline is already defined") : (o.Polyline = o.util.createClass(o.Object, { type: "polyline", points: null, exactBoundingBox: !1, cacheProperties: o.Object.prototype.cacheProperties.concat("points"), initialize: function (t, e) { e = e || {}, this.points = t || [], this.callSuper("initialize", e), this._setPositionDimensions(e); }, _projectStrokeOnPoints: function () { return e(this.points, this, !0); }, _setPositionDimensions: function (t) { var e, i = this._calcDimensions(t), r = this.exactBoundingBox ? this.strokeWidth : 0; this.width = i.width - r, this.height = i.height - r, t.fromSVG || (e = this.translateToGivenOrigin({ x: i.left - this.strokeWidth / 2 + r / 2, y: i.top - this.strokeWidth / 2 + r / 2 }, "left", "top", this.originX, this.originY)), void 0 === t.left && (this.left = t.fromSVG ? i.left : e.x), void 0 === t.top && (this.top = t.fromSVG ? i.top : e.y), this.pathOffset = { x: i.left + this.width / 2 + r / 2, y: i.top + this.height / 2 + r / 2 }; }, _calcDimensions: function () { var t = this.exactBoundingBox ? this._projectStrokeOnPoints() : this.points, e = r(t, "x") || 0, i = r(t, "y") || 0; return { left: e, top: i, width: (n(t, "x") || 0) - e, height: (n(t, "y") || 0) - i }; }, toObject: function (t) { return a(this.callSuper("toObject", t), { points: this.points.concat() }); }, _toSVG: function () { for (var t = [], e = this.pathOffset.x, i = this.pathOffset.y, r = o.Object.NUM_FRACTION_DIGITS, n = 0, s = this.points.length; n < s; n++)t.push(c(this.points[n].x - e, r), ",", c(this.points[n].y - i, r), " "); return ["<" + this.type + " ", "COMMON_PARTS", 'points="', t.join(""), '" />\n']; }, commonRender: function (t) { var e, i = this.points.length, r = this.pathOffset.x, n = this.pathOffset.y; if (!i || isNaN(this.points[i - 1].y)) return !1; t.beginPath(), t.moveTo(this.points[0].x - r, this.points[0].y - n); for (var s = 0; s < i; s++)e = this.points[s], t.lineTo(e.x - r, e.y - n); return !0; }, _render: function (t) { this.commonRender(t) && this._renderPaintInOrder(t); }, complexity: function () { return this.get("points").length; } }), o.Polyline.ATTRIBUTE_NAMES = o.SHARED_ATTRIBUTES.concat(), o.Polyline.fromElementGenerator = function (s) { return function (t, e, i) { if (!t) return e(null); i || (i = {}); var r = o.parsePointsAttribute(t.getAttribute("points")), n = o.parseAttributes(t, o[s].ATTRIBUTE_NAMES); n.fromSVG = !0, e(new o[s](r, a(n, i))); }; }, o.Polyline.fromElement = o.Polyline.fromElementGenerator("Polyline"), o.Polyline.fromObject = function (t, e) { return o.Object._fromObject("Polyline", t, e, "points"); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var i = t.fabric || (t.fabric = {}), e = i.util.projectStrokeOnPoints; i.Polygon ? i.warn("fabric.Polygon is already defined") : (i.Polygon = i.util.createClass(i.Polyline, { type: "polygon", _projectStrokeOnPoints: function () { return e(this.points, this); }, _render: function (t) { this.commonRender(t) && (t.closePath(), this._renderPaintInOrder(t)); } }), i.Polygon.ATTRIBUTE_NAMES = i.SHARED_ATTRIBUTES.concat(), i.Polygon.fromElement = i.Polyline.fromElementGenerator("Polygon"), i.Polygon.fromObject = function (t, e) { i.Object._fromObject("Polygon", t, e, "points"); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var f = t.fabric || (t.fabric = {}), d = f.util.array.min, g = f.util.array.max, n = f.util.object.extend, i = f.util.object.clone, e = f.util.toFixed; f.Path ? f.warn("fabric.Path is already defined") : (f.Path = f.util.createClass(f.Object, { type: "path", path: null, cacheProperties: f.Object.prototype.cacheProperties.concat("path", "fillRule"), stateProperties: f.Object.prototype.stateProperties.concat("path"), initialize: function (t, e) { delete (e = i(e || {})).path, this.callSuper("initialize", e), this._setPath(t || [], e); }, _setPath: function (t, e) { this.path = f.util.makePathSimpler(Array.isArray(t) ? t : f.util.parsePath(t)), f.Polyline.prototype._setPositionDimensions.call(this, e || {}); }, _renderPathCommands: function (t) { var e, i = 0, r = 0, n = 0, s = 0, o = 0, a = 0, c = -this.pathOffset.x, h = -this.pathOffset.y; t.beginPath(); for (var l = 0, u = this.path.length; l < u; ++l)switch ((e = this.path[l])[0]) { case "L": n = e[1], s = e[2], t.lineTo(n + c, s + h); break; case "M": i = n = e[1], r = s = e[2], t.moveTo(n + c, s + h); break; case "C": n = e[5], s = e[6], o = e[3], a = e[4], t.bezierCurveTo(e[1] + c, e[2] + h, o + c, a + h, n + c, s + h); break; case "Q": t.quadraticCurveTo(e[1] + c, e[2] + h, e[3] + c, e[4] + h), n = e[3], s = e[4], o = e[1], a = e[2]; break; case "z": case "Z": n = i, s = r, t.closePath(); } }, _render: function (t) { this._renderPathCommands(t), this._renderPaintInOrder(t); }, toString: function () { return "#<fabric.Path (" + this.complexity() + '): { "top": ' + this.top + ', "left": ' + this.left + " }>"; }, toObject: function (t) { return n(this.callSuper("toObject", t), { path: this.path.map(function (t) { return t.slice(); }) }); }, toDatalessObject: function (t) { var e = this.toObject(["sourcePath"].concat(t)); return e.sourcePath && delete e.path, e; }, _toSVG: function () { return ["<path ", "COMMON_PARTS", 'd="', f.util.joinPath(this.path), '" stroke-linecap="round" ', "/>\n"]; }, _getOffsetTransform: function () { var t = f.Object.NUM_FRACTION_DIGITS; return " translate(" + e(-this.pathOffset.x, t) + ", " + e(-this.pathOffset.y, t) + ")"; }, toClipPathSVG: function (t) { var e = this._getOffsetTransform(); return "\t" + this._createBaseClipPathSVGMarkup(this._toSVG(), { reviver: t, additionalTransform: e }); }, toSVG: function (t) { var e = this._getOffsetTransform(); return this._createBaseSVGMarkup(this._toSVG(), { reviver: t, additionalTransform: e }); }, complexity: function () { return this.path.length; }, _calcDimensions: function () { for (var t, e, i = [], r = [], n = 0, s = 0, o = 0, a = 0, c = 0, h = this.path.length; c < h; ++c) { switch ((t = this.path[c])[0]) { case "L": o = t[1], a = t[2], e = []; break; case "M": n = o = t[1], s = a = t[2], e = []; break; case "C": e = f.util.getBoundsOfCurve(o, a, t[1], t[2], t[3], t[4], t[5], t[6]), o = t[5], a = t[6]; break; case "Q": e = f.util.getBoundsOfCurve(o, a, t[1], t[2], t[1], t[2], t[3], t[4]), o = t[3], a = t[4]; break; case "z": case "Z": o = n, a = s; }e.forEach(function (t) { i.push(t.x), r.push(t.y); }), i.push(o), r.push(a); } var l = d(i) || 0, u = d(r) || 0; return { left: l, top: u, width: (g(i) || 0) - l, height: (g(r) || 0) - u }; } }), f.Path.fromObject = function (i, r) { if ("string" == typeof i.sourcePath) { var t = i.sourcePath; f.loadSVGFromURL(t, function (t) { var e = t[0]; e.setOptions(i), i.clipPath ? f.util.enlivenObjects([i.clipPath], function (t) { e.clipPath = t[0], r && r(e); }) : r && r(e); }); } else f.Object._fromObject("Path", i, r, "path"); }, f.Path.ATTRIBUTE_NAMES = f.SHARED_ATTRIBUTES.concat(["d"]), f.Path.fromElement = function (t, e, i) { var r = f.parseAttributes(t, f.Path.ATTRIBUTE_NAMES); r.fromSVG = !0, e(new f.Path(r.d, n(r, i))); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var h = t.fabric || (t.fabric = {}), l = h.util.array.min, u = h.util.array.max; h.Group || (h.Group = h.util.createClass(h.Object, h.Collection, { type: "group", strokeWidth: 0, subTargetCheck: !1, cacheProperties: [], useSetOnGroup: !1, initialize: function (t, e, i) { e = e || {}, this._objects = [], i && this.callSuper("initialize", e), this._objects = t || []; for (var r = this._objects.length; r--;)this._objects[r].group = this; if (i) this._updateObjectsACoords(); else { var n = e && e.centerPoint; void 0 !== e.originX && (this.originX = e.originX), void 0 !== e.originY && (this.originY = e.originY), n || this._calcBounds(), this._updateObjectsCoords(n), delete e.centerPoint, this.callSuper("initialize", e); } this.setCoords(); }, _updateObjectsACoords: function () { for (var t = this._objects.length; t--;)this._objects[t].setCoords(!0); }, _updateObjectsCoords: function (t) { t = t || this.getCenterPoint(); for (var e = this._objects.length; e--;)this._updateObjectCoords(this._objects[e], t); }, _updateObjectCoords: function (t, e) { var i = t.left, r = t.top; t.set({ left: i - e.x, top: r - e.y }), t.group = this, t.setCoords(!0); }, toString: function () { return "#<fabric.Group: (" + this.complexity() + ")>"; }, addWithUpdate: function (t) { var e = !!this.group; return this._restoreObjectsState(), h.util.resetObjectTransform(this), t && (e && h.util.removeTransformFromObject(t, this.group.calcTransformMatrix()), this._objects.push(t), t.group = this, t._set("canvas", this.canvas)), this._calcBounds(), this._updateObjectsCoords(), this.dirty = !0, e ? this.group.addWithUpdate() : this.setCoords(), this; }, removeWithUpdate: function (t) { return this._restoreObjectsState(), h.util.resetObjectTransform(this), this.remove(t), this._calcBounds(), this._updateObjectsCoords(), this.setCoords(), this.dirty = !0, this; }, _onObjectAdded: function (t) { this.dirty = !0, t.group = this, t._set("canvas", this.canvas); }, _onObjectRemoved: function (t) { this.dirty = !0, delete t.group; }, _set: function (t, e) { var i = this._objects.length; if (this.useSetOnGroup) for (; i--;)this._objects[i].setOnGroup(t, e); if ("canvas" === t) for (; i--;)this._objects[i]._set(t, e); h.Object.prototype._set.call(this, t, e); }, toObject: function (r) { var n = this.includeDefaultValues, t = this._objects.filter(function (t) { return !t.excludeFromExport; }).map(function (t) { var e = t.includeDefaultValues; t.includeDefaultValues = n; var i = t.toObject(r); return t.includeDefaultValues = e, i; }), e = h.Object.prototype.toObject.call(this, r); return e.objects = t, e; }, toDatalessObject: function (r) { var t, e = this.sourcePath; if (e) t = e; else { var n = this.includeDefaultValues; t = this._objects.map(function (t) { var e = t.includeDefaultValues; t.includeDefaultValues = n; var i = t.toDatalessObject(r); return t.includeDefaultValues = e, i; }); } var i = h.Object.prototype.toDatalessObject.call(this, r); return i.objects = t, i; }, render: function (t) { this._transformDone = !0, this.callSuper("render", t), this._transformDone = !1; }, shouldCache: function () { var t = h.Object.prototype.shouldCache.call(this); if (t) for (var e = 0, i = this._objects.length; e < i; e++)if (this._objects[e].willDrawShadow()) return this.ownCaching = !1; return t; }, willDrawShadow: function () { if (h.Object.prototype.willDrawShadow.call(this)) return !0; for (var t = 0, e = this._objects.length; t < e; t++)if (this._objects[t].willDrawShadow()) return !0; return !1; }, isOnACache: function () { return this.ownCaching || this.group && this.group.isOnACache(); }, drawObject: function (t) { for (var e = 0, i = this._objects.length; e < i; e++)this._objects[e].render(t); this._drawClipPath(t, this.clipPath); }, isCacheDirty: function (t) { if (this.callSuper("isCacheDirty", t)) return !0; if (!this.statefullCache) return !1; for (var e = 0, i = this._objects.length; e < i; e++)if (this._objects[e].isCacheDirty(!0)) { if (this._cacheCanvas) { var r = this.cacheWidth / this.zoomX, n = this.cacheHeight / this.zoomY; this._cacheContext.clearRect(-r / 2, -n / 2, r, n); } return !0; } return !1; }, _restoreObjectsState: function () { var e = this.calcOwnMatrix(); return this._objects.forEach(function (t) { h.util.addTransformToObject(t, e), delete t.group, t.setCoords(); }), this; }, destroy: function () { return this._objects.forEach(function (t) { t.set("dirty", !0); }), this._restoreObjectsState(); }, dispose: function () { this.callSuper("dispose"), this.forEachObject(function (t) { t.dispose && t.dispose(); }), this._objects = []; }, toActiveSelection: function () { if (this.canvas) { var t = this._objects, e = this.canvas; this._objects = []; var i = this.toObject(); delete i.objects; var r = new h.ActiveSelection([]); return r.set(i), r.type = "activeSelection", e.remove(this), t.forEach(function (t) { t.group = r, t.dirty = !0, e.add(t); }), r.canvas = e, r._objects = t, (e._activeObject = r).setCoords(), r; } }, ungroupOnCanvas: function () { return this._restoreObjectsState(); }, setObjectsCoords: function () { return this.forEachObject(function (t) { t.setCoords(!0); }), this; }, _calcBounds: function (t) { for (var e, i, r, n, s = [], o = [], a = ["tr", "br", "bl", "tl"], c = 0, h = this._objects.length, l = a.length; c < h; ++c) { for (r = (e = this._objects[c]).calcACoords(), n = 0; n < l; n++)i = a[n], s.push(r[i].x), o.push(r[i].y); e.aCoords = r; } this._getBounds(s, o, t); }, _getBounds: function (t, e, i) { var r = new h.Point(l(t), l(e)), n = new h.Point(u(t), u(e)), s = r.y || 0, o = r.x || 0, a = n.x - r.x || 0, c = n.y - r.y || 0; this.width = a, this.height = c, i || this.setPositionByOrigin({ x: o, y: s }, "left", "top"); }, _toSVG: function (t) { for (var e = ["<g ", "COMMON_PARTS", " >\n"], i = 0, r = this._objects.length; i < r; i++)e.push("\t\t", this._objects[i].toSVG(t)); return e.push("</g>\n"), e; }, getSvgStyles: function () { var t = void 0 !== this.opacity && 1 !== this.opacity ? "opacity: " + this.opacity + ";" : "", e = this.visible ? "" : " visibility: hidden;"; return [t, this.getSvgFilter(), e].join(""); }, toClipPathSVG: function (t) { for (var e = [], i = 0, r = this._objects.length; i < r; i++)e.push("\t", this._objects[i].toClipPathSVG(t)); return this._createBaseClipPathSVGMarkup(e, { reviver: t }); } }), h.Group.fromObject = function (r, n) { var s = r.objects, o = h.util.object.clone(r, !0); delete o.objects, "string" != typeof s ? h.util.enlivenObjects(s, function (t) { h.util.enlivenObjectEnlivables(r, o, function () { n && n(new h.Group(t, o, !0)); }); }) : h.loadSVGFromURL(s, function (t) { var e = h.util.groupSVGElements(t, r, s), i = o.clipPath; delete o.clipPath, e.set(o), i ? h.util.enlivenObjects([i], function (t) { e.clipPath = t[0], n && n(e); }) : n && n(e); }); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var n = t.fabric || (t.fabric = {}); n.ActiveSelection || (n.ActiveSelection = n.util.createClass(n.Group, { type: "activeSelection", initialize: function (t, e) { e = e || {}, this._objects = t || []; for (var i = this._objects.length; i--;)this._objects[i].group = this; e.originX && (this.originX = e.originX), e.originY && (this.originY = e.originY), this._calcBounds(), this._updateObjectsCoords(), n.Object.prototype.initialize.call(this, e), this.setCoords(); }, toGroup: function () { var t = this._objects.concat(); this._objects = []; var e = n.Object.prototype.toObject.call(this), i = new n.Group([]); if (delete e.type, i.set(e), t.forEach(function (t) { t.canvas.remove(t), t.group = i; }), i._objects = t, !this.canvas) return i; var r = this.canvas; return r.add(i), (r._activeObject = i).setCoords(), i; }, onDeselect: function () { return this.destroy(), !1; }, toString: function () { return "#<fabric.ActiveSelection: (" + this.complexity() + ")>"; }, shouldCache: function () { return !1; }, isOnACache: function () { return !1; }, _renderControls: function (t, e, i) { t.save(), t.globalAlpha = this.isMoving ? this.borderOpacityWhenMoving : 1, this.callSuper("_renderControls", t, e), void 0 === (i = i || {}).hasControls && (i.hasControls = !1), i.forActiveSelection = !0; for (var r = 0, n = this._objects.length; r < n; r++)this._objects[r]._renderControls(t, i); t.restore(); } }), n.ActiveSelection.fromObject = function (e, i) { n.util.enlivenObjects(e.objects, function (t) { delete e.objects, i && i(new n.ActiveSelection(t, e, !0)); }); }); }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var n = fabric.util.object.extend; t.fabric || (t.fabric = {}), t.fabric.Image ? fabric.warn("fabric.Image is already defined.") : (fabric.Image = fabric.util.createClass(fabric.Object, { type: "image", strokeWidth: 0, srcFromAttribute: !1, _lastScaleX: 1, _lastScaleY: 1, _filterScalingX: 1, _filterScalingY: 1, minimumScaleTrigger: .5, stateProperties: fabric.Object.prototype.stateProperties.concat("cropX", "cropY"), cacheProperties: fabric.Object.prototype.cacheProperties.concat("cropX", "cropY"), cacheKey: "", cropX: 0, cropY: 0, imageSmoothing: !0, initialize: function (t, e) { e || (e = {}), this.filters = [], this.cacheKey = "texture" + fabric.Object.__uid++, this.callSuper("initialize", e), this._initElement(t, e); }, getElement: function () { return this._element || {}; }, setElement: function (t, e) { return this.removeTexture(this.cacheKey), this.removeTexture(this.cacheKey + "_filtered"), this._element = t, this._originalElement = t, this._initConfig(e), 0 !== this.filters.length && this.applyFilters(), this.resizeFilter && this.applyResizeFilters(), this; }, removeTexture: function (t) { var e = fabric.filterBackend; e && e.evictCachesForKey && e.evictCachesForKey(t); }, dispose: function () { this.callSuper("dispose"), this.removeTexture(this.cacheKey), this.removeTexture(this.cacheKey + "_filtered"), this._cacheContext = void 0, ["_originalElement", "_element", "_filteredEl", "_cacheCanvas"].forEach(function (t) { fabric.util.cleanUpJsdomNode(this[t]), this[t] = void 0; }.bind(this)); }, getCrossOrigin: function () { return this._originalElement && (this._originalElement.crossOrigin || null); }, getOriginalSize: function () { var t = this.getElement(); return { width: t.naturalWidth || t.width, height: t.naturalHeight || t.height }; }, _stroke: function (t) { if (this.stroke && 0 !== this.strokeWidth) { var e = this.width / 2, i = this.height / 2; t.beginPath(), t.moveTo(-e, -i), t.lineTo(e, -i), t.lineTo(e, i), t.lineTo(-e, i), t.lineTo(-e, -i), t.closePath(); } }, toObject: function (t) { var e = []; this.filters.forEach(function (t) { t && e.push(t.toObject()); }); var i = n(this.callSuper("toObject", ["cropX", "cropY"].concat(t)), { src: this.getSrc(), crossOrigin: this.getCrossOrigin(), filters: e }); return this.resizeFilter && (i.resizeFilter = this.resizeFilter.toObject()), i; }, hasCrop: function () { return this.cropX || this.cropY || this.width < this._element.width || this.height < this._element.height; }, _toSVG: function () { var t, e = [], i = [], r = this._element, n = -this.width / 2, s = -this.height / 2, o = "", a = ""; if (!r) return []; if (this.hasCrop()) { var c = fabric.Object.__uid++; e.push('<clipPath id="imageCrop_' + c + '">\n', '\t<rect x="' + n + '" y="' + s + '" width="' + this.width + '" height="' + this.height + '" />\n', "</clipPath>\n"), o = ' clip-path="url(#imageCrop_' + c + ')" '; } if (this.imageSmoothing || (a = '" image-rendering="optimizeSpeed'), i.push("\t<image ", "COMMON_PARTS", 'xlink:href="', this.getSvgSrc(!0), '" x="', n - this.cropX, '" y="', s - this.cropY, '" width="', r.width || r.naturalWidth, '" height="', r.height || r.height, a, '"', o, "></image>\n"), this.stroke || this.strokeDashArray) { var h = this.fill; this.fill = null, t = ["\t<rect ", 'x="', n, '" y="', s, '" width="', this.width, '" height="', this.height, '" style="', this.getSvgStyles(), '"/>\n'], this.fill = h; } return e = "fill" !== this.paintFirst ? e.concat(t, i) : e.concat(i, t); }, getSrc: function (t) { var e = t ? this._element : this._originalElement; return e ? e.toDataURL ? e.toDataURL() : this.srcFromAttribute ? e.getAttribute("src") : e.src : this.src || ""; }, setSrc: function (t, i, r) { return fabric.util.loadImage(t, function (t, e) { this.setElement(t, r), this._setWidthHeight(), i && i(this, e); }, this, r && r.crossOrigin), this; }, toString: function () { return '#<fabric.Image: { src: "' + this.getSrc() + '" }>'; }, applyResizeFilters: function () { var t = this.resizeFilter, e = this.minimumScaleTrigger, i = this.getTotalObjectScaling(), r = i.scaleX, n = i.scaleY, s = this._filteredEl || this._originalElement; if (this.group && this.set("dirty", !0), !t || e < r && e < n) return this._element = s, this._filterScalingX = 1, this._filterScalingY = 1, this._lastScaleX = r, void (this._lastScaleY = n); fabric.filterBackend || (fabric.filterBackend = fabric.initFilterBackend()); var o = fabric.util.createCanvasElement(), a = this._filteredEl ? this.cacheKey + "_filtered" : this.cacheKey, c = s.width, h = s.height; o.width = c, o.height = h, this._element = o, this._lastScaleX = t.scaleX = r, this._lastScaleY = t.scaleY = n, fabric.filterBackend.applyFilters([t], s, c, h, this._element, a), this._filterScalingX = o.width / this._originalElement.width, this._filterScalingY = o.height / this._originalElement.height; }, applyFilters: function (t) { if (t = (t = t || this.filters || []).filter(function (t) { return t && !t.isNeutralState(); }), this.set("dirty", !0), this.removeTexture(this.cacheKey + "_filtered"), 0 === t.length) return this._element = this._originalElement, this._filteredEl = null, this._filterScalingX = 1, this._filterScalingY = 1, this; var e = this._originalElement, i = e.naturalWidth || e.width, r = e.naturalHeight || e.height; if (this._element === this._originalElement) { var n = fabric.util.createCanvasElement(); n.width = i, n.height = r, this._element = n, this._filteredEl = n; } else this._element = this._filteredEl, this._filteredEl.getContext("2d").clearRect(0, 0, i, r), this._lastScaleX = 1, this._lastScaleY = 1; return fabric.filterBackend || (fabric.filterBackend = fabric.initFilterBackend()), fabric.filterBackend.applyFilters(t, this._originalElement, i, r, this._element, this.cacheKey), this._originalElement.width === this._element.width && this._originalElement.height === this._element.height || (this._filterScalingX = this._element.width / this._originalElement.width, this._filterScalingY = this._element.height / this._originalElement.height), this; }, _render: function (t) { fabric.util.setImageSmoothing(t, this.imageSmoothing), !0 !== this.isMoving && this.resizeFilter && this._needsResize() && this.applyResizeFilters(), this._stroke(t), this._renderPaintInOrder(t); }, drawCacheOnCanvas: function (t) { fabric.util.setImageSmoothing(t, this.imageSmoothing), fabric.Object.prototype.drawCacheOnCanvas.call(this, t); }, shouldCache: function () { return this.needsItsOwnCache(); }, _renderFill: function (t) { var e = this._element; if (e) { var i = this._filterScalingX, r = this._filterScalingY, n = this.width, s = this.height, o = Math.min, a = Math.max, c = a(this.cropX, 0), h = a(this.cropY, 0), l = e.naturalWidth || e.width, u = e.naturalHeight || e.height, f = c * i, d = h * r, g = o(n * i, l - f), p = o(s * r, u - d), v = -n / 2, m = -s / 2, b = o(n, l / i - c), y = o(s, u / r - h); e && t.drawImage(e, f, d, g, p, v, m, b, y); } }, _needsResize: function () { var t = this.getTotalObjectScaling(); return t.scaleX !== this._lastScaleX || t.scaleY !== this._lastScaleY; }, _resetWidthHeight: function () { this.set(this.getOriginalSize()); }, _initElement: function (t, e) { this.setElement(fabric.util.getById(t), e), fabric.util.addClass(this.getElement(), fabric.Image.CSS_CANVAS); }, _initConfig: function (t) { t || (t = {}), this.setOptions(t), this._setWidthHeight(t); }, _initFilters: function (t, e) { t && t.length ? fabric.util.enlivenObjects(t, function (t) { e && e(t); }, "fabric.Image.filters") : e && e(); }, _setWidthHeight: function (t) { t || (t = {}); var e = this.getElement(); this.width = t.width || e.naturalWidth || e.width || 0, this.height = t.height || e.naturalHeight || e.height || 0; }, parsePreserveAspectRatioAttribute: function () { var t, e = fabric.util.parsePreserveAspectRatioAttribute(this.preserveAspectRatio || ""), i = this._element.width, r = this._element.height, n = 1, s = 1, o = 0, a = 0, c = 0, h = 0, l = this.width, u = this.height, f = { width: l, height: u }; return !e || "none" === e.alignX && "none" === e.alignY ? (n = l / i, s = u / r) : ("meet" === e.meetOrSlice && (t = (l - i * (n = s = fabric.util.findScaleToFit(this._element, f))) / 2, "Min" === e.alignX && (o = -t), "Max" === e.alignX && (o = t), t = (u - r * s) / 2, "Min" === e.alignY && (a = -t), "Max" === e.alignY && (a = t)), "slice" === e.meetOrSlice && (t = i - l / (n = s = fabric.util.findScaleToCover(this._element, f)), "Mid" === e.alignX && (c = t / 2), "Max" === e.alignX && (c = t), t = r - u / s, "Mid" === e.alignY && (h = t / 2), "Max" === e.alignY && (h = t), i = l / n, r = u / s)), { width: i, height: r, scaleX: n, scaleY: s, offsetLeft: o, offsetTop: a, cropX: c, cropY: h }; } }), fabric.Image.CSS_CANVAS = "canvas-img", fabric.Image.prototype.getSvgSrc = fabric.Image.prototype.getSrc, fabric.Image.fromObject = function (t, i) { var r = fabric.util.object.clone(t); fabric.util.loadImage(r.src, function (e, t) { t ? i && i(null, !0) : fabric.Image.prototype._initFilters.call(r, r.filters, function (t) { r.filters = t || [], fabric.Image.prototype._initFilters.call(r, [r.resizeFilter], function (t) { r.resizeFilter = t[0], fabric.util.enlivenObjectEnlivables(r, r, function () { var t = new fabric.Image(e, r); i(t, !1); }); }); }); }, null, r.crossOrigin); }, fabric.Image.fromURL = function (t, i, r) { fabric.util.loadImage(t, function (t, e) { i && i(new fabric.Image(t, r), e); }, null, r && r.crossOrigin); }, fabric.Image.ATTRIBUTE_NAMES = fabric.SHARED_ATTRIBUTES.concat("x y width height preserveAspectRatio xlink:href crossOrigin image-rendering".split(" ")), fabric.Image.fromElement = function (t, e, i) { var r = fabric.parseAttributes(t, fabric.Image.ATTRIBUTE_NAMES); fabric.Image.fromURL(r["xlink:href"], e, n(i ? fabric.util.object.clone(i) : {}, r)); }); }("undefined" != typeof exports ? exports : this), fabric.util.object.extend(fabric.Object.prototype, { _getAngleValueForStraighten: function () { var t = this.angle % 360; return 0 < t ? 90 * Math.round((t - 1) / 90) : 90 * Math.round(t / 90); }, straighten: function () { return this.rotate(this._getAngleValueForStraighten()); }, fxStraighten: function (t) { var e = function () { }, i = (t = t || {}).onComplete || e, r = t.onChange || e, n = this; return fabric.util.animate({ target: this, startValue: this.get("angle"), endValue: this._getAngleValueForStraighten(), duration: this.FX_DURATION, onChange: function (t) { n.rotate(t), r(); }, onComplete: function () { n.setCoords(), i(); } }); } }), fabric.util.object.extend(fabric.StaticCanvas.prototype, { straightenObject: function (t) { return t.straighten(), this.requestRenderAll(), this; }, fxStraightenObject: function (t) { return t.fxStraighten({ onChange: this.requestRenderAllBound }); } }), function () { "use strict"; function t(t) { t && t.tileSize && (this.tileSize = t.tileSize), this.setupGLContext(this.tileSize, this.tileSize), this.captureGPUInfo(); } fabric.isWebglSupported = function (t) { if (fabric.isLikelyNode) return !1; t = t || fabric.WebglFilterBackend.prototype.tileSize; var e, i, r, n = document.createElement("canvas"), s = n.getContext("webgl") || n.getContext("experimental-webgl"), o = !1; if (s) { fabric.maxTextureSize = s.getParameter(s.MAX_TEXTURE_SIZE), o = fabric.maxTextureSize >= t; for (var a = ["highp", "mediump", "lowp"], c = 0; c < 3; c++)if (void 0, i = "precision " + a[c] + " float;\nvoid main(){}", r = (e = s).createShader(e.FRAGMENT_SHADER), e.shaderSource(r, i), e.compileShader(r), e.getShaderParameter(r, e.COMPILE_STATUS)) { fabric.webGlPrecision = a[c]; break; } } return this.isSupported = o; }, (fabric.WebglFilterBackend = t).prototype = { tileSize: 2048, resources: {}, setupGLContext: function (t, e) { this.dispose(), this.createWebGLCanvas(t, e), this.aPosition = new Float32Array([0, 0, 0, 1, 1, 0, 1, 1]), this.chooseFastestCopyGLTo2DMethod(t, e); }, chooseFastestCopyGLTo2DMethod: function (t, e) { var i, r = void 0 !== window.performance; try { new ImageData(1, 1), i = !0; } catch (t) { i = !1; } var n = "undefined" != typeof ArrayBuffer, s = "undefined" != typeof Uint8ClampedArray; if (r && i && n && s) { var o = fabric.util.createCanvasElement(), a = new ArrayBuffer(t * e * 4); if (fabric.forceGLPutImageData) return this.imageBuffer = a, void (this.copyGLTo2D = copyGLTo2DPutImageData); var c, h, l = { imageBuffer: a, destinationWidth: t, destinationHeight: e, targetCanvas: o }; o.width = t, o.height = e, c = window.performance.now(), copyGLTo2DDrawImage.call(l, this.gl, l), h = window.performance.now() - c, c = window.performance.now(), copyGLTo2DPutImageData.call(l, this.gl, l), window.performance.now() - c < h ? (this.imageBuffer = a, this.copyGLTo2D = copyGLTo2DPutImageData) : this.copyGLTo2D = copyGLTo2DDrawImage; } }, createWebGLCanvas: function (t, e) { var i = fabric.util.createCanvasElement(); i.width = t, i.height = e; var r = { alpha: !0, premultipliedAlpha: !1, depth: !1, stencil: !1, antialias: !1 }, n = i.getContext("webgl", r); n || (n = i.getContext("experimental-webgl", r)), n && (n.clearColor(0, 0, 0, 0), this.canvas = i, this.gl = n); }, applyFilters: function (t, e, i, r, n, s) { var o, a = this.gl; s && (o = this.getCachedTexture(s, e)); var c = { originalWidth: e.width || e.originalWidth, originalHeight: e.height || e.originalHeight, sourceWidth: i, sourceHeight: r, destinationWidth: i, destinationHeight: r, context: a, sourceTexture: this.createTexture(a, i, r, !o && e), targetTexture: this.createTexture(a, i, r), originalTexture: o || this.createTexture(a, i, r, !o && e), passes: t.length, webgl: !0, aPosition: this.aPosition, programCache: this.programCache, pass: 0, filterBackend: this, targetCanvas: n }, h = a.createFramebuffer(); return a.bindFramebuffer(a.FRAMEBUFFER, h), t.forEach(function (t) { t && t.applyTo(c); }), resizeCanvasIfNeeded(c), this.copyGLTo2D(a, c), a.bindTexture(a.TEXTURE_2D, null), a.deleteTexture(c.sourceTexture), a.deleteTexture(c.targetTexture), a.deleteFramebuffer(h), n.getContext("2d").setTransform(1, 0, 0, 1, 0, 0), c; }, dispose: function () { this.canvas && (this.canvas = null, this.gl = null), this.clearWebGLCaches(); }, clearWebGLCaches: function () { this.programCache = {}, this.textureCache = {}; }, createTexture: function (t, e, i, r, n) { var s = t.createTexture(); return t.bindTexture(t.TEXTURE_2D, s), t.texParameteri(t.TEXTURE_2D, t.TEXTURE_MAG_FILTER, n || t.NEAREST), t.texParameteri(t.TEXTURE_2D, t.TEXTURE_MIN_FILTER, n || t.NEAREST), t.texParameteri(t.TEXTURE_2D, t.TEXTURE_WRAP_S, t.CLAMP_TO_EDGE), t.texParameteri(t.TEXTURE_2D, t.TEXTURE_WRAP_T, t.CLAMP_TO_EDGE), r ? t.texImage2D(t.TEXTURE_2D, 0, t.RGBA, t.RGBA, t.UNSIGNED_BYTE, r) : t.texImage2D(t.TEXTURE_2D, 0, t.RGBA, e, i, 0, t.RGBA, t.UNSIGNED_BYTE, null), s; }, getCachedTexture: function (t, e) { if (this.textureCache[t]) return this.textureCache[t]; var i = this.createTexture(this.gl, e.width, e.height, e); return this.textureCache[t] = i; }, evictCachesForKey: function (t) { this.textureCache[t] && (this.gl.deleteTexture(this.textureCache[t]), delete this.textureCache[t]); }, copyGLTo2D: copyGLTo2DDrawImage, captureGPUInfo: function () { if (this.gpuInfo) return this.gpuInfo; var t = this.gl, e = { renderer: "", vendor: "" }; if (!t) return e; var i = t.getExtension("WEBGL_debug_renderer_info"); if (i) { var r = t.getParameter(i.UNMASKED_RENDERER_WEBGL), n = t.getParameter(i.UNMASKED_VENDOR_WEBGL); r && (e.renderer = r.toLowerCase()), n && (e.vendor = n.toLowerCase()); } return this.gpuInfo = e; } }; }(), function () { "use strict"; var t = function () { }; function e() { } (fabric.Canvas2dFilterBackend = e).prototype = { evictCachesForKey: t, dispose: t, clearWebGLCaches: t, resources: {}, applyFilters: function (t, e, i, r, n) { var s = n.getContext("2d"); s.drawImage(e, 0, 0, i, r); var o = { sourceWidth: i, sourceHeight: r, imageData: s.getImageData(0, 0, i, r), originalEl: e, originalImageData: s.getImageData(0, 0, i, r), canvasEl: n, ctx: s, filterBackend: this }; return t.forEach(function (t) { t.applyTo(o); }), o.imageData.width === i && o.imageData.height === r || (n.width = o.imageData.width, n.height = o.imageData.height), s.putImageData(o.imageData, 0, 0), o; } }; }(), fabric.Image = fabric.Image || {}, fabric.Image.filters = fabric.Image.filters || {}, fabric.Image.filters.BaseFilter = fabric.util.createClass({ type: "BaseFilter", vertexSource: "attribute vec2 aPosition;\nvarying vec2 vTexCoord;\nvoid main() {\nvTexCoord = aPosition;\ngl_Position = vec4(aPosition * 2.0 - 1.0, 0.0, 1.0);\n}", fragmentSource: "precision highp float;\nvarying vec2 vTexCoord;\nuniform sampler2D uTexture;\nvoid main() {\ngl_FragColor = texture2D(uTexture, vTexCoord);\n}", initialize: function (t) { t && this.setOptions(t); }, setOptions: function (t) { for (var e in t) this[e] = t[e]; }, createProgram: function (t, e, i) { e = e || this.fragmentSource, i = i || this.vertexSource, "highp" !== fabric.webGlPrecision && (e = e.replace(/precision highp float/g, "precision " + fabric.webGlPrecision + " float")); var r = t.createShader(t.VERTEX_SHADER); if (t.shaderSource(r, i), t.compileShader(r), !t.getShaderParameter(r, t.COMPILE_STATUS)) throw new Error("Vertex shader compile error for " + this.type + ": " + t.getShaderInfoLog(r)); var n = t.createShader(t.FRAGMENT_SHADER); if (t.shaderSource(n, e), t.compileShader(n), !t.getShaderParameter(n, t.COMPILE_STATUS)) throw new Error("Fragment shader compile error for " + this.type + ": " + t.getShaderInfoLog(n)); var s = t.createProgram(); if (t.attachShader(s, r), t.attachShader(s, n), t.linkProgram(s), !t.getProgramParameter(s, t.LINK_STATUS)) throw new Error('Shader link error for "${this.type}" ' + t.getProgramInfoLog(s)); var o = this.getAttributeLocations(t, s), a = this.getUniformLocations(t, s) || {}; return a.uStepW = t.getUniformLocation(s, "uStepW"), a.uStepH = t.getUniformLocation(s, "uStepH"), { program: s, attributeLocations: o, uniformLocations: a }; }, getAttributeLocations: function (t, e) { return { aPosition: t.getAttribLocation(e, "aPosition") }; }, getUniformLocations: function () { return {}; }, sendAttributeData: function (t, e, i) { var r = e.aPosition, n = t.createBuffer(); t.bindBuffer(t.ARRAY_BUFFER, n), t.enableVertexAttribArray(r), t.vertexAttribPointer(r, 2, t.FLOAT, !1, 0, 0), t.bufferData(t.ARRAY_BUFFER, i, t.STATIC_DRAW); }, _setupFrameBuffer: function (t) { var e, i, r = t.context; 1 < t.passes ? (e = t.destinationWidth, i = t.destinationHeight, t.sourceWidth === e && t.sourceHeight === i || (r.deleteTexture(t.targetTexture), t.targetTexture = t.filterBackend.createTexture(r, e, i)), r.framebufferTexture2D(r.FRAMEBUFFER, r.COLOR_ATTACHMENT0, r.TEXTURE_2D, t.targetTexture, 0)) : (r.bindFramebuffer(r.FRAMEBUFFER, null), r.finish()); }, _swapTextures: function (t) { t.passes--, t.pass++; var e = t.targetTexture; t.targetTexture = t.sourceTexture, t.sourceTexture = e; }, isNeutralState: function () { var t = this.mainParameter, e = fabric.Image.filters[this.type].prototype; if (t) { if (Array.isArray(e[t])) { for (var i = e[t].length; i--;)if (this[t][i] !== e[t][i]) return !1; return !0; } return e[t] === this[t]; } return !1; }, applyTo: function (t) { t.webgl ? (this._setupFrameBuffer(t), this.applyToWebGL(t), this._swapTextures(t)) : this.applyTo2d(t); }, retrieveShader: function (t) { return t.programCache.hasOwnProperty(this.type) || (t.programCache[this.type] = this.createProgram(t.context)), t.programCache[this.type]; }, applyToWebGL: function (t) { var e = t.context, i = this.retrieveShader(t); 0 === t.pass && t.originalTexture ? e.bindTexture(e.TEXTURE_2D, t.originalTexture) : e.bindTexture(e.TEXTURE_2D, t.sourceTexture), e.useProgram(i.program), this.sendAttributeData(e, i.attributeLocations, t.aPosition), e.uniform1f(i.uniformLocations.uStepW, 1 / t.sourceWidth), e.uniform1f(i.uniformLocations.uStepH, 1 / t.sourceHeight), this.sendUniformData(e, i.uniformLocations), e.viewport(0, 0, t.destinationWidth, t.destinationHeight), e.drawArrays(e.TRIANGLE_STRIP, 0, 4); }, bindAdditionalTexture: function (t, e, i) { t.activeTexture(i), t.bindTexture(t.TEXTURE_2D, e), t.activeTexture(t.TEXTURE0); }, unbindAdditionalTexture: function (t, e) { t.activeTexture(e), t.bindTexture(t.TEXTURE_2D, null), t.activeTexture(t.TEXTURE0); }, getMainParameter: function () { return this[this.mainParameter]; }, setMainParameter: function (t) { this[this.mainParameter] = t; }, sendUniformData: function () { }, createHelpLayer: function (t) { if (!t.helpLayer) { var e = document.createElement("canvas"); e.width = t.sourceWidth, e.height = t.sourceHeight, t.helpLayer = e; } }, toObject: function () { var t = { type: this.type }, e = this.mainParameter; return e && (t[e] = this[e]), t; }, toJSON: function () { return this.toObject(); } }), fabric.Image.filters.BaseFilter.fromObject = function (t, e) { var i = new fabric.Image.filters[t.type](t); return e && e(i), i; }, function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.ColorMatrix = r(i.BaseFilter, { type: "ColorMatrix", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nvarying vec2 vTexCoord;\nuniform mat4 uColorMatrix;\nuniform vec4 uConstants;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\ncolor *= uColorMatrix;\ncolor += uConstants;\ngl_FragColor = color;\n}", matrix: [1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0], mainParameter: "matrix", colorsOnly: !0, initialize: function (t) { this.callSuper("initialize", t), this.matrix = this.matrix.slice(0); }, applyTo2d: function (t) { var e, i, r, n, s, o = t.imageData.data, a = o.length, c = this.matrix, h = this.colorsOnly; for (s = 0; s < a; s += 4)e = o[s], i = o[s + 1], r = o[s + 2], h ? (o[s] = e * c[0] + i * c[1] + r * c[2] + 255 * c[4], o[s + 1] = e * c[5] + i * c[6] + r * c[7] + 255 * c[9], o[s + 2] = e * c[10] + i * c[11] + r * c[12] + 255 * c[14]) : (n = o[s + 3], o[s] = e * c[0] + i * c[1] + r * c[2] + n * c[3] + 255 * c[4], o[s + 1] = e * c[5] + i * c[6] + r * c[7] + n * c[8] + 255 * c[9], o[s + 2] = e * c[10] + i * c[11] + r * c[12] + n * c[13] + 255 * c[14], o[s + 3] = e * c[15] + i * c[16] + r * c[17] + n * c[18] + 255 * c[19]); }, getUniformLocations: function (t, e) { return { uColorMatrix: t.getUniformLocation(e, "uColorMatrix"), uConstants: t.getUniformLocation(e, "uConstants") }; }, sendUniformData: function (t, e) { var i = this.matrix, r = [i[0], i[1], i[2], i[3], i[5], i[6], i[7], i[8], i[10], i[11], i[12], i[13], i[15], i[16], i[17], i[18]], n = [i[4], i[9], i[14], i[19]]; t.uniformMatrix4fv(e.uColorMatrix, !1, r), t.uniform4fv(e.uConstants, n); } }), e.Image.filters.ColorMatrix.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.Brightness = r(i.BaseFilter, { type: "Brightness", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uBrightness;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\ncolor.rgb += uBrightness;\ngl_FragColor = color;\n}", brightness: 0, mainParameter: "brightness", applyTo2d: function (t) { if (0 !== this.brightness) { var e, i = t.imageData.data, r = i.length, n = Math.round(255 * this.brightness); for (e = 0; e < r; e += 4)i[e] = i[e] + n, i[e + 1] = i[e + 1] + n, i[e + 2] = i[e + 2] + n; } }, getUniformLocations: function (t, e) { return { uBrightness: t.getUniformLocation(e, "uBrightness") }; }, sendUniformData: function (t, e) { t.uniform1f(e.uBrightness, this.brightness); } }), e.Image.filters.Brightness.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass; r.Convolute = n(r.BaseFilter, { type: "Convolute", opaque: !1, matrix: [0, 0, 0, 0, 1, 0, 0, 0, 0], fragmentSource: { Convolute_3_1: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uMatrix[9];\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = vec4(0, 0, 0, 0);\nfor (float h = 0.0; h < 3.0; h+=1.0) {\nfor (float w = 0.0; w < 3.0; w+=1.0) {\nvec2 matrixPos = vec2(uStepW * (w - 1), uStepH * (h - 1));\ncolor += texture2D(uTexture, vTexCoord + matrixPos) * uMatrix[int(h * 3.0 + w)];\n}\n}\ngl_FragColor = color;\n}", Convolute_3_0: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uMatrix[9];\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = vec4(0, 0, 0, 1);\nfor (float h = 0.0; h < 3.0; h+=1.0) {\nfor (float w = 0.0; w < 3.0; w+=1.0) {\nvec2 matrixPos = vec2(uStepW * (w - 1.0), uStepH * (h - 1.0));\ncolor.rgb += texture2D(uTexture, vTexCoord + matrixPos).rgb * uMatrix[int(h * 3.0 + w)];\n}\n}\nfloat alpha = texture2D(uTexture, vTexCoord).a;\ngl_FragColor = color;\ngl_FragColor.a = alpha;\n}", Convolute_5_1: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uMatrix[25];\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = vec4(0, 0, 0, 0);\nfor (float h = 0.0; h < 5.0; h+=1.0) {\nfor (float w = 0.0; w < 5.0; w+=1.0) {\nvec2 matrixPos = vec2(uStepW * (w - 2.0), uStepH * (h - 2.0));\ncolor += texture2D(uTexture, vTexCoord + matrixPos) * uMatrix[int(h * 5.0 + w)];\n}\n}\ngl_FragColor = color;\n}", Convolute_5_0: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uMatrix[25];\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = vec4(0, 0, 0, 1);\nfor (float h = 0.0; h < 5.0; h+=1.0) {\nfor (float w = 0.0; w < 5.0; w+=1.0) {\nvec2 matrixPos = vec2(uStepW * (w - 2.0), uStepH * (h - 2.0));\ncolor.rgb += texture2D(uTexture, vTexCoord + matrixPos).rgb * uMatrix[int(h * 5.0 + w)];\n}\n}\nfloat alpha = texture2D(uTexture, vTexCoord).a;\ngl_FragColor = color;\ngl_FragColor.a = alpha;\n}", Convolute_7_1: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uMatrix[49];\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = vec4(0, 0, 0, 0);\nfor (float h = 0.0; h < 7.0; h+=1.0) {\nfor (float w = 0.0; w < 7.0; w+=1.0) {\nvec2 matrixPos = vec2(uStepW * (w - 3.0), uStepH * (h - 3.0));\ncolor += texture2D(uTexture, vTexCoord + matrixPos) * uMatrix[int(h * 7.0 + w)];\n}\n}\ngl_FragColor = color;\n}", Convolute_7_0: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uMatrix[49];\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = vec4(0, 0, 0, 1);\nfor (float h = 0.0; h < 7.0; h+=1.0) {\nfor (float w = 0.0; w < 7.0; w+=1.0) {\nvec2 matrixPos = vec2(uStepW * (w - 3.0), uStepH * (h - 3.0));\ncolor.rgb += texture2D(uTexture, vTexCoord + matrixPos).rgb * uMatrix[int(h * 7.0 + w)];\n}\n}\nfloat alpha = texture2D(uTexture, vTexCoord).a;\ngl_FragColor = color;\ngl_FragColor.a = alpha;\n}", Convolute_9_1: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uMatrix[81];\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = vec4(0, 0, 0, 0);\nfor (float h = 0.0; h < 9.0; h+=1.0) {\nfor (float w = 0.0; w < 9.0; w+=1.0) {\nvec2 matrixPos = vec2(uStepW * (w - 4.0), uStepH * (h - 4.0));\ncolor += texture2D(uTexture, vTexCoord + matrixPos) * uMatrix[int(h * 9.0 + w)];\n}\n}\ngl_FragColor = color;\n}", Convolute_9_0: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uMatrix[81];\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = vec4(0, 0, 0, 1);\nfor (float h = 0.0; h < 9.0; h+=1.0) {\nfor (float w = 0.0; w < 9.0; w+=1.0) {\nvec2 matrixPos = vec2(uStepW * (w - 4.0), uStepH * (h - 4.0));\ncolor.rgb += texture2D(uTexture, vTexCoord + matrixPos).rgb * uMatrix[int(h * 9.0 + w)];\n}\n}\nfloat alpha = texture2D(uTexture, vTexCoord).a;\ngl_FragColor = color;\ngl_FragColor.a = alpha;\n}" }, retrieveShader: function (t) { var e = Math.sqrt(this.matrix.length), i = this.type + "_" + e + "_" + (this.opaque ? 1 : 0), r = this.fragmentSource[i]; return t.programCache.hasOwnProperty(i) || (t.programCache[i] = this.createProgram(t.context, r)), t.programCache[i]; }, applyTo2d: function (t) { var e, i, r, n, s, o, a, c, h, l, u, f, d, g = t.imageData, p = g.data, v = this.matrix, m = Math.round(Math.sqrt(v.length)), b = Math.floor(m / 2), y = g.width, _ = g.height, x = t.ctx.createImageData(y, _), C = x.data, S = this.opaque ? 1 : 0; for (u = 0; u < _; u++)for (l = 0; l < y; l++) { for (s = 4 * (u * y + l), d = n = r = i = e = 0; d < m; d++)for (f = 0; f < m; f++)o = l + f - b, (a = u + d - b) < 0 || _ <= a || o < 0 || y <= o || (c = 4 * (a * y + o), h = v[d * m + f], e += p[c] * h, i += p[c + 1] * h, r += p[c + 2] * h, S || (n += p[c + 3] * h)); C[s] = e, C[s + 1] = i, C[s + 2] = r, C[s + 3] = S ? p[s + 3] : n; } t.imageData = x; }, getUniformLocations: function (t, e) { return { uMatrix: t.getUniformLocation(e, "uMatrix"), uOpaque: t.getUniformLocation(e, "uOpaque"), uHalfSize: t.getUniformLocation(e, "uHalfSize"), uSize: t.getUniformLocation(e, "uSize") }; }, sendUniformData: function (t, e) { t.uniform1fv(e.uMatrix, this.matrix); }, toObject: function () { return i(this.callSuper("toObject"), { opaque: this.opaque, matrix: this.matrix }); } }), e.Image.filters.Convolute.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.Grayscale = r(i.BaseFilter, { type: "Grayscale", fragmentSource: { average: "precision highp float;\nuniform sampler2D uTexture;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\nfloat average = (color.r + color.b + color.g) / 3.0;\ngl_FragColor = vec4(average, average, average, color.a);\n}", lightness: "precision highp float;\nuniform sampler2D uTexture;\nuniform int uMode;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 col = texture2D(uTexture, vTexCoord);\nfloat average = (max(max(col.r, col.g),col.b) + min(min(col.r, col.g),col.b)) / 2.0;\ngl_FragColor = vec4(average, average, average, col.a);\n}", luminosity: "precision highp float;\nuniform sampler2D uTexture;\nuniform int uMode;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 col = texture2D(uTexture, vTexCoord);\nfloat average = 0.21 * col.r + 0.72 * col.g + 0.07 * col.b;\ngl_FragColor = vec4(average, average, average, col.a);\n}" }, mode: "average", mainParameter: "mode", applyTo2d: function (t) { var e, i, r = t.imageData.data, n = r.length, s = this.mode; for (e = 0; e < n; e += 4)"average" === s ? i = (r[e] + r[e + 1] + r[e + 2]) / 3 : "lightness" === s ? i = (Math.min(r[e], r[e + 1], r[e + 2]) + Math.max(r[e], r[e + 1], r[e + 2])) / 2 : "luminosity" === s && (i = .21 * r[e] + .72 * r[e + 1] + .07 * r[e + 2]), r[e] = i, r[e + 1] = i, r[e + 2] = i; }, retrieveShader: function (t) { var e = this.type + "_" + this.mode; if (!t.programCache.hasOwnProperty(e)) { var i = this.fragmentSource[this.mode]; t.programCache[e] = this.createProgram(t.context, i); } return t.programCache[e]; }, getUniformLocations: function (t, e) { return { uMode: t.getUniformLocation(e, "uMode") }; }, sendUniformData: function (t, e) { t.uniform1i(e.uMode, 1); }, isNeutralState: function () { return !1; } }), e.Image.filters.Grayscale.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.Invert = r(i.BaseFilter, { type: "Invert", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform int uInvert;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\nif (uInvert == 1) {\ngl_FragColor = vec4(1.0 - color.r,1.0 -color.g,1.0 -color.b,color.a);\n} else {\ngl_FragColor = color;\n}\n}", invert: !0, mainParameter: "invert", applyTo2d: function (t) { var e, i = t.imageData.data, r = i.length; for (e = 0; e < r; e += 4)i[e] = 255 - i[e], i[e + 1] = 255 - i[e + 1], i[e + 2] = 255 - i[e + 2]; }, isNeutralState: function () { return !this.invert; }, getUniformLocations: function (t, e) { return { uInvert: t.getUniformLocation(e, "uInvert") }; }, sendUniformData: function (t, e) { t.uniform1i(e.uInvert, this.invert); } }), e.Image.filters.Invert.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass; r.Noise = n(r.BaseFilter, { type: "Noise", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uStepH;\nuniform float uNoise;\nuniform float uSeed;\nvarying vec2 vTexCoord;\nfloat rand(vec2 co, float seed, float vScale) {\nreturn fract(sin(dot(co.xy * vScale ,vec2(12.9898 , 78.233))) * 43758.5453 * (seed + 0.01) / 2.0);\n}\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\ncolor.rgb += (0.5 - rand(vTexCoord, uSeed, 0.1 / uStepH)) * uNoise;\ngl_FragColor = color;\n}", mainParameter: "noise", noise: 0, applyTo2d: function (t) { if (0 !== this.noise) { var e, i, r = t.imageData.data, n = r.length, s = this.noise; for (e = 0, n = r.length; e < n; e += 4)i = (.5 - Math.random()) * s, r[e] += i, r[e + 1] += i, r[e + 2] += i; } }, getUniformLocations: function (t, e) { return { uNoise: t.getUniformLocation(e, "uNoise"), uSeed: t.getUniformLocation(e, "uSeed") }; }, sendUniformData: function (t, e) { t.uniform1f(e.uNoise, this.noise / 255), t.uniform1f(e.uSeed, Math.random()); }, toObject: function () { return i(this.callSuper("toObject"), { noise: this.noise }); } }), e.Image.filters.Noise.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.Pixelate = r(i.BaseFilter, { type: "Pixelate", blocksize: 4, mainParameter: "blocksize", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uBlocksize;\nuniform float uStepW;\nuniform float uStepH;\nvarying vec2 vTexCoord;\nvoid main() {\nfloat blockW = uBlocksize * uStepW;\nfloat blockH = uBlocksize * uStepW;\nint posX = int(vTexCoord.x / blockW);\nint posY = int(vTexCoord.y / blockH);\nfloat fposX = float(posX);\nfloat fposY = float(posY);\nvec2 squareCoords = vec2(fposX * blockW, fposY * blockH);\nvec4 color = texture2D(uTexture, squareCoords);\ngl_FragColor = color;\n}", applyTo2d: function (t) { var e, i, r, n, s, o, a, c, h, l, u, f = t.imageData, d = f.data, g = f.height, p = f.width; for (i = 0; i < g; i += this.blocksize)for (r = 0; r < p; r += this.blocksize)for (n = d[e = 4 * i * p + 4 * r], s = d[e + 1], o = d[e + 2], a = d[e + 3], l = Math.min(i + this.blocksize, g), u = Math.min(r + this.blocksize, p), c = i; c < l; c++)for (h = r; h < u; h++)d[e = 4 * c * p + 4 * h] = n, d[e + 1] = s, d[e + 2] = o, d[e + 3] = a; }, isNeutralState: function () { return 1 === this.blocksize; }, getUniformLocations: function (t, e) { return { uBlocksize: t.getUniformLocation(e, "uBlocksize"), uStepW: t.getUniformLocation(e, "uStepW"), uStepH: t.getUniformLocation(e, "uStepH") }; }, sendUniformData: function (t, e) { t.uniform1f(e.uBlocksize, this.blocksize); } }), e.Image.filters.Pixelate.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var l = t.fabric || (t.fabric = {}), e = l.util.object.extend, i = l.Image.filters, r = l.util.createClass; i.RemoveColor = r(i.BaseFilter, { type: "RemoveColor", color: "#FFFFFF", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform vec4 uLow;\nuniform vec4 uHigh;\nvarying vec2 vTexCoord;\nvoid main() {\ngl_FragColor = texture2D(uTexture, vTexCoord);\nif(all(greaterThan(gl_FragColor.rgb,uLow.rgb)) && all(greaterThan(uHigh.rgb,gl_FragColor.rgb))) {\ngl_FragColor.a = 0.0;\n}\n}", distance: .02, useAlpha: !1, applyTo2d: function (t) { var e, i, r, n, s = t.imageData.data, o = 255 * this.distance, a = new l.Color(this.color).getSource(), c = [a[0] - o, a[1] - o, a[2] - o], h = [a[0] + o, a[1] + o, a[2] + o]; for (e = 0; e < s.length; e += 4)i = s[e], r = s[e + 1], n = s[e + 2], c[0] < i && c[1] < r && c[2] < n && i < h[0] && r < h[1] && n < h[2] && (s[e + 3] = 0); }, getUniformLocations: function (t, e) { return { uLow: t.getUniformLocation(e, "uLow"), uHigh: t.getUniformLocation(e, "uHigh") }; }, sendUniformData: function (t, e) { var i = new l.Color(this.color).getSource(), r = parseFloat(this.distance), n = [0 + i[0] / 255 - r, 0 + i[1] / 255 - r, 0 + i[2] / 255 - r, 1], s = [i[0] / 255 + r, i[1] / 255 + r, i[2] / 255 + r, 1]; t.uniform4fv(e.uLow, n), t.uniform4fv(e.uHigh, s); }, toObject: function () { return e(this.callSuper("toObject"), { color: this.color, distance: this.distance }); } }), l.Image.filters.RemoveColor.fromObject = l.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass, n = { Brownie: [.5997, .34553, -.27082, 0, .186, -.0377, .86095, .15059, 0, -.1449, .24113, -.07441, .44972, 0, -.02965, 0, 0, 0, 1, 0], Vintage: [.62793, .32021, -.03965, 0, .03784, .02578, .64411, .03259, 0, .02926, .0466, -.08512, .52416, 0, .02023, 0, 0, 0, 1, 0], Kodachrome: [1.12855, -.39673, -.03992, 0, .24991, -.16404, 1.08352, -.05498, 0, .09698, -.16786, -.56034, 1.60148, 0, .13972, 0, 0, 0, 1, 0], Technicolor: [1.91252, -.85453, -.09155, 0, .04624, -.30878, 1.76589, -.10601, 0, -.27589, -.2311, -.75018, 1.84759, 0, .12137, 0, 0, 0, 1, 0], Polaroid: [1.438, -.062, -.062, 0, 0, -.122, 1.378, -.122, 0, 0, -.016, -.016, 1.483, 0, 0, 0, 0, 0, 1, 0], Sepia: [.393, .769, .189, 0, 0, .349, .686, .168, 0, 0, .272, .534, .131, 0, 0, 0, 0, 0, 1, 0], BlackWhite: [1.5, 1.5, 1.5, 0, -1, 1.5, 1.5, 1.5, 0, -1, 1.5, 1.5, 1.5, 0, -1, 0, 0, 0, 1, 0] }; for (var s in n) i[s] = r(i.ColorMatrix, { type: s, matrix: n[s], mainParameter: !1, colorsOnly: !0 }), e.Image.filters[s].fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var f = t.fabric, e = f.Image.filters, i = f.util.createClass; e.BlendColor = i(e.BaseFilter, { type: "BlendColor", color: "#F95C63", mode: "multiply", alpha: 1, fragmentSource: { multiply: "gl_FragColor.rgb *= uColor.rgb;\n", screen: "gl_FragColor.rgb = 1.0 - (1.0 - gl_FragColor.rgb) * (1.0 - uColor.rgb);\n", add: "gl_FragColor.rgb += uColor.rgb;\n", diff: "gl_FragColor.rgb = abs(gl_FragColor.rgb - uColor.rgb);\n", subtract: "gl_FragColor.rgb -= uColor.rgb;\n", lighten: "gl_FragColor.rgb = max(gl_FragColor.rgb, uColor.rgb);\n", darken: "gl_FragColor.rgb = min(gl_FragColor.rgb, uColor.rgb);\n", exclusion: "gl_FragColor.rgb += uColor.rgb - 2.0 * (uColor.rgb * gl_FragColor.rgb);\n", overlay: "if (uColor.r < 0.5) {\ngl_FragColor.r *= 2.0 * uColor.r;\n} else {\ngl_FragColor.r = 1.0 - 2.0 * (1.0 - gl_FragColor.r) * (1.0 - uColor.r);\n}\nif (uColor.g < 0.5) {\ngl_FragColor.g *= 2.0 * uColor.g;\n} else {\ngl_FragColor.g = 1.0 - 2.0 * (1.0 - gl_FragColor.g) * (1.0 - uColor.g);\n}\nif (uColor.b < 0.5) {\ngl_FragColor.b *= 2.0 * uColor.b;\n} else {\ngl_FragColor.b = 1.0 - 2.0 * (1.0 - gl_FragColor.b) * (1.0 - uColor.b);\n}\n", tint: "gl_FragColor.rgb *= (1.0 - uColor.a);\ngl_FragColor.rgb += uColor.rgb;\n" }, buildSource: function (t) { return "precision highp float;\nuniform sampler2D uTexture;\nuniform vec4 uColor;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\ngl_FragColor = color;\nif (color.a > 0.0) {\n" + this.fragmentSource[t] + "}\n}"; }, retrieveShader: function (t) { var e, i = this.type + "_" + this.mode; return t.programCache.hasOwnProperty(i) || (e = this.buildSource(this.mode), t.programCache[i] = this.createProgram(t.context, e)), t.programCache[i]; }, applyTo2d: function (t) { var e, i, r, n, s, o, a, c = t.imageData.data, h = c.length, l = 1 - this.alpha; e = (a = new f.Color(this.color).getSource())[0] * this.alpha, i = a[1] * this.alpha, r = a[2] * this.alpha; for (var u = 0; u < h; u += 4)switch (n = c[u], s = c[u + 1], o = c[u + 2], this.mode) { case "multiply": c[u] = n * e / 255, c[u + 1] = s * i / 255, c[u + 2] = o * r / 255; break; case "screen": c[u] = 255 - (255 - n) * (255 - e) / 255, c[u + 1] = 255 - (255 - s) * (255 - i) / 255, c[u + 2] = 255 - (255 - o) * (255 - r) / 255; break; case "add": c[u] = n + e, c[u + 1] = s + i, c[u + 2] = o + r; break; case "diff": case "difference": c[u] = Math.abs(n - e), c[u + 1] = Math.abs(s - i), c[u + 2] = Math.abs(o - r); break; case "subtract": c[u] = n - e, c[u + 1] = s - i, c[u + 2] = o - r; break; case "darken": c[u] = Math.min(n, e), c[u + 1] = Math.min(s, i), c[u + 2] = Math.min(o, r); break; case "lighten": c[u] = Math.max(n, e), c[u + 1] = Math.max(s, i), c[u + 2] = Math.max(o, r); break; case "overlay": c[u] = e < 128 ? 2 * n * e / 255 : 255 - 2 * (255 - n) * (255 - e) / 255, c[u + 1] = i < 128 ? 2 * s * i / 255 : 255 - 2 * (255 - s) * (255 - i) / 255, c[u + 2] = r < 128 ? 2 * o * r / 255 : 255 - 2 * (255 - o) * (255 - r) / 255; break; case "exclusion": c[u] = e + n - 2 * e * n / 255, c[u + 1] = i + s - 2 * i * s / 255, c[u + 2] = r + o - 2 * r * o / 255; break; case "tint": c[u] = e + n * l, c[u + 1] = i + s * l, c[u + 2] = r + o * l; } }, getUniformLocations: function (t, e) { return { uColor: t.getUniformLocation(e, "uColor") }; }, sendUniformData: function (t, e) { var i = new f.Color(this.color).getSource(); i[0] = this.alpha * i[0] / 255, i[1] = this.alpha * i[1] / 255, i[2] = this.alpha * i[2] / 255, i[3] = this.alpha, t.uniform4fv(e.uColor, i); }, toObject: function () { return { type: this.type, color: this.color, mode: this.mode, alpha: this.alpha }; } }), f.Image.filters.BlendColor.fromObject = f.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var _ = t.fabric, e = _.Image.filters, i = _.util.createClass; e.BlendImage = i(e.BaseFilter, { type: "BlendImage", image: null, mode: "multiply", alpha: 1, vertexSource: "attribute vec2 aPosition;\nvarying vec2 vTexCoord;\nvarying vec2 vTexCoord2;\nuniform mat3 uTransformMatrix;\nvoid main() {\nvTexCoord = aPosition;\nvTexCoord2 = (uTransformMatrix * vec3(aPosition, 1.0)).xy;\ngl_Position = vec4(aPosition * 2.0 - 1.0, 0.0, 1.0);\n}", fragmentSource: { multiply: "precision highp float;\nuniform sampler2D uTexture;\nuniform sampler2D uImage;\nuniform vec4 uColor;\nvarying vec2 vTexCoord;\nvarying vec2 vTexCoord2;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\nvec4 color2 = texture2D(uImage, vTexCoord2);\ncolor.rgba *= color2.rgba;\ngl_FragColor = color;\n}", mask: "precision highp float;\nuniform sampler2D uTexture;\nuniform sampler2D uImage;\nuniform vec4 uColor;\nvarying vec2 vTexCoord;\nvarying vec2 vTexCoord2;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\nvec4 color2 = texture2D(uImage, vTexCoord2);\ncolor.a = color2.a;\ngl_FragColor = color;\n}" }, retrieveShader: function (t) { var e = this.type + "_" + this.mode, i = this.fragmentSource[this.mode]; return t.programCache.hasOwnProperty(e) || (t.programCache[e] = this.createProgram(t.context, i)), t.programCache[e]; }, applyToWebGL: function (t) { var e = t.context, i = this.createTexture(t.filterBackend, this.image); this.bindAdditionalTexture(e, i, e.TEXTURE1), this.callSuper("applyToWebGL", t), this.unbindAdditionalTexture(e, e.TEXTURE1); }, createTexture: function (t, e) { return t.getCachedTexture(e.cacheKey, e._element); }, calculateMatrix: function () { var t = this.image, e = t._element.width, i = t._element.height; return [1 / t.scaleX, 0, 0, 0, 1 / t.scaleY, 0, -t.left / e, -t.top / i, 1]; }, applyTo2d: function (t) { var e, i, r, n, s, o, a, c, h, l, u, f = t.imageData, d = t.filterBackend.resources, g = f.data, p = g.length, v = f.width, m = f.height, b = this.image; d.blendImage || (d.blendImage = _.util.createCanvasElement()), l = (h = d.blendImage).getContext("2d"), h.width !== v || h.height !== m ? (h.width = v, h.height = m) : l.clearRect(0, 0, v, m), l.setTransform(b.scaleX, 0, 0, b.scaleY, b.left, b.top), l.drawImage(b._element, 0, 0, v, m), u = l.getImageData(0, 0, v, m).data; for (var y = 0; y < p; y += 4)switch (s = g[y], o = g[y + 1], a = g[y + 2], c = g[y + 3], e = u[y], i = u[y + 1], r = u[y + 2], n = u[y + 3], this.mode) { case "multiply": g[y] = s * e / 255, g[y + 1] = o * i / 255, g[y + 2] = a * r / 255, g[y + 3] = c * n / 255; break; case "mask": g[y + 3] = n; } }, getUniformLocations: function (t, e) { return { uTransformMatrix: t.getUniformLocation(e, "uTransformMatrix"), uImage: t.getUniformLocation(e, "uImage") }; }, sendUniformData: function (t, e) { var i = this.calculateMatrix(); t.uniform1i(e.uImage, 1), t.uniformMatrix3fv(e.uTransformMatrix, !1, i); }, toObject: function () { return { type: this.type, image: this.image && this.image.toObject(), mode: this.mode, alpha: this.alpha }; } }), _.Image.filters.BlendImage.fromObject = function (i, r) { _.Image.fromObject(i.image, function (t) { var e = _.util.object.clone(i); e.image = t, r(new _.Image.filters.BlendImage(e)); }); }; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var m = t.fabric || (t.fabric = {}), D = Math.pow, j = Math.floor, M = Math.sqrt, F = Math.abs, h = Math.round, r = Math.sin, I = Math.ceil, e = m.Image.filters, i = m.util.createClass; e.Resize = i(e.BaseFilter, { type: "Resize", resizeType: "hermite", scaleX: 1, scaleY: 1, lanczosLobes: 3, getUniformLocations: function (t, e) { return { uDelta: t.getUniformLocation(e, "uDelta"), uTaps: t.getUniformLocation(e, "uTaps") }; }, sendUniformData: function (t, e) { t.uniform2fv(e.uDelta, this.horizontal ? [1 / this.width, 0] : [0, 1 / this.height]), t.uniform1fv(e.uTaps, this.taps); }, retrieveShader: function (t) { var e = this.getFilterWindow(), i = this.type + "_" + e; if (!t.programCache.hasOwnProperty(i)) { var r = this.generateShader(e); t.programCache[i] = this.createProgram(t.context, r); } return t.programCache[i]; }, getFilterWindow: function () { var t = this.tempScale; return Math.ceil(this.lanczosLobes / t); }, getTaps: function () { for (var t = this.lanczosCreate(this.lanczosLobes), e = this.tempScale, i = this.getFilterWindow(), r = new Array(i), n = 1; n <= i; n++)r[n - 1] = t(n * e); return r; }, generateShader: function (t) { for (var e = new Array(t), i = this.fragmentSourceTOP, r = 1; r <= t; r++)e[r - 1] = r + ".0 * uDelta"; return i += "uniform float uTaps[" + t + "];\n", i += "void main() {\n", i += "  vec4 color = texture2D(uTexture, vTexCoord);\n", i += "  float sum = 1.0;\n", e.forEach(function (t, e) { i += "  color += texture2D(uTexture, vTexCoord + " + t + ") * uTaps[" + e + "];\n", i += "  color += texture2D(uTexture, vTexCoord - " + t + ") * uTaps[" + e + "];\n", i += "  sum += 2.0 * uTaps[" + e + "];\n"; }), i += "  gl_FragColor = color / sum;\n", i += "}"; }, fragmentSourceTOP: "precision highp float;\nuniform sampler2D uTexture;\nuniform vec2 uDelta;\nvarying vec2 vTexCoord;\n", applyTo: function (t) { t.webgl ? (t.passes++, this.width = t.sourceWidth, this.horizontal = !0, this.dW = Math.round(this.width * this.scaleX), this.dH = t.sourceHeight, this.tempScale = this.dW / this.width, this.taps = this.getTaps(), t.destinationWidth = this.dW, this._setupFrameBuffer(t), this.applyToWebGL(t), this._swapTextures(t), t.sourceWidth = t.destinationWidth, this.height = t.sourceHeight, this.horizontal = !1, this.dH = Math.round(this.height * this.scaleY), this.tempScale = this.dH / this.height, this.taps = this.getTaps(), t.destinationHeight = this.dH, this._setupFrameBuffer(t), this.applyToWebGL(t), this._swapTextures(t), t.sourceHeight = t.destinationHeight) : this.applyTo2d(t); }, isNeutralState: function () { return 1 === this.scaleX && 1 === this.scaleY; }, lanczosCreate: function (i) { return function (t) { if (i <= t || t <= -i) return 0; if (t < 1.1920929e-7 && -1.1920929e-7 < t) return 1; var e = (t *= Math.PI) / i; return r(t) / t * r(e) / e; }; }, applyTo2d: function (t) { var e = t.imageData, i = this.scaleX, r = this.scaleY; this.rcpScaleX = 1 / i, this.rcpScaleY = 1 / r; var n, s = e.width, o = e.height, a = h(s * i), c = h(o * r); "sliceHack" === this.resizeType ? n = this.sliceByTwo(t, s, o, a, c) : "hermite" === this.resizeType ? n = this.hermiteFastResize(t, s, o, a, c) : "bilinear" === this.resizeType ? n = this.bilinearFiltering(t, s, o, a, c) : "lanczos" === this.resizeType && (n = this.lanczosResize(t, s, o, a, c)), t.imageData = n; }, sliceByTwo: function (t, e, i, r, n) { var s, o, a = t.imageData, c = !1, h = !1, l = .5 * e, u = .5 * i, f = m.filterBackend.resources, d = 0, g = 0, p = e, v = 0; for (f.sliceByTwo || (f.sliceByTwo = document.createElement("canvas")), ((s = f.sliceByTwo).width < 1.5 * e || s.height < i) && (s.width = 1.5 * e, s.height = i), (o = s.getContext("2d")).clearRect(0, 0, 1.5 * e, i), o.putImageData(a, 0, 0), r = j(r), n = j(n); !c || !h;)i = u, r < j(.5 * (e = l)) ? l = j(.5 * l) : (l = r, c = !0), n < j(.5 * u) ? u = j(.5 * u) : (u = n, h = !0), o.drawImage(s, d, g, e, i, p, v, l, u), d = p, g = v, v += u; return o.getImageData(d, g, r, n); }, lanczosResize: function (t, g, p, v, m) { var b = t.imageData.data, y = t.ctx.createImageData(v, m), _ = y.data, x = this.lanczosCreate(this.lanczosLobes), C = this.rcpScaleX, S = this.rcpScaleY, T = 2 / this.rcpScaleX, w = 2 / this.rcpScaleY, O = I(C * this.lanczosLobes / 2), k = I(S * this.lanczosLobes / 2), P = {}, E = {}, A = {}; return function t(e) { var i, r, n, s, o, a, c, h, l, u, f; for (E.x = (e + .5) * C, A.x = j(E.x), i = 0; i < m; i++) { for (E.y = (i + .5) * S, A.y = j(E.y), l = h = c = a = o = 0, r = A.x - O; r <= A.x + O; r++)if (!(r < 0 || g <= r)) { u = j(1e3 * F(r - E.x)), P[u] || (P[u] = {}); for (var d = A.y - k; d <= A.y + k; d++)d < 0 || p <= d || (f = j(1e3 * F(d - E.y)), P[u][f] || (P[u][f] = x(M(D(u * T, 2) + D(f * w, 2)) / 1e3)), 0 < (n = P[u][f]) && (o += n, a += n * b[s = 4 * (d * g + r)], c += n * b[s + 1], h += n * b[s + 2], l += n * b[s + 3])); } _[s = 4 * (i * v + e)] = a / o, _[s + 1] = c / o, _[s + 2] = h / o, _[s + 3] = l / o; } return ++e < v ? t(e) : y; }(0); }, bilinearFiltering: function (t, e, i, r, n) { var s, o, a, c, h, l, u, f, d, g = 0, p = this.rcpScaleX, v = this.rcpScaleY, m = 4 * (e - 1), b = t.imageData.data, y = t.ctx.createImageData(r, n), _ = y.data; for (a = 0; a < n; a++)for (c = 0; c < r; c++)for (h = p * c - (s = j(p * c)), l = v * a - (o = j(v * a)), d = 4 * (o * e + s), u = 0; u < 4; u++)f = b[d + u] * (1 - h) * (1 - l) + b[d + 4 + u] * h * (1 - l) + b[d + m + u] * l * (1 - h) + b[d + m + 4 + u] * h * l, _[g++] = f; return y; }, hermiteFastResize: function (t, e, i, r, n) { for (var s = this.rcpScaleX, o = this.rcpScaleY, a = I(s / 2), c = I(o / 2), h = t.imageData.data, l = t.ctx.createImageData(r, n), u = l.data, f = 0; f < n; f++)for (var d = 0; d < r; d++) { for (var g = 4 * (d + f * r), p = 0, v = 0, m = 0, b = 0, y = 0, _ = 0, x = 0, C = (f + .5) * o, S = j(f * o); S < (f + 1) * o; S++)for (var T = F(C - (S + .5)) / c, w = (d + .5) * s, O = T * T, k = j(d * s); k < (d + 1) * s; k++) { var P = F(w - (k + .5)) / a, E = M(O + P * P); 1 < E && E < -1 || 0 < (p = 2 * E * E * E - 3 * E * E + 1) && (x += p * h[(P = 4 * (k + S * e)) + 3], m += p, h[P + 3] < 255 && (p = p * h[P + 3] / 250), b += p * h[P], y += p * h[P + 1], _ += p * h[P + 2], v += p); } u[g] = b / v, u[g + 1] = y / v, u[g + 2] = _ / v, u[g + 3] = x / m; } return l; }, toObject: function () { return { type: this.type, scaleX: this.scaleX, scaleY: this.scaleY, resizeType: this.resizeType, lanczosLobes: this.lanczosLobes }; } }), m.Image.filters.Resize.fromObject = m.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.Contrast = r(i.BaseFilter, { type: "Contrast", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uContrast;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\nfloat contrastF = 1.015 * (uContrast + 1.0) / (1.0 * (1.015 - uContrast));\ncolor.rgb = contrastF * (color.rgb - 0.5) + 0.5;\ngl_FragColor = color;\n}", contrast: 0, mainParameter: "contrast", applyTo2d: function (t) { if (0 !== this.contrast) { var e, i = t.imageData.data, r = i.length, n = Math.floor(255 * this.contrast), s = 259 * (n + 255) / (255 * (259 - n)); for (e = 0; e < r; e += 4)i[e] = s * (i[e] - 128) + 128, i[e + 1] = s * (i[e + 1] - 128) + 128, i[e + 2] = s * (i[e + 2] - 128) + 128; } }, getUniformLocations: function (t, e) { return { uContrast: t.getUniformLocation(e, "uContrast") }; }, sendUniformData: function (t, e) { t.uniform1f(e.uContrast, this.contrast); } }), e.Image.filters.Contrast.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.Saturation = r(i.BaseFilter, { type: "Saturation", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uSaturation;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\nfloat rgMax = max(color.r, color.g);\nfloat rgbMax = max(rgMax, color.b);\ncolor.r += rgbMax != color.r ? (rgbMax - color.r) * uSaturation : 0.00;\ncolor.g += rgbMax != color.g ? (rgbMax - color.g) * uSaturation : 0.00;\ncolor.b += rgbMax != color.b ? (rgbMax - color.b) * uSaturation : 0.00;\ngl_FragColor = color;\n}", saturation: 0, mainParameter: "saturation", applyTo2d: function (t) { if (0 !== this.saturation) { var e, i, r = t.imageData.data, n = r.length, s = -this.saturation; for (e = 0; e < n; e += 4)i = Math.max(r[e], r[e + 1], r[e + 2]), r[e] += i !== r[e] ? (i - r[e]) * s : 0, r[e + 1] += i !== r[e + 1] ? (i - r[e + 1]) * s : 0, r[e + 2] += i !== r[e + 2] ? (i - r[e + 2]) * s : 0; } }, getUniformLocations: function (t, e) { return { uSaturation: t.getUniformLocation(e, "uSaturation") }; }, sendUniformData: function (t, e) { t.uniform1f(e.uSaturation, -this.saturation); } }), e.Image.filters.Saturation.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.Vibrance = r(i.BaseFilter, { type: "Vibrance", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform float uVibrance;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\nfloat max = max(color.r, max(color.g, color.b));\nfloat avg = (color.r + color.g + color.b) / 3.0;\nfloat amt = (abs(max - avg) * 2.0) * uVibrance;\ncolor.r += max != color.r ? (max - color.r) * amt : 0.00;\ncolor.g += max != color.g ? (max - color.g) * amt : 0.00;\ncolor.b += max != color.b ? (max - color.b) * amt : 0.00;\ngl_FragColor = color;\n}", vibrance: 0, mainParameter: "vibrance", applyTo2d: function (t) { if (0 !== this.vibrance) { var e, i, r, n, s = t.imageData.data, o = s.length, a = -this.vibrance; for (e = 0; e < o; e += 4)i = Math.max(s[e], s[e + 1], s[e + 2]), r = (s[e] + s[e + 1] + s[e + 2]) / 3, n = 2 * Math.abs(i - r) / 255 * a, s[e] += i !== s[e] ? (i - s[e]) * n : 0, s[e + 1] += i !== s[e + 1] ? (i - s[e + 1]) * n : 0, s[e + 2] += i !== s[e + 2] ? (i - s[e + 2]) * n : 0; } }, getUniformLocations: function (t, e) { return { uVibrance: t.getUniformLocation(e, "uVibrance") }; }, sendUniformData: function (t, e) { t.uniform1f(e.uVibrance, -this.vibrance); } }), e.Image.filters.Vibrance.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var g = t.fabric || (t.fabric = {}), e = g.Image.filters, i = g.util.createClass; e.Blur = i(e.BaseFilter, { type: "Blur", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform vec2 uDelta;\nvarying vec2 vTexCoord;\nconst float nSamples = 15.0;\nvec3 v3offset = vec3(12.9898, 78.233, 151.7182);\nfloat random(vec3 scale) {\nreturn fract(sin(dot(gl_FragCoord.xyz, scale)) * 43758.5453);\n}\nvoid main() {\nvec4 color = vec4(0.0);\nfloat total = 0.0;\nfloat offset = random(v3offset);\nfor (float t = -nSamples; t <= nSamples; t++) {\nfloat percent = (t + offset - 0.5) / nSamples;\nfloat weight = 1.0 - abs(percent);\ncolor += texture2D(uTexture, vTexCoord + uDelta * percent) * weight;\ntotal += weight;\n}\ngl_FragColor = color / total;\n}", blur: 0, mainParameter: "blur", applyTo: function (t) { t.webgl ? (this.aspectRatio = t.sourceWidth / t.sourceHeight, t.passes++, this._setupFrameBuffer(t), this.horizontal = !0, this.applyToWebGL(t), this._swapTextures(t), this._setupFrameBuffer(t), this.horizontal = !1, this.applyToWebGL(t), this._swapTextures(t)) : this.applyTo2d(t); }, applyTo2d: function (t) { t.imageData = this.simpleBlur(t); }, simpleBlur: function (t) { var e, i, r = t.filterBackend.resources, n = t.imageData.width, s = t.imageData.height; r.blurLayer1 || (r.blurLayer1 = g.util.createCanvasElement(), r.blurLayer2 = g.util.createCanvasElement()), e = r.blurLayer1, i = r.blurLayer2, e.width === n && e.height === s || (i.width = e.width = n, i.height = e.height = s); var o, a, c, h, l = e.getContext("2d"), u = i.getContext("2d"), f = .06 * this.blur * .5; for (l.putImageData(t.imageData, 0, 0), u.clearRect(0, 0, n, s), h = -15; h <= 15; h++)c = f * (a = h / 15) * n + (o = (Math.random() - .5) / 4), u.globalAlpha = 1 - Math.abs(a), u.drawImage(e, c, o), l.drawImage(i, 0, 0), u.globalAlpha = 1, u.clearRect(0, 0, i.width, i.height); for (h = -15; h <= 15; h++)c = f * (a = h / 15) * s + (o = (Math.random() - .5) / 4), u.globalAlpha = 1 - Math.abs(a), u.drawImage(e, o, c), l.drawImage(i, 0, 0), u.globalAlpha = 1, u.clearRect(0, 0, i.width, i.height); t.ctx.drawImage(e, 0, 0); var d = t.ctx.getImageData(0, 0, e.width, e.height); return l.globalAlpha = 1, l.clearRect(0, 0, e.width, e.height), d; }, getUniformLocations: function (t, e) { return { delta: t.getUniformLocation(e, "uDelta") }; }, sendUniformData: function (t, e) { var i = this.chooseRightDelta(); t.uniform2fv(e.delta, i); }, chooseRightDelta: function () { var t, e = 1, i = [0, 0]; return this.horizontal ? 1 < this.aspectRatio && (e = 1 / this.aspectRatio) : this.aspectRatio < 1 && (e = this.aspectRatio), t = e * this.blur * .12, this.horizontal ? i[0] = t : i[1] = t, i; } }), e.Blur.fromObject = g.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass; i.Gamma = r(i.BaseFilter, { type: "Gamma", fragmentSource: "precision highp float;\nuniform sampler2D uTexture;\nuniform vec3 uGamma;\nvarying vec2 vTexCoord;\nvoid main() {\nvec4 color = texture2D(uTexture, vTexCoord);\nvec3 correction = (1.0 / uGamma);\ncolor.r = pow(color.r, correction.r);\ncolor.g = pow(color.g, correction.g);\ncolor.b = pow(color.b, correction.b);\ngl_FragColor = color;\ngl_FragColor.rgb *= color.a;\n}", gamma: [1, 1, 1], mainParameter: "gamma", initialize: function (t) { this.gamma = [1, 1, 1], i.BaseFilter.prototype.initialize.call(this, t); }, applyTo2d: function (t) { var e, i = t.imageData.data, r = this.gamma, n = i.length, s = 1 / r[0], o = 1 / r[1], a = 1 / r[2]; for (this.rVals || (this.rVals = new Uint8Array(256), this.gVals = new Uint8Array(256), this.bVals = new Uint8Array(256)), e = 0, n = 256; e < n; e++)this.rVals[e] = 255 * Math.pow(e / 255, s), this.gVals[e] = 255 * Math.pow(e / 255, o), this.bVals[e] = 255 * Math.pow(e / 255, a); for (e = 0, n = i.length; e < n; e += 4)i[e] = this.rVals[i[e]], i[e + 1] = this.gVals[i[e + 1]], i[e + 2] = this.bVals[i[e + 2]]; }, getUniformLocations: function (t, e) { return { uGamma: t.getUniformLocation(e, "uGamma") }; }, sendUniformData: function (t, e) { t.uniform3fv(e.uGamma, this.gamma); } }), e.Image.filters.Gamma.fromObject = e.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var n = t.fabric || (t.fabric = {}), e = n.Image.filters, i = n.util.createClass; e.Composed = i(e.BaseFilter, { type: "Composed", subFilters: [], initialize: function (t) { this.callSuper("initialize", t), this.subFilters = this.subFilters.slice(0); }, applyTo: function (e) { e.passes += this.subFilters.length - 1, this.subFilters.forEach(function (t) { t.applyTo(e); }); }, toObject: function () { return n.util.object.extend(this.callSuper("toObject"), { subFilters: this.subFilters.map(function (t) { return t.toObject(); }) }); }, isNeutralState: function () { return !this.subFilters.some(function (t) { return !t.isNeutralState(); }); } }), n.Image.filters.Composed.fromObject = function (t, e) { var i = (t.subFilters || []).map(function (t) { return new n.Image.filters[t.type](t); }), r = new n.Image.filters.Composed({ subFilters: i }); return e && e(r), r; }; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var s = t.fabric || (t.fabric = {}), e = s.Image.filters, i = s.util.createClass; e.HueRotation = i(e.ColorMatrix, { type: "HueRotation", rotation: 0, mainParameter: "rotation", calculateMatrix: function () { var t = this.rotation * Math.PI, e = s.util.cos(t), i = s.util.sin(t), r = Math.sqrt(1 / 3) * i, n = 1 - e; this.matrix = [1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0], this.matrix[0] = e + n / 3, this.matrix[1] = 1 / 3 * n - r, this.matrix[2] = 1 / 3 * n + r, this.matrix[5] = 1 / 3 * n + r, this.matrix[6] = e + 1 / 3 * n, this.matrix[7] = 1 / 3 * n - r, this.matrix[10] = 1 / 3 * n - r, this.matrix[11] = 1 / 3 * n + r, this.matrix[12] = e + 1 / 3 * n; }, isNeutralState: function (t) { return this.calculateMatrix(), e.BaseFilter.prototype.isNeutralState.call(this, t); }, applyTo: function (t) { this.calculateMatrix(), e.BaseFilter.prototype.applyTo.call(this, t); } }), s.Image.filters.HueRotation.fromObject = s.Image.filters.BaseFilter.fromObject; }("undefined" != typeof exports ? exports : this), function (t) { "use strict"; var C = t.fabric || (t.fabric = {}), d = C.util.object.clone; if (C.Text) C.warn("fabric.Text is already defined"); else { var r = "fontFamily fontWeight fontSize text underline overline linethrough textAlign fontStyle lineHeight textBackgroundColor charSpacing styles direction path pathStartOffset pathSide pathAlign".split(" "); C.Text = C.util.createClass(C.Object, { _dimensionAffectingProps: ["fontSize", "fontWeight", "fontFamily", "fontStyle", "lineHeight", "text", "charSpacing", "textAlign", "styles", "path", "pathStartOffset", "pathSide", "pathAlign"], _reNewline: /\r?\n/, _reSpacesAndTabs: /[ \t\r]/g, _reSpaceAndTab: /[ \t\r]/, _reWords: /\S+/g, type: "text", fontSize: 40, fontWeight: "normal", fontFamily: "Times New Roman", underline: !1, overline: !1, linethrough: !1, textAlign: "left", fontStyle: "normal", lineHeight: 1.16, superscript: { size: .6, baseline: -.35 }, subscript: { size: .6, baseline: .11 }, textBackgroundColor: "", stateProperties: C.Object.prototype.stateProperties.concat(r), cacheProperties: C.Object.prototype.cacheProperties.concat(r), stroke: null, shadow: null, path: null, pathStartOffset: 0, pathSide: "left", pathAlign: "baseline", _fontSizeFraction: .222, offsets: { underline: .1, linethrough: -.315, overline: -.88 }, _fontSizeMult: 1.13, charSpacing: 0, styles: null, _measuringContext: null, deltaY: 0, direction: "ltr", _styleProperties: ["stroke", "strokeWidth", "fill", "fontFamily", "fontSize", "fontWeight", "fontStyle", "underline", "overline", "linethrough", "deltaY", "textBackgroundColor"], __charBounds: [], CACHE_FONT_SIZE: 400, MIN_TEXT_WIDTH: 2, initialize: function (t, e) { this.styles = e && e.styles || {}, this.text = t, this.__skipDimension = !0, this.callSuper("initialize", e), this.path && this.setPathInfo(), this.__skipDimension = !1, this.initDimensions(), this.setCoords(), this.setupState({ propertySet: "_dimensionAffectingProps" }); }, setPathInfo: function () { var t = this.path; t && (t.segmentsInfo = C.util.getPathSegmentsInfo(t.path)); }, getMeasuringContext: function () { return C._measuringContext || (C._measuringContext = this.canvas && this.canvas.contextCache || C.util.createCanvasElement().getContext("2d")), C._measuringContext; }, _splitText: function () { var t = this._splitTextIntoLines(this.text); return this.textLines = t.lines, this._textLines = t.graphemeLines, this._unwrappedTextLines = t._unwrappedLines, this._text = t.graphemeText, t; }, initDimensions: function () { this.__skipDimension || (this._splitText(), this._clearCache(), this.path ? (this.width = this.path.width, this.height = this.path.height) : (this.width = this.calcTextWidth() || this.cursorWidth || this.MIN_TEXT_WIDTH, this.height = this.calcTextHeight()), -1 !== this.textAlign.indexOf("justify") && this.enlargeSpaces(), this.saveState({ propertySet: "_dimensionAffectingProps" })); }, enlargeSpaces: function () { for (var t, e, i, r, n, s, o, a = 0, c = this._textLines.length; a < c; a++)if (("justify" === this.textAlign || a !== c - 1 && !this.isEndOfWrapping(a)) && (r = 0, n = this._textLines[a], (e = this.getLineWidth(a)) < this.width && (o = this.textLines[a].match(this._reSpacesAndTabs)))) { i = o.length, t = (this.width - e) / i; for (var h = 0, l = n.length; h <= l; h++)s = this.__charBounds[a][h], this._reSpaceAndTab.test(n[h]) ? (s.width += t, s.kernedWidth += t, s.left += r, r += t) : s.left += r; } }, isEndOfWrapping: function (t) { return t === this._textLines.length - 1; }, missingNewlineOffset: function () { return 1; }, toString: function () { return "#<fabric.Text (" + this.complexity() + '): { "text": "' + this.text + '", "fontFamily": "' + this.fontFamily + '" }>'; }, _getCacheCanvasDimensions: function () { var t = this.callSuper("_getCacheCanvasDimensions"), e = this.fontSize; return t.width += e * t.zoomX, t.height += e * t.zoomY, t; }, _render: function (t) { var e = this.path; e && !e.isNotVisible() && e._render(t), this._setTextStyles(t), this._renderTextLinesBackground(t), this._renderTextDecoration(t, "underline"), this._renderText(t), this._renderTextDecoration(t, "overline"), this._renderTextDecoration(t, "linethrough"); }, _renderText: function (t) { "stroke" === this.paintFirst ? (this._renderTextStroke(t), this._renderTextFill(t)) : (this._renderTextFill(t), this._renderTextStroke(t)); }, _setTextStyles: function (t, e, i) { if (t.textBaseline = "alphabetical", this.path) switch (this.pathAlign) { case "center": t.textBaseline = "middle"; break; case "ascender": t.textBaseline = "top"; break; case "descender": t.textBaseline = "bottom"; }t.font = this._getFontDeclaration(e, i); }, calcTextWidth: function () { for (var t = this.getLineWidth(0), e = 1, i = this._textLines.length; e < i; e++) { var r = this.getLineWidth(e); t < r && (t = r); } return t; }, _renderTextLine: function (t, e, i, r, n, s) { this._renderChars(t, e, i, r, n, s); }, _renderTextLinesBackground: function (t) { if (this.textBackgroundColor || this.styleHas("textBackgroundColor")) { for (var e, i, r, n, s, o, a, c = t.fillStyle, h = this._getLeftOffset(), l = this._getTopOffset(), u = 0, f = 0, d = this.path, g = 0, p = this._textLines.length; g < p; g++)if (e = this.getHeightOfLine(g), this.textBackgroundColor || this.styleHas("textBackgroundColor", g)) { r = this._textLines[g], i = this._getLineLeftOffset(g), u = f = 0, n = this.getValueOfPropertyAt(g, 0, "textBackgroundColor"); for (var v = 0, m = r.length; v < m; v++)s = this.__charBounds[g][v], o = this.getValueOfPropertyAt(g, v, "textBackgroundColor"), d ? (t.save(), t.translate(s.renderLeft, s.renderTop), t.rotate(s.angle), (t.fillStyle = o) && t.fillRect(-s.width / 2, -e / this.lineHeight * (1 - this._fontSizeFraction), s.width, e / this.lineHeight), t.restore()) : o !== n ? (a = h + i + u, "rtl" === this.direction && (a = this.width - a - f), (t.fillStyle = n) && t.fillRect(a, l, f, e / this.lineHeight), u = s.left, f = s.width, n = o) : f += s.kernedWidth; o && !d && (a = h + i + u, "rtl" === this.direction && (a = this.width - a - f), t.fillStyle = o, t.fillRect(a, l, f, e / this.lineHeight)), l += e; } else l += e; t.fillStyle = c, this._removeShadow(t); } }, getFontCache: function (t) { var e = t.fontFamily.toLowerCase(); C.charWidthsCache[e] || (C.charWidthsCache[e] = {}); var i = C.charWidthsCache[e], r = t.fontStyle.toLowerCase() + "_" + (t.fontWeight + "").toLowerCase(); return i[r] || (i[r] = {}), i[r]; }, _measureChar: function (t, e, i, r) { var n, s, o, a, c = this.getFontCache(e), h = i + t, l = this._getFontDeclaration(e) === this._getFontDeclaration(r), u = e.fontSize / this.CACHE_FONT_SIZE; if (i && void 0 !== c[i] && (o = c[i]), void 0 !== c[t] && (a = n = c[t]), l && void 0 !== c[h] && (a = (s = c[h]) - o), void 0 === n || void 0 === o || void 0 === s) { var f = this.getMeasuringContext(); this._setTextStyles(f, e, !0); } return void 0 === n && (a = n = f.measureText(t).width, c[t] = n), void 0 === o && l && i && (o = f.measureText(i).width, c[i] = o), l && void 0 === s && (s = f.measureText(h).width, a = (c[h] = s) - o), { width: n * u, kernedWidth: a * u }; }, getHeightOfChar: function (t, e) { return this.getValueOfPropertyAt(t, e, "fontSize"); }, measureLine: function (t) { var e = this._measureLine(t); return 0 !== this.charSpacing && (e.width -= this._getWidthOfCharSpacing()), e.width < 0 && (e.width = 0), e; }, _measureLine: function (t) { var e, i, r, n, s, o, a = 0, c = this._textLines[t], h = new Array(c.length), l = 0, u = this.path, f = "right" === this.pathSide; for (this.__charBounds[t] = h, e = 0; e < c.length; e++)i = c[e], n = this._getGraphemeBox(i, t, e, r), a += (h[e] = n).kernedWidth, r = i; if (h[e] = { left: n ? n.left + n.width : 0, width: 0, kernedWidth: 0, height: this.fontSize }, u) { switch (o = u.segmentsInfo[u.segmentsInfo.length - 1].length, (s = C.util.getPointOnPath(u.path, 0, u.segmentsInfo)).x += u.pathOffset.x, s.y += u.pathOffset.y, this.textAlign) { case "left": l = f ? o - a : 0; break; case "center": l = (o - a) / 2; break; case "right": l = f ? 0 : o - a; }for (l += this.pathStartOffset * (f ? -1 : 1), e = f ? c.length - 1 : 0; f ? 0 <= e : e < c.length; f ? e-- : e++)n = h[e], o < l ? l %= o : l < 0 && (l += o), this._setGraphemeOnPath(l, n, s), l += n.kernedWidth; } return { width: a, numOfSpaces: 0 }; }, _setGraphemeOnPath: function (t, e, i) { var r = t + e.kernedWidth / 2, n = this.path, s = C.util.getPointOnPath(n.path, r, n.segmentsInfo); e.renderLeft = s.x - i.x, e.renderTop = s.y - i.y, e.angle = s.angle + ("right" === this.pathSide ? Math.PI : 0); }, _getGraphemeBox: function (t, e, i, r, n) { var s, o = this.getCompleteStyleDeclaration(e, i), a = r ? this.getCompleteStyleDeclaration(e, i - 1) : {}, c = this._measureChar(t, o, r, a), h = c.kernedWidth, l = c.width; 0 !== this.charSpacing && (l += s = this._getWidthOfCharSpacing(), h += s); var u = { width: l, left: 0, height: o.fontSize, kernedWidth: h, deltaY: o.deltaY }; if (0 < i && !n) { var f = this.__charBounds[e][i - 1]; u.left = f.left + f.width + c.kernedWidth - c.width; } return u; }, getHeightOfLine: function (t) { if (this.__lineHeights[t]) return this.__lineHeights[t]; for (var e = this._textLines[t], i = this.getHeightOfChar(t, 0), r = 1, n = e.length; r < n; r++)i = Math.max(this.getHeightOfChar(t, r), i); return this.__lineHeights[t] = i * this.lineHeight * this._fontSizeMult; }, calcTextHeight: function () { for (var t, e = 0, i = 0, r = this._textLines.length; i < r; i++)t = this.getHeightOfLine(i), e += i === r - 1 ? t / this.lineHeight : t; return e; }, _getLeftOffset: function () { return "ltr" === this.direction ? -this.width / 2 : this.width / 2; }, _getTopOffset: function () { return -this.height / 2; }, _renderTextCommon: function (t, e) { t.save(); for (var i = 0, r = this._getLeftOffset(), n = this._getTopOffset(), s = 0, o = this._textLines.length; s < o; s++) { var a = this.getHeightOfLine(s), c = a / this.lineHeight, h = this._getLineLeftOffset(s); this._renderTextLine(e, t, this._textLines[s], r + h, n + i + c, s), i += a; } t.restore(); }, _renderTextFill: function (t) { (this.fill || this.styleHas("fill")) && this._renderTextCommon(t, "fillText"); }, _renderTextStroke: function (t) { (this.stroke && 0 !== this.strokeWidth || !this.isEmptyStyles()) && (this.shadow && !this.shadow.affectStroke && this._removeShadow(t), t.save(), this._setLineDash(t, this.strokeDashArray), t.beginPath(), this._renderTextCommon(t, "strokeText"), t.closePath(), t.restore()); }, _renderChars: function (t, e, i, r, n, s) { var o, a, c, h, l, u = this.getHeightOfLine(s), f = -1 !== this.textAlign.indexOf("justify"), d = "", g = 0, p = this.path, v = !f && 0 === this.charSpacing && this.isEmptyStyles(s) && !p, m = "ltr" === this.direction, b = "ltr" === this.direction ? 1 : -1, y = e.canvas.getAttribute("dir"); if (e.save(), y !== this.direction && (e.canvas.setAttribute("dir", m ? "ltr" : "rtl"), e.direction = m ? "ltr" : "rtl", e.textAlign = m ? "left" : "right"), n -= u * this._fontSizeFraction / this.lineHeight, v) return this._renderChar(t, e, s, 0, i.join(""), r, n, u), void e.restore(); for (var _ = 0, x = i.length - 1; _ <= x; _++)h = _ === x || this.charSpacing || p, d += i[_], c = this.__charBounds[s][_], 0 === g ? (r += b * (c.kernedWidth - c.width), g += c.width) : g += c.kernedWidth, f && !h && this._reSpaceAndTab.test(i[_]) && (h = !0), h || (o = o || this.getCompleteStyleDeclaration(s, _), a = this.getCompleteStyleDeclaration(s, _ + 1), h = C.util.hasStyleChanged(o, a, !1)), h && (p ? (e.save(), e.translate(c.renderLeft, c.renderTop), e.rotate(c.angle), this._renderChar(t, e, s, _, d, -g / 2, 0, u), e.restore()) : (l = r, this._renderChar(t, e, s, _, d, l, n, u)), d = "", o = a, r += b * g, g = 0); e.restore(); }, _applyPatternGradientTransformText: function (t) { var e, i = C.util.createCanvasElement(), r = this.width + this.strokeWidth, n = this.height + this.strokeWidth; return i.width = r, i.height = n, (e = i.getContext("2d")).beginPath(), e.moveTo(0, 0), e.lineTo(r, 0), e.lineTo(r, n), e.lineTo(0, n), e.closePath(), e.translate(r / 2, n / 2), e.fillStyle = t.toLive(e), this._applyPatternGradientTransform(e, t), e.fill(), e.createPattern(i, "no-repeat"); }, handleFiller: function (t, e, i) { var r, n; return i.toLive ? "percentage" === i.gradientUnits || i.gradientTransform || i.patternTransform ? (r = -this.width / 2, n = -this.height / 2, t.translate(r, n), t[e] = this._applyPatternGradientTransformText(i), { offsetX: r, offsetY: n }) : (t[e] = i.toLive(t, this), this._applyPatternGradientTransform(t, i)) : (t[e] = i, { offsetX: 0, offsetY: 0 }); }, _setStrokeStyles: function (t, e) { return t.lineWidth = e.strokeWidth, t.lineCap = this.strokeLineCap, t.lineDashOffset = this.strokeDashOffset, t.lineJoin = this.strokeLineJoin, t.miterLimit = this.strokeMiterLimit, this.handleFiller(t, "strokeStyle", e.stroke); }, _setFillStyles: function (t, e) { return this.handleFiller(t, "fillStyle", e.fill); }, _renderChar: function (t, e, i, r, n, s, o) { var a, c, h = this._getStyleDeclaration(i, r), l = this.getCompleteStyleDeclaration(i, r), u = "fillText" === t && l.fill, f = "strokeText" === t && l.stroke && l.strokeWidth; (f || u) && (e.save(), u && (a = this._setFillStyles(e, l)), f && (c = this._setStrokeStyles(e, l)), e.font = this._getFontDeclaration(l), h && h.textBackgroundColor && this._removeShadow(e), h && h.deltaY && (o += h.deltaY), u && e.fillText(n, s - a.offsetX, o - a.offsetY), f && e.strokeText(n, s - c.offsetX, o - c.offsetY), e.restore()); }, setSuperscript: function (t, e) { return this._setScript(t, e, this.superscript); }, setSubscript: function (t, e) { return this._setScript(t, e, this.subscript); }, _setScript: function (t, e, i) { var r = this.get2DCursorLocation(t, !0), n = this.getValueOfPropertyAt(r.lineIndex, r.charIndex, "fontSize"), s = this.getValueOfPropertyAt(r.lineIndex, r.charIndex, "deltaY"), o = { fontSize: n * i.size, deltaY: s + n * i.baseline }; return this.setSelectionStyles(o, t, e), this; }, _getLineLeftOffset: function (t) { var e = this.getLineWidth(t), i = this.width - e, r = this.textAlign, n = this.direction, s = 0, o = this.isEndOfWrapping(t); return "justify" === r || "justify-center" === r && !o || "justify-right" === r && !o || "justify-left" === r && !o ? 0 : ("center" === r && (s = i / 2), "right" === r && (s = i), "justify-center" === r && (s = i / 2), "justify-right" === r && (s = i), "rtl" === n && (s -= i), s); }, _clearCache: function () { this.__lineWidths = [], this.__lineHeights = [], this.__charBounds = []; }, _shouldClearDimensionCache: function () { var t = this._forceClearCache; return t || (t = this.hasStateChanged("_dimensionAffectingProps")), t && (this.dirty = !0, this._forceClearCache = !1), t; }, getLineWidth: function (t) { if (void 0 !== this.__lineWidths[t]) return this.__lineWidths[t]; var e = this.measureLine(t).width; return this.__lineWidths[t] = e; }, _getWidthOfCharSpacing: function () { return 0 !== this.charSpacing ? this.fontSize * this.charSpacing / 1e3 : 0; }, getValueOfPropertyAt: function (t, e, i) { var r = this._getStyleDeclaration(t, e); return r && void 0 !== r[i] ? r[i] : this[i]; }, _renderTextDecoration: function (t, e) { if (this[e] || this.styleHas(e)) { for (var i, r, n, s, o, a, c, h, l, u, f, d, g, p, v, m, b = this._getLeftOffset(), y = this._getTopOffset(), _ = this.path, x = this._getWidthOfCharSpacing(), C = this.offsets[e], S = 0, T = this._textLines.length; S < T; S++)if (i = this.getHeightOfLine(S), this[e] || this.styleHas(e, S)) { c = this._textLines[S], p = i / this.lineHeight, s = this._getLineLeftOffset(S), f = u = 0, h = this.getValueOfPropertyAt(S, 0, e), m = this.getValueOfPropertyAt(S, 0, "fill"), l = y + p * (1 - this._fontSizeFraction), r = this.getHeightOfChar(S, 0), o = this.getValueOfPropertyAt(S, 0, "deltaY"); for (var w = 0, O = c.length; w < O; w++)if (d = this.__charBounds[S][w], g = this.getValueOfPropertyAt(S, w, e), v = this.getValueOfPropertyAt(S, w, "fill"), n = this.getHeightOfChar(S, w), a = this.getValueOfPropertyAt(S, w, "deltaY"), _ && g && v) t.save(), t.fillStyle = m, t.translate(d.renderLeft, d.renderTop), t.rotate(d.angle), t.fillRect(-d.kernedWidth / 2, C * n + a, d.kernedWidth, this.fontSize / 15), t.restore(); else if ((g !== h || v !== m || n !== r || a !== o) && 0 < f) { var k = b + s + u; "rtl" === this.direction && (k = this.width - k - f), h && m && (t.fillStyle = m, t.fillRect(k, l + C * r + o, f, this.fontSize / 15)), u = d.left, f = d.width, h = g, m = v, r = n, o = a; } else f += d.kernedWidth; k = b + s + u; "rtl" === this.direction && (k = this.width - k - f), t.fillStyle = v, g && v && t.fillRect(k, l + C * r + o, f - x, this.fontSize / 15), y += i; } else y += i; this._removeShadow(t); } }, _getFontDeclaration: function (t, e) { var i = t || this, r = this.fontFamily, n = -1 < C.Text.genericFonts.indexOf(r.toLowerCase()), s = void 0 === r || -1 < r.indexOf("'") || -1 < r.indexOf(",") || -1 < r.indexOf('"') || n ? i.fontFamily : '"' + i.fontFamily + '"'; return [C.isLikelyNode ? i.fontWeight : i.fontStyle, C.isLikelyNode ? i.fontStyle : i.fontWeight, e ? this.CACHE_FONT_SIZE + "px" : i.fontSize + "px", s].join(" "); }, render: function (t) { this.visible && (this.canvas && this.canvas.skipOffscreen && !this.group && !this.isOnScreen() || (this._shouldClearDimensionCache() && this.initDimensions(), this.callSuper("render", t))); }, _splitTextIntoLines: function (t) { for (var e = t.split(this._reNewline), i = new Array(e.length), r = ["\n"], n = [], s = 0; s < e.length; s++)i[s] = C.util.string.graphemeSplit(e[s]), n = n.concat(i[s], r); return n.pop(), { _unwrappedLines: i, lines: e, graphemeText: n, graphemeLines: i }; }, toObject: function (t) { var e = r.concat(t), i = this.callSuper("toObject", e); return i.styles = C.util.stylesToArray(this.styles, this.text), i.path && (i.path = this.path.toObject()), i; }, set: function (t, e) { this.callSuper("set", t, e); var i = !1, r = !1; if ("object" == typeof t) for (var n in t) "path" === n && this.setPathInfo(), i = i || -1 !== this._dimensionAffectingProps.indexOf(n), r = r || "path" === n; else i = -1 !== this._dimensionAffectingProps.indexOf(t), r = "path" === t; return r && this.setPathInfo(), i && (this.initDimensions(), this.setCoords()), this; }, complexity: function () { return 1; } }), C.Text.ATTRIBUTE_NAMES = C.SHARED_ATTRIBUTES.concat("x y dx dy font-family font-style font-weight font-size letter-spacing text-decoration text-anchor".split(" ")), C.Text.DEFAULT_SVG_FONT_SIZE = 16, C.Text.fromElement = function (t, e, i) { if (!t) return e(null); var r = C.parseAttributes(t, C.Text.ATTRIBUTE_NAMES), n = r.textAnchor || "left"; if ((i = C.util.object.extend(i ? d(i) : {}, r)).top = i.top || 0, i.left = i.left || 0, r.textDecoration) { var s = r.textDecoration; -1 !== s.indexOf("underline") && (i.underline = !0), -1 !== s.indexOf("overline") && (i.overline = !0), -1 !== s.indexOf("line-through") && (i.linethrough = !0), delete i.textDecoration; } "dx" in r && (i.left += r.dx), "dy" in r && (i.top += r.dy), "fontSize" in i || (i.fontSize = C.Text.DEFAULT_SVG_FONT_SIZE); var o = ""; "textContent" in t ? o = t.textContent : "firstChild" in t && null !== t.firstChild && "data" in t.firstChild && null !== t.firstChild.data && (o = t.firstChild.data), o = o.replace(/^\s+|\s+$|\n+/g, "").replace(/\s+/g, " "); var a = i.strokeWidth; i.strokeWidth = 0; var c = new C.Text(o, i), h = c.getScaledHeight() / c.height, l = ((c.height + c.strokeWidth) * c.lineHeight - c.height) * h, u = c.getScaledHeight() + l, f = 0; "center" === n && (f = c.getScaledWidth() / 2), "right" === n && (f = c.getScaledWidth()), c.set({ left: c.left - f, top: c.top - (u - c.fontSize * (.07 + c._fontSizeFraction)) / c.lineHeight, strokeWidth: void 0 !== a ? a : 1 }), e(c); }, C.Text.fromObject = function (t, i) { var e = d(t), r = t.path; return delete e.path, C.Object._fromObject("Text", e, function (e) { e.styles = C.util.stylesFromArray(t.styles, t.text), r ? C.Object._fromObject("Path", r, function (t) { e.set("path", t), i(e); }, "path") : i(e); }, "text"); }, C.Text.genericFonts = ["sans-serif", "serif", "cursive", "fantasy", "monospace"], C.util.createAccessors && C.util.createAccessors(C.Text); } }("undefined" != typeof exports ? exports : this), fabric.util.object.extend(fabric.Text.prototype, { isEmptyStyles: function (t) { if (!this.styles) return !0; if (void 0 !== t && !this.styles[t]) return !0; var e = void 0 === t ? this.styles : { line: this.styles[t] }; for (var i in e) for (var r in e[i]) for (var n in e[i][r]) return !1; return !0; }, styleHas: function (t, e) { if (!this.styles || !t || "" === t) return !1; if (void 0 !== e && !this.styles[e]) return !1; var i = void 0 === e ? this.styles : { 0: this.styles[e] }; for (var r in i) for (var n in i[r]) if (void 0 !== i[r][n][t]) return !0; return !1; }, cleanStyle: function (t) { if (!this.styles || !t || "" === t) return !1; var e, i, r = this.styles, n = 0, s = !0, o = 0; for (var a in r) { for (var c in e = 0, r[a]) { var h; n++, (h = r[a][c]).hasOwnProperty(t) ? (i ? h[t] !== i && (s = !1) : i = h[t], h[t] === this[t] && delete h[t]) : s = !1, 0 !== Object.keys(h).length ? e++ : delete r[a][c]; } 0 === e && delete r[a]; } for (var l = 0; l < this._textLines.length; l++)o += this._textLines[l].length; s && n === o && (this[t] = i, this.removeStyle(t)); }, removeStyle: function (t) { if (this.styles && t && "" !== t) { var e, i, r, n = this.styles; for (i in n) { for (r in e = n[i]) delete e[r][t], 0 === Object.keys(e[r]).length && delete e[r]; 0 === Object.keys(e).length && delete n[i]; } } }, _extendStyles: function (t, e) { var i = this.get2DCursorLocation(t); this._getLineStyle(i.lineIndex) || this._setLineStyle(i.lineIndex), this._getStyleDeclaration(i.lineIndex, i.charIndex) || this._setStyleDeclaration(i.lineIndex, i.charIndex, {}), fabric.util.object.extend(this._getStyleDeclaration(i.lineIndex, i.charIndex), e); }, get2DCursorLocation: function (t, e) { void 0 === t && (t = this.selectionStart); for (var i = e ? this._unwrappedTextLines : this._textLines, r = i.length, n = 0; n < r; n++) { if (t <= i[n].length) return { lineIndex: n, charIndex: t }; t -= i[n].length + this.missingNewlineOffset(n); } return { lineIndex: n - 1, charIndex: i[n - 1].length < t ? i[n - 1].length : t }; }, getSelectionStyles: function (t, e, i) { void 0 === t && (t = this.selectionStart || 0), void 0 === e && (e = this.selectionEnd || t); for (var r = [], n = t; n < e; n++)r.push(this.getStyleAtPosition(n, i)); return r; }, getStyleAtPosition: function (t, e) { var i = this.get2DCursorLocation(t); return (e ? this.getCompleteStyleDeclaration(i.lineIndex, i.charIndex) : this._getStyleDeclaration(i.lineIndex, i.charIndex)) || {}; }, setSelectionStyles: function (t, e, i) { void 0 === e && (e = this.selectionStart || 0), void 0 === i && (i = this.selectionEnd || e); for (var r = e; r < i; r++)this._extendStyles(r, t); return this._forceClearCache = !0, this; }, _getStyleDeclaration: function (t, e) { var i = this.styles && this.styles[t]; return i ? i[e] : null; }, getCompleteStyleDeclaration: function (t, e) { for (var i, r = this._getStyleDeclaration(t, e) || {}, n = {}, s = 0; s < this._styleProperties.length; s++)n[i = this._styleProperties[s]] = void 0 === r[i] ? this[i] : r[i]; return n; }, _setStyleDeclaration: function (t, e, i) { this.styles[t][e] = i; }, _deleteStyleDeclaration: function (t, e) { delete this.styles[t][e]; }, _getLineStyle: function (t) { return !!this.styles[t]; }, _setLineStyle: function (t) { this.styles[t] = {}; }, _deleteLineStyle: function (t) { delete this.styles[t]; } }), function () { function o(t) { t.textDecoration && (-1 < t.textDecoration.indexOf("underline") && (t.underline = !0), -1 < t.textDecoration.indexOf("line-through") && (t.linethrough = !0), -1 < t.textDecoration.indexOf("overline") && (t.overline = !0), delete t.textDecoration); } fabric.IText = fabric.util.createClass(fabric.Text, fabric.Observable, { type: "i-text", selectionStart: 0, selectionEnd: 0, selectionColor: "rgba(17,119,255,0.3)", isEditing: !1, editable: !0, editingBorderColor: "rgba(102,153,255,0.25)", cursorWidth: 2, cursorColor: "", cursorDelay: 1e3, cursorDuration: 600, caching: !0, hiddenTextareaContainer: null, _reSpace: /\s|\n/, _currentCursorOpacity: 0, _selectionDirection: null, _abortCursorAnimation: !1, __widthOfSpace: [], inCompositionMode: !1, initialize: function (t, e) { this.callSuper("initialize", t, e), this.initBehavior(); }, setSelectionStart: function (t) { t = Math.max(t, 0), this._updateAndFire("selectionStart", t); }, setSelectionEnd: function (t) { t = Math.min(t, this.text.length), this._updateAndFire("selectionEnd", t); }, _updateAndFire: function (t, e) { this[t] !== e && (this._fireSelectionChanged(), this[t] = e), this._updateTextarea(); }, _fireSelectionChanged: function () { this.fire("selection:changed"), this.canvas && this.canvas.fire("text:selection:changed", { target: this }); }, initDimensions: function () { this.isEditing && this.initDelayedCursor(), this.clearContextTop(), this.callSuper("initDimensions"); }, render: function (t) { this.clearContextTop(), this.callSuper("render", t), this.cursorOffsetCache = {}, this.renderCursorOrSelection(); }, _render: function (t) { this.callSuper("_render", t); }, clearContextTop: function (t) { if (this.isEditing && this.canvas && this.canvas.contextTop) { var e = this.canvas.contextTop, i = this.canvas.viewportTransform; e.save(), e.transform(i[0], i[1], i[2], i[3], i[4], i[5]), this.transform(e), this._clearTextArea(e), t || e.restore(); } }, renderCursorOrSelection: function () { if (this.isEditing && this.canvas && this.canvas.contextTop) { var t = this._getCursorBoundaries(), e = this.canvas.contextTop; this.clearContextTop(!0), this.selectionStart === this.selectionEnd ? this.renderCursor(t, e) : this.renderSelection(t, e), e.restore(); } }, _clearTextArea: function (t) { var e = this.width + 4, i = this.height + 4; t.clearRect(-e / 2, -i / 2, e, i); }, _getCursorBoundaries: function (t) { void 0 === t && (t = this.selectionStart); var e = this._getLeftOffset(), i = this._getTopOffset(), r = this._getCursorBoundariesOffsets(t); return { left: e, top: i, leftOffset: r.left, topOffset: r.top }; }, _getCursorBoundariesOffsets: function (t) { if (this.cursorOffsetCache && "top" in this.cursorOffsetCache) return this.cursorOffsetCache; var e, i, r, n, s = 0, o = 0, a = this.get2DCursorLocation(t); r = a.charIndex, i = a.lineIndex; for (var c = 0; c < i; c++)s += this.getHeightOfLine(c); e = this._getLineLeftOffset(i); var h = this.__charBounds[i][r]; return h && (o = h.left), 0 !== this.charSpacing && r === this._textLines[i].length && (o -= this._getWidthOfCharSpacing()), n = { top: s, left: e + (0 < o ? o : 0) }, "rtl" === this.direction && (n.left *= -1), this.cursorOffsetCache = n, this.cursorOffsetCache; }, renderCursor: function (t, e) { var i = this.get2DCursorLocation(), r = i.lineIndex, n = 0 < i.charIndex ? i.charIndex - 1 : 0, s = this.getValueOfPropertyAt(r, n, "fontSize"), o = this.scaleX * this.canvas.getZoom(), a = this.cursorWidth / o, c = t.topOffset, h = this.getValueOfPropertyAt(r, n, "deltaY"); c += (1 - this._fontSizeFraction) * this.getHeightOfLine(r) / this.lineHeight - s * (1 - this._fontSizeFraction), this.inCompositionMode && this.renderSelection(t, e), e.fillStyle = this.cursorColor || this.getValueOfPropertyAt(r, n, "fill"), e.globalAlpha = this.__isMousedown ? 1 : this._currentCursorOpacity, e.fillRect(t.left + t.leftOffset - a / 2, c + t.top + h, a, s); }, renderSelection: function (t, e) { for (var i = this.inCompositionMode ? this.hiddenTextarea.selectionStart : this.selectionStart, r = this.inCompositionMode ? this.hiddenTextarea.selectionEnd : this.selectionEnd, n = -1 !== this.textAlign.indexOf("justify"), s = this.get2DCursorLocation(i), o = this.get2DCursorLocation(r), a = s.lineIndex, c = o.lineIndex, h = s.charIndex < 0 ? 0 : s.charIndex, l = o.charIndex < 0 ? 0 : o.charIndex, u = a; u <= c; u++) { var f, d = this._getLineLeftOffset(u) || 0, g = this.getHeightOfLine(u), p = 0, v = 0; if (u === a && (p = this.__charBounds[a][h].left), a <= u && u < c) v = n && !this.isEndOfWrapping(u) ? this.width : this.getLineWidth(u) || 5; else if (u === c) if (0 === l) v = this.__charBounds[c][l].left; else { var m = this._getWidthOfCharSpacing(); v = this.__charBounds[c][l - 1].left + this.__charBounds[c][l - 1].width - m; } f = g, (this.lineHeight < 1 || u === c && 1 < this.lineHeight) && (g /= this.lineHeight); var b = t.left + d + p, y = v - p, _ = g, x = 0; this.inCompositionMode ? (e.fillStyle = this.compositionColor || "black", _ = 1, x = g) : e.fillStyle = this.selectionColor, "rtl" === this.direction && (b = this.width - b - y), e.fillRect(b, t.top + t.topOffset + x, y, _), t.topOffset += f; } }, getCurrentCharFontSize: function () { var t = this._getCurrentCharIndex(); return this.getValueOfPropertyAt(t.l, t.c, "fontSize"); }, getCurrentCharColor: function () { var t = this._getCurrentCharIndex(); return this.getValueOfPropertyAt(t.l, t.c, "fill"); }, _getCurrentCharIndex: function () { var t = this.get2DCursorLocation(this.selectionStart, !0), e = 0 < t.charIndex ? t.charIndex - 1 : 0; return { l: t.lineIndex, c: e }; } }), fabric.IText.fromObject = function (t, e) { var i = fabric.util.stylesFromArray(t.styles, t.text), r = Object.assign({}, t, { styles: i }); if (o(r), r.styles) for (var n in r.styles) for (var s in r.styles[n]) o(r.styles[n][s]); fabric.Object._fromObject("IText", r, e, "text"); }; }(), function () { var u = fabric.util.object.clone; fabric.util.object.extend(fabric.IText.prototype, { initBehavior: function () { this.initAddedHandler(), this.initRemovedHandler(), this.initCursorSelectionHandlers(), this.initDoubleClickSimulation(), this.mouseMoveHandler = this.mouseMoveHandler.bind(this); }, onDeselect: function () { this.isEditing && this.exitEditing(), this.selected = !1; }, initAddedHandler: function () { var e = this; this.on("added", function () { var t = e.canvas; t && (t._hasITextHandlers || (t._hasITextHandlers = !0, e._initCanvasHandlers(t)), t._iTextInstances = t._iTextInstances || [], t._iTextInstances.push(e)); }); }, initRemovedHandler: function () { var e = this; this.on("removed", function () { var t = e.canvas; t && (t._iTextInstances = t._iTextInstances || [], fabric.util.removeFromArray(t._iTextInstances, e), 0 === t._iTextInstances.length && (t._hasITextHandlers = !1, e._removeCanvasHandlers(t))); }); }, _initCanvasHandlers: function (t) { t._mouseUpITextHandler = function () { t._iTextInstances && t._iTextInstances.forEach(function (t) { t.__isMousedown = !1; }); }, t.on("mouse:up", t._mouseUpITextHandler); }, _removeCanvasHandlers: function (t) { t.off("mouse:up", t._mouseUpITextHandler); }, _tick: function () { this._currentTickState = this._animateCursor(this, 1, this.cursorDuration, "_onTickComplete"); }, _animateCursor: function (t, e, i, r) { var n; return n = { isAborted: !1, abort: function () { this.isAborted = !0; } }, t.animate("_currentCursorOpacity", e, { duration: i, onComplete: function () { n.isAborted || t[r](); }, onChange: function () { t.canvas && t.selectionStart === t.selectionEnd && t.renderCursorOrSelection(); }, abort: function () { return n.isAborted; } }), n; }, _onTickComplete: function () { var t = this; this._cursorTimeout1 && clearTimeout(this._cursorTimeout1), this._cursorTimeout1 = setTimeout(function () { t._currentTickCompleteState = t._animateCursor(t, 0, this.cursorDuration / 2, "_tick"); }, 100); }, initDelayedCursor: function (t) { var e = this, i = t ? 0 : this.cursorDelay; this.abortCursorAnimation(), this._currentCursorOpacity = 1, this._cursorTimeout2 = setTimeout(function () { e._tick(); }, i); }, abortCursorAnimation: function () { var t = this._currentTickState || this._currentTickCompleteState, e = this.canvas; this._currentTickState && this._currentTickState.abort(), this._currentTickCompleteState && this._currentTickCompleteState.abort(), clearTimeout(this._cursorTimeout1), clearTimeout(this._cursorTimeout2), this._currentCursorOpacity = 0, t && e && e.clearContext(e.contextTop || e.contextContainer); }, selectAll: function () { return this.selectionStart = 0, this.selectionEnd = this._text.length, this._fireSelectionChanged(), this._updateTextarea(), this; }, getSelectedText: function () { return this._text.slice(this.selectionStart, this.selectionEnd).join(""); }, findWordBoundaryLeft: function (t) { var e = 0, i = t - 1; if (this._reSpace.test(this._text[i])) for (; this._reSpace.test(this._text[i]);)e++, i--; for (; /\S/.test(this._text[i]) && -1 < i;)e++, i--; return t - e; }, findWordBoundaryRight: function (t) { var e = 0, i = t; if (this._reSpace.test(this._text[i])) for (; this._reSpace.test(this._text[i]);)e++, i++; for (; /\S/.test(this._text[i]) && i < this._text.length;)e++, i++; return t + e; }, findLineBoundaryLeft: function (t) { for (var e = 0, i = t - 1; !/\n/.test(this._text[i]) && -1 < i;)e++, i--; return t - e; }, findLineBoundaryRight: function (t) { for (var e = 0, i = t; !/\n/.test(this._text[i]) && i < this._text.length;)e++, i++; return t + e; }, searchWordBoundary: function (t, e) { for (var i = this._text, r = this._reSpace.test(i[t]) ? t - 1 : t, n = i[r], s = fabric.reNonWord; !s.test(n) && 0 < r && r < i.length;)n = i[r += e]; return s.test(n) && (r += 1 === e ? 0 : 1), r; }, selectWord: function (t) { t = t || this.selectionStart; var e = this.searchWordBoundary(t, -1), i = this.searchWordBoundary(t, 1); this.selectionStart = e, this.selectionEnd = i, this._fireSelectionChanged(), this._updateTextarea(), this.renderCursorOrSelection(); }, selectLine: function (t) { t = t || this.selectionStart; var e = this.findLineBoundaryLeft(t), i = this.findLineBoundaryRight(t); return this.selectionStart = e, this.selectionEnd = i, this._fireSelectionChanged(), this._updateTextarea(), this; }, enterEditing: function (t) { if (!this.isEditing && this.editable) return this.canvas && (this.canvas.calcOffset(), this.exitEditingOnOthers(this.canvas)), this.isEditing = !0, this.initHiddenTextarea(t), this.hiddenTextarea.focus(), this.hiddenTextarea.value = this.text, this._updateTextarea(), this._saveEditingProps(), this._setEditingProps(), this._textBeforeEdit = this.text, this._tick(), this.fire("editing:entered"), this._fireSelectionChanged(), this.canvas && (this.canvas.fire("text:editing:entered", { target: this }), this.initMouseMoveHandler(), this.canvas.requestRenderAll()), this; }, exitEditingOnOthers: function (t) { t._iTextInstances && t._iTextInstances.forEach(function (t) { t.selected = !1, t.isEditing && t.exitEditing(); }); }, initMouseMoveHandler: function () { this.canvas.on("mouse:move", this.mouseMoveHandler); }, mouseMoveHandler: function (t) { if (this.__isMousedown && this.isEditing) { document.activeElement !== this.hiddenTextarea && this.hiddenTextarea.focus(); var e = this.getSelectionStartFromPointer(t.e), i = this.selectionStart, r = this.selectionEnd; (e === this.__selectionStartOnMouseDown && i !== r || i !== e && r !== e) && (e > this.__selectionStartOnMouseDown ? (this.selectionStart = this.__selectionStartOnMouseDown, this.selectionEnd = e) : (this.selectionStart = e, this.selectionEnd = this.__selectionStartOnMouseDown), this.selectionStart === i && this.selectionEnd === r || (this.restartCursorIfNeeded(), this._fireSelectionChanged(), this._updateTextarea(), this.renderCursorOrSelection())); } }, _setEditingProps: function () { this.hoverCursor = "text", this.canvas && (this.canvas.defaultCursor = this.canvas.moveCursor = "text"), this.borderColor = this.editingBorderColor, this.hasControls = this.selectable = !1, this.lockMovementX = this.lockMovementY = !0; }, fromStringToGraphemeSelection: function (t, e, i) { var r = i.slice(0, t), n = fabric.util.string.graphemeSplit(r).length; if (t === e) return { selectionStart: n, selectionEnd: n }; var s = i.slice(t, e); return { selectionStart: n, selectionEnd: n + fabric.util.string.graphemeSplit(s).length }; }, fromGraphemeToStringSelection: function (t, e, i) { var r = i.slice(0, t).join("").length; return t === e ? { selectionStart: r, selectionEnd: r } : { selectionStart: r, selectionEnd: r + i.slice(t, e).join("").length }; }, _updateTextarea: function () { if (this.cursorOffsetCache = {}, this.hiddenTextarea) { if (!this.inCompositionMode) { var t = this.fromGraphemeToStringSelection(this.selectionStart, this.selectionEnd, this._text); this.hiddenTextarea.selectionStart = t.selectionStart, this.hiddenTextarea.selectionEnd = t.selectionEnd; } this.updateTextareaPosition(); } }, updateFromTextArea: function () { if (this.hiddenTextarea) { this.cursorOffsetCache = {}, this.text = this.hiddenTextarea.value, this._shouldClearDimensionCache() && (this.initDimensions(), this.setCoords()); var t = this.fromStringToGraphemeSelection(this.hiddenTextarea.selectionStart, this.hiddenTextarea.selectionEnd, this.hiddenTextarea.value); this.selectionEnd = this.selectionStart = t.selectionEnd, this.inCompositionMode || (this.selectionStart = t.selectionStart), this.updateTextareaPosition(); } }, updateTextareaPosition: function () { if (this.selectionStart === this.selectionEnd) { var t = this._calcTextareaPosition(); this.hiddenTextarea.style.left = t.left, this.hiddenTextarea.style.top = t.top; } }, _calcTextareaPosition: function () { if (!this.canvas) return { x: 1, y: 1 }; var t = this.inCompositionMode ? this.compositionStart : this.selectionStart, e = this._getCursorBoundaries(t), i = this.get2DCursorLocation(t), r = i.lineIndex, n = i.charIndex, s = this.getValueOfPropertyAt(r, n, "fontSize") * this.lineHeight, o = e.leftOffset, a = this.calcTransformMatrix(), c = { x: e.left + o, y: e.top + e.topOffset + s }, h = this.canvas.getRetinaScaling(), l = this.canvas.upperCanvasEl, u = l.width / h, f = l.height / h, d = u - s, g = f - s, p = l.clientWidth / u, v = l.clientHeight / f; return c = fabric.util.transformPoint(c, a), (c = fabric.util.transformPoint(c, this.canvas.viewportTransform)).x *= p, c.y *= v, c.x < 0 && (c.x = 0), c.x > d && (c.x = d), c.y < 0 && (c.y = 0), c.y > g && (c.y = g), c.x += this.canvas._offset.left, c.y += this.canvas._offset.top, { left: c.x + "px", top: c.y + "px", fontSize: s + "px", charHeight: s }; }, _saveEditingProps: function () { this._savedProps = { hasControls: this.hasControls, borderColor: this.borderColor, lockMovementX: this.lockMovementX, lockMovementY: this.lockMovementY, hoverCursor: this.hoverCursor, selectable: this.selectable, defaultCursor: this.canvas && this.canvas.defaultCursor, moveCursor: this.canvas && this.canvas.moveCursor }; }, _restoreEditingProps: function () { this._savedProps && (this.hoverCursor = this._savedProps.hoverCursor, this.hasControls = this._savedProps.hasControls, this.borderColor = this._savedProps.borderColor, this.selectable = this._savedProps.selectable, this.lockMovementX = this._savedProps.lockMovementX, this.lockMovementY = this._savedProps.lockMovementY, this.canvas && (this.canvas.defaultCursor = this._savedProps.defaultCursor, this.canvas.moveCursor = this._savedProps.moveCursor)); }, exitEditing: function () { var t = this._textBeforeEdit !== this.text, e = this.hiddenTextarea; return this.selected = !1, this.isEditing = !1, this.selectionEnd = this.selectionStart, e && (e.blur && e.blur(), e.parentNode && e.parentNode.removeChild(e)), this.hiddenTextarea = null, this.abortCursorAnimation(), this._restoreEditingProps(), this._currentCursorOpacity = 0, this._shouldClearDimensionCache() && (this.initDimensions(), this.setCoords()), this.fire("editing:exited"), t && this.fire("modified"), this.canvas && (this.canvas.off("mouse:move", this.mouseMoveHandler), this.canvas.fire("text:editing:exited", { target: this }), t && this.canvas.fire("object:modified", { target: this })), this; }, _removeExtraneousStyles: function () { for (var t in this.styles) this._textLines[t] || delete this.styles[t]; }, removeStyleFromTo: function (t, e) { var i, r, n = this.get2DCursorLocation(t, !0), s = this.get2DCursorLocation(e, !0), o = n.lineIndex, a = n.charIndex, c = s.lineIndex, h = s.charIndex; if (o !== c) { if (this.styles[o]) for (i = a; i < this._unwrappedTextLines[o].length; i++)delete this.styles[o][i]; if (this.styles[c]) for (i = h; i < this._unwrappedTextLines[c].length; i++)(r = this.styles[c][i]) && (this.styles[o] || (this.styles[o] = {}), this.styles[o][a + i - h] = r); for (i = o + 1; i <= c; i++)delete this.styles[i]; this.shiftLineStyles(c, o - c); } else if (this.styles[o]) { r = this.styles[o]; var l, u, f = h - a; for (i = a; i < h; i++)delete r[i]; for (u in this.styles[o]) h <= (l = parseInt(u, 10)) && (r[l - f] = r[u], delete r[u]); } }, shiftLineStyles: function (t, e) { var i = u(this.styles); for (var r in this.styles) { var n = parseInt(r, 10); t < n && (this.styles[n + e] = i[n], i[n - e] || delete this.styles[n]); } }, restartCursorIfNeeded: function () { this._currentTickState && !this._currentTickState.isAborted && this._currentTickCompleteState && !this._currentTickCompleteState.isAborted || this.initDelayedCursor(); }, insertNewlineStyleObject: function (t, e, i, r) { var n, s = {}, o = !1, a = this._unwrappedTextLines[t].length === e; for (var c in i || (i = 1), this.shiftLineStyles(t, i), this.styles[t] && (n = this.styles[t][0 === e ? e : e - 1]), this.styles[t]) { var h = parseInt(c, 10); e <= h && (o = !0, s[h - e] = this.styles[t][c], a && 0 === e || delete this.styles[t][c]); } var l = !1; for (o && !a && (this.styles[t + i] = s, l = !0), l && i--; 0 < i;)r && r[i - 1] ? this.styles[t + i] = { 0: u(r[i - 1]) } : n ? this.styles[t + i] = { 0: u(n) } : delete this.styles[t + i], i--; this._forceClearCache = !0; }, insertCharStyleObject: function (t, e, i, r) { this.styles || (this.styles = {}); var n = this.styles[t], s = n ? u(n) : {}; for (var o in i || (i = 1), s) { var a = parseInt(o, 10); e <= a && (n[a + i] = s[a], s[a - i] || delete n[a]); } if (this._forceClearCache = !0, r) for (; i--;)Object.keys(r[i]).length && (this.styles[t] || (this.styles[t] = {}), this.styles[t][e + i] = u(r[i])); else if (n) for (var c = n[e ? e - 1 : 1]; c && i--;)this.styles[t][e + i] = u(c); }, insertNewStyleBlock: function (t, e, i) { for (var r = this.get2DCursorLocation(e, !0), n = [0], s = 0, o = 0; o < t.length; o++)"\n" === t[o] ? n[++s] = 0 : n[s]++; 0 < n[0] && (this.insertCharStyleObject(r.lineIndex, r.charIndex, n[0], i), i = i && i.slice(n[0] + 1)), s && this.insertNewlineStyleObject(r.lineIndex, r.charIndex + n[0], s); for (o = 1; o < s; o++)0 < n[o] ? this.insertCharStyleObject(r.lineIndex + o, 0, n[o], i) : i && this.styles[r.lineIndex + o] && i[0] && (this.styles[r.lineIndex + o][0] = i[0]), i = i && i.slice(n[o] + 1); 0 < n[o] && this.insertCharStyleObject(r.lineIndex + o, 0, n[o], i); }, setSelectionStartEndWithShift: function (t, e, i) { i <= t ? (e === t ? this._selectionDirection = "left" : "right" === this._selectionDirection && (this._selectionDirection = "left", this.selectionEnd = t), this.selectionStart = i) : t < i && i < e ? "right" === this._selectionDirection ? this.selectionEnd = i : this.selectionStart = i : (e === t ? this._selectionDirection = "right" : "left" === this._selectionDirection && (this._selectionDirection = "right", this.selectionStart = e), this.selectionEnd = i); }, setSelectionInBoundaries: function () { var t = this.text.length; this.selectionStart > t ? this.selectionStart = t : this.selectionStart < 0 && (this.selectionStart = 0), this.selectionEnd > t ? this.selectionEnd = t : this.selectionEnd < 0 && (this.selectionEnd = 0); } }); }(), fabric.util.object.extend(fabric.IText.prototype, { initDoubleClickSimulation: function () { this.__lastClickTime = +new Date, this.__lastLastClickTime = +new Date, this.__lastPointer = {}, this.on("mousedown", this.onMouseDown); }, onMouseDown: function (t) { if (this.canvas) { this.__newClickTime = +new Date; var e = t.pointer; this.isTripleClick(e) && (this.fire("tripleclick", t), this._stopEvent(t.e)), this.__lastLastClickTime = this.__lastClickTime, this.__lastClickTime = this.__newClickTime, this.__lastPointer = e, this.__lastIsEditing = this.isEditing, this.__lastSelected = this.selected; } }, isTripleClick: function (t) { return this.__newClickTime - this.__lastClickTime < 500 && this.__lastClickTime - this.__lastLastClickTime < 500 && this.__lastPointer.x === t.x && this.__lastPointer.y === t.y; }, _stopEvent: function (t) { t.preventDefault && t.preventDefault(), t.stopPropagation && t.stopPropagation(); }, initCursorSelectionHandlers: function () { this.initMousedownHandler(), this.initMouseupHandler(), this.initClicks(); }, doubleClickHandler: function (t) { this.isEditing && this.selectWord(this.getSelectionStartFromPointer(t.e)); }, tripleClickHandler: function (t) { this.isEditing && this.selectLine(this.getSelectionStartFromPointer(t.e)); }, initClicks: function () { this.on("mousedblclick", this.doubleClickHandler), this.on("tripleclick", this.tripleClickHandler); }, _mouseDownHandler: function (t) { !this.canvas || !this.editable || t.e.button && 1 !== t.e.button || (this.__isMousedown = !0, this.selected && (this.inCompositionMode = !1, this.setCursorByClick(t.e)), this.isEditing && (this.__selectionStartOnMouseDown = this.selectionStart, this.selectionStart === this.selectionEnd && this.abortCursorAnimation(), this.renderCursorOrSelection())); }, _mouseDownHandlerBefore: function (t) { !this.canvas || !this.editable || t.e.button && 1 !== t.e.button || (this.selected = this === this.canvas._activeObject); }, initMousedownHandler: function () { this.on("mousedown", this._mouseDownHandler), this.on("mousedown:before", this._mouseDownHandlerBefore); }, initMouseupHandler: function () { this.on("mouseup", this.mouseUpHandler); }, mouseUpHandler: function (t) { if (this.__isMousedown = !1, !(!this.editable || this.group || t.transform && t.transform.actionPerformed || t.e.button && 1 !== t.e.button)) { if (this.canvas) { var e = this.canvas._activeObject; if (e && e !== this) return; } this.__lastSelected && !this.__corner ? (this.selected = !1, this.__lastSelected = !1, this.enterEditing(t.e), this.selectionStart === this.selectionEnd ? this.initDelayedCursor(!0) : this.renderCursorOrSelection()) : this.selected = !0; } }, setCursorByClick: function (t) { var e = this.getSelectionStartFromPointer(t), i = this.selectionStart, r = this.selectionEnd; t.shiftKey ? this.setSelectionStartEndWithShift(i, r, e) : (this.selectionStart = e, this.selectionEnd = e), this.isEditing && (this._fireSelectionChanged(), this._updateTextarea()); }, getSelectionStartFromPointer: function (t) { for (var e, i = this.getLocalPointer(t), r = 0, n = 0, s = 0, o = 0, a = 0, c = 0, h = this._textLines.length; c < h && s <= i.y; c++)s += this.getHeightOfLine(c) * this.scaleY, 0 < (a = c) && (o += this._textLines[c - 1].length + this.missingNewlineOffset(c - 1)); n = this._getLineLeftOffset(a) * this.scaleX, e = this._textLines[a], "rtl" === this.direction && (i.x = this.width * this.scaleX - i.x + n); for (var l = 0, u = e.length; l < u && (r = n, (n += this.__charBounds[a][l].kernedWidth * this.scaleX) <= i.x); l++)o++; return this._getNewSelectionStartFromOffset(i, r, n, o, u); }, _getNewSelectionStartFromOffset: function (t, e, i, r, n) { var s = t.x - e, o = i - t.x, a = r + (s < o || o < 0 ? 0 : 1); return this.flipX && (a = n - a), a > this._text.length && (a = this._text.length), a; } }), fabric.util.object.extend(fabric.IText.prototype, { initHiddenTextarea: function () { this.hiddenTextarea = fabric.document.createElement("textarea"), this.hiddenTextarea.setAttribute("autocapitalize", "off"), this.hiddenTextarea.setAttribute("autocorrect", "off"), this.hiddenTextarea.setAttribute("autocomplete", "off"), this.hiddenTextarea.setAttribute("spellcheck", "false"), this.hiddenTextarea.setAttribute("data-fabric-hiddentextarea", ""), this.hiddenTextarea.setAttribute("wrap", "off"); var t = this._calcTextareaPosition(); this.hiddenTextarea.style.cssText = "position: absolute; top: " + t.top + "; left: " + t.left + "; z-index: -999; opacity: 0; width: 1px; height: 1px; font-size: 1px; padding-top: " + t.fontSize + ";", this.hiddenTextareaContainer ? this.hiddenTextareaContainer.appendChild(this.hiddenTextarea) : fabric.document.body.appendChild(this.hiddenTextarea), fabric.util.addListener(this.hiddenTextarea, "keydown", this.onKeyDown.bind(this)), fabric.util.addListener(this.hiddenTextarea, "keyup", this.onKeyUp.bind(this)), fabric.util.addListener(this.hiddenTextarea, "input", this.onInput.bind(this)), fabric.util.addListener(this.hiddenTextarea, "copy", this.copy.bind(this)), fabric.util.addListener(this.hiddenTextarea, "cut", this.copy.bind(this)), fabric.util.addListener(this.hiddenTextarea, "paste", this.paste.bind(this)), fabric.util.addListener(this.hiddenTextarea, "compositionstart", this.onCompositionStart.bind(this)), fabric.util.addListener(this.hiddenTextarea, "compositionupdate", this.onCompositionUpdate.bind(this)), fabric.util.addListener(this.hiddenTextarea, "compositionend", this.onCompositionEnd.bind(this)), !this._clickHandlerInitialized && this.canvas && (fabric.util.addListener(this.canvas.upperCanvasEl, "click", this.onClick.bind(this)), this._clickHandlerInitialized = !0); }, keysMap: { 9: "exitEditing", 27: "exitEditing", 33: "moveCursorUp", 34: "moveCursorDown", 35: "moveCursorRight", 36: "moveCursorLeft", 37: "moveCursorLeft", 38: "moveCursorUp", 39: "moveCursorRight", 40: "moveCursorDown" }, keysMapRtl: { 9: "exitEditing", 27: "exitEditing", 33: "moveCursorUp", 34: "moveCursorDown", 35: "moveCursorLeft", 36: "moveCursorRight", 37: "moveCursorRight", 38: "moveCursorUp", 39: "moveCursorLeft", 40: "moveCursorDown" }, ctrlKeysMapUp: { 67: "copy", 88: "cut" }, ctrlKeysMapDown: { 65: "selectAll" }, onClick: function () { this.hiddenTextarea && this.hiddenTextarea.focus(); }, onKeyDown: function (t) { if (this.isEditing) { var e = "rtl" === this.direction ? this.keysMapRtl : this.keysMap; if (t.keyCode in e) this[e[t.keyCode]](t); else { if (!(t.keyCode in this.ctrlKeysMapDown && (t.ctrlKey || t.metaKey))) return; this[this.ctrlKeysMapDown[t.keyCode]](t); } t.stopImmediatePropagation(), t.preventDefault(), 33 <= t.keyCode && t.keyCode <= 40 ? (this.inCompositionMode = !1, this.clearContextTop(), this.renderCursorOrSelection()) : this.canvas && this.canvas.requestRenderAll(); } }, onKeyUp: function (t) { !this.isEditing || this._copyDone || this.inCompositionMode ? this._copyDone = !1 : t.keyCode in this.ctrlKeysMapUp && (t.ctrlKey || t.metaKey) && (this[this.ctrlKeysMapUp[t.keyCode]](t), t.stopImmediatePropagation(), t.preventDefault(), this.canvas && this.canvas.requestRenderAll()); }, onInput: function (t) { var e = this.fromPaste; if (this.fromPaste = !1, t && t.stopPropagation(), this.isEditing) { var i, r, n, s, o, a = this._splitTextIntoLines(this.hiddenTextarea.value).graphemeText, c = this._text.length, h = a.length, l = h - c, u = this.selectionStart, f = this.selectionEnd, d = u !== f; if ("" === this.hiddenTextarea.value) return this.styles = {}, this.updateFromTextArea(), this.fire("changed"), void (this.canvas && (this.canvas.fire("text:changed", { target: this }), this.canvas.requestRenderAll())); var g = this.fromStringToGraphemeSelection(this.hiddenTextarea.selectionStart, this.hiddenTextarea.selectionEnd, this.hiddenTextarea.value), p = u > g.selectionStart; d ? (i = this._text.slice(u, f), l += f - u) : h < c && (i = p ? this._text.slice(f + l, f) : this._text.slice(u, u - l)), r = a.slice(g.selectionEnd - l, g.selectionEnd), i && i.length && (r.length && (n = this.getSelectionStyles(u, u + 1, !1), n = r.map(function () { return n[0]; })), d ? (s = u, o = f) : p ? (s = f - i.length, o = f) : o = (s = f) + i.length, this.removeStyleFromTo(s, o)), r.length && (e && r.join("") === fabric.copiedText && !fabric.disableStyleCopyPaste && (n = fabric.copiedTextStyle), this.insertNewStyleBlock(r, u, n)), this.updateFromTextArea(), this.fire("changed"), this.canvas && (this.canvas.fire("text:changed", { target: this }), this.canvas.requestRenderAll()); } }, onCompositionStart: function () { this.inCompositionMode = !0; }, onCompositionEnd: function () { this.inCompositionMode = !1; }, onCompositionUpdate: function (t) { this.compositionStart = t.target.selectionStart, this.compositionEnd = t.target.selectionEnd, this.updateTextareaPosition(); }, copy: function () { this.selectionStart !== this.selectionEnd && (fabric.copiedText = this.getSelectedText(), fabric.disableStyleCopyPaste ? fabric.copiedTextStyle = null : fabric.copiedTextStyle = this.getSelectionStyles(this.selectionStart, this.selectionEnd, !0), this._copyDone = !0); }, paste: function () { this.fromPaste = !0; }, _getClipboardData: function (t) { return t && t.clipboardData || fabric.window.clipboardData; }, _getWidthBeforeCursor: function (t, e) { var i, r = this._getLineLeftOffset(t); return 0 < e && (r += (i = this.__charBounds[t][e - 1]).left + i.width), r; }, getDownCursorOffset: function (t, e) { var i = this._getSelectionForOffset(t, e), r = this.get2DCursorLocation(i), n = r.lineIndex; if (n === this._textLines.length - 1 || t.metaKey || 34 === t.keyCode) return this._text.length - i; var s = r.charIndex, o = this._getWidthBeforeCursor(n, s), a = this._getIndexOnLine(n + 1, o); return this._textLines[n].slice(s).length + a + 1 + this.missingNewlineOffset(n); }, _getSelectionForOffset: function (t, e) { return t.shiftKey && this.selectionStart !== this.selectionEnd && e ? this.selectionEnd : this.selectionStart; }, getUpCursorOffset: function (t, e) { var i = this._getSelectionForOffset(t, e), r = this.get2DCursorLocation(i), n = r.lineIndex; if (0 === n || t.metaKey || 33 === t.keyCode) return -i; var s = r.charIndex, o = this._getWidthBeforeCursor(n, s), a = this._getIndexOnLine(n - 1, o), c = this._textLines[n].slice(0, s), h = this.missingNewlineOffset(n - 1); return -this._textLines[n - 1].length + a - c.length + (1 - h); }, _getIndexOnLine: function (t, e) { for (var i, r, n = this._textLines[t], s = this._getLineLeftOffset(t), o = 0, a = 0, c = n.length; a < c; a++)if (e < (s += i = this.__charBounds[t][a].width)) { r = !0; var h = s - i, l = s, u = Math.abs(h - e); o = Math.abs(l - e) < u ? a : a - 1; break; } return r || (o = n.length - 1), o; }, moveCursorDown: function (t) { this.selectionStart >= this._text.length && this.selectionEnd >= this._text.length || this._moveCursorUpOrDown("Down", t); }, moveCursorUp: function (t) { 0 === this.selectionStart && 0 === this.selectionEnd || this._moveCursorUpOrDown("Up", t); }, _moveCursorUpOrDown: function (t, e) { var i = this["get" + t + "CursorOffset"](e, "right" === this._selectionDirection); e.shiftKey ? this.moveCursorWithShift(i) : this.moveCursorWithoutShift(i), 0 !== i && (this.setSelectionInBoundaries(), this.abortCursorAnimation(), this._currentCursorOpacity = 1, this.initDelayedCursor(), this._fireSelectionChanged(), this._updateTextarea()); }, moveCursorWithShift: function (t) { var e = "left" === this._selectionDirection ? this.selectionStart + t : this.selectionEnd + t; return this.setSelectionStartEndWithShift(this.selectionStart, this.selectionEnd, e), 0 !== t; }, moveCursorWithoutShift: function (t) { return t < 0 ? (this.selectionStart += t, this.selectionEnd = this.selectionStart) : (this.selectionEnd += t, this.selectionStart = this.selectionEnd), 0 !== t; }, moveCursorLeft: function (t) { 0 === this.selectionStart && 0 === this.selectionEnd || this._moveCursorLeftOrRight("Left", t); }, _move: function (t, e, i) { var r; if (t.altKey) r = this["findWordBoundary" + i](this[e]); else { if (!t.metaKey && 35 !== t.keyCode && 36 !== t.keyCode) return this[e] += "Left" === i ? -1 : 1, !0; r = this["findLineBoundary" + i](this[e]); } if (void 0 !== r && this[e] !== r) return this[e] = r, !0; }, _moveLeft: function (t, e) { return this._move(t, e, "Left"); }, _moveRight: function (t, e) { return this._move(t, e, "Right"); }, moveCursorLeftWithoutShift: function (t) { var e = !0; return this._selectionDirection = "left", this.selectionEnd === this.selectionStart && 0 !== this.selectionStart && (e = this._moveLeft(t, "selectionStart")), this.selectionEnd = this.selectionStart, e; }, moveCursorLeftWithShift: function (t) { return "right" === this._selectionDirection && this.selectionStart !== this.selectionEnd ? this._moveLeft(t, "selectionEnd") : 0 !== this.selectionStart ? (this._selectionDirection = "left", this._moveLeft(t, "selectionStart")) : void 0; }, moveCursorRight: function (t) { this.selectionStart >= this._text.length && this.selectionEnd >= this._text.length || this._moveCursorLeftOrRight("Right", t); }, _moveCursorLeftOrRight: function (t, e) { var i = "moveCursor" + t + "With"; this._currentCursorOpacity = 1, e.shiftKey ? i += "Shift" : i += "outShift", this[i](e) && (this.abortCursorAnimation(), this.initDelayedCursor(), this._fireSelectionChanged(), this._updateTextarea()); }, moveCursorRightWithShift: function (t) { return "left" === this._selectionDirection && this.selectionStart !== this.selectionEnd ? this._moveRight(t, "selectionStart") : this.selectionEnd !== this._text.length ? (this._selectionDirection = "right", this._moveRight(t, "selectionEnd")) : void 0; }, moveCursorRightWithoutShift: function (t) { var e = !0; return this._selectionDirection = "right", this.selectionStart === this.selectionEnd ? (e = this._moveRight(t, "selectionStart"), this.selectionEnd = this.selectionStart) : this.selectionStart = this.selectionEnd, e; }, removeChars: function (t, e) { void 0 === e && (e = t + 1), this.removeStyleFromTo(t, e), this._text.splice(t, e - t), this.text = this._text.join(""), this.set("dirty", !0), this._shouldClearDimensionCache() && (this.initDimensions(), this.setCoords()), this._removeExtraneousStyles(); }, insertChars: function (t, e, i, r) { void 0 === r && (r = i), i < r && this.removeStyleFromTo(i, r); var n = fabric.util.string.graphemeSplit(t); this.insertNewStyleBlock(n, i, e), this._text = [].concat(this._text.slice(0, i), n, this._text.slice(r)), this.text = this._text.join(""), this.set("dirty", !0), this._shouldClearDimensionCache() && (this.initDimensions(), this.setCoords()), this._removeExtraneousStyles(); } }), function () { var l = fabric.util.toFixed, u = /  +/g; fabric.util.object.extend(fabric.Text.prototype, { _toSVG: function () { var t = this._getSVGLeftTopOffsets(), e = this._getSVGTextAndBg(t.textTop, t.textLeft); return this._wrapSVGTextAndBg(e); }, toSVG: function (t) { return this._createBaseSVGMarkup(this._toSVG(), { reviver: t, noStyle: !0, withShadow: !0 }); }, _getSVGLeftTopOffsets: function () { return { textLeft: -this.width / 2, textTop: -this.height / 2, lineTop: this.getHeightOfLine(0) }; }, _wrapSVGTextAndBg: function (t) { var e = this.getSvgTextDecoration(this); return [t.textBgRects.join(""), '\t\t<text xml:space="preserve" ', this.fontFamily ? 'font-family="' + this.fontFamily.replace(/"/g, "'") + '" ' : "", this.fontSize ? 'font-size="' + this.fontSize + '" ' : "", this.fontStyle ? 'font-style="' + this.fontStyle + '" ' : "", this.fontWeight ? 'font-weight="' + this.fontWeight + '" ' : "", e ? 'text-decoration="' + e + '" ' : "", 'style="', this.getSvgStyles(!0), '"', this.addPaintOrder(), " >", t.textSpans.join(""), "</text>\n"]; }, _getSVGTextAndBg: function (t, e) { var i, r = [], n = [], s = t; this._setSVGBg(n); for (var o = 0, a = this._textLines.length; o < a; o++)i = this._getLineLeftOffset(o), (this.textBackgroundColor || this.styleHas("textBackgroundColor", o)) && this._setSVGTextLineBg(n, o, e + i, s), this._setSVGTextLineText(r, o, e + i, s), s += this.getHeightOfLine(o); return { textSpans: r, textBgRects: n }; }, _createTextCharSpan: function (t, e, i, r) { var n = t !== t.trim() || t.match(u), s = this.getSvgSpanStyles(e, n), o = s ? 'style="' + s + '"' : "", a = e.deltaY, c = "", h = fabric.Object.NUM_FRACTION_DIGITS; return a && (c = ' dy="' + l(a, h) + '" '), ['<tspan x="', l(i, h), '" y="', l(r, h), '" ', c, o, ">", fabric.util.string.escapeXml(t), "</tspan>"].join(""); }, _setSVGTextLineText: function (t, e, i, r) { var n, s, o, a, c, h = this.getHeightOfLine(e), l = -1 !== this.textAlign.indexOf("justify"), u = "", f = 0, d = this._textLines[e]; r += h * (1 - this._fontSizeFraction) / this.lineHeight; for (var g = 0, p = d.length - 1; g <= p; g++)c = g === p || this.charSpacing, u += d[g], o = this.__charBounds[e][g], 0 === f ? (i += o.kernedWidth - o.width, f += o.width) : f += o.kernedWidth, l && !c && this._reSpaceAndTab.test(d[g]) && (c = !0), c || (n = n || this.getCompleteStyleDeclaration(e, g), s = this.getCompleteStyleDeclaration(e, g + 1), c = fabric.util.hasStyleChanged(n, s, !0)), c && (a = this._getStyleDeclaration(e, g) || {}, t.push(this._createTextCharSpan(u, a, i, r)), u = "", n = s, i += f, f = 0); }, _pushTextBgRect: function (t, e, i, r, n, s) { var o = fabric.Object.NUM_FRACTION_DIGITS; t.push("\t\t<rect ", this._getFillAttributes(e), ' x="', l(i, o), '" y="', l(r, o), '" width="', l(n, o), '" height="', l(s, o), '"></rect>\n'); }, _setSVGTextLineBg: function (t, e, i, r) { for (var n, s, o = this._textLines[e], a = this.getHeightOfLine(e) / this.lineHeight, c = 0, h = 0, l = this.getValueOfPropertyAt(e, 0, "textBackgroundColor"), u = 0, f = o.length; u < f; u++)n = this.__charBounds[e][u], (s = this.getValueOfPropertyAt(e, u, "textBackgroundColor")) !== l ? (l && this._pushTextBgRect(t, l, i + h, r, c, a), h = n.left, c = n.width, l = s) : c += n.kernedWidth; s && this._pushTextBgRect(t, s, i + h, r, c, a); }, _getFillAttributes: function (t) { var e = t && "string" == typeof t ? new fabric.Color(t) : ""; return e && e.getSource() && 1 !== e.getAlpha() ? 'opacity="' + e.getAlpha() + '" fill="' + e.setAlpha(1).toRgb() + '"' : 'fill="' + t + '"'; }, _getSVGLineTopOffset: function (t) { for (var e, i = 0, r = 0; r < t; r++)i += this.getHeightOfLine(r); return e = this.getHeightOfLine(r), { lineTop: i, offset: (this._fontSizeMult - this._fontSizeFraction) * e / (this.lineHeight * this._fontSizeMult) }; }, getSvgStyles: function (t) { return fabric.Object.prototype.getSvgStyles.call(this, t) + " white-space: pre;"; } }); }(), function (t) { "use strict"; var b = t.fabric || (t.fabric = {}); b.Textbox = b.util.createClass(b.IText, b.Observable, { type: "textbox", minWidth: 20, dynamicMinWidth: 2, __cachedLines: null, lockScalingFlip: !0, noScaleCache: !1, _dimensionAffectingProps: b.Text.prototype._dimensionAffectingProps.concat("width"), _wordJoiners: /[ \t\r]/, splitByGrapheme: !1, initDimensions: function () { this.__skipDimension || (this.isEditing && this.initDelayedCursor(), this.clearContextTop(), this._clearCache(), this.dynamicMinWidth = 0, this._styleMap = this._generateStyleMap(this._splitText()), this.dynamicMinWidth > this.width && this._set("width", this.dynamicMinWidth), -1 !== this.textAlign.indexOf("justify") && this.enlargeSpaces(), this.height = this.calcTextHeight(), this.saveState({ propertySet: "_dimensionAffectingProps" })); }, _generateStyleMap: function (t) { for (var e = 0, i = 0, r = 0, n = {}, s = 0; s < t.graphemeLines.length; s++)"\n" === t.graphemeText[r] && 0 < s ? (i = 0, r++, e++) : !this.splitByGrapheme && this._reSpaceAndTab.test(t.graphemeText[r]) && 0 < s && (i++, r++), n[s] = { line: e, offset: i }, r += t.graphemeLines[s].length, i += t.graphemeLines[s].length; return n; }, styleHas: function (t, e) { if (this._styleMap && !this.isWrapping) { var i = this._styleMap[e]; i && (e = i.line); } return b.Text.prototype.styleHas.call(this, t, e); }, isEmptyStyles: function (t) { if (!this.styles) return !0; var e, i, r = 0, n = !1, s = this._styleMap[t], o = this._styleMap[t + 1]; for (var a in s && (t = s.line, r = s.offset), o && (n = o.line === t, e = o.offset), i = void 0 === t ? this.styles : { line: this.styles[t] }) for (var c in i[a]) if (r <= c && (!n || c < e)) for (var h in i[a][c]) return !1; return !0; }, _getStyleDeclaration: function (t, e) { if (this._styleMap && !this.isWrapping) { var i = this._styleMap[t]; if (!i) return null; t = i.line, e = i.offset + e; } return this.callSuper("_getStyleDeclaration", t, e); }, _setStyleDeclaration: function (t, e, i) { var r = this._styleMap[t]; t = r.line, e = r.offset + e, this.styles[t][e] = i; }, _deleteStyleDeclaration: function (t, e) { var i = this._styleMap[t]; t = i.line, e = i.offset + e, delete this.styles[t][e]; }, _getLineStyle: function (t) { var e = this._styleMap[t]; return !!this.styles[e.line]; }, _setLineStyle: function (t) { var e = this._styleMap[t]; this.styles[e.line] = {}; }, _wrapText: function (t, e) { var i, r = []; for (this.isWrapping = !0, i = 0; i < t.length; i++)r = r.concat(this._wrapLine(t[i], i, e)); return this.isWrapping = !1, r; }, _measureWord: function (t, e, i) { var r, n = 0; i = i || 0; for (var s = 0, o = t.length; s < o; s++) { n += this._getGraphemeBox(t[s], e, s + i, r, !0).kernedWidth, r = t[s]; } return n; }, _wrapLine: function (t, e, i, r) { var n = 0, s = this.splitByGrapheme, o = [], a = [], c = s ? b.util.string.graphemeSplit(t) : t.split(this._wordJoiners), h = "", l = 0, u = s ? "" : " ", f = 0, d = 0, g = 0, p = !0, v = this._getWidthOfCharSpacing(); r = r || 0; 0 === c.length && c.push([]), i -= r; for (var m = 0; m < c.length; m++)h = s ? c[m] : b.util.string.graphemeSplit(c[m]), f = this._measureWord(h, e, l), l += h.length, i < (n += d + f - v) && !p ? (o.push(a), a = [], n = f, p = !0) : n += v, p || s || a.push(u), a = a.concat(h), d = s ? 0 : this._measureWord([u], e, l), l++, p = !1, g < f && (g = f); return m && o.push(a), g + r > this.dynamicMinWidth && (this.dynamicMinWidth = g - v + r), o; }, isEndOfWrapping: function (t) { return !this._styleMap[t + 1] || this._styleMap[t + 1].line !== this._styleMap[t].line; }, missingNewlineOffset: function (t) { return this.splitByGrapheme ? this.isEndOfWrapping(t) ? 1 : 0 : 1; }, _splitTextIntoLines: function (t) { for (var e = b.Text.prototype._splitTextIntoLines.call(this, t), i = this._wrapText(e.lines, this.width), r = new Array(i.length), n = 0; n < i.length; n++)r[n] = i[n].join(""); return e.lines = r, e.graphemeLines = i, e; }, getMinWidth: function () { return Math.max(this.minWidth, this.dynamicMinWidth); }, _removeExtraneousStyles: function () { var t = {}; for (var e in this._styleMap) this._textLines[e] && (t[this._styleMap[e].line] = 1); for (var e in this.styles) t[e] || delete this.styles[e]; }, toObject: function (t) { return this.callSuper("toObject", ["minWidth", "splitByGrapheme"].concat(t)); } }), b.Textbox.fromObject = function (t, e) { var i = b.util.stylesFromArray(t.styles, t.text), r = Object.assign({}, t, { styles: i }); return b.Object._fromObject("Textbox", r, e, "text"); }; }("undefined" != typeof exports ? exports : this), function () { var t = fabric.controlsUtils, e = t.scaleSkewCursorStyleHandler, i = t.scaleCursorStyleHandler, r = t.scalingEqually, n = t.scalingYOrSkewingX, s = t.scalingXOrSkewingY, o = t.scaleOrSkewActionName, a = fabric.Object.prototype.controls; if (a.ml = new fabric.Control({ x: -.5, y: 0, cursorStyleHandler: e, actionHandler: s, getActionName: o }), a.mr = new fabric.Control({ x: .5, y: 0, cursorStyleHandler: e, actionHandler: s, getActionName: o }), a.mb = new fabric.Control({ x: 0, y: .5, cursorStyleHandler: e, actionHandler: n, getActionName: o }), a.mt = new fabric.Control({ x: 0, y: -.5, cursorStyleHandler: e, actionHandler: n, getActionName: o }), a.tl = new fabric.Control({ x: -.5, y: -.5, cursorStyleHandler: i, actionHandler: r }), a.tr = new fabric.Control({ x: .5, y: -.5, cursorStyleHandler: i, actionHandler: r }), a.bl = new fabric.Control({ x: -.5, y: .5, cursorStyleHandler: i, actionHandler: r }), a.br = new fabric.Control({ x: .5, y: .5, cursorStyleHandler: i, actionHandler: r }), a.mtr = new fabric.Control({ x: 0, y: -.5, actionHandler: t.rotationWithSnapping, cursorStyleHandler: t.rotationStyleHandler, offsetY: -40, withConnection: !0, actionName: "rotate" }), fabric.Textbox) { var c = fabric.Textbox.prototype.controls = {}; c.mtr = a.mtr, c.tr = a.tr, c.br = a.br, c.tl = a.tl, c.bl = a.bl, c.mt = a.mt, c.mb = a.mb, c.mr = new fabric.Control({ x: .5, y: 0, actionHandler: t.changeWidth, cursorStyleHandler: e, actionName: "resizing" }), c.ml = new fabric.Control({ x: -.5, y: 0, actionHandler: t.changeWidth, cursorStyleHandler: e, actionName: "resizing" }); } }();
    return fabric;
});
