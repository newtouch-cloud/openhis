var keyValue = $.request("keyValue");
$(function () {
    initControl();
    //获取最新卡号
    $.ajax({
        url: "/PatientManage/HospiterRes/GetCardNoAndBLH?r=" + Math.random(),
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            $.currentWindow().$("#kh").val(data.kh);
        }
    });
    $('#nl').val("");
    $('#mz').val("");
    $('#gj').val("");
    $("#hf").val("");
    $('#myTab a:first').tab('show');
    //根据姓名获得拼音
    $('#xm').blur(function () {
        $('#py').val($(this).toPinyin());

    });

    $('#zjh').bind("keydown blur", function (e) {
        if ($('#zjlx').val() == "0") {
            //获取输入的身份证号
            var sfzh = $(this).val();
            var csrq;
            if (sfzh.length == 15) {
                csrq = "19" + sfzh.substr(6, 6);
            } else if (sfzh.length == 18) {
                csrq = sfzh.substr(6, 8);
            }
            if ((sfzh.length == 15 && !csrq) || sfzh.length == 18) {
                csrq = csrq.replace(/(.{4})(.{2})/, "$1-$2-");
                //获取性别
                var xb;
                if (parseInt(sfzh.substr(16, 1) % 2 == 1)) {
                    xb = 1;
                } else {
                    xb = 0;
                }
                //获取年龄
                var myDate = new Date();
                var month = myDate.getMonth() + 1;
                var day = myDate.getDate();
                var age = myDate.getFullYear() - sfzh.substring(6, 10) - 1;
                if (sfzh.substring(10, 12) < month || sfzh.substring(10, 12) == month && sfzh.substring(12, 14) <= day) {
                    age++;
                }
                $('#zjh').val(sfzh);
                $('#csny').val(csrq);
                //$('input[name="xb"]:checked').parent().remove('active');
                if (xb == 0) {
                    //  $("input[name=xb]:eq(0)").removeAttr("checked");
                    if ($("input[name=xb]:eq(0)").parent().hasClass('active')) {
                        $("input[name=xb]:eq(0)").parent().removeClass('active');
                    }
                    $("input[name=xb]:eq(1)").attr("checked", 'checked');//女
                    $("input[name=xb]:eq(1)").parent().addClass('active');
                } else {
                    // $("input[name=xb]:eq(1)").removeAttr("checked");
                    if ($("input[name=xb]:eq(1)").parent().hasClass('active')) {
                        $("input[name=xb]:eq(1)").parent().removeClass('active');
                    }
                    $("input[name=xb]:eq(0)").attr("checked", 'checked');//男
                    $("input[name=xb]:eq(0)").parent().addClass('active');
                }
                $('#nl').val(age);
            }
        }
    });
})

function getAge(date) {
    var aDate = new Date();
    var thisYear = aDate.getFullYear();
    var brith = date;
    brith = brith.substr(0, 4);
    age = (thisYear - brith);
    $("#nl").val(age);
}

function initControl() {
    ///报销政策
    $("#brxz").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //遍历数据源，用keyword来筛选出结果
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysPatientNatureList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "病人性质",
        colModel: [
            { label: '编号', name: 'brxzbh', widthratio: 25 },
            { label: '代码', name: 'brxz', widthratio: 25 },
            { label: '名称', name: 'brxzmc', widthratio: 25 },
            { label: '拼音', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#brxz").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
            $("#brxz").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
    //地域
    //$("#dy").newtouchBindSelect({
    //    datasource: function () {
    //        var resultObjArr = new Array();
    //        if (top.clients.dataItems) {
    //            $.each(top.clients.dataItems, function (idx, val) {
    //                if (idx == "AreaCity") {
    //                    $.each(val, function (key, value) {
    //                        $('#dy').append('<option value="' + key + '">' + value + '</option>');
    //                    });
    //                }
    //            });
    //        }
    //        return resultObjArr;
    //    }
    //});
    //学历
    $("#xl").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            if (top.clients.dataItems) {
                $.each(top.clients.dataItems, function (idx, val) {
                    if (idx == "Education") {
                        $.each(val, function (key, value) {
                            $('#xl').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                });
            }
            return resultObjArr;
        }
    });
    //国籍
    $("#gj2").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            if (top.clients.dataItems) {
                $.each(top.clients.dataItems, function (idx, val) {
                    if (idx == "Nationality") {
                        $.each(val, function (key, value) {
                            $('#gj2').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                });
            }
            return resultObjArr;
        }
    });
    //民族
    $("#mz2").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            if (top.clients.dataItems) {
                $.each(top.clients.dataItems, function (idx, val) {
                    if (idx == "Nation") {
                        $.each(val, function (key, value) {
                            $('#mz2').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                });
            }
            return resultObjArr;
        }
    });
    //职业
    $("#zy").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            if (top.clients.dataItems) {
                $.each(top.clients.dataItems, function (idx, val) {
                    if (idx == "Office") {
                        $.each(val, function (key, value) {
                            $('#zy').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                });
            }
            return resultObjArr;
        }
    });
}

function submitForm() {
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data["brxz"] = $("#brxz").attr("data-label");
        //data["dy"] = $("#dy").attr("data-label");
        $.submitForm({
            url: "/PatientManage/HospiterRes/SubmitForm",
            param: data,
            success: function () {
                $.loading(false);
                if ($.currentWindow().$("#txtkh").length == 1) {
                    //门诊挂号免卡登记
                    $.currentWindow().LoadPatientInfo(data.kh);
                }
                else {
                    //住院病人基本信息登记 免卡
                    var kh = $.currentWindow().$("#kh").val();
                    getSysBasicInfoByCardNo(kh);
                }
                $.currentWindow().DisableSysBasicInfo(true); //true:标志是从免卡登记或根据卡号查询返回明细, false:页面加载时，可以输kh
            }
        })
    }
}

function getSysBasicInfoByCardNo() {
    var kh = $.currentWindow().$("#kh").val();
    $.ajax({
        url: "/PatientManage/HospiterRes/GetBasicInfoByCardNo?keyValue=" + kh,
        dataType: "json",
        async: false,
        success: function (data) {
            $.currentWindow().$("#kh").val(data.kh);
            $.currentWindow().$("#xm").val(data.xm);
            $.currentWindow().$("#xb").val(data.xb);
            $.currentWindow().$("#csny").val(data.csny);
            $.currentWindow().$("#kh").val(data.kh);
            $.currentWindow().$("#zjh").val(data.zjh);
            if (data.xb == 0) {
                $.currentWindow().$("input[name=xb]:eq(1)").attr("checked", 'checked');//女
                $.currentWindow().$("input[name=xb]:eq(1)").parent().addClass('active');
            } else {
                $.currentWindow().$("input[name=xb]:eq(0)").attr("checked", 'checked');//男
                $.currentWindow().$("input[name=xb]:eq(0)").parent().addClass('active');
            }
            $.currentWindow().$("#nl").val(data.nl);
            $.currentWindow().$("#blh").val(data.blh);
            $.currentWindow().$("#dh").val(data.dh);
            $.currentWindow().$("#dy").val(data.dy);
            $.currentWindow().$("#hf").val(data.hf);
            $.currentWindow().$("#patid").val(data.patid);
            if (data.zjlx) {
                var type = data.zjlx.trim();
                switch (type) {
                    case "0": type = "身份证"; break;
                    case "1": type = "护照"; break;
                    case "2": type = "军官证"; break;
                    default: break;

                }
                $.currentWindow().$("#zjlx").html(type);
            }
            if (data.cs_sheng) {
                $.currentWindow().$("#cs_sheng").val(data.cs_sheng);
            }
            if (data.cs_shi) {
                $.currentWindow().$("#cs_shi").val(data.cs_shi);
            }
            if (data.cs_xian) {
                $.currentWindow().$("#cs_xian").val(data.cs_xian);
            }
            if (data.xian_dz) {
                $.currentWindow().$("#xian_dz").val(data.xian_dz);
            }
            if (data.xian_sheng) {
                $.currentWindow().$("#xian_sheng").val(data.xian_sheng);
            }
            if (data.xian_shi) {
                $.currentWindow().$("#xian_shi").val(data.xian_shi);
            }
            if (data.xian_xian) {
                $.currentWindow().$("#xian_xian").val(data.xian_xian);
            }
            if (data.hu_dz) {
                $.currentWindow().$("#hu_dz").val(data.hu_dz);
            }
            if (data.hu_sheng) {
                $.currentWindow().$("#hu_sheng").val(data.hu_sheng);
            }
            if (data.hu_shi) {
                $.currentWindow().$("#hu_shi").val(data.hu_shi);
            }
            if (data.hu_xian) {
                $.currentWindow().$("#hu_xian").val(data.hu_xian);
            }
            $.loading(false);
        }
    });
}

function checkNotNull() {
    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            xm: { required: true },
            brxz: { required: true },
            nl: { required: true },
            hf: { required: true },
            zjh: { isIdentity: true },
            email: { email: true },
            phone: { isMobile: true },
            dh: { isPhone: true },
            jjlldh: { isPhone: true }
        },
        messages: {
            xm: { required: "姓名必须填写" },
            brxz: { required: "病人性质必须填写" },
            nl: { required: "年龄必须填写" },
            hf: { required: "婚否必须选择" },
            blh: { required: "婚否必须选择" },
            zjh: { isIdentity: "证件格式不正确" },
            email: { email: "邮箱格式不正确" },
            phone: { isMobile: "手机格式不正确" },
            dh: { isPhone: "电话格式不正确" },
            jjlldh: { isPhone: "电话格式不正确" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                $.modalAlert(errorList[0].message, 'warning');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }
    //病历号
    var blh = $("#blh").val();
    if (!blh) {
        $.modalAlert("病历号为空！", 'warning');
        return false;
    }
    //拼音
    var py = $("#py").val();
    if (!py) {
        $.modalAlert("拼音为空！", 'warning');
        return false;
    }
    //性别
    var xb = false;
    $('input[name="xb"]').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("active")) {
            xb = true;
            $("#xb").val()
        }
    });
    if (!xb) {
        $.modalAlert("请选中性别！", 'warning');
        return false;
    }
    return true;
}