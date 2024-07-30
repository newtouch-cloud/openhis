var newtouchclients = [];
$(function () {
    //newtouchclients = $.newtouchclientsInit();
});

$.newtouchclientsInit = function () {
    var dataJson = {
    };
    var init = function () {
        $.ajax({
            url: "/NewtouchClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                dataJson.handOutMedicinesrmList = data.handOutMedicinesrmList;
            }
        });
    }
    init();
    return dataJson;
}