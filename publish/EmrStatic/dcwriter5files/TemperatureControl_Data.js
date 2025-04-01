//* 开始时间:
//* 开发者:
//* 重要描述:迁移四代BS时间轴前端的相关函数
//*************************************************************************
//* 最后更新时间: 2024-3-6
//* 最后更新人: wuyichao
//*************************************************************************


"use strict";
import { DCTools20221228 } from "./DCTools20221228.js";

export let TemperatureControl_Data = {
    /**
     * 新建时间轴Y轴对象
     */
    NewYAxis: function () {
        var yaxis = new Object();
        yaxis.AllowOutofRange = false;                      //同意超出范围            //适配
        yaxis.MaxValue = 100;                               //最大值                  //适配      
        yaxis.MinValue = 0;                                 //最小值                  //适配
        yaxis.Name = "";                                    //名称                    //适配
        yaxis.Style = "Value";                              //样式                    //适配
        yaxis.SymbolColorValue = null;                      //图例颜色                //适配
        yaxis.SymbolStyle = "SolidCicle";                   //图例样式                //适配
        yaxis.Title = "";                                   //标题                    //适配
        yaxis.YSplitNum = "8";                              //纵向分割数              //适配
        yaxis.EnableLanternValue = false;                   //是否显示灯笼值          //适配
        yaxis.SymbolSize = "20";                            //图例大小                //适配
        yaxis.TitleColorValue = null;                       //标题颜色                //适配
        yaxis.ShadowName = null;                            //阴影数据序列名          //适配
        yaxis.CharSymbol = "R";                             //灯笼字符                //适配
        yaxis.CharLantern = "R";                            //灯笼图标                //适配
        yaxis.TitleVisible = true;                          //标题可见                //适配
        yaxis.ShowLegendInRule = true;                      //在标尺中显示图例        //适配
        yaxis.BottomTitle = "";                             //底部标题                //适配
        yaxis.RedLineValue = -10000;                          //红线数值                //适配
        yaxis.VerticalLine = false;                          //文本分割线是否显示      //适配
        yaxis.ShadowPointVisible = true;                    //阴影区是否可见          //适配
        yaxis.HollowCovertTargetName = null;                //空心覆盖目标            //未适配
        yaxis.AllowInterrupt = true;                       //允许断线                //适配
        yaxis.LanternValueColorForUpValue = "#0000FF";      //显示超出上限值的颜色    //适配
        yaxis.LanternValueColorForDownValue = "#FF0000";    //显示低于下限值的颜色    //适配
        yaxis.TopPadding = -10000;                               //网格线顶端空白边距比例  //适配
        yaxis.BottomPadding = -10000;                            //网格线底端空白边距比例  //适配
        yaxis.MergeIntoLeft = false;                        //并入左边的标尺          //适配
        yaxis.SpecifyTitleWidth = 0;                        //标题宽度                //适配
        yaxis.AlertLineColorValue = "#FF0000";                                        //未适配
        yaxis.LineStyleForLanternValue = "Dash";            //灯笼值连线样式          //适配
        yaxis.ValueTextBackColorValue = null;               //文本值背景色            //未适配
        yaxis.ShowPointValue = false;                       //显示数值点的值          //适配
        yaxis.Visible = true;                               //可见                    //适配
        yaxis.BorderVisible = true;                         //侧边框是否可见
        yaxis.ShadowStyle = "LeftSlant";                    //阴影样式  LeftSlant / RightSlant / Vertical   //适配
        //新增
        yaxis.TickTextAlignment = null;                            //标题对齐方式"left""center""right"  
        yaxis.Scales = null;                                //刻度               //适配

        return yaxis;
    },
    /**
     * 新建时间轴数据点对象
     */
    NewValuePoint: function () {
        var date = new Date();
        var point = {
            ColorValue: null,            //文本颜色                                           //
            EndTime: "0001-01-01 00:00:00", //结束时间                                           //适配
            LanternValue: null,          //灯笼值                                             //适配
            Link: null,                                                                       //未适配
            SpecifySymbolStyle: "Default",                                                    //未适配
            Text: null,                  //文本                                               //适配
            TextAlign: "Center",         //水平对齐方式                                       //适配
            TextColorValue: null,        //文本颜色                                           //适配
            Time: DCTools20221228.FormatDateTime(date, "yyyy-MM-dd hh:mm:ss"), //时间          //适配
            Title: null,                //标题                                                //适配
            Value: -10000,                //数据                                                //适配
            CharSymbol: "R",            //灯笼样式                                            //适配
            CharLantern: "R",           //灯笼文本                                            //适配
            TextFontSize: null,         //字体大小                                            //适配
            TextFontName: null,         //字体名                                              //适配
            // UseAdvVerticalStyle: false,                                                        //未适配
            // UseAdvVerticalStyle2: false,                                                       //未适配
            Verified: false,                                                                   //未适配
            Superscript: false,                                                                //未适配
            LineStop: "Inherit",                                                               //适配
            ID: null,                   //标识符
            ShowPointValue: "Inherit",                                                         //适配
            UpAndDown: "None"                                                                  //适配
        };
        return point;
    },
    /**
     * 新建时间轴数据行对象
     */
    NewTitleLine: function () {
        var line = new Object();
        line.Name = "";//名称                                                                 //适配
        line.Title = "";//标题                                                                //适配
        line.TitleColorValue = "#000000";
        line.StartDateKeyword = "";//指定开始时间关键字                                       //适配
        line.LayoutType = "Normal";//布局模式                                                 //适配(Free,HorizCascade,Slant,Slant2,Slant3,Fraction)  
        line.TickStep = 1;//刻度步长                                                          //适配
        line.ValueType = "SerialDate";//数据类型                                                    //适配(TickText,Data,Text,HourTick,DayIndex,SerialDate,GlobalDayIndex,NewSerialDate)
        line.TickLineVisible = true;//是否显示刻度线                                          //适配
        line.SpecifyHeight = 0;//指定高度                                                     //适配
        line.AutoHeight = false;//自动调整高度                                                //未适配
        line.AfterOperaDaysFromZero = true; //术后天数计算的当天是否从0开始显示              //适配
        line.AfterOperaDaysBeginOne = false; //术后天数的当天是否从1开始显示                  //适配
        line.TextFontName = "宋体";//文本使用字体名称                                           //适配
        line.TextFontSize = 9;//文本使用字体大小                                           //适配
        line.LoopTextList = null;//循环文本列表                                               //适配
        line.UpAndDownTextType = "None";//上下交替文本样式"None""ShowByTick""ShowByText"      //适配
        line.TitleAlign = "Center";//数据行标题对齐方式"Near""Far""Center"                    //适配
        line.SpecifyTitleWidth = 0;//标题类宽度                                               //适配
        line.PreserveStartKeywordOrder = false; //保留开始关键字顺序                          //未适配
        line.PageTitleTexts = null;                                                           //适配
        line.BlankDateWhenNoData = false;  //当页没有数据点时隐藏                             //未适配
        line.EndDateKeyword = "";  //指定结束时间关键字                                       //适配
        line.TextColorValue = "";  //文本颜色                                            //适配      
        line.ExtendGridLineType = "Below"; //扩展网格线类型 默认值Below  "None""Below""Above"           //适配
        return line;
    },
    /**
     * 新建空白的时间轴页面设置信息对象
     */
    NewPageSettings: function () {
        var ps = new Object();
        ps.PaperSizeName = "A4";   //纸张类型   
        ps.PaperHeight = 296.93;   //纸张高度             //适配
        ps.PaperWidth = 210.06;    //纸张宽度             //适配
        ps.Landscape = false;      //横向打印             //适配
        ps.TopMargin = 25.4;       //上边距               //适配
        ps.BottomMargin = 25.4;    //下边距               //适配
        ps.LeftMargin = 25.4;      //左边距               //适配
        ps.RightMargin = 25.4;     //右边距               //适配
        ps.Unit = "Millimeter";
        return ps;
    },
    /**
     * 新建空白的时间轴文本框对象
     */
    NewTextLabel: function () {
        var label = new Object();
        label.Left = 0;                                     //左偏移         //适配
        label.Top = 0;                                      //上偏移         //适配
        label.Width = 100;                                  //宽度           //适配
        label.Height = 100;                                 //高度           //适配
        label.Text = "文本框";                              //文本           //适配
        label.MultiLine = false;                            //多行文本       //适配
        label.ShowBorder = false;                           //显示边框       //适配
        label.BackColorValue = null;                        //背景色         //适配
        label.TextColorValue = null;                        //文本颜色       //适配
        label.Alignment = "Center";                         //水平对齐方式   //适配
        label.LineAlignment = "Center";                     //垂直对齐方式   //适配
        label.TextFontName = "宋体";                        //字体名         //适配
        label.TextFontSize = 9;                             //字体大小       //适配
        label.TextFontBold = false;                         //文本加粗       //适配
        label.TextFontItalic = false;                       //文本倾斜       //适配
        label.TextFontUnderline = false;                    //文本下划线     //适配
        label.ImageDataBase64String = null;                 //图片base64数据 //适配
        label.ParameterName = "";                           //参数名         //适配
        label.PositionFixModeForAutoHeightLine = "None";                     //未适配(与数据行对象配合使用)
        return label;
    },
    /**
     * 新建空白的时间轴页眉小标签对象
     */
    NewHeaderLabel: function () {
        var label = new Object();
        label.SeperatorChar = ":";       //分割符                     //适配
        label.ParameterName = "";        //字段名                     //适配
        label.Title = "";                //参数名                     //适配
        label.Value = "";                //数值                       //适配
        label.GroupIndex = 0;            //分组坐标                   //适配
        label.ValueUnderLine = false;            //数值下划线                   //不适配
        return label;
    },
    /**
     * 新建空白的时间轴文档设置信息对象
     */
    NewTemperatureDocumentConfig: function () {
        var config = new Object();
        config.GridYSpaceNum = 5;     //网格垂直内部分割数                                       //适配
        config.GridYSplitNum = 8;     //网格垂直拆分数                                           //适配
        config.NumOfDaysInOnePage = 7;    //单页天数                                             //适配
        config.SpecifyStartDate = null;      //指定开始时间                                      //适配
        config.SpecifyEndDate = null;       //知道结束时间                                       //适配
        config.Title = null;                //大标题                                             //适配
        config.SpecifyTitleHeight = 0;      //指定标题高度                                       //适配
        config.BigVerticalGridLineWidth = 2;         //大垂直网格线线宽                          //适配
        config.BigVerticalGridLineColorValue = null; //大垂直网格线颜色                          //适配
        config.GridLineColorValue = null;   //网格线颜色                                         //适配
        config.FontName = "宋体";         //字体名                                               //适配
        config.FontSize = 9;            //字体大小                                               //适配
        config.DateFormatString = "yyyy-MM-dd";     //时间字符串格式
        config.DateFormatStringForCrossMonth = "MM-dd";  //输出跨月日期时间字符串格式
        config.DateFormatStringForCrossWeek = "dd";     //输出跨星期日期时间字符串格式
        config.DateFormatStringForCrossYear = "yyyy-MM-dd"; //输出跨年日期时间字符串格式
        config.DateFormatStringForFirstIndexFirstPage = "yyyy-MM-dd";  //首页首个日期时间字符串格式
        config.DateFormatStringForFirstIndexOtherPage = "dd";   //非首页首个日期时间字符串格式
        config.PageIndexText = "第[%pageindex%]页";   //页码文本                                  //适配
        config.EnableDataGridLinearAxisMode = false;   //数据点是否绘制在时刻区间实际位置         //适配
        config.TickTexts = "2,6,10,14,18,22";       //刻度文本                                    //适配
        config.TickValues = "2,6,10,14,18,22";      //刻度值                                      //适配
        config.TickColorValues = "#FF0000,#FF0000,,,,#FF0000";   //刻度颜色                       //适配  
        config.DataGridBottomPadding = 0;     //网格上边距                                        //适配
        config.DataGridTopPadding = 0;      //网格下边距                                          //适配
        config.BigTitleFontSize = 27;         //大标题字体大小                                    //适配
        config.FooterDescription = "";     //底部说明                                             //适配
        config.ThinLineWidth = 1;       //细线宽度                                                //适配
        config.ThickLineWidth = 2;      //粗线宽度                                                //适配
        config.FooterLines = new Array();    //页脚数据
        config.HeaderLabels = new Array();   //小标题数据
        config.HeaderLines = new Array();    //页眉数据
        config.YAxisInfos = new Array();     //y轴数据
        config.Labels = new Array();        //文本标签数据
        config.BigTitleFontName = null;     //大标题字体名                                     
        config.BigTitleFontBold = false;     //大标题加粗

        return config;
    },
    /**
     * 新建空白的时间轴文档对象
     */
    NewTemperatureDocument: function () {
        var doc = new Object();
        doc.PageIndex = 0;
        doc.ViewMode = "Page";
        doc.Config = this.NewTemperatureDocumentConfig();
        doc.Values = new Array();
        return doc;
    },
    /**
     * 对时间轴文档数据集添加数据点的内部函数
     * @param {Array} datas 指定前端时间轴文档对象的Values数据集
     * @param {string} name 要新增的数据点所属的名称，对应Y轴或数据行的名称属性
     * @param {object} vp 要新增的数据点对象
     */
    AddValuePoint: function (datas, name, vp) {
        if (Array.isArray(datas) == false || typeof (name) != "string" || typeof (vp) != "object") {
            return false;
        }
        var addname = true;
        for (var i = 0; i < datas.length; i++) {
            var data = datas[i];
            if (data.Name != null && data.Name === name) {
                addname = false;
                if (Array.isArray(data.Datas) == false) {
                    data.Datas = new Array();
                }
                data.Datas.push(vp);
                return true;
            }
        }
        if (addname === true) {
            var data = new Object();
            data.Name = name;
            data.Datas = new Array();
            data.Datas.push(vp);
            return true;
        }
        return false;
    }
};

//* 开始时间:
//* 开发者:
//* 重要描述:用于五代体温单读取xml文件，并将其解析为json数据的方法20241030
//*************************************************************************
//* 最后更新时间: 20241030
//* 最后更新人: lixinyu
//**
export let TemperatureControl_XMLToJSON = {
    // 解析xml数据
    GetJsonData: function (xmlContent) {
        let parser = new DOMParser();
        let xmlDoc = parser.parseFromString(xmlContent, "text/xml");

        let TemperaturejsonData = TemperatureControl_XMLToJSON.parseXmlData(xmlDoc.documentElement);
        // console.log(TemperaturejsonData);
        return TemperaturejsonData;
    },
    // 解析xml数据
    parseXmlData: function (node) {
        let children = node.children;

        let pNodeName = node.nodeName;
        if (children.length > 0) {
            //区分解析成对象or数组
            if (TemperatureControl_XMLToJSON.toArrLabelsName.includes(pNodeName)) {
                let defaultValue = TemperatureControl_XMLToJSON.loadDefaultValue(pNodeName);
                return TemperatureControl_XMLToJSON.transAttributesAndChildToObject(children, defaultValue);
            } else {
                let xmlData = TemperatureControl_XMLToJSON.loadDefaultValue(pNodeName);
                for (let i = 0; i < children.length; i++) {
                    let child = children[i];
                    let cNodeName = child.nodeName;

                    if (cNodeName == 'Datas') {
                        xmlData.Values = TemperatureControl_XMLToJSON.handleDatas(child.children);
                    } else if (cNodeName == 'Parameters') {
                        xmlData.Parameters = TemperatureControl_XMLToJSON.transAttributesAndChildToObject(child.children);
                    } else if (cNodeName == 'Ticks') {
                        Object.assign(xmlData, TemperatureControl_XMLToJSON.handleTicks(child.children));
                    } else {
                        xmlData[cNodeName] = TemperatureControl_XMLToJSON.parseXmlData(child);
                    }
                }
                return xmlData;
            }
        } else {
            if (['LeftMargin', 'TopMargin', 'RightMargin', 'BottomMargin'].includes(pNodeName)) {
                return TemperatureControl_XMLToJSON.transValue(node.innerHTML);
            }
            return TemperatureControl_XMLToJSON.transType(node.innerHTML);
        }
    },
    loadDefaultValue: function (labelName) {
        switch (labelName) {
            case 'TemperatureDocument':
                return TemperatureControl_Data.NewTemperatureDocument();
            case 'Config':
                return TemperatureControl_Data.NewTemperatureDocumentConfig();
            case 'Datas':
                let NewValuePoint = TemperatureControl_Data.NewValuePoint();
                NewValuePoint.EndTime = '0001-01-01 00:00:00';
                return NewValuePoint;
            case 'Labels':
                return TemperatureControl_Data.NewTextLabel();
            case 'YAxisInfos':
                return TemperatureControl_Data.NewYAxis();
            case 'HeaderLines':
                //刻度步长改成默认是1
                let data = TemperatureControl_Data.NewTitleLine();
                data.TickStep = 1;
                data.ValueType = 'SerialDate';
                return data;
            case 'HeaderLabels':
                return TemperatureControl_Data.NewHeaderLabel();
            case 'FooterLines':
                let data2 = TemperatureControl_Data.NewTitleLine();
                data2.TickStep = 1;
                data2.ValueType = 'SerialDate';
                return data2;
            case 'PageSettings':
                return TemperatureControl_Data.NewPageSettings();
            default:
                return {};
                break;
        }
    },
    transType: function (prop, nodeName) {
        if (nodeName == 'MaxValue') {
            return prop * 1;
        } else if (nodeName == 'SeperatorChar') {
            return String.fromCharCode(prop * 1);
        }
        return prop;

    },
    concatPropsToObject: function (props, innerHTML) {
        let obj = {};
        if (props.length > 0) {
            //针对Labels
            for (var i = 0; i < props.length; i++) {
                let p = props[i];

                if (p.nodeName == 'Font') {
                    let cc = [...p.children];
                    cc.forEach(c => {
                        obj['TextFont' + c.nodeName] = TemperatureControl_XMLToJSON.transType(c.innerHTML);
                    });
                } else if (p.nodeName == 'Scales') {
                    //临时增加解析Scales 20241113 lixinyu
                    if (p.children.length > 0) {
                        obj[p.nodeName] = TemperatureControl_XMLToJSON.transAttributesAndChildToObject(p.children);
                    }
                } else if (p.nodeName == 'Image') {
                    obj['ImageDataBase64String'] = p.children[0].innerHTML;
                } else {
                    obj[p.nodeName] = TemperatureControl_XMLToJSON.transType(p.innerHTML);
                }

            }

        } else {
            //兼容Parameters
            obj.Value = TemperatureControl_XMLToJSON.transType(innerHTML);
        }
        return obj;
    },
    transAttributesAndChildToObject: function (children, defaultValue = {}) {

        let result = [];
        for (let i = 0; i < children.length; i++) {
            const child = children[i];
            //处理属性
            let obj = {}, attributes = [...child.attributes];
            attributes.forEach(item => {
                obj[item.name] = TemperatureControl_XMLToJSON.transType(item.nodeValue, item.name);
            });
            //处理子元素
            if (child.children.length > 0 || child.innerHTML) {
                Object.assign(obj, TemperatureControl_XMLToJSON.concatPropsToObject(child.children, child.innerHTML));
            }
            result.push({ ...defaultValue, ...obj });
        }
        return result;
    },
    handleDatas: function (datas) {
        let d = [...datas];
        let result = [];
        d.forEach(data => {
            let defaultValue = TemperatureControl_XMLToJSON.loadDefaultValue('Datas');
            result.push({
                Name: data.attributes.length > 0 ? data.attributes[0].nodeValue : '',
                Datas: TemperatureControl_XMLToJSON.transAttributesAndChildToObject(data.children[0].children, defaultValue)
            });
        });
        return result;
    },
    handleTicks: function (ticks) {
        let ticksArr = TemperatureControl_XMLToJSON.transAttributesAndChildToObject(ticks);
        let result = {
            TickTexts: '',
            TickValues: '',
            TickColorValues: ''
        };
        ticksArr.forEach((tick, index) => {
            let sign = index == 0 ? '' : ',';
            result.TickTexts += sign + (tick.Text || '');
            result.TickValues += sign + (tick.Value || '');
            result.TickColorValues += sign + (tick.ColorValue || '');
        });
        return result;
    },
    toArrLabelsName: ['Labels', 'YAxisInfos', 'HeaderLines', 'HeaderLabels', 'FooterLines'],
    transValue: function (value) {
        let d = (value * 1) / 100 * 25.4;
        d = Math.round(d, 2);
        return d.toString();
    }

};