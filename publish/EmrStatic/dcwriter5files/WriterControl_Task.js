"use strict";
export class WriterControlTaskList {
    constructor() {
        this.__Tasks = new Array();
    }
    AddTask(task) {
        this.__Tasks.push(task);
    }
    Run() {
        var funcRunTask = function ( thisList ) {
            if (typeof (thisList.__Timer_Run) == "number") {
                // 停止执行任务
                window.clearTimeout(thisList.__Timer_Run);
                WriterControl_Task.__Timer_Run = null;
            }
            var tasks2 = thisList.__Tasks;
            if (tasks2 != null && tasks2.length > 0) {
                var firstTask = null;
                if (tasks2.length == 1) {
                    // 只有一个任务，那就执行之。
                    firstTask = tasks2[0];
                }
                else {
                    // 获得优先级最高的任务
                    for (var iCount = 0; iCount < tasks2.length; iCount++) {
                        var item = tasks2[iCount];
                        if (typeof (item.Priority) != "number") {
                            item.Priority = 0;
                        }
                        if (firstTask == null || firstTask.Priority > item.Priority) {
                            firstTask = item;
                        }
                    }
                }
                var nextTaskTimeout = 5;
                if (firstTask != null) {
                    if (typeof (firstTask.WaitingTime) == "number") {
                        // 这个任务要等待较长时间才能执行
                        var startTime = firstTask.StartTimeValue;
                        if (typeof (startTime) != "number") {
                            startTime = new Date().valueOf();
                            firstTask.StartTimeValue = startTime;
                        }
                        var timeSpan = new Date().valueOf() - startTime;
                        if (timeSpan < firstTask.WaitingTime - 50) {
                            // 时辰未到
                            nextTaskTimeout = firstTask.WaitingTime - timeSpan;
                            firstTask = null;
                        }
                    }
                }
                if (firstTask != null) {
                    if (firstTask == tasks2[0]) {
                        tasks2.shift();
                    }
                    else {
                        tasks2.splice(tasks2.indexOf(firstTask), 1);
                    }
                    if (typeof (firstTask.RunTask) == "function") {
                        firstTask.RunTask.call(firstTask, firstTask);
                    }
                    else if (typeof (firstTask) == "function") {
                        firstTask.call(firstTask);
                    }
                }
                if (tasks2.length > 0) {
                    // 还有任务，继续执行
                    thisList.__Timer_Run = window.setTimeout(funcRunTask, nextTaskTimeout);
                }
                else {
                    // 所有任务已经完成
                    if (typeof (thisList.EventCompletedAll) == "function") {
                        thisList.EventCompletedAll.call(thisList, thisList);
                    }
                }
            }
        };
        this.__Timer_Run = window.setTimeout(funcRunTask, 5 , this);
    }
};
/**处理任务列表相关的模块 */
export let WriterControl_Task = {
    __Tasks: new Array(),
    /** 清空任务列表 
     * @param {string | Function} strTypeName 任务类型名称
     */
    ClearTask( strTypeName , ownerWriterControl) {
        var tasks = WriterControl_Task.__Tasks;
        if (tasks != null) {
            if (strTypeName == null || strTypeName.length == 0) {
                tasks.splice(0, tasks.length);
            }
            else {
                for (var iCount = tasks.length - 1; iCount >= 0; iCount--) {
                    var item = tasks[iCount];
                    if (item.TypeName == strTypeName
                        && item.OwnerWriterControl == ownerWriterControl) {
                        if (typeof (item.OnDeleted) == "function") {
                            item.OnDeleted.call(item);
                        }
                        tasks.splice(iCount, 1);
                    }
                }
            }
        }
    },
    /**
     * 添加任务
     * @param {any} task 任务对象,必须有RunTask()成员，建议有CanEatTask()成员
     * 
     */
    AddTask(task) {
        if (task == null) {
            return ;
        }
        if (typeof (task.RunTask) != "function" && typeof (task) != "function") {
            // 不是合格的任务对象
            return;
        }
        var tasks = WriterControl_Task.__Tasks;
        if (tasks == null) {
            tasks = new Array();
            WriterControl_Task.__Tasks = tasks;
        }
        if (typeof (WriterControl_Task.__Timer_Run) == "number") {
            // 停止执行任务
            window.clearTimeout(WriterControl_Task.__Timer_Run);
            WriterControl_Task.__Timer_Run = null;
        }
        var canAddItem = true;
        if (typeof (task.CanEatTask) == "function") {
            // 进行任务的合并
            for (var iCount = tasks.length - 1; iCount >= 0; iCount--) {
                var item = tasks[iCount];
                if (item == task) {
                    // 已经存在任务，不处理
                    canAddItem = false;
                    break;
                }
                if (task.CanEatTask.call(task, item) == true) {
                    if (typeof (item.OnDeleted) == "function") {
                        item.OnDeleted.call(item,task);
                    }
                    tasks.splice(iCount, 1);
                    if (typeof (item.Cancel) == "function") {
                        item.Cancel.call(item);
                    }
                }
                else if (typeof (item.CanEatTask) == "function") {
                    if (item.CanEatTask.call(item, task) == true) {
                        // 被已有任务吞并了，不处理。
                        if (typeof (task.OnDeleted) == "function") {
                            task.OnDeleted.call(task,item);
                        }
                        canAddItem = false;
                        break;
                    }
                }
            }//for
        }
        if (canAddItem == true) {
            // 添加新任务
            tasks.push(task);
        }
        if (tasks.length > 0) {
            // 执行任务
            var funcRunTask = function () {
                if (typeof (WriterControl_Task.__Timer_Run) == "number") {
                    // 停止执行任务
                    window.clearTimeout(WriterControl_Task.__Timer_Run);
                    WriterControl_Task.__Timer_Run = null;
                }
                var tasks2 = WriterControl_Task.__Tasks;
                if (tasks2 != null && tasks2.length > 0) {
                    var firstTask = null;
                    if (tasks2.length == 1) {
                        // 只有一个任务，那就执行之。
                        firstTask = tasks2[0];
                    }
                    else {
                        // 获得优先级最高的任务
                        for (var iCount = 0; iCount < tasks2.length; iCount++) {
                            var item = tasks2[iCount];
                            if (typeof (item.Priority) != "number") {
                                item.Priority = 0;
                            }
                            if (firstTask == null || firstTask.Priority > item.Priority) {
                                firstTask = item;
                            }
                        }
                    }
                    var nextTaskTimeout = 5;
                    if (firstTask != null) {
                        if (typeof (firstTask.WaitingTime) == "number") {
                            // 这个任务要等待较长时间才能执行
                            var startTime = firstTask.StartTimeValue;
                            if (typeof (startTime) != "number") {
                                startTime = new Date().valueOf();
                                firstTask.StartTimeValue = startTime;
                            }
                            var timeSpan = new Date().valueOf() - startTime;
                            if (timeSpan < firstTask.WaitingTime - 50 ) {
                                // 时辰未到
                                nextTaskTimeout = firstTask.WaitingTime - timeSpan;
                                firstTask = null;
                            }
                        }
                    }
                    if (firstTask != null) {
                        if (firstTask == tasks2[0]) {
                            tasks2.shift();
                        }
                        else {
                            tasks2.splice(tasks2.indexOf(firstTask), 1);
                        }
                        if (typeof (firstTask.RunTask) == "function") {
                            firstTask.RunTask.call(firstTask, firstTask);
                        }
                        else if (typeof (firstTask) == "function") {
                            firstTask.call(firstTask);
                        }
                    }
                    if (tasks2.length > 0) {
                        // 还有任务，继续执行
                        WriterControl_Task.__Timer_Run = window.setTimeout(funcRunTask, nextTaskTimeout);
                    }
                    else {
                        // 所有任务已经完成
                        var list = WriterControl_Task.__CallbacksForCompletedAllTasks;
                        WriterControl_Task.__CallbacksForCompletedAllTasks = null;
                        if (list != null && list.length > 0) {
                            for (var iCount = 0; iCount < list.length; iCount++) {
                                list[iCount].call();
                            }
                        }
                    }
                }
            };
            WriterControl_Task.__Timer_Run = window.setTimeout(funcRunTask, 5);
        }
    },
    /**
     * 所有任务执行完毕后的回调处理
     * @param {Function} func 回调函数
     */
    AddCallbackForCompletedAllTasks: function (func) {
        if (typeof (func) == "function") {
            var list = WriterControl_Task.__CallbacksForCompletedAllTasks;
            if (list == null) {
                list = new Array();
                WriterControl_Task.__CallbacksForCompletedAllTasks = list;
            }
            list.push(func);
        }
    },
    /**
     * 快速添加任务
     * @param {any} task
     */
    AddTaskFast: function (task) {
        var tasks = WriterControl_Task.__Tasks;
        if (tasks == null) {
            tasks = new Array();
            WriterControl_Task.__Tasks = tasks;
        }
        var index = tasks.indexOf(task);
        if (index < 0) {
            tasks.push(task);
        }
        else {
            console.log("已经存在任务");
        }
    }
}