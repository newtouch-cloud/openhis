import { d3 } from "./WriterControl_DrawD3.js";

export let WriterControl_DrawFu = {
    //初始化方法
    CreateTemperatureInit: function (rootElement, data, type, args) {
        if (data == null) {
            data = {};
        }
        if (typeof rootElement == "string") {
            rootElement = document.getElementById(rootElement);
        }

        if (rootElement != null) {

            //记录上一次的位置
            var lastScrollTop = 0;
            if (rootElement.pageContainer) {
                lastScrollTop = rootElement.pageContainer.scrollTop;
            }

            rootElement.document = document; //绑定一次rootElemnt所对应的document
            //赋默认值
            WriterControl_DrawFu.CreateDefaultData(rootElement, data);
            //绘制外层包裹元素
            WriterControl_DrawFu.CreatePageContainer(rootElement);
            //最开始直接修正数据
            WriterControl_DrawFu.ChangeData(rootElement, data, type);
            //对数据进行修正
            WriterControl_DrawFu.ChangeValue(rootElement, data);
            //绘制svg元素
            WriterControl_DrawFu.DrawSvg(rootElement);
            //渲染表格
            if (data && data.BottomTableGroups && Object.keys(data.BottomTableGroups).length > 0) {
                WriterControl_DrawTable.DrawTableInit(rootElement);
            }
            rootElement._ctl = null;

            //设置注册码信息
            var registerCodeStr = rootElement.getAttribute("registercode") || null;
            if (registerCodeStr) {
                rootElement.TemperatureRegisterCode = registerCodeStr;
            }

            if (rootElement.isFileNew !== true) {
                if (type) {
                    if (lastScrollTop && type != "EventTemperatureDocumentLoad") {
                        rootElement.pageContainer.scrollTo(0, lastScrollTop);
                    }
                    rootElement.InnerRaiseEvent(type, args);
                } else {
                    //初始化完成回调函数
                    rootElement.InnerRaiseEvent("EventTemperatureControlInit");
                }
            }
        } else {
            console.log("未查找到根元素");
            return false;
        }

    },

    //制作默认数据
    CreateDefaultData: function (rootElement, data) {
        //默认数据
        var DefaultData = {
            ViewMode: "Page",
            PageIndex: 0,
            NumOfPages: 0,
            Config: {
                "SpecifyStartDate": null,
                "SpecifyEndDate": null,
                "GridYSplitNum": 8,
                "GridYSpaceNum": 5,
                "Title": "",
                "SpecifyTitleHeight": 300,
                "NumOfDaysInOnePage": 7,
                "BigVerticalGridLineColorValue": "#000000",
                BigVerticalGridLineWidth: 2,
                "BigTitleFontName": "宋体",
                "BigTitleFontBold": false,
                "TagString": null,
                "GridLineColorValue": "#E0E0E0",
                "FontName": "宋体",
                "FontSize": 9,
                "DateFormatString": "yyyy-MM-dd",
                "DateFormatStringForCrossMonth": "MM-dd",
                "DateFormatStringForCrossWeek": "dd",
                "DateFormatStringForCrossYear": "yyyy-MM-dd",
                "DateFormatStringForFirstIndexFirstPage": "yyyy-MM-dd",
                "DateFormatStringForFirstIndexOtherPage": "dd",
                "PageIndexText": "第[%pageindex%]页",
                "EnableDataGridLinearAxisMode": false,
                "DataGridBottomPadding": 0,
                "DataGridTopPadding": 0,
                "BigTitleFontSize": 27,
                "FooterDescription": "",
                "ThickLineWidth": 2,
                "ThinLineWidth": 1,
                "TickTexts": "2,6,10,14,18,22",
                "TickValues": "2,6,10,14,18,22",
                "TickColorValues": "#FF0000,#FF0000,,,,#FF0000",
                "PageSettings": {
                    "PaperSizeName": "A4",
                    "PaperHeight": 296.93,
                    "PaperWidth": 210.06,
                    "Landscape": false,
                    "TopMargin": 12.7,
                    "BottomMargin": 12.7,
                    "LeftMargin": 12.7,
                    "RightMargin": 12.7,
                    "Unit": "Millimeter"
                },
                "HeaderLabels": [],
                "HeaderLines": [],
                "Labels": [],
                "FooterLines": [],
                "YAxisInfos": []
            },
            Parameters: [],
            Values: [],
            Version: ""
        };
        //页面的计算数据
        var CalculationData = {
            //模式
            DCType: "DCTemperatureControlForWASM",
            UIDElePositon: [], //元素位置
            ZoomRate: rootElement.getAttribute("zoomrate") || 1, //页面缩放比例
            //页面设置相关
            PaperWidth: 0, //页面宽
            PaperHeight: 0, //页面高
            InnerWidth: 0, //内部宽
            InnerHeight: 0, //内部高
            TopMargin: 0, //上边距
            BottomMargin: 0, //左边距
            LeftMargin: 0, //左边距
            RightMargin: 0, //右边距
            //svg数量相关
            NumOfDaysInOnePage: 7, //一页展示日期数
            TotalPageNumber: 1, //总页数
            HeaderFooterValueArr: {}, //页眉页脚value数据对象
            GetViewMode: "page", //页面设置
            HasPageIndex: 1, //页码
            SvgScrollTop: 0, //当前svg距离页面的高度
            //页眉页脚表格相关
            FirstTextWidth: 0, //第一行文本宽度
            HeaderLinesSingleHeight: [], //页眉数据行高度数组
            HeaderLinesTotalHeight: 0, //页眉数据行总高度
            FooterLinesSingleHeight: [], //页脚数据行高度数组
            FooterLinesTotalHeight: 0, //页脚数据行总高度
            HeaderAndFooterStepWidth: 0, //页眉页脚除第一列外单列宽度
            //页眉页脚时间相关
            HeaderLineStartDateTime: new Date(1900 - 1 - 1), //时间轴开始日期
            HeaderLineEndDateTime: new Date(1900 - 1 - 7), //时间轴结束日期
            IntervalDateTime: 6, //间隔日期
            AllTimeData: [], //页眉日期毫秒数数组
            AllDateData: [], //页眉日期数组
            TickTextsData: [], //时刻文本数据
            TickColorData: [], //时刻颜色数据
            TickValuesData: [], //新的文本数据值
            OldTickValuesData: [], //原始文本值数据
            TickTextStepWidth: 0, //时刻步进宽度
            StartDateKeyword: {}, //需要通过特定时间开始绘制的文本
            EndDateKeyword: {}, //需要通过特定时间结束绘制的文本
            //标题相关
            TitleHeight: 36, //大标题高度  默认27pt 36px
            //页码相关
            LableHeight: 16, //页码高度 默认9pt 1.5倍行高 16px
            singleLableTextWidth: 12, //单个字符的宽度 默认9pt 12px
            //小标题相关
            AllHeaderLabels: [], //分组计算标题宽度
            SpecifyTitleHeight: 0, //小标题所占总高度
            //底部说明元素相关
            FooterDescriptionHeight: 0, //底部说明元素的高度
            //表格包裹元素相关
            TableContainerHeight: 0, //表格包裹元素的高度
            //Y轴数据
            YAxisInfosData: [], //y轴数据拼接
            ShadowNameArr: {}, //Y轴合并数组
            ShadowPointVisible: [], //是否显示阴影
            YAxisInfosSingleWidth: [], //y轴数据行的宽度数组
            YAxisInfosTotalWidth: 0, //y轴数据行的总宽度
            YAxisInfosTotalHeight: 0, //y轴数据行的总高度
            HasYTopPadding: [], //存在上边距
            HasYBottomPadding: [], //存在下边距
            GridTopPadding: 0, //网格区上边距
            GridBottomPadding: 0, //网格区下边距
            PainScoreInfo: {}, //疼痛评分的位置信息
            HeaderLabelChangeDate: null, //页眉日期变化日期
            HeaderLinesChangeDate: null, //页眉线变化日期
            FooterLinesChangeDate: null, //页脚线变化日期
        };
        //页面具体元素
        var SVGElement = [];
        //配置默认值
        WriterControl_DrawFu.DocumentOptions = {
            DefaultData: DefaultData,
            CalculationData: CalculationData,
            SVGElement: SVGElement
        };

        //表格
        if (data && data.BottomTableGroups && Object.keys(data.BottomTableGroups).length > 0) {
            WriterControl_DrawFu.DocumentOptions.DefaultData['BottomTableGroups'] = data.BottomTableGroups;
            // WriterControl_DrawFu.DocumentOptions.CalculationData['BottomTableGroups'] = data.BottomTableGroups;
        }

        rootElement.DocumentOptions = WriterControl_DrawFu.DocumentOptions;
    },

    //创建外层包裹元素方便滚动
    CreatePageContainer: function (rootElement) {
        var pageContainer = document.createElement("div");
        pageContainer.setAttribute("dctype", "page-container");
        rootElement.innerHTML = "";
        rootElement.appendChild(pageContainer);
        // rootElement.style.height = "100%";
        if (rootElement.style.backgroundColor == "") {
            rootElement.style.backgroundColor = "rgb(230, 230, 230)";
        }
        if (rootElement.style.borderBottom == "") {
            rootElement.style.borderBottom = "0px solid black";
        }
        if (rootElement.style.backgroundAttachment == "") {
            rootElement.style.backgroundAttachment = "fixed";
        }
        if (rootElement.style.backgroundSize == "") {
            rootElement.style.backgroundSize = "cover";
        }
        if (rootElement.style.backgroundPosition == "") {
            rootElement.style.backgroundPosition = "center top";
        }
        if (rootElement.style.overflow == "") {
            rootElement.style.overflow = "clip";
        }
        //写入样式
        pageContainer.style.height = "100%";
        pageContainer.style.width = "100%";
        pageContainer.style.setProperty('position', 'relative', 'important');
        pageContainer.style.textAlign = "center";
        pageContainer.style.overflow = "auto";
        rootElement.pageContainer = pageContainer;

        return pageContainer;
    },

    //计算数据
    ChangeData: function (rootElement, data, type) {
        //默认数据进行数据矫正
        if (data && typeof data == "object" && data.Config) {
            for (var i in data.Config) {
                if (i == "PageSettings") {
                    for (var j in data.Config[i]) {
                        if (data.Config[i][j]) {
                            WriterControl_DrawFu.DocumentOptions.DefaultData.Config[i][j] = data.Config[i][j];
                        }
                    }
                } else {
                    if (data.Config[i]) {
                        WriterControl_DrawFu.DocumentOptions.DefaultData.Config[i] = data.Config[i];
                    }
                }
            }
        }

        if (data && typeof data == "object" && data.Parameters) {
            WriterControl_DrawFu.DocumentOptions.DefaultData.Parameters = data.Parameters;
        }

        WriterControl_DrawFu.DocumentOptions.DefaultData.Version = data.Version;

        //默认配置
        var defaultData = WriterControl_DrawFu.DocumentOptions.DefaultData.Config;
        //数据
        // var value = WriterControl_DrawFu.DocumentOptions.DefaultData.Values;
        //编辑器计算相关s数据
        var calculationData = WriterControl_DrawFu.DocumentOptions.CalculationData;

        //写入模式
        var DCType = rootElement.getAttribute("dctype");
        calculationData.DCType = DCType == "DCTemperatureDesignControlForWASM" ? DCType : "DCTemperatureControlForWASM";

        var pageSettings = defaultData.PageSettings;
        //页面总高度
        var PaperHeight = Math.round(parseFloat(pageSettings.PaperHeight) / 25.4 * 96);
        //计算页面设置属性
        var needChangeMMToPX = ['PaperHeight', "PaperWidth", "TopMargin", "BottomMargin", "LeftMargin", "RightMargin"];

        for (var i in pageSettings) {
            if (needChangeMMToPX.indexOf(i) >= 0) {
                calculationData[i] = Math.round(parseFloat(pageSettings[i]) / 25.4 * 96);
                if (i == 'PaperHeight') {
                    //保留一遍页面高度,新生儿表格可能会导致原始高度发生改变
                    calculationData["OldPaperHeight"] = calculationData[i];

                }

            }
        }


        var tableAllHeight = 0;
        //判断是否含有表格
        if (data && data.BottomTableGroups && Object.keys(data.BottomTableGroups).length > 0) {
            WriterControl_DrawFu.DocumentOptions.CalculationData["BottomTableGroups"] = JSON.parse(JSON.stringify(data.BottomTableGroups));
            var bottomTableGroups = WriterControl_DrawFu.DocumentOptions.CalculationData["BottomTableGroups"];
            //设置每一个表格的高度
            for (var item in bottomTableGroups) {
                var tableOptionItem = bottomTableGroups[item];
                if (tableOptionItem) {
                    // 补充一个id 
                    if (!tableOptionItem.id) {
                        tableOptionItem.id = item;
                    }
                    //高度转换
                    var tableSetting = ["tableHeight", "tableWidth", "keepHeight"];
                    tableSetting.forEach(function (key) {
                        if (tableOptionItem[key]) {
                            var height = parseFloat(tableOptionItem[key]);
                            if (typeof tableOptionItem[key] == 'string' && tableOptionItem[key].endsWith('%')) {
                                //百分比
                                tableOptionItem[key] = PaperHeight * (height / 100);
                            } else {
                                //像素
                                tableOptionItem[key] = height;
                            }


                            //重新计算Y轴画布的高度
                            if (key == "tableHeight" || key == "keepHeight") {
                                tableAllHeight += tableOptionItem[key];
                            }
                        }
                    });

                    //Y轴区域高度递减
                    calculationData.PaperHeight -= (tableOptionItem.keepHeight + tableOptionItem.tableHeight);
                }
            }
        }



        if (pageSettings.Landscape === true) {
            var oldWidth = calculationData.PaperWidth;
            var oldHeight = calculationData.PaperHeight;
            var oldLeft = calculationData.LeftMargin;
            var oldRight = calculationData.RightMargin;
            var oldTop = calculationData.TopMargin;
            var oldBottom = calculationData.BottomMargin;
            calculationData.PaperWidth = oldHeight;
            calculationData.PaperHeight = oldWidth;
            calculationData.LeftMargin = oldBottom;
            calculationData.RightMargin = oldTop;
            calculationData.TopMargin = oldLeft;
            calculationData.RightMargin = oldRight;
        }

        //外层元素宽高
        calculationData.InnerWidth = calculationData.PaperWidth - calculationData.LeftMargin - calculationData.RightMargin;
        calculationData.InnerHeight = calculationData.PaperHeight - calculationData.TopMargin - calculationData.BottomMargin;

        //拿到一页展示日期数
        calculationData.NumOfDaysInOnePage = parseInt(defaultData.NumOfDaysInOnePage);

        //获取页眉页脚时间段并写入时间段数组
        //时刻数据
        calculationData.TickTextsData = defaultData.TickTexts.split(",");
        calculationData.TickValuesData = defaultData.TickValues.split(",");
        calculationData.TickTextsData.forEach((d, i) => {
            calculationData.TickTextsData[i] = d.toLowerCase().trim();
        });
        calculationData.TickValuesData.forEach((d, i) => {
            calculationData.TickValuesData[i] = d.toLowerCase().trim();
        });
        // if (calculationData.TickTextsData[0] == "0") {
        //     calculationData.TickTextsData.unshift();
        // }
        // if (calculationData.TickTextsData[calculationData.TickTextsData.length - 1] == "24") {
        //     calculationData.TickTextsData.pop();
        // }
        // if (calculationData.TickValuesData[0] == "0") {
        //     calculationData.TickValuesData.unshift();
        // }
        // if (calculationData.TickValuesData[calculationData.TickValuesData.length - 1] == "24") {
        //     calculationData.TickValuesData.pop();
        // }
        //修正TickTextsData属性
        if (calculationData.TickTextsData.indexOf("") >= 0 || calculationData.TickTextsData.length > 6) {
            calculationData.TickTextsData = ["2", "6", "10", "14", "18", "22"];
            calculationData.TickValuesData = ["2", "6", "10", "14", "18", "22"];
        }
        if (calculationData.TickValuesData.indexOf("") >= 0 || calculationData.TickValuesData.length > 6) {
            calculationData.TickTextsData = ["2", "6", "10", "14", "18", "22"];
            calculationData.TickValuesData = ["2", "6", "10", "14", "18", "22"];
        }
        calculationData.OldTickValuesData = calculationData.TickValuesData;
        calculationData.TickValuesData = [];
        //对TickTextsData的值进行修正
        calculationData.OldTickValuesData.forEach((d, i) => {
            if (i == 0) {
                calculationData.TickValuesData[i] = "0";
            } else {
                calculationData.TickValuesData[i] = String(calculationData.OldTickValuesData[i] - ((calculationData.OldTickValuesData[i] - calculationData.OldTickValuesData[i - 1]) / 2));
            }
        });
        calculationData.TickValuesData.push("24");
        //强制修正TickColorData属性
        calculationData.TickColorData = defaultData.TickColorValues.split(",");
        for (var i = 0; i < 6; i++) {
            var thisData = calculationData.TickColorData[i];
            if (i == 0 || i == 1 || i == 5) {
                if (thisData == "") {
                    calculationData.TickColorData[i] = "#ff0000";
                }
            } else {
                if (thisData == "") {
                    calculationData.TickColorData[i] = "#000000";
                }
            }
        }
        if (!calculationData['HeaderLinesChangeDateSourceName']) {
            calculationData['HeaderLinesChangeDateSourceName'] = {};
        }
        //循环页眉数据行
        for (var i = 0; i < defaultData.HeaderLines.length; i++) {
            var thisHeaderLine = defaultData.HeaderLines[i];
            //记录一次自定义前的默认数据
            if (!calculationData['HeaderLinesChangeDateSourceName'][thisHeaderLine.Name]) {
                calculationData['HeaderLinesChangeDateSourceName'][thisHeaderLine.Name] = thisHeaderLine.Title || '';
            }
            //设置默认值
            if (!thisHeaderLine.ExtendGridLineType) {
                thisHeaderLine.ExtendGridLineType = 'Below';
            }
            var thisTitle = thisHeaderLine.Title ? thisHeaderLine.Title : "页眉数据行";
            var textFontSize = thisHeaderLine.TextFontSize ? thisHeaderLine.TextFontSize : calculationData.singleLableTextWidth;
            var thisWidth = thisTitle.length * calculationData.singleLableTextWidth;
            if (thisHeaderLine.SpecifyTitleWidth && parseFloat(thisHeaderLine.SpecifyTitleWidth) > 0) {
                var specifyTitleWidth = parseFloat((thisHeaderLine.SpecifyTitleWidth / 300 * 96.00001209449).toFixed(2));
                calculationData.FirstTextWidth = calculationData.FirstTextWidth > specifyTitleWidth ? calculationData.FirstTextWidth : specifyTitleWidth;
            } else {
                calculationData.FirstTextWidth = calculationData.FirstTextWidth > thisWidth ? calculationData.FirstTextWidth : thisWidth;
            }
            if (thisHeaderLine.SpecifyHeight && parseFloat(thisHeaderLine.SpecifyHeight) > 0) {
                //计算高度
                calculationData.HeaderLinesSingleHeight[i] = parseFloat((thisHeaderLine.SpecifyHeight / 300 * 96.00001209449).toFixed(2)) * 1.3;
            } else {
                calculationData.HeaderLinesSingleHeight[i] = calculationData.LableHeight * 1.3;
            }
            calculationData.HeaderLinesTotalHeight += calculationData.HeaderLinesSingleHeight[i];

            if (thisHeaderLine.StartDateKeyword && thisHeaderLine.StartDateKeyword.length > 0) {
                calculationData.StartDateKeyword[thisHeaderLine.StartDateKeyword] = null;
            }
            if (thisHeaderLine.EndDateKeyword && thisHeaderLine.EndDateKeyword.length > 0) {
                calculationData.EndDateKeyword[thisHeaderLine.EndDateKeyword] = null;
            }
        }

        if (!calculationData['FooterLinesChangeDateSourceName']) {
            calculationData['FooterLinesChangeDateSourceName'] = {};
        }
        //循环页脚数据行
        for (var i = 0; i < defaultData.FooterLines.length; i++) {
            var thisFooterLine = defaultData.FooterLines[i];
            //记录一次自定义前的默认数据
            if (!calculationData['FooterLinesChangeDateSourceName'][thisFooterLine.Name]) {
                calculationData['FooterLinesChangeDateSourceName'][thisFooterLine.Name] = thisFooterLine.Title || {};
            }

            //设置默认值
            if (!thisFooterLine.ExtendGridLineType) {
                thisFooterLine.ExtendGridLineType = 'Below';
            }
            var thisTitle = thisFooterLine.Title ? thisFooterLine.Title : "页脚数据行";
            //对thisTitle进行解析判断是否存在空格和字母
            var charLength = thisTitle.match(/[0-9a-zA-z ]/g);
            if (charLength) {
                var thisWidth = ((thisTitle.length - charLength.length) * calculationData.singleLableTextWidth) + charLength.length * (calculationData.singleLableTextWidth / 2);
            } else {
                var thisWidth = thisTitle.length * calculationData.singleLableTextWidth;
            }

            if (thisFooterLine.SpecifyTitleWidth && parseFloat(thisFooterLine.SpecifyTitleWidth) > 0) {
                var specifyTitleWidth = parseFloat((thisFooterLine.SpecifyTitleWidth / 300 * 96.00001209449).toFixed(2));
                calculationData.FirstTextWidth = calculationData.FirstTextWidth > specifyTitleWidth ? calculationData.FirstTextWidth : specifyTitleWidth;
            } else {
                calculationData.FirstTextWidth = calculationData.FirstTextWidth > thisWidth ? calculationData.FirstTextWidth : thisWidth;
            }
            if (thisFooterLine.SpecifyHeight && parseFloat(thisFooterLine.SpecifyHeight) > 0) {
                //计算高度
                calculationData.FooterLinesSingleHeight[i] = parseFloat((thisFooterLine.SpecifyHeight / 300 * 96.00001209449).toFixed(2)) * 1.3;
            } else {
                calculationData.FooterLinesSingleHeight[i] = calculationData.LableHeight * 1.3;
            }
            calculationData.FooterLinesTotalHeight += calculationData.FooterLinesSingleHeight[i];

            if (thisFooterLine.StartDateKeyword && thisFooterLine.StartDateKeyword.length > 0) {
                calculationData.StartDateKeyword[thisFooterLine.StartDateKeyword] = null;
            }
            if (thisFooterLine.EndDateKeyword && thisFooterLine.EndDateKeyword.length > 0) {
                calculationData.EndDateKeyword[thisFooterLine.EndDateKeyword] = null;
            }
        }

        //循环y轴数据行
        calculationData.YAxisInfosData = [];
        for (var i = 0; i < defaultData.YAxisInfos.length; i++) {
            //[DUWRITER5_0-3489]lxy 20240830 解决隐藏Y轴Id丢失问题
            var uid = `dcYAxisInfos_` + i;
            defaultData.YAxisInfos[i]['UID'] = uid;
            var thisYAxisInfos = defaultData.YAxisInfos[i];
            thisYAxisInfos.AllowInterrupt = thisYAxisInfos.AllowInterrupt === true || thisYAxisInfos.AllowInterrupt === 'true';
            // thisYAxisInfos.MinValue = parseInt(thisYAxisInfos.MinValue);
            // thisYAxisInfos.MaxValue = parseInt(thisYAxisInfos.MaxValue);
            // thisYAxisInfos.YSplitNum = parseInt(thisYAxisInfos.YSplitNum);
            // thisYAxisInfos.SymbolSize = parseInt(thisYAxisInfos.SymbolSize);
            // thisYAxisInfos.RedLineValue = parseInt(thisYAxisInfos.RedLineValue);
            // thisYAxisInfos.TopPadding = parseInt(thisYAxisInfos.TopPadding);
            // thisYAxisInfos.BottomPadding = parseInt(thisYAxisInfos.BottomPadding);
            // thisYAxisInfos.SpecifyTitleWidth = parseInt(thisYAxisInfos.SpecifyTitleWidth);
            // thisYAxisInfos.LineWidth = parseInt(thisYAxisInfos.LineWidth);

            var thisTitle = thisYAxisInfos.Title ? thisYAxisInfos.Title : "页脚数据行";
            var thisWidth = (thisTitle.length) * calculationData.singleLableTextWidth;
            //判断y轴的值
            if (thisYAxisInfos.Visible == false) {
                continue;
            }
            if (typeof thisYAxisInfos.ShadowName == "string" && thisYAxisInfos.ShadowName.length > 0) {
                if (!calculationData.ShadowNameArr.Name) {
                    calculationData.ShadowNameArr.Name = [];
                }
                if (!calculationData.ShadowNameArr.List) {
                    calculationData.ShadowNameArr.List = {};
                }
                calculationData.ShadowNameArr.List[thisYAxisInfos.Name] = thisYAxisInfos.ShadowName;

                //判断已有的是否存在数据
                //var thisData = calculationData.YAxisInfosData["dc_" + calculationData.ShadowNameArr.Name[z]];
                //if (thisData) {
                //    calculationData.YAxisInfosData.splice(thisData.Index, 1);
                //} else {
                //    calculationData.ShadowNameArr.Name.push(thisYAxisInfos.ShadowName);
                //}
                //如果不存在,则写入
                if (thisYAxisInfos.ShadowName && calculationData.ShadowNameArr.Name.indexOf(thisYAxisInfos.ShadowName) < 0) {
                    calculationData.ShadowNameArr.Name.push(thisYAxisInfos.ShadowName);
                }
            }

            calculationData.YAxisInfosData["dc_" + thisYAxisInfos.Name] = thisYAxisInfos;
            var SymbolStyle = thisYAxisInfos.SymbolStyle;
            switch (SymbolStyle) {
                case "RoundDotIcon"://圆点
                    calculationData.YAxisInfosData["dc_" + thisYAxisInfos.Name]['SymbolStyle'] = 'SolidCicle';
                    break;
                case "HollowCicle"://空心圆
                case "circular"://空心圆
                case "OpaqueHollowCicle":
                    calculationData.YAxisInfosData["dc_" + thisYAxisInfos.Name]['SymbolStyle'] = 'HollowCicle';
                    break;
                case "HollowTriangle": //空心三角形
                    calculationData.YAxisInfosData["dc_" + thisYAxisInfos.Name]['SymbolStyle'] = 'HollowSolidTriangle';
                    break;
                case "HollowTriangleReversed"://空心倒三角形
                    calculationData.YAxisInfosData["dc_" + thisYAxisInfos.Name]['SymbolStyle'] = 'HollowSolidTriangleReversed';
                    break;
            }

            //拼接数据
            var min = parseFloat(thisYAxisInfos.MinValue);
            var max = parseFloat(thisYAxisInfos.MaxValue);
            var diff = (max - min) / parseFloat(thisYAxisInfos.YSplitNum);
            diff = diff == 0 ? 1 : diff;
            var index = calculationData.YAxisInfosData.length;
            while (max >= min) {
                if (calculationData.YAxisInfosData[index] == null) {
                    calculationData.YAxisInfosData[index] = [[], thisYAxisInfos];
                }
                calculationData.YAxisInfosData[index][0].push(parseFloat(max.toFixed(2)));
                max -= diff;
            }
            calculationData.YAxisInfosData["dc_" + thisYAxisInfos.Name].Index = index;

            //[DUWRITER5_0-3598] 20240920 lxy 解决Y轴文字首次加载位置不准确问题
            if (!thisYAxisInfos.TitleVisible && calculationData.DCType == "DCTemperatureControlForWASM") {
                calculationData.YAxisInfosData[index] = [[], thisYAxisInfos];
                calculationData.YAxisInfosData["dc_" + thisYAxisInfos.Name].Index = index;
                continue;
            }



            if (calculationData.ShadowNameArr.Name) {
                var hasIndex = calculationData.ShadowNameArr.Name.indexOf(thisYAxisInfos.Name);
                if (hasIndex >= 0 && calculationData.DCType == "DCTemperatureControlForWASM") {
                    continue;
                }
            }
            if (thisYAxisInfos.Style == "Text" && calculationData.DCType == "DCTemperatureControlForWASM") {
                continue;
            }
            index = calculationData.YAxisInfosSingleWidth.length;
            //限定一个最小值24
            calculationData.YAxisInfosSingleWidth[index] = calculationData.YAxisInfosSingleWidth[index] > (24 * 1.3) ? calculationData.YAxisInfosSingleWidth[index] : (24 * 1.31);
            if (thisYAxisInfos.Title) {
                var titleWidth = calculationData.singleLableTextWidth * thisYAxisInfos.Title.length;
                calculationData.YAxisInfosSingleWidth[index] = calculationData.YAxisInfosSingleWidth[index] > titleWidth ? calculationData.YAxisInfosSingleWidth[index] : titleWidth;
            }
            if (thisYAxisInfos.BottomTitle) {
                var bottomTitleWidth = calculationData.singleLableTextWidth * thisYAxisInfos.BottomTitle.length;
                calculationData.YAxisInfosSingleWidth[index] = calculationData.YAxisInfosSingleWidth[index] > bottomTitleWidth ? calculationData.YAxisInfosSingleWidth[index] : bottomTitleWidth;
            }

            //判断是否存在指定宽度
            if (thisYAxisInfos.SpecifyTitleWidth && parseFloat(thisYAxisInfos.SpecifyTitleWidth) > 0) {
                calculationData.YAxisInfosSingleWidth[index] = parseFloat((thisYAxisInfos.SpecifyTitleWidth / 300 * 96.00001209449).toFixed(2));
            }


            if (!thisYAxisInfos.MergeIntoLeft) {
                calculationData.YAxisInfosTotalWidth += calculationData.YAxisInfosSingleWidth[index];
            } else {
                //重新计算
                var thisYInfosWidth = calculationData.YAxisInfosSingleWidth.pop();
                var prevYInfosWidth = calculationData.YAxisInfosSingleWidth.pop();
                if (prevYInfosWidth < thisYInfosWidth) {
                    calculationData.YAxisInfosSingleWidth.push(thisYInfosWidth);
                    calculationData.YAxisInfosTotalWidth = calculationData.YAxisInfosTotalWidth - prevYInfosWidth + thisYInfosWidth;
                } else {
                    calculationData.YAxisInfosSingleWidth.push(prevYInfosWidth);
                }
            }

            if ((thisYAxisInfos.ShadowPointVisible === false || thisYAxisInfos.ShadowPointVisible === "false") && thisYAxisInfos.Name) {
                calculationData.ShadowPointVisible.push(thisYAxisInfos.Name);
            }



        }
        //判断宽度
        if (calculationData.FirstTextWidth > calculationData.YAxisInfosTotalWidth) {
            //计算超出多少,拼在每一个y轴上
            var calcWidth = (calculationData.FirstTextWidth - calculationData.YAxisInfosTotalWidth) / calculationData.YAxisInfosSingleWidth.length;
            calculationData.YAxisInfosSingleWidth.forEach((d, i) => {
                calculationData.YAxisInfosSingleWidth[i] = d + calcWidth;
            });
        } else {
            calculationData.FirstTextWidth = calculationData.YAxisInfosTotalWidth;
        }
        //页眉页脚除第一列外的单列宽度
        calculationData.HeaderAndFooterStepWidth = parseFloat((calculationData.InnerWidth - calculationData.FirstTextWidth) / (calculationData.NumOfDaysInOnePage).toFixed(2));

        //页眉页脚时刻步进宽度
        calculationData.TickTextStepWidth = calculationData.HeaderAndFooterStepWidth / calculationData.TickTextsData.length;

        // //计算横向的线条数量
        calculationData.Horizontallength = defaultData.GridYSplitNum * defaultData.GridYSpaceNum;

        calculationData.Verticalallength = (calculationData.TickTextsData.length ? calculationData.TickTextsData.length : 1) * calculationData.NumOfDaysInOnePage;
        //计算背景线区域横向步进高度
        calculationData.HorizontStepHeight = parseFloat((calculationData.YAxisInfosTotalHeight / calculationData.Horizontallength).toFixed(2));
        calculationData.BgLineWidth = calculationData.InnerWidth - calculationData.FirstTextWidth;
        //计算背景线区域纵向步进宽度
        calculationData.VerticalStepWidth = parseFloat((calculationData.BgLineWidth / calculationData.Verticalallength).toFixed(2));

        //小标题数据计算
        calculationData.AllHeaderLabels = [];
        //循环分组
        for (var i = 0; i < defaultData.HeaderLabels.length; i++) {
            var thisLabel = defaultData.HeaderLabels[i];
            //解决id不准确问题
            thisLabel['UID'] = 'dcHeaderLabels_' + i;

            var groupIndex = parseInt(thisLabel.GroupIndex);
            if (isNaN(groupIndex)) {
                groupIndex = 0;
            }
            if (calculationData.AllHeaderLabels[groupIndex] == null) {
                calculationData.AllHeaderLabels[groupIndex] = [];
            }
            calculationData.AllHeaderLabels[groupIndex].push(JSON.parse(JSON.stringify(thisLabel)));
        }

        //判断是否存在SpecifyTitleHeight属性
        if (defaultData.SpecifyTitleHeight && parseFloat(defaultData.SpecifyTitleHeight) > 0) {
            //存在直接计算
            calculationData.SpecifyTitleHeight = parseFloat((defaultData.SpecifyTitleHeight / 300 * 96.00001209449).toFixed(2));
        } else {
            calculationData.SpecifyTitleHeight = calculationData.TitleHeight * 1.5 + ((calculationData.AllHeaderLabels.length ? calculationData.AllHeaderLabels.length : 1) * calculationData.LableHeight);
        }

    },

    //对value进行计算
    ChangeValue: function (rootElement, data) {
        //对value进行数据矫正
        if (data && typeof data == "object" && data.Values) {
            WriterControl_DrawFu.DocumentOptions.DefaultData.Values = data.Values;
        }

        var defaultData = WriterControl_DrawFu.DocumentOptions.DefaultData.Config;
        var calculationData = WriterControl_DrawFu.DocumentOptions.CalculationData;

        var value = WriterControl_DrawFu.DocumentOptions.DefaultData.Values;
        value = value ? value : [];
        //通过value来计算日期
        var allDateTime = [];
        if (value && value.length > 0) {
            //循环数据
            for (var i = 0; i < value.length; i++) {
                var thisValue = value[i];
                var keywordLength = calculationData.StartDateKeyword;
                var endKeywordLength = calculationData.EndDateKeyword;
                //循环数据拿到时间
                if (thisValue.Datas && thisValue.Datas.length > 0) {
                    for (var j = 0; j < thisValue.Datas.length; j++) {
                        var thisData = thisValue.Datas[j];

                        if (thisData.Value == "NaN") {
                            thisData.Value = -10000;
                        }
                        if (thisData.Time && thisData.Time != "0001-01-01 00:00:00") {
                            thisData.Time = thisData.Time.replace("T", " ");
                            allDateTime.push(thisData.Time);
                        }
                        //判断关键字
                        if (keywordLength && Object.keys(keywordLength).length > 0 && thisData.Text) {
                            for (var k in keywordLength) {
                                //只对y轴数据进行判断
                                if (!thisValue.Name || !calculationData.YAxisInfosData["dc_" + thisValue.Name]) {
                                    continue;
                                }
                                if (thisData.Text.indexOf(k) >= 0) {

                                    //如果存在值
                                    //if (keywordLength[k]) {
                                    //    var newTime = new Date(thisData.Time).getTime();
                                    //    if (newTime < keywordLength[k]) {
                                    //        keywordLength = newTime;
                                    //    }
                                    //} else {
                                    //    //如果不存在值
                                    //    keywordLength[k] = new Date(thisData.Time).getTime();
                                    //}
                                    if (!Array.isArray(keywordLength[k])) {
                                        keywordLength[k] = [];
                                    }
                                    keywordLength[k].push(new Date(thisData.Time).getTime());
                                }
                            }
                        }
                        //再次判断结束关键字
                        if (endKeywordLength && Object.keys(endKeywordLength).length > 0 && thisData.Text) {
                            for (var k in endKeywordLength) {

                                //只对y轴数据进行判断
                                if (!thisValue.Name || !calculationData.YAxisInfosData["dc_" + thisValue.Name]) {
                                    continue;
                                }

                                if (thisData.Text.indexOf(k) >= 0) {
                                    //如果存在值
                                    if (endKeywordLength[k]) {
                                        var newTime = new Date(thisData.Time).getTime();
                                        if (newTime < endKeywordLength[k]) {
                                            endKeywordLength = newTime;
                                        }
                                    } else {
                                        //如果不存在值
                                        endKeywordLength[k] = new Date(thisData.Time).getTime();
                                    }
                                    //if (!Array.isArray(endKeywordLength[k])) {
                                    //    endKeywordLength[k] = [];
                                    //}
                                    //endKeywordLength[k].push(new Date(thisData.Time).getTime())
                                }
                            }
                        }

                        //处理图标（兼容部分cs）
                        switch (thisData.SpecifySymbolStyle) {
                            case "RoundDotIcon"://圆点
                                thisData.SpecifySymbolStyle = 'SolidCicle';
                                break;
                            case "HollowCicle"://空心圆
                            case "circular":
                            case "OpaqueHollowCicle":
                                thisData.SpecifySymbolStyle = 'HollowCicle';
                                break;
                            case "HollowTriangle": //空心三角形
                                thisData.SpecifySymbolStyle = 'HollowSolidTriangle';
                                break;
                            case "HollowTriangleReversed"://空心倒三角形
                                thisData.SpecifySymbolStyle = 'HollowSolidTriangleReversed';
                                break;
                        }
                    }
                    //进行排序,保证顺序
                    thisValue.Datas = thisValue.Datas.sort((b, c) => {
                        return new Date(b.Time).getTime() - new Date(c.Time).getTime();
                    });
                }
            }
        }
        //时间去重
        allDateTime = Array.from(new Set(allDateTime));
        allDateTime = allDateTime.sort((a, b) => {
            return new Date(a) - new Date(b);
        });
        //通过value中所有的时间和TickValuesData进行计算
        //对数据进行划分
        if (allDateTime.length != 0) {
            //获得第一天的时间
            var firstDate = new Date(allDateTime[0]);
            //获得最后一天的时间
            var lastDate = new Date(allDateTime[allDateTime.length - 1]);
            //页眉页脚数据开始时间
            calculationData.HeaderLineStartDateTime = new Date(firstDate.getFullYear() + "-" + (Number(firstDate.getMonth()) + 1) + "-" + firstDate.getDate(0) + " 00:00:00");
            //页眉页脚数据结束时间
            calculationData.HeaderLineEndDateTime = new Date(lastDate.getFullYear() + "-" + (Number(lastDate.getMonth()) + 1) + "-" + lastDate.getDate(0) + " 00:00:00");

            //在此处判断是否存在全局的开始和结束时间
            if (defaultData.SpecifyStartDate) {
                var isDate = new Date(defaultData.SpecifyStartDate).getTime();
                if (!isNaN(isDate)) {
                    //对只进行处理保证开始时间为00:00:00
                    isDate = new Date(isDate);
                    calculationData.HeaderLineStartDateTime = new Date(isDate.getFullYear() + "-" + (Number(isDate.getMonth()) + 1) + "-" + isDate.getDate(0) + " 00:00:00");
                }
            }
            if (defaultData.SpecifyEndDate) {
                var isDate = new Date(defaultData.SpecifyEndDate).getTime();
                if (!isNaN(isDate)) {
                    //对只进行处理保证开始时间为00:00:00
                    isDate = new Date(isDate);
                    calculationData.HeaderLineEndDateTime = new Date(isDate.getFullYear() + "-" + (Number(isDate.getMonth()) + 1) + "-" + isDate.getDate(0) + " 00:00:00");
                }
            }

            //判断大小设置
            if (calculationData.HeaderLineStartDateTime > calculationData.HeaderLineEndDateTime) {
                calculationData.HeaderLineStartDateTime = calculationData.HeaderLineStartDateTime + calculationData.HeaderLineEndDateTime;
                calculationData.HeaderLineEndDateTime = calculationData.HeaderLineStartDateTime - calculationData.HeaderLineEndDateTime;
                calculationData.HeaderLineStartDateTime = calculationData.HeaderLineStartDateTime - calculationData.HeaderLineEndDateTime;
            }
            calculationData.HeaderLineStartDateTime = new Date(calculationData.HeaderLineStartDateTime);
            calculationData.HeaderLineEndDateTime = new Date(calculationData.HeaderLineEndDateTime);

            //时间差值
            calculationData.IntervalDateTime = (calculationData.HeaderLineEndDateTime - calculationData.HeaderLineStartDateTime) / 1000 / 60 / 60 / 24;
            //[DUWRITER5_0-3608] 20240920 lxy 解决日期计算问题。js计算日期差值时,少计算一天，这里直接粗暴的+1
            calculationData.IntervalDateTime += 1;

            //通过时间差值结算出总共有多少页
            calculationData.TotalPageNumber = Math.ceil(calculationData.IntervalDateTime / calculationData.NumOfDaysInOnePage);
            calculationData.TotalPageNumber = calculationData.TotalPageNumber ? calculationData.TotalPageNumber : 1;
            //修改值的数据
            WriterControl_DrawFu.DocumentOptions.DefaultData.NumOfPages = calculationData.TotalPageNumber;

            if (calculationData.DCType == "DCTemperatureDesignControlForWASM") {
                calculationData.HeaderLineEndDateTime = new Date(calculationData.HeaderLineStartDateTime.getTime() + (1000 * 60 * 60 * 24 * calculationData.NumOfDaysInOnePage));
                calculationData.TotalPageNumber = 1;
            } else {
                //当存在viewMode的时候处理自此处判断位置并修改页码数
                calculationData.GetViewMode = rootElement.getAttribute("viewmode");
                if (calculationData.GetViewMode && typeof calculationData.GetViewMode == "string") {
                    calculationData.GetViewMode = calculationData.GetViewMode.toLowerCase().trim();
                }
                calculationData.HasPageIndex = rootElement.getAttribute("pageindex");
                if (calculationData.HasPageIndex && typeof calculationData.HasPageIndex == "string") {
                    calculationData.HasPageIndex = parseInt(calculationData.HasPageIndex);
                    if (isNaN(calculationData.HasPageIndex)) {
                        calculationData.HasPageIndex = 1;
                    }
                }
                //给当前页面坐标默认值
                if (!calculationData.HasPageIndex || typeof calculationData.HasPageIndex != "number") {
                    calculationData.HasPageIndex = 1;
                }

                //如果存在表格数据，则获取表格最大页数，将其与总页数比较
                var TableTotalPageNumber = WriterControl_DrawTable.GetTableTotalPageNumber();
                calculationData.TotalPageNumber = Math.max(calculationData.TotalPageNumber, TableTotalPageNumber);


                //如果超出最大页数,则直接显示为最后一页
                if (calculationData.HasPageIndex > calculationData.TotalPageNumber) {
                    calculationData.HasPageIndex = calculationData.TotalPageNumber;
                }
                if (calculationData.GetViewMode && calculationData.GetViewMode.toLowerCase().trim() == "singlepage") {
                    WriterControl_DrawFu.DocumentOptions.DefaultData.ViewMode = "SinglePage";
                    //通过页数计算开始和结束日期
                    if (calculationData.HasPageIndex != 1) {
                        //[DUWRITER5_0-3469] 20240829 lxy 解决单页模式下页码展示不正确问题
                        // calculationData.HasPageIndex--;
                        //不是第一页计算开始时间
                        calculationData.HeaderLineStartDateTime = new Date(calculationData.HeaderLineStartDateTime.getTime() + (1000 * 60 * 60 * 24 * calculationData.NumOfDaysInOnePage * (calculationData.HasPageIndex - 1)));
                    }
                    if (calculationData.HasPageIndex != calculationData.TotalPageNumber) {
                        //不是最后一页计算结束时间
                        calculationData.HeaderLineEndDateTime = new Date(calculationData.HeaderLineStartDateTime.getTime() + (1000 * 60 * 60 * 24 * calculationData.NumOfDaysInOnePage));
                    }
                    calculationData.IntervalDateTime = calculationData.NumOfDaysInOnePage;
                    rootElement.setAttribute("pageindex", calculationData.HasPageIndex || 1);
                    WriterControl_DrawFu.DocumentOptions.DefaultData.PageIndex = calculationData.HasPageIndex;
                    calculationData.TotalPageNumber = 1;
                } else if (calculationData.GetViewMode && calculationData.GetViewMode.toLowerCase().trim() == "temperature") {
                    WriterControl_DrawFu.DocumentOptions.DefaultData.ViewMode = "Temperature";
                } else {
                    WriterControl_DrawFu.DocumentOptions.DefaultData.ViewMode = "Page";
                    rootElement.setAttribute("pageindex", calculationData.HasPageIndex || 1);
                    WriterControl_DrawFu.DocumentOptions.DefaultData.PageIndex = calculationData.HasPageIndex;
                }
            }

        } else {
            if (calculationData.DCType != "DCTemperatureDesignControlForWASM") {
                //当没有体温单画点数据时，则使用表格数据
                var tableTotalPageNumber = data.BottomTableGroups ? WriterControl_DrawTable.GetTableTotalPageNumber() : 0;
                calculationData.GetViewMode = rootElement.getAttribute("viewmode") || "page";//视图模式

                if (calculationData.GetViewMode === "singlepage") {
                    //单页展示时
                    calculationData.TotalPageNumber = 1;//渲染总页数
                } else {
                    //多页展示
                    calculationData.TotalPageNumber = tableTotalPageNumber || 1;//渲染总页数
                }

                //当前展示的页码
                calculationData.HasPageIndex = rootElement.getAttribute("pageindex") || 1;//当前展示的页码
                if (calculationData.HasPageIndex && typeof calculationData.HasPageIndex == "string") {
                    calculationData.HasPageIndex = parseInt(calculationData.HasPageIndex);
                    if (isNaN(calculationData.HasPageIndex)) {
                        calculationData.HasPageIndex = 1;
                    }
                }
                //如果超出表格最大值，则使用表格总页数
                if (calculationData.HasPageIndex > tableTotalPageNumber) {
                    calculationData.HasPageIndex = tableTotalPageNumber;
                    rootElement.setAttribute("pageindex", calculationData.HasPageIndex || 1);
                }
            }

        }
        //开始日期毫秒数
        var startDate = calculationData.HeaderLineStartDateTime.getTime();
        //旧的开始日期毫秒数
        var oldStartDate = startDate;
        //结束日期毫秒数
        var endDate = calculationData.HeaderLineEndDateTime.getTime();
        //循环次数
        var whileIndex = 0;
        //记录月份
        var thisMount = 0;
        //记录年份
        var thisYear = 0;
        // console.log(calculationData.TickValuesData);
        while (startDate <= endDate) {
            // console.log(111)
            for (var z = 0; z < calculationData.TickValuesData.length; z++) {
                var calc = 0;
                if (z == 0) {
                    calc = parseInt(calculationData.TickValuesData[z]);
                    startDate = oldStartDate + 1000 * 60 * 60 * 24 * whileIndex;
                    //把所有的日期写入
                    var thisDate = new Date(startDate);
                    //根据DateFormatString,DateFormatStringForCrossMonth;DateFormatStringForCrossWeek;DateFormatStringForCrossYear;DateFormatStringForFirstIndexFirstPage;DateFormatStringForFirstIndexOtherPage写入数据日期数
                    var newYear = thisDate.getFullYear();
                    var newMonth = Number(thisDate.getMonth()) + 1;
                    var newDay = thisDate.getDate(0);
                    if (calculationData.AllDateData.length == 0) {
                        var firstYear = defaultData.DateFormatStringForFirstIndexFirstPage;
                        if (!firstYear || (firstYear.indexOf("yyyy") < 0 && firstYear.indexOf("MM") < 0 || firstYear.indexOf("dd") < 0)) {
                            firstYear = defaultData.DateFormatString;
                            if (firstYear == null) {
                                firstYear = "yyyy-MM-dd";
                            }
                        }
                        var firstData = firstYear.replace("yyyy", newYear);
                        firstData = firstData.replace("MM", newMonth);
                        firstData = firstData.replace("dd", newDay);
                        calculationData.AllDateData.push(firstData);
                        thisMount = newMonth;
                        thisYear = newYear;
                    } else {
                        if (thisYear != newYear) {
                            //判断DateFormatStringForCrossYear
                            //var crossYear = defaultData.DateFormatStringForCrossYear;
                            var crossYear = "yyyy-MM-dd";
                            if (!crossYear || (crossYear.indexOf("yyyy") < 0 && crossYear.indexOf("MM") < 0 && crossYear.indexOf("dd") < 0)) {
                                crossYear = "yyyy-MM-dd";
                            }
                            var crossYearData = crossYear.replace("yyyy", newYear);
                            crossYearData = crossYearData.replace("MM", newMonth);
                            crossYearData = crossYearData.replace("dd", newDay);
                            calculationData.AllDateData.push(crossYearData);

                        } else if (thisMount != newMonth) {
                            //var crossMount = defaultData.DateFormatStringForCrossMonth;
                            var crossMount = "yyyy-MM-dd";
                            if (!crossMount || (crossMount.indexOf("yyyy") < 0 && crossMount.indexOf("MM") < 0 && crossMount.indexOf("dd") < 0)) {
                                crossMount = "MM-dd";
                            }
                            var crossMountData = crossMount.replace("yyyy", newYear);
                            crossMountData = crossMountData.replace("MM", newMonth);
                            crossMountData = crossMountData.replace("dd", newDay);
                            calculationData.AllDateData.push(crossMountData);
                        } else {
                            //var crossDay = defaultData.DateFormatStringForCrossWeek;
                            //判断是否为单页的首位如果是显示年月日
                            if (calculationData.AllDateData.length % calculationData.NumOfDaysInOnePage == 0) {
                                var crossDay = "yyyy-MM-dd";
                            } else {
                                var crossDay = "dd";
                            }
                            if (!crossDay || (crossDay.indexOf("yyyy") < 0 && crossDay.indexOf("MM") < 0 && crossDay.indexOf("dd") < 0)) {
                                crossDay = "dd";
                            }
                            var crossDayData = crossDay.replace("yyyy", newYear);
                            crossDayData = crossDayData.replace("MM", newMonth);
                            crossDayData = crossDayData.replace("dd", newDay);
                            calculationData.AllDateData.push(String(crossDayData));
                        }
                        thisMount = newMonth;
                        thisYear = newYear;
                    }
                } else {
                    calc = parseInt(calculationData.TickValuesData[z]) - parseInt(calculationData.TickValuesData[z - 1]);
                }
                //差值
                var count = 1000 * 60 * 60 * calc;
                startDate += count;
                calculationData.AllTimeData.push(startDate);
            }
            whileIndex++;
        }
        calculationData.AllTimeData = Array.from(new Set(calculationData.AllTimeData));
        // //console.log(calculationData.AllTimeData);
        //拼接页眉页脚一同获取数据
        var headerFooterLines = [...defaultData.HeaderLines, ...defaultData.FooterLines];
        // console.log(headerFooterLines, value);

        //循环页眉页脚
        headerFooterLines.forEach((a, i) => {
            if (a.Name && (a.ValueType == "Data" || a.ValueType == "TickText" || a.ValueType == "Text")) {
                //循环value拿到值
                var thisValue = value.find(b => {
                    if (b.Name && b.Name == a.Name) {
                        return b;
                    }
                });
                if (thisValue && thisValue.Datas) {
                    // //console.log(thisValue);
                    //拿到每一页的时间数
                    calculationData.HeaderFooterValueArr[a.Name] = [];

                    var pageDateNum = (calculationData.TickValuesData.length - 1) * calculationData.NumOfDaysInOnePage;
                    for (var j = 0; j < calculationData.TotalPageNumber; j++) {
                        var thisBand = calculationData.AllTimeData.slice(pageDateNum * j, (pageDateNum * j) + pageDateNum + 1);
                        var maxScaleWidth = (thisBand.length - 1) * calculationData.TickTextStepWidth;
                        //根据分组修改数据
                        thisBand = thisBand.filter((b, c) => {
                            if (c % a.TickStep == 0) {
                                return b;
                            }
                        });
                        //保证顺序
                        thisBand = thisBand.sort((b, c) => {
                            return b - c;
                        });
                        // //console.log(thisBand);
                        var SmallDate = thisBand[0];
                        var BiggerDate = thisBand[thisBand.length - 1];
                        //分段写入标尺
                        // var xScale = d3.scaleLinear([SmallDate, BiggerDate], [0, maxScaleWidth]);
                        // //console.log(maxScaleWidth)
                        if (a.LayoutType == "Free") {
                            var xScale = d3.scaleLinear([SmallDate, BiggerDate], [0, maxScaleWidth]);
                        } else {
                            var xScale = d3.scaleBand(thisBand, [0, maxScaleWidth]).paddingInner(1);
                        }
                        // var svg3 = d3.select("[dctype=page]");
                        // var g3 = svg3.append('g')
                        //     .attr("transform",`translate(${100},${100})`)
                        //     //.attr("id","maingroup")
                        // var xAxis = d3.axisBottom(xScale).tickSize(-svg3.attr("width")).tickPadding(10)
                        // g3.append('g').call(xAxis).attr("transform",`translate(0,${svg3.attr("height")})`)
                        // console.warn("111111111111111111111")
                        //根据日期组成对象
                        for (var k = 0; k < thisValue.Datas.length; k++) {
                            var thisData = thisValue.Datas[k];

                            var hasText = thisData.Text ? thisData.Text : thisData.Value == -10000 || !thisData.Value ? "" : String(thisData.Value);
                            if (hasText == "") {
                                continue;
                            }

                            var thisTime = new Date(thisData.Time).getTime();
                            var endTime = new Date(thisData.EndTime).getTime();
                            endTime = endTime == 978278400000 ? null : endTime;
                            if (thisTime >= SmallDate && thisTime < BiggerDate) {
                                if (a.LayoutType == "Free") {
                                    //thisTime时间不需要改变,计算endTime
                                    if (endTime == null) {
                                        var nextIndex = 1;
                                        //判断是否存在下一个数据,用下一个数据的开始位置作为结束位置
                                        while (true) {
                                            var nextData = thisValue.Datas[k + nextIndex];
                                            nextIndex++;
                                            if (nextData) {
                                                endTime = new Date(nextData.Time).getTime();
                                                //判断是否在
                                                if (endTime >= SmallDate && endTime < BiggerDate) {
                                                    break;
                                                } else if (endTime > BiggerDate) {
                                                    endTime = BiggerDate;
                                                    break;
                                                }
                                            } else {
                                                //把endtime写到最后并在下一页开启新数据连接 
                                                endTime = BiggerDate;
                                                // thisValue.Datas.push
                                                break;
                                            }
                                        }
                                    }
                                } else {
                                    try {
                                        thisBand.forEach((e, d) => {
                                            if (e > thisTime) {
                                                if (d == 0) {
                                                    thisTime = thisBand[0];
                                                    throw new error();
                                                } else {
                                                    thisTime = thisBand[d - 1];
                                                    throw new error();
                                                }
                                            } else if (e == thisTime) {
                                                thisTime = thisBand[d];
                                                throw new error();
                                            }
                                        });
                                    } catch (err) { };

                                }
                            } else {
                                continue;
                            }

                            if (!calculationData.HeaderFooterValueArr[a.Name][j]) {
                                calculationData.HeaderFooterValueArr[a.Name][j] = {};
                            }
                            var thisIndex = xScale(thisTime);
                            if (endTime) {
                                endTime = xScale(endTime);
                            }
                            // //console.log(calculationData.HeaderFooterValueArr[a.Name][j]["dc_" + thisIndex]);
                            if (calculationData.HeaderFooterValueArr[a.Name][j]["dc_" + thisIndex]) {
                                //if (a.LayoutType == "HorizCascade") {
                                calculationData.HeaderFooterValueArr[a.Name][j]["dc_" + thisIndex].text.push(hasText);
                                calculationData.HeaderFooterValueArr[a.Name][j]["dc_" + thisIndex].data.push(thisData);
                                calculationData.HeaderFooterValueArr[a.Name][j]["dc_" + thisIndex].title.push(thisData.Time + " " + a.Title);
                                //}
                            } else {
                                //直接绘制矩形然后绘制文本就行
                                calculationData.HeaderFooterValueArr[a.Name][j]["dc_" + thisIndex] = {
                                    "basisWidth": calculationData.TickTextStepWidth * a.TickStep,
                                    "startIndex": thisIndex,
                                    "endIndex": endTime,
                                    "text": [hasText],
                                    "title": [thisData.Time + " " + a.Title],
                                    "data": [thisData]
                                };
                            }
                        }

                    }
                }

            }
        });

    },

    //绘制svg
    DrawSvg: function (rootElement) {
        //  console.log("DrawSvg");
        var that = this;
        var defaultData = WriterControl_DrawFu.DocumentOptions.DefaultData.Config;
        var calculationData = WriterControl_DrawFu.DocumentOptions.CalculationData;
        var svgEle = WriterControl_DrawFu.DocumentOptions.SVGElement;

        //查找到pageContainer元素
        var pageContainer = rootElement.pageContainer;
        //清空包裹元素所有的子元素,重新绘制
        pageContainer.innerHTML = "";

        //创建span元素并展示title
        var titleSpan = document.createElement("span");
        titleSpan.id = "dc_titleSpan";
        titleSpan.style.display = "none";
        titleSpan.style.padding = "3px 10px";
        titleSpan.style.backgroundColor = "#fff";
        titleSpan.style.borderRadius = "3px";
        titleSpan.style.fontFamily = "宋体";
        titleSpan.style.fontSize = "9pt";
        titleSpan.style.position = "absolute";
        titleSpan.style.boxShadow = "0px 0px 5px rgba(0, 0, 0, 0.3)";
        pageContainer.appendChild(titleSpan);

        //var borderSpan = document.createElement("div");
        //borderSpan.id = "dc_borderSpan";
        ////borderSpan.style.display = "none";
        //borderSpan.style.position = "absolute";
        //borderSpan.style.boxShadow = "inset 0px 0px 15px rgba(0, 0, 0, 0.3)"
        //borderSpan.style.top = "0px";
        //borderSpan.style.left = "0px";
        //borderSpan.style.width = "100px";
        //borderSpan.style.height = "100px";
        //pageContainer.appendChild(borderSpan);
        //判断是否存在zoomrate
        calculationData.ZoomRate = rootElement.getAttribute("zoomrate");
        calculationData.ZoomRate = parseFloat(calculationData.ZoomRate);
        calculationData.ZoomRate = isNaN(calculationData.ZoomRate) ? 1 : calculationData.ZoomRate;
        calculationData.ZoomRate = calculationData.ZoomRate ? calculationData.ZoomRate : 1;
        //calculationData[i] = calculationData[i] * calculationData.ZoomRate;

        //绘制svg
        for (var i = 0; i < calculationData.TotalPageNumber; i++) {
            //最外侧svg
            var svg = d3
                .create('svg')
                .attr('dctype', 'page')
                .attr('viewBox', "0 0 " + calculationData.PaperWidth + " " + (calculationData.OldPaperHeight ? calculationData.OldPaperHeight : calculationData.PaperHeight))//增加一个视口
                .attr('width', calculationData.PaperWidth)
                .attr('height', calculationData.OldPaperHeight ? calculationData.OldPaperHeight : calculationData.PaperHeight)//如果是新生儿体温单就需要用保留的高度
                .attr('native-width', calculationData.PaperWidth)
                .attr('native-height', calculationData.OldPaperHeight ? calculationData.OldPaperHeight : calculationData.PaperHeight)//如果是新生儿体温单就需要用保留的高度
                .attr('style', "border-radius: 6px;margin: 6px auto; vertical-align: top;background-color: window;border: 1px solid #E6E6E6; cursor: default;display: block;"); //box-sizing:border-box;

            if (svgEle[i] == null) {
                svgEle[i] = {};
            }
            svgEle[i].page = svg;
            var thisSvgEle = svg.node();
            pageContainer.appendChild(thisSvgEle);

            //在此处计算出每个svg的top值
            thisSvgEle.setAttribute("dcTop", calculationData.SvgScrollTop);
            calculationData.SvgScrollTop += thisSvgEle.clientHeight + 7;

            //[DUWRITER5_0-3574] 20240919 lxy 设置注册码信息
            var aboutMessage = "都昌信息科技有限公司";

            //获取新的注册信息
            if (rootElement.TemperatureRegisterCode) {
                const regex = /\[用户名:(.*?)\]/;
                const match = regex.exec(rootElement.TemperatureRegisterCode);
                aboutMessage = match && match.length > 1 ? match[1] : rootElement.TemperatureRegisterCode;
            }

            var tooltip = svg.append('g').style('pointer-events', 'none');
            //绘制文本
            tooltip.append('text')
                .attr('style', `font-family:宋体;font-size:9pt;line-height: 1`)
                .attr('dctype', 'typesign')
                .attr('id', 'dc_typesign' + i)//增加id属性，方便后续修改注册码
                .attr('x', 12)
                .attr('y', 12)
                .text(aboutMessage);

            //thisSvgEle.onpointerenter = function (e) {
            //    //console.log("onpointerenter",e)
            //}
            thisSvgEle.addEventListener("pointermove", function (e) {
                ////console.log("onpointermove", e)
                var targetEle = e.target;
                var hasTitle = targetEle.getAttribute("title");
                if (hasTitle && hasTitle.length > 0) {
                    var currentTarget = e.currentTarget;
                    //var thisTop = parseFloat(currentTarget.getAttribute("dcTop"));
                    var pagePosition = rootElement.pageContainer.getBoundingClientRect();
                    var svgPosition = currentTarget.getBoundingClientRect();
                    titleSpan.style.display = "block";
                    titleSpan.style.top = e.offsetY + 30 + (svgPosition.y - pagePosition.y) + rootElement.pageContainer.scrollTop + "px";
                    titleSpan.style.left = e.offsetX + (svgPosition.x - pagePosition.x) + "px";
                    titleSpan.innerText = hasTitle;
                } else {
                    titleSpan.style.display = "none";
                }
            });
            //体温单点击事件
            thisSvgEle.addEventListener("click", function (e) {
                var dcType = rootElement.getAttribute("dctype");//用于判断是否为设计器模式
                if (dcType === "DCTemperatureDesignControlForWASM") {
                    //设计器模式
                    if (calculationData.UIDElePositon && calculationData.UIDElePositon.length > 0) {
                        var hasEle = calculationData.UIDElePositon.find((item, index) => {
                            var y1 = item.Positon.svgTop;
                            var x1 = item.Positon.svgLeft;
                            var y2 = y1 + item.Positon.height;
                            var x2 = x1 + item.Positon.width;
                            if (e.offsetX >= x1 && e.offsetX <= x2 && e.offsetY >= y1 && e.offsetY <= y2) {
                                WriterControl_DrawFu.MoveBorderRect(rootElement, item.UID);
                                var thisProp = rootElement.GetInternalProperties(item.UID);
                                rootElement.InnerRaiseEvent("EventStructureClick", thisProp, item.Type);
                                return true;
                            }
                        });
                        if (!hasEle) {
                            WriterControl_DrawFu.MoveBorderRect(rootElement);
                            //如果存在在返回全局的属性
                            var thisProp = rootElement.GetDocumentConfigProperties();
                            rootElement.InnerRaiseEvent("EventStructureClick", thisProp, "DocumentConfig");
                        }
                    }
                } else {
                    //非设计器模式
                    // [DUWRITER5_0-3330] lxy 20240813 点击背景线时，触发画点区域的点击事件。新增EventBackgroundLineClick事件
                    let dcbgLineG = this.querySelector('[dctype=dcbgLineG]');
                    if (dcbgLineG) {
                        let dcbgLineGRect = dcbgLineG.getBoundingClientRect();
                        let dcbgLineGRectWidth = dcbgLineGRect.width;//矩形的宽度
                        let dcbgLineGRectHeight = dcbgLineGRect.height;//矩形的高度
                        //背景线区域的偏移值
                        let dcbgLineGTransform = dcbgLineG.getAttribute('transform');
                        if (dcbgLineGTransform) {
                            const match = dcbgLineGTransform.match(/translate\((\d+\.?\d*),(\d+\.?\d*)\)/);
                            if (match) {
                                const x = Number(match[1]) || 0;
                                const y = Number(match[2]) || 0;
                                if (e.offsetX >= x && e.offsetX <= x + dcbgLineGRectWidth && e.offsetY >= y && e.offsetY <= y + dcbgLineGRectHeight) {
                                    // //console.log('点击了背景线');//触发事件EventBackgroundLineClick
                                    rootElement.InnerRaiseEvent("EventBackgroundLineClick");
                                    return;
                                }
                            }
                        }
                    }
                }
            });

            pageContainer.addEventListener("wheel", (event) => {
                if (event.ctrlKey === true) {
                    clearTimeout(rootElement.wheelTime);
                    rootElement.wheelTime = setTimeout(() => {
                        //获取到设置的大小
                        var hasZoomRate = rootElement.getAttribute("zoomrate");
                        hasZoomRate = parseFloat(hasZoomRate);
                        hasZoomRate = isNaN(hasZoomRate) ? 1 : hasZoomRate;
                        hasZoomRate = hasZoomRate ? hasZoomRate : 1;
                        if (event.wheelDelta > 0) {
                            hasZoomRate += 0.1;
                        } else if (event.wheelDelta < 0) {
                            hasZoomRate -= 0.1;
                        }
                        WriterControl_DrawFu.SetZoomRate(rootElement, hasZoomRate);
                        //WriterControl_DrawFu.CreateTemperatureInit(rootElement, WriterControl_DrawFu.DocumentOptions.DefaultData, "EventTemperatureChangeZoomRate",);
                    }, 100);
                    event.stopPropagation();
                    event.preventDefault();
                    return false;
                }
            });

            //调用各层的绘制方法
            WriterControl_DrawFu.DrawTitle(svg, calculationData, defaultData, svgEle, i);
            WriterControl_DrawFu.DrawPageNumber(svg, calculationData, defaultData, svgEle, i);
            WriterControl_DrawFu.DrawFooterDescription(svg, calculationData, defaultData, svgEle, i);
            WriterControl_DrawFu.DrawTableContainer(svg, calculationData, defaultData, svgEle, i);
            WriterControl_DrawFu.DrawHeaderFooterLine(rootElement, svg, calculationData, defaultData, svgEle, i, "HeaderLines");
            WriterControl_DrawFu.DrawHeaderFooterLine(rootElement, svg, calculationData, defaultData, svgEle, i, "FooterLines");
            WriterControl_DrawFu.DrawYAxisInfos(svg, calculationData, defaultData, svgEle, i);
            WriterControl_DrawFu.DrawbgLine(svg, calculationData, defaultData, svgEle, i);
            WriterControl_DrawFu.DrawLabel(svg, calculationData, defaultData, svgEle, i, WriterControl_DrawFu.DocumentOptions.DefaultData.Parameters);
            WriterControl_DrawFu.DrawValuePath(svg, calculationData, defaultData, svgEle, i, WriterControl_DrawFu.DocumentOptions.DefaultData.Values);
            //绘制转院、转床、转病区等特殊的眉栏值修改
            WriterControl_DrawFu.DrawHeadLable(svg, calculationData, defaultData, svgEle, i, WriterControl_DrawFu.DocumentOptions.DefaultData.Parameters);




            //监听元素
            var targetContent = svg.node();
            // 观察器的配置（需要观察什么变动）attributes: true,
            const config = { childList: true, subtree: true, characterData: true, characterDataOldValue: true };
            // 当观察到变动时执行的回调函数
            const callback = function (mutationsList, observer) {
                ////console.log(mutationsList, observer);
                if (mutationsList && mutationsList.length > 0) {
                    for (var j = 0; j < mutationsList.length; j++) {
                        if (mutationsList[j].removedNodes && mutationsList[j].removedNodes.length > 0) {
                            for (var z = 0; z < mutationsList[j].removedNodes.length; z++) {
                                var thisRemoveNode = mutationsList[j].removedNodes[z];
                                //为纯文本
                                if (thisRemoveNode.nodeType == 1 && thisRemoveNode.getAttribute("dctype") == "typesign") {
                                    alert(window.__DCSR.DeleteRegister);
                                    d3.select(mutationsList[j].target).append('text')
                                        .attr('style', `font-family:宋体;font-size:9pt;line-height: 1`)
                                        .attr('dctype', 'typesign')
                                        .attr('id', 'dc_typesign' + i)//增加id属性，方便后续修改注册码
                                        .attr('x', 12)
                                        .attr('y', 12)
                                        .text(aboutMessage);
                                }
                            }
                        }
                    }
                }
            };
            // 创建一个观察器实例并传入回调函数
            const observer = new MutationObserver(callback);
            // 以上述配置开始观察目标节点
            observer.observe(targetContent, config);

            thisSvgEle.style.zoom = calculationData.ZoomRate;

        }




        //防止有自定义的title被篡改，重新设置一遍title值
        var FooterLines = defaultData.FooterLines || [];
        var FooterLinesChangeDateSourceName = calculationData.FooterLinesChangeDateSourceName || {};
        for (var j = 0; j < FooterLines.length; j++) {
            if (FooterLines[j].Name) {
                FooterLines[j].Title = FooterLinesChangeDateSourceName[FooterLines[j].Name] || FooterLines[j].Title;
            }
        }
        //防止有自定义的title被篡改，重新设置一遍title值
        var HeaderLines = defaultData.HeaderLines || [];
        var HeaderLinesChangeDateSourceName = calculationData.HeaderLinesChangeDateSourceName || {};
        for (var j = 0; j < HeaderLines.length; j++) {
            if (HeaderLines[j].Name) {
                HeaderLines[j].Title = HeaderLinesChangeDateSourceName[HeaderLines[j].Name] || HeaderLines[j].Title;
            }
        }



        WriterControl_DrawFu.ComputeAllUIDPosition(rootElement, svgEle[0]);
        // //console.log(WriterControl_DrawFu.DocumentOptions);

        //判断大小改变
        var resizeObserver = new ResizeObserver((e) => {
            //清掉
            var borderSpan = rootElement.querySelector("#dc_borderSpan");
            if (borderSpan && !rootElement.dc_MoveBorderRect) {
                borderSpan.style.display = "none";
            }
            //clearTimeout(pageContainer.timeout);
            //pageContainer.timeout = setTimeout(() => {
            //    WriterControl_DrawFu.ComputeAllUIDPosition(rootElement, svgEle[0]);
            //}, 500);
        });

        resizeObserver.observe(pageContainer);

    },

    //进行缩放
    SetZoomRate: function (rootElement, newRate) {
        var calculationData = WriterControl_DrawFu.DocumentOptions.CalculationData;
        rootElement.setAttribute("zoomRate", newRate);

        //获取到所有的svg元素
        var allSvg = rootElement.querySelectorAll("[dctype=page]");
        for (var i = 0; i < allSvg.length; i++) {
            allSvg[i].style.zoom = newRate;
            calculationData.ZoomRate = newRate;
        }

        var borderSpan = rootElement.querySelector("#dc_borderSpan");
        if (borderSpan && !rootElement.dc_MoveBorderRect) {
            borderSpan.style.display = "none";
        }
        //缩放后重新计算所有元素的位置
        setTimeout(() => {
            WriterControl_DrawFu.CreateTemperatureInit(rootElement, WriterControl_DrawFu.DocumentOptions.DefaultData, "EventTemperatureChangeZoomRate",);
        }, 200);
    },

    //绘制标题
    DrawTitle: function (svg, calculationData, defaultData, svgEle, i) {
        //大标题包裹元素
        var dcTitleG = svg
            .append('g')
            .attr('dctype', "dcTitleG")
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin},${calculationData.TopMargin})`
            );
        svgEle[i].dcTitleG = dcTitleG;
        //绘制文本
        if (defaultData.Title && defaultData.Title.length > 0) {
            dcTitleG.append('text')
                .attr('style', `font-family:${defaultData.BigTitleFontName || defaultData.FontName || '宋体'} ;font-weight:${defaultData.BigTitleFontBold ? 900 : 0}; font-size:${defaultData.BigTitleFontSize}pt;text-anchor: middle;line-height: 1`)
                .attr('dctype', 'dcTitle')
                .attr('x', calculationData.InnerWidth / 2)
                .attr('y', () => calculationData.TitleHeight)
                .text(defaultData.Title);
        }
    },

    //绘制页码
    DrawPageNumber: function (svg, calculationData, defaultData, svgEle, i) {
        //页码包裹元素
        var dcPageNumberG = svg
            .append('g')
            .attr('dctype', "dcPageNumberG")
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin},${(calculationData.OldPaperHeight ? calculationData.OldPaperHeight : calculationData.PaperHeight) - calculationData.BottomMargin})`
            );
        svgEle[i].dcPageNumberG = dcPageNumberG;
        //页码元素
        var pageNumber = defaultData.PageIndexText.replace("[%pageindex%]", "");
        pageNumber = defaultData.PageIndexText.replace("[%pagecount%]", "");
        var pageNumberEle = dcPageNumberG.append('text')
            .attr('style', `font-family:${defaultData.FontName || '宋体'} ;font-size:${defaultData.FontSize}pt;text-anchor: middle;line-height: 1.5`)
            .attr('dctype', 'dcPageNumber')
            .attr('x', calculationData.InnerWidth / 2)
            .attr('y', 0)
            .text(pageNumber);

        //获取页码的高度
        calculationData.LableHeight = dcPageNumberG.node().getBBox().height * 1.5;
        //计算单个字符宽度
        calculationData.singleLableTextWidth = dcPageNumberG.node().getBBox().width / pageNumber.length;
        pageNumber = defaultData.PageIndexText.replace("[%pageindex%]", i + 1);
        pageNumber = pageNumber.replace("[%pagecount%]", calculationData.TotalPageNumber);
        if (calculationData.GetViewMode == "singlepage") {
            pageNumber = defaultData.PageIndexText.replace("[%pageindex%]", calculationData.HasPageIndex || 1);
        }
        pageNumberEle.text(pageNumber);
    },

    //绘制说明元素
    DrawFooterDescription: function (svg, calculationData, defaultData, svgEle, i) {
        //说明元素包裹元素
        var dcFooterDescriptionG = svg
            .append('g')
            .attr('dctype', "dcFooterDescriptionG")
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin},${calculationData.TopMargin + calculationData.InnerHeight - calculationData.LableHeight})`
            );
        svgEle[i].dcFooterDescriptionG = dcFooterDescriptionG;
        dcFooterDescriptionG.append('text')
            .attr('style', `font-family:${defaultData.FontName || '宋体'} ;font-size:${defaultData.FontSize}pt;line-height: 1.5`)
            .attr('dctype', 'dcFooterDescription')
            .attr('x', 0)
            .attr('y', 0)
            .text(defaultData.FooterDescription);

        //获取页码的高度
        calculationData.FooterDescriptionHeight = dcFooterDescriptionG.node().getBBox().height * 1.5;
    },


    //绘制文本标签
    DrawLabel: function (svg, calculationData, defaultData, svgEle, i, parameters) {
        //文本标签包裹元素
        var dcLabelG = svg
            .append('g')
            .attr('dctype', "dcLabelsG")
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin},${calculationData.TopMargin})`
            );
        svgEle[i].dcLabelG = dcLabelG;
        //绘制文本
        if (defaultData.Labels && defaultData.Labels.length > 0) {
            //由于存在图片和文本的区别,使用外部循环而不使用selectAll
            for (var j = 0; j < defaultData.Labels.length; j++) {
                var thisLabel = defaultData.Labels[j];
                ////console.log(thisLabel);
                var uid = 'dcLabels_' + j;
                thisLabel.UID = uid;
                var thisLabelG = dcLabelG.append('g')
                    .attr("uid", uid)
                    .attr(
                        'transform',
                        `translate(0,0)`
                    );
                //文本标签的位置单独计算
                var width = parseFloat((thisLabel.Width / 300 * 96.00001209449).toFixed(2));
                var height = parseFloat((thisLabel.Height / 300 * 96.00001209449).toFixed(2));
                var top = parseFloat((thisLabel.Top / 300 * 96.00001209449).toFixed(2));
                var left = parseFloat((thisLabel.Left / 300 * 96.00001209449).toFixed(2));
                //为了绘制边框
                if (thisLabel.Text == "疼痛强度" || thisLabel.Text == "疼痛评分") {
                    var rect = thisLabelG.append('rect')
                        .attr('width', calculationData.PainScoreInfo.Width)
                        .attr('height', calculationData.PainScoreInfo.Height)
                        .attr('fill', `${thisLabel.BackColorValue ? thisLabel.BackColorValue : "transparent"}`)
                        // .attr('stroke', `${thisLabel.ShowBorder ? "black" : "none"}`)
                        .attr('stroke', "black")
                        .attr('x', 0)
                        .attr('y', calculationData.PainScoreInfo.Top);
                } else {
                    var rect = thisLabelG.append('rect')
                        .attr('width', width)
                        .attr('height', height)
                        .attr('fill', `${thisLabel.BackColorValue ? thisLabel.BackColorValue : "none"}`)
                        .attr('stroke', `${thisLabel.ShowBorder ? "black" : "none"}`)
                        // .attr('stroke', "black")
                        .attr('x', left)
                        .attr('y', top);
                }
                if (thisLabel.ImageDataBase64String && thisLabel.ImageDataBase64String.length > 0) {
                    thisLabelG.append('image')
                        .attr('dctype', 'dcLables')
                        .attr('x', left)
                        .attr('y', top)
                        .attr('width', width)
                        .attr('height', height)
                        .attr('href', `data:application/pdf;base64,${thisLabel.ImageDataBase64String}`);
                } else {
                    var textEle = thisLabelG.append('text')
                        .attr('style', `font-family:${thisLabel.TextFontName || '宋体'} ;font-size:${thisLabel.TextFontSize}pt;font-weight:${thisLabel.TextFontBold ? 900 : 0};text-decoration: ${thisLabel.TextFontUnderline ? 'underline' : 'none'};color:${thisLabel.TextColorValue};font-style:${thisLabel.TextFontItalic ? 'italic' : 'none'};`)
                        .attr('dctype', 'dcLables')
                        .attr('parameterName', `${thisLabel.ParameterName}`)
                        .attr('fill', `${thisLabel.TextColorValue ? thisLabel.TextColorValue : "black"}`)
                        .attr('x', left)
                        .attr('y', top + (height / 2))
                        .attr('xml:space', 'preserve')
                        .text(() => {
                            var returnText = thisLabel.Text;
                            if (thisLabel.ParameterName) {
                                //如果不存在则查找Parameters是否存在
                                for (var z = 0; z < parameters.length; z++) {
                                    if (parameters[z].Name == thisLabel.ParameterName) {
                                        returnText = parameters[z].Value;
                                        break;
                                    }
                                }
                            }
                            ////console.log(returnText)
                            return returnText;
                        });
                    var textWidth = textEle.node().getBBox().width;
                    var textHeight = textEle.node().getBBox().height;
                    var textReatWidth = rect.node().getBBox().width;
                    var textReatHeight = rect.node().getBBox().height;




                    //水平对齐方式Alignment  Near(默认) Center Far
                    if (thisLabel.Alignment == "Center") {
                        textEle.attr('x', (textReatWidth - textWidth) / 2 + left);
                    } else if (thisLabel.LineAlignment == "Far") {
                        textEle.attr('x', (textReatWidth - textWidth) + left);
                    }
                    //垂直对齐方式LineAlignment Near(默认) Center Far
                    if (thisLabel.LineAlignment == "Center") {
                        textEle.attr('y', top + (height / 2) + (textHeight / 1.5 / 2));
                    } else if (thisLabel.LineAlignment == "Far") {
                        textEle.attr('y', top + (height / 2) + textHeight / 1.5);
                    }

                    //判断MultiLine属性  多行文本
                    if (thisLabel.MultiLine && textWidth > textReatWidth) {
                        //计算单个文本宽度
                        var singleTextWidth = textWidth / thisLabel.Text.length;
                        var eachRowTextNumber = Math.floor(textReatWidth / singleTextWidth);

                        //判断一行能放几个字
                        var remainder = thisLabel.Text.length % eachRowTextNumber; //对字符串的长度取行的余数
                        // 如果设置的疼痛评分宽度小于实际宽度，并且可以展示开一个文字，则默认一行展示一个文本
                        if (thisLabel.Text == "疼痛强度" || thisLabel.Text == "疼痛评分") {
                            if (width < calculationData.PainScoreInfo.Width && width > eachRowTextNumber) {
                                eachRowTextNumber = 1;
                            }
                        }
                        var n = (thisLabel.Text.length - remainder) / eachRowTextNumber; //截完一共多少行（如果余数大于1，则共有n+1行
                        var thisStr = [];
                        textEle
                            .attr("x", left)
                            .text("");

                        for (var z = 0; z < n; z++) {
                            thisStr.push(thisLabel.Text.slice(z * eachRowTextNumber, (z + 1) * eachRowTextNumber));
                        }
                        if (remainder > 0) {
                            thisStr.push(thisLabel.Text.slice(n * eachRowTextNumber));
                        }
                        textEle.html(() => {
                            return thisStr.map((data, index) => {
                                return `<tspan dx="-${index == 0 ? 0 : Math.round(singleTextWidth * eachRowTextNumber)}" dy="${index == 0 ? 2 : textHeight + 2}">${data}</tspan>`;
                            }).join('');
                        });
                        //查看是否需要显示
                        //计算高度
                        var textReatY = rect.node().getBBox().y;
                        var allTspan = textEle.selectAll("tspan").nodes();
                        for (var z = 0; z < allTspan.length; z++) {
                            var thisTspanHeight = allTspan[z].getBBox().height;
                            var thisTspanY = allTspan[z].getBBox().y;
                            if (thisTspanY - textReatY + thisTspanHeight > textReatHeight) {
                                allTspan[z].remove();
                            }
                        }
                    }

                    //存在图例符号
                    if (thisLabel.SymbolVisible && thisLabel.SymbolStyle) {
                        var SymbolStyle = thisLabel.SymbolStyle || "circle";
                        //绘制的数据对象
                        var IconData = {
                            content: thisLabelG,
                            data: [j],
                            x: () => {
                                var textEleX = textEle.node().getBBox().x;
                                var textEleWidth = textEle.node().getBBox().width;
                                return textEleX + (textEleWidth / 2);
                            },
                            y: () => {
                                var textEleY = textEle.node().getBBox().y;
                                var textEleHeight = textEle.node().getBBox().height;
                                return textEleY + textEleHeight + (calculationData.LableHeight / 2);
                            },
                            fill: thisLabel.SymbolColorValue || "red",
                            stroke: thisLabel.SymbolColorValue || "red",
                            r: thisLabel.SymbolSize || 5,
                            title: '',
                        };
                        WriterControl_DrawFu.IconDrawObj()[SymbolStyle] && WriterControl_DrawFu.IconDrawObj()[SymbolStyle](IconData);
                    }
                }
            }
        }
    },

    //绘制小标题
    DrawHeadLable: function (svg, calculationData, defaultData, svgEle, i, parameters) {
        var thisBand = null; //存放当前页时间范围的变量
        //如果存在转院内容
        if (defaultData.HeaderLabelChangeDate && calculationData.AllDateData.length) {
            thisBand = calculationData.AllTimeData.slice(calculationData.Verticalallength * i, calculationData.Verticalallength * i + calculationData.Verticalallength + 1);
            thisBand = thisBand.sort((a, b) => {
                return a - b; //保证顺序
            });

            for (var j = 0; j < calculationData.AllHeaderLabels.length; j++) {
                var thisHeaderLables = calculationData.AllHeaderLabels[j];
                for (var q = 0; q < thisHeaderLables.length; q++) {
                    var headerLableItem = thisHeaderLables[q];//每一个小标题对象
                    var UID = thisHeaderLables[q].UID;
                    headerLableItem['ValueForPage'] = headerLableItem['ValueForPage'] ? headerLableItem['ValueForPage'] : {};
                    var HeaderLabelChangeObject = defaultData.HeaderLabelChangeDate[UID] || null;

                    if (HeaderLabelChangeObject) { //当前小标题存在修改内容

                        var ValueChangeData = HeaderLabelChangeObject.ValueChangeData || [];//修改内容数组
                        var ValueConnectorSymbol = HeaderLabelChangeObject.ValueConnectorSymbol || "→";//连接符号
                        // //给小标题对象增加每一页的value值,
                        headerLableItem['ValueForPage'][i] = (headerLableItem.Value || "");

                        for (var z = 0; z < ValueChangeData.length; z++) {
                            var changeTime = new Date(ValueChangeData[z].Time).getTime(); //修改时间
                            ValueChangeData[z].Time = changeTime;
                            if (changeTime >= thisBand[0] && changeTime <= thisBand[thisBand.length - 1]) {
                                //当前页存在转科的情况
                                if (headerLableItem['ValueForPage'][i] && headerLableItem['ValueForPage'][i].length) {
                                    headerLableItem['ValueForPage'][i] += `${ValueConnectorSymbol}${ValueChangeData[z].Value}`;

                                    //将后续的页中的标题都改为修改后的科室
                                    headerLableItem.Value = ValueChangeData[z].Value;

                                }
                            }
                        }
                    }
                }
            }
        }



        for (var j = 0; j < calculationData.AllHeaderLabels.length; j++) {
            var thisHeaderLables = calculationData.AllHeaderLabels[j];

            var dcHeadLineG = svg
                .append('g')
                .attr('dctype', "dcHeaderLabelsG")
                .attr(
                    'transform',
                    `translate(${calculationData.LeftMargin},${calculationData.TopMargin + calculationData.SpecifyTitleHeight + (j * calculationData.LableHeight)})`
                );
            if (!svgEle[i].dcHeadLineG) {
                svgEle[i].dcHeadLineG = [];
            }
            svgEle[i].dcHeadLineG.push(dcHeadLineG);
            if (thisHeaderLables && thisHeaderLables.length > 0) {
                //循环小标题
                for (var k = 0; k < thisHeaderLables.length; k++) {


                    //[DUWRITER5_0-3394] 20240829 lxy 修改小标题的UID，解决分组后定位不准确的问题
                    var thisHeaderLineG = dcHeadLineG.append('g')
                        .attr("uid", thisHeaderLables[k].UID);
                    //计算宽度
                    thisHeaderLineG.append('text')
                        .attr('dctype', 'dcHeadLabels')
                        .attr('style', 'font-family:' + (defaultData.FontName || '宋体') + ';font-size:' + defaultData.FontSize + 'pt;line-height: 1')
                        .attr('x', dcHeadLineG.node().getBBox().width)
                        .attr('y', calculationData.LableHeight / 1.5)
                        .html(() => {
                            var titleValue = thisHeaderLables[k].Value;
                            // 处理小标题的修改内容 //记录转院信息等
                            if (thisHeaderLables[k].ValueForPage && thisHeaderLables[k].ValueForPage[i]) {
                                titleValue = thisHeaderLables[k].ValueForPage[i];
                            } else if (thisHeaderLables[k].ParameterName) {
                                //如果不存在则查找Parameters是否存在
                                for (var z = 0; z < parameters.length; z++) {
                                    if (parameters[z].Name == thisHeaderLables[k].ParameterName) {
                                        titleValue = parameters[z].Value;
                                        break;
                                    }
                                }
                            }


                            return `${thisHeaderLables[k].Title}${thisHeaderLables[k].SeperatorChar ? thisHeaderLables[k].SeperatorChar : ':'
                                } ${titleValue}`;
                        });
                }

                //获取g的宽高
                var thisWidth = dcHeadLineG.node().getBBox().width;
                var x = parseFloat((calculationData.InnerWidth - thisWidth) / (thisHeaderLables.length - 1).toFixed(2));
                ////console.log(x);
                if (x < 0) {
                    //对当前数组进行重组
                    var allChild = dcHeadLineG.node().childNodes;
                    var allWidth = 0;
                    for (var l = 0; l < allChild.length; l++) {
                        allWidth += allChild[l].getBBox().width;
                        if (allWidth > calculationData.InnerWidth) {
                            //截断数组
                            var newArr = thisHeaderLables.slice(l);
                            calculationData.AllHeaderLabels[j] = calculationData.AllHeaderLabels[j].slice(0, l);
                            calculationData.AllHeaderLabels.splice(j + 1, 0, newArr);
                            j--;
                            dcHeadLineG.remove();
                            break;
                        }
                    }
                } else {
                    var allText = dcHeadLineG.selectAll("text").nodes();
                    dcHeadLineG.selectAll("text")
                        .attr("x", (d, i) => {
                            var newLeft = 0;
                            if (i != 0) {
                                var prevText = allText[i - 1].getBBox();
                                newLeft = prevText.x + prevText.width + x;
                            }
                            return newLeft;
                        });
                }
            }

        }

    },

    //绘制表格主体区域
    DrawTableContainer: function (svg, calculationData, defaultData, svgEle, i) {
        var dcTableContainerG = svg
            .append('g')
            .attr('dctype', "dcTableContainerG")
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin},${calculationData.TopMargin + calculationData.SpecifyTitleHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length)})`
            );
        svgEle[i].dcTableContainerG = dcTableContainerG;
        calculationData.TableContainerHeight = calculationData.InnerHeight - calculationData.SpecifyTitleHeight - (calculationData.LableHeight * calculationData.AllHeaderLabels.length) - calculationData.FooterDescriptionHeight - calculationData.LableHeight;
        dcTableContainerG.append('rect')
            .attr('x', 0)
            .attr('y', 0)
            .attr('width', calculationData.InnerWidth)
            .attr('height', calculationData.TableContainerHeight)
            .attr('stroke', "black")
            .attr('fill', 'none')
            .attr('style', 'stroke-width: 2');
    },

    //绘制页眉页脚
    DrawHeaderFooterLine: function (rootElement, svg, calculationData, defaultData, svgEle, i, type) {
        if (type == "HeaderLines") {
            var lineData = defaultData.HeaderLines;
            var headerFooterLinesSingleHeight = calculationData.HeaderLinesSingleHeight;
            var translateY = calculationData.TopMargin + calculationData.SpecifyTitleHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length);
            var linesChangeDate = (defaultData && defaultData.HeaderLinesChangeDate) ? defaultData.HeaderLinesChangeDate : {};
        } else {
            var lineData = defaultData.FooterLines;
            var headerFooterLinesSingleHeight = calculationData.FooterLinesSingleHeight;
            var translateY = calculationData.TopMargin + calculationData.InnerHeight - calculationData.LableHeight - calculationData.FooterDescriptionHeight - calculationData.FooterLinesTotalHeight;
            var linesChangeDate = (defaultData && defaultData.FooterLinesChangeDate) ? defaultData.FooterLinesChangeDate : {};
        }
        // try {
        //页眉页脚包裹元素
        var dcHeaderFooterLineG = svg
            .append('g')
            .attr('dctype', `dc${type}G`)
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin},${translateY})`
            );
        svgEle[i].dcHeaderFooterLineG = dcHeaderFooterLineG;

        //线宽
        var ThickLineWidth = defaultData.ThickLineWidth;
        ThickLineWidth = ThickLineWidth ? ThickLineWidth : 1;
        ThickLineWidth = parseFloat((ThickLineWidth / 300 * 96.00001209449).toFixed(2));
        ThickLineWidth = ThickLineWidth >= 1 ? ThickLineWidth : 1;
        //[DUWRITER5_0-4015] 20241219 lxy 适配页眉页脚扩展分割线属性：ExtendGridLineType
        if (headerFooterLinesSingleHeight && headerFooterLinesSingleHeight.length > 0) {
            for (var h = 0; h < headerFooterLinesSingleHeight.length; h++) {
                //当前行的分割线样式
                var ExtendGridLineType = (lineData[h] && lineData[h].ExtendGridLineType && lineData[h].ExtendGridLineType) || 'Below';
                ExtendGridLineType = ExtendGridLineType.trim();


                if (headerFooterLinesSingleHeight[h] > 0) {
                    var dcHeaderFooterLineGHeight = dcHeaderFooterLineG.node().getBBox().height;//获取当前元素高度
                    //计算下标h前面数值的总和
                    dcHeaderFooterLineG.append('g')
                        .attr("dctype", `dc${type}VerticalG_${h}`)
                        .selectAll('line')
                        .data([...new Array(calculationData.NumOfDaysInOnePage).keys()])
                        .join('line')
                        .attr('x1', (index) => calculationData.FirstTextWidth + (index * calculationData.HeaderAndFooterStepWidth))
                        .attr('y1', dcHeaderFooterLineGHeight)
                        .attr('y2', dcHeaderFooterLineGHeight + headerFooterLinesSingleHeight[h])
                        .attr('x2', (index) => calculationData.FirstTextWidth + (index * calculationData.HeaderAndFooterStepWidth))
                        .attr('stroke', (index) => {
                            //判断是否使用大垂直网格线颜色
                            if (ExtendGridLineType === 'Above') {
                                var BigVerticalGridLineColorValue = (defaultData && defaultData.BigVerticalGridLineColorValue) || 'black';
                                return index === 0 ? "black" : BigVerticalGridLineColorValue;//第一条线使用黑色
                            }
                            return "black";
                        })
                        .attr('stroke-width', (index) => {
                            //判断是否使用大垂直网格线宽度
                            if (ExtendGridLineType === 'Above' && index !== 0) {
                                var gridLineWidth = defaultData.BigVerticalGridLineWidth;
                                gridLineWidth = gridLineWidth ? gridLineWidth : 2;
                                gridLineWidth = parseFloat((gridLineWidth / 300 * 96.00001209449).toFixed(2));
                                gridLineWidth = gridLineWidth >= 2 ? gridLineWidth : 1;
                                return gridLineWidth;
                            } else if (ExtendGridLineType == "None" && index !== 0) {
                                //当数据类型为时间时，使用小网格线
                                if (lineData[h] && lineData[h].ValueType == "HourTick") {
                                    return 1;
                                }
                                return 0;
                            }
                            return ThickLineWidth;
                        });
                }
            }
        }

        //算出有多少个页眉
        var data = new Array(headerFooterLinesSingleHeight.length + 1);
        //绘制横线位置横线
        dcHeaderFooterLineG.append("g")
            .attr("dctype", `dc${type}HorizontalG`)
            .selectAll('line')
            .data(data)
            .join('line')
            .attr('x1', 0)
            .attr('y1', (d, j) => {
                if (j == 0) {
                    return 0;
                } else {
                    return headerFooterLinesSingleHeight.slice(0, j).reduce((prev, next) => {
                        return prev + next;
                    });
                }
            })
            .attr('y2', (d, j) => {
                if (j == 0) {
                    return 0;
                } else {
                    return headerFooterLinesSingleHeight.slice(0, j).reduce((prev, next) => {
                        return prev + next;
                    });
                }
            })
            .attr('x2', calculationData.InnerWidth)
            .attr('fill', 'none')
            .attr('stroke', "black")
            .attr('stroke-width', ThickLineWidth)
            .attr('stroke-linejoin', "round")
            .attr('stroke-linecap', "round");

        //当前页面的时间范围
        var thisBand = null; //存放当前页时间范围的变量
        thisBand = calculationData.AllTimeData.slice(calculationData.Verticalallength * i, calculationData.Verticalallength * i + calculationData.Verticalallength + 1);
        thisBand = thisBand.sort((a, b) => {
            return a - b; //保证顺序
        });

        //绘制数据
        var breakFooter = false;//记录循环是否被终止
        for (var index = 0; index < lineData.length; index++) {
            var d = lineData[index];
            //如果存在页眉页脚需要换行，且还没有设置过指定高度，则终止循环
            if (breakFooter) {
                break;
            }
            var uid = `dc${type}_` + index;
            d.UID = uid;
            ////console.log(d)
            var thisHeaderFooter = dcHeaderFooterLineG.append('g')
                .attr('uid', uid);
            //在此处对标题进行判断
            var oldTitle = d.Title;
            if (d.PageTitleTexts && typeof d.PageTitleTexts !== 'string') {
                d.PageTitleTexts = String(d.pageTitleTexts);
            }
            var allPageTitle = d.PageTitleTexts;
            allPageTitle = allPageTitle ? allPageTitle : "";
            allPageTitle = allPageTitle.split(",");
            if (allPageTitle[i]) {
                d.Title = allPageTitle[i];
            }

            //[DUWRITER5_0-4117]20250114 lxy 每页可以自定义标题功能
            var dName = d.Name || d.UID || '';//支持name或者是uid
            if (dName !== '') {
                var linesChangeObject = (linesChangeDate && linesChangeDate[dName]) || null;
                if (linesChangeObject) {
                    var ValueChangeData = linesChangeObject.ValueChangeData || [];//修改内容数组
                    for (var z = 0; z < ValueChangeData.length; z++) {
                        var changeTime = new Date(ValueChangeData[z].Time).getTime(); //修改时间
                        ValueChangeData[z].Time = changeTime;
                        //如果修改时间大于thisBand[0],小于thisBand[thisBand.length-1],则进行修改。剩余的页数中使用最后一次修改的内容
                        if (changeTime >= thisBand[0] && changeTime <= thisBand[thisBand.length - 1]) {
                            oldTitle = d.Title = ValueChangeData[z].Value;
                        }

                    }
                }
            }


            if (d.ValueType == "SerialDate" || d.ValueType == "GlobalDayIndex" || d.ValueType == "NewSerialDate") {
                thisHeaderFooter.append("g")
                    .attr("dctype", uid + '_FirstText')
                    .selectAll('text')
                    .data([0])
                    .join('text')
                    .attr('style', () => {
                        var style = `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                        if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "near") {
                            return style + "text-anchor:start;";
                        } else if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "far") {
                            return style + "text-anchor:end;";
                        } else {
                            return style + "text-anchor:middle;";
                        }
                    })
                    .attr('xml:space', 'preserve')
                    .attr('fill', d.TitleColorValue ? d.TitleColorValue : "black")
                    .text(d.Title)
                    .attr("x", () => {
                        var style = `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                        if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "near") {
                            return 0;
                        } else if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "far") {
                            return calculationData.FirstTextWidth;
                        } else {
                            return calculationData.FirstTextWidth / 2;
                        }
                    })
                    .attr("y", () => {
                        //[DUWRITER5_0-3727] 20241016 lxy 解决修改指定高度后，内容错位的问题
                        var thisy = 0;
                        headerFooterLinesSingleHeight.map((item, itemIndex) => {
                            if (itemIndex < index) {
                                thisy += item;
                            }
                        });
                        thisy = thisy + (calculationData.LableHeight / 2 / 1.5) + (headerFooterLinesSingleHeight[index] / 2);
                        return thisy;
                    });
                //此处为日期和住院日期的写法
                thisHeaderFooter.append("g")
                    .attr("dctype", uid + '_Text')
                    .selectAll('text')
                    .data([...new Array(calculationData.NumOfDaysInOnePage).keys()])
                    .join('text')
                    .attr('style', `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};text-anchor:middle;`)
                    .attr('xml:space', 'preserve')
                    .attr('fill', d.TextColorValue ? d.TextColorValue : "black")
                    .text((j) => {
                        j = j + 1;
                        if (d.ValueType == "SerialDate" || d.ValueType == "NewSerialDate") {
                            if (i == 0 && j == 1) {
                                return calculationData.AllDateData[i * calculationData.NumOfDaysInOnePage + j - 1];
                            }
                            return calculationData.AllDateData[i * calculationData.NumOfDaysInOnePage + j - 1];
                        } else if (d.ValueType == "GlobalDayIndex") {
                            if (i * calculationData.NumOfDaysInOnePage + j - 1 < calculationData.AllDateData.length) {
                                if (calculationData.GetViewMode && calculationData.GetViewMode.toLowerCase().trim() === "singlepage") {
                                    //单页模式下使用页码计算当前的住院天数
                                    return calculationData.NumOfDaysInOnePage * ((calculationData.HasPageIndex || 0) - 1) + j;
                                } else {
                                    //普通模式下使用日期计算当前的住院天数
                                    return i * calculationData.NumOfDaysInOnePage + j;
                                }
                            }
                        }
                    })
                    .attr('x', (j) => {
                        return calculationData.FirstTextWidth + ((j + 1) * calculationData.HeaderAndFooterStepWidth) - calculationData.HeaderAndFooterStepWidth / 2;
                    })
                    .attr('y', () => {
                        //[DUWRITER5_0-3727] 20241016 lxy 解决修改指定高度后，内容错位的问题
                        var thisy = 0;
                        headerFooterLinesSingleHeight.map((item, itemIndex) => {
                            if (itemIndex < index) {
                                thisy += item;
                            }
                        });
                        thisy = thisy + (calculationData.LableHeight / 2 / 1.5) + (headerFooterLinesSingleHeight[index] / 2);
                        return thisy;
                    });
            } else if (d.ValueType == "HourTick") {
                //此处为时刻的写法
                thisHeaderFooter.append('g')
                    .attr('dctype', uid + "_FirstText")
                    .selectAll('text')
                    .data([0])
                    .join('text')
                    .attr('style', () => {
                        var style = `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                        if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "near") {
                            return style + "text-anchor:start;";
                        } else if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "far") {
                            return style + "text-anchor:end;";
                        } else {
                            return style + "text-anchor:middle;";
                        }
                    })
                    .attr('xml:space', 'preserve')
                    .attr('fill', d.TitleColorValue ? d.TitleColorValue : "black")
                    .text(d.Title)
                    .attr('x', () => {
                        var style = `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                        if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "near") {
                            return 0;
                        } else if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "far") {
                            return calculationData.FirstTextWidth;
                        } else {
                            return calculationData.FirstTextWidth / 2;
                        }
                    })
                    .attr('y', () => {
                        //[DUWRITER5_0-3727] 20241016 lxy 解决修改指定高度后，内容错位的问题
                        var thisy = 0;
                        headerFooterLinesSingleHeight.map((item, itemIndex) => {
                            if (itemIndex < index) {
                                thisy += item;
                            }
                        });
                        thisy = thisy + (calculationData.LableHeight / 2 / 1.5) + (headerFooterLinesSingleHeight[index] / 2);
                        return thisy;
                    });
                //拆分单个数据组成数组引用
                const data = new Array(calculationData.TickTextsData.length * calculationData.NumOfDaysInOnePage)
                    .fill('')
                    .map((d, i) => calculationData.TickTextsData[i % calculationData.TickTextsData.length]);
                // 线条
                thisHeaderFooter.append('g')
                    .attr('dctype', uid + '_Pos')
                    .selectAll('line')
                    .data(data)
                    .join('line')
                    .attr('x1', (d, j) => {
                        return calculationData.FirstTextWidth + calculationData.TickTextStepWidth * j;
                    })
                    .attr('y1', () => {
                        //[DUWRITER5_0-3727] 20241016 lxy 解决修改指定高度后，内容错位的问题
                        var thisy = 0;
                        headerFooterLinesSingleHeight.map((item, itemIndex) => {
                            if (itemIndex <= index) {
                                thisy += item;
                            }
                        });
                        thisy = thisy - headerFooterLinesSingleHeight[index];
                        return thisy;
                    })
                    .attr('x2', (d, j) => {
                        return calculationData.FirstTextWidth + calculationData.TickTextStepWidth * j;
                    })
                    .attr('y2', () => {
                        //[DUWRITER5_0-3727] 20241016 lxy 解决修改指定高度后，内容错位的问题
                        var thisy = 0;
                        headerFooterLinesSingleHeight.map((item, itemIndex) => {
                            if (itemIndex <= index) {
                                thisy += item;
                            }
                        });
                        return thisy;
                    })
                    .attr('fill', 'none')
                    .attr('class', 'dataLine')
                    .attr('stroke', "black")
                    .attr('stroke-width', (d, i) => {
                        return i % 6 ? 1 : 0;
                    })
                    .attr('stroke-linejoin', "round")
                    .attr('stroke-linecap', "round");
                thisHeaderFooter.append('g')
                    .attr('dctype', uid + 'Text')
                    .selectAll('text')
                    .data(data)
                    .join('text')
                    .attr('style', `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}px;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};text-anchor:middle;`)
                    .attr('xml:space', 'preserve')
                    .attr('fill', (a, j) => {
                        if (d.TextColorValue && d.TextColorValue !== '' && d.TextColorValue !== "#000000") {
                            return d.TextColorValue;
                        }
                        var color = calculationData.TickColorData[j % 6];
                        if (color) {
                            return color;
                        }
                        return "black";
                    })
                    .text((d, j) => {
                        return d;
                    })
                    .attr('x', (d, j) => {
                        return calculationData.FirstTextWidth + calculationData.TickTextStepWidth * (j + 1) - calculationData.TickTextStepWidth / 2;
                    })
                    .attr('y', () => {
                        //[DUWRITER5_0-3727] 20241016 lxy 解决修改指定高度后，内容错位的问题
                        var thisy = 0;
                        headerFooterLinesSingleHeight.map((item, itemIndex) => {
                            if (itemIndex < index) {
                                thisy += item;
                            }
                        });
                        thisy = thisy + (headerFooterLinesSingleHeight[index] / 2) + (calculationData.LableHeight / 2 / 1.5);
                        return thisy;
                    });
            } else if (d.ValueType == "TickText" || d.ValueType == "Data" || d.ValueType == "Text") {
                if (index == 0) {
                    var thisy = 0;
                } else {
                    var thisy = headerFooterLinesSingleHeight.slice(0, index).reduce((prev, next) => {
                        return prev + next;
                    });
                }
                //判断是否存在
                // //console.log(d.ValueType);
                thisHeaderFooter.append("g")
                    .attr("dctype", uid + '_FirstText')
                    .selectAll('text')
                    .data([0])
                    .join('text')
                    .attr('style', () => {
                        var style = `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                        if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "near") {
                            return style + "text-anchor:start;";
                        } else if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "far") {
                            return style + "text-anchor:end;";
                        } else {
                            return style + "text-anchor:middle;";
                        }
                    })
                    .attr('xml:space', 'preserve')
                    .attr('fill', d.TitleColorValue ? d.TitleColorValue : "black")
                    .text(d.Title)
                    .attr('x', () => {
                        var style = `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                        if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "near") {
                            return 0;
                        } else if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "far") {
                            return calculationData.FirstTextWidth;
                        } else {
                            return calculationData.FirstTextWidth / 2;
                        }
                    })
                    .attr('y', (thisy + (calculationData.LableHeight / 2 / 1.5) + (headerFooterLinesSingleHeight[index] / 2)));
                var textList = [];
                if (d.LoopTextList && d.LoopTextList.length > 0) {
                    //拆分
                    textList = d.LoopTextList.split(",");
                }
                var tickStep = parseFloat(d.TickStep);
                if (isNaN(tickStep) || tickStep <= 0 || tickStep > 6 || d.LayoutType == "Free") {
                    tickStep = 6;
                }
                const data = new Array(6 / tickStep * calculationData.NumOfDaysInOnePage)
                    .fill('')
                    .map((d, i) => textList[i % textList.length]);
                var thisStep = calculationData.HeaderAndFooterStepWidth / (6 / tickStep);

                if (d.TickLineVisible === true) {
                    // 垂直线条
                    thisHeaderFooter.append('g')
                        .attr('dctype', uid + '_Pos')
                        .selectAll('line')
                        .data(data)
                        .join('line')
                        .attr('x1', (d, j) => {
                            return calculationData.FirstTextWidth + thisStep * j;
                        })
                        .attr('y1', thisy)
                        .attr('x2', (d, j) => {
                            return calculationData.FirstTextWidth + thisStep * j;
                        })
                        .attr('y2', thisy + headerFooterLinesSingleHeight[index])
                        .attr('fill', 'none')
                        .attr('class', 'dataLine')
                        .attr('stroke', 'black')
                        .attr('stroke-width', (d, j) => {
                            if ((j * tickStep) % 6 == 0 && j !== 0) {
                                return 0;
                            } else {
                                return 1;
                            }
                        })
                        .attr('stroke-linejoin', "round")
                        .attr('stroke-linecap', "round");
                }

                //绘制文本
                if (textList.length > 0) {
                    thisHeaderFooter.append('g')
                        .attr('dctype', uid + '_Text')
                        .selectAll('text')
                        .data(data)
                        .join('text')
                        .attr('style', `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};text-anchor:middle;`)
                        .attr('xml:space', 'preserve')
                        .attr('fill', d.TextColorValue ? d.TextColorValue : "black")
                        .text((d, j) => {
                            return d;
                        })
                        .attr('x', (d, j) => {
                            return calculationData.FirstTextWidth + thisStep * (j + 1) - thisStep / 2;
                        })
                        .attr('y', (thisy + (calculationData.LableHeight / 2 / 1.5) + (headerFooterLinesSingleHeight[index] / 2)));
                } else {
                    //判断是否value存在相同值
                    var hasValue = calculationData.HeaderFooterValueArr[d.Name];
                    ////console.log(hasValue);
                    //绘制矩形
                    if (hasValue && hasValue[i]) {
                        var dcHeadLineTextG = thisHeaderFooter.append('g')
                            .attr('dctype', uid + '_Text')
                            .attr(
                                'transform',
                                `translate(0,${thisy})`
                            );
                        var upAndDown = false;
                        // //console.log(hasValue);

                        for (var k in hasValue[i]) {
                            //如果存在页眉页脚需要换行，且还没有设置过指定高度，则终止循环
                            if (breakFooter) {
                                // //console.log("终止了倒数第二层循环------------");
                                break;
                            }
                            var thisRectValue = hasValue[i][k];
                            //计算直线位置
                            if (thisRectValue.text && thisRectValue.text.length > 0) {
                                var stepWidth = thisRectValue.basisWidth / thisRectValue.text.length;
                                stepWidth = Math.round(stepWidth);
                                if (thisRectValue.endIndex) {
                                    stepWidth = thisRectValue.endIndex - thisRectValue.startIndex;
                                }
                                for (var j = 0; j < thisRectValue.text.length; j++) {
                                    if (!thisRectValue.text[j] || thisRectValue.text[j].length == 0) {
                                        continue;
                                    }
                                    var dcHeadLineTextG_rect = null;
                                    if (d.TickLineVisible === true) {
                                        if (d.LayoutType == "Cascade") { //Cascade || AutoCascade  水平分割线
                                            dcHeadLineTextG_rect = dcHeadLineTextG.append('rect')
                                                .attr('width', thisRectValue.basisWidth)
                                                .attr('height', headerFooterLinesSingleHeight[index] / thisRectValue.text.length)
                                                .attr('fill', thisRectValue.data[j].ColorValue ? thisRectValue.data[j].ColorValue : 'transparent')
                                                .attr('stroke', "transparent")
                                                .attr('x', calculationData.FirstTextWidth + thisRectValue.startIndex)
                                                .attr('y', () => headerFooterLinesSingleHeight[index] / thisRectValue.text.length * j);
                                        } else {
                                            dcHeadLineTextG_rect = dcHeadLineTextG.append('rect')
                                                .attr('width', stepWidth)
                                                .attr('height', headerFooterLinesSingleHeight[index])
                                                .attr('fill', thisRectValue.data[j].ColorValue ? thisRectValue.data[j].ColorValue : 'transparent')
                                                .attr('stroke', "transparent")
                                                .attr('x', j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex)
                                                .attr('y', 0);
                                        }

                                    }

                                    var result = thisRectValue.text[j] == null || thisRectValue.text[j] == "" ? "" : thisRectValue.text[j];
                                    var thisTextEle = dcHeadLineTextG.append('text')
                                        .attr('style', `font-size:${thisRectValue.data[j].TextFontSize ? thisRectValue.data[j].TextFontSize : d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${thisRectValue.data[j].TextFontName ? thisRectValue.data[j].TextFontName : d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')}`)
                                        .attr('xml:space', 'preserve')
                                        .attr('fill', () => {
                                            var textColor = "black";
                                            textColor = d.TextColorValue ? d.TextColorValue : textColor;
                                            textColor = thisRectValue.data[j].TextColorValue ? thisRectValue.data[j].TextColorValue : textColor;
                                            return textColor;
                                        })
                                        .attr('x', () => {
                                            if (d.LayoutType == "Cascade") {
                                                return calculationData.FirstTextWidth + thisRectValue.startIndex;
                                            } else {
                                                return j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex;
                                            }
                                        })
                                        .attr('y', () => {
                                            if (d.LayoutType == "Cascade") {
                                                return (calculationData.LableHeight / 1.5 / 4 * 3) + (headerFooterLinesSingleHeight[index] / thisRectValue.text.length * j);
                                            } else {
                                                //判断是否存在UpAndDown;
                                                if (thisRectValue.data[j].UpAndDown == "Up") {
                                                    return calculationData.LableHeight / 1.5;
                                                } else if (thisRectValue.data[j].UpAndDown == "Down") {
                                                    return headerFooterLinesSingleHeight[index] - 2;
                                                } else {
                                                    if (d.UpAndDownTextType != "None") {
                                                        upAndDown = !upAndDown;
                                                        if (upAndDown) {
                                                            //为true Up
                                                            return calculationData.LableHeight / 1.5 / 4 * 3 + 2;
                                                        } else {
                                                            //为false Down
                                                            return headerFooterLinesSingleHeight[index] - 2;
                                                        }
                                                    } else {
                                                        return (calculationData.LableHeight / 1.5 / 4 * 3 / 2) + (headerFooterLinesSingleHeight[index] / 2);
                                                    }
                                                }
                                            }

                                        })
                                        .text(() => {
                                            // if (thisRectValue.data[j].CharLantern) {
                                            //     return thisRectValue.data[j].CharLantern;
                                            // }
                                            if (result.indexOf("R") >= 0) {
                                                var reg = new RegExp("R", "g");
                                                result = result.replace(reg, "®");
                                            }

                                            return result.trim();
                                        })
                                        .attr("title", () => {
                                            var thisTitle = thisRectValue.title[j] + ' ' + thisRectValue.text[j];
                                            thisTitle = thisRectValue.data[j].Title ? thisRectValue.data[j].Title : thisTitle;
                                            return thisTitle;
                                        });

                                    if (thisRectValue.text[j] == null || thisRectValue.text[j] == "") {
                                        continue;
                                    }
                                    //判断溢出文本是自动换行
                                    var FooterLinesCalculateExceedHeight = rootElement.getAttribute("FooterLinesCalculateExceedHeight") || null;
                                    //判断文本
                                    var textWidth = thisTextEle.node().getBBox().width;
                                    if (textWidth > stepWidth) {
                                        var stepTextWidth = textWidth / thisRectValue.text[j].length;
                                        var eachRowTextNumber = Math.floor(stepWidth / stepTextWidth);
                                        eachRowTextNumber = eachRowTextNumber == 0 ? 1 : eachRowTextNumber;//一行展示几个文字
                                        //单行字体高度
                                        var textHeight = thisTextEle.node().getBBox().height;
                                        //当文本宽度大于步长宽度时，是否需要换行展示，并重新计算本行的高度
                                        if (type == "FooterLines" && (FooterLinesCalculateExceedHeight === true || FooterLinesCalculateExceedHeight === "true")) {
                                            //需要的行数
                                            var needLine = Math.ceil(thisRectValue.text[j].length / eachRowTextNumber);
                                            // 单位转换(用于设置指定高度)
                                            var alltextLineHeight = Math.ceil((((needLine * textHeight) + 2) * 300 / 96) / 1.3);

                                            //当需要换行展示，且没有设置需要的指定高度时，则需要重新计算行高后svg再渲染一次
                                            if (!d.SpecifyHeight || d.SpecifyHeight < alltextLineHeight) {
                                                //保留一次设计器状态
                                                var rootType = rootElement.getAttribute("dctype");
                                                var isDesignerMode = rootType == "DCTemperatureDesignControlForWASM";
                                                //修改FooterHeader行高度
                                                rootElement.DesignerMode(true);
                                                rootElement.SetSpecialItemsProperties(d.UID, {
                                                    SpecifyHeight: alltextLineHeight,//设置指定高度
                                                });
                                                rootElement.DesignerMode(isDesignerMode);//修改指定高度后，恢复设计器状态
                                                breakFooter = true;//用于记录是否需要重新渲染svg
                                                // //console.log('终止最内部了循环=');
                                                break;

                                            } else {
                                                //title
                                                var thisTitle = thisRectValue.title[j] + ' ' + thisRectValue.text[j];
                                                thisTitle = thisRectValue.data[j].Title ? thisRectValue.data[j].Title : thisTitle;
                                                //外层rect元素
                                                var dcHeadLineTextG_rectNode = dcHeadLineTextG_rect.node().getBBox();
                                                //换行子元素的x位置
                                                var needX = dcHeadLineTextG_rectNode.x;
                                                //开启了自动换行显示，且设置过了指定高度，则直接渲染换行子元素
                                                thisTextEle.html(() => {
                                                    // (thisRectValue.basisWidth / 2) + calculationData.FirstTextWidth + thisRectValue.startIndex//居中时会用到的位置
                                                    var thisTextEleHtml = '';
                                                    for (var i = 0; i < needLine; i++) {
                                                        //每一行文本
                                                        var thisLineText = thisRectValue.text[j].substring(i * eachRowTextNumber, (i + 1) * eachRowTextNumber);
                                                        thisTextEleHtml += `<tspan x="${needX}" y="${(i + 1) * textHeight}" title="${thisTitle} ">${thisLineText}</tspan>`;
                                                    }
                                                    return thisTextEleHtml;
                                                });

                                                //当开启了自动撑高高度并换行展示时，将强制设置文本的对齐方式为左对齐
                                                thisRectValue.data[j].TextAlign = "near";
                                            }
                                        } else {
                                            //没有设置换行展示时，则直接截取展示（默认展示1行）
                                            thisTextEle.text(thisRectValue.text[j].substring(0, eachRowTextNumber));
                                        }
                                    }

                                    var style = `font-family:${thisRectValue.data[j].TextFontName ? thisRectValue.data[j].TextFontName : d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                                    if (typeof thisRectValue.data[j].TextAlign == "string") {
                                        if (d.LayoutType == "Cascade") {
                                            thisTextEle
                                                .attr('x', (thisRectValue.basisWidth / 2) + calculationData.FirstTextWidth + thisRectValue.startIndex);
                                            style += "text-anchor:middle;";
                                        } else {
                                            var thisTextAlign = thisRectValue.data[j].TextAlign.toLowerCase().trim();
                                            if (thisTextAlign == "near") {
                                                //判断颜色,位置
                                                thisTextEle
                                                    .attr('x', (j * stepWidth) + calculationData.FirstTextWidth + thisRectValue.startIndex);
                                                style += "text-anchor:start;";
                                            } else if (thisTextAlign == 'far') {
                                                //判断颜色,位置
                                                thisTextEle
                                                    .attr('x', (j * stepWidth) + (stepWidth) + calculationData.FirstTextWidth + thisRectValue.startIndex);
                                                style += "text-anchor:end;";
                                            } else {
                                                //判断颜色,位置
                                                thisTextEle
                                                    .attr('x', (j * stepWidth) + (stepWidth / 2) + calculationData.FirstTextWidth + thisRectValue.startIndex);
                                                style += "text-anchor:middle;";
                                            }
                                        }
                                    }
                                    thisTextEle.attr('style', () => {

                                        if (result.indexOf("®") >= 0) {
                                            style += `font-size:18px;`;
                                        } else {
                                            style += `font-size:${d.TextFontSize || defaultData.FontSize || thisRectValue.data[j].TextFontSize}pt`;
                                        }
                                        return style;
                                    });

                                    //适配LayoutType的值
                                    if (d.LayoutType == "Slant") {
                                        thisTextEle
                                            .attr('x', (j * stepWidth) + calculationData.FirstTextWidth + thisRectValue.startIndex)
                                            .attr('y', calculationData.LableHeight)
                                            .attr('style', `font-family:${(defaultData.FontName || '宋体')};font-size:${defaultData.FontSize}px;`);
                                        //计算0 点
                                        var baseIndex = j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex;
                                        //绘制三角形遮挡
                                        dcHeadLineTextG.append('polygon')
                                            .attr('fill', '#fff')
                                            .attr('stroke', "black")
                                            .attr('points', `${baseIndex},${0} ${baseIndex + stepWidth},${0} ${baseIndex + stepWidth},${headerFooterLinesSingleHeight[index]}`);
                                    } else if (d.LayoutType == "Slant2") {
                                        thisTextEle
                                            .attr('x', (j * stepWidth) + calculationData.FirstTextWidth + thisRectValue.startIndex)
                                            .attr('y', calculationData.LableHeight / 2)
                                            .attr('style', `font-family:${(defaultData.FontName || '宋体')};font-size:${defaultData.FontSize}px;`);
                                        //计算0 点
                                        var baseIndex = j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex;
                                        //绘制三角形遮挡
                                        dcHeadLineTextG.append('polygon')
                                            .attr('fill', '#fff')
                                            .attr('stroke', "black")
                                            .attr('points', `${baseIndex + stepWidth},${0} ${baseIndex + stepWidth},${headerFooterLinesSingleHeight[index]} ${baseIndex},${headerFooterLinesSingleHeight[index]}`);
                                    } else if (d.LayoutType == "Slant3") {
                                        //新的文本
                                        var newTextArr = thisRectValue.text[j].split('/');
                                        thisTextEle
                                            .text(newTextArr[0])
                                            .attr('x', (j * stepWidth) + calculationData.FirstTextWidth + thisRectValue.startIndex)
                                            .attr('y', calculationData.LableHeight / 2)
                                            .attr('style', `font-family:${(defaultData.FontName || '宋体')};font-size:${defaultData.FontSize}px;`);

                                        //计算0 点
                                        var baseIndex = j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex;
                                        //绘制三角形遮挡
                                        dcHeadLineTextG.append('polygon')
                                            .attr('fill', '#fff')
                                            .attr('stroke', "black")
                                            .attr('points', `${baseIndex + stepWidth},${0} ${baseIndex + stepWidth},${headerFooterLinesSingleHeight[index]} ${baseIndex},${headerFooterLinesSingleHeight[index]}`);
                                        dcHeadLineTextG.append('text')
                                            .attr('style', `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}px;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};text-anchor:end;`)
                                            .attr('xml:space', 'preserve')
                                            .attr('fill', d.TextColorValue ? d.TextColorValue : "black")
                                            .attr('x', j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex + thisRectValue.basisWidth)
                                            .attr('y', calculationData.LableHeight)
                                            .text(newTextArr[1] ? newTextArr[1] : " ")
                                            .attr("title", () => {
                                                var thisTitle = thisRectValue.title[j] + ' ' + thisRectValue.text[j];
                                                thisTitle = thisRectValue.data[j].Title ? thisRectValue.data[j].Title : thisTitle;
                                                return thisTitle;
                                            });
                                    } else if (d.LayoutType == "Fraction") { //Fraction
                                        //新的文本
                                        var newTextArr = thisRectValue.text[j].split('/');
                                        thisTextEle
                                            .text(newTextArr[0])
                                            .attr('y', calculationData.LableHeight / 2)
                                            .attr('x', j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex + thisRectValue.basisWidth / 2)
                                            .attr('style', `font-family:${(defaultData.FontName || '宋体')};font-size:${defaultData.FontSize}px;text-anchor:middle;`);

                                        //计算0 点
                                        var baseIndex = j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex;
                                        //绘制分割线
                                        dcHeadLineTextG.append("line")
                                            .attr('y1', headerFooterLinesSingleHeight[index] / 2)
                                            .attr('x1', baseIndex)
                                            .attr('y2', headerFooterLinesSingleHeight[index] / 2)
                                            .attr('x2', baseIndex + + thisRectValue.basisWidth)
                                            .attr('stroke', d.TextColorValue ? d.TextColorValue : "black");
                                        dcHeadLineTextG.append('text')
                                            .attr('style', `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}px;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};text-anchor:middle;`)
                                            .attr('xml:space', 'preserve')
                                            .attr('fill', d.TextColorValue ? d.TextColorValue : "black")
                                            .attr('x', j * stepWidth + calculationData.FirstTextWidth + thisRectValue.startIndex + thisRectValue.basisWidth / 2)
                                            .attr('y', calculationData.LableHeight)
                                            .text(newTextArr[1] ? newTextArr[1] : " ")
                                            .attr("title", () => {
                                                var thisTitle = thisRectValue.title[j] + ' ' + thisRectValue.text[j];
                                                thisTitle = thisRectValue.data[j].Title ? thisRectValue.data[j].Title : thisTitle;
                                                return thisTitle;
                                            });
                                    }

                                }
                            }
                        }

                    }
                }

            } else if (d.ValueType == "DayIndex") {
                //此处为DayIndex的写法
                thisHeaderFooter.append('g')
                    .attr('dctype', uid + "_FirstText")
                    .selectAll('text')
                    .data([0])
                    .join('text')
                    .attr('fill', () => {
                        return d.TitleColorValue ? d.TitleColorValue : "black";
                    })
                    .attr('style', () => {
                        var style = `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                        if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "near") {
                            return style + "text-anchor:start;";
                        } else if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "far") {
                            return style + "text-anchor:end;";
                        } else {
                            return style + "text-anchor:middle;";
                        }
                    })
                    .attr('xml:space', 'preserve')
                    .text(d.Title)
                    .attr('x', () => {
                        var style = `font-size:${d.TextFontSize ? d.TextFontSize : defaultData.FontSize}pt;font-family:${d.TextFontName ? d.TextFontName : (defaultData.FontName || '宋体')};`;
                        if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "near") {
                            return 0;
                        } else if (typeof d.TitleAlign == "string" && d.TitleAlign.toLowerCase().trim() == "far") {
                            return calculationData.FirstTextWidth;
                        } else {
                            return calculationData.FirstTextWidth / 2;
                        }
                    })
                    .attr('y', () => {
                        //[DUWRITER5_0-3727] 20241016 lxy 解决修改指定高度后，内容错位的问题
                        var thisy = 0;
                        headerFooterLinesSingleHeight.map((item, itemIndex) => {
                            if (itemIndex < index) {
                                thisy += item;
                            }
                        });
                        thisy = thisy + (calculationData.LableHeight / 2 / 1.5) + (headerFooterLinesSingleHeight[index] / 2);
                        return thisy;
                    });
                //判断是否存在时间
                if (d.StartDateKeyword) {
                    var thisBand = calculationData.AllTimeData.slice(calculationData.Verticalallength * i, calculationData.Verticalallength * i + calculationData.Verticalallength);
                    if (i == calculationData.TotalPageNumber - 1) {
                        thisBand.pop();
                    }
                    //保证顺序
                    thisBand = thisBand.sort((a, b) => {
                        return a - b;
                    });
                    thisBand = thisBand.filter((data, index) => index % 6 == 0);
                    var haskeyWordTime = calculationData.StartDateKeyword[d.StartDateKeyword];
                    //排序去重
                    if (haskeyWordTime) {
                        haskeyWordTime = haskeyWordTime.sort((a, b) => a - b).filter((item, index, arr) => {
                            return arr.indexOf(item) === index;
                        });
                    }
                    var hasEndWordTime = calculationData.EndDateKeyword[d.EndDateKeyword];
                    if (haskeyWordTime) {
                        //计算时间
                        thisHeaderFooter.append('g')
                            .attr('dctype', 'dcHeadFooter_' + index + '_Text')
                            .selectAll('text')
                            .data([...new Array(thisBand.length).keys()])
                            .join('text')
                            .attr('style', `font-size:${defaultData.FontSize}pt;font-family:${(defaultData.FontName || '宋体')};text-anchor:middle;`)
                            .attr('fill', () => {
                                return d.TextColorValue ? d.TextColorValue : "black";
                            })
                            .attr('xml:space', 'preserve')
                            .text((j) => {
                                ////console.log(j)
                                var diffNumArr = [];
                                for (var k = 0; k < haskeyWordTime.length; k++) {
                                    var diffNum = (thisBand[j] - haskeyWordTime[k]) / 1000 / 60 / 60 / 24;
                                    if (diffNum > -1) {
                                        diffNum = Math.ceil(diffNum);

                                        //判断是否存在结束时间
                                        if (hasEndWordTime) {
                                            var enddiffNumn = (thisBand[j] - hasEndWordTime) / 1000 / 60 / 60 / 24;
                                            if (enddiffNumn > -1) {
                                                diffNumArr = [];
                                                break;
                                            }
                                        }
                                        if (diffNum == 0) {
                                            if (d.AfterOperaDaysFromZero === true) {
                                                diffNumArr.unshift(diffNum);
                                                continue;
                                            } else {
                                                if (d.AfterOperaDaysBeginOne === true) {
                                                    diffNumArr.unshift(diffNum + 1);
                                                    continue;
                                                }
                                                diffNumArr.unshift(diffNum);
                                                continue;
                                            }
                                        }
                                        if (d.AfterOperaDaysBeginOne === true) {
                                            diffNumArr.unshift(diffNum + 1);
                                            continue;
                                        }
                                        diffNumArr.unshift(diffNum);
                                        continue;
                                    }
                                }

                                //[DUWRITER5_0-3853] 20241113 lxy 术后天数兼容河北省的标准
                                var surgicalReverseAndChinese = rootElement.getAttribute("SurgicalReverseAndChinese") || null;
                                surgicalReverseAndChinese = surgicalReverseAndChinese === true || surgicalReverseAndChinese === "true";
                                if (surgicalReverseAndChinese && d.StartDateKeyword == "手术") {
                                    var surgicalDaysText = "";
                                    diffNumArr = diffNumArr.reverse();
                                    //console.log(diffNumArr);
                                    if (diffNumArr[0] == 0 && diffNumArr.length === 1) {
                                        surgicalDaysText = "术日";
                                    } else {
                                        diffNumArr.forEach((item, index) => {
                                            if (item == 0) {
                                                surgicalDaysText += "/术" + diffNumArr.length;
                                            } else {
                                                surgicalDaysText += surgicalDaysText.length ? ("/" + item) : (item + "");
                                            }

                                        });
                                    }
                                    return surgicalDaysText;
                                }

                                return diffNumArr.join('/');
                            })
                            .attr('x', (j) => {
                                return calculationData.FirstTextWidth + (j * calculationData.HeaderAndFooterStepWidth) + calculationData.HeaderAndFooterStepWidth / 2;
                            })
                            .attr('y', () => {
                                //[DUWRITER5_0-3727] 20241016 lxy 解决修改指定高度后，内容错位的问题
                                var thisy = 0;
                                headerFooterLinesSingleHeight.map((item, itemIndex) => {
                                    if (itemIndex < index) {
                                        thisy += item;
                                    }
                                });
                                thisy = thisy + (calculationData.LableHeight / 2 / 1.5) + (headerFooterLinesSingleHeight[index] / 2);
                                return thisy;
                            });
                    }
                }
            }
            d.Title = oldTitle;
        }

        if (breakFooter) {
            //开启了自动换行展示时，需要重新计算高度后渲染svg
            WriterControl_DrawFu.DrawSvg(rootElement);
        }
    },

    //绘制Y轴
    DrawYAxisInfos: function (svg, calculationData, defaultData, svgEle, i) {
        //计算拿到Y轴总高带
        calculationData.YAxisInfosTotalHeight = calculationData.InnerHeight - calculationData.SpecifyTitleHeight - (calculationData.LableHeight * calculationData.AllHeaderLabels.length) - calculationData.HeaderLinesTotalHeight - calculationData.FooterLinesTotalHeight - calculationData.FooterDescriptionHeight - calculationData.LableHeight;
        //在此处判断起始位置和结束位置
        if (defaultData.DataGridTopPadding) {
            //if (defaultData.DataGridTopPadding > 0 && defaultData.DataGridTopPadding < 1) {
            //    defaultData.DataGridTopPadding = 0.1;
            //} else {
            //    defaultData.DataGridTopPadding = 0;
            //}
            if (defaultData.DataGridTopPadding > 1) {
                defaultData.DataGridTopPadding = 0.1;
            } else if (defaultData.DataGridTopPadding < 0) {
                defaultData.DataGridTopPadding = 0;
            }
            //先计算出起始位置
            calculationData.GridTopPadding = calculationData.YAxisInfosTotalHeight * defaultData.DataGridTopPadding;
            // calculationData.NewYAxisInfosTotalHeight = calculationData.YAxisInfosTotalHeight - calculationData.GridTopPadding;
        }
        if (defaultData.DataGridBottomPadding) {
            //if (defaultData.DataGridBottomPadding > 0 && defaultData.DataGridBottomPadding < 1) {
            //    defaultData.DataGridBottomPadding = 0.1;
            //} else {
            //    defaultData.DataGridBottomPadding = 0;
            //}
            if (defaultData.DataGridBottomPadding > 1) {
                defaultData.DataGridBottomPadding = 0.1;
            } else if (defaultData.DataGridBottomPadding < 0) {
                defaultData.DataGridBottomPadding = 0;
            }
            //计算结束高度
            calculationData.GridBottomPadding = calculationData.YAxisInfosTotalHeight * defaultData.DataGridBottomPadding;
            // calculationData.NewYAxisInfosTotalHeight = calculationData.NewYAxisInfosTotalHeight - calculationData.GridBottomPadding;
        }
        //Y轴包裹元素
        var dcYAxisInfosG = svg
            .append('g')
            .attr('dctype', "dcYAxisInfosG")
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin},${calculationData.TopMargin + calculationData.SpecifyTitleHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length) + calculationData.HeaderLinesTotalHeight})`
            );
        svgEle[i].dcYAxisInfosG = dcYAxisInfosG;
        var index = 0;
        var totleWidth = 0;

        //循环绘制数据
        calculationData.YAxisInfosData.map((yData, yIndex) => {

            //[DUWRITER5_0-3489]lxy 20240830 解决隐藏Y轴Id丢失问题
            var uid = yData[1].UID || `dcYAxisInfos_` + yIndex;

            // yData[1].UID = uid;
            var thisNameYAxis = calculationData.YAxisInfosData["dc_" + yData[1].Name];

            //对齐方式
            var TickTextAlignment = thisNameYAxis.TickTextAlignment;
            //重新计算当前Y轴的高度
            thisNameYAxis.thisYInfoHeight = calculationData.YAxisInfosTotalHeight;


            var YTopPadding = yData[1].TopPadding;
            thisNameYAxis.YTopHeight = 0;
            // YTopPadding = YTopPadding > 1 ? 1 : YTopPadding;
            var YBottomPadding = yData[1].BottomPadding;
            thisNameYAxis.YBottomHeight = 0;
            // YBottomPadding = YBottomPadding > 1 ? 1 : YBottomPadding;
            if (YTopPadding > 0 && YTopPadding < 1) {
                //判断YTopPadding和DataGridTopPadding那个更大
                thisNameYAxis.YTopHeight = calculationData.YAxisInfosTotalHeight * YTopPadding;
                thisNameYAxis.YTopHeight = thisNameYAxis.YTopHeight > calculationData.GridTopPadding ? thisNameYAxis.YTopHeight : calculationData.GridTopPadding;
                thisNameYAxis.thisYInfoHeight -= thisNameYAxis.YTopHeight;
                calculationData.HasYTopPadding.push(yData[1].Name);
            } else if (calculationData.GridTopPadding != 0) {
                thisNameYAxis.YTopHeight = calculationData.GridTopPadding;
                thisNameYAxis.thisYInfoHeight -= thisNameYAxis.YTopHeight;
                calculationData.HasYTopPadding.push(yData[1].Name);
            }
            if (YBottomPadding > 0 && YBottomPadding < 1) {
                thisNameYAxis.YBottomHeight = calculationData.YAxisInfosTotalHeight * YBottomPadding;
                thisNameYAxis.YBottomHeight = thisNameYAxis.YBottomHeight > calculationData.GridBottomPadding ? thisNameYAxis.YBottomHeight : calculationData.GridBottomPadding;
                thisNameYAxis.thisYInfoHeight -= thisNameYAxis.YBottomHeight;
                calculationData.HasYBottomPadding.push(yData[1].Name);
            } else if (calculationData.GridBottomPadding != 0) {
                thisNameYAxis.YBottomHeight = calculationData.GridBottomPadding;
                if (!(YTopPadding > 0 && YTopPadding < 1)) {
                    thisNameYAxis.thisYInfoHeight -= thisNameYAxis.YBottomHeight;
                }
                calculationData.HasYBottomPadding.push(yData[1].Name);
            }
            if (thisNameYAxis.Visible === false) {
                return;
            }

            if (thisNameYAxis.Style == "Text" && calculationData.DCType == "DCTemperatureControlForWASM") {
                return;
            }
            if (calculationData.ShadowNameArr.Name) {
                var hasIndex = calculationData.ShadowNameArr.Name.indexOf(yData[1].Name);
                if (hasIndex >= 0 && calculationData.DCType == "DCTemperatureControlForWASM") {
                    return;
                }
            }
            if (yData[1].MergeIntoLeft) {
                index--;
                totleWidth -= calculationData.YAxisInfosSingleWidth[index];
            }


            var thisYAxisInfoscales = [];
            if (thisNameYAxis && thisNameYAxis.Scales) {
                thisNameYAxis.Scales.forEach((scalesItem) => {
                    scalesItem.Value = parseFloat(scalesItem.Value) || 0;
                    scalesItem.ScaleRate = parseFloat(scalesItem.ScaleRate) || 0;


                    thisYAxisInfoscales.push(scalesItem);

                });

                //排序
                thisYAxisInfoscales = thisYAxisInfoscales.sort((a, b) => {
                    return parseFloat(a.ScaleRate) - parseFloat(b.ScaleRate);
                });

                //记录区间值
                thisYAxisInfoscales.forEach((item, index) => {
                    // item["RangeArray"] = [];
                    // if (index < thisYAxisInfoscales.length - 1) {
                    //     item["RangeArray"] = [parseFloat(item.Value), parseFloat(thisYAxisInfoscales[index + 1].Value)];
                    // }
                    if (index > 0) {
                        item["RangeValueArray"] = [parseFloat(thisYAxisInfoscales[index - 1].Value), parseFloat(item.Value)];
                        // item["RangeYInfoHeightArray"] = [parseFloat(thisYAxisInfoscales[index - 1].Value), parseFloat(item.Value)];
                    }
                });

                thisNameYAxis.Scales = thisYAxisInfoscales;

            }

            //d.UID = uid;
            var thisYAxisInfos = dcYAxisInfosG.append('g')
                .attr('uid', uid);
            var titleColorValue = yData[1].TitleColorValue;
            var dcYAxisInfos_Text = thisYAxisInfos.append('g')
                .attr("dctype", "dcYAxisInfos_Text")
                .selectAll('text')
                .data(thisYAxisInfoscales.length ? thisYAxisInfoscales : yData[0])
                .join('text')
                .attr('style', `font-family:${(defaultData.FontName || '宋体')};font-size:${defaultData.FontSize}pt;fill:${titleColorValue ? titleColorValue : "black"};text-anchor: middle`)
                .html((d, i) => {
                    if (thisYAxisInfoscales.length) {
                        return thisYAxisInfoscales[i].Value;
                    }


                    //按默认的Y轴刻度计算
                    if (i === 0) {

                        //是否需要显示图标
                        if (yData[1].ShowLegendInRule === true || yData[1].ShowLegendInRule === 'true') {
                            //判断是否为空心圆
                            var symbolStyle = yData[1].SymbolStyle;
                            // var symbolSize = yData[1].SymbolSize || 4;
                            // symbolSize = parseFloat((symbolSize / 300 * 96.00001209449).toFixed(2)) / 2;

                            var symbolsX = totleWidth + (calculationData.YAxisInfosSingleWidth[index] / 2);
                            if (TickTextAlignment === 'Near') {
                                symbolsX = totleWidth + calculationData.singleLableTextWidth;
                            } else if (TickTextAlignment === 'Far') {
                                symbolsX = totleWidth + (calculationData.YAxisInfosSingleWidth[index] - calculationData.singleLableTextWidth);
                            }


                            //绘制图标
                            var symbolColorValue = yData[1].SymbolColorValue ? yData[1].SymbolColorValue : "#ff0000";
                            if (symbolStyle == "HollowCicle" || symbolStyle == "circular") {
                                symbolStyle = "HollowCicle";
                                symbolColorValue = "#ffffff";
                            }
                            WriterControl_DrawFu.IconDrawObj()[symbolStyle] && WriterControl_DrawFu.IconDrawObj()[symbolStyle]({
                                content: thisYAxisInfos,
                                data: [0],
                                x: () => { return symbolsX; },
                                y: () => { return calculationData.LableHeight / 1.5 * 2; },
                                fill: symbolColorValue,
                                stroke: yData[1].SymbolColorValue ? yData[1].SymbolColorValue : "#ff0000",
                                r: 4
                            });
                        }

                        //是否显示标题
                        if (yData[1].Title) {
                            //绘制Title和BottomTitle
                            thisYAxisInfos.append('g')
                                .attr("dctype", "dcYAxisInfos_Title")
                                .selectAll("text")
                                .data([0])
                                .join('text')
                                .attr('style', `font-family:${(defaultData.FontName || '宋体')};font-size:${defaultData.FontSize}pt;fill:${titleColorValue ? titleColorValue : "black"};text-anchor: middle`)
                                .attr("x", () => {
                                    var x = totleWidth + (calculationData.YAxisInfosSingleWidth[index] / 2);
                                    if (TickTextAlignment === 'Near') {
                                        x = totleWidth + calculationData.singleLableTextWidth * 2;
                                    } else if (TickTextAlignment === 'Far') {
                                        x = totleWidth + (calculationData.YAxisInfosSingleWidth[index] - (calculationData.singleLableTextWidth * (yData[1].Title.length || 2)));
                                    }
                                    return x;
                                })
                                .attr("y", () => {
                                    //if (YTopPadding > 0 && YTopPadding < 1) {
                                    //    return calculationData.GridTopPadding + calculationData.LableHeight / 1.5;
                                    //} else {
                                    //    return calculationData.LableHeight / 1.5;
                                    //}
                                    return calculationData.LableHeight / 1.5;
                                })
                                .text(yData[1].Title);


                            if (thisNameYAxis.YTopHeight == 0 || yData[1].MergeIntoLeft) {
                                d = "";
                            }
                        }

                    }
                    if (i == yData[0].length - 1) {
                        if (yData[1].BottomTitle) {
                            //绘制Title和BottomTitle
                            thisYAxisInfos.append('g')
                                .attr("dctype", "dcYAxisInfos_BottomTitle")
                                .selectAll("text")
                                .data([0])
                                .join('text')
                                .attr('style', `font-family:${(defaultData.FontName || '宋体')};font-size:${defaultData.FontSize}pt;fill:${titleColorValue ? titleColorValue : "black"};text-anchor: middle`)
                                .attr("x", () => {
                                    var x = totleWidth + (calculationData.YAxisInfosSingleWidth[index] / 2);
                                    if (TickTextAlignment === 'Near') {
                                        x = totleWidth + calculationData.singleLableTextWidth * 2;
                                    } else if (TickTextAlignment === 'Far') {
                                        x = totleWidth + (calculationData.YAxisInfosSingleWidth[index] - (calculationData.singleLableTextWidth * 1.5));

                                    }
                                    return x;
                                })
                                .attr("y", calculationData.YAxisInfosTotalHeight - 2)
                                .text(yData[1].BottomTitle);

                            if (thisNameYAxis.YBottomHeight == 0) {
                                d = "";
                            }
                        }
                    }
                    return `${d}`;
                })
                .attr('x', (d, i) => {



                    var x = totleWidth;
                    return x;
                })
                .attr('y', (d, i) => {
                    //存在自定义Y轴刻度时
                    if (thisYAxisInfoscales.length) {

                        var startY = calculationData.TopMargin + calculationData.SpecifyTitleHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length) + calculationData.HeaderLinesTotalHeight;
                        //顶部留白
                        var TopPadding = thisNameYAxis.TopPadding < 0 ? 0 : thisNameYAxis.TopPadding;
                        //底部留白
                        var BottomPadding = thisNameYAxis.BottomPadding < 0 ? 0 : thisNameYAxis.BottomPadding;

                        //Y轴刻度最高点
                        var topScalesYPoint = TopPadding;
                        //Y轴刻度最低点
                        var BottomScalesYPoint = calculationData.YAxisInfosTotalHeight - BottomPadding;


                        //当前刻度
                        var thisScale = thisYAxisInfoscales[i] || null;
                        var scaleRate = thisScale && thisScale.ScaleRate && parseFloat(thisScale.ScaleRate);//当前刻度Y轴占比

                        if (scaleRate === 0) {
                            //最低点
                            thisYAxisInfoscales[i].YTopHeight = BottomScalesYPoint;//最低点
                        } else if (scaleRate === 1) {
                            //最高点
                            thisYAxisInfoscales[i].YTopHeight = topScalesYPoint + (calculationData.LableHeight / 1.5);//最高点
                        } else {
                            // var 
                            //自定义比例点
                            thisYAxisInfoscales[i].YTopHeight = calculationData.YAxisInfosTotalHeight - ((calculationData.YAxisInfosTotalHeight - TopPadding - BottomPadding) * scaleRate);
                        }

                        return thisYAxisInfoscales[i].YTopHeight;
                    }



                    //默认的Y轴刻度计算
                    if (i == 0) {
                        return thisNameYAxis.YTopHeight + calculationData.LableHeight / 1.5;
                    } else if (i == yData[0].length - 1) {
                        return thisNameYAxis.YTopHeight + (thisNameYAxis.thisYInfoHeight / (yData[0].length - 1) * i);
                    } else {
                        return thisNameYAxis.YTopHeight + calculationData.LableHeight / 1.5 + ((thisNameYAxis.thisYInfoHeight - calculationData.LableHeight / 1.5) / (yData[0].length - 1) * i);
                    }

                });

            //循环所有text,纠正数字的左对齐位置
            var AllText = dcYAxisInfos_Text.nodes();
            AllText.forEach((item) => {
                var itemWidth = item.getBBox().width; //获取当前元素的宽度
                var x = item.getAttribute("x");
                x = parseFloat(x);
                if (TickTextAlignment === 'Near') {
                    x = (1 + x + (itemWidth / 2));
                    item.setAttribute("x", x);

                } else if (TickTextAlignment === 'Far') {
                    console.log((calculationData.YAxisInfosSingleWidth[index] - (itemWidth / 2)));
                    x = x + (calculationData.YAxisInfosSingleWidth[index] - (itemWidth / 2));
                    item.setAttribute("x", x - 1);
                } else {
                    x = x + ((calculationData.YAxisInfosSingleWidth[index] / 2));
                    item.setAttribute("x", x - 1);
                }

            });

            totleWidth += calculationData.YAxisInfosSingleWidth[index];
            if (yData[1].MergeIntoLeft) {
                if (yData[1].TopPadding > 0 && yData[1].BottomPadding < 1) {
                    //绘制横线
                    thisYAxisInfos.append('g')
                        .attr("dctype", "dcYAxiosInfos_Pos")
                        .selectAll("line")
                        .data([0])
                        .join('line')
                        .attr('y1', thisNameYAxis.YTopHeight) // 这个还要修改一下
                        .attr('x1', 0)
                        .attr('y2', thisNameYAxis.YTopHeight)
                        .attr('x2', () => {
                            calculationData.PainScoreInfo.Top = calculationData.SpecifyTitleHeight + calculationData.HeaderLinesTotalHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length) + thisNameYAxis.YTopHeight;
                            calculationData.PainScoreInfo.Height = calculationData.YAxisInfosTotalHeight - thisNameYAxis.YTopHeight;
                            calculationData.PainScoreInfo.Width = totleWidth - calculationData.YAxisInfosSingleWidth[index];
                            return totleWidth;
                        })
                        .attr('stroke', "black");
                }

            } else {
                if (!Object.keys(yData[1]).includes("BorderVisible") || yData[1].BorderVisible) {
                    //绘制垂直线
                    thisYAxisInfos.append('g')
                        .attr("dctype", "dcYAxiosInfos_Pos")
                        .selectAll("line")
                        .data([0])
                        .join('line')
                        .attr('y1', 0) // 这个还要修改一下
                        .attr('x1', totleWidth)
                        .attr('y2', calculationData.YAxisInfosTotalHeight)
                        .attr('x2', totleWidth)
                        .attr('stroke', "black");
                }
            }

            index++;
            ////console.log(index);
        });
    },

    //绘制网格线
    DrawbgLine: function (svg, calculationData, defaultData, svgEle, i) {
        //网格线包裹元素
        var dcbgLineG = svg
            .append('g')
            .attr('dctype', "dcbgLineG")
            .attr("fill", gridLineColorValue)
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin + calculationData.FirstTextWidth},${calculationData.TopMargin + calculationData.SpecifyTitleHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length) + calculationData.HeaderLinesTotalHeight})`
            );
        svgEle[i].dcbgLineG = dcbgLineG;
        if (defaultData.YAxisInfos && defaultData.YAxisInfos.length > 0) {
            var bigGridLineColorValue = defaultData.BigVerticalGridLineColorValue;
            bigGridLineColorValue = bigGridLineColorValue ? bigGridLineColorValue : "red";
            var gridLineWidth = defaultData.BigVerticalGridLineWidth;
            gridLineWidth = gridLineWidth ? gridLineWidth : 2;
            gridLineWidth = parseFloat((gridLineWidth / 300 * 96.00001209449).toFixed(2));
            gridLineWidth = gridLineWidth >= 2 ? gridLineWidth : 1;
            var verticalData = [...new Array(calculationData.Verticalallength).keys()];
            var gridLineColorValue = defaultData.GridLineColorValue ? defaultData.GridLineColorValue : "transparent";
            var thinLineWidth = defaultData.ThinLineWidth;
            thinLineWidth = thinLineWidth ? thinLineWidth : 1;
            thinLineWidth = parseFloat((thinLineWidth / 300 * 96.00001209449).toFixed(2));
            thinLineWidth = thinLineWidth >= 1 ? thinLineWidth : 1;
            //这里是绘制纵向网格线
            var dcBgVerticalLineG = dcbgLineG.append('g')
                .attr("dctype", "dcBgVerticalLine")
                .selectAll('line')
                .data(verticalData)
                .join('line')
                .attr('x1', (d, i) => {
                    return i * calculationData.VerticalStepWidth;
                })
                .attr('y1', 0)
                .attr('y2', calculationData.YAxisInfosTotalHeight)
                .attr('x2', (d, i) => {
                    return i * calculationData.VerticalStepWidth;
                })
                .attr('fill', 'none')
                .attr('class', 'dataLine')
                .attr('stroke', (d, i) => {
                    if (i == 0) {
                        return "black";
                    } else {
                        return gridLineColorValue;
                    }
                })
                .attr('stroke-linejoin', "round")
                .attr('stroke-linecap', "round")
                .attr('style', (d, i) => {
                    if (i % (calculationData.TickTextsData.length) == 0 && i != 0) {
                        return `stroke-width: ${gridLineWidth}; stroke: ${bigGridLineColorValue};`;
                    }
                    return `stroke-width: ${thinLineWidth}`;
                });

            calculationData.NewGridYSplitNum = defaultData.GridYSplitNum;
            if (calculationData.GridTopPadding > 0) {
                calculationData.NewGridYSplitNum++;
            }
            if (calculationData.GridBottomPadding > 0) {
                calculationData.NewGridYSplitNum++;
            }
            calculationData.Horizontallength = calculationData.NewGridYSplitNum * defaultData.GridYSpaceNum;
            var horizontalData = [...new Array(calculationData.Horizontallength).keys()];
            ////console.log(calculationData.Horizontallength);
            //计算背景线区域横向步进高度
            calculationData.HorizontStepHeight = parseFloat((calculationData.YAxisInfosTotalHeight / calculationData.Horizontallength).toFixed(2));
            dcbgLineG.append('g')
                .attr("dctype", "dcBghorizontalLine")
                .selectAll('line')
                .data(horizontalData)
                .join('line')
                .attr('x1', 0)
                .attr('y1', (d, i) => {
                    return i * calculationData.HorizontStepHeight;
                })
                .attr('y2', (d, i) => {
                    return i * calculationData.HorizontStepHeight;
                })
                .attr('x2', calculationData.BgLineWidth)
                .attr('fill', 'none')
                .attr('class', 'dataLine')
                .attr('stroke', (d, i) => {
                    if (i == 0) {
                        return "black";
                    } else {
                        return gridLineColorValue;
                    }
                })
                .attr('stroke-width', 1)
                .attr('stroke-linejoin', "round")
                .attr('stroke-linecap', "round")
                .attr('style', (d, i) => {
                    if (i % parseInt(defaultData.GridYSpaceNum) == 0) {
                        return 'stroke-width: 1; stroke: black;';
                    }
                    return `stroke-width: ${thinLineWidth}`;
                });

            var allVerticalLine = dcBgVerticalLineG.nodes();
            var needLine = allVerticalLine.filter((item, index) => index % (calculationData.TickTextsData.length) == 0);
            var newDcBgVerticalLine = dcbgLineG.append('g')
                .attr("dctype", "dcBgVerticalLine");
            newDcBgVerticalLine = newDcBgVerticalLine.node();
            needLine.forEach(item => {
                newDcBgVerticalLine.appendChild(item);
            });
        }

    },

    //绘制数据折线
    DrawValuePath: function (svg, calculationData, defaultData, svgEle, i, value) {
        //网格线包裹元素
        var dcValuePathG = svg
            .append('g')
            .attr('dctype', "dcValuePathG")
            .attr(
                'transform',
                `translate(${calculationData.LeftMargin + calculationData.FirstTextWidth},${calculationData.TopMargin + calculationData.SpecifyTitleHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length) + calculationData.HeaderLinesTotalHeight})`
            );
        svgEle[i].dcValuePathG = dcValuePathG;
        // var value = defaultData.Values
        var thisBand = calculationData.AllTimeData.slice(calculationData.Verticalallength * i, calculationData.Verticalallength * i + calculationData.Verticalallength + 1);
        //保证顺序
        thisBand = thisBand.sort((a, b) => {
            return a - b;
        });
        ////console.log(thisBand);

        if (defaultData.EnableDataGridLinearAxisMode === true) {
            var maxScaleWidth = (thisBand.length - 1) * calculationData.VerticalStepWidth;
            //分段写入标尺
            var xScale = d3.scaleLinear([thisBand[0], thisBand[thisBand.length - 1]], [0, maxScaleWidth]);
        } else {
            var maxScaleWidth = thisBand.length * calculationData.VerticalStepWidth;
            //分段写入标尺
            var xScale = d3.scaleBand(thisBand, [0, maxScaleWidth])
                .round(false);
        }
        // var svg3 = d3.select("[dctype=page]");
        // var g3 = svg3.append('g')
        //     .attr("transform",`translate(${100},${100})`)
        //     //.attr("id","maingroup")
        // var a = [];
        // for(var l=0;l<43;l++){
        //     a.push(l);
        // }
        // // //console.log(maxScaleWidth)
        // var xs = d3.scaleBand(a,[0, (thisBand.length - 1) * calculationData.VerticalStepWidth]).paddingInner(1);
        // var xAxis = d3.axisBottom(xs)
        // dcValuePathG.append('g').call(xAxis)

        var minMax = null;
        if (value && value.length > 0) {
            ////console.log(value)
            //循环value 并确定value是YAxisInfos中存在的数据
            for (var j = 0; j < value.length; j++) {
                ////console.log(value[j]);
                // if(value[j].Name == "tiwen"){
                minMax = calculationData.YAxisInfosData["dc_" + value[j].Name];

                if (minMax) {
                    //自此处对value的值进行判断,如果存在HollowCovertTargetName则移到最后绘制
                    if (minMax.HollowCovertTargetName) {
                        //将value移到后面去目标值之后
                        var thisIndex = null;
                        for (var k = j; k < value.length; k++) {
                            if (value[k].Name == minMax.HollowCovertTargetName) {
                                thisIndex = k;
                                break;
                            }
                        }
                        if (thisIndex != null) {
                            var oldValue = value.splice(j, 1);
                            value.splice(thisIndex, 0, oldValue[0]);
                            j--;
                            continue;
                        }
                    }

                    var YBottomPositin = minMax.YTopHeight + minMax.thisYInfoHeight;
                    var YTopPosition = minMax.YTopHeight;
                    //设置标尺
                    var yScale = WriterControl_DrawFu.ReturnScale(parseFloat(minMax.MinValue), parseFloat(minMax.MaxValue), [YBottomPositin, YTopPosition]);
                    if (minMax.RedLineValue) {
                        var lineValue = parseFloat(minMax.RedLineValue);
                        if (!isNaN(lineValue)) {
                            lineValue = yScale(lineValue);
                            dcValuePathG.append("line")
                                .attr('y1', lineValue)
                                .attr('x1', 0)
                                .attr('y2', lineValue)
                                .attr('x2', calculationData.BgLineWidth)
                                .attr('stroke', "red");
                        }
                    }

                    if (value[j].Datas) {
                        //创建外层包裹元素
                        var thisValuePath = dcValuePathG
                            .append('g')
                            .attr("dctype", `dcValuePath_${value[j].Name}`)
                            .attr("name", value[j].Name);

                        var symbolStyle = minMax.SymbolStyle;
                        var symbolColorValue = minMax.SymbolColorValue ? minMax.SymbolColorValue : "#ff0000";
                        //计算图例大小
                        var symbolSize = minMax.SymbolSize;
                        symbolSize = symbolSize ? parseFloat(symbolSize) : 20;
                        symbolSize = parseFloat((symbolSize / 300 * 96.00001209449).toFixed(2)) / 2;
                        // //console.log(value[j]);
                        //var dcValuePathG = svg
                        //    .append('g')
                        //    .attr('dctype', "dcValuePathG")
                        //    .attr('name', value[j].Name)
                        //    .attr(
                        //        'transform',
                        //        `translate(${calculationData.LeftMargin + calculationData.FirstTextWidth + (calculationData.VerticalStepWidth / 2)},${calculationData.TopMargin + calculationData.SpecifyTitleHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length) + calculationData.HeaderLinesTotalHeight})`
                        //    )
                        //svgEle[i].dcValuePathG = dcValuePathG;
                        //根据不同的value绘制不同的数据
                        ////console.log(value[j]);

                        //if (minMax.Style == "Text") {
                        //    YTopPosition = 0;
                        //    YBottomPositin = calculationData.YAxisInfosTotalHeight
                        //}

                        var thisSvgDataArr = [];
                        var thisIndex = 0;
                        //对数据进行解析
                        value[j].Datas.forEach((x, y) => {
                            var thisTime = new Date(x.Time);
                            if (thisTime) {
                                thisTime = thisTime.getTime();
                                if (thisTime >= thisBand[0] && thisTime < thisBand[thisBand.length - 1]) {
                                    if (thisSvgDataArr[thisIndex] == null) {
                                        thisSvgDataArr[thisIndex] = [];
                                    }
                                    if (minMax.Style == "Text" && x.Text) {
                                        thisSvgDataArr[thisIndex].push(x);
                                    } else {
                                        //直接判断断线情况
                                        if (x.LineStop === true) {
                                            if (x.Value && x.Value != "-10000") {
                                                if (thisSvgDataArr[thisIndex] && thisSvgDataArr[thisIndex].length > 0) {
                                                    thisIndex++;
                                                    thisSvgDataArr[thisIndex] = [];
                                                }
                                                thisSvgDataArr[thisIndex].push(x);
                                                thisIndex++;
                                                return;
                                            }
                                        }

                                        //允许断线
                                        if (minMax.AllowInterrupt === true || minMax.AllowInterrupt == 'true') {
                                            if (x.Value == null || x.Value == "-10000") {
                                                thisIndex++;
                                                return;
                                            }
                                            //同意超出范围
                                            if (!minMax.AllowOutofRange) {
                                                if (x.Value < parseFloat(minMax.MinValue) || x.Value > parseFloat(minMax.MaxValue)) {
                                                    thisIndex++;
                                                    return;
                                                }
                                            }
                                            //自此处修改是因为x.Value如果为0会导致画线为空白
                                            if (x.Value == 0) {
                                                x.Value = 0.0001;
                                            }
                                            thisSvgDataArr[thisIndex].push(x);
                                        } else {
                                            if (x.Value && x.Value != "-10000") {
                                                if (!minMax.AllowOutofRange) {
                                                    if (x.Value >= parseFloat(minMax.MinValue) && x.Value <= parseFloat(minMax.MaxValue)) {
                                                        thisSvgDataArr[thisIndex].push(x);
                                                    }
                                                } else {
                                                    thisSvgDataArr[thisIndex].push(x);
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        });
                        ////console.log(thisSvgDataArr)
                        //在此处进行判断进行绘制
                        var allPointIndex = [];
                        if (minMax.HollowCovertTargetName) {
                            //获取到对应所有的数据点的左边
                            ////console.log(dcValuePathG)
                            var targetNameDOM = dcValuePathG.node().querySelector(`[dctype=dcValuePath_${minMax.HollowCovertTargetName}]`) || null;
                            var allPointDOM = targetNameDOM ? targetNameDOM.querySelectorAll("[dcSymbolX]") : null;
                            if (allPointDOM && allPointDOM.length > 0) {
                                for (var d = 0; d < allPointDOM.length; d++) {
                                    allPointIndex.push({
                                        x: allPointDOM[d].getAttribute('dcSymbolX'),
                                        y: allPointDOM[d].getAttribute('dcSymbolY')
                                    });
                                }
                            }
                        }


                        thisSvgDataArr.forEach((thisSvgData) => {
                            if (thisSvgData.length == 0) {
                                return;
                            }
                            if (minMax.Style == "Text") {
                                thisValuePath.append("g")
                                    .selectAll('text')
                                    .data([...new Array(thisSvgData.length)])
                                    .join('text')
                                    .attr('style', (d, i) => {

                                        //[DUWRITER5_0-3334]lxy 20240809 修复文字大小不生效问题
                                        return `font-size:${thisSvgData[i].TextFontSize || defaultData.FontSize}pt;font-family:${thisSvgData[i].TextFontName || (defaultData.FontName || '宋体')}; writing-mode: vertical-lr; /* 从上到下书写，从左到右 */
                                        text-orientation: upright; /* 文本方向为正常方向 */
                                        white-space: nowrap; /* 防止文本换行 */
                                        `;
                                    })
                                    .attr('fill', (d, i) => {
                                        var textColor = "#000";
                                        textColor = minMax.SymbolColorValue ? minMax.SymbolColorValue : textColor;
                                        textColor = thisSvgData[i].TextColorValue ? thisSvgData[i].TextColorValue : textColor;
                                        return textColor;
                                    })
                                    // .attr('title', (d, i) => {
                                    //     var result = thisSvgData[i].Time + " " + thisSvgData[i].Text;
                                    //     result = result.replace(/&dc&/g, ',');
                                    //     return result;
                                    // })
                                    .html((d, i) => {
                                        var result = thisSvgData[i].Text;
                                        var thisResult = result && result.split && result.split("&dc&");
                                        var newResultHtml = "";
                                        if (thisResult) {
                                            if (thisResult.length == 1) {
                                                newResultHtml = thisResult[0];
                                            } else {
                                                var newResult = "";
                                                thisResult.forEach(item => {
                                                    if (minMax.VerticalLine === true) {
                                                        if (newResult != "") {
                                                            newResult += "-";
                                                        }
                                                    }
                                                    newResult += item;
                                                });
                                                newResultHtml = newResult;
                                            }
                                        } else {
                                            newResultHtml = "";
                                        }

                                        //[DUWRITER5_0-3331]lxy 20240810 修复特殊字符被旋转问题，如下箭头↓被渲染为左箭头←的问题
                                        if (newResultHtml && newResultHtml.length) {
                                            //title放在子元素上
                                            var titletext = thisSvgData[i].Time + " " + thisSvgData[i].Text;
                                            titletext = titletext.replace(/&dc&/g, ',');
                                            //将所有文字用tspan包裹，正向渲染，避免旋转
                                            newResultHtml = newResultHtml.split('').map(char => `<tspan title="${titletext}" style="${char.match(/[a-zA-Z0-9]/g) ? '' : 'writing-mode: lr;'}" >${char}</tspan>`).join('');
                                        }
                                        return newResultHtml;
                                    })

                                    .attr('x', (d, i) => {
                                        var thisTime = new Date(thisSvgData[i].Time).getTime();
                                        if (defaultData.EnableDataGridLinearAxisMode !== true) {
                                            if (thisBand.indexOf(thisTime) < 0) {
                                                thisBand.find((x, y) => {
                                                    if (x > thisTime) {
                                                        if (y == 0) {
                                                            thisTime = thisBand[y];
                                                            return true;
                                                        } else {
                                                            thisTime = thisBand[y - 1];
                                                            return true;
                                                        }
                                                    }
                                                });
                                            }
                                            var xValue = xScale(thisTime);
                                            xValue += parseFloat(defaultData.FontSize) / 2;
                                        } else {
                                            var xValue = xScale(thisTime);
                                        }
                                        return xValue;
                                    })
                                    .attr('y', (d, i) => {
                                        var thisValue = Number(thisSvgData[i].Value);
                                        if (thisValue >= parseFloat(minMax.MaxValue)) {
                                            thisValue = parseFloat(minMax.MaxValue) - 0.0001;
                                        } else if (thisValue <= parseFloat(minMax.MinValue)) {
                                            if (thisValue == -10000) {
                                                return yScale(thisValue);
                                            }
                                            thisValue = parseFloat(minMax.MinValue) + 0.0001;
                                        }
                                        ////console.log(thisValue)
                                        return yScale(thisValue);
                                    });
                                return;
                            }

                            const I = d3.map(thisSvgData, (_, i) => i);
                            var thisXValue = null;
                            var pointValueArr = [];
                            const line = d3
                                .line()
                                .defined((i) => thisSvgData[i].Value)
                                .x((i) => {
                                    //选中距离最近的值
                                    var thisTime = new Date(thisSvgData[i].Time).getTime();
                                    ////console.log(thisTime);
                                    if (defaultData.EnableDataGridLinearAxisMode !== true) {
                                        if (thisBand.indexOf(thisTime) < 0) {
                                            thisBand.find((e, d) => {
                                                if (e > thisTime) {
                                                    if (d == 0) {
                                                        thisTime = thisBand[d];
                                                        return true;
                                                    } else {
                                                        thisTime = thisBand[d - 1];
                                                        return true;
                                                    }
                                                }
                                            });
                                        }
                                        var xValue = xScale(thisTime);
                                        xValue += calculationData.VerticalStepWidth / 2;
                                    } else {
                                        var xValue = xScale(thisTime);
                                    }

                                    thisXValue = xValue;
                                    return xValue;

                                })
                                .y((i) => {
                                    var yValue = null;
                                    var thisValue = Number(thisSvgData[i].Value);
                                    //当存在自定义比例时
                                    if (minMax.Scales && minMax.Scales.length > 0) {

                                        var thisScaleY = 0;
                                        //如果存在自定义刻度，则使用自定义刻度
                                        for (var k = 0; k < minMax.Scales.length; k++) {

                                            var thisScale = minMax.Scales[k];
                                            var thisScalesValue = parseFloat(thisScale.Value);
                                            if (thisValue === thisScalesValue) {
                                                //与标尺点汇合时
                                                thisScaleY = thisScale.YTopHeight;
                                            } else if (thisScale && thisScale.RangeValueArray && thisScale.RangeValueArray.length > 0) {
                                                //在自定义区间范围内


                                                var prevScale = minMax.Scales[k - 1];//上一个位置
                                                var thisRangeValuePrev = thisScale.RangeValueArray[0];//上一个刻度值
                                                var thisRangeValueCurrent = thisScale.RangeValueArray[1];//当前刻度值
                                                if (thisValue > thisRangeValuePrev && thisValue < thisRangeValueCurrent) {
                                                    //目标数值差
                                                    var diffTargetValue = thisRangeValueCurrent - thisValue;

                                                    //范围差值
                                                    var diffHeight = prevScale.YTopHeight - thisScale.YTopHeight;

                                                    //范围数值差
                                                    var diffValue = thisRangeValueCurrent - thisRangeValuePrev;

                                                    //距离当前范围最高点的高度差(等比计算对应范围内的点位高度)
                                                    thisScaleY = thisScale.YTopHeight + (diffHeight * (diffTargetValue / diffValue));
                                                }
                                            }
                                        }
                                        thisSvgData[i]['ScaleY'] = yValue = thisScaleY;


                                    } else {

                                        if (thisValue > parseFloat(minMax.MaxValue)) {
                                            thisValue = parseFloat(minMax.MaxValue) - 0.0001;
                                            //超出最大值，写入文本
                                        } else if (thisValue < parseFloat(minMax.MinValue)) {
                                            thisValue = parseFloat(minMax.MinValue) + 0.0001;
                                        }
                                        yValue = yScale(thisValue);
                                    }


                                    //判断是否存在相同位置的点
                                    allPointIndex.forEach((item) => {
                                        if (thisXValue == item.x && yValue == item.y) {
                                            pointValueArr.push(i);
                                        }
                                    });

                                    return yValue;
                                });


                            var lineWidth = parseFloat(((minMax.LineWidth || 1) / 300 * 96.00001209449).toFixed(1));
                            //绘制连接线
                            WriterControl_DrawFu.GetDrawPath({
                                content: thisValuePath,
                                line: line(I.filter(i => thisSvgData[i].Value)),
                                stroke: minMax.SymbolColorValue ? minMax.SymbolColorValue : "#ff0000",
                                width: lineWidth >= 1 ? lineWidth : 1
                            });
                            var supportStyle = Object.keys(WriterControl_DrawFu.IconDrawObj());

                            if (symbolStyle == "HollowCicle") {
                                symbolStyle = "SolidCicle";
                                symbolColorValue = "#ffffff";
                            }

                            if (supportStyle.indexOf(symbolStyle) < 0) {
                                symbolStyle = "SolidCicle";
                            }

                            for (var z = 0; z < thisSvgData.length; z++) {
                                //自此处修改是因为x.Value如果为0会导致画线为空白
                                if (thisSvgData[z].Value == 0.0001) {
                                    thisSvgData[z].Value = 0;
                                }
                                //绘制图标点
                                var IconData = {
                                    content: thisValuePath,
                                    data: [z],
                                    x: (i) => {
                                        // 绘制点的坐标X轴
                                        var thisTime = new Date(thisSvgData[i].Time).getTime();
                                        if (defaultData.EnableDataGridLinearAxisMode !== true) {
                                            if (thisBand.indexOf(thisTime) < 0) {
                                                thisBand.find((e, d) => {
                                                    if (e > thisTime) {
                                                        if (d == 0) {
                                                            thisTime = thisBand[d];
                                                            return true;
                                                        } else {
                                                            thisTime = thisBand[d - 1];
                                                            return true;
                                                        }
                                                    }
                                                });
                                            }
                                            var xValue = xScale(thisTime);
                                            xValue += calculationData.VerticalStepWidth / 2;
                                        } else {
                                            var xValue = xScale(thisTime);
                                        }
                                        // //console.log(thisBand,thisTime)
                                        // //console.log(xValue);
                                        return xValue;
                                    },
                                    y: (i) => {
                                        var thisY = 0;
                                        var thisX = IconData.x(i);

                                        if (minMax && minMax.Scales && minMax.Scales.length > 0) {
                                            //画线时已经给自定义的区间高度赋值，此处可以直接使用ScaleY
                                            if (thisSvgData[i] && (thisSvgData[i].ScaleY || thisSvgData[i].ScaleY == 0)) {
                                                thisY = thisSvgData[i].ScaleY;
                                            }
                                        } else {
                                            // 绘制点的坐标Y轴
                                            var thisValue = Number(thisSvgData[i].Value);

                                            if (thisValue >= parseFloat(minMax.MaxValue)) {
                                                thisValue = parseFloat(minMax.MaxValue) - 0.0001;
                                            } else if (thisValue <= parseFloat(minMax.MinValue)) {
                                                thisValue = parseFloat(minMax.MinValue) + 0.0001;
                                            }
                                            thisY = yScale(thisValue);
                                        }

                                        if (minMax.ShowPointValue === true || thisSvgData[i].ShowPointValue === true) {
                                            //判断是否需要写入文本
                                            thisValuePath.append("text")
                                                .attr('style', `font-family:宋体;font-size:9px;text-anchor:middle;`)
                                                .attr('fill', 'black')
                                                .text(thisSvgData[i].Value)
                                                .attr('x', thisX)
                                                .attr('y', thisY + 12);
                                        }

                                        if (IconData.sign && thisSvgData[i].LanternValue && thisSvgData[i].LanternValue > 0 && minMax.EnableLanternValue !== false) {
                                            var thislanterValue = Number(thisSvgData[i].LanternValue);
                                            if (thislanterValue >= parseFloat(minMax.MaxValue)) {
                                                thislanterValue = parseFloat(minMax.MaxValue) - 0.0001;
                                            } else if (thislanterValue <= parseFloat(minMax.MinValue)) {
                                                thislanterValue = parseFloat(minMax.MinValue) + 0.0001;
                                            }
                                            var lanternY = yScale(thislanterValue);

                                            if (thisSvgData[i].LineStyleForLanternValue !== "None") {
                                                thisValuePath.append('line')
                                                    //.attr('fill', 'stroke')
                                                    .attr('x1', thisX)
                                                    .attr('y1', thisY)
                                                    .attr('y2', lanternY - Number(defaultData.FontSize))
                                                    .attr('x2', thisX)
                                                    .attr("stroke-dasharray", () => {
                                                        if (minMax.LineStyleForLanternValue == "Solid") {
                                                            return "null";
                                                        } else if (minMax.LineStyleForLanternValue == "Dash") {
                                                            return "5,5";
                                                        } else if (minMax.LineStyleForLanternValue == "Dot") {
                                                            return "1, 5";
                                                        } else if (minMax.LineStyleForLanternValue == "DashDot") {
                                                            return "5,5,1,5";
                                                        } else if (minMax.LineStyleForLanternValue == "DashDotDot") {
                                                            return "5,5,1,5,1,5";
                                                        } else {
                                                            return "5,5";
                                                        }
                                                    })
                                                    .attr('stroke', () => {
                                                        //物理升温
                                                        if ((parseFloat(thisSvgData[i].LanternValue) > parseFloat(thisSvgData[i].Value))) {
                                                            if (minMax.LanternValueColorForUpValue) {
                                                                return minMax.LanternValueColorForUpValue;
                                                            } else {
                                                                return "rgba(0,0,255,0.6)";
                                                            }
                                                        } else {
                                                            if (minMax.LanternValueColorForDownValue) {
                                                                return minMax.LanternValueColorForDownValue;
                                                            } else {
                                                                return "rgba(255,0,0,0.6)";
                                                            }
                                                        }
                                                    });
                                            }


                                            thisValuePath.append('text')
                                                .attr('style', `font-family:${(defaultData.FontName || '宋体')};font-weight:${thisSvgData[i].TextFontWeight ? thisSvgData[i].TextFontWeight : 'normal'};font-size:${thisSvgData[i].CharLantern == "R" ? 14 : (thisSvgData[i] && thisSvgData[i].TextFontSize ? thisSvgData[i].TextFontSize : defaultData.FontSize)}pt;text-anchor:middle;`)
                                                .attr('fill', () => {
                                                    if (thisSvgData[i].CharLanternColor) {
                                                        return thisSvgData[i].CharLanternColor;
                                                    }
                                                    //物理升温
                                                    if ((parseFloat(thisSvgData[i].LanternValue) > parseFloat(thisSvgData[i].Value))) {
                                                        if (minMax.LanternValueColorForUpValue) {
                                                            return minMax.LanternValueColorForUpValue;
                                                        } else {
                                                            return "rgba(0,0,255,0.6)";
                                                        }
                                                    } else {
                                                        if (minMax.LanternValueColorForDownValue) {
                                                            return minMax.LanternValueColorForDownValue;
                                                        } else {
                                                            return "rgba(255,0,0,0.6)";
                                                        }
                                                    }
                                                })
                                                .attr('x', thisX)
                                                .attr('y', () => {
                                                    if (thisSvgData[i].LanternValue) {
                                                        if (parseFloat(thisSvgData[i].LanternValue) > parseFloat(thisSvgData[i].Value)) {
                                                            return lanternY - defaultData.FontSize;
                                                        } else {
                                                            return lanternY + Number(defaultData.FontSize) / 2;
                                                        }
                                                    } else {
                                                        return lanternY + Number(defaultData.FontSize) / 2;
                                                    }
                                                })
                                                .text(() => {
                                                    if (thisSvgData[i].CharLantern == "R") {
                                                        return "®";
                                                    }
                                                    return thisSvgData[i].CharLantern;
                                                })
                                                .attr("title", () => {
                                                    var thisTitle = thisSvgData[i].Time + " " + minMax.Title + " " + thisSvgData[i].LanternValue;
                                                    thisTitle = thisSvgData[i].Title ? thisSvgData[i].Title : thisTitle;
                                                    return thisTitle;
                                                });
                                        }
                                        return thisY;
                                    },
                                    fill: symbolColorValue,
                                    stroke: minMax.SymbolColorValue ? minMax.SymbolColorValue : "#ff0000",
                                    r: symbolSize,
                                    title: ((i) => {
                                        //[DUWRITER5_0-3404]20240826 lxy 自定义画点Title
                                        var titleText = thisSvgData[i].Time + " " + minMax.Title + " " + thisSvgData[i].Value + `${thisSvgData[i].LanternValue > 0 ? ((parseFloat(thisSvgData[i].LanternValue) > parseFloat(thisSvgData[i].Value)) ? ' ⬆ ' : ' ⬇ ') + thisSvgData[i].LanternValue : ''}`;
                                        if (thisSvgData[i].Title && thisSvgData[i].Title.length > 0) {
                                            titleText = thisSvgData[i].Title;
                                        }
                                        return titleText;
                                    }),
                                    sign: false
                                };
                                ////console.log(symbolStyle, thisSvgData[z].SpecifySymbolStyle)
                                var oldSymbolStyle = null;
                                //判断是否存在单独的样式
                                if (thisSvgData[z].SpecifySymbolStyle && thisSvgData[z].SpecifySymbolStyle != "Default") {
                                    oldSymbolStyle = symbolStyle;
                                    symbolStyle = thisSvgData[z].SpecifySymbolStyle;
                                }

                                if (supportStyle.indexOf(symbolStyle) < 0) {
                                    symbolStyle = "SolidCicle";
                                }
                                if (symbolStyle == "HollowCicle") {
                                    symbolStyle = "SolidCicle";
                                    IconData.fill = "#ffffff";
                                }
                                if (pointValueArr.indexOf(z) >= 0) {
                                    symbolStyle = "SolidCicle";
                                    IconData.fill = "transparent";
                                    IconData.r = symbolSize * 1.5;
                                }
                                //设置图标位置，方便阴影绘制

                                WriterControl_DrawFu.IconDrawObj()[symbolStyle] && WriterControl_DrawFu.IconDrawObj()[symbolStyle](IconData);
                                ////console.log(oldSymbolStyle);
                                //重置样式
                                if (oldSymbolStyle) {
                                    symbolStyle = oldSymbolStyle;
                                }
                                //if (symbolStyle != "SolidCicle") {
                                IconData.r = 4;
                                IconData.fill = "transparent";
                                IconData.stroke = "transparent";
                                IconData.sign = true;
                                //绘制第二个小点用于处理
                                WriterControl_DrawFu.IconDrawObj().SolidCicle(IconData, thisSvgData, yScale);
                            }


                            //}
                        });
                    }

                }

                // }
            }
        };



        //绘制阴影区域
        //获取日期区域的数组
        var dateBand = [];
        thisBand.forEach((data, index) => {
            if ((index + 1) % 6 == 0) {
                dateBand.push((index + 1) * calculationData.VerticalStepWidth);
            }
        });
        //获取到所有的坐标
        if (calculationData.ShadowNameArr.List) {
            for (var z in calculationData.ShadowNameArr.List) {
                //如果存在不显示阴影直接进入下一个
                if (calculationData.ShadowPointVisible.indexOf(z) >= 0) {
                    continue;
                }
                var thisValue = calculationData.ShadowNameArr.List[z];
                var allValueG = svg.node().querySelector("g[dctype=dcValuePathG]").querySelectorAll(`g[name]`);
                var nameG = null, valueG = null;

                for (var y = 0; y < allValueG.length; y++) {
                    if (allValueG[y].getAttribute("name") == z) {
                        nameG = allValueG[y];
                    }
                    if (allValueG[y].getAttribute("name") == thisValue) {
                        valueG = allValueG[y];
                    }
                }

                if (nameG && valueG) {
                    nameG = nameG.querySelectorAll("g[isshow=true],g[isshow=false]");
                    valueG = valueG.querySelectorAll('g[isshow=true],g[isshow=false]');

                    if (nameG && valueG) {
                        var nameCoordinate = [];
                        var valueCoordinate = [];
                        for (var y = 0; y < nameG.length; y++) {
                            if (!nameG[y]) {
                                continue;
                            }
                            var nameCircle = nameG[y].querySelectorAll('[dcSymbolX]');
                            nameCircle = Array.from(nameCircle);
                            nameCircle.forEach((d) => {
                                nameCoordinate.push({ x: d.getAttribute("dcSymbolX"), y: d.getAttribute("dcSymbolY") });
                            });
                        }

                        for (var y = 0; y < valueG.length; y++) {
                            if (!valueG[y]) {
                                continue;
                            }
                            var valueCircle = valueG[y].querySelectorAll('[dcSymbolX]');
                            valueCircle = Array.from(valueCircle);
                            valueCircle.forEach((d) => {
                                valueCoordinate.push({ x: d.getAttribute("dcSymbolX"), y: d.getAttribute("dcSymbolY") });
                            });
                        }


                        if (nameCoordinate.length > 0 && valueCoordinate.length > 0) {
                            nameCoordinate = nameCoordinate.sort((a, b) => {
                                return parseFloat(a.x) - parseFloat(b.x);
                            });
                            valueCoordinate = valueCoordinate.sort((a, b) => {
                                return parseFloat(a.x) - parseFloat(b.x);
                            });

                            var nameX = nameCoordinate[nameCoordinate.length - 1].x;
                            var valueX = valueCoordinate[valueCoordinate.length - 1].x;
                            var lastX = dateBand.find(d => d > ((nameX > valueX) ? valueX : nameX));
                            var firstX = nameCoordinate.length < valueCoordinate.length ? parseFloat(nameCoordinate[0].x) : parseFloat(valueCoordinate[0].x);
                            firstX = parseFloat(firstX);


                            valueCoordinate = valueCoordinate.reverse();

                            var dcAreaG = svg
                                .append('g')
                                .attr('dctype', "dcAreaG")
                                .attr('name', z)
                                .attr(
                                    'transform',
                                    `translate(${calculationData.LeftMargin + calculationData.FirstTextWidth},${calculationData.TopMargin + calculationData.SpecifyTitleHeight + calculationData.HeaderLinesTotalHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length)})`
                                );
                            svgEle[i].dcAreaG = dcAreaG;


                            var newCoordinateArr = Array.from(new Set([...nameCoordinate, ...valueCoordinate]));

                            // 去掉小于firstX和大于lastX的坐标
                            var CoordinateArr = [];
                            newCoordinateArr.forEach(d => {
                                if (d.x >= firstX && d.x <= lastX) {
                                    CoordinateArr.push(d);
                                }
                            });
                            var area = d3.area()
                                .x((d) => d.x)
                                .y1((d) => d.y)
                                .y0(CoordinateArr[0].y);


                            // if (minMax.ShadowStyle) {
                            var shadowStyle = "leftslant";
                            if (minMax && minMax.ShadowStyle) {
                                shadowStyle = minMax.ShadowStyle.trim().toLowerCase();
                            }
                            switch (shadowStyle) {
                                case "leftslant":
                                    // 在SVG中定义斜线图案-左倾斜的线
                                    svg.append("defs")
                                        .append("pattern")
                                        .attr("id", "diagonalHatch")
                                        .attr("patternUnits", "userSpaceOnUse")
                                        .attr("width", 4) // 调整斜线图案的宽度
                                        .attr("height", 4) // 调整斜线图案的高度
                                        .append("path")
                                        .attr("d", "M-1,1 l2,-2 M0,4 l4,-4 M3,5 l2,-2") // 增加斜线数量，使斜线更密集
                                        .style("stroke", "red") // 更深的红色
                                        .style("stroke-width", 1.5); // 增加斜线的宽度

                                    // 创建一个g元素，并在其中添加路径元素
                                    dcAreaG.append("g")
                                        .append("path")
                                        .attr("fill", "url(#diagonalHatch)") // 引用斜线图案
                                        .attr('style', `fill-opacity: 0.4`) // 增加填充的透明度，使颜色更饱满
                                        .datum(CoordinateArr)
                                        .attr("d", area);
                                    break;
                                case "rightslant":
                                    // 在SVG中定义斜线图案-右倾斜的线
                                    svg.append("defs")
                                        .append("pattern")
                                        .attr("id", "diagonalHatchRight")
                                        .attr("patternUnits", "userSpaceOnUse")
                                        .attr("width", 4)
                                        .attr("height", 4)
                                        .append("path")
                                        .attr("d", "M0,0 l4,4 M4,4 l8,8 M-1,3 l4,4")
                                        .style("stroke", "red")
                                        .style("stroke-width", 1.5);


                                    // 创建一个g元素，并在其中添加路径元素
                                    dcAreaG.append("g")
                                        .append("path")
                                        .attr("fill", "url(#diagonalHatchRight)")
                                        .attr('style', `fill-opacity: 0.4`)
                                        .datum(CoordinateArr)
                                        .attr("d", area);
                                    break;
                                default:
                                    //背景色
                                    dcAreaG.append("g")
                                        .append("path")
                                        .attr("fill", "red")
                                        .attr('style', `fill-opacity: 0.2`)
                                        .datum(CoordinateArr)
                                        .attr("d", area);
                                    break;

                            }
                        }
                    }
                }
            }
        }



        //[DUWRITER5_0-4098] 20250109 lxy 绘制点与点之间的连接垂直线
        var dNodeSymbolPointArr = [];//当前数据点的坐标
        var targetNodeSymbolPointArr = [];//目标数据点的坐标
        //获取数据点的坐标
        var yAxisInfos = defaultData.YAxisInfos || [];
        yAxisInfos.forEach(d => {
            // 如果开起来绘制垂直线
            if (d.VerticalLine === 'true' || d.VerticalLine === true) {
                var SymbolColorValue = d.SymbolColorValue ? d.SymbolColorValue : "#ff0000";
                var dNode = dcValuePathG.node().querySelector(`[dctype=dcValuePath_${d.Name}]`) || null;
                var targetNode = dcValuePathG.node().querySelector(`[dctype=dcValuePath_${d.ShadowName}]`) || null;

                if (dNode && targetNode) {
                    // 当前图标点的坐标
                    var dNodeSymbolArr = dNode.querySelectorAll('[dcSymbolX]') || [];
                    if (dNodeSymbolArr.length > 0) {
                        dNodeSymbolArr.forEach(dSymbol => {
                            var dOption = {
                                x: dSymbol.getAttribute('dcSymbolX'),
                                y: dSymbol.getAttribute('dcSymbolY'),
                                SymbolColorValue
                            };
                            //防止重复添加
                            var index = dNodeSymbolPointArr.findIndex(d => d.x === dOption.x && d.y === dOption.y);
                            if (index < 0) {
                                dNodeSymbolPointArr.push(dOption);
                            }
                        });
                    }

                    //目标图标点的坐标
                    var targetNodeSymbolArr = targetNode.querySelectorAll('[dcSymbolX]') || [];
                    if (targetNodeSymbolArr.length > 0) {
                        targetNodeSymbolArr.forEach(targetSymbol => {
                            var targetOption = {
                                x: targetSymbol.getAttribute('dcSymbolX'),
                                y: targetSymbol.getAttribute('dcSymbolY'),
                                SymbolColorValue
                            };
                            //防止重复添加
                            var index = targetNodeSymbolPointArr.findIndex(d => d.x === targetOption.x && d.y === targetOption.y);
                            if (index < 0) {
                                targetNodeSymbolPointArr.push(targetOption);
                            }

                        });
                    }
                }
            }

        });

        //当前数据点和目标数据点
        if (dNodeSymbolPointArr.length > 0 && targetNodeSymbolPointArr.length > 0) {
            var dcValueVerticalLineG = dcValuePathG.append('g')
                .attr('dctype', 'dcValueVerticalLineG');
            for (var j = 0; j < dNodeSymbolPointArr.length; j++) {
                var dNodeSymbolPoint = dNodeSymbolPointArr[j];
                for (var k = 0; k < targetNodeSymbolPointArr.length; k++) {
                    var targetNodeSymbolPoint = targetNodeSymbolPointArr[k];
                    if (dNodeSymbolPoint.x === targetNodeSymbolPoint.x) {
                        // 绘制垂直线
                        dcValueVerticalLineG.append('line')
                            .attr('class', 'dcValueVerticalLine')
                            .attr('x1', dNodeSymbolPoint.x)
                            .attr('y1', dNodeSymbolPoint.y)
                            .attr('x2', targetNodeSymbolPoint.x)
                            .attr('y2', targetNodeSymbolPoint.y)
                            .attr('stroke', dNodeSymbolPoint.SymbolColorValue)
                            .attr('stroke-width', 1);

                    }
                }
            }
        }

    },

    //绘制标尺
    ReturnScale: function (min, max, range) {
        return d3.scaleLinear(
            [min, max],
            range
        );
    },

    // 绘制一个线条
    GetDrawPath: function ({ content, line, stroke = 'blue', width = 1 }) {
        content
            .append('path')
            .attr('class', 'mylines')
            .attr('fill', 'none')
            .attr('stroke', stroke)
            .attr('stroke-width', width)
            .attr('stroke-linejoin', "round")
            .attr('stroke-linecap', "round")
            .attr('d', line);
    },

    //绘制图形
    IconDrawObj: function () {
        return {
            // 绘制圆形图标
            SolidCicle: ({
                content,
                data,
                x,
                y,
                fill = 'blue',
                stroke = 'blue',
                r = 4,
                title,
            }) => { //是否为标记物
                // 增加icon  红色空心
                content
                    .append('g')
                    .attr('fill', fill)
                    .attr('stroke', stroke)
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('circle')
                    .data(data)
                    .join('circle')
                    .attr('transform', (i, d) => {
                        var yVal = y(i) || 0;
                        if (!yVal) {
                            return 'scale(0)';
                        }
                        return '';
                    })
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y)
                    .attr('r', i => {
                        if (y(i)) {
                            return r;
                        }
                        return 0;
                    })
                    .attr('title', (i) => {
                        //[DUWRITER5_0-3404]20240826 lxy 自定义画点Title
                        if (typeof title === 'function') {
                            title = title(i);
                        }
                        return title;
                    });
                // icon.node().setAttribute('dcSymbolX', x);
                // icon.node().setAttribute('dcSymbolY', y);
            },
            // 绘制x
            Cross: ({
                content,
                data,
                x,
                y,
                fill = 'blue',
                stroke = 'blue',
                r,
                title
            }) => {
                content
                    .append('g')
                    .attr('fill', fill)
                    .attr('stroke', stroke)
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('line')
                    .data(data)
                    .join('line')
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y)
                    .attr('transform', i => {
                        let yVal = y;
                        if (typeof y === 'function') {
                            yVal = y(i) || 0;
                        }
                        if (!yVal) {
                            return 'scale(0)';
                        }
                        return '';
                    })
                    .attr('x1', function (d) {
                        return x(d) - r;
                    })
                    .attr('y1', function (d) {
                        return y(d) - r;
                    })
                    .attr('x2', function (d) {
                        return x(d) + r;
                    })
                    .attr('y2', function (d) {
                        return y(d) + r;
                    })
                    .clone()
                    .attr('x1', function (d) {
                        return x(d) + r;
                    })
                    .attr('y1', function (d) {
                        return y(d) - r;
                    })
                    .attr('x2', function (d) {
                        return x(d) - r;
                    })
                    .attr('y2', function (d) {
                        return y(d) + r;
                    })
                    .attr('title', title);

            },
            // ·绘制圆形点图标
            RoundDotIcon: ({
                content,
                data,
                x,
                y,
                fill = 'white',
                stroke = 'blue',
                r = 4,
                title
            }) => {
                content
                    .append('g')
                    .attr('fill', fill)
                    .attr('stroke', stroke)
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('circle')
                    .data(data)
                    .join('circle')
                    .attr('transform', i => {
                        const yVal = y(i) || 0;
                        if (!yVal) {
                            return 'scale(0)';
                        }
                        return '';
                    })
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('r', r)
                    .clone()
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('r', 1)
                    .attr('fill', stroke)
                    .attr('title', title)
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);

            },
            // 绘制三角形
            SolidTriangle: ({
                content,
                data,
                x,
                y,
                fill = 'blue',
                stroke = 'blue',
                riangle = 20,
                title
            }) => {
                // 蓝色三角形
                content
                    .append('g')
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('g')
                    .data(data)
                    .join('g')
                    .attr('transform', i => {
                        const yVal = y(i) || 0;
                        if (!yVal) {
                            return 'scale(0)';
                        }
                        return `translate(${x(i)},${yVal})`;
                    })
                    .append('path')
                    .call(path => {
                        const symbolThree = d3.symbol();
                        const symbolIndex = 5;
                        symbolThree.type(d3.symbols[symbolIndex]);
                        path
                            .attr('d', symbolThree.size(riangle))
                            .attr('fill', fill)
                            .attr('stroke', stroke)
                            .transition();
                        // .duration(1500)
                        // .attr('d', symbolThree.size(48))
                    })
                    .attr('title', title)
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);

            },
            // 空心正三角形
            HollowSolidTriangle: ({
                content,
                data,
                x,
                y,
                fill = 'blue',
                stroke = 'blue',
                riangle = 28,
                title
            }) => {
                // 蓝色三角形
                content
                    .append('g')
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('g')
                    .data(data)
                    .join('g')
                    .attr('transform', i => {
                        const yVal = y(i) || 0;
                        if (!yVal) {
                            return 'scale(0)';
                        }
                        return `translate(${x(i)},${yVal})`;
                    })
                    .append('path')
                    .call(path => {
                        const symbolThree = d3.symbol();
                        const symbolIndex = 5;
                        symbolThree.type(d3.symbols[symbolIndex]);
                        path
                            .attr('d', symbolThree.size(riangle))
                            .attr('fill', "white")
                            .attr('stroke', stroke)
                            .transition();
                        // .duration(1500)
                        // .attr('d', symbolThree.size(48))
                    })
                    .attr('title', title)
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);

            },
            // 绘制倒三角
            SolidTriangleReversed: ({
                content,
                data,
                x,
                y,
                fill = 'blue',
                stroke = 'blue',
                riangle = 28,
                title
            }) => {
                // 蓝色三角形
                content
                    .append('g')
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('g')
                    .data(data)
                    .join('g')
                    .attr('transform', i => {
                        const yVal = y(i) || 0;
                        if (!yVal) {
                            return 'scale(0)';
                        }
                        return `translate(${x(i)},${yVal})`;
                    })
                    .append('path')
                    .call(path => {
                        const symbolThree = d3.symbol();
                        const symbolIndex = 5;
                        symbolThree.type(d3.symbols[symbolIndex]);
                        path
                            .attr('d', symbolThree.size(riangle))
                            .attr('fill', fill)
                            .attr('stroke', stroke)
                            .transition();
                        // .duration(1500)
                        // .attr('d', symbolThree.size(48))
                    })
                    .attr("style", "transform: rotate(0.5turn);")
                    .attr('title', title)
                    .attr("isShow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);
            },
            // ·空心倒三角
            HollowSolidTriangleReversed: ({
                content,
                data,
                x,
                y,
                fill = 'blue',
                stroke = 'blue',
                riangle = 28,
                title
            }) => {
                // 蓝色三角形
                content
                    .append('g')
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('g')
                    .data(data)
                    .join('g')
                    .attr('transform', i => {
                        const yVal = y(i) || 0;
                        if (!yVal) {
                            return 'scale(0)';
                        }
                        return `translate(${x(i)},${yVal})`;
                    })
                    .append('path')
                    .call(path => {
                        const symbolThree = d3.symbol();
                        const symbolIndex = 5;
                        symbolThree.type(d3.symbols[symbolIndex]);
                        path
                            .attr('d', symbolThree.size(riangle))
                            .attr('fill', "white")
                            .attr('stroke', stroke)
                            .transition();
                        // .duration(1500)
                        // .attr('d', symbolThree.size(48))
                    })
                    .attr("style", "transform: rotate(0.5turn);")
                    .attr('title', title)
                    .attr("isShow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);
            },
            // 绘制方形
            Square: ({
                content,
                data,
                x,
                y,
                fill = 'blue',
                stroke = 'blue',
                r = 4,
                title,
            }) => { //是否为标记物
                // 增加icon  红色空心
                content
                    .append('g')
                    .attr('fill', fill)
                    .attr('stroke', stroke)
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('rect')
                    .data(data)
                    .join('rect')
                    .attr('transform', (i, d) => {
                        var yVal = y(i) || 0;
                        if (!yVal) {
                            return 'scale(0)';
                        }
                        return '';
                    })
                    .attr('x', (i, d) => {
                        return x(i) - (r);
                    })
                    .attr('y', (i, d) => {
                        return y(i) - (r);
                    })
                    .attr('width', r * 2)
                    .attr('height', r * 2)
                    .attr('title', title)
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);

            },
            //·空心圆
            HollowCicle: ({
                content,
                data,
                x,
                y,
                fill = 'green',
                stroke = 'green',
                r = 4,
                title,
            }) => {
                content
                    .append('g')
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('circle')
                    .data(data)
                    .join('circle')
                    .attr('stroke', "blue")
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('fill', "white")
                    .attr('stroke', stroke)
                    .attr('r', r)
                    .attr('title', (i) => {
                        if (typeof title === 'function') {
                            title = title(i);
                        }
                        return title;
                    })
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);


            },
            //·空心带圆点
            HollowWithCircularDots: ({
                content,
                data,
                x,
                y,
                fill = 'green',
                stroke = 'green',
                r = 5,
                title,
            }) => {
                content
                    .append('g')
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('circle')
                    .data(data)
                    .join('circle')
                    .attr('stroke', "blue")
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('fill', "white")
                    .attr('stroke', stroke)
                    .attr('r', r)
                    .attr('title', (i) => {
                        if (typeof title === 'function') {
                            title = title(i);
                        }
                        return title;
                    });
                content
                    .append('g')
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('circle')
                    .data(data)
                    .join('circle')
                    .attr('stroke', "blue")
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('fill', stroke)
                    .attr('stroke', stroke)
                    .attr('r', 5 / 2)
                    .attr('title', (i) => {
                        if (typeof title === 'function') {
                            title = title(i);
                        }
                        return title;
                    })
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);

            },
            //· 双层同心圆
            DoubleConcentricCircles: ({
                content,
                data,
                x,
                y,
                fill = 'green',
                stroke = 'green',
                r = 5,
                title,
            }) => {
                content
                    .append('g')
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('circle')
                    .data(data)
                    .join('circle')
                    .attr('stroke', "blue")
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('fill', "white")
                    .attr('stroke', stroke)
                    .attr('r', r)
                    .attr('title', (i) => {
                        if (typeof title === 'function') {
                            title = title(i);
                        }
                        return title;
                    });
                content
                    .append('g')
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('circle')
                    .data(data)
                    .join('circle')
                    .attr('stroke', "blue")
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('fill', "white")
                    .attr('stroke', stroke)
                    .attr('r', 3)
                    .attr('title', (i) => {
                        if (typeof title === 'function') {
                            title = title(i);
                        }
                        return title;
                    })
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);

            },
            //H形
            H: ({
                content,
                data,
                x,
                y,
                fill = 'green',
                stroke = 'green',
                r = 5,
                title,
            }) => {
                content
                    .append('g')
                    .attr('stroke-width', 1)
                    .attr("isshow", () => {
                        if (fill == "transparent") {
                            return "false";
                        } else {
                            return "true";
                        }
                    })
                    .selectAll('path')
                    .data(data)
                    .join('path')
                    .attr('stroke', "blue")
                    .attr('d', (i, d) => {
                        r = 12;
                        // 假设 r 控制着H的宽度和间距，y(i)决定H的顶部基线
                        var topY = y(i) - (r / 2) || 0; // H的上部y坐标
                        var middleY = y(i) || 0; // H的中部y坐标
                        var bottomY = y(i) + (r / 2) || 0; // H的底部y坐标

                        // H的宽度，这里假设x(i)是H左侧的x坐标
                        var hWidth = r / 1.5; // H的宽度

                        // 构建H的路径
                        var path =
                            "M" + x(i) + " " + topY + " L" + x(i) + " " + bottomY + // 左侧垂直线
                            "M" + (x(i) - (hWidth / 2)) + " " + topY + " L" + (x(i) - (hWidth / 2)) + " " + bottomY + // 右侧垂直线
                            "M" + x(i) + " " + middleY + " L" + (x(i) - (hWidth / 2)) + " " + middleY; // 水平线

                        return path;
                    })
                    .attr('fill', "white")
                    .attr('stroke', stroke)
                    .attr('title', (i) => {
                        if (typeof title === 'function') {
                            title = title(i);
                            return title;
                        }
                    })
                    .attr('dcSymbolX', x)
                    .attr('dcSymbolY', y);
            },
        };

        // 未兼容的cs中使用的图标
        // HollowSquare 空心正方形
        // Diamond 菱形
        // HollowDiamond 空心菱形
        // V
        // VReversed 倒V
        // Character R
        // CharacterCircle R带圈
        // CrossCircle 狙击点

    },

    //计算所有的元素详细位置
    ComputeAllUIDPosition: function (rootElement, firstSvg) {
        if (!rootElement || !firstSvg) {
            return;
        }
        var calculationData = rootElement.DocumentOptions.CalculationData;
        firstSvg = firstSvg.page.node();
        var allUIDEle = firstSvg.querySelectorAll("g[uid]");
        //清空数组
        calculationData.UIDElePositon = [];
        var pagePosition = rootElement.pageContainer.getBoundingClientRect();
        var svgPosiiton = firstSvg.getBoundingClientRect();
        for (var i = 0; i < allUIDEle.length; i++) {
            var uid = allUIDEle[i].getAttribute('uid');
            var allItem = ["Labels", "HeaderLabels", "HeaderLines", "FooterLines", "YAxisInfos"];
            //解析uid
            var idArr = uid.split("_");
            if (Array.isArray(idArr)) {
                idArr[0] = idArr[0].substring(2);
            }
            if (idArr[0] && typeof idArr[0] == "string" && allItem.indexOf(idArr[0]) >= 0) {
                var thisG = rootElement.querySelector(`[uid=${uid}]`);
                if (!thisG) {
                    return;
                }
                if (idArr[0] == "Labels" || idArr[0] == "HeaderLabels") {
                    //直接获取到对应的g元素
                    var gPosition = thisG.getBoundingClientRect();
                    calculationData.UIDElePositon.push({
                        UID: uid,
                        Positon: {
                            width: gPosition.width,
                            height: gPosition.height * 1.1,
                            containerTop: (gPosition.y - pagePosition.y + rootElement.pageContainer.scrollTop),
                            containerLeft: (gPosition.x - pagePosition.x),
                            svgTop: gPosition.y - svgPosiiton.y,
                            svgLeft: gPosition.x - svgPosiiton.x,
                        },
                        Type: idArr[0]
                    });
                } else if (idArr[0] == "HeaderLines" || idArr[0] == "FooterLines") {
                    var index = parseInt(idArr[1]);
                    var parentEle = thisG.parentElement;
                    if (idArr[0] == "HeaderLines") {
                        //找到包裹线的位置
                        var horizontalLine = parentEle.querySelector("[dctype=dcHeaderLinesHorizontalG]").querySelectorAll("line");
                    } else {
                        //找到包裹线的位置
                        var horizontalLine = parentEle.querySelector("[dctype=dcFooterLinesHorizontalG]").querySelectorAll("line");
                    }
                    var topLine = horizontalLine[index];
                    var bottomLine = horizontalLine[index + 1];
                    var topLinePosition = topLine.getBoundingClientRect();
                    var bottomLinePosition = bottomLine.getBoundingClientRect();
                    calculationData.UIDElePositon.push({
                        UID: uid,
                        Positon: {
                            width: topLinePosition.width,
                            height: bottomLinePosition.y - topLinePosition.y,
                            containerTop: topLinePosition.y - pagePosition.y + rootElement.pageContainer.scrollTop,
                            containerLeft: topLinePosition.x - pagePosition.x,
                            svgTop: topLinePosition.y - svgPosiiton.y,
                            svgLeft: topLinePosition.x - svgPosiiton.x,
                        },
                        Type: idArr[0]
                    });

                } else if (idArr[0] == "YAxisInfos") {
                    var index = parseInt(idArr[1]);
                    var parentEle = thisG.parentElement;

                    var bottomLine = thisG.querySelector("[dctype=dcYAxiosInfos_Pos]");
                    if (!bottomLine) {
                        continue;
                    }
                    var bottomLine = bottomLine.getBoundingClientRect();
                    calculationData.UIDElePositon.push({
                        UID: uid,
                        Positon: {
                            width: calculationData.YAxisInfosSingleWidth[index] * calculationData.ZoomRate,
                            height: bottomLine.height,
                            containerTop: bottomLine.y - pagePosition.y + rootElement.pageContainer.scrollTop,
                            containerLeft: (bottomLine.x - pagePosition.x) - (calculationData.YAxisInfosSingleWidth[index] * calculationData.ZoomRate),
                            svgTop: bottomLine.y - svgPosiiton.y,
                            svgLeft: bottomLine.x - calculationData.YAxisInfosSingleWidth[index] - svgPosiiton.x,
                        },
                        Type: idArr[0]
                    });
                }
            }
        }
    },

    //移动边框元素到目标元素上
    MoveBorderRect: function (rootElement, uid) {
        // //console.log("111111111111111")
        if (!rootElement) {
            return;
        }
        var calculationData = rootElement.DocumentOptions.CalculationData;
        var borderSpan = rootElement.querySelector("#dc_borderSpan");
        if (borderSpan == null) {
            borderSpan = document.createElement("div");
            borderSpan.id = "dc_borderSpan";
            //borderSpan.style.display = "none";
            borderSpan.style.position = "absolute";
            borderSpan.style.boxShadow = "inset 0px 0px 15px rgba(0, 255, 255, 0.5)";
            borderSpan.style.pointerEvents = "none";
            rootElement.pageContainer.appendChild(borderSpan);
        }
        //判断是否存在大小改变
        // borderSpan.style.zoom = calculationData.ZoomRate;
        borderSpan.style.display = "block";
        //进行处理
        rootElement.dc_MoveBorderRect = true;
        setTimeout(() => {
            rootElement.dc_MoveBorderRect = false;
        }, 500);
        var firstSvg = rootElement.querySelector('[dctype=page]');
        var pagePosition = rootElement.pageContainer.getBoundingClientRect();
        var svgPosition = firstSvg.getBoundingClientRect();
        //如果uid不存在选中最外层
        if (!uid) {
            var width = svgPosition.width;
            var height = svgPosition.height;
            borderSpan.style.width = width + 'px';
            borderSpan.style.height = height + 'px';
            borderSpan.style.top = (svgPosition.y) - pagePosition.y + rootElement.pageContainer.scrollTop + "px";
            borderSpan.style.left = (svgPosition.x) - pagePosition.x + rootElement.pageContainer.scrollLeft + "px";
        } else {
            if (calculationData.UIDElePositon && calculationData.UIDElePositon.length > 0) {
                calculationData.UIDElePositon.find((item, index) => {
                    if (item.UID == uid) {
                        borderSpan.style.width = item.Positon.width + 'px';
                        borderSpan.style.height = item.Positon.height + 'px';
                        borderSpan.style.top = item.Positon.containerTop + "px";
                        borderSpan.style.left = item.Positon.containerLeft + "px";

                        // borderSpan.style.top = svgPosition.y + (item.Positon.svgTop) + rootElement.pageContainer.scrollTop - pagePosition.y + 'px';

                        // borderSpan.style.left = svgPosition.x + (item.Positon.svgLeft) - pagePosition.x + rootElement.pageContainer.scrollLeft + 'px';
                        return true;
                    }
                });
            }
        }
        //    var allItem = ["Label","HeaderLabel", "HeaderLine", "FooterLine", "YAxisInfo"];
        //    //解析uid
        //    var idArr = uid.split("_");
        //    if (Array.isArray(idArr)) {
        //        idArr[0] = idArr[0].substring(2);
        //    }
        //    if (idArr[0] && typeof idArr[0] == "string" && allItem.indexOf(idArr[0]) >= 0) {
        //        var thisG = rootElement.querySelector(`[uid=${uid}]`);
        //        if (!thisG) {
        //            return;
        //        }
        //        if (idArr[0] == "Label" || idArr[0] == "HeaderLabel") {
        //            //直接获取到对应的g元素
        //            var pagePosition = rootElement.pageContainer.getBoundingClientRect();
        //            var gPosition = thisG.getBoundingClientRect();
        //            var top = (gPosition.y - pagePosition.y + rootElement.pageContainer.scrollTop);
        //            var left = (gPosition.x - pagePosition.x);
        //            borderSpan.style.width = gPosition.width + 'px';
        //            borderSpan.style.height = gPosition.height * 1.1 + 'px';
        //            borderSpan.style.top = top + "px";
        //            borderSpan.style.left = left + "px";
        //        } else if (idArr[0] == "HeaderLine" || idArr[0] == "FooterLine") {
        //            var index = parseInt(idArr[1]);
        //            var parentEle = thisG.parentElement;
        //            if (idArr[0] == "HeaderLine") {
        //                //找到包裹线的位置
        //                var horizontalLine = parentEle.querySelector("[dctype=dcHeaderLineHorizontalG]").querySelectorAll("line");
        //            } else {
        //                //找到包裹线的位置
        //                var horizontalLine = parentEle.querySelector("[dctype=dcFooterLineHorizontalG]").querySelectorAll("line");
        //            }
        //            var topLine = horizontalLine[index];
        //            var bottomLine = horizontalLine[index + 1];
        //            var pagePosition = rootElement.pageContainer.getBoundingClientRect();
        //            var topLinePosition = topLine.getBoundingClientRect();
        //            var bottomLinePosition = bottomLine.getBoundingClientRect();
        //            var width = topLinePosition.width;
        //            var height = bottomLinePosition.y - topLinePosition.y;
        //            var top = topLinePosition.y - pagePosition.y + rootElement.pageContainer.scrollTop;
        //            var left = topLinePosition.x - pagePosition.x;
        //            borderSpan.style.width = width + 'px';
        //            borderSpan.style.height = height + 'px';
        //            borderSpan.style.top = top + "px";
        //            borderSpan.style.left = left + "px";
        //        } else if (idArr[0] == "YAxisInfo") {
        //            var index = parseInt(idArr[1]);
        //            var parentEle = thisG.parentElement;
        //            var bottomLine = thisG.querySelector("[dctype=dcYAxiosInfos_Pos]");
        //            var pagePosition = rootElement.pageContainer.getBoundingClientRect();
        //            var bottomLine = bottomLine.getBoundingClientRect();
        //            var width = calculationData.YAxisInfosSingleWidth[index];
        //            var height = bottomLine.height;
        //            var top = bottomLine.y - pagePosition.y + rootElement.pageContainer.scrollTop;
        //            var left = bottomLine.x - width - pagePosition.x;
        //            borderSpan.style.width = width + 'px';
        //            borderSpan.style.height = height + 'px';
        //            borderSpan.style.top = top + "px";
        //            borderSpan.style.left = left + "px";
        //        }
        //    }
        //}
    },
    //修改注册码标签的显示内容 [DUWRITER5_0-3574] 20240919 lxy
    ChangeAboutMessageText: function (rootElement, text) {
        if (rootElement.DocumentOptions.SVGElement && rootElement.DocumentOptions.SVGElement.length) {
            var SVGElement = rootElement.DocumentOptions.SVGElement;
            for (var i = 0; i < SVGElement.length; i++) {
                // 注册码标签
                var AboutMessageText = rootElement.querySelector("#dc_typesign" + i);

                //匹配注册码中的项目名
                const regex = /\[项目名:(.*?)\]/;
                const match = regex.exec(text);
                //新的注册码
                let newRegisCode = match && match.length ? match[1] : text;
                //当前正在展示的注册码
                let currentRegisterCode = AboutMessageText.textContent || '';
                //如果当前展示的注册码和新的注册码一致，则不进行修改
                if (currentRegisterCode.trim() === newRegisCode.trim()) {
                    return;
                }
                AboutMessageText.textContent = newRegisCode;
            }
        }

    }

};
// 表格绘制
export let WriterControl_DrawTable = {
    //表格列数据排列顺序的数组
    ColumnOrder: [],
    //初始化表格的壳子
    DrawTableInit: function (rootElement, BottomTableGroups = null) {
        var calculationData = WriterControl_DrawFu.DocumentOptions.CalculationData;

        if (!BottomTableGroups) {
            BottomTableGroups = calculationData.BottomTableGroups;
        }

        //整个盒子的高度
        var foreignObjectHeight = 0;
        for (var i in BottomTableGroups) {
            if (BottomTableGroups[i].tableHeight) {
                foreignObjectHeight += BottomTableGroups[i].tableHeight;
            }
            if (BottomTableGroups[i].keepHeight) {
                foreignObjectHeight += BottomTableGroups[i].keepHeight;
            }
        }

        //视图模式
        var viewMode = calculationData.GetViewMode ? calculationData.GetViewMode.toLowerCase().trim() : "page";
        //所有的svg页面
        var svgEle = WriterControl_DrawFu.DocumentOptions.SVGElement;

        //循环给每一页画表格
        for (var i = 0; i < svgEle.length; i++) {
            //删除已存在的svg表格，重新画（设置值用）
            if (svgEle[i] && svgEle[i]["dcTableG" + i]) {
                svgEle[i]["dcTableG" + i].node().remove();
            }

            //svg页面
            var svg = svgEle[i].page;

            //计算表格的位置
            var x = calculationData.LeftMargin;
            var y = calculationData.TopMargin + calculationData.SpecifyTitleHeight + (calculationData.LableHeight * calculationData.AllHeaderLabels.length) + calculationData.TableContainerHeight;
            var dcTableG = svg
                .append('g')
                .attr('dctype', "dcTableG")
                .attr(
                    'transform',
                    `translate(${x},${y})`
                );
            svgEle[i]["dcTableG" + i] = dcTableG;

            //用于存放表格dom的壳子
            var foreignObject = dcTableG.append('foreignObject')
                .attr('width', calculationData.InnerWidth)
                .attr('height', foreignObjectHeight);

            //创建表格
            //单页模式时只加载当前页的数据
            var pageindex = rootElement.getAttribute("pageindex");
            var tableHtml = WriterControl_DrawTable.GetSvgTableHtml(viewMode === "singlepage" ? pageindex - 1 : i);
            foreignObject.node().innerHTML = `<div id="dc_temperaturet_table_div${i}"  class="dc_temperaturet_table_div">${tableHtml}</div>`;


            //处理表格中文字小于12px时可能会不生效的情况
            try {
                var allFontSizeValidDom = foreignObject.node().querySelectorAll('span[dc-data-fontsize]') || null;
                allFontSizeValidDom && allFontSizeValidDom.length && allFontSizeValidDom.forEach(span => {
                    var targetFontSize = span.getAttribute('dc-data-fontsize');
                    if (targetFontSize && parseInt(targetFontSize) < 12) {
                        span.style.display = "inline-block";
                        const computedStyle = window.getComputedStyle(span);
                        var fontSize = computedStyle.getPropertyValue('font-size');
                        fontSize = parseFloat(fontSize);
                        if (targetFontSize < fontSize) {

                            const scale = targetFontSize / 12;

                            span.style.transform = `scale(${scale})`;
                        }
                    } else {
                        span.style.transform = `scale(1)`;
                    }
                });

            } catch (error) {
                console.warn(error);
            }

            //增加表格样式
            var styleElement = WriterControl_DrawTable.DragTableStyle();
            rootElement.append(styleElement);
        }
    },

    //获取svg表格html
    GetSvgTableHtml: function (pageIndex) {
        var calculationData = WriterControl_DrawFu.DocumentOptions.CalculationData;
        var BottomTableGroups = calculationData.BottomTableGroups;
        var tableHtml = '';//表格内容
        for (var i in BottomTableGroups) {

            var TableOptions = BottomTableGroups[i];
            var table = document.createElement('table');
            table.setAttribute('width', "100%");
            table.setAttribute('height', TableOptions.tableHeight + 'px');
            table.style.border = "2px solid #000";
            table.style.borderCollapse = "collapse";
            table.style.fontSize = "12px";
            table.style.padding = "0";
            table.style.boxSizing = "border-box";

            //表格间距
            if (TableOptions.keepHeight) {
                table.style.marginTop = TableOptions.keepHeight - 8 + 'px';
            }
            // 表格头
            var thead = WriterControl_DrawTable.GetRenderTableHeader(TableOptions.tableColumns, TableOptions);
            table.appendChild(thead);

            //表格体
            var tbody = WriterControl_DrawTable.GetRenderTableBody(TableOptions, pageIndex);
            table.appendChild(tbody);
            tableHtml += table.outerHTML;
        }
        return tableHtml;
    },

    //渲染表头
    GetRenderTableHeader: function (tableColumns, TableOptions) {
        //设置第一层的level为1
        tableColumns.map(item => item.rowLevel = 1);

        //数据扁平化
        var newJson = flatten(tableColumns);
        function flatten(arr, parent = null) {
            let result = [];
            for (let i = 0; i < arr.length; i++) {
                result.push(arr[i]);
                if (parent) {
                    arr[i].rowLevel = parent.rowLevel + 1;
                }
                if (arr[i].children && arr[i].children.length) {
                    result = result.concat(flatten(arr[i].children, arr[i]));
                }
            }
            return result;
        }

        //根据level获取总行数
        var maxRow = Math.max.apply(Math, newJson.map(i => i.rowLevel));
        if (TableOptions.tableHeight) {
            TableOptions['tableBodyHeight'] = TableOptions.tableHeight - (30 * maxRow);
        }

        //递归计算每个节点所有的子孙节点总数，用于合并单元格
        function countNodes(arr) {
            let count = 0;
            function countHelper(arr) {
                arr.forEach(item => {
                    if (item.children && Array.isArray(item.children)) {
                        countHelper(item.children); // 递归嵌套数组
                    } else {
                        //计算总的子节点数
                        count += 1;
                    }
                });
            }
            countHelper(arr); // 开始递归计数
            return count; // 返回总节点数
        }

        //需要展示值的列
        WriterControl_DrawTable.ColumnOrder = [];
        newJson.forEach(item => {
            if (item.children && item.children.length && item.headerFlag === true) {
                item['allChidrenLength'] = countNodes(item.children);
            } else {
                item['allChidrenLength'] = 0;
                WriterControl_DrawTable.ColumnOrder.push(item);
            }
        });



        //表格头
        var thead = document.createElement('thead');
        thead.style.borderBottom = "1px solid #000";
        thead.style.padding = "0";
        thead.style.boxSizing = "border-box";
        for (var i = 1; i <= maxRow; i++) {
            var tr = document.createElement('tr');//行
            tr.style.height = "30px";
            newJson.map(item => {
                if (item.rowLevel === i) {
                    var th = document.createElement('th');//列
                    th.style.border = "1px solid #ccc";
                    th.style.padding = "0";
                    th.style.boxSizing = "border-box";
                    th.style.width = item.width ? item.width + 'px' : null;
                    th.style.fontSize = item.fontsize ? item.fontsize + 'px' : "12px";
                    th.style.fontWeight = item.bold ? "900" : "100";
                    th.style.textAlign = item.align ? item.align : null;
                    th.style.fontFamily = item.fontname ? item.fontname : '宋体';
                    th.style.color = item.fontcolor ? item.fontcolor : null;

                    //增加span处理表格中文字小于12px时可能会不生效的情况
                    th.innerHTML = `<span style="display: inline-block;width: 100%;text-align: center;" dc-data-fontsize="${item.fontsize}">${item.label}</span>`;
                    if (item.children && item.children.length && item.headerFlag) {
                        if (item.allChidrenLength) {
                            th.colSpan = item.allChidrenLength;//合并列
                        }
                    } else {
                        th.rowSpan = maxRow - item.rowLevel + 1;//合并行
                    }
                    tr.append(th);
                }
            });
            thead.append(tr);
        }
        return thead;
    },
    //渲染表格体
    GetRenderTableBody: function (TableOptions, pageIndex) {
        var calculationData = WriterControl_DrawFu.DocumentOptions.CalculationData;
        var tableData = TableOptions.tableData || [];//表格数据
        //设置一页中的数据行数，默认继承体温单的长度
        var rowSpacing = calculationData.NumOfDaysInOnePage || 7;
        if (TableOptions.rowSpacing || TableOptions.rowSpacing === 0) {
            rowSpacing = TableOptions.rowSpacing;
        }
        if (!rowSpacing) {
            return;
        }

        var tableRowHeight = TableOptions.tableBodyHeight ? TableOptions.tableBodyHeight / rowSpacing : 30;
        //获取当前页的数据
        var currentPageTableData = tableData.slice(pageIndex * rowSpacing, (pageIndex + 1) * rowSpacing);




        //创建表格体
        var tbody = document.createElement('tbody');
        for (var i = 0; i < rowSpacing; i++) {
            var tr = document.createElement('tr');    //行
            tr.style.height = tableRowHeight + 'px';  //行高(暂定默认30px)
            //按列数据顺序渲染
            for (var j = 0; j < WriterControl_DrawTable.ColumnOrder.length; j++) {
                var prop = WriterControl_DrawTable.ColumnOrder[j].prop;
                var td = document.createElement('td');
                td.style.border = "1px solid #ccc";
                td.style.padding = "0";
                td.style.boxSizing = "border-box";
                td.style.fontFamily = "宋体";
                td.setAttribute('data-prop', prop);


                var itemFontSize = WriterControl_DrawTable.ColumnOrder[j].fontsize || null;
                if (currentPageTableData[i] && (currentPageTableData[i][prop] || currentPageTableData[i][prop] === 0)) {
                    //增加span处理表格中文字小于12px时可能会不生效的情况
                    td.innerHTML = `<span style="display: inline-block;width: 100%;text-align: center;" dc-data-fontsize="${itemFontSize || ''}">${currentPageTableData[i][prop]}</span>`;
                }
                tr.appendChild(td);
            }
            tbody.appendChild(tr);
        }
        return tbody;
    },
    //表格样式
    DragTableStyle: function () {
        if (document.getElementById("dc_temperaturet_table_style")) {
            return "";
        }
        var style = document.createElement('style');
        style.id = "dc_temperaturet_table_style";
        style.innerHTML = `
        .dc_NewbornTable_keepBox{
            width: 100%;
            border-bottom: 2px solid rgb(0, 0, 0);
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            -webkit-text-size-adjust: none;
        }
        .dc_temperaturet_table_div{
            width: 100%;
            height: 100%;
            overflow: hidden;
            background-color: transparent;
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            border-top:none;
        }
        .dc_temperaturet_table_div table {
            border-collapse: collapse;
            font-family: sans-serif;
            font-size: 9pt;
        }
        .dc_temperaturet_table_div  thead,
        .dc_temperaturet_table_div tfoot,
        .dc_temperaturet_table_div  tbody {
            background-color: transparent;
        }
        .dc_temperaturet_table_div thead tr th{
            padding:4px 0;
            box-sizing:border-box;
        }
        .dc_temperaturet_table_div tbody tr {
            width: 100%;
            height: 30px;
        }
        .dc_temperaturet_table_div th,
        .dc_temperaturet_table_div  td {
            border: 1px solid rgb(160 160 160);
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            font-weight: normal;
            font-family:宋体;
            font-size:12px;
        }

        .dc_temperaturet_table_div td:last-of-type {
            text-align: center;
        }

        .dc_temperaturet_table_div tfoot th {
            text-align: right;
        }

        .dc_temperaturet_table_div tfoot td {
            font-weight: bold;
        }
        `;
        return style;
    },
    //获取表格要用到的总页数
    GetTableTotalPageNumber: function () {
        var defaultData = WriterControl_DrawFu.DocumentOptions.DefaultData;
        var calculationData = WriterControl_DrawFu.DocumentOptions.CalculationData;
        var tableDataMaxPage = 0;
        if (defaultData.BottomTableGroups) {
            Object.keys(defaultData.BottomTableGroups).forEach(key => {
                if (key && defaultData.BottomTableGroups[key] && defaultData.BottomTableGroups[key].tableData) {
                    var dataLength = defaultData.BottomTableGroups[key].tableData.length;
                    var days = dataLength / calculationData.NumOfDaysInOnePage;
                    days = Math.ceil(days);
                    if (days > tableDataMaxPage) {
                        tableDataMaxPage = days;
                    }
                }
            });
        }
        return tableDataMaxPage;
    }
};