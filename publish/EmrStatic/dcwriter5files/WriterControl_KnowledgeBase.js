/**
 * 20240103lxy:新增WriterControl_KnowledgeBase.js文件，用于开发知识库功能
 * */
import { DCTools20221228 } from "./DCTools20221228.js";

("use strict");
export let WriterControl_KnowledgeBase = {
    KnowledgeBaseOptions: null,// 用于存放当前数据内容，防止查找后重新渲染的数据没有存在初始化创建的数据中。
    /**
     * 创建知识库
     * @param {Object} ctl 编辑器对象
    */
    CreateKnowledgeBase: function (ctl) {
        var that = this;
        var rootElement = DCTools20221228.GetOwnerWriterControl(ctl);
        //判断是否有知识库数据或者是否有自定义知识库事件
        if (rootElement && rootElement.EventKnowledgeBase || rootElement && rootElement.KnowledgeBaseOptions) {

            // 判断知识库对话框是否已经存在，如果存在则不会再重新创建
            if (rootElement.ownerDocument.getElementById("divknowledgeBase20240103")) {
                return;
            }
            // 创建知识库外壳
            var divknowledgeBase = rootElement.ownerDocument.createElement("div");
            divknowledgeBase.id = "divknowledgeBase20240103";// 知识库id
            divknowledgeBase.rootElementID = rootElement.id || '';// 保留编辑器id

            // 给知识库增加样式
            var styleknowledgeBase = rootElement.ownerDocument.createElement("style");
            styleknowledgeBase.innerHTML = that.GetKnowledgeBaseStyle(rootElement);
            divknowledgeBase.appendChild(styleknowledgeBase);

            //知识库外壳创建到编辑器中
            var pageContainer = rootElement.querySelector("[dctype=page-container]");
            pageContainer.appendChild(divknowledgeBase);
            //判断用户是否有自定义知识库功能，如果有则放权给客户自定义知识库功能
            if (!!rootElement.EventKnowledgeBase && typeof (rootElement.EventKnowledgeBase) == "function") {

                //创建用户自定义知识库的盒子,防止用户在自定义时直接清空知识库盒子，冲掉样式。
                var EventKnowledgeBaseDiv = rootElement.ownerDocument.createElement("div");
                EventKnowledgeBaseDiv.style.cssText = "width:100%;height:100%;"
                divknowledgeBase.appendChild(EventKnowledgeBaseDiv);
                //将知识库自定义盒子释放给用户
                rootElement.EventKnowledgeBase.call(rootElement, EventKnowledgeBaseDiv);
                return false;
            }
            // 给知识库外壳绑定一个销毁方法，便于在其他场景关闭知识库对话框
            divknowledgeBase.DistroyKnowledgeBase = that.DistroyKnowledgeBase.bind(that);

            // 创建知识库的搜索
            that.CreatedSearchKnowledgeBase(rootElement, divknowledgeBase);

            // 给知识库增加一个用于存放树结构的div
            var knowledgeBaseContainer = rootElement.ownerDocument.createElement("div");
            knowledgeBaseContainer.id = 'knowledgeBaseContainer20240106';
            divknowledgeBase.appendChild(knowledgeBaseContainer);

            // 根据客户数据渲染知识库的树结构并进行事件监听,知识库数据必须是一个数组对象
            if (rootElement.KnowledgeBaseOptions && Array.isArray(rootElement.KnowledgeBaseOptions)) {
                if (rootElement.KnowledgeBaseOptions.length) {
                    that.RenderknowledgeBaseContainer(rootElement.KnowledgeBaseOptions);
                    knowledgeBaseContainer.addEventListener('click', that.handleClick.bind(that, rootElement));// 监听树结构的点击事件
                    document.addEventListener('keydown', that.handleKeyDown);// 监听树结构的键盘事件
                } else {
                    knowledgeBaseContainer.innerHTML = '暂无知识库数据'
                    knowledgeBaseContainer.style.cssText = "text-align:center;color:grey;font-size:12px;"
                }

            }
        }
    },
    /**
     * 知识库树结构点击事件
     * @param {Object} rootElement 编辑器对象
     * @param {Object} event 点击事件对象
    */
    handleClick: function (rootElement, event) {
        event.stopPropagation()
        var that = this;
        var target = event && event.target;
        if (target) {
            if (Array.from(target.classList).indexOf('KnowledgeBaseOptionsItemFold') !== -1) {
                // 折叠或展开子列表
                var targetChildrenBox = target.parentNode.nextElementSibling;
                //判断是否为要折叠或展开的children盒子
                if (targetChildrenBox && Array.from(targetChildrenBox.classList).indexOf('KnowledgeBaseOptions-childrenBox') !== -1) {
                    var isFold = target.getAttribute('isfold');
                    isFold = isFold === 'true';// 折叠判断当前状态
                    targetChildrenBox.style.display = isFold ? 'block' : 'none';// 设置折叠展开
                    // 给所有的子元素标签设置隐藏，便于上下键选中操作
                    var LabelInnerArr = targetChildrenBox.querySelectorAll('.KnowledgeBaseOptionsItemLabelInner');
                    for (var i = 0; i < LabelInnerArr.length; i++) {
                        var node = LabelInnerArr[i];
                        node.style.display = isFold ? 'block' : 'none';
                        // 如果隐藏的元素中有选中状态，则清除掉选中
                        if (node.classList.contains('active')) {
                            node.classList.remove('active');
                        }
                    }

                    target.innerHTML = isFold ? that.ShouqiIcon : that.ZhankaiIcon;// 重置折叠的图标
                    target.setAttribute('isfold', isFold ? 'false' : 'true');// 重置折叠状态
                }
            } else if (Array.from(target.classList).indexOf('KnowledgeBaseOptionsItemLabel') !== -1) {
                // 使用知识库内容
                var KnowledgeBaseItemType = target.getAttribute('KnowledgeBaseItemType');
                if (KnowledgeBaseItemType !== '') {
                    var itemLabel = target.getAttribute('itemLabel');
                    //是用当前渲染的数据去查询that.KnowledgeBaseOptions
                    var clickTarget = that.FindItemByLabel(that.KnowledgeBaseOptions, itemLabel);
                    if (!!rootElement.EventKnowledgeBaseOnClick && typeof (rootElement.EventKnowledgeBaseOnClick) == "function") {
                        rootElement.EventKnowledgeBaseOnClick.call(rootElement, clickTarget);
                    }
                    that.DistroyKnowledgeBase(rootElement);
                }
            }
        }
    },
    /**
    * 知识库键盘事件，用于上下按键时选中选择
    * @param {Object} event 键盘事件对象
    */
    handleKeyDown: function (event) {
        if (event.key === 'ArrowUp' || event.key === 'ArrowDown') {
            event.preventDefault();
            var nodes = document.querySelectorAll('.KnowledgeBaseOptionsItemLabelInner');
            var visibelNodes = [];

            if (nodes && nodes.length) {
                let selectedNodeIndex = -1;
                //先排除隐藏的标签label
                for (var i = 0; i < nodes.length; i++) {
                    var node = nodes[i];
                    if (node.style && node.style.display !== 'none') {
                        visibelNodes.push(node);
                    }
                }

                //获取当前选中位置
                for (var i = 0; i < visibelNodes.length; i++) {
                    var node = visibelNodes[i];
                    if (node.classList.contains('active')) {
                        selectedNodeIndex = i;
                    }
                }

                if (event.key === 'ArrowUp') {
                    // 向上箭头键
                    selectedNodeIndex = ((selectedNodeIndex - 1) + visibelNodes.length) % visibelNodes.length;
                } else if (event.key === 'ArrowDown') {
                    // 向下箭头键
                    selectedNodeIndex = ((selectedNodeIndex + 1) + visibelNodes.length) % visibelNodes.length;
                }
                // 移除之前选中的节点的样式
                for (var i = 0; i < visibelNodes.length; i++) {
                    var node = visibelNodes[i];
                    node.classList.remove('active');// 删除上一个选中节点的样式
                    if (i === selectedNodeIndex) {
                        node.classList.add('active');// 添加新选中节点的样式
                    }
                }
            }

        } else if (event.key === "Enter") {
            // 在选中输入域或者段落时回车，会直接触发点击事件感知用户EventKnowledgeBaseOnClick
            var activeNode = document.querySelectorAll('.KnowledgeBaseOptionsItemLabelInner.active')[0];
            if (activeNode) {
                event.preventDefault();
                if (activeNode.classList.contains('field')) {
                    var activeParentNode = activeNode.parentNode;
                    activeParentNode.click();
                } else {
                    //在选中文件夹上回车，会关闭知识库对话框，与cs效果一致
                    //此处this指向document-Keydown事件，所以直接使用知识库标签绑定的销毁方法，保证编辑器的onkeydown事件不受影响
                    var divknowledgeBase = document.getElementById("divknowledgeBase20240103");
                    divknowledgeBase.DistroyKnowledgeBase();
                }
            }
        }
    },
    /**
    * 创建知识库的搜索功能
    * @param {Object} rootElement 编辑器对象
    * @param {Object} searchParentDiv 用于存放搜索框的div
    */
    CreatedSearchKnowledgeBase: function (rootElement, searchParentDiv) {
        var that = this;
        var searchDiv = rootElement.ownerDocument.createElement('div');
        searchDiv.innerHTML = `
            <input type="text" id="inputSearchKnowledgeBase20240110" autocomplete="off" placeholder="请输入搜索内容" class="input__inner">
        `
        searchParentDiv.appendChild(searchDiv)
        var inputSearchKnowledgeBase20240110 = rootElement.ownerDocument.getElementById('inputSearchKnowledgeBase20240110');
        inputSearchKnowledgeBase20240110.focus()
        inputSearchKnowledgeBase20240110.addEventListener('input', function (e) {
            if (!!rootElement.EventKnowledgeBaseSearchOnInput && typeof (rootElement.EventKnowledgeBaseSearchOnInput) == "function") {
                rootElement.EventKnowledgeBaseSearchOnInput(e.target.value, function (data) {
                    // 此回调用于客户返回查找后的数据时重新渲染知识库
                    that.RenderknowledgeBaseContainer(data);
                });
            }
        })
    },
    /**
    * 根据客户数据渲染知识库树状结构的公共方法
    * @param {Array} data 知识库数据
    */
    RenderknowledgeBaseContainer: function (data) {
        this.KnowledgeBaseOptions = data;// 重新保留数据
        var knowledgeBaseContainer = document.getElementById('knowledgeBaseContainer20240106')
        knowledgeBaseContainer.innerHTML = null;
        knowledgeBaseContainer.innerHTML = this.renderChildrenInner(data, true);// 递归渲染出树状结构
    },
    /**
    * 渲染多层知识库树状结构的递归函数
    * @param {Array} childNodes 知识库数据的子数据
    * @param {Boolean} firstLevel 是否是第一层数据，用于设置左间距形成树状。第一层数据无需设置做间距。
    */
    renderChildrenInner: function (childNodes, firstLevel = false) {
        var that = this;
        var childNodesInner = '';
        if (Array.isArray(childNodes) && childNodes.length) {
            for (var i = 0; i < childNodes.length; i++) {
                var item = childNodes[i];
                childNodesInner += `
                <div class="KnowledgeBaseOptions-item" style="${firstLevel ? '' : 'margin-left:20px;'}">
                    <div class="KnowledgeBaseOptions-lable">
                        <span class="KnowledgeBaseOptionsItemFold" isFold="false">
                            ${item.children && item.children.length ? that.ShouqiIcon : ''}
                        </span>
                        <span class="KnowledgeBaseOptionsItemLabel" KnowledgeBaseItemType="${(item && item.type) ? item.type : ''}"  itemLabel=${item.label} >
                            ${item.children && item.children.length ? that.ShubenIcon : ''}
                            ${item.type && item.type === 'inputFieldElement' ? that.ShuruyuFileIcon : ''}
                            ${item.type && item.type === 'ParagraphFlagElement' ? that.Duanluo2Icon : ''}
                            <span class='KnowledgeBaseOptionsItemLabelInner ${item.type ? 'field' : ''}'>${item.label}</span>
                        </span>
                    </div>
                    ${item.children ? that.renderChildrenInner(item.children) : ``}
                </div>`
            }
        }
        return `<div class="KnowledgeBaseOptions-childrenBox">${childNodesInner}</div>`;
    },
    /**
    * 知识库整体功能的销毁方法
    */
    DistroyKnowledgeBase: function () {
        var that = this;
        var divknowledgeBase = document.getElementById("divknowledgeBase20240103");
        if (divknowledgeBase) {
            if (divknowledgeBase.rootElementID) {
                var ctl = document.getElementById(divknowledgeBase.rootElementID);
                that.KnowledgeBaseOptions = null;
                document.removeEventListener('keydown', that.handleKeyDown)
                divknowledgeBase.innerHTML = null;
                divknowledgeBase.remove();//删除知识库
                if (ctl && ctl.Focus && typeof (ctl.Focus) == "function") {
                    ctl.Focus();//重新聚焦光标
                }
            }
        }
    },
    /**
    * 获取知识库位置的位置。根据光标计算出位置，返回left、top
    * @param {object} rootElement 编辑器对象
    */
    GetCaretPosition(rootElement) {
        let caretPosition = null;
        var divCaret = rootElement.querySelector("#divCaret20221213");
        let { top, height, left } = divCaret.style;
        try {
            var divCaretTop = top && parseInt(top, 10);
            var divCaretHeight = height && parseInt(height, 10);
            var divCaretLeft = left && parseInt(left, 10);
            caretPosition = {
                top: divCaretTop + divCaretHeight,
                left: divCaretLeft
            }
        } catch (error) {
            console.warn('光标值解析错误', error)
        }
        return caretPosition
    },
    /**
    * 根绝label查找对应的数据对象，返回给客户
    * @param {object} data 当前知识库数据
    * @param {string} label 数据标签
    */
    FindItemByLabel: function (data, label) {
        var that = this;
        for (let i = 0; i < data.length; i++) {
            const item = data[i];
            if (item.label === label) {
                return item;
            } else if (item.children) {
                const result = that.FindItemByLabel(item.children, label);
                if (result) {
                    return result;
                }
            }
        }
        return null;
    },

    /**
    * 知识库样式
    * @param {object} rootElement 编辑器对象
    * @param {string} label 数据标签
    */
    GetKnowledgeBaseStyle(rootElement) {
        var caretPosition = this.GetCaretPosition(rootElement);//获取知识库要创建的位置
        return `
            #divknowledgeBase20240103{
                min-width:100px;
                min-height:50px;
                box-shadow: 0 2px 12px 0 rgba(0,0,0,.1);
                position: absolute;
                top:${caretPosition.top}px;
                left:${caretPosition.left}px;
                z-index: 100009; 
                text-align:left;
                user-select: none; 
                background:#ffffff;
            }
            #knowledgeBaseContainer20240106::-webkit-scrollbar {
                width: 8px;
                height: 8px;
            }
            #knowledgeBaseContainer20240106::-webkit-scrollbar-thumb {
                border-radius: 10px;
                opacity: 0.2;
                background: #9093994d;
            }
            #knowledgeBaseContainer20240106::-webkit-scrollbar-track {
                border-radius: 0;
                border-right: 2px solid #f6f6f6;
            }
            .input__inner{
                background-color: #fff;
                background-image: none;
                border:none;
                border-bottom: 1px solid #dcdfe6;
                box-sizing: border-box;
                color: #606266;
                display: inline-block;
                font-size: 14px;
                height: 34px;
                line-height: 34px;
                outline: none;
                padding: 0 15px;
                transition: border-color .2s cubic-bezier(.645,.045,.355,1);
                width: 100%;
            }
            .KnowledgeBaseOptions-item,.KnowledgeBaseOptions-lable,.KnowledgeBaseOptions-childrenBox{
                cursor: pointer;  
            }
            #knowledgeBaseContainer20240106{
                padding: 12px;
                box-sizing: border-box;
                max-height: 300px;
                overflow: auto;
            }
            .KnowledgeBaseOptions-lable {
                line-height: 24px;
            }
            .svg-icon,.svg-icon path,.KnowledgeBaseOptionsItemLabelInner{
                pointer-events: none; 
            }
            .KnowledgeBaseOptions-lable,.KnowledgeBaseOptionsItemLabel{
                display: flex;
                align-items: center;
            }
            .KnowledgeBaseOptionsItemLabel,.KnowledgeBaseOptionsItemLabelInner{
                flex: 1;
                padding: 0 5px;
                box-sizing: border-box;
            }
            .KnowledgeBaseOptionsItemLabelInner.active{
                background:#c6e2ff;
            }
            `
    },
    Duanluo2Icon: `<svg t="1704882030943" class="svg-icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="16658" width="16" height="16"><path d="M316 680h392c15.5 0 28 12.5 28 28s-12.5 28-28 28H316c-15.5 0-28-12.5-28-28s12.5-28 28-28z m0-168h392c15.5 0 28 12.5 28 28s-12.5 28-28 28H316c-15.5 0-28-12.5-28-28s12.5-28 28-28z" p-id="16659"></path><path d="M600.8 120L848 367.2V904H176V120h424.8m0-56H176c-30.9 0-56 25.1-56 56v784c0 30.9 25.1 56 56 56h672c30.9 0 56-25.1 56-56V367.2c0-14.9-5.9-29.1-16.4-39.6L640.4 80.4A55.924 55.924 0 0 0 600.8 64z" p-id="16660"></path><path d="M624 120h-56v252c0 15.5 12.5 28 28 28h28V120z" p-id="16661"></path><path d="M848 344H568v28c0 15.5 12.5 28 28 28h252v-56z" p-id="16662"></path></svg>`,
    ShuruyuFileIcon: `<svg t="1704881652345" class="svg-icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="114384" width="14" height="14"><path d="M566.958763 0.323519H133.702114A63.032957 63.032957 0 0 0 64.593692 56.901293v911.319856a63.032957 63.032957 0 0 0 69.108422 55.438625h756.015765a63.032957 63.032957 0 0 0 69.108422-56.577774V403.202838z m344.782679 438.192964v536.919282H111.678551V48.547528l362.62936-3.797166v389.968955z m-368.325109-389.968955l356.174177 345.162395v-3.417449h-377.05859v-341.744946z" p-id="114385"></path></svg>`,
    ZhankaiIcon: `<svg class="svg-icon" width="14" height="14" viewBox="0 0 1024 1024" version="1.1"
    xmlns="http://www.w3.org/2000/svg">
    <path fill="#303133" d="M70.4 153.6c0-45.9264 37.2736-83.2 83.2-83.2h716.8c45.9264 0 83.2 37.2736 83.2 83.2v716.8c0 45.9264-37.2736 83.2-83.2 83.2H153.6A83.2 83.2 0 0 1 70.4 870.4V153.6z m64 0v716.8c0 10.5984 8.6016 19.2 19.2 19.2h716.8a19.2 19.2 0 0 0 19.2-19.2V153.6A19.2 19.2 0 0 0 870.4 134.4H153.6A19.2 19.2 0 0 0 134.4 153.6z" />
    <path fill="#303133" d="M307.2 544a32 32 0 1 1 0-64h409.6a32 32 0 1 1 0 64H307.2z" />
    <path fill="#303133" d="M480 307.2a32 32 0 1 1 64 0v409.6a32 32 0 1 1-64 0V307.2z" />
</svg>`,
    ShouqiIcon: `<svg class="svg-icon" width="14" height="14" viewBox="0 0 1024 1024" version="1.1"
    xmlns="http://www.w3.org/2000/svg">
    <path fill="#303133" d="M70.4 153.6c0-45.9264 37.2736-83.2 83.2-83.2h716.8c45.9264 0 83.2 37.2736 83.2 83.2v716.8c0 45.9264-37.2736 83.2-83.2 83.2H153.6A83.2 83.2 0 0 1 70.4 870.4V153.6z m64 0v716.8c0 10.5984 8.6016 19.2 19.2 19.2h716.8a19.2 19.2 0 0 0 19.2-19.2V153.6A19.2 19.2 0 0 0 870.4 134.4H153.6A19.2 19.2 0 0 0 134.4 153.6z" />
    <path fill="#303133" d="M307.2 544a32 32 0 1 1 0-64h409.6a32 32 0 1 1 0 64H307.2z" />
</svg>`,
    ShubenIcon: `<svg class="svg-icon" width="16px" height="16px" viewBox="0 0 1024 1024" version="1.1"
    xmlns="http://www.w3.org/2000/svg">
    <path fill="#800080" d="M959.4 135.5v822.6H212.5C129.3 958.2 62 890.8 62 807.7V211.2C62 128 129.3 60.7 212.5 60.7h597.3V659H213.4c-42.1 0-76.7 34.6-76.7 76.6v71c0 42.1 34.6 76.7 76.7 76.7h671.2V135.5h74.8zM809.8 733.8H211.6v74.8h598.3v-74.8z m0 0" />
</svg>`,

};
