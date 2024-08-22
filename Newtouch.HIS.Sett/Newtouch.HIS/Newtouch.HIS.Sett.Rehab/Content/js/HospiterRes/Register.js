var keyValue = $.request("keyValue");
$(function () {
    if (!!keyValue) {
        $.ajax({
            url: "/PatientManage/HospiterRes/GetFormJson?T=" + new Date(),
            data: { "keyValue": keyValue },
            dataType: 'json',
            async: false,
            success: function (rep) {
                $("#form1").formSerialize(rep);
                if (rep["jkjl"]) {
                    $("#jkjlmc").attr("data-label", rep["jkjl"]);
                }
                if (rep["doctor"]) {
                    $("#doctormc").attr("data-label", rep["doctor"]);
                }
                if (rep["brxz"]) {
                    $("#brxzmc").attr("data-label", rep["brxz"]);
                }
                if (rep["ryzd1"]) {
                    $("#zdmc1").attr("data-label", rep["ryzd1"]);
                }
                if (rep["ryzd2"]) {
                    $("#zdmc2").attr("data-label", rep["ryzd2"]);
                }
                if (rep["ryzd3"]) {
                    $("#zdmc3").attr("data-label", rep["ryzd3"]);
                }
                if (rep["ks"]) {
                    $("#ksmc").attr("data-label", rep["ks"]);
                }
                if (rep["bq"]) {
                    $("#bqmc").attr("data-label", rep["bq"]);
                }
                if (rep["mz"]) {
                    $("#mzmc").attr("data-label", rep["mz"]);
                }
                if (rep["gj"]) {
                    $("#gjmc").attr("data-label", rep["gj"]);
                }
                if (rep["zjlx"]) {
                    var type = rep["zjlx"].trim();
                    switch (type) {
                        case "0": type = "身份证"; break;
                        case "1": type = "护照"; break;
                        case "2": type = "军官证"; break;
                        default: break;

                    }
                    $("#zjlx").html(type + "<span>*</span>");
                }
                $(".toolbar").hide();
                $("#noCardRes").attr("disabled", "disabled");
            }
        });
    }
    $("#ryrq").val($.getDate());
    $("#bje").val("");

    $("#kh").keydown(function (e) {
        if (e.keyCode == 13) {
            $.ajax({
                url: "/PatientManage/HospiterRes/CheckCardState",
                data: { "kh": $("#kh").val() },
                dataType: 'json',
                async: false,
                success: function (rep) {
                    if (rep == "0") {
                        //无卡做免卡登记 
                        btn_NocardRes();
                    } else {
                        //有卡显示基本信息
                        $("#form1").formSerialize(rep);
                        if (rep["jkjl"]) {
                            $("#jkjlmc").attr("data-label", rep["jkjl"]);
                        }
                        if (rep["doctor"]) {
                            $("#doctormc").attr("data-label", rep["doctor"]);
                        }
                        if (rep["brxz"]) {
                            $("#brxzmc").attr("data-label", rep["brxz"]);
                        }
                        if (rep["ryzd1"]) {
                            $("#zdmc1").attr("data-label", rep["ryzd1"]);
                        }
                        if (rep["ryzd2"]) {
                            $("#zdmc2").attr("data-label", rep["ryzd2"]);
                        }
                        if (rep["ryzd3"]) {
                            $("#zdmc3").attr("data-label", rep["ryzd3"]);
                        }
                        if (rep["ks"]) {
                            $("#ksmc").attr("data-label", rep["ks"]);
                        }
                        if (rep["bq"]) {
                            $("#bqmc").attr("data-label", rep["bq"]);
                        }
                        if (rep["mz"]) {
                            $("#mzmc").attr("data-label", rep["mz"]);
                        }
                        if (rep["gj"]) {
                            $("#gjmc").attr("data-label", rep["gj"]);
                        }
                        if (rep["zjlx"]) {
                            var type = rep["zjlx"].trim();
                            switch (type) {
                                case "0": type = "身份证"; break;
                                case "1": type = "护照"; break;
                                case "2": type = "军官证"; break;
                                default: break;

                            }
                            $("#zjlx").html(type + "<span>*</span>");
                        }
                        //禁用系统病人基本信息
                        DisableSysBasicInfo();
                    }
                }
            });
        }
    });
    initControl();
});

function btn_NocardRes() {
    $.modalOpen({
        id: "Form",
        title: "患者登记",
        url: "/Patient/AddPatient",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function initControl() {
    ///健康教练
    $("#jkjlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysStaffList, function (idx, val) {
                if (((val.py && val.py.indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "")
                    && val.ryCode == "1") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "健康教练",
        colModel: [
            {
                label: '代码', name: 'ry', widthratio: 25
            },
            {
                label: '名称', name: 'rymc', widthratio: 25
            },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#jkjlmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#jkjlmc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
    ///医生
    $("#doctormc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();

            $.each(top.window.newtouchclients.sysStaffList, function (idx, val) {
                if (((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "")
                     && val.ryCode == "2") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "医生",
        colModel: [
            {
                label: '代码', name: 'ry', widthratio: 25
            },
            {
                label: '名称', name: 'rymc', widthratio: 25
            },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#doctormc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#doctormc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
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
            $.each(top.window.newtouchclients.sysPatientNatureList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "病人性质",
        colModel: [
            {
                label: '编号', name: 'brxzbh', widthratio: 25
            },
            {
                label: '代码', name: 'brxz', widthratio: 25
            },
            {
                label: '名称', name: 'brxzmc', widthratio: 25
            },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#brxzmc").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
            $("#brxzmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
    ///入院诊断 zdmc1
    $("#zdmc1").newtouchFloatingSelector(
        {
            height: 200,
            width: 300,
            url: "/PatientManage/HospiterRes/GetryzdSelect",
            ajaxparameters: function () {
                return "ryzd=" + $("#zdmc1").val();
            },
            caption: "入院诊断",
            colModel: [
           {
               label: '编号', name: 'zdbh', widthratio: 25
           },
           {
               label: 'icd10', name: 'icd10', widthratio: 25
           },
           {
               label: '名称', name: 'zdmc', widthratio: 25
           },
           { label: '内码', name: 'zdnm', widthratio: 25 }
            ],
            itemdbclickhandler: function (data) {
                $("#zdmc1").attr("data-label", data.find("td:eq(0)").html() + "-" + data.find("td:eq(3)").html());
                $("#zdmc1").val(data.find('td:eq(2)').html());
                return;
            }
        });
    ///入院诊断 zdmc2
    $("#zdmc2").newtouchFloatingSelector(
           {
               height: 200,
               width: 300,
               url: "/PatientManage/HospiterRes/GetryzdSelect",
               ajaxparameters: function () {
                   return "ryzd=" + $("#zdmc2").val();
               },
               caption: "入院诊断",
               colModel: [
                   {
                       label: '编号', name: 'zdbh', widthratio: 25
                   },
                   {
                       label: 'icd10', name: 'icd10', widthratio: 25
                   },
                   {
                       label: '名称', name: 'zdmc', widthratio: 25
                   },
                   { label: '内码', name: 'zdnm', widthratio: 25 }
               ],
               itemdbclickhandler: function (data) {
                   $("#zdmc2").attr("data-label", data.find("td:eq(0)").html() + "-" + data.find("td:eq(3)").html());
                   $("#zdmc2").val(data.find('td:eq(2)').html());
                   return;
               }
           });
    ///入院诊断 zdmc3
    $("#zdmc3").newtouchFloatingSelector(
              {
                  height: 200,
                  width: 300,
                  url: "/PatientManage/HospiterRes/GetryzdSelect",
                  ajaxparameters: function () {
                      return "ryzd=" + $("#zdmc3").val();
                  },
                  caption: "入院诊断",
                  colModel: [
                       {
                           label: '编号', name: 'zdbh', widthratio: 25
                       },
                       {
                           label: 'icd10', name: 'icd10', widthratio: 25
                       },
                       {
                           label: '名称', name: 'zdmc', widthratio: 25
                       },
                       { label: '内码', name: 'zdnm', widthratio: 25 }
                  ],
                  itemdbclickhandler: function (data) {
                      $("#zdmc3").attr("data-label", data.find("td:eq(0)").html() + "-" + data.find("td:eq(3)").html());
                      $("#zdmc3").val(data.find('td:eq(2)').html());
                      return;
                  }
              });
    ///科室
    $("#ksmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysDepartList, function (idx, val) {
                if ((val.py && val.py.indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "科室",
        colModel: [
            {
                label: '编号', name: 'ksbh', widthratio: 25
            },
            {
                label: '代码', name: 'ks', widthratio: 25
            },
            {
                label: '名称', name: 'ksmc', widthratio: 25
            },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#ksmc").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
            $("#ksmc").val($thistr.find('td:eq(2)').html());
            return;
        },
    });
    ///病区
    $("#bqmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysPatiAreaList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "病区",
        colModel: [
            {
                label: '编号', name: 'bqbh', widthratio: 25
            },
            {
                label: '代码', name: 'bq', widthratio: 25
            },
            {
                label: '名称', name: 'bqmc', widthratio: 25
            },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#bqmc").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
            $("#bqmc").val($thistr.find('td:eq(2)').html());
            return;
        },
    });
    ///民族
    $("#mzmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysNationList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "民族",
        colModel: [
            {
                label: '代码', name: 'mz', widthratio: 25
            },
            {
                label: '名称', name: 'mzmc', widthratio: 25
            },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#mzmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#mzmc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
    ///国籍
    $("#gjmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysNationalityList, function (idx, val) {
                if ((val.py && val.py.indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "国籍",
        colModel: [
            {
                label: '代码', name: 'gj', widthratio: 25
            },
            {
                label: '名称', name: 'gjmc', widthratio: 25
            },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#gjmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#gjmc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
}

function btn_Save() {
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data["jkjl"] = $("#jkjlmc").attr("data-label");
        data["doctor"] = $("#doctormc").attr("data-label");
        data["brxz"] = $("#brxzmc").attr("data-label");
        var zd1 = $("#zdmc1").attr("data-label");
        data["ryzd1"] = null;
        if (zd1) {
            data["ryzd1"] = (zd1.split('-', 1))[0];
        }
        var zd2 = $("#zdmc2").attr("data-label");
        data["ryzd2"] = null;
        if (zd2) {
            data["ryzd2"] = (zd2.split('-', 1))[0];
        }
        var zd3 = $("#zdmc3").attr("data-label");
        data["ryzd3"] = null;
        if (zd3) {
            data["ryzd3"] = (zd3.split('-', 1))[0];
        }
        data["ks"] = $("#ksmc").attr("data-label");
        data["bq"] = $("#bqmc").attr("data-label");
        data["mz"] = $("#mzmc").attr("data-label");
        data["gj"] = $("#gjmc").attr("data-label");
        $.submitForm({
            url: "/PatientManage/HospiterRes/SaveSysHosBasicInfo",
            loading: "正在提交数据...",
            param: data,
            successwithtipmsg: false,
            success: function (data) {
                if (data.message == "操作成功") {
                    $.modalAlert("入院登记成功！住院号为:" + data.data, 'warning');
                    ClearAll();
                }
                $.loading(false);
            }
        });
    }
}

function PrinyZYInfo() {
    if (!$("#patid").val()) {
        $.modalAlert("病人基本信息不全，无法打印住院信息", 'error');
        return;
    } else {
        $.modalAlert("功能正在开发中", 'warning');
        return;
    }
    var data = $("#form1").formSerialize();
    data["ryzd1"] = $("#zdmc1").attr("data-label");
    data["ryzd2"] = $("#zdmc2").attr("data-label");
    data["ryzd3"] = $("#zdmc3").attr("data-label");
    $.ajax({
        url: "/PatientManage/HospiterRes/PrinyZYInfo",
        data: data,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.message = "打印成功！") {
                $.modalAlert(data.message, "success");
            }
        }
    });
}

function PrintWDInfo() {

    if (!$("#patid").val()) {
        $.modalAlert("病人基本信息不全，无法打印腕带", 'error');
        return;
    } else {
        $.modalAlert("功能正在开发中", 'warning');
        return;
    }
    $.ajax({
        url: "/PatientManage/HospiterRes/PrintWDInfo",
        data: $("#form1").formSerialize(),
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.message = "打印成功！") {
                $.modalAlert(data.message, "success");
            }
        }
    });
}

function Cancel() {
    if (!$("#patid").val()) {
        $.modalAlert("病人基本信息不全，无法打印住院信息", 'error');
        return;
    }
    $.ajax({
        url: "/PatientManage/HospiterRes/CancelAdmission?zyh=" + $("#zyh").val(),
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.message == "取消入院成功！") {
                AbledSysBasicInfo();
                newtouch_globalevent_f4(event);
            }
            $.modalAlert(data.message, "success");
        }
    });
}

function DisableSysBasicInfo() {
    $("#kh").attr("disabled", "disabled");
    $("#zyh").attr("disabled", "disabled");
    $("#xm").attr("disabled", "disabled");
    $("#zjh").attr("disabled", "disabled");
    $("#csny").attr("disabled", "disabled");
    $('input[name="xb"]').each(
        function () {
            $(this).parent().attr("disabled", "disabled");
        }
        );
    $("#nl").attr("disabled", "disabled");
    $("#blh").attr("disabled", "disabled");
    $("#dh").attr("disabled", "disabled");
    $("#dy").attr("disabled", "disabled");
    $("#hf").attr("disabled", "disabled");
}

function AbledSysBasicInfo() {
    $("#kh").removeAttr("disabled");
    $("#zyh").removeAttr("disabled");
    $("#xm").removeAttr("disabled");
    $("#zjh").removeAttr("disabled");
    $("#csny").removeAttr("disabled");
    $('input[name="xb"]').each(
        function () {
            // $(this).parent().removeAttr("disabled");
            $(this).parent().removeClass("active");
        }
        );
    $("#nl").removeAttr("disabled");
    $("#blh").removeAttr("disabled");
    $("#dh").removeAttr("disabled");
    $("#dy").removeAttr("disabled");
    $("#hf").removeAttr("disabled");
}

function ClearAll() {
    AbledSysBasicInfo();
    newtouch_globalevent_f4();
}

function newtouch_globalevent_f6() {
    PrintWDInfo();
}

function newtouch_globalevent_f7() {
    PrinyZYInfo();
}

function newtouch_globalevent_f8() {
    btn_Save();
}

function newtouch_globalevent_f9() {
    Cancel();
}

function newtouch_event_f4() {
    AbledSysBasicInfo();
    $("#patid").val("");
}

function checkNotNull() {
    var patid = $("#patid").val();
    if (!patid) {
        $.modalAlert("病人基本信息不存在，无法保存", 'warning');
        return false;
    }

    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            brxzmc: { required: true },
            ksmc: { required: true },
            bqmc: { required: true },
            zy: { required: true },
            mzmc: { required: true },
            gjmc: { required: true },
            bje: { required: true },
            lxr: { required: true },
            lxrgx: { required: true },
            lxrdh: { isMobile: true, required: true },
            lxrdz: { required: true },
            kh: { required: true }
        },
        messages: {
            brxzmc: { required: "报销政策必须填写" },
            ksmc: { required: "科室必须填写" },
            bqmc: { required: "病区必须填写" },
            mzmc: { required: "民族必须填写" },
            gjmc: { required: "国籍必须填写" },
            zy: { required: "职业必须填写" },
            bje: { required: "报警额必须填写" },
            lxr: { required: "紧急联系人必须填写" },
            lxrgx: { required: "紧急联系人关系必须填写" },
            lxrdh: { isMobile: "移动电话格式不正确", required: "紧急联系人移动电话必须填写" },
            lxrdz: { required: "紧急联系人地址必须填写" },
            kh: { required: "卡号必须填写" }
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
    return true;
}
