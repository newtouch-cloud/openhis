//诊断
$.fn.zdFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/SysBaseData/GetDiagnosisList',
        width: 300,
        height: 200,
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
            { label: '名称', name: 'zdmc', widthratio: 50 },
            { label: '拼音', name: 'py', widthratio: 25 },
            { label: 'icd10', name: 'icd10', widthratio: 25 },
            { label: 'icd10附加码', name: 'icd10fjm', hidden: true }
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}

//手术名称
$.fn.ssmcFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/SysBaseData/GetOperationList',
        width: 400,
        height: 200,
        clickautotrigger: true,
        caption: "选择手术",
        ajaxparameters: function ($thisinput) {
            //return "organizeId=" + options.organizeId + "&keyword=" + $thisinput.val();

			return "keyword=" + $thisinput.val() + "&type=true";
        },
        itemdbclickhandler: null,
        colModel: [
            { label: '代码', name: 'id', hidden: true },
            { label: '手术名称', name: 'ssmc', widthratio: 60 },
            { label: '助记码', name: 'zjm', widthratio: 20 },
            { label: '手术代码', name: 'ssdm', widthratio: 20 }
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}


//医生人员
$.fn.staffFloatingSelector = function (options) {
	var defaults = {
		url: '/SystemManage/SysBaseData/GetFloatStaffList',
		width: 200,
		height: 200,
		clickautotrigger: true,
		caption: "选择医生",
		ajaxparameters: function ($thisinput) {
			//if (!!!options.ybnhlx) {
			//    return "keyword=" + $thisinput.val() + "&zdlx=" + options.zdlx;
			//} else {
			//    return "keyword=" + $thisinput.val() + "&zdlx=" + options.zdlx + "&ybnhlx=" + options.ybnhlx;
			//}
			return "organizeId=" + options.organizeId + "&keyword=" + $thisinput.val();

		},
		itemdbclickhandler: null,
		colModel: [
			{ label: '代码', name: 'id', hidden: true },
			{ label: '医生姓名', name: 'Name', widthratio: 40 },
			{ label: '助记码', name: 'zjm', widthratio: 30 },
			{ label: '工号', name: 'Code', widthratio: 30 }
		]
	};
	var options = $.extend(defaults, options);

	$(this).newtouchFloatingSelector(options);
}


//收费项目 浮层（包括药品、项目，也可以非治疗项目）
$.fn.sfxmFloatingSelector = function (options) {
    //默认options
    var defaults = {
        width: 830,
        height: 200,
        clickautotrigger: true,
        url: '/SystemManage/SysChargeTemplate/GetDicChargeItems',
        ajaxparameters: null,   //请指定
        itemdbclickhandler: null,   //请指定
        djDecimalPlaces: 2,  //单价精度 默认2
        searchType: null,   //仅控制列显隐，不参与筛选//检索目标 "yp,sfxm"、"yp"、"sfxm"、"yp.kc"、"sfxm.dwjls"、"yp,sfxm.dwjls"、"yp.kc,sfxm"、"yp.kc,sfxm.dwjls"
        showColModel: null,   //可指定 [{name:;widthratio:;},{name:;widthratio:;}] widthratio可选
        ypkccbjc: true,    //药品库存初步检查   是否被控制 是否库存0
        colModel: [
            { label: '代码', name: 'sfxmCode', widthratio: 14 },
            { label: '名称', name: 'sfxmmc', widthratio: 25 },
            {
                label: '规格', name: 'gg', widthratio: 10
                , hidden: true
            },
            { label: '医嘱类型', name: 'yzlx', hidden: true },
            { label: '首拼', name: 'py', widthratio: 12 },
            { label: '收费大类', name: 'sfdlCode', hidden: true },
            {
                label: '收费大类', name: 'sfdlmc'
                , widthratio: 10
                //, hidden: true
            },
            { label: '单位', name: 'dw', widthratio: 6 },
            {
                label: '单价', name: 'dj', widthratio: 8, formatter: function (cellvalue) {
                    return (!!cellvalue || cellvalue == 0) ? (parseFloat(cellvalue).toFixed(options.djDecimalPlaces ? options.djDecimalPlaces : 2)) : '';
                }
            },
            { label: '计价策略', name: 'jjcl', hidden: true },
            {
                label: '计量数', name: 'dwjls', widthratio: 8, formatter: function (cellvalue) {
                    return !!cellvalue ? cellvalue : '';
                }
                , hidden: true
            },
            { label: '报销政策', name: 'zfxz', hidden: true },
            {
                label: '报销政策', name: 'zfxzmc', widthratio: 10, formatter: function (cellvalue, a, b) {
                    return $.enum.getDescByValue("EnumZiFuXingZhi", b.zfxz);
                }
            },
            { label: 'zfbl', name: 'zfbl', hidden: true },
            { label: 'ypjxCode', name: 'ypjxCode', hidden: true },
            { label: 'duration', name: 'duration', hidden: true },
            { label: 'jldw', name: 'jldw', hidden: true },
            { label: 'jldwzhxs', name: 'jldwzhxs', hidden: true },
            { label: 'cls', name: 'cls', hidden: true },
            { label: 'zyqzlx', name: 'zyqzlx', hidden: true },
            { label: 'zxks', name: 'zxks', hidden: true },
            { label: 'zxksmc', name: 'zxksmc', hidden: true },
            { label: 'ybdm', name: 'ybdm', hidden: true },
            { label: 'xzyy', name: 'xzyy', hidden: true },
            { label: 'xzyysm', name: 'xzyysm', hidden: true },
            { label: 'yfbmCode', name: 'yfbmCode', hidden: true },
            {
                label: '药房', name: 'yfbmmc', formatter: function (cellvalue, a, b) {
                    return b.yzlx === '1' && !!cellvalue ? cellvalue : '';
                }, widthratio: 10, hidden: true
            },
            {
                label: '库存', name: 'kcsl', formatter: function (cellvalue, a, b) {
                    return b.yzlx === '1' && !!cellvalue ? cellvalue : '';
                }, widthratio: 6, hidden: true
            },
            {
                label: '库存', name: 'clkcsl', formatter: function (cellvalue, a, b) {
                    //给的kcsl是最小单位的，这里要显示门诊/住院单位的数量
                    return b.yzlx === '1' ? (!!b.kcsl && !!b.cls ? (parseInt(b.kcsl / b.cls)) : '无') : '';
                }, widthratio: 6, hidden: true
            },
            {
                label: '控制', name: 'kzbz', formatter: function (cellvalue, a, b) {
                    return cellvalue == '1' ? '控制' : '';
                }, widthratio: 6, hidden: true
            },
            { label: '备注', name: 'bz', hidden: true },
        ]
    };

    var options = $.extend(defaults, options);

    if (!!!options.searchType) {
        options.searchType = "yp,sfxm"; //默认
    }
    if (options.searchType && !!!options.showColModel) {
        var colsfxmmc = null;
        var colgg = null;
        var colsfdlmc = null;
        var coldwjls = null;
        var colyfbmmc = null;
        var colclkcsl = null;
        var colkzbz = null;
        $.each(options.colModel, function () {
            if (this.name === 'sfxmmc') {
                colsfxmmc = this;
            }
            else if (this.name === 'gg') {
                colgg = this;
            }
            else if (this.name === 'sfdlmc') {
                colsfdlmc = this;
            }
            else if (this.name === 'dwjls') {
                coldwjls = this;
            }
            else if (this.name === 'yfbmmc') {
                colyfbmmc = this;
            }
            else if (this.name === 'clkcsl') {
                colclkcsl = this;
            }
            else if (this.name === 'kzbz') {
                colkzbz = this;
            }
        });
        if (!!colsfxmmc && !!colgg && !!colsfdlmc && !!coldwjls && !!colyfbmmc && !!colclkcsl && !!colkzbz) {
            //已被占用50宽
            switch (options.searchType) {
                case "yp":
                    colsfxmmc.widthratio = 23;
                    colgg.hidden = undefined;
                    colgg.widthratio = 12;
                    colsfdlmc.hidden = undefined;
                    colsfdlmc.widthratio = 12;
                    break;
                case "sfxm":
                    colgg.hidden = undefined;
                    colgg.widthratio = 10;
                    colsfxmmc.widthratio = 20;
                    colsfdlmc.hidden = undefined;
                    colsfdlmc.widthratio = 12;
                    break;
                case "yp,sfxm":
                case "sfxm,yp":
                    colsfxmmc.widthratio = 30;
                    colgg.hidden = undefined;
                    colgg.widthratio = 15;
                    break;
                case "yp.kc":
                case "yp.kc,sfxm":
                case "sfxm,yp.kc":
                    colsfxmmc.widthratio = 19;
                    colgg.hidden = undefined;
                    colgg.widthratio = 11;
                    colyfbmmc.hidden = undefined;
                    colyfbmmc.widthratio = 7;
                    colclkcsl.hidden = undefined;
                    colclkcsl.widthratio = 5;
                    colkzbz.hidden = undefined;
                    colkzbz.widthratio = 6;
                    break;
                case "sfxm.dwjls":
                    colgg.hidden = undefined;
                    colgg.widthratio = 10;
                    colsfxmmc.widthratio = 25;
                    coldwjls.hidden = undefined;
                    coldwjls.widthratio = 10;
                    break;
                case "yp,sfxm.dwjls":
                case "sfxm.dwjls,yp":
                    colsfxmmc.widthratio = 25;
                    colgg.hidden = undefined;
                    colgg.widthratio = 15;
                    coldwjls.hidden = undefined;
                    coldwjls.widthratio = 8;
                    break;
                case "yp.kc,sfxm.dwjls":
                case "sfxm.dwjls,yp.kc":
                    colsfxmmc.widthratio = 16;
                    colgg.hidden = undefined;
                    colgg.widthratio = 9;
                    colyfbmmc.hidden = undefined;
                    colyfbmmc.widthratio = 7;
                    colclkcsl.hidden = undefined;
                    colclkcsl.widthratio = 5;
                    colkzbz.hidden = undefined;
                    colkzbz.widthratio = 5;
                    coldwjls.hidden = undefined;
                    coldwjls.widthratio = 6;
                    break;
                default:
                    break;
            }
        }
    }

    if (options.showColModel) {
        //[{name:;widthratio:;},{name:;widthratio:;}] widthratio可选
        $.each(options.colModel, function () {
            var thisCol = this;
            var matchedShow = $.jsonWhere(options.showColModel, function (v) {
                return v.name == thisCol.name;
            });
            if (matchedShow && matchedShow.length) {
                thisCol.hidden = undefined;
                if (matchedShow[0].widthratio) {
                    thisCol.widthratio = matchedShow[0].widthratio;
                }
            }
            else {
                thisCol.hidden = true;
            }
        });
    }

    if (options.searchType.indexOf('yp.kc') >= 0 && options.ypkccbjc === true) {
        options.itemdbclickhandleBefore = function ($iiithis) {
            if ($iiithis.attr('data-yzlx') == '1') {
                if ((!isNaN($iiithis.attr('data-clkcsl')) && parseInt($iiithis.attr('data-clkcsl')) <= 0) || $iiithis.attr('data-clkcsl') == '无') {
                    $.modalAlert("不能开立无库存药品", 'warning');
                    return false;
                }
                if ($iiithis.attr('data-kzbz') == '控制') {
                    $.modalAlert("不能开立控制药品", 'warning');
                    return false;
                }
            }
            return;
        }
    }

    $(this).newtouchFloatingSelector(options);
}

