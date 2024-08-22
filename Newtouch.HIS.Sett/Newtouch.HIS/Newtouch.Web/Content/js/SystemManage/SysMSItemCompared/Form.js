
var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemManage/SysMSItemCompared/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    }
})
function InitControl() {
    xmList();//收费项目
    ksList();//病人性质
}
function xmList() {
    $("#sfxm").bindSelect({
        url: "/SystemManage/SysMSItemCompared/GetxmSelect",
    });
}

function ksList() {
    $("#ks").bindSelect({
        url: "/SystemManage/SysMSItemCompared/GetksSelect"
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/SysMSItemCompared/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}