$.guianyibao = {
    $yibaoAX: $('#guianyibaoAX')[0],
    /// <summary>
    ///修改密码（02）
    /// <summary>
    ModifyPassword: function () {
        return eval('(' + this.$yibaoAX.ModifyPassword() + ')');
    },
    /// <summary>
    /// 身份识别（03）
    /// <summary>
    Identification: function () {
        return eval('(' + this.$yibaoAX.Identification() + ')');
    },
    /// <summary>
    /// 编码对照信息获取（57）
    /// <summary>
    BmdzxxQuery: function () {
        return eval('(' + this.$yibaoAX.BmdzxxQuery() + ')');
    },
    /// <summary>
    /// 中心 ICD10 数据下载（61）
    /// <summary>
    UploadICD10: function (inDataJson) {
        return eval('(' + this.$yibaoAX.UploadICD10(inDataJson) + ')');
    },
    /// <summary>
    /// 普通门诊及慢性病结算 (48)
    /// <summary>
    MzFeejs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.MzFeejs(inDataJson) + ')');
    },
    /// <summary>
    /// 门诊结算回退(42) 
    /// </summary>
    MzJsht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.MzJsht(inDataJson) + ')');
    },
    /// <summary>
    /// 入院办理（21）
    /// </summary>
    ZyRybl: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyRybl(inDataJson) + ')');
    },
    /// <summary>
    /// 入院办理回退（22）
    /// </summary>
    ZyRyblht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyRyblht(inDataJson) + ')');
    },
    /// <summary>
    /// 入院办理信息修改（23）
    /// </summary>
    ZyRyblxxXg: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyRyblxxXg(inDataJson) + ')');
    },
    /// <summary>
    /// 门诊,住院 批量删除明细（33）
    /// </summary>
    DeleteMx: function (inDataJson) {
        return eval('(' + this.$yibaoAX.DeleteMx(inDataJson) + ')');
    },
    /// <summary>
    /// 住院费用明细写入（31）
    /// </summary>
    ZyFeemxXr: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyFeemxXr(DAC001) + ')');
    },
    /// <summary>
    /// 住院模拟结算(43)
    /// </summary>
    ZyMnjs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyMnjs(inDataJson) + ')');
    },
    /// <summary>
    /// 住院费用结算(41) 
    /// </summary>
    ZyFeejs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyFeejs(inDataJson) + ')');
    },
    /// <summary>
    /// 住院结算回退(42)
    /// </summary>
    ZyFeejsht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyFeejsht(inDataJson) + ')');
    },
    /// <summary>
    /// 出院办理（25）
    /// </summary>
    ZyCybl: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyCybl(inDataJson) + ')');
    },
    /// <summary>
    /// 出院办理回退（26）
    /// </summary>
    ZyCyblht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyCyblht(inDataJson) + ')');
    },
    /// <summary>
    /// 资源释放函数，在 HIS 应用退出时调用
    /// </summary>
    YibaoDestroy: function () {
        return eval('(' + this.$yibaoAX.YibaoDestroy() + ')');
    },
    /// <summary>
    /// 对于处理类交易，HIS 调用接口成功后完成 HIS 系统数据保存，则调用该交易进行中心确认处理
    /// </summary>
    /// str_jylsh：交易流水号，str_jyyzm：交易验证码 （下同）
    YibaoConfirm: function (str_jylsh, str_jyyzm) {
        return eval('(' + this.$yibaoAX.YibaoConfirm(str_jylsh, str_jyyzm) + ')');
    },
    /// <summary>
    /// 对于处理类交易，HIS 调用接口成功后未完成 HIS 系统数据保存，则调用该交易进行中心取消处理
    /// </summary>
    YibaoCancle: function (str_jylsh) {
        return eval('(' + this.$yibaoAX.YibaoCancle(str_jylsh) + ')');
    },
    /// <summary>
    /// 查询交易
    /// </summary>
    YibaoGetUncertaintyTrade: function (str_jylsh) {
        return eval('(' + this.$yibaoAX.YibaoGetUncertaintyTrade(str_jylsh) + ')');
    },
    /// <summary>
    /// 读身份证
    /// </summary>
    ReadIdCard: function () {
        return eval('(' + this.$yibaoAX.ReadIdCard() + ')');
    }
};