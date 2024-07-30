function stopDefault(e) {
    e = e || window.event; if (e && e.preventDefault) { e.preventDefault(); }
    else if (window.event) { window.event.returnValue = false; }
    return false;
}
function stopPropagation(e) {
    e = e || window.event; if (e && e.stopPropagation) { e.stopPropagation(); }
    else if (window.event) { window.event.cancelBubble = true; }
    return false;
}
var getDOMPositionPoint = function (obj) {
    var t = obj.offsetTop; var l = obj.offsetLeft; while (obj = obj.offsetParent) { t += obj.offsetTop; l += obj.offsetLeft; }
    return { t: t, l: l };
}
window.newtouch_globalconfig = { $currentActiveWindow: null, f4opions: null, }; var newtouch_setCurrentWindow = function (win) {
    if (!win) { win = window; }
    window.top.top.newtouch_globalconfig.$currentActiveWindow = window.top.newtouch_globalconfig.$currentActiveWindow = window.newtouch_globalconfig.$currentActiveWindow = $(win); if (win.newtouch_globalconfig) { window.top.top.newtouch_globalconfig.f4opions = window.top.newtouch_globalconfig.f4opions = window.newtouch_globalconfig.f4opions = win.newtouch_globalconfig.f4opions; }
}; var newtouch_getCurrentActiveWindow = function () {
    if (window.newtouch_globalconfig.$currentActiveWindow == null) { window.newtouch_globalconfig.$currentActiveWindow = $(window); }
    if (window.newtouch_globalconfig.$currentActiveWindow[0].top == window.newtouch_globalconfig.$currentActiveWindow[0]) { var curFrameDataid = $(window.document).find('.page-tabs-content a.active').attr('data-id'); var $activeIframe = $(window.document).find('.content-iframe iframe[data-id="' + curFrameDataid + '"]'); newtouch_setCurrentWindow($activeIframe[0].contentWindow) }
    return window.newtouch_globalconfig.$currentActiveWindow;
}
var newtouch_globalevent_f1 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); if ($currentActiveWindow[0].newtouch_event_f1) { $currentActiveWindow[0].newtouch_event_f1(event); return true; }
    return false;
}; var newtouch_globalevent_f2 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); if ($currentActiveWindow[0].newtouch_event_f2) { $currentActiveWindow[0].newtouch_event_f2(event); return true; }
    return false;
}; var newtouch_globalevent_f3 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); if ($currentActiveWindow[0].newtouch_event_f3) { $currentActiveWindow[0].newtouch_event_f3(event); return true; }
    return false;
}; var newtouch_globalevent_f4 = function (event, options) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); var defaults = { container: "body", inner: true, justinner: false, }; if (options) { var options = $.extend(defaults, options); }
    else if (window.newtouch_globalconfig.f4opions) { var options = $.extend(defaults, window.newtouch_globalconfig.f4opions); }
    else { options = defaults; }
    if (!(options.justinner === true)) { var $thisContainer = $($currentActiveWindow[0].document).find(options.container); $thisContainer.find(':checked').not('.formClearIgnore').trigger("click"); $thisContainer.find('select').not('.formClearIgnore').not('.ui-pg-selbox').each(function (idx, val) { $(val)[0].selectedIndex = 0; }); $thisContainer.find('select').not('.formClearIgnore').not('.ui-pg-selbox').trigger("change"); $thisContainer.find('textarea').not('.formClearIgnore').val(''); $thisContainer.find('[type=text]').not('.formClearIgnore').val(''); $thisContainer.find('.formValue>label[id]').not('.formClearIgnore').html(''); }
    if (options.inner === true && $currentActiveWindow[0].newtouch_event_f4) { $currentActiveWindow[0].newtouch_event_f4(event); }
    if (event) { stopDefault(event); }
}; var newtouch_globalevent_f5 = function (event) {
    if (top.top.$('.dropdown-menu .tabReload').length > 0) { top.top.$('.dropdown-menu .tabReload').trigger('click'); if (event) { stopDefault(event); } }
    else { window.location.href = window.location.href; }
}; var newtouch_globalevent_f6 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); if ($currentActiveWindow[0].newtouch_event_f6) { $currentActiveWindow[0].newtouch_event_f6(event); return true; }
    return false;
}; var newtouch_globalevent_f7 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); if ($currentActiveWindow[0].newtouch_event_f7) { $currentActiveWindow[0].newtouch_event_f7(event); return true; }
    return false;
}; var newtouch_globalevent_f8 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); if ($currentActiveWindow[0].newtouch_event_f8) { $currentActiveWindow[0].newtouch_event_f8(event); return true; }
    return false;
}; var newtouch_globalevent_f9 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); if ($currentActiveWindow[0].newtouch_event_f9) { $currentActiveWindow[0].newtouch_event_f9(event); return true; }
    return false;
}; var newtouch_globalevent_f10 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow(); if ($currentActiveWindow[0].newtouch_event_f10) { $currentActiveWindow[0].newtouch_event_f10(event); return true; }
    return false;
}; $(document).keydown(function (event) {
    $('.form-an-cur').removeClass('form-an-cur'); event = event || window.event; if ((event.keyCode >= 112 && event.keyCode <= 123)) { newtouch_setCurrentWindow(); }
    if (1 == 2) { }
    else if (((event.keyCode >= 112 && event.keyCode <= 114) || (event.keyCode >= 117 && event.keyCode <= 121)) && top.top.$('#loadingPage:visible').length >= 1) {
        return false;
    }
    else if (event.keyCode == 8) { if (!(event.target.nodeName == 'INPUT' || event.target.nodeName == 'SELECT' || event.target.nodeName == 'TEXTAREA')) { stopDefault(event); stopPropagation(event); return false; } }
    else if (event.keyCode == 112) { if (newtouch_globalevent_f1(event)) { stopDefault(event); stopPropagation(event); } }
    else if (event.keyCode == 113) { if (newtouch_globalevent_f2(event)) { stopDefault(event); stopPropagation(event); } }
    else if (event.keyCode == 114) { if (newtouch_globalevent_f3(event)) { stopDefault(event); stopPropagation(event); } }
    else if (event.keyCode == 115) { newtouch_globalevent_f4(event); stopPropagation(event); }
    else if (event.keyCode == 116) { newtouch_globalevent_f5(event); stopPropagation(event); }
    else if (event.keyCode == 117) { if (newtouch_globalevent_f6(event)) { stopDefault(event); stopPropagation(event); } }
    else if (event.keyCode == 118) { if (newtouch_globalevent_f7(event)) { stopDefault(event); stopPropagation(event); } }
    else if (event.keyCode == 119) { if (newtouch_globalevent_f8(event)) { stopDefault(event); stopPropagation(event); } }
    else if (event.keyCode == 120) { if (newtouch_globalevent_f9(event)) { stopDefault(event); stopPropagation(event); } }
    else if (event.keyCode == 121) { if (newtouch_globalevent_f10(event)) { stopDefault(event); stopPropagation(event); } }
    else if (false) { }
}); $.fn.newtouchBindSelect = function (options) {
    var defaults = { id: "id", text: "text", search: false, change: null, data: null, datasource: null, attdata: null }; var options = $.extend(defaults, options); var $element = $(this); if (options.datasource) { options.data = options.datasource(); }
    $.each(options.data, function (i) {
        if (options.attdata) { $element.append($('<option data-' + options.attdata + '="' + options.data[i][options.attdata] + '"></option>').val(options.data[i][options.id]).html(options.data[i][options.text])); }
        else { $element.append($("<option></option>").val(options.data[i][options.id]).html(options.data[i][options.text])); }
    }); if (options.selectedValue) { $element.val(options.selectedValue); }
    $element.select2({ minimumResultsForSearch: options.search == true ? 0 : -1 }); $element.on("change", function (e) {
        if (options.change != null) { options.change(options.data); }
        $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
    });
}
$.fn.dataNewGrid = function (options, data) {
    var defaults = { datatype: "local", autowidth: true, rownumbers: true, shrinkToFit: false, gridview: true, multiselect: false, caption: "" }; var options = $.extend(defaults, options); if ((options.sortable === false || !!!options.pager) && options.colModel && options.colModel.length > 0) {
        for (i = 0; i < options.colModel.length; i++) { if (options.colModel[i].sortable === undefined) { options.colModel[i].sortable = false; } }
        if (!!!options.pager) { options.rowNum = -1; }
    }
    var $element = $(this); $element.css({ "cursor": "pointer" }); if (!options.onSelectRow) {
        options["onSelectRow"] = function (rowid, status) {
            var $operate = $(".operate"); if ($operate.length > 0) {
                var length = $(this).jqGrid("getGridParam", "selrow").length; if (length > 0) { $operate.animate({ "left": 0 }, 200); } else { $operate.animate({ "left": '-100.1%' }, 200); }
                $operate.find('.close').click(function () { $operate.animate({ "left": '-100.1%' }, 200); });
            }
            if (!options.multiselect && window.btn_selectrow) { btn_selectrow(); }
            if (options.multiselect && window.grid_checkedit) { grid_checkedit(); }
            if (options.onSelectRow_page) { options.onSelectRow_page(rowid, status); }
        };
    }
    if (!options.ondblClickRow) {
        options["ondblClickRow"] = function (rowid) {
            if (options.ondblClickRow_page) { options.ondblClickRow_page(rowid); }
            else if (window.btn_edit) { btn_edit(rowid); }
        }
    }
    $element.jqGrid(options); for (var i = 0; i <= data.length; i++) { $element.jqGrid('addRowData', i + 1, data[i]); }
    if (data.length > 0) { $('.ui-jqgrid-bdiv').find(".unwritten").remove(); }
}; function initLayout(formId) { reSetGridWidth(); return; $("table[rel=" + formId + "]").each(function () { var rel = $(this).attr("rel"); if (rel) { var $form = $("#" + rel); var gridWidth = $form.width() - 3; $(this).setGridWidth(gridWidth, true); } }); }
$.fn.newtouchLocalDataGrid = function (options, data) {
    var defaults = { datatype: "local", autowidth: true, rownumbers: true, shrinkToFit: false, gridview: true, posttofirst: true, showloadingLayer: false }; var options = $.extend(defaults, options); if (!!!options.pager) { options.rowNum = -1; }
    var $element = $(this); if (!options.actions || options.actions.indexOf("declare") >= 0) {
        if (options.colModel && options.colModel.length > 0) { for (i = 0; i < options.colModel.length; i++) { if (options.colModel[i].sortable === undefined) { options.colModel[i].sortable = false; } } }
        $element.jqGrid(options);
    }
    if ((!options.actions || options.actions.indexOf("addRowData") >= 0) && data) {
        var baseId = 0; var curRowIds = $element.jqGrid('getDataIDs'); if (curRowIds && curRowIds.length && curRowIds.length > 0) { for (var j = 0; j < curRowIds.length; j++) { if (curRowIds[j] && parseInt(curRowIds[j]) && parseInt(curRowIds[j]) != NaN && parseInt(curRowIds[j]) > baseId) { baseId = parseInt(curRowIds[j]); } } }
        if (data instanceof Array) {
            if (data.length == 1) {
                if (options.posttofirst) { $element.addRowData(baseId + 1, data[0], "first"); }
                else { $element.addRowData(baseId + 1, data[0]); }
            }
            else {
                if (options.posttofirst) { for (var i = 0; i <= data.length; i++) { $element.addRowData(baseId + i + 1, data[i], "first"); } }
                else { for (var i = 0; i <= data.length; i++) { $element.jqGrid('addRowData', baseId + i + 1, data[i]); } }
            }
        }
        else {
            if (options.posttofirst) { $element.addRowData(baseId + 1, data, "first"); }
            else { $element.addRowData(baseId + 1, data); }
        }
    }
};
$.fn.newtouchFloatingSelector = function (options) {
    var defaults = {
        height: 400, width: 100, X: 0, Y: 0, leftshift: 0, id: null, fiter: null, url: null, ajaxmethod: 'GET', ajaxreqdata: null, ajaxsync: false, ajaxparameters: null, isjsonp: false, isinputchangetriggered: true, inputtextcheck: null,
    };
    var options = $.extend(defaults, options);
    options.$this = $(this);
    if (options.isinputchangetriggered) {
        options.prveValue = options.$this.val();
    }
    if (!options.id) {
        options.id = options.$this.attr('id');
        if (options.id) { options.id = "divfloat_" + options.id; }
    }
    if (!options.id) {
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required"); return;
    }
    if (!options.colModel || options.colModel.length <= 0) {
        throw new Error("argument colModel is required"); return;
    }
    options.$this.attr('data-releatednfs', 'true');
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id);
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;overflow:auto;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            var thisContainerHeight = options.height + 2;
            if (options.tableWidth && options.width <= options.tableWidth + 15) {
                thisContainerHeight += 15;
            }
            $divContainer.css({ height: thisContainerHeight });
            if ($(document).width() - options.width - 10 > 0) {
                $divContainer.css({ width: options.width });
            } else {
                $divContainer.css({ width: $(document).width() - 15 });
            }
            $divContainer.append('<div class="ui-nfs-container"><table></table></div>');
            if (options.tableWidth) {
                $divContainer.find('table').css('width', options.tableWidth);
            }
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
                        thHtml += ' class="isthelasttd"';
                    }
                    thHtml += '>' + options.colModel[i].label + '</th>';
                    $divContainer.find('table thead tr').append(thHtml);
                }
            }
        }
        options.container = $divContainer;
    };
    options.location = function () {
        if (options.X && options.Y) { options.container.css('left', options.X); options.container.css('top', options.Y); }
        else {
            var thisDomPoint = getDOMPositionPoint(options.$this[0]);
            if (options.width > ($(document).width() / 2 + $(document).width() / 4)) {
                options.container.css('left', "10px");
            }
            else {
                if (thisDomPoint.l < ($(document).width() / 2)) {
                    if (options.leftshift) { options.container.css('left', (thisDomPoint.l - options.leftshift) + "px"); }
                    else { options.container.css('left', (thisDomPoint.l) + "px"); }
                }
                else { options.container.css('left', (thisDomPoint.l - options.width + options.$this.width() + 5) + "px"); }
            }
            if (($(document).height() - thisDomPoint.t - options.$this.height() - 5) < options.height) {
                if ((thisDomPoint.t - options.height - 3) >= 0) {
                    options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");//放上面
                } else {
                    options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");//放下面
                    options.container.css({
                        height: ($(document).height() - thisDomPoint.t - options.$this.height() - 10)
                    });
                }
            } else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");//放下面
            }
        }
    };
    options.loaddata = function () {
        if (!options.url) {
            if (options.filter) {
                if (options.isinputchangetriggered) { var resultObjArr = (options.filter)(options.$this.val()); }
                else { var resultObjArr = options.filter(); }
                (options.funcBindRowData)(resultObjArr);
            }
        }
        else {
            var thisUrl = options.url; var thisReqData = null; if (options.ajaxparameters) { var thisParameters = options.ajaxparameters(options.$this); if (thisParameters) { thisUrl += "?" + $.encodeRequestParameters(thisParameters); } }
            if (options.ajaxreqdata) { var thisReqData = options.ajaxreqdata(); }
            options.lastRequestMark = thisUrl + (!!thisReqData ? thisReqData.toString() : ''); if (!options.isjsonp) {
                $.ajax({
                    type: options.ajaxmethod, url: thisUrl, data: thisReqData, dataType: "json", cache: false, success: function (data) {
                        if (data && data.length && data.length > 0) {
                            if (!!options.lastRequestMark && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) { return; }
                            if (options.filter) { data = (options.filter)(data); }
                            (options.funcBindRowData)(data);
                        }
                        else { }
                    }, error: function () { options.container.hide(); }
                });
            }
            else {
                $.ajax({
                    url: thisUrl, dataType: 'JSONP', jsonp: 'jsonpCallback', jsonpCallback: "just_callback_in_success", data: thisReqData, success: function (data) {
                        if (data && data.code == "10000" && data.data) {
                            if (!!options.lastRequestMark && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) { return; }
                            if (!(data.data instanceof Object)) { data.data = JSON.parse(data.data); }
                            if (options.filter) { (options.funcBindRowData)((options.filter)(data.data)); }
                            (options.funcBindRowData)(data.data);
                        }
                        else { }
                    }, error: function () { options.container.hide(); }
                });
            }
        }
    };
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr && options.colModel && options.colModel.length > 0) {
            if (options.container) { options.container.find('table tbody').html(''); }
            if (options.container.find('table tbody').length <= 0) { options.container.find('table').append('<tbody></tbody>'); }
            if (options.height) { options.container.find('table tbody').css('height', (options.height - 33)); }
            for (i = 0; i < resultObjArr.length; i++) {
                var trHtml = '<tr'; if (i == 0) { trHtml += ' class="active"'; }
                for (j = 0; j < options.colModel.length; j++) {
                    var thistdVal = resultObjArr[i][options.colModel[j].name]; if (options.colModel[j].formatter) {
                        if ($.isFunction(options.colModel[j].formatter)) { thistdVal = options.colModel[j].formatter(thistdVal, null, resultObjArr[i]); }
                        else {
                            if (options.colModel[j].formatter === "date") { if (thistdVal && $.strToDate(thistdVal)) { thistdVal = $.getDate({ date: $.strToDate(thistdVal) }); } }
                            else if (options.colModel[j].formatter === "datetime") { if (thistdVal && $.strToDate(thistdVal)) { thistdVal = $.getTime({ date: $.strToDate(thistdVal), second: false }); } }
                        }
                    }
                    trHtml += ' data-' + options.colModel[j].name + '="' + (thistdVal === null ? "" : thistdVal) + '"';
                }
                trHtml += ">"; for (j = 0; j < options.colModel.length; j++) {
                    trHtml += '<td'; trHtml += ' style="'; if (options.colModel[j].widthratio) { trHtml += ' width:' + options.colModel[j].widthratio + '%;'; }
                    else if (options.colModel[j].width) { trHtml += 'width:' + options.colModel[j].width + 'px;'; }
                    if (options.colModel[j].align) { trHtml += 'text-align:' + options.colModel[j].align + ';'; }
                    if (options.colModel[j].hidden && options.colModel[j].hidden == true) { trHtml += 'display:none;'; }
                    trHtml += '"'; if (j == options.colModel.length - 1) { trHtml += ' class="isthelasttd"'; }
                    var thistdVal = resultObjArr[i][options.colModel[j].name]; if (options.colModel[j].formatter) {
                        if ($.isFunction(options.colModel[j].formatter)) { thistdVal = options.colModel[j].formatter(thistdVal, null, resultObjArr[i]); }
                        else {
                            if (options.colModel[j].formatter === "date") { if (thistdVal && $.strToDate(thistdVal)) { thistdVal = $.getDate({ date: $.strToDate(thistdVal) }); } }
                            else if (options.colModel[j].formatter === "datetime") { if (thistdVal && $.strToDate(thistdVal)) { thistdVal = $.getTime({ date: $.strToDate(thistdVal), second: false }); } }
                        }
                    }
                    trHtml += '>' + (thistdVal === null ? "" : thistdVal) + '</td>';
                }
                trHtml += "</tr>"; options.container.find('table tbody').append(trHtml);
            }
            if (options.tableWidth && options.width <= options.tableWidth + 15) {
                var tblHeight = resultObjArr.length * 40;
                if (tblHeight > options.height) {
                    $('body').find('div#' + options.id).find('table tbody').css('height', '');
                    $('body').find('div#' + options.id).find('table').css('height', resultObjArr.length * 26 + 10 + 33);
                }
            }
        }
    }
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind(); options.container.on('click', 'tbody tr',
                function () {
                    var godbclick = true;
                    if (options.itemdbclickhandleBefore) {
                        godbclick = options.itemdbclickhandleBefore($(this));    //双击一行 处理之前
                    }
                    if (!(godbclick === false)) {
                        options.itemdbclickhandler($(this), options.$this); //谁触发的，返回给页面
                    }
                    if (options.isinputchangetriggered) {
                        options.focusfromdbclickitem = true; options.$this.trigger('focus');
                    }
                    if (options.itemdbclickhandleComplete) { options.itemdbclickhandleComplete($(this)); }
                    options.container.find('table tbody').html(''); options.container.hide();
                });
        }
    }
    options.okload = function () {
        if (!options.container) { options.create(); }
        options.location(); if (options.container) { options.container.find('table tbody').html(''); }
        options.container.show(); options.loaddata(); options.binddbclickchooseevent();
    }
    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide(); options.prveValue = options.$this.val(); if (options.minlength && $.trim(options.$this.val()).length < options.minlength) { return; }
                if (typeof options.inputtextcheck === 'function') { if (!(options.inputtextcheck(options.$this.val()))) { return; } }
                options.okload();
            });
        }
        else if (options.focusautotrigger === true) {
            options.$this.focus(function (event) {
                if (options.focusfromdbclickitem === true) { options.focusfromdbclickitem = false; return; }
                $('body div.ui-nfs:visible').hide(); options.prveValue = options.$this.val(); if (options.minlength && $.trim(options.$this.val()).length < options.minlength) { return; }
                if (typeof options.inputtextcheck === 'function') { if (!(options.inputtextcheck(options.$this.val()))) { return; } }
                options.okload();
            });
        }
        options.$this.keyup(function (event) {
            event = event || window.event; if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) { return; }
            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) { return; }
            $('body div.ui-nfs:visible').hide(); options.prveValue = options.$this.val(); if (options.minlength && $.trim(options.$this.val()).length < options.minlength) { return; }
            if (typeof options.inputtextcheck === 'function') { if (!(options.inputtextcheck(options.$this.val()))) { return; } }
            options.okload();
        }); options.$this.keydown(function (event) {
            event = event || window.event; if (event.keyCode == 38 || event.keyCode == 40 && options.container) {
                var $trs = options.container.find('table tbody tr'); if ($trs.length == 0) { return; }
                var curActiveIndex = -1; for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) { if ($($trs[iiiiii]).hasClass('active')) { curActiveIndex = iiiiii; } }
                var dstnIndex = 0; if (curActiveIndex == -1) { dstnIndex = 0; }
                else {
                    if (event.keyCode == 38) { dstnIndex = curActiveIndex - 1; }
                    if (event.keyCode == 40) { dstnIndex = curActiveIndex + 1; }
                    if (dstnIndex < 0) { dstnIndex = 0; }
                    else if (dstnIndex > $trs.length - 1) { dstnIndex = 0; }
                }
                var scrollTop = $trs[dstnIndex].offsetTop; $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50); $trs.removeClass('active'); $($trs[dstnIndex]).addClass('active'); stopDefault(event); stopPropagation(event);
            }
            else if (event.keyCode == 13) { if (options.container) { var $activeTrs = options.container.find('table tbody tr.active'); if ($activeTrs.length == 1) { $activeTrs.trigger('click'); } } }
        }); $(document).click(function (e) {
            e = e || window.event; $target = $(e.target); if (($target.attr('data-releatednfs') === 'true')) { return; }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) { $('body div.ui-nfs:visible').hide(); }
        })
    }
    else {
        options.$this.click(function () { $('body div.ui-nfs:visible').hide(); options.okload(); }); $(document).click(function (e) {
            e = e || window.event; $target = $(e.target); if (($target.attr('data-releatednfs') === 'true')) { return; }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) { $('body div.ui-nfs:visible').hide(); }
        })
    }
};
$.fn.newtouchBatchFloatingSelector = function (options) { this.each(function () { var defaults = {}; defaults = $.extend(defaults, options); $(this).newtouchFloatingSelector(defaults); }); }; $.newtouchAlert = function (data) {
    if (!$.xhrSuccessDataExCheckHandle(data)) { return false; }
    var msg = data.message; if (!msg) { msg = data.msg; }
    if (!msg) { msg = data.message; }
    var type = 'success'; $.modalAlert(msg, type);
}; (function ($) { newtouch_setCurrentWindow(); $(function () { $('.form-control-focus').focus(); $('.newtouch_Readonly').attr("disabled", "disabled"); CheckStorage(); $('.icontoggle').click(function () { var $dstnDiv = $(this).parent().prev().children('tbody.dispTbody'); if ($(".icontoggle").hasClass("fa-angle-double-down")) { $dstnDiv.fadeToggle("slow"); $(".icontoggle").removeClass("fa-angle-double-down"); $(".icontoggle").addClass("fa-angle-double-up"); } else { $dstnDiv.fadeToggle("hide"); $(".icontoggle").removeClass("fa-angle-double-up"); $(".icontoggle").addClass("fa-angle-double-down"); } }); }); window.onload = function () { AutoNext_V2.init(); }; })(jQuery); var AutoNext_V2 = {
    init: function () {
        $('[id].form-an').keydown(function (event) {
            event = event || window.event; if (event.keyCode == 13) {
                var nodes = $('.form-an:visible'); for (i = 0; i < nodes.length; i++) {
                    if ($(this)[0].id == nodes[i].id) {
                        if ($(this).is('.form-an-end')) { return; }
                        var ancheck = $(this).attr('form-an-ancheck'); var goonnext = true; if (ancheck) { goonnext = eval(ancheck + "('" + $(this).val() + "')"); }
                        if (goonnext === true) {
                            var $nextEle = null; if (i == nodes.length - 1) { $nextEle = $(nodes[0]); }
                            else { $nextEle = $(nodes[i + 1]); }
                            if ($nextEle[0].tagName == 'SELECT') {
                                if ($nextEle.next('.select2').length == 1) { $nextEle.addClass('form-an-cur'); $nextEle.select2("open"); }
                                else { }
                            }
                            else { $nextEle.addClass('form-an-cur').trigger('focus'); }
                        }
                        return;
                    }
                }
            }
        }); $('select[id].form-an').on("select2:select", function (e) {
            var nodes = $('.form-an:visible'); for (i = 0; i < nodes.length; i++) {
                if ($(this)[0].id == nodes[i].id) {
                    if ($(this).is('.form-an-end')) { return; }
                    var ancheck = $(this).attr('form-an-ancheck'); var goonnext = true; if (ancheck) { goonnext = eval(ancheck + "('" + $(this).val() + "')"); }
                    if (goonnext === true) {
                        var $nextEle = null; if (i == nodes.length - 1) { $nextEle = $(nodes[0]); }
                        else { $nextEle = $(nodes[i + 1]); }
                        if ($nextEle[0].tagName == 'SELECT') {
                            if ($nextEle.next('.select2').length == 1) { $nextEle.addClass('form-an-cur'); $nextEle.select2("open"); }
                            else { }
                        }
                        else { $nextEle.addClass('form-an-cur').trigger('focus'); }
                    }
                    return;
                }
            }
        });
    }
}
function CheckStorage() { var basePath = '~/'; var cssPath = getlocalStorage("cssPath"); if (cssPath != "") { $("#color-skin").attr("href", cssPath); } }
function setlocalStorage(c_name, value) { var strKey = c_name; var storage = window.localStorage; storage.setItem(strKey, value); }
function getlocalStorage(c_name) {
    var strKey = c_name; var storage = window.localStorage; if (storage.getItem(strKey) != null) { return storage.getItem(strKey); }
    return "";
}
$(window).resize(function () { reSetGridWidth(); }); function reSetGridWidth($jqgridTable) {
    if (!$jqgridTable) { $jqgridTable = $("table.ui-jqgrid-btable:visible"); }
    $jqgridTable.each(function () {
        var rel = $(this).attr("rel"); if (rel) {
            var $form = $("#" + rel); var gridWidth = $form.width() * 0.999; if ($(this).width() >= gridWidth) { $(this).setGridWidth(gridWidth, false); }
            else { $(this).setGridWidth(gridWidth, true); }
        }
        else {
            var gridWidth = $("#gbox_" + $(this).attr('id')).parent().width() * 0.999; if ($(this).width() >= gridWidth) { $(this).setGridWidth(gridWidth, false); }
            else { $(this).setGridWidth(gridWidth, true); }
        }
    });
}
$.strToDate = function (str) {
    var d; var d = new Date(str); if (isNaN(d)) { return null; }
    else { return d; }
}
$.getDate = function (options) {
    if (!!!options) { options = {}; }
    if ((options.emptywhenundefined || options.ute) && !!!options.date) { return ""; }
    var oDate = new Date(); if (options.date) {
        if (typeof (options.date) == typeof ('abcc')) {
            var idateStr = options.date; idateStr = idateStr.replace(/T/g, ' '); idateStr = idateStr.replace(new RegExp(/-/gm), "/"); if (idateStr.length > 19) { idateStr = idateStr.substr(0, 19); }
            oDate = new Date(idateStr);
        }
        else { oDate = options.date; }
    }
    var year = oDate.getFullYear(); var month = oDate.getMonth() + 1; if (month < 10) { month = "0" + month; }
    var date = oDate.getDate(); if (date < 10) { date = "0" + date; }
    return year + "-" + month + "-" + date;
}
$.getTime = function (options) {
    if (!!!options) { options = {}; }
    if ((options.emptywhenundefined || options.ute) && !!!options.date) { return ""; }
    var oDate = new Date(); if (options.date) {
        if (typeof (options.date) == typeof ('abcc')) {
            var idateStr = options.date; idateStr = idateStr.replace(/T/g, ' '); idateStr = idateStr.replace(new RegExp(/-/gm), "/"); if (idateStr.length > 19) { idateStr = idateStr.substr(0, 19); }
            oDate = new Date(idateStr);
        }
        else { oDate = options.date; }
    }
    var year = oDate.getFullYear(); var month = oDate.getMonth() + 1; var hour = oDate.getHours(); var min = oDate.getMinutes(); if (month < 10) { month = "0" + month; }
    var date = oDate.getDate(); if (date < 10) { date = "0" + date; }
    if (hour < 10) { hour = "0" + hour; }
    if (min < 10) { min = "0" + min; }
    var reslut = year + "-" + month + "-" + date + " " + hour + ":" + min; if (!(options.second === false)) {
        var second = oDate.getSeconds(); if (second < 10) { second = "0" + second; }
        reslut = reslut + ":" + second;
    }
    if (options.ms === true) {
        var second = oDate.getMilliseconds(); if (second < 10) { second = "00" + second; }
        else if (second < 100) { second = "0" + second; }
        reslut = reslut + "." + second;
    }
    return reslut;
}
function GetAgeByCsny(strBirthday) {
    if (!!!strBirthday) { return ""; }
    var returnAge; strBirthday = strBirthday.split(" ")[0]; var strBirthdayArr = strBirthday.split("-"); var birthYear = strBirthdayArr[0]; var birthMonth = strBirthdayArr[1]; var birthDay = strBirthdayArr[2]; d = new Date(); var nowYear = d.getFullYear(); var nowMonth = d.getMonth() + 1; var nowDay = d.getDate(); if (nowYear == birthYear) { returnAge = 0; }
    else {
        var ageDiff = nowYear - birthYear; if (ageDiff > 0) {
            if (nowMonth == birthMonth) {
                var dayDiff = nowDay - birthDay; if (dayDiff < 0) { returnAge = ageDiff - 1; }
                else { returnAge = ageDiff; }
            }
            else {
                var monthDiff = nowMonth - birthMonth; if (monthDiff < 0) { returnAge = ageDiff - 1; }
                else { returnAge = ageDiff; }
            }
        }
        else { returnAge = -1; }
    }
    return returnAge;
}
function getBrowserType() { return "Chrome"; }
function roundingBy4she6ru5chengshuang(src, pos) {
    if (isNaN(src)) { throw Error('src not valid'); }
    var result = 0; var isNagt = false; if (src < 0) { src = -src; isNagt = true; }
    src = src.toString(); var pointIndex = src.indexOf('.'); if (pointIndex <= 0) { result = parseFloat(src).toFixed(pos); }
    else {
        if (pointIndex + pos + 1 >= src.length) { result = parseFloat(src).toFixed(pos); }
        else {
            var resultIndex = pointIndex + pos; if (src.substr(resultIndex + 1, 1) <= '4') { result = parseFloat(src.substring(0, resultIndex + 1)).toFixed(pos); }
            else if (src.substr(resultIndex + 1, 1) >= '6') {
                result = (parseFloat(src.substring(0, resultIndex + 1))
                    + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);
            }
            else {
                var tn = true; for (iiiii = resultIndex + 2; iiiii <= src.length - 1; iiiii++) { if (src.substr(iiiii, 1) != '0') { tn = false; break; } }
                if (!tn) {
                    result = (parseFloat(src.substring(0, resultIndex + 1))
                        + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);
                }
                else {
                    if (parseInt(src.substr(resultIndex, 1)) % 2 == 0) { result = parseFloat(src.substring(0, resultIndex + 1)).toFixed(pos); }
                    else {
                        result = (parseFloat(src.substring(0, resultIndex + 1))
                            + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);
                    }
                }
            }
        }
    }
    if (isNagt) { return -result; }
    return result;
}
$.LoadNavTree = function () {
    var data = top.clients.authorizeMenu; var _html = ""; var language_type = top.$.cookie($.getCurrentAppId() + '_Newtouch_language_type'); $.each(data, function (i) {
        var row = data[i]; if (row.ParentId == "0" || !row.ParentId) {
            _html += '<li>'; _html += '<a data-id="' + row.Id + '" href="#" class="dropdown-toggle"><i class="'
                + row.Icon + '"></i><span data-cnName="' + row.Name + '">'
                + ((language_type == 'en' && row.EnName) ? row.EnName : row.Name)
                + '</span><i class="fa fa-angle-right drop-icon"></i></a>'; var childNodes = row.ChildNodes; if (childNodes.length > 0) {
                    _html += '<ul class="submenu">'; $.each(childNodes, function (i) {
                        var subrow = childNodes[i]; _html += '<li>'; _html += '<a class="menuItem" data-target="' + subrow.Target
                            + '" data-cnName="' + subrow.Name
                            + '" title="' + (!!subrow.Description ? subrow.Description : ((language_type == 'en' && subrow.EnName) ? subrow.EnName : subrow.Name))
                            + '" data-AppId="' + (!!subrow.AppId ? subrow.AppId : "") + '" data-id="' + subrow.Id + '" href="' + subrow.UrlAddress + '" data-index="' + subrow.px + '">' + ((language_type == 'en' && subrow.EnName) ? subrow.EnName : subrow.Name) + '</a>'; _html += '</li>';
                    }); _html += '</ul>';
                }
            _html += '</li>';
        }
    }); $("#sidebar-nav ul").prepend(_html);
}
Date.prototype.format = function (fmt) {
    var o = { "M+": this.getMonth() + 1, "d+": this.getDate(), "h+": this.getHours(), "m+": this.getMinutes(), "s+": this.getSeconds(), "q+": Math.floor((this.getMonth() + 3) / 3), "S": this.getMilliseconds() }
    var week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"]; if (/(y+)/.test(fmt)) { fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length)); }
    if (/(w+)/.test(fmt)) { fmt = fmt.replace(RegExp.$1, week[this.getDay()]); }
    for (var k in o) { if (new RegExp("(" + k + ")").test(fmt)) { fmt = fmt.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length)); } }
    return fmt;
}
Date.prototype.add = function (part, value) {
    value *= 1; if (isNaN(value)) { value = 0; }
    switch (part) { case "y": this.setFullYear(this.getFullYear() + value); break; case "m": this.setMonth(this.getMonth() + value); break; case "d": this.setDate(this.getDate() + value); break; case "h": this.setHours(this.getHours() + value); break; case "n": this.setMinutes(this.getMinutes() + value); break; case "s": this.setSeconds(this.getSeconds() + value); break; default: }
}
$.dateAdd = function (date, part, value) { date.add(part, value); return date; }
String.prototype.StartWith = function (str) { var reg = new RegExp("^" + str); return reg.test(this); }; String.prototype.EndWith = function (str) { var reg = new RegExp(str + "$"); return reg.test(this); }; Array.prototype.remove = function (from, to) { var rest = this.slice((to || from) + 1 || this.length); this.length = from < 0 ? this.length + from : from; return this.push.apply(this, rest); }; String.prototype.indexOfIgnoreCase = function (str) {
    if ((this || this == '0') && (str || str == '0')) { return this.toLowerCase().indexOf(str.toLowerCase()); }
    else { return -1; }
}
String.prototype.equalsIgnoreCase = function (str) {
    if ((this || this == '0') && (str || str == '0')) { return this.toLowerCase() === str.toLowerCase(); }
    else { return -1; }
}