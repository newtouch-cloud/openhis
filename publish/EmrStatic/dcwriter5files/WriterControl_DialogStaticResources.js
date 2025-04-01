//*************************************************************************
//* 项目名称：
//* 当前版本: V 5.3.1
//* 开始时间: 20230703
//* 开发者:李新宇
//* 重要描述:WriterControl_Dialog对话框的静态资源和css样式
//*************************************************************************
//* 最后更新时间:2023-7-3 15:23
//* 最后更新人:李新宇
//*************************************************************************

//对话框的样式
let DIALOGSTYLE = `#dc_dialogMark {
    width: 100%;
    height: 100%;
    background-color: #000000;
    position: relative;
    z-index: 10000;
    top: -100%;
    opacity: 0.2;
}

button,
input,
select,
textarea {
    line-height: normal;
    border:1px solid #333333;
    box-sizing: border-box;
}
button[disabled],
input[disabled],
select[disabled],
textarea[disabled]{
    background-color: #f5f5f5;
    color: #999;
    cursor: not-allowed;
}

#dc_dialogContainer,
#dc_dialogContainer1,
#dc_dialogContainer2 {
    display: block;
    width: 400px;
    overflow: hidden;
    border-radius: 6px;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 10001;
    user-select: none;
    box-shadow: 0 1px 6px rgba(0, 0, 0, .15);
    background: #fff;
    box-sizing: border-box;
    font-size: 12px;
}

#dc_dialogContainer *,
#dc_dialogContainer ::after,
#dc_dialogContainer ::before {
    box-sizing: border-box;
    margin: 0;
    outline: none;
}

#dc_dialogContainer [type=button],
#dc_dialogContainer [type=reset],
#dc_dialogContainer [type=submit],
#dc_dialogContainer button {
    -webkit-appearance: button;
    border-width: 1px;
}

#dc_dialogContainer table {
    width: 96%;
    margin-left: 2%;
    border-collapse: collapse;
    table-layout: fixed;
}

#dc_dialogContainer td,
#dc_dialogContainer th {
    word-wrap: break-word;
    text-align: center;
    font-size: 12px;
    border: 1px solid black;
}

#dc_dialogContainer textarea {
    border: 1px solid #ccc;
    resize: none;
}

/* 对话框标头部分 */
#dc_dialogContainer #dcPanelHeader {
    background: linear-gradient(to bottom, #ffffff 0, #eeeeee 100%);
    padding: 6px;
    position: relative;
    border-bottom: 1px solid #c6c6c6;
}

#dc_dialogContainer .dcHeader-title {
    font-size: 12px;
    font-weight: bold;
    color: #0E2D5F;
    height: 16px;
    line-height: 16px;
    padding-left: 18px
}

#dc_dialogContainer .dcHeader-tool {
    right: 5px;
    width: auto;
    position: absolute;
    top: 50%;
    margin-top: -8px;
    height: 16px;
}

#dc_dialogContainer .dcHeader-tool a {
    display: inline-block;
    width: 16px;
    height: 16px;
    opacity: 0.6;
    filter: alpha(opacity=60);
    margin: 0 0 0 2px;
    vertical-align: top;
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABe0lEQVQ4jWNkgII5IUH/BW/sYCAGvNfwYEhZsw6sF0ys1eH6b2AiyiBvIsTw79sfgkY8vvaJ4cKZ1wzBV74xMoJsduQ5yyCrxcfw89pnolwANuTtL4Zj7BYMLCBny8YpgzV/+fib4f2f/2AFgiyMKBrQxUE0SC8TiAOzGaRIc9NTMD7/+S9cM4gNE4cZxMPPCqbBBrxi+A+2HRl47n4O1gjCIDY6gKmHuOAtxOkgZ213lYQrBWlE1owsB3MJE0wA5jdDXmYUhciaQXIgdTDNKAYgCxID2IWRwuAdExPcEFx+hoUJzKLn7/+iugAEHnz/h+Fn9DABqUEGTI+5hBiQXYGsWYGTCYzRwwRZLeMEM5n/anzYky8fNwuY/vQVVR4k/kyWh+HZqS8MTDxyZgy3PkEUvhXhQlEI0oisGSQPwiDNP9/8YgDpBccdyBXCSmxgRewiEBofAGl+e+8XQ8GpJ4zwBJ+dnf1f9fRGgppB4LapP8PUqVMZGRgYGADODLn9wgQgMgAAAABJRU5ErkJggg==) no-repeat;
}

#dc_dialogContainer .dcHeader-tool a:hover {
    opacity: 1;
    filter: alpha(opacity=100);
    background-color: #eaf2ff;
    -moz-border-radius: 3px 3px 3px 3px;
    -webkit-border-radius: 3px 3px 3px 3px;
    border-radius: 3px 3px 3px 3px;
}

/* 对话框正文公共样式 */
#dc_dialogContainer #dcPanelBody {
    padding: 10px;
    background: rgb(250, 250, 250);
    height: 300px;
    color: #000000;
    font-size: 12px;
    overflow: auto;
    border-top-width: 0;
    text-align: left;
}

#dc_dialogContainer #dcPanelBody .dcTitle-line {
    border-top: 1px solid #ccc;
    display: inline-block;
    width: calc(100% - 40px);
    text-align: center;
    vertical-align: middle;
}

#dc_dialogContainer #dcPanelBody .dcBody-content {
    padding: 10px 10px 0;
}

#dc_dialogContainer .dcNumberbox {
    box-sizing: border-box;
    position: relative;
    border: 1px solid #95B8E7;
    background-color: #fff;
    vertical-align: middle;
    display: inline-block;
    overflow: hidden;
    white-space: nowrap;
    margin: 0;
    padding: 0;
    -moz-border-radius: 5px 5px 5px 5px;
    -webkit-border-radius: 5px 5px 5px 5px;
    border-radius: 5px 5px 5px 5px;
}

#dc_dialogContainer .dcNumberbox-right {
    position: absolute;
}

#dc_dialogContainer .dcNumberbox-icon {
    box-sizing: border-box;
    background: 0 0;
    color: #134da5;
    display: inline-block;
    overflow: hidden;
    vertical-align: top;
    background-position: center center;
    cursor: pointer;
    text-decoration: none;
    outline-style: none;
    opacity: 1.0;

}

#dc_dialogContainer .dcNumberbox-top,
.dcNumberbox-bottom {
    box-sizing: border-box;
    overflow: hidden;
    vertical-align: top;
    margin: 0;
    padding: 0;
    background-color: #E0ECFF;
    position: relative;
    display: block;
    width: 100%;
    height: 50%;
    cursor: pointer;
    opacity: 0.6;
}

#dc_dialogContainer .dcNumberbox-arrow-up,
.dcNumberbox-arrow-down {
    box-sizing: border-box;
    display: block;
    font-size: 1px;
    color: #444;
    outline-style: none;
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAAAQCAYAAACm53kpAAAAXElEQVR42mNgGAWjYBQMViA77cV/EB4NgJHqgAG1H2Y5KY7Ap55UswY0ANA9T2kgEGsGLnuJdQel+in2AKUBOOABQM0kTE7qGXa1AKUeGK0GRwNghAfAKBgFtAcA6XQOoD36cvIAAAAASUVORK5CYII=) no-repeat 1px center;
    opacity: 1.0;
    cursor: pointer;
    width: 16px;
    height: 16px;
    top: 50%;
    left: 50%;
    margin-top: -8px;
    margin-left: -8px;
    position: absolute;
    background-color: transparent;
}

#dc_dialogContainer .dcNumberbox-arrow-down {
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAAAQCAYAAACm53kpAAAAXElEQVR42mNgGAWjYBQMViA77cV/EB4NgJHqgAG1H2Y5KY7Ap55UswY0ANA9T2kgEGsGLnuJdQel+in2AKUBOOABQM0kTE7qGXa1AKUeGK0GRwNghAfAKBgFtAcA6XQOoD36cvIAAAAASUVORK5CYII=) no-repeat -15px center;
}

#dc_dialogContainer .dcNumberbox-top:hover,
.dcNumberbox-bottom:hover {
    opacity: 1.0;
    background-color: #eaf2ff;
}

#dc_dialogContainer .dcNumberbox-arrow-up:hover,
.dcNumberbox-arrow-up:hover {
    background-color: transparent;
}

#dc_dialogContainer .dcNumberbox_input {
    box-sizing: border-box;
    border-radius: 5px 5px 5px 5px;
    font-size: 14px;
    border: 0;
    padding: 0 4px;
    white-space: normal;
    vertical-align: top;
    outline-style: none;
    resize: none;
}

/* 设置间距 */
#dc_dialogContainer .dc_gap-width {
    display: inline-block;
    width: 20px;
}

#dc_dialogContainer input[type=checkbox],
input[type=radio] {
    vertical-align: middle;
}

/* 表格滚动的样式 */
#dcPanelBody .dc_scroll-table thead tr,
#dcPanelBody .dc_scroll-table tbody tr {
    box-sizing: border-box;
    table-layout: fixed;
    display: table;
    width: 100%;
}

#dcPanelBody .dc_scroll-table thead {
    display: block;
}

#dcPanelBody .dc_scroll-table tbody {
    display: block;
}

#dcPanelBody .dc_blockelement {
    display: block;
}

#dcPanelBody .dc_blockelement .fullWidth {
    width: 100%;
    margin: 5px 0;
}

/* 对话框底部部分 */
#dc_dialogContainer #dcPanelFooter,
#dc_dialogContainer1 #dcPanelFooter1,
#dc_dialogContainer2 #dcPanelFooter2 {
    box-sizing: border-box;
    text-align: center;
    background: rgb(250, 250, 250);
    padding: 6px;
    border-top: 1px solid #c6c6c6;
}

/* 按钮 */
#dc_dialogContainer .dclinkbutton,
#dc_dialogContainer1 .dclinkbutton,
#dc_dialogContainer2 .dclinkbutton {
    box-sizing: border-box;
    text-decoration: none;
    display: inline-block;
    overflow: hidden;
    margin: 0;
    padding: 0;
    cursor: pointer;
    outline: none;
    text-align: center;
    vertical-align: middle;
    line-height: normal;
    border-radius: 5px;
    background: linear-gradient(to bottom, #ffffff 0, #eeeeee 100%);
    background-repeat: repeat-x;
    border: 1px solid #bbb;
    color: #444;
    opacity: 0.9;
    filter: alpha(opacity=90);

}

#dc_dialogContainer .dclinkbutton:hover {
    color: #000000;
    border: 1px solid #666666;
    filter: none;
    opacity: 1.0;
    filter: alpha(opacity=100);
}

#dc_dialogContainer .dcButton-left {
    box-sizing: border-box;
    display: inline-block;
    position: relative;
    overflow: hidden;
    margin: 0;
    padding: 0;
    vertical-align: top;
}

#dc_dialogContainer .dcButton-text {
    box-sizing: border-box;
    display: inline-block;
    vertical-align: top;
    width: auto;
    line-height: 28px;
    font-size: 12px;
    padding: 0;
    margin: 0 10px;
}

#dc_dialogContainer .dc_submitValue_Readonly {
    cursor: no-drop;
}

#dc_dialogContainer .dc_submitValue_Readonly:hover {
    color: #000000;
    border: 1px solid #bbb;
    filter: none;
    opacity: 1.0;
}

/* 页面设置样式 */
#dc_dialogContainer #dcPanelBody.DocumentSettings{
    font-size: 14px;
}
.dc_radioBtn.endwise {
    margin-left: 23px
}

#dcPanelBody.DocumentSettings .dc_setting-section {
    height: 36px;
}

#dcPanelBody.DocumentSettings .dc_setting-left {
    display: inline-block;
    width: 88px;
    height: 100%;
    vertical-align: middle;
}

#dcPanelBody.DocumentSettings .dc_setting-name {
    display: inline-block;
    width: 100%;
    font-size: 14px;
}

#dcPanelBody.DocumentSettings .dc_DocumentSettings_Box_content label {
    margin: 5px 0;
    margin-right: 16px;
}

.dc_DocumentSettings_Box_content input {
    width: 100px;
    height: 26px;
}

#dcPanelBody.DocumentSettings .dc_setting-right {
    display: inline-block;
    height: 100%;
    vertical-align: middle;
}

#dcPanelBody.DocumentSettings .dc_radioBtn {
    position: relative;
    display: inline-block;
    line-height: 36px;
    cursor: pointer;
}

#dcPanelBody.DocumentSettings .dc_btnSelect {
    position: absolute;
    display: inline-block;
    top: 50%;
    left: 0;
    transform: translateY(-50%);
    width: 16px;
    height: 16px;
    border-style: solid;
    border-width: 1px;
    border-radius: 50%;
    border-color: #d4d4d4;
    box-sizing: border-box;
    vertical-align: top;
}

#dcPanelBody.DocumentSettings .dc_setting-right>.dc_radioBtn>span {
    display: inline-block;
    line-height: 36px;
    margin-left: 25px;
    cursor: default;
}

#dcPanelBody.DocumentSettings .dc_selected {
    position: absolute;
    top: 50%;
    left: 0;
    transform: translateY(-50%);
    width: 16px;
    height: 16px;
    box-sizing: border-box;
    border-width: 4px;
    border-style: solid;
    border-color: transparent;
    border-radius: 50%;
    background: #0188fb;
    background-clip: content-box;
    display: none;
}

#dcPanelBody.DocumentSettings .setting-select {
    display: inline-block;
    height: 100%;
    vertical-align: middle;
}

#dcPanelBody.DocumentSettings .dc_select-button {
    width: 224px;
    height: 26px;
    text-indent: 5px;
}

#dcPanelBody.DocumentSettings .dc_Box {
    margin-top: 16px;
}

#dcPanelBody.DocumentSettings #dc_attr-box {
    margin-bottom: 10px;
    margin-right: 10px
}

.dc_document_setting_label {
    margin-top: 10px;
}

#DCEnableHeaderFooterControl {
    margin-top: 0;
}
#dc_dialogContainer .DCEnableHeaderFooterControl_label_HeaderDistance{
    margin:5px 0;
    display: inline-block;
}

#dc_dialogContainer .DCEnableHeaderFooterControl_label_HeaderDistance span,
#dc_dialogContainer .DCEnableHeaderFooterControl_label_FooterDistance span,
#dc_dialogContainer .DCEnableHeaderFooterControl_label_DCPageIndexsForHideHeaderFooter span {
    display: inline-block;
}
#dc_dialogContainer .DCEnableHeaderFooterControl_label_HeaderDistance input,
#dc_dialogContainer .DCEnableHeaderFooterControl_label_FooterDistance input,
#dc_dialogContainer .DCEnableHeaderFooterControl_label_DCPageIndexsForHideHeaderFooter input {
    width: 100px;
    height: 26px;
}

#dcPanelBody.DocumentSettings .dc_select-button>option {
    font-size: 14px;
}

#dcPanelBody.DocumentSettings .number-input {
    position: relative;
    height: 36px;
    display: inline-block;
}

#dcPanelBody.DocumentSettings .pad-input {
    border: 1px solid #d4d4d4;
    font-size: 14px;
    padding: 10px;
    box-sizing: border-box;
    height: 36px;
    outline: none;
}

#dcPanelBody.DocumentSettings .pad-input::-webkit-inner-spin-button {
    -webkit-appearance: none;
}

#dcPanelBody.DocumentSettings .pad-input::-webkit-outer-spin-button {
    -webkit-appearance: none;
    /* 有无看不出差别 */
}

#dcPanelBody.DocumentSettings .height-top {
    position: absolute;
    top: 1px;
    right: 1px;
    height: 17px;
    width: 12px;
    display: none;
}

#dcPanelBody.DocumentSettings .height-top:hover {
    background: #e1e2e3;
}

#dcPanelBody.DocumentSettings .width-top {
    position: absolute;
    top: 1px;
    right: 1px;
    height: 17px;
    width: 12px;
    display: none;
}

#dcPanelBody.DocumentSettings .width-top:hover {
    background: #e1e2e3;
}

#dcPanelBody.DocumentSettings .carrow-top {
    position: absolute;
    top: 50%;
    left: 50%;
    border-left: 3px solid transparent;
    border-right: 3px solid transparent;
    border-bottom: 4px solid #8c9093;
    transform: translate(-50%, -50%);
}

#dcPanelBody.DocumentSettings .height-bottom {
    position: absolute;
    right: 1px;
    bottom: 1px;
    height: 17px;
    width: 12px;
    background: #f5f5f5;
    display: none;
}

#dcPanelBody.DocumentSettings .width-bottom {
    position: absolute;
    right: 1px;
    bottom: 1px;
    height: 17px;
    width: 12px;
    background: #f5f5f5;
    display: none;
}

#dcPanelBody.DocumentSettings .carrow-bottom {
    position: absolute;
    top: 50%;
    left: 50%;
    border-left: 3px solid transparent;
    border-right: 3px solid transparent;
    border-top: 4px solid #8c9093;
    transform: translate(-50%, -50%);
}

#dcPanelBody.DocumentSettings .width-bottom:hover {
    background: #e1e2e3;
}

#dcPanelBody.DocumentSettings .height-bottom:hover {
    background: #e1e2e3;
}

/* 网格线样式 */
#dcPanelBody.DocumentGridLine form {
    padding: 5px 0 0 15px;
}

#dcPanelBody.DocumentGridLine form span.dc_txt {
    display: inline-block;
    width: 100px;
}

#dcPanelBody.DocumentGridLine form .dc_changewidth [data-text] {
    width: 200px;
    height: 27px;
}

/* 装订线样式 */
#dcPanelBody.DocumentGutter .dc_GutterStyleDiv label {
    margin-left: 20px;
}

/* 单复选框样式,准备复用 */
#dcPanelBody label.dc_flex {
    display: flex;
    margin-top: 5px;
}

#dcPanelBody label.dc_flex span.dcTitle-text {
    display: inline-block;
    width: 100px;
}

#dcPanelBody label.dc_flex .dc_full {
    flex: 1;
}
#dcPanelBody label.dc_flex .dc_full.dc_full_SpecifyHeight{
    width: 100px;

}
#dcPanelBody label.dc_flex  .dc_data-text-model{
    color: red;
    margin-left: 5px;
}
#dcPanelBody .dc_checkboxs {
    margin: 5px 0;
}

#dcPanelBody .dc_checkboxs label {
    display: inline-block;
    width: 30%;
    margin-bottom: 5px;
    white-space: nowrap;
}

#dcPanelBody .needDialog {
    display: flex;
}

#dcPanelBody .needDialog input {
    flex: 1;
    margin-right: 5px;
}

#dcPanelBody .dc_Box,
#dcPanelBody1 .dc_Box {
    border: 1px solid #eee;
    border-radius: 5px;
    padding: 10px;
    margin-top: 18px;
    position: relative;
}

#dcPanelBody .dc_Box h6.dc_title,
#dcPanelBody1 .dc_Box h6.dc_title {
    margin: 0;
    background-color: #fff;
    position: absolute;
    top: -9px;
    font-size: 12px;
    font-weight: 700;
}
#dcPanelBody #dc_CheckboxVisibility{
    width: 100%;
    display: flex;
    justify-content: space-between;
    padding: 0 15px 6px 10px;
    box-sizing: border-box;
}
/* 插入单复选框样式 */
#dcPanelBody.InsertMultipleCheckBoxOrRadio .IsRadioBox label span {
    vertical-align: middle;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio .IsRadioBox label.dc_firstRadio {
    margin-right: 25px;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio #dc_ListItems {
    border-collapse: collapse;
    font-size: 14px;
    overflow: hidden;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio #dc_ListItems th {
    padding: 5px;
    text-align: left;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio #dc_ListItems td input {
    outline: none;
    border: none;
    padding: 5px;
    width: 100%;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio #dc_ListItems td.dc_delete,
#dcPanelBody.InsertMultipleCheckBoxOrRadio #dc_ListItems th.dc_last {
    width: 40px;
    text-align: center;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio #dc_ListItems td.delete {
    padding: 0;
    cursor: pointer;
    font-size: 16px;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio .dc_tab3Content label {
    margin: 2px 0;
    width: 100%;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio .dc_tab3Content span {
    width: 26%;
    display: inline-block;
}

#dcPanelBody.InsertMultipleCheckBoxOrRadio .dc_tab3Content input {
    width: 70%;
}


/* 标签文本样式,复用 */
#dcPanelBody .dc_blockelement textarea {
    width: 100%;
    height: 160px;
    margin-top: 4px;
    padding: 5px;
    outline: none;
}

/* 页码样式 */
#dcPanelBody.HorizontalLineElement ul#dc_ValueType {
    background-color: #fff;
    width: 100%;
    border: 1px solid #000;
    margin: 5px 0;
    padding: 5px;
}

#dcPanelBody.HorizontalLineElement ul#dc_ValueType li {
    list-style: none;
    padding: 5px;
}

#dcPanelBody.HorizontalLineElement .dc_active {
    background: #0078D7;
    color: #FFFFFF
}

#dcPanelBody.HorizontalLineElement .dc_minflex .dcTitle-text {
    min-width: 80px;
    width: auto;
}

#dcPanelBody.HorizontalLineElement .dc_Newline {
    width: 100%;
}

#dcPanelBody.HorizontalLineElement .dc_Newline input.dc_full {
    width: 100%;
    margin: 5px 0;
}

/* 按钮样式 */
#dcPanelBody .dc_imgButtonBox {
    line-height: 40px;
}

#dcPanelBody label.dc_imgButtonBox span.dcTitle-text {
    width: 100px;
}

#dcPanelBody .dc_imgButtonBox [data-value="img"] {
    position: relative;
    height: 40px;
    overflow: hidden;
}

#dcPanelBody .dc_imgButtonBox [data-value="img"] img {
    width: 100%;
    height: 100%;
}

#dcPanelBody .dc_imgButtonBox [data-value="img"] [type="file"] {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    opacity: 0;
}

#dcPanelBody.ButtonElement .dc_blockelement textarea {
    height: 55px;
}

#dcPanelBody.ButtonElement .dc_Box div {
    line-height: 20px;
}

#dcPanelBody.ButtonElement .dc_button_style_box input {
    width: 0;
    height: 0;
    opacity: 0;

}


#dcPanelBody.ButtonElement  div#dc_button_style_box {
    display: flex;
}

#dcPanelBody.ButtonElement  .dc_button_style_box_backgroundcolorstring {
    flex: 1;
    display: flex;
}

#dcPanelBody.ButtonElement .dc_button_style_box_colorstring,
#dcPanelBody.ButtonElement  .dc_button_style_box_backgroundcolorstring {
    flex: 1;
    display: flex;
    align-items: center;
}


#dcPanelBody.ButtonElement .dc_color_label {
    display: flex;
    align-items: center;
}

#dcPanelBody.ButtonElement  div#dc_backgroundcolorstring_box,
#dcPanelBody.ButtonElement  div#dc_colorstring_box {
    width: 16px;
    height: 16px;
    border: 1px solid #ccc;
}

#dcPanelBody.ButtonElement .dc_button_style_box span {
    display: inline-block;
    width: 64px;
}

#dcPanelBody.ButtonElement .dc_button_style_box select {
    width: 270px;
    margin-bottom: 6px;
}

/* 二维码样式 */
#dcPanelBody.QRCodeElement textarea {
    height: 100px;
}

/* 条形码样式 */
#dcPanelBody.BarcodeElement label.dc_flex span.dcTitle-text {
    width: 85px;
}

/* 修改图片属性对话框样式 */
#dcPanelBody.ImageElement #dc_image_box .imgDiv,
#dcPanelBody.MediaElement #video_box .videoDiv {
    height: 200px;
    border: 1px solid #000;
    margin: 5px 0;
    user-select: none;
    overflow: scroll;
}

#dcPanelBody.ImageElement #dc_image_box input[type="file"] {
    display: none;
}

#dcPanelBody.MediaElement #videoName {
    display: inline-block;
    width: calc(100% - 90px);
    height: 21px;
    vertical-align: bottom;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

/* 水印 */
.dcTitle-text-input-number {
    width: 50px;
    outline: none;
}

.dc_imgWaterType_wring,
.dcBody-content-wring {
    color: red;
}

.dc_textWaterType_label_box {
    padding: 0px 5px 5px;
}

.dc_textWaterType_label_box_font,
.dc_textWaterType_label_box_color {
    padding: 5px;
}

.dc_textWaterType_label_box input,
.dc_textWaterType_label_box_font input,
.dc_textWaterType_label_box_color input {
    width: 250px;
    height: 18px
}

/* 字体样式 */
.dc_font-content-dialog {
    display: flex;
    justify-content: space-around;
    margin-bottom: 5px;
}

#dc_font-content-dialog #font-ul-content,
#font-size-ul-content {
    height: 160px;
}

#dcPanelBody.fontStyleElement .dc_font-box {
    width: 46%;
    height: 100%;
}

#dcPanelBody.fontStyleElement .dc_font-box input {
    width: 100%;
}

#dcPanelBody.fontStyleElement .dc_ul-content {
    border: 1px solid #666666;
    list-style: none;
    height: 200px;
    padding-left: 0px;
    overflow: auto;
}

#dcPanelBody.fontStyleElement .dc_ul-content li {
    padding: 0 5px;
}

#dcPanelBody.fontStyleElement #dc_font-check-box {
    margin-top: 20px;
}

#dcPanelBody.fontStyleElement #dc_font-check-box .dc_font-style-content-dialog {
    margin-top: 20px;
}

#dcPanelBody.fontStyleElement #dc_font-check-box .dc_font-style-content-dialog .dc_Body-V {
    margin-right: 20px;
    display: inline-block;
}

#dc_dialogContainer .dc_Body-V input[type=checkbox] {
    vertical-align: text-top;
    margin: 0 2px;
}

/* 胎心图值样式 */
#dc_dialogContainer .FetalHeartElement table {
    width: 100%;
    margin: 0;
    border-collapse: collapse;
    table-layout: inherit;
}

#dc_fetal-heart-table td,
#dc_fetal-heart-table th {
    border: none;
}

#dc_fetal-heart-table .dc_fetal-heart-table-line-td {
    border: none;
}

#dc_fetal-heart-table .dc_fetal-heart-table-input {
    padding-bottom: 5px;
}

#dc_fetal-heart-table .dc_fetal-heart-table-input>input {
    width: 130px
}

#dc_fetal-heart-table .dc_fetal-heart-table-td-border {
    border-color: #000000;
    border-top-style: solid;
    border-width: 3px;
    width: 10px;
}

#dc_fetal-heart-table .dc_fetal-heart-table-border-right {
    border-color: #000000;
    border-bottom-style: solid;
    border-right-style: solid;
    border-width: 3px;
}

#dc_fetal-heart-table .dc_fetal-heart-table-border-left {
    border-color: #000000;
    border-left-style: solid;
    border-bottom-style: solid;
    border-width: 3px;
}

#dc_fetal-heart-table .dc_fetal-heart-table-border-top-right {
    border-top-style: solid;
    border-right-style: solid;
    border-width: 3px;
    border-top-color: #000000;
    border-right-color: #000000;
    width: 10px;
}

#dc_fetal-heart-table .dc_fetal-heart-table-border-top-left {
    border-top-style: solid;
    border-left-style: solid;
    border-width: 3px;
    border-top-color: #000000;
    border-left-color: #000000;
    width: 10px;
}

#dc_dialogContainer .FetalHeartElement #dc_fetal-heart-table .dc_fetal-heart-table-border-bottom {
    border-color: #000000;
    border-bottom-style: solid;
    border-width: 3px;
}

#dc_fetal-heart-table .dc_fetal-heart-table-input.table-input-border-top {
    border-color: #000000;
    border-width: 3px
}

/* 0-10之间的数字样式 */
#dc_dialogContainer #dcPanelBody.dc_PainIndexElement {
    padding: 24px;
}

#dc_dialogContainer #dcPanelBody.dc_PainIndexElement input {
    width: 120px;
}

/* 分数值样式 */
#dc_dialogContainer .dc_FractionElement td,
#dc_dialogContainer .dc_FractionElement th {
    border: none;
}

#dc_dialogContainer .dc_Fraction_table_tr {
    text-align: center;
    vertical-align: middle;
}

#dc_dialogContainer .dc_Fraction_table_input {
    width: 150px
}

#dc_dialogContainer .dc_Fraction_table_td {
    padding-right: 5px;
}

#dc_dialogContainer .dc_FractionElement table {
    width: auto;
}

/* 月经史值1样式 */
#dc_dialogContainer .dc_FourValuesElement td,
#dc_dialogContainer .dc_FourValuesElement th {
    border: none;
}

#dc_dialogContainer #dcPanelBody.dc_FourValuesElement {
    width: 100%;
}

#dc_dialogContainer #dcPanelBody.dc_FourValuesElement table {
    margin: 0;
    width: 100%;
}

#dc_dialogContainer .dc_FourValues_table_tr1 {
    text-align: center;
}

#dc_dialogContainer .dc_FourValues_table_td1_value1 {
    padding-bottom: 10px;
}

#dc_dialogContainer #dcPanelBody.dc_FourValuesElement input {
    width: 120px;
}

#dc_dialogContainer .dc_FourValues_table_td2_value {
    padding-bottom: 25px;
}

#dc_dialogContainer .dc_FourValues_table_tr2 {
    text-align: center
}

#dc_dialogContainer .dc_FourValues_table_tr2_td {
    border-color: #000000;
    border-top-style: solid;
    border-width: 3px;
}

#dc_dialogContainer .dc_FourValues_table_td1_value_border {
    border-color: #000000;
    border-bottom-style: solid;
    padding-bottom: 5px;
    border-width: 3px;
}

/* 月经史值2样式 */
#dc_dialogContainer #dcPanelBody.dc_FourValues1Element table {
    width: 100%;
    margin: 0;
    text-align: center
}

#dc_dialogContainer #dcPanelBody.dc_FourValues1Element td {
    border-width: 3px;
}

#dc_dialogContainer #dcPanelBody.dc_FourValues1Element td,
#dc_dialogContainer #dcPanelBody.dc_FourValues1Element th {
    border: none;
    border-color: #000000;
}

#dc_dialogContainer #dcPanelBody.dc_FourValues1Element .dc_FourValues_table_border_left {
    border-left-style: solid;
}

#dc_dialogContainer #dcPanelBody.dc_FourValues1Element .dc_FourValues_table_border_right {
    border-right-style: solid;
    padding-bottom: 5px;
}

#dc_dialogContainer #dcPanelBody.dc_FourValues1Element .dc_FourValues_table_border_bottom {
    border-bottom-style: solid;
}

#dc_dialogContainer #dcPanelBody.dc_FourValues1Element .dc_FourValues_table_border_top {
    border-top-style: solid;
}

/* 月经史值3样式 */
#dc_dialogContainer .dc_FourValues2Element td,
#dc_dialogContainer .dc_FourValues2Element th {
    border: none;
}

#dc_dialogContainer .dc_FourValues2Element table {
    width: 100%;
    margin: 0;
}

#dc_dialogContainer .dc_FourValues2Element .dc_FourValues_table_input {
    width: 100px;
}

#dc_dialogContainer .dc_FourValues2Element .dc_LeftLine {
    transform: rotate(340deg);
    width: 320px;
    height: 0;
    border-bottom: 3px solid #000;
    position: absolute;
    top: 102px;
    left: 42px;
}

#dc_dialogContainer .dc_FourValues2Element .dc_RightLine {
    transform: rotate(380deg);
    width: 320px;
    height: 0;
    border-bottom: 3px solid #000;
    position: absolute;
    top: 102px;
    left: 42px;
}

#dc_dialogContainer .dc_FourValues2Element .dc_FourValues_table_td {
    padding-bottom: 5px
}

/* 视野图公式 */
#dc_dialogContainer .dc_PerimetricDom td,
#dc_dialogContainer .dc_PerimetricDom th {
    border: none;
}

#dc_dialogContainer .dc_PerimetricDom table {
    width: 100%;
    margin: 0;
    height: 150px;
}

#dc_dialogContainer .dc_PerimetricDom .dc_Perimetric_table_td {
    padding-bottom: 5px
}

#dc_dialogContainer .dc_PerimetricDom .dc_LeftLine {
    transform: rotate(340deg);
    width: 320px;
    height: 0;
    border-bottom: 3px solid #000;
    position: absolute;
    top: 102px;
    left: 42px;
}

#dc_dialogContainer .dc_PerimetricDom .dc_RightLine {
    transform: rotate(380deg);
    width: 320px;
    height: 0;
    border-bottom: 3px solid #000;
    position: absolute;
    top: 102px;
    left: 42px;
}
#dc_dialogContainer .dc_PerimetricDom input{
    width:100%;
}
/* 月经史值4样式 */
#dc_dialogContainer .dc_ThreeValuesElement table {
    width: auto;
}

#dc_dialogContainer .dc_ThreeValuesElement tr {
    text-align: center;
    vertical-align: middle;
}

#dc_dialogContainer .dc_ThreeValuesElement input {
    width: 150px
}

#dc_dialogContainer .dc_ThreeValuesElement .dc_FourValues_table_value2_td {
    border-color: #000000;
    border-bottom-style: solid;
    padding-bottom: 5px;
    border-width: 3px;
}

#dc_dialogContainer .dc_ThreeValuesElement .dc_FourValues_table_value1_td {
    padding-right: 5px
}

#dc_dialogContainer .dc_ThreeValuesElement .dc_FourValues_table_value1_td3 {
    border-color: #000000;
    border-top-style: solid;
    border-width: 3px;
}

#dc_dialogContainer .dc_ThreeValuesElement td,
#dc_dialogContainer .dc_ThreeValuesElement th {
    border: none;
}

/* 瞳孔图值样式 */
#dc_dialogContainer .PupilElement table {
    width: auto;
    text-align: center;
}

#dc_dialogContainer .PupilElement input {
    width: 120px;
}

#dc_dialogContainer .PupilElement td,
#dc_dialogContainer .PupilElement th {
    border: none;
}

/* 光定位值样式 */
#dc_dialogContainer .dc_LightPositioningElement table {
    width: auto;
}

#dc_dialogContainer .dc_LightPositioningElement tr {
    text-align: center;
}

#dc_dialogContainer .dc_LightPositioningElement input {
    width: 120px;
}

#dc_dialogContainer .dc_LightPositioningElement td,
#dc_dialogContainer .dc_LightPositioningElement th {
    border: none;
}

/* 恒牙牙位图样式 */
#dc_dialogContainer .dc_PermanentTeethBitmapElement td,
#dc_dialogContainer .dc_PermanentTeethBitmapElement th {
    border: none;
}

#dc_dialogContainer .dc_PermanentTeethBitmapElement table {
    margin: 0;
    width: 100%;
    position: relative;
    padding: 0 30px;
}

.dc_PermanentTeethBitmapElement .dc_PermanentTeethBitmap_tr1 {
    position: absolute;
    width: 100%;
    padding-left: 50%;
}

.dc_PermanentTeethBitmapElement .dc_PermanentTeethBitmap_tr2 {
    position: absolute;
    height: 100%;
    left: 0;
    top: 60%;
}

.dc_PermanentTeethBitmapElement .dc_PermanentTeethBitmap_span1 {
    height: 100px;
    margin-left: -46px;
}

.dc_PermanentTeethBitmapElement .dc_PermanentTeethBitmap_span2 {
    height: 100px;
}

.dc_PermanentTeethBitmapElement .dc_PermanentTeethBitmap_tr3 {
    text-align: center;
    border-bottom: 1px solid #d3d3d3
}

.dc_PermanentTeethBitmapElement .dc_PermanentTeethBitmap_tr3 td {
    vertical-align: bottom;
}

.dc_PermanentTeethBitmapElement .dc_PermanentTeethBitmap_tr3 td span {
    width: 20px;
    height: 100px;
}

.dc_PermanentTeethBitmapElement .dc_PermanentTeethBitmap_tr4 {
    text-align: center;
}

.dc_PermanentTeethBitmap_hr {
    margin: 3px 0;
}

#dc_dialogContainer .dc_PermanentTeethBitmapElement table tr td input {
    cursor: pointer;
    font-size: 14px;
}

#dc_dialogContainer #dcPanelBody.dc_PermanentTeethBitmapElement {
    width: 100%;
}

#dc_dialogContainer .dc_PermanentTeethBitmap_table2,
#dc_dialogContainer .dc_PermanentTeethBitmap_table3 {
    width: 100%;
    background-color: #f0f0f0;
    padding: 0 30px;
    position: relative;
}

#dc_dialogContainer .dc_PermanentTeethBitmap_table2 span,
#dc_dialogContainer .dc_PermanentTeethBitmap_table3 span {
    height: 100px;
}

#dc_dialogContainer .dc_PermanentTeethBitmap_table3 .dc_PermanentTeethBitmap_table3_tr1 span {
    height: 100px;
}

#dc_dialogContainer .dc_PermanentTeethBitmap_table3 .dc_PermanentTeethBitmap_table3_tr {
    text-align: center
}

#dc_dialogContainer .dc_PermanentTeethBitmap_table3 .dc_PermanentTeethBitmap_table3_tr1 {
    position: absolute;
    height: 100%;
    left: 0;
    top: 40%;
}

#dc_dialogContainer .dc_PermanentTeethBitmap_table2_tr1 {
    position: absolute;
    height: 100%;
    left: 0;
    top: 30%;
}

#dc_dialogContainer .dc_PermanentTeethBitmap_table2_tr2 {
    position: absolute;
    height: 100%;
    right: 0;
    top: 30%;
}

#dc_dialogContainer .dc_PermanentTeethBitmap_table2_tr3,
#dc_dialogContainer .dc_PermanentTeethBitmap_table2_tr4 {
    text-align: center
}

#dc_dialogContainer .dc_PermanentTeethBitmap_center {
    margin: 5px 0;
}

/* 乳牙牙位图样式 */
#dc_dialogContainer .dc_DeciduousTeech_table1 {
    width: 100%;
    position: relative;
    padding: 0 30px;
    margin: 0;
}

#dc_dialogContainer .DeciduousTeechElement td,
#dc_dialogContainer .DeciduousTeechElement th {
    border: none;
}

#dc_dialogContainer #dcPanelBody.DeciduousTeechElement {
    width: 100%;
}

#dc_dialogContainer #dcPanelBody.DeciduousTeechElement table tr td input {
    cursor: pointer;
    font-size: 14px;
}

#dc_dialogContainer .dc_DeciduousTeech_table1_tr1 {
    position: absolute;
    width: 100%;
    padding-left: 50%;
}

#dc_dialogContainer .dc_DeciduousTeech_table1_tr1 td span {
    height: 100px;
    margin-left: -46px;
}

#dc_dialogContainer .dc_DeciduousTeech_table1_tr2 {
    position: absolute;
    height: 100%;
    left: 5px;
    top: 60%;
}

#dc_dialogContainer .dc_DeciduousTeech_table1_tr2 td span {
    height: 100px;
}

#dc_dialogContainer .dc_DeciduousTeech_table1_tr3 {
    text-align: center;
    border-bottom: 1px solid #d3d3d3;
}

#dc_dialogContainer .dc_DeciduousTeech_table1_tr3 td {
    vertical-align: bottom;
}

#dc_dialogContainer .dc_DeciduousTeech_table1_tr3 td span {
    width: 20px;
    height: 100px;
}

#dc_dialogContainer .dc_DeciduousTeech_table1_tr4,
#dc_dialogContainer .dc_DeciduousTeech_table1_tr5,
#dc_dialogContainer .dc_DeciduousTeech_table1_tr6,
#dc_dialogContainer .dc_DeciduousTeech_table1_tr7,
#dc_dialogContainer .dc_DeciduousTeech_table1_tr8,
#dc_dialogContainer .dc_DeciduousTeech_table1_tr9 {
    text-align: center;
}

#dc_dialogContainer .dc_DeciduousTeech_table2 {
    width: 100%;
    background-color: #f0f0f0;
    padding: 0 30px;
    position: relative;
    margin-left: 0px;
}

#dc_dialogContainer .dc_DeciduousTeech_table2_tr1 {
    position: absolute;
    height: 100%;
    left: 5px;
    top: 30%;
}

#dc_dialogContainer .dc_DeciduousTeech_table2_tr1 td span {
    height: 100px;
}

#dc_dialogContainer .dc_DeciduousTeech_table2_tr2 {
    position: absolute;
    height: 100%;
    right: 5px;
    top: 30%;
}

#dc_dialogContainer .dc_DeciduousTeech_table2_tr2 td span {
    height: 100px;
}

#dc_dialogContainer .dc_DeciduousTeech_table2_tr3,
#dc_dialogContainer .dc_DeciduousTeech_table2_tr4 {
    text-align: center;
}

#dc_dialogContainer .dc_DeciduousTeech_table3 {
    width: 100%;
    background-color: #f0f0f0;
    padding: 0 30px;
    position: relative;
    margin-left: 0px;
}

#dc_dialogContainer .dc_DeciduousTeech_table3_tr1 {
    position: absolute;
    height: 100%;
    left: 5px;
    top: 40%;
}

#dc_dialogContainer .dc_DeciduousTeech_table3_tr1 td span {
    height: 100px;
}

#dc_dialogContainer .dc_DeciduousTeech_table3_tr2,
#dc_dialogContainer .dc_DeciduousTeech_table3_tr3,
#dc_dialogContainer .dc_DeciduousTeech_table3_tr4,
#dc_dialogContainer .dc_DeciduousTeech_table3_tr5,
#dc_dialogContainer .dc_DeciduousTeech_table3_tr6,
#dc_dialogContainer .dc_DeciduousTeech_table3_tr7 {
    text-align: center;
}


#dc_dialogContainer .dc_DeciduousTeech_hr {
    margin: 3px 0;
}

#dc_dialogContainer .dc_DeciduousTeech_center {
    margin: 5px 0;
}


/* 病变上牙 */
#dc_dialogContainer .dc_DiseasedTeethTop_box {
    display: flex;
    height: 280px;
    align-items: center;
    position: relative;
}

#dc_dialogContainer .dc_DiseasedTeethTop_box .dc_use_value1_1 {
    width: 80px;
    border-bottom: 5px solid #000;
    transform: rotate(40deg);
    position: absolute;
    top: 100px;
    left: 50%;
    margin-left: -70px;
}

#dc_dialogContainer .dc_DiseasedTeethTop_box .dc_use_value1_2 {
    width: 80px;
    border-bottom: 5px solid #000;
    transform: rotate(140deg);
    position: absolute;
    top: 100px;
    left: 50%;
    margin-left: -10px;
}

#dc_dialogContainer .dc_DiseasedTeethTop_box .dc_use_value1_3 {
    width: 80px;
    border-bottom: 5px solid #000;
    transform: rotate(90deg);
    position: absolute;
    top: 164px;
    left: 50%;
    margin-left: -40px;
}

#dc_dialogContainer .dc_DiseasedTeethTop_box .dc_use_value1_4 {
    position: absolute;
    width: 50px;
    top: 68px;
    left: 50%;
    margin-left: -25px;
}

#dc_dialogContainer .dc_DiseasedTeethTop_box .dc_DiseasedTeethTop_input1 {
    position: absolute;
    width: 50px;
    left: 32%;
}

#dc_dialogContainer .dc_DiseasedTeethTop_box .dc_DiseasedTeethTop_input2 {
    position: absolute;
    width: 50px;
    left: 54%;
}

/*病变下牙*/
#dc_dialogContainer .dc_DiseasedTeethBotton_box {
    display: flex;
    height: 140px;
    align-items: center;
}

#dc_dialogContainer .dc_DiseasedTeethBotton_value1_box {
    flex: 1;
    padding-right: 10px;
}

#dc_dialogContainer .dc_DiseasedTeethBotton_value1_box input {
    display: inline-block;
    width: 100%;
}

#dc_dialogContainer .dc_DiseasedTeethBotton_value2_box {
    flex: 1;
    padding-left: 10px;
}

#dc_dialogContainer .dc_DiseasedTeethBotton_value2_box input {
    display: inline-block;
    width: 100%;
    margin-bottom: 20px;
}

#dc_dialogContainer .dc_DiseasedTeethBotton_value2_box p {
    border-bottom: 6px solid #000;
}

#dc_dialogContainer .dc_DiseasedTeethBotton_value2_box input:last-child {
    margin-top: 20px;
}


/* 执行命令对话框样式 */
.dc_DCExecuteCommandTable_container {
    width: 100%;
    height: 100%;
    overflow: hidden;
    display: flex;
    flex-direction: column;
}
    
#dc_DCExecuteCommandTable_search_box{
   width: 100%;
    overflow: hidden;
    display: flex;
    align-items: center;
    box-sizing: border-box;
    margin-bottom: 10px;
}
#dc_DCExecuteCommandTable_search_input {
    height: 24px;
    display: inline-block;
    width: 212px;
    padding: 4px;
    box-sizing: border-box;
}
#dc_DCExecuteCommandTable_search_btn {
    height: 24px;
    padding: 0 10px;
    margin-left: -1px;
}

#dcPanelBody.dc_DCExecuteCommandsElement #dc_DCExecuteCommandTable {
    width: 100%;
    height: 68%;
    border-collapse: collapse;
    overflow-y: scroll;
}

#dc_dc_DCExecuteCommand_options {
    flex: 1;
    overflow: scroll;
}

#dc_DCExecuteCommandTable table {
    margin: 0;
    width: 100%;
}

.dc_dc_DCExecuteCommand_options_box {
    flex: 1;
    display: flex;
    flex-direction: column;
}

#dcPanelBody.dc_DCExecuteCommandsElement th {
    position: sticky;
    top: 0;
    background-color: #f2f2f2;
}

#dcPanelBody.dc_DCExecuteCommandsElement th,
#dcPanelBody.dc_DCExecuteCommandsElement td {
    padding: 8px;
    text-align: left;
    border-bottom: 1px solid #ddd;
}

#dcPanelBody.dc_DCExecuteCommandsElement tr:last-child td {
    border-bottom: 1px solid black;
}

#dcPanelBody.dc_DCExecuteCommandsElement tr.ClickLine {
    background-color: #0078D7;
    color: #fff;
}

#dcPanelBody.dc_DCExecuteCommandsElement .dc_DCExecuteCommand_options_title {
    color: #000;
    font-weight: 900;
    margin: 5px 0;
}

/* 查找和替换样式 */
#dcPanelBody.SearchAndReplace .dcBody-contents {
    display: flex;
}
#dc_dialogContainer #dcPanelFooter .foot_btn {
    height: 28px;
    width: auto;
    border: 1px solid #bbb;
}

#dcPanelBody.SearchAndReplace .dcBody-contents .dc_Box {
    width: 40%;
}

#dcPanelBody.SearchAndReplace .dcBody-contents .dc_checkboxs {
    margin-top: 10px;
    width: 60%;
    padding-left: 20px;
    padding-top: 5px;
}

#dcPanelBody.SearchAndReplace .dcBody-contents .dc_Box .dcBody-content {
    display: flex;
    justify-content: space-between;
    height: 100%;
}

#dcPanelBody.SearchAndReplace .dcBody-contents .dc_Box .dcBody-content label {
    padding-top: 16px;
}

/*批注样式*/
#dcPanelBody.dc_EditDocumentComments {
    display: flex;
    flex-direction: column;
}

#dcPanelBody.dc_EditDocumentComments .dc_EditDocumentComments_dialog {
    flex: 1;
    width: 100%;
    height: auto;
}

#dcPanelBody.dc_EditDocumentComments .dc_EditDocumentComments_dialog #dc_Text {
    width: 100%;
    height: 100px;
}

#dcPanelBody.dc_EditDocumentComments .dc_Box {
    width: 100%;
    height: auto;
    margin: 10px 0;
}

#dcPanelBody.dc_EditDocumentComments .dc_Box label {
    display: block;
    margin-bottom: 10px;
}

#dcPanelBody.dc_EditDocumentComments .dc_Box input[type="color"] {
    width: 20px;
    height: 20px;
    margin-right: 20px;
}

#dcPanelBody.dc_EditDocumentComments .dc_Box h6 {
    margin-top: 0;
}

#dcPanelBody.dc_EditDocumentComments .dc_Box p {
    margin: 5px 0;
}

#dcPanelBody.dc_EditDocumentComments .dc_Box .dc_title {
    margin-bottom: 10px;
}

#dcPanelBody.dc_EditDocumentComments #dc_attr-box {
    margin-bottom: 10px;
    margin-right: 10px;
}


/*输入域样式*/
.dc_InputFieldContent .dc_tab1Content {
    display: flex;
    flex-wrap: wrap;
}

.dc_InputField_tab_box {
    height: 334px;
    overflow-y: auto;
}

.dc_InputField_tab_box .dc_tab1 {
    display: block;
    flex: 1;
    width: 100%;
    height: auto;
}

.dc_InputFieldContent .dc_tab1Content>label {
    width: 50%
}

.InputFieldElement .dc_InputFieldContent .dc_tab1Content {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab1Content>label {
    width: 48%;
    box-sizing: border-box;
    display: flex;
    margin-bottom: 13px;
    line-height: 20px;
    align-items: center;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab1Content>label>input,
#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab1Content>label>select {
    width: 60%;
    height: 20px;
}
#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab1Content>label>.dc_SpecifyWidth_input{
    width: 60px;
}
#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab1Content>label>.dc-text-model-SpecifyWidth{
    width: 60px;
    color: red;
    margin-left: 5px;
}
#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab1Content>label>span {
    width: 42%;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab1Content .dc_borderBox {
    flex: 1;
    display: flex;
    justify-content: space-between;
    padding: 0;
}

.dc_Box.dc_DisplayFormat_box {
    height: 300px;
}

.dc_DisplayFormat_Style_box {
    display: flex;
    max-height: 350px;
}

.dc_DisplayFormat_Style_box_center {
    flex: 1;
    text-align: left;

}

.dc_DisplayFormat_Style_box_center span {
    display: inline-block;
    width: 100%;

}

#dc_UI-1,
#dc_UI-2 {
    width: 154px;
    border: 1px solid #767676;
    background: #ffffff;
}

#dc_tab4 .dc_Box {
    margin-top: 10px;
}

#dc_tab3>div {
    margin-bottom: 10px;
}

#dc_tab3 .dc_Box.dc_changeDisabled {
    margin-top: 0;
}

.dc_tab3_content {
    display: flex;
    flex-wrap: wrap;
    margin-bottom: 10px;

}

.dc_tab3_content label {
    width: 48%;

}


#dc_dialogContainer .dc_tab3_content label input[type="text"],
#dc_dialogContainer  #dc_tab3 .dc_tab3_text_content input[type="number"],
#dc_dialogContainer  #dc_tab3 .dc_tab3_date_content input[type="datetime-local"]{
    width: 140px;
    display: inline-block;
    margin-bottom: 4px;
}
#dc_dialogContainer  #dc_tab3 .dc_tab3_number_content input[type="number"]{
    width: 100px;
    margin-bottom: 4px;
}
#dc_dialogContainer .dc_tab3_content label span{
    width: 72px;
    display: inline-block;
}
#dc_dialogContainer  #dc_tab3 .dc_tab3_text_content span,
#dc_dialogContainer  #dc_tab3 .dc_tab3_date_content sapn{
    width: 62px;
    display: inline-block;
}
#dc_dialogContainer  #dc_tab3 .dc_tab3_number_content span{
     width: 100px;
     display: inline-block;
}

#dc_tab4 .dc_input_VisibleExpression_label,
#dc_tab4 .dc_input_PropertyExpressions_label {
    margin-top: 10px;
}

#dc_tab4 .dc_input_ValueExpression_label,
#dc_tab4 .dc_input_VisibleExpression_label,
#dc_tab4 .dc_input_PropertyExpressions_label {
    display: flex;
}

#dc_tab4 .dc_input_ValueExpression_label span,
#dc_tab4 .dc_input_VisibleExpression_label span,
#dc_tab4 .dc_input_PropertyExpressions_label span {
    width: 100px;
}

#dc_tab4 .dc_input_ValueExpression_label input,
#dc_tab4 .dc_input_VisibleExpression_label input,
#dc_tab4 .dc_input_PropertyExpressions_label input {
    flex: 1;
}

#dc_tab4 .dc_input_SourcePropertyName_label {
    margin: 5px 0;
}

#dc_tab4 .dc_input_SourceID_label,
#dc_tab4 .dc_input_SourcePropertyName_label,
#dc_tab4 .dc_input_DescPropertyName_label {
    display: flex;
}

#dc_tab4 .dc_input_SourceID_label span,
#dc_tab4 .dc_input_SourcePropertyName_label span,
#dc_tab4 .dc_input_DescPropertyName_label span {
    width: 100px;
}

#dc_tab4 .dc_input_SourceID_label input,
#dc_tab4 .dc_input_SourcePropertyName_label input,
#dc_tab4 .dc_input_DescPropertyName_label input {
    flex: 1;
}

#dc_tab4 .dc_input_Attributes_label,
#dc_tab4 .dc_input_AcceptChildElementTypes_label {
    display: flex;
    margin-top: 5px;
}

#dc_tab4 .dc_input_Attributes_label span,
#dc_tab4 .dc_input_AcceptChildElementTypes_label span {
    width: 100px;
}

#dc_tab4 .dc_input_Attributes_label input,
#dc_tab4 .dc_input_AcceptChildElementTypes_label input {
    flex: 1;
}

#dc_ListValueFormatString option {
    background-color: #eee;
}

.dc_borderBox_label {
    display: flex;
}

.dc_borderBox_label input {
    width: 30px
}

.dc_EditorActiveModeButton_label {
    position: relative;
}

.dc_SpecifyWidth_label>span {
    display: flex;
    align-items: center;
}

.dc_BorderVisible_label {
    display: flex;
}

.dc_input_ContentReadonly_label {
    display: flex;
}

.dc_input_ContentReadonly_label span {
    width: 84px;
    display: inline-block;
}

.dc_input_ContentReadonly_label select {
    width: 120px;
}

.dc_input_UserEditable_label input {
    vertical-align: text-top;
}

.dc_input_Deleteable_label input {
    vertical-align: text-top;

}

.dc_input_ViewEncryptType_label {
    display: flex;
}

.dc_input_ViewEncryptType_label span {
    width: 88px;
    display: inline-block;
}

.dc_input_ViewEncryptType_label select {
    width: 120px;

}

.dc_input_TextColor_label #dc_TextColor_box,
.dc_input_BackgroundTextColor_label #dc_BackgroundTextColor_box,
.dc_BackgroundColorString_container #dc_BackgroundColorString_box{
    display: inline-block;
    width: 16px;
    height: 16px;
    border: 1px solid #767676;
}

.dc_input_TextColor_label input,
.dc_input_BackgroundTextColor_label input,
.dc_input_BackgroundColorString_label input  {
    width: 0;
    height: 0;
    opacity: 0;

}
    
#dc_colorContainer{
    display: flex;
    align-items: center;
}
#dc_colorContainer .dc_TextColor_container,
#dc_colorContainer .dc_BackgroundTextColor_container,
#dc_colorContainer .dc_BackgroundColorString_container{
    flex: 1;
    display: flex;
    align-items: center;
}

#dc_dropDownList_formbox  .dc_Check-box .dc_dc_Check_box_label {
    margin-right: 10px;
    margon-bottom: 10px;
}

.dc_dc_Check_box_label_flex {
    display: flex;
}

.dc_dropDownList_formbox {
    margin-left: 18px;
    width: 76%;
}
#dc_dropDownList_formbox .dc_Check-box .dc_dropDownList_formbox_label{
    display: flex;
    align-items: center;
}

#dc_dropDownList_formbox .dc_Check-box .dc_dropDownList_formbox_label span{
    width: 110px;
}
#dc_dropDownList_formbox .dc_Check-box .dc_dropDownList_formbox_label select,
#dc_dropDownList_formbox .dc_Check-box .dc_dropDownList_formbox_label input{
    width: 160px;
}
.dc_input_ListValueFormatString_label {
    width: 100%;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab2Content {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab2Content>label {
    width: 48%;
    line-height: 20px;
    margin-bottom: 10px;
     display: flex;
}
#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab2Content>label span {
    width: 92px;
}
#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab2Content .dc_tab2Content_label_inp{
   width: 120px;
}
#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab3Content {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab3Content>label {
    line-height: 20px;
    display: flex;
    margin-bottom: 10px;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab3Content>label>span {
    width: 86px;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab3Content>label>input,
#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab3Content>label>select {
    height: 20px;
    width: 124px;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_tab3Content {
    display: flex;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_buttonBox {
    height: 28px;
    width: 100%;
    display: flex;
    line-height: 28px;
    margin-bottom: 18px;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_buttonBox>span {
    width: 50px;
    text-align: center;
    background: #e1e1e1;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent .dc_buttonBox>span.dc_active {
    background: #fff;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent #dc_tab2 {
    display: flex;
    flex-direction: column;
    text-align: left;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent #dc_tab2 label {
    line-height: 28px;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent #dc_tab2 .dc_Check-box {
    display: flex;
    flex-wrap: wrap;
    padding-left: 20px;
    box-sizing: border-box;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent #dc_tab2 .dc_Check-box>label {
    line-height: 22px;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent #tab3>label {
    margin-bottom: 16px;
}

#dcPanelBody.InputFieldElement .dc_InputFieldContent #tab4>.dc_Box>label {
    margin-bottom: 8px;
}

#dc_dialogContainer1 .dcHeader-tool,
#dc_dialogContainer2 .dcHeader-tool {
    right: 5px;
    width: auto;
    position: absolute;
    top: 25%;
    margin-top: -8px;
    height: 16px;
    overflow: hidden;
}

#dc_dialogContainer1 .dcHeader-tool a {
    display: inline-block;
    width: 16px;
    height: 16px;
    opacity: 0.6;
    filter: alpha(opacity=60);
    margin: 0 0 0 2px;
    vertical-align: top;
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABe0lEQVQ4jWNkgII5IUH/BW/sYCAGvNfwYEhZsw6sF0ys1eH6b2AiyiBvIsTw79sfgkY8vvaJ4cKZ1wzBV74xMoJsduQ5yyCrxcfw89pnolwANuTtL4Zj7BYMLCBny8YpgzV/+fib4f2f/2AFgiyMKBrQxUE0SC8TiAOzGaRIc9NTMD7/+S9cM4gNE4cZxMPPCqbBBrxi+A+2HRl47n4O1gjCIDY6gKmHuOAtxOkgZ213lYQrBWlE1owsB3MJE0wA5jdDXmYUhciaQXIgdTDNKAYgCxID2IWRwuAdExPcEFx+hoUJzKLn7/+iugAEHnz/h+Fn9DABqUEGTI+5hBiQXYGsWYGTCYzRwwRZLeMEM5n/anzYky8fNwuY/vQVVR4k/kyWh+HZqS8MTDxyZgy3PkEUvhXhQlEI0oisGSQPwiDNP9/8YgDpBccdyBXCSmxgRewiEBofAGl+e+8XQ8GpJ4zwBJ+dnf1f9fRGgppB4LapP8PUqVMZGRgYGADODLn9wgQgMgAAAABJRU5ErkJggg==) no-repeat;
}

#dc_dialogContainer2 .dcHeader-tool a {
    display: inline-block;
    width: 16px;
    height: 16px;
    opacity: 0.6;
    filter: alpha(opacity=60);
    margin: 0 0 0 2px;
    vertical-align: top;
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABe0lEQVQ4jWNkgII5IUH/BW/sYCAGvNfwYEhZsw6sF0ys1eH6b2AiyiBvIsTw79sfgkY8vvaJ4cKZ1wzBV74xMoJsduQ5yyCrxcfw89pnolwANuTtL4Zj7BYMLCBny8YpgzV/+fib4f2f/2AFgiyMKBrQxUE0SC8TiAOzGaRIc9NTMD7/+S9cM4gNE4cZxMPPCqbBBrxi+A+2HRl47n4O1gjCIDY6gKmHuOAtxOkgZ213lYQrBWlE1owsB3MJE0wA5jdDXmYUhciaQXIgdTDNKAYgCxID2IWRwuAdExPcEFx+hoUJzKLn7/+iugAEHnz/h+Fn9DABqUEGTI+5hBiQXYGsWYGTCYzRwwRZLeMEM5n/anzYky8fNwuY/vQVVR4k/kyWh+HZqS8MTDxyZgy3PkEUvhXhQlEI0oisGSQPwiDNP9/8YgDpBccdyBXCSmxgRewiEBofAGl+e+8XQ8GpJ4zwBJ+dnf1f9fRGgppB4LapP8PUqVMZGRgYGADODLn9wgQgMgAAAABJRU5ErkJggg==) no-repeat;
}

#dc_dialogContainer1 .dcHeader-tool a:hover {
    opacity: 1;
    filter: alpha(opacity=100);
    background-color: #eaf2ff;
    -moz-border-radius: 3px 3px 3px 3px;
    -webkit-border-radius: 3px 3px 3px 3px;
    border-radius: 3px 3px 3px 3px;
}

#dc_dialogContainer2 .dcHeader-tool a:hover {
    opacity: 1;
    filter: alpha(opacity=100);
    background-color: #eaf2ff;
    -moz-border-radius: 3px 3px 3px 3px;
    -webkit-border-radius: 3px 3px 3px 3px;
    border-radius: 3px 3px 3px 3px;
}

#dc_dialogContainer1 .dcHeader-tool,
#dc_dialogContainer2 .dcHeader-tool {
    top: 14px;
}

#dc_dialogContainer1 .dclinkbutton,
#dc_dialogContainer2 .dclinkbutton {
    width: 36px;
    height: 30px;
}

#dc_dialogContainer1 #dcPanelBody1,
#dc_dialogContainer2 #dcPanelBody2 {
    height: 500px;
    padding: 10px;
    background: rgb(250, 250, 250);
    height: 300px;
    color: #000000;
    font-size: 12px;
    overflow: auto;
    border-top-width: 0;
    box-sizing: border-box;
}

#dcPanelBody1.InputFieldElement .dc_InputFieldContent #dc_batchPlanTable tbody,
#dc_batchPlanTable td,
#dc_batchPlanTable tfoot,
#dc_batchPlanTable th,
#dc_batchPlanTable thead,
#dc_batchPlanTable tr,
#dc_attr-table-box td,
#dc_attr-table-box tfoot,
#dc_attr-table-box th,
#dc_attr-table-box thead,
#dc_attr-table-box tr {
    border: 1px solid #d3d3d3;
    overflow: hidden;
}

#dc_batchPlanTable th.dc_on-1 {
    width: 32px;
}

#dc_AcceptChildCheck .dc_EditorActiveItem {
    display: inline-block;
}

.dc_cellGridlineBox,
.dc_cellGridlineContent {
    display: flex;
    flex-direction: column;
}

.dc_cellGridlineContent {
    margin-left: 10px;
}

.dc_cellGridlineBox-span {
    width: 140px;
    display: inline-block;
    line-height: 30px;
}

.dc_cellGridlineBox-input,
.dc_cellGridlineContent #dc_LineStyle {
    display: inline-block;
    width: 170px;
}

.dc_cellDiagonalLineBox #dc_slantsplitlinestyle {
    border: 1px solid #858585;
    flex: 1;
    height: 20px;
    line-height: 20px;
}

.dc_cellDiagonalLineBox>div {
    display: flex;
}

.dc_cellDiagonalLineBox>div>span {
    display: inline-block;
    line-height: 50px;
}

.dc_cellDiagonalLineBox .dc_None,
.dc_cellDiagonalLineBox .dc_TopLeftOneLine,
.dc_cellDiagonalLineBox .dc_TopLeftTwoLines,
.dc_cellDiagonalLineBox .dc_TopRightOneLine,
.dc_cellDiagonalLineBox .dc_TopRightTwoLines,
.dc_cellDiagonalLineBox .dc_BottomRightOneLine,
.dc_cellDiagonalLineBox .dc_BottomRigthTwoLines,
.dc_cellDiagonalLineBox .dc_BottomLeftOneLine,
.dc_cellDiagonalLineBox .dc_BottomLeftTwoLines {
    border: 1px solid #858585;
    width: 100px;
    height: 50px;
    margin: 5px 10px 5px 0 !important;
}

/* 图片编辑界面元素 */
#dcPanelBody.dc_imgEdit #dc_wrap #dc_wrap_imgBox::-webkit-scrollbar {
    width: 12px;
    height: 12px;
}


#dcPanelBody.dc_imgEdit #dc_wrap .dc_cut_line{
    width: 1px;
    height: 24px;
    border-right: 1px solid #ccc;
   margin-left: 12px;
    margin-right: 8px;
}
#dcPanelBody.dc_imgEdit #dc_wrap .dc_cut_confirm {
    display: flex;
    align-items: center;
      margin-left: 10px;
    cursor: pointer;

}

#dcPanelBody.dc_imgEdit #dc_wrap .dc_cut_cencal {
    display: flex;
    align-items: center;
    cursor: pointer;


}

#dcPanelBody.dc_imgEdit #dc_wrap .dc_cut_confirm:hover,
#dcPanelBody.dc_imgEdit #dc_wrap .dc_cut_cencal:hover{
    background-color: #ecf5ff;
    
}
#dcPanelBody.dc_imgEdit  #dc_wrap .dc_Box.dc_imgEdit_Content::-webkit-scrollbar-thumb,
#dcPanelBody.dc_imgEdit #dc_wrap #dc_wrap_imgBox::-webkit-scrollbar-thumb {
    background-color: rgba(0, 0, 0, .3);
    background-clip: padding-box;
    border: 3px solid transparent;
    border-radius: 7px;
}
#dcPanelBody.dc_imgEdit  #dc_wrap .dc_Box.dc_imgEdit_Content::-webkit-scrollbar-thumb:hover,
#dcPanelBody.dc_imgEdit #dc_wrap #dc_wrap_imgBox::-webkit-scrollbar-thumb:hover {
    background-color: rgba(0, 0, 0, 0.4);
}
#dcPanelBody.dc_imgEdit  #dc_wrap .dc_Box.dc_imgEdit_Content::-webkit-scrollbar-track,
#dcPanelBody.dc_imgEdit #dc_wrap #dc_wrap_imgBox::-webkit-scrollbar-track {
    background-color: #fff;
}
#dcPanelBody.dc_imgEdit  #dc_wrap .dc_Box.dc_imgEdit_Content::-webkit-scrollbar-track:hover,
#dcPanelBody.dc_imgEdit #dc_wrap #dc_wrap_imgBox::-webkit-scrollbar-track:hover {
    background-color: #f8fafc;
}

#dc_dialogContainer #dcPanelBody.imgEdit button,
#dc_dialogContainer #dcPanelBody.imgEdit input {
    padding: 5px;
    font-size: 12px;
    cursor: pointer;
}

#dc_dialogContainer #dcPanelBody.imgEdit #wrap {
    cursor: default;
    overflow: auto;
    width: 100%;
    height: 320px;
    border: #666 1px solid;
    background: #999;
    position: relative;
}

.InsertSpecifyCharacter .dc_tabButtonItem {
    border: 1px solid #ccc;
    padding: 4px 8px;
    margin-bottom: -1px !important;
    margin-left: -1px !important;
    background: #fff;
    cursor: pointer;
}

.InsertSpecifyCharacter .dc_tabButtonItem.dc_active {
    border-bottom: none;
}

.InsertSpecifyCharacter #dc_tabButton {
    display: flex;
    border-bottom: 1px solid #ccc;
    font-size: 14px;
}

.InsertSpecifyCharacter #dc_tabDomBox {
    border: 1px solid #ccc;
    border-top: 0;
    overflow: auto;
    font-size: 14px;
}

.InsertSpecifyCharacter .dc_tabDomBoxItem {
    display: flex;
    flex-wrap: wrap;
    padding: 10px;
    box-sizing: border-box;
    text-align: center;
    height: 440px;
    align-content: flex-start
}

.InsertSpecifyCharacter .dc_tabDomBoxItem .dc_charSpanDomItem {
    width: 10%;
    height: 28px;
    line-height: 28px;
    text-align: center;
    cursor: pointer;
    display: inline-block;
}

.InsertSpecifyCharacter .dc_tabDomBoxItem .dc_charSpanDomItem:hover {
    color: #9bbbe3;
}

.dc_tabDomBoxItem#MedicalCharacters .dc_charSpanDomItem {
    width: 14%;
}

/* 眼球突出度 */
.EyeballProtrusion .dc_EyeballProtrusionBox {
    width: 100%;
    height: 180px;
    display: flex;
}

.EyeballProtrusion .dc_EyeballProtrusionBox .dc_EyeballProtrusionContainerLeft,
.EyeballProtrusion .dc_EyeballProtrusionBox .dc_EyeballProtrusionContainerRight {
    width: 100px;
    position: absolute;
    z-index: 9;
}

.EyeballProtrusion .dc_EyeballProtrusionBox .dc_EyeballProtrusionContainerLeft {
    top: 106px;
    left: 30px;
}

.EyeballProtrusion .dc_EyeballProtrusionBox .dc_EyeballProtrusionContainerRight {
    right: 15px;
    top: 106px;
}

.EyeballProtrusion .dc_EyeballProtrusionBox .dc_EyeballProtrusionContainerCenter {
    width: 100%;
}

.EyeballProtrusion .dc_EyeballProtrusionBox .LineBox .dc_line {
    border-top: 2px solid #000;
}

.EyeballProtrusion .dc_EyeballProtrusionBox .LineBox {
    position: relative;
    height: 90%;
    left: 0;
    top: 0;
    width: 100%;
}

.EyeballProtrusion .dc_EyeballProtrusionBox .LineBox .dc_line.dc_LineCenter {
    width: 180px;
    position: absolute;
    left: 50%;
    top: 50%;
    margin-left: -90px !important;
}

.EyeballProtrusion .dc_EyeballProtrusionBox .LineBox .dc_line.dc_LineLeftTop {
    position: absolute;
    left: 90px;
    top: 56px;
    width: 70px;
    transform: rotate(45deg);
}

.EyeballProtrusion .dc_EyeballProtrusionBox .LineBox .dc_line.dc_LineLeftBottom {
    position: absolute;
    left: 90px;
    top: 106px;
    width: 70px;
    transform: rotate(315deg);
}

.EyeballProtrusion .dc_EyeballProtrusionBox .LineBox .dc_line.dc_LineRightTop {
    position: absolute;
    right: 90px;
    top: 56px;
    width: 70px;
    transform: rotate(315deg);
}

.EyeballProtrusion .dc_EyeballProtrusionBox .LineBox .dc_line.dc_LineRightBottom {
    position: absolute;
    right: 90px;
    top: 106px;
    width: 70px;
    transform: rotate(45deg);
}

.EyeballProtrusion .dc_EyeballProtrusionBox .dc_ValueInput {
    font-size: 14px;
    width: 70px;
    height: 28px;
}

#dc_dialogContainer .dc_EyeballProtrusionContainerCenter_value2 {
    top: 60px;
    left: 50%;
    margin-left: -42px;
    width: 100px;
    position: absolute;
    z-index: 9;

}



/* 创建斜视符号*/
.dc_SquintSymbolBox {
    display: flex;
    width: 100%;
    height: 180px;
    justify-content: space-evenly;
}

.dc_SquintSymbolLeftContainer {
    width: 50%;
    height: 100%;
    background: #9bbbe3;
    padding: 10px;
}

#dc_LeftLineText_L {
    position: absolute;
    bottom: 0;
    left: 10px;
    font-size: 14px;
    font-weight: 900;
}

#dc_LeftLineText_R {
    position: absolute;
    bottom: 0;
    right: 10px;
    font-size: 14px;
    font-weight: 900;
}

.SquintSymbol .dc_RadioLabel {
    width: 100%;
    line-height: 30px;
}

.dc_ShowView {
    width: 100%;
    height: 100%;
    background: rgba(255, 255, 255, 0.8);
    position: relative;
}

.dc_ShowView .dc_LeftLine {
    width: 100%;
    border-top: 3px solid #000;
    position: absolute;
    top: 50%;
    transform: rotate(-45deg)
}

.dc_ShowView .dc_RightLine {
    width: 100%;
    border-top: 3px solid #000;
    position: absolute;
    top: 50%;
    transform: rotate(45deg)
}

.dc_ShowView .dc_CenterRound {
    width: 10px;
    height: 10px;
    border-radius: 50%;
    background: #000;
    position: absolute;
    top: 50%;
    left: 50%;
    margin-top: -5px !important;
    margin-left: -5px !important;
}

#dc_EditorActiveModeSelect,
#dc_AcceptChildElementTypesHtml {
    background: #f7f7f7;
    position: fixed;
    opacity: 1;
    z-index: 99999999;
    left: 50%;
    width: 220px;
    top: 50%;
    border: 1px solid;
    margin-left: -110px;
    margin-top: -230px;
    font-size: 12px;
    z-index: 10003;
}

#dc_AcceptChildElementTypesHtml {
    width: 240px;
}

#dc_EditorActiveModeSelect .dc_EditorActiveItem,
#dc_AcceptChildElementTypesHtml .dc_EditorActiveItem {
    min-height: 30px;
    line-height: 30px;
    width: 100%;
    word-wrap: break-word;
    word-break: normal;
}

#dc_EditorActiveModeSelect .dc_EditorActiveItem input,
#dc_EditorActiveModeContainer .dc_EditorActiveItem input {
    margin-right: 4px;
}

#dc_AcceptChildElementTypesHtmlConfom {
    width: 46px;
    height: 30px;
    text-align: center;
    line-height: 30px;
    border: 1px solid #c2c2c2;
    border-radius: 5px;
    margin: 0 5px;
}

.dc_EditorActiveModeContent_Box {
    padding: 10px;
    box-sizing: border-box;
}

#dc_dialogContainer #dc_wrap {
    width: 490px;
    height: 280px;
    overflow: auto;
}

#dc_EditorActiveModeSelect .dc_EditorActiveModeDialogBox,
#dc_AcceptChildElementTypesHtml .dc_EditorActiveModeDialogBox {
    width: 100%;
    display: flex;
    justify-content: center;
    border-top: 1px solid #ccc;
    padding: 5px 10px;
    box-sizing: border-box;
}

#dc_EditorActiveModeConfom,
#dc_EditorActiveModeCancel {
    width: 46px;
    height: 30px;
    text-align: center;
    line-height: 30px;
    border: 1px solid #c2c2c2;
    border-radius: 5px;
    margin: 0 5px;
    cursor: pointer;
}

#dc_EditorActiveModeSelect .dc_EditorActiveModeHeader,
#dc_AcceptChildElementTypesHtml .dc_EditorActiveModeHeader {
    width: 100%;
    display: flex;
    justify-content: space-between;
    border-bottom: 1px solid #ccc;
    padding: 5px;
    box-sizing: border-box;
}

#dc_EditorActiveModeSelect .dc_EditorActiveModeHeader>p {
    margin: 0;
}

#dc_EditorActiveModeSelect .dc_EditorActiveModeCancelButtonIcon,
#dc_AcceptChildElementTypesHtml .dc_EditorActiveModeCancelButtonIcon {
    width: 16px;
    height: 16px;
    opacity: 0.6;
    filter: alpha(opacity=60);
    margin: 0 0 0 2px;
    vertical-align: top;
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABe0lEQVQ4jWNkgII5IUH/BW/sYCAGvNfwYEhZsw6sF0ys1eH6b2AiyiBvIsTw79sfgkY8vvaJ4cKZ1wzBV74xMoJsduQ5yyCrxcfw89pnolwANuTtL4Zj7BYMLCBny8YpgzV/+fib4f2f/2AFgiyMKBrQxUE0SC8TiAOzGaRIc9NTMD7/+S9cM4gNE4cZxMPPCqbBBrxi+A+2HRl47n4O1gjCIDY6gKmHuOAtxOkgZ213lYQrBWlE1owsB3MJE0wA5jdDXmYUhciaQXIgdTDNKAYgCxID2IWRwuAdExPcEFx+hoUJzKLn7/+iugAEHnz/h+Fn9DABqUEGTI+5hBiQXYGsWYGTCYzRwwRZLeMEM5n/anzYky8fNwuY/vQVVR4k/kyWh+HZqS8MTDxyZgy3PkEUvhXhQlEI0oisGSQPwiDNP9/8YgDpBccdyBXCSmxgRewiEBofAGl+e+8XQ8GpJ4zwBJ+dnf1f9fRGgppB4LapP8PUqVMZGRgYGADODLn9wgQgMgAAAABJRU5ErkJggg==) no-repeat;
}

#dc_EditorActiveModeSelect .dc_EditorActiveModeContainer {
    padding: 10px;
    display: flex;
    flex-direction: column;
}

#dc_EditorActiveModeButton {
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
    width: 60%;
    height: 20px;
    border: 1px solid #767676;
    padding: 0 2px;
    box-sizing: border-box;
    border-radius: 2px;
}

#dc_dialogContainer1 {
    border: 1px solid #ccc;
    z-index: 99999;
    position: fixed;
    top: 50%;
    left: 50%;
    width: 550px;
    background: #fafafa;
}

#dcPanelHeader1 {
    background: #F5F5F5;
    border-bottom: 1px solid #c6c6c6;
    font-size: 12px;
    font-weight: bold;
    color: #0E2D5F;
    line-height: 16px;
    padding-left: 18px;
    text-align: left;
    padding: 6px;
}

#dcPanelFooter1 {
    background: #F5F5F5;
}

.dc_watermark_container {
    text-align: left;
    margin-bottom: 20px;
}

.dc_watermarks_text {
    margin-left: 10px;
    font-weight: 600;
    font-size: 12px;
}

.dc_watermark_container_2 {
    text-align: right;
}

#dc_batchPlanTable {
    width: 100%;
    border-collapse: collapse;
    margin: 10px 0;
    background: #fafafa;

}

#dc_batchPlanTable th {
    border-width: 1px;

}

#dc_batchPlanTable tr.dc_tr_ab td input {
    width: 100%;
    height: 100%;
    border: none;
}

#dc_batchPlanTable input {
    width: 100%;
    height: 100%;
    border: none;

}

#dc_dialogContainer2 {
    border: 1px solid #ccc;
    z-index: 99999;
    position: fixed;
    top: 50%;
    left: 50%;
    width: 550px;
    background: #fafafa;
}

#dcPanelHeader2 {
    background: #F5F5F5;
    border-bottom: 1px solid #c6c6c6;
    font-size: 12px;
    font-weight: bold;
    color: #0E2D5F;
    line-height: 16px;
    padding-left: 18px;
    text-align: left;
    padding: 6px;

}

#dcPanelBody2 {
    margin-top: 20px;

}

#dc_attr-box {
    margin-bottom: 10px;
    margin-right: 10px;

}

#dcPanelFooter2 {
    background: #F5F5F5;

}

.dcTool-close {
    background: #cd6b45;
    color: #fff;
    display: inline-block;
    width: 16px;
    height: 16px;
    text-align: center;
    line-height: 16px;
    opacity: 0.8;
    font-size: 10px;
    border-radius: 3px;
    cursor: pointer;
}

.dcTool-close:hover {
    opacity: 1;
}

.dc_childrenDialogContainer,
#dc_PropertyExpressionsBoxDialog {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: 10002;
    background: rgb(0, 0, 0, 0.1);
}

/*查看错误信息*/
.dc_ReportExceptionsElement .dc_ReportExceptionsTd {
    line-height: 20px;
    width: 100px;
    display: inline-block;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
}

.dc_ReportExceptionsElement .dc_ReportExceptions_Box {
    font-size: 14px;
}

.dc_ReportExceptionsElement .dc_ReportExceptionRefresh {
    text-align: right;
}

.dc_ReportExceptionsElement #dc_ReportExceptionItem {
    height: calc(100% - 40px);
    overflow: auto;
    user-select: text;
}

.dc_ReportExceptionsElement .dc_ReportExceptionsTr {
    height: 20px;
    overflow: hidden;
    line-height: 20px;
}

.dc_ReportExceptionsElement .dc_ReportExceptionsTr .dc_ReportExceptionsTd3 {
    color: #409eff;
    cursor: pointer;
}

.dc_ReportExceptionsElement .dc_ReportExceptionsTr .dc_ReportExceptionsTd3:hover {
    text-decoration: underline;
}

.dc_ReportExceptionsElement .dc_ReportExceptionRefresh {
    color: #409eff;
    cursor: pointer;
}

.dc_ReportExceptionsElement .dc_ReportExceptionRefresh:hover {
    text-decoration: underline;
}

.dc_ReportExceptionsElement #dc_ReportExceptionErrorBox {
    position: absolute;
    width: 100%;
    background: #fff;
    z-index: 100010;
    left: 0;
    height: 100%;
    top: 0;
    display: none;
    padding: 10px;
    overflow: auto;
    box-sizing: border-box;
    border-radius: 5px;
    border: 1px solid #ccc;
}

.dc_ReportExceptionsElement #dc_dc_ReportExceptionErrorBox {
    height: 20px;
    border-bottom: 1px solid #ccc;
    display: flex;
    justify-content: end;
    margin-bottom: 10px;
}

.dc_ReportExceptionsElement #dc_dc_ReportExceptionErrorBox #ReportExceptionItemTop {
    background: #cd6b45;
    color: #fff;
    display: inline-block;
    width: 16px;
    height: 16px;
    text-align: center;
    line-height: 16px;
    opacity: 0.8;
    font-size: 10px;
    border-radius: 3px;
    cursor: pointer;
}

.dc_ReportExceptionsElement #dc_ReportExceptionRefreshBox {
    display: flex;
    justify-content: flex-end;
    padding: 0 10px;
    margin-bottom: 4px;
}

#WatermarkSrcImg {
    max-width: 100px;
    max-height: 100px;
    overflow: auto;
    padding: 2px;
    margin-top: 5px;
}

.dc_color_box {
    display: flex;
    align-items: center;
    justify-content: center;
}

/*可见性表达式提示框*/
#dc_visible_expression_table {
    width: 700px;
    height: 700px;
    position: fixed;
    top: 50%;
    left: 50%;
    background: #fff;
    overflow: auto;
    z-index: 99999;
    margin-top: -300px;
    margin-left: -350px;
    display: flex;
    flex-direction: column;
    overflow: hidden;
    border-radius: 6px;
}

.dc_visibleexpression_table_content td {
    border: 1px solid black;
    word-break: break-all;
    padding: 6px;
}

.dc_visibleexpression_container {
    padding: 14px;
    box-sizing: border-box;
    flex: 1;
    overflow-y: auto;
}

.dc_visibleexpression_table_content {
    border: 1px solid black;
    border-collapse: collapse;
    table-layout: fixed
}

.dc_visibleexpression_table_content td {
    border: 1px solid black;
    word-break: break-all
}

.dc_visibleexpression_header {
    height: 30px;
    display: flex;
    justify-content: space-between;
    font-weight: 900;
    font-size: 14px;
    line-height: 30px;
    align-items: center;
    background: linear-gradient(to bottom, #ffffff 0, #eeeeee 100%);
    padding: 6px;
    position: relative;
    border-bottom: 1px solid #c6c6c6;
}

.dc_visible_expression_table_footer {
    height: 46px;
    box-sizing: border-box;
    text-align: center;
    background: rgb(250, 250, 250);
    padding: 6px;
    border-top: 1px solid #c6c6c6;
}

.dc_visible_expression_table_footer>.dc_visible_expression_table_close {
    display: inline-block;
    width: 50px;
    height: 30px;
    line-height: 30px;
    padding: 0;
    cursor: pointer;
    border-radius: 5px;
    background: linear-gradient(to bottom, #ffffff 0, #eeeeee 100%);
    background-repeat: repeat-x;
    border: 1px solid #bbb;
    color: #444;
    opacity: 0.9;
}

.dc_visible_expression_table_footer>.dc_visible_expression_table_close:hover {
    color: #000000;
    border: 1px solid #666666;
}

#dc_visible_expression_table .dc_visibleexpression_header_td td {
    font-weight: 900;
    font-size: 14px;
}

#dc_visible_expression_table .dc_visible_expression_POW_table_sup {
    font-weight: normal;
    text-decoration: none
}

#dc_visible_expression_table .dc_visible_expression_SQRT_table {
    background-color: white;
    color: #333333;
    font-family: 宋体;
    font-size: 9.75pt;
    font-style: normal;
    font-weight: normal;
    text-decoration: none
}

.dc_SpecifyWidth_title {
    display: inline-block;
    width: 15px;
    height: 15px;
    border: 1px solid #000000;
    color: #000000;
    text-align: center;
    line-height: 14px;
    border-radius: 9px;
    cursor: pointer;
}

.dc_SpecifyWidth_title:hover {
    border: 1px solid #606266;
    color: #606266;
}

.dc_StationaryBridgeTeethBox {
    width: 100%;
    height: 280px;
    display: flex;
    flex-direction: column;
    position: relative;
}

.dc_StationaryBridgeTeethTop,
.dc_StationaryBridgeTeethBottom {
    width: 100%;
    height: 20px;
    text-align: center;
    position: absolute;
    width: 50px;
    left: 50%;
    margin-left: -25px !important;
}

.dc_StationaryBridgeTeethTop {
    top: 30px;
}

.dc_StationaryBridgeTeethBottom {
    top: 245px;
}

.dc_StationaryBridgeTeethCenter {
    flex: 1;
    display: flex;
    flex-direction: column;
}

.dc_StationaryBridgeTeethCenter_Top,
.dc_StationaryBridgeTeethCenter_Bottom {
    width: 100%;
    height: 120px;
    display: flex;
    justify-content: space-between;
    padding: 0 28px;
}

#dc_StationaryBridgeTeethCenter_Top_value1,
#dc_StationaryBridgeTeethCenter_Top_value2 {
    display: flex;
    align-items: flex-end;
    text-align: center;
}

#dc_StationaryBridgeTeethCenter_Top_value3,
#dc_StationaryBridgeTeethCenter_Top_value4 {
    display: flex;
    align-items: flex-start;
    text-align: center;
}

.dc_StationaryBridgeTeethButton {
    writing-mode: vertical-rl;
    text-orientation: upright;
    letter-spacing: 4px;
    width: 20px;
    text-align: center;
    border: 1px solid #ccc;
    padding: 2px 0;
    margin: 0 2px !important;
    cursor: pointer;
}

.dc_StationaryBridgeTeethCenter_Content {
    display: flex;
    align-items: center;
    margin: 8px 0 !important;
}

.dc_StationaryBridgeTeethCenter_Center {
    flex: 1;
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
}

.dc_StationaryBridgeTeethCenter_Center_line {
    width: 100%;
    border-top: 1px solid #000000;
}

#dc_StationaryBridgeTeethCenter_Center_value1,
#dc_StationaryBridgeTeethCenter_Center_value2,
#dc_StationaryBridgeTeethCenter_Center_value3,
#dc_StationaryBridgeTeethCenter_Center_value4 {
    display: flex;
    align-items: center;
    height: 20px;
}

.dc_StationaryBridgeTeethNum {
    width: 24px;
    text-align: center;
    line-height: 20px;
    height: 20px;
}

.dc_StationaryBridgeTeethNum:nth-child(odd) {
    background-color: #d7e4f2;
    /* 设置背景颜色 */
}

.dc_StationaryBridgeTeethNum:nth-child(even) {
    background-color: #ffffff;
    /* 设置背景颜色 */
}

#dc_StationaryBridgeTeethCenter_Center_value2,
#dc_StationaryBridgeTeethCenter_Center_value4 {
    border-left: 1px solid #000;
}

#dc_StationaryBridgeTeethCenter_Center_value3,
#dc_StationaryBridgeTeethCenter_Center_value4 {
    border-top: 1px solid #000;
}

.dc_StationaryBridgeTeethCenter_Content_clear {
    width: 20px;
    height: 20px;
    text-align: center;
    border: 1px solid #ccc;
    cursor: pointer;
}

.dc_StationaryBridgeTeethCenter_Content_left .dc_StationaryBridgeTeethCenter_Content_clear {
    margin-right: 8px !important;
}

.dc_StationaryBridgeTeethCenter_Content_right .dc_StationaryBridgeTeethCenter_Content_clear {
    margin-left: 7px !important;
}

.dc_StationaryBridgeTeethButton.dc_jia_teeth_button {
    background-color: #ccccff;
}

.dc_StationaryBridgeTeethButton.dc_jian_teeth_button {
    background-color: #f9cc9d;
}

.dc_StationaryBridgeTeethButton.dc_deciduous_teeth_button {
    background-color: yellow;
}

/*电活力牙位图*/
.dc_ElectricPulpTestingTeeth_Box {
    width: 100%;
    height: 250px;
    flex-direction: column;
    position: relative;
    padding: 10px 0;
}

.dc_ElectricPulpTestingTeeth_top,
.dc_ElectricPulpTestingTeeth_bottom {
    width: 50px;
    height: 20px;
    text-align: center;
    position: absolute;
    left: 50%;
    margin-left: -25px !important;
}

.dc_ElectricPulpTestingTeeth_top {
    top: 0;
}

.dc_ElectricPulpTestingTeeth_bottom {
    bottom: 0;
}

.dc_ElectricPulpTestingTeeth_center {
    width: 100%;
    height: 100%;
    display: flex;

}

.dc_ElectricPulpTestingTeeth_center_left {
    width: 20px;
    height: 100%;
    display: flex;
    flex-direction: column;
}

.dc_ElectricPulpTestingTeeth_center_left>div {
    writing-mode: vertical-rl;
    text-orientation: upright;
    letter-spacing: 4px;
    width: 20px;
    text-align: center;
    flex: 1;
}

.dc_ElectricPulpTestingTeeth_center_right {
    flex: 1;
    display: flex;
    flex-direction: column;
}

#dc_ElectricPulpTestingTeeth_center_right_top,
#dc_ElectricPulpTestingTeeth_center_right_bottom {
    display: flex;
}

#dc_ElectricPulpTestingTeeth_center_right_center {
    display: flex;
    flex-wrap: wrap;
}

#dc_ElectricPulpTestingTeeth_button_box1,
#dc_ElectricPulpTestingTeeth_button_box2,
#dc_ElectricPulpTestingTeeth_button_box3,
#dc_ElectricPulpTestingTeeth_button_box4 {
    display: flex;
    align-items: flex-end;
}

#dc_ElectricPulpTestingTeeth_button_box3,
#dc_ElectricPulpTestingTeeth_button_box4 {
    align-items: flex-start;
}

.dc_teeth_text_item {
    width: 28px;
    writing-mode: vertical-rl;
    text-orientation: upright;
    letter-spacing: 4px;
    display: flex;
    justify-content: center;
    align-items: center;
    margin-right: 2px !important;
}

.dc_teeth_input_item {
    width: 28px;
    height: 28px;
    border: 1px solid #ccc;
    text-align: center;
    margin-right: 2px !important;
}

.dc_teeth_input_item::-webkit-inner-spin-button,
.dc_teeth_input_item::-webkit-outer-spin-button {
    -webkit-appearance: none;
    margin: 0;
}

.dc_teeth_input_item {
    -moz-appearance: textfield;
}

#dc_ElectricPulpTestingTeeth_input_box1,
#dc_ElectricPulpTestingTeeth_input_box2,
#dc_ElectricPulpTestingTeeth_input_box3,
#dc_ElectricPulpTestingTeeth_input_box4 {
    height: 30px;
    display: flex;
}

.dc_ElectricPulpTestingTeeth_center_yamian {
    display: flex;
    justify-content: flex-end;
}

.dc_ElectricPulpTestingTeeth_center_clean_box {
    height: 60px;
    position: relative;
}

.dc_ElectricPulpTestingTeeth_center_yamian2 {
    display: flex;
    justify-content: center;
}

.dc_input_clear_button {
    position: absolute;
    left: -2px;
    top: 50%;
    margin-top: -6px !important;
    width: 20px;
    height: 20px;
    border: 1px solid #ccc;
    cursor: pointer;
}

/*混合牙位图*/
#dcPanelBody .dc_AdvancedTeech_AutoSize{
    margin-top: 14px;
}
#dc_dialogContainer #dcPanelBody .dc_AdvancedTeech_AutoSize input{
    width: 13px;
}
.dc_AdvancedTeech_Box {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    position: relative;
    padding: 10px;
    box-sizing: border-box;
}

.dc_AdvancedTeech_title_top,
.dc_AdvancedTeech_title_bottom {
    position: absolute;
    width: 50px;
    height: 20px;
    text-align: center;
    left: 50%;
    margin-left: -25px !important;
}

.dc_AdvancedTeech_title_top {
    top: 0;
}

.dc_AdvancedTeech_title_bottom {
    bottom: 0;
}

.dc_AdvancedTeech_top {
    width: 100%;
}

.dc_AdvancedTeech_cnter {
    width: 100%;
    display: flex;
    justify-content: space-between;
    padding: 10px 0;
    margin: 10px 0 !important;
    border-top: 1px solid #000;
    border-bottom: 1px solid #000;
}

.dc_AdvancedTeech_top_title_box {
    width: 452px;
    height: 80px;
    display: flex;
    align-items: flex-end;
    padding-left: 26px;
}

.dc_AdvancedTeech_top_title_Item {
    width: 26px;
    writing-mode: vertical-rl;
    text-orientation: upright;
    letter-spacing: 4px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.dc_AdvancedTeech_top_teeth_box,
.dc_AdvancedTeech_Bootom_teeth_box {
    width: 100%;
    display: flex;
}

.dc_AdvancedTeech_teeth_content,
.dc_AdvancedTeech_teeth_content_bottom {
    width: 427px;
    display: flex;
}

.dc_AdvancedTeech_teeth_content_List {
    width: 26px;
    text-align: center;
}

.dc_AdvancedTeech_teeth_content_Item {
    width: 100%;
    text-align: center;
    height: 26px;
    line-height: 26px;
    margin-top: -1px !important;
    border: 1px solid #ccc;
    cursor: pointer;
}

.dc_AdvancedTeech_teeth_content_List,
.dc_AdvancedTeech_top_title_Item {
    margin-left: -1px !important;
}

.dc_AdvancedTeech_cnter_left,
.dc_AdvancedTeech_cnter_right {
    width: 26px;
    text-align: center;
    display: flex;
    align-items: center;
    justify-content: center;
}

.dc_AdvancedTeech_cnter_center {
    width: 427px;
}

.dc_AdvancedTeech_cnter_permanent_top,
.dc_AdvancedTeech_cnter_permanent_bottom,
.dc_AdvancedTeech_cnter_deciduous_top,
.dc_AdvancedTeech_cnter_deciduous_bottom {
    width: 100%;
    display: flex;
}

.dc_AdvancedTeech_cnter_permanent_bottom .dc_AdvancedTeech_teeth_content_Item,
.dc_AdvancedTeech_cnter_permanent_top .dc_AdvancedTeech_teeth_content_Item,
.dc_AdvancedTeech_cnter_deciduous_top .dc_AdvancedTeech_teeth_content_Item,
.dc_AdvancedTeech_cnter_deciduous_bottom .dc_AdvancedTeech_teeth_content_Item {
    width: 26px;
    margin-left: -1px !important;
}

.dc_AdvancedTeech_cnter_deciduous_top,
.dc_AdvancedTeech_cnter_deciduous_bottom {
    justify-content: center;
    margin-top: 4px !important;
}

.dc_AdvancedTeech_cnter_supernumerary_top,
.dc_AdvancedTeech_cnter_supernumerary_bottom {
    width: 100%;
    border-bottom: 1px solid #000;
    display: flex;
    justify-content: space-between;
}

.dc_AdvancedTeech_cnter_supernumerary_top {
    margin-top: 10px !important;
    padding: 0 13px 2px;
}

.dc_AdvancedTeech_cnter_supernumerary_bottom {
    border-bottom: none;
    padding: 2px 13px 0;
    margin-bottom: 10px !important;
}

.dc_AdvancedTeech_teeth_triangle_back {
    width: 24px;
    height: 24px;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 1px !important;
    cursor: pointer;
    margin-left: -1px !important;
}

.dc_AdvancedTeech_teeth_triangle_back.dc_current_select {
    background: #303133;
}

.dc_AdvancedTeech_teeth_triangle_back.dc_current_select>.dc_AdvancedTeech_teeth_triangle {
    border-bottom-color: #fff;
}

.dc_AdvancedTeech_teeth_triangle {
    width: 0;
    height: 0;
    border-left: 1.5mm solid transparent;
    border-right: 1.5mm solid transparent;
    border-bottom: 2.598mm solid #000;
    cursor: pointer;
}

.dc_AdvancedTeech_teeth_content_Item.dc_current_select {
    color: #fff;
    background: #303133;
}

#dc_StationaryBridgeTeethCenter_Center_value1 .dc_jia_teeth_StationaryBridgeTeethNum,
#dc_StationaryBridgeTeethCenter_Center_value1 .dc_jian_teeth_StationaryBridgeTeethNum,
#dc_StationaryBridgeTeethCenter_Center_value2 .dc_jia_teeth_StationaryBridgeTeethNum,
#dc_StationaryBridgeTeethCenter_Center_value2 .dc_jian_teeth_StationaryBridgeTeethNum {
    border-top: 3px solid #000;
}

#dc_StationaryBridgeTeethCenter_Center_value3 .dc_jia_teeth_StationaryBridgeTeethNum,
#dc_StationaryBridgeTeethCenter_Center_value3 .dc_jian_teeth_StationaryBridgeTeethNum,
#dc_StationaryBridgeTeethCenter_Center_value4 .dc_jia_teeth_StationaryBridgeTeethNum,
#dc_StationaryBridgeTeethCenter_Center_value4 .dc_jian_teeth_StationaryBridgeTeethNum {
    border-bottom: 3px solid #000;
}

.dc_PDTeech_center {
    flex: 1;
    display: flex;
    flex-direction: column;
}

.dc_PDTeech_content {
    height: 140px;
    width: 100%;
    display: flex;
}


.dc_PDTeech_left .dc_PDTeech_left_input,
.dc_PDTeech_right .dc_PDTeech_left_input {
    width: 70px;
    height: 30px;
}

.dc_PDTeech_right,
.dc_PDTeech_left {
    width: 70px;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-evenly;
    align-items: center;
}

.dc_PDTeech_center_top,
.dc_PDTeech_center_bottom {
    flex: 1;
    display: flex;
}

.dc_PDTeech_center_top .dc_PDTeech_center_inner {
    border-bottom: 2px solid #000;
}

.dc_PDTeech_center_inner.dc_center_input {
    border-left: 2px solid #000;
    border-right: 2px solid #000;
}

.dc_PDTeech_center_inner {
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: center;
}

.dc_PDTeech_center .dc_PDTeech_left_input {
    width: 70px;
    height: 30px;
}

/*出血指数*/
#dcPanelBody .dc_FourValues4dialog_box {
    width: 100%;
    height: 130px;
    padding: 16px;
    box-sizing: border-box;
    display: flex;
    justify-content: space-between;
}

#dcPanelBody .dc_FourValues4dialog_center1 {
    width: 100px;
    padding: 0 10px;
}

#dcPanelBody .dc_FourValues4dialog_center2 {
    width: 150px;
}

#dcPanelBody .dc_FourValues4dialog_center2 input {
    width: 100%;
    height: 20px;
    text-align: center;
}

#dcPanelBody .dc_FourValues4dialog_center1 input {
    width: 100%;
    height: 20px;
    text-align: right;
}

#dcPanelBody .dc_FourValues4dialog_center1 input {
    width: 100%;
    height: 20px;
    text-align: right;
}

#dcPanelBody .dc_FourValues4dialog_center2 input {
    width: 100%;
    height: 20px;
    text-align: center;
}

#dcPanelBody .dc_FourValues4dialog_Value_center {
    height: 10px;
    margin: 22px 0;
}

#dcPanelBody .dc_FourValues4dialog_Value_center div {
    border-bottom: 10px solid #000;
}

/*表格边框*/
#dcPanelBody .dc_bordersShading_dialog {
    display: flex;
    flex-wrap: wrap;
}
#dcPanelBody .dc_bordersShading_dialog > label > span{
    display: inline-block;
    width: 72px;
    text-align: left;
}
#dcPanelBody .dc_bordersShading_border_box {
    width: 150px;
    flex: 1
}

#dcPanelBody .dc_bordershading_border_box_content {
    display: flex;
    padding-left: 26px;
    flex-direction: column
}

#dcPanelBody .dc_border_BorderTop_label {
    margin-right: 10px;
}

#dcPanelBody .dc_border_BorderBottom_label {
    margin-right: 10px;
}

#dcPanelBody .dc_border_BorderLeft_label {
    margin-right: 10px;
}

#dcPanelBody .dc_border_BorderRight_label {
    margin-right: 10px;
}

#dcPanelBody .dc_bordersShading_color_box {
    margin-top: 16px;
    width: 150px;
    margin-left: 20px;
    flex: 1;
}

#dcPanelBody .dc_bordershading_color_box_content {
    padding-left: 26px;
}

#dcPanelBody .dc_bordershading_color_box_content>div,
#dcPanelBody .dc_border_BorderTopColor_label,
#dcPanelBody .dc_border_BorderBottomColor_label,
#dcPanelBody .dc_border_BorderLeftColor_label,
#dcPanelBody .dc_border_BorderRightColor_label {
    display: flex;
    align-items: center;
    line-height: 20px;
    font-size: 12px;
}

#dcPanelBody .dc_border_BorderTopColor_label input,
#dcPanelBody .dc_border_BorderBottomColor_label input,
#dcPanelBody .dc_border_BorderLeftColor_label input,
#dcPanelBody .dc_border_BorderRightColor_label input {
    width: 0;
    height: 0;
    opacity: 0;
}

#dcPanelBody .dc_border_BorderTopColor_label > div,
#dcPanelBody .dc_border_BorderBottomColor_label > div,
#dcPanelBody .dc_border_BorderLeftColor_label > div,
#dcPanelBody .dc_border_BorderRightColor_label > div {
  width: 16px;
  height: 16px;
  border: 1px solid #ccc;
  cursor: pointer;
  margin-left: 4px;
}



#dcPanelBody .dc_BorderStyle_label,
#dcPanelBody .dc_BorderWidth_label,
#dcPanelBody .dc_Apply_label {
    margin-top: 16px;
    width: 100%;
}

#dcPanelBody .dc_BorderWidth_label span.dcTitle-text {
    width: 72px;
    display: inline-block;
}

#dcPanelBody .dc_BorderStyle_label select,
#dcPanelBody .dc_Apply_label select,
#dcPanelBody .dc_BorderWidth_label input {
    width: 150px;
}

/*插入表格*/
#dcPanelBody .dc_insertTable_content {
    padding-left: 10px;
    margin-bottom: 10px;
}

#dcPanelBody .dc_inserttable_TableID_label,
#dcPanelBody .dc_inserttable_RowCount_label,
#dcPanelBody .dc_inserttable_ColumnCount_label {
    margin-bottom: 11px;
    display: flex;
}

#dcPanelBody .dc_inserttable_TableID_label h6,
#dcPanelBody .dc_inserttable_RowCount_label h6,
#dcPanelBody .dc_inserttable_ColumnCount_label h6 {
    font-size: 12px;
    font-weight: 600;
}

#dcPanelBody .dc_inserttable_TableID_label input,
#dcPanelBody .dc_inserttable_RowCount_label input,
#dcPanelBody .dc_inserttable_ColumnCount_label input {
    width: 100px;
}

#dcPanelBody .dc_Box.dc_inserttable_Box_footer {
    width: 100%;
    height: auto;
}

#dcPanelBody #dc_tableBox {
    height: 121px;
    width: 100%;
}

/*拆分单元格*/
#dcPanelBody .dc_splitCell_dialog {
    padding: 10px;
}

#dcPanelBody .dc_splitCell_dialog label {
    margin-bottom: 11px;
    display: flex;
}

#dcPanelBody .dc_splitCell_dialog h6 {
    font-size: 12px;
    font-weight: 600;
}

#dcPanelBody .dc_splitCell_dialog input {
    width: 100px;
}

/*字符套圈*/
#dcPanelBody .dc_CharacterCircle_dialog label {
    line-height: 26px;
}

/*特殊字符*/
#dcPanelBody .dc_SpecialCharacters {
    margin-left: 0px;
}

#dcPanelBody .dc_charSpanDomItem {
    white-space: nowrap;
}

/*表单模式*/
#dcPanelBody.dc_formMode label {
    display: block;
    margin-bottom: 10px;
}

/*内容保护*/
#dcPanelBody.dc_contentProtectedMode label {
    display: block;
    margin-bottom: 10px;
}

/*用户登录*/
#dcPanelBody.dc_login_dialog label {
    margin-bottom: 11px;
}

#dcPanelBody.dc_login_dialog label span {
    font-size: 12px;
    font-weight: 600;
}

/*段落*/
#dcPanelBody.dc_paragraph .dc_paragraph_paragraphStyle_label {
    display: flex;
}
.dc_paragraph_paragraphStyle_label_span{
    display: inline-block;
    width: 50px;
}

#dcPanelBody.dc_paragraph .dc_paragraph_paragraphStyle_label #dc_paragraphStyle {
    width: 140px;
    margin-left: 5px;
}

#dcPanelBody.dc_paragraph .dc_Box {
    flex: 1;
    width: 100%;
    height: auto;
    margin-bottom: 10px;
}

#dcPanelBody.dc_paragraph .dc_label_box {
    width: 100%;
    margin: 10px 0;
    display: flex;
    flex-wrap: wrap;
}

#dcPanelBody.dc_paragraph .dc_label_box label {
    width: 100%;
}

#dcPanelBody.dc_paragraph .dc_label_box label input {
    width: 148px;
    margin-left: 16px;
}

#dcPanelBody.dc_paragraph .dc_label_box_span {
    width: 64px;
    display: inline-block;
    margin-top: 10px;
}
#dcPanelBody.dc_paragraph .dc-data-model{
    color: red;
}
#dcPanelBody.dc_paragraph .dc_paragraph_paragraphStyle_label span {
    margin-right: 10px;
}

#dcPanelBody.dc_paragraph .dc_paragraph_paragraphStyle_label select,
#dcPanelBody.dc_paragraph #dc_LineSpacingBox input {
    width:140px;
}

#dcPanelBody.dc_paragraph #dc_LineSpacingBox {
    flex: 1;
    display: flex;
    align-items: center;
    margin-top: 10px;
}

#dcPanelBody.dc_paragraph #dc_LineSpacingBox span {
    margin-right: 10px;
}


/*表格属性*/
#dcPanelBody.dc_tableDialog .dcTitle-text {
    width: 108px;
}

#dcPanelBody.dc_tableDialog .dc_table_AllowUserToResizeColumns_box,
#dcPanelBody.dc_tableDialog .dc_table_AllowUserInsertRow_box,
#dcPanelBody.dc_tableDialog .dc_table_AllowUserDeleteRow_box,
#dcPanelBody.dc_tableDialog .dc_table_Deleteable_box,
#dcPanelBody.dc_tableDialog .dc_table_CompressOwnerLineSpacing_box {
    margin-top: 10px;
}

#dcPanelBody.dc_tableDialog .dc_table_buttons_box {
    margin-top: 10px;
    display: flex;
    justify-content: space-between;
}

#dcPanelBody.dc_tableDialog .dc_table_VisibleExpression_label,
#dcPanelBody.dc_tableDialog .dc_table_PrintVisibilityExpression_label {
    display: flex;
    margin-bottom: 4px;
}

#dcPanelBody.dc_tableDialog .dc_table_VisibleExpression_label span,
#dcPanelBody.dc_tableDialog .dc_table_PrintVisibilityExpression_label span {
    width: 160px;
}

#dcPanelBody.dc_tableDialog .dc_table_VisibleExpression_label input,
#dcPanelBody.dc_tableDialog .dc_table_PrintVisibilityExpression_label input {
    flex: 1;
}

#dcPanelBody.dc_tableDialog .dc_visible_expression {
    margin-left: 10px;
}

#dcPanelBody.dc_tableDialog .dc_Box.dc_table_ValueBinding_box {
    margin-top: 20px;
}

#dcPanelBody.dc_tableDialog .dc_Box.dc_table_ValueBinding_box .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableDialog .dc_tab3Content {
    margin-top: 10px;
}

#dcPanelBody.dc_tableDialog .dc_table_DataSource_label,
#dcPanelBody.dc_tableDialog .dc_table_BindingPath_label {
    width: 100%;
    display: flex;
}

#dcPanelBody.dc_tableDialog .dc_table_DataSource_label span,
#dcPanelBody.dc_tableDialog .dc_table_BindingPath_label span {
    width: 108px;
}

#dcPanelBody.dc_tableDialog .dc_table_DataSource_label input,
#dcPanelBody.dc_tableDialog .dc_table_BindingPath_label input {
    flex: 1;
}

#dcPanelBody.dc_tableDialog .dc_table_BindingPath_label {
    margin-top: 5px;
}

#dcPanelBody.dc_tableDialog .dc_Box.dc_table_color_box {
    margin-top: 20px;
}

#dcPanelBody.dc_tableDialog .dc_Box.dc_table_color_box .dc_title {
    margin-top: 0;
}



#dcPanelBody.dc_tableDialog #dc_TableBackground label {
    display: flex;
    align-items: center;
}

#dcPanelBody.dc_tableDialog #dc_TableBackgroundText_box{
    width: 16px;
    height: 16px;
    border: 1px solid #ccc;
}

#dcPanelBody.dc_tableDialog #dc_TableBackgroundText {
    width: 0;
    height: 0;
    opacity: 0;
}

#dcPanelBody.dc_tableDialog .dcBody-content.dc_Box.dc_table_custom_attr_box {
    margin-top: 20px;
}

#dcPanelBody.dc_tableDialog .dcBody-content.dc_Box.dc_table_custom_attr_box .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableDialog #dc_attr-box {
    margin-bottom: 10px;
    margin-right: 10px;
}

/*单元格属性*/
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_algin_box {
    display: flex;
}

#dcPanelBody.dc_tableCellElementBox .dc_flex.dc_table_cell_Id_label .dcTitle-text,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_ContentReadonly_label .dcTitle-text,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_CloneType_label .dcTitle-text,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_AutoFixFontSizeMode_label .dcTitle-text,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_ValueExpression_label .dcTitle-text,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_PrintVisibility_label .dcTitle-text,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_EnablePermission_label .dcTitle-text,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_algin_box .dcTitle-text,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_MoveFocusHotKey_label .dcTitle-text {
    width: 100px;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_algin_box_container {
    width: 256px;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_algin_box_container .dc_table_cell_Align_label,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_algin_box_container .dc_table_cell_VerticalAlign_label {
    width: 38%;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_algin_box_container .dc_table_cell_Align_select,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_algin_box_container .dc_table_cell_VerticalAlign_select {
    width: 60%;
    cursor: no-drop;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_algin_box_container .dc_table_cell_VerticalAlign_select {
    margin-bottom: 5px;
}

#dcPanelBody.dc_tableCellElementBox .dc_Box.dc_table_cell_padding_box {
    margin-top: 20px;
}

#dcPanelBody.dc_tableCellElementBox .dc_Box.dc_table_cell_padding_box .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_padding_box_container {
    display: flex;
    flex-wrap: wrap;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_padding_box_container label {
    width: 100%;
    display: flex;
    margin-bottom: 5px;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_padding_box_container label span {
    display: inline-block;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_padding_box_container label input {
    width: 110px;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_padding_box_container dc_table_cell_PaddingLeft_label,
#dcPanelBody.dc_tableCellElementBox .dc_table_cell_padding_box_container dc_table_cell_PaddingRight_label {
    margin-top: 5px;
}

#dcPanelBody.dc_tableCellElementBox #dc_BorderRenderVisibility {
    margin-top: 20px;
}

#dcPanelBody.dc_tableCellElementBox #dc_BorderRenderVisibility .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_border_box_container {
    display: flex;
    flex-wrap: wrap;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_border_box_container label {
    width: 30%;
    display: flex;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_border_box_container label input {
    margin-right: 5px;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_border_wring {
    color: red;
    margin-top: 5px;
    font-size: 12px;
}

#dcPanelBody.dc_tableCellElementBox #dc_ContentRenderVisibility {
    margin-top: 20px;
}

#dcPanelBody.dc_tableCellElementBox #dc_ContentRenderVisibility .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_ContentRenderVisibility_box_container {
    display: flex;
    flex-wrap: wrap;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_ContentRenderVisibility_box_container label {
    width: 30%;
    display: flex;
}

#dcPanelBody.dc_tableCellElementBox .dc_table_cell_ContentRenderVisibility_box_container label input {
    margin-right: 5px;
}

#dcPanelBody.dc_tableCellElementBox .dc_Box.dc_table_cell_Data_box {
    margin-top: 20px;
}

#dcPanelBody.dc_tableCellElementBox .dc_Box.dc_table_cell_Data_box .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableCellElementBox .dc_tab3Content {
    display: flex;
    flex-wrap: wrap;
}

#dcPanelBody.dc_tableCellElementBox .dc_tab3Content label {
    width: 100%;
    display: flex;
    margin: 4px 0;
}

#dcPanelBody.dc_tableCellElementBox .dc_tab3Content label span {
    width: 26%;
    display: inline-block;
}

#dcPanelBody.dc_tableCellElementBox .dc_tab3Content label input {
    width: 70%;
}

#dcPanelBody.dc_tableCellElementBox .dc_Box.dc_table_cell_color_box {
    margin-top: 20px;
}
#dcPanelBody.dc_tableCellElementBox  .dc_TableCellBackgroundText_label{
    display: flex;
    align-items: center;
}
#dcPanelBody.dc_tableCellElementBox .dc_Box.dc_table_cell_color_box  #dc_TableCellBackgroundText_box{
    width:16px;
    height: 16px;
    border: 1px solid #ccc;
}
#dcPanelBody.dc_tableCellElementBox .dc_Box.dc_table_cell_color_box #dc_TableCellBackgroundText{
    width: 0;
    height: 0;
    opacity: 0;
}
#dcPanelBody.dc_tableCellElementBox .dc_Box.dc_table_cell_color_box .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableCellElementBox #dc_attr-box {
    margin-top: 20px;
    margin-bottom: 10px;
    margin-right: 10px;
}

#dcPanelBody.dc_tableCellElementBox #dc_attr-box .dc_title {
    margin-top: 0;
}

/*表格行*/
#dcPanelBody.dc_tableRow .dc_flex {
    display: flex;
    align-items: center;
}

#dcPanelBody.dc_tableRow .dc_flex span {
    width: 112px;
}

#dcPanelBody.dc_tableRow .dc_flex label {
    margin: 4px 0;
}

#dcPanelBody.dc_tableRow .dc_full {
    width: 100%;
}
#dcPanelBody.dc_tableRow .dc_full_SpecifyHeight{

}
#dcPanelBody.dc_tableRow .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableRow .dc_tab3Content label {
    margin: 4px 0;
}
#dcPanelBody.dc_tableRow .dc_tab3Content span{
    width:112px;
}
#dcPanelBody.dc_tableRow .dc_Box {
    margin-top: 20px;
}

#dcPanelBody.dc_tableRow .dc_Box .dc_title {
    margin-top: 0;
}
#dcPanelBody.dc_tableRow .dc_Box span{
    display: inline-block;
    width: 112px;
}
#dcPanelBody.dc_tableRow .dc_Box input[type="text"]{
    display: inline-block;
    width: 170px;
}
#dcPanelBody.dc_tableRow #dc_TableRowBackgroundText{
    width: 0;
    height: 0;
    opacity: 0;
}
#dcPanelBody.dc_tableRow #dc_TableRowBackgroundText_box{
    width: 16px;
    height: 16px;
    border: 1px solid #ccc;
}

/*表格列*/
#dcPanelBody.dc_tableColumn .dc_Box {
    margin-top: 20px;
}

#dcPanelBody.dc_tableColumn .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_tableColumn .dc_tab3Content label {
    margin: 4px 0;
    display: flex;
    align-items: center;
}

#dcPanelBody.dc_tableColumn .dc_tab3Content label span {
    width: 82px;
    display: inline-block;
}

#dcPanelBody.dc_tableColumn input[type="checkbox"] {
    margin-right: 5px;
}
#dcPanelBody.dc_tableColumn #dc_TableColumnBackground,
#dcPanelBody.dc_tableColumn #dc_TableColumnBackground label{
    display: flex;
    aligin-items: center;
}
#dcPanelBody.dc_tableColumn #dc_TableColumnBackgroundText_box{
    width: 16px;
    height: 16px;
    border: 1px solid #ccc;
}
#dcPanelBody.dc_tableColumn #dc_TableColumnBackgroundText{
    width: 0;
    height: 0;
    opacity: 0;
}
/*单元格网格线*/
#dcPanelBody.dc_cellGridline .dc_cellGridlineBox {
    margin-top: 20px;
}

#dcPanelBody.dc_cellGridline .dc_cellGridlineContent {
    margin-left: 16px;
}

#dcPanelBody.dc_cellGridline .dc_title {
    margin-top: 0;
}

#dcPanelBody.dc_cellGridline .dc_cellGridlineBox-span {
    display: inline-block;
}

#dcPanelBody.dc_cellGridline .dc_cellGridlineBox-input {
    height: 20px;
}

/*单元格斜分线*/
#dcPanelBody.dc_cellDiagonalLine .dc_slantsplitlinestyle-box {
    display: flex;
    align-items: center;
}

#dcPanelBody.dc_cellDiagonalLine .dc_cellDiagonalLineBox {
    margin-top: 10px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_slantsplitlinestyle-item {
    margin-top: 10px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_slantsplitlinestyle-item span {
    margin-left: 10px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_None p {
    width: 110px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(26deg);
    margin-top: 24px;
    margin-left: -6px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_TopLeftOneLine p {
    width: 110px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(26deg);
    margin-top: 24px;
    margin-left: -6px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_TopLeftTwoLines p:nth-child(1) {
    width: 104px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(17deg);
    margin-top: 15px;
    margin-left: -3px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_TopLeftTwoLines p:nth-child(2) {
    width: 80px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(36deg);
    margin-top: 8px;
    margin-left: -6px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_TopRightOneLine p {
    width: 110px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(-26deg);
    margin-top: 23px;
    margin-left: -6px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_TopRightTwoLines p:nth-child(1) {
    width: 104px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(-17deg);
    margin-top: 15px;
    margin-left: -3px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_TopRightTwoLines p:nth-child(2) {
    width: 80px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(-36deg);
    margin-top: 8px;
    margin-left: 25px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_BottomRightOneLine p {
    width: 110px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(26deg);
    margin-top: 24px;
    margin-left: -6px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_BottomRigthTwoLines p:nth-child(1) {
    width: 89px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(33deg);
    margin-top: 24px;
    margin-left: 17px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_BottomRigthTwoLines p:nth-child(2) {
    width: 100px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(18deg);
    margin-top: 6px;
    margin-left: -2px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_BottomLeftOneLine p {
    width: 110px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(-26deg);
    margin-top: 23px;
    margin-left: -6px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_BottomLeftTwoLines p:nth-child(1) {
    width: 92px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(-32deg);
    margin-top: 24px;
    margin-left: -8px;
}

#dcPanelBody.dc_cellDiagonalLine .dc_BottomLeftTwoLines p:nth-child(2) {
    width: 102px;
    height: 1px;
    border-bottom: 1px solid #858585;
    transform: rotate(345deg);
    margin-top: 9px;
    margin-left: -2px;
}

/*图片编辑*/
/* 颜色选择器的样式 */
#dcPanelBody.dc_imgEdit input[type=color]{
    width: 30px;
    height: 20px;
}
/* tab页样式 */
#dcPanelBody.dc_imgEdit #dc_wrap{
    width: 100%;
    height: 100%;
    overflow: hidden;
    paddin:0;
    margin:0;
    box-sizing: border-box;
}
#dcPanelBody.dc_imgEdit .dc_list {
    list-style: none;
    display: flex;
    padding: 0;
    margin: 0;
    box-sizing: border-box;
}

#dcPanelBody.dc_imgEdit .dc_list li {
    margin: 0 4px -1px 0;
    position: relative;
    border: 0;
    padding: 0 10px;
    height: 32px;
    line-height: 30px;
    border-radius: 5px 5px 0 0;
    background: linear-gradient(to bottom, #EFF5FF 0, #E0ECFF 100%);
    cursor: pointer;
}

#dcPanelBody.dc_imgEdit .dc_list li.dc_imgEdit_active {
    font-weight: bold;
    background: #ffffff;
}

/* 盒子样式 */
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content {
    margin-top: 0;
    border: none;
    padding: 4px 14px;
    width: 180px;
    height: 100%;
    overflow-y: auto;
    border-left: 1px solid #ccc;
    border-radius: 0;
}

#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content .dc_list_box {

}

#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content #dc_back_img_color{
    height: 20px;
    width: 80px;
    font-size: 12px;
    color: #606266;
    border-color: #606266;
    box-sizing: border-box;
}

#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content .dc_imgEdit_fill_label,
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content .dc_imgEdit_stroke_label {
    display: flex;
    flex-direction: column;
    background: #fff;
    border: 1px solid #dcdfe6;
    color: #606266;
    width: 40px;
height: 30px;
    align-items: center;
    justify-content: center;
    padding: 4px;
    box-sizing: border-box;
    margin-right: -1px;
    margin-top: -1px
}
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content .dc_imgEdit_fill_label:hover,
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content .dc_imgEdit_stroke_label:hover {
  background: #ecf5ff;
}

#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content .dc_imgEdit_fill_label  #dc_fill_color,
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content .dc_imgEdit_stroke_label  #dc_stroke_color{
    width: 0;
    height: 0;
    padding: 0;
    margin: 0;
    border: none;
    opacity: 0;
    margin-right: 4px;
}
#dcPanelBody.dc_imgEdit .dc_mouseMode,
#dcPanelBody.dc_imgEdit .dc_mouseModeBtn{
    display:flex;
    align-items:center;
    justify-content:center;
    margin-right: -1px;
    margin-top: -1px;
    padding: 4px;
    box-sizing: border-box;
     background: #fff;
    border: 1px solid #dcdfe6;
    color: #606266;
    width: 40px;
    height: 30px;
}

#dcPanelBody.dc_imgEdit .dc_mouseModeBtn[disabled]{
    background: #f5f5f5;
    cursor: not-allowed;
}
#dcPanelBody.dc_imgEdit .dc_mouseMode_label{
    box-sizing: border-box;
    color: #606266;
    display: flex;
    align-items: center;
    margin-top:2px;
}
#dcPanelBody.dc_imgEdit  .dc_mouseMode_box{
  display: flex;
  align-items: center;
}


#dcPanelBody.dc_imgEdit  .dc_mouseMode_box_title{
    color: #8f8f8f;
    font-size: 14px;
    margin: 4px 0;
    display: flex;
    align-items: center;
}
#dcPanelBody.dc_imgEdit   .dc_mouseMode_box_title .dc_mouseMode_box_title_line{
    flex:1;
    height:1px;
    background-color:#8f8f8f;
   
}
#dcPanelBody.dc_imgEdit   .dc_mouseMode_box_title .dc_mouseMode_box_title_text{
    margin:0 10px;
}

#dcPanelBody.dc_imgEdit .dc_mouseMode .icon path{
    fill: #606266;
}

#dcPanelBody.dc_imgEdit .dc_mouseMode.active {
    background: #ecf5ff;
}
#dcPanelBody.dc_imgEdit .dc_mouseMode:hover{
    background: #ecf5ff;
}
#dcPanelBody.dc_imgEdit .dc_mouseMode.active .icon path{
    fill: #409EFF;
}
#dcPanelBody.dc_imgEdit .dc_mouseMode:hover .icon path{
    fill: #409EFF;
}

#dcPanelBody.dc_imgEdit select{
    width: 100px;
}

#dcPanelBody.dc_imgEdit #dc_canvas_box{
    display: flex;
    flex-direction: column;
    height: 100%;
}

#dcPanelBody.dc_imgEdit #dc_wrap {
    flex: 1;
    overflow: hidden;
    display: flex;
}
#dcPanelBody.dc_imgEdit #dc_wrap #dc_wrap_imgBox{
   flex:1;
    overflow: auto;
}
#dcPanelBody.dc_imgEdit .dc_imgEditFont_box{
margin-top: 6px;
}
#dcPanelBody.dc_imgEdit .dc_imgEditFont_box label{
    margin-right: 10px;
    display: flex;
    align-items: center;
    text-align: center;
}

#dcPanelBody.dc_imgEdit .dc_font_base_label{
    display: flex;
    align-items: center;
}
#dcPanelBody.dc_imgEdit .dc_font_base_label .dc_mouseMode{
    background: #fff;
    border: 1px solid #606266;
    color: #409eff;
    border-radius: 2px;
    border-bottom-left-radius: 0;
    border-top-left-radius: 0;
    width: 37px;
    height: 20px;
    font-size: 12px;
    margin-left: -1px;
}
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content #dc_font_base_select,
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content #dc_fontSize,
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content #dc_fontFamily,
#dcPanelBody.dc_imgEdit .dc_Box.dc_imgEdit_Content #dc_fontColor{
height: 20px;
    width: 80px;
font-size: 12px;
color: #606266;
border-color: #606266;
padding: 0 4px;
box-sizing: border-box;
}



#dcPanelBody.dc_imgEdit .dc_img_tools_one {
    width: 100%;
    height: 24px;
    display: flex;
    align-items: center;
    justify-content: flex-end;
    padding-bottom: 10px;
    box-sizing: border-box;
}
/*图片对话框全屏按钮*/
#dcPanelBody.dc_imgEdit .dc_img_tools_one #dc_imgFullScreen,
#dcPanelBody.dc_imgEdit .dc_img_tools_one #dc_imgCancelFullScreen {
    color: #0367ce;
    cursor: pointer;
}

#dcPanelBody.dc_imgEdit .dc_img_tools_one #dc_imgCancelFullScreen {
    display: none;
}
#dcPanelBody.dc_imgEdit  #dc_img_zoom_box{
    display: flex;
    align-items: center;
    padding-right: 8px;
    box-sizing: border-box;
}
#dcPanelBody.dc_imgEdit  #dc_img_zoom_box #dc_img_zoomIn,
#dcPanelBody.dc_imgEdit  #dc_img_zoom_box #dc_img_zoomOut {
   font-size: 18px;
   cursor: pointer;
}

#dcPanelBody.dc_imgEdit #zoomSelect {
    width: 104px;
    border-radius: 2px;
    height: 20px;
    font-size: 12px;
    color: #606266;
    border-color: #dcdfe6;
    padding: 0 4px;
    box-sizing: border-box;
    text-align: center;
}
#dcPanelBody.dc_imgEdit .dc_img_tools_full_box{
    border-left: 1px solid #ccc;
    padding-left: 8px;
}

/*前景色*/
#dcPanelBody.dc_ColorElement .dc_color_box input {
    width: 84px;
    cursor: pointer;
}

/*背景色*/
#dcPanelBody.dc_BackColorElement .dc_color_box input {
    width: 84px;
    cursor: pointer;
}

/*自定义属性*/
#dc_dialogContainer2 .dc_attribute_dialog_box {
    width: 100%;
}
.dc_attribute_dialog_box table input{
    border: none;
}

#dc_dialogContainer2 .dc_attribute_dialog_title {
    text-align: right;
}

#dc_dialogContainer2 #dc_addButton {
    margin-right: 4px;
    cursor: pointer;
    color: #15428b;
}

#dc_dialogContainer2 #dc_deletButton {
    cursor: pointer;
    color: #15428b;
}

#dc_dialogContainer2 .dc_currentTableDom {
    width: 100%;
}

#dc_dialogContainer2 .dc_currentTableDom th {
    border-width: 1px;
}

#dc_dialogContainer2 .dc_input-dom input {
    width: 100%;
    height: 100%;
    border: none;
}

#dc_dialogContainer2 .dc_delete-button {
    cursor: pointer;
}

#dc_dialogContainer2 .dc_ons-2 input,
#dc_dialogContainer2 .dc_ons-3 input {
    width: 100%;
    height: 100%;
    border: none;
}

#dc_dialogContainer2 .dc_ons-4.dc_delete-button {
    cursor: pointer;
}

#dc_dialogContainer .dc_underline_color_box{
    margin-bottom: 10px;
}
#dc_dialogContainer #dc_underline_color,
#dc_dialogContainer #dc_underline_style{
    width: 100px;
    height: 20px;
    padding: 0;
    margin: 0;
}
/* 打印背景色 */
#dc_PrintBackColor_content{
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
}
/* 承载控件 */
#dcPanelBody .dc_ControlHost_content {
    width: 100%;
    height: 100%;
    overflow: auto;
    padding: 10px;
    box-sizing: border-box;
}
#dcPanelBody .dc_controlHost_item {
    width: 100%;
    margin-bottom: 10px !important;
    display: flex;
    align-items: center;
}
#dcPanelBody .dc_controlHost_item_title {
    width: 84px;
}
#dcPanelBody .dc_dc_controlHost_item_value {
    flex: 1;
    height: 20px;
}
/* 选择医学表达式的对话框 */
.dc_MedicalExpressionChooseDiv_active{
    background-color: #d7e4f2;
}
.dc_MedicalExpressionChooseDiv{
    width: 100%;
    display: flex;
    align-items: center;
    padding: 10px;
    border-bottom: 1px solid #ccc;
    font-size: 14px;
    color: #333;
    cursor: pointer;
}
.dc_MedicalExpressionChooseDiv img{
    width: 146px;
    display: inline-block;
    margin-right: 10px;
}
.dc_PropertyExpressionsBox{
    height: 500px;
    width: 500px;
    overflow: hidden;
    border-radius: 6px;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 10002;
    user-select: none;
    box-shadow: 0 1px 6px rgba(0, 0, 0, .15);
    background: #fff;
    box-sizing: border-box;
    font-size: 12px;
}
.dc_PropertyExpressionsBox {
    display: flex;
    flex-direction: column;
}

.dc_PropertyExpressionsBox_top {
    display: flex;
    justify-content: space-between;
    height: 28px;
    align-items: center;
    box-sizing: border-box;
    background: linear-gradient(to bottom, #ffffff 0, #eeeeee 100%);
    padding: 6px;
    position: relative;
    border-bottom: 1px solid #c6c6c6;
}

.dc_PropertyExpressionsBox_center {
    flex: 1;
    padding: 10px;
    box-sizing: border-box;
    overflow: auto;
    margin:5px 0;
}

.dc_PropertyExpressionsBox_bottom {
    width: 100%;
    height: 42px;
    padding: 0 10px;
    box-sizing: border-box;
    display: flex;
    justify-content: center;
    align-items: center;
    text-align: center;
    background: rgb(250, 250, 250);
    border-top: 1px solid #c6c6c6;
}

.dc_PropertyExpressionsTable_item,
.dc_PropertyExpressionsTable_item_header {
    width: 100%;
    display: flex;
    height: 40px;
    line-height: 40px;
    overflow: hidden;
    align-items: center;
    padding: 0 10px;
    box-sizing: border-box;
    border-bottom: 1px solid #E4E7ED;
    margin-top:-1px;
}
.dc_PropertyExpressionsTable_item_header .dc_PropertyExpressionsTable_item_left,
.dc_PropertyExpressionsTable_item_header .dc_PropertyExpressionsTable_input{
    background: #fff;
    font-weight: bold;
    color: #303133;
}
.dc_PropertyExpressionsTable_item:hover{
    background: #f5f7fa;
}
.dc_PropertyExpressionsTable_item:hover .dc_PropertyExpressionsTable_input{
    background: #FFF;
}


.dc_PropertyExpressionsBox_title {
    font-size: 12px;
    font-weight: bold;
    color: #0E2D5F;
    height: 16px;
    line-height: 16px;
    padding-left: 18px;
}

.dc_PropertyExpressionsBox_button {
    box-sizing: border-box;
    padding:6px 10px;
    margin: 0 10px;
    border-radius: 5px;
    background: linear-gradient(to bottom, #ffffff 0, #eeeeee 100%);
    border: 1px solid #bbb;
    color: #444;
}
.dc_PropertyExpressionsTable {
    border: 1px solid #E4E7ED;
    box-shadow: rgba(0, 0, 0, 0.1) 0px 2px 12px 0px;
    overflow: hidden;
}
.dc_PropertyExpressionsTable_item_left{
    border-right: 1px solid #E4E7ED;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
}
.dc_PropertyExpressionsTable_item_left,
.dc_PropertyExpressionsTable_input {
    flex: 1;
    height: 100%;
    font-size: 14px;
    color: #303133;
}

.dc_PropertyExpressionsTable_input::placeholder {
  font-size: 12px; 
  color: #909399;
}

.dc_PropertyExpressionsTable_input {
    display: inline-block;
    width: 100%;
    height: 30px;
    line-height: 30px;
    border: none;
    outline: none;
    font-size: 14px;
    padding: 0 10px;
    margin-left: 10px;
    box-sizing: border-box;
    background: #f5f7fa;
}

.dc_FourValues5_content{
    width: 100%;
    display: flex;
    align-items: flex-end;
}
.dc_FourValues5_input{
    width: 60px;
    height: 22px;
    margin-top: 10px;
}
.dc_FourValues5_line{
    width: 50px;
    height: 5px;
    background: #000;
    margin: 8px 5px !important;
}

`;
//线的样式列表
let DASHSTYLE = [
    {
        "name": "Solid",
        "show": "——"
    },
    {
        "name": "Dash",
        "show": "-------"
    },
    {
        "name": "Dot",
        "show": "▪▪▪▪▪▪▪"
    },
    {
        "name": "DashDot",
        "show": "-▪-▪-▪-"
    },
    {
        "name": "DashDotDot",
        "show": "-▪▪-▪▪-"
    },
];

//输入域列表格式
let LBSJ = [
    { id: "1", text: "None", Child: [] },
    { id: "2", text: "Numeric", Child: [{ id: "2-1", text: "0.00" }, { id: "2-2", text: "#.00" }, { id: "2-3", text: "c" }, { id: "2-4", text: "e" }, { id: "2-5", text: "f" }, { id: "2-6", text: "g" }, { id: "2-7", text: "r" }, { id: "2-8", text: "FormatedSize" }] },
    { id: "3", text: "Currency", Child: [{ id: "3-1", text: "00.00" }, { id: "3-2", text: "大写中文" }, { id: "3-3", text: "小写中文" }, { id: "3-4", text: "#.00" }, { id: "3-5", text: "c" }] },
    {
        id: "4", text: "DateTime", Child: [{ id: "4-1", text: "yyyy-MM-dd HH:mm:ss" }, { id: "4-2", text: "yyyy-MM-dd" }, { id: "4-3", text: "yyyy-MM-dd hh:mm:ss" }, { id: "4-4", text: "HH:mm:ss" }, { id: "4-5", text: "yyyy年MM月dd日" }, { id: "4-6", text: "yyyy年MM月dd日 HH时mm分ss秒" }, { id: "4-7", text: "d" },
        { id: "4-7", text: "D" }, { id: "4-8", text: "f" }, { id: "4-9", text: "F" }]
    },
    { id: "5", text: "String", Child: [{ id: "5-1", text: "trim" }, { id: "5-2", text: "normalizespace" }, { id: "5-3", text: "htmltext" }, { id: "5-4", text: "left,1" }, { id: "5-5", text: "right,1" }, { id: "5-6", text: "lower" }, { id: "5-7", text: "upper" }] }
];

//特殊字符
let SPECIALCHARACTERS = ["、", "。", "·", "ˉ", "ˇ", "¨", "〃", "々", "—", "～", "‖", "…", "‘", "’", "“", "”", "〔", "〕", "〈", "〉",
    "《", "》", "「", "」", "『", "』", "〖", "〗", "【", "】", "±", "×", "÷", "∶", "∧", "∨", "∑", "∏", "∪", "∩", "∈", "∷", "√", "⊥",
    "∥", "∠", "⌒", "⊙", "∫", "∮", "≡", "≌", "≈", "∽", "∝", "≠", "≮", "≯", "≤", "≥", "∞", "∵", "∴", "♂", "♀", "°", "′", "″",
    "℃", "＄", "¤", "￠", "￡", "‰", "§", "№", "☆", "★", "○", "●", "◎", "◇", "◆", "□", "■", "△", "▲", "※", "→", "←", "↑", "↓",
    "〓", "〡", "〢", "〣", "〤", "〥", "〦", "〧", "〨", "〩", "㊣", "㎎", "㎏", "㎜", "㎝", "㎞", "㎡", "㏄", "㏎", "㏑", "㏒", "㏕", "︰"];

//罗马字符
let ROMANCHARACTERS = ["ⅰ", "ⅱ", "ⅲ", "ⅳ", "ⅴ", "ⅵ", "ⅶ", "ⅷ", "ⅸ", "ⅹ", "Ⅰ", "Ⅱ", "Ⅲ", "Ⅳ", "Ⅴ", "Ⅵ", "Ⅶ", "Ⅷ", "Ⅸ", "Ⅹ",
    "Ⅺ", "Ⅻ"];

//数字字符
let NUMERICCHARACTERS = ["⒈", "⒉", "⒊", "⒋", "⒌", "⒍", "⒎", "⒏", "⒐", "⒑", "⒒", "⒓", "⒔", "⒕", "⒖", "⒗", "⒘", "⒙", "⒚", "⒛",
    "⑴", "⑵", "⑶", "⑷", "⑸", "⑹", "⑺", "⑻", "⑼", "⑽", "⑾", "⑿", "⒀", "⒁", "⒂", "⒃", "⒄", "⒅", "⒆", "⒇", "①", "②", "③", "④",
    "⑤", "⑥", "⑦", "⑧", "⑨", "⑩", "㈠", "㈡", "㈢", "㈣", "㈤", "㈥", "㈦", "㈧", "㈨", "㈩"];

//医学字符
let MEDICALCHARACTERS = ["RP", "P.O", "INJ.", "MIXT.", "TAD.", "SOL.", "CO.", "PR.", "I.D", "I.V", "I.V.GTT.", "IH", "IM", "O.M", "O.N", "HS.", "AM.", "PM.", "A.C.", "P.C.", "SOS.", "ST.", "QD", "BID", "TID", "QOD", "QH", "Q2H", "Q3H", "MCG", "MG", "G", "ML", "sig", "qd", "bid", "tid", "qid", "qh", "q2h", "q4h", "q6h", "qn", "qod", "biw", "hs", "am", "pm", "St", "DC", "prn", "sos", "ac", "pc", "12n", "12mn", "gtt", "ID", "IH", "IM", "IV", "aa", "et", "Rp.", "sig./S.", "St./Stat.", "Cit.", "s.o.s.", "p.r.n", "a.c.", "p.c.", "a.m.", "p.m.", "q.n.", "h.s.", "q.h.", "q.d.", "B.i.d.", "T.i.d.", "Q.i.d.", "q.4h.", "p.o.", "adus.int.", "adus.ext.", "H.", "im./M.", "iv./V.", "ivgtt.", "Inhal.", "O.D.", "O.L.", "O.S.", "O.U.", "No./N.", "s.s", "ug.", "mg.", "g.", "kg.", "ml.", "L.", "q.s", "Ad.", "Aq.", "Aq.dest.", "Ft.", "Dil", "M.D.S.", "Co./Comp.", "Mist", "Pulv.", "Amp.", "Emul.", "Syr.", "Tr.", "Neb.", "Garg.", "rtt./gutt.", "collyr.", "Ocul.", "Liq.", "Sol.", "Lot.", "Linim.", "Crem.", "Ung.", "Past.", "Ol.", "Enem.", "Supp.", "Tab.", "Pil.", "Caps.", "Inj.", "po", "im", "iv", "ivgtt", "qd", "bid", "tid", "qid", "q8h", "qn", "Rp", "sig", "prn"];

//恒牙牙位图数据==============start================
let NAMELIST = ["a1", "a3", "a5", "a7", "a10", "a12", "a14", "a16", "a17", "a19", "a21", "a23", "a26", "a28", "a30", "a32"];
let IDTYPELIST = ["Value", "a", "b", "c", "d", "e", "f"];
let IDLIST = ["Value1", "Value3", "Value5", "Value7", "Value10", "Value12", "Value14", "Value16", "Value17", "Value19", "Value21", "Value23", "Value26", "Value28", "Value30", "Value32"];
let PERMANENTTEETHTOP = [
    {
        idPrefix: 'a',
        parentId: '#dc_P-permanent-tooth',
        teethKey: 'P',
        isTop: true,
    },
    {
        idPrefix: 'b',
        parentId: '#dc_L-permanent-tooth',
        teethKey: 'L',
        isTop: true
    },
    {
        idPrefix: 'c',
        parentId: '#dc_B-permanent-tooth',
        teethKey: 'B',
        isTop: true
    },
    {
        idPrefix: 'd',
        parentId: '#dc_D-permanent-tooth',
        teethKey: 'D',
        isTop: true
    },
    {
        idPrefix: 'e',
        parentId: '#dc_O-permanent-tooth',
        teethKey: 'O',
        isTop: true
    },
    {
        idPrefix: 'f',
        parentId: '#dc_M-permanent-tooth',
        teethKey: 'M',
        isTop: true
    }];
let PERMANENTTEETHBOTTOM = [{
    idPrefix: 'a',
    parentId: '#dc_M-bottom-permanent-tooth',
    teethKey: 'M',
    isTop: false
},
{
    idPrefix: 'b',
    parentId: '#dc_O-bottom-permanent-tooth',
    teethKey: 'O',
    isTop: false
},
{
    idPrefix: 'c',
    parentId: '#dc_D-bottom-permanent-tooth',
    teethKey: 'D',
    isTop: false
},
{
    idPrefix: 'd',
    parentId: '#dc_B-bottom-permanent-tooth',
    teethKey: 'B',
    isTop: false
},
{
    idPrefix: 'e',
    parentId: '#dc_L-bottom-permanent-tooth',
    teethKey: 'L',
    isTop: false
},
{
    idPrefix: 'f',
    parentId: '#dc_P-bottom-permanent-tooth',
    teethKey: 'P',
    isTop: false
}];
//恒牙牙位图数据 ===========================end============================

//乳牙牙位图数据=============start===================
let TEETHPOSTIONTOPOBJ = [
    {
        idPrefix: 'a',
        parentId: '#dc_P-teeth-list',
        teethKey: 'P'
    },
    {
        idPrefix: 'b',
        parentId: '#dc_L-teeth-list',
        teethKey: 'L'
    },
    {
        idPrefix: 'c',
        parentId: '#dc_B-teeth-list',
        teethKey: 'B'
    },
    {
        idPrefix: 'd',
        parentId: '#dc_D-teeth-list',
        teethKey: 'D'
    },
    {
        idPrefix: 'e',
        parentId: '#dc_O-teeth-list',
        teethKey: 'O'
    },
    {
        idPrefix: 'f',
        parentId: '#dc_M-teeth-list',
        teethKey: 'M'
    },
];
let TEETHPOSTIONBOTTOMOBJ = [
    {
        idPrefix: 'a',
        parentId: '#dc_M-bottom-teeth-list',
        teethKey: 'M'
    },
    {
        idPrefix: 'b',
        parentId: '#dc_O-bottom-teeth-list',
        teethKey: 'O'
    },
    {
        idPrefix: 'c',
        parentId: '#dc_D-bottom-teeth-list',
        teethKey: 'D'
    },
    {
        idPrefix: 'd',
        parentId: '#dc_B-bottom-teeth-list',
        teethKey: 'B'
    },
    {
        idPrefix: 'e',
        parentId: '#dc_L-bottom-teeth-list',
        teethKey: 'L'
    },
    {
        idPrefix: 'f',
        parentId: '#dc_P-bottom-teeth-list',
        teethKey: 'P'
    },
];
//乳牙牙位图数据=============end===================
//段落
let BULLETEDLIST = [{
    data: "无",
    title: "None",
    value: 0
}, {
    data: "1.2.3.4",
    title: "ListNumberStyle",
    value: 1
}, {
    data: "1,2,3,4",
    title: "ListNumberStyleArabic1",
    value: 2
}, {
    data: "1）2）3）4）",
    title: "ListNumberStyleArabic2",
    value: 3
}, {
    data: "1、2、3、4、",
    title: "ListNumberStyleArabic3",
    value: 15
}, {
    data: "1、2、3、4、",
    title: "ListNumberStyleArabic3",
    value: 3
}, {
    data: "a）b）c）d）",
    title: "ListNumberStyleLowercaseLetter",
    value: 4
}, {
    data: "i）ii）iii）iv）",
    title: "ListNumberStyleLowercaseRoman",
    value: 5
}, {
    data: "①, ②, ③ ,④,",
    title: "ListNumberStyleNumberInCircle",
    value: 6
}, {
    data: "一.二.三.四",
    title: "ListNumberStyleSimpChinNum1",
    value: 7
}, {
    data: "一）二）三）四",
    title: "ListNumberStyleSimpChinNum2",
    value: 8
}, {
    data: "壹.贰.叁.肆",
    title: "ListNumberStyleTradChinNum1",
    value: 9
}, {
    data: "壹）贰）叁）肆",
    title: "ListNumberStyleTradChinNum2",
    value: 10
}, {
    data: "A）B）C）D",
    title: "ListNumberStyleUppercaseLetter",
    value: 11
}, {
    data: "Ⅰ）Ⅱ）Ⅲ）Ⅳ",
    title: "ListNumberStyleUppercaseRoman",
    value: 12
}, {
    data: "甲,乙,丙,丁",
    title: "ListNumberStyleZodiac1",
    value: 13
}, {
    data: "子,丑,寅,卯",
    title: "ListNumberStyleZodiac2",
    value: 14
}, {
    data: "● Bulletedlist",
    title: "BulletedList",
    value: 10000
}, {
    data: "■ Bulletedlistblock",
    title: "BulletedListBlock",
    value: 10001
}, {
    data: "◆ Bulletedlistdiamond",
    title: "BulletedListDiamond",
    value: 10002

}, {
    data: "✔ BulletedListCheck ",
    title: "BulletedListCheck ",
    value: 10003

}, {
    data: "➢ BulletedListRightArrow",
    title: "BulletedListRightArrow",
    value: 10004
}, {
    data: "◇ BulletedListHollowStar",
    title: "BulletedListHollowStar",
    value: 10005
}];
//模拟数据
let MOCKARRAY = [
    { name: "AboutControl", description: "关于" },
    { name: 'AdministratorViewMode', description: "开启管理员模式" },
    { name: 'AlignBottomCenter', description: "单元格底端居中对齐" },
    { name: 'AlignBottomLeft', description: "单元格底端左对齐" },
    { name: 'AlignBottomRight', description: "单元格底端右对齐" },
    { name: 'AlignCenter', description: "段落居中对齐" },
    { name: 'AlignDistribute', description: "段落分散对齐" },
    { name: 'AlignLeft', description: "段落左对齐" },
    { name: 'AlignMiddleCenter', description: "单元格垂直水平居中" },
    { name: 'AlignMiddleLeft', description: "单元格垂直水平左对齐" },
    { name: 'AlignMiddleRight', description: "单元格垂直水平右对齐" },
    { name: 'AlignRight', description: "段落右对齐" },
    { name: 'AlignTopCenter', description: "单元格顶端居中对齐" },
    { name: 'AlignTopLeft', description: "单元格顶端居左对齐" },
    { name: 'AlignTopRight', description: "单元格顶端居右对齐" },
    { name: 'allfontname', description: "全局修改字体名称" },
    { name: 'allfontsize', description: "全局修改字体大小" },
    { name: 'AttachCurrentUserTrackToBodyContent', description: "对文档中没有痕迹的信息附加当前用户的痕迹。" },
    { name: 'BackColor', description: "设置背景色" },
    { name: 'BackgroundMode', description: "设置后台运行模式。" },
    { name: 'Backspace', description: "退格删除" },
    { name: 'Bold', description: "对选中的内容设置粗体" },
    { name: 'BulletedList', description: "设置圆点列表" },
    { name: 'CharacterCircle', description: "字符套圈" },
    { name: 'CleanViewMode', description: "清洁视图模式" },
    { name: 'ClearAllFieldValue', description: "清空文档中所有输入域内容" },
    { name: 'ClearAllUserTrace', description: "清空文档中所有用户书写的痕迹" },
    { name: 'ClearBodyContent', description: "清空文档中正文的内容" },
    { name: 'ClearCurrentFieldValue', description: "清空当前光标处输入域的内容" },
    { name: 'ClearJumpPrintMode', description: "退出续打模式" },
    { name: 'ClearUndo', description: "清除撤销信息操作列表" },
    { name: 'ClearUserTrace', description: "清除用户留下的痕迹" },
    { name: 'ClearValueValidateResult', description: "清空校验结果" },
    { name: 'Color', description: "设置前景色" },
    { name: 'CommitUserTrace', description: "提交所有的用户痕迹信息效果" },
    { name: 'ComplexViewMode', description: "复杂（留痕）视图模式" },
    { name: 'ContentProtect', description: "设置内容保护" },
    { name: 'ConvertContentToField', description: "将选中的文档内容转换成输入域对象" },
    { name: 'ConvertFieldToContent', description: "将当前光标所在的输入域转成普通文本" },
    { name: 'ConvertTextContentToElementLabel', description: "文档内容转换为表单元素的标签文本" },
    { name: 'Copy', description: "复制" },
    { name: 'CopyAsText', description: "纯文本复制" },
    { name: 'Cut', description: "剪切" },
    { name: 'DCInsertImage', description: "插入图片" },
    { name: 'Delete', description: "删除" },
    { name: 'DeleteAbsolute', description: "物理删除" },
    { name: 'DeleteAllComment', description: "删除所有批注" },
    { name: 'DeleteComment', description: "除当前光标处的批注" },
    { name: 'DeleteField', description: "删除文本输入域" },
    { name: 'DeleteLine', description: "删除当前光标处的一行文本" },
    { name: 'DeleteRedundant', description: "删除文档后面多余的空白行" },
    { name: 'DeleteSection', description: "删除当前光标处文档节" },
    { name: 'DesignMode', description: "设计模式" },
    { name: 'DisplayWhole', description: "显示所有隐藏的输入域" },
    { name: 'DocumentDefaultFont', description: "设置默认字体样式" },
    { name: 'DocumentValueValidate', description: "文档中的数据进行校验" },
    { name: 'DocumentValueValidateWithCreateDocumentComments', description: "批注式校验" },
    { name: 'ElementProperties', description: "元素属性对话框" },
    { name: 'EmphasisMark', description: "将选中的内容设置着重号" },
    { name: 'ExecuteCommand', description: "命令对话框" },
    { name: 'FieldSpecifyWidth', description: "设置输入域固定宽度" },
    { name: 'FileNew', description: "新建空白文档" },
    { name: 'FileOpen', description: "加载本地文档" },
    { name: 'FirstLineIndent', description: "首行缩进" },
    { name: 'Font', description: "选中一段内容设置字体" },
    { name: 'Fontborder', description: "设置字符边框" },
    { name: 'FontName', description: "选中一段内容设置字体名称" },
    { name: 'FontSize', description: "选中一段内容设置字体大小" },
    { name: 'FormatBrush', description: "格式刷" },
    { name: 'hangingIndent', description: "段落悬挂缩进" },
    { name: 'FormViewMode', description: "表单模式" },
    { name: 'Header1', description: "设置标题的数字列表1样式" },
    { name: 'Header1WithMultiNumberlist', description: "设置标题的数字列表1样式" },
    { name: 'HideElementMarkAutoHide', description: "隐藏被标记为自动隐藏的元素" },
    { name: 'Indent', description: "段落缩进" },
    { name: 'InputFieldUnderLine', description: "设置或取消输入域的下划线" },
    // { name: 'InsertAccountingNumber', description: "插入会计数字" },
    { name: 'InsertBarcodeElement', description: "插入条形码" },
    { name: 'InsertButton', description: "插入按钮" },
    { name: 'InsertChartElement', description: "插入折现图" },
    { name: 'insertcheckboxorradio', description: "插入单、复选框组" },
    { name: 'InsertComment', description: "插入批注" },
    { name: 'InsertDateTimeField', description: "插入时间输入域" },
    { name: 'InsertDateTimeString', description: "插入时间字符串" },
    { name: 'InsertHorizontalLine', description: "插入水平线" },
    { name: 'InsertHtml', description: "插入html内容" },
    { name: 'InsertImage', description: "插入图片" },
    { name: 'InsertInputField', description: "插入输入域元素" },
    { name: 'InsertLabelElement', description: "插入标签文本元素" },
    { name: 'InsertLineBreak', description: "插入软回车" },
    { name: 'InsertMediaElement', description: "插入视频音频元素" },
    { name: 'InsertMedicalExpression', description: "插入医学表达式" },
    { name: 'InsertMode', description: "写模式" },
    { name: 'Insertorderedlist', description: "设置有序列表" },
    { name: 'InsertPageBreak', description: "插入分页符" },
    { name: 'InsertPageInfoElement', description: "插入页码元素" },
    { name: 'InsertParagrahFlag', description: "插入段落符号" },
    { name: 'InsertRuler', description: "插入标尺元素" },
    { name: 'InsertSection', description: "插入文档节" },
    { name: 'InsertString', description: "插入字符串数据" },
    { name: 'InsertTable', description: "插入表格" },
    { name: 'InsertTDBarcodeElement', description: "插入二维码" },
    { name: 'Insertunorderedlist', description: "插入无序列表" },
    { name: 'InsertWhiteSpaceForAlignRight', description: "插入空格使后面的内容右对齐" },
    { name: 'InsertXML', description: "插入xml小片段内容" },
    { name: 'Italic', description: "设置斜体" },
    { name: 'JumpPrintMode', description: "开启续打模式" },
    { name: 'alllineheight', description: "全局修改行间距" },
    { name: 'lineheight', description: "设置行间距" },
    { name: 'MoveDown', description: "向下移动一行" },
    { name: 'MoveEnd', description: "移动到行尾" },
    { name: 'MoveHome', description: "移动到行首" },
    { name: 'MoveLeft', description: "向左移动一列" },
    { name: 'MovePageDown', description: "向下翻页" },
    { name: 'MovePageUp', description: "向上翻页" },
    { name: 'MoveRight', description: "向右移动一列" },
    { name: 'MoveTo', description: "移动插入点位置" },
    { name: 'MoveToPage', description: "移动插入点到指定页" },
    { name: 'MoveToPosition', description: "移动插入点到指定的位置" },
    { name: 'MoveUp', description: "向上移动一行" },
    { name: 'NormalViewMode', description: "普通视图模式" },
    { name: 'NumberedList', description: "数字列表" },
    { name: 'OffsetJumpPrintMode', description: "偏移续打模式" },
    { name: 'PageViewMode', description: "页面视图方式" },
    { name: 'ParagraphFormat', description: "段落格式" },
    { name: 'Paste', description: "粘贴" },
    { name: 'PasteAsText', description: "纯文本粘贴" },
    { name: 'ReadViewMode', description: "阅读版式视图方式" },
    { name: 'Redo', description: "撤销" },
    { name: 'RefreshDocument', description: "刷新文档" },
    { name: 'RejectUserTrace', description: "撤销所有用户修订" },
    { name: 'RemoveFontFamily', description: "清除字体，把选择的内容字体还原成宋体" },
    { name: 'Removefontsize', description: "清除字体，把选择的内容字体还原成12号字体" },
    { name: 'ResetFormValue', description: "重置表单数据" },
    { name: 'rowspacing', description: "设置段前距" },
    { name: 'rowspacing', description: "设置段后距" },
    { name: 'SelectAll', description: "全选" },
    { name: 'SelectLine', description: "选中光标所在的当前行" },
    { name: 'ShiftMoveDown', description: "带选择的向下移动一行" },
    { name: 'ShiftMoveEnd', description: "带选择的移动到行尾" },
    { name: 'ShiftMoveHome', description: "带选择的移动到行首" },
    { name: 'ShiftMoveLeft', description: "带选择的向左移动一列" },
    { name: 'ShiftMovePageDown', description: "带选择的向下翻页" },
    { name: 'ShiftMovePageUp', description: "带选择的向上翻页" },
    { name: 'ShiftMoveRight', description: "带选择的向右移动一列" },
    { name: 'ShiftMoveUp', description: "带选择的向上移动一行" },
    { name: 'ShowBackgroundCellID', description: "显示背景单元格编号" },
    { name: 'ShowElementMarkAutoHide', description: "显示被标记为自动隐藏的元素" },
    { name: 'ShowFormButton', description: "显示表单按钮" },
    { name: 'Spechars', description: "插入特殊字符" },
    { name: 'Strikeout', description: "插入删除线" },
    { name: 'Strikethrough', description: "插入删除线" },
    { name: 'Subscript', description: "插入上标" },
    { name: 'Superscript', description: "插入下标" },
    { name: 'Table_DeleteColumn', description: "删除表格列" },
    { name: 'Table_DeleteRow', description: "删除表格行" },
    { name: 'Table_DeleteTable', description: "删除表格" },
    { name: 'Table_HeaderRow', description: "设置表格标题行" },
    { name: 'Table_IncreaseRowHeightToPageBottom', description: "修改表格行的高度，使其扩展到页面底端" },
    { name: 'Table_InsertColumnLeft', description: "在当前表格列的左边插入新的表格列" },
    { name: 'Table_InsertColumnRight', description: "在当前表格列的右边插入新的表格列" },
    { name: 'Table_InsertRowDown', description: "在当前表格行的下边插入新的表格行" },
    { name: 'Table_InsertRowUp', description: "在当前表格行的上边插入新的表格行" },
    { name: 'Table_MergeCell', description: "合并单元格" },
    // { name: 'Table_ReadDataFromTable', description: "获取光标所在的表格内容" },
    // { name: 'Table_RemoveEmptyRowsInLastPage', description: "删除最后一页中的空白行" },
    { name: 'Table_SplitCellExt', description: "拆分单元格" },
    { name: 'Table_SplitRowsByContentLines', description: "根据内容行数来拆分表格行" },
    { name: 'Table_WriteDataToTable', description: "表格数据绑定" },
    { name: 'TableCellProperties', description: "单元格属性对话框" },
    { name: 'TableProperties', description: "表格属性对话框" },
    { name: 'TableRowProperties', description: "表格行属性对话框" },
    { name: 'TolowerCase', description: "字母转小写" },
    { name: 'ToupperCase', description: "字母转大写" },
    { name: 'UnderLine', description: "设置下划线" },
    { name: 'Undo', description: "重做" },
    { name: 'TextSurroundings', description: "设置图片在文字中四周环绕" },
    { name: 'EmbedInText', description: "取消图片在文字中四周环绕" },
];
// 字号大小
let DATAFONTSIZE = [
    8,
    9,
    10,
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
];
var TEETHBUTTONLIST = [
    {
        id: 1,
        value: "*",
        text: "中切牙"
    }, {
        id: 2,
        value: "*",
        text: "侧切牙"
    }, {
        id: 3,
        value: "*",
        text: "尖牙"
    }, {
        id: 4,
        value: "*",
        text: "第一前磨牙"
    }, {
        id: 5,
        value: "*",
        text: "第二前磨牙"
    }, {
        id: 6,
        value: "*",
        text: "第一磨牙"
    }, {
        id: 7,
        value: "*",
        text: "第二磨牙"
    }, {
        id: 8,
        value: "*",
        text: "第三磨牙"
    }
];
var INSERTXMLSTR = `<XTextDocument xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" EditorVersionString="2024.8.20.0">
   <EnableValueValidate>true</EnableValueValidate>
   <XElements>
      <Element xsi:type="XTextHeader">
         <AcceptTab>true</AcceptTab>
         <EnableValueValidate>true</EnableValueValidate>
         <XElements>
            <Element xsi:type="XParagraphFlag" StyleIndex="0">
               <AutoCreate>true</AutoCreate>
            </Element>
            <Element xsi:type="XParagraphFlag" StyleIndex="0">
               <AutoCreate>true</AutoCreate>
            </Element>
         </XElements>
      </Element>
      <Element xsi:type="XTextBody">
         <AcceptTab>true</AcceptTab>
         <EnableValueValidate>true</EnableValueValidate>
         <XElements>
            <Element xsi:type="XString">
               <Text>这是一段内容</Text>
            </Element>
            <Element xsi:type="XParagraphFlag">
               <AutoCreate>true</AutoCreate>
            </Element>
         </XElements>
      </Element>
      <Element xsi:type="XTextFooter">
         <AcceptTab>true</AcceptTab>
         <EnableValueValidate>true</EnableValueValidate>
         <XElements>
            <Element xsi:type="XParagraphFlag">
               <AutoCreate>true</AutoCreate>
            </Element>
         </XElements>
      </Element>
   </XElements>
   <SerializeParameterValue>true</SerializeParameterValue>
   <FileFormat>XML</FileFormat>
   <ContentStyles>
      <Default xsi:type="DocumentContentStyle">
         <FontName>宋体</FontName>
         <FontSize>12</FontSize>
      </Default>
      <Styles>
         <Style Index="0">
            <FontName>宋体</FontName>
            <FontSize>12</FontSize>
            <Align>Center</Align>
         </Style>
      </Styles>
   </ContentStyles>
   <Info>
      <LicenseText>都昌信息科技有限公司:都昌5.0WEB旗舰版演示</LicenseText>
      <CreationTime>1980-01-01T00:00:00+08:00</CreationTime>
      <LastModifiedTime>2024-08-21T16:22:41+08:00</LastModifiedTime>
      <LastPrintTime>1980-01-01T00:00:00+08:00</LastPrintTime>
      <Operator>DCSoft.Writer Version:2024.8.20.0</Operator>
   </Info>
   <BodyText>这是一段内容</BodyText>
   <LocalConfig />
   <PageSettings />
</XTextDocument>`;
var INSERTHTMLSTR = `<p align="left" style="direction:ltr;font-family:宋体;font-size:12pt;font-style:normal;font-weight:normal;text-decoration:none;word-wrap:break-word;word-break:break-all"><span style="font-family:宋体;font-size:12pt;font-style:normal;font-weight:normal;text-decoration:none">主诉：</span><span id="field1" rbtc="#808080" bd2019="*" dc_innertext="[1111]" dc_innervalue="[1111]" class="InputFieldNormal" dctype="XTextInputFieldElement" eventlist="default" fixedcontentwidth="80px" hasbegin="true" hasend="true" dc_enablevaluevalidate="true" rehl="Enabled" rmfhk="Tab" dc_specifywidth="236.2205" dc_validatestyle="MaxLength:5;CheckMaxValue:True;MaxValue:0" style="font-family: 宋体; font-size: 12pt; font-style: normal; font-weight: normal; text-decoration: none; display: inline-block; min-width: 75px; text-align: left; text-indent: 0px;" contenteditable="true"><span dctype="start" dcignore="1" style="color: blue; float: none;">[</span><span id="field2" rbtc="#808080" bd2019="*" dc_innertext="1111" dc_innervalue="1111" class="InputFieldNormal" dctype="XTextInputFieldElement" eventlist="default" hasbegin="true" hasend="true" dc_backgroundtext="子元素" dc_enablevaluevalidate="true" dc_endbordertext="]" rehl="Enabled" rmfhk="Tab" dc_startbordertext="[" style="font-family: 宋体; font-size: 12pt; font-style: normal; font-weight: normal; text-decoration: none; display: inline;" contenteditable="true"><span dctype="start" dcignore="1" style="color: blue; float: none;">[</span><span style="font-family:宋体;font-size:12pt;font-style:normal;font-weight:normal;text-decoration:none">1111</span><span dctype="end" dcignore="1" style="color: blue; float: none;">]</span></span><span dctype="end" dcignore="1" style="color: blue; float: right;">]</span></span></p>`;
var DCEXECUTECOMMANDDEFAULTOPTIONS = {
    AllFontName: "楷体",
    AllFontSize: 18,
    // Font: "楷体",
    FontName: "楷体",
    FontSize: "20",
    insertxml: INSERTXMLSTR,
    InsertHtml: INSERTHTMLSTR,
    InsertMediaElement: {
        Width: '1871',
        Height: '1000',
        PrintVisibility: 'none',
        FileName: 'https://www.dcwriter.cn/static/images/websiteexplain.mp4',
    },
    Insertorderedlist: {
        liststyle: "ListNumberStyleArabic1"
    },
    DocumentDefaultFont: {
        "fontFamily": '幼圆',
        "fontSize": 24
    },
    InsertMedicalExpression: {
        "ID": "",
        "ExpressionStyle": "",
        "Type": "XTextNewMedicalExpressionElement",
        "FontSize": "12",
        "Width": "112px",
        "Height": "46px",
        "Values": ""
    }
};
//医学表达式对象
var MEDICALCHARACTERSIMGOBJECT = [
    {
        "expressionStyle": "FourValues1",
        "title": "月经史公式1",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAIASURBVHhe7dPRCsMwCIXhvf9LdxQSKJKTKLYXyv9BLraZgHr2u4AgQoMwQoMwQoMwQoMwQoMwQoMwQoMwQiP8foxGYTICodGYjEBoNCYjEBqNyQiERmMyAqHRWk/Gs3hVc7qbebu6ll3dy5pnZ1ez+353b/LUVNWzqyGzWM/dnd3b1fXsatgtbf6mak4Lz7xdXc+uBk8gPDUrmber69nVkFnsaeGZt6vr2dWwWpr9zrP8lczb1fXsalCLVefJfrZWvz/fsqeTXt0YnmWpmtPdzNvV9exqIDTfaNnVvSx7rFON/TzZO6s6T01lvbp5UbdFv4nJCIRGYzICodGYjEBoNCYjEBqNyQiERmMyAqHRSk/mXmzVUxl/J6H6Yr/EZARCozEZgdBoTEYgNBqTEQiN1noynsWrmtPdzNvVtezqXtY8O7ua3fe7e5OnpqqeXQ2ZxXru7uzerq5nV8NuafM3VXNaeObt6np2NXgC4alZybxdXc+uhsxiTwvPvF1dz66G1dLsd57lr2Terq5nV4NarDpP9rO1+v35lj2d9OrG8CxL1ZzuZt6urmdXA6H5Rsuu7mXZY51q7OfJ3lnVeWoq69XNi7ot+k1MRiA0GpMRCI3GZARCozEZgdBoTEYgNBqTEQiNxmQEQqMxGYQRGoQRGoQRGoQRGoQRGoQRGgRd1x/Fh1E1QauElgAAAABJRU5ErkJggg=="
    },
    {
        "expressionStyle": "FourValues2",
        "title": "月经史公式2",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAALgSURBVHhe7ZPRbsQgDATv/3/6qlVBohSIAYd47YzEwyUO2Ozc52uUz8dsa8ewegemk4ksjuXZzacSURzrM1MkEkkchllp0oggDsuMVEl4FodpNroUPIrDNhNlAnddsmRf7bMZ/wSU0gDNy8ZeeY2Q1MyguddJaKUB2pd+UhpWYQC1NEDz8kd75Xca5zELA7i7T2iF0NunfL57FrswgH+ChEYYrT3qZzvneBAG+JgisRtKT5remmG23jKupAE74Ui+XdnfkzDAnTRgNaQ7pPEmDHApDZgJC7X1qpHU1EhqGHErDXgyNK/CANfSgCfC8ywMcC8NOBmid2FACGnAiTAjCAPCSAPuDDWKMCCUNOCOcCMJA8JJAzRDjiYMCCkN0Ag7ojAgrDRgJ/SowoDQ0oCV8CMLA8JLA2YkiC4MeG8gcSUD3r/C/PLeQkFPileWv7y3UVEL8grzHzc3IglXKkCumxVGswfL0E+AEPIaIakpmanPtVf1M3tahn+ChGZgZd1MyJo9WIZ/gsQojPxOElirRhq0Vg/W4Z8g0QujfH4V2Oi9JGyNHhjgnyDRCqN+NgpMEuZVzW4PLPBPkOgF1lsl9e8Ro9rWu3xea7HC23mFJIRWzUp4vW9We2CDf4LESmA7Aba+XemBEfoJEEK9alo1WLvkPep98/MSSQ0LvJ1voBkYc/irhJv4jpCjiRNq2jvDjSROmElPhBpFnBBTngwzgjjuJ3wiRO/iuJ7uyfA8i+N2spXQJN/M7OtVHJdTzYaF+rxGSGpqZusZcDfRTkh3SAO8ieNqmt1wRt/nd6tneBLHzSQaofT2KJ/vnONFHBdTaIXR2qd+tnuWB3HoJ9AMoSdNb62y860FqLvXvnzJflpnMotD2/kdl35SGsAqDmXX2peN/epVI6lZQWufk9B1zHjJV7DNRNWtR2EyTLPRdOpZmAzLjBRdRhAmwzCr+Q4jCZOxPrPp7iIKk7E7+/f7Ay7sD3dbyMQ1AAAAAElFTkSuQmCC"
    },
    {
        "expressionStyle": "ThreeValues",
        "title": "月经史公式3",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAFwSURBVHhe7dTRisIwFEVR//+nnSlMoA2N3jPGh7RrQRDT+yDJto8nhERDTDTERENMNMREQ0w0xERDTDTERENMNMREQ0w0xERDTDTERDPJ4/H+KCszKxDNh7YQ2nqlMrMK0UwiGmKvgmjPRMPBKIj9vmg4EA2xsyD6PdFwMIpmtFYmmkkqIaweSyOaSURD+YK3uX71KjMruVw0lQt5N7P6pX7bZU6n/YMrQVRmGLvc6Yjm+24VTXtWmWHsNtHs90XzGdHsCKbmFtH0e/+NZnv+jbWa9X7xG2eX0F/Sfu313zl3i2h6ZzOCqRPNH9HUXeaktkvvV2800z6pcVq/RJNxWsREM0nlbXWVN5poPrSF0NYrlZlViGYS0RB7FUR7JhoORkHs90XDgWiInQXR74mGg1E0o7Uy0UxSCWH1WBrRTCIayrYQ+tWrzKxENMREQ0w0xERDTDTERENMNMREQ0w0xERDTDTERENMNMREQ+j5/AGiGIgMG4ID/gAAAABJRU5ErkJggg=="
    },
    {
        "expressionStyle": "FourValues",
        "title": "月经史公式4",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAGnSURBVHhe7dbRasMwEETR/P9PuzZYsFosd+bBida6B/SiqFRdXUg/G2AiGtiIBjaigY1oYCMa2IgGNqKBjWhgIxrYiAY2ooGNaGAjGtiIBjaiCT6f/8ehnHk7JrA7QmjrjnJmBUwgIBoNEwjugmifEQ3RdEZBxH2iIZoO0WiYQHAVRN4jGqLpjKIZrVURTaCEsHIsDRMIiEbDBHZHCHllyplVlPzLlQeb5VEr3VVV6rbHcNu6o5x5WqW7umrd9lTpISrdVfW6aNpnszxEpbuqat32NBpy3J/lISrdVVXrtqdKD1Hprqpatz1dDTnvjc48tUauPst7dz8/o1q3PY0eYrR+6er35/vFVcFroslmeYBKd1URzcOI5seO4eaVKWe+QbmHcmZGpaLBHIgGNqIJlK+HKl8hT2ICO/V/CuXMCphAQDQaJhDcBdE+Ixqi6YyCiPtEQzQdotEwgeAqiLxHNETTGUUzWqsimkAJYeVYGiYQEI2GCeyOEPLKlDOrIBrYiAY2ooGNaGAjGtiIBjaigY1oYCMa2IgGNqKBjWhgIxrYiAambfsDKpYobN1+T10AAAAASUVORK5CYII="
    },
    {
        "expressionStyle": "Pupil",
        "title": "瞳孔",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAIFSURBVHhe7dnRisMwDETR/v9Pdwk4YESc1QRUa8I94JfEdQdr3vL5AiJKAxmlgYzSQEZpILMszefzf+zMnl9wyppllfa43HPdyeyp5pRV5ZV2cBqEU9as15XmfNdlEE5Zs7zSDqtLnp93GYRT1iyvtMPVJcdnXQbhlDXLK+2wGsRq7XT1/zHfvBy8pjRRlwE4Zc2iNMUozWbH5cYVZfb8QiZHZk9HVqVBD5QGMkoDGaWBjNJARmkgozSQURrIKA1klAYySgOZZWky32iefMepOLcq605WaY/LPdedzJ5ZxbkVZ3bhlXaoGkTFuRVn7uaVdri75PPdk0FUnFtx5m5eaYfVJc/Pnwyi4tyKM3fzSjtcXXJ89mQQFedWnLmbV9phNYjVyrraG8+aV8bVvnjOvBx4pAwyl/tkABXnVmXdySvtUDWIinOrsu5klfa43LiizJ4o85vMnllmf2ZPRx4p0QqlgYzSQEZpIKM0kFEayCgNZJQGMkoDGaWBjNJARmkgsyxN5sNel49/TlmzrNIel3uuO5k91ZyyqrzSDk6DcMqa9brSnO+6DMIpa5ZX2mF1yfPzLoNwyprllXa4uuT4rMsgnLJmeaUdVoNYrZ2u/j/mm5eD15Qm6jIAp6xZlKYYpdnsuNy4osyeX8jkyOzpyKo06IHSQEZpIKM0kFEayCgNRN/vH2/1IWUBjEsoAAAAAElFTkSuQmCC"
    },
    {
        "expressionStyle": "FetalHeart",
        "title": "胎心值",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAKCSURBVHhe7dLBigQxCEXR+f+f7qksCkS8FQNmobwD2aTSSevz7yehvz+1hqgzQEPD1BmgoWHqDNDQMHUGaGjYyM5kAt+dORmaivc6mVPJYwXzri/ZMzvvPZm7Mvd1MacSoyLEk5Ar3utkTiXGV0Dvt0zQWRXvdTKnEoMCsvu7EE9CrnivkzmVGFFAfm8X4knIFe91MqcSg0KkFaH9SHTW3u9Xd/0rCGSC2Z05CbfivU7mVGJoaO6aU8ljBeOXlzmz0L7l74l+kznTTf8KLpkQ7i3qDNDQMHUGaGiYOgM0NEydARoaps4ADQ1TZ4CGhqkzQEPD1BmgoWHqDNDQMHUGaGiYOgM0NGxkZzKB786cDE3Fe53MqeSxgnnXl+yZnfeezF2Z+7qYU4lREeJJyBXvdTKnEuMroPdbJuisivc6mVOJQQHZ/V2IJyFXvNdJm0pW0+36En33e3Tma5Hom9+jM3510ONfHoqa78OxK0L7keisvd+v7vpXEMgEsztzEm7Fe53MqcTQ0Nw1p5LHCsYvL3NmoX3L3xP9JnOmm/4VXDIh3FvUGaChYeoM0NAwdQZoaJg6AzQ0TJ0BGhqmzgANDVNngIaGqTNAQ8PUGaChYeoM0NCwkZ3JBL47czI0Fe91MqeSxwrmXV+yZ3beezJ3Ze7rYk4lRkWIJyFXvNfJnEqMr4Deb5mgsyre62ROJQYFZPd3IZ6EXPFeJ3MqMaKA/N4uxJOQK97rZE4lBoVIK0L7keisvd+v7vpXEMgEsztzEm7Fe53MqcTQ0Nw1p5LHCsYvL3NmoX3L3xP9JnOmm/4VXDIh3FvUGaChYeoM0NAwdQZoaJg6AzQ0TJ0BGhry+/0D8/+sy1gpJfoAAAAASUVORK5CYII="
    },
    {
        "expressionStyle": "ThreeValues2",
        "title": "眼球突出度",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAALDSURBVHhe7ZSBbgMhDEP7/z/dDYloiEGh7cWxuTwJlcLpEjtuH08HHg+X1yYf4DELt+lmcOLxmoHrZDM4cXh67z7VDA4eb88hE83g4EB4DZsmY3BOCzNKD9Q1liGVPmydAlIL3DWmQZ0SGrSOENdYhtX3Ub7P1s59BBG1w9RGGm2MemjPRvvVPZKImoWYqpUo0cao/iwItl/do0DXa4mrXAkVP6g9C4LtV/cIkLVGxFavoE0o9frVn7ff2/3orN97gqixIr6DCoMZ7LB4RDWpDM4cJm/oppTB+Q+bJ5QTyuD8wegF7XTuHpyin9UD6sncNTjsuumnwvyL80BBq8w07hAcFY1Skzg5OEra5KZwYnDUNElO4KTgKGqRdf+E4KhqkHZeOTjSvddPWST/3sX/JWHdF6PadSVKQ2h7bf2Q0lA/3enNuhoF01ceKGgoQLpEGcRset8byhMPIF2+Mqh8ztbOfc/sPJKdntpnTN9o7dx7A6kyEtOerfY7z7bMziPY6WX0zEyn7Vf3nvhX+GUkZEe07Xee7Sl3DOsVr55pz0f71b0n/hUqr4StDFjd98zOI3inx5m20X7nWS/8K1SKmHYZ/dloPzrr90b/nYFRT9Z7v/q79nu7H531ey983w7G26xvYO7tXY5RojCUU4JzhAqlYZwQHHkFikNQD45098rmS/deP+U44m9eVINk1ycExlDUItVxMfikwBhqmmS6PTEsLUr6JDo9PTCGik76Lu8SGENBL3WHdwuMwa6btru7BsYo+lk9oOzq7oFpYfSCrqMMzH/YPKHqJgMzh8kbmk4yMGtYPKLoIgOzD4NX4R1kYN4n2rPQ6hmYz4n0LqxyBuZ7ojwMqZqBuY4IL+EVMzDXg/YUWi0D4wfSW1ilDIw/KI8hVTIwOBBeu1fIwODx9tz17RmYODy9d3tzBiYenxk8nz+vA1wc+iGiVwAAAABJRU5ErkJggg=="
    },
    {
        "expressionStyle": "StrabismusSymbol",
        "title": "斜视符号",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAFXSURBVHhe7dULasNADEVR73/TadXWJDgfkDOe1NI5oA28uTDLBZJEQ5poSBMNaaIhTTSkiYY00ZAmGtJEQ5poSBMNaaIhTTSkiWaiZVl+7uxEM0mFWFaimaBSMEE0B6sWTBDNgSoGE0RzgIilajBBNINVjmUlmoE6BBNEM0iXYIJoBugUTBDNGyKWbsEE0ezUMZaVaHboHEwQTVL3YIIFEgTzywpPRCC3kQjmyhIPrMHcHlfWeEAwr1lkYxuMaO5Z5AHRvGaRjW0worlnkT/bQATznFW+iSOn/VqCyWu9mGD2abuaYPZrt1zEIpj3tFpPLGO0WVEw47RYUjBjlV9TMOOVXTRiEcwxSq4qlmOVW1cwxyu1sGDmKLOyYOYpsbRg5rI2aaIh7RTRxPezHp93mlcQzP8hGtJEQ5poSBMNaaIhTTSkiYa0U0cjpM84xeoRx7NjtsvlC061fSXnSpS3AAAAAElFTkSuQmCC"
    },
    {
        "expressionStyle": "PainIndex",
        "title": "标尺",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAALbSURBVHhe7ZWBasNADEP7/z/dIcrBxecmkXJx0kwPCrMSyXZr2OttDImPxtD4aAyNj8bQ+GgMjY/G0PhoDI2PxtD4aAyNj8bQ+GgMjY/G0PhoDI2PxtBQR/N6XX9jnuHDlTMMndeGUZ5VeYBn+HD2DIOiBAOmaWOmB3iGD2fPMChKMGCaNmZ6gGf4cPYMg6IEA6ZpY6YHeIYPZ88wKP1L0bBW4+/s+do73+qo9azV0QuitreOWs9aHb0ganvrqPWs1dELora37rXGoLSXomlmnWnVdaZV15lWXWdaVvcMyjfTzDrTqutMq64zrbrOtKzuGZRvppl1plXXmVZdZ1p1nWlZ3TMo30wz60yrrjOtus606jrTsrpnUPqX/fEHn0h6NAqK72ke8B++h0FRgkH07cmZtcQWVR7wtJ0yz6AowSD69uTMWmKLKg942k6ZZ1CUYBB9e3JmLbFFlQc8bafMMyhKMIg+1FtZSq87e8DTdso8g6IEg+hDvZWl9LqzBzxtp8wzKEowiD7UW1lKrzt7wNN2yjyDogSD6EO9laX0urMHPG2nzDMoSjCIPtRbWUqvO3vA03bKPHzKTtBMGXKLMzJ/kSu/h587GnM9p/yq7WB8OM/Ev6ih8dEYmp86mqp/deq/VdXD+qpm+8YiSVkAqB7WVzWf2qPKV+UBzdd7FylHglmUXlXzVfToUear8IDm672LlCPBLNW9GKrmaijzVXhA8/XeRcqRYIWKXlX7wKP4QNVOR3y9d5FyJFihopfaQ+mj9AJKLxZ1vubrvYuUI8Esigewvso+Si/VU92r9y5S4sO9VHkA66vaqaoPuKJX712kxId7qfRU9FL6VM0GrujVexcp8eFeVA/rq5rvzrMBdT6F1qv3a0kdMXAvqk+hqs+dmfkb+ds0ND4aQ+OjMTQ+GkPjozE0PhpD46MxND4aQ+OjMTQ+GkPjozE0PhpD46MxND4aQ/J+/wEFIxAGtPJElgAAAABJRU5ErkJggg=="
    },
    {
        "expressionStyle": "PermanentTeethBitmap",
        "title": "恒牙牙位图",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAANpSURBVHhe7ZfBjuRACEPn/3+6d9gdaz0EV4UUp8hPigKGoiI3l/76GNPES2PaeGlMGy+NaeOlMW28NKaNl8a08dKYNl4awdeXrVHYGYGXRmNnBF4ajZ0ReGk0dkbgpdHYGYGXRmNnBF4azbgz2ezIqx9gpeFMN5+kmqfuydrdvqA6C6o53TxgLcd4wK4e/M4OyRfsYtaCuzq/uZfjU/IsdU/EVe8TjfOs4610vLkOVD33qvql7+c9RnVpsPuYiJWu4DPBqrdLnrW6R9VWfUD1qDlVrOpB5KqeewNoVQ3oykOefGAQMfLdOcBnglVvlzxrdY+qrfqA6lFzdjNyPXJVz70BtKoGdOUh6rI7H8jvHCMHnHPvFHfuA6q26gPdnk4d8arOecBnqnpwVQ4pL9loiHNf5FVfkGPOJ8jzkFd3ca7iIOfBruckR1xpTPfMdcIhu49iQl9+3KKOWNVPqWZVdwbqG1Z9YNfTnYG40oLT88F1wiGXC1LORK16QJUDxKp+ympWrqlvWPWBu7PATkMc7/xAz7BW9eUz1wmHXC5IeVB9WJD1eHNPFav6KatZuaa+YdUH7s4CVf3JmWB1Xp0JfmcDqMuq+PIxSY8391Sxqp+SZ1X3gar3Tl9QneV3sItVHTw5r84Ev7NDYjienEMLch7kPq6zDnb1U6r7qnsqvdsHjfP85DrnINcD1vgBOeY6x8xVMX+pzDL/sDMCL43Gzgi8NBo7I/DSaOyMwEujsTMCL43m25v/f7P8+Ln1/CyPSYQ5psbOCLw0Gjsj8NJo7IzAS6OxMwIvjcbOCLw0mnFnstmR42E479Z3+QTT897EqDPVj4ecde7LOt6qnnO8WZ9get6bGHdGmZ115KxXcdZ2+RSTs97GuDPK7KwjZ72Ks7bLp5ic9TbGnanM3mmI72og8lX9hMlZb2PcmcrsnRZx7kHOb+7J9YDjUyZnvY1xZyqzVxrXdvFdbYLJWW9j3JnK7JXGNRWDJ2eeMjnrbYw7U5m90rimYvDkzFMmZ72NcWcqs1ca16pYvQPWWJ9get6bGHUGP142vMq5j+OA6/xUNdYmmZ73JuyMwEujsTMCL43Gzgi8NBo7I/DSaOyMwEujsTMCL43Gzgi8NBo7Y9p4aUwbL41p46Uxbbw0po2XxrTx0pgmn88fktMBP6wlx0MAAAAASUVORK5CYII="
    },
    {
        "expressionStyle": "DeciduousTeech",
        "title": "乳牙牙位图",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAPASURBVHhe7ZfRjiVHCEPn/396MiQXLUvbFFSTRFr5SK3b2JSnGvEyX99CDNHSiDFaGjFGSyPGaGnEGC2NGKOlEWO0NGKMlobw9aXRMDQZgpaGo8kQtDQcTYagpeFoMgQtDUeTIWhpOJoMQUvD+dcmw4Zuuj+ZyjOiHnv92WQzD93PtfhEprrBNNZ/y15SgF0ya7HO76fzyN9kK5/dOed3vFNP1gx09i17SYnqA5xYV55hddW/zUZ+NyP3sbrb55zqW3ZSAOiCk49AvVGrzm6wkd/NyH2s7vY5p/qWnRRA54Ksh31s1O09PttsZHrG6Y7Ii2ed3Pe2vmUnBXC6IPNNZx8b9fyez7xlIy/fi2Ui3c+y89kzpvUtOymA6oKdy3tP7H2bOWEjL2egzJNm76yOujGtb9lJAbALdi/uffabHwTTb9nIyxkos6Oxu5z6ujlTdlIA3Q91Oh8YtU7/GzbyOnfsaKzn1NfJuWEnBYAuWH1E5wNZv72j/jds5bE7Ox3N6zdZyLtlLylgF/QnEvWTn0Ee0rbYyjzdsdLROaQZp/6sv2Ev6Q9jc8h/GpoMQUvD0WQIWhqOJkPQ0nA0GYKWhqPJELQ0nJ/Z/PqXTI+e1vNZHpGw4QiMJkPQ0nA0GYKWhqPJELQ0HE2GoKXhaDIELQ2HTqYa2o1nuj8IpFdnWI7R6ffc+ERyHTl5zEe698cnw7SN/qluPJUfWLNx42UN1VVP9lF/JHuovzpvMB9lOVHPPewc6ougc1V96mfvBvJyj/FUPqBmZ+pl7aank2GYjrzueafyO/mI27+Z9a16qju/V4HcGJl6p0sYVabRyTBM7+Sz807ld/IRt38z61v1VHd+rwK5MXLrOahnmllpnX6r45NBmlP1szyjyjQm51yrPOdUOyzzUX9+H7Bg49YzmF/pyKu0Tn+s7b3yM8jLGayngvksy59M1mLdOVOdN56nP6Bg57/2jOxXNcp6mx/p5Hd6IhMv1vZe+U7sO/Uf68/vg9wYufGqM8bUR3V+IrnOTPqR1zk/zXRO2ac6E33Ue8z//D6o/vCNV50xHhcb1hHk/d/5xjTTOWWf6oh50Ue9pzyajsKcqXe6hHHq6WQ403x7r/wM83Jm5ibTyN6bmr0byMs9xlP5wRr9ydx4UT/5kaluIA9pBtMNpBmnMzeeUeno3K0eib3ZR5rzVMTfoGGJf9BkCFoajiZD0NJwNBmCloajyRC0NBxNhqCl4WgyBC0NR5MRY7Q0YoyWRozR0ogxWhoxRksjxmhpxJDv778ArCVZ2BW4HnUAAAAASUVORK5CYII="
    },
    {
        "expressionStyle": "Fraction",
        "title": "分数公式",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAIAAACoSmOYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAETSURBVHhe7dpBasMwFEBBu/e/sytqFUpKkm1Gem8TKTszfNkGn9d1HfXxfc3f+uxyMsrJKCejnIxyMsrJKCejnIxyMsrJKCejnIxyMsrJKCejnIxyMsrJKCejnIxyMsrJaH2n8zznSo53WoPhbbDTENoEaQQ7XT/NzZMG5BofZu/1HHGP4N38C2kjp2Fzj+CdRbWy0w0zN3h7nXtuyzr9HyZ6tjadJ+5I3NFJvG/Bd9qHB7a/F/JCQkQawU4veoaBIo02OvcekMZ2roQWnKcXwzRXv0HXvqDTkvWea5STUU5GORnlZJSTUU5GORnlZJSTUU5GORnlZJSTUU5GORnlZJSTUU5GORnlZJSTUU5GOQkdxzeUVEt50ZQxrQAAAABJRU5ErkJggg=="
    },
    {
        "expressionStyle": "DiseasedTeethTop",
        "title": "病变上牙",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAGkSURBVHhe7dKBasMwDEXR/P9PZxHdmJv5ORZOM9C7BwJdaBmS7rYDSUSDNKJBGtEgjWiQRjRIIxqkEQ3Sykezbc+P+B//80lE8wFEU8CTR6weTCCaGzkEEzymPDxxUKIpKHPUbAAuwQSiOYnv/DyznIIJXtMeZg9MNJpdNGHmyJ+IqwqiEYhGs4wmXB37zrCqIRrhjqiq8p38MDo80WjW0QR1/JWgqiMaokmzjya0EcTn89M6/+2IDRxmQyCYF7bwbSYIonlhC41RFATzi000VBgE845tnPQCIZp3bKOjjYRg/mIjHUQzxkaEiIVg+tiKQDAamxGIRmMzAtFobEYgGo3NCESjsRmBaDTLzUQQ7dMzen/12+rspu4d+u531dlNTDTr7CYmmnV+E3esxEA0htTRZ2JwDCZYRzM6+lUQrsEE28lXonAOJlhOP3N09R33YILdBnpHv/tddXYTx5F7z5l613vc+E08yTGGWWxGIBqNzQhEo7EZgWg0NiMQjcZmBKLR2IxANBqbEYhGYzNIIxqkEQ3SiAZpRIM0okEa0SBp378AjMHkr16/6I8AAAAASUVORK5CYII="
    },
    {
        "expressionStyle": "DiseasedTeethBotton",
        "title": "病变下牙",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAEXSURBVHhe7dZBaoRQFEVB979pw0eCCRj4p53lVcEbObse7D5OiERDJhoy0ZCJhkw0ZKIhEw2ZaMhEQyYaMtGQiYZMNGSiIRNNcBzHr5tKNJueIpkajmg2ieYmmk2iuYnmBdGQTA1mEc0HJgeziCaaHsxigUAwFytsegpmakSi2bQCebqJRkcz9aW/NXK16V+Kt3xpyERDJhoy0ZCJ5g/r2af334mGTDRkoiEbudr3f4+fxz5rkYmGTDSBn7SLaDY9RTI1HNFsEs1NNJtEcxPNC6IhmRrMIpoPTA5mEU00PZjFAoFgLlbY9BTM1IhEs2kF8nQTiYZMNGSiIRMNmWjIREMmGjLRkImGTDRkoiETDZloyERDdJ5fgaCSEDt60S4AAAAASUVORK5CYII="
    },
    {
        "expressionStyle": "LightPositioning",
        "title": "光定位",
        "base64ImgSrc": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAI0AAABACAYAAAAnKPTPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAJISURBVHhe7dnBqiMxDETR9/8/naHBDUbYPZKhkAvuAW8SpShs7fL3A4pYGpSxNChjaVDG0qDMcmn+/v5fOzMTKXJVXTtZtX0u9z1fMjMzRa4i8xZebQfVQyhyFZndvNoOX5f8fnfyEIpcRWY3r7bD7pLnz08eQpGryOzm1XZYXXL87OQhFLmKzG5ebYfdQ+xO1mo2Zs0nYzUXc+bjwKNlkLnckwdQ5Kq6dvJqO6geQpGr6trJqu1zufFEmZko85vMzCwzn5m5kUdLXIWlQRlLgzKWBmUsDcpYGpSxNChjaVDG0qCMpUEZS4Myy6XJ/Edz8j+OIlfVtZNV2+dy3/MlMzNT5Coyb+HVdlA9hCJXkdnNq+3wdcnvdycPochVZHbzajvsLnn+/OQhFLmKzG5ebYfVJcfPTh5CkavI7ObVdtg9xO5krWZj1nwyVnMxZz4OPFoGmcs9eQBFrqprJ6+2g+ohFLmqrp2s2j6XG0+UmYkyv8nMzDLzmZkbebTEVVgalLE0KGNpUMbSoIylQRlLgzKWBmUsDcpYGpSxNChjaVBmuTSZP/ZO/vxT5Kq6drJq+1zue75kZmaKXEXmLbzaDqqHUOQqMrt5tR2+Lvn97uQhFLmKzG5ebYfdJc+fnzyEIleR2c2r7bC65PjZyUMochWZ3bzaDruH2J2s1WzMmk/Gai7mzMeBR8sgc7knD6DIVXXt5NV2UD2EIlfVtZNV2+dy44kyM1HmN5mZWWY+M3Mjj5a4CkuDMpYGZSwNylgalLE0KPr9/gGdDyxMCcFWuQAAAABJRU5ErkJggg=="
    }
];

//图片编辑的svg图标
var IMGEDITSVGOBJECT = {
    TIANCHONG: `<svg class="icon" width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M1001.386667 797.013333c-7.04-69.546667-130.56-260.906667-130.56-260.906666s-119.893333 185.173333-130.346667 260.906666c-17.92 129.28 64 144.426667 130.346667 144.426667s143.146667-14.506667 130.56-144.426667zM842.88 498.346667a64 64 0 0 0-18.346667-53.76L501.76 121.813333a64 64 0 0 0-92.16 0L317.226667 213.333333 178.773333 75.52a32.426667 32.426667 0 0 0-46.08 0 32.853333 32.853333 0 0 0 0 46.08l138.453334 138.453333L40.533333 490.666667a64 64 0 0 0 0 92.16L362.666667 905.6a65.066667 65.066667 0 0 0 92.373333 0l369.493333-368.853333a64 64 0 0 0 18.346667-38.4zM132.693333 490.666667l184.533334-184.533334 115.413333 115.413334a32.64 32.64 0 0 0 46.08-46.293334L362.666667 260.053333l92.373333-92.16L778.453333 490.666667z"/></svg>`,
    BIANKUANGSE: `<svg class="icon" width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M128 853.333h768q4.224 0 8.32 0.854 4.139 0.768 8.021 2.389 3.84 1.621 7.382 3.968 3.456 2.304 6.442 5.29 2.987 2.987 5.291 6.443 2.347 3.499 3.968 7.382 1.621 3.882 2.432 8.021 0.81 4.139 0.81 8.32t-0.853 8.32q-0.768 4.139-2.389 8.021-1.621 3.84-3.968 7.382-2.304 3.456-5.29 6.442-2.987 2.987-6.443 5.291-3.499 2.347-7.382 3.968-3.882 1.621-8.021 2.432-4.053 0.81-8.32 0.81H128q-4.181 0-8.32-0.853-4.139-0.768-8.021-2.389-3.84-1.621-7.382-3.968-3.456-2.304-6.442-5.29-2.987-2.987-5.291-6.443-2.347-3.499-3.968-7.382-1.621-3.882-2.432-8.021-0.81-4.139-0.81-8.32t0.853-8.32q0.768-4.139 2.389-8.021 1.621-3.84 3.968-7.382 2.304-3.456 5.29-6.442 2.987-2.987 6.443-5.291 3.499-2.347 7.382-3.968 3.882-1.621 8.021-2.432 4.139-0.81 8.32-0.81z m0-768h298.667q4.181 0 8.32 0.854 4.138 0.768 8.021 2.389 3.84 1.621 7.381 3.968 3.456 2.304 6.443 5.29 2.987 2.987 5.29 6.443 2.347 3.499 3.969 7.382 1.621 3.882 2.432 8.021 0.81 4.139 0.81 8.32t-0.853 8.32q-0.768 4.139-2.39 8.021-1.62 3.84-3.967 7.382-2.304 3.456-5.291 6.442-2.987 2.987-6.443 5.291-3.498 2.347-7.381 3.968t-8.021 2.432q-4.139 0.81-8.32 0.81H128q-4.181 0-8.32-0.853-4.139-0.768-8.021-2.389-3.84-1.621-7.382-3.968-3.456-2.304-6.442-5.29-2.987-2.987-5.291-6.443-2.347-3.499-3.968-7.382-1.621-3.882-2.432-8.021-0.81-4.139-0.81-8.32t0.853-8.32q0.768-4.139 2.389-8.021 1.621-3.84 3.968-7.382 2.304-3.456 5.29-6.442 2.987-2.987 6.443-5.291 3.499-2.347 7.382-3.968 3.882-1.621 8.021-2.432 4.139-0.81 8.32-0.81zM512 466.347a171.861 171.861 0 0 0 48.341 121.813 170.368 170.368 0 0 0 279.126-51.755 171.904 171.904 0 0 0 13.866-65.365v-4.693c0-112.64-173.013-295.68-173.013-295.68S512 353.707 512 466.347z"/><path fill="#707070" d="M85.333 128q0-4.181 0.854-8.32 0.768-4.139 2.389-8.021 1.621-3.84 3.968-7.382 2.304-3.456 5.29-6.442 2.987-2.987 6.443-5.291 3.499-2.347 7.382-3.968 3.882-1.621 8.021-2.432 4.139-0.81 8.32-0.81t8.32 0.853q4.139 0.768 8.021 2.389 3.84 1.621 7.382 3.968 3.456 2.304 6.442 5.29 2.987 2.987 5.291 6.443 2.347 3.499 3.968 7.382 1.621 3.882 2.432 8.021 0.81 4.139 0.81 8.32v768q0 4.224-0.853 8.32-0.768 4.139-2.389 8.021-1.621 3.84-3.968 7.382-2.304 3.456-5.29 6.442-2.987 2.987-6.443 5.291-3.499 2.347-7.382 3.968-3.882 1.621-8.021 2.432-4.139 0.81-8.32 0.81t-8.32-0.853q-4.139-0.768-8.021-2.389-3.84-1.621-7.382-3.968-3.456-2.304-6.442-5.29-2.987-2.987-5.291-6.443-2.347-3.499-3.968-7.382-1.621-3.882-2.432-8.021-0.81-4.053-0.81-8.32V128z m768 597.333q0-4.181 0.854-8.32 0.768-4.138 2.389-8.021 1.621-3.84 3.968-7.381 2.304-3.456 5.29-6.443 2.987-2.987 6.443-5.29 3.499-2.347 7.382-3.969 3.882-1.621 8.021-2.432 4.139-0.81 8.32-0.81t8.32 0.853q4.139 0.768 8.021 2.39 3.84 1.62 7.382 3.967 3.456 2.304 6.442 5.291 2.987 2.987 5.291 6.443 2.347 3.498 3.968 7.381t2.432 8.021q0.81 4.139 0.81 8.32V896q0 4.181-0.853 8.32-0.768 4.139-2.389 8.021-1.621 3.84-3.968 7.382-2.304 3.456-5.29 6.442-2.987 2.987-6.443 5.291-3.499 2.347-7.382 3.968-3.882 1.621-8.021 2.432-4.139 0.81-8.32 0.81t-8.32-0.853q-4.139-0.768-8.021-2.389-3.84-1.621-7.382-3.968-3.456-2.304-6.442-5.29-2.987-2.987-5.291-6.443-2.347-3.499-3.968-7.382-1.621-3.882-2.432-8.021-0.81-4.139-0.81-8.32V725.333z"/></svg>`,
    ZITISE: `<svg class="icon"width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M490.666667 106.666667h-27.968l-11.157334 25.621333-139.370666 320L286.186667 512h1.28l-81.557334 197.013333 78.848 32.64L379.818667 512h264.32l95.104 229.653333 78.848-32.64L736.512 512h1.28l-26.005333-59.712-139.349334-320L561.28 106.666667H490.666667z m116.906666 320h-191.146666L512 207.210667 607.573333 426.666667zM192 917.333333h640v-85.333333H192v85.333333z"/></svg>`,
    SHANCHU: `<svg class="icon"width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M725.333333 192V106.666667H298.666667v85.333333H106.666667v85.333333h74.666666v576a64 64 0 0 0 64 64h533.333334a64 64 0 0 0 64-64V277.333333H917.333333V192h-192zM266.666667 832V277.333333h490.666666v554.666667h-490.666666zM384 384v320h85.333333V384h-85.333333z m170.666667 0v320h85.333333V384h-85.333333z"/></svg>`,
    HUABI: `<svg class="icon"width="20px" height="20px" viewBox="0 0 1024 1024" version="1.1"><path d="M258.56 916.48c-30.72 0-64-5.12-92.16-15.36-64-23.04-97.28-69.12-99.84-128-2.56-89.6 66.56-120.32 120.32-143.36 51.2-23.04 79.36-35.84 79.36-74.24 0-46.08-79.36-84.48-112.64-89.6-12.8-5.12-20.48-17.92-20.48-30.72 2.56-12.8 15.36-23.04 28.16-20.48 46.08 7.68 156.16 56.32 156.16 140.8 0 74.24-61.44 99.84-110.08 120.32-56.32 25.6-92.16 43.52-89.6 97.28 0 38.4 23.04 66.56 64 81.92 66.56 25.6 166.4 7.68 192-23.04 10.24-10.24 25.6-12.8 35.84-2.56 10.24 10.24 12.8 25.6 2.56 35.84-25.6 30.72-89.6 51.2-153.6 51.2z" fill="#858E9E"/><path d="M435.2 757.76c-5.12 5.12 2.56 17.92 12.8 25.6s23.04 10.24 28.16 5.12l107.52-81.92-102.4-74.24-46.08 125.44zM929.28 120.32c-28.16-20.48-69.12-15.36-89.6 15.36L509.44 591.36l102.4 74.24 332.8-455.68c20.48-28.16 12.8-69.12-15.36-89.6z" fill="#525C6A"/></svg>`,
    JUXING: `<svg class="icon" width="20px" height="20px" viewBox="0 0 1029 1024" version="1.1"><path fill="#707070" d="M931.590244 1023.500488c-20.90958 0-40.780176-6.613541-57.463883-19.111337-16.134244-12.088195-28.27239-29.301385-34.196605-48.472663l-0.109893-0.349659h-645.669463l-0.109893 0.349659c-5.914224 19.161288-18.062361 36.374478-34.196605 48.472663-16.683707 12.507785-36.554302 19.111337-57.463882 19.111337-52.878361 0-95.906341-43.02798-95.906342-95.906342 0-38.182712 22.627902-72.708995 57.643707-87.964097l0.299708-0.129873V188.116293l-0.319688-0.119883c-17.932488-6.873288-33.257522-18.8416-44.316722-34.636176C8.481717 137.19602 2.497561 118.194576 2.497561 98.403902c0-52.878361 43.02798-95.906341 95.906341-95.906341 40.420527 0 76.715083 25.565034 90.321796 63.607883l0.119882 0.329678h648.306888l0.119883-0.329678C850.879063 28.062595 887.17362 2.497561 927.594146 2.497561c52.878361 0 95.906341 43.02798 95.906342 95.906341 0 39.131785-23.407141 73.987746-59.631766 88.803278l-0.309698 0.129874v649.815414l0.329678 0.119883c38.042849 13.606712 63.607883 49.901268 63.607883 90.321795 0 52.878361-43.02798 95.906341-95.906341 95.906342zM190.354107 125.73721c-9.360859 31.5392-34.885932 56.51481-66.604956 65.186341l-0.369639 0.099903v642.972097l0.38962 0.089912c32.018732 7.282888 58.5728 31.11961 69.302322 62.209249l0.119883 0.339668h647.60757l0.119883-0.339668c10.080156-29.191493 33.757034-51.859356 63.348137-60.64078l0.359649-0.109893V191.532956l-0.37963-0.089912c-32.708059-8.18201-58.99239-33.367415-68.593014-65.705834l-0.109893-0.359649H190.45401l-0.099903 0.359649zM927.594146 2.997073c52.608624 0 95.406829 42.798205 95.40683 95.406829 0 19.321132-5.74439 37.922966-16.613776 53.797464-10.609639 15.504859-25.37522 27.45319-42.708293 34.536273l-0.619395 0.249756v650.504742l0.659356 0.239765c37.843044 13.53678 63.278205 49.641522 63.278205 89.852254 0 52.608624-42.798205 95.406829-95.406829 95.406829-20.799688 0-40.56039-6.57358-57.164176-19.011434-16.054322-12.028254-28.132527-29.151532-34.01678-48.212917l-0.219786-0.709307H193.790751l-0.219785 0.709307c-5.884254 19.061385-17.962459 36.184663-34.016781 48.212917-16.593795 12.437854-36.364488 19.011434-57.164175 19.011434C49.791376 1023.000976 6.993171 980.202771 6.993171 927.594146c0-37.982907 22.50802-72.329366 57.344-87.504546l0.599414-0.259746v-652.06322l-0.639375-0.249756c-17.842576-6.833327-33.087688-18.751688-44.086947-34.456351C8.951259 136.996215 2.997073 118.094673 2.997073 98.403902 2.997073 45.795278 45.795278 2.997073 98.403902 2.997073c40.210732 0 76.315473 25.425171 89.852254 63.278205l0.239766 0.659356H837.492137l0.239765-0.659356C851.278673 28.422244 887.383415 2.997073 927.594146 2.997073M192.831688 897.123902h648.316878l0.229775-0.669346c10.020215-29.041639 33.57721-51.58962 63.018459-60.321093l0.719298-0.209795V191.143337l-0.759259-0.189815c-32.538224-8.142049-58.682693-33.18759-68.233366-65.366166l-0.209795-0.719297H190.084371l-0.209795 0.719297c-4.585522 15.454907-13.137171 29.621073-24.725854 40.96999-11.598673 11.358907-25.964644 19.610849-41.539434 23.866693l-0.739278 0.199805v643.751336l0.779239 0.179825c15.774595 3.586498 30.490224 11.239024 42.568429 22.1184 12.008273 10.819434 21.129366 24.56601 26.374244 39.761171l0.239766 0.689326M927.594146 1.998049c-41.849132 0-77.464351 26.673951-90.791336 63.937561H189.195239C175.868254 28.672 140.253034 1.998049 98.403902 1.998049 45.155902 1.998049 1.998049 45.155902 1.998049 98.403902c0 41.099863 25.724878 76.1856 61.939512 90.052059v650.714537c-34.096702 14.855493-57.943415 48.852293-57.943415 88.423648 0 53.248 43.157854 96.405854 96.405854 96.405854 43.327688 0 79.981893-28.592078 92.130029-67.933659h644.930186c12.148137 39.34158 48.802341 67.933659 92.130029 67.933659 53.248 0 96.405854-43.157854 96.405854-96.405854 0-41.849132-26.673951-77.464351-63.937561-90.791336V187.666732c35.165659-14.375961 59.941463-48.922224 59.941463-89.26283C1024 45.155902 980.842146 1.998049 927.594146 1.998049zM193.550985 896.124878c-10.759493-31.179551-37.073795-55.116176-69.671961-62.528937V191.403083c32.098654-8.771434 57.513834-33.707083 66.954615-65.52601h644.340761c9.640585 32.468293 35.914927 57.783571 68.952663 66.045503v643.241834c-29.800898 8.841366-53.577678 31.709034-63.677814 60.960468H193.550985z"/></svg>`,
    TUOYUAN: `<svg class="icon"width="22px" height="22px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M308.224 962.56c-37.888 0-72.704-6.144-103.424-18.432l-8.192 8.192-24.576-24.576c-15.36-9.216-29.696-19.456-43.008-32.768C63.488 829.44 45.056 727.04 77.824 605.184c31.744-112.64 102.4-228.352 200.704-326.656 98.304-99.328 214.016-171.008 326.656-200.704 120.832-32.768 224.256-14.336 289.792 51.2 65.536 66.56 83.968 169.984 51.2 290.816-30.72 111.616-102.4 227.328-200.704 326.656-98.304 99.328-214.016 171.008-326.656 200.704-38.912 10.24-75.776 15.36-110.592 15.36zM215.04 896c49.152 22.528 116.736 24.576 191.488 5.12 104.448-27.648 211.968-95.232 305.152-188.416 93.184-94.208 159.744-201.728 188.416-305.152 27.648-103.424 13.312-189.44-38.912-243.712-53.248-53.248-139.264-66.56-242.688-38.912-104.448 28.672-211.968 95.232-305.152 188.416-92.16 92.16-159.744 200.704-188.416 305.152C96.256 721.92 110.592 807.936 163.84 860.16c12.288 12.288 24.576 21.504 38.912 28.672l12.288 7.168z"/></svg>`,
    JIANTOU: `<svg class="icon"width="22px" height="22px" viewBox="0 0 1000 1000" version="1.1"><path fill="#707070" d="M259.774 362.57v364.149c0 28.44 23.051 51.491 51.491 51.491h364.149c28.44 0 51.491-23.051 51.491-51.491s-23.051-51.491-51.491-51.491h-239.829l349.073-349.073c20.119-20.119 20.119-52.711 0-72.831s-52.711-20.119-72.831 0l-349.073 349.073v-239.829c0.001-14.202-5.754-27.093-15.076-36.415s-22.195-15.094-36.415-15.076c-28.44 0-51.491 23.051-51.491 51.491z"/></svg>`,
    PENQIANG: `<svg class="icon"width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M844.8 216.436364c6.981818 0 16.290909-2.327273 20.945455-6.981819l90.763636-69.818181c16.290909-11.636364 18.618182-34.909091 6.981818-48.872728-11.636364-16.290909-34.909091-18.618182-48.872727-6.981818l-90.763637 69.818182c-16.290909 11.636364-18.618182 34.909091-6.981818 48.872727 6.981818 9.309091 16.290909 13.963636 27.927273 13.963637zM926.254545 304.872727h-81.454545c-18.618182 0-34.909091 16.290909-34.909091 34.909091s16.290909 34.909091 34.909091 34.909091h81.454545c18.618182 0 34.909091-16.290909 34.909091-34.909091s-16.290909-34.909091-34.909091-34.909091zM947.2 537.6l-81.454545-65.163636c-16.290909-11.636364-37.236364-9.309091-48.872728 6.981818s-9.309091 37.236364 6.981818 48.872727l81.454546 65.163636c6.981818 4.654545 13.963636 6.981818 20.945454 6.981819 9.309091 0 20.945455-4.654545 27.927273-13.963637 11.636364-13.963636 9.309091-37.236364-6.981818-48.872727zM714.472727 67.490909H581.818182v83.781818h-195.490909L332.8 209.454545H109.381818L53.527273 495.709091h111.709091L86.109091 847.127273c-11.636364 46.545455 11.636364 93.090909 53.527273 107.054545 6.981818 2.327273 16.290909 4.654545 23.272727 4.654546 11.636364 0 25.6-2.327273 34.909091-9.309091 18.618182-11.636364 34.909091-30.254545 41.890909-53.527273l148.945454-400.290909h37.236364v232.727273c0 18.618182 16.290909 34.909091 34.909091 34.909091s34.909091-16.290909 34.909091-34.909091v-232.727273H581.818182v83.781818h134.981818c27.927273 0 51.2-25.6 51.2-53.527273V121.018182c-2.327273-30.254545-25.6-53.527273-53.527273-53.527273zM139.636364 425.890909L167.563636 279.272727h195.490909l53.527273-60.509091h162.909091v207.127273H139.636364z m556.218181 83.781818h-46.545454v-372.363636h46.545454v372.363636z"/></svg>`,
    CHONGZUO: `<svg class="icon"width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M727.296 145.28l196.096 196.096-196.096 196.117333-60.352-60.352L760.106667 384H394.666667a202.666667 202.666667 0 0 0 0 405.333333H661.333333v85.333334H394.666667C235.605333 874.666667 106.666667 745.728 106.666667 586.666667S235.605333 298.666667 394.666667 298.666667h365.333333l-93.056-93.056 60.352-60.330667z"/></svg>`,
    CHEXIAO: `<svg class="icon"width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M296.704 145.28l-196.096 196.096 196.096 196.117333 60.352-60.352L263.893333 384H629.333333a202.666667 202.666667 0 0 1 0 405.333333H362.666667v85.333334h266.666666C788.394667 874.666667 917.333333 745.728 917.333333 586.666667S788.394667 298.666667 629.333333 298.666667H264l93.056-93.056-60.352-60.330667z"/></svg>`,
    XIANDUAN: `<svg class="icon"width="22px" height="22px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M693.024 376.224l-316.8 316.8a32 32 0 0 1-45.248-45.248l316.8-316.8a32 32 0 0 1 45.248 45.248z"/><path fill="#707070" d="M240.48 693.024a64 64 0 1 0 90.496 90.496 64 64 0 0 0-90.496-90.496zM195.2 647.776a128 128 0 1 1 181.024 180.992A128 128 0 0 1 195.2 647.776zM693.024 240.48a64 64 0 1 0 90.496 90.496 64 64 0 0 0-90.496-90.496zM647.776 195.2a128 128 0 1 1 180.992 181.024A128 128 0 0 1 647.776 195.2z"/></svg>`,
    ZITIYANGSHI: `<svg class="icon" width="20px" height="20px" viewBox="0 0 1024 1024" version="1.1"><path d="M572.4 584.1m-159.4 0a159.4 159.4 0 1 0 318.8 0 159.4 159.4 0 1 0-318.8 0Z" fill="#FF4C4D"/><path d="M223.3 743.5l54.5-145.4h199.7l51.9 145.4h61.9L408.6 231.8l-6.8-19.7-41.5-0.6-199.3 532h62.3zM380 325l76.6 214.8h-157L380 325zM869.1 474.2c17 19.2 25.4 45.2 25.4 78v183.6H853v-48.1c-11.4 15.9-26.2 28.8-44.2 38.7-21.8 11.4-45.4 17.1-70.8 17.1-28.8 0-51.6-7.4-68.6-22.1-17.7-15.1-26.6-35.2-26.6-60.3 0-36.5 14.6-63.1 43.7-79.6 23.6-14 55.3-21 95.1-21l68.6-0.6v-9.4c0-47.9-24.9-71.9-74.7-71.9-23.2 0-41.3 4.8-54.2 14.4-14.4 10-23.2 24.7-26.5 44.2h-43.7c4.4-32.8 18.4-57.1 42-73 20.6-14.7 49-22.1 85.2-22.1 40.7 0 70.9 10.7 90.8 32.1z m-18.8 120.5l-65.8 0.6c-64.1 0-96.2 21.8-96.2 65.3 0 14 5.2 25.4 15.5 34.3 10.7 8.5 25.2 12.7 43.7 12.7 28.8 0 53.3-8.9 73.6-26.6 19.5-17.7 29.3-38.2 29.3-61.4v-24.9z"/></svg>`,
    ZITIYANSE: `<svg class="icon" width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path d="M839 768H735.3l-74.1-192.7H358.6L288.7 768H185L461.8 64h100.4L839 768zM632.1 495.8L522.3 203.1c-3.4-9.4-7.2-25.7-11.3-49.1h-2.3c-3.4 21.2-7.4 37.6-11.7 49.1L388.1 495.8h244z" fill="#727272"/><path d="M64 832h896v128H64z" fill="#000000"/></svg>`,
    ZIHAO: `<svg class="icon" width="20px" height="20.00px" viewBox="0 0 1024 1024" version="1.1"><path d="M818 728h-160c-18.8 0-34-15.2-34-34s15.2-34 34-34h160c18.8 0 34 15.2 34 34s-15.2 34-34 34z" fill="#FF4C4D"/><path d="M890 903.2c-14 0-26.8-8.4-32-22.4l-152-417.2c-6.4-17.6 2.8-37.2 20.4-43.6 8.4-3.2 17.6-2.8 26 1.2 8.4 4 14.4 10.8 17.6 19.2l152 417.2c6.4 17.6-2.8 37.2-20.4 43.6-3.6 1.6-7.6 2-11.6 2z m-180-440.8z" fill="#FF4C4D"/><path d="M621.6 805.6c-4 0-8-0.8-11.6-2-17.6-6.4-26.8-26-20.4-43.6l116.4-319.6c3.2-8.4 9.2-15.2 17.6-19.2 8.4-4 17.6-4.4 26-1.2 8.4 3.2 15.2 9.2 19.2 17.6 4 8.4 4.4 17.6 1.2 26l-116.4 319.6c-3.2 8.4-9.2 15.2-17.6 19.2-4.8 2-9.6 3.2-14.4 3.2z m-28.4-44.4z m172.8-298.8z" fill="#FF4C4D"/><path d="M123.6 888.4c-4 0-8-0.8-11.6-2-8.4-3.2-15.2-9.2-19.2-17.6-4-8.4-4.4-17.6-1.2-26L347.6 140c6.4-17.6 26-26.8 43.6-20.4 17.6 6.4 26.8 26 20.4 43.6l-256 702.8c-3.2 8.4-9.2 15.2-17.6 19.2-4.8 2.4-9.6 3.2-14.4 3.2z m28.4-23.6c-0.4 0-0.4 0 0 0zM95.2 844c0 0.4 0 0.4 0 0z" fill="#606266"/><path d="M635.6 888.4c-14 0-26.8-8.4-32-22.4L348 163.2c-6.4-17.6 2.8-37.2 20.4-43.6 17.6-6.4 37.2 2.8 43.6 20.4l256 702.8c6.4 17.6-2.8 37.2-20.4 43.6-4 1.6-8 2-12 2z" fill="#606266"/><path d="M514 584h-272c-18.8 0-34-15.2-34-34s15.2-34 34-34h272c18.8 0 34 15.2 34 34s-15.2 34-34 34z" fill="#606266"/></svg>`,
    BEIJINGYANGSHI: `<svg class="icon" width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"><path fill="#707070" d="M925.522557 1024v-1023.960617h39.3831v1023.960617z m-866.428214 0v-1023.960617h39.3831v1023.960617zM98.477443 1023.960617h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.383101H98.477443v-19.69155h19.691551v-39.3831H98.477443v-19.691551h19.691551v-39.3831H98.477443v-19.691551h19.691551v-39.3831H98.477443v-19.691551h19.691551V157.532403H98.477443V137.840852h19.691551V98.457752H98.477443V78.766201h19.691551V39.383101H98.477443V19.69155h19.691551V0h19.69155v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.691551v19.69155h39.3831V0h19.69155v19.69155h19.691551v19.691551h-19.691551v39.3831h19.691551v19.691551h-19.691551v39.3831h19.691551v19.691551h-19.691551v39.3831h19.691551v19.691551h-19.691551v39.3831h19.691551v19.691551h-19.691551v39.3831h19.691551v19.691551h-19.691551v39.3831h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101h19.691551v19.69155h-19.691551v39.383101z m787.662013 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101h-39.3831v39.383101z m-59.074651 0v-39.383101H196.935195v39.383101z m-59.074651 0v-39.383101H137.860544v39.383101z m708.895812-59.074651v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831H196.935195v39.3831z m-59.074651 0v-39.3831H137.860544v39.3831z m708.895812-59.074651v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831H196.935195v39.3831z m-59.074651 0v-39.3831H137.860544v39.3831z m708.895812-59.074651v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831h-39.3831v39.3831z m-59.074651 0v-39.3831H196.935195v39.3831z m-59.074651 0v-39.3831H137.860544v39.3831z m708.895812-59.074651V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403h-39.3831v39.3831z m-59.074651 0V157.532403H196.935195v39.3831z m-59.074651 0V157.532403H137.860544v39.3831z m708.895812-59.074651V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831z m-59.074651 0V98.457752h-39.3831v39.3831zM236.318295 137.840852V98.457752H196.935195v39.3831zM177.243644 137.840852V98.457752H137.860544v39.3831z m708.895812-59.074651V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831z m-59.074651 0V39.383101h-39.3831v39.3831zM236.318295 78.766201V39.383101H196.935195v39.3831zM177.243644 78.766201V39.383101H137.860544v39.3831z"/></svg>`,
    JIANQIE: `<svg  t="1739169577462" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1551" width="20" height="20"><path fill="#707070" d="M288 124a36 36 0 0 1 35.92 33.536l0.08 2.464v92H704a68 68 0 0 1 67.92 64.704l0.08 3.296v380H864a36 36 0 0 1 35.92 33.536l0.08 2.464a36 36 0 0 1-33.536 35.92L864 772h-92V864a36 36 0 0 1-71.92 2.464L700 864v-92H320a68 68 0 0 1-67.92-64.704L252 704V324H160a36 36 0 0 1-35.92-33.536L124 288a36 36 0 0 1 33.536-35.92L160 252h92V160A36 36 0 0 1 288 124z m412 576v-376h-376v376h376z"   p-id="1552"></path></svg>`,
    CHARU: `<svg t="1739167492664" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="4211" width="18" height="18"><path d="M192 544a32 32 0 0 1 0-64h640a32 32 0 0 1 0 64z" p-id="4212"></path><path d="M480 192a32 32 0 0 1 64 0v640a32 32 0 0 1-64 0z" p-id="4213"></path></svg>`,
    QUANPING: `<svg t="1739168131879" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="5585" width="20" height="16"><path d="M145.066667 85.333333h153.6c25.6 0 42.666667-17.066667 42.666666-42.666666S324.266667 0 298.666667 0H34.133333C25.6 0 17.066667 8.533333 8.533333 17.066667 0 25.6 0 34.133333 0 42.666667v256c0 25.6 17.066667 42.666667 42.666667 42.666666s42.666667-17.066667 42.666666-42.666666V145.066667l230.4 230.4c17.066667 17.066667 42.666667 17.066667 59.733334 0 17.066667-17.066667 17.066667-42.666667 0-59.733334L145.066667 85.333333z m170.666666 563.2L162.133333 802.133333l-76.8 76.8V725.333333C85.333333 699.733333 68.266667 682.666667 42.666667 682.666667s-42.666667 17.066667-42.666667 42.666666v256c0 25.6 17.066667 42.666667 42.666667 42.666667h256c25.6 0 42.666667-17.066667 42.666666-42.666667s-17.066667-42.666667-42.666666-42.666666H145.066667l76.8-76.8 153.6-153.6c17.066667-17.066667 17.066667-42.666667 0-59.733334-17.066667-17.066667-42.666667-17.066667-59.733334 0z m665.6 34.133334c-25.6 0-42.666667 17.066667-42.666666 42.666666v153.6l-76.8-76.8-153.6-153.6c-17.066667-17.066667-42.666667-17.066667-59.733334 0-17.066667 17.066667-17.066667 42.666667 0 59.733334l153.6 153.6 76.8 76.8H725.333333c-25.6 0-42.666667 17.066667-42.666666 42.666666s17.066667 42.666667 42.666666 42.666667h256c25.6 0 42.666667-17.066667 42.666667-42.666667v-256c0-25.6-17.066667-42.666667-42.666667-42.666666z m0-682.666667h-256c-25.6 0-42.666667 17.066667-42.666666 42.666667s17.066667 42.666667 42.666666 42.666666h153.6l-76.8 76.8-153.6 153.6c-17.066667 17.066667-17.066667 42.666667 0 59.733334 17.066667 17.066667 42.666667 17.066667 59.733334 0l153.6-153.6 76.8-76.8v153.6c0 25.6 17.066667 42.666667 42.666666 42.666666s42.666667-17.066667 42.666667-42.666666v-256c0-25.6-17.066667-42.666667-42.666667-42.666667z" fill="#707070" p-id="5586"></path></svg>`,
    QUXIAOQUANPING: `<svg t="1739168211747" fill="#707070" class="icon" viewBox="0 0 1028 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="6640" width="20" height="16"><path d="M631.874913 676.029564v307.086828a39.857982 39.857982 0 0 0 79.715964 0v-221.853699l250.818816 249.863922a38.054293 38.054293 0 0 0 53.933826-53.721628l-250.818816-249.863922h222.808593a39.681149 39.681149 0 1 0 0-79.397665h-308.25392a43.712924 43.712924 0 0 0-48.204463 47.886164z m-280.137597-46.719071H43.483396a39.716516 39.716516 0 1 0 0 79.397665h222.808593L15.437806 958.430614a38.054293 38.054293 0 1 0 53.933826 53.721627l250.818816-249.863922v221.853699a39.857982 39.857982 0 0 0 79.715964 0v-307.086827c-2.511017-32.537128-18.850314-47.709332-48.169096-47.709332z m325.831042-234.691718h308.25392a39.716516 39.716516 0 1 0 0-79.397666h-222.808593l250.818816-249.863922A38.054293 38.054293 0 0 0 959.898675 11.63556L709.256691 261.711681V39.857982a39.857982 39.857982 0 0 0-79.715963 0V346.944809a44.773917 44.773917 0 0 0 48.062997 47.709332zM397.395394 347.935069V40.848242a39.857982 39.857982 0 0 0-79.715963 0v221.853699L66.860615 12.838019A38.054293 38.054293 0 0 0 12.926789 66.559646l250.818816 249.863922h-222.808593a39.716516 39.716516 0 1 0 0 79.397666h308.25392c31.865165-1.167093 48.204462-16.339297 48.204462-47.886165z" p-id="6641"></path></svg>`,
    XIANGZUOXUANZHUAN: `<svg t="1739168949753" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="4172" width="20" height="20"><path d="M469.333333 384h426.666667a42.666667 42.666667 0 0 1 42.666667 42.666667v426.666666a42.666667 42.666667 0 0 1-42.666667 42.666667H469.333333a42.666667 42.666667 0 0 1-42.666666-42.666667V426.666667a42.666667 42.666667 0 0 1 42.666666-42.666667z m42.666667 85.333333v341.333334h341.333333v-341.333334h-341.333333z m-256-17.664l77.994667-78.037333 60.373333 60.373333L213.333333 614.997333 32.298667 434.005333l60.373333-60.373333L170.666667 451.669333V341.333333a213.333333 213.333333 0 0 1 213.333333-213.333333h170.666667v85.333333H384a128 128 0 0 0-128 128v110.336z" fill="#707070" p-id="4173"></path></svg>`,
    XIANGYOUXUANZHUAN: `<svg t="1739169220187" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="6142" width="20" height="20"><path d="M451.669333 170.666667L373.632 92.672 434.005333 32.298667 614.997333 213.333333l-180.992 181.034667-60.373333-60.373333L451.669333 256H341.333333a128 128 0 0 0-128 128v170.666667H128V384a213.333333 213.333333 0 0 1 213.333333-213.333333h110.336zM384 469.333333a42.666667 42.666667 0 0 1 42.666667-42.666666h426.666666a42.666667 42.666667 0 0 1 42.666667 42.666666v426.666667a42.666667 42.666667 0 0 1-42.666667 42.666667H426.666667a42.666667 42.666667 0 0 1-42.666667-42.666667V469.333333z m85.333333 42.666667v341.333333h341.333334v-341.333333h-341.333334z"  fill="#707070"  p-id="6143"></path></svg>`,
    QUEREN: `<svg t="1739175283105" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="5046" width="20" height="20"><path d="M413.243 806.833a40.913 40.913 0 0 1-28.924-11.984L73.985 484.475c-15.98-15.98-15.98-41.888 0-57.848 15.98-15.98 41.888-15.98 57.848 0l281.41 281.449 478.923-478.924c15.961-15.98 41.889-15.98 57.85 0 15.979 15.98 15.979 41.868 0 57.848L442.167 794.849a40.915 40.915 0 0 1-28.924 11.984z" fill="#259e84" p-id="5047"></path></svg>`,
    QUXIAO: `<svg t="1739175547534" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="4541" width="16" height="16"><path d="M842.947458 778.116917 576.847937 512.013303 842.946434 245.883083c8.67559-8.674567 13.447267-20.208251 13.43908-32.477692-0.008186-12.232602-4.7727-23.715121-13.414521-32.332383-8.655124-8.677637-20.149922-13.450337-32.384571-13.4575-12.286838 0-23.808242 4.771677-32.474622 13.434987L512.019443 447.143876 245.88206 181.050496c-8.66331-8.66331-20.175505-13.434987-32.416294-13.434987-12.239765 0-23.75196 4.770653-32.414247 13.43294-8.66024 8.636704-13.428847 20.12434-13.437034 32.356942-0.008186 12.269441 4.76349 23.803125 13.437034 32.476669l266.135336 266.13022L181.050496 778.11794c-8.664334 8.66331-13.43601 20.173458-13.43601 32.41527 0 12.239765 4.7727 23.752983 13.437034 32.417317 8.662287 8.66331 20.173458 13.43294 32.413224 13.43294 12.240789 0 23.754007-4.770653 32.416294-13.43294l266.134313-266.100544 266.101567 266.100544c8.66331 8.66331 20.185738 13.43294 32.4429 13.43294 12.265348-0.008186 23.74889-4.771677 32.369222-13.412474C860.81643 825.081555 860.821547 795.991006 842.947458 778.116917z" fill="#707070" p-id="4542"></path></svg>`,
    JINGXIANG: `<svg t="1739241694160" class="icon" viewBox="0 0 1112 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="6832" width="20" height="20"><path d="M582.299 61.058h-462.484c-18.473 0-33.502 15.028-33.502 33.502v345.932c0 18.473 15.028 33.502 33.502 33.502h462.484c18.473 0 33.503-15.028 33.503-33.502v-345.932c0-18.473-15.029-33.502-33.503-33.502zM558.913 417.104h-415.711v-299.157h415.711v299.157z" p-id="6833"></path><path d="M992.292 590.713h-460.523c-18.473 0-33.502 15.028-33.502 33.502v344.951c0 18.473 15.028 33.502 33.502 33.502h460.523c18.473 0 33.502-15.028 33.502-33.502v-344.951c0-18.473-15.028-33.502-33.502-33.502zM968.905 945.778h-413.749v-298.176h413.749v298.176z" p-id="6834"></path><path d="M323.187 770.207h-118.765v-118.682h58.851l-88.276-118.682-89.664 118.682h60.238v177.533h177.616v55.908l118.192-85.333-118.192-90.238z" fill="#515151" p-id="6835"></path></svg>`,
};
//输入域的属性表达式的属性列表
var INPUTFIELDPROPERTYEXPRESSIONSARRAY = [
    // "FormulaValue",
    "AcceptTab",
    "AdornTextType",
    "Alignment",
    "AutoFixFontSize",
    "AutoSetSpellCodeInDropdownList",
    "BackgroundText",
    "BackgroundTextColor",
    "BackgroundTextItalic",
    "BorderElementColor",
    "BorderTextPosition",
    "BorderVisible",
    "BringoutToSave",
    "CanBeReferenced",
    "ContentReadonly",
    "CustomAdornText",
    "CustomValueEditorTypeName",
    "DataName",
    "DefaultSelectedIndexs",
    "DefaultValueType",
    "Deleteable",
    "DisplayFormat",
    "EditorActiveMode",
    "EditorControlTypeName",
    "EnableHighlight",
    "EnablePermission",
    "EnableUserEditInnerValue",
    "EndBorderText",
    "EndingLineBreak",
    "FastInputMode",
    "FormButtonStyle",
    "InnerValue",
    "LabelText",
    "MaxInputLength",
    "Modified",
    "MoveFocusHotKey",
    "PrintVisibility",
    "ReferencedDataName",
    "SelectedIndex",
    "SelectedSpellCode",
    "ShowFormButton",
    "ShowInputFieldStateTag",
    "SpecifyWidth",
    "StartBorderText",
    "TabIndex",
    "TabStop",
    "TagValue",
    "Text",
    "TextColor",
    "ToolTip",
    "UnitText",
    "UserEditable",
    "UserFlags",
    "ViewEncryptType",
    // "Visible"
];
//单复选框的属性表达式列表
var RADIOCHECKPROPERTYEXPRESSIONSARRAY = [
    "FormulaValue",
    "BringoutToSave",
    "CanBeReferenced",
    "Caption",
    "CaptionAlign",
    "CaptionFlowLayout",
    "CheckAlignLeft",
    "Checked",
    "ContentReadonly",
    "DataName",
    "DefaultCheckedForValueBinding",
    "Deleteable",
    "Enabled",
    "Name",
    "PrintVisibility",
    "ReferencedDataName",
    "StringTag",
    "Tag",
    // "Visible"
];
export default {
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
};
