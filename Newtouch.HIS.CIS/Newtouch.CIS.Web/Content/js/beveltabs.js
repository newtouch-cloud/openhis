function resetbeveltabs() {
    $("#treeContent > div").hide(); //Hide all content
    $("#beveltabs a").attr("id", ""); //Reset id's      
}
var myUrl = window.location.href; //get URL
var myUrlTab = myUrl.substring(myUrl.indexOf("#"));
var myUrlTabName = myUrlTab.substring(0, 4);
(function () {
    $("#treeContent > div").hide(); // Initially hide all content
    $("#beveltabs li:first a").attr("id", "current"); // Activate first tab
    $("#treeContent > div:first").fadeIn(); // Show first tab content

    $("#beveltabs a").on("click", function (e) {
        e.preventDefault();
        if ($(this).attr("id") == "current") { //detection for current tab
            return
        }
        else {
            resetbeveltabs();
            $(this).attr("id", "current"); // Activate this
            $($(this).attr('name')).fadeIn(); // Show content for current tab
        }
    });
    for (i = 1; i <= $("#beveltabs li").length; i++) {
        if (myUrlTab == myUrlTabName + i) {
            resetbeveltabs();
            $("a[name='" + myUrlTab + "']").attr("id", "current"); // Activate url tab
            $(myUrlTab).fadeIn(); // Show url tab content        
        }
    }
})(jQuery)
