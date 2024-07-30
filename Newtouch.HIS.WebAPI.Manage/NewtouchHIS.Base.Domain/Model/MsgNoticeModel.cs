using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Model
{
    public class MsgNoticeModel
    {
         

    }
    /// <summary>
    /// 病历质控消息模板
    /// </summary>
    public class MrqcMsgNoticeModel: MsgNoticeModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// 就诊号
        /// </summary>
        public string? jzh { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// 床号
        /// </summary>
        public string? ch { get; set; }
        /// <summary>
        /// 住院医生
        /// </summary>
        public string? zyys { get; set; }
        /// <summary>
        /// 质控类型
        /// </summary>
        public string? zklx { get; set; }
        /// <summary>
        /// 患者病历文档
        /// </summary>
        public string? hzwd { get; set; }
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string? fknr { get; set; }
        /// <summary>
        /// 严重等级
        /// </summary>
        public string? wtdj { get; set; }
        /// <summary>
        /// 处理限期
        /// </summary>
        public string? qxclsj { get; set; }
        /// <summary>
        /// 质控日期
        /// </summary>
        public string? zkrq { get; set; }

    }
}
