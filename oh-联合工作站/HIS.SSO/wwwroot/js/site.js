// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
if (window.isIE && window.isIE()) {
    var cssurl = '@MvcHelper.GetStaticResourceScriptUrl("~/css/framework-ui-ie.css")';
    var linkHtml = '<link href="' + cssurl + '" rel="stylesheet" />';
    document.write(linkHtml);
}
if (window.isIElte8 && window.isIElte8()) {
    var cssurl = '@MvcHelper.GetStaticResourceScriptUrl("~/css/framework-ui-ie-lte8.css")';
    var linkHtml = '<link href="' + cssurl + '" rel="stylesheet" />';
    document.write(linkHtml);
}