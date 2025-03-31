"use strict";
//import { es6laydate as laydate } from './js/laydate.js'

export let WriterControl_DateTimeControl = {
    //创建时间选择界面
    CreateDateTimeControl: function (divContainer, dtmInputValue, rootElement, intStyle, callBack) {
        if (dtmInputValue == null) {
            dtmInputValue = new Date();
        }
        if (intStyle != 5) {
            //替换文本
            dtmInputValue = new Date(dtmInputValue);
            const year = dtmInputValue.getFullYear();
            const month = dtmInputValue.getMonth() + 1; // 由于月份从0开始，因此需加1
            const day = dtmInputValue.getDate();
            const hour = dtmInputValue.getHours();
            const minute = dtmInputValue.getMinutes();
            const second = dtmInputValue.getSeconds();
            function pad(timeEl, total = 2, str = '0') {
                return timeEl.toString().padStart(total, str);
            }
            dtmInputValue = `${pad(year, 4)}-${pad(month)}-${pad(day)} ${pad(hour)}:${pad(minute)}:${pad(second)}`;
        }
        WriterControl_DateTimeControl.rootElement = rootElement;
        //对divContainer添加一个隐藏的输入域
        var hiddenInput = rootElement.ownerDocument.createElement('input');
        hiddenInput.style.cssText = 'padding:0px;height:0px;border:0px;vertical-align: top';
        hiddenInput.setAttribute('readonly', true);
        hiddenInput.id = 'DCDateTime_calendar';
        hiddenInput.value = dtmInputValue;
        divContainer.appendChild(hiddenInput);
        //判断参数
        var dateType = 'datetime';
        var dateFormat = 'Y-m-d H:i:s';
        switch (intStyle) {
            case 2:
                dateType = 'date';
                dateFormat = 'Y-m-d';
                break;
            case 3:
                dateType = 'datetime';
                dateFormat = 'Y-m-d H:i:s';
                break;
            case 4:
                dateType = 'datetime';
                dateFormat = 'Y-m-d H:i';
                break;
            case 5:
                dateType = 'time';
                dateFormat = 'H:i:s';
                break;
            default:
                var dateType = 'datetime';
                dateFormat = 'Y-m-d H:i:s';
                break;
        }
        var thisApi = rootElement.ownerDocument.cxCalendar.attach(hiddenInput, {
            startDate: new Date().getFullYear() - 100,
            endDate: new Date().getFullYear() + 100
        });
        thisApi.setOptions({
            type: dateType,
            //format: 'Y-m-d H:i:s'
            format: dateFormat,
            //baseClass: 'not_secs',
        });
        if (dateFormat.indexOf('s') < 0) {
            thisApi.setOptions({
                baseClass: 'dc_not_secs',
            });
        }
        hiddenInput.addEventListener('change', function (e) {
            callBack && callBack(e.target.value);
            callBack = null;
        });
        thisApi.show(rootElement);
        divContainer.thisApi = thisApi;
        //var hasScale = window.getComputedStyle(divContainer, null).getPropertyValue("transform").slice(7, 'matrix(0.826923, 0, 0, 0.826923, 0, 0)'.length - 1).split(', ')[0]
        //if (hasScale && hasScale != '0') {
        //    thisApi.dom.panel.style.transform = "scale(" + hasScale + ")";
        //    thisApi.dom.panel.style.top = thisApi.dom.panel.offsetTop - (((1 - hasScale) * 0.5) * thisApi.dom.panel.offsetHeight) + 'px';
        //    thisApi.dom.panel.style.left = thisApi.dom.panel.offsetLeft - (((1 - hasScale) * 0.5) * thisApi.dom.panel.offsetWidth)  + 'px';
        //}
    },

    CreateCalendarCss: function (rootElement) {
        var calendarCssString = `/*!
        * cxCalendar
        * ------------------------------ */
       /* 火狐浏览器兼容样式 */
        @-moz-document url-prefix() {
            .dc_cxcalendar {
               //width:254px !important;
            }
            .dc_cxcalendar_hd .times input{
                padding: 0 !important;
            }
        }
        .dc_cxcalendar {
        --cxcalendar-bg: #fff;
        --cxcalendar-border: #e4e4e4;
        --cxcalendar-item-bg: #e4e4e4;
        --cxcalendar-text-color: #333;
        --cxcalendar-days-color: #666;
        --cxcalendar-sat-color: #4a89dc;
        --cxcalendar-sun-color: #da4453;
        --cxcalendar-other-color: #ccc;
        --cxcalendar-note-color: #aaa;
        --cxcalendar-now-bg: #f3f3f3;
        --cxcalendar-set-bg: #70a9ce;
        --cxcalendar-range-bg: #dceffc;
        --cxcalendar-range-set-bg: #70a9ce;
        --cxcalendar-btn-color: #fff;
        --cxcalendar-btn-bg: #666;
        --cxcalendar-confirm-bg: #4a89dc;
        --cxcalendar-gap-out: 8px;
        --cxcalendar-text-size: 14px;
        --cxcalendar-title-size: 16px;
        --cxcalendar-unit-size: 10px;
        --cxcalendar-btn-size: 12px;
        position: fixed;
        z-index: 10000;
        top: -999px;
        left: -999px;
        width: 400px;
        border: 1px solid var(--cxcalendar-border);
        border-radius: 3px;
        background-color: var(--cxcalendar-bg);
        box-shadow: 1px 2px 3px rgba(0,0,0,.2);
        color: var(--cxcalendar-text-color);
        font-size: var(--cxcalendar-text-size);
        opacity: 0;
        transform: translate(0,5%);
        transition-property: opacity,transform;
        /* transition-duration: .3s; */
        -webkit-user-select: none;
        user-select: none
        box-sizing: border-box;
        }
        .dc_cxcalendar *{
        box-sizing: border-box;
        }
        .dc_cxcalendar_mask {
        display: none;
        position: fixed;
        z-index: 9999;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: rgba(0,0,0,0)
        }
        .dc_cxcalendar.dc_show {
        opacity: 1;
        transform: translate(0,0);
        transform: scale(0.9);
        transform-origin: top left;
        }
        .dc_cxcalendar.dc_show + .dc_cxcalendar_mask {
        display: block
        }
        .dc_cxcalendar_wp {
        }
        .dc_cxcalendar_wp_body {
         margin-left:70px;
        }
        .dc_cxcalendar_sb{
        position: absolute;
        top: 0;
        // width: 378px;
        box-sizing: border-box;
        padding-top: 6px;
        background-color: #fff;
        overflow: auto;
        }
        .dc_cxcalendar_sb ul{
        text-align: left;
        padding-left: 7px;
        
        }
        .dc_cxcalendar_sb ul li{
        // margin-top:5px;
        font-size:14px;
        line-height: 28px;
        }
        .dc_cxcalendar_sb ul li span{
            display: block;
            border-radius: 8px;
            min-width: 50px;
            border: 1px solid var(--cxcalendar-border);
            text-align: center;
            margin-top: 10px;
            padding: 0px 13px;
        }
        .dc_cxcalendar_sb ul li:first-child span{
             margin-top: 2px;
        }
        .dc_cxcalendar_sb ul li:hover{
        cursor: pointer;
        color:var(--cxcalendar-confirm-bg);
        }
        .dc_cxcalendar_hd {
        position: relative;
        height: 84px;
        //padding:0 var(--cxcalendar-gap-out);
        padding-bottom: 0;
        font-size: var(--cxcalendar-title-size);
        line-height: 32px;
        text-align: center;
        //zoom: 0.8
        }
        .dc_cxcalendar_hd .next,
        .dc_cxcalendar_hd .prev {
        position: absolute;
        top: var(--cxcalendar-gap-out);
        width: 30px;
        height: 30px;
        padding: 0;
        border: 1px solid transparent;
        border-radius: 3px;
        color: var(--cxcalendar-text-color);
        font-size: 0;
        line-height: 0;
        text-decoration: none;
        outline: 0;
        transition-property: border-color,background-color;
        transition-duration: .2s;
        margin-top:38px;
        }
        .dc_cxcalendar_hd .prev {
        left: var(--cxcalendar-gap-out)
        }
        .dc_cxcalendar_hd .next {
        right: var(--cxcalendar-gap-out)
        }
        .dc_cxcalendar_hd .next:before,
        .dc_cxcalendar_hd .prev:before {
        content: "";
        height: 10px;
        width: 10px;
        top: 12px;
        border-width: 1px 1px 0 0;
        border-color: black;
        border-style: solid;
        position: absolute;
        }
        
        .dc_cxcalendar_hd .prev:before {
        transform: matrix(-0.71, -0.71, 0.71, -0.71, 0, 0);
        }
        .dc_cxcalendar_hd .next:before {
        transform: matrix(0.71, 0.71, -0.71, 0.71, 0, 0);
        }

        .dc_prev-next-box-group,.dc_prev-next-box{
            display:flex;
            justify-content: space-between;
            margin: 0 13px;
        }
        .dc_prev-next-box{
            width:50px;
        }
        .dc_prev-next-box-group .dc_prev-next-box:first-child{
            margin-left: 7px;
        }
        .dc_prevYear,.dc_prevMonth,.dc_nextMonth,.dc_nextYear :hover{
            cursor: pointer;
        }
        .dc_arrow-left,.dc_arrow-right{
            height: 22px;
            display :inline-block;
            position: relative;
          }
          .dc_arrow-right::after,.dc_arrow-left::before {
            content: "";
            height: 6px;
            width: 6px;
            top: 12px;
            border-width: 1px 1px 0 0;
            border-color: #303133;
            border-style: solid;
            position: absolute;
          }
          .dc_arrow-right::after {
            transform: matrix(0.71, 0.71, -0.71, 0.71, 0, 0);
          }
          .dc_arrow-left::before {
            transform: matrix(-0.71, -0.71, 0.71, -0.71, 0, 0);
          }

        .dc_cxcalendar_hd,.dc_cxcalendar_bd{
        border-left: 1px solid var(--cxcalendar-border);
        }
        .dc_cxcalendar_hd .dc_times input,.dc_cxcalendar_hd .dc_times select,
        .dc_cxcalendar_hd select {
        display: inline-block;
        box-sizing: border-box;
        height: 30px;
        margin: 0;
        padding: 0 .5em;
        border: 1px solid transparent;
        border-radius: 3px;
        background: 0 0;
        color: var(--cxcalendar-text-color);
        font-weight: 400;
        font-size: var(--cxcalendar-title-size);
        line-height: 30px;
        text-align: center;
        vertical-align: top;
        outline: 0;
        cursor: pointer;
        transition-property: border-color,background-color;
        transition-duration: .2s;
        -webkit-appearance: none;
        appearance: none;
        }
        .dc_cxcalendar_hd .dc_times input{
        width: 42px;
        // background-color: #f3f3f3;
        padding: 0px;
        font-size: var(--cxcalendar-text-size);
        -webkit-appearance: none;
        background-color: #fff;
        background-image: none;
        border-radius: 4px;
        border: 1px solid #dcdfe6;
        box-sizing: border-box;
        color: #606266;
        display: inline-block;
        // font-size: inherit;
        outline: none;
        // padding: 0 15px;
        transition: border-color .2s cubic-bezier(.645,.045,.355,1);
        }
        .dc_cxcalendar_hd em {
        display: inline-block;
        padding: 0 1px;
        font-style: normal
        }
        .dc_cxcalendar_hd .dc_year + em:after {
        content: '年'
        }
        .dc_cxcalendar_hd .dc_month + em:after {
        content: '月'
        }
        .dc_cxcalendar_hd .dc_fill {
        font-weight: 400;
        font-size: var(--cxcalendar-text-size)
        }
        .dc_cxcalendar_hd .dc_fill span {
        padding: 0 4px
        }
        .dc_cxcalendar_hd .dc_times select:hover,
        .dc_cxcalendar_hd .dc_next:hover,
        .dc_cxcalendar_hd .dc_prev:hover,
        .dc_cxcalendar_hd select:hover {
        border-color: var(--cxcalendar-border);
        background: var(--cxcalendar-item-bg)
        }
        .dc_cxcalendar_bd {
        position: relative;
        padding: var(--cxcalendar-gap-out);
        padding-top: 0;
        z-index: 1;
        //margin-top: 12px;
        // border-top: 1px solid var(--cxcalendar-border);
        //zoom:0.8
        }
        .dc_cxcalendar_bd ul {
        position: relative;
        margin: 0;
        padding: 0;
        list-style: none;
        color: var(--cxcalendar-days-color);
        line-height: 32px;
        text-align: center;
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        align-content: flex-start;
        }
        .dc_cxcalendar_bd ul li {
        box-sizing: border-box;
        position: relative;
        height: 36px;
        margin: 0;
        padding: 0;
        //border: 2px solid var(--cxcalendar-bg);
        border-radius: 5px;
        cursor: pointer;
        flex: none;
        transition-property: background-color,color;
        transition-duration: .2s
        }
        .dc_cxcalendar_bd ul li.del,
        .dc_cxcalendar_bd ul li.week {
        cursor: default
        }
        .dc_cxcalendar_bd ul li.del,
        .dc_cxcalendar_bd ul li.del.holiday,
        .dc_cxcalendar_bd ul li.del.now,
        .dc_cxcalendar_bd ul li.del.sat,
        .dc_cxcalendar_bd ul li.del.sun {
        color: var(--cxcalendar-other-color);
        text-decoration: line-through
        }
        .dc_cxcalendar_bd ul li.now {
        /* background-color: var(--cxcalendar-now-bg) */
        }
        .dc_cxcalendar_bd ul li.del:hover,
        .dc_cxcalendar_bd ul li.now.del,
        .dc_cxcalendar_bd ul li.week:hover {
        background: 0 0
        }
        .cxcalendar_bd ul li:hover {
        background-color: var(--cxcalendar-item-bg)
        }
        .dc_cxcalendar_bd ul li.DCcxCalendarSelected,
        .dc_cxcalendar_bd ul li.DCcxCalendarSelected.dc_holiday,
        .dc_cxcalendar_bd ul li.DCcxCalendarSelected.dc_other,
        .dc_cxcalendar_bd ul li.DCcxCalendarSelected.dc_sat,
        .dc_cxcalendar_bd ul li.DCcxCalendarSelected.dc_sun,
        .dc_cxcalendar_bd ul li.DCcxCalendarSelected:hover {
        /* background-color: var(--cxcalendar-set-bg); */
        background-color: var(--cxcalendar-item-bg);
        /* color: var(--cxcalendar-btn-color) */
        color: var(--cxcalendar-text-color)
        }
        .dc_cxcalendar_bd ul li.del:after,
        .dc_cxcalendar_bd ul li.DCcxCalendarSelected:after {
        color: inherit
        }
        .dc_cxcalendar_bd .dc_days li {
        flex-basis: 14%
        }
        .dc_cxcalendar_bd .dc_days .dc_sat {
        /* color: var(--cxcalendar-sat-color) */
        color: var(--cxcalendar-days-color)
        }
        .dc_cxcalendar_bd .dc_days .dc_holiday,
        .dc_cxcalendar_bd .dc_days .dc_sun {
        /* color: var(--cxcalendar-sun-color) */
        color: var(--cxcalendar-days-color)
        }
        .dc_cxcalendar_bd .dc_days .dc_other {
        color: var(--cxcalendar-other-color)
        }
        .dc_cxcalendar_bd .dc_days li.dc_week.dc_sat,
        .dc_cxcalendar_bd .dc_days li.dc_week.dc_sun {
        color: inherit
        }
        .dc_cxcalendar_bd .dc_months li {
        flex-basis: 33%
        }
        .dc_cxcalendar_bd .dc_years li {
        flex-basis: 25%
        }
        .dc_cxcalendar_bd .dc_months li:after,
        .dc_cxcalendar_bd .dc_years li:after {
        content: '月';
        display: inline-block;
        margin-left: 2px;
        color: var(--cxcalendar-note-color);
        font-size: var(--cxcalendar-unit-size);
        vertical-align: top
        }
        .dc_cxcalendar_bd .dc_years li:after {
        content: '年'
        }
        .dc_cxcalendar_hd .dc_times {
        position: relative;
        //margin-top: var(--cxcalendar-gap-out);
        // padding-top: var(--cxcalendar-gap-out);
        padding: var(--cxcalendar-gap-out) var(--cxcalendar-gap-out) 0;
        border-bottom: 1px solid var(--cxcalendar-border);
        color: var(--cxcalendar-note-color);
        line-height: 32px;
        display: flex
        }
        .dc_cxcalendar_hd .dc_times:before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        height: 1px;
        //background: linear-gradient(to right,rgba(0,0,0,0) 0,rgba(0,0,0,.1) 15%,rgba(0,0,0,.1) 85%,rgba(0,0,0,0) 100%)
        }
        .dc_cxcalendar_hd .dc_times:only-child:before {
        display: none
        }
        .dc_cxcalendar_hd .dc_times section {
        flex: 1;
        display: flex;
        justify-content: center;
        height: 31.99px;
        margin-bottom: var(--cxcalendar-gap-out);
        }
        .dc_cxcalendar_hd .dc_times section:before {
        content: '';
        margin-right: 3px;
        font-size: 13px;
        }
        .dc_cxcalendar_hd .dc_times select {
        font-weight: 400;
        font-size: var(--cxcalendar-text-size)
        }
        .dc_cxcalendar_hd .dc_times i {
        padding: 0 1px;
        font-style: normal
        }
        .dc_cxcalendar_hd .dc_times .dc_time-group i:after {
        content: ':'
        }
        .dc_cxcalendar_hd .dc_times .dc_date-group i:after {
        content: '-'
        }
        .dc_cxcalendar_hd .dc_times .dc_date-group input:first-child {
        width:60px;
        }
        .dc_cxcalendar_acts {
        padding: var(--cxcalendar-gap-out) 5px;
        border-top: 1px solid var(--cxcalendar-border);
        border-radius: 0 0 3px 3px;
        /* background-color: var(--cxcalendar-item-bg); */
        font-size: 13px;
        line-height: 30px;
        display: flex;
        justify-content: flex-end;
        //zoom: 0.8;
        }
        .dc_cxcalendar_acts a {
        font-weight: 700;
        padding: 0 15px;
        border-radius: 3px;
        /* background-color: var(--cxcalendar-btn-bg); */
        background-color: var(--cxcalendar-now-bg);
        /* color: var(--cxcalendar-btn-color); */
        color: var(--cxcalendar-text-color);
        text-decoration: none;
        text-align: center;
        transition-property: opacity;
        transition-duration: .2s;
        height: 30px;
        font-size: 13px;
        }
        .dc_cxcalendar_acts a + a {
        margin-left: 15px
        }
        .dc_cxcalendar_acts a:hover {
        /* color: var(--cxcalendar-btn-color); */
        color: var(--cxcalendar-text-color);
        opacity: .6
        }
        .dc_cxcalendar_acts .dc_today:before {
        content: '今天'
        }
        .dc_cxcalendar_acts .dc_clear {
        height:100%
        }
        .dc_cxcalendar_acts .dc_clear:before {
        content: '清除'
        }
        .dc_cxcalendar_acts .dc_confirm {
        background-color: var(--cxcalendar-confirm-bg);
        height: 100%
        }
        .dc_cxcalendar_acts .dc_confirm:before {
        content: '确定'
        }
        .dc_cxcalendar.m_datetime .dc_cxcalendar_acts .dc_today:before,
        .dc_cxcalendar.dc_m_time .dc_cxcalendar_acts .dc_today:before {
        content: '现在'
        }
        .dc_cxcalendar.m_year .dc_cxcalendar_acts .dc_today:before {
        content: '今年'
        }
        .dc_cxcalendar.m_month .dc_cxcalendar_acts .dc_today:before {
        content: '本月'
        }
        .dc_cxcalendar.dc_at_end .dc_cxcalendar_hd .dc_next,
        .dc_cxcalendar.dc_at_start .dc_cxcalendar_hd .prev {
        color: var(--cxcalendar-other-color);
        cursor: default
        }
        .dc_cxcalendar.dc_at_end .dc_cxcalendar_hd .dc_next:hover,
        .dc_cxcalendar.dc_at_start .dc_cxcalendar_hd .dc_prev:hover {
        border-color: transparent;
        background: 0 0
        }
        .dc_cxcalendar.dc_range {
        width: 524px
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd .dc_days,
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd .dc_months,
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd .dc_years,
        .dc_cxcalendar.dc_range .dc_cxcalendar_hd {
        display: flex
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd ul,
        .dc_cxcalendar.dc_range .dc_cxcalendar_hd section {
        flex: 1
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd ul + ul {
        margin-left: var(--cxcalendar-gap-out)
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd ul + ul:before {
        content: '';
        position: absolute;
        top: 0;
        bottom: 0;
        left: -4px;
        width: 1px;
        background: linear-gradient(to bottom,rgba(0,0,0,0) 0,rgba(0,0,0,.1) 35%,rgba(0,0,0,.1) 65%,rgba(0,0,0,0) 100%)
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd ul li.dc_other {
        visibility: hidden
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd ul li.dc_DCcxCalendarSelected {
        background-color: var(--cxcalendar-range-bg);
        color: var(--cxcalendar-days-color)
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd ul li.dc_DCcxCalendarSelected.dc_end,
        .dc_cxcalendar.dc_range .dc_cxcalendar_bd ul li.dc_DCcxCalendarSelected.dc_start {
        background-color: var(--cxcalendar-range-set-bg);
        color: var(--cxcalendar-btn-color)
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_hd .dc_times section:first-child:before {
        content: '开始时间'
        }
        .dc_cxcalendar.dc_range .dc_cxcalendar_hd .dc_times section:nth-child(2):before {
        content: '结束时间'
        }
        .dc_cxcalendar.dc_fixed {
        position: fixed;
        top: auto;
        bottom: -500px;
        left: 0;
        right: 0;
        width: auto;
        border: none;
        border-radius: 0;
        box-shadow: none;
        opacity: 1;
        transform: none;
        transition-property: bottom
        }
        .dc_cxcalendar.dc_fixed + .dc_cxcalendar_mask {
        display: block;
        background-color: rgba(0,0,0,0);
        transform: translate(0,-100%);
        transition-property: background-color,transform;
        transition-duration: .3s,0s;
        transition-delay: 0s,0.3s
        }
        .dc_cxcalendar.dc_fixed.dc_show {
        bottom: 0
        }
        .dc_cxcalendar.dc_fixed.dc_show + .dc_cxcalendar_mask {
        background-color: rgba(0,0,0,.4);
        transform: translate(0,0);
        transition-delay: 0s
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_bd ul:nth-child(2),
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_hd section:nth-child(2) {
        display: none
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_hd .dc_times {
        display: block
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_hd .dc_prev {
        left: 24px
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_hd .dc_next {
        right: 24px
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_bd {
        padding-bottom: 20px
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_bd ul {
        line-height: 36px
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_bd ul li {
        height: 40px
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_hd .dc_times {
        padding-top: 10px;
        padding-bottom: 10px
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_acts {
        position: absolute;
        top: auto;
        left: auto;
        bottom: 100%;
        right: 10px;
        width: auto;
        padding: 0;
        border: none;
        background: 0 0;
        line-height: 32px;
        display: flex
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_acts a {
        border-radius: 3px 3px 0 0
        }
        .dc_cxcalendar.dc_has_weeknum .dc_cxcalendar_bd .dc_days ul {
        padding-left: 24px
        }
        .dc_cxcalendar.dc_has_weeknum .dc_cxcalendar_bd .dc_days li[data-week-num]:before {
        content: attr(data-week-num);
        position: absolute;
        top: 50%;
        left: -16px;
        width: 16px;
        margin-top: -8px;
        margin-left: -8px;
        border-radius: 3px;
        background-color: rgba(0,0,0,.1);
        color: #fff;
        font-size: var(--cxcalendar-unit-size);
        line-height: 16px;
        text-align: center;
        pointer-events: none
        }
        .dc_cxcalendar.dc_not_secs .dc_times .dc_mint + i,
        .dc_cxcalendar.dc_not_secs .dc_times .dc_secs {
        display: none
        }
        .dc_cxcalendar.dc_not_acts .dc_cxcalendar_acts {
        display: none
        }
        .dc_cxcalendar.dc_en .dc_cxcalendar_bd .dc_months li:after,
        .dc_cxcalendar.dc_en .dc_cxcalendar_bd .dc_years li:after,
        .dc_cxcalendar.dc_en .dc_cxcalendar_hd .dc_month + em:after,
        .dc_cxcalendar.dc_en .dc_cxcalendar_hd .dc_year + em:after {
        content: ''
        }
        .dc_cxcalendar.dc_en .dc_cxcalendar_hd .dc_times section:before {
        content: 'Time:'
        }
        .dc_cxcalendar.dc_en .dc_cxcalendar_acts .dc_today:before {
        content: 'Now'
        }
        .dc_cxcalendar.dc_en .dc_cxcalendar_acts .dc_clear:before {
        content: 'Clear'
        }
        .dc_cxcalendar.dc_en .dc_cxcalendar_acts .dc_confirm:before {
        content: 'Ok'
        }
        .dc_cxcalendar.dc_en.dc_range .dc_cxcalendar_hd .dc_times section:first-child:before {
        content: 'Start Time:'
        }
        .dc_cxcalendar.dc_en.dc_range .dc_cxcalendar_hd .dc_times section:nth-child(2):before {
        content: 'End Time:'
        }

        .dc_only-time {
            width: 230px;
        }

        .dc_only-time .dc_cxcalendar_wp_body{
            margin-left: 0px;
        }
        .dc_only-time .dc_cxcalendar_hd{
            height: 45px;
        }
        .dc_only-time .dc_cxcalendar_acts{
            justify-content: center;
        }
        .dc_only-time .dc_cxcalendar_bd{
            display:none;
        }

        .dc_only-time .dc_cxcalendar_hd .dc_times input{
            width: 50px;
        }    

        @media (min-width:640px) {
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_bd ul:nth-child(2),
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_hd section:nth-child(2) {
        display: inherit
        }
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_hd .dc_times {
        display: flex
        }
        }
        @media (width:375px) and (height:812px),
        (width:414px) and (height:896px),
        (width:390px) and (height:844px),
        (width:428px) and (height:926px) {
        .dc_cxcalendar.dc_fixed .dc_cxcalendar_bd {
        padding-bottom: env(safe-area-inset-bottom)
        }
        }
        `;
        let oldCxcalendar = rootElement.ownerDocument.querySelector('.dc_cxcalendar');
        if (oldCxcalendar) {
            return;
        }
        let oldCss = rootElement.ownerDocument.getElementById('dc_calendarCss');
        if (!oldCss) {
            var calendarCss = rootElement.ownerDocument.createElement('style');
            rootElement.ownerDocument.head.appendChild(calendarCss);
            calendarCss.id = 'dc_calendarCss';
            calendarCss.innerHTML = calendarCssString;
        }
        /**
         * cxCalendar
         * @version 3.0.5
         * @author ciaoca
         * @email ciaoca@gmail.com
         * @site https://github.com/ciaoca/cxCalendar
         * @license Released under the MIT license
         */
        (function (rootElement, factory) {
            //typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
            //    typeof define === 'function' && define.amd ? define(factory) :
            //        (global = typeof globalThis !== 'undefined' ? globalThis : global || self, global.cxCalendar = factory());
            // global = typeof globalThis !== 'undefined' ? globalThis : global || self, global.cxCalendar = factory();
            rootElement.ownerDocument.cxCalendar = factory();
        })(rootElement, (function () {
            'use strict';

            const theTool = {
                dom: {},
                reg: {
                    isYear: /^\d{4}$/,
                    isTime: /^\d{1,2}(\:\d{1,2}){1,2}$/
                },
                cxId: 1,
                bindFuns: {},
                cacheDate: {},
                cacheApi: null,

                // 默认语言
                language: {
                    am: '上午',
                    pm: '下午',
                    monthList: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                    weekList: ['日', '一', '二', '三', '四', '五', '六'],
                },

                isElement: (o) => {
                    if (o && (typeof HTMLElement === 'function' || typeof HTMLElement === 'object') && o instanceof HTMLElement) {
                        return true;
                    } else {
                        return (o && o.nodeType && o.nodeType === 1) ? true : false;
                    }
                },
                isInteger: (value) => {
                    if (typeof value === 'string' && /^\-?\d+$/.test(value)) {
                        value = parseInt(value, 10);
                    } return typeof value === 'number' && isFinite(value);
                },
                isObject: (value) => {
                    if (value === undefined || value === null || Object.prototype.toString.call(value) !== '[object Object]') {
                        return false;
                    }
                    if (value.constructor && !Object.prototype.hasOwnProperty.call(value.constructor.prototype, 'isPrototypeOf')) {
                        return false;
                    }
                    return true;
                },
                isDate: (value) => {
                    return (value instanceof Date || Object.prototype.toString.call(value) === '[object Date]') && isFinite(value.getTime());
                },
            };

            // 合并对象
            theTool.extend = function (target, ...sources) {
                const self = this;

                if (!self.isObject(target)) {
                    return;
                }
                for (let x of sources) {
                    if (!self.isObject(x)) {
                        continue;
                    }
                    for (let y in x) {
                        if (Array.isArray(x[y])) {
                            target[y] = [].concat(x[y]);

                        } else if (self.isObject(x[y])) {
                            if (!self.isObject(target[y])) {
                                target[y] = {};
                            } self.extend(target[y], x[y]);

                        } else {
                            target[y] = x[y];
                        }
                    }
                }
                return target;
            };

            // 补充前置零
            theTool.fillLeadZero = function (value, num) {
                let str = String(value);

                if (str.length < num) {
                    str = Array(num - str.length).fill(0).join('') + value;
                }
                return str;
            };

            // 获取当年每月的天数
            theTool.getMonthDays = function (year) {
                const leapYearDay = ((year % 4 === 0 && year % 100 !== 0) || year % 400 === 0) ? 1 : 0;
                return [31, 28 + leapYearDay, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            };

            // 获取周数
            theTool.getWeekNum = function (dateObj) {
                const self = this;
                const curTime = dateObj.getTime();
                const yearFirstDate = new Date(dateObj.getFullYear(), 0, 1, 0, 0, 0, 0);
                let weekFirstTime = yearFirstDate.getTime();
                let weekDay = yearFirstDate.getDay();
                let weekNum = 0;

                if (weekDay === 0) {
                    weekDay = 7;
                }
                if (weekDay > 4) {
                    weekFirstTime += (8 - weekDay) * 86400000;
                } else {
                    weekFirstTime += (1 - weekDay) * 86400000;
                }
                if (curTime < weekFirstTime) {
                    weekNum = self.getWeekNum(new Date(dateObj.getFullYear() - 1, 11, 31));
                } else {
                    weekNum = Math.floor((curTime - weekFirstTime) / 86400000) + 1;
                    weekNum = Math.ceil(weekNum / 7);
                }
                return weekNum;
            };

            /**
             * 解析日期
             * 默认支持 ISO 8601 格式，和以下格式
             * y
             * y-m
             * y-m-d
             * y-m-d h:i
             * y-m-d h:i:s
             * m-d
             * m-d h:i
             * m-d h:i:s
             * h:i
             * h:i:s
             * 
             * 日期连接符 '-' 可替换为 '.' 或 '/'
            **/
            theTool.parseDate = function (value, mustDef) {
                const self = this;
                let theDate = new Date();

                if (self.reg.isYear.test(value)) {
                    theDate.setFullYear(parseInt(value, 10));

                } else if (self.isInteger(value)) {
                    theDate.setTime(parseInt(value, 10));

                } else if (typeof value === 'string' && value.length) {
                    let tags;

                    if (self.reg.isTime.test(value)) {
                        tags = value.split(':').map((x) => {
                            return parseInt(x, 10);
                        });

                        if (tags.length < 4) {
                            tags = tags.concat(Array(4 - tags.length).fill(0));
                        } else if (tags.length > 4) {
                            tags.length = 4;
                        }
                        theDate.setHours.apply(theDate, tags);

                    } else {
                        value = value.replace(/[\.\/]/g, '-');
                        if (/^\d{1,2}-\d{1,2}/.test(value)) {
                            value = theDate.getFullYear() + '-' + value;
                        } else if (/^\d{4}-\d{1,2}$/.test(value)) {
                            value += '-1';
                        }
                        tags = value.split(/[\-\sT\:]/).map((x) => {
                            return parseInt(x, 10);
                        });

                        if (tags.length > 1) {
                            tags[1] -= 1;
                        }
                        if (tags.length < 7) {
                            tags = tags.concat(Array(7 - tags.length).fill(0));
                        } else if (tags.length > 7) {
                            tags.length = 7;
                        }
                        theDate.setFullYear.apply(theDate, tags.slice(0, 3));
                        theDate.setHours.apply(theDate, tags.slice(3));
                    }
                } else {
                    theDate = null;
                }
                if (mustDef === true && !self.isDate(theDate)) {
                    theDate = new Date();
                }
                return theDate;
            };

            // 格式化日期值
            theTool.formatDate = function (style, time, language) {
                const self = this;
                const theDate = self.parseDate(time);
                const lang = self.extend({}, self.language);

                if (self.isObject(language)) {
                    self.extend(lang, language);
                }
                if (typeof style !== 'string' || !self.isDate(theDate)) {
                    return time;
                }
                const attr = {
                    Y: theDate.getFullYear(),
                    n: theDate.getMonth() + 1,
                    j: theDate.getDate(),
                    G: theDate.getHours(),
                    timestamp: theDate.getTime(),
                };

                attr.y = attr.Y.toString(10).slice(-2);
                attr.m = self.fillLeadZero(attr.n, 2);
                attr.d = self.fillLeadZero(attr.j, 2);
                attr.W = self.getWeekNum(theDate);

                attr.H = self.fillLeadZero(attr.G, 2);
                attr.g = attr.G > 12 ? attr.G - 12 : attr.G;
                attr.h = self.fillLeadZero(attr.g, 2);
                attr.i = self.fillLeadZero(theDate.getMinutes(), 2);
                attr.s = self.fillLeadZero(theDate.getSeconds(), 2);
                attr.a = attr.G > 12 ? lang.pm : lang.am;

                const keys = ['timestamp', 'Y', 'y', 'm', 'n', 'd', 'j', 'W', 'H', 'h', 'G', 'g', 'i', 's', 'a'];
                const reg = new RegExp('(' + keys.join('|') + ')', 'g');
                let str = style;

                // 转义边界符号
                str = str.replace(/([\{\}])/g, '\\$1');

                // 转义关键词
                str = str.replace(reg, (match, p1) => {
                    return '{{' + p1 + '}}';
                });

                // 还原转义字符
                str = str.replace(/\\\{\{(.)\}\}/g, '$1');

                // 替换关键词
                for (let x of keys) {
                    str = str.replace(new RegExp('{{' + x + '}}', 'g'), attr[x]);
                }
                // 还原转义内容
                str = str.replace(/\\(.)/g, '$1');

                return str;
            };

            // 获取语言配置
            theTool.getLanguage = function (name) {
                const self = this;
                const lang = {};

                if (self.isObject(name)) {
                    return self.extend(lang, self.language, name);
                }
                if (typeof name !== 'string' || !name.length) {
                    if (typeof navigator.language === 'string') {
                        name = navigator.language;
                    } else if (typeof navigator.browserLanguage === 'string') {
                        name = navigator.browserLanguage;
                    }
                }
                if (typeof name === 'string' && name.length && window.cxCalendar && self.isObject(window.cxCalendar.languages)) {
                    name = name.toLowerCase();
                    if (self.isObject(window.cxCalendar.languages[name])) {
                        self.extend(lang, window.cxCalendar.languages[name]);
                    } else if (self.isObject(window.cxCalendar.languages.default)) {
                        self.extend(lang, window.cxCalendar.languages.default);
                    }
                }
                if (!Object.keys(lang).length) {
                    self.extend(lang, self.language);
                }
                return lang;
            };

            theTool.init = function () {
                const self = this;
                self.buildStage();
                self.bindEvent();
            };

            // 构建选择器
            theTool.buildStage = function () {
                const self = this;

                self.dom.maskBg = rootElement.ownerDocument.createElement('div');
                self.dom.maskBg.classList.add('dc_cxcalendar_mask');

                self.dom.panel = rootElement.ownerDocument.createElement('div');
                self.dom.panel.classList.add('dc_cxcalendar');

                self.dom.wrapper = rootElement.ownerDocument.createElement('div');
                self.dom.wrapper.classList.add('dc_cxcalendar_wp');

                self.dom.wrapperBody = rootElement.ownerDocument.createElement('div');
                self.dom.wrapperBody.classList.add('dc_cxcalendar_wp_body');

                self.dom.sidebar = rootElement.ownerDocument.createElement('div');
                self.dom.sidebar.classList.add('dc_cxcalendar_sb');

                self.dom.head = rootElement.ownerDocument.createElement('div');
                self.dom.head.classList.add('dc_cxcalendar_hd');

                self.dom.main = rootElement.ownerDocument.createElement('div');
                self.dom.main.classList.add('dc_cxcalendar_bd');

                self.dom.acts = rootElement.ownerDocument.createElement('div');
                self.dom.acts.classList.add('dc_cxcalendar_acts');

                self.dom.dateSet = rootElement.ownerDocument.createElement('div');

                self.dom.timeSet = rootElement.ownerDocument.createElement('div');
                self.dom.timeSet.classList.add('dc_times');

                self.dom.wrapperBody.insertAdjacentElement('beforeend', self.dom.main);

                self.dom.wrapper.insertAdjacentElement('beforeend', self.dom.wrapperBody);

                self.dom.panel.insertAdjacentElement('beforeend', self.dom.wrapper);

                rootElement.ownerDocument.body.insertAdjacentElement('beforeend', self.dom.panel);

                self.dom.panel.insertAdjacentElement('afterend', self.dom.maskBg);
            };

            // 绑定事件
            theTool.bindEvent = function () {
                const self = this;

                // 关闭面板
                self.dom.maskBg.addEventListener('click', () => {
                    self.hidePanel();
                });

                self.dom.panel.addEventListener('click', (e) => {
                    const el = e.target;
                    const nodeName = el.nodeName.toLowerCase();
                    if (nodeName === 'a' && el.rel) {
                        // 时间下拉框按下确定避免报错
                        if (e.preventDefault) {
                            e.preventDefault();
                        }

                        switch (el.rel) {
                            // 上个月
                            case 'dc_prev':
                                self.gotoPrev();
                                break;

                            // 下个月
                            case 'dc_next':
                                self.gotoNext();
                                break;

                            // 今日
                            case 'dc_today':
                                self.hidePanel();
                                self.cacheApi.setDate(new Date().getTime());
                                break;

                            // 清除
                            case 'dc_clear':
                                self.hidePanel();
                                self.cacheApi.clearDate();
                                break;

                            // 确认
                            case 'dc_confirm':
                                self.hidePanel();
                                if (self.cacheApi.settings.mode === 'range') {
                                    self.confirmRange();
                                } else {
                                    self.confirmTime();
                                }
                                break;


                        }
                        // 选择日期
                    } else if (nodeName === 'li' && el.dataset.date) {
                        const dateText = el.dataset.date;

                        if (typeof dateText !== 'string' || !dateText.length) {
                            return;
                        }
                        for (let x of el.parentNode.parentNode.querySelectorAll('li')) {
                            x.classList.remove('DCcxCalendarSelected');
                        } el.classList.add('DCcxCalendarSelected');

                        // 范围选择，需手动确认
                        if (self.cacheApi.settings.mode === 'range') {
                            const theDate = self.parseDate(dateText);
                            theDate.setHours(0, 0, 0, 0);
                            const theTime = theDate.getTime();

                            if (typeof self.cacheDate.startTime === 'number' && typeof self.cacheDate.endTime === 'number') {
                                if ((theTime >= self.cacheDate.startTime && theTime <= self.cacheDate.endTime)) {
                                    delete self.cacheDate.startTime;
                                    delete self.cacheDate.endTime;
                                } else {
                                    self.cacheDate.startTime = theTime;
                                    delete self.cacheDate.endTime;
                                }
                            } else if (typeof self.cacheDate.startTime === 'number') {
                                if (theTime === self.cacheDate.startTime) {
                                    delete self.cacheDate.startTime;
                                } else if (theTime > self.cacheDate.startTime) {
                                    self.cacheDate.endTime = theTime;
                                } else {
                                    self.cacheDate.startTime = theTime;
                                }
                            } else {
                                self.cacheDate.startTime = theTime;
                            }
                            self.gotoDate();
                            return;
                        }
                        // 时间选择，需手动确认
                        if (self.cacheApi.settings.type === 'datetime') {
                            self.cacheDate.time = self.parseDate(dateText).getTime();
                            self.setDateValues(self.cacheDate.time);
                            return;
                        }
                        self.hidePanel();
                        self.cacheApi.setDate(dateText);

                    }
                });

                // 选择年月
                self.dom.panel.addEventListener('change', (e) => {
                    const el = e.target;
                    const nodeName = el.nodeName.toLowerCase();

                    if (nodeName === 'select' && ['year', 'month'].indexOf(el.name) >= 0) {
                        self.rebuildMonthSelect();
                        self.gotoDate();
                    }
                });
                // 屏蔽日期时间选择框的右键菜单20230928
                self.dom.panel.addEventListener('contextmenu', function (e) {
                    e.preventDefault();
                });
                // 屏蔽日期弹出框的遮罩层右键菜单20230928  修改写法不然初始化报错
                //let cxcalendar_mask = document.getElementsByClassName('cxcalendar_mask')[0]
                self.dom.maskBg.addEventListener('contextmenu', function (e) {
                    e.preventDefault();
                });

                // 左侧按钮事件
                self.dom.sidebar.addEventListener('click', function (e) {
                    const el = e.target;
                    const nodeName = el.nodeName.toLowerCase();
                    const today = new Date();
                    let selectDate;
                    switch (el.className) {
                        case 'dc_today':
                            //年月下拉框赋值
                            $("select.dc_year").val(today.getFullYear());
                            $("select.dc_month").val(today.getMonth() + 1);

                            //日期输入框赋值
                            self.setDateValues(today);
                            //跳转指定日历
                            self.gotoDate();
                            //格式化日期，定位日历
                            selectDate = theTool.formatDate('Y-m-d', today.getTime());
                            self.locateDay(selectDate);
                            break;
                        case 'dc_yesterday':
                            //获取昨天日期
                            let yesterdayDate = new Date(today.getTime() - 24 * 60 * 60 * 1000);
                            //年月下拉框赋值
                            $("select.dc_year").val(yesterdayDate.getFullYear());
                            $("select.dc_month").val(yesterdayDate.getMonth() + 1);
                            //日期输入框赋值
                            self.setDateValues(yesterdayDate);
                            //跳转指定日历
                            self.gotoDate();
                            //格式化昨日日期，定位日历
                            selectDate = theTool.formatDate('Y-m-d', (today.getTime() - 24 * 60 * 60 * 1000));
                            self.locateDay(selectDate);
                            break;
                        case 'dc_lastMonth':
                            self.gotoPrev();
                            break;
                        case 'dc_nextMonth':
                            self.gotoNext();
                            break;

                        default:
                            break;
                    }
                });
            };

            // 获取内部选框控件
            theTool.getSelects = function (list, values) {
                const self = this;
                const selects = {};

                for (let x of self.dom.head.querySelectorAll('select')) {
                    if (list.indexOf(x.name) >= 0) {
                        selects[x.name] = x;

                        if (self.isObject(values)) {
                            values[x.name] = parseInt(x.value, 10);
                        }
                    }
                }
                return selects;
            };

            // 创建面板
            theTool.buildPanel = function () {
                const self = this;

                if (self.cacheApi.settings.date) {
                    self.cacheDate = {
                        time: self.cacheApi.defDate.time,
                    };

                    if (typeof self.cacheApi.defDate.start === 'number' && typeof self.cacheApi.defDate.end === 'number') {
                        self.cacheDate.startTime = self.cacheApi.defDate.start;
                        self.cacheDate.endTime = self.cacheApi.defDate.end;
                    }
                } else {
                    self.cacheDate = {};
                }
                //格式化innerHTML
                self.dom.sidebar.innerHTML = '';
                self.dom.wrapper.innerHTML = '';
                self.dom.head.innerHTML = '';
                self.dom.main.innerHTML = '';
                self.dom.timeSet.innerHTML = '';
                // 基础样式
                const classValue = ['dc_cxcalendar', 'm_' + self.cacheApi.settings.type];

                if (self.cacheApi.settings.mode === 'range') {
                    classValue.push('dc_range');
                }
                if (typeof self.cacheApi.settings.baseClass === 'string' && self.cacheApi.settings.baseClass.length) {
                    classValue.push(self.cacheApi.settings.baseClass);
                }
                self.dom.panel.className = classValue.join(' ');

                const splitHtml = '<em></em>';
                // const prevNextHtml = '<a class="prev" href="javascript://" rel="prev"></a><a class="next" href="javascript://" rel="next"></a>';
                const prevHtml = `<div class='dc_prev-next-box'>
                                    <div class='dc_prevYear' name='prevYear'>
                                        <span class="dc_arrow-left"></span>
                                        <span class="dc_arrow-left"></span>
                                    </div>
                                    <div class='dc_prevMonth'>
                                        <span class="dc_arrow-left" name='prev'></span>
                                    </div>
                                 </div>`;

                const prevNextHtml = `<div class='dc_prev-next-box'>
                                        <div class='dc_nextMonth' >
                                            <span class="dc_arrow-right" name='next'></span>
                                        </div>
                                        <div class='dc_nextYear' name='next'>
                                            <span class="dc_arrow-right"></span>
                                            <span class="dc_arrow-right"></span>
                                        </div>
                                    </div>`;

                const fillHtml = self.cacheApi.settings.mode === 'range' ? '<section class="dc_fill"></section>' : '';


                let html = `<div class='dc_prev-next-box-group'>` + prevHtml + '<section>';
                // let html ='<div><section>';

                // 年份选择框
                if (['month', 'date', 'datetime'].indexOf(self.cacheApi.settings.type) >= 0) {
                    html += '<select name="year" class="dc_year">';

                    for (let i = self.cacheApi.minDate.year; i <= self.cacheApi.maxDate.year; i++) {
                        html += '<option value="' + i + '">' + i + '</option>';
                    }
                    html += '</select>';

                } else if (self.cacheApi.settings.type === 'year') {
                    const start = Math.floor(self.cacheApi.minDate.year / 10) * 10;

                    html += '<select name="year" class="dc_year">';

                    for (let i = start; i <= self.cacheApi.maxDate.year; i += self.cacheApi.settings.yearNum) {
                        const end = i + self.cacheApi.settings.yearNum - 1;
                        html += '<option value="' + i + '">' + i + ' - ';
                        // html += end < self.cacheApi.maxDate.year ? end : self.cacheApi.maxDate.year;
                        html += end;
                        html += '</option>';
                    }
                    html += '</select>';
                }
                if (['date', 'datetime'].indexOf(self.cacheApi.settings.type) >= 0) {
                    html += splitHtml;
                    html += '<select name="month" class="dc_month"></select>';
                    html += splitHtml;
                    html += '</section>';
                    html += fillHtml;
                    html += prevNextHtml;
                    html += '</div>';

                    self.dom.head.innerHTML = html;
                    self.dom.dateSet.className = 'dc_days';

                    self.dom.main.insertAdjacentElement('beforeend', self.dom.dateSet);
                    self.dom.wrapperBody.insertAdjacentElement('beforeend', self.dom.main);
                    self.dom.wrapperBody.insertAdjacentElement('afterbegin', self.dom.head);
                    self.dom.wrapper.insertAdjacentElement('afterbegin', self.dom.wrapperBody);
                    self.buildFlipFunc();

                    if (self.cacheApi.settings.type === 'datetime') {
                        self.buildDateTimes();

                    } else {
                        self.buildDate();
                    }
                    self.rebuildMonthSelect();
                    self.gotoDate([self.cacheApi.defDate.year, self.cacheApi.defDate.month].join('-'));

                    let sidebarHTML = `<ul><li><span class='dc_today'>今天</span></li><li><span class='dc_yesterday'>昨天</span></li><li><span class='dc_lastMonth'>上月</span></li><li><span class='dc_nextMonth'>下月</span></li></ul>`;
                    self.dom.sidebar.innerHTML = sidebarHTML;
                    self.dom.wrapper.insertAdjacentElement('afterbegin', self.dom.sidebar);

                } else if (self.cacheApi.settings.type === 'time') {
                    //if (self.dom.wrapperBody.contains(self.dom.head)) {
                    //    self.dom.wrapperBody.removeChild(self.dom.head);
                    //    //self.dom.wrapper.insertAdjacentElement('afterbegin', self.dom.wrapperBody);
                    //}
                    self.dom.wrapperBody.insertAdjacentElement('afterbegin', self.dom.head);
                    self.dom.panel.className += ' dc_only-time';
                    self.dom.wrapper.insertAdjacentElement('afterbegin', self.dom.wrapperBody);
                    self.buildTimes();

                } else if (self.cacheApi.settings.type === 'month') {
                    html += splitHtml;
                    html += '</section>';
                    html += fillHtml;
                    html += prevNextHtml;
                    html += '</div>';

                    self.dom.head.innerHTML = html;
                    self.dom.dateSet.className = 'dc_months';

                    self.dom.main.insertAdjacentElement('beforeend', self.dom.dateSet);
                    self.dom.wrapperBody.insertAdjacentElement('beforeend', self.dom.main);
                    self.dom.wrapperBody.insertAdjacentElement('afterbegin', self.dom.head);
                    self.dom.wrapper.insertAdjacentElement('afterbegin', self.dom.wrapperBody);
                    self.buildFlipFunc();

                    self.gotoDate(self.cacheApi.defDate.year);

                } else if (self.cacheApi.settings.type === 'year') {
                    html += '</section>';
                    html += fillHtml;
                    html += prevNextHtml;
                    html += '</div>';

                    self.dom.head.innerHTML = html;
                    self.dom.dateSet.className = 'dc_years';

                    self.dom.main.insertAdjacentElement('beforeend', self.dom.dateSet);
                    self.dom.wrapperBody.insertAdjacentElement('beforeend', self.dom.main);
                    self.dom.wrapperBody.insertAdjacentElement('afterbegin', self.dom.head);
                    self.dom.wrapper.insertAdjacentElement('afterbegin', self.dom.wrapperBody);
                    self.buildFlipFunc();

                    self.gotoDate(self.cacheApi.defDate.year);
                }

                self.buildActs();
            };

            // 构建操作按钮
            theTool.buildActs = function () {
                const self = this;
                const nowDate = new Date();
                const nowTime = nowDate.getTime();
                const list = [];

                if (self.cacheApi.settings.button.today !== false && self.cacheApi.settings.mode !== 'range' && self.cacheApi.minDate.time <= nowTime && self.cacheApi.maxDate.time >= nowTime) {
                    list.push('dc_today');
                }
                if (self.cacheApi.settings.button.clear !== false) {
                    list.push('dc_clear');
                }
                if (self.cacheApi.settings.mode === 'range' || ['datetime', 'time', 'date'].indexOf(self.cacheApi.settings.type) >= 0) {
                    list.push('dc_confirm');
                }
                let html = '';

                for (let x of list) {
                    html += '<a class="' + x + '" href="javascript://" rel="' + x + '"></a>';
                }
                if (html.length) {
                    self.dom.acts.innerHTML = html;
                    self.dom.panel.insertAdjacentElement('beforeend', self.dom.acts);

                } else if (self.dom.panel.contains(self.dom.acts)) {
                    self.dom.panel.removeChild(self.dom.acts);
                }
            };

            // 重新构建月份选项
            theTool.rebuildMonthSelect = function () {
                const self = this;
                const values = {};
                const selects = self.getSelects(['year', 'month'], values);
                let start = 1;
                let end = 12;

                if (values.year === self.cacheApi.minDate.year && values.year === self.cacheApi.maxDate.year) {
                    start = self.cacheApi.minDate.month;
                    end = self.cacheApi.maxDate.month;
                } else if (values.year === self.cacheApi.minDate.year) {
                    start = self.cacheApi.minDate.month;
                } else if (values.year === self.cacheApi.maxDate.year) {
                    end = self.cacheApi.maxDate.month;
                }
                let html = '';

                for (let i = start; i <= end; i++) {
                    html += '<option value="' + i + '"';

                    if (values.month === i) {
                        html += ' selected';
                    }
                    html += '>' + self.cacheApi.settings.language.monthList[i - 1] + '</option>';
                }
                selects.month.innerHTML = html;
            };

            // 构建日期列表
            theTool.buildDays = function (year, month) {
                const self = this;

                if (!self.isInteger(year) || !self.isInteger(month)) {
                    return;
                }
                const theDate = new Date(year, month - 1, 1);
                year = theDate.getFullYear();
                month = theDate.getMonth() + 1;

                if (year < self.cacheApi.minDate.year || (year === self.cacheApi.minDate.year && month < self.cacheApi.minDate.month)) {
                    year = self.cacheApi.minDate.year;
                    month = self.cacheApi.minDate.month;

                    // } else if (year > self.cacheApi.maxDate.year || (year === self.cacheApi.maxDate.year && month > self.cacheApi.maxDate.month)) {
                    //   year = self.cacheApi.maxDate.year;
                    //   month = self.cacheApi.maxDate.month;
                }
                const jsMonth = month - 1;
                const monthDays = self.getMonthDays(year);
                const sameMonthDate = new Date(year, jsMonth, 1);
                const nowDate = new Date();
                const nowText = [nowDate.getFullYear(), nowDate.getMonth() + 1, nowDate.getDate()].join('-');
                const selectedText = self.formatDate('Y-n-j', self.cacheDate.time);

                // 获取当月第一天
                let monthFirstDay = sameMonthDate.getDay() - self.cacheApi.settings.weekStart;
                if (monthFirstDay < 0) {
                    monthFirstDay += 7;
                }
                // 获取周末位置
                const saturday = 6 - self.cacheApi.settings.weekStart;
                const sunday = (7 - self.cacheApi.settings.weekStart) % 7;

                // 自适应或固定行数
                const monthDayMax = self.cacheApi.settings.lockRow ? 42 : Math.ceil((monthDays[jsMonth] + monthFirstDay) / 7) * 7;

                // 日期范围值
                const rangeValue = {};
                let rangeMaxTime = 0;

                if (typeof self.cacheDate.startTime === 'number') {
                    rangeValue.start = parseInt(self.formatDate('Ymd', self.cacheDate.startTime), 10);

                    if (typeof self.cacheApi.settings.rangeMaxDay === 'number' && self.cacheApi.settings.rangeMaxDay) {
                        rangeMaxTime = self.cacheDate.startTime + self.cacheApi.settings.rangeMaxDay * 86400000;
                    }
                    if (typeof self.cacheDate.endTime === 'number') {
                        rangeValue.end = parseInt(self.formatDate('Ymd', self.cacheDate.endTime), 10);
                    } else {
                        rangeValue.end = rangeValue.start;
                    }
                }
                let html = '<ul>';

                // 星期排序
                for (let i = 0; i < 7; i++) {
                    html += '<li class="dc_week';

                    // 高亮周末
                    if (i === saturday) {
                        html += ' sat';
                    } else if (i === sunday) {
                        html += ' sun';
                    }
                    html += '">' + self.cacheApi.settings.language.weekList[(i + self.cacheApi.settings.weekStart) % 7] + '</li>';
                }
                for (let i = 0; i < monthDayMax; i++) {
                    const classValue = [];
                    let todayYear = year;
                    let todayMonth = month;
                    let todayNum = i - monthFirstDay + 1;

                    // 填充上月和下月的日期
                    if (todayNum <= 0) {
                        classValue.push('dc_other');

                        if (todayMonth <= 1) {
                            todayYear--;
                            todayMonth = 12;
                            todayNum = monthDays[11] + todayNum;
                        } else {
                            todayMonth--;
                            todayNum = monthDays[jsMonth - 1] + todayNum;
                        }
                    } else if (todayNum > monthDays[jsMonth]) {
                        classValue.push('dc_other');

                        if (todayMonth >= 12) {
                            todayYear++;
                            todayMonth = 1;
                            todayNum = todayNum - monthDays[0];
                        } else {
                            todayMonth++;
                            todayNum -= monthDays[jsMonth];
                        }
                    }
                    const todayDate = new Date(todayYear, todayMonth - 1, todayNum);
                    const todayTime = todayDate.getTime();
                    const todayText = [todayYear, todayMonth, todayNum].join('-');
                    const todayInt = parseInt([todayYear, self.fillLeadZero(todayMonth, 2), self.fillLeadZero(todayNum, 2)].join(''), 10);
                    let todayName = '';

                    // 高亮已选择
                    if (self.cacheApi.settings.mode === 'range') {
                        if (todayInt === rangeValue.start || todayInt === rangeValue.end || (todayInt >= rangeValue.start && todayInt <= rangeValue.end)) {
                            classValue.push('DCcxCalendarSelected');

                            if (todayInt === rangeValue.start) {
                                classValue.push('dc_start');
                            } if (todayInt === rangeValue.end) {
                                classValue.push('dc_end');
                            }
                        }
                    } else if (todayText === selectedText) {
                        classValue.push('DCcxCalendarSelected');
                    }
                    // 高亮今天
                    if (todayText === nowText) {
                        classValue.push('dc_now');
                    }
                    // 高亮周末
                    if (i % 7 === saturday) {
                        classValue.push('dc_sat');
                    } else if (i % 7 === sunday) {
                        classValue.push('dc_sun');
                    }
                    if (rangeMaxTime && todayTime > rangeMaxTime) {
                        classValue.push('dc_del');

                        // 超出范围的无效日期
                    } else if (todayTime < self.cacheApi.minDate.time || todayTime > self.cacheApi.maxDate.time) {
                        classValue.push('dc_del');

                        // 不可选择的日期（星期）
                    } else if (Array.isArray(self.cacheApi.settings.disableWeek) && self.cacheApi.settings.disableWeek.length && self.cacheApi.settings.disableWeek.indexOf((i + self.cacheApi.settings.weekStart) % 7) >= 0) {
                        classValue.push('dc_del');

                        // 不可选择的日期
                    } else if (Array.isArray(self.cacheApi.settings.disableDay) && self.cacheApi.settings.disableDay.length) {
                        if (self.cacheApi.settings.disableDay.indexOf(String(todayNum)) >= 0 || self.cacheApi.settings.disableDay.indexOf([todayMonth, todayNum].join('-')) >= 0 || self.cacheApi.settings.disableDay.indexOf([todayYear, todayMonth, todayNum].join('-')) >= 0) {
                            classValue.push('dc_del');
                        }
                    }
                    // 判断是否有节假日
                    if (self.cacheApi.holiday) {
                        const keys = [
                            [todayYear, todayMonth, todayNum].join('-'),
                            [todayMonth, todayNum].join('-'),
                        ];

                        for (let x of keys) {
                            if (typeof self.cacheApi.holiday[x] === 'string') {
                                classValue.push('dc_holiday');
                                todayName = self.cacheApi.holiday[x];
                                break;
                            }
                        }
                    }
                    html += '<li';

                    if (classValue.length) {
                        html += ' class="' + classValue.join(' ') + '"';
                    }
                    if (classValue.indexOf('del') === -1) {
                        html += ' data-date="' + todayText + '"';
                    }
                    if (todayName.length) {
                        html += ' data-title="' + todayName + '"';
                    }
                    if (i % 7 === 0) {
                        html += ' data-week-num="' + self.getWeekNum(todayDate) + '"';
                    }
                    html += '>' + todayNum + '</li>';
                }
                html += '</ul>';

                return html;
            };

            // 构建时间选择
            theTool.buildTimes = function () {
                const self = this;
                const splitHtml = '<i></i>';
                let html = `<section class='dc_time-group'>`;

                html += '<input type="number" name="hour" class="dc_hour" min="0" max="23" autocomplete="false"/>';
                //html += '<select name="hour" class="hour" >';

                //for (let i = 0; i < 24; i += self.cacheApi.settings.hourStep) {
                //    const optionValue = self.fillLeadZero(i, 2);
                //    html += '<option value="' + optionValue + '">' + optionValue + '</option>';
                //}
                //html += '</select>';
                html += splitHtml;
                html += '<input type="number" name="mint" class="dc_mint" min="0" max="59" autocomplete="false"/>';
                //html += '<select name="mint" class="mint" >';

                //for (let i = 0; i < 60; i += self.cacheApi.settings.minuteStep) {
                //    const optionValue = self.fillLeadZero(i, 2);
                //    html += '<option value="' + optionValue + '">' + optionValue + '</option>';
                //}
                //html += '</select>';
                html += splitHtml;
                html += '<input type="number" name="secs" class="dc_secs" min="0" max="59" autocomplete="false"/>';

                //html += '<select name="secs" class="secs">';

                //for (let i = 0; i < 60; i += self.cacheApi.settings.secondStep) {
                //    const optionValue = self.fillLeadZero(i, 2);
                //    html += '<option value="' + optionValue + '">' + optionValue + '</option>';
                //}
                //html += '</select>';
                html += '</section>';

                if (self.cacheApi.settings.mode === 'range') {
                    html += html;
                }
                self.dom.timeSet.innerHTML += html;
                self.dom.head.insertAdjacentElement('afterbegin', self.dom.timeSet);
                var hourEle = self.dom.timeSet.querySelector('.dc_hour');//时
                var mintEle = self.dom.timeSet.querySelector('.dc_mint');//分
                var secsEle = self.dom.timeSet.querySelector('.dc_secs');//秒
                if (hourEle) {
                    hourEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 23) {
                                    e.target.value = '23';
                                } else if (e.target.value < 0) {
                                    e.target.value = '0';
                                }
                                if (e.target.value.length > 1 && e.data) {
                                    if (e.target.value > 9) {
                                        e.target.value = parseInt(e.target.value);
                                    }
                                    if (mintEle) {
                                        mintEle.select();
                                        mintEle.focus();
                                    }
                                }
                                if (e.target.value.length < 2 && e.target.value < 10 && e.target.value != '0') {
                                    e.target.value = '0' + e.target.value;
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    hourEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                // var mintEle = self.dom.timeSet.querySelector('.mint');
                if (mintEle) {
                    mintEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {

                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 59) {
                                    e.target.value = '59';
                                } else if (e.target.value < 0) {
                                    e.target.value = '0';
                                }
                                if (e.target.value.length > 1 && e.data) {
                                    if (e.target.value > 9) {
                                        e.target.value = parseInt(e.target.value);
                                    }
                                    if (secsEle) {
                                        secsEle.select();
                                        secsEle.focus();
                                    }
                                }
                                if (e.target.value.length < 2 && e.target.value < 10 && e.target.value != '0') {
                                    e.target.value = '0' + e.target.value;
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    mintEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                // var secsEle = self.dom.timeSet.querySelector('.secs');
                if (secsEle) {
                    secsEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 59) {
                                    e.target.value = '59';
                                } else if (e.target.value < 0) {
                                    e.target.value = '0';
                                }
                                if (e.target.value < 10) {
                                    e.target.value = '0' + e.target.value;
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    secsEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                self.setDateValues();
                self.setTimesValues();
            };
            // 构建日期选择
            theTool.buildDate = function () {
                const self = this;
                const splitHtml = '<i></i>';



                let html = `<section class='dc_date-group'>`;

                html += '<input type="number" name="year" class="dc_year" min="1900" max="2300" autocomplete="false"/>';
                html += splitHtml;
                html += '<input type="number" name="month" class="dc_month" min="1" max="12" autocomplete="false"/>';
                html += splitHtml;
                html += '<input type="number" name="day" class="dc_day" min="1" max="31" autocomplete="false"/>';
                html += '</section>';


                if (self.cacheApi.settings.mode === 'range') {
                    html += html;
                }
                self.dom.timeSet.innerHTML = html;
                self.dom.head.insertAdjacentElement('afterbegin', self.dom.timeSet);
                var yearEle = self.dom.timeSet.querySelector('.dc_year');//年
                var monthEle = self.dom.timeSet.querySelector('.dc_month');//月
                var dayEle = self.dom.timeSet.querySelector('.dc_day');//日
                if (yearEle) {
                    yearEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                const currentTime = new Date();
                                e.target.value = currentTime.getFullYear();
                            } else {
                                if (e.target.value.length > 3 && e.data) {
                                    if (monthEle) {
                                        monthEle.select();
                                        monthEle.focus();
                                    }
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    yearEle.addEventListener('change', function (e) {
                        $("select.year").val(e.target.value);
                        self.gotoDate();
                        let selectDate = theTool.formatDate('Y-m-d', e.target.value + '-' + monthEle.value + '-' + dayEle.value);
                        self.locateDay(selectDate);
                    });
                    yearEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                if (monthEle) {
                    monthEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 12) {
                                    e.target.value = '12';
                                } else if (e.target.value < 0) {
                                    e.target.value = '1';
                                }

                                if ((e.target.value.length > 1 || (e.target.value > 1 && e.target.value < 10)) && e.data) {
                                    if (dayEle) {
                                        dayEle.select();
                                        dayEle.focus();
                                    }
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    monthEle.addEventListener('change', function (e) {
                        //e.target.value=self.fillLeadZero(e.target.value,2)
                        $("select.month").val(e.target.value);
                        self.gotoDate();
                        let selectDate = theTool.formatDate('Y-m-d', yearEle.value + '-' + e.target.value + '-' + dayEle.value);
                        self.locateDay(selectDate);
                    });
                    monthEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                if (dayEle) {
                    dayEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 31) {
                                    e.target.value = '31';
                                } else if (e.target.value < 0) {
                                    e.target.value = '1';
                                }
                                let selectDate = theTool.formatDate('Y-m-d', yearEle.value + '-' + monthEle.value + '-' + e.target.value)
                                    ;
                                self.locateDay(selectDate);
                            }
                        } else {
                            e.target.value = '1';
                        }
                    });
                    dayEle.addEventListener('change', function (e) {
                        e.target.value = self.fillLeadZero(e.target.value, 2);
                    });
                    dayEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                self.setDateValues();

            };
            // 构建日期时间选择
            theTool.buildDateTimes = function () {
                const self = this;
                const splitHtml = '<i></i>';



                let html = `<section class='dc_date-group'>`;

                html += '<input type="number" name="year" class="dc_year" min="1900" max="2300" autocomplete="false"/>';
                html += splitHtml;
                html += '<input type="number" name="month" class="dc_month" min="1" max="12" autocomplete="false"/>';
                html += splitHtml;
                html += '<input type="number" name="day" class="dc_day" min="1" max="31" autocomplete="false"/>';
                html += '</section>';

                html += `<section class='dc_time-group'>`;
                html += '<input type="number" name="hour" class="dc_hour" min="0" max="23" autocomplete="false"/>';
                html += splitHtml;
                html += '<input type="number" name="mint" class="dc_mint" min="0" max="59" autocomplete="false"/>';
                html += splitHtml;
                html += '<input type="number" name="secs" class="dc_secs" min="0" max="59" autocomplete="false"/>';
                html += '</section>';

                if (self.cacheApi.settings.mode === 'range') {
                    html += html;
                }
                self.dom.timeSet.innerHTML = html;
                self.dom.head.insertAdjacentElement('afterbegin', self.dom.timeSet);
                var yearEle = self.dom.timeSet.querySelector('.dc_year');//年
                var monthEle = self.dom.timeSet.querySelector('.dc_month');//月
                var dayEle = self.dom.timeSet.querySelector('.dc_day');//日

                var hourEle = self.dom.timeSet.querySelector('.dc_hour');//时
                var mintEle = self.dom.timeSet.querySelector('.dc_mint');//分
                var secsEle = self.dom.timeSet.querySelector('.dc_secs');//秒

                if (yearEle) {
                    yearEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                const currentTime = new Date();
                                e.target.value = currentTime.getFullYear();
                            } else {
                                if (e.target.value.length > 3 && e.data) {
                                    if (monthEle) {
                                        monthEle.select();
                                        monthEle.focus();
                                    }
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    yearEle.addEventListener('change', function (e) {
                        $("select.year").val(e.target.value);
                        self.gotoDate();
                        let selectDate = theTool.formatDate('Y-m-d', e.target.value + '-' + monthEle.value + '-' + dayEle.value);
                        self.locateDay(selectDate);
                    });

                    yearEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                if (monthEle) {
                    monthEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            //e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 12) {
                                    e.target.value = '12';
                                } else if (e.target.value < 0) {
                                    e.target.value = '1';
                                }

                                if ((e.target.value.length > 1 || (e.target.value > 1 && e.target.value < 10)) && e.data) {
                                    if (e.target.value == '00') {
                                        e.target.value = '01';
                                    }
                                    if (e.target.value > 9) {
                                        e.target.value = parseInt(e.target.value);
                                    }
                                    if (dayEle) {
                                        dayEle.select();
                                        dayEle.focus();
                                    }
                                }
                                if (e.target.value.length < 2 && e.target.value < 10 && e.target.value != '0') {
                                    e.target.value = '0' + e.target.value;
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    monthEle.addEventListener('change', function (e) {
                        //e.target.value=self.fillLeadZero(e.target.value,2)
                        $("select.month").val(e.target.value);
                        self.gotoDate();
                        let selectDate = theTool.formatDate('Y-m-d', yearEle.value + '-' + e.target.value + '-' + dayEle.value);
                        self.locateDay(selectDate);
                    });
                    monthEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                if (dayEle) {
                    dayEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            //e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 31) {
                                    e.target.value = '31';
                                } else if (e.target.value < 0) {
                                    e.target.value = '1';
                                }
                                let selectDate = theTool.formatDate('Y-m-d', yearEle.value + '-' + monthEle.value + '-' + e.target.value)
                                    ;
                                self.locateDay(selectDate);
                                if (e.target.value.length > 1 && e.data) {
                                    if (e.target.value == '00') {
                                        e.target.value = '01';
                                    }
                                    if (e.target.value > 9) {
                                        e.target.value = parseInt(e.target.value);
                                    }
                                    if (hourEle) {

                                        hourEle.select();
                                        hourEle.focus();
                                    }
                                }
                                if (e.target.value.length < 2 && e.target.value < 10 && e.target.value != '0') {
                                    e.target.value = '0' + e.target.value;
                                }
                            }
                        } else {
                            e.target.value = '1';
                        }
                    });
                    dayEle.addEventListener('change', function (e) {
                        e.target.value = self.fillLeadZero(e.target.value, 2);
                    });
                    dayEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }

                if (hourEle) {
                    hourEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            //e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 23) {
                                    e.target.value = '23';
                                } else if (e.target.value < 0) {
                                    e.target.value = '0';
                                }
                                if (e.target.value.length > 1 && e.data) {
                                    if (e.target.value > 9) {
                                        e.target.value = parseInt(e.target.value);
                                    }
                                    if (mintEle) {
                                        mintEle.select();
                                        mintEle.focus();
                                    }
                                }
                                if (e.target.value.length < 2 && e.target.value < 10 && e.target.value != '0') {
                                    e.target.value = '0' + e.target.value;
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    hourEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                // var mintEle = self.dom.timeSet.querySelector('.mint');
                if (mintEle) {
                    mintEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            console.log(e.target.value);
                            //e.target.value = parseInt(e.target.value);

                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 59) {
                                    e.target.value = '59';
                                } else if (e.target.value < 0) {
                                    e.target.value = '0';
                                }
                                if (e.target.value.length > 1 && e.data) {
                                    if (e.target.value > 9) {
                                        e.target.value = parseInt(e.target.value);
                                    }
                                    if (secsEle) {
                                        secsEle.select();
                                        secsEle.focus();
                                    }
                                }
                                if (e.target.value.length < 2 && e.target.value < 10 && e.target.value != '0') {
                                    e.target.value = '0' + e.target.value;
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    mintEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                // var secsEle = self.dom.timeSet.querySelector('.secs');
                if (secsEle) {
                    secsEle.addEventListener('input', function (e) {
                        if (e.target.value && e.target.value.length > 0) {
                            e.target.value = parseInt(e.target.value);
                            if (isNaN(e.target.value)) {
                                e.target.value = '0';
                            } else {
                                if (e.target.value > 59) {
                                    e.target.value = '59';
                                } else if (e.target.value < 0) {
                                    e.target.value = '0';
                                }
                                if (e.target.value < 10) {
                                    e.target.value = '0' + e.target.value;
                                }
                            }
                        } else {
                            e.target.value = '0';
                        }
                    });
                    secsEle.addEventListener('keydown', function (e) {
                        const key = e.keyCode || e.which;
                        if (key === 13) {
                            e.target.blur();
                            e.preventDefault();
                            self.confirmTime();
                        }
                    });
                }
                self.setDateValues();
                self.setTimesValues();
            };
            // 定位日期
            theTool.locateDay = function (dateValue) {
                const self = this;
                if (!dateValue) {
                    return;
                }
                for (let x of self.dom.dateSet.querySelectorAll('li')) {
                    x.classList.remove('DCcxCalendarSelected');
                    if (dateValue == theTool.formatDate('Y-m-d', x.dataset.date)) {
                        x.classList.add('DCcxCalendarSelected');
                    }
                }
                // if (self.cacheApi.settings.type === 'datetime') {
                //     self.cacheDate.time = self.parseDate(dateValue).getTime();
                //     return
                // }
                // self.cacheApi.setDate(dateValue);

                self.cacheDate.time = self.parseDate(dateValue).getTime();
            };
            // 构建翻页函数
            theTool.buildFlipFunc = function () {
                const self = this;
                var prevYearEle = self.dom.head.querySelector('.dc_prevYear');//上一年
                var prevMonthEle = self.dom.head.querySelector('.dc_prevMonth');//上一月
                var nextMonthEle = self.dom.head.querySelector('.dc_nextMonth');//下一月
                var nextYearEle = self.dom.head.querySelector('.dc_nextYear');//下一年

                if (prevYearEle) {
                    prevYearEle.addEventListener('click', function (e) {
                        self.gotoPrevYear();
                    });
                }
                if (prevMonthEle) {
                    prevMonthEle.addEventListener('click', function (e) {
                        self.gotoPrev();
                    });
                }
                if (nextMonthEle) {
                    nextMonthEle.addEventListener('click', function (e) {
                        self.gotoNext();
                    });
                }
                if (nextYearEle) {
                    nextYearEle.addEventListener('click', function (e) {
                        self.gotoNextYear();
                    });
                }
            };

            // 赋值时间选择
            theTool.setTimesValues = function () {
                const self = this;
                const values = [];

                if (self.cacheDate.startTime && self.cacheDate.endTime) {
                    values.push(self.cacheDate.startTime, self.cacheDate.endTime);
                } else if (self.cacheDate.startTime) {
                    values.push(self.cacheDate.startTime, self.cacheDate.startTime);
                } else {
                    values.push(self.cacheApi.defDate.time, self.cacheApi.defDate.time);
                }
                const times = {
                    hour: [],
                    mint: [],
                    secs: [],
                };

                if (self.cacheApi.settings.type === 'time') {
                    var dataInput = rootElement.ownerDocument.querySelector('#DCDateTime_calendar');
                    if (dataInput && dataInput.value) {
                        var d = new Date(dataInput.value);
                        times.hour.push(self.fillLeadZero(d.getHours(), 2));
                        times.mint.push(self.fillLeadZero(d.getMinutes(), 2));
                        times.secs.push(self.fillLeadZero(d.getSeconds(), 2));
                    }
                    console.log(times, '=============times');
                } else {
                    for (let x of values) {
                        const d = new Date(x);
                        times.hour.push(self.fillLeadZero(d.getHours(), 2));
                        times.mint.push(self.fillLeadZero(d.getMinutes(), 2));
                        times.secs.push(self.fillLeadZero(d.getSeconds(), 2));
                    }

                }

                for (let x of self.dom.timeSet.querySelectorAll('.dc_time-group input')) {
                    if (times[x.name] && times[x.name].length) {
                        x.value = times[x.name].shift();
                    }
                }

            };

            // 赋值日期选择框
            theTool.setDateValues = function (dataValue) {
                const self = this;
                const values = [];

                if (dataValue) {
                    values.push(dataValue);
                }
                else if (self.cacheDate.startTime && self.cacheDate.endTime) {
                    values.push(self.cacheDate.startTime, self.cacheDate.endTime);
                } else if (self.cacheDate.startTime) {
                    values.push(self.cacheDate.startTime, self.cacheDate.startTime);
                } else {
                    values.push(self.cacheApi.defDate.time, self.cacheApi.defDate.time);
                }
                const date = {
                    year: [],
                    month: [],
                    day: [],
                };

                for (let x of values) {
                    const d = new Date(x);
                    date.year.push(d.getFullYear());
                    date.month.push(self.fillLeadZero(d.getMonth() + 1, 2));
                    date.day.push(self.fillLeadZero(d.getDate(), 2));
                }
                for (let x of self.dom.timeSet.querySelectorAll('.dc_date-group input')) {
                    if (date[x.name] && date[x.name].length) {
                        x.value = date[x.name].shift();
                    }
                }
            };

            // 构建月份列表
            theTool.buildMonths = function (year) {
                const self = this;

                if (!self.isInteger(year)) {
                    return;
                }
                const nowDate = new Date();
                const nowText = [nowDate.getFullYear(), nowDate.getMonth() + 1].join('-');
                const selectedText = self.formatDate('Y-n', self.cacheDate.time);
                const maxInt = parseInt(self.cacheApi.maxDate.year + self.fillLeadZero(self.cacheApi.maxDate.month, 2), 10);
                const minInt = parseInt(self.cacheApi.minDate.year + self.fillLeadZero(self.cacheApi.minDate.month, 2), 10);

                // 日期范围值
                const rangeValue = {};
                let rangeMaxInt = 0;

                if (typeof self.cacheDate.startTime === 'number') {
                    rangeValue.start = parseInt(self.formatDate('Ym', self.cacheDate.startTime), 10);

                    if (typeof self.cacheApi.settings.rangeMaxMonth === 'number' && self.cacheApi.settings.rangeMaxMonth) {
                        const rangeDate = new Date(self.cacheDate.startTime);
                        rangeDate.setMonth(rangeDate.getMonth() + self.cacheApi.settings.rangeMaxMonth);
                        rangeMaxInt = parseInt(self.formatDate('Ym', rangeDate.getTime()), 10);
                    }
                    if (typeof self.cacheDate.endTime === 'number') {
                        rangeValue.end = parseInt(self.formatDate('Ym', self.cacheDate.endTime), 10);
                    } else {
                        rangeValue.end = rangeValue.start;
                    }
                }
                let html = '<ul>';

                for (let i = 1; i <= 12; i++) {
                    const classValue = [];
                    const todayText = year + '-' + i;
                    const todayInt = parseInt(year + self.fillLeadZero(i, 2), 10);

                    if (self.cacheApi.settings.mode === 'range') {
                        if (todayInt === rangeValue.start || todayInt === rangeValue.end || (todayInt >= rangeValue.start && todayInt <= rangeValue.end)) {
                            classValue.push('DCcxCalendarSelected');

                            if (todayInt === rangeValue.start) {
                                classValue.push('dc_start');
                            } if (todayInt === rangeValue.end) {
                                classValue.push('dc_end');
                            }
                        }
                    } else if (todayText === selectedText) {
                        classValue.push('DCcxCalendarSelected');
                    }
                    if (todayText === nowText) {
                        classValue.push('dc_now');
                    }
                    if (rangeMaxInt && todayInt > rangeMaxInt) {
                        classValue.push('dc_del');

                    } else if (todayInt < minInt || todayInt > maxInt) {
                        classValue.push('dc_del');
                    }
                    html += '<li';

                    if (classValue.length) {
                        html += ' class="' + classValue.join(' ') + '"';
                    }
                    if (classValue.indexOf('dc_del') === -1) {
                        html += ' data-date="' + todayText + '"';
                    }
                    html += '>' + self.cacheApi.settings.language.monthList[i - 1] + '</li>';
                }
                html += '</ul>';

                return html;
            };

            // 构建年份列表
            theTool.buildYears = function (year) {
                const self = this;
                let start = self.cacheApi.minDate.year;
                let end;
                let diff;

                if (!self.isInteger(year)) {
                    return;
                }
                const nowDate = new Date();
                const nowYear = nowDate.getFullYear();
                const selectedText = parseInt(self.formatDate('Y', self.cacheDate.time), 10);

                if (year < self.cacheApi.minDate.year) {
                    start = self.cacheApi.minDate.year;
                }
                start = Math.floor(start / 10) * 10;
                diff = year - start;

                if (diff >= self.cacheApi.settings.yearNum) {
                    start += Math.floor(diff / self.cacheApi.settings.yearNum) * self.cacheApi.settings.yearNum;
                }
                end = start + self.cacheApi.settings.yearNum - 1;

                // if (end > self.cacheApi.maxDate.year) {
                //   end = self.cacheApi.maxDate.year;
                // };

                // 日期范围值
                const rangeValue = {};
                let rangeMaxYear = 0;

                if (typeof self.cacheDate.startTime === 'number') {
                    rangeValue.start = parseInt(self.formatDate('Y', self.cacheDate.startTime), 10);

                    if (typeof self.cacheApi.settings.rangeMaxYear === 'number' && self.cacheApi.settings.rangeMaxYear) {
                        rangeMaxYear = rangeValue.start + self.cacheApi.settings.rangeMaxYear;
                    }
                    if (typeof self.cacheDate.endTime === 'number') {
                        rangeValue.end = parseInt(self.formatDate('Y', self.cacheDate.endTime), 10);
                    } else {
                        rangeValue.end = rangeValue.start;
                    }
                }
                let html = '<ul>';

                for (let i = start; i <= end; i++) {
                    const classValue = [];

                    if (self.cacheApi.settings.mode === 'range') {
                        if (i === rangeValue.start || i === rangeValue.end || (i >= rangeValue.start && i <= rangeValue.end)) {
                            classValue.push('DCcxCalendarSelected');

                            if (i === rangeValue.start) {
                                classValue.push('dc_start');
                            } if (i === rangeValue.end) {
                                classValue.push('dc_end');
                            }
                        }
                    } else if (i === selectedText) {
                        classValue.push('DCcxCalendarSelected');
                    }
                    if (i === nowYear) {
                        classValue.push('dc_now');
                    }
                    if (rangeMaxYear && i > rangeMaxYear) {
                        classValue.push('dc_del');

                    } else if (i < self.cacheApi.minDate.year || i > self.cacheApi.maxDate.year) {
                        classValue.push('dc_del');
                    }
                    html += '<li';

                    if (classValue.length) {
                        html += ' class="' + classValue.join(' ') + '"';
                    }
                    if (classValue.indexOf('dc_del') === -1) {
                        html += ' data-date="' + i + '"';
                    }
                    html += '>' + i + '</li>';
                }
                html += '</ul>';

                return html;
            };

            // 跳转到日期
            theTool.gotoDate = function (value) {
                const self = this;
                const values = {};
                const selects = self.getSelects(['year', 'month'], values);

                if (value === undefined) {
                    value = values.year;

                    if (values.month) {
                        value += '-' + values.month;
                    }
                }
                const theDate = self.parseDate(value, true);
                const theTime = theDate.getTime();

                if (theTime < self.cacheApi.minDate.time) {
                    theDate.setTime(self.cacheApi.minDate.time);
                } else if (theTime > self.cacheApi.maxDate.time) {
                    theDate.setTime(self.cacheApi.maxDate.time);
                }
                let theYear = theDate.getFullYear();
                let theMonth = theDate.getMonth() + 1;

                if (self.cacheApi.settings.type === 'year') {
                    let startYear = theYear;

                    for (let x of selects.year.options) {
                        const val = parseInt(x.value, 10);

                        if (val <= theYear) {
                            startYear = val;
                        } else {
                            break;
                        }
                    }
                    theYear = startYear;

                    if (startYear !== values.year) {
                        selects.year.value = startYear;
                    }
                } else if (theYear !== values.year) {
                    selects.year.value = theYear;
                }
                if (selects.month) {
                    if (theYear !== values.year || theMonth !== values.month) {
                        self.rebuildMonthSelect();
                        selects.month.value = theMonth;
                    }
                }
                const atState = {
                    start: true,
                    end: true,
                };

                for (let x in selects) {
                    if (selects[x].selectedIndex !== 0) {
                        atState.start = false;
                    } if (selects[x].selectedIndex !== selects[x].length - 1) {
                        atState.end = false;
                    }
                }
                for (let x in atState) {
                    if (atState[x]) {
                        self.dom.panel.classList.add('dc_at_' + x);
                    } else if (self.dom.panel.classList.contains('dc_at_' + x)) {
                        self.dom.panel.classList.remove('dc_at_' + x);
                    }
                }
                let html = '';
                let fillHtml = '';

                switch (self.cacheApi.settings.type) {
                    case 'date':
                    case 'datetime':
                        html = self.buildDays(theYear, theMonth);

                        if (self.cacheApi.settings.mode === 'range') {
                            let fillMonth = theMonth + 1;

                            if (fillMonth > 12) {
                                fillMonth = 1;
                            }
                            fillHtml = '<span class="dc_year">' + theYear + '</span><em></em>';
                            fillHtml += '<span class="dc_month">' + self.cacheApi.settings.language.monthList[fillMonth - 1] + '</span><em></em>';
                            html += self.buildDays(theYear, theMonth + 1);
                        } break;

                    case 'month':
                        html = self.buildMonths(theYear);

                        if (self.cacheApi.settings.mode === 'range') {
                            fillHtml = '<span class="dc_year">' + (theYear + 1) + '</span><em></em>';
                            html += self.buildMonths(theYear + 1);
                        } break;

                    case 'year':
                        html = self.buildYears(theYear);

                        if (self.cacheApi.settings.mode === 'range') {
                            fillHtml = '<span class="dc_year">' + (theYear + self.cacheApi.settings.yearNum) + ' - ' + (theYear + self.cacheApi.settings.yearNum * 2 - 1) + '</span>';
                            html += self.buildYears(theYear + self.cacheApi.settings.yearNum);
                        } break;
                }
                self.dom.dateSet.innerHTML = html;

                if (self.cacheApi.settings.mode === 'range') {
                    const el = self.dom.head.querySelectorAll('section');

                    if (el.length > 1) {
                        el[1].innerHTML = fillHtml;
                    }
                }
            };

            // 向前翻页（月）
            theTool.gotoPrev = function () {
                const self = this;
                const selects = self.getSelects(['year', 'month']);

                switch (self.cacheApi.settings.type) {
                    case 'date':
                    case 'datetime':
                        const monthIndex = selects.month.selectedIndex;

                        if (monthIndex > 0) {
                            selects.month.selectedIndex -= 1;
                            self.gotoDate();

                        } else if (monthIndex === 0) {
                            if (selects.year.selectedIndex > 0) {
                                selects.year.selectedIndex -= 1;

                                self.rebuildMonthSelect();
                                selects.month.selectedIndex = selects.month.length - 1;
                                self.gotoDate();
                            }
                        } break;

                    case 'month':
                    case 'year':
                        if (selects.year.selectedIndex > 0) {
                            selects.year.selectedIndex -= 1;
                            self.gotoDate();
                        } break;
                }
            };

            // 向后翻页（月）
            theTool.gotoNext = function () {
                const self = this;
                const selects = self.getSelects(['year', 'month']);

                switch (self.cacheApi.settings.type) {
                    case 'date':
                    case 'datetime':
                        const monthIndex = selects.month.selectedIndex;
                        const monthMax = selects.month.length - 1;

                        if (monthIndex < monthMax) {
                            selects.month.selectedIndex += 1;
                            self.gotoDate();

                        } else if (monthIndex === monthMax) {
                            if (selects.year.selectedIndex < selects.year.length - 1) {
                                selects.year.selectedIndex += 1;

                                self.rebuildMonthSelect();
                                selects.month.selectedIndex = 0;
                                self.gotoDate();
                            }
                        } break;

                    case 'month':
                    case 'year':
                        if (selects.year.selectedIndex < selects.year.length - 1) {
                            selects.year.selectedIndex += 1;
                            self.gotoDate();
                        } break;
                }
            };

            // 向前翻页（年）
            theTool.gotoPrevYear = function () {
                const self = this;
                const selects = self.getSelects(['year', 'month']);

                switch (self.cacheApi.settings.type) {
                    case 'date':
                    case 'datetime':
                        const yearIndex = selects.year.selectedIndex;

                        if (yearIndex > 0) {
                            selects.year.selectedIndex -= 1;
                            self.gotoDate();
                        } break;

                    case 'month':
                    case 'year':
                        if (selects.year.selectedIndex > 0) {
                            selects.year.selectedIndex -= 1;
                            self.gotoDate();
                        } break;
                }
            };

            // 向后翻页（年）
            theTool.gotoNextYear = function () {
                const self = this;
                const selects = self.getSelects(['year', 'month']);

                switch (self.cacheApi.settings.type) {
                    case 'date':
                    case 'datetime':
                        const yearIndex = selects.year.selectedIndex;

                        if (yearIndex > 0) {
                            selects.year.selectedIndex += 1;
                            self.gotoDate();
                        }
                        break;

                    case 'month':
                    case 'year':
                        if (selects.year.selectedIndex > 0) {
                            selects.year.selectedIndex += 1;
                            self.gotoDate();
                        } break;
                }
            };

            // 显示面板
            theTool.showPanel = function (rootElement) {
                const self = this;

                if (self.delayHide) {
                    clearTimeout(self.delayHide);
                }
                const pos = self.cacheApi.settings.position;

                const winWidth = rootElement.ownerDocument.defaultView.innerWidth || rootElement.ownerDocument.documentElement.clientWidth || rootElement.ownerDocument.body.clientWidth;
                const winHeight = rootElement.ownerDocument.defaultView.innerHeight || rootElement.ownerDocument.documentElement.clientHeight || rootElement.ownerDocument.body.clientHeight;
                const elRect = self.cacheApi.input.getBoundingClientRect();
                const elWidth = elRect.width;
                const elHeight = elRect.height;
                const elClientTop = elRect.top;
                const elClientLeft = elRect.left;
                const elTop = elClientTop + rootElement.ownerDocument.defaultView.pageYOffset - rootElement.ownerDocument.documentElement.clientTop;
                const elLeft = elClientLeft + rootElement.ownerDocument.defaultView.pageXOffset - rootElement.ownerDocument.documentElement.clientLeft;

                const panelRect = self.dom.panel.getBoundingClientRect();
                const panelWidth = panelRect.width;
                const panelHeight = panelRect.height;
                let panelTop = elTop + elHeight - 20;
                // let panelTop = (elClientTop + elHeight + panelHeight > winHeight && elTop - panelHeight >= 0) ? elTop - panelHeight : elTop + elHeight - 20;
                //获取到页面的焦点的位置
                let caretEle = WriterControl_DateTimeControl.rootElement.querySelector('#divCaret20221213');
                if (caretEle) {
                    //如果焦点距离顶部比日期框距离顶部远 说明 日期框弹出在上面
                    let caretEleRect = caretEle.getBoundingClientRect();
                    if (caretEleRect.top > elClientTop) {
                        //去掉一个间距
                        panelTop -= 10;

                        //日期格式下，divContainer的高度有变化，所以先在此处调整位置
                        var CurrentElement = rootElement.GetElementProperties(rootElement.CurrentElement());
                        if (CurrentElement && CurrentElement.InnerEditStyle && CurrentElement.InnerEditStyle == "Date") {
                            panelTop -= 35;
                        }

                    }



                }
                let panelLeft = (elClientLeft + panelWidth > winWidth && elLeft - panelWidth >= 0) ? elLeft - panelWidth + elWidth : elLeft;

                if (typeof pos === 'string' && pos.length) {
                    switch (pos) {
                        case 'fixed':
                            panelTop = null;
                            panelLeft = null;
                            break;

                        case 'top':
                            panelTop = elTop - panelHeight;
                            break;

                        case 'bottom':
                            panelTop = elTop + elHeight;
                            break;

                        case 'left':
                        case 'right':
                            panelTop = ((elClientTop + elHeight + panelHeight) > winHeight) ? elTop + elHeight - panelHeight : elTop;
                            panelLeft = (pos === 'left') ? elLeft - panelWidth : elLeft + elWidth;
                            break;
                    }
                }

                if (typeof panelTop === 'number' && typeof panelLeft === 'number') {
                    self.dom.panel.style.top = panelTop + 20 + 'px';
                    self.dom.panel.style.left = panelLeft + 'px';
                }
                self.dom.panel.classList.add('dc_show');
            };

            // 隐藏面板
            theTool.hidePanel = function () {
                const self = this;
                self.dom.panel.classList.remove('dc_show');
                let selects = self.dom.head.querySelectorAll('select');
                if (selects.length > 0) {
                    for (let i = 0; i < selects.length; i++) {
                        let s = selects[i];
                        while (s.options.length > 0) {
                            s.remove(0);
                        }
                    }
                }
                self.delayHide = setTimeout(() => {
                    self.dom.panel.removeAttribute('style');
                }, 300);
            };

            // 确认选择日期范围
            theTool.confirmRange = function () {
                const self = this;
                let values = [];

                if (self.cacheDate.startTime && self.cacheDate.endTime) {
                    values.push(self.cacheDate.startTime, self.cacheDate.endTime);
                } else if (self.cacheDate.startTime) {
                    values.push(self.cacheDate.startTime, self.cacheDate.startTime);
                } else {
                    values.push(self.cacheApi.defDate.time, self.cacheApi.defDate.time);
                }
                if (['datetime', 'time'].indexOf(self.cacheApi.settings.type) >= 0) {
                    const times = {
                        hour: [],
                        mint: [],
                        secs: [],
                    };

                    for (let x of self.dom.timeSet.querySelectorAll('input')) {
                        if (times[x.name]) {
                            times[x.name].push(parseInt(x.value));
                        }
                    }
                    // 日期对比时间顺序
                    if (self.cacheApi.settings.type === 'datetime') {
                        const diffTime = [];

                        for (let i = 0, l = values.length; i < l; i++) {
                            diffTime.push(parseInt([1, times.hour[i], times.mint[i], times.secs[i]].join(''), 10));
                        }
                        if (diffTime[0] > diffTime[1]) {
                            for (let x in times) {
                                if (times[x].length > 1) {
                                    times[x][1] = times[x][0];
                                }
                            }
                        }
                    }
                    values = values.map((val, index) => {
                        const d = new Date(val);
                        d.setHours(times.hour[index], times.mint[index], times.secs[index], 0);
                        return d.getTime();
                    });
                }
                self.cacheApi.setDate(values.join(self.cacheApi.settings.rangeSymbol));
            };

            // 确认选择
            theTool.confirmTime = function () {
                const self = this;
                const theDate = new Date();
                let theTime = self.cacheApi.defDate.time;
                // console.log('defDate.time',self.cacheApi.defDate.time);
                // if (self.cacheApi.settings.type === 'datetime' && typeof self.cacheDate.time === 'number') {
                //     theTime = self.cacheDate.time;
                // }
                // console.log('self.cacheDate.time',self.cacheDate.time);
                theTime = self.cacheDate.time;
                theDate.setTime(theTime);

                // 时分秒
                const times = Array(4).fill(0);
                const map = {
                    hour: 0,
                    mint: 1,
                    secs: 2,
                };

                for (let x of self.dom.timeSet.querySelectorAll('input[type=number]')) {
                    if (x.name in map) {
                        times[map[x.name]] = parseInt(x.value ? x.value : '00');
                    }
                }
                theDate.setHours(...times);

                self.cacheApi.setDate(theDate.getTime());
            };

            // 解除绑定
            theTool.detach = function (el) {
                const self = this;

                if (!self.isElement(el)) {
                    return;
                }
                const alias = 'id_' + el.dataset.cxCalendarId;
                delete el.dataset.cxCalendarId;

                if (typeof self.bindFuns[alias] === 'function') {
                    el.removeEventListener('click', self.bindFuns[alias]);
                    delete self.bindFuns[alias];
                }
            };

            // document.addEventListener('DOMContentLoaded', () => {
            //   theTool.init();
            // });
            theTool.init();

            // 选择器实例
            const picker = function () {
                const self = this;
                let options = {};
                let isAttach = false;

                // 分配参数
                for (let x of arguments) {
                    if (theTool.isElement(x)) {
                        self.input = x;
                    } else if (theTool.isObject(x)) {
                        options = x;
                    } else if (typeof x === 'boolean') {
                        isAttach = x;
                    }
                }
                if (!self.input || !self.input.nodeName || ['input', 'textarea'].indexOf(self.input.nodeName.toLowerCase()) === -1) {
                    console.warn('[cxCalendar] Not input element.');
                    return;
                }
                // 合并输入框参数
                const keys = [
                    'baseClass',
                    'disableWeek',
                    'disableDay',
                    'endDate',
                    'format',
                    'hourStep',
                    'language',
                    'lockRow',
                    'minuteStep',
                    'position',
                    'mode',
                    'rangeSymbol',
                    'rangeMaxDay',
                    'rangeMaxMonth',
                    'rangeMaxYear',
                    'secondStep',
                    'startDate',
                    'type',
                    'weekStart',
                    'yearNum',
                ];
                const inputSettings = {};

                for (let x of keys) {
                    if (typeof self.input.dataset[x] !== 'string' || !self.input.dataset[x].length) {
                        continue;
                    }
                    switch (x) {
                        case 'hourStep':
                        case 'minuteStep':
                        case 'secondStep':
                        case 'rangeMaxDay':
                        case 'rangeMaxMonth':
                        case 'rangeMaxYear':
                        case 'weekStart':
                        case 'yearNum':
                            inputSettings[x] = parseInt(self.input.dataset[x], 10);
                            break;

                        case 'lockRow':
                            inputSettings[x] = Boolean(parseInt(self.input.dataset[x], 10));
                            break;

                        case 'disableWeek':
                        case 'disableDay':
                            inputSettings[x] = self.input.dataset[x].split(',');
                            break;

                        default:
                            inputSettings[x] = self.input.dataset[x];
                            break;
                    }
                }
                if (Array.isArray(inputSettings.disableWeek)) {
                    inputSettings.disableWeek = inputSettings.disableWeek.map((val) => {
                        return parseInt(val, 10);
                    });
                }
                self.settings = theTool.extend({}, options, inputSettings);
                self.setOptions();

                let alias = 'id_' + self.input.dataset.cxCalendarId;

                if (typeof theTool.bindFuns[alias] === 'function') {
                    theTool.detach(self.input);
                }
                self.eventChange = new CustomEvent('change', {
                    bubbles: true
                });

                if (isAttach) {
                    self.input.dataset.cxCalendarId = theTool.cxId;
                    alias = 'id_' + theTool.cxId;
                    theTool.cxId++;
                    theTool.bindFuns[alias] = self.show.bind(self);

                    self.input.addEventListener('click', theTool.bindFuns[alias]);

                } else {
                    self.show();
                }
            };

            picker.prototype.setOptions = function (options) {
                const self = this;
                let maxDate;
                let minDate;
                let defDate;

                if (theTool.isObject(options)) {
                    theTool.extend(self.settings, options);
                }
                // 结束日期（默认为当前日期）
                if (theTool.reg.isYear.test(self.settings.endDate)) {
                    maxDate = new Date(self.settings.endDate, 11, 31);
                } else {
                    maxDate = theTool.parseDate(self.settings.endDate, true);
                }
                maxDate.setHours(23, 59, 59);

                self.maxDate = {
                    year: maxDate.getFullYear(),
                    month: maxDate.getMonth() + 1,
                    day: maxDate.getDate(),
                    time: maxDate.getTime()
                };

                // 起始日期（默认为当前日期的前一年）
                if (theTool.reg.isYear.test(self.settings.startDate)) {
                    minDate = new Date(self.settings.startDate, 0, 1);
                } else {
                    minDate = theTool.parseDate(self.settings.startDate);
                }
                if (!theTool.isDate(minDate)) {
                    minDate = new Date();
                    minDate.setFullYear(minDate.getFullYear() - 1);
                }
                // 若超过结束日期，则设为结束日期的前一年
                if (minDate.getTime() > self.maxDate.time) {
                    minDate = new Date(self.maxDate.year - 1, self.maxDate.month - 1, self.maxDate.day);
                }
                minDate.setHours(0, 0, 0, 0);

                self.minDate = {
                    year: minDate.getFullYear(),
                    month: minDate.getMonth() + 1,
                    day: minDate.getDate(),
                    time: minDate.getTime()
                };

                const rangeValue = {};

                // 默认日期
                if (self.settings.mode === 'range') {
                    const dateSp = String(self.settings.date).split(self.settings.rangeSymbol);

                    if (dateSp.length === 2) {
                        defDate = theTool.parseDate(dateSp[0], true);

                        const rangeEndDate = theTool.parseDate(dateSp[1], true);
                        rangeValue.start = defDate.getTime();
                        rangeValue.end = rangeEndDate.getTime();
                    }
                }
                if (!defDate) {
                    defDate = theTool.parseDate(self.settings.date, true);
                }
                if (defDate.getTime() < self.minDate.time) {
                    defDate = theTool.parseDate(self.minDate.time, true);
                } else if (defDate.getTime() > self.maxDate.time) {
                    defDate = theTool.parseDate(self.maxDate.time, true);
                }
                self.defDate = {
                    year: defDate.getFullYear(),
                    month: defDate.getMonth() + 1,
                    day: defDate.getDate(),
                    hour: defDate.getHours(),
                    mint: defDate.getMinutes(),
                    secs: defDate.getSeconds(),
                    time: defDate.getTime(),
                    start: rangeValue.start,
                    end: rangeValue.end,
                };

                // 星期的起始位置
                self.settings.weekStart %= 7;

                // 语言配置
                self.settings.language = theTool.getLanguage(self.settings.language);

                // 统计节假日
                if (Array.isArray(self.settings.language.holiday) && self.settings.language.holiday.length) {
                    self.holiday = {};

                    for (let x of self.settings.language.holiday) {
                        self.holiday[x.day] = x.name;
                    }
                }
            };

            picker.prototype.show = function (rootElement) {
                const self = this;

                if (self.input.value || !self.settings.date) {
                    self.settings.date = self.input.value;
                } self.setOptions();

                theTool.cacheApi = self;
                theTool.buildPanel();
                theTool.showPanel(rootElement);
            };

            picker.prototype.hide = function () {
                theTool.hidePanel();
            };

            picker.prototype.getDate = function (style) {
                const self = this;
                const oldValue = self.input.value;
                const dateList = [];

                if (typeof style !== 'string' || !style.length) {
                    style = self.settings.format;
                }
                if (self.settings.mode === 'range') {
                    const dateSp = String(oldValue).split(self.settings.rangeSymbol);

                    if (dateSp.length === 2) {
                        dateList.push(...dateSp);
                    }
                } else {
                    dateList.push(oldValue);
                }
                let newValue = [];

                for (let x of dateList) {
                    const theDate = theTool.parseDate(x);

                    if (!theTool.isDate(theDate)) {
                        newValue = [];
                        break;
                    }
                    newValue.push(theTool.formatDate(style, theDate.getTime(), self.settings.language));
                }
                newValue = self.settings.mode === 'range' ? newValue.join(self.settings.rangeSymbol) : newValue.join('');

                return newValue;
            };

            picker.prototype.setDate = function (value) {
                // console.log(value);
                const self = this;
                const oldValue = self.input.value;
                const dateList = [];

                if (self.settings.mode === 'range') {
                    const dateSp = String(value).split(self.settings.rangeSymbol);

                    if (dateSp.length === 2) {
                        dateList.push(...dateSp);
                    }
                } else {
                    dateList.push(value);
                }
                let newValue = [];

                for (let x of dateList) {
                    const theDate = theTool.parseDate(x);

                    if (!theTool.isDate(theDate)) {
                        newValue = [];
                        break;
                    }
                    let theTime = theDate.getTime();

                    if (theTime < self.minDate.time) {
                        theTime = self.minDate.time;
                    } else if (theTime > self.maxDate.time) {
                        theTime = self.maxDate.time;
                    }
                    newValue.push(theTool.formatDate(self.settings.format, theTime, self.settings.language));
                }
                newValue = self.settings.mode === 'range' ? newValue.join(self.settings.rangeSymbol) : newValue.join('');

                //if (oldValue !== newValue) {

                //}
                self.input.value = newValue;
                self.input.dispatchEvent(self.eventChange);
            };

            picker.prototype.clearDate = function () {
                const self = this;
                const oldValue = self.input.value;

                if (oldValue && oldValue.length) {
                    self.input.value = '';
                    self.input.dispatchEvent(self.eventChange);
                }
            };

            const cxCalendar = function (el, options, isAttach) {
                if (theTool.isObject(options)) {
                    options = theTool.extend({}, cxCalendar.defaults, options);
                } else {
                    options = theTool.extend({}, cxCalendar.defaults);
                }
                const result = new picker(el, options, isAttach);

                if (isAttach) {
                    result.theTool = theTool;
                    return result;
                }
            };

            cxCalendar.attach = function (el, options) {
                return this(el, options, true);
            };

            cxCalendar.detach = function (el) {
                theTool.detach(el);
            };

            // 默认配置
            cxCalendar.defaults = {
                startDate: undefined,   // 开始日期
                endDate: undefined,     // 结束日期
                date: undefined,        // 默认日期
                type: 'date',           // 日期类型
                format: 'Y-m-d',        // 日期值格式
                weekStart: 0,           // 星期开始于周几
                lockRow: false,         // 固定日期的行数
                yearNum: 20,            // 年份每页条数
                hourStep: 1,            // 小时间隔
                minuteStep: 1,          // 分钟间隔
                secondStep: 1,          // 秒间隔
                disableWeek: [],        // 不可选择的日期（星期值）
                disableDay: [],         // 不可选择的日期
                mode: 'single',         // 选择模式
                rangeSymbol: ' - ',     // 日期范围拼接符号
                rangeMaxDay: 0,         // 日期范围最长间隔
                rangeMaxMonth: 0,       // 月份范围最长间隔
                rangeMaxYear: 0,        // 年份范围最长间隔
                button: {},             // 操作按钮
                position: undefined,    // 面板位置
                baseClass: undefined,   // 基础样式
                language: undefined     // 语言配置
            };

            return cxCalendar;

        }));
    }
}



