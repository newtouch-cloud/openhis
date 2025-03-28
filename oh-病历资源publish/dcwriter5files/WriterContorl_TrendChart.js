import { d3 } from "./WriterControl_DrawD3.js";

export let WriterControl_TrendChart = {
    //创建接口
    CreateTrendChart: function (rootElement, newOptions) {
        //初始化判断是否存在TrendChartSvg
        if (rootElement.TrendChartSvg == null) {
            rootElement.TrendChartSvg = {};
        }
        //初始化options
        WriterControl_TrendChart.TrendChartDefaultAttributes(rootElement, newOptions);
        //开始进行计算
        WriterControl_TrendChart.DataCalculationFun(rootElement);
        //绘制svg元素
        WriterControl_TrendChart.CreateSvgElement(rootElement);
        //绘制网格线
        WriterControl_TrendChart.DrawGridlines(rootElement);
        //绘制y轴
        WriterControl_TrendChart.DrawYAxis(rootElement);
        //绘制点
        WriterControl_TrendChart.DrawPoint(rootElement);
        //绘制文本标签
        WriterControl_TrendChart.DrawTextLabel(rootElement);
        return;
    },

    //定义默认元素
    TrendChartDefaultAttributes(rootElement,newValue) {
        //判断是否存在svg元素
        if (!rootElement.TrendChartSvg.Options) {
            rootElement.TrendChartSvg.Options = {
                Default: { //基础属性
                    width: 0, //宽度
                    height: 0, //长度
                    horizontalDivisionNum: 14, //横向分割数
                    leftYAxisSpace: 139, //左侧Y轴空白区域
                    rightYAxisSpace: 0, //右侧侧Y轴空白区域
                    chronologicalOrder: [8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,0,1,2,3,4,5,6,7], //x轴时间顺序
                    timeDivisionNum: 2, //垂直分割数
                    //date: "", //日期
                },
                YAxis: [  //Y轴属性(可以存在多个Y轴)
                    {
                        id: "ceshi111",  //唯一标记
                        legendColor: "blue", //图例颜色
                        legendSize: "",  //图例大小
                        legendStyle: "", //图例样式
                        title: "", //标题
                        titleColor: "blue", //标题颜色
                        titleVisible: "", // 标题可见
                        rightShow: false, // boolean 是否右侧显示
                        axisType: "point", //text point  y轴类型 (可以不需要)
                        axisVisible: true, // boolean y轴是否展示
                        axisColor: "", //y轴颜色
                        axisStartValue: 0, //y轴起始值
                        axisEndValue: 100,//y轴结束值
                        scaleDivisionNum: 14, //刻度分割数
                        startValueOffset: 1, //起始值偏移量
                        endValueOffset: 1,//结束值偏移量
                        customScale: [],//自定义刻度
                        customHeightProportion: 0.2, //自定义高度比例
                        customHeightScale: [], //自定义高度刻度
                        style: {}, //文本轴样式
                        breakpointConnected: false, //断点是否连线
                    },
                    {
                        id: "ceshi222",  //唯一标记
                        legendColor: "red", //图例颜色
                        legendSize: "",  //图例大小
                        legendStyle: "", //图例样式
                        title: "", //标题
                        titleColor: "red", //标题颜色
                        titleVisible: "", // 标题可见
                        rightShow: false, // boolean 是否右侧显示
                        axisType: "point", //text point  y轴类型 (可以不需要)
                        axisVisible: true, // boolean y轴是否展示
                        axisColor: "", //y轴颜色
                        axisStartValue: 35, //y轴起始值
                        axisEndValue: 43,//y轴结束值
                        scaleDivisionNum: 14, //刻度分割数
                        startValueOffset: 4, //起始值偏移量
                        endValueOffset: 1,//结束值偏移量
                        customScale: [],//自定义刻度
                        customHeightProportion: 0.2, //自定义高度比例
                        customHeightScale: [], //自定义高度刻度
                        style: {}, //文本轴样式
                        breakpointConnected: false, //断点是否连线
                    },
                    //{
                    //    id: "ceshi333",  //唯一标记
                    //    legendColor: "black", //图例颜色
                    //    legendSize: "",  //图例大小
                    //    legendStyle: "", //图例样式
                    //    title: "", //标题
                    //    titleColor: "black", //标题颜色
                    //    titleVisible: "", // 标题可见
                    //    rightShow: true, // boolean 是否右侧显示
                    //    axisType: "point", //text point  y轴类型 (可以不需要)
                    //    axisVisible: true, // boolean y轴是否展示
                    //    axisColor: "", //y轴颜色
                    //    axisStartValue: 100, //y轴起始值
                    //    axisEndValue: 0,//y轴结束值
                    //    scaleDivisionNum: 14, //刻度分割数
                    //    startValueOffset: 0, //起始值偏移量
                    //    endValueOffset: 0,//结束值偏移量
                    //    customScale: [],//自定义刻度
                    //    customHeightProportion: 0.2, //自定义高度比例
                    //    customHeightScale: [], //自定义高度刻度
                    //    style: {}, //文本轴样式
                    //    breakpointConnected: false, //断点是否连线
                    //}
                ],
                Labels: [
                    {
                        left: 0,                                     //左偏移         
                        top: 0,                                      //上偏移                   
                        text: "文本框1111",                              //文本           
                        alignment: "Center",                         //水平对齐方式   
                        lineAlignment: "Center",                     //垂直对齐方式   
                        textFontName: "宋体",                        //字体名        
                        textFontSize: 9,                             //字体大小       
                        textFontBold: false,                         //文本加粗       
                        textFontItalic: false,                       //文本倾斜       
                        textFontUnderline: false,                    //文本下划线     
                    },
                    {
                        left: 0,                                     //左偏移         
                        top: 20,                                      //上偏移                   
                        text: "文本框222222",                              //文本           
                        alignment: "Center",                         //水平对齐方式   
                        lineAlignment: "Center",                     //垂直对齐方式   
                        textFontName: "宋体",                        //字体名        
                        textFontSize: 9,                             //字体大小       
                        textFontBold: false,                         //文本加粗       
                        textFontItalic: false,                       //文本倾斜       
                        textFontUnderline: false,                    //文本下划线     
                    },
                ],
                Data: { //数据
                    "ceshi111": [
                        {
                            time: "08:20",
                            value: "60",
                            text: "",
                        },
                        {
                            time: "12:30",
                            value: "50",
                            text: "",
                        },
                        {
                            time: "20:40",
                            value: "70",
                            text: "",
                        },
                        {
                            time: "04:10",
                            value: "30",
                            text: "",
                        },
                    ],
                    "ceshi222": [
                        {
                            time: "05:20",
                            value: "35",
                            text: "",
                        },
                        {
                            time: "7:30",
                            value: "40",
                            text: "",
                        },
                        {
                            time: "9:40",
                            value: "37",
                            text: "",
                        },
                        {
                            time: "12:10",
                            value: "36",
                            text: "",
                        },

                    ],
                    "ceshi333": [
                        {
                            time: "08:20",
                            value: "60",
                            text: "",
                        },
                        {
                            time: "12:30",
                            value: "50",
                            text: "",
                        },
                        {
                            time: "20:40",
                            value: "70",
                            text: "",
                        },
                        {
                            time: "04:10",
                            value: "30",
                            text: "",
                        },

                    ],
                }, 
                TimeScale: {  //时间刻度
                    startTime: "",//起始时间
                    endTime: "", //结束时间
                    scaleColor: "", //刻度颜色
                }
            }
        }
        if (typeof newValue == "object") {
            //循环赋值
            for (var i in rootElement.TrendChartSvg.Options) {
                if (newValue[i]) { //存在特定的对象
                    for (var j in rootElement.TrendChartSvg.Options[i]) {
                        if (newValue[i][j] != null) {
                            //只要不等于null都赋值
                            rootElement.TrendChartSvg.Options[i][j] = newValue[i][j]
                        }
                    }

                }
            }
        }
        return rootElement.TrendChartSvg.options;
    },

    //进行数据计算
    DataCalculationFun: function (rootElement) {
        console.log("DataCalculationFun");
        //拿到opt
        var opts = rootElement.TrendChartSvg.Options;
        var infos = rootElement.TrendChartSvg.SvgDataInfos;
        if (infos == null) {
            infos = rootElement.TrendChartSvg.SvgDataInfos = {
                top: 0, //定位
                left: 0, //定位
                width: opts.Default.width, //宽度
                height: opts.Default.height, //高度
                columnIndex: [], //垂直坐标
                rowIndex: [], //水平坐标
                stepColumnNumber: 0, //垂直网格线之间的距离
                stepRowNumber: 0, //水平网格线之间的距离
                textlableHeight: 12, //文本展示高度
                verticalBand: [], //垂直分割坐标
                verticalText: [], //垂直分割值
                verticalDivisionNum: 0, //垂直分割数
                horizontalBand: [], //水平分割坐标
                horizontalText: [], //水平分割值
                yAxis: {
                    "ceshi111": {
                        //id: "", // id
                        //top: 0, //垂直起始位置
                        //bottom: 0, //垂直结束位置
                        //left: 0, //水平位置
                        //stepYValue: 0, //数据间隔
                        //stepYValueArr: [], //数据值
                        //stepHeight: 0, //步进高度
                    }
                }, //y轴相关属性
            };
        }

        //拿到当前单元格属性
        var targetCell = rootElement.CurrentTableCell();
        if (targetCell != null) {
            targetCell = rootElement.GetElementProperties(targetCell);
            //找到目标canvas元素
            var targetCanvas = rootElement.PageContainer.querySelectorAll("canvas")[targetCell.OwnerPageIndex];
            //计算此处的位置
            infos.top = parseFloat((targetCell.TopInOwnerPage / 300 * 96.00001209449).toFixed(2)) + targetCanvas.offsetTop + 3;
            infos.left = parseFloat((targetCell.LeftInOwnerPage / 300 * 96.00001209449).toFixed(2)) + targetCanvas.offsetLeft;


            //判断是否存在宽度
            if (opts.Default.width == 0 || opts.Default.height == 0) {
                if (!rootElement.__DCWriterReference.invokeMethod("HasSelection")) {
                    //自己计算宽度
                    rootElement.SelectCurrentElement();
                }
                
                var allSelectCell = rootElement.GetSelectTableAndCell();
                //找到开始和结束两个
                //var firstCell = allSelectCell[0];
                var lastCell = allSelectCell[allSelectCell.length - 1];
                lastCell = rootElement.GetElementProperties(lastCell.CellNativeHandle);
                infos.width = parseFloat(((lastCell.LeftInOwnerPage + lastCell.Width - targetCell.LeftInOwnerPage) / 300 * 96.00001209449).toFixed(2));
                infos.height = parseFloat(((lastCell.TopInOwnerPage + lastCell.Height - targetCell.TopInOwnerPage) / 300 * 96.00001209449).toFixed(2)) + 1;
            }
        } 

        infos.columnIndex = [];
        var verticalDivisionNum = infos.verticalDivisionNum = opts.Default.chronologicalOrder.length * opts.Default.timeDivisionNum;
        //console.log(verticalDivisionNum)
        //计算垂直线的位置
        var stepColumnNumber = infos.stepColumnNumber = parseFloat(((infos.width - opts.Default.leftYAxisSpace - opts.Default.rightYAxisSpace) / verticalDivisionNum).toFixed(2));
        //自此出拆分水平坐标
        opts.Default.chronologicalOrder.forEach((item, index) => {
            var nextText = opts.Default.chronologicalOrder[index + 1]
            if (nextText == null) {
                nextText = opts.Default.chronologicalOrder[0]
            }
            if (nextText == 0) {
                nextText = 24;
            }
            var step = (nextText - item) / opts.Default.timeDivisionNum;
            for (var i = 0; i < opts.Default.timeDivisionNum; i++) {
                infos.horizontalText.push(item + (i * step))
            }
            if (index == opts.Default.chronologicalOrder.length - 1) {
                infos.horizontalText.push(nextText - 0.01);
            }
        })
        for (var i = 0; i <= verticalDivisionNum; i++) {
            infos.columnIndex.push(stepColumnNumber * i + opts.Default.leftYAxisSpace);
        }
        infos.rowIndex = [];
        //计算水平线的位置
        var stepRowNumber = infos.stepRowNumber = parseFloat((infos.height / opts.Default.horizontalDivisionNum).toFixed(2));
        for (var i = 1; i < opts.Default.horizontalDivisionNum; i++) {
            infos.rowIndex.push(stepRowNumber * i);
        }
    },

    //创建svg元素
    CreateSvgElement: function (rootElement) {
        var infos = rootElement.TrendChartSvg.SvgDataInfos;
        var svg = d3
            .create('svg')
            .attr('dctype', 'TrendChartPage')
            .attr('width', infos.width)
            .attr('height', infos.height)
            .attr('native-width', infos.width)
            .attr('native-height', infos.height)
            .attr('style',``);
        var thisSvgEle = svg.node();
        //定位到页面位置中
        //获取到编辑器页面的offsetTop,和offsetLeft
        thisSvgEle.style.background = "#ffffff";
        thisSvgEle.style.position = "absolute";
        thisSvgEle.style.border = "1px solid #000000";
        thisSvgEle.style.boxSizing = "border-box";
        thisSvgEle.style.zIndex = "10000";
        thisSvgEle.style.top = infos.top + "px";
        thisSvgEle.style.left = infos.left + "px";
        rootElement.PageContainer.appendChild(thisSvgEle);
        rootElement.TrendChartSvg.SvgNode = thisSvgEle;
        rootElement.TrendChartSvg.D3SvgNode = {
            MainSvg: svg
        }; 
    },

    //绘制网格线
    DrawGridlines: function (rootElement) {
        var opts = rootElement.TrendChartSvg.Options;
        var infos = rootElement.TrendChartSvg.SvgDataInfos;
        //console.log(infos);
        var trendChartBgLine = rootElement.TrendChartSvg.D3SvgNode.MainSvg.append('g')
            .attr('dctype', "trendChartBgLine")
        rootElement.TrendChartSvg.D3SvgNode.TrendChartBgLine = trendChartBgLine;
        //计算所需要的线条
        var verticalData = [...new Array(infos.columnIndex.length).keys()]
        //开始绘制线条
        var trendChartVerticalLine = trendChartBgLine.append('g')
            .attr("dctype", "trendChartVerticalLine")
            .selectAll('line')
            .data(verticalData)
            .join('line')
            .attr('x1', (d, i) => {
                //自此出报错垂直线坐标
                infos.horizontalBand.push(infos.columnIndex[i]);
                return infos.columnIndex[i];
            })
            .attr('x2', (d, i) => {
                return infos.columnIndex[i];
            })
            .attr('y1', 0)
            .attr('y2', infos.height)
            
            .attr('fill', 'none')
            .attr('stroke', "black")
            .attr('stroke-linejoin', "round")
            .attr('stroke-linecap', "round")
            //.attr('style', (d, i) => {
            //    if (i % (calculationData.TickTextsData.length) == 0 && i != 0) {
            //        return `stroke-width: ${gridLineWidth}; stroke: ${bigGridLineColorValue};`;
            //    }
            //    return `stroke-width: ${thinLineWidth}`;
            //});
        rootElement.TrendChartSvg.D3SvgNode.TrendChartVerticalLine = trendChartVerticalLine;

        //计算所需要的线条
        var horizontalData = [...new Array(infos.rowIndex.length).keys()]
        //开始绘制线条
        var trendCharthorizontalLine = trendChartBgLine.append('g')
            .attr("dctype", "trendCharthorizontalLine")
            .selectAll('line')
            .data(horizontalData)
            .join('line')
            .attr('x1', opts.Default.leftYAxisSpace)
            .attr('x2', infos.width - opts.Default.rightYAxisSpace)
            .attr('y1', (d, i) => {
                return infos.rowIndex[i];
            })
            .attr('y2', (d, i) => {
                return infos.rowIndex[i];
            })
            .attr('fill', 'none')
            .attr('stroke', "black")
            .attr('stroke-linejoin', "round")
            .attr('stroke-linecap', "round")
        //.attr('style', (d, i) => {
        //    if (i % (calculationData.TickTextsData.length) == 0 && i != 0) {
        //        return `stroke-width: ${gridLineWidth}; stroke: ${bigGridLineColorValue};`;
        //    }
        //    return `stroke-width: ${thinLineWidth}`;
        //});
        rootElement.TrendChartSvg.D3SvgNode.trendCharthorizontalLine = trendCharthorizontalLine;
    },

    //绘制y轴样式
    DrawYAxis: function (rootElement) {
        var opts = rootElement.TrendChartSvg.Options;
        var infos = rootElement.TrendChartSvg.SvgDataInfos;
        //console.log("DrawYAxis", infos);
        //循环y轴数据
        if (Array.isArray(opts.YAxis)) {
            //绘制一个y轴包裹元素
            var trendChartYAxisContainer = rootElement.TrendChartSvg.D3SvgNode.MainSvg.append('g')
                .attr('dctype', "trendChartYAxisContainer")
            rootElement.TrendChartSvg.D3SvgNode.TrendChartYAxisContainer = trendChartYAxisContainer;
            //开启循环绘制
            //计算左右两侧的数量
            var leftSide = 0;
            var rightSide = 0;
            for (var i = 0; i < opts.YAxis.length; i++) {
                //开始绘制先确定位置默认宽15px
                var thisYAttr = opts.YAxis[i];
                if (thisYAttr.axisVisible) {
                    var infoYAttr = infos.yAxis[thisYAttr.id];
                    infoYAttr = infoYAttr ? infoYAttr : {};
                    infoYAttr = infos.yAxis[thisYAttr.id] = thisYAttr;

                    if (thisYAttr.rightShow) {
                        infoYAttr.left = infos.width - opts.Default.rightYAxisSpace + (rightSide * 35) + 15;
                        rightSide++;
                    } else {
                        //表示为左侧展示
                        infoYAttr.left = opts.Default.leftYAxisSpace - (leftSide * 35 + 15);
                        leftSide++;
                    }
                    //infoYAttr.top = thisYAttr.startValueOffset * infos.stepColumnNumber;
                    //infoYAttr.bottom = infos.height - (thisYAttr.endValueOffset * infos.stepColumnNumber);
                    infoYAttr.height = infos.height;

                    //开始绘制
                    var trendChartYAxis = trendChartYAxisContainer.append('g')
                        .attr('dctype', "trendChartYAxis")
                        .attr(
                            'transform',
                            `translate(${infoYAttr.left},${0})`)
                        .attr("trendChartID", infoYAttr.id);
                    //判断y轴数组是否存在
                    if (Array.isArray(rootElement.TrendChartSvg.D3SvgNode.TrendChartYAxis) == false) {
                        rootElement.TrendChartSvg.D3SvgNode.TrendChartYAxis = [];
                    }
                    rootElement.TrendChartSvg.D3SvgNode.TrendChartYAxis.push(trendChartYAxis); 

                    //开始拆分数据
                    //axisStartValue: 0, //y轴起始值
                    //axisEndValue: 100,//y轴结束值
                    console.log("%c计算值", "color: blue")
                    thisYAttr.scaleDivisionNum = thisYAttr.scaleDivisionNum ? thisYAttr.scaleDivisionNum : opts.Default.horizontalDivisionNum;
                    var divNum = thisYAttr.scaleDivisionNum - thisYAttr.startValueOffset - thisYAttr.endValueOffset;
                    infoYAttr.stepYValue = (thisYAttr.axisEndValue - thisYAttr.axisStartValue) / divNum;
                    //获取线分割单只
                    infoYAttr.stepHeight = (infoYAttr.height - (thisYAttr.startValueOffset + thisYAttr.endValueOffset) * infos.stepRowNumber) / divNum;
                    infoYAttr.stepYValueArr = [];
                    for (var j = 0; j <= divNum; j++) {
                        infoYAttr.stepYValueArr.unshift((infoYAttr.stepYValue * j) + thisYAttr.axisStartValue);
                    }

                    trendChartYAxis.append('g')
                        .attr("dctype", "trendChartYAxis_Text")
                        .selectAll('text')
                        .data(infoYAttr.stepYValueArr)
                        .join('text')
                        .attr('style', `font-family:${"宋体"};font-size:${"12"}px;fill:${thisYAttr.titleColor};text-anchor: middle`)
                        .html((d, i) => {
                            if (i === 0) {
                                //是否显示标题
                                if (thisYAttr.titleVisible) {
                                    //绘制Title和BottomTitle
                                    trendChartYAxis.append('g')
                                        .attr("dctype", "dcYAxisInfos_Title")
                                        .selectAll("text")
                                        .data([0])
                                        .join('text')
                                        .attr('style', `font-family:${"宋体"};font-size:${"9"}px;fill:${"black"};text-anchor: middle`)
                                        .attr("x", 0)
                                        .attr("y", () => {
                                            return infos.textlableHeight / 1.5;
                                        })
                                        .text(thisYAttr.title);
                                }
                                infos.verticalText[thisYAttr.id] = [];
                            }
                            // <tspan dx="${-35 + index * 10}" dy="20">${value[1]}</tspan>`
                            //在此处保存y轴的分割值
                            infos.verticalText[thisYAttr.id].push(parseFloat(d.toFixed(2)));
                            return `${d.toFixed(2)}`;
                        })
                        .attr('x', 0)
                        .attr('y', (d, i) => {
                            if (i == 0) {
                                infos.verticalBand[thisYAttr.id] = []
                            }
                            infos.verticalBand[thisYAttr.id].push(infoYAttr.stepHeight * i + (thisYAttr.startValueOffset * infos.stepRowNumber));
                            //console.log(infoYAttr.stepHeight * i, i);
                            if (i == 0 && thisYAttr.startValueOffset == 0) {
                                return (infos.textlableHeight / 2);
                            }
                            else if (i == infoYAttr.stepYValueArr.length - 1 && thisYAttr.endValueOffset == 0) {
                                return infoYAttr.stepHeight * i + (infos.textlableHeight / 2) - 5;
                            }
                            else {
                                return infoYAttr.stepHeight * i + (infos.textlableHeight / 2) + (thisYAttr.startValueOffset) * infos.stepRowNumber
                            }
                        });
                    
                }
                
            }
        }
    },

    //绘制点
    DrawPoint: function (rootElement) {
        var opts = rootElement.TrendChartSvg.Options;
        var infos = rootElement.TrendChartSvg.SvgDataInfos;
        //绘制一个y轴包裹元素
        var trendChartYPointContainer = rootElement.TrendChartSvg.D3SvgNode.MainSvg.append('g')
            .attr('dctype', "trendChartYAxisContainer");
        rootElement.TrendChartSvg.D3SvgNode.TrendChartYPointContainer = trendChartYPointContainer;
        //console.log("DrawPoint", infos);
        //var xScale = d3.scaleBand(infos.horizontalText, infos.horizontalBand);
        //开始循环并计算结果
        if (opts.Data) {
            //console.log(opts.Data);
            for (var i in opts.Data) {
                //创建元素对应的g包裹元素
                var yVal = infos.verticalBand[i];
                //只有存在y轴的情况下才展示
                if (yVal && Array.isArray(opts.Data[i])) {
                    //创建外层包裹元素
                    var trendChartYPoint = trendChartYPointContainer
                        .append('g')
                        .attr("dctype", `trendChartYPoint`)
                        .attr("name", i);
                    //x轴的标尺
                    //var yScale = d3.scaleOrdinal(infos.verticalText[i], yVal);
                    var pointIndexArr = [];
                    opts.Data[i].forEach((item, index) => {
                        //计算到所有的位置
                        var timeText = item.time ? item.time.split(":") : [];
                        //只有在确保数据正确的情况下展示
                        if (timeText.length == 2) {
                            timeText[1] = parseInt(timeText[1]) / 60 * 100;
                            //拼接数据
                            timeText = parseFloat(timeText[0] + '.' + timeText[1]);
                            //console.log(timeText);
                            pointIndexArr.push({
                                //x: xScale(timeText),
                                //y: yScale(parseInt(item.value))
                                x: WriterControl_TrendChart.CalculateLocation(timeText, infos.horizontalText, infos.horizontalBand),
                                y: WriterControl_TrendChart.CalculateLocation(parseInt(item.value), infos.verticalText[i], infos.verticalBand[i])
                            })
                        }
                    })
                    var yAttr = infos.yAxis[i];
                    //console.log(pointIndexArr);
                    pointIndexArr.sort((a,b) => {
                        return a.x - b.x
                    })
                    //开始绘制点和折线
                    pointIndexArr.forEach((point,pointIndex) => {
                        
                        //绘制连线
                        if (pointIndex < pointIndexArr.length - 1) {
                            trendChartYPoint.append("line")
                                .attr('x1', point.x )
                                .attr('y1', point.y)
                                .attr('x2', pointIndexArr[pointIndex + 1].x)
                                .attr('y2', pointIndexArr[pointIndex + 1].y)
                                .attr('fill', 'none')
                                .attr('stroke', yAttr.legendColor)
                                .attr('stroke-width', 1)
                                .attr('stroke-linejoin', "round")
                                .attr('stroke-linecap', "round");
                        }

                        var IconData = {
                            content: trendChartYPoint,
                            data: [0],
                            x: point.x,
                            y: point.y,
                            fill: yAttr.legendColor,
                            stroke: yAttr.legendColor,
                            r: 5,
                        };
                        WriterControl_TrendChart.IconDrawObj()["SolidCicle"](IconData);
                    })
                }
            }
        }
        
        ////计算位置
        ////console.log(xScale(0), yScale(0))
        ////展示标尺
        //var svgEle = rootElement.TrendChartSvg.D3SvgNode.TrendChartBgLine
        //var g3 = svgEle.append('g')
        //    .attr("transform",`translate(${160},${0})`)
        ////var xAxis = d3.axisTop(xScale).tickPadding(10)
        //var yAxis = d3.axisLeft(yScale)
        //window.yScale = yScale;
        ////g3.append('g').call(xAxis)
        //g3.append('g').call(yAxis)

    },

    //绘制文本标签
    DrawTextLabel: function (rootElement) {
        var opts = rootElement.TrendChartSvg.Options;
        var infos = rootElement.TrendChartSvg.SvgDataInfos;
        if (Array.isArray(opts.Labels)) {
            //console.log(infos);
            var trendChartTextLabelContainer = rootElement.TrendChartSvg.D3SvgNode.MainSvg.append('g')
                .attr('dctype', "trendChartTextLableContainer")
            rootElement.TrendChartSvg.D3SvgNode.TrendChartTextLabelContainer = trendChartTextLabelContainer;
            for (var i = 0; i < opts.Labels.length; i++) {
                var thisLabel = opts.Labels[i];
                trendChartTextLabelContainer.append('text')
                    .attr('style', `font-family:${thisLabel.textFontName} ;font-size:${thisLabel.textFontSize}px;font-weight:${thisLabel.textFontBold ? 900 : 0};text-decoration: ${thisLabel.textFontUnderline ? 'underline' : 'none'};color:${thisLabel.textColorValue};font-style:${thisLabel.textFontItalic ? 'italic' : 'none'};`)
                    .attr('fill', `${thisLabel.textColorValue ? thisLabel.textColorValue : "black"}`)
                    .attr('x', thisLabel.left)
                    .attr('y', thisLabel.top + (thisLabel.textFontSize * 1.2))
                    .attr('xml:space', 'preserve')
                    .text(thisLabel.text);
            }
        }
    },

    //计算位置
    CalculateLocation: function (value, textArr, bandArr) {
        //开始进行计算
        var hasIndex = textArr.indexOf(value);
        if (hasIndex >= 0) {
            return bandArr[hasIndex];
        } else {
            //开始循环计算是否在中间时刻
            for (var i = 0; i < textArr.length - 1; i++) {
                var nextText = textArr[i + 1];
                if (nextText == 0) {
                    nextText = 24;
                }
                if ((value > textArr[i] && value < nextText) || (value < textArr[i] && value >= nextText)) {
                    //计算其中的差值
                    var diff = Math.abs(bandArr[i + 1] - bandArr[i]) / Math.abs(nextText - textArr[i]);
                    //计算位置
                    return Math.abs(value - textArr[i]) * diff + bandArr[i];
                }
            }

            //如果到了这里依旧没有return出去 比较最大值和最小值的差距
            if (value < textArr[0]) {
                return bandArr[0];
            } else if (value > textArr[textArr.length - 1]) {
                return bandArr[bandArr.length - 1];
            }

        }
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
                r = 5,
            }) => { //是否为标记物
                // 增加icon  红色空心
                content
                    .append('g')
                    .attr('fill', fill)
                    .attr('stroke', stroke)
                    .attr('stroke-width', 1)
                    .selectAll('circle')
                    .data(data)
                    .join('circle')
                    .attr('cx', x)
                    .attr('cy', y)
                    .attr('r', r)
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
            // 绘制圆形点图标
            RoundDotIcon: ({
                content,
                data,
                x,
                y,
                fill = 'white',
                stroke = 'blue',
                r = 6,
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
                    .attr('title', title);

            },
            // 绘制三角形
            SolidTriangle: ({
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
                    .attr('title', title);

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
                    .attr('title', title);

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
                    });
            },
            // 空心倒三角
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
                    });
            },
            // 绘制方形
            Square: ({
                content,
                data,
                x,
                y,
                fill = 'blue',
                stroke = 'blue',
                r = 5,
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
                    .attr('title', title);

            },
            //空心圆
            circular: ({
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
                    .attr('r', 5)
                    .attr('title', (i) => {
                        if (typeof title === 'function') {
                            title = title(i);
                        }
                        return title;
                    });


            },
            //空心带圆点
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
                    .attr('r', 5)
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
                    });

            },
            // 双层同心圆
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
                    .attr('r', 5)
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
                    });

            },
        };
    },

    //数据源绑定赋值
    TrendChartDataSourceToDocument: function(rootElement,data){
        // console.log(data)
        var hasTargetCustomAttr = rootElement.getAttribute("DCRecordAndTrendChartData");
        if (hasTargetCustomAttr == null){
            hasTargetCustomAttr = "DCRecordAndTrendChartData";
        }
        //拿到对应的数据源配置信息
        var datasource = rootElement.GetCustomAttribute(hasTargetCustomAttr);
        if(datasource){
            datasource = JSON.parse(datasource);
            if(typeof data == "object"){
                data = WriterControl_TrendChart.DefaulDataSourceData(data);
                //开始循环数据
                for(var i in data){
                    var hasDataAttr = datasource[i];
                    if(hasDataAttr){
                        var targetTable = rootElement.GetElementProperties(hasDataAttr.tableID);
                        //开始循环赋值并合并单元格
                        var thisDataSourceData = data[i];
                        //获取到所有的表格行
                        var allRowEle = targetTable.Rows;
                        // console.log(targetTable,hasDataAttr);
                        //矫正表格开始和结束位置
                        if(Array.isArray(hasDataAttr.rowColStart)){
                            var startIndex = hasDataAttr.rowColStart[0];
                            startIndex = parseInt(startIndex);
                            if(isNaN(startIndex)){
                                startIndex = 1;
                            }
                            hasDataAttr.rowColStart[0] = startIndex;
                            var endIndex = hasDataAttr.rowColStart[1];
                            endIndex = parseInt(endIndex);
                            if(isNaN(endIndex)){
                                endIndex = 1;
                            }
                            hasDataAttr.rowColStart[1] = endIndex;
                        }else{
                            hasDataAttr.rowColStart = [1,1]
                        }
                        //判断是否需要自动生成表格行
                        if(allRowEle.length - hasDataAttr.rowColStart[0] < thisDataSourceData.length){
                            //对表格进行行新增
                            var options = {
                                tableid: hasDataAttr.tableID,
                                rowindex: null,
                                rowcount: thisDataSourceData.length - (allRowEle.length - (hasDataAttr.rowColStart[0] - 1))
                            };
                            rootElement.DCExecuteCommand("table_insertrowdown", false, options);
                            //重新获取所有表格行
                            targetTable = rootElement.GetElementProperties(hasDataAttr.tableID);
                            allRowEle = targetTable.Rows;
                        }
                        if(Array.isArray(hasDataAttr.rowColEnd)){
                            var startIndex = hasDataAttr.rowColEnd[0];
                            startIndex = parseInt(startIndex);
                            if(isNaN(startIndex)){
                                startIndex = targetTable.Rows.length;
                            }
                            hasDataAttr.rowColEnd[0] = startIndex;
                            var endIndex = hasDataAttr.rowColEnd[1];
                            endIndex = parseInt(endIndex);
                            if(isNaN(endIndex)){
                                endIndex = targetTable.Columns.length;
                            }
                            hasDataAttr.rowColEnd[1] = endIndex;
                        }else{
                            //设置到当前表格的最后一行
                            hasDataAttr.rowColEnd = [targetTable.Rows.length, targetTable.Columns.length];
                        }
                        //拆分需要哪些行(防止长度不对)
                        allRowEle.splice(hasDataAttr.rowColEnd[0] - 1, allRowEle.length - hasDataAttr.rowColEnd[0])
                        allRowEle.splice(0, hasDataAttr.rowColStart[0] - 1)
                        // console.log(allRowEle);
                        //在此处处理单元格
                        var allCellEle = [];
                        //在此处判断元素
                        //var targetPageIndex = 0;
                        //开始循环行获取单元格
                        allRowEle.forEach((row,rowIndex) => {
                            //获取到所有的单元格
                            var rowAttr = rootElement.GetElementProperties(row);
                            var targetRowCells = rowAttr.Cells;
                            //拆分需要哪些单元格
                            targetRowCells.splice(hasDataAttr.rowColEnd[1] - 1, targetRowCells.length - hasDataAttr.rowColEnd[1])
                            targetRowCells.splice(0, hasDataAttr.rowColStart[1] - 1)
                            // console.log(targetRowCells);
                            allCellEle.push(targetRowCells);
                        })

                        //var a = new Date().getTime();
                        //开始行的循环
                        thisDataSourceData.forEach((rowItem,rowIndex)=>{
                            //var b = new Date().getTime();
                            //在此处循环清空null值
                            // allCellEle[rowIndex] = allCellEle[rowIndex].filter(n => n != null)
                            //清空总合并值
                            var colSpanNum = 0;
                            rowItem.forEach((cellItem,cellIndex)=>{
                                // console.log(rowIndex,cellIndex + colSpanNum)
                                var cellNativeHandle = allCellEle[rowIndex][cellIndex + colSpanNum];
                                while(cellNativeHandle === null){
                                    colSpanNum = colSpanNum + 1;
                                    cellNativeHandle = allCellEle[rowIndex][cellIndex + colSpanNum];
                                }
                                // console.log("aaaaaa")
                                //开始合并单元格
                                cellItem.colspan = parseInt(cellItem.colspan);
                                if(isNaN(cellItem.colspan)){
                                    cellItem.colspan = 1;
                                }
                                cellItem.rowspan = parseInt(cellItem.rowspan);
                                if(isNaN(cellItem.rowspan)){
                                    cellItem.rowspan = 1
                                }
                                //开始合并单元格
                                if(cellItem.colspan > 1 || cellItem.rowspan > 1){
                                    // rootElement.SelectContentByStartEndElement(cellNativeHandle,allCellEle[rowIndex][cellIndex + colSpanNum]);
                                    // document.WriterControl.DCExecuteCommand("Table_MergeCell",false,null);
                                    rootElement.SetElementProperties(cellNativeHandle,{
                                        ColSpan: cellItem.colspan,
                                        RowSpan: cellItem.rowspan,
                                        Style: {
                                            Align: cellItem.Align,
                                            Verticalalign: cellItem.verticalalign
                                        }
                                    },false);
                                    //在此次对单元格进行配置,防止出现赋值问题
                                    if(cellItem.rowspan > 1){
                                        for(var i=1;i < cellItem.rowspan;i++){
                                            //找到对应的数据
                                            var targetRow = allCellEle[rowIndex + i];
                                            for(var j=0;j<cellItem.colspan;j++){
                                                //是否存在合并列
                                                targetRow.splice(cellIndex + colSpanNum + j,1,null);
                                            }
                                        }
                                    }
                                    colSpanNum = colSpanNum + cellItem.colspan - 1;
                                }
                                //判断值
                                if (/\.(jpg|jpeg|png|gif|webp)$/i.test(cellItem.text) || /^(http|https)/i.test(cellItem.text)) {
                                    var options = [
                                        {
                                            image: {
                                                id: "image1",  //图片id
                                                src: cellItem.text, //图片路径
                                                width: 100,  //图片宽度
                                                height: 100,  //图片高度
                                                savecontentinfile: false
                                            }
                                        }
                                    ]
                                    //console.log(options, cellNativeHandle)
                                    rootElement.SetChildElements(cellNativeHandle, options, 'beforeBegin',false);
                                    //rootElement.SelectContentByStartEndElement(cellNativeHandle, cellNativeHandle);
                                    //var options = [
                                    //    {
                                    //        id: "123",  //图片id
                                    //        src: cellItem.text, //图片路径
                                    //        width: "100",  //图片宽度
                                    //        height: "100",  //图片高度
                                    //        savecontentinfile: false
                                    //    }
                                    //]
                                    //rootElement.InsertImageByJSONText(options);
                                } else {
                                    rootElement.SetTableCellTextExtByHandle(cellNativeHandle, cellItem.text);
                                }
                            })
                            //var c = new Date().getTime();
                            //console.log("完成一行赋值:",c - b);
                            
                        })
                        //var d = new Date().getTime()
                        //rootElement.RefreshInnerView();
                        //刷新表格内数据
                        rootElement.EditorRefreshViewElement(hasDataAttr.tableID)
                        //console.log("完成全部赋值:",d - a)
                    }
                }
            }
        }

        ////自此出对元素进行配置
        //if (allRowEle && Array.isArray(allRowEle)) {
        //    var targetPageIndex = 0;
        //    for (var z = 0; z < allRowEle.length; z++) {
        //        if (allRowEle[z].HeaderStyle === false) {
        //            //找打对应的OwnerPageIndex
        //            //if (allRowEle[z])
        //        }
        //    }
        //}
    },

    //整理下数据
    DefaulDataSourceData: function(data){
        var a = new Date().getTime();
        if(data){
            //设置默认
            var defaultData = {
                "text": "",
                "colspan":1,//右合并
                "rowspan":1,//下合并
                "align":"left",//左右对齐（居左、居右、居中）
                "verticalalign":"top",//上下对齐（顶端、垂直居中、底端）
                "firstLineIndent":"false",// 默认false
                "fontsize":"", //默认取当前字体
                "fontcolor":"", //默认取当前颜色 
                "bold":"false", //"默认：false",不加粗
                "italic":"false", //"默认：false",不斜体
                "strike":"false", //"默认：false",不删除线
                "underline":"false", //"默认：false",不下划线

                "bordertop":"1", 
                "borderbottom":"1",
                "borderleft":"1",
                "borderright":"1",
                "bordertopleft":"0",
                "bordertopright":"0",

                "bordertopcolor":"#000000",
                "borderbottomcolor":"#000000",
                "borderleftcolor":"#000000",
                "borderrightcolor":"#000000",

                "cellgridline":"false",//用于自动换行实现，自动计算所需跨度等
                "autofix":"none",//枚举:默认none，  none,singleline,multiline
             }
            for(var i in data){
                if(Array.isArray(data[i])){
                    data[i].forEach((item)=>{
                        item.forEach((d,index)=>{
                            var cloneDefault = JSON.parse(JSON.stringify(defaultData));
                            //在此处对元素进行赋值
                            for(var attr in cloneDefault){
                                if(d[attr] != null){
                                    cloneDefault[attr] = d[attr];
                                }
                            }
                            item[index] = cloneDefault;
                        })
                    })
                }
            }
        }
        //var b = new Date().getTime();
        //console.log("DefaulDataSourceData",b - a);
        return data
    },


    //在此处对图片进行整理,对当前页所有的行进行属性设置
    InsertImageSetTableRowAttr: function (rootElement) {
        //获取到当前光标的位置
        var oldCaretOption = rootElement.oldCaretOption;
        //获取到在第几页
        var pageIndex = oldCaretOption.intPageIndex;
        var inputAttr = rootElement.GetElementProperties(rootElement.CurrentInputField());
        var imageAttr = null;
        //找打当前图片
        for (var j = 0; j < inputAttr.Elements.length; j++) {
            imageAttr = rootElement.GetElementProperties(inputAttr.Elements[j]);
            if (imageAttr && imageAttr.TypeName == "XTextImageElement") {
                break;
            }
        }
        if (imageAttr && typeof pageIndex == "number") {
            //设置输入域属性
            rootElement.SetTableRowAttribute(inputAttr.NativeHandle, {
                LockInputFieldImage: imageAttr.Src
            })
            //获取到所有的表格行
            var rows = rootElement.GetElementsByTypeName("XTextTableRowElement");
            if (rows != null) {
                //找到当前页的所有表格行
                //var currentPageTableRows = [];
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i].HeaderStyle === false && rows[i].OwnerPageIndex == pageIndex) {
                        //currentPageTableRows.push(rows[i])
                        //直接设置属性
                        rootElement.SetTableRowAttribute(rows[i].NativeHandle, {
                            LockInputFieldImage: imageAttr.Src
                        })
                    }
                }
            }
        }
    },
}
