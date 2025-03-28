
function Search() {

}

//患者登记
function AddNewPatient() {
    $.modalOpen({
        id: "Form",
        title: "患者登记",
        url: "/Patient/AddPatient",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

//保存项目
function AddItem() {
    BindItemData();
}

//修改项目
function EditItem() {

}


function CleanDefaultValue(v) {
    if (v === "门诊号/身份证号/姓名") {
        $("#txtSearchInfo").val("");
    }
}
