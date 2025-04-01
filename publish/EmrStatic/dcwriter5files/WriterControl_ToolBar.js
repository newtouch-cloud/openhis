"use strict";

// import { html } from "parse5";
import { DCTools20221228 } from "./DCTools20221228.js";
import { WriterControl_UI } from "./WriterControl_UI.js";

/**处理文档工具条模块 */
export let WriterControl_ToolBar = {
  CreateToolBarControl: function (containerID) {
    let rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
    if (rootElement == null) {
      return null;
    }
        let toolPanel = rootElement.querySelector(".DC-toolBar-panel");
        if (toolPanel) {
            return null;
        }
    let toolBarCssString = `
    .DC-toolBar-panel{
      color:#555555;
      font-family:"微软雅黑";
      position: relative;
    }
    .DC-toolBar-panel-menu{
        background: repeating-linear-gradient(transparent 0 1px, transparent 1px 39px) center top 39px / 100% calc(100% - 39px) no-repeat;
        background-color: #fff;
        display: flex;
        flex: 0 0 auto;
        flex-shrink: 0;
        flex-wrap: wrap;
        grid-column: 1 / -1;
        grid-row: 1;
        padding-left: 5px;
        z-index: 19999;
    }
    .DC-toolBar-panel-menu button{
        align-items: center;
        background: 0 0;
        border: 0;
        border-radius: 3px;
        box-shadow: none;
        color: #222f3e;
        display: flex;
        flex: 0 0 auto;
        font-size: 14px;
        font-style: normal;
        font-weight: 400;
        height: 28px;
        justify-content: center;
        margin: 5px 1px 6px 0;
        outline: 0;
        overflow: hidden;
        padding: 0 10px;
        text-transform: none;
        width: auto;
        
    }
    .DC-toolBar-panel-menu button:hover{
        background: #dee0e2;
        border: 0;
        box-shadow: none;
        color: #222f3e;
        cursor: pointer;
        
    }

    .DC-toolBar-panel-toolsbar{
      display: flex;
      flex: 0 0 auto;
      flex-shrink: 0;
      flex-wrap: nowrap;
      padding: 0 0;
      border: 1px solid #ccc;
      overflow-y: hidden;
      overflow-x: auto;
      background-color:#fff;
    }
    .DC-toolBar-panel-toolsbar .toolbar-group{
      align-items: center;
      display: flex;
      flex-wrap: nowrap;
      margin: 0 0;
      // padding: 0 4px 0 4px;
      line-height:22px;
    }
    .DC-toolBar-panel-toolsbar .toolbar-group:not(:last-of-type) {
      border-right: 1px solid #ccc;
    }
    .DC-toolBar-panel-toolsbar .toolbar-group span{
      align-items: center;
      background: 0 0;
      border: 0;
      border-radius: 3px;
      box-shadow: none;
      color: #222f3e;
      display: flex;
      flex: 0 0 auto;
      font-size: 14px;
      font-style: normal;
      font-weight: 400;
      height: 22px;
      justify-content: center;
      margin: 5px 1px 6px 0;
      outline: 0;
      overflow: hidden;
      // padding: 0 10px;
      text-transform: none;
      width: auto;
      white-space: nowrap;
    }
    .DC-toolBar-panel-toolsbar .toolbar-box{
      display:flex;
      padding:5px 10px;
    }
    .DC-toolBar-panel-toolsbar .toolbar-box-select{
      margin:0 2px;
      height:38px;
      
    }
    .DC-toolBar-panel-toolsbar .toolbar-box-select select{
      height:100%;
      border: 1px solid #dcdfe6;
      font-size:14px;
      width:70px;
    }

    .DC-toolBar-panel-toolsbar .toolbar-box-select select::-webkit-scrollbar {
      width: 6px;
      transition: all 2s;
      height:6px;
    }
    .DC-toolBar-panel-toolsbar .toolbar-box-select select::-webkit-scrollbar-thumb {
      background-color: #dddddd;
      border-radius: 100px;
    }
    .DC-toolBar-panel-toolsbar .toolbar-box-select select::-webkit-scrollbar-thumb:hover {
      background-color: #bbb;
    }
    .DC-toolBar-panel-toolsbar .toolbar-box-select select::-webkit-scrollbar-corner {
      background-color: rgba(255, 255, 255, 0);
    }

    
    .DC-toolBar-panel-toolsbar .toolbar-box:hover{
        background: #dee0e2;
        border: 0;
        box-shadow: none;
        color: #222f3e;
        cursor: pointer;
    }
    .DC-toolBar-panel-toolsbar .toolbar-box .toolbar-box-svg{
      width: 14px;
      margin-top: 6px;
    }
    .DC-toolBar-panel-toolsbar .toolbar-box .only-toolbar-box-svg{
      width: 18px;
      margin-top: 7px;
    }
    .DC-toolBar-panel-toolsbar .toolbar-box svg{
      vertical-align: baseline;
    }
    .DC-toolBar-panel-toolsbar::-webkit-scrollbar {
      width: 6px;
      transition: all 2s;
      height:6px;
    }
    .DC-toolBar-panel-toolsbar::-webkit-scrollbar-thumb {
      background-color: #dddddd;
      border-radius: 100px;
    }
    .DC-toolBar-panel-toolsbar::-webkit-scrollbar-thumb:hover {
      background-color: #bbb;
    }
    .DC-toolBar-panel-toolsbar::-webkit-scrollbar-corner {
      background-color: rgba(255, 255, 255, 0);
    }
    .DC-toolBar-panel .DC-toolBar-panel-menuList{
      position: absolute;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.12), 0 0 6px rgba(0, 0, 0, 0.04);
      background: #fff;
      border-radius: 4px;
      // padding: 8px 0;
      user-select: none;
      // opacity: 0;
      visibility:hidden;
      z-index: 19999;
    }
    .DC-toolBar-panel .show{
      // opacity: 1;
      visibility:visible;
    }

    .DC-toolBar-mask {
      display: none;
      position: absolute;
      z-index: 9999;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      background-color: rgba(0,0,0,0);
    } 
    .display-block {
        display: block
    }
    .DC-toolBar-panel-menuList{
      overflow: auto;
    }
    .DC-toolBar-panel-menuList ul{
      font-size:14px;
    }
    .DC-toolBar-panel-menuList li{
      // margin: 10px;
      padding:10px;
      min-width: 180px;
      display: flex;
    }
    .DC-toolBar-panel-menuList li:hover{
      // border: 0;
      box-shadow: none;
      cursor: pointer;
      background: #ecf5ff;
      color: #409eff;
    }
    .DC-toolBar-menuList-li-hasChild::after{
      content: "";
      height: 6px;
      width: 6px;
      margin: 8px;
      right: 8px;
      border-width: 1px 1px 0 0;
      border-color: #303133;
      border-style: solid;
      position: absolute;
      transform: matrix(0.71, 0.71, -0.71, 0.71, 0, 0);
    }
    .DC-toolBar-panel-menuList li div:first-child{
      width: 16px;
      margin-right: 5px;
    }
    .DC-toolBar-panel-menuList::-webkit-scrollbar {
      width: 6px;
      transition: all 2s;
      height:6px;
    }
    .DC-toolBar-panel-menuList::-webkit-scrollbar-thumb {
      background-color: #dddddd;
      border-radius: 100px;
    }
    .DC-toolBar-panel-menuList::-webkit-scrollbar-thumb:hover {
      background-color: #bbb;
    }
    .DC-toolBar-panel-menuList::-webkit-scrollbar-corner {
      background-color: rgba(255, 255, 255, 0);
    }
    .DC-toolBar-panel-menuList .menu_item__divided{
      border-top: 1px solid #ebeef5;
    }
    .DC-renderColorPickerBox {
      position: fixed;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.12), 0 0 6px rgba(0, 0, 0, 0.04);
      background: #fff;
      border-radius: 4px;
      // padding: 8px 0;
      user-select: none;
      // opacity: 0;
      // visibility:hidden;
      z-index: 19999;
    }
    `;
    let toolBarCss = rootElement.ownerDocument.createElement("style");
    toolBarCss.id = "DCToolBarCss";
    toolBarCss.innerHTML = toolBarCssString;
    rootElement.ownerDocument.head.appendChild(toolBarCss);

    let theTool = {
      dom: {},
      MenuBars: [
        {
          label: "文件",
          value: "file",
        },
        {
          label: "格式",
          value: "format",
        },
        {
          label: "段落",
          value: "paragraph",
        },
        {
          label: "常规",
          value: "normal",
        },
        {
          label: "设计",
          value: "design",
        },
        {
          label: "表格",
          value: "table",
        },
        {
          label: "插入",
          value: "insert",
        },
        {
          label: "高级",
          value: "other",
        },
      ],

      toolsbar_groups: [
        [
          // {
          //   label: "保存",
          //   icon: "ri-save-line",
          //   showLabel: true,
          // },
          {
            label: "撤销",
            commendName: "undo",
            icon: "ri-arrow-go-back-line",
            hiddenLabel: true,
          },
          {
            label: "重做",
            commendName: "redo",
            icon: "ri-arrow-go-forward-line",
            hiddenLabel: true,
          },
        ],
        [
          {
            label: "剪切",
            commendName: "cut",
            icon: "ri-scissors-2-fill",
            hiddenLabel: true,
          },
          {
            label: "复制",
            commendName: "copy",
            icon: "ri-file-copy-line",
            hiddenLabel: true,
          },
          {
            label: "粘贴",
            commendName: "paste",
            icon: "ri-clipboard-line",
            hiddenLabel: true,
          },
        ],
        [
          {
            type: "select",
            label: "字体",
            commendName: "fontname",
            placeholder: "字体",
            value: "",
            listProps: {
              label: "ch",
              value: "en",
            },
            style: "width:160px;",
            optionName: "fontFamilys",
          },
          {
            type: "select",
            label: "字号",
            commendName: "fontsize",
            placeholder: "字号",
            value: "",
            list: this.FontsArr,
            style: "margin-left:5px;width:160px",
            optionName: "fontSize",
          },
        ],
        [
          {
            label: "加粗",
            commendName: "bold",
            icon: "ri-bold",
            hiddenLabel: true,
          },
          {
            label: "斜体",
            commendName: "italic",
            icon: "ri-italic",
            hiddenLabel: true,
          },
          {
            label: "下划线",
            commendName: "underline",
            icon: "ri-underline",
            hiddenLabel: true,
          },
          {
            label: "删除线",
            commendName: "strikeout",
            icon: "ri-strikethrough",
            hiddenLabel: true,
          },
          {
            type: "color",
            value: "",
            label: "文字颜色",
            commendName: "color",
            icon: "ri-font-color",
            hiddenLabel: true,
            onclick: function (e, that) {
              // that.buildColorPicker(e, "color", function (colorStr) {
              //   rootElement.DCExecuteCommand("color", false, colorStr);
              // });
            },
          },
          {
            type: "color",
            value: "",
            label: "背景颜色",
            commendName: "backcolor",
            icon: "ri-palette-fill",
            hiddenLabel: true,
            onclick: function (e, that) {
              // that.buildColorPicker(e, "backcolor", function (colorStr) {
              //   rootElement.DCExecuteCommand("backcolor", false, colorStr);
              // });
            },
          },
        ],
        [
          {
            label: "居左",
            commendName: "AlignLeft",
            icon: "ri-align-left",
            hiddenLabel: true,
          },
          {
            label: "居中",
            commendName: "AlignCenter",
            icon: "ri-align-center",
            hiddenLabel: true,
          },
          {
            label: "居右",
            commendName: "AlignRight",
            icon: "ri-align-right",
            hiddenLabel: true,
          },
          {
            label: "分散对齐",
            commendName: "AlignDistribute",
            icon: "ri-align-justify",
            hiddenLabel: true,
          },
        ],
        [
          {
            type: "popover",
            label: "插入特殊字符",
            commendName: "",
            icon: "ri-omega",
            hiddenLabel: true,
            // onclick: () => {
            //   rootElement.DCExecuteCommand("InsertSpecifyCharacter", true, {});
            // },
          },
          {
            type: "popover",
            label: "插入图片",
            commendName: "",
            icon: "ri-image-add-fill",
            hiddenLabel: true,
            // onclick: () => {
            //   rootElement.DCExecuteCommand("InsertImage", true, {});
            // },
          },
        ],
        [
          {
            label: "设计模式",
            commendName: "DesignMode",
            icon: "ri-pencil-ruler-2-line",
            hiddenLabel: true,
          },
          // {
          //   label: "刷新文档",
          //   commendName: "",
          //   hiddenLabel: true,
          //   icon: "ri-loop-right-fill",
          // },
        ],
      ],
      selectOptionsList: {
        fontFamilys: [],
        fontSize: [
          8,
          9,
          10,
          10.5,
          11,
          12,
          14,
          16,
          18,
          20,
          22,
          24,
          26,
          28,
          36,
          48,
          72,
          "初号",
          "小初",
          "一号",
          "小一",
          "二号",
          "小二",
          "三号",
          "小三",
          "四号",
          "小四",
          "五号",
          "小五",
          "六号",
          "小六",
          "七号",
          "八号",
        ],
        orderList: [
          {
            label: "1.2.3.4",
            value: "ListNumberStyle",
            index: 1,
          },
          {
            label: "1,2,3,4",
            value: "ListNumberStyleArabic1",
            index: 2,
          },
          {
            label: "1）2）3）4）",
            value: "ListNumberStyleArabic2",
            index: 3,
            },
            {
                label: "1、2、3、4、",
                value: "ListNumberStyleArabic3",
                index: 15,
            },
          {
            label: "a）b）c）d）",
            value: "ListNumberStyleLowercaseLetter",
            index: 4,
          },
          {
            label: "i）ii）iii）iv）",
            value: "ListNumberStyleLowercaseRoman",
            index: 5,
          },
          {
            label: "① ② ③ ④",
            value: "ListNumberStyleNumberInCircle",
            index: 6,
          },
          {
            label: "一.二.三.四",
            value: "ListNumberStyleSimpChinNum1",
            index: 7,
          },
          {
            label: "一）二）三）四",
            value: "ListNumberStyleSimpChinNum2",
            index: 8,
          },
          {
            label: "壹.贰.叁.肆",
            value: "ListNumberStyleTradChinNum1",
            index: 9,
          },
          {
            label: "壹）贰）叁）肆",
            value: "ListNumberStyleTradChinNum2",
            index: 10,
          },
          {
            label: "A）B）C）D",
            value: "ListNumberStyleUppercaseLetter",
            index: 11,
          },
          {
            label: "Ⅰ）Ⅱ）Ⅲ）Ⅳ",
            value: "ListNumberStyleUppercaseRoman",
            index: 12,
          },
          {
            label: "甲,乙,丙,丁",
            value: "ListNumberStyleZodiac1",
            index: 13,
          },
          {
            label: "子,丑,寅,卯",
            value: "ListNumberStyleZodiac2",
            index: 14,
          },
        ],
        unorderList: [
          {
            label: "● Bulletedlist",
            value: "BulletedList",
            index: 10000,
          },
          {
            label: "■ Bulletedlistblock",
            value: "BulletedListBlock",
            index: 10001,
          },
          {
            label: "◆ Bulletedlistdiamond",
            value: "BulletedListDiamond",
            index: 10002,
          },
          {
            label: "✔ BulletedListCheck ",
            value: "BulletedListCheck ",
            index: 10003,
          },
          {
            label: "➢ BulletedListRightArrow",
            value: "BulletedListRightArrow",
            index: 10004,
          },
          {
            label: "◇ BulletedListHollowStar",
            value: "BulletedListHollowStar",
            index: 10005,
          },
        ],
        lineheight: [1.0, 1.5, 2.0, 2.5, 3.0],
        medicalExpression: [
          {
            label: "月经史公式1",
            ID: "expression1",
            ExpressionStyle: "FourValues1",
            Values: "Value1:14;Value2:14;Value3:14;Value4:14",
            ValuesCount: 4,
          },
          {
            label: "月经史公式2",
            ID: "expression2",
            ExpressionStyle: "FourValues2",
            Values: "Value1:14;Value2:14;Value3:14;Value4:14",
            ValuesCount: 4,
          },
          {
            label: "月经史公式3",
            ID: "expression3",
            ExpressionStyle: "ThreeValues",
            Values: "Value1:14;Value2:14;Value3:14",
            ValuesCount: 3,
          },
          {
            label: "月经史公式4",
            ID: "expression4",
            ExpressionStyle: "FourValues",
            Values: "Value1:14;Value2:14;Value3:14;Value4:14",
            ValuesCount: 4,
          },
          {
            label: "瞳孔",
            ID: "expression5",
            ExpressionStyle: "Pupil",
            Values: "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;Value7:14;",
            ValuesCount: 7,
          },
          {
            label: "胎心值",
            ID: "expression6",
            ExpressionStyle: "FetalHeart",
            Values: "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;",
            ValuesCount: 6,
          },
          {
            label: "眼球突出度",
            ID: "expression7",
            ExpressionStyle: "ThreeValues2",
            Values: "Value1:0;Value2:1;Value3:2;",
            ValuesCount: 3,
          },
          {
            label: "斜视符号",
            ID: "expression8",
            ExpressionStyle: "StrabismusSymbol",
            Values: "Value1:L;",
            ValuesCount: 1,
          },
          {
            label: "标尺",
            ID: "expression9",
            ExpressionStyle: "PainIndex",
            Values: "Value1:14;",
            ValuesCount: 1,
          },
          {
            label: "恒牙牙位图",
            ID: "expression10",
            ExpressionStyle: "PermanentTeethBitmap",
            Values:
              "Value1:1;Value2:2;Value3:3;Value4:4;Value5:5;Value6:6;Value7:7;Value8:8;Value9:9;Value10:10;Value11:11;Value12:12;Value13:13;Value14:14;Value15:15;Value16:16;Value17:17;Value18:18;Value19:19;Value20:20;",
            ValuesCount: 20,
          },
          {
            label: "乳牙牙位图",
            ID: "expression11",
            ExpressionStyle: "DeciduousTeech",
            Values:
              "Value1:1;Value2:2;Value3:3;Value4:4;Value5:5;Value6:6;Value7:7;Value8:8;Value9:9;Value10:10;Value11:11;Value12:12;Value13:13;Value14:14;Value15:15;Value16:16;Value17:17;Value18:18;Value19:19;Value20:20;",
            ValuesCount: 20,
          },
          {
            label: "分数公式",
            ID: "expression12",
            ExpressionStyle: "Fraction",
            Values: "Value1:1;Value2:2;",
            ValuesCount: 2,
          },
          {
            label: "病变上牙",
            ID: "expression13",
            ExpressionStyle: "DiseasedTeethTop",
            Values: "Value1:1;Value2:2;Value3:2;",
            ValuesCount: 3,
          },
          {
            label: "病变下牙",
            ID: "expression14",
            ExpressionStyle: "DiseasedTeethBotton",
            Values: "Value1:1;Value2:2;Value3:2;",
            ValuesCount: 3,
          },
          {
            label: "光定位",
            ID: "expression15",
            ExpressionStyle: "LightPositioning",
            Values: "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;Value7:14;Value8:14;Value9:14;",
            ValuesCount: 9,
          },
        ],
      },
    };
    theTool.init = function () {
      let self = this;
      let FontFamilysArr = rootElement.getSupportFontFamilys();
      // console.log(FontFamilysArr);
      if (FontFamilysArr && FontFamilysArr.length > 0) {
        selectOptionsList.fontFamilys = FontFamilysArr;
      }
      FontFamilysArr = null;

      self.buildStage();
      self.buildPanel();

      self.bindEvent();
      let pageContainer = WriterControl_UI.GetPageContainer(rootElement);
      if (pageContainer) {
        pageContainer.style.height = `calc(100% - 24px - ${self.dom.pannel.offsetHeight}px)`;
      }
      // pageContainer=null;
      // self=null
    };
    // 构建选择器
    theTool.buildStage = function () {
      const self = this;
      self.dom.maskBg = rootElement.ownerDocument.createElement("div");
      self.dom.maskBg.classList.add("DC-toolBar-mask");
      self.dom.pannel = rootElement.ownerDocument.createElement("div");
      self.dom.pannel.classList.add("DC-toolBar-panel");
      self.dom.menu = rootElement.ownerDocument.createElement("div");
      self.dom.menu.classList.add("DC-toolBar-panel-menu");
      self.dom.toolsbar = rootElement.ownerDocument.createElement("div");
      self.dom.toolsbar.classList.add("DC-toolBar-panel-toolsbar");
      self.dom.menuList = rootElement.ownerDocument.createElement("div");
      self.dom.menuList.classList.add("DC-toolBar-panel-menuList");
      self.dom.pannel.insertAdjacentElement("beforeend", self.dom.menuList);
    };

    theTool.buildPanel = function () {
      let self = this;

      //   self.dom.pannel.innerHTML = "<div>我是工具条</div>";
      self.dom.pannel.classList.add("DC-toolBar-panel");
      rootElement.insertAdjacentElement("afterbegin", self.dom.pannel);
      //插入菜单
      for (let i = 0; i < this.MenuBars.length; i++) {
        const element = this.MenuBars[i];
        // menuHtml += `<button name=${element.value}> ${element.label}</button>`;
        let menuBtnDOM = rootElement.ownerDocument.createElement("button");
        menuBtnDOM.name = element.value;
        menuBtnDOM.innerText = element.label;
        menuBtnDOM._rootElementID = rootElement.id;
        // 菜单悬浮事件
        menuBtnDOM.onmouseenter = (e) => {
          const el = e.target;
          if (el.name) {
            e.stopPropagation();
            let rootElement = DCTools20221228.GetOwnerWriterControl(el._rootElementID);
            let menuListDOM = rootElement.querySelector(".DC-toolBar-panel-menuList");
            if (menuListDOM.classList.contains("show")) {
              hiddenMenu(rootElement);
              e.target.click();
            }
          }
        };
        self.dom.menu.insertAdjacentElement("beforeend", menuBtnDOM);
      }
      self.dom.pannel.insertAdjacentElement("afterbegin", self.dom.menu);

      //插入按钮组
      build_ToolBar(rootElement.id);
      // let toolbarGroupDOM = rootElement.ownerDocument.createElement("div");
      // for (let i = 0; i < toolsbar_groups.length; i++) {
      //   let groups = toolsbar_groups[i];
      //   if (groups.length > 0) {
      //     toolbarGroupDOM.classList.add("toolbar-group");
      //     for (let j = 0; j < groups.length; j++) {
      //       let tool = groups[j];
      //       let svgDOM = rootElement.ownerDocument.createElement("div");
      //       if (tool.icon && SVG_Dictionary[tool.icon]) {
      //         if (tool.hiddenLabel) {
      //           svgDOM.classList.add("only-toolbar-box-svg");
      //           svgDOM.innerHTML = SVG_Dictionary[tool.icon];
      //         } else {
      //           svgDOM.classList.add("toolbar-box-svg");
      //           svgDOM.innerHTML = SVG_Dictionary[tool.icon];
      //         }
      //       }
      //       if (tool.type == "select") {
             
      //         if (tool.optionName) {
      //           let options = selectOptionsList[tool.optionName];
      //           if (options && options.length) {
      //             let selectDOM = rootElement.ownerDocument.createElement("select");
      //             let optionStr = "";
      //             for (let k = 0; k < options.length; k++) {
      //               optionStr += `<option value="${options[k]}">${options[k]}</option>`;
      //             }
      //             // console.log(optionStr);
      //             // selectDOM.innerHTML+=optionStr
               
      //           let selecDivDOM = rootElement.ownerDocument.createElement("div");
      //           selecDivDOM.name = tool.label;
      //           selecDivDOM.title = tool.label;
      //           selecDivDOM.classList.add("toolbar-box-select");
      //           selecDivDOM.insertAdjacentElement("beforeend", selectDOM);
      //           selectDOM.onchange = (e) => {
      //             rootElement.DCExecuteCommand(tool.commendName, false, e.target.value);
      //           };
      //           toolbarGroupDOM.insertAdjacentElement("beforeend", selecDivDOM);
      //           }
      //         }
      //       } else {
      //         let toolDivDOM = rootElement.ownerDocument.createElement("div");
      //         toolDivDOM.name = tool.label;
      //         toolDivDOM.title = tool.label;
      //         toolDivDOM.classList.add("toolbar-box");
      //         toolDivDOM.insertAdjacentElement("beforeend", svgDOM);

      //         toolDivDOM.removeEventListener("click", null);
      //         toolDivDOM.onclick = (e) => {
      //           if (tool.onclick) {
      //             tool.onclick(e, rootElement);
      //           } else if (tool.commendName) {
      //             rootElement.DCExecuteCommand(tool.commendName, false, null);
      //           }
      //         };
      //         toolbarGroupDOM.insertAdjacentElement("beforeend", toolDivDOM);
      //         if (!tool.hiddenLabel) {
      //           let labelSpanDivDOM = rootElement.ownerDocument.createElement("div");
      //           labelSpanDivDOM.innerHTML = `<span>${tool.label}</span>`;
      //           toolDivDOM.insertAdjacentElement("beforeend", labelSpanDivDOM);
      //         }
      //       }
      //     }
      //   }
      // }

      // self.dom.toolsbar.insertAdjacentElement("beforeend", toolbarGroupDOM);
      // self.dom.pannel.insertAdjacentElement("beforeend", self.dom.toolsbar);
      // //拿到外层包裹元素pageContainer
      // var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
      // if (pageContainer) {
      //   pageContainer.style.height = pageContainer.offsetHeight - self.dom.pannel.offsetHeight + "px";
      // }
    };
    // 绑定事件
    theTool.bindEvent = function () {
      const self = this;
      //遮罩层点击事件,关闭menuList
      self.dom.maskBg.addEventListener("click", function () {
        self.hiddenMenu();
      });
      //工具条点击事件，关闭menuList
      self.dom.pannel.addEventListener("click", function () {
        hiddenMenu(rootElement);
      });
      //菜单点击事件
      self.dom.menu.addEventListener("click", handle_Menu_click);
      
    };
    theTool.init();
  },
  //隐藏工具条
  HiddenSelf: function (containerID) {
    var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
    let toolPanel = rootElement.querySelector(".DC-toolBar-panel");
    var oldStyle = toolPanel.style.display;
    toolPanel.style.display = "none";
    //已打开的子菜单
    let oldchildMenu = rootElement.querySelector("#dc_childMenu");
    if (oldchildMenu) {
      oldchildMenu.classList.remove("show");
    }
    //已打开的颜色选择器
    let colorParentDiv = rootElement.querySelector(".DC-renderColorPickerBox");
    if (colorParentDiv) {
      colorParentDiv.style.visibility = "hidden";
    }
    if (oldStyle != "none") {
      //拿到外层包裹元素pageContainer
      var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
      if (pageContainer) {
        pageContainer.style.height = `calc(100% - 24px)`;
      }
    }
  },
  ShowSelf: function (containerID) {
    var rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
    let toolPanel = rootElement.querySelector(".DC-toolBar-panel");
    var oldStyle = "none";
    if (toolPanel) {
      oldStyle = toolPanel.style.display;
      toolPanel.style.display = "block";
      var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
      if (pageContainer) {
        pageContainer.style.height = `calc(100% - 24px - ${toolPanel.offsetHeight}px)`;
      }
    } else this.CreateToolBarControl(containerID);
  },
  Unload: function (containerID) {},
};

let handle_Menu_click = function (e) {
  const el = e.target;
  if (el.name) {
    e.stopPropagation();
    let menuOpt = MenuOptionList[el.name + "Menu"];
    // console.log(menuOpt);
    let rootElement = DCTools20221228.GetOwnerWriterControl(el._rootElementID);
    showMaskBg(rootElement);
    let menuListDOM = rootElement.querySelector(".DC-toolBar-panel-menuList");
    menuListDOM.style.left = el.offsetLeft + "px";
    menuListDOM.classList.add("show");
    //构建菜单下拉框
    build_MenuList(menuOpt, rootElement, menuListDOM);
  }
};

let build_MenuList = function (menuOpt, rootElement, menuListDOM) {
  menuListDOM.innerHTML = "";
  if (menuOpt && menuOpt.length > 0) {
    let ulDOM = rootElement.ownerDocument.createElement("ul");
    for (let i = 0; i < menuOpt.length; i++) {
      const element = menuOpt[i];
      let liDOM = rootElement.ownerDocument.createElement("li");
      //图标
      if (element.icon && SVG_Dictionary[element.icon]) {
        liDOM.innerHTML += `<div>${SVG_Dictionary[element.icon]}</div>`;
      } else {
        liDOM.innerHTML += `<div>${SVG_Dictionary["ri-tools-line"]}</div>`;
      }
      //如果有点击事件
      if (element.onClick && typeof element.onClick === "function") {
        liDOM.addEventListener("click", () => {
          hiddenMenu(rootElement);
          element.onClick(rootElement);
        });
      }
      if (element.hasOption) {
        let options = selectOptionsList[element.optionName];
        if (options && options.length) {
          element.children = [];
          for (let i = 0; i < options.length; i++) {
            let child = {
              label: typeof options[i] == "object" ? options[i].label : options[i],
              onClick: () => {
                element.childrenOnclick(options[i], rootElement);
              },
            };
            element.children.push(child);
          }
        }
      }
      if (element.divided) {
        liDOM.classList.add("menu_item__divided");
      }
      liDOM.innerHTML += `<div>${element.label}</div>`;
      //如果有子元素
      if (element.children) {
        liDOM.classList.add("DC-toolBar-menuList-li-hasChild");
      }
      //为菜单添加鼠标移入事件
      liDOM.onmouseenter = (e) => {
        let liListMenuDOM = rootElement.querySelector("#dc_childMenu");
        if (!element.children && liListMenuDOM) {
          liListMenuDOM.classList.remove("show");
          return;
        } else if (!element.children) {
          return;
        }
        if (!liListMenuDOM) {
          liListMenuDOM = rootElement.ownerDocument.createElement("div");
          liListMenuDOM.id = "dc_childMenu";
          liListMenuDOM.classList.add("DC-toolBar-panel-menuList");
          let pannelDOM = rootElement.querySelector(".DC-toolBar-panel");
          pannelDOM.insertAdjacentElement("beforeend", liListMenuDOM);
        }
        liListMenuDOM.classList.add("show");
        // let liListMenuDOM = rootElement.ownerDocument.createElement("div");
        let _LIMenuDOM = build_LIMenuList(element.children, liListMenuDOM, rootElement);
        liListMenuDOM.style.left = e.target.offsetParent.offsetLeft + e.target.offsetParent.offsetWidth + "px";
        liListMenuDOM.style.top = e.target.offsetParent.offsetTop + e.target.offsetTop + "px";
        liListMenuDOM.style.maxHeight = rootElement.offsetHeight - (e.target.offsetTop + 30) + "px";
        liListMenuDOM.insertAdjacentElement("beforeend", _LIMenuDOM);
      };

      ulDOM.insertAdjacentElement("beforeend", liDOM);
    }
    menuListDOM.insertAdjacentElement("beforeend", ulDOM);
  }
};

let build_LIMenuList = function (menuOpt, parentNode, rootElement) {
  parentNode.innerHTML = "";
  if (menuOpt && menuOpt.length > 0) {
    let ulDOM = rootElement.ownerDocument.createElement("ul");
    for (let i = 0; i < menuOpt.length; i++) {
      const element = menuOpt[i];
      let liDOM = rootElement.ownerDocument.createElement("li");
      if (element.icon && SVG_Dictionary[element.icon]) {
        liDOM.innerHTML += `<div>${SVG_Dictionary[element.icon]}</div>`;
      } else if (element.icon) {
        liDOM.innerHTML += `<div>${SVG_Dictionary["ri-tools-line"]}</div>`;
      } else {
        liDOM.innerHTML += `<div></div>`;
        // liDOM.style.marginLeft='-5px'
      }
      if (element.onClick && typeof element.onClick === "function") {
        liDOM.onclick = (e) => {
          hiddenMenu(rootElement);
          element.onClick(rootElement);
        };
      }
      liDOM.innerHTML += `<div><span>${element.label}</span></div>`;
      //目前只有2级菜单 后续如果需要拓展，递归自己
      // if (element.children) {
      //   liDOM.classList.add("DC-toolBar-menuList-li-hasChild");
      //   liDOM.addEventListener("mouseenter", (e) => {
      //     let liListMenuDOM = rootElement.ownerDocument.createElement("div");
      //     liListMenuDOM.className = "DC-toolBar-panel-menuList";
      //   });
      // }

      ulDOM.insertAdjacentElement("beforeend", liDOM);
    }
    return ulDOM;
  }
};

let showMaskBg = function (rootElement) {
  let oldMaskBgDOM = rootElement.querySelector(".DC-toolBar-mask");
  if (oldMaskBgDOM) {
    oldMaskBgDOM.classList.add("display-block");
  } else {
    let pageContainer = WriterControl_UI.GetPageContainer(rootElement);
    let newMaskBgDOM = rootElement.ownerDocument.createElement("div");
    newMaskBgDOM.addEventListener("click", () => hiddenMenu(rootElement));
    newMaskBgDOM.classList.add("DC-toolBar-mask");
    newMaskBgDOM.classList.add("display-block");
    pageContainer.insertAdjacentElement("beforeend", newMaskBgDOM);
  }
};

let hiddenMenu = function (rootElement) {
  let oldmenuList = rootElement.querySelector(".DC-toolBar-panel-menuList");
  if (oldmenuList) {
    oldmenuList.classList.remove("show");
  }
  let oldmaskBg = rootElement.querySelector(".DC-toolBar-mask");
  if (oldmaskBg) {
    oldmaskBg.classList.remove("display-block");
  }
  // self.dom.menuList.classList.remove("show");
  // self.dom.maskBg.classList.remove("display-block");
  let oldchildMenu = rootElement.querySelector("#dc_childMenu");
  if (oldchildMenu) {
    oldchildMenu.classList.remove("show");
  }
  let colorParentDiv = rootElement.querySelector(".DC-renderColorPickerBox");
  if (colorParentDiv) {
    colorParentDiv.style.visibility = "hidden";
  }
};
let build_ToolBar = function (rootElementID) {
  let rootElement = DCTools20221228.GetOwnerWriterControl(rootElementID);
  let toolbarGroupDOM = rootElement.ownerDocument.createElement("div");
  for (let i = 0; i < toolsbar_groups.length; i++) {
    let groups = toolsbar_groups[i];
    if (groups.length > 0) {
      toolbarGroupDOM.classList.add("toolbar-group");
      for (let j = 0; j < groups.length; j++) {
        let tool = groups[j];
        let svgDOM = rootElement.ownerDocument.createElement("div");
        if (tool.icon && SVG_Dictionary[tool.icon]) {
          if (tool.hiddenLabel) {
            svgDOM.classList.add("only-toolbar-box-svg");
            svgDOM.innerHTML = SVG_Dictionary[tool.icon];
          } else {
            svgDOM.classList.add("toolbar-box-svg");
            svgDOM.innerHTML = SVG_Dictionary[tool.icon];
          }
        }
        if (tool.type == "select") {
          if (tool.optionName) {
            let options = selectOptionsList[tool.optionName];
            if (options && options.length) {
              let optionStr = "";
              for (let k = 0; k < options.length; k++) {
                optionStr += `<option value="${options[k]}">${options[k]}</option> `;
              }
              let selectDOM = rootElement.ownerDocument.createElement("select");
              selectDOM.innerHTML += optionStr;

              let selecDivDOM = rootElement.ownerDocument.createElement("div");
              selecDivDOM.name = tool.label;
              selecDivDOM.title = tool.label;
              selecDivDOM.classList.add("toolbar-box-select");
              // selecDivDOM.appendChild(selectDOM);
              selecDivDOM.insertAdjacentElement("beforeend", selectDOM);
              selectDOM.onchange = (e) => {
                rootElement.DCExecuteCommand(tool.commendName, false, e.target.value);
              };
              // toolbarGroupDOM.appendChild(selecDivDOM);
              toolbarGroupDOM.insertAdjacentElement("beforeend", selecDivDOM);
              selectDOM=null
              selecDivDOM=null
              
            }
          }
        } else {
          let toolDivDOM = rootElement.ownerDocument.createElement("div");
          toolDivDOM.name = tool.label;
          toolDivDOM.title = tool.label;
          toolDivDOM.classList.add("toolbar-box");
          toolDivDOM.insertAdjacentElement("beforeend", svgDOM);

          toolDivDOM.removeEventListener("click", null);
          toolDivDOM.onclick = (e) => {
            if (tool.onclick) {
              tool.onclick(e, rootElement);
            } else if (tool.commendName) {
              rootElement.DCExecuteCommand(tool.commendName, false, null);
            }
          };
          toolbarGroupDOM.insertAdjacentElement("beforeend", toolDivDOM);
          if (!tool.hiddenLabel) {
            let labelSpanDivDOM = rootElement.ownerDocument.createElement("div");
            labelSpanDivDOM.innerHTML = `<span>${tool.label}</span>`;
            toolDivDOM.insertAdjacentElement("beforeend", labelSpanDivDOM);
          }
        }
      }
    }
  }
  let toolBarDOM = rootElement.ownerDocument.createElement("div");
  toolBarDOM.classList.add("DC-toolBar-panel-toolsbar");
  let panelDOM = rootElement.querySelector(".DC-toolBar-panel");
  toolBarDOM.insertAdjacentElement("beforeend", toolbarGroupDOM);
  panelDOM.insertAdjacentElement("beforeend", toolBarDOM);
  //拿到外层包裹元素pageContainer
  var pageContainer = WriterControl_UI.GetPageContainer(rootElement);
  if (pageContainer) {
    pageContainer.style.height = pageContainer.offsetHeight - panelDOM.offsetHeight + "px";
  }
};

let buildColorPicker = function (e, commendName, rootElement) {
  showMaskBg(rootElement);
  e.stopPropagation();
  var closest = $(e.target).closest(".toolbar-box");
  let colorParentDiv = rootElement.querySelector(".DC-renderColorPickerBox");
  let oldflag = true;
  if (!colorParentDiv) {
    oldflag = false;
    colorParentDiv = rootElement.ownerDocument.createElement("div");
    colorParentDiv.classList.add("DC-renderColorPickerBox");
    colorParentDiv.innerHTML = colorPickerHTML;
    var styleDIV = rootElement.ownerDocument.createElement("style");
    styleDIV.innerHTML = colorPickerSTYLE;
    colorParentDiv.appendChild(styleDIV);
  }
  colorParentDiv.style.left = closest[0].offsetLeft + "px";
  colorParentDiv.style.top = closest[0].offsetTop + closest[0].height + "px";
  colorParentDiv.style.visibility = "visible";
  let toolPanel = rootElement.querySelector(".DC-toolBar-panel");
  toolPanel.insertAdjacentElement("beforeend", colorParentDiv);

  var colorRecentlyUsed = localStorage.getItem("colorRecentlyUsed") || "";
  var colorRecentlyUsedArr = colorRecentlyUsed ? JSON.parse(colorRecentlyUsed) : [];
  var RecentlyUsedDiv = document.querySelectorAll("#recentlyUsedDom>.dc_color_box");
  if (colorRecentlyUsedArr && colorRecentlyUsedArr.length) {
    colorRecentlyUsedArr.reverse(); //倒序最近颜色
    for (var i = 0; i <= colorRecentlyUsedArr.length; i++) {
      RecentlyUsedDiv[i] && (RecentlyUsedDiv[i].style["background-color"] = colorRecentlyUsedArr[i]);
    }
  }
  var dcColorPicker = rootElement.querySelector(".dc_inner_color_picker");
  dcColorPicker.commendName = commendName;
  if (oldflag) {
    return;
  }

  // dcColorPicker.removeEventListener("click",null);
  dcColorPicker.onclick = function (e) {
    e.stopPropagation();
    if (e.target.classList.contains("dc_color_box")) {
      var colorString = e.target.style["background-color"] || "";
      var setcolorRecentlyUsedArr = colorRecentlyUsed ? JSON.parse(colorRecentlyUsed) : [];
      //防止有重复颜色
      if (setcolorRecentlyUsedArr.indexOf(colorString) !== -1) {
        var index = setcolorRecentlyUsedArr.indexOf(colorString);
        setcolorRecentlyUsedArr.splice(index, 1);
      }
      // 始终只保留最近10次使用的颜色
      if (setcolorRecentlyUsedArr.length >= 10) {
        setcolorRecentlyUsedArr.shift();
      }
      setcolorRecentlyUsedArr.push(colorString);
      localStorage.setItem("colorRecentlyUsed", JSON.stringify(setcolorRecentlyUsedArr)); //保存颜色

      colorParentDiv.style.visibility = "hidden";
      rootElement.DCExecuteCommand(this.commendName, false, colorString);
    }
  };
};
let dcwriterFunc = {
  //打开本地文件
  DCWriterFileOpen: function (rootElement) {
    let file = rootElement.querySelector("input#dcInputFile");
    if (!file) {
      file = rootElement.ownerDocument.createElement("input");
      file.setAttribute("id", "dcInputFile");
      file.setAttribute("type", "file");
      file.setAttribute("accept", ".xml,.json,.rtf,.html,.htm,.odt");
      file.style.display = "none";
      rootElement.appendChild(file);
    }
    file.vlaue = "";
    file.click();
    // file文件选中事件
    file.onchange = function (ev) {
      let fileList = this.files;
      if (fileList.length > 0) {
        let reader = new FileReader();
        reader.readAsText(fileList[0], "UFT-8");
        reader.onload = function (e) {
          //获取到文件内容
          let strFileContent = e.target.result;
          //获取文件格式
          let fileFormat = "xml";
          fileFormat = fileList[0].name.substring(fileList[0].name.lastIndexOf(".") + 1);
          strFileContent = strFileContent.replace("&#x16;", " ");
          rootElement.LoadDocumentFromString(strFileContent, fileFormat);
        };
      }
      file.vlaue = "";
    };
  },
  /**
   * 编辑器导出文件方法
   * @param {*} ctl 编辑器元素
   * @param {*} format 下载的格式
   */
  DownLoadFile(ctl, format) {
    if (format == null || format == "") {
      format = "xml";
    }
    if (format == "doc") {
        this.DownLoadFiledoc(ctl);
    } else {
      ctl.DownLoadFile(format);
    }
  },

  DownLoadFiledoc(ctl) {
    var strFileName = Date.now() + ".doc";;
    ctl.DownLoadFile('rtf', strFileName, function (str) {
        let blob = new Blob([str], { type: "application/rtf" });
        let downloadElement = ctl.ownerDocument.createElement("a");
        let href = window.URL.createObjectURL(blob); //创建下载的链接
        downloadElement.href = href;
        downloadElement.download = strFileName;// file.name; //下载后文件名
        ctl.ownerDocument.body.appendChild(downloadElement);
        downloadElement.click(); //点击下载
        ctl.ownerDocument.body.removeChild(downloadElement); //下载完成移除元素
        window.URL.revokeObjectURL(href); //释放掉blob对象
    });
  },
};

let MenuOptionList = {
  // 文件下拉列表信息

  fileMenu: [
    //{
    //  label: "另存为",
    //  icon: "ri-file-copy-line",
    //  onClick: () => {
    //    this.$parent.Handle_SaveAsTemplate();
    //  },
    //},

    {
      label: "打开本地文件",
      icon: "ri-file-upload-line",
      onClick: (rootElement) => {
        dcwriterFunc.DCWriterFileOpen(rootElement);
      },
    },

    {
      label: "导出到本地",
      icon: "ri-file-download-line",
      children: [
        {
          label: "XML",
          icon: "ri-file-line",
          onClick: (rootElement) => {
            dcwriterFunc.DownLoadFile(rootElement, "xml");
          },
        },
        {
          label: "PDF",
          icon: "ri-file-pdf-2-fill",
          onClick: (rootElement) => {
            dcwriterFunc.DownLoadFile(rootElement, "local.pdf");
          },
        },
        {
          label: "DOC",
          icon: "ri-file-word-fill",
          onClick: (rootElement) => {
            dcwriterFunc.DownLoadFile(rootElement, "doc");
          },
        },
        {
          label: "TXT",
          icon: "ri-text",
          onClick: (rootElement) => {
            dcwriterFunc.DownLoadFile(rootElement, "text");
          },
        },
        {
          label: "HTML",
          icon: "ri-html5-fill",
          onClick: (rootElement) => {
            dcwriterFunc.DownLoadFile(rootElement, "html");
          },
        },
        {
          label: "JSON",
          icon: "ri-javascript-line",
          onClick: (rootElement) => {
            dcwriterFunc.DownLoadFile(rootElement, "json");
          },
        },
      ],
    },
    {
      label: "打印",
      divided: true,
      icon: "ri-printer-line",
      onClick: (rootElement) => {
        rootElement.PrintDocument();
      },
    },
    {
      label: "打印预览",
      icon: "ri-eye-line",
      onClick: (rootElement) => {
        rootElement.LoadPrintPreview();
      },
    },
    {
      label: "关闭打印预览",
      icon: "ri-eye-off-line",
      onClick: (rootElement) => {
        rootElement.ClosePrintPreview();
      },
    },
    {
      label: "清空",
      icon: "ri-eraser-fill",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("FileNew", false, null);
      },
    },
  ],
  // 格式下拉列表信息
  formatMenu: [
    {
      label: "粗体",
      icon: "ri-bold",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Bold", true, null);
      },
    },
    {
      label: "斜体",
      icon: "ri-italic",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Italic", true, null);
      },
    },
    {
      label: "下划线",
      icon: "ri-underline",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("UnderLine", true, null);
      },
    },
    {
      label: "删除线",
      icon: "ri-strikethrough",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Strikeout", true, null);
      },
    },
    {
      label: "字符边框",
      divided: true,
      icon: "ri-t-box-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Fontborder", true, null);
      },
    },
    {
      label: "文字套圈",
      icon: "ri-loader-3-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("CharacterCircle", true, {});
      },
    },
    {
      label: "格式刷",
      divided: true,
      icon: "ri-paint-brush-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("FormatBrush", false);
      },
    },
    {
      label: "字号",
      icon: "ri-font-size-2",
      hasOption: true,
      optionName: "fontSize",
      childrenOnclick: function (option, rootElement) {
        rootElement.DCExecuteCommand("FontSize", true, option + "");
      },
      // children: selectOptionsList.fontSize,
    },
    {
      label: "字体",
      icon: "ri-font-family",
      hasOption: true,
      optionName: "fontFamilys",
      childrenOnclick: function (option, rootElement) {
        rootElement.DCExecuteCommand("FontName", true, option + "");
      },

      // children: selectOptionsList.fontFamilys,
    },
    {
      label: "上标",
      divided: true,
      icon: "ri-superscript",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Superscript", false);
      },
    },
    {
      label: "下标",
      icon: "ri-subscript",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Subscript", false);
      },
    },
  ],
  // 段落下拉列表信息
  paragraphMenu: [
    {
      label: "居左",
      icon: "ri-align-left",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("AlignLeft", false);
      },
    },
    {
      label: "居中",
      icon: "ri-align-center",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("AlignCenter", false);
      },
    },
    {
      label: "居右",
      icon: "ri-align-right",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("AlignRight", false);
      },
    },
    {
      label: "分散对齐",
      icon: "ri-align-justify",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("AlignDistribute", false);
      },
    },
    {
      label: "首行缩进",
      divided: true,
      icon: "ri-indent-increase",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Indent", false);
      },
    },
    {
      label: "悬挂缩进",
      icon: "ri-indent-decrease",
      onClick: (rootElement) => {
        var options = {
          firstlineindent: "-100",
          leftindent: "100",
        };
        rootElement.DCExecuteCommand("hangingindent", false, options);
      },
    },
    {
      label: "清空段落缩进",
      icon: "ri-format-clear",
      onClick: (rootElement) => {
        var options = {
          firstlineindent: "0",
          leftindent: "0",
        };
        rootElement.DCExecuteCommand("hangingindent", false, options);
      },
    },
    {
      label: "行间距",
      icon: "ri-line-height",
      divided: true,
      hasOption: true,
      optionName: "lineheight",
      childrenOnclick: function (option, rootElement) {
        rootElement.DCExecuteCommand("lineheight", true, option);
      },
      // children: lineheightListArr,
    },
    {
      label: "有序列表",
      divided: true,
      icon: "ri-list-ordered",
      hasOption: true,
      optionName: "orderList",
      childrenOnclick: function (option, rootElement) {
        var options = {
          liststyle: option.value,
        };
        rootElement.DCExecuteCommand("insertorderedlist", true, options);
      },
      // children: orderlistArr,
    },
    {
      label: "无序列表",
      icon: "ri-list-unordered",
      hasOption: true,
      optionName: "unorderList",
      childrenOnclick: function (option, rootElement) {
        var options = {
          liststyle: option.value,
        };
        rootElement.DCExecuteCommand("insertunorderedlist", true, options);
      },
      // children: unorderlistArr,
    },
    {
      label: "清空有序/无序列表",
      icon: "ri-format-clear",
      onClick: (rootElement) => {
        rootElement.SetCurrentParagraphStyle({
          ParagraphListStyle: "None",
        });
      },
    },
    {
      label: "整体段落设置对话框",
      divided: true,
      icon: "ri-paragraph",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("paragraphformat", true);
      },
    },
  ],
  // 常规下拉列表信息
  normalMenu: [
    {
      label: "剪切(Ctrl+X)",
      icon: "ri-scissors-2-fill",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Cut", false, null);
      },
    },
    {
      label: "复制(Ctrl+C)",
      icon: "ri-file-copy-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Copy", false, null);
      },
    },
    {
      label: "粘贴(Ctrl+V)",
      icon: "ri-clipboard-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Paste", false, null);
      },
    },

    {
      label: "纯文本复制",
      divided: true,
      icon: "ri-file-copy-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("CopyAsText", false, null);
      },
    },
    {
      label: "纯文本粘贴",
      icon: "ri-clipboard-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("PasteAsText", false, null);
      },
    },
    {
      label: "撤销",
      divided: true,
      icon: "ri-arrow-go-back-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Undo", false, null);
      },
    },
    {
      label: "重做",
      icon: "ri-arrow-go-forward-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Redo", false, null);
      },
    },
    {
      label: "查找&替换",
      divided: true,
      icon: "ri-search-line",
      onClick: (rootElement) => {
        rootElement.SearchAndReplaceDialog();
      },
    },
  ],
  // 设计下拉列表信息
  designMenu: [
    {
      label: "页面设置",
      icon: "ri-file-settings-line",
      onClick: (rootElement) => {
        rootElement.DocumentSettingsDialog();
      },
    },
    {
      label: "水印",
      icon: "ri-water-percent-line",
      onClick: (rootElement) => {
        rootElement.WatermarkDialog();
      },
    },
    {
      label: "网格线",
      icon: "ri-grid-line",
      onClick: (rootElement) => {
        rootElement.DocumentGridLineDialog();
      },
    },
    {
      label: "装订线",
      icon: "ri-file-settings-line",
      onClick: (rootElement) => {
        rootElement.DocumentGutterDialog();
      },
    },
    {
      label: "插入页码",
      icon: "ri-number-1",
      divided: true,
      onClick: (rootElement) => {
        var options = {
          ID: "pageinfo1", //页码ID，可为空
          Height: "65", //页码元素高度，可为空
          Width: "400", //页码元素宽度，可为空
          ValueType: "PageIndex", //页码元素类型,PageIndex代表显示为当前页号，NumOfPages显示为总页数，可为空
          FormatString: "第[%PageIndex%]页 共[%NumOfPages%]页", //页码文本格式化字符串，可为空
          //SpecifyPageIndexTextList: "1,2,3,4"//自定义页码序号列表，可为空
        };
        rootElement.DCExecuteCommand("InsertPageInfoElement", true, options);
      },
    },
  ],
  // 表格下拉列表信息
  tableMenu: [
    {
      label: "插入表格",
      icon: "ri-table-fill",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("InsertTable", true, null);
      },
    },
    {
      label: "删除表格",
      icon: "ri-delete-bin-line",
      // disabled: !cell,
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("Table_DeleteTable", false);
      },
    },
    {
      label: "表格自适应宽度",
      icon: "ri-font-family",
      // disabled: !cell,
      onClick: (rootElement) => {
        rootElement.AutoFixTableWidth();
      },
    },
    {
      label: "表格边框",
      icon: "ri-shape-line",
      // disabled: !cell,
      onClick: (rootElement) => {
        rootElement.bordersShadingDialog();
      },
    },
    {
      label: "表格属性",
      icon: "ri-table-2",
      // disabled: !cell,
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("tableproperties", true, null);
      },
    },
    {
      label: "表格行",
      icon: "ri-layout-row-fill",
      divided: true,
      children: [
        {
          label: "在上面插入表格行",
          icon: "ri-insert-row-top",
          // disabled: !cell,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("Table_InsertRowUp", true);
          },
        },
        {
          label: "在下面插入表格行",
          icon: "ri-insert-row-bottom",
          // disabled: !cell,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("Table_InsertRowDown", true);
          },
        },
        {
          label: "删除表格行",
          icon: "ri-delete-row",
          // disabled: !cell,
          divided: true,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("Table_DeleteRow", false);
          },
        },
        {
          label: "表格行属性",
          // disabled: !cell,
          divided: true,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("tablerowproperties", true, null);
          },
        },
      ],
    },
    {
      label: "表格列",
      icon: "ri-layout-column-fill",

      children: [
        {
          label: "在左面插入表格列",
          icon: "ri-insert-column-left",
          // disabled: !cell,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("Table_InsertColumnLeft", true);
          },
        },
        {
          label: "在右面插入表格列",
          icon: "ri-insert-column-right",
          // disabled: !cell,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("Table_InsertColumnRight", true);
          },
        },
        {
          label: "删除表格列",
          icon: "ri-delete-column",
          // disabled: !cell,
          divided: true,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("Table_DeleteColumn", false);
          },
        },
        {
          label: "表格列属性",
          // disabled: !cell,
          divided: true,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("tableColumnproperties", true, null);
          },
        },
      ],
    },
    {
      label: "单元格",
      icon: "ri-rectangle-line",
      children: [
        {
          label: "合并单元格",
          icon: "ri-merge-cells-horizontal",
          // disabled: !cell,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("Table_MergeCell", false, null);
          },
        },
        {
          label: "拆分单元格",
          icon: "ri-split-cells-horizontal",
          // disabled: !cell,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("Table_SplitCellExt", true, null);
          },
        },
        {
          label: "单元格网格线",
          icon: "ri-merge-cells-horizontal",
          // disabled: !cell,
          divided: true,
          onClick: (rootElement) => {
            rootElement.cellGridlineDialog();
          },
        },
        {
          label: "单元格斜分割线",
          icon: "ri-slash-commands-2",
          // disabled: !cell,
          onClick: (rootElement) => {
            rootElement.cellDiagonalLineDialog();
          },
        },
        {
          label: "单元格背景编号",
          icon: "",
          divided: true,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("ShowBackgroundCellID", false);
          },
        },
        {
          label: "单元格边框",
          icon: "",
          // disabled: !cell,
          divided: true,
          onClick: (rootElement) => {
            rootElement.borderShadingcellDialog();
          },
        },
        {
          label: "单元格属性",
          // disabled: !cell,
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("tablecellproperties", true, null);
          },
        },
      ],
    },
    {
      label: "单元格对齐方式",
      divided: true,
      icon: "ri-align-justify",
      children: [
        {
          label: "顶端左对齐",
          // disabled: !cell,
          // icon:"ri-align-left",
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignTopLeft", false);
          },
        },
        {
          label: "顶端右对齐",
          // disabled: !cell,
          // icon:'ri-align-right',
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignTopRight", false);
          },
        },
        {
          label: "顶端中间对齐",
          // disabled: !cell,
          // icon:'ri-align-justify',
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignTopCenter", false);
          },
        },
        {
          label: "垂直居中水平左对齐",
          // disabled: !cell,
          // icon:'ri-align-vertically',
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignMiddleLeft", false);
          },
        },
        {
          label: "垂直居中水平右对齐",
          // disabled: !cell,
          // icon:'ri-align-justify',
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignMiddleRight", false);
          },
        },
        {
          label: "垂直居中水平中间对齐",
          // disabled: !cell,
          // icon:'ri-align-justify',
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignMiddleCenter", false);
          },
        },
        {
          label: "底端左对齐",
          // disabled: !cell,
          // icon:'ri-align-justify',
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignBottomLeft", false);
          },
        },
        {
          label: "底端右对齐",
          // disabled: !cell,
          // icon:'ri-align-justify',
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignBottomRight", false);
          },
        },
        {
          label: "底端居中对齐",
          // disabled: !cell,
          // icon:'ri-align-justify',
          onClick: (rootElement) => {
            rootElement.DCExecuteCommand("AlignBottomCenter", false);
          },
        },
      ],
    },
  ],
  // 插入下拉列表信息
  insertMenu: [
    {
      label: "插入输入域",
      icon: "ri-input-method-line",
      onClick: (rootElement) => {
        var allinputs = rootElement.GetAllInputFields();
        // console.log(allinputs);
        var options = {
          ID: "field" + ++allinputs.length,
        };
        rootElement.DCExecuteCommand("InsertInputField", true, options);
      },
    },
    {
      label: "插入单选框",
      icon: "ri-radio-button-line",
      onClick: (rootElement) => {
        var options = {
          Name: "rad" + Math.random(), //单选框的Name属性相同
          Type: "radio", //radio、checkbox
          CaptionFlowLayout: true,
        };
        rootElement.DCExecuteCommand("InsertCheckBoxOrRadio", true, options);
      },
    },
    {
      label: "插入复选框",
      icon: "ri-checkbox-line",
      onClick: (rootElement) => {
        var options = {
          Name: "chk" + Math.random(), //单选框的Name属性相同
          Type: "checkbox", //radio、checkbox
          CaptionFlowLayout: true,
        };
        rootElement.DCExecuteCommand("InsertCheckBoxOrRadio", true, options);
      },
    },
    {
      label: "插入水平线",
      icon: "ri-separator",
      onClick: (rootElement) => {
        var options = {
          LineLengthInCM: "100px",
          LineSize: "10px",
          Color: "red",
          LineStyle: "dashed",
          ID: "hr1",
        };
        rootElement.DCExecuteCommand("InsertHorizontalLine", true, options);
      },
    },
    {
      label: "插入按钮",
      icon: "ri-bold",
      onClick: (rootElement) => {
        var options = {
          ID: "",
          Name: "",
          Text: "按钮文本",
          Height: "100",
          Width: "200",
        };
        rootElement.DCExecuteCommand("InsertButton", true, options);
      },
    },
    {
      label: "插入本地图片",
      icon: "ri-image-add-fill",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("InsertImage", true);
      },
    },
    {
      label: "插入医学表达式",
      icon: "ri-stethoscope-fill",
      // children: MedicalExpressionItems,
      hasOption: true,
      optionName: "medicalExpression",
      childrenOnclick: function (option, rootElement) {
        rootElement.DCExecuteCommand("insertmedicalexpression", true, option);
      },
    },
    {
      label: "插入条形码",
      icon: "ri-barcode-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("InsertBarcodeElement", true);
      },
    },
    {
      label: "插入二维码",
      icon: "ri-qr-code-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommand("InsertTDBarcodeElement", true);
      },
    },
  ],
  // 高级下拉列表信息
  otherMenu: [
    {
      label: "执行命令",
      icon: "ri-terminal-line",
      onClick: (rootElement) => {
        rootElement.DCExecuteCommandDialog();
      },
    },
    {
      label: "关于",
      icon: "ri-copyright-line",
      onClick: (rootElement) => {
        rootElement.AboutControl();
      },
    },
    // {
    //   label: "显示隐藏输入域",
    //   icon: "el-icon-view",
    //   onClick: () => {
    //     // this.$parent.Handle_OpenVisibleAttributeDialog();
    //   },
    // },
    // {
    //   label: "显示模板自定义属性",
    //   icon: "el-icon-view",
    //   onClick: () => {
    //     // this.$parent.Handle_OpenTemplateAttributeDialog();
    //   },
    // },
    // // {
    // //   label: '文档默认页面设置',
    // //   icon: 'el-icon-view',
    // //   onClick: () => {
    // //     this.$parent.Handle_OpenDefaultPagesizeSettingDialog();
    // //   },
    // // },
    // {
    //   label: "批量更改输入域",
    //   icon: "el-icon-view",
    //   onClick: () => {
    //     // this.$parent.Handle_OpenBatchModifyAttributeDialog();
    //   },
    // },
  ],
};
let SVG_Dictionary = {
  "ri-save-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M7 19V13H17V19H19V7.82843L16.1716 5H5V19H7ZM4 3H17L21 7V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3ZM9 15V19H15V15H9Z"></path></svg>',

  "ri-arrow-go-back-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5.82843 6.99955L8.36396 9.53509L6.94975 10.9493L2 5.99955L6.94975 1.0498L8.36396 2.46402L5.82843 4.99955H13C17.4183 4.99955 21 8.58127 21 12.9996C21 17.4178 17.4183 20.9996 13 20.9996H4V18.9996H13C16.3137 18.9996 19 16.3133 19 12.9996C19 9.68584 16.3137 6.99955 13 6.99955H5.82843Z"></path></svg>',

  "ri-arrow-go-forward-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M18.1716 6.99955H11C7.68629 6.99955 5 9.68584 5 12.9996C5 16.3133 7.68629 18.9996 11 18.9996H20V20.9996H11C6.58172 20.9996 3 17.4178 3 12.9996C3 8.58127 6.58172 4.99955 11 4.99955H18.1716L15.636 2.46402L17.0503 1.0498L22 5.99955L17.0503 10.9493L15.636 9.53509L18.1716 6.99955Z"></path></svg>',

  "ri-scissors-2-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 13.4108L9.44618 15.9646C9.79807 16.5601 10 17.2548 10 17.9966C10 20.2057 8.20914 21.9966 6 21.9966C3.79086 21.9966 2 20.2057 2 17.9966C2 15.7874 3.79086 13.9966 6 13.9966C6.74181 13.9966 7.43645 14.1985 8.03197 14.5504L10.5858 11.9966L4.56497 5.97577C3.78392 5.19472 3.78392 3.92839 4.56497 3.14734L12 10.5824L19.435 3.14734C20.2161 3.92839 20.2161 5.19472 19.435 5.97577L13.4142 11.9966L15.968 14.5504C16.5635 14.1985 17.2582 13.9966 18 13.9966C20.2091 13.9966 22 15.7874 22 17.9966C22 20.2057 20.2091 21.9966 18 21.9966C15.7909 21.9966 14 20.2057 14 17.9966C14 17.2548 14.2019 16.5601 14.5538 15.9646L12 13.4108ZM6 19.9966C7.10457 19.9966 8 19.1012 8 17.9966C8 16.892 7.10457 15.9966 6 15.9966C4.89543 15.9966 4 16.892 4 17.9966C4 19.1012 4.89543 19.9966 6 19.9966ZM18 19.9966C19.1046 19.9966 20 19.1012 20 17.9966C20 16.892 19.1046 15.9966 18 15.9966C16.8954 15.9966 16 16.892 16 17.9966C16 19.1012 16.8954 19.9966 18 19.9966Z"></path></svg>',

  "ri-file-copy-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M6.9998 6V3C6.9998 2.44772 7.44752 2 7.9998 2H19.9998C20.5521 2 20.9998 2.44772 20.9998 3V17C20.9998 17.5523 20.5521 18 19.9998 18H16.9998V20.9991C16.9998 21.5519 16.5499 22 15.993 22H4.00666C3.45059 22 3 21.5554 3 20.9991L3.0026 7.00087C3.0027 6.44811 3.45264 6 4.00942 6H6.9998ZM5.00242 8L5.00019 20H14.9998V8H5.00242ZM8.9998 6H16.9998V16H18.9998V4H8.9998V6Z"></path></svg>',

  "ri-clipboard-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M7 4V2H17V4H20.0066C20.5552 4 21 4.44495 21 4.9934V21.0066C21 21.5552 20.5551 22 20.0066 22H3.9934C3.44476 22 3 21.5551 3 21.0066V4.9934C3 4.44476 3.44495 4 3.9934 4H7ZM7 6H5V20H19V6H17V8H7V6ZM9 4V6H15V4H9Z"></path></svg>',

  "ri-bold":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M8 11H12.5C13.8807 11 15 9.88071 15 8.5C15 7.11929 13.8807 6 12.5 6H8V11ZM18 15.5C18 17.9853 15.9853 20 13.5 20H6V4H12.5C14.9853 4 17 6.01472 17 8.5C17 9.70431 16.5269 10.7981 15.7564 11.6058C17.0979 12.3847 18 13.837 18 15.5ZM8 13V18H13.5C14.8807 18 16 16.8807 16 15.5C16 14.1193 14.8807 13 13.5 13H8Z"></path></svg>',

  "ri-italic":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M15 20H7V18H9.92661L12.0425 6H9V4H17V6H14.0734L11.9575 18H15V20Z"></path></svg>',

  "ri-underline":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M8 3V12C8 14.2091 9.79086 16 12 16C14.2091 16 16 14.2091 16 12V3H18V12C18 15.3137 15.3137 18 12 18C8.68629 18 6 15.3137 6 12V3H8ZM4 20H20V22H4V20Z"></path></svg>',

  "ri-strikethrough":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M17.1538 14C17.3846 14.5161 17.5 15.0893 17.5 15.7196C17.5 17.0625 16.9762 18.1116 15.9286 18.867C14.8809 19.6223 13.4335 20 11.5862 20C9.94674 20 8.32335 19.6185 6.71592 18.8555V16.6009C8.23538 17.4783 9.7908 17.917 11.3822 17.917C13.9333 17.917 15.2128 17.1846 15.2208 15.7196C15.2208 15.0939 15.0049 14.5598 14.5731 14.1173C14.5339 14.0772 14.4939 14.0381 14.4531 14H3V12H21V14H17.1538ZM13.076 11H7.62908C7.4566 10.8433 7.29616 10.6692 7.14776 10.4778C6.71592 9.92084 6.5 9.24559 6.5 8.45207C6.5 7.21602 6.96583 6.165 7.89749 5.299C8.82916 4.43299 10.2706 4 12.2219 4C13.6934 4 15.1009 4.32808 16.4444 4.98426V7.13591C15.2448 6.44921 13.9293 6.10587 12.4978 6.10587C10.0187 6.10587 8.77917 6.88793 8.77917 8.45207C8.77917 8.87172 8.99709 9.23796 9.43293 9.55079C9.86878 9.86362 10.4066 10.1135 11.0463 10.3004C11.6665 10.4816 12.3431 10.7148 13.076 11H13.076Z"></path></svg>',
  "ri-font-color":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5.55397 22H3.3999L10.9999 3H12.9999L20.5999 22H18.4458L16.0458 16H7.95397L5.55397 22ZM8.75397 14H15.2458L11.9999 5.88517L8.75397 14Z"></path></svg>',
  "ri-palette-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 2C17.5222 2 22 5.97778 22 10.8889C22 13.9556 19.5111 16.4444 16.4444 16.4444H14.4778C13.5556 16.4444 12.8111 17.1889 12.8111 18.1111C12.8111 18.5333 12.9778 18.9222 13.2333 19.2111C13.5 19.5111 13.6667 19.9 13.6667 20.3333C13.6667 21.2556 12.9 22 12 22C6.47778 22 2 17.5222 2 12C2 6.47778 6.47778 2 12 2ZM10.8111 18.1111C10.8111 16.0843 12.451 14.4444 14.4778 14.4444H16.4444C18.4065 14.4444 20 12.851 20 10.8889C20 7.1392 16.4677 4 12 4C7.58235 4 4 7.58235 4 12C4 16.19 7.2226 19.6285 11.324 19.9718C10.9948 19.4168 10.8111 18.7761 10.8111 18.1111ZM7.5 12C6.67157 12 6 11.3284 6 10.5C6 9.67157 6.67157 9 7.5 9C8.32843 9 9 9.67157 9 10.5C9 11.3284 8.32843 12 7.5 12ZM16.5 12C15.6716 12 15 11.3284 15 10.5C15 9.67157 15.6716 9 16.5 9C17.3284 9 18 9.67157 18 10.5C18 11.3284 17.3284 12 16.5 12ZM12 9C11.1716 9 10.5 8.32843 10.5 7.5C10.5 6.67157 11.1716 6 12 6C12.8284 6 13.5 6.67157 13.5 7.5C13.5 8.32843 12.8284 9 12 9Z"></path></svg>',
  "ri-align-left":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3 4H21V6H3V4ZM3 19H17V21H3V19ZM3 14H21V16H3V14ZM3 9H17V11H3V9Z"></path></svg>',
  "ri-align-center":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3 4H21V6H3V4ZM5 19H19V21H5V19ZM3 14H21V16H3V14ZM5 9H19V11H5V9Z"></path></svg>',
  "ri-align-right":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3 4H21V6H3V4ZM7 19H21V21H7V19ZM3 14H21V16H3V14ZM7 9H21V11H7V9Z"></path></svg>',
  "ri-align-justify":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3 4H21V6H3V4ZM3 19H21V21H3V19ZM3 14H21V16H3V14ZM3 9H21V11H3V9Z"></path></svg>',
  "ri-omega":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M14 20V17.8432C15.8631 16.6512 17.5 13.9677 17.5 10.8844C17.5 7.8107 15.5 4.85516 12 4.85516C8.5 4.85516 6.5 7.8107 6.5 10.8844C6.5 13.9677 8.13687 16.6512 10 17.8432V20H3V18H7.7597C5.66635 16.5054 4 13.9889 4 10.8844C4 6.24653 7.5 3 12 3C16.5 3 20 6.24653 20 10.8844C20 13.9889 18.3336 16.5054 16.2403 18H21V20H14Z"></path></svg>',
  "ri-image-add-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M21 15V18H24V20H21V23H19V20H16V18H19V15H21ZM21.0082 3C21.556 3 22 3.44495 22 3.9934V13H20V5H4V18.999L14 9L17 12V14.829L14 11.8284L6.827 19H14V21H2.9918C2.44405 21 2 20.5551 2 20.0066V3.9934C2 3.44476 2.45531 3 2.9918 3H21.0082ZM8 7C9.10457 7 10 7.89543 10 9C10 10.1046 9.10457 11 8 11C6.89543 11 6 10.1046 6 9C6 7.89543 6.89543 7 8 7Z"></path></svg>',
  "ri-pencil-ruler-2-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M7.05033 14.1213L4.929 16.2427L7.75743 19.0711L19.0711 7.75737L16.2427 4.92894L14.1214 7.05026L15.5356 8.46448L14.1214 9.87869L12.7072 8.46448L11.293 9.87869L12.7072 11.2929L11.293 12.7071L9.87875 11.2929L8.46454 12.7071L9.87875 14.1213L8.46454 15.5355L7.05033 14.1213ZM16.9498 2.80762L21.1925 7.05026C21.583 7.44079 21.583 8.07395 21.1925 8.46448L8.46454 21.1924C8.07401 21.5829 7.44085 21.5829 7.05033 21.1924L2.80768 16.9498C2.41716 16.5592 2.41716 15.9261 2.80768 15.5355L15.5356 2.80762C15.9261 2.4171 16.5593 2.4171 16.9498 2.80762ZM14.1214 18.3635L15.5356 16.9493L17.7781 19.1918H19.1923V17.7776L16.9498 15.5351L18.364 14.1208L20.9997 16.7565V20.9999H16.7578L14.1214 18.3635ZM5.63597 9.87806L2.80754 7.04963C2.41702 6.65911 2.41702 6.02594 2.80754 5.63542L5.63597 2.80699C6.02649 2.41647 6.65966 2.41647 7.05018 2.80699L9.87861 5.63542L8.4644 7.04963L6.34308 4.92831L4.92886 6.34253L7.05018 8.46385L5.63597 9.87806Z"></path></svg>',
  "ri-loop-right-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 4C14.7486 4 17.1749 5.38626 18.6156 7.5H16V9.5H22V3.5H20V5.99936C18.1762 3.57166 15.2724 2 12 2C6.47715 2 2 6.47715 2 12H4C4 7.58172 7.58172 4 12 4ZM20 12C20 16.4183 16.4183 20 12 20C9.25144 20 6.82508 18.6137 5.38443 16.5H8V14.5H2V20.5H4V18.0006C5.82381 20.4283 8.72764 22 12 22C17.5228 22 22 17.5228 22 12H20Z"></path></svg>',
  "ri-tools-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5.32943 3.27152C6.56252 2.83314 7.9923 3.10743 8.97927 4.0944C10.1002 5.21531 10.3019 6.90735 9.5843 8.23378L20.293 18.9436L18.8788 20.3579L8.16982 9.64869C6.84325 10.3668 5.15069 10.1653 4.02952 9.04415C3.04227 8.0569 2.7681 6.62659 3.20701 5.39326L5.44373 7.62994C6.02952 8.21572 6.97927 8.21572 7.56505 7.62994C8.15084 7.04415 8.15084 6.0944 7.56505 5.50862L5.32943 3.27152ZM15.6968 5.15506L18.8788 3.38729L20.293 4.80151L18.5252 7.98349L16.7574 8.33704L14.6361 10.4584L13.2219 9.04415L15.3432 6.92283L15.6968 5.15506ZM8.97927 13.2868L10.3935 14.701L5.09018 20.0043C4.69966 20.3948 4.06649 20.3948 3.67597 20.0043C3.31334 19.6417 3.28744 19.0698 3.59826 18.6773L3.67597 18.5901L8.97927 13.2868Z"></path></svg>',
  "ri-file-upload-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M15 4H5V20H19V8H15V4ZM3 2.9918C3 2.44405 3.44749 2 3.9985 2H16L20.9997 7L21 20.9925C21 21.5489 20.5551 22 20.0066 22H3.9934C3.44476 22 3 21.5447 3 21.0082V2.9918ZM13 12V16H11V12H8L12 8L16 12H13Z"></path></svg>',
  "ri-file-download-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M13 12H16L12 16L8 12H11V8H13V12ZM15 4H5V20H19V8H15V4ZM3 2.9918C3 2.44405 3.44749 2 3.9985 2H16L20.9997 7L21 20.9925C21 21.5489 20.5551 22 20.0066 22H3.9934C3.44476 22 3 21.5447 3 21.0082V2.9918Z"></path></svg>',
  "ri-file-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M9 2.00318V2H19.9978C20.5513 2 21 2.45531 21 2.9918V21.0082C21 21.556 20.5551 22 20.0066 22H3.9934C3.44476 22 3 21.5501 3 20.9932V8L9 2.00318ZM5.82918 8H9V4.83086L5.82918 8ZM11 4V9C11 9.55228 10.5523 10 10 10H5V20H19V4H11Z"></path></svg>',
  "ri-file-pdf-2-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3.9985 2C3.44749 2 3 2.44405 3 2.9918V21.0082C3 21.5447 3.44476 22 3.9934 22H20.0066C20.5551 22 21 21.5489 21 20.9925L20.9997 7L16 2H3.9985ZM10.5 7.5H12.5C12.5 9.98994 14.6436 12.6604 17.3162 13.5513L16.8586 15.49C13.7234 15.0421 10.4821 16.3804 7.5547 18.3321L6.3753 16.7191C7.46149 15.8502 8.50293 14.3757 9.27499 12.6534C10.0443 10.9373 10.5 9.07749 10.5 7.5ZM11.1 13.4716C11.3673 12.8752 11.6043 12.2563 11.8037 11.6285C12.2754 12.3531 12.8553 13.0182 13.5102 13.5953C12.5284 13.7711 11.5666 14.0596 10.6353 14.4276C10.8 14.1143 10.9551 13.7948 11.1 13.4716Z"></path></svg>',
  "ri-file-word-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M16 2L21 7V21.0082C21 21.556 20.5551 22 20.0066 22H3.9934C3.44476 22 3 21.5447 3 21.0082V2.9918C3 2.44405 3.44495 2 3.9934 2H16ZM14 8V12.989L12 11L10.0108 13L10 8H8V16H10L12 14L14 16H16V8H14Z"></path></svg>',
  "ri-text":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M13 6V21H11V6H5V4H19V6H13Z"></path></svg>',
  "ri-html5-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 18.1778L16.6192 16.9222L17.2434 10.1444H9.02648L8.82219 7.88889H17.4477L17.6747 5.67778H6.32535L6.96091 12.3556H14.7806L14.5195 15.2222L12 15.8889L9.48045 15.2222L9.32156 13.3778H7.0517L7.38083 16.9222L12 18.1778ZM3 2H21L19.377 20L12 22L4.62295 20L3 2Z"></path></svg>',
  "ri-javascript-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M13.3344 16.055 12.4764 17.243C13.2904 17.969 14.3024 18.332 15.5124 18.332 16.4364 18.31 17.1404 18.0717 17.6244 17.617 18.1157 17.155 18.3614 16.605 18.3614 15.967 18.3614 15.3437 18.1891 14.8303 17.8444 14.427 17.4997 14.0237 16.9204 13.701 16.1064 13.459 15.4317 13.2537 14.9551 13.0667 14.6764 12.898 14.3977 12.722 14.2584 12.5093 14.2584 12.26 14.2584 12.0327 14.3721 11.8493 14.5994 11.71 14.8267 11.5707 15.1311 11.501 15.5124 11.501 15.7911 11.501 16.1064 11.556 16.4584 11.666 16.8104 11.7613 17.1221 11.9153 17.3934 12.128L18.1634 10.929C17.4887 10.3863 16.5941 10.115 15.4794 10.115 14.6801 10.115 14.0237 10.3203 13.5104 10.731 12.9824 11.1417 12.7184 11.6513 12.7184 12.26 12.7257 12.9053 12.9384 13.4077 13.3564 13.767 13.7817 14.1263 14.3867 14.4197 15.1714 14.647 15.8241 14.8523 16.2677 15.0577 16.5024 15.263 16.7297 15.4683 16.8434 15.7177 16.8434 16.011 16.8434 16.297 16.7297 16.517 16.5024 16.671 16.2677 16.8323 15.9304 16.913 15.4904 16.913 14.7717 16.9203 14.0531 16.6343 13.3344 16.055ZM7.80405 16.693C7.58405 16.561 7.37872 16.3667 7.18805 16.11L6.15405 16.957C6.46205 17.4777 6.84339 17.8407 7.29805 18.046 7.72339 18.2367 8.21105 18.332 8.76105 18.332 9.06172 18.332 9.37339 18.2843 9.69605 18.189 10.0187 18.0937 10.3157 17.9323 10.5871 17.705 11.0637 17.3237 11.3131 16.7003 11.3351 15.835V10.247H9.85005V15.549C9.85005 16.055 9.73639 16.4107 9.50905 16.616 9.28172 16.814 8.99572 16.913 8.65105 16.913 8.32105 16.913 8.03872 16.8397 7.80405 16.693ZM3 6C3 4.34315 4.34315 3 6 3H18C19.6569 3 21 4.34315 21 6V18C21 19.6569 19.6569 21 18 21H6C4.34315 21 3 19.6569 3 18V6ZM6 5C5.44772 5 5 5.44772 5 6V18C5 18.5523 5.44772 19 6 19H18C18.5523 19 19 18.5523 19 18V6C19 5.44772 18.5523 5 18 5H6Z"></path></svg>',
  "ri-printer-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M17 2C17.5523 2 18 2.44772 18 3V7H21C21.5523 7 22 7.44772 22 8V18C22 18.5523 21.5523 19 21 19H18V21C18 21.5523 17.5523 22 17 22H7C6.44772 22 6 21.5523 6 21V19H3C2.44772 19 2 18.5523 2 18V8C2 7.44772 2.44772 7 3 7H6V3C6 2.44772 6.44772 2 7 2H17ZM16 17H8V20H16V17ZM20 9H4V17H6V16C6 15.4477 6.44772 15 7 15H17C17.5523 15 18 15.4477 18 16V17H20V9ZM8 10V12H5V10H8ZM16 4H8V7H16V4Z"></path></svg>',
  "ri-eye-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12.0003 3C17.3924 3 21.8784 6.87976 22.8189 12C21.8784 17.1202 17.3924 21 12.0003 21C6.60812 21 2.12215 17.1202 1.18164 12C2.12215 6.87976 6.60812 3 12.0003 3ZM12.0003 19C16.2359 19 19.8603 16.052 20.7777 12C19.8603 7.94803 16.2359 5 12.0003 5C7.7646 5 4.14022 7.94803 3.22278 12C4.14022 16.052 7.7646 19 12.0003 19ZM12.0003 16.5C9.51498 16.5 7.50026 14.4853 7.50026 12C7.50026 9.51472 9.51498 7.5 12.0003 7.5C14.4855 7.5 16.5003 9.51472 16.5003 12C16.5003 14.4853 14.4855 16.5 12.0003 16.5ZM12.0003 14.5C13.381 14.5 14.5003 13.3807 14.5003 12C14.5003 10.6193 13.381 9.5 12.0003 9.5C10.6196 9.5 9.50026 10.6193 9.50026 12C9.50026 13.3807 10.6196 14.5 12.0003 14.5Z"></path></svg>',
  "ri-eye-off-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M17.8827 19.2968C16.1814 20.3755 14.1638 21.0002 12.0003 21.0002C6.60812 21.0002 2.12215 17.1204 1.18164 12.0002C1.61832 9.62282 2.81932 7.5129 4.52047 5.93457L1.39366 2.80777L2.80788 1.39355L22.6069 21.1925L21.1927 22.6068L17.8827 19.2968ZM5.9356 7.3497C4.60673 8.56015 3.6378 10.1672 3.22278 12.0002C4.14022 16.0521 7.7646 19.0002 12.0003 19.0002C13.5997 19.0002 15.112 18.5798 16.4243 17.8384L14.396 15.8101C13.7023 16.2472 12.8808 16.5002 12.0003 16.5002C9.51498 16.5002 7.50026 14.4854 7.50026 12.0002C7.50026 11.1196 7.75317 10.2981 8.19031 9.60442L5.9356 7.3497ZM12.9139 14.328L9.67246 11.0866C9.5613 11.3696 9.50026 11.6777 9.50026 12.0002C9.50026 13.3809 10.6196 14.5002 12.0003 14.5002C12.3227 14.5002 12.6309 14.4391 12.9139 14.328ZM20.8068 16.5925L19.376 15.1617C20.0319 14.2268 20.5154 13.1586 20.7777 12.0002C19.8603 7.94818 16.2359 5.00016 12.0003 5.00016C11.1544 5.00016 10.3329 5.11773 9.55249 5.33818L7.97446 3.76015C9.22127 3.26959 10.5793 3.00016 12.0003 3.00016C17.3924 3.00016 21.8784 6.87992 22.8189 12.0002C22.5067 13.6998 21.8038 15.2628 20.8068 16.5925ZM11.7229 7.50857C11.8146 7.50299 11.9071 7.50016 12.0003 7.50016C14.4855 7.50016 16.5003 9.51488 16.5003 12.0002C16.5003 12.0933 16.4974 12.1858 16.4919 12.2775L11.7229 7.50857Z"></path></svg>',
  "ri-eraser-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M8.58564 8.85449L3.63589 13.8042L8.83021 18.9985L9.99985 18.9978V18.9966H11.1714L14.9496 15.2184L8.58564 8.85449ZM9.99985 7.44027L16.3638 13.8042L19.1922 10.9758L12.8283 4.61185L9.99985 7.44027ZM13.9999 18.9966H20.9999V20.9966H11.9999L8.00229 20.9991L1.51457 14.5113C1.12405 14.1208 1.12405 13.4877 1.51457 13.0971L12.1212 2.49053C12.5117 2.1 13.1449 2.1 13.5354 2.49053L21.3136 10.2687C21.7041 10.6592 21.7041 11.2924 21.3136 11.6829L13.9999 18.9966Z"></path></svg>',
  "ri-t-box-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5 5V19H19V5H5ZM4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3ZM13 10V17H11V10H7V8H17V10H13Z"></path></svg>',
  "ri-loader-3-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3.05469 13H5.07065C5.55588 16.3923 8.47329 19 11.9998 19C15.5262 19 18.4436 16.3923 18.9289 13H20.9448C20.4474 17.5 16.6323 21 11.9998 21C7.36721 21 3.55213 17.5 3.05469 13ZM3.05469 11C3.55213 6.50005 7.36721 3 11.9998 3C16.6323 3 20.4474 6.50005 20.9448 11H18.9289C18.4436 7.60771 15.5262 5 11.9998 5C8.47329 5 5.55588 7.60771 5.07065 11H3.05469Z"></path></svg>',
  "ri-paint-brush-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5 4.9967V7.9967H19V4.9967H5ZM4 2.9967H20C20.5523 2.9967 21 3.44442 21 3.9967V8.9967C21 9.54899 20.5523 9.9967 20 9.9967H4C3.44772 9.9967 3 9.54899 3 8.9967V3.9967C3 3.44442 3.44772 2.9967 4 2.9967ZM6 11.9967H12C12.5523 11.9967 13 12.4444 13 12.9967V15.9967H14V21.9967H10V15.9967H11V13.9967H5C4.44772 13.9967 4 13.549 4 12.9967V10.9967H6V11.9967ZM17.7322 13.7289L19.5 11.9612L21.2678 13.7289C22.2441 14.7052 22.2441 16.2882 21.2678 17.2645C20.2915 18.2408 18.7085 18.2408 17.7322 17.2645C16.7559 16.2882 16.7559 14.7052 17.7322 13.7289Z"></path></svg>',
  "ri-font-size-2":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M10 6V21H8V6H2V4H16V6H10ZM18 14V21H16V14H13V12H21V14H18Z"></path></svg>',
  "ri-font-family":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5.55397 22H3.3999L10.9999 3H12.9999L20.5999 22H18.4458L16.0458 16H7.95397L5.55397 22ZM8.75397 14H15.2458L11.9999 5.88517L8.75397 14Z"></path></svg>',
  "ri-subscript":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5.59567 4L10.5 9.92831L15.4043 4H18L11.7978 11.4971L18 18.9943V19H15.4091L10.5 13.0659L5.59092 19H3V18.9943L9.20216 11.4971L3 4H5.59567ZM21.8 16C21.8 15.5582 21.4418 15.2 21 15.2C20.5582 15.2 20.2 15.5582 20.2 16C20.2 16.0762 20.2107 16.15 20.2306 16.2198L19.0765 16.5496C19.0267 16.375 19 16.1906 19 16C19 14.8954 19.8954 14 21 14C22.1046 14 23 14.8954 23 16C23 16.5727 22.7593 17.0892 22.3735 17.4538L20.7441 19H23V20H19V19L21.5507 16.5803C21.7042 16.4345 21.8 16.2284 21.8 16Z"></path></svg>',
  "ri-superscript":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5.59567 5L10.5 10.9283L15.4043 5H18L11.7978 12.4971L18 19.9943V20H15.4091L10.5 14.0659L5.59092 20H3V19.9943L9.20216 12.4971L3 5H5.59567ZM21.5507 6.5803C21.7042 6.43453 21.8 6.22845 21.8 6C21.8 5.55817 21.4418 5.2 21 5.2C20.5582 5.2 20.2 5.55817 20.2 6C20.2 6.07624 20.2107 6.14999 20.2306 6.21983L19.0765 6.54958C19.0267 6.37497 19 6.1906 19 6C19 4.89543 19.8954 4 21 4C22.1046 4 23 4.89543 23 6C23 6.57273 22.7593 7.08923 22.3735 7.45384L20.7441 9H23V10H19V9L21.5507 6.5803V6.5803Z"></path></svg>',
  "ri-indent-increase":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3 4H21V6H3V4ZM3 19H21V21H3V19ZM11 14H21V16H11V14ZM11 9H21V11H11V9ZM7 12.5L3 16V9L7 12.5Z"></path></svg>',
  "ri-indent-decrease":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3 4H21V6H3V4ZM3 19H21V21H3V19ZM11 14H21V16H11V14ZM11 9H21V11H11V9ZM3 12.5L7 9V16L3 12.5Z"></path></svg>',
  "ri-format-clear":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12.6512 14.0654L11.6047 20H9.57389L10.9247 12.339L3.51465 4.92892L4.92886 3.51471L20.4852 19.0711L19.071 20.4853L12.6512 14.0654ZM11.7727 7.53009L12.0425 5.99999H10.2426L8.24257 3.99999H19.9999V5.99999H14.0733L13.4991 9.25652L11.7727 7.53009Z"></path></svg>',
  "ri-line-height":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M11 4H21V6H11V4ZM6 7V11H4V7H1L5 3L9 7H6ZM6 17H9L5 21L1 17H4V13H6V17ZM11 18H21V20H11V18ZM9 11H21V13H9V11Z"></path></svg>',
  "ri-list-ordered":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M8 4H21V6H8V4ZM5 3V6H6V7H3V6H4V4H3V3H5ZM3 14V11.5H5V11H3V10H6V12.5H4V13H6V14H3ZM5 19.5H3V18.5H5V18H3V17H6V21H3V20H5V19.5ZM8 11H21V13H8V11ZM8 18H21V20H8V18Z"></path></svg>',
  "ri-list-unordered":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M8 4H21V6H8V4ZM4.5 6.5C3.67157 6.5 3 5.82843 3 5C3 4.17157 3.67157 3.5 4.5 3.5C5.32843 3.5 6 4.17157 6 5C6 5.82843 5.32843 6.5 4.5 6.5ZM4.5 13.5C3.67157 13.5 3 12.8284 3 12C3 11.1716 3.67157 10.5 4.5 10.5C5.32843 10.5 6 11.1716 6 12C6 12.8284 5.32843 13.5 4.5 13.5ZM4.5 20.4C3.67157 20.4 3 19.7284 3 18.9C3 18.0716 3.67157 17.4 4.5 17.4C5.32843 17.4 6 18.0716 6 18.9C6 19.7284 5.32843 20.4 4.5 20.4ZM8 11H21V13H8V11ZM8 18H21V20H8V18Z"></path></svg>',
  "ri-paragraph":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 6V21H10V16C6.68629 16 4 13.3137 4 10C4 6.68629 6.68629 4 10 4H20V6H17V21H15V6H12ZM10 6C7.79086 6 6 7.79086 6 10C6 12.2091 7.79086 14 10 14V6Z"></path></svg>',
  "ri-search-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M18.031 16.6168L22.3137 20.8995L20.8995 22.3137L16.6168 18.031C15.0769 19.263 13.124 20 11 20C6.032 20 2 15.968 2 11C2 6.032 6.032 2 11 2C15.968 2 20 6.032 20 11C20 13.124 19.263 15.0769 18.031 16.6168ZM16.0247 15.8748C17.2475 14.6146 18 12.8956 18 11C18 7.1325 14.8675 4 11 4C7.1325 4 4 7.1325 4 11C4 14.8675 7.1325 18 11 18C12.8956 18 14.6146 17.2475 15.8748 16.0247L16.0247 15.8748Z"></path></svg>',
  "ri-file-settings-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M8.59456 12.8115C8.53273 12.5511 8.5 12.2794 8.5 12C8.5 11.7207 8.53272 11.449 8.59454 11.1886L7.60291 10.6161L8.60291 8.88402L9.59523 9.45694C9.98834 9.08508 10.4676 8.80338 11 8.64494V7.5H13V8.64494C13.5324 8.80337 14.0116 9.08506 14.4047 9.4569L15.3971 8.88393L16.3972 10.616L15.4054 11.1885C15.4673 11.449 15.5 11.7207 15.5 12C15.5 12.2793 15.4673 12.551 15.4055 12.8114L16.3972 13.3839L15.3972 15.116L14.4048 14.543C14.0117 14.9149 13.5325 15.1966 13.0001 15.355V16.5H11.0001V15.3551C10.4677 15.1967 9.98844 14.915 9.59532 14.5431L8.60297 15.1161L7.60291 13.384L8.59456 12.8115ZM12 13.5C12.8284 13.5 13.5 12.8284 13.5 12C13.5 11.1716 12.8284 10.5 12 10.5C11.1716 10.5 10.5 11.1716 10.5 12C10.5 12.8284 11.1716 13.5 12 13.5ZM15 4H5V20H19V8H15V4ZM3 2.9918C3 2.44405 3.44749 2 3.9985 2H16L20.9997 7L21 20.9925C21 21.5489 20.5551 22 20.0066 22H3.9934C3.44476 22 3 21.5447 3 21.0082V2.9918Z"></path></svg>',
  "ri-water-percent-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M7.05025 8.04673L12 3.09698L16.9497 8.04673C19.6834 10.7804 19.6834 15.2126 16.9497 17.9462C14.2161 20.6799 9.78392 20.6799 7.05025 17.9462C4.31658 15.2126 4.31658 10.7804 7.05025 8.04673ZM18.364 6.63252L12 0.268555L5.63604 6.63252C2.12132 10.1472 2.12132 15.8457 5.63604 19.3604C9.15076 22.8752 14.8492 22.8752 18.364 19.3604C21.8787 15.8457 21.8787 10.1472 18.364 6.63252ZM16.2427 10.1714L14.8285 8.75718L7.7574 15.8282L9.17161 17.2425L16.2427 10.1714ZM8.11095 11.232C8.69674 11.8178 9.64648 11.8178 10.2323 11.232C10.8181 10.6463 10.8181 9.69652 10.2323 9.11073C9.64648 8.52494 8.69674 8.52494 8.11095 9.11073C7.52516 9.69652 7.52516 10.6463 8.11095 11.232ZM15.8891 16.8889C15.3033 17.4747 14.3536 17.4747 13.7678 16.8889C13.182 16.3031 13.182 15.3534 13.7678 14.7676C14.3536 14.1818 15.3033 14.1818 15.8891 14.7676C16.4749 15.3534 16.4749 16.3031 15.8891 16.8889Z"></path></svg>',
  "ri-grid-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M14 10H10V14H14V10ZM16 10V14H19V10H16ZM14 19V16H10V19H14ZM16 19H19V16H16V19ZM14 5H10V8H14V5ZM16 5V8H19V5H16ZM8 10H5V14H8V10ZM8 19V16H5V19H8ZM8 5H5V8H8V5ZM4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3Z"></path></svg>',
  "ri-number-1":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M14 1.5V22H12V3.704L7.5 4.91V2.839L12.5 1.5H14Z"></path></svg>',
  "ri-table-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M4 8H20V5H4V8ZM14 19V10H10V19H14ZM16 19H20V10H16V19ZM8 19V10H4V19H8ZM3 3H21C21.5523 3 22 3.44772 22 4V20C22 20.5523 21.5523 21 21 21H3C2.44772 21 2 20.5523 2 20V4C2 3.44772 2.44772 3 3 3Z"></path></svg>',
  "ri-delete-bin-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M17 6H22V8H20V21C20 21.5523 19.5523 22 19 22H5C4.44772 22 4 21.5523 4 21V8H2V6H7V3C7 2.44772 7.44772 2 8 2H16C16.5523 2 17 2.44772 17 3V6ZM18 8H6V20H18V8ZM9 11H11V17H9V11ZM13 11H15V17H13V11ZM9 4V6H15V4H9Z"></path></svg>',
  "ri-shape-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M7.82929 20C7.41746 21.1652 6.30622 22 5 22C3.34315 22 2 20.6569 2 19C2 17.6938 2.83481 16.5825 4 16.1707V7.82929C2.83481 7.41746 2 6.30622 2 5C2 3.34315 3.34315 2 5 2C6.30622 2 7.41746 2.83481 7.82929 4H16.1707C16.5825 2.83481 17.6938 2 19 2C20.6569 2 22 3.34315 22 5C22 6.30622 21.1652 7.41746 20 7.82929V16.1707C21.1652 16.5825 22 17.6938 22 19C22 20.6569 20.6569 22 19 22C17.6938 22 16.5825 21.1652 16.1707 20H7.82929ZM7.82929 18H16.1707C16.472 17.1476 17.1476 16.472 18 16.1707V7.82929C17.1476 7.52801 16.472 6.85241 16.1707 6H7.82929C7.52801 6.85241 6.85241 7.52801 6 7.82929V16.1707C6.85241 16.472 7.52801 17.1476 7.82929 18ZM5 6C5.55228 6 6 5.55228 6 5C6 4.44772 5.55228 4 5 4C4.44772 4 4 4.44772 4 5C4 5.55228 4.44772 6 5 6ZM19 6C19.5523 6 20 5.55228 20 5C20 4.44772 19.5523 4 19 4C18.4477 4 18 4.44772 18 5C18 5.55228 18.4477 6 19 6ZM19 20C19.5523 20 20 19.5523 20 19C20 18.4477 19.5523 18 19 18C18.4477 18 18 18.4477 18 19C18 19.5523 18.4477 20 19 20ZM5 20C5.55228 20 6 19.5523 6 19C6 18.4477 5.55228 18 5 18C4.44772 18 4 18.4477 4 19C4 19.5523 4.44772 20 5 20Z"></path></svg>',
  "ri-table-2":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M13 10V14H19V10H13ZM11 10H5V14H11V10ZM13 19H19V16H13V19ZM11 19V16H5V19H11ZM13 5V8H19V5H13ZM11 5H5V8H11V5ZM4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3Z"></path></svg>',
  "ri-layout-row-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M19 12H5V19H19V12ZM4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3Z"></path></svg>',
  "ri-insert-row-top":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M20 13C20.5523 13 21 13.4477 21 14V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V14C3 13.4477 3.44772 13 4 13H20ZM19 15H5V19H19V15ZM12 1C14.7614 1 17 3.23858 17 6C17 8.76142 14.7614 11 12 11C9.23858 11 7 8.76142 7 6C7 3.23858 9.23858 1 12 1ZM13 3H11V4.999L9 5V7L11 6.999V9H13V6.999L15 7V5L13 4.999V3Z"></path></svg>',
  "ri-insert-row-bottom":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 13C14.7614 13 17 15.2386 17 18C17 20.7614 14.7614 23 12 23C9.23858 23 7 20.7614 7 18C7 15.2386 9.23858 13 12 13ZM13 15H11V16.999L9 17V19L11 18.999V21H13V18.999L15 19V17L13 16.999V15ZM20 3C20.5523 3 21 3.44772 21 4V10C21 10.5523 20.5523 11 20 11H4C3.44772 11 3 10.5523 3 10V4C3 3.44772 3.44772 3 4 3H20ZM5 5V9H19V5H5Z"></path></svg>',
  "ri-delete-row":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M20 5C20.5523 5 21 5.44772 21 6V12C21 12.5523 20.5523 13 20 13C20.628 13.8355 21 14.8743 21 16C21 18.7614 18.7614 21 16 21C13.2386 21 11 18.7614 11 16C11 14.8743 11.372 13.8355 11.9998 12.9998L4 13C3.44772 13 3 12.5523 3 12V6C3 5.44772 3.44772 5 4 5H20ZM13 15V17H19V15H13ZM19 7H5V11H19V7Z"></path></svg>',
  "ri-layout-column-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 5V19H19V5H12ZM4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3Z"></path></svg>',
  "ri-insert-column-left":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M20 3C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H14C13.4477 21 13 20.5523 13 20V4C13 3.44772 13.4477 3 14 3H20ZM19 5H15V19H19V5ZM6 7C8.76142 7 11 9.23858 11 12C11 14.7614 8.76142 17 6 17C3.23858 17 1 14.7614 1 12C1 9.23858 3.23858 7 6 7ZM7 9H5V10.999L3 11V13L5 12.999V15H7V12.999L9 13V11L7 10.999V9Z"></path></svg>',
  "ri-insert-column-right":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M10 3C10.5523 3 11 3.44772 11 4V20C11 20.5523 10.5523 21 10 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3H10ZM9 5H5V19H9V5ZM18 7C20.7614 7 23 9.23858 23 12C23 14.7614 20.7614 17 18 17C15.2386 17 13 14.7614 13 12C13 9.23858 15.2386 7 18 7ZM19 9H17V10.999L15 11V13L17 12.999V15H19V12.999L21 13V11L19 10.999V9Z"></path></svg>',
  "ri-delete-column":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 3C12.5523 3 13 3.44772 13 4L12.9998 11.9998C13.8355 11.372 14.8743 11 16 11C18.7614 11 21 13.2386 21 16C21 18.7614 18.7614 21 16 21C14.9681 21 14.0092 20.6874 13.2129 20.1518L13 20C13 20.5523 12.5523 21 12 21H6C5.44772 21 5 20.5523 5 20V4C5 3.44772 5.44772 3 6 3H12ZM11 5H7V19H11V5ZM19 15H13V17H19V15Z"></path></svg>',
  "ri-merge-cells-horizontal":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M20 3C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3H20ZM11 5H5V10.999H7V9L10 12L7 15V13H5V19H11V17H13V19H19V13H17V15L14 12L17 9V10.999H19V5H13V7H11V5ZM13 13V15H11V13H13ZM13 9V11H11V9H13Z"></path></svg>',
  "ri-split-cells-horizontal":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M20 3C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3H20ZM11 5H5V19H11V15H13V19H19V5H13V9H11V5ZM15 9L18 12L15 15V13H9V15L6 12L9 9V11H15V9Z"></path></svg>',
  "ri-merge-cells-horizontal":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M20 3C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3H20ZM11 5H5V10.999H7V9L10 12L7 15V13H5V19H11V17H13V19H19V13H17V15L14 12L17 9V10.999H19V5H13V7H11V5ZM13 13V15H11V13H13ZM13 9V11H11V9H13Z"></path></svg>',
  "ri-slash-commands-2":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5 2C3.34315 2 2 3.34315 2 5V19C2 20.6569 3.34315 22 5 22H19C20.6569 22 22 20.6569 22 19V5C22 3.34315 20.6569 2 19 2H5ZM4 5C4 4.44772 4.44772 4 5 4H19C19.5523 4 20 4.44772 20 5V19C20 19.5523 19.5523 20 19 20H5C4.44772 20 4 19.5523 4 19V5ZM9.72318 18L16.5803 6H14.2768L7.41968 18H9.72318Z"></path></svg>',
  "ri-input-method-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M5 5V19H19V5H5ZM4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3ZM9.86885 15L9.04918 17H6.83333L11 7H13L17.1667 17H14.9508L14.1311 15H9.86885ZM10.6885 13H13.3115L12 9.8L10.6885 13Z"></path></svg>',
  "ri-radio-button-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M12 22C6.47715 22 2 17.5228 2 12C2 6.47715 6.47715 2 12 2C17.5228 2 22 6.47715 22 12C22 17.5228 17.5228 22 12 22ZM12 20C16.4183 20 20 16.4183 20 12C20 7.58172 16.4183 4 12 4C7.58172 4 4 7.58172 4 12C4 16.4183 7.58172 20 12 20ZM12 17C9.23858 17 7 14.7614 7 12C7 9.23858 9.23858 7 12 7C14.7614 7 17 9.23858 17 12C17 14.7614 14.7614 17 12 17Z"></path></svg>',
  "ri-checkbox-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M4 3H20C20.5523 3 21 3.44772 21 4V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V4C3 3.44772 3.44772 3 4 3ZM5 5V19H19V5H5ZM11.0026 16L6.75999 11.7574L8.17421 10.3431L11.0026 13.1716L16.6595 7.51472L18.0737 8.92893L11.0026 16Z"></path></svg>',
  "ri-separator":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M2 11H4V13H2V11ZM6 11H18V13H6V11ZM20 11H22V13H20V11Z"></path></svg>',
  "ri-rectangle-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3 4H21C21.5523 4 22 4.44772 22 5V19C22 19.5523 21.5523 20 21 20H3C2.44772 20 2 19.5523 2 19V5C2 4.44772 2.44772 4 3 4ZM4 6V18H20V6H4Z"></path></svg>',
  "ri-stethoscope-fill":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M8 3V5H6V9C6 11.2091 7.79086 13 10 13C12.2091 13 14 11.2091 14 9V5H12V3H15C15.5523 3 16 3.44772 16 4V9C16 11.9727 13.8381 14.4405 11.0008 14.9169L11 16.5C11 18.433 12.567 20 14.5 20C15.9973 20 17.275 19.0598 17.7749 17.7375C16.7283 17.27 16 16.2201 16 15C16 13.3431 17.3431 12 19 12C20.6569 12 22 13.3431 22 15C22 16.3711 21.0802 17.5274 19.824 17.8854C19.2102 20.252 17.0592 22 14.5 22C11.4624 22 9 19.5376 9 16.5L9.00019 14.9171C6.16238 14.4411 4 11.9731 4 9V4C4 3.44772 4.44772 3 5 3H8ZM19 14C18.4477 14 18 14.4477 18 15C18 15.5523 18.4477 16 19 16C19.5523 16 20 15.5523 20 15C20 14.4477 19.5523 14 19 14Z"></path></svg>',
  "ri-barcode-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M2 4H4V20H2V4ZM6 4H7V20H6V4ZM8 4H10V20H8V4ZM11 4H13V20H11V4ZM14 4H16V20H14V4ZM17 4H18V20H17V4ZM19 4H22V20H19V4Z"></path></svg>',
  "ri-qr-code-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M16 17V16H13V13H16V15H18V17H17V19H15V21H13V18H15V17H16ZM21 21H17V19H19V17H21V21ZM3 3H11V11H3V3ZM5 5V9H9V5H5ZM13 3H21V11H13V3ZM15 5V9H19V5H15ZM3 13H11V21H3V13ZM5 15V19H9V15H5ZM18 13H21V15H18V13ZM6 6H8V8H6V6ZM6 16H8V18H6V16ZM16 6H18V8H16V6Z"></path></svg>',
  "ri-terminal-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M3 3H21C21.5523 3 22 3.44772 22 4V20C22 20.5523 21.5523 21 21 21H3C2.44772 21 2 20.5523 2 20V4C2 3.44772 2.44772 3 3 3ZM4 5V19H20V5H4ZM12 15H18V17H12V15ZM8.66685 12L5.83842 9.17157L7.25264 7.75736L11.4953 12L7.25264 16.2426L5.83842 14.8284L8.66685 12Z"></path></svg>',
  "ri-copyright-line":
    '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor"><path d="M16.2877 9.42773C15.413 7.97351 13.8195 7 12 7 9.23999 7 7 9.23999 7 12 7 14.76 9.23999 17 12 17 13.8195 17 15.413 16.0265 16.2877 14.5723L14.5729 13.5442C14.0483 14.4166 13.0927 15 12 15 10.3425 15 9 13.6575 9 12 9 10.3425 10.3425 9 12 9 13.093 9 14.0491 9.58386 14.5735 10.4568L16.2877 9.42773ZM22 12C22 6.47998 17.52 2 12 2 6.47998 2 2 6.47998 2 12 2 17.52 6.47998 22 12 22 17.52 22 22 17.52 22 12ZM4 12C4 7.57996 7.57996 4 12 4 16.42 4 20 7.57996 20 12 20 16.42 16.42 20 12 20 7.57996 20 4 16.42 4 12Z"></path></svg>',
};
let selectOptionsList = {
  fontFamilys: [],
  fontSize: [
    8,
    9,
    10,
    10.5,
    11,
    12,
    14,
    16,
    18,
    20,
    22,
    24,
    26,
    28,
    36,
    48,
    72,
    "初号",
    "小初",
    "一号",
    "小一",
    "二号",
    "小二",
    "三号",
    "小三",
    "四号",
    "小四",
    "五号",
    "小五",
    "六号",
    "小六",
    "七号",
    "八号",
  ],
  orderList: [
    {
      label: "1.2.3.4",
      value: "ListNumberStyle",
      index: 1,
    },
    {
      label: "1,2,3,4",
      value: "ListNumberStyleArabic1",
      index: 2,
    },
    {
      label: "1）2）3）4）",
      value: "ListNumberStyleArabic2",
      index: 3,
      },
      {
          label: "1、2、3、4、",
          value: "ListNumberStyleArabic3",
          index: 15,
      },
    {
      label: "a）b）c）d）",
      value: "ListNumberStyleLowercaseLetter",
      index: 4,
    },
    {
      label: "i）ii）iii）iv）",
      value: "ListNumberStyleLowercaseRoman",
      index: 5,
    },
    {
      label: "① ② ③ ④",
      value: "ListNumberStyleNumberInCircle",
      index: 6,
    },
    {
      label: "一.二.三.四",
      value: "ListNumberStyleSimpChinNum1",
      index: 7,
    },
    {
      label: "一）二）三）四",
      value: "ListNumberStyleSimpChinNum2",
      index: 8,
    },
    {
      label: "壹.贰.叁.肆",
      value: "ListNumberStyleTradChinNum1",
      index: 9,
    },
    {
      label: "壹）贰）叁）肆",
      value: "ListNumberStyleTradChinNum2",
      index: 10,
    },
    {
      label: "A）B）C）D",
      value: "ListNumberStyleUppercaseLetter",
      index: 11,
    },
    {
      label: "Ⅰ）Ⅱ）Ⅲ）Ⅳ",
      value: "ListNumberStyleUppercaseRoman",
      index: 12,
    },
    {
      label: "甲,乙,丙,丁",
      value: "ListNumberStyleZodiac1",
      index: 13,
    },
    {
      label: "子,丑,寅,卯",
      value: "ListNumberStyleZodiac2",
      index: 14,
    },
  ],
  unorderList: [
    {
      label: "● Bulletedlist",
      value: "BulletedList",
      index: 10000,
    },
    {
      label: "■ Bulletedlistblock",
      value: "BulletedListBlock",
      index: 10001,
    },
    {
      label: "◆ Bulletedlistdiamond",
      value: "BulletedListDiamond",
      index: 10002,
    },
    {
      label: "✔ BulletedListCheck ",
      value: "BulletedListCheck ",
      index: 10003,
    },
    {
      label: "➢ BulletedListRightArrow",
      value: "BulletedListRightArrow",
      index: 10004,
    },
    {
      label: "◇ BulletedListHollowStar",
      value: "BulletedListHollowStar",
      index: 10005,
    },
  ],
  lineheight: [1.0, 1.5, 2.0, 2.5, 3.0],
  medicalExpression: [
    {
      label: "月经史公式1",
      ID: "expression1",
      ExpressionStyle: "FourValues1",
      Values: "Value1:14;Value2:14;Value3:14;Value4:14",
      ValuesCount: 4,
    },
    {
      label: "月经史公式2",
      ID: "expression2",
      ExpressionStyle: "FourValues2",
      Values: "Value1:14;Value2:14;Value3:14;Value4:14",
      ValuesCount: 4,
    },
    {
      label: "月经史公式3",
      ID: "expression3",
      ExpressionStyle: "ThreeValues",
      Values: "Value1:14;Value2:14;Value3:14",
      ValuesCount: 3,
    },
    {
      label: "月经史公式4",
      ID: "expression4",
      ExpressionStyle: "FourValues",
      Values: "Value1:14;Value2:14;Value3:14;Value4:14",
      ValuesCount: 4,
    },
    {
      label: "瞳孔",
      ID: "expression5",
      ExpressionStyle: "Pupil",
      Values: "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;Value7:14;",
      ValuesCount: 7,
    },
    {
      label: "胎心值",
      ID: "expression6",
      ExpressionStyle: "FetalHeart",
      Values: "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;",
      ValuesCount: 6,
    },
    {
      label: "眼球突出度",
      ID: "expression7",
      ExpressionStyle: "ThreeValues2",
      Values: "Value1:0;Value2:1;Value3:2;",
      ValuesCount: 3,
    },
    {
      label: "斜视符号",
      ID: "expression8",
      ExpressionStyle: "StrabismusSymbol",
      Values: "Value1:L;",
      ValuesCount: 1,
    },
    {
      label: "标尺",
      ID: "expression9",
      ExpressionStyle: "PainIndex",
      Values: "Value1:14;",
      ValuesCount: 1,
    },
    {
      label: "恒牙牙位图",
      ID: "expression10",
      ExpressionStyle: "PermanentTeethBitmap",
      Values:
        "Value1:1;Value2:2;Value3:3;Value4:4;Value5:5;Value6:6;Value7:7;Value8:8;Value9:9;Value10:10;Value11:11;Value12:12;Value13:13;Value14:14;Value15:15;Value16:16;Value17:17;Value18:18;Value19:19;Value20:20;",
      ValuesCount: 20,
    },
    {
      label: "乳牙牙位图",
      ID: "expression11",
      ExpressionStyle: "DeciduousTeech",
      Values:
        "Value1:1;Value2:2;Value3:3;Value4:4;Value5:5;Value6:6;Value7:7;Value8:8;Value9:9;Value10:10;Value11:11;Value12:12;Value13:13;Value14:14;Value15:15;Value16:16;Value17:17;Value18:18;Value19:19;Value20:20;",
      ValuesCount: 20,
    },
    {
      label: "分数公式",
      ID: "expression12",
      ExpressionStyle: "Fraction",
      Values: "Value1:1;Value2:2;",
      ValuesCount: 2,
    },
    {
      label: "病变上牙",
      ID: "expression13",
      ExpressionStyle: "DiseasedTeethTop",
      Values: "Value1:1;Value2:2;Value3:2;",
      ValuesCount: 3,
    },
    {
      label: "病变下牙",
      ID: "expression14",
      ExpressionStyle: "DiseasedTeethBotton",
      Values: "Value1:1;Value2:2;Value3:2;",
      ValuesCount: 3,
    },
    {
      label: "光定位",
      ID: "expression15",
      ExpressionStyle: "LightPositioning",
      Values: "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;Value7:14;Value8:14;Value9:14;",
      ValuesCount: 9,
    },
  ],
};
let toolsbar_groups = [
  [
    {
      label: "撤销",
      commendName: "undo",
      icon: "ri-arrow-go-back-line",
      hiddenLabel: true,
    },
    {
      label: "重做",
      commendName: "redo",
      icon: "ri-arrow-go-forward-line",
      hiddenLabel: true,
    },
  ],
  [
    {
      label: "剪切",
      commendName: "cut",
      icon: "ri-scissors-2-fill",
      hiddenLabel: true,
    },
    {
      label: "复制",
      commendName: "copy",
      icon: "ri-file-copy-line",
      hiddenLabel: true,
    },
    {
      label: "粘贴",
      commendName: "paste",
      icon: "ri-clipboard-line",
      hiddenLabel: true,
    },
  ],
  [
    {
      type: "select",
      label: "字体",
      commendName: "fontname",
      placeholder: "字体",
      value: "",
      listProps: {
        label: "ch",
        value: "en",
      },
      style: "width:160px;",
      optionName: "fontFamilys",
    },
    {
      type: "select",
      label: "字号",
      commendName: "fontsize",
      placeholder: "字号",
      value: "",

      style: "margin-left:5px;width:160px",
      optionName: "fontSize",
    },
  ],
  [
    {
      label: "加粗",
      commendName: "bold",
      icon: "ri-bold",
      hiddenLabel: true,
    },
    {
      label: "斜体",
      commendName: "italic",
      icon: "ri-italic",
      hiddenLabel: true,
    },
    {
      label: "下划线",
      commendName: "underline",
      icon: "ri-underline",
      hiddenLabel: true,
    },
    {
      label: "删除线",
      commendName: "strikeout",
      icon: "ri-strikethrough",
      hiddenLabel: true,
    },
    {
      type: "color",
      value: "",
      label: "文字颜色",
      commendName: "color",
      icon: "ri-font-color",
      hiddenLabel: true,
      onclick: function (e, rootElement) {
        buildColorPicker(e, "color", rootElement);
      },
    },
    {
      type: "color",
      value: "",
      label: "背景颜色",
      commendName: "backcolor",
      icon: "ri-palette-fill",
      hiddenLabel: true,
      onclick: function (e, rootElement) {
        buildColorPicker(e, "backcolor", rootElement);
      },
    },
  ],
  [
    {
      label: "居左",
      commendName: "AlignLeft",
      icon: "ri-align-left",
      hiddenLabel: true,
    },
    {
      label: "居中",
      commendName: "AlignCenter",
      icon: "ri-align-center",
      hiddenLabel: true,
    },
    {
      label: "居右",
      commendName: "AlignRight",
      icon: "ri-align-right",
      hiddenLabel: true,
    },
    {
      label: "分散对齐",
      commendName: "AlignDistribute",
      icon: "ri-align-justify",
      hiddenLabel: true,
    },
  ],
  [
    {
      type: "popover",
      label: "插入特殊字符",
      commendName: "",
      icon: "ri-omega",
      hiddenLabel: true,
      onclick: (e,rootElement) => {
        rootElement.DCExecuteCommand("InsertSpecifyCharacter", true, {});
      },
    },
    {
      type: "popover",
      label: "插入图片",
      commendName: "",
      icon: "ri-image-add-fill",
      hiddenLabel: true,
      onclick: (e,rootElement) => {
        rootElement.DCExecuteCommand("InsertImage", true, {});
      },
    },
  ],
  [
    {
      label: "设计模式",
      commendName: "DesignMode",
      icon: "ri-pencil-ruler-2-line",
      hiddenLabel: true,
    },
    // {
    //   label: "刷新文档",
    //   commendName: "",
    //   hiddenLabel: true,
    //   icon: "ri-loop-right-fill",
    // },
  ],
];
let colorPickerHTML = `
        <div id="dc_inner_color_picker" class="dc_inner_color_picker">
        <div class="dc_inner_color_picker_default dc_color_box">默认</div>
        <div class="dc_inner_color_picker_choose">
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #f2f0f3;"></div>
                <div class="dc_color_box" style="background-color: #dad8db;"></div>
                <div class="dc_color_box" style="background-color: #bfbdc0;"></div>
                <div class="dc_color_box" style="background-color: #a5a3a6;"></div>
                <div class="dc_color_box" style="background-color: #959396;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #010002;"></div>
                <div class="dc_color_box" style="background-color: #7f7d80;"></div>
                <div class="dc_color_box" style="background-color: #5b595c;"></div>
                <div class="dc_color_box" style="background-color: #3f3d40;"></div>
                <div class="dc_color_box" style="background-color: #272527;"></div>
                <div class="dc_color_box" style="background-color: #09070a;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #4b5265;"></div>
                <div class="dc_color_box" style="background-color: #f5f3f6;"></div>
                <div class="dc_color_box" style="background-color: #cac7cf;"></div>
                <div class="dc_color_box" style="background-color: #86889e;"></div>
                <div class="dc_color_box" style="background-color: #262427;"></div>
                <div class="dc_color_box" style="background-color: #212024;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #3f71fc;"></div>
                <div class="dc_color_box" style="background-color: #e6effb;"></div>
                <div class="dc_color_box" style="background-color: #cadbfd;"></div>
                <div class="dc_color_box" style="background-color: #a0befc;"></div>
                <div class="dc_color_box" style="background-color: #2253ba;"></div>
                <div class="dc_color_box" style="background-color: #182f76;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #3ca4f3;"></div>
                <div class="dc_color_box" style="background-color: #e4faf8;"></div>
                <div class="dc_color_box" style="background-color: #d0edff;"></div>
                <div class="dc_color_box" style="background-color: #a5dcfe;"></div>
                <div class="dc_color_box" style="background-color: #2d74a0;"></div>
                <div class="dc_color_box" style="background-color: #18405b;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #469959;"></div>
                <div class="dc_color_box" style="background-color: #edf8f0;"></div>
                <div class="dc_color_box" style="background-color: #cbe5d4;"></div>
                <div class="dc_color_box" style="background-color: #a2d6b4;"></div>
                <div class="dc_color_box" style="background-color: #337c4f;"></div>
                <div class="dc_color_box" style="background-color: #27472f;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #d63733;"></div>
                <div class="dc_color_box" style="background-color: #fce7ec;"></div>
                <div class="dc_color_box" style="background-color: #f9c9c9;"></div>
                <div class="dc_color_box" style="background-color: #f6979a;"></div>
                <div class="dc_color_box" style="background-color: #941d15;"></div>
                <div class="dc_color_box" style="background-color: #4c110b;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #e98209;"></div>
                <div class="dc_color_box" style="background-color: #faf3eb;"></div>
                <div class="dc_color_box" style="background-color: #f9dcc3;"></div>
                <div class="dc_color_box" style="background-color: #f6b785;"></div>
                <div class="dc_color_box" style="background-color: #ae6203;"></div>
                <div class="dc_color_box" style="background-color: #5b2d0c;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #efc500;"></div>
                <div class="dc_color_box" style="background-color: #f8f9e0;"></div>
                <div class="dc_color_box" style="background-color: #ffefa9;"></div>
                <div class="dc_color_box" style="background-color: #fce261;"></div>
                <div class="dc_color_box" style="background-color: #998208;"></div>
                <div class="dc_color_box" style="background-color: #5c5006;"></div>
            </div>
            <div class="dc_color-group">
                <div class="dc_color_box" style="background-color: #9631dd;"></div>
                <div class="dc_color_box" style="background-color: #fbeafb;"></div>
                <div class="dc_color_box" style="background-color: #edc9fd;"></div>
                <div class="dc_color_box" style="background-color: #cd8eff;"></div>
                <div class="dc_color_box" style="background-color: #54288f;"></div>
                <div class="dc_color_box" style="background-color: #371347;"></div>
            </div>

        </div>
        <div class="dc_inner_color_picker_standard">
            <div class="dc_inner_color_picker_standard_text">标准色</div>
            <div class="dc_inner_color_picker_standard_choose">
                <div class="dc_color_box" style="background-color: #af0308;"></div>
                <div class="dc_color_box" style="background-color: #f10300;"></div>
                <div class="dc_color_box" style="background-color: #f7c003;"></div>
                <div class="dc_color_box" style="background-color: #fffb00;"></div>
                <div class="dc_color_box" style="background-color: #9cd04a;"></div>
                <div class="dc_color_box" style="background-color: #37b246;"></div>
                <div class="dc_color_box" style="background-color: #39b1f7;"></div>
                <div class="dc_color_box" style="background-color: #2372b6;"></div>
                <div class="dc_color_box" style="background-color: #0d1d63;"></div>
                <div class="dc_color_box" style="background-color: #6a319b;"></div>
            </div>
        </div>
        <div class="dc_inner_color_picker_standard">
            <div class="dc_inner_color_picker_standard_text">最近使用</div>
            <div class="dc_inner_color_picker_standard_choose" id="recentlyUsedDom">
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
                <div class="dc_color_box" style="background-color: #ffffff;"></div>
            </div>
        </div>
    </div>
    `;
let colorPickerSTYLE = ` .dc_inner_color_picker {
            width: 260px;
            height: 310px;
            background: #ffffff;
            padding: 10px;
            box-sizing: border-box;
            margin: 0;
            box-shadow: 0 0 8px 0 rgba(232, 237, 250, .6), 0 2px 4px 0 rgba(232, 237, 250, .5);
            position:fixed;
            z-index:20000
        }

        .dc_inner_color_picker_default.dc_color_box {
            width: 100%;
            height: 26px;
            border: 1px solid #ebebeb;
            text-align: center;
            line-height: 26px;
            font-size: 14px;
            color: #606266;
        }

        .dc_inner_color_picker_choose {
            width: 100%;
            height: 136px;
            padding: 5px 0;
            display: flex;
            justify-content: space-between;
        }

        .dc_color-group {
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .dc_color_box {
            width: 18px;
            height: 18px;
            border: 1px solid #ebebeb;
            cursor: pointer;
        }

        .dc_inner_color_picker_standard_text {
            margin: 6px 0;
            font-size: 14px;
            color: #606266;
        }

        .dc_inner_color_picker_standard_choose {
            width: 100%;
            display: flex;
            justify-content: space-between;
        }`;
