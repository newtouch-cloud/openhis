$(function () {
    $.clientsInit();
})
$.clientsInit = function () {
    var extToClients = function (data) {
        //遍历data的属性 扩展到window.clients
        if (!!!window.clients) {
            window.clients = {};
        }
        for (dataitemName in data) {
            var itemdata = data[dataitemName];
            if (typeof (itemdata) == typeof ('abc')) {
                //字符串则要使用eval方法
                itemdata = eval(itemdata);
            }
            window.clients[dataitemName] = itemdata;
        }
    }
    //发ajax请求
    var funcClientsAjax = function (url, async) {
        $.ajax({
            url: url,
            type: "get",
            dataType: "json",
            async: async,
            cache: false,
            success: function (data) {
                extToClients(data);
            }
        });
    };

    //同步请求  //如菜单内容必须用同步方式
    funcClientsAjax("/ClientsData/GetClientsDataJson", false);
    //异步请求
    funcClientsAjax("/NewtouchClientsData/GetClientsDataJson", true);
}