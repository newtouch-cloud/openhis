using System;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// 门诊结算金额支付相关
    /// </summary>
    public class OutpatientSettFeeRelatedDTO
    {
        /// <summary>
        /// 结算总金额（dj*sl之和）
        /// </summary>
        public decimal? zje { get; set; }
        /// <summary>
        /// 原支付应收
        /// </summary>
        public decimal? orglxjzfys { get; set; }
        /// <summary>
        /// 折扣比例
        /// </summary>
        public decimal? zkbl { get; set; }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal? zkje { get; set; }
        /// <summary>
        /// 支付应收（原支付应收打折后）
        /// </summary>
        public decimal? xjzfys { get; set; }
        /// <summary>
        /// 实收款
        /// </summary>
        public decimal? ssk { get; set; }
        public string zffs1 { get; set; }
        public decimal? zfje1 { get; set; }
        public string zffs2 { get; set; }
        public decimal? zfje2 { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public decimal? zhaoling { get; set; }
        /// <summary>
        /// 抵用交易流水号，重庆医保账户抵用
        /// </summary>
        public string dyjylsh { get; set; }

        #region new
        /// <summary>
        /// 预交金支付金额
        /// </summary>
        public decimal? yjjzfje { get; set; }
        /// <summary>
        /// 预交金可退余额
        /// </summary>
        public decimal? yjjtye { get; set; }
        /// <summary>
        /// 窗口实收金额
        /// </summary>
        public decimal? djjess { get; set; }
        /// <summary>
        /// 窗口实收支付方式
        /// </summary>
        public string djjesszffs { get; set; }

        #endregion
        /// <summary>
        /// 医保结算流水号 
        /// </summary>
        public string ybjslsh { get; set; }
        /// <summary>
        /// 电子凭证码
        /// </summary>
        public string ecToken { get; set; }
    }

    /// <summary>
    /// 门诊结算医保相关费用
    /// </summary>
    public class OutpatientSettYbFeeRelatedDTO
    {
        /// <summary>
        /// 总费用
        /// </summary>
        public decimal? ZFY { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public decimal? XJZF { get; set; }
        /// <summary>
        /// 一般费用（居保：医保费用）
        /// </summary>
        public decimal? YBFY { get; set; }
        /// <summary>
        /// 特殊费用
        /// </summary>
        public decimal? TSFY { get; set; }
        /// <summary>
        /// 基本支付（居保：账户支付）
        /// </summary>
        public decimal? JBZF { get; set; }
        /// <summary>
        /// 公补支付
        /// </summary>
        public decimal? GBZF { get; set; }
        /// <summary>
        /// 门诊自负段一般可报费用累计(居保起付线)
        /// </summary>
        public decimal? SUMZFDYBFY { get; set; }
        /// <summary>
        /// 门诊自负段内一般可报费用（居保：本次门诊自负段）
        /// </summary>
        public decimal? ZFDYBFY { get; set; }
        /// <summary>
        /// 基本余额
        /// </summary>
        public decimal? JBYE { get; set; }
        /// <summary>
        /// 公补余额
        /// </summary>
        public decimal? GBYE { get; set; }
        /// <summary>
        /// 可到中心报销一般费用
        /// </summary>
        public decimal? KBXYBFY { get; set; }
        /// <summary>
        /// 可到中心报销特殊费用
        /// </summary>
        public decimal? KBXTSFY { get; set; }


        /// <summary>
        /// （居保）统筹支付
        /// </summary>
        public decimal? TCZF { get; set; }
        /// <summary>
        /// （居保）救助(优抚)支付
        /// </summary>
        public decimal? JZZF { get; set; }
        /// <summary>
        /// （居保）大病补充支付
        /// </summary>
        public decimal? DKC023 { get; set; }

        /// <summary>
        /// 医保账户支出
        /// </summary>
        public decimal? YBZHZC
        {
            get
            {
                if (JBZF.HasValue || GBZF.HasValue)
                {
                    return (JBZF ?? 0) + (GBZF ?? 0);
                }
                return null;
            }
        }

    }
    /// <summary>
    /// 贵安和常熟医保返回字段合并
    /// </summary>
    public class OutpatientSettGAYbFeeRelatedDTO: OutpatientSettYbFeeRelatedDTO
    {

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string astr_jylsh { get; set; }
        /// <summary>
        /// 交易验证码
        /// </summary>
        public string astr_jyyzm { get; set; }
        /// <summary>
        /// 交易标志
        /// </summary>
        public long aint_appcode { get; set; }
        /// <summary>
        /// 交易信息
        /// </summary>
        public string astr_appmsg { get; set; }
        /// <summary>
        /// 就诊编号
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// 分中心编号
        /// </summary>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// 结算编号
        /// </summary>
        public string prm_yka103 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string prm_aac001 { get; set; }
        /// <summary>
        /// 个人账户支付金额
        /// </summary>
        public decimal? prm_yka065 { get; set; }
        /// <summary>
        /// 经办时间
        /// </summary>
        public DateTime? prm_aae036 { get; set; }
        /// <summary>
        /// 医疗费总额
        /// </summary>
        public decimal prm_yka055 { get; set; }
        /// <summary>
        /// 全自费金额
        /// </summary>
        public decimal prm_yka056 { get; set; }
        /// <summary>
        /// 挂钩自费金额
        /// </summary>
        public decimal prm_yka057 { get; set; }
        /// <summary>
        /// 符合范围金额
        /// </summary>
        public decimal prm_yka111 { get; set; }
        /// <summary>
        /// 进入起付线金额
        /// </summary>
        public decimal? prm_yka058 { get; set; }
        /// <summary>
        /// 基本医疗统筹支付金额
        /// </summary>
        public decimal? prm_yka248 { get; set; }
        /// <summary>
        /// 大额医疗支付金额
        /// </summary>
        public decimal? prm_yka062 { get; set; }
        /// <summary>
        /// 公务员补助报销金额
        /// </summary>
        public decimal prm_yke030 { get; set; }
        /// <summary>
        /// 伤残人员医疗保障基金
        /// </summary>
        public decimal prm_ake032 { get; set; }
        /// <summary>
        /// 民政补助基金
        /// </summary>
        public decimal prm_ake181 { get; set; }
        /// <summary>
        /// 其他基金支付
        /// </summary>
        public decimal prm_ake173 { get; set; }
        /// <summary>
        /// 本次支付后账户余额
        /// </summary>
        public decimal prm_akc087 { get; set; }
        /// <summary>
        /// 清算分中心
        /// </summary>
        public string prm_ykb037 { get; set; }
        /// <summary>
        /// 清算类别
        /// </summary>
        public string prm_yka316 { get; set; }
        /// <summary>
        /// 医疗人员类别
        /// </summary>
        public string prm_akc021 { get; set; }
        /// <summary>
        /// 公务员级别
        /// </summary>
        public string prm_ykc120 { get; set; }
        /// <summary>
        /// 参保所属分中心
        /// </summary>
        public string prm_yab139 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string prm_aac003 { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string prm_aac004 { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string prm_aac002 { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime prm_aac006 { get; set; }
        /// <summary>
        /// 实足年龄
        /// </summary>
        public decimal? prm_akc023 { get; set; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public string prm_aab001 { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string prm_aab004 { get; set; }
        /// <summary>
        /// 个人参保状态
        /// </summary>
        public string prm_aac031 { get; set; }
        /// <summary>
        /// 居民医疗人员类别
        /// </summary>
        public string prm_ykc280 { get; set; }
        /// <summary>
        /// 居民医疗人员身份
        /// </summary>
        public string prm_ykc281 { get; set; }
        /// <summary>
        /// 清算方式
        /// </summary>
        public string prm_yka054 { get; set; }
        /// <summary>
        /// 清算期号
        /// </summary>
        public string prm_yae366 { get; set; }
        /// <summary>
        /// 本年真实住院次数
        /// </summary>
        public decimal? prm_akc090 { get; set; }
        /// <summary>
        /// 基本统筹已累计金额
        /// </summary>
        public decimal? prm_yka120 { get; set; }
        /// <summary>
        /// 大额统筹已累计金额
        /// </summary>
        public decimal? prm_yka122 { get; set; }
        /// <summary>
        /// 公务员补助普通门诊起付年度累计（含慢性病）
        /// </summary>
        public decimal? prm_yka368 { get; set; }
        /// <summary>
        /// 本年公务员门诊补助累计额（含慢性病）
        /// </summary>
        public decimal? prm_yke025 { get; set; }
        /// <summary>
        /// 规定病种起付线累计
        /// </summary>
        public decimal? prm_yka900 { get; set; }
        /// <summary>
        /// 年度
        /// </summary>
        public decimal? prm_aae001 { get; set; }
        /// <summary>
        /// 单病种（结算）编码
        /// </summary>
        public string prm_yka089 { get; set; }
        /// <summary>
        /// 单病种（结算）病种名称
        /// </summary>
        public string prm_yka027 { get; set; }
        /// <summary>
        /// 单病种（结算）医疗机构自费费用
        /// </summary>
        public decimal? prm_yka028 { get; set; }
        /// <summary>
        /// 单病种（结算）包干标准
        /// </summary>
        public decimal? prm_yka345 { get; set; }
        /// <summary>
        /// 医保地类型，贵安：guian，常熟：changshu
        /// </summary>
        public string ybdlx { get; set; }


        //以下医保返回有值
        /// <summary>
        /// 
        /// </summary>
        public string prm_aka130 { get; set; }
        public string prm_ykc299 { get; set; }
        public string prm_yka128 { get; set; }
        public decimal? prm_sdxj { get; set; }
        public decimal? prm_bzxj { get; set; }
        public decimal? prm_ysxj { get; set; }

        
    }
}
