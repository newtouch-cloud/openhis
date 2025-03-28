
var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    //if (!!keyValue) {
    $.ajax({
        url: "/SystemManage/SysCISpecialMark/GetFormJson?r=" + Math.random(),
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            if (data["sfxm"]) {
                $("#sfxmmc").attr("data-label", data["sfxm"]);
            }
            if (data["brxz"]) {
                $("#brxzmc").attr("data-label", data["brxz"]);
            }
        }
    });
    //}
})
function InitControl() {
    xmList();//收费项目
    xzList();//病人性质
}
function xmList() {
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

function xzList() {
    $("#brxzmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "病人性质",
        colModel: [
            { label: '编号', name: 'brxzbh', widthratio: 25 },
            { label: '代码', name: 'brxz', widthratio: 25 },
            { label: '名称', name: 'brxzmc', widthratio: 25 },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#brxzmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#brxzmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}
function submitForm() {
    if (checkNotNull()) {
        var data = $("#form1").formSerialize();
        data["sfxm"] = $("#sfxmmc").attr("data-label");
        data["brxz"] = $("#brxzmc").attr("data-label");
        $.submitForm({
            url: "/SystemManage/SysCISpecialMark/SubmitForm?keyValue=" + keyValue,
            param: data,
            success: function () {
                //$.currentWindow().$("#gridList").resetSelection();
                //$.currentWindow().$("#gridList").trigger("reloadGrid");
                //$.loading(false);
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
            brxzmc: { required: true },
            zt: { required: true }
        },
        messages: {
            brxzmc: { required: "病人性质必须填写" },
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
    return true;
}