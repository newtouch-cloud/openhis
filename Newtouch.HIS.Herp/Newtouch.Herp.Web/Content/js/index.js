function KfChange() {
    $.modalOpen({
        id: "Form",
        title: "�ⷿ�л�",
        url: "/Home/KfChange",
        width: "500px",
        height: "400px",
        callBack: function (iframeId) {
            $.currentWindow(iframeId).AcceptClick(function () {

            });
        }
    });
}

//��ȡ��������֮���������
function getDaysBetween(date1, date2) {
    const ONE_DAY = 1000 * 60 * 60 * 24; // һ��ĺ�����
    const date1Time = date1.getTime(); // ��ȡʱ���
    const date2Time = date2.getTime();

    const difference = Math.abs(date1Time - date2Time); // ��ȡʱ���
    return Math.round(difference / ONE_DAY); // ��������֮�������
}