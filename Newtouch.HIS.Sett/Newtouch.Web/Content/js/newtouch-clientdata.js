var newtouchclients = [];
$(function () {
    return;
    newtouchclients = $.newtouchclientsInit();
})
$.newtouchclientsInit = function () {
    var dataJson = {
    };
    var init = function () {
        $.ajax({
            url: "/NewtouchClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: true,
            cache: false,
            success: function (data) {
                dataJson.sysPatientNatureList = data.sysPatientNatureList;//病人性质，报销政策
                //dataJson.sysCashPay = data.sysCashPay;
                dataJson.SysForCashPayList = data.SysForCashPayList;
                //dataJson.sysFieldList = data.sysFieldList;
                dataJson.sysStaffList = eval(data.sysStaffList);//人员
                dataJson.sysDepartList = eval(data.sysDepartList),//科室
                dataJson.sysPatiAreaList = eval(data.sysPatiAreaList),//病区
                dataJson.sysNationalityList = eval(data.sysNationalityList),//国籍
                dataJson.sysNationList = eval(data.sysNationList),//民族
                //dataJson.sysChargeItemList = data.sysChargeItemList,//住院记账收费项目
                dataJson.doctorInHosBookkeep = data.doctorInHosBookkeep//住院记账门诊医生
                dataJson.sysMajorClassList = eval(data.sysMajorClassList);//大类
                //dataJson.sysChargeItemList = data.sysChargeItemList;//收费项目
                //dataJson.sysChargeClassifyList = data.sysChargeClassifyList;//收费分类
                //dataJson.syszlxmList = data.syszlxmList;//诊疗项目
                dataJson.sysnbsfdlList = eval(data.sysnbsfdlList);//农保收费大类
                dataJson.sysbasfdlList = eval(data.sysbasfdlList);//病案收费大类
                dataJson.sysStaffDutyList = eval(data.sysStaffDutyList);//医生，健康教练
                //dataJson.regScheduleList = data.regScheduleList//挂号排班
                //GetDYList
                //dataJson.organize = data.organize;
                //dataJson.role = data.role;
                //dataJson.duty = data.duty;
                //dataJson.authorizeMenu = eval(data.authorizeMenu);
                //dataJson.authorizeButton = data.authorizeButton;
                dataJson.commercialInsuranceList = data.commercialInsuranceList;//商保
            }
        });
    }
    init();
    return dataJson;
}