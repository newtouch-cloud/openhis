if ($.getLocationHost() != top.top.$.getLocationHost()) {
    var appId = $.getCurrentAppId();
    if (!!appId) {
        if (top.top.$.clients.checkFirst(appId)) {
            $.ajax({
                url: "/ClientsData/GetClientsDataJson",
                type: "get",
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {
                    top.top.$.clients.extToClients(data, appId);
                }
            });
            $.ajax({
                url: "/ClientsData/GetAsyncClientsDataJson",
                type: "get",
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {
                    top.top.$.clients.extToClients(data, appId);
                }
            });
            //top.top.$.clients.funcClientsAjax($.getLocationHost() + "/ClientsData/GetClientsDataJson", false, appId);
        }
    }
}