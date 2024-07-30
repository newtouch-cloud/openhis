var clients = [];
$(function () {
    clients = $.clientsInit();
})
$.clientsInit = function () {
    var dataJson = {
        dataItems: [],
        topOrganize: [],
        //department: [],
        //authorizeMenu: []
    };
    var init = function () {
        $.ajax({
            url: "/ClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                dataJson.dataItems = data.dataItems;
                dataJson.topOrganize = data.topOrganize;
                //dataJson.department = data.department;
                //dataJson.authorizeMenu = eval(data.authorizeMenu);
            }
        });
    }
    init();
    return dataJson;
}