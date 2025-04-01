//
// 2025-2-12
// 第五代WEB编辑器启动脚本
// 南京都昌信息科技有限公司
// 当配合5代文件发布器 DCWriter5FileDownload 时，对本文件作出任何改变，导致文件修改时间发生改变时，
// 浏览器都会自动重新下载所有程序文件（DLL/GZ）等，而无需清空浏览器的缓存。
//
"use strict";
(function () {
    if (window.__DCWriter5Started == true) {
        // 避免重复调用
        return;
    }
    window.__DCWriter5Started = true;
    window.__DCWriter5FullLoaded = false;
    var strAppVersion = "$$version$$";
    window.strAppVersion = strAppVersion;
    // 获得资源基础路径
    var strBasePath = "_framework/";
    var bolDebugMode = false;
    var strServicePageUrl = null;
    var requestDLLUsingBase64 = false;
    if (document.currentScript != null) {
        //debugger;
        bolDebugMode = document.currentScript.getAttribute("debugmode") == "true";
        strBasePath = document.currentScript.getAttribute("src");
        strServicePageUrl = document.currentScript.getAttribute("servicepageurl");
        // if (!strBasePath && !strServicePageUrl && window._DCWriter5SpecifyBasePath && window._DCWriter5SpecifyServicePageUrl) {
        //     strBasePath = window._DCWriter5SpecifyBasePath; // 试图设置编辑器程序文件下载基础路径
        //     strServicePageUrl = window._DCWriter5SpecifyServicePageUrl; // 试图设置服务器页面地址路径
        // }
        // 两个属性分开判断，避免影响
        if (!strBasePath && window._DCWriter5SpecifyBasePath) {
            strBasePath = window._DCWriter5SpecifyBasePath; // 试图设置编辑器程序文件下载基础路径
        }
        if (!strServicePageUrl && window._DCWriter5SpecifyServicePageUrl) {
            strServicePageUrl = window._DCWriter5SpecifyServicePageUrl; // 试图设置服务器页面地址路径
        }
    }
    else {
        // 在类似微服务的前端框架下，本JS代码不是通过 <script src="xxx">来引用的，而是通过在主窗体使用eval()来执行的，
        // 此时document.currentScript是无效的，需要用户指定信息
        strBasePath = window._DCWriter5SpecifyBasePath; // 试图设置编辑器程序文件下载基础路径
        strServicePageUrl = window._DCWriter5SpecifyServicePageUrl; // 试图设置服务器页面地址路径
    }
    if (strBasePath != null && strBasePath.length > 0) {
        ////wyc20240828:使用特殊请求标记DUWRITER5_0-3459
        if (strBasePath.indexOf("&dcloaddllusingbase64=1") >= 0) {
            strBasePath = strBasePath.replace("&dcloaddllusingbase64=1", "");
            requestDLLUsingBase64 = true;
        }
        var index = strBasePath.lastIndexOf("?");
        if (index > 0) {
            strServicePageUrl = strBasePath.substring(0, index).trim();
        }
        else {
            index = strBasePath.lastIndexOf("/");
            if (index < 0) {
                index = strBasePath.lastIndexOf("\\");
            }
            if (index < 0) {
                strBasePath = "./";
            }
            else {
                strBasePath = strBasePath.substring(0, index) + "/";
            }
        }
        strBasePath = strBasePath.trim();
    }
    else {
        // 使用默认路径
        strBasePath = "_framework/";
    }
    window.strBasePath = strServicePageUrl;
    if (strServicePageUrl != null && strServicePageUrl.length > 0) {
        var index5 = strServicePageUrl.indexOf("?");
        if (index5 > 0) {
            strServicePageUrl = strServicePageUrl.substring(0, index5);
        }
    }
    if (strServicePageUrl != null && strServicePageUrl.length > 0) {
        console.info("DCWriter5全局服务器页面地址:" + strServicePageUrl);
        window.__DCWriterServicePageUrl = strServicePageUrl;
    }
    else {
        console.info("DCWriter5基础路径:" + strBasePath);
    }
    var jsScript = document.createElement("script");
    jsScript.setAttribute("language", "javascript");
    var strEnvironment = "";
    var strResourceBasePath = strBasePath;
    if (strServicePageUrl != null && strServicePageUrl.length > 0) {
        var strFlag = strServicePageUrl + "@" + window.location.origin;
        var totalValue = 0;
        for (var iCount = 0; iCount < strFlag.length; iCount++) {
            totalValue += strFlag.charCodeAt(iCount);
        }
        for (var iCount = 0; iCount < strFlag.length; iCount += 2) {
            totalValue += strFlag.charCodeAt(iCount);
        }
        for (var iCount = 1; iCount < strFlag.length; iCount += 2) {
            totalValue += strFlag.charCodeAt(iCount) * strFlag.charCodeAt(iCount - 1);
        }
        strResourceBasePath = strServicePageUrl + "?wasmres={0}&ver=" + strAppVersion + "&flag=" + totalValue + "&wasmrootpath=" + encodeURIComponent(strServicePageUrl);
        jsScript.src = strResourceBasePath.replace("{0}", "blazor.webassembly.js");
    }
    else {
        jsScript.src = strResourceBasePath + "blazor.webassembly.js";
    }
    if (window.__DCResourceBasePath == null) {
        window.__DCResourceBasePath = strResourceBasePath;
    }
    //jsScript.src = "_framework/blazor.webassembly.js";
    /*if (strServicePageUrl != null && strServicePageUrl.length > 0) {
        jsScript.src = strServicePageUrl + "?ver=" + strAppVersion + "&wasmrootpath=" + strServicePageUrl + "&wasmres=blazor.webassembly.js";
        strEnvironment = strServicePageUrl + "?ver=" + strAppVersion + "&wasmrootpath=" + strServicePageUrl + "&wasmres=";
    }
    else {
        jsScript.src = strBasePath + "blazor.webassembly.js";
        strEnvironment = strBasePath;
    }*/
    strEnvironment = strResourceBasePath;

    //在此处判断是否为chrome 71一下的版本
    var UserAgent = navigator.userAgent.toLowerCase();
    //如果是chrome
    if (UserAgent.indexOf('chrome') > -1) {
        var versions = parseInt(UserAgent.match(/chrome\/([\d.]+)/)[1]);
        if (versions < 71) {
            if (typeof self !== "undefined") {
                window.globalThis = self;
            } else if (typeof window !== "undefined") {
                window.globalThis = window;
            } else if (typeof global !== "undefined") {
                window.globalThis = global;
            }
            if (!window.globalThis) {
                throw new Error("unable to locate global object");
            }
        }
    }
    // 修复nw.js和WASM的冲突，在Blazor加载之前重命名全局变量process
    var ENVIRONMENT_IS_NODE = typeof process == "object" && typeof process.versions == "object" && typeof process.versions.node == "string";
    if (ENVIRONMENT_IS_NODE) {
        globalThis.__process = globalThis.process;
        delete globalThis.process;
    }

    jsScript.setAttribute("autostart", "false");
    jsScript.onload = function () {
        Blazor.start({
            environment: strEnvironment,
            loadBootResource: function (type, name, defaultUri, integrity) {
                if (name == 'blazor.boot.json') {
                    //对微前端框架MicroApp的支持
                    if (window.__MICRO_APP_WINDOW__) {
                        __MICRO_APP_WINDOW__.document.defaultView.Blazor = window.Blazor;
                        __MICRO_APP_WINDOW__.document.defaultView.DotNet = window.DotNet;
                    }
                }
                //对微前端框架QianKun的支持
                if (window.__POWERED_BY_QIANKUN__) {
                    window.document.defaultView.Blazor = window.Blazor;
                    window.document.defaultView.DotNet = window.DotNet;
                }
                var strRuntimeUrl = null;
                if (strResourceBasePath.indexOf("{0}") > 0) {
                    strRuntimeUrl = strResourceBasePath.replace("{0}", name);
                }
                else {
                    strRuntimeUrl = strResourceBasePath + name;
                }
                //if (name.indexOf(".dll") >= 0) {
                //    strRuntimeUrl = strRuntimeUrl.replace(name, name + ".gz");
                //}
                //var strRuntimeUrl = defaultUri;
                //if (strServicePageUrl != null && strServicePageUrl.length > 0) {
                //    strRuntimeUrl = strServicePageUrl + "?ver=" + strAppVersion + "&wasmrootpath=" + strServicePageUrl + "&wasmres=" + name;
                //}
                //else if (strBasePath != null && strBasePath.length > 0) {
                //    // 启用自定义下载路径
                //    strRuntimeUrl = strBasePath + name;
                //}
                //console.log(strRuntimeUrl);
                if (bolDebugMode == true) {
                    console.log("DCWriter5加载资源:" + strRuntimeUrl);
                }
                if (type != "dotnetjs"
                    && name != "blazor.boot.json"
                    && strServicePageUrl != null
                    && strServicePageUrl.length > 0) {
                    // 如果遇到使用服务器页面的情况，则允许启用本地缓存

                    //wyc20240828:隐藏wasmres远程请求的url信息DUWRITER5_0-3459
                    //debugger;
                    var strs2 = strRuntimeUrl;
                    if (requestDLLUsingBase64 === true) {
                        var strs = strRuntimeUrl.split('?');
                        strs2 = strs[0] + "?wasmres=" + window.btoa(strs[1]);
                    }
                    var promise = fetch(
                        strs2, {
                        method: "GET",
                        credentials: "include",
                        cache: "default"
                    });
                    if (promise instanceof Promise) {
                        return promise;
                    } else {
                        return strRuntimeUrl;
                    }
                }
                else {
                    return strRuntimeUrl;
                }
            }
        }).then(function () {
            // 修复nw.js和WASM的冲突，在Blazor启动后将全局变量process更改回来
            globalThis.process = globalThis.__process;
            delete globalThis.__process;
        });
    };
    document.head.appendChild(jsScript);

    //在此处创建CreateTemperatureControlForWASM方法
    window.CreateTemperatureControlForWASM = function (rootElement) {
        if (typeof rootElement == "string") {
            rootElement = document.getElementById(rootElement);
        }
        if (!rootElement) {
            return "未找到正确的时间轴元素";
        }
        //在内部判断是否
        if (window.StartGlobal) {
            CreateWriterControlForWASM(rootElement, "TemperatureControl");
        } else {
            rootElement.LoadStartGlobal = function (rootElement) {
                CreateWriterControlForWASM(rootElement, "TemperatureControl");
            };
        }
    };

    // 在此处创建产程图CreateFlowControlForWASM方法;
    window.CreateFlowControlForWASM = function (rootElement) {
        if (typeof rootElement == "string") {
            rootElement = document.getElementById(rootElement);
        }
        if (!rootElement) {
            return "未找到正确的产程图元素";
        }
        //在内部判断是否
        if (window.StartGlobal) {
            CreateWriterControlForWASM(rootElement, "DCFlowControlForWASM");
        } else {
            rootElement.LoadStartGlobal = function (rootElement) {
                CreateWriterControlForWASM(rootElement, "DCFlowControlForWASM");
            };
        }
    };

})();