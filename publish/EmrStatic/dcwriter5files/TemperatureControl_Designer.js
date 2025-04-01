//* 开始时间:2024-6-12 16:00:00
//* 开发者:李新宇
//* 重要描述:开发体温单设计器功能
//*************************************************************************
//* 最后更新时间: 2024-6-12 16:00:00
//* 最后更新人: 李新宇
//*************************************************************************


"use strict";
import { DCTools20221228 } from "./DCTools20221228.js";

export let TemperatureControl_Designer = {
    /**
     * 体温单设计器的css样式
     */
    designerDivStyleInnerHTML: `
        #dc_designer_options_content::-webkit-scrollbar,
        #designerDiv20240612160000_myWriterControl div::-webkit-scrollbar,
        #dc_designer_tree::-webkit-scrollbar {
            width: 8px;
            height: 8px;
        }
        #dc_designer_options_content::-webkit-scrollbar-thumb,
        #designerDiv20240612160000_myWriterControl div::-webkit-scrollbar-thumb,
        #dc_designer_tree::-webkit-scrollbar-thumb {
            border-radius: 8px;
            opacity: 0.2;
            background: #c1c1c1;
        }
        #dc_designer_options_content::-webkit-scrollbar-track,
        #designerDiv20240612160000_myWriterControl div::-webkit-scrollbar-track,
        #dc_designer_tree::-webkit-scrollbar-track{
            border-radius: 0;
            background: #f1f1f1;
        }

        #dc_designerDiv20240612160000_mask{
            width:100%;
            height:100%;
            position:fixed;
            top:0;
            left:0;
            background-color:rgba(0,0,0,.5);
            z-index:999;
        }
        #dc_designerDiv20240612160000{
            width:90%;
            height:90%;
            border:1px solid #e8edfa;
            background-color:#fff;
            overflow:hidden;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            z-index:99;
            border-radius: 5px;
            display: flex;
            flex-direction: column;
            overflow: hidden;
            font-size:13px;
        }
        #dc_designerDiv20240612160000_title {
            display: flex;
            justify-content: space-between;
            padding: 20px;
            background-color: #fafafa;
        }
        .dcHeader-title{
            font-weight: 900;
            font-size: 20px;
        }
        #dc_designerDiv20240612160000_top{
            display: flex;
            padding: 10px 20px;
            border-bottom: 1px solid #dcdfe6;
            background-color: #fafafa;
            user-select: none;
            color: #606266;
        }
        #dc_designerDiv20240612160000_center {
            flex: 1;
            display: flex;
            overflow:hidden;
        }
        #dc_designer_tree{
            width: 15%;
            height: 100%;
            padding: 10px;
            overflow: auto;
            user-select: none;
            min-width: 150px;
            color:#606266;
            box-sizing: border-box;
        }
        .dc_designer_view{
            flex:1;
            margin:0 5px !important;
            overflow:hidden;
        }
        .dc_designer_options{
            width: 30%;
            height: 100%;
            display: flex;
            flex-direction: column;
            background: #e4e7ed;
            min-width: 200px;
        }
        .dc_designer_tree_item{
            padding-left: 24px;
        }
        .dc_designer_tree_item_title_text{
            cursor:pointer;
            line-height:26px;
            font-size: 13px;
            width: 100%;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }
        .dc_designer_tree_item_title_icon{
            display:inline-block;
            width: 0;
            height: 0;
            border-left: 4px solid transparent;
            border-right: 4px solid transparent;
            border-bottom: 6px solid #c0c4cc;
            cursor: pointer;
            transition: transform 0.3s;
            transform:rotate(180deg);
          
        }
        .dc_designer_options_item_title_icon {
            border: 1px solid #ccc;
            border-radius: 2px;
            width: 16px;
            display: inline-block;
            text-align: center;
            line-height: 12px;
            padding: 0px;
            height: 16px;
            cursor:pointer;
        }
        .dc_designer_options_item_title_icon:hover,
        .dc_designer_tree_item_title_text:hover,
        .dc_designer_tree_item_title_icon:hover{
            color:#409eff;
        }
        .dc_designer_button{
            padding: 10px 0;
            margin-right: 50px;
        }
        .dc_designer_tree_item_title_text_active{
            color:#409eff;
        }
        .dc_designer_button_confirm{
            width: 74px;
            text-align: center;
            border-radius: 5px;
            color: #fff;
            background-color: #409eff;
            border-color: #409eff;
        }
        .dc_designer_button.dc_designer_button_confirm:hover,
        .dc_designer_button.dc_designer_button_cancel:hover{
            background: #66b1ff;
            border-color: #66b1ff;
            color: #fff;
        }
        
        .dc_designer_button_cancel{
            width: 74px;
            background: #fff;
            border: 1px solid #dcdfe6;
            color: #606266;
            text-align: center;
            border-radius: 5px;
        }
        .dc_designer_options_item{
            width: 100%;
            display: flex;
            justify-content: space-between;
            border-bottom: 1px solid #fff;
            align-items: center;
            height: 40px;
            background: #e4e7ed;
        }

        .dc_designer_options_item_title {
            width: 55%;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
            padding: 0 2px;
            color: #909399;
            cursor: default;
            padding-left: 20px;
            box-sizing: border-box;
        }
        .dc_designer_options_item_title_text{
            color: #606266;
        }
        .dc_designer_options_item_value {
            flex: 1;
            border-bottom: 1px solid #e5e9f2;
            height: 100%;
            background: #fff;
        }
        .dc_designer_options_item_value_input {
            width: 100%;
            height: 90%;
            padding: 0 10px;
            border: 1px solid transparent;
            color: #606266;
            outline: none;
            background: #fff;
            box-sizing: border-box;
            overflow: hidden;
        }
        .dc_designer_options_item_value_input:focus {
            outline: none;
            border:1px solid #409eff;
        }
        .dc_designer_options_item_value  .dc_designer_options_item_value_input[type="checkbox"] {
            width: 24px;
            height: 24px;
            cursor: pointer;
            margin-top: 8px;
            margin-left: 10px;
        }

        #dc_designer_options_content{
            flex:1;
            border-bottom: 1px solid #e5e9f2;
            background: #fff;
            overflow-x:hidden;
            overflow-y:auto;
        }
        .dc_designer_options_item_children .dc_designer_options_item_title{
            padding-left: 56px;
            width: calc(55% - 54px);
        }
        .dc_designer_options_item_title_icon_null{
            display:inline-block;
            width:16px;
            height:16px;
        }
        .dc_designerDiv_close {
            width: 24px;
            font-weight: 900;
            font-size: 16px;
            cursor: pointer;
            color: #989696;
            border: none;
            background: transparent;

        }
        .dc_designerDiv_close:hover{
            color: #409eff;
        }
        .dc_designer_button svg:hover{
            fill:#409eff !important;
        }
        .dc_designer_tree_item {
            display: flex;
            flex-direction: column;
        }
        #dc_designerDiv20240612160000_bottom{
            display: flex;
            padding: 24px;
            border-top: 1px solid #c1c1c1;
            user-select: none;
            justify-content: center;
            align-items: flex-end;
            background-color: #fff;
        }
        #dc_designer_options_top{
            background-color:#FFF;
            height:60px;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        #dc_designer_change_options{
            display: inline-block;
            line-height: 1;
            white-space: nowrap;
            cursor: pointer;
            border: 1px solid #dcdfe6;
            text-align: center;
            box-sizing: border-box;
            transition: .1s;
            font-weight: 500;
            padding: 12px 20px;
            border-radius: 4px;
            color: #409eff;
            background: #ecf5ff;
            border-color: #b3d8ff;
        }
        #dc_designer_change_options:hover{
            color: #fff;
            background-color: #409eff;
            border-color: #409eff;
        }
            
        .dc_designer_button_content {
            position: absolute;
            z-index: 999;
            border: 1px solid #ebeef5;
            border-bottom: none;
            border-radius: 4px;
            background: #fff;
            overflow: hidden;
            color: #303133;
            transition: .3s;
            box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
            display: none;
        }

        .dc_designer_button_content_item,
        .dc_designer_add_element_item {
            width: 100%;
            height: 28px;
            line-height: 28px;
            padding: 0 10px;
            box-sizing: border-box;
            font-size: 13px;
        }
        .dc_designer_button_content_item:hover,
        .dc_designer_button_content_item_active,
        .dc_designer_add_element_item:hover,
        .dc_designer_add_element_item_active {
            color: #409eff;
        }
        .dc_designer_button:hover > .dc_designer_button_span{
            color:#409eff;
            cursor:pointer;
        }
        #dc_designer_page_setting_button_content {
            position: absolute;
            z-index: 999;
            padding: 20px;
            width: 300px;
            box-sizing: border-box;
            border-radius: 4px;
            background: #fff;
            overflow: hidden;
            color: #303133;
            transition: .3s;
            border: 1px solid #ebeef5;
            box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
            font-size: 13px;
            display: none;
        }

        .dc_designer_page_setting_item {
            width: 100%;
            display: flex;
            margin-bottom: 10px;
        }

        #dc_designer_page_size_input,
        #dc_designer_page_Landscape_input,
        #dc_designer_page_setting_PaperWidth_value,
        #dc_designer_page_setting_PaperHeight_value {
            flex: 1;
            height: 24px;
            padding: 0 10px;
            border: 1px solid #cccc;
            color: #606266;
            border-radius: 2px;
        }
        #dc_designer_page_size_input:focus-visible,
        #dc_designer_page_Landscape_input:focus-visible {
            border-color:#409eff;
            outline: none;
        }
        #dc_designer_page_setting_button_content .dc_designer_page_setting_item_title{
            width: 80px;
        }
        .dc_designer_page_setting_item_Margin_Box{
            flex:1;
            overflow:hidden;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
        }
        .dc_designer_page_setting_pageMargin_value{
            width: 100%;
            height: 24px;
            padding: 0 10px;
            border: 1px solid #cccc;
            color: #606266;
            border-radius: 2px;
        }
        .dc_designer_page_setting_pageMargin_value:focus{
            outline: none;
            border-color:#409eff;
        }
        .dc_designer_page_setting_item_pageMargin {
            width: 86px;
            overflow: hidden;
        }
        .dc_designer_page_setting_footer{
            width: 100%;
            display: flex;
            justify-content: flex-end;
            align-items: center;
            margin-top: 24px;
        }
        .dc_designer_page_setting_footer > div {
           cursor: pointer;
           margin-left: 16px;
           cursor: pointer;
        }

        #dc_designer_page_setting_button_confirm{
            color: #409eff;
        }

        .dc_designer_add_element_content{
            position: absolute;
            z-index: 999;
            padding: 10px;
            width: 150px;
            box-sizing: border-box;
            border-radius: 4px;
            background: #fff;
            overflow: hidden;
            color: #303133;
            transition: .3s;
            border: 1px solid #ebeef5;
            box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
            font-size: 13px;
            display: none;
        }
        #dc_designerDiv_top_view_box,
        #dc_designerDiv_top_page_setting_box,
        #dc_designerDiv_top_add_element_box{
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            z-index: 99;
            transform: translate3d(0px, 0px, 0px);
            display: none;
        }
        #designerDiv20240612160000_myWriterControl{
            width:100%;
            height:100%;
            padding: 1px;
        }
        .dc_designer_tree_item_title_box{
            display: flex;
            align-items: center;
        }

        .dc_designer_tree_item_title_span,
        .dc_designer_tree_item_title_text{
            display:inline-block;
            font-size:13px;
            margin-left:2px;
        }

        .dc_designer_options_item_array_box {
            width: 100%;
            height: auto;
            overflow: hidden;
            box-sizing: border-box;
            background-color: #e4e7ed;
            color: #909399;
        }

        .dc_designer_options_array_item {
            width: 100%;
            margin-top:-1px;
        }

        .dc_designer_options_array_option_title,
        .dc_designer_options_array_option_value  {
            display: flex;
            flex-wrap: nowrap;
            border: 1px solid #fff;
        }

        .dc_designer_options_array_option_title_item,
        .dc_designer_options_array_option_value_item{
            overflow: hidden;
            border-left: 1px solid #fff;
            margin-left: -1px;
            box-sizing: border-box;
            padding: 0 2px;
            line-height: 24px;
        }

        .dc_designer_options_array_option_title_item.dc_designer_options_array_option_title_item_index,
        .dc_designer_options_array_option_value_item.dc_designer_options_array_option_value_item_index{
            width: 60px;
            text-align: center;
        }
        .dc_designer_options_array_option_value {
            margin-top: -1px;
        }
        .dc_designer_options_item_object_box{
            width: 100%;
            height: 100%;
            background-color: #e4e7ed;
        }
        .dc_designer_options_object_item {
            width: 100%;
            height: 24px;
            line-height: 26px;
            display: flex;
            padding-left: 30px;
            box-sizing: border-box;
            flex-wrap: nowrap;
            border-bottom: 1px solid #fff;
            overflow: hidden;
        }

        .dc_designer_options_object_item_title {
            flex: 1;
            padding-left: 0px;
        }

        .dc_designer_options_object_item_value {
            flex: 1;
        }
        `,
    /**
      * 体温单id
      */
    TemperatureControlID: null,
    /**
      * 保留一份当前正在修改的元素的属性
      */
    TemperatureCurrentElementOldOptions: null,
    /**
     * 初始化体温单设计器对话框
     * @param {Object} rootElement 设计器div的根元素
     * @param {String} xml 设计器的xml字符串
     */
    initDesignerDiv: function (rootElement, xml) {
        let that = this;
        that.TemperatureControlID = rootElement.id;//保留体温单id
        var designerDiv = that.GetDesignerRootElementDialog();
        if (designerDiv) { return false; }
        //判断是否已经有css
        var dcHead = document.head;
        var hasdesignerDivCss = dcHead.querySelector('#dc_designerDiv20240612160000_content');
        if (!hasdesignerDivCss) {
            var designerDivStyle = rootElement.ownerDocument.createElement('style');
            designerDivStyle.innerHTML = that.designerDivStyleInnerHTML;
            designerDivStyle.id = 'dc_designerDiv20240612160000_content';
            dcHead.appendChild(designerDivStyle);
        }




        //创建一个遮罩层div
        var maskDiv = rootElement.ownerDocument.createElement('div');
        maskDiv.id = 'dc_designerDiv20240612160000_mask';
        /**有问题，暂时不用固定纸张大小
         *  <div class="dc_designer_page_setting_item_title">纸张大小：</div>
            <select id="dc_designer_page_size_input" class="dc_designer_page_setting_item_value" attr-id="PaperSizeName">
                <option value="A3">A3</option>
                <option value="A4">A4</option>
                <option value="A5">A5</option>
                <option value="B4">B4</option>
                <option value="B5">B5</option>
            </select>
        */



        //创建设计器最外层div
        designerDiv = rootElement.ownerDocument.createElement('div');
        designerDiv.id = 'dc_designerDiv20240612160000';
        designerDiv.innerHTML = `
        <div id="dc_designerDiv20240612160000_title">
            <div class="dcHeader-title">都昌时间轴样式设计器</div>
            <span id="dc_designerDiv20240612160000_close" class="dc_designerDiv_close dc_designer_button_cancel" title="关闭设计器">✖</span>
        </div>
        <div id="dc_designerDiv20240612160000_top">
            <div  class="dc_designer_button ">
                <div class="dc_designer_button_span" id="dc_designer_tree_button">隐藏结构树</div>
            </div>
            <div id="dc_designer_tree_button_refresh" class="dc_designer_button">
                <div class="dc_designer_button_span" id="dc_designer_tree_button_refresh">刷新结构树</div>
            </div>
            <div id="dc_designer_button_view_mode_Box" class="dc_designer_button">
                <div class="dc_designer_button_span" id="dc_designer_view_button">视图模式</div>
                <div class="dc_designer_button_content" id="dc_designer_button_content">
                    <div class="dc_designer_button_content_item dc_designer_button_content_item_active" data-value="Page">普通</div>
                    <div class="dc_designer_button_content_item" data-value="Temperature">时间轴视图</div>
                    <div class="dc_designer_button_content_item" data-value="SinglePage">单页视图</div>
                </div>
                <div id="dc_designerDiv_top_view_box"></div>
            </div>
            <div class="dc_designer_button">
                <div class="dc_designer_button_span" id="dc_designer_page_setting_button">页面设置</div>
                <div class="dc_designer_page_setting_button" id="dc_designer_page_setting_button_content">
                    <div style="margin-bottom: 10px;background-color: #f4f4f5;color: #909399;font-size: 12px;padding-left: 5px;">提示：单位均为毫米。</div>
                    <div class="dc_designer_page_setting_item">
                        <div class="dc_designer_page_setting_item_title">页面宽度：</div>
                        <input type="number" id="dc_designer_page_setting_PaperWidth_value" class="dc_designer_page_setting_item_value dc_designer_page_setting_pageMargin_value" attr-id="PaperWidth"></input>
                    </div>
                    <div class="dc_designer_page_setting_item">
                        <div class="dc_designer_page_setting_item_title">页面高度：</div>
                        <input type="number" id="dc_designer_page_setting_PaperHeight_value" class="dc_designer_page_setting_item_value dc_designer_page_setting_pageMargin_value" attr-id="PaperHeight"></input>
                    </div>
                    <div class="dc_designer_page_setting_item">
                        <div class="dc_designer_page_setting_item_title">纸张方向：</div>
                        <select id="dc_designer_page_Landscape_input" class="dc_designer_page_setting_item_value" attr-id="Landscape">
                            <option value="true">横向</option>
                            <option value="false">纵向</option>
                        </select>
                    </div>
                    <div class="dc_designer_page_setting_item">
                        <div class="dc_designer_page_setting_item_title">页边距：</div>
                            <div class="dc_designer_page_setting_item_Margin_Box">
                                <div class="dc_designer_page_setting_item_pageMargin">
                                    <div class="dc_designer_page_setting_item_title">左(L)：</div>
                                    <input type="number" class="dc_designer_page_setting_pageMargin_value dc_designer_page_setting_item_value" attr-id="LeftMargin"></input>
                                </div>
                                <div class="dc_designer_page_setting_item_pageMargin">
                                    <div class="dc_designer_page_setting_item_title">右(R)：</div>
                                    <input type="number" class="dc_designer_page_setting_pageMargin_value dc_designer_page_setting_item_value" attr-id="RightMargin"></input>
                                </div>
                                <div class="dc_designer_page_setting_item_pageMargin">
                                    <div class="dc_designer_page_setting_item_title">上(T)：</div>
                                    <input type="number" class="dc_designer_page_setting_pageMargin_value dc_designer_page_setting_item_value" attr-id="TopMargin"></input>
                                </div>
                                <div class="dc_designer_page_setting_item_pageMargin">
                                    <div class="dc_designer_page_setting_item_title">下(B)：</div>
                                    <input type="number" class="dc_designer_page_setting_pageMargin_value dc_designer_page_setting_item_value" attr-id="BottomMargin"></input>
                                </div>
                            </div>
                        </div>
                        <div class="dc_designer_page_setting_footer">
                            <div id="dc_designer_page_setting_button_confirmcancel">取消</div>
                            <div id="dc_designer_page_setting_button_confirm">确认</div>
                        </div>
                    </div>
                </div>
                <div id="dc_designerDiv_top_page_setting_box"></div>
                <div class="dc_designer_button">
                    <div class="dc_designer_button_span" id="dc_designer_add_element_button">新增元素</div>
                    <div class="dc_designer_add_element_content" id="dc_designer_add_element_content">
                        <div class="dc_designer_add_element_item" data-element="HeaderLabel">新增眉栏属性</div>
                        <div class="dc_designer_add_element_item" data-element="Label">新增文本标签属性</div>
                        <div class="dc_designer_add_element_item" data-element="GeneralItem">新增一般项目属性</div>
                        <div class="dc_designer_add_element_item" data-element="SpecialItems">新增特殊项目属性</div>
                        <div class="dc_designer_add_element_item" data-element="YAxisInfos">新增体征项目属性</div>
                    </div>
                    <div id="dc_designerDiv_top_add_element_box"></div>
                </div>
                <div class="dc_designer_button">
                    <div class="dc_designer_button_span" id="dc_designer_move_up_button">顺序靠前</div>
                </div>
                <div class="dc_designer_button">
                    <div class="dc_designer_button_span" id="dc_designer_move_down_button">顺序靠后</div>
                </div>
                 <div class="dc_designer_button">
                    <div class="dc_designer_button_span" id="dc_designer_delete_button">删除</div>
                </div>
                <div class="dc_designer_button">
                    <div class="dc_designer_button_span" id="dc_designer_export_button">导出文档</div>
                </div>
            </div>
            <div id="dc_designerDiv20240612160000_center">
                <div id="dc_designer_tree" class="dc_designer_tree" style="display:block;">暂无数据</div>
                <div class="dc_designer_view">
                    <div id="designerDiv20240612160000_myWriterControl" dctype="DCTemperatureControlForWASM"></div>
                </div>
                <div class="dc_designer_options">
                    <div id="dc_designer_options_content"></div>
                    <div id="dc_designer_options_top">
                        <div id="dc_designer_change_options" title="将修改后的属性应用到设计器中">应用</div>
                    </div>
                </div>
            </div>
            <div id="dc_designerDiv20240612160000_bottom">
                <div id="dc_designer_button_confirm" class="dc_designer_button dc_designer_button_confirm ">确定</div>
                <div id="dc_designer_button_cancel" class="dc_designer_button dc_designer_button_cancel">取消</div>
            </div>
        `;
        maskDiv.appendChild(designerDiv);
        rootElement.appendChild(maskDiv);


        // 创建设计器体温单
        if (CreateTemperatureControlForWASM) {
            var DesignerRootElement = that.GetDesignerRootElement();
            //给体温单增加四代服务
            var servicepageurl = rootElement.getAttribute('servicepageurl') || null;
            if (servicepageurl) {
                DesignerRootElement.setAttribute("servicepageurl", servicepageurl);
            }

            //给体温单兼容河北省标准 术日中文展示
            var surgicalreverseandchinese = rootElement.getAttribute('surgicalreverseandchinese') || null;
            if (surgicalreverseandchinese == "true" || surgicalreverseandchinese == true) {
                DesignerRootElement.setAttribute("surgicalreverseandchinese", true);
            }

            //给体温单增加注册码
            var registerCodeStr = rootElement.getAttribute("registercode") || null;
            if (registerCodeStr) {
                DesignerRootElement.setAttribute("registercode", registerCodeStr);
            }

            DesignerRootElement.EventTemperatureControlInit = function () {
                //创建设计器对话框顶部工具栏点击事件
                DesignerRootElement.DesignerMode(true);
                let loadXml = JSON.parse(JSON.stringify(xml));
                DesignerRootElement.LoadTemperatureDocumentFromString(loadXml);//加载设计的xml文档
            };
            DesignerRootElement.EventTemperatureDocumentLoad = function () {
                that.refreshDesignerTree(); //渲染目录树
            };
            DesignerRootElement.EventStructureClick = function (control, StructureInfo, type) {
                console.log(StructureInfo, '----StructureInfo');
                //判断是否为文档全局配置
                var uid = StructureInfo && StructureInfo.UID ? JSON.parse(JSON.stringify(StructureInfo.UID)) : 'document';
                that.triggerDesignerTreeActive(uid);//选中左侧结构列表

                let CurrentDesignerElement = designerDiv.querySelector(`span[attr-id="${uid}"]`);
                CurrentDesignerElement.scrollIntoView();
            };
            CreateTemperatureControlForWASM(DesignerRootElement);//创建体温单
            //-------------事件监听start-----------------------------------------------------------

            //-----------确认按钮----点击事件的监听-------------
            var dc_designer_button_confirm = designerDiv.querySelector('#dc_designer_button_confirm');
            dc_designer_button_confirm.addEventListener('click', function () {
                that.ConfirmDesigner();
            });

            //-----------取消按钮----点击事件的监听-------------
            var dc_designer_button_cancel = designerDiv.querySelector('#dc_designer_button_cancel');
            dc_designer_button_cancel.addEventListener('click', function () {
                that.CancelDesigner();
            });
            //-----------关闭按钮----点击事件的监听-------------
            var dc_designerDiv20240612160000_close = designerDiv.querySelector('#dc_designerDiv20240612160000_close');
            dc_designerDiv20240612160000_close.addEventListener('click', function () {
                that.CancelDesigner();
            });
            //-----------显隐目录树----点击事件的监听-------------
            var dc_designer_tree_button = designerDiv.querySelector('#dc_designer_tree_button');
            dc_designer_tree_button.addEventListener('click', function () {
                var DesignerTree = designerDiv.querySelector('#dc_designer_tree');
                DesignerTree.style.display = DesignerTree.style.display === "block" ? 'none' : 'block';
                dc_designer_tree_button.innerHTML = DesignerTree.style.display === "block" ? '隐藏结构树' : '展示结构树';
            });

            //-----------目录树整体----点击事件的监听--
            var DesignerTree = designerDiv.querySelector('#dc_designer_tree');
            DesignerTree.addEventListener('click', function (e) {
                e.stopPropagation();
                if (e.target.className.indexOf('dc_designer_tree_item_title_text') >= 0) {
                    //点击列表定位
                    var attrId = e.target.getAttribute('attr-id');
                    if (attrId) {
                        that.triggerDesignerTreeActive(attrId); //change选中节点
                    }
                } else if (e.target.className.indexOf('dc_designer_tree_item_title_icon') >= 0) {
                    //树状列表点击展开收缩
                    var isShow = e.target.getAttribute('attr-show');
                    var iconParentBox = e.target.parentNode.parentNode;
                    if (iconParentBox) {
                        var childrenBox = iconParentBox.querySelector('.dc_designer_tree_item');
                        if (childrenBox) {
                            isShow === 'true' ? childrenBox.style.display = 'none' : childrenBox.style.display = 'block';
                            e.target.setAttribute('attr-show', isShow === 'true' ? 'false' : 'true');
                            e.target.style.transform = isShow === 'true' ? 'rotate(90deg)' : 'rotate(180deg)';
                        }
                    }
                }

            });

            //-----------刷新目录树----点击事件的监听-------------
            var dc_designer_tree_button_refresh = designerDiv.querySelector('#dc_designer_tree_button_refresh');
            dc_designer_tree_button_refresh.addEventListener('click', function () {

                //给一个刷新状态
                var rootElementDesignerDialog = that.GetDesignerRootElementDialog();
                var DesignerTree = rootElementDesignerDialog.querySelector('#dc_designer_tree');
                DesignerTree.innerHTML = '<span style="color:grey;text-align:center;margin-top:20px;">加载中...</span>';
                //创建节点树后重新选中节点
                let CurrentDesignerElementID = that.GetDesignerCurrentElementID();
                setTimeout(() => {
                    //刷新目录树
                    that.refreshDesignerTree(CurrentDesignerElementID);
                }, 200);

            });

            //-----------应用按钮----点击事件的监听-------------
            var dc_designer_change_options = designerDiv.querySelector('#dc_designer_change_options');
            dc_designer_change_options.addEventListener('click', function () {
                that.ApplyDesignerProperty();
            });

            //-----------显隐视图模式按钮----点击事件的监听-------------
            var dc_designer_view_button = designerDiv.querySelector('#dc_designer_view_button');
            //视图模式选项框
            var dcDesignerViewModeContent = designerDiv.querySelector('#dc_designer_button_content');
            var dcDesignerDivTopViewBox = designerDiv.querySelector('#dc_designerDiv_top_view_box');//遮罩层
            dc_designer_view_button.addEventListener('click', function () {
                dcDesignerViewModeContent.style.display = dcDesignerViewModeContent.style.display === "block" ? 'none' : 'block';
                dcDesignerDivTopViewBox.style.display = dcDesignerViewModeContent.style.display === "block" ? 'block' : 'none';
                //设计器模式下没有视图模式，在此修改的是体温单的视图模式
                var rootElement = that.GetRootElement();
                let viewMode = rootElement.GetTemperatureViewMode();
                //视图模式增加选中状态
                var dcDesignerCurrentViewModeactive = designerDiv.querySelector('.dc_designer_button_content_item_active');
                dcDesignerCurrentViewModeactive && dcDesignerCurrentViewModeactive.classList.remove('dc_designer_button_content_item_active');
                let currentViewMode = dcDesignerViewModeContent.querySelector('.dc_designer_button_content_item[data-value="' + viewMode + '"]');
                currentViewMode.classList.add('dc_designer_button_content_item_active');
            });

            //-----------点击选择视图模式----点击事件的监听-------------
            dcDesignerViewModeContent.addEventListener('click', function (e) {
                e.stopPropagation();
                if (e && e.target && e.target.className) {
                    if (e.target.className.indexOf('dc_designer_button_content_item') === -1) {
                        console.log('不是视图模式选项');
                        return false;
                    }

                    //删除上一个视图模式
                    var dcDesignerCurrentViewModeactive = designerDiv.querySelector('.dc_designer_button_content_item_active');
                    dcDesignerCurrentViewModeactive && dcDesignerCurrentViewModeactive.classList.remove('dc_designer_button_content_item_active');

                    //给当前点击的视图模式增加选中状态
                    e.target.classList.add('dc_designer_button_content_item_active');

                    //设计器下没有视图模式，在此修改的是体温单的视图模式
                    var rootElement = that.GetRootElement();
                    var viewMode = e.target.getAttribute('data-value');
                    rootElement.SetTemperatureViewMode(viewMode);

                    //关闭视图模式选项
                    dcDesignerViewModeContent.style.display = 'none';
                    dcDesignerDivTopViewBox.style.display = 'none';

                }
            });
            //-----------点击视图模式之外的区域----点击事件的监听-
            dcDesignerDivTopViewBox.addEventListener('click', function () {
                //关闭视图模式选项
                dcDesignerViewModeContent.style.display = 'none';
                dcDesignerDivTopViewBox.style.display = 'none';
            });


            //-----------页面设置按钮----点击事件的监听-------------
            //页面设置对话框
            var dcDesignerPageSettingFormDialog = designerDiv.querySelector('#dc_designer_page_setting_button_content');
            //页面设置表单input元素
            var dcDesignerPageFormDomArr = dcDesignerPageSettingFormDialog.querySelectorAll('.dc_designer_page_setting_item_value');
            //页面设置按钮点击事件
            var dcDesignerPageSettingButton = designerDiv.querySelector('#dc_designer_page_setting_button');
            var dcDesignerDivTopPageSettingBox = designerDiv.querySelector('#dc_designerDiv_top_page_setting_box');//遮罩层
            dcDesignerPageSettingButton.addEventListener('click', function () {
                //页面设置按钮点击事件
                if (!dcDesignerPageSettingFormDialog) {
                    return false;
                }
                let dcDesPageIsShow = dcDesignerPageSettingFormDialog.style.display === "block";
                dcDesignerPageSettingFormDialog.style.display = dcDesPageIsShow ? 'none' : 'block';
                dcDesignerDivTopPageSettingBox.style.display = dcDesPageIsShow ? 'none' : 'block';
                if (dcDesPageIsShow) {
                    //关闭时直接返回，不需要给dom赋值
                    return false;
                }
                //展开页面设置时，设置页面属性
                //获取体温单当前页面属性
                var DesignerRootElement = that.GetDesignerRootElement();
                let pageSettings = DesignerRootElement.GetPageSettings();
                if (pageSettings) {
                    //设置页面属性
                    if (dcDesignerPageFormDomArr.length > 0) {
                        dcDesignerPageFormDomArr.forEach(item => {
                            let pageSettingsName = item.getAttribute('attr-id');
                            if (pageSettingsName) {
                                if (typeof pageSettings[pageSettingsName] === 'boolean') {
                                    pageSettings[pageSettingsName] = pageSettings[pageSettingsName].toString();
                                }
                                item.value = pageSettings[pageSettingsName];
                            }
                        });
                    }


                }
            });

            //-----------页面对话框中的点击----点击事件的监听-------------
            dcDesignerPageSettingFormDialog.addEventListener('click', function (e) {
                e.stopPropagation();
                if (e.target && e.target.id === "dc_designer_page_setting_button_confirmcancel") {
                    //关闭页面设置
                    dcDesignerPageSettingFormDialog && (dcDesignerPageSettingFormDialog.style.display = 'none');
                    dcDesignerDivTopPageSettingBox.style.display = 'none';
                } else if (e.target && e.target.id === "dc_designer_page_setting_button_confirm") {
                    //确认页面设置
                    if (dcDesignerPageFormDomArr.length > 0) {
                        var pageSettings = {};
                        dcDesignerPageFormDomArr.forEach(item => {
                            let pageSettingsName = item.getAttribute('attr-id');
                            if (pageSettingsName) {
                                if (['LeftMargin', 'RightMargin', 'TopMargin', 'BottomMargin', 'PaperWidth', 'PaperHeight'].indexOf(pageSettingsName) > -1) {
                                    pageSettings[pageSettingsName] = parseFloat(item.value);
                                } else if (['Landscape'].indexOf(pageSettingsName) > -1) {
                                    pageSettings[pageSettingsName] = (item.value === 'true');
                                } else {
                                    pageSettings[pageSettingsName] = item.value;
                                }
                            }
                        });
                        var DesignerRootElement = that.GetDesignerRootElement();
                        DesignerRootElement.DesignerMode(false);
                        DesignerRootElement.SetPageSettings(pageSettings);
                        DesignerRootElement.DesignerMode(true);
                        //当前是否有正在展示全局属性，如果是则需要刷新一下右侧属性列表，右侧列表中也有页面设置
                        let CurrentDesignerElementID = that.GetDesignerCurrentElementID();
                        if (CurrentDesignerElementID === 'document') {
                            //刷新目录树
                            that.refreshDesignerTree(CurrentDesignerElementID);
                        }

                    }
                    //关闭页面设置
                    dcDesignerPageSettingFormDialog && (dcDesignerPageSettingFormDialog.style.display = 'none');
                    dcDesignerDivTopPageSettingBox.style.display = 'none';
                }

            });
            //-----------页面设置对话框之外的点击----点击事件的监听-
            dcDesignerDivTopPageSettingBox.addEventListener('click', function () {
                //关闭新增元素对话框
                dcDesignerPageSettingFormDialog && (dcDesignerPageSettingFormDialog.style.display = 'none');
                dcDesignerDivTopPageSettingBox.style.display = 'none';
            });


            //------------新增元素按钮----点击事件的监听-------------
            var dcDesignerAddElementButton = designerDiv.querySelector('#dc_designer_add_element_button');
            var dcDesignerAddElementContent = designerDiv.querySelector('#dc_designer_add_element_content');
            var dcDesignerDivTopAddElementBox = designerDiv.querySelector('#dc_designerDiv_top_add_element_box');//遮罩层
            dcDesignerAddElementButton.addEventListener('click', function () {
                //展示新增元素对话框
                let dcDesAddIsShow = dcDesignerAddElementContent.style.display === "block";
                dcDesignerAddElementContent.style.display = dcDesAddIsShow ? 'none' : 'block';
                dcDesignerDivTopAddElementBox.style.display = dcDesAddIsShow ? 'none' : 'block';
            });

            //------------新增元素对话框中的点击----点击事件的监听-
            dcDesignerAddElementContent.addEventListener('click', function (e) {
                if (e && e.target && e.target.className) {
                    if (e.target.className.indexOf('dc_designer_add_element_item') === -1) {
                        console.log('不是新增元素选项');
                        return false;
                    }
                    let elementName = e.target.getAttribute('data-element');
                    let DesignerRootElement = that.GetDesignerRootElement();
                    let addElementResult = null;
                    switch (elementName) {
                        case 'HeaderLabel':
                            addElementResult = DesignerRootElement.AddHeaderLabel({
                                "Title": "眉栏",
                                "Value": "",
                            });
                            break;
                        case 'Label':
                            addElementResult = DesignerRootElement.AddLabel({
                                "Text": "标签",
                            });
                            console.log('新增文本标签属性');
                            break;
                        case 'GeneralItem':
                            addElementResult = DesignerRootElement.AddGeneralItem({
                                "Title": "一般项目",
                            });
                            console.log('新增一般项目属性');
                            break;
                        case 'SpecialItems':
                            addElementResult = DesignerRootElement.AddSpecialItems({
                                "Title": "特殊项目",
                            });
                            console.log('新增特殊项目属性');
                            break;
                        case 'YAxisInfos':
                            addElementResult = DesignerRootElement.AddYAxisInfos({
                                "Title": "体征项目",
                            });
                            console.log('新增体征项目属性');
                            break;
                        default:
                            addElementResult = null;
                            break;
                    }
                    if (addElementResult && addElementResult.UID) {
                        //刷新目录树
                        that.refreshDesignerTree(addElementResult.UID);
                        //滚动到对应的目录树位置
                        let CurrentDesignerElement = designerDiv.querySelector(`span[attr-id="${addElementResult.UID}"]`);
                        CurrentDesignerElement.scrollIntoView();
                        //将光标自动定位到title输入框中
                        let DCDESFormContent = designerDiv.querySelector('#dc_designer_options_content') || null;
                        if (DCDESFormContent) {
                            let selectTargetDom = (elementName === 'Label' ? '#dc_designer_options_content input[attr-option-name="Text"]' : '#dc_designer_options_content input[attr-option-name="Title"]');
                            let newElementTitleInput = DCDESFormContent.querySelector(selectTargetDom);
                            newElementTitleInput && newElementTitleInput.select && newElementTitleInput.select();
                            console.log('新增眉栏属性后光标Focus', document.querySelector('#dc_designer_options_content input[attr-option-name="Title"]'));

                        }
                    }
                    dcDesignerAddElementContent.style.display = 'none';
                    dcDesignerDivTopAddElementBox.style.display = 'none';

                }
            });
            //------------新增元素对话框之外的点击----点击事件的监听-
            dcDesignerDivTopAddElementBox.addEventListener('click', function () {
                //关闭新增元素对话框
                dcDesignerAddElementContent.style.display = 'none';
                dcDesignerDivTopAddElementBox.style.display = 'none';

            });

            //------------顺序前移----点击事件的监听-
            var dcDesignerMoveUpButton = designerDiv.querySelector('#dc_designer_move_up_button');
            dcDesignerMoveUpButton.addEventListener('click', function () {
                let DesignerRootElement = that.GetDesignerRootElement();
                //获取当前元素
                let CurrentDesignerElementID = that.GetDesignerCurrentElementID();
                if (CurrentDesignerElementID) {
                    let moveElementResult = DesignerRootElement.MoveProjectUpAndDown(CurrentDesignerElementID, -1);
                    //刷新目录树
                    moveElementResult && moveElementResult.UID && that.refreshDesignerTree(moveElementResult.UID);
                }
            });
            //------------顺序后移----点击事件的监听-------
            var dcDesignerMoveUpButton = designerDiv.querySelector('#dc_designer_move_down_button');
            dcDesignerMoveUpButton.addEventListener('click', function () {
                let DesignerRootElement = that.GetDesignerRootElement();
                //获取当前元素
                let CurrentDesignerElementID = that.GetDesignerCurrentElementID();
                if (CurrentDesignerElementID) {
                    let moveElementResult = DesignerRootElement.MoveProjectUpAndDown(CurrentDesignerElementID, 1);
                    //刷新目录树
                    moveElementResult && moveElementResult.UID && that.refreshDesignerTree(moveElementResult.UID);
                }

            });

            //------------删除----点击事件的监听-------
            var dcDesignerDeleteButton = designerDiv.querySelector('#dc_designer_delete_button');
            dcDesignerDeleteButton.addEventListener('click', function () {
                let DesignerRootElement = that.GetDesignerRootElement();
                //获取当前元素  
                let CurrentTreeNode = designerDiv.querySelector('.dc_designer_tree_item_title_text_active');
                //获取当前元素的类型
                let attrType = CurrentTreeNode.getAttribute('attr-type');
                //获取当前元素的id
                let CurrentAttrId = CurrentTreeNode.getAttribute('attr-id');
                if (CurrentAttrId && attrType) {
                    switch (attrType) {
                        case 'HeaderLabels':
                            DesignerRootElement.RemoveHeaderLabel(CurrentAttrId);
                            break;
                        case 'HeaderLines':
                            DesignerRootElement.RemoveGeneralItem(CurrentAttrId);
                            break;
                        case 'YAxisInfos':
                            DesignerRootElement.RemoveYAxisInfos(CurrentAttrId);
                            break;
                        case 'FooterLines':
                            DesignerRootElement.RemoveSpecialItems(CurrentAttrId);
                            break;
                        case 'Labels':
                            DesignerRootElement.RemoveLabel(CurrentAttrId);
                            break;
                        default:
                            break;
                    }
                    //刷新目录树
                    that.refreshDesignerTree();
                }
            });
            //------------导出----点击事件的监听-------
            var dcDesignerExportButton = designerDiv.querySelector('#dc_designer_export_button');
            dcDesignerExportButton && dcDesignerExportButton.addEventListener('click', function () {
                let DesignerRootElement = that.GetDesignerRootElement();
                DesignerRootElement.SaveTemperatureDocumentToFile();
            });

            //------------右侧属性----点击事件的监听-------
            var DesignerOptionsContent = designerDiv.querySelector('#dc_designer_options_content');
            DesignerOptionsContent.addEventListener('click', function (e) {
                if (e.target.className.indexOf('dc_designer_options_item_title_icon') >= 0) {
                    e.stopPropagation();
                    var isShow = e.target.getAttribute('attr-show');
                    isShow = (isShow === 'true');
                    var optionName = e.target.getAttribute('attr-option-name');
                    var optionChildrenList = designerDiv.querySelectorAll(`div.dc_designer_options_item_children[attr-option-name="${optionName}"]`);
                    console.log(optionChildrenList, '========optionChildrenList');
                    optionChildrenList.length > 0 && optionChildrenList.forEach(item => {
                        item.style.display = isShow ? 'none' : 'flex';
                        e.target.setAttribute('attr-show', isShow ? 'false' : 'true');
                        e.target.innerHTML = isShow ? '+' : '-';
                    });
                }
            });
            //------------------------事件监听end----------
        }
    },
    /**
    * 确定按钮
    */
    ConfirmDesigner: function () {
        let that = this;
        let isUnsave = that.GetDesignerFormPropUnsave();//获取是否带有未保存的修改
        if (isUnsave) {
            if (confirm('当前设计器有未保存的修改，是否确认保存？')) {
                console.log('应用之后再确认');
                that.ApplyDesignerProperty(false);//应用表单的修改并且不需要刷新左侧目录树
            }
        }

        var rootElementDesigner = that.GetDesignerRootElement();
        var documentConfig = rootElementDesigner.GetDocumentConfigProperties();//文档属性
        documentConfig = JSON.parse(JSON.stringify(documentConfig));//深拷贝文档属性
        var YAxisInfos = rootElementDesigner.GetYAxisInfosProperties();//体征项目属性
        var HeaderLabels = rootElementDesigner.GetHeaderLabelProperties();//眉栏属性
        var Labels = rootElementDesigner.GetLabelProperties();//文本标签属性
        var HeaderLines = rootElementDesigner.GetGeneralItemProperties();//一般项目属性
        var FooterLines = rootElementDesigner.GetSpecialItemsProperties();//页脚特殊属性

        var xml = rootElementDesigner.SaveTemperatureDocumentToString();//获取设计的xml文档
        console.log(documentConfig, '==========documentConfig');
        // var PageSettings = rootElementDesigner.GetPageSettings();//页面设置属性
        var rootElement = that.GetRootElement();//体温单编辑器
        if (rootElement) {
            var newTemperatureDocumentStructure = JSON.parse(JSON.stringify({ HeaderLabels, HeaderLines, YAxisInfos, FooterLines, Labels }));//深拷贝所有属性
            if (rootElement && rootElement.EventDesignerControlConfirm && typeof rootElement.EventDesignerControlConfirm === 'function') {
                rootElement.EventDesignerControlConfirm(documentConfig, newTemperatureDocumentStructure, xml);
            } else {
                var jsonData = rootElementDesigner.SaveTemperatureDocumentToString('json');//获取设计的xml文档
                rootElement.DesignerMode(true);
                //修改体温单数据,直接重新加载一遍文档的json数据
                rootElement.LoadTemperatureDocumentFromString(jsonData);
                rootElement.DesignerMode(false);
                var DesignerRootElementDialog = that.GetDesignerRootElementDialog();
                DesignerRootElementDialog && DesignerRootElementDialog.remove();
                document.TemperatureWriterControl = rootElement;//重置为原来的对象
            }

            //关闭设计器监听事件
            if (rootElement && rootElement.EventDesignerControlConfirmAfter && typeof rootElement.EventDesignerControlConfirmAfter === 'function') {
                rootElement.EventDesignerControlConfirmAfter(documentConfig);
            }
        }

    },
    /**
    * 取消按钮
    */
    CancelDesigner: function () {
        let that = this;
        var DesignerRootElementDialog = that.GetDesignerRootElementDialog();
        var templateRoot = this.GetRootElement();
        let isUnsave = that.GetDesignerFormPropUnsave();
        //有修改内容时提示用户
        if (isUnsave && confirm('当前设计器有未保存的修改，是否确认关闭？')) {
            console.log('确认之后在关闭');
            DesignerRootElementDialog && DesignerRootElementDialog.remove();
            document.TemperatureWriterControl = templateRoot;//重置为原来的对象
        } else {
            DesignerRootElementDialog && DesignerRootElementDialog.remove();
            document.TemperatureWriterControl = templateRoot;//重置为原来的对象
        }

    },
    /**
     * 更新左侧目录树选中状态
     */
    triggerDesignerTreeActive(uid = null) {
        if (!uid) { return false; }
        // console.log('触发左侧树状列表选中状态,默认是document', uid);
        let that = this;
        let DesignerRootElementDialog = that.GetDesignerRootElementDialog();
        //删除原有的选中样式
        var treeItemPrve = DesignerRootElementDialog.querySelector('.dc_designer_tree_item_title_text_active');
        treeItemPrve && treeItemPrve.classList.remove('dc_designer_tree_item_title_text_active');
        //设置新的选中样式
        var treeItem = DesignerRootElementDialog.querySelector(`.dc_designer_tree_item_title_text[attr-id=${uid}]`);
        treeItem && treeItem.classList.add('dc_designer_tree_item_title_text_active');
        that.CreatDesignerPropertyDom(uid);//获取属性值并渲染右侧列表
    },
    /**
     * 渲染导航节点树
     * defaultUid: 选中节点的uid,用于实时刷新左侧列表并重新选中节点
     */
    refreshDesignerTree: function (defaultUid = null) {
        console.log('渲染导航节点树');
        let that = this;
        //获取体温单编辑器
        var rootElementDesigner = that.GetDesignerRootElement();
        var rootElementDesignerDialog = that.GetDesignerRootElementDialog();
        //获取体温单目录导航、并渲染目录树
        var TemperatureDocumentStructure = rootElementDesigner.GetTemperatureDocumentStructure();
        //创建节点树后重新选中节点
        var DesignerTree = rootElementDesignerDialog.querySelector('#dc_designer_tree');
        // DesignerTree.innerHTML = '<span style="color:grey;text-align:center;margin-top:20px;">加载中...</span>';

        //创建树节点
        var nodeName = ``;
        that.DesignerListSortData.forEach(item => {
            var itemData = TemperatureDocumentStructure[item];
            if (itemData && itemData.length > 0) {
                var itemDataChildren = ``;
                itemData.forEach(file => {
                    //重新获取下标签
                    if (item === 'Labels') {
                        file = rootElementDesigner.GetLabelProperties(file.UID);
                    }
                    itemDataChildren += `
                    <span attr-id="${file.UID}" attr-type="${item}" style="display: block;" class="dc_designer_tree_item_title_text">${file.Title || file.Text}</span>
                    `;
                });
            }
            nodeName += `<div class="dc_${item}_box">
                <div class="dc_${item}_title dc_designer_tree_item_title_box"> 
                 <span class="dc_designer_tree_item_title_icon" attr-show="true"></span>
                    <span class="dc_designer_tree_item_title_span" >${that.DesignerListSortDataZh[item]}</span>
                 </div>
                <div class="dc_${item}_children_box dc_designer_tree_item" attr-type="${item}">${itemDataChildren}</div>
            </div>
            `;
        });
        DesignerTree.innerHTML = `<div class="dc_DocumentConfig_box">
                <div class="dc_DocumentConfig_title dc_designer_tree_item_title_box">
                    <span class="dc_designer_tree_item_title_icon" attr-show="true" id="dc_designer_tree_document_icon"></span>
                    <span class="dc_designer_tree_item_title_text "  attr-id="document" attr-type="document">全局属性</span>
                </div>
                <div class="dc_DocumentConfig_children_box dc_designer_tree_item ">${nodeName}</div>
            </div>
            `;

        that.triggerDesignerTreeActive(defaultUid || 'document');//默认展开文档属性
    },
    /**
    * 获取属性对象，并渲染到右侧属性面板
    * @param {Object} rootElement 设计器div的根元素
    * @param {String} attrId 属性ID
    */
    CreatDesignerPropertyDom: function (attrId = null) {
        var that = this;

        var designerDiv = document.querySelector('#dc_designerDiv20240612160000');
        var DesignerOptionsContent = designerDiv.querySelector('#dc_designer_options_content');
        DesignerOptionsContent.innerHTML = '';

        //当前数据的属性类型
        var attrType = attrId === 'document' ? 'document' : designerDiv.querySelector(`span[attr-id="${attrId}"]`).getAttribute('attr-type');

        if (attrId === null || attrType === null) {
            that.TemperatureCurrentElementOldOptions = null;//记录旧的属性值
            return;
        }

        var rootElementDesigner = that.GetDesignerRootElement();//设计器对象
        //定位文档位置
        rootElementDesigner.LocationStructure(attrId === 'document' ? null : attrId);
        //获取属性值渲染页面
        var prop = (attrId === 'document' ? rootElementDesigner.GetDocumentConfigProperties() : rootElementDesigner.GetInternalProperties(attrId, attrType));

        that.TemperatureCurrentElementOldOptions = JSON.parse(JSON.stringify(prop));//记录旧的属性值
        if (prop && Object.keys(prop).length > 0) {
            var propKeys = Object.keys(prop);
            propKeys.forEach(key => {
                //值
                var propValue = JSON.parse(JSON.stringify(prop[key]));
                //表单元素的html,默认text
                var formInput = `<input  ${key === "UID" || key === "Index" ? "disabled" : ''} attr-option-name="${key}" class="dc_designer_options_item_value_input"  type="text" value="${propValue || ''}"></input>`;
                // if (typeof propValue === "boolean" || propValue === "true" || propValue === "false") {//布尔值

                // }
                //其他类型的指定表单html
                if (DCDesignerDocumentObject[attrType] && DCDesignerDocumentObject[attrType][key] && DCDesignerDocumentObject[attrType][key].type) {
                    //指定的表单类型
                    let inputType = DCDesignerDocumentObject[attrType][key].type;
                    //其他指定类型
                    switch (inputType) {
                        case 'checkbox':
                            formInput = `<input attr-option-name="${key}" class="dc_designer_options_item_value_input" type="checkbox" ${propValue ? 'checked' : ''}></input>`;
                            break;
                        case 'color':
                            if (/^#?([0-9A-F]{3}|[0-9A-F]{6})$/i.test(propValue)) {
                                //颜色值
                                formInput = `<input attr-option-name="${key}" class="dc_designer_options_item_value_input"  type="color" value="${propValue}"></input>`;
                            } else {
                                formInput = `<input   attr-option-name="${key}" class="dc_designer_options_item_value_input"  type="text" value="${propValue || ''}"></input>`;
                            }
                            break;
                        case 'number':
                            var numberValue = propValue;
                            if (!isNaN(propValue)) {
                                numberValue = parseFloat(propValue);
                            }
                            formInput = `<input attr-option-name="${key}" class="dc_designer_options_item_value_input"  type="number" value="${propValue}"></input>`;;
                            break;
                        case 'select':
                            formInput = `<select attr-option-name="${key}" class="dc_designer_options_item_value_input">${DCDesignerDocumentObject[attrType][key].options.map(item => `<option value="${item}" ${item === propValue ? 'selected' : ''}>${item}</option>`).join('')}</select>`;
                            break;
                        case 'array':
                            if (!propValue || propValue.length === 0) {
                                return;
                            }
                            formInput = `<div class="dc_designer_options_item_object_box" attr-option-html-type="array"  attr-option-name="${key}"></div>`;
                            break;
                        case 'object':
                            if (!propValue || Object.keys(propValue).length === 0) {
                                return;
                            }
                            formInput = `<div class="dc_designer_options_item_object_box" attr-option-html-type="object" attr-option-name="${key}"></div>`;
                            break;
                        case 'boolean':
                            propValue = (propValue === true || propValue === "true") ? true : false;
                            formInput = `<input attr-option-name="${key}" class="dc_designer_options_item_value_input"  type="checkbox" ${propValue ? 'checked' : ''}></input>`;
                            break;
                        case "none":
                            //不需要展示的，直接进入下一个循环
                            return;
                            break;
                    }
                }


                DesignerOptionsContent.innerHTML += `
                <div class="dc_designer_options_item">
                    <div class="dc_designer_options_item_title" title="${that.DCDesignerDocumentObjectCN[key]}&nbsp;&nbsp;${key}">
                        <span><span class="dc_designer_options_item_title_text">${that.DCDesignerDocumentObjectCN[key]}</span>&nbsp;&nbsp;${key}</span>  
                    </div>
                    <div class="dc_designer_options_item_value">
                     ${formInput}
                    </div>
                </div>
                `;
            });

            var optionValueObject = DesignerOptionsContent.querySelectorAll('.dc_designer_options_item_object_box');


            optionValueObject.forEach((item, i) => {
                var showKey = [];

                var optionName = item.getAttribute('attr-option-name');
                var optionType = item.getAttribute('attr-option-html-type');
                var optionValue = prop[optionName];

                if (DCDesignerDocumentObject[attrType] && DCDesignerDocumentObject[attrType][optionName] && DCDesignerDocumentObject[attrType][optionName].showKey) {
                    //存在指定要展示的值时
                    showKey = DCDesignerDocumentObject[attrType][optionName].showKey;
                }


                if (optionType === 'array' && Array.isArray(optionValue) && optionValue.length > 0) {
                    // 当值为数组时(Scales)
                    var optionHtml = "";
                    optionValue.forEach((option, index) => {
                        //需要展示的值
                        showKey = showKey.length ? showKey : Object.keys(option);
                        optionHtml += `<div class="dc_designer_options_array_option_value">
                            <div  style="width:50px;" class="dc_designer_options_array_option_value_item dc_designer_options_array_option_value_item_index">${index}</div>
                            ${showKey.map(subKey => {
                            var subValue = option[subKey];
                            return `<div style="flex:1;" class="dc_designer_options_array_option_value_item">     
                                            <input attr-option-name="${subKey}"  attr-option-parent-name=${optionName}  attr-option-index=${index} class="dc_designer_options_item_value_input"  type="text" value="${subValue}"></input>
                                        </div>`;
                        }).join('')}
                        </div>`;
                    });
                    var childrenBox = document.createElement('div');

                    childrenBox.setAttribute('class', 'dc_designer_options_item_array_box');
                    childrenBox.setAttribute('optionName', optionName);
                    childrenBox.setAttribute('optionType', "array");
                    childrenBox.innerHTML = `
                       <div class="dc_designer_options_array_item" attr-option-name="${optionName}_${i}">
                            <div class="dc_designer_options_array_option_title">
                                <div style="width:50px;" class="dc_designer_options_array_option_title_item dc_designer_options_array_option_title_item_index"></div>
                                ${showKey.map(subKey => {
                        return `<div style="flex:1;" class="dc_designer_options_array_option_title_item">${subKey}</div>`;
                    }).join('')}
                    </div>
                        </div>${optionHtml}
                    ` ;
                    //插入到父级元素中
                    item.parentNode.parentNode.parentNode.insertBefore(childrenBox, item.parentNode.parentNode.nextSibling);
                } else if (optionType === 'object' && typeof optionValue === 'object' && Object.keys(optionValue).length > 0) {
                    // 当值为对象时(pageSetting)
                    showKey = showKey.length ? showKey : Object.keys(optionValue);

                    var optionHtml = "";
                    showKey.forEach(subKey => {
                        optionHtml += `
                            <div class="dc_designer_options_object_item">
                                <div class="dc_designer_options_object_item_title">
                                    <span><span class="dc_designer_options_item_title_text">${that.DCDesignerDocumentObjectCN[subKey]}</span>&nbsp;&nbsp;${subKey}</span> 
                                </div>
                                <div class="dc_designer_options_object_item_value">
                                    <input attr-option-name="${subKey}" attr-option-parent-name="${optionName}" class="dc_designer_options_item_value_input"  type="text" value="${optionValue[subKey]}"></input>
                                </div>
                            </div>`;
                    });

                    var childrenBox = document.createElement('div');
                    childrenBox.setAttribute('class', 'dc_designer_options_item_array_box');
                    childrenBox.setAttribute('optionName', optionName);
                    childrenBox.setAttribute('optionType', "object");
                    childrenBox.innerHTML = optionHtml;
                    item.parentNode.parentNode.parentNode.insertBefore(childrenBox, item.parentNode.parentNode.nextSibling);

                }
            });
        }
    },
    /**
   * 获取属性对象，并渲染到右侧属性面板
   * @param {string} isSave 是否增加询问对话框
   */
    ApplyDesignerProperty: function (isRefreshTree = true) {
        let that = this;
        var designerDiv = that.GetDesignerRootElementDialog();//对话框对象
        //获取当前选中的节点
        var currentTreeNode = designerDiv.querySelector('span.dc_designer_tree_item_title_text.dc_designer_tree_item_title_text_active');
        if (currentTreeNode === null) {
            console.log('未选中任何节点');
            return;
        }
        let currentDesignerFormProp = that.GetDesignerFormProp();//获取右侧的表单数据
        if (currentDesignerFormProp && Object.keys(currentDesignerFormProp).length > 0) {
            var rootElementDesigner = that.GetDesignerRootElement();//设计器对象
            var DesignerCurrentElementID = that.GetDesignerCurrentElementID();
            var DesignerCurrentElementTYPE = currentTreeNode.getAttribute('attr-type');
            var uid = DesignerCurrentElementID === 'document' ? null : DesignerCurrentElementID;
            switch (DesignerCurrentElementTYPE) {
                case 'document':
                    rootElementDesigner.SetDocumentConfigProperties(currentDesignerFormProp);
                    break;
                case 'HeaderLabels':
                    uid && rootElementDesigner.SetHeaderLabelProperties(uid, currentDesignerFormProp);
                    break;
                case 'HeaderLines':
                    uid && rootElementDesigner.SetGeneralItemProperties(uid, currentDesignerFormProp);
                    break;
                case 'YAxisInfos':
                    uid && rootElementDesigner.SetYAxisInfosProperties(uid, currentDesignerFormProp);
                    break;
                case 'FooterLines':
                    uid && rootElementDesigner.SetSpecialItemsProperties(uid, currentDesignerFormProp);
                    break;
                case 'Labels':
                    uid && rootElementDesigner.SetLabelProperties(uid, currentDesignerFormProp);
                    break;
                default:
                    break;
            }
            isRefreshTree && that.refreshDesignerTree(DesignerCurrentElementID);//同时更新左侧树形列表
        }
    },
    /**
     * 获取当前设计器选中的元素
    */
    GetDesignerCurrentElementID: function () {
        let that = this;
        //整个设计器对话框
        var designerDiv = that.GetDesignerRootElementDialog();
        //当前选中的节点
        let CurrentTreeNode = designerDiv && designerDiv.querySelector && designerDiv.querySelector('.dc_designer_tree_item_title_text_active');
        let CurrentAttrId = CurrentTreeNode && CurrentTreeNode.getAttribute && CurrentTreeNode.getAttribute('attr-id');
        if (CurrentAttrId) {
            return CurrentAttrId;
        }
        return null;
    },
    /**
     * 获取设计器的右侧表单属性值
    */
    GetDesignerFormProp: function () {
        let that = this;
        let CurrentDesignerElementID = that.GetDesignerCurrentElementID();
        if (CurrentDesignerElementID) {
            var designerDiv = that.GetDesignerRootElementDialog();
            //整理数据为一个扁平对象
            var changeOptions = {};
            //先获取第一层数据
            var inputList = designerDiv.querySelectorAll('.dc_designer_options_item_value > .dc_designer_options_item_value_input');
            inputList && inputList.forEach(item => {
                var itemNodeName = item.nodeName;
                var optionName = item.getAttribute('attr-option-name');
                switch (itemNodeName) {
                    case 'INPUT':
                        if (item.type === 'checkbox') {
                            changeOptions[optionName] = item.checked;
                        } else if (item.type === 'number') {
                            changeOptions[optionName] = parseFloat(item.value);
                        } else {
                            changeOptions[optionName] = item.value;
                        }
                        break;
                    case 'SELECT':
                        changeOptions[optionName] = item.value;
                        break;
                }

            });

            var optionValueObject = designerDiv.querySelectorAll('.dc_designer_options_item_array_box');
            optionValueObject.forEach(item => {
                var optionName = item.getAttribute('optionName');
                var optionType = item.getAttribute('optionType');

                if (optionType === 'array') {
                    changeOptions[optionName] = [];
                    var optionValueArrayDom = item.querySelectorAll('.dc_designer_options_array_option_value');
                    optionValueArrayDom && optionValueArrayDom.forEach(optionValue => {
                        var optionValueItem = {};
                        var itemArrDom = optionValue.querySelectorAll('.dc_designer_options_item_value_input');
                        itemArrDom && itemArrDom.forEach(itemArr => {
                            var optionName = itemArr.getAttribute('attr-option-name');
                            optionValueItem[optionName] = itemArr.value;
                        });
                        changeOptions[optionName].push(optionValueItem);

                    });
                } else if (optionType === 'object') {
                    var optionValueObject = {};
                    var allValueInput = item.querySelectorAll('.dc_designer_options_item_value_input');
                    allValueInput && allValueInput.forEach(itemArr => {
                        var optionName = itemArr.getAttribute('attr-option-name');
                        var optionValue = itemArr.value;
                        if (!isNaN(optionValue)) {
                            optionValue = parseFloat(optionValue);
                        } else if (optionValue === 'true' || optionValue === 'false') {
                            optionValue = optionValue === 'true';
                        }
                        optionValueObject[optionName] = optionValue;
                    });
                    changeOptions[optionName] = optionValueObject;
                }
            });
            return JSON.parse(JSON.stringify(changeOptions));
        }
        return null;
    },
    /**
     * 获取当前表单是否存在修改后未保存的内容
     */
    GetDesignerFormPropUnsave: function () {
        let that = this;
        var designerDiv = that.GetDesignerRootElementDialog();
        //获取右侧的表单数据
        var formCurrentDesignerElementProp = that.GetDesignerFormProp();
        var rootElementDesigner = that.GetDesignerRootElement();//设计器对象
        //获取当前选中的节点
        var currentTreeNode = designerDiv.querySelector('span.dc_designer_tree_item_title_text_active');
        if (currentTreeNode) {
            var isChange = false;
            //当前修改的节点的关键属性
            var attrId = currentTreeNode.getAttribute('attr-id');
            var prop = (attrId === 'document' ? rootElementDesigner.GetDocumentConfigProperties() : rootElementDesigner.GetInternalProperties(attrId));
            if (formCurrentDesignerElementProp && Object.keys(formCurrentDesignerElementProp).length > 0) {
                //判断是否有修改
                Object.keys(formCurrentDesignerElementProp).forEach(changeKey => {
                    //判断是否有修改
                    if (JSON.stringify({ changeKey: formCurrentDesignerElementProp[changeKey] }) !== JSON.stringify({ changeKey: prop[changeKey] })) {
                        isChange = true;
                        return;
                    }
                });
            }
            if (isChange) {
                console.log('有修改未保存');
            } else {
                console.log('未修改');
            }
            return isChange; //有修改
        }
        return false; //未修改

    },

    /**
    * 获取体温单rootElement对象
    */
    GetRootElement: function () {
        var that = this;
        var rootElement = DCTools20221228.GetOwnerWriterControl(that.TemperatureControlID);
        return rootElement;
    },

    /**
    * 获取设计器整个div的根元素
    */
    GetDesignerRootElementDialog: function () {
        var that = this;
        var rootElement = that.GetRootElement();
        var DesignerRootElementDialog = rootElement.querySelector('#dc_designerDiv20240612160000_mask');
        return DesignerRootElementDialog;
    },
    /**
    * 获取设计器rootElement对象
    */
    GetDesignerRootElement: function () {
        var that = this;
        var rootElement = that.GetRootElement();
        var DesignerRootElement = rootElement.querySelector('#designerDiv20240612160000_myWriterControl');
        return DesignerRootElement;
    },
    /**
        * 获取右侧属性面板的数据类型对应的元素
        * 
        */
    // GetRightPropertyDom: function (optionItem) { 
    //     var that = this;
    //     var optionItemType = typeof optionItem;
    //     switch (optionItem) { 
    //         case 'Number':
    //     }
    // },

    /**
     * 静态数据，设计器左侧树形列表的排列顺序
     */
    DesignerListSortData: ['HeaderLabels', 'HeaderLines', 'YAxisInfos', 'FooterLines', 'Labels'],
    /**
     * 静态数据，设计器左侧树形列表的排列顺序中文名
     */
    DesignerListSortDataZh: {
        'HeaderLabels': '眉栏',
        'HeaderLines': '一般项目',
        'YAxisInfos': '体征项目',
        'FooterLines': '特殊项目',
        'Labels': '文本标签'
    },
    /**
    * 简单的文档静态数据，用于中文展示
    */
    DCDesignerDocumentObjectCN: {
        "SpecifyStartDate": "指定开始日期",
        "SpecifyEndDate": "指定结束日期",
        "GridYSplitNum": "网格Y分割数",
        "GridYSpaceNum": "网格Y间隔数",
        "Title": "标题",
        "SpecifyTitleHeight": "指定标题高度",
        "NumOfDaysInOnePage": "一页中的天数",
        "BigVerticalGridLineColorValue": "大垂直网格线颜色值",
        "BigVerticalGridLineWidth": "大垂直网格线宽度",
        "GridLineColorValue": "网格线颜色值",
        "FontName": "字体名称",
        "FontSize": "字体大小",
        "DateFormatString": "日期格式字符串",
        "DateFormatStringForCrossMonth": "跨月日期格式字符串",
        "DateFormatStringForCrossWeek": "跨周日期格式字符串",
        "DateFormatStringForCrossYear": "跨年日期格式字符串",
        "DateFormatStringForFirstIndexFirstPage": "第一页第一个索引日期格式字符串",
        "DateFormatStringForFirstIndexOtherPage": "其他页第一个索引日期格式字符串",
        "PageIndexText": "页索引文本",
        "EnableDataGridLinearAxisMode": "启用数据网格线性轴模式",
        "DataGridBottomPadding": "数据网格底部填充",
        "DataGridTopPadding": "数据网格顶部填充",
        "BigTitleFontSize": "大标题字体大小",
        "FooterDescription": "页脚描述",
        "ThickLineWidth": "粗线宽度",
        "ThinLineWidth": "细线宽度",
        "TickTexts": "刻度文本",
        "TickValues": "刻度值",
        "TickColorValues": "刻度颜色值",
        "PageSettings": "页面设置",
        "PaperSizeName": "纸张尺寸名称",
        "PaperHeight": "纸张高度",
        "PaperWidth": "纸张宽度",
        "Landscape": "横向",
        "TopMargin": "顶部边距",
        "BottomMargin": "底部边距",
        "LeftMargin": "左边距",
        "RightMargin": "右边距",
        "Unit": "单位",
        "SeperatorChar": "分隔符字符",
        "ParameterName": "参数名称",
        "Value": "数值",
        "GroupIndex": "组索引",
        "UID": "UID",
        "Name": "名称",
        "StartDateKeyword": "开始日期关键词",
        "EndDateKeyword": "结束日期关键词",
        "AfterOperaDaysFromZero": "从零开始的操作天数",
        "AfterOperaDaysBeginOne": "从一开始的操作天数",
        "PreserveStartKeywordOrder": "保留开始关键词顺序",
        "LayoutType": "布局类型",
        "TickStep": "刻度步长",
        "TickLineVisible": "刻度线可见",
        "ValueType": "数值类型",
        "SpecifyHeight": "指定高度",
        "AutoHeight": "自动高度",
        "TextFontName": "文本字体名称",
        "TextFontSize": "文本字体大小",
        "LoopTextList": "循环文本列表",
        "UpAndDownTextType": "上下文本类型",
        "TitleAlign": "标题对齐",
        "SpecifyTitleWidth": "指定标题宽度",
        "PageTitleTexts": "页面标题文本",
        "BlankDateWhenNoData": "无数据时空白日期",
        "TextColorValue": "文本颜色值",
        "AllowOutofRange": "允许超出范围",
        "EnableLanternValue": "启用灯笼值",
        "MaxValue": "最大值",
        "MinValue": "最小值",
        "SymbolColorValue": "符号颜色值",
        "SymbolStyle": "符号样式",
        "YSplitNum": "Y轴分割数",
        "Style": "样式",
        "SymbolSize": "符号大小",
        "TitleColorValue": "标题颜色值",
        "ShadowName": "阴影名称",
        "CharSymbol": "字符符号",
        "CharLantern": "字符灯笼",
        "TitleVisible": "标题可见",
        "ShowLegendInRule": "在标尺中显示图例",
        "BottomTitle": "底部标题",
        "RedLineValue": "红线数值",
        "AlertLineColorValue": "警戒线颜色值",
        "VerticalLine": "垂直线",
        "ShadowPointVisible": "阴影点可见",
        "AllowInterrupt": "允许中断",
        "HollowCovertTargetName": "空心转换目标名称",
        "LanternValueColorForUpValue": "上涨值的灯笼值颜色",
        "LanternValueColorForDownValue": "下跌值的灯笼值颜色",
        "TopPadding": "顶部填充",
        "BottomPadding": "底部填充",
        "MergeIntoLeft": "合并到左侧",
        "LineStyleForLanternValue": "灯笼值的线条样式",
        "ValueTextBackColorValue": "值文本背景颜色值",
        "ShowPointValue": "显示点值",
        "Visible": "可见",
        "Index": "索引",
        "thisYInfoHeight": "当前Y轴信息高度",
        "YTopHeight": "Y轴顶部高度",
        "YBottomHeight": "Y轴底部高度",
        "Left": "左边距",
        "Top": "顶部距离",
        "Width": "宽度",
        "Height": "高度",
        "Text": "文本",
        "MultiLine": "多行",
        "ShowBorder": "显示边框",
        "BackColorValue": "背景颜色值",
        "TextFontBold": "文本加粗",
        "TextFontItalic": "文本斜体",
        "TextFontUnderline": "文本下划线",
        "ImageDataBase64String": "图像Base64字符串",
        "Alignment": "水平对齐",
        "LineAlignment": "垂直对齐",
        "PositionFixModeForAutoHeightLine": "自动高度线的位置修正模式",
        "LineWidth": "线宽",
        "BorderVisible": "边框可见",
        "YTopHeight": "Y轴顶部高度",
        "YBottomHeight": "Y轴底部高度",
        "ShadowStyle": "阴影样式",
        "Scales": "刻度",
        "TickTextAlignment": "刻度文本对齐方式",
        "ValueUnderLine": "值下划线",
        "ScaleRate": "刻度比例",
        "ColorValueForPointValue": "显示数据点的值",
        "ColorValueForDownValue": "下跌值的颜色值",
        "ColorValueForUpValue": "上涨值的颜色值",
        "BigTitleFontBold": "大标题加粗",
        "BigTitleFontName": "大标题字体名称",
        "ExtendGridLineType": "扩展网格线类型"
    }
};
// 记录特殊的设计器属性
var DCDesignerDocumentObject = {
    document: {
        BigVerticalGridLineColorValue: {
            type: 'color',
        },
        GridLineColorValue: {
            type: 'color',
        },
        PageSettings: {
            type: 'object',
            showKey: ["PaperHeight", "PaperWidth", "Landscape", "TopMargin", "BottomMargin", "LeftMargin", "RightMargin"]
        },
        TagString: {
            type: 'none'
        },
        GridYSplitInfo: {
            type: "none"
        },
        PaperHeight: {
            type: "number"
        },
        PaperWidth: {
            type: "number"
        },
        TopMargin: {
            type: "number"
        },
        BottomMargin: {
            type: "number"
        },
        LeftMargin: {
            type: "number"
        },
        RightMargin: {
            type: "number"
        },
        Landscape: {
            type: "boolean"
        },
        GridYSpaceNum: {
            type: "number"
        },
        GridYSplitNum: {
            type: "number"
        },
        NumOfDaysInOnePage: {
            type: "number"
        },
        SpecifyTitleHeight: {
            type: "number"
        },
        BigVerticalGridLineWidth: {
            type: "number"
        },
        FontSize: {
            type: "number"
        },
        DataGridBottomPadding: {
            type: "number"
        },
        DataGridTopPadding: {
            type: "number"
        },
        BigTitleFontSize: {
            type: "number"
        },
        ThinLineWidth: {
            type: "number"
        },
        ThickLineWidth: {
            type: "number"
        },
        BigTitleFontBold: {
            type: "boolean"
        },
        EnableDataGridLinearAxisMode: {
            type: "boolean"
        }
    },
    YAxisInfos: {
        SymbolColorValue: {
            type: 'color',
        },
        SymbolStyle: {
            type: 'select',
            options: ['SolidCicle', 'HollowCicle', 'Cross', 'SolidTriangle', 'SolidTriangleReversed']
            // : ["SolidCicle",
            //     "Cross",
            //     // "RoundDotIcon",
            //     "SolidTriangle",
            //     "HollowSolidTriangle",
            //     "SolidTriangleReversed",
            //     "HollowSolidTriangleReversed",
            //     "Square",
            //     "circular",
            //     "HollowWithCircularDots",
            //     "DoubleConcentricCircles"]
        },
        LanternValueColorForUpValue: {
            type: 'color',
        },
        LanternValueColorForDownValue: {
            type: 'color',
        },
        AlertLineColorValue: {
            type: 'color',
        },
        LineStyleForLanternValue: {
            type: 'select',
            options: ['Solid', 'Dash', 'Dot', 'DashDot', 'DashDotDot']
        },
        ValueTextBackColorValue: {
            type: 'color',
        },
        ShadowStyle: {
            type: 'select',
            options: ["LeftSlant", "RightSlant", "Vertical"]
        },
        TickTextAlignment: {
            type: 'select',
            options: ["Center", "Near", "Far"]
        },
        Scales: {
            type: 'array',
            showKey: ["ScaleRate", "Value"]
        },
        Style: {
            type: 'select',
            options: ["Value", "Text"],
        },
        thisYInfoHeight: {
            type: "none"
        },
        YTopHeight: {
            type: "none"
        },
        YBottomHeight: {
            type: "none"
        },
        Index: {
            type: "none"
        },
        AbNormalRangeSettings: {
            type: 'none'
        },
        MaxValue: {
            type: "number"
        },
        MinValue: {
            type: "number"
        },
        YSplitNum: {
            type: "number"
        },
        SymbolSize: {
            type: "number"
        },
        RedLineValue: {
            type: "number"
        },
        TopPadding: {
            type: "number"
        },
        BottomPadding: {
            type: "number"
        },
        SpecifyTitleWidth: {
            type: "number"
        },
        AllowInterrupt: {
            type: "boolean"
        },
        AllowOutofRange: {
            type: "boolean"
        },
        BorderVisible: {
            type: "boolean"
        },
        EnableLanternValue: {
            type: "boolean"
        },
        MergeIntoLeft: {
            type: "boolean"
        },
        ShadowPointVisible: {
            type: "boolean"
        },
        ShowLegendInRule: {
            type: "boolean"
        },
        ShowPointValue: {
            type: "boolean"
        },
        TitleVisible: {
            type: "boolean"
        },
        VerticalLine: {
            type: "boolean"
        },
        Visible: {
            type: "boolean"
        },

    },
    HeaderLabels: {
        GroupIndex: {
            type: "number"
        },
        ValueUnderLine: {
            type: "boolean"
        }
    },
    HeaderLines: {
        LayoutType: {
            type: 'select',
            options: ["Normal", "Free", "FreeText", "Cascade", "HorizCascade", "AutoCascade", "Slant", "Slant2", "Slant3", "Fraction"]
        },
        ValueType: {
            type: 'select',
            options: ["SerialDate", "TickText", "Data", "Text", "HourTick", "DayIndex", "SerialDate", "GlobalDayIndex", "NewSerialDate"]
        },
        UpAndDownTextType: {
            type: 'select',
            options: ["None", "ShowByTick", "ShowByText"]
        },
        TitleAlign: {
            type: 'select',
            options: ["Near", "Far", "Center"]
        },
        ExtendGridLineType: {
            type: 'select',
            options: ["Below", "Above", "None"]
        },
        TextColorValue: {
            type: 'color',
        },
        TickStep: {
            type: "number",
        },
        SpecifyHeight: {
            type: "number",
        },
        TextFontSize: {
            type: "number",
        },
        SpecifyTitleWidth: {
            type: "number",
        },
        TickLineVisible: {
            type: "boolean"
        },
        AutoHeight: {
            type: "boolean"
        },
        AfterOperaDaysFromZero: {
            type: "boolean"
        },
        AfterOperaDaysBeginOne: {
            type: "boolean"
        },
        PreserveStartKeywordOrder: {
            type: "boolean"
        },
        BlankDateWhenNoData: {
            type: "boolean"
        }
    },
    FooterLines: {
        LayoutType: {
            type: 'select',
            options: ["Normal", "Free", "FreeText", "Cascade", "HorizCascade", "AutoCascade", "Slant", "Slant2", "Slant3", "Fraction"]
        },
        ValueType: {
            type: 'select',
            options: ["SerialDate", "TickText", "Data", "Text", "HourTick", "DayIndex", "SerialDate", "GlobalDayIndex", "NewSerialDate"]
        },
        UpAndDownTextType: {
            type: 'select',
            options: ["None", "ShowByTick", "ShowByText"]
        },
        TitleAlign: {
            type: 'select',
            options: ["Near", "Far", "Center"]
        },
        ExtendGridLineType: {
            type: 'select',
            options: ["Below", "Above", "None"]
        },
        TextColorValue: {
            type: 'color',
        },
        TickStep: {
            type: "number",
        },
        SpecifyHeight: {
            type: "number",
        },
        TextFontSize: {
            type: "number",
        },
        SpecifyTitleWidth: {
            type: "number",
        },
        TickLineVisible: {
            type: "boolean"
        },
        AutoHeight: {
            type: "boolean"
        },
        AfterOperaDaysFromZero: {
            type: "boolean"
        },
        AfterOperaDaysBeginOne: {
            type: "boolean"
        },
        PreserveStartKeywordOrder: {
            type: "boolean"
        },
        BlankDateWhenNoData: {
            type: "boolean"
        }
    },
    Labels: {
        BackColorValue: {
            type: 'color',
        },
        TextColorValue: {
            type: 'color',
        },
        LineAlignment: {
            type: 'select',
            options: ["Near", "Far", "Center"]
        },
        Alignment: {
            type: 'select',
            options: ["Near", "Far", "Center"]
        },
        PositionFixModeForAutoHeightLine: {
            type: "none"
        },
        Left: {
            type: "number"
        },
        Top: {
            type: "number"
        },
        Width: {
            type: "number"
        },
        Height: {
            type: "number"
        },
        TextFontSize: {
            type: "number"
        },
        MultiLine: {
            type: "boolean"
        },
        ShowBorder: {
            type: "boolean"
        },
        TextFontBold: {
            type: "boolean"
        },
        TextFontItalic: {
            type: "boolean"
        },
        TextFontUnderline: {
            type: "boolean"
        }
    }
};
