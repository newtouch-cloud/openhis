$(function (){
    closedRegList();
})
function closedRegList() {
    var isJz = false;
    if ($("#sel_mzlx").val() == 3) {
        isJz = true;    //急诊
    }
    //var isJs = true;  //表示已结算请求
    var $closedRegList = $("#closedRegList");
    $closedRegList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/BusinessManage/OutPatient/GetSettledFeesList",
        height: 142,
        colModel: [
            { label: '序号', name: '', width: 100, algin: 'left' },
            { label: '挂号内码', name: 'ghnm', hidden: true, width: 100, algin: 'left' },
            { label: '挂号类型', name: 'sfxmmc', width: 100, algin: 'left' },
            { label: '病人类型', name: 'brxzmc', width: 100, algin: 'left' },
            { label: '大病项目', name: 'dbxmmc', width: 100, algin: 'left' },
            { label: '总金额', name: 'zje', width: 100, algin: 'left' },
            { label: '是否已退号', name: 'isth', width: 100, algin: 'left' },
            { label:'是否退磁卡/工本费',id:'isReturnAll', formatter: "checkbox" }
        ]
    });

    $("#linkclosedReg").click(function () {
        //每次点击tab页，调整Grid宽度
        initLayout("MyTabGrid");

        $closedRegList.jqGrid('setGridParam', {
            postData: { kh: $("#txtkh").val(), isJz: isJz },
        }).trigger('reloadGrid');
    });

}