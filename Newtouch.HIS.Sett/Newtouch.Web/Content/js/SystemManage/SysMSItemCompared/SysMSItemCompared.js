$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysMSItemCompared/GetdzGridJson",
        height: $(window).height() - 96,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            { label: '科室', name: 'ksmc', width: 100, align: 'left' },
            { label: '收费项目', name: 'sfxmmc', width: 220, align: 'left' },
            {
                label: '可收费', name: 'ksbz', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "只允许确认不允许收费  "
                    } else {
                        return "允许确认和收费"
                    }
                }
            },
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
            { label: '建档人员', name: 'jdry', width: 100, align: 'left' },
            {
                label: '建档日期', name: 'jdrq', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            }
        ]
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
        title: "系统医技项目对照维护",
        url: "/SystemManage/SysMSItemCompared/Form",
        width: "500px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "修改信息",
        url: "/SystemManage/SysMSItemCompared/Form?keyValue=" + keyValue,
        width: "500px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysMSItemCompared/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemManage/SysMSItemCompared/Form?keyValue=" + keyValue,
        width: "500px",
        height: "300px",
        btn: null,
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/SysMSItemCompared/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}