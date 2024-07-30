$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemManage/SysChargeClassify/GetdlGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: "主键", name: "flbh", hidden: true, key: true },
            { label: "代码", name: "fl", width: 80, align: 'left' },
            { label: '分类', name: 'flmc', width: 100, align: 'left' },
             { label: '拼音', name: 'py', width: 100, align: 'left' },
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
        title: "系统分类信息维护",
        url: "/SystemManage/SysChargeClassify/Form",
        width: "700px",
        height: "220px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().flbh;
    $.modalOpen({
        id: "Form",
        title: "修改信息",
        url: "/SystemManage/SysChargeClassify/Form?keyValue=" + keyValue,
        width: "700px",
        height: "220px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysChargeClassify/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().flbh },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().flbh;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemManage/SysChargeClassify/Form?keyValue=" + keyValue,
        width: "700px",
        height: "220px",
        btn: null,
    });
}
$("#btn_search").click(function () {
    $gridList.jqGrid('setGridParam', {
        postData: { keyword: $("#txt_keyword").val() },
    }).trigger('reloadGrid');
});