var VoucharsData = [];
ShowChargeItems();

function ShowChargeItems() {
    GetVoucherList();
    BindVouchersGrid();
}

function GetVoucherList() {
    for (var i = 0; i < 10; i++) {
        var item = {
            PatientName: "张三",
            CardNo: "1000122",
            VoucherNo: "10001",
            CreateVoucherDate: "2017-04-21",
            Total: "265.00"
        };
        VoucharsData.push(item);
    }
}

function BindVouchersGrid() {
    window.jQuery("#VoucherListGrid").jqGrid("clearGridData");
    window.jQuery("#VoucherListGrid").jqGrid({
        datatype: "local",
        height: document.documentElement.clientHeight - 85,
        width: document.documentElement.clientWidth - 5,
        shrinkToFit: true,
        rowNum: 30,
        rowList: [10, 20, 30],
        colModel: [
            { label: "姓名", name: "PatientName", width: "20%", align: "left" },
            { label: "卡号", name: "CardNo", width: "20%", align: "center" },
            { label: "凭证号", name: "VoucherNo", width: "20%", align: "center" },
            { label: "凭证生成日期", name: "CreateVoucherDate", width: "20%", align: "center" },
            { label: "总金额", name: "Total", width: "20%", align: "left" }
        ],
        pager: "#PageVoucherListGrid",
        viewrecords: true,
        unwritten: false,
        ondblClickRow: function (rowid) {
            EditSelectItem(rowid);
        }
    });
    for (var i = 0; i <= VoucharsData.length; i++) {
        window.jQuery("#VoucherListGrid").jqGrid('addRowData', i + 1, VoucharsData[i]);
    }
    window.jQuery("#VoucherListGrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
}

function EditSelectItem(rowId) {
    $.currentWindow().LoadChargeItems(rowId);
    $.modalClose();
}