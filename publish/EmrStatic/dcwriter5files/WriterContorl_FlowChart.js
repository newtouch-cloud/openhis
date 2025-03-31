/* 产程图表控件
 * @Author: 南京都昌信息科技有限公司 李新宇
 * @Date: 2024-11-26 09:58:01
 * @LastEditors: lxy x13141668665@163.com
 * @LastEditTime: 2024-11-26 11:06:42
 * @Description:
 */

"use strict";
import { d3 } from "./WriterControl_DrawD3.js";
export let WriterContorl_FlowChart = {
  /**
   *  创建产程图表控件
   * @param {any} instance .NET对象
   * @param {string} typeName 类型名称
   * @returns 创建的JS封装包，如果未能封装则返回对象本身
   */
  CreateFlowControlInit: function (rootElement, data, type, args) {
    let that = this;
    // //整理数据
    if (!data) {
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

      that.CreateDefaultData(rootElement, data); //赋默认值
      //最开始直接修正数据
      that.ChangeData(rootElement, data);
      // //对数据进行修正
      // that.ChangeValue(rootElement, data);

      //绘制外层包裹元素
      that.CreatePageContainer(rootElement);

      // //绘制svg元素
      that.DrawSvg(rootElement);

      //绘制值
      if (data && data.Values) {
        //处理并绘制值
        that.ChangeValue(rootElement, data.Values);
      }

      rootElement._ctl = null;
      if (rootElement.isFileNew !== true) {
        if (type) {
          if (lastScrollTop && type != "EventFlowDocumentLoad") {
            rootElement.pageContainer.scrollTo(0, lastScrollTop);
          }
          rootElement.InnerRaiseEvent(type, args);
        } else {
          //初始化完成回调函数
          rootElement.InnerRaiseEvent("EventFlowControlInit");
          //设置注册码信息
          var registerCodeStr =
            rootElement.getAttribute("registercode") || null;
          if (registerCodeStr) {
            rootElement.FlowRegisterCode = registerCodeStr;
          }
        }
      }
    } else {
      console.log("未查找到根元素");
      return false;
    }
  },
  //整理数据
  ChangeData: function (rootElement, data) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    if (!CalculationData) {
      CalculationData = {};
    }

    var DefaultData = rootElement.DocumentOptions.DefaultData;
    if (!DefaultData) {
      DefaultData = {};
    }
    //先使用用户值覆盖默认值
    if (data) {
      for (var i in data) {
        CalculationData[i] = JSON.parse(JSON.stringify(data[i]));
        DefaultData[i] = JSON.parse(JSON.stringify(data[i]));
      }
    }
    /******************开始修正页面公共数据******************/
    //写入模式
    var DCType = rootElement.getAttribute("dctype");
    CalculationData.DCType =
      DCType == "DCFlowDesignControlForWASM" ? DCType : "DCFlowControlForWASM";
    //页面设置
    var PageSettings = CalculationData.Config.PageSettings;
    if (PageSettings) {
      //横向
      if (PageSettings.Landscape === true) {
        var oldWidth = PageSettings.PaperWidth;
        var oldHeight = PageSettings.PaperHeight;
        var oldLeft = PageSettings.LeftMargin;
        var oldRight = PageSettings.RightMargin;
        var oldTop = PageSettings.TopMargin;
        var oldBottom = PageSettings.BottomMargin;
        PageSettings.PaperWidth = oldHeight;
        PageSettings.PaperHeight = oldWidth;
        PageSettings.LeftMargin = oldBottom;
        PageSettings.RightMargin = oldTop;
        PageSettings.TopMargin = oldLeft;
        PageSettings.RightMargin = oldRight;
      }

      //计算svg内部绘制区域的宽高
      PageSettings.InnerWidth =
        PageSettings.PaperWidth -
        PageSettings.LeftMargin -
        PageSettings.RightMargin;
      PageSettings.InnerHeight =
        PageSettings.PaperHeight -
        PageSettings.TopMargin -
        PageSettings.BottomMargin;
    }
    /******************结束修正页面公共数据******************/

    /*********页眉*************/
    CalculationData["AllHeaderLabels"] = [];
    if (
      CalculationData &&
      CalculationData.Config &&
      CalculationData.Config.HeaderLabels
    ) {
      var HeaderLabels =
        JSON.parse(JSON.stringify(CalculationData.Config.HeaderLabels)) || [];
      //循环分组
      for (var i = 0; i < HeaderLabels.length; i++) {
        var thisLabel = HeaderLabels[i];
        //解决id不准确问题
        thisLabel["UID"] = "dcHeaderLabels_" + i;

        var groupIndex = parseInt(thisLabel.GroupIndex);
        if (isNaN(groupIndex)) {
          groupIndex = 0;
        }
        if (CalculationData["AllHeaderLabels"][groupIndex] == null) {
          CalculationData["AllHeaderLabels"][groupIndex] = [];
        }
        CalculationData.AllHeaderLabels[groupIndex].push(thisLabel);
      }
    }

    /*********附加表格高度*************/
    CalculationData["dcAttachedTableTotalHeight"] = 0;
    if (CalculationData.Config && CalculationData.Config.AttachedTable) {
      var AttachedTable = CalculationData.Config.AttachedTable;
      //附加表格的头部标签高度
      //   var AttachedTableHeaderLabelHeight = AttachedTable.AttachedTableHeaderLabelHeight || 0;

      //开始计算附加表格的总高度
      //   CalculationData["dcAttachedTableTotalHeight"] += AttachedTableHeaderLabelHeight;
      // console.log(CalculationData["dcAttachedTableTotalHeight"],'============');
      //附加表格的行数据
      var TableRowData = AttachedTable.TableRowData || [];
      if (TableRowData && TableRowData.length > 0) {
        for (var i = 0; i < TableRowData.length; i++) {
          var thisTableRowData = TableRowData[i];
          //行高默认是30
          CalculationData["dcAttachedTableTotalHeight"] +=
            thisTableRowData.RowHeight || 30;
        }
      }
    }

    /*********计算格子区域的总宽度*************/
    CalculationData["dcLeftYAxisTotalWidth"] = 0; //左边Y轴总宽度
    CalculationData["dcRightYAxisTotalWidth"] = 0; //右边Y轴总宽度
    var Config = CalculationData.Config || {};
    var YAxis = Config.YAxis || {};
    var LeftYAxis = YAxis.LeftYAxis || [];
    var RightYAxis = YAxis.RightYAxis || [];
    //计算Y轴总宽度
    for (var i = 0; i < [LeftYAxis, RightYAxis].length; i++) {
      var dcYAxisTotalWidthName = i === 0 ? "dcLeftYAxisTotalWidth" : "dcRightYAxisTotalWidth";
      var dcYAxis = i === 0 ? LeftYAxis : RightYAxis;
      for (var j = 0; j < dcYAxis.length; j++) {
        var thisYAxis = dcYAxis[j];
        var SpecifyTitleWidth = thisYAxis.SpecifyTitleWidth || 0;
        //文本的总宽度（+10是为了留出空白）
        var TitleTotalWidth = (thisYAxis.Title.length * CalculationData.Config.FontSize) + 10;
        if ((SpecifyTitleWidth === 0) || (SpecifyTitleWidth < TitleTotalWidth)) {
          //如果指定宽度小于title宽度，则按title宽度计算
          SpecifyTitleWidth = thisYAxis.SpecifyTitleWidth = TitleTotalWidth;
        }
        CalculationData[dcYAxisTotalWidthName] += SpecifyTitleWidth;
      }
    }
    //记录所有的Y轴信息
    CalculationData['AllYAxis'] = {};
    [LeftYAxis, RightYAxis].map(axis => {
      axis.map(item => {


        CalculationData['AllYAxis'][item.Name] = JSON.parse(JSON.stringify(item));
      });
    });

    //计算中间格子区域的总宽度
    CalculationData["dcAttachedTableTotalWidth"] =
      PageSettings.InnerWidth -
      CalculationData["dcLeftYAxisTotalWidth"] -
      CalculationData["dcRightYAxisTotalWidth"];
  },
  //整理value数据
  ChangeValue: function (rootElement) {
    // 先清空所有值，删除原有值的node
    rootElement.DocumentOptions.CalculationData.Values = {};
    var dcFlowYAixsValueG = rootElement.querySelector('[dctype="dcFlowYAixsValueG"]');
    dcFlowYAixsValueG && dcFlowYAixsValueG.remove();
    //删除警戒线
    var dcYAxisWringLineBoxG = rootElement.querySelector('[dctype="dcYAxisWringLineBoxG"]');
    dcYAxisWringLineBoxG && dcYAxisWringLineBoxG.remove();


    var dcAttachedTableValueG = rootElement.querySelector('[dctype="dcAttachedTableValueG"]');
    if (dcAttachedTableValueG) {
      dcAttachedTableValueG.remove();
    }

    //每次绘制都以DefaultData.Values为准
    var data = rootElement.DocumentOptions.DefaultData.Values || {};
    if (data && Object.keys(data).length > 0) {
      //整理值，绘制值
      var dataKeys = Object.keys(data) || [];
      var CalculationData = rootElement.DocumentOptions.CalculationData;
      var Config = CalculationData.Config || {};
      var YAxis = Config.YAxis || {};
      var AllYAxis = CalculationData.AllYAxis || {};

      //时间
      var StartingTime = Config.StartingTime; //开始时间(规律宫缩开始时间)
      var SplitXAxisNumberArray = YAxis.SplitXAxisNumberArray; //自定以产程时刻
      if (!SplitXAxisNumberArray || SplitXAxisNumberArray.length == 0) {
        // var SplitXAxisNumber = YAxis.SplitXAxisNumber || 24; //设置默认的产程时刻
        SplitXAxisNumberArray = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24];
      }


      //可绘制区域宽高
      var dcYAxisSplitLineG = rootElement.querySelector('[dctype="dcYAxisSplitLineG"]');   //轴绘制区域标签
      var dcYAxisHeight = dcYAxisSplitLineG.getBBox().height;   //Y轴绘制区域的实际总高度
      var dcYAxisWidth = dcYAxisSplitLineG.getBBox().width;   //Y轴绘制区域的实际总宽度

      for (var i = 0; i < dataKeys.length; i++) {
        if (dataKeys[i] == 'AttachedTableData') {
          // 如果是表格数据，则不处理
          continue;
        }

        var thisKey = dataKeys[i];
        var thisValue = data[thisKey] || [];
        CalculationData.Values[thisKey] = [];//初始化值对象

        //根据时间排序
        thisValue.sort(function (a, b) {
          return new Date(a.Time) - new Date(b.Time);
        });

        //根据Time去重，后面的覆盖前面的
        var newArray = [];
        for (var j = 0; j < thisValue.length; j++) {
          var thisTime = thisValue[j].Time;
          var found = false;
          for (var k = 0; k < newArray.length; k++) {
            if (newArray[k].Time == thisTime) {
              newArray[k] = thisValue[j]; // 如果有相同的Time，则覆盖前面的元素
              found = true;
              break;
            }
          }
          if (!found) {
            newArray.push(thisValue[j]); // 如果没有找到相同的Time，则添加到新数组中
          }
        }
        thisValue = newArray;


        var IsEndValueIndex = -1;
        if (AllYAxis && AllYAxis[thisKey]) {//Y轴信息存在时，绘制Y轴
          CalculationData.Values[thisKey] = [];
          thisValue.forEach(function (item, itemIndex) {
            //计算产程图标value值的点位的X,Y坐标
            var time = new Date(item.Time);
            var y = (item.Value - AllYAxis[thisKey].MinValue) / (AllYAxis[thisKey].MaxValue - AllYAxis[thisKey].MinValue) * dcYAxisHeight;

            //(当前时间-开始时间)=现在是第几个小时
            //开始时间戳
            var startTimeStamp = new Date(StartingTime).getTime();
            //当前时间戳
            var currentTimeStamp = time.getTime();
            //时间差
            var timeDiff = currentTimeStamp - startTimeStamp;
            //24小时制的时间戳
            var oneDayTimeStamp = 24 * 60 * 60 * 1000;
            //位置
            var x = (timeDiff / oneDayTimeStamp) * dcYAxisWidth;
            item["X"] = x;
            item["Y"] = dcYAxisHeight - y;
            if (item.IsEndValue) {
              IsEndValueIndex = itemIndex;
            }
          });
        }
        //只截取到IsEndValueIndex结束位置
        if (IsEndValueIndex >= 0) {
          CalculationData.Values[thisKey] = thisValue.slice(0, IsEndValueIndex + 1);
        } else {
          CalculationData.Values[thisKey] = thisValue;
        }
      }
      //开始绘制折线图
      WriterContorl_FlowChart.DrawYAxisValueChart(rootElement);
      ////////////////////////////////////////////////////////////////

      //附加表格数据
      if (dataKeys.indexOf('AttachedTableData') >= 0) {
        //可绘制区域宽高
        // var dcAttachedTableG = rootElement.querySelector('[dctype="dcAttachedTableG"]');  
        var AttachedTableData = data.AttachedTableData ? JSON.parse(JSON.stringify(data.AttachedTableData)) : [] || [];
        if (AttachedTableData && AttachedTableData.length > 0) {
          //这个循环用于先计算出每个小时对应的列索引，然后再根据索引获取最新的数据
          AttachedTableData.forEach(function (item) {
            //设置对应的行列下标，用于定位
            var time = new Date(item.Time);
            //开始时间戳
            var startTimeStamp = new Date(StartingTime).getTime();
            //当前时间戳
            var currentTimeStamp = time.getTime();
            //时间差
            var timeDiff = currentTimeStamp - startTimeStamp;
            //24小时制的时间戳
            var oneDayTimeStamp = 24 * 60 * 60 * 1000;
            //计算第几个小时
            var colIndex = Math.ceil(timeDiff / 1000 / 60 / 60);
            //第几个小时对应的列索引，且不能超过24个小时，也不能低于0个小时
            if (colIndex < 24 || colIndex > 0) {
              item['colIndex'] = colIndex;
            }

          });

          //这个循环用于获取最新的数据
          var newAttachedTableData = [];
          var indexMap = new Map();
          // 遍历已有数据，使用 Map 存储每个 colIndex 对应的最新时间数据
          for (var i = 0; i < AttachedTableData.length; i++) {
            var thisRowData = AttachedTableData[i];
            var colIndex = thisRowData.colIndex;
            // 检查是否已经存在该 colIndex
            if (indexMap.has(colIndex)) {
              //如果存在，则比较时间，取最新的数据
              var oldRowData = indexMap.get(colIndex);
              var oldTime = new Date(oldRowData.Time).getTime();
              var thisTime = new Date(thisRowData.Time).getTime();
              if (thisTime > oldTime) {
                indexMap.set(colIndex, thisRowData);
              }
            } else {
              indexMap.set(colIndex, thisRowData);
            }

          }

          // 将最新的数据添加到新数组中
          newAttachedTableData = Array.from(indexMap.values());

          //这个循环用于处理完数据的排序
          newAttachedTableData.sort(function (a, b) {
            return new Date(a.Time) - new Date(b.Time);
          });


          CalculationData['AttachedTableDataValues'] = newAttachedTableData;
          //绘制附加数据值
          WriterContorl_FlowChart.DrawAttachedTableValueChart(rootElement);


        }

      }



    }
  },
  //绘制value值的图表
  DrawYAxisValueChart: function (rootElement) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var SVGElement = rootElement.DocumentOptions.SVGElement;
    var Values = CalculationData.Values || {};
    var AllYAxis = CalculationData.AllYAxis || {};
    //根据划线区域，绘制一个同样大小的盒子，用于存放折线图数据
    for (var i = 0; i < SVGElement.length; i++) {
      var svg = SVGElement[i];
      /***************绘制Y轴值Start******************/
      var dcYAxisG = svg.dcYAxisG || null;
      var dcYAxisSplitLineG = svg.dcYAxisSplitLineG || null;
      if (dcYAxisG && dcYAxisSplitLineG) {
        var transform = dcYAxisSplitLineG.attr("transform");
        var dcFlowYAixsValueG = dcYAxisG.append("g")
          .attr("dctype", "dcFlowYAixsValueG")
          .attr("transform", transform)
          .attr("width", dcYAxisSplitLineG.node().getBBox().width || 0)
          .attr("height", dcYAxisSplitLineG.node().getBBox().height || 0);

        //绘制折线图
        for (var key in Values) {
          var thisValue = Values[key];

          if (thisValue && thisValue.length > 0) {
            let points = '';
            var symbolArr = [];
            // 生成折线的坐标字符串
            thisValue.forEach((point, pointIndex) => {

              if (point.X && point.Y) {
                //图例
                var x = point.X;
                var y = point.Y;
                var SymbolStyle = AllYAxis[key] && AllYAxis[key].SymbolStyle || 'circle';
                var SymbolSize = AllYAxis[key] && AllYAxis[key].SymbolSize || 5;
                var SymbolColorValue = AllYAxis[key] && AllYAxis[key].SymbolColorValue || '#000000';
                var SymbolBorderColorValue = AllYAxis[key] && AllYAxis[key].SymbolBorderColorValue || SymbolColorValue;
                //如果是结束点，则使用结束符号
                if (point.IsEndValue) {
                  SymbolStyle = AllYAxis[key] && AllYAxis[key].EndSymbolStyle || SymbolStyle;
                  SymbolSize = AllYAxis[key] && AllYAxis[key].EndSymbolSize || SymbolSize;
                  SymbolColorValue = AllYAxis[key] && AllYAxis[key].EndSymbolColorValue || SymbolColorValue;
                  if (AllYAxis[key] && AllYAxis[key].EndValueDownArrowVisible) {
                    // 画向下箭头↓
                    var arrowLength = AllYAxis[key].EndValueDownArrowLength || 36;
                    //先画垂直线
                    dcFlowYAixsValueG.append("line")
                      .attr("x1", x)
                      .attr("y1", y)
                      .attr("x2", x)
                      .attr("y2", y + arrowLength)
                      .attr("stroke", SymbolBorderColorValue)
                      .attr("stroke-width", 1);
                    // 再使用两条线画箭头，模拟V形状
                    dcFlowYAixsValueG.append("line")
                      .attr("x1", x - (SymbolSize)) // 左半边起点x坐标
                      .attr("y1", y + (arrowLength - (SymbolSize * 1.5)))       // 左半边起点y坐标
                      .attr("x2", x)                     // 左半边终点x坐标
                      .attr("y2", y + arrowLength)                     // 左半边终点y坐标
                      .attr("stroke", SymbolBorderColorValue)
                      .attr("stroke-width", 1);

                    dcFlowYAixsValueG.append("line")
                      .attr("x1", x + (SymbolSize)) // 右半边起点x坐标
                      .attr("y1", y + (arrowLength - (SymbolSize * 1.5)))       // 右半边起点y坐标
                      .attr("x2", x)                     // 右半边终点x坐标
                      .attr("y2", y + arrowLength)                     // 右半边终点y坐标
                      .attr("stroke", SymbolBorderColorValue)
                      .attr("stroke-width", 1);

                    //绘制文本、
                    if (AllYAxis[key] && AllYAxis[key].EndValueDownArrowText) {
                      var text = AllYAxis[key].EndValueDownArrowText;//文本内容
                      var textFontSize = AllYAxis[key].EndValueDownArrowTextFontSize || 12;//文本字体大小
                      var textX = x + SymbolSize - (textFontSize);
                      var textY = y + (arrowLength - (SymbolSize * 1.5) + (textFontSize * 1.5));
                      var textStyle = `fill:${SymbolBorderColorValue};font-size:${textFontSize}px;font-family:宋体;`;
                      dcFlowYAixsValueG.append("text")
                        .attr("x", textX)
                        .attr("y", textY)
                        .attr("style", textStyle)
                        .text(text);
                    }
                  }
                }
                // 获取图例
                var symbol = WriterContorl_FlowChart.CreateSymbols(SymbolStyle, x, y, SymbolSize, SymbolColorValue, SymbolBorderColorValue, '' + point.Time + " " + point.Value);
                symbolArr.push(symbol);

                //折线点
                points += `${x},${y} `;

              } else {
                console.log("数据不完整，无法绘制折线图", point);
                return false;
              }
            });
            // 线段样式
            var style = `fill:none;stroke:${AllYAxis[key] && AllYAxis[key].LineColor || '#000000'};stroke-width:${AllYAxis[key] && AllYAxis[key].LineWidth || 1};stroke-linecap:round;stroke-linejoin:round;`;
            // 创建polyline元素
            const polyline = document.createElementNS('http://www.w3.org/2000/svg', 'polyline');
            polyline.setAttribute('class', "dcFlowYAixsValueLine" + key);
            polyline.setAttribute('style', style);
            polyline.setAttribute('points', points.trim());
            dcFlowYAixsValueG.node().appendChild(polyline);

            //最后再绘制所有图例，1.防止被折线遮挡 2.兼容可能存在结束符的情况
            for (var j = 0; j < symbolArr.length; j++) {
              dcFlowYAixsValueG.node().appendChild(symbolArr[j]);
            }

          }
        }
      }
      /***************绘制Y轴值End******************/

      // 处理重合点样式
      var YAxis = CalculationData.Config.YAxis;

      if (YAxis.YAxisCoincidentPointSymbolStyle && YAxis.YAxisCoincidentPointSymbolStyle.length > 0) {
        // 获取所有Y轴的折线图数据
        var AllYAxisSymbols = dcFlowYAixsValueG.selectAll("[dctype='dcSymbols']");
        AllYAxisSymbols = AllYAxisSymbols ? AllYAxisSymbols.nodes() : [];

        if (AllYAxisSymbols.length > 0) {
          // 过滤重复的点
          var repeatYAxisSymbols = new Set();
          AllYAxisSymbols.forEach(function (d) {
            var thisY = d.getAttribute("y");
            var thisX = d.getAttribute("x");
            var thisSymbolxy = `${thisX},${thisY}`; //使用模板字符串

            if (repeatYAxisSymbols.has(thisSymbolxy)) {
              // 重复的点，删除
              d.remove();
              var symbol = WriterContorl_FlowChart.CreateSymbols(YAxis.YAxisCoincidentPointSymbolStyle, thisX, thisY, YAxis.YAxisCoincidentPointSymbolSize, YAxis.YAxisCoincidentPointSymbolColorValue);
              dcFlowYAixsValueG.node().appendChild(symbol);
            } else {
              repeatYAxisSymbols.add(thisSymbolxy); // 将当前点加入 Set
            }
          });
        }
      }

      //画警戒线
      this.DrawYAxisWarningLine(rootElement, svg, i);
    }
  },

  //画警戒线
  DrawYAxisWarningLine: function (rootElement, svg, i) {

    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var YAxis = Config.YAxis;
    var dcYAxisSplitLineG = svg.dcYAxisSplitLineG || null;


    //画警戒线
    var WarningLineOptions = YAxis.WarningLineOptions;
    if (WarningLineOptions && WarningLineOptions.Visible) {
      var TargetYAxisName = WarningLineOptions.TargetYAxisName;   //目标Y轴的名称
      var TargetYAxisValue = WarningLineOptions.TargetYAxisValue;  //警戒线目标Y轴值

      if (TargetYAxisName && TargetYAxisName.length > 0) {
        var YAxisValueArray = CalculationData.Values[TargetYAxisName]; //获取目标值数组
        var TargetYAxisValueItem = null;
        if (YAxisValueArray) { // 查找第一个大于等于 TargetYAxisValue 的元素
          for (let item of YAxisValueArray) {
            if (item.Value >= TargetYAxisValue) {
              TargetYAxisValueItem = item;
              break;
            }
          }
        }

        // 开始画警戒线
        if (TargetYAxisValueItem) {
          var x = TargetYAxisValueItem.X;//警戒线的X坐标
          var y = TargetYAxisValueItem.Y;//警戒线的Y坐标
          var TopX = x;
          var TopY = 0;

          var LineExtremePointObject = [];

          //警戒线向左平移量 = 第一个超出预警值Value - 警戒线Y轴值
          var LinePanLeftOffset = TargetYAxisValueItem.Value - YAxis.WarningLineOptions.TargetYAxisValue;
          x = x - (LinePanLeftOffset * CalculationData.dcAttachedTableCellWidth);

          //警戒线偏移几个小时连接到顶部
          var WarningLineConnectedToTopOffset = WarningLineOptions.WarningLineConnectedToTopOffset;
          //计算警戒线的顶点x坐标
          TopX = x + (WarningLineConnectedToTopOffset * CalculationData.dcAttachedTableCellWidth);


          //开始画警戒线
          if (dcYAxisSplitLineG) {
            var dcYAxisSplitLineGWidth = dcYAxisSplitLineG.node().getBBox().width;
            //插入新的警戒线包裹元素
            var dcYAxisWringLineBoxG = dcYAxisSplitLineG.append("g")
              .attr("dctype", "dcYAxisWringLineBoxG");

            //画警戒线
            var warningLine = dcYAxisWringLineBoxG.append("line")
              .attr("dctype", "dcYAxisWarningLine")
              .attr("x1", TopX)
              .attr("y1", TopY)
              .attr("x2", x)
              .attr("y2", y)
              .attr("stroke", WarningLineOptions.LineColorValue || "grey")
              .attr("stroke-width", WarningLineOptions.LineWidth || 1);


            // 是否将警戒线连接到底部
            var IsLineConnectedToBottom = WarningLineOptions.IsLineConnectedToBottom;
            if (IsLineConnectedToBottom) {
              var toBottomHeight = dcYAxisSplitLineG.node().getBBox().height - y;
              var stepX = warningLine.node().getBBox().width / y;
              x = x - (stepX * toBottomHeight);
              y = dcYAxisSplitLineG.node().getBBox().height;
              warningLine.attr("x2", x);
              warningLine.attr("y2", y);
            }
            //警戒线超出父元素的边界处理
            if (x < 0) {  //处理底部x超出父元素左侧边界的情况
              //超出时的宽
              var beforeWidth = warningLine.node().getBBox().width;
              x = 0;
              warningLine.attr("x2", x);
              //超出后的宽
              var afterWidth = warningLine.node().getBBox().width;
              //计算超出比例
              var scaleWidth = (beforeWidth - afterWidth) / beforeWidth;

              var beforeHeigh = warningLine.node().getBBox().height;
              y = beforeHeigh - (beforeHeigh * scaleWidth);
              warningLine.attr("y2", y);
            } else if (TopX >= dcYAxisSplitLineGWidth) { //右侧底部x超出父元素的右侧边界的情况
              var beforeWidth = warningLine.node().getBBox().width;
              TopX = dcYAxisSplitLineGWidth;
              warningLine.attr("x1", TopX);
              var afterWidth = warningLine.node().getBBox().width;
              //超出的比例
              var scaleWidth = (beforeWidth - afterWidth) / beforeWidth;
              var beforeHeigh = warningLine.node().getBBox().height;
              y = beforeHeigh * scaleWidth;
              warningLine.attr("y1", y);
            }
            if (WarningLineOptions.WarningLineExtremePointVisible) {
              //画警戒线的顶点
              LineExtremePointObject.push({
                x: TopX,
                y: TopY,
                color: WarningLineOptions.WarningLineExtremePointColorValue || "blue"
              });
              //画警戒线的低点
              LineExtremePointObject.push({
                x: x,
                y: y,
                color: WarningLineOptions.WarningLineExtremePointColorValue || "blue"
              });
            }

            //处理线
            var ProcessingLineOffsetX = WarningLineOptions.ProcessingLineOffsetX || 4;
            var offsetX = (ProcessingLineOffsetX * CalculationData.dcAttachedTableCellWidth);//处理线的x偏移量

            var ProcessingLineX = x + offsetX;
            var ProcessingLineY = y;
            var ProcessingTopLineX = TopX + offsetX;
            var ProcessingTopLineY = TopY;

            var ProcessingLine = null;   //处理线
            var ProcessingLineTopPoint = null;   //处理线端点
            var ProcessingLineBottomPoint = null;   //处理线端点

            if (!(ProcessingLineX >= dcYAxisSplitLineGWidth) && !(ProcessingTopLineX < 0)) {
              //处理线底部X没有超出父元素的右侧边界,并且处理线的顶部X坐标大于等于0时，才画处理线
              ProcessingLine = dcYAxisWringLineBoxG.append("line")
                .attr("dctype", "dcYAxisProcessingLine")
                .attr("x1", ProcessingTopLineX)
                .attr("y1", ProcessingTopLineY)
                .attr("x2", ProcessingLineX)
                .attr("y2", ProcessingLineY)
                .attr("stroke", WarningLineOptions.LineColorValue || "grey")
                .attr("stroke-width", WarningLineOptions.LineWidth || 1);
              //处理线端点
              if (WarningLineOptions.ProcessingLineExtremePointVisible) {


                //画处理线的顶点
                LineExtremePointObject.push({
                  x: ProcessingTopLineX,
                  y: ProcessingTopLineY,
                  color: WarningLineOptions.ProcessingLineExtremePointColorValue || "blue"
                });
                //画处理线的低点
                LineExtremePointObject.push({
                  x: ProcessingLineX,
                  y: ProcessingLineY,
                  color: WarningLineOptions.ProcessingLineExtremePointColorValue || "blue"
                });
              }
            }

            //处理线超出父元素的边界处理
            if (ProcessingLine) {
              if (ProcessingLineX < 0) {  //处理线底部x超出父元素左侧边界的情况
                var beforeWidth = ProcessingLine.node().getBBox().width;
                ProcessingLineX = 0;
                ProcessingLine.attr("x2", ProcessingLineX);
                var afterWidth = ProcessingLine.node().getBBox().width;
                //计算超出比例
                var scaleWidth = (beforeWidth - afterWidth) / beforeWidth;
                var beforeHeigh = ProcessingLine.node().getBBox().height;
                ProcessingLineY = beforeHeigh - (beforeHeigh * scaleWidth);
                ProcessingLine.attr("y2", ProcessingLineY);
                //端点
                ProcessingLineBottomPoint && ProcessingLineBottomPoint.attr("cx", ProcessingLineX);
                ProcessingLineBottomPoint && ProcessingLineBottomPoint.attr("cy", ProcessingLineY);

              } else if (ProcessingTopLineX >= dcYAxisSplitLineGWidth) { //右侧顶部x超出父元素的右侧边界的情况
                var beforeWidth = ProcessingLine.node().getBBox().width;
                ProcessingTopLineX = dcYAxisSplitLineGWidth;
                ProcessingLine.attr("x1", ProcessingTopLineX);
                var afterWidth = ProcessingLine.node().getBBox().width;
                //超出的比例
                var scaleWidth = (beforeWidth - afterWidth) / beforeWidth;
                var beforeHeigh = ProcessingLine.node().getBBox().height;
                ProcessingTopLineY = beforeHeigh * scaleWidth;
                ProcessingLine.attr("y1", ProcessingTopLineY);
                //端点
                ProcessingLineTopPoint && ProcessingLineTopPoint.attr("cx", ProcessingTopLineX);
                ProcessingLineTopPoint && ProcessingLineTopPoint.attr("cy", ProcessingTopLineY);
              }
            }

            //画警戒线的端点
            if (WarningLineOptions.WarningLineExtremePointVisible) {
              if (LineExtremePointObject && LineExtremePointObject.length > 0) {
                for (var s = 0; s < LineExtremePointObject.length; s++) {
                  var symbolItme = LineExtremePointObject[s];
                  var symbol = WriterContorl_FlowChart.CreateSymbols('circle', symbolItme.x, symbolItme.y, 2, symbolItme.color);
                  dcYAxisWringLineBoxG.node().appendChild(symbol);
                }
              }
            }



          }
        }
      }
    }
  },

  //绘制带位置的文本元素，用于显示附加表格数据
  DrawAttachedTableValueChart: function (rootElement) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var AttachedTableDataValues = CalculationData.AttachedTableDataValues || [];

    var SVGElement = rootElement.DocumentOptions.SVGElement;
    for (var i = 0; i < SVGElement.length; i++) {
      var svg = SVGElement[i];
      //删除原有的值
      var dcAttachedTableValueG = svg.dcAttachedTableValueG || null;
      if (dcAttachedTableValueG) {
        dcAttachedTableValueG.remove();
      }



      //附加表格包裹元素
      var dcAttachedTableG = svg.dcAttachedTableG || null;
      //附加表格写值区域的占位元素
      var dcAttachedTableBox = svg.dcAttachedTableBox || null;
      if (dcAttachedTableG && dcAttachedTableBox) {
        var dcAttachedTableValueG = dcAttachedTableG.append("g")
          .attr("dctype", "dcAttachedTableValueG");

        //绘制表格
        for (var j = 0; j < AttachedTableDataValues.length; j++) {
          var thisRowData = AttachedTableDataValues[j];
          var colIndex = thisRowData.colIndex;
          for (var k in thisRowData) {
            if (k !== 'Time' && k !== 'colIndex') {
              var targetRect = dcAttachedTableG.node().querySelector('rect[colIndex="' + colIndex + '"][rowName="' + k + '"]');
              if (targetRect) {
                var thisTableRow = null;
                CalculationData.Config.AttachedTable.TableRowData.forEach(function (item) {
                  if (k === item.Name) {
                    thisTableRow = item;
                  }
                });

                var value = thisRowData[k];
                //css
                var FontName = thisTableRow && thisTableRow.FontName || CalculationData.Config.FontName || "宋体";
                var FontSize = thisTableRow && thisTableRow.FontSize || CalculationData.Config.FontSize || 12;
                var FontBold = thisTableRow && thisTableRow.FontBold || CalculationData.Config.FontBold || false;
                var FontItalic = thisTableRow && thisTableRow.FontItalic || CalculationData.Config.FontItalic || false;
                //色彩
                var TextColorValue = thisTableRow && thisTableRow.TextColorValue || CalculationData.Config.TextColorValue || "#000000";
                var TextBackColorValue = thisTableRow && thisTableRow.TextBackColorValue || CalculationData.Config.TextBackColorValue || "#FFFFFF";


                var x = targetRect.getAttribute("x");
                var y = targetRect.getAttribute("y");
                var width = targetRect.getAttribute("width");
                var height = targetRect.getAttribute("height");

                //包裹值的元素
                var textG = dcAttachedTableValueG.append("g")
                  .attr("dctype", "attachedTableValueG")
                  .attr('style', `font-family:${FontName};font-size:${FontSize}px;font-weight:${FontBold ? 'bold' : 'normal'};font-style:${FontItalic ? 'italic' : 'normal'};text-align:${TextAlign};vertical-align:${VerticalAlign};fill:${TextColorValue};`);

                var IsCellSegment = targetRect.getAttribute("IsCellSegment");
                if (IsCellSegment && IsCellSegment === "true") {
                  //  如果是dcAttachedTableCellLine斜线拆分单元格，则分别绘制两个文本到指定位置
                  var topValue = '';
                  var bottomValue = '';
                  if (value && value.length) {
                    topValue = value[0];
                    bottomValue = value[1];
                  }
                  textG.append("text")
                    .attr("dctype", "attachedTableValueText")
                    .attr("x", 0)
                    .attr("y", CalculationData.LableHeight)
                    .attr('title', topValue + '' + bottomValue)
                    .text(topValue);
                  var bottomText = textG.append("text")
                    .attr("dctype", "attachedTableValueText")
                    .attr("y", height - CalculationData.LableHeight)
                    .attr('title', topValue + '' + bottomValue)
                    .text(bottomValue);
                  var bottomTextWidth = bottomText.node().getBBox().width;
                  bottomText.attr("x", width - bottomTextWidth);

                } else if (thisTableRow && thisTableRow.SingleLineVertical) {
                  //  如果是单行竖排，则绘制一个文本到指定位置
                  if (value && value.length > 0) {
                    for (var l = 0; l < value.length; l++) {
                      textG.append("text")
                        .attr("dctype", "attachedTableValueText")
                        .attr("x", 0)
                        .attr("y", (l + 1) * CalculationData.LableHeight)
                        .attr('title', value)
                        .text(value[l]);
                    }
                  }
                } else {
                  //获取换行后的文本数组
                  var TextObject = WriterContorl_FlowChart.GetTextWidth(value, parseFloat(width), parseFloat(height), FontSize, FontName, FontBold);
                  //换行后的文本数组
                  var textArr = TextObject && TextObject.content || [];
                  //字体可能会被缩小
                  var scaleFontSize = TextObject.fontSize || FontSize || 12;

                  if (value && value.length > 0) {
                    //绘制文本
                    for (var n = 0; n < textArr.length; n++) {
                      textG.append("text")
                        .attr("dctype", "attachedTableValueText")
                        .attr("style", `font-size:${scaleFontSize}px;`)
                        .attr("y", (n + 1) * scaleFontSize)
                        .attr('title', value)
                        .text(textArr[n]);
                    }
                  }
                }


                //对齐方式
                var TextAlign = thisTableRow && thisTableRow.TextAlign || 'center';
                var VerticalAlign = thisTableRow && thisTableRow.VerticalAlign || 'middle';
                var textGBBox = textG.node().getBBox();
                var textGX = parseFloat(x);
                var textGY = parseFloat(y);

                //非拆分的单元格，支持调整文本位置水平垂直居中
                if (!thisRowData.IsCellSegment) {
                  if (TextAlign == 'center') {
                    textGX = parseFloat(x) + ((parseFloat(width) - textGBBox.width) / 2);
                  } else if (TextAlign == 'right') {
                    textGX = parseFloat(x) + (parseFloat(width) - textGBBox.width);
                  } else {
                    textGX = parseFloat(x);
                  }
                  if (VerticalAlign == 'middle') {
                    textGY = parseFloat(y) + ((parseFloat(height) - textGBBox.height) / 2);
                  } else if (VerticalAlign == 'bottom') {
                    textGY = parseFloat(y) + (parseFloat(height) - textGBBox.height);
                  } else {
                    textGY = parseFloat(y);
                  }
                }


                textG.attr("transform", "translate(" + (textGX) + "," + textGY + ")");

              }
            }
          }
        }
        svg.dcAttachedTableValueG = dcAttachedTableValueG;
      }
    }

  },
  //绘制svg元素
  DrawSvg: function (rootElement) {
    let that = this;
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var svgEle = rootElement.DocumentOptions.SVGElement;
    //查找到pageContainer元素 //清空包裹元素所有的子元素,重新绘制
    var pageContainer = rootElement.pageContainer;
    pageContainer.innerHTML = "";

    //创建span元素并展示title
    var titleSpan = document.createElement("span");
    titleSpan.id = "dc_titleSpan";
    titleSpan.style.display = "none";
    titleSpan.style.padding = "3px 10px";
    titleSpan.style.backgroundColor = "#fff";
    titleSpan.style.borderRadius = "3px";
    titleSpan.style.fontFamily = "宋体";
    titleSpan.style.fontSize = "12px";
    titleSpan.style.position = "absolute";
    titleSpan.style.boxShadow = "0px 0px 5px rgba(0, 0, 0, 0.3)";
    pageContainer.appendChild(titleSpan);

    //缩放
    CalculationData.ZoomRate = rootElement.getAttribute("zoomrate");
    CalculationData.ZoomRate = parseFloat(CalculationData.ZoomRate);
    CalculationData.ZoomRate = isNaN(CalculationData.ZoomRate)
      ? 1
      : CalculationData.ZoomRate;
    CalculationData.ZoomRate = CalculationData.ZoomRate
      ? CalculationData.ZoomRate
      : 1;
    //总页数 最小为1
    if (
      !CalculationData.Config.TotalPageNumber ||
      CalculationData.Config.TotalPageNumber <= 0
    ) {
      CalculationData.Config.TotalPageNumber = 1;
    }

    var PageSettings = CalculationData.Config.PageSettings;
    //绘制svg
    for (var i = 0; i < CalculationData.Config.TotalPageNumber; i++) {
      //最外侧svg
      var svg = d3
        .create("svg")
        .attr("dctype", "page")
        .attr(
          "viewBox",
          "0 0 " + PageSettings.PaperWidth + " " + PageSettings.PaperHeight
        ) //增加一个视口
        .attr("width", PageSettings.PaperWidth)
        .attr("height", PageSettings.PaperHeight) //如果是新生儿体温单就需要用保留的高度
        .attr("native-width", PageSettings.PaperWidth)
        .attr("native-height", PageSettings.PaperHeight) //如果是新生儿体温单就需要用保留的高度
        .attr(
          "style",
          `border-radius: 6px;margin: 6px auto; vertical-align: top;background-color: window;border: 1px solid #E6E6E6; cursor: default;display: block;font-family:${CalculationData.Config.FontName || '宋体'};font-size:${CalculationData.Config.FontSize}px;`
        ); //box-sizing:border-box;

      if (svgEle[i] == null) {
        svgEle[i] = {};
      }
      svgEle[i].page = svg;
      var thisSvgEle = svg.node();
      pageContainer.appendChild(thisSvgEle);

      //在此处计算出每个svg的top值
      thisSvgEle.setAttribute("dcTop", CalculationData.SvgScrollTop + 7);
      CalculationData.SvgScrollTop += thisSvgEle.clientHeight + 7;

      //设置注册码信息
      var aboutMessage = "都昌信息科技有限公司";

      //获取新的注册信息
      if (rootElement.TemperatureRegisterCode) {
        const regex = /\[用户名:(.*?)\]/;
        const match = regex.exec(rootElement.TemperatureRegisterCode);
        aboutMessage =
          match && match.length > 1
            ? match[1]
            : rootElement.TemperatureRegisterCode;
      }
      var tooltip = svg.append("g").style("pointer-events", "none");
      //绘制文本
      tooltip
        .append("text")
        .attr("style", `font-family:宋体;font-size:9pt;line-height: 1`)
        .attr("dctype", "typesign")
        .attr("id", "dc_typesign" + i) //增加id属性，方便后续修改注册码
        .attr("x", 12)
        .attr("y", 12)
        .text(aboutMessage);
      CalculationData['TypesignHeight'] = tooltip.node().getBBox().height;
      thisSvgEle.addEventListener("pointermove", function (e) {
        var targetEle = e.target;
        var hasTitle = targetEle.getAttribute("title");
        if (hasTitle && hasTitle.length > 0) {
          var currentTarget = e.currentTarget;

          var pagePosition = rootElement.pageContainer.getBoundingClientRect();
          var svgPosition = currentTarget.getBoundingClientRect();
          titleSpan.style.display = "block";
          titleSpan.style.top =
            e.offsetY +
            30 +
            (svgPosition.y - pagePosition.y) +
            rootElement.pageContainer.scrollTop +
            "px";
          titleSpan.style.left =
            e.offsetX + (svgPosition.x - pagePosition.x) + "px";
          titleSpan.innerText = hasTitle;
        } else {
          titleSpan.style.display = "none";
        }
      });
      //体温单点击事件
      thisSvgEle.addEventListener("click", function (e) {
        console.log("点击了svg", e.target);
        // var dcType = rootElement.getAttribute("dctype");//用于判断是否为设计器模式
        // if (dcType === "DCFlowDesignControlForWASM") {
        //     //设计器模式
        //     if (CalculationData.UIDElePositon && CalculationData.UIDElePositon.length > 0) {
        //         var hasEle = CalculationData.UIDElePositon.find((item, index) => {
        //             var y1 = item.Positon.svgTop;
        //             var x1 = item.Positon.svgLeft;
        //             var y2 = y1 + item.Positon.height;
        //             var x2 = x1 + item.Positon.width;
        //             if (e.offsetX >= x1 && e.offsetX <= x2 && e.offsetY >= y1 && e.offsetY <= y2) {
        //                 WriterControl_DrawFu.MoveBorderRect(rootElement, item.UID);
        //                 var thisProp = rootElement.GetInternalProperties(item.UID);
        //                 rootElement.InnerRaiseEvent("EventStructureClick", thisProp, item.Type);
        //                 return true;
        //             }
        //         });
        //         if (!hasEle) {
        //             WriterControl_DrawFu.MoveBorderRect(rootElement);
        //             //如果存在在返回全局的属性
        //             var thisProp = rootElement.GetDocumentConfigProperties();
        //             rootElement.InnerRaiseEvent("EventStructureClick", thisProp, "DocumentConfig");
        //         }
        //     }
        // } else {
        //     //非设计器模式
        //     // [DUWRITER5_0-3330] lxy 20240813 点击背景线时，触发画点区域的点击事件。新增EventBackgroundLineClick事件
        //     let dcbgLineG = this.querySelector('[dctype=dcbgLineG]');
        //     if (dcbgLineG) {
        //         let dcbgLineGRect = dcbgLineG.getBoundingClientRect();
        //         let dcbgLineGRectWidth = dcbgLineGRect.width;//矩形的宽度
        //         let dcbgLineGRectHeight = dcbgLineGRect.height;//矩形的高度
        //         //背景线区域的偏移值
        //         let dcbgLineGTransform = dcbgLineG.getAttribute('transform');
        //         if (dcbgLineGTransform) {
        //             const match = dcbgLineGTransform.match(/translate\((\d+\.?\d*),(\d+\.?\d*)\)/);
        //             if (match) {
        //                 const x = Number(match[1]) || 0;
        //                 const y = Number(match[2]) || 0;
        //                 if (e.offsetX >= x && e.offsetX <= x + dcbgLineGRectWidth && e.offsetY >= y && e.offsetY <= y + dcbgLineGRectHeight) {
        //                     // //console.log('点击了背景线');//触发事件EventBackgroundLineClick
        //                     rootElement.InnerRaiseEvent("EventBackgroundLineClick");
        //                     return;
        //                 }
        //             }
        //         }
        //     }
        // }
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
            that.SetZoomRate(rootElement, hasZoomRate);
          }, 100);
          event.stopPropagation();
          event.preventDefault();
          return false;
        }
      });

      //调用各层的绘制方法
      that.DrawTitle(rootElement, svg, svgEle, i);
      that.DrawPageNumber(rootElement, svg, svgEle, i);
      that.DrawFooterDescription(rootElement, svg, svgEle, i);
      that.DrawHeadLable(rootElement, svg, svgEle, i);
      that.DrawAttachedTable(rootElement, svg, svgEle, i);
      that.DrawYAxisInfos(rootElement, svg, svgEle, i);
      that.DrawLabel(rootElement, svg, svgEle, i);

      //监听元素
      var targetContent = svg.node();
      // 观察器的配置（需要观察什么变动）attributes: true,
      const config = {
        childList: true,
        subtree: true,
        characterData: true,
        characterDataOldValue: true,
      };
      // 当观察到变动时执行的回调函数
      const callback = function (mutationsList, observer) {
        if (mutationsList && mutationsList.length > 0) {
          for (var j = 0; j < mutationsList.length; j++) {
            if (
              mutationsList[j].removedNodes &&
              mutationsList[j].removedNodes.length > 0
            ) {
              for (var z = 0; z < mutationsList[j].removedNodes.length; z++) {
                var thisRemoveNode = mutationsList[j].removedNodes[z];
                //为纯文本
                if (
                  thisRemoveNode.nodeType == 1 &&
                  thisRemoveNode.getAttribute("dctype") == "typesign"
                ) {
                  alert(window.__DCSR.DeleteRegister);
                  d3.select(mutationsList[j].target)
                    .append("text")
                    .attr(
                      "style",
                      `font-family:宋体;font-size:9pt;line-height: 1`
                    )
                    .attr("dctype", "typesign")
                    .attr("id", "dc_typesign" + i) //增加id属性，方便后续修改注册码
                    .attr("x", 12)
                    .attr("y", 12)
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

      thisSvgEle.style.zoom = CalculationData.ZoomRate;
    }
  },
  //绘制标题
  DrawTitle: function (rootElement, svg, svgEle, i) {
    var { CalculationData } = rootElement.DocumentOptions;
    var { Config } = CalculationData;
    var { PageSettings } = Config;

    // 大标题包裹元素
    var dcTitleG = svg.append("g").attr("dctype", "dcTitleG");

    // 绘制文本
    var bigTitle = Config.BigTitle;
    if (bigTitle && bigTitle.length > 0) {
      var textStyle = `font-family:${Config.BigTitleFontName || Config.FontName || '宋体'}; 
                         font-weight:${Config.BigTitleFontBold ? "bold" : ""}; 
                         font-size:${Config.BigTitleFontSize}px;`;

      var dcTitle = dcTitleG.append("text")
        .attr("style", textStyle)
        .attr("dctype", "dcTitle")
        .attr("x", 0)
        .attr("y", (CalculationData.TypesignHeight || 0) + PageSettings.TopMargin + Config.BigTitleMarinTop)
        .text(bigTitle);
      //修正对齐方式，使其居中
      var textWidth = dcTitle.node().getBBox().width;
      var x = (PageSettings.PaperWidth - textWidth) / 2;
      dcTitle.attr("x", x);
      // 修正大标题包裹元素高度
      CalculationData.TitleHeight = dcTitle.node().getBBox().height;
    }

    svgEle[i].dcTitleG = dcTitleG;
  },

  // 绘制页眉
  DrawHeadLable: function (rootElement, svg, svgEle, i) {
    const CalculationData = rootElement.DocumentOptions.CalculationData;
    const Config = CalculationData.Config;
    const PageSettings = Config.PageSettings;
    const HeaderLabelWidth = PageSettings.InnerWidth - CalculationData.dcLeftYAxisTotalWidth - CalculationData.dcRightYAxisTotalWidth;
    var startX = PageSettings.LeftMargin + CalculationData.dcLeftYAxisTotalWidth;
    var startY = (CalculationData.TypesignHeight || 0) + PageSettings.TopMargin + Config.BigTitleMarinTop + Config.HeaderLabelsMarginTop;


    var dcHeaderLabelsGBox = svg.append("g")
      .attr("dctype", "dcHeaderLabelsGBox")
      .attr("transform", `translate(${startX},${startY})`);

    var HeaderLableList = Config.HeaderLabels;
    this.DrawHeaderLabelsTool(CalculationData, dcHeaderLabelsGBox, HeaderLableList, HeaderLabelWidth);


    CalculationData.HeaderLabelsHeight = dcHeaderLabelsGBox.node().getBBox().height + Config.YAxisMarginTop + 5;
  },

  //绘制页码
  DrawPageNumber: function (rootElement, svg, svgEle, i) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;

    //页码包裹元素
    var dcPageNumberG = svg
      .append("g")
      .attr("dctype", "dcPageNumberG")
      .attr(
        "transform",
        `translate(${PageSettings.LeftMargin},${PageSettings.PaperHeight - PageSettings.BottomMargin
        })`
      );
    svgEle[i].dcPageNumberG = dcPageNumberG;
    //页码元素

    // 生成页码文本
    var pageNumber = Config.PageIndexText.replace("[%pageindex%]", "").replace(
      "[%pagecount%]",
      ""
    );

    var pageNumberEle = dcPageNumberG
      .append("text")
      .attr("style", `text-anchor: middle;line-height: 1`)
      .attr("dctype", "dcPageNumber")
      .attr("x", PageSettings.InnerWidth / 2)
      .attr("y", 0)
      .text(pageNumber);

    //计算不带数字时，每一个文本的宽度
    //获取页码的高度
    CalculationData.LableHeight = dcPageNumberG.node().getBBox().height;
    //计算单个字符宽度
    CalculationData.LableWidth =
      dcPageNumberG.node().getBBox().width / pageNumber.length;

    // 获取当前页码和总页码
    var currentPageIndex = i + 1;
    var totalPageNumber = Config.TotalPageNumber || 1;
    //将数字写入
    pageNumber = Config.PageIndexText.replace(
      "[%pageindex%]",
      currentPageIndex
    ).replace("[%pagecount%]", totalPageNumber);
    pageNumberEle.text(pageNumber);
    //记录页码的实际高度
    CalculationData["PageNumberHeight"] = dcPageNumberG.node().getBBox().height;
  },
  //绘制说明元素
  DrawFooterDescription: function (rootElement, svg, svgEle, i) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;
    //说明元素包裹元素
    var dcFooterDescriptionG = svg
      .append("g")
      .attr("dctype", "dcFooterDescriptionG")
      .attr(
        "transform",
        `translate(${PageSettings.LeftMargin},${PageSettings.PaperHeight -
        PageSettings.BottomMargin -
        CalculationData.PageNumberHeight
        })`
      );
    svgEle[i].dcFooterDescriptionG = dcFooterDescriptionG;
    dcFooterDescriptionG
      .append("text")
      .attr("dctype", "dcFooterDescription")
      .attr("x", 0)
      .attr("y", 0)
      .text(Config.FooterDescription);

    //记录说明元素的高度
    CalculationData["FooterDescriptionHeight"] = dcFooterDescriptionG
      .node()
      .getBBox().height;
  },
  //绘制文本标签
  DrawLabel: function (rootElement, svg, svgEle, i) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;
    var Labels = Config.Labels;
    //文本标签包裹元素
    var dcLabelG = svg
      .append('g')
      .attr('dctype', "dcLabelsG")
      .attr(
        'transform',
        `translate(${PageSettings.LeftMargin},${PageSettings.TopMargin})`
      );
    svgEle[i].dcLabelG = dcLabelG;


    //绘制文本
    if (Labels && Labels.length > 0) {
      //由于存在图片和文本的区别,使用外部循环而不使用selectAll
      for (var j = 0; j < Labels.length; j++) {
        var thisLabel = Labels[j];

        var uid = 'dcLabels_' + j;
        thisLabel.UID = uid;
        var thisLabelG = dcLabelG.append('g')
          .attr("uid", uid)
          .attr(
            'transform',
            `translate(${thisLabel.Left},${thisLabel.Top})`
          );
        //文本标签的位置单独计算
        var width = parseFloat(thisLabel.Width);
        var height = parseFloat(thisLabel.Height);
        var top = parseFloat(thisLabel.Top);
        var left = parseFloat(thisLabel.Left);
        var FontSize = parseFloat(thisLabel.FontSize) || 12;
        var FontName = thisLabel.FontName || '宋体';
        var FontBold = thisLabel.FontBold || false;

        thisLabelG.append('rect')
          .attr('width', width)
          .attr('height', height)
          .attr('fill', `${thisLabel.BackColorValue ? thisLabel.BackColorValue : "none"}`)
          .attr('stroke', `${thisLabel.ShowBorder ? "black" : "none"}`);



        if (thisLabel.ImageDataBase64String && thisLabel.ImageDataBase64String.length > 0) {
          thisLabelG.append('image')
            .attr('dctype', 'dcLables')
            .attr('x', left)
            .attr('y', top)
            .attr('width', width)
            .attr('height', height)
            .attr('href', `data:application/pdf;base64,${thisLabel.ImageDataBase64String}`);
        } else {
          if (thisLabel.SymbolVisible && thisLabel.SymbolStyle) {
            //存在图例符号
            var SymbolColorValue = thisLabel.SymbolColorValue || "red";
            var SymbolBorderColorValue = thisLabel.SymbolBorderColorValue || null;
            var SymbolSize = thisLabel.SymbolSize || 5;
            var SymbolStyle = thisLabel.SymbolStyle || "SolidCircle";
            var symbol = WriterContorl_FlowChart.CreateSymbols(SymbolStyle, 0, -SymbolSize, SymbolSize, SymbolColorValue, SymbolBorderColorValue || SymbolColorValue, '');
            if (symbol.tagName) { //判断是否是一个dom标签
              thisLabelG.node().appendChild(symbol);
            }
          }
          if (thisLabel.MultiLine) {
            //MultiLine属性,多行文本
            let TextObject = WriterContorl_FlowChart.GetTextWidth(thisLabel.Text, width, height, FontSize, FontName, FontBold, false);
            let TextContent = TextObject && TextObject.content || [];
            //重新生成换行的文本标签
            var thisTextEleG = thisLabelG.append('g');
            for (var index = 0; index < TextContent.length; index++) {
              var data = TextContent[index];
              thisTextEleG.append('text')
                .text(data)
                .attr('style', `font-family:${FontName} ;font-size:${FontSize}px;font-weight:${FontBold ? 900 : 0};text-decoration: ${thisLabel.FontUnderline ? 'underline' : 'none'};color:${thisLabel.ColorValue};font-style:${thisLabel.FontItalic ? 'italic' : 'none'};`)
                .attr('dctype', 'dcLables')
                .attr('fill', `${thisLabel.ColorValue ? thisLabel.ColorValue : "black"}`)
                .attr('x', 0)
                .attr('y', FontSize * (index + 1))
                .attr('xml:space', 'preserve');
            }



            // //垂直对齐方式LineAlignment Near(默认) Center Far
            // var thisTextHeight = thisTextEleG.node().getBBox().height;
            // if (thisLabel.LineAlignment == "Center") {
            //   thisTextEleG.attr('transform', `translate(0,${(height - thisTextHeight) / 2})`);
            // } else if (thisLabel.LineAlignment == "Far") {
            //   thisTextEleG.attr('transform', `translate(0,${(height - thisTextHeight)})`);
            // }


            // //查看是否需要显示
            // //计算高度
            // var textReatHeight = rect.node().getBBox().height;
            // var textReatY = rect.node().getBBox().y;
            // var allTspan = textEle.selectAll("tspan").nodes();
            // for (var z = 0; z < allTspan.length; z++) {
            //   var thisTspanHeight = allTspan[z].getBBox().height;
            //   var thisTspanY = allTspan[z].getBBox().y;
            //   if (thisTspanY - textReatY + thisTspanHeight > textReatHeight) {
            //     allTspan[z].remove();
            //   }
            // }

          } else {
            //单行文本
            thisLabelG.append('text')
              .text(returnText)
              .attr('style', `font-family:${FontName} ;font-size:${FontSize}px;font-weight:${FontBold ? 900 : 0};text-decoration: ${thisLabel.FontUnderline ? 'underline' : 'none'};color:${thisLabel.ColorValue};font-style:${thisLabel.FontItalic ? 'italic' : 'none'};`)
              .attr('dctype', 'dcLables')
              .attr('parameterName', `${thisLabel.ParameterName}`)
              .attr('fill', `${thisLabel.ColorValue ? thisLabel.ColorValue : "black"}`)
              .attr('xml:space', 'preserve')
              .attr('y', 0)
              .attr('x', thisLabelG.node().getBBox().width);
          }

          //垂直对齐方式VerticalAlign top(默认) middle bottom
          var VerticalAlign = thisLabel.VerticalAlign || "top";
          VerticalAlign = VerticalAlign.toLowerCase().trim();
          //水平对齐方式TextAlign  left(默认) center right
          var TextAlign = thisLabel.TextAlign || "left";
          TextAlign = TextAlign.toLowerCase().trim();
          var thisLabelGChildrens = thisLabelG.node().children;

          for (var k = 0; k < thisLabelGChildrens.length; k++) {
            var x = 0;
            var y = 0;
            var thisChild = thisLabelGChildrens[k];
            if (thisChild.tagName == "g") {
              //计算x坐标
              if (TextAlign == "center") {
                x += (width - thisChild.getBBox().width) / 2;
              } else if (TextAlign == "right") {
                x += (width - thisChild.getBBox().width);
              }
              //计算y坐标
              if (VerticalAlign == "middle") {
                y += (height - thisChild.getBBox().height) / 2;
              } else if (VerticalAlign == "bottom") {
                y += (height - thisChild.getBBox().height);
              }
              thisChild.setAttribute('transform', `translate(${x},${y})`);


              //根据文本长度再重置一次symbol的Y位置
              var symbol = thisLabelG.select(`[dctype="dcSymbols"]`);
              if (symbol && symbol.node()) {
                //如果存在偏移位置，则以偏移位置为基础
                var symbolHeigth = symbol.node().getBBox().height;
                var symbolWidth = symbol.node().getBBox().width;
                x += (thisChild.getBBox().width - symbolWidth) / 2;
                symbol.node().setAttribute('transform', `translate(${1 + x + SymbolSize / 2},${y - symbolHeigth})`);
              }
            }
          }

        }
      }
    }
  },
  //绘制附加表格
  DrawAttachedTable: function (rootElement, svg, svgEle, i) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;
    var AttachedTable = Config.AttachedTable || {};

    //附加表格包裹元素
    var dcAttachedTableG = svg
      .append("g")
      .attr("dctype", "dcAttachedTableG");

    //获取附加表格Y开始的点
    var dcAttachedTablYStartPositon =
      PageSettings.PaperHeight -
      PageSettings.BottomMargin -
      (CalculationData.PageNumberHeight || 0) -
      (CalculationData.FooterDescriptionHeight || 0) -
      (CalculationData.dcAttachedTableTotalHeight || 0) -
      (Config.AttachedTableMarginTop || 0);
    //获取附加表格Y开始的点
    CalculationData['dcAttachedTablYStartPositon'] = dcAttachedTablYStartPositon;

    //获取附加表格X开始的点
    var dcAttachedTableXStartPositon =
      PageSettings.LeftMargin + CalculationData.dcLeftYAxisTotalWidth;
    //获取附加表格X开始的点
    CalculationData['dcAttachedTableXStartPositon'] = dcAttachedTableXStartPositon;



    /**********表格线*********/
    this.DrawAttachedTableLine(rootElement, svg, svgEle, i, dcAttachedTableG);

    /**********绘制附加表格左侧Y轴*********/
    this.DrawAttachedTableHeaderLeftYAxis(rootElement, svg, svgEle, i, dcAttachedTableG);

    /**********绘制附加表格顶部的标题*********/
    //开始绘制带边框的方形盒子
    var dcAttachedTableBox = dcAttachedTableG
      .append("rect")
      .attr("dctype", "dcAttachedTableBox")
      .attr("x", dcAttachedTableXStartPositon)
      .attr("y", dcAttachedTablYStartPositon)
      .attr("width", CalculationData.dcAttachedTableTotalWidth)
      .attr("height", CalculationData.dcAttachedTableTotalHeight)
      .attr("fill", "none")
      .attr("stroke", AttachedTable.TableHeaderCellBorderCorlorValue || '#606266') // 设置边框颜色
      .attr("stroke-width", AttachedTable.TableHeaderCellBorderWidth || 1); // 设置边框宽度
    svgEle[i].dcAttachedTableBox = dcAttachedTableBox;
    this.DrawAttachedTableHeader(rootElement, svg, svgEle, i, dcAttachedTableBox, dcAttachedTableG);

    //高度
    CalculationData.dcAttachedTableHeight = dcAttachedTableG.node().getBBox().height;

    svgEle[i].dcAttachedTableG = dcAttachedTableG;

  },
  //绘制Y轴
  DrawYAxisInfos: function (rootElement, svg, svgEle, i) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;
    var YAxis = Config.YAxis;

    //Y轴X开始的位置
    var dcYAxisX = PageSettings.LeftMargin + CalculationData.dcLeftYAxisTotalWidth;
    //Y轴Y开始的位置
    var dcYAxisY = (CalculationData.TypesignHeight || 0) + (PageSettings.TopMargin || 0) + (Config.BigTitleMarinTop || 0) + (Config.HeaderLabelsMarginTop || 0) + (CalculationData.HeaderLabelsHeight || 0) + (Config.YAxisMarginTop || 0);

    // Y轴总高度 = 附加表格的高度 - 附加表格的标题高度 - Y轴开始的位置
    var dcYAxisTotalHeight = 0;
    if (CalculationData.dcAttachedTablYStartPositon) {
      dcYAxisTotalHeight = CalculationData.dcAttachedTablYStartPositon - (Config.AttachedTable.AttachedTableHeaderLabelHeight || 0) - (dcYAxisY || 0);
    }
    // 减去底下表格和当前的间距
    if (Config.AttachedTableMarginTop) {
      dcYAxisTotalHeight -= Config.AttachedTableMarginTop;
    }
    // 减去底部刻度文本的高度
    if (YAxis.SplitXAxisNumberArrayHeight) {
      dcYAxisTotalHeight -= YAxis.SplitXAxisNumberArrayHeight;
    }
    //存一次高度
    CalculationData['dcYAxisTotalHeight'] = dcYAxisTotalHeight;

    //Y轴内部表格区域的宽度
    var dcYAxisTotalWidth = PageSettings.InnerWidth
      - (CalculationData.dcLeftYAxisTotalWidth || 0)
      - (CalculationData.dcRightYAxisTotalWidth || 0);


    //Y轴包裹元素
    var DrawYAxisG = svg
      .append("g")
      .attr("dctype", "dcYAxisG")
      .attr(
        "transform",
        `translate(${dcYAxisX},${dcYAxisY})`
      );
    svgEle[i].dcYAxisG = DrawYAxisG;

    //画外层边框
    DrawYAxisG.append("rect")
      .attr("x", 0)
      .attr("y", 0)
      .attr("width", dcYAxisTotalWidth)
      .attr("height", dcYAxisTotalHeight)
      .attr("fill", "none")
      .attr("stroke", "black")
      .attr("stroke-width", 1);

    //画表头
    var XAxisForHeaderLableList = YAxis.XAxisForHeaderLableList;
    this.DrawHeaderLabelsTool(CalculationData, DrawYAxisG, XAxisForHeaderLableList, dcYAxisTotalWidth);
    CalculationData['dcYAxisHeaderLabelsGHeight'] = (YAxis.XAxisForHeaderLableBottomMargin || 0);
    // 重新计算高度
    var alldcYAxisHeaderLabelsG = DrawYAxisG.selectAll('[dctype="dcYAxisHeaderLabelsG"]');
    var dcYAxisHeaderLabelsGHeight = 0;
    if (alldcYAxisHeaderLabelsG && alldcYAxisHeaderLabelsG.nodes().length > 0) {
      // 计算每个标签的位置
      for (var j = 0; j < alldcYAxisHeaderLabelsG.nodes().length; j++) {
        var labelsG = alldcYAxisHeaderLabelsG.nodes()[j];
        var labelsTotalHeight = labelsG.getBBox().height;
        //整合标签高度
        CalculationData['dcYAxisHeaderLabelsGHeight'] += (labelsTotalHeight + ((j + 1) * 5));
      }
    }

    //画第一条横线
    if (CalculationData['dcYAxisHeaderLabelsGHeight']) {
      DrawYAxisG.append("line")
        .attr("x1", 0)
        .attr("y1", CalculationData['dcYAxisHeaderLabelsGHeight'])
        .attr("x2", dcYAxisTotalWidth)
        .attr("y2", CalculationData['dcYAxisHeaderLabelsGHeight'])
        .attr("stroke", "black")
        .attr("stroke-width", 1);
    }

    //画分割线
    this.DrawYAxisSplitLine(rootElement, svg, svgEle, i, DrawYAxisG);
    //画左右两侧刻度
    this.DrawYAxisLeftRightSplitLine(rootElement, svg, svgEle, i, DrawYAxisG);
    //画刻度数字
    this.DrawYAxisBottomSplitXAxisNumberArray(rootElement, svg, svgEle, i, DrawYAxisG);

  },

  // 绘制Y轴的表头
  DrawHeaderLabelsTool: function (CalculationData, DrawYAxisG, XAxisForHeaderLableList = [], dcYAxisTotalWidth = 100) {
    //先做一次数据分组
    var XAxisForHeaderLableListArray = [];
    if (XAxisForHeaderLableList && XAxisForHeaderLableList.length > 0) {
      for (var j = 0; j < XAxisForHeaderLableList.length; j++) {
        var item = XAxisForHeaderLableList[j];
        if (XAxisForHeaderLableListArray[item.GroupIndex]) {
          XAxisForHeaderLableListArray[item.GroupIndex].push(item);
        } else {
          XAxisForHeaderLableListArray[item.GroupIndex] = [item];
        }
      }
    }

    //开始绘制
    if (XAxisForHeaderLableListArray && XAxisForHeaderLableListArray.length > 0) {
      //外层包裹元素
      for (var j = 0; j < XAxisForHeaderLableListArray.length; j++) {
        var thisHeaderLables = XAxisForHeaderLableListArray[j];

        if (thisHeaderLables && thisHeaderLables.length > 0) {

          var dcYAxisHeaderLabelsG = DrawYAxisG.append("g")
            .attr("dctype", "dcYAxisHeaderLabelsG")
            .attr(
              "transform",
              `translate(0,${((j + 1) * CalculationData.LableHeight) + 5})`
            );

          for (var k = 0; k < thisHeaderLables.length; k++) {
            var thisHeaderLable = thisHeaderLables[k];
            console.log(thisHeaderLable);
            var FontBold = thisHeaderLable.FontBold || false;
            var FontName = thisHeaderLable.FontName || "宋体";
            var FontSize = thisHeaderLable.FontSize || 12;


            let titleValue = thisHeaderLable.Value;
            dcYAxisHeaderLabelsG.append("text")
              .attr('style', `font-family:${FontName} ;font-size:${FontSize}px;font-weight:${FontBold ? 900 : 0};`)
              .attr("dctype", "dcYAxisHeaderLabel")
              .attr("x", dcYAxisHeaderLabelsG.node().getBBox().width)
              .attr("y", 0)
              .html(`${thisHeaderLable.Title}${thisHeaderLable.SeperatorChar ? thisHeaderLable.SeperatorChar : ":"} ${titleValue}`);
            var thisWidth = dcYAxisHeaderLabelsG.node().getBBox().width;
            var x = (dcYAxisTotalWidth - thisWidth) / (thisHeaderLables.length - 1).toFixed(2);
            // 需要换行展示时
            if (x < 0) {
              //对当前数组进行重组
              var allChild = dcYAxisHeaderLabelsG.node().childNodes;
              var allWidth = 0;
              for (var l = 0; l < allChild.length; l++) {
                allWidth += allChild[l].getBBox().width;
                if (allWidth > (dcYAxisTotalWidth)) {
                  //截断数组
                  var newArr = thisHeaderLables.slice(l) || [];
                  XAxisForHeaderLableListArray[j] = XAxisForHeaderLableListArray[j].slice(0, l);

                  dcYAxisHeaderLabelsG.remove();
                  j--;
                  XAxisForHeaderLableListArray.splice(j + 1, 0, newArr);
                  break;
                }
              }
              break;
            }
          }

        }
      }

      // 重新计算每个标签的位置, 平均分配宽度
      var dcYAxisHeaderLabelsGArr = DrawYAxisG.selectAll('[dctype="dcYAxisHeaderLabelsG"]').nodes();
      if (dcYAxisHeaderLabelsGArr && dcYAxisHeaderLabelsGArr.length > 0) {
        // 计算每个标签的位置
        for (var j = 0; j < dcYAxisHeaderLabelsGArr.length; j++) {
          var labelsG = dcYAxisHeaderLabelsGArr[j];
          var labelsTotalWidth = labelsG.getBBox().width;
          var allText = labelsG.querySelectorAll("[dctype='dcYAxisHeaderLabel']");
          //平均分配宽度
          var x = (dcYAxisTotalWidth - labelsTotalWidth) / (allText.length - 1);

          if (allText && allText.length > 0) {
            for (var k = 0; k < allText.length; k++) {
              var text = allText[k];
              var textX = text.getAttribute("x") ? parseInt(text.getAttribute("x")) : 0;
              if (k === allText.length - 1) {
                text.setAttribute("x", (textX + (x * k)) - 2);
              } else if (k === 0) {
                text.setAttribute("x", 2);
              } else if (k > 0) {
                text.setAttribute("x", textX + (x * k));
              }
            }
          }


        }
      }

    }


  },
  // 画分割线
  DrawYAxisSplitLine: function (rootElement, svg, svgEle, i, DrawYAxisG) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;
    var YAxis = Config.YAxis;

    //盒子宽度
    var YAxisTotalWidth = PageSettings.InnerWidth - CalculationData.dcLeftYAxisTotalWidth - CalculationData.dcRightYAxisTotalWidth;
    // 盒子高度
    var YAxisTotalHeight = CalculationData.dcYAxisTotalHeight;

    if (CalculationData.dcYAxisHeaderLabelsGHeight) {
      YAxisTotalHeight -= CalculationData.dcYAxisHeaderLabelsGHeight;
    } else {
      CalculationData.dcYAxisHeaderLabelsGHeight = 0;
    }

    // 画分割线最外层包裹盒子
    var DrawYAxisSplitLineG = DrawYAxisG.append("g")
      .attr("dctype", "dcYAxisSplitLineG")
      .attr(
        "transform",
        `translate(0,${CalculationData.dcYAxisHeaderLabelsGHeight})`
      );

    // 画纵向分割线
    var SplitYAxisNumber = (YAxis.SplitYAxisNumber || 10);
    // 计算每条线之间的距离，也就是每个各自的宽度
    var gridHeight = YAxisTotalHeight / SplitYAxisNumber;
    CalculationData['dcYAxisSplitLineHeight'] = gridHeight;
    for (var j = 0; j < SplitYAxisNumber; j++) {
      if (j < SplitYAxisNumber - 1) {
        // 开始画横线
        DrawYAxisSplitLineG.append("line")
          .attr("dctype", "dcYAxisSplitLine")
          .attr("x1", 0)
          .attr("y1", (j + 1) * gridHeight)
          .attr("x2", YAxisTotalWidth)
          .attr("y2", (j + 1) * gridHeight)
          .attr("stroke", YAxis.YAxisLineColorValue || "#DCDFE6")
          .attr("stroke-width", YAxis.YAxisLineWidth || 1);
      }
    }

    // 画横向分割线
    var SplitXAxisNumber = (YAxis.SplitXAxisNumber || 24);
    // 计算每条线之间的距离，也就是每个各自的宽度
    var gridWidth = YAxisTotalWidth / SplitXAxisNumber;
    CalculationData['dcYAxisSplitLineWidth'] = gridWidth;
    for (var j = 0; j < SplitXAxisNumber; j++) {
      if (j < SplitXAxisNumber - 1) {
        // 开始画横线
        DrawYAxisSplitLineG.append("line")
          .attr("dctype", "dcYAxisSplitLine")
          .attr("x1", (j + 1) * gridWidth)
          .attr("y1", 0)
          .attr("x2", (j + 1) * gridWidth)
          .attr("y2", YAxisTotalHeight)
          .attr("stroke", YAxis.XAxisLineColorValue || "#DCDFE6")
          .attr("stroke-width", YAxis.XAxisLineWidth || 1);
      }
    }

    svgEle[i].dcYAxisSplitLineG = DrawYAxisSplitLineG;

  },
  // 画左右两侧刻度
  DrawYAxisLeftRightSplitLine: function (rootElement, svg, svgEle, i, DrawYAxisG) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;
    var YAxis = Config.YAxis;
    // 盒子高度

    //右侧宽度开始位置
    var dcRightYAxisStartX = PageSettings.InnerWidth
      - (CalculationData.dcLeftYAxisTotalWidth || 0)
      - (CalculationData.dcRightYAxisTotalWidth || 0);


    var LeftYAxis = YAxis.LeftYAxis || [];
    var RightYAxis = YAxis.RightYAxis || [];

    var YAxisArray = [LeftYAxis, RightYAxis];
    var DrawLeftYAxisG = null;


    // 画左侧刻度的函数
    function drawYAxisTicks(DrawLeftYAxisG, YAxisData, startXPosition, currentTarget, CalculationData, Config, DrawYAxisG) {
      // 计算Y轴总高度
      var YAxisTotalHeight = CalculationData.dcYAxisTotalHeight - CalculationData.dcYAxisHeaderLabelsGHeight;


      // 当前刻度列开始的位置
      var thisYAxisXStartPositon = startXPosition;

      for (var j = 0; j < YAxisData.length; j++) {
        var thisLeftYAxis = YAxisData[j];

        // 判断是否带有边框, 防止线段重叠，只画三条线
        if (thisLeftYAxis.BorderVisible) {
          // 定义绘制线段的函数
          function drawLine(x1, y1, x2, y2) {
            DrawLeftYAxisG.append("line")
              .attr("dctype", "dcLeftYAxisBorder")
              .attr("x1", x1)
              .attr("y1", y1)
              .attr("x2", x2)
              .attr("y2", y2)
              .attr("stroke", "black")
              .attr("stroke-width", 1);
          }

          // 上边框
          drawLine(thisYAxisXStartPositon, 0 - CalculationData.dcYAxisHeaderLabelsGHeight,
            thisYAxisXStartPositon + thisLeftYAxis.SpecifyTitleWidth, 0 - CalculationData.dcYAxisHeaderLabelsGHeight);

          // 下边框
          drawLine(thisYAxisXStartPositon, YAxisTotalHeight,
            thisYAxisXStartPositon + thisLeftYAxis.SpecifyTitleWidth, YAxisTotalHeight);

          if (currentTarget === "LeftYAxis") {
            // 左边框
            drawLine(thisYAxisXStartPositon, 0 - CalculationData.dcYAxisHeaderLabelsGHeight,
              thisYAxisXStartPositon, YAxisTotalHeight);
          } else {
            // 右边框
            drawLine(thisYAxisXStartPositon + thisLeftYAxis.SpecifyTitleWidth, 0 - CalculationData.dcYAxisHeaderLabelsGHeight,
              thisYAxisXStartPositon + thisLeftYAxis.SpecifyTitleWidth, YAxisTotalHeight);
          }
        }

        // 画图例
        if (thisLeftYAxis.SymbolVisible && thisLeftYAxis.SymbolStyle) {
          var x = DrawLeftYAxisG.node().getBBox().width / 2;
          var y = -CalculationData.LableHeight || 0;
          var SymbolColorValue = thisLeftYAxis.SymbolColorValue || "red";
          var SymbolBorderColorValue = thisLeftYAxis.SymbolBorderColorValue || null;
          var SymbolSize = thisLeftYAxis.SymbolSize || 5;
          var SymbolStyle = thisLeftYAxis.SymbolStyle || "SolidCircle";
          var symbol = WriterContorl_FlowChart.CreateSymbols(SymbolStyle, x, y, SymbolSize, SymbolColorValue, SymbolBorderColorValue || SymbolColorValue, '');
          if (symbol && typeof symbol === "object") {
            DrawLeftYAxisG.node().appendChild(symbol);
          }

        }

        // 画标题Title
        var thisLeftYAxisTitle = (thisLeftYAxis.Title || "");
        if (thisLeftYAxisTitle.length > 0 && thisLeftYAxis.TitleVisible) {
          DrawLeftYAxisG.append("text")
            .attr("dctype", "dcLeftYAxisTitle")
            .attr("style", `font-size:${thisLeftYAxis.TitleFontSize || Config.FontSize || 12}px;`)
            .attr("x", thisYAxisXStartPositon)
            .attr("y", -CalculationData.dcYAxisHeaderLabelsGHeight + (CalculationData.LableHeight))
            .html(thisLeftYAxisTitle);


        }

        //画红线数值的警戒线
        var RedLineValue = thisLeftYAxis.RedLineValue;
        var redLineY = 0;
        if (RedLineValue !== undefined && RedLineValue !== null) {
          var redLineInterVal = thisLeftYAxis.MaxValue - RedLineValue;
          // 计算红线的Y轴占比
          redLineY = (redLineInterVal / (thisLeftYAxis.MaxValue - thisLeftYAxis.MinValue)) * YAxisTotalHeight;
          var dcYAxisSplitLineG = DrawYAxisG.select('[dctype="dcYAxisSplitLineG"]');
          dcYAxisSplitLineG.append("line")
            .attr("dctype", "dcYAxisRedLine")
            .attr("x1", 0)
            .attr("y1", redLineY)
            .attr("x2", CalculationData.dcAttachedTableTotalWidth)
            .attr("y2", redLineY)
            .attr("stroke", "red")
            .attr("stroke-width", 1);
        }

        //刻度分割数量
        thisLeftYAxis.YSplitNum = thisLeftYAxis.YSplitNum || YAxis.SplitYAxisNumber || 10;
        //刻度间隔
        var thisLeftYAxisInterval = (thisLeftYAxis.MaxValue - thisLeftYAxis.MinValue) / (thisLeftYAxis.YSplitNum);
        //刻度高度间隔
        var thisLeftYAxisHeightInterval = YAxisTotalHeight / thisLeftYAxis.YSplitNum;
        // 画刻度
        for (var k = 0; k < thisLeftYAxis.YSplitNum; k++) {
          var thisLeftYAxisValue = thisLeftYAxis.MaxValue - (k * thisLeftYAxisInterval);
          // 计算刻度值,保留两位小数
          if (thisLeftYAxisValue) {
            thisLeftYAxisValue = (thisLeftYAxisValue % 1 === 0) ? Math.floor(thisLeftYAxisValue) : thisLeftYAxisValue.toFixed(1);
          }
          // 画刻度值
          DrawLeftYAxisG.append("text")
            .attr("dctype", "dcLeftYAxisSplitValue")
            .attr("style", `font-size:${thisLeftYAxis.YSplitFontSize || Config.FontSize || 12}px;`)
            .attr("x", 0)
            .attr("y", (k * thisLeftYAxisHeightInterval) + (CalculationData.LableHeight / 2))
            .html(thisLeftYAxisValue);
        }
        //是否展示最小值刻度
        if (thisLeftYAxis.MinValueVisible) {
          DrawLeftYAxisG.append("text")
            .attr("dctype", "dcLeftYAxisSplitValue")
            .attr("x", 0)
            .attr("y", YAxisTotalHeight + (CalculationData.LableHeight / 2)) //放到最底部
            .html(thisLeftYAxis.MinValue);
        }


        var allChild = DrawLeftYAxisG.node && DrawLeftYAxisG.node().childNodes;
        for (var l = 0; l < allChild.length; l++) {
          var child = allChild[l];
          var dctype = child.getAttribute("dctype") || null;
          if (dctype && ['dcSymbols', 'dcLeftYAxisTitle', 'dcLeftYAxisSplitValue'].indexOf(dctype) > -1) {
            var currentWidth = child.getBBox().width || 0;
            // 计算Title宽度
            var chaWidth = thisLeftYAxis.SpecifyTitleWidth - currentWidth;
            var x = thisYAxisXStartPositon + 4;
            // 设置对齐方式
            switch (thisLeftYAxis.TitleAlign) {
              case "right":
                x = thisYAxisXStartPositon + chaWidth - 4;
                break;
              case "center":
                x = thisYAxisXStartPositon + (chaWidth / 2);
                break;
            }
            if (dctype === "dcSymbols") {
              child.setAttribute("cx", x);
            } else {
              child.setAttribute("x", x);
            }
          }
        }

        //累计每一列刻度的宽度
        thisYAxisXStartPositon += thisLeftYAxis.SpecifyTitleWidth;

      }
    }

    // 在循环中调用
    for (var y = 0; y < YAxisArray.length; y++) {
      var currentTarget = y === 0 ? "LeftYAxis" : "RightYAxis";
      var currentYAxisGX = y === 0 ? -CalculationData.dcLeftYAxisTotalWidth : dcRightYAxisStartX;
      var currentYAxisGY = CalculationData.dcYAxisHeaderLabelsGHeight; // 表头标签结束位置为Y轴开始位置

      // 画左侧刻度
      DrawLeftYAxisG = DrawYAxisG.append("g")
        .attr("dctype", "dcLeftYAxisG")
        .attr("transform", `translate(${currentYAxisGX},${currentYAxisGY})`);

      // 调用画刻度的函数
      drawYAxisTicks(DrawLeftYAxisG, YAxis[currentTarget], 0, currentTarget, CalculationData, Config, DrawYAxisG);



    }

  },
  //画Y轴底部刻度数字
  DrawYAxisBottomSplitXAxisNumberArray: function (rootElement, svg, svgEle, i, DrawYAxisG) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;
    var YAxis = Config.YAxis;
    var SplitXAxisNumber = YAxis.SplitXAxisNumber || 0;
    var SplitXAxisNumberArray = YAxis.SplitXAxisNumberArray || [];
    //24个小时按指定划分
    if (!SplitXAxisNumberArray || SplitXAxisNumberArray.length === 0) {
      SplitXAxisNumberArray = [0];
      for (var k = 0; k < SplitXAxisNumber; k++) {
        SplitXAxisNumberArray.push((k + 1) * (24 / SplitXAxisNumber));
      }
    }


    // 画底部刻度数字
    var DrawYAxisBottomSplitXAxisNumberArrayG = DrawYAxisG.append("g")
      .attr("dctype", "dcYAxisBottomSplitXAxisNumberArrayG")
      .attr("transform", `translate(${0},${CalculationData.dcYAxisTotalHeight})`);


    // 计算X轴刻度间隔
    var XAxisInterval = CalculationData.dcAttachedTableCellWidth;
    // 如果数组长度和SplitXAxisNumber不一致，则重新计算间隔
    if (SplitXAxisNumber !== SplitXAxisNumberArray.length) {
      XAxisInterval = CalculationData.dcAttachedTableTotalWidth / (SplitXAxisNumberArray.length - 1);
    }
    // 画X轴刻度数字
    for (var j = 0; j < SplitXAxisNumberArray.length; j++) {
      // 画刻度值
      var dcYAxisBottomSplitXAxisNumberItem = DrawYAxisBottomSplitXAxisNumberArrayG.append("text") // 画刻度值
        .attr("dctype", "dcYAxisBottomSplitXAxisNumberArray")
        .attr("style", `font-size:${YAxis.XSplitFontSize || Config.FontSize || 12}px;`)
        .attr("x", j * XAxisInterval)
        .attr("y", YAxis.SplitXAxisNumberArrayHeight || CalculationData.LableHeight || 12)
        .html(SplitXAxisNumberArray[j]);
      var currentWidth = dcYAxisBottomSplitXAxisNumberItem.node().getBBox().width;
      if (currentWidth) {
        var currentWidth = (j * XAxisInterval) - (currentWidth / 2);
        dcYAxisBottomSplitXAxisNumberItem.node().setAttribute("x", currentWidth);
      }
    }
  },
  //绘制表格单元格线
  DrawAttachedTableLine: function (rootElement, svg, svgEle, i, dcAttachedTableG) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var AttachedTable = Config.AttachedTable || {};
    var TableRowData = Config.AttachedTable.TableRowData || [];

    //获取附加表格Y开始的点
    var dcAttachedTablYStartPositon = CalculationData['dcAttachedTablYStartPositon'];
    //获取附加表格X开始的点
    var dcAttachedTableXStartPositon = CalculationData['dcAttachedTableXStartPositon'];

    var dcAttachedTableLineG = dcAttachedTableG.append("g")
      .attr("dctype", "dcAttachedTableLineG");

    //绘制表格竖线,SplitXAxisNumber画线区域的Y轴分割线数量
    var SplitXAxisNumber = Config.YAxis.SplitXAxisNumber || 0;
    //计算每条线之间的距离，也就是每个各自的宽度
    var gridWidth = CalculationData.dcAttachedTableTotalWidth / SplitXAxisNumber;
    CalculationData['dcAttachedTableCellWidth'] = gridWidth;

    var gridWidthTempTotal = dcAttachedTableXStartPositon;
    for (var j = 0; j < SplitXAxisNumber; j++) {
      if (j > 0) {
        dcAttachedTableLineG.append("line")
          .attr("dctype", "dcAttachedTableBoxLine")
          .attr("x1", gridWidthTempTotal)
          .attr("y1", dcAttachedTablYStartPositon)
          .attr("x2", gridWidthTempTotal)
          .attr("y2", dcAttachedTablYStartPositon + CalculationData.dcAttachedTableTotalHeight)
          .attr("stroke", "black")
          .attr("stroke-width", 1);
      }
      gridWidthTempTotal += gridWidth;
    }

    //绘制单元格
    for (var k = 0; k < TableRowData.length; k++) {
      var thisRowData = TableRowData[k];
      if (k > 0) {
        dcAttachedTablYStartPositon += TableRowData[k - 1].RowHeight || 0;
      }

      for (var m = 0; m < SplitXAxisNumber; m++) {
        var cellHeight = thisRowData.RowHeight || 0; // 获取每一行的高度
        var cellX = dcAttachedTableXStartPositon + (m * gridWidth);
        var cellY = dcAttachedTablYStartPositon;

        // 添加单元格矩形
        dcAttachedTableLineG.append("rect")
          .attr("dctype", "dcAttachedTableCellRect")
          .attr("rowIndex", k)
          .attr("colIndex", m + 1)
          .attr("rowName", thisRowData.Name)
          .attr("IsCellSegment", thisRowData.IsCellSegment)
          .attr("x", cellX)
          .attr("y", cellY)
          .attr("width", gridWidth)
          .attr("height", cellHeight)
          .attr("fill", "white") // 设置填充颜色
          .attr("stroke", AttachedTable.TableCellBorderCorlorValue || '#606266') // 设置边框颜色
          .attr("stroke-width", AttachedTable.TableCellBorderWidth || 1); // 设置边框宽度

        // 判断是否需要分割
        if (thisRowData.IsCellSegment) {
          // 计算对角线的起点和终点
          var lineX1 = cellX;
          var lineY1 = cellY + cellHeight; // 修改为左下角
          var lineX2 = cellX + gridWidth;
          var lineY2 = cellY; // 修改为右上角

          // 添加反斜线
          dcAttachedTableLineG.append("line")
            .attr("dctype", "dcAttachedTableCellLine")
            .attr("x1", lineX1)
            .attr("y1", lineY1)
            .attr("x2", lineX2)
            .attr("y2", lineY2)
            .attr("stroke", AttachedTable.TableCellBorderCorlorValue || '#606266') // 设置边框颜色
            .attr("stroke-width", AttachedTable.TableCellBorderWidth || 1); // 设置边框宽度
        }

      }


    }

  },
  //绘制附加表格顶部的标题
  DrawAttachedTableHeader: function (rootElement, svg, svgEle, i, dcAttachedTableBox, dcAttachedTableG) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;


    var AttachedTableHeaderLabelHeight =
      Config.AttachedTable.AttachedTableHeaderLabelHeight || 0;

    //获取附加表格Y开始的点
    var dcAttachedTablYStartPositon = CalculationData['dcAttachedTablYStartPositon'];
    //获取附加表格X开始的点
    var dcAttachedTableXStartPositon = CalculationData['dcAttachedTableXStartPositon'];

    //绘制附加表格标题内容
    var AttachedTableHeaderLabels = Config.AttachedTable.AttachedTableHeaderLabels || [];
    if (AttachedTableHeaderLabels && AttachedTableHeaderLabels.length) {
      //获取绘制区域的宽度
      // var dcYAxisTotalWidth = CalculationData.dcYAxisTotalWidth;
      //Y轴内部表格区域的宽度
      var dcYAxisTotalWidth = PageSettings.InnerWidth
        - (CalculationData.dcLeftYAxisTotalWidth || 0)
        - (CalculationData.dcRightYAxisTotalWidth || 0);

      var dcAttachedTableHeaderLabelsItemWidth = dcAttachedTableBox.node().getBBox().width / AttachedTableHeaderLabels.length;
      var dcAttachedTableHeaderLabelsG = dcAttachedTableG
        .append("g")
        .attr("dctype", "dcAttachedTableHeaderLabelsG")
        .attr("transform", `translate(${dcAttachedTableXStartPositon},${dcAttachedTablYStartPositon - AttachedTableHeaderLabelHeight})`);
      svgEle[i].dcAttachedTableHeaderLabelsG = dcAttachedTableHeaderLabelsG;

      this.DrawHeaderLabelsTool(CalculationData, dcAttachedTableHeaderLabelsG, AttachedTableHeaderLabels, dcYAxisTotalWidth);

      var dcAttachedTableHeaderLabelsGHeight = dcAttachedTableHeaderLabelsG.node().getBBox().height || 0;
      CalculationData.Config.AttachedTable.AttachedTableHeaderLabelHeight = dcAttachedTableHeaderLabelsGHeight;

    }
  },
  //绘制附加表格的表头左侧Y轴
  DrawAttachedTableHeaderLeftYAxis: function (rootElement, svg, svgEle, i, dcAttachedTableG) {
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    var Config = CalculationData.Config;
    var PageSettings = Config.PageSettings;
    var AttachedTable = Config.AttachedTable || {};

    //获取附加表格Y开始的点
    var dcAttachedTablYStartPositon = CalculationData['dcAttachedTablYStartPositon'];
    //获取附加表格X开始的点
    var dcAttachedTableXStartPositon = CalculationData['dcAttachedTableXStartPositon'];

    var TableRowData = Config.AttachedTable.TableRowData || [];
    if (
      TableRowData &&
      TableRowData.length > 0
    ) {
      var dcAttachedTableLeftRowBoxG = dcAttachedTableG
        .append("g")
        .attr("dctype", "dcAttachedTableLeftRowBoxG");
      for (var j = 0; j < TableRowData.length; j++) {
        var thisRowData = TableRowData[j];

        if (j > 0) {
          dcAttachedTablYStartPositon += TableRowData[j - 1].RowHeight || 0;
        }

        //先在顶部画线
        var dcAttachedTableLeftRowBoxLine = dcAttachedTableLeftRowBoxG
          .append("line")
          .attr("dctype", "dcAttachedTableLeftRowBoxLine")
          .attr("x1", dcAttachedTableXStartPositon)
          .attr("y1", dcAttachedTablYStartPositon)
          .attr(
            "x2",
            dcAttachedTableXStartPositon - CalculationData.dcLeftYAxisTotalWidth
          )
          .attr("y2", dcAttachedTablYStartPositon)
          .attr("stroke", AttachedTable.TableHeaderCellBorderCorlorValue || '#606266') // 设置边框颜色
          .attr("stroke-width", AttachedTable.TableHeaderCellBorderWidth || 1); // 设置边框宽度
        //再画左侧的线
        var dcAttachedTableLeftRowBoxLine2 = dcAttachedTableLeftRowBoxG
          .append("line")
          .attr("dctype", "dcAttachedTableLeftRowBoxLine2")
          .attr(
            "x1",
            dcAttachedTableXStartPositon - CalculationData.dcLeftYAxisTotalWidth
          )
          .attr("y1", dcAttachedTablYStartPositon)
          .attr(
            "x2",
            dcAttachedTableXStartPositon - CalculationData.dcLeftYAxisTotalWidth
          )
          .attr("y2", dcAttachedTablYStartPositon + thisRowData.RowHeight)
          .attr("stroke", AttachedTable.TableHeaderCellBorderCorlorValue || '#606266') // 设置边框颜色
          .attr("stroke-width", AttachedTable.TableHeaderCellBorderWidth || 1); // 设置边框宽度
        if (j === TableRowData.length - 1) {
          //再画左侧的线
          var dcAttachedTableLeftRowBoxLine2 = dcAttachedTableLeftRowBoxG
            .append("line")
            .attr("dctype", "dcAttachedTableLeftRowBoxLine2")
            .attr(
              "x1",
              dcAttachedTableXStartPositon -
              CalculationData.dcLeftYAxisTotalWidth
            )
            .attr("y1", dcAttachedTablYStartPositon + thisRowData.RowHeight)
            .attr("x2", dcAttachedTableXStartPositon)
            .attr("y2", dcAttachedTablYStartPositon + thisRowData.RowHeight)
            .attr("stroke", AttachedTable.TableHeaderCellBorderCorlorValue || '#606266') // 设置边框颜色
            .attr("stroke-width", AttachedTable.TableHeaderCellBorderWidth || 1); // 设置边框宽度
        }

        //表头g
        var dcAttachedTableLeftRowG = dcAttachedTableLeftRowBoxG
          .append("g")
          .attr("dctype", "dcAttachedTableLeftRowG")
          .attr("UID", "dcAttachedTableLeftTableRow" + "_" + j)
          .attr("width", CalculationData.dcLeftYAxisTotalWidth)
          .attr("height", TableRowData[0].RowHeight)
          .attr(
            "transform",
            `translate(${PageSettings.LeftMargin},${dcAttachedTablYStartPositon})`
          );

        //先创建个文本框
        var dcAttachedTableLeftRowText = dcAttachedTableLeftRowG
          .append("text")
          .attr("dctype", "dcAttachedTableLeftRowText")
          .attr("x", 0)
          .attr("y", 0);

        // var dcAttachedTableLeftRowTspan = null

        //设置文本内容
        if (thisRowData.Label && thisRowData.Label.length > 0) {
          //单元格宽
          var cellWidth = CalculationData.dcLeftYAxisTotalWidth;
          //单元格高
          var cellHeight = thisRowData.RowHeight;
          //单个文本的宽度
          var textWidth = CalculationData.LableWidth;
          //单个文本的高度
          var textHeight = CalculationData.LableHeight;
          //当前单元格文本
          var text = thisRowData.Label;
          //自适应单元格宽高，自动换行展示，并使得文本居中
          if (cellWidth > textWidth * text.length) {
            //单元格宽大于文本宽度，一行可以展示开。则不换行展示
            //居中对齐
            var TextAlign = thisRowData.TextAlign || "center";
            var x = 0;
            if (TextAlign == "center") {
              x = (cellWidth - textWidth * text.length) / 2;
            } else if (TextAlign == "right") {
              x = cellWidth - textWidth * text.length;
            }
            //垂直对齐
            var VerticalAlign = thisRowData.VerticalAlign || "middle";
            var y = 0;
            if (VerticalAlign === "middle") {
              y = (cellHeight - textHeight) / 2;
            } else if (VerticalAlign === "bottom") {
              y = cellHeight - textHeight;
            }

            dcAttachedTableLeftRowText
              .append("tspan")
              .attr("x", x)
              .attr("y", y + textHeight)
              .text(text);
          } else if (cellHeight > textHeight * text.length) {
            //居中对齐
            var TextAlign = thisRowData.TextAlign || "center";
            var x = 0;
            if (TextAlign == "center") {
              x = (cellWidth - textWidth) / 2;
            } else if (TextAlign == "right") {
              x = cellWidth - textWidth;
            }
            //垂直对齐
            var VerticalAlign = thisRowData.VerticalAlign || "middle";
            var y = 0;
            if (VerticalAlign === "middle") {
              y = (cellHeight - textHeight * text.length) / 2;
            } else if (VerticalAlign === "bottom") {
              y = cellHeight - textHeight * text.length;
            }

            //单元格高大于文本高度，一列可以展示开。则单列展示
            for (var q = 0; q < text.length; q++) {
              dcAttachedTableLeftRowText
                .append("tspan")
                .attr("x", x)
                .attr("y", y + (textHeight * (q + 1)))
                .text(text[q]);
            }
          } else {
            //单元格宽小于文本宽度，自动换行处理
            var textArr = [];
            var textArrTemp = "";
            for (var q = 0; q < text.length; q++) {
              if (textWidth * (textArrTemp.length + 1) > cellWidth) {
                textArr.push(textArrTemp);
                textArrTemp = "";
              }
              textArrTemp += text[q];
            }
            textArr.push(textArrTemp);

            //居中对齐
            var TextAlign = thisRowData.TextAlign || "center";
            var x = 0;
            if (TextAlign == "center") {
              x = (cellWidth - textWidth * textArr[0].length) / 2;
            } else if (TextAlign == "right") {
              x = cellWidth - textWidth * textArr[0].length;
            }

            //垂直对齐
            var VerticalAlign = thisRowData.VerticalAlign || "middle";
            var y = 0;
            if (VerticalAlign === "middle") {
              y = (cellHeight - (textHeight * textArr.length)) / 2;
            } else if (VerticalAlign === "bottom") {
              y = cellHeight - (textHeight * textArr.length);
            }
            //设置文本内容
            for (var q = 0; q < textArr.length; q++) {
              dcAttachedTableLeftRowText
                .append("tspan")
                .attr("x", x)
                .attr("y", y + (textHeight * (q + 1)))
                .text(textArr[q]);
            }
          }
        }

        if (!svgEle[i].dcAttachedTableLeftRowBoxG) {
          svgEle[i].dcAttachedTableLeftRowBoxG = [];
        }
        svgEle[i].dcAttachedTableLeftRowBoxG.push(dcAttachedTableLeftRowG);
      }
    }
  },
  //进行缩放
  SetZoomRate: function (rootElement, newRate) {
    let that = this;
    var CalculationData = rootElement.DocumentOptions.CalculationData;
    rootElement.setAttribute("zoomRate", newRate);

    //获取到所有的svg元素
    var allSvg = rootElement.querySelectorAll("[dctype=page]");
    for (var i = 0; i < allSvg.length; i++) {
      allSvg[i].style.zoom = newRate;
      CalculationData.ZoomRate = newRate;
    }

    var borderSpan = rootElement.querySelector("#dc_borderSpan");
    if (borderSpan && !rootElement.dc_MoveBorderRect) {
      borderSpan.style.display = "none";
    }
    //缩放后重新计算所有元素的位置
    setTimeout(() => {
      that.CreateFlowControlInit(
        rootElement,
        rootElement.DocumentOptions.DefaultData,
        "EventFlowControlChangeZoomRate"
      );
    }, 200);
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
    pageContainer.style.setProperty("position", "relative", "important");
    pageContainer.style.textAlign = "center";
    pageContainer.style.overflow = "auto";
    rootElement.pageContainer = pageContainer;

    return pageContainer;
  },
  //创建默认初始化的数据
  CreateDefaultData: function (rootElement, data) {

    rootElement["DocumentOptions"] = {};
    var DefaultData = WriterContorl_FlowChart_DataInit;
    for (var key in data) {
      DefaultData[key] = data[key];
    }

    //处理初始化数据
    this.DocumentOptions = rootElement["DocumentOptions"] = {
      DefaultData: DefaultData,
      CalculationData: JSON.parse(JSON.stringify(DefaultData)),
      SVGElement: [],
    };
  },
  // 获取当前文本绘制后占用的宽度（因为svg绘制文本、数字、字母等，宽度不固定，需要计算），超出父元素宽度的部分需要换行展示，当换行后依旧超出高度时，则缩小字体继续展示
  /**
  * @param {string} text - 文本内容
  * @param {number} parentWidth - 父元素的宽度
  * @param {number} parentHeight - 父元素的高度
  * @param {number} fontSize - 字体大小
  * @param {string} fontFamily - 字体
  * @param {string} fontWeight - 字体粗细
  * @returns {object} - 文本绘制后占用的宽度、需要的行数，存放每行的文本内容数组
  */
  GetTextWidth: function (text = '', parentWidth = 0, parentHeight = 0, fontSize = 12, fontFamily = '宋体', fontWeight = false, isScaleFontSize = true) {
    if (text == null || text == undefined || text == "" || typeof text !== 'string') {
      return null;
    }
    parentWidth = (parseFloat(parentWidth)) || 0;// 减去2边框宽度
    parentHeight = (parseFloat(parentHeight) - 2) || 0;// 减去2边框宽度
    fontSize = parseFloat(fontSize);

    if (isNaN(parentWidth) || isNaN(parentHeight) || isNaN(fontSize)) {
      return null;
    }

    var lineObject = {};

    // 创建 SVG 元素
    const svgNS = "http://www.w3.org/2000/svg";
    const svg = document.createElementNS(svgNS, "svg");
    const textElement = document.createElementNS(svgNS, "text");

    // 设置文本样式
    textElement.setAttribute('style', `font-size:${fontSize}px;font-family:${fontFamily};font-weight:${fontWeight};`);

    svg.appendChild(textElement);
    document.body.appendChild(svg);
    text = text.toString();
    textElement.textContent = text;
    lineObject['textHeight'] = textElement.getBBox().height;// 文本高
    lineObject['textWidth'] = textElement.getBBox().width;// 文本宽度
    // 是否超出父元素宽度
    if (Math.ceil(lineObject['textWidth']) <= Math.ceil(parentWidth)) {
      // 宽度不超过父元素宽度，则直接返回
      lineObject['content'] = [text];// 文本截取数组
    } else {
      // 宽度超过父元素宽度，则需要换行展示
      textElement.textContent = '';
      lineObject['content'] = [];
      var prevTextIndex = 0;
      for (var i = 0; i <= text.length; i++) {
        textElement.textContent += text[i];
        var textWidth = textElement.getBBox().width;
        if (textWidth > parentWidth) {
          // 超出父元素宽度，则删除最后一个字符，重新计算
          // textElement.textContent = text.substring(prevTextIndex , i);
          lineObject['textHeight'] += textElement.getBBox().height;
          lineObject['content'].push(text.substring(prevTextIndex, i));
          textElement.textContent = text[i];
          prevTextIndex = i;
        }
      }
    }

    //是否超出父元素高度
    if (Math.ceil(lineObject.textHeight) >= Math.ceil(parentHeight)) {
      if (isScaleFontSize) {
        // 行数大于1且文本高度超过父元素高度，则缩小字体继续展示
        fontSize = fontSize * (parentHeight / lineObject.textHeight);
        lineObject['fontSize'] = fontSize;
      }
    }
    svg.remove();
    return lineObject;
  },

  //图例绘制
  CreateSymbols: function (typeName = "", x = 0, y = 0, size = 5, SymbolColorValue = 'blue', stroke = 'blue', title = "") {
    x = parseFloat(x).toFixed(2);
    y = parseFloat(y).toFixed(2);
    size = parseFloat(size).toFixed(2);

    if (!typeName || typeName == "") {
      typeName = "SolidCircle";
    }
    typeName = typeName.toLowerCase().trim();
    var fill = (SymbolColorValue.split(",") || []) || ['blue'];

    var g = document.createElementNS("http://www.w3.org/2000/svg", "g");
    g.setAttribute("transform", `translate(${x},${y})`);
    switch (typeName) {
      case "circle": //绘制实心圆的标签
      case "hollowcircle": //绘制空心圆的标签
      case "concentricdots"://同心圆点
      case "concentriccircle"://同心圆
      case "circlecenterx"://圆中嵌套X
        var circle = document.createElementNS("http://www.w3.org/2000/svg", "circle");
        circle.setAttribute("r", size);
        circle.setAttribute("fill", typeName == "circle" ? fill[0] : "#ffffff");
        circle.setAttribute("stroke", fill[0]); // 可以根据需要设置边框颜色
        circle.setAttribute("stroke-width", 1);
        circle.setAttribute('title', title);
        g.appendChild(circle);

        var fill2 = fill[1] && fill[1].length && fill[1].trim ? fill[1].trim() : fill[0];
        if (typeName == "concentricdots" || typeName == "concentriccircle") {
          var circle2 = document.createElementNS("http://www.w3.org/2000/svg", "circle");
          circle2.setAttribute("r", 2);
          circle2.setAttribute("fill", typeName == "concentricdots" ? fill2 : "#ffffff");
          circle2.setAttribute("stroke", fill2); // 可以根据需要设置边框颜色
          circle2.setAttribute("stroke-width", 1);
          circle2.setAttribute('title', title);
          g.appendChild(circle2);
        } else if (typeName == "circlecenterx") {
          size = size - 2;
          var cross = document.createElementNS("http://www.w3.org/2000/svg", "path");
          cross.setAttribute("d", "M -" + size + " -" + size + " L " + size + " " + size + " M " + size + " -" + size + " L -" + size + " " + size);
          cross.setAttribute("stroke", fill2);
          cross.setAttribute("stroke-width", 1);
          cross.setAttribute('title', title);
          g.appendChild(cross);
        }

        break;

      case "cross": // x
        const createLine = (x1, y1, x2, y2) => {
          const line = document.createElementNS("http://www.w3.org/2000/svg", "line");
          line.setAttribute("x1", x1);
          line.setAttribute("y1", y1);
          line.setAttribute("x2", x2);
          line.setAttribute("y2", y2);
          line.setAttribute("stroke", fill[0]);
          line.setAttribute("stroke-width", 1.5);
          line.setAttribute('title', title);
          return line;
        };
        const line1 = createLine(0 - size, 0 - size, size, size);
        const line2 = createLine(0 - size, size, size, 0 - size);
        g.appendChild(line1);
        g.appendChild(line2);
        break;
      case "solidtriangle"://实心三角形
      case "hollowsolidtriangle":// 空心三角形
        var triangle = document.createElementNS("http://www.w3.org/2000/svg", "polygon");
        triangle.setAttribute("points", `0,${-size} ${-size},${size} ${size},${size}`);
        triangle.setAttribute("fill", typeName == "solidtriangle" ? fill[0] : "#ffffff");
        triangle.setAttribute("stroke", fill[0]); // 可以根据需要设置边框颜色
        triangle.setAttribute("stroke-width", 1);
        triangle.setAttribute('title', title);
        g.appendChild(triangle);
        break;
      case "solidtrianglereversed"://倒三角形
      case "hollowsolidtrianglereversed":// 空心倒三角形
        var triangle = document.createElementNS("http://www.w3.org/2000/svg", "polygon");
        triangle.setAttribute("points", `0,${size} ${size},${-size} ${-size},${-size}`);
        triangle.setAttribute("fill", typeName == "solidtrianglereversed" ? fill[0] : "#ffffff");
        triangle.setAttribute("stroke", fill[0]); // 可以根据需要设置边框颜色
        triangle.setAttribute("stroke-width", 1);
        triangle.setAttribute('title', title);
        g.appendChild(triangle);
        break;


      case "solidsquare"://实心正方形
      case "hollowsolidsquare":// 空心正方形
        var square = document.createElementNS("http://www.w3.org/2000/svg", "rect");
        square.setAttribute("x", -size);
        square.setAttribute("y", -size);
        square.setAttribute("width", size * 2);
        square.setAttribute("height", size * 2);
        square.setAttribute("fill", typeName == "solidsquare" ? fill[0] : "#ffffff");
        square.setAttribute("stroke", fill[0]); // 可以根据需要设置边框颜色
        square.setAttribute("stroke-width", 1);
        square.setAttribute('title', title);
        g.appendChild(square);
        break;
    }


    if (g && g.childNodes && g.childNodes.length > 0) {
      g.setAttribute("x", x);
      g.setAttribute("y", y);
      g.setAttribute("dctype", "dcSymbols");
      return g;
    }
    return null;
  }
};
//绘制图形

let WriterContorl_FlowChart_DataInit = {
  //默认的公共属性
  Config: {
    ViewMode: "Page", //视图模式，single：单页视图，Normal：普通视图
    PageIndex: 0, //当前页码
    PageCount: 1, //总页数
    BigTitleMarinTop: 15, //大标题距离顶部的高度
    HeaderLabelsMarginTop: 20, //页面标题距离顶部的高度
    BigTitle: "北京某某医院", //大标题
    BigTitleFontSize: 27, //大标题字体大小
    BigTitleFontName: "", //大标题字体名称
    BigTitleFontBold: false, //大标题字体是否加粗
    FontName: "宋体", //默认字体名称，宋体
    FontSize: 9, //默认字体大小，9像素
    PageIndexText: "第[%pageindex%]页", //页码文本
    GridLineColorValue: "#E0E0E0", //网格线颜色
    FooterDescription: "", //底部说明
    LineWidth: 1, //线宽度
    StartingTime: "", //产程开始时间
    PageSettings: {
      PaperSizeName: "A4", //纸张大小名称
      PaperHeight: 1123, //纸张高度
      PaperWidth: 792, //纸张宽度
      Landscape: false, //是否横向
      TopMargin: 50, //上边距
      BottomMargin: 50, //下边距
      LeftMargin: 50, //左边距
      RightMargin: 50, //右边距
      Unit: "px", //单位像素
    },
    //页眉属性
    HeaderLabel: [],

    //产程折线图属性
    YAxis: {
      SplitYAxisNumber: 10,//Y格子数量
      SplitXAxisNumber: 24,//X格子数量
      ShowXAxis: true,//是否展示底部X轴的数值
      SplitXAxisNumberArray: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24],//底部数值
      SplitXAxisNumberArrayHeight: 30,//底部X轴文字高度
      XAxisForHeaderLableList: [],//表头
      LeftYAxis: [],//左侧Y轴
      RightYAxis: [],//右侧Y轴
    },

    //附属表格
    AttachedTable: {
      //附属表格的顶部数据
      AttachedTableHeaderLabels: [],
      //附属表格的行数据
      TableRowData: [],
    },
    //文本标签
    TextLabels: [],
  },
  Values: null,
};
let WriterContorl_FlowChart_Data_default = {
  //页眉属性
  HeaderLabel: {
    Title: "", //标题
    Value: "", //值
    FontSize: 9, //   字体大小
    FontName: "宋体", //字体名称
    FontBold: false, //是否加粗
    GroupIndex: 0, //分组索引
    SeperatorChar: "", //分隔符
  },
  //产程Y轴属性
  YAxis: {
    Name: "", //名称
    Title: "", //标题
    Value: "", //值
    FontSize: 9, //字体大小
    Fontcolor: "", //字体颜色
    FontName: "宋体", //字体名称
    FontBold: false, //是否加粗
    Vertical: true, //是否垂直
    MinValue: 1, //最小值
    MaxValue: 10, //最大值
    SplitNumber: 10, //分割数量
    SplitNumberForTextList: [], //分割数量对应的文本列表
    LineWidth: 1, //折线宽度
    LineColor: "", //折线颜色
    SpecifyWidth: 0, //指定宽度
    SymbolVisible: false, //是否显示点
    SymbolStyle: "SolidCicle", //点样式
    SymbolColorValue: "red", //点颜色
    SymbolSize: 20, //点大小
    EndSymbolStyle: "circlecenterx", //结束点样式
    EndSymbolColorValue: "#ff1515,#1b6ae7", //结束点颜色
    EndSymbolSize: 4, //结束点大小
    EndSymbolLineStyle: "dashed", //结束点线样式
    EndSymbolLineWidth: 1, //结束点线宽度
    EndSymbolLineColor: "gray", //结束点线颜色

    EndValueDownArrowVisible: false, //是否展示结束箭头
    EndValueDownArrowLength: 32, //结束箭头长度（px）
    EndValueDownArrowText: "",
    EndValueDownArrowTextFontSize: 12,
  },
  TableRow: {
    Name: "", //名称
    Title: "", //标题
    FontSize: 9, //字体大小
    Fontcolor: "", //字体颜色
    FontName: "宋体", //字体名称
    FontBold: false, //是否加粗
    RowHeifgt: 60, //行高
    IsSplitLine: false, //是否斜线分割
    SplitLineColor: "", //分割线颜色
    SplitLineStyle: "solid", //分割线样式
    SplitLineWidth: 1, //分割线宽度
    ShowTitle: true, //是否显示标题
  },
  //标签
  NewTextLabel: {
    Left: 0, //左偏移
    Top: 0, //上偏移
    Width: 100, //宽度
    Height: 100, //高度
    Text: "文本标签", //文本 [%Symbol%]
    MultiLine: false, //多行文本
    ShowBorder: false, //显示边框
    BackColorValue: null, //背景色
    TextColorValue: null, //文本颜色
    Alignment: "Center", //水平对齐方式
    LineAlignment: "Center", //垂直对齐方式
    TextFontName: "宋体", //字体名
    TextFontSize: 9, //字体大小
    TextFontBold: false, //文本加粗
    TextFontItalic: false, //文本倾斜
    TextFontUnderline: false, //文本下划线
    ImageDataBase64String: null, //图片base64数据
    SymbolVisible: false, //是否显示点
    SymbolStyle: "SolidCicle", //点样式
    SymbolColorValue: "red", //点颜色
    SymbolSize: 20, //点大小
  },
};
