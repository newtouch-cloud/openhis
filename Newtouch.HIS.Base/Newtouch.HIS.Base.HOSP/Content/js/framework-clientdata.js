var clients = [];
$(function () {
    $.clientsRefresh();
})

$.clientsRefresh = function () {
    var dataJson = {
        dataItems: [],
        organize: [],
        department: [],
        authorizeMenu: []
    };
    var init = function () {
        $.najax({
            url: "/ClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                dataJson.dataItems = data.dataItems;
                dataJson.organize = data.organize;
                dataJson.department = data.department;
                dataJson.authorizeMenu = eval(data.authorizeMenu);
            }
        });
    }
    init();
    clients = dataJson;
}