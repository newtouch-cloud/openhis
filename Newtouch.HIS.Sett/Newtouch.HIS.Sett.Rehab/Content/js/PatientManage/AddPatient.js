var PatientInputDto = new Object;
$(function () {
    $('#myTab a:first').tab('show');
    gridList();
});

function gridList() {
    PatientInputDto.zyh = $("#txtNo").val();
    PatientInputDto.patientName = $("#txtName").val();
    PatientInputDto.BeginDate = $("#ksrq").val();
    PatientInputDto.EndDate = $("#jsrq").val();
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        //ExpandColumn: "OrganizeId",
        postData: PatientInputDto,
        url: "/Patient/GetGridJson",
        height: $(window).height - 215,
        colModel: [
     { label: "姓名", name: "Name", width: "8%", align: "left" },
     { label: "性别", name: "Gender", width: "8%", align: "center" },
     { label: "年龄", name: "Age", width: "8%", align: "center" },
     { label: "出生日期", name: "Birthday", width: "10%", align: "center" },
     { label: "病人性质", name: "Nature", width: "8%", align: "left" },
     { label: "诊断", name: "Diagnosis", width: "20%", align: "left" },
     { label: "门诊号", name: "OutPatientNo", width: "10%", align: "center" },
     { label: "住院号", name: "InpatientNo", width: "10%", align: "center" },
     { label: "最近治疗时间", name: "TreatDate", width: "10%", align: "left" },
     { label: "自费比例", name: "LastTreatmentTime", width: "8%", align: "center" },
     { label: "备注", name: "Remark", width: "10%", align: "left" }
        ],
        pager: "#gridPager",
        viewrecords: true,
        unwritten: false,
        sortname: "ItemName",
        ondblClickRow: function (rowid) {
            EditSelectedItem(rowid);
        }
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: PatientInputDto
        }).trigger('reloadGrid');
    })
}

function checkNotNull() {
    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            mzh: { required: true },
            xm: { required: true },
            brxz: { required: true },
            nl: { required: true },
            hf: { required: true },
            zjh: { isIdentity: true },
            email: { email: true },
            phone: { isMobile: true },
            dh: { isPhone: true },
            jjlldh: { isPhone: true }
        },
        messages: {
            mzh: { required: "门诊号必须填写" },
            xm: { required: "姓名必须填写" },
            brxz: { required: "病人性质必须填写" },
            nl: { required: "年龄必须填写" },
            hf: { required: "婚否必须选择" },
            blh: { required: "婚否必须选择" },
            zjh: { isIdentity: "证件格式不正确" },
            email: { email: "邮箱格式不正确" },
            phone: { isMobile: "手机格式不正确" },
            dh: { isPhone: "电话格式不正确" },
            jjlldh: { isPhone: "电话格式不正确" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                $.modalAlert(errorList[0].message, 'warning');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }
    //病历号
    var blh = $("#blh").val();
    if (!blh) {
        $.modalAlert("病历号为空！", 'warning');
        return false;
    }
    //拼音
    var py = $("#py").val();
    if (!py) {
        $.modalAlert("拼音为空！", 'warning');
        return false;
    }
    //性别
    var xb = false;
    $('input[name="xb"]').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("active")) {
            xb = true;
            $("#xb").val();
        }
    });
    if (!xb) {
        $.modalAlert("请选中性别！", 'warning');
        return false;
    }
    return true;
}

//*****************************  以下为患者查询功能  **********************************//

var gridData = [];
//function GetPatientsInfo() {
//    for (var i = 0; i <= 10; i++) {
//        var item = {
//            Name: "张三",
//            Gender: "男",
//            Age: "26",
//            Birthday: "1989-06-21",
//            Nature: "自费",
//            Diagnosis: "骨折",
//            OutPatientNo: "10001",
//            InpatientNo: "",
//            LastTreatmentTime: "2017-04-23",
//            Remark: ""
//        }
//        gridData.push(item);
//    }
//}

//function BindPatientsData() {
//    window.jQuery("#PatientListGrid").jqGrid("clearGridData");
//    window.jQuery("#PatientListGrid").jqGrid({
//        //datatype: "local",
//        height: 300,
//        width: document.body.clientWidth - 20,
//        rowNum: 30,
//        rowList: [10, 20, 30],
//        autoScroll: true,
//        colModel: [
//            { label: "姓名", name: "Name", width: "8%", align: "left" },
//            { label: "性别", name: "Gender", width: "8%", align: "center" },
//            { label: "年龄", name: "Age", width: "8%", align: "center" },
//            { label: "出生日期", name: "Birthday", width: "10%", align: "center" },
//            { label: "病人性质", name: "Nature", width: "8%", align: "left" },
//            { label: "诊断", name: "Diagnosis", width: "20%", align: "left" },
//            { label: "门诊号", name: "OutPatientNo", width: "10%", align: "center" },
//            { label: "住院号", name: "InpatientNo", width: "10%", align: "center" },
//            { label: "最近治疗时间", name: "TreatDate", width: "10%", align: "left" },
//            { label: "自费比例", name: "LastTreatmentTime", width: "8%", align: "center" },
//            { label: "备注", name: "Remark", width: "10%", align: "left" }
//        ],
//        pager: "#PagePatientListGrid",
//        viewrecords: true,
//        unwritten: false,
//        sortname: "ItemName",
//        ondblClickRow: function (rowid) {
//            EditSelectedItem(rowid);
//        }
//    });
//    //for (var i = 0; i <= gridData.length; i++) {
//    //    window.jQuery("#PatientListGrid").jqGrid("addRowData", i + 1, gridData[i]);
//    //}
//    window.jQuery("#PatientListGrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
//}

function EditSelectedItem(rowId) {
    $.modalOpen({
        id: "Form",
        title: "修改患者信息",
        url: "/Patient/AddPatient",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function AddNewPatient() {
    $.modalOpen({
        id: "Form",
        title: "新增患者",
        url: "/Patient/AddPatient",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}