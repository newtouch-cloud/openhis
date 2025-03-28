$.yibao = {
    $yibaoAX: $('#yibaoAX')[0],
    //读取卡信息
    GetCardInfo: function (Cardtype) {
        return eval('(' + this.$yibaoAX.GetCardInfo(Cardtype) + ')');
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
    RegClinic: function (Cardtype, DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) {
        return eval('(' + this.$yibaoAX.RegClinic(Cardtype, DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) + ')');
    },
    /// <summary>
    /// 挂号取消
    /// </summary>
    /// <param name="DKE005">结算号</param>
    /// <param name="DAC001">社保编号</param>
    /// <param name="AKE556">费用性质</param>
    DenyReg: function (DKE005, DAC001, AKE556) {
        return eval('(' + this.$yibaoAX.DenyReg(DKE005, DAC001, AKE556) + ')');
    },
    /// <summary>
    /// 明细记帐
    /// </summary>
    /// <param name="DKE005">结算号</param>
    /// <param name="Detail">记账项目明细</param>
    PreAccount: function (DKE005, Detail) {
        return eval('(' + this.$yibaoAX.PreAccount(DKE005, Detail) + ')');
    },
    /// <summary>
    /// 清除记账
    /// </summary>
    /// <param name="DKE005">结算号</param>
    ClearDetail: function (DKE005) {
        return eval('(' + this.$yibaoAX.ClearDetail(DKE005) + ')');
    },
    /// <summary>
    /// 门诊预结算
    /// </summary>
    /// <param name="Cardtype">卡类型</param>
    /// <param name="DKE005">结算号</param>
    /// <param name="DAC001">社保编号</param>
    /// <param name="ZFY">医院端总费用</param>
    /// <param name="AKE162">就诊原因编码</param>
    /// <param name="AKE181">出院时间</param>
    /// <param name="AKC195">治疗效果</param>
    /// <param name="BKC036">出院医生编号</param>
    /// <param name="AKE194">出院医生姓名</param>
    /// <param name="AKA120">出院诊断主要疾病编码 ICD10</param>
    /// <param name="AKA121">出院诊断主要疾病名称</param>
    /// <param name="AKE279">结算经办人编码</param>
    /// <param name="AKE278">结算经办人</param>
    /// <param name="BKC038">出院病区(科室)编号</param>
    /// <param name="BKC039">出院病区(科室)名称</param>
    PreSettleclinic: function (Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) {
        return eval('(' + this.$yibaoAX.PreSettleclinic(Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
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
    /// 门诊结算确认完全流程接口
    /// <param name="Cardtype">卡类型</param>
    /// </summary>
    SettleClinicA: function (Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036, AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) {
        return eval('(' + this.$yibaoAX.SettleClinicA(Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036, AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) + ')');
    },
    /// <summary>
    /// 退费结算确认
    /// </summary>
    /// <param name="Cardtype">卡类型</param>
    /// <param name="DKE005">结算号</param>
    /// <param name="DAC001">社保编号</param>
    /// <param name="AKE279">结算经办人编码</param>
    /// <param name="AKE278">结算经办人</param>
    DenySettle: function (Cardtype, DKE005, DAC001, AKE279, AKE278) {
        return eval('(' + this.$yibaoAX.DenySettle(Cardtype, DKE005, DAC001, AKE279, AKE278) + ')');
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
    },
    /// <summary>
    /// 查询医保目录
    /// </summary>
    /// <param name="DKE085">项目类别</param>
    /// <param name="DKE166">社保端编码</param>
    /// <param name="DKE012">医院端编码</param>
    /// <param name="AKE002">项目名称</param>
    QCatalog: function (DKE085, DKE166, DKE012, AKE002) {
        return eval('(' + this.$yibaoAX.QCatalog(DKE085, DKE166, DKE012, AKE002) + ')');
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

$.printAX = {
    $printAX: $('#yibaoAX')[0],
    /// <summary>
    /// 发票打印
    /// </summary>
    /// <param name="InitJson">发票主体信息</param>
    /// <param name="InvoiceItemJson">发票明细信息</param>
    ///InitJson = "{InfoClientName:'张冬林',InfoClientTaxCode:'',InfoClientBankAccount:'',InfoClientAddressPhone:'16621173664',InfoSellerBankAccount:'',InfoSellerAddressPhone:'',InfoTaxRate:0,InfoNotes:'测试',InfoInvoicer:'美迪柯',InfoChecker:'',InfoCashier:'',InfoListName:'',InfoBillNumber:''}";
    ///InvoiceItemJson = "[{ListGoodsName:'挂号费',ListTaxItem:'4001',ListStandard:'',ListUnit:'项',ListNumber:1,ListPrice:1,ListAmount:1,ListPriceKind:0,ListTaxAmount:0}]";
    PrintInvoice: function (InitJson, InvoiceItemJson) {
        return eval('(' + this.$printAX.PrintInvoice(InitJson, InvoiceItemJson) + ')');
    },
    /// <summary>
    /// 已开发票作废
    /// </summary>
    /// <param name="InfoTypeCode">发票十位代码</param>
    /// <param name="InfoNumber">发票号码</param>
    CancelInvoice: function (InfoTypeCode, InfoNumber) {
        return eval('(' + this.$printAX.CancelInvoice(InfoTypeCode, InfoNumber) + ')');
    },
    /// <summary>
    /// 关闭金税卡
    /// </summary>
    CloseCard: function () {
        return eval('(' + this.$printAX.CloseCard() + ')');
    }
};