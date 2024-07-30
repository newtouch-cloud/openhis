/* newtouch 浮层选择器 */
$.fn.multiquencyNewtouchFloatingSelector = function (options, data) {
    var defaults = {
        height: 400,
        width: 100,
        X: 0,
        Y: 0,
        id: null,
        fiter: null,
        url: null,
        ajaxmethod: 'GET',
        ajaxreqdata: null,
        //function post 或者 jsonp 
        ajaxsync: false,
        ajaxparameters: null,
        //function get
        isjsonp: false,
        //是否使用jsonp
        isinputchangetriggered: true,
        //是input change触发的（比如 单击按钮 浮层出来，就是传false）
        inputtextcheck: null,
        //由input change触发，可先验证输入的文本 function
        mutiselect: false,
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

    options.$this.attr('data-releatednfs', 'true'); //关联浮层
    //创建浮层
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id + '');
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            $divContainer.css({
                height: (options.height + 8),
                width: options.width
            });
            //add by sunny 20180807 频次每行对齐
            var className = "ui-nfs-container";
            if (options.className !== undefined) {
                className = options.className
            }
            $divContainer.append('<div class="' + className + '"><ul></ul>');
            //如果多选，增加确定关闭按钮
            if (options.mutiselect) {
                $divContainer.append('<div class="ui-nfs-container_bottom" style="float: right;width: 95px;margin-top: -5px;"><input type="button" class="btn btn-primary btn_submit"  value="确定" style="display:table-cell;margin-right: 10px;"><input type = "button"  class= "btn btn-primary btn_close" value = "关闭" style = "display:table-cell;" ></div>');
            }
            $divContainer.append('</div >');
            if (options.tableWidth) {
                $divContainer.find('ul').css('width', options.tableWidth);
            }
            options.container = $divContainer;
            options.bindclick();//绑定多选的确认关闭事件
        }
        else {
            options.container = $divContainer;
        }

    };
    //给浮层定位（根据element的位置）
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        } else {
            //根据options.$this的坐标 来定位
            var thisDomPoint = getDOMPositionPoint(options.$this[0]);

            if (options.width > ($(document).width() / 2 + $(document).width() / 4)) {
                options.container.css('left', "10px");
            } else {
                if (thisDomPoint.l < ($(document).width() / 2)) {
                    options.container.css('left', (thisDomPoint.l) + "px");
                } else {
                    options.container.css('left', (thisDomPoint.l - options.width + options.$this.width() + 5) + "px");
                }
            }

            if (($(document).height() - thisDomPoint.t - options.$this.height() - 5) < options.height) {
                //下方不够用,置到上方
                options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");
            } else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");
            }
        }
    };
    //加载数据
    options.loaddata = function () {
        if (data) {
            (options.funcBindRowData)(data);
        }
        else if (!options.url) {
            if (options.filter) {
                if (options.isinputchangetriggered) {
                    var resultObjArr = (options.filter)(options.$this.val());
                } else {
                    var resultObjArr = options.filter();
                } (options.funcBindRowData)(resultObjArr);
            }
        } else {
            //通过接口获取数据
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this); //谁触发的，返回给页面
                if (thisParameters) {
                    thisUrl += "?" + thisParameters;
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
                            if (!!options.lastRequestMark && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                return;
                            }
                            if (options.filter) {
                                data = (options.filter)(data);
                            } (options.funcBindRowData)(data);
                        } else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            } else {
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
                            if (!!options.lastRequestMark && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                return;
                            }

                            if (!(data.data instanceof Object)) {
                                data.data = JSON.parse(data.data);
                            }
                            if (options.filter) {
                                (options.funcBindRowData)((options.filter)(data.data));
                            } (options.funcBindRowData)(data.data);
                        } else {
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
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //清除当前tbody内容
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- 属性赋值
                });
                var itemActived = false;
                if (typeof options.checkItemActivity === 'function') {
                    itemActived = options.checkItemActivity($li, options.$this);
                }
                if (itemActived) {
                    $li.addClass('active');
                }
                $li.appendTo(options.container.find('ul'));
            }
        }
    }
    var selectorsList = [];
    //bind单击选中事件
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'ul li',
                function () {
                    if (options.mutiselect) { //多选
                        if ($(this).hasClass('active')) { //取消选中
                            $(this).removeClass('active');
                        } else { //选中 
                            $(this).addClass('active');
                        };
                    } else //单选
                    {
                        options.itemdbclickhandler($(this), options.$this); //谁触发的，返回给页面
                        if (options.isinputchangetriggered) {
                            options.$this.trigger('focus'); //如果是input本身的change触发 给文本框焦点
                        }
                        if (options.itemdbclickhandleComplete) {
                            ;
                            options.itemdbclickhandleComplete($(this)); //双击一行 处理完之后
                        }
                        options.container.find('ul').html(''); //清除当前tbody内容
                        options.container.hide();
                    }
                });
        }
    }

    options.bindclick = function () {
        //确定 
        options.container.find('.btn_submit').click(function () {
            selectorsList = [];
            options.container.find('li').each(function () {
                if ($(this).hasClass('active')) {
                    selectorsList.push($(this));
                }
            });
            options.itemdbclickhandler(selectorsList, options.$this); //谁触发的，返回给页面
            options.container.find('.btn_close').trigger('click');
        });

        //关闭
        options.container.find('.btn_close').click(function () {
            if (options.isinputchangetriggered) {
                options.$this.trigger('focus'); //如果是input本身的change触发 给文本框焦点
            }
            options.container.find('ul').html(''); //清除当前tbody内容
            options.container.hide();
        });
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //创建浮层
        }
        options.location(); //定位
        options.container.show(); //定位好 再显示
        options.loaddata(); //加载数据
        options.binddbclickchooseevent(); //双击选中事件  //应该是触发一次就可以了
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
        } else if (options.focusautotrigger === true) {
            options.$this.focus(function (event) {
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

        options.$this.keyup(function (event) { //必须是keyup
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) {
                return;
            }
            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) {
                return; //值没有变  则不触发
            }

            //先隐藏
            if (options.container) {
                options.container.find('ul').html(''); //清除当前ul内容
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
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li'); //当前浮层中的所有tr
                if ($trs.length == 0) {
                    return; //没有行
                }
                var curActiveIndex = -1; //当前active的行
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('active')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0; //目标index
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                } else {
                    if (event.keyCode == 37) {
                        //alert("按了←键！");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("按了 →键！");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("按了↑键！");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("按了↓键！");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    } else if (dstnIndex > $trs.length - 1) {
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
            } else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active'); //当前浮层中的所有li
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
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
    } else {
        //由click事件来触发
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //清除当前tbody内容
            }
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

/* 频次浮层选择器 */
$.fn.frequencyNewtouchFloatingSelector = function (options, data) {
    var defaults = {
        height: 400,
        width: 100,
        X: 0,
        Y: 0,
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
        console.log(idtemp);
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required");
        return;
    }

    options.$this.attr('data-releatednfs', 'true'); //关联浮层

    //创建浮层
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id + '');
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            $divContainer.css({
                height: (options.height + 2),
                width: options.width
            });
            //add by sunny 20180807 频次每行对齐
            var className = "ui-nfs-container";
            if (options.className !== undefined) {
                className = options.className
            }
            $divContainer.append('<div class="' + className + '"><ul></ul>');
            if (options.tableWidth) {
                $divContainer.find('ul').css('width', options.tableWidth);
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
                    options.container.css('left', (thisDomPoint.l) + "px");
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
        if (!options.url) {
            if (options.filter) {
                if (options.isinputchangetriggered) {
                    var resultObjArr = (options.filter)(options.$this.val());
                }
                else {
                    var resultObjArr = options.filter();
                }
                (options.funcBindRowData)(resultObjArr);
            }
        }
        else {
            //通过接口获取数据
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this);  //谁触发的，返回给页面
                if (thisParameters) {
                    thisUrl += "?" + thisParameters;
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
                            (options.funcBindRowData)(data.data);
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
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //清除当前tbody内容
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- 属性赋值
                });
                var itemActived = false;
                if (typeof options.checkItemActivity === 'function') {
                    itemActived = options.checkItemActivity($li, options.$this);
                }
                if (itemActived) {
                    $li.addClass('active');
                }
                options.showtitle = 'yzpcmcsm';
                if (options.showtitle && resultObjArr[i][options.showtitle]) {
                    $li.attr('title', resultObjArr[i][options.showtitle]);
                }
                $li.appendTo(options.container.find('ul'));
            }
        }
    }
    //bind单击选中事件
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'ul li', function () {
                options.itemdbclickhandler($(this), options.$this); //谁触发的，返回给页面
                if (options.isinputchangetriggered) {
                    //如果是input本身的change触发 给文本框焦点
                    options.focusfromdbclickitem = true;
                    options.$this.trigger('focus');
                }
                if (options.itemdbclickhandleComplete) {
                    options.itemdbclickhandleComplete($(this));    //双击一行 处理完之后
                }
                options.container.find('ul').html(''); //清除当前tbody内容
                options.container.hide();
            });
        }
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //创建浮层
        }
        options.location();   //定位
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

            //先隐藏
            if (options.container) {
                options.container.find('ul').html(''); //清除当前ul内容
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
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li');    //当前浮层中的所有tr
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
                    if (event.keyCode == 37) {
                        //alert("按了←键！");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("按了 →键！");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("按了↑键！");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("按了↓键！");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
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
                    var $activeTrs = options.container.find('ul li.active');    //当前浮层中的所有li
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
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
            if (options.container) {
                options.container.find('ul li').html(''); //清除当前tbody内容
            }
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