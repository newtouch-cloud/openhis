//*************************************************************************
//* 当前版本: 20240625
//* 开始时间: 20240625
//* 开发者: 李新宇
//* 重要描述: 创建输入域的表格下拉列表控件
//*************************************************************************
//* 最后更新时间: 2024-06-25 14:25:00
//* 最后更新人: 李新宇
//*************************************************************************
"use strict";
import { DCTools20221228 } from "./DCTools20221228.js";
import { WriterControl_Paint } from "./WriterControl_Paint.js";

export let WriterControl_QueryListTableControl = {
    /**
     * 记录当前编辑器对象
    */
    rootElement: null,
    /**
     * 记录当前编辑器的下拉表格数据
    */
    tableOptions: null,
    /**
    * 初始化下拉列表控件
    * @param {string} containerID 编辑器编号
    * @param {number} tableData 元素的下拉表格数据
    */
    InitDropDownInputTableList: function (containerID, tableOptions, intPageIndex, intLeft, intTop, intHeight) {
        this.rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        this.tableOptions = JSON.parse(JSON.stringify(tableOptions));//保留一遍原始数据
        //创建下拉表格dom结构的样式
        this.TableControlStyle(containerID, intPageIndex, intLeft, intTop, intHeight);
        //创建下拉表格dom结构
        this.CreatedDropDownInputTableDom(containerID, this.tableOptions);
    },
    /**
    * 创建下拉列表dom结构
    * @param {string} containerID 编辑器编号
    * @param {number} tableData 元素的下拉表格数据
    */
    CreatedDropDownInputTableDom: function (containerID, tableOptions) {
        let that = this;
        let rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        let tableControlID = "DCTableControl20240625151400";
        let tableControl = rootElement.querySelector("#" + tableControlID) || null;
        if (tableControl === null) {
            //创建最外层div
            tableControl = rootElement.ownerDocument.createElement("div");
            tableControl.id = tableControlID;
            var pageContainer = rootElement.querySelector('[dctype="page-container"]');
            pageContainer.appendChild(tableControl);
            // 绑定一个销毁方法，便于在其他场景关闭下拉
            tableControl.CloseDropdownTable = this.CloseDropdownTable.bind(this);
            //创建搜索
            let searchBox = rootElement.ownerDocument.createElement("div");
            searchBox.classList.add("dc_drop_down_input_table_search_box");

            //创建搜索框的输入框
            let searchInput = rootElement.ownerDocument.createElement("input");
            searchInput.classList.add("dc_drop_down_input_table_search_input");
            searchInput.type = "text";
            searchInput.placeholder = "请输入搜索关键字";
            searchInput.title = "";
            searchBox.appendChild(searchInput);//给搜索增加输入框

            //创建搜索按钮
            let searchButton = rootElement.ownerDocument.createElement("div");
            searchButton.classList.add("dc_drop_down_input_table_search_button");
            searchButton.innerHTML = "查找";
            searchBox.appendChild(searchButton);//给搜索增加查找按钮
            searchButton.addEventListener("click", function (e) {
                that.DispatchEventQueryTableListDataSearch(rootElement);
            });

            //创建重置按钮
            let searchResetButton = rootElement.ownerDocument.createElement("div");
            searchResetButton.classList.add("dc_drop_down_input_table_reset_button");
            searchResetButton.innerHTML = "重置";
            searchBox.appendChild(searchResetButton);//给搜索增加重置按钮
            searchResetButton.addEventListener('click', function () {
                let searchDom = rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_search_input");
                searchDom.value = '';//置空查找的内容
                searchButton.click();//触发查找事件
            });

            //创建搜索框
            tableControl.appendChild(searchBox);
            //默认聚焦到搜索框
            searchInput.focus();

            //创建中间表格部分
            let tableBox = rootElement.ownerDocument.createElement("div");
            tableBox.classList.add("dc_drop_down_input_table_box");
            tableControl.appendChild(tableBox);
            //根据数据渲染内容tableBox
            if (tableOptions && tableOptions.tableData) {
                this.ChangedPageTabelData(tableOptions.tableData);
            }

            //如果有分页，则会创建页脚分页盒子
            if (tableOptions && tableOptions.pagination) {
                let footerBox = rootElement.ownerDocument.createElement("div");
                footerBox.classList.add("dc_drop_down_input_table_footer_box");
                tableControl.appendChild(footerBox);
                //渲染分页内部结构
                this.RenderPagination(rootElement, tableOptions.pagination, footerBox);
            }
            var selectKeyDownClass = "dc_drop_down_input_table_row_keyDown_active";
            tableControl.addEventListener("keydown", function (e) {
                //上下键选中行
                if (e && (e.code === "ArrowUp" && e.keyCode === 38) || (e.code === "ArrowDown" && e.keyCode === 40)) {
                    e.preventDefault();
                    e.stopPropagation();
                    //获取所有的行
                    var allRows = tableControl.querySelectorAll(".dc_drop_down_input_table_row:not(.dc_drop_down_input_table_row_header)");
                    //获取当前是否有被键盘选中的行
                    let currentActiveKeyDownRow = tableControl.querySelector("." + selectKeyDownClass);
                    if (currentActiveKeyDownRow) {
                        allRows.forEach((item, index) => {
                            if (item === currentActiveKeyDownRow) {
                                item.classList.remove(selectKeyDownClass);

                                if (e.code === "ArrowUp" && e.keyCode === 38) {  //上键选中行
                                    //选中上一行
                                    if (index > 0) {
                                        allRows[index - 1].classList.add(selectKeyDownClass);
                                        return;
                                    }
                                    //上键选中行，当前是第一行，则选中最后一行
                                    if (index === 0) {
                                        allRows[allRows.length - 1].classList.add(selectKeyDownClass);
                                        return;
                                    }
                                } else if (e.code === "ArrowDown" && e.keyCode === 40) {//下键选中行
                                    //选中下一行
                                    if (index < allRows.length - 1) {
                                        allRows[index + 1].classList.add(selectKeyDownClass);
                                        return;
                                    }
                                    //下键选中行，当前是最后一行，则选中第一行
                                    if (index === allRows.length - 1) {
                                        allRows[0].classList.add(selectKeyDownClass);
                                        return;
                                    }
                                }
                            }
                        });
                    } else {
                        if (e.code === "ArrowUp" && e.keyCode === 38) {
                            allRows[allRows.length - 1].classList.add(selectKeyDownClass);
                        } else if (e.code === "ArrowDown" && e.keyCode === 40) {
                            allRows[0].classList.add(selectKeyDownClass);
                        }
                    }
                    //将最新选中的行滚动到可视区域
                    var newcurrentActiveKeyDownRow = tableControl.querySelector("." + selectKeyDownClass);
                    newcurrentActiveKeyDownRow.scrollIntoViewIfNeeded(true);
                } else if (e && (e.code === "Enter" && e.keyCode === 13)) {
                    e.preventDefault();
                    e.stopPropagation();
                    //获取当前选中的行
                    var currentActiveKeyDownRow = tableControl.querySelector("." + selectKeyDownClass);
                    if (currentActiveKeyDownRow) {
                        let currentCheckbox = currentActiveKeyDownRow.querySelector(".dc_drop_down_input_table_checkbox");
                        if (currentCheckbox) {
                            //多选
                            currentCheckbox.click();
                            searchInput.focus();//聚焦到搜索框,防止光标被抢
                        } else {
                            //单选
                            currentActiveKeyDownRow.click();
                        }
                    } else {
                        //没有选中行，触发查找事件
                        that.DispatchEventQueryTableListDataSearch(rootElement);
                    }
                } else {
                    //其他键盘事件，直接取消选中行
                    var currentActiveKeyDownRow = tableControl.querySelector("." + selectKeyDownClass);
                    if (currentActiveKeyDownRow) {
                        currentActiveKeyDownRow.classList.remove(selectKeyDownClass);
                    }
                }
            });
        }
    },
    /**
     * RenderPagination
     * @param {object} rootElement 编辑器对象
     * @param {object} paginationOptions 分页配置
     * @param {object} footerBox 分页控件父容器
    */
    RenderPagination: function (rootElement, paginationOptions, footerBox) {
        let that = this;
        //ever_count每页条数//page_total总页数//page_currentpage当前页数
        let { page_total, page_currentpage } = paginationOptions;
        //上一页按钮
        var page_prev_box = rootElement.ownerDocument.createElement("div");
        page_prev_box.classList.add("dc_drop_down_input_table_page_prev_box");
        page_prev_box.innerHTML = "‹";
        page_prev_box.title = "上一页";
        if (page_currentpage <= 1) {
            page_prev_box.classList.add("dc_drop_down_input_table_page_prev_box_disabled");
        }
        footerBox.appendChild(page_prev_box);
        page_prev_box.addEventListener("click", function () {
            let cureentpage = parseInt(rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_page_total_current_span").innerHTML);
            if (cureentpage > 1) {
                //页码发生改变事件
                that.DispatchEventQueryTableListDataPageChanged(rootElement, cureentpage - 1);
            }
            let searchDom = rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_search_input");
            searchDom.focus();
        });

        // //总页数按钮
        // var page_total_box = rootElement.ownerDocument.createElement("div");
        // page_total_box.classList.add("dc_drop_down_input_table_page_total_box");
        // for (var i = 0; i < page_total; i++) {
        //     let pageItem = rootElement.ownerDocument.createElement("div");
        //     pageItem.classList.add("dc_drop_down_input_table_page_item");
        //     if (i + 1 === page_currentpage) {
        //         pageItem.classList.add("dc_drop_down_input_table_page_item_active");
        //     }
        //     pageItem.innerHTML = i + 1;
        //     page_total_box.appendChild(pageItem);
        // }
        // footerBox.appendChild(page_total_box);

        //页脚总计
        var page_total_count = rootElement.ownerDocument.createElement("div");
        page_total_count.classList.add("dc_drop_down_input_table_page_total_count");
        page_total_count.innerHTML = `<span class="dc_drop_down_input_table_page_total_current_span">${page_currentpage}</span>/<span class="dc_drop_down_input_table_page_total_span">${page_total}</span>`;
        footerBox.appendChild(page_total_count);

        //下一页按钮
        var page_next_box = rootElement.ownerDocument.createElement("div");
        page_next_box.classList.add("dc_drop_down_input_table_page_next_box");
        page_next_box.innerHTML = "›";
        page_next_box.title = "下一页";
        if (page_currentpage >= page_total_count) {
            page_prev_box.classList.add("dc_drop_down_input_table_page_next_box_disabled");
        }
        footerBox.appendChild(page_next_box);
        page_next_box.addEventListener("click", function () {
            let cureentpage = parseInt(rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_page_total_current_span").innerHTML);
            if (cureentpage < page_total) {
                //页码发生改变事件
                that.DispatchEventQueryTableListDataPageChanged(rootElement, cureentpage + 1);
            }
            let searchDom = rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_search_input");
            searchDom.focus();
        });


    },
    /**
     * 触发分页事件
     * @param {object} rootElement 编辑器对象
     * @param {number} changedPage 改变的页码
    */
    DispatchEventQueryTableListDataPageChanged: function (rootElement, changedPage) {
        let that = this;
        if (rootElement.EventQueryTableListDataPageChanged != null && typeof rootElement.EventQueryTableListDataPageChanged == 'function') {
            rootElement.EventQueryTableListDataPageChanged(changedPage, that.ChangedPageTabelData.bind(that));
        }
        //更新页码显示
        rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_page_total_current_span").innerHTML = changedPage;
        //上一页按钮是否禁用
        let page_prev_box = rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_page_prev_box");
        if (changedPage <= 1) {
            page_prev_box.classList.add("dc_drop_down_input_table_page_prev_box_disabled");
        } else {
            page_prev_box.classList.remove("dc_drop_down_input_table_page_prev_box_disabled");
        }
        //下一页按钮是否禁用
        let page_next_box = rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_page_next_box");
        let page_total = parseInt(rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_page_total_span").innerHTML);
        if (changedPage >= page_total) {
            page_next_box.classList.add("dc_drop_down_input_table_page_next_box_disabled");
        } else {
            page_next_box.classList.remove("dc_drop_down_input_table_page_next_box_disabled");
        }
    },
    /**
     * 触发查找事件
    */
    DispatchEventQueryTableListDataSearch: function (rootElement) {
        let that = this;
        let searchValue = rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_search_input").value;
        searchValue = searchValue.trim();//查找的内容
        //触发查找事件
        if (rootElement.EventQueryTableListDataSearch != null && typeof rootElement.EventQueryTableListDataSearch == 'function') {
            rootElement.EventQueryTableListDataSearch(searchValue, that.TabelDataSearchForReset.bind(that));
        } else {
            //内部查找
            var newTableOptions = JSON.parse(JSON.stringify(that.tableOptions));//防止改变原数据
            var { tableData, tableHeader } = newTableOptions;
            if (searchValue === '') {
                this.ChangedPageTabelData(tableData);
            } else {
                let newTableData = {};
                Object.keys(tableData).forEach(key => {
                    if (tableData[key].length > 0) {
                        tableData[key].forEach(DataItem => {
                            tableHeader.forEach(HeaderItem => {
                                //筛选展示出来的内容中，是否包含查找到内容
                                if ((DataItem[HeaderItem.prop].toString().toLowerCase()).indexOf(searchValue.toLowerCase()) > -1) {
                                    if (newTableData[key]) {
                                        newTableData[key].push(DataItem);
                                    } else {
                                        newTableData[key] = [DataItem];
                                    }
                                }
                            });
                        });
                    }
                });
                this.ChangedPageTabelData(newTableData);//重新渲染表格
            }
        }
    },
    /**
     *查找数据后，重置表格和分页
    */
    TabelDataSearchForReset: function (searchTableOptions) {
        let that = this;
        // 需要重走表格、页脚分页逻辑
        //根据数据渲染内容tableBox
        this.tableOptions = JSON.parse(JSON.stringify(searchTableOptions));//保留一遍原始数据
        if (searchTableOptions && searchTableOptions.tableData) {
            this.ChangedPageTabelData(searchTableOptions.tableData);
        }
        //如果有分页，则会创建页脚分页盒子
        if (searchTableOptions && searchTableOptions.pagination) {
            let footerBox = this.rootElement.ownerDocument.querySelector('.dc_drop_down_input_table_footer_box');
            footerBox.innerHTML = "";
            //渲染分页内部结构
            this.RenderPagination(this.rootElement, searchTableOptions.pagination, footerBox);
        }
    },
    /**
     * 修改表格内容事件 
     * */
    ChangedPageTabelData: function (pageTableData = null) {
        let that = this;
        if (!pageTableData) {
            //不传值时，默认取当前tableOptions（只有打开下拉）
            pageTableData = JSON.parse(JSON.stringify(that.tableOptions.tableData));
        }

        let newTableHeader = JSON.parse(JSON.stringify(that.tableOptions.tableHeader));
        var targetInput = this.rootElement.CurrentInputField();
        var targetInputProps = this.rootElement.GetElementProperties(targetInput);
        let { InnerMultiSelect, RepulsionForGroup } = targetInputProps;
        if (InnerMultiSelect) {
            newTableHeader.unshift({ prop: 'check', label: '', width: 30 });
        }
        if (pageTableData && Object.keys(pageTableData).length > 0) {
            var tableBox = this.rootElement.ownerDocument.querySelector(".dc_drop_down_input_table_box");
            //非分组互斥的情况下，给表格添加一个class，便于样式控制
            if (!RepulsionForGroup) {
                tableBox.classList.add('dc_drop_down_input_table_repulsion_none_group');
            }
            tableBox.innerHTML = "";
            //渲染分页内部结构
            Object.keys(pageTableData) && Object.keys(pageTableData).forEach(key => {
                if (pageTableData[key].length > 0) {
                    that.RenderTable(this.rootElement, newTableHeader, pageTableData[key], tableBox, key);
                }
            });

            // 将滚动条滚动到最顶部
            tableBox.scrollTop = 0;
        }
    },

    /**
    * 创建表格
    * @param {object} rootElement 编辑器对象
    * @param {array} tableHeader 表头数据
    * @param {array} tableData 表格数据
    * @param {object} tableControl 表格控件
    * @param {object} key 表格分组键名
    */
    RenderTable: function (rootElement, tableHeader, tableData, tableControl, key) {
        let that = this;
        //给每个tableGroup添加一个id，便于全选逻辑
        // var dcTableGroupID = "DCTableGroup20240625151400_tableGroup_" + new Date().getTime();
        /** 当前输入域 */
        var targetInput = rootElement.CurrentInputField();
        var targetInputProps = rootElement.GetElementProperties(targetInput);
        let { InnerValue, Text, Elements, RepulsionForGroup } = targetInputProps;

        //每一个分组
        let tableGroup = rootElement.ownerDocument.createElement('div');
        tableGroup.classList.add('dc_drop_down_input_table_group');
        // tableGroup.setAttribute('data-group-id', dcTableGroupID);//设置分组id，对应全选按钮

        //分组的title
        let tableTile = rootElement.ownerDocument.createElement('div');
        tableTile.classList.add('dc_drop_down_input_table_title');
        tableTile.innerHTML = `<span class="dc_drop_down_input_table_title_span">${key}</span>`;
        tableGroup.appendChild(tableTile);//给分组中增加标题

        //分组的内容
        let tableContent = rootElement.ownerDocument.createElement('div');
        tableContent.classList.add('dc_drop_down_input_table_content');
        tableGroup.appendChild(tableContent);//给分组中增加内容盒子

        //创建表格
        const table = rootElement.ownerDocument.createElement('div');
        table.classList.add('dc_drop_down_input_table_table');
        const headerRow = rootElement.ownerDocument.createElement('div');
        headerRow.classList.add('dc_drop_down_input_table_row');
        headerRow.classList.add('dc_drop_down_input_table_row_header');
        table.appendChild(headerRow);//给表格增加头部
        tableContent.appendChild(table);//给分组内容盒子中增加表格
        tableControl.appendChild(tableGroup);//将分组内容加入到控件中

        //创建表头
        tableHeader.forEach(item => {
            const cell = rootElement.ownerDocument.createElement('div');
            cell.classList.add('dc_drop_down_input_table_cell');
            cell.classList.add('dc_drop_down_input_table_header');
            cell.style.width = item.width ? (item.width + 'px') : 'auto';
            if (item.prop === 'check') {
                //增加全选勾选框
                const checkbox = rootElement.ownerDocument.createElement('input');
                checkbox.type = 'checkbox';
                checkbox.title = '全选';
                checkbox.classList.add('dc_drop_down_input_table_all_checkbox');
                // checkbox.setAttribute('data-target-group-id', dcTableGroupID);
                cell.appendChild(checkbox);
                checkbox.addEventListener('change', function (e) {
                    // 全选事件
                    const isChecked = e.target.checked;
                    const checkboxes = tableGroup.querySelectorAll('.dc_drop_down_input_table_checkbox');
                    var allTableGroupText = [];
                    var allTableGroupValue = [];
                    checkboxes.forEach((checkbox, index) => {
                        const currentTableRow = checkbox.parentNode.parentNode;
                        // 仅在状态不同的情况下更新状态
                        if (checkbox.checked !== isChecked) {
                            checkbox.checked = isChecked;
                            if (tableData[index].type === 'image') {
                                // 图片类型走原来的逻辑
                                that.SelectTableRow(rootElement, tableData[index], key, currentTableRow);
                            } else {
                                allTableGroupText.push(tableData[index].text);
                                allTableGroupValue.push(tableData[index].value);
                            }
                        }
                    });

                    if (allTableGroupText.length > 0 || allTableGroupValue.length > 0) {
                        // 对文本批量处理
                        that.SelectAllTableRow(rootElement, isChecked, allTableGroupText, allTableGroupValue, key);
                    }

                });
            } else {
                cell.innerHTML = item.label;
            }

            headerRow.appendChild(cell);
        });
        //创建行
        tableData.forEach(item => {
            const row = rootElement.ownerDocument.createElement('div');
            row.classList.add('dc_drop_down_input_table_row');
            if (item.text === Text) {
                row.classList.add('dc_drop_down_input_table_row_active');
            }
            if (item.type === 'image') {
                Elements.forEach(optionsItem => {
                    let PropItem = rootElement.GetElementProperties(optionsItem);
                    if (PropItem.ID === ('dcTableSelectImage' + item.value)) {
                        row.classList.add('dc_drop_down_input_table_row_active');
                    }
                });
            }
            tableHeader.forEach(headerItem => {
                const cell = rootElement.ownerDocument.createElement('div');
                cell.classList.add('dc_drop_down_input_table_cell');
                cell.style.width = headerItem.width ? (headerItem.width + 'px') : 'auto';
                if (headerItem.prop === 'check') {
                    const checkbox = rootElement.ownerDocument.createElement('input');
                    checkbox.type = 'checkbox';
                    checkbox.classList.add('dc_drop_down_input_table_checkbox');
                    cell.appendChild(checkbox);
                    if (item.type === 'image') {
                        Elements.forEach(optionsItem => {
                            let PropItem = rootElement.GetElementProperties(optionsItem);
                            if (PropItem.ID === ('dcTableSelectImage' + item.value)) {
                                checkbox.checked = true;
                            }
                        });
                    } else {
                        if (Text && Text.split(',').indexOf(item.text) > -1 && InnerValue && InnerValue.split(',').indexOf(item.value) > -1) {
                            //value存在于当前值中，则选中
                            checkbox.checked = true;
                        }
                    }

                } else if (headerItem.prop === 'text' && item.type === 'image') {
                    const image = rootElement.ownerDocument.createElement('img');
                    image.classList.add('dc_drop_down_input_table_img');
                    image.src = item[headerItem.prop];
                    cell.appendChild(image);
                } else {
                    cell.innerHTML = item[headerItem.prop];
                }
                row.appendChild(cell);
            });

            table.appendChild(row);
            row.addEventListener('click', (e) => {
                if (targetInputProps && targetInputProps.InnerMultiSelect) {
                    // 多选模式
                    if (e.target && e.target.tagName === 'INPUT' && e.target.type === 'checkbox') {
                        this.SelectTableRow(rootElement, item, key, row);

                        //重置全选状态
                        if (e.target.type === 'checkbox') {
                            var allCheckboxDom = tableGroup.querySelector('.dc_drop_down_input_table_all_checkbox');
                            if (e.target.checked === false) {
                                allCheckboxDom.checked = false;
                            } else {
                                let checkboxes = tableGroup.querySelectorAll('.dc_drop_down_input_table_checkbox');
                                allCheckboxDom.checked = Array.from(checkboxes).every(checkbox => checkbox.checked);
                            }
                        }

                    }
                } else {
                    // 单选模式
                    this.SelectTableRow(rootElement, item, key, row);
                }

            });
        });

        //重置全选状态
        if (targetInputProps && targetInputProps.InnerMultiSelect) {
            //全选框
            var allCheckbox = tableGroup.querySelector('.dc_drop_down_input_table_all_checkbox');
            //判断当前表格是否全被选中
            let checkboxes = tableGroup.querySelectorAll('.dc_drop_down_input_table_checkbox');
            allCheckbox.checked = Array.from(checkboxes).every(checkbox => checkbox.checked);
        }
    },


    /**
    * 表格全选事件 （20241119 新增 lxy 此方法是为了处理全选多次操作卡顿的情况，但又考虑到表格数据中可能会存在图片的情况，所以只有纯文本时才会调用此方法）
    * (此文件中的多选、全选逻辑还需要优化。)
    * @param {object} rootElement 编辑器对象
    * @param {object} isChecked 全选/取消全选
    * @param {string} allTableGroupText 所有操作的文本
    * @param {string} allTableGroupValue 所有操作的value
    * @param {string} key 表格分组键名
    */
    SelectAllTableRow: function (rootElement, isChecked, allTableGroupText, allTableGroupValue, key) {
        let that = this;
        /** 当前输入域 */
        var targetInput = rootElement.CurrentInputField();
        var targetInputProps = rootElement.GetElementProperties(targetInput);
        let { InnerValue, Text, Elements } = targetInputProps;
        let newValue = InnerValue ? InnerValue.split(',') : [];
        let newText = Text ? Text.split(',') : [];
        //文本
        allTableGroupText.forEach(itemText => {
            let indexText = newText.indexOf(itemText);
            if (isChecked) {
                if (indexText === -1) {
                    newText.push(itemText);
                }

            } else {
                if (indexText > -1) {
                    newText.splice(indexText, 1);
                }
            }
        });
        //值
        allTableGroupValue.forEach(itemValue => {
            let indexValue = newValue.indexOf(itemValue);
            if (isChecked) {
                if (indexValue === -1) {
                    newValue.push(itemValue);
                }
            } else {
                if (indexValue > -1) {
                    newValue.splice(indexValue, 1);
                }
            }
        });

        rootElement.SetElementProperties(targetInputProps.NativeHandle, {
            Text: newText.join(','),
            innerValue: newValue.join(','),
        });
        rootElement.FocusElement(targetInputProps.NativeHandle);
        if (rootElement && rootElement.EventQueryTableListItemSelected && typeof rootElement.EventQueryTableListItemSelected === 'function') {
            rootElement.EventQueryTableListItemSelected({
                Text: newText.join(','),
                innerValue: newValue.join(','),
            });
        }
    },
    /**
    * 点击行单选事件
    * @param {object} rootElement 编辑器对象
    * @param {object} row 行对象
    * @param {string} key 表格分组键名
    * @param {object} rowDOM 行dom对象
    */
    SelectTableRow: function (rootElement, row, key, rowDOM) {
        let that = this;
        /** 当前输入域 */
        var targetInput = rootElement.CurrentInputField();
        var targetInputProps = rootElement.GetElementProperties(targetInput);
        let { InnerValue, Text } = targetInputProps;
        if (targetInputProps && targetInputProps.InnerMultiSelect) {
            // 多选模式
            //分页之后怎么处理？????
            let newValue = InnerValue ? InnerValue.split(',') : [];
            let newText = Text ? Text.split(',') : [];
            let currentCheckDom = rowDOM.querySelector('input[type="checkbox"]');
            let currentCheck = currentCheckDom.checked;
            let indexValue = newValue.indexOf(row.value);
            let indexText = newText.indexOf(row.text);
            //将图片数据保留，防止图片丢失
            let ElementsData = [];
            if (targetInputProps.Elements && targetInputProps.Elements.length > 0) {
                targetInputProps.Elements.find(item => {
                    let itemOption = rootElement.GetElementProperties(item);
                    //保留图片元素属性
                    if (itemOption.TypeName === "XTextImageElement") {
                        ElementsData.push(itemOption);
                    }
                });
            }
            if (currentCheck) {
                if (indexValue === -1 && row.type !== 'image') {
                    newValue.push(row.value);
                    newText.push(row.text);
                }
                if (row.type === 'image') {
                    var options = {
                        ID: 'dcTableSelectImage' + row.value,
                        src: row.text,
                        width: 50,
                        height: 50,
                    };
                    let hasCurrentInputImage = ElementsData.forEach(item => {
                        return (item.ID === options.ID);
                    });
                    if (!hasCurrentInputImage) {
                        ElementsData.push(options);
                    }
                }
            }
            if (!currentCheck) {
                if (indexValue > -1) {
                    newValue.splice(indexValue, 1);
                    newText.splice(indexText, 1);
                }
                if (row.type === 'image') {
                    let imgID = 'dcTableSelectImage' + row.value;
                    //重新获取一遍子元素，防止nativeHandle发生改变。删除图片元素
                    var InputProps = rootElement.GetElementProperties(rootElement.CurrentInputField());
                    //删除当前输入域元素下的子元素，防止删除错误（其他地方可能会有同id的图片元素）
                    InputProps.Elements.forEach((InputPropsitem, InputPropsindex) => {
                        var InputPropsitemOption = rootElement.GetElementProperties(InputPropsitem);
                        if (InputPropsitemOption.ID === imgID && InputPropsitemOption.TypeName === "XTextImageElement") {
                            rootElement.DeleteElement(InputPropsitemOption.NativeHandle);
                        }
                    });
                    ElementsData.forEach((imgItem, imgIndex) => {
                        if (imgItem.ID === imgID) {
                            //删除图片元素
                            ElementsData.splice(imgIndex, 1);
                        }
                    });
                }
            }
            rootElement.SetElementProperties(targetInputProps.NativeHandle, {
                Text: newText.join(','),
                innerValue: newValue.join(','),
            });
            rootElement.FocusElement(targetInputProps.NativeHandle);
            if (rootElement && rootElement.EventQueryTableListItemSelected && typeof rootElement.EventQueryTableListItemSelected === 'function') {
                rootElement.EventQueryTableListItemSelected({
                    Text: newText.join(','),
                    innerValue: newValue.join(','),
                });
            }

            //将原有的图片赋值
            if (ElementsData.length > 0) {
           
                ElementsData.forEach(item => {
                    rootElement.InsertImageByJSONText(item);
                    rootElement.FocusElement(targetInputProps.NativeHandle);
                });
            }
        } else {
            // 单选模式
            if (row.type === 'image') {
                rootElement.SetElementProperties(targetInputProps.NativeHandle, {
                    Text: null,
                    innerValue: row.value,
                });
                var options = {
                    ID: 'dcTableSelectImage' + row.value,
                    src: row.text,
                    width: 50,
                    height: 50,
                };
                rootElement.InsertImageByJSONText(options);
                rootElement.FocusElement(targetInputProps.NativeHandle);
                if (rootElement && rootElement.EventQueryTableListItemSelected && typeof rootElement.EventQueryTableListItemSelected === 'function') {
                    rootElement.EventQueryTableListItemSelected({
                        Text: null,
                        innerValue: row.value,
                    });
                }
            } else {
                rootElement.SetElementProperties(targetInputProps.NativeHandle, {
                    Text: row.text,
                    innerValue: row.value,
                });
                if (rootElement && rootElement.EventQueryTableListItemSelected && typeof rootElement.EventQueryTableListItemSelected === 'function') {
                    rootElement.EventQueryTableListItemSelected({
                        Text: row.text,
                        innerValue: row.value,
                    });
                }
            }
            that.CloseDropdownTable();
        }
    },
    /**
    * 样式设置
    */
    TableControlStyle: function (containerID, intPageIndex, intLeft, intTop, intHeight) {
        let rootElement = DCTools20221228.GetOwnerWriterControl(containerID);
        let targetInput = rootElement.GetElementProperties(rootElement.CurrentElement());
        let styleID = "DCTableControlStyle20240625151400";
        var styleDom = rootElement.querySelector('#' + styleID) || null;
        if (styleDom === null) {
            styleDom = rootElement.ownerDocument.createElement("style");
            styleDom.id = styleID;
            //根据光标解析位置
            var divCaret = rootElement.querySelector("#divCaret20221213");

            //获取元素位置
            var pageElement = WriterControl_Paint.GetCanvasElementByPageIndex(containerID, intPageIndex);//当前元素所在canvas元素
            var currentDomAbsBounds = rootElement.GetAbsBoundsInDocument(rootElement.CurrentElement());//当前元素位置
            var ZoomRate = rootElement.GetZoomRate();
            var left = pageElement.offsetLeft + parseFloat((currentDomAbsBounds.LeftInOwnerPage / 300 * 96.00001209449 * ZoomRate).toFixed(2));
            // [DUWRITER5_0-3348] 20240812 lxy 修复表格弹出框位置超出最大宽度
            //获取当前编辑器的最大可视范围
            var pageContainer = rootElement.querySelector('[dctype=page-container]');
            if ((left + 500) > pageContainer.clientWidth && (pageContainer.clientWidth > 550)) {
                //超出右边界,向左边界靠拢
                left = pageContainer.clientWidth - 500;
            }

            //offsetTop+元素的TopInOwnerPage（1/300英寸）
            var top = pageElement.offsetTop + parseFloat((currentDomAbsBounds.TopInOwnerPage / 300 * 96.00001209449 * ZoomRate).toFixed(2)) + parseFloat(divCaret.style.height);
            styleDom.innerHTML = `
                #DCTableControl20240625151400::-webkit-scrollbar{width: 8px;}
                #DCTableControl20240625151400::-webkit-scrollbar-track{background-color: #ddd;}
                #DCTableControl20240625151400::-webkit-scrollbar-thumb{background-color: #999;border-radius: 7px;}
                #DCTableControl20240625151400 div::-webkit-scrollbar{width: 8px}
                #DCTableControl20240625151400 div::-webkit-scrollbar-track{background-color: #ddd;}
                #DCTableControl20240625151400 div::-webkit-scrollbar-thumb{background-color: #999;border-radius: 7px;}
                #DCTableControl20240625151400{overscroll-behavior: contain}
                #DCTableControl20240625151400{
                    position: absolute;
                    top:${top}px;
                    left:${left}px;
                    z-index:9;
                    background-color: #fff;
                    border: 1px solid #ccc;
                    box-shadow: 0 0 10px rgba(0,0,0,0.3);
                    padding: 0;
                    box-sizing: border-box;
                    width: 500px;
                    max-height: 500px;
                    overflow: auto;
                    text-align: left;
                    display: flex;
                    flex-direction: column;
                }
                .dc_drop_down_input_table_box {
                    flex: 1;
                    overflow: auto;
                    padding: 10px;
                    box-sizing: border-box;
                }
                .dc_drop_down_input_table_repulsion_none_group.dc_drop_down_input_table_box .dc_drop_down_input_table_group{
                    box-shadow:none;
                    border:none;
                    border-radius:0;
                    margin-bottom:0;
                }
                .dc_drop_down_input_table_repulsion_none_group.dc_drop_down_input_table_box .dc_drop_down_input_table_title{
                    display:none;
                }
                .dc_drop_down_input_table_repulsion_none_group.dc_drop_down_input_table_box .dc_drop_down_input_table_content{
                    padding:2px;
                    box-sizing: border-box;
                }
                .dc_drop_down_input_table_footer_box {
                    width: 100%;
                    height: 60px;
                    border-top: 1px solid #ccc;
                    box-sizing: border-box;
                    padding: 10px 20px;
                    overflow: hidden;
                    display: flex;
                    align-items: center;
                    justify-content: flex-end;
                    flex-shrink: 0;
                }

                .dc_drop_down_input_table_table {
                    display: table;
                    width: 100%;
                    border-collapse: collapse;
                    font-size:13px;
                    font-family: Helvetica Neue,Helvetica,PingFang SC,Hiragino Sans GB,Microsoft YaHei,SimSun,sans-serif;
                }
                .dc_drop_down_input_table_row {
                    display: table-row;
                    cursor: pointer;
                }
                .dc_drop_down_input_table_row:hover{
                    background-color: #f5f7fa;
                }
                .dc_drop_down_input_table_cell {
                    display: table-cell;
                    box-sizing: border-box;
                    border: 1px solid #ebeef5;
                    padding: 5px 10px;
                    color: #606266;
                }
                .dc_drop_down_input_table_header {
                    font-weight: bold;
                    color: #909399;
                    background-color: #f5f7fa;
                }
                .dc_drop_down_input_table_row_active,.dc_drop_down_input_table_row_keyDown_active{
                    background-color: #f5f7fa;
                }
                .dc_drop_down_input_table_img{
                    width:16px;
                }
                .dc_drop_down_input_table_group{
                    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
                    width: 100%;
                    margin-bottom: 10px;
                    background-color: #fff;
                    border-radius: 4px;
                    border: 1px solid #ebeef5;
                    overflow: hidden;
                    transition: .3s;
                    box-sizing: border-box;
                }
                .dc_drop_down_input_table_title{
                    padding: 8px 16px;
                    border-bottom: 1px solid #ebeef5;
                    box-sizing: border-box;
                    color:#909399;
                    font-size: 14px;
                }
                .dc_drop_down_input_table_content {
                    padding: 10px 16px;
                    box-sizing: border-box;
                }
                .dc_drop_down_input_table_search_box {
                    width: 100%;
                    display: flex;
                    align-items: center;
                    border-bottom: 1px solid #ccc;
                    padding: 10px 20px;
                    box-sizing: border-box;
                }

                .dc_drop_down_input_table_search_input {
                    width: 320px;
                    height: 28px;
                    border: 1px solid #ccc;
                    font-size: 13px;
                    padding: 0 10px;
                    box-sizing: border-box;
                    border-radius: 2px;
                  
                }

                .dc_drop_down_input_table_search_input:focus {
                    outline: none;
                    border-color: #409eff;
                }

                .dc_drop_down_input_table_search_button{
                    margin:0 8px; 
                    color: #fff;
                    background-color: #409eff;
                    border: 1px solid #409eff;
                }
                .dc_drop_down_input_table_reset_button{
                    background-color: #FFFFFF;
                    border: 1px solid #ccc;
                }
                
                .dc_drop_down_input_table_search_button,
                .dc_drop_down_input_table_reset_button{
                    display: inline-block;
                    width: 60px;
                    height: 26px;
                    line-height: 26px;
                    text-align: center;
                    border-radius: 2px;
                    font-size: 13px;
                    cursor: pointer;
                }
                .dc_drop_down_input_table_search_button:hover{
                    background: #66b1ff;
                    border-color: #66b1ff;
                    color: #fff;
                }
                .dc_drop_down_input_table_reset_button:hover{
                    color: #409eff;
                    border-color: #c6e2ff;
                    background-color: #ecf5ff;
                }
                .dc_drop_down_input_table_page_total_box {
                    display: flex;
                    padding: 0 10px;
                }  
                .dc_drop_down_input_table_page_prev_box,
                .dc_drop_down_input_table_page_next_box,
                .dc_drop_down_input_table_page_item{
                    padding: 0 4px;
                    vertical-align: top;
                    font-size: 28px;
                    min-width: 30px;
                    height: 28px;
                    line-height: 22px;
                    cursor: pointer;
                    box-sizing: border-box;
                    text-align: center;
                    margin: 0 5px;
                    background-color: #f4f4f5;
                    color: #606266;
                    border-radius: 2px;
                    user-select: none;
                }  
                .dc_drop_down_input_table_page_prev_box:hover,
                .dc_drop_down_input_table_page_next_box:hover,
                .dc_drop_down_input_table_page_item:hover{
                    color: #409eff;
                }
                .dc_drop_down_input_table_page_prev_box_disabled,
                .dc_drop_down_input_table_page_next_box_disabled, 
                .dc_drop_down_input_table_page_prev_box.dc_drop_down_input_table_page_prev_box_disabled:hover,
                .dc_drop_down_input_table_page_next_box.dc_drop_down_input_table_page_next_box_disabled:hover{
                    color: #c0c4cc;
                    cursor: not-allowed;
                }
                .dc_drop_down_input_table_page_total_count{
                    color: #606266;
                }
                `;
            rootElement.appendChild(styleDom);
        }
    },
    /**
     * CloseDropdownTable 关闭下拉表格控件
    */
    CloseDropdownTable: function () {
        let tableControl = this.rootElement.querySelector("#DCTableControl20240625151400");
        let styleDom = this.rootElement.querySelector("#DCTableControlStyle20240625151400");
        if (tableControl) {
            tableControl.parentNode.removeChild(tableControl);
        }
        if (styleDom) {
            styleDom.parentNode.removeChild(styleDom);
        }
    },
};