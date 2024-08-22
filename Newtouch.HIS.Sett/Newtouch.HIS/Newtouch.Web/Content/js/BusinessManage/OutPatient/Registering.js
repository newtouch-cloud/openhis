$(function () {
    registeringListNew([]);
});

function registeringListNew(mydata) {
    var $registeringList = $("#registeringList");
    $registeringList.dataNewGrid({  
        height: 142,
        colModel: [
            { label: '挂号类型', name: 'ghlx', width: 100, algin: 'left' },
            { label: '病人性质编号', name: 'brxzbh', hidden: true, width: 100, algin: 'left' },
            { label: '病人类型', name: 'brxz', width: 100, algin: 'left' },
            { label: '大病项目', name: 'dbxm', width: 100, algin: 'left' },
            { label: '挂号费', name: 'ghf', width: 100, algin: 'left' },
            { label: '诊疗费', name: 'zlf', width: 100, algin: 'left' },
            { label: '磁卡费', name: 'ckf', width: 100, algin: 'left' },
            { label: '工本费', name: 'gbf', width: 100, algin: 'left' },
            { label: '总金额', name: 'totalfees', width: 100, algin: 'left' },
        ]
    }, mydata);
    if (mydata.length > 0)
    {
        $('.ui-jqgrid-bdiv').find(".unwritten").remove();
    }


}