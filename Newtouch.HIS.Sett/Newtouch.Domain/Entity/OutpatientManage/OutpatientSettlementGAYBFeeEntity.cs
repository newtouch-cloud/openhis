using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.OutpatientManage
{
    [Table("mz_js_gaybjyfy")]
    public class OutpatientSettlementGAYBFeeEntity: IEntity<OutpatientSettlementGAYBFeeEntity>
    {
        /// <summary>
        /// 费用内码
        /// </summary>
        [Key]
        public int id { get; set; }
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
        public decimal? prm_yka055 { get; set; }
        /// <summary>
        /// 全自费金额
        /// </summary>
        public decimal? prm_yka056 { get; set; }
        /// <summary>
        /// 挂钩自费金额
        /// </summary>
        public decimal? prm_yka057 { get; set; }
        /// <summary>
        /// 符合范围金额
        /// </summary>
        public decimal? prm_yka111 { get; set; }
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
        public decimal? prm_yke030 { get; set; }
        /// <summary>
        /// 伤残人员医疗保障基金
        /// </summary>
        public decimal? prm_ake032 { get; set; }
        /// <summary>
        /// 民政补助基金
        /// </summary>
        public decimal? prm_ake181 { get; set; }
        /// <summary>
        /// 其他基金支付
        /// </summary>
        public decimal? prm_ake173 { get; set; }
        /// <summary>
        /// 本次支付后账户余额
        /// </summary>
        public decimal? prm_akc087 { get; set; }
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
        public DateTime? prm_aac006 { get; set; }
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
        /// 城乡居民人员类型
        /// </summary>
        public string prm_ykc299 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 结算内码
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// 结算回退返回的结算编号
        /// </summary>
        public string prm_yka198 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
