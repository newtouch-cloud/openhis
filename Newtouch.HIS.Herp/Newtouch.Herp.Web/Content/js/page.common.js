//禁用软件  1：禁用  2：启用
function readonly(list, type) {
    if (list && list.length > 0) {
        for (var i = 0; i < list.length; i++) {
            if (type === 1) {//禁用
                $("#" + list[i]["id"]).addClass('newtouch_Readonly').attr("disabled", "disabled").css("background-color", "rgb(238,238,238)");
            } else {//启用
                $("#" + list[i]["id"]).removeClass("newtouch_Readonly").removeAttr("disabled").css("background-color", "");
            }

        }
    }
}