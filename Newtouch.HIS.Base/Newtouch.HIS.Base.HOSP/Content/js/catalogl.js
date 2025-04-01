function mlbh(mlbh)
{
    var colModel;
    switch (mlbh) {
        case "1301":
            colModel = [
                { label: '医疗目录编码', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: '名称', name: 'REG_NAME', width: 100, align: 'left' },
                { label: '编码', name: 'DRUGSTDCODE', width: 100, align: 'left' },
                { label: '药品剂型', name: 'DRUG_DOSFORM', width: 60, align: 'left' },
                { label: '剂型名称', name: 'DRUG_DOSFORM_NAME', width: 100, align: 'left',hidden:true },
                { label: '药品类别', name: 'DRUG_TYPE', width: 50, align: 'left' },
                { label: '药品规格', name: 'DRUG_SPEC', width: 50, align: 'left' }, 
                { label: '注册剂型', name: 'REG_DOSFORM', width: 60, align: 'left' },
                { label: '注册规格', name: 'REG_SPEC', width: 60, align: 'left' },
                { label: '每次用量', name: 'EACH_DOS', width: 100, align: 'left' },
                { label: '非处方药标志', name: 'OTC_FLAG', width: 20, align: 'left', hidden: true },
                { label: '包装材质', name: 'PACMATL', width: 50, align: 'left' },
                { label: '功能主治', name: 'EFCC_ATD', width: 100, align: 'left' },
                { label: '最小包装数量', name: 'MIN_PAC_CNT', width: 40, align: 'left' },
                { label: '最小包装单位', name: 'MIN_PACUNT', width: 40, align: 'left' },
                { label: '最小制剂单位', name: 'MIN_PREPUNT', width: 40, align: 'left' },
                { label: '药品有效期', name: 'DRUG_EXPY', width: 60, align: 'left' },
                { label: '最小计价单位', name: 'MIN_PRCUNT', width: 40, align: 'left' },
                { label: '生产企业编号', name: 'PRODENTP_CODE', width: 80, align: 'left' },
                { label: '生产企业名称', name: 'PRODENTP_NAME', width: 120, align: 'left' },
                { label: '市场状态', name: 'MKT_STAS', width: 50, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 20, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },

            ];
            break;
        case "1302":
            colModel = [
                { label: '医疗目录编码', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: '单味药名称', name: 'TCMHERB_NAME', width: 100, align: 'left' },
                { label: '药用部位', name: 'MED_PART', width: 100, align: 'left' },
                { label: '常规用法', name: 'CNVL_USED', width: 60, align: 'left' },
                { label: '性味', name: 'NATFLA', width: 100, align: 'left' },
                { label: '品种', name: 'CAT', width: 60, align: 'left' },
                { label: '开始日期', name: 'BEGNDATE', width: 100, align: 'left' },
                { label: '结束日期', name: 'ENDDATE', width: 80, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
                { label: '药材名称', name: 'MLMS_NAME', width: 60, align: 'left' },
                { label: '功能主治', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: '炮制方法', name: 'PSDG_MTD', width: 120, align: 'left' },
                { label: '功效分类', name: 'ECY_TYPE', width: 80, align: 'left' },
                { label: '药材种来源', name: 'MLMS_CAT_SOUC', width: 80, align: 'left' },
            ];
            break;
        case "1303":
            colModel = [
                 { label: '医疗目录编码', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: '药品商品名', name: 'DRUG_PRODNAME', width: 100, align: 'left' },
                { label: '剂型', name: 'DOSFORM', width: 80, align: 'left' },
                { label: '功能主治', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: '药品规格', name: 'DRUG_SPEC', width: 100, align: 'left' },
                { label: '每次用量', name: 'EACH_DOS', width: 100, align: 'left' },
                { label: '药品类别', name: 'DRUG_TYPE', width: 80, align: 'left' },
                { label: '包装材质', name: 'PACMATL', width: 100, align: 'left' },
                { label: '最小包装数量', name: 'MIN_PAC_CNT', width: 50, align: 'left' },
                { label: '最小包装单位', name: 'MIN_PACUNT', width: 50, align: 'left' },
                { label: '最小制剂单位', name: 'MIN_PREPUNT', width: 50, align: 'left' },
                { label: '生产企业名称', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: '生产企业地址', name: 'PSDG_MTD', width: 120, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: '开始时间', name: 'BEGNTIME', width: 80, align: 'left' },
                { label: '结束时间', name: 'ENDTIME', width: 80, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1305":
            colModel = [
                 { label: '医疗目录编码', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: '计价单位', name: 'PRCUNT', width: 60, align: 'left' },
                { label: '计价单位名称', name: 'PRCUNT_NAME', width: 100, align: 'left' },
                { label: '诊疗项目说明', name: 'TRT_ITEM_DSCR', width: 120, align: 'left' },
                { label: '诊疗除外内容', name: 'TRT_EXCT_CONT', width: 120, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: '服务项目类别', name: 'SERVITEM_TYPE', width: 80, align: 'left' },
                { label: '医疗服务项目名称', name: 'SERVITEM_NAME', width: 100, align: 'left' },
                { label: '开始日期', name: 'BEGNDATE', width: 80, align: 'left' },
                { label: '结束日期', name: 'ENDDATE', width: 80, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1306":
            colModel = [
                 { label: '医疗目录编码', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: '医保通用名', name: 'HI_GENNAME', width: 100, align: 'left' },
                { label: '规格', name: 'SPEC', width: 60, align: 'left' },
                { label: '耗材材质', name: 'MCS_MATL', width: 100, align: 'left' },
                { label: '开始日期', name: 'BEGNDATE', width: 100, align: 'left' },
                { label: '结束日期', name: 'ENDDATE', width: 100, align: 'left' },
                { label: '生产企业编号', name: 'PRODENTP_CODE', width: 100, align: 'left' },
                { label: '生产企业名称', name: 'PRODENTP_NAME', width: 120, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1307":
            colModel = [
                { label: '西医疾病诊断ID', name: 'DIAG_LIST_ID', width: 100, align: 'left', hidden:true },
                 { label: '章', name: 'CPR', width: 30, align: 'left' },
                { label: '章代码范围', name: 'CPR_CODE_SCP', width: 80, align: 'left' },
                { label: '章名称', name: 'CPR_NAME', width: 100, align: 'left' },
                { label: '节代码范围', name: 'SEC_CODE_SCP', width: 80, align: 'left' },
                { label: '节名称', name: 'SEC_NAME', width: 100, align: 'left' },
                { label: '类目代码', name: 'CGY_CODE', width: 60, align: 'left' },
                { label: '类目名称', name: 'CGY_NAME', width: 100, align: 'left' },
                { label: '亚目代码', name: 'SOR_CODE', width: 60, align: 'left' },
                { label: '亚目名称', name: 'SOR_NAME', width: 100, align: 'left' },
                { label: '诊断代码', name: 'DIAG_CODE', width: 80, align: 'left' },
                { label: '诊断名称', name: 'DIAG_NAME', width: 100, align: 'left' },
                { label: '使用标记', name: 'USED_STD', width: 40, align: 'left' },
                { label: '国标版诊断代码', name: 'NATSTD_DIAG_CODE', width: 80, align: 'left' },
                { label: '国标版诊断名称', name: 'NATSTD_DIAG_NAME', width: 100, align: 'left' },
                { label: '临床版诊断代码', name: 'CLNC_DIAG_CODE', width: 80, align: 'left' },
                { label: '临床版诊断名称', name: 'CLNC_DIAG_NAME', width: 100, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1308":
            colModel = [
                { label: '手术标准目录ID', name: 'OPRN_STD_LIST_ID', width: 130, align: 'left', hidden: true },
                 { label: '章', name: 'CPR', width: 30, align: 'left' },
                { label: '章代码范围', name: 'CPR_CODE_SCP', width: 60, align: 'left' },
                { label: '章名称', name: 'CPR_NAME', width: 100, align: 'left' },
                { label: '类目代码', name: 'CGY_CODE', width: 60, align: 'left' },
                { label: '类目名称', name: 'CGY_NAME', width: 100, align: 'left' },
                { label: '亚目代码', name: 'SOR_CODE', width: 60, align: 'left' },
                { label: '亚目名称', name: 'SOR_NAME', width: 100, align: 'left' },
                { label: '细目代码', name: 'DTLS_CODE', width: 60, align: 'left' },
                { label: '细目名称', name: 'DTLS_NAME', width: 100, align: 'left' },
                { label: '手术操作代码', name: 'OPRN_OPRT_CODE', width: 60, align: 'left' },
                { label: '手术操作名称', name: 'OPRN_OPRT_NAME', width: 100, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1309":
            colModel = [
                { label: '门慢门特病种目录代码', name: 'OPSP_DISE_CODE', width: 120, align: 'left' },
                { label: '门慢门特病种大类名称', name: 'OPSP_DISE_MAJCLS_NAME', width: 120, align: 'left' },
                { label: '门慢门特病种细分类名称', name: 'OPSP_DISE_SUBD_CLSS_NAME', width: 120, align: 'left' },
                { label: '医保区划', name: 'ADMDVS', width: 100, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: '门慢门特病种名称', name: 'OPSP_DISE_NAME', width: 120, align: 'left' },
                { label: '门慢门特病种大类代码', name: 'OPSP_DISE_MAJCLS_CODE', width: 80, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1310":
            colModel = [
                { label: '病种结算目录ID', name: 'DISE_SETL_LIST_ID', width: 100, align: 'left', hidden: true },
                 { label: '按病种结算病种目录代码', name: 'BYDISE_SETL_LIST_CODE', width: 100, align: 'left' },
                { label: '按病种结算病种名称', name: 'BYDISE_SETL_DISE_NAME', width: 120, align: 'left' },
                { label: '限定手术操作代码', name: 'QUA_OPRN_OPRT_CODE', width: 100, align: 'left' },
                { label: '限定手术操作名称', name: 'QUA_OPRN_OPRT_NAME', width: 120, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1311":
            colModel = [
                 { label: '日间手术治疗目录ID', name: 'DAYSRG_TRT_LIST_ID', width: 100, align: 'left', hidden: true },
                 { label: '日间手术病种目录代码', name: 'DAYSRG_DISE_LIST_CODE', width: 120, align: 'left' },
                { label: '日间手术病种名称', name: 'DAYSRG_DISE_NAME', width: 120, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: '手术操作名称', name: 'QUA_OPRN_OPRT_CODE', width: 100, align: 'left' },
                { label: '手术操作代码', name: 'QUA_OPRN_OPRT_NAME', width: 120, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1313":
            colModel = [
                { label: '肿瘤形态学ID', name: 'TMOR_MPY_ID', width: 100, align: 'left', hidden: true },
                 { label: '肿瘤/细胞类型代码', name: 'TMOR_CELL_TYPE_CODE', width: 120, align: 'left' },
                { label: '肿瘤/细胞类型', name: 'TMOR_CELL_TYPE', width: 120, align: 'left' },
                { label: '形态学分类代码', name: 'MPY_TYPE_CODE', width: 100, align: 'left' },
                { label: '形态学分类', name: 'MPY_TYPE', width: 100, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 80, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1314":
            colModel = [
                { label: '中医疾病诊断ID', name: 'TCM_DIAG_ID', width: 100, align: 'left', hidden: true },
                 { label: '中医疾病诊断', name: 'CATY_CGY_CODE', width: 100, align: 'left' },
                { label: '科别类目名称', name: 'CATY_CGY_NAME', width: 120, align: 'left' },
                { label: '专科系统分类目代码', name: 'SPCY_SYS_TAXA_CODE', width: 120, align: 'left' },
                { label: '专科系统分类目名称', name: 'SPCY_SYS_TAXA_NAME', width: 120, align: 'left' },
                { label: '疾病分类代码', name: 'DISE_TYPE_CODE', width: 100, align: 'left' },
                { label: '疾病分类名称', name: 'DISE_TYPE_NAME', width: 120, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 100, align: 'left' },
            ];
            break;
        case "1315":
            colModel = [
                { label: '中医证候ID', name: 'TCMSYMP_ID', width: 100, align: 'left', hidden: true },
                 { label: '证候类目代码', name: 'SDS_CGY_CODE', width: 100, align: 'left' },
                { label: '证候类目名称', name: 'SDS_CGY_NAME', width: 120, align: 'left' },
                { label: '证候属性代码', name: 'SDS_ATTR_CODE', width: 120, align: 'left' },
                { label: '证候属性', name: 'SDS_ATTR', width: 120, align: 'left' },
                { label: '证候分类代码', name: 'SDS_TYPE_CODE', width: 100, align: 'left' },
                { label: '证候分类名称', name: 'SDS_TYPE_NAME', width: 120, align: 'left' },
                { label: '有效标志', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: '版本号', name: 'VER', width: 60, align: 'left' },
                { label: '版本名称', name: 'VER_NAME', width: 100, align: 'left' },
            ];
            break;
            //查询类目录
        case "1304":
            colModel = [
                { label: '医疗目录编码', name: 'med_list_codg', width: 100, align: 'left', },
                { label: '药品商品名', name: 'drug_prodname', width: 100, align: 'left' },
                { label: '通用名编号', name: 'genname_codg', width: 120, align: 'left' },
                { label: '药品通用名', name: 'drug_genname', width: 120, align: 'left' },
                { label: '民族药种类', name: 'ethdrug_type', width: 100, align: 'left' },
                { label: '化学名称', name: 'chemname', width: 100, align: 'left' },
                { label: '别名', name: 'alis', width: 100, align: 'left' },
                { label: '英文名称', name: 'eng_name', width: 100, align: 'left' },
                { label: '剂型', name: 'dosform', width: 120, align: 'left' },
                { label: '每次用量', name: 'each_dos', width: 60, align: 'left' },
                { label: '使用频次', name: 'used_frqu', width: 60, align: 'left' },
                { label: '国家药品编号', name: 'nat_drug_no', width: 100, align: 'left' },
                { label: '用法', name: 'used_mtd', width: 100, align: 'left', },
                { label: '成分', name: 'ing', width: 80, align: 'left' },
                { label: '性状', name: 'chrt', width: 100, align: 'left' },
                { label: '不良反应', name: 'defs', width: 120, align: 'left' },
                { label: '禁忌', name: 'tabo', width: 100, align: 'left' },
                { label: '注意事项', name: 'mnan', width: 100, align: 'left' },
                { label: '贮藏', name: 'stog', width: 60, align: 'left' },
                { label: '药品规格', name: 'drug_spec', width: 60, align: 'left' },
                { label: '计价单位类型', name: 'prcunt_type', width: 60, align: 'left' },
                { label: '非处方药标志', name: 'otc_flag', width: 60, align: 'left' },
                { label: '包装材质', name: 'pacmatl', width: 60, align: 'left' },
                { label: '包装规格', name: 'pacspec', width: 60, align: 'left' },
                 { label: '最小使用单位', name: 'min_useunt', width: 60, align: 'left' },
                { label: '最小销售单位', name: 'min_salunt', width: 60, align: 'left' },
                { label: '说明书', name: 'manl', width: 60, align: 'left' },
                { label: '给药途径', name: 'rute', width: 60, align: 'left' },
                { label: '开始日期', name: 'begndate', width: 60, align: 'left' },
                { label: '结束日期', name: 'enddate', width: 60, align: 'left' },
                { label: '药理分类', name: 'pham_type', width: 60, align: 'left' },
                { label: '备注', name: 'memo', width: 60, align: 'left' },
                { label: '包装数量', name: 'pac_cnt', width: 60, align: 'left' },
                 { label: '最小计量单位', name: 'min_unt', width: 60, align: 'left' },
                { label: '最小包装数量', name: 'min_pac_cnt', width: 60, align: 'left' },
                 { label: '最小包装单位', name: 'min_pacunt', width: 60, align: 'left' },
                { label: '最小制剂单位', name: 'min_prepunt', width: 60, align: 'left' },
                { label: '药品有效期', name: 'drug_expy', width: 60, align: 'left' },
                { label: '功能主治', name: 'efcc_atd', width: 60, align: 'left' },
                { label: '最小计价单位', name: 'min_prcunt', width: 60, align: 'left' },
                { label: '五笔助记码', name: 'wubi', width: 60, align: 'left' },
                { label: '拼音助记码', name: 'pinyin', width: 60, align: 'left' },
                { label: '有效标志', name: 'vali_flag', width: 60, align: 'left' },
                { label: '唯一记录号', name: 'rid', width: 60, align: 'left' },
                { label: '数据创建时间', name: 'crte_time', width: 60, align: 'left' },
                { label: '数据更新时间', name: 'updt_time', width: 60, align: 'left' },
                { label: '创建人', name: 'crter_id', width: 60, align: 'left' },
                { label: '创建人姓名', name: 'crter_name', width: 60, align: 'left' },
                { label: '创建经办机构', name: 'crte_optins_no', width: 60, align: 'left' },
                { label: '经办人', name: 'opter_id', width: 60, align: 'left' },
                { label: '经办人姓名', name: 'opter_name', width: 60, align: 'left' },
                { label: '经办时间', name: 'opt_time', width: 60, align: 'left' },
                { label: '经办机构', name: 'optins_no', width: 60, align: 'left' },
                //{ label: '功能主治', name: 'efcc_atd', width: 100, align: 'left' },
                //{ label: '有效标志', name: 'vali_flag', width: 40, align: 'left' },
                { label: '版本号', name: 'ver', width: 60, align: 'left' }
            ];
            break;
        case "1312":
            colModel = [
                { label: '医保目录编码', name: 'hilist_code', width: 100, align: 'left' },
                { label: '医保目录名称', name: 'hilist_name', width: 100, align: 'left' },
                { label: '参保机构医保区划', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: '开始日期', name: 'begndate', width: 100, align: 'left' },
                { label: '结束日期', name: 'enddate', width: 100, align: 'left' },
                { label: '医疗收费项目类别', name: 'med_chrgitm_type', width: 60, align: 'left' },
                { label: '收费项目等级', name: 'chrgitm_lv', width: 60, align: 'left' },
                { label: '限制使用标志', name: 'lmt_used_flag', width: 60, align: 'left' },
                { label: '目录类别', name: 'list_type', width: 60, align: 'left' },
                { label: '医疗使用标志', name: 'med_use_flag', width: 100, align: 'left' },
                { label: '生育使用标志', name: 'matn_used_flag', width: 100, align: 'left' },
                { label: '医保目录使用类别', name: 'hilist_use_type', width: 100, align: 'left' },
                { label: '限复方使用类型', name: 'lmt_cpnd_type', width: 100, align: 'left' },
                { label: '五笔助记码', name: 'wubi', width: 100, align: 'left' },
                { label: '拼音助记码', name: 'pinyin', width: 100, align: 'left' },
                { label: '备注', name: 'memo', width: 100, align: 'left' },
                { label: '有效标志', name: 'vali_flag', width: 100, align: 'left' },
                { label: '唯一记录号', name: 'rid', width: 100, align: 'left' },
                { label: '更新时间', name: 'updt_time', width: 100, align: 'left' },
                { label: '创建人', name: 'crter_id', width: 100, align: 'left' },
                { label: '创建人姓名', name: 'crter_name', width: 100, align: 'left' },
                { label: '创建时间', name: 'crte_time', width: 100, align: 'left' },
                { label: '创建机构', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: '经办人', name: 'opter_id', width: 100, align: 'left' },
                { label: '经办人姓名', name: 'opter_name', width: 100, align: 'left' },
                { label: '经办时间', name: 'opt_time', width: 100, align: 'left' },
                { label: '经办机构', name: 'optins_no', width: 100, align: 'left' },
                { label: '统筹区', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1316":
            colModel = [
                { label: '医疗目录编码', name: 'med_list_codg', width: 100, align: 'left' },
                { label: '医保目录编码', name: 'hilist_code', width: 100, align: 'left' },
                { label: '目录类别', name: 'list_type', width: 100, align: 'left' },
                { label: '参保机构医保区划', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: '开始日期', name: 'begndate', width: 100, align: 'left' },
                { label: '结束日期', name: 'enddate', width: 60, align: 'left' },
                { label: '备注', name: 'memo', width: 60, align: 'left' },
                { label: '有效标志', name: 'vali_flag', width: 60, align: 'left' },
                { label: '唯一记录号', name: 'rid', width: 60, align: 'left' },
                { label: '更新时间', name: 'updt_time', width: 100, align: 'left' },
                { label: '创建人', name: 'crter_id', width: 100, align: 'left' },
                { label: '创建人姓名', name: 'crter_name', width: 100, align: 'left' },
                { label: '创建时间', name: 'crte_time', width: 100, align: 'left' },
                { label: '创建机构', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: '经办人', name: 'opter_id', width: 100, align: 'left' },
                { label: '经办人姓名', name: 'opter_name', width: 100, align: 'left' },
                { label: '经办时间', name: 'opt_time', width: 100, align: 'left' },
                { label: '经办机构', name: 'optins_no', width: 100, align: 'left' },
                { label: '统筹区', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1317":
            colModel = [
                { label: '定点医药机构编号', name: 'fixmedins_code', width: 100, align: 'left' },
                { label: '定点医药机构目录编号', name: 'medins_list_codg', width: 100, align: 'left' },
                { label: '定点医药机构目录名称', name: 'medins_list_name', width: 100, align: 'left' },
                { label: '参保机构医保区划', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: '目录类别', name: 'list_type', width: 100, align: 'left' },
                { label: '医疗目录编码', name: 'med_list_codg', width: 100, align: 'left' },
                { label: '开始日期', name: 'begndate', width: 60, align: 'left' },
                { label: '结束日期', name: 'enddate', width: 60, align: 'left' },
                { label: '批准文号', name: 'aprvno', width: 60, align: 'left' },
                { label: '剂型', name: 'dosform', width: 100, align: 'left' },
                { label: '除外内容', name: 'exct_cont', width: 100, align: 'left' },
                { label: '项目内涵', name: 'item_cont', width: 100, align: 'left' },
                { label: '计价单位', name: 'prcunt', width: 100, align: 'left' },
                { label: '规格', name: 'spec', width: 100, align: 'left' },
                { label: '包装规格', name: 'pacspec', width: 100, align: 'left' },
                { label: '备注', name: 'memo', width: 100, align: 'left' },
                { label: '有效标志', name: 'vali_flag', width: 100, align: 'left' },
                { label: '唯一记录号', name: 'rid', width: 100, align: 'left' },
                { label: '更新时间', name: 'updt_time', width: 100, align: 'left' },
                { label: '创建人', name: 'crter_id', width: 100, align: 'left' },
                { label: '创建人姓名', name: 'crter_name', width: 100, align: 'left' },
                { label: '创建时间', name: 'crte_time', width: 100, align: 'left' },
                { label: '创建机构', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: '经办人', name: 'opter_id', width: 100, align: 'left' },
                { label: '经办人姓名', name: 'opter_name', width: 100, align: 'left' },
                { label: '经办时间', name: 'opt_time', width: 100, align: 'left' },
                { label: '经办机构', name: 'optins_no', width: 100, align: 'left' },
                { label: '统筹区', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1318":
            colModel = [
                { label: '医保目录编码', name: 'hilist_code', width: 100, align: 'left' },
                { label: '医保目录限价类型', name: 'hilist_lmtpric_type', width: 100, align: 'left' },
                { label: '医保目录超限处理方式', name: 'overlmt_dspo_way', width: 100, align: 'left' },
                { label: '参保机构医保区划', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: '开始日期', name: 'begndate', width: 100, align: 'left' },
                { label: '结束日期', name: 'enddate', width: 100, align: 'left' },
                { label: '医保目录定价上限金额', name: 'hilist_pric_uplmt_amt', width: 60, align: 'left' },
                { label: '有效标志', name: 'vali_flag', width: 60, align: 'left' },
                { label: '唯一记录号', name: 'rid', width: 60, align: 'left' },
                { label: '更新时间', name: 'updt_time', width: 100, align: 'left' },
                { label: '创建人', name: 'crter_id', width: 100, align: 'left' },
                { label: '创建人姓名', name: 'crter_name', width: 100, align: 'left' },
                { label: '创建时间', name: 'crte_time', width: 100, align: 'left' },
                { label: '创建机构', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: '经办人', name: 'opter_id', width: 100, align: 'left' },
                { label: '经办人姓名', name: 'opter_name', width: 100, align: 'left' },
                { label: '经办时间', name: 'opt_time', width: 100, align: 'left' },
                { label: '经办机构', name: 'optins_no', width: 100, align: 'left' },
                { label: '表名', name: 'tabname', width: 100, align: 'left' },
                { label: '统筹区', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1319":
            colModel = [
                { label: '医保目录编码', name: 'hilist_code', width: 100, align: 'left' },
                { label: '医保目录自付比例人员类别', name: 'selfpay_prop_psn_type', width: 100, align: 'left' },
                { label: '目录自付比例类别', name: 'selfpay_prop_type', width: 100, align: 'left' },
                { label: '参保机构医保区划', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: '开始日期', name: 'begndate', width: 100, align: 'left' },
                { label: '结束日期', name: 'enddate', width: 100, align: 'left' },
                { label: '自付比例', name: 'selfpay_prop', width: 60, align: 'left' },
                { label: '有效标志', name: 'vali_flag', width: 60, align: 'left' },
                { label: '唯一记录号', name: 'rid', width: 60, align: 'left' },
                { label: '更新时间', name: 'updt_time', width: 100, align: 'left' },
                { label: '创建人', name: 'crter_id', width: 100, align: 'left' },
                { label: '创建人姓名', name: 'crter_name', width: 100, align: 'left' },
                { label: '创建时间', name: 'crte_time', width: 100, align: 'left' },
                { label: '创建机构', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: '经办人', name: 'opter_id', width: 100, align: 'left' },
                { label: '经办人姓名', name: 'opter_name', width: 100, align: 'left' },
                { label: '经办时间', name: 'opt_time', width: 100, align: 'left' },
                { label: '经办机构', name: 'optins_no', width: 100, align: 'left' },
                { label: '表名', name: 'tabname', width: 100, align: 'left' },
                { label: '统筹区', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1901":
            colModel = [
                { label: '医保目录编码', name: 'hilist_code', width: 100, align: 'left' },
                { label: '医保目录自付比例人员类别', name: 'selfpay_prop_psn_type', width: 100, align: 'left' },
                { label: '目录自付比例类别', name: 'selfpay_prop_type', width: 100, align: 'left' },
                { label: '参保机构医保区划', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: '开始日期', name: 'begndate', width: 100, align: 'left' },
                { label: '结束日期', name: 'enddate', width: 100, align: 'left' },
                { label: '自付比例', name: 'selfpay_prop', width: 60, align: 'left' },
                { label: '有效标志', name: 'vali_flag', width: 60, align: 'left' },
                { label: '唯一记录号', name: 'rid', width: 60, align: 'left' },
                { label: '更新时间', name: 'updt_time', width: 100, align: 'left' },
                { label: '创建人', name: 'crter_id', width: 100, align: 'left' },
                { label: '创建人姓名', name: 'crter_name', width: 100, align: 'left' },
                { label: '创建时间', name: 'crte_time', width: 100, align: 'left' },
                { label: '创建机构', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: '经办人', name: 'opter_id', width: 100, align: 'left' },
                { label: '经办人姓名', name: 'opter_name', width: 100, align: 'left' },
                { label: '经办时间', name: 'opt_time', width: 100, align: 'left' },
                { label: '经办机构', name: 'optins_no', width: 100, align: 'left' },
                { label: '表名', name: 'tabname', width: 100, align: 'left' },
                { label: '统筹区', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;

    }
    return colModel;
}