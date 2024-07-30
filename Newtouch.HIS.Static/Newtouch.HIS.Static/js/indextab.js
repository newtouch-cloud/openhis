(function ($) {
    //
    var initedAppIdUAAddrArr = new Array();
    //
    $.Newtouchtab = {
        getIsPageLoadMoniter: function () {
            //是否记录页面load日志
            var temp = $('body:eq(0)').attr('data-NavPageLoadMoniter');
            if (!!temp)
                return true;
            return false;
        },
        getCurrentCurrentModuleidKey: function () {
            return $.getCurrentAppId() + "_Newtouch_currentmoduleid";
        },
        triggerMenuTabActiveEvent: function (dataId, menuname, openType) {
            if (window.menuTabActiveEvent && typeof window.menuTabActiveEvent === 'function') {
                window.menuTabActiveEvent(dataId, menuname, openType);
            }
        },
        requestFullScreen: function () {
            var de = document.documentElement;
            if (de.requestFullscreen) {
                de.requestFullscreen();
            } else if (de.mozRequestFullScreen) {
                de.mozRequestFullScreen();
            } else if (de.webkitRequestFullScreen) {
                de.webkitRequestFullScreen();
            }
        },
        exitFullscreen: function () {
            var de = document;
            if (de.exitFullscreen) {
                de.exitFullscreen();
            } else if (de.mozCancelFullScreen) {
                de.mozCancelFullScreen();
            } else if (de.webkitCancelFullScreen) {
                de.webkitCancelFullScreen();
            }
        },
        refreshTab: function () {
            //靠右 的 刷新当前
            var currentId = $('.page-tabs-content').find('.active').attr('data-id');

            var target = $('.Newtouch_iframe[data-id="' + currentId + '"]');
            if (target.length > 0) {
                var url = target.attr('src');
                $.loading(true);
                target.attr('src', url).load(function () {
                    $.loading(false);
                });
                var dataId = $('a.menuItem[href="' + currentId + '"]').attr('data-id'); //相同Url的Module？
                //菜单切换的事件订阅
                $.Newtouchtab.triggerMenuTabActiveEvent(dataId, $('.page-tabs-content').find('.active').text(), 'refresh');
            }
        },
        activeTab: function () {
            //点击上方的Tab来激活
            var currentId = $(this).data('id');
            if (currentId && !$(this).hasClass('active')) {
                $('.mainContent .Newtouch_iframe').each(function () {
                    if ($(this).data('id') == currentId) {
                        $(this).show().siblings('.Newtouch_iframe').hide();
                        return false;
                    }
                });
                $(this).addClass('active').siblings('.menuTab').removeClass('active');
                $.Newtouchtab.scrollToTab(this);

                var dataId = $('a.menuItem[href="' + currentId + '"]').attr('data-id'); //相同Url的Module？
                $.cookie($.Newtouchtab.getCurrentCurrentModuleidKey(), !!dataId ? dataId : '', { path: "/" });
                //菜单切换的事件订阅
                $.Newtouchtab.triggerMenuTabActiveEvent(dataId, $(this).text());
            }
        },
        closeOtherTabs: function () {
            $('.page-tabs-content').children("[data-id]").find('.fa-times-circle').parents('a').not(".active").each(function () {
                $('.Newtouch_iframe[data-id="' + $(this).data('id') + '"]').remove();
                $(this).remove();
            });
            $('.page-tabs-content').css("margin-left", "0");
        },
        closeTab: function () {
            var closeTabId = $(this).parents('.menuTab').data('id');
            var currentWidth = $(this).parents('.menuTab').width();
            if ($(this).parents('.menuTab').hasClass('active')) {
                if ($(this).parents('.menuTab').next('.menuTab').size()) {
                    var activeId = $(this).parents('.menuTab').next('.menuTab:eq(0)').data('id');
                    if (activeId != "") {
                        var dataId = $('a.menuItem[href="' + activeId + '"]').attr('data-id'); //相同Url的Module？
                        $.cookie($.Newtouchtab.getCurrentCurrentModuleidKey(), !!dataId ? dataId : '', { path: "/" });
                        //菜单切换的事件订阅
                        $.Newtouchtab.triggerMenuTabActiveEvent(dataId, $(this).parents('.menuTab').next('.menuTab:eq(0)').text());
                    }
                    $(this).parents('.menuTab').next('.menuTab:eq(0)').addClass('active');
                    $('.mainContent .Newtouch_iframe').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).show().siblings('.Newtouch_iframe').hide();
                            return false;
                        }
                    });
                    var marginLeftVal = parseInt($('.page-tabs-content').css('margin-left'));
                    if (marginLeftVal < 0) {
                        $('.page-tabs-content').animate({
                            marginLeft: (marginLeftVal + currentWidth) + 'px'
                        }, "fast");
                    }
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .Newtouch_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                }
                else if ($(this).parents('.menuTab').prev('.menuTab').size()) {
                    var activeId = $(this).parents('.menuTab').prev('.menuTab:last').data('id');
                    if (activeId != "") {
                        var dataId = $('a.menuItem[href="' + activeId + '"]').attr('data-id'); //相同Url的Module？
                        $.cookie($.Newtouchtab.getCurrentCurrentModuleidKey(), !!dataId ? dataId : '', { path: "/" });
                        //菜单切换的事件订阅
                        $.Newtouchtab.triggerMenuTabActiveEvent(dataId, $(this).parents('.menuTab').prev('.menuTab:last').text());
                    }
                    $(this).parents('.menuTab').prev('.menuTab:last').addClass('active');
                    $('.mainContent .Newtouch_iframe').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).show().siblings('.Newtouch_iframe').hide();
                            return false;
                        }
                    });
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .Newtouch_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                }
                else {
                    //删除唯一的一个tab
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .Newtouch_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                    $.cookie($.Newtouchtab.getCurrentCurrentModuleidKey(), '', { path: "/" });
                    //$.Newtouchtab.scrollToTab($('.menuTab.active'));
                }
            }
            else {
                $(this).parents('.menuTab').remove();
                $('.mainContent .Newtouch_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        return false;
                    }
                });
                $.Newtouchtab.scrollToTab($('.menuTab.active'));
            }
            return false;
        },
        addTab: function () {
            //点击左侧的菜单项
            $("#header-nav>ul>li.open").removeClass("open");
            var dataId = $(this).attr('data-id');
            var dataUrl = $(this).attr('href');
            var menuName;
            if ($(this).find('.menuName').length == 1) {
                menuName = $.trim($(this).find('.menuName').text());
            }
            else {
                menuName = $.trim($(this).text());
            }
            if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
                return false;
            }

            //AppId
            var thisLinkAppId = $(this).attr('data-AppId');
            if (!!thisLinkAppId) {
                var matchedddd = $.jsonWhere(initedAppIdUAAddrArr, function (v) {
                    return v.AppId == thisLinkAppId;
                });
                var uaAddr;
                if (matchedddd && matchedddd.length > 0) {
                    uaAddr = matchedddd[0].Addr;
                }
                else {
                    $.ajax({
                        url: '/Login/GetUALoginAddres',
                        data: { appId: thisLinkAppId },
                        type: "post",
                        async: false,
                        dataType: "json",
                        success: function (ajaxResp) {
                            if (ajaxResp && ajaxResp.data) {
                                uaAddr = ajaxResp.data;
                                initedAppIdUAAddrArr.push({ AppId: thisLinkAppId, Addr: uaAddr });
                            }
                        }
                    });
                }
                if (!!uaAddr) {
                    dataUrl = uaAddr + "&returnUrl=" + encodeURIComponent(dataUrl);
                }
            }

            if ($(this).attr('data-target') === 'toplocation') {
                top.location.href = dataUrl;
                return false;
            }

            var flag = true;    //是否触发active（已经处于active false）
            $('.menuTab').each(function () {
                if (flag && $(this).data('id') == dataUrl) {
                    if (!$(this).hasClass('active')) {
                        $(this).addClass('active').siblings('.menuTab').removeClass('active');
                        $.Newtouchtab.scrollToTab(this);
                        $('.mainContent .Newtouch_iframe').each(function () {
                            if ($(this).data('id') == dataUrl) {
                                $(this).show().siblings('.Newtouch_iframe').hide();
                                return false;
                            }
                        });
                    }
                    flag = false;
                    return false;
                }
            });

            if (flag) {
                var srcUrl = dataUrl;
                var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataUrl + '">' + menuName + ' <i class="fa fa-times-circle"></i></a>';
                $('.menuTab').removeClass('active');
                var str1 = '<iframe class="Newtouch_iframe" id="iframe' + dataId + '" name="iframe' + dataId + '"  width="100%" height="100%" src="' + srcUrl + '" frameborder="0" data-id="' + dataUrl + '" seamless></iframe>';
                $('.mainContent').find('iframe.Newtouch_iframe').hide();
                $('.mainContent').append(str1);
                $.loading(true);
                var target = $('.mainContent iframe:visible');
                top.iframeReqStratTime = new Date();
                if ($.Newtouchtab.getIsPageLoadMoniter()) {
                    try { target.unbind('load', moniterLogFunc); } catch (e) { };
                }
                target.load(moniterLogFunc = function () {
                    $.loading(false);
                    if ($.Newtouchtab.getIsPageLoadMoniter()) {
                        var reqTotalMS = new Date() - top.iframeReqStratTime;  //毫秒
                        $.ajax({
                            url: '/Home/PageLoadMoniter',
                            data: {
                                url: srcUrl, ms: reqTotalMS, enterTime: $.getTime({ date: top.iframeReqStratTime, ms: true }).toString()
                            },
                            type: 'get',
                            cache: false,
                            dataType: 'json'
                        });
                    }
                });

                $('.menuTabs .page-tabs-content').append(str);
                $.Newtouchtab.scrollToTab($('.menuTab.active'));
            }

            $.cookie($.Newtouchtab.getCurrentCurrentModuleidKey(), !!dataId ? dataId : '', { path: "/" });
            //菜单切换的事件订阅
            $.Newtouchtab.triggerMenuTabActiveEvent(dataId, menuName);

            return false;
        },
        scrollTabRight: function () {
            var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
            var tabOuterWidth = $.Newtouchtab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").width() < visibleWidth) {
                return false;
            } else {
                var tabElement = $(".menuTab:first");
                var offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) < (visibleWidth) && tabElement.length > 0) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                scrollVal = $.Newtouchtab.calSumWidth($(tabElement).prevAll());
                if (scrollVal > 0) {
                    $('.page-tabs-content').animate({
                        marginLeft: 0 - scrollVal + 'px'
                    }, "fast");
                }
            }
        },
        scrollTabLeft: function () {
            var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
            var tabOuterWidth = $.Newtouchtab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").width() < visibleWidth) {
                return false;
            } else {
                var tabElement = $(".menuTab:first");
                var offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                offsetVal = 0;
                if ($.Newtouchtab.calSumWidth($(tabElement).prevAll()) > visibleWidth) {
                    while ((offsetVal + $(tabElement).outerWidth(true)) < (visibleWidth) && tabElement.length > 0) {
                        offsetVal += $(tabElement).outerWidth(true);
                        tabElement = $(tabElement).prev();
                    }
                    scrollVal = $.Newtouchtab.calSumWidth($(tabElement).prevAll());
                }
            }
            $('.page-tabs-content').animate({
                marginLeft: 0 - scrollVal + 'px'
            }, "fast");
        },
        scrollToTab: function (element) {
            var marginLeftVal = $.Newtouchtab.calSumWidth($(element).prevAll()), marginRightVal = $.Newtouchtab.calSumWidth($(element).nextAll());
            var tabOuterWidth = $.Newtouchtab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").outerWidth() < visibleWidth) {
                scrollVal = 0;
            } else if (marginRightVal <= (visibleWidth - $(element).outerWidth(true) - $(element).next().outerWidth(true))) {
                if ((visibleWidth - $(element).next().outerWidth(true)) > marginRightVal) {
                    scrollVal = marginLeftVal;
                    var tabElement = element;
                    while ((scrollVal - $(tabElement).outerWidth()) > ($(".page-tabs-content").outerWidth() - visibleWidth)) {
                        scrollVal -= $(tabElement).prev().outerWidth();
                        tabElement = $(tabElement).prev();
                    }
                }
            } else if (marginLeftVal > (visibleWidth - $(element).outerWidth(true) - $(element).prev().outerWidth(true))) {
                scrollVal = marginLeftVal - $(element).prev().outerWidth(true);
            }
            $('.page-tabs-content').animate({
                marginLeft: 0 - scrollVal + 'px'
            }, "fast");
        },
        calSumWidth: function (element) {
            var width = 0;
            $(element).each(function () {
                width += $(this).outerWidth(true);
            });
            return width;
        },
        triggerMenuItemClick: function (options) {
            if (!!options.m1 && !!options.m2) {
                var $nav1 = top.top.$('#sidebar-nav .dropdown-toggle span:contains("' + options.m1 + '")');
                if ($nav1.length == 1) {
                    //所以一定要维护菜单的 中文名称
                    var $nav2 = $nav1.closest('ul').find('a[data-cnName="' + options.m2 + '"]');
                    if ($nav2.length == 1) {
                        $nav2.trigger('click');
                    }
                }
            }
        },
        addTabWithOutMenu: function (options) {
            //{name: enName: url:}  //会自动识别使用英文名称 还是中文名称
            var language_type = $.cookie($.getCurrentAppId() + '_Newtouch_language_type');
            var menuName;
            var dataUrl = options.url;
			if(options.AppId)
			{
				$.ajax({
					url: '/Login/GetUALoginAddres',
					data: { appId: options.AppId },
					type: "post",
					async: false,
					dataType: "json",
					success: function (ajaxResp) {
						if (ajaxResp && ajaxResp.data) {
							debugger
							uaAddr = ajaxResp.data;
                            dataUrl = uaAddr + "&returnUrl=" + encodeURIComponent(options.url);
						}
					}
				});
			}
            if (language_type == 'en') {
                menuName = options.enName;
            }
            if (!!!menuName) {
                menuName = options.name;
            }
            if (!!dataUrl) {
                var flag = true;    //是否触发active（已经处于active false）
                $('a.menuItem[href]').each(function () {
                    if (flag && dataUrl.equalsIgnoreCase($(this).attr('href'))) {
                        $(this).trigger('click');   //触发菜单点击
                        flag = false;
                        return false;
                    }
                });
                if (flag && !!menuName) {
                    $('.menuTab').each(function () {
                        if (flag && $(this).data('id') == dataUrl) {
                            if (!$(this).hasClass('active')) {
                                $(this).addClass('active').siblings('.menuTab').removeClass('active');
                                $.Newtouchtab.scrollToTab(this);
                                $('.mainContent .Newtouch_iframe').each(function () {
                                    if ($(this).data('id') == dataUrl) {
                                        $(this).show().siblings('.Newtouch_iframe').hide();
                                        return false;
                                    }
                                });
                            }
                            flag = false;
                            return false;
                        }
                    });

                    var dataId = Math.random().toString();
                    if (dataId.length > 2) {
                        dataId = dataId.substring(2);
                    }
                    if (flag) {
                        var srcUrl = dataUrl;
                        var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataUrl + '">' + menuName + ' <i class="fa fa-times-circle"></i></a>';
                        $('.menuTab').removeClass('active');
                        var str1 = '<iframe class="Newtouch_iframe" id="iframe' + dataId + '" name="iframe' + dataId + '"  width="100%" height="100%" src="' + srcUrl + '" frameborder="0" data-id="' + dataUrl + '" seamless></iframe>';
                        $('.mainContent').find('iframe.Newtouch_iframe').hide();
                        $('.mainContent').append(str1);

                        $.loading(true);
                        var target = $('.mainContent iframe:visible');
                        top.iframeReqStratTime = new Date();
                        if ($.Newtouchtab.getIsPageLoadMoniter()) {
                            try { target.unbind('load', moniterLogFunc); } catch (e) { };
                        }
                        target.load(moniterLogFunc = function () {
                            $.loading(false);
                            if ($.Newtouchtab.getIsPageLoadMoniter()) {
                                var reqTotalMS = new Date() - top.iframeReqStratTime;  //毫秒
                                $.ajax({
                                    url: '/Home/PageLoadMoniter',
                                    data: {
                                        url: srcUrl, ms: reqTotalMS, enterTime: $.getTime({ date: top.iframeReqStratTime, ms: true }).toString()
                                    },
                                    type: 'get',
                                    cache: false,
                                    dataType: 'json'
                                });
                            }
                        });

                        $('.menuTabs .page-tabs-content').append(str);
                        $.Newtouchtab.scrollToTab($('.menuTab.active'));
                    }

                    $.cookie($.Newtouchtab.getCurrentCurrentModuleidKey(), '', { path: "/" });
                    //菜单切换的事件订阅
                    $.Newtouchtab.triggerMenuTabActiveEvent('', $(this).text());
                }
            }
        },
        tryCloseTab: function (options) {
            //{name: enName: url: urlPrefix}
            var menuTabs = $('.menuTabs a.menuTab');
            for (var i = 0; i < menuTabs.length; i++) {
                var thisName = $.trim($(menuTabs[i]).text());
                var thisDataId = $.trim($(menuTabs[i]).attr('data-id'));
                //
                var nameMatch = undefined;
                var urlMatch = undefined;
                var urlPrefixMatch = undefined;
                if (options.name || options.enName) {
                    nameMatch = false;
                    if (options.name && options.name === thisName) {
                        nameMatch = true;
                    }
                    if (options.enName && options.enName === thisName) {
                        nameMatch = true;
                    }
                }
                if (options.url) {
                    urlMatch = false;
                    if (thisDataId && options.url.length == thisDataId.length
                    && thisDataId.indexOfIgnoreCase(options.url) == 0) {
                        urlMatch = true;
                    }
                }
                if (options.urlPrefix) {
                    urlPrefixMatch = false;
                    if (thisDataId && thisDataId.indexOfIgnoreCase(options.urlPrefix) == 0) {
                        urlPrefixMatch = true;
                    }
                }
                var isThis = nameMatch !== false && urlMatch !== false && urlPrefixMatch !== false && (nameMatch || urlMatch || urlPrefixMatch);
                if (isThis) {
                    $(menuTabs[i]).find('i').trigger('click');
                    return;
                }
            }
        },
        init: function () {
            $('.menuItem').on('click', $.Newtouchtab.addTab);
            $('.menuItem').on('click', function () {
                $('#sidebar-nav .menuItem').removeClass('active');
                if ($(this).parents('#sidebar-nav').length == 1) {
                    $(this).addClass('active');
                }
            });
            $('.menuTabs').on('click', 'a.menuTab i', $.Newtouchtab.closeTab);
            $('.menuTabs').on('click', 'a.menuTab', $.Newtouchtab.activeTab);
            $('.tabLeft').on('click', $.Newtouchtab.scrollTabLeft);
            $('.tabRight').on('click', $.Newtouchtab.scrollTabRight);
            $('.tabReload').on('click', $.Newtouchtab.refreshTab);
            $('.tabCloseCurrent').on('click', function () {
                $('.page-tabs-content').find('a.menuTab.active i').trigger("click");
            });
            //            $('.tabCloseAll').on('click', function () {
            //                $('.page-tabs-content').children("[data-id]").find('.fa-times-circle')
            //.each(function () {
            //    $('.Newtouch_iframe[data-id="' + $(this).data('id') + '"]').remove();
            //    $(this).parents('a').remove();
            //});
            //                $('.page-tabs-content').children("[data-id]:first").each(function () {
            //                    $('.Newtouch_iframe[data-id="' + $(this).data('id') + '"]').show();
            //                    $(this).addClass("active");
            //                });
            //                $('.page-tabs-content').css("margin-left", "0");
            //            });
            $('.tabCloseAll').on('click', function () {
                $('.page-tabs-content').find('a.menuTab i').trigger("click");
                if (window.menuTabCloseAllEvent && typeof window.menuTabCloseAllEvent === 'function') {
                    window.menuTabCloseAllEvent();
                }
            });
            $('.tabCloseOther').on('click', $.Newtouchtab.closeOtherTabs);
            $('.fullscreen').on('click', function () {
                if (!$(this).attr('fullscreen')) {
                    $(this).attr('fullscreen', 'true');
                    $.Newtouchtab.requestFullScreen();
                } else {
                    $(this).removeAttr('fullscreen')
                    $.Newtouchtab.exitFullscreen();
                }
            });
            //$('.fullscreen').trigger('click');  //立刻触发全屏
        }
    };
    $(function () {
        $.Newtouchtab.init();
    });
})(jQuery);