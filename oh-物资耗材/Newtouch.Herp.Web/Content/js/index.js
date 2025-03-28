function KfChange() {
    $.modalOpen({
        id: "Form",
        title: "库房切换",
        url: "/Home/KfChange",
        width: "500px",
        height: "400px",
        callBack: function (iframeId) {
            $.currentWindow(iframeId).AcceptClick(function () {

            });
        }
    });
}

//获取两个日期之间的天数差
function getDaysBetween(date1, date2) {
    const ONE_DAY = 1000 * 60 * 60 * 24; // 一天的毫秒数
    const date1Time = date1.getTime(); // 获取时间戳
    const date2Time = date2.getTime();

    const difference = Math.abs(date1Time - date2Time); // 获取时间差
    return Math.round(difference / ONE_DAY); // 两个日期之间的天数
}