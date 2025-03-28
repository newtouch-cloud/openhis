//药品名称
$.fn.ypFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        width: 600,
        height: 300,
        caption: "药品名称",
        url: '/SysMedicine/GetFloatingYPXXJson',
        ajaxparameters: function ($thistr) {
            return 'organizeId=' + $.trim($('#OrganizeId').val()) + '&keyword=' + $thistr;
        },
        itemdbclickhandler: null,
        colModel: [
            { label: '名称', name: 'ypmc', widthratio: 25 },
            { label: '医保代码', name: 'ybdm', widthratio: 20 },
            { label: '规格', name: 'gg', widthratio: 20 },
            { label: '药厂名称', name: 'ycmc', widthratio: 25 },
            { label: '药品性质', name: 'ypxz', widthratio: 25 },
            { label: '拼音', name: 'py', hidden: true },
            { label: '药品性质', name: 'ybxz', widthratio: 25, hidden: true }
        ]
    });
}

//药品分类
$.fn.ypflFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector( {
        url: '/PharmacyDrugStorage/SysMedicineClassification/GetListSelectJson',
        width: 300,
        height: 300,
        clickautotrigger: true,
        caption: "药品分类",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput;
        },
        itemdbclickhandler: null,
        colModel: [
            { label: '编码', name: 'ypflCode', hidden: true },
            { label: '名称', name: 'ypflmc', widthratio: 50 },
            {
                label: '首拼', name: 'py', widthratio: 50, formatter: function (val) {
                    return $.trim(val);
                }
            }
        ]
    });
}