
var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    //if (!!keyValue) {
    $.ajax({
        url: "/SystemManage/SysCIRemindContent/GetFormJson?r=" + Math.random(),
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            if (data["sfxmmc"]) {
                $("#sfxmmc").attr("data-label", data["sfxmmc"]);
            }
        }
    });
    //}
})
function InitControl() {
    dlList();//收费项目
}
function dlList() {
    $("#sfxmmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.clients.sysChargeItemList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "收费项目",
        colModel: [
            { label: '编号', name: 'sfxmbh', widthratio: 25 },
            { label: '代码', name: 'sfxm', widthratio: 25 },
            { label: '名称', name: 'sfxmmc', widthratio: 25 },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#sfxmmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#sfxmmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}
function submitForm() {
    if (checkNotNull()) {
        var data = $("#form1").formSerialize();
        data["sfxm"] = $("#sfxmmc").attr("data-label");
        $.submitForm({
            url: "/SystemManage/SysCIRemindContent/SubmitForm?keyValue=" + keyValue,
            param: data,
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
            mzjsnr: { required: true },
            zyjsnr: { required: true },
            mzjsjb: { required: true },
            zyjsjb: { required: true },
            zt: { required: true }
        },
        messages: {
            mzjsnr: { required: "门诊警示内容必须填写" },
            zyjsnr: { required: "住院警示内容必须填写" },
            mzjsjb: { required: "门诊警示级别必须选择" },
            zyjsjb: { required: "住院警示级别必须选择" },
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
    ////状态
    //var zt = false;
    //$('input[name="zt"]').each(function () {
    //    var $this = $(this);
    //    if ($this.parent().hasClass("active")) {
    //        zt = true;
    //        $("#zt").val()
    //    }
    //});
    //if (!zt) {
    //    $.modalAlert("请选择是否有效！", 'warning');
    //    return false;
    //}
    return true;
}