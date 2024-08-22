$(function () {

    //浏览器窗口调整大小时重新加载jqGrid的宽
    $(window).resize(function () {
        initLayout("MyTabGrid");
    });

    //页面加载，禁用控件
    DisableSysBasicInfo(false);
    initFPH();
    initLastFPHInfo();
    initZffs();

    //回车键
    $("#txtkh").keypress(function (e) {
        if (e.keyCode == 13) {
            LoadPatientInfo()
            if ($("#linkregistering").parent()[0].className == "active") {
                $("#linkregistering").trigger("click");
            }
            if ($("#linkclosedReg").parent()[0].className == "active") {
                $("#linkclosedReg").trigger("click");
            }
        }
    });
    $("#txtssk").keypress(function (e) {
        if (e.keyCode == 13) {
            var zl = parseFloat($("#txtssk").val()) - parseFloat($("#txtysk").val());
            $("#txtzl").val(zl.toFixed(2)); //保留小数点后两位小数
            $("#txtzl").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        }
    });
});

//页面进来，加载发票号
function initFPH() {
    $.ajax({
        url: "/BusinessManage/OutPatient/GetInvoice",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#txtfph").val(data);
        }
    });
}

//页面进来，加载上一张发票信息
function initLastFPHInfo() {
    $.ajax({
        url: "/BusinessManage/OutPatient/GetLastSettleInfo",
        dataType: "json",
        async: false,
        success: function (data) {
            //查询ok
            if (data.state = "success" & data != null) {
                $("#lblqfph").val(data[0].fph);
                $("#lblzje").val(data[0].xjzf);
                $("#lblys").val(data[0].ysk);
                $("#lblzl").val(data[0].zl);
            } else //查询ok，但当天还没挂号结算信息
            {
                $("#lblqfph").val('');
                $("#lblzje").val('');
                $("#lblys").val('');
                $("#lblzl").val('');

            }
        }
    });
}

//页面进来，加载支付方式
function initZffs() {
    $("#ddlzffs").newtouchBindSelect({
        id: "xjzffsbh",
        text: "xjzffsmc",
        datasource: function () {
            //遍历数据源
            var resultObjArr = new Array();
            $.each(top.clients.SysForCashPayList, function (idx, val) {
                if (val.zhxz == 'a') {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;
            console.log(resultObjArr);
        }
    });

}

//免卡登记
function FreeCardReg() {
    $.modalOpen({
        id: "Form",
        title: "免卡登记",
        url: "/PatientManage/HospiterRes/PatientBasic",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

//根据卡号加载信息
function LoadPatientInfo() {
    var kh = $("#txtkh").val();
    $.ajax({
        url: "/BusinessManage/OutPatient/LoadPatientInfo?kh=" + kh,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.state == "error") {
                newtouch_globalevent_f4(event);
                FreeCardReg();
            } else {
                $("#txtxm").val(data.xm);
                $("#txtxb").val($.getGender(data.xb));
                $("txtfzbz").val(data.fzbz);
                if (data.fzbz == 0) {
                    $("#txtcfz").val("[0]初诊");
                } else {
                    $("#txtcfz").val("[1]复诊");
                }
                $("#txtpatid").val(data.patid);
                $("#txtcsny").val(data.csny);
                $("#txtdb").val(data.db);
                $("#txtdbzd").val(data.dbzd);
                $("#txtblh").val(data.blh);
                $("#txtbrxz").val(data.brxz);
                $("#txtbrxzqc").val("[" + data.brxz + "]" + data.brxzmc);
                $("#txtbrxzbh").val(data.brxzbh);
                DisableSysBasicInfo(true); //true:标志是从免卡登记或根据卡号查询返回明细, false:页面加载时，可以输kh

            }
        }
    });
}

//弹出发票号的窗口
function ShowInvoicePanel() {
    $.modalOpen({
        id: "InvoiceNoPanel",
        title: "选发票号",
        url: "/BusinessManage/OutPatient/ChooseInvoice",
        width: "300px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].checkFPH();//窗口点确定的回调函数
        }
    });
}

//病人类型浮层
$("#txtbrxz").newtouchFloatingSelector({
    height: 200,
    width: 300,
    filter: function (keyword) {
        if (!keyword) {
            return null;
        }
        //遍历数据源，用keyword来筛选出结果
        var resultObjArr = new Array();
        $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
            if ((val.brxzmc && val.brxzmc.indexOf(keyword) >= 0)
                || (val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0)) {
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
        $("#brxz").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
        $("#brxz").val($thistr.find('td:eq(2)').html());
        return;
    }
});

//挂号类型
var currentghlx;
//挂号排班浮层
$("#txtksys").newtouchFloatingSelector({
    height: 200,
    width: 350,
    id: 'regSchedule',
    url: '/BusinessManage/OutPatient/GetRegScheduleList',
    ajaxmethod: 'POST',
    ajaxreqdata: function () {
        var reqData = {};
        reqData.keyword = $("#txtksys").val();
        reqData.mzlx = $("#sel_mzlx").val();
        return reqData;
    },
    caption: "挂号排班",
    colModel: [
        { label: 'ghlx', name: 'ghlx', hidden: true, widthratio: 0 },
        { label: 'ghlxbh', name: 'ghlxbh', hidden: true, widthratio: 0 },
        { label: '挂号类型', name: 'sfxmmc', widthratio: 25 },
        { label: '挂号科室', name: 'ksmc', widthratio: 25 },
        { label: '医生', name: 'rymc', widthratio: 25 },
        {
            label: '专病', name: 'ghzbmc', widthratio: 25, formatter: function (cellvalue) {
                return cellvalue == "null" ? "" : cellvalue;
            }
        },
        { label: 'ks', name: 'ks', hidden: true, widthratio: 0 },
        { label: 'ksbh', name: 'ksbh', hidden: true, widthratio: 0 }
    ],
    itemdbclickhandler: function ($thistr) {
        if ($("#sel_mzlx").val() == "1" || $("#sel_mzlx").val() == "2")//门诊/急诊  只显示科室
        {
            $("#txtksys").val($thistr.find('td:eq(3)').html());
        }
        if ($("#sel_mzlx").val() == "3")//专家门诊  显示科室和医生
        {
            $("#txtksys").val($thistr.find('td:eq(3)').html() + "/" + $thistr.find('td:eq(4)').html());
        }
        $("#txtghlx").val($thistr.find('td:eq(0)').html());
        $("#txtghlxbh").val($thistr.find('td:eq(1)').html());
        $("#txtks").val($thistr.find('td:eq(6)').html());
        $("#txtksbh").val($thistr.find('td:eq(7)').html());
        currentghlx = $thistr.attr('data-ghlx');
        RegisterInfo();


        return;
    }

});

//加载正在挂号信息 
function RegisterInfo() {
    if ($("#txtksys").val() != null & $("#ksys").val() != "") {


        //把tab页中所需的数据保存到一个对象
        list = new Object();
        list.ghlx = $("#txtksys").val();
        list.brxz = $("#txtbrxz").val();
        list.brxzbh = $("#txtbrxzbh").val();
        list.dbxm = $("#txtdbxm").val();
        //获取挂号费 诊疗费 磁卡费 工本费
        var feesList = GHFees(true);
        list.ghf = feesList.ghf;
        list.zlf = feesList.zlf;
        list.ckf = feesList.ckf;
        list.gbf = feesList.gbf;
        list.totalfees = feesList.totalfees;

        //正在挂号
        registeringListNew([list]);
        $("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        $("#txtysk").val(list.totalfees);  //应收款
    }
}

//获取挂号费用
function GHFees(flag) {
    var isZzjm = false;
    var isCkf = false;
    var isGbf = false;
    if ($("#chk_zzjm").is(":checked")) {
        isZzjm = true;
    }
    if ($("#chk_ckf").is(":checked")) {
        isCkf = true;
    }
    if ($("#chk_gbf").is(":checked")) {
        isGbf = true;
    }
    //if ($("#chk_ckf").is(":checked") & $("#chk_gbf").is(":checked"))
    //{
    //    return false;
    //}
    feesList = new Object();
    $.ajax({
        url: "/BusinessManage/OutPatient/GetOutpatientFees",
        dataType: "json",
        data: { ghlx: currentghlx, isZzjm: isZzjm, isCkf: isCkf, isGbf: isGbf },
        type: "POST",
        async: false,
        success: function (data) {
            feesList.ghf = data.ghfPrice;
            feesList.zlf = data.zlfPrice;
            if (!isCkf) {
                feesList.ckf = "";
            } else {
                feesList.ckf = data.ckfPrice;
            }
            if (!isGbf) {
                feesList.gbf = "";
            } else {
                feesList.gbf = data.gbfPrice;
            }

            feesList.totalfees = data.totalfees;
        }
    });
    //单独点击CheckBox
    if (!flag) {
        //var rowData = $('#registeringList').jqGrid('getRowData', 1);
        $('#registeringList').jqGrid('setCell', 1, 'ckf', feesList.ckf);
        $('#registeringList').jqGrid('setCell', 1, 'gbf', feesList.gbf);
        $('#registeringList').jqGrid('setCell', 1, 'totalfees', feesList.totalfees);
        //更新Grid费用的同时，更新总金额
        $("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        $("#txtysk").val(feesList.totalfees);

    }
    return feesList;
}


//退号
function BackNum() {
    var currtab = $("#linkclosedReg").parent();
    if (currtab[0].className == "active") {
        var count = $("#closedRegList").getGridParam('records');
        if (count >= 1) {
            var ghnm = $("#closedRegList").jqGridRowValue().ghnm;
            if (ghnm == null) {
                $.modalAlert("请选中一条需要退号的挂号信息", 'warning');
                return;
            }
            else {
                if ($("#closedRegList").jqGridRowValue().isth == "已退号") {
                    $.modalAlert("该挂号已退", 'warning');
                    return;
                }
                var isReturnAll = false;
                if ($("#isReturnAll").is(":checked")) {
                    isReturnAll = true;
                }
                if (window.confirm("是否退号？")) {
                    var ghnm = $("#closedRegList").jqGridRowValue().ghnm;
                    $.ajax({
                        url: "/BusinessManage/OutPatient/BackNum",
                        dataType: "json",
                        data: { patid: $("#txtpatid").val(), ghnm: ghnm, isReturnAll: isReturnAll },
                        type: "POST",
                        async: false,
                        success: function (data) {
                            $.loading(false);
                            $.modalAlert("退号成功", 'success');
                            $("#linkclosedReg").trigger("click");
                        }
                    });

                } else {
                    return false;
                }
            }
        }
        else {
            $.modalAlert("没有可退号的信息", 'warning');
        }
    }
    else {
        $.modalAlert("请在已结算列表里选择要退号的项", 'warning');
        return;
    }
}

//保存
function Save() {
    //先看当前是不是在正在挂号列表，再看列表有没有数据，如果有 默认第一条，若没有则提示
    var currtab = $("#linkregistering").parent();
    if (currtab[0].className == "active") {
        //获取jqGrid的行数
        var count = $("#registeringList").getGridParam('records');
        if (count > 0) {
            RegList = new Object();
            RegList.fph = $("#txtfph").val();
            RegList.patid = $("#txtpatid").val();
            RegList.kh = $("#txtkh").val();
            RegList.brxz = $("#txtbrxz").val();
            RegList.ghlx = $("#txtghlx").val();
            //数据验证
            if (!CheckItem(RegList)) {
                return false;
            }
            //var isJS;
            var ysk = 0;
            var ssk = 0;
            ysk = $("#txtysk").val();
            ssk = $("#txtssk").val();
            zl = $("#txtzl").val();
            if (parseFloat(ssk) < parseFloat(ysk)) {
                $.modalAlert("实收款必须不能小于应收款", 'warning');
                return;
            } else {
                var zl = parseFloat($("#txtssk").val()) - parseFloat($("#txtysk").val());
                //结算窗体
                showBillSuccessDialog(ysk, ssk, 0, zl.toFixed(2));

            }

        } else {
            $.modalAlert("当前没有正在挂号信息", 'warning');
            return;
        }
    }
}
//弹出金额确认窗口
function showBillSuccessDialog(ysk, ssk, srce, zl) {
    var url = "/OutHospital/Bill/BillSuccessDialog?yingshoukuan=" + ysk
        + "&ssk=" + ssk
        + "&srce=" + srce
        + "&zhaoling=" + zl;
    $.modalOpen({
        id: "FormShowBillSuccessDialog",
        title: "结算确认",
        url: url,
        width: "320px",
        height: "260px",
        callBack: function (iframeId) {
            $.modalClose("FormShowBillSuccessDialog");   //关闭结算金额的窗体
            $.loading(true, "正在请求数据...");
            SaveMethod(ysk, ssk);
        }
    });
}
//具体保存执行
function SaveMethod(ysk, ssk) {
    var mzlx = $.currentWindow().$("#sel_mzlx").val(); //门急诊标志 1：门诊 2：急诊
    var jzxh = 0;  //就诊序号
    $.ajax({
        url: "/BusinessManage/OutPatient/GetJZXH",
        dataType: "json",
        data: { ghlx: $("#txtghlx").val(), ks: $("#txtks").val(), ys: 0, ghzb: 0 },
        type: "POST",
        async: false,
        success: function (data) {
            if (data == null || data == "") {
                $.modalAlert("获取就诊序号失败", 'warning');
                return;
            }
            else {
                jzxh = data;
            }
        }
    });
    var zzjm = "";
    if ($("#chk_zzjm").is(":checked")) {
        zzjm = "true";
    } else {
        zzjm = "false";
    }
    var ghf = ghf = $("#registeringList").jqGridRowValue().ghf;
    var zlf = zlf = $("#registeringList").jqGridRowValue().zlf;

    var ckf = 0
    if ($("#registeringList").jqGridRowValue().ckf != "") {
        ckf = $("#registeringList").jqGridRowValue().ckf;
    }
    var gbf = 0;
    if ($("#registeringList").jqGridRowValue().gbf != "") {
        gbf = $("#registeringList").jqGridRowValue().gbf;
    }
    //保存
    $.ajax({
        url: "/BusinessManage/OutPatient/Save",
        dataType: "json",
        //ghxz:0 普通挂号； jzbz:"1",ghzt:"1" 无效字段； ghzbbh:0,ghzb:0 暂且默认为0；ghly: "0" 挂号来源为窗口；  gsrdh:"",zdicd10:"",zdmc:"" 暂且默认空
        data: { patid: $("#txtpatid").val(), brxz: $("#txtbrxz").val(), brxzbh: $("#txtbrxzbh").val(), ghly: "0", mjzbz: mzlx, ksbh: $("#txtksbh").val(), ks: $("#txtks").val(), rybh: $("#txtrybh").val(), ys: 0, ghlxbh: $("#txtghlxbh").val(), ghlx: $("#txtghlx").val(), ghzbbh: 0, ghzb: 0, jzxh: jzxh, fzbz: $("txtfzbz").val(), jzbz: "1", ghzt: "1", ghxz: 0, dbxm: $("#txtdb").val(), gsrdh: "", zdicd10: "", zdmc: "", ghf: ghf, zlf: zlf, ckf: ckf, gbf: ghf, zzjm: zzjm, kh: $("#txtkh").val(), ysk: ysk, ssk: ssk, fph: $("#txtfph").val() },
        type: "POST",
        async: false,
        success: function (data) {
            if (data.state == "success") {
                $.modalAlert("保存成功", 'success');

                //重新加载发票号
                initFPH();
                Clear();
                $.loading(false);
            }
            else if (data.state == "error") {
                $.modalAlert("保存失败", 'error');
                $.loading(false);
                return;
            }
        }
    });

}

//保存验证
function CheckItem(RegList) {
    if (RegList.fph == "") {
        $.modalAlert("发票号不可为空", 'warning');
        return;
    }
    if (RegList.patid == "") {
        $.modalAlert("请先新增病人信息", 'warning');
        return false;
    }
    if (RegList.kh == "") {
        $.modalAlert("请先输入卡号", 'warning');
        return false;
    }
    if (RegList.brxz == "") {
        $.modalAlert("请选择病人性质", 'warning');
        return false;
    }
    if (RegList.ghlx == "") {
        $.modalAlert("请选择挂号类型", 'warning');
        return false;
    }
    return true;
}
//清除
function Clear() {

    //清空Grid
    $('#registeringList').jqGrid("clearGridData");
    $('#notSettledRegList').jqGrid("clearGridData");
    $('#closedRegList').jqGrid("clearGridData");

    //清空控件
    newtouch_globalevent_f4(event);

    //启用控件
    EnabledSysBasicInfo();
}

//禁用
function DisableSysBasicInfo(flag) {
    //true:标志是从免卡登记或根据卡号查询返回明细, false:页面加载时，可以输kh
    if (flag) {
        $("#txtkh").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
    } else {
        $("#txtbrxzqc").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238");
    }
}

//启用
function EnabledSysBasicInfo() {
    $("#txtkh").removeAttr("disabled").removeAttr("style");;
}


//作废 需求变更：去掉未结算内容 update by caishanshan 20161229
//function Abandon() {
//    var currtab = $("#linknotSettledReg").parent();
//    if (currtab[0].className == "active") {
//        var count = $("#notSettledRegList").getGridParam('records');
//        if (count >= 1) {
//            var ghnm = $("#notSettledRegList").jqGridRowValue().ghnm;
//            if (ghnm == null) {
//                $.modalAlert("请选中一条需要作废的挂号信息", 'warning');
//                return;
//            }
//            else
//            {
//                if (window.confirm("确定要作废吗？")) {
//                    $.ajax({
//                        url: "/BusinessManage/OutPatient/SaveRegAbandRecord",
//                        dataType: "json",
//                        data: { ghnm: ghnm },
//                        type: "POST",
//                        async: false,
//                        success: function (data) {
//                            if (data.state == "success") {
//                                $.loading(false);
//                                $.modalAlert("作废成功", 'success');
//                                $("#linknotSettledReg").trigger("click");
//                            } else {
//                                $.modalAlert("失败", 'error');
//                                return;
//                            }
//                        }
//                    });
//                }
//                else
//                {
//                    return false;
//                }
//            }
//        }
//        else
//        {
//            $.modalAlert("没有可作废的信息", 'warning');
//            return;
//        }
//    }
//    else
//    {
//        $.modalAlert("请在未结算列表里选择要作废的项", 'warning');
//        return;
//    }
//}




