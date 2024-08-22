$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysCISpecialMark/GetbzGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: "主键", name: "sfxmtsbzbh", hidden: true, key: true },
            { label: '收费项目', name: 'sfxmmc', width: 200, align: 'left' },
            { label: '病人性质', name: 'brxzmc', width: 220, align: 'left' },
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
            { label: '建档人员', name: 'CreatorCode', width: 100, align: 'left' },
            {
                label: '建档日期', name: 'CreateTime', width: 100, align: 'left',
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
        title: "系统收费项目特殊标志维护",
        url: "/SystemManage/SysCISpecialMark/Form",
        width: "500px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().sfxmtsbzbh;
    $.modalOpen({
        id: "Form",
        title: "修改信息",
        url: "/SystemManage/SysCISpecialMark/Form?keyValue=" + keyValue,
        width: "500px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysCISpecialMark/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().sfxmtsbzbh },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().sfxmtsbzbh;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemManage/SysCISpecialMark/Form?keyValue=" + keyValue,
        width: "400px",
        height: "200px",
        btn: null,
    });
}
function submitForm() {
    $.submitForm({
        url: "/SystemManage/SysCISpecialMark/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").resetSelection();
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
            $.loading(false);
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}