var clients = [];
$(function () {
    clients = $.clientsInit();
});
$.clientsInit = function () {
    var dataJson = {
        dataItems: [],
        organize: [],
        department: [],
        authorizeMenu: [],
        authorizeButton: []
    };
    var init = function () {
        $.najax({
            url: "/ClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                dataJson.dataItems = data.dataItems;
                dataJson.sysDepartList = data.sysDepartList;
                dataJson.authorizeMenu = eval(data.authorizeMenu);
                dataJson.authorizeButton = eval(data.authorizeButton);
                dataJson.SysFailedCodeMessageMapList = data.SysFailedCodeMessageMapList;

                dataJson.yearArr = data.yearArr;
                dataJson.monthArr = data.monthArr;
            }
        });
    }
    init();
    return dataJson;
}