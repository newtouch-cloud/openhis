$(function () {
    lastSettleInfoList();
})

function lastSettleInfoList() {
    var $lastSettleInfoList = $("#lastSettleInfoList");
    $lastSettleInfoList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/OutpatientManage/OutpatientReg/GetLastSettleInfo",
        height: 142,
        colModel: [
            { label: '发票号', name: 'fph', width: 100, algin: 'left' },
            { label: '姓名', name: 'xm', width: 100, algin: 'left' },
            { label: '应收', name: 'ysk', width: 100, algin: 'left' },
            { label: '实收', name: 'xjzf', width: 100, algin: 'left' },
            { label: '找零', name: 'zl', width: 100, algin: 'left' }
        ],
        loadComplete: function (data) {
            if (data && data.length && data.length > 0) {
                $("#lblqfph").text(data[0].fph);
                $("#lblzje").text(data[0].xjzf);
                $("#lblys").text(data[0].ysk);
                $("#lblzl").text(data[0].zl);
            }
        }
    });
    $("#linkLastSettledInfo").click(function () {
        //每次点击tab页，调整Grid宽度
        initLayout("MyTabGrid");

        $lastSettleInfoList.jqGrid('setGridParam', {
        }).trigger('reloadGrid');

    });

}