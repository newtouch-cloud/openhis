$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemManage/SysChargeItem/GetsfGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: "主键", name: "sfxmbh", hidden: true, key: true },
            { label: "代码", name: "sfxm", width: 80, align: 'left' },
            { label: '收费项目', name: 'sfxmmc', width: 100, align: 'left' },
            { label: '拼音', name: 'py', width: 50, align: 'left' },
            { label: '单位', name: 'dw', width: 50, align: 'left' },
            { label: '单价', name: 'dj', width: 50, align: 'left' },
            { label: '大类', name: 'dlmc', width: 100, align: 'left' },
            { label: '分类', name: 'flmc', width: 100, align: 'left' },
            { label: '自负比例', name: 'zfbl', width: 50, align: 'left' },
            { label: '医保代码', name: 'ybdm', width: 100, align: 'left' },
            { label: '物价代码', name: 'wjdm', width: 100, align: 'left' },
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
             }, {
                 label: '实施', name: 'ssbz', width: 50, align: 'left',
                 formatter: function (cellvalue) {
                     if (cellvalue == 0) {
                         return "是"
                     } else {
                         return "否"
                     }
                 }
             },
{
    label: '特殊', name: 'tsbz', width: 50, align: 'left',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return "是"
        } else {
            return "否"
        }
    }
},
{
    label: '警示', name: 'jsbz', width: 50, align: 'left',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return "是"
        } else {
            return "否"
        }
    }
},
{ label: '收费项', name: 'sfbz', width: 50, align: 'left' },
{
    label: '有效', name: 'zt', width: 50, align: 'left',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return "无效"
        } else {
            return "有效"
        }
    }
}],
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
        title: "收费项目维护",
        url: "/SystemManage/SysChargeItem/Form",
        width: "800px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().sfxmbh;
    $.modalOpen({
        id: "Form",
        title: "修改信息",
        url: "/SystemManage/SysChargeItem/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysChargeItem/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().sfxmbh },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().sfxmbh;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemManage/SysChargeItem/Form?keyValue=" + keyValue,
        width: "700px",
        height: "560px",
        btn: null,
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/SysChargeItem/SubmitForm?keyValue=" + keyValue,
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