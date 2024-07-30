
//单选选中
function bstableSelected(table, uniqueId, values, $element) {
    //判断是否已选中
    if ($($element).hasClass("table-selected")) {
        $($element).removeClass('table-selected');
        bstableSelectedToggle(table, uniqueId, values, false);
    }
    else {
        $(".table-selected").removeClass('table-selected');
        $($element).addClass('table-selected');
        bstableSelectedToggle(table, uniqueId, values, true);
    }
}
//多选选中
function bstableMutiSelected(table, uniqueId, values, $element) {
    //判断是否已选中
    if ($($element).hasClass("table-selected")) {
        $($element).removeClass('table-selected');
        bstableSelectedToggle(table, uniqueId, values, false);
    }
    else {
        $($element).addClass('table-selected');
        bstableSelectedToggle(table, uniqueId, values, true);
    }
}
function bstableSelectedToggle(table, uniqueId, values, checked) {
    if (checked == true) {
        $(table).bootstrapTable('checkBy', { field: uniqueId, values: values });
    }
    else {
        $(table).bootstrapTable('uncheckBy', { field: uniqueId, values: values });
    }
}
//请求的参数
function pageQueryParams(params, paradata) {
    var data = {
        //每页多少条数据
        limit: params.limit,
        //请求第几页
        offset: params.offset,
        order: params.order,
        sort: params.sort,
        search: params.search,
        queryParams: paradata
    };
    return data;
}

/*********************************bootstrap-treeview********************************/
var getCheckableNodes = function ($bstree, checkableNodes, ignoreCase, exactMatch) {
    return $bstree.treeview('search', [checkableNodes, { ignoreCase: ignoreCase, exactMatch: exactMatch }]);
};
var checkNodes = function ($bstree, checkableNodes, silent) {
    return $bstree.treeview('checkNode', [checkableNodes, { silent: silent }]);
};
var uncheckNodes = function ($bstree, checkableNodes, silent) {
    return $bstree.treeview('uncheckNode', [checkableNodes, { silent: silent }]);
};
var toggleCheckNodes = function ($bstree, checkableNodes, silent) {
    return $bstree.treeview('toggleNodeChecked', [checkableNodes, { silent: silent }]);
};
var expandNodes = function ($bstree, expandibleNodes, levels, silent) {
    return $bstree.treeview('expandNode', [expandibleNodes, { levels: levels, silent: silent }]);
};
var collapseNodes = function ($bstree, expandibleNodes, silent, levels) {
    return $bstree.treeview('collapseNode', [expandibleNodes, {silent: silent }]);
};
var toggleNodeExpand = function ($bstree, expandibleNodes, silent, levels) {
    return $bstree.treeview('toggleNodeExpanded', [expandibleNodes, {silent: silent }]);
};

// 递归选中所有子节点
function checkSubNodes($bstree,node, value) {
    if (node.nodes && node.nodes.length > 0) {
        var children = $bstree.treeview('getSiblings', node.nodes[0].nodeId);
        children.push(node.nodes[0]);
        $.each(children, function (index, child) {
            $bstree.treeview('checkNode', [child.nodeId, { silent: true }]);
            if (child.nodes && child.nodes.length > 0) {
                checkSubNodes($bstree, child, value);
            }
        });
    }
}
function uncheckSubNodes($bstree, node, value,relParent) {
    if (node.nodes && node.nodes.length > 0) {
        var children = $bstree.treeview('getSiblings', node.nodes[0].nodeId);
        children.push(node.nodes[0]);
        $.each(children, function (index, child) {
            $bstree.treeview('uncheckNode', [child.nodeId, { silent: true }]);
            if (child.nodes && child.nodes.length > 0) {
                uncheckSubNodes($bstree, child, value);
            }
        });
    }
    //取消父节点选中状态
    else if (relParent) {
        var parent = $bstree.treeview('getParent', node.nodeId);
        if (parent) {
            $bstree.treeview('uncheckNode', [node.nodeId, { silent: true }]);
        }
    }
}
//选择节点选中，并展开父节点
function bsTreeCheckedNodeProc($bstree, selectNodes) {
    if (!selectNodes) {
        return false;
    }
    var expNodes = [];
    $.each(selectNodes, function (e) {
        checkNodes($bstree, this, true);
        var parent = $bstree.treeview('getParent', this);
        if (!!parent) {
            expNodes.push(parent);
        }

    });
    if (expNodes.length > 0) {
        expandNodes($bstree, expNodes, 99, true);
    }
};
