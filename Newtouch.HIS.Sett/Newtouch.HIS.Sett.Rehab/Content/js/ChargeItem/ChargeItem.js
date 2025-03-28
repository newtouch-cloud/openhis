var ReplaceItemsCout = 0;
var SelectedItemCount = 0;

//康复项目剩余次数
var RemainderTimes = [
    {
        key: "1", name: "超声药物透入治疗", count: 10
    },
    {
        key: "0", name: "红外线治疗", count: 10
    }
];

$(function () {
    window.$('[name="radio_srif"]').bootstrapSwitch({
        onText: "无",
        offText: "有",
        size: "mini",
        onSwitchChange: function (event, state) {
            if (state === true) {
                window.$(this).val("1");
                NoReplaceItemDefaultVal();
            } else {
                window.$(this).val("0");
                HadReplaceItemDefaultVal();
            }
        }
    });
});

//添加
function SaveItem() {
    if (!CheckVal()) return;
    var items = GetInputVal();
    if (items != undefined && items.length > 0) {
        $.each(items, function (index, val) {
            griddata.push(val);
        });
        BindItemData();
    }
    DefaultVal();
}

//修改
function ModifyItem() {
    if (SelectedItemCount != undefined && SelectedItemInfo.No !== "") {
        var items = GetInputVal();
        if (items != undefined && items.length > 0 && griddata != undefined && griddata.length > 0) {
            DeleteItem();
            $.each(items, function (index, val) {
                griddata.push(val);
            });
            BindItemData();
        }
    }
    DefaultVal();
}

//单条提交
function DeleteItem() {
    if (SelectedItemInfo != undefined && SelectedItemInfo.No !== "" && griddata != undefined && griddata.length > 0) {
        for (var i = 0; i < griddata.length; i++) {
            if (griddata[i].No === SelectedItemInfo.No) {
                griddata.splice(i, 1);
            }
        }
        BindItemData();
    }
    DefaultVal();
}

//清楚
function newtouch_globalevent_f4() {
    SelectedItemCount = 0;
    if (griddata != undefined && griddata.length > 0) {
        griddata.splice(0, griddata.length);
    }
    BindItemData();
    DefaultVal();
}

//全部提交
function newtouch_globalevent_f8() {
    ReckonRemainderTimes();
    newtouch_globalevent_f4();
    alert("提交成功！");
}

//检查输入值
function CheckVal(cnt) {
    if (window.$("#ddlzlxm").val().trim() === "") {
        alert("请选择项目！");
        return false;
    }
    return true;
}

//移除当前替换项目
function RemoveItem(i) {
    window.$("#trReplaceItem_" + i).remove();
    if (window.$("#tabReplaceItems").find("tr") == undefined ||
        window.$("#tabReplaceItems").find("tr").length === 0) {
        NoReplaceItemDefaultVal();
    }
}

//更多替代项目
function MoreReplaceItem() {
    window.$("#tabReplaceItems").append("<tr class=\"form\" id=\"trReplaceItem_" + (++ReplaceItemsCout) + "\">"
        + "<th class=\"formTitle \" >替代项目：</th>"
        + " <td class=\"formValue\" colspan=\"3\">"
        + "<select id=\"ddltdxm_" + ReplaceItemsCout + "\" class=\"form-control\"  onchange=\"ddltdxmchange(this)\">"
        + "<option value=\"\">===请选择===</option>"
        + "<option value=\"0\">红外线治疗</option>"
        + "<option value=\"1\">超声药物透入治疗</option>"
        + "<option value=\"2\">普通针刺（体针）</option>"
        + "</select>"
        + "</td>"
        + " <th class=\"formTitle\">收费标准：</th>"
        + " <td class=\"formValue\"><input type=\"text\" class=\"form-control newtouch_Readonly\" id=\"txtsfbz_" + ReplaceItemsCout + "\" name=\"txtsfbz\" /></td>"
        + "<input type=\"text\" class=\"form-control \" id=\"txttddw_" + ReplaceItemsCout + "\" name=\"txttddw\" />"
        + " <th class=\"formTitle\">数量：</th>"
        + " <td class=\"formValue\"><input type=\"text\" class=\"form-control\" id=\"txtsl_" + ReplaceItemsCout + "\" name=\"txtsl\" value=\"1\" /></td>"
        + " <th class=\"formTitle\">总费用：</th>"
        + " <td class=\"formValue\"><input type=\"text\" class=\"form-control newtouch_Readonly\" id=\"txtzfy_" + ReplaceItemsCout + "\" name=\"txtzfy\"/></td>"
        + " <th class=\"formTitle\">治疗时长：</th>"
        + " <td class=\"formValue\"><input type=\"text\" class=\"form-control\" placeHolder=\"分钟\" id=\"txtzlsc_" + ReplaceItemsCout + "\" name=\"txtzlsc\"/></td>"
        + "<td class=\"formTitle\">"
        + "<span style=\"text-align:center;color: red; font-size:16px;font-size: 1px;font-size: px;font-size: 20px;\" class=\"glyphicon glyphicon-minus\" onclick=\"RemoveItem(" + ReplaceItemsCout + ")\"></span></td>"
        + "</td>"
        + "<td class=\"formValue\">"
        + "</td>"
        + "</tr>");
}

//已添加项目
var griddata = [];

//Grid绑定已选项目
function BindItemData() {
    window.jQuery("#SelectedListgrid").jqGrid("clearGridData");
    window.jQuery("#SelectedListgrid").jqGrid({
        datatype: "local",
        height: 'auto',
        width: document.body.clientWidth - 20,
        rowNum: 30,
        rowList: [10, 20, 30],
        autoScroll: true,
        colModel: [
            { label: "治疗项目", name: "ItemName", width: "15%", align: "left" },
            { label: "替代项目", name: "ReplaceTtemName", width: "15%", align: "left" },
            { label: "数量", name: "ItemCount", width: "3%", align: "left" },
            { label: "单位", name: "Unit", width: "5%", align: "left" },
            { label: "单次时长", name: "SingleTimeLength", width: "9%", align: "left" },
            { label: "单价", name: "UnitPrice", width: "5%", align: "left" },
            { label: "类别", name: "Category", width: "3%", align: "left" },
            { label: "治疗师", name: "Doctor", width: "5%", align: "left" },
            { label: "记账方式", name: "jzfs", width: "10%", align: "left" },
            { label: "频次", name: "pc", width: "10%", align: "left" },
            { label: "开始时间", name: "BeginDate", width: "10%", align: "left" },
            { label: "结束时间", name: "EndDate", width: "10%", align: "left" },
            { label: "自费比例", name: "SelfPaidRatio", width: "5%", align: "left" },
            { label: "总价", name: "Total", width: "5%", align: "left" },
            { label: "备注", name: "Remark", width: "20%", align: "left" },
            { label: "No", name: "No", hidden: true },
            { label: "ItemCode", name: "ItemCode", hidden: true },
            { label: "ReplaceItemCode", name: "ReplaceItemCode", hidden: true },
            { label: "ReplaceTtemCount", name: "ReplaceTtemCount", hidden: true },
            { label: "ReplaceTtemUnit", name: "ReplaceTtemUnit", hidden: true },
            { label: "ReplaceTtemUnitPrice", name: "ReplaceTtemUnitPrice", hidden: true },
            { label: "Visit", name: "Visit", hidden: true },
            { label: "ReplaceTtemSingleTimeLength", name: "ReplaceTtemSingleTimeLength", hidden: true }
        ],
        //pager: "#pSelectedListgrid",
        //viewrecords: true,
        unwritten: false,
        sortname: 'ItemName',
        ondblClickRow: function (rowid) {
            EditSelectItem(rowid);
        }
    });
    for (var i = 0; i <= griddata.length; i++) {
        window.jQuery("#SelectedListgrid").jqGrid('addRowData', i + 1, griddata[i]);
    }
    window.jQuery("#SelectedListgrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
}

//初始化所有数据
function DefaultVal() {
    SelectedItemInfo.rowid = 0;
    SelectedItemInfo.No = "";
    SelectedItemInfo.ItemCode = "";
    SelectedItemInfo.ItemName = "";
    SelectedItemInfo.ItemCount = "";
    SelectedItemInfo.Unit = "";
    SelectedItemInfo.UnitPrice = "";
    SelectedItemInfo.SingleTimeLength = "";
    SelectedItemInfo.Category = "";
    SelectedItemInfo.Doctor = "";
    SelectedItemInfo.TreatDate = "";
    SelectedItemInfo.Visit = "";
    SelectedItemInfo.SelfPaidRatio = "";
    SelectedItemInfo.Total = "";
    SelectedItemInfo.ReplaceItemCode = "";
    SelectedItemInfo.ReplaceTtemName = "";
    SelectedItemInfo.ReplaceTtemCount = "";
    SelectedItemInfo.ReplaceTtemUnit = "";
    SelectedItemInfo.ReplaceTtemUnitPrice = "";
    SelectedItemInfo.ReplaceTtemSingleTimeLength = "";
    SelectedItemInfo.Remark = "";

    ReplaceItemsCout = 0;

    window.$("#ddlzlxm").val("");
    window.$("#txtsl").val("1");
    window.$("#txtkssj").val("");
    window.$("#txtjssj").val("");
    window.$("#jzfs").val("");
    window.$("#pc").val("");
    window.$("#txtsfbz").val("");
    window.$("#txtzfy").val("");
    window.$("#selItemType").val("");
    window.$("#txtzlsc").val("");
    window.$("#txtzls").val("");
    window.$('[name="radio_fz"]').val("0");

    NoReplaceItemDefaultVal();
    $("#sp_remainderTimesRemind").html("");
}

//被双击选中的项目数据保存在此Json中
var SelectedItemInfo = {
    rowid: 0,
    No: "",
    ItemCode: "",
    ItemName: "",
    ItemCount: "",
    Unit: "",
    UnitPrice: "",
    SingleTimeLength: "",
    Category: "",
    Doctor: "",
    TreatDate: "",
    Visit: "",
    SelfPaidRatio: "",
    Total: "",
    ReplaceItemCode: "",
    ReplaceTtemName: "",
    ReplaceTtemCount: "",
    ReplaceTtemUnit: "",
    ReplaceTtemUnitPrice: "",
    ReplaceTtemSingleTimeLength: "",
    Remark: ""
}

// 修改选中项目
function EditSelectItem(rowId) {
    DefaultVal();
    var tds = window.$("#" + rowId).find("td");
    if (tds != undefined && tds.length >= 19) {
        var val = window.$(tds[0]).text();
        var ddlval;
        switch (val) {
            case "红外线治疗":
                ddlval = "0";
                break;
            case "超声药物透入治疗":
                ddlval = "1";
                break;
            case "普通针刺（体针）":
                ddlval = "2";
                break;
            default:
                ddlval = "===请选择===";
                break;
        }

        switch (ddlval) {
            case "0":
                $("#txtsfbz").val("5元/部位");
                $("#txtzfy").val("5");
                $("#txtzlsc").val("20m");
                break;
            case "1":
                $("#txtsfbz").val("60/次");
                $("#txtzfy").val("60");
                $("#txtzlsc").val("");
                break;
            case "2":
                $("#txtsfbz").val("3/穴");
                $("#txtzfy").val("3");
                $("#txtzlsc").val("5m");
                break;
            default:
                $("#txtsfbz").val("");
                $("#txtzfy").val("");
                $("#txtzlsc").val("");
                break;
        }
        window.$("#ddlzlxm").val(ddlval);
        window.$("#txtsl").val(window.$(tds[2]).text());
        window.$("#txtzls").val(window.$(tds[4]).text());
        //window.$("#txtdj").val(window.$(tds[5]).text());
        //window.$("#txtdw").val(window.$(tds[3]).text());
        //var sfbz = window.$(tds[3]).text() + "/" + window.$(tds[5]).text();
        // window.$("#txtsfbz").val(sfbz);
        window.$("#selItemType").val(window.$(tds[6]).text());
        window.$("#txtzls").val(window.$(tds[7]).text());
        window.$("#txtzlsc").val(window.$(tds[4]).text());
        window.$("#pc").val(window.$(tds[9]).text());
        window.$("#txtkssj").val(window.$(tds[10]).text());
        window.$("#txtjssj").val(window.$(tds[11]).text());
        window.$('[name="radio_fz"]').val(window.$(tds[18]).text());

        SelectedItemInfo.rowid = rowId;
        SelectedItemInfo.No = window.$(tds[12]).text();
        SelectedItemInfo.ItemCode = window.$(tds[2]).text();
        SelectedItemInfo.ItemName = window.$(tds[0]).text();
        SelectedItemInfo.ItemCount = window.$(tds[2]).text();
        SelectedItemInfo.Unit = window.$(tds[3]).text();
        SelectedItemInfo.UnitPrice = window.$(tds[5]).text();
        SelectedItemInfo.SingleTimeLength = window.$(tds[4]).text();
        SelectedItemInfo.Category = window.$(tds[6]).text();
        SelectedItemInfo.Doctor = window.$(tds[7]).text();
        SelectedItemInfo.BeginDate = window.$(tds[10]).text();
        SelectedItemInfo.EndDate = window.$(tds[11]).text();
        SelectedItemInfo.pc = window.$(tds[9]).text();
        SelectedItemInfo.jzfs = "长期";//8
        SelectedItemInfo.Visit = window.$(tds[18]).text();
        SelectedItemInfo.SelfPaidRatio = window.$(tds[9]).text();
        SelectedItemInfo.Total = window.$(tds[10]).text();
        SelectedItemInfo.Remark = window.$(tds[11]).text();

        if (window.$(tds[1]).text() != undefined && window.$(tds[1]).text().trim().length > 0) {
            HadReplaceItemDefaultVal();
            var tval = window.$(tds[1]).text();
            var tddlval;
            switch (tval) {
                case "红外线治疗":
                    tddlval = "0";
                    break;
                case "超声药物透入治疗":
                    tddlval = "1";
                    break;
                case "普通针刺（体针）":
                    tddlval = "2";
                    break;
                default:
                    tddlval = "===请选择===";
                    break;
            }

            switch (tddlval) {
                case "0":
                    $("#txtsfbz_1").val("5元/部位");
                    $("#txtzfy_1").val("5");
                    $("#txtzlsc_1").val("20m");
                    break;
                case "1":
                    $("#txtsfbz_1").val("60/次");
                    $("#txtzfy_1").val("60");
                    $("#txtzlsc_1").val("");
                    break;
                case "2":
                    $("#txtsfbz_1").val("3/穴");
                    $("#txtzfy_1").val("3");
                    $("#txtzlsc_1").val("5m");
                    break;
                default:
                    $("#txtsfbz_1").val("");
                    $("#txtzfy_1").val("");
                    $("#txtzlsc_1").val("");
                    break;
            }
            window.$("#ddltdxm_1").val(tddlval);
            window.$("#txtsl_1").val(window.$(tds[15]).text());
            window.$("#txtzlsc_1").val(window.$(tds[19]).text());
            //window.$("#txttddj_1").val(window.$(tds[17]).text());
            //window.$("#txttddw_1").val(window.$(tds[16]).text());

            SelectedItemInfo.ReplaceItemCode = window.$(tds[14]).text();
            SelectedItemInfo.ReplaceTtemName = window.$(tds[1]).text();
            SelectedItemInfo.ReplaceTtemCount = window.$(tds[15]).text();
            SelectedItemInfo.ReplaceTtemUnit = window.$(tds[16]).text();
            SelectedItemInfo.ReplaceTtemUnitPrice = window.$(tds[17]).text();
            SelectedItemInfo.ReplaceTtemSingleTimeLength = window.$(tds[19]).text();
        }
    }
}

//获取用户输入的信息
function GetInputVal() {
    debugger;
    var items = [];
    var array = window.$("#txtsfbz").val().split("/");
    var dj = array[0];
    var dw = array[1];
    if (window.$("#tabReplaceItems").find("tr") != undefined && window.$("#tabReplaceItems").find("tr").length > 0) {
        window.$("#tabReplaceItems").find("tr").each(function (index, val) {
            var i = val.id.replace("trReplaceItem_", "");
            var iarray = window.$("#txtsfbz_" + i).val().split("/");
            var idj = iarray[0];
            var idw = iarray[1];
            var replaceitem = {
                No: (++SelectedItemCount).toString(),
                ItemCode: window.$("#ddlzlxm").val(),
                ItemName: window.$("#ddlzlxm").find("option:selected").text(),
                ItemCount: window.$("#txtsl").val(),
                Unit: dw,
                UnitPrice: dj,
                SingleTimeLength: window.$("#txtzlsc").val(),
                Category: window.$("#selItemType").val(),
                Doctor: window.$("#txtzls").val(),
                jzfs: window.$("#jzfs").val(),
                pc: window.$("#pc").val(),
                BeginDate: window.$("#txtkssj").val(),
                EndDate: window.$("#txtjssj").val(),
                Visit: window.$('[name="radio_fz"]').val(),
                SelfPaidRatio: "0.1%",
                Total: window.$("#txtzfy").val(),

                ReplaceItemCode: window.$("#ddltdxm_" + i).val(),
                ReplaceTtemName: window.$("#ddltdxm_" + i).find("option:selected").text(),
                ReplaceTtemCount: window.$("#txtsl_" + i).val(),
                ReplaceTtemUnit: idw,
                ReplaceTtemUnitPrice: idj,
                ReplaceTtemSingleTimeLength: window.$("#txtzlsc_" + i).val(),
                Remark: (window.$("#ddltdxm_" + i).find("option:selected").text() + "To" + window.$("#ddlzlxm").find("option:selected").text())
            };
            items.push(replaceitem);
        });
    } else {
        var item = {
            No: (++SelectedItemCount).toString(),
            ItemCode: window.$("#ddlzlxm").find("option:selected").val(),
            ItemName: window.$("#ddlzlxm").find("option:selected").text(),
            ItemCount: window.$("#txtsl").val(),
            Unit: dw,
            UnitPrice: dj,
            SingleTimeLength: window.$("#txtzlsc").val(),
            Category: window.$("#selItemType").val(),
            Doctor: window.$("#txtzls").val(),
            jzfs: window.$("#jzfs").val(),
            pc: window.$("#pc").val(),
            BeginDate: window.$("#txtkssj").val(),
            EndDate: window.$("#txtjssj").val(),
            Visit: window.$('[name="radio_fz"]').val(),
            SelfPaidRatio: "0.1%",
            Total: (parseFloat(window.$("#txtsfbz").val()) * parseFloat(window.$("#txtsfbz").val())),
            ReplaceItemCode: "",
            ReplaceTtemName: "",
            ReplaceTtemCount: "",
            ReplaceTtemUnit: "",
            ReplaceTtemUnitPrice: "",
            ReplaceTtemSingleTimeLength: "",
            Remark: ""
        };
        items.push(item);
    }
    return items;
}

//初始化没有替代项目控件
function NoReplaceItemDefaultVal() {
    window.$("#tabReplaceItems").find("tr").remove();
    window.$("#radio_srif").bootstrapSwitch("state", true, true);
    window.$("#dvMoreReplaceItems").hide();
}

//初始化有替代项目控件
function HadReplaceItemDefaultVal() {
    MoreReplaceItem();
    window.$("#dvMoreReplaceItems").show();
}

function ddltdxmchange(obj) {
    var id = $(obj).attr("id");
    var val = obj.value;
    var count = id.charAt(id.length - 1);
    switch (val) {
        case "0":
            $("#txtsfbz_" + count).val("5元/部位");
            $("#txtzfy_" + count).val("5");
            $("#txtzlsc_" + count).val("20m");
            break;
        case "1":
            $("#txtsfbz_" + count).val("60/次");
            $("#txtzfy_" + count).val("60");
            $("#txtzlsc_" + count).val("");
            break;
        case "2":
            $("#txtsfbz_" + count).val("3/穴");
            $("#txtzfy_" + count).val("3");
            $("#txtzlsc_" + count).val("5m");
            break;
        default:
            $("#txtsfbz_" + count).val("");
            $("#txtzfy_" + count).val("");
            $("#txtzlsc_" + count).val("");
            break;
    }
    BuildRemainderTimesMsg(val);
}

function BuildRemainderTimesMsg(itemid) {
    $("#sp_remainderTimesRemind").html("");
    var remainder = GetRemainderTimes(itemid);
    if (remainder != undefined) {
        $("#sp_remainderTimesRemind").append(remainder.name + "  医保报销还剩<span style='font-size:16px; margin-left:2px; margin-right:2px;'>" + remainder.count + "</span>次");
    }
}

//计算指定治疗项目剩余次数
function GetRemainderTimes(itemId) {
    if (RemainderTimes != undefined && RemainderTimes.length > 0) {
        for (var i = 0; i < RemainderTimes.length; i++) {
            if (RemainderTimes[i].key.toString().trim() === itemId.toString().trim()) {
                return { name: RemainderTimes[i].name, count: (RemainderTimes[i].count - GetSelectedItemCount(itemId)) };
            }
        }
    }
    return undefined;
}

//计算指定治疗项目剩余次数
function ReckonRemainderTimes() {
    if (RemainderTimes != undefined && RemainderTimes.length > 0 && griddata != undefined && griddata.length > 0) {
        for (var i = 0; i < RemainderTimes.length; i++) {
            RemainderTimes[i].count = RemainderTimes[i].count - GetSelectedItemCount(RemainderTimes[i].key);
        }
    }
}

function GetSelectedItemCount(item) {
    var ret = 0;
    if (griddata != undefined && griddata.length > 0) {
        for (var i = 0; i < griddata.length; i++) {
            if (griddata[i].ReplaceItemCode.trim() !== "" && griddata[i].ReplaceItemCode.trim() === item.trim()) {
                ret += parseInt(griddata[i].ReplaceTtemCount.trim() === "" ? 0 : griddata[i].ReplaceTtemCount.trim());
            } else if (griddata[i].ItemCode.trim() === item.trim()) {
                ret += parseInt(griddata[i].ItemCount.trim() === "" ? 0 : griddata[i].ItemCount.trim());
            }
        }
    }
    return ret;
}
