$(function () {

    //浏览器窗口调整大小时重新加载jqGrid的宽
    $(window).resize(function () {
        initLayout("MyTabGrid");
    });

    //页面加载，禁用控件
    DisableSysBasicInfo(false);
    //页面进来，加载门诊类型
    $("#sel_mzlx").select2({ minimumResultsForSearch: -1 }); //去掉筛选条件
    initFPH();
    initLastFPHInfo();
    initZffs();

    //回车键
    $("#txtkh").keypress(function (e) {
        if (e.keyCode === 13) {
            LoadPatientInfo($("#txtkh").val(), true);
            if ($("#linkregistering").parent()[0].className === "active") {
                $("#linkregistering").trigger("click");
            }
            if ($("#linkclosedReg").parent()[0].className == "active") {
                $("#linkclosedReg").trigger("click");
            }
        }
    });
    $("#txtssk").keypress(function (e) {
        //先清空
        $("#labzl").text('');
        if (e.keyCode == 13) {
            if ($("#txtssk").val() == "" || isNaN($("#txtssk").val())) {
                $.modalAlert("请填写实收款并且只可填数字", 'warning');
                return false;
            }
            var zl = parseFloat($("#txtssk").val()) - parseFloat($("#labysk").text());
            $("#labzl").text(zl.toFixed(2)); //保留小数点后两位小数
        }
    });
});

//搜索
$("#btn_search").click(function () {
    LoadPatientInfo($("#txtkh").val(), true);
    if ($("#linkregistering").parent()[0].className == "active") {
        $("#linkregistering").trigger("click");
    }
    if ($("#linkclosedReg").parent()[0].className == "active") {
        $("#linkclosedReg").trigger("click");
    }
})

//刷卡
function SK() {
    $.modalAlert("功能正在开发中", 'warning');
    return false;
}

//页面进来，加载发票号
function initFPH() {
    $.najax({
        url: "/OutpatientManage/OutpatientReg/GetInvoice?r=" + Math.random(),
        dataType: "text",
        async: false,
        success: function (data) {
            $("#txtfph").val(data);
        }
    });
}

//页面进来，加载上一张发票信息
function initLastFPHInfo() {
    $.najax({
        url: "/OutpatientManage/OutpatientReg/GetLastSettleInfo?r=" + Math.random(),
        dataType: "json",
        async: false,
        success: function (data) {
            //查询ok
            if (data != null && data.length > 0) {
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
var kh = "";
function LoadPatientInfo(kh, isAutoOpenFreeCardReg) {
    if (!kh) {
        return;
    }
    $("#txtkh").val(kh.toUpperCase());
    $.najax({
        url: "/OutpatientManage/OutpatientReg/LoadPatientInfo?kh=" + kh + "&r=" + Math.random(),
        dataType: "json",
        async: true,
        alertbierror: false,
        success: function (data) {
            $("#labxm").text(data.data.xm);
            $("#labxb").text($.getGender(data.data.xb));
            $("#txtfzbz").val(data.data.fzbz);
            if (data.data.fzbz == 0) {
                $("#txtcfz").val("[0]初诊");
            } else {
                $("#txtcfz").val("[1]复诊");
            }
            $("#txtpatid").val(data.data.patid);
            $("#txtcsny").val(data.data.csny);
            $("#txtdb").val(data.data.db);
            $("#txtdbzd").val(data.data.dbzd);
            $("#txtblh").val(data.data.blh);
            $("#txtbrxz").val(data.data.brxz);
            $("#txtbrxzqc").val("[" + data.data.brxz + "]" + data.data.brxzmc);
            $("#txtbrxzbh").val(data.data.brxzbh);
            DisableSysBasicInfo(true); //true:标志是从免卡登记或根据卡号查询返回明细, false:页面加载时，可以输kh
        },
        errorCallback: function (data) {
            newtouch_globalevent_f4();
            $("#txtkh").val(kh.toUpperCase());
            if (isAutoOpenFreeCardReg === true) {
                FreeCardReg();
            }
            else {
                $.newtouchAlert(data);
            }
            return;
        }

    });
}

//弹出发票号的窗口
function ShowInvoicePanel() {
    $.modalOpen({
        id: "InvoiceNoPanel",
        title: "选发票号",
        url: "/OutpatientManage/OutpatientReg/ChooseInvoice",
        width: "300px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].checkFPH();//窗口点确定的回调函数
        }
    });
}

//病人性质浮层
$("#txtbrxzqc").newtouchFloatingSelector({
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
        checkBrxz($thistr.attr('data-brxz'));
        $("#txtbrxzqc").val("[" + $thistr.attr('data-brxz') + "]" + $thistr.attr('data-brxzmc'));
        $("#txtbrxz").val($thistr.attr('data-brxz'));
        $("#txtbrxzbh").val($thistr.attr('data-brxzbh'));
        return;
    }
});

//check病人性质
function checkBrxz(updateBrxz) {
    var kh = $("#txtkh").val();
    $.najax({
        url: "/OutpatientManage/OutpatientReg/CheckBrxz?kh=" + kh + "&updateBrxz=" + updateBrxz + "&r=" + Math.random(),
        dataType: "json",
        async: false,
        success: function (data) {

        },
        errorCallback: function (data) {   //状态为error时自动弹出错误信息，该方法为弹出错误信息之后需做的事情

        }
    })
}

//门诊类型发生改变清空科室/医生
function sel_mzlxChange() {
    $("#txtksys").val('');
}

//挂号类型
var currentghlx;
//挂号排班浮层
$("#txtksys").newtouchBatchFloatingSelector({
    height: 200,
    width: 350,
    clickautotrigger: true,
    id: 'regSchedule',
    url: '/OutpatientManage/OutpatientReg/GetRegScheduleList',
    ajaxmethod: 'POST',
    ajaxreqdata: function () {
        var reqData = {};
        reqData.keyword = $("#txtksys").val();
        reqData.mzlx = $("#sel_mzlx").val();
        return reqData;
    },
    caption: "挂号排班",
    colModel: [
        { label: 'gh', name: 'gh', hidden: true, widthratio: 0 },
        { label: 'ghlx', name: 'ghlx', hidden: true, widthratio: 0 },
        { label: 'ghlxbh', name: 'ghlxbh', hidden: true, widthratio: 0 },
        { label: '挂号类型', name: 'sfxmmc', widthratio: 25 },
        { label: '挂号科室', name: 'ksmc', widthratio: 25 },
        { label: '医生', name: 'rymc', widthratio: 25 },
        {
            label: '专病', name: 'ghzbmc', widthratio: 25, formatter: function (cellvalue) {
                return cellvalue === "null" ? "" : cellvalue;
            }
        },
        { label: 'ks', name: 'ks', hidden: true, widthratio: 0 },
        { label: 'ksbh', name: 'ksbh', hidden: true, widthratio: 0 }
    ],
    itemdbclickhandler: function ($thistr) {
        if ($("#sel_mzlx").val() === "1" || $("#sel_mzlx").val() === "2")//门诊/急诊  只显示科室
        {
            $("#txtksys").val($thistr.attr('data-ksmc'));
        }
        if ($("#sel_mzlx").val() === "3")//专家门诊  显示科室和医生
        {
            $("#txtksys").val($thistr.attr('data-ksmc') + "/" + $thistr.attr('data-rymc'));
        }
        $("#txtghlx").val($thistr.attr('data-ghlx'));
        $("#txtghlxbh").val($thistr.attr('data-ghlxbh'));
        $("#txtks").val($thistr.attr('data-ks'));
        $("#txtksbh").val($thistr.attr('data-ksbh'));
        $("#txtrygh").val($thistr.attr('data-gh'));
        currentghlx = $thistr.attr('data-ghlx');
        RegisterInfo();
        return;
    }
});

//加载正在挂号信息 
function RegisterInfo() {
    //var rowIDs = $("#registeringList").getDataIDs();
    //for (var i = 0; i < rowIDs.length; i++) {
    //    var getRow = $("#registeringList").getRowData(rowIDs[i]);
    //    if ($("#txtksys").val() == getRow.ghlx) {
    //        $.modalAlert("同个号不能挂多次", 'warning');
    //        $("#txtksys").val("");
    //        return;
    //    }

    //}
    $('#registeringList').jqGrid("clearGridData");
    if ($("#txtksys").val() != null && $("#ksys").val() !== "") {

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
        //$("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        $("#labysk").text(list.totalfees);  //应收款
        $("#labysk").addClass('moneybg');
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
    feesList = new Object();
    $.najax({
        url: "/OutpatientManage/OutpatientReg/GetOutpatientFees",
        dataType: "json",
        data: { ghlx: currentghlx, isZzjm: isZzjm, isCkf: isCkf, isGbf: isGbf },
        type: "POST",
        async: false,
        success: function (data) {
            feesList.ghf = data.ghfPrice;
            feesList.zlf = data.zlfPrice;
            if (!isCkf) {
                feesList.ckf = " ";
            } else {
                feesList.ckf = data.ckfPrice;
            }
            if (!isGbf) {
                feesList.gbf = " ";
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
        //$("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        $("#labysk").text(feesList.totalfees);

    }
    return feesList;
}


//退号
function BackNum() {
    var currtab = $("#linkclosedReg").parent();
    if (currtab[0].className === "active") {
        if ($("#closedRegList").getDataIDs().length == 0) {
            $.modalAlert("没有可退号的挂号信息", 'warning');
            return;
        }
        var seleRowid = jQuery("#closedRegList").jqGrid("getGridParam", "selrow");
        if (!(seleRowid)) {
            $.modalAlert("请选中一条需要退号的挂号信息", 'warning');
            return;
        }
        var seleRowData = $("#closedRegList").jqGrid('getRowData', seleRowid);
        if (seleRowData.isth === "已退号") {
            $.modalAlert("该挂号已退", 'warning');
            return;
        }
        seleRowData.isReturnCKGBFee = (seleRowData.isReturnCKGBFee === 'Yes');
        $.modalConfirm("是否退号？", function (flag) {
            if (flag) {
                $.najax({
                    url: "/OutpatientManage/OutpatientReg/BackNum",
                    dataType: "json",
                    data: {
                        patid: $("#txtpatid").val(), ghnm: seleRowData.ghnm, isReturnCKGBFee: seleRowData.isReturnCKGBFee
                    },
                    type: "POST",
                    async: false,
                    alertbierror: false,
                    success: function (data) {
                        $.loading(false);
                        if (data === true) {
                            $.modalAlert("退号成功", 'success');
                            $("#linkclosedReg").trigger("click");
                        }
                        else {
                            $.newtouchAlert(data);
                        }
                    }
                });
            }
            else {
                return false;
            }
        }, false); //false:modalConfirm不关闭dialog
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
    if (currtab[0].className === "active") {
        //获取jqGrid的行数
        var count = $("#registeringList").getGridParam('records');
        if (count > 0) {
            var regList = new Object();
            regList.fph = $("#txtfph").val();
            regList.patid = $("#txtpatid").val();
            regList.kh = $("#txtkh").val();
            regList.brxz = $("#txtbrxz").val();
            regList.ghlx = $("#txtghlx").val();
            var ysk = 0;
            ysk = $("#labysk").text();
            regList.ssk = $("#txtssk").val();
            //数据验证
            if (!CheckItem(regList)) {
                return false;
            }
            if (parseFloat(regList.ssk) < parseFloat(ysk)) {
                $.modalAlert("实收款必须不能小于应收款", 'warning');
                return false;
            } else {
                var zl = parseFloat($("#txtssk").val()) - parseFloat($("#labysk").text());
                //Book(ysk, regList.ssk);
                //结算窗体
                showBillSuccessDialog(ysk, regList.ssk, 0, zl.toFixed(2));
            }

        } else {
            $.modalAlert("当前没有正在挂号信息", 'warning');
            return false;
        }
    } else {
        $.modalAlert("请到正在挂号列表进行结算", 'warning');
    }
}

//下暂存单
function Book(ysk, ssk) {
    var selRowRowId = $("#registeringList").jqGrid("getGridParam", "selrow");
    if (!selRowRowId && $("#registeringList").getGridParam('records') === 1) {
        selRowRowId = 1;
    }

    if (!selRowRowId) {
        $.modalAlert("请选中挂号记录", 'warning');
    }

    var selRow = $("#registeringList").jqGrid('getRowData', selRowRowId);

    var jdata = {
        patid: $("#txtpatid").val(),
        brxz: $("#txtbrxz").val(),
        brxzbh: $("#txtbrxzbh").val(),
        ghly: "0",
        mjzbz: $.currentWindow().$("#sel_mzlx").val(),////门急诊标志 1：门诊 2：急诊
        ksbh: $("#txtksbh").val(),
        ks: $("#txtks").val(),
        ys: $("#txtrygh").val(),
        ghlxbh: $("#txtghlxbh").val(),
        ghlx: $("#txtghlx").val(),
        ghzbbh: 0,
        ghzb: 0,
        fzbz: $("#txtfzbz").val(),
        jzbz: "1",
        ghzt: "1",
        ghxz: 0,
        dbxm: $("#txtdb").val(),
        gsrdh: "",
        zdicd10: "",
        zdmc: "",
        ghf: selRow.ghf,
        zlf: selRow.zlf,
        ckf: selRow.ckf,
        gbf: selRow.gbf,
        zzjm: $("#chk_zzjm").is(":checked") ? true : false,
        kh: $("#txtkh").val(),
        ssk: ssk,
        fph: $("#txtfph").val(),
        isCkf: $("#chk_ckf").is(":checked") ? true : false,
        isGbf: $("#chk_gbf").is(":checked") ? true : false,
        xm: $("#labxm").text(),
        xb: $.getGenderCode($("#labxb").text()),
        csny: $("#txtcsny").val(),
        blh: $("#txtblh").val(),
        updateBrxz: $("#txtbrxz").val()
    };
    $.loading(true, "正在请求数据...");
    //保存
    $.najax({
        url: "/OutpatientManage/OutpatientReg/Book",
        dataType: "text",
        data: jdata,
        type: "POST",
        async: false,
        success: function (data) {
            $.loading(false, "正在请求数据...");
            $("#hidOrderId").val(parseInt(data));
        },
        errorCallback: function (data) {
            $.loading(false, "正在请求数据...");
        },
        complete: function () {
            $.loading(false, "正在请求数据...");
        }
    });
}

//弹出金额确认窗口
function showBillSuccessDialog(ysk, ssk, srce, zl) {
    var url = "/HospitalizationManage/Settlement/SettSuccessDialog?yingshoukuan=" + ysk
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
    var isZzjm = "";
    if ($("#chk_zzjm").is(":checked")) {
        isZzjm = true;
    } else {
        isZzjm = false;
    }
    var isCkf = false;
    if ($("#chk_ckf").is(":checked")) {
        isCkf = true;
    }
    var isGbf = false;
    if ($("#chk_gbf").is(":checked")) {
        isGbf = true;
    }

    var selRowRowId = $("#registeringList").jqGrid("getGridParam", "selrow");
    if (!selRowRowId && $("#registeringList").getGridParam('records') == 1) {
        selRowRowId = 1;
    }

    if (!selRowRowId) {
        $.modalAlert("请选中挂号记录", 'warning');
    }

    var selRow = $("#registeringList").jqGrid('getRowData', selRowRowId);

    var ghf = selRow.ghf;
    var zlf = selRow.zlf;
    var ckf = selRow.ckf;
    var gbf = selRow.gbf;
    var xb = $.getGender($("#labxb").text());
    var orderId = 0;
    if ($("#hidOrderId").val() !== "") {
        orderId = parseInt($("#hidOrderId").val());
    }

    //保存
    $.najax({
        url: "/OutpatientManage/OutpatientReg/Save",
        dataType: "json",
        //ghxz:0 普通挂号； jzbz:"1",ghzt:"1" 无效字段； ghzbbh:0,ghzb:0 暂且默认为0；ghly: "0" 挂号来源为窗口；  gsrdh:"",zdicd10:"",zdmc:"" 暂且默认空
        data: { OrderId: orderId, patid: $("#txtpatid").val(), brxz: $("#txtbrxz").val(), brxzbh: $("#txtbrxzbh").val(), ghly: "0", mjzbz: mzlx, ksbh: $("#txtksbh").val(), ks: $("#txtks").val(), ys: $("#txtrygh").val(), ghlxbh: $("#txtghlxbh").val(), ghlx: $("#txtghlx").val(), ghzbbh: 0, ghzb: 0, fzbz: $("#txtfzbz").val(), jzbz: "1", ghzt: "1", ghxz: 0, dbxm: $("#txtdb").val(), gsrdh: "", zdicd10: "", zdmc: "", ghf: ghf, zlf: zlf, ckf: ckf, gbf: gbf, zzjm: isZzjm, kh: $("#txtkh").val(), ssk: ssk, fph: $("#txtfph").val(), isCkf: isCkf, isGbf: isGbf, xm: $("#labxm").text(), xb: xb, csny: $("#txtcsny").val(), blh: $("#txtblh").val(), updateBrxz: $("#txtbrxz").val() },
        type: "POST",
        async: false,
        success: function (data) {
            $.modalAlert("保存成功", 'success');

            //重新加载发票号
            initFPH();
            newtouch_globalevent_f4();
            $("#labysk").text = String.Empty;
            $("#labzl").text = String.Empty;
            //刷新上一次结算信息
            $("#lastSettleInfoList").jqGrid('setGridParam', {}).trigger('reloadGrid');
            $.loading(false);
        },
        errorCallback: function (data) {
            $.loading(false);
            $("#labysk").text = String.Empty;
            $("#labzl").text = String.Empty;
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
    if (RegList.ssk == "" || isNaN(RegList.ssk)) {
        $.modalAlert("请填写实收款并且只可填数字", 'warning');
        return false;
    }

    return true;
}

//快捷键：清除
function newtouch_event_f4() {
    //清空Grid
    $('#registeringList').jqGrid("clearGridData");
    $('#closedRegList').jqGrid("clearGridData");
    $('#labxm').text("");
    $('#labxb').text("");
    //$("input[name=radio_xb]").removeAttr("checked");
    //$("input[name=radio_xb]").parent().removeClass('active').css('background-color', '#fff');

    //启用控件
    EnabledSysBasicInfo();
}
//快捷键:保存
function newtouch_event_f8() {
    Save();
}
//快捷键：退号
function newtouch_event_f9() {
    BackNum();
}

//禁用
function DisableSysBasicInfo(flag) {
    //true:标志是从免卡登记或根据卡号查询返回明细, false:页面加载时，可以输kh
    if (flag) {
        $("#txtkh").attr("disabled", "disabled").css("background-color", "#f1f4f6");
        $("#txtbrxzqc").removeAttr("disabled").removeAttr("style");
        $('input[name="xb"]').each(
            function () {
                $(this).parent().attr("disabled", "disabled");
            }
        );
    } else {
        $("#txtbrxzqc").attr("disabled", "disabled").css("background-color", "#f1f4f6");
    }
    //$("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
    //$("#txtzl").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
}

//启用
function EnabledSysBasicInfo() {
    $("#txtkh").removeAttr("disabled").removeAttr("style");
}






