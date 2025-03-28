function Search() {
    var v = $("input[type='radio'][name='sourceFrom']:checked").val();
    if (v != undefined && v == "His") {
        BindHisItemData();
    } else {
        BindOptimaItemData();
    }
}

//His康复项目列表 jGrid声明
function BindHisItemData() {
    jQuery("#HisGridList").jqGrid({
        height: 200,
        width: document.body.clientWidth / 2 - 20,
        autowidth: false,
        rownumbers: false,
        unwritten: false,
        sortable: false,
        rowNum: 13,
        pager: jQuery('#HisGridPager'),
        shrinkToFit: true,
        datatype: "local",
        colModel: [
            { label: "His编码", name: "xmbm", width: "20%", key: true, align: "center" },
            { label: "His名称", name: "xmmc", width: "60%", align: "left" },
            { label: "是否有效", name: "isValid", width: "20%", align: "center" }
        ],
        ondblClickRow: function (rowid, iRow, iCol, e) {
            ThrowItemToRelationPool(GetItemCodebyIndex(rowid), "His");
        }
    });
    var mydata = [{ xmbm: 1001, xmmc: "红外线治疗", isValid: "是" },
        { xmbm: 1002, xmmc: "激光疗法", isValid: "是" },
        { xmbm: 1003, xmmc: "低频脉冲电治疗", isValid: "是" },
        { xmbm: 1004, xmmc: "神经肌肉电刺激治疗", isValid: "是" },
        { xmbm: 1005, xmmc: "经皮神经电刺激治疗", isValid: "是" },
        { xmbm: 1006, xmmc: "低频脉冲电治疗（≥3照射区）", isValid: "是" }];
    for (var i = 0; i <= mydata.length; i++) {
        jQuery("#HisGridList").jqGrid('addRowData', i + 1, mydata[i]);
    }
    jQuery("#HisGridList").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });
}

//His康复项目列表 jGrid声明
function BindOptimaItemData() {
    jQuery("#OptimaGridList").jqGrid({
        height: 200,
        width: document.body.clientWidth / 2 - 20,
        autowidth: false,
        rownumbers: false,
        unwritten: false,
        sortable: false,
        rowNum: 13,
        pager: jQuery('#OptimaGridPager'),
        shrinkToFit: true,
        datatype: "local",
        colModel: [
            { label: "Optima编码", name: "xmbm", width: "20%", key: true, align: "center" },
            { label: "Optima名称", name: "xmmc", width: "60%", align: "left" },
            { label: "是否有效", name: "isValid", width: "20%", align: "center" }
        ],
        ondblClickRow: function (rowid, iRow, iCol, e) {
            ThrowItemToRelationPool(GetItemCodebyIndex(rowid), "Optima");
        }
    });
    var mydata = [{ xmbm: 1001, xmmc: "红外线治疗", isValid: "是" },
        { xmbm: 1002, xmmc: "激光疗法", isValid: "是" },
        { xmbm: 1003, xmmc: "低频脉冲电治疗", isValid: "是" },
        { xmbm: 1004, xmmc: "神经肌肉电刺激治疗", isValid: "是" },
        { xmbm: 1005, xmmc: "经皮神经电刺激治疗", isValid: "是" },
        { xmbm: 1006, xmmc: "低频脉冲电治疗（≥3照射区）", isValid: "是" }];
    for (var i = 0; i <= mydata.length; i++) {
        jQuery("#OptimaGridList").jqGrid('addRowData', i + 1, mydata[i]);
    }
    jQuery("#OptimaGridList").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });
}

//将指定项目丢入关联池
function ThrowItemToRelationPool(itemCode, sourceFrom) {
    relateData.push({ xmbm: 1001, xmmc: "红外线治疗", isValid: "是" });
    BindRelateData();
}

//索引找到指定项目带
function GetItemCodebyIndex(i) {
    return 1001;
}

////需要关联的数据
//var relateData = [{ xmbm: 1001, xmmc: "红外线治疗", isValid: "是" },
//    { xmbm: 1002, xmmc: "激光疗法", isValid: "是" },
//    { xmbm: 1003, xmmc: "低频脉冲电治疗", isValid: "是" },
//    { xmbm: 1004, xmmc: "神经肌肉电刺激治疗", isValid: "是" },
//    { xmbm: 1005, xmmc: "经皮神经电刺激治疗", isValid: "是" },
//    { xmbm: 1006, xmmc: "低频脉冲电治疗（≥3照射区）", isValid: "是" }];
var relateData = [];

function BindRelateData() {
    jQuery("#RelateGridList").jqGrid("clearGridData");
    jQuery("#RelateGridList").jqGrid({
        height: 300,
        width: document.body.clientWidth,
        autowidth: false,
        rownumbers: false,
        unwritten: false,
        sortable: false,
        rowNum: 13,
        pager: jQuery('#RelateGridPager'),
        shrinkToFit: true,
        datatype: "local",
        colModel: [
            { label: "Optima编码", name: "xmbm", width: "20%", key: true, align: "center" },
            { label: "Optima名称", name: "xmmc", width: "60%", align: "left" },
            { label: "是否有效", name: "isValid", width: "20%", align: "center" }
        ]
    });
    for (var i = 0; i <= relateData.length; i++) {
        jQuery("#RelateGridList").jqGrid('addRowData', i + 1, relateData[i]);
    }
    jQuery("#RelateGridList").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });
}

//清楚已选关联数据
function Clean() {
    if (relateData != undefined && relateData.length > 0) {
        relateData.splice(0, relateData.length);
    }
    BindRelateData();
}

//提交关联
function SubmintRelate() {
    alert("关联成功！");
    Clean();
}