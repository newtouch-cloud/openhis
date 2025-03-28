$(function () {
    gridList();
});
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysAgInsurChargeCateg/GetsfGridJson",
        height: $(window).height - 96,
        colModel: [
            { label: '主键', name: 'dlbh', hidden: true, key: true },
            { label: '代码', name: 'dl', width: 100, algin: 'left' },
            { label: '大类', name: 'dlmc', width: 100, algin: 'left' },
            { label: '拼音', name: 'py', width: 100, algin: 'left' },
            {
                label: '范围', name: 'mzzybz', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == "0") {
                        return "通用";
                    } else if (cellvalue == "1") {
                        return "门诊";
                    } else if (cellvalue == "2") {
                        return "部门";
                    } else if (cellvalue == "3") {
                        return "住院";
                    } else {
                        return "";
                    }
                }
            },
            {
                label: '有效', name: 'zt', width: 100, algin: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "否";
                    } else {
                        return "是";
                    }
                }
            },
            { label: '排序', name: 'px', width: 100, algin: 'left'},
            {
                label: '建档日期', name: 'jdrq', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
           { label: '建档人员', name: 'jdry', width: 80, align: 'left' }
        ]
    });
}

$("#btn_search").click(function () {
    $gridList.jqGrid('setGridParam', {
        postData: { keyword: $("#txt_keyword").val() }
    }).trigger('reloadGrid');
})
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "系统农保收费大类维护",
        url: "/SystemManage/SysAgInsurChargeCateg/Form",
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().brsfjmbh;
    $.modalOpen({
        id: "Form",
        title: "修改信息",
        url: "/SystemManage/AgInsurChargeCateg/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/AgInsurChargeCateg/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().brsfjmbh },
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().brsfjmbh;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemManage/AgInsurChargeCateg/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        btn: null,
    });
}