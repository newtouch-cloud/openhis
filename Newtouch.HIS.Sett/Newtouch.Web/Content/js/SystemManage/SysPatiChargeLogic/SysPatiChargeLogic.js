$(function () {
    gridList();
});
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemManage/SysPatiChargeLogic/GetsfGridJson",
        ExpandColumn: "orgId",
        postData: { keyword: $("#txt_keyword").val(), ogrId: $("#OrganizeId").val() },
        height: $(window).height() - 128,
        colModel: [
            { label: "主键", name: "brsfsfbh", hidden: true, key: true },
            { label: "病人性质", name: "brxzmc", width: 80, align: 'left' },
            { label: '大类', name: "dlmc", width: 100, align: 'left' },
            { label: '收费项目', name: "sfxmmc", width: 100, align: 'left' },
            {
                label: '适用范围', name: "mzzybz", width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue === "0") {
                            return "通用";
                        } else if (cellvalue === "1") {
                            return "门诊";
                        } else if (cellvalue === "2") {
                            return "部门";
                        } else if (cellvalue === "3") {
                            return "住院";
                        } else if (cellvalue === "4") {
                            return "系统";
                        } else {
                            return "";
                        }
                    }
                    return "";
                }
            },
            {
                label: '算法级别', name: 'sfjb', width: 80, align: 'left'
            },
            {
                label: "费用范围", name: 'fyfw', width: 130, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == "0") {
                            return "可报";
                        } else if (cellvalue == "1") {
                            return "可报+分类自负";
                        } else if (cellvalue == "2") {
                            return "可报+分类自负+自费";
                        } else if (cellvalue == "3") {
                            return "可报+分类自负+自费+绝对自费";
                        }
                    }
                    return "";
                }
            },
            { label: '自负比例', name: 'zfbl', width: 80, align: 'left' },
            {
                label: '自负性质', name: 'zfxz', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == "0") {
                            return "自付";
                        } else if (cellvalue == "1") {
                            return "自理";
                        }
                    }
                    return "";
                }
            },
            { label: '费用上限', name: 'fysx', width: 50, align: 'left' },
            {
                label: '有效', name: 'zt', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == 0) {
                            return "无效";
                        } else {
                            return "有效";
                        }
                    }
                    return "";
                }
            }
        ],
        pager: "#gridPager",
        sortname: "CreateTime desc",
        rowNum: "10",
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid("setGridParam",
            {
                postData: { keyword: $("#txt_keyword").val(), ogrId: $("#OrganizeId").val() }
            }).trigger("reloadGrid");
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "系统病人收费算法维护",
        url: "/SystemManage/SysPatiChargeLogic/Form",
        width: "700px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var seleRowid = jQuery("#gridList").jqGrid("getGridParam", "selrow");
    if (!(seleRowid)) {
        $.modalAlert("请先选中一条信息", "warning");
        return;
    }
    var keyValue = $("#gridList").jqGridRowValue().brsfsfbh;
    $.modalOpen({
        id: "Form",
        title: "修改信息",
        url: "/SystemManage/SysPatiChargeLogic/Form?keyValue=" + keyValue,
        width: "700px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    var seleRowid = jQuery("#gridList").jqGrid("getGridParam", "selrow");
    if (!(seleRowid)) {
        $.modalAlert("请先选中一条信息", "warning");
        return;
    }
    $.deleteForm({
        url: "/SystemManage/SysPatiChargeLogic/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().brsfsfbh },
        success: function() {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    });
}
function btn_details() {
    var seleRowid = jQuery("#gridList").jqGrid("getGridParam", "selrow");
    if (!(seleRowid)) {
        $.modalAlert("请先选中一条信息", "warning");
        return;
    }
    var keyValue = $("#gridList").jqGridRowValue().brsfsfbh;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemManage/SysPatiChargeLogic/Form?keyValue=" + keyValue,
        width: "700px",
        height: "350px",
        btn: null,
    });
}