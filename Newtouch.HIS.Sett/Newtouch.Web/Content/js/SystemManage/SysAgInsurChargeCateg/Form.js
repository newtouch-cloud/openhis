var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemManage/SysAgInsurChargeCateg/GetFormJson",
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
    dlList();
    sfxmList();
}
function dlList() {
    $("#dl").bindSelect({
        url: "/SystemManage/xt_brsfsf/GetdlSelect"
    });
}

function sfxmList() {
    $("#sfxm").bindSelect({
        url: "/SystemManage/xt_brsfsf/GetsfxmSelect"
    });
}

function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/SysAgInsurChargeCateg/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}