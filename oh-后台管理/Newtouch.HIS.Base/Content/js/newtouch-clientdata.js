var newtouchclients = [];
$(function () {
    //newtouchclients = $.newtouchclientsInit();
})
$.newtouchclientsInit = function () {
    var dataJson = {
    };
    var init = function () {
        $.ajax({
            url: "/NewtouchClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                dataJson.sysPatientNatureList = data.sysPatientNatureList;
                dataJson.sysCashPay = data.sysCashPay;
                dataJson.SysForCashPayList = data.SysForCashPayList;
                dataJson.sysFieldList = data.sysFieldList;
                dataJson.sysStaffList = data.sysStaffList;//人员
                dataJson.sysDepartList = data.sysDepartList,//科室
                dataJson.sysPatiAreaList = data.sysPatiAreaList,//病区
                dataJson.sysNationalityList = data.sysNationalityList,//国籍
                dataJson.sysNationList = data.sysNationList,//民族
                //dataJson.sysChargeItemList = data.sysChargeItemList,//住院记账收费项目
                dataJson.doctorInHosBookkeep = data.doctorInHosBookkeep//住院记账门诊医生
                //dataJson.regScheduleList = data.regScheduleList//挂号排班
                //GetDYList
                //dataJson.organize = data.organize;
                //dataJson.role = data.role;
                //dataJson.duty = data.duty;
                //dataJson.authorizeMenu = eval(data.authorizeMenu);
                //dataJson.authorizeButton = data.authorizeButton;
                dataJson.SysFailedCodeMessageMapList = data.SysFailedCodeMessageMapList;//FailedException错误提示映射关系
            }
        });
    }
    init();
    return dataJson;
}