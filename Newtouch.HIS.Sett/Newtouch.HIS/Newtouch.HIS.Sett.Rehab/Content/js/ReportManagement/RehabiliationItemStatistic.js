
var griddata = [];

var gridColumns = [];

// ************************ 公共部分  end  ************************************** //

//获取ItemsGrid的列 主要为了拼接大类
function GetItemsGridColumns() {
    //gridColumns = [
    //    { label: "日期", name: "Date", width: "100", align: "center" },
    //    { label: "门诊号", name: "OutpatientNo", width: "100", align: "center" },
    //    { label: "姓名", name: "PatientName", width: "100", align: "center" },
    //    { label: "性别", name: "Gender", width: "60", align: "center" },
    //    { label: "年龄", name: "Age", width: "60", align: "center" },
    //    { label: "病症", name: "Disease", width: "120", align: "center" },
    //    { label: "备注", name: "Remark", width: "120", align: "center" },
    //    { label: "治疗师", name: "Doctor", width: "100", align: "center" },
    //    { label: "费用", name: "Total", width: "50", align: "left" },
    //    { label: "推拿", name: "TuiNa", width: "50", align: "left" },
    //    { label: "针法", name: "ZhenFa", width: "50", align: "left" },
    //    { label: "灸法", name: "JiuFa", width: "50", align: "left" },
    //    { label: "拔罐", name: "BaGuan", width: "50", align: "left" },
    //    { label: "药熏", name: "YaoXun", width: "50", align: "left" },
    //    { label: "牵引", name: "QianYin", width: "50", align: "left" },
    //    { label: "超声", name: "ChaoSheng", width: "50", align: "left" },
    //    { label: "药透", name: "YaoTou", width: "50", align: "left" },
    //    { label: "理疗", name: "LiLiao", width: "50", align: "left" },
    //    { label: "康复", name: "kangFu", width: "50", align: "left" },
    //    { label: "副木", name: "FuMu", width: "50", align: "left" },
    //    { label: "药品", name: "Medicine", width: "50", align: "left" },
    //    { label: "检查", name: "Check", width: "50", align: "left" },
    //    { label: "人次", name: "PersonTime", width: "50", align: "left" }
    //];
    gridColumns = [
        { label: "日期", name: "Date", width: "6%", align: "center" },
        { label: "门诊号", name: "OutpatientNo", width: "6%", align: "center" },
        { label: "姓名", name: "PatientName", width: "6%", align: "center" },
        { label: "性别", name: "Gender", width: "5%", align: "center" },
        { label: "年龄", name: "Age", width: "5%", align: "center" },
        { label: "病症", name: "Disease", width: "11%", align: "center" },
        { label: "备注", name: "Remark", width: "10%", align: "center" },
        { label: "治疗师", name: "Doctor", width: "6%", align: "center" },
        { label: "费用", name: "Total", width: "3%", align: "left" },
        { label: "推拿", name: "TuiNa", width: "3%", align: "left" },
        { label: "针法", name: "ZhenFa", width: "3%", align: "left" },
        { label: "灸法", name: "JiuFa", width: "3%", align: "left" },
        { label: "拔罐", name: "BaGuan", width: "3%", align: "left" },
        { label: "药熏", name: "YaoXun", width: "3%", align: "left" },
        { label: "牵引", name: "QianYin", width: "3%", align: "left" },
        { label: "超声", name: "ChaoSheng", width: "3%", align: "left" },
        { label: "药透", name: "YaoTou", width: "3%", align: "left" },
        { label: "理疗", name: "LiLiao", width: "3%", align: "left" },
        { label: "康复", name: "kangFu", width: "3%", align: "left" },
        { label: "副木", name: "FuMu", width: "3%", align: "left" },
        { label: "药品", name: "Medicine", width: "3%", align: "left" },
        { label: "检查", name: "Check", width: "3%", align: "left" },
        { label: "人次", name: "PersonTime", width: "3%", align: "left" }
    ];
}

function BindItemData() {
    window.jQuery("#ItemsGrid").jqGrid("clearGridData");
    window.jQuery("#ItemsGrid").jqGrid({
        datatype: "local",
        height: "auto",
        width: document.documentElement.clientWidth-5,
        rowNum: 30,
        shrinkToFit: true,
        autoScroll: false,
        rowList: [10, 20, 30],
        colModel: gridColumns,
        viewrecords: true,
        unwritten: false,
        sortname: "ItemName",
        pager: "#PageItemsGrid",
        ondblClickRow: function (rowid, iRow, iCol, e) {
            ShowSelectedDetail(rowid, iRow, iCol, e);
        }
    });
    for (var i = 0; i <= griddata.length; i++) {
        window.jQuery("#ItemsGrid").jqGrid('addRowData', i + 1, griddata[i]);
    }
}

function FullItemsGrid() {
    GetItemsGridColumns();
    GetItemsGridData();
    BindItemData();
}

function GetItemsGridData() {
    griddata = [];
    for (var i = 0; i < 5; i++) {
        var itemjson = {
            Week: "周一",
            Date: "2017-04-01",
            OutpatientNo: "10001",
            PatientName: "张三",
            Gender: "男",
            Age: "30",
            Disease: "骨折",
            Remark: "三级",
            Doctor: "童咪咪",
            Total: "200",
            TuiNa: "20",
            ZhenFa: "60",
            JiuFa: "0",
            BaGuan: "0",
            YaoXun: "0",
            QianYin: "0",
            ChaoSheng: "100",
            YaoTou: "0",
            LiLiao: "20",
            kangFu: "0",
            FuMu: "0",
            Medicine: "",
            Check: "",
            PersonTime: ""
        };
        griddata.push(itemjson);
    }
}

//跳转到详细信息页面
function ShowSelectedDetail(rowid, iRow, iCol, e) {
    $.modalOpen({
        id: "Form",
        title: "门诊收费项目统计明细",
        url: "/ReportManagement/RehabiliationItemStatisticDetail",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
    //window.location.href = "RehabiliationItemStatisticDetail";
}


// ************************ 以下为明细部分 ************************************** //

//Grid绑定已选项目
function BindItemDetailData() {
    window.jQuery("#ItemDetailGrid").jqGrid("clearGridData");
    window.jQuery("#ItemDetailGrid").jqGrid({
        datatype: "local",
        height: 665,
        width: document.body.clientWidth,
        rowNum: 30,
        rowList: [10, 20, 30],
        autoScroll: true,
        colModel: [
            { label: "大类", name: "dl", width: "5%", align: "left" },
            { label: "治疗项目", name: "ItemName", width: "15%", align: "left" },
            { label: "替代项目", name: "ReplaceTtemName", width: "15%", align: "left" },
            { label: "数量", name: "ItemCount", width: "3%", align: "center" },
            { label: "单位", name: "Unit", width: "4%", align: "center" },
            { label: "单次时长", name: "SingleTimeLength", width: "9%", align: "center" },
            { label: "单价", name: "UnitPrice", width: "5%", align: "center" },
            { label: "类别", name: "Category", width: "3%", align: "center" },
            { label: "治疗师", name: "Doctor", width: "7%", align: "left" },
            { label: "治疗时间", name: "TreatDate", width: "8%", align: "center" },
            { label: "自费比例", name: "SelfPaidRatio", width: "6%", align: "center" },
            { label: "总价", name: "Total", width: "5%", align: "center" },
            { label: "备注", name: "Remark", width: "15%", align: "left" },
            { label: "ItemCode", name: "ItemCode", hidden: true },
            { label: "ReplaceItemCode", name: "ReplaceItemCode", hidden: true },
            { label: "ReplaceTtemCount", name: "ReplaceTtemCount", hidden: true },
            { label: "ReplaceTtemUnit", name: "ReplaceTtemUnit", hidden: true },
            { label: "ReplaceTtemUnitPrice", name: "ReplaceTtemUnitPrice", hidden: true },
            { label: "ReplaceTtemSingleTimeLength", name: "ReplaceTtemSingleTimeLength", hidden: true }
        ],
        pager: "#PageDetailGrid",
        viewrecords: true,
        sortname: "ItemName"
    });
    for (var i = 0; i <= griddata.length; i++) {
        window.jQuery("#ItemDetailGrid").jqGrid('addRowData', i + 1, griddata[i]);
    }
    window.jQuery("#ItemDetailGrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
}

function FullItemDetailGrid() {
    GetItemDetailGridData();
    BindItemDetailData();
}

function GetItemDetailGridData() {
    griddata = [];
    for (var i = 0; i < 5; i++) {
        var itemjson = {
            dl: "推拿",
            ItemCode: "1001",
            ItemName: "红外线治疗",
            ItemCount: "2",
            Unit: "次",
            UnitPrice: "20",
            SingleTimeLength: "30",
            Category: "PT",
            Doctor: "童咪咪",
            TreatDate: "2017-04-11",
            SelfPaidRatio: "0.1%",
            Total: "40",
            ReplaceItemCode: "",
            ReplaceTtemName: "",
            ReplaceTtemCount: "",
            ReplaceTtemUnit: "",
            ReplaceTtemUnitPrice: "",
            ReplaceTtemSingleTimeLength: "",
            Remark: ""
        };
        griddata.push(itemjson);
    }
}