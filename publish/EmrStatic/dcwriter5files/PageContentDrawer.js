"use strict";
// 文档内容绘图器 

import { DCTools20221228 } from "./DCTools20221228.js";
import { WriterControl_Task } from "./WriterControl_Task.js";
import { DCBinaryReader } from "./DCTools20221228.js";
import { WriterControl_Paint } from "./WriterControl_Paint.js";


export class PageContentDrawer {

    /**
     * 确保指定的字体安装了
     * @param {Array} fontNames 字体名称数组
     * @param { HTMLElement} element 相关的文档元素对象
     */
    EnsureFontInstall(fontNames, element) {
        var doc = document;
        if (element != null) {
            doc = element.ownerDocument;
        }
        if (fontNames == null || fontNames.length == 0) {
            return;
        }
        var list = window.__DCFontSourceList;
        if (list == null) {
            return;
        }
        for (var iCount = 0; iCount < fontNames.length; iCount++) {
            var fontName = fontNames[iCount];
            if (fontName == null || fontName.length == 0) {
                continue;
            }
            var info = DCTools20221228.ParseFontString(fontName);
            if (info == null || info.Name == null || info.Name.length == 0) {
                continue;
            }
           var strFontUrl = list[info.FullName];
            if (strFontUrl != null && strFontUrl.length > 0) {
                // 指定了字体文件下载地址
                if (doc.fonts != null && typeof (doc.fonts.forEach) == "function") {
                    var bolHasMatch = false;
                    doc.fonts.forEach(function (vKey, vValue) {
                        //console.log(vKey);
                        if (vKey.family == info.FullName) {
                            bolHasMatch = true;
                        }
                    });
                    if (bolHasMatch == false) {
                        // 未找到字体
                        //window.alert(strFontUrl);
                        //var ff = new FontFace(info.FullName, "local(\"" + info.FullName + "\"), url(\"" + strFontUrl + "\")");
                        var ff = new FontFace(info.FullName, "url(\"" + strFontUrl + "\")");
                        doc.fonts.add(ff);
                        ff.load().then(() =>{
                            console.log("DCWriter下载字体文件:" + strFontUrl);
                            var aaa = ff;
                            WriterControl_Task.AddTask({
                                TypeName: "FontLoaded",
                                RootElement: DCTools20221228.GetOwnerWriterControl(element),
                                CanEatTask: function (task2) {
                                    return task2.TypeName == this.TypeName || task2.RootElement == this.RootElement;
                                },
                                RunTask: function () {
                                    WriterControl_Paint.InvalidateAllView(this.RootElement);
                                }
                            }, (err) =>
                            {
                                console.log(err);
                            });
                        });
                    }
                    return;
                }
            }
        }
    }

    /** 获得数据读取器
     * @returns { DCBinaryReader } 数据读取器
     */
    get Reader() {
        return this._Reader;
    }
    /*** 设置数据读取器
     * @param { DCBinaryReader} reader 数据读取器
     */
    set Reader(reader) {
        this._Reader = reader;
    }
    /**
     * 初始化对象
     * @param {HTMLCanvasElement} vCanvasElement 画布对象
     * @param {Uint8Array | DCBinaryReader} vCodes 绘图指令
     */
    constructor(vCanvasElement, vCodes) {
        this.TypeName = "PageContentDrawer";
        //if (vCanvasElement == null) {
        //    throw "画布元素不能为空";
        //}
        //if (vCodes == null) {
        //    throw "绘图指令不能为空";
        //}
        this.IsMacintosh = /macintosh|mac os x/i.test(navigator.userAgent);
        this._Cancel = false;
        this.CreationTime = new Date();
        this.CreationTick = new Date().valueOf() % 10000;
        this.DoubleBuffer = false;
        this.TempElementForDoubleBuffer = null;
        this.AllowClip = true;
        this.RectangleForClear = null;
        this.CanvasElement = vCanvasElement;
        if (vCodes != null) {
            if (vCodes.TypeName = "DCBinaryReader") {
                this._Reader = vCodes;
            }
            else if (vCodes.length > 0) {
                this._Reader = new DCBinaryReader(vCodes);
            }
        }
        this.SaveCount = 0;
        this.PageUnit = 0;
        this.FontTable = new Array();
        this.PenTable = new Array();
        this.BrushTable = new Array();
        this.ColorTable = new Array();
        this.ImageTable = new Array();
        this.Pages = new Array();

        var MySelectPen = function (pen) {
            if (typeof (pen) == "number") {
                pen = this.PenTable[pen];
            }
            var oldPen = new Object();
            oldPen.Color = this.ctx.strokeStyle;
            oldPen.Width = this.ctx.lineWidth;
            this.ctx.strokeStyle = pen.Color;
            this.ctx.lineWidth = pen.Width + "px";
            if (this.ctx.setLineDash) {
                oldPen.LineDashData = this.ctx.getLineDash();
                this.ctx.setLineDash(pen.LineDashData);
            }
            return oldPen;
        };

        var MySelectBrush = function (brush) {
            var oldBrush = this.ctx.fillStyle;
            if (typeof (brush) == "number") {
                brush = this.BrushTable[brush];
            }
            this.ctx.fillStyle = brush;
            return oldBrush;
        };
        //this._offsetX = 0;
        //this._offsetY = 0;
        var SetPageUnit = function (Value) {
            ///*  World : 0, Display : 1, Pixel : 2, Point : 3, Inch : 4, Document : 5, Millimeter : 6, */
            this.PageUnit = Value;
            //switch (Value) {
            //    case 0: this.PageUnitRate = 1; break;
            //    case 1: this.PageUnitRate = 1; break;
            //    case 2: this.PageUnitRate = 1; break;
            //    case 3: this.PageUnitRate = 1.3333333333; break;
            //    case 4: this.PageUnitRate = 96; break;
            //    case 5: this.PageUnitRate = 0.32; break;
            //    case 6: this.PageUnitRate = 3.77952744641642; break;
            //    default:
            //        throw "不支持的单位:" + Value;
            //}
        }
        var _InnerSelectPen = function (pen) {
            var oldPen = new Object();
            oldPen.Color = this.ctx.strokeStyle;
            oldPen.Width = this.ctx.lineWidth;
            this.ctx.strokeStyle = pen.Color;
            this.ctx.lineWidth = pen.Width;
            if (pen.Width > 1) {
                var xx = "xxxxx";
            }
            if (this.ctx.setLineDash) {
                oldPen.LineDashData = this.ctx.getLineDash();
                this.ctx.setLineDash(pen.LineDashData);
            }
            return oldPen;
        }
        // 绘制线段
        var DrawLine = function (pen, x1, y1, x2, y2) {
            var oldPen = _InnerSelectPen.call(this, pen);
            this.ctx.beginPath();
            if (x1 == x2) {
                // 竖线
                this.ctx.moveTo(x1 + 0.5, y1 );
                this.ctx.lineTo(x2 +0.5, y2 );
            }
            else if (y1 == y2) {
                // 横线
                this.ctx.moveTo(x1 , y1 + 0.5);
                this.ctx.lineTo(x2 , y2 + 0.5);
            }
            else {
                // 斜线
                this.ctx.moveTo(x1, y1);
                this.ctx.lineTo(x2, y2);
            }
            this.ctx.stroke();
            _InnerSelectPen.call(this, oldPen);
        }
        // 绘制多条线段
        var DrawLines = function (pen, xyArray) {
            if (xyArray == null || xyArray.length < 4) {
                // 参数无意义
                return;
            }
            // 进行坐标修正
            var arrayFix = xyArray.slice();
            var len = arrayFix.length;
            // 修正所有的竖线坐标
            for (var iCount = 2; iCount < len; iCount += 2) {
                if (arrayFix[iCount - 2] == arrayFix[iCount]) {
                    // 出现竖线
                    var x = arrayFix[iCount];
                    for (iCount -= 2; iCount < len; iCount += 2) {
                        if (arrayFix[iCount] == x) {
                            arrayFix[iCount] += 0.5;
                        }
                        else {
                            break;
                        }
                    }
                }
            }
            // 修正所有的横线坐标
            for (var iCount = 3; iCount < len; iCount += 2) {
                if (arrayFix[iCount - 2] == arrayFix[iCount]) {
                    // 出现横线
                    var x = arrayFix[iCount];
                    for (iCount -= 2; iCount < len; iCount += 2) {
                        if (arrayFix[iCount] == x) {
                            arrayFix[iCount] += 0.5;
                        }
                        else {
                            break;
                        }
                    }
                }
            }
            var oldPen = _InnerSelectPen.call(this, pen);
            this.ctx.beginPath();
            this.ctx.moveTo(arrayFix[0], arrayFix[1]);
            for (var iCount = 0; iCount < len; iCount += 2) {
                var x = arrayFix[iCount];
                var y = arrayFix[iCount + 1];
                this.ctx.lineTo(x, y);
            }
            this.ctx.stroke();
            _InnerSelectPen.call(this, oldPen);
        }

        var DrawRectangle = function (pen, x, y, width, height) {
            var oldPen = _InnerSelectPen.call(this, pen);
            this.ctx.strokeRect(x, y, width, height);
            _InnerSelectPen.call(this, oldPen);
        }
        var FillRectangle = function (fillStyle, x, y, width, height) {
            var oldStyle = this.ctx.fillStyle;
            this.ctx.fillStyle = fillStyle;
            this.ctx.fillRect(x, y, width, height);
            this.ctx.fillStyle = oldStyle;
        }
        var DrawEllipse = function (pen, x, y, width, height) {
            var oldPen = _InnerSelectPen.call(this, pen);
            if (width == height) {
                // 正圆形
                this.ctx.beginPath();
                this.ctx.arc(x + width / 2, y + height / 2, width / 2, 0, 2 * Math.PI);
                this.ctx.stroke();
            }
            else {
                //var x = x;
                //var y = y;
                var a = width;
                var b = height;
                var k = 0.5522848;
                var ox = a * k; // 水平控制点偏移量
                var oy = b * k; // 垂直控制点偏移量
                this.ctx.beginPath();
                //从椭圆的左端点开始顺时针绘制四条三次贝塞尔曲线
                this.ctx.moveTo(x - a, y);
                this.ctx.bezierCurveTo(x - a, y - oy, x - ox, y - b, x, y - b);
                this.ctx.bezierCurveTo(x + ox, y - b, x + a, y - oy, x + a, y);
                this.ctx.bezierCurveTo(x + a, y + oy, x + ox, y + b, x, y + b);
                this.ctx.bezierCurveTo(x - ox, y + b, x - a, y + oy, x - a, y);
                this.ctx.closePath();
                this.ctx.stroke();
            }
            _InnerSelectPen.call(this, oldPen);
        }
        var FillEllipse = function (fillStyle, x, y, width, height) {
            var oldBrush = this.ctx.fillStyle;
            this.ctx.fillStyle = fillStyle;
            this.ctx.beginPath();
            if (width == height) {
                // 填充正圆
                this.ctx.arc((x + width / 2), (y + height / 2), width / 2, 0, Math.PI * 2);
            }
            else {
                //var x = x;
                //var y = y;
                var a = width;
                var b = height;
                var k = 0.5522848;
                var ox = a * k; // 水平控制点偏移量
                var oy = b * k; // 垂直控制点偏移量
                //从椭圆的左端点开始顺时针绘制四条三次贝塞尔曲线
                this.ctx.moveTo(x - a, y);
                this.ctx.bezierCurveTo(x - a, y - oy, x - ox, y - b, x, y - b);
                this.ctx.bezierCurveTo(x + ox, y - b, x + a, y - oy, x + a, y);
                this.ctx.bezierCurveTo(x + a, y + oy, x + ox, y + b, x, y + b);
                this.ctx.bezierCurveTo(x - ox, y + b, x - a, y + oy, x - a, y);
            }
            this.ctx.closePath();
            this.ctx.fill();
            this.ctx.fillStyle = oldBrush;
        }
        this._ClipBounds = null;
        var CreateStateBack = function (drawer) {
            var list = [
                drawer.ctx.strokeStyle,
                drawer.ctx.lineWidth,
                drawer.ctx.setLineDash ? drawer.ctx.getLineDash() : null,
                drawer.ctx.fillStyle,
                drawer.ctx.font,
                drawer.ctx.getTransform()];
            list.restore = function (drawer) {
                drawer.ctx.strokeStyle = this[0];
                drawer.ctx.lineWidth = this[1];
                if (drawer.ctx.setLineDash) {
                    drawer.ctx.setLineDash(this[2]);
                }
                drawer.ctx.fillStyle = this[3];
                drawer.ctx.font = this[4];
                //var item4 = drawer.ctx.getTransform();
                drawer.ctx.setTransform(this[5]);
            };
            return list;
        };
        var SetClip = function (left, top, width, height) {
            if (this.AllowClip == true) {
                var stateBack = CreateStateBack(this);
                while (this.SaveCount > 0) {
                    this.ctx.restore();
                    this.SaveCount--;
                }
                this.ctx.save();
                this.SaveCount++;
                stateBack.restore.call(stateBack, this);
                this.ctx.beginPath();
                this.ctx.rect(left, top, width + 1, height + 1);
                this.ctx.clip();
                return;
            }
        };
        var ResetClip = function () {
            if (this.AllowClip == true) {
                var stateBack = CreateStateBack(this);
                while (this.SaveCount > 0) {
                    this.ctx.restore();
                    this.SaveCount--;
                }
                this.ctx.save();
                this.SaveCount++;
                stateBack.restore.call(stateBack, this);
                this.ctx.beginPath();
                this.ctx.rect(-100000000, -100000000, 200000000, 200000000);
                this.ctx.clip();
            }
        };

        // 定义绘图函数列表 ******************
        this.Functions = new Array();
        var funcIndex = 0;
        //0 First
        this.Functions[0] = function (reader) { return 0; };
        //1 SetCurrentFont
        this.Functions[1] = function (reader) {
            this.ctx.font = this.FontTable[reader.ReadInt16()];
        };
        //2 SetCurrentBrush
        this.Functions[2] = function (reader) {
            var index = reader.ReadInt16();
            this.ctx.fillStyle = this.BrushTable[index];
            //var oldBrush = this.ctx.fillStyle;
            //if (typeof (brush) == "number") {
            //    brush = this.BrushTable[brush];
            //}
            //this.ctx.fillStyle = brush;
            //return oldBrush;
            //MySelectBrush.call(this, reader.ReadInt16());
        };
        //3 SetCurrentPen
        this.Functions[3] = function (reader) {
            MySelectPen.call(this, reader.ReadInt16());
        };
        //4 AddPage
        this.Functions[4] = null;
        //5 DrawLine
        this.Functions[5] = function (reader) {
            var pIndex = reader.ReadInt16();
            var p4 = this.PenTable[pIndex];
            if (p4 == null) {
                console.log("zzzz");
            }
            DrawLine.call(
                this,
                p4,
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32());
        };
        //6 DrawString
        this.Functions[6] = function (reader) {
            var str = reader.ReadString();
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            var w = reader.ReadInt16() + 4;
            if (str.length > 15) {
                this.ctx.letterSpacing = "-0.3px";
            }
            else {
                this.ctx.letterSpacing = "";
            }
            this.ctx.fillText(
                str,
                x - 0.5,
                y,
                w);
        };
        //7 DrawChar
        this.Functions[7] = function (reader) {
            var txt = reader.ReadChar();
            //因为在macos下宋体被定义为非等宽字体导致排版错乱，现在修改此设置** 这里的判断移动到 17:FontTable 中处理了。**
            //var hasChangeFont = false
            //if (this.IsMacintosh) {
            //    var oldFont = this.ctx.font;
            //    if (oldFont && oldFont.indexOf('宋体') >= 0) {
            //        this.ctx.font = oldFont.replace('宋体', '宋体,monospace');
            //        hasChangeFont = true
            //    }
            //}
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            this.ctx.fillText(
                txt,
                x - 0.5,
                y );
            //if (hasChangeFont) {
            //    this.ctx.font = oldFont;
            //}
        };
        //8 DrawRoundRectangle
        this.Functions[8] = function (reader) {
            //var oldStyle = this.ctx.fillStyle;
            var pindex = reader.ReadInt16();
            _InnerSelectPen.call(this, this.PenTable[pindex]);
            //this.ctx.strokeStyle =  this.PenTable[pindex];
            //this.ctx.lineWidth = "1";
            var vRadius = reader.ReadInt16();
            var vLeft = reader.ReadInt32();
            var vTop = reader.ReadInt32();
            var vWidth = reader.ReadInt32();
            var vHeight = reader.ReadInt32();
            if (vRadius > vWidth / 2) vRadius = vWidth / 2;
            if (vRadius > vHeight / 2) vRadius = vHeight / 2;
            this.ctx.beginPath();
            this.ctx.moveTo(vLeft + vRadius, vTop);
            this.ctx.lineTo(vLeft + vWidth - vRadius, vTop);
            this.ctx.arcTo(vLeft + vWidth, vTop, vLeft + vWidth, vTop + vRadius, vRadius);
            this.ctx.lineTo(vLeft + vWidth, vTop + vHeight - vRadius);
            this.ctx.arcTo(vLeft + vWidth, vTop + vHeight, vLeft + vWidth - vRadius, vTop + vHeight, vRadius);
            this.ctx.lineTo(vLeft + vRadius, vTop + vHeight);
            this.ctx.arcTo(vLeft, vTop + vHeight, vLeft, vTop + vHeight - vRadius, vRadius);
            this.ctx.lineTo(vLeft, vTop + vRadius);
            this.ctx.arcTo(vLeft, vTop, vLeft + vRadius, vTop, vRadius);
            this.ctx.stroke();

            //DrawRectangle.call(
            //    this,
            //    this.PenTable[reader.ReadInt16()],
            //    reader.ReadInt32(),
            //    reader.ReadInt32(),
            //    reader.ReadInt32(),
            //    reader.ReadInt32());
        };
        //9 DrawRectangle
        this.Functions[9] = function (reader) {
            DrawRectangle.call(
                this,
                this.PenTable[reader.ReadInt16()],
                reader.ReadInt32() + 0.5,
                reader.ReadInt32() + 0.5,
                reader.ReadInt32(),
                reader.ReadInt32());
        };
        //10 FillRectangle
        this.Functions[10] = function (reader) {
            var oldStyle = this.ctx.fillStyle;
            this.ctx.fillStyle = this.BrushTable[reader.ReadInt16()];
            var vLeft = reader.ReadInt32();
            var vTop = reader.ReadInt32();
            var vWidth = reader.ReadInt32();
            var vHeight = reader.ReadInt32();
            this.ctx.fillRect(vLeft + 0.5, vTop + 0.5, vWidth, vHeight);
            this.ctx.fillStyle = oldStyle;
            //if (vWidth == 338) {
            //    var trnas = this.ctx.getTransform();
            //    console.log(vLeft + "  $$$ " + vWidth + " " + trnas);
            //}
        };
        //11 DrawEllipse
        this.Functions[11] = function (reader) {
            DrawEllipse.call(
                this,
                this.PenTable[reader.ReadInt16()],
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32());
        };
        //12 FillEllipse
        this.Functions[12] = function (reader) {
            FillEllipse.call(
                this,
                this.BrushTable[reader.ReadInt16()],
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32());
        };
        //13 CheckVersion
        this.Functions[13] = function (reader) {
            if (reader.ReadInt32() != 20221031) {
                throw "系统版本不匹配。"
            }
        };
        //14 SetTransform
        this.Functions[14] = function (reader) {
            var v1 = reader.ReadSingle();
            var v2 = reader.ReadSingle();
            var v3 = reader.ReadSingle();
            var v4 = reader.ReadSingle();
            var v5 = reader.ReadSingle();
            var v6 = reader.ReadSingle();
            v5 = Math.round(v5);
            v6 = Math.round(v6);
            //if (v5 != Math.round(v5)) {
            //    v5 = v5;
            //}
            this.ctx.setTransform(v1, v2, v3, v4, v5, v6);
        };
        //15 DrawImageExt
        this.Functions[15] = function (reader) {
            var img = this.ImageTable.GetValue(reader.ReadInt16());
            var p1 = reader.ReadInt16();
            var p2 = reader.ReadInt16();
            var p3 = reader.ReadInt16();
            var p4 = reader.ReadInt16();
            var p5 = reader.ReadInt32();
            var p6 = reader.ReadInt32();
            var p7 = reader.ReadInt32();
            var p8 = reader.ReadInt32();
            //if (img.naturalHeight == 0) {
            //    console.log("sssssssssssss");
            //}
            this.ctx.drawImage(
                img,
                p1,
                p2,
                p3,
                p4,
                p5,
                p6,
                p7,
                p8);
        };

        //16 Save
        this.Functions[16] = function (reader) {
            this.ctx.save();
        };
        //17 Restore
        this.Functions[17] = function (reader) {
            this.ctx.restore();
        };
        //18 FontTable
        this.Functions[18] = function (reader) {
            //this.FontTable = new Array();
            var len = reader.ReadInt16();
            for (var iCount = 0; iCount < len; iCount++) {
                var strFont = reader.ReadString();
                if (this.IsMacintosh) {
                    if (strFont.indexOf("宋体") >= 0) {
                        strFont = strFont.replace('宋体', '宋体,monospace');
                    }
                }
                this.FontTable.push(strFont);
            }
            this.EnsureFontInstall(this.FontTable, this.CanvasElement);
        };
        //19 ColorTable
        this.Functions[19] = function (reader) {
            //this.ColorTable = new Array();
            var len = reader.ReadInt16();
            for (var iCount = 0; iCount < len; iCount++) {
                this.ColorTable.push(reader.ReadString());
            }
        };
        //20 PenTable
        this.Functions[20] = function (reader) {
            //this.PenTable = new Array();
            var len = reader.ReadInt16();
            for (var iCount = 0; iCount < len; iCount++) {
                var p = new Object();
                p.Color = reader.ReadString();
                p.Width = reader.ReadByte();
                switch (reader.ReadByte()) {
                    case 0: p.LineDashData = []; break; // Solid
                    case 1: p.LineDashData = [6, 6]; break; // Dash
                    case 2: p.LineDashData = [3, 3]; break; // Dot
                    case 3: p.LineDashData = [6, 3]; break; // DashDot
                    case 4: p.LineDashData = [6, 3, 3]; break; // DashDotDot
                    default: p.LineDashData = []; break;
                }
                this.PenTable.push(p);
            }
        };
        //21 DrawImageXY
        this.Functions[21] = function (reader) {
            this.ctx.drawImage(
                this.ImageTable.GetValue(reader.ReadInt16()),
                reader.ReadInt32(),
                reader.ReadInt32());
        };
        //22 DrawImage
        this.Functions[22] = function (reader) {
            var imgIndex = reader.ReadInt16();
            var img = this.ImageTable.GetValue(imgIndex);
            this.ctx.drawImage(
                img,
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32());
        };
        //23 SetPageUnit
        this.Functions[23] = function (reader) {
            SetPageUnit.call(this, reader.ReadByte());
        };

        //24 TranslateTransform
        this.Functions[24] = function (reader) {
            var dx = reader.ReadSingle();
            var dy = reader.ReadSingle();
            //console.log(dx + " ######## " + dy);
            this.ctx.translate(dx, dy);
        };
        //25 RotateTransform
        this.Functions[25] = function (reader) {
            this.ctx.rotate(reader.ReadSingle());
        };
        //26 ScaleTransform
        this.Functions[26] = function (reader) {
            var sx = reader.ReadSingle();
            var sy = reader.ReadSingle();
            //if (sx != 1 || sy != 1) {
            //    console.log("8");
            //}
            this.ctx.scale(sx, sy);
        };
        //27 SetClip
        this.Functions[27] = function (reader) {
            SetClip.call(
                this,
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32());
        };
        //28 ResetClip
        this.Functions[28] = function (reader) {
            ResetClip.call(this);
        };

        this.FunctionIndex_ImageTable = 29;
        //29 ImageTable
        this.Functions[29] = function (reader) {
            // 加载图片列表
            var len = reader.ReadInt16();
            //this.ImageTable = new Array();
            this.ImageTable.GetValue = function (index) {
                var imgElement2 = this[index];
                return imgElement2;
            };
            this.ImageTable.loaded = false;
            for (var iCount = 0; iCount < len; iCount++) {
                var imgElemnent = document.createElement("IMG");// new Image();
                imgElemnent.loading = "eager";
                this.ImageTable.push(imgElemnent);
                var strImageHeader = null;
                switch (reader.ReadByte()) {
                    case 0: strImageHeader = "data:image/jpeg;base64,"; break;
                    case 1: strImageHeader = "data:image/png;base64,"; break;
                    case 2: strImageHeader = "data:image/gif;base64,"; break;
                    case 3: strImageHeader = "data:image/bmp;base64,"; break;
                    case 4: strImageHeader = "data:image;base64,"; break;
                }
                var bsData = reader.ReadByteArray();
                var txt2 = '';
                var chunk = 8 * 1024;
                for (var i = 0; i < bsData.length / chunk; i++) {
                    txt2 += String.fromCharCode.apply(null, bsData.slice(i * chunk, (i + 1) * chunk));
                }
                txt2 += String.fromCharCode.apply(null, bsData.slice(i * chunk));
                var strImageUrl = strImageHeader + window.btoa(txt2);
                imgElemnent.decoding = "sync";
                imgElemnent.src = strImageUrl;
                imgElemnent.decode();
            }
        };
        //30 BrushTable
        this.Functions[30] = function (reader) {
            var len = reader.ReadInt16();
            this.BrushTable = new Array();
            for (var iCount = 0; iCount < len; iCount++) {
                var strBrush = reader.ReadString();
                if (strBrush.charAt(0) == "*") {
                    // 线性渐变画刷
                    var strItems = strBrush.split('$');
                    var gb = this.ctx.createLinearGradient(
                        parseFloat(strItems[1]),
                        parseFloat(strItems[2]),
                        parseFloat(strItems[3]),
                        parseFloat(strItems[4]));
                    for (var itemCount = 5; itemCount < strItems.length; itemCount += 2) {
                        gb.addColorStop(parseFloat(strItems[itemCount]), strItems[itemCount + 1]);
                    }
                    this.BrushTable.push(gb);
                }
                else {
                    // 普通纯色画刷
                    this.BrushTable.push(strBrush);
                }
            }
        };
        //31 DrawLines
        this.Functions[31] = function (reader) {
            var pIndex = reader.ReadInt16();
            var len = reader.ReadInt16();
            var ps = new Array();
            for (var iCount = 0; iCount < len; iCount++) {
                ps.push(reader.ReadInt32());
                ps.push(reader.ReadInt32());
            }
            DrawLines.call(
                this,
                this.PenTable[pIndex],
                ps);
        };
        //32 空白
        this.Functions[32] = null;
        //33 FillPolygon
        this.Functions[33] = function (reader) {
            var bIndex = reader.ReadInt16();
            var len = reader.ReadInt16();
            var brushBack = this.ctx.fillStyle;
            this.ctx.fillStyle = this.BrushTable[bIndex];
            this.ctx.beginPath();
            var firstX = 0;
            var firstY = 0;
            for (var iCount = 0; iCount < len; iCount++) {
                var x = reader.ReadInt32();
                var y = reader.ReadInt32();
                if (iCount == 0) {
                    firstX = x;
                    firstY = y;
                    this.ctx.moveTo(x, y);
                }
                else {
                    this.ctx.lineTo(x, y);
                }
            }
            this.ctx.lineTo(firstX, firstY);
            this.ctx.fill();
            this.ctx.fillStyle = brushBack;
        };
        //34 FillPie
        this.Functions[34] = function (reader) {
            var bIndex = reader.ReadInt16();
            var brushBack = this.ctx.fillStyle;
            this.ctx.fillStyle = this.BrushTable[bIndex] ;
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            var w = reader.ReadInt32();
            var h = reader.ReadInt32();
            var startAngle = reader.ReadSingle();
            var endAngle = reader.ReadSingle();
            this.ctx.beginPath();
            this.ctx.moveTo(x + w / 2, y + h / 2);
            this.ctx.ellipse(
                x + w / 2,
                y + h / 2,
                w / 2,
                h / 2,
                0,
                startAngle,
                endAngle);
            this.ctx.fill();
            this.ctx.fillStyle = brushBack;
        };
        //35 DrawPie
        this.Functions[35] = function (reader) {
            var pIndex = reader.ReadInt16();
            _InnerSelectPen.call(this, this.PenTable[pIndex]);
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            var w = reader.ReadInt32();
            var h = reader.ReadInt32();
            var startAngle = reader.ReadSingle();
            var endAngle = reader.ReadSingle();
            this.ctx.beginPath();
            this.ctx.moveTo(x + w / 2, y + h / 2);
            this.ctx.ellipse(
                x + w / 2,
                y + h / 2,
                w / 2,
                h / 2,
                0,
                startAngle,
                endAngle);
            this.ctx.stroke();
        };
        function ParseGraphicsPath(strPathCode, zoomRate, offsetX, offsetY) {
            var pathCode = JSON.parse(strPathCode);
            var result = new Path2D();
            for (var itemIndex = 1; itemIndex < pathCode.length;) {
                var itemType = pathCode[itemIndex++];
                switch (itemType) {
                    case 0://AddArc
                        {
                            var x = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y = pathCode[itemIndex++] * zoomRate + offsetY;
                            var w = pathCode[itemIndex++] * zoomRate;
                            var h = pathCode[itemIndex++] * zoomRate;
                            var startAngle = pathCode[itemIndex++];
                            var endAngle = pathCode[itemIndex++];
                            if (startAngle > endAngle) {
                                result.ellipse(
                                    x + w / 2,
                                    y + h / 2,
                                    w / 2,
                                    h / 2,
                                    0,
                                    startAngle,
                                    endAngle,
                                    true);
                            }
                            else {
                                result.ellipse(
                                    x + w / 2,
                                    y + h / 2,
                                    w / 2,
                                    h / 2,
                                    0,
                                    startAngle,
                                    endAngle);
                            }
                        }
                        break;
                    case 1://AddBezier
                        {
                            var x1 = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y1 = pathCode[itemIndex++] * zoomRate + offsetY;
                            var x2 = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y2 = pathCode[itemIndex++] * zoomRate + offsetY;
                            var x3 = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y3 = pathCode[itemIndex++] * zoomRate + offsetY;
                            var x4 = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y4 = pathCode[itemIndex++] * zoomRate + offsetY;
                            result.lineTo(x1, y1);
                            result.bezierCurveTo(x2, y2, x3, y3, x4, y4);
                        }
                        break;
                    case 3:// AddEllipse
                        {
                            var x = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y = pathCode[itemIndex++] * zoomRate + offsetY;
                            var w = pathCode[itemIndex++] * zoomRate;
                            var h = pathCode[itemIndex++] * zoomRate;
                            result.ellipse(x + w / 2, y + h / 2, w / 2, h / 2, 0, 0, Math.PI * 2);
                        }
                        break;
                    case 4:// AddLine
                        {
                            var x1 = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y1 = pathCode[itemIndex++] * zoomRate + offsetY;
                            var x2 = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y2 = pathCode[itemIndex++] * zoomRate + offsetY;
                            result.lineTo(x1, y1);
                            result.lineTo(x2, y2);
                        }
                        break;
                    case 5:// AddLines
                        {
                            var ps = pathCode[itemIndex++];
                            result.lineTo(
                                ps[0] * zoomRate + offsetX,
                                ps[1] * zoomRate + offsetY);
                            for (var pIndex = 2; pIndex < ps.length; pIndex += 2) {
                                var x = ps[pIndex] * zoomRate + offsetX;
                                var y = ps[pIndex + 1] * zoomRate + offsetY;
                                result.lineTo(x, y);
                            }
                        }
                        break;
                    case 6://AddPie
                        {
                            var x = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y = pathCode[itemIndex++] * zoomRate + offsetY;
                            var w = pathCode[itemIndex++] * zoomRate;
                            var h = pathCode[itemIndex++] * zoomRate;
                            var startAngle = pathCode[itemIndex++];
                            var endAngle = pathCode[itemIndex++];
                            if (startAngle > endAngle) {
                                var temp2 = startAngle;
                                startAngle = endAngle;
                                endAngle = temp2;
                            }
                            result.lineTo(x + w / 2, y + h / 2);
                            result.ellipse(
                                x + w / 2,
                                y + h / 2,
                                w / 2,
                                h / 2,
                                0,
                                startAngle,
                                endAngle);
                        }
                        break;
                    case 7://AddRectangle
                        {
                            var x = pathCode[itemIndex++] * zoomRate + offsetX;
                            var y = pathCode[itemIndex++] * zoomRate + offsetY;
                            var w = pathCode[itemIndex++] * zoomRate;
                            var h = pathCode[itemIndex++] * zoomRate;
                            result.rect(x, y, w, h);
                        }
                        break;
                    case 11://AddPolygon
                        {
                            var ps = pathCode[itemIndex++];
                            result.lineTo(
                                ps[0] * zoomRate + offsetX,
                                ps[1] * zoomRate + offsetY);
                            for (var pIndex = 2; pIndex < ps.length; pIndex += 2) {
                                var x = ps[pIndex] * zoomRate + offsetX;
                                var y = ps[pIndex + 1] * zoomRate + offsetY;
                                result.lineTo(x, y);
                            }
                            result.lineTo(
                                ps[0] * zoomRate + offsetX,
                                ps[1] * zoomRate + offsetY);
                        }
                        break;
                    default:
                        throw "不支持的GraphPath：" + itemType;
                }
            }
            return result;
        };
        //36 DrawPath
        this.Functions[36] = function (reader) {
            var pIndex = reader.ReadInt16();
            _InnerSelectPen.call(this, this.PenTable[pIndex]);
            var zoomRate3 = reader.ReadSingle();
            var offsetX3 = reader.ReadSingle();
            var offsetY3 = reader.ReadSingle();
            var strPathCode = reader.ReadString();
            var pathData = ParseGraphicsPath(strPathCode, zoomRate3, offsetX3, offsetY3);
            this.ctx.stroke(pathData);
        };
        //37 FillPath
        this.Functions[37] = function (reader) {
            var bIndex = reader.ReadInt16();
            var fillStyleBack = this.ctx.fillStyle;
            this.ctx.fillStyle = this.BrushTable[bIndex];
            var zoomRate3 = reader.ReadSingle();
            var offsetX3 = reader.ReadSingle();
            var offsetY3 = reader.ReadSingle();
            var strPathCode = reader.ReadString();
            var pathData = ParseGraphicsPath(strPathCode, zoomRate3, offsetX3, offsetY3);
            this.ctx.fill(pathData);
            this.ctx.fillStyle = fillStyleBack;
        };
        function GetStandartImageByIndex(imgIndex) {
            var imgs = window.__DCStandardImageList;
            if (imgs != null && imgs.length > 0 && imgIndex >= 0 && imgIndex < imgs.length) {
                var img = imgs[imgIndex];
                return img;
            }
            return null;
        };
        //38 DrawImageXYStdImageIndex
        this.Functions[38] = function (reader) {
            var img = GetStandartImageByIndex(reader.ReadInt16());
            if (img != null) {
                this.ctx.drawImage(
                    img,
                    reader.ReadInt32(),
                    reader.ReadInt32());
            }
            else {
                reader.ReadInt32();
                reader.ReadInt32();
            }
        };
        //39 DrawImageStdImageIndex
        this.Functions[39] = function (reader) {
            var img = GetStandartImageByIndex(reader.ReadInt16());
            if (img != null) {
                this.ctx.drawImage(
                    img,
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32());
            }
            else {
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
            }
        };
        // 40 ClearRect
        this.Functions[40] = function (reader) {
            this.ctx.clearRect(reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32());
        };
        // 41 SetImageSmoothing
        this.Functions[41] = function (reader) {
            var v = reader.ReadBoolean();
            if (v == true) {
                this.ctx.imageSmoothingEnabled = true;
                this.ctx.imageSmoothingQuality = "high";
            }
            else {
                this.ctx.imageSmoothingEnabled = false;
            }
        };
        // 42 SetTextBaseline
        this.Functions[42] = function (reader) {
            var type = reader.ReadByte();
            if (type == 0) this.ctx.textBaseline = "top";
            else if (type == 1) this.ctx.textBaseline = "middle";
            else if (type == 2) this.ctx.textBaseline = "bottom";
            else if (type == 3) this.ctx.textBaseline = "hanging";
        };
        // 43 FillRoundRectangle
        this.Functions[43] = function (reader) {
            var oldStyle = this.ctx.fillStyle;
            this.ctx.fillStyle = this.BrushTable[reader.ReadInt16()];
            var vRadius = reader.ReadInt16();
            var vLeft = reader.ReadInt32();
            var vTop = reader.ReadInt32();
            var vWidth = reader.ReadInt32();
            var vHeight = reader.ReadInt32();
            if (vRadius > vWidth / 2) vRadius = vWidth / 2;
            if (vRadius > vHeight / 2) vRadius = vHeight / 2;
            this.ctx.beginPath();
            this.ctx.moveTo(vLeft + vRadius, vTop);
            this.ctx.lineTo(vLeft + vWidth - vRadius, vTop);
            this.ctx.arcTo(vLeft + vWidth, vTop, vLeft + vWidth, vTop + vRadius, vRadius);
            this.ctx.lineTo(vLeft + vWidth, vTop + vHeight - vRadius);
            this.ctx.arcTo(vLeft + vWidth, vTop + vHeight, vLeft + vWidth - vRadius, vTop + vHeight, vRadius);
            this.ctx.lineTo(vLeft + vRadius, vTop + vHeight);
            this.ctx.arcTo(vLeft, vTop + vHeight, vLeft, vTop + vHeight - vRadius, vRadius);
            this.ctx.lineTo(vLeft, vTop + vRadius);
            this.ctx.arcTo(vLeft, vTop, vLeft + vRadius, vTop, vRadius);
            this.ctx.fill();
            this.ctx.fillStyle = oldStyle;
        };
        // 44 FillMatrix 填充一个矩阵，用于绘制二维码
        this.Functions[44] = function (reader) {
            var fillColor = reader.ReadString();
            var strData = reader.ReadString();
            var vLeft = reader.ReadInt32();
            var vTop = reader.ReadInt32();
            var vWidth = reader.ReadInt32();
            var vHeight = reader.ReadInt32();
            if (strData == null || strData.length == 0) {
                throw "FillMatrix 数据错误";
            }
            var strLines = strData.split('|');
            var oldStyle = this.ctx.fillStyle;
            this.ctx.fillStyle = fillColor;
            this.ctx.beginPath();
            for (var rowIndex = 0; rowIndex < strLines.length; rowIndex++) {
                var strLine = strLines[rowIndex];
                for (var colIndex = 0; colIndex < strLine.length; colIndex++) {
                    if (strLine.charAt(colIndex) == '0') {
                        // 此处填充一个小黑框
                        this.ctx.rect(
                            vLeft + (vWidth * colIndex / strLine.length),
                            vTop + (vHeight * rowIndex / strLines.length),
                            vWidth / strLine.length,
                            vHeight / strLines.length);
                    }
                }
            }
            this.ctx.fill();
            this.ctx.fillStyle = oldStyle;
        };
        // FillRectangleFloat 采用浮点数坐标的填充矩形
        this.Functions[45] = function (reader) {
            var oldStyle = this.ctx.fillStyle;
            this.ctx.fillStyle = reader.ReadString();
            var vLeft = reader.ReadSingle();
            var vTop = reader.ReadSingle();
            var vWidth = reader.ReadSingle();
            var vHeight = reader.ReadSingle();
            this.ctx.fillRect(vLeft, vTop, vWidth, vHeight);
            this.ctx.fillStyle = oldStyle;
        };
    }
    /** 取消任务 */
    Cancel() {
        this._Cancel = true;
    }
    /**
     * 判断能否吃掉其他任务
     * @param {any} otherTask 其他任务对象
     * @returns {boolean} 能否被其他任务吃掉
     */
    CanEatTask(otherTask) {
        if (otherTask == null
            || otherTask == this
            || otherTask.TypeName != "PageContentDrawer") {
            return false;
        }
        if (this.CanvasElement != null
            && this.RectangleForClear != null
            && this.CanvasElement == otherTask.CanvasElement
            && otherTask.RectangleForClear != null) {
            if (this.RectangleForClear[0] <= otherTask.RectangleForClear[0]
                && this.RectangleForClear[1] <= otherTask.RectangleForClear[1]
                && this.RectangleForClear[0] + this.RectangleForClear[2] >= otherTask.RectangleForClear[0] + otherTask.RectangleForClear[2]
                && this.RectangleForClear[1] + this.RectangleForClear[3] >= otherTask.RectangleForClear[1] + otherTask.RectangleForClear[3]) {
                // 可以吞并任务
                return true;
            }
        }
        return false;
    }

    /** 将自己添加到任务列表中 */
    AddToTask() {
        if (this.OwnerWriterControl == null && this.CanvasElement != null) {
            this.OwnerWriterControl = DCTools20221228.GetOwnerWriterControl(this.CanvasElement);
        }
        WriterControl_Task.AddTask(this);
    }
    /**
     * 设置清空区域
     * @param {number} vLeft 区域的左端位置
     * @param {number} vTop 区域的顶端位置
     * @param {number} vWidth 区域的宽度
     * @param {number} vHeight 区域的高度
     */
    SetClearRectangle(vLeft, vTop, vWidth, vHeight) {
        this.RectangleForClear = [vLeft, vTop, vWidth, vHeight];
    }
    ///**
    // * 删除绘图任务
    // * @param {Array} tasks 绘图任务列表
    // * @param {PageContentDrawer} item 要删除的任务对象
    // */
    //__RemoveTask(tasks, item) {
    //    if (tasks != null) {
    //        // 从绘图任务队列中删除对象
    //        var index = tasks.indexOf(this);
    //        if (index >= 0) {
    //            tasks.splice(index, 1);
    //        }
    //    }
    //}

    /**
     * 启用双缓冲功能
     * @param {number} vLeft 绘制区域的坐标
     * @param {number} vTop 绘制区域的坐标
     * @param {number} vWidth 绘制区域的坐标
     * @param {number} vHeight 绘制区域的坐标
     */
    EnableDoubleBuffer(vLeft, vTop, vWidth, vHeight) {
        this.DoubleBuffer = true;
        this.DoubleBufferBounds = [vLeft, vTop, vWidth, vHeight];
    }
    /** 执行绘图任务 */
    RunTask() {
        if (this._Cancel == true) {
            return;
        }
        if (this.Reader == null && typeof (this.EventQueryCodes) == "function") {
            var codes = this.EventQueryCodes.call(this, this);
            if (this._Cancel == true) {
                // 取消任务了
                return;
            }
            if (codes != null) {
                if (codes.TypeName == "DCBinaryReader") {
                    // 传入的已经是一个数据读取器
                    this.Reader = codes;
                }
                else if (codes.length > 0) {
                    this.Reader = new DCBinaryReader(codes);
                }
            }
        }
        if (this.Reader == null
            || this.CanvasElement == null
            || this._Cancel == true) {
            return;
        }
        if (this.Reader == null || this._Cancel == true) {
            return;
        }
        if (this.ImageTable != null && this.ImageTable.length > 0) {
            var tick = new Date().valueOf() - this.CreationTime.valueOf();
            if (tick < 500) {
                // 任务创建尚未有500毫秒，则判断图片是否加载完毕
                for (var iCount = 0; iCount < this.ImageTable.length; iCount++) {
                    var img = this.ImageTable[iCount];
                    if (img.complete == false) {
                        // 还存在图片尚未加载完毕,等待,暂时退出
                        WriterControl_Task.AddTaskFast(this);
                        return false;
                    }
                }
            }
        }
        //var tasks = window.__DCWriter_DrawTasks20221221;
        //this.DoubleBuffer = false;
        if (this.DoubleBuffer == true) {
            // 启用双缓冲技术
            if (this.TempElementForDoubleBuffer == null) {
                this.TempElementForDoubleBuffer = document.createElement("CANVAS");
            }
            if (this.TempElementForDoubleBuffer.width != this.CanvasElement.width
                || this.TempElementForDoubleBuffer.height != this.CanvasElement.height) {
                this.TempElementForDoubleBuffer.width = this.CanvasElement.width;// this.DoubleBufferBounds[2];
                this.TempElementForDoubleBuffer.height = this.CanvasElement.height;// this.DoubleBufferBounds[3];
            }
            this.ctx = this.TempElementForDoubleBuffer.getContext("2d");
        }
        else {
            this.ctx = this.CanvasElement.getContext("2d");
        }
        this.ctx.imageSmoothingEnabled = false;
        var thisClass = this;
        this.DrawCodeCount = 0;
        var fLength = thisClass.Functions.length;
        if (this.AllowClip) {
            thisClass.ctx.save();
        }
        //function ParseFunctionIndex(strIndex) {
        //    var names = [
        //        "First",//0,
        //        "SetCurrentFont",//1,
        //        "SetCurrentBrush",//2,
        //        "SetCurrentPen",//3,
        //        "AddPage",//4,
        //        "DrawLine",//5,
        //        "DrawString",//6,
        //        "DrawChar",//7,
        //        "DrawRoundRectangle",//8,
        //        "DrawRectangle",//9,
        //        "FillRectangle",//10,
        //        "DrawEllipse",//11,
        //        "FillEllipse",//12,
        //        "CheckVersion",//13,
        //        "SetTransform",//14,
        //        "DrawImageExt",//15,
        //        "Save",//16,
        //        "Restore",//17,
        //        "FontTable",//18,
        //        "ColorTable",//19,
        //        "PenTable",//20,
        //        "DrawImageXY",//21,
        //        "DrawImage",//22,
        //        "SetPageUnit",//23,
        //        "TranslateTransform",//24,
        //        "RotateTransform",//25,
        //        "ScaleTransform",//26,
        //        "SetClip",//27,
        //        "ResetClip",//28,
        //        "ImageTable",//29,
        //        "BrushTable",//30,
        //        "DrawLines",//31,
        //        "UpdateClearRectangle"];
        //    for (var iCount = names.length - 1; iCount >= 0; iCount--) {
        //        if (strIndex == names[iCount]) {
        //            return iCount;
        //        }
        //    }
        //    return -1;
        //};
        var thisReader = this.Reader;
        this.ctx.imageSmoothingEnabled = false;
        this.ctx.textBaseline = "alphabetic";
        this.ctx.textRendering = "optimizeLegibility";
        //var lastFuncIndex = -1;
        //var funcIndexs = new Array();
        while (thisReader.EOF == false && this._Cancel != true) {
            //var f1 = thisReader.ReadByte();
            //var f2 = thisReader.ReadByte();
            //if (f1 != 0xff && f2 != 0xff) {
            //    console.log("aaaa222 " + thisReader.Position);
            //}
            var fIndex = thisReader.ReadByte();

            //if (typeof (fIndex) == "number") {
            //    console.log("zzz");
            //}
            //fIndex = ParseFunctionIndex(fIndex);

            if (fIndex == 32) {
                if (this.RectangleForClear != null) {
                    this.ctx.clearRect(
                        this.RectangleForClear[0],
                        this.RectangleForClear[1],
                        this.RectangleForClear[2],
                        this.RectangleForClear[3]);
                    this.RectangleForClear = null;
                }
                continue;
            }
            if (fIndex >= 0 && fIndex < fLength) {
                var func = thisClass.Functions[fIndex];
                if (func == null) {
                    throw "不支持的模块编号:" + fIndex;
                }
                thisClass.DrawCodeCount++;
                func.call(thisClass, thisReader);
                if (fIndex == this.FunctionIndex_ImageTable) {
                    // 遇到图片库，则需要等待图片数据加载完毕，暂时退出
                    WriterControl_Task.AddTaskFast(this);
                    return false;
                }
            }
            else {
                throw "不支持的模块编号:" + fIndex;
            }
            //lastFuncIndex = fIndex;
            //funcIndexs.push(fIndex);
            //if (funcIndexs.length > 50) {
            //    funcIndexs.shift();
            //}
        }
        if (thisReader.EOF) {
            // 任务完毕，清空数据
            //this.__RemoveTask(tasks, this);
            if (this.AllowClip) {
                if (this.SaveCount > 0) {
                    while (this.SaveCount > 0) {
                        this.ctx.restore();
                        this.SaveCount--;
                    }
                }
                this.ctx.restore();
            }
            if (this.DoubleBuffer == true && this.TempElementForDoubleBuffer != null) {
                // 提交缓冲的结果
                var ctx2 = this.CanvasElement.getContext("2d");
                var x1 = this.DoubleBufferBounds[0];
                var y1 = this.DoubleBufferBounds[1];
                var w1 = this.DoubleBufferBounds[2];
                var h1 = this.DoubleBufferBounds[3];
                this.DoubleBufferBounds = null;
                ctx2.imageSmoothingEnabled = false;
                ctx2.clearRect(x1, y1, w1, h1);
                //ctx2.strokeStyle = "blue";
                //ctx2.setLineDash([6, 6]);
                //ctx2.strokeRect(x1, y1, w1, h1);
                ctx2.drawImage(
                    this.TempElementForDoubleBuffer,
                    x1, y1, w1, h1,
                    x1, y1, w1, h1);
                this.TempElementForDoubleBuffer.remove();
                this.TempElementForDoubleBuffer = null;
            }
            if (typeof (this.EventAfterDraw) == "function") {
                // 触发事件
                var func = this.EventAfterDraw;
                this.EventAfterDraw = null;
                func.call(this, this);
            }
            this.CanvasElement = null;
            this.Reader = null;
            this.ctx = null;
            this.ImageTable = null;
            this.BrushTable = null;
            this.ColorTable = null;
            this.FontTable = null;
            return true;
        }
        else {
            return false;
        }
    }
};

export let DCSVGDrawer = {
    /**
     * 绘制SVG图形
     * @param {HTMLOrSVGElement} svgElement SVG元素
     * @param { DCBinaryReader} reader 数据读取器
     */
    DrawSVG: function (svgElement, reader) {
        if (svgElement == null || svgElement.nodeName != "SVG") {
            throw "没有指定SVG元素";
        }
        if (reader == null) {
            return;
        }
        var doc = svgElement.ownerDocument;
        const svgNS = "http://www.w3.org/2000/svg";
        var vCurrentFont = null;
        var fontTable = new Array();
        var vCurrentBrush = null;
        var brushTable = new Array();
        var vCurrentPen = null;
        var penTable = new Array();
        while (reader.EOF == false) {
            var fIndex = reader.ReadByte();
            switch (fIndex) {
                case 32: break;
                case 1: // SetCurrentFont
                    vCurrentFont = fontTable[reader.ReadInt16()];
                    break;
                case 2: // SetCurrentBrush
                    vCurrentBrush = brushTable[reader.ReadInt16()];
                    break;
                case 3:// SetCurrentPen
                    vCurrentPen = penTable[reader.ReadInt16()];
                    break;
                case 5:// DrawLine
                    {
                        var pen = penTable[reader.ReadInt16()];
                        var line = doc.createElementNS(svgNS, "line");
                        line.setAttribute("x1", reader.ReadInt32());
                        line.setAttribute("y1", reader.ReadInt32());
                        line.setAttribute("x2", reader.ReadInt32());
                        line.setAttribute("y2", reader.ReadInt32());
                        svgElement.appendChild(line);
                    }
                    break;
            }
        }
    }
};