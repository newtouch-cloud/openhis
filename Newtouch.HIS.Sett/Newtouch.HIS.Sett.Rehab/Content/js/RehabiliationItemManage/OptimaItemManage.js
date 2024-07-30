//打开新增Optima康复项目弹层
function AddOptimaRehabilitationItem() {
    $.modalOpen({
        id: "Form",
        title: "新增Optima康复项目",
        url: "/ItemBaseInfo/Add",
        width: "800px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

//打开修改His康复项目弹层
function EditOptimaRehabilitationItem(itemCode) {
    $.modalOpen({
        id: "Form",
        title: "修改His康复项目",
        url: "/ItemBaseInfo/Add?ItemCode=" + itemCode,
        width: "800px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

//His康复项目列表 jGrid声明
$("#gridList").newtouchLocalDataGrid({
    height: document.documentElement.clientHeight - 125,
    width: document.body.clientWidth,
    autowidth: false,
    rownumbers: false,
    unwritten: false,
    sortable: false,
    rowNum: 13,
    pager: jQuery('#gridPager'),
    shrinkToFit: true,
    colModel: [
        { label: "编码", name: "xmbm", width: "15%", key: true, align: "center" },
        { label: "名称", name: "xmmc", width: "40%", align: "left" },
        { label: "拼音", name: "xmpy", width: "10%", align: "left" },
        { label: "单位", name: "dw", width: "10%", align: "left" },
        {
            label: "单价", name: "dj", width: "10%", align: "left", formatter: function (cellvalue) {
                return cellvalue ? cellvalue.toFixed(2) : "0.00";
            }
        },
        { label: "时长", name: "sc", width: "5%", align: "left" },
        { label: "是否有效", name: "isValid", width: "10%", align: "center" }
    ],
    ondblClickRow: function (rowid, iRow, iCol, e) {
        EditOptimaRehabilitationItem(1001);
    }
}, [{ xmbm: 1001, xmmc: "红外线治疗", xmpy: "hwxzl", dw: "部位", dj: 5, sc: "20", isValid: "是" },
    { xmbm: 1002, xmmc: "激光疗法", xmpy: "jgzl", dw: "部位", dj: 10, sc: "20", isValid: "是" },
    { xmbm: 1003, xmmc: "低频脉冲电治疗", xmpy: "dpmcdzl", dw: "部位", dj: 8, sc: "20", isValid: "是" },
    { xmbm: 1004, xmmc: "神经肌肉电刺激治疗", xmpy: "sjjrdcjzl", dw: "部位", dj: 8, sc: "20", isValid: "是" },
    { xmbm: 1005, xmmc: "经皮神经电刺激治疗", xmpy: "jpsjdcjzl", dw: "部位", dj: 8, sc: "20", isValid: "是" },
    { xmbm: 1006, xmmc: "低频脉冲电治疗（≥3照射区）", xmpy: "dpmcdzl", dw: "", dj: 24, sc: "20", isValid: "是" },
    { xmbm: 1007, xmmc: "神经肌肉电刺激治疗", xmpy: "sjjrdcjzl", dw: "", dj: 24, sc: "20", isValid: "是" },
    { xmbm: 1008, xmmc: "红外线治疗", xmpy: "hwxzl", dw: "部位", dj: 5, sc: "20", isValid: "是" },
    { xmbm: 1009, xmmc: "激光疗法", xmpy: "jgzl", dw: "部位", dj: 10, sc: "20", isValid: "是" },
    { xmbm: 1010, xmmc: "低频脉冲电治疗", xmpy: "dpmcdzl", dw: "部位", dj: 8, sc: "20", isValid: "是" },
    { xmbm: 1011, xmmc: "神经肌肉电刺激治疗", xmpy: "sjjrdcjzl", dw: "部位", dj: 8, sc: "20", isValid: "是" },
    { xmbm: 1012, xmmc: "经皮神经电刺激治疗", xmpy: "jpsjdcjzl", dw: "部位", dj: 8, sc: "20", isValid: "是" },
    { xmbm: 1013, xmmc: "低频脉冲电治疗（≥3照射区）", xmpy: "dpmcdzl", dw: "", dj: 24, sc: "20", isValid: "是" }
]);
