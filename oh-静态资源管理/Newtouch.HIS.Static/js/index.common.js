var storage, fail, uid; try { uid = new Date; (storage = window.localStorage).setItem(uid, uid); fail = storage.getItem(uid) != uid; storage.removeItem(uid); fail && (storage = false); } catch (e) { }
if (storage) {
    var usedSkin = localStorage.getItem('config-skin');
    if (usedSkin != '' && usedSkin != null) {
        document.body.className = usedSkin;
    }
    else {
        document.body.className = 'theme-blue-gradient';
        localStorage.setItem('config-skin', "theme-blue-gradient");
    }
}
else {
    document.body.className = 'theme-blue';
}
$(function () {
    if (storage) {
        try {
            var usedSkin = localStorage.getItem('config-skin');
            if (usedSkin != '') {
                $('#skin-colors .skin-changer').removeClass('active'); $('#skin-colors .skin-changer[data-skin="' + usedSkin + '"]').addClass('active');
            }
        }
        catch (e) { console.log(e); }
    }
})
$.fn.removeClassPrefix = function (prefix) {
    this.each(function (i, el) {
        var classes = el.className.split(" ").filter(function (c) {
            return c.lastIndexOf(prefix, 0) !== 0;
        });
        el.className = classes.join(" ");
    });
    return this;
};
$(function ($) {
    $('#config-tool-cog').on('click', function () { $('#config-tool').toggleClass('closed'); }); $('#config-fixed-header').on('change', function () {
        var fixedHeader = '';
        if ($(this).is(':checked')) {
            $('body').addClass('fixed-header'); fixedHeader = 'fixed-header';
        }
        else {
            $('body').removeClass('fixed-header');
            if ($('#config-fixed-sidebar').is(':checked')) {
                $('#config-fixed-sidebar').prop('checked', false);
                $('#config-fixed-sidebar').trigger('change'); location.reload();
            }
        }
    });
    $('#skin-colors .skin-changer').on('click', function () {
        $('body').removeClassPrefix('theme-');
        $('body').addClass($(this).data('skin'));
        $('#skin-colors .skin-changer').removeClass('active');
        $(this).addClass('active');
        writeStorage(storage, 'config-skin', $(this).data('skin'));
    });
    function writeStorage(storage, key, value) {
        if (storage) {
            try {
                localStorage.setItem(key, value);
            }
            catch (e) { console.log(e); }
        }
    }
});
$(function ($) {
    $("#content-wrapper").find('.mainContent').height($(window).height() - 100);
    $(window).resize(function (e) {
        $("#content-wrapper").find('.mainContent').height($(window).height() - 100);
    });
    $('#sidebar-nav,#nav-col-submenu').on('click', '.dropdown-toggle', function (e) {
        e.preventDefault();
        var $item = $(this).parent();
        if (!$item.hasClass('open')) {
            $item.parent().find('.open .submenu').slideUp('fast');
            $item.parent().find('.open').toggleClass('open');
        }
        $item.toggleClass('open');
        if ($item.hasClass('open')) {
            $item.children('.submenu').slideDown('fast', function () {
                var _height1 = $(window).height() - $item.position().top;
                //var _height1 = $(window).height() - 500;
                var _height2 = $item.find('ul.submenu').height() + 10;
                var _height3 = _height2 > _height1 ? _height1 : _height2;
                $item.find('ul.submenu').css({
                    overflow: "auto",
                    height: _height3
                })
            });
        }
        else {
            $item.children('.submenu').slideUp('fast');
        }
    });
    if (!($('body:eq(0)').attr('data-loadnavtree') === 'false')) {
        $('div#sidebar-nav>ul.nav-stacked:eq(0)').html('');
        $.LoadNavTree();
    }
    else {
        $('div#sidebar-nav>ul.nav-stacked:not(:visible)').show();
    }
    $('body').on('mouseenter', '#page-wrapper.nav-small #sidebar-nav .dropdown-toggle', function (e) {
        if ($(document).width() >= 992) {
            var $item = $(this).parent();
            if ($('body').hasClass('fixed-leftmenu')) {
                var topPosition = $item.position().top;

                if ((topPosition + 4 * $(this).outerHeight()) >= $(window).height()) {
                    topPosition -= 6 * $(this).outerHeight();
                }
                $('#nav-col-submenu').html($item.children('.submenu').clone());
                $('#nav-col-submenu > .submenu').css({ 'top': topPosition });
            }

            $item.addClass('open');
            $item.children('.submenu').slideDown('fast');
        }
    });
    $('body').on('mouseleave', '#page-wrapper.nav-small #sidebar-nav > .nav-pills > li', function (e) {
        if ($(document).width() >= 992) {
            var $item = $(this);
            if ($item.hasClass('open')) {
                $item.find('.open .submenu').slideUp('fast');
                $item.find('.open').removeClass('open');
                $item.children('.submenu').slideUp('fast');
            }
            $item.removeClass('open');
        }
    });
    $('body').on('mouseenter', '#page-wrapper.nav-small #sidebar-nav a:not(.dropdown-toggle)', function (e) {
        if ($('body').hasClass('fixed-leftmenu')) {
            $('#nav-col-submenu').html('');
        }
    });
    $('body').on('mouseleave', '#page-wrapper.nav-small #nav-col', function (e) {
        if ($('body').hasClass('fixed-leftmenu')) {
            $('#nav-col-submenu').html('');
        }
    });
    //$('body').find('#make-small-nav').click(function (e) {
    //    $('#page-wrapper').toggleClass('nav-small');
    //});
    //$('body').find('#make-small-nav').toggle(function (e) {
    //    $('#page-wrapper').addClass('nav-small');
    //    $('#nav-col').css('overflow-y', 'visible');
    //}, function (e) {
    //    $('#page-wrapper').removeClass('nav-small');
    //    $('#nav-col').css('overflow-y', 'scroll');
    //});
    $('body').find('#make-small-nav').click(function (e) {
        if ($('#page-wrapper').hasClass('nav-small')) {
            $('#page-wrapper').removeClass('nav-small');
            $('#nav-col').css('overflow-y', 'scroll');
        }
        else {
            $('#page-wrapper').addClass('nav-small');
            $('#nav-col').css('overflow-y', 'visible');
        }
    });
    $('body').find('.mobile-search').click(function (e) {
        e.preventDefault();
        $('.mobile-search').addClass('active');
        $('.mobile-search form input.form-control').focus();
    });
    $(document).mouseup(function (e) {
        var container = $('.mobile-search');
        if (!container.is(e.target) && container.has(e.target).length === 0) // ... nor a descendant of the container
        {
            container.removeClass('active');
        }
    });
    $(window).load(function () {
        window.setTimeout(function () {
            $('#ajax-loader').fadeOut();
        }, 10);
    });
});