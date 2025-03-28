(function ($) {
    //
    var initedAppIdArr = new Array();
    //
    $.clients = {
        extToClients: function (data, appId) {
            //遍历data的属性 扩展到clients
            if (!!!appId) {
                if (!!!top.top.clients) {
                    top.top.clients = {
                    };
                }
            }
            else {
                if (!!!top.top.appclients) {
                    top.top.appclients = {
                    };
                }
                if (!!!top.top.appclients[appId]) {
                    top.top.appclients[appId] = {
                    };
                }
            }
            for (dataitemName in data) {
                var itemdata = data[dataitemName];
                if (typeof (itemdata) == typeof ('abc')) {
                    //字符串则要使用eval方法
                    itemdata = eval(itemdata);
                }
                if (!!!appId) {
                    top.top.clients[dataitemName] = itemdata;
                }
                else {
                    top.top.appclients[appId][dataitemName] = itemdata;
                }
            }
        },
        funcClientsAjax: function (url, async, appId) {
            $.ajax({
                url: url,
                type: "get",
                dataType: "json",
                async: async,
                cache: false,
                success: function (data) {
                    $.clients.extToClients(data, appId);
                }
            });
        },
        checkFirst: function (appId) {
            if (!!!appId) {
                appId = "";
            }
            var matchedd = $.jsonWhere(initedAppIdArr, function (v) {
                return v == appId;
            });
            if (matchedd && matchedd.length && matchedd.length > 0) {
                return false;
            }
            initedAppIdArr.push(appId);
            return true;
        },
        get: function (appId, key) {
            if (top.top.appclients
                && top.top.appclients[appId]
                && top.top.appclients[appId][key]) {
                return top.top.appclients[appId][key];
            }
            else {
                return top.top.clients[key];
            }
        },
        init: function (appId, locationHost) {
            if ($.clients.checkFirst(appId)) {
                //避免多次加载
                if (!!!appId) {
                    //同步请求菜单
                    $.clients.funcClientsAjax("/ClientsData/GetModuleDataJson", false);
                    //同步请求
                    $.clients.funcClientsAjax("/ClientsData/GetClientsDataJson", false);
                    //异步请求
                    $.clients.funcClientsAjax("/ClientsData/GetAsyncClientsDataJson", true);
                }
                else {
                    //均以同步请求方式来发送
                    $.clients.funcClientsAjax((!!locationHost ? locationHost : "") + "/ClientsData/GetClientsDataJson", false, appId);
                    $.clients.funcClientsAjax((!!locationHost ? locationHost : "") + "/ClientsData/GetAsyncClientsDataJson", false, appId);
                }
            }
        }
    };

    $(function () {
        $.clients.init();
    });
})(jQuery);