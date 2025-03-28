/**
 * author：xyzsyx@163.com 在使用中遇到问题或发现bug更或者有技术交流爱好的朋友请发邮件到我的email，大家一起学习，一起进步！
 *
 *
 * 初始化调用方法  在js的onload事件或jq的$(document).ready()里面调用stepBar.init(id, option)即可。
 * 第一个参数为stepBar容器的id，必填，允许传入的值包括如下：
 *     jQuery对象
 *     javascript对象
 *     DOM元素（可转化为ID的字符串，如 “stepBar” || “#stepBar”） 纠错：误把jQuery对象的“#”写成“.”也同样能识别出来，但是必须保证次参数能转化成元素ID
 * 第二个参数为一个对象直接量，选填，包含如下的零个或多个
 *     step                string number   目标进度  默认为1（第一步），选填
 *     change              boolean    设置插件是否可被操作，选填  默认false
 *     animation           boolean    设置插件是否采用动画形式（前提stepBar.change为true），选填  默认false
 *     speed               number     动画速度（前提，change和animation为true） 选填   默认1000ms
 *     stepEasingForward   string     从当前步数往前过渡动画（前提，change和animation为true） 选填  默认 "easeOutExpo"  更多参数请参照 jquery.easing.js
 *     stepEasingBackward  string     从当前步数往后过渡动画（前提，change和animation为true） 选填  默认 "easeOutElastic"  更多参数请参照 jquery.easing.js
 *
 *     PS：不合法的参数将强行使用默认值
 */

var stepBar = {
    bar : {},
    item : {},
    barWidth : 0,
    itemCount : 2,
    itemWidth : 0,
    processWidth : 0,
    curProcessWidth : 0,
    step : 1,
    curStep : 0,
    triggerStep : 1,
    change : false,
    animation : false,
    speed : 1000,
    stepEasingForward : "easeOutCubic",
    stepEasingBackward: "easeOutElastic",
    checkStepFunc: null,    //验证step是否可以点击
    itemClickFunc: null,
    
    init : function(id, option){
        if (typeof id == "object" || id.indexOf("#") == 0) {
            this.bar = $(id);
        } else {
            if (id.indexOf(".") == 0) {
                id = id.substring(1, id.length);
            }
            this.bar = $("#" + id);
        }
        this.checkStepFunc = option.checkStepFunc;
        this.itemClickFunc = option.itemClickFunc;
        this.change = option.change ? true : false;
        this.animation = this.change && option.animation ? true : false;
        this.layout();
        this.item = this.bar.find(".ui-stepInfo");
        if (this.item.length < 2) {
            return;
        }
        this.bar.show();
        this.itemCount = this.item.length;
        this.step = !isNaN(option.step) && option.step <= this.itemCount && option.step > 0 ? option.step : 1;
        this.triggerStep = this.step;
        if (!isNaN(option.speed) && option.speed > 0) {
            this.speed = parseInt(option.speed);
        }
        this.stepEasing(option.stepEasingForward, false);
        this.stepEasing(option.stepEasingBackward, true);
        this.firstStepFun();
    },
    stepEasing : function(stepEasing, backward){
        if(typeof jQuery.easing[stepEasing] === "function"){
            if(backward){
                this.stepEasingBackward = stepEasing;
            } else {
                this.stepEasingForward = stepEasing;
            }
        }
    },
    layout : function(){
        this.bar.find(".ui-stepInfo .ui-stepSequence").addClass("judge-stepSequence-hind");
        this.bar.find(".ui-stepInfo:first-child .ui-stepSequence").addClass("judge-stepSequence-pre");
    },
    classHover : function(){
        if(this.change){
            this.bar.find(".ui-stepInfo .judge-stepSequence-pre").removeClass("judge-stepSequence-hind-change").addClass("judge-stepSequence-pre-change");
            this.bar.find(".ui-stepInfo .judge-stepSequence-hind").removeClass("judge-stepSequence-pre-change").addClass("judge-stepSequence-hind-change");
        }
    },
    firstStepFun: function () {
        if(this.itemCount > 0){
            this.barWidth = this.bar.width();
            this.itemWidth = Math.floor((this.barWidth * 0.9) / (this.itemCount - 1));
            this.bar.find(".ui-stepLayout").width(Math.floor(this.barWidth * 0.9 + this.itemWidth));
            this.item.width(this.itemWidth);
            this.bar.find(".ui-stepLayout").css({"margin-left": -Math.floor(this.itemWidth / 2) + 10 });
            if(this.change){
                this._event();
            }
            this.percent();

            if (this.triggerStep && this.itemClickFunc && (typeof this.itemClickFunc === 'function')) {
                this.itemClickFunc(this.triggerStep);
            }
        }
    },
    _event : function(){
        var _this = this;
        _this.bar.on("click", ".ui-stepSequence", function () {
            var preStep = _this.curStep;
            var triggerStep = $(this).text();
            if (_this.checkStepFunc && (typeof _this.checkStepFunc === 'function') && !_this.checkStepFunc(triggerStep)) {
                return;
            }
            if(!isNaN(parseInt(triggerStep)) && triggerStep > 0 && triggerStep <= _this.itemCount && triggerStep != _this.curStep){
                _this.triggerStep = triggerStep;
                
                _this.percent();

                if (_this.itemClickFunc && (typeof _this.itemClickFunc === 'function')) {
                    _this.itemClickFunc(triggerStep, preStep);
                }
            }
        });
    },
    percent : function(){
        var _this = this;
        var calc = 100 / (_this.itemCount - 1);
        _this.processWidth = calc * (_this.triggerStep - 1) + "%";
        if(_this.animation){
            if(_this.triggerStep < _this.curStep){
                 _this._animate();
                 _this.curStep--;
            } else {
                 _this.curStep++;
            }
            _this.curProcessWidth = calc * (_this.curStep - 1) + "%";
            _this.bar.find(".ui-stepProcess").stop(true).animate({"width" : _this.curProcessWidth}, _this.speed, function(){
                _this._animate();
                if(_this.processWidth != _this.curProcessWidth){
                    _this.percent();
                }
            });
        } else {
            if(_this.curProcessWidth != _this.processWidth){
                _this.curProcessWidth = _this.processWidth;
                _this.bar.find(".ui-stepProcess").width(_this.processWidth);
                _this.jump();
            }
        }
    },
    jump : function(){
        this.bar.find(".ui-stepInfo .ui-stepSequence").removeClass("judge-stepSequence-pre").addClass("judge-stepSequence-hind");
        this.bar.find(".ui-stepInfo:nth-child(-n+" + this.triggerStep + ") .ui-stepSequence").removeClass("judge-stepSequence-hind").addClass("judge-stepSequence-pre");
        this.classHover();
    },
    _animate : function(){
        var stepSequence_size = {},
            easing = this.stepEasingBackward,
            preClass,
            hindClass;
        if(this.triggerStep < this.curStep){
            stepSequence_size.padding = "6px 10px";
            preClass = "judge-stepSequence-pre";
            hindClass = "judge-stepSequence-hind";
            easing = this.stepEasingForward;
        } else {
            stepSequence_size.padding = "8px 12px";
            preClass = "judge-stepSequence-hind";
            hindClass = "judge-stepSequence-pre";
        }
        this.bar.find(".ui-stepInfo:nth-child(" + this.curStep + ") .ui-stepSequence").removeClass(preClass).addClass(hindClass);
        this.bar.find(".ui-stepInfo:nth-child(" + this.curStep + ") .ui-stepSequence").animate(stepSequence_size, 500, easing);
        this.classHover();
    }
};