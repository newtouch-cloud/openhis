﻿
var keyValue = $.request("keyValue");
$(function () {
    if (!!keyValue) {
        $.ajax({
            url: "/SystemManage/xt_xjzffs/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    }
})
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/xt_xjzffs/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}