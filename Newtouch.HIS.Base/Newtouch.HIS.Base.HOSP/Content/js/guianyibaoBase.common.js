$.guianyibaoBase = {
    $yibaoBaseAX: $('#guianyibaoBaseAX')[0],
    /// <summary>
    /// 处理服务项目数据
    /// <summary>
    DealFwxm: function (psFolderPath, lsKeyWord, jdry) {
        return eval('(' + this.$yibaoBaseAX.DealFwxm(psFolderPath, lsKeyWord, jdry) + ')');
    }
};