"use strict";

export let WriterControl_CalculateControl = {
    //创建数字计算器的方法
    CreateCalculateControl: function (dblInputValue, callBack) {
        //获取初始化的值,这里使用newInputValue对计算器赋值
        //var newInputValue = dblInputValue;
        //每次点击计算器时的回调
        var callbackEvery = function (number) { }

        // 这里编写代码显示一个数字计算器，用户确认操作后调用callBack
        var keyBoardDiv, keyBoard, commit, dialog, input, label, span, table, tbody, tr, td;
        var keyBoardClick, keyBoardDivClick;
        var keyModels = {
            SIMPLE: {
                COLS: 3,
                WIDTH: '33.3%',
                TYPE: 1,
                KEYS: [7, 8, 9, 4, 5, 6, 1, 2, 3, '-', 0, '<']
            },
            PLUS: {
                COLS: 4,
                WIDTH: '25%',
                TYPE: 1,
                KEYS: [7, 8, 9, 'C', 4, 5, 6, '↑', 1, 2, 3, '↓', '-', 0, '.', '<']
            }
        };
        var currModel;
        var inputText = "",
            currText, fixed = 0,
            offset = -1;
        var _this = this;
        // 参数置换
        if (typeof keyModels.PLUS === "function") {
            callbackEvery = keyModels.PLUS;
            keyModels.PLUS = undefined;
        }
        // UI
        keyModels.PLUS = keyModels.PLUS || keyModels.SIMPLE;
        if (!keyBoardDiv || keyModels.PLUS !== currModel) {
            inputText = "";
            currModel = keyModels.PLUS;
            // 键盘上的对话框
            dialog = document.createElement("DIV");
            label = document.createElement("DIV");
            span = document.createElement("SPAN");
            input = document.createElement("SPAN");
            commit = document.createElement("BUTTON");

            dialog.className = 'qs-keyBoard-dialog';
            commit.innerHTML = "完成";
            input.className = "qs-inset-input";
            input.style.textAlign = 'center';
            input.style.overflow = "hidden";
            input.style.textOverflow = "ellipsis";
            label.appendChild(input);
            label.appendChild(commit);
            dialog.appendChild(span);
            dialog.appendChild(label);
            if (isNaN(dblInputValue)) {

            }
            else {
                input.innerHTML = inputText = (dblInputValue + "") || "";
            }
            span.innerHTML = '请输入数字' || '';
            keyBoardDiv = document.createElement("DIV");
            //给最外层包裹元素一个样式
            keyBoardDiv.style.cssText = 'box-shadow: 0px 0px 15px rgba(0,0,0,20%);font: 12px/1.4 Arial, Tahoma, simsun'

            // 键盘部分
            keyBoard = document.createElement("DIV");
            table = document.createElement("TABLE");
            tbody = document.createElement("TBODY");
            keyBoard.className = "qs-key-board";
            keyBoard.id = 'qs-keyboard-id';
            table.border = '0';
            for (var i = 0; i < currModel.KEYS.length; i++) {
                if (i % currModel.COLS === 0) {
                    tr = document.createElement("TR");
                }
                if (currModel.KEYS[i] || currModel.KEYS[i] === 0) {
                    td = document.createElement("TD");
                    td.style.cursor = 'default'
                    td.style.width = currModel.WIDTH;
                    if (typeof (currModel.KEYS[i]) === "object") {
                        currModel.KEYS[i].icon ? td.className = currModel.KEYS[i].icon : td.innerHTML = currModel.KEYS[i].text;
                        currModel.KEYS[i].rows && td.setAttribute('rowspan', currModel.KEYS[i].rows);
                        td.setAttribute("qs-data-value", currModel.KEYS[i].text);
                    } else {
                        td.innerHTML = currModel.KEYS[i];
                        td.setAttribute("qs-data-value", currModel.KEYS[i]);
                    }
                    tr.appendChild(td);
                }
                if (i % currModel.COLS === currModel.COLS - 1) {
                    tbody.appendChild(tr);
                }
            }
            table.appendChild(tbody);
            keyBoard.appendChild(dialog);
            keyBoard.appendChild(table);
            keyBoardDiv.appendChild(keyBoard);
            var s = ".qs-key-board{background-color:white;width:100%;}.qs-keyBoard-dialog{padding:5px 10px;background-color:white;box-shadow:inset 0px 5px 15px#efefef;text-align: left;}.qs-keyBoard-dialog>div{display:flex;height:30px;}.qs-keyBoard-dialog>div>button{width:6em;}.qs-keyBoard-dialog>span{font-size:14px;display:block;padding:2px;color:#999999;white-space:nowrap;text-overflow:ellipsis;overflow:hidden;}.qs-key-board>table{width:100%;background-color:#efefef;border-spacing:6px;border-collapse:separate;}.qs-key-board tr{height:1.5rem;}.qs-key-board td{width:33.3%;border:solid 1px#dedede;border-radius:6px;-webkit-border-radius:6px;font-size:24px;text-align:center;vertical-align:middle;background-color:white;}.qs-key-board td:active{background-color:#dedede;}.qs-inset-input{position:relative;display:inline-block;border-radius:3px;-webkit-border-radius:3px;margin-right:10px;border:none;font-size:18px!important;width:100%;height:30px!important;line-height:30px;background-color:rgb(238,238,238)!important;}";
            var style = document.createElement("style");
            style.innerHTML = s;
            keyBoardDiv.appendChild(style);
        }
        // 监听事件
        keyBoardDivClick = function () {
            inputText = inputText === '-' ? '' : inputText;
            callbackLast && callbackLast(inputText ? Number(inputText) : '');
        };

        keyBoardClick = function (e) {
            switch (e.target.nodeName) {
                case 'TD':
                    //DCDomTools.completeEvent(e);
                    e.stopPropagation();
                    e.preventDefault();
                    e.returnValue = false;
                    doKeys(e);
                    break;
                case 'BUTTON':
                    e.stopPropagation();
                    e.preventDefault();
                    e.returnValue = false;
                    var value = parseFloat(input.innerHTML);
                    if (isNaN(value) == true) {
                        value = 0;
                    }
                    callBack && callBack(value)
                    break;
                default:
                    e.stopPropagation();
                    e.preventDefault();
                    e.returnValue = false;
                    break;
            }
        };
        //计算器中非完成按钮的点击事件
        function doKeys(e) {
            currText = e.target.getAttribute("qs-data-value");
            inputText = inputText === '0' ? '' : inputText;
            switch (currText) {
                case '-':
                    inputText = inputText.indexOf('-') === -1 ? '-' + inputText : inputText.slice(1);
                    break;
                case '.':
                    inputText = inputText ? inputText === '-' ? inputText = '-0.' : (inputText.indexOf('.') === -1 ? inputText + '.' : inputText) : '0.';
                    break;
                case '<':
                    inputText = inputText ? inputText.slice(0, -1) : '';
                    break;
                case 'C':
                    inputText = '';
                    break;
                case '↑':
                    inputText = calcNumber(inputText, 2);
                    break;
                case '↓':
                    inputText = calcNumber(inputText, 1);
                    break;
                default:
                    inputText = inputText === '-0' ? '-' : inputText;
                    inputText += currText;
                    break;
            }
            // 数字计算器下拉时如果是空值时就赋值0
            if (inputText === '') {
                inputText = '0';
            } else if (inputText === '-') {
                inputText = '-0';
            }
            input.innerHTML = inputText;
            callbackEvery && callbackEvery(inputText ? parseFloat(inputText) : '');
        }
        //数字按键的处理逻辑
        function calcNumber(str, type) {
            //xym 20200720 修复数字小键盘逻辑错误
            if (str == '') {
                str = '0';
            }
            str = str === '-' ? "0" : str;
            offset = str.indexOf('.');
            fixed = offset > -1 ? str.length - offset - 1 : 0;
            str = Math.round(parseFloat(str) * Math.pow(10, fixed) + Math.pow(10, fixed) * Math.pow(-1, type)) / Math.pow(10, fixed);
            return str.toString();
        }

        // 注册监听事件
        keyBoard.addEventListener("click", keyBoardClick, false);
        keyBoardDiv.addEventListener("click", keyBoardDivClick, false);

        return keyBoardDiv;
    },
}

