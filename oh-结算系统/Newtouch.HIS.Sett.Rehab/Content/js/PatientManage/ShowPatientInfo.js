$("#imoretag").click(function () {
    $(".MorePatientDetail").fadeToggle("slow");
    if ($("#imoretag").attr("tag") === "s") {
        $("#imoretag").attr("tag", "c");
        $("#imoretag").removeClass("fa-angle-double-down");
        $("#imoretag").addClass("fa-angle-double-up");
    } else {
        $("#imoretag").attr("tag", "s");
        $("#imoretag").removeClass("fa-angle-double-up");
        $("#imoretag").addClass("fa-angle-double-down");
    }
});