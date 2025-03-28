$(function () {
    document.body.className = localStorage.getItem('config-skin'); if ($("[data-toggle='tooltip']").length) { $("[data-toggle='tooltip']").tooltip(); }
    $(document).keydown(function (event) { event = event || window.event; if (event.keyCode == 13) { if (top.top.$('.layui-layer-dialog').length == 1) { top.top.$('.layui-layer-dialog').find('.btn-primary').trigger('click'); } } }); $('select[id].form-an').on("select2:close", function (e) { $(this).parents('.has-error').find('i.error').remove(); $(this).parents('.has-error').removeClass('has-error'); $(this).removeClass('error'); });
})
//AppId
$.getCurrentAppId = function () {
    var temp = $('body:eq(0)').attr('data-appid');
    if (temp)
        return temp;
    else
        return "";
};
$.getLocationHost = function () {
    var reg = new RegExp("^[^/]*//[^/]*");
    var matched = reg.exec(location.href);
    if (matched.length > 0 && location.href.StartWith(matched[0])) {
        return matched[0];
    }
    return null;
}
$.getUrlWithHost = function (url) {
    if (!!url && url.StartWith('/')) {
        var locationHost = $.getLocationHost();
        if (!!locationHost) {
            return locationHost + url;
        }
    }
    return url;
}
//
$.htmlEncode = function (value) { return $('<div/>').text(value).html(); }
$.htmlDecode = function (value) { return $('<div/>').html(value).text(); }
$.reload = function () { location.reload(); return false; }
$.najax = function (options) {
    var opts = $.extend({ alerterror: true, alertbierror: true, cache: false, dataType: "json" }, options); opts.success = function (data, st, xhr) {
        if (opts.alerterror) {
            if (!$.xhrSuccessDataExCheckHandle(data, opts.alertbierror)) {
                if (options.errorCallback) { options.errorCallback(data, st, xhr); }
                return false;
            }
        }
        if (options.success) {
            if (!(options.loading === false) || options.loadingtext) {
                $.loading(false);
            }
            options.success(data, st, xhr);
        }
    }
    if (!(options.loading === false) || options.loadingtext) { $.loading(true, options.loadingtext); opts.error = function (XMLHttpRequest, textStatus, errorThrown) { $.loading(false); $.modalMsg(errorThrown, "error"); if (options.error) { options.error(XMLHttpRequest, textStatus, errorThrown); } }; opts.complete = function () { $.loading(false); if (options.complete) { options.complete(); } }; }
    $.ajax(opts);
}
$.loading = function (bool, text) {
    try {
        var $loadingpage = top.$("#loadingPage"); var $loadingtext = $loadingpage.find('.loading-content'); if (bool) { $loadingpage.show(); } else { if ($loadingtext.attr('istableloading') == undefined) { $loadingpage.hide(); } }
        if (!!text) { $loadingtext.html(text); } else { $loadingtext.html("数据加载中，请稍后…"); }

        var iiiiiw = top.$('body').width();
        var iiiiih = top.$('body').height();
        if (window.top == window) {
            var iiiiiw = $(window).width();
            var iiiiih = $(window).height();
        }
        $loadingtext.css("left", (iiiiiw - $loadingtext.width()) / 2 - 50);
        $loadingtext.css("top", (iiiiih - $loadingtext.height()) / 2);
    }
    catch (ex) { }
}
$.request = function (name) {
    var search = location.search.slice(1); var arr = search.split("&"); for (var i = 0; i < arr.length; i++) { var ar = arr[i].split("="); if (ar[0] == name) { if (decodeURIComponent(ar[1]) == 'undefined') { return ""; } else { return decodeURIComponent(ar[1]); } } }
    return "";
}
$.encodeRequestParameters = function (params) {
    var resultParams = "";
    var arr = params.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (resultParams) {
            resultParams += "&";
        }
        resultParams += ar[0];
        resultParams += "=";
        if (ar[1]) {
            resultParams += encodeURIComponent(ar[1]);
        }
    }
    return resultParams;
}
$.currentWindow = function (iframeId) {
    if (!!!iframeId) {
        iframeId = top.$(".Newtouch_iframe:visible").attr("id");
    }
    if (!!!iframeId) {
        return null;    //not found
    }
    for (var frameIndex = 0; frameIndex < top.frames.length; frameIndex++) {
        try {
            var thisIdxWindow = null;
            thisIdxWindow = top.top.window[frameIndex];
            if (thisIdxWindow && thisIdxWindow.frameElement.id && thisIdxWindow.frameElement.id == iframeId) {
                return thisIdxWindow;
            }
        }
        catch (err) {
            //跨域 等
            //保证continue
        }
    }
    return null;
}
$.browser = function () {
    var userAgent = navigator.userAgent; var isOpera = userAgent.indexOf("Opera") > -1; if (isOpera) { return "Opera" }; if (userAgent.indexOf("Firefox") > -1) { return "FF"; }
    if (userAgent.indexOf("Chrome") > -1) { if (window.navigator.webkitPersistentStorage.toString().indexOf('DeprecatedStorageQuota') > -1) { return "Chrome"; } else { return "360"; } }
    if (userAgent.indexOf("Safari") > -1) { return "Safari"; }
    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) { return "IE"; };
}
$.download = function (url, data, method) { if (url && data) { data = typeof data == 'string' ? data : jQuery.param(data); var inputs = ''; $.each(data.split('&'), function () { var pair = this.split('='); inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />'; }); $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove(); }; };
$.modalOpen = function (options) {
    options.url = $.getUrlWithHost(options.url);
    var defaults = {
        id: null,
        title: '系统窗口',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['确认', '关闭'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null,
        showleftlalbel: false,
        leftlalbelchecked: true,
        leftlalbelcheckedasClose: true,
    };
    var options = $.extend(defaults, options);
    var _width = options.width;
    var _height = options.height;
    try {
        _width = top.$(window).width() > parseInt(options.width.replace('px', '')) ? options.width : top.$(window).width() + 'px';
        _height = top.$(window).height() > parseInt(options.height.replace('px', '')) ? options.height : top.$(window).height() + 'px';
    } catch (ex) { }
    var configss = {
        id: options.id,
        closeBtn: options.closeBtn,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        showleftlalbel: options.showleftlalbel,
        leftlabel: {
            text: options.leftlabeltext || "确认并关闭"
            , checked: options.leftlalbelchecked
        },
        yes: function () {
            if (options.showleftlalbel === true) {
                options.callBack(options.id
                    , options.leftlalbelcheckedasClose ? top.$("#leftlabel").is(":checked") : !top.$("#leftlabel").is(":checked"));
            }
            else {
                options.callBack(options.id);
            }
        }, cancel: function () {
            if (options.cancelCallBack) {
                return options.cancelCallBack(options.id, "cancel");
            }
            else {
                return true;
            }
        }
        , no: function () {
            if (options.cancelCallBack) {
                return options.cancelCallBack(options.id, "no");
            }
            else {
                return true;
            }
        }
    }
    top.layer.open(configss);
}
$.modalConfirm = function (content, callBack, close, confirmOptions) { var defaults = { icon: "fa-exclamation-circle", title: "系统提示", btn: ['确认', '取消'], btnclass: ['btn btn-primary', 'btn btn-danger'], }; var confirmOptions = $.extend(defaults, confirmOptions); top.layer.confirm(content, confirmOptions, function () { callBack(true); if (!(close === false)) { top.layer.closeAll('dialog'); } }, function () { callBack(false); }); }
$.modalAlert = function (content, type) {
    var icon = ""; if (type == 'success') { icon = "fa-check-circle"; }
    if (type == 'error') { icon = "fa-times-circle"; }
    if (type == 'warning') { icon = "fa-exclamation-circle"; }
    top.layer.alert(content, { icon: icon, title: "系统提示", btn: ['确认'], btnclass: ['btn btn-primary'], }); if (top.top.$('.layui-layer-dialog').length == 1) { top.top.$('.layui-layer-dialog').trigger('focus'); }
}
$.modalMsg = function (content, type, msgtime) {
    if (!msgtime) { msgtime = 2000; }
    if (type != undefined) {
        var icon = ""; if (type == 'success') { icon = "fa-check-circle"; }
        if (type == 'ok') { icon = "fa-check-circle-o"; }
        if (type == 'error') { icon = "fa-times-circle"; }
        if (type == 'warning') { icon = "fa-exclamation-circle"; }
        top.layer.msg(content, { icon: icon, time: msgtime, shift: 5 }); top.$(".layui-layer-msg").find('i.' + icon).parents('.layui-layer-msg').addClass('layui-layer-msg-' + type);
    } else { top.layer.msg(content); }
}

$.modalClose = function (iframeId) {
    try {
        if (!iframeId) {
            if (window.frameElement && window.frameElement.id) {
                iframeId = window.frameElement.id;
            }
            else {
                iframeId = window.name;
            }
        }
        var index = top.layer.getFrameIndex(iframeId); //先得到当前iframe层的索引
        var $IsdialogClose = top.$("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
        var IsClose = $IsdialogClose.is(":checked");
        if ($IsdialogClose.length == 0) {
            IsClose = true;
        }
        if (IsClose) {
            top.layer.close(index);
        } else {
            location.reload();
        }
    }
    catch (modalCloseExcption) { }
}
$.getFailedAlertMessage = function (data) {
    var msg = data.message; if (data.code) {
        var msgMapList;
        try {
            msgMapList = top.top.$.clients.get($.getCurrentAppId(), "SysFailedCodeMessageMapList");
        }
        catch (eeeeeeeee) {
            //为了兼容以前的版本
            msgMapList = top.top.clients.SysFailedCodeMessageMapList;
        }
        if (msgMapList) { for (i = 0; i < msgMapList.length; i++) { if (data.code == msgMapList[i].code) { msg = msgMapList[i].msg; i = msgMapList.length; } } }
    }
    if (!msg) { msg = data.msg; }
    if (!msg) { msg = data.message; }
    if (!msg) { msg = "错误代码：" + data.code; }
    return msg;
}; $.xhrSuccessDataExCheckHandle = function (data, alertbierror) {
    if (data && data.state == 'error') {
        if (data.code === 'SESSION_TIMEOUT') { top.top.location.href = '/Login/Index'; return false; }
        else if (data.code === 'SESSION_SIDELINED') { top.top.location.href = '/Login/Index'; return false; }
        else if (data.code === 'SYSTEM_ERROR') {
            $.modalConfirm('程序发生内部错误<br/>点击“异常详情”按钮查看详情', function (flag) {
                if (flag) { ; }
                else { var exStackTrace = $.htmlEncode(data.exStackTrace); top.layer.open({ type: 1, title: '异常详情', area: ['700px', '500px'], closeBtn: 1, shadeClose: true, skin: 'divSysExceptionDetail', content: '<h3>' + data.message + '</h3><div>' + exStackTrace + '</div>' }); }
            }, null, { btn: ['确认', '异常详情'] });
        }
        else { if (alertbierror === undefined || alertbierror === true) { $.modalAlert($.getFailedAlertMessage(data), data.state); } }
        return false;
    }
    return true;
}
$.submitForm = function (options) {
    var defaults = { url: "", param: [], loading: "正在提交数据...", success: null, close: true, successwithtipmsg: true }; var options = $.extend(defaults, options); $.loading(true, options.loading); window.setTimeout(function () {
        if ($('[name=__RequestVerificationToken]').length > 0) { options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val(); }
        $.najax({
            url: options.url, data: options.param, type: "post", loadingtext: options.loading, success: function (data) {
                if (options.successwithtipmsg && data.message) { $.modalMsg(data.message, data.state); }
                options.success(data);
                if (options.close == true) {
                    $.modalClose();
                }
            }
        });
    }, 10);
}
$.deleteForm = function (options) {
    var defaults = { prompt: "注：您确定要删除该项数据吗？", url: "", param: [], loading: "正在删除数据...", success: null, close: true }; var options = $.extend(defaults, options); if ($('[name=__RequestVerificationToken]').length > 0) { options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val(); }
    $.modalConfirm(options.prompt, function (r) {
        if (r) {
            $.loading(true, options.loading); window.setTimeout(function () {
                $.ajax({
                    url: options.url, data: options.param, type: "post", dataType: "json", success: function (data) {
                        $(".operate").animate({ "left": '-100.1%' }, 200); if (!$.xhrSuccessDataExCheckHandle(data)) { return false; }
                        options.success(data); $.modalMsg(data.message, data.state);
                    }, error: function (XMLHttpRequest, textStatus, errorThrown) { $.loading(false); $.modalMsg(errorThrown, "error"); }, beforeSend: function () { $.loading(true, options.loading); }, complete: function () { $.loading(false); }
                });
            }, 10);
        }
    });
}
$.jsonWhere = function (data, action) {
    if (action == null) return; var reval = new Array(); $(data).each(function (i, v) { if (action(v)) { reval.push(v); } })
    return reval;
}
$.fn.jqGridRowValue = function () {
    var $grid = $(this); var selectedRowIds = $grid.jqGrid("getGridParam", "selarrrow"); if (selectedRowIds != "") {
        var json = []; var len = selectedRowIds.length; for (var i = 0; i < len; i++) { var rowData = $grid.jqGrid('getRowData', selectedRowIds[i]); json.push(rowData); }
        return json;
    } else { return $grid.jqGrid('getRowData', $grid.jqGrid('getGridParam', 'selrow')); }
}
$.fn.formValid = function (options) { var defaults = { errorPlacement: function (error, element) { element.parents('.formValue').addClass('has-error'); element.parents('.has-error').find('i.error').remove(); element.parents('.has-error').append('<i class="form-control-feedback fa fa-exclamation-circle error" data-placement="left" data-toggle="tooltip" title="' + error + '"></i>'); $("[data-toggle='tooltip']").tooltip(); if (element.parents('.input-group').hasClass('input-group')) { element.parents('.has-error').find('i.error').css('right', '33px') } }, success: function (element) { element.parents('.has-error').find('i.error').remove(); element.parents('.has-error').removeClass('has-error'); } }; var options = $.extend(defaults, options); return $(this).valid(options); }
$.fn.formSerialize = function (formdate, forlabel) {
    var element = $(this); if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('#' + key); var value = $.trim(formdate[key]).replace(/&nbsp;/g, ''); var type = $id.attr('type'); if ($id.hasClass("select2-hidden-accessible")) { type = "select"; }
            switch (type) {
                case "checkbox": if (value == "true" || value == "1") { $id.attr("checked", 'checked'); } else { $id.removeAttr("checked"); }
                    break; case "select": if ($id.find('option[value="' + value + '"]').length > 0) { $id.val(value).trigger("change"); }
                    break; case 'radio': $('input[name="' + key + '"]').each(function () { var $this = $(this); if ($this.val() == value) { $this[0].checked = true; $this.parent().addClass('active'); } }); break; default: $id.val(value); if (forlabel === true && $id.length > 0 && $id[0].tagName == 'LABEL') { $id.html(value); }
                    break;
            }
        }; return false;
    }
    var postdata = {}; element.find('input,select,textarea,hidden').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        if (!!!id) {
            return; //没有id属性 continue
        }
        var type = $this.attr('type'); switch (type) {
            case "checkbox": postdata[id] = $this.is(":checked"); break; case 'radio': postdata[id] = element.find('input[name="' + id + '"]:checked').val(); break; default: var value = ($this.val() == null || $this.val() == "") ? "" : $this.val(); if (!$.request("keyValue")) { value = value.replace(/&nbsp;/g, ''); }
                postdata[id] = value; break;
        }
    }); if ($('[name=__RequestVerificationToken]').length > 0) { postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val(); }
    return postdata;
};
$.fn.formReset = function () {
    $(this).removeAttr('novalidate');
    $(this).find(".error").parents('.has-error').find('i.error').remove();
    $(this).find(".error").parents('.has-error').removeClass('has-error');
    $(this).find(".error").removeClass('error');
}

$.fn.bindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        url: "",
        param: [],
        change: null,
        pleaseselect: false,
        pleaseselectText: null,
        handlenoresult: true,
        dropdownAutoWidth: !1,
        selectedValue: null,    //选中项
        data: null,
        datasource: null,
        minimumResultsForSearch: Infinity   //不显示search
    };
    var options = $.extend(defaults, options);
    var $element = $(this);

    options.init = function (data) {
        if ($element.find('option:first').length == 1
            && (!!$element.find('option:first').attr('default') || $element.find('option:first').attr('value') === undefined || $element.find('option:first').attr('value') === '')
        ) {
            options.pleaseselect = true;
            options.pleaseselectText = $element.find('option:first').html();
        }
        else {
            options.pleaseselect = false;
        }
        $element.html('');  //先清空
        if (options.pleaseselect === true) {
            $element.append($("<option value=''>" + options.pleaseselectText + "</option>"));
        }
        if (data && data.length > 0) {
            $.each(data, function (i) {
                $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
            });
            if ($element && options.selectedValue) {
                //$element.val(options.selectedValue);
                $element.find("option[value='" + options.selectedValue + "']").attr('selected', 'selected');
            }
        }
        else {
            if (options.pleaseselect !== true && options.handlenoresult === true) {
                //处理noresult，又un pleaseselect
                $element.append($("<option value=''>No results found</option>"));
            }
        }
        $element.select2({
            minimumResultsForSearch: options.minimumResultsForSearch,
            dropdownAutoWidth: options.dropdownAutoWidth
        });
        $element.on("change", function (e) {
            if (options.change != null) {
                //modified by sunny  此处因为下拉框选项添加了‘请选择’,索引位置不正确
                //options.change(data[$(this).find("option:selected").index()]);
                options.change(data);
            }
            $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
        });
    }

    if (options.url != "") {
        $.najax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                if (options.callBack) {
                    options.callBack(data);
                }
                options.init(data);
            }
        });
    }
    else if (options.data || options.datasource) {
        if (options.datasource) {
            options.data = options.datasource();
        }
        if (options.data) {
            options.init(options.data);
        }
    }
    else {
        $element.select2({
            minimumResultsForSearch: options.minimumResultsForSearch,
            dropdownAutoWidth: options.dropdownAutoWidth
        });
    }
}
$.fn.authorizeButton = function () {
    var moduleId = top.$(".Newtouch_iframe:visible").attr("id").substr(6); var dataJson = top.clients.authorizeButton[moduleId]; var $element = $(this); $element.find('a[authorize=yes]').attr('authorize', 'no'); if (dataJson != undefined) { $.each(dataJson, function (i) { $element.find("#" + dataJson[i].Code).attr('authorize', 'yes'); }); }
    $element.find("[authorize=no]").parents('li').prev('.split').remove(); $element.find("[authorize=no]").parents('li').remove(); $element.find('[authorize=no]').remove();
}
$.fn.dataGrid = function (options) {
    var defaults = { datatype: "json", autowidth: true, rownumbers: true, shrinkToFit: false, gridview: true }; var options = $.extend(defaults, options); if ((options.sortable === false || !!!options.pager) && options.colModel && options.colModel.length > 0) {
        for (i = 0; i < options.colModel.length; i++) { if (options.colModel[i].sortable === undefined) { options.colModel[i].sortable = false; } }
        if (!!!options.pager) { options.rowNum = -1; }
    }
    var $element = $(this); $element.css({ "cursor": "pointer" }); if (!options.onSelectRow) {
        options["onSelectRow"] = function (rowid, status) {
            var $operate = $(".operate"); if ($operate.length > 0) {
                var selRow = $(this).jqGrid("getGridParam", "selrow"); if (selRow) {
                    var length = selRow.length; if (length > 0) { $operate.animate({ "left": 0 }, 200); } else { $operate.animate({ "left": '-100.1%' }, 200); }
                    $operate.find('.close').click(function () { $operate.animate({ "left": '-100.1%' }, 200); });
                }
                else { $operate.animate({ "left": '-100.1%' }, 200); }
            }
            if (window.btn_loaddata && !options.multiselect) { btn_loaddata(); }
            if (options.onSelectRow_page) { options.onSelectRow_page(rowid, status); }
        };
    }
    if (!options.ondblClickRow) {
        options["ondblClickRow"] = function (rowid) {
            if (options.ondblClickRow_page) { options.ondblClickRow_page(rowid); }
            else if (window.btn_edit) { btn_edit(rowid); }
        }
    }
    options.beforeProcessing = function (data, st, xhr) {
        if (!$.xhrSuccessDataExCheckHandle(data)) { return false; }
        return true;
    }; $element.jqGrid(options);
}; $.fn.nfnAddReadonly = function () { $(this).addClass('newtouch_Readonly').attr('disabled', 'disabled'); }
$.fn.nfnRemoveReadonly = function () { $(this).removeClass('newtouch_Readonly').removeAttr('disabled'); }
$.fn.keydownEnterEvent = function (func) {
    $(this).keydown(function (event, arg1, arg2) {
        event = event || window.event;
        if (event.keyCode == 13) {
            if (func) {
                var returnwhat = func(event, arg1, arg2);
                if (returnwhat === true || returnwhat === false) {
                    return returnwhat;
                }
            }
        }
    });
}
$.fn.keyupEnterEvent = function (func) { $(this).keyup(function (event, arg1, arg2) { event = event || window.event; if (event.keyCode == 13) { if (func) { func(event, arg1, arg2); } } }); }
$.zeroPrefixInteger = function (num, length) { return (Array(length).join('0') + num).slice(-length); }
$.addNum = function (num1, num2) {
    var sq1, sq2, m; try { sq1 = num1.toString().split(".")[1].length; }
    catch (e) { sq1 = 0; }
    try { sq2 = num2.toString().split(".")[1].length; }
    catch (e) { sq2 = 0; }
    m = Math.pow(10, Math.max(sq1, sq2)); return (num1 * m + num2 * m) / m;
}
/**
 ** 减法函数，用来得到精确的减法结果
 ** 说明：javascript的减法结果会有误差，在两个浮点数相减的时候会比较明显。这个函数返回较为精确的减法结果。
 ** 调用：accSub(arg1,arg2)
 ** 返回值：arg1减去arg2的精确结果
 **/
$.accSub = function (arg1, arg2) {
    if (!!!arg2) {
        return arg1;
    }
    var r1, r2, m, n;
    try {
        r1 = arg1.toString().split(".")[1].length;
    }
    catch (e) {
        r1 = 0;
    }
    try {
        r2 = arg2.toString().split(".")[1].length;
    }
    catch (e) {
        r2 = 0;
    }
    m = Math.pow(10, Math.max(r1, r2)); //last modify by deeka //动态控制精度长度
    n = (r1 >= r2) ? r1 : r2;
    return ((parseFloat(arg1) * m - parseFloat(arg2) * m) / m).toFixed(n);
}
/* 获取性别 */
$.getGender = function (value) {
    if (value === undefined || value === null) {
        return '';
    }
    var valStr = $.trim(value.toString());
    if (valStr === "男" || valStr === "女" || valStr.length != 1) {
        return valStr;
    }
    return valStr == '1' ? '男' : (valStr == '2' ? '女' : (valStr == '3' ? '不详' : ''));
}
/* 获取性别 */
$.getGenderCode = function (value) {
    if (value === undefined || value === null) {
        return '3'; //3不详
    }
    var valStr = $.trim(value.toString());
    return valStr == "男" ? "1" : (valStr == "女" ? "2" : "3"); //1男2女3不详
}
$.undefinedwith0 = function (value) {
    if (value === undefined || value === null || value === '') { return 0; }
    return value;
}
$.undefinedwith1 = function (value) {
    if (value === undefined || value === null || value === '') { return 1; }
    return value;
}
$.undefinedwithEmptyString = function (value) {
    if (value === undefined || value === null || value === '') {
        return '';
    }
    return value;
}
$.cloneObject = function (obj) {
    var o; if (typeof obj == "object") {
        if (obj === null) { o = null; }
        else {
            if (obj instanceof Array) { o = []; for (var i = 0, len = obj.length; i < len; i++) { o.push(clone(obj[i])); } }
            else { o = {}; for (var j in obj) { o[j] = clone(obj[j]); } }
        }
    }
    else { o = obj; }
    return o;
}
$.enum = {
    getItems: function (type) {
        var enumsTemp;
        try {
            enumsTemp = top.top.$.clients.get($.getCurrentAppId(), "enums");
        }
        catch (eeeeeeeee) {
            //为了兼容以前的版本
            enumsTemp = top.top.clients.enums;
        }
        if (enumsTemp) {
            var matched = $.jsonWhere(enumsTemp, function (v) {
                return v.Type == type;
            }); if (matched && matched.length == 1) { return matched[0].Items; }
        }
        return [];
    }, getValueByDesc: function (type, desc) {
        var arr = this.getItems(type); if (arr) { var matched = $.jsonWhere(arr, function (v) { return v.Desc == desc; }); if (matched && matched.length == 1) { return matched[0].Value; } }
        return '';
    }, getNameByDesc: function (type, desc) {
        var arr = this.getItems(type); if (arr) { var matched = $.jsonWhere(arr, function (v) { return v.Desc == desc; }); if (matched && matched.length == 1) { return matched[0].Name; } }
        return '';
    }, getNameByValue: function (type, value) {
        var arr = this.getItems(type); if (arr) { var matched = $.jsonWhere(arr, function (v) { return v.Value == value; }); if (matched && matched.length == 1) { return matched[0].Name; } }
        return '';
    }, getDescByValue: function (type, value) {
        var arr = this.getItems(type); if (arr) { var matched = $.jsonWhere(arr, function (v) { return v.Value == value; }); if (matched && matched.length == 1) { return matched[0].Desc; } }
        return '';
    }, getValueByName: function (type, name) {
        var arr = this.getItems(type); if (arr) { var matched = $.jsonWhere(arr, function (v) { return v.Name == name; }); if (matched && matched.length == 1) { return matched[0].Value; } }
        return '';
    }, getDescByName: function (type, name) {
        var arr = this.getItems(type); if (arr) { var matched = $.jsonWhere(arr, function (v) { return v.Name == name; }); if (matched && matched.length == 1) { return matched[0].Desc; } }
        return '';
    },
}; $.fn.enumBindSelect = function (options) {
    var defaults = { enumtype: null, id: "Value", text: "Desc", someValues: null, someNames: null, }; var options = $.extend(defaults, options); if (!!!options.enumtype) { return; }
    var data = $.enum.getItems(options.enumtype); if (!!!data) { return; }
    if (options.someValues && options.someValues.length && options.someValues.length > 0) { data = $.jsonWhere(data, function (v) { return (',' + options.someValues + ',').indexOf(',' + v.Value.toString() + ',') >= 0; }); }
    else if (options.someNames && options.someNames.length && options.someNames.length > 0) { data = $.jsonWhere(data, function (v) { return (',' + options.someNames + ',').indexOf(',' + v.Name.toString() + ',') >= 0; }); }
    options.data = data; var $element = $(this); $element.bindSelect(options);
}
$(function () { $('select[data-EnumType]').each(function () { $(this).enumBindSelect({ enumtype: $(this).attr('data-EnumType'), someValues: $(this).attr('data-SomeValues'), someNames: $(this).attr('data-SomeNames'), selectedValue: $(this).attr('data-SelectedValue') }); }); });
$.itemDetails = {
    getItems: function (type, isContantsInvalid) {
        var itemDetailsTemp;
        try {
            itemDetailsTemp = top.top.$.clients.get($.getCurrentAppId(), "itemDetails");
        }
        catch (eeeeeeeee) {
            //为了兼容以前的版本
            itemDetailsTemp = top.top.clients.itemDetails;
        }
        if (itemDetailsTemp) {
            var matched = $.jsonWhere(itemDetailsTemp, function (v) {
                return v.Type == type;
            }); if (matched && matched.length == 1) {
                var details = matched[0].Items; if (details && details.length) { if (!!!isContantsInvalid) { details = $.jsonWhere(details, function (v) { return v.zt === "1" }); } }
                return details;
            }
        }
        return [];
    }, getCodeByName: function (type, name) {
        var arr = this.getItems(type, true); if (arr) { var matched = $.jsonWhere(arr, function (v) { return v.Name == name; }); if (matched && matched.length >= 1) { return matched[0].Code; } }
        return '';
    }, getNameByCode: function (type, code) {
        var arr = this.getItems(type, true); if (arr) { var matched = $.jsonWhere(arr, function (v) { return v.Code == code; }); if (matched && matched.length >= 1) { return matched[0].Name; } }
        return '';
    },
}
$.fn.itemDetailsBindSelect = function (options) {
    var defaults = { itemtype: null, id: "Code", text: "Name", }; var options = $.extend(defaults, options); if (!!!options.itemtype) { return; }
    var data = $.itemDetails.getItems(options.itemtype); if (!!!data) { return; }
    options.data = data; var $element = $(this); $element.bindSelect(options);
}
$(function () { $('select[data-ItemType]').each(function () { $(this).itemDetailsBindSelect({ itemtype: $(this).attr('data-ItemType'), selectedValue: $(this).attr('data-SelectedValue') }); }); }); $.deepClone = {
    getType: function (obj) {
        var toString = Object.prototype.toString; var map = { '[object Boolean]': 'boolean', '[object Number]': 'number', '[object String]': 'string', '[object Function]': 'function', '[object Array]': 'array', '[object Date]': 'date', '[object RegExp]': 'regExp', '[object Undefined]': 'undefined', '[object Null]': 'null', '[object Object]': 'object' }; if (obj instanceof Element) { return 'element'; }
        return map[toString.call(obj)];
    }, clone: function (data) {
        var type = this.getType(data); var obj; if (type === 'array') { obj = []; } else if (type === 'object') { obj = {}; } else { return data; }
        if (type === 'array') { for (var i = 0, len = data.length; i < len; i++) { obj.push(this.clone(data[i])); } } else if (type === 'object') { for (var key in data) { obj[key] = this.clone(data[key]); } }
        return obj;
    }
};
//$('any').val() 返回undefined ，nval返回空串
$.fn.nval = function () {
    var val = $(this).val();
    if (!!val || !isNaN(val)) {
        return val;
    }
    else {
        return '';
    }
}

//三方目录对照扩展
$.CataComparison = {
    getItems: function (ttMark, code) {
        var cataComparisonList = top.top.$.clients.get($.getCurrentAppId(), "CataloguesComparisonList");
        if (cataComparisonList) {
            var matched = $.jsonWhere(cataComparisonList, function (v) {
                return v.Code == code && v.TTMark == ttMark;
            });
            if (matched && matched.length == 1) {
                var details = matched[0].Items;
                return details;
            }
        }
        return [];
    },
    getItemsByTTCode: function (ttMark, ttCode) {
        var cataComparisonList = top.top.$.clients.get($.getCurrentAppId(), "CataloguesComparisonList");
        if (cataComparisonList) {
            var matched = $.jsonWhere(cataComparisonList, function (v) {
                return v.TTCode == ttCode && v.TTMark == ttMark;
            });
            if (matched && matched.length == 1) {
                var details = matched[0].Items;
                return details;
            }
        }
        return [];
    },
    getItem: function (ttMark, code, itemCode) {
        var arr = this.getItems(ttMark, code);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Code == itemCode;
            });
            if (matched && matched.length >= 1) {
                return matched[0];
            }
        }
        return {};
    },
    getItemByTT: function (ttMark, ttCode, ttItemCode) {
        var arr = this.getItemsByTTCode(ttMark, ttCode);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.TTCode == ttItemCode;
            });
            if (matched && matched.length >= 1) {
                return matched[0];
            }
        }
        return {};
    },
}