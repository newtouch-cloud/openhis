$(function () {
    gridList();
});
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysChargeProjPriceAdj/GetGridJson",
        height: $(window).height - 96,
        colModel: [
            { label: '主键', name: 'tjbh', hidden: true, key: true },
            { label: '收费项目', name: 'sfxm', width: 100, algin: 'left' },
            { label: '收费项目名称', name: 'sfxmmc', width: 100, algin: 'left' },
            { label: '单价', name: 'dj', width: 100, algin: 'left' },
            { label: '自付比例', name: 'zfbl', width: 100, algin: 'left' },
            {
                label: '自付性质', name: 'zfxz', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == "0") {
                        return "自付";
                    } else if (cellvalue == "1") {
                        return "自理";
                    }
                }
             },
            {
                label: '状态', name: 'zt', width: 100, algin: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "否";
                    } else {
                        return "是";
                    }
                }
            },
            { label: '执行标志', name: 'zxbz', width: 100, algin: 'left' },
            {
                label: '执行日期', name: 'zxrq', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
             },
            {
                label: '定点生效日期', name: 'ddsxrq', width: 100, algin: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            { label: '生效标志', name: 'sxbz', width: 100, algin: 'left' },
            {
                label: '建档日期', name: 'CreateTime', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
           { label: '建档人员', name: 'CreatorName', width: 80, align: 'left' }
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
        title: "系统收费项目调价维护",
        url: "/SystemManage/SysChargeProjPriceAdj/Form",
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
        url: "/SystemManage/SysChargeProjPriceAdj/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysChargeProjPriceAdj/DeleteForm",
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
        url: "/SystemManage/SysChargeProjPriceAdj/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        btn: null,
    });
}