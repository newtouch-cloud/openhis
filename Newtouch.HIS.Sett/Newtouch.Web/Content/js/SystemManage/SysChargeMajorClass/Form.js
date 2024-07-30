
var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    //if (!!keyValue) {
        $.ajax({
            url: "/SystemManage/SysChargeMajorClass/GetFormJson?r=" + Math.random(),
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    //}
})
function InitControl() {
    printreportList();
    printbillList();
}
function printreportList() {
    $("#mzprintreportcode").bindSelect({
        url: "/SystemManage/SysChargeMajorClass/GetdlSelect",
    });
}

function printbillList() {
    $("#mzprintbillcode").bindSelect({
        url: "/SystemManage/SysChargeMajorClass/GetdlSelect"
    });
}

function submitForm() {
    if (checkNotNull()) {
        $.submitForm({
            url: "/SystemManage/SysChargeMajorClass/SubmitForm?keyValue=" + keyValue,
            param: $("#form1").formSerialize(),
            success: function () {
                //$.currentWindow().$("#gridList").resetSelection();
                //$.currentWindow().$("#gridList").trigger("reloadGrid");
                $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").resetSelection();
                $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
                $.loading(false);
            }
        })
    }
}

function checkNotNull() {
    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            dlmc: { required: true },
            py: { required: true },
            mzzybz: { required: true },
            mzprintreportcode: { required: true },
            mzprintbillcode: { required: true },
            zt: { required: true },
        },
        messages: {
            dlmc: { required: "大类必须填写" },
            py: { required: "拼音必须填写" },
            mzzybz: { required: "范围必须填写" },
            mzprintreportcode: { required: "门诊报表大类必须填写" },
            mzprintbillcode: { required: "门诊账单大类必须填写" },
            zt: { required: "状态必须选择" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                if (Array.isArray(errorList) && errorList.length == 0) {
                    return;
                }
                $.modalAlert(errorList[0].message, 'warning');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }
    //状态
    var zt = false;
    $('input[name="zt"]').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("active")) {
            zt = true;
            $("#zt").val()
        }
    });
    if (!zt) {
        $.modalAlert("请选择是否有效！", 'warning');
        return false;
    }
    return true;
}