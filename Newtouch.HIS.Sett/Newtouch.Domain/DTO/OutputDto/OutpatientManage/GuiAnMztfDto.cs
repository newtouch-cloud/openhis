using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    public class GuiAnMztfDto
    {
    }

    public class InMzjshtDto
    {
        /// <summary>
        /// 就诊编号
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// 分中心编号
        /// </summary>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// 支付类别
        /// </summary>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// 结算编号
        /// </summary>
        public string prm_yka103 { get; set; }
        /// <summary>
        /// 经办人员编码
        /// </summary>
        public string prm_aae011 { get; set; }
        /// <summary>
        /// 经人人姓名
        /// </summary>
        public string prm_ykc141 { get; set; }
        /// <summary>
        /// 经办时间
        /// </summary>
        public string prm_aae036 { get; set; }
        /// <summary>
        /// 退费原因
        /// </summary>
        public string prm_aae013 { get; set; }
        /// <summary>
        /// 社会保险办法
        /// </summary>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string prm_aac001 { get; set; }
    }
    /// <summary>
    /// 前端传参到后台
    /// </summary>
    public class GuiAnMztfProDto
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
        /// 总金额
        /// </summary>
        public decimal? ZFY { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public decimal? XJZF { get; set; }

    }
}
