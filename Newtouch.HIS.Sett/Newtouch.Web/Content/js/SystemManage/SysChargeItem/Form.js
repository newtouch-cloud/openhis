
var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    //if (!!keyValue) {
    $.ajax({
        url: "/SystemManage/SysChargeItem/GetFormJson?r=" + Math.random(),
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            if (data["dlmc"]) {
                $("#dlmc").attr("data-label", data["dl"]);
            }
            if (data["flmc"]) {
                $("#flmc").attr("data-label", data["fl"]);
            }
            if (data["nbdlmc"]) {
                $("#nbdlmc").attr("data-label", data["nbdl"]);
            }
            if (data["badlmc"]) {
                $("#badlmc").attr("data-label", data["badl"]);
            }
        }
    });
    //}
    $("#sfxmmc").blur(function () {
        //$("#py").val($(this).toPinyin());
        $('#py').val($(this).toPinyin());
    });
})
function InitControl() {
    dlList();//大类
    flList();//分类 
    ybdmList(); //医保代码
    nbList();//农保大类
    baList();//病案大类
}
function dlList() {
    $("#dlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysMajorClassList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "大类",
        colModel: [
            { label: '编号', name: 'dlbh', widthratio: 25 },
            { label: '代码', name: 'dl', widthratio: 25 },
            { label: '名称', name: 'dlmc', widthratio: 25 },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#dlmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#dlmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function flList() {
    $("#flmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysChargeClassifyList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "分类",
        colModel: [
            { label: '编号', name: 'flbh', widthratio: 25 },
            { label: '代码', name: 'fl', widthratio: 25 },
            { label: '名称', name: 'flmc', widthratio: 25 },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#flmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#flmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function nbList() {
    $("#nbdlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysnbsfdlList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "大类",
        colModel: [
            { label: '编号', name: 'dlbh', widthratio: 25 },
            { label: '代码', name: 'dl', widthratio: 25 },
            { label: '名称', name: 'dlmc', widthratio: 25 },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#nbdlmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#nbdlmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function baList() {
    $("#badlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysbasfdlList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "大类",
        colModel: [
            { label: '编号', name: 'dlbh', widthratio: 25 },
            { label: '代码', name: 'dl', widthratio: 25 },
            { label: '名称', name: 'dlmc', widthratio: 25 },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#badlmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#badlmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function ybdmList() {
    $("#ybdm").newtouchFloatingSelector({
        height: 200,
        width: 400,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.syszlxmList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "分类",
        colModel: [
            { label: '编号', name: 'py', widthratio: 25 },
            { label: '医保代码', name: 'ybdm', widthratio: 25 },
            { label: '物价代码', name: 'wjdm', widthratio: 25 },
            { label: '名称', name: 'xmmc', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#ybdm").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#ybdm").val($thistr.find('td:eq(1)').html());
            $("#wjdm").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function submitForm() {
    if (checkNotNull()) {
        var data = $("#form1").formSerialize();
        data["dl"] = $("#dlmc").attr("data-label");
        data["fl"] = $("#flmc").attr("data-label");
        data["badl"] = $("#badlmc").attr("data-label");
        data["nbdl"] = $("#nbdlmc").attr("data-label");
        $.submitForm({
            url: "/SystemManage/SysChargeItem/SubmitForm?keyValue=" + keyValue,
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
            sfxmmc: { required: true },
            py: { required: true },
            dw: { required: true },
            dj: { required: true },
            zfbl: { required: true },
            zfxz: { required: true },
            mzzybz: { required: true },
            ssbz: { required: true },
            tsbz: { required: true },
            jsbz: { required: true },
            sfbz: { required: true },
            zt: { required: true },
            dl: { required: true },
        },
        messages: {
            sfxmmc: { required: "收费项目名称必须填写" },
            py: { required: "拼音必须填写" },
            dw: { required: "单位必须填写" },
            dj: { required: "单价必须填写" },
            zfbl: { required: "自负比例必须填写" },
            zfxz: { required: "自负性质必须选择" },
            mzzybz: { required: "范围必须选择" },
            ssbz: { required: "实施标志必须选择" },
            tsbz: { required: "特殊标志必须选择" },
            jsbz: { required: "警示标志必须选择" },
            sfbz: { required: "收费标志必须选择" },
            zt: { required: "状态必须选择" },
            dl: { required: "大类必须选择" },
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
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