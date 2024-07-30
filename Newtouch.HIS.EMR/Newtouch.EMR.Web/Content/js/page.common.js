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
        width: 450,
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
            { label: '手术级别', name: 'ssjb', widthratio: 18 },
        ],
        rowNum: 100,
        rownumbers: true,
        rownumWidth: 40,
        gridview: true,
        pager: '#pscrolling',
        sortname: 'ssmc desc',
        viewrecords: false,
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
            { label: '医生名称', name: 'StaffName', widthratio: 38 },
            { label: '助记码', name: 'StaffPY', widthratio: 20 },
            { label: '科室名称', name: 'ksmc', widthratio: 40 },
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

//职业选择器
$.fn.ZYFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/ZYgetlist',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: '编码', name: 'Code', widthratio: 30 },
            { label: '职业名称', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//关系选择器
$.fn.GXFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GXgetlist',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: '编码', name: 'Code', widthratio: 30 },
            { label: '关系名称', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}

//病历大类子目录
$.fn.BllxFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetBllxListGrid',
        width: 300,
        height: 200,
        treeGrid: true,
        treeGridModel: "adjacency",
        ExpandColumn: "Code",
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val() + "&bllx=" + options.code;
        },
        colModel: [
            { label: '病历目录', name: 'Name', widthratio: 50 },
            { label: '类别编码', name: 'Code', widthratio: 50 },
            //{ label: "简码", name: "Id", width: 20, align: 'center', },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //保存时验证val和data-Name一致
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}

//病历元素选择
$.fn.ElementFloatingSelector = function (options) {
    var defaults = {
        url: '/MedicalRecordManage/Bljgh/GetYsList',
        width: 550,
        height: 200,
        clickautotrigger: true,
        caption: "选择诊断",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        itemdbclickhandler: null,
        colModel: [

             { label: 'ysId', name: 'ysId', widthratio: 70, hidden: true },
             { label: '元素代码', name: 'ysdm', widthratio: 15 },
             { label: '元素名称', name: 'ysmc', widthratio: 40 },
             { label: '元素类型', name: 'yslx', widthratio: 70, hidden: true },
             { label: '元素类型名称', name: 'yslxmc', widthratio: 20 },
             { label: '父级元素', name: 'ysdl', widthratio: 25 },
             { label: 'BindingPath', name: 'BindingPath', widthratio: 30, hidden: true }
        ],
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}
//病历表字段选择
$.fn.TabColumnFloatingSelector = function (options) {
    var defaults = {
        url: '/MedicalRecordManage/Bljgh/GetTableColumnList',
        width: 350,
        height: 200,
        clickautotrigger: true,
        caption: "选择字段",
        ajaxparameters: function ($thisinput) {
            return "TableName=" + options.TableName + "&keyword=" + $thisinput.val();
        },
        itemdbclickhandler: null,
        colModel: [
             { label: '序号', name: 'column_id', widthratio: 20 },
             { label: '字段', name: 'columnName', widthratio: 50 },
             { label: '字段类型', name: 'columnType', widthratio: 20 },
             { label: '最大长度', name: 'max_length', widthratio: 30, hidden: true },
             { label: '小数位', name: 'scale', widthratio: 70, hidden: true }
        ],
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}
//病历元素明细选择
$.fn.ElementMxFloatingSelector = function (options) {
    var defaults = {
        url: '/MedicalRecordManage/Bljgh/GetElementMx',
        width: 350,
        height: 200,
        clickautotrigger: true,
        caption: "选择元素值",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + options.ysId;
        },
        itemdbclickhandler: null,
        colModel: [
             { label: 'ysId', name: 'ysId', widthratio: 20, hidden: true },
             { label: '代码', name: 'ysmxcode', widthratio: 40 },
             { label: '值', name: 'ysmxName', widthratio: 60}
        ],
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}
//病历文书浮层
$.fn.BindBlTableFloatingSelector = function (options) {
    var defaults = {
        url: '/MedicalRecordManage/Bljgh/GetBlTable',
        width: 350,
        height: 200,
        clickautotrigger: true,
        caption: "选择元素值",
        ajaxparameters: function ($thisinput) {
            return "keyword=" +$thisinput.val();
        },
        itemdbclickhandler: null,
        colModel: [
             { label: 'bllx', name: 'bllx', widthratio: 30,hidden:true },
             { label: '病历名称', name: 'bllxmc', widthratio: 50 },
             { label: '表名', name: 'relTB', widthratio: 50 }
        ],
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}