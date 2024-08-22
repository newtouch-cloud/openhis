var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemManage/SysPatiChargeLogic/GetFormJson?r=" + Math.random(),
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                if (data) {
                    $("#form1").formSerialize(data);
                    if (data["dlmc"]) {
                        $("#dlmc").attr("data-label", data["dl"]);
                    }
                    if (data["brxzmc"]) {
                        $("#brxzmc").attr("data-label", data["brxz"]);
                    }
                    if (data["sfxmmc"]) {
                        $("#sfxmmc").attr("data-label", data["sfxm"]);
                    }
                }
            }
        });
    }

    $("#dlmc").keyup(function () {
        if ($(this).val().trim() === "*") {
            $("#sfxmmc").attr("disabled", "disabled");
            $("#sfxmmc").css("background-color", "#f1f4f6");
        } else {
            $("#sfxmmc").removeAttr("disabled");
            $("#sfxmmc").css("background-color", "#fff");
        }
    });
});
function InitControl() {
    brxzList();
    dlList();
    sfxmList();

    $("#OrganizeId").bindSelect({
        url: "/SystemManage/Organize/GetChildTreeSelectJson"
    });
}
function brxzList() {
    ///报销政策
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
            { label: "编号", name: 'brxzbh', widthratio: 25 },
            { label: "代码", name: 'brxz', widthratio: 25 },
            { label: "名称", name: 'brxzmc', widthratio: 25 },
            { label: "拼音", name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#brxzmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#brxzmc").val($thistr.find("td:eq(2)").html());
            return;
        }
    });
}

function dlList() {
    $("#dlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            
            //请用ajax

        },
        caption: "大类",
        colModel: [
            { label: "编号", name: "dlCode", widthratio: 25 },
            { label: "名称", name: "dlmc", widthratio: 25 },
            { label: "拼音", name: "py", widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#dlmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#dlmc").val($thistr.find('td:eq(1)').html());
            return;
        }
    });
}

function sfxmList() {
    ///收费项目
    //$("#sfxmmc").newtouchFloatingSelector({
    //    height: 200,
    //    width: 300,
    //    filter: function (keyword) {
    //        if (!keyword) {
    //            return null;
    //        }
    //        //遍历数据源，用keyword来筛选出结果
    //        var resultObjArr = new Array();
    //        $.each(top.window.clients.sysChargeItemList, function (idx, val) {
    //            if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
    //                resultObjArr.push(val);
    //            }
    //        });
    //        return resultObjArr;
    //    },
    //    caption: "收费项目",
    //    colModel: [
    //        { label: '编号', name: 'sfxmbh', widthratio: 25 },
    //        { label: '代码', name: 'sfxm', widthratio: 25 },
    //        { label: '名称', name: 'sfxmmc', widthratio: 25 },
    //        { label: '拼音', name: 'py', widthratio: 25 }
    //    ],
    //    itemdbclickhandler: function ($thistr) {
    //        $("#sfxmmc").attr("data-label", $thistr.find("td:eq(1)").html());
    //        $("#sfxmmc").val($thistr.find('td:eq(2)').html());
    //        return;
    //    }
    //});

    $("#sfxmmc").newtouchFloatingSelector({
        height: 200,
        width: 400,
        url: "/SystemManage/SysPatiChargeLogic/GetSFXMItemInfoByDlCode",
        ajaxmethod: "POST",
        ajaxreqdata: function () {
            var reqData = {};
            reqData.keyword = $.trim($("#sfxm").val());
            reqData.dlCode = $.trim($("#dlmc").attr("data-label"));
            return reqData;
        },
        caption: "收费项目",
        colModel: [
            { label: "项目与否", name: "xmtype", widthratio: 25, hidden: true },
            { label: "编码", name: "py", widthratio: 25 },
            { label: "通用名", name: "xmmc", widthratio: 25 },
            { label: "商品名", name: "spm", widthratio: 25, hidden: true },
            { label: "单位", name: "dw", widthratio: 25, hidden: true },
            { label: "单价", name: "dj", widthratio: 25, hidden: true },
            { label: "规格", name: "ypgg", widthratio: 25, hidden: true },
            { label: "医保代码", name: "ybdm", widthratio: 25 },
            { label: "包装代码", name: "ypbzdm", widthratio: 25, hidden: true },
            { label: "xmbh", name: "xmbh", widthratio: 25, hidden: true },
            { label: "sfxm", name: "sfxm", widthratio: 25, hidden: true },
            { label: "dl", name: "dl", widthratio: 25, hidden: true },
            { label: "zfbl", name: "zfbl", widthratio: 25, hidden: true },
            { label: "zfxz", name: "zfxz", widthratio: 25, hidden: true },
            { label: "ybdm", name: "ybdm", widthratio: 25, hidden: true },
            { label: "yfdm", name: "yfdm", widthratio: 25, hidden: true }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#sfxmmc").attr("data-label", $thistr.find("td:eq(10)").html());
            $("#sfxmmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function submitForm() {
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data["sfxm"] = $("#sfxmmc").attr("data-label");
        data["brxz"] = $("#brxzmc").attr("data-label");
        data["dl"] = $("#dlmc").attr("data-label");
        $.submitForm({
            url: "/SystemManage/SysPatiChargeLogic/SubmitForm?keyValue=" + keyValue,
            param: data,
            success: function () {
                $.currentWindow().$("#gridList").resetSelection();
                $.currentWindow().$("#gridList").trigger("reloadGrid");
                //$.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").resetSelection();
                //$.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
                $.loading(false);
            }
        });
    }
}

function checkNotNull() {
    var validator = $("#form1").validate();
    validator.settings = {
        rules: {
            syfw: { required: true },
            sfjb: { required: true },
            fyfw: { required: true },
            zfbl: { required: true },
            zfxz: { required: true },
            fysx: { required: true }
        },
        messages: {
            syfw: { required: "适用范围必须填写" },
            sfjb: { required: "算法级别必须填写" },
            zfbl: { required: "自付比例必须填写" },
            zfxz: { required: "自付性质必须填写" },
            fysx: { required: "费用上限必须填写" }
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
    //        $("#zt").val();
    //    }
    //});
    //if (!zt) {
    //    $.modalAlert("请选择是否有效！", "warning");
    //    return false;
    //}
    return true;
}