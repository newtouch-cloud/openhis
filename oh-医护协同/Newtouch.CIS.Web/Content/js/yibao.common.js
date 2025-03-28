$.yibao = {
    $yibaoAX: $('#yibaoAX')[0],
    //读取卡信息
    GetCardInfo: function () {
        return eval('(' + this.$yibaoAX.GetCardInfo() + ')');
    },
    /// <summary>
    /// 门诊挂号
    /// </summary>
    /// <param name="DAC001">社保编号</param>
    /// <param name="AKE556">费用性质</param>
    /// <param name="AKE186">就诊类型</param>
    /// <param name="AKE162">就诊原因</param>
    /// <param name="DAE147">验证码（可为空）</param>
    /// <param name="AKE276">挂号/入院操作员编码</param>
    /// <param name="AKE277">挂号/入院操作员</param>
    /// <param name="AKE189">就诊序号/住院号</param>
    /// <param name="CUSTNAME">购药人姓名(自费人员必填)</param>
    RegClinic: function (DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) {
        return eval('(' + this.$yibaoAX.RegClinic(DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) + ')');
    },
    /// <summary>
    /// 挂号取消
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="DAC001"></param>
    /// <param name="AKE556"></param>
    DenyReg: function (DKE005, DAC001, AKE556) {
        return eval('(' + this.$yibaoAX.DenyReg(DKE005, DAC001, AKE556) + ')');
    },
    /// <summary>
    /// 明细记账
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="Detail"></param>
    PreAccount: function (DKE005, Detail) {
        return eval('(' + this.$yibaoAX.PreAccount(DKE005, Detail) + ')');
    },
    /// <summary>
    /// 清除记账
    /// </summary>
    /// <param name="DKE005"></param>
    ClearDetail: function (DKE005) {
        return eval('(' + this.$yibaoAX.ClearDetail(DKE005) + ')');
    },
    /// <summary>
    /// 门诊预结算
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="DAC001"></param>
    /// <param name="ZFY"></param>
    /// <param name="AKE162"></param>
    /// <param name="AKE181"></param>
    /// <param name="AKC195"></param>
    /// <param name="BKC036"></param>
    /// <param name="AKE194"></param>
    /// <param name="AKA120"></param>
    /// <param name="AKA121"></param>
    /// <param name="AKE279"></param>
    /// <param name="AKE278"></param>
    /// <param name="BKC038"></param>
    /// <param name="BKC039"></param>
    PreSettleclinic: function (DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) {
        return eval('(' + this.$yibaoAX.PreSettleclinic(DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) + ')');
    },
    /// <summary>
    /// 门诊结算确认
    /// </summary>
    /// <param name="DKE005">结算号</param>
    SettleClinic: function (DKE005) {
        return eval('(' + this.$yibaoAX.SettleClinic(DKE005) + ')');
    },
    /// <summary>
    /// 退费结算确认
    /// </summary>
    /// <param name="DKE005">结算号</param>
    /// <param name="DAC001">社保编号</param>
    /// <param name="AKE279">结算经办人编码</param>
    /// <param name="AKE278">结算经办人</param>
    DenySettle: function (DKE005, DAC001, AKE279, AKE278) {
        return eval('(' + this.$yibaoAX.DenySettle(DKE005, DAC001, AKE279, AKE278) + ')');
    },
    /// <summary>
    /// 查询未结算信息
    /// </summary>
    /// <param name="DAC001">社保编号</param>
    QUnSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QUnSettleFee(DAC001) + ')');
    },
    /// <summary>
    /// 查询已结算信息
    /// </summary>
    /// <param name="DAC001">社保编号</param>
    QSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QSettleFee(DAC001) + ')');
    }
};

$.shiqian = {
    $shiqianAX: $('#yibaoAX')[0],
    //门诊接诊历史信息
    MzjzInfo: function (Head, JzInfo) {
        this.$shiqianAX.MzjzInfo(Head, JzInfo);
    },
    //门诊录入诊断
    MzlrZDInfo: function (Head, ZD, ZDList) {
        this.$shiqianAX.MzlrZDInfo(Head, ZD, ZDList);
    },
    //门诊处方录入
    MzcfxxlrInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxlrInfo(Head, CFInfo, YP, XM);
    },
    //门诊处方保存
    MzcfxxSaveInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxSaveInfo(Head, CFInfo, YP, XM);
    }
};