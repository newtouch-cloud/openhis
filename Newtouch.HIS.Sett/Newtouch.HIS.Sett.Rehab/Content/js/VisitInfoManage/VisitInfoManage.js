var griddata = [];

//Grid绑定已选项目
function BindVisitInfoData() {
    window.jQuery("#AccountingListGrid").jqGrid("clearGridData");
    window.jQuery("#AccountingListGrid").jqGrid({
        datatype: "local",
        height: "auto",
        width: $("#dvVisitG").width(),
        shrinkToFit: true,
        rowNum: 30,
        rowList: [10, 20, 30],
        autoScroll: true,
        colModel: [
            {
                label: "退", name: "IS_RETURN", width: "3%", align: "left", editable: true, edittype: "checkbox", formatter: function (cellvalue) {
                    return "<input name='chkReturn' class='IS_RETURN' type='checkbox' value='' />";
                }
            },
            { label: "治疗项目", name: "ItemName", width: "15%", align: "left" },
            { label: "替代项目", name: "ReplaceTtemName", width: "15%", align: "left" },
            { label: "数量", name: "ItemCount", width: "3%", align: "left" },
            {
                label: "退数量", name: "RETURNS_SL", width: "5%", align: "left", formatter: function (cellvalue) {
                    return "<input name='IS_RETURN' type='text' class='tuishuliang' style='width: 35px; height:15px;' checked value='" + cellvalue + "' />";
                }
            },
            { label: "单位", name: "Unit", width: "5%", align: "left" },
            { label: "单次时长", name: "SingleTimeLength", width: "9%", align: "left" },
            { label: "单价", name: "UnitPrice", width: "5%", align: "left" },
            { label: "类别", name: "Category", width: "3%", align: "left" },
            { label: "治疗师", name: "Doctor", width: "5%", align: "left" },
            { label: "治疗时间", name: "TreatDate", width: "10%", align: "left" },
            { label: "自费比例", name: "SelfPaidRatio", width: "5%", align: "left" },
            { label: "总价", name: "Total", width: "5%", align: "left" },
            { label: "备注", name: "Remark", width: "12%", align: "left" },
            { label: "No", name: "No", hidden: true },
            { label: "ItemCode", name: "ItemCode", hidden: true },
            { label: "ReplaceItemCode", name: "ReplaceItemCode", hidden: true },
            { label: "ReplaceTtemCount", name: "ReplaceTtemCount", hidden: true },
            { label: "ReplaceTtemUnit", name: "ReplaceTtemUnit", hidden: true },
            { label: "ReplaceTtemUnitPrice", name: "ReplaceTtemUnitPrice", hidden: true },
            { label: "Visit", name: "Visit", hidden: true },
            { label: "ReplaceTtemSingleTimeLength", name: "ReplaceTtemSingleTimeLength", hidden: true }
        ],
        pager: "#PageAccountingListGrid",
        viewrecords: true,
        unwritten: false,
        sortname: "ItemName"
        //ondblClickRow: function (rowid) {
        //    EditSelectItem(rowid);
        //}
    });
    for (var i = 0; i <= griddata.length; i++) {
        window.jQuery("#AccountingListGrid").jqGrid('addRowData', i + 1, griddata[i]);
    }
    window.jQuery("#AccountingListGrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
}

function GetVisitInfo(rowId) {
    for (var i = 0; i < 5; i++) {
        var itemjson = {
            No: i.toString(),
            ItemCode: "1001",
            ItemName: "红外线治疗",
            ItemCount: "2",
            RETURNS_SL: "1",
            Unit: "次",
            UnitPrice: "20",
            SingleTimeLength: "30",
            Category: "PT",
            Doctor: "童咪咪",
            TreatDate: "2017-04-11",
            SelfPaidRatio: "0.1%",
            Total: "40",
            IS_RETURN: "",
            ReplaceItemCode: "",
            ReplaceTtemName: "",
            ReplaceTtemCount: "",
            ReplaceTtemUnit: "",
            ReplaceTtemUnitPrice: "",
            ReplaceTtemSingleTimeLength: "",
            Visit: "",
            Remark: ""
        };
        griddata.push(itemjson);
    }
    return griddata;
}

function LoadChargeItems(rowId) {
    GetVisitInfo(rowId);
    BindVisitInfoData();
}

function Refund() {
    var refundCount = 0;
    var v = "";
    $("[name='chkReturn']:checked").each(function () {
        var tds = $(this).parent().parent().find("td");
        if (tds != undefined && tds.length > 0) {
            for (var n = 0; n < tds.length; n++) {
                if ($(tds[n]).attr("aria-describedby").replace("AccountingListGrid_", "") === "RETURNS_SL") {
                    refundCount = $(tds[n]).find("input").val();
                    continue;
                }
                if ($(tds[n]).attr("aria-describedby").replace("AccountingListGrid_", "") === "No") {
                    v = $(tds[n]).text();
                    for (var i = 0; i < griddata.length; i++) {
                        if (griddata[i].No === v) {
                            griddata[i].ItemCount = parseInt(griddata[i].ItemCount) - parseInt(refundCount);
                            if (griddata[i].ItemCount <= 0) griddata.splice(i, 1);
                        }
                    }
                }
            }
        }
    });
    BindVisitInfoData();
}

//快捷键：清除
function newtouch_event_f4() {
    $("#AccountingListGrid").jqGrid("clearGridData");
    BindVisitInfoData();
}

//快捷键：F6：退费
function newtouch_event_f6() {
    $.modalConfirm("您确定要退费吗？",
        function (flag) {
            if (flag === true) Refund();
        });
}

