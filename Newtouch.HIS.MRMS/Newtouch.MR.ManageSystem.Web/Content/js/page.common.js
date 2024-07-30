//字典
$.fn.dicCommonFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetCommonList',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val() + "&type=" + options.rule;
        },
        colModel: [
            { label: '编码', name: 'Code', widthratio: 30 },
            { label: '名称', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//科室
$.fn.departmentFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetDepartment',
        width: 300,
        height: 200,
        clickautotrigger: true,
        caption: "选择科室",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: '科室代码', name: 'Code', widthratio: 40 },
            { label: '科室名称', name: 'Name', widthratio: 60 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//诊断
$.fn.zdFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/Common/GetDiagnosisList',
        width: 600,
        height: 300,
        clickautotrigger: true,
        caption: "选择诊断",
        ajaxparameters: function ($thisinput) {
            if (!!!options.ybnhlx) {
                return "keyword=" + $thisinput.val() + "&zdlx=" + options.zdlx;
            } else {
                return "keyword=" + $thisinput.val() + "&zdlx=" + options.zdlx + "&ybnhlx=" + options.ybnhlx;
            }

        },
        itemdbclickhandler: null,
        colModel: [
            { label: '代码', name: 'zdCode', hidden: true },
            { label: '名称', name: 'zdmc', widthratio: 60 },
            { label: '拼音', name: 'py', widthratio: 20 },
            { label: 'icd10', name: 'icd10', widthratio: 20 },
            { label: 'icd10附加码', name: 'icd10fjm', hidden: true }
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}

//手术
$.fn.opFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/Common/GetOperationList',
        width: 600,
        height: 300,
        clickautotrigger: true,
        caption: "选择手术",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val() + "&type=true";
        },
        itemdbclickhandler: null,
        colModel: [
            { label: '代码', name: 'ssdm', hidden: true },
            { label: '名称', name: 'ssmc', widthratio: 60 },
            { label: '助记码', name: 'zjm', widthratio: 20 },
            { label: '手术级别', name: 'ssjb', widthratio: 20 },
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}

//岗位人员选择器
$.fn.dutyStaffFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetStaffListByDutyCode',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "dutyCode=" + options.dutyCode + "&keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'StaffGh', name: 'StaffGh', hidden: true },
            { label: 'ks', name: 'ks', hidden: true },
            { label: '医生名称', name: 'StaffName', widthratio: 48 },
            { label: '科室名称', name: 'ksmc', widthratio: 50 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('data-StaffGh', $thistr.attr('data-StaffGh'));
            $thisinput.attr('data-ks', $thistr.attr('data-ks'));
            $thisinput.attr('data-ksmc', $thistr.attr('data-ksmc'));
            $thisinput.val($thistr.attr('data-StaffName'));
        }
    });
}

//国籍选择器
$.fn.NationalityFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetNationality',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: '国家代码', name: 'Code', widthratio: 30 },
            { label: '国家', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//民族选择器
$.fn.NationsFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetNationas',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: '民族代码', name: 'Code', widthratio: 30 },
            { label: '民族', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//收费大类选择器
$.fn.hissfdlFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetHisSfdl',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: '收费大类', name: 'dlcode', widthratio: 30 },
            { label: '大类名称', name: 'dlmc', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('attr-dlcode', $thistr.attr('data-dlcode'));
            $thisinput.val($thistr.attr('data-dlmc'));
        }
    });
}

