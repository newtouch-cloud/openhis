// DCWriter文档中数值表达式中的函数的执行器
// 南京都昌信息科技有限公司 2023-12-6
// 修改记录: 2023-12-6 增加计算表达式方法 lxy
//未完成：PARAMETER
"use strict";

import { DCTools20221228 } from "./DCTools20221228.js";
export let WriterControl_EF = {
    /**
     * 执行数值表达式中的函数
     * @param {string | HTMLDivElement} rootElement 编辑器元素对象
     * @param {string} strFunctionName 函数名
     * @param {Array} args 参数数组
     * @returns { object} 返回值
     */
    Exec: function (rootElement, strFunctionName, ...args) {
        rootElement = DCTools20221228.GetOwnerWriterControl(rootElement);
        if (rootElement == null) {
            return null;
        }
        if (strFunctionName == null || strFunctionName.length == 0) {
            return null;
        }
        if (args != null && args.length > 0) {
            for (var iCount = args.length - 1; iCount >= 0; iCount--) {
                if (args[iCount] == 2147439148) {
                    // NaN值无法直接传递，在此间接传递
                    args[iCount] = NaN;
                }
            }
        }
        try {
            if (rootElement.CustomExpressionFunctions != null) {
                // 首先执行用户自定义函数
                // 用户可以定义一个JS对象，里面列出所有可用的函数，然后设置到编辑器控件的CustomExpressionFunctions属性上去。
                var func = rootElement.CustomExpressionFunctions[strFunctionName];
                if (typeof func == "function") {
                    // 找到用户指定名称的函数，执行并返回
                    var result55 = func(...args);
                    return result55;
                }
            }
            // 执行标准函数
            switch (strFunctionName.toUpperCase()) {
                case "MAXINGROUP":
                    return this.MAXINGROUP(...args);
                    break;
                case "MININGROUP":
                    return this.MININGROUP(...args);
                    break;
                case "SUMINGROUP":
                    return this.SUMINGROUP(...args);
                    break;
                case "CONCATENATE":
                    return this.CONCATENATE(...args);
                    break;
                case "LOOKUP":
                    return this.LOOKUP(...args);
                    break;
                case "IF":
                    return this.IF(...args);
                    break;
                case "AGE":
                    return this.AGE(...args);
                    break;
                case "AGEMONTH":
                    return this.AGEMONTH(...args);
                    break;
                case "AGEWEEK":
                    return this.AGEWEEK(...args);
                    break;
                case "AGEDAY":
                    return this.AGEDAY(...args);
                    break;
                case "AGEHOUR":
                    return this.AGEHOUR(...args);
                    break;
                case "FINDINDEX":
                    return this.FINDINDEX(...args);
                    break;
                case "FIND":
                    return this.FIND(...args);
                    break;
                case "LEN":
                    return this.LEN(...args);
                    break;
                // case "PARAMETER": return this.PARAMETER(...args); break;
                case "ABS":
                    return this.ABS(...args);
                    break;
                case "ACOS":
                    return this.ACOS(...args);
                    break;
                case "ASIN":
                    return this.ASIN(...args);
                    break;
                case "ATAN":
                    return this.ATAN(...args);
                    break;
                case "ATAN2":
                    return this.ATAN2(...args);
                    break;
                case "AVERAGE":
                    return this.AVERAGE(...args);
                    break;
                case "CEILING":
                    return this.CEILING(...args);
                    break;
                case "COS":
                    return this.COS(...args);
                    break;
                case "COUNT":
                    return this.COUNT(...args);
                    break;
                case "EXP":
                    return this.EXP(...args);
                    break;
                case "FLOOR":
                    return this.FLOOR(...args);
                    break;
                case "INT":
                    return this.INT(...args);
                    break;
                case "LOG":
                    return this.LOG(...args);
                    break;
                case "LOG10":
                    return this.LOG10(...args);
                    break;
                case "MAX":
                    return this.MAX(...args);
                    break;
                case "MIN":
                    return this.MIN(...args);
                    break;
                case "MOD":
                    return this.MOD(...args);
                    break;
                case "ODD":
                    return this.ODD(...args);
                    break;
                case "POW":
                    return this.POW(...args);
                    break;
                case "PRODUCT":
                    return this.PRODUCT(...args);
                    break;
                case "RADIANS":
                    return this.RADIANS(...args);
                    break;
                case "RAND":
                    return this.RAND(...args);
                    break;
                case "ROUND":
                    return this.ROUND(...args);
                    break;
                case "ROUNDDOWN":
                    return this.ROUNDDOWN(...args);
                    break;
                case "ROUNDUP":
                    return this.ROUNDUP(...args);
                    break;
                case "SIGN":
                    return this.SIGN(...args);
                    break;
                case "SIN":
                    return this.SIN(...args);
                    break;
                case "SQRT":
                    return this.SQRT(...args);
                    break;
                case "SUM":
                    return this.SUM(...args);
                    break;
                case "SUMINNERVALUE":
                    return this.SUMINNERVALUE(...args);
                    break;
                case "TAN":
                    return this.TAN(...args);
                    break;
                case "CINT":
                    return this.CINT(...args);
                    break;
                case "CDOUBLE":
                    return this.CDOUBLE(...args);
                    break;
                case "SOLARTERM":
                    return this.SOLARTERM(...args);
                    break;
                case "MINUTEDIFF":
                    return this.MINUTEDIFF(...args);
                    break;
                case "MINUTEDIFF2":
                    return this.MINUTEDIFF2(...args);
                    break;
                case "ADDDAY":
                    return this.ADDDAY(...args);
                    break;
                case "ADDHOUR":
                    return this.ADDHOUR(...args);
                    break;
                case "ADDMINUTES":
                    return this.ADDMINUTES(...args);
                    break;
                case "DAYDIFF":
                    return this.DAYDIFF(...args);
                    break;
                case "HOURDIFF":
                    return this.HOURDIFF(...args);
                    break;
                case "SUMINNERVALUE":
                    return this.SUMINNERVALUE(...args);
                    break;
                // default:
                //     throw strFunctionName;
                //     break;
            }
        } catch (err) {
            console.error(
                "DCWriter执行表达式函数[" +
                strFunctionName +
                "]发生错误:" +
                err +
                ",函数参数值为:"
            );
            console.log(args);
        }
        return null;
    },
    /**
     * 根据innervalue列表求和
     * text:数值
     */
    SUMINNERVALUE: function () {
        //抄自四代wyc20241223
        if (arguments.length !== 1) {
            return null;
        }
        //console.log(arguments[0]);
        var parameters = arguments[0] != null ? arguments[0].toString().split(',') : null;
        if (parameters !== null && parameters.length > 0) {
            var result = 0;
            for (var i = 0; i < parameters.length; i++) {
                var j = parseFloat(parameters[i]);
                if (isNaN(j) === false) {
                    result = result + j;
                }
            }
            return result;
        } else {
            return null;
        }
    },
    /**
     * 返回指定角度的正弦值
     * text:数值
     */
    DAYDIFF: function () {
        if (arguments.length < 2 || arguments.length > 3) {
            return null;
        }
        var bit = parseInt(arguments[2]);
        if (isNaN(bit)) {
            bit = 0;
        }
        var dateone = null;
        var datetwo = null;
        var dateStr = DCTools20221228.StringConvert(arguments[0]);
        if (dateStr != null) {
            dateStr = dateStr.replace(/(^\s*)|(\s*$)/g, ''); //去除可能的前后空格
            dateStr = dateStr.toString().replace(/-/g, "/");//传入的时间字符串为yyyy-MM-dd hh:mm:ss，全部转换为/
            dateone = DCTools20221228.strToDate(dateStr); //分隔字符串，转换为时间
        }

        dateStr = DCTools20221228.StringConvert(arguments[1]);
        if (dateStr != null) {
            dateStr = dateStr.replace(/(^\s*)|(\s*$)/g, ''); //去除可能的前后空格
            dateStr = dateStr.toString().replace(/-/g, "/");//传入的时间字符串为yyyy-MM-dd hh:mm:ss，全部转换为/
            datetwo = DCTools20221228.strToDate(dateStr); //分隔字符串，转换为时间
        }

        if (dateone !== null && datetwo !== null && typeof (dateone.getTime) === "function" && typeof (datetwo.getTime) === "function") {
            var ms = datetwo.getTime() - dateone.getTime();
            var data = ms / (1000 * 3600 * 24);
            var daydiff = data.toFixed(bit);
            return parseFloat(daydiff);
        } else {
            return null;
        }
    },
    /**
     * 返回指定角度的正弦值
     * text:数值
     */
    HOURDIFF: function () {
        if (arguments.length < 2 || arguments.length > 3) {
            return null;
        }
        var bit = parseInt(arguments[2]);
        if (isNaN(bit)) {
            bit = 0;
        }
        var dateone = null;
        var datetwo = null;
        var dateStr = DCTools20221228.StringConvert(arguments[0]);
        if (dateStr != null) {
            dateStr = dateStr.replace(/(^\s*)|(\s*$)/g, ''); //去除可能的前后空格
            dateStr = dateStr.toString().replace(/-/g, "/");//传入的时间字符串为yyyy-MM-dd hh:mm:ss，全部转换为/
            dateone = DCTools20221228.strToDate(dateStr); //分隔字符串，转换为时间
        }

        dateStr = DCTools20221228.StringConvert(arguments[1]);
        if (dateStr != null) {
            dateStr = dateStr.replace(/(^\s*)|(\s*$)/g, ''); //去除可能的前后空格
            dateStr = dateStr.toString().replace(/-/g, "/");//传入的时间字符串为yyyy-MM-dd hh:mm:ss，全部转换为/
            datetwo = DCTools20221228.strToDate(dateStr); //分隔字符串，转换为时间
        }

        if (dateone !== null && datetwo !== null && typeof (dateone.getTime) === "function" && typeof (datetwo.getTime) === "function") {
            var ms = datetwo.getTime() - dateone.getTime();
            var data = ms / (1000 * 3600);
            var daydiff = data.toFixed(bit);
            return parseFloat(daydiff);
        } else {
            return null;
        }
    },
    /**
     * 四代BS移植的计算分钟差的公式
     * text:数值
     */
    MINUTEDIFF2: function () {
        if (arguments.length < 2 || arguments.length > 3) {
            return null;
        }
        var bit = parseInt(arguments[2]);
        if (isNaN(bit)) {
            bit = 0;
        }
        var dateone = null;
        var datetwo = null;
        var dateStr = DCTools20221228.StringConvert(arguments[0]);
        if (dateStr != null) {
            dateStr = dateStr.replace(/(^\s*)|(\s*$)/g, ''); //去除可能的前后空格
            dateStr = dateStr.toString().replace(/-/g, "/");//传入的时间字符串为yyyy-MM-dd hh:mm:ss，全部转换为/
            dateone = DCTools20221228.strToDate(dateStr); //分隔字符串，转换为时间
        }

        dateStr = DCTools20221228.StringConvert(arguments[1]);
        if (dateStr != null) {
            dateStr = dateStr.replace(/(^\s*)|(\s*$)/g, ''); //去除可能的前后空格
            dateStr = dateStr.toString().replace(/-/g, "/");//传入的时间字符串为yyyy-MM-dd hh:mm:ss，全部转换为/
            datetwo = DCTools20221228.strToDate(dateStr); //分隔字符串，转换为时间
        }

        if (dateone !== null && datetwo !== null && typeof (dateone.getTime) === "function" && typeof (datetwo.getTime) === "function") {
            var ms = datetwo.getTime() - dateone.getTime();
            var data = ms / (1000 * 60);
            var daydiff = data.toFixed(bit);;
            return parseFloat(daydiff);
        } else {
            return null;
        }
    },
    /**
     * 返回指定角度的正弦值
     * text:数值
     */
    SIN: function (text) {
        return Math.sin(text);
    },
    /**
     * 返回指定弧度的COS值
     * text:数值
     */
    COS: function (text) {
        return Math.cos(text);
    },
    /**
     * 前端新增用于计算多选框或多选下拉项选中项的最大值
     */
    MAXINGROUP: function () {
        //强制只接受一个或两个参数
        if (arguments.length !== 1) {
            return null;
        }
        var parameters = arguments[0].toString().split(",");
        if (parameters !== null && parameters.length > 0) {
            var result = null;
            for (var i = 0; i < parameters.length; i++) {
                var j = parseFloat(parameters[i]);
                if (isNaN(j) === false && (result === null || j >= result)) {
                    result = j;
                }
            }
            return result;
        } else {
            return null;
        }
    },
    /**
     * 前端新增用于计算多选框或多选下拉项选中项的最小值
     */
    MININGROUP: function () {
        //强制只接受一个或两个参数
        if (arguments.length !== 1) {
            return null;
        }
        var parameters = arguments[0].toString().split(",");
        if (parameters !== null && parameters.length > 0) {
            var result = null;
            for (var i = 0; i < parameters.length; i++) {
                var j = parseFloat(parameters[i]);
                if (isNaN(j) === false && (result === null || j <= result)) {
                    result = j;
                }
            }
            return result;
        } else {
            return null;
        }
    },
    /**
     * 前端新增用于计算多选框或多选下拉项的值求和计算
     */
    SUMINGROUP: function () {
        //强制只接受一个参数
        if (arguments.length < 1 || arguments.length > 2) {
            return null;
        }
        var sum = DCExpression.SUMINNERVALUE(arguments[0]);
        var limitmax = parseFloat(arguments[1]);
        if (isNaN(limitmax) === true) {
            return sum;
        }
        var result = sum;
        if (limitmax <= sum) {
            result = limitmax;
        }
        return result;
    },
    /**
     * 前端新增字符串连接函数
     */
    CONCATENATE: function () {
        var resultstr = "";
        for (var i = 0; i < arguments.length; i++) {
            var arg = arguments[i];
            if (arg != undefined && arg != null) {
                resultstr = resultstr + arg.toString();
            }
        }
        return resultstr;
    },
    /**
     * 新增对LOOKUP表达式公式的支持
     */
    LOOKUP: function () {
        if (arguments.length < 3) {
            return null;
        }
        var result = null;
        //wyc20240131:若参数为空则不进行比较
        if (arguments[0] === null || arguments[0] === "") {
            return null;
        }
        for (var i = 1; i <= arguments.length; i = i + 2) {
            if (isNaN(arguments[i]) === true || arguments[0] < arguments[i]) {
                if (result == null && i - 1 > 0 && isNaN(arguments[0]) === false) {
                    result = arguments[i - 1];
                }
                return result;
            } else if (i + 2 <= arguments.length && arguments[0] < arguments[i + 2]) {
                result = arguments[i + 1];
                console.log(result);
            }
        }
        return result;
    },
    /**
     * Boolean转换
     */
    IF: function (text, a, b) {
        // if (text == null) {
        //     return b; //false
        // }
        // var Result;
        // if (typeof text == "string") {
        //     if (text.length == 0) {
        //         Result = new Boolean(""); //false
        //     } else {
        //         Result = new Boolean("true"); //new Boolean("true")和new Boolean("false")相同结果，都是true
        //     }
        // } else {
        //     Result = new Boolean(text);
        // }
        if (text) {
            return a;
        } else {
            return b;
        }
    },
    /**
     * 计算年龄
     */
    AGE: function (text) {
        if (text == null) {
            return 0;
        }
        if (typeof text == "string") {
            if (text.length == 0) {
                return 0;
            }
        }
        var brithday = new Date(Date.parse(text.replace(/-/g, "/"))); //转换为日期格式
        var nowDate = new Date();
        return nowDate.getFullYear() - brithday.getFullYear();
    },
    /**
     * 计算月龄
     */
    AGEMONTH: function (text) {
        if (text == null) {
            return 0;
        }
        if (typeof text == "string") {
            if (text.length == 0) {
                return 0;
            }
        }
        var brithday = new Date(Date.parse(text.replace(/-/g, "/"))); //转换为日期格式
        var nowDate = new Date();

        var year1 = brithday.getFullYear();
        var year2 = nowDate.getFullYear();
        var month1 = brithday.getMonth();
        var month2 = nowDate.getMonth();

        return (year2 - year1) * 12 + (month2 - month1);
    },
    /**
     * 计算周龄
     */
    AGEWEEK: function (text) {
        if (text == null) {
            return 0;
        }
        if (typeof text == "string") {
            if (text.length == 0) {
                return 0;
            }
        }
        var brithday = new Date(Date.parse(text.replace(/-/g, "/"))).getTime(); //转换为日期格式,再取毫秒数
        var nowDate = new Date().getTime();
        var divNum = 1000 * 60 * 60 * 24 * 7;
        return this.CINT((nowDate - brithday) / divNum); //不计算余数
    },
    /**
     * 计算日期龄
     */
    AGEDAY: function (text) {
        if (text == null) {
            return 0;
        }
        if (typeof text == "string") {
            if (text.length == 0) {
                return 0;
            }
        }
        var brithday = new Date(Date.parse(text.replace(/-/g, "/"))).getTime(); //转换为日期格式,再取毫秒数
        var nowDate = new Date().getTime();
        var divNum = 1000 * 60 * 60 * 24;
        return this.CINT((nowDate - brithday) / divNum); //不计算余数
    },
    /**
     * 计算小时龄
     */
    AGEHOUR: function (text) {
        if (text == null) {
            return 0;
        }
        if (typeof text == "string") {
            if (text.length == 0) {
                return 0;
            }
        }
        var brithday = new Date(Date.parse(text.replace(/-/g, "/"))).getTime(); //转换为日期格式,再取毫秒数
        var nowDate = new Date().getTime();
        var divNum = 1000 * 60 * 60;
        return this.CINT((nowDate - brithday) / divNum); //不计算余数
    },
    /**
     * 精确查找下拉项中是否有某一项
     */
    FINDINDEX: function (text, stringValue) {
        if (
            typeof text !== "string" ||
            typeof stringValue !== "string" ||
            text.length === 0 ||
            stringValue.length === 0
        ) {
            return -1;
        }
        var txts = stringValue.split(",");
        for (var i = 0; i < txts.length; i++) {
            if (text === txts[i]) {
                return this.FIND(text + ",", stringValue);
            }
        }
        return -1;
    },
    /**
     * 返回第一个文本串在第二个字符串位置的值
     */
    FIND: function (text, stringValue) {
        if (text == null || stringValue == null) {
            return -1;
        }
        //wyc20220722:修改逻辑
        var txt = text.toString();
        var str = stringValue.toString();
        if (txt.length == 0 || str.length == 0) {
            return -1;
        }
        var result = str.indexOf(txt);
        return result;
    },
    /**
     * 字符串长度
     */
    LEN: function (text) {
        if (text == null) {
            return 0;
        }
        return text.toString().length;
    },
    /**
     * 未实现
     */
    // PARAMETER: function () {
    //     return null
    // },
    /**
     * 返回绝对值
     */
    ABS: function (text) {
        return Math.abs(text);
    },
    /**
     * 返回指定弧度的反余弦值
     */
    ACOS: function (text) {
        return Math.acos(text);
    },
    /**
     * 返回指定弧度的反正弦值
     */
    ASIN: function (text) {
        return Math.asin(text);
    },
    /**
     * 返回指定弧度的反正切值
     */
    ATAN: function (text) {
        return Math.asin(text);
    },
    /**
     * 返回指定弧度的正切值
     */
    ATAN2: function (y, x) {
        if (isNaN(y) || isNaN(x)) {
            return Number.NaN;
        }
        return Math.atan2(y, x);
    },
    /**
     * 返回算术平均值
     */
    AVERAGE: function () {
        var total = 0;
        var count = 0;
        var iCount = 0;
        for (iCount = 0; iCount < arguments.length; iCount++) {
            if (isNaN(arguments[iCount]) == false) {
                total = total + arguments[iCount];
                count = count + 1;
            }
        }
        if (count > 0) {
            return total / count;
        }
        return Number.NaN;
    },
    /**
     * 获得最近的整数
     */
    CEILING: function (text) {
        if (isNaN(text)) {
            return Number.NaN;
        }
        var c = Math.ceil(text);
        var f = Math.floor(text);
        var m = (c + f) / 2;
        if (m > text) {
            return c;
        } else if (m == text) {
            if (c % 2 == 0) {
                return c;
            } else {
                return f;
            }
        } else {
            return f;
        }
    },
    /**
     * 返回参数的个数
     */
    COUNT: function () {
        var count = 0;
        var iCount = 0;
        for (iCount = 0; iCount < arguments.length; iCount++) {
            if (arguments[iCount] != null && isNaN(arguments[iCount]) == false) {
                count = count + 1;
            }
        }
        return count;
    },
    /**
     * 返回e的n次方
     */
    EXP: function (text) {
        return Math.exp(text);
    },
    /**
     * 返回向下舍入取整数
     */
    FLOOR: function (text) {
        return Math.floor(text);
    },
    /**
     * 将数值向小取整为最接近的整数
     */
    INT: function (text) {
        return Math.floor(text);
    },
    /**
     * 根据给定底数返回数字的对数
     */
    LOG: function (b, a) {
        if (isNaN(b) || isNaN(a)) {
            return Number.NaN;
        } else {
            var tmp = Math.log(bb) / Math.log(aa);
            return Math.round(1000000 * tmp) / 1000000;
        }
    },
    /**
     * 返回以10为底的对数
     */
    LOG10: function (text) {
        if (isNaN(text)) {
            return Number.NaN;
        } else {
            var tmp = Math.log(text) / Math.log(10);
            return Math.round(1000000 * tmp) / 1000000;
        }
    },
    /**
     * 返回最大值
     */
    MAX: function () {
        var result = Number.NaN;
        var iCount = 0;
        for (iCount = 0; iCount < arguments.length; iCount++) {
            if (isNaN(result)) {
                result = arguments[iCount];
            } else if (isNaN(arguments[iCount]) == false) {
                if (result < arguments[iCount]) {
                    result = arguments[iCount];
                }
            }
        }
        return result;
    },
    /**
     * 返回最小值
     */
    MIN: function () {
        var result = Number.NaN;
        var iCount = 0;
        for (iCount = 0; iCount < arguments.length; iCount++) {
            if (isNaN(result)) {
                result = arguments[iCount];
            } else if (isNaN(arguments[iCount]) == false) {
                if (result > arguments[iCount]) {
                    result = arguments[iCount];
                }
            }
        }
        return result;
    },
    /**
     * 返回两个数相除的余数
     */
    MOD: function (a, b) {
        if (isNaN(a) || isNaN(b)) {
            return Number.NaN;
        }
        return a % b;
    },
    /**
     * 将正（负）数向上（下）舍入到最接近的奇数
     */
    ODD: function (text) {
        if (isNaN(text)) {
            return Number.NaN;
        } else {
            var tmp = Math.ceil(Math.abs(text));
            if (tmp % 2 == 0) {
                tmp++;
            }
            if (text < 0) {
                return 0 - tmp;
            } else {
                return tmp;
            }
        }
    },
    /**
     * 返回某数的乘幂
     */
    POW: function (a, b) {
        if (isNaN(a) || isNaN(b)) {
            return Number.NaN;
        }
        return Math.pow(a, b);
    },
    /**
     * 计算所有参数的乘积
     */
    PRODUCT: function () {
        var total = 1;
        //wyc20241108:重写DUWRITER5_0-3848
        for (var iCount = 0; iCount < arguments[0].length; iCount++) {
            var val = Number.NaN;
            var arg = arguments[0][iCount];
            var argtype = typeof (arg);
            if (argtype === "number") {
                val = arg;
            }
            else if (argtype === "string") {
                if (arg.indexOf(".") >= 0) {
                    val = parseFloat(arg);
                } else {
                    val = parseInt(arg);
                }
            }
            if (isNaN(val) === false) {
                total = total * val;
            }
        }
        return total;
    },
    /**
     * 将角度转换为弧度
     */
    RADIANS: function (text) {
        return (Math.PI / 180) * text;
    },
    /**
     * 返回一个介于0到1之间的随机数
     */
    RAND: function () {
        return Math.random();
    },
    /**
     * 进行四舍五入计算
     */
    ROUND: function (text, num) {
        //wyc20241108重写DUWRITER5_0-3844
        var val = parseFloat(text);
        if (isNaN(val)) {
            return Number.NaN;
        }
        var num2 = parseInt(num);
        if (isNaN(num2) === true || num2 < 0) {
            num2 = 0;
        }

        //20240412 lxy 保留一位小数（DUWRITER5_0-2283）
        const factor = Math.pow(10, num2); // 使用次幂保留小数位数
        return Math.round(val * factor) / factor; // 四舍五入并缩小到原值
    },
    /**
     * 向下舍入数字
     */
    ROUNDDOWN: function (text) {
        if (isNaN(text)) {
            return Number.NaN;
        }
        return Math.floor(text);
    },
    /**
     * 向上舍入数字
     */
    ROUNDUP: function (text) {
        if (isNaN(text)) {
            return Number.NaN;
        }
        return Math.ceil(text);
    },
    /**
     * 为正数返回1，为零返回0，为负数返回-1
     */
    SIGN: function (text) {
        if (isNaN(text)) {
            return Number.NaN;
        } else {
            if (text > 0) {
                return 1;
            } else if (text == 0) {
                return 0;
            } else {
                return -1;
            }
        }
    },
    /**
     * 返回数值的平方根
     */
    SQRT: function (text) {
        if (isNaN(text)) {
            return Number.NaN;
        }
        return Math.sqrt(text);
    },
    /**
     * 计算所有数值的和
     */
    SUM: function () {
        var that = this;
        var total = 0;
        var iCount = 0;
        //wyc20221012:设法规避JS底层求和精度缺失的问题
        var args2 = new Array();
        var maxdigit = 0;

        //lxy20240131:sum适配表达式有多种参数类型：数字，数组，数字数组的组合。增加flattenArray方法兼容以上情况
        var sumTarget = that.flattenArray(arguments);
        //wyc20240130:若参数全为空则求和返回空而不是返回零
        var allblank = true;
        for (iCount = 0; iCount < sumTarget.length; iCount++) {
            var digitstr = sumTarget[iCount];
            if (digitstr !== "" && digitstr !== null && isNaN(digitstr) == false) {
                allblank = false;
                args2.push(digitstr);
                //计算求和项的最大小数位数
                if (digitstr) {
                    var str = digitstr.toString();
                    var strs = str.split(".");
                    if (strs.length == 2) {
                        maxdigit = Math.max(maxdigit, strs[1].length);
                    }
                }
            }
        }
        if (allblank === true) {
            return null;
        }

        var pow = Math.pow(10, maxdigit);
        for (var i = 0; i < args2.length; i++) {
            total = total + args2[i] * pow;
        }
        return total / pow;
    },
    /**
     * 前端新增用于计算多选框或多选下拉项的值求和计算
     */
    SUMINNERVALUE: function () {
        //强制只接受一个参数
        if (arguments.length !== 1) {
            return null;
        }
        //console.log(arguments[0]);
        var parameters =
            arguments[0] != null ? arguments[0].toString().split(",") : null;
        if (parameters !== null && parameters.length > 0) {
            var result = 0;
            for (var i = 0; i < parameters.length; i++) {
                var j = parseFloat(parameters[i]);
                if (isNaN(j) === false) {
                    result = result + j;
                }
            }
            return result;
        } else {
            return null;
        }
    },
    /**
     * 返回指定角度的正切值
     */
    TAN: function (text) {
        return Math.tan(text);
    },
    /**
     * 将数据转换为一个整数
     */
    CINT: function (text, defaultValue) {
        if (isNaN(text)) {
            return defaultValue;
        }
        return parseInt(text);
    },
    /**
     * 将数据转换为一个双精度浮点数
     */
    CDOUBLE: function (text, defaultValue) {
        if (isNaN(text)) {
            return defaultValue;
        }
        return parseFloat(text);
    },
    /**
     * 前端新增节气计算函数
     */
    SOLARTERM: function () {
        //强制只接受一个参数
        if (arguments.length !== 1 || arguments[0].toString == undefined) {
            return null;
        }
        var date1 = new Date(arguments[0].toString());
        var sTermInfo = new Array(
            0,
            21208,
            42467,
            63836,
            85337,
            107014,
            128867,
            150921,
            173149,
            195551,
            218072,
            240693,
            263343,
            285989,
            308563,
            331033,
            353350,
            375494,
            397447,
            419210,
            440795,
            462224,
            483532,
            504758
        );
        var SolarTerm = new Array(
            "小寒",
            "大寒",
            "立春",
            "雨水",
            "惊蛰",
            "春分",
            "清明",
            "谷雨",
            "立夏",
            "小满",
            "芒种",
            "夏至",
            "小暑",
            "大暑",
            "立秋",
            "处暑",
            "白露",
            "秋分",
            "寒露",
            "霜降",
            "立冬",
            "小雪",
            "大雪",
            "冬至"
        );
        var baseDateAndTime;
        var num;
        var y;
        var tempStr = "";

        y = date1.getFullYear();
        Date.prototype.GetDayIndex = function () {
            var dateArr = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
            var day = this.getDate();
            var month = this.getMonth(); //getMonth()是从0开始
            var year = this.getFullYear();
            var result = 0;
            for (var i = 0; i < month; i++) {
                result += dateArr[i];
            }
            result += day;
            //判断是否闰年
            if ((month > 1 && year % 4 == 0 && year % 100 != 0) || year % 400 == 0) {
                result += 1;
            }
            return result;
        };

        for (var i = 1; i <= 24; i++) {
            num = 525948.76 * (y - 1900) + sTermInfo[i - 1];
            baseDateAndTime = new Date(1900, 0, 6, 2, 5, 0);
            baseDateAndTime.setMinutes(baseDateAndTime.getMinutes() + num);
            var dayOfNewDate = baseDateAndTime.GetDayIndex();
            var dayOfDate1 = date1.GetDayIndex();
            if (dayOfNewDate == dayOfDate1) {
                tempStr = SolarTerm[i - 1];
                break;
            }
        }
        return tempStr;
    },
    /**
     * 计算时间差
     */
    MINUTEDIFF: function (date1, date2) {
        // 将中文日期转换为标准日期格式
        function chineseDateToStandardDate(chineseDate) {
            var year = chineseDate.match(/\d+/)[0];
            var month = chineseDate.match(/年(\d+)月/)[1];
            var day = chineseDate.match(/月(\d+)日/)[1];
            return year + '-' + month.padStart(2, '0') + '-' + day.padStart(2, '0');
        }
        if (date1 && date2) {
            var difference = 0;
            //判断是否为时间格式如：12:00:01
            var timeRegex = /^(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$/;
            if (timeRegex.test(date1) && timeRegex.test(date2)) {
                var time1Date = new Date("1970-01-01T" + date1 + "Z");
                var time2Date = new Date("1970-01-01T" + date2 + "Z");
                difference = Math.abs(time2Date.getTime() - time1Date.getTime());
            } else {
                var regex = /(\d{4}年|\d{2}月|\d{2}日)/;
                //字符串1中包含年、月或日
                if (regex.test(date1)) {
                    date1 = chineseDateToStandardDate(date1);
                }
                //字符串2中包含年、月或日
                if (regex.test(date2)) {
                    date2 = chineseDateToStandardDate(date2);
                }
                // 将日期字符串转换为日期对象
                let d1 = new Date(date1);
                let d2 = new Date(date2);
                // // 计算时间差（以毫秒为单位）
                difference = Math.abs(d2 - d1);
            }
            // // 将时间差转换为天数、小时数、分钟数和秒数
            const days = Math.floor(difference / (1000 * 60 * 60 * 24));
            const hours = Math.floor((difference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            const minutes = Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60));
            const seconds = Math.floor((difference % (1000 * 60)) / 1000);
            // console.log(days, hours, minutes, seconds)
            // 返回时间差的对象
            return `${days ? (days + "天") : ''}${hours ? (hours + "小时") : ''}${minutes ? (minutes + "分钟") : ''}${seconds ? (seconds + "秒") : ''}`;
        }
        return '';

    },
    /**
    * 增加天
    */
    ADDDAY: function (dateString, daysToAdd) {
        var date = new Date(dateString);
        date.setDate(date.getDate() + daysToAdd);

        var year = date.getFullYear();
        var month = (date.getMonth() + 1).toString().padStart(2, '0');
        var day = date.getDate().toString().padStart(2, '0');
        var hours = date.getHours().toString().padStart(2, '0');
        var minutes = date.getMinutes().toString().padStart(2, '0');
        var seconds = date.getSeconds().toString().padStart(2, '0');

        var newDate = year + '-' + month + '-' + day + ' ' + hours + ':' + minutes + ':' + seconds;

        return newDate;
    },
    /**
    * 增加小时
    */
    ADDHOUR: function (dateString, hoursToAdd) {
        var timeRegex = /^(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$/;//判断格式03:00:00
        var newDate = '';
        //时间格式：
        if (timeRegex.test(dateString)) {
            var timeParts = dateString.split(':');
            var date = new Date();
            date.setHours(parseInt(timeParts[0]) + hoursToAdd);
            date.setMinutes(parseInt(timeParts[1]));
            date.setSeconds(parseInt(timeParts[2]));
            var hours = date.getHours().toString().padStart(2, '0');
            var minutes = date.getMinutes().toString().padStart(2, '0');
            var seconds = date.getSeconds().toString().padStart(2, '0');
            newDate = hours + ':' + minutes + ':' + seconds;
        } else {
            //日期时间格式：
            var date = new Date(dateString);
            date.setHours(date.getHours() + hoursToAdd);
            var year = date.getFullYear();
            var month = (date.getMonth() + 1).toString().padStart(2, '0');
            var day = date.getDate().toString().padStart(2, '0');
            var hours = date.getHours().toString().padStart(2, '0');
            var minutes = date.getMinutes().toString().padStart(2, '0');
            var seconds = date.getSeconds().toString().padStart(2, '0');
            newDate = year + '-' + month + '-' + day + ' ' + hours + ':' + minutes + ':' + seconds;
        }
        return newDate;
    },
    /**
    * 增加分钟
    */
    ADDMINUTES: function (dateTime, minutes) {
        var timeRegex = /^(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$/;//判断格式03:00:00
        //时间格式：
        if (timeRegex.test(dateTime)) {
            var timeParts = dateTime.split(':');
            var date = new Date();
            date.setHours(parseInt(timeParts[0]));
            date.setMinutes(parseInt(timeParts[1]) + minutes);
            date.setSeconds(parseInt(timeParts[2]));
            var hours = date.getHours().toString().padStart(2, '0');
            var minutes = date.getMinutes().toString().padStart(2, '0');
            var seconds = date.getSeconds().toString().padStart(2, '0');
            var newDate = hours + ':' + minutes + ':' + seconds;
            return newDate;
        }
        //日期时间格式：
        var newDateTime = new Date(dateTime);
        newDateTime.setMinutes(newDateTime.getMinutes() + minutes);
        return newDateTime;
    },
    /**
     * 数组扁平化的方法（不是表达式）
     */
    flattenArray: function (arr) {
        var that = this;
        let result = [];
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            if (Array.isArray(item)) {
                result = result.concat(that.flattenArray(item));
            } else {
                result.push(item);
            }
        }
        return result;
    }
};
