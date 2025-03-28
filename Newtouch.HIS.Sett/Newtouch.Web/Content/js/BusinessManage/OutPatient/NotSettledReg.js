$(function (){
    notSettledRegList();
})

function notSettledRegList() {
    var isJz = false;
    if ($("#sel_mzlx").val() == 3)
    {
        isJz = true;    //急诊
    }

    var isJs = false;  //表示未结算请求
    var $notSettledRegList = $("#notSettledRegList");
    $notSettledRegList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/BusinessManage/OutPatient/GetSettledFeesList",
        height: 142,
        colModel: [
            { label: '序号', name: '', algin: 'left' },
            { label: '挂号内码', name: 'ghnm', hidden:true, algin: 'left' },
            { label: '挂号类型', name: 'sfxmmc', algin: 'left' },
            { label: '病人类型', name: 'brxzmc', algin: 'left' },
            { label: '大病项目', name: 'dbxmmc', algin: 'left' },
            { label: '总金额', name: 'zje', algin: 'left' },
            { label: '是否已作废', name: 'iszf', algin: 'left' }
        ]
    });

    $("#linknotSettledReg").click(function () {
        //每次点击tab页，调整Grid宽度
        initLayout("MyTabGrid");

        $notSettledRegList.jqGrid('setGridParam', {
            postData: { kh: $("#txtkh").val(),isJz:isJz,isJs:isJs },
        }).trigger('reloadGrid');
    });
    
}
