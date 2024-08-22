$(function () {
    lastSettleInfoList();
})
function lastSettleInfoList() {
    var $lastSettleInfoList = $("#lastSettleInfoList");
    $lastSettleInfoList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/BusinessManage/OutPatient/GetLastSettleInfo", 
        height: 142,
        colModel: [
            { label: '姓名', name: 'xm', width: 100, algin: 'left' },
            { label: '应收', name: 'ysk', width: 100, algin: 'left' },
            { label: '实收', name: 'xjzf', width: 100, algin: 'left' },
            { label: '找零', name: 'zl', width: 100, algin: 'left' }
        ]
    });
    $("#linkLastSettledInfo").click(function () {
        //每次点击tab页，调整Grid宽度
        initLayout("MyTabGrid");

        $lastSettleInfoList.jqGrid('setGridParam', {
        }).trigger('reloadGrid');
    });
}