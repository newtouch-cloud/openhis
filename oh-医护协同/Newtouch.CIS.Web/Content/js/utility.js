var chinese = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九'];
var len = ['十'];
var ydm = ['年', '月', '日'];
var hm = ['时', '分'];
function etchinese(s) {
    //将单个数字转化成中文  
    s = "" + s;
    var result = "";
    for (var i = 0; i < s.length; i++) {
        result += chinese[s.charAt(i)];
    }
    return result;
}
function specialcharacter(s) {
    //对特殊情况进行处理，并调用etchinese(s)方法，返回相关的中文  
    s = "" + s;
    var result = "";
    if (s.length == 2) {
        if (s.charAt(0) == "1") {
            if (s.charAt(1) == "0") return len[0];
            return len[0] + chinese[s.charAt(1)];
        }
        if (s.charAt(1) == "0") return chinese[s.charAt(0)] + len[0];
        return chinese[s.charAt(0)] + len[0] + chinese[s.charAt(1)];
    }
    return etchinese(s)
}
function transformchinese(s) {
    //验证输入的日期格式，并调用specialcharacter(s)方法，将相关数字转化为中文  
    var datePat = /^(\d{2}|\d{4})(\/|-)(\d{1,2})(\2)(\d{1,2})$/;
    var matchArray = s.match(datePat);
    var ok = "";
    if (matchArray == null) return false;
    for (var i = 1; i < matchArray.length; i = i + 2) {
        ok += specialcharacter(matchArray[i] - 0) + ydm[(i - 1) / 2];
    }
    return ok;
}  

function transToChineseHM(hours, minutes) {
    // 映射个位数用
    var onesPlace = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十']
    // 两位数的个位数映射有点不同
    var onesPlaceForTensDigit = ['', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十']
    // 映射十位数
    var tensPlace = ['', '十', '二十', '三十', '四十', '五十']
    // 数字转化为中文函数
    var t = num => num <= 10 ? onesPlace[num] : tensPlace[Math.floor(num / 10)] + onesPlaceForTensDigit[Math.floor(num % 10)]
    // 数字时间转化为中文时间函数
    return t(hours) + '点' + t(minutes) + '分';
    //return hours < 6 ? '凌晨' + t(hours) + '点' + t(minutes) + '分' : hours < 12 ? '上午' + t(hours) + '点' + t(minutes) + '分' : hours < 18 ? '下午' + t(hours - 12) + '点' + t(minutes) + '分' : '晚上' + t(hours - 12) + '点' + t(minutes) + '分';

}

