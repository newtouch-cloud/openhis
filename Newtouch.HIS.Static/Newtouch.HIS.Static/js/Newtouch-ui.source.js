
//阻止默认行为
function stopDefault(e) {
    e = e || window.event;
    if (e && e.preventDefault) {
        e.preventDefault();
    }
    else if (window.event) {
        window.event.returnValue = false;
    }
    return false;
}

//阻止冒泡
function stopPropagation(e) {
    e = e || window.event;
    if (e && e.stopPropagation) {   //如果提供了事件对象，则这是一个非IE浏览器
        e.stopPropagation();    //因此它支持W3C的stopPropagation()方法
    }
    else if (window.event) {
        window.event.cancelBubble = true;  //否则，我们需要使用IE的方式来取消事件冒泡
    }
    return false;
}

/* 获取DOM元素坐标 */
var getDOMPositionPoint = function (obj) { //获取元素举例body边框的位置
    var t = obj.offsetTop; //获取该元素对应父容器的上边距
    var l = obj.offsetLeft; //对应父容器的上边距
    //判断是否有父容器，如果存在则累加其边距
    while (obj = obj.offsetParent) {//等效 obj = obj.offsetParent;while (obj != undefined)
        t += obj.offsetTop; //叠加父容器的上边距
        l += obj.offsetLeft; //叠加父容器的左边距
    }
    return { t: t, l: l };
}

//newtouch global
window.newtouch_globalconfig = {
    $currentActiveWindow: null,  //当前激活的Window对象
    f4opions: null,

};

//
var newtouch_setCurrentWindow = function (win) {
    if (!win) {
        win = window;
    }
    try {
        window.top.top.newtouch_globalconfig.$currentActiveWindow = window.top.newtouch_globalconfig.$currentActiveWindow =
            window.newtouch_globalconfig.$currentActiveWindow = $(win);

        if (win.newtouch_globalconfig) {
            window.top.top.newtouch_globalconfig.f4opions = window.top.newtouch_globalconfig.f4opions =
                window.newtouch_globalconfig.f4opions = win.newtouch_globalconfig.f4opions;
        }
    }
    catch (errr) { }
};

var newtouch_getCurrentActiveWindow = function () {
    if (window.newtouch_globalconfig.$currentActiveWindow == null) {
        window.newtouch_globalconfig.$currentActiveWindow = $(window);
    }
    if (window.newtouch_globalconfig.$currentActiveWindow[0].top
        == window.newtouch_globalconfig.$currentActiveWindow[0]) {
        //如果是Home/Index 取当前激活的那个iframe
        var curFrameDataid = $(window.document).find('.page-tabs-content a.active').attr('data-id');
        var $activeIframe = $(window.document).find('.content-iframe iframe[data-id="' + curFrameDataid + '"]');
        newtouch_setCurrentWindow($activeIframe[0].contentWindow)
    }
    return window.newtouch_globalconfig.$currentActiveWindow;
}

//F1
var newtouch_globalevent_f1 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f1) {
        $currentActiveWindow[0].newtouch_event_f1(event);
        return true;
    }
    return false;
};

//F2
var newtouch_globalevent_f2 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f2) {
        $currentActiveWindow[0].newtouch_event_f2(event);
        return true;
    }
    return false;
};

//F3
var newtouch_globalevent_f3 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f3) {
        $currentActiveWindow[0].newtouch_event_f3(event);
        return true;
    }
    return false;
};

//F4清除页面表单内容
var newtouch_globalevent_f4 = function (event, options) {

    var $currentActiveWindow = newtouch_getCurrentActiveWindow();   //要在window.newtouch_globalconfig.f4opions之后

    var defaults = {
        container: "body",
        inner: true,   //是否执行页面内部自定义的清除方法
        justinner: false,   //是否‘仅’执行页面内部自定义的清除方法
    };
    if (options) {
        var options = $.extend(defaults, options);
    }
    else if (window.newtouch_globalconfig.f4opions) {
        var options = $.extend(defaults, window.newtouch_globalconfig.f4opions);
    }
    else {
        options = defaults;
    }

    if (!(options.justinner === true)) {
        var $thisContainer = $($currentActiveWindow[0].document).find(options.container);
        //清除复选框的选中状态
        $thisContainer.find(':checked').not('.formClearIgnore').trigger("click");
        //清除select的选中状态
        $thisContainer.find('select').not('.formClearIgnore')
            .not('.ui-pg-selbox')   //排除分页的页码select
            .each(function (idx, val) {
                $(val)[0].selectedIndex = 0;
            });
        $thisContainer.find('select').not('.formClearIgnore')
            .not('.ui-pg-selbox')   //排除分页的页码select
            .trigger("change");
        $thisContainer.find('textarea').not('.formClearIgnore').val(''); //清除文本域内容
        $thisContainer.find('[type=text]').not('.formClearIgnore').val(''); //清除文本框内容
        $thisContainer.find('.formValue>label[id]').not('.formClearIgnore').html(''); //清除formValue下label的内容
    }

    if (options.inner === true && $currentActiveWindow[0].newtouch_event_f4) {
        $currentActiveWindow[0].newtouch_event_f4(event);
    }
    if (event) {
        stopDefault(event);
    }
};

//F5刷新页面（当前页）
var newtouch_globalevent_f5 = function (event) {
    if (top.top.$('.dropdown-menu .tabReload').length > 0) {
        top.top.$('.dropdown-menu .tabReload').trigger('click');
        if (event) {
            stopDefault(event);
        }
    }
    else {
        window.location.href = window.location.href;
    }
};

//F6
var newtouch_globalevent_f6 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f6) {
        $currentActiveWindow[0].newtouch_event_f6(event);
        return true;
    }
    return false;
};

//F7
var newtouch_globalevent_f7 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f7) {
        $currentActiveWindow[0].newtouch_event_f7(event);
        return true;
    }
    return false;
};

//F8
var newtouch_globalevent_f8 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f8) {
        $currentActiveWindow[0].newtouch_event_f8(event);
        return true;
    }
    return false;
};

//F9
var newtouch_globalevent_f9 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f9) {
        $currentActiveWindow[0].newtouch_event_f9(event);
        return true;
    }
    return false;
};

//F10
var newtouch_globalevent_f10 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f10) {
        $currentActiveWindow[0].newtouch_event_f10(event);
        return true;
    }
    return false;
};

//documen的keydown事件
$(document).keydown(function (event) {
    //global start
    $('.form-an-cur').removeClass('form-an-cur');  //AutoNext V2
    //globle end
    event = event || window.event;
    if ((event.keyCode >= 112 && event.keyCode <= 123)) {
        newtouch_setCurrentWindow();    //重新明确当前windown     //弹层之后，关闭弹层，得重新定位到主页面
    }

    if (1 == 2) {

    }
    else if (((event.keyCode >= 112 && event.keyCode <= 114) || (event.keyCode >= 117 && event.keyCode <= 121)) && top.top.$('#loadingPage:visible').length >= 1) {
        return false;
    }
    else if (event.keyCode == 8) {  //Backspace
        if (!(event.target.nodeName == 'INPUT' || event.target.nodeName == 'SELECT' || event.target.nodeName == 'TEXTAREA')) {
            stopDefault(event);
            stopPropagation(event);
            return false;
        }
    }
    else if (event.keyCode == 112) { //F1
        if (newtouch_globalevent_f1(event)) {
            stopDefault(event);
            stopPropagation(event); //阻止向外冒泡
        }
    }
    else if (event.keyCode == 113) { //F2
        if (newtouch_globalevent_f2(event)) {
            stopDefault(event);
            stopPropagation(event); //阻止向外冒泡
        }
    }
    else if (event.keyCode == 114) { //F3
        if (newtouch_globalevent_f3(event)) {
            stopDefault(event);
            stopPropagation(event); //阻止向外冒泡
        }
    }
    else if (event.keyCode == 115) { //F4清除
        newtouch_globalevent_f4(event);
        stopPropagation(event); //阻止向外冒泡
    }
    else if (event.keyCode == 116) { //F5刷新
        newtouch_globalevent_f5(event);
        stopPropagation(event); //阻止向外冒泡
    }
    else if (event.keyCode == 117) { //F6
        if (newtouch_globalevent_f6(event)) {
            stopDefault(event);
            stopPropagation(event); //阻止向外冒泡
        }
    }
    else if (event.keyCode == 118) { //F7
        if (newtouch_globalevent_f7(event)) {
            stopDefault(event);
            stopPropagation(event); //阻止向外冒泡
        }
    }
    else if (event.keyCode == 119) { //F8
        if (newtouch_globalevent_f8(event)) {
            stopDefault(event);
            stopPropagation(event); //阻止向外冒泡
        }
    }
    else if (event.keyCode == 120) { //F9
        if (newtouch_globalevent_f9(event)) {
            stopDefault(event);
            stopPropagation(event); //阻止向外冒泡
        }
    }
    else if (event.keyCode == 121) { //F10
        if (newtouch_globalevent_f10(event)) {
            stopDefault(event);
            stopPropagation(event); //阻止向外冒泡
        }
    }
    else if (false) {

    }
    //stopPropagation(event);
});

/* newtouch默认下拉 绑定local数据，非ajax请求 */
$.fn.newtouchBindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        change: null,
        data: null,
        datasource: null,
        attdata: null //添加自定义属性
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    if (options.datasource) {   //如果有datasource
        options.data = options.datasource();
    }
    $.each(options.data, function (i) {
        if (options.attdata) {
            $element.append($('<option data-' + options.attdata + '="' + options.data[i][options.attdata] + '"></option>').val(options.data[i][options.id]).html(options.data[i][options.text]));
        }
        else {
            $element.append($("<option></option>").val(options.data[i][options.id]).html(options.data[i][options.text]));
        }
    });
    if (options.selectedValue) {
        $element.val(options.selectedValue);
    }
    $element.select2({
        minimumResultsForSearch: options.search == true ? 0 : -1
    });
    $element.on("change", function (e) {
        if (options.change != null) {
            //modified by sunny  此处因为下拉框选项添加了‘请选择’,索引位置不正确
            //options.change(data[$(this).find("option:selected").index()]);
            options.change(options.data);
        }
        $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
    });
}


//创建Grid表格
$.fn.dataNewGrid = function (options, data) {
    var defaults = {
        datatype: "local",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        multiselect: false,
        caption: ""
    };
    var options = $.extend(defaults, options);
    //非分页的grid，统一禁用掉排序 start
    if ((options.sortable === false || !!!options.pager)
        && options.colModel && options.colModel.length > 0) {
        //
        for (i = 0; i < options.colModel.length; i++) {
            if (options.colModel[i].sortable === undefined) {
                options.colModel[i].sortable = false;
            }
        }
        //
        if (!!!options.pager) {
            options.rowNum = -1;    //非分页 显示全部
        }
    }
    //非分页的grid，统一禁用掉排序 end
    var $element = $(this);
    $element.css({ "cursor": "pointer" });
    if (!options.onSelectRow) {
        options["onSelectRow"] = function (rowid, status) {
            var $operate = $(".operate");
            if ($operate.length > 0) {
                var length = $(this).jqGrid("getGridParam", "selrow").length;
                if (length > 0) {
                    $operate.animate({ "left": 0 }, 200);
                } else {
                    $operate.animate({ "left": '-100.1%' }, 200);
                }
                $operate.find('.close').click(function () {
                    $operate.animate({ "left": '-100.1%' }, 200);
                });
            }
            if (!options.multiselect && window.btn_selectrow) {
                btn_selectrow();
            }
            //多选
            if (options.multiselect && window.grid_checkedit) {
                grid_checkedit();
            }
            if (options.onSelectRow_page) {
                options.onSelectRow_page(rowid, status);
            }
        };
    }
    if (!options.ondblClickRow) {
        options["ondblClickRow"] = function (rowid) {
            if (options.ondblClickRow_page) {
                options.ondblClickRow_page(rowid);
            }
            else if (window.btn_edit) {
                btn_edit(rowid);
            }
        }
    }
    $element.jqGrid(options);
    for (var i = 0; i <= data.length; i++) {
        $element.jqGrid('addRowData', i + 1, data[i]);
    }
    if (data.length > 0) {
        $('.ui-jqgrid-bdiv').find(".unwritten").remove();
    }
};


//浏览器窗口调整大小时重新加载jqGrid的宽
function initLayout(formId) {
    reSetGridWidth();
    return;
    $("table[rel=" + formId + "]").each(function () {
        var rel = $(this).attr("rel");
        if (rel) {
            var $form = $("#" + rel);
            var gridWidth = $form.width() - 3;
            $(this).setGridWidth(gridWidth, true);
        }
    });
}

/* newtouch local 默认绑定jGrid */
$.fn.newtouchLocalDataGrid = function (options, data) {
    var defaults = {
        datatype: "local",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        posttofirst: true,
        showloadingLayer: false
    };
    var options = $.extend(defaults, options);
    if (!!!options.pager) {
        options.rowNum = -1;    //非分页 显示全部
    }
    var $element = $(this);
    if (!options.actions || options.actions.indexOf("declare") >= 0) {
        //非分页的grid，统一禁用掉排序 start
        if (options.colModel && options.colModel.length > 0) {
            //
            for (i = 0; i < options.colModel.length; i++) {
                if (options.colModel[i].sortable === undefined) {
                    options.colModel[i].sortable = false;
                }
            }
            //
        }
        //非分页的grid，统一禁用掉排序 end
        //声明
        $element.jqGrid(options);
    }
    if ((!options.actions || options.actions.indexOf("addRowData") >= 0) && data) {
        //bind数据
        //获取当前的最大行id
        var baseId = 0;
        var curRowIds = $element.jqGrid('getDataIDs');
        if (curRowIds && curRowIds.length && curRowIds.length > 0) {
            for (var j = 0; j < curRowIds.length; j++) {
                if (curRowIds[j] && parseInt(curRowIds[j]) && parseInt(curRowIds[j]) != NaN && parseInt(curRowIds[j]) > baseId) {
                    baseId = parseInt(curRowIds[j]);
                }
            }
        }
        if (data instanceof Array) {
            //传进来了一个数组
            if (data.length == 1) {
                if (options.posttofirst) {
                    $element.addRowData(baseId + 1, data[0], "first");
                }
                else {
                    $element.addRowData(baseId + 1, data[0]);
                }
            }
            else {
                //传进来了一批数据
                if (options.posttofirst) {
                    for (var i = 0; i <= data.length; i++) {
                        $element.addRowData(baseId + i + 1, data[i], "first");
                    }
                }
                else {
                    for (var i = 0; i <= data.length; i++) {
                        $element.jqGrid('addRowData', baseId + i + 1, data[i]);
                    }
                }
            }
        }
        else {
            //不是数组，直接传进来了一个object
            if (options.posttofirst) {
                $element.addRowData(baseId + 1, data, "first");
            }
            else {
                $element.addRowData(baseId + 1, data);
            }
        }
    }
};

/* newtouch 浮层选择器 */
$.fn.newtouchFloatingSelector = function (options) {
    var defaults = {
        height: 400,
        width: 100,
        X: 0,
        Y: 0,
        leftshift: 0,   //左偏移   //仅与input对齐时产生效果
        id: null,
        fiter: null,
        url: null,
        ajaxmethod: 'GET',
        ajaxreqdata: null,  //function post 或者 jsonp 
        ajaxsync: false,
        ajaxparameters: null,    //function get
        isjsonp: false,  //是否使用jsonp
        isinputchangetriggered: true,    //是input change触发的（比如 单击按钮 浮层出来，就是传false）
        inputtextcheck: null,   //由input change触发，可先验证输入的文本 function
    };
    var options = $.extend(defaults, options);
    options.$this = $(this);
    if (options.isinputchangetriggered) {
        options.prveValue = options.$this.val();
    }
    if (!options.id) {
        options.id = options.$this.attr('id');
        if (options.id) {
            options.id = "divfloat_" + options.id;
        }
    }
    if (!options.id) {
        //随机生成id
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required");
        return;
    }
    if (!options.colModel || options.colModel.length <= 0) {
        throw new Error("argument colModel is required");
        return;
    }

    options.$this.attr('data-releatednfs', 'true'); //关联浮层

    //创建浮层
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id);
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            var thisContainerHeight = options.height + 2;
            //因为有横向滚动条 横向滚动条的高度
            if (options.tableWidth &&
                options.width <= options.tableWidth + 15) {
                thisContainerHeight += 15;
            }
            $divContainer.css({
                height: thisContainerHeight,
                width: options.width
            });
            //标题
            //170829隐藏caption
            //if (options.caption) {
            //    $divContainer.append('<div class="ui-nfs-caption">' + options.caption + '<div style="float:right;margin-right: 10px;cursor:pointer;" ><i class="fa fa-times" aria-hidden="true"></i></div></div>');
            //    $('div#' + options.id + ' div.ui-nfs-caption i.fa-times').click(function () {
            //        $('body').find('div#' + options.id).hide();
            //    });
            //}
            $divContainer.append('<div class="ui-nfs-container"><table></table></div>')
            if (options.tableWidth) {
                $divContainer.find('table').css('width', options.tableWidth);
            }
            //thead
            $divContainer.find('table').append('<thead><tr></tr></thead>');
            if (options.colModel && options.colModel.length > 0) {
                for (i = 0; i < options.colModel.length; i++) {
                    var thHtml = '<th style="';
                    if (options.colModel[i].widthratio) {
                        thHtml += 'width:' + options.colModel[i].widthratio + '%;';
                    }
                    else if (options.colModel[i].width) {
                        thHtml += 'width:' + options.colModel[i].width + 'px;';
                    }

                    if (options.colModel[i].hidden && options.colModel[i].hidden == true) {
                        thHtml += 'display:none;';
                    }
                    thHtml += '"';
                    if (i == options.colModel.length - 1) {
                        //样式例外
                        thHtml += ' class="isthelasttd"';
                    }
                    thHtml += '>' + options.colModel[i].label + '</th>';
                    $divContainer.find('table thead tr').append(thHtml);
                }
            }
        }
        options.container = $divContainer;
    };
    //给浮层定位（根据element的位置）
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        }
        else {
            //根据options.$this的坐标 来定位
            var thisDomPoint = getDOMPositionPoint(options.$this[0]);

            if (options.width > ($(document).width() / 2 + $(document).width() / 4)) {
                options.container.css('left', "10px");
            }
            else {
                if (thisDomPoint.l < ($(document).width() / 2)) {
                    if (options.leftshift) {
                        options.container.css('left', (thisDomPoint.l - options.leftshift) + "px");
                    }
                    else {
                        options.container.css('left', (thisDomPoint.l) + "px");
                    }
                }
                else {
                    options.container.css('left', (thisDomPoint.l - options.width + options.$this.width() + 5) + "px");
                }
            }

            if (($(document).height() - thisDomPoint.t - options.$this.height() - 5) < options.height) {
                //下方不够用,置到上方
                options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");
            }
            else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");
            }
        }
    };
    //加载数据
    options.loaddata = function () {
        //options.container.html('loading data ...');
        if (!options.url) {
            if (options.filter) {
                var resultObjArr;
                if (options.isinputchangetriggered) {
                    resultObjArr = (options.filter)(options.$this.val());
                }
                else {
                    resultObjArr = options.filter();
                }
                if (resultObjArr) {
                    (options.funcBindRowData)(resultObjArr);
                }
            }
        }
        else {
            //通过接口获取数据
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this);  //谁触发的，返回给页面
                if (thisParameters) {
                    thisUrl += "?" + $.encodeRequestParameters(thisParameters);
                }
            }
            if (options.ajaxreqdata) {
                var thisReqData = options.ajaxreqdata();
            }

            options.lastRequestMark = thisUrl + (!!thisReqData ? thisReqData.toString() : '');

            if (!options.isjsonp) {
                $.ajax({
                    type: options.ajaxmethod,
                    url: thisUrl,
                    data: thisReqData,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        if (data && data.length && data.length > 0) {

                            //防止错乱呈现
                            if (!!options.lastRequestMark
                                && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                //console.log("skip bind data");
                                return;
                            }

                            if (options.filter) {
                                data = (options.filter)(data);
                            }
                            (options.funcBindRowData)(data);
                        }
                        else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            }
            else {
                //jsonp加载数据
                $.ajax({
                    url: thisUrl,
                    dataType: 'JSONP',
                    jsonp: 'jsonpCallback',
                    jsonpCallback: "just_callback_in_success",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "10000" && data.data) {

                            //防止错乱呈现
                            if (!!options.lastRequestMark
                                && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                return;
                            }

                            if (!(data.data instanceof Object)) {
                                data.data = JSON.parse(data.data);
                            }
                            if (options.filter) {
                                (options.funcBindRowData)((options.filter)(data.data));
                            }
                            else {
                                (options.funcBindRowData)(data.data);
                            }
                        }
                        else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            }
        }
    };
    //得到返回结果，遍历呈现之
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr && options.colModel && options.colModel.length > 0) {
            if (options.container) {
                options.container.find('table tbody').html(''); //清除当前tbody内容
            }
            if (options.container.find('table tbody').length <= 0) {
                options.container.find('table').append('<tbody></tbody>');
            }
            if (options.height) {
                options.container.find('table tbody').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var trHtml = '<tr';
                //tr样式
                if (i == 0) {
                    trHtml += ' class="active"';    //第一行
                }
                for (j = 0; j < options.colModel.length; j++) {
                    var thistdVal = resultObjArr[i][options.colModel[j].name];
                    if (options.colModel[j].formatter) {
                        if ($.isFunction(options.colModel[j].formatter)) {
                            thistdVal = options.colModel[j].formatter(thistdVal, null, resultObjArr[i]);
                        }
                        else {
                            if (options.colModel[j].formatter === "date") {
                                if (thistdVal && $.strToDate(thistdVal)) {
                                    thistdVal = $.getDate({ date: $.strToDate(thistdVal) });
                                }
                            }
                            else if (options.colModel[j].formatter === "datetime") {
                                if (thistdVal && $.strToDate(thistdVal)) {
                                    thistdVal = $.getTime({ date: $.strToDate(thistdVal), second: false });
                                }
                            }
                        }
                    }
                    trHtml += ' data-' + options.colModel[j].name + '="' + (thistdVal === null ? "" : thistdVal) + '"';
                }
                trHtml += ">";
                for (j = 0; j < options.colModel.length; j++) {
                    trHtml += '<td';
                    trHtml += ' style="';
                    if (options.colModel[j].widthratio) {
                        trHtml += ' width:' + options.colModel[j].widthratio + '%;';
                    }
                    else if (options.colModel[j].width) {
                        trHtml += 'width:' + options.colModel[j].width + 'px;';
                    }
                    if (options.colModel[j].align) {
                        trHtml += 'text-align:' + options.colModel[j].align + ';';
                    }
                    if (options.colModel[j].hidden && options.colModel[j].hidden == true) {
                        trHtml += 'display:none;';
                    }
                    trHtml += '"';
                    if (j == options.colModel.length - 1) {
                        //样式例外
                        trHtml += ' class="isthelasttd"';
                    }
                    var thistdVal = resultObjArr[i][options.colModel[j].name];
                    if (options.colModel[j].formatter) {
                        if ($.isFunction(options.colModel[j].formatter)) {
                            thistdVal = options.colModel[j].formatter(thistdVal, null, resultObjArr[i]);
                        }
                        else {
                            if (options.colModel[j].formatter === "date") {
                                if (thistdVal && $.strToDate(thistdVal)) {
                                    thistdVal = $.getDate({ date: $.strToDate(thistdVal) });
                                }
                            }
                            else if (options.colModel[j].formatter === "datetime") {
                                if (thistdVal && $.strToDate(thistdVal)) {
                                    thistdVal = $.getTime({ date: $.strToDate(thistdVal), second: false });
                                }
                            }
                        }
                    }
                    trHtml += '>' + (thistdVal === null ? "" : thistdVal) + '</td>';
                }
                trHtml += "</tr>";
                options.container.find('table tbody').append(trHtml);
            }
            //改变竖向滚动条   //牺牲了thead浮顶
            if (options.tableWidth &&
                options.width <= options.tableWidth + 15) {
                var tblHeight = resultObjArr.length * 40;
                if (tblHeight > options.height) {
                    $('body').find('div#' + options.id).find('table tbody').css('height', '');
                    $('body').find('div#' + options.id).find('table').css('height', resultObjArr.length * 26 + 10 + 33);
                }
            }
        }
    }
    //bind单击选中事件
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'tbody tr', function () {
                var godbclick = true;
                if (options.itemdbclickhandleBefore) {
                    godbclick = options.itemdbclickhandleBefore($(this));    //双击一行 处理之前
                }
                if (!(godbclick === false)) {
                    options.itemdbclickhandler($(this), options.$this); //谁触发的，返回给页面
                }
                if (options.isinputchangetriggered) {
                    //如果是input本身的change触发 给文本框焦点
                    options.focusfromdbclickitem = true;
                    options.$this.trigger('focus');
                }
                if (options.itemdbclickhandleComplete) {
                    options.itemdbclickhandleComplete($(this));    //双击一行 处理完之后
                }
                options.container.find('table tbody').html(''); //清除当前tbody内容
                options.container.hide();
            });
        }
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //创建浮层
        }
        options.location();   //定位
        if (options.container) {
            options.container.find('table tbody').html(''); //清除当前tbody内容
        }
        options.container.show();   //定位好 再显示
        options.loaddata();   //加载数据
        options.binddbclickchooseevent();   //双击选中事件  //应该是触发一次就可以了
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();
                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //没到最小长度，隐藏之
                }
                if (typeof options.inputtextcheck === 'function') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }

                options.okload();
            });
        }
        else if (options.focusautotrigger === true) {
            options.$this.focus(function (event) {
                if (options.focusfromdbclickitem === true) {
                    options.focusfromdbclickitem = false;   //不能循环触发focus
                    return;
                }

                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //没到最小长度，隐藏之
                }
                if (typeof options.inputtextcheck === 'function') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }

                options.okload();
            });
        }

        options.$this.keyup(function (event) {   //必须是keyup
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) {
                return;
            }

            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) {
                return; //值没有变  则不触发
            }

            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //没到最小长度，隐藏之
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }

            options.okload();
        });

        //bind键盘上下事件
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 && options.container) {
                var $trs = options.container.find('table tbody tr');    //当前浮层中的所有tr
                if ($trs.length == 0) {
                    return; //没有行
                }
                var curActiveIndex = -1;    //当前active的行
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('active')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0;  //目标index
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                }
                else {
                    if (event.keyCode == 38) {
                        //alert("按了↑键！");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 40) {
                        //alert("按了↓键！");
                        dstnIndex = curActiveIndex + 1;
                    }
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    }
                    else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //调整sroll的位置
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            }
            else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('table tbody tr.active');    //当前浮层中的所有tr
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');    //
                        //stopDefault(event);
                        //stopPropagation(event);
                    }
                }
            }
        });

        //单击其他 隐藏浮层
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('data-releatednfs') === 'true')) {
                return;
            }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) {
                $('body div.ui-nfs:visible').hide();
            }
        })
    }
    else {
        //由click事件来触发
        options.$this.click(function () {
            $('body div.ui-nfs:visible').hide();

            options.okload();
        });

        //单击其他 隐藏浮层
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('data-releatednfs') === 'true')) {
                return;
            }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) {
                $('body div.ui-nfs:visible').hide();
            }
        })
    }
};
/* 给多个元素绑定同一类浮层 */
$.fn.newtouchBatchFloatingSelector = function (options) {
    this.each(function () {
        var defaults = {
        };
        defaults = $.extend(defaults, options);
        $(this).newtouchFloatingSelector(defaults);
    });
};

//提示
$.newtouchAlert = function (data) {
    if (!$.xhrSuccessDataExCheckHandle(data)) {
        return false;
    }

    //success？
    var msg = data.message;
    if (!msg) {
        msg = data.msg;
    }
    if (!msg) {
        msg = data.message;
    }
    var type = 'success';

    $.modalAlert(msg, type);
};

(function ($) {
    //立即触发
    newtouch_setCurrentWindow();
    $(function () {
        //ready完之后触发
        $('.form-control-focus').focus();   //给焦点
        $('.newtouch_Readonly').attr("disabled", "disabled");
        CheckStorage();
        //icon切换

        $('.icontoggle').click(function () {
            var $dstnDiv = $(this).parent().prev().children('tbody.dispTbody');
            if ($(".icontoggle").hasClass("fa-angle-double-down")) {
                $dstnDiv.fadeToggle("slow");
                $(".icontoggle").removeClass("fa-angle-double-down");
                $(".icontoggle").addClass("fa-angle-double-up");
            } else {
                $dstnDiv.fadeToggle("hide");
                $(".icontoggle").removeClass("fa-angle-double-up");
                $(".icontoggle").addClass("fa-angle-double-down");
            }
        });
    });
    window.onload = function () {
        //在最后
        AutoNext_V2.init(); //keydown绑定了多个事件，按顺序执行，auto-next在最后
    };
})(jQuery);

/* AutoNext V2 */
var AutoNext_V2 = {
    init: function () {
        $('[id].form-an').keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 13) {
                var nodes = $('.form-an:visible');
                for (i = 0; i < nodes.length; i++) {
                    if ($(this)[0].id == nodes[i].id) {
                        if ($(this).is('.form-an-end')) {
                            return; //不再往下一个走，但可以由上一个过来
                        }
                        var ancheck = $(this).attr('form-an-ancheck');
                        var goonnext = true;
                        //ancheck是html中写的一个验证函数，返回bool。true继续往后跳，否则不自动跳下一个
                        if (ancheck) {
                            goonnext = eval(ancheck + "('" + $(this).val() + "')");
                        }
                        if (goonnext === true) {
                            var $nextEle = null;
                            if (i == nodes.length - 1) {
                                $nextEle = $(nodes[0]);
                            }
                            else {
                                $nextEle = $(nodes[i + 1]);
                            }
                            if ($nextEle[0].tagName == 'SELECT') {
                                if ($nextEle.next('.select2').length == 1) {
                                    $nextEle.addClass('form-an-cur');
                                    $nextEle.select2("open");
                                    //根本不会触发此上定义的keydown事件
                                }
                                else {

                                }
                            }
                            else {
                                $nextEle.addClass('form-an-cur').trigger('focus');
                            }
                        }
                        return;
                    }
                }
            }
        });
        $('select[id].form-an').on("select2:select", function (e) {
            var nodes = $('.form-an:visible');
            for (i = 0; i < nodes.length; i++) {
                if ($(this)[0].id == nodes[i].id) {
                    if ($(this).is('.form-an-end')) {
                        return; //不再往下一个走，但可以由上一个过来
                    }
                    var ancheck = $(this).attr('form-an-ancheck');
                    var goonnext = true;
                    //ancheck是html中写的一个验证函数，返回bool。true继续往后跳，否则不自动跳下一个
                    if (ancheck) {
                        goonnext = eval(ancheck + "('" + $(this).val() + "')");
                    }
                    if (goonnext === true) {
                        var $nextEle = null;
                        if (i == nodes.length - 1) {
                            $nextEle = $(nodes[0]);
                        }
                        else {
                            $nextEle = $(nodes[i + 1]);
                        }
                        if ($nextEle[0].tagName == 'SELECT') {
                            if ($nextEle.next('.select2').length == 1) {
                                $nextEle.addClass('form-an-cur');
                                $nextEle.select2("open");
                                //根本不会触发此上定义的keydown事件
                            }
                            else {

                            }
                        }
                        else {
                            $nextEle.addClass('form-an-cur').trigger('focus');
                        }
                    }
                    return;
                }
            }
        });
    }
}

function CheckStorage() {
    var basePath = '~/'; //基路径
    var cssPath = getlocalStorage("cssPath"); //获取默认皮肤路径
    if (cssPath != "") {
        $("#color-skin").attr("href", cssPath);
    }
}

/*存取storage*/
function setlocalStorage(c_name, value) {
    var strKey = c_name;
    var storage = window.localStorage;
    storage.setItem(strKey, value);
}

/*读取*/
function getlocalStorage(c_name) {
    var strKey = c_name;
    var storage = window.localStorage;
    if (storage.getItem(strKey) != null) {
        //alert(storage.getItem(strKey)+'localStorage');
        return storage.getItem(strKey);
    }
    return "";
}

//浏览器窗口调整大小时
$(window).resize(function () {
    //调整jqGrid的宽
    reSetGridWidth();

});
//重置grid宽度
function reSetGridWidth($jqgridTable) {
    if (!$jqgridTable) {
        //页面中所有grid //可见的
        $jqgridTable = $("table.ui-jqgrid-btable:visible");
    }
    //调整jqGrid的宽
    $jqgridTable.each(function () {
        var rel = $(this).attr("rel");
        if (rel) {
            //为了兼容旧版
            var $form = $("#" + rel);
            var gridWidth = $form.width() * 0.999;
            if ($(this).width() >= gridWidth) {
                $(this).setGridWidth(gridWidth, false);
            }
            else {
                $(this).setGridWidth(gridWidth, true);
            }
        }
        else {
            var gridWidth = $("#gbox_" + $(this).attr('id')).parent().width() * 0.999;
            if ($(this).width() >= gridWidth) {
                $(this).setGridWidth(gridWidth, false);
            }
            else {
                $(this).setGridWidth(gridWidth, true);
            }
        }
    });
}
//字符串转日期，非日期格式返回null
$.strToDate = function (str) {
    var d;
    var d = new Date(str);
    if (isNaN(d)) {
        return null;
    }
    else {
        return d;
    }
}

$.getDate = function (options) {
    if (!!!options) {
        options = {};   //防止报错
    }
    //ute undefined then empty
    if ((options.emptywhenundefined || options.ute) && !!!options.date) {
        return "";
    }
    var oDate = new Date(); //实例一个时间对象；
    if (options.date) {
        if (typeof (options.date) == typeof ('abcc')) {
            var idateStr = options.date;
            idateStr = idateStr.replace(/T/g, ' '); //json T
            idateStr = idateStr.replace(new RegExp(/-/gm), "/"); 　　//将所有的'-'转为'/'即可
            if (idateStr.length > 19) {
                idateStr = idateStr.substr(0, 19);  //去除毫秒位，否则IE报错
            }
            oDate = new Date(idateStr);
        }
        else {
            oDate = options.date;  //用传进来的那个日期
        }
    }
    var year = oDate.getFullYear();   //获取系统的年；
    var month = oDate.getMonth() + 1;   //获取系统月份，由于月份是从0开始计算，所以要加1
    if (month < 10) {
        month = "0" + month;
    }
    var date = oDate.getDate(); // 获取系统日，
    if (date < 10) {
        date = "0" + date;
    }
    return year + "-" + month + "-" + date;
}

$.getTime = function (options) {
    if (!!!options) {
        options = {};   //防止报错
    }
    if ((options.emptywhenundefined || options.ute) && !!!options.date) {
        return "";
    }
    var oDate = new Date(); //实例一个时间对象；
    if (options.date) {
        if (typeof (options.date) == typeof ('abcc')) {
            var idateStr = options.date;
            idateStr = idateStr.replace(/T/g, ' '); //json T
            idateStr = idateStr.replace(new RegExp(/-/gm), "/"); 　　//将所有的'-'转为'/'即可
            if (idateStr.length > 19) {
                idateStr = idateStr.substr(0, 19);  //去除毫秒位，否则IE报错
            }
            oDate = new Date(idateStr);
        }
        else {
            oDate = options.date;  //用传进来的那个日期
        }
    }
    var year = oDate.getFullYear();   //获取系统的年；
    var month = oDate.getMonth() + 1;   //获取系统月份，由于月份是从0开始计算，所以要加1
    var hour = oDate.getHours();
    var min = oDate.getMinutes();
    if (month < 10) {
        month = "0" + month;
    }
    var date = oDate.getDate(); // 获取系统日，
    if (date < 10) {
        date = "0" + date;
    }
    if (hour < 10) {
        hour = "0" + hour;
    }
    if (min < 10) {
        min = "0" + min;
    }
    var reslut = year + "-" + month + "-" + date + " " + hour + ":" + min;
    if (!(options.second === false)) {
        var second = oDate.getSeconds();
        if (second < 10) {
            second = "0" + second;
        }
        reslut = reslut + ":" + second;
    }
    if (options.ms === true) {
        var second = oDate.getMilliseconds();
        if (second < 10) {
            second = "00" + second;
        }
        else if (second < 100) {
            second = "0" + second;
        }
        reslut = reslut + "." + second;
    }
    return reslut;
}

/* 已过时 */
//根据出生年月获取年龄
function GetAgeByCsny(strBirthday) {
    if (!!!strBirthday) {
        return "";
    }
    var returnAge;
    strBirthday = strBirthday.split(" ")[0];
    var strBirthdayArr = strBirthday.split("-");
    var birthYear = strBirthdayArr[0];
    var birthMonth = strBirthdayArr[1];
    var birthDay = strBirthdayArr[2];

    d = new Date();
    var nowYear = d.getFullYear();
    var nowMonth = d.getMonth() + 1;
    var nowDay = d.getDate();

    if (nowYear == birthYear) {
        returnAge = 0;//同年 则为0岁
    }
    else {
        var ageDiff = nowYear - birthYear; //年之差
        if (ageDiff > 0) {
            if (nowMonth == birthMonth) {
                var dayDiff = nowDay - birthDay;//日之差
                if (dayDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
            else {
                var monthDiff = nowMonth - birthMonth;//月之差
                if (monthDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
        }
        else {
            returnAge = -1;//返回-1 表示出生日期输入错误 晚于今天
        }
    }

    return returnAge;//返回周岁年龄

}
//如果不小于1年 岁；否则如果不小于1个月 N月M天；否则不小于3天 天；否则小时
function getAgeFromBirthTime(options) {
    try {
        if (!options.begin) {
            return { nl: -1, nldw: -1, text: '' };
        }
        //格式化一下 成yyyy-MM-dd HH:mm:ss
        options.begin = $.getTime({ date: options.begin });
        if (!options.end) {
            options.end = $.getTime();
        }
        else if (options.end.length == 10) {
            //未提供时分秒 格式化一下
            options.end = $.getTime({ date: options.end });
        }

        if (options.begin >= options.end) {
            return { nl: -1, nldw: -1, text: '' };
        }

        var reg = new RegExp(
            /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})(\s)(\d{1,2})(:)(\d{1,2})(:{0,1})(\d{0,2})$/);
        var beginArr = options.begin.match(reg);
        var endArr = options.end.match(reg);

        var days = 0;
        var month = 0;
        var year = 0;

        days = endArr[4] - beginArr[4];
        if (days < 0) {
            month = -1;
            days = 30 + days;
        }

        month = month + (endArr[3] - beginArr[3]);
        if (month < 0) {
            year = -1;
            month = 12 + month;
        }

        year = year + (endArr[1] - beginArr[1]);

        if (year < 0) {
            return { nl: -1, nldw: -1, text: '' };
        }

        var nl = -1; //年龄 数字
        var nldw = -1;   //年龄单位 1、年 2、月 3、日4、小时
        var nlStr = '';

        if (year >= 1) {
            //年
            nldw = 1;
            nl = year;
            if (month >= 6) {
                nl = nl + 1;
            }
            nlStr = nl + '岁';
        }
        else if (month >= 1) {
            //月
            nldw = 2;
            nl = month;
            nlStr = nl + '个月';
        }
        else if (days >= 3) {
            //天
            nldw = 3;
            nl = days;
            nlStr = nl + '天';
        }
        else {
            //小时
            nldw = 4;
            var begDate = new Date(beginArr[1], beginArr[3] - 1,
                beginArr[4], beginArr[6], beginArr[8], beginArr[10]);
            var endDate = new Date(endArr[1], endArr[3] - 1, endArr[4],
                endArr[6], endArr[8], endArr[10]);

            var between = (endDate.getTime() - begDate.getTime()) / 1000;
            days = Math.floor(between / (24 * 3600));
            var hours = Math.floor(between / 3600 - (days * 24));
            nl = days * 24 + hours;
            if (nl == 0) {
                nl = 1; //1小时
            }
            nlStr = nl + '小时';
        }

        return { nl: nl, nldw: nldw, text: nlStr };
    }
    catch (errr) {
        return { nl: -1, nldw: -1, text: '' };
    }
}
/* 已过时 */
function getBrowserType() {
    return "Chrome";
}

//四舍六入五成双
function roundingBy4she6ru5chengshuang(src, pos) {
    if (isNaN(src)) {
        throw Error('src not valid');
    }
    var result = 0;
    var isNagt = false;  //是负数  //这样是不准的
    if (src < 0) {
        //throw Error('negative not support');
        src = -src;
        isNagt = true;
    }
    //src 11.2350000
    src = src.toString();//.replace("-", "");   //‘11.2350000’
    var pointIndex = src.indexOf('.');  //2
    if (pointIndex <= 0) {
        //原来就没有小数点
        result = parseFloat(src).toFixed(pos);
    }
    else {
        //
        if (pointIndex + pos + 1 >= src.length) {
            //.后面的位数本来就不够
            result = parseFloat(src).toFixed(pos);
        }
        else {
            //在src中找到我们要保留的那一位所在的index
            var resultIndex = pointIndex + pos; //4
            if (src.substr(resultIndex + 1, 1) <= '4') {
                //4则舍
                result = parseFloat(src.substring(0, resultIndex + 1)).toFixed(pos);
            }
            else if (src.substr(resultIndex + 1, 1) >= '6') {
                //6则入
                result = (parseFloat(src.substring(0, resultIndex + 1))
                    + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);
            }
            else {
                //src.substring(5, 1) 5
                var tn = true; //5 后面没有非‘0’数
                for (iiiii = resultIndex + 2; iiiii <= src.length - 1; iiiii++) {
                    if (src.substr(iiiii, 1) != '0') {
                        tn = false;
                        break;
                    }
                }
                if (!tn) {
                    //5的后面还有不为‘0’的数 一定进位
                    result = (parseFloat(src.substring(0, resultIndex + 1))
                        + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);
                }
                else {
                    //考虑奇偶
                    if (parseInt(src.substr(resultIndex, 1)) % 2 == 0) {
                        result = parseFloat(src.substring(0, resultIndex + 1)).toFixed(pos);
                    }
                    else {
                        result = (parseFloat(src.substring(0, resultIndex + 1))
                            + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);  //凑偶
                    }
                }
            }
        }
    }
    if (isNagt) {
        return -result;
    }
    return result;
}
//加载导航树
$.LoadNavTree = function () {
    var data = top.clients.authorizeMenu;
    var _html = "";

    var language_type = top.$.cookie($.getCurrentAppId() + '_Newtouch_language_type');

    $.each(data, function (i) {
        var row = data[i];
        if (row.ParentId == "0" || !row.ParentId) {
            _html += '<li>';
            _html += '<a data-id="' + row.Id + '" href="#" class="dropdown-toggle"><i class="'
                + row.Icon + '"></i><span data-cnName="' + row.Name + '">'
                + ((language_type == 'en' && row.EnName) ? row.EnName : row.Name)
                + '</span><i class="fa fa-angle-right drop-icon"></i></a>';
            var childNodes = row.ChildNodes;
            if (childNodes.length > 0) {
                _html += '<ul class="submenu">';
                $.each(childNodes, function (i) {
                    var subrow = childNodes[i];
                    _html += '<li>';
                    _html += '<a class="menuItem" data-target="' + subrow.Target
                        + '" data-cnName="' + subrow.Name
                        + '" title="' + (!!subrow.Description ? subrow.Description : ((language_type == 'en' && subrow.EnName) ? subrow.EnName : subrow.Name))
                        + '" data-AppId="' + (!!subrow.AppId ? subrow.AppId : "") + '" data-id="' + subrow.Id + '" href="' + subrow.UrlAddress + '" data-index="' + subrow.px + '">' + ((language_type == 'en' && subrow.EnName) ? subrow.EnName : subrow.Name) + '</a>';
                    _html += '</li>';
                });
                _html += '</ul>';
            }
            _html += '</li>';
        }
    });
    $("#sidebar-nav ul").prepend(_html);
}
//prototype扩展 start
/**  
* js时间对象的格式化; 
* eg:format="yyyy-MM-dd hh:mm:ss";   
*/
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,  //month   
        "d+": this.getDate(),     //day   
        "h+": this.getHours(),    //hour   
        "m+": this.getMinutes(),  //minute   
        "s+": this.getSeconds(), //second   
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter   
        "S": this.getMilliseconds() //millisecond   
    }
    var week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(w+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, week[this.getDay()]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return fmt;
}

/** 
*js中更改日期  
* y年， m月， d日， h小时， n分钟，s秒  
*/
Date.prototype.add = function (part, value) {
    value *= 1;
    if (isNaN(value)) {
        value = 0;
    }
    switch (part) {
        case "y":
            this.setFullYear(this.getFullYear() + value);
            break;
        case "m":
            this.setMonth(this.getMonth() + value);
            break;
        case "d":
            this.setDate(this.getDate() + value);
            break;
        case "h":
            this.setHours(this.getHours() + value);
            break;
        case "n":
            this.setMinutes(this.getMinutes() + value);
            break;
        case "s":
            this.setSeconds(this.getSeconds() + value);
            break;
        default:

    }
}
$.dateAdd = function (date, part, value) {
    date.add(part, value);
    return date;
}
//字符串EndWith
String.prototype.StartWith = function (str) {
    var reg = new RegExp("^" + str);
    return reg.test(this);
};
String.prototype.EndWith = function (str) {
    var reg = new RegExp(str + "$");
    return reg.test(this);
};
//prototype扩展 end

//Array Remove start
Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};
//Array Remove end

//不区分大小写IndexOf
String.prototype.indexOfIgnoreCase = function (str) {
    if ((this || this == '0') && (str || str == '0')) {
        return this.toLowerCase().indexOf(str.toLowerCase());
    }
    else {
        return -1;
    }
}
//不区分大小写 ==
String.prototype.equalsIgnoreCase = function (str) {
    if ((this || this == '0') && (str || str == '0')) {
        return this.toLowerCase() === str.toLowerCase();
    }
    else {
        return -1;
    }
}