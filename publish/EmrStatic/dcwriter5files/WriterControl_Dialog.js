//*************************************************************************
//* 项目名称：
//* 当前版本: V 5.3.1
//* 开始时间: 20230601
//* 开发者:
//* 重要描述:
//*************************************************************************
//* 最后更新时间:2023-8-10 10:59:25
//* 最后更新人:xym
//*************************************************************************

"use strict";
import { DCTools20221228 } from "./DCTools20221228.js";
import WriteControlDialogStaticResources from "./WriterControl_DialogStaticResources.js";
var {
    DASHSTYLE,
    LBSJ,
    DIALOGSTYLE,
    SPECIALCHARACTERS,
    ROMANCHARACTERS,
    NUMERICCHARACTERS,
    MEDICALCHARACTERS,
    NAMELIST,
    IDTYPELIST,
    IDLIST,
    PERMANENTTEETHTOP,
    PERMANENTTEETHBOTTOM,
    TEETHPOSTIONTOPOBJ,
    TEETHPOSTIONBOTTOMOBJ,
    BULLETEDLIST,
    MOCKARRAY,
    DATAFONTSIZE,
    TEETHBUTTONLIST,
    DCEXECUTECOMMANDDEFAULTOPTIONS,
    MEDICALCHARACTERSIMGOBJECT,
    IMGEDITSVGOBJECT,
    INPUTFIELDPROPERTYEXPRESSIONSARRAY,
    RADIOCHECKPROPERTYEXPRESSIONSARRAY,
} = WriteControlDialogStaticResources;

export let WriterControl_Dialog = {
    /**
     * 弹出框样式字符串
     */
    InsertSpecifyCharacterObj: {
        SpecialCharacters: SPECIALCHARACTERS,
        RomanCharacters: ROMANCHARACTERS,
        NumericCharacters: NUMERICCHARACTERS,
        MedicalCharacters: MEDICALCHARACTERS,
    },

    /**
     * 显示对话框
     * @param {string} strContainerID 编辑器容器元素编号
     * @param {string} strDialogName 对话框的名称
     * @param {any} options 参数
     */
    ShowDialog(strContainerID, strDialogName, options) {
        var rootElement =
            typeof strContainerID == "string"
                ? DCTools20221228.GetOwnerWriterControl(strContainerID)
                : strContainerID;
        if (rootElement != null) {
            // 这里的对话框名称定义在C#类型 DCSoft.WASMDialogName 中
            switch (strDialogName) {
                case "EditComment":
                    // CommentEditableWhenReadonly（只读模式下：true允许编辑|false不允许编辑）
                    // DoubleClickToEditComment = true; 允许双击 | false不允许双击
                    // console.log(
                    //     rootElement.DocumentOptions.BehaviorOptions
                    //         .CommentEditableWhenReadonly,
                    //     "CommentEditableWhenReadonly"
                    // );
                    // console.log(
                    //     rootElement.DocumentOptions.BehaviorOptions
                    //         .DoubleClickToEditComment,
                    //     "DoubleClickToEditComment"
                    // );
                    // console.log(rootElement.Readonly, "Readonly");
                    if (
                        rootElement.DocumentOptions.BehaviorOptions.DoubleClickToEditComment
                    ) {
                        if (
                            !rootElement.Readonly ||
                            (rootElement.Readonly &&
                                rootElement.DocumentOptions.BehaviorOptions
                                    .CommentEditableWhenReadonly)
                        ) {
                            // 修改批注
                            var currentComment = rootElement.GetCurrentComment();
                            if (currentComment) {
                                //判断双击的是校验，则不展示批注对话宽
                                if (currentComment.IsInternal === "True") {
                                    return;
                                }
                                WriterControl_Dialog.EditDocumentCommentsDialog(options, rootElement);
                            }
                        }
                    }
                    break;
            }
        }
    },

    /**
     * 插入绑定数据源元素
     * @param appendNode 把绑定数据源元素插入到该元素位置最后面
     * @param ctl 编辑器元素
     */
    appendValueBindingDiv: function (appendNode) {
        if (!(appendNode instanceof jQuery)) {
            return false;
        }
        var ValueBindingHtml = `
        <div class="dcBody-content">
            <div id="dc_ValueBinding_Box" class="dc_Box">
                <h6 class="dc_title">数据源信息</h6>
                <div class="dcBody-content">
                    <label>
                        <input type="checkbox" name="Enabled" data-text="ValueBinding.Enabled" checked="checked">
                        <span class="dcTitle-text">数据源绑定设置有效</span>
                    </label>
                </div>
                <form>
                    <div class="dcBody-content">
                        <label class="dc_flex">
                            <span class="dcTitle-text">数据源：</span>
                            <input type="text" class="dc_full" name="DataSource" data-text="ValueBinding.DataSource">
                        </label>
                    </div>
                    <div class="dcBody-content">
                        <label class="dc_flex">
                            <span class="dcTitle-text">格式化：</span>
                            <input type="text" class="dc_full" name="FormatString" data-text="ValueBinding.FormatString">
                        </label>
                    </div>
                    <div class="dcBody-content">
                        <label class="dc_flex">
                            <span class="dcTitle-text">绑定路径：</span>
                            <input type="text" class="dc_full" name="BindingPath" data-text="ValueBinding.BindingPath">
                        </label>
                    </div>
                    <div class="dcBody-content">
                        <label class="dc_blockelement">
                            <span class="dcTitle-text">Text的绑定路径(仅适用于输入域元素)：</span>
                            <input type="text" class="fullWidth" name="BindingPathForText" data-text="ValueBinding.BindingPathForText">
                        </label>
                    </div>
                    <div class="dcBody-content">
                        <label>
                            <input type="checkbox" name="Readonly" data-text="ValueBinding.Readonly">
                            <span class="dcTitle-text">只读，不能回填</span>
                        </label>
                    </div>
                    <div class="dcBody-content">
                        <label>
                            <input type="checkbox" name="AutoUpdate" data-text="ValueBinding.AutoUpdate">
                            <span class="dcTitle-text">自动更新，当加载文档或数据源发生改变时自动更新数值</span>
                        </label>
                    </div>
                    <div class="dcBody-content">
                        <label class="dc_flex">
                            <span class="dcTitle-text">执行状态：</span>
                            <select id="dc_ProcessState" data-text="ValueBinding.ProcessState" class="dc_full">
                                <option value="Always">总是执行</option>
                                <option value="Once">只执行一次</option>
                                <option value="Never">不执行</option>
                            </select>
                        </label>
                    </div>
                </form>
            </div>
        </div>`;
        var ValueBindingDiv = jQuery(ValueBindingHtml);
        var dc_ValueBinding_Box = ValueBindingDiv.find("#dc_ValueBinding_Box");
        var EnabledCheckbox = dc_ValueBinding_Box.find(
            '[data-text="ValueBinding.Enabled"]'
        );
        let that = this;
        // 绑定复选框修改事件
        EnabledCheckbox.off("change");
        EnabledCheckbox.change(function () {
            var formNode = dc_ValueBinding_Box.find("form")[0];
            that.changeFormDisable(formNode, !this.checked);
        });
        appendNode.append(ValueBindingDiv);
    },

    /**
     * 创建视频文件的弹窗
     * @param {any} options
     * @param {any} ctl
     */
    MediaDialog: function (options, ctl, ele) {
        //var ele = null;
        if (!options || typeof options != "object") {
            ele = ctl.CurrentElement("xtextmediaelement");
            if (ele == null) {
                options = {};
            } else {
                options = ctl.GetElementProperties(ele);
            }
        }
        var MediaHtml = `
               <div class="dc_Box">
                <h6 class="dc_title">属性</h6>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">编号：</span>
                        <input type="text" class="dc_full" name="ID" data-text="ID"></input>
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">宽度：</span>
                        <input type="number" class="dc_full" name="Width" data-text="Width"></input>
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">高度：</span>
                        <input type="number" class="dc_full" name="Height" data-text="Height"></input>
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">地址：</span>
                        <input type="text" class="dc_full" name="FileName" data-text="FileName"></input>
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">打印显示：</span>
                        <select name="PrintVisibility" data-text="PrintVisibility">
                            <option value="None">不显示</option>
                            <option value="Visible">显示</option>
                        </select>
                    </label>
                </div>
            </div>
        `;
        var dialogOptions = {
            title: "音视频元素",
            bodyHeight: 240,
            bodyClass: "MediaElement",
            bodyHtml: MediaHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        // WriterControl_Dialog.appendValueBindingDiv(dcPanelBody);
        var allDataText = $(dcPanelBody).find("[data-text]");
        for (var i = 0; i < allDataText.length; i++) {
            var thisEle = allDataText[i];
            if (thisEle.nodeName == "INPUT") {
                thisEle.value = options[thisEle.getAttribute("data-text")];
            } else if (thisEle.nodeName == "SELECT") {
                var selectValue = options[thisEle.getAttribute("data-text")];
                selectValue = selectValue ? selectValue : "None";
                jQuery(thisEle)
                    .find("option[value=" + selectValue + "]")
                    .attr("selected", true);
            }
        }

        function successFun() {
            var opt = {};
            for (var i = 0; i < allDataText.length; i++) {
                opt[allDataText[i].getAttribute("data-text")] = allDataText[i].value;
            }
            //获取到需要的元素
            // console.log("successFun -> _data", _data)
            ctl.SetElementProperties(ele, opt);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentElement("xtextmediaelement"));
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 创建图片属性对话框
     * @param options 图片属性
     * @param ctl 编辑器元素
     */
    ImageDialog: function (options, ctl, ele) {
        //var ele = null;
        if (!options || typeof options != "object") {
            // 当未传入值时
            ele = ctl.CurrentElement("xtextimageelement");
            if (ele == null) {
                return false;
            }
            options = ctl.GetElementProperties(ele);
        }
        options["KeepWidthHeightRate"] = options["KeepWidthHeightRate"] === true ? "true" : "false";
        options["SmoothZoom"] = options["SmoothZoom"] === true ? "true" : "false";
        options["EnableEditImageAdditionShape"] = options["EnableEditImageAdditionShape"] === true ? "true" : "false";
        console.log(options, '========options');
        var ImageHtml = `
            <div class="dc_Box">
                <h6 class="dc_title">属性</h6>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">编号：</span>
                        <input type="text" class="dc_full" name="ID" data-text="ID">
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">宽度：</span>
                        <input type="number" class="dc_full" name="Width" data-text="Width">
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">高度：</span>
                        <input type="number" class="dc_full" name="Height" data-text="Height">
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">允许编辑：</span>
                        <select  class="dc_full" name="EnableEditImageAdditionShape" data-text="EnableEditImageAdditionShape">
                            <option value="true">是</option>
                            <option value="false">否</option>
                        </select>
                    </label>
                </div>
                  <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">保持长宽比<span class="dc_SpecifyWidth_title" title="当设置了保持长宽比，并修改了图片的宽高时，长宽比会根据宽高中的较大值进行缩放调整。若宽高相等，则优先以高度为缩放标准。">?</span>：</span>
                        <select  class="dc_full" name="KeepWidthHeightRate" data-text="KeepWidthHeightRate">
                            <option value="true">是</option>
                            <option value="false">否</option>
                        </select>
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">平滑缩放：</span>
                        <select  class="dc_full" name="SmoothZoom" data-text="SmoothZoom">
                            <option value="true">是</option>
                            <option value="false">否</option>
                        </select>
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">可见性表达式：</span>
                       <input data-text="VisibleExpression" type="text">
                        <button class="dc_visible_expression">示例</button>
                    </label>
                </div>
                <div id="VisibleExpressionBox"></div>
            </div>
            <div class="dc_Box">
                <h6 class="dc_title">样式属性</h6>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">布局方式：</span>
                       <select id="dc_ZOrderStyle" class="dc_full" name="ZOrderStyle" data-text="ZOrderStyle">
                            <option value="Normal">普通</option>
                            <option value="UnderText">文本下方</option>
                            <option value="InFrontOfText">文本上方</option>
                        </select>
                    </label>
                </div>
                 <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">X偏移量:</span>
                       <input  class="dc_full dc_disabled_ZOrderStyle" type="number" data-text="OffsetX">
                    </label>
                </div>
                 <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">Y偏移量:</span>
                       <input class="dc_full dc_disabled_ZOrderStyle" type="number" data-text="OffsetY" >
                    </label>
                </div>
                 <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">透明色:</span>
                       <input class="dc_full " data-text="TransparentColorValue" type="text">
                    </label>
                </div>
                <div class="dcBody-content">
                    <label class="dc_flex">
                        <span class="dcTitle-text">垂直对齐方式:</span>
                       <select  class="dc_full " name="VAlign" data-text="VAlign">
                            <option value="Top">上</option>
                            <option value="Middle">中</option>
                            <option value="Bottom">下</option>
                        </select>
                    </label>
                </div>
            </div>
            <div class="dc_Box">
                <h6 class="dc_title">图片内容</h6>
                <div id="dc_image_box" class="dcBody-content">
                    <button id="dc_changeImage" onclick="this.querySelector('input').click()">
                        <span>修改图片</span>
                        <input type="file"  accept="image/*">
                    </button>
                    <div class="imgDiv">
                        <input type="hidden" data-text="Src" data-value="img">
                        <img src="" alt="" id="dc_SrcImg">
                    </div>
                </div>
            </div>
        `;
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var dialogOptions = {
            title: "图片元素",
            bodyHeight: 475,
            bodyClass: "ImageElement",
            bodyHtml: ImageHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        // console.log(this.visibleexpressionDialog(ctl, 'VisibleExpressionBox'))
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        // WriterControl_Dialog.appendValueBindingDiv(dcPanelBody);
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        changeDisabledZOrderStyle(options.ZOrderStyle);

        GetOrChangeData(dcPanelBody, opts);
        // 图片的默认赋值
        dcPanelBody.find("[data-value='img']").each(function () {
            var _val = this.value.replace(/[\n\r]/g, "").replace(/ /g, "");
            if (_val) {
                var str = _val;
                if (_val.indexOf("base64,") == -1) {
                    str = "data:image/png;base64," + str;
                    // jQuery(this).val(str);
                }
                jQuery(this).siblings("img#dc_SrcImg").attr("src", str);
            }
        });
        dcPanelBody.find("#dc_changeImage input").change(function () {
            var files = this.files;
            if (files.length == 0) {
                return;
            }
            var dc_image_box = jQuery(this).parents("#dc_image_box:first");
            var imgNode = dc_image_box.find("img#dc_SrcImg");
            var imgInputNode = dc_image_box.find("[data-value=img]");
            if (files[0] && files[0].type.slice(0, 5) == "image") {
                var fileinfo = files[0];
                var reader = new FileReader();
                reader.readAsDataURL(fileinfo);
                reader.onload = function () {
                    var base64 = reader.result;
                    imgNode.attr("src", base64);
                    imgInputNode.val(base64);
                    // imgNode.show();
                    // var str = base64.substr(base64.indexOf("base64,") + 7, base64.length);
                    // btnNode.val(str);
                };
                reader.onerror = function (error) {
                    console.log(error);
                };
            }
        });
        jQuery(ctl).find('#dc_ZOrderStyle').change(function () {
            var selectedValue = $(this).val();
            changeDisabledZOrderStyle(selectedValue);
        });
        //设置样式是否禁用
        function changeDisabledZOrderStyle(selectedValue) {
            if (selectedValue) {
                var dc_disabled_ZOrderStyle = ctl.ownerDocument.querySelectorAll('.dc_disabled_ZOrderStyle');
                for (var i = 0; i < dc_disabled_ZOrderStyle.length; i++) {
                    var item = dc_disabled_ZOrderStyle[i];
                    if (selectedValue === 'Normal') {
                        item.setAttribute('disabled', true);
                    } else {
                        item.removeAttribute('disabled');
                    }
                }
            }

        }
        function getDefaultOptions() {
            var _data = GetOrChangeData(dcPanelBody);
            _data["KeepWidthHeightRate"] = _data["KeepWidthHeightRate"] === "true";
            _data["SmoothZoom"] = _data["SmoothZoom"] === "true";
            _data["EnableEditImageAdditionShape"] = _data["EnableEditImageAdditionShape"] === "true";
            return _data;
        }
        var oldoptions = getDefaultOptions();

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            _data["KeepWidthHeightRate"] = _data["KeepWidthHeightRate"] === "true";
            _data["SmoothZoom"] = _data["SmoothZoom"] === "true";
            _data["EnableEditImageAdditionShape"] = _data["EnableEditImageAdditionShape"] === "true";
            //判断后端是否需要刷新
            if (JSON.stringify(oldoptions) !== JSON.stringify(_data)) {
                _data['refreshparent'] = true;
            } else {
                _data['refreshparent'] = false;
            }

            ctl.SetElementProperties(ele, _data);
            console.log("successFun -> _data", _data);

            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentElement("xtextimageelement"));
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }

    },

    /**
     * 创建水印对话框
     * @param options 水印属性
     * @param ctl 编辑器元素
     */
    WatermarkDialog: function (options, ctl) {
        if (!options || typeof options != "object") {
            // 当未传入值时，获取当前的水印数据
            options = ctl.GetDocumentWatermark();
        }
        var watermarkHtml = `
            <div class="dcBody-content">
                <span class="dcTitle-text">倾斜角度:</span>
                <input type="number" class="dcTitle-text-input-number" value="0"  name="angle" min="0" max="360"/>
                <span class="dc_gap-width"></span>
                <span class="dcTitle-text">透明度:</span>
                <input type="number" value="0"  class="dcTitle-text-input-number"  name="alpha" min="0" max="100"/>
            </div>
            <div class="dcBody-content">
                <input type="checkbox" id="dc_repeatCheckbox" name="repeat"/>
                    <label for="dc_repeatCheckbox" class="dcTitle-text">重复平铺，密度(0到1之间):</span>
                <input type="number" value="0"  class="dcTitle-text-input-number"  name="densityforrepeat" min="0" max="1" step="0.1"/>
            </div>
            <div class="dcBody-content">
                <input type="radio" name="type" id="dc_noWaterType" value="None" checked>
                <label for="dc_noWaterType">无水印</label>
            </div>
            <div class="dcBody-content">
                <input type="radio" name="type" id="dc_imgWaterType" value="Image">
                <label for="dc_imgWaterType">图片水印</label>
              <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <span class="dc_imgWaterType_wring">注：图片水印暂不支持设置透明度</span>
                <div class="dcBody-content">
                    <span class="dc_gap-width"></span>
                    <input type="hidden" id="dc_imagedatabase64string" name="imagedatabase64string" value="选择文件" disabled/>
                    <input type="file" id="WatermarkSrcImgButton" disabled accept="image/*">
                     <img src="" alt="" id="WatermarkSrcImg">
                </div>
            </div>
            <div class="dcBody-content">
                <input type="radio" name="type" id="textWaterType" value="Text">
                <label for="textWaterType">文字水印</label>
                <div class="dcBody-content">
                    <div class="dc_textWaterType_label_box" >
                        <span class="dc_gap-width"></span>
                        <span class="dcTitle-text">文字：</span>
                        <input type="text"  name="text" disabled>
                    </div>
                    <div class="dc_textWaterType_label_box_font">
                        <span class="dc_gap-width"></span>
                        <span class="dcTitle-text">字体：</span>
                        <input type="text"  name="font" disabled>
                    </div>
                    <div class="dc_textWaterType_label_box_color">
                        <span class="dc_gap-width"></span>
                        <span class="dcTitle-text">颜色：</span>
                        <input type="color"  name="colorvalue" value="#000000" disabled>
                    </div>
                </div>
            </div>
        `;
        var dialogOptions = {
            title: "水印",
            bodyHeight: 326,
            bodyClass: "Watermark",
            bodyHtml: watermarkHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        // 进行水印类型的切换
        dcPanelBody.find("input[type=radio][name=type]").on("change", function (e) {
            //先将文本和图片的禁用，再根据点击开启
            dcPanelBody
                .find("input[type=radio]")
                .nextAll(".dcBody-content")
                .find("input,select")
                .attr("disabled", "disabled");
            jQuery(this)
                .nextAll(".dcBody-content")
                .find("input,select")
                .removeAttr("disabled");
        });
        //图片上传，对文件按钮进行判断
        dcPanelBody.find("input[type=file]").on("change", function (e) {
            var reader = new FileReader();
            var file = e.target.files[0];
            if (file) {
                reader.readAsDataURL(file);
                reader.onloadend = function () {
                    //console.log(reader.result);
                    dcPanelBody.find("#dc_imagedatabase64string").val(reader.result);
                    dcPanelBody.find("img#WatermarkSrcImg").attr("src", reader.result);
                };
            }
        });

        //开始对对话框赋值
        if (options != null && typeof options == "object") {
            // console.log("处理前的水印数据==>", options)
            options = WriterControl_Dialog.checkWaterValue(options);
            // console.log("当前的水印数据==>", options)
            for (var item in options) {
                var _value = options[item];
                var _input = dcPanelBody.find("[name='" + item + "']");
                var _type = _input.attr("type");
                if (_type == "checkbox") {
                    if (typeof _value == "boolean") {
                        _input.prop("checked", _value);
                    }
                } else if (_type == "radio") {
                    if (typeof _value == "string") {
                        _input.filter("[value=" + _value + "]").click();
                    }
                } else {
                    _input.val(_value);
                }
            }
        }

        if (options.type && options.type === "Image") {
            if (
                options.imagedatabase64string &&
                options.imagedatabase64string.length
            ) {
                let _val = options.imagedatabase64string;
                let str = "";
                if (_val.indexOf("base64,") == -1) {
                    str = "data:image/png;base64," + options.imagedatabase64string;
                }
                dcPanelBody.find("img#WatermarkSrcImg").attr("src", str);
            }
        }

        //成功的回调函数
        function successFun() {
            //获取到所有的属性
            var opt = {
                type: "None", //类型
                densityforrepeat: "", //水印间隔,0-1之间,允许小数,需要是number
                repeat: "", //是否重复
                alpha: "", //透明度,可能是0-255,需要是number
                // "backcolorvalue": "",//颜色值
                colorvalue: "", //字体颜色值
                angle: "", //倾斜角度，0-360之间，允许小数,需要是number
                imagedatabase64string: "", //图片
                text: "", //文本内容
                font: "", //字体样式 微软雅黑, 10.5, style=Bold, Italic, Underline, Strikeout
                // "fontname": "",//字体名称
                // "fontsize": ""//字体大小,需要是number
            };
            for (var item in opt) {
                //找到对应的元素
                var _input = dcPanelBody.find("[name='" + item + "']");
                var _type = _input.attr("type");
                if (_type == "checkbox") {
                    opt[item] = _input.prop("checked");
                } else if (_type == "radio") {
                    opt[item] = _input.filter(":checked").val();
                } else {
                    opt[item] = _input.val();
                }
            }
            ctl.SetDocumentWatermark(opt);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetDocumentWatermark();
                ctl.EventDialogChangeProperties(changedOptions);
            };
            return options;
        }
    },

    /**
     * 处理水印数据
     * @param gridLineInfo 水印属性
     * @return 处理完成的水印数据
     */
    checkWaterValue: function (gridLineInfo) {
        if (!gridLineInfo || typeof gridLineInfo != "object") {
            return null;
        }
        var opt = {
            type: "None", //类型
            densityforrepeat: "", //水印间隔,0-1之间,允许小数,需要是number
            repeat: "", //是否重复
            alpha: "", //透明度,可能是0-255,需要是number
            // "backcolorvalue": "",//颜色值
            colorvalue: "", //字体颜色值
            angle: "", //倾斜角度，0-360之间，允许小数,需要是number
            imagedatabase64string: "", //图片
            text: "", //文本内容
            font: "", //字体样式 微软雅黑, 10.5, style=Bold, Italic, Underline, Strikeout
            // "fontname": "",//字体名称
            // "fontsize": ""//字体大小,需要是number
        };
        for (var item in gridLineInfo) {
            var lower_item = item.toLowerCase(); //转换为小写字母
            if (Object.hasOwnProperty.call(opt, lower_item)) {
                var _value = gridLineInfo[item]; //传入对象的内容
                if (_value == null) {
                    _value = "";
                }
                switch (lower_item) {
                    case "type":
                        _value += "";
                        _value = _value.toLowerCase();
                        switch (_value) {
                            case "1":
                            case "image":
                                opt[lower_item] = "Image";
                                break;
                            case "2":
                            case "text":
                                opt[lower_item] = "Text";
                                break;
                            default:
                                opt[lower_item] = "None";
                                break;
                        }
                        break;
                    case "densityforrepeat":
                    case "alpha":
                    case "angle":
                        // case 'fontsize':
                        //确保是数值
                        _value = Number(_value);
                        if (typeof _value === "number" && isNaN(_value)) {
                            // 是NaN
                            _value = 0;
                        }
                        opt[lower_item] = _value;
                        break;
                    case "backcolorvalue":
                    case "colorvalue":
                        //判断是否为颜色
                        if (_value) {
                            var reg_str = "";
                            if (/^rgb\(/.test(_value)) {
                                reg_str =
                                    "^[rR][gG][Bb][(]([\\s]*(2[0-4][0-9]|25[0-5]|[01]?[0-9][0-9]?)[\\s]*,){2}[\\s]*(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)[\\s]*[)]{1}$";
                            } else if (/^rgba\(/.test(_value)) {
                                reg_str =
                                    "^[rR][gG][Bb][Aa][(]([\\s]*(2[0-4][0-9]|25[0-5]|[01]?[0-9][0-9]?)[\\s]*,){3}[\\s]*(1|1.0|0|0.[0-9])[\\s]*[)]{1}$";
                            } else if (/^#/.test(_value)) {
                                reg_str = "^#([0-9a-fA-F]{6}|[0-9a-fA-F]{3})$";
                            } else if (/^hsl\(/.test(_value)) {
                                reg_str =
                                    "^[hH][Ss][Ll][(]([\\s]*(2[0-9][0-9]|360｜3[0-5][0-9]|[01]?[0-9][0-9]?)[\\s]*,)([\\s]*((100|[0-9][0-9]?)%|0)[\\s]*,)([\\s]*((100|[0-9][0-9]?)%|0)[\\s]*)[)]$";
                            } else if (/^hsla\(/.test(_value)) {
                                reg_str =
                                    "^[hH][Ss][Ll][Aa][(]([\\s]*(2[0-9][0-9]|360｜3[0-5][0-9]|[01]?[0-9][0-9]?)[\\s]*,)([\\s]*((100|[0-9][0-9]?)%|0)[\\s]*,){2}([\\s]*(1|1.0|0|0.[0-9])[\\s]*)[)]$";
                            }
                            var re = new RegExp(reg_str);
                            if (_value.match(re) == null) {
                                _value = "#000000";
                            }
                        } else {
                            _value = "#000000";
                        }
                        opt[lower_item] = _value;
                        break;
                    case "repeat":
                        //确保是boolean值
                        if (typeof _value != "boolean") {
                            _value += "";
                            _value = _value.toLowerCase();
                            if (_value == "true") {
                                _value = true;
                            } else {
                                _value = false;
                            }
                        }
                        opt[lower_item] = _value;
                        break;
                    case "text":
                    // case 'fontname':
                    case "font":
                    case "imagedatabase64string":
                        //只要是纯文本就行
                        _value += "";
                        var BASE64_MARKER = ";base64,"; //声明文件流编码格式
                        if (
                            lower_item == "imagedatabase64string" &&
                            _value.indexOf(BASE64_MARKER) > -1
                        ) {
                            var base64Index =
                                _value.indexOf(BASE64_MARKER) + BASE64_MARKER.length;
                            _value = _value.substring(base64Index);
                        }
                        opt[lower_item] = _value;
                        break;
                    default:
                        break;
                }
            }
        }
        return opt;
    },

    /**
     * 创建页面设置对话框
     * @param options 页面设置属性
     * @param ctl 编辑器元素
     */
    DocumentSettingsDialog: function (options, ctl, callBack) {
        if (!options || typeof options != "object") {
            // 当未传入值时，获取当前的页面设置数据
            options = ctl.GetDocumentPageSettings();
        }
        var DocumentSttingsHtml = `
       <div>
    <div class="dc_setting-section">
        <div class="dc_setting-left">
            <span class="dc_setting-name">页面方向:</span>
        </div>
        <div class="dc_setting-right">
            <div class="dc_radioBtn">
                <div class="dc_btnSelect"></div>
                <span>横向</span>
                <div class="dc_selected dc_select-left" style="display: none">
                </div>
            </div>
            <div class="dc_radioBtn endwise" >
                <div class="dc_btnSelect"></div>
                <span>纵向</span>
                <div class="dc_selected dc_select-right" style="display: block">
                </div>
            </div>
        </div>
    </div>
    <div class="dc_setting-section">
        <div class="dc_setting-left">
            <span class="dc_setting-name">页面大小:</span>
        </div>
        <div class="dc_setting-right">
            <select class="dc_select-button"></select>
        </div>
    </div>
    <label class="dc_document_setting_label" >
        <input type="checkbox"  id="DCEnableHeaderFooter"></input>
        <span>启用页眉页脚功能</span>
    </label>
    <div id="DCEnableHeaderFooterControl" class="dc_Box" >
        <div>
            <div>
                <label class="DCEnableHeaderFooterControl_label_DCHeaderFooterDifferentFirstPage" >
                   <input type="checkbox"  id="DCHeaderFooterDifferentFirstPage"></input>
                    <span>首页页眉页脚不同:</span>
                </label>
                 <br />
                <label class="DCEnableHeaderFooterControl_label_DCPageIndexsForHideHeaderFooter">
                    <span>隐藏页眉页脚的页码:</span>
                    <input  id="DCPageIndexsForHideHeaderFooter"></input>
                </label>

                <label class="DCEnableHeaderFooterControl_label_HeaderDistance">
                    <span>页眉高度:</span>
                    <input class="dc_input_number_data_model" type="number" data-text="HeaderDistance" data="HeaderDistance" value="">
                    <span dc-text-model="HeaderDistance"></span>
                </label>
                <label class="DCEnableHeaderFooterControl_label_FooterDistance">
                    <span>页脚高度:</span>
                    <input type="number" data="FooterDistance" data-text="FooterDistance" class="dc_input_number_data_model" value="">
                    <span dc-text-model="FooterDistance"></span>
                </label>
            </div>
        </div>
    </div>

   
    <div class="dc_Box" >
        <h6 class="dc_title">页面尺寸(厘米)</h6>
        <div class="dc_DocumentSettings_Box_content">
            <label>
                <span>高:</span>
                <input type="number" data="PaperHeightInCM" value="" >
            </label>
            <label >
                <span>宽:</span>
                <input type="number" data="PaperWidthInCM" value="">
            </label>
        </div>
    </div>

    <div class="dc_Box" >
        <h6 class="dc_title">页边距(厘米)</h6>
        <div class="dc_DocumentSettings_Box_content">
            <div >
                <label>
                    <span>上边距:</span>
                    <input type="number" data="TopMarginInCM"  />
                </label>
                <label>
                    <span>下边距:</span>
                    <input type="number" data="BottomMarginInCM"  />
                </label>
                <label >
                    <span>左边距:</span>
                    <input type="number" data="LeftMarginInCM"  />
                </label>
                <label >
                    <span>右边距:</span>
                    <input type="number" data="RightMarginInCM"  />
                </label>
            </div>
        </div>
    </div>
</div>
        `;
        var dialogOptions = {
            title: "页面设置",
            bodyHeight: 320,
            bodyClass: "DocumentSettings",
            bodyHtml: DocumentSttingsHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions, callBack);
        var selectList = [
            {
                name: "A3",
                widthmm: 29.6926,
                heightmm: 42.0116,
                pageName: 8,
            },
            {
                name: "A4",
                widthmm: 21.0058,
                heightmm: 29.6926,
                pageName: 9,
            },
            {
                name: "A5",
                widthmm: 14.8082,
                heightmm: 21.0058,
                pageName: 11,
            },
            {
                name: "B4",
                widthmm: 24.9936,
                heightmm: 35.306,
                pageName: 12,
            },
            {
                name: "B5",
                widthmm: 17.6022,
                heightmm: 24.9936,
                pageName: 13,
            },
            {
                name: "16K(自定义)",
                widthmm: 19.5,
                heightmm: 27,
                pageName: 14,
            },
            {
                name: "Prc16K",
                widthmm: 14.61,
                heightmm: 21.49,
                pageName: 15,
            },
            {
                name: "Prc16KRotated",
                widthmm: 14.61,
                heightmm: 21.49,
                pageName: 16,
            },
            {
                name: "Custom",
                widthmm: "",
                heightmm: "",
                pageName: 0,
            },
        ];
        // console.log(selectList)
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var PaperKind_node = dcPanelBody.find(".dc_select-button"); //页面大小
        var PaperHeight_node = dcPanelBody.find("input[data=PaperHeightInCM]"); //页面高度
        var PaperWidth_node = dcPanelBody.find("input[data=PaperWidthInCM]"); //页面宽度

        // 填充下拉
        for (var i in selectList) {
            var name = selectList[i].name;
            var widthMm = selectList[i].widthmm;
            var heightMm = selectList[i].heightmm;
            PaperKind_node.append(
                `<option value="${name}" dataWidth="${widthMm}" dataHeight="${heightMm}">${name}</option>`
            );
        }
        // 横向纵向的点击事件
        dcPanelBody.find(".dc_radioBtn").on("click", function () {
            jQuery(this).find(".dc_selected").show();
            jQuery(this).siblings(".dc_radioBtn").find(".dc_selected").hide();
        });
        // 页面大小的切换事件
        PaperKind_node.on("change", function () {
            var _option = jQuery(this).find("option:selected");
            if (_option.attr("dataWidth")) {
                PaperWidth_node.val(_option.attr("dataWidth"));
            }
            if (_option.attr("dataHeight")) {
                PaperHeight_node.val(_option.attr("dataHeight"));
            }
            if (_option.val() != "Custom") {
                PaperWidth_node.attr("disabled", "disabled");
                PaperHeight_node.attr("disabled", "disabled");
            } else {
                PaperWidth_node.removeAttr("disabled");
                PaperHeight_node.removeAttr("disabled");
            }
        });
        options = {
            ...options,
            PaperHeightInCM: options.PaperHeightInCM.toFixed(4),
            PaperWidthInCM: options.PaperWidthInCM.toFixed(4),
            TopMarginInCM: options.TopMarginInCM.toFixed(2),
            BottomMarginInCM: options.BottomMarginInCM.toFixed(2),
            LeftMarginInCM: options.LeftMarginInCM.toFixed(2),
            RightMarginInCM: options.RightMarginInCM.toFixed(2),
        };
        console.log("当前页面设置==>", options);
        jQuery(ctl).find('#DCHeaderFooterDifferentFirstPage').attr("checked", options.HeaderFooterDifferentFirstPage);//首页页眉页脚不同
        jQuery(ctl).find('#DCPageIndexsForHideHeaderFooter').val(options.PageIndexsForHideHeaderFooter);//隐藏页眉页脚的页码
        PageSettingsData(options);
        function PageSettingsData(data) {
            var isChange = typeof data == "object"; //是否是修改数据
            var obj = {};
            // 横向纵向
            var flag = dcPanelBody.find(".dc_select-right").is(":hidden"); //true 横向 | false 纵向
            obj.Landscape = flag;
            if (isChange) {
                if (data.Landscape == true) {
                    // 横向
                    dcPanelBody.find(".dc_select-left").show();
                    dcPanelBody.find(".dc_select-right").hide();
                } else {
                    dcPanelBody.find(".dc_select-left").hide();
                    dcPanelBody.find(".dc_select-right").show();
                }
            }
            // 页面大小
            obj.PaperKind = PaperKind_node.val();
            if (isChange && Object.hasOwnProperty.call(data, "PaperKind")) {
                let PaperKindName = "";
                if (data.PaperKind || data.PaperKind === 0) {
                    selectList.map((item) => {
                        if (
                            item.pageName == data.PaperKind ||
                            item.name == data.PaperKind
                        ) {
                            PaperKindName = item.name;
                        }
                    });
                }
                PaperKind_node.val(PaperKindName);
                if (data.PaperKind != "Custom") {
                    PaperWidth_node.attr("disabled", "disabled");
                    PaperHeight_node.attr("disabled", "disabled");
                }
            }
            // 数据
            dcPanelBody.find("[data]").each(function () {
                var _data = jQuery(this).attr("data");

                obj[_data] = jQuery(this).val();
                if (jQuery(this).attr("type") == "number") {
                    obj[_data] -= 0;
                }
                if (isChange && Object.hasOwnProperty.call(data, _data)) {
                    jQuery(this).val(data[_data]);
                }
            });
            if (isChange) {
                return true;
            } else {
                return obj;
            }
        }
        //赋值启用页眉页脚
        if (options && options.EnableHeaderFooter) {
            jQuery(ctl).find('#DCEnableHeaderFooter').attr("checked", options.EnableHeaderFooter === 'True' ? true : false);
            DCSetEnableHeaderFooterControl(options.EnableHeaderFooter);
        }
        //根据是否启用页眉页脚，控制页眉页脚表单的禁用状态
        function DCSetEnableHeaderFooterControl(control) {
            var DCEnableHeaderFooterControl = jQuery(ctl).find('#DCEnableHeaderFooterControl').find('input');
            if (DCEnableHeaderFooterControl && DCEnableHeaderFooterControl.length) {
                for (var i = 0; i < DCEnableHeaderFooterControl.length; i++) {
                    var item = DCEnableHeaderFooterControl[i];
                    jQuery(item).attr("disabled", control === 'False' ? true : false);
                }
            }
        }
        //监听启用页眉页脚的勾选事件
        jQuery(ctl).find('#DCEnableHeaderFooter').change(function () {
            DCSetEnableHeaderFooterControl(jQuery(this).is(':checked') ? 'True' : 'False');
        });

        function successFun() {
            var _data = PageSettingsData();
            var HeaderFooterDifferentFirstPage = jQuery(ctl).find('#DCHeaderFooterDifferentFirstPage')[0].checked;//首页页眉页脚不同
            var PageIndexsForHideHeaderFooter = jQuery(ctl).find('#DCPageIndexsForHideHeaderFooter')[0].value;//隐藏页眉页脚的页码
            var EnableHeaderFooter = jQuery(ctl).find('#DCEnableHeaderFooter')[0].checked;//启用页眉页脚功能
            _data = {
                ..._data,
                EnableHeaderFooter,
                HeaderFooterDifferentFirstPage,
                PageIndexsForHideHeaderFooter
            };
            ctl.ChangeDocumentSettings(_data);
            ctl.RefreshDocument();
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetDocumentPageSettings();
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },
    /**
     * 创建文档网格线设置对话框
     * @param options 文档网格线属性
     * @param ctl 编辑器元素
     */
    DocumentGridLineDialog: function (options, ctl) {
        if (!options || typeof options != "object") {
            // 当未传入值时，获取当前的文档网格线数据
            options = ctl.GetDocumentGridLine();
        }
        options = keysToLowerCase(options);
        var opts = {
            Visible: "",
            ColorValue: "",
            LineStyle: "",
            GridNumInOnePage: "",
            AlignToGridLine: "",
            Printable: "",
        };
        for (var i in opts) {
            var low_i = i.toLowerCase();
            if (Object.hasOwnProperty.call(options, low_i)) {
                opts[i] = options[low_i];
            }
        }
        var DocumentGridLineHtml = `
        <div class="dcBody-content">
            <label>
                <input type="checkbox" name="Visible" data-text="Visible">
                <span>绘制网格线</span>
            </label>
        </div>
        <form id="dc_DocumentGridLine_form">
            <div class="dcBody-content">
                <label class="dc_changewidth">
                    <span class="dc_txt">网格线颜色：</span>
                    <input type="color" name="ColorValue" data-text="ColorValue">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_changewidth">
                    <span class="dc_txt">网格线样式：</span>
                    <select name="LineStyle" id="dc_LineStyle" data-text="LineStyle"></select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_changewidth">
                    <span class="dc_txt">每页行数<span class="dc_SpecifyWidth_title" title="每页行数必须大于2">?</span>：</span>
                    <input type="number" name="GridNumInOnePage" data-text="GridNumInOnePage" value="20">
                </label>
            </div>
            <div class="dcBody-content">
                <label>
                    <input type="checkbox" name="AlignToGridLine" data-text="AlignToGridLine" checked>
                    <span>文本是否对齐到网格线</span>
                </label>
            </div>
            <div class="dcBody-content">
                <label>
                    <input type="checkbox" name="Printable" data-text="Printable" checked>
                    <span>是否打印网格线</span>
                </label>
            </div>
        </form>
        `;
        var dialogOptions = {
            title: "文档网格线设置",
            bodyClass: "DocumentGridLine",
            bodyHtml: DocumentGridLineHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var LineStyle_node = dcPanelBody.find("#dc_LineStyle");
        let that = this;

        for (var i = 0; i < DASHSTYLE.length; i++) {
            var _DashStyle = DASHSTYLE[i];
            LineStyle_node.append(
                "<option value='" +
                _DashStyle.name +
                "'>" +
                _DashStyle.show +
                _DashStyle.name +
                "</option>"
            );
        }
        var DocumentGridLine_form = dcPanelBody.find(
            "form#dc_DocumentGridLine_form"
        )[0];
        // 是否绘制网格线点击事件
        dcPanelBody.find("input[data-text=Visible]").on("click", function () {
            var isVisible = jQuery(this).is(":checked");
            that.changeFormDisable(DocumentGridLine_form, !isVisible);
        });

        function DocumentGridLineData(data) {
            var isChange = typeof data == "object";
            var obj = {};
            dcPanelBody.find("[data-text]").each(function () {
                var _el = jQuery(this);
                var _txt = _el.attr("data-text");
                if (this.type == "checkbox") {
                    obj[_txt] = _el.is(":checked");
                    if (isChange) {
                        _el.prop("checked", data[_txt]);
                        if (_txt == "Visible") {
                            //是否绘制网格线
                            that.changeFormDisable(DocumentGridLine_form, !data[_txt]);
                        }
                    }
                } else {
                    obj[_txt] = _el.val();
                    if (this.type == "number") {
                        obj[_txt] -= 0;
                    }
                    if (isChange) {
                        _el.val(data[_txt]);
                    }
                }
            });
            if (isChange) {
                return true;
            } else {
                return obj;
            }
        }
        // console.log("当前文档网格线设置值=>", opts)
        // 添加值
        DocumentGridLineData(opts);
        function successFun() {
            var _data = DocumentGridLineData();
            ctl.SetDocumentGridLine(_data);
            ctl.RefreshDocument();
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetDocumentGridLine();
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 创建文档装订线设置对话框
     * @param options 文档装订线属性
     * @param ctl 编辑器元素
     */
    DocumentGutterDialog: function (options, ctl) {
        if (!options || typeof options != "object") {
            // 当未传入值时，获取当前的文档装订线数据
            options = ctl.GetDocumentGutter();
        }
        options = keysToLowerCase(options);
        var opts = {
            ShowGutterLine: "",
            GutterPosition: "",
            SwapGutter: "",
            GutterStyle: "",
        };
        for (var i in opts) {
            var low_i = i.toLowerCase();
            if (Object.hasOwnProperty.call(options, low_i)) {
                opts[i] = options[low_i];
            }
        }
        var DocumentGutterHtml = `<div class="dcBody-content">
                <label>
                    <input type="checkbox" name="ShowGutterLine" data-text="ShowGutterLine">
                    <span>显示装订线</span>
                </label>
            </div>
            <div class="dcBody-content">
                <label>
                    <span class="dc_txt">装订线距离：</span>
                    <input type="number" name="GutterPosition" data-text="GutterPosition" value="0">
                </label>
            </div>
            <div class="dcBody-content">
                <label>
                    <input type="checkbox" name="SwapGutter" data-text="SwapGutter" checked>
                    <span>为双面打印切换装订线</span>
                </label>
            </div>
            <div class="dcBody-content dc_GutterStyleDiv">
                <span>位置</span>
                <label><input type="radio" name="GutterStyle" value="Left" data-text="GutterStyle" checked><span>左</span></label>
                <label><input type="radio" name="GutterStyle" value="Top" data-text="GutterStyle"><span>上</span></label>
                <label><input type="radio" name="GutterStyle" value="Right" data-text="GutterStyle"><span>右</span></label>
            </div>`;
        var dialogOptions = {
            title: "文档装订线设置",
            bodyClass: "DocumentGutter",
            bodyHtml: DocumentGutterHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        GetOrChangeData(dcPanelBody, opts);
        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            // console.log("successFun -> _data", _data)
            ctl.SetDocumentGutter(_data);
            ctl.RefreshDocument();
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetDocumentGutter();
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 创建单复选框属性对话框
     * @param options 单复选框属性
     * @param ctl 编辑器元素
     */
    CheckboxAndRadioDialog: function (options, ctl, ele) {
        var typename = ctl.GetCurrentElementTypeName();//当前元素的类型名称
        if (!options || typeof options != "object" || JSON.stringify(options) === '{}') {
            if (ele) {
                options = ctl.GetElementProperties(ele);
            } else if (['xtextradioboxelement', 'xtextcheckboxelement'].includes(typename)) {
                options = ctl.GetElementProperties(ctl.CurrentElement(typename));
            }
        }
        if (!options) {
            return false;
        }
        var backupCaptionFlowLayout = options.CaptionFlowLayout;
        var backupParent = options.Parent;
        //wyc20231019:防止数据丢失做一个转换
        if (options.VisualStyle === "SystemDefault") {
            options.VisualStyle = "Default";
        }
        if (options.VisualStyle === "SystemCheckBox") {
            options.VisualStyle = "CheckBox";
        }
        if (options.VisualStyle === "SystemRadioBox") {
            options.VisualStyle = "RadioBox";
        }

        var checkboxAndRadioHTML = `
                    <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">编号：</span>
                    <input type="text" class="dc_full" name="ID" data-text="ID">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">名称：</span>
                    <input type="text" class="dc_full" name="Name" data-text="Name">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">文本：</span>
                    <input type="text" class="dc_full" name="Text" data-text="Text">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">数值：</span>
                    <input type="text" class="dc_full" name="Value" data-text="Value">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">提示文本：</span>
                    <input type="text" class="dc_full" name="ToolTip" data-text="ToolTip">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">附加数据：</span>
                    <input type="text" class="dc_full" name="StringTag" data-text="StringTag">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">显示样式：</span>
                    <select class="dc_full" data-text="VisualStyle">
                        <option value="Default">默认样式</option>
                        <option value="CheckBox">复选框样式</option>
                        <option value="RadioBox">单选框样式</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span  class="dcTitle-text">高亮度状态：</span>
                    <select class="dc_full" data-text="EnableHighlight">
                        <option value="Default">默认</option>
                        <option value="Enabled">允许</option>
                        <option value="Disabled">禁止</option>
                    </select>
                </label>
            </div>
             <div class="dcBody-content">
                 <label class="dc_flex">
                    <span class="dcTitle-text">可见性表达式：</span>
                    <input data-text="VisibleExpression" class="dc_full"  type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_input_PropertyExpressions_label" >
                    <span class="dcTitle-text">属性表达式：</span>
                    <input id="dc_PropertyExpressions_show_input" class="dc_full" readonly="readonly" type="text">
                    <button id="dc_PropertyExpressionsButton">打开</button>
                </label>
            </div>
            
            <div class="dcBody-content">
                <div class="dc_checkboxs">
                    <label>
                        <input type="checkbox" name="Checked"  data-text="Checked">
                        <span>处于选择状态</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Deleteable"  data-text="Deleteable">
                        <span>可以删除</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Multiline"  data-text="Multiline">
                        <span>文本多行</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Enabled"  data-text="Enabled">
                        <span>对象是否可用</span>
                    </label>
                    <label>
                        <input type="checkbox" name="CheckAlignLeft"  data-text="CheckAlignLeft">
                        <span>勾选框左对齐</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Requried"  data-text="Requried">
                        <span>必勾项</span>
                    </label>
                    <label>
                        <input type="checkbox" name="CaptionFlowLayout" data-text="CaptionFlowLayout">
                        <span>文本参与流式排版</span>
                    </label>
                </div>
            </div>
            <div class="dcBody-content dc_Box">
                <h6 class="dc_title">可见性配置</h6>
                <div id="dc_CheckboxVisibility">
                    <label>
                        <input type="checkbox" data-value="Hidden">
                        <span>Hidden</span>
                    </label>
                    <label>
                        <input type="checkbox" data-value="Paint">
                        <span>Paint</span>
                    </label>
                    <label>
                        <input type="checkbox" data-value="Print">
                        <span>Print</span>
                    </label>
                    <label>
                        <input type="checkbox" data-value="PDF">
                        <span>PDF</span>
                    </label>
                    <label>
                        <input type="checkbox" data-value="All">
                        <span>All</span>
                    </label>
                </div>
            </div>
            <div class="dcBody-content dc_Box">
                <h6 class="dc_title">自定义属性</h6>
                <div id="dc_attr-box"></div>
            </div>
        `;
        var dialogOptions = {
            title: (typename === 'xtextradioboxelement' ? "单" : "复") + "选框属性",
            bodyHeight: 455,
            bodyClass: "CheckboxAndRadio",
            bodyHtml: checkboxAndRadioHTML,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        this.attributeComponents(
            "#dc_attr-box",
            (options && options.Attributes) || {},
            ctl
        );
        WriterControl_Dialog.appendValueBindingDiv(dcPanelBody);
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, opts);

        //[DUWRITER5_0-3387]20240820 lxy 增加可见性配置
        var allCheckboxVisibility = dcPanelBody.find("#dc_CheckboxVisibility input[data-value]");
        if (options.CheckboxVisibility) {
            let checkValue = options.CheckboxVisibility.toLowerCase();
            for (var i = 0; i < allCheckboxVisibility.length; i++) {
                let itemValue = allCheckboxVisibility[i].getAttribute('data-value');
                itemValue = itemValue.toLowerCase().trim();
                if (checkValue.indexOf(itemValue) > -1) {
                    allCheckboxVisibility[i].checked = true;
                } else {
                    allCheckboxVisibility[i].checked = false;
                }
            }

        }



        //[DUWRITER5_0-3748] 20241025 lxy 新增属性表达式功能
        //属性表达式值回填
        var inputValue = '';
        var propertyExpressionObject = {};
        var propertyShowInput = ctl.ownerDocument.getElementById('dc_PropertyExpressions_show_input');
        var propertyKeyArr = (options.PropertyExpressions && Object.keys(options.PropertyExpressions)) || [];
        RADIOCHECKPROPERTYEXPRESSIONSARRAY.forEach(item => {
            if (propertyKeyArr.length && propertyKeyArr.indexOf && propertyKeyArr.indexOf(item) > -1) {
                propertyExpressionObject[item] = options.PropertyExpressions[item];
                //展示文本
                if (options.PropertyExpressions[item] !== '') {
                    inputValue += `${inputValue === '' ? '' : ','}${item}:${options.PropertyExpressions[item]}`;
                }
            } else {
                propertyExpressionObject[item] = '';
            }
        });

        propertyShowInput.value = inputValue;

        //属性表达式操作对话框

        var propertyExpressionsButton = jQuery(ctl).find("#dc_PropertyExpressionsButton");
        propertyExpressionsButton.click(function () {

            // //判断是否已经存在修改过的属性表达式
            // if (!propertyExpressionObject || Object.keys(propertyExpressionObject).length === 0) {
            //     RADIOCHECKPROPERTYEXPRESSIONSARRAY.forEach(item => {
            //         propertyExpressionObject[item] = '';
            //     });
            // }

            that.PropertyExpressionsDialog(propertyExpressionObject, ctl, function (changedPropertyExpressions) {
                //获取修改后的属性表达式
                propertyExpressionObject = JSON.parse(JSON.stringify(changedPropertyExpressions));
                //更新属性表达式的显示
                var inputValue = '';
                for (let key in changedPropertyExpressions) {
                    if (changedPropertyExpressions[key] !== '' && key !== 'Visible') {
                        inputValue += `${inputValue === '' ? '' : ','}${key}:${changedPropertyExpressions[key]}`;
                    }
                }
                propertyShowInput.value = inputValue;
            });
        });



        //成功的回调函数
        let that = this;
        function successFun() {
            let dcAttrBox = dcPanelBody.find('#dc_attr-box');
            let Attributes = that.attributeComponents_getAttributeObj(dcAttrBox);
            var _data = GetOrChangeData(dcPanelBody);
            _data["Attributes"] = Attributes;
            let CheckboxVisibility = [];
            allCheckboxVisibility.each(function () {
                if (this.checked) {
                    CheckboxVisibility.push(this.getAttribute('data-value'));
                }
            });
            _data["CheckboxVisibility"] = CheckboxVisibility.join(",");

            // [DUWRITER5_0-3748] 20241025 lxy 新增属性表达式功能
            _data['PropertyExpressions'] = {};
            for (var key in propertyExpressionObject) {
                if (propertyExpressionObject[key] !== '') {
                    _data['PropertyExpressions'][key] = propertyExpressionObject[key];
                }
            }

            //可见性表达式赋值
            if (_data && _data.VisibleExpression && _data.VisibleExpression.trim() !== '') {
                _data['PropertyExpressions']['Visible'] = _data.VisibleExpression;
            }

            ctl.SetElementProperties(ele, _data, true);
            //wyc20231019:更改流式排版属性会导致排版错乱，在此追加一个对父容器的刷新
            if (_data.CaptionFlowLayout !== backupCaptionFlowLayout) {
                ctl.EditorRefreshContainerView(backupParent);
            }
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 创建插入多个单选框/复选框对话框
     * @param options 单复选框属性
     * @param ctl 编辑器元素
     */
    InsertMultipleCheckBoxOrRadioDialog: function (options, ctl) {
        if (!options || typeof options != "object") {
            // 当未传入值时,
            options = {};
        }
        console.log(options, '========options');
        var InsertMultipleCheckBoxOrRadioHtml = `
        <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">名称：</span>
                    <input type="text" class="dc_full" name="Name" data-text="Name">
                </label>
                <label class="dc_flex">
                    <span class="dcTitle-text">可见性表达式：</span>
                    <input data-text="VisibleExpression" class="dc_full" type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
            </div>
            <div class="dcBody-content">
                <form class="dc_Box IsRadioBox">
                    <h6 class="dc_title">类型</h6>
                    <div class="dcBody-content">
                        <label class="dc_firstRadio">
                            <input type="radio" name="Type" data-text="Type" value="radio" checked>
                            <span>单选框</span>
                        </label>
                        <label>
                            <input type="radio" name="Type" data-text="Type" value="checkbox">
                            <span>复选框</span>
                        </label>
                    </div>
                </form>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">显示样式：</span>
                    <select class="dc_full" data-text="VisualStyle">
                        <option value="Default">默认样式</option>
                        <option value="CheckBox">复选框样式</option>
                        <option value="RadioBox">单选框样式</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <div class="dc_checkboxs">
                    <label>
                        <input type="checkbox" name="Deleteable" data-text="Deleteable" checked>
                        <span>可以删除</span>
                    </label>
                    <label>
                        <input type="checkbox" name="CheckAlignLeft" data-text="CheckAlignLeft" checked>
                        <span>勾选框左对齐</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Multiline" data-text="Multiline">
                        <span>文本多行</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Requried" data-text="Requried">
                        <span>必勾项</span>
                    </label>
                    <label>
                        <input type="checkbox" name="CaptionFlowLayout" checked data-text="CaptionFlowLayout">
                        <span>文本参与流式排版</span>
                    </label>
                </div>
            </div>
            <div class="dc_Box" >
                <h6 class="dc_title">赋值属性：</h6>
                <div class="dc_tab3Content">
                    <label>
                        <span>数据源名称：</span>
                        <input id="dc_DataSource" type="text" />
                    </label>
                    <label>
                        <span>绑定路径：</span>
                        <input id="dc_BindingPath"  type="text" />
                    </label>
                </div>
            </div>
            <div class="dcBody-content">
                <form class="dc_Box">
                    <h6 class="dc_title">项目</h6>
                    <table id="dc_ListItems" data-text="ListItems" data-type="Array" class="dc_scroll-table">
                        <thead>
                            <th>编号</th>
                            <th>文本</th>
                            <th>数值</th>
                            <th class="dc_last">操作</th>
                        </thead>
                        <tbody>
                            <tr>
                                <td><input type="text" data-arraytext="ID"></td>
                                <td><input type="text" data-arraytext="Text"></td>
                                <td><input type="text" data-arraytext="Value"></td>
                                <td class="dc_delete" title="删除">×</td>
                            </tr>
                        </tbody>
                        <template class="dc_template_item">
                            <tr>
                                <td><input type="text" data-arraytext="ID"></td>
                                <td><input type="text" data-arraytext="Text"></td>
                                <td><input type="text" data-arraytext="Value"></td>
                                <td class="dc_delete" title="删除">×</td>
                            </tr>
                        </template>
                    </table>
                </form>
            </div>
        `;
        var dialogOptions = {
            title: "插入多个单选框/复选框",
            bodyHeight: 500,
            bodyClass: "InsertMultipleCheckBoxOrRadio",
            bodyHtml: InsertMultipleCheckBoxOrRadioHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        // 增加接口
        dcPanelBody
            .find("#dc_ListItems")
            .on("input", "input[data-arraytext]", function () {
                var input = jQuery(this);
                var tr = input.parents("tr");
                if (tr.nextAll("tr").length == 0) {
                    var ListItems_item = input
                        .parents("table")
                        .find("template.dc_template_item")[0];
                    tr.after(ListItems_item.content.cloneNode(true));
                }
            });
        dcPanelBody.find("#dc_ListItems").on("click", "td.dc_delete", function () {
            var tr = jQuery(this).parents("tr");
            if (tr.nextAll("tr").length > 0) {
                tr.remove();
            }
        });
        // 给数据源绑定赋值
        if (options && options.ValueBinding) {
            dcPanelBody
                .find("#dc_DataSource")
                .val(options.ValueBinding.DataSource || "");
            dcPanelBody
                .find("#dc_BindingPath")
                .val(options.ValueBinding.BindingPath || "");
        }
        GetOrChangeData(dcPanelBody, opts);

        function successFun() {
            let {
                Type,
                Name,
                ListItems,
                VisualStyle,
                Deleteable,
                CheckAlignLeft,
                Multiline,
                Requried,
                VisibleExpression,
                CaptionFlowLayout
            } = GetOrChangeData(dcPanelBody);
            let DataSource = dcPanelBody.find("#dc_DataSource").val();
            let BindingPath = dcPanelBody.find("#dc_BindingPath").val();
            var listItemsOptions = {
                Multiline,
                VisualStyle,
                Deleteable,
                CheckAlignLeft,
                Requried,
                VisibleExpression,
                CaptionFlowLayout
            };
            if (ListItems && ListItems.length) {
                //如果没有原始数据，则直接进行添加
                for (var j = 0; j < ListItems.length; j++) {
                    ListItems[j] = Object.assign(ListItems[j], listItemsOptions);
                    ListItems[j]['ValueBinding'] = {
                        DataSource,
                        BindingPath,
                    };
                }
            }
            // 如果插入时设置了一些对话框中没有的属性，则进行一次数据保留
            if (options && options.ListItems && options.ListItems.length) {
                for (var i = 0; i < options.ListItems.length; i++) {
                    var item = options.ListItems[i];
                    for (var j = 0; j < ListItems.length; j++) {
                        if (item.ID === ListItems[j].ID) {
                            ListItems[j] = Object.assign(item, ListItems[j]);
                        }
                    }
                }
            }

            var newData = {
                ...options,
                Type,
                Name,
                ListItems
            };
            var result = ctl.DCExecuteCommand("insertcheckboxorradio", false, newData);
            if (result) {
                //DUWRITER5_0-3877 20241121 lxy 新增插入多个单选框/复选框后触发事件
                if (ctl.EventInsertMultipleCheckBoxOrRadioAfter && typeof ctl.EventInsertMultipleCheckBoxOrRadioAfter === 'function') {
                    ctl.EventInsertMultipleCheckBoxOrRadioAfter(newData);
                }
            }

        }
    },

    /**
     * 创建文本标签属性对话框
     * @param options 文本标签属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    LabelDialog: function (options, ctl, isInsertMode, ele) {
        if (!options || typeof options != "object") {
            // 当未传入值时
            if (isInsertMode == true) {
                options = {};
            } else {
                ele = ctl.CurrentElement("xtextlabelelement");
                if (ele == null) {
                    return false;
                }
                options = ctl.GetElementProperties(ele);
                if (options == null) {
                    return false;
                }
            }
        }
        if (!options) {
            return false;
        }
        if (Object.hasOwnProperty.call(options, "Text") == false) {
            // 当数据中不包含Text时，赋值默认
            options.Text = "标签文本";
        }
        var LabelHtml = `
                    <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">编号：</span>
                    <input type="text" class="dc_full" name="ID" data-text="ID">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">名称：</span>
                    <input type="text" class="dc_full" name="Name" data-text="Name">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">可见性表达式：</span>
                    <input data-text="VisibleExpression" class="dc_full"  type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
            </div>
   
            <div class="dcBody-content">
                <div class="dc_checkboxs">
                    <label>
                        <input type="checkbox" name="AutoSize"  data-text="AutoSize" >
                        <span>自动大小</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Multiline" data-text="Multiline" >
                        <span>自动换行</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Deleteable" data-text="Deleteable" >
                        <span>能否被删除</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Bold" data-text="Bold" >
                        <span>是否加粗</span>
                    </label>
                </div>
            </div>
            <div class="dcBody-content">
                <form class="dc_Box">
                    <h6 class="dc_title">连接模式设置</h6>
                    <div class="dcBody-content">
                        <label class="dc_flex">
                            <span class="dcTitle-text">模式：</span>
                            <select data-text="ContactAction" class="dc_full">
                                <option value="Disable" title="禁止文本连接">Disable</option>
                                <option value="Normal" title="正常模式">Normal</option>
                                <option value="FirstSectionInPage" title="只显示当前页面中第一个文档的文本">FirstSectionInPage</option>
                                <option value="LastSectionInPage" title="只显示当前页面中最后一个文档的文本">LastSectionInPage</option>
                                <option value="TableRow" title="表格行">TableRow</option>
                                <option value="FirstTableRowInPage" title="页面中第一个表格行">FirstTableRowInPage</option>
                                <option value="LastTableRowInPage" title="页面中最后一个表格行">LastTableRowInPage</option>
                            </select>
                        </label>
                    </div>
                    <div class="dcBody-content">
                        <label class="dc_flex">
                            <span class="dcTitle-text">属性名：</span>
                            <input type="text" class="dc_full" name="AttributeNameForContactAction"
                                data-text="AttributeNameForContactAction">
                        </label>
                    </div>
                    <div class="dcBody-content">
                        <label class="dc_flex">
                            <span class="dcTitle-text">连接文本：</span>
                            <input type="text" class="dc_full" name="LinkTextForContactAction"
                                data-text="LinkTextForContactAction">
                        </label>
                    </div>
                </form>
            </div>
            <div class="dcBody-content">
                <label class="dc_blockelement">
                    <div>文本（当文本内容过多时，建议勾选自动大小）：</div>
                    <div>
                        <textarea data-text="Text" value="标签文本"></textarea>
                    </div>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "文本标签元素",
            bodyHeight: 475,
            bodyClass: "labelElement",
            bodyHtml: LabelHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, opts);
        //处理设置后无效的属性
        let checkList = dcPanelBody.find('input[type="checkbox"]');
        let optionKeys = Object.keys(options);
        for (var i = 0; i < checkList.length; i++) {
            let item = checkList[i];
            if (optionKeys.indexOf(item.name) !== -1) {
                if (options[item.name]) {
                    item.setAttribute("checked", true);
                } else {
                    jQuery(item).removeAttr("checked");
                }
            } else {
                jQuery(item).removeAttr("checked");
            }
        }
        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            console.log("successFun -> _data", {
                ...options,
                ..._data
            });
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("InsertLabelElement", false, {
                    ...options,
                    ..._data
                });
            } else {
                ctl.SetElementProperties(ele, _data, true);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement("xtextlabelelement"));
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }

        }
    },

    /**
     * 创建水平线/分割线属性对话框
     * @param options 水平线/分割线属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    HorizontalLineDialog: function (options, ctl, isInsertMode, ele) {
        console.log(options, '=========options');
        if (!options || typeof options != "object") {
            // 当未传入值时
            if (isInsertMode == true) {
                options = {};
            } else {
                ele = ctl.CurrentElement("xtexthorizontallineelement");
                if (ele == null) {
                    return false;
                }
                options = ctl.GetElementProperties(ele);
            }
        }
        if (options == null) {
            return false;
        }
        var HorizontalLineHtml = `
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text">编号：</span>
                <input type="text" class="dc_full" name="ID" data-text="ID">
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text">可见性表达式：</span>
                <input data-text="VisibleExpression" data-text="VisibleExpression" class="dc_full" type="text">
                <button class="dc_visible_expression">示例</button>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text">颜色：</span>
                <input type="color" class="dc_full" name="Color" data-text="Color">
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text">线条样式：</span>
                <select class="dc_full" name="Style" data-text="LineStyle">
                    <option value="Solid">Solid</option>
                    <option value="Dash">Dash</option>
                    <option value="Dot">Dot</option>
                    <option value="DashDot">DashDot</option>
                    <option value="DashDotDot">DashDotDot</option>
                </select>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text">线条粗细：</span>
                <input type="number" class="dc_full" name="Width" data-text="LineSize">
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text">线条长度：</span>
                <input type="number" class="dc_full" name="Length" data-text="LineLengthInCM">
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "水平分割线属性",
            bodyHeight: 220,
            bodyClass: "HorizontalLineElement",
            bodyHtml: HorizontalLineHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        // GetOrChangeData(dcPanelBody, options);
        Object.keys(options).forEach(item => {
            dcPanelBody.find(`[data-text="${item}"]`).val(options[item]);
        });

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            // console.log("successFun -> _data", _data)
            options = {
                ...options,
                ..._data
            };
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("InsertHorizontalLine", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement("xtexthorizontallineelement"));
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建页码属性对话框
     * @param options 页码属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    PageNumberDialog: function (options, ctl, isInsertMode, ele) {
        if (!options || typeof options != "object") {
            // 当未传入值时
            if (isInsertMode == true) {
                options = {};
            } else {
                ele = ctl.CurrentElement("xtextpageinfoelement");
                options = ctl.GetElementProperties(ele);
            }
        }
        if (options == null) {
            return false;
        }
        var PageNumberHtml = `
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">编号：</span>
                    <input type="text" class="dc_full" name="ID" data-text="ID">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">宽度：</span>
                    <input type="number" class="dc_full" name="Width" data-text="Width">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">高度：</span>
                    <input type="number" class="dc_full" name="Height" data-text="Height">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">可见性表达式：</span>
                    <input data-text="VisibleExpression" class="dc_full" type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
            </div>
            <div class="dcBody-content">
                <span class="dcTitle-text">内容：</span>
                <input type="hidden" name="ValueType" data-text="ValueType">
                <ul id="dc_ValueType">
                    <li data-value="PageIndex" class="dc_active">页码</li>
                    <li data-value="NumOfPages">总页码</li>
                    <li data-value="LocalPageIndex">本地页码</li>
                    <li data-value="LocalNumOfPages">本地总页码</li>
                </ul>
            </div>
            <div class="dcBody-content" style="display: none;">
                <label class="dc_flex dc_minflex">
                    <span class="dcTitle-text">数字显示格式：</span>
                    <select class="dc_full">
                        <option value="None">无</option>
                        <option value="ListNumberStyle" selected>1. 2. 3. 4.</option>
                        <option value="ListNumberStyleArabic1">1, 2, 3, 4,</option>
                        <option value="ListNumberStyleArabic2">1) 2) 3) 4)</option>
                        <option value="ListNumberStyleLowercaseLetter ">a) b) c) d)</option>
                        <option value="ListNumberStyleLowercaseRoman">i) ii) iii) iv)</option>
                        <option value="ListNumberStyleSimpChinNum1">一. 二. 三. 四.</option>
                        <option value="ListNumberStyleSimpChinNum2">一) 二) 三) 四)</option>
                        <option value="ListNumberStyleNumberInCircle">①, ②, ③, ④,</option>
                        <option value="ListNumberStyleTradChinNum1">壹. 贰. 叁. 肆.</option>
                        <option value="ListNumberStyleTradChinNum2">壹) 贰) 叁) 肆)</option>
                        <option value="ListNumberStyleUppercaseLetter">A) B) C) D)</option>
                        <option value="ListNumberStyleUppercaseRoman">I) II) III) IV)</option>
                        <option value="ListNumberStyleZodiac1">甲, 乙, 丙, 丁,</option>
                        <option value="ListNumberStyleZodiac2">子, 丑, 寅, 卯,</option>
                        <option value="ListNumberStyleArabic3">1、2、3、4、</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_minflex">
                    <span class="dcTitle-text">格式化字符串：</span>
                    <input type="text" class="dc_full" name="FormatString" data-text="FormatString"
                        list="FormatStringList">
                    <datalist>
                        <option value="第[%PageIndex%]页 共[%NumOfPages%]页">第[%PageIndex%]页 共[%NumOfPages%]页</option>
                        <option value="[%PageIndex%]/[%NumOfPages%]">[%PageIndex%]/[%NumOfPages%]</option>
                    </datalist>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_Newline">
                    <span class="dcTitle-text">指定的页码编号文本列表：</span>
                    <input type="text" class="dc_full" name="SpecifyPageIndexTextList"
                        data-text="SpecifyPageIndexTextList">
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "页码属性对话框",
            bodyHeight: 400,
            bodyClass: "HorizontalLineElement",
            bodyHtml: PageNumberHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var ValueTypeInput = dcPanelBody.find("[data-text=ValueType]");
        var lis = dcPanelBody.find("#dc_ValueType li");
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, opts);
        lis.removeClass("dc_active");
        lis
            .filter("[data-value='" + ValueTypeInput.val() + "']")
            .addClass("dc_active");
        if (lis.filter(".dc_active").length == 0) {
            lis.eq(0).addClass("dc_active");
        }
        // 页码内容选择
        lis.on("click", function () {
            jQuery(this).siblings("li").removeClass("dc_active");
            jQuery(this).addClass("dc_active");
            ValueTypeInput.val(jQuery(this).attr("data-value"));
        });
        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            // console.log("successFun -> _data", _data)
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("InsertPageInfoElement", false, _data);
            } else {
                ctl.SetElementProperties(ele, _data, true);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement("xtextpageinfoelement"));
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建按钮属性对话框
     * @param options 按钮属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    ButtonDialog: function (options, ctl, isInsertMode, ele) {
        let that = this;
        // ctl.CurrentElement('xtextbuttonelement');
        if (!options || typeof options != "object") {
            // 当未传入值时
            if (isInsertMode == true) {
                options = {};
            } else {
                ele = ctl.CurrentElement("xtextbuttonelement");
                if (ele == null) {
                    return false;
                }
                options = ctl.GetElementProperties(ele);
            }
        }
        if (options == null) {
            return false;
        }
        console.log(options, "==========options");
        var ButtonHtml = `
        <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">编号：</span>
                    <input type="text" class="dc_full" name="ID" data-text="ID">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">名称：</span>
                    <input type="text" class="dc_full" name="Name" data-text="Name">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">宽度：</span>
                    <input type="number" class="dc_full" name="Width" data-text="Width">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">高度：</span>
                    <input type="number" class="dc_full" name="Height" data-text="Height">
                </label>
            </div>
            <div class="dcBody-content">
                <div class="dc_checkboxs">
                    <label>
                        <input type="checkbox" name="Deleteable" data-text="Deleteable" checked>
                        <span>能否被删除</span>
                    </label>
                    <label>
                        <input type="checkbox" name="Enabled" data-text="Enabled" checked>
                        <span>是否可用</span>
                    </label>
                    <label>
                        <input type="checkbox" name="AutoSize" data-text="AutoSize">
                        <span>自动大小</span>
                    </label>
                    <label>
                        <input type="checkbox" name="PrintAsText" data-text="PrintAsText">
                        <span>以文本方式打印</span>
                    </label>
                </div>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">打印时可见：</span>
                    <select  data-text="PrintVisibility" class="dc_full">
                        <option value="Visible">显示</option>
                        <option value="Hidden">隐藏</option>
                        <option value="None">隐藏而且不占位</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">命令文本：</span>
                    <input type="text" class="dc_full" name="CommandName" data-text="CommandName">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">可见性表达式：</span>
                    <input data-text="VisibleExpression" class="dc_full" type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
            </div>

            <div class="dcBody-content dcBody-content-wring" >注：下面图片使用png格式图片</div>
            <div class="dcBody-content">
                <label class="dc_flex dc_imgButtonBox">
                    <span class="dcTitle-text">按钮图片：</span>
                    <button class="dc_full" name="ImgBase64" data-text="ImgBase64" data-value="img">
                        <img src="" alt="">
                        <input type="file" accept="image/*">
                    </button>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_imgButtonBox">
                    <span class="dcTitle-text">按下时图片：</span>
                    <button class="dc_full" name="ImgBase64ForDown" data-text="ImgBase64ForDown" data-value="img">
                        <img src="" alt="">
                        <input type="file" accept="image/*">
                    </button>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_imgButtonBox">
                    <span class="dcTitle-text">鼠标悬停时图片：</span>
                    <button class="dc_full" name="ImgBase64ForOver" data-text="ImgBase64ForOver" data-value="img">
                        <img src="" alt="">
                        <input type="file" accept="image/*">
                    </button>
                </label>
            </div>
            <div class="dc_Box">
                <h6 class="dc_title">颜色设置：</h6>
                <div class="dc_button_style_box" id="dc_button_style_box">
                    <div class="dc_button_style_box_backgroundcolorstring">
                        <span>背景颜色：</span>
                        <label class="dc_color_label">
                            <div id="dc_backgroundcolorstring_box" data-value=""></div>
                            <input id="dc_button_style_backgroundcolorstring" dc-target-id="dc_backgroundcolorstring_box" type="color" />
                        </label>
                    </div>
                    <div class="dc_button_style_box_colorstring">
                        <span >文字颜色：</span>
                        <label class="dc_color_label">
                            <div id="dc_colorstring_box" data-value=""></div>
                            <input id="dc_button_style_colorstring" dc-target-id="dc_colorstring_box" type="color" />
                        </label>
                    </div>
                </div>
            </div>
            <div class="dc_Box">
                <h6 class="dc_title">按钮字体设置：</h6>
                <div class="dc_button_style_box" >
                    <div>
                        <span >字体名称：</span>
                        <select id="dc_button_style_fontname"></select>
                    </div>
                    <div>
                        <span >字体大小：</span>
                        <select  id="dc_button_style_fontsize"></select>
                    </div>
                    <div>
                        <span >加粗：</span>
                        <select  id="dc_button_style_bold" >
                            <option value="false">否</option>
                            <option value="true">是</option>
                        </select>
                    </div>
                   
                </div>
            </div>
            <div class="dcBody-content">
                <label class="dc_blockelement">
                    <div>文本：</div>
                    <div>
                        <textarea data-text="Text"></textarea>
                    </div>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_blockelement">
                    <div>脚本：</div>
                    <div>
                        <textarea data-text="ScriptTextForClick"></textarea>
                    </div>
                </label>
            </div>
            <div class="dcBody-content dc_Box">
                <h6 class="dc_title">自定义属性</h6>
                <div id="dc_attr-box"></div>
            </div>
        `;
        var dialogOptions = {
            title: "按钮属性",
            bodyHeight: 400,
            bodyClass: "ButtonElement",
            bodyHtml: ButtonHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");


        that.attributeComponents("#dc_attr-box", options.Attributes || {}, ctl);

        // 字体样式回填
        var arrFont = [];
        if (
            window.WriterControl_SupportFontFamilys &&
            window.WriterControl_SupportFontFamilys.length
        ) {
            arrFont = window.WriterControl_SupportFontFamilys;
        } else {
            arrFont = window.WriterControl_SupportFontFamilys =
                ctl.getSupportFontFamilys();
        }
        var dc_fontFamily_button = dcPanelBody.find("#dc_button_style_fontname"); //字体样式
        var fontFamilyUlHtml = "";
        arrFont.length && arrFont.forEach(function (obj) {
            if (obj) {
                var styleStr = "font-family:" + obj + ";";
                fontFamilyUlHtml +=
                    "<option class='font-item' style='" + styleStr + "' value='" + obj + "'>" + obj + "</option>";
            }
        });
        dc_fontFamily_button.html(fontFamilyUlHtml);

        //字体大小回填
        var dc_fontSizeUl = dcPanelBody.find("#dc_button_style_fontsize"); //字体大小
        var fontSizeUlHtml = "";
        DATAFONTSIZE.forEach(function (value) {
            if (value) {
                fontSizeUlHtml +=
                    "<option class='font-item' value='" + value + "'>" + value + "</option>";
            }
        });
        dc_fontSizeUl.html(fontSizeUlHtml);


        //背景颜色值修改事件
        jQuery(ctl).find("#dc_button_style_backgroundcolorstring").change(function () {
            var color = jQuery(this).val();
            var dc_backgroundcolorstring_box = dcPanelBody.find("#dc_backgroundcolorstring_box");
            dc_backgroundcolorstring_box.css("background-color", color);
            dc_backgroundcolorstring_box.attr("data-value", color);
        });
        //文本颜色值修改事件
        jQuery(ctl).find("#dc_button_style_colorstring").change(function () {
            var color = jQuery(this).val();
            var dc_colorstring_box = dcPanelBody.find("#dc_colorstring_box");
            dc_colorstring_box.css("background-color", color);
            dc_colorstring_box.attr("data-value", color);
        });

        //按钮的部分Style属性(目前弹框中可以修改的属性)
        var buttonStyle = {
            BackgroundColorString: null,
            ColorString: null,
            Bold: null,
            FontName: null,
            FontSize: null
        };

        //按钮样式属性值回填
        if (options && options.Style) {
            Object.keys(options.Style).forEach((key) => {
                var newKey = key.toLowerCase();
                switch (newKey) {
                    case "backgroundcolorstring":
                        //展示色块赋值
                        var dc_backgroundcolorstring_box = dcPanelBody.find(`#dc_backgroundcolorstring_box`);
                        dc_backgroundcolorstring_box.css("background-color", options.Style[key]);
                        dc_backgroundcolorstring_box.attr("data-value", options.Style[key]);
                        // 颜色选择器赋值
                        var dc_button_style_backgroundcolorstring = dcPanelBody.find(`#dc_button_style_backgroundcolorstring`);
                        dc_button_style_backgroundcolorstring.val(options.Style[key]);
                        break;
                    case "colorstring":
                        if (options.Style[key]) {
                            //展示色块赋值
                            var dc_colorstring_box = dcPanelBody.find(`#dc_colorstring_box`);
                            dc_colorstring_box.css("background-color", options.Style[key]);
                            dc_colorstring_box.attr("data-value", options.Style[key]);
                            // 颜色选择器赋值
                            var dc_button_style_colorstring = dcPanelBody.find(`#dc_button_style_colorstring`);
                            dc_button_style_colorstring.val(options.Style[key]);
                        }
                        break;
                    case "fontname":
                    case "fontsize":
                    case "bold":
                        if (newKey === "bold") {
                            options.Style[key] = (options.Style[key] === true ? "true" : "false");
                        }
                        dc_dialogContainer.find(`#dc_button_style_${newKey}`).val(options.Style[key]);
                        break;
                }
            });
        } else {
            //如果是插入按钮，没有Style属性。则置空选择框
            Object.keys(buttonStyle).forEach((key) => {
                var newKey = key.toLowerCase();
                var styleDom = ctl.ownerDocument.getElementById(`dc_button_style_${newKey}`);
                if (styleDom.nodeName === "SELECT") {
                    styleDom.selectedIndex = -1;
                }
            });
        }


        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, opts);
        // 图片的默认赋值
        dcPanelBody.find("[data-value='img']").each(function () {
            var imgNode = jQuery(this).find("img");
            var _val = jQuery(this).val();
            if (_val) {
                var str = _val;
                if (_val.indexOf("base64,") == -1) {
                    str = "data:image/png;base64," + str;
                    jQuery(this).val(str);
                }
                imgNode.attr("src", str);
            } else {
                // 没有内容，隐藏
                imgNode.hide();
            }
        });
        // 图片的提交
        dcPanelBody
            .find("[data-value='img'] [type='file']")
            .on("change", function () {
                var files = this.files;
                if (files.length == 0) {
                    return;
                }
                var btnNode = jQuery(this).parent();
                var imgNode = btnNode.find("img");
                if (files[0] && files[0].type.slice(0, 5) == "image") {
                    var fileinfo = files[0];
                    var reader = new FileReader();
                    reader.readAsDataURL(fileinfo);
                    reader.onload = function () {
                        var base64 = reader.result;
                        imgNode.attr("src", base64);
                        imgNode.show();
                        // var str = base64.substr(base64.indexOf("base64,") + 7, base64.length);
                        btnNode.val(base64);
                    };
                    reader.onerror = function (error) {
                        console.log(error);
                    };
                }
            });
        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);

            Object.keys(buttonStyle).forEach(key => {
                var newKey = key.toLowerCase();
                var value = dc_dialogContainer.find(`#dc_button_style_${newKey}`).val();
                switch (newKey) {
                    case "backgroundcolorstring":
                        var colorstring = dcPanelBody.find(`#dc_backgroundcolorstring_box`).attr("data-value");
                        buttonStyle[key] = colorstring;
                        break;
                    case "colorstring":
                        var colorstring = dcPanelBody.find(`#dc_colorstring_box`).attr("data-value");
                        buttonStyle[key] = colorstring;
                        break;
                    case "bold":
                        buttonStyle[key] = value === "true";
                        break;
                    default:
                        buttonStyle[key] = value;
                        break;
                }
            });
            _data['Style'] = { ...buttonStyle };
            var dcAttrBox = dcPanelBody.find("#dc_attr-box");
            let Attributes = that.attributeComponents_getAttributeObj(dcAttrBox);
            _data['Attributes'] = Attributes;
            console.log("successFun -> _data", _data);
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("InsertButton", false, { ...options, ..._data });
            } else {
                ctl.SetElementProperties(ele, _data, true);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement("xtextbuttonelement"));
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建二维码属性对话框
     * @param options 二维码属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    QRCodeDialog: function (options, ctl, isInsertMode, ele) {
        if (!options || typeof options != "object") {
            // 当未传入值时
            if (isInsertMode == true) {
                options = {};
            } else {
                ele = ctl.CurrentElement("xtexttdbarcodeelement");
                if (ele == null) {
                    return false;
                }
                options = ctl.GetElementProperties(ele);
            }
        }

        if (options == null) {
            return false;
        }
        var QRCodeHtml = `
                    <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">编号：</span>
                    <input type="text" class="dc_full" name="ID" data-text="ID">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_blockelement">
                    <div>文本：</div>
                    <div>
                        <textarea data-text="Text"></textarea>
                    </div>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">宽度：</span>
                    <input type="number" class="dc_full" name="Width" data-text="Width">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">高度：</span>
                    <input type="number" class="dc_full" name="Height" data-text="Height">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">类型：</span>
                    <select data-text="BarcodeStyle" class="dc_full">
                        <option value="QR">QR</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">纠错能力：</span>
                    <select  data-text="ErroeCorrectionLevel" class="dc_full">
                        <option value="L">L:7%的字码可被修正</option>
                        <option value="M" selected>M:15%的字码可被修正</option>
                        <option value="Q">Q:25%的字码可被修正</option>
                        <option value="H">H:30%的字码可被修正</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">可见性表达式：</span>
                    <input data-text="VisibleExpression" class="dc_full" type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "二维码属性",
            bodyHeight: 420,
            bodyClass: "QRCodeElement",
            bodyHtml: QRCodeHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        WriterControl_Dialog.appendValueBindingDiv(dcPanelBody);
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });

        GetOrChangeData(dcPanelBody, opts);
        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            // console.log("successFun -> _data", _data)
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("InsertTDBarcodeElement", false, _data);
            } else {
                ctl.SetElementProperties(ele, _data, true);
            }
            //[DUWRITER5_0-3762] 20241101 lxy 修改EventDialogChangeProperties事件在插入时也触发
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 创建条形码属性对话框
     * @param options 条形码属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    BarCodeDialog: function (options, ctl, isInsertMode, ele) {
        if (!options || typeof options != "object") {
            // 当未传入值时
            if (isInsertMode == true) {
                options = {};
            } else {
                ele = ctl.CurrentElement("xtextnewbarcodeelement");
                if (ele == null) {
                    return false;
                }
                options = ctl.GetElementProperties(ele);
            }
        }
        if (options == null) {
            return false;
        }
        console.log(options, '===========options');

        var BarCodeHtml = `
                    <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">编号：</span>
                    <input type="text" class="dc_full" name="ID" data-text="ID">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">名称：</span>
                    <input type="text" class="dc_full" name="Name" data-text="Name">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">文本内容：</span>
                    <input type="text" class="dc_full" name="Text" data-text="Text">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">宽度：</span>
                    <input type="number" class="dc_full" name="Width" data-text="Width">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">高度：</span>
                    <input type="number" class="dc_full" name="Height" data-text="Height">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">条码样式：</span>
                    <select id="dc_BarcodeStyle" data-text="BarcodeStyle" class="dc_full">
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">文本对齐方式：</span>
                    <select data-text="TextAlignment" class="dc_full">
                        <option value="Near">左对齐</option>
                        <option value="Center" selected="selected">居中对齐</option>
                        <option value="Far">右对齐</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">可见性表达式：</span>
                    <input data-text="VisibleExpression" class="dc_full" type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
            </div>
            <div class="dcBody-content">
                <label>
                    <input type="checkbox" name="ShowText" data-text="ShowText" checked="checked">
                    <span class="dcTitle-text">是否显示文本</span>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "条形码属性",
            bodyHeight: 360,
            bodyClass: "BarcodeElement",
            bodyHtml: BarCodeHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var BarcodeStyleSelect = dcPanelBody.find("select#dc_BarcodeStyle");
        WriterControl_Dialog.appendValueBindingDiv(dcPanelBody);
        var BarcodeStyleArr = [
            { Text: "UPCA" },
            { Text: "UPCE" },
            { Text: "SUPP2" },
            { Text: "SUPP5" },
            { Text: "EAN13" },
            { Text: "EAN8" },
            { Text: "Interleaved2of5" },
            { Text: "I2of5" },
            { Text: "Standard2of5" },
            { Text: "Code39" },
            { Text: "Code39Extended" },
            { Text: "Code93" },
            { Text: "Codabar" },
            { Text: "PostNet" },
            { Text: "BOOKLAND" },
            { Text: "ISBN" },
            { Text: "JAN13" },
            { Text: "MSI_Mod10" },
            { Text: "MSI_2Mod10" },
            { Text: "MSI_Mod11" },
            { Text: "MSI_Mod11_Mod10" },
            { Text: "Modified_Plessey" },
            { Text: "CODE11" },
            { Text: "USD8" },
            { Text: "UCC12" },
            { Text: "UCC13" },
            { Text: "LOGMARS" },
            { Text: "Code128A" },
            { Text: "Code128B" },
            { Text: "Code128C", Selected: true },
        ];
        var BarcodeStyleSelectHtml = "";
        for (var i = 0; i < BarcodeStyleArr.length; i++) {
            var style_txt = BarcodeStyleArr[i].Text;
            var optHtml = "<option value='" + style_txt + "'";
            if (BarcodeStyleArr[i].Selected == true) {
                optHtml += " selected='selected'";
            }
            optHtml += ">" + style_txt + "</option>";
            BarcodeStyleSelectHtml += optHtml;
        }
        BarcodeStyleSelect.html(BarcodeStyleSelectHtml);
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, opts);
        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            console.log("successFun -> _data", _data);
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("InsertBarcodeElement", false, _data);
            } else {
                ctl.SetElementProperties(ele, _data, true);
            }
            //[DUWRITER5_0-3762] 20241101 lxy 修改EventDialogChangeProperties事件在插入时也触发
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 创建字体选择对话框
     * @param options 字体属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    FontSelectionDialog: function (options, ctl, isInsertMode) {
        if (!options || typeof options != "object") {
            options = ctl.getFontObject();
        }
        if (options == null) {
            return false;
        }
        var arrFont = [];
        if (
            window.WriterControl_SupportFontFamilys &&
            window.WriterControl_SupportFontFamilys.length
        ) {
            arrFont = window.WriterControl_SupportFontFamilys;
        } else {
            arrFont = window.WriterControl_SupportFontFamilys =
                ctl.getSupportFontFamilys();
        }
        var fontFormatHtml = `
                <div class="dc_font-content-dialog" id="dc_font-content-dialog">
                   <div class="dc_font-box">
                        字体(F)：
                        <input type="text" data-text="fontFamily">
                        <ul class="dc_ul-content" id="dc_fontFamilyUl"></ul>
                   </div>
                   <div class="dc_font-box">
                        大小(S)：
                        <input type="text" data-text="fontSize">
                        <ul class="dc_ul-content" id="dc_fontSizeUl"></ul>
                   </div>
                </div>  
                <div id="font-check-box">
                    <div class="dc_font-style-content-dialog dc_Box">
                        <h6 class="dc_title">字形</h6>
                        <div>
                            <div class="dc_Body-V"> <label> <input type="checkbox"  data-text="Bold"  id="Bold">粗体</label></div>
                            <div class="dc_Body-V"> <label> <input type="checkbox"  data-text="Italic" id="Italic">斜体</label></div>
                        </div>
                    </div>  
                    <div class="dc_font-style-content-dialog dc_Box">
                        <h6 class="dc_title">效果</h6>
                        <div>
                            <div class="dc_Body-V"> <label> <input type="checkbox"  data-text="Underline"  id="Underline">下划线(U)</label></div>
                            <div class="dc_Body-V"> <label> <input type="checkbox"  data-text="Strikeout"  id="Strikeout">删除线(k)</label></div>
                        </div>
                    </div> 
                </div>
            `;
        var dialogOptions = {
            title: "选择字体对话框",
            bodyHeight: 400,
            bodyClass: "fontStyleElement",
            bodyHtml: fontFormatHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, opts);

        // 字号大小
        var dataFontSize = DATAFONTSIZE;

        var dc_fontFamilyUl = dcPanelBody.find("ul#dc_fontFamilyUl"); //字体样式
        var fontFamilyUlHtml = "";
        arrFont.forEach(function (font) {
            if (font) {
                var styleStr = "font-family:" + font + ";";
                if (font == opts.fontFamily) {
                    styleStr += "background:#0078D7;color:#FFFFFF;";
                }
                fontFamilyUlHtml +=
                    "<li class='font-item' style='" + styleStr + "'>" + font + "</li>";
            }
        });
        dc_fontFamilyUl.html(fontFamilyUlHtml);
        var dc_fontSizeUl = dcPanelBody.find("ul#dc_fontSizeUl"); //字体大小
        var fontSizeUlHtml = "";
        dataFontSize.forEach(function (value) {
            if (value) {
                var styleStr = "";
                if (value == opts.fontSize) {
                    styleStr += "background:#0078D7;color:#FFFFFF;";
                }
                fontSizeUlHtml +=
                    "<li class='font-item' style='" + styleStr + "'>" + value + "</li>";
            }
        });
        dc_fontSizeUl.html(fontSizeUlHtml);
        dcPanelBody
            .find("ul#dc_fontFamilyUl li,ul#dc_fontSizeUl li")
            .click(function () {
                var _fontbox = jQuery(this).parents(".dc_font-box");
                _fontbox.find("input").val(jQuery(this).text());
                jQuery(this)
                    .css({
                        background: "#0078D7",
                        color: "#FFFFFF",
                    })
                    .siblings("li")
                    .css({
                        background: "none",
                        color: "black",
                    });
            });
        function successFun() {
            // var dc_dialogContainer = jQuery(ctl).children('#dc_dialogContainer');
            // var dcPanelBody = jQuery(dc_dialogContainer).find('#dcPanelBody');
            var _data = GetOrChangeData(dcPanelBody);
            ctl.setFontObject(_data);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.getFontObject();
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 创建输入域属性对话框
     * @param options 输入域属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    //会导致报错先不加

    InputFieldDialog: function (options, ctl, isInsertMode) {
        var ele = ctl.CurrentInputField();
        if (options == null) {
            options = {};
        }
        if (!isInsertMode) {
            if (ele == null) {
                return false;
            }
            options = ctl.GetElementProperties(ele);
        }
        var InnerListSourceName = "";
        if (options && options.InnerListSourceName) {
            var InnerListSourceName = options.InnerListSourceName;
        }

        //必须保证输入域校验有一种校验方式，默认是Text
        if (options && options.ValidateStyle == null) {
            options['ValidateStyle'] = {
                ValueType: "Text"
            };
        } else {
            // 20240329 lixinyu 修复校验选择时间校验不设置时间,再次获取时间变成1980/1/1问题(DUWRITER5_0-2165)
            var ValueType = options && options.ValidateStyle && options.ValidateStyle.ValueType || '';
            if (ValueType && ValueType === "DateTime") {
                var DateTimeMaxValue = options.ValidateStyle.DateTimeMaxValue || '';
                var DateTimeMinValue = options.ValidateStyle.DateTimeMinValue || '';
                if (DateTimeMaxValue === "0001/1/1 上午12:00:00") {
                    options.ValidateStyle.DateTimeMaxValue = null;
                }
                if (DateTimeMinValue === "0001/1/1 上午12:00:00") {
                    options.ValidateStyle.DateTimeMinValue = null;
                }
            }
        }
        var InputFieldHTML = `
    <!-- 选项卡 -->
    <div class="dc_InputFieldContent">
       <p id="dc_InputFieldContentButtonBox" class="dc_buttonBox">
            <span showDomId="dc_tab1" class="dc_tabButton dc_active">常规</span>
            <span showDomId="dc_tab2" class="dc_tabButton">格式</span>
            <span showDomId="dc_tab3" class="dc_tabButton">校验</span>
            <span  showDomId="dc_tab4" class="dc_tabButton">其他</span>
        </p>
      
    <div class="dc_InputField_tab_box" >
        <!-- 第一个  -->
        <div id="dc_tab1" class="dc_tab">
            <div class="dc_Box">
                <h6 class="dc_title">基本属性</h6>
                   <div class="dc_tab1Content">
                        <label>
                            <span> *编号(ID)：</span>
                            <input data-text="ID" placeholder="" type="text"></input>
                        </label>
                        <label>
                            <span>*名称(Name)：</span>
                            <input data-text="Name" type="text"></input>
                        </label>

                        <label>
                            <span>背景文字：</span>
                            <input data-text="BackgroundText"  type="text"></input>
                        </label>
                        <label>
                            <span>提示文字：</span>
                            <input data-text="ToolTip" type="text"></input>
                        </label>
                        <label>
                            <span>边框：</span>
                            <div class="dc_borderBox"></input>
                                <label class="dc_borderBox_label" >
                                    <span>(左):</span>
                                    <input   data-text="StartBorderText" type="text"></input>
                                </label>
                                <label class="dc_borderBox_label" >
                                    <span>(右):</span>
                                    <input data-text="EndBorderText"  type="text"></input>
                                </label>
                            </div>
                        </label>
                        <label>
                            <span>内容对齐方式：</span>
                            <select data-text="Alignment">
                                <option value="Near">Near</option>
                                <option value="Center">Center</option>
                                <option value="Far">Far</option>
                            </select>
                        </label>
                        <label class="dc_SpecifyWidth_label">
                            <span>固定宽度<span class="dc_SpecifyWidth_title" title="若背景文字、边框、标签文本、单位文本的宽度超出固定宽度，则固定宽度无效">?</span>：</span>
                            <input data-text="SpecifyWidth"  class="dc_SpecifyWidth_input dc_input_number_data_model" type="number"></input>
                            <span class="dc-text-model-SpecifyWidth" dc-text-model="SpecifyWidth"></span>
                        </label>
                        <label>
                            <span>焦点快捷键：</span>
                            <select data-text="MoveFocusHotKey">
                                <option value="None">None</option>
                                <option value="Default">Default</option>
                                <option value="Tab">Tab</option>
                                <option value="Enter">Enter</option>
                            </select>
                        </label>
                        <label>
                            <span>标签文本：</span>
                            <input data-text="LabelText" type="text"></input>
                        </label>
                        <label>
                            <span>单位文本：</span>
                            <input data-text="UnitText" type="text"></input>
                        </label>
                       
                        <label>
                            <span>简单级联：</span>
                            <input data-text="DefaultEventExpression" type="text"></input>
                        </label>
                        <label class="dc_EditorActiveModeButton_label" >
                            <span>激活模式：</span>
                            <p id="dc_EditorActiveModeButton" data-text="EditorActiveMode" > None</p>
                        </label>
                         <label>
                            <span>高亮度状态：</span>
                            <select data-text="EnableHighlight">
                                <option value="Default">默认</option>
                                <option value="Enabled">允许</option>
                                <option value="Disabled">禁止</option>
                            </select>
                        </label>
                          <label>
                            <span>打印显示：</span>
                            <select data-text="PrintVisibility">
                                <option value="Visible">Visible</option>
                                <option value="Hidden">Hidden</option>
                                <option value="None">None</option>
                            </select>
                        </label>
                        <label class="dc_BorderVisible_label">
                            <span>边框是否可见:</span>
                            <select  data-text="BorderVisible" >
                                <option value="Default">默认</option>
                                <option value="Visible">可见</option>
                                <option value="Hidden">隐藏</option>
                                <option value="AlwaysVisible">始终可见</option>
                            </select>
                        </label>
                        <label label >
                            <span>标签单位加粗：</span>
                            <select data-text="LableUnitTextBold">
                                <option value="Default">Default</option>
                                <option value="True">True</option>
                                <option value="False">False</option>
                            </select>
                        </label >
                   </div>
                </div>


                   <div class="dc_Box">
                        <h6 class="dc_title">权限属性：</h6>
                        <div class="dc_tab2Content" >
                            <label>
                                <input type="checkbox" data-text="HiddenPrintWhenEmpty">为空时打印隐藏</input>
                            </label>
                           <label class="dc_input_MaxInputLength_label" >
                                <span>输入最大字符数:</span>
                                <input class="dc_tab2Content_label_inp" data-text="MaxInputLength" type="number" />
                            </label>
                            <label  class="dc_input_UserEditable_label">
                                <input type="checkbox" data-text="UserEditable" name="running" checked="true">是否可以直接编辑修改内容</input>
                            </label>
                             <label  class="dc_input_ContentReadonly_label">
                                <span  >是否只读:</span>
                                <select class="dc_tab2Content_label_inp" id="dc_ContentReadonly"  data-text="ContentReadonly" >
                                    <option value="Inherit">继承父级元素</option>
                                    <option value="True">是</option>
                                    <option value="False">否</option>
                                </select>
                            </label>
                            <label class="dc_input_Deleteable_label">
                                <input  type="checkbox" data-text="Deleteable" name="running" checked="true">是否允许被删除</input>
                            </label>
                           
                            <label  class="dc_input_ViewEncryptType_label">
                                <span>加密显示:</span>
                                <select class="dc_tab2Content_label_inp" data-text="ViewEncryptType" >
                                    <option value="None">不加密</option>
                                    <option value="Partial">部分加密</option>
                                    <option value="Both">全部加密</option>
                                </select>
                            </label>
                        </div>
                    </div>

                    <div id="dc_ValueBinding" class="dc_Box">
                        <h6 class="dc_title">赋值属性：</h6>
                        <div class="dc_tab3Content" >
                            <label>
                                <span>数据源名称：</span>
                                <input  id="dc_DataSource" data-text="Datasource" type="text" />
                            </label>
                            <label >
                                <span>绑定路径：</span>
                                <input  id="dc_BindingPath" data-text="BindingPath" type="text" />
                            </label>
                            <label>
                                <span>Text绑定路径：</span>
                                <input  id="dc_BindingPathForText" type="text" data-text="BindingPathForText" />
                            </label>
                            <label>
                                <span>执行状态:</span>
                                <select id="dc_ProcessState" data-text="ProcessState" >
                                    <option value="Always">总是执行</option>
                                    <option value="Once">只执行一次</option>
                                    <option value="Never">不执行</option>
                                </select>
                            </label>
                        </div>
                    </div>

                    <div class="dc_Box">
                        <h6 class="dc_title">颜色属性：</h6>
                        <div id="dc_colorContainer" >
                            <div class="dc_TextColor_container">
                                <span>文字颜色：</span>
                                <label class="dc_input_TextColor_label" >
                                    <div data-value="" id="dc_TextColor_box"></div>
                                    <input id="dc_TextColor" dc-target-id="dc_TextColor_box" data-text="TextColor" type="color" />
                                </label>
                            </div>
                            <div class="dc_BackgroundTextColor_container">
                                <span>背景文字颜色：</span>
                                <label class="dc_input_BackgroundTextColor_label" >
                                    <div data-value="" id="dc_BackgroundTextColor_box"></div>
                                    <input id="dc_BackgroundTextColor" dc-target-id="dc_BackgroundTextColor_box" data-text="BackgroundTextColor"  type="color"/>
                                </label>
                            </div>
                            <div class="dc_BackgroundColorString_container" >
                                <span>背景颜色：</span>
                                <label class="dc_input_BackgroundColorString_label">
                                    <div data-value="" id="dc_BackgroundColorString_box"></div>
                                    <input id="dc_BackgroundColorString" dc-target-id="dc_BackgroundColorString_box"  data-text="BackgroundColorString"  type="color"  />
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

        <!-- 第二个  -->
        <div id="dc_tab2" style="display: none;" class="dc_tab">
            <label>
                <input class="dc_tab2-radio-ipt" type="radio" attrId="Text" name="myRadioGroup">纯文本元素(Text)</input>
            </label>
            <label>
                <input id="dc_InnerEditStyle_DropDownList" class="dc_tab2-radio-ipt " attrId="DropdownList" type="radio" name="myRadioGroup" id="DropdownList">
                下拉列表方式(DorpDownList)</input>
            </label>
            <div id="dc_dropDownList_formbox" class="dc_Box" >
                <h6 class="dc_title">列表方式</h6>
                <div class="dc_Check-box">
                    <label class="dc_dc_Check_box_label">
                        <input type="checkbox" name="runnings" data-text="InnerMultiSelect" id="InnerMultiSelect">是否允许多选</input>
                    </label>
                    <label class="dc_dc_Check_box_label">
                        <input type="checkbox" name="runnings" data-text="DynamicListItems" id="DynamicListItems">动态下拉列表</input>
                    </label>
                    <label class="dc_dc_Check_box_label">
                        <input type="checkbox" name="runnings" data-text="RepulsionForGroup" id="RepulsionForGroup">分组互斥</input>
                    </label>
                    <label class="dc_input_ListValueFormatString_label dc_dropDownList_formbox_label">
                    <span>列表格式化:</span>
                        <input type="text" data-text="ListValueFormatString" name="ListValueFormatString" value="" list="dc_ListValueFormatString" autocomplete="off">
                        <datalist id="dc_ListValueFormatString">
                            <option value=""></option>
                            <option value="有[includelist],无[excludelist]"></option>
                        </datalist>
                    </label>
                    <label class="dc_dc_Check_box_label_flex dc_dropDownList_formbox_label">
                        <span>列表项目分割字符:</span>
                        <select id="dc_ListValueSeparatorChar" data-text="ListValueSeparatorChar">
                            <option value=",">,</option>
                            <option value="、">、</option>
                            <option value="|">|</option>
                            <option value="#">#</option>
                            <option value="*">*</option>
                        </select>
                    </label>
                    <label class="dc_StaticSelection_label dc_dropDownList_formbox_label">
                        <span>静态选择项内容：</span>
                        <input type="text" id="dc_StaticSelection" readonly="readonly" name="runnings">
                        <button id="dc_browseTextTableContent" name="runnings">浏览</button>
                    </label>
                </div>
            </div>
            
             <label>
                <input class="dc_tab2-radio-ipt" type="radio" name="myRadioGroup" attrId="DateTime">日期时间格式(DataTime)</input>
            </label>
            <label>
                <input class="dc_tab2-radio-ipt" type="radio" name="myRadioGroup" attrId="Date">日期格式(Date)</input>
            </label>
            <label>
                <input class="dc_tab2-radio-ipt" type="radio" name="myRadioGroup" attrId="Numeric">数字类型(Numeric)</input>
            </label>
            <label>
                <input class="dc_tab2-radio-ipt" type="radio" name="myRadioGroup" attrId="Time">时间格式(Time)</input>
            </label>
            <label>
                <input class="dc_tab2-radio-ipt" type="radio" name="myRadioGroup" attrId="DateTimeWithoutSecond">日期时间格式(不含秒)(DataTime)</input>
            </label>
            <div > 
                <div class="dc_Box dc_DisplayFormat_box" >
                    <h6 class="dc_title">输出格式：</h6>
                    <div class="dc_DisplayFormat_Style_box" >
                        <div class="dc_DisplayFormat_Style_box_center" >
                            <span>格式类型:</span>
                            <input class="dc_selection" data-text="DisplayFormat.Style" type="text" id="dc_DisplayFormat">
                            <ul id="dc_UI-1" ></ul>
                        </div>
                        <div class="dc_DisplayFormat_Style_box_center">
                            <span >格式:</span>
                            <input class="dc_selectionRight" data-text="DisplayFormat.Format" type="text" id="dc_selectionRight">
                            <ul id="dc_UI-2" ></ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
          

        <!-- 第3个  -->
        <div id="dc_tab3"  style="display: none;" class="dc_tab">
            <div class="dc_tab3_content">
                <label class="dc_tab3_content_Required_label">
                    <input type="checkbox" data-text="Required">是否必填</input>
                </label>
                <label class="dc_tab3_content_CustomMessage_label" >
                    <span>错误提示：</span>
                    <input data-text="CustomMessage" type="text"  />
                </label>
                <label class="dc_tab3_content_ExcludeKeywords_label" >
                    <span>违禁关键字：</span> 
                    <input data-text="ExcludeKeywords" type="text" />
                </label>
                <label class="dc_tab3_content_IncludeKeywords_label" >
                    <span>允许关键字：</span> 
                    <input data-text="IncludeKeywords" type="text" />
                </label>
            </div>
            <div class="dc_tab3_text_content">
                <input class="dc_radio-ipt-choose"  type="radio" name="CheckRule2" attrId="Text">
                    纯文本格式校验</input>
                <div class="dc_Box dc_changeDisabled">
                    <span>最小长度：</span>
                    <input type="number" data-text="MinLength"  name="runningA"></input>
                    <br/>
                    <span>最大长度：</span>
                    <input type="number" data-text="MaxLength" name="runningA"></input>
                </div>
            </div>
            <div class="dc_tab3_number_content">
                <input class="dc_radio-ipt-choose" type="radio" name="CheckRule2" attrId="Numeric"> 数值格式校验</input>
                    <div class="dc_Box dc_changeDisabled">
                            <input type="checkbox" id="DC_ValiNumber_CheckMinValue" data-text="CheckMinValue"/>
                            <span>最小值：</span>
                            <input type="number" data-text="MinValue" name="runningA"></input>
                        <br/>
                            <input type="checkbox" id="DC_ValiNumber_CheckMaxValue" data-text="CheckMaxValue"/>
                            <span>最大值：</span>
                            <input type="number" data-text="MaxValue" name="runningA"></input>
                        <br/>
                            <input type="checkbox" data-text="CheckDecimalDigits"/>
                            <span>最大小数位数：</span>
                            <input type="number" data-text="MaxDecimalDigits" name="runningA" ></input>
                        <br/>
                            <input type="checkbox" id="DCNumericInteger"/>
                            <span>只能输入整数</span>
                    </div>
            </div>
            <div class="dc_tab3_date_content" >
                <input class="dc_radio-ipt-choose" type="radio" name="CheckRule2" attrId="DateTime"> 日期时间格式校验</input>
                    <div class="dc_Box dc_changeDisabled" >
                       <input type="checkbox" data-text="CheckMinValue" />
                       <span>不得早于：</span>
                       <input type="datetime-local" data-text="DateTimeMinValue" name="runningB" id="dc_DateTimeMinValue">
                       <br/>
                       <input type="checkbox" data-text="CheckMaxValue" />
                       <span>不得晚于：</span>
                       <input type="datetime-local"  data-text="DateTimeMaxValue" name="runningB" id="dc_DateTimeMaxValue">
                    </div>
            </div>
            <div class="dc_tab3_reg_content" >
                <input class="dc_radio-ipt-choose" type="radio" name="CheckRule2" attrId="RegExpress"> 正则表达式</input>
                    <div class="dc_Box dc_changeDisabled" >
                        <input data-text="RegExpression" name="runningA" type="text" /> 
                    </div>
            </div>
        </div>

        <!-- 第4个  -->
        <div id="dc_tab4"  style="display: none;" class="dc_tab">
            <div class="dc_Box">
                <h6 class="dc_title">表达式：</h6>
                <label class="dc_input_ValueExpression_label" >
                    <span>计算表达式：</span>
                    <input data-text="ValueExpression"   type="text">
                </label>
                <label class="dc_input_VisibleExpression_label" >
                    <span>可见性表达式：</span>
                    <input data-text="VisibleExpression" type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
                <label class="dc_input_PropertyExpressions_label" >
                    <span>属性表达式：</span>
                    <input id="dc_PropertyExpressions_show_input" readonly="readonly" type="text">
                    <button id="dc_PropertyExpressionsButton">打开</button>
                </label>
            </div>
            <div class="dc_Box">
                <h6 class="dc_title">内容复制来源：</h6>
                <div id="dc_CopySource">
                    <label class="dc_input_SourceID_label" >
                        <span >复制来源：</span>
                        <input  data-text="SourceID" type="text">
                    </label>
                    <label class="dc_input_SourcePropertyName_label">
                        <span >来源属性：</span>
                        <input data-text="SourcePropertyName"  type="text">
                    </label>
                    <label class="dc_input_DescPropertyName_label">
                        <span >目标属性：</span>
                        <input data-text="DescPropertyName"  type="text">
                    </label>
                </div>
            </div>

            <div class="dc_Box">
                <label class="dc_input_Attributes_label" >
                    <span >自定义属性：</span>
                    <input type="text"  id="dc_Attributes"  readonly="readonly">
                    <button id="dc_browsess" name="">浏览</button>
                </label>

                <label class="dc_input_AcceptChildElementTypes_label" >
                    <span >可包含的内容：</span>
                    <input type="text"  data-text="AcceptChildElementTypes" id="dc_AcceptChildElementTypesInput" readonly="readonly">
                    <button id="dc_AcceptChildElementTypesButton" name="">浏览</button>
                </label>
            </div>

        </div>
          
    </div>
                            `;
        var dialogOptions = {
            title: "文字输入域属性",
            bodyHeight: 400,
            dialogContainerBodyWidth: 504,
            bodyClass: "InputFieldElement",
            bodyHtml: InputFieldHTML,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        domShowAndHide(); //默认渲染
        function domShowAndHide() {
            let showDomId = jQuery(ctl)
                .find("#dc_InputFieldContentButtonBox > .dc_tabButton.dc_active")
                .attr("showDomId");
            jQuery(ctl)
                .find("#" + showDomId)
                .siblings()
                .hide();
            jQuery(ctl)
                .find("#" + showDomId)
                .show();
        }

        //给输出格式填充数据
        //列表数据
        var lbsj = LBSJ;
        //循环创建格式选项卡里的li标签
        var oUl = ctl.ownerDocument.getElementById("dc_UI-1");
        var oUlRight = ctl.ownerDocument.getElementById("dc_UI-2");
        //循环创建li标签
        for (let i = 0; i < lbsj.length; i++) {
            var oLi = ctl.ownerDocument.createElement("li");
            oLi.id = lbsj[i].id;
            oLi.className = "sss";
            oLi.style = "line-height:20px;padding:0 10px;";
            oLi.innerHTML = lbsj[i].text;
            oUl.appendChild(oLi);
        }

        // 给输出格式左侧列表添加点击事件，填充数据
        jQuery(ctl).find("#dc_UI-1 li").on("click", function () {
            if (this.innerHTML == "None") {
                jQuery(ctl).find("#dc_UI-2").hide();
            } else {
                jQuery(ctl).find("#dc_UI-2").show();
            }
            jQuery(ctl).find(".dc_selection").val(this.innerHTML);
            jQuery(ctl).find(".dc_selectionRight").val("");
            jQuery(ctl).find(".sss").css("background", "none");
            jQuery(ctl).find(".sss").css("color", "black");
            this.style.color = "#ffffff"; /*点击的*/
            this.style.backgroundColor = "#4098ff"; /*点击的*/
            // this.style = "color:#000000;backgroundColor:#d7e4f2;"
            oUlRight.innerHTML = "";
            for (let i = 0; i < lbsj.length; i++) {
                if (this.id == lbsj[i].id) {
                    var lbsj2 = lbsj[i].Child;
                    for (let i = 0; i < lbsj2.length; i++) {
                        var oLi = ctl.ownerDocument.createElement("li");
                        oLi.className = "sss2";
                        oLi.id = lbsj2[i].id;
                        oLi.style = "line-height:20px;padding:0 10px;";
                        oLi.innerHTML = lbsj2[i].text;
                        oUlRight.appendChild(oLi);
                    }
                }
            }
        });

        // 输出格式第二列ui列表的点击操作
        jQuery(ctl).find("#dc_UI-2").delegate("li", "click", function () {
            jQuery(ctl).find(".sss2").css({ "background": "none", "color": "black" });
            this.style.cssText = "color:#ffffff;background-color:#4098ff;line-height:20px;padding:0px 10px; ";
            jQuery(ctl).find(".dc_selectionRight").val(this.innerHTML);
        });

        //输入域的输入方式的点击事件，用于对下拉方式的内部属性禁用
        jQuery(ctl).find(".dc_tab2-radio-ipt").click(function () {
            let dropDownListFormList = jQuery(ctl).find("#dc_dropDownList_formbox").find("input, select, button");
            if (this.getAttribute('attrId') === "DropdownList") {
                dropDownListFormList.removeAttr("disabled");
            } else {
                dropDownListFormList.attr("disabled", true);
            }
        });

        //校验表单禁用逻辑-tab3互斥单选
        jQuery(ctl).find(".dc_radio-ipt-choose").click(function (e) {
            let disableChangeElement =
                ctl.ownerDocument.getElementsByClassName("dc_changeDisabled");
            for (var i = 0; i < disableChangeElement.length; i++) {
                let childrenNode = disableChangeElement[i].children;
                for (var j = 0; j < childrenNode.length; j++) {
                    if (disableChangeElement[i].previousElementSibling != this) {
                        jQuery(childrenNode[j]).attr("disabled", true);
                    } else {
                        jQuery(childrenNode[j]).removeAttr("disabled");
                    }
                }
            }
        });

        //根据options渲染页面所需数据
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        //输入域属性回显到对话框
        if (options) {
            //先将不需要转换的简单数据进行赋值
            GetOrChangeData(dcPanelBody, options);

            //校验tab渲染options.ValidateStyle
            if (options.ValidateStyle) {
                var dc_tab3 = jQuery(ctl).find("#dc_tab3");
                GetOrChangeData(dc_tab3, options.ValidateStyle);//先将值赋值给对话框
                //其他需要特殊处理的校验逻辑
                if (options.ValidateStyle.ValueType) {
                    //对数值校验进行转换
                    if (options.ValidateStyle.ValueType === 'Integer') {
                        options.ValidateStyle.ValueType = "Numeric";
                        var DCNumericInteger = jQuery(ctl).find('#DCNumericInteger');
                        DCNumericInteger.attr('checked', true);
                    }

                    //选中一个校验格式，并禁用其他校验的内容输入
                    if (options.ValidateStyle.ValueType) {
                        var valuetype = options.ValidateStyle.ValueType;
                        var currentValidateStyleDom = jQuery(ctl).find(`#dc_tab3 input[name=CheckRule2][attrId=${valuetype}]`);
                        currentValidateStyleDom.click();//获取到对应的校验格式并做选中状态

                        //如果是时间校验，则需要转换日期时间格式校验
                        if (options.ValidateStyle.ValueType === "DateTime") {
                            if (options.ValidateStyle.DateTimeMaxValue) {
                                ctl.ownerDocument.getElementById("dc_DateTimeMaxValue").value =
                                    RangeDate(options.ValidateStyle.DateTimeMaxValue);
                            }
                            if (options.ValidateStyle.DateTimeMinValue) {
                                ctl.ownerDocument.getElementById("dc_DateTimeMinValue").value =
                                    RangeDate(options.ValidateStyle.DateTimeMinValue);
                            }
                        }
                    }
                }
            }

            //绑定路径
            if (options && options.ValueBinding) {
                var dc_ValueBinding = jQuery(ctl).find("#dc_ValueBinding");
                GetOrChangeData(dc_ValueBinding, options.ValueBinding);
            }

            //自定义属性文本值
            if (options && options.Attributes && Object.keys(options.Attributes)) {
                jQuery(ctl).find("#dc_Attributes").val(Object.keys(options.Attributes).length + "items");
            }
            //静态属性文本值
            if (options && options.ListItems) {
                jQuery(ctl).find("#dc_StaticSelection").val(options.ListItems.length + "items");
            }

            //输入校验类型
            var innerEditStyle = options.InnerEditStyle ? options.InnerEditStyle : "Text";//设置输入域默认类型
            var currentInnerEditStyleDom = jQuery(ctl).find(`#dc_tab2>label>input[type="radio"]`);
            //根据属性InnerEditStyle复显输入域的输入类型
            for (var i = 0; i < currentInnerEditStyleDom.length; i++) {
                var itemAttrId = currentInnerEditStyleDom[i].getAttribute('attrid').toLowerCase() || '';
                if (itemAttrId === innerEditStyle.toLowerCase()) {
                    currentInnerEditStyleDom[i].click();
                }
            }

            //输出格式
            if (options.DisplayFormat) {
                if (options.DisplayFormat.Style) {
                    let liList = jQuery(ctl).find("#dc_UI-1 li");
                    for (var i = 0; i < liList.length; i++) {
                        if (liList[i].innerHTML === options.DisplayFormat.Style) {
                            liList[i].click();
                        }
                    }
                    if (options.DisplayFormat.Format) {
                        jQuery(ctl)
                            .find('[data-text="DisplayFormat.Format"]')
                            .val(options.DisplayFormat.Format);
                        let li2List = jQuery(ctl).find("#dc_UI-2 li");
                        for (var i = 0; i < li2List.length; i++) {
                            if (li2List[i].innerHTML === options.DisplayFormat.Format) {
                                li2List[i].click();
                            }
                        }
                    }
                }
            } else {
                jQuery(ctl).find("#dc_UI-2").hide();
            }



            //复制来源值复显
            if (options && options.CopySource) {
                var dc_CopySource = jQuery(ctl).find('#dc_CopySource');
                GetOrChangeData(dc_CopySource, options.CopySource);
            }

            //是否只读
            jQuery(ctl).find("#dc_tab1 #dc_ContentReadonly").attr("checked", options.ContentReadonly && options.ContentReadonly == "True");
        }

        //初始化背景颜色
        if (options && options.Style && options.Style.BackgroundColorString) {
            //input赋值
            jQuery(ctl).find("#dc_BackgroundColorString").val(options.Style.BackgroundColorString);
            //颜色框
            jQuery(ctl).find("#dc_BackgroundColorString_box").css("background-color", options.Style.BackgroundColorString);
            jQuery(ctl).find("#dc_BackgroundColorString_box").attr("data-value", options.Style.BackgroundColorString);
        }

        //初始化背景文字颜色
        if (options && options.BackgroundTextColor) {
            //input赋值
            jQuery(ctl).find("#dc_BackgroundTextColor").val(options.BackgroundTextColor);
            //颜色框
            jQuery(ctl).find("#dc_BackgroundTextColor_box").css("background-color", options.BackgroundTextColor);
            jQuery(ctl).find("#dc_BackgroundTextColor_box").attr("data-value", options.BackgroundTextColor);
        }
        //初始化文本颜色
        if (options && options.TextColor) {
            //input赋值
            jQuery(ctl).find("#dc_TextColor").val(options.TextColor);
            //颜色框
            jQuery(ctl).find("#dc_TextColor_box").css("background-color", options.TextColor);
            jQuery(ctl).find("#dc_TextColor_box").attr("data-value", options.TextColor);
        }
        //背景颜色改变
        jQuery(ctl).find("#dc_BackgroundColorString").change(function () {
            var targetId = this.getAttribute("dc-target-id");
            var color = this.value;
            jQuery(ctl).find("#" + targetId).css("background-color", color);
            jQuery(ctl).find("#" + targetId).attr("data-value", color);

        });
        //文本颜色改变
        jQuery(ctl).find('#dc_TextColor').change(function () {
            var targetId = this.getAttribute("dc-target-id");
            var color = this.value;
            jQuery(ctl).find("#" + targetId).css("background-color", color);
            jQuery(ctl).find("#" + targetId).attr("data-value", color);
        });


        //背景文字颜色改变
        jQuery(ctl).find('#dc_BackgroundTextColor').change(function () {
            var targetId = this.getAttribute("dc-target-id");
            var color = this.value;
            jQuery(ctl).find("#" + targetId).css("background-color", color);
            jQuery(ctl).find("#" + targetId).attr("data-value", color);

        });


        //激活模式值复显
        if (options && options.EditorActiveMode) {
            jQuery(ctl).find("#dc_EditorActiveModeButton").text(options.EditorActiveMode.length ? options.EditorActiveMode : "None");
            jQuery(ctl).find("#dc_EditorActiveModeButton").prop("title", options.EditorActiveMode.length ? options.EditorActiveMode : "");
        }


        var AcceptChildElementTypes = "";
        if (options && options.AcceptChildElementTypes) {
            AcceptChildElementTypes = options.AcceptChildElementTypes;
        }

        jQuery(ctl).find("#dc_AcceptChildElementTypesButton").click(function () {
            let dc_AcceptChildElementTypesHtml =
                jQuery(` <div id="dc_childrenDialogContainer" class="dc_childrenDialogContainer"></div>
            <div id="dc_AcceptChildElementTypesHtml" >
                                <div class="dc_EditorActiveModeHeader">
                                    <p>可包含的内容</p>
                                    <p class="dc_EditorActiveModeCancelButtonIcon"></p>
                                </div> 
                                <div class="dc_EditorActiveModeContent_Box">
                                    <div>
                                        <input class="dc_radioType" type="radio" id="dc_allType" />所有文档类型
                                    </div>
                                    <div class="dc_Box">
                                        <div> <input class="dc_radioType"  type="radio" id="dc_appointType" />特定文档元素类型</div>
                                        <div id="dc_AcceptChildCheck">
                                        <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="Text" />
                                            文本类型
                                        </label>
                                        <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="LineBreak" />
                                            换行
                                        </label>
                                        <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="Field" />
                                            文本域类型
                                        </label>
                                        <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="PageBreak" />
                                            分页符
                                        </label>
                                        <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="InputField" />
                                            输入域类型
                                        </label>
                                        <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="ParagraphFlag" />
                                            段落
                                        </label>
                                            <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="Table" />
                                            表格类型
                                        </label>
                                            <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="CheckBox" />
                                            单/复选框
                                        </label>
                                            <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="Object" />
                                            对象类型
                                        </label>
                                            <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="Image" />
                                            图片
                                        </label>
                                        <label class="dc_EditorActiveItem" >
                                            <input type="checkbox" value="Button" />
                                            按钮
                                        </label>
                                      </div>
                                    </div>
                                <div class="dc_EditorActiveModeDialogBox">
                                    <p id="dc_AcceptChildElementTypesHtmlConfom">确认</p>
                                    <p id="dc_EditorActiveModeCancel">取消</p>
                                </div>
                                </div>
                            </div>`);
            dc_AcceptChildElementTypesHtml.appendTo(ctl);
            if (
                AcceptChildElementTypes &&
                AcceptChildElementTypes.length &&
                AcceptChildElementTypes != "All"
            ) {
                jQuery(ctl).find("#dc_appointType").prop("checked", true);
                let valuearr = AcceptChildElementTypes.split(",");
                valuearr = valuearr.map((item) => item.trim());
                let allDomCheck = jQuery(ctl)
                    .find("#dc_AcceptChildCheck")
                    .find('input[type="checkbox"]');
                for (var i = 0; i < allDomCheck.length; i++) {
                    let item = allDomCheck[i];
                    if (valuearr.indexOf(item.value) !== -1) {
                        jQuery(item).attr("checked", true);
                    }
                }
            }
            if (AcceptChildElementTypes === "All") {
                jQuery(ctl).find("#dc_allType").prop("checked", true);
                jQuery(ctl)
                    .find("#dc_AcceptChildCheck")
                    .find('input[type="checkbox"]')
                    .attr("disabled", true);
            }
            jQuery(ctl)
                .find(
                    ".dc_EditorActiveModeCancelButtonIcon,#dc_EditorActiveModeCancel"
                )
                .click(function () {
                    jQuery(ctl).find("#dc_AcceptChildElementTypesHtml").remove();
                    jQuery(ctl).find("#dc_childrenDialogContainer").remove();
                });
            jQuery(ctl)
                .find(".dc_radioType")
                .click(function (e) {
                    if (e.target.id == "dc_allType") {
                        jQuery(ctl).find("#dc_appointType").removeAttr("checked");
                        jQuery(ctl)
                            .find("#dc_AcceptChildCheck")
                            .find('input[type="checkbox"]')
                            .attr("disabled", true);
                    } else {
                        jQuery(ctl).find("#dc_allType").removeAttr("checked");
                        jQuery(ctl)
                            .find("#dc_AcceptChildCheck")
                            .find('input[type="checkbox"]')
                            .removeAttr("disabled");
                    }
                });
            jQuery(ctl)
                .find("#dc_AcceptChildElementTypesHtmlConfom")
                .click(function () {
                    if (jQuery(ctl).find("#dc_allType").attr("checked")) {
                        AcceptChildElementTypes = "All";
                    }
                    if (jQuery(ctl).find("#dc_appointType").attr("checked")) {
                        let arrDom = jQuery(ctl)
                            .find("#dc_AcceptChildCheck")
                            .find('input[type="checkbox"]');
                        let str = [];
                        for (var i = 0; i < arrDom.length; i++) {
                            let item = arrDom[i];
                            if (jQuery(item).attr("checked")) {
                                str.push(item.value);
                            }
                        }
                        AcceptChildElementTypes = str.join();
                    }
                    jQuery(ctl)
                        .find("#dc_AcceptChildElementTypesInput")
                        .val(AcceptChildElementTypes);
                    jQuery(ctl).find("#dc_AcceptChildElementTypesHtml").remove();
                    jQuery(ctl).find("#dc_childrenDialogContainer").remove();
                });
        });
        //激活模式点击
        jQuery(ctl).find("#dc_EditorActiveModeButton").click(function () {
            let editorActiveModeSelectHtml =
                jQuery(` <div id="dc_childrenDialogContainer" class="dc_childrenDialogContainer"></div>
            <div id="dc_EditorActiveModeSelect" >
                                <div class="dc_EditorActiveModeHeader">
                                    <p>激活模式选项</p>
                                    <p class="dc_EditorActiveModeCancelButtonIcon"></p>
                                </div>
                                <div class="dc_EditorActiveModeContainer">
                                    <label class="dc_EditorActiveItem" >
                                        <input  type="checkbox" value="Default"></input>默认激活模式，由文档对象的BehaviorOptions,DefaultEditorActiveMode属性值指定
                                    </label>
                                    <label class="dc_EditorActiveItem">
                                        <input  type="checkbox" value="Program"></input>应用程序激活
                                    </label>
                                    <label class="dc_EditorActiveItem">
                                        <input  type="checkbox" value="F2"></input>按下F2键激活
                                    </label>
                                    <label class="dc_EditorActiveItem">
                                        <input  type="checkbox" value="GotFocus"></input>获得输入焦点时就激活
                                    </label>
                                    <label class="dc_EditorActiveItem">
                                        <input  type="checkbox" value="MouseDblClick"></input>鼠标双击就激活
                                    </label>
                                    <label class="dc_EditorActiveItem">
                                        <input  type="checkbox" value="MouseClick"></input>鼠标单击就激活
                                    </label>
                                    <label class="dc_EditorActiveItem">
                                        <input  type="checkbox" value="MouseRightClick"></input>鼠标右击就激活
                                    </label>
                                    <label class="dc_EditorActiveItem">
                                        <input  type="checkbox" value="Enter"></input>键盘Enter键激活
                                    </label>
                                </div>
                                <div class="dc_EditorActiveModeDialogBox">
                                    <p id="dc_EditorActiveModeConfom">确认</p>
                                    <p id="dc_EditorActiveModeCancel">取消</p>
                                </div>
                            </div>`);
            editorActiveModeSelectHtml.appendTo(ctl);
            console.log(jQuery(ctl).find("#dc_EditorActiveModeButton").text());
            if (jQuery(ctl).find("#dc_EditorActiveModeButton").text()) {
                let EditorActiveModeValueArr = jQuery(ctl)
                    .find("#dc_EditorActiveModeButton")
                    .text();
                let EditorActiveItemArr = jQuery(ctl).find(
                    '.dc_EditorActiveItem>input[type="checkbox"]'
                );
                for (var i = 0; i < EditorActiveItemArr.length; i++) {
                    let item = EditorActiveItemArr[i];
                    if (EditorActiveModeValueArr.includes(item.value)) {
                        item.checked = true;
                    } else {
                        item.checked = false;
                    }
                }
            }
            jQuery(ctl)
                .find(
                    ".dc_EditorActiveModeCancelButtonIcon,#dc_EditorActiveModeCancel"
                )
                .click(function () {
                    jQuery(ctl).find("#dc_EditorActiveModeSelect").remove();
                    jQuery(ctl).find("#dc_childrenDialogContainer").remove();
                });
            jQuery(ctl)
                .find("#dc_EditorActiveModeConfom")
                .click(function () {
                    let EditorActiveItemArr = jQuery(ctl).find(
                        '.dc_EditorActiveItem>input[type="checkbox"]'
                    );
                    let EditorActiveModeValueStr = "";
                    for (var i = 0; i < EditorActiveItemArr.length; i++) {
                        let item = EditorActiveItemArr[i];
                        if (item.checked) {
                            EditorActiveModeValueStr +=
                                (EditorActiveModeValueStr.length > 1 ? "," : "") + item.value;
                        }
                    }
                    jQuery(ctl)
                        .find("#dc_EditorActiveModeButton")
                        .text(
                            EditorActiveModeValueStr.length
                                ? EditorActiveModeValueStr
                                : "None"
                        );
                    jQuery(ctl)
                        .find("#dc_EditorActiveModeButton")
                        .prop("title", EditorActiveModeValueStr);
                    jQuery(ctl).find("#dc_EditorActiveModeSelect").remove();
                    jQuery(ctl).find("#dc_childrenDialogContainer").remove();
                });
        });

        //tab切换
        jQuery(ctl).find("#dc_InputFieldContentButtonBox > .dc_tabButton").click(function () {
            let tabButtonActive = jQuery(ctl).find(
                "#dc_InputFieldContentButtonBox > .dc_tabButton.dc_active"
            );
            if (tabButtonActive[0] !== this) {
                tabButtonActive.removeClass("dc_active");
                this.className = "dc_tabButton dc_active";
                domShowAndHide();
            }
        });


        //静态属性内容二级弹框
        var AttributesC = [];
        if (options && options.ListItems && options.ListItems.length) {
            AttributesC = [...options.ListItems];
        }
        jQuery(ctl).find("#dc_browseTextTableContent").click(function () {
            setBrowseTextTableContent();
        });
        //自定义属性二级弹框
        var AttributesA = {}; //接收自定义属性
        if (
            options &&
            options.Attributes &&
            Object.keys(options.Attributes).length
        ) {
            AttributesA = JSON.parse(JSON.stringify(options.Attributes));
        }
        jQuery(ctl).find("#dc_tab4 #dc_browsess").click(function () {
            setCustomAttributeContent();
        });

        // 静态选择内容弹框
        function setBrowseTextTableContent() {
            let staticDialog = jQuery(`
            <div id="dc_BrowseTextTableDialog" class="dc_childrenDialogContainer"></div>
            <div id="dc_dialogContainer1" >
                <div id="dcPanelHeader1" >
                <span>静态选择项内容</span>
                <div class="dc_cancel3 dcHeader-tool">
                <b class="dcTool-close">✖</b>
            </div>
                </div>
                <div id="dcPanelBody1" ></div>
                <div id="dcPanelFooter1">  
                    <button class="dclinkbutton dc_determine3">确认</button> 
                    <button class="dclinkbutton dc_cancel3">取消</button>
                </div>
            </div>
            `);
            staticDialog.appendTo(ctl);

            var watermark1 = `
                <div class="dc_watermark_container"  >
                    <span class="dc_watermarks_text" >字典来源：</span>
                    <input id="dc_InnerListSourceName" data-text="InnerListSourceName" type="text">
                </div> 
                <div class="dc_watermark_container_2">
                    <svg style="cursor: pointer;width:20px;height:20px;" t="1682241871634" class="dc_newlyIncreased" viewBox="0 0 1024 1024" version="1.1" 
                        xmlns="http://www.w3.org/2000/svg" p-id="4289" width="200" height="200">
                        <title>添加一行</title>
                        <path d="M544 480v-298.666667h-64v298.666667h-298.666667v64h298.666667v298.666667h64v-298.666667h298.666667v-64h-298.666667z"
                            fill="#61ea6a" p-id="4290"></path></svg>
                    <svg style="cursor: pointer;width:20px;height:20px;" t="1682241967818" class=" dc_moveUp" viewBox="0 0 1024 1024" version="1.1" 
                        xmlns="http://www.w3.org/2000/svg" p-id="5609" width="200" height="200">
                        <title>向上一行</title>
                        <path d="M489.386667 361.386667a32 32 0 0 1 45.226666 0L813.226667 640 768 685.226667l-256-256-256 256L210.773333 640l278.613334-278.613333z"
                            fill="#1296db" p-id="5610"></path></svg>
                    <svg style="cursor: pointer;width:20px;height:20px;" t="1682241944286" class=" dc_MoveDown" viewBox="0 0 1024 1024" version="1.1" 
                        xmlns="http://www.w3.org/2000/svg" p-id="5319" width="200" height="200">
                        <title>向下一行</title>
                        <path d="M256 338.773333l256 256 256-256L813.226667 384l-278.613334 278.613333a32 32 0 0 1-45.226666 0L210.773333 384 256 338.773333z"
                            fill="#1296db" p-id="5320"></path></svg>
                    <svg style="cursor: pointer;width:20px;height:20px;" t="1682241933018" class=" dc_DeleteIine" viewBox="0 0 1024 1024" version="1.1" 
                        xmlns="http://www.w3.org/2000/svg" p-id="5174" width="200" height="200">
                        <title>删除本行</title>
                        <path d="M557.226667 512l256-256L768 210.773333l-256 256-256-256L210.773333 256l256 256-256 256L256 813.226667l256-256 256 256L813.226667 768l-256-256z"
                            fill="#cf514b" p-id="5175"></path></svg>
                    <svg style="cursor: pointer;width:18px;height:18px;" t="1682241953506" class=" dc_DeleteAll" viewBox="0 0 1024 1024" version="1.1" 
                        xmlns="http://www.w3.org/2000/svg" p-id="5464" width="200" height="200">
                        <title>清空列表项目</title>
                        <path d="M394.666667 128v96h256V128a10.666667 10.666667 0 0 0-10.666667-10.666667H405.333333a10.666667 10.666667 0 0 0-10.666666 10.666667z m-64 96V128c0-41.216 33.450667-74.666667 74.666666-74.666667H640c41.216 0 74.666667 33.450667 74.666667 74.666667v96h213.333333v64h-85.333333V896A74.666667 74.666667 0 0 1 768 970.666667H256A74.666667 74.666667 0 0 1 181.333333 896V288h-85.333333v-64h234.666667z m-85.333334 64V896c0 5.888 4.778667 10.666667 10.666667 10.666667h512a10.666667 10.666667 0 0 0 10.666667-10.666667V288H245.333333z m213.333334 149.333333v320h-64v-320h64z m170.666666 320v-320h-64v320h64z"
                            fill="#d81e06" p-id="5465"></path>
                    </svg>
                </div>
                <table id="dc_batchPlanTable"  class="dc_currentTableDom"  border="1">
                    <tr>
                        <th  class="dc_on-1">序号</th>
                        <th  class="dc_on-2">文本</th>
                        <th  class="dc_on-3">值</th>
                        <th  class="dc_on-3">指定列表文本</th>
                        <th  class="dc_on-3">分组名</th>
                    </tr>
                    <tr id="tr_table" class="dc_tr_ab">
                        <td  id="dc_batchPlanTableon1" class="dc_on-1"></td>
                        <td><input class="dc_on-2" type="text" data-arraytext="Text" ></input></td>
                        <td><input class="dc_on-3" type="text" data-arraytext="Value" ></input></td>
                        <td><input  class="dc_tr-s"  type="text" data-arraytext="TextInList" ></input></td>
                        <td><input  class="dc_tr-g"  type="text" data-arraytext="Group" ></input></td>
                    </tr>
                    <template class="dc_template_item">
                        <tr id="tr_table" class="dc_tr_ab">
                            <td class="dc_on-1"></td>
                            <td><input class="dc_on-2" type="text" data-arraytext="Text" ></input></td>
                            <td><input class="dc_on-3"  type="text" data-arraytext="Value" ></input></td>
                            <td><input class="dc_tr-s" type="text" data-arraytext="TextInList" ></input></td>
                            <td><input class="dc_tr-g" type="text" data-arraytext="Group" ></input></td>
                        </tr>
                    </template>
                </table>`;
            //插入dom结构
            jQuery(ctl).find("#dcPanelBody1").html(watermark1).css("background", "#FAFAFA");
            //优先展示当前用户操作过的列表
            let newListItem = AttributesC && AttributesC.length
                ? AttributesC
                : (options && options.ListItems) || [];

            if (newListItem && newListItem.length) {
                var CDC = jQuery(ctl).find(".dc_tr_ab")[0];
                for (var i = 0; i < newListItem.length; i++) {
                    var tr = ctl.ownerDocument.createElement("tr");
                    tr.className = "dc_tr_ab";
                    tr.innerHTML = `<td  class="dc_on-1">${i + 1}</td>
                            <td><input  class="dc_on-2" type="text" data-arraytext="Text" value="${newListItem[i].Text || ""
                        }" ></input> </td>
                            <td><input  class="dc_on-3" type="text" data-arraytext="Text" value="${newListItem[i].Value || ""
                        }" ></input> </td>
                            <td><input  class="dc_tr-s" type="text" data-arraytext="Text" value="${newListItem[i].TextInList || ""
                        }" ></input></td>
                        <td><input  class="dc_tr-g" type="text" data-arraytext="Text" value="${newListItem[i].Group || ""
                        }" ></input></td>`;
                    CDC.before(tr);
                }

                let dc_batchPlanTableon1 = ctl.ownerDocument.getElementById("dc_batchPlanTableon1");
                if (dc_batchPlanTableon1) {
                    dc_batchPlanTableon1.innerHTML = (newListItem && newListItem.length + 1) || '';
                }
            }
            if (InnerListSourceName) {
                //数据字典回填
                jQuery(ctl).find("#dc_InnerListSourceName").val(InnerListSourceName);
            }


            //自动增加行
            var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer1");
            var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody1");
            dcPanelBody
                .find("#dc_batchPlanTable")
                .on("input", "input[data-arraytext]", function () {
                    var input = jQuery(this);
                    var tr = input.parents("tr");
                    if (tr.nextAll("tr").length == 0) {
                        var ListItems_item = input
                            .parents("table")
                            .find("template.dc_template_item")[0];
                        tr.after(ListItems_item.content.cloneNode(true));
                        let newOn1 = jQuery(ctl).find(".dc_on-1");
                        for (var i = 1; i < newOn1.length; i++) {
                            newOn1[i].innerHTML = i;
                        }
                    }
                });
            //添加自定义属性
            AddCustomProperties();
            //表格行操作 新增
            AddTableRow();
            // 表格行操作 上移
            TableRowMovedUp();
            // 表格行操作 下移
            TableRowDown();
            //表格行操作 删除行
            DeleteLine();
            //表格行操作 全部删除
            dc_DeleteAll();

            // 按下键盘上下左右，控制光标跳转单元格
            function CellInputFocus(event) {
                //键盘监听上下左右键，根据按键跳转单元格输入框
                let cells = dcPanelBody.find("#dc_batchPlanTable input");
                let currentCell = ctl.ownerDocument.activeElement; // 获取当前拥有焦点的单元格
                let index = 0;//默认当前input的下标
                let targetCell = null;//目标input
                if (event.key === 'ArrowUp') {
                    // 切换到上方的单元格
                    index = Array.from(cells).indexOf(currentCell);
                    targetCell = cells[index - 4]; // 每行有4个input
                } else if (event.key === 'ArrowDown') {
                    // 切换到下方的单元格
                    index = Array.from(cells).indexOf(currentCell);
                    targetCell = cells[index + 4]; // 每行有4个input
                } else if (event.key === 'ArrowLeft') {
                    // 判断光标位置
                    if (currentCell.selectionStart === 0 && currentCell.selectionEnd === 0) {
                        // 切换到左边的单元格
                        index = Array.from(cells).indexOf(currentCell);
                        targetCell = cells[index - 1];
                    }
                } else if (event.key === 'ArrowRight') {
                    // 判断光标位置
                    if (currentCell.selectionStart === currentCell.value.length && currentCell.selectionEnd === currentCell.value.length) {
                        // 切换到右边的单元格
                        index = Array.from(cells).indexOf(currentCell);
                        targetCell = cells[index + 1];
                    }
                }
                if (targetCell) {
                    setTimeout(() => {
                        targetCell.focus();
                        targetCell.setSelectionRange(targetCell.value.length, targetCell.value.length);
                    });
                }
            }

            // 确认--->收集数据
            jQuery(ctl)
                .find(".dc_determine3")
                .click(function () {
                    AttributesC = [];
                    InnerListSourceName = jQuery(ctl)
                        .find("#dc_InnerListSourceName")
                        .val();
                    let allTrAb = jQuery(ctl).find(".dc_tr_ab");
                    for (let i = 0; i < jQuery(ctl).find(".dc_tr_ab").length; i++) {
                        var AttributesB = {}; //接收动态列表
                        var ssf = jQuery(ctl).find(".dc_tr_ab")[i].children;
                        for (let u = 0; u < ssf.length; u++) {
                            if (ssf[u].children && ssf[u].children[0]) {
                                if (ssf[u].children[0].className == "dc_on-2") {
                                    AttributesB.Text = jQuery(ssf[u].children[0]).val();
                                }
                                if (ssf[u].children[0].className == "dc_on-3") {
                                    AttributesB.Value = jQuery(ssf[u].children[0]).val();
                                }
                                if (ssf[u].children[0].className == "dc_tr-s") {
                                    AttributesB.TextInList = jQuery(ssf[u].children[0]).val();
                                }
                                if (ssf[u].children[0].className == "dc_tr-g") {
                                    AttributesB.Group = jQuery(ssf[u].children[0]).val();
                                }
                            }
                        }

                        if (i == allTrAb.length - 1) {
                            if (Object.values(AttributesB).join("").length) {
                                AttributesC.push(AttributesB);
                            }
                        } else {
                            AttributesC.push(AttributesB);
                        }
                    }
                    jQuery(ctl)
                        .find("#dc_StaticSelection")
                        .val(AttributesC.length + " " + "items");
                    jQuery(ctl).find("#dc_dialogContainer1").remove();
                    jQuery(ctl).find("#dc_BrowseTextTableDialog").remove();
                    // 删除监听键盘按下事件
                    ctl.ownerDocument.removeEventListener('keydown', CellInputFocus);
                });
            // 关闭
            jQuery(ctl)
                .find(".dc_cancel3,.dcTool-close")
                .click(function () {
                    jQuery(ctl).find("#dc_dialogContainer1").remove();
                    jQuery(ctl).find("#dc_BrowseTextTableDialog").remove();
                    // 删除监听键盘按下事件
                    ctl.ownerDocument.removeEventListener('keydown', CellInputFocus);
                });

            // 监听键盘按下事件
            ctl.ownerDocument.addEventListener('keydown', CellInputFocus);
        }
        let that = this;
        // 自定义属性弹框
        function setCustomAttributeContent() {
            let customAttributeDialog = jQuery(`
            <div id="dc_CustomAttributeTableDialog" class="dc_childrenDialogContainer"></div>
            <div id="dc_dialogContainer2" >
                <div id="dcPanelHeader2" >
                <span>自定义属性</span>
                <div class="dc_cancel2 dcHeader-tool">
                    <b class= "dcTool-close">✖</b>
                </div>
                </div>
                <div id="dcPanelBody2">
                    <div id="dc_attr-box" ></div>
                </div>
                <div id="dcPanelFooter2" >  
                    <button class="dclinkbutton dc_determine2">确认</button> 
                    <button class="dclinkbutton dc_cancel2">取消</button>
                </div>
            </div>
            `);
            customAttributeDialog.appendTo(ctl);

            that.attributeComponents("#dc_attr-box", AttributesA || {}, ctl);

            //确认
            jQuery(ctl)
                .find(".dc_determine2")
                .click(function () {
                    var dcAttrBox = jQuery(ctl).find("#dc_attr-box");
                    AttributesA = that.attributeComponents_getAttributeObj(dcAttrBox);
                    jQuery(ctl).find("#dc_Attributes").val(Object.values(AttributesA).length + " " + "items");
                    jQuery(ctl).find("#dc_dialogContainer2").remove();
                    jQuery(ctl).find("#dc_CustomAttributeTableDialog").remove();
                });
            // 关闭
            jQuery(ctl)
                .find(".dc_cancel2,.dcTool-close")
                .click(function () {
                    jQuery(ctl).find("#dc_dialogContainer2").remove();
                    jQuery(ctl).find("#dc_CustomAttributeTableDialog").remove();
                });
        }
        //表格行操作 全部删除
        function dc_DeleteAll() {
            jQuery(ctl)
                .find(".dc_DeleteAll")
                .on("click", function () {
                    if (jQuery(ctl).find("#dcPanelBody1").css("display") == "block") {
                        var CDC = jQuery(ctl).find(".dc_tr_ab");
                        for (let i = 0; i < CDC.length; i++) {
                            CDC[i].remove();
                        }
                        var tr = ctl.ownerDocument.createElement("tr");
                        tr.className = "dc_tr_ab";
                        tr.innerHTML = `
                        <td  class="dc_on-1">1</td>
                        <td><input class="dc_on-2" type="text" data-arraytext="Text" value="" ></input> </td>
                        <td><input class="dc_on-3" type="text" data-arraytext="Text" value="" ></input> </td>
                        <td><input class="dc_tr-s" type="text" data-arraytext="Text" value="" ></input></td>`;
                        jQuery(ctl).find("#dc_batchPlanTable").append(tr);
                    }
                });
        }
        //表格行操作 删除行
        function DeleteLine() {
            jQuery(ctl)
                .find(".dc_DeleteIine")
                .on("click", function () {
                    if (jQuery(ctl).find("#dcPanelBody1").css("display") == "block") {
                        var CDC = jQuery(ctl).find(".dc_tr_ab");
                        for (let i = 0; i < CDC.length; i++) {
                            if (CDC[i].getAttribute("moveh") == "true") {
                                var current = CDC[i];
                                current.remove();
                            }
                        }
                    }
                });
        }
        //表格行操作 下移
        function TableRowDown() {
            jQuery(ctl)
                .find(".dc_MoveDown")
                .on("click", function () {
                    if (jQuery(ctl).find("#dcPanelBody1").css("display") == "block") {
                        var CDC = jQuery(ctl).find(".dc_tr_ab");
                        for (let i = 0; i < CDC.length; i++) {
                            if (CDC[i].getAttribute("moveh") == "true") {
                                var current = CDC[i];
                                var prev = current.nextElementSibling;
                                console.log(CDC[i].rowIndex);
                                jQuery(prev).after(current);
                            }
                        }
                    }
                });
        }
        // 表格行操作 上移
        function TableRowMovedUp() {
            jQuery(ctl)
                .find(".dc_moveUp")
                .on("click", function () {
                    if (jQuery(ctl).find("#dcPanelBody1").css("display") == "block") {
                        var CDC = jQuery(ctl).find(".dc_tr_ab");
                        for (let i = 0; i < CDC.length; i++) {
                            if (CDC[i].getAttribute("moveh") == "true") {
                                var current = CDC[i];
                                var prev = current.previousElementSibling;
                                if (CDC[i].rowIndex > 1) {
                                    jQuery(prev).before(current);
                                } else {
                                    // alert("已经到头部了")
                                }
                            }
                        }
                    }
                });
        }
        //表格行操作 新增
        function AddTableRow() {
            jQuery(ctl)
                .find(".dc_newlyIncreased")
                .on("click", function () {
                    if (jQuery(ctl).find("#dcPanelBody1").css("display") == "block") {
                        var tr = ctl.ownerDocument.createElement("tr");
                        let newOn1 = jQuery(ctl).find(".dc_on-1");
                        tr.className = "dc_tr_ab";
                        tr.innerHTML = `
                        <td  class="dc_on-1">${newOn1.length}</td>
                        <td><input class="dc_on-2" type="text" data-arraytext="Text" value="" ></input> </td>
                        <td><input class="dc_on-3" type="text" data-arraytext="Text" value="" ></input> </td>
                        <td><input class="dc_on-s" type="text" data-arraytext="Text" value="" ></input> </td>
                        <td><input class="dc_tr-g" type="text" data-arraytext="Text" value="" ></input></td>`;
                        jQuery(ctl).find("#dc_batchPlanTable").append(tr);
                        return;
                    } else {
                        return;
                    }
                });
        }
        //添加自定义属性
        function AddCustomProperties() {
            if (jQuery(ctl).find("#dcPanelBody1").css("display") == "block") {
                jQuery(ctl)
                    .find("#dc_batchPlanTable")
                    .delegate(".dc_tr_ab", "click", function () {
                        jQuery(ctl).find(".dc_tr_ab").removeAttr("moveh", false);
                        jQuery(this).attr("moveh", true);
                        // console.log(this);
                    });
            }
        }
        // 处理新增单元格的问题
        //绑定事件需要修改
        ctl.ownerDocument.onkeyup = function () {
            if (jQuery(ctl).find("#dcPanelBody1").css("display") == "block") {
                if (
                    jQuery(ctl).find("table").find("tr:last")[0].children[1].children[0]
                        .value === ""
                ) {
                    return;
                } else {
                    var tr = ctl.ownerDocument.createElement("tr");
                    tr.className = "dc_tr_ab";
                    tr.innerHTML = `
                    <td  class="dc_on-1"></td>
                            <td><input  class="dc_on-2" type="text" data-arraytext="Text" value="" ></input> </td>
                            <td><input  class="dc_on-3" type="text" data-arraytext="Text" value="" ></input> </td>
                            <td><input  class="dc_tr-s" type="text" data-arraytext="Text" value="" ></input></td>`;
                    jQuery(ctl).find("#dc_batchPlanTable").append(tr);
                }
            } else {
                return;
            }
        };

        //[DUWRITER5_0-3748] 20241025 lxy 新增属性表达式功能
        var propertyExpressionObject = {};
        //属性表达式值回填
        var propertyShowInput = ctl.ownerDocument.getElementById('dc_PropertyExpressions_show_input');
        if (options.PropertyExpressions) {
            //防止渲染重复的可见性表达式值
            var inputValue = '';
            var propertyKeyArr = Object.keys(options.PropertyExpressions) || [];
            INPUTFIELDPROPERTYEXPRESSIONSARRAY.forEach(item => {
                if (propertyKeyArr && propertyKeyArr.indexOf(item) > -1) {
                    propertyExpressionObject[item] = options.PropertyExpressions[item];
                    //拼接展示文本
                    if (propertyExpressionObject[item] !== '') {
                        inputValue += `${inputValue === '' ? '' : ','}${item}:${propertyExpressionObject[item]}`;
                    }
                } else {
                    propertyExpressionObject[item] = '';
                }
            });
            propertyShowInput.value = inputValue;
        }

        //属性表达式操作对话框
        var propertyExpressionsButton = jQuery(ctl).find("#dc_tab4 #dc_PropertyExpressionsButton");
        propertyExpressionsButton.click(function () {
            //判断是否已经存在修改过的属性表达式
            if (!propertyExpressionObject || Object.keys(propertyExpressionObject).length === 0) {
                INPUTFIELDPROPERTYEXPRESSIONSARRAY.forEach(item => {
                    propertyExpressionObject[item] = '';
                });
            }

            that.PropertyExpressionsDialog(propertyExpressionObject, ctl, function (changedPropertyExpressions) {
                //获取修改后的属性表达式
                propertyExpressionObject = JSON.parse(JSON.stringify(changedPropertyExpressions));
                //更新属性表达式的显示
                var inputValue = '';
                for (let key in changedPropertyExpressions) {
                    if (changedPropertyExpressions[key] !== '') {
                        inputValue += `${inputValue === '' ? '' : ','}${key}:${changedPropertyExpressions[key]}`;
                    }
                }
                propertyShowInput.value = inputValue;
            });
        });


        function successFun() {
            let newOptions = {};
            var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
            var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
            var dcPanelBodyTab3 = jQuery(dc_dialogContainer).find("#dc_tab3");
            var dc_CopySource = jQuery(dc_dialogContainer).find('#dc_CopySource');

            // 获取输入域基本属性
            newOptions = GetOrChangeData(dcPanelBody);

            // 获取输入域校验属性
            newOptions['ValidateStyle'] = GetOrChangeData(dcPanelBodyTab3);

            //获取复制来源
            newOptions['CopySource'] = GetOrChangeData(dc_CopySource);

            // 获取输入域静态资源
            newOptions.Attributes = AttributesA;

            //获取输入域静态资源
            newOptions['ListItems'] = AttributesC && AttributesC.length ? AttributesC : null;

            //获取输入域格式类型tab2
            var InnerEditStyleCheck = jQuery(ctl).find('#dc_tab2>label>input[type="radio"]:checked');
            newOptions["InnerEditStyle"] = InnerEditStyleCheck.attr('attrid');

            //获取输入域的输出格式
            let Style = jQuery(ctl).find("#dc_DisplayFormat").val();
            let Format = jQuery(ctl).find("#dc_selectionRight").val();
            newOptions["DisplayFormat"] = { Style, Format };

            //获取输入域的校验类型
            var validateStyleCheck = jQuery(ctl).find('#dc_tab3>div>input[type="radio"]:checked');
            newOptions.ValidateStyle["ValueType"] = validateStyleCheck.attr('attrid');

            if (newOptions.ValidateStyle && newOptions.ValidateStyle.ValueType === "DateTime") {

                //20240329 lixinyu 修复校验选择时间校验不设置时间,再次获取时间变成1980/1/1问题(DUWRITER5_0-2165)
                if (newOptions.ValidateStyle["DateTimeMaxValue"] === '') {
                    newOptions.ValidateStyle["DateTimeMaxValue"] = "0001/1/1 上午12:00:00";
                }
                if (newOptions.ValidateStyle["DateTimeMinValue"] === '') {
                    newOptions.ValidateStyle["DateTimeMinValue"] = "0001/1/1 上午12:00:00";
                }
            } else if (newOptions.ValidateStyle && newOptions.ValidateStyle.ValueType === "Text") {
                newOptions.ValidateStyle["CheckMaxValue"] = true;
                newOptions.ValidateStyle["CheckMinValue"] = true;
            } else if (newOptions.ValidateStyle && newOptions.ValidateStyle.ValueType === "Numeric") {
                var DCNumericInteger = ctl.ownerDocument.getElementById('DCNumericInteger');
                if (DCNumericInteger.checked) {
                    newOptions.ValidateStyle.ValueType = 'Integer';
                }

                var DC_ValiNumber_CheckMaxValue = ctl.ownerDocument.getElementById('DC_ValiNumber_CheckMaxValue');
                var DC_ValiNumber_CheckMinValue = ctl.ownerDocument.getElementById('DC_ValiNumber_CheckMinValue');
                newOptions['ValidateStyle']['CheckMaxValue'] = DC_ValiNumber_CheckMaxValue.checked;
                newOptions['ValidateStyle']['CheckMinValue'] = DC_ValiNumber_CheckMinValue.checked;


            }
            // 获取输入域的数据源属性
            let { BindingPath, BindingPathForText, Datasource, ProcessState } = newOptions;
            newOptions["ValueBinding"] = { BindingPath, BindingPathForText, Datasource, ProcessState };

            // 获取静态资源弹框的字典来源
            newOptions["InnerListSourceName"] = InnerListSourceName;


            //激活模式
            newOptions["EditorActiveMode"] = jQuery(ctl).find("#dc_EditorActiveModeButton").text();

            //删除外层数据源无用数据
            newOptions["BindingPath"] && delete newOptions["BindingPath"];
            newOptions["BindingPathForText"] && delete newOptions["BindingPathForText"];
            newOptions["Datasource"] && delete newOptions["Datasource"];
            newOptions["ProcessState"] && delete newOptions["ProcessState"];

            //20241011 lxy 颜色属性取值
            var TextColor = jQuery(ctl).find("#dc_TextColor_box").attr("data-value") || '';
            var BackgroundTextColor = jQuery(ctl).find("#dc_BackgroundTextColor_box").attr("data-value") || '';
            var BackgroundColorString = jQuery(ctl).find("#dc_BackgroundColorString_box").attr("data-value") || '';
            newOptions['TextColor'] = TextColor;
            newOptions['BackgroundTextColor'] = BackgroundTextColor;
            newOptions['Style'] = newOptions['Style'] ? {
                ...newOptions['Style'],
                BackgroundColorString
            } : {
                BackgroundColorString
            };

            //[DUWRITER5_0-3748] 20241025 lxy 新增属性表达式功能
            newOptions['PropertyExpressions'] = {};
            for (var key in propertyExpressionObject) {
                if (propertyExpressionObject[key] !== '') {
                    newOptions['PropertyExpressions'][key] = propertyExpressionObject[key];
                }
            }
            //计算表达式
            if (newOptions && newOptions.ValueExpression && newOptions.ValueExpression.trim() !== '') {
                newOptions['PropertyExpressions']['FormulaValue'] = newOptions.ValueExpression;
            }

            //可见性表达式
            if (newOptions && newOptions.VisibleExpression && newOptions.VisibleExpression.trim() !== '') {
                newOptions['PropertyExpressions']['Visible'] = newOptions.VisibleExpression;
            }

            console.log("newOptions", newOptions);
            // [DUWRITER5_0-3613] 20240919 lxy 新增插入元素确定后，返回新插入的元素属性
            var insertResult = null;
            if (isInsertMode == true) {
                insertResult = ctl.DCExecuteCommand("InsertInputField", false, newOptions);
            } else {
                ctl.SetElementProperties(ctl.CurrentInputField(), newOptions);
            }
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentInputField());
                ctl.EventDialogChangeProperties(insertResult ? insertResult : changedOptions);
            };

        }
    },


    /**
       * 属性表达式公共弹框
       * @param options 属性表达式属性
       * @param ctl 编辑器元素
       */
    PropertyExpressionsDialog: function (propertyExpressions, ctl, callBack) {
        var PropertyExpressionsTableHtml = "";
        Object.keys(propertyExpressions).forEach(key => {
            PropertyExpressionsTableHtml += `
                <div class="dc_PropertyExpressionsTable_item">
                    <span class="dc_PropertyExpressionsTable_item_left" title="${key}">${key}</span>
                    <input placeholder="请输入表达式内容" type="text" class="dc_PropertyExpressionsTable_input" propertykey="${key}" value="${propertyExpressions[key]}" />
                </div>
            `;
        });
        var PropertyExpressionsDialog = ctl.ownerDocument.createElement("div");
        PropertyExpressionsDialog.className = "dc_PropertyExpressionsBox";
        PropertyExpressionsDialog.innerHTML = `
            <div class="dc_PropertyExpressionsBox_top">
                <span class="dc_PropertyExpressionsBox_title">属性表达式</span>
                <span class="dcTool-close">✖</span>
            </div>
            <div class="dc_PropertyExpressionsBox_center">
                <div class="dc_PropertyExpressionsTable">
                    <div class="dc_PropertyExpressionsTable_item_header">
                        <span class="dc_PropertyExpressionsTable_item_left">属性名称</span>
                        <span class="dc_PropertyExpressionsTable_input">表达式</span>
                    </div>
                    ${PropertyExpressionsTableHtml}
                </div>
            </div>
            <div class="dc_PropertyExpressionsBox_bottom">
                <div class="dc_PropertyExpressionsBox_button" id="dc_PropertyExpressionsBox_bottom_confirm">确认</div>
                <div class="dc_PropertyExpressionsBox_button" id="dc_PropertyExpressionsBox_bottom_cancel">取消</div>
            </div>
        `;
        ctl.appendChild(PropertyExpressionsDialog);
        // 关闭
        jQuery(PropertyExpressionsDialog)
            .find(".dcTool-close")
            .click(function () {
                jQuery(PropertyExpressionsDialog).remove();
            });
        // 取消
        jQuery(PropertyExpressionsDialog)
            .find("#dc_PropertyExpressionsBox_bottom_cancel")
            .click(function () {
                jQuery(PropertyExpressionsDialog).remove();
            });

        // 确认
        jQuery(PropertyExpressionsDialog)
            .find("#dc_PropertyExpressionsBox_bottom_confirm")
            .click(function () {
                //修改后的参数
                var changedProperty = {};
                Object.keys(propertyExpressions).forEach(key => {
                    var input = jQuery(PropertyExpressionsDialog).find(`.dc_PropertyExpressionsTable_input[propertykey="${key}"]`);
                    var inputvalue = input.val();
                    changedProperty[key] = inputvalue;
                });
                callBack && callBack(changedProperty);
                jQuery(PropertyExpressionsDialog).remove();
            });

        console.log("PropertyExpressionsDialog", PropertyExpressionsDialog);
    },





    // ======医学表达式-start======


    /**
     * 医学表达式根据图片选择插入
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    MedicalExpressionChooseIMGDialog: function (options, ctl) {
        var MedicalExpressionChooseIMGHtml = `
        <div id="MedicalExpressionChooseIMGBox"></div>
        `;
        var dialogOptions = {
            title: "请选择要插入的医学表达式",
            bodyHeight: 440,
            bodyClass: "MedicalExpressionChooseIMGElement",
            bodyHtml: MedicalExpressionChooseIMGHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        var MedicalExpressionChooseIMGBox = ctl.ownerDocument.getElementById("MedicalExpressionChooseIMGBox");
        if (MedicalExpressionChooseIMGBox) {
            if (MEDICALCHARACTERSIMGOBJECT && MEDICALCHARACTERSIMGOBJECT.length) {
                for (var i = 0; i < MEDICALCHARACTERSIMGOBJECT.length; i++) {
                    var img = MEDICALCHARACTERSIMGOBJECT[i];
                    var MedicalExpressionChooseDiv = ctl.ownerDocument.createElement("div");
                    MedicalExpressionChooseDiv.setAttribute('expressionStyle', img.expressionStyle);
                    MedicalExpressionChooseDiv.setAttribute('class', "dc_MedicalExpressionChooseDiv");
                    MedicalExpressionChooseDiv.id = "dc_MedicalExpressionChooseDiv_" + img.expressionStyle;
                    MedicalExpressionChooseDiv.innerHTML = `
                    <image src="${img.base64ImgSrc}" />
                    <div style="margin-left:10px;">${img.title}</div>`;
                    MedicalExpressionChooseIMGBox.appendChild(MedicalExpressionChooseDiv);
                }

            }
            MedicalExpressionChooseIMGBox.addEventListener('click', function (event) {
                var target = event.target;
                if (target.id === 'MedicalExpressionChooseIMGBox') {
                    return;
                }
                //删除上一次选中
                var prevChoose = ctl.ownerDocument && ctl.ownerDocument.querySelector && ctl.ownerDocument.querySelector(".dc_MedicalExpressionChooseDiv_active");
                if (prevChoose) {
                    prevChoose.classList.remove("dc_MedicalExpressionChooseDiv_active");
                }

                if (target.classList.contains("dc_MedicalExpressionChooseDiv")) {
                    var expressionStyle = target.getAttribute("expressionStyle");
                    options.ExpressionStyle = expressionStyle;
                    target.classList.add("dc_MedicalExpressionChooseDiv_active");
                } else {
                    target.parentNode.classList.add("dc_MedicalExpressionChooseDiv_active");
                }

            });
        }

        function successFun() {
            var MedicalExpressionChooseIMGBox = ctl.ownerDocument.getElementById("MedicalExpressionChooseIMGBox");
            if (MedicalExpressionChooseIMGBox) {
                var chooseDiv = MedicalExpressionChooseIMGBox.querySelector(".dc_MedicalExpressionChooseDiv_active");
                if (chooseDiv) {
                    var expressionStyle = chooseDiv.getAttribute("expressionStyle");
                    options.ExpressionStyle = expressionStyle;
                    ctl.DCExecuteCommand("InsertMedicalExpression", true, options);
                }
            }
        }
    },

    /**
     * 创建胎心图值对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    FetalHeartDialog: function (options, ctl, isInsertMode, ele) {
        if (options == null) {
            return false;
        }

        let arr = this.stringToObject(options.Values);
        var FetalHeartHtml = `
        <table width="100%" id="dc_fetal-heart-table" cellspacing="0">
            <tr>
                <td rowspan="2" class="dc_fetal-heart-table-line-td" />
                <td align="center" class="dc_fetal-heart-table-input">
                    <input  type="text"  data-text="Value1" />
                </td>
                <td rowspan="2" class="dc_fetal-heart-table-border-right" />
                <td rowspan="2" class="dc_fetal-heart-table-border-left" />
                <td align="center"  class="dc_fetal-heart-table-input">
                    <input  type="text"  data-text="Value2" />
                </td>
                <td rowspan="2" class="dc_fetal-heart-table-border-bottom" />
            </tr>
            <tr>
                <td rowspan="2" class="dc_fetal-heart-table-input dc_table-input-border-top" align="center">
                    <input  type="text" data-text="Value3" />
                </td>
                <td rowspan="2" class="dc_fetal-heart-table-input dc_table-input-border-top" align="center">
                    <input  type="text" data-text="Value4" />
                </td>
            </tr>
            <tr>
                <td rowspan="2" class="dc_fetal-heart-table-td-border" >
                </td>
                <td rowspan="2" class="dc_fetal-heart-table-border-top-right" />
                <td rowspan="2" class="dc_fetal-heart-table-border-top-left" />
                <td rowspan="2" class="dc_fetal-heart-table-td-border" />
            </tr>
            <tr>
                <td align="center" class="dc_fetal-heart-table-input" >
                    <input  type="text" data-text="Value5" />
                </td>
                <td align="center" class="dc_fetal-heart-table-input">
                    <input  type="text" data-text="Value6" />
                </td>
            </tr>
        </table>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;

        var dialogOptions = {
            title: "请输入胎心值",
            bodyHeight: 130,
            bodyClass: "FetalHeartElement",
            bodyHtml: FetalHeartHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var opts = {};
        let that = this;
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建标尺对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    PainIndexDialog: function (options, ctl, isInsertMode, ele) {
        // if (!options || typeof (options) != "object") {
        //     options = ctl.getFontObject();
        // }
        if (options == null) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        for (var i in arr) {
            arr[i] = arr[i] / 10;
        }
        var PainIndexHtml = `
            数字（0-10）：
            <input type="number" data-text="Value1"/>
            <div class="dc_AdvancedTeech_AutoSize">
                <label>
                    <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "标尺（请输入0到10之间的数字）",
            bodyHeight: "auto",
            bodyClass: "dc_PainIndexElement",
            bodyHtml: PainIndexHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);

        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            _data.Value1 = _data.Value1 * 10;
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
     * 创建眼球突出度
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */

    EyeballProtrusionDialog: function (options, ctl, isInsertMode, ele) {
        let arr = this.stringToObject(options.Values);
        var EyeballProtrusionHtml = `
        <div class="dc_EyeballProtrusionBox"> 
            <div class="dc_EyeballProtrusionContainerLeft">
                <input  data-text="Value1"  class="dc_ValueInput" type="number" />
                <span>mm</span>
            </div>
            <div class="dc_EyeballProtrusionContainerCenter">
                <div class="dc_EyeballProtrusionContainerCenter_value2" >
                    <input  data-text="Value2" class="dc_ValueInput" type="number" />
                    <span>mm</span>
                </div>
                <div class="LineBox">
                    <p class="dc_line dc_LineLeftTop"></p>
                    <p class="dc_line dc_LineLeftBottom"></p>
                    <p class="dc_line dc_LineCenter"></p>
                    <p class="dc_line dc_LineRightTop"></p>
                    <p class="dc_line dc_LineRightBottom"></p>
                </div>
            </div>
            <div class="dc_EyeballProtrusionContainerRight">
                <input  data-text="Value3" class="dc_ValueInput" type="number" />
                <span>mm</span>
            </div>
        </div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "插入眼球突出度",
            bodyHeight: 240,
            dialogContainerBodyWidth: 500,
            bodyClass: "EyeballProtrusion",
            bodyHtml: EyeballProtrusionHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        let that = this;
        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            console.log(options);
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
     * 创建斜视符号
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    SquintSymbolDialog: function (options, ctl, isInsertMode, ele) {
        let { Value1 } = this.stringToObject(options.Values);
        var SquintSymbolHtml = `<div class="dc_SquintSymbolBox"> 
                <div class="dc_SquintSymbolLeftContainer">
                    <div class="dc_ShowView">
                        <p  class="dc_LeftLineText dc_LeftLine"></p>
                        <p  class="dc_RightLineText dc_RightLine"></p>
                        <p class="dc_CenterRound"></p>
                        <p class="dc_LeftLineText" id="dc_LeftLineText_L" >L</p>
                        <p class="dc_RightLineText" id="dc_LeftLineText_R"  >R</p>
                    </div>
                </div>
                <div class="dc_Box">
                    <h6 class="dc_title">类型</h6>
                    <form>
                        <label class="dc_RadioLabel">
                            <input type="radio" id="L" />
                            <span>L</span>
                        </label>
                        <label class="dc_RadioLabel">
                            <input type="radio" id="R" />
                            <span>R</span>
                        </label>
                        <label class="dc_RadioLabel">
                            <input type="radio" id="LR" />
                            <span>LR</span>
                        </label>
                    </form>
                </div>
            </div>
            <div class="dc_AdvancedTeech_AutoSize">
                <label>
                    <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                </label>
            </div>`;
        var dialogOptions = {
            title: "插入斜视符号",
            bodyHeight: 240,
            bodyClass: " SquintSymbol",
            bodyHtml: SquintSymbolHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");

        if (Value1) {
            dcPanelBody
                .find(".dc_RadioLabel>input[type=radio]")
                .attr("checked", false);
            dcPanelBody.find(`#${Value1}`).attr("checked", true);
            changeShowView(Value1);
        }
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        dcPanelBody.find(".dc_RadioLabel>input[type=radio]").change(function (e) {
            dcPanelBody
                .find(".dc_RadioLabel>input[type=radio]")
                .attr("checked", false);
            this.checked = true;
            changeShowView(this.id);
        });
        function changeShowView(value) {
            switch (value) {
                case "L":
                    dcPanelBody.find(".dc_LeftLineText").show();
                    dcPanelBody.find(".dc_RightLineText").hide();
                    break;
                case "R":
                    dcPanelBody.find(".dc_LeftLineText").hide();
                    dcPanelBody.find(".dc_RightLineText").show();
                    break;
                case "LR":
                    dcPanelBody.find(".dc_LeftLineText").show();
                    dcPanelBody.find(".dc_RightLineText").show();
                    break;
            }
        }

        function successFun() {
            let radioArr = dcPanelBody.find(".dc_RadioLabel>input[type=radio]");
            for (var i = 0; i < radioArr.length; i++) {
                if (radioArr[i].checked) {
                    options["Values"] = `Value1:${radioArr[i].id};`;
                }
            }
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
     * 创建输入分数值对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    FractionDialog: function (options, ctl, isInsertMode, ele) {
        // if (!options || typeof (options) != "object") {
        //     options = ctl.getFontObject();
        // }
        if (options == null) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var FractionHtml = `
            <table cellspacing="10">
                <tr class="dc_Fraction_table_tr">
                    <td>
                        A值<input class="dc_Fraction_table_input" type="text" data-text="Value1"/>
                    </td>
                    <td rowspan="2" class="dc_Fraction_table_td" >
                    <strong>/</strong>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="dc_Fraction_table_tr" >
                    <td>
                    </td>
                    <td></td>
                    <td>
                        B值<input class="dc_Fraction_table_input" type="text" data-text="Value2"  />
                    </td>
                </tr>
            </table>
            <div class="dc_AdvancedTeech_AutoSize">
                <label>
                    <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "请输入分数值",
            bodyHeight: "auto",
            bodyClass: "dc_FractionElement",
            bodyHtml: FractionHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建输入月经史值对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    FourValuesDialog: function (options, ctl, isInsertMode, ele) {
        // if (!options || typeof (options) != "object") {
        //     options = ctl.getFontObject();
        // }
        if (options == null) {
            return false;
        }

        let arr = this.stringToObject(options.Values);
        var FourValuesHtml = `
            <table cellspacing="0">
                <tr class="dc_FourValues_table_tr1" >
                    <td rowspan="2" class="dc_FourValues_table_td1_value1" >初潮年龄(岁)<br /><input type="text" placeholder="初潮年龄(岁)" data-text="Value1" /></td>
                    <td class="dc_FourValues_table_td1_value_border" >经期(天)<br /><input type="text" placeholder="经期(天)"  data-text="Value2" /></td>
                    <td class="dc_FourValues_table_td2_value" rowspan="2" >末次月经/<br />绝经年龄(岁) <br /><input type="text" placeholder="末次月经/绝经年龄(岁)" data-text="Value4"/></td>
                </tr>
                <tr class="dc_FourValues_table_tr2">
                    <td class="dc_FourValues_table_tr2_td">周期(天)<br /><input type="text" placeholder="周期(天)" data-text="Value3"/></td>
                </tr>      
            </table>
            <div class="dc_AdvancedTeech_AutoSize">
                <label>
                    <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "请输入月经史值",
            bodyHeight: 'auto',
            dialogContainerBodyWidth: 500,
            bodyClass: "dc_FourValuesElement",
            bodyHtml: FourValuesHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建输入月经史值2对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    FourValues1Dialog: function (options, ctl, isInsertMode, ele) {
        // if (!options || typeof (options) != "object") {
        //     options = ctl.getFontObject();
        // }
        if (options == null) {
            return false;
        }

        let arr = this.stringToObject(options.Values);
        var FourValues1Html = `
        <table border="0" cellspacing="0">
            <tbody>
                <tr>
                    <td class="dc_FourValues_table_tr1_td1 dc_FourValues_table_border_right">初潮年龄(岁)</td>
                    <td class="dc_FourValues_table_border_left" >经期(天)</td>
                </tr>
                <tr>
                    <td class="dc_FourValues_table_border_right dc_FourValues_table_border_bottom">
                        <input type="text" data-text="Value1" ></td>
                    <td
                        class="dc_FourValues_table_border_left dc_FourValues_table_border_bottom">
                        <input type="text" data-text="Value2"></td>
                </tr>
                <tr>
                    <td  class="dc_FourValues_table_border_right dc_FourValues_table_border_right_top">末次月经/绝经年龄(岁)</td>
                    <td class="dc_FourValues_table_border_left dc_FourValues_table_border_left_top">周期(天)</td>
                </tr>
                <tr>
                    <td class="dc_FourValues_table_border_right" >
                        <input type="text" data-text="Value3" >
                        </td>
                    <td class="dc_FourValues_table_border_left" >
                        <input type="text"  data-text="Value4" >
                        </td>
                </tr>
            </tbody>
        </table>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "请输入月经史值",
            bodyHeight: "auto",
            dialogContainerBodyWidth: 500,
            bodyClass: "dc_FourValues1Element",
            bodyHtml: FourValues1Html,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
    * 创建龋齿公式对话框
    * @param options 医学表达式属性
    * @param ctl 编辑器元素
    * @param isInsertMode 是否是插入模式
    */
    DentalCariesDialog: function (options, ctl, isInsertMode, ele) {
        if (options == null) {
            return false;
        }

        let arr = this.stringToObject(options.Values);
        var FourValues1Html = `
        <table border="0" cellspacing="0">
            <tbody>
                <tr>
                    <td class="dc_FourValues_table_tr1_td1 dc_FourValues_table_border_right">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="dc_FourValues_table_border_left" >&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="dc_FourValues_table_border_right dc_FourValues_table_border_bottom">
                        <input type="text" data-text="Value1" ></td>
                    <td
                        class="dc_FourValues_table_border_left dc_FourValues_table_border_bottom">
                        <input type="text" data-text="Value2"></td>
                </tr>
                <tr>
                    <td  class="dc_FourValues_table_border_right dc_FourValues_table_border_right_top">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="dc_FourValues_table_border_left dc_FourValues_table_border_left_top">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="dc_FourValues_table_border_right" >
                        <input type="text" data-text="Value3" >
                        </td>
                    <td class="dc_FourValues_table_border_left" >
                        <input type="text"  data-text="Value4" >
                        </td>
                </tr>
            </tbody>
        </table>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "请输入龋齿值",
            bodyHeight: "auto",
            dialogContainerBodyWidth: 500,
            bodyClass: "dc_FourValues1Element",
            bodyHtml: FourValues1Html,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
     * 创建输入月经史值2对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    FourValues2Dialog: function (options, ctl, isInsertMode, ele) {
        // if (!options || typeof (options) != "object") {
        //     options = ctl.getFontObject();
        // }
        if (options == null) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var FourValues2Html = `
        <table >
            <tr>
                <td></td>
                <td align="center" class="dc_FourValues_table_td" >
                 <label>
                    经期（天）：
                    <input data-text="Value2" class="dc_FourValues_table_input" type="text" autocomplete="off"/></td>
                </label>
                    <td></td>
            </tr>
            <tr>
                <td align="center" class="dc_FourValues_table_td">
                <label>
                    初潮年龄(岁)：
                    <input  data-text="Value1"   class="dc_FourValues_table_input" type="text" autocomplete="off"/></td>
                 </label>
                    <td></td>
                <td align="center" class="dc_FourValues_table_td">

                 <label>
                    周期（天）：
                    <input data-text="Value3"  class="dc_FourValues_table_input" type="text" autocomplete="off"/>
                 </label>
                    
                    </td>
            </tr>
            <tr>
                <td></td>
                <td align="center" class="dc_FourValues_table_td">
                    <label>
                    末次月经/绝经年龄(岁)：
                    <input data-text="Value4"  class="dc_FourValues_table_input" type="text"  autocomplete="off"/></td>
                 </label>
               
                    <td></td>
            </tr>
        </table>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        <div class="dc_LeftLine"></div>
        <div class="dc_RightLine"></div>
        `;
        var dialogOptions = {
            title: "请输入月经史值",
            bodyHeight: "auto",
            bodyClass: "dc_FourValues2Element",
            bodyHtml: FourValues2Html,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建输入月经史值4对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    ThreeValuesDialog: function (options, ctl, isInsertMode, ele) {
        // if (!options || typeof (options) != "object") {
        //     options = ctl.getFontObject();
        // }
        if (options == null) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var ThreeValuesHtml = `
            <table cellspacing="0">
                <tr >
                    <td rowspan="2">
                        A值<input type="text" data-text="Value1"/>
                    </td>
                    <td rowspan="2" class="dc_FourValues_table_value1_td">
                    <strong>/</strong>
                    </td>
                    <td class="dc_FourValues_table_value2_td">
                        B值<br />
                    <input type="text" data-text="Value2"/>
                    </td>
                </tr>
                <tr >
                    <td class="dc_FourValues_table_value1_td3">
                        C值<br />
                    <input type="text"  data-text="Value3"/>
                    </td>
                </tr>
            </table>
            <div class="dc_AdvancedTeech_AutoSize">
                <label>
                    <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                </label>
            </div>`;
        var dialogOptions = {
            title: "请输入月经史值",
            bodyHeight: "auto",
            bodyClass: "dc_ThreeValuesElement",
            bodyHtml: ThreeValuesHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建输入瞳孔图值对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    PupilDialog: function (options, ctl, isInsertMode, ele) {
        if (options == null) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var PupilHtml = `
            <table cellspacing="5">
                <tr>
                    <td><input type="text" data-text="Value1" /></td>
                    <td></td>
                    <td><input type="text" data-text="Value2"/></td>
                </tr>
                <tr>
                    <td><input type="text" data-text="Value3"/></td>
                    <td><input type="text" data-text="Value4"/></td>
                    <td><input type="text" data-text="Value5"/></td>
                </tr>
                <tr>
                    <td><input type="text" data-text="Value6"/></td>
                    <td></td>
                    <td><input type="text" data-text="Value7"/></td>
                </tr>       
            </table>
            <div class="dc_AdvancedTeech_AutoSize">
                <label>
                    <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "请输入瞳孔图值",
            bodyHeight: "auto",
            bodyClass: "PupilElement",
            bodyHtml: PupilHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
        * 创建输入视野图对话框
        * @param options 医学表达式属性
        * @param ctl 编辑器元素
        * @param isInsertMode 是否是插入模式
        */
    PerimetricDialog: function (options, ctl, isInsertMode, ele) {
        if (options == null) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var PerimetricHtml = `
        <table >
            <tr>
                <td></td>
                <td align="center" class="dc_Perimetric_table_td">
                 <label>
                    <div></div>
                    <input data-text="Value2" type="text"  autocomplete="off"/></td>
                </label>
                    <td></td>
            </tr>
            <tr>
                <td align="center" class="dc_Perimetric_table_td" >
                <label>
                    <input  data-text="Value1"  type="text" autocomplete="off"/></td>
                 </label>
                    <td></td>
                <td align="center" class="dc_Perimetric_table_td">

                 <label>
                    <input data-text="Value3" type="text"   autocomplete="off"/>
                 </label>
                    
                    </td>
            </tr>
            <tr>
                <td></td>
                <td align="center" class="dc_Perimetric_table_td">
                    <label>
                    <input data-text="Value4" type="text"  autocomplete="off"/></td>
                 </label>
               
                    <td></td>
            </tr>
        </table>
        <div class="dc_LeftLine"></div>
        <div class="dc_RightLine"></div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "请输入视野图值",
            bodyHeight: "auto",
            bodyClass: "dc_PerimetricDom",
            bodyHtml: PerimetricHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            console.log(options, '==================');
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
     * 创建输入光定位值对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    LightPositioningDialog: function (options, ctl, isInsertMode, ele) {
        if (options == null) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var LightPositioningHtml = `
           <table  cellspacing="5">
                <tr>
                    <td><input type="text" data-text="Value1"/></td>
                    <td><input type="text" data-text="Value2"/></td>
                    <td><input type="text" data-text="Value3"/></td>
                </tr>
                <tr>
                    <td><input type="text" data-text="Value4"/></td>
                    <td><input type="text" data-text="Value5"td>
                    <td><input type="text" data-text="Value6"/></td>
                </tr>
                <tr>
                    <td><input type="text" data-text="Value7"/></td>
                    <td><input type="text" data-text="Value8"/></td>
                    <td><input type="text" data-text="Value9"/></td>
                </tr>       
            </table>
            <div class="dc_AdvancedTeech_AutoSize">
                <label>
                    <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "请输入光定位值",
            bodyHeight: "auto",
            bodyClass: "dc_LightPositioningElement",
            bodyHtml: LightPositioningHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(arr, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var _data = GetOrChangeData(dcPanelBody);
            options.Values = that.ObjectToString(_data);
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 创建恒牙牙位图对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    PermanentTeethBitmapDialog: function (options, ctl, isInsertMode, ele) {
        console.log(options, "=======options");
        if (options == null) {
            return false;
        }
        let arr = options.Values && this.stringToObject(options.Values);
        var PermanentTeethBitmapDialogHtml = `
        <div unselectable="on">
            <!--Table结构，cellspacing控制中间空行 -->
            <div>
                <table cellspacing="0">
                    <tr class="dc_PermanentTeethBitmap_tr1" >
                        <td><span class="dc_PermanentTeethBitmap_span1">上颌</span></td>
                    </tr>
                    <tr class="dc_PermanentTeethBitmap_tr2">
                        <td><span class="dc_PermanentTeethBitmap_span2">牙<br />面</span></td>
                    </tr>
                    <tr class="dc_PermanentTeethBitmap_tr3">
                        <td><span>第<br />三<br />磨<br />牙</span></td>
                        <td><span>第<br />二<br />磨<br />牙</span></td>
                        <td><span>第<br />一<br />磨<br />牙</span></td>
                        <td><span>第<br />二<br />前<br />磨<br />牙</span></td>
                        <td><span>第<br />一<br />前<br />磨<br />牙</span></td>
                        <td><span>尖<br />牙</span></td>
                        <td><span>侧<br />切<br />牙</span>
                        </td>
                        <td><span>中<br />切<br />牙</span>
                        </td>
                        <td><span>中<br />切<br />牙</span>
                        </td>
                        <td><span>侧<br />切<br />牙</span>
                        </td>
                        <td><span>尖<br />牙</span></td>
                        <td><span>第<br />一<br />前<br />磨<br />牙</span></td>
                        <td><span>第<br />二<br />前<br />磨<br />牙</span></td>
                        <td><span>第<br />一<br />磨<br />牙</span></td>
                        <td><span>第<br />二<br />磨<br />牙</span></td>
                        <td><span>第<br />三<br />磨<br />牙</span></td>
                    </tr>
                    <tr class="dc_PermanentTeethBitmap_tr4"  id="dc_P-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_tr4"  id="dc_L-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_tr4"  id="dc_B-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_tr4"  id="dc_D-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_tr4"  id="dc_O-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_tr4"  id="dc_M-permanent-tooth"></tr>
                </table>
            </div>
            <hr class="dc_PermanentTeethBitmap_hr" />
            <!--Table结构，cellspacing控制中间空行 -->
            <div>
                <table class="dc_PermanentTeethBitmap_table2"  cellspacing="0">
                    <tr class="dc_PermanentTeethBitmap_table2_tr1">
                        <td><span >右</span></td>
                    </tr>
                    <tr class="dc_PermanentTeethBitmap_table2_tr2">
                        <td><span>左</span></td>
                    </tr>
                    <tr class="dc_PermanentTeethBitmap_table2_tr3" id="dc_valueNumberBox1"/>
                    <tr class="dc_PermanentTeethBitmap_table2_tr4" id="dc_valueNumberBox2"/>
                </table>
            </div>
            <hr class="dc_PermanentTeethBitmap_hr"/>
            <!--Table结构，cellspacing控制中间空行 -->
            <div>
                <table class="dc_PermanentTeethBitmap_table3"  cellspacing="0">
                    <tr class="dc_PermanentTeethBitmap_table3_tr1">
                        <td><span>牙<br />面</span></td>
                    </tr>
                    <tr class="dc_PermanentTeethBitmap_table3_tr"  id="dc_M-bottom-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_table3_tr"  id="dc_O-bottom-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_table3_tr"  id="dc_D-bottom-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_table3_tr"  id="dc_B-bottom-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_table3_tr"  id="dc_L-bottom-permanent-tooth"></tr>
                    <tr class="dc_PermanentTeethBitmap_table3_tr"  id="dc_P-bottom-permanent-tooth"></tr>
                </table>
            </div>
            <center class="dc_PermanentTeethBitmap_center">
                <span>下颌</span>
            </center>
        </div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "恒牙牙位图",
            bodyHeight: 470,
            dialogContainerBodyWidth: 800,
            bodyClass: "dc_PermanentTeethBitmapElement",
            bodyHtml: PermanentTeethBitmapDialogHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let nameList = NAMELIST;
        let idTypeList = IDTYPELIST;
        let idList = IDLIST;
        let PermanentTeethTop = PERMANENTTEETHTOP;
        let PermanentTeethBottom = PERMANENTTEETHBOTTOM;
        let PermanentTeethtopList = [...PERMANENTTEETHTOP, ...PERMANENTTEETHBOTTOM];
        PermanentTeethtopList.filter((item) => {
            this.PermanentToothPosition(
                item.idPrefix,
                item.parentId,
                item.teethKey,
                item.isTop,
                ctl
            );
        });
        this.PermanentToothValueNumber("#dc_valueNumberBox1", 0, ctl);
        this.PermanentToothValueNumber("#dc_valueNumberBox2", 16, ctl);
        SetValues(arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        dcPanelBody.find("td>input.inp").click(function () {
            if (this.getAttribute("dccheck") == "true") {
                for (var n = 1; n < 33; n++) {
                    if (this.name == "a" + n + "") {
                        this.style.cssText = `width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color:${nameList.includes(this.name) ? "#fff" : "#d7e4f2"
                            } `;
                        this.setAttribute("dccheck", "false");
                    } else if (this.getAttribute("id") == "Value" + n + "") {
                        idTypeList.filter((item) => {
                            ctl.ownerDocument
                                .getElementById(item + n + "")
                                .setAttribute("dccheck", "false");
                            ctl.ownerDocument.getElementById(
                                item + n + ""
                            ).style.cssText = `width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: ${idList.includes(this.getAttribute("id")) ? "#fff;" : "#d7e4f2;"
                            }`;
                        });
                    } else {
                    }
                }
            } else {
                for (var n = 1; n < 33; n++) {
                    if (this.name == "a" + n + "") {
                        ctl.ownerDocument.getElementById("Value" + n + "").style.cssText =
                            "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #0078d7;";
                        ctl.ownerDocument
                            .getElementById("Value" + n + "")
                            .setAttribute("dccheck", "true");
                    } else {
                        this.style.cssText =
                            "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #0078d7;";
                        this.setAttribute("dccheck", "true");
                    }
                }
            }
        });
        function successFun() {
            let newArr = GetCurrentDatas();
            let str = "";
            newArr.filter((item, index) => {
                str += `Value${index}:${item};`;
            });
            options.Values = str;
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
        function GetCurrentDatas() {
            var jsonObj = new Array();
            for (var n = 0; n < 32; n++) {
                var vall = "";
                var i = n + 1;
                if (
                    ctl.ownerDocument
                        .getElementById("Value" + i + "")
                        .getAttribute("dccheck") == "true"
                ) {
                    vall = ctl.ownerDocument.getElementById("Value" + i + "").value;
                    //从下标1开始计算
                    for (var j = 1; j < idTypeList.length; j++) {
                        if (
                            ctl.ownerDocument
                                .getElementById(idTypeList[j] + i + "")
                                .getAttribute("dccheck") == "true"
                        ) {
                            vall += ctl.ownerDocument.getElementById(
                                idTypeList[j] + i + ""
                            ).value;
                        }
                    }
                    jsonObj[n + 1] = vall;
                } else {
                    jsonObj[n + 1] = "";
                }
            }
            return jsonObj;
        }
        function SetValues(values) {
            //[DUWRITER5_0-3772] 20241028 lxy 修复恒牙牙位图设置值时，不能正确显示值的bug
            var ValueArr = Object.keys(values);
            for (var n = 0; n <= 32; n++) {
                for (var j = 0; j < ValueArr.length; j++) {
                    //当值存在时，设置值
                    if (values[ValueArr[j]] && ValueArr[j] == "Value" + (n + 1)) {
                        var value = values[ValueArr[j]];
                        var id = "Value" + (n + 1);
                        ctl.ownerDocument.getElementById(id).style.cssText =
                            "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #0078d7;";
                        ctl.ownerDocument.getElementById(id).setAttribute("dccheck", "true");
                        (n <= 15 ? PermanentTeethTop : PermanentTeethBottom).filter(
                            (item) => {
                                if (value.indexOf(item.teethKey) >= 0) {
                                    ctl.ownerDocument.getElementById(
                                        item.idPrefix + (n + 1)
                                    ).style.cssText =
                                        "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #0078d7;";
                                    ctl.ownerDocument
                                        .getElementById(item.idPrefix + (n + 1))
                                        .setAttribute("dccheck", "true");
                                }
                            }
                        );
                    }
                }
            }
        }
    },

    /**
     * 创建乳牙牙位图对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    DeciduousTeechDialog: function (options, ctl, isInsertMode, ele) {
        if (options == null) {
            return false;
        }
        let arr = options.Values && this.stringToObject(options.Values);
        var DeciduousTeechHtml = `<div unselectable="on">
                <!--Table结构，cellspacing控制中间空行 -->
                <div>
                    <table class="dc_DeciduousTeech_table1" cellspacing="0" >
                        <tr class="dc_DeciduousTeech_table1_tr1"><td><span>上颌</span></td></tr>
                        <tr class="dc_DeciduousTeech_table1_tr2"><td><span>牙<br />面</span></td></tr>
                        <tr class="dc_DeciduousTeech_table1_tr3">
                            <td><span>第<br />二<br />乳<br />磨<br />牙</span></td>
                            <td><span>第<br />一<br />乳<br />磨<br />牙</span></td>
                            <td><span>乳<br />尖<br />牙</span></td>
                            <td><span>乳<br />侧<br />切<br />牙</span></td>
                            <td><span>乳<br />中<br />切<br />牙</span></td>
                            <td><span>乳<br />中<br />切<br />牙</span></td>
                            <td><span>乳<br />侧<br />切<br />牙</span></td>
                            <td><span>乳<br />尖<br />牙</span></td>
                            <td><span>第<br />一<br />乳<br />磨<br />牙</span></td>
                            <td><span>第<br />二<br />乳<br />磨<br />牙</span></td>
                        </tr>
                        <tr class="dc_DeciduousTeech_table1_tr4" id="dc_P-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table1_tr5" id="dc_L-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table1_tr6" id="dc_B-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table1_tr7" id="dc_D-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table1_tr8" id="dc_O-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table1_tr9" id="dc_M-teeth-list"></tr>
                    </table>
                </div>
                <hr class="dc_DeciduousTeech_hr" />
                <div>
                    <table class="dc_DeciduousTeech_table2" cellspacing="0">
                        <tr class="dc_DeciduousTeech_table2_tr1" ><td><span>右</span></td></tr>
                        <tr class="dc_DeciduousTeech_table2_tr2"><td><span>左</span></td></tr>
                        <tr class="dc_DeciduousTeech_table2_tr3" id="dc_roman-top"></tr>
                        <tr class="dc_DeciduousTeech_table2_tr4" id="dc_roman-bottom"></tr> 
                    </table>
                </div>
                <hr class="dc_DeciduousTeech_hr" />
                <!--Table结构，cellspacing控制中间空行 -->
                <div>
                    <table class="dc_DeciduousTeech_table3"  cellspacing="0">
                        <tr class="dc_DeciduousTeech_table3_tr1"><td><span >牙<br />面</span></td></tr>
                        <tr class="dc_DeciduousTeech_table3_tr2" id="dc_M-bottom-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table3_tr3" id="dc_O-bottom-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table3_tr4" id="dc_D-bottom-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table3_tr5" id="dc_B-bottom-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table3_tr6" id="dc_L-bottom-teeth-list"></tr>
                        <tr class="dc_DeciduousTeech_table3_tr7" id="dc_P-bottom-teeth-list"></tr>
                    </table>
                </div>
                <center class="dc_DeciduousTeech_center" >
                    <span>下颌</span>
                </center>
            </div>
            <div class="dc_AdvancedTeech_AutoSize">
                <label>
                    <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "乳牙牙位图",
            bodyHeight: "auto",
            dialogContainerBodyWidth: 800,
            bodyClass: "DeciduousTeechElement",
            bodyHtml: DeciduousTeechHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let teethPostionTopObj = TEETHPOSTIONTOPOBJ;
        //上颌牙位置
        teethPostionTopObj.filter((item) => {
            this.teethPosition(item.idPrefix, item.parentId, item.teethKey, true, ctl);
        });
        // 下颌牙位置
        let teethPostionBottomObj = TEETHPOSTIONBOTTOMOBJ;
        //下颌牙位置
        teethPostionBottomObj.filter((item) => {
            this.teethPosition(item.idPrefix, item.parentId, item.teethKey, false, ctl);
        });
        //罗马牙位（value值）结构渲染
        this.romanteethPosition(1, "#dc_roman-top", ctl);
        this.romanteethPosition(11, "#dc_roman-bottom", ctl);
        //值复显
        SetValues(arr);

        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        dcPanelBody.find("td>input.inp").click(function () {
            let nameArr = [
                "a1",
                "a3",
                "a5",
                "a6",
                "a8",
                "a10",
                "a11",
                "a13",
                "a15",
                "a16",
                "a18",
                "a20",
            ];
            let attributeArr = [
                "Value1",
                "Value3",
                "Value5",
                "Value6",
                "Value8",
                "Value10",
                "Value11",
                "Value13",
                "Value15",
                "Value16",
                "Value18",
                "Value20",
            ];
            if (this.getAttribute("dccheck") == "true") {
                for (var n = 1; n < 21; n++) {
                    if (this.name == "a" + n + "") {
                        this.style.cssText = `width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: ${nameArr.includes(this.name) ? "#fff;" : "#d7e4f2;"
                            };`;
                        this.setAttribute("dccheck", "false");
                    } else if (this.getAttribute("id") == "Value" + n + "") {
                        ["Value", "a", "b", "c", "d", "e", "f"].filter((item) => {
                            ctl.ownerDocument
                                .getElementById(item + n + "")
                                .setAttribute("dccheck", "false");
                            ctl.ownerDocument.getElementById(
                                item + n + ""
                            ).style.cssText = `width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color:${attributeArr.includes(this.getAttribute("id"))
                                ? "#fff;"
                                : "#d7e4f2;"
                            } `;
                        });
                    }
                }
            } else {
                for (var n = 1; n < 21; n++) {
                    if (this.name == "a" + n + "") {
                        ctl.ownerDocument.getElementById("Value" + n + "").style.cssText =
                            "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #0078d7;";
                        ctl.ownerDocument
                            .getElementById("Value" + n + "")
                            .setAttribute("dccheck", "true");
                    } else {
                        this.style.cssText =
                            "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #0078d7;";
                        this.setAttribute("dccheck", "true");
                    }
                }
            }
        });
        function successFun() {
            let newArr = GetCurrentDatas();
            let str = "";
            newArr.filter((item, index) => {
                str += `Value${index}:${item};`;
            });
            options.Values = str;
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
        function GetCurrentDatas() {
            var jsonObj = new Array();
            for (var n = 0; n < 20; n++) {
                var vall = "";
                var i = n + 1;
                if (
                    ctl.ownerDocument
                        .getElementById("Value" + i + "")
                        .getAttribute("dccheck") == "true"
                ) {
                    vall = ctl.ownerDocument.getElementById("Value" + i + "").value;
                    ["a", "b", "c", "d", "e", "f"].filter((item) => {
                        if (
                            ctl.ownerDocument
                                .getElementById(item + i + "")
                                .getAttribute("dccheck") == "true"
                        ) {
                            vall += ctl.ownerDocument.getElementById(item + i + "").value;
                        }
                    });
                    jsonObj[n + 1] = vall;
                } else {
                    jsonObj[n + 1] = "";
                }
            }
            return jsonObj;
        }
        function SetValues(values) {
            //[DUWRITER5_0-3772] 20241028 lxy 修复恒牙牙位图设置值时，不能正确显示值的bug
            var ValueArr = Object.keys(values);
            for (var i = 0; i < 20; i++) {
                for (var j = 0; j < ValueArr.length; j++) {
                    //当值存在时，设置值
                    var id = "Value" + (i + 1) + "";
                    var value = values[ValueArr[j]];

                    if (values[ValueArr[j]] && ValueArr[j] == id) {
                        ctl.ownerDocument.getElementById(id).style.cssText =
                            "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #0078d7;";
                        ctl.ownerDocument.getElementById(id).setAttribute("dccheck", "true");
                        let teethKeyObject =
                            i <= 9
                                ? {
                                    //上颌牙
                                    P: "a",
                                    L: "b",
                                    B: "c",
                                    D: "d",
                                    O: "e",
                                    M: "f",
                                }
                                : {
                                    //下颌牙
                                    M: "a",
                                    O: "b",
                                    D: "c",
                                    B: "d",
                                    L: "e",
                                    P: "f",
                                };
                        for (var key in teethKeyObject) {
                            if (value.indexOf(key) >= 0) {
                                ctl.ownerDocument.getElementById(
                                    teethKeyObject[key] + (i + 1) + ""
                                ).style.cssText =
                                    "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #0078d7;";
                                ctl.ownerDocument
                                    .getElementById(teethKeyObject[key] + (i + 1) + "")
                                    .setAttribute("dccheck", "true");
                            }
                        }

                    }
                }
            }
        }
    },

    /**
     * 病变上牙对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     */
    DiseasedTeethTopDialog: function (options, ctl, isInsertMode, ele) {
        if (!isInsertMode && (!options || typeof options != "object")) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var DiseasedTeethTopHtml = `
        <div class="dc_DiseasedTeethTop_box" >
            <div class="dc_use_value1_block dc_use_value1_1"></div>
            <div  class="dc_use_value1_block dc_use_value1_2"></div>
            <div class="dc_use_value1_3"></div>
            <input class="dc_use_value1_block dc_use_value1_4" type="text" name="Value1" data-text="Value1" >
            <input class="dc_DiseasedTeethTop_input1" type="text" name="Value2" data-text="Value2" >
            <input class="dc_DiseasedTeethTop_input2" type="text" name="Value3" data-text="Value3" >
        </div>
        <label class="dc_use_value1">
            <input type="checkbox" id="dc_use_value1_check">
            使用1号位置值
        </label>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "病变上牙设置",
            bodyHeight: 360,
            bodyClass: "dc_DiseasedTeethTop",
            bodyHtml: DiseasedTeethTopHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        // //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        GetOrChangeData(dcPanelBody, arr);
        //获取是否需要隐藏value1位置的元素:true隐藏
        var isHideDCUseValue1Block = arr && arr.ValueX && (arr.ValueX === '1');
        isHideDCUseValue1Block && dcTrrigerUseDomList(true);
        jQuery(dcPanelBody).find('#dc_use_value1_check').attr("checked", !isHideDCUseValue1Block);

        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        // 设置隐藏或显示value1位置的元素
        function dcTrrigerUseDomList(isHide) {
            var dcUseDomList = ctl.ownerDocument.querySelectorAll(".dc_use_value1_block");
            dcUseDomList && dcUseDomList.length && dcUseDomList.forEach(item => {
                item && (item.style.display = isHide ? "none" : "block");
            });
        }

        jQuery(dcPanelBody).on('change', '#dc_use_value1_check', function () {
            if (jQuery(this).is(':checked')) {
                // 使用值1
                dcTrrigerUseDomList(false);
            } else {
                // 不使用值1
                dcTrrigerUseDomList(true);
            }
        });

        function successFun() {
            let arr = Object.values(GetOrChangeData(dcPanelBody));
            let str = "";
            arr.filter((item, index) => {
                str += `Value${index + 1 + ""}:${item};`;
            });
            str += 'ValueX:' + (jQuery(dcPanelBody).find('#dc_use_value1_check').is(':checked') ? '' : '1') + ";";
            options.Values = str;
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            console.log(options, '==========option');
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 病变下牙对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     */
    DiseasedTeethBottonDialog: function (options, ctl, isInsertMode, ele) {
        if (!isInsertMode && (!options || typeof options != "object")) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var DiseasedTeethBottonHtml = `
        <div class="dc_DiseasedTeethBotton_box" >
            <div class="dc_DiseasedTeethBotton_value1_box" >
                <input  type="text" name="Value1" data-text="Value1" >
            </div>
            <div class="dc_DiseasedTeethBotton_value2_box" >
                    <input type="text" name="Value2" data-text="Value2" >
                <p></p>
                    <input type="text" name="Value3" data-text="Value3" >
            </div>
        </div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "病变下牙设置",
            bodyHeight: 200,
            bodyClass: "dc_DiseasedTeethBotton",
            bodyHtml: DiseasedTeethBottonHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        // //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        GetOrChangeData(dcPanelBody, arr);
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            let arr = Object.values(GetOrChangeData(dcPanelBody));
            let str = "";
            arr.filter((item, index) => {
                str += `Value${index + 1 + ""}:${item};`;
            });
            // console.log(str)
            options.Values = str;
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
     * 固定桥牙位图
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     */

    StationaryBridgeTeethDialog: function (options, ctl, isInsertMode, ele) {
        if (!isInsertMode && (!options || typeof options != "object")) {
            return false;
        }
        var StationaryBridgeTeethHtml = `
        <div class="dc_StationaryBridgeTeethBox">
            <div class="dc_StationaryBridgeTeethTop">上颌</div>
            <div class="dc_StationaryBridgeTeethCenter" id="dc_StationaryBridgeTeethCenter">
                <div class="dc_StationaryBridgeTeethCenter_Top">
                    <div id="dc_StationaryBridgeTeethCenter_Top_value1"></div>
                    <div id="dc_StationaryBridgeTeethCenter_Top_value2"></div>
                </div>
                <div class="dc_StationaryBridgeTeethCenter_Content">
                   <div class="dc_StationaryBridgeTeethCenter_Content_left">
                        <div valueNumber="value1" class="dc_StationaryBridgeTeethCenter_Content_clear">清</div>
                        <div valueNumber="value3" class="dc_StationaryBridgeTeethCenter_Content_clear">清</div>
                   </div>
                    <div class="dc_StationaryBridgeTeethCenter_Center">
                        <div id="dc_StationaryBridgeTeethCenter_Center_value1"></div>
                        <div id="dc_StationaryBridgeTeethCenter_Center_value2"></div>
                        <div class="dc_StationaryBridgeTeethCenter_Center_line"></div>
                        <div id="dc_StationaryBridgeTeethCenter_Center_value3"></div>
                        <div id="dc_StationaryBridgeTeethCenter_Center_value4"></div>
                    </div>
                   <div class="dc_StationaryBridgeTeethCenter_Content_right">
                        <div valueNumber="value2" class="dc_StationaryBridgeTeethCenter_Content_clear">清</div>
                        <div valueNumber="value4" class="dc_StationaryBridgeTeethCenter_Content_clear">清</div>
                   </div>
                </div>
                <div class="dc_StationaryBridgeTeethCenter_Bottom">
                    <div id="dc_StationaryBridgeTeethCenter_Top_value3"></div>
                    <div id="dc_StationaryBridgeTeethCenter_Top_value4"></div>
                </div>
            </div>
            <div class="dc_StationaryBridgeTeethBottom">下颌</div>
        </div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "固定桥牙位图",
            bodyHeight: 350,
            dialogContainerBodyWidth: 460,
            bodyClass: "StationaryBridgeTeeth",
            bodyHtml: StationaryBridgeTeethHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        //按钮数据
        var buttonList = TEETHBUTTONLIST;
        //格式化按钮数据
        var toothButtonObject = {};
        var newbuttonList = JSON.parse(JSON.stringify(buttonList));//防止污染源数据

        if (options.Values) {
            let arr = this.stringToObject(options.Values);
            Object.keys(arr).forEach((item) => {
                var newItem = item.toLowerCase();
                var valueArr = arr[item].split('');
                //渲染数字
                if (newItem === 'value1' || newItem === 'value3') {
                    valueArr = arr[item].split('').reverse();
                }
                newbuttonList.forEach((btnItem, index) => {
                    btnItem.value = valueArr[index];
                });
                toothButtonObject[`${newItem}`] = JSON.parse(JSON.stringify(newbuttonList));
            });
        } else {
            //当options中没有Values值时，默认渲染4个值（防止牙位按钮无法渲染）
            let arr = new Array(4).fill().map((_, index) => ("value" + (index + 1))); // 这将创建一个包含四个元素且值为递增的数组
            arr.forEach(item => {
                toothButtonObject[item] = JSON.parse(JSON.stringify(newbuttonList));
            });
        }


        //渲染点击牙位按钮
        Object.keys(toothButtonObject).forEach((key) => {
            var itemTootoothBox = ctl.ownerDocument.getElementById(`dc_StationaryBridgeTeethCenter_Top_${key}`);//按钮
            var itemTootoothNumbox = ctl.ownerDocument.getElementById(`dc_StationaryBridgeTeethCenter_Center_${key}`);//数字
            itemTootoothBox.innerHTML = itemTootoothNumbox.innerHTML = "";
            var strs = ["A", "B", "C", "D", "E"];
            if (['value1', 'value3'].indexOf(key) !== -1) {
                //反向渲染
                for (var i = toothButtonObject[key].length - 1; i >= 0; i--) {
                    var item = toothButtonObject[key][i];
                    itemTootoothBox.innerHTML += `<div valueNumber=${key} valueId=${item.id} valueData="${item.value}" class="dc_StationaryBridgeTeethButton ${item.value === '+' ? "dc_jia_teeth_button" : (item.value === '-' ? "dc_jian_teeth_button" : (strs.indexOf(item.value) >= 0 ? "dc_deciduous_teeth_button" : ""))}">${item.text}</div>`;
                    itemTootoothNumbox.innerHTML += `<div id="dc_StationaryBridgeTeethNum${key}${item.id}" class="dc_StationaryBridgeTeethNum ${item.value === '+' ? "dc_jia_teeth_StationaryBridgeTeethNum" : (item.value === '-' ? "dc_jian_teeth_StationaryBridgeTeethNum" : "")}">${item.id}</div>`;
                }
            } else {
                //正向渲染
                toothButtonObject[key].forEach((item) => {
                    itemTootoothBox.innerHTML += `<div valueNumber=${key} valueId=${item.id} valueData="${item.value}" class="dc_StationaryBridgeTeethButton  ${item.value === '+' ? "dc_jia_teeth_button" : (item.value === '-' ? "dc_jian_teeth_button" : (strs.indexOf(item.value) >= 0 ? "dc_deciduous_teeth_button" : ""))}">${item.text}</div>`;
                    itemTootoothNumbox.innerHTML += `<div id="dc_StationaryBridgeTeethNum${key}${item.id}" class="dc_StationaryBridgeTeethNum ${item.value === '+' ? "dc_jia_teeth_StationaryBridgeTeethNum" : (item.value === '-' ? "dc_jian_teeth_StationaryBridgeTeethNum" : "")}" >${item.id}</div>`;
                });
            }
        });


        var dcStationaryBridgeTeethCenter = ctl.ownerDocument.getElementById('dc_StationaryBridgeTeethCenter');
        dcStationaryBridgeTeethCenter.addEventListener('click', eventClickDispatch);

        //固定桥对话框点击事件派发
        function eventClickDispatch(e) {
            if (e && e.target && e.target.nodeName && e.target.nodeName === "DIV") {
                if (e.target.className.indexOf("dc_StationaryBridgeTeethButton") > -1) {
                    //点击牙位按钮
                    clickTeethButton(e.target);
                } else if (e.target.className.indexOf("dc_StationaryBridgeTeethCenter_Content_clear") > -1) {
                    clickClearButton(e.target);
                }
            }
        }
        //清空选中内容
        function clickClearButton(currentDom) {
            var valueNumber = currentDom.getAttribute('valuenumber');
            toothButtonObject[valueNumber] = JSON.parse(JSON.stringify(buttonList));
            var ItemValues = jQuery(ctl).find(`#dc_StationaryBridgeTeethCenter_Top_${valueNumber} .dc_StationaryBridgeTeethButton`);
            ItemValues.removeClass("dc_jia_teeth_button dc_jian_teeth_button");
            // 清空边框标识
            var ItemBorder = jQuery(ctl).find(`#dc_StationaryBridgeTeethCenter_Center_${valueNumber} .dc_jia_teeth_StationaryBridgeTeethNum,#dc_StationaryBridgeTeethCenter_Center_${valueNumber} .dc_jian_teeth_StationaryBridgeTeethNum`);
            ItemBorder.removeClass("dc_jia_teeth_StationaryBridgeTeethNum dc_jian_teeth_StationaryBridgeTeethNum");

        }

        //牙位图点击事件
        function clickTeethButton(currentDom) {
            let valueNumber = currentDom.getAttribute("valueNumber");
            let valueId = currentDom.getAttribute("valueId");
            var currentValueArr = JSON.parse(JSON.stringify(toothButtonObject[valueNumber]));//获取当前value数组
            var changeBorderDom = ctl.ownerDocument.getElementById("dc_StationaryBridgeTeethNum" + valueNumber + valueId);
            currentValueArr.forEach((item) => {
                //修改数据值
                if (item.id === Number(valueId)) {
                    var strs = ["", "A", "B", "C", "D", "E"];
                    //修改值状态
                    switch (item.value) {
                        case "*":
                            item.value = "+";
                            currentDom.className = "dc_StationaryBridgeTeethButton dc_jia_teeth_button";
                            changeBorderDom.className = "dc_StationaryBridgeTeethNum dc_jia_teeth_StationaryBridgeTeethNum";
                            break;
                        case "+":
                            if (item.id >= 1 && item.id <= 5) {
                                item.value = strs[item.id];
                                currentDom.className = "dc_StationaryBridgeTeethButton dc_deciduous_teeth_button";
                            } else {
                                item.value = "-";
                                currentDom.className = "dc_StationaryBridgeTeethButton dc_jian_teeth_button";
                                changeBorderDom.className = "dc_StationaryBridgeTeethNum dc_jian_teeth_StationaryBridgeTeethNum";
                            }
                            break;
                        case "-":
                            item.value = "*";
                            currentDom.className = "dc_StationaryBridgeTeethButton";
                            changeBorderDom.className = "dc_StationaryBridgeTeethNum";
                            break;
                        default:
                            if (strs.indexOf(item.value) >= 0) {
                                item.value = "-";
                                currentDom.className = "dc_StationaryBridgeTeethButton dc_jian_teeth_button";
                                changeBorderDom.className = "dc_StationaryBridgeTeethNum dc_jian_teeth_StationaryBridgeTeethNum";
                            }
                            break;
                    }
                    currentDom.setAttribute("valueData", item.value);
                }

            });
            toothButtonObject[valueNumber] = JSON.parse(JSON.stringify(currentValueArr));
        }

        function getValuesStringForToothButtonObject() {
            let str = '';
            Object.keys(toothButtonObject).forEach((item, index) => {
                var newItem = item.charAt(0).toUpperCase() + item.slice(1);//将第一个字母转成大写，后台解析需要大写开头
                str += `${newItem}:`;
                let valueArr = toothButtonObject[item];

                //按牙齿顺序排列值
                if (item === 'value1' || item === 'value3') {
                    for (var i = 8; i >= 1; i--) {
                        var item_teeth = valueArr.find(item => item.id === i);
                        str += item_teeth.value;
                    }
                } else {
                    for (var i = 1; i <= 8; i++) {
                        var item_teeth = valueArr.find(item => item.id === i);
                        str += item_teeth.value;
                    }
                }

                //最后一个不拼接逗号
                if (item !== 'Value4') {
                    str += ';';
                }

            });
            return str;
        }

        function successFun() {
            options.Values = getValuesStringForToothButtonObject();
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            console.log(options.Values, '============options');
            console.log(options, '============options');
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },


    /**
       * 电活力测试牙位图
       * @param options 医学表达式属性
       * @param ctl 编辑器元素
       */
    ElectricPulpTestingTeethDialog: function (options, ctl, isInsertMode, ele) {
        if (!isInsertMode && (!options || typeof options != "object")) {
            return false;
        }
        let arr = this.stringToObject(options.Values);
        var ElectricPulpTestingTeethHtml = `
        <div class="dc_ElectricPulpTestingTeeth_Box">
            <div class="dc_ElectricPulpTestingTeeth_top">上颌</div>
            <div class="dc_ElectricPulpTestingTeeth_center">
               
                <div class="dc_ElectricPulpTestingTeeth_center_left">
                    <div class="dc_ElectricPulpTestingTeeth_center_yamian">牙面</div>
                    <div class="dc_ElectricPulpTestingTeeth_center_clean_box">
                        <div id="dc_input_clear_button" class="dc_input_clear_button">清</div>
                    </div>
                    <div class="dc_ElectricPulpTestingTeeth_center_yamian2">牙面</div>
                </div>
                <div id="dc_ElectricPulpTestingTeeth_center_right" class="dc_ElectricPulpTestingTeeth_center_right">
                    <div id="dc_ElectricPulpTestingTeeth_center_right_top" calss="dc_ElectricPulpTestingTeeth_center_right_top">
                        <div id="dc_ElectricPulpTestingTeeth_button_box1"></div>
                        <div id="dc_ElectricPulpTestingTeeth_button_box2"></div>
                    </div>
                    <div id="dc_ElectricPulpTestingTeeth_center_right_center" calss="dc_ElectricPulpTestingTeeth_center_right_center">
                    <div id="dc_ElectricPulpTestingTeeth_input_box1"></div>
                    <div id="dc_ElectricPulpTestingTeeth_input_box2"></div>
                    <div id="dc_ElectricPulpTestingTeeth_input_box3"></div>
                    <div id="dc_ElectricPulpTestingTeeth_input_box4"></div>
                    </div>
                    <div id="dc_ElectricPulpTestingTeeth_center_right_bottom" calss="dc_ElectricPulpTestingTeeth_center_right_bottom">
                        <div id="dc_ElectricPulpTestingTeeth_button_box3"></div>
                        <div id="dc_ElectricPulpTestingTeeth_button_box4"></div>
                    </div>
                </div>
            </div>
            <div class="dc_ElectricPulpTestingTeeth_bottom">下颌</div>
        </div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "电活力测试牙位图",
            bodyHeight: 310,
            dialogContainerBodyWidth: 520,
            bodyClass: "ElectricPulpTestingTeeth",
            bodyHtml: ElectricPulpTestingTeethHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //按钮数据
        for (var dotIndex = 1; dotIndex <= 4; dotIndex++) {
            //渲染牙位图
            var dcTeethTextHtml = ``;
            var dcTeethInputHtml = ``;
            var dc_EdcTeethTextHtmlBoxItem = ctl.ownerDocument.getElementById(`dc_ElectricPulpTestingTeeth_button_box${dotIndex}`);
            var dc_ElectricPulpTestingTeeth_InputBox = ctl.ownerDocument.getElementById(`dc_ElectricPulpTestingTeeth_input_box${dotIndex}`);
            if (dotIndex === 2 || dotIndex === 4) {
                //正序
                for (var i = 1; i <= 8; i++) {
                    TEETHBUTTONLIST.forEach(item => {
                        if (item.id === i) {
                            dcTeethTextHtml += `<div class="dc_teeth_text_item">${item.text}</div>`;
                            dcTeethInputHtml += `<input type="number" data-value-name="Value${dotIndex}${item.id}" class="dc_teeth_input_item"></input>`;

                        }
                    });
                }
            } else {
                //倒序
                for (var i = 8; i > 0; i--) {
                    TEETHBUTTONLIST.forEach(item => {
                        if (item.id === i) {
                            dcTeethTextHtml += `<div class="dc_teeth_text_item">${item.text}</div>`;
                            dcTeethInputHtml += `<input type="number" data-value-name="Value${dotIndex}${item.id}"  class="dc_teeth_input_item"></input>`;
                        }
                    });
                }
            }
            dc_EdcTeethTextHtmlBoxItem.innerHTML = dcTeethTextHtml;//正序
            dc_ElectricPulpTestingTeeth_InputBox.innerHTML = dcTeethInputHtml;
        }

        // 获取所有type为number的input元素
        const numberInputs = ctl.ownerDocument.querySelectorAll('.dc_teeth_input_item[type="number"]');
        // 遍历所有numberInputs，限制只能输入1-99之间的数值，并且不能输入小数
        numberInputs.forEach(input => {
            // 添加input事件监听器
            input.addEventListener('keydown', function (event) {
                var nextValue = `${this.value}${event.key}`;//用于判断是否会超出最大最小值
                // 限制只能输入1-99之间的数值，并且不能输入小数
                if ((event.key === '.') || (parseInt(nextValue) < 1) || (parseInt(nextValue) > 99)) {
                    if (event.preventDefault) {
                        event.preventDefault();
                    } else {
                        event.returnValue = false;
                    }
                }
            });
        });


        //牙位图数据值展示
        if (arr && Object.keys(arr).length) {
            Object.keys(arr).forEach(item => {
                var valueItem = jQuery(ctl).find(`input[data-value-name=${item}]`);
                if (valueItem) {
                    valueItem.val(arr[item] !== '' ? Number(arr[item]) : '');
                }
            });
        }
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }


        //清空按钮点击事件
        var clearButton = ctl.ownerDocument.getElementById("dc_input_clear_button");
        clearButton.addEventListener("click", function () {
            var allInputBox = ctl.ownerDocument.querySelectorAll(".dc_teeth_input_item");
            allInputBox.forEach(item => {
                item.value = "";
            });
        });

        function successFun() {
            var newDataObject = {};
            var allDataValueList = ctl.ownerDocument.querySelectorAll('input[data-value-name]');
            if (allDataValueList && allDataValueList.length) {
                allDataValueList.forEach(item => {
                    var valueName = item.getAttribute('data-value-name');
                    newDataObject[valueName] = item.value;
                });
            }
            var newValues = '';
            Object.keys(newDataObject).map(key => newValues += `${key}:${newDataObject[key]};`);
            options.Values = newValues;
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            console.log(options, '=======options');
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
     * 恒牙乳牙多生牙混合牙位图
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     */
    AdvancedTeechDialog: function (options, ctl, isInsertMode, ele) {
        if (!isInsertMode && (!options || typeof options != "object")) {
            return false;
        }
        console.log(options);
        var AdvancedTeechHtml = `
            <div id="dc_AdvancedTeech_Box" class="dc_AdvancedTeech_Box">
                <div class="dc_AdvancedTeech_top">
                    <div id="dc_AdvancedTeech_top_title_box" class="dc_AdvancedTeech_top_title_box"></div>
                    <div id="dc_AdvancedTeech_top_teeth_box" class="dc_AdvancedTeech_top_teeth_box">
                        <div class="dc_AdvancedTeech_top_title_Item">牙面</div>
                        <div id="dc_AdvancedTeech_teeth_content" class="dc_AdvancedTeech_teeth_content"></div>
                    </div>
                </div>
                <div class="dc_AdvancedTeech_cnter">
                    <div class="dc_AdvancedTeech_cnter_left">左</div>
                    <div class="dc_AdvancedTeech_cnter_center">
                        <div id="dc_AdvancedTeech_cnter_permanent_top" class="dc_AdvancedTeech_cnter_permanent_top"></div>
                        <div id="dc_AdvancedTeech_cnter_deciduous_top" class="dc_AdvancedTeech_cnter_deciduous_top"></div>
                        <div id="dc_AdvancedTeech_cnter_supernumerary_top" class="dc_AdvancedTeech_cnter_supernumerary_top"></div>
                        <div id="dc_AdvancedTeech_cnter_supernumerary_bottom" class="dc_AdvancedTeech_cnter_supernumerary_bottom"></div>
                        <div id="dc_AdvancedTeech_cnter_deciduous_bottom" class="dc_AdvancedTeech_cnter_deciduous_bottom"></div>
                        <div id="dc_AdvancedTeech_cnter_permanent_bottom" class="dc_AdvancedTeech_cnter_permanent_bottom"></div>
                    </div>
                    <div class="dc_AdvancedTeech_cnter_right">右</div>
                </div>
                <div class="dc_AdvancedTeech_bottom">
                    <div id="dc_AdvancedTeech_Bootom_teeth_box" class="dc_AdvancedTeech_Bootom_teeth_box">
                        <div class="dc_AdvancedTeech_top_title_Item">牙面</div>
                        <div id="dc_AdvancedTeech_teeth_content_bottom" class="dc_AdvancedTeech_teeth_content"></div>
                    </div>
                </div>
                <div class="dc_AdvancedTeech_AutoSize">
                    <label>
                        <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                        <span>自动调整大小</span>
                    </label>
                </div>
                <div class="dc_AdvancedTeech_title_top">上颌</div>
                <div class="dc_AdvancedTeech_title_bottom">下颌</div>
            </div>
        `;
        var dialogOptions = {
            title: "恒牙乳牙多生牙混合牙位图",
            bodyHeight: 660,
            dialogContainerBodyWidth: 520,
            bodyClass: "AdvancedTeech",
            bodyHtml: AdvancedTeechHtml,
        };

        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        //数据初始化
        var permanentTeeth = ["M", "O", "D", "B", "L", "P"];//恒牙数据
        var deciduousTeeth = ["Ⅰ", "Ⅱ", "Ⅲ", "Ⅳ", "Ⅴ"];//乳牙数据
        var deciduousTeethReplace = ["U", "V", "W", "X", "Y"];//后期考虑用户可以自定义，与deciduousTeeth对应
        var TeethDescription = ["第四磨牙", "第三磨牙", "第二磨牙", "第一磨牙", "第二前磨牙", "第一前磨牙", "尖牙", "侧切牙", "中切牙",];//牙位描述

        //渲染顶部牙位说明
        var dcAdvancedTeechTopTitleBox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_top_title_box");
        TeethDescription = TeethDescription.concat([...TeethDescription].reverse());
        dcAdvancedTeechTopTitleBox.innerHTML += TeethDescription.map(item => `<div class="dc_AdvancedTeech_top_title_Item">${item}</div>`).join('');


        //上颌牙位图
        var dcTopteethContent = ctl.ownerDocument.getElementById("dc_AdvancedTeech_teeth_content");
        //下颌牙位图
        var dcBottomteethContent = ctl.ownerDocument.getElementById("dc_AdvancedTeech_teeth_content_bottom");
        //上牙恒牙数字标识
        var dcPermanentTopNumber = ctl.ownerDocument.getElementById("dc_AdvancedTeech_cnter_permanent_top");
        //下牙恒牙数字标识
        var dcPermanentBootomNumber = ctl.ownerDocument.getElementById("dc_AdvancedTeech_cnter_permanent_bottom");
        //上乳牙罗马数字
        var dcDeciduousTopNumber = ctl.ownerDocument.getElementById("dc_AdvancedTeech_cnter_deciduous_top");
        //下乳牙罗马数字
        var dcDeciduousBottomNumber = ctl.ownerDocument.getElementById("dc_AdvancedTeech_cnter_deciduous_bottom");
        //上牙多生牙
        var dcSupernumeraryTop = ctl.ownerDocument.getElementById("dc_AdvancedTeech_cnter_supernumerary_top");
        //下牙多生牙
        var dcSupernumeraryBottom = ctl.ownerDocument.getElementById("dc_AdvancedTeech_cnter_supernumerary_bottom");

        for (var j = 1; j <= 4; j++) {
            //用于记录正序或倒序
            let startNum = j === 1 || j === 3 ? 9 : 1;

            //恒牙英文id标识
            var newPermanentTeeth = (j <= 2) ? JSON.parse(JSON.stringify(permanentTeeth)).reverse() : JSON.parse(JSON.stringify(permanentTeeth));
            //开始渲染可以点击的牙位
            for (var i = 1; i <= 9; i++) {
                //渲染牙面列表
                var dcTeethListNumberHtml = newPermanentTeeth.map(item => `<div dc-data-button-type="dentalSurface" id="${j}${startNum}${item}" class="dc_AdvancedTeech_teeth_content_Item">${item}</div>`).join('');
                var dcTeethList = `<div class='dc_AdvancedTeech_teeth_content_List'>${dcTeethListNumberHtml}</div>`;
                (j <= 2) ? (dcTopteethContent.innerHTML += dcTeethList) : (dcBottomteethContent.innerHTML += dcTeethList);//(j <= 2)表示上牙位

                //多生牙（没有29Z、49Z牙位）
                var dcSupernumerary = (['49', '29'].indexOf(`${j}${startNum}`) > -1) ? '' : `<div  dc-data-button-type="supernumeraryTeeth" id="${j}${startNum}Z" class="dc_AdvancedTeech_teeth_triangle_back"><div class="dc_AdvancedTeech_teeth_triangle"></div></div>`;
                (j <= 2) ? (dcSupernumeraryTop.innerHTML += dcSupernumerary) : (dcSupernumeraryBottom.innerHTML += dcSupernumerary);//(j <= 2)表示上牙位

                //对应的数字牙位
                var dcTeethListNumber = `<div dc-data-button-type="permanentTeeth" id="${j}${startNum}" class="dc_AdvancedTeech_teeth_content_Item">${startNum}</div>`;
                (j <= 2) ? (dcPermanentTopNumber.innerHTML += dcTeethListNumber) : (dcPermanentBootomNumber.innerHTML += dcTeethListNumber);//(j <= 2)表示上牙位

                //乳牙罗马数字（只有5个牙位）
                var dcTeethDeciduousListNumberHtml = (startNum <= 5) ? `<div dc-data-button-type="deciduousTeeth" dc-permanent-data-id="${j}${startNum}" id='${j}${deciduousTeethReplace[startNum - 1]}' class="dc_AdvancedTeech_teeth_content_Item">${deciduousTeeth[startNum - 1]}</div>` : '';
                (j <= 2) ? (dcDeciduousTopNumber.innerHTML += dcTeethDeciduousListNumberHtml) : (dcDeciduousBottomNumber.innerHTML += dcTeethDeciduousListNumberHtml);//(j <= 2)表示上牙位

                //用于记录正序或倒序
                startNum = (j === 1 || j === 3) ? --startNum : ++startNum;
            }
        }

        //对弹框绑定点击事件
        var dcAdvancedTeechBox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_Box");
        dcAdvancedTeechBox.addEventListener('click', handleAdvancedTeechClick);
        function handleAdvancedTeechClick(e) {
            //监听整个对话款的点击事件
            e.stopPropagation();
            var targetClass = (e.target && e.target.className) || '';
            var clickDom = null;

            if (targetClass.indexOf('dc_AdvancedTeech_teeth_content_Item') > -1) {
                //点击了牙位图
                clickDom = e.target;
            } else if (targetClass.indexOf('dc_AdvancedTeech_teeth_triangle_back') > -1) {
                //点击了多生牙
                clickDom = e.target;
            } else if (targetClass.indexOf('dc_AdvancedTeech_teeth_triangle') > -1) {
                //点击了多生牙的三角形
                clickDom = e.target.parentNode;
            }

            //修改点击元素的样式
            if (clickDom && clickDom.className) {
                var selectClass = "dc_current_select";//选中时的类名
                var clickDomId = parseInt(clickDom.id);
                var clickDomIdButtonType = clickDom.getAttribute("dc-data-button-type");
                //获取当前点击牙位是否被选中
                var isSelect = clickDom.className.indexOf(selectClass) > -1;
                //获取当前点击牙位对应的乳牙数字是否选中 
                var deciduousTeethTarget = ctl.ownerDocument.querySelector(`div[dc-data-button-type="deciduousTeeth"][dc-permanent-data-id="${clickDomId}"]`);
                var deciduousTeethTargetSelect = (deciduousTeethTarget && deciduousTeethTarget.className.indexOf(selectClass) > -1);
                //获取当前点击牙位对应的恒牙数字是否选中
                var permanentTeethTarget = ctl.ownerDocument.getElementById(`${clickDomId}`);
                var permanentTeethTargetSelect = (permanentTeethTarget && permanentTeethTarget.className.indexOf(selectClass) > -1);

                switch (clickDomIdButtonType) {
                    case "dentalSurface":
                        //点击选中牙面
                        if (isSelect === false) {
                            //乳牙存在的牙位
                            if (deciduousTeethTarget) {
                                //当二者都没选中时，则优先选中对应恒牙数字
                                if (!deciduousTeethTargetSelect && !permanentTeethTargetSelect) {
                                    permanentTeethTarget.classList.add(selectClass);
                                }
                            } else {
                                if (!permanentTeethTargetSelect) {
                                    permanentTeethTarget.classList.add(selectClass);
                                }
                            }
                        }
                        break;
                    case "permanentTeeth":
                        //点击取消恒牙的阿拉伯数字
                        if (isSelect) {
                            if (!deciduousTeethTargetSelect) {
                                // 先判断相同位置乳牙数字是否选中，如果没选中需要将关联数据清空
                                permanentTeeth.forEach(item => {
                                    var dentalSurfaceItem = ctl.ownerDocument.getElementById(`${clickDomId}${item}`);
                                    dentalSurfaceItem && dentalSurfaceItem.classList.remove(selectClass);
                                });
                                // 取消对应的多生牙
                                var supernumeraryTeethItem = ctl.ownerDocument.getElementById(`${clickDomId}Z`);
                                supernumeraryTeethItem && supernumeraryTeethItem.classList.remove(selectClass);
                            }
                        }
                        break;
                    case "deciduousTeeth":
                        //点击取消乳牙的罗马数字
                        if (isSelect) {
                            //获取对应的牙面值
                            var dentalSurfaceNumber = clickDom.getAttribute("dc-permanent-data-id");
                            //获取恒牙
                            var permanentTeethTarget = ctl.ownerDocument.getElementById(`${dentalSurfaceNumber}`);
                            var permanentTeethTargetSelect = (permanentTeethTarget && permanentTeethTarget.className.indexOf(selectClass) > -1);
                            if (!permanentTeethTargetSelect) {
                                //先判断相同位置恒牙数字是否选中，如果没选中需要将关联数据清空
                                permanentTeeth.forEach(item => {
                                    var dentalSurfaceItem = ctl.ownerDocument.getElementById(`${dentalSurfaceNumber}${item}`);
                                    dentalSurfaceItem && dentalSurfaceItem.classList.remove('dc_current_select');
                                });

                                // 取消对应的多生牙
                                var supernumeraryTeethItem = ctl.ownerDocument.getElementById(`${dentalSurfaceNumber}Z`);
                                supernumeraryTeethItem && supernumeraryTeethItem.classList.remove(selectClass);
                            }
                        }
                        break;
                    case "supernumeraryTeeth":
                        //点击多生牙
                        if (isSelect === false) {
                            //获取多生牙对应的下一个位置的牙位
                            var clickDomNextId = ((clickDomId <= 29 && clickDomId >= 21) || (clickDomId <= 49 && clickDomId >= 41)) ? clickDomId + 1 : clickDomId - 1;
                            if (clickDomNextId === 10 || clickDomNextId === 30) {
                                clickDomNextId = clickDomNextId + 11;
                            }

                            //获取多生牙对应的下一个乳牙数字是否选中
                            var deciduousTeethTargetNext = ctl.ownerDocument.querySelector(`div[dc-data-button-type="deciduousTeeth"][dc-permanent-data-id="${clickDomNextId}"]`);
                            var deciduousTeethTargetSelectNext = (deciduousTeethTargetNext && deciduousTeethTargetNext.className.indexOf(selectClass) > -1);

                            //获取多生牙对应的下一个恒牙数字是否选中
                            var permanentTeethTargetNext = ctl.ownerDocument.getElementById(`${clickDomNextId}`);
                            var permanentTeethTargetSelectNext = (permanentTeethTargetNext && permanentTeethTargetNext.className.indexOf(selectClass) > -1);

                            //乳牙存在的牙位
                            if (deciduousTeethTarget) {
                                //当二者都没选中时，则优先选中对应恒牙数字
                                if ((!deciduousTeethTargetSelect || !deciduousTeethTargetSelectNext) && (!permanentTeethTargetSelect || !permanentTeethTargetSelectNext)) {
                                    permanentTeethTarget.classList.add(selectClass);
                                    permanentTeethTargetNext.classList.add(selectClass);
                                }
                            } else {
                                if (!permanentTeethTargetSelect || !permanentTeethTargetSelectNext) {
                                    permanentTeethTarget.classList.add(selectClass);
                                    permanentTeethTargetNext.classList.add(selectClass);

                                }
                            }

                        }
                        break;
                    default:
                        break;
                }
                //对当前点击目标进行样式修改
                isSelect ? clickDom.classList.remove(selectClass) : clickDom.classList.add(selectClass);

            }
        }


        //将数据展示在页面
        let advancedTeechValueObject = this.stringToObject(options.Values);
        if (advancedTeechValueObject && advancedTeechValueObject.Value1) {
            let { Value1 } = advancedTeechValueObject;
            let teethArr = Value1.split(",") || [];
            teethArr.length && teethArr.forEach((item => {
                item = item.trim();
                var itemDom = ctl.ownerDocument.getElementById(item);
                if (itemDom) {
                    itemDom.classList.add('dc_current_select');
                } else {
                    console.log('未找到元素：', item);
                }
            }));
        }



        function successFun() {
            var allselectListDom = ctl.ownerDocument.querySelectorAll(".dc_current_select");

            var Values = "Value1:";
            allselectListDom.forEach((item) => {
                Values += item.id + ",";
            });

            if (advancedTeechValueObject && advancedTeechValueObject.ValueX) {
                Values += (";" + "ValueX:" + advancedTeechValueObject.ValueX + ";");
            }
            options.Values = Values;
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
     * 出血指数
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     */
    FourValues4Dialog: function (options, ctl, isInsertMode, ele) {
        if (!isInsertMode && (!options || typeof options != "object")) {
            return false;
        }

        var FourValues4Html = `
        <div class="dc_FourValues4dialog_box" >
            <div class="dc_FourValues4dialog_center1">
                <div class="dc_FourValues4dialog_Value1_box">
                    <input  type="text" data-id="Value1"></input>
                </div>
                <div class="dc_FourValues4dialog_Value_center"></div>
                <div class="dc_FourValues4dialog_Value3_box">
                    <input type="text" data-id="Value3"></input>
                </div>
            </div>
            <div  class="dc_FourValues4dialog_center2">
                <div  class="dc_FourValues4dialog_Value2_box">
                    <input   type="text" data-id="Value2"></input>
                </div>
                <div  class="dc_FourValues4dialog_Value_center">
                    <div></div>
                </div>
                <div  class="dc_FourValues4dialog_Value4_box">
                    <input   type="text" data-id="Value4"></input>
                </div>
            </div>
        </div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "出血指数设置",
            bodyHeight: 200,
            dialogContainerBodyWidth: 320,
            bodyClass: "dc_FourValues4",
            bodyHtml: FourValues4Html,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        let arr = this.stringToObject(options.Values);
        console.log(arr, '================arr');
        if (arr && Object.keys(arr) && Object.keys(arr).length) {
            Object.keys(arr).map(item => {
                var dom = ctl.ownerDocument.querySelector(`input[data-id="${item}"]`);
                dom && (dom.value = arr[item]);
            });
        }
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            var dc_FourValues4_AllValue = ctl.ownerDocument.querySelectorAll(`.dc_FourValues4 input[data-id]`);
            console.log(dc_FourValues4_AllValue);
            options.Values = '';
            dc_FourValues4_AllValue.forEach(item => {
                var key = item.getAttribute("data-id");
                var value = item.value;
                options.Values += `${key}:${value};`;
            });
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            console.log(options, '==============出血指数');
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },

    /**
    * 探诊深度
    * @param options 医学表达式属性
    * @param ctl 编辑器元素
    */
    PDTeechDialog: function (options, ctl, isInsertMode, ele) {
        if (!isInsertMode && (!options || typeof options != "object")) {
            return false;
        }
        var PDTeechHtml = `<div class="dc_PDTeech_content">
            <div class="dc_PDTeech_left">
                <input type="text" data-id="Value7" class="dc_PDTeech_left_input"></input>
            </div>
            <div class="dc_PDTeech_center">
                <div class="dc_PDTeech_center_top">
                    <div class="dc_PDTeech_center_inner">
                        <input type="text" data-id="Value1" class="dc_PDTeech_left_input"></input>
                    </div>
                    <div class="dc_PDTeech_center_inner dc_center_input">
                        <input type="text" data-id="Value2" class="dc_PDTeech_left_input"></input>
                    </div>
                    <div class="dc_PDTeech_center_inner">
                        <input type="text" data-id="Value3" class="dc_PDTeech_left_input"></input>
                    </div>
                </div>
                <div class="dc_PDTeech_center_bottom">
                    <div class="dc_PDTeech_center_inner">
                        <input type="text" data-id="Value4" class="dc_PDTeech_left_input"></input>
                    </div>
                    <div class="dc_PDTeech_center_inner dc_center_input">
                        <input type="text" data-id="Value5" class="dc_PDTeech_left_input"></input>
                    </div>
                    <div class="dc_PDTeech_center_inner">
                        <input type="text" data-id="Value6" class="dc_PDTeech_left_input"></input>
                    </div>
                </div>
            </div>
            <div class="dc_PDTeech_right">
                <input type="text" data-id="Value9" class="dc_PDTeech_left_input"></input>
                <input type="text" data-id="Value8" class="dc_PDTeech_left_input"></input>
                <input type="text" data-id="Value10" class="dc_PDTeech_left_input"></input>
            </div>
        </div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "探诊深度设置",
            bodyHeight: 200,
            dialogContainerBodyWidth: 450,
            bodyClass: "dc_PDTeech",
            bodyHtml: PDTeechHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        let arr = this.stringToObject(options.Values);
        var allInput = ctl.ownerDocument.querySelectorAll(`.dc_PDTeech_left_input`);
        console.log(arr, '================arr');
        if (arr && Object.keys(arr).length) {
            allInput.length && allInput.forEach(item => {
                let key = item.getAttribute("data-id");
                item.value = arr[key];
            });
        }
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            options.Values = "";
            allInput.forEach(item => {
                var key = item.getAttribute("data-id");
                var value = item.value;
                options.Values += `${key}:${value};`;
            });
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            console.log(options);
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
        * 婚育史
        * @param options 医学表达式属性
        * @param ctl 编辑器元素
        */
    FourValues5Dialog: function (options, ctl, isInsertMode, ele) {
        if (!isInsertMode && (!options || typeof options != "object")) {
            return false;
        }
        var FourValues5Html = `<div class="dc_FourValues5_content">
            <div>
                <div>足月数</div>
                <div><input type="number" min="0" value="0"  data-id="Value1" class="dc_FourValues5_input"></input></div>
            </div>
            <div class="dc_FourValues5_line"></div>
            <div>
                <div>早产数</div>
                <div><input type="number"  min="0"  value="0" data-id="Value2" class="dc_FourValues5_input"></input></div>
            </div>
            <div class="dc_FourValues5_line"></div>
            <div>
                <div>流产数</div>
                <div><input type="number" min="0"  value="0" data-id="Value3" class="dc_FourValues5_input"></input></div>
            </div>
            <div class="dc_FourValues5_line"></div>
            <div>
                <div>现有数</div>
                <div><input type="number" min="0"  value="0" data-id="Value4" class="dc_FourValues5_input"></input></div>
            </div>
        </div>
        <div class="dc_AdvancedTeech_AutoSize">
            <label>
                <input type="checkbox" id="dc_AdvancedTeech_AutoSize_checkbox" class="dc_AdvancedTeech_AutoSize_checkbox">
                    <span>自动调整大小</span>
            </label>
        </div>
        `;
        var dialogOptions = {
            title: "婚育史",
            bodyHeight: 100,
            dialogContainerBodyWidth: 440,
            bodyClass: "dc_FourValues5",
            bodyHtml: FourValues5Html,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        let arr = this.stringToObject(options.Values);
        var allInput = ctl.ownerDocument.querySelectorAll(`.dc_FourValues5_input`);

        if (arr && Object.keys(arr).length) {
            allInput.length && allInput.forEach(item => {
                let key = item.getAttribute("data-id");
                item.value = parseFloat(arr[key]);
            });
        }
        //自动调整大小
        var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
        const hasAutoSize = Object.keys(options).some(key => key.toLowerCase() === 'autosize');
        // 不存在默认值给true
        if (hasAutoSize) {
            autoSizeCheckbox.checked = options.AutoSize || false;
        } else {
            autoSizeCheckbox.checked = true;
        }

        function successFun() {
            options.Values = "";
            allInput.forEach(item => {
                var key = item.getAttribute("data-id");
                var value = item.value;
                options.Values += `${key}:${value};`;
            });
            //自动调整大小
            var autoSizeCheckbox = ctl.ownerDocument.getElementById("dc_AdvancedTeech_AutoSize_checkbox");
            options['AutoSize'] = autoSizeCheckbox.checked;

            console.log(options);
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertmedicalexpression", false, options);
            } else {
                ctl.SetElementProperties(ele, options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },


    /**
    * 打印背景色选择
    * @param options 颜色值
    * @param ctl 
    */
    PrintBackColorDialog: function (options, ctl) {
        if (!ctl) {
            return false;
        }
        var PrintBackColorHtml = `
        <div class="dc_PrintBackColor_content">
           请选择颜色： <input type="color" id='dc_Print_Back_color' />
        </div>
        `;
        var dialogOptions = {
            title: "打印背景色选择",
            bodyHeight: 80,
            dialogContainerBodyWidth: 240,
            bodyClass: "dc_PrintBackColor",
            bodyHtml: PrintBackColorHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        var dcPrintBackColor = ctl.ownerDocument.getElementById("dc_Print_Back_color");
        if (dcPrintBackColor) {
            if (options && typeof options === "string") {
                dcPrintBackColor.value = options;
            }
        }

        function successFun() {
            ctl.DCExecuteCommand("PrintBackColor", false, dcPrintBackColor.value);
        }
    },
    /**
     * 打印字体色
     * @param options 颜色值
     * @param ctl 
     */
    PrintColorDialog: function (options, ctl) {
        if (!ctl) {
            return false;
        }
        var PrintColorHtml = `
        <div class="dc_PrintColor_content">
           请选择颜色： <input type="color" id='dc_Print_Back_color' />
        </div>
        `;
        var dialogOptions = {
            title: "选择打印字体色",
            bodyHeight: 80,
            dialogContainerBodyWidth: 240,
            bodyClass: "dc_PrintColor",
            bodyHtml: PrintColorHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        var dcPrintColor = ctl.ownerDocument.getElementById("dc_Print_Back_color");
        if (dcPrintColor) {
            if (options && typeof options === "string") {
                dcPrintColor.value = options;
            }
        }

        function successFun() {
            ctl.DCExecuteCommand("PrintColor", false, dcPrintColor.value);
            ctl.DocumentOptions.ViewOptions.BothBlackWhenPrint = false;
            ctl.ApplyDocumentOptions();
        }
    },
    /**
     * 插入ControlHost
     * @param options 
     * @param ctl 
     */
    InsertControlHostDialog: function (options, ctl, isInsertMode) {
        var ele = null;
        if (!options || typeof options != "object") {
            // 当未传入值时
            if (isInsertMode == true) {
                options = {};
            } else {
                ele = ctl.CurrentElement("XTextControlHostElement");
                if (ele == null) {
                    return false;
                }
                options = ctl.GetElementProperties(ele);
                if (options == null) {
                    return false;
                }
            }
        }
        //console.log(options, '===================');
        if (!options) {
            return false;
        }

        var ControlHostHtml = `
        <div class="dc_ControlHost_content">
            <div class="dc_controlHost_item">
                <div class="dc_controlHost_item_title">ID：</div>
                <input data-text="id" class="dc_dc_controlHost_item_value" type="text"/>
            </div>
            <div class="dc_controlHost_item">
                <div class="dc_controlHost_item_title">Name：</div>
                <input data-text="name" class="dc_dc_controlHost_item_value" type="text"/>
            </div>
            <div class="dc_controlHost_item">
                <div class="dc_controlHost_item_title">宽度：</div>
                <input data-text="width" class="dc_dc_controlHost_item_value" type="text"/>
            </div>
            <div class="dc_controlHost_item">
                <div class="dc_controlHost_item_title">高度：</div>
                <input data-text="height" class="dc_dc_controlHost_item_value" type="text"/>
            </div>
            <div class="dc_controlHost_item">
                <div class="dc_controlHost_item_title">完整名称：</div>
                <input data-text="typefullname" class="dc_dc_controlHost_item_value" type="text"/>
            </div>
            <div class="dc_controlHost_item">
                <div class="dc_controlHost_item_title">打印可见性：</div>
                <select data-text="printvisibility" class="dc_dc_controlHost_item_value">
                    <option value="visible">Visible</option>
                    <option value="hidden">Hidden</option>
                    <option value="none">None</option>
                </select>
            </div>
            <div class="dc_controlHost_item">
                <div class="dc_controlHost_item_title">允许调整大小：</div>
                <select data-text="allowuserresize" class="dc_dc_controlHost_item_value" type="text">
                    <option value="fixsize">FixSize</option>
                    <option value="width">Width</option>
                    <option value="height">Height</option>
                    <option value="widthandheight">WidthAndHeight</option>
                </select>
            </div>
        </div>
        `;
        var dialogOptions = {
            title: "承载控件属性设置",
            bodyHeight: 250,
            dialogContainerBodyWidth: 320,
            bodyClass: "dc_ControlHost",
            bodyHtml: ControlHostHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        if (options && Object.keys(options).length) {
            Object.keys(options).map(item => {
                var dom = ctl.ownerDocument.querySelector(`[data-text="${item.toLowerCase()}"]`);
                if (dom) {
                    if (dom.nodeName && dom.nodeName === "SELECT") {
                        dom.value = options[item] && options[item].toLowerCase && options[item].toLowerCase();
                    } else {
                        dom.value = options[item];
                    }
                }
            });
        }
        function successFun() {
            let allDataDom = ctl.ownerDocument.querySelectorAll('.dc_ControlHost_content [data-text]');
            let data = {};
            if (allDataDom) {
                allDataDom.forEach(item => {
                    let key = item.getAttribute("data-text");
                    let value = item.value;
                    data[key] = value;
                });
            }
            if (isInsertMode == true) {
                ctl.DCExecuteCommand("insertcontrolhost", false, {
                    ...options,
                    ...data
                });
            } else {
                ctl.SetElementProperties(ele, {
                    ...options,
                    ...data
                });
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetElementProperties(ctl.CurrentElement("XTextControlHostElement"));
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            }
        }
    },
    /**
     * 医学表达式通用值转换方法（字符串转换为对象）
     * @param values 医学表达式values默认值的字符串
     * @return arr 处理完成的对象
     */
    stringToObject: function (values) {
        let arr = {};
        if (values) {
            let newValues = values.split(";");
            newValues.filter((item) => {
                if (item) {
                    let keyName = item.slice(0, item.indexOf(":"));
                    let keyvalue = item.slice(item.indexOf(":") + 1, item.length);
                    arr[keyName] = keyvalue;
                }
            });
        }
        return arr;
    },

    /**
     * 医学表达式通用值转换方法（字符串转换为对象）
     * @param _data 获取的输入库数据
     * @return str 处理完成的字符串
     */
    ObjectToString: function (_data) {
        let str = "";
        for (var i in _data) {
            str += `${i}:${_data[i]};`;
        }
        return str;
    },

    /**
     * 牙位生成函数
     * @param idPrefix id绑定的前缀
     * @param parentId 父元素id
     * @param teethKey 牙位标识
     * @param isTop 是否为上颌（用于区分上下颌）
     * @return
     */
    teethPosition: function (idPrefix, parentId, teethKey, isTop = true, ctl) {
        let newPTeethList = "";
        let namePArr = isTop
            ? ["a1", "a2", "a3", "a4", "a5", "a6", "a7", "a8", "a9", "a10"]
            : ["a11", "a12", "a13", "a14", "a15", "a16", "a17", "a18", "a19", "a20"];
        let idNum = isTop ? 1 : 11;
        for (var i = 0; i < namePArr.length; i++) {
            newPTeethList += `<td><input class="inp" dccheck="false" readonly="readonly" unselectable="on" name="${namePArr[i]
                }" type="text" id="${idPrefix + (i + idNum) + ""
                }" value="${teethKey}" style="width:20px; height:20px;text-align:center;border: 1px solid rgb(169, 169, 169);
        background-color: rgb(255, 255, 255); ${[1, 3, 6, 8].includes(i)
                    ? "border:1px solid #a9a9a9;background-color: #d7e4f2;"
                    : ""
                }" /></td>`;
        }
        jQuery(ctl).find(parentId).append($(newPTeethList));
    },

    /**
     * 恒牙牙位生成函数
     * @param idPrefix id绑定的前缀
     * @param parentId 父元素id
     * @param teethKey 牙位标识
     * @param isTop 是否为上颌（用于区分上下颌）
     * @return
     */
    PermanentToothPosition: function (
        idPrefix,
        parentId,
        teethKey,
        isTop = true,
        ctl
    ) {
        let num = isTop ? 1 : 17;
        let newRomanTeethList = "";
        for (var i = 0; i < 16; i++) {
            newRomanTeethList += `<td><input class="inp" dccheck="false" readonly="readonly" unselectable="on" name="a${i + num + ""
                }" type="text" id="${idPrefix + (i + num) + ""
                }" value="${teethKey}" style="width:20px; height:20px;text-align:center;${[1, 3, 5, 7, 8, 10, 12, 14].includes(i)
                    ? 'border:1px solid #a9a9a9;background-color: #d7e4f2;"'
                    : ""
                }" /></td>`;
        }
        jQuery(ctl).find(parentId).append($(newRomanTeethList));
    },

    /**
     * 恒牙牙位图，value值dom生成函数
     */
    PermanentToothValueNumber: function (parentId, valueNum, ctl) {
        let newNumberValueHtml = "";
        for (var i = 8; i > 0; i--) {
            newNumberValueHtml += ` <td><input class="inp" dccheck="false" readonly="readonly" unselectable="on"
                            type="text" id="Value${valueNum + (9 - i)
                }" value="${i}"
                            style="${i % 2 != 0
                    ? "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #d7e4f2;"
                    : "width:20px; height:20px;text-align:center;"
                }"
                            /></td>`;
        }
        for (var i = 1; i <= 8; i++) {
            newNumberValueHtml += `<td><input class="inp" dccheck="false" readonly="readonly" unselectable="on"
                type="text" id="Value${valueNum + (i + 8)}" value="${i}"
                style="${i % 2 != 0
                    ? "width:20px; height:20px;text-align:center;border:1px solid #a9a9a9;background-color: #d7e4f2;"
                    : "width:20px; height:20px;text-align:center;"
                }"
                /></td>`;
        }
        jQuery(ctl).find(parentId).append(jQuery(newNumberValueHtml));
        // console.log(jQuery('#dc_valueNumberBox1'))
    },

    /**
     * 罗马数字牙位生成函数
     * @param  id绑定的前缀
     * @param valueNum value值的数字后缀
     * @param parentId 父元素id
     * @return
     */
    romanteethPosition: function (valueNum, parentId, ctl) {
        let newRomanTeethList = "";
        let romanArr = ["Ⅴ", "Ⅳ", "Ⅲ", "Ⅱ", "Ⅰ", "Ⅰ", "Ⅱ", "Ⅲ", "Ⅳ", "Ⅴ"];
        for (var i = 0; i < romanArr.length; i++) {
            newRomanTeethList += `<td><input class="inp" dccheck="false" readonly="readonly" unselectable="on" type="text" id="Value${i + valueNum + ""
                }" data-text="Value${i + valueNum + ""}" value="${romanArr[i]
                }" style="width:20px; height:20px;text-align:center;border: 1px solid rgb(169, 169, 169);
        background-color: rgb(255, 255, 255);${[1, 3, 6, 8].includes(i)
                    ? "border:1px solid #a9a9a9;background-color: #d7e4f2;"
                    : ""
                }"/></td>`;
        }
        jQuery(ctl).find(parentId).append($(newRomanTeethList));
    },

    /**
     * 全部的医学表达式对话框
     * @param options 医学表达式属性
     * @param ctl 编辑器元素
     * @param isInsertMode 是否是插入模式
     */
    MedicalExpressionDialog: function (options, ctl, isInsertMode, ele) {
        if (!options) {
            return false;
        }

        //DUWRITER5_0-2450 20240507 lxy在只读病程中的医学表达式，不允许弹出对话框修改（如果是新插入的医学表达式，GetCanModify无法获取所以不做判断）
        if (Boolean(isInsertMode) === false && ele) {
            var canModify = ctl.GetCanModify(ele);
            if (Boolean(canModify) === false) {
                return false;
            }
        }
        if (options.ExpressionStyle === '' || !options.ExpressionStyle) {
            WriterControl_Dialog.MedicalExpressionChooseIMGDialog(options, ctl);
            return true;
        }

        options.ExpressionStyle += "";
        // "FourValuesGeneral" 通用公式
        switch (options.ExpressionStyle) {
            // 月经史公式
            case "FourValues":
                WriterControl_Dialog.FourValuesDialog(options, ctl, isInsertMode, ele);
                break;
            // 月经史公式1
            case "FourValues1":
                //如果自定义属性中带有参数ISPERIMETRIC,则表示为龋齿公式
                if (options && options.Attributes && options.Attributes['ISPERIMETRIC'] && options.Attributes['ISPERIMETRIC'] === 'true') {
                    WriterControl_Dialog.DentalCariesDialog(options, ctl, isInsertMode, ele);
                } else {
                    //月经史1
                    WriterControl_Dialog.FourValues1Dialog(options, ctl, isInsertMode, ele);
                }
                break;
            // 月经史公式2
            case "FourValues2":
                //如果自定义属性中带有参数ISPERIMETRIC,则表示为视野图公式
                if (options && options.Attributes && options.Attributes['ISPERIMETRIC'] && options.Attributes['ISPERIMETRIC'] === 'true') {
                    WriterControl_Dialog.PerimetricDialog(options, ctl, isInsertMode, ele);
                } else {
                    //月经史2
                    WriterControl_Dialog.FourValues2Dialog(options, ctl, isInsertMode, ele);
                }
                break;
            // 月经史公式4
            case "ThreeValues":
                WriterControl_Dialog.ThreeValuesDialog(options, ctl, isInsertMode, ele);
                break;
            // 瞳孔
            case "Pupil":
                WriterControl_Dialog.PupilDialog(options, ctl, isInsertMode, ele);
                break;
            // 胎心值
            case "FetalHeart":
                WriterControl_Dialog.FetalHeartDialog(options, ctl, isInsertMode, ele);
                break;
            // 标尺
            case "PainIndex":
                WriterControl_Dialog.PainIndexDialog(options, ctl, isInsertMode, ele);
                break;
            // 眼球突出度
            case "ThreeValues2":
                WriterControl_Dialog.EyeballProtrusionDialog(options, ctl, isInsertMode, ele);
                break;
            // 斜视符号
            case "StrabismusSymbol":
                WriterControl_Dialog.SquintSymbolDialog(options, ctl, isInsertMode, ele);
                break;
            // 恒牙牙位图
            case "PermanentTeethBitmap":
                WriterControl_Dialog.PermanentTeethBitmapDialog(options, ctl, isInsertMode, ele);
                break;
            // 乳牙牙位图
            case "DeciduousTeech":
                WriterControl_Dialog.DeciduousTeechDialog(options, ctl, isInsertMode, ele);
                break;
            // 分数公式
            case "Fraction":
                WriterControl_Dialog.FractionDialog(options, ctl, isInsertMode, ele);
                break;
            // 光定位
            case "LightPositioning":
                WriterControl_Dialog.LightPositioningDialog(options, ctl, isInsertMode, ele);
                break;
            // 病变上牙
            case "DiseasedTeethTop":
                WriterControl_Dialog.DiseasedTeethTopDialog(options, ctl, isInsertMode, ele);
                break;
            // 病变下牙
            case "DiseasedTeethBotton":
                WriterControl_Dialog.DiseasedTeethBottonDialog(options, ctl, isInsertMode, ele);
                break;
            //固定桥牙位图
            case "StationaryBridgeTeeth":
                WriterControl_Dialog.StationaryBridgeTeethDialog(options, ctl, isInsertMode, ele);
                break;
            //电活力测试牙位图
            case "ElectricPulpTestingTeeth":
                WriterControl_Dialog.ElectricPulpTestingTeethDialog(options, ctl, isInsertMode, ele);
                break;
            //恒牙乳牙多生牙混合牙位图
            case "AdvancedTeech":
                WriterControl_Dialog.AdvancedTeechDialog(options, ctl, isInsertMode, ele);
                break;
            //出血指数
            case "FourValues4":
                WriterControl_Dialog.FourValues4Dialog(options, ctl, isInsertMode, ele);
                break;
            //探诊深度
            case "PDTeech":
                WriterControl_Dialog.PDTeechDialog(options, ctl, isInsertMode, ele);
                break;
            //婚育史
            case "FourValues5":
                WriterControl_Dialog.FourValues5Dialog(options, ctl, isInsertMode, ele);
                break;
            default:
                if (isInsertMode == true) {
                    // 是插入模式，执行插入医学表达式
                    return ctl.__DCWriterReference.invokeMethod("DCExecuteCommand", "insertmedicalexpression", false, options);
                }
                break;
        }
        return true;
    },
    // ======医学表达式-end======

    /**
     * 创建表格/单元格边框设置对话框
     * @param options 单元格属性
     * @param ctl 编辑器元素
     */
    borderShadingcellDialog: function (options, ctl, showTableOption = false, tableOptions = null) {
        console.log(showTableOption);
        let { title, type } = options;
        let ele = null;
        if (type && type === "tableCell") {
            ele = ctl.CurrentTableCell();
        } else if (type && type === "table") {
            ele = ctl.CurrentTable();
        }
        if (!ele) {
            return;
        }
        options = ctl.GetElementProperties(ele);
        let SetTabletyle = options.Style ? options.Style : {};
        var bordersShadingHtml = `
        <div class="dc_bordersShading_dialog">
            <div class="dc_bordersShading_border_box dc_Box">
                <h6 class="dc_title">边框</h6>
                <div class="dc_bordershading_border_box_content">
                    <label class="dc_border_BorderTop_label">
                        <input type="checkbox" name="BorderTop" data-text="BorderTop" checked="checked">
                        <span class="dcTitle-text">上</span>
                    </label>
                    <label class="dc_border_BorderBottom_label">
                        <input type="checkbox" name="BorderBottom" data-text="BorderBottom" checked="checked">
                        <span class="dcTitle-text">下</span>
                    </label>
                    <label class="dc_border_BorderLeft_label">
                        <input type="checkbox" name="BorderLeft" data-text="BorderLeft" checked="checked">
                        <span class="dcTitle-text">左</span>
                        </label>
                    <label class="dc_border_BorderRight_label">
                        <input type="checkbox" name="BorderRight" data-text="BorderRight" checked="checked">
                        <span class="dcTitle-text">右</span>
                    </label>
                </div>
            </div>

            <div class="dc_Box dc_bordersShading_color_box"  >
                <h6 class="dc_title">颜色</h6>
                <div id="dc_bordershading_color_box_content" class="dc_bordershading_color_box_content" >
                    <div>
                         <label class="dc_border_BorderTopColor_label" >
                            <div class="dc_border_show_box" data-value="" id="dc_border_BorderTopColor_box"></div>
                            <input type="color" data-text="BorderTopColorString" name="BorderTopColor" dc-target-id="dc_border_BorderTopColor_box"  >
                        </label>
                        <span class="dcTitle-text">上</span>
                    </div>
                    <div>
                         <label class="dc_border_BorderBottomColor_label" >
                            <div data-value="" class="dc_border_show_box" id="dc_border_BorderBottomColorString_box"></div>
                            <input type="color" data-text="BorderBottomColorString" dc-target-id="dc_border_BorderBottomColorString_box"  >
                        </label>
                        <span class="dcTitle-text">下</span>
                    </div>
                    <div>
                         <label class="dc_border_BorderLeftColor_label" >
                            <div data-value=""  class="dc_border_show_box" id="dc_border_BorderLeftColorString_box"></div>
                            <input type="color" data-text="BorderLeftColorString" dc-target-id="dc_border_BorderLeftColorString_box"  >
                        </label>
                        <span class="dcTitle-text">左</span>
                    </div>
                    <div>
                         <label class="dc_border_BorderRightColor_label" >
                            <div data-value=""  class="dc_border_show_box" id="dc_border_BorderRightColorString_box"></div>
                            <input type="color" data-text="BorderRightColorString" dc-target-id="dc_border_BorderRightColorString_box"  >
                        </label>
                        <span class="dcTitle-text">右</span>
                    </div>
                </div>
            </div>
                <label class="dc_BorderStyle_label" >
                    <span class="dc_txt">网格线样式：</span>
                    <select name="BorderStyle" id="dc_BorderStyle" data-text="BorderStyle"></select>
                </label>

                <label class="dc_BorderWidth_label" >
                    <span class="dcTitle-text">宽度
                        <span class="dc_SpecifyWidth_title" title="单位：三百分之一英寸">?</span>：
                    </span>
                    <input type="number" data-text="BorderWidth" name="BorderWidth">
                </label>

                <label class="dc_Apply_label" >
                    <span class="dcTitle-text">应用于：</span>
                    <select  id="dc_borderApply" >
                        <option value="table">表格</option>
                        <option value="cell">单元格</option>
                    </select>
                </label>
        </div>
        `;
        var dialogOptions = {
            title,
            bodyHeight: 280,
            bodyClass: "bordersShading",
            bodyHtml: bordersShadingHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions, function () {
            //次函数用于点击取消按钮后判断是否展示表格弹框
            if (showTableOption) {
                ctl.tableDialog(tableOptions, ctl);
            }
        });
        // //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");

        var LineStyle_node = dcPanelBody.find("#dc_BorderStyle");
        for (var i = 0; i < DASHSTYLE.length; i++) {
            var _DashStyle = DASHSTYLE[i];
            LineStyle_node.append(
                "<option value='" +
                _DashStyle.name +
                "'>" +
                _DashStyle.show +
                _DashStyle.name +
                "</option>"
            );
        }
        GetOrChangeData(dcPanelBody, SetTabletyle);

        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        //获取对话框元素，复显值
        var colorContainer = dcPanelBody.find("#dc_bordershading_color_box_content");
        if (colorContainer) {
            var allborderInput = colorContainer.find("input[type='color']");
            if (allborderInput && allborderInput.length > 0) {
                for (var i = 0; i < allborderInput.length; i++) {
                    var itemInput = allborderInput[i];
                    var borderColorType = itemInput.getAttribute("data-text");
                    var targetDivId = itemInput.getAttribute("dc-target-id");
                    var targetDiv = dcPanelBody.find("#" + targetDivId)[0];
                    if (targetDiv) {
                        targetDiv.setAttribute("data-value", SetTabletyle[borderColorType] || '');
                        targetDiv.style.backgroundColor = SetTabletyle[borderColorType] || '';
                    }
                }
            }
            //监听input颜色值改变
            allborderInput.change(function () {
                var targetDivId = this.getAttribute("dc-target-id");
                var targetDiv = dcPanelBody.find("#" + targetDivId)[0];
                if (targetDiv) {
                    targetDiv.setAttribute("data-value", this.value);
                    targetDiv.style.backgroundColor = this.value;
                }
            });
        }

        if (type == "tableCell") {
            ctl.ownerDocument.getElementById("dc_borderApply").value = "cell";
        } else if (type == "table") {
            ctl.ownerDocument.getElementById("dc_borderApply").value = "table";
        }
        function successFun() {
            var styleOpt = {
                Style: GetOrChangeData(dcPanelBody),
            };
            var colorContainer = dcPanelBody.find("#dc_bordershading_color_box_content");
            if (colorContainer) {
                var allborderInput = colorContainer.find("input[type='color']");
                if (allborderInput && allborderInput.length > 0) {
                    for (var i = 0; i < allborderInput.length; i++) {
                        var itemInput = allborderInput[i];
                        var borderColorType = itemInput.getAttribute("data-text");
                        var targetDivId = itemInput.getAttribute("dc-target-id");
                        var targetDiv = dcPanelBody.find("#" + targetDivId)[0];
                        if (targetDiv) {
                            styleOpt.Style[borderColorType] = targetDiv.getAttribute("data-value") || '';
                        }
                    }
                }
            }


            let applyType = ctl.ownerDocument.getElementById("dc_borderApply").value;
            if (applyType && applyType === "cell") {
                ctl.SetSelectTableCellBorder(styleOpt); //支持设置选择的多个单元格，或光标定位在单元格内
            } else if (applyType && applyType === "table") {
                ele = ctl.CurrentTable();
                ctl.SetTableBorder(ele, styleOpt["Style"]);
            }
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                ctl.EventDialogChangeProperties(changedOptions);
            };
            console.log(showTableOption);
            if (showTableOption) {
                ctl.tableDialog(tableOptions, ctl);
            }
        }
    },

    /**
     * 插入表格
     * @param options 表格属性
     * @param ctl 编辑器元素
     */
    insertTableDialog: function (options, ctl, isInsertMode, callBack) {
        var insertTableHtml = `
        <div>
            <div class="dc_insertTable_content" >
                <label class="dc_inserttable_TableID_label dc_flex" >
                    <h6>编号：</h6>
                    <input type="text"  data-text="TableID">
                </label>
                 <label class="dc_inserttable_RowCount_label dc_flex">
                    <h6>行数：</h6>
                    <input type="number" value="2" id="dc_RowCount" class="dc_tableRowAndColumns" data-text="RowCount">
                </label>
                <label  class="dc_inserttable_ColumnCount_label dc_flex">
                    <h6>列数：</h6>
                    <input type="number" value="3" id="dc_ColumnCount" class="dc_tableRowAndColumns" data-text="ColumnCount">
                </label>
            </div>
            <div class="dc_Box dc_inserttable_Box_footer">
                <h6 class="dc_title">预览</h6>
                <table id="dc_tableBox"></table>
            </div>
        </div>
        `;
        var dialogOptions = {
            title: "插入表格",
            bodyHeight: "auto",
            bodyClass: "insertTable",
            bodyHtml: insertTableHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions, callBack);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        createTable();
        function successFun() {
            let params = GetOrChangeData(dcPanelBody);
            if (!params.RowCount || !params.ColumnCount) {
                return alert(window.__DCSR.TableRowClolumnMastBePositiveInteger);
            }
            console.log(params, '========params');
            ctl.DCExecuteCommand("InsertTable", false, params);
        }
        dcPanelBody.find(".dc_tableRowAndColumns").change(function () {
            let rowNum = dcPanelBody.find("#dc_RowCount").val();
            let columnNum = dcPanelBody.find("#dc_ColumnCount").val();
            createTable(rowNum, columnNum);
        });
        function createTable(rowNum = 2, columnNum = 3) {
            var box = ctl.ownerDocument.getElementById("dc_tableBox");
            box.innerHTML && (box.innerHTML = "");
            for (var i = 0; i < rowNum; i++) {
                var tr = ctl.ownerDocument.createElement("tr");
                for (var j = 0; j < columnNum; j++) {
                    var td = ctl.ownerDocument.createElement("td");
                    tr.appendChild(td);
                }
                box.appendChild(tr);
            }
        }
    },

    /**
     * 拆分单元格
     * @param options 单元格属性
     * @param ctl 编辑器元素
     */
    splitCellDialog: function (options, ctl) {
        var splitCellHtml = `
        <div class="dc_splitCell_dialog">
            <label class="dc_flex" >
                <h6 >行数(R)：</h6>
                <input type="number" value="1" id="dc_RowNum" class="dc_tableRowAndColumns" data-text="Value2">
            </label>
            <label class="dc_flex" >
                <h6 >列数(C)：</h6>
                <input type="number" value="1" id="dc_ColumnsNum" class="dc_tableRowAndColumns" data-text="Value1">
            </label>
            <div id="dc_detachable_rows"></div>
        </div>
        `;
        var dialogOptions = {
            title: "拆分单元格",
            dialogContainerBodyWidth: 250,
            bodyHeight: 140,
            bodyClass: "splitCell",
            bodyHtml: splitCellHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        if (options) {
            let rowstr = options.split(",")[0];
            let columstr = options.split(",")[1];
            dcPanelBody.find("#dc_RowNum").val(rowstr);
            dcPanelBody.find("#dc_ColumnsNum").val(columstr);
        }

        var selectTableCell = ctl.GetSelectTableCells() || [];
        var selectRow = [];
        if (selectTableCell && selectTableCell.length > 1) {
            selectTableCell.forEach((cell) => {
                if (selectRow.indexOf(cell.RowIndex) === -1) {
                    selectRow.push(cell.RowIndex);
                }
            });
        }

        //当前单元格属性
        var currentTableRow = ctl.GetElementProperties(ctl.CurrentTableCell());
        if (currentTableRow) {
            var rowspan = selectRow && selectRow.length || currentTableRow.RowSpan;
            var rowAllChooseArr = [];
            for (var i = 1; i <= rowspan; i++) {
                if ((rowspan % i) == 0) {
                    rowAllChooseArr.push(i);
                }
            }
            ctl.ownerDocument.getElementById('dc_detachable_rows').innerHTML = '行数可选值为：' + rowAllChooseArr.join();
        }
        function successFun() {
            let RowNum = dcPanelBody.find("#dc_RowNum").val();
            let columsNum = dcPanelBody.find("#dc_ColumnsNum").val();
            var selectTableCell = ctl.GetSelectTableCells() || [];
            if (selectTableCell && selectTableCell.length > 1) {
                if (selectTableCell.indexOf(RowNum) === -1) {
                    // console.log('行数可选值为：' + rowAllChooseArr.join());
                    return false;
                }
                ctl.DCExecuteCommand("Table_MergeCell", false, null);
            }

            let str = "" + RowNum + "," + columsNum + "";
            ctl.DCExecuteCommand("Table_SplitCellExt", false, str);
        }
    },
    /**
     * 字符套圈
     * @param options 单元格属性
     * @param ctl 编辑器元素
     */
    CharacterCircleDialog: function (options, ctl, isInsertMode) {
        console.log(ctl.CurrentStyle);
        let optionValue = "";
        if (ctl && ctl.CurrentStyle && ctl.CurrentStyle.CharacterCircle) {
            optionValue = ctl.CurrentStyle.CharacterCircle;
        }
        var CharacterCircleHtml = `
             <div class="dc_CharacterCircle_dialog">
                <label>
                    <input class="dc_CharacterCircleinput" checked type="radio" value="None" />
                    <span>无字符套圈</span>
                </label>
                <br />
                <label>
                    <input  class="dc_CharacterCircleinput" type="radio" value="Circle" />
                    <span>圆形字符套圈</span>
                </label>
                <br />
                <label>
                    <input  class="dc_CharacterCircleinput" type="radio" value="Rectangle"  />
                    <span>矩形字符套圈</span>
                </label>
             </div>
            `;
        var dialogOptions = {
            title: "字符套圈",
            dialogContainerBodyWidth: 250,
            bodyHeight: 100,
            bodyClass: "CharacterCircle",
            bodyHtml: CharacterCircleHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        let arr = dcPanelBody.find(".dc_CharacterCircleinput");
        renderRadio(optionValue);
        dcPanelBody.find(".dc_CharacterCircleinput").click(function (e) {
            if (e.target.value) {
                renderRadio(e.target.value);
                optionValue = e.target.value;
            }
        });
        //渲染单选框
        function renderRadio(optionValue1) {
            if (optionValue1 !== "" || optionValue1 !== "None") {
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].value === optionValue1) {
                        jQuery(arr[i]).prop("checked", true);
                    } else {
                        jQuery(arr[i]).removeAttr("checked");
                    }
                }
            }
        }

        function successFun() {
            ctl.DCExecuteCommand("CharacterCircle", false, optionValue);
        }
    },
    /**
     * 插入特殊字符
     * @param options 属性
     * @param ctl 编辑器元素
     */
    InsertSpecifyCharacterDialog: function (options, ctl, isInsertMode) {
        let CharacterType = Object.keys(this.InsertSpecifyCharacterObj);
        let value = "";
        var InsertSpecifyCharacterHtml = `
               <div id="dc_tabButton">
                    <p class="dc_tabButtonItem dc_SpecialCharacters dc_active" tabId="SpecialCharacters">特殊字符</p>
                    <p class="dc_tabButtonItem" tabId="RomanCharacters">罗马字符</p>
                    <p class="dc_tabButtonItem" tabId="NumericCharacters">数字字符</p>
                    <p class="dc_tabButtonItem" tabId="MedicalCharacters">医学字符</p>
               </div>
               <div id="dc_tabDomBox"></div>
            `;
        var dialogOptions = {
            title: "插入特殊字符",
            dialogContainerBodyWidth: 500,
            bodyHeight: 500,
            bodyClass: "InsertSpecifyCharacter",
            bodyHtml: InsertSpecifyCharacterHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        var TabDomList = ``;
        for (var i = 0; i < CharacterType.length; i++) {
            var charSpanDom = ``;
            for (
                var j = 0;
                j < this.InsertSpecifyCharacterObj[CharacterType[i]].length;
                j++
            ) {
                charSpanDom += `
                <span class="dc_charSpanDomItem" title="${this.InsertSpecifyCharacterObj[CharacterType[i]][j]
                    }">${this.InsertSpecifyCharacterObj[CharacterType[i]][j]}</span>
                `;
            }
            TabDomList += `
                <div class="dc_tabDomBoxItem" style="display:${CharacterType[i] === "SpecialCharacters" ? "" : "none"
                }" id="${CharacterType[i]}">${charSpanDom}</div>
            `;
        }
        ctl.ownerDocument.getElementById("dc_tabDomBox").innerHTML = TabDomList;
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        dcPanelBody.find(".dc_tabButtonItem").on("click", function () {
            dcPanelBody.find(".dc_tabButtonItem.dc_active").removeClass("dc_active");
            jQuery(this).addClass("dc_active");
            dcPanelBody.find(".dc_tabDomBoxItem").hide();
            dcPanelBody.find(`#${this.getAttribute("tabId")}`).show();
        });
        dcPanelBody.find(".dc_charSpanDomItem").on("click", function () {
            value = "" + this.innerHTML + "";
            console.log(value);
            ctl.DCExecuteCommand("InsertSpecifyCharacter", false, value);
            // 关闭对话框
            var dc_dialogMark = jQuery(ctl).children("#dc_dialogMark"); //蒙版元素
            var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer"); //对话框元素
            dc_dialogMark.remove();
            dc_dialogContainer.remove();
        });
        function successFun() { }
    },

    /**
     * 编辑文档批注
     * @param options 单元格属性
     * @param ctl 编辑器元素
     */
    EditDocumentCommentsDialog: function (options, ctl, isInsertMode) {
        var EditDocumentCommentsHtml = `
            <div class="dc_Box dc_EditDocumentComments_dialog">
                <h6 class="dc_title">批注内容</h6>
                 <textarea id="dc_Text" data-text="Text" ></textarea>
            </div>
            <div class="dc_Box">
                <h6 class="dc_title">颜色设置</h6>
                <label>
                    <span>背景颜色:</span>
                    <input type="color" value="#f6e6e6" id="dc_BackColor"  data-text="BackColor" />
                </label>
                <label>
                    <span>前景颜色:</span>
                    <input type="color" value="#121111" id="dc_ForeColor"  data-text="ForeColor" />
                </label>
            </div>
            <div class="dc_Box">
                <h6 class="dc_title">作者</h6>
                <p>姓名：<span id="dc_Author"></span></p>
                <p>编号：<span id="dc_AuthorID"></span></p>
            </div>
            <div class="dc_Box dcBody-content">
                <h6 class="dc_title">自定义属性</h6>
                <div id="dc_attr-box"></div>
            </div>
            `;
        var dialogOptions = {
            title: "编辑文档批注",
            bodyHeight: 400,
            bodyClass: "dc_EditDocumentComments",
            bodyHtml: EditDocumentCommentsHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        //打开对话框做数据回填
        if (options) {
            //不可修改的用户属性，只用span做展示
            let Author = options.Author || options.author;
            dcPanelBody.find("#dc_Author").text(Author);
            dcPanelBody.find("#dc_AuthorID").text(options.AuthorID);

            //可修改的文本框、input等
            var keys = Object.keys(options);
            keys.length && keys.map(item => {
                var currentInput = ctl.ownerDocument.getElementById('dc_' + item);
                if (currentInput && currentInput.tagName) {
                    if (['INPUT', 'TEXTAREA'].indexOf(currentInput.tagName) > -1) {
                        if (currentInput.type == "color" && typeof (options[item]) == "string") {
                            // 颜色选择器,将颜色字符串转换为十六进制格式，防止失效【DUWRITER5_0-3694】
                            options[item] = DCTools20221228.colorToHex(options[item]);
                        }
                        currentInput.value = options[item];
                    }
                }

            });
        }
        //渲染自定义属性框
        this.attributeComponents(
            "#dc_attr-box",
            (options && options.Attributes) || {},
            ctl
        );

        var that = this;
        function successFun() {
            var dcAttrBox = dcPanelBody.find("#dc_attr-box");
            let Attributes = that.attributeComponents_getAttributeObj(dcAttrBox);
            var _data = GetOrChangeData(dcPanelBody);
            options = {
                ...options,
                ..._data,
                Attributes
            };
            if (!isInsertMode) {
                ctl.SetCurrentComment(options);
                if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                    var changedOptions = ctl.GetCurrentComment();
                    ctl.EventDialogChangeProperties(changedOptions);
                };
            } else {
                ctl.DCExecuteCommand("insertcomment", false, options);
            }
        }
    },

    /**
     * 表单模式
     * @param options 单元格属性
     * @param ctl 编辑器元素
     */
    formModeDialog: function (options, ctl, isInsertMode) {
        let formMode = ctl.FormView();
        var formModeHtml = `
            <form>
                <label>
                    <input class="dc_radioItem" type="radio" data-text="formDisable" ${formMode && formMode == "Disable" && "checked"
            } name="Disable">
                    <span>不处于表单视图模式</span>
                </label>
                <label>
                    <input class="dc_radioItem" type="radio" data-text="formNormal" ${formMode && formMode == "Normal" && "checked"
            } name="Normal">
                    <span>普通表单视图模式</span>
                </label>
                <label>
                    <input class="dc_radioItem" type="radio" data-text="formStrict" ${formMode && formMode == "Strict" && "checked"
            } name="Strict">
                    <span>严格表单视图模式</span>
                </label>
            </form>
        `;
        var dialogOptions = {
            title: "表单模式",
            bodyHeight: 110,
            bodyClass: "dc_formMode",
            bodyHtml: formModeHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        var newDomArr = ctl.ownerDocument.getElementsByClassName("dc_radioItem");
        jQuery(ctl)
            .find(".dc_radioItem")
            .click(function () {
                for (var i = 0; i < newDomArr.length; i++) {
                    newDomArr[i].checked = false;
                }
                this.checked = true;
            });
        function successFun() {
            for (var i = 0; i < newDomArr.length; i++) {
                if (newDomArr[i].checked) {
                    var result = ctl.ExecuteCommand("FormViewMode", false, newDomArr[i].name);
                    ctl.DocumentOptions.BehaviorOptions.FormView = result;
                    ctl.ApplyDocumentOptions();//更改选项的值
                }
            }
        }
    },

    /**
     * 内容保护模式
     * @param options 单元格属性
     * @param ctl 编辑器元素
     */
    contentProtectedModeDialog: function (options, ctl, isInsertMode) {
        let contentProtectedMode = ctl.ProtectType();
        var contentProtectedModeHtml = `
           <form>
                <label>
                    <input class="dc_radioItem" type="radio" ${contentProtectedMode &&
            contentProtectedMode == "None" &&
            "checked"
            } data-text="Value1"  name="None">
                    <span>不保护内容。</span>
                </label>
                 <label>
                    <input class="dc_radioItem" type="radio" ${contentProtectedMode &&
            contentProtectedMode == "Content" &&
            "checked"
            }  data-text="Value2"  name="Content">
                    <span>保护内容，但可以在中间插入新内容。</span>
                </label>
                <label>
                    <input class="dc_radioItem" type="radio" ${contentProtectedMode &&
            contentProtectedMode == "Range" &&
            "checked"
            }  data-text="Value3"  name="Range">
                    <span>保护区域，中间不能插入新内容。</span>
                </label>
            </form>
        `;
        var dialogOptions = {
            title: "内容保护模式",
            bodyHeight: 110,
            bodyClass: "dc_contentProtectedMode",
            bodyHtml: contentProtectedModeHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        jQuery(ctl)
            .find(".dc_radioItem")
            .click(function () {
                var newDomArr =
                    ctl.ownerDocument.getElementsByClassName("dc_radioItem");
                for (var i = 0; i < newDomArr.length; i++) {
                    newDomArr[i].checked = false;
                }
                this.checked = true;
            });
        function successFun() {
            var newDomArr = ctl.ownerDocument.getElementsByClassName("dc_radioItem");
            for (var i = 0; i < newDomArr.length; i++) {
                if (newDomArr[i].checked) {
                    ctl.ExecuteCommand("ContentProtect", false, newDomArr[i].name);
                }
            }
        }
    },

    /**
     * 用户登录
     * @param options 单元格属性
     * @param ctl 编辑器元素
     */
    loginDialog: function (options, ctl, isInsertMode) {
        // if (!options || typeof (options) != "object") {
        //     return false
        // }
        var loginHtml = `
           <div>
                <label class="dc_flex">
                    <span>用户编号：</span>
                    <input type="text" data-text="Value1">
                </label>
                <label class="dc_flex">
                    <span>姓名：</span>
                    <input type="text" data-text="Value2">
                </label>
                <label class="dc_flex">
                    <span>等级：</span>
                    <input type="number" data-text="Value3">
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "用户登录",
            bodyHeight: 120,
            dialogContainerBodyWidth: 250,
            bodyClass: "dc_login_dialog",
            bodyHtml: loginHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        function successFun() {
            // console.log(jQuery('#textareaBox').val())
        }
    },

    /**
     * 段落
     * @param options 单元格属性
     * @param ctl 编辑器元素
     */
    paragraphDialog: function (options, ctl, isInsertMode) {
        if (!options || typeof options != "object") {
            options = ctl.GetCurrentParagraphStyle();
        }
        console.log(options, "==========options");
        var paragraphHtml = `
            <div>
                <label class="dc_paragraph_paragraphStyle_label">
                    <span>段落列表样式:</span>
                    <select id="dc_paragraphStyle" data-text="ParagraphListStyle"></select>
                </label>
                <div class="dc_Box">
                    <h6 class="dc_title">间距和缩进（单位：三百分之一英寸）</h6>
                    <div class="dc_label_box" id="dc_paragraph_spacing_box">
                        <label>
                            <span>左侧缩进量:</span>
                            <input  class="dc_input_number_data_model" type="number" data-text="LeftIndent"></input>
                            <span class="dc-data-model" dc-text-model="LeftIndent"></span>
                        </label>
                        <label>
                            <span class="dc_label_box_span">首行缩进量:</span>
                            <input  class="dc_input_number_data_model" type="number" data-text="FirstLineIndent"></input>
                            <span class="dc-data-model" dc-text-model="FirstLineIndent"></span>
                        </label>
                        <label>
                            <span class="dc_label_box_span">段落前间距:</span>
                            <input  class="dc_input_number_data_model" type="number" data-text="SpacingBeforeParagraph"></input>
                            <span class="dc-data-model" dc-text-model="SpacingBeforeParagraph"></span>
                        </label>
                        <label>
                            <span class="dc_label_box_span">段落后间距:</span>
                            <input  class="dc_input_number_data_model" type="number" data-text="SpacingAfterParagraph"></input>
                            <span class="dc-data-model" dc-text-model="SpacingAfterParagraph"></span>
                        </label>
                    </div>
                </div>


               
                <label class="dc_paragraph_paragraphStyle_label" >
                    <span class="dc_paragraph_paragraphStyle_label_span">行距:</span>
                    <select id="dc_LineSpacingStyle" class="space" data-text="LineSpacingStyle">
                        <option value="SpaceSingle">单倍行距</option>
                        <option value="Space1pt5">1.5倍行距</option>
                        <option value="SpaceDouble">2倍行距</option>
                        <option value="SpaceExactly">最小值</option>
                        <option value="SpaceSpecify">固定值</option>
                        <option value="SpaceMultiple">多倍行距</option>
                    </select>
                </label>
                <label id="dc_LineSpacingBox" >
                    <span>设置值<span id="dc_LineSpacingUnit"></span>:</span>
                    <input type="number"  data-text="LineSpacing"></input>
                </label>
            </div>
        `;
        var dialogOptions = {
            title: "段落",
            bodyHeight: 260,
            bodyClass: "dc_paragraph",
            bodyHtml: paragraphHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素，复显值
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        //动态生成下拉节点
        let domStr = "";
        BULLETEDLIST.filter((item) => {
            domStr += `<option title="${item.title}" value="${item.title}">${item.data}</option>`;
        });
        dcPanelBody.find("#dc_paragraphStyle").append(domStr);
        options &&
            options.LineSpacingStyle &&
            showLineSpacFn(options.LineSpacingStyle);
        GetOrChangeData(dcPanelBody, options);
        dcPanelBody.find("#dc_LineSpacingStyle").bind("change", function (e) {
            showLineSpacFn(dcPanelBody.find("#dc_LineSpacingStyle").val());
        });
        function showLineSpacFn(value) {
            if (["SpaceSpecify", "SpaceMultiple"].includes(value)) {
                dcPanelBody.find("#dc_LineSpacingBox").show();
                dcPanelBody.find("#dc_LineSpacingUnit").innerHTML =
                    value === "SpaceSpecify" ? "值" : "倍";
            } else {
                dcPanelBody.find("#dc_LineSpacingBox").hide();
            }
        }


        function successFun() {
            let newOptions = GetOrChangeData(dcPanelBody);
            newOptions = {
                ...options,
                ...newOptions,
            };
            console.log(newOptions);
            ctl.SetCurrentParagraphStyle(newOptions);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetCurrentParagraphStyle();
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 表格
     * @param options 查找替换属性
     * @param ctl 编辑器元素
     */
    tableDialog: function (options, ctl, isInsertMode) {
        if (!options || typeof options != "object") {
            var ele = ctl.CurrentTable();
            options = ctl.GetElementProperties(ele);
            if (options == null) {
                return false;
            }
        }
        let optionsID = (options && options.ID) || "";
        var LabelHtml = `
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text">表格ID：</span>
                <input class="dc_full" type="text" data-text="ID" />
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text">内容只读保护：</span>
                <select  class="dc_full" data-text="ContentReadonly">
                    <option value="Inherit">继承上级设置</option>
                    <option value="True">只读</option>
                    <option value="False">不只读</option>
                </select>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text" >启用授权控制：</span>
                <select  class="dc_full" data-text="EnablePermission">
                    <option value="Inherit">继承上级设置</option>
                    <option value="True">是</option>
                    <option value="False">否</option>
                </select>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span class="dcTitle-text" >打印显示：</span>
                <select class="dc_full" data-text="PrintVisibility">
                    <option value="Visible">显示</option>
                    <option value="Hidden">隐藏</option>
                    <option value="None">隐藏而且不占位</option>
                </select>
            </label>
        </div>
        <div class="dcBody-content">
            <div>
                <label>
                    <input type="checkbox"  data-text="AllowUserToResizeRows">
                    <span>用户可调整行高</span>
                </label>
            </div>
            <div class="dc_table_AllowUserToResizeColumns_box">
                <label>
                    <input type="checkbox" data-text="AllowUserToResizeColumns">
                    <span> 用户可调整列宽</span>
                </label>
            </div>
            <div class="dc_table_AllowUserInsertRow_box">
                <label>
                    <input type="checkbox"  data-text="AllowUserInsertRow">
                    <span>用户可新增表格行</span>
                </label>
            </div>
            <div class="dc_table_AllowUserDeleteRow_box">
                <label>
                    <input type="checkbox"  data-text="AllowUserDeleteRow">
                    <span> 用户可删除表格行</span>
                </label>
            </div>
            <div class="dc_table_Deleteable_box">
                <label>
                    <input type="checkbox"  data-text="Deleteable">
                    <span>用户可删除表格</span>
                </label>
            </div> 
            <div class="dc_table_CompressOwnerLineSpacing_box">
                <label>
                    <input type="checkbox"  data-text="CompressOwnerLineSpacing">
                    <span>压缩行间距</span>
                </label>
            </div>
            <div class="dc_table_buttons_box">
                <button id="dctableSetTableBorder">表格边框</button>
                <button id="dctableAutoFixTableWidth">自适应宽度</button>
                <button id="dctableAverageTableRows">平均分布各行</button>
                <button id="dctableAverageTableColumns">平均分布各列</button>
            </div>
            <div class="dc_Box">
                <h6 class="dc_title">表达式：</h6>
                <label class="dc_table_VisibleExpression_label" >
                    <span>可见性表达式：</span>
                    <input data-text="VisibleExpression" type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
                <label class="dc_table_PrintVisibilityExpression_label">
                    <span>打印可见性表达式：</span>
                     <input data-text="PrintVisibilityExpression" type="text">
                    <button class="dc_visible_expression">示例</button>
                </label>
            </div>
            <div class="dc_Box dc_table_ValueBinding_box">
                <h6 class="dc_title">赋值属性：</h6>
                <div class="dc_tab3Content">
                    <label class="dc_table_DataSource_label">
                        <span>数据源名称：</span>
                        <input id="dc_DataSource" type="text" />
                    </label>
                    <label class="dc_table_BindingPath_label">
                        <span>绑定路径：</span>
                        <input id="dc_BindingPath"  type="text" />
                    </label>
                </div>
            </div>
            <div class="dc_Box dc_table_color_box">
                <h6 class="dc_title">背景颜色属性：</h6>
                <div id="dc_TableBackground">
                   <div>
                        <label>
                            <div id="dc_TableBackgroundText_box" data-value=""></div>
                            <input id="dc_TableBackgroundText" dc-target-id="dc_TableBackgroundText_box" type="color">
                        </label>
                   </div>
                </div>
            </div>
            <div class="dcBody-content dc_Box dc_table_custom_attr_box">
                <h6 class="dc_title">自定义属性</h6>
                <div id="dc_attr-box"></div>
            </div>`;

        var dialogOptions = {
            title: "表格属性对话框",
            bodyHeight: 444,
            bodyClass: "dc_tableDialog",
            bodyHtml: LabelHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        let that = this;

        ctl.ownerDocument.getElementById('dctableSetTableBorder').addEventListener('click', function (e) {
            var tableOptions = GetTabDialogOptions();
            console.log(tableOptions, '========tableOptions');
            that.borderShadingcellDialog({
                title: "单元格边框设置",
                type: "tableCell"
            }, ctl, true, tableOptions);
        });
        ctl.ownerDocument.getElementById('dctableAutoFixTableWidth').addEventListener('click', function () {
            ctl.AutoFixTableWidth();
        });
        ctl.ownerDocument.getElementById('dctableAverageTableRows').addEventListener('click', function () {
            ctl.AverageTableRows(optionsID);
        });
        ctl.ownerDocument.getElementById('dctableAverageTableColumns').addEventListener('click', function () {
            ctl.AverageTableColumns(optionsID);
        });
        //渲染自定义属性框
        this.attributeComponents(
            "#dc_attr-box",
            (options && options.Attributes) || {},
            ctl
        );
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");

        if (options && options.ValueBinding) {
            dcPanelBody
                .find("#dc_DataSource")
                .val(options.ValueBinding.DataSource || "");
            dcPanelBody
                .find("#dc_BindingPath")
                .val(options.ValueBinding.BindingPath || "");
        }

        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, opts);
        if (options && options.Style && options.Style.BackgroundColorString) {
            jQuery(ctl).find('#dc_TableBackgroundText').val(options.Style.BackgroundColorString);
            jQuery(ctl).find('#dc_TableBackgroundText_box').css('background-color', options.Style.BackgroundColorString);
            jQuery(ctl).find('#dc_TableBackgroundText_box').attr('data-value', options.Style.BackgroundColorString);
        }

        //表格背景颜色
        jQuery(ctl).find('#dc_TableBackgroundText').change(function () {
            var color = jQuery(this).val();
            jQuery(ctl).find('#dc_TableBackgroundText_box').css('background-color', color);
            jQuery(ctl).find('#dc_TableBackgroundText_box').attr('data-value', color);
        });


        function GetTabDialogOptions() {
            var tableOptions = {};
            var dcAttrBox = dcPanelBody.find("#dc_attr-box");
            let Attributes = that.attributeComponents_getAttributeObj(dcAttrBox);
            var _data = GetOrChangeData(dcPanelBody);
            let DataSource = dcPanelBody.find("#dc_DataSource").val();
            let BindingPath = dcPanelBody.find("#dc_BindingPath").val();
            let BackgroundColorString = jQuery(ctl).find('#dc_TableBackgroundText_box').attr('data-value');

            tableOptions = {
                ..._data,
                ValueBinding: {
                    DataSource,
                    BindingPath,
                },
                Attributes,
                Style: {
                    BackgroundColorString//设置背景颜色
                }
            };

            return tableOptions;
        }


        function successFun() {
            var newOptions = GetTabDialogOptions();
            console.log(newOptions, "========newOptions");
            //循环每一列设置背景颜色
            var columns = options.Columns || [];
            if (columns && columns.length) {
                for (var i = 0; i < columns.length; i++) {
                    ctl.HighlightTableRowColumn(ctl.CurrentTable(), -1, i, newOptions.Style.BackgroundColorString, false);
                }
            }
            ctl.SetElementProperties(ctl.CurrentTable(), newOptions);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentTable());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 单元格
     * @param options 把绑定数据源元素插入到该元素位置最后面
     * @param ctl 编辑器元素
     */
    tableCellDialog: function (options, ctl) {
        var ele = ctl.CurrentTableCell();
        if (!options || typeof options != "object") {
            options = ctl.GetElementProperties(ele);
            if (options == null) {
                return false;
            }
        }

        if (options.Style.Align && options.Style.VerticalAlign) {
            //wyc20230711:修改单元格获取水平对齐的逻辑
            var crtpStyle = ctl.GetCurrentParagraphStyle();
            options["Align"] = crtpStyle.Align;
            options["VerticalAlign"] = options.Style.VerticalAlign;
        }
        if (options.ValueExpression === null || options.ValueExpression == undefined) {
            //lixinyu20240701:修复数值表达式不生效问题
            options["ValueExpression"] = (options.PropertyExpressions && options.PropertyExpressions.FormulaValue) || null;
        }
        var LabelHtml = `
        <div class="dcBody-content">
            <div class="dcBody-content">
                <label class="dc_flex dc_table_cell_Id_label">
                    <span class="dcTitle-text">ID：</span>
                    <input class="dc_full" data-text="ID" />
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_table_cell_ContentReadonly_label">
                    <span class="dcTitle-text">内容只读保护：</span>
                    <select  class="dc_full" data-text="ContentReadonly">
                        <option value="Inherit">继承上级设置</option>
                        <option value="True">只读</option>
                        <option value="False">不只读</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_table_cell_EnablePermission_label">
                    <span class="dcTitle-text">启用授权控制：</span>
                    <select  class="dc_full" data-text="EnablePermission">
                        <option value="Inherit">继承上级设置</option>
                        <option value="True">是</option>
                        <option value="False">否</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_table_cell_CloneType_label">
                    <span class="dcTitle-text">复制模式：</span>
                    <select  class="dc_full" data-text="CloneType">
                        <option value="Default">继承上级设置</option>
                        <option value="ContentWithClearField">复制内容，但删除输入域中的内容</option>
                        <option value="Complete"> 完整的复制，包括输入域中的内容</option>
                        <option value="ClearDirectContentAndFieldContent">清空复制的普通内容和输入域的内容</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_table_cell_MoveFocusHotKey_label dc_flex">
                    <span class="dcTitle-text">焦点快捷键：</span>
                    <select class="dc_full" data-text="MoveFocusHotKey">
                        <option value="None">None</option>
                        <option value="Default">Default</option>
                        <option value="Tab">Tab</option>
                        <option value="Enter">Enter</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_table_cell_AutoFixFontSizeMode_label">
                    <span class="dcTitle-text">缩小字体填充：</span>
                    <select class="dc_full" data-text="AutoFixFontSizeMode">
                        <option value="None">不启用</option>
                        <option value="SingleLine">单行模式</option>
                        <option value="MultiLine">多行模式</option>
                       
                    </select>
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_table_cell_ValueExpression_label">
                    <span class="dcTitle-text">数值表达式：</span>
                    <input class="dc_full" data-text="ValueExpression" type="text" />
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex dc_table_cell_PrintVisibility_label">
                    <span class="dcTitle-text" >打印显示：</span>
                    <select class="dc_full" data-text="PrintVisibility">
                        <option value="Visible">显示</option>
                        <option value="Hidden">隐藏</option>
                        <option value="None">隐藏而且不占位</option>
                    </select>
                </label>
            </div>
            <div class="dcBody-content dc_table_cell_algin_box">
                <span class="dcTitle-text" >对齐方式：</span>
                <div class="dc_table_cell_algin_box_container">
                    <label class="dc_table_cell_Align_label" for="dc_Align" disabled>水平对齐方式:</label>
                        <select class="dc_table_cell_Align_select" disabled id="dc_Align" data-text="Align" >
                            <option value="Left">左对齐</option>
                            <option value="Right">右对齐</option>
                            <option value="Center">居中对齐</option>
                        </select>
                    <label class="dc_table_cell_VerticalAlign_label" for="dc_vertical">垂直对齐方式:</label>
                    <select class="dc_table_cell_VerticalAlign_select" id="dc_vertical" data-text="VerticalAlign">
                        <option value="Top">顶端对齐</option>
                        <option value="Bottom">底端对齐</option>
                        <option value="Middle">居中对齐</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="dc_Box dc_table_cell_padding_box">
            <h6 class="dc_title">内边距：</h6>
            <div class="dc_table_cell_padding_box_container">
                <label class="dc_table_cell_PaddingTop_label">
                    <span>上边距：</span>
                    <input id="dc_PaddingTop" data-text="PaddingTop" class="dc_input_number_data_model" type="number" />
                    <span dc-text-model="PaddingTop"></span>

                </label>
                <label class="dc_table_cell_PaddingBottom_label">
                    <span>下边距：</span>
                    <input id="dc_PaddingBottom" data-text="PaddingBottom" class="dc_input_number_data_model" type="number" />
                    <span dc-text-model="PaddingBottom"></span>
                </label>
                <label class="dc_table_cell_PaddingLeft_label">
                    <span>左边距：</span>
                    <input id="dc_PaddingLeft" data-text="PaddingLeft" class="dc_input_number_data_model" type="number" />
                    <span dc-text-model="PaddingLeft"></span>
                </label>
                <label  class="dc_table_cell_PaddingRight_label">
                    <span>右边距：</span>
                    <input id="dc_PaddingRight" data-text="PaddingRight" class="dc_input_number_data_model" type="number" />
                    <span dc-text-model="PaddingRight"></span>
                </label>               
            </div>
        </div>
        <div id="dc_BorderRenderVisibility" class="dc_Box" >
            <h6 class="dc_title">边框可见性:</h6>
            <div class="dc_table_cell_border_box_container">
                <label class="dc_table_cell_Hidden_label">
                    <input attrId='0' attrValue='Hidden' type="checkbox" />
                    <span >Hidden</span>
                </label>
                <label class="dc_table_cell_Paint_label">
                    <input attrId='1' attrValue='Paint' type="checkbox" />
                    <span >Paint</span>
                </label>   
                <label class="dc_table_cell_Print_label">
                    <input attrId='2' attrValue='Print' type="checkbox" />
                    <span>Print</span>
                </label>   
                <label class="dc_table_cell_PDF_label">
                    <input attrId='4' attrValue='PDF' type="checkbox" />
                    <span>PDF</span>
                </label>  
                <label class="dc_table_cell_All_label">
                    <input attrId='8'  attrValue='All' type="checkbox" />
                    <span>All</span>
                </label>  
                <span class="dc_table_cell_border_wring">注：此属性值不保存到XML中</span>

            </div>
        </div>
        <div id="dc_ContentRenderVisibility" class="dc_Box">
            <h6 class="dc_title">内容可见性:</h6>
            <div class="dc_table_cell_ContentRenderVisibility_box_container" >
                <label class="dc_table_cell_Content_Hidden_label">
                    <input attrId='0' attrValue='Hidden'    type="checkbox" />
                    <span >Hidden</span>
                </label>
                <label class="dc_table_cell_Content_Paint_label" >
                    <input attrId='1' attrValue='Paint' type="checkbox" />
                    <span>Paint</span>
                </label>   
                <label class="dc_table_cell_Content_Print_label" >
                    <input attrId='2' attrValue='Print'  type="checkbox" />
                    <span>Print</span>
                </label>   
                <label class="dc_table_cell_Content_PDF_label">
                    <input attrId='4' attrValue='PDF' type="checkbox" />
                    <span>PDF</span>
                </label>  
                <label class="dc_table_cell_Content_All_label">
                    <input attrId='8'  attrValue='All' type="checkbox" />
                    <span>All</span>
                </label> 
                <span class="dc_table_cell_border_wring">注：此属性值不保存到XML中</span>
            </div>
        </div>
        <div class="dc_Box dc_table_cell_Data_box">
            <h6 class="dc_title">赋值属性：</h6>
            <div class="dc_tab3Content">
                <label class="dc_table_cell_DataSource_label">
                    <span>数据源名称：</span>
                    <input id="dc_DataSource" type="text" />
                </label>
                <label  class="dc_table_cell_BindingPath_label">
                    <span>绑定路径：</span>
                    <input id="dc_BindingPath"  type="text" />
                </label>
            </div>
        </div>
        <div class="dc_Box dc_table_cell_color_box">
            <h6 class="dc_title">背景颜色属性：</h6>
            <div>
                <div style="display:flex;align-items:center;">
                    背景色：
                    <label class="dc_TableCellBackgroundText_label">
                        <div id="dc_TableCellBackgroundText_box" data-value=""></div>
                        <input id="dc_TableCellBackgroundText" type="color" >
                    </label>
                </div>
            </div>
            
        </div>
        <div class="dcBody-content Box">
            <h6 class="dc_title">自定义属性</h6>
            <div id="dc_attr-box"></div>
        </div>  `;

        var dialogOptions = {
            title: "单元格属性对话框",
            bodyHeight: 476,
            bodyClass: "dc_tableCellElementBox",
            bodyHtml: LabelHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);

        //渲染自定义属性框
        this.attributeComponents(
            "#dc_attr-box",
            (options && options.Attributes) || {},
            ctl
        );
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        GetOrChangeData(dcPanelBody, options);
        if (options && options.ValueBinding) {
            dcPanelBody
                .find("#dc_DataSource")
                .val(options.ValueBinding.DataSource || "");
            dcPanelBody
                .find("#dc_BindingPath")
                .val(options.ValueBinding.BindingPath || "");
        }
        if (options.Style && options.Style) {
            ctl.ownerDocument.getElementById("dc_PaddingTop").value =
                options.Style.PaddingTop;
            ctl.ownerDocument.getElementById("dc_PaddingBottom").value =
                options.Style.PaddingBottom;
            ctl.ownerDocument.getElementById("dc_PaddingLeft").value =
                options.Style.PaddingLeft;
            ctl.ownerDocument.getElementById("dc_PaddingRight").value =
                options.Style.PaddingRight;
        }
        if (options && options.BorderRenderVisibility) {
            SetBorderOrContentRenderVisibility(
                "#dc_BorderRenderVisibility",
                options.BorderRenderVisibility
            );
        }
        if (options && options.ContentRenderVisibility) {
            SetBorderOrContentRenderVisibility(
                "#dc_ContentRenderVisibility",
                options.ContentRenderVisibility
            );
        }
        let that = this;
        function getBorderOrContentRenderVisibility(id) {
            let arr = dcPanelBody.find(id).find('input[type="checkbox"]');
            let valueArr = [];
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].checked) {
                    valueArr.push(arr[i].getAttribute("attrValue"));
                }
            }
            return valueArr.join(",");
        }
        function SetBorderOrContentRenderVisibility(id, RenderVisibility) {
            let attrValue = RenderVisibility.split(",");
            for (var i = 0; i < attrValue.length; i++) {
                let itemDom = dcPanelBody
                    .find(id)
                    .find(`input[attrValue=${attrValue[i]}]`);
                jQuery(itemDom).prop("checked", true);
            }
            let arr = dcPanelBody.find(id).find('input[type="checkbox"]');
        }
        //背景色赋值
        if (options && options.Style && options.Style.BackgroundColorString) {
            jQuery(ctl).find('#dc_TableCellBackgroundText').val(options.Style.BackgroundColorString);
            var dc_TableCellBackgroundText_box = jQuery(ctl).find('#dc_TableCellBackgroundText_box');
            dc_TableCellBackgroundText_box.css("background-color", options.Style.BackgroundColorString);
            dc_TableCellBackgroundText_box.attr("data-value", options.Style.BackgroundColorString);
        }

        //背景色改变
        jQuery(ctl).find('#dc_TableCellBackgroundText').change(function () {
            let color = jQuery(this).val();
            var dc_TableCellBackgroundText_box = jQuery(ctl).find('#dc_TableCellBackgroundText_box');
            dc_TableCellBackgroundText_box.css("background-color", color);
            dc_TableCellBackgroundText_box.attr("data-value", color);
        });

        //[DUWRITER5_0-2632]20240515 lxy 判断是否选中了多个单元格，如果选中多个单元格，则禁用所有输入框，仅支持设置垂直对齐方式
        var selectTableCells = ctl.GetSelectTableCells();
        if (selectTableCells && selectTableCells.length >= 2) {
            dcPanelBody.find('input,select').attr('disabled', true);//禁用所有的输入框
            dcPanelBody.find('input').val('');//清空所有输入框
            // dcPanelBody.find('input').prop('checked', false);//取消所有勾选的输入框
            var selectAll = ctl.ownerDocument.querySelectorAll(".dc_tableCellElementBox select");
            selectAll.length && selectAll.forEach(item => item.selectedIndex = -1);//清空所有的下拉框
            //放开垂直对齐方式的输入框
            dcPanelBody.find("#dc_vertical").prop("disabled", false);
            //放开内边距输入框
            dcPanelBody.find("#dc_PaddingTop").prop("disabled", false);
            dcPanelBody.find("#dc_PaddingBottom").prop("disabled", false);
            dcPanelBody.find("#dc_PaddingLeft").prop("disabled", false);
            dcPanelBody.find("#dc_PaddingRight").prop("disabled", false);
        }

        function successFun() {
            //如果选中的是多个单元格，则需要对选中单元格循环设置属性
            if (selectTableCells && selectTableCells.length >= 2) {
                let vertical = dcPanelBody.find("#dc_vertical").val();//水平对齐方式
                //内边距
                let PaddingBottom = dcPanelBody.find("#dc_PaddingBottom").val();
                let PaddingLeft = dcPanelBody.find("#dc_PaddingLeft").val();
                let PaddingRight = dcPanelBody.find("#dc_PaddingRight").val();
                let PaddingTop = dcPanelBody.find("#dc_PaddingTop").val();

                //设置属性
                let selectAllOptions = {
                    Style: {
                        VerticalAlign: vertical,
                        PaddingBottom,
                        PaddingLeft,
                        PaddingRight,
                        PaddingTop
                    }
                };
                selectTableCells.forEach(item => {
                    ctl.SetElementProperties(item.NativeHandle, selectAllOptions);
                });
                return;
            }

            let BorderRenderVisibility = getBorderOrContentRenderVisibility(
                "#dc_BorderRenderVisibility"
            );
            let ContentRenderVisibility = getBorderOrContentRenderVisibility(
                "#dc_ContentRenderVisibility"
            );
            let dcAttrBox = dcPanelBody.find('#dc_attr-box');
            let Attributes = that.attributeComponents_getAttributeObj(dcAttrBox);
            var _data = GetOrChangeData(dcPanelBody);
            let DataSource = dcPanelBody.find("#dc_DataSource").val();
            let BindingPath = dcPanelBody.find("#dc_BindingPath").val();
            let { Align, VerticalAlign, ValueExpression } = _data;
            //单元格内间距
            let PaddingTop = ctl.ownerDocument.getElementById("dc_PaddingTop").value;
            let PaddingBottom =
                ctl.ownerDocument.getElementById("dc_PaddingBottom").value;
            let PaddingLeft =
                ctl.ownerDocument.getElementById("dc_PaddingLeft").value;
            let PaddingRight =
                ctl.ownerDocument.getElementById("dc_PaddingRight").value;
            options = {
                ...options,
                ..._data,
                Attributes,
                ValueBinding: {
                    DataSource,
                    BindingPath,
                },
                Style: {
                    Align,
                    VerticalAlign,
                    PaddingTop,
                    PaddingBottom,
                    PaddingLeft,
                    PaddingRight,
                },
                PropertyExpressions: {
                    FormulaValue: ValueExpression
                },
                ContentRenderVisibility,
                BorderRenderVisibility,
            };
            delete options.Align;
            delete options.VerticalAlign;

            //设置背景颜色
            var dc_TableCellBackgroundText_box = jQuery(ctl).find('#dc_TableCellBackgroundText_box');
            if (dc_TableCellBackgroundText_box.attr("data-value")) {
                options['Style']['BackgroundColorString'] = dc_TableCellBackgroundText_box.attr("data-value");
            }

            console.log(options, '=========options');
            ctl.SetElementProperties(ele, options);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentTableCell());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 表格行
     * @param options 表格行属性
     * @param ctl 编辑器元素
     */
    tableRowDialog: function (options, ctl) {
        var ele = ctl.CurrentTableRow();
        if (options && options.ID) {
            ele = options.ID;
        }
        if (!options || typeof options != "object") {
            options = ctl.GetElementProperties(ele);
            if (options == null) {
                return false;
            }
        }

        var LabelHtml = `
        <div class="dcBody-content">
            <label class="dc_flex">
                <span>ID：</span>
                <input class="dc_full"  type="text" data-text="ID" />
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span>固定高度：</span>
                <input class="dc_full dc_full_SpecifyHeight dc_input_number_data_model"  id="dc_full_SpecifyHeight"  type="number" data-text="SpecifyHeight" id="dc_SpecifyHeight" />
                <span  class="dc_data-text-model" dc-text-model="SpecifyHeight"></span>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span>复制模式：</span>
                <select class="dc_full" data-text="CloneType">
                    <option value="Default">继承上级设置</option>
                    <option value="ContentWithClearField">复制内容，但删除输入域中的内容</option>
                    <option value="Complete"> 完整的复制，包括输入域中的内容</option>
                    <option value="ClearDirectContentAndFieldContent">清空复制的普通内容和输入域的内容</option>
                </select>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span>打印显示：</span>
                <select class="dc_full" data-text="PrintVisibility">
                    <option value="Visible">显示</option>
                    <option value="Hidden">隐藏</option>
                    <option value="None">隐藏而且不占位</option>
                </select>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span >内容只读保护：</span>
                <select  class="dc_full" data-text="ContentReadonly">
                    <option value="Inherit">继承上级设置</option>
                    <option value="True">只读</option>
                    <option value="False">不只读</option>
                </select>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span >启用授权控制：</span>
                <select  class="dc_full" data-text="EnablePermission">
                    <option value="Inherit">继承上级设置</option>
                    <option value="True">是</option>
                    <option value="False">否</option>
                </select>
            </label>
        </div>
        <div class="dcBody-content">
            <label class="dc_flex">
                <span >允许用户调整高度：</span>
                <select  class="dc_full" data-text="AllowUserToResizeHeight">
                   <option value="Inherit">继承上级设置</option>
                    <option value="True">是</option>
                    <option value="False">否</option>
                </select>
            </label>
        </div>
        
        
        <div class="dcBody-content">
            <div>
                <label>
                    <input type="checkbox" data-text="HeaderStyle">
                    <span>在各页顶端以标题形式重复出现</span>
                </label>
            </div>
            <div>
                <label>
                    <input type="checkbox" data-text="NewPage">
                    <span>强制分页</span>
                </label>
            </div> 
            <div>
                <label>
                    <input type="checkbox" data-text="CanSplitByPageLine">
                    <span>是否允许跨页</span>
                </label>
            </div> 
            <div>
                <label>
                    <input type="checkbox" data-text="PrintCellBorder">
                    <span>打印单元格边框</span>
                </label>
            </div> 
        </div>
        <div class="dc_Box">
            <h6 class="dc_title">表达式：</h6>
            <label>
                <span>可见性表达式：</span>
                <input id="dc_VisibleExpression" data-text="VisibleExpression"  type="text">
                <button class="dc_visible_expression">示例</button>
            </label>
            <label>
                <span>打印可见性表达式：</span>
                <input id="dc_PrintVisibilityExpression" data-text="PrintVisibilityExpression" type="text">
                <button class="dc_visible_expression">示例</button>
            </label>
        </div>
        <div class="dc_Box">
            <h6 class="dc_title">赋值属性：</h6>
            <div class="dc_tab3Content">
                <label>
                    <span>数据源名称：</span>
                    <input  id="dc_DataSource" data-text="Datasource" type="text" />
                </label>
                <label>
                    <span>绑定路径：</span>
                    <input id="dc_BindingPath" data-text="BindingPath" type="text" />
                </label>
            </div>
        </div>
        <div class="dc_Box" >
            <h6 class="dc_title">背景颜色属性：</h6>
            <div id="dc_TableRowBackground">
                <div style="display:flex;align-items:center;">
                    表格行背景色：
                    <label style="display:flex;align-items:center;">
                        <div id="dc_TableRowBackgroundText_box" data-value="" ></div>
                        <input id="dc_TableRowBackgroundText" type="color" >
                    </label>
                </div>
            </div>
        </div>
        <div class="dc_Box dcBody-content">
                <h6 class="dc_title">自定义属性</h6>
                <div id="dc_attr-box"></div>
        </div>  
`;
        var dialogOptions = {
            title: "表格行属性对话框",
            bodyHeight: 400,
            bodyClass: "dc_tableRow",
            bodyHtml: LabelHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");

        //获取是否选中多个表格行
        var selectTableCells = ctl.GetSelectTableCells();
        //判断选中的单元格是否为不同的表格行
        var selectTableRow = [];
        if (selectTableCells && selectTableCells.length > 1) {
            for (var i = 0; i < selectTableCells.length; i++) {
                let selectCellItem = selectTableCells[i];
                if (selectCellItem && selectCellItem.TypeName && selectCellItem.TypeName === "XTextTableCellElement") {
                    if (selectTableRow.indexOf(selectCellItem.RowIndex) == -1) {
                        selectTableRow.push(selectCellItem.RowIndex);
                    }
                }
            }
        }


        //渲染自定义属性框
        this.attributeComponents(
            "#dc_attr-box",
            (options && options.Attributes) || {},
            ctl
        );


        var Box2 = dcPanelBody.find(".dcBody-contentflex").find("#Box2")[0];
        var that = this;
        var opts = {};
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(options, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(opts, _txt, _value);
        });
        // 设置禁用
        dc_dialogContainer.find("#Visible").change(function () {
            that.changeFormDisable(Box2, !this.checked);
        });
        GetOrChangeData(dcPanelBody, opts);
        if (options && options.ValueBinding) {
            dcPanelBody
                .find("#dc_DataSource")
                .val(options.ValueBinding.DataSource || "");
            dcPanelBody
                .find("#dc_BindingPath")
                .val(options.ValueBinding.BindingPath || "");
        }
        //可见性表达式 DUWRITER5_0-3053 VisibleExpression和PropertyExpressions.VisibleExpression都指向可见性表达式属性，VisibleExpression优先级更高
        if (options.VisibleExpression === null || options.VisibleExpression === undefined) {
            dcPanelBody
                .find("#dc_VisibleExpression")
                .val((options.PropertyExpressions && options.PropertyExpressions.Visible) || "");
        }
        //打印可见性表达式 DUWRITER5_0-3053
        if (options.PrintVisibilityExpression === null || options.PrintVisibilityExpression === undefined) {
            dcPanelBody
                .find("#dc_PrintVisibilityExpression")
                .val((options.PropertyExpressions && options.PropertyExpressions.PrintVisibility) || "");
        }

        //背景色赋值
        if (options && options.Style && options.Style.BackgroundColorString) {
            jQuery(ctl).find('#dc_TableRowBackgroundText').val(options.Style.BackgroundColorString);
            var dc_TableRowBackgroundText_box = jQuery(ctl).find('#dc_TableRowBackgroundText_box');
            dc_TableRowBackgroundText_box.css("background-color", options.Style.BackgroundColorString);
            dc_TableRowBackgroundText_box.attr("data-value", options.Style.BackgroundColorString);
        }
        jQuery(ctl).find('#dc_TableRowBackgroundText').change(function () {
            let color = jQuery(this).val();
            var dc_TableRowBackgroundText_box = jQuery(ctl).find('#dc_TableRowBackgroundText_box');
            dc_TableRowBackgroundText_box.css("background-color", color);
            dc_TableRowBackgroundText_box.attr("data-value", color);
        });


        //如果是选中了多个表格行
        if (selectTableRow.length > 1) {
            dcPanelBody.find('input,select').attr('disabled', true);//禁用所有的输入框
            dcPanelBody.find('input').val('');//清空所有输入框
            dcPanelBody.find('input').prop('checked', false);//取消所有勾选的输入框
            //清空选中
            var selectAll = ctl.ownerDocument.querySelectorAll("select.dc_full");
            selectAll.length && selectAll.forEach(item => item.selectedIndex = -1);//清空所有的下拉框
            //放开固定高度输入框
            dcPanelBody.find('#dc_SpecifyHeight').attr('disabled', false);//禁用所有的输入框
            dcPanelBody.find('#dc_SpecifyHeight').val(0);//禁用所有的输入框
        }


        function successFun() {
            //当选中多个表格行，只设置选中行的高度
            if (selectTableRow.length > 1) {
                var rowHeightVal = dcPanelBody.find('#dc_SpecifyHeight').val();
                rowHeightVal = parseInt(rowHeightVal);
                var currentTableProp = ctl.GetElementProperties(ctl.CurrentTable());
                let RowsHeight = currentTableProp.RowsHeight;
                for (var i = 0; i < selectTableRow.length; i++) {
                    let index = selectTableRow[i];
                    RowsHeight[index] = rowHeightVal;
                }
                ctl.SetElementProperties(currentTableProp.NativeHandle, { RowsHeight });
                return;
            }

            let dcAttrBox = dcPanelBody.find('#dc_attr-box');
            let Attributes = that.attributeComponents_getAttributeObj(dcAttrBox);
            let DataSource = dcPanelBody.find("#dc_DataSource").val();
            let BindingPath = dcPanelBody.find("#dc_BindingPath").val();
            var _data = GetOrChangeData(dcPanelBody);
            let { PrintVisibilityExpression, VisibleExpression } = _data;
            options = {
                ...options,
                ..._data,
                Attributes,
                ValueBinding: {
                    DataSource, BindingPath
                },
                PropertyExpressions: {
                    PrintVisibility: PrintVisibilityExpression,
                    Visible: VisibleExpression
                },
            };

            // //设置背景颜色

            var dc_TableRowBackgroundText_box = jQuery(ctl).find('#dc_TableRowBackgroundText_box');
            if (dc_TableRowBackgroundText_box.attr("data-value")) {
                options['Style']['BackgroundColorString'] = dc_TableRowBackgroundText_box.attr("data-value");
            }

            ctl.SetElementProperties(ctl.CurrentTableRow(), options);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentTableRow());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 表格列
     * @param options 把绑定数据源元素插入到该元素位置最后面
     * @param ctl 编辑器元素
     */
    tableColumnDialog: function (options, ctl) {
        var ele = ctl.CurrentTableColumn();
        if (!options || typeof options != "object") {
            options = ctl.GetElementProperties(ele);
            if (options == null) {
                return false;
            }
        }
        var LabelHtml = `
         <div class="dc_Box">
            <h6 class="dc_title">赋值属性：</h6>
            <div class="dc_tab3Content">
                <label>
                    <span>数据源名称：</span>
                    <input  id="dc_DataSource" data-text="Datasource" type="text" />
                </label>
                <label>
                    <span>绑定路径：</span>
                    <input  id="dc_BindingPath" data-text="BindingPath" type="text" />
                </label>
            </div>
        </div>
        <div class="dc_Box" >
            <h6 class="dc_title">背景颜色属性：</h6>
            <div id="dc_TableColumnBackground" >
                <span>表格列背景色：</span>
                <label>
                    <div id="dc_TableColumnBackgroundText_box" data-value="" ></div>
                    <input id="dc_TableColumnBackgroundText"  type="color" >
                </label>
            </div>
        </div>
        `;
        var dialogOptions = {
            title: "表格列属性对话框",
            bodyHeight: 210,
            bodyClass: "dc_tableColumn",
            bodyHtml: LabelHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //渲染自定义属性框
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        if (options && options.ValueBinding) {
            dcPanelBody
                .find("#dc_DataSource")
                .val(options.ValueBinding.DataSource || "");
            dcPanelBody
                .find("#dc_BindingPath")
                .val(options.ValueBinding.BindingPath || "");
        }

        //背景色赋值
        if (options && options.Style && options.Style.BackgroundColorString) {
            jQuery(ctl).find('#dc_TableColumnBackgroundText').val(options.Style.BackgroundColorString);
            var dc_TableColumnBackgroundText_box = jQuery(ctl).find('#dc_TableColumnBackgroundText_box');
            dc_TableColumnBackgroundText_box.css("background-color", options.Style.BackgroundColorString);
            dc_TableColumnBackgroundText_box.attr("data-value", options.Style.BackgroundColorString);
        }
        jQuery(ctl).find('#dc_TableColumnBackgroundText').change(function () {
            let color = jQuery(this).val();
            var dc_TableColumnBackgroundText_box = jQuery(ctl).find('#dc_TableColumnBackgroundText_box');
            dc_TableColumnBackgroundText_box.css("background-color", color);
            dc_TableColumnBackgroundText_box.attr("data-value", color);
        });



        function successFun() {
            let DataSource = dcPanelBody.find("#dc_DataSource").val();
            let BindingPath = dcPanelBody.find("#dc_BindingPath").val();
            var BackgroundColorString = null;
            // //设置背景颜色
            var dc_TableColumnBackgroundText_box = jQuery(ctl).find('#dc_TableColumnBackgroundText_box');
            if (dc_TableColumnBackgroundText_box.attr("data-value")) {
                BackgroundColorString = dc_TableColumnBackgroundText_box.attr("data-value");
            }

            var index = options.Index;
            //修改表格列背景颜色
            ctl.HighlightTableRowColumn(ctl.CurrentTable(), -1, index, BackgroundColorString, false);
            options = {
                ValueBinding: {
                    DataSource,
                    BindingPath,
                },
                Style: {
                    BackgroundColorString
                }
            };
            ctl.SetElementProperties(ele, options);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentTableColumn());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 单元格网格线
     * @param options 属性
     * @param ctl 编辑器元素
     */
    cellGridlineDialog: function (options, ctl, isInsertMode) {
        let cell = ctl.CurrentTableCell();
        if (!cell) {
            return;
        }
        options = ctl.GetElementProperties(cell);

        var cellGridlineHtml = `
        <div class="dc_cellGridlineBox">
            <div>
                <input type="checkbox" name="Visible" data-text="Visible">
                <span>网格是否显示</span>
            </div>
            <div id="dc_cellGridlineContent" class="dc_cellGridlineContent dc_Box">
                <h6 class="dc_title">网格线属性</h6>
                <div>
                    <span class="dc_cellGridlineBox-span">网格线样式:</span>
                    <select name="LineStyle" id="dc_LineStyle" data-text="LineStyle"></select>
                </div>
                <div>
                    <span class="dc_cellGridlineBox-span">网格线颜色:</span>
                    <input class="dc_cellGridlineBox-input"  type="color" name="Color" data-text="Color">
                </div>
                <div>
                    <span class="dc_cellGridlineBox-span">网格线宽度(1/300英寸):</span>
                    <input class="dc_cellGridlineBox-input"  type="number" name="LineWidth" data-text="LineWidth">
                </div>
                <div>
                    <span class="dc_cellGridlineBox-span">网格线之间的跨度(CM):</span>
                    <input class="dc_cellGridlineBox-input" type="number" name="GridSpanInCM" data-text="GridSpanInCM">
                </div>
                <div>
                    <input type="checkbox" name="AlignToGridLine" data-text="AlignToGridLine">
                    <span>文本行对齐到网格线</span>
                </div>
                <div>
                    <input type="checkbox" name="Printable" data-text="Printable">
                    <span>打印预览是否显示</span>
                </div>
            </div>
           
        </div>
        `;
        var dialogOptions = {
            title: "单元格网格线",
            bodyClass: "dc_cellGridline",
            bodyHtml: cellGridlineHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        for (var i = 0; i < DASHSTYLE.length; i++) {
            dcPanelBody
                .find("#dc_LineStyle")
                .append(
                    "<option value = '" +
                    DASHSTYLE[i].name +
                    "' > " +
                    DASHSTYLE[i].show +
                    DASHSTYLE[i].name +
                    "</option >"
                );
        }
        let GridLineOptions = options.GridLine
            ? options.GridLine
            : {
                AlignToGridLine: true, //文本行对齐到网格线
                ColorValue: "#000000", //网格线颜色
                GridSpanInCM: 1, //网格线之间的宽度
                LineStyle: "Solid", //网格线样式
                LineWidth: 1, //网格线宽度
                Printable: true, //打印预览是否显示
                Visible: true, //网格是否显示
            };

        dcPanelBody.find("input[data-text=Visible]").on("click", function (e) {
            var isVisible = jQuery(this).is(":checked");
            let inputArr = dcPanelBody.find("#dc_cellGridlineContent").find("input");
            dcPanelBody.find("#dc_LineStyle").attr("disabled", !isVisible);
            for (var i = 0; i < inputArr.length; i++) {
                inputArr[i].disabled = !isVisible;
            }
        });
        dcPanelBody.find("[data-text]").each(function () {
            var _el = jQuery(this);
            var _txt = _el.attr("data-text");
            var low_txt = _txt.toLowerCase();
            var _value = getDown(GridLineOptions, low_txt);
            if (_value == undefined) {
                _value = "";
            }
            getDown(GridLineOptions, _txt, _value);
        });
        GetOrChangeData(dcPanelBody, GridLineOptions);
        //如果没有表格行则重置选择
        if (!options.GridLine) {
            console.log(111);
            console.log(dcPanelBody.find("input[data-text=Visible]")[0]);
            dcPanelBody.find("input[data-text=Visible]")[0].click();
        }
        function successFun() {
            var cell = ctl.CurrentTableCell();
            var _data = GetOrChangeData(dcPanelBody);
            //wyc20230713:该对话框改成只设置网格线不处理其它属性了
            options = {
                GridLine: _data,
            };
            //判断是否为选中多个单元格
            var selectTableCells = ctl.GetSelectTableCells() || [];
            if (selectTableCells && selectTableCells.length && selectTableCells.length > 1) {
                ctl.SetSelectTableCellGridLineInfo(options);
                console.log('设置选择多个单元格的网格线', _data);
            } else {
                ctl.SetElementProperties(cell, options);
            }
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentTableCell());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 单元格斜分线
     * @param options 属性
     * @param ctl 编辑器元素
     */
    cellDiagonalLineDialog: function (options, ctl, isInsertMode) {
        let cell = ctl.CurrentTableCell();
        if (!cell) {
            return;
        }
        options = ctl.GetElementProperties(cell);
        let { SlantSplitLineStyle } = options;
        var cellDiagonalLineHtml = `
        <div class="dc_cellDiagonalLineBox">
           <div class="dc_slantsplitlinestyle-box"> 
                <span>斜分线样式:</span>
                <p id="dc_slantsplitlinestyle">None</p>
            </div>
            <div class="dc_slantsplitlinestyle-item" attrId='None'> 
               <div class="dc_None"></div> 
               <span> None </span>
            </div>
            <div  class="dc_slantsplitlinestyle-item" attrId='TopLeftOneLine'>
                <div class="dc_TopLeftOneLine">
                    <p></p>
                </div>
                <span> TopLeftOneLine </span>
            </div>

            <div  class="dc_slantsplitlinestyle-item" attrId='TopLeftTwoLines'>
                <div class="dc_TopLeftTwoLines">
                    <p></p>
                    <p></p>
                </div>
                <span>TopLeftTwoLines</span>
            </div>

            <div  class="dc_slantsplitlinestyle-item" attrId='TopRightOneLine'>
                <div class="dc_TopRightOneLine">
                    <p></p>
                </div>
                <span>TopRightOneLine</span>
            </div>
            <div  class="dc_slantsplitlinestyle-item" attrId='TopRightTwoLines'>
                <div class="dc_TopRightTwoLines">
                    <p></p>
                    <p></p>
                </div>
                <span>TopRightTwoLines</span>
            </div>
            <div  class="dc_slantsplitlinestyle-item" attrId='BottomRightOneLine'>
                <div class="dc_BottomRightOneLine">
                    <p></p>
                </div>
                <span>BottomRightOneLine</span>
            </div>
            <div  class="dc_slantsplitlinestyle-item" attrId='BottomRigthTwoLines'>
                <div class="dc_BottomRigthTwoLines">
                    <p></p>
                    <p></p>
                </div>
                <span>BottomRigthTwoLines</span>
            </div>
            <div  class="dc_slantsplitlinestyle-item" attrId='BottomLeftOneLine'>
                <div class="dc_BottomLeftOneLine">
                    <p></p>
                </div>
                <span>BottomLeftOneLine</span>
            </div>
            <div  class="dc_slantsplitlinestyle-item" attrId='BottomLeftTwoLines'>
                <div class="dc_BottomLeftTwoLines">
                    <p></p>
                    <p></p>
                </div>
                <span>BottomLeftTwoLines</span>
            </div>
        </div>
        `;
        var dialogOptions = {
            title: "单元格斜分线",
            bodyClass: "dc_cellDiagonalLine",
            bodyHtml: cellDiagonalLineHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        let slantsplitlinestyleArr = jQuery(ctl).find(
            ".dc_slantsplitlinestyle-item"
        );
        if (SlantSplitLineStyle) {
            for (var i = 0; i < slantsplitlinestyleArr.length; i++) {
                if (
                    slantsplitlinestyleArr[i].getAttribute("attrId") ===
                    SlantSplitLineStyle
                ) {
                    slantsplitlinestyleArr[i].style = "background:#d7e4f2;";
                    jQuery(ctl)
                        .find("#dc_slantsplitlinestyle")
                        .html(slantsplitlinestyleArr[i].id);
                }
            }
        }
        slantsplitlinestyleArr.click(function () {
            for (var i = 0; i < slantsplitlinestyleArr.length; i++) {
                slantsplitlinestyleArr[i].style = "background:#fafafa";
            }
            this.style = "background:#d7e4f2;";
            jQuery(ctl)
                .find("#dc_slantsplitlinestyle")
                .html(this.getAttribute("attrId"));
        });
        function successFun() {
            let SlantSplitLineStyle = jQuery(ctl).find("#dc_slantsplitlinestyle").html();
            SlantSplitLineStyle = SlantSplitLineStyle ? SlantSplitLineStyle : "None";
            ctl.SetElementProperties(ctl.CurrentTableCell(), { SlantSplitLineStyle });
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentTableCell());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 编辑图片对话框fabric.js
     * @param options 图片属性
     * @param ctl 编辑器元素
     */
    imgEditDialog: function (options, ctl, isInsertMode) {
        //[DUWRITER5_0-2346]20240423 lxy 如果没有图片属性、或者有图片属性但是图片的路径是空，则不弹出图片编辑对话框
        if (!options || (options && !options.Src)) {
            return false;
        }
        if (ctl.ownerDocument.defaultView.frameElement) {
            fabric.document = ctl.ownerDocument;
        } else {
            fabric.document = window.document;
        }
        //[DUWRITER5_0-2025]20240314 lxy 判断指定元素能否修改;
        var canModify = options && options.ID && ctl.GetCanModify(options.ID);
        if (canModify === false) {
            return false;
        }
        var imgEditHtml = `
        <div id="dc_canvas_box">
            <div class="dc_img_tools_one">
                <div id="dc_img_zoom_box">
                    <div class="zoomBtn" step="0.1" id="dc_img_zoomIn" titlt="放大">
                        <svg class="icon" width="20px" height="20px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M469.333333 469.333333V234.666667a21.333333 21.333333 0 0 1 21.333334-21.333334h42.666666a21.333333 21.333333 0 0 1 21.333334 21.333334V469.333333h234.666666a21.333333 21.333333 0 0 1 21.333334 21.333334v42.666666a21.333333 21.333333 0 0 1-21.333334 21.333334H554.666667v234.666666a21.333333 21.333333 0 0 1-21.333334 21.333334h-42.666666a21.333333 21.333333 0 0 1-21.333334-21.333334V554.666667H234.666667a21.333333 21.333333 0 0 1-21.333334-21.333334v-42.666666a21.333333 21.333333 0 0 1 21.333334-21.333334H469.333333z"/></svg>
                    </div>
                    <select id="zoomSelect">
                        <option value=""></option>
                        <option value="0.1">10%</option>
                        <option value="0.3">30%</option>
                        <option value="0.5">50%</option>
                        <option value="1" selected>100%</option>
                        <option value="1.5">150%</option>
                        <option value="2">200%</option>
                        <option value="4">400%</option>
                        <option value="autowidth">适应宽度</option>
                        <option value="autoheight">适合页面大小</option>
                    </select>
                    <div class="zoomBtn" step="-0.1" id="dc_img_zoomOut" titlt="缩小">
                        <svg class="icon" width="20px" height="20px" viewBox="0 0 1024 1024" version="1.1">
                            <path fill="#707070" d="M213.333333 469.333333m21.333334 0l554.666666 0q21.333333 0 21.333334 21.333334l0 42.666666q0 21.333333-21.333334 21.333334l-554.666666 0q-21.333333 0-21.333334-21.333334l0-42.666666q0-21.333333 21.333334-21.333334Z"/>
                        </svg>
                    </div>
                </div>
                <div class="dc_img_tools_full_box">
                    <div id="dc_imgFullScreen">${IMGEDITSVGOBJECT.QUANPING}</div>
                    <div id="dc_imgCancelFullScreen">${IMGEDITSVGOBJECT.QUXIAOQUANPING}</div>
                </div>
            </div>
            <div id="dc_wrap">
                
                <div id="dc_wrap_imgBox">
                    <canvas width="300" height="200" id="dc_show_canvas" >
                        <p>不支持canvas</p>
                    </canvas>
                </div>



                <div class="dc_Box dc_imgEdit_Content">
                    <div class="dc_list_box">
                        <div class="dc_cg">
                            <div class="dc_mouseMode_box_title">
                                <div class="dc_mouseMode_box_title_line"></div>
                                <div class="dc_mouseMode_box_title_text">常规</div>
                                <div class="dc_mouseMode_box_title_line"></div>
                            </div>
                            <div class="dc_mouseMode_box">
                                <button class="dc_undoBtn dc_mouseModeBtn" title="撤销">${IMGEDITSVGOBJECT.CHEXIAO}</button>
                                <button class="dc_redoBtn dc_mouseModeBtn" title="重做">${IMGEDITSVGOBJECT.CHONGZUO}</button>
                                <button id="dc_del" class="dc_mouseModeBtn" title="删除">${IMGEDITSVGOBJECT.SHANCHU}</button>
                            </div>
                            <div class="dc_mouseMode_box">
                              <label title="填充背景色" class="dc_imgEdit_fill_label">
                                    ${IMGEDITSVGOBJECT.TIANCHONG}
                                    <input type="color" name="fill" id="dc_fill_color">
                                </label>
                                <label title="边框色" class="dc_imgEdit_stroke_label">
                                    ${IMGEDITSVGOBJECT.BIANKUANGSE}
                                    <input type="color" name="stroke" id="dc_stroke_color">
                                </label>
                                <button class="dc_mouseMode" value="SprayBrushMode" title="喷枪">${IMGEDITSVGOBJECT.PENQIANG}</button>
                            </div>
                            <div class="dc_mouseMode_box">
                                <button class="dc_mouseMode" value="Line" title="画直线">${IMGEDITSVGOBJECT.XIANDUAN}</button>
                                <button class="dc_mouseMode" value="draw" title="画笔">${IMGEDITSVGOBJECT.HUABI}</button>
                                <button class="dc_mouseMode" value="Triangle" title="箭头">${IMGEDITSVGOBJECT.JIANTOU}</button>
                            </div>
                            <div class="dc_mouseMode_box_title" style="margin-top:20px;">
                                <div class="dc_mouseMode_box_title_line"></div>
                                <div class="dc_mouseMode_box_title_text">图形</div>
                                <div class="dc_mouseMode_box_title_line"></div>
                            </div>

                            <div class="dc_mouseMode_box">
                                <button class="dc_mouseMode" value="Rect" title="画矩形">${IMGEDITSVGOBJECT.JUXING}</button>
                                <button class="dc_mouseMode" value="Ellipse" title="画椭圆">${IMGEDITSVGOBJECT.TUOYUAN}</button>
                            </div>
                            <div>
                                <label class="dc_mouseMode_label" title="图形绘制时是否添加默认文字">
                                    文本：<input type="checkbox" id="dc_default_text" />
                                </label>
                                <label class="dc_mouseMode_label" title="图形绘制时是否添加底纹">
                                    底纹：<select title="底纹填充" id="dc_back_img_color">
                                            <option value="0">
                                                空白
                                            </option>
                                            <option value="1">
                                                触觉减退
                                            </option>
                                            <option value="2">
                                                触觉消失
                                            </option>
                                            <option value="3">
                                                触觉过敏或异常
                                            </option>
                                            <option value="4">
                                                痛觉减退
                                            </option>
                                            <option value="5">
                                                痛觉消失
                                            </option>
                                            <option value="6">
                                                痛觉过敏或异常
                                            </option>
                                            <option value="7">
                                                震动觉减退或异常
                                            </option>
                                            <option value="8">
                                                位置觉减退或异常
                                            </option>
                                            <option value="9">
                                                浅感觉全部消失
                                            </option>
                                            <option value="10">
                                                深浅感觉全部消失
                                            </option>
                                            <option value="11">
                                                Ⅰ度
                                            </option>
                                            <option value="12">
                                                Ⅱ度
                                            </option>
                                            <option value="13">
                                                深Ⅱ度
                                            </option>
                                            <option value="14">
                                                Ⅲ度
                                            </option>
                                        </select>
                                </label>
                            </div>
                            </div>
                          </div>
                        <div class="dc_mouseMode_box_title" style="margin-top:20px;">
                            <div class="dc_mouseMode_box_title_line"></div>
                            <div class="dc_mouseMode_box_title_text">文本</div>
                            <div class="dc_mouseMode_box_title_line"></div>
                        </div>
                        <div class="dc_wz">
                            <button class="dc_mouseMode" id="dc_InsertText" title="插入文本"  value="SpecialText">${IMGEDITSVGOBJECT.CHARU}</button>
                            <div class="dc_imgEditFont_box">
                                <label title="字号：" class="dc_imgEdit_fontsize_label">
                                    <div style="padding-left:4px;">
                                    ${IMGEDITSVGOBJECT.ZIHAO}：</div>
                                    <select id="dc_fontSize" name="fontSize">
                                    </select>
                                </label>
                                <label title="字体样式：" class="dc_imgEdit_fontsize_label">
                                <div style="padding-left:4px;margin:4px 0;">
                                    ${IMGEDITSVGOBJECT.ZITIYANGSHI}：</div>
                                    <select id="dc_fontFamily" name="fontFamily">
                                    </select>
                                </label>
                                <label title="字体色" class="dc_imgEdit_fontcolor_label">
                                  <div style="padding-left:6px;">
                                    ${IMGEDITSVGOBJECT.ZITIYANSE} ：</div>   
                                    <input type="color" id="dc_fontColor" name="fontColor">
                                </label>
                                <div class="dc_font_base_label" style="font-size:12px;color:#606266;margin-top:6px;">
                                <div> 内容：</div>   
                                    <select id="dc_font_base_select">
                                        <option value="默认文字">默认文字</option>
                                        <option value="◇">◇</option>
                                        <option value="◆">◆</option>
                                        <option value="×">×</option>
                                        <option value="＋">＋</option>
                                        <option value="●">●</option>
                                        <option value="○">○</option>
                                        <option value="△">△</option>
                                        <option value="★">★</option>
                                        <option value="←">←</option>
                                        <option value="→">→</option>
                                        <option value="↑">↑</option>
                                        <option value="↓">↓</option>
                                    </select>
                                </div>
                            </div>
                        </div>

                        <div class="dc_mouseMode_box_title" style="margin-top:20px;">
                            <div class="dc_mouseMode_box_title_line"></div>
                            <div class="dc_mouseMode_box_title_text">其他</div>
                            <div class="dc_mouseMode_box_title_line"></div>
                        </div>
                        <div class="dc_mouseMode_box">
                            <button class="dc_mouseMode" value="rotateLeft" id="dc_rotate_left" title="向左旋转">${IMGEDITSVGOBJECT.XIANGZUOXUANZHUAN}</button>
                            <button class="dc_mouseMode" value="rotateRight" id="dc_rotate_right" title="向右旋转">${IMGEDITSVGOBJECT.XIANGYOUXUANZHUAN}</button>
                        </div>
                      
                        <div class="dc_mouseMode_box" style="margin-top:10px;">
                            <button class="dc_mouseMode" value="cut" id="dc_cut" title="剪切">${IMGEDITSVGOBJECT.JIANQIE}</button>
                            <div class="dc_cut_line"></div>
                            <div id="dc_cut_cencal" title="取消剪裁">${IMGEDITSVGOBJECT.QUXIAO}</div>
                            <div id="dc_cut_confirm" class="dc_cut_confirm" title="使用剪裁" style="color:#259e84">${IMGEDITSVGOBJECT.QUEREN}应用</div>
                        </div>
                        
                </div>
            </div>
        </div>`;

        var dialogOptions = {
            title: "图片编辑对话框",
            bodyHeight: 600,
            dialogContainerBodyWidth: 700,
            bodyClass: "dc_imgEdit",
            bodyHtml: imgEditHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        /** 撤销&重做 */
        var ActionBackNext = {
            state: {
                // 指针标记当前位置
                pointer: -1,
                // 操作记录
                operateList: [],
                // 深度，记录步骤次数
                // deep: 20,
            },
            constructor(canvas) {
                // 保存 canvas 对象
                this.state["canvas"] = canvas;
                // 绑定键盘事件
                this.bindKeyBoard(canvas);
                // 添加也要保存
                this.operateData();
                //鼠标选中事件
                canvas.on("selection:created", function (e) {
                    //当前选中了元素
                    jQuery(ctl).find("#dc_del").removeAttr("disabled");

                });
                // 监听没有选中任何元素的事件
                canvas.on('selection:cleared', function (event) {
                    jQuery(ctl).find("#dc_del").attr("disabled", "disabled");
                });
            },
            /**
             * 修改按钮是否可以用
             * @param canvas
             */
            changeBtnState() {
                // console.log("changeBtnState")
                if (this.state.pointer > 0) {
                    jQuery(ctl).find(".dc_undoBtn").removeAttr("disabled");
                } else {
                    jQuery(ctl).find(".dc_undoBtn").attr("disabled", "disabled");
                }
                if (this.state.pointer + 1 >= this.state.operateList.length) {
                    jQuery(ctl).find(".dc_redoBtn").attr("disabled", "disabled");
                } else {
                    jQuery(ctl).find(".dc_redoBtn").removeAttr("disabled");
                }
            },
            /**
             * 绑定键盘事件
             * @param canvas
             */
            bindKeyBoard(canvas) {
                // $(document).on('keydown', (e) => {
                //   const key = e.originalEvent.keyCode;
                //   switch (key) {
                //     case KeyCode.W: // 上一步
                //       console.log('back');
                //       this.prevStepOperate();
                //       break;
                //     case KeyCode.E: // 下一步
                //       console.log('next');
                //       this.nextStepOperate();
                //       break;
                //   }
                // });
                canvas.on("object:modified", (e) => {
                    // 为了方便保存，调整图形直接触发保存
                    this.operateData();
                });
            },
            /**
             * 操作保存的数据
             */
            operateData() {
                const { canvas, operateList, pointer, deep } = this.state;
                let max = deep;
                let list = [...operateList];
                // 当前状态
                let currentPointer = pointer;
                const json = canvas.toJSON();
                // 更新指针位置
                currentPointer += 1;
                // 考虑到可能存在后续动作，插入队列操作
                list.splice(currentPointer, 0, json);
                if (max && max < list.length) {
                    // 深度存在，则判断当前队列长，超出则从头移出队列
                    list.shift();
                    currentPointer -= 1;
                }
                // 保存数据
                this.setState({
                    operateList: list,
                    pointer: currentPointer,
                });
                this.changeBtnState();
                // console.log('save');
            },
            /**
             * 合并更新
             * @param obj
             */
            setState(obj) {
                this.state = Object.assign(this.state, obj);
            },
            /**
             * 上一步
             */
            prevStepOperate() {
                const { canvas, operateList, pointer } = this.state;
                let list = [...operateList];
                let currentPointer = pointer;
                if (currentPointer > 0) {
                    // 加载前一步
                    currentPointer -= 1;
                    canvas.loadFromJSON(list[currentPointer]).renderAll();
                }
                this.setState({
                    operateList: [...list],
                    pointer: currentPointer,
                });
                this.changeBtnState();
            },
            /**
             * 下一步
             */
            nextStepOperate() {
                const { canvas, operateList, pointer } = this.state;
                let list = [...operateList];
                let currentPointer = pointer;
                // 指针移动
                currentPointer += 1;
                if (currentPointer >= list.length) {
                    this.changeBtnState();
                    return;
                }
                canvas.loadFromJSON(list[currentPointer]).renderAll();
                this.setState({
                    operateList: [...list],
                    pointer: currentPointer,
                });
                this.changeBtnState();
            },
        };
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = dc_dialogContainer.find("#dcPanelBody");
        var canvasElement = dcPanelBody.find("#dc_show_canvas")[0];
        var canvas = new fabric.Canvas(canvasElement, {
            containerClass: "canvasElementContainer",
        });
        var wrap = dcPanelBody.find("#dc_wrap_imgBox")[0];
        var wrapperEl = canvas.wrapperEl;
        var img = new Image();
        if (options && options.Src && options.Src.indexOf("http") == 0) {
            img.setAttribute("crossOrigin", "Anonymous");
        }
        img.src = "data:image/png;base64," + options.Src;
        //保留一遍原始图片的宽高
        var imgOptions = null;
        //用于记录旋转后的宽高
        var imgRotateOptions = null;

        img.onload = function () {
            //使用实际图片宽高

            imgOptions = {
                width: img.width,
                height: img.height,
            };
            // 设置画布的宽高
            canvas.setDimensions({
                width: img.width,
                height: img.height,
            });
            wrapperEl.setAttribute("native-width", img.width);
            wrapperEl.setAttribute("native-height", img.height);

            if (
                options &&
                options.Attributes &&
                options.Attributes.inneradditionshape
            ) {
                var loadobj = JSON.parse(options.Attributes.inneradditionshape);
                canvas.loadFromJSON(loadobj, function () {
                    canvas.renderAll();
                    ActionBackNext.constructor(canvas);
                });
            } else {
                // 将base64图片设置成背景
                canvas.setBackgroundImage(img.src, function () {
                    canvas.renderAll(); // 刷新画布
                    ActionBackNext.constructor(canvas);
                });
            }
            dcPanelBody.find(".dc_mouseMode").click(handleMouseModeChange);

            // , {
            // scaleX: canvas.width / options.Width, scaleY: canvas.height / options.Height
            // }
        };
        /**
         * 放大缩小图片编辑
         * @param {*} zoom 放大缩小倍数
         * @returns 
         */
        var SetCanvasZoom = function (zoom) {
            if (typeof (zoom) != "number") {
                return;
            }
            // 控制缩放范围在 0.01~20 的区间内
            if (zoom > 20) zoom = 20;
            if (zoom < 0.01) zoom = 0.01;
            // 设置画布缩放比例
            canvas.setZoom(zoom);
            // 设置画布的宽高
            canvas.setDimensions({
                width: parseFloat(wrapperEl.getAttribute("native-width")) * zoom,
                height: parseFloat(wrapperEl.getAttribute("native-height")) * zoom
            });
        };
        var mouseMode = "Rect";
        /**
         * 监听模式改变事件
         * @param {*} event 事件Event
         * @returns {*} null
         */
        var handleMouseModeChange = function (event) {
            if (!mouseMode) return;
            mouseMode = this.value;
            clearAllEvents();
            //剪切功能清除
            canvas.remove(selectionRect);
            selectionRect = null;
            // 选择模式
            switch (mouseMode) {
                case "Rect":
                    handleRectMode();
                    break;
                case "Ellipse":
                    handleEllipseMode();
                    break;
                case "Line":
                    handleLineMode();
                    break;
                case "IText":
                    handleTextMode();
                    break;
                case "SpecialText":
                    var font = dcPanelBody.find("#dc_font_base_select").val();
                    handleTextMode(font);
                    break;
                case 'SprayBrushMode':
                    handleSprayBrushMode();
                    break;
                case 'draw':
                    handleDraw();
                    break;
                case 'Triangle':
                    handleTriangle();
                    break;
                case 'cut':
                    handleCut();
                    break;
                case 'rotateLeft':
                    handleRotate('left');
                    break;
                case 'rotateRight':
                    handleRotate('right');
                    break;
                default:
                    break;
            }
            var mouseModeBtns = jQuery(ctl).find(".dc_mouseMode");
            for (var i = 0; i < mouseModeBtns.length; i++) {
                if (mouseModeBtns[i] == this) {
                    mouseModeBtns[i].classList.add("active");
                } else {
                    mouseModeBtns[i].classList.remove("active");
                }
            }
        };
        /**
         * 将所有事件清除掉
         * @returns {*} null
         */
        var clearAllEvents = function () {
            // 在添加事件之前先把该事件清除掉，以免重复添加
            canvas.off("mouse:down");
            canvas.off("mouse:move");
            canvas.off("mouse:up");
            canvas.isDrawingMode = false;//停止绘制
            var mouseModeBtns = jQuery(ctl).find(".dc_mouseMode.active");
            for (var i = 0; i < mouseModeBtns.length; i++) {
                mouseModeBtns[i].classList.remove("active");
            }
        };


        /**
         * 监听鼠标矩形事件
         * @returns {*} null
         */
        var handleRectMode = function () {
            clearAllEvents();
            var isMouseDown = false; //是否鼠标按下
            var zoom = canvas.getZoom();
            var rect;
            var rectObj = {};
            canvas.on("mouse:down", function (options) {
                if (options.target) {
                    return;
                }
                var left = options.pointer.x / zoom;
                var top = options.pointer.y / zoom;
                let fill = dcPanelBody.find("#dc_fill_color").val();
                let stroke = dcPanelBody.find("#dc_stroke_color").val();
                isMouseDown = true;
                rectObj = {
                    left: left,
                    top: top,
                    fill,
                    stroke,
                    opacity: 0.6,
                };
                rect = new fabric.Rect(rectObj);
                // 添加矩形到画布上
                canvas.add(rect);
            });
            canvas.on("mouse:move", function (options) {
                if (isMouseDown) {
                    var move_left = options.pointer.x / zoom - rect.left;
                    var move_top = options.pointer.y / zoom - rect.top;
                    rect.set({
                        width: move_left,
                        height: move_top,
                    });
                    canvas.renderAll();
                }
            });
            canvas.on("mouse:up", function (options) {
                isMouseDown = false;
                // console.log(rect, "======rect");
                if (rect) {
                    if (rect.get("width") == 0 || rect.get("height") == 0) {
                        canvas.remove(rect); // 移除这个矩形
                    } else {
                        ActionBackNext.operateData();

                        //[DUWRITER5_0-3980] 20241217 增加椭圆中带有文本的功能
                        var textId = 'text_rect_' + new Date().getTime();
                        rect.set({ 'targetTextId': textId });

                        var isHasText = dcPanelBody.find("#dc_default_text")[0].checked;
                        isHasText && addCenterText(rect, rectObj, canvas);
                    }
                }
                clearAllEvents();
            });
        };

        /**
            * 监听鼠标椭圆形事件
            * @returns {*} null
            */
        var handleEllipseMode = function () {
            clearAllEvents();
            var isMouseDown = false; //是否鼠标按下
            var zoom = canvas.getZoom();
            var Ellipse;
            var downPointer;
            var EllipseObj = null;
            canvas.on("mouse:down", function (options) {
                if (options.target) {
                    return;
                }
                downPointer = options.pointer;
                var left = options.pointer.x / zoom;
                var top = options.pointer.y / zoom;
                let fill = dcPanelBody.find("#dc_fill_color").val();
                let stroke = dcPanelBody.find("#dc_stroke_color").val();
                isMouseDown = true;
                EllipseObj = {
                    left,
                    top,
                    fill,
                    stroke,
                    opacity: 0.6,
                };
                Ellipse = new fabric.Ellipse(EllipseObj);
                // 添加椭圆形到画布上
                canvas.add(Ellipse);
            });
            canvas.on("mouse:move", function (options) {
                if (isMouseDown) {
                    var move_left = options.pointer.x / zoom - downPointer.x / zoom;
                    var move_top = options.pointer.y / zoom - downPointer.y / zoom;
                    if (move_left != 0 || move_top != 0) {
                        EllipseObj = {
                            left: Math.min(options.pointer.x / zoom, downPointer.x / zoom),
                            top: Math.min(options.pointer.y / zoom, downPointer.y / zoom),
                            rx: Math.abs(move_left) / 2,
                            ry: Math.abs(move_top) / 2,
                        };
                        Ellipse.set(EllipseObj);
                        canvas.renderAll();
                    }
                }
            });
            canvas.on("mouse:up", function (options) {
                isMouseDown = false;

                isHasText = dcPanelBody.find("#dc_default_text")[0].checked;
                // 修复图片编辑弹框报错问题【DUWRITER5_0-2993】
                if (Ellipse) {
                    if ((Ellipse.get("rx") == 0 || Ellipse.get("ry") == 0)) {
                        canvas.remove(Ellipse); // 移除这个椭圆形
                    } else {
                        ActionBackNext.operateData();
                        //[DUWRITER5_0-3980] 20241217 增加椭圆中带有文本的功能
                        var textId = 'text_Ellipse_' + new Date().getTime();
                        Ellipse.set({ 'targetTextId': textId });

                        var isHasText = dcPanelBody.find("#dc_default_text")[0].checked;
                        isHasText && addCenterText(Ellipse, EllipseObj, canvas);
                    }
                }
                clearAllEvents();
            });




        };
        /**
         * 增加一个居中文本给椭圆、矩形  
         *  //[DUWRITER5_0-3980] 20241217 增加椭圆中带有文本的功能
         * @returns {*} null
         */
        var addCenterText = function (targetEle, targetEleObj, canvas) {
            // 在添加事件之前先把该事件清除掉，以免重复添加
            clearAllEvents();

            //获取这个目标图形的坐标
            var Left = targetEleObj.left;
            var Top = targetEleObj.top;
            // 给椭圆添加文本,并设置文本对于椭圆的位置，要求文本在椭圆的中心位置，水平和垂直都居中，如果超出椭圆可以在适当位置换行
            var text = new fabric.IText("默认文字", {
                left: Left,
                top: Top,
                fontFamily: dcPanelBody.find("#dc_fontFamily").val(),
                fontSize: dcPanelBody.find("#dc_fontSize").val(),
                fill: dcPanelBody.find("#dc_fontColor").val(),
                textId: targetEle.targetTextId,
            });
            canvas.add(text);
            // text.set({
            //     textId: targetEle.targetTextId,
            // });
            canvas.renderAll();
            ActionBackNext.operateData();
            // 将文本设置为活动对象
            canvas.setActiveObject(text);
            text.enterEditing();  // 进入编辑模式
            if (text) {
                // 监听修改文本完成后再次修正位置为居中;
                text.on("editing:exited", function () {
                    //重新获取这个目标图形的坐标
                    Left = targetEle.left;
                    Top = targetEle.top;
                    var targetEleWidth = targetEle.width;
                    var targetEleHeight = targetEle.height;
                    var textWidth = text.width;
                    var textHeight = text.height;
                    var textLeft = (targetEleWidth - textWidth) / 2;
                    var textTop = (targetEleHeight - textHeight) / 2;
                    text.set({
                        top: Top + textTop,
                        left: Left + textLeft,
                    });
                    canvas.renderAll();
                    canvas.setActiveObject(text);
                });

                // //将targetEle和text互相关联，互相跟随移动
                text.on("moving", function () {
                    var notSelectedObjects = [];
                    canvas.getObjects().forEach(function (obj) {
                        if (obj && !obj.selected) {//判断是否为选中状态
                            notSelectedObjects.push(obj);
                        }
                    });

                    var targetObj = notSelectedObjects.find(function (obj) {
                        return obj.targetTextId == text.textId;
                    });

                    if (targetObj) {
                        //跟随文本的中心点移动椭圆
                        var left = text.left;
                        var top = text.top;
                        var textWidth = text.width;
                        var textHeight = text.height;
                        var targetObjWidth = targetObj.width;
                        var targetObjHeigth = targetObj.height;

                        targetObj.set({
                            left: left + ((textWidth - targetObjWidth) / 2),
                            top: top + ((textHeight - targetObjHeigth) / 2),
                        });
                        //文本元素置于最上层
                        canvas.bringToFront(text);
                    }
                });


                //给椭圆添加事件监听，在移动椭圆形的时候，跟随移动文本
                targetEle.on("moving", function (e) {
                    //获取所有选中的元素
                    var notSelectedObjects = [];
                    canvas.getObjects().forEach(function (obj) {
                        if (obj && !obj.selected) {//判断是否为选中状态
                            notSelectedObjects.push(obj);
                        }
                    });
                    //获取没有被移动的对应文本，使文本跟随椭圆形移动（防止多选元素移动时，重复操作的移动）
                    var targetText = notSelectedObjects.find(function (obj) {
                        return obj.textId == targetEle.targetTextId;
                    });
                    if (targetText) {
                        var textWidth = text.width;
                        var textHeight = text.height;
                        var textLeft = (targetEle.width - textWidth) / 2;
                        var textTop = (targetEle.height - textHeight) / 2;
                        targetText.set({
                            top: targetEle.top + textTop,
                            left: targetEle.left + textLeft,
                        });
                        //文本元素置于最上层
                        canvas.bringToFront(text);
                    }
                });
            }
        };
        /**
         * 监听鼠标线段事件
         * @returns {*} null
         */
        var handleLineMode = function () {
            clearAllEvents();
            var isMouseDown = false; //是否鼠标按下
            var zoom = canvas.getZoom();
            var Line;
            var downPointer;
            canvas.on("mouse:down", function (options) {
                if (options.target) {
                    return;
                }
                downPointer = options.pointer;
                // var left = options.pointer.x;
                // var top = options.pointer.y;
                isMouseDown = true;
                // Line = new fabric.Line([left, top],{
                //   stroke: 'black', // 笔触颜色
                // });
                // 添加椭圆形到画布上
                // canvas.add(Line);
            });
            canvas.on("mouse:move", function (options) {
                if (isMouseDown) {
                    var move_left = options.pointer.x / zoom - downPointer.x / zoom;
                    var move_top = options.pointer.y / zoom - downPointer.y / zoom;
                    canvas.remove(Line);
                    if (move_left != 0 || move_top != 0) {
                        var obj = {
                            x2: options.pointer.x / zoom,
                            y2: options.pointer.y / zoom
                        };
                        Line = new fabric.Line([downPointer.x / zoom, downPointer.y / zoom, obj.x2, obj.y2], {
                            stroke: 'black', // 笔触颜色
                        });
                        // 添加椭圆形到画布上
                        canvas.add(Line);
                        // Line.set(obj);
                        canvas.renderAll();
                    }
                }
            });
            canvas.on("mouse:up", function (options) {
                isMouseDown = false;
                clearAllEvents();
                if (Line) {
                    ActionBackNext.operateData();
                }
            });
        };

        /**
         * 监听鼠标添加文字事件
         * @returns {*} null
         */
        var handleTextMode = function (font) {
            // 在添加事件之前先把该事件清除掉，以免重复添加
            clearAllEvents();
            // canvas.on('mouse:down', function (options) {
            // });
            // canvas.on('mouse:move', function (options) {
            // });
            canvas.on("mouse:up", function (options) {
                if (options.target) {
                    clearAllEvents();
                    return;
                }
                var dc_active = canvas.getActiveObject();
                if (dc_active) {
                    clearAllEvents();
                    return;
                }
                var zoom = canvas.getZoom();
                var left = options.pointer.x / zoom;
                var top = options.pointer.y / zoom;
                var IText = new fabric.IText(font ? font : "默认文字", {
                    left: left,
                    top: top,
                    fontFamily: dcPanelBody.find("#dc_fontFamily").val(),
                    fontSize: dcPanelBody.find("#dc_fontSize").val(),
                    fill: dcPanelBody.find("#dc_fontColor").val(),
                });
                canvas.add(IText).setActiveObject(IText);
                font ? null : IText.exitEditing();
                clearAllEvents();
                ActionBackNext.operateData();
            });
        };

        /**
        * 监听鼠标喷枪事件
        * @returns {*} null
        */
        var handleSprayBrushMode = function () {
            clearAllEvents();
            var isSpraying = false;
            var sprayDensity = 40; // 喷雾密度，可以根据需要进行调整

            canvas.on("mouse:down", function (options) {
                isSpraying = true;
                spray(options.pointer);
            });

            canvas.on("mouse:move", function (options) {
                if (isSpraying) {
                    spray(options.pointer);
                }
            });

            canvas.on("mouse:up", function (options) {
                isSpraying = false;
                clearAllEvents();
                ActionBackNext.operateData();
            });
            //喷雾绘制
            function spray(pointer) {
                var zoom = canvas.getZoom();
                var x = pointer.x / zoom;
                var y = pointer.y / zoom;
                for (var i = 0; i < sprayDensity; i++) {
                    var offsetX = fabric.util.getRandomInt(-40, 40);
                    var offsetY = fabric.util.getRandomInt(-40, 40);
                    var sprayPoint = new fabric.Circle({
                        radius: 2,
                        fill: "black",
                        selectable: true
                    });
                    sprayPoint.left = x + offsetX;
                    sprayPoint.top = y + offsetY;
                    canvas.add(sprayPoint);
                    canvas.discardActiveObject().renderAll();
                }
            }
        };

        /**
        * 绘制
        * @returns {*} null
        */
        function handleDraw() {
            clearAllEvents();
            // 设置绘制时的线颜色
            var color = ctl.ownerDocument.getElementById('dc_stroke_color').value;
            canvas.freeDrawingBrush.color = color;

            // 设置绘制时的线段粗细
            canvas.freeDrawingBrush.width = 3;

            canvas.isDrawingMode = true;
            canvas.on("mouse:move", function (options) {
                canvas.discardActiveObject().renderAll();
            });
            canvas.on("mouse:up", function (options) {
                ActionBackNext.operateData();
                canvas.isDrawingMode = false;
                clearAllEvents();
                var objects = canvas.getObjects();
                if (objects.length > 0) {
                    var lastObject = objects[objects.length - 1];
                    canvas.setActiveObject(lastObject);
                }
            });
        }

        /**
        * 箭头
        * @returns {*} null
        */
        function handleTriangle() {
            clearAllEvents(); // 清除事件监听
            let fill = dcPanelBody.find("#dc_fill_color").val();
            var isMouseDown = false;
            var arrow;
            var startPoint;
            canvas.on("mouse:down", function (options) {
                if (options.target) {
                    return;
                }
                isMouseDown = true;
                startPoint = canvas.getPointer(options.e);
            });
            canvas.on("mouse:move", function (options) {
                if (isMouseDown) {
                    var pointer = canvas.getPointer(options.e);
                    var angle = Math.atan2(pointer.y - startPoint.y, pointer.x - startPoint.x) + Math.PI / 2; // 加上90度
                    var arrowBaseX = pointer.x - 10 * Math.cos(angle); // 调整箭头位置使其居中
                    var arrowBaseY = pointer.y - 10 * Math.sin(angle); // 调整箭头位置使其居中
                    // 移除旧箭头
                    if (arrow) {
                        canvas.remove(arrow);
                    }
                    // 创建线段
                    var line = new fabric.Line([startPoint.x, startPoint.y, pointer.x, pointer.y], {
                        stroke: fill,
                        strokeWidth: 2,
                    });
                    // 创建三角形箭头
                    var triangle = new fabric.Triangle({
                        left: arrowBaseX,
                        top: arrowBaseY,
                        width: 20,
                        height: 30,
                        fill: fill,
                        angle: angle * 180 / Math.PI,
                    });
                    // 将线段和三角形组合为一个元素
                    arrow = new fabric.Group([line, triangle], {
                        selectable: true,
                    });
                    canvas.add(arrow);
                    canvas.renderAll();
                }
            });
            canvas.on("mouse:up", function () {
                var objects = canvas.getObjects();
                if (objects.length > 0) {
                    var lastObject = objects[objects.length - 1];
                    canvas.setActiveObject(lastObject);
                }
                ActionBackNext.operateData();
                isMouseDown = false;
                clearAllEvents(); // 清除事件监听
            });
        }

        /**
         * 剪切
         * @returns {*} null
         */
        var selectionRect = null;
        function handleCut() {
            clearAllEvents(); // 清除事件监听
            var isDrawing = false;
            var startX, startY, endX, endY;

            canvas.on({
                'mouse:down': function (e) {
                    isDrawing = true;
                    startX = e.pointer.x;
                    startY = e.pointer.y;
                    selectionRect = new fabric.Rect({
                        left: startX,
                        top: startY,
                        originX: 'left',
                        originY: 'top',
                        fill: '#00000000',
                        // stroke: '#409eff',
                        strokeWidth: 1,
                        width: 1,
                        height: 1,
                        selectable: true,
                        evented: true,// 确保矩形可以调整大小
                        hasRotatingPoint: true// 确保矩形可以旋转
                    });
                    canvas.add(selectionRect);
                    selectionRect.on('scaling', function (options) {
                        // 在缩放事件中更新宽度和高度
                        var scaleX = selectionRect.scaleX;
                        var scaleY = selectionRect.scaleY;
                        var newWidth = selectionRect.width * scaleX;
                        var newHeight = selectionRect.height * scaleY;
                        selectionRect.set({
                            width: newWidth,
                            height: newHeight,
                            scaleX: 1,
                            scaleY: 1
                        });
                        canvas.renderAll();
                    });

                    selectionRect.on('rotating', function (options) {
                        // 在旋转事件中更新角度
                        var angle = selectionRect.angle;
                        selectionRect.set({
                            angle: angle
                        });
                        canvas.renderAll();
                    });

                    selectionRect.on('moving', function (options) {
                        // 在移动事件中更新左上角位置
                        var left = selectionRect.left;
                        var top = selectionRect.top;
                        selectionRect.set({
                            left: left,
                            top: top
                        });
                        canvas.renderAll();
                    });

                },
                'mouse:move': function (e) {
                    if (!isDrawing) return;
                    const endX = e.pointer.x;
                    const endY = e.pointer.y;
                    selectionRect.set({
                        width: Math.abs(endX - startX),
                        height: Math.abs(endY - startY),
                        left: Math.min(startX, endX),
                        top: Math.min(startY, endY),
                    });
                    canvas.renderAll();
                },
                'mouse:up': function (e) {
                    isDrawing = false;
                    clearAllEvents(); // 清除事件监听
                }
            });

        }
        //剪切的确认事件
        dcPanelBody.find("#dc_cut_confirm").click(function () {
            //将selectionRect区域中的内容截取为图片base64
            if (!selectionRect) { return; }
            var imgData = canvas.toDataURL({
                format: 'png',
                left: selectionRect.left - 1,
                top: selectionRect.top - 1,
                width: selectionRect.width + 1,
                height: selectionRect.height + 1,
            });

            // 重置画布背景图
            var img = new Image();
            img.src = imgData;
            img.onload = function () {

                // console.log(imgOptions, img.width, img.height);
                // imgRotateOptions = {
                //     width: img.width,
                //     height: img.height,
                // };
                //修改canvas的宽高
                canvas.setDimensions({
                    width: img.width,
                    height: img.height,
                });
                wrapperEl.setAttribute("native-width", img.width);
                wrapperEl.setAttribute("native-height", img.height);
                // //用于记录旋转后的宽高
                imgRotateOptions = {
                    width: img.width,
                    height: img.height,
                };

                // 将base64图片设置成背景
                canvas.setBackgroundImage(img.src, function () {
                    canvas.renderAll(); // 刷新画布
                    canvas.remove(selectionRect);
                    selectionRect = null;
                    ActionBackNext.constructor(canvas);

                });
            };
        });
        //剪切取消事件
        dcPanelBody.find("#dc_cut_cencal").click(function () {
            canvas.remove(selectionRect);
            selectionRect = null;
        });



        //旋转
        function handleRotate(type) {
            clearAllEvents(); // 清除事件监听

            // 先获取当前的背景图
            var backgroundImage = canvas.backgroundImage;
            if (backgroundImage) {
                // 获取背景图的 base64 编码字符串
                var base64Image = backgroundImage.toDataURL({
                    format: 'png', // 你可以根据需要选择格式，例如 'jpeg'
                    quality: 1 // 图像质量，1 是最高质量
                });




                if (type === 'left' || type === 'right') {
                    // 重置画布背景图
                    var img = new Image();
                    img.src = base64Image;
                    img.onload = function () {
                        //旋转前先设置画布为旋转后的大小,防止旋转后canvas放不下图片
                        canvas.setDimensions({
                            width: img.height,
                            height: img.width,
                        });
                        wrapperEl.setAttribute("native-width", img.height);
                        wrapperEl.setAttribute("native-height", img.width);
                        // //用于记录旋转后的宽高
                        imgRotateOptions = {
                            width: img.height,
                            height: img.width,
                        };
                        //获取旋转后的图片
                        rotateBase64Image(base64Image, type === 'left' ? -90 : 90) // 旋转90度
                            .then(newBase64String => {
                                // 将旋转后的图片设置成canvas的背景图
                                canvas.setBackgroundImage(newBase64String, canvas.renderAll.bind(canvas));
                            })
                            .catch(err => {
                                console.error('图片旋转失败:', err);
                            });
                    };
                }
            } else {
                console.log("没有背景图");
            }

        };
        //旋转图片
        function rotateBase64Image(base64String, angle) {
            return new Promise((resolve, reject) => {
                // 创建一个新的Image对象
                const img = new Image();

                // 设置图片加载完成后的处理函数
                img.onload = () => {
                    // 创建一个canvas元素
                    const canvas = document.createElement('canvas');
                    const ctx = canvas.getContext('2d');

                    // 计算旋转后canvas的尺寸
                    canvas.width = img.height;
                    canvas.height = img.width;

                    // 保存当前canvas的上下文状态
                    ctx.save();

                    // 将canvas的原点移动到中心
                    ctx.translate(canvas.width / 2, canvas.height / 2);

                    // 旋转canvas
                    ctx.rotate(angle * Math.PI / 180);

                    // 绘制图片
                    ctx.drawImage(img, -img.width / 2, -img.height / 2);

                    // 恢复canvas的上下文状态
                    ctx.restore();

                    // 将canvas内容转换为新的Base64字符串
                    const newBase64String = canvas.toDataURL(img.src.split(';')[0].split(':')[1]);
                    resolve(newBase64String);
                };

                // 设置图片加载错误的处理函数
                img.onerror = (err) => {
                    reject(err);
                };

                // 设置图片的src属性为Base64字符串
                img.src = base64String;
            });
        }


        // 切换tab页
        dcPanelBody.find(".dc_list li").click(function () {
            // 修改样式
            $(this).addClass("dc_imgEdit_active").siblings("li").removeClass("dc_imgEdit_active");
            var name = $(this).attr("name");
            dcPanelBody.find(".dc_list_box > div").hide();
            dcPanelBody.find(".dc_list_box > div." + name).show();
        });
        // 模拟点击第一个tab页
        dcPanelBody.find(".dc_list li:first").click();
        /**
        * 改变颜色
        * @param {*} color_str 颜色字符串
        * @returns {*} null
        */
        var changeColor = function (color_str, isBackColor = "fill") {
            var active = canvas.getActiveObject();
            if (active) {
                console.log(active.type);
                var name = isBackColor;
                // 设置填充颜色或者边框颜色
                // 矩形 椭圆形 线段 圆形 折线
                if (active.type == "rect" || active.type == "ellipse" || active.type == "line" || active.type == "circle" || active.type == "path") {
                    var obj = {};
                    obj[name] = color_str;
                    active.set(obj);
                    canvas.renderAll();
                    ActionBackNext.operateData();
                } else if (active.type == "activeSelection" || active.type == "group") {
                    // 选中多个内容 || 组，目前只有箭头
                    active.forEachObject(function (obj) {
                        var setobj = {};
                        setobj[name] = color_str;
                        obj.set(setobj);
                    });
                    canvas.renderAll();
                    ActionBackNext.operateData();
                }
            }
        };
        // 更新填充颜色
        dcPanelBody.find("#dc_fill_color").change(function () {
            console.log("fill color change");
            var dc_active = canvas.getActiveObject();
            if (dc_active) {
                changeColor(this.value, "fill");
            }
            let svgPath = dcPanelBody.find(".dc_imgEdit_fill_label svg path");
            svgPath && svgPath.css("fill", this.value);
        });

        // 更新边框颜色
        dcPanelBody.find("#dc_stroke_color").change(function () {
            var dc_active = canvas.getActiveObject();
            if (dc_active) {
                changeColor(this.value, "stroke");
            }
            let svgPath = dcPanelBody.find(".dc_imgEdit_stroke_label svg path");
            svgPath && svgPath.css("fill", this.value);
        });

        // 更新字体颜色
        dcPanelBody.find("#dc_fontColor").change(function () {
            var dc_active = canvas.getActiveObject();
            if (dc_active) {
                changeColor(this.value, "fill");
            }
            let svgPath = dcPanelBody.find(".dc_imgEdit_fontcolor_label svg path");
            svgPath && svgPath.css("fill", this.value);
        });


        //更新矢量图
        dcPanelBody.find("#dc_back_img_color").change(function () {
            var fillImgType = this.value;
            let activeElement = canvas.getActiveObject();
            if (activeElement) {
                //当前选中了元素
                if (activeElement.type == "activeSelection" || activeElement.type == "group") {
                    //选中了一组
                    activeElement.getObjects().forEach(function (activeElementItem) {
                        //更新图片
                        refreshEditElementFill(fillImgType, activeElementItem);
                    });
                } else {
                    //更新图片
                    refreshEditElementFill(fillImgType, activeElement);
                }
            }
        });

        //更新背景图
        function refreshEditElementFill(fillImgType, activeElement) {
            if (activeElement.type == "rect" || activeElement.type == "ellipse") {
                if (fillImgType === '14') {
                    //深三度，黑色填充
                    activeElement.set({
                        opacity: 1,
                        fill: "#000000",
                        // _cacheProperties: {
                        //     _fillImgType: "14",
                        // }
                    });
                    activeElement.set('_fillImgType', "14");

                    canvas.renderAll();

                } else if (fillImgType === '0') {
                    //空白，恢复默认
                    activeElement.set({
                        fill: '#000000',
                        opacity: 0.6,
                        // _cacheProperties: {
                        //     _fillImgType: "0",
                        // }
                    });
                    activeElement.set('_fillImgType', "0");

                    canvas.renderAll();

                } else {
                    //backgrounImg
                    var fillImgSrc = getImgEditElementFill(fillImgType, activeElement);
                    if (fillImgSrc) {
                        var img = new Image();
                        img.src = fillImgSrc;
                        // 等待图像加载完成
                        img.onload = function () {
                            // 计算平均缩放比例
                            var pattern = new fabric.Pattern({
                                source: img,
                            });
                            activeElement.set({
                                fill: pattern,
                                opacity: 0.6,
                                // _cacheProperties: {
                                //     _fillImgType: fillImgType,
                                // }
                                // _fillImgType: fillImgType,
                            });

                            activeElement.set('_fillImgType', fillImgType);

                            canvas.renderAll();

                            console.log(activeElement, '==============activeElement');

                        };
                    }
                }
            }


        }
        //获取背景图片
        function getImgEditElementFill(typeNumber, canvasElement) {
            var canvasFill = ctl.ownerDocument.createElement("canvas");

            // 默认使用元素的宽高，getBoundingRect防止元素被拉伸后导致图片变形
            var canvasElementWidth = (canvasElement.getBoundingRect && canvasElement.getBoundingRect().width) || canvasElement.width;
            var canvasElementHeight = (canvasElement.getBoundingRect && canvasElement.getBoundingRect().height) || canvasElement.height;

            // var canvasElementWidth = canvasElement.width;
            // var canvasElementHeight = canvasElement.height;

            canvasFill.width = canvasElementWidth;
            canvasFill.height = canvasElementHeight;

            var ctxFill = canvasFill.getContext('2d');
            ctxFill.clearRect(0, 0, canvasFill.width, canvasFill.height);


            var imgSrc = null;
            var rectWidth = 5; // 矩形宽度
            var rectHeight = 3; // 矩形高度
            var minSpacing = 2; // 最小水平间距
            var maxSpacing = 5; // 最大水平间距
            var verticalSpacing = 3; // 垂直间距
            var x = 0;
            var y = 0;
            switch (typeNumber) {
                case "1": //触觉减退

                    while (y + rectHeight <= canvasElementHeight) {
                        ctxFill.fillStyle = 'black';
                        ctxFill.fillRect(x, y, rectWidth, rectHeight);
                        x += rectWidth + Math.floor(Math.random() * (maxSpacing - minSpacing + 1) + minSpacing);
                        if (x + rectWidth > canvasElementWidth) {
                            x = Math.floor(Math.random() * (maxSpacing - minSpacing + 1) + minSpacing);
                            y += rectHeight + verticalSpacing;
                        }
                    }


                    break;
                case "2": //触觉消失
                    rectWidth = 4; // 菱形宽度
                    rectHeight = 4; // 菱形高度
                    x = 0;
                    y = 0;
                    while (y + rectHeight <= canvas.height) {
                        ctxFill.strokeStyle = 'black';
                        ctxFill.beginPath();
                        ctxFill.moveTo(x + rectWidth / 2, y);
                        ctxFill.lineTo(x + rectWidth, y + rectHeight / 2);
                        ctxFill.lineTo(x + rectWidth / 2, y + rectHeight);
                        ctxFill.lineTo(x, y + rectHeight / 2);
                        ctxFill.closePath();
                        ctxFill.stroke();

                        x += rectWidth + 6;//6水平间距
                        if (x + rectWidth > canvasElementWidth) {
                            x = 0;
                            y += rectHeight + 4;//4垂直间距
                        }
                    }
                    break;

                case "3": //触觉过敏或异常
                    rectWidth = 4; // 菱形宽度
                    rectHeight = 4; // 菱形高度
                    x = 0;
                    y = 0;
                    while (y + rectHeight <= canvasElementHeight) {
                        ctxFill.strokeStyle = 'black';
                        ctxFill.beginPath();
                        ctxFill.moveTo(x, y);
                        ctxFill.lineTo(x + 4, y + 4);
                        ctxFill.moveTo(x + 4, y);
                        ctxFill.lineTo(x, y + 4);
                        ctxFill.closePath();
                        ctxFill.stroke();
                        x += 10;
                        if (x + rectWidth > canvasElementWidth) {
                            x = 0;
                            y += 10;
                        }
                    }
                    break;

                case "4": //痛觉减退
                    rectWidth = 6; // 菱形宽度
                    rectHeight = 5; // 菱形高度
                    x = 0;
                    y = 0;
                    while (y + rectHeight <= canvasElementHeight) {
                        ctxFill.strokeStyle = 'black';
                        ctxFill.beginPath();
                        ctxFill.moveTo(x + 6, y);
                        ctxFill.lineTo(x, y + 5);
                        ctxFill.closePath();
                        ctxFill.stroke();
                        x += 8;
                        if (x + rectWidth > canvasElementWidth) {
                            x = 0;
                            y += 8;
                        }
                    }
                    break;

                case "5": //痛觉消失
                    rectWidth = 8; // 菱形宽度
                    rectHeight = 10; // 菱形高度
                    x = 0;
                    y = 0;
                    while (y + rectHeight <= canvas.height) {
                        ctxFill.strokeStyle = 'black';
                        //修改线宽
                        ctxFill.lineWidth = 2;
                        ctxFill.beginPath();
                        ctxFill.moveTo(x + rectWidth / 2, y);
                        ctxFill.lineTo(x + rectWidth, y + rectHeight / 2);
                        ctxFill.lineTo(x + rectWidth / 2, y + rectHeight);
                        ctxFill.lineTo(x, y + rectHeight / 2);
                        ctxFill.closePath();
                        ctxFill.stroke();

                        x += rectWidth + 2;//6水平间距
                        if (x + rectWidth > canvasElementWidth) {
                            x = 0;
                            y += rectHeight + 3;//4垂直间距
                        }
                    }
                    break;
                case "6": //痛觉过敏或异常
                    while (y + rectHeight <= canvasElementHeight) {
                        ctxFill.fillStyle = 'black';
                        ctxFill.lineWidth = 4;
                        ctxFill.beginPath();
                        ctxFill.moveTo(x, y);
                        ctxFill.lineTo(x + canvasElementWidth, y);
                        ctxFill.closePath();
                        ctxFill.stroke();
                        x = 0;
                        y += 4 + 4;
                    }
                    break;
                case "7": //震动觉减退或异常
                    rectWidth = 4; // V型宽度
                    rectHeight = 6; // V型高度
                    x = 0;
                    y = 0;
                    while (y + rectHeight <= canvasElementHeight) {
                        ctxFill.strokeStyle = 'black';
                        ctxFill.beginPath();

                        // // 画倒V型
                        // ctxFill.moveTo(x + rectWidth / 2, y);
                        // ctxFill.lineTo(x + rectWidth, y + rectHeight);

                        // ctxFill.moveTo(x, y + rectHeight);
                        // ctxFill.lineTo(x + rectWidth / 2, y);

                        // 画正向V型
                        ctxFill.moveTo(x, y);
                        ctxFill.lineTo(x + rectWidth / 2, y + rectHeight);

                        ctxFill.moveTo(x + rectWidth / 2, y + rectHeight);
                        ctxFill.lineTo(x + rectWidth, y);

                        ctxFill.stroke();

                        x += 8;
                        if (x + rectWidth > canvasElementWidth) {
                            x = 0;
                            y += 8;
                        }
                    }

                    break;

                case "8": //位置觉减退或异常
                    rectWidth = 6; // 菱形宽度
                    rectHeight = 6; // 菱形高度
                    x = 0;
                    y = 0;
                    while (y + rectHeight <= canvasElementHeight) {
                        ctxFill.strokeStyle = 'black';
                        ctxFill.beginPath();

                        ctxFill.moveTo(x, y + rectHeight);
                        ctxFill.lineTo(x + rectWidth / 2, y);
                        ctxFill.lineTo(x + rectWidth, y + rectHeight);
                        ctxFill.closePath();
                        ctxFill.stroke();

                        x += 9;
                        if (x + rectWidth > canvasElementWidth) {
                            x = 0;
                            y += 9;
                        }
                    }
                    break;


                case "9": //浅感觉全部消失
                    while (x + rectWidth <= canvasElementWidth) {
                        ctxFill.fillStyle = 'black';
                        ctxFill.lineWidth = 4;
                        ctxFill.beginPath();
                        ctxFill.moveTo(x, y);
                        ctxFill.lineTo(x, y + canvasElementHeight);
                        ctxFill.closePath();
                        ctxFill.stroke();
                        x += 10;
                        y += 0;
                    }

                    // while (y + rectHeight <= canvasElementHeight) {
                    //     ctxFill.fillStyle = 'black';
                    //     ctxFill.lineWidth = 2;
                    //     ctxFill.beginPath();
                    //     ctxFill.moveTo(x, y);
                    //     ctxFill.lineTo(x + canvasElementWidth, y);

                    //     ctxFill.moveTo(x + 4, y);
                    //     ctxFill.lineTo(x + 4 + canvasElementWidth, y);

                    //     ctxFill.closePath();
                    //     ctxFill.stroke();
                    //     x = 0;
                    //     y += 4 + 6;
                    // }


                    break;
                case "10": //深浅感觉全部消失
                    // 横线
                    while (x + rectWidth <= canvasElementWidth) {
                        ctxFill.fillStyle = 'black';
                        ctxFill.lineWidth = 1;
                        ctxFill.beginPath();
                        ctxFill.moveTo(x, y);
                        ctxFill.lineTo(x, y + canvasElementHeight);
                        ctxFill.closePath();
                        ctxFill.stroke();
                        x += 10;
                        y += 0;
                    }
                    // 竖线
                    x = 0;
                    y = 0;
                    while (y + rectHeight <= canvasElementHeight) {
                        ctxFill.fillStyle = 'black';
                        ctxFill.lineWidth = 1;
                        ctxFill.beginPath();
                        ctxFill.moveTo(x, y);
                        ctxFill.lineTo(x + canvasElementWidth, y);
                        ctxFill.closePath();
                        ctxFill.stroke();
                        x = 0;
                        y += 9;
                    }


                    break;
                case "11": //一度
                    rectWidth = 2; // 矩形宽度
                    rectHeight = 4; // 矩形高度
                    minSpacing = 5; // 最小水平间距
                    maxSpacing = 10; // 最大水平间距
                    verticalSpacing = 6; // 垂直间距
                    while (y + rectHeight <= canvasElementHeight) {
                        ctxFill.fillStyle = 'black';
                        ctxFill.fillRect(x, y, rectWidth, rectHeight);
                        x += rectWidth + Math.floor(Math.random() * (maxSpacing - minSpacing + 1) + minSpacing);
                        if (x + rectWidth > canvasElementWidth) {
                            x = Math.floor(Math.random() * (maxSpacing - minSpacing + 1) + minSpacing);
                            y += rectHeight + verticalSpacing;
                        }
                    }
                    break;
                case "12": //二度
                    //画铺满矩形的线左倾斜线
                    var numLines = Math.ceil(canvasElementWidth / 2); // 计算斜线数量

                    // 画斜线
                    for (var i = 0; i < numLines; i++) {
                        var startX = canvasElementWidth / 2 + i * 8 + 32;
                        var startY = canvasElementHeight / 2 + i * 8 + 32;
                        ctxFill.beginPath();
                        ctxFill.moveTo(startX, -canvasElementHeight - 20);
                        ctxFill.lineTo(-canvasElementWidth - 20, startY);
                        ctxFill.stroke();
                    }
                    break;
                case "13": //深二度
                    //画铺满矩形的线左倾斜线
                    var numLines = Math.ceil(canvasElementWidth / 2); // 计算斜线数量
                    for (var i = 0; i < numLines; i++) {
                        // 画左斜线
                        var startXLeft = canvasElementWidth / 2 + i * 8 + 32;
                        var startYLeft = canvasElementHeight / 2 + i * 8 + 32;
                        ctxFill.beginPath();
                        ctxFill.moveTo(startXLeft, -canvasElementHeight - 20);
                        ctxFill.lineTo(-canvasElementWidth - 20, startYLeft);
                        ctxFill.stroke();
                        // 画右斜线
                        var startXRight = canvasElementWidth / 2 - i * 8 - 100;
                        var startYRight = canvasElementHeight / 2 + i * 8 - 100;
                        ctxFill.beginPath();
                        ctxFill.moveTo(startXRight, -canvasElementHeight - 20);
                        ctxFill.lineTo(canvasElementWidth + 20, startYRight);
                        ctxFill.stroke();
                    }
                    break;
                case "14":

                    break;
                default:
                    break;
            }
            imgSrc = canvasFill.toDataURL();
            canvasFill.remove();
            return imgSrc;
        }

        // ====================放大缩小STRAT==========================
        dcPanelBody.find(".zoomBtn").click(function () {
            var zoom = canvas.getZoom();
            var zoom_step = parseFloat(this.getAttribute("step"));
            zoom += zoom_step;
            SetCanvasZoom(zoom);

            // 选择放大倍数的设置为空
            dcPanelBody.find("#zoomSelect").val("");
        });
        dcPanelBody.find("#zoomSelect").change(function () {
            var zoom;
            wrap.style.overflow = "scroll";
            switch (this.value) {
                case "autowidth":
                    // 适应宽度
                    zoom = wrap.clientWidth / parseFloat(wrapperEl.getAttribute("native-width"));
                    break;
                case "autoheight":
                    // 适合页面大小
                    zoom = wrap.clientHeight / parseFloat(wrapperEl.getAttribute("native-height"));
                    break;
                default:
                    zoom = parseFloat(this.value);
                    break;
            }
            wrap.style.overflow = "auto";
            if (zoom) {
                SetCanvasZoom(zoom);
            }
        });
        // =====================放大缩小END===========================
        // ====================填充文字字体下拉START====================
        var FontFamilysArray = ctl.getSupportFontFamilys();
        if (!FontFamilysArray || FontFamilysArray.length == 0) {
            FontFamilysArray = ["宋体", "黑体", "微软雅黑", "楷体"];
        }
        var dc_fontFamily = dcPanelBody.find("#dc_fontFamily");
        for (var i = 0; i < FontFamilysArray.length; i++) {
            var option = $("<option></option>");
            option.text(FontFamilysArray[i]);
            option.val(FontFamilysArray[i]);
            option.css("font-family", FontFamilysArray[i]);
            dc_fontFamily.append(option);
        }
        dc_fontFamily.val("黑体");
        // 绑定事件
        dc_fontFamily.change(function () {
            let fontFamilyStr = this.value;
            dc_fontFamily.css("font-family", fontFamilyStr);
            let activeTxt = canvas.getActiveObject();
            if (!activeTxt || activeTxt.get("type") != "i-text") return;
            if (activeTxt.isEditing) {
                // 编辑状态
                activeTxt.setSelectionStyles({ "fontFamily": fontFamilyStr });
            } else {
                activeTxt.fontFamily = fontFamilyStr;
                let s = activeTxt.styles;
                // 遍历行和列
                for (let i in s) {
                    for (let j in s[i]) {
                        s[i][j].fontFamily = fontFamilyStr; // 针对每个字设置字号
                    }
                }
                activeTxt.dirty = true;
            }
            canvas.renderAll();
            ActionBackNext.operateData();
        });
        // =====================填充文字字体下拉END=====================
        // ====================填充文字字号下拉START====================
        // 字号大小
        var dataFontSize = [
            { "title": "大特号", "number": "63" },
            { "title": "特号", "number": "54" },
            { "title": "初号", "number": "42" },
            { "title": "小初", "number": "36" },
            { "title": "一号", "number": "26.25" },
            { "title": "小一", "number": "24" },
            { "title": "二号", "number": "21.75" },
            { "title": "小二", "number": "18" },
            { "title": "三号", "number": "15.75" },
            { "title": "小三", "number": "15" },
            { "title": "四号", "number": "14.25" },
            { "title": "小四", "number": "12" },
            { "title": "五号", "number": "10.5" },
            { "title": "小五", "number": "9" },
            { "title": "六号", "number": "7.5" },
            { "title": "小六", "number": "6.75" },
            { "title": "七号", "number": "5.25" },
            { "title": "八号", "number": "4.5" },
            { "title": "8", "number": "8" },
            { "title": "9", "number": "9" },
            { "title": "10", "number": "10" },
            { "title": "11", "number": "11" },
            { "title": "12", "number": "12" },
            { "title": "14", "number": "14" },
            { "title": "16", "number": "16" },
            { "title": "18", "number": "18" },
            { "title": "20", "number": "20" },
            { "title": "22", "number": "22" },
            { "title": "24", "number": "24" },
            { "title": "26", "number": "26" },
            { "title": "28", "number": "28" },
            { "title": "36", "number": "36" },
            { "title": "40", "number": "40" },
            { "title": "48", "number": "48" },
            { "title": "72", "number": "72" },
        ];
        var dc_fontSize = dcPanelBody.find("#dc_fontSize");
        for (var i = 0; i < dataFontSize.length; i++) {
            var option = $("<option></option>");
            option.text(dataFontSize[i].title);
            option.val(dataFontSize[i].number);
            dc_fontSize.append(option);
        }
        dc_fontSize.val(40);
        // 绑定事件
        dc_fontSize.change(function () {
            let fontSizeStr = this.value;
            let activeTxt = canvas.getActiveObject();
            if (!activeTxt || activeTxt.get("type") != "i-text") return;
            if (activeTxt.isEditing) {
                // 编辑状态
                activeTxt.setSelectionStyles({ "fontSize": fontSizeStr });
            } else {
                activeTxt.fontSize = fontSizeStr;
                let s = activeTxt.styles;
                // 遍历行和列
                for (let i in s) {
                    for (let j in s[i]) {
                        s[i][j].fontSize = fontSizeStr; // 针对每个字设置字号
                    }
                }
                activeTxt.dirty = true;
            }
            canvas.renderAll();
            ActionBackNext.operateData();
        });
        // =====================填充文字字号下拉END=====================
        // ====================更新文字颜色START====================
        dcPanelBody.find("#dc_fontColor").change(function () {
            let fontColorStr = this.value;
            let activeTxt = canvas.getActiveObject();
            if (!activeTxt || activeTxt.get("type") != "i-text") return;
            if (activeTxt.isEditing) {
                // 编辑状态
                activeTxt.setSelectionStyles({ "fill": fontColorStr });
            } else {
                activeTxt.fill = fontColorStr;
                let s = activeTxt.styles;
                // 遍历行和列
                for (let i in s) {
                    for (let j in s[i]) {
                        s[i][j].fill = fontColorStr; // 针对每个字设置字号
                    }
                }
                activeTxt.dirty = true;
            }
            canvas.renderAll();
            ActionBackNext.operateData();
        });
        // =====================更新文字颜色END=====================
        // 删除元素
        dcPanelBody.find("#dc_del").click(function () {
            var dc_active = canvas.getActiveObject();
            if (dc_active) {
                canvas.remove(dc_active);
                if (dc_active.type == "activeSelection") {
                    dc_active.getObjects().forEach(function (x) {
                        canvas.remove(x);
                    });
                    canvas.discardActiveObject().renderAll();
                }
                ActionBackNext.operateData();
            }
        });
        dcPanelBody.find(".dc_undoBtn").click(function () {
            ActionBackNext.prevStepOperate();
            canvas.remove(selectionRect);
        });
        dcPanelBody.find(".dc_redoBtn").click(function () {
            ActionBackNext.nextStepOperate();
            canvas.remove(selectionRect);
        });
        dcPanelBody.find("#dc_imgFullScreen").click(function () {
            // console.log("全屏-------------------------");
            dc_dialogContainer.css({
                width: "100%",
                height: "100%",
                left: 0,
                top: 0,
                transform: "none",
            });
            dc_dialogContainer.find("#dcPanelBody").css({
                height: "calc(100% - 72px)",
                width: "100%",
            });
            dcPanelBody.find("#dc_imgCancelFullScreen").show();
            dcPanelBody.find("#dc_imgFullScreen").hide();
        });
        dcPanelBody.find("#dc_imgCancelFullScreen").click(function () {
            // console.log("取消全屏-------------------------");
            dc_dialogContainer.css({
                width: "600px",
                height: "620px",
                top: "50%",
                left: "50%",
                transform: "translate(-50%, -50%)",
            });
            dc_dialogContainer.find("#dcPanelBody").css({
                height: "550px",
                width: "100%",
            });
            dcPanelBody.find("#dc_imgFullScreen").show();
            dcPanelBody.find("#dc_imgCancelFullScreen").hide();
        });
        function successFun() {
            // 还原画布缩放比例
            SetCanvasZoom(1);
            //获取图片编辑后的base64
            // let ctx = canvas.getContext("2d");
            // ctx.drawImage(img, 0, 0);


            //根据旋转后的比例修改容器宽高。imgOptions：原本的图片宽高。imgRotateOptions：旋转后的图片宽高。
            // 计算缩放比例
            var newoptionswidth = options.Width;// 原容器宽
            var newoptionsheight = options.Height;// 原容器高
            if (imgOptions && imgRotateOptions) {
                var scalewidth = (imgRotateOptions.width / imgOptions.width) || 1;
                var scaleheight = (imgRotateOptions.height / imgOptions.height) || 1;
                if (scaleheight && scalewidth) {
                    newoptionswidth = newoptionswidth * scalewidth;
                    newoptionsheight = newoptionsheight * scaleheight;
                }
            }

            // 转编译一定要在图像绘制完毕以后
            let base64 = canvas.toDataURL("image/jpeg", 0.1);
            // console.log(base64, '=============base64');
            let index = base64.indexOf(",");
            let Src = "";
            if (index !== -1) {
                Src = base64.slice(index + 1, base64.length);
            }

            var data = {
                ID: options.ID,
                Src,
                Height: newoptionsheight,
                Width: newoptionswidth,
                Attributes: {
                    inneradditionshape: JSON.stringify(canvas.toJSON()),
                },
            };
            ctl.SetElementProperties(options.ID || ctl.CurrentElement(), data);
            if (ctl.EventDialogChangeProperties && typeof ctl.EventDialogChangeProperties === 'function') {
                var changedOptions = ctl.GetElementProperties(ctl.CurrentElement());
                ctl.EventDialogChangeProperties(changedOptions);
            };
        }
    },

    /**
     * 创建执行编辑器命令对话框
     * @param options 执行命令属性
     * @param ctl 编辑器元素
     */
    DCExecuteCommandDialog: function (ctl) {
        var DCExecuteCommandHtml = `
        <div class="dc_DCExecuteCommandTable_container">
            <div id="dc_DCExecuteCommandTable_search_box">
                <input type="text" id="dc_DCExecuteCommandTable_search_input" placeholder="请输入命令名称">
                <button id="dc_DCExecuteCommandTable_search_btn">搜索</button>
            </div>
            <div id="dc_DCExecuteCommandTable">
                <table>
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th>说明</th>
                        </tr>
                    </thead>
                    <tbody> </tbody>
                </table>
            </div>
            
            <div class="dc_dc_DCExecuteCommand_options_box">
                <div class="dc_DCExecuteCommand_options_title">参数：</div>
                <textarea id="dc_dc_DCExecuteCommand_options"></textarea>
            </div>
        </div>
        `;
        var dialogOptions = {
            title: "执行命令对话框",
            bodyHeight: 500,
            dialogContainerBodyWidth: 550,
            bodyClass: "dc_DCExecuteCommandsElement",
            bodyHtml: DCExecuteCommandHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        var opts = {};

        //渲染表格
        renderCommandTable();

        dcPanelBody.find('#dc_DCExecuteCommandTable_search_input').focus();
        dcPanelBody.find('#dc_DCExecuteCommandTable_search_input').on('input', function () {
            searchCommand();
        });

        // dcPanelBody.find('#dc_DCExecuteCommandTable_search_input').keypress(function (event) {
        //     if (event.which === 13) {
        //         searchCommand();
        //     }
        // });
        dcPanelBody.find("#dc_DCExecuteCommandTable_search_btn").click(searchCommand);
        function searchCommand() {
            var searchInput = dcPanelBody.find("#dc_DCExecuteCommandTable_search_input");
            var searchValue = searchInput.val().trim();
            if (searchValue == "") {
                // 没有输入内容，显示所有行
                renderCommandTable();
            } else {
                let searchCommandArr = [];
                MOCKARRAY.forEach(item => {
                    let ItemName = item.name.toLowerCase();
                    let searchItem = searchValue.toLowerCase();
                    //判断字符串是否以搜索内容开头
                    if (ItemName.startsWith(searchItem)) {
                        searchCommandArr.push(item);
                    }
                });
                // 渲染查找后的数据
                renderCommandTable(searchCommandArr);
            }
        }

        function renderCommandTable(CommandArr = MOCKARRAY) {
            opts = {};
            dcPanelBody.find('.ClickLine').removeClass('ClickLine');
            var mockArray = JSON.parse(JSON.stringify(CommandArr));
            // 根据英文首字母进行排序
            mockArray.sort(function (a, b) {
                return (a.name + "").localeCompare(b.name + "");
            });

            //循环创建元素
            var tbodyHtml = "";
            for (var i = 0; i < mockArray.length; i++) {
                var TRHtml = `
            <tr commandname="${mockArray[i].name}" >
                    <td class="dc_name">${mockArray[i].name}</td>
                    <td class="dc_description">${mockArray[i].description}</td>
            </tr>
            `;
                tbodyHtml += TRHtml;
            }
            dcPanelBody.find("#dc_DCExecuteCommandTable tbody").html(tbodyHtml);

            // 所有的表格行
            var trs = dcPanelBody.find("#dc_DCExecuteCommandTable tr[commandname]");
            // tr点击事件
            trs.click(function () {
                trs.removeClass("ClickLine");
                jQuery(this).addClass("ClickLine");
                var tds = jQuery(this).find("td");
                opts = {
                    name: tds.filter(".dc_name").html(),
                    description: tds.filter(".dc_description").html(),
                };
                Object.keys(DCEXECUTECOMMANDDEFAULTOPTIONS).forEach(item => {
                    if (item.toLowerCase().trim() === opts.name.toLowerCase().trim()) {
                        if (Object.prototype.toString.call(DCEXECUTECOMMANDDEFAULTOPTIONS[item]) === '[object Object]') {
                            dc_dialogContainer.find("#dc_dc_DCExecuteCommand_options").val(JSON.stringify(DCEXECUTECOMMANDDEFAULTOPTIONS[item]));
                        } else {
                            dc_dialogContainer.find("#dc_dc_DCExecuteCommand_options").val(DCEXECUTECOMMANDDEFAULTOPTIONS[item]);
                        }
                    }
                });
            });
            // 命令对话框中tr双击事件模拟点击确定按钮
            trs.dblclick(function () {
                var dc_submitValue = dc_dialogContainer.find("#dc_submitValue");
                dc_submitValue.click();
            });
        }

        function successFun() {
            var DCExecuteCommandOptions = dc_dialogContainer.find("#dc_dc_DCExecuteCommand_options").val();
            DCExecuteCommandOptions = DCExecuteCommandOptions.trim();
            // if (DCExecuteCommandOptions && DCExecuteCommandOptions.length) {
            try {
                DCExecuteCommandOptions = JSON.parse(DCExecuteCommandOptions);
            } catch (error) {
                console.log('命令参数格式不是一个对象', DCExecuteCommandOptions);
            }
            // }
            if (opts) {
                if (opts.name === "AboutControl") {
                    //设置弹框关闭后在弹出alert关于
                    setTimeout(() => {
                        ctl.AboutControl();
                    }, 0);
                } else if (opts.name === "TextSurroundings" || opts.name === "EmbedInText") {
                    //图片环绕和嵌入文本
                    ctl.DCExecuteCommand(opts.name, false, DCExecuteCommandOptions === null ? 'true' : DCExecuteCommandOptions);
                } else if (["Table_DeleteColumn", "Table_DeleteRow", "DocumentValueValidate", 'InputFieldUnderLine', 'Insertunorderedlist', 'Table_InsertRowDown', 'Table_InsertRowUp', 'Table_MergeCell'].indexOf(opts.name) !== -1) {
                    //参数必须为null的命令
                    ctl.DCExecuteCommand(opts.name, false, null);
                } else if (opts.name === "InsertChartElement") {
                    //    插入空图表
                    var options = { 'ID': 'chart1', 'Name': 'chartName', 'Width': 1200, 'Height': 1000 };
                    ctl.DCExecuteCommand(opts.name, true, options);
                } else if (opts.name === "rowspacing" && opts.description == '设置段前距') {
                    ctl.DCExecuteCommand("rowspacing", false, DCExecuteCommandOptions + ',top');
                } else if (opts.name === "rowspacing" && opts.description == '设置段后距') {
                    ctl.DCExecuteCommand("rowspacing", false, DCExecuteCommandOptions + ',bottom');
                } else {
                    if (opts.name != null && opts.name.toLowerCase().trim() === 'fontsize') {
                        DCExecuteCommandOptions = DCExecuteCommandOptions.toString();
                    }
                    ctl.DCExecuteCommand(opts.name, true, DCExecuteCommandOptions);

                }
            }
        }
    },

    /**
     * 创建查找&替换设置对话框
     * @param options 查找替换属性
     * @param ctl 编辑器元素
     */
    SearchAndReplaceDialog: function (options, ctl) {
        // var options = {
        //     "searchstring": "",//要查找的字符串
        //     "enablereplacestring": "true",//是否启用替换
        //     "replacestring": "李四",//要替换的字符串
        //     "backward": "true",//是否向后替换
        //     "ignorecase": "true",//是否区分大小写
        //     "logundo": "true",//记录撤销操作信息
        //     "excludebackgroundtext": "true",//忽略掉背景文字
        //     "SearchID": "false"//是否限制为查询元素编号
        // }
        // console.log(options);

        var SearchAndReplaceHtml = `
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">查找内容：</span>
                    <input type="text" id="dc_searchstring" class="dc_full" data-text="searchstring">
                </label>
            </div>
            <div class="dcBody-content">
                <label class="dc_flex">
                    <span class="dcTitle-text">
                    <label>
                        <input type="checkbox" id="dc_enablereplacestring" data-text="enablereplacestring" >
                        <span>替换为：</span>
                    </label>
                    </span>
                    <input type="text" class="dc_full" id="dc_replacestring" data-text="replacestring" disabled>
                </label>
            </div>
            <div class="dcBody-contents">
                <form class="dc_Box">
                    <h6 class="dc_title">方向</h6>
                    <div class="dcBody-content">
                        <label>
                            <input type="radio" id="dc_upward" name="radios">
                            <span>向上</span>
                        </label>
                        <label>
                            <input type="radio" id="dc_backward" name="radios" checked>
                            <span>向下</span>
                        </label>
                    </div>
                </form>
                <div class="dc_checkboxs">
                    <label>
                        <input type="checkbox" id="dc_ignorecase" data-text="ignorecase" >
                        <span>不区分大小写</span>
                    </label>
                    <br>
                    <label>
                        <input type="checkbox" id="dc_excludebackgroundtext" data-text="excludebackgroundtext" checked >
                        <span>忽略输入背景文字</span>
                    </label>
                    <br>
                    <label>
                        <input type="checkbox" id="dc_SearchID" data-text="SearchID" >
                        <span>限制为查找文档元素编号</span>
                    </label>
                    <br>
                    <label>
                        <input type="checkbox" id="dc_logundo" data-text="logundo" >
                        <span>记录撤销操作信息</span>
                    </label>
                </div>
            </div>
         
        `;
        var dialogOptions = {
            title: "查找和替换",
            bodyHeight: 196,
            bodyClass: "SearchAndReplace",
            bodyHtml: SearchAndReplaceHtml,
        };
        this.pageAppendDialog(ctl, function () { }, dialogOptions);
        // DUWRITER5_0-927：像word一样查找替换的时候选择其他字
        jQuery(ctl).children("#dc_dialogMark").css({
            opacity: 0,
            "pointer-events": "none",
        });
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = dc_dialogContainer.find("#dcPanelBody");
        var dcPanelFooter = dc_dialogContainer.find("#dcPanelFooter");
        //重新设置按钮
        dcPanelFooter.html(` 
            <button id="dc_search" class="foot_btn" disabled>查找(s)</button>
            <button id="dc_replace" class="foot_btn" disabled>替换(r)</button>
            <button id="dc_ReplaceAll" class="foot_btn" disabled>全部替换</button>
            <button id="dc_cancel" class="foot_btn">取消(c)</button>
        `);
        var searchInput = dcPanelBody.find("#dc_searchstring"); //查找内容输入框
        searchInput.focus();
        var replaceCheckbox = dcPanelBody.find("#dc_enablereplacestring"); //替换复选框
        var replaceInput = dcPanelBody.find("#dc_replacestring"); //替换内容输入框
        var SearchIDBtn = dcPanelBody.find("#dc_SearchID"); //限制为查找文档元素编号复选框
        var btns = dcPanelFooter.find("button.foot_btn:not(#dc_cancel)"); //查找、替换、全部替换按钮
        // 查找内容输入框输入事件
        searchInput.on("input", function () {
            // 选择查找编号
            if (SearchIDBtn.is(":checked")) {
                // 所有不启用
                btns.prop("disabled", true);
                // 查找按钮需要用查找内容输入框是否为空来判断
                btns.filter("#dc_search").prop("disabled", jQuery(this).val() == "");
                return;
            }
            var isChecked = replaceCheckbox.is(":checked"); // 替换复选框是否选择
            if (jQuery(this).val() != "") {
                // 查找内容输入框不为空
                // 替换是否启用来确定按钮是否启用
                btns.prop("disabled", !isChecked);
                btns.filter("#dc_search").prop("disabled", false); //查找按钮一定启用
            } else {
                // 为空
                btns.prop("disabled", true);
            }
        });
        // 替换复选框切换事件
        replaceCheckbox.change(function () {
            var isChecked = jQuery(this).is(":checked"); // 替换复选框是否选择
            // 选择查找编号
            if (SearchIDBtn.is(":checked")) {
                return;
            }
            // 设置替换内容输入框是否可用
            replaceInput.prop("disabled", !isChecked);
            // 查找内容输入框不为空
            if (searchInput.val() != "") {
                // 查找内容输入框为空
                btns.filter("#dc_replace,#dc_ReplaceAll").prop("disabled", !isChecked);
            }
        });
        // 限制为查找文档元素编号复选框切换事件
        SearchIDBtn.change(function () {
            var isSearchID = SearchIDBtn.is(":checked");
            dcPanelBody
                .find(
                    "#dc_upward,#dc_backward,#dc_ignorecase,#dc_excludebackgroundtext,#dc_logundo"
                )
                .prop("disabled", isSearchID);
            if (replaceCheckbox.is(":checked")) {
                replaceInput.prop("disabled", isSearchID);
                if (searchInput.val() != "") {
                    btns
                        .filter("#dc_replace,#dc_ReplaceAll")
                        .prop("disabled", isSearchID);
                }
            }
        });
        // 按钮点击事件
        dcPanelFooter.find("button.foot_btn").click(function () {
            var _data = GetOrChangeData(dcPanelBody);
            var dc_backward = ctl.ownerDocument.getElementById('dc_backward');
            _data['backward'] = dc_backward.checked;
            console;
            var PromptJumpBackForSearch = ctl.DocumentOptions.BehaviorOptions.PromptJumpBackForSearch;
            var FindReplaceNotFoundWrinText = ctl.getAttribute('FindReplaceNotFoundWrinText');
            FindReplaceNotFoundWrinText = (FindReplaceNotFoundWrinText && FindReplaceNotFoundWrinText === 'true');
            switch (this.id) {
                case "dc_search":
                    // 查找
                    //20240102lxy:增加查找不到内容的提示DUWRITER5_0-1580
                    var result = ctl.Search(_data);
                    if (PromptJumpBackForSearch === false && result === -1 && FindReplaceNotFoundWrinText) {
                        alert(window.__DCSR.SearchReplaceNotFound);
                    }
                    break;
                case "dc_replace":
                    // 替换
                    var result = ctl.Reaplace(_data);
                    if (PromptJumpBackForSearch === false && result === -1 && FindReplaceNotFoundWrinText) {
                        alert(window.__DCSR.SearchReplaceNotFound);
                    }
                    break;
                case "dc_ReplaceAll":
                    // 全部替换
                    var result = ctl.ReplaceAll(_data);
                    if (PromptJumpBackForSearch === false && result === 0 && FindReplaceNotFoundWrinText) {
                        alert(window.__DCSR.SearchReplaceNotFound);
                    }
                    break;
                case "dc_cancel":
                    // 取消
                    jQuery(ctl).children("#dc_dialogMark,#dc_dialogContainer").remove();
                    break;
                default:
                    break;
            }
        });
    },
    /**
    * 前景色对话框
    * @param options 属性
    * @param ctl 编辑器元素
    * @param isInsertMode 是否是插入模式
    */
    ColorDialog: function (options, ctl, isInsertMode, ele) {
        var ColorHtml = `
                <div class="dc_color_box">
                    <span>请选择前景色：</span>
                    <input type="color" id='dc_color' />
                </div>
        `;
        var dialogOptions = {
            title: "设置前景色属性",
            bodyHeight: 70,
            bodyClass: "dc_ColorElement",
            bodyHtml: ColorHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        function successFun() {
            var color = dcPanelBody.find("#dc_color").val();
            ctl.DCExecuteCommand('color', false, color);
        }
    },
    /**
    * 背景景色对话框
    * @param options 属性
    * @param ctl 编辑器元素
    * @param isInsertMode 是否是插入模式
    */
    BackColorDialog: function (options, ctl, isInsertMode, ele) {
        var BackColorHtml = `
              <div class="dc_color_box">
                    <span>请选择背景色：</span>
                    <input type="color" id='dc_back_color' />
                </div>
        `;
        var dialogOptions = {
            title: "设置背景色属性",
            bodyHeight: 70,
            bodyClass: "dc_BackColorElement",
            bodyHtml: BackColorHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        function successFun() {
            var color = dcPanelBody.find("#dc_back_color").val();
            ctl.DCExecuteCommand('BackColor', false, color);
        }
    },
    /**
    * 下划线属性对话框
    * @param ctl 编辑器元素
    */
    UnderlineStyleDialog: function (options, ctl, isInsertMode, ele) {
        var UnderlineStyleHtml = `
            <div class="dc_underline_box">
                <div class="dc_underline_color_box">
                    <span>下划线颜色：</span>
                    <input type="color" id='dc_underline_color' />
                </div>
                <div class="dc_underline_style_box">
                    <span>下划线样式：</span>
                    <select id="dc_underline_style">
                        <option value="None">None</option>
                        <option value="Single">Single</option>
                        <option value="Thick">Thick</option>
                        <option value="Dash">Dash</option>
                        <option value="Dot">Dot</option>
                        <option value="DashDot">DashDot</option>
                        <option value="DashDotDot">DashDotDot</option>
                        <option value="Double">Double</option>
                        <option value="Wavy">Wavy</option>
                        <option value="WavyDouble">WavyDouble</option>
                        <option value="WavyHeavy">WavyHeavy</option>
                    </select>
                </div>
            </div>
        `;
        var dialogOptions = {
            title: "设置下划线样式",
            bodyClass: "dc_UnderlineElement",
            bodyHeight: 100,
            dialogContainerBodyWidth: 200,
            bodyHtml: UnderlineStyleHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        if (options && Object.keys(options).length) {
            dcPanelBody.find("#dc_underline_color").val(options.color);
            dcPanelBody.find("#dc_underline_style").val(options.textunderlinestyle);
        }
        function successFun() {
            var color = dcPanelBody.find("#dc_underline_color").val();
            var textunderlinestyle = dcPanelBody.find("#dc_underline_style").val();
            ctl.DCExecuteCommand('UnderlineStyle', false, {
                color,
                textunderlinestyle
            });
        }
    },
    //查看错误信息对话框
    ReportExceptionDialog: function (ctl) {
        var ReportExceptionHtml = `
        <div class="dc_ReportExceptions_Box">
            <div id="dc_ReportExceptionRefreshBox">
                <span class="dc_ReportExceptionRefresh" >刷新</span>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>序号</th>
                        <th>来源名称</th>
                        <th>异常信息</th>
                         <th>错误等级</th>

                    </tr>
                </thead>
                <tbody id="dc_ReportExceptionBox"> </tbody>
            </table>
            <div id="dc_ReportExceptionErrorBox" >
                <div id="dc_ReportExceptionItemTopBox"><span id="dc_ReportExceptionItemTop">✖</span></div>
                <pre id="dc_ReportExceptionItem"></pre>
            </div>
        </div>
        `;
        var dialogOptions = {
            title: "查看错误信息对话框",
            bodyHeight: 325,
            dialogContainerBodyWidth: 550,
            bodyClass: "dc_ReportExceptionsElement",
            bodyHtml: ReportExceptionHtml,
        };
        this.pageAppendDialog(ctl, successFun, dialogOptions);
        //获取对话框元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer");
        var dcPanelBody = jQuery(dc_dialogContainer).find("#dcPanelBody");
        function renderReportException() {
            if (
                window &&
                window.ReportExceptionArr &&
                window.ReportExceptionArr.length
            ) {
                let arr = window.ReportExceptionArr;
                let arrHTML = ``;
                for (var i = 0; i < arr.length; i++) {
                    let item = arr[i];
                    arrHTML += `
                    <tr class="dc_ReportExceptionsTr">
                        <td>${i + 1}</td>
                        <td><span class="dc_ReportExceptionsTd" title="${item.strSourceName
                        }">${item.strSourceName}</span></td>
                        <td><span id="${i}" class="dc_ReportExceptionsTd dc_ReportExceptionsTd3 dc_ReportExceptionItemButton" title="${item.strExceptionMessage
                        }">${item.strExceptionMessage}</span></td>
                        <td><span class="dc_ReportExceptionsTd dc_ReportExceptionsTd2" title="${item.intLevel
                        }">${item.intLevel}</span></td>
                        </tr>
                    `;
                }
                jQuery(dc_dialogContainer).find("#dc_ReportExceptionBox")[0].innerHTML =
                    arrHTML;
            }
        }
        renderReportException();
        jQuery(dc_dialogContainer)
            .find("#dc_ReportExceptionRefreshBox")
            .click(function (e) {
                if (e.target && e.target.className === "dc_ReportExceptionRefresh") {
                    renderReportException();
                }
            });
        jQuery(dc_dialogContainer)
            .find("#dc_ReportExceptionBox")
            .click(function (e) {
                if (
                    e.target &&
                    e.target.className.indexOf("dc_ReportExceptionItemButton") !== -1
                ) {
                    jQuery(dc_dialogContainer).find(
                        "#dc_ReportExceptionErrorBox"
                    )[0].style.display = "block";
                    jQuery(dc_dialogContainer).find(
                        "#dc_ReportExceptionItem"
                    )[0].innerHTML =
                        window.ReportExceptionArr[e.target.id].strSourceName +
                        `<br />` +
                        window.ReportExceptionArr[e.target.id].strExceptionMessage +
                        `<br />` +
                        window.ReportExceptionArr[e.target.id].strExceptionString;
                }
            });
        jQuery(dc_dialogContainer)
            .find("#dc_ReportExceptionErrorBox")
            .click(function (e) {
                if (e.target && e.target.id === "dc_ReportExceptionItemTop") {
                    jQuery(dc_dialogContainer).find(
                        "#dc_ReportExceptionErrorBox"
                    )[0].style.display = "none";
                }
            });

        function successFun() { }
    },

    //自定义属性公共组件
    attributeComponents: function (parentId = "", attributes = {}, ctl) {
        //DUWRITER5_0-3015，兼容四代数组的方式
        if (Array.isArray(attributes)) {
            let dc_arrayListattributes = JSON.parse(JSON.stringify(attributes));
            attributes = {};
            dc_arrayListattributes.forEach((item) => {
                attributes[item.Name] = item.Value;
            });
        }
        jQuery(ctl).find("#dc_attr-table-box").on('keydown');
        let dataList = [];
        // 对象转换为数组
        if (
            attributes &&
            Object.keys(attributes) &&
            Object.keys(attributes).length
        ) {
            let keys = Object.keys(attributes);
            keys.map((item) => {
                dataList.push({
                    key: item,
                    val: attributes[item],
                });
            });
        }
        let attrHtml = `
        <div class="dc_attribute_dialog_box" id="${parentId}"  >
          <p class="dc_attribute_dialog_title" > 
            <span id="dc_addButton" >添加</span>
            <span id="dc_deletButton" >清空</span>
          </p>  
        <div >
            <table id="dc_attr-table-box" class="dc_currentTableDom"  border="1">
                <tr>
                    <th class="dc_ons-2">名称</th>
                    <th class="dc_ons-3">值</th>
                    <th class="dc_ons-4">操作</th>
                </tr>
                <tr class="dc_tr_abs" >
                    <td class="dc_ons-2 dc_input-dom"><input class="dc_ons-2" type="text" data-arraytext="Text" ></input></td>
                    <td class="dc_ons-3 dc_input-dom"><input class="dc_ons-3" type="text" data-arraytext="Text" ></input></td>
                    <td class="dc_ons-4 dc_delete-button">删除</td>
                </tr>
            </table>
        </div>
        </div>`;
        jQuery(ctl).find("#dc_attr-box").html(attrHtml).css("background", "#FAFAFA");

        if (dataList && dataList.length) {
            var CDC = jQuery(ctl).find(".dc_tr_abs")[0];
            for (var i = 0; i < dataList.length; i++) {
                var tr = ctl.ownerDocument.createElement("tr");
                var trKey = dataList[i].key && dataList[i].key.replace ? dataList[i].key.replace(/"/g, '&quot;').replace(/'/g, '&apos;') : dataList[i].key;
                var trVal = dataList[i].val && dataList[i].val.replace ? dataList[i].val.replace(/"/g, '&quot;').replace(/'/g, '&apos;') : dataList[i].val;
                tr.className = "dc_tr_abs";
                tr.innerHTML = `
                    <td class="dc_ons-2"><input class="dc_ons-2 dc_input-dom" type="text" data-arraytext="Text" value=${trKey}></input></td>
                    <td class="dc_ons-3"><input class="dc_ons-3 dc_input-dom" type="text" data-arraytext="Text" value=${trVal}></input></td>
                    <td class="dc_ons-4 dc_delete-button" >删除</td>`;
                CDC.before(tr);
            }
        }
        jQuery(ctl).find("#dc_deletButton").click(function () {
            var CDC = jQuery(ctl).find(".dc_tr_abs");
            for (let i = 0; i < CDC.length; i++) {
                CDC[i].remove();
            }
            jQuery(ctl).find("#dc_attr-table-box").append(tr_template());
        });
        jQuery(ctl).find("#dc_addButton").click(function () {
            let trArr = jQuery(ctl).find(".dc_tr_abs");
            let last = trArr[trArr.length - 1];
            last.after(tr_template());
        });
        jQuery(ctl).find("#dc_attr-table-box").on("input", "input", function () {
            var input = jQuery(this);
            var tr = input.parents("tr");
            if (tr.nextAll("tr").length == 0) {
                tr.after(tr_template());
            }
        });
        jQuery(ctl).find("#dc_attr-table-box").on("click", "td", function () {
            let trArr = jQuery(ctl).find(".dc_tr_abs");
            if (this.className.includes("dc_delete-button")) {
                if (trArr.length <= 1) {
                    return alert(window.__DCSR.InputAttributeTableDelete);
                }
                this.parentNode.remove();
            }
        });
        //定义表格行公用模板
        var tr_template = function () {
            let templateHtml = `
            <td class="dc_ons-2"><input class="dc_ons-2 dc_input-dom" type="text" data-arraytext="Text" value=""></input></td>
            <td class="dc_ons-3"><input class="dc_ons-3 dc_input-dom" type="text" data-arraytext="Text" value=""></input></td>
            <td class="dc_ons-4 dc_delete-button" >删除</td>`;
            var newtr = ctl.ownerDocument.createElement("tr");
            newtr.className = "dc_tr_abs";
            newtr.innerHTML = templateHtml;
            return newtr;
        };
        // 按下键盘上下左右，控制光标跳转单元格
        function AttributeCellInputFocus(event) {
            //键盘监听上下左右键，根据按键跳转单元格输入框
            let cells = jQuery(ctl).find(`${parentId} input`);
            let currentCell = ctl.ownerDocument.activeElement; // 获取当前拥有焦点的单元格
            let index = 0;//默认当前input的下标
            let targetCell = null;//目标input
            if (event.key === 'ArrowUp') {
                // 切换到上方的单元格
                index = Array.from(cells).indexOf(currentCell);
                targetCell = cells[index - 2]; // 每行有2个input
            } else if (event.key === 'ArrowDown') {
                // 切换到下方的单元格
                index = Array.from(cells).indexOf(currentCell);
                targetCell = cells[index + 2]; // 每行有2个input
            } else if (event.key === 'ArrowLeft') {
                // 判断光标位置
                if (currentCell.selectionStart === 0 && currentCell.selectionEnd === 0) {
                    // 切换到左边的单元格
                    index = Array.from(cells).indexOf(currentCell);
                    targetCell = cells[index - 1];
                }
            } else if (event.key === 'ArrowRight') {
                // 判断光标位置
                if (currentCell.selectionStart === currentCell.value.length && currentCell.selectionEnd === currentCell.value.length) {
                    // 切换到右边的单元格
                    index = Array.from(cells).indexOf(currentCell);
                    targetCell = cells[index + 1];
                }
            }
            if (targetCell) {
                targetCell.setSelectionRange(targetCell.value.length, targetCell.value.length);
                setTimeout(() => {
                    targetCell.focus();
                });
            }
        }
        // 监听键盘按下事件
        jQuery(ctl).find("#dc_attr-table-box").on('keydown', AttributeCellInputFocus);
    },

    //获取自定义属性
    attributeComponents_getAttributeObj: function (parentDom) {
        let tableBox = jQuery(parentDom).find("table")[0];
        let trList = jQuery(tableBox).find(".dc_tr_abs");
        let attributes = {};
        for (var i = 0; i < trList.length; i++) {
            let key = trList[i].children[0].children[0].value;
            let val = trList[i].children[1].children[0].value;
            if (key !== "" || val !== "") {
                attributes = {
                    ...attributes,
                    [key]: val,
                };
            }
        }
        return attributes;
    },

    /**
     * 取消浏览器默认事件
     * @param eventObject 事件Event
     */
    completeEvent: function (eventObject) {
        if (eventObject == null) {
            if (window.event) {
                eventObject = window.event;
            }
        }
        if (eventObject != null) {
            eventObject.cancelBubble = true;
            if (eventObject.stopPropagation) {
                eventObject.stopPropagation();
            }
            if (eventObject.stopImmediatePropagation) {
                eventObject.stopImmediatePropagation();
            }
            if (eventObject.preventDefault) {
                eventObject.preventDefault();
            }
            eventObject.returnValue = false;
        }
    },

    /**
     * 创建的对话框方法
     * @param ctl 编辑器元素
     * @param successFun 确定按钮触发事件
     * @param options 对话框一些设置
     */
    pageAppendDialog: function (ctl, successFun, options, callBack) {
        // 查看用户是否设置了对话框只读
        var IsDialogReadOnly = ctl.getAttribute('IsDialogReadOnly');

        //创建包裹html
        var containerInnerHtml = `<div id="dc_dialogMark"></div><div id="dc_dialogContainer">
        <div id="dcPanelHeader">
            <div class="dcHeader-title"></div>
            <div class="dcHeader-tool">
                <b class="dcTool-close">✖</b>
            </div>
        </div>
        <div id="dcPanelBody">
            
        </div>
        <div id="dcPanelFooter">
                <span class="dcButton-left dclinkbutton ${IsDialogReadOnly === 'true' || IsDialogReadOnly === true ? 'dc_submitValue_Readonly' : ''}"   title="${IsDialogReadOnly === 'true' || IsDialogReadOnly === true ? '只读模式下禁止修改' : ''}" id="dc_submitValue">
                    <span class="dcButton-text">确认</span>
                </span>
            <span class="dc_gap-width"></span>
                <span class="dcButton-left dclinkbutton" id="dc_removeDialog">
                    <span class="dcButton-text">取消</span>
                </span>
        </div>
    </div>`;
        // 创建对话框时清空编辑器的title【DUWRITER5_0-1189】
        ctl.title = "";
        //创建css样式
        var docHead = ctl.ownerDocument.head;
        var dialogLink = docHead.querySelector("style#dialogStyle");
        if (!dialogLink) {
            // 防止多次插入样式元素
            dialogLink = ctl.ownerDocument.createElement("style");
            dialogLink.setAttribute("id", "dialogStyle");
            dialogLink.innerHTML = DIALOGSTYLE;
            docHead.appendChild(dialogLink);
        }
        //确保页面中只有一个对话框元素
        jQuery(ctl).children("#dc_dialogMark").remove();
        jQuery(ctl).children("#dc_dialogContainer").remove();
        //判断右键菜单是否展示
        var hasContextMenu = ctl.querySelector('#dcContextMenu');
        if (hasContextMenu) {
            hasContextMenu.remove();
        }
        //页面中插入对话框
        jQuery(ctl).append(containerInnerHtml);
        // 弹出对话框的同时取消光标和下拉
        var dropdown = ctl.querySelector("#divDropdownContainer20230111");
        var caret = ctl.querySelector("#divCaret20221213");
        if (dropdown != null) {
            dropdown.CloseDropdown();
        }
        //关闭表格下拉输入域
        var dropdownTable = ctl.querySelector(`#DCTableControl20240625151400`);
        if (dropdownTable && dropdownTable.CloseDropdownTable) {
            dropdownTable.CloseDropdownTable();
        }
        //关闭知识库
        var divknowledgeBase = ctl.querySelector('#divknowledgeBase20240103');
        if (divknowledgeBase && divknowledgeBase.DistroyKnowledgeBase) {
            divknowledgeBase.DistroyKnowledgeBase();
        }
        if (caret != null) {
            caret.style.display = "none";
            clearInterval(caret.handleTimer);
        }
        var dc_dialogMark = jQuery(ctl).children("#dc_dialogMark"); //蒙版元素
        var dc_dialogContainer = jQuery(ctl).children("#dc_dialogContainer"); //对话框元素
        var dcPanelBody = dc_dialogContainer.find("#dcPanelBody"); //对话框正文元素
        // 对话框属性设置
        if (options && typeof options == "object") {
            // 修改标题数据
            if (options.title) {
                dc_dialogContainer.find(".dcHeader-title").html(options.title);
            }
            // 修改对话框正文元素高度
            if (options.bodyHeight) {
                dcPanelBody.height(options.bodyHeight);
            }
            // 修改对话框元素宽度
            if (options.dialogContainerBodyWidth) {
                dc_dialogContainer.width(options.dialogContainerBodyWidth);
            }
            // 对话框正文元素添加class名称
            if (options.bodyClass) {
                dcPanelBody.addClass(options.bodyClass);
            }
            // 给对话框正文元素赋值
            if (options.bodyHtml) {
                dcPanelBody.html(options.bodyHtml).promise().done(function () {
                    var dcDataModel = dcPanelBody.find('.dc_input_number_data_model');
                    if (dcDataModel && dcDataModel.length) {
                        setTimeout(() => {//展示计算结果
                            for (var i = 0; i < dcDataModel.length; i++) {
                                var item = dcDataModel[i];
                                updateDataModelValue(item);//延时计算厘米值
                            }
                        }, 0);
                    }
                });;
            }
        }
        //遮罩层取消掉鼠标事件和键盘事件
        dc_dialogMark.on("mousedown click keydown", function (e) {
            // this.completeEvent(e);
        });
        dc_dialogContainer.on("mousedown click keydown", function (e) {
            e.stopPropagation();
        });
        // 暂时处理在对话框中输入框中会取消光标
        dc_dialogContainer.on("focus", "input", function () {
            var caret = ctl.querySelector("#divCaret20221213");
            if (caret != null) {
                caret.style.display = "none";
                clearInterval(caret.handleTimer);
            }
        });
        //点击x图标的事件
        var closeIcon = dc_dialogContainer.find(".dcTool-close");
        closeIcon.on("click", function () {
            dc_dialogMark.remove();
            dc_dialogContainer.remove();
        });
        //点击确认的事件
        var dc_submitValue = dc_dialogContainer.find("#dc_submitValue");
        dc_submitValue.on("click", function () {
            if (IsDialogReadOnly === 'true' || IsDialogReadOnly === true) {
                return;
            }
            !!successFun && typeof successFun == "function" && successFun();
            dc_dialogMark.remove();
            dc_dialogContainer.remove();
            if (callBack) {
                typeof callBack == "function" ? callBack() : null;
            }
        });
        //点击取消的事件
        var dc_removeDialog = dc_dialogContainer.find("#dc_removeDialog");
        dc_removeDialog.on("click", function () {
            dc_dialogMark.remove();
            dc_dialogContainer.remove();
            if (options.bodyClass === 'bordersShading') {
                callBack && callBack();
            }
        });

        //弹框拖拽
        let drag = ctl.ownerDocument.getElementById("dc_dialogContainer");
        let dragContent = ctl.ownerDocument.getElementById("dcPanelHeader");
        let dialogBox = ctl.ownerDocument.getElementById("dc_dialogMark");
        let SearchAndReplaceDialog = (options && options.bodyClass && options.bodyClass === 'SearchAndReplace');
        var isDragging = false;
        var offsetX = 0;
        var offsetY = 0;
        dragContent.addEventListener("mousedown", function (e) {
            //[DUWRITER5_0-3923] 增加判断，编辑器宽高小于弹框宽高时，才允许拖拽
            var dragRect = drag.getBoundingClientRect();//对护框的大小
            var ctlRect = ctl.getBoundingClientRect();//编辑器的大小
            if (ctlRect.width > dragRect.width || ctlRect.height > dragRect.height) {
                isDragging = true;
                offsetX = e.clientX - drag.offsetLeft;
                offsetY = e.clientY - drag.offsetTop;
            }
            SearchAndReplaceDialog ? dialogBox.style["pointer-events"] = "auto" : null; //在查找与替换时设置遮罩层不允许穿透
        });
        ctl.ownerDocument.addEventListener("mousemove", function (e) {
            if (isDragging) {
                var posX = e.clientX - offsetX;
                var posY = e.clientY - offsetY;
                // 限制有问题暂时不用
                posX = Math.max(
                    drag.offsetWidth / 2,
                    Math.min(posX, window.innerWidth - drag.offsetWidth / 2)
                );
                posY = Math.max(
                    0,
                    Math.min(posY, window.innerHeight - drag.offsetHeight / 2)
                );
                //重新设置禁止拖拽至可视区域之外
                if (posY < drag.offsetHeight / 2) {
                    posY = drag.offsetHeight / 2;
                }
                drag.style.left = posX + "px";
                drag.style.top = posY + "px";
            }
        });
        ctl.ownerDocument.addEventListener("mouseup", function () {
            isDragging = false;
            SearchAndReplaceDialog ? dialogBox.style["pointer-events"] = "none" : null; //查找弹框时设置遮罩可穿透属性
        });

        let that = this;
        // 可见性表达式鼠标点击展示说明内容
        var dcvisibleexpression = jQuery(ctl).find('.dc_visible_expression');
        if (dcvisibleexpression && dcvisibleexpression.length) {
            for (var i = 0; i < dcvisibleexpression.length; i++) {
                var item = dcvisibleexpression[i];
                item.addEventListener('click', function () {
                    that.visibleexpressionDialog(ctl);
                });
            }
        }

        dialogBox.addEventListener('mouseover', function (event) {
            // 鼠标滑过元素时要执行的操作
            // 在这里可以添加你的代码逻辑
            // 例如，可以修改元素的样式或执行其他操作
            event.stopPropagation(); // 阻止事件冒泡到其他元素
        });

        //监听所有需要转换显示1/300英寸为厘米的输入框
        var dcDataModel = dcPanelBody.find('.dc_input_number_data_model');
        dcDataModel.on('input', function () {
            if (this.value && this.value > 0) {
                updateDataModelValue(this);
            }
        });

        // 计算预估值并更新显示
        function updateDataModelValue(inputElement) {
            var key = inputElement.getAttribute("data-text");
            var datamodelDom = dcPanelBody.find(`[dc-text-model=${key}]`)[0] || null;
            if (datamodelDom) {

                datamodelDom.style.color = "red";
                datamodelDom.style.marginLeft = "2px";

                var value = parseFloat(inputElement.value);
                if (!isNaN(value) && value > 0) {
                    // 计算预估值
                    var datamodelValua = ((value / 300) * 2.54).toFixed(2);
                    datamodelDom.innerText = `≈${datamodelValua}厘米`;
                } else {
                    datamodelDom.innerText = '';
                }
            }
        }






    },
    /**
        * 可见性表达式的提示对话框
        * @param ctl 编辑器元素
        */
    visibleexpressionDialog: function (ctl) {
        var visibleexpressionHtml = `
        <div class="dc_visibleexpression_header">
            <span>表达式介绍</span>
            <div class="dcHeader-tool">
                    <b class="dcTool-close dc_visible_expression_table_close">✖</b>
                </div>
        </div>
        <div class="dc_visibleexpression_container">

          <table class="dc_visibleexpression_table_content" cellpadding="0" cellspacing="0" width="100%">
            <thead>
                <tr class="dc_visibleexpression_header_td">
                    <td>函数</td>
                    <td>解释说明</td>
                    <td>用法</td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>ABS(V)</td>
                    <td>获得绝对值</td>
                    <td>ABS(V)</td>
                </tr>
                <tr>
                    <td>ACOS(V) </td>
                    <td>计算反余弦值。</td>
                    <td>ACOS(V) </td>
                </tr>
                <tr>
                    <td>ASIN(V)</td>
                    <td>计算反正弦值。</td>
                    <td>ASIN(V)</td>
                </tr>
                <tr>
                    <td>ATAN(V)</td>
                    <td>计算反正切值。</td>
                    <td>ATAN(V)</td>
                </tr>
                <tr>
                    <td>ATAN2(X,Y)</td>
                    <td>计算反正切值。</td>
                    <td>ATAN2(X,Y)</td>
                </tr>
                <tr>
                    <td>AVERAGE(X1，X2...) </td>
                    <td>计算算术平均值。</td>
                    <td>示例：输入域数值表达式：AVERAGE([field1],[field2])--代表当前输入域值为输入域ID为field1和ID为field2之和的平均值。（10、20=15）</td>
                </tr>
                <tr>
                    <td>CDOUBLE(V,DefaultValue) </td>
                    <td>将指定数据转换为浮点数，第二个参数为转换失败后的返回的默认值。</span> </td>
                    <td>CDOUBLE(V,DefaultValue) </td>
                </tr>
                <tr>
                    <td>CEILING(V) </td>
                    <td>获得大于等于指定数值的最小整数。</td>
                    <td>CEILING(V) </td>
                </tr>
                <tr>
                    <td>CINT(V,DefaultValue) </td>
                    <td>将指定数据转换为整数，第二个参数为转换失败后返回的默认值。</span> </td>
                    <td>CINT(V,DefaultValue) </td>
                </tr>
                <tr>
                    <td>COS(V)</td>
                    <td>返回指定角度的余弦值。 </td>
                    <td>COS(V)</td>
                </tr>
                <tr>
                    <td>COUNT(X1,X2,...)</td>
                    <td>返回参数的个数 </td>
                    <td>示例：输入域数值表达式：COUNT([field1],[field3])--代表当前输入域值为该函数里面的个数（10,11,12=3）</td>
                </tr>
                <tr>
                    <td>EXP(V)</td>
                    <td>返回e的n次方。</td>
                    <td>EXP(V)</td>
                </tr>
                <tr>
                    <td>FLOOR(V)</td>
                    <td>返回小于等于指定数字的整数。</td>
                    <td>FLOOR(V)</td>
                </tr>
                <tr>
                    <td>INT(V)</td>
                    <td>四舍五入的数字取整。 </td>
                    <td>示例：输入域数值表达式：INT([field1])--代表当前输入域值为输入域ID-field1的整数值(12.4=12)</td>
                </tr>
                <tr>
                    <td>LOG(A,BASE) </td>
                    <td>返回指定底数的对数值。 </td>
                    <td>LOG(A,BASE) </td>
                </tr>
                <tr>
                    <td>LOG(V)</td>
                    <td>返回以10为底数的对数值。 </td>
                    <td>LOG(V)</td>
                </tr>
                <tr>
                    <td>MAX(V1，V2...) </td>
                    <td>返回最大值。</td>
                    <td>示例：输入域数值表达式：MAX([field1],[field2])--代表当前输入域值为输入域ID为field1、ID为field2值的最大值（20、40=40）
                    </td>
                </tr>
                <tr>
                    <td>MIN(V1，V2...) </td>
                    <td>返回最小值。</td>
                    <td>示例：输入域数值表达式：MIN([field1],[field2])--代表当前输入域值为输入域ID为field1、ID为field2值的最小值(20、40=20)
                    </td>
                </tr>
                <tr>
                    <td>MOD(V,DIVISOR)</td>
                    <td>返回两数相除的余数。 </td>
                    <td>示例：输入域数值表达式：MOD([field1],[field2])--代表当前输入域值为输入域ID为field1和ID为field2相除的余数（20/6=2）
                    </td>
                </tr>
                <tr>
                    <td>ODD(V)</td>
                    <td>将正（负）数向上（下）舍入到最接近的奇数。</span> </td>
                    <td>ODD(V)</td>
                </tr>
                <tr>
                    <td>POW(NUMER,POWER)</td>
                    <td>返回某数的乘幂。</td>
                    <td>示例：输入域数值表达式：POW([field1],[field2])--代表当前输入域值为输入域ID为field1的乘幂、ID为field2的输入域值为底数（6^</span><sup
                           class="dc_visible_expression_POW_table_sup">2</sup>=64</span>）</td>
                </tr>
                <tr>
                    <td>PRODUCT(V1,V2,V3...) </td>
                    <td>返回所有参数的乘积。 </td>
                    <td>示例：输入域数值表达式：PRODUCT([field1],[field2])--代表当前输入域值为输入域ID为field1、输入域ID为field2的乘积。（3*4=12）
                    </td>
                </tr>
                <tr>
                    <td>RADIANS(V) </td>
                    <td>将角度转换为弧度。 </td>
                    <td>RADIANS(V) </td>
                </tr>
                <tr>
                    <td>RAND()</td>
                    <td>返回一个介于0到1之间的随机数。</td>
                    <td>RAND()</td>
                </tr>
                <tr>
                    <td>ROUND(V)</td>
                    <td>进行四舍五入计算。 </td>
                    <td>示例：输入域数值表达式：ROUND([field1])--代表当前输入域值为输入域ID为field1进行四舍五入。（11.3=11）（11.5=12）
                    </td>
                </tr>
                <tr>
                    <td>ROUNDDOWN(V) </td>
                    <td>向下舍入数字。</td>
                    <td>示例：输入域数值表达式：ROUNDDOWN([field1])--代表当前输入域值为输入域ID为field1的值小数位去掉（11.3=11）（11.899=11）
                    </td>
                </tr>
                <tr>
                    <td>ROUNDUP(V) </td>
                    <td>向上舍入数字。</td>
                    <td>示例：输入域数值表达式：ROUNDDOWN([field1])--代表当前输入域值为输入域ID为field1的值小数位向上加一（11.011=12）（11.899=12）
                    </td>
                </tr>
                <tr>
                    <td>SIGN(V)</td>
                    <td>为正数返回1，为零返回0，为负数返回-1。</span> </td>
                    <td>示例：输入域数值表达式：ROUNDDOWN([field1])--代表当前输入域值为输入域ID为field1的值小数位向上加一（11.011=1）（0=0）(-12=-1)
                    </td>
                </tr>
                <tr>
                    <td>SIN(V)</td>
                    <td>返回指定角度的正弦值。 </td>
                    <td>SIN(V)</td>
                </tr>
                <tr>
                    <td>SQRT(V)</td>
                    <td>返回数值的平方根。 </td>
                    <td>示例：输入域数值表达式：ROUNDDOWN([field1])--代表当前输入域值为输入域ID为field1的平方根（</span><span dcopi="2"
                        class="dc_visible_expression_SQRT_table">√￣</span>4=2）
                    </td>
                </tr>
                <tr>
                    <td>SUM(V1,V2...) </td>
                    <td>返回所有参数的和。 </td>
                    <td>示例：输入域数值表达式：POW([field1],[field2])--代表当前输入域值为输入域ID为field1、ID为field2的输入域值之和（1+2=3）<br
                            dcpf="1" />单元格数值表达式：SUM([C1:C3])--代表单元格背景编号C1-C3值之和</td>
                </tr>
                <tr>
                    <td>TAN(V)</td>
                    <td>返回指定角度的正切值。 </td>
                    <td>TAN(V)</td>
                </tr>
                <tr>
                    <td>PARAMETER </td>
                    <td></td>
                    <td>PARAMETER </td>
                </tr>
                <tr>
                    <td>CINT</td>
                    <td>将数据转换为一个整数 </td>
                    <td>CINT</td>
                </tr>
                <tr>
                    <td>CDOUBLE</td>
                    <td>将数据转换为一个双精度浮点数</td>
                    <td>CDOUBLE</td>
                </tr>
                <tr>
                    <td>LEN</td>
                    <td></td>
                    <td>LEN</td>
                </tr>
                <tr>
                    <td>FIND</td>
                    <td>函数FIND和FINDB用于在第二个文本串中定位第一个文本串，并返回第一个文本串的</span>
                    </td>
                    <td>示例：输入域可见性表达式：FIND('男',[field1])&gt;=0--代表当前输入域值为多选下拉输入域ID为field1值包含男，该输入域就显示，否则隐藏。
                    </td>
                </tr>
                <tr>
                    <td>LOOKUP</td>
                    <td>进行数组比较，返回比较结果。</td>
                    <td>示例：输入域数值表达式：LOOKUP([field1],0,'不及格',60,'及格',70,'中',80,'良',90,'优')--代表当前输入域值为输入域ID为field1值所属某个区间的值（45=不及格）（81=良）
                    </td>
                </tr>
                <tr>
                    <td>IF</td>
                    <td>对一个参数值转换为布尔值，如果为true则返回第二个参数值，如果为false则返回第三个参数值。</td>
                    <td>示例：输入域可见性表达式：if([field1]='',true,false)--代表输入域ID为field1值为空时，当前输入域显示，不为空的时候，当前输入域隐藏。示例：输入域数值表达式if([field1]='',0,2)--代表当前输入域值为输入域ID为field1值为空时显示0，否则显示2
                    </td>
                </tr>
                <tr>
                    <td>AGE(V)</td>
                    <td>计算周岁年龄。参数为表示时间日期数值。</td>
                    <td>示例：输入域数值表达式AGE([field1])--代表当前输入域值为输入域id为field1日期值的周岁（20201122=3）
                    </td>
                </tr>
                <tr>
                    <td>AGEMONTH(V) </td>
                    <td>计算月龄。参数为表示时间日期的数值。</td>
                    <td>示例：输入域数值表达式AGEMONTH([field1])--代表当前输入域值为输入域id为field1日期值的月龄（2020-12-31=34）
                    </td>
                </tr>
                <tr>
                    <td>AGEWEEK(V) </td>
                    <td>计算周龄。参数为表示时间日期的数值。</td>
                    <td>示例：输入域数值表达式AGEWEEK([field1])--代表当前输入域值为输入域id为field1日期值的月龄（2020-11-01=157）
                    </td>
                </tr>
                <tr>
                    <td>AGEHOUR(V) </td>
                    <td>计算小时龄。参数为表示时间日期的数值。</td>
                    <td>示例：输入域数值表达式AGEHOUR([field1])--代表当前输入域值为输入域id为field1日期值的月龄（2020-11-30=25724）
                    </td>
                </tr>
                <tr>
                    <td>SUNINNERVALUE(V1,V2) </td>
                    <td>返回所有参数的INNERVALUE之和</td>
                    <td>示例：输入域数值表达式SUNINNERVALUE([aa])--代表当前输入域值为复选框组name值为aa的勾选之和。</td>
                </tr>
                <tr>
                    <td>性别-月经史级联</td>
                    <td></td>
                    <td>示例：输入域可见性表达式[field1]='男'--当输入域id为男的时候，该输入域显示，否则隐藏</td>
                </tr>
            </tbody>
        </table>
        </div>
        <div class="dc_visible_expression_table_footer">
                <span class="dc_visible_expression_table_close">关闭</span>
        </div>
        `;
        var dc_visible_expression_table = jQuery(ctl).find('#dc_visible_expression_table');
        //判断是否已经有表达式关于弹框
        if (dc_visible_expression_table.length === 0) {
            var visibleexpression = ctl.ownerDocument.createElement('div');
            visibleexpression.id = "dc_visible_expression_table";
            visibleexpression.innerHTML = visibleexpressionHtml;
            jQuery(ctl).append(visibleexpression);
            var expressionDom = jQuery(ctl).find('#dc_visible_expression_table')[0];
            expressionDom.addEventListener('click', function (e) {
                if (e && e.target) {
                    if (e.target.classList.contains('dc_visible_expression_table_close')) {
                        expressionDom.remove();
                    }
                }
            });
        }
    },

    /**
     * 设置form表单元素是否可用
     * @param formNode 表单元素
     * @param isDisable 是否可以使用，true表示不可用
     */
    changeFormDisable: function (formNode, isDisable) {
        if (!formNode || formNode.nodeName != "FORM") {
            return;
        }
        var els = formNode.elements;
        if (els && els.length > 0) {
            for (var i = 0; i < els.length; i++) {
                els[i].disabled = isDisable ? true : false;
            }
        }
    },

    /**
         * 删除表格行或者表格列的对话框
         * @param rootElement 编辑器对象
         * @param callBack 对话框处理完成的回调事件，用于处理防抖
         */
    DeleteRowOfColumnsDialog: function (rootElement, callBack) {
        //样式
        var queryDeleteRowOfColumnsStyle = rootElement.querySelector && rootElement.querySelector('#dc_query_delete_row_of_columns_mask_style') || null;
        if (!queryDeleteRowOfColumnsStyle) {
            queryDeleteRowOfColumnsStyle = rootElement.ownerDocument.createElement("style");
            queryDeleteRowOfColumnsStyle.id = 'dc_query_delete_row_of_columns_mask_style';
            queryDeleteRowOfColumnsStyle.innerHTML = `
                #dc_maskDiv{
                    width:100%;
                    height:100%;
                    background-color:#000000;
                    position:relative;
                    z-index:10000;
                    top:-100%;
                    opacity:0.2;
                }
                #dc_queryDeleteRowOfColumnsBox_Box{
                    width: 230px;
                    height: 150px;
                    position:absolute;
                    top: 50%;
                    left: 50%;
                    background-color: #fff;
                    border: 1px solid #ccc;
                    z-index: 10001;
                    margin-left: -120px;
                    margin-top: -100px;
                    display: flex;
                    flex-direction: column;
                }
                #dc_queryDeleteRowOfColumnsBox_Box .dc_queryDeleteRowOfColumnsBox_header{
                    padding: 6px 10px;
                    box-sizing: border-box;
                    background:linear-gradient(to bottom, #ffffff 0, #eeeeee 100%);
                    border-bottom: 1px solid #c6c6c6;
                    color:#0E2D5F;
                    font-size: 12px;
                    font-weight: bold;
                }
                #dc_queryDeleteRowOfColumnsBox_Box .dc_queryDeleteRowOfColumnsBox_Container{
                    padding: 10px;
                    box-sizing: border-box;
                    flex:1;
                    display: flex;
                    justify-content: space-evenly;
                    align-items: center;
                }
                #dc_queryDeleteRowOfColumnsBox_Box .dc_queryDeleteRowOfColumnsBox_footer{
                    padding: 6px 10px;
                    box-sizing: border-box;
                    background-color: #f5f5f5;
                    border-top: 1px solid #ccc;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                }
                #dc_queryDeleteRowOfColumnsBox_Box .dc_queryDeleteRowOfColumnsBox_footer > div{
                    padding: 0 5px;
                    box-sizing: border-box;
                    font-size: 12px;
                    margin: 0 4px;
                    cursor: pointer;
                    background: linear-gradient(to bottom, #ffffff 0, #eeeeee 100%);
                    border: 1px solid #bbb;
                    color: #444;
                    border-radius: 3px;
                }
                #dc_queryDeleteRowOfColumnsBox_Box .dc_queryDeleteRowOfColumnsBox_footer > div:hover{
                    background-color: #ccc;
                }
            `;
            rootElement.appendChild(queryDeleteRowOfColumnsStyle);


            // 遮罩
            var maskDiv = rootElement.querySelector("#dc_maskDiv") || null;
            if (!maskDiv) {
                maskDiv = rootElement.ownerDocument.createElement("div");
                maskDiv.id = "dc_maskDiv";
                rootElement.appendChild(maskDiv);
            }

            //对话框内容
            var dialogContentDiv = rootElement.querySelector && rootElement.querySelector('#dc_queryDeleteRowOfColumnsBox_Box') || null;
            if (!dialogContentDiv) {
                dialogContentDiv = rootElement.ownerDocument.createElement("div");
                dialogContentDiv.id = "dc_queryDeleteRowOfColumnsBox_Box";
                dialogContentDiv.innerHTML = `
                <div class="dc_queryDeleteRowOfColumnsBox_header">请选择删除操作</div>
                <div class="dc_queryDeleteRowOfColumnsBox_Container">
                    <label>
                        <input type="radio" name="operation" value="deleteRow" checked> 删除表格行
                    </label>
                    <label>
                        <input type="radio" name="operation" value="deleteColumn"> 删除表格列
                    </label>
                </div>
                <div class="dc_queryDeleteRowOfColumnsBox_footer">
                    <div id="dc_queryDeleteRowOfColumnsBox_footer_confirmbtn">确认</div>
                    <div id="dc_queryDeleteRowOfColumnsBox_footer_cancelbtn">取消</div>
                </div>
            `;
                rootElement.appendChild(dialogContentDiv);
                // 取消按钮
                var cancelBtn = dialogContentDiv.querySelector && dialogContentDiv.querySelector("#dc_queryDeleteRowOfColumnsBox_footer_cancelbtn");
                cancelBtn.addEventListener("click", function () {
                    RemoveDeleteRowOfColumns();
                    rootElement.Focus();
                });
                // 确认按钮
                var confirmBtn = dialogContentDiv.querySelector && dialogContentDiv.querySelector("#dc_queryDeleteRowOfColumnsBox_footer_confirmbtn");
                confirmBtn.addEventListener("click", function () {
                    var queryDeleteRowOfColumnsMask = rootElement.querySelector && rootElement.querySelector("#dc_queryDeleteRowOfColumnsBox_Box") || null;
                    if (queryDeleteRowOfColumnsMask) {
                        let removeValue = queryDeleteRowOfColumnsMask.querySelector && queryDeleteRowOfColumnsMask.querySelector("input[name='operation']:checked").value || null;
                        if (removeValue) {
                            if (removeValue === 'deleteRow') {
                                rootElement.DCExecuteCommand("Table_DeleteRow", false, null);
                            } else if (removeValue === 'deleteColumn') {
                                rootElement.DCExecuteCommand("Table_DeleteColumn", false, null);
                            }
                        }

                        // 删除对话框
                        RemoveDeleteRowOfColumns();
                    }
                });

                function RemoveDeleteRowOfColumns() {
                    //移除遮罩层
                    var maskDiv = rootElement.querySelector && rootElement.querySelector("#dc_maskDiv") || null;
                    maskDiv && maskDiv.remove();

                    //移除对话框内容
                    var dialogContentDiv = rootElement.querySelector && rootElement.querySelector('#dc_queryDeleteRowOfColumnsBox_Box') || null;
                    dialogContentDiv && dialogContentDiv.remove();

                    //移除样式
                    var queryDeleteRowOfColumnsStyle = rootElement.querySelector && rootElement.querySelector('#dc_query_delete_row_of_columns_mask_style') || null;
                    queryDeleteRowOfColumnsStyle && queryDeleteRowOfColumnsStyle.remove();

                    //调用回调函数
                    callBack && callBack();

                }


            }
        }
    }
};

/**
 * 将原始日期字符串转换为Date对象
 * @param originalDate 输入域目前已知时间校验字符串的格式有："01/6/2024 10:56:00"、"2024/2/1 下午1:47:00"
 * @return convertedDate 转换后的时间字符串：'2024-01-06 10:56:00'
 * DUWRITER5_0-1886:20240201,lxy,增加一个种带有中文的校验格式："2024/2/1 下午1:47:00"
 */
var RangeDate = function (originalDate) {
    var isAdd12HoursFlag = false;
    if (originalDate.indexOf('上午') > -1) {
        originalDate = originalDate.replace("上午", "");
    } else if (originalDate.indexOf('下午') > -1) {
        isAdd12HoursFlag = true;
        originalDate = originalDate.replace("下午", "");
    }
    var dateObj = new Date(originalDate);

    // 获取年、月、日、小时、分钟和秒
    var year = dateObj.getFullYear();
    var month = ("0" + (dateObj.getMonth() + 1)).slice(-2);
    var day = ("0" + dateObj.getDate()).slice(-2);
    var hours = isAdd12HoursFlag ? ("0" + (dateObj.getHours() + 12)).slice(-2) : ("0" + dateObj.getHours()).slice(-2);
    var minutes = ("0" + dateObj.getMinutes()).slice(-2);
    var seconds = ("0" + dateObj.getSeconds()).slice(-2);

    // 判断是否需要进位
    if (hours == "24") {
        dateObj.setDate(dateObj.getDate() + 1);
        year = dateObj.getFullYear();
        month = ("0" + (dateObj.getMonth() + 1)).slice(-2);
        day = ("0" + dateObj.getDate()).slice(-2);
        hours = "00";
    }

    var convertedDate = year + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;

    return convertedDate;
};

/**
 * 将对象的所有键转换为小写
 * @param obj 需要处理的对象
 * @return obj 处理完成的对象
 */
var keysToLowerCase = function (obj) {
    var keys = Object.keys(obj);
    var n = keys.length;
    while (n--) {
        var key = keys[n]; // "cache" it, for less lookups to the array
        if (key !== key.toLowerCase()) {
            // might already be in its lower case version
            obj[key.toLowerCase()] = obj[key]; // swap the value to a new lower case key
            delete obj[key]; // delete the old key
        }
    }
    return obj;
};

/**
 * 获取或者添加最下面的数据
 * @param data 需要处理的对象
 * @param txt 键值对中的键，可以是"b.c",获取的是 data["b"]["c"]
 * @param value 键值对中的值，可以是"b.c",获取的是 data["b"]["c"]
 */
var getDown = function getDown(data, txt, value) {
    if (!data || typeof data != "object") {
        return;
    }
    if (value == undefined) {
        data = keysToLowerCase(JSON.parse(JSON.stringify(data)));
        txt = txt.toLowerCase();
    }
    var _index = txt.indexOf(".");
    if (_index > -1) {
        var objArr = [txt.slice(0, _index), txt.slice(_index + 1, txt.length)];
        if (!data[objArr[0]] && value != undefined) {
            data[objArr[0]] = {};
        }
        return getDown(data[objArr[0]], objArr[1], value);
    } else {
        if (value == undefined) {
            return data[txt];
        } else {
            data[txt] = value;
        }
    }
};

/**
 * 获取或者修改数据
 * @param dcPanelBody 获取元素的父元素JQUEY对象
 * @param data 需要修改的数据
 * @return obj 处理完成的对象
 */
var GetOrChangeData = function (dcPanelBody, data, specialTreatmentFunc) {
    if (!dcPanelBody) {
        return;
    }
    var isChange = typeof data == "object";
    var obj = {};
    dcPanelBody.find("[data-text]").each(function () {
        var _el = jQuery(this);
        var _txt = _el.attr("data-text");
        if (!!specialTreatmentFunc && typeof specialTreatmentFunc == "function") {
            if (specialTreatmentFunc.call(this, _txt, isChange) == false) {
                return true;
            }
        }
        if (this.type == "checkbox") {
            getDown(obj, _txt, _el.is(":checked"));
            if (isChange) {
                var isChecked = getDown(data, _txt);
                if (typeof isChecked == "boolean") {
                    _el.prop("checked", isChecked);
                    if (!!_el.change && typeof _el.change == "function") {
                        _el.change();
                    }
                }
            }
        } else if (this.type == "radio") {
            if (_el.is(":checked")) {
                getDown(obj, _txt, _el.val());
            }
            if (isChange && _el.val() == getDown(data, _txt)) {
                _el.prop("checked", true);
                if (!!_el.change && typeof _el.change == "function") {
                    _el.change();
                }
            }
        } else {
            // 获取或者接受的数据类型
            switch (_el.attr("data-type")) {
                case "Object":
                    //{}
                    break;
                case "Array":
                    //[{}]
                    var tbody = _el.find("tbody");
                    var trs = tbody.find("tr");
                    if (isChange) {
                        var item = _el.find("template.dc_template_item")[0].content;
                        var item_contents = getDown(data, _txt);
                        if (
                            Object.prototype.toString.call(item_contents) === "[object Array]"
                        ) {
                            // 数据是数组
                            trs.remove();
                            for (var i = 0; i < item_contents.length; i++) {
                                var item_obj = item_contents[i];
                                var clone_item = item.cloneNode(true);
                                var inputs = clone_item.querySelectorAll("[data-arraytext]");
                                for (var j = 0; j < inputs.length; j++) {
                                    var input = inputs[j];
                                    var _arraytext = input.getAttribute("data-arraytext");
                                    input.value = item_obj[_arraytext];
                                }
                                tbody.append(clone_item);
                            }
                            tbody.append(item.cloneNode(true));
                        }
                    } else {
                        var item_arr = [];
                        trs.each(function () {
                            var item_obj = {};
                            var isPush = false; //是否存储
                            jQuery(this)
                                .find("[data-arraytext]")
                                .each(function () {
                                    var _arraytext = jQuery(this).attr("data-arraytext");
                                    var _arrayvalue = jQuery(this).val();
                                    if (_arrayvalue) {
                                        // 当存在一个数据时就存储
                                        isPush = true;
                                    }
                                    item_obj[_arraytext] = _arrayvalue;
                                });
                            if (isPush) {
                                item_arr.push(item_obj);
                            }
                        });
                        getDown(obj, _txt, item_arr);
                    }
                    break;
                default:
                    var _value = _el.val();
                    if (this.type == "number") {
                        _value -= 0;
                    }
                    getDown(obj, _txt, _value);
                    if (isChange) {
                        var _value = getDown(data, _txt);
                        if (this.type == "number") {
                            _value = parseFloat(_value);
                        }
                        if (this.nodeName == "SELECT" && _value == "") {
                            // 是下拉，并且值为空
                            return true;
                        }
                        if (_value && typeof _value == "object") {
                            _el.val(JSON.stringify(_value));
                        } else {
                            _el.val(_value);
                        }
                    }
                    break;
            }
        }
    });
    if (isChange) {
        return true;
    } else {
        return obj;
    }
};
