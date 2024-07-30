using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiBaoScheduling.Model
{
    public class UploadData
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string hisId { get; set; }
        /// <summary>
        /// 就诊ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// 参保地
        /// </summary>
        public string insuplc_admdvs { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string insutype { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string operatorId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string operatorName { get; set; }
        /// <summary>
        /// 就诊类型 身份证、社保卡、电子凭证
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string mdtrt_cert_no { get; set; }
        /// <summary>
        /// 病种编码
        /// </summary>
        public string dise_codg { get; set; }
        /// <summary>
        /// 病种名称
        /// </summary>
        public string dise_name { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 单次上传数
        /// </summary>
        public string uploadCount { get; set; }
        /// <summary>
        /// 费用截止时间
        /// </summary>
        public DateTime jssj { get; set; }
    }

    public class PreSett:UploadData
    {
        /// <summary>
        /// 医保总金额
        /// </summary>
        public decimal medfee_sumamt { get; set; }
        /// <summary>
        /// 个人结算方式
        /// </summary>
        public string psn_setlway { get; set; }
        /// <summary>
        /// 中途结算标志
        /// </summary>
        public string mid_setl_flag { get; set; }
        /// <summary>
        /// 个人账户使用标志
        /// </summary>
        public string acct_used_flag { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public string dscgTime { get; set; }
    }
}
