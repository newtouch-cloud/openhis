$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysCIRemindContent/GetnrGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: "主键", name: "sfxjsnrbh", hidden: true, key: true },
            { label: '收费项目', name: 'sfxmmc', width: 100, align: 'left' },
            { label: '门诊警示内容', name: 'mzjsnr', width: 100, align: 'left' },
            { label: '住院警示内容', name: 'zyjsnr', width: 220, align: 'left' },
             {
                 label: '门诊警示级别', name: 'mzjsjb', width: 50, align: 'left',
                 formatter: function (cellvalue) {
                     if (cellvalue == 0) {
                         return "不提示";
                     } else if (cellvalue == 1) {
                         return "普通提示";
                     } else if (cellvalue == 2) {
                         return "警示";
                     } else if (cellvalue == 3) {
                         return "禁止使用";
                     }
                 }
             },
            {
                label: '住院警示级别', name: 'zyjsjb', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "不提示";
                    } else if (cellvalue == 1) {
                        return "普通提示";
                    } else if (cellvalue == 2) {
                        return "警示";
                    } else if (cellvalue == 3) {
                        return "禁止使用";
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
        title: "系统收费项目警示内容维护",
        url: "/SystemManage/SysCIRemindContent/Form",
        width: "700px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().sfxjsnrbh;
    $.modalOpen({
        id: "Form",
        title: "修改信息",
        url: "/SystemManage/SysCIRemindContent/Form?keyValue=" + keyValue,
        width: "700px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysCIRemindContent/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().sfxjsnrbh },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().sfxjsnrbh;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemManage/SysCIRemindContent/Form?keyValue=" + keyValue,
        width: "700px",
        height: "300",
        btn: null,
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/SysCIRemindContent/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").resetSelection();
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}