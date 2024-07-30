window.isIE = function () { //ie?  
    if (!!window.ActiveXObject || "ActiveXObject" in window)
        return true;
    else
        return false;
};
(function (a, b) {
    //局部函数  验证是IE浏览器，且IE版本 v及以下（是返回true，否则返回false）
    var checkIEVersionLTE = function (v) {
        if (!window.isIE()) {
            return false;
        }
        else {
            //ie version
            var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
            reIE.test(navigator.userAgent);
            var fIEVersion = parseFloat(RegExp["$1"]);
            if (!isNaN(fIEVersion) && fIEVersion >= v + 1) {
                return false;
            }
            else if (navigator.userAgent.indexOf('Mozilla/5.0') != -1) {
                return false;
            }
            else {
                return true;
            }
        }
    };
    //是否是低版本的IE浏览器（IE8及以下，是返回true，否则返回false）
    window.isIElte8 = function () {
        var ck = checkIEVersionLTE(8);
        if (ck) {
            if (navigator.userAgent.indexOf('Mozilla/5.0') != -1) {
                ck = false; //Edge
            }
        }
        return ck;
    };
    var jqlink = '';
    if (!window.isIElte8()) {
        jqlink = 'jquery-2.1.1.min.js';
    }
    else {
        jqlink = 'jquery-1.9.1.min.js';
    }
    if (!!jqlink) {
        var a = document.scripts,
            b = a[a.length - 1],
            src = b.src;
        var path = src.substring(0, src.lastIndexOf("/") + 1);

        //var e = document.getElementsByTagName('head')[0];
        //var h = document.createElement("script");

        jqlink = jqlink.replace(/\s/g, "");

        //h["src"] = path + jqlink;
        //e.appendChild(h);

        document.write('<script type="text/javascript" src="' + path + jqlink + '"></script>');
    }
})(window);