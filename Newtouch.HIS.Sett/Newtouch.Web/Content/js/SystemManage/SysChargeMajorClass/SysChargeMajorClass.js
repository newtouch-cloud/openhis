$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemManage/SysChargeMajorClass/GetdlGridJson",
        height: $(window).height() - 128,
        cache: false,
        colModel: [
            { label: "主键", name: "dlId", hidden: true, key: true },
            { label: "代码", name: "dlCode", width: 80, align: 'left' },
            { label: '大类', name: 'dlmc', width: 100, align: 'left' },
             { label: '拼音', name: 'py', width: 100, align: 'left' },
            {
                label: '范围', name: 'mzzybz', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == "0") {
                        return "通用";
                    } else if (cellvalue == "1") {
                        return "门诊";
                    } else if (cellvalue == "2") {
                        return "住院";
                    } else {
                        return "";
                    }
                }
            },
            {
                label: '门诊报告大类', name: 'mzprintreportcode', width: 80, align: 'left'
            },
            {
                label: '门诊账单大类', name: 'mzprintbillcode', width: 80, align: 'left'
            },
            { label: '排序', name: 'px', width: 80, align: 'left' },
            {
                label: '有效', name: 'zt', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "无效"
                    } else {
                        return "有效"
                    }
                }
            },
            { label: '建档人员', name: 'CreatorCode', width: 50, align: 'left' },
            {
                label: '建档日期', name: 'CreateTime', width: 50, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            }
        ],
        pager: "#gridPager",
        sortname: 'CreateTime desc',
        rowNum: '10',
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: { keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "系统病人收费算法维护",
        url: "/SystemManage/SysChargeMajorClass/Form",
        width: "700px",
        height: "320px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().dlId;
    $.modalOpen({
        id: "Form",
        title: "修改信息",
        url: "/SystemManage/SysChargeMajorClass/Form?keyValue=" + keyValue,
        width: "700px",
        height: "320px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysChargeMajorClass/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().dlId },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().dlId;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemManage/SysChargeMajorClass/Form?keyValue=" + keyValue,
        width: "700px",
        height: "320px",
        btn: null,
    });
}
$("#btn_search").click(function () {
    $gridList.jqGrid('setGridParam', {
        postData: { keyword: $("#txt_keyword").val() },
    }).trigger('reloadGrid');
});